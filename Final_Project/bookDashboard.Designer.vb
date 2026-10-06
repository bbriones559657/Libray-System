<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class bookDashboard
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
        dgvBooks = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        lblSB = New Label()
        ComboBox1 = New ComboBox()
        CType(dgvBooks, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label11.ForeColor = Color.SteelBlue
        Label11.Location = New Point(21, 15)
        Label11.Name = "Label11"
        Label11.Size = New Size(174, 30)
        Label11.TabIndex = 11
        Label11.Text = "Book Inventory"
        ' 
        ' dgvBooks
        ' 
        dgvBooks.AllowUserToAddRows = False
        dgvBooks.AllowUserToDeleteRows = False
        dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvBooks.BackgroundColor = Color.White
        dgvBooks.BorderStyle = BorderStyle.None
        dgvBooks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.SteelBlue
        DataGridViewCellStyle1.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvBooks.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvBooks.EnableHeadersVisualStyles = False

        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        dgvBooks.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvBooks.DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
        dgvBooks.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        dgvBooks.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
        dgvBooks.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvBooks.RowHeadersVisible = False

        dgvBooks.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3, Column4, Column5})
        dgvBooks.Location = New Point(21, 100)
        dgvBooks.Name = "dgvBooks"
        dgvBooks.ReadOnly = True
        dgvBooks.Size = New Size(700, 250)
        dgvBooks.TabIndex = 12
        ' 
        ' Column1 (and others truncated for brevity in property listing)
        ' 
        Column1.DataPropertyName = "b_id"
        Column1.HeaderText = "Book ID"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.DataPropertyName = "b_title"
        Column2.HeaderText = "Book Title"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.DataPropertyName = "category_name"
        Column3.HeaderText = "Category"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        ' 
        ' Column4
        ' 
        Column4.DataPropertyName = "AvailableCopies"
        Column4.HeaderText = "Available Copies"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        ' 
        ' Column5
        ' 
        Column5.DataPropertyName = "b_isbn"
        Column5.HeaderText = "ISBN"
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        ' 
        ' Buttons
        ' 
        Button1.BackColor = Color.White
        Button1.FlatAppearance.BorderColor = Color.SteelBlue
        Button1.FlatAppearance.BorderSize = 1
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        Button1.ForeColor = Color.SteelBlue
        Button1.Location = New Point(21, 370)
        Button1.Name = "Button1"
        Button1.Size = New Size(119, 34)
        Button1.TabIndex = 13
        Button1.Text = "Full Details"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.SteelBlue
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        Button2.ForeColor = Color.White
        Button2.Location = New Point(157, 370)
        Button2.Name = "Button2"
        Button2.Size = New Size(119, 34)
        Button2.TabIndex = 14
        Button2.Text = "Add Books"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button3
        ' 
        Button3.BackColor = Color.SteelBlue
        Button3.FlatAppearance.BorderSize = 0
        Button3.FlatStyle = FlatStyle.Flat
        Button3.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        Button3.ForeColor = Color.White
        Button3.Location = New Point(292, 370)
        Button3.Name = "Button3"
        Button3.Size = New Size(119, 34)
        Button3.TabIndex = 15
        Button3.Text = "Edit Books"
        Button3.UseVisualStyleBackColor = False
        ' 
        ' Button4
        ' 
        Button4.BackColor = Color.DarkRed
        Button4.FlatAppearance.BorderSize = 0
        Button4.FlatStyle = FlatStyle.Flat
        Button4.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        Button4.ForeColor = Color.White
        Button4.Location = New Point(427, 370)
        Button4.Name = "Button4"
        Button4.Size = New Size(119, 34)
        Button4.TabIndex = 16
        Button4.Text = "Delete Books"
        Button4.UseVisualStyleBackColor = False
        ' 
        ' Search UI
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = Color.DarkSlateGray
        Label2.Location = New Point(21, 65)
        Label2.Name = "Label2"
        Label2.Text = "Filter By:"

        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.Font = New Font("Segoe UI", 10.0F)
        ComboBox1.Items.AddRange(New Object() {"ID", "Title", "Category"})
        ComboBox1.Location = New Point(85, 62)
        ComboBox1.Size = New Size(121, 25)
        ComboBox1.TabIndex = 22

        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label1.ForeColor = Color.DarkSlateGray
        Label1.Location = New Point(230, 65)
        Label1.Text = "Search:"

        TextBox1.Font = New Font("Segoe UI", 10.0F)
        TextBox1.Location = New Point(290, 62)
        TextBox1.Size = New Size(256, 25)
        TextBox1.TabIndex = 17

        lblSB.Visible = False
        ' 
        ' bookDashboard
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(740, 430)
        Controls.Add(ComboBox1)
        Controls.Add(lblSB)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox1)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(dgvBooks)
        Controls.Add(Label11)
        Name = "bookDashboard"
        Text = "Book Inventory"
        CType(dgvBooks, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents dgvBooks As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents lblSB As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
End Class