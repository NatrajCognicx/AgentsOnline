Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Security.Cryptography

Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography.Xml
Imports System.Xml
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Net.Cache
Imports System.Web.Script.Serialization
Imports System.Configuration
Imports System.Web.Security

Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Collections

Partial Class ChangePassword
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLHotelSearch As New BLLHotelSearch
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Private strTotalValueHeading As String = ""
    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton
    Dim objResParam As New ReservationParameters
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        '-------------------------------------
        'changed by mohamed on 01/10/2020
        'Check IDP Login for Agent 
        Dim lIDPRefreshActionRequired As Integer
        lIDPRefreshActionRequired = objBLLLogin.IsIDPRefreshTokenRequired()
        If lIDPRefreshActionRequired = 2 Then
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('IDP Login Time Expired, please close and open the application again');", True)
            Response.Redirect("~\idplogin.aspx")
            Exit Sub
        ElseIf lIDPRefreshActionRequired = 1 Then
            If objBLLLogin.IDPPostDataForRefreshToken = False Then
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('Something went wrong while refreshing token');", True)
                Response.Redirect("~\idplogin.aspx")
                Exit Sub
            End If
        End If
        '-------------------------------------

        If Not IsPostBack Then
            Try

                Session("State") = "New"
                'Session("lineno") = Nothing
                If Session("sAgentCompany") Is Nothing Or Session("GlobalUserName") Is Nothing Then
                    Dim strAbsoluteUrl As String = hdAbsoluteUrl.Value
                    If (strAbsoluteUrl = "") Then
                        strAbsoluteUrl = "Login.aspx"
                    End If

                    Response.Redirect(strAbsoluteUrl, True)
                End If
                If Not Session("sAbsoluteUrl") Is Nothing Then
                    Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
                    hdAbsoluteUrl.Value = strAbsoluteUrl
                End If

                LoadHome()
                txtOldPassword.Focus()

            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("ChangePassword.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
            End Try

        End If
    End Sub

    'Protected Overrides Sub OnInit(ByVal e As EventArgs)
    '    MyBase.OnInit(e)

    'End Sub

    Private Sub LoadHome()



        LoadFooter()
        LoadLogo()
        LoadMenus()
        LoadSubMenus()
        objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        LoadFields()
        ShowMyBooking()
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If
    End Sub

    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = "Logos/" & strLogo

            End If

        End If
    End Sub

    'Private Sub LoadMenus()
    '    Try


    '        Dim strType As String = ""
    '        Dim strMenuMobHtml As New StringBuilder
    '        Dim strMenuHtml As New StringBuilder

    '        Dim dtMenus As DataTable
    '        objResParam = Session("sobjResParam")
    '        dtMenus = objBLLMenu.Getmenus(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode)
    '        If dtMenus.Rows.Count > 0 Then
    '            strMenuMobHtml.Append(" <nav><ul>")
    '            strMenuHtml.Append(" <nav  class='header-nav'><ul>") '
    '            For i As Integer = 0 To dtMenus.Rows.Count - 1
    '                strMenuMobHtml.Append("<li><a href='" & dtMenus.Rows(i)("pagename").ToString & "'>" & dtMenus.Rows(i)("menudesc").ToString & "</a></li>")
    '                If dtMenus.Rows(i)("pagename").ToString = "" Then
    '                    strMenuHtml.Append("<li><a style='color:#2c2c2c;'>" & dtMenus.Rows(i)("menudesc").ToString & "</a></li>")
    '                Else
    '                    strMenuHtml.Append("<li><a href='" & dtMenus.Rows(i)("pagename").ToString & "'>" & dtMenus.Rows(i)("menudesc").ToString & "</a></li>")
    '                End If

    '            Next
    '            strMenuHtml.Append("</ul></nav>")
    '            strMenuMobHtml.Append("</ul></nav>")
    '        End If
    '        ltMenu.Text = strMenuHtml.ToString
    '        dvMobmenu.InnerHtml = strMenuMobHtml.ToString
    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("ChangePassword.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub

    'Modified param 17/10/2018
    Private Sub LoadMenus()
        Try
            ' Modified by abin on 20180717
            Dim strType As String = ""
            Dim strMenuMobHtml As New StringBuilder
            Dim strMenuHtml As String = ""
            objResParam = Session("sobjResParam")
            strMenuHtml = objBLLMenu.GetMenusReturnAstring(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode)

            ltMenu.Text = strMenuHtml.ToString
            dvMobmenu.InnerHtml = strMenuHtml.ToString
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub LoadFields()

        If Not Session("GlobalUserName") Is Nothing Then
            lblHeaderUserName.Text = Session("GlobalUserName")
            txtUserName.Text = Session("GlobalUserName").ToString
        End If
        dvCurrency.InnerHtml = "<a  href='#'>" & Session("sCurrencyCode") & "</a>"
        If Not Session("sLoginType") Is Nothing And Not Session("sCurrencyCode") Is Nothing Then
            If Session("sLoginType") = "RO" Then
                dvFlag.Visible = False
                dvCurrency.Visible = False

            Else
                dvFlag.Visible = False
                dvCurrency.Visible = True
                If Session("sCurrencyCode") = "AED" Then
                    imgLang.Src = "img/uae.gif"
                Else
                    imgLang.Src = "img/en.gif"
                End If

            End If
        Else
            dvFlag.Visible = False
            dvCurrency.Visible = False
        End If
        Dim objDataTable As DataTable
        objDataTable = objBLLLogin.LoadLoginPageFields(Session("sAgentCompany"), Session("sLoginType"), Session("sAgentCode"), Session("GlobalUserName"))
        If Not objDataTable Is Nothing Then
            If objDataTable.Rows.Count > 0 Then
                lblPhoneNo.Text = objDataTable.Rows(0)("tel1").ToString
                Page.Title = objDataTable.Rows(0)("companyname").ToString 'companyname
                lblHeaderAgentName.Text = objDataTable.Rows(0)("agentname").ToString
            End If
        End If


        If Not Session("sLoginType") Is Nothing Then
            hdLoginType.Value = Session("sLoginType")
            If Session("sLoginType") <> "RO" Then
   
                Dim strSqlQry As String = ""
                Dim myDS As New DataSet
                Dim Hotelnames As New List(Of String)
                Try



                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' order by ctryname"

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count = 1 Then
                     
                    Else
                    
                    End If


                Catch ex As Exception

                End Try
            Else
             
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub




    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogOut.Click
        Try
            Dim strAbsoluteUrl As String = hdAbsoluteUrl.Value
            If (strAbsoluteUrl = "") Then
                strAbsoluteUrl = "Login.aspx"
            End If
            Session.Clear()
            Session.Abandon()
            Response.Redirect(strAbsoluteUrl, False)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("ChangePassword.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub



    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
        Try
            If Session("sRequestId") Is Nothing Then
                MessageBox.ShowMessage(Page, MessageType.Info, "You have no booking.")
            Else
                Dim objBLLCommonFuntions As New BLLCommonFuntions
                Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                If dt.Rows.Count > 0 Then
                    Response.Redirect("MoreServices.aspx")
                Else
                    MessageBox.ShowMessage(Page, MessageType.Info, "You have no booking.")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("ChangePassword.aspx ::btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub ShowMyBooking()
        If Session("sRequestId") Is Nothing Then
            dvMybooking.Visible = False
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                dvMybooking.Visible = True
            Else
                dvMybooking.Visible = True
            End If
        End If
    End Sub


    Private Sub LoadFooter()
        Try

            Dim dtFooterAddress As DataTable
            Dim objBLLCommonFuntions As BLLCommonFuntions = New BLLCommonFuntions()
            dtFooterAddress = objBLLCommonFuntions.GetFooterAddress(Session("sAgentCompany"))

            If dtFooterAddress.Rows.Count > 0 Then
                lblFAdd1.Text = dtFooterAddress.Rows(0)("address1").ToString
                lblFAdd2.Text = dtFooterAddress.Rows(0)("address2").ToString
                lblFAdd3.Text = dtFooterAddress.Rows(0)("address3").ToString
                lblFAdd4.Text = dtFooterAddress.Rows(0)("address4").ToString

                lblFPhone.Text = dtFooterAddress.Rows(0)("phone").ToString
                lblFEmail.Text = dtFooterAddress.Rows(0)("email").ToString
                lblFWorkingTime.Text = dtFooterAddress.Rows(0)("workingtime").ToString

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("ChangePassword.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    


    
    Protected Sub btnMyAccount_Click(sender As Object, e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub

    Private Sub LoadSubMenus()
        Dim dtMenus As DataTable
        objResParam = Session("sobjResParam")
        dtMenus = objBLLMenu.Getmenus(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode, "2")
        If dtMenus.Rows.Count > 0 Then
            Dim dtMenusHeader As DataTable
            dtMenusHeader = dtMenus.DefaultView.ToTable(True, "parentname")
            If dtMenusHeader.Rows.Count > 0 Then
                dlSubMenuHeader.DataSource = dtMenusHeader
                dlSubMenuHeader.DataBind()


            End If
        End If
    End Sub
    Protected Sub dlSubMenuHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblMenuHeader As Label = CType(e.Item.FindControl("lblMenuHeader"), Label)
                Dim dlSubMenu As DataList = CType(e.Item.FindControl("dlSubMenu"), DataList)
                Dim dtMenus As DataTable
                objResParam = Session("sobjResParam")
                dtMenus = objBLLMenu.Getmenus(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode, "2")
                If dtMenus.Rows.Count > 0 Then
                    Dim dvResults As DataView = New DataView(dtMenus)
                    dvResults.RowFilter = "parentname='" & lblMenuHeader.Text.Trim & "'"
                    dlSubMenu.DataSource = dvResults.ToTable()
                    dlSubMenu.DataBind()
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: dlSubMenuHeader_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub dlSubMenu_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblMenuDesc As Label = CType(e.Item.FindControl("lblMenuDesc"), Label)
                Dim lblPageName As Label = CType(e.Item.FindControl("lblPageName"), Label)

                If lblPageName.Text = "" Then
                    lblMenuDesc.ForeColor = Drawing.Color.DimGray
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: dlSubMenuHeader_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangePassword.Click
        Try
            'Dim strUserName As String = ""
            'Dim strOldPassword As String = ""
            'Dim strValidateOldPassword As String = ""
            'Dim strNewPassword As String = ""
            'Dim strConfirmPassword As String = ""
            'strUserName = Session("GlobalUserName")
            'strOldPassword = txtOldPassword.Text
            'strNewPassword = txtNewPassword.Text
            'strConfirmPassword = txtNewConfirmPassword.Text
            'Dim objBLLLogin As New BLLLogin
            'strValidateOldPassword = objBLLLogin.ValidateOldPassword(strUserName, strOldPassword, Session("sLoginType"))
            'If strValidateOldPassword = 0 Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Old password is not correct.")
            '    Exit Sub
            'End If
            'Dim strStatus As String = ""
            'strStatus = objBLLLogin.ChangePassword(strUserName, strOldPassword, strNewPassword, Session("sLoginType"))
            'If strStatus = "0" Then
            '    MessageBox.ShowMessage(Page, MessageType.Success, "Password changed succesfully.")
            'End If
            Dim lsErrorMessage As String = "Failed to changed password, please try again."
            HttpContext.Current.Session("IDPChangePasswordErrorMhd") = ""
            If fnIDPAddUser(CType(txtUserName.Text, String), CType(txtOldPassword.Text, String), CType(txtNewPassword.Text, String)) = True Then
                MessageBox.ShowMessage(Page, MessageType.Success, "Password is changed succesfully.")
            Else
                If HttpContext.Current.Session("IDPChangePasswordErrorMhd") IsNot Nothing Then
                    lsErrorMessage = "Failed to changed password, please try again, Error - " & HttpContext.Current.Session("IDPChangePasswordErrorMhd").ToString.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace(":", "-").Replace("""", "").Replace("'", "")
                End If
                MessageBox.ShowMessage(Page, MessageType.Success, lsErrorMessage)
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("ChangePassword.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Public Class IDPLoginAddUser
        Public Property username As String
        Public Property old_password As String
        Public Property new_password As String
    End Class

    Function fnIDPAddUser(ByVal luserName As String, ByVal lOldPassword As String, ByVal lNewPassword As String) As Boolean
        Dim response As HttpWebResponse
        Try
            HttpContext.Current.Session("IDPChangePasswordErrorMhd") = ""
            Dim request = CType(WebRequest.Create(ConfigurationManager.AppSettings("IDP_passwordAPIURL").ToString()), HttpWebRequest)
            'Dim postData = "client_id=" & Uri.EscapeDataString(ConfigurationManager.AppSettings("IDP_Code").ToString())
            'postData += "&client_secret=" & Uri.EscapeDataString(ConfigurationManager.AppSettings("IDP_ClientSecret").ToString())
            'postData += "&code=" & Uri.EscapeDataString(gscode)
            'postData += "&redirect_uri=" & Uri.EscapeDataString(ConfigurationManager.AppSettings("IDP_RedirectURL").ToString())
            'postData += "&grant_type=" & Uri.EscapeDataString("authorization_code")
            'postData += "&scope=" & Uri.EscapeDataString("openid profile email")

            Dim user As IDPLoginAddUser = New IDPLoginAddUser()
            user.username = luserName
            user.old_password = lOldPassword
            user.new_password = lNewPassword

            Dim serializer = New JavaScriptSerializer()
            Dim postData = serializer.Serialize(user)

            Dim data = Encoding.ASCII.GetBytes(postData)

            request.Headers.Add("Authorization", "Bearer " + Session("accesstoken"))
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = data.Length

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using
            Try
                response = CType(request.GetResponse(), HttpWebResponse)
                Dim responseString = New StreamReader(response.GetResponseStream()).ReadToEnd()
                Dim responseFromServer As String = responseString.ToString()

                If response.StatusCode = HttpStatusCode.OK Or response.StatusCode = HttpStatusCode.Created Or response.StatusCode = HttpStatusCode.Accepted Or response.StatusCode = HttpStatusCode.NoContent Then
                    Return True
                Else
                    Return False
                End If

            Catch e As WebException
                Using response1 As WebResponse = e.Response
                    Dim httpResponse As HttpWebResponse = CType(response1, HttpWebResponse)
                    'Console.WriteLine("Error code: {0}", httpResponse.StatusCode)

                    Using data1 As Stream = response1.GetResponseStream()

                        Using reader = New StreamReader(data1)
                            Dim text As String = reader.ReadToEnd()
                            'Console.WriteLine(text)
                            HttpContext.Current.Session("IDPChangePasswordErrorMhd") = text
                        End Using
                    End Using
                End Using
                Return False
            End Try

        Catch ex As Exception
            'Response.Write(ex)
            Return False
        End Try
        Return False
    End Function

    
End Class

