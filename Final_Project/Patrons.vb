Imports MySql.Data.MySqlClient

Public Class Patrons
    <System.ComponentModel.Browsable(False)>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Property MemberID As Integer

    Private Sub Patrons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnAdd

        If MemberID > 0 Then
            Label1.Text = "EDIT MEMBERSHIP"
            btnAdd.Text = "UPDATE MEMBER"
            loadMemberData()
        Else
            chkIsActive.Checked = True
        End If
    End Sub

    Private Sub loadMemberData()
        Dim memberExists As Boolean = False

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim query As String =
                    "SELECT m_firstName, m_middleName, m_LastName, m_email, m_phone, " &
                    "m_isActive, m_validID, m_validIDName " &
                    "FROM members WHERE m_id = @member_id LIMIT 1"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@member_id", MemberID)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            memberExists = True
                            txtFirstName.Text = reader("m_firstName").ToString()
                            txtMiddleName.Text = reader("m_middleName").ToString()
                            txtLastName.Text = reader("m_LastName").ToString()
                            txtEmail.Text = reader("m_email").ToString()
                            txtPhone.Text = reader("m_phone").ToString()
                            txtValidID.Text = reader("m_validID").ToString()
                            txtValidIDName.Text = reader("m_validIDName").ToString()
                            chkIsActive.Checked = Convert.ToBoolean(reader("m_isActive"))
                        End If
                    End Using
                End Using
            End Using

            If Not memberExists Then
                MsgBox("The selected member no longer exists.", vbOKOnly Or vbExclamation, "Member Not Found")
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("Unable to load member details." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim middleName As String = txtMiddleName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim phone As String = txtPhone.Text.Trim()
        Dim validID As String = txtValidID.Text.Trim()
        Dim validIDName As String = txtValidIDName.Text.Trim()

        If String.IsNullOrEmpty(firstName) OrElse String.IsNullOrEmpty(lastName) OrElse String.IsNullOrEmpty(email) OrElse String.IsNullOrEmpty(phone) Then
            MsgBox("Please fill in the required member information.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        If Not isValidEmail(email) Then
            MsgBox("Please enter a valid email address.", vbOKOnly Or vbExclamation, "Warning")
            txtEmail.Focus()
            Return
        End If

        If Not isValidPhone(phone) Then
            MsgBox("Please enter a valid contact number using 7 to 15 digits.", vbOKOnly Or vbExclamation, "Warning")
            txtPhone.Focus()
            Return
        End If

        If (validID = "" AndAlso validIDName <> "") OrElse (validID <> "" AndAlso validIDName = "") Then
            MsgBox("Enter both the Valid ID Number and Valid ID Name, or leave both blank.", vbOKOnly Or vbExclamation, "Warning")
            Return
        End If

        Try
            Using conn As MySqlConnection = createDBConnection()
                conn.Open()

                Dim duplicateQuery As String = "SELECT COUNT(*) FROM members WHERE m_email = @member_email"
                If MemberID > 0 Then
                    duplicateQuery &= " AND m_id <> @member_id"
                End If

                Using duplicateCmd As New MySqlCommand(duplicateQuery, conn)
                    duplicateCmd.Parameters.AddWithValue("@member_email", email)
                    If MemberID > 0 Then
                        duplicateCmd.Parameters.AddWithValue("@member_id", MemberID)
                    End If

                    If Convert.ToInt32(duplicateCmd.ExecuteScalar()) > 0 Then
                        MsgBox("A member with this email address already exists.", vbOKOnly Or vbExclamation, "Duplicate Email")
                        txtEmail.Focus()
                        Return
                    End If
                End Using

                If MemberID > 0 AndAlso Not chkIsActive.Checked Then
                    Using borrowCmd As New MySqlCommand(
                        "SELECT COUNT(*) FROM borrowed WHERE m_id = @member_id AND return_date IS NULL", conn)
                        borrowCmd.Parameters.AddWithValue("@member_id", MemberID)

                        If Convert.ToInt32(borrowCmd.ExecuteScalar()) > 0 Then
                            MsgBox("This member still has an unreturned book. Return all borrowed books before setting the account to inactive.",
                                   vbOKOnly Or vbExclamation, "Deactivation Blocked")
                            chkIsActive.Checked = True
                            Return
                        End If
                    End Using
                End If

                If MemberID > 0 Then
                    updateMember(conn, firstName, middleName, lastName, email, phone, validID, validIDName)
                Else
                    addMember(conn, firstName, middleName, lastName, email, phone, validID, validIDName)
                End If
            End Using
        Catch ex As Exception
            MsgBox("Unable to save member information." & Environment.NewLine & ex.Message, vbOKOnly Or vbCritical, "Database Error")
        End Try
    End Sub

    Private Sub addMember(conn As MySqlConnection, firstName As String, middleName As String, lastName As String, email As String, phone As String, validID As String, validIDName As String)
        Dim query As String =
            "INSERT INTO members (m_firstName, m_middleName, m_LastName, m_email, m_phone, m_isActive, m_validID, m_validIDName) " &
            "VALUES (@first_name, @middle_name, @last_name, @email, @phone, @is_active, @valid_id, @valid_id_name)"

        Using cmd As New MySqlCommand(query, conn)
            setMemberParameters(cmd, firstName, middleName, lastName, email, phone, validID, validIDName)
            cmd.ExecuteNonQuery()
        End Using

        MsgBox("Member saved successfully.", vbOKOnly Or vbInformation, "Success")
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub updateMember(conn As MySqlConnection, firstName As String, middleName As String, lastName As String, email As String, phone As String, validID As String, validIDName As String)
        Dim query As String =
            "UPDATE members SET m_firstName = @first_name, m_middleName = @middle_name, " &
            "m_LastName = @last_name, m_email = @email, m_phone = @phone, " &
            "m_isActive = @is_active, m_validID = @valid_id, m_validIDName = @valid_id_name " &
            "WHERE m_id = @member_id"

        Using cmd As New MySqlCommand(query, conn)
            setMemberParameters(cmd, firstName, middleName, lastName, email, phone, validID, validIDName)
            cmd.Parameters.AddWithValue("@member_id", MemberID)
            cmd.ExecuteNonQuery()
        End Using

        MsgBox("Member details updated successfully.", vbOKOnly Or vbInformation, "Success")
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub setMemberParameters(cmd As MySqlCommand, firstName As String, middleName As String, lastName As String, email As String, phone As String, validID As String, validIDName As String)
        cmd.Parameters.AddWithValue("@first_name", firstName)
        cmd.Parameters.AddWithValue("@middle_name", If(middleName = "", DBNull.Value, middleName))
        cmd.Parameters.AddWithValue("@last_name", lastName)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@phone", phone)
        cmd.Parameters.AddWithValue("@is_active", If(chkIsActive.Checked, 1, 0))
        cmd.Parameters.AddWithValue("@valid_id", If(validID = "", DBNull.Value, validID))
        cmd.Parameters.AddWithValue("@valid_id_name", If(validIDName = "", DBNull.Value, validIDName))
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtFirstName.Clear()
        txtMiddleName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtValidID.Clear()
        txtValidIDName.Clear()
        chkIsActive.Checked = True
        txtFirstName.Focus()
    End Sub
End Class
