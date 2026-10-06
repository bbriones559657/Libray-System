USE library;

-- Run this once on an existing database created before the soft-delete update.
-- It is safe to run repeatedly on MariaDB because of IF NOT EXISTS.
ALTER TABLE books
    ADD COLUMN IF NOT EXISTS b_isActive TINYINT(1) NOT NULL DEFAULT 1;

UPDATE books
SET b_isActive = 1
WHERE b_isActive IS NULL;
