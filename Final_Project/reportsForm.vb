Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Text
Imports System.Linq

Public Class reportsForm
    Inherits Form

    Private ReadOnly cmbReport As New ComboBox()
    Private ReadOnly cmbDatePreset As New ComboBox()
    Private ReadOnly dtpFrom As New DateTimePicker()
    Private ReadOnly dtpTo As New DateTimePicker()
    Private ReadOnly btnGenerate As New Button()
    Private ReadOnly btnExport As New Button()
    Private ReadOnly btnPrint As New Button()
    Private ReadOnly dgvReport As New DataGridView()
    Private ReadOnly lblSummary As New Label()

    Private ReadOnly reportPrintDocument As New PrintDocument()
    Private printRowIndex As Integer = 0

    Public Sub New()
        Me.Text = "Reports"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.WhiteSmoke
        Me.ClientSize = New Size(1120, 650)
        Me.MinimumSize = New Size(980, 600)

        Dim title As New Label() With {
            .Text = "Library Reports",
            .Font = New Font("Segoe UI", 16.0F, FontStyle.Bold),
            .ForeColor = Color.SteelBlue,
            .AutoSize = True,
            .Location = New Point(20, 18)
        }

        Dim reportLabel As New Label() With {
            .Text = "Report:",
            .Font = New Font("Segoe UI Semibold", 9.5F),
            .ForeColor = Color.DarkSlateGray,
            .AutoSize = True,
            .Location = New Point(20, 67)
        }

        cmbReport.DropDownStyle = ComboBoxStyle.DropDownList
        cmbReport.Font = New Font("Segoe UI", 9.5F)
        cmbReport.Items.AddRange(New Object() {
            "Circulation Transactions",
            "Overdue Loans",
            "Inventory Summary",
            "Member Activity",
            "Popular Books"
        })
        cmbReport.SelectedIndex = 0
        cmbReport.Location = New Point(80, 63)
        cmbReport.Size = New Size(210, 25)
        AddHandler cmbReport.SelectedIndexChanged, AddressOf reportSelectionChanged

        Dim periodLabel As New Label() With {
            .Text = "Period:",
            .Font = New Font("Segoe UI Semibold", 9.5F),
            .ForeColor = Color.DarkSlateGray,
            .AutoSize = True,
            .Location = New Point(315, 67)
        }

        cmbDatePreset.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDatePreset.Font = New Font("Segoe UI", 9.5F)
        cmbDatePreset.Items.AddRange(New Object() {"Today", "Last 7 Days", "Last 30 Days", "All Time", "Custom"})
        cmbDatePreset.SelectedIndex = 2
        cmbDatePreset.Location = New Point(370, 63)
        cmbDatePreset.Size = New Size(125, 25)
        AddHandler cmbDatePreset.SelectedIndexChanged, AddressOf presetChanged

        dtpFrom.Format = DateTimePickerFormat.Short
        dtpFrom.Font = New Font("Segoe UI", 9.0F)
        dtpFrom.Location = New Point(515, 63)
        dtpFrom.Size = New Size(110, 25)

        dtpTo.Format = DateTimePickerFormat.Short
        dtpTo.Font = New Font("Segoe UI", 9.0F)
        dtpTo.Location = New Point(635, 63)
        dtpTo.Size = New Size(110, 25)

        btnGenerate.Text = "GENERATE"
        styleButton(btnGenerate)
        btnGenerate.Location = New Point(765, 60)
        btnGenerate.Size = New Size(100, 30)
        AddHandler btnGenerate.Click, AddressOf btnGenerate_Click

        btnExport.Text = "EXPORT CSV"
        styleButton(btnExport)
        btnExport.Location = New Point(875, 60)
        btnExport.Size = New Size(105, 30)
        AddHandler btnExport.Click, AddressOf btnExport_Click

        btnPrint.Text = "PRINT / PDF"
        styleButton(btnPrint)
        btnPrint.Location = New Point(990, 60)
        btnPrint.Size = New Size(105, 30)
        AddHandler btnPrint.Click, AddressOf btnPrint_Click

        lblSummary.AutoSize = False
        lblSummary.Font = New Font("Segoe UI Semibold", 9.5F)
        lblSummary.ForeColor = Color.DarkSlateGray
        lblSummary.Location = New Point(20, 102)
        lblSummary.Size = New Size(1075, 25)
        lblSummary.Text = "Generate a report to view results."

        dgvReport.Location = New Point(20, 135)
        dgvReport.Size = New Size(1075, 490)
        dgvReport.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvReport.ReadOnly = True
        dgvReport.AllowUserToAddRows = False
        dgvReport.AllowUserToDeleteRows = False
        dgvReport.MultiSelect = False
        dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvReport.BackgroundColor = Color.White
        dgvReport.BorderStyle = BorderStyle.None
        dgvReport.RowHeadersVisible = False
        dgvReport.EnableHeadersVisualStyles = False
        dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvReport.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        dgvReport.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        dgvReport.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue
        dgvReport.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvReport.DefaultCellStyle.ForeColor = Color.DarkSlateGray
        dgvReport.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F)

        AddHandler reportPrintDocument.PrintPage, AddressOf reportPrintDocument_PrintPage
        AddHandler reportPrintDocument.BeginPrint, AddressOf reportPrintDocument_BeginPrint

        Me.Controls.Add(title)
        Me.Controls.Add(reportLabel)
        Me.Controls.Add(cmbReport)
        Me.Controls.Add(periodLabel)
        Me.Controls.Add(cmbDatePreset)
        Me.Controls.Add(dtpFrom)
        Me.Controls.Add(dtpTo)
        Me.Controls.Add(btnGenerate)
        Me.Controls.Add(btnExport)
        Me.Controls.Add(btnPrint)
        Me.Controls.Add(lblSummary)
        Me.Controls.Add(dgvReport)

        AddHandler Me.Load, AddressOf reportsForm_Load
        applyPreset()
    End Sub

    Private Sub styleButton(button As Button)
        button.BackColor = Color.SteelBlue
        button.ForeColor = Color.White
        button.FlatStyle = FlatStyle.Flat
        button.FlatAppearance.BorderSize = 0
        button.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        button.UseVisualStyleBackColor = False
    End Sub

    Private Sub reportsForm_Load(sender As Object, e As EventArgs)
        generateReport()
    End Sub

    Private Sub reportSelectionChanged(sender As Object, e As EventArgs)
        Dim usesDates As Boolean = cmbReport.Text <> "Inventory Summary"
        cmbDatePreset.Enabled = usesDates
        dtpFrom.Enabled = usesDates AndAlso cmbDatePreset.Text <> "All Time"
        dtpTo.Enabled = usesDates AndAlso cmbDatePreset.Text <> "All Time"
    End Sub

    Private Sub presetChanged(sender As Object, e As EventArgs)
        applyPreset()
    End Sub

    Private Sub applyPreset()
        Dim today As Date = Date.Today

        Select Case cmbDatePreset.Text
            Case "Today"
                dtpFrom.Value = today
                dtpTo.Value = today
            Case "Last 7 Days"
                dtpFrom.Value = today.AddDays(-6)
                dtpTo.Value = today
            Case "Last 30 Days"
                dtpFrom.Value = today.AddDays(-29)
                dtpTo.Value = today
            Case "All Time"
                dtpFrom.Value = New Date(2000, 1, 1)
                dtpTo.Value = today
        End Select

        Dim enableDates As Boolean = cmbDatePreset.Text <> "All Time" AndAlso cmbReport.Text <> "Inventory Summary"
        dtpFrom.Enabled = enableDates
        dtpTo.Enabled = enableDates
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs)
        generateReport()
    End Sub

    Private Function dateCondition(fieldName As String) As String
        If cmbDatePreset.Text = "All Time" Then Return ""
        Return fieldName & " BETWEEN @from_date AND @to_date"
    End Function

    Private Sub addDateParameters(cmd As MySqlCommand)
        If cmbDatePreset.Text = "All Time" OrElse cmbReport.Text = "Inventory Summary" Then Return
        cmd.Parameters.AddWithValue("@from_date", dtpFrom.Value.Date)
        cmd.Parameters.AddWithValue("@to_date", dtpTo.Value.Date)
    End Sub

    Private Sub generateReport()
        If dtpFrom.Value.Date > dtpTo.Value.Date Then
            MsgBox("The From date cannot be later than the To date.", vbOKOnly Or vbExclamation, "Reports")
            Return
        End If

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String = ""
                Dim condition As String = dateCondition("br.borrow_date")

                Select Case cmbReport.Text
                    Case "Circulation Transactions"
                        query =
                            "SELECT br.brd_id AS `Transaction ID`, " &
                            "TRIM(CONCAT_WS(' ', m.m_firstName, NULLIF(m.m_middleName, ''), m.m_LastName)) AS `Member`, " &
                            "b.b_title AS `Book Title`, CONCAT('COPY-', LPAD(bc.bc_id, 6, '0')) AS `Barcode`, " &
                            "br.borrow_date AS `Borrow Date`, br.due_date AS `Due Date`, br.return_date AS `Return Date`, " &
                            "CASE WHEN br.return_date IS NOT NULL THEN 'Returned' WHEN br.due_date < CURDATE() THEN 'Overdue' " &
                            "WHEN br.due_date = CURDATE() THEN 'Due Today' ELSE 'Borrowed' END AS `Status` " &
                            "FROM borrowed br INNER JOIN members m ON br.m_id=m.m_id " &
                            "INNER JOIN bookcopies bc ON br.bc_id=bc.bc_id INNER JOIN books b ON bc.b_id=b.b_id " &
                            If(condition = "", "", "WHERE " & condition & " ") &
                            "ORDER BY br.borrow_date DESC, br.brd_id DESC"

                    Case "Overdue Loans"
                        Dim conditions As New List(Of String) From {"br.return_date IS NULL", "br.due_date < CURDATE()"}
                        If condition <> "" Then conditions.Add(condition)
                        query =
                            "SELECT br.brd_id AS `Transaction ID`, m.m_id AS `Member ID`, " &
                            "TRIM(CONCAT_WS(' ', m.m_firstName, NULLIF(m.m_middleName, ''), m.m_LastName)) AS `Borrower`, " &
                            "m.m_phone AS `Contact`, b.b_title AS `Book Title`, CONCAT('COPY-', LPAD(bc.bc_id, 6, '0')) AS `Barcode`, " &
                            "br.borrow_date AS `Borrow Date`, br.due_date AS `Due Date`, DATEDIFF(CURDATE(), br.due_date) AS `Days Overdue` " &
                            "FROM borrowed br INNER JOIN members m ON br.m_id=m.m_id " &
                            "INNER JOIN bookcopies bc ON br.bc_id=bc.bc_id INNER JOIN books b ON bc.b_id=b.b_id " &
                            "WHERE " & String.Join(" AND ", conditions) & " ORDER BY br.due_date"

                    Case "Inventory Summary"
                        query =
                            "SELECT b.b_id AS `Book ID`, b.b_title AS `Book Title`, b.b_isbn AS `ISBN`, c.category_name AS `Category`, " &
                            "CASE WHEN b.b_isActive=1 THEN 'Active' ELSE 'Inactive' END AS `Book Status`, " &
                            "COUNT(bc.bc_id) AS `Total Copies`, " &
                            "SUM(CASE WHEN bc.bc_status='Available' THEN 1 ELSE 0 END) AS `Available`, " &
                            "SUM(CASE WHEN bc.bc_status='Borrowed' THEN 1 ELSE 0 END) AS `Borrowed`, " &
                            "SUM(CASE WHEN bc.bc_status='Unavailable' THEN 1 ELSE 0 END) AS `Unavailable` " &
                            "FROM books b LEFT JOIN categories c ON b.b_category=c.category_id " &
                            "LEFT JOIN bookcopies bc ON b.b_id=bc.b_id " &
                            "GROUP BY b.b_id, b.b_title, b.b_isbn, c.category_name, b.b_isActive ORDER BY b.b_title"

                    Case "Member Activity"
                        Dim joinDate As String = If(condition = "", "", " AND " & condition)
                        query =
                            "SELECT m.m_id AS `Member ID`, TRIM(CONCAT_WS(' ', m.m_firstName, NULLIF(m.m_middleName, ''), m.m_LastName)) AS `Member`, " &
                            "CASE WHEN m.m_isActive=1 THEN 'Active' ELSE 'Inactive' END AS `Member Status`, " &
                            "COUNT(br.brd_id) AS `Transactions`, " &
                            "COALESCE(SUM(CASE WHEN br.brd_id IS NOT NULL AND br.return_date IS NULL THEN 1 ELSE 0 END),0) AS `Current Loans`, " &
                            "COALESCE(SUM(CASE WHEN br.brd_id IS NOT NULL AND br.return_date IS NULL AND br.due_date < CURDATE() THEN 1 ELSE 0 END),0) AS `Overdue`, " &
                            "MAX(br.borrow_date) AS `Last Borrowed` " &
                            "FROM members m LEFT JOIN borrowed br ON m.m_id=br.m_id" & joinDate & " " &
                            "GROUP BY m.m_id, m.m_firstName, m.m_middleName, m.m_LastName, m.m_isActive ORDER BY `Transactions` DESC, `Member`"

                    Case "Popular Books"
                        Dim joinDate As String = If(condition = "", "", " AND " & condition)
                        query =
                            "SELECT b.b_id AS `Book ID`, b.b_title AS `Book Title`, c.category_name AS `Category`, " &
                            "COUNT(br.brd_id) AS `Times Borrowed`, " &
                            "COALESCE(SUM(CASE WHEN br.brd_id IS NOT NULL AND br.return_date IS NULL THEN 1 ELSE 0 END),0) AS `Currently Borrowed` " &
                            "FROM books b LEFT JOIN categories c ON b.b_category=c.category_id " &
                            "LEFT JOIN bookcopies bc ON b.b_id=bc.b_id " &
                            "LEFT JOIN borrowed br ON bc.bc_id=br.bc_id" & joinDate & " " &
                            "GROUP BY b.b_id, b.b_title, c.category_name ORDER BY `Times Borrowed` DESC, b.b_title"
                End Select

                Using cmd As New MySqlCommand(query, conn)
                    addDateParameters(cmd)
                    Using da As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)
                        dgvReport.DataSource = dt
                        lblSummary.Text = cmbReport.Text & " — " & dt.Rows.Count.ToString() & " row(s)" & getPeriodText()
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to generate the report." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Report Error")
        End Try
    End Sub

    Private Function getPeriodText() As String
        If cmbReport.Text = "Inventory Summary" Then Return ""
        If cmbDatePreset.Text = "All Time" Then Return " — All Time"
        Return " — " & dtpFrom.Value.ToString("yyyy-MM-dd") & " to " & dtpTo.Value.ToString("yyyy-MM-dd")
    End Function

    Private Sub btnExport_Click(sender As Object, e As EventArgs)
        If dgvReport.Columns.Count = 0 OrElse dgvReport.Rows.Count = 0 Then
            MsgBox("Generate a report before exporting.", vbOKOnly Or vbExclamation, "Export")
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV file (*.csv)|*.csv"
            saveDialog.FileName = cmbReport.Text.Replace(" ", "_") & "_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Try
                Using writer As New IO.StreamWriter(saveDialog.FileName, False, New UTF8Encoding(True))
                    Dim headers As New List(Of String)()
                    For Each column As DataGridViewColumn In dgvReport.Columns
                        If column.Visible Then headers.Add(csvEscape(column.HeaderText))
                    Next
                    writer.WriteLine(String.Join(",", headers))

                    For Each row As DataGridViewRow In dgvReport.Rows
                        If row.IsNewRow Then Continue For
                        Dim values As New List(Of String)()
                        For Each column As DataGridViewColumn In dgvReport.Columns
                            If column.Visible Then
                                values.Add(csvEscape(If(row.Cells(column.Index).Value, "").ToString()))
                            End If
                        Next
                        writer.WriteLine(String.Join(",", values))
                    Next
                End Using

                MsgBox("Report exported successfully." & Environment.NewLine & saveDialog.FileName,
                       vbOKOnly Or vbInformation, "Export Complete")
            Catch ex As Exception
                MsgBox("Unable to export the report." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Export Error")
            End Try
        End Using
    End Sub

    Private Function csvEscape(value As String) As String
        If value Is Nothing Then value = ""
        Return """" & value.Replace("""", """""") & """"
    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        If dgvReport.Columns.Count = 0 OrElse dgvReport.Rows.Count = 0 Then
            MsgBox("Generate a report before printing.", vbOKOnly Or vbExclamation, "Print")
            Return
        End If

        Using dialog As New PrintDialog()
            dialog.Document = reportPrintDocument
            dialog.UseEXDialog = True
            If dialog.ShowDialog(Me) = DialogResult.OK Then
                Try
                    reportPrintDocument.Print()
                Catch ex As Exception
                    MsgBox("Unable to print the report." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Print Error")
                End Try
            End If
        End Using
    End Sub

    Private Sub reportPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)
        printRowIndex = 0
    End Sub

    Private Sub reportPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim x As Single = e.MarginBounds.Left
        Dim y As Single = e.MarginBounds.Top
        Dim titleFont As New Font("Segoe UI", 14.0F, FontStyle.Bold)
        Dim normalFont As New Font("Segoe UI", 8.0F)
        Dim headerFont As New Font("Segoe UI", 8.0F, FontStyle.Bold)

        e.Graphics.DrawString("Library Report - " & cmbReport.Text, titleFont, Brushes.Black, x, y)
        y += 30
        e.Graphics.DrawString(getPeriodText().Trim(), normalFont, Brushes.Black, x, y)
        y += 25

        Dim visibleColumns = dgvReport.Columns.Cast(Of DataGridViewColumn)().Where(Function(c) c.Visible).ToList()
        If visibleColumns.Count = 0 Then Return

        Dim columnWidth As Single = e.MarginBounds.Width / visibleColumns.Count
        For i As Integer = 0 To visibleColumns.Count - 1
            e.Graphics.DrawString(visibleColumns(i).HeaderText, headerFont, Brushes.Black, x + (i * columnWidth), y)
        Next
        y += 22
        e.Graphics.DrawLine(Pens.Black, x, y, e.MarginBounds.Right, y)
        y += 5

        While printRowIndex < dgvReport.Rows.Count
            Dim row As DataGridViewRow = dgvReport.Rows(printRowIndex)
            If row.IsNewRow Then
                printRowIndex += 1
                Continue While
            End If

            If y + 35 > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return
            End If

            For i As Integer = 0 To visibleColumns.Count - 1
                Dim value As String = If(row.Cells(visibleColumns(i).Index).Value, "").ToString()
                Dim rect As New RectangleF(x + (i * columnWidth), y, columnWidth - 4, 32)
                e.Graphics.DrawString(value, normalFont, Brushes.Black, rect)
            Next

            y += 35
            printRowIndex += 1
        End While

        e.HasMorePages = False
    End Sub
End Class
