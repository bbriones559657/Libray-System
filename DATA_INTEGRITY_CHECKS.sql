USE library;

-- 1) A physical copy marked Borrowed but with no active borrowing row.
SELECT bc.bc_id, bc.b_id, bc.bc_status
FROM bookcopies bc
LEFT JOIN borrowed br
    ON br.bc_id = bc.bc_id
   AND br.return_date IS NULL
WHERE bc.bc_status = 'Borrowed'
  AND br.brd_id IS NULL;

-- 2) An active borrowing row whose physical copy is not marked Borrowed.
SELECT br.brd_id, br.m_id, br.bc_id, bc.bc_status
FROM borrowed br
INNER JOIN bookcopies bc ON bc.bc_id = br.bc_id
WHERE br.return_date IS NULL
  AND bc.bc_status <> 'Borrowed';

-- 3) More than one active borrowing transaction for the same physical copy.
SELECT bc_id, COUNT(*) AS ActiveBorrowCount
FROM borrowed
WHERE return_date IS NULL
GROUP BY bc_id
HAVING COUNT(*) > 1;

-- 4) Inactive books that still have an active borrowing transaction.
SELECT DISTINCT b.b_id, b.b_title, br.brd_id, br.bc_id
FROM books b
INNER JOIN bookcopies bc ON bc.b_id = b.b_id
INNER JOIN borrowed br ON br.bc_id = bc.bc_id
WHERE b.b_isActive = 0
  AND br.return_date IS NULL;

-- 5) Inactive members that still have an active borrowing transaction.
SELECT DISTINCT m.m_id, m.m_firstName, m.m_LastName, br.brd_id, br.bc_id
FROM members m
INNER JOIN borrowed br ON br.m_id = m.m_id
WHERE m.m_isActive = 0
  AND br.return_date IS NULL;

-- 6) Borrowing transactions missing a due date.
SELECT brd_id, m_id, bc_id, borrow_date, due_date
FROM borrowed
WHERE due_date IS NULL;

-- 7) Due date earlier than borrow date.
SELECT brd_id, m_id, bc_id, borrow_date, due_date
FROM borrowed
WHERE due_date < borrow_date;

-- 8) No active Admin account (system should always have at least one).
SELECT 'NO ACTIVE ADMIN' AS Problem
WHERE NOT EXISTS (
    SELECT 1 FROM users WHERE isActive = 1 AND usr_role = 'Admin'
);

-- For a healthy V4 database, all eight checks above should return 0 rows.
