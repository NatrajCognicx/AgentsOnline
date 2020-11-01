Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Security.Cryptography

Partial Class MyAccount
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

    Dim OpCopy As String = ""
    Dim OpEmail As String = ""
    Dim QuoteDownload As String = ""

    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton
    Protected Sub imgbtnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            If lblRequestId.Text <> "" Then
                Dim strpop As String
                Dim ResParam As New ReservationParameters
                ResParam = Session("sobjResParam")
                Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + lblRequestId.Text.Trim + "'")
                If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                If chkCumulative.Trim() = "CUMULATIVE" And ResParam.LoginType = "RO" Then
                    strpop = "window.open('PrintPage.aspx?printId=QuoteCostingExcel&quoteId=" & lblRequestId.Text.Trim & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupQCosting", strpop, True)
                End If
                '   strpop = "window.open('PrintPage.aspx?RequestId=" & lblRequestId.Text.Trim & "&printId=bookingConfirmation');"
                strpop = "window.open('PrintPage.aspx?printID=bookingQuote&quoteId=" & lblRequestId.Text.Trim & "');"
                Session("QuoteDownload") = "YES"
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup", strpop, True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: imgbtnDownload_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    '*** Danny 25/09/2018
    Protected Sub gvSearchResultsQuotes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearchResultsQuotes.RowDataBound
        Dim str As String = ""
        Try
            'If (e.Row.RowType = DataControlRowType.Header) Then
            '    If Session("sLoginType") <> "RO" Then
            '        e.Row.Cells(23).Visible = False

            '    End If



            'End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim lImgBtnDownload As LinkButton = CType(e.Row.FindControl("ImgBtnDownload"), LinkButton) '*** Danny 12/09/2018


                If (QuoteDownload = "Y" And Session("sLoginType") = "RO") Then
                    lImgBtnDownload.Visible = True
                End If

            End If



            'If (e.Row.RowType = DataControlRowType.DataRow) Then
            '    Dim lblCanEdit As Label = CType(e.Row.FindControl("lblCanEdit"), Label)
            '    Dim lbEdit As LinkButton = CType(e.Row.FindControl("lbEdit"), LinkButton)

            '    If Session("sLoginType") <> "RO" Then
            '        e.Row.Cells(23).Visible = False
            '    Else
            '        If lblCanEdit.Text = "0" Then
            '            lbEdit.Visible = False
            '        Else
            '            lbEdit.Visible = True
            '        End If
            '    End If

            'End If
            If hdPrintVoucher.Value <> "1" Then
                e.Row.Cells(24).Visible = False
            End If

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: gvSearchResults_RowDataBound :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        QuoteDownload = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=68")
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
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
                OpCopy = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=64") '*** Danny 19/09/2018
                OpEmail = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=65")


                LoadHome()
                '*** Danny 19/09/2018 Hiding Grid Columns
                Dim strChkGridCaption As String = "CHA" '*** Eg:- C="Copy" Column in Grid. In followin loop is the column not found after loop this string will have "C" means the column name been changed.
                For Each GrCol In gvSearchResults.Columns
                    If GrCol.ToString.Trim() = "Copy" Then
                        strChkGridCaption = strChkGridCaption.Replace("C", "") '*** C=Copy Column
                        If IIf(OpCopy Is Nothing, "", OpCopy) <> "Y" Then
                            GrCol.Visible = False
                        End If
                    ElseIf GrCol.ToString.Trim() = "Hotel Email" Then
                        strChkGridCaption = strChkGridCaption.Replace("H", "") '*** H=Hotel Email Column
                        If IIf(OpEmail Is Nothing, "", OpEmail) <> "Y" Then
                            GrCol.Visible = False
                        End If
                    ElseIf GrCol.ToString.Trim() = "Agent Email" Then
                        strChkGridCaption = strChkGridCaption.Replace("A", "") '*** A=Agent Email Column
                        If IIf(OpEmail Is Nothing, "", OpEmail) <> "Y" Then
                            GrCol.Visible = False
                        End If
                    End If

                Next
                If strChkGridCaption.Trim.Length > 0 Then
                    MessageBox.ShowMessage(Page, MessageType.Alert, "Missing Column(s) in Booking Grid.")
                End If



                dvReadMore.Visible = False
                dvWarning.Visible = False
                AutoCompleteExtender_txtHotelName.ContextKey = ""
            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("MyAccount.aspx :: Page_Load :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
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
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS) '*** Danny 06/09/2018
        lnkCSS.Attributes("href") = Session("strTheme").ToString
        LoadFields()
        ShowMyBooking()
        FillRPorRGReference()
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If
        If hdFindBooking.Value <> "1" Then
            btnSearch.Visible = False
        End If
        BindPreviousSearch()
    End Sub

    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo '*** Danny 10/07/2018

            End If

        End If
    End Sub

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
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
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
                If lblPhoneNo.Text.Trim.Length = 0 Then
                    lblPhoneNo.Visible = False
                    dvlblHeaderAgentName.Style.Add("display", "none")
                End If
                Page.Title = objDataTable.Rows(0)("companyname").ToString 'companyname
                lblHeaderAgentName.Text = objDataTable.Rows(0)("agentname").ToString
            End If
        End If


        If Not Session("sLoginType") Is Nothing Then
            hdLoginType.Value = Session("sLoginType")
            If Session("sLoginType") <> "RO" Then
                dvForRO.Visible = False
                dvForAgent.Visible = False
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
                        txtCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtCountry.ReadOnly = True
                        AutoCompleteExtender_txtCountry.Enabled = False
                    Else
                        txtCountry.ReadOnly = False
                        AutoCompleteExtender_txtCountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else
                dvForRO.Visible = True
                dvForAgent.Visible = True
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub



    Private Sub BindSearchResults()
        Dim objBLLMyAccount As New BLLMyAccount
        objBLLMyAccount.LoginType = Session("sLoginType")
        objBLLMyAccount.WebUserName = Session("GlobalUserName")
        If Session("sLoginType") = "RO" Then
            objBLLMyAccount.AgentCode = txtCustomerCode.Text.Trim
        Else
            objBLLMyAccount.AgentCode = Session("sAgentCode")
        End If

        objBLLMyAccount.RequestId = txtBookingRef.Text.Trim
        objBLLMyAccount.ServiceType = ddlServiceType.SelectedValue
        Dim DestinationCodeAndType As String = txtDestinationCode.Text
        Dim strDest As String() = txtDestinationCode.Text.Split("|")
        If strDest.Length = 2 Then
            objBLLMyAccount.DestinationCode = strDest(0)
            objBLLMyAccount.DestinationType = strDest(1)
        End If
        objBLLMyAccount.AgentRef = txtAgentRef.Text
        objBLLMyAccount.GuestFirstName = txtGuestFirstName.Text
        objBLLMyAccount.GuestLastName = txtGuestSecondName.Text
        objBLLMyAccount.TravelDateType = ddlTravelDate.SelectedValue
        objBLLMyAccount.TravelDateFrom = txtTravelFromDate.Text
        objBLLMyAccount.TravelDateTo = txtTravelToDate.Text
        objBLLMyAccount.BookingDateType = ddlBookingDate.SelectedValue
        objBLLMyAccount.BookingDateFrom = txtBookingFromDate.Text
        objBLLMyAccount.BookingDateTo = txtBookingToDate.Text
        objBLLMyAccount.BookingStatus = ddlBookingStatus.SelectedValue
        objBLLMyAccount.PartyCode = txtHotelCode.Text
        objBLLMyAccount.HotelConfNo = txtHotelConfNo.Text
        objBLLMyAccount.SearchAgentCode = txtCustomerCode.Text
        objBLLMyAccount.SourceCountrycode = txtCountryCode.Text
        objBLLMyAccount.UserCode = txtROCode.Text
        objBLLMyAccount.CompanyCode = Session("sAgentCompany")
        If Not Session("sobjResParam") Is Nothing Then
            objResParam = Session("sobjResParam")
            objBLLMyAccount.SubUserCode = objResParam.SubUserCode
        Else
            objBLLMyAccount.SubUserCode = ""
        End If
        objBLLMyAccount.Tab = hdTab.Value

        Session("sobjBLLMyAccount") = objBLLMyAccount
        If hdTab.Value = "1" Then
            objBLLMyAccount.quoteType = ""
            Dim dtQuoteResult As DataTable
            dtQuoteResult = objBLLMyAccount.GetQuoteSearchDetails()
            If dtQuoteResult.Rows.Count > 0 Then
                Session("sQuoteDetails") = dtQuoteResult
                gvSearchResultsQuotes.DataSource = dtQuoteResult
                gvSearchResultsQuotes.DataBind()
                dvWarning.Visible = False
            Else
                Session("sQuoteDetails") = Nothing
                gvSearchResultsQuotes.PageIndex = 0
                gvSearchResultsQuotes.DataBind()
                dvWarning.Visible = True
            End If
        ElseIf hdTab.Value = "2" Then
            objBLLMyAccount.quoteType = "PAYMENT"
            Dim dtQuoteResult As DataTable
            dtQuoteResult = objBLLMyAccount.GetQuoteSearchDetails()
            If dtQuoteResult.Rows.Count > 0 Then
                Session("sQuotePaymentDetails") = dtQuoteResult
                gvSearchResultsPayments.DataSource = dtQuoteResult
                gvSearchResultsPayments.DataBind()
                dvWarning.Visible = False
            Else
                Session("sQuotePaymentDetails") = Nothing
                gvSearchResultsPayments.PageIndex = 0
                gvSearchResultsPayments.DataBind()
                dvWarning.Visible = True
            End If
        Else
            Dim dtResult As DataTable
            dtResult = objBLLMyAccount.GetBookingSearchDetails()
            If dtResult.Rows.Count > 0 Then
                Session("sBookDetails") = dtResult
                gvSearchResults.DataSource = dtResult
                gvSearchResults.DataBind()
                dvWarning.Visible = False
            Else
                Session("sBookDetails") = Nothing
                gvSearchResults.PageIndex = 0
                gvSearchResults.DataBind()
                dvWarning.Visible = True
            End If
        End If
    End Sub

    Private Sub BindSearchResults_ForPaging()
        If hdTab.Value = "1" Then
            If Not Session("sQuoteDetails") Is Nothing Then
                Dim dtQuoteResult As DataTable
                dtQuoteResult = Session("sQuoteDetails")
                If dtQuoteResult.Rows.Count > 0 Then
                    gvSearchResultsQuotes.DataSource = dtQuoteResult
                    gvSearchResultsQuotes.DataBind()
                    dvWarning.Visible = False
                Else
                    gvSearchResultsQuotes.PageIndex = 0
                    gvSearchResultsQuotes.DataBind()
                    dvWarning.Visible = True
                End If
            End If
        ElseIf hdTab.Value = "2" Then
            If Not Session("sQuotePaymentDetails") Is Nothing Then
                Dim dtQuoteResult As DataTable
                dtQuoteResult = Session("sQuotePaymentDetails")
                If dtQuoteResult.Rows.Count > 0 Then
                    gvSearchResultsPayments.DataSource = dtQuoteResult
                    gvSearchResultsPayments.DataBind()
                    dvWarning.Visible = False
                Else
                    gvSearchResultsPayments.PageIndex = 0
                    gvSearchResultsPayments.DataBind()
                    dvWarning.Visible = True
                End If
            End If
        Else
            If Not Session("sBookDetails") Is Nothing Then
                Dim dtResult As DataTable
                dtResult = Session("sBookDetails")
                If dtResult.Rows.Count > 0 Then
                    gvSearchResults.DataSource = dtResult
                    gvSearchResults.DataBind()
                    dvWarning.Visible = False
                Else
                    gvSearchResults.PageIndex = 0
                    gvSearchResults.DataBind()
                    dvWarning.Visible = True
                End If
            End If
        End If
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
  <System.Web.Services.WebMethod()> _
    Public Shared Function GetDeastinationList(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select destcode,destname,desttype from view_destination_search(nolock) where destname like  '%" & prefixText & "%' "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1

                    'Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("destname").ToString(), myDS.Tables(0).Rows(i)("destcode").ToString()))
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("destname").ToString(), myDS.Tables(0).Rows(i)("destcode").ToString() + "|" + myDS.Tables(0).Rows(i)("desttype").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
   <System.Web.Services.WebMethod()> _
    Public Shared Function GetHotelName(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            Dim strDest As String = ""
            Dim strStar As String = ""
            Dim strPropType As String = ""
            If prefixText = " " Then
                prefixText = ""
            End If
            If contextKey <> "" Then
                Dim strContext As String()
                strContext = contextKey.Trim.Split("||")
                For i As Integer = 0 To strContext.Length - 1
                    If strContext(i).Contains("DC:") Then
                        strDest = strContext(i).Replace("DC:", "")
                    End If

                Next


            End If

            Dim str As String = contextKey
            '  strSqlQry = "select p.partycode,p.partyname from partymast p,sectormaster s,catmast c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partyname like  '" & prefixText & "%' "
            'strSqlQry = "select p.partycode,p.partyname from partymast p,sectormaster s,catmast c,view_approved_hotels v where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode=v.partycode  and p.partyname like  '" & prefixText & "%' "
            strSqlQry = "select v.partycode,v.partyname from sectormaster(nolock) s,catmast(nolock) c,view_approved_hotels_new v where v.sectorcode=s.sectorcode  and v.catcode=c.catcode   and v.partyname like  '%" & prefixText & "%' "
            If strDest.Trim <> "" Then
                strSqlQry = strSqlQry & " and (v.citycode = '" & strDest.Trim & "' or s.sectorcode = '" & strDest.Trim & "')  "
            End If


            strSqlQry = strSqlQry & " order by v.partyname  "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
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
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & contextKey.Trim & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    Else
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    End If
                Else
                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"

                End If


            Else
                strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"


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
    <WebMethod()> _
    Public Shared Function GetCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Countries")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

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
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
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
    Public Shared Function GetRODetails(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim UserName As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                strSqlQry = "select UserName,usercode from UserMaster(nolock) where active=1 and  UserName like  '" & prefixText & "%'  order by UserName "

            End If


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    UserName.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("UserName").ToString(), myDS.Tables(0).Rows(i)("usercode").ToString()))
                Next

            End If

            Return UserName
        Catch ex As Exception
            Return UserName
        End Try

    End Function



    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ViewState("vReadMore") = "Read More"
            BindSearchResults()
            If gvSearchResults.Rows.Count > 0 Then
                dvReadMore.Visible = True
            Else
                dvReadMore.Visible = False
            End If
            txtFocus.Focus()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: btnSearch_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
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
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: btnLogOut_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
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
            objclsUtilities.WriteErrorLog("MyAccount.aspx ::btnMyBooking_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
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
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub FillRPorRGReference()
        Dim strRpOrRg As String = ""
        'If Session("sLoginType") <> "RO" Then
        Dim lsDiv01BookingIdPrefixNew As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=2033")
        Dim lsDiv02BookingIdPrefixNew As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=2034")

        If Session("sDivCode") = "01" Then
            txtBookingRef.Text = lsDiv01BookingIdPrefixNew + "/"
            hdBookingRef.Value = lsDiv01BookingIdPrefixNew + "/"
            hdQuoteBookingRef.Value = "QR"
            hdPaymentBookingRef.Value = "QP"
        ElseIf Session("sDivCode") = "02" Then
            txtBookingRef.Text = lsDiv02BookingIdPrefixNew + "/"
            hdBookingRef.Value = lsDiv02BookingIdPrefixNew + "/"
            hdQuoteBookingRef.Value = "QG"
            hdPaymentBookingRef.Value = "QP"
        End If
        'End If


    End Sub


    Protected Sub lbPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)

            Dim lsshowhotelrequest As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected  from reservation_parameters where param_id=5308")

            If lblRequestId.Text <> "" Then
                Dim strpop As String
                Dim ResParam As New ReservationParameters
                ResParam = Session("sobjResParam")
                'Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + lblRequestId.Text.Trim + "'")
                'If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                'If chkCumulative.Trim() = "CUMULATIVE" Then
                'strpop = "window.open('PrintPage.aspx?printId=Itinerary&RequestId=" & lblRequestId.Text.Trim & "');"
                'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupItinerary", strpop, True)
                Dim ConfirmStatus As String = objclsUtilities.ExecuteQueryReturnStringValue("select dbo.fn_booking_confirmstatus_hotel('" & lblRequestId.Text.Trim & "') as ConfirmStatus")
                If ConfirmStatus = "1" And ResParam.LoginType = "RO" Then
                    Dim hvDt As DataTable = objclsUtilities.GetDataFromDataTable("select rlineno from booking_hotel_detail where requestid ='" + lblRequestId.Text.Trim + "' group by rlineno")
                    If hvDt.Rows.Count > 0 Then
                        Dim rlineNumber As Integer = 0
                        For Each hvDr As DataRow In hvDt.Rows
                            rlineNumber = Convert.ToInt32(hvDr("rlineno"))
                            strpop = "window.open('PrintPage.aspx?printId=hotelVoucher&RequestId=" & lblRequestId.Text.Trim & "&rlineNo=" & rlineNumber.ToString() & "');"
                            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup" + rlineNumber.ToString(), strpop, True)
                        Next
                    End If
                End If
                'End If
                strpop = "window.open('PrintPage.aspx?RequestId=" & lblRequestId.Text.Trim & "&printId=bookingConfirmation');"
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup", strpop, True)

                If Session("sLoginType") = "RO" And lsshowhotelrequest = "1" Then '' Added shahul 29/07/18
                    showhotelrequest(lblRequestId.Text.Trim)
                End If

                '*** Service Voucher Danny 14/10/2018 
                If Session("sLoginType") = "RO" Then
                    '*** Check parameter for Need to attach.  Danny
                    '*** Only Confirmed Booking need to create SR. Danny
                    Dim parm(0) As SqlParameter
                    parm(0) = New SqlParameter("@RequestID", CType(lblRequestId.Text.Trim, String))
                    Dim ds_SR As New DataSet
                    ds_SR = objclsUtilities.GetDataSet("SP_SelectServiceDetails", parm)
                    If ds_SR.Tables(0).Rows(0)("Attach").ToString() = "Y" And ds_SR.Tables(0).Rows(0)("ConfirmStatus").ToString() = "1" Then
                        strpop = "window.open('PrintPage.aspx?printId=ServiceVoucher&RequestId=" & lblRequestId.Text.Trim & "');"
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupSR", strpop, True)
                    End If
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbPrint_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

        '
    End Sub

    '' Added shahul 29/07/18
    Sub showhotelrequest(ByVal requestid As String)
        Try

            Dim strpop As String
            'Dim requestid = hdFinalReqestId.Value
            Dim sqlstr As String = "select  distinct d.partycode,isnull(h.amended,0)  amended,  " _
                                   & " isnull(h.cancelled,0) cancelled from booking_hotel_detail d(nolock),booking_hotel_detail_confcancel h(nolock) where d.requestid=h.requestid and d.rlineno=h.rlineno and " _
                                   & " d.requestid='" & requestid & "' and ( isnull(amended,0)=1 or isnull(cancelled,0)=1 or convert(varchar(10),d.moddate,111)=convert(varchar(10),getdate(),111) or  isnull(convert(varchar(10),d.moddate,111),'')='' ) " _
                                   & " and (isnull(d.Rateplansource,'')='' or  d.Rateplansource='Columbus') " 'added param 07/10/2020
            Dim PrintDt As DataTable = objclsUtilities.GetDataFromDataTable(sqlstr)
            Dim Amended As Boolean = False

            If PrintDt.Rows.Count > 0 Then

                For Each printDr As DataRow In PrintDt.Rows


                    strpop = "window.open('PrintPage.aspx?printId=hotelRequest&requestid=" & requestid.Trim & "&partycode=" & printDr("partycode").ToString() & "&amended=" & printDr("amended").ToString() & "&cancelled=" & printDr("cancelled").ToString() & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupHotelRequest" + printDr("partycode").ToString(), strpop, True)



                Next


            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Protected Sub gvSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSearchResults.RowDataBound
        Try
            'If (e.Row.RowType = DataControlRowType.Header) Then
            '    If Session("sLoginType") <> "RO" Then
            '        e.Row.Cells(23).Visible = False

            '    End If



            'End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim lblCanEdit As Label = CType(e.Row.FindControl("lblCanEdit"), Label)
                Dim lbEdit As LinkButton = CType(e.Row.FindControl("lbEdit"), LinkButton)
                'Dim lbCopy As LinkButton = CType(e.Row.FindControl("lbCopy"), LinkButton) '*** Danny 12/09/2018


                If (lblCanEdit.Text = "0" Or lblCanEdit.Text = "1") And Session("sLoginType") = "RO" Then
                    lbEdit.Visible = True
                    'lbCopy.Visible = True



                ElseIf lblCanEdit.Text = "1" And Session("sLoginType") <> "RO" Then
                    lbEdit.Visible = True
                    'lbCopy.Visible = True
                Else
                    lbEdit.Visible = False
                    'lbCopy.Visible = False
                End If

                'Added by abin on 20190211
                If Session("sLoginType") <> "RO" Then
                    lbEdit.Visible = False
                End If

            End If



            'If (e.Row.RowType = DataControlRowType.DataRow) Then
            '    Dim lblCanEdit As Label = CType(e.Row.FindControl("lblCanEdit"), Label)
            '    Dim lbEdit As LinkButton = CType(e.Row.FindControl("lbEdit"), LinkButton)

            '    If Session("sLoginType") <> "RO" Then
            '        e.Row.Cells(23).Visible = False
            '    Else
            '        If lblCanEdit.Text = "0" Then
            '            lbEdit.Visible = False
            '        Else
            '            lbEdit.Visible = True
            '        End If
            '    End If

            'End If
            If hdPrintVoucher.Value <> "1" Then
                e.Row.Cells(24).Visible = False
            End If

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: gvSearchResults_RowDataBound :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub gvSearchResults_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSearchResults.PageIndexChanging
        gvSearchResults.PageIndex = e.NewPageIndex
        '  BindSearchResults()
        BindSearchResults_ForPaging()
        If Not Session("sobjBLLMyAccount") Is Nothing Then
            Dim objMyAccount As New BLLMyAccount
            objMyAccount = Session("sobjBLLMyAccount")
            objMyAccount.GridPageIndex = e.NewPageIndex
            Session("sobjBLLMyAccount") = objMyAccount
        End If
    End Sub
    '*** Danny 19/09/2018
    Sub sendemail(ByVal emailstatus As String, ByVal requestid As String)
        Try

            Dim bc As clsBookingConfirmationPdf = New clsBookingConfirmationPdf()
            '   Dim requestid = hdFinalReqestId.Value
            Dim objclsUtilities As New clsUtilities

            Dim ds As New DataSet
            Dim strpath1 As String = ""
            Dim bytes As Byte() = {}
            Dim ResParam As New ReservationParameters
            ResParam = Session("sobjResParam")
            Dim fileName As String = requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + requestid + "'")
            If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
            If chkCumulative.Trim() = "CUMULATIVE" Then
                bc.GenerateCumulativeReport(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            Else
                bc.GenerateReport(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            End If
            strpath1 = Server.MapPath("~\SavedReports\") + fileName
            Session("sobjResParam") = ResParam

            If chkCumulative.Trim() = "CUMULATIVE" Then
                fileName = "Itinerary@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                fileName = fileName.Replace("/", "-")
                Dim Itinerary As clsItineraryPdf = New clsItineraryPdf()
                bytes = {}
                Itinerary.GenerateItineraryReport(requestid, bytes, "SaveServer", fileName)
                If strpath1 = "" Then
                    strpath1 = Server.MapPath("~\SavedReports\") + fileName
                Else
                    strpath1 = strpath1 + ";" + Server.MapPath("~\SavedReports\") + fileName
                End If
            End If


            ''' Email Formatting
            ''' 

            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim othServDt As DataTable = ds.Tables(3)
            Dim airportDt As DataTable = ds.Tables(4)
            Dim visaDt As DataTable = ds.Tables(5)
            Dim tourDt As DataTable = ds.Tables(6)
            Dim OtherDt As DataTable = ds.Tables(7)
            Dim guestDt As DataTable = ds.Tables(8)
            Dim contactDt As DataTable = ds.Tables(9)
            Dim BankDt As DataTable = ds.Tables(10)
            Dim dtGuestDetails As DataTable = ds.Tables(12)

            Dim strMessage As String = ""
            Dim AgentName As String = "", agentref As String = "", status As String = "", agentcontact As String = "", agentemail As String = "", agentuser As String = ""
            Dim confstatus As String = ""
            Dim clsEmail As New clsEmail
            Dim strQuery As String = ""

            Dim strfromemailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            confstatus = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1151")

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")
            Dim timelimit As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=528")
            timelimit = CType(Val(timelimit) * 24, String)


            Dim to_email As String = ""

            Dim revertmsg As String = "(Please revert within" & confstatus & " hours)"
            Dim strSubject1 As String = ""
            Dim bookingvalue As String = ""
            Dim divcode As String = ""

            If headerDt.Rows.Count > 0 Then
                AgentName = headerDt.Rows(0)("agentname")
                agentref = headerDt.Rows(0)("agentref")
                agentcontact = headerDt.Rows(0)("agentcontact")
                ResParam = Session("sobjResParam")
                If ResParam.LoginType <> "RO" And ResParam.WhiteLabel = "1" Then
                    agentemail = headerDt.Rows(0)("agentandsubemail")
                Else
                    agentemail = headerDt.Rows(0)("agentemail")
                End If



                agentuser = headerDt.Rows(0)("webusername")
                divcode = headerDt.Rows(0)("div_code")
                bookingvalue = CType(headerDt.Rows(0)("salevalue"), String)
                If Emailmode = "Test" Then
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmed", "On Request")
                Else
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmed", "On Request")
                End If

                If Emailmode = "Test" Then
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmation(" & requestid & ")", "Request(" & requestid & ")")
                Else
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmation(" & requestid & ")", "Request(" & requestid & ")")
                End If

            End If

            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                to_email = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                '   strMessage = "Dear " + contactDt.Rows(0)("salesperson") + "," + "&nbsp;<br /><br />"
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                If Session("sLoginType") = "RO" Then
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We Cancelled the attached  booking .</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'>" + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                        If emailstatus = "Amended" Then
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the Attached revised invoice as requested by You and   Details of the booking is as follows:</span></p>"
                        Else
                            If status = "Confirmed" Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following booking  and the same has been <span style='font-weight: bold;color:#1E8449;'>" & status & " </span> . Details of the booking is as follows:</span></p>"
                            Else
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following booking  and the same has been <span style='font-weight: bold;color:#F72A0A;'>" & status & " </span> . Details of the booking is as follows:</span></p>"
                            End If

                        End If


                    End If


                Else
                    Dim contact As String = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You Received the  Cancelled Booking from   <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> .Please check the Attached Booking Ref.</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You have received the following booking from  <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> . Details of the booking is as follows:</span></p>"
                    End If

                End If

            End If



            ' strMessage += "You have received the following booking from " + CType(hdFinalReqestId.Value, String) + " and the same has been confirmed / (on request). Details of the booking is as follows:  <br /><br />"






            'strMessage += "Agent Name : " & AgentName & "&nbsp;<br />"
            'strMessage += "Agency Reference Number : " & agentref & "&nbsp;<br />"

            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agent Name : " + AgentName + "</span></p>"
            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agency Reference Number : " + agentref + "</span></p> <p class='MsoNormal' style='margin: 0'><font face='Calibri,sans-serif'>&nbsp;</font></p>"




            If hotelDt.Rows.Count > 0 Then

                strMessage += " <br /><table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Hotel Name</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check In</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check Out</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Meal Plan</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Lead Guest</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Confirmation Status</th></tr><tr> "

                For i = 0 To hotelDt.Rows.Count - 1

                    strQuery = "select  top 1 title+ ' ' + firstname + ' ' + isnull(middlename,'') + ' ' + isnull(g.lastname,'') guestname   from booking_guest g(nolock),booking_hotel_detail d(nolock) ,booking_hotel_detail_confcancel c(nolock) " _
                        & "  where g.requestid=d.requestid  and g.rlineno=d.rlineno and  d.requestid=c.requestid and d.rlineno =c.rlineno and isnull(c.cancelled,0)=0 and g.roomno=c.roomno  " _
                        & "  and g.requestid='" & requestid & "' and g.rlineno =" & hotelDt.Rows(i)("rlineno") & "  and g.roomno=" & hotelDt.Rows(i)("roomno")
                    Dim leadguest As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("partyname") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkin") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkout") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("roomdetail") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + leadguest + "</td>"
                    If hotelDt.Rows(i)("hotelconfno") <> "" Then
                        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>YES</td></tr>"
                    Else
                        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>NO " + revertmsg + "</td></tr>"
                    End If


                Next
                strMessage += "</table>"
            End If

            If dtGuestDetails.Rows.Count > 0 Then
                strMessage = strMessage + "<br /><p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'><b>Flight Details</b></span></p> "
                strMessage += " <table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>No</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Guest Name</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Arrival Date</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Code</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Time</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Arrival</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Departure Date</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Code</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Time</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Departure</th></tr><tr> "

                For i = 0 To dtGuestDetails.Rows.Count - 1

                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + (i + 1).ToString + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("GuestName") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrdate") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrflightcode") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrflighttime") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arairportbordername") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depdate") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depflightcode") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depflighttime") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depairportbordername") + "</td> </tr>"
                Next
                strMessage += "</table>"
            End If





            If visaDt.Rows.Count > 0 Or othServDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                strMessage += "<br />"
                '  strMessage += "Please find the details of other services booked:" & "&nbsp;<br /><br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the details of other services booked:</span></p>"

            End If
            strMessage += "<br />"
            If visaDt.Rows.Count > 0 Then
                '    Dim visaconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_visa_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(visaconfno,'') <> '' ")
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>VISA            :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If

            If othServDt.Rows.Count > 0 Then
                ' Dim trfconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_transfers_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(transferconfno,'') <> '' ")

                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TRANSFERS       :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If
            If airportDt.Rows.Count > 0 Then
                '  Dim airconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_airportmate_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(airportmateconfno,'') <> '' ")

                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>AIRPORT SERVICES:" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If
            If tourDt.Rows.Count > 0 Then
                ' Dim tourconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_tours_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(toursconfno,'') <> '' ")

                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TOURS           :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If
            If OtherDt.Rows.Count > 0 Then
                '  Dim tourconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_others_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(othersconfno,'') <> '' ")
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>OTHER SERVICES  :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If

            strMessage += "  <br /><br />"
            '  strMessage += "<br />Best Regards,<br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Best Regards,</span></p>"
            ' strMessage += "<br />AgentOnlineCommon software admin team<br />"
            Dim strClientName As String = ""
            strClientName = objclsUtilities.ExecuteQueryReturnSingleValue("select TOP 1 CONM from columbusmaster ")
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>" + IIf(strClientName Is Nothing, "", strClientName) + " software admin team</span></p>"




            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentOnlineCommon
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentOnlineCommon1
            End If



            If agentemail = "" Then
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else
                    agentemail = agentemail + "," + ccemails
                    to_email = IIf(to_email = "", strfromemailid, to_email)
                End If
            Else
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else

                    agentemail = agentemail + "," + ccemails
                    to_email = IIf(to_email = "", strfromemailid, to_email)  ''' RO Email Blank it will take admin Email
                End If
            End If

            ''Added shahul  27/05/2018
            Dim defaultusername As String = "", defaultpwd As String = ""
            Dim strfromusername As String = "", strfrompwd As String = ""

            defaultusername = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1504")
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) option_value from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='" & salesperson & "'")
            If emaildt.Rows.Count > 0 Then
                strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))
            Else
                strfromusername = defaultusername
                strfrompwd = defaultpwd
            End If
            Dim lsSendEmailFromEmailService = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2045")

            If Session("sLoginType") <> "RO" Then
                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    agentemail = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                End If


                If lsSendEmailFromEmailService = 1 Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                Else
                    If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, agentemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                    Else
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                    End If
                End If



            Else
                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    agentemail = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                    If Session("sLoginType") = "RO" Then
                        Dim lsROEmailId As String
                        lsROEmailId = objclsUtilities.ExecuteQueryReturnStringValue("select usemail from usermaster (nolock) where usercode='" & Session("GlobalUserName") & "'")
                        to_email = IIf(lsROEmailId.Trim = "", agentemail, lsROEmailId.Trim)
                        strfromemailid = to_email
                    End If
                End If

                If lsSendEmailFromEmailService = 1 Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                Else
                    If clsEmail.SendEmailOnlinenew(strfromemailid, agentemail, to_email, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                    Else
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                    End If
                End If


            End If



        Catch ex As Exception
            ModalPopupDays.Hide()
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: Sendemail :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Sub sendemailtohotel(ByVal partycode As String, ByVal amended As String, ByVal cancelled As String, ByVal requestid As String)
        ''' Added shahul 30/06/18
        Dim strfromemailid As String = ""
        Dim to_email As String = "", roemail As String = ""
        Dim strSubject1 As String = ""
        Dim strMessage As String = ""
        Dim strpath1 As String = ""

        Dim tosendhotelflag As String = ""

        Try

            Dim ds As New DataSet

            '  If PrintDt.Rows.Count > 0 Then

            Dim cnt As Integer = 0
            'For Each printDr As DataRow In PrintDt.Rows
            ''' Added shahul 30/06/18

            tosendhotelflag = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from agentbooking_notsenthotel(nolock) where agentcode in (select agentcode from booking_header(nolock) where requestid='" & requestid & "')")

            Dim lsShowRateParam As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=2021")
            Dim lbShowRate As Boolean = False
            If lsShowRateParam.Trim.ToUpper = "Y" Then
                lbShowRate = True
            End If

            cnt = cnt + 1
            'Dim partycode = printDr("partycode").ToString()
            'Dim amended As Integer = printDr("amended").ToString()
            'Dim cancelled As Integer = printDr("cancelled").ToString()
            Dim strpop As String
            Dim pop As String = "popup" + cnt.ToString()

            Dim bh As ClsBookingHotelPdf = New ClsBookingHotelPdf()


            Dim bytes As Byte() = {}
            Dim fileName As String = "BookHotel@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            bh.BookingHotelPrint(requestid, partycode, amended, cancelled, bytes, ds, "SaveServer", fileName)
            strpath1 = Server.MapPath("~\SavedReports\") + fileName


            ''' Email Formatting
            ''' 

            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim guestDt As DataTable = ds.Tables(3)
            Dim allotmentdDt As DataTable = ds.Tables(4)
            Dim contactDt As DataTable = ds.Tables(5)

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim URLAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1503")
            Dim RequestDt As DataTable = objclsUtilities.GetDataFromDataTable("select * from booking_hotel_request where requestid='" & requestid & "' and partycode='" & partycode & "'")
            Dim requestDr = (From n In RequestDt.AsEnumerable() Where n.Field(Of String)("partycode") = partycode).FirstOrDefault()
            Dim encryptParam As String = ""
            If Not requestDr Is Nothing Then
                Dim crypto As New clsHotelCryptography()
                Dim combineParam As String = requestid.Trim() + "~" + partycode.Trim() + "~" + amended.ToString() + "~" + cancelled.ToString() + "~" + requestDr("pwdConfirm").ToString().Trim
                encryptParam = crypto.Encrypt(combineParam)
            End If


            Dim dt As New DataTable
            Dim BLLGuest = New BLLGuest
            Dim roomtocheck As Boolean = False






            Dim companyname As String = "", partyname As String = "", status As String = "", contact As String = "", address As String = "", agentuser As String = ""
            Dim confstatus As String = ""
            Dim strQuery As String = ""

            Dim clsEmail As New clsEmail

            strfromemailid = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            confstatus = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1151")

            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")


            Dim guestnames As String = "", offernames As String = "", price As Double
            Dim divcode As String = ""
            If headerDt.Rows.Count > 0 Then
                companyname = headerDt.Rows(0)("division_master_des")
                partyname = headerDt.Rows(0)("partyname")
                address = headerDt.Rows(0)("address1") + " Tel No:" + headerDt.Rows(0)("tel") + " Fax No: " + headerDt.Rows(0)("fax")
                to_email = headerDt.Rows(0)("email1")
                divcode = headerDt.Rows(0)("div_code")
                If Not Session("sEditRequestId") Is Nothing Then
                    If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") Then
                        strSubject1 = IIf(cancelled = "1", "Cancellation Reservation Alert - ", "Amendment Reservation Alert - ") + requestid
                    Else
                        strSubject1 = "New Reservation Alert - " + requestid
                    End If

                Else
                    strSubject1 = "New Reservation Alert - " + requestid
                End If

            End If
            Dim GRcontactno As String = ""
            If divcode = "01" Then
                GRcontactno = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1203")
            Else
                GRcontactno = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1204")
            End If
            Dim salesemail As String = ""
            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                roemail = contactDt.Rows(0)("salesemail")
                salesemail = contactDt.Rows(0)("salesemail")
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                contact = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
            End If
            If guestDt.Rows.Count > 0 Then

                For i = 0 To guestDt.Rows.Count - 1
                    If Val(guestDt.Rows(i)("childage")) <> 0 Then
                        guestnames = guestnames + "/" + guestDt.Rows(i)("guestname") + " ( " + CType(Format(guestDt.Rows(i)("childage"), 0), String) + " Yrs) "
                    Else
                        guestnames = guestnames + "/" + guestDt.Rows(i)("guestname")
                    End If

                Next

                guestnames = Right(guestnames, Len(guestnames) - 1)
            End If
            Dim totalcost As Double
            If hotelDt.Rows.Count > 0 Then
                For i = 0 To hotelDt.Rows.Count - 1
                    totalcost = totalcost + Val(hotelDt.Rows(i)("costvalue"))
                Next
            End If

            If tariffDt.Rows.Count > 0 Then
                For i = 0 To tariffDt.Rows.Count - 1
                    offernames = offernames + "/" + tariffDt.Rows(i)("bookingcode")

                Next
                offernames = Right(offernames, Len(offernames) - 1)
            End If

            strQuery = "select 't' from booking_hotel_detail h, booking_hotel_detail_confcancel d(nolock) where h.requestid=d.requestid and h.rlineno=d.rlineno and  h.requestid='" & requestid & "' and h.partycode='" & partycode & "' and isnull(d.cancelled,0)=0 and isnull(d.hotelconfno,'') <>''"
            Dim hotelconf As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            If hotelDt.Rows.Count > 0 Then



                strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #3E3F4B; font-size: 12pt;'>Dear Team,</span><span style='font-family:calibri,sans-serif;color:#3E3F4B'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                If cancelled = "1" Then   '' Cancellation Email Text

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Greetings from  <span style='font-weight: bold;'>" + CType(companyname, String) + "  </span> !.</p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We request you to cancel the below booking without any charges.</p>"

                    For i = 0 To hotelDt.Rows.Count - 1

                        Dim guestname As String = ""
                        Dim bookingcode As String = ""
                        dt = BLLGuest.HotelEmailRoomcheck(requestid, partycode, amended, cancelled, hotelDt.Rows(i)("rlineno"))
                        If dt.Rows.Count > 0 Then
                            roomtocheck = True
                        Else
                            roomtocheck = False
                        End If

                        strQuery = "select Isnull((Stuff((Select  '/' + g.title+' '+g.firstname+' '+g.middlename+' '+g.lastname + case when guesttype='Child' then ' ( ' + convert(varchar(10),convert(int,g.childage)) + ' Yrs)' else ''  end  from booking_guest g(nolock)  " _
                            & " where requestid='" & requestid & "' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,1,'')),'') "
                        guestname = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        strQuery = "select Isnull((Stuff((Select distinct  '/' + g.bookingcode    from booking_hotel_detail_prices g(nolock)  " _
                          & " where requestid='" & requestid & "'  and bookingcode<>'' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,1,'')),'') "
                        bookingcode = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)



                        strMessage += "<br />"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Hotel Name        : " + partyname + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Room Type         : " + hotelDt.Rows(i)("rmtypname") + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Meal Plan         : " + hotelDt.Rows(i)("mealcode") + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Name of Pax       : " + guestname + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Offer Confirmed   : " + bookingcode + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Confirmation No   : " + hotelDt.Rows(i)("hotelconfno") + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Cancellation No   : " + hotelDt.Rows(i)("hotelcancelno") + "</span></p>"
                    Next





                Else

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Greetings from  <span style='font-weight: bold;'>" + CType(companyname, String) + "  </span> !.</p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We have received the below booking for your hotel.</p>"

                    If hotelconf <> "" Then
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>It has already been confirmed to the agency through the availability in our system and the agency has received the prepaid accommodation voucher.</p>"
                    Else
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We request you to send the confirmation for the below booking received from our agency partner.</p>"
                    End If

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Please find the details below given for the booking:</span></p>"


                    For i = 0 To hotelDt.Rows.Count - 1

                        Dim guestname As String = ""
                        Dim bookingcode As String = ""
                        dt = BLLGuest.HotelEmailRoomcheck(requestid, partycode, amended, cancelled, hotelDt.Rows(i)("rlineno"))
                        If dt.Rows.Count > 0 Then
                            roomtocheck = True
                        Else
                            roomtocheck = False
                        End If

                        strQuery = "select Isnull((Stuff((Select  '/' + g.title+' '+g.firstname+' '+g.middlename+' '+g.lastname + case when guesttype='Child' then ' ( ' + convert(varchar(10),convert(int,g.childage)) + ' Yrs)' else ''  end  from booking_guest g(nolock)  " _
                            & " where requestid='" & requestid & "' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,1,'')),'') "
                        guestname = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        strQuery = "select Isnull((Stuff((Select distinct  '/' + g.bookingcode    from booking_hotel_detail_prices g(nolock)  " _
                          & " where requestid='" & requestid & "'  and bookingcode<>'' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,1,'')),'') "
                        bookingcode = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        If roomtocheck = True Then

                            strMessage += "<br />"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Booking Time      : " + Now + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Hotel Name        : " + partyname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Room Type         : " + hotelDt.Rows(i)("rmtypname") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Meal Plan         : " + hotelDt.Rows(i)("mealcode") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Accommodation Type: " + hotelDt.Rows(i)("roomdetails") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Name        : " + guestname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Nationality : " + IIf(guestnames <> "", guestDt.Rows(0)("nationality"), "") + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Offer Name        : " + bookingcode + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>No.Of.Rooms        : " + CType(hotelDt.Rows.Count, String) + "</span></p>"

                            ''// Hide value temporarily for VAT implementation
                            If lbShowRate = True Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Total Reservation Price: " + headerDt.Rows(0)("costcurrcode") + " " + CType(totalcost, String) + "</span></p>"
                            End If
                            Exit For
                        Else
                            strMessage += "<br />"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Booking Time      : " + Now + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Hotel Name        : " + partyname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Room Type         : " + hotelDt.Rows(i)("rmtypname") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Meal Plan         : " + hotelDt.Rows(i)("mealcode") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Accommodation Type: " + hotelDt.Rows(i)("roomdetails") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Name        : " + guestname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Nationality : " + IIf(guestnames <> "", guestDt.Rows(0)("nationality"), "") + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Offer Name        : " + bookingcode + "</span></p>"

                            ''// Hide value temporarily for VAT implementation
                            If lbShowRate = True Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Total Reservation Price: " + headerDt.Rows(0)("costcurrcode") + " " + CType(hotelDt.Rows(i)("costvalue"), String) + "</span></p>"
                            End If
                        End If



                    Next

                    If cancelled = 0 Then
                        strMessage += "<br />"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B;font-weight:bold'>Please visit the following link to confirm the booking :</span><br/><a href='" + URLAddress + "/BookingHotelConfirm.aspx?ids=" + encryptParam + "'>" + URLAddress + "/BookingHotelConfirm.aspx?ids=" + encryptParam + "</a></p>"
                    End If

                    strMessage += "<br />"
                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Please find attached herewith PDF document for  further details </span></p>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Also please note : </span></p>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoListParagraph' style='text-indent:-.25in'><span style='font-family:symbol'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ·</span><span style='font-size:5.0pt;font-family:times new roman,serif'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family: calibri, arial, helvetica, sans-serif;'>Payment has been guaranteed by " + CType(companyname, String) + " as per the booking and credit terms.</span>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoListParagraph' style='text-indent:-.25in'><span style='font-family:symbol'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ·</span><span style='font-size:5.0pt;font-family:times new roman,serif'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family: calibri, arial, helvetica, sans-serif;'>Rates are confidential and not to be disclosed to the clients.</span>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoListParagraph' style='text-indent:-.25in'><span style='font-family:symbol'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ·</span><span style='font-size:5.0pt;font-family:times new roman,serif'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family: calibri, arial, helvetica, sans-serif;'>Deposits / Extras / Tourism Dirham under the account of the guest and to be collected directly as per hotel policy.</span>"

                    strMessage += "<br /><br />"
                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Contact details: </span></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Reservation  : " + contact + "<" + roemail + ">" + " </span></p>" ' <p class='MsoNormal' style='margin: 0'><font face='Calibri,sans-serif'>&nbsp;</font></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>During guest(s) stay: In case of any Emergency Please contact Guest Relation Officer ( " + GRcontactno + " ) </span></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Other urgent issues :</span></p>"
                    Dim strClientName As String = ""
                    strClientName = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=69") '*** Danny 19/09/2018
                    strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>StopSale  :<span style='font-family: calibri, arial, helvetica, sans-serif; color: #0033ff; text-decoration: underline;'>" + IIf(strClientName Is Nothing, "", strClientName) + "</span></span></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Invoice address:" + companyname + "</span></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>" + address + "</span></p>"

                End If

                strMessage += "<br /><br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Best Regards,</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>" + contact + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>" + companyname + "</span></p>"

            End If


            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentOnlineCommon
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentOnlineCommon1
            End If


            If to_email = "" Then
                If Emailmode = "Test" Then
                    to_email = testEmail
                    roemail = testEmail
                Else
                    to_email = strfromemailid  '''  if Hotel Email Blank it will take Admin

                    roemail = roemail + "," + ccemails
                End If
            Else
                If Emailmode = "Test" Then
                    to_email = testEmail
                    roemail = testEmail
                Else
                    ' to_email = strfromemailid
                    If roemail = "" Then    '''  if RO Email Blank it will take Admin
                        roemail = strfromemailid + "," + ccemails
                    Else
                        roemail = roemail + "," + ccemails
                    End If

                End If
            End If


            ''Added shahul  27/05/2018
            Dim defaultusername As String = "", defaultpwd As String = ""
            Dim strfromusername As String = "", strfrompwd As String = ""

            defaultusername = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1504")
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) option_value from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='" & salesperson & "'")
            If emaildt.Rows.Count > 0 Then
                strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))
            Else
                strfromusername = defaultusername
                strfrompwd = defaultpwd
            End If

            Dim lsSendEmailFromEmailService = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2045")
            If tosendhotelflag <> "" Then
                to_email = strfromemailid
                roemail = salesemail


                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    to_email = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                End If

                'changed by mohamed on 01/08/2018 as not to send email from online since it is taking time

                If lsSendEmailFromEmailService = 1 Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                Else
                    If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, roemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_HOTEL")
                    Else
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                    End If
                End If
            Else
                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    to_email = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                    If Session("sLoginType") = "RO" Then
                        Dim lsROEmailId As String
                        lsROEmailId = objclsUtilities.ExecuteQueryReturnStringValue("select usemail from usermaster (nolock) where usercode='" & Session("GlobalUserName") & "'")
                        roemail = IIf(lsROEmailId.Trim = "", to_email, lsROEmailId.Trim)
                        strfromemailid = roemail
                    End If
                End If

                If lsSendEmailFromEmailService = 1 Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                Else
                    If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, roemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_HOTEL")
                    Else
                        objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                    End If
                End If
            End If


            '    Next




            'End If


        Catch ex As Exception
            ModalPopupDays.Hide()
            If tosendhotelflag = "" Then
                objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
            End If
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("Myaccount.aspx :: SendemailHotel :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub lbAgeEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            If lblRequestId.Text <> "" And Session("sLoginType") = "RO" Then
                Dim requestid = lblRequestId.Text
                If objclsUtilities.fn_NeedToSendAgentEmail() Then 'This Condition is added By Mohamed 'changed by mohamed on 28/06/2018

                    sendemail("New", requestid)
                    objclsUtilities.SaveEmailLog(lblRequestId.Text, "Agent", "", "0", "0", "1", "1", Session("GlobalUserName").ToString)
                End If
            End If

        Catch ex As Exception
            ModalPopupDays.Hide()
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: Sendemail :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Protected Sub lbHotEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim objclsUtilities As New clsUtilities
        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)

            Dim lsshowhotelrequest As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected  from reservation_parameters where param_id=5308")

            If lblRequestId.Text <> "" And Session("sLoginType") = "RO" Then

                Dim requestid = lblRequestId.Text
                Dim sqlstr As String = "select  distinct d.partycode,isnull(h.amended,0)  amended,  " _
                                       & " isnull(h.cancelled,0) cancelled from booking_hotel_detail d(nolock),booking_hotel_detail_confcancel h(nolock) where d.requestid=h.requestid and d.rlineno=h.rlineno and " _
                                       & " d.requestid='" & requestid & "' and ( isnull(amended,0)=1 or isnull(cancelled,0)=1 or convert(varchar(10),d.moddate,111)=convert(varchar(10),getdate(),111) or  isnull(convert(varchar(10),d.moddate,111),'')='' ) " _
                                       & " and (isnull(d.Rateplansource,'')='' or  d.Rateplansource='Columbus') " 'added param 07/10/2020
                Dim PrintDt As DataTable = objclsUtilities.GetDataFromDataTable(sqlstr)
                Dim Amended As Boolean = False

                If PrintDt.Rows.Count > 0 Then
                    Dim lbNeedHotelEmail As Boolean
                    lbNeedHotelEmail = objclsUtilities.fn_NeedToSendHotelEmail()
                    For Each printDr As DataRow In PrintDt.Rows
                        If lbNeedHotelEmail = True Then 'This Condition is added By Mohamed 'changed by mohamed on 28/06/2018

                            ' disabled hotel email for cumulative user. Modified by abin on 17/12/2017
                            '    FindCumilative()
                            Dim iCumulativeUser As Integer = 0
                            If Session("sLoginType") = "RO" Then
                                Dim strQuery As String = "select count(agentcode)CNT from agentmast(nolock) where bookingengineratetype='CUMULATIVE' and agentcode=(select min(agentcode) from  booking_header(nolock) where requestid='" & requestid & "')"
                                iCumulativeUser = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

                            End If

                            'If hdBookingEngineRateType.Value = "1" Or iCumulativeUser > 0 Then
                            'Else

                            If Not lblRequestId.Text Is Nothing And printDr("amended").ToString() = 1 And printDr("cancelled").ToString() = 0 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString(), requestid)
                                Amended = True
                            ElseIf lblRequestId.Text Is Nothing And printDr("amended").ToString() = 0 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString(), requestid)

                            ElseIf Not lblRequestId.Text Is Nothing And printDr("cancelled").ToString() = 1 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString(), requestid)
                            ElseIf Not lblRequestId.Text Is Nothing And printDr("amended").ToString() = 0 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString(), requestid)
                            End If

                            ' End If


                            objclsUtilities.SaveEmailLog(lblRequestId.Text, "Hotel", printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString(), "1", "1", Session("GlobalUserName").ToString)
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbPrint_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

        '
    End Sub
    '*** Danny 12/09/2018
    Protected Sub lbCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lbCopy As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbCopy.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            Dim strEncrypted As String = objclsUtilities.Encrypt(lblRequestId.Text)

            If lblRequestId.Text <> "" Then
                hdRequestId.Value = lblRequestId.Text

                Dim myAcct As New BLLMyAccount()
                Dim usercode As String = Session("GlobalUserName")
                Dim appid As String = "3" ' Reservation module
                Dim privilegeid As String
                Dim supplierUpdated As String = myAcct.checkSupplierUpdated(lblRequestId.Text.Trim)
                If supplierUpdated <> "" Then
                    If Session("sLoginType") <> "RO" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Updated Suppliers for this booking; Can not edit")
                        Exit Sub
                    Else
                        privilegeid = "29" 'Allow amend the invoice after suppliers updated for booking
                        Dim PrivilegeAfterSupplierUpdate As Boolean = myAcct.checkPrivilege(usercode, appid, privilegeid)
                        If PrivilegeAfterSupplierUpdate = False Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Updated Suppliers for this booking; Can not edit")
                            Exit Sub
                        Else
                            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "suppUpdate", "alert('Once you edit the booking, you may have to update suppliers again');", True)
                            'MessageBox.ShowMessage(Page, MessageType.Warning, "Once you edit the booking, you may have to update suppliers again")
                        End If
                    End If
                End If
                Dim invoiced As String = myAcct.checkInvoiced(lblRequestId.Text.Trim)
                If invoiced <> "" Then
                    If Session("sLoginType") <> "RO" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Invoice already prepared; Can not edit")
                        Exit Sub
                    Else
                        privilegeid = "12" 'Amend Reservation after invoice
                        Dim checkPrivilege As Boolean = myAcct.checkPrivilege(usercode, appid, privilegeid)
                        If checkPrivilege = True Then
                            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmPrivilege('" + lblRequestId.Text + "');", True)
                            Exit Sub
                        Else
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Invoice already prepared; Can not edit")
                            Exit Sub
                        End If
                    End If
                Else
                    btnCopy_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbCopy_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim strRequestid As String = Request.Form("req_value")
        If Request.Form("req_value") Is Nothing Then
            strRequestid = hdRequestId.Value
        End If
        If strRequestid <> "" Then
            clearallBookingSessions()
            strRequestid = FillBookingToTempForEdit(strRequestid, "COPY")
            Session("sEditRequestId") = Nothing
            Session("sRequestId") = strRequestid
            Response.Redirect("MoreServices.aspx")
        End If
    End Sub
    Protected Sub lbEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim lbEdit As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbEdit.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            Dim strEncrypted As String = objclsUtilities.Encrypt(lblRequestId.Text)
            ' Dim strDecrypted As String = objclsUtilities.Decrypt(strEncrypted)
            If lblRequestId.Text <> "" Then
                hdRequestId.Value = lblRequestId.Text
                'If Not Session("sRequestId") Is Nothing Then
                '    Dim strScript As String = "javascript: fnConfirm();"
                '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)
                'End If
                If hdRequestId.Value.Contains("RGV") Or hdRequestId.Value.Contains("RPV") Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "It is visa only booking; You can not edit. Please contact VISA Reservation Officer.")
                    Exit Sub
                End If


                'Dim dtAssignServiceLock As DataTable = objclsUtilities.GetDataFromDataTable("execute sp_CheckService_AssignStatus '" + hdRequestId.Value + "' ")

                'If dtAssignServiceLock.Rows.Count > 0 Then
                '    Dim strLockWarning As String = ""
                '    For i As Integer = 0 To dtAssignServiceLock.Rows.Count - 1
                '        strLockWarning = strLockWarning & "</br>" & dtAssignServiceLock.Rows(i)("Warning").ToString
                '    Next
                '    MessageBox.ShowMessage(Page, MessageType.Warning, strLockWarning)
                '    Exit Sub

                'End If

                Dim strAllow As String = objclsUtilities.ExecuteQueryReturnStringValue("execute sp_checkpastbooking '" + hdRequestId.Value + "','" + Session("GlobalUserName") + "' ")
                If strAllow <> "1" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "It is a past booking; You can not edit. Please contact administrator.")
                    Exit Sub
                End If

                Dim myAcct As New BLLMyAccount()
                Dim usercode As String = Session("GlobalUserName")
                Dim appid As String = "3" ' Reservation module
                Dim privilegeid As String
                Dim supplierUpdated As String = myAcct.checkSupplierUpdated(lblRequestId.Text.Trim)
                If supplierUpdated <> "" Then
                    If Session("sLoginType") <> "RO" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Updated Suppliers for this booking; Can not edit")
                        Exit Sub
                    Else
                        'privilegeid = "29" 'Allow amend the invoice after suppliers updated for booking
                        'Dim PrivilegeAfterSupplierUpdate As Boolean = myAcct.checkPrivilege(usercode, appid, privilegeid)
                        'If PrivilegeAfterSupplierUpdate = False Then
                        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Updated Suppliers for this booking; Can not edit")
                        '    Exit Sub
                        'Else
                        '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "suppUpdate", "alert('Once you edit the booking, you may have to update suppliers again');", True)
                        '    'MessageBox.ShowMessage(Page, MessageType.Warning, "Once you edit the booking, you may have to update suppliers again")
                        'End If
                    End If
                End If
                Dim invoiced As String = myAcct.checkInvoiced(lblRequestId.Text.Trim)
                If invoiced <> "" Then
                    If Session("sLoginType") <> "RO" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Invoice already prepared; Can not edit")
                        Exit Sub
                    Else
                        privilegeid = "12" 'Amend Reservation after invoice
                        Dim checkPrivilege As Boolean = myAcct.checkPrivilege(usercode, appid, privilegeid)
                        If checkPrivilege = True Then
                            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmPrivilege('" + lblRequestId.Text + "');", True)
                            Exit Sub
                        Else
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Invoice already prepared; Can not edit")
                            Exit Sub
                        End If
                    End If
                Else
                    btnEdit_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbEdit_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim strRequestid As String = Request.Form("req_value")
        If Request.Form("req_value") Is Nothing Then
            strRequestid = hdRequestId.Value
        End If
        If strRequestid <> "" Then
            clearallBookingSessions()
            FillBookingToTempForEdit(strRequestid, "EDIT")
            Session("sEditRequestId") = strRequestid
            Session("sRequestId") = strRequestid
            Response.Redirect("MoreServices.aspx")
        End If
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

        Session("VisaDetailsDT") = Nothing
        Session("vlineno") = Nothing
        Session("sFinalBooked") = Nothing



    End Sub

    Private Function FillBookingToTempForEdit(ByVal strRequestId As String, ByVal CopyOREdit As String) As String
        Dim objBLLMyAccount As New BLLMyAccount
        FillBookingToTempForEdit = objBLLMyAccount.FillBookingToTempForEdit(strRequestId, Session("GlobalUserName"), CopyOREdit)

    End Function

    Private Sub FillQuoteToTempForEdit(ByVal strRequestId As String)
        Dim objBLLMyAccount As New BLLMyAccount
        objBLLMyAccount.FillQuoteToTempForEdit(strRequestId, Session("GlobalUserName"))

    End Sub


    Protected Sub imgbReadMoreNew_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        If gvSearchResults.Rows.Count > 0 Then
            dvReadMore.Visible = True
            If imgbReadMoreNew.ToolTip = "Read More" Then
                imgbReadMoreNew.ToolTip = "Read Less"
                ViewState("vReadMore") = "Read Less"
                gvSearchResults.Columns(1).Visible = True
                gvSearchResults.Columns(3).Visible = True
                gvSearchResults.Columns(8).Visible = True
                gvSearchResults.Columns(9).Visible = True
                gvSearchResults.Columns(11).Visible = True
                gvSearchResults.Columns(14).Visible = True
                gvSearchResults.Columns(19).Visible = True
                gvSearchResults.Columns(20).Visible = True
                gvSearchResults.Columns(21).Visible = True
                gvSearchResults.Columns(22).Visible = True
                imgbReadMoreNew.ImageUrl = "~/img/Readless.png"
            Else
                imgbReadMoreNew.ToolTip = "Read More"
                ViewState("vReadMore") = "Read More"

                gvSearchResults.Columns(1).Visible = False
                gvSearchResults.Columns(3).Visible = False
                gvSearchResults.Columns(8).Visible = False
                gvSearchResults.Columns(9).Visible = False
                gvSearchResults.Columns(11).Visible = False
                gvSearchResults.Columns(14).Visible = False
                gvSearchResults.Columns(19).Visible = False
                gvSearchResults.Columns(20).Visible = False
                gvSearchResults.Columns(21).Visible = False
                gvSearchResults.Columns(22).Visible = False
                imgbReadMoreNew.ImageUrl = "~/img/ReadMore.png"
            End If


        End If
    End Sub

    Private Sub LoadSubMenus()
        Dim dtMenus As DataTable
        objResParam = Session("sobjResParam")
        dtMenus = objBLLMenu.Getmenus(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode, "2")
        hdFindBooking.Value = "0"
        hdPrintVoucher.Value = "0"
        If dtMenus.Rows.Count > 0 Then
            If objResParam.SubUserCode <> "" Then
                If dtMenus.Rows.Count > 0 Then
                    For Each row As DataRow In dtMenus.Rows
                        If row("menudesc") = "Find Booking" Then
                            hdFindBooking.Value = "1"
                            Exit For
                        End If
                    Next
                    For Each row As DataRow In dtMenus.Rows
                        If row("menudesc") = "Print Vouchers" Then
                            hdPrintVoucher.Value = "1"
                            Exit For
                        End If
                    Next
                End If
            Else
                hdFindBooking.Value = "1"
                hdPrintVoucher.Value = "1"
            End If
            If Session("sLoginType") = "RO" Then
                hdFindBooking.Value = "1"
                hdPrintVoucher.Value = "1"
            End If
            Dim dtMenusHeader As DataTable
            dtMenusHeader = dtMenus.DefaultView.ToTable(True, "parentname")
            If dtMenusHeader.Rows.Count > 0 Then
                dlSubMenuHeader.DataSource = dtMenusHeader
                dlSubMenuHeader.DataBind()

            Else


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
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: dlSubMenuHeader_ItemDataBound :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
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
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: dlSubMenuHeader_ItemDataBound :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub



    Protected Sub gvSearchResultsQuotes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSearchResultsQuotes.PageIndexChanging
        gvSearchResultsQuotes.PageIndex = e.NewPageIndex
        'BindSearchResults()
        BindSearchResults_ForPaging()
        If Not Session("sobjBLLMyAccount") Is Nothing Then
            Dim objMyAccount As New BLLMyAccount
            objMyAccount = Session("sobjBLLMyAccount")
            objMyAccount.GridPageIndex = e.NewPageIndex
            Session("sobjBLLMyAccount") = objMyAccount
        End If
    End Sub

    Protected Sub lbQAlternativeHotel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbQAlternativeHotel_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Protected Sub lbQEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lbEdit As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbEdit.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            Dim lblColumbusRef As Label = CType(gvRow.FindControl("lblColumbusRef"), Label)

            If lblColumbusRef.Text.Contains("RP/") Or lblColumbusRef.Text.Contains("RG/") Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Quote has been converted to booking.")
                Exit Sub
            End If


            If lblRequestId.Text <> "" Then
                hdRequestId.Value = lblRequestId.Text

                'Dim objBLLMyAccounts As New BLLMyAccount
                'Dim strRateChange As String = objBLLMyAccounts.CheckQuoteRateChange(lblRequestId.Text)
                'If strRateChange = "3" Then
                '    MessageBox.ShowMessage(Page, MessageType.Warning, "The rate plans in the quote are not valid now so this quote cannot be converted.")
                '    Exit Sub
                'End If
                'If strRateChange = "2" Then

                '    If Session("sLoginType") <> "RO" Then
                '        MessageBox.ShowMessage(Page, MessageType.Warning, "The prices are manually overridden. Please contact RO to convert this quote.")
                '        Exit Sub
                '    End If

                '    Dim strMsg As String = "The prices are manually overridden. Please check the prices once again and proceed."
                '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmRateChange('" + lblRequestId.Text + "','" + strMsg + "');", True)
                '    Exit Sub
                'End If
                'If strRateChange = "1" Then
                '    Dim strMsg As String = "The prices have changed, do you want to proceed?."
                '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmRateChange('" + lblRequestId.Text + "','" + strMsg + "');", True)
                '    Exit Sub
                'End If

                Dim dt As DataTable
                dt = objclsUtilities.GetDataFromDataTable("exec sp_validate_booking_quotation_edit  '" + lblRequestId.Text + "'")
                Dim strMsg As String = ""
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        strMsg = strMsg & dt.Rows(i)("warning").ToString & "\n"
                    Next
                    strMsg = strMsg & " Do you want to proceed?"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmRateChange('" + lblRequestId.Text + "','" + strMsg + "');", True)
                    Exit Sub
                End If

                btnQuoteEdit_Click()

            End If
            '    End If

            'End If


        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbQEdit_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Protected Sub lbQPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            If lblRequestId.Text <> "" Then
                Dim strpop As String
                Dim ResParam As New ReservationParameters
                ResParam = Session("sobjResParam")
                Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + lblRequestId.Text.Trim + "'")
                If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                If chkCumulative.Trim() = "CUMULATIVE" And ResParam.LoginType = "RO" Then
                    strpop = "window.open('PrintPage.aspx?printId=QuoteCostingExcel&quoteId=" & lblRequestId.Text.Trim & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupQCosting", strpop, True)
                End If
                '   strpop = "window.open('PrintPage.aspx?RequestId=" & lblRequestId.Text.Trim & "&printId=bookingConfirmation');"
                strpop = "window.open('PrintPage.aspx?printID=bookingQuote&quoteId=" & lblRequestId.Text.Trim & "');"
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup", strpop, True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbQPrint_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnQuoteEdit_Click() Handles btnQuoteEdit.Click
        Dim strRequestid As String = Request.Form("req_value")

        If Request.Form("req_value") Is Nothing Then
            strRequestid = hdRequestId.Value
        End If

        If strRequestid <> "" Then
            clearallBookingSessions()
            FillQuoteToTempForEdit(strRequestid)
            RevisePriceForEdit(strRequestid)

            Session("sEditRequestId") = strRequestid
            Session("sRequestId") = strRequestid
            Response.Redirect("MoreServices.aspx")
        End If
    End Sub

    Private Sub RevisePriceForEdit(ByVal strRequestid As String)
        Dim objBLLMyAccount As New BLLMyAccount
        Dim strStatus As String = objBLLMyAccount.RevisePriceForEdit(strRequestid)
    End Sub
    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
    End Sub

    Private Sub BindPreviousSearch()
        If Not Session("sobjBLLMyAccount") Is Nothing Then
            Dim objMyAccount As New BLLMyAccount
            objMyAccount = Session("sobjBLLMyAccount")
            hdTab.Value = objMyAccount.Tab

            txtBookingRef.Text = objMyAccount.RequestId
            ddlServiceType.SelectedValue = objMyAccount.ServiceType
            If objMyAccount.DestinationCode <> "" And objMyAccount.DestinationCode <> "" Then
                txtDestinationCode.Text = objMyAccount.DestinationCode & "|" & objMyAccount.DestinationType
            End If


            txtAgentRef.Text = objMyAccount.AgentRef
            txtGuestFirstName.Text = objMyAccount.GuestFirstName
            txtGuestSecondName.Text = objMyAccount.GuestLastName
            ddlTravelDate.SelectedValue = objMyAccount.TravelDateType
            txtTravelFromDate.Text = objMyAccount.TravelDateFrom
            txtTravelToDate.Text = objMyAccount.TravelDateTo
            ddlBookingDate.SelectedValue = objMyAccount.BookingDateType
            txtBookingFromDate.Text = objMyAccount.BookingDateFrom
            txtBookingToDate.Text = objMyAccount.BookingDateTo
            ddlBookingStatus.SelectedValue = objMyAccount.BookingStatus
            txtHotelCode.Text = objMyAccount.PartyCode
            txtHotelConfNo.Text = objMyAccount.HotelConfNo
            txtCustomerCode.Text = objMyAccount.SearchAgentCode
            txtCountryCode.Text = objMyAccount.SourceCountrycode
            txtROCode.Text = objMyAccount.UserCode

            'If hdTab.Value <> "1" Then
            '    Dim dtResult As DataTable
            '    dtResult = objMyAccount.GetBookingSearchDetails()
            '    If dtResult.Rows.Count > 0 Then
            '        gvSearchResults.PageIndex = objMyAccount.GridPageIndex
            '        gvSearchResults.DataSource = dtResult
            '        gvSearchResults.DataBind()
            '        dvWarning.Visible = False
            '    Else
            '        gvSearchResults.PageIndex = 0
            '        gvSearchResults.DataBind()
            '        dvWarning.Visible = True
            '    End If
            'Else
            '    Dim dtQuoteResult As DataTable
            '    dtQuoteResult = objMyAccount.GetQuoteSearchDetails()
            '    If dtQuoteResult.Rows.Count > 0 Then
            '        gvSearchResultsQuotes.PageIndex = objMyAccount.GridPageIndex
            '        gvSearchResultsQuotes.DataSource = dtQuoteResult
            '        gvSearchResultsQuotes.DataBind()
            '        dvWarning.Visible = False
            '    Else
            '        gvSearchResultsQuotes.PageIndex = 0
            '        gvSearchResultsQuotes.DataBind()
            '        dvWarning.Visible = True
            '    End If
            'End If
        End If
    End Sub

    Protected Sub btnRetrieve_Click(sender As Object, e As System.EventArgs) Handles btnRetrieve.Click


        If txtTempRefNo.Text.Trim <> "" And txtTempRefNo.Text.Trim.Contains("TP/") Then
            clearallBookingSessions()
            Dim strRequestId As String = txtTempRefNo.Text.Trim.Replace("TP/", "")
            Dim isBooking As String = objclsUtilities.ExecuteQueryReturnStringValue("select count(*)cnt from booking_headertemp(nolock) where requestid='" + strRequestId.Trim + "'")
            If isBooking > 0 Then
                Session("sRequestId") = strRequestId
                Session("sEditRequestId") = strRequestId
                Response.Redirect("MoreServices.aspx")

            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, "Invalid Booking Reference.")
            End If
      
        Else
            MessageBox.ShowMessage(Page, MessageType.Warning, "Invalid Booking Reference.")
        End If
    End Sub

#Region "Protected Sub lbItinerary_Click(ByVal sender As Object, ByVal e As System.EventArgs)"
    Protected Sub lbItinerary_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            Dim requestid As String = lblRequestId.Text.Trim
            If requestid <> "" Then
                Dim loginType As String = CType(Session("sLoginType"), String)
                Dim loginUser As String = CType(Session("GlobalUserName"), String)
                Dim g As Guid = Guid.NewGuid()
                Dim tokenId As String = Convert.ToBase64String(g.ToByteArray())
                tokenId = tokenId.Replace("=", "")
                tokenId = tokenId.Replace("+", "")
                tokenId = Date.Now.ToString("yyyyMMddHHmmssfffffff") + tokenId
                Dim finalTokenId As String = objBLLCommonFuntions.fnItineraryTokenGeneration(tokenId, requestid, loginType, loginUser)
                If finalTokenId <> "" Then
                    Dim urlName As String = ConfigurationManager.AppSettings("ItineraryUrlName").ToString
                    Dim encryptPassword As String = ConfigurationManager.AppSettings("ItineraryEncryptPassword").ToString
                    Dim strEncodeUrl As String = System.Web.HttpUtility.UrlEncode(objBLLCommonFuntions.fnEncryption(finalTokenId, encryptPassword))
                    'Dim strurl1 As String = objBLLCommonFuntions.fnDecryption(HttpUtility.UrlDecode(strEncodeUrl), encryptPassword)
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "javascript:redirectItinerary('" + urlName + "','" + strEncodeUrl + "');", True)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: btnPrintItinerary_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
#End Region

    Protected Sub lbQPayEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lbEdit As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbEdit.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            Dim lblColumbusRef As Label = CType(gvRow.FindControl("lblColumbusRef"), Label)

            If lblColumbusRef.Text.Contains("RP/") Or lblColumbusRef.Text.Contains("RG/") Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Quote has been converted to booking.")
                Exit Sub
            End If


            If lblRequestId.Text <> "" Then
                hdRequestId.Value = lblRequestId.Text

                'Dim objBLLMyAccounts As New BLLMyAccount
                'Dim strRateChange As String = objBLLMyAccounts.CheckQuoteRateChange(lblRequestId.Text)
                'If strRateChange = "3" Then
                '    MessageBox.ShowMessage(Page, MessageType.Warning, "The rate plans in the quote are not valid now so this quote cannot be converted.")
                '    Exit Sub
                'End If
                'If strRateChange = "2" Then

                '    If Session("sLoginType") <> "RO" Then
                '        MessageBox.ShowMessage(Page, MessageType.Warning, "The prices are manually overridden. Please contact RO to convert this quote.")
                '        Exit Sub
                '    End If

                '    Dim strMsg As String = "The prices are manually overridden. Please check the prices once again and proceed."
                '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmRateChange('" + lblRequestId.Text + "','" + strMsg + "');", True)
                '    Exit Sub
                'End If
                'If strRateChange = "1" Then
                '    Dim strMsg As String = "The prices have changed, do you want to proceed?."
                '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmRateChange('" + lblRequestId.Text + "','" + strMsg + "');", True)
                '    Exit Sub
                'End If

                Dim dt As DataTable
                dt = objclsUtilities.GetDataFromDataTable("exec sp_validate_booking_quotation_edit  '" + lblRequestId.Text + "'")
                Dim strMsg As String = ""
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        strMsg = strMsg & dt.Rows(i)("warning").ToString & "\n"
                    Next
                    strMsg = strMsg & " Do you want to proceed?"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "script", "fnConfirmRateChange('" + lblRequestId.Text + "','" + strMsg + "');", True)
                    Exit Sub
                End If

                btnQuoteEdit_Click()

            End If
            '    End If

            'End If


        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbQPayEdit_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbQPayPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbPrint As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType(lbPrint.NamingContainer, GridViewRow)
            Dim lblRequestId As Label = CType(gvRow.FindControl("lblRequestId"), Label)
            If lblRequestId.Text <> "" Then
                Dim strpop As String
                Dim ResParam As New ReservationParameters
                ResParam = Session("sobjResParam")
                Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + lblRequestId.Text.Trim + "'")
                If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                If chkCumulative.Trim() = "CUMULATIVE" And ResParam.LoginType = "RO" Then
                    strpop = "window.open('PrintPage.aspx?printId=QuoteCostingExcel&quoteId=" & lblRequestId.Text.Trim & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupQCosting", strpop, True)
                End If
                '   strpop = "window.open('PrintPage.aspx?RequestId=" & lblRequestId.Text.Trim & "&printId=bookingConfirmation');"
                strpop = "window.open('PrintPage.aspx?printID=bookingQuote&quoteId=" & lblRequestId.Text.Trim & "');"
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup", strpop, True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MyAccount.aspx :: lbQPayPrint_Click :: " & ex.StackTrace.ToString & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub gvSearchResultsPayments_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSearchResultsPayments.PageIndexChanging
        gvSearchResultsPayments.PageIndex = e.NewPageIndex
        BindSearchResults_ForPaging()
        If Not Session("sobjBLLMyAccount") Is Nothing Then
            Dim objMyAccount As New BLLMyAccount
            objMyAccount = Session("sobjBLLMyAccount")
            objMyAccount.GridPageIndex = e.NewPageIndex
            Session("sobjBLLMyAccount") = objMyAccount
        End If
    End Sub

End Class

