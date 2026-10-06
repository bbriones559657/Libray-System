<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Label1 = New Label()
        Label2 = New Label()
        txtEmail = New TextBox()
        txtPassword = New TextBox()
        btnLogin = New Button()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(40, 100)
        Label1.Name = "Label1"
        Label1.Size = New Size(43, 19)
        Label1.TabIndex = 5
        Label1.Text = "Email"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(40, 150)
        Label2.Name = "Label2"
        Label2.Size = New Size(68, 19)
        Label2.TabIndex = 4
        Label2.Text = "Password"
        ' 
        ' txtEmail
        ' 
        txtEmail.Font = New Font("Segoe UI", 11.0F)
        txtEmail.Location = New Point(120, 96)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(250, 27)
        txtEmail.TabIndex = 1
        ' 
        ' txtPassword
        ' 
        txtPassword.Font = New Font("Segoe UI", 11.0F)
        txtPassword.Location = New Point(120, 146)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(250, 27)
        txtPassword.TabIndex = 2
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' btnLogin
        ' 
        btnLogin.BackColor = Color.SteelBlue
        btnLogin.FlatAppearance.BorderSize = 0
        btnLogin.FlatStyle = FlatStyle.Flat
        btnLogin.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(40, 205)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(330, 45)
        btnLogin.TabIndex = 3
        btnLogin.Text = "LOGIN"
        btnLogin.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label3.ForeColor = Color.SteelBlue
        Label3.Location = New Point(35, 35)
        Label3.Name = "Label3"
        Label3.Size = New Size(311, 30)
        Label3.TabIndex = 0
        Label3.Text = "Library Management System"
        ' 
        ' Form1
        ' 
        AcceptButton = btnLogin
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(420, 290)
        Controls.Add(Label3)
        Controls.Add(btnLogin)
        Controls.Add(txtPassword)
        Controls.Add(txtEmail)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Library Login"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents Label3 As Label
End Class