Imports MySql.Data.MySqlClient

Public Class Dashboard
    Private dashboardFeaturesReady As Boolean = False

    Private ReadOnly pnlSidebar As New Panel()
    Private ReadOnly pnlRight As New Panel()
    Private ReadOnly pnlHeader As New Panel()
    Private ReadOnly pnlContent As New Panel()
    Private ReadOnly pnlDashboardHome As New Panel()
    Private ReadOnly pnlStats As New Panel()
    Private ReadOnly pnlHistory As New Panel()
    Private ReadOnly pnlHistoryHeader As New Panel()

    Private ReadOnly lblAppName As New Label()
    Private ReadOnly lblCurrentUser As New Label()
    Private ReadOnly btnLogout As New Button()

    Private ReadOnly btnNavDashboard As New Button()
    Private ReadOnly btnNavBooks As New Button()
    Private ReadOnly btnNavMembers As New Button()
    Private ReadOnly btnNavBorrow As New Button()
    Private ReadOnly btnNavReturn As New Button()
    Private ReadOnly btnNavActiveLoans As New Button()
    Private ReadOnly btnNavTransactions As New Button()
    Private ReadOnly btnNavReports As New Button()
    Private ReadOnly btnNavAdmin As New Button()

    Private ReadOnly lblTotalBooksValue As New Label()
    Private ReadOnly lblTotalMembersValue As New Label()
    Private ReadOnly lblBorrowedValue As New Label()
    Private ReadOnly lblOverdueValue As New Label()

    Private ReadOnly cmbDateFilter As New ComboBox()
    Private ReadOnly cmbStatusFilter As New ComboBox()
    Private ReadOnly lblDateFilter As New Label()
    Private ReadOnly lblStatusFilter As New Label()

    Private currentEmbeddedForm As Form = Nothing
    Private activeLoansPage As activeLoansForm = Nothing
    Private reportsPage As reportsForm = Nothing

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.MultiSelect = False
        initializeDashboardFeatures()
        PrepareForCurrentUser()
        showDashboardHome(True)
    End Sub

    Private Sub Dashboard_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If dashboardFeaturesReady AndAlso pnlDashboardHome.Visible Then
            refreshDashboardData()
        End If
    End Sub

    Private Sub initializeDashboardFeatures()
        If dashboardFeaturesReady Then Return
        dashboardFeaturesReady = True

        Me.MinimumSize = New Size(1100, 700)
        Me.WindowState = FormWindowState.Maximized
        Me.BackColor = Color.WhiteSmoke

        ' The original horizontal toolbar is kept in the designer for compatibility,
        ' but the running application now uses a permanent left navigation sidebar.
        ToolStrip1.Visible = False

        buildApplicationShell()
        configureTransactionHistory()
    End Sub

    Private Sub buildApplicationShell()
        pnlSidebar.Dock = DockStyle.Left
        pnlSidebar.Width = 220
        pnlSidebar.BackColor = Color.FromArgb(38, 64, 76)
        pnlSidebar.Padding = New Padding(0, 0, 0, 12)

        Dim lblBrand As New Label() With {
            .Text = "LIBRARY" & Environment.NewLine & "MANAGEMENT SYSTEM",
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 14.0F, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleLeft,
            .Dock = DockStyle.Top,
            .Height = 92,
            .Padding = New Padding(20, 18, 10, 12)
        }

        pnlSidebar.Controls.Add(createSidebarSpacer())
        addSidebarButton(btnNavAdmin, "Admin Settings", AddressOf btnNavAdmin_Click)
        addSidebarButton(btnNavReports, "Reports", AddressOf btnNavReports_Click)
        addSidebarButton(btnNavTransactions, "Transactions", AddressOf btnNavTransactions_Click)
        addSidebarButton(btnNavActiveLoans, "Active Loans", AddressOf btnNavActiveLoans_Click)
        addSidebarButton(btnNavReturn, "Return Book", AddressOf btnNavReturn_Click)
        addSidebarButton(btnNavBorrow, "Borrow Book", AddressOf btnNavBorrow_Click)
        addSidebarButton(btnNavMembers, "Members", AddressOf btnNavMembers_Click)
        addSidebarButton(btnNavBooks, "Books", AddressOf btnNavBooks_Click)
        addSidebarButton(btnNavDashboard, "Dashboard", AddressOf btnNavDashboard_Click)
        pnlSidebar.Controls.Add(lblBrand)

        pnlRight.Dock = DockStyle.Fill
        pnlRight.BackColor = Color.WhiteSmoke

        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 64
        pnlHeader.BackColor = Color.White
        pnlHeader.Padding = New Padding(24, 0, 18, 0)

        lblAppName.Text = "Dashboard"
        lblAppName.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        lblAppName.ForeColor = Color.DarkSlateGray
        lblAppName.AutoSize = True
        lblAppName.Location = New Point(24, 18)

        btnLogout.Text = "Logout"
        btnLogout.Font = New Font("Segoe UI Semibold", 9.5F)
        btnLogout.ForeColor = Color.Firebrick
        btnLogout.BackColor = Color.White
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.FlatAppearance.BorderColor = Color.Gainsboro
        btnLogout.Size = New Size(88, 34)
        btnLogout.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        AddHandler btnLogout.Click, AddressOf btnLogout_Click

        lblCurrentUser.Font = New Font("Segoe UI", 9.5F)
        lblCurrentUser.ForeColor = Color.DarkSlateGray
        lblCurrentUser.TextAlign = ContentAlignment.MiddleRight
        lblCurrentUser.AutoEllipsis = True
        lblCurrentUser.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        AddHandler pnlHeader.Resize, AddressOf header_Resize
        pnlHeader.Controls.Add(lblAppName)
        pnlHeader.Controls.Add(lblCurrentUser)
        pnlHeader.Controls.Add(btnLogout)

        pnlContent.Dock = DockStyle.Fill
        pnlContent.BackColor = Color.WhiteSmoke
        pnlContent.Padding = New Padding(24)

        pnlDashboardHome.Dock = DockStyle.Fill
        pnlDashboardHome.BackColor = Color.WhiteSmoke

        buildStatsSection()
        buildHistorySection()

        pnlDashboardHome.Controls.Add(pnlHistory)
        pnlDashboardHome.Controls.Add(pnlStats)
        pnlContent.Controls.Add(pnlDashboardHome)

        pnlRight.Controls.Add(pnlContent)
        pnlRight.Controls.Add(pnlHeader)

        Me.Controls.Add(pnlRight)
        Me.Controls.Add(pnlSidebar)

        header_Resize(Nothing, EventArgs.Empty)
    End Sub

    Private Function createSidebarSpacer() As Panel
        Return New Panel() With {
            .Dock = DockStyle.Bottom,
            .Height = 16,
            .BackColor = pnlSidebar.BackColor
        }
    End Function

    Private Sub addSidebarButton(button As Button, textValue As String, handler As EventHandler)
        button.Text = textValue
        button.Dock = DockStyle.Top
        button.Height = 48
        button.TextAlign = ContentAlignment.MiddleLeft
        button.Padding = New Padding(20, 0, 0, 0)
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        button.ForeColor = Color.WhiteSmoke
        button.BackColor = pnlSidebar.BackColor
        button.Cursor = Cursors.Hand
        AddHandler button.Click, handler
        pnlSidebar.Controls.Add(button)
    End Sub

    Private Sub buildStatsSection()
        pnlStats.Dock = DockStyle.Top
        pnlStats.Height = 125
        pnlStats.BackColor = Color.WhiteSmoke
        pnlStats.Padding = New Padding(0, 0, 0, 18)

        Dim table As New TableLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 4,
            .RowCount = 1,
            .BackColor = Color.WhiteSmoke,
            .Margin = New Padding(0)
        }

        For i As Integer = 0 To 3
            table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        Next
        table.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))

        table.Controls.Add(createStatCard("Total Books", lblTotalBooksValue), 0, 0)
        table.Controls.Add(createStatCard("Total Members", lblTotalMembersValue), 1, 0)
        table.Controls.Add(createStatCard("Borrowed Books", lblBorrowedValue), 2, 0)
        table.Controls.Add(createStatCard("Overdue Books", lblOverdueValue), 3, 0)

        pnlStats.Controls.Add(table)
    End Sub

    Private Function createStatCard(titleText As String, valueLabel As Label) As Panel
        Dim card As New Panel() With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.White,
            .Margin = New Padding(0, 0, 14, 0),
            .Padding = New Padding(18, 14, 18, 10),
            .BorderStyle = BorderStyle.FixedSingle
        }

        Dim title As New Label() With {
            .Text = titleText,
            .Dock = DockStyle.Top,
            .Height = 26,
            .Font = New Font("Segoe UI Semibold", 9.5F),
            .ForeColor = Color.DimGray,
            .TextAlign = ContentAlignment.MiddleLeft
        }

        valueLabel.Text = "0"
        valueLabel.Dock = DockStyle.Fill
        valueLabel.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold)
        valueLabel.ForeColor = Color.SteelBlue
        valueLabel.TextAlign = ContentAlignment.MiddleLeft

        card.Controls.Add(valueLabel)
        card.Controls.Add(title)
        Return card
    End Function

    Private Sub buildHistorySection()
        pnlHistory.Dock = DockStyle.Fill
        pnlHistory.BackColor = Color.White
        pnlHistory.Padding = New Padding(18)
        pnlHistory.BorderStyle = BorderStyle.FixedSingle

        pnlHistoryHeader.Dock = DockStyle.Top
        pnlHistoryHeader.Height = 72
        pnlHistoryHeader.BackColor = Color.White

        LabelTitle.Text = "Transaction History"
        LabelTitle.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.SteelBlue
        LabelTitle.AutoSize = True
        LabelTitle.Location = New Point(0, 0)

        lblDateFilter.Text = "Date:"
        lblDateFilter.AutoSize = True
        lblDateFilter.Font = New Font("Segoe UI Semibold", 9.5F)
        lblDateFilter.ForeColor = Color.DarkSlateGray
        lblDateFilter.Location = New Point(0, 43)

        cmbDateFilter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDateFilter.Font = New Font("Segoe UI", 9.5F)
        cmbDateFilter.Items.AddRange(New Object() {"All Time", "Today", "Last 7 Days", "Last 30 Days"})
        cmbDateFilter.SelectedIndex = 0
        cmbDateFilter.Location = New Point(45, 39)
        cmbDateFilter.Size = New Size(135, 25)
        AddHandler cmbDateFilter.SelectedIndexChanged, AddressOf filtersChanged

        lblStatusFilter.Text = "Status:"
        lblStatusFilter.AutoSize = True
        lblStatusFilter.Font = New Font("Segoe UI Semibold", 9.5F)
        lblStatusFilter.ForeColor = Color.DarkSlateGray
        lblStatusFilter.Location = New Point(205, 43)

        cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatusFilter.Font = New Font("Segoe UI", 9.5F)
        cmbStatusFilter.Items.AddRange(New Object() {"All", "Active", "Returned", "Due Today", "Overdue"})
        cmbStatusFilter.SelectedIndex = 0
        cmbStatusFilter.Location = New Point(263, 39)
        cmbStatusFilter.Size = New Size(125, 25)
        AddHandler cmbStatusFilter.SelectedIndexChanged, AddressOf filtersChanged

        pnlHistoryHeader.Controls.Add(LabelTitle)
        pnlHistoryHeader.Controls.Add(lblDateFilter)
        pnlHistoryHeader.Controls.Add(cmbDateFilter)
        pnlHistoryHeader.Controls.Add(lblStatusFilter)
        pnlHistoryHeader.Controls.Add(cmbStatusFilter)

        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = Point.Empty
        DataGridView1.Margin = New Padding(0)

        pnlHistory.Controls.Add(DataGridView1)
        pnlHistory.Controls.Add(pnlHistoryHeader)
    End Sub

    Private Sub configureTransactionHistory()
        If DataGridView1.Columns("DueDateColumn") Is Nothing Then
            Dim dueColumn As New DataGridViewTextBoxColumn() With {
                .Name = "DueDateColumn",
                .HeaderText = "Due Date",
                .ReadOnly = True
            }
            DataGridView1.Columns.Insert(4, dueColumn)
        End If
    End Sub

    Private Sub header_Resize(sender As Object, e As EventArgs)
        If pnlHeader.Width <= 0 Then Return

        btnLogout.Location = New Point(Math.Max(0, pnlHeader.ClientSize.Width - btnLogout.Width - 18), 15)
        lblCurrentUser.Size = New Size(290, 34)
        lblCurrentUser.Location = New Point(Math.Max(0, btnLogout.Left - lblCurrentUser.Width - 14), 15)
    End Sub

    Public Sub PrepareForCurrentUser()
        initializeDashboardFeatures()

        btnNavAdmin.Visible = IsAdmin

        Dim displayName As String = If(String.IsNullOrWhiteSpace(CurrentUserName), CurrentUserEmail, CurrentUserName)
        lblCurrentUser.Text = If(IsLoggedIn, displayName & "  •  " & CurrentUserRole, "")

        showDashboardHome(True)
    End Sub

    Public Sub refreshTransactions()
        If Me.IsDisposed Then Return
        loadTransactions()
        loadStats()
    End Sub

    Private Sub refreshDashboardData()
        loadStats()
        loadTransactions()
    End Sub

    Private Sub loadStats()
        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT " &
                    "(SELECT COUNT(*) FROM books WHERE b_isActive = 1) AS TotalBooks, " &
                    "(SELECT COUNT(*) FROM members WHERE m_isActive = 1) AS TotalMembers, " &
                    "(SELECT COUNT(*) FROM borrowed WHERE return_date IS NULL) AS BorrowedBooks, " &
                    "(SELECT COUNT(*) FROM borrowed WHERE return_date IS NULL AND due_date < CURDATE()) AS OverdueBooks"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            lblTotalBooksValue.Text = reader("TotalBooks").ToString()
                            lblTotalMembersValue.Text = reader("TotalMembers").ToString()
                            lblBorrowedValue.Text = reader("BorrowedBooks").ToString()
                            lblOverdueValue.Text = reader("OverdueBooks").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            lblTotalBooksValue.Text = "-"
            lblTotalMembersValue.Text = "-"
            lblBorrowedValue.Text = "-"
            lblOverdueValue.Text = "-"
        End Try
    End Sub

    Private Sub filtersChanged(sender As Object, e As EventArgs)
        If dashboardFeaturesReady AndAlso pnlDashboardHome.Visible Then loadTransactions()
    End Sub

    Private Sub loadTransactions()
        If Not dashboardFeaturesReady Then Return

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim whereParts As New List(Of String)()

                Select Case cmbDateFilter.Text
                    Case "Today"
                        whereParts.Add("br.borrow_date = CURDATE()")
                    Case "Last 7 Days"
                        whereParts.Add("br.borrow_date >= DATE_SUB(CURDATE(), INTERVAL 6 DAY)")
                    Case "Last 30 Days"
                        whereParts.Add("br.borrow_date >= DATE_SUB(CURDATE(), INTERVAL 29 DAY)")
                End Select

                Select Case cmbStatusFilter.Text
                    Case "Active"
                        whereParts.Add("br.return_date IS NULL")
                    Case "Returned"
                        whereParts.Add("br.return_date IS NOT NULL")
                    Case "Due Today"
                        whereParts.Add("br.return_date IS NULL AND br.due_date = CURDATE()")
                    Case "Overdue"
                        whereParts.Add("br.return_date IS NULL AND br.due_date < CURDATE()")
                End Select

                Dim whereClause As String = If(whereParts.Count = 0, "", " WHERE " & String.Join(" AND ", whereParts))

                Dim query As String =
                    "SELECT br.brd_id, m.m_LastName, m.m_id, br.borrow_date, br.due_date, br.return_date, " &
                    "CASE " &
                    "WHEN br.return_date IS NOT NULL THEN 'Returned' " &
                    "WHEN br.due_date < CURDATE() THEN 'Overdue' " &
                    "WHEN br.due_date = CURDATE() THEN 'Due Today' " &
                    "ELSE 'Borrowed' END AS transaction_status " &
                    "FROM borrowed br " &
                    "INNER JOIN members m ON br.m_id = m.m_id " &
                    whereClause & " ORDER BY br.brd_id DESC"

                Using cmd As New MySqlCommand(query, conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        DataGridView1.Rows.Clear()

                        While reader.Read()
                            Dim returnedDate As String = "-"
                            If Not IsDBNull(reader("return_date")) Then
                                returnedDate = Convert.ToDateTime(reader("return_date")).ToString("yyyy-MM-dd")
                            End If

                            Dim dueDateText As String = "-"
                            If Not IsDBNull(reader("due_date")) Then
                                dueDateText = Convert.ToDateTime(reader("due_date")).ToString("yyyy-MM-dd")
                            End If

                            DataGridView1.Rows.Add(
                                reader("brd_id").ToString(),
                                reader("m_LastName").ToString(),
                                reader("m_id").ToString(),
                                Convert.ToDateTime(reader("borrow_date")).ToString("yyyy-MM-dd"),
                                dueDateText,
                                returnedDate,
                                reader("transaction_status").ToString()
                            )
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to load transaction history." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub showDashboardHome(showStats As Boolean)
        hideEmbeddedPage()
        pnlDashboardHome.Visible = True
        pnlDashboardHome.BringToFront()
        pnlStats.Visible = showStats
        lblAppName.Text = If(showStats, "Dashboard", "Transactions")
        setActiveNavigation(If(showStats, btnNavDashboard, btnNavTransactions))
        refreshDashboardData()
    End Sub

    Private Sub showEmbeddedPage(targetForm As Form, pageTitle As String, activeButton As Button)
        If targetForm Is Nothing OrElse targetForm.IsDisposed Then Return

        If currentEmbeddedForm IsNot Nothing AndAlso currentEmbeddedForm IsNot targetForm AndAlso Not currentEmbeddedForm.IsDisposed Then
            currentEmbeddedForm.Hide()
        End If

        pnlDashboardHome.Visible = False
        currentEmbeddedForm = targetForm

        If targetForm.TopLevel Then
            targetForm.TopLevel = False
            targetForm.FormBorderStyle = FormBorderStyle.None
            targetForm.Dock = DockStyle.Fill
        End If

        If targetForm.Parent IsNot pnlContent Then
            pnlContent.Controls.Add(targetForm)
        End If

        targetForm.Show()
        targetForm.BringToFront()
        lblAppName.Text = pageTitle
        setActiveNavigation(activeButton)
        refreshEmbeddedPage(targetForm)
    End Sub

    Private Sub refreshEmbeddedPage(targetForm As Form)
        If TypeOf targetForm Is bookDashboard Then
            DirectCast(targetForm, bookDashboard).RefreshBooks()
        ElseIf TypeOf targetForm Is memberDashboard Then
            DirectCast(targetForm, memberDashboard).RefreshMembers()
        ElseIf TypeOf targetForm Is activeLoansForm Then
            DirectCast(targetForm, activeLoansForm).RefreshLoans()
        ElseIf TypeOf targetForm Is adminSettings Then
            DirectCast(targetForm, adminSettings).RefreshUsers()
        End If
    End Sub

    Private Sub hideEmbeddedPage()
        If currentEmbeddedForm IsNot Nothing AndAlso Not currentEmbeddedForm.IsDisposed Then
            currentEmbeddedForm.Hide()
        End If
        currentEmbeddedForm = Nothing
    End Sub

    Private Sub setActiveNavigation(activeButton As Button)
        Dim buttons As Button() = {
            btnNavDashboard, btnNavBooks, btnNavMembers, btnNavBorrow, btnNavReturn,
            btnNavActiveLoans, btnNavTransactions, btnNavReports, btnNavAdmin
        }

        For Each button As Button In buttons
            button.BackColor = pnlSidebar.BackColor
            button.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        Next

        If activeButton IsNot Nothing Then
            activeButton.BackColor = Color.FromArgb(52, 86, 101)
            activeButton.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        End If
    End Sub

    Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs)
        showDashboardHome(True)
    End Sub

    Private Sub btnNavTransactions_Click(sender As Object, e As EventArgs)
        showDashboardHome(False)
    End Sub

    Private Sub btnNavBooks_Click(sender As Object, e As EventArgs)
        showEmbeddedPage(bookDashboard, "Books", btnNavBooks)
    End Sub

    Private Sub btnNavMembers_Click(sender As Object, e As EventArgs)
        showEmbeddedPage(memberDashboard, "Members", btnNavMembers)
    End Sub

    Private Sub btnNavActiveLoans_Click(sender As Object, e As EventArgs)
        If activeLoansPage Is Nothing OrElse activeLoansPage.IsDisposed Then
            activeLoansPage = New activeLoansForm()
        End If
        showEmbeddedPage(activeLoansPage, "Active Loans", btnNavActiveLoans)
    End Sub

    Private Sub btnNavReports_Click(sender As Object, e As EventArgs)
        If reportsPage Is Nothing OrElse reportsPage.IsDisposed Then
            reportsPage = New reportsForm()
        End If
        showEmbeddedPage(reportsPage, "Reports", btnNavReports)
    End Sub

    Private Sub btnNavAdmin_Click(sender As Object, e As EventArgs)
        If Not IsAdmin Then
            MsgBox("Only an Administrator can open Staff Management.", vbOKOnly Or vbExclamation, "Access Denied")
            Return
        End If
        showEmbeddedPage(adminSettings, "Admin Settings", btnNavAdmin)
    End Sub

    Private Sub btnNavBorrow_Click(sender As Object, e As EventArgs)
        setActiveNavigation(btnNavBorrow)
        Using frm As New borrowBook()
            frm.ShowDialog(Me)
        End Using
        showDashboardHome(True)
    End Sub

    Private Sub btnNavReturn_Click(sender As Object, e As EventArgs)
        setActiveNavigation(btnNavReturn)
        Using frm As New returnBook()
            frm.ShowDialog(Me)
        End Using
        showDashboardHome(True)
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs)
        If MsgBox("Log out of the current account?", vbYesNo Or vbQuestion, "Logout") <> vbYes Then Return

        Dim formsToClose As New List(Of Form)()
        For Each frm As Form In Application.OpenForms
            If frm IsNot Me AndAlso frm IsNot Form1 Then
                formsToClose.Add(frm)
            End If
        Next

        For Each frm As Form In formsToClose
            frm.Close()
        Next

        currentEmbeddedForm = Nothing
        activeLoansPage = Nothing
        reportsPage = Nothing
        pnlDashboardHome.Visible = True

        ClearCurrentUser()
        Form1.ResetLoginForm()
        Form1.Show()
        Form1.BringToFront()
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex < 0 OrElse DataGridView1.Rows(e.RowIndex).Cells(0).Value Is Nothing Then Return

        Dim transactionID As Integer
        If Integer.TryParse(DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString(), transactionID) Then
            showTransactionReceipt(transactionID)
        End If
    End Sub

    Private Sub showTransactionReceipt(transactionID As Integer)
        Dim receiptText As String = ""

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT br.brd_id, br.borrow_date, br.due_date, br.return_date, " &
                    "m.m_id, TRIM(CONCAT_WS(' ', m.m_firstName, NULLIF(m.m_middleName, ''), m.m_LastName)) AS MemberName, " &
                    "b.b_id, b.b_title, b.b_isbn, bc.bc_id " &
                    "FROM borrowed br " &
                    "INNER JOIN members m ON br.m_id = m.m_id " &
                    "INNER JOIN bookcopies bc ON br.bc_id = bc.bc_id " &
                    "INNER JOIN books b ON bc.b_id = b.b_id " &
                    "WHERE br.brd_id = @transaction_id LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@transaction_id", transactionID)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim borrowDate As Date = Convert.ToDateTime(reader("borrow_date"))
                            Dim dueDate As Date = Convert.ToDateTime(reader("due_date"))
                            Dim returnDateText As String = "Not yet returned"
                            Dim status As String = "BORROWED"
                            Dim daysLate As Integer = 0

                            If Not IsDBNull(reader("return_date")) Then
                                Dim returnDate As Date = Convert.ToDateTime(reader("return_date"))
                                returnDateText = returnDate.ToString("yyyy-MM-dd")
                                daysLate = Math.Max(0, CInt((returnDate.Date - dueDate.Date).TotalDays))
                                status = "RETURNED"
                            ElseIf Date.Today > dueDate.Date Then
                                daysLate = CInt((Date.Today - dueDate.Date).TotalDays)
                                status = "OVERDUE"
                            ElseIf Date.Today = dueDate.Date Then
                                status = "DUE TODAY"
                            End If

                            Dim copyID As Integer = Convert.ToInt32(reader("bc_id"))

                            receiptText =
                                "LIBRARY TRANSACTION RECEIPT" & Environment.NewLine &
                                New String("-"c, 40) & Environment.NewLine &
                                "Transaction ID : " & reader("brd_id").ToString() & Environment.NewLine &
                                "Status         : " & status & Environment.NewLine &
                                "Member ID      : " & reader("m_id").ToString() & Environment.NewLine &
                                "Member Name    : " & reader("MemberName").ToString() & Environment.NewLine &
                                "Book ID        : " & reader("b_id").ToString() & Environment.NewLine &
                                "Book Title     : " & reader("b_title").ToString() & Environment.NewLine &
                                "ISBN           : " & reader("b_isbn").ToString() & Environment.NewLine &
                                "Copy ID        : " & copyID.ToString() & Environment.NewLine &
                                "Copy Barcode   : " & formatCopyBarcode(copyID) & Environment.NewLine &
                                "Borrow Date    : " & borrowDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                                "Due Date       : " & dueDate.ToString("yyyy-MM-dd") & Environment.NewLine &
                                "Return Date    : " & returnDateText & Environment.NewLine &
                                "Days Late      : " & daysLate.ToString() & Environment.NewLine &
                                New String("-"c, 40) & Environment.NewLine &
                                "Keep this receipt for your records."
                        End If
                    End Using
                End Using
            End Using

            If receiptText = "" Then
                MsgBox("Transaction was not found.", vbOKOnly Or vbExclamation, "Receipt")
                Return
            End If

            Using frm As New receiptForm("Transaction Receipt", receiptText)
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MsgBox("Unable to load the transaction receipt." & Environment.NewLine & ex.Message,
                   vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub Dashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
End Class
