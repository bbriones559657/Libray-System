<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Patrons
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
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        txtFirstName = New TextBox()
        txtMiddleName = New TextBox()
        txtLastName = New TextBox()
        txtEmail = New TextBox()
        txtPhone = New TextBox()
        txtValidID = New TextBox()
        txtValidIDName = New TextBox()
        chkIsActive = New CheckBox()
        btnAdd = New Button()
        btnClear = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label1.ForeColor = Color.SteelBlue
        Label1.Location = New Point(25, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(224, 30)
        Label1.TabIndex = 0
        Label1.Text = "ADD MEMBERSHIP"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(25, 75)
        Label2.Name = "Label2"
        Label2.Size = New Size(74, 17)
        Label2.TabIndex = 1
        Label2.Text = "First Name"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label3.ForeColor = Color.DarkSlateGray
        Label3.Location = New Point(25, 135)
        Label3.Name = "Label3"
        Label3.Size = New Size(89, 17)
        Label3.TabIndex = 2
        Label3.Text = "Middle Name"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label4.ForeColor = Color.DarkSlateGray
        Label4.Location = New Point(25, 195)
        Label4.Name = "Label4"
        Label4.Size = New Size(72, 17)
        Label4.TabIndex = 3
        Label4.Text = "Last Name"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label5.ForeColor = Color.DarkSlateGray
        Label5.Location = New Point(25, 255)
        Label5.Name = "Label5"
        Label5.Size = New Size(40, 17)
        Label5.TabIndex = 4
        Label5.Text = "Email"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label6.ForeColor = Color.DarkSlateGray
        Label6.Location = New Point(25, 315)
        Label6.Name = "Label6"
        Label6.Size = New Size(110, 17)
        Label6.TabIndex = 5
        Label6.Text = "Contact Number"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label7.ForeColor = Color.DarkSlateGray
        Label7.Location = New Point(285, 75)
        Label7.Name = "Label7"
        Label7.Size = New Size(201, 17)
        Label7.TabIndex = 6
        Label7.Text = "Valid ID Number (If applicable)"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label8.ForeColor = Color.DarkSlateGray
        Label8.Location = New Point(285, 135)
        Label8.Name = "Label8"
        Label8.Size = New Size(191, 17)
        Label8.TabIndex = 7
        Label8.Text = "Valid ID Name (If applicable)"
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Font = New Font("Segoe UI", 10.5F)
        txtFirstName.Location = New Point(25, 95)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(220, 26)
        txtFirstName.TabIndex = 0
        ' 
        ' txtMiddleName
        ' 
        txtMiddleName.Font = New Font("Segoe UI", 10.5F)
        txtMiddleName.Location = New Point(25, 155)
        txtMiddleName.Name = "txtMiddleName"
        txtMiddleName.Size = New Size(220, 26)
        txtMiddleName.TabIndex = 1
        ' 
        ' txtLastName
        ' 
        txtLastName.Font = New Font("Segoe UI", 10.5F)
        txtLastName.Location = New Point(25, 215)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(220, 26)
        txtLastName.TabIndex = 2
        ' 
        ' txtEmail
        ' 
        txtEmail.Font = New Font("Segoe UI", 10.5F)
        txtEmail.Location = New Point(25, 275)
        txtEmail.Name = "txtEmail"
        txtEmail.Size = New Size(220, 26)
        txtEmail.TabIndex = 3
        ' 
        ' txtPhone
        ' 
        txtPhone.Font = New Font("Segoe UI", 10.5F)
        txtPhone.Location = New Point(25, 335)
        txtPhone.Name = "txtPhone"
        txtPhone.Size = New Size(220, 26)
        txtPhone.TabIndex = 4
        ' 
        ' txtValidID
        ' 
        txtValidID.Font = New Font("Segoe UI", 10.5F)
        txtValidID.Location = New Point(285, 95)
        txtValidID.Name = "txtValidID"
        txtValidID.Size = New Size(220, 26)
        txtValidID.TabIndex = 5
        ' 
        ' txtValidIDName
        ' 
        txtValidIDName.Font = New Font("Segoe UI", 10.5F)
        txtValidIDName.Location = New Point(285, 155)
        txtValidIDName.Name = "txtValidIDName"
        txtValidIDName.Size = New Size(220, 26)
        txtValidIDName.TabIndex = 6
        ' 
        ' chkIsActive
        ' 
        chkIsActive.AutoSize = True
        chkIsActive.Checked = True
        chkIsActive.CheckState = CheckState.Checked
        chkIsActive.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        chkIsActive.ForeColor = Color.DarkSlateGray
        chkIsActive.Location = New Point(285, 195)
        chkIsActive.Name = "chkIsActive"
        chkIsActive.Size = New Size(117, 21)
        chkIsActive.TabIndex = 7
        chkIsActive.Text = "Account Active"
        chkIsActive.UseVisualStyleBackColor = True
        ' 
        ' btnAdd
        ' 
        btnAdd.BackColor = Color.SteelBlue
        btnAdd.FlatAppearance.BorderSize = 0
        btnAdd.FlatStyle = FlatStyle.Flat
        btnAdd.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnAdd.ForeColor = Color.White
        btnAdd.Location = New Point(285, 255)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(220, 45)
        btnAdd.TabIndex = 8
        btnAdd.Text = "SAVE MEMBER"
        btnAdd.UseVisualStyleBackColor = False
        ' 
        ' btnClear
        ' 
        btnClear.BackColor = Color.White
        btnClear.FlatAppearance.BorderColor = Color.DarkGray
        btnClear.FlatAppearance.BorderSize = 1
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnClear.ForeColor = Color.DarkSlateGray
        btnClear.Location = New Point(285, 321)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(220, 40)
        btnClear.TabIndex = 9
        btnClear.Text = "Clear Fields"
        btnClear.UseVisualStyleBackColor = False
        ' 
        ' Patrons
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(540, 400)
        Controls.Add(btnClear)
        Controls.Add(btnAdd)
        Controls.Add(chkIsActive)
        Controls.Add(txtValidIDName)
        Controls.Add(txtValidID)
        Controls.Add(txtPhone)
        Controls.Add(txtEmail)
        Controls.Add(txtLastName)
        Controls.Add(txtMiddleName)
        Controls.Add(txtFirstName)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "Patrons"
        Text = "Add Membership"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtMiddleName As TextBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents txtValidID As TextBox
    Friend WithEvents txtValidIDName As TextBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnClear As Button
End Class