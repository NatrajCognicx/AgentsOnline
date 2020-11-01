Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLSubUsers

    Dim strAgentCode As String = ""
    Dim strAgentSubCode As String = ""
    Dim strPassword As String = ""
    Dim strSubUserName As String = ""
    Dim strSubUserEmail As String = ""
    Dim strAddress As String = ""
    Dim strTelephone As String = ""
    Dim strFax As String = ""
    Dim stradduser As String = ""
    Dim strmoduser As String = ""
    Dim stractive As String = ""
    Dim strOPMode As String = ""
    Dim strEmailConfirm As String = ""

    Public Property AgentCode As String
        Get
            Return strAgentCode
        End Get
        Set(value As String)
            strAgentCode = value
        End Set
    End Property
    Public Property AgentSubCode As String
        Get
            Return strAgentSubCode
        End Get
        Set(value As String)
            strAgentSubCode = value
        End Set
    End Property
    Public Property Password As String
        Get
            Return strPassword
        End Get
        Set(value As String)
            strPassword = value
        End Set
    End Property
    Public Property SubUserName As String
        Get
            Return strSubUserName
        End Get
        Set(value As String)
            strSubUserName = value
        End Set
    End Property
    Public Property Address As String
        Get
            Return strAddress
        End Get
        Set(value As String)
            strAddress = value
        End Set
    End Property
    Public Property Telephone As String
        Get
            Return strTelephone
        End Get
        Set(value As String)
            strTelephone = value
        End Set
    End Property

    Public Property Fax As String
        Get
            Return strFax
        End Get
        Set(value As String)
            strFax = value
        End Set
    End Property
    Public Property LoggedUser As String
        Get
            Return stradduser
        End Get
        Set(value As String)
            stradduser = value
        End Set
    End Property
    Public Property Active As String
        Get
            Return stractive
        End Get
        Set(value As String)
            stractive = value
        End Set
    End Property
    Public Property OPMode As String
        Get
            Return strOPMode
        End Get
        Set(value As String)
            strOPMode = value
        End Set
    End Property

    Public Property EmailConfirm As String
        Get
            Return strEmailConfirm
        End Get
        Set(value As String)
            strEmailConfirm = value
        End Set
    End Property

    Function GetWebAgentList(strAgentCode As String, strAgentName As String, strShortName As String, strCountryCode As String) As DataTable
        Dim dt As DataTable
        Dim objDALSubUsers As New DALSubUsers
        dt = objDALSubUsers.GetWebAgentList(strAgentCode, strAgentName, strShortName, strCountryCode)
        Return dt
    End Function

    Function GetSubUsersList(strAgentCode As String) As DataTable
        Dim dt As DataTable
        Dim objDALSubUsers As New DALSubUsers
        dt = objDALSubUsers.GetSubUsersList(strAgentCode)
        Return dt
    End Function
    Function GetSubUsersListFromSubAgentCode(strSubAgentCode As String) As DataTable
        Dim dt As DataTable
        Dim objDALSubUsers As New DALSubUsers
        dt = objDALSubUsers.GetSubUsersListFromSubAgentCode(strSubAgentCode)
        Return dt
    End Function

    Function IsExistSubUser(strEmailId As String, strId As String) As Boolean
        Dim objDALSubUsers As New DALSubUsers
        Return objDALSubUsers.IsExistSubUser(strEmailId, strId)
    End Function

    Function SaveSubUserDetails() As Boolean
        Dim objDALSubUsers As New DALSubUsers
        objDALSubUsers.AgentCode = AgentCode
        objDALSubUsers.AgentSubCode = AgentSubCode
        objDALSubUsers.SubUserName = SubUserName
        objDALSubUsers.Password = Password
        objDALSubUsers.Telephone = Telephone
        objDALSubUsers.Address = Address
        objDALSubUsers.Active = Active
        objDALSubUsers.Fax = Fax
        objDALSubUsers.LoggedUser = LoggedUser
        objDALSubUsers.OPMode = OPMode
        objDALSubUsers.EmailConfirm = EmailConfirm
        Dim strStatus As Boolean = objDALSubUsers.SaveSubUserDetails()
        Return strStatus
    End Function

    Function GetMenuList(ByVal strMenuType As String, ByVal logintype As String) As DataTable
        Dim dt As DataTable
        Dim objDALSubUsers As New DALSubUsers
        dt = objDALSubUsers.GetMenuList(strMenuType, logintype)
        Return dt
    End Function

    Function SaveUserRights(strUserType As String, strAgentCode As String, strSubUserCode As String, strUserRights As String, strLoggedUser As String) As Boolean
        Dim objDALSubUsers As New DALSubUsers
        Dim iStatus As Boolean = objDALSubUsers.SaveUserRights(strUserType, strAgentCode, strSubUserCode, strUserRights, strLoggedUser)
        Return iStatus
    End Function

    Function GetActiveMenuList(strUserType As String, strAgentCode As String, strSubUserCode As String) As DataTable
        Dim objDALSubUsers As New DALSubUsers
        Dim dt As DataTable
        dt = objDALSubUsers.GetActiveMenuList(strUserType, strAgentCode, strSubUserCode)
        Return dt
    End Function

    Function UpdateEmailConfirmStatus(strEmailCode As String) As Boolean
        Dim bStatus As Boolean
        Dim objDALSubUsers As New DALSubUsers
        bStatus = objDALSubUsers.UpdateEmailConfirmStatus(strEmailCode)
        Return bStatus
    End Function

End Class
