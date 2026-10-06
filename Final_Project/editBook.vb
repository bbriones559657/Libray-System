Imports MySql.Data.MySqlClient

Public Class editBook
    Private originalISBN As String = ""

    <System.ComponentModel.Browsable(False)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property BookID As Integer

    Private Sub editBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If BookID <= 0 Then
            MsgBox("No book was selected.", vbOKOnly Or vbExclamation, "Warning")
            Me.Close()
            Return
        End If

        loadBookData()
        Me.AcceptButton = btnUpdate
    End Sub

    Private Sub loadBookData()
        Dim bookExists As Boolean = False

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                cmbCategory.Items.Clear()

                Using categoryCmd As New MySqlCommand("SELECT category_name FROM categories ORDER BY category_name", conn)
                    Using reader As MySqlDataReader = categoryCmd.ExecuteReader()
                        While reader.Read()
                            cmbCategory.Items.Add(reader("category_name").ToString())
                        End While
                    End Using
                End Using

                Dim query As String =
                    "SELECT b.b_id, b.b_isbn, b.b_title, b.author_name, c.category_name " &
                    "FROM books b " &
                    "LEFT JOIN categories c ON b.b_category = c.category_id " &
                    "WHERE b.b_id = @book_id AND b.b_isActive = 1 LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@book_id", BookID)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            bookExists = True
                            txtbID.Text = reader("b_id").ToString()
                            txtISBN.Text = reader("b_isbn").ToString()
                            originalISBN = txtISBN.Text
                            txtbTitle.Text = reader("b_title").ToString()
                            txtAuthor.Text = reader("author_name").ToString()

                            If Not IsDBNull(reader("category_name")) Then
                                cmbCategory.SelectedItem = reader("category_name").ToString()
                            End If
                        End If
                    End Using
                End Using
            End Using

            If Not bookExists Then
                MsgBox("The selected book no longer exists or is inactive.", vbOKOnly Or vbExclamation, "Book Not Found")
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("Unable to load book details." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim isbn As String = txtISBN.Text.Trim()
        Dim title As String = txtbTitle.Text.Trim()
        Dim category As String = If(cmbCategory.SelectedItem IsNot Nothing, cmbCategory.SelectedItem.ToString(), "")
        Dim author As String = txtAuthor.Text.Trim()

        If String.IsNullOrEmpty(isbn) OrElse String.IsNullOrEmpty(title) OrElse String.IsNullOrEmpty(category) OrElse String.IsNullOrEmpty(author) Then
            MsgBox("Please fill in all fields.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                If Not String.Equals(isbn, originalISBN, StringComparison.OrdinalIgnoreCase) Then
                    Using duplicateCmd As New MySqlCommand(
                        "SELECT b_id, b_title FROM books WHERE b_isbn = @isbn AND b_id <> @book_id AND b_isActive = 1 LIMIT 1", conn)
                        duplicateCmd.Parameters.AddWithValue("@isbn", isbn)
                        duplicateCmd.Parameters.AddWithValue("@book_id", BookID)

                        Using reader As MySqlDataReader = duplicateCmd.ExecuteReader()
                            If reader.Read() Then
                                Dim duplicateID As String = reader("b_id").ToString()
                                Dim duplicateTitle As String = reader("b_title").ToString()
                                reader.Close()
                                MsgBox("That ISBN is already used by Book ID " & duplicateID & " (" & duplicateTitle & ").",
                                       vbOKOnly Or vbExclamation, "Duplicate ISBN")
                                txtISBN.Focus()
                                Return
                            End If
                        End Using
                    End Using
                End If

                Dim categoryID As Integer
                Using categoryCmd As New MySqlCommand("SELECT category_id FROM categories WHERE category_name = @category_name LIMIT 1", conn)
                    categoryCmd.Parameters.AddWithValue("@category_name", category)
                    Dim result As Object = categoryCmd.ExecuteScalar()

                    If result Is Nothing OrElse IsDBNull(result) Then
                        MsgBox("Selected category does not exist in the database.", vbOKOnly Or vbCritical, "Error")
                        Return
                    End If

                    categoryID = Convert.ToInt32(result)
                End Using

                Dim query As String =
                    "UPDATE books SET b_isbn = @book_isbn, b_title = @book_title, " &
                    "b_category = @book_category, author_name = @book_author " &
                    "WHERE b_id = @book_id AND b_isActive = 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@book_isbn", isbn)
                    cmd.Parameters.AddWithValue("@book_title", title)
                    cmd.Parameters.AddWithValue("@book_category", categoryID)
                    cmd.Parameters.AddWithValue("@book_author", author)
                    cmd.Parameters.AddWithValue("@book_id", BookID)

                    If cmd.ExecuteNonQuery() > 0 Then
                        MsgBox("Book details updated successfully.", vbOKOnly Or vbInformation, "Success")
                        Me.DialogResult = DialogResult.OK
                        Me.Close()
                    Else
                        MsgBox("No active book record was updated.", vbOKOnly Or vbExclamation, "Warning")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to update the book." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub
End Class
