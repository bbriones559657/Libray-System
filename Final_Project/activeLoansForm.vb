Imports MySql.Data.MySqlClient

Public Class activeLoansForm
    Inherits Form

    Private ReadOnly dgvLoans As New DataGridView()
    Private ReadOnly txtSearch As New TextBox()
    Private ReadOnly cmbStatus As New ComboBox()
    Private ReadOnly btnRefresh As New Button()
    Private ReadOnly btnReturnSelected As New Button()

    Public Sub New()
        Me.Text = "Active Loans / Borrowers"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.WhiteSmoke
        Me.ClientSize = New Size(1040, 560)
        Me.MinimumSize = New Size(900, 520)

        Dim title As New Label() With {
            .Text = "Active Loans / Borrowers",
            .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
            .ForeColor = Color.SteelBlue,
            .AutoSize = True,
            .Location = New Point(20, 18)
        }

        Dim searchLabel As New Label() With {
            .Text = "Search:",
            .Font = New Font("Segoe UI Semibold", 9.5F),
            .ForeColor = Color.DarkSlateGray,
            .AutoSize = True,
            .Location = New Point(20, 67)
        }

        txtSearch.Font = New Font("Segoe UI", 9.5F)
        txtSearch.Location = New Point(78, 63)
        txtSearch.Size = New Size(250, 25)
        AddHandler txtSearch.TextChanged, AddressOf filterChanged

        Dim statusLabel As New Label() With {
            .Text = "Show:",
            .Font = New Font("Segoe UI Semibold", 9.5F),
            .ForeColor = Color.DarkSlateGray,
            .AutoSize = True,
            .Location = New Point(350, 67)
        }

        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.Font = New Font("Segoe UI", 9.5F)
        cmbStatus.Items.AddRange(New Object() {"All Active Loans", "Due Today", "Overdue"})
        cmbStatus.SelectedIndex = 0
        cmbStatus.Location = New Point(400, 63)
        cmbStatus.Size = New Size(155, 25)
        AddHandler cmbStatus.SelectedIndexChanged, AddressOf filterChanged

        btnRefresh.Text = "REFRESH"
        btnRefresh.BackColor = Color.SteelBlue
        btnRefresh.ForeColor = Color.White
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnRefresh.Location = New Point(575, 60)
        btnRefresh.Size = New Size(100, 30)
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click

        btnReturnSelected.Text = "RETURN SELECTED"
        btnReturnSelected.BackColor = Color.SteelBlue
        btnReturnSelected.ForeColor = Color.White
        btnReturnSelected.FlatStyle = FlatStyle.Flat
        btnReturnSelected.FlatAppearance.BorderSize = 0
        btnReturnSelected.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnReturnSelected.Location = New Point(695, 60)
        btnReturnSelected.Size = New Size(150, 30)
        AddHandler btnReturnSelected.Click, AddressOf btnReturnSelected_Click

        dgvLoans.Location = New Point(20, 105)
        dgvLoans.Size = New Size(1000, 430)
        dgvLoans.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvLoans.ReadOnly = True
        dgvLoans.AllowUserToAddRows = False
        dgvLoans.AllowUserToDeleteRows = False
        dgvLoans.MultiSelect = False
        dgvLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvLoans.BackgroundColor = Color.White
        dgvLoans.BorderStyle = BorderStyle.None
        dgvLoans.RowHeadersVisible = False
        dgvLoans.EnableHeadersVisualStyles = False
        dgvLoans.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        dgvLoans.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvLoans.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        dgvLoans.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        dgvLoans.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
        dgvLoans.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvLoans.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        dgvLoans.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)

        Me.Controls.Add(title)
        Me.Controls.Add(searchLabel)
        Me.Controls.Add(txtSearch)
        Me.Controls.Add(statusLabel)
        Me.Controls.Add(cmbStatus)
        Me.Controls.Add(btnRefresh)
        Me.Controls.Add(btnReturnSelected)
        Me.Controls.Add(dgvLoans)

        AddHandler Me.Load, AddressOf activeLoansForm_Load
        AddHandler Me.Activated, AddressOf activeLoansForm_Activated
    End Sub

    Private Sub activeLoansForm_Load(sender As Object, e As EventArgs)
        loadActiveLoans()
    End Sub

    Private Sub activeLoansForm_Activated(sender As Object, e As EventArgs)
        loadActiveLoans()
    End Sub

    Private Sub filterChanged(sender As Object, e As EventArgs)
        If Me.Visible Then loadActiveLoans()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        loadActiveLoans()
    End Sub

    Private Sub loadActiveLoans()
        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim conditions As New List(Of String) From {"br.return_date IS NULL"}

                Select Case cmbStatus.Text
                    Case "Due Today"
                        conditions.Add("br.due_date = CURDATE()")
                    Case "Overdue"
                        conditions.Add("br.due_date < CURDATE()")
                End Select

                If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                    conditions.Add("(CAST(m.m_id AS CHAR) LIKE @search OR m.m_firstName LIKE @search OR m.m_LastName LIKE @search OR b.b_title LIKE @search OR CAST(bc.bc_id AS CHAR) LIKE @search)")
                End If

                Dim query As String =
                    "SELECT br.brd_id AS `Transaction ID`, m.m_id AS `Member ID`, " &
                    "TRIM(CONCAT_WS(' ', m.m_firstName, NULLIF(m.m_middleName, ''), m.m_LastName)) AS `Borrower`, " &
                    "m.m_phone AS `Contact`, b.b_title AS `Book Title`, bc.bc_id AS `Copy ID`, " &
                    "CONCAT('COPY-', LPAD(bc.bc_id, 6, '0')) AS `Barcode`, " &
                    "br.borrow_date AS `Borrow Date`, br.due_date AS `Due Date`, " &
                    "CASE WHEN br.due_date < CURDATE() THEN 'OVERDUE' WHEN br.due_date = CURDATE() THEN 'DUE TODAY' ELSE 'BORROWED' END AS `Status`, " &
                    "GREATEST(DATEDIFF(CURDATE(), br.due_date), 0) AS `Days Late` " &
                    "FROM borrowed br " &
                    "INNER JOIN members m ON br.m_id = m.m_id " &
                    "INNER JOIN bookcopies bc ON br.bc_id = bc.bc_id " &
                    "INNER JOIN books b ON bc.b_id = b.b_id " &
                    "WHERE " & String.Join(" AND ", conditions) & " " &
                    "ORDER BY CASE WHEN br.due_date < CURDATE() THEN 0 ELSE 1 END, br.due_date, br.brd_id"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                        cmd.Parameters.AddWithValue("@search", "%" & txtSearch.Text.Trim() & "%")
                    End If

                    Using da As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        dgvLoans.DataSource = dt
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to load active loans." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub btnReturnSelected_Click(sender As Object, e As EventArgs)
        If dgvLoans.SelectedRows.Count = 0 Then
            MsgBox("Select an active loan first.", vbOKOnly Or vbExclamation, "Return Book")
            Return
        End If

        Dim value As Object = dgvLoans.SelectedRows(0).Cells("Copy ID").Value
        Dim copyID As Integer
        If value Is Nothing OrElse Not Integer.TryParse(value.ToString(), copyID) Then
            MsgBox("The selected loan does not have a valid Physical Copy ID.", vbOKOnly Or vbExclamation, "Return Book")
            Return
        End If

        Using frm As New returnBook()
            frm.PrepareCopyForReturn(copyID)
            frm.ShowDialog(Me)
        End Using

        loadActiveLoans()
        If Dashboard.Visible Then Dashboard.refreshTransactions()
    End Sub

    Public Sub RefreshLoans()
        loadActiveLoans()
    End Sub

End Class
