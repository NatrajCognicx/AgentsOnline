Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Imports System.IO

Imports OneLogin.Saml
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml


Partial Class Login
    Inherits System.Web.UI.Page
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLAgentRegistration As New BLLAgentRegistration

    Dim strCompany As String = ""
    Private Function GetStyleName(Optional ByVal strClientID As String = "") As String
        '*** Danny 12/08/2018 Get Client Style or get Application Default style
        Try
            If strClientID Is Nothing Then
                strClientID = "924065660726315"
            End If
            Dim strStyle As String = ""
            strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select StylesheetName from TB_Clientstyles where RandomNumber=" + strClientID)
            If Not strStyle Is Nothing Then
                If strStyle.Trim.Length = 0 Or strStyle.Trim = "0" Then
                    strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=58")
                End If
            Else
                strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=58")
            End If
            Return strStyle
        Catch
            Return "style-style1.css"
        End Try

    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("sRequestId") = Nothing
            Session("sEditRequestId") = Nothing
            Dim IDPTargetLogoutURL As String
            If HttpContext.Current.Request.UrlReferrer IsNot Nothing Then
                IDPTargetLogoutURL = ConfigurationManager.AppSettings("idp_sso_target_logouturl").ToString()
                Response.Redirect(IDPTargetLogoutURL)
                Exit Sub
            ElseIf Trim(UCase(ConfigurationManager.AppSettings("idp_sso_manualloginrequired"))) <> "YES" Then 'HttpContext.Current.Request.UrlReferrer IsNot Nothing Then
                IDPTargetLogoutURL = ConfigurationManager.AppSettings("idp_sso_target_url").ToString()
                Response.Redirect(IDPTargetLogoutURL)
                Exit Sub
            End If

            CheckUnderConstruction()
            Dim strAbsoluteUri As String = Page.Request.Url.AbsoluteUri.ToString
            Session("sAbsoluteUrl") = strAbsoluteUri
            Session("strTheme") = ""
            Dim strTheme As String = ""

            If Not Request.QueryString("comp") Is Nothing Then

                strCompany = Request.QueryString("comp")
                Session("sAgentCompany") = strCompany
                If strCompany = "924065660726315" Then 'AgentsOnlineCommon
                    Session("sDivCode") = "01"
                    strTheme = "css/" + GetStyleName(strCompany)
                ElseIf strCompany = "675558760549078" Then 'AgentsOnlineCommon1
                    Session("sDivCode") = "02"
                    strTheme = "css/style-style3.css"
                Else
                    Session("sDivCode") = "0"
                    strTheme = "css/" + GetStyleName(strCompany)
                End If
            Else

                strCompany = "924065660726315" 'Default company 
                Session("sAgentCompany") = strCompany
                Session("sDivCode") = "01"
                strTheme = "css/" + GetStyleName(strCompany)


            End If
            Session("strTheme") = strTheme


            If Session("sobjResParam") Is Nothing Then
                Dim objResParam As New ReservationParameters
                objResParam.AbsoluteUrl = strAbsoluteUri
                objResParam.AgentCompany = strCompany
                objResParam.DivCode = Session("sDivCode")
                objResParam.CssTheme = strTheme
                Session("sobjResParam") = objResParam
            Else

                Dim objResParam As New ReservationParameters
                objResParam = Session("sobjResParam")
                objResParam.AbsoluteUrl = strAbsoluteUri
                objResParam.AgentCompany = strCompany
                objResParam.DivCode = Session("sDivCode")
                objResParam.CssTheme = strTheme
                Session("sobjResParam") = objResParam

            End If




            LoadLoginPageFields()
            'objclsUtilities.LoadTheme_New(strCompany, lnkCSS, strTheme)
            lnkCSS.Attributes("href") = Session("strTheme").ToString
            hdPopup.Value = "N"
            hdPopup.Value = "0"

            Dim strROLogin As String = ""
            If Not Request.QueryString("RO") Is Nothing Then
                If Request.QueryString("RO") = "1" Then
                    strROLogin = "1"
                End If
            End If
            If strROLogin = "1" Then

                aROLogin.Attributes.Remove("class")
                aAgentLogin.Attributes.Remove("class")
                aROLogin.Attributes.Add("class", "autorize-tab-b current")
                aAgentLogin.Attributes.Add("style", "display:none")
                aROLogin.Attributes.Add("style", "display:block")
                hdTab.Value = "1"

                ScriptManager.RegisterStartupScript(Page, GetType(Page), "Tab", "javascript:fnTabShow();", True)
            Else
                aROLogin.Attributes.Remove("class")
                aAgentLogin.Attributes.Remove("class")
                aAgentLogin.Attributes.Add("class", "autorize-tab-a current")
                aROLogin.Attributes.Add("style", "display:none")
                aAgentLogin.Attributes.Add("style", "display:block")
                hdTab.Value = "0"
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "Tab", "javascript:fnTabShow();", True)
            End If

        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Dim strROLogin As String = ""
            If Not Request.QueryString("RO") Is Nothing Then
                If Request.QueryString("RO") = "1" Then
                    strROLogin = "1"
                End If
            End If

            If strROLogin = "1" Then 'RO Login *******************
                fnROLogin()
            Else

                If txtUserName.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter username.")
                    Exit Sub
                End If
                If txtPassword.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter password.")
                    Exit Sub
                End If

                If strROLogin = "1" Then 'RO Login *******************
                Else

                    If txtShortCode.Text = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter short name.")
                        Exit Sub
                    End If
                    If chkTermsAndConditions.Checked = False Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please accept the terms and conditions.")
                        Exit Sub
                    End If
                    objBLLLogin.UserName = txtUserName.Text.Trim
                    objBLLLogin.Password = txtPassword.Text.Trim
                    objBLLLogin.ShortName = txtShortCode.Text.Trim
                    Dim LoginIp As String = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
                    objBLLLogin.LoginType = "MAIN"
                    objBLLLogin.IpAddress = LoginIp
                    objBLLLogin.DivCode = Session("sDivCode")
                    Dim dtValidate As DataTable
                    dtValidate = objBLLLogin.ValidateUser()
                    If dtValidate.Rows.Count > 0 Then
                        Session.Add("GlobalUserName", txtUserName.Text.Trim)
                        Session.Add("Userpwd", txtPassword.Text.Trim)
                        Session.Add("changeyear", Now.Year.ToString)
                        Session.Add("sLoginType", "Agent")
                        If chkRememberMe.Checked Then
                            addcookie()
                        End If


                        '    Dim objDataTable As DataTable
                        '  objDataTable = objBLLLogin.LoadLoginPageSessionFieldsAgents(txtUserName.Text.Trim, txtPassword.Text.Trim)
                        ' If objDataTable.Rows.Count > 0 Then
                        Session.Add("sAgentCode", dtValidate.Rows(0)("agentcode").ToString)
                        Session.Add("sCurrencyCode", dtValidate.Rows(0)("currcode").ToString)
                        Session.Add("sCountryCode", dtValidate.Rows(0)("ctrycode").ToString)
                        'If dtValidate.Rows(0)("agentcode").ToString = "RPMA05" Then
                        '    Session("sLang") = "ru-RU"
                        'Else
                        '    Session("sLang") = "en-us"
                        'End If
                        Session("sLang") = "en-us"
                        'End If
                        GetReservationParamValues()
                        Dim objResParam As New ReservationParameters
                        If Not Session("sobjResParam") Is Nothing Then
                            objResParam = Session("sobjResParam")
                        End If
                        objResParam.LoginIp = LoginIp
                        Dim strIpLocation As String = "" ' GetIpLocationName(LoginIp)
                        objResParam.LoginIpLocationName = strIpLocation
                        objResParam.AgentCode = dtValidate.Rows(0)("agentcode").ToString
                        objResParam.LoginType = "Agent"
                        Dim objBLLHotelSearch As New BLLHotelSearch
                        objResParam.Cumulative = objBLLHotelSearch.FindBookingEnginRateType(dtValidate.Rows(0)("agentcode").ToString)
                        objResParam.WhiteLabel = objBLLHotelSearch.FindWhiteLabel(dtValidate.Rows(0)("agentcode").ToString)
                        If dtValidate.Rows(0)("logintype").ToString = "subuser" Then
                            objResParam.SubUserCode = dtValidate.Rows(0)("agentsubcode").ToString
                            objResParam.IsSubUser = "1"
                        Else
                            objResParam.SubUserCode = ""
                            objResParam.IsSubUser = "0"
                        End If


                        Session("sobjResParam") = objResParam

                        FormsAuthentication.SetAuthCookie(txtUserName.Text.Trim, False)
                        Response.Redirect("Home.aspx", False)

                    Else
                        MessageBox.ShowMessage(Page, MessageType.Errors, "The username or password or short name you entered is incorrect")
                        txtUserName.Text = ""
                        txtPassword.Text = ""
                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Login.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub addcookie()

        If Not (Request.Cookies("user") Is Nothing) Then

            Dim cookie As New HttpCookie("user")
            cookie.Values.Add("userId", txtUserName.Text)
            cookie.Expires = DateTime.Now.AddMonths(1)
            Response.Cookies.Add(cookie)

        Else
            If Response.Cookies("user").Item("userId") <> txtUserName.Text Then

                Response.Cookies.Remove("user")

                Dim cookie As New HttpCookie("user")
                cookie.Values.Add("userId", txtUserName.Text)
                cookie.Expires = DateTime.Now.AddMonths(1)
                Response.Cookies.Add(cookie)

            End If
        End If


    End Sub
    Protected Sub addROcookie()

        If Not (Request.Cookies("ROuser") Is Nothing) Then

            Dim ROCookie As New HttpCookie("ROuser")
            ROCookie.Values.Add("ROuserId", txtROuserName.Text)
            ROCookie.Expires = DateTime.Now.AddMonths(1)
            Response.Cookies.Add(ROCookie)

        Else
            If Response.Cookies("ROuser").Item("ROuserId") <> txtROuserName.Text Then

                Response.Cookies.Remove("ROuser")

                Dim ROCookie As New HttpCookie("ROuser")
                ROCookie.Values.Add("ROUserId", txtROuserName.Text)
                ROCookie.Expires = DateTime.Now.AddMonths(1)
                Response.Cookies.Add(ROCookie)

            End If
        End If

    End Sub

    Private Sub FillPhoneNo()
        Dim strPhonNo As String = objBLLLogin.GetPhoneNo()
        If strPhonNo <> "" Then
            lblPhoneNo.Text = strPhonNo.Trim
            Session("sPhoneNo") = strPhonNo.Trim
        End If

    End Sub

    Private Sub FillShortName(ByVal strCompany As String)
        Dim strShortName As String = objBLLLogin.GetShortName(strCompany)
        If strShortName <> "" Then
            txtShortCode.Text = strShortName.Trim
            Session("sShortName") = strShortName.Trim
        End If
    End Sub

    Public Sub LoadLogo(ByVal strCompany As String)
        Dim strLogo As String = objBLLLogin.GetAgentLogo(strCompany)
        If strLogo <> "" Then
            imgLogo.Src = LogoPathFix(strLogo) '*** Danny

        End If
    End Sub
    ''' <summary>
    ''' LoadLoginPageFields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLoginPageFields()
        Try
            'Dim strCompany As String = ""
            'If Not Request.QueryString("comp") Is Nothing Then
            '    strCompany = Request.QueryString("comp")
            'Else
            '    strCompany = "924065660726315"
            'End If

            Dim objDataTable As DataTable
            objDataTable = objBLLLogin.LoadLoginPageFields(Session("sAgentCompany"), "RO", "", "LOGIN")
            If Not objDataTable Is Nothing Then
                If objDataTable.Rows.Count > 0 Then
                    If objDataTable.Rows(0)("shortname").ToString = "" Then
                        txtShortCode.Text = ""
                        txtShortCode.Enabled = True
                        btnRegister.Visible = True
                        pDontAccount.Visible = True

                    Else
                        txtShortCode.Text = objDataTable.Rows(0)("shortname").ToString
                        txtShortCode.Enabled = False
                        btnRegister.Visible = False
                        pDontAccount.Visible = False
                    End If
                    lblPhoneNo.Text = objDataTable.Rows(0)("tel1").ToString
                    Page.Title = objDataTable.Rows(0)("companyname").ToString
                    Dim strLogo As String = objDataTable.Rows(0)("logofilename").ToString
                    If strLogo <> "" Then

                        imgLogo.Src = LogoPathFix(strLogo)

                        'imgColumbusLogo.Src = "~\img\LoginLogo.png"
                    End If

                    If Session("sobjResParam") Is Nothing Then
                        Dim objResParam As New ReservationParameters
                        objResParam.PhoneNo = lblPhoneNo.Text
                        objResParam.AgentName = objDataTable.Rows(0)("agentname").ToString
                        objResParam.Logo = strLogo.Trim
                        objResParam.ShortName = objDataTable.Rows(0)("shortname").ToString
                        Session("sobjResParam") = objResParam
                    Else

                        Dim objResParam As New ReservationParameters
                        objResParam = Session("sobjResParam")
                        objResParam.PhoneNo = lblPhoneNo.Text
                        objResParam.AgentName = objDataTable.Rows(0)("agentname").ToString
                        objResParam.Logo = strLogo.Trim
                        objResParam.ShortName = objDataTable.Rows(0)("shortname").ToString
                        Session("sobjResParam") = objResParam

                    End If

                    lblContent.Text = objBLLLogin.GetTermsAndCondions(Session("sDivCode"))
                End If

            End If

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
        End Try
    End Sub
    Private Function LogoPathFix(ByVal strLogo As String) As String  '*** Danny 30/06/2018
        If File.Exists(Server.MapPath("Logos/" & strLogo.Trim)) Then '*** Danny 30/06/2018
            'strLogo = "Logos/" & strLogo.Trim     ***Priyanka 40/02/2020 for demo
            strLogo = "Logos/mahce_logo.jpg"
        Else
            strLogo = "Logos/mahce_logo.jpg"
        End If
        Session("sLogo") = strLogo.Trim
        Return strLogo.Trim
    End Function
    ''' <summary>
    ''' lbForgotPassword_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbForgotPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbForgotPassword.Click
        Try
            Dim strBody As String = ""
            Dim strCc As String = ""
            Dim strCcs As String = ""
            Dim strCcAdmin As String = ""
            Dim strFromId As String = ""
            Dim strAttachmnet As String = ""
            Dim strFileName As String = ""

            Dim objEmail As New clsEmail
            If txtUserName.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter username.")
                Exit Sub
            End If

            If txtShortCode.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter short name.")
                Exit Sub
            End If
            objBLLLogin.UserName = txtUserName.Text.Trim
            objBLLLogin.ShortName = txtShortCode.Text.Trim
            objBLLLogin.MainUser = "MAIN"
            Dim strWebMail As String = ""
            Dim strAgentName As String = ""

            'Dim strCompany As String = ""
            'If Not Request.QueryString("comp") Is Nothing Then
            '    strCompany = Request.QueryString("comp")
            'Else
            '    strCompany = "924065660726315"

            'End If
            strAgentName = objBLLLogin.GetAgentName(Session("sAgentCompany"))
            Dim dt As DataTable
            dt = objBLLLogin.GetUserWebMail()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("webemail").ToString <> "" Then
                    strWebMail = dt.Rows(0)("webemail").ToString
                    strFileName = Session("sLogo")
                    If strFileName <> "" Then
                        strAttachmnet = Server.MapPath(strFileName) '*** Danny 30/06/2018
                    End If

                    Dim password As String = objBLLLogin.GetRandomPassword()
                    Dim iStatus As Integer = objBLLLogin.UpdateAgentPassword(txtUserName.Text.Trim, txtShortCode.Text.Trim, password, "MAIN")

                    strBody = "<html><table  style='width:100%;border-width:1px;border-color:black;border-style:groove;'><tr><td><table  style='width:100%'><tr><td style='width:100%;background-color:Black;height:30px;'></td></tr> "
                    strBody += "<tr><td style='width:100%;background-color:White;height:50px;'><a href='#'><img id='imgLogo'  alt='' src='cid:" & strFileName & "' /></a></td></tr>"
                    strBody += "<tr><td style='width:750px;background:#E6E6E6;padding-left:50px;font-family:Raleway;text-decoration:none;text-align: justify;'>"
                    strBody += "<br/>Dear " & txtUserName.Text.Trim & " <br/>"
                    strBody += "<br/>Greeting from " & strAgentName & "<br/>"
                    strBody += "<br/>Please note that your password have been reset as per your request following  account Info:<br/>"
                    strBody += "<br/>Username: " & txtUserName.Text & ""
                    strBody += "<br/>Password: " & password & "<br/>"
                    strBody += "<br/><br/>  Thank you & best Regards <br/> System Administrator <br/> " & strAgentName & ""
                    strBody += "<br/><br/></td></tr>"
                    strBody += "<tr><td style='width:100%;background-color:Black;height:20px;'></td></tr></table></td></tr></table></html>"
                    strFromId = dt.Rows(0)("fromemailid").ToString
                    strCc = dt.Rows(0)("CcMail").ToString
                    strCcAdmin = dt.Rows(0)("AdminMail").ToString
                    If strCc <> "" Then
                        strCcs = strCc
                    End If
                    If strCcAdmin <> "" Then
                        strCcs = strCcs + ", " + strCcAdmin
                    End If

                    If objEmail.SendEmailCC(strFromId, strWebMail, strCcs, "Forgot Password", strBody, strAttachmnet) Then
                        MessageBox.ShowMessage(Page, MessageType.Success, "Mail has been Sent Sucessfully to your Registered Email-ID .")
                        objBLLLogin.PwdSendMailLog_Entry(dt.Rows(0)("agentcode").ToString, strWebMail + ";" + strCcs, "Forgot Password", txtUserName.Text.Trim)
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Errors, "Mail Sent Failed.")
                    End If
                Else
                    If dt.Rows(0)("option_selected").ToString = "Test" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Test Email address unavailable.")
                        Exit Sub
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Email address unavailable.")
                        Exit Sub
                    End If

                End If
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter valid username or shortname.")
                Exit Sub
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' btnRegistration_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnRegistration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistration.Click
        Try
            SaveDetails()
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' ValidatesFields
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatesFields() As Boolean
        Dim strMessages As String = ""
        If txtRegName.Text = "" Then
            strMessages = strMessages & "Enter Name.<br/> "
        End If
        If txtRegNo.Text = "" Then
            strMessages = strMessages & "Enter Company Registration No.<br/> "
        End If
        'If txtRegBuildingNo.Text = "" Then
        '    strMessages = strMessages & "Enter Building No.<br/> "
        'End If
        'If txtRegStrretLine1.Text = "" Then
        '    strMessages = strMessages & "Enter Street Line1.<br/> "
        'End If
        If txtRegCountry.Text = "" Then
            strMessages = strMessages & "Enter Country.<br/> "
        End If
        If txtRegCity.Text = "" Then
            strMessages = strMessages & "Enter City.<br/> "
        End If
        If txtRegZipCode.Text = "" Then
            strMessages = strMessages & "Enter Zip Code.<br/> "
        End If
        If txtRegEmailId.Text = "" Then
            strMessages = strMessages & "Enter EmailId.<br/> "
        End If
        If txtRegTelNo.Text = "" Then
            strMessages = strMessages & "Enter Tel No.<br/> "
        End If

        If txtRegFounded.Text = "" Then
            strMessages = strMessages & "Enter Founded Year.<br/> "
        End If
        If txtRegNoOfEmp.Text = "" Then
            strMessages = strMessages & "Enter No of Employees.<br/> "
        End If
        If txtCName.Text = "" Then
            strMessages = strMessages & "Enter Personal Name.<br/> "
        End If
        If txtCPosition.Text = "" Then
            strMessages = strMessages & "Enter Position.<br/> "
        End If
        If txtCPhoneNo.Text = "" Then
            strMessages = strMessages & "Enter Phone No.<br/> "
        End If
        'If txtCFaxNo.Text = "" Then
        '    strMessages = strMessages & "Enter Fax No.<br/> "
        'End If
        If txtCMobileNo.Text = "" Then
            strMessages = strMessages & "Enter Mobile No.<br/> "
        End If
        If txtCEmail.Text = "" Then
            strMessages = strMessages & "Enter Email.<br/> "
        End If
        If txtCaptchaCode.Text = "" Then
            strMessages = strMessages & "Enter Captcha Code.<br/> "
        Else
            ccJoin.ValidateCaptcha(txtCaptchaCode.Text)
            If Not ccJoin.UserValidated Then
                strMessages = strMessages & "Captcha Code is not valid.<br/> "
            End If
        End If

        If strMessages <> "" Then
            MessageBox.ShowMessage(Page, MessageType.Warning, strMessages)
            Return False
        Else
            Return True
        End If

    End Function
    ''' <summary>
    ''' Page_PreInit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "popup", "javascript:fnPopupShow();", True)
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "Tab", "javascript:fnTabShow();", True)
    End Sub
    ''' <summary>
    ''' GetCountry
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountry(ByVal prefixText As String) As List(Of String)
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim strCountryName As New List(Of String)
        Try
            strSqlQry = "select ctrycode,ctryname from ctrymast where active=1 and ctryname like '%" & prefixText & "%' order by ctryname "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)
            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    strCountryName.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("ctryname").ToString().ToUpper, myDS.Tables(0).Rows(i)("ctrycode").ToString()))
                Next
            End If
            Return strCountryName
        Catch ex As Exception

            Return strCountryName
        End Try
    End Function
    ''' <summary>
    ''' GetCity
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
   <System.Web.Services.WebMethod()> _
    Public Shared Function GetCity(ByVal prefixText As String) As List(Of String)
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim strCityName As New List(Of String)
        Try
            strSqlQry = "select citycode,cityname from citymast where active=1 and cityname like '%" & prefixText & "%' order by cityname "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)
            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    strCityName.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("cityname").ToString().ToUpper, myDS.Tables(0).Rows(i)("citycode").ToString()))
                Next
            End If
            Return strCityName
        Catch ex As Exception
            Return strCityName
        End Try
    End Function
    ''' <summary>
    ''' SaveDetails
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveDetails()
        Try


            If ValidatesFields() = False Then
                Exit Sub
            End If


            objBLLAgentRegistration.RegNo = txtRegNo.Text.Trim
            objBLLAgentRegistration.Name = txtRegName.Text.Trim
            objBLLAgentRegistration.BuildingNo = txtRegBuildingNo.Text.Trim
            objBLLAgentRegistration.StreetLine1 = txtRegStrretLine1.Text.Trim
            objBLLAgentRegistration.StreetLine2 = txtRegStrretLine2.Text.Trim
            objBLLAgentRegistration.CtryCode = txtCountryCode.Text.Trim
            'objBLLAgentRegistration.StateCode = txtRegState.Text.Trim

            objBLLAgentRegistration.CityCode = txtCityCode.Text.Trim
            objBLLAgentRegistration.ZipCode = txtRegZipCode.Text.Trim
            objBLLAgentRegistration.Tel1 = txtRegTelNo.Text.Trim
            objBLLAgentRegistration.Fax = txtRegFax.Text.Trim
            objBLLAgentRegistration.Email = txtRegEmailId.Text.Trim
            objBLLAgentRegistration.Type = txtRegTelNo.Text.Trim
            objBLLAgentRegistration.Founded = txtRegFounded.Text.Trim
            objBLLAgentRegistration.NoOfEmployees = txtRegNoOfEmp.Text.Trim
            objBLLAgentRegistration.AboutUs = txtRegAboutUs.Text.Trim

            objBLLAgentRegistration.Contactperson_Name = txtCName.Text.Trim
            objBLLAgentRegistration.Contactperson_PhneNo = txtCPhoneNo.Text.Trim
            objBLLAgentRegistration.Contactperson_MobileNo = txtCMobileNo.Text.Trim
            objBLLAgentRegistration.Contactperson_FaxNo = txtCFaxNo.Text.Trim
            objBLLAgentRegistration.Contactperson_Email = txtCEmail.Text.Trim
            objBLLAgentRegistration.Contactperson_Position = txtCPosition.Text.Trim

            Dim strValidate() As String
            strValidate = objBLLAgentRegistration.checkForAgentDuplicate()
            If strValidate(0) <> "" Then
                txtCaptchaCode.Text = ""
                MessageBox.ShowMessage(Page, MessageType.Warning, strValidate(0))
                Exit Sub
            End If


            Dim iStatus As Integer = objBLLAgentRegistration.SaveRegistrationDetails()
            If iStatus > 0 Then
                SendEmail()
                ClearFields()
                MessageBox.ShowMessage(Page, MessageType.Success, "Registered Successfully !! Your registration is under approval and you will be notified shortly.")
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Registered failed.")
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
        End Try

    End Sub
    ''' <summary>
    ''' ClearFields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearFields()
        txtRegNo.Text = ""
        txtRegName.Text = ""
        txtRegBuildingNo.Text = ""
        txtRegStrretLine1.Text = ""
        txtRegStrretLine2.Text = ""
        txtRegCountry.Text = ""
        txtCountryCode.Text = ""
        'txtRegState.Text = ""
        txtCityCode.Text = ""
        txtRegCity.Text = ""
        txtRegZipCode.Text = ""
        txtRegTelNo.Text = ""
        txtRegFax.Text = ""
        txtRegEmailId.Text = ""
        txtRegTelNo.Text = ""
        txtRegFounded.Text = ""
        txtRegNoOfEmp.Text = ""
        txtRegAboutUs.Text = ""
        txtCName.Text = ""
        txtCMobileNo.Text = ""
        txtCFaxNo.Text = ""
        txtCEmail.Text = ""
        txtCPosition.Text = ""
        txtCaptchaCode.Text = ""
        txtCPhoneNo.Text = ""
    End Sub
    ''' <summary>
    ''' SendEmail
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendEmail()
        Try

            Dim strFileName As String = ""
            Dim strBody As String = ""
            Dim strAttachmnet As String = ""

            Dim strAgentName As String = ""
            Dim strTestmail As String = ""
            Dim strFromId As String = ""
            Dim objEmail As New clsEmail
            'Dim strCompany As String = ""
            'If Not Request.QueryString("comp") Is Nothing Then
            '    strCompany = Request.QueryString("comp")              
            'Else
            '    strCompany = "924065660726315"
            'End If
            Dim dt As DataTable = objBLLLogin.GetEmailParameters(Session("sAgentCompany"))
            If dt.Rows.Count = 1 Then
                strAgentName = dt.Rows(0)("agentname").ToString
                strTestmail = dt.Rows(0)("TestMail").ToString
                strFromId = dt.Rows(0)("fromemailid").ToString
            End If
            If strTestmail = "" Or strFromId = "" Then
                Exit Sub
            End If

            strFileName = Session("sLogo")
            If strFileName <> "" Then
                strAttachmnet = Server.MapPath(strFileName) '*** Danny 30/06/2018
            End If

            strBody = "<html><table  style='width:100%;border-width:1px;border-color:black;border-style:groove;'><tr><td><table  style='width:100%'><tr><td style='width:100%;background-color:Black;height:30px;'></td></tr> "
            strBody += "<tr><td style='width:100%;background-color:White;height:50px;'><a href='#'><img id='imgLogo'  alt='' src='cid:" & strFileName & "' /></a></td></tr>"
            strBody += "<tr><td style='width:750px;background:#E6E6E6;padding-left:50px;font-family:Raleway;text-decoration:none;text-align: justify;'>"
            strBody += "<br/>Dear " & txtCName.Text.Trim & " <br/>"
            strBody += "<br/>Greeting from " & strAgentName & "<br/>"
            strBody += "<br/>Registration No : " + txtRegNo.Text + "<br/>"
            strBody += "<br/>Thank you for successfully registering to " & strAgentName & ". We shall be in contact with you the soonest possible time.<br/>"
            strBody += "<br/><br/>  Thank you & Best Regards <br/> " & strAgentName & ""
            strBody += "<br/><br/></td></tr>"
            strBody += "<tr><td style='width:100%;background-color:Black;height:20px;'></td></tr></table></td></tr></table></html>"

            If strTestmail = "" Then
                strTestmail = txtRegEmailId.Text.Trim
            End If

            If objEmail.SendEmailCC(strFromId, strTestmail, "", "Agent Registration", strBody, strAttachmnet) Then
                ' MessageBox.ShowMessage(Page, MessageType.Success, "Mail has been Sent Sucessfully to your Registered Email-ID .")
                objBLLLogin.PwdSendMailLog_Entry(txtRegNo.Text, strTestmail, "Agent Registration", txtCName.Text.Trim)
            Else
                objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & "Mail Sent Failed.")

            End If

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' btnROLogin_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnROLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnROLogin.Click
        fnROLogin()
    End Sub

    Protected Sub ibCaptchaRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibCaptchaRefresh.Click
        hdPopup.Value = "Y"
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "popup", "javascript:fnPopupShow();", True)
    End Sub

    Private Sub fnLogin()
        Throw New NotImplementedException
    End Sub

    Private Sub fnROLogin()
        If txtROuserName.Text = "" Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter username.")
            Exit Sub
        End If
        If txtROPassword.Text = "" Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter password.")
            Exit Sub
        End If
        'objclsUtilities.WriteErrorLog("Validate User Starts " & Date.Now)

        If objBLLLogin.ValidateROUser(txtROuserName.Text.Trim, txtROPassword.Text.Trim) = True Then
            'objclsUtilities.WriteErrorLog("Validate User End " & Date.Now)
            If chkRORemeberMe.Checked = True Then
                addROcookie()
            End If
            Session.Add("sLoginType", "RO")
            Dim objResParam As New ReservationParameters
            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
            End If
            objResParam.LoginType = "RO"
            Session("sLang") = "en-us"
            Dim objDataTable As DataTable
            objDataTable = objBLLLogin.LoadLoginPageSessionFields(Session("sAgentCompany"))
            If objDataTable.Rows.Count > 0 Then
                Session.Add("sAgentCode", objDataTable.Rows(0)("agentcode").ToString)
                Session.Add("sCurrencyCode", objDataTable.Rows(0)("currcode").ToString)
                Session.Add("sCountryCode", objDataTable.Rows(0)("ctrycode").ToString)
                Session.Add("GlobalUserName", txtROuserName.Text.Trim)
                objResParam.AgentCode = objDataTable.Rows(0)("agentcode").ToString
                objResParam.GlobalUserName = txtROuserName.Text.Trim
            Else

            End If

            'objclsUtilities.WriteErrorLog("Session " & Date.Now)

            GetReservationParamValues()

            'objclsUtilities.WriteErrorLog("Param Value " & Date.Now)

            Dim LoginIp As String = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            objResParam.LoginIp = LoginIp
            Dim strIpLocation As String = "" 'GetIpLocationName(LoginIp)
            objResParam.LoginIpLocationName = strIpLocation
            Dim strAgentCode As String = Session("sAgentCode").ToString
            Dim objBLLHotelSearch As New BLLHotelSearch()
            objResParam.Cumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
            objResParam.WhiteLabel = objBLLHotelSearch.FindWhiteLabel(strAgentCode)
            Session("sobjResParam") = objResParam

            'objclsUtilities.WriteErrorLog("Redirect Before " & Date.Now)

            Response.Redirect("Home.aspx", False)
            ' Server.Transfer("Home.aspx")


        Else
            MessageBox.ShowMessage(Page, MessageType.Errors, "The username or password you entered is incorrect.")
        End If
    End Sub

    Private Sub GetReservationParamValues()
        Dim strParams As String = "514,2009"
        Dim objReservationParm As DataTable
        objReservationParm = objBLLLogin.GetReservationParamValues(strParams)
        If Not objReservationParm Is Nothing Then
            If objReservationParm.Rows.Count > 0 Then
                Dim objResParam As New ReservationParameters
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                End If


                For i As Integer = 0 To objReservationParm.Rows.Count - 1
                    If objReservationParm.Rows(i)("param_id").ToString = "514" Then
                        objResParam.NoOfNightLimit = objReservationParm.Rows(i)("option_selected").ToString
                    End If
                    If objReservationParm.Rows(i)("param_id").ToString = "2009" Then
                        objResParam.ChildAgeLimit = objReservationParm.Rows(i)("option_selected").ToString
                    End If
                Next
                Session("sobjResParam") = objResParam
            End If
        End If
    End Sub

    Private Sub CheckUnderConstruction()
        Dim strUnderConstruction As String = ""
        strUnderConstruction = objBLLLogin.GetUnderConstructionValue()
        If strUnderConstruction = "1" Then
            Response.Redirect("UnderConstruction.aspx")
        End If
    End Sub

    Private Function GetIpLocationName(ByVal LoginIp As String) As String
        Dim location As String = ""
        Try
            '  Dim ip As String = Server.HtmlEncode(Request.UserHostAddress)
            Dim doc As XmlDocument = New XmlDocument()
            Dim getdetails As String = "http://www.freegeoip.net/xml/" + LoginIp
            doc.Load(getdetails)
            Dim nodeLstCity As XmlNodeList = doc.GetElementsByTagName("City")
            Dim nodeLstCountry As XmlNodeList = doc.GetElementsByTagName("CountryName")
            location = nodeLstCity(0).InnerText + ", " + nodeLstCountry(0).InnerText
            Return location
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("Login.aspx:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return location
        End Try
    End Function



    Protected Sub btnOktaLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOktaLogin.Click
        If HttpContext.Current.Request.UrlReferrer IsNot Nothing Then
            Dim laccountSettings As AccountSettings = New AccountSettings()
            Dim IDPTargetURL As String = ConfigurationManager.AppSettings("idp_sso_target_url").ToString
            Dim req As OneLogin.Saml.AuthRequest = New AuthRequest(New AppSettings(), laccountSettings)
            Response.Redirect(IDPTargetURL + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64)))
        End If
    End Sub
End Class
