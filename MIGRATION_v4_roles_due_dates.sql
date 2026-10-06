USE library;

-- V4: roles + due dates.
-- Safe to run on the existing project database after checking whether the columns already exist.

SET @has_role := (
    SELECT COUNT(*) FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'users' AND COLUMN_NAME = 'usr_role'
);
SET @sql := IF(@has_role = 0,
    "ALTER TABLE users ADD COLUMN usr_role VARCHAR(20) NOT NULL DEFAULT 'Staff' AFTER usr_password",
    "SELECT 'usr_role already exists'");
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET @has_due := (
    SELECT COUNT(*) FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'borrowed' AND COLUMN_NAME = 'due_date'
);
SET @sql := IF(@has_due = 0,
    "ALTER TABLE borrowed ADD COLUMN due_date DATE NULL AFTER borrow_date",
    "SELECT 'due_date already exists'");
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- If an older database has no Admin yet, promote the first active account.
UPDATE users
SET usr_role = 'Admin'
WHERE usr_id = (
    SELECT first_id FROM (
        SELECT MIN(usr_id) AS first_id
        FROM users
        WHERE isActive = 1
    ) x
)
AND NOT EXISTS (
    SELECT 1 FROM (
        SELECT usr_id FROM users WHERE usr_role = 'Admin' AND isActive = 1 LIMIT 1
    ) a
);

-- Due-date rule:
-- maximum loan period = 3 calendar days
-- if the third day falls on Saturday/Sunday, move the due date back to Friday
UPDATE borrowed
SET due_date = CASE
    WHEN DAYOFWEEK(DATE_ADD(borrow_date, INTERVAL 3 DAY)) = 7
        THEN DATE_SUB(DATE_ADD(borrow_date, INTERVAL 3 DAY), INTERVAL 1 DAY)
    WHEN DAYOFWEEK(DATE_ADD(borrow_date, INTERVAL 3 DAY)) = 1
        THEN DATE_SUB(DATE_ADD(borrow_date, INTERVAL 3 DAY), INTERVAL 2 DAY)
    ELSE DATE_ADD(borrow_date, INTERVAL 3 DAY)
END
WHERE due_date IS NULL;

-- Correct rows created by the first V4 rule, where Wednesday loans were
-- incorrectly due on Saturday. This only changes that exact old V4 pattern.
UPDATE borrowed
SET due_date = DATE_ADD(borrow_date, INTERVAL 2 DAY)
WHERE DAYOFWEEK(borrow_date) = 4
  AND due_date = DATE_ADD(borrow_date, INTERVAL 3 DAY);
