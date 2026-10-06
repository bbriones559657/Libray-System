<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class borrowBook
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
        Label11 = New Label()
        txtCopyID = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txtMemberID = New TextBox()
        btnBorrow = New Button()
        SuspendLayout()

        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label11.ForeColor = Color.SteelBlue
        Label11.Location = New Point(25, 20)
        Label11.Text = "Checkout Book"

        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(25, 75)
        Label2.Text = "Patron Member ID"

        txtMemberID.Font = New Font("Segoe UI", 11.0F)
        txtMemberID.Location = New Point(25, 95)
        txtMemberID.Size = New Size(240, 27)

        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(25, 135)
        Label1.Text = "Physical Copy ID (Barcode)"

        txtCopyID.Font = New Font("Segoe UI", 11.0F)
        txtCopyID.Location = New Point(25, 155)
        txtCopyID.Size = New Size(240, 27)

        btnBorrow.BackColor = Color.SteelBlue
        btnBorrow.FlatAppearance.BorderSize = 0
        btnBorrow.FlatStyle = FlatStyle.Flat
        btnBorrow.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnBorrow.ForeColor = Color.White
        btnBorrow.Location = New Point(25, 210)
        btnBorrow.Size = New Size(240, 45)
        btnBorrow.Text = "CONFIRM BORROW"
        btnBorrow.UseVisualStyleBackColor = False

        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(290, 290)
        Controls.Add(btnBorrow)
        Controls.Add(Label2)
        Controls.Add(txtMemberID)
        Controls.Add(Label1)
        Controls.Add(txtCopyID)
        Controls.Add(Label11)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "borrowBook"
        Text = "Borrow Book"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents txtCopyID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtMemberID As TextBox
    Friend WithEvents btnBorrow As Button
End Class