USE library;

-- V4.1 due-date correction
-- Rule: the maximum loan period is 3 calendar days.
-- If day 3 falls on Saturday or Sunday, the book is due on Friday instead.
-- Examples:
-- Monday -> Thursday
-- Tuesday -> Friday
-- Wednesday -> Friday
-- Thursday -> Friday
-- Friday -> Monday

-- Fix records produced by the original V4 logic.
-- The only incorrect V4 case was Wednesday -> Saturday.
UPDATE borrowed
SET due_date = DATE_ADD(borrow_date, INTERVAL 2 DAY)
WHERE DAYOFWEEK(borrow_date) = 4
  AND due_date = DATE_ADD(borrow_date, INTERVAL 3 DAY);

-- Fill any missing due dates using the corrected rule.
UPDATE borrowed
SET due_date = CASE
    WHEN DAYOFWEEK(DATE_ADD(borrow_date, INTERVAL 3 DAY)) = 7
        THEN DATE_SUB(DATE_ADD(borrow_date, INTERVAL 3 DAY), INTERVAL 1 DAY)
    WHEN DAYOFWEEK(DATE_ADD(borrow_date, INTERVAL 3 DAY)) = 1
        THEN DATE_SUB(DATE_ADD(borrow_date, INTERVAL 3 DAY), INTERVAL 2 DAY)
    ELSE DATE_ADD(borrow_date, INTERVAL 3 DAY)
END
WHERE due_date IS NULL;
