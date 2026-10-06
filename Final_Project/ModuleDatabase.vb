Imports MySql.Data.MySqlClient

Module ModuleDatabase
    Private db_server As String = "localhost"
    Private db_username As String = "root"
    Private db_password As String = ""
    Private db_port As String = "3306"
    Private db_name As String = "library"

    ' Kept for compatibility with older form logic. New code should use createDBConnection()
    ' so each form/query owns its own connection and cannot conflict with an open DataReader.
    Public sqlcon As New MySqlConnection

    Public Function createDBConnection() As MySqlConnection
        Dim connectionString As String =
            "server=" & db_server &
            "; port=" & db_port &
            "; user id=" & db_username &
            "; password=" & db_password &
            "; database=" & db_name & ";"

        Return New MySqlConnection(connectionString)
    End Function

    Public Sub connectDB()
        If sqlcon Is Nothing Then
            sqlcon = createDBConnection()
        End If

        If sqlcon.State = ConnectionState.Broken Then
            sqlcon.Close()
        End If

        If sqlcon.State = ConnectionState.Closed Then
            sqlcon.ConnectionString = createDBConnection().ConnectionString
            sqlcon.Open()
        End If
    End Sub

    Public Sub disconnectDB()
        If sqlcon IsNot Nothing AndAlso sqlcon.State <> ConnectionState.Closed Then
            sqlcon.Close()
        End If
    End Sub

    Public Sub ensureDatabaseSchema()
        Using conn As MySqlConnection = createDBConnection()
            conn.Open()

            EnsureColumn(conn, "books", "b_isActive",
                         "ALTER TABLE books ADD COLUMN b_isActive TINYINT(1) NOT NULL DEFAULT 1")

            EnsureColumn(conn, "users", "usr_role",
                         "ALTER TABLE users ADD COLUMN usr_role VARCHAR(20) NOT NULL DEFAULT 'Staff'")

            EnsureColumn(conn, "borrowed", "due_date",
                         "ALTER TABLE borrowed ADD COLUMN due_date DATE NULL AFTER borrow_date")

            ' Existing databases did not have roles. Ensure at least one active administrator exists.
            Using adminCountCmd As New MySqlCommand(
                "SELECT COUNT(*) FROM users WHERE isActive = 1 AND usr_role = 'Admin'", conn)
                If Convert.ToInt32(adminCountCmd.ExecuteScalar()) = 0 Then
                    Using promoteCmd As New MySqlCommand(
                        "UPDATE users SET usr_role = 'Admin' WHERE usr_id = (SELECT first_id FROM (SELECT MIN(usr_id) AS first_id FROM users WHERE isActive = 1) x)", conn)
                        promoteCmd.ExecuteNonQuery()
                    End Using
                End If
            End Using

            ' Backfill due dates for transactions created before the due-date feature.
            ' Rule: maximum three calendar days. If the third day falls on Saturday or
            ' Sunday, the due date is moved back to Friday.
            Using dueCmd As New MySqlCommand(
                "UPDATE borrowed SET due_date = CASE " &
                "WHEN DAYOFWEEK(DATE_ADD(borrow_date, INTERVAL 3 DAY)) = 7 " &
                "THEN DATE_SUB(DATE_ADD(borrow_date, INTERVAL 3 DAY), INTERVAL 1 DAY) " &
                "WHEN DAYOFWEEK(DATE_ADD(borrow_date, INTERVAL 3 DAY)) = 1 " &
                "THEN DATE_SUB(DATE_ADD(borrow_date, INTERVAL 3 DAY), INTERVAL 2 DAY) " &
                "ELSE DATE_ADD(borrow_date, INTERVAL 3 DAY) END " &
                "WHERE due_date IS NULL", conn)
                dueCmd.ExecuteNonQuery()
            End Using

            ' V4 originally handled Thursday only. Correct Wednesday loans that were
            ' previously stored as Saturday due dates, without touching custom dates.
            Using fixOldV4DueDatesCmd As New MySqlCommand(
                "UPDATE borrowed " &
                "SET due_date = DATE_ADD(borrow_date, INTERVAL 2 DAY) " &
                "WHERE DAYOFWEEK(borrow_date) = 4 " &
                "AND due_date = DATE_ADD(borrow_date, INTERVAL 3 DAY)", conn)
                fixOldV4DueDatesCmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub EnsureColumn(conn As MySqlConnection, tableName As String, columnName As String, alterSql As String)
        Dim query As String =
            "SELECT COUNT(*) FROM information_schema.COLUMNS " &
            "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @table_name AND COLUMN_NAME = @column_name"

        Using checkCmd As New MySqlCommand(query, conn)
            checkCmd.Parameters.AddWithValue("@table_name", tableName)
            checkCmd.Parameters.AddWithValue("@column_name", columnName)

            If Convert.ToInt32(checkCmd.ExecuteScalar()) = 0 Then
                Using alterCmd As New MySqlCommand(alterSql, conn)
                    alterCmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    Public Function calculateDueDate(borrowDate As Date) As Date
        Dim dueDate As Date = borrowDate.Date.AddDays(3)

        ' The loan may last up to three calendar days, but a due date must not
        ' fall on a weekend. Saturday/Sunday deadlines are moved back to Friday.
        If dueDate.DayOfWeek = DayOfWeek.Saturday Then
            dueDate = dueDate.AddDays(-1)
        ElseIf dueDate.DayOfWeek = DayOfWeek.Sunday Then
            dueDate = dueDate.AddDays(-2)
        End If

        Return dueDate
    End Function

    Public Function getLoanStatus(returnDate As Object, dueDate As Date, referenceDate As Date) As String
        If returnDate IsNot Nothing AndAlso returnDate IsNot DBNull.Value Then
            Return "Returned"
        End If

        If referenceDate.Date > dueDate.Date Then
            Return "Overdue"
        End If

        If referenceDate.Date = dueDate.Date Then
            Return "Due Today"
        End If

        Return "Borrowed"
    End Function

    Public Function tryParseCopyID(value As String, ByRef copyID As Integer) As Boolean
        copyID = 0

        If String.IsNullOrWhiteSpace(value) Then
            Return False
        End If

        Dim cleanValue As String = value.Trim().ToUpperInvariant()

        If cleanValue.StartsWith("COPY-") Then
            cleanValue = cleanValue.Substring(5)
        ElseIf cleanValue.StartsWith("BC-") Then
            cleanValue = cleanValue.Substring(3)
        End If

        cleanValue = cleanValue.Trim()
        Return Integer.TryParse(cleanValue, copyID) AndAlso copyID > 0
    End Function

    Public Function formatCopyBarcode(copyID As Integer) As String
        Return "COPY-" & copyID.ToString("000000")
    End Function

    Public Function isValidEmail(email As String) As Boolean
        Try
            Dim emailAddress As New System.Net.Mail.MailAddress(email)
            Return emailAddress.Address = email
        Catch
            Return False
        End Try
    End Function

    Public Function isValidPhone(phone As String) As Boolean
        If String.IsNullOrWhiteSpace(phone) Then
            Return False
        End If

        Dim cleanPhone As String = phone.Replace(" ", "").Replace("-", "")

        If cleanPhone.Length < 7 OrElse cleanPhone.Length > 15 Then
            Return False
        End If

        For Each digit As Char In cleanPhone
            If Not Char.IsDigit(digit) Then
                Return False
            End If
        Next

        Return True
    End Function
End Module
