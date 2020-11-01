Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.IO
Imports System.Web.Security
Imports System.Configuration

Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports System.Text

Imports System.Net.Cache



Public Class BLLLogin
    Private _UserName As String = ""
    Private _Password As String = ""
    Private _IpAddress As String = ""
    Private _MainUser As String = ""
    Private _SubUser As String = ""
    Private _LoginType As String = ""
    Private _ShortName As String = ""
    Private _DivCode As String = ""

    Dim objDALLogin As New DALLogin

    Public Property UserName As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property
    Public Property IpAddress As String
        Get
            Return _IpAddress
        End Get
        Set(ByVal value As String)
            _IpAddress = value
        End Set
    End Property
    Public Property MainUser As String
        Get
            Return _MainUser
        End Get
        Set(ByVal value As String)
            _MainUser = value
        End Set
    End Property
    Public Property SubUser As String
        Get
            Return _SubUser
        End Get
        Set(ByVal value As String)
            _SubUser = value
        End Set
    End Property
    Public Property LoginType As String
        Get
            Return _LoginType
        End Get
        Set(ByVal value As String)
            _LoginType = value
        End Set
    End Property
    Public Property ShortName As String
        Get
            Return _ShortName
        End Get
        Set(ByVal value As String)
            _ShortName = value
        End Set
    End Property
    Public Property DivCode As String
        Get
            Return _DivCode
        End Get
        Set(ByVal value As String)
            _DivCode = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateUser() As DataTable
        Dim dt As DataTable
        dt = objDALLogin.ValidateUser(LoginType, UserName, Password, SubUser, IpAddress, ShortName, DivCode)
        Return dt
    End Function

    Function ValidateUserWithoutPassword() As DataTable
        Dim dt As DataTable
        dt = objDALLogin.ValidateUserWithoutPassword(LoginType, UserName, Password, SubUser, IpAddress, ShortName, DivCode)
        Return dt
    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Function IsIDPRefreshTokenRequired() As Integer
        Dim lReturnValue As Integer = 0 '0 - No action needed, 1-RefreshTokenRequired, 2 - Session Expired
        If HttpContext.Current.Session("IDPLOginType") IsNot Nothing And HttpContext.Current.Session("refreshtoken") IsNot Nothing Then
            If HttpContext.Current.Session("IDPLOginType").ToString.Trim.ToUpper = "AGENT" Then
                Dim lObjBllLogin As New BLLLogin
                Dim lCurrDate As Date
                lCurrDate = Date.Now
                If HttpContext.Current.Session("idp_refreshtoken_expiry") Is Nothing Then
                    lReturnValue = 2
                Else
                    If lCurrDate >= DateAdd(DateInterval.Minute, -5, CDate(HttpContext.Current.Session("idp_refreshtoken_expiry"))) And lCurrDate <= CDate(HttpContext.Current.Session("idp_refreshtoken_expiry")) Then
                        lReturnValue = 1
                    ElseIf lCurrDate > CDate(HttpContext.Current.Session("idp_refreshtoken_expiry")) Then
                        lReturnValue = 2
                    End If
                End If
            End If
        End If
        Return lReturnValue
    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Function IDPPostDataForRefreshToken() As Boolean 'ByVal asRefreshToken As String
        Try
            Dim request = CType(WebRequest.Create(ConfigurationManager.AppSettings("IDP_TokenURL").ToString()), HttpWebRequest)
            Dim postData = "client_id=" & Uri.EscapeDataString(ConfigurationManager.AppSettings("IDP_Code").ToString())
            postData += "&client_secret=" & Uri.EscapeDataString(ConfigurationManager.AppSettings("IDP_ClientSecret").ToString())
            postData += "&refresh_token=" & Uri.EscapeDataString(HttpContext.Current.Session("refreshtoken"))
            postData += "&redirect_uri=" & Uri.EscapeDataString(ConfigurationManager.AppSettings("IDP_RedirectURL").ToString())
            postData += "&grant_type=" & Uri.EscapeDataString("refresh_token")
            postData += "&scope=" & Uri.EscapeDataString("openid profile email")
            Dim data = Encoding.ASCII.GetBytes(postData)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = data.Length

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Dim response = CType(request.GetResponse(), HttpWebResponse)
            Dim responseString = New StreamReader(response.GetResponseStream()).ReadToEnd()
            Dim responseFromServer As String = responseString.ToString()
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Dim model As IDPLoginModel = j.Deserialize(Of IDPLoginModel)(responseFromServer)

            If model IsNot Nothing Then
                HttpContext.Current.Session("accesstoken") = model.access_token
                HttpContext.Current.Session("refreshtoken") = model.refresh_token

                Dim CurrDate As Date
                CurrDate = DateTime.Now
                CurrDate = CurrDate.AddMinutes(Convert.ToDouble(model.expires_in) / 60)
                HttpContext.Current.Session("idp_refreshtoken_expiry") = CurrDate

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            'Response.Write(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' GetAgentLogo
    ''' </summary>
    ''' <param name="strCompany"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAgentLogo(ByVal strCompany As String) As String
        Dim strLogo As String = ""
        strLogo = objDALLogin.GetAgentLogo(strCompany)
        Return strLogo
    End Function

    Function GetPhoneNo() As String
        Dim strPhoneno As String = ""
        strPhoneno = objDALLogin.GetPhoneNo()
        Return strPhoneno
    End Function

    Function GetShortName(ByVal strCompany As String) As String
        Dim strShortName As String = ""
        strShortName = objDALLogin.GetShortName(strCompany)
        Return strShortName
    End Function
    Function GetAgentName(ByVal strCompany As String) As String
        Dim strAgentName As String = ""
        strAgentName = objDALLogin.GetAgentName(strCompany)
        Return strAgentName
    End Function

    Function LoadLoginPageFields(ByVal strCompany As String, ByVal strLoginType As String, ByVal strAgentCode As String, ByVal strUserName As String) As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALLogin.LoadLoginPageFields(strCompany, strLoginType, strAgentCode, strUserName)
        Return objDataTable
    End Function
    Function LoadLoginPageSessionFields(ByVal strCompany As String) As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALLogin.LoadLoginPageSessionFields(strCompany)
        Return objDataTable
    End Function
    Function LoadLoginPageSessionFieldsAgents(ByVal strUserName As String, ByVal strPassword As String) As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALLogin.LoadLoginPageSessionFieldsAgents(strUserName, strPassword)
        Return objDataTable
    End Function
    Function GetUserWebMail() As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALLogin.GetUserWebMail(UserName, ShortName, MainUser, SubUser)
        Return objDataTable
    End Function

    Function GetRandomPassword() As String
        Dim strPassword As String = ""
        strPassword = objDALLogin.GetRandomPassword()
        Return strPassword
    End Function

    Function UpdateAgentPassword(ByVal UserName As String, ByVal ShortName As String, ByVal password As String, ByVal strUser As String) As Integer
        Dim iStatus As Integer = objDALLogin.UpdateAgentPassword(UserName, ShortName, password, strUser)
        Return iStatus
    End Function

    Sub PwdSendMailLog_Entry(ByVal AgentCode As String, ByVal strToMail As String, ByVal Pagename As String, ByVal userName As String)
        objDALLogin.PwdSendMailLog_Entry(AgentCode, strToMail, Pagename, userName)
    End Sub

    Function getTestMail() As String
        Dim strTestmail As String = objDALLogin.getTestMail()
        Return strTestmail
    End Function

    Function GetEmailParameters(ByVal strCompany As String) As DataTable
        Dim dt As DataTable = objDALLogin.GetEmailParameters(strCompany)
        Return dt
    End Function

    Function ValidateROUser(ByVal UserName As String, ByVal Password As String) As Boolean
        Dim dt As DataTable
        dt = objDALLogin.ValidateROUsers(UserName, Password)
        If dt Is Nothing Then
            Return False
        Else
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    Function ValidateROUser(ByVal UserName As String) As Boolean
        Dim dt As DataTable
        dt = objDALLogin.ValidateROUsers(UserName)
        If dt Is Nothing Then
            Return False
        Else
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    Function GetReservationParamValues(ByVal strParams As String) As DataTable
        Dim dt As DataTable
        dt = objDALLogin.GetReservationParamValues(strParams)
        Return dt
    End Function

    Function GetTermsAndCondions(ByVal strDivCode As String) As String
        Dim strTermsAndCondions As String = objDALLogin.GetTermsAndCondions(strDivCode)
        Return strTermsAndCondions
    End Function

    Function GetUnderConstructionValue() As String
        Dim strUnderConstruction As String = ""
        strUnderConstruction = objDALLogin.GetUnderConstructionValue()
        Return strUnderConstruction

    End Function

    Function ValidateOldPassword(ByVal strUserName As String, ByVal strOldPassword As String, ByVal strLoginType As String) As String
        Dim objDALLogin As New DALLogin
        Dim strValidateOldPassword As String
        strValidateOldPassword = objDALLogin.ValidateOldPassword(strUserName, strOldPassword, strLoginType)
        Return strValidateOldPassword
    End Function

    Function ChangePassword(ByVal strUserName As String, ByVal strOldPassword As String, ByVal strNewPassword As String, ByVal strLoginType As Object) As String
        Dim objDALLogin As New DALLogin
        Dim strStatus As String
        strStatus = objDALLogin.ChangePassword(strUserName, strOldPassword, strNewPassword, strLoginType)
        Return strStatus
    End Function

End Class
