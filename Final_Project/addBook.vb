Imports MySql.Data.MySqlClient
Imports System.Collections.Generic

Public Class addBook
    Private Sub addBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadFormData()
        Me.AcceptButton = Button1
    End Sub

    Private Sub loadFormData()
        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                txtbCategory.Items.Clear()

                Using cmd As New MySqlCommand("SELECT category_name FROM categories ORDER BY category_name", conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            txtbCategory.Items.Add(reader("category_name").ToString())
                        End While
                    End Using
                End Using

                If txtbCategory.Items.Count > 0 Then
                    txtbCategory.SelectedIndex = 0
                End If

                Dim idQuery As String =
                    "SELECT AUTO_INCREMENT FROM information_schema.TABLES " &
                    "WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'books'"

                Using cmdID As New MySqlCommand(idQuery, conn)
                    Dim result As Object = cmdID.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        txtbID.Text = result.ToString()
                    Else
                        txtbID.Text = ""
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to load book form data." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim title As String = txtbTitle.Text.Trim()
        Dim category As String = If(txtbCategory.SelectedItem IsNot Nothing, txtbCategory.SelectedItem.ToString(), "")
        Dim isbn As String = txtISBN.Text.Trim()
        Dim author As String = txtAuthor.Text.Trim()
        Dim copies As Integer = Convert.ToInt32(numCopies.Value)

        If String.IsNullOrEmpty(title) OrElse String.IsNullOrEmpty(category) OrElse String.IsNullOrEmpty(isbn) OrElse String.IsNullOrEmpty(author) Then
            MsgBox("Please fill in all fields.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        If copies <= 0 Then
            MsgBox("Number of copies must be at least 1.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        Dim transaction As MySqlTransaction = Nothing
        Dim newBookID As Integer = 0
        Dim existingBookUsed As Boolean = False
        Dim createdCopies As New List(Of Integer)()

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()
                transaction = conn.BeginTransaction()

                Dim categoryId As Integer
                Dim queryCategory As String = "SELECT category_id FROM categories WHERE category_name = @category_name LIMIT 1"

                Using cmdCategory As New MySqlCommand(queryCategory, conn, transaction)
                    cmdCategory.Parameters.AddWithValue("@category_name", category)
                    Dim result As Object = cmdCategory.ExecuteScalar()

                    If result Is Nothing OrElse IsDBNull(result) Then
                        transaction.Rollback()
                        transaction = Nothing
                        MsgBox("Selected category does not exist in the database.", vbOKOnly Or vbCritical, "Error")
                        Return
                    End If

                    categoryId = Convert.ToInt32(result)
                End Using

                Dim existingTitle As String = ""
                Dim existingAuthor As String = ""
                Dim existingCategoryID As Integer = 0

                Dim existingQuery As String =
                    "SELECT b_id, b_title, author_name, b_category FROM books " &
                    "WHERE b_isbn = @book_isbn AND b_isActive = 1 ORDER BY b_id LIMIT 1 FOR UPDATE"

                Using existingCmd As New MySqlCommand(existingQuery, conn, transaction)
                    existingCmd.Parameters.AddWithValue("@book_isbn", isbn)

                    Using reader As MySqlDataReader = existingCmd.ExecuteReader()
                        If reader.Read() Then
                            newBookID = Convert.ToInt32(reader("b_id"))
                            existingTitle = reader("b_title").ToString()
                            existingAuthor = reader("author_name").ToString()
                            existingCategoryID = Convert.ToInt32(reader("b_category"))
                            existingBookUsed = True
                        End If
                    End Using
                End Using

                If existingBookUsed Then
                    If Not String.Equals(existingTitle, title, StringComparison.OrdinalIgnoreCase) OrElse
                       Not String.Equals(existingAuthor, author, StringComparison.OrdinalIgnoreCase) OrElse
                       existingCategoryID <> categoryId Then

                        transaction.Rollback()
                        transaction = Nothing
                        MsgBox("This ISBN already belongs to Book ID " & newBookID & " (" & existingTitle & ")." & Environment.NewLine &
                               "Use the same title, author, and category if you want to add more physical copies.",
                               vbOKOnly Or vbExclamation, "ISBN Already Exists")
                        Return
                    End If
                Else
                    Dim queryBook As String =
                        "INSERT INTO books (b_isbn, b_title, b_category, author_name, b_isActive) " &
                        "VALUES (@book_isbn, @book_title, @book_category, @book_author, 1)"

                    Using cmdBook As New MySqlCommand(queryBook, conn, transaction)
                        cmdBook.Parameters.AddWithValue("@book_isbn", isbn)
                        cmdBook.Parameters.AddWithValue("@book_title", title)
                        cmdBook.Parameters.AddWithValue("@book_category", categoryId)
                        cmdBook.Parameters.AddWithValue("@book_author", author)
                        cmdBook.ExecuteNonQuery()
                        newBookID = Convert.ToInt32(cmdBook.LastInsertedId)
                    End Using
                End If

                Dim queryCopy As String = "INSERT INTO bookcopies (b_id, bc_status) VALUES (@book_id, 'Available')"

                For i As Integer = 1 To copies
                    Using cmdCopy As New MySqlCommand(queryCopy, conn, transaction)
                        cmdCopy.Parameters.AddWithValue("@book_id", newBookID)
                        cmdCopy.ExecuteNonQuery()
                        createdCopies.Add(Convert.ToInt32(cmdCopy.LastInsertedId))
                    End Using
                Next

                transaction.Commit()
                transaction = Nothing
            End Using

            Dim barcodeLines As New List(Of String)()
            For Each copyID As Integer In createdCopies
                barcodeLines.Add(formatCopyBarcode(copyID) & " (Copy ID: " & copyID & ")")
            Next

            Dim actionMessage As String = If(existingBookUsed,
                                             "Existing book found. New physical copies added successfully.",
                                             "Book saved successfully.")

            MsgBox(actionMessage & Environment.NewLine &
                   "Book ID: " & newBookID & Environment.NewLine &
                   "Copies added: " & copies & Environment.NewLine & Environment.NewLine &
                   "Physical Copy IDs / Barcodes:" & Environment.NewLine &
                   String.Join(Environment.NewLine, barcodeLines),
                   vbOKOnly Or vbInformation, "Success")

            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            If transaction IsNot Nothing Then
                Try
                    transaction.Rollback()
                Catch
                End Try
            End If

            MsgBox("Unable to save the book." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clearFields()
    End Sub

    Private Sub clearFields()
        txtISBN.Clear()
        txtbTitle.Clear()
        txtAuthor.Clear()
        numCopies.Value = numCopies.Minimum

        If txtbCategory.Items.Count > 0 Then
            txtbCategory.SelectedIndex = 0
        End If

        loadFormData()
        txtISBN.Focus()
    End Sub
End Class
