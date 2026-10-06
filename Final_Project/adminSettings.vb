Imports MySql.Data.MySqlClient

Public Class adminSettings
    Private ReadOnly cmbRole As New ComboBox()
    Private ReadOnly lblRole As New Label()
    Private roleUIReady As Boolean = False

    Private Sub adminSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsAdmin Then
            MsgBox("Only an Administrator can manage staff accounts.", vbOKOnly Or vbExclamation, "Access Denied")
            Me.Close()
            Return
        End If

        initializeRoleUI()
        dgvUsers.AutoGenerateColumns = False
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvUsers.MultiSelect = False
        txtPass.UseSystemPasswordChar = True
        Me.AcceptButton = btnSaveUser
        loadUsers()
    End Sub

    Private Sub initializeRoleUI()
        If roleUIReady Then Return
        roleUIReady = True

        If dgvUsers.Columns("ColURole") Is Nothing Then
            Dim roleColumn As New DataGridViewTextBoxColumn() With {
                .Name = "ColURole",
                .HeaderText = "Role",
                .DataPropertyName = "usr_role",
                .ReadOnly = True
            }
            dgvUsers.Columns.Insert(3, roleColumn)
        End If

        GroupBox1.Size = New Size(500, 205)
        Me.ClientSize = New Size(544, 455)

        lblRole.AutoSize = True
        lblRole.Font = New Font("Segoe UI Semibold", 9.5F)
        lblRole.ForeColor = Color.DarkSlateGray
        lblRole.Location = New Point(355, 25)
        lblRole.Text = "Role"

        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRole.Font = New Font("Segoe UI", 9.5F)
        cmbRole.Items.AddRange(New Object() {"Staff", "Admin"})
        cmbRole.SelectedIndex = 0
        cmbRole.Location = New Point(355, 45)
        cmbRole.Size = New Size(125, 25)

        chkActive.Location = New Point(355, 82)
        btnSaveUser.Location = New Point(355, 120)

        GroupBox1.Controls.Add(lblRole)
        GroupBox1.Controls.Add(cmbRole)
    End Sub

    Private Sub adminSettings_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If roleUIReady AndAlso IsAdmin Then loadUsers()
    End Sub

    Private Sub loadUsers()
        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT usr_id, usr_email, usr_role, " &
                    "TRIM(CONCAT_WS(' ', usr_firstName, NULLIF(usr_middleName, ''), usr_lastName)) AS StaffName, " &
                    "CASE WHEN isActive = 1 THEN 'Yes' ELSE 'No' END AS isActive " &
                    "FROM users ORDER BY usr_id DESC"

                Using da As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvUsers.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to load staff accounts." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click
        If Not IsAdmin Then
            MsgBox("Only an Administrator can create staff accounts.", vbOKOnly Or vbExclamation, "Access Denied")
            Return
        End If

        Dim email As String = txtEmail.Text.Trim()
        Dim password As String = txtPass.Text
        Dim firstName As String = txtFName.Text.Trim()
        Dim lastName As String = txtLName.Text.Trim()
        Dim role As String = cmbRole.Text

        If String.IsNullOrEmpty(email) OrElse String.IsNullOrEmpty(password) OrElse
           String.IsNullOrEmpty(firstName) OrElse String.IsNullOrEmpty(lastName) OrElse String.IsNullOrEmpty(role) Then
            MsgBox("Please fill in all staff fields.", vbOKOnly Or vbExclamation, "Warning")
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

                Using duplicateCmd As New MySqlCommand("SELECT COUNT(*) FROM users WHERE usr_email = @email", conn)
                    duplicateCmd.Parameters.AddWithValue("@email", email)
                    If Convert.ToInt32(duplicateCmd.ExecuteScalar()) > 0 Then
                        MsgBox("A staff account with this email already exists.", vbOKOnly Or vbExclamation, "Duplicate Email")
                        txtEmail.Focus()
                        Return
                    End If
                End Using

                Dim query As String =
                    "INSERT INTO users (usr_firstName, usr_middleName, usr_lastName, usr_email, usr_password, usr_role, isActive) " &
                    "VALUES (@first_name, NULL, @last_name, @email, @password, @role, @is_active)"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@first_name", firstName)
                    cmd.Parameters.AddWithValue("@last_name", lastName)
                    cmd.Parameters.AddWithValue("@email", email)
                    cmd.Parameters.AddWithValue("@password", password)
                    cmd.Parameters.AddWithValue("@role", role)
                    cmd.Parameters.AddWithValue("@is_active", If(chkActive.Checked, 1, 0))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MsgBox(role & " account saved successfully.", vbOKOnly Or vbInformation, "Success")
            clearFields()
            loadUsers()
        Catch ex As Exception
            MsgBox("Unable to save the staff account." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub clearFields()
        txtEmail.Clear()
        txtPass.Clear()
        txtFName.Clear()
        txtLName.Clear()
        cmbRole.SelectedIndex = 0
        chkActive.Checked = True
        txtEmail.Focus()
    End Sub

    Public Sub RefreshUsers()
        If IsAdmin Then
            initializeRoleUI()
            loadUsers()
        End If
    End Sub

End Class
