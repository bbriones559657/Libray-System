<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        ToolStripButton1 = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        ToolStripButton2 = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        ToolStripButton3 = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        ToolStripButton4 = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        ToolStripButton5 = New ToolStripButton()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Column6 = New DataGridViewTextBoxColumn()
        LabelTitle = New Label()
        ToolStrip1.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.BackColor = Color.White
        ToolStrip1.GripStyle = ToolStripGripStyle.Hidden
        ToolStrip1.Font = New Font("Segoe UI", 10.0F)
        ToolStrip1.Items.AddRange(New ToolStripItem() {ToolStripButton1, ToolStripSeparator1, ToolStripButton2, ToolStripSeparator2, ToolStripButton3, ToolStripSeparator3, ToolStripButton4, ToolStripSeparator4, ToolStripButton5})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(10, 5, 10, 5)
        ToolStrip1.Size = New Size(850, 32)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButton1.ForeColor = Color.DarkSlateGray
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(146, 19)
        ToolStripButton1.Text = "Members Dashboard"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 22)
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButton2.ForeColor = Color.DarkSlateGray
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(51, 19)
        ToolStripButton2.Text = "Books"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 22)
        ' 
        ' ToolStripButton3
        ' 
        ToolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButton3.ForeColor = Color.DarkSlateGray
        ToolStripButton3.Name = "ToolStripButton3"
        ToolStripButton3.Size = New Size(94, 19)
        ToolStripButton3.Text = "Borrow Book"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 22)
        ' 
        ' ToolStripButton4
        ' 
        ToolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButton4.ForeColor = Color.DarkSlateGray
        ToolStripButton4.Name = "ToolStripButton4"
        ToolStripButton4.Size = New Size(90, 19)
        ToolStripButton4.Text = "Return Book"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 22)
        ' 
        ' ToolStripButton5
        ' 
        ToolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Text
        ToolStripButton5.ForeColor = Color.DarkSlateGray
        ToolStripButton5.Name = "ToolStripButton5"
        ToolStripButton5.Size = New Size(108, 19)
        ToolStripButton5.Text = "Admin Settings"
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.SteelBlue
        LabelTitle.Location = New Point(12, 50)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(215, 30)
        LabelTitle.TabIndex = 2
        LabelTitle.Text = "Transaction History"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.BackgroundColor = Color.White
        DataGridView1.BorderStyle = BorderStyle.None
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

        ' Custom Header Styling
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.SteelBlue
        DataGridViewCellStyle1.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        DataGridView1.EnableHeadersVisualStyles = False

        ' Alternating Row Colors
        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        DataGridView1.GridColor = Color.LightGray
        DataGridView1.RowHeadersVisible = False
        DataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
        DataGridView1.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black

        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2, Column3, Column4, Column5, Column6})
        DataGridView1.Location = New Point(17, 95)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.Size = New Size(815, 380)
        DataGridView1.TabIndex = 1
        ' 
        ' Column1
        ' 
        Column1.DataPropertyName = "brd_id"
        Column1.HeaderText = "Transaction ID"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.DataPropertyName = "m_LastName"
        Column2.HeaderText = "Patron Last Name"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.DataPropertyName = "m_id"
        Column3.HeaderText = "Member ID"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        ' 
        ' Column4
        ' 
        Column4.DataPropertyName = "borrow_date"
        Column4.HeaderText = "Date Issued"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        ' 
        ' Column5
        ' 
        Column5.DataPropertyName = "return_date"
        Column5.HeaderText = "Date Returned"
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        ' 
        ' Column6
        ' 
        Column6.DataPropertyName = "TransactionStatus"
        Column6.HeaderText = "Status"
        Column6.Name = "Column6"
        Column6.ReadOnly = True
        ' 
        ' Dashboard
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(850, 500)
        Controls.Add(LabelTitle)
        Controls.Add(DataGridView1)
        Controls.Add(ToolStrip1)
        Name = "Dashboard"
        Text = "Library Dashboard"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripButton4 As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripButton5 As ToolStripButton
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents LabelTitle As Label
End Class