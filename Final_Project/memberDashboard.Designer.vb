<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class memberDashboard
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
        dgvMembers = New DataGridView()
        ColID = New DataGridViewTextBoxColumn()
        ColName = New DataGridViewTextBoxColumn()
        ColEmail = New DataGridViewTextBoxColumn()
        ColPhone = New DataGridViewTextBoxColumn()
        ColStatus = New DataGridViewTextBoxColumn()
        btnAddMember = New Button()
        btnEditMember = New Button()
        CType(dgvMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        Label11.ForeColor = Color.SteelBlue
        Label11.Location = New Point(21, 15)
        Label11.Name = "Label11"
        Label11.Size = New Size(150, 30)
        Label11.TabIndex = 11
        Label11.Text = "Members List"
        ' 
        ' dgvMembers
        ' 
        dgvMembers.AllowUserToAddRows = False
        dgvMembers.AllowUserToDeleteRows = False
        dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMembers.BackgroundColor = Color.White
        dgvMembers.BorderStyle = BorderStyle.None
        dgvMembers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.SteelBlue
        DataGridViewCellStyle1.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvMembers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvMembers.EnableHeadersVisualStyles = False

        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        dgvMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        dgvMembers.DefaultCellStyle.Font = New Font("Segoe UI", 9.5F)
        dgvMembers.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        dgvMembers.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
        dgvMembers.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvMembers.RowHeadersVisible = False

        dgvMembers.Columns.AddRange(New DataGridViewColumn() {ColID, ColName, ColEmail, ColPhone, ColStatus})
        dgvMembers.Location = New Point(21, 60)
        dgvMembers.Name = "dgvMembers"
        dgvMembers.ReadOnly = True
        dgvMembers.Size = New Size(700, 250)
        dgvMembers.TabIndex = 12
        ' 
        ' ColID
        ' 
        ColID.DataPropertyName = "m_id"
        ColID.HeaderText = "Member ID"
        ColID.Name = "ColID"
        ColID.ReadOnly = True
        ' 
        ' ColName
        ' 
        ColName.DataPropertyName = "FullName"
        ColName.HeaderText = "Full Name"
        ColName.Name = "ColName"
        ColName.ReadOnly = True
        ' 
        ' ColEmail
        ' 
        ColEmail.DataPropertyName = "m_email"
        ColEmail.HeaderText = "Email"
        ColEmail.Name = "ColEmail"
        ColEmail.ReadOnly = True
        ' 
        ' ColPhone
        ' 
        ColPhone.DataPropertyName = "m_phone"
        ColPhone.HeaderText = "Contact"
        ColPhone.Name = "ColPhone"
        ColPhone.ReadOnly = True
        ' 
        ' ColStatus
        ' 
        ColStatus.DataPropertyName = "AccountStatus"
        ColStatus.HeaderText = "Status"
        ColStatus.Name = "ColStatus"
        ColStatus.ReadOnly = True
        ' 
        ' btnAddMember
        ' 
        btnAddMember.BackColor = Color.SteelBlue
        btnAddMember.FlatAppearance.BorderSize = 0
        btnAddMember.FlatStyle = FlatStyle.Flat
        btnAddMember.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        btnAddMember.ForeColor = Color.White
        btnAddMember.Location = New Point(21, 330)
        btnAddMember.Name = "btnAddMember"
        btnAddMember.Size = New Size(150, 35)
        btnAddMember.TabIndex = 13
        btnAddMember.Text = "Add New Member"
        btnAddMember.UseVisualStyleBackColor = False
        ' 
        ' btnEditMember
        ' 
        btnEditMember.BackColor = Color.White
        btnEditMember.FlatAppearance.BorderColor = Color.SteelBlue
        btnEditMember.FlatAppearance.BorderSize = 1
        btnEditMember.FlatStyle = FlatStyle.Flat
        btnEditMember.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        btnEditMember.ForeColor = Color.SteelBlue
        btnEditMember.Location = New Point(185, 330)
        btnEditMember.Name = "btnEditMember"
        btnEditMember.Size = New Size(150, 35)
        btnEditMember.TabIndex = 14
        btnEditMember.Text = "Edit Member Details"
        btnEditMember.UseVisualStyleBackColor = False
        ' 
        ' memberDashboard
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(740, 390)
        Controls.Add(btnEditMember)
        Controls.Add(btnAddMember)
        Controls.Add(dgvMembers)
        Controls.Add(Label11)
        Name = "memberDashboard"
        Text = "Members Dashboard"
        CType(dgvMembers, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label11 As Label
    Friend WithEvents dgvMembers As DataGridView
    Friend WithEvents ColID As DataGridViewTextBoxColumn
    Friend WithEvents ColName As DataGridViewTextBoxColumn
    Friend WithEvents ColEmail As DataGridViewTextBoxColumn
    Friend WithEvents ColPhone As DataGridViewTextBoxColumn
    Friend WithEvents ColStatus As DataGridViewTextBoxColumn
    Friend WithEvents btnAddMember As Button
    Friend WithEvents btnEditMember As Button
End Class