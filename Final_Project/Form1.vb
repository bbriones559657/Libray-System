Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim email As String = txtEmail.Text.Trim()
        Dim password As String = txtPassword.Text

        If String.IsNullOrEmpty(email) OrElse String.IsNullOrEmpty(password) Then
            MsgBox("Please enter your email and password.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        If Not isValidEmail(email) Then
            MsgBox("Please enter a valid email address.", vbOKOnly Or vbExclamation, "Warning")
            txtEmail.Focus()
            Return
        End If

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT usr_id, usr_email, usr_role, " &
                    "TRIM(CONCAT_WS(' ', usr_firstName, NULLIF(usr_middleName, ''), usr_lastName)) AS FullName " &
                    "FROM users " &
                    "WHERE usr_email = @usr_email AND usr_password = @usr_password AND isActive = 1 LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@usr_email", email)
                    cmd.Parameters.AddWithValue("@usr_password", password)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            SetCurrentUser(
                                Convert.ToInt32(reader("usr_id")),
                                reader("FullName").ToString(),
                                reader("usr_email").ToString(),
                                reader("usr_role").ToString()
                            )
                        Else
                            MsgBox("Invalid credentials or inactive account.", vbOKOnly Or vbCritical, "OOPS!")
                            txtPassword.SelectAll()
                            txtPassword.Focus()
                            Return
                        End If
                    End Using
                End Using
            End Using

            Dashboard.PrepareForCurrentUser()
            Dashboard.Show()
            Dashboard.BringToFront()
            Me.Hide()
        Catch ex As Exception
            ClearCurrentUser()
            MsgBox("Unable to connect to the database." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPassword.UseSystemPasswordChar = True
        Me.AcceptButton = btnLogin

        Try
            ensureDatabaseSchema()
        Catch ex As Exception
            MsgBox("The database schema could not be prepared." & Environment.NewLine &
                   "Run MIGRATION_v4_roles_due_dates.sql if this continues." & Environment.NewLine & Environment.NewLine &
                   ex.Message,
                   vbOKOnly Or vbCritical, "Database Setup Error")
        End Try
    End Sub

    Public Sub ResetLoginForm()
        txtEmail.Clear()
        txtPassword.Clear()
        txtEmail.Focus()
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class
