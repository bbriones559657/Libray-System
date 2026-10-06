Imports MySql.Data.MySqlClient

Public Class bookDashboard
    Private Sub bookDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvBooks.AutoGenerateColumns = False
        dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvBooks.MultiSelect = False

        If ComboBox1.Items.Count > 0 AndAlso ComboBox1.SelectedIndex = -1 Then
            ComboBox1.SelectedIndex = 0
        Else
            loadBooks()
        End If
    End Sub

    Private Sub bookDashboard_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        loadBooks()
    End Sub

    Private Sub loadBooks()
        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim filterBy As String = If(ComboBox1.SelectedItem Is Nothing, "ID", ComboBox1.SelectedItem.ToString())
                Dim searchValue As String = TextBox1.Text.Trim()
                Dim searchClause As String = ""

                If searchValue <> "" Then
                    Select Case filterBy
                        Case "Title"
                            searchClause = " AND b.b_title LIKE @search "
                        Case "Category"
                            searchClause = " AND c.category_name LIKE @search "
                        Case Else
                            searchClause = " AND CAST(b.b_id AS CHAR) LIKE @search "
                    End Select
                End If

                Dim query As String =
                    "SELECT b.b_id, b.b_title, c.category_name, " &
                    "SUM(CASE WHEN bc.bc_status = 'Available' THEN 1 ELSE 0 END) AS AvailableCopies, " &
                    "b.b_isbn " &
                    "FROM books b " &
                    "LEFT JOIN categories c ON b.b_category = c.category_id " &
                    "LEFT JOIN bookcopies bc ON b.b_id = bc.b_id " &
                    "WHERE b.b_isActive = 1 " &
                    searchClause &
                    "GROUP BY b.b_id, b.b_title, c.category_name, b.b_isbn " &
                    "ORDER BY b.b_id DESC"

                Using da As New MySqlDataAdapter(query, conn)
                    If searchValue <> "" Then
                        da.SelectCommand.Parameters.AddWithValue("@search", "%" & searchValue & "%")
                    End If

                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvBooks.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to load books." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Function getSelectedBookID() As Integer
        If dgvBooks.CurrentRow Is Nothing OrElse dgvBooks.CurrentRow.Cells("Column1").Value Is Nothing Then
            Return 0
        End If

        Dim bookID As Integer
        If Integer.TryParse(dgvBooks.CurrentRow.Cells("Column1").Value.ToString(), bookID) Then
            Return bookID
        End If

        Return 0
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem IsNot Nothing Then
            lblSB.Text = ComboBox1.SelectedItem.ToString()
        End If
        loadBooks()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        loadBooks()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim bookID As Integer = getSelectedBookID()
        If bookID = 0 Then
            MsgBox("Please select a book first.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        Dim details As String = ""

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT b.b_id, b.b_isbn, b.b_title, b.author_name, c.category_name, " &
                    "COUNT(bc.bc_id) AS TotalCopies, " &
                    "SUM(CASE WHEN bc.bc_status = 'Available' THEN 1 ELSE 0 END) AS AvailableCopies, " &
                    "SUM(CASE WHEN bc.bc_status = 'Borrowed' THEN 1 ELSE 0 END) AS BorrowedCopies, " &
                    "SUM(CASE WHEN bc.bc_status = 'Unavailable' THEN 1 ELSE 0 END) AS UnavailableCopies, " &
                    "GROUP_CONCAT(CONCAT('COPY-', LPAD(bc.bc_id, 6, '0'), ' [ID ', bc.bc_id, '] - ', bc.bc_status) " &
                    "ORDER BY bc.bc_id SEPARATOR ', ') AS CopyList " &
                    "FROM books b " &
                    "LEFT JOIN categories c ON b.b_category = c.category_id " &
                    "LEFT JOIN bookcopies bc ON b.b_id = bc.b_id " &
                    "WHERE b.b_id = @book_id AND b.b_isActive = 1 " &
                    "GROUP BY b.b_id, b.b_isbn, b.b_title, b.author_name, c.category_name"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@book_id", bookID)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            details =
                                "Book ID: " & reader("b_id").ToString() & Environment.NewLine &
                                "ISBN: " & reader("b_isbn").ToString() & Environment.NewLine &
                                "Title: " & reader("b_title").ToString() & Environment.NewLine &
                                "Author: " & reader("author_name").ToString() & Environment.NewLine &
                                "Category: " & reader("category_name").ToString() & Environment.NewLine & Environment.NewLine &
                                "Total Copies: " & reader("TotalCopies").ToString() & Environment.NewLine &
                                "Available: " & reader("AvailableCopies").ToString() & Environment.NewLine &
                                "Borrowed: " & reader("BorrowedCopies").ToString() & Environment.NewLine &
                                "Unavailable: " & reader("UnavailableCopies").ToString() & Environment.NewLine & Environment.NewLine &
                                "Physical Copy IDs / Barcodes:" & Environment.NewLine &
                                If(IsDBNull(reader("CopyList")), "None", reader("CopyList").ToString())
                        End If
                    End Using
                End Using
            End Using

            If details = "" Then
                MsgBox("The selected book no longer exists or is inactive.", vbOKOnly Or vbExclamation, "Book Not Found")
            Else
                ' Show the message only after the reader/connection has been closed.
                MsgBox(details, vbOKOnly Or vbInformation, "Book Details")
            End If
        Catch ex As Exception
            MsgBox("Unable to load book details." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using frm As New addBook()
            frm.ShowDialog(Me)
        End Using
        loadBooks()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim bookID As Integer = getSelectedBookID()
        If bookID = 0 Then
            MsgBox("Please select a book to edit.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        Using frm As New editBook()
            frm.BookID = bookID
            frm.ShowDialog(Me)
        End Using

        loadBooks()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim bookID As Integer = getSelectedBookID()
        If bookID = 0 Then
            MsgBox("Please select a book to delete.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        If MsgBox("Deactivate this book?" & Environment.NewLine & Environment.NewLine &
                  "The book and its history will stay in the database, but it will no longer appear in the active inventory or be available for borrowing.",
                  vbYesNo Or vbQuestion, "Confirm Soft Delete") <> vbYes Then
            Return
        End If

        Dim transaction As MySqlTransaction = Nothing

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()
                transaction = conn.BeginTransaction()

                Dim activeBorrowQuery As String =
                    "SELECT COUNT(*) FROM borrowed br " &
                    "INNER JOIN bookcopies bc ON br.bc_id = bc.bc_id " &
                    "WHERE bc.b_id = @book_id AND br.return_date IS NULL"

                Using activeCmd As New MySqlCommand(activeBorrowQuery, conn, transaction)
                    activeCmd.Parameters.AddWithValue("@book_id", bookID)
                    Dim activeBorrowCount As Integer = Convert.ToInt32(activeCmd.ExecuteScalar())

                    If activeBorrowCount > 0 Then
                        transaction.Rollback()
                        transaction = Nothing
                        MsgBox("This book still has borrowed physical copies. Return all copies first before deactivating the book.",
                               vbOKOnly Or vbExclamation, "Deactivate Blocked")
                        Return
                    End If
                End Using

                Using bookCmd As New MySqlCommand(
                    "UPDATE books SET b_isActive = 0 WHERE b_id = @book_id AND b_isActive = 1", conn, transaction)
                    bookCmd.Parameters.AddWithValue("@book_id", bookID)

                    If bookCmd.ExecuteNonQuery() = 0 Then
                        transaction.Rollback()
                        transaction = Nothing
                        MsgBox("The selected book no longer exists or is already inactive.", vbOKOnly Or vbExclamation, "No Changes")
                        Return
                    End If
                End Using

                transaction.Commit()
                transaction = Nothing
            End Using

            MsgBox("Book deactivated successfully. No book or borrowing records were deleted.", vbOKOnly Or vbInformation, "Success")
            loadBooks()
        Catch ex As Exception
            If transaction IsNot Nothing Then
                Try
                    transaction.Rollback()
                Catch
                End Try
            End If
            MsgBox("Unable to deactivate the book." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub dgvBooks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooks.CellDoubleClick
        If e.RowIndex >= 0 Then
            Button1.PerformClick()
        End If
    End Sub

    Public Sub RefreshBooks()
        loadBooks()
    End Sub

End Class
