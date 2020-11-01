Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services

Partial Class ContactUs
    Inherits System.Web.UI.Page
    Dim objBLLLogin As New BLLLogin
    Dim objBLLMenu As New BLLMenu
    Dim objBLLHome As New BLLHome
    Dim objclsUtilities As New clsUtilities
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim objResParam As New ReservationParameters
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                hdWhiteLabel.Value = objResParam.WhiteLabel
            End If
            LoadHome()
            LoadContactDetails()


        End If
        ' AutoCompleteExtender_txtHotelName.ContextKey = txtDestinationName.Text
    End Sub
  
    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
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
    '        objclsUtilities.WriteErrorLog("Home.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub

    ' Modified Param 17/10/2018

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
                Dim strLogo As String = objDataTable.Rows(0)("logofilename").ToString
                If strLogo <> "" Then
                    imgLogo.Src = "Logos/" & strLogo.Trim
                    Session("sLogo") = strLogo.Trim
                End If
            End If
        End If



    End Sub

  

    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
        If Session("sRequestId") Is Nothing Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "You have no booking.")
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                Response.Redirect("MoreServices.aspx")
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, "You have no booking.")
            End If
        End If
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

                lblCFAdd1.Text = dtFooterAddress.Rows(0)("address1").ToString
                lblCFAdd2.Text = dtFooterAddress.Rows(0)("address2").ToString
                lblCFAdd3.Text = dtFooterAddress.Rows(0)("address3").ToString
                lblCFAdd4.Text = dtFooterAddress.Rows(0)("address4").ToString

                lblCFPhone.Text = dtFooterAddress.Rows(0)("phone").ToString
                lblCFEmail.Text = dtFooterAddress.Rows(0)("email").ToString
                lblCFWorkingTime.Text = dtFooterAddress.Rows(0)("workingtime").ToString

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("ContactUs.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub LoadContactDetails()
        If Session("sLoginType") = "RO" Then
            dvDestinationSpecialist.Visible = False
        Else
            Dim objBLLContactUs As New BLLContactUs()
            Dim dt As DataTable
            dt = objBLLContactUs.GetContactDetails(Session("sAgentCode"))
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("UserImage").ToString = "" Then
                    imgPhoto.Visible = False
                Else
                    imgPhoto.ImageUrl = "ImageDisplay.aspx?FileName=" & dt.Rows(0)("UserImage").ToString & "&Type=6"
                End If

                lblDesignation.Text = dt.Rows(0)("userdesign").ToString
                lblPhone.Text = dt.Rows(0)("ustel").ToString
                lblEmail.Text = dt.Rows(0)("usemail").ToString
                dvDestinationSpecialist.Visible = True
            Else
                dvDestinationSpecialist.Visible = False
            End If
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
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Protected Sub btnMyAccount_Click(sender As Object, e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub
End Class
