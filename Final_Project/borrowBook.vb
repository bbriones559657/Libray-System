Imports MySql.Data.MySqlClient

Public Class borrowBook
    Private Sub borrowBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnBorrow
        Label1.Text = "Physical Copy ID / Barcode"
    End Sub

    Private Sub btnBorrow_Click(sender As Object, e As EventArgs) Handles btnBorrow.Click
        Dim memberID As Integer
        Dim copyID As Integer

        If Not Integer.TryParse(txtMemberID.Text.Trim(), memberID) OrElse memberID <= 0 Then
            MsgBox("Please enter a valid Patron Member ID.", vbOKOnly Or vbExclamation, "Warning")
            txtMemberID.Focus()
            Return
        End If

        If Not tryParseCopyID(txtCopyID.Text, copyID) Then
            MsgBox("Please enter a valid Physical Copy ID or barcode, for example 5001 or COPY-005001.",
                   vbOKOnly Or vbExclamation, "Warning")
            txtCopyID.Focus()
            Return
        End If

        Dim transaction As MySqlTransaction = Nothing
        Dim transactionID As Integer = 0
        Dim memberName As String = ""
        Dim bookID As Integer = 0
        Dim bookTitle As String = ""
        Dim isbn As String = ""
        Dim borrowDate As Date = Date.Today
        Dim dueDate As Date = Date.Today

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()
                transaction = conn.BeginTransaction()

                Dim memberFound As Boolean = False
                Dim memberActive As Boolean = False
                Dim memberQuery As String =
                    "SELECT m_isActive, TRIM(CONCAT_WS(' ', m_firstName, NULLIF(m_middleName, ''), m_LastName)) AS MemberName " &
                    "FROM members WHERE m_id = @member_id LIMIT 1 FOR UPDATE"

                Using memberCmd As New MySqlCommand(memberQuery, conn, transaction)
                    memberCmd.Parameters.AddWithValue("@member_id", memberID)

                    Using reader As MySqlDataReader = memberCmd.ExecuteReader()
                        If reader.Read() Then
                            memberFound = True
                            memberActive = Convert.ToBoolean(reader("m_isActive"))
                            memberName = reader("MemberName").ToString()
                        End If
                    End Using
                End Using

                If Not memberFound Then
                    transaction.Rollback()
                    transaction = Nothing
                    MsgBox("Member ID was not found.", vbOKOnly Or vbExclamation, "Member Not Found")
                    Return
                End If

                If Not memberActive Then
                    transaction.Rollback()
                    transaction = Nothing
                    MsgBox("This member account is inactive and cannot borrow books.", vbOKOnly Or vbExclamation, "Inactive Member")
                    Return
                End If

                Dim copyFound As Boolean = False
                Dim copyStatus As String = ""
                Dim bookActive As Boolean = False
                Dim copyQuery As String =
                    "SELECT bc.bc_status, b.b_isActive, b.b_id, b.b_title, b.b_isbn " &
                    "FROM bookcopies bc " &
                    "INNER JOIN books b ON bc.b_id = b.b_id " &
                    "WHERE bc.bc_id = @copy_id LIMIT 1 FOR UPDATE"

                Using copyCmd As New MySqlCommand(copyQuery, conn, transaction)
                    copyCmd.Parameters.AddWithValue("@copy_id", copyID)

                    Using reader As MySqlDataReader = copyCmd.ExecuteReader()
                        If reader.Read() Then
                            copyFound = True
                            copyStatus = reader("bc_status").ToString()
                            bookActive = Convert.ToBoolean(reader("b_isActive"))
                            bookID = Convert.ToInt32(reader("b_id"))
                            bookTitle = reader("b_title").ToString()
                            isbn = reader("b_isbn").ToString()
                        End If
                    End Using
                End Using

                If Not copyFound Then
                    transaction.Rollback()
                    transaction = Nothing
                    MsgBox("Physical Copy ID / barcode was not found.", vbOKOnly Or vbExclamation, "Copy Not Found")
                    Return
                End If

                If Not bookActive Then
                    transaction.Rollback()
                    transaction = Nothing
                    MsgBox("This book is inactive and cannot be borrowed.", vbOKOnly Or vbExclamation, "Inactive Book")
                    Return
                End If

                If Not String.Equals(copyStatus, "Available", StringComparison.OrdinalIgnoreCase) Then
                    transaction.Rollback()
                    transaction = Nothing
                    MsgBox("This physical copy is currently " & copyStatus & ".", vbOKOnly Or vbExclamation, "Book Not Available")
                    Return
                End If

                Using activeCmd As New MySqlCommand(
                    "SELECT COUNT(*) FROM borrowed WHERE bc_id = @copy_id AND return_date IS NULL", conn, transaction)
                    activeCmd.Parameters.AddWithValue("@copy_id", copyID)
                    If Convert.ToInt32(activeCmd.ExecuteScalar()) > 0 Then
                        transaction.Rollback()
                        transaction = Nothing
                        MsgBox("This physical copy already has an active borrowing transaction.", vbOKOnly Or vbExclamation, "Book Not Available")
                        Return
                    End If
                End Using

                Using updateCmd As New MySqlCommand(
                    "UPDATE bookcopies SET bc_status = 'Borrowed' WHERE bc_id = @copy_id AND bc_status = 'Available'", conn, transaction)
                    updateCmd.Parameters.AddWithValue("@copy_id", copyID)

                    If updateCmd.ExecuteNonQuery() <> 1 Then
                        transaction.Rollback()
                        transaction = Nothing
                        MsgBox("The copy is no longer available. Refresh and try again.", vbOKOnly Or vbExclamation, "Book Not Available")
                        Return
                    End If
                End Using

                Using dateCmd As New MySqlCommand("SELECT CURDATE()", conn, transaction)
                    borrowDate = Convert.ToDateTime(dateCmd.ExecuteScalar())
                End Using

                dueDate = calculateDueDate(borrowDate)

                Dim insertQuery As String =
                    "INSERT INTO borrowed (m_id, bc_id, borrow_date, due_date, return_date) " &
                    "VALUES (@member_id, @copy_id, @borrow_date, @due_date, NULL)"

                Using insertCmd As New MySqlCommand(insertQuery, conn, transaction)
                    insertCmd.Parameters.AddWithValue("@member_id", memberID)
                    insertCmd.Parameters.AddWithValue("@copy_id", copyID)
                    insertCmd.Parameters.AddWithValue("@borrow_date", borrowDate.Date)
                    insertCmd.Parameters.AddWithValue("@due_date", dueDate.Date)
                    insertCmd.ExecuteNonQuery()
                    transactionID = Convert.ToInt32(insertCmd.LastInsertedId)
                End Using

                transaction.Commit()
                transaction = Nothing
            End Using

            If Dashboard.Visible Then
                Dashboard.refreshTransactions()
            End If

            Dim receiptText As String =
                "LIBRARY BORROW RECEIPT" & Environment.NewLine &
                New String("-"c, 40) & Environment.NewLine &
                "Transaction ID : " & transactionID & Environment.NewLine &
                "Member ID      : " & memberID & Environment.NewLine &
                "Member Name    : " & memberName & Environment.NewLine &
                "Book ID        : " & bookID & Environment.NewLine &
                "Book Title     : " & bookTitle & Environment.NewLine &
                "ISBN           : " & isbn & Environment.NewLine &
                "Copy ID        : " & copyID & Environment.NewLine &
                "Copy Barcode   : " & formatCopyBarcode(copyID) & Environment.NewLine &
                "Borrow Date    : " & borrowDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                "Due Date       : " & dueDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                "Status         : BORROWED" & Environment.NewLine &
                New String("-"c, 40) & Environment.NewLine &
                "Return the exact physical copy shown above."

            txtMemberID.Clear()
            txtCopyID.Clear()
            txtMemberID.Focus()

            Using frm As New receiptForm("Borrow Receipt", receiptText)
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            If transaction IsNot Nothing Then
                Try
                    transaction.Rollback()
                Catch
                End Try
            End If

            MsgBox("Unable to process the borrowing transaction." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub
End Class
