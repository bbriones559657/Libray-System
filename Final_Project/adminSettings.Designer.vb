<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class adminSettings
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Label11 = New Label()
        dgvUsers = New DataGridView()
        ColUID = New DataGridViewTextBoxColumn()
        ColUEmail = New DataGridViewTextBoxColumn()
        ColUName = New DataGridViewTextBoxColumn()
        ColUActive = New DataGridViewTextBoxColumn()
        GroupBox1 = New GroupBox()
        btnSaveUser = New Button()
        chkActive = New CheckBox()
        txtLName = New TextBox()
        txtFName = New TextBox()
        txtPass = New TextBox()
        txtEmail = New TextBox()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        CType(dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label11.ForeColor = Color.SteelBlue
        Label11.Location = New Point(20, 20)
        Label11.Name = "Label11"
        Label11.Text = "Staff Management"
        ' 
        ' dgvUsers
        ' 
        dgvUsers.AllowUserToAddRows = False
        dgvUsers.AllowUserToDeleteRows = False
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvUsers.BackgroundColor = Color.White
        dgvUsers.BorderStyle = BorderStyle.None
        dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.SteelBlue
        DataGridViewCellStyle1.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvUsers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvUsers.EnableHeadersVisualStyles = False

        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        dgvUsers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvUsers.DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
        dgvUsers.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        dgvUsers.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
        dgvUsers.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvUsers.RowHeadersVisible = False

        dgvUsers.Columns.AddRange(New DataGridViewColumn() {ColUID, ColUEmail, ColUName, ColUActive})
        dgvUsers.Location = New Point(21, 65)
        dgvUsers.Name = "dgvUsers"
        dgvUsers.ReadOnly = True
        dgvUsers.Size = New Size(500, 150)
        ' 
        ' Columns
        ' 
        ColUID.DataPropertyName = "usr_id"
        ColUID.HeaderText = "ID"
        ColUID.Name = "ColUID"

        ColUEmail.DataPropertyName = "usr_email"
        ColUEmail.HeaderText = "Email Address"
        ColUEmail.Name = "ColUEmail"

        ColUName.DataPropertyName = "StaffName"
        ColUName.HeaderText = "Name"
        ColUName.Name = "ColUName"

        ColUActive.DataPropertyName = "isActive"
        ColUActive.HeaderText = "Active"
        ColUActive.Name = "ColUActive"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.BackColor = Color.White
        GroupBox1.Controls.Add(btnSaveUser)
        GroupBox1.Controls.Add(chkActive)
        GroupBox1.Controls.Add(txtLName)
        GroupBox1.Controls.Add(txtFName)
        GroupBox1.Controls.Add(txtPass)
        GroupBox1.Controls.Add(txtEmail)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Font = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold)
        GroupBox1.ForeColor = Color.SteelBlue
        GroupBox1.Location = New Point(21, 230)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(500, 160)
        GroupBox1.Text = "Add New Staff Member"
        ' 
        ' Form Setup
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9.5F)
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(20, 33)
        Label1.Text = "Email Address"

        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.5F)
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(20, 92)
        Label2.Text = "Password"

        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9.5F)
        Label3.ForeColor = Color.DarkSlateGray
        Label3.Location = New Point(184, 33)
        Label3.Text = "First Name"

        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9.5F)
        Label4.ForeColor = Color.DarkSlateGray
        Label4.Location = New Point(184, 92)
        Label4.Text = "Last Name"

        txtEmail.Font = New Font("Segoe UI", 10.0F)
        txtEmail.Location = New Point(20, 51)
        txtEmail.Size = New Size(144, 25)

        txtPass.Font = New Font("Segoe UI", 10.0F)
        txtPass.Location = New Point(20, 110)
        txtPass.Size = New Size(144, 25)

        txtFName.Font = New Font("Segoe UI", 10.0F)
        txtFName.Location = New Point(184, 51)
        txtFName.Size = New Size(149, 25)

        txtLName.Font = New Font("Segoe UI", 10.0F)
        txtLName.Location = New Point(184, 110)
        txtLName.Size = New Size(149, 25)

        chkActive.AutoSize = True
        chkActive.Checked = True
        chkActive.CheckState = CheckState.Checked
        chkActive.Font = New Font("Segoe UI Semibold", 9.5F)
        chkActive.ForeColor = Color.DarkSlateGray
        chkActive.Location = New Point(355, 53)
        chkActive.Text = "Account Active"

        btnSaveUser.BackColor = Color.SteelBlue
        btnSaveUser.FlatAppearance.BorderSize = 0
        btnSaveUser.FlatStyle = FlatStyle.Flat
        btnSaveUser.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnSaveUser.ForeColor = Color.White
        btnSaveUser.Location = New Point(355, 95)
        btnSaveUser.Size = New Size(125, 40)
        btnSaveUser.Text = "SAVE STAFF"
        btnSaveUser.UseVisualStyleBackColor = False
        ' 
        ' Main Form
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(544, 410)
        Controls.Add(GroupBox1)
        Controls.Add(dgvUsers)
        Controls.Add(Label11)
        Name = "adminSettings"
        Text = "Settings"
        CType(dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents ColUID As DataGridViewTextBoxColumn
    Friend WithEvents ColUEmail As DataGridViewTextBoxColumn
    Friend WithEvents ColUName As DataGridViewTextBoxColumn
    Friend WithEvents ColUActive As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnSaveUser As Button
    Friend WithEvents chkActive As CheckBox
    Friend WithEvents txtLName As TextBox
    Friend WithEvents txtFName As TextBox
    Friend WithEvents txtPass As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class