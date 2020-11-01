Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Security.Cryptography

Partial Class SubUsers
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
    Dim objResParam As New ReservationParameters
    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
                If Not Session("sAbsoluteUrl") Is Nothing Then
                    Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
                    hdAbsoluteUrl.Value = strAbsoluteUrl
                End If

                LoadHome()

                dvWarning.Visible = False

            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("SubUsers.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
            End Try

        End If
        'If ViewState("vPopup") = "Y" Then
        '    mpSubUsers.Show()
        'End If
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
        BindWebAgents()

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
    '                    If dtMenus.Rows(i)("menudesc").ToString = "Home" Then
    '                        If Not Session("sEditRequestId") Is Nothing Then
    '                            Dim str As String = Session("sRequestId").ToString.Trim
    '                            strMenuHtml.Append("<li><div onclick=fnConfirmHome('" + str + "')><a href='#'>  " & dtMenus.Rows(i)("menudesc").ToString & " </a>" & "</div></li>")
    '                        Else
    '                            If Not Session("sRequestId") Is Nothing Then
    '                                Dim str As String = "New"
    '                                strMenuHtml.Append("<li><div onclick=fnConfirmHome('" + str + "')><a href='#'>  " & dtMenus.Rows(i)("menudesc").ToString & " </a>" & "</div></li>")
    '                            Else
    '                                strMenuHtml.Append("<li><div> <a href='" & dtMenus.Rows(i)("pagename").ToString & "'>" & dtMenus.Rows(i)("menudesc").ToString & "</a> </div></li>")
    '                            End If

    '                        End If

    '                    Else
    '                        strMenuHtml.Append("<li><a href='" & dtMenus.Rows(i)("pagename").ToString & "'>" & dtMenus.Rows(i)("menudesc").ToString & "</a></li>")

    '                    End If

    '                End If

    '            Next
    '            strMenuHtml.Append("</ul></nav>")
    '            strMenuMobHtml.Append("</ul></nav>")
    '        End If
    '        ltMenu.Text = strMenuHtml.ToString
    '        dvMobmenu.InnerHtml = strMenuMobHtml.ToString
    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("SubUsers.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub

    'Modified param 17//10/2018
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
         
     
        Else
            hdLoginType.Value = ""
        End If

    End Sub



    Private Sub BindSearchResults()


    End Sub



    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Try
    '        BindSearchResults()
    '        txtFocus.Focus()
    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("SubUsers.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub






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
            objclsUtilities.WriteErrorLog("SubUsers.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("SubUsers.aspx ::btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("SubUsers.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

  

    Protected Sub gvAgentsList_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgentsList.RowDataBound
        Try


            If (e.Row.RowType = DataControlRowType.DataRow) Then
                ' Dim lblCanEdit As Label = CType(e.Row.FindControl("lblCanEdit"), Label)
              

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("SubUsers.aspx :: gvSearchResults_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub gvAgentsList_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAgentsList.PageIndexChanging
        gvAgentsList.PageIndex = e.NewPageIndex
        BindWebAgents()

    End Sub

    'Protected Sub lbShowSubUsers_Click(sender As Object, e As System.EventArgs)

    '    Try
    '        Dim lbShow As LinkButton = CType(sender, LinkButton)
    '        Dim gvRow As GridViewRow = CType(lbShow.NamingContainer, GridViewRow)
    '        Dim lblAgentCode As Label = CType(gvRow.FindControl("lblAgentCode"), Label)
    '        BindSubUsers(lblAgentCode.Text)

    '    Catch ex As Exception
    '        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
    '        objclsUtilities.WriteErrorLog("SubUsers.aspx :: lbShowSubUsers_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub

    Protected Sub btnSFilter_Click(sender As Object, e As System.EventArgs) Handles btnSFilter.Click
        Try
            BindWebAgents()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("SubUsers.aspx :: lbEdit_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub BindWebAgents()
        If Session("sLoginType") = "RO" Then

            Dim strAgentCode As String = txtSAgentCode.Text
            Dim strAgentName As String = txtSAgentNameCode.Text
            Dim strShortName As String = txtSShortName.Text
            Dim strCountryCode As String = txtSCountryCode.Text
            Dim dt As DataTable
            Dim objBLLSubUsers As New BLLSubUsers
            dt = objBLLSubUsers.GetWebAgentList(strAgentCode, strAgentName, strShortName, strCountryCode)
            If dt.Rows.Count > 0 Then
                gvAgentsList.DataSource = dt
                gvAgentsList.DataBind()
            Else
                gvAgentsList.DataBind()
            End If
            dvSubUser.Visible = False
            dvAgentUser.Visible = True
        Else
            BindSubUsers(Session("sAgentCode"))
            dvSubUser.Visible = True
            dvAgentUser.Visible = False
        End If
    End Sub

    Private Sub BindSubUsers(strAgentCode As String)
        If Session("sLoginType") <> "RO" Then
            Dim objDataTable As DataTable
            objDataTable = objBLLLogin.LoadLoginPageFields(Session("sAgentCompany"), Session("sLoginType"), Session("sAgentCode"), Session("GlobalUserName"))
            If Not objDataTable Is Nothing Then
                If objDataTable.Rows.Count > 0 Then
                    txtAgentName.Text = objDataTable.Rows(0)("agentname").ToString
                    lblPopupAgentCode.Text = strAgentCode
                End If
            End If
        End If

        Dim dt As DataTable
        Dim objBLLSubUsers As New BLLSubUsers
        dt = objBLLSubUsers.GetSubUsersList(strAgentCode)
        If dt.Rows.Count > 0 Then
            gvSubUsers.DataSource = dt
            gvSubUsers.DataBind()
        Else
            gvSubUsers.DataBind()
        End If
        dvSubUser.Visible = True
    End Sub

    Protected Sub lbShowSubUsers_Click(sender As Object, e As System.EventArgs)
        Try
            Dim lbShow As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbShow.NamingContainer, GridViewRow)
            Dim lblAgentCode As Label = CType(gvRow.FindControl("lblAgentCode"), Label)
            Dim lblAgentName As Label = CType(gvRow.FindControl("lblAgentName"), Label)
            txtAgentName.Text = lblAgentName.Text
            lblPopupAgentCode.Text = lblAgentCode.Text
            BindSubUsers(lblAgentCode.Text)
            txtFocus.Focus()

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("SubUsers.aspx :: lbShowSubUsers_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnAddSubUsers_Click(sender As Object, e As System.EventArgs) Handles btnAddSubUsers.Click
        lblPopupTittle.Text = "Add Sub User"
        EnableControls()
        ClearPopupControls()
        hdOPMode.Value = "N"
        dvEmailConfirm.Visible = False
        btnSubUserSave.Text = "Save"
        mpSubUsers.Show()
    End Sub

    Protected Sub lbView_Click(sender As Object, e As System.EventArgs)
        ClearPopupControls()
        Dim lbView As LinkButton = CType(sender, LinkButton)
        Dim gvRow As GridViewRow = CType(lbView.NamingContainer, GridViewRow)
        Dim lblAgentName As Label = CType(gvRow.FindControl("lblAgentName"), Label)
        Dim lblSubUserAgentName As Label = CType(gvRow.FindControl("lblSubUserAgentName"), Label)
        Dim lblSubUserCode As Label = CType(gvRow.FindControl("lblSubUserCode"), Label)
        Dim lblSubUserAddress As Label = CType(gvRow.FindControl("lblSubUserAddress"), Label)
        Dim lblSubUserTel As Label = CType(gvRow.FindControl("lblSubUserTel"), Label)
        Dim lblSubUserFax As Label = CType(gvRow.FindControl("lblSubUserActive"), Label)
        Dim lblSubUserActive As Label = CType(gvRow.FindControl("lblSubUserActive"), Label)
        Dim lblSubUserPassword As Label = CType(gvRow.FindControl("lblSubUserPassword"), Label)
        Dim lblgSubId As Label = CType(gvRow.FindControl("lblSubId"), Label)
        Dim lblEmailConfirm As Label = CType(gvRow.FindControl("lblEmailConfirm"), Label)



        lblPopupSubId.Text = lblgSubId.Text
        txtpassword.Text = lblSubUserPassword.Text
        txtConfirmPassword.Text = lblSubUserPassword.Text
        txtSubUserEmailId.Text = lblSubUserCode.Text
        txtSubUserName.Text = lblSubUserAgentName.Text
        txtAddress.Text = lblSubUserAddress.Text
        txtTelephone.Text = lblSubUserTel.Text
        txtFax.Text = lblSubUserFax.Text
        chkActive.Checked = IIf(lblSubUserActive.Text = "1", True, False)
        chkEmailConfirm.Checked = IIf(lblEmailConfirm.Text = "Yes", True, False)

        hdOPMode.Value = "V"
        dvEmailConfirm.Visible = True

        lblPopupTittle.Text = "View Sub User"
        DisableControls()
        mpSubUsers.Show()
    End Sub

    Protected Sub lbEdit_Click(sender As Object, e As System.EventArgs)
        ClearPopupControls()
        EnableControls()
        Dim lbEdit As LinkButton = CType(sender, LinkButton)
        Dim gvRow As GridViewRow = CType(lbEdit.NamingContainer, GridViewRow)
        Dim lblAgentName As Label = CType(gvRow.FindControl("lblAgentName"), Label)
        Dim lblSubUserAgentName As Label = CType(gvRow.FindControl("lblSubUserAgentName"), Label)
        Dim lblSubUserCode As Label = CType(gvRow.FindControl("lblSubUserCode"), Label)
        Dim lblSubUserAddress As Label = CType(gvRow.FindControl("lblSubUserAddress"), Label)
        Dim lblSubUserTel As Label = CType(gvRow.FindControl("lblSubUserTel"), Label)
        Dim lblSubUserFax As Label = CType(gvRow.FindControl("lblSubUserActive"), Label)
        Dim lblSubUserActive As Label = CType(gvRow.FindControl("lblSubUserActive"), Label)
        Dim lblSubUserPassword As Label = CType(gvRow.FindControl("lblSubUserPassword"), Label)
        Dim lblgSubId As Label = CType(gvRow.FindControl("lblSubId"), Label)
        Dim lblEmailConfirm As Label = CType(gvRow.FindControl("lblEmailConfirm"), Label)
        lblPopupSubId.Text = lblgSubId.Text
        txtpassword.Text = lblSubUserPassword.Text
        txtConfirmPassword.Text = lblSubUserPassword.Text
        txtSubUserEmailId.Text = lblSubUserCode.Text
        txtSubUserName.Text = lblSubUserAgentName.Text
        txtAddress.Text = lblSubUserAddress.Text
        txtTelephone.Text = lblSubUserTel.Text
        txtFax.Text = lblSubUserFax.Text
        chkActive.Checked = IIf(lblSubUserActive.Text = "1", True, False)
        chkEmailConfirm.Checked = IIf(lblEmailConfirm.Text = "Yes", True, False)
        hdOPMode.Value = "E"
        dvEmailConfirm.Visible = True
        lblPopupTittle.Text = "Edit Sub User"
        btnSubUserSave.Text = "Update"
        mpSubUsers.Show()
    End Sub

    Protected Sub lbDelete_Click(sender As Object, e As System.EventArgs)
        hdOPMode.Value = "D"
        'ClearPopupControls()
        'Dim lbDelete As LinkButton = CType(sender, LinkButton)
        'Dim gvRow As GridViewRow = CType(lbDelete.NamingContainer, GridViewRow)
        'Dim lblAgentName As Label = CType(gvRow.FindControl("lblAgentName"), Label)
        'Dim lblSubUserAgentName As Label = CType(gvRow.FindControl("lblSubUserAgentName"), Label)
        'Dim lblSubUserEmail As Label = CType(gvRow.FindControl("lblSubUserEmail"), Label)
        'txtSubUserEmailId.Text = lblSubUserEmail.Text
        '' txtAgentName.Text = lblAgentName.Text
        'txtSubUserName.Text = lblSubUserAgentName.Text
        'lblPopupTittle.Text = "Delete Sub User"
        'mpSubUsers.Show()
    End Sub
    Protected Sub lbROAssignRights_Click(sender As Object, e As System.EventArgs)
        Dim lbROAssignRights As LinkButton = CType(sender, LinkButton)
        Dim gvRow As GridViewRow = CType(lbROAssignRights.NamingContainer, GridViewRow)
        Dim lblAgentName As Label = CType(gvRow.FindControl("lblAgentName"), Label) 
        Dim lblAgentCode As Label = CType(gvRow.FindControl("lblAgentCode"), Label)
        lblARAgentName.Text = "<span style='color:#ff7200;'>Agent:</span>  " & lblAgentName.Text
        lblARAgentCode.Text = lblAgentCode.Text
        hdPopupUserType.Value = "Agent"
        lblARAgentSubUserCode.Text = ""
        lbluserRirgtheading.Text = "Assign Agent Rights"
        BindMenuList("Agent", lblAgentCode.Text, "")
        mpUserRights.Show()
    End Sub
    Protected Sub lbAssignRights_Click(sender As Object, e As System.EventArgs)
        Dim lbAssignRights As LinkButton = CType(sender, LinkButton)
        Dim gvRow As GridViewRow = CType(lbAssignRights.NamingContainer, GridViewRow)
        Dim lblAgentName As Label = CType(gvRow.FindControl("lblAgentName"), Label)
        Dim lblSubUserAgentName As Label = CType(gvRow.FindControl("lblSubUserAgentName"), Label)
        Dim lblSubUserCode As Label = CType(gvRow.FindControl("lblSubUserCode"), Label)
        Dim lblAgentCode As Label = CType(gvRow.FindControl("lblAgentcode_"), Label)
        lblARAgentName.Text = "<span style='color:#ff7200;'>Agent:</span> " & lblAgentName.Text
        lblARAgentSubUserCode.Text = lblSubUserCode.Text
        lblARAgentSubUser.Text = "<span style='color:#ff7200;'>Sub User:</span> " & lblSubUserCode.Text
        lblARAgentCode.Text = lblAgentCode.Text
        hdPopupUserType.Value = "SubUser"
        lbluserRirgtheading.Text = "Assign Sub User Rights"
        BindMenuList("SubUser", lblARAgentCode.Text, lblARAgentSubUserCode.Text)
        mpUserRights.Show()
    End Sub


    Private Sub ClearPopupControls()
        txtSubUserEmailId.Text = ""
        txtSubUserName.Text = ""
        txtpassword.Text = ""
        txtConfirmPassword.Text = ""
        txtAddress.Text = ""
        txtTelephone.Text = ""
        txtFax.Text = ""
        lblPopupSubId.Text = ""
        chkActive.Checked = False
    End Sub
    Protected Sub btnMyAccount_Click(sender As Object, e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
      <System.Web.Services.WebMethod()> _
    Public Shared Function GetCustomers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'Royal Park
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode=1 and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode=2 and agentname like  '" & prefixText & "%'  order by agentname  "
                End If
            End If

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("agentname").ToString(), myDS.Tables(0).Rows(i)("agentcode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If Not HttpContext.Current.Session("sLoginType") Is Nothing Then
                If HttpContext.Current.Session("sLoginType") = "RO" Then
                    If contextKey <> "" Then
                        strSqlQry = "select distinct a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & contextKey.Trim & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    Else
                        ' strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                        strSqlQry = "select distinct a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and ctryname like  '" & prefixText & "%'  order by ctryname"
                    End If
                Else
                    strSqlQry = "select distinct a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and ctryname like  '" & prefixText & "%'  order by ctryname"

                End If


            Else
                strSqlQry = "select distinct a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and ctryname like  '" & prefixText & "%'  order by ctryname"


            End If

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("ctryname").ToString(), myDS.Tables(0).Rows(i)("ctrycode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    Protected Sub btnSubUserSave_Click(sender As Object, e As System.EventArgs) Handles btnSubUserSave.Click
        If txtAgentName.Text = "" Or txtSubUserName.Text = "" Or txtSubUserEmailId.Text = "" Or txtpassword.Text = "" Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter sub user details")
            Exit Sub
        End If
        Dim objBLLSubUsers As New BLLSubUsers
        If hdOPMode.Value = "N" Then

            If objBLLSubUsers.IsExistSubUser(txtSubUserEmailId.Text, "") Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "The entered email id is already registered. Please enter new id.")
                hdPopup.Value = "Y"
                ViewState("vPopup") = "Y"
                Exit Sub

            End If
        ElseIf hdOPMode.Value = "E" Then
            If objBLLSubUsers.IsExistSubUser(txtSubUserEmailId.Text, lblPopupSubId.Text) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "The entered email id is already registered. Please enter new id.")
                ViewState("vPopup") = "Y"
                Exit Sub

            End If
        End If
        Dim strAgentName As String = txtAgentName.Text
        objBLLSubUsers.AgentCode = lblPopupAgentCode.Text
        objBLLSubUsers.AgentSubCode = txtSubUserEmailId.Text
        objBLLSubUsers.SubUserName = txtSubUserName.Text
        objBLLSubUsers.Password = txtpassword.Text
        objBLLSubUsers.Telephone = txtTelephone.Text
        objBLLSubUsers.Address = txtAddress.Text
        objBLLSubUsers.Active = IIf(chkActive.Checked = True, "1", "0")
        objBLLSubUsers.Fax = txtFax.Text
        objBLLSubUsers.LoggedUser = Session("GlobalUserName")
        If hdOPMode.Value = "E" Then
            objBLLSubUsers.OPMode = "2"
            objBLLSubUsers.EmailConfirm = IIf(chkEmailConfirm.Checked = True, "1", "0")
            Dim strSave As Boolean = objBLLSubUsers.SaveSubUserDetails()
            If strSave = True Then
                MessageBox.ShowMessage(Page, MessageType.Success, "Updated Succesfully.")
                BindSubUsers(lblPopupAgentCode.Text)
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Faild to update.")
            End If
           
        ElseIf hdOPMode.Value = "N" Then
            objBLLSubUsers.OPMode = "1"

            Dim strSave As Boolean = objBLLSubUsers.SaveSubUserDetails()
            If strSave = True Then
                'Commented for demo --20200711
                ' If SendEmail(objBLLSubUsers.SubUserName, objBLLSubUsers.AgentSubCode, objBLLSubUsers.AgentCode, strAgentName) Then
                MessageBox.ShowMessage(Page, MessageType.Success, "Registerd Succesfully. A varification link has been sent to email(" & objBLLSubUsers.AgentSubCode & ") account.")
                BindSubUsers(lblPopupAgentCode.Text)
                'Else
                '    MessageBox.ShowMessage(Page, MessageType.Success, "Registerd Succesfully. We'll send a varification link email to " & objBLLSubUsers.AgentSubCode & ".")
                '    BindSubUsers(lblPopupAgentCode.Text)
                'End If

            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Registration failed.")
            End If

        End If


    End Sub

    Private Sub DisableControls()
        txtSubUserEmailId.Enabled = False
        txtSubUserName.Enabled = False
        txtpassword.Enabled = False
        txtConfirmPassword.Enabled = False
        txtAddress.Enabled = False
        txtTelephone.Enabled = False
        txtFax.Enabled = False
        chkActive.Enabled = False
        btnSubUserSave.Visible = False
        btnReset.Visible = False
        chkEmailConfirm.Enabled = False
    End Sub
    Private Sub EnableControls()
        txtSubUserEmailId.Enabled = True
        txtSubUserName.Enabled = True
        txtpassword.Enabled = True
        txtConfirmPassword.Enabled = True
        txtAddress.Enabled = True
        txtTelephone.Enabled = True
        txtFax.Enabled = True
        chkActive.Enabled = True
        btnSubUserSave.Visible = True
        btnReset.Visible = True
        chkEmailConfirm.Enabled = True
    End Sub


    Private Sub BindMenuList(strUserType As String, strAgentCode As String, strSubUserCode As String)
        Dim dtMainMenu As DataTable
        Dim objBLLSubUsers As New BLLSubUsers

        dtMainMenu = objBLLSubUsers.GetMenuList("1", Session("sLoginType")) 'Main Menus
        If dtMainMenu.Rows.Count > 0 Then
            gvMainMenu.DataSource = dtMainMenu
            gvMainMenu.DataBind()


        End If

        Dim dtSubMenu As DataTable
        dtSubMenu = objBLLSubUsers.GetMenuList("2", Session("sLoginType")) ' Sub Menus
        If dtSubMenu.Rows.Count > 0 Then
            gvSubMenu.DataSource = dtSubMenu
            gvSubMenu.DataBind()
        End If
        FillAssignedRights(strUserType, strAgentCode, strSubUserCode)
    End Sub

    Private Sub FillAssignedRights(strUserType As String, strAgentCode As String, strSubUserCode As String)
        Dim dt As DataTable
        Dim objBLLSubUsers As New BLLSubUsers
        dt = objBLLSubUsers.GetActiveMenuList(strUserType, strAgentCode, strSubUserCode)
        If dt.Rows.Count > 0 Then
            For Each row As GridViewRow In gvMainMenu.Rows
                Dim lblmenuId As Label = CType(row.FindControl("lblmenuId"), Label)
                Dim chkActive As CheckBox = CType(row.FindControl("chkActive"), CheckBox)
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim strMenuId As String = dt.Rows(i)("menuid").ToString
                    If strMenuId.Trim = lblmenuId.Text.Trim Then
                        chkActive.Checked = True
                    End If
                Next

            Next
            For Each row As GridViewRow In gvSubMenu.Rows
                Dim lblmenuId As Label = CType(row.FindControl("lblmenuId"), Label)
                Dim chkActive As CheckBox = CType(row.FindControl("chkActive"), CheckBox)
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim strMenuId As String = dt.Rows(i)("menuid").ToString
                    If strMenuId.Trim = lblmenuId.Text.Trim Then
                        chkActive.Checked = True
                    End If
                Next
            Next
          

        End If
    End Sub

    Protected Sub btnUserRightSave_Click(sender As Object, e As System.EventArgs) Handles btnUserRightSave.Click

        Dim strUserRights As New StringBuilder
        strUserRights.Append("<DocumentElement>")
        If gvMainMenu.Rows.Count > 0 Then

            For Each row As GridViewRow In gvMainMenu.Rows
                Dim lblmenuId As Label = CType(row.FindControl("lblmenuId"), Label)
                Dim chkActive As CheckBox = CType(row.FindControl("chkActive"), CheckBox)
                If chkActive.Checked = True Then
                    strUserRights.Append("<Table>")
                    strUserRights.Append("<menuid>" & lblmenuId.Text & "</menuid>")
                    strUserRights.Append("<active>1</active>")
                    strUserRights.Append("</Table>")
                End If
            Next

        
        End If

        If gvSubMenu.Rows.Count > 0 Then
            For Each row As GridViewRow In gvSubMenu.Rows
                Dim lblmenuId As Label = CType(row.FindControl("lblmenuId"), Label)
                Dim chkActive As CheckBox = CType(row.FindControl("chkActive"), CheckBox)
                If chkActive.Checked = True Then
                    strUserRights.Append("<Table>")
                    strUserRights.Append("<menuid>" & lblmenuId.Text & "</menuid>")
                    strUserRights.Append("<active>1</active>")
                    strUserRights.Append("</Table>")
                End If
            Next
        End If
        strUserRights.Append("</DocumentElement>")
        Dim objBLLSubUsers As New BLLSubUsers
        Dim iStatus As Boolean = objBLLSubUsers.SaveUserRights(hdPopupUserType.Value, lblARAgentCode.Text, lblARAgentSubUserCode.Text, strUserRights.ToString, Session("GlobalUserName"))
        If iStatus = True Then
            MessageBox.ShowMessage(Page, MessageType.Success, "User Right Assigned Successfully.")
        Else
            MessageBox.ShowMessage(Page, MessageType.Errors, "Faild to Save.")
        End If
    End Sub

    Private Function SendEmail(strSubUserName As String, strSubUserCode As String, strAgentCode As String, strAgentName As String) As Boolean
        Dim strBody As String = ""
        Dim strLogo As String = ""
        Dim strAttachmnet As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")      
        End If
        Dim strEmailConfirmLink As String = ""

        strEmailConfirmLink = ConfigurationManager.AppSettings("EmailConfirmLink").ToString() & "?code=" & objclsUtilities.Encrypt(strSubUserCode)

        strAttachmnet = Server.MapPath("Logos/" & strLogo)
        strBody = "<html><table  style='width:100%;border-width:1px;border-color:black;border-style:solid;'><tr><td><table  style='width:100%'><tr><td style='width:100%;background-color:Black;height:30px;'></td></tr> "
        strBody += "<tr><td style='width:100%;background-color:White;height:50px;'><a href='#'><img id='imgLogo'  alt='' src='cid:" & strLogo & "' /></a></td></tr>"
        strBody += "<tr><td style='width:750px;background:#E6E6E6;padding-left:50px;font-family:Raleway;text-decoration:none;text-align: justify;'>"
        strBody += "<br/>Dear " & strSubUserName.Trim & " <br/>"
        strBody += "<br/>Greeting from " & strAgentName & "<br/>"
        strBody += "<br/>Please confirm that you want to use this as your account email address. Once it's done you will be able to access booking and other service.<br/>"
        strBody += "<br/><br/>  <a href='" & strEmailConfirmLink & "'>Verify My Email</a> <br/>"
        strBody += "<br/><br/>  Thank you & best Regards <br/> System Administrator <br/> " & strAgentName & ""
        strBody += "<br/><br/></td></tr>"
        strBody += "<tr><td style='width:100%;background-color:Black;height:20px;'></td></tr></table></td></tr></table></html>"
        Dim strFromId As String = strSubUserCode
        Dim objEmail As New clsEmail

     
        Dim strFromEmailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
        Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
        Dim TestEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")
        If Emailmode = "Test" Then
            strSubUserCode = TestEmail
        End If



        If objEmail.SendEmailOnline(strFromEmailid, strSubUserCode, "", "Verify Your Email Address", strBody, strAttachmnet) Then
            objclsUtilities.SendEmailNotification("REGISTER", strFromEmailid, strSubUserCode, "", "", "Verify Your Email Address", strBody, "1", "1", strAttachmnet, "Y", "", "SUB USER REGISTERATION")
            Return True
        Else
            objclsUtilities.SendEmailNotification("REGISTER", strFromEmailid, strSubUserCode, "", "", "Verify Your Email Address", strBody, "1", "1", strAttachmnet, "N", "", "SUB USER REGISTERATION")
            Return False
        End If
    End Function

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
    Protected Sub btnConfirmHome_Click(sender As Object, e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
    End Sub
    Sub clearallBookingSessions()

        Session("sRequestId") = Nothing
        Session("sEditRequestId") = Nothing
        Session("sdtPriceBreakup") = Nothing
        Session("showservices") = Nothing
        Session("ShowGuests") = Nothing
        Session("ShowGuestsDep") = Nothing
        Session("sobjBLLHotelSearchActive") = Nothing
        Session("sobjBLLOtherSearchActive") = Nothing
        Session("sobjBLLMASearchActive") = Nothing
        Session("sobjBLLTourSearchActive") = Nothing
        Session("sobjBLLTransferSearchActive") = Nothing
        Session("sdsSpecialEvents") = Nothing
        Session("sdtSelectedSpclEvent") = Nothing
        Session("slbSpecialEvents") = Nothing
        Session("State") = Nothing
        Session("sdsPriceBreakup") = Nothing
        Session("sdsPriceBreakupTemp") = Nothing
        Session("sDSSearchResults") = Nothing
        Session("sMailBoxPageIndex") = Nothing
        Session("sdtRoomType") = Nothing

        Session("sobjBLLTourSearch") = Nothing
        Session("sobjBLLTourSearchActive") = Nothing
        Session("sDSTourSearchResults") = Nothing
        Session("sTourPageIndex") = Nothing
        Session("sdtTourPriceBreakup") = Nothing
        Session("slbTourTotalSaleValue") = Nothing
        Session("selectedtourdatatable") = Nothing
        Session("sTourLineNo") = Nothing

        Session("sobjBLLTransferSearch") = Nothing
        Session("sobjBLLTransferSearchActive") = Nothing
        Session("sDSTrfSearchResults") = Nothing
        Session("sTrfMailBoxPageIndex") = Nothing
        Session("tlineno") = Nothing
        Session("sSender") = Nothing
        Session("sEventArgs") = Nothing

        Session("sobjBLLMASearch") = Nothing
        Session("sobjBLLMASearchActive") = Nothing
        Session("sDSMASearchResults") = Nothing
        Session("sMAMailBoxPageIndex") = Nothing
        Session("sMALineNo") = Nothing
        Session("sMAMailBoxPageIndex") = Nothing
        Session("slbMATotalSaleValue") = Nothing

        Session("sdtRecentlyViewedHotel") = Nothing


        Session("ShowGuests") = Nothing
        Session("ShowGuestsDep") = Nothing
        Session("showservices") = Nothing

        Session("sdtPriceBreakup") = Nothing
        Session("sobjBLLOtherSearchActive") = Nothing
        Session("sobjBLLOtherSearch") = Nothing
        Session("sDSOtherSearchResults") = Nothing
        Session("sOtherPageIndex") = Nothing
        Session("slbOtherTotalSaleValue") = Nothing
        Session("selectedotherdatatable") = Nothing
        Session("olineno") = Nothing
        Session("sdsPackageSummary") = Nothing
        Session("VisaDetailsDT") = Nothing
        Session("vlineno") = Nothing
    End Sub
End Class

