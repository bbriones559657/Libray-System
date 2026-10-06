<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class addBook
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
        txtbID = New TextBox()
        txtISBN = New TextBox()
        Label4 = New Label()
        Label5 = New Label()
        txtAuthor = New TextBox()
        Label6 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        txtbCategory = New ComboBox()
        txtbTitle = New TextBox()
        Label7 = New Label()
        numCopies = New NumericUpDown()
        CType(numCopies, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label1.ForeColor = Color.SteelBlue
        Label1.Location = New Point(22, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(130, 30)
        Label1.TabIndex = 1
        Label1.Text = "ADD BOOK"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(25, 70)
        Label2.Name = "Label2"
        Label2.Size = New Size(54, 17)
        Label2.TabIndex = 2
        Label2.Text = "Book ID"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label3.ForeColor = Color.DarkSlateGray
        Label3.Location = New Point(25, 130)
        Label3.Name = "Label3"
        Label3.Size = New Size(70, 17)
        Label3.TabIndex = 3
        Label3.Text = "Book ISBN"
        ' 
        ' txtbID
        ' 
        txtbID.Font = New Font("Segoe UI", 10.5F)
        txtbID.Location = New Point(25, 90)
        txtbID.Name = "txtbID"
        txtbID.ReadOnly = True
        txtbID.Size = New Size(240, 26)
        txtbID.TabIndex = 0
        ' 
        ' txtISBN
        ' 
        txtISBN.Font = New Font("Segoe UI", 10.5F)
        txtISBN.Location = New Point(25, 150)
        txtISBN.Name = "txtISBN"
        txtISBN.Size = New Size(240, 26)
        txtISBN.TabIndex = 1
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label5.ForeColor = Color.DarkSlateGray
        Label5.Location = New Point(25, 190)
        Label5.Name = "Label5"
        Label5.Size = New Size(67, 17)
        Label5.TabIndex = 6
        Label5.Text = "Book Title"
        ' 
        ' txtbTitle
        ' 
        txtbTitle.Font = New Font("Segoe UI", 10.5F)
        txtbTitle.Location = New Point(25, 210)
        txtbTitle.Name = "txtbTitle"
        txtbTitle.Size = New Size(240, 26)
        txtbTitle.TabIndex = 2
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label4.ForeColor = Color.DarkSlateGray
        Label4.Location = New Point(25, 250)
        Label4.Name = "Label4"
        Label4.Size = New Size(99, 17)
        Label4.TabIndex = 7
        Label4.Text = "Book Category"
        ' 
        ' txtbCategory
        ' 
        txtbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        txtbCategory.Font = New Font("Segoe UI", 10.5F)
        txtbCategory.FormattingEnabled = True
        txtbCategory.Location = New Point(25, 270)
        txtbCategory.Name = "txtbCategory"
        txtbCategory.Size = New Size(240, 27)
        txtbCategory.TabIndex = 14
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label6.ForeColor = Color.DarkSlateGray
        Label6.Location = New Point(25, 310)
        Label6.Name = "Label6"
        Label6.Size = New Size(85, 17)
        Label6.TabIndex = 10
        Label6.Text = "Book Author"
        ' 
        ' txtAuthor
        ' 
        txtAuthor.Font = New Font("Segoe UI", 10.5F)
        txtAuthor.Location = New Point(25, 330)
        txtAuthor.Name = "txtAuthor"
        txtAuthor.Size = New Size(240, 26)
        txtAuthor.TabIndex = 4
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label7.ForeColor = Color.DarkSlateGray
        Label7.Location = New Point(25, 370)
        Label7.Name = "Label7"
        Label7.Size = New Size(119, 17)
        Label7.TabIndex = 15
        Label7.Text = "Number of Copies"
        ' 
        ' numCopies
        ' 
        numCopies.Font = New Font("Segoe UI", 10.5F)
        numCopies.Location = New Point(25, 390)
        numCopies.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        numCopies.Name = "numCopies"
        numCopies.Size = New Size(240, 26)
        numCopies.TabIndex = 16
        numCopies.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.SteelBlue
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Button1.ForeColor = Color.White
        Button1.Location = New Point(25, 440)
        Button1.Name = "Button1"
        Button1.Size = New Size(240, 45)
        Button1.TabIndex = 5
        Button1.Text = "SAVE BOOK"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.White
        Button2.FlatAppearance.BorderColor = Color.DarkGray
        Button2.FlatAppearance.BorderSize = 1
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Button2.ForeColor = Color.DarkSlateGray
        Button2.Location = New Point(25, 495)
        Button2.Name = "Button2"
        Button2.Size = New Size(240, 40)
        Button2.TabIndex = 13
        Button2.Text = "Clear Fields"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' addBook
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(300, 560)
        Controls.Add(numCopies)
        Controls.Add(Label7)
        Controls.Add(txtbCategory)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(txtAuthor)
        Controls.Add(Label6)
        Controls.Add(txtbTitle)
        Controls.Add(Label4)
        Controls.Add(Label5)
        Controls.Add(txtISBN)
        Controls.Add(txtbID)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "addBook"
        Text = "Add Book"
        CType(numCopies, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtbID As TextBox
    Friend WithEvents txtISBN As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAuthor As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents txtbCategory As ComboBox
    Friend WithEvents txtbTitle As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents numCopies As NumericUpDown
End Class