Module UserSession
    Public CurrentUserID As Integer = 0
    Public CurrentUserName As String = ""
    Public CurrentUserEmail As String = ""
    Public CurrentUserRole As String = "Staff"

    Public ReadOnly Property IsLoggedIn As Boolean
        Get
            Return CurrentUserID > 0
        End Get
    End Property

    Public ReadOnly Property IsAdmin As Boolean
        Get
            Return String.Equals(CurrentUserRole, "Admin", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property

    Public Sub SetCurrentUser(userID As Integer, fullName As String, email As String, role As String)
        CurrentUserID = userID
        CurrentUserName = fullName
        CurrentUserEmail = email
        CurrentUserRole = If(String.IsNullOrWhiteSpace(role), "Staff", role.Trim())
    End Sub

    Public Sub ClearCurrentUser()
        CurrentUserID = 0
        CurrentUserName = ""
        CurrentUserEmail = ""
        CurrentUserRole = "Staff"
    End Sub
End Module
