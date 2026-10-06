Imports MySql.Data.MySqlClient

Public Class memberDashboard
    Private Sub memberDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvMembers.AutoGenerateColumns = False
        dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMembers.MultiSelect = False
        loadMembers()
    End Sub

    Private Sub memberDashboard_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        loadMembers()
    End Sub

    Private Sub loadMembers()
        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT m_id, " &
                    "TRIM(CONCAT_WS(' ', m_firstName, NULLIF(m_middleName, ''), m_LastName)) AS FullName, " &
                    "m_email, m_phone, " &
                    "CASE WHEN m_isActive = 1 THEN 'Active' ELSE 'Inactive' END AS AccountStatus " &
                    "FROM members ORDER BY m_id DESC"

                Using da As New MySqlDataAdapter(query, conn)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    dgvMembers.DataSource = dt
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Unable to load members." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Function getSelectedMemberID() As Integer
        If dgvMembers.CurrentRow Is Nothing OrElse dgvMembers.CurrentRow.Cells("ColID").Value Is Nothing Then
            Return 0
        End If

        Dim memberID As Integer
        If Integer.TryParse(dgvMembers.CurrentRow.Cells("ColID").Value.ToString(), memberID) Then
            Return memberID
        End If

        Return 0
    End Function

    Private Sub btnAddMember_Click(sender As Object, e As EventArgs) Handles btnAddMember.Click
        Using frm As New Patrons()
            frm.ShowDialog(Me)
        End Using
        loadMembers()
    End Sub

    Private Sub btnEditMember_Click(sender As Object, e As EventArgs) Handles btnEditMember.Click
        Dim memberID As Integer = getSelectedMemberID()
        If memberID = 0 Then
            MsgBox("Please select a member to edit.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        Using frm As New Patrons()
            frm.MemberID = memberID
            frm.ShowDialog(Me)
        End Using

        loadMembers()
    End Sub

    Private Sub dgvMembers_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMembers.CellDoubleClick
        If e.RowIndex >= 0 Then
            btnEditMember.PerformClick()
        End If
    End Sub

    Public Sub RefreshMembers()
        loadMembers()
    End Sub

End Class
