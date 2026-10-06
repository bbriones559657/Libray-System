<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class returnBook
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
        btnReturn = New Button()
        SuspendLayout()

        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label11.ForeColor = Color.SteelBlue
        Label11.Location = New Point(25, 20)
        Label11.Text = "Return Book"

        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(25, 75)
        Label1.Text = "Physical Copy ID (Barcode)"

        txtCopyID.Font = New Font("Segoe UI", 11.0F)
        txtCopyID.Location = New Point(25, 95)
        txtCopyID.Size = New Size(240, 27)

        btnReturn.BackColor = Color.SteelBlue
        btnReturn.FlatAppearance.BorderSize = 0
        btnReturn.FlatStyle = FlatStyle.Flat
        btnReturn.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnReturn.ForeColor = Color.White
        btnReturn.Location = New Point(25, 150)
        btnReturn.Size = New Size(240, 45)
        btnReturn.Text = "PROCESS RETURN"
        btnReturn.UseVisualStyleBackColor = False

        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(290, 220)
        Controls.Add(btnReturn)
        Controls.Add(Label1)
        Controls.Add(txtCopyID)
        Controls.Add(Label11)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "returnBook"
        Text = "Return Book"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents txtCopyID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnReturn As Button
End Class