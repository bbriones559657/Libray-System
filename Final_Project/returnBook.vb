Imports MySql.Data.MySqlClient

Public Class returnBook
    Private pendingCopyID As Integer = 0
    Private pendingBorrowID As Integer = 0
    Private pendingMemberID As Integer = 0
    Private pendingMemberName As String = ""
    Private pendingBookID As Integer = 0
    Private pendingBookTitle As String = ""
    Private pendingISBN As String = ""
    Private pendingBorrowDate As Date
    Private pendingDueDate As Date

    Private ReadOnly grpBorrower As New GroupBox()
    Private ReadOnly lblBorrowerDetails As New Label()

    Private Sub returnBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnReturn
        Label1.Text = "Physical Copy ID / Barcode"
        initializeBorrowerDetails()
        resetReturnState(False)
    End Sub

    Private Sub initializeBorrowerDetails()
        grpBorrower.Text = "Borrower / Book Details"
        grpBorrower.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        grpBorrower.ForeColor = Color.DarkSlateGray
        grpBorrower.Location = New Point(25, 135)
        grpBorrower.Size = New Size(240, 175)

        lblBorrowerDetails.AutoSize = False
        lblBorrowerDetails.Font = New Font("Segoe UI", 9.0F)
        lblBorrowerDetails.ForeColor = Color.DarkSlateGray
        lblBorrowerDetails.Location = New Point(10, 24)
        lblBorrowerDetails.Size = New Size(220, 140)
        lblBorrowerDetails.Text = "Enter or scan a copy barcode, then click FIND BORROWER."

        grpBorrower.Controls.Add(lblBorrowerDetails)
        Me.Controls.Add(grpBorrower)

        btnReturn.Location = New Point(25, 325)
        btnReturn.Size = New Size(240, 45)
        Me.ClientSize = New Size(290, 395)
    End Sub

    Private Sub txtCopyID_TextChanged(sender As Object, e As EventArgs) Handles txtCopyID.TextChanged
        If pendingCopyID = 0 Then
            Return
        End If

        Dim enteredCopyID As Integer
        If Not tryParseCopyID(txtCopyID.Text, enteredCopyID) OrElse enteredCopyID <> pendingCopyID Then
            resetReturnState(False)
        End If
    End Sub

    Public Sub PrepareCopyForReturn(copyID As Integer)
        txtCopyID.Text = formatCopyBarcode(copyID)
        If Me.IsHandleCreated AndAlso Me.Visible Then
            loadBorrowerDetails(copyID)
        Else
            pendingCopyID = 0
            pendingBorrowID = 0
        End If
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Dim copyID As Integer

        If Not tryParseCopyID(txtCopyID.Text, copyID) Then
            MsgBox("Please enter a valid Physical Copy ID or barcode, for example 5001 or COPY-005001.",
                   vbOKOnly Or vbExclamation, "Warning")
            txtCopyID.Focus()
            Return
        End If

        If pendingCopyID = copyID AndAlso pendingBorrowID > 0 Then
            confirmReturn(copyID)
        Else
            loadBorrowerDetails(copyID)
        End If
    End Sub

    Private Sub loadBorrowerDetails(copyID As Integer)
        Dim found As Boolean = False

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim copyExistsQuery As String = "SELECT COUNT(*) FROM bookcopies WHERE bc_id = @copy_id"
                Using copyCmd As New MySqlCommand(copyExistsQuery, conn)
                    copyCmd.Parameters.AddWithValue("@copy_id", copyID)
                    If Convert.ToInt32(copyCmd.ExecuteScalar()) = 0 Then
                        resetReturnState(False)
                        MsgBox("Physical Copy ID / barcode was not found.", vbOKOnly Or vbExclamation, "Copy Not Found")
                        Return
                    End If
                End Using

                Dim borrowQuery As String =
                    "SELECT br.brd_id, br.borrow_date, br.due_date, " &
                    "m.m_id, TRIM(CONCAT_WS(' ', m.m_firstName, NULLIF(m.m_middleName, ''), m.m_LastName)) AS MemberName, " &
                    "b.b_id, b.b_title, b.b_isbn " &
                    "FROM borrowed br " &
                    "INNER JOIN members m ON br.m_id = m.m_id " &
                    "INNER JOIN bookcopies bc ON br.bc_id = bc.bc_id " &
                    "INNER JOIN books b ON bc.b_id = b.b_id " &
                    "WHERE br.bc_id = @copy_id AND br.return_date IS NULL " &
                    "ORDER BY br.brd_id DESC LIMIT 1"

                Using borrowCmd As New MySqlCommand(borrowQuery, conn)
                    borrowCmd.Parameters.AddWithValue("@copy_id", copyID)

                    Using reader As MySqlDataReader = borrowCmd.ExecuteReader()
                        If reader.Read() Then
                            found = True
                            pendingCopyID = copyID
                            pendingBorrowID = Convert.ToInt32(reader("brd_id"))
                            pendingMemberID = Convert.ToInt32(reader("m_id"))
                            pendingMemberName = reader("MemberName").ToString()
                            pendingBookID = Convert.ToInt32(reader("b_id"))
                            pendingBookTitle = reader("b_title").ToString()
                            pendingISBN = reader("b_isbn").ToString()
                            pendingBorrowDate = Convert.ToDateTime(reader("borrow_date"))
                            pendingDueDate = Convert.ToDateTime(reader("due_date"))
                        End If
                    End Using
                End Using
            End Using

            If Not found Then
                resetReturnState(False)
                MsgBox("This physical copy does not have an active borrowing transaction.", vbOKOnly Or vbExclamation, "Nothing to Return")
                Return
            End If

            lblBorrowerDetails.Text =
                "Member ID: " & pendingMemberID & Environment.NewLine &
                "Borrower: " & pendingMemberName & Environment.NewLine &
                "Book ID: " & pendingBookID & Environment.NewLine &
                "Book: " & pendingBookTitle & Environment.NewLine &
                "Copy: " & formatCopyBarcode(pendingCopyID) & Environment.NewLine &
                "Borrowed: " & pendingBorrowDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                "Due: " & pendingDueDate.ToString("yyyy-MM-dd") &
                If(Date.Today > pendingDueDate.Date, "  (OVERDUE)", "")

            btnReturn.Text = "CONFIRM RETURN"
            btnReturn.Focus()
        Catch ex As Exception
            resetReturnState(False)
            MsgBox("Unable to load borrower details." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub confirmReturn(copyID As Integer)
        Dim transaction As MySqlTransaction = Nothing
        Dim returnDate As Date = Date.Today
        Dim receiptTransactionID As Integer = pendingBorrowID
        Dim receiptMemberID As Integer = pendingMemberID
        Dim receiptMemberName As String = pendingMemberName
        Dim receiptBookID As Integer = pendingBookID
        Dim receiptBookTitle As String = pendingBookTitle
        Dim receiptISBN As String = pendingISBN
        Dim receiptBorrowDate As Date = pendingBorrowDate
        Dim receiptDueDate As Date = pendingDueDate

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()
                transaction = conn.BeginTransaction()

                Dim stillActive As Boolean = False
                Dim verifyQuery As String =
                    "SELECT br.brd_id FROM borrowed br " &
                    "WHERE br.brd_id = @borrow_id AND br.bc_id = @copy_id AND br.return_date IS NULL " &
                    "LIMIT 1 FOR UPDATE"

                Using verifyCmd As New MySqlCommand(verifyQuery, conn, transaction)
                    verifyCmd.Parameters.AddWithValue("@borrow_id", pendingBorrowID)
                    verifyCmd.Parameters.AddWithValue("@copy_id", copyID)
                    stillActive = verifyCmd.ExecuteScalar() IsNot Nothing
                End Using

                If Not stillActive Then
                    transaction.Rollback()
                    transaction = Nothing
                    resetReturnState(False)
                    MsgBox("This transaction is no longer active. The book may already have been returned.",
                           vbOKOnly Or vbExclamation, "Return Changed")
                    Return
                End If

                Using updateBorrowCmd As New MySqlCommand(
                    "UPDATE borrowed SET return_date = CURDATE() WHERE brd_id = @borrow_id AND return_date IS NULL", conn, transaction)
                    updateBorrowCmd.Parameters.AddWithValue("@borrow_id", pendingBorrowID)

                    If updateBorrowCmd.ExecuteNonQuery() <> 1 Then
                        transaction.Rollback()
                        transaction = Nothing
                        resetReturnState(False)
                        MsgBox("The borrowing transaction could not be updated.", vbOKOnly Or vbExclamation, "Return Failed")
                        Return
                    End If
                End Using

                Using updateCopyCmd As New MySqlCommand(
                    "UPDATE bookcopies SET bc_status = 'Available' WHERE bc_id = @copy_id", conn, transaction)
                    updateCopyCmd.Parameters.AddWithValue("@copy_id", copyID)
                    updateCopyCmd.ExecuteNonQuery()
                End Using

                Using dateCmd As New MySqlCommand("SELECT CURDATE()", conn, transaction)
                    returnDate = Convert.ToDateTime(dateCmd.ExecuteScalar())
                End Using

                transaction.Commit()
                transaction = Nothing
            End Using

            If Dashboard.Visible Then
                Dashboard.refreshTransactions()
            End If

            Dim receiptText As String =
                "LIBRARY RETURN RECEIPT" & Environment.NewLine &
                New String("-"c, 40) & Environment.NewLine &
                "Transaction ID : " & receiptTransactionID & Environment.NewLine &
                "Member ID      : " & receiptMemberID & Environment.NewLine &
                "Member Name    : " & receiptMemberName & Environment.NewLine &
                "Book ID        : " & receiptBookID & Environment.NewLine &
                "Book Title     : " & receiptBookTitle & Environment.NewLine &
                "ISBN           : " & receiptISBN & Environment.NewLine &
                "Copy ID        : " & copyID & Environment.NewLine &
                "Copy Barcode   : " & formatCopyBarcode(copyID) & Environment.NewLine &
                "Borrow Date    : " & receiptBorrowDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                "Due Date       : " & receiptDueDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                "Return Date    : " & returnDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                "Days Late      : " & Math.Max(0, CInt((returnDate.Date - receiptDueDate.Date).TotalDays)).ToString() & Environment.NewLine &
                "Status         : RETURNED" & Environment.NewLine &
                New String("-"c, 40) & Environment.NewLine &
                "Return completed successfully."

            resetReturnState(True)

            Using frm As New receiptForm("Return Receipt", receiptText)
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            If transaction IsNot Nothing Then
                Try
                    transaction.Rollback()
                Catch
                End Try
            End If

            MsgBox("Unable to process the return transaction." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub resetReturnState(clearCopyText As Boolean)
        pendingCopyID = 0
        pendingBorrowID = 0
        pendingMemberID = 0
        pendingMemberName = ""
        pendingBookID = 0
        pendingBookTitle = ""
        pendingISBN = ""
        pendingBorrowDate = Date.MinValue
        pendingDueDate = Date.MinValue

        lblBorrowerDetails.Text = "Enter or scan a copy barcode, then click FIND BORROWER."
        btnReturn.Text = "FIND BORROWER"

        If clearCopyText Then
            txtCopyID.Clear()
        End If

        txtCopyID.Focus()
    End Sub
End Class
