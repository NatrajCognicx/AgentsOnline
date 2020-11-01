Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System

Partial Class OtherSearch
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLTourSearch As New BLLTourSearch
    Dim objBLLOtherSearch As New BLLOtherSearch
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Dim objUtil As New clsUtils
    Dim objResParam As New ReservationParameters
    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '  Session("selectedotherdatatable") = Nothing

            Try

                If Session("sDSOtherSearchResults") IsNot Nothing Then
                    'changed by shahul on 12/02/2018
                    txtSearchTour.Enabled = False
                    btnotherTextSearch.Enabled = False
                    ddlSorting.Enabled = False
                End If

                Session("State") = "New"
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
                If hdWhiteLabel.Value = "1" Then
                    lblNoOfUnits.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(lblNoOfUnits.ClientID, String) + "','" + CType(txtwlUnitPrice.ClientID, String) + "','" + CType(txtwlUnitSaleValue.ClientID, String) + "' )")
                Else
                    lblNoOfUnits.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(lblNoOfUnits.ClientID, String) + "','" + CType(txtUnitPrice.ClientID, String) + "','" + CType(lblUnitSaleValue.ClientID, String) + "' )")
                End If


            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("OtherSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub

    'Protected Overrides Sub OnInit(ByVal e As EventArgs)
    '    MyBase.OnInit(e)

    'End Sub
    Private Sub LoadRoomAdultChild()
        'Dim strRequestId As String = ""
        'If Not Session("sRequestId") Is Nothing Then
        '    strRequestId = Session("sRequestId")
        '    Dim dtDetails As DataTable
        '    dtDetails = objBLLCommonFuntions.GetRoomAdultAndChildDetails(strRequestId)
        '    If dtDetails.Rows.Count > 0 Then
        '        FillSpecifiedAdultChild(dtDetails.Rows(0)("adults").ToString, dtDetails.Rows(0)("child").ToString)
        '        If dtDetails.Rows(0)("child").ToString > 0 Then
        '            ''' Added 01/06/17 shahul
        '            Dim childages As String = dtDetails.Rows(0)("childages").ToString.Replace(",", ";")
        '            ''''
        '            FillSpecifiedChildAges(childages)
        '            'FillSpecifiedChildAges(dtDetails.Rows(0)("childages").ToString)
        '        End If
        '    Else
        '        FillSpecifiedAdultChild("16", "8")
        '    End If
        'Else
        '    FillSpecifiedAdultChild("16", "8")
        'End If

        ' Above part commented asper Arun request on 04/06/2017. No need to restrict adult and child based on other booking.
        FillSpecifiedAdultChild("20", "6")

    End Sub
    Private Sub BindOtherdetails()
        Dim strQuery As String = ""
        Dim objDALHotelSearch As New DALHotelSearch
        Dim objBLLHotelSearch = New BLLHotelSearch
        objBLLHotelSearch = Session("sobjBLLHotelSearchActive")

        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

            txtothCustomercode.Text = objBLLHotelSearch.CustomerCode
            strQuery = "select agentname from agentmast where active=1 and agentcode='" & objBLLHotelSearch.AgentCode & "'"
            txtothCustomer.Text = objBLLHotelSearch.Customer
            txtothSourceCountry.Text = objBLLHotelSearch.SourceCountry
            txtothSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode


        End If
    End Sub
    Private Sub Amendheaderfill()
        Dim dt As DataTable
        Dim dtpax As DataTable
        Dim strQuery As String = ""
        Try

          dt = objBLLOtherSearch.GetEditBookingDetails(Session("sEditRequestId"), Request.QueryString("OLineNo"))
            If dt.Rows.Count > 0 Then

                txtothFromDate.Text = dt.Rows(0)("fromdate").ToString
                txtothToDate.Text = dt.Rows(0)("todate").ToString
                txtothgroup.Text = dt.Rows(0)("groupname").ToString
                txtothgroupcode.Text = dt.Rows(0)("groupcode").ToString

                txtothSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                txtothCustomer.Text = dt.Rows(0)("agentname").ToString
                txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString
                chkothoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)


                'strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sEditRequestId") & "')"
                'dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                '  If dtpax.Rows.Count > 0 Then
                ddlTourAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)

                If Val(dt.Rows(0)("child").ToString) <> 0 Then
                    ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString

                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtTourChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtTourChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtTourChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtTourChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtTourChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtTourChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtTourChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtTourChild8.Text = strChildAges(7)
                    End If
                End If

                ' End If



                Othersearch()
                BindHotelCheckInAndCheckOutHiddenfield()
                Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                Dim javaScriptChldrn As String = "<script type='text/javascript'>CallToDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)
                If Session("sLoginType") = "RO" Then

                    txtothSourceCountry.Enabled = False
                    txtothCustomer.Enabled = False

                End If

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: AmendHeaderFille :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Sub NewHeaderFill()

        Dim strrequestid As String = ""
        Dim strQuery As String = ""
        Dim dt As New DataTable
        Dim dtpax As New DataTable
        Try



            If Not Session("sobjBLLOtherSearch") Is Nothing Then
                objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)

                txtothFromDate.Text = objBLLOtherSearch.FromDate
                txtothToDate.Text = objBLLOtherSearch.ToDate
                txtothgroup.Text = objBLLOtherSearch.SelectGroup
                txtothgroupcode.Text = objBLLOtherSearch.SelectGroupCode

                txtothSourceCountry.Text = objBLLOtherSearch.SourceCountry
                txtothSourceCountryCode.Text = objBLLOtherSearch.SourceCountryCode

                txtothCustomer.Text = objBLLOtherSearch.Customer
                txtothCustomercode.Text = objBLLOtherSearch.CustomerCode

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

                ddlTourAdult.SelectedValue = objBLLOtherSearch.Adult
                ddlTourChildren.SelectedValue = objBLLOtherSearch.Children
                txtTourChild1.Text = objBLLOtherSearch.Child1
                txtTourChild2.Text = objBLLOtherSearch.Child2
                txtTourChild3.Text = objBLLOtherSearch.Child3
                txtTourChild4.Text = objBLLOtherSearch.Child4
                txtTourChild5.Text = objBLLOtherSearch.Child5
                txtTourChild6.Text = objBLLOtherSearch.Child6
                txtTourChild7.Text = objBLLOtherSearch.Child7
                txtTourChild8.Text = objBLLOtherSearch.Child8

                If objBLLOtherSearch.Children <> "0" Then
                    Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                    Dim javaScriptChldrn As String = "<script type='text/javascript'>ShowTourChild();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)
                End If


                If objBLLOtherSearch.OverridePrice = "1" Then
                    chkothoverride.Checked = True
                Else
                    chkothoverride.Checked = False
                End If

                objBLLOtherSearch.AmendRequestid = GetExistingRequestId()
                objBLLOtherSearch.AmendLineno = ViewState("Olineno")

                BindSearchResults()

                BindHotelCheckInAndCheckOutHiddenfield()
                If Not Page.Request.UrlReferrer Is Nothing Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    If previousPage.Contains("MoreServices.aspx") Then
                        BindTourChildAge()
                    End If
                End If
            Else
                Dim objBLLCommonFuntions = New BLLCommonFuntions

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
                If dt.Rows.Count > 0 Then

                    txtothFromDate.Text = dt.Rows(0)("mindate_").ToString
                    txtothToDate.Text = dt.Rows(0)("maxdate_").ToString
                    txtothgroup.Text = "" ' dt.Rows(0)("groupname").ToString
                    txtothgroupcode.Text = "" 'dt.Rows(0)("groupcode").ToString

                    txtothSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                    txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                    txtothCustomer.Text = dt.Rows(0)("agentname").ToString
                    txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString
                    '  chkothoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                    strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sEditRequestId") & "')"
                    dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                    If dtpax.Rows.Count > 0 Then
                        ddlTourAdult.SelectedValue = Val(dtpax.Rows(0)("adults").ToString)

                        If Val(dtpax.Rows(0)("child").ToString) <> 0 Then
                            ddlTourChildren.SelectedValue = dtpax.Rows(0)("child").ToString

                            Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                            If Left(childages, 1) = ";" Then
                                childages = Right(childages, (childages.Length - 1))
                            End If
                            Dim strChildAges As String() = childages.ToString.Split(";")
                            '''''''

                            If strChildAges.Length <> 0 Then
                                txtTourChild1.Text = strChildAges(0)
                            End If

                            If strChildAges.Length > 1 Then
                                txtTourChild2.Text = strChildAges(1)
                            End If
                            If strChildAges.Length > 2 Then
                                txtTourChild3.Text = strChildAges(2)
                            End If
                            If strChildAges.Length > 3 Then
                                txtTourChild4.Text = strChildAges(3)
                            End If
                            If strChildAges.Length > 4 Then
                                txtTourChild5.Text = strChildAges(4)
                            End If
                            If strChildAges.Length > 5 Then
                                txtTourChild6.Text = strChildAges(5)
                            End If
                            If strChildAges.Length > 6 Then
                                txtTourChild7.Text = strChildAges(6)
                            End If
                            If strChildAges.Length > 7 Then
                                txtTourChild8.Text = strChildAges(7)
                            End If
                        End If

                    End If



                  

                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: NewHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
        objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        LoadRoomAdultChild()
        LoadFields()
        CreateSelectedTourDataTable()
        BindOtherdetails()
        ShowMyBooking()
        hdnLineno.Value = Request.QueryString("OLineNo")
        ViewState("Olineno") = Request.QueryString("OLineNo")

        objBLLOtherSearch = New BLLOtherSearch
        Dim dt As DataTable
        If Not Session("sEditRequestId") Is Nothing Then

            If ViewState("Olineno") Is Nothing Then
                NewHeaderFill()
            Else
                Amendheaderfill()
            End If
        Else
            If Not Session("sobjBLLOtherSearch") Is Nothing Then

                If ViewState("Olineno") Is Nothing Then
                    NewHeaderFill()

                Else
                    EditHeaderFill()

                End If
            Else
                BindHotelCheckInAndCheckOutHiddenfield()
                If Not Page.Request.UrlReferrer Is Nothing Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    If previousPage.Contains("MoreServices.aspx") Then
                        BindTourChildAge()
                    End If
                End If
            End If
        End If
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If
    End Sub
    Private Sub EditHeaderFill()
        Try
            Dim strQuery As String = ""
            Dim chksector As String = ""
            Dim trftype As String = ""
            Dim dt As New DataTable

            Dim strrequestid As String = ""
            Dim dtpax As DataTable


            objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)

            strrequestid = GetExistingRequestId()

            dt = objBLLOtherSearch.GetEditBookingDetails(strrequestid, Request.QueryString("OLineNo"))

            If dt.Rows.Count > 0 Then

                txtothFromDate.Text = dt.Rows(0)("fromdate").ToString
                txtothToDate.Text = dt.Rows(0)("todate").ToString
                txtothgroup.Text = dt.Rows(0)("groupname").ToString
                txtothgroupcode.Text = dt.Rows(0)("groupcode").ToString

                txtothSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                txtothCustomer.Text = dt.Rows(0)("agentname").ToString
                txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString
                chkothoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                ' ''' To Fill Adult  & Child Shahul 07/04/18
                ' ''' 
                'strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & strrequestid & "')"
                'dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                'If dtpax.Rows.Count > 0 Then

                ddlTourAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                If Val(dt.Rows(0)("child").ToString) <> 0 Then
                    ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString

                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtTourChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtTourChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtTourChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtTourChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtTourChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtTourChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtTourChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtTourChild8.Text = strChildAges(7)
                    End If
                End If
                '  End If




                Othersearch()
                BindHotelCheckInAndCheckOutHiddenfield()
                Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                Dim javaScriptChldrn As String = "<script type='text/javascript'>CallToDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)

                If Session("sLoginType") = "RO" Then

                    txtothSourceCountry.Enabled = False
                    txtothCustomer.Enabled = False

                End If


            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx ::EditHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetOtherservicegroup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim TourStartingFrom As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = ""
            Dim objBLLHotelSearch = New BLLHotelSearch
            If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
                objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")
                Dim dt As DataTable

                dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(objBLLHotelSearch.OBrequestid)
                'If dt.Rows.Count > 0 Then
                '    strSqlQry = "select othgrpcode,othgrpname  from  othgrpmast o,booking_hotel_detailtemp d,partymast p,sectormaster s where o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and d.requestid='" & objBLLHotelSearch.OBrequestid & "' order by o.othtypname "
                'Else
                strSqlQry = "select othgrpcode,othgrpname  from  othgrpmast where active=1 and othgrpcode not in (select othgrpcode from view_system_othgrp) and  othgrpname like '" & prefixText & "%' order by othgrpname "
                '  End If
            Else
                strSqlQry = "select othgrpcode,othgrpname  from  othgrpmast where active=1 and othgrpcode not in (select othgrpcode from view_system_othgrp) and  othgrpname like '" & prefixText & "%' order by othgrpname "
            End If


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othgrpname").ToString(), myDS.Tables(0).Rows(i)("othgrpcode").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
        End Try

    End Function

    Private Sub BindTourChildAge()
        Dim objBLLHotelSearch = New BLLHotelSearch
        Dim dtpax As DataTable
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
            Dim strQuery = "select top 1 checkin,checkout,adults,child,childages,(select distinct reqoverride from booking_headertemp where requestid='" & objBLLHotelSearch.OBrequestid & "')reqoverride from   booking_hotel_detailtemp where requestid='" & objBLLHotelSearch.OBrequestid & "'"
            Dim dt As DataTable
            dt = objBLLHotelSearch.GetResultAsDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                hdTab.Value = "1"

                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & objBLLHotelSearch.OBrequestid & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 Then

                    ddlTourAdult.SelectedValue = dtpax.Rows(0)("adults").ToString
                    ddlTourChildren.SelectedValue = dtpax.Rows(0)("child").ToString

                    Dim strChildAges As String() = dtpax.Rows(0)("childages").ToString.Split(";")
                    IIf(dtpax.Rows(0)("child").ToString = "1", txtTourChild1.Text = objBLLHotelSearch.Child1, txtTourChild1.Text = txtTourChild1.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "2", txtTourChild2.Text = objBLLHotelSearch.Child2, txtTourChild2.Text = txtTourChild2.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "3", txtTourChild3.Text = objBLLHotelSearch.Child3, txtTourChild3.Text = txtTourChild3.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "4", txtTourChild4.Text = objBLLHotelSearch.Child4, txtTourChild4.Text = txtTourChild4.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "5", txtTourChild5.Text = objBLLHotelSearch.Child5, txtTourChild5.Text = txtTourChild5.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "6", txtTourChild6.Text = objBLLHotelSearch.Child6, txtTourChild6.Text = txtTourChild6.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "7", txtTourChild7.Text = objBLLHotelSearch.Child7, txtTourChild7.Text = txtTourChild7.Text)
                    IIf(dtpax.Rows(0)("child").ToString = "8", txtTourChild8.Text = objBLLHotelSearch.Child8, txtTourChild8.Text = txtTourChild8.Text)

                    'ddlTourAdult.Enabled = False
                    'ddlTourChildren.Enabled = False
                    'ddlTourChild1.Enabled = False
                    'ddlTourChild2.Enabled = False
                    'ddlTourChild3.Enabled = False
                    'ddlTourChild4.Enabled = False
                    'ddlTourChild5.Enabled = False
                    'ddlTourChild6.Enabled = False
                    'ddlTourChild7.Enabled = False
                    'ddlTourChild8.Enabled = False
                Else
                    ddlTourAdult.SelectedValue = dt.Rows(0)("adults").ToString
                    ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString

                    If dt.Rows(0)("child").ToString <> "0" Then
                        Dim strChildAges As String() = dt.Rows(0)("childages").ToString.Split(";")
                        IIf(dt.Rows(0)("child").ToString = "1", txtTourChild1.Text = objBLLHotelSearch.Child1, txtTourChild1.Text = txtTourChild1.Text)
                        IIf(dt.Rows(0)("child").ToString = "2", txtTourChild2.Text = objBLLHotelSearch.Child2, txtTourChild2.Text = txtTourChild2.Text)
                        IIf(dt.Rows(0)("child").ToString = "3", txtTourChild3.Text = objBLLHotelSearch.Child3, txtTourChild3.Text = txtTourChild3.Text)
                        IIf(dt.Rows(0)("child").ToString = "4", txtTourChild4.Text = objBLLHotelSearch.Child4, txtTourChild4.Text = txtTourChild4.Text)
                        IIf(dt.Rows(0)("child").ToString = "5", txtTourChild5.Text = objBLLHotelSearch.Child5, txtTourChild5.Text = txtTourChild5.Text)
                        IIf(dt.Rows(0)("child").ToString = "6", txtTourChild6.Text = objBLLHotelSearch.Child6, txtTourChild6.Text = txtTourChild6.Text)
                        IIf(dt.Rows(0)("child").ToString = "7", txtTourChild7.Text = objBLLHotelSearch.Child7, txtTourChild7.Text = txtTourChild7.Text)
                        IIf(dt.Rows(0)("child").ToString = "8", txtTourChild8.Text = objBLLHotelSearch.Child8, txtTourChild8.Text = txtTourChild8.Text)

                        ddlTourAdult.Enabled = False
                        ddlTourChildren.Enabled = False
                        txtTourChild1.Enabled = False
                        txtTourChild2.Enabled = False
                        txtTourChild3.Enabled = False
                        txtTourChild4.Enabled = False
                        txtTourChild5.Enabled = False
                        txtTourChild6.Enabled = False
                        txtTourChild7.Enabled = False
                        txtTourChild8.Enabled = False


                        'If dt.Rows(0)("child").ToString <> "0" Then
                        '    Dim scriptKeyChldrn As String = "UniqueKeyForThisScript11"
                        '    Dim javaScriptChldrn As String = "<script type='text/javascript'>ShowTourChild();</script>"
                        '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)
                        'End If
                        If dt.Rows(0)("reqoverride").ToString = "1" Then
                            chkothoverride.Checked = True
                        Else
                            chkothoverride.Checked = False
                        End If

                    End If
                End If
            Else

                ddlTourAdult.Enabled = True
                ddlTourChildren.Enabled = True
                txtTourChild1.Enabled = True
                txtTourChild2.Enabled = True
                txtTourChild3.Enabled = True
                txtTourChild4.Enabled = True
                txtTourChild5.Enabled = True
                txtTourChild6.Enabled = True
                txtTourChild7.Enabled = True
                txtTourChild8.Enabled = True

                chkothoverride.Checked = False

            End If
            Else

                ddlTourAdult.Enabled = True
                ddlTourChildren.Enabled = True
            txtTourChild1.Enabled = True
            txtTourChild2.Enabled = True
            txtTourChild3.Enabled = True
            txtTourChild4.Enabled = True
            txtTourChild5.Enabled = True
            txtTourChild6.Enabled = True
            txtTourChild7.Enabled = True
            txtTourChild8.Enabled = True
                chkothoverride.Checked = False
            End If



    End Sub
    Private Sub BindHotelCheckInAndCheckOutHiddenfield()
        Dim objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sRequestId") Is Nothing Then
            Dim strRequestId = GetExistingRequestId()
            Dim dt As DataTable
            dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(strRequestId)
            If dt.Rows.Count > 0 Then
                hdCheckInPrevDay.Value = dt.Rows(0)("CheckInPrevDay").ToString
                'hdCheckInNextDay.Value = dt.Rows(0)("CheckInNextDay").ToString
                ' hdCheckOutPrevDay.Value = dt.Rows(0)("CheckOutPrevDay").ToString
                hdCheckOutNextDay.Value = dt.Rows(0)("CheckOutNextDay").ToString

                txtothFromDate.Text = dt.Rows(0)("CheckIn").ToString
                txtothToDate.Text = dt.Rows(0)("CheckOut").ToString


                If Session("sLoginType") = "RO" Then
                    'txtTourCustomerCode.Text = objBLLHotelSearch.CustomerCode
                    'txtTourCustomer.Text = objBLLHotelSearch.Customer

                    'txtTourSourceCountry.Text = objBLLHotelSearch.SourceCountry
                    'txtTourSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode

                End If

            Else
                hdCheckInPrevDay.Value = "0"
                hdCheckOutNextDay.Value = "0"
            End If
        Else
            hdCheckInPrevDay.Value = "0"
            hdCheckOutNextDay.Value = "0"

        End If
    End Sub


     Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo

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
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            If Session("sLoginType") <> "RO" Then
                divothcustomer.Visible = False
                divothoverride.Visible = False
                Dim strSqlQry As String = ""
                Dim myDS As New DataSet
                Dim Hotelnames As New List(Of String)
                Try



                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' order by ctryname"

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count = 1 Then
                        txtothSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtothSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtothSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtothSourceCountry.Enabled = False
                    Else
                        txtothSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtothSourceCountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else
                divothcustomer.Visible = True
                divothoverride.Visible = True
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub



    Protected Sub btnFilter_Click(sender As Object, e As System.EventArgs) Handles btnFilter.Click
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSOtherSearchResults")
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

        'Dim strScript As String = "javascript: CallPriceSlider();"
        'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)


    End Sub



    Protected Sub ddlSorting_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSorting.SelectedIndexChanged
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSOtherSearchResults")
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: ddlSorting_SelectedIndexChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub Page_Changed(sender As Object, e As EventArgs)
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSOtherSearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sOtherPageIndex") = pageIndex.ToString
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Sub BindTourMainDetailsWithFilter(ByVal dsSearchResults As DataSet)

        If dsSearchResults.Tables.Count > 0 Then
            If dsSearchResults.Tables(0).Rows.Count > 0 Then

                'changed by mohamed on 12/02/2018
                Dim dtMainDetails1 As DataTable = dsSearchResults.Tables(0).Copy
                Dim dtMainDetailsRet As DataTable, dtMainDetailsMiddle As DataTable

                dtMainDetails1.Columns.Add("CustomSortHelp", Type.GetType("System.Int64"), "2")
                Dim dvMaiDetails As DataView = New DataView(dtMainDetails1) ' New DataView(dsSearchResults.Tables(0))

                ' Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
                ' Filter HotelStars *****************
                Dim strNotSelectedHotelStar As String = ""
                If chkHotelStars.Items.Count > 0 Then
                    For Each chkitem As ListItem In chkHotelStars.Items
                        If chkitem.Selected = False Then
                            If strNotSelectedHotelStar = "" Then
                                strNotSelectedHotelStar = "'" & chkitem.Value & "'"
                            Else
                                strNotSelectedHotelStar = strNotSelectedHotelStar & "," & "'" & chkitem.Value & "'"
                            End If

                        End If
                    Next
                End If

                Dim strNotSelectedClassification As String = ""
                If chkRoomClassification.Items.Count > 0 Then
                    For Each chkitem As ListItem In chkRoomClassification.Items
                        If chkitem.Selected = False Then
                            If strNotSelectedClassification = "" Then
                                strNotSelectedClassification = "'" & chkitem.Value & "'"
                            Else
                                strNotSelectedClassification = strNotSelectedClassification & "," & "'" & chkitem.Value & "'"
                            End If

                        End If
                    Next
                End If


                ' Filter for Price *****************
                Dim strFilterCriteria As String = ""
                If strNotSelectedHotelStar <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "othgrpcode NOT IN (" & strNotSelectedHotelStar & ")"
                    Else
                        strFilterCriteria = " othgrpcode NOT IN (" & strNotSelectedHotelStar & ")"
                    End If
                End If

                'If strNotSelectedClassification <> "" Then
                '    If strFilterCriteria <> "" Then
                '        strFilterCriteria = strFilterCriteria & " AND " & "classificationcode NOT IN (" & strNotSelectedClassification & ")"
                '    Else
                '        strFilterCriteria = " classificationcode NOT IN (" & strNotSelectedClassification & ")"
                '    End If
                'End If


                If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
                    Else
                        strFilterCriteria = "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
                    End If
                End If

                'changed by mohamed on 12/02/2018
                Dim strFilterCriteriaSearchTour As String = ""
                Dim lsTourSearchOrder As String = ""
                If txtSearchTour.Text <> "" Then
                    lsTourSearchOrder = "CustomSortHelp, "
                    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " othtypname like ('" & txtSearchTour.Text & "%')"
                End If

                If strFilterCriteria & strFilterCriteriaSearchTour <> "" Then
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                End If


                'If strFilterCriteria <> "" Then
                '    dvMaiDetails.RowFilter = strFilterCriteria
                'End If


                'Search Text in Middle
                If txtSearchTour.Text <> "" Then
                    dtMainDetailsRet = dvMaiDetails.ToTable.Copy
                    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " othtypname like ('%" & txtSearchTour.Text & "%') and othtypname not like ('" & txtSearchTour.Text & "%')"
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                    dtMainDetailsMiddle = dvMaiDetails.ToTable.Copy
                    dtMainDetailsMiddle.Columns("CustomSortHelp").Expression = "3"
                    dtMainDetailsRet.Merge(dtMainDetailsMiddle)
                    'dvMaiDetails = Nothing
                    dvMaiDetails = New DataView(dtMainDetailsRet)
                End If



                If ddlSorting.Text = "Name" Then
                    'dvMaiDetails.Sort = "othtypname ASC"
                    dvMaiDetails.Sort = lsTourSearchOrder & " othtypname ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    'dvMaiDetails.Sort = "unitsalevalue ASC"
                    dvMaiDetails.Sort = lsTourSearchOrder & " unitsalevalue ASC"
                ElseIf ddlSorting.Text = "0" Then
                    '  dvMaiDetails.Sort = "unitsalevalue ASC"
                    dvMaiDetails.Sort = lsTourSearchOrder & " unitsalevalue ASC"
                    'ElseIf ddlSorting.Text = "Rating" Then
                    '    dvMaiDetails.Sort = "othgrpcode DESC,othtypname ASC "
                End If


                Dim recordCount As Integer = dvMaiDetails.Count

                BindTourMainDetails(dvMaiDetails)
                Me.PopulatePager(recordCount)

            End If
        Else
            dlTourSearchResults.DataBind()
        End If
    End Sub
    Private Sub PopulatePager(ByVal recordCount As Integer)
        Dim currentPage As Integer = 1
        If Not Session("sOtherPageIndex") Is Nothing Then
            currentPage = Session("sOtherPageIndex")
        End If

        Dim dblPageCount As Double = CDbl(CDec(recordCount) / Convert.ToDecimal(PageSize))
        Dim pageCount As Integer = CInt(Math.Ceiling(dblPageCount))
        Dim pages As New List(Of ListItem)()
        If pageCount > 0 Then
            For i As Integer = 1 To pageCount
                pages.Add(New ListItem(i.ToString(), i.ToString(), i <> currentPage))
            Next
        End If

        rptPager.DataSource = pages
        rptPager.DataBind()
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
            Session.Clear()
            Session.Abandon()
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
   <System.Web.Services.WebMethod()> _
    Public Shared Function GetotherCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

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
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & contextKey.Trim & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    Else
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    End If
                Else
                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"

                End If


            Else
                strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"


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

    Protected Sub lbReadMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
            Dim lblExcText As Label = CType(dlItem.FindControl("lblExcText"), Label)
            Dim strText As String = lblExcText.Text
            Dim strToolTip As String = lblExcText.ToolTip
            If myLinkButton.Text = "Read More." Then
                lblExcText.Text = strToolTip
                lblExcText.ToolTip = strText
                myLinkButton.Text = "Read Less."
            Else
                lblExcText.Text = strToolTip
                lblExcText.ToolTip = strText
                myLinkButton.Text = "Read More."
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub BindSearchResults()
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                Dim objBLLHotelSearch As New BLLHotelSearch
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString

                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If

            End If
        End If

        objBLLOtherSearch = New BLLOtherSearch
        If Session("sobjBLLOtherSearch") Is Nothing Then
            Response.Redirect("Home.aspx?Tab=4")
        End If
        objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)

        Dim dsOtherSearchResults As New DataSet
        objBLLOtherSearch.DateChange = "0"
        dsOtherSearchResults = objBLLOtherSearch.GetSearchDetails()

        If dsOtherSearchResults.Tables(0).Rows.Count = 0 Then
            dvhotnoshow.Style.Add("display", "block")
        Else
            dvhotnoshow.Style.Add("display", "none")
        End If

        'dsOtherSearchResults.Tables(0).Columns.Add(New DataColumn("complimentarycust", GetType(String)))
        'dsOtherSearchResults.Tables(0).AcceptChanges()

        Session("sDSOtherSearchResults") = dsOtherSearchResults
        If dsOtherSearchResults.Tables.Count > 0 Then

            BindTourPricefilter(dsOtherSearchResults.Tables(1))
            BindTourHotelStars(dsOtherSearchResults.Tables(2))
            '   BindTourRoomClassification(dsOtherSearchResults.Tables(3))
            Session("sDSOtherSearchResults") = dsOtherSearchResults
            Session("sOtherPageIndex") = "1"
            Dim dvMaiDetails As DataView = New DataView(dsOtherSearchResults.Tables(0))
            If ddlSorting.Text = "Name" Then
                dvMaiDetails.Sort = "othtypname ASC"
            ElseIf ddlSorting.Text = "Price" Then
                dvMaiDetails.Sort = "unitsalevalue ASC"
                'ElseIf ddlSorting.Text = "Rating" Then
                '    dvMaiDetails.Sort = "othgrpcode DESC,othtypname ASC "
            End If
            Dim recordCount As Integer = dvMaiDetails.Count
            BindTourMainDetails(dvMaiDetails)
            Me.PopulatePager(recordCount)
            lblHotelCount.Text = dsOtherSearchResults.Tables(0).Rows.Count & " Records Found"

        Else
            hdPriceMinRange.Value = "0"
            hdPriceMaxRange.Value = "1"
        End If


    End Sub

    Private Sub BindTourPricefilter(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            hdPriceMinRange.Value = IIf(dataTable.Rows(0)("minprice").ToString = "", "0", dataTable.Rows(0)("minprice").ToString)
            hdPriceMaxRange.Value = IIf(dataTable.Rows(0)("maxprice").ToString = "", "0", dataTable.Rows(0)("maxprice").ToString)
        Else
            hdPriceMinRange.Value = "0"
            hdPriceMaxRange.Value = "1"
        End If
    End Sub

    Private Sub BindTourHotelStars(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            chkHotelStars.DataSource = dataTable
            chkHotelStars.DataTextField = "othgrpname"
            chkHotelStars.DataValueField = "othgrpcode"
            chkHotelStars.DataBind()
            If chkHotelStars.Items.Count > 0 Then
                For Each chkitem As ListItem In chkHotelStars.Items
                    chkitem.Selected = True
                Next
            End If
        Else
            chkHotelStars.Items.Clear()
            chkHotelStars.DataBind()
        End If
    End Sub

    Private Sub BindTourRoomClassification(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            chkRoomClassification.DataSource = dataTable
            chkRoomClassification.DataTextField = "classificationname"
            chkRoomClassification.DataValueField = "classificationcode"
            chkRoomClassification.DataBind()
            If chkRoomClassification.Items.Count > 0 Then
                For Each chkitem As ListItem In chkRoomClassification.Items
                    chkitem.Selected = True
                Next
            End If
        Else
            chkRoomClassification.Items.Clear()
            chkRoomClassification.DataBind()
        End If
    End Sub

    Private Sub BindTourMainDetails(ByVal dvMaiDetails As DataView)
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                Dim objBLLHotelSearch As New BLLHotelSearch
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If
        Dim dt As New DataTable
        dt = dvMaiDetails.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then
            lblHotelCount.Text = dt.Rows.Count & " Records Found"
            Dim iPageIndex As Integer = 1
            ' Dim iPageSize As Integer = 0
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sOtherPageIndex") Is Nothing Then
                iPageIndex = Session("sOtherPageIndex")
            End If

            iRowNoFrom = (iPageIndex - 1) * PageSize + 1
            iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
            dv.Table.Columns.Add("rowIndex")
            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("rowIndex") = (i + 1).ToString
            Next

            dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & iRowNoTo

            dlTourSearchResults.DataSource = dv
            dlTourSearchResults.DataBind()

        Else
            dlTourSearchResults.DataBind()
        End If


        Dim dtselectedtour As DataTable = CType(Session("selectedotherdatatable"), DataTable)
        For Each gvRow As DataListItem In dlTourSearchResults.Items
            Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
            Dim chkSelectTour As CheckBox = gvRow.FindControl("chkSelectTour")
            Dim txtTourChangeDate As TextBox = gvRow.FindControl("txtTourChangeDate")
            Dim hdntourdate As HiddenField = gvRow.FindControl("hdntourdate")
            Dim hdExcCode As HiddenField = gvRow.FindControl("hdExcCode")
            Dim hdVehicleCode As HiddenField = gvRow.FindControl("hdVehicleCode")
            Dim lblunits As LinkButton = gvRow.FindControl("lblunits")
            Dim hdncumunits As HiddenField = gvRow.FindControl("hdncumunits")

            If hdnselected.Value = 1 Then
                chkSelectTour.Checked = True
                txtTourChangeDate.Text = Format(CType(hdntourdate.Value, Date), "dd/MM/yyyy")
                lblunits.Text = hdncumunits.Value + " Units"
                For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                    If dtselectedtour.Rows(i)("othtypcode") = hdExcCode.Value.ToString Then
                        dtselectedtour.Rows.RemoveAt(i)
                    End If
                Next

                dtselectedtour.Rows.Add(hdExcCode.Value.ToString, txtTourChangeDate.Text)
            End If

        Next

        Session("selectedotherdatatable") = dtselectedtour

    End Sub

    

    Protected Sub dlTourSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlTourSearchResults.ItemDataBound
        Dim lblunits As LinkButton = CType(e.Item.FindControl("lblunits"), LinkButton)
        Dim hdncumunits As HiddenField = CType(e.Item.FindControl("hdncumunits"), HiddenField)


        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbPrice As LinkButton = CType(e.Item.FindControl("lbPrice"), LinkButton)
            lbPrice.Text = lbPrice.Text.Replace(".000", "")
            Dim hdCurrCode As HiddenField = CType(e.Item.FindControl("hdCurrCode"), HiddenField)
            Dim hdwlCurrCode As HiddenField = CType(e.Item.FindControl("hdwlCurrCode"), HiddenField)
            Dim lbwlPrice As LinkButton = CType(e.Item.FindControl("lbwlPrice"), LinkButton)
            Dim dWlPrice As Double = IIf(lbwlPrice.Text = "", 0, lbwlPrice.Text)
            lbwlPrice.Text = Math.Round(dWlPrice, 2, MidpointRounding.AwayFromZero) & " " & hdwlCurrCode.Value




            Dim chkSelectTour As CheckBox = CType(e.Item.FindControl("chkSelectTour"), CheckBox)
            chkSelectTour.Attributes.Add("onChange", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "')")
            chkSelectTour.Attributes.Add("onclick", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "')")

            If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                lbwlPrice.Visible = True
                lbPrice.Visible = False
                hdSliderCurrency.Value = " " & hdwlCurrCode.Value
            Else
                lbwlPrice.Visible = False
                lbPrice.Visible = True
                hdSliderCurrency.Value = " " & hdCurrCode.Value
            End If

            ' chkSelectTour.Attributes.Add("OnCheckedChanged", "Check_Changed()")

            'Show Hotel Image
            Dim imgExcImage As Image = CType(e.Item.FindControl("imgExcImage"), Image)
            Dim lblExcImage As Label = CType(e.Item.FindControl("lblExcImage"), Label)
            imgExcImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblExcImage.Text & "&Type=4"

            'Show Hotel Stars
            Dim hdNoOfExcStars As HiddenField = CType(e.Item.FindControl("hdNoOfExcStars"), HiddenField)
            Dim dvExcStars As HtmlGenericControl = CType(e.Item.FindControl("dvExcStars"), HtmlGenericControl)
            Dim strExcStarHTML As New StringBuilder

   

            Dim lblExcText As Label = CType(e.Item.FindControl("lblExcText"), Label)
            Dim lbReadMore As LinkButton = CType(e.Item.FindControl("lbReadMore"), LinkButton)
            If lblExcText.Text.Length > 150 Then
                lblExcText.Text = lblExcText.Text.Substring(0, 149)

            Else
                lbReadMore.Visible = False
            End If


        End If

        If iCumulative = 1 Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lbPrice As LinkButton = CType(e.Item.FindControl("lbPrice"), LinkButton)
                lbPrice.Text = lbPrice.Text.Replace(".000", "")
                Dim lblPriceBy As Label = CType(e.Item.FindControl("lblPriceBy"), Label)

                lbPrice.Visible = False
                lblPriceBy.Visible = False
                lblunits.Visible = True
                lblunits.Text = hdncumunits.Value + " Units"
            End If
        Else
            lblunits.Visible = False
        End If
    End Sub

    Protected Sub lbPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
           

            Dim lbPrice As LinkButton = CType(sender, LinkButton)
            '  Dim lblPriceBy As Label = CType(e.Item.FindControl("lblPriceBy"), Label)


            Session("slbOtherTotalSaleValue") = lbPrice
            Dim dlItem As DataListItem = CType((lbPrice).NamingContainer, DataListItem)
            Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
            Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
            Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
            Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
            Dim hdRateBasis As HiddenField = CType(dlItem.FindControl("hdRateBasis"), HiddenField)
            Dim txtTourChangeDate As TextBox = CType(dlItem.FindControl("txtTourChangeDate"), TextBox)
            Dim chkselecttour As CheckBox = CType(dlItem.FindControl("chkselecttour"), CheckBox)



            hdExcCodePopup.Value = hdExcCode.Value
            hdRateBasisPopup.Value = hdRateBasis.Value
            hdCurrCodePopup.Value = hdCurrCode.Value
            If txtTourChangeDate.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Select date for price.")
            Else
                lblTotlaPriceHeading.Text = lblExcName.Text
                objBLLOtherSearch = New BLLOtherSearch
                If Session("sobjBLLOtherSearch") Is Nothing Then
                    Response.Redirect("Home.aspx?Tab=4")
                End If
                objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)
                objBLLOtherSearch.DateChange = "1"
                objBLLOtherSearch.OtherTypeCode = hdExcCode.Value
                '  objBLLOtherSearch.VehicleCode = hdVehicleCode.Value
                objBLLOtherSearch.SelectedDate = txtTourChangeDate.Text
                Dim dsTourPriceResults As New DataSet

                'dsTourPriceResults.Tables(0).Columns.Add(New DataColumn("complimentarycust", GetType(String)))
                'dsTourPriceResults.Tables(0).AcceptChanges()

                If Session("sDSOtherSearchResults") Is Nothing Then
                    dsTourPriceResults = objBLLOtherSearch.GetSearchDetails()

                Else
                    dsTourPriceResults = Session("sDSOtherSearchResults")
                End If

                If dsTourPriceResults.Tables.Count > 0 Then

                    Dim dv As New DataView



                    'Dim dr As DataRow = dsTourPriceResults.Tables(0).Select("othtypcode='" & hdExcCodePopup.Value & "'").First
                    'dr("unitsalevalue") = fTotalSaleValue.ToString.Replace(".00", "").Replace(".0", "")

                    'dv.Table = dsTourPriceResults.Tables(0)
                    'dv.RowFilter =

                    'For I As Integer = 0 To dsTourPriceResults.Tables(0).Rows.Count - 1
                    '    dsTourPriceResults.Tables(0).Rows(I).Item(2) = lbPrice.Text
                    'Next

                    If dsTourPriceResults.Tables(0).Rows.Count > 0 Then

                        If hdRateBasis.Value = "ACS" Then

                        Else
                            dvACS.Visible = False
                            dvUnits.Visible = True
                            lblNoOfAdult.Text = ""
                            lblNoOfchild.Text = ""
                            lblNoOfSeniors.Text = ""
                            lblNoOfUnits.Text = dsTourPriceResults.Tables(0).Rows(0)("units").ToString

                            txtAdultPrice.Text = ""
                            txtChildprice.Text = ""
                            txtSeniorsPrice.Text = ""
                            'txtUnitPrice.Text = dsTourPriceResults.Tables(0).Rows(0)("unitprice").ToString
                            'lblUnitSaleValue.Text = dsTourPriceResults.Tables(0).Rows(0)("unitsalevalue").ToString
                            'chkComplimentaryToCustomer.Checked = IIf(dsTourPriceResults.Tables(0).Rows(0)("unitprice").ToString = "", False, True)

                            If dsTourPriceResults.Tables(0).Rows.Count >= 1 Then
                                Dim dr As DataRow = dsTourPriceResults.Tables(0).Select("othtypcode='" & hdExcCodePopup.Value & "'").First

                                txtUnitPrice.Text = dr("unitprice").ToString 'dsTourPriceResults.Tables(0).Rows(0)("unitprice").ToString
                                lblUnitSaleValue.Text = dr("unitsalevalue").ToString ' dsTourPriceResults.Tables(0).Rows(0)("unitsalevalue").ToString
                                chkComplimentaryToCustomer.Checked = IIf(Val(dr("complimentarycust").ToString) = 0, False, True)

                                Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                                Dim dwlUnitPrice As Decimal
                                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                                dwlUnitPrice = dUnitPrice * dWlMarkup
                                txtwlUnitPrice.Text = dr("wlunitprice").ToString 'dwlUnitPrice 'dr("unitprice").ToString.ToString
                                txtwlUnitSaleValue.Text = dr("wlunitsalevalue").ToString
                            End If

                            lblAdultSaleValue.Text = ""
                            lblchildSaleValue.Text = ""
                            lblSeniorSaleValue.Text = ""


                            If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                                txtwlUnitPrice.Style.Add("display", "none")
                                txtwlUnitSaleValue.Style.Add("display", "none")
                                txtUnitPrice.Style.Add("display", "block")
                                lblUnitSaleValue.Style.Add("display", "block")



                            ElseIf hdWhiteLabel.Value = "1" Then

                                txtwlUnitPrice.Style.Add("display", "block")
                                txtwlUnitSaleValue.Style.Add("display", "block")
                                txtUnitPrice.Style.Add("display", "none")
                                lblUnitSaleValue.Style.Add("display", "none")



                            Else
                                txtUnitPrice.Style.Add("display", "block")
                                lblUnitSaleValue.Style.Add("display", "block")
                                txtwlUnitPrice.Style.Add("display", "none")
                                txtwlUnitSaleValue.Style.Add("display", "none")


                            End If


                        End If

                        'lblNoOfUnits.ReadOnly = True

                        txtUnitPrice.ReadOnly = True
                        txtAdultPrice.ReadOnly = True
                        txtChildprice.ReadOnly = True
                        txtSeniorsPrice.ReadOnly = True



                        If Session("sLoginType") = "RO" Then
                            dvComplimentaryToCustomer.Visible = True
                            If chkothoverride.Checked = True Then
                                txtUnitPrice.ReadOnly = False
                                txtAdultPrice.ReadOnly = False
                                txtChildprice.ReadOnly = False
                                txtSeniorsPrice.ReadOnly = False



                            Else
                                txtUnitPrice.ReadOnly = True
                                txtAdultPrice.ReadOnly = True
                                txtChildprice.ReadOnly = True
                                txtSeniorsPrice.ReadOnly = True


                            End If

                            txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(lblAdultSaleValue.ClientID, String) + "')")
                            txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(lblchildSaleValue.ClientID, String) + "')")
                            txtSeniorsPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfSeniors.ClientID, String) + "', '" + CType(txtSeniorsPrice.ClientID, String) + "' ,'" + CType(lblSeniorSaleValue.ClientID, String) + "')")
                            txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")
                        Else
                            dvComplimentaryToCustomer.Visible = False
                            txtUnitPrice.ReadOnly = True
                            txtAdultPrice.ReadOnly = True
                            txtChildprice.ReadOnly = True
                            txtSeniorsPrice.ReadOnly = True
                        End If
                        If hdBookingEngineRateType.Value = "1" Then

                            dvnoUnits.Style.Add("display", "none")
                            dvsalevalue.Style.Add("display", "none")
                        End If
                        mpTotalprice.Show()
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: lbPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub btnotherTextSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnotherTextSearch.Click
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSOtherSearchResults")
            BindTourMainDetailsWithFilter(dsTourSearchResults)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnotherTextSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbwlPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbwlPrice As LinkButton = CType(sender, LinkButton)
            Session("slbOtherTotalSaleValue") = lbwlPrice
            Dim dlItem As DataListItem = CType((lbwlPrice).NamingContainer, DataListItem)
            Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
            Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
            Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
            Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
            Dim hdRateBasis As HiddenField = CType(dlItem.FindControl("hdRateBasis"), HiddenField)
            Dim txtTourChangeDate As TextBox = CType(dlItem.FindControl("txtTourChangeDate"), TextBox)
            Dim chkselecttour As CheckBox = CType(dlItem.FindControl("chkselecttour"), CheckBox)



            hdExcCodePopup.Value = hdExcCode.Value
            hdRateBasisPopup.Value = hdRateBasis.Value
            hdCurrCodePopup.Value = hdCurrCode.Value
            If txtTourChangeDate.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Select date for price.")
            Else
                lblTotlaPriceHeading.Text = lblExcName.Text
                objBLLOtherSearch = New BLLOtherSearch
                If Session("sobjBLLOtherSearch") Is Nothing Then
                    Response.Redirect("Home.aspx?Tab=4")
                End If
                objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)
                objBLLOtherSearch.DateChange = "1"
                objBLLOtherSearch.OtherTypeCode = hdExcCode.Value
                '  objBLLOtherSearch.VehicleCode = hdVehicleCode.Value
                objBLLOtherSearch.SelectedDate = txtTourChangeDate.Text
                Dim dsTourPriceResults As New DataSet

                'dsTourPriceResults.Tables(0).Columns.Add(New DataColumn("complimentarycust", GetType(String)))
                'dsTourPriceResults.Tables(0).AcceptChanges()

                If Session("sDSOtherSearchResults") Is Nothing Then
                    dsTourPriceResults = objBLLOtherSearch.GetSearchDetails()

                Else
                    dsTourPriceResults = Session("sDSOtherSearchResults")
                End If

                If dsTourPriceResults.Tables.Count > 0 Then

                    Dim dv As New DataView



                    If dsTourPriceResults.Tables(0).Rows.Count > 0 Then

                        If hdRateBasis.Value = "ACS" Then

                        Else
                            dvACS.Visible = False
                            dvUnits.Visible = True
                            lblNoOfAdult.Text = ""
                            lblNoOfchild.Text = ""
                            lblNoOfSeniors.Text = ""
                            lblNoOfUnits.Text = dsTourPriceResults.Tables(0).Rows(0)("units").ToString


                            txtAdultPrice.Text = ""
                            txtChildprice.Text = ""
                            txtSeniorsPrice.Text = ""
                            lblAdultSaleValue.Text = ""
                            lblchildSaleValue.Text = ""
                            lblSeniorSaleValue.Text = ""

                            If dsTourPriceResults.Tables(0).Rows.Count >= 1 Then
                                Dim dr As DataRow = dsTourPriceResults.Tables(0).Select("othtypcode='" & hdExcCodePopup.Value & "'").First

                                txtUnitPrice.Text = dr("unitprice").ToString 'dsTourPriceResults.Tables(0).Rows(0)("unitprice").ToString
                                lblUnitSaleValue.Text = dr("unitsalevalue").ToString ' dsTourPriceResults.Tables(0).Rows(0)("unitsalevalue").ToString
                                chkComplimentaryToCustomer.Checked = IIf(Val(dr("complimentarycust").ToString) = 0, False, True)

                                Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                                Dim dwlUnitPrice As Decimal
                                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                                dwlUnitPrice = dUnitPrice * dWlMarkup
                                txtwlUnitPrice.Text = dr("wlunitprice").ToString 'dwlUnitPrice 'dr("unitprice").ToString.ToString
                                txtwlUnitSaleValue.Text = dr("wlunitsalevalue").ToString

                            End If

                            If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                                txtwlUnitPrice.Style.Add("display", "none")
                                txtwlUnitSaleValue.Style.Add("display", "none")
                                txtUnitPrice.Style.Add("display", "block")
                                lblUnitSaleValue.Style.Add("display", "block")



                            ElseIf hdWhiteLabel.Value = "1" Then

                                txtwlUnitPrice.Style.Add("display", "block")
                                txtwlUnitSaleValue.Style.Add("display", "block")
                                txtUnitPrice.Style.Add("display", "none")
                                lblUnitSaleValue.Style.Add("display", "none")



                            Else
                                txtUnitPrice.Style.Add("display", "block")
                                lblUnitSaleValue.Style.Add("display", "block")
                                txtwlUnitPrice.Style.Add("display", "none")
                                txtwlUnitSaleValue.Style.Add("display", "none")


                            End If



                        End If

                        'lblNoOfUnits.ReadOnly = True

                        txtUnitPrice.ReadOnly = True
                        txtAdultPrice.ReadOnly = True
                        txtChildprice.ReadOnly = True
                        txtSeniorsPrice.ReadOnly = True
                        txtwlUnitPrice.ReadOnly = True



                        If Session("sLoginType") = "RO" Then
                            dvComplimentaryToCustomer.Visible = True
                            If chkothoverride.Checked = True Then
                                txtUnitPrice.ReadOnly = False
                                txtAdultPrice.ReadOnly = False
                                txtChildprice.ReadOnly = False
                                txtSeniorsPrice.ReadOnly = False
                                txtwlUnitPrice.ReadOnly = False



                            Else
                                txtUnitPrice.ReadOnly = True
                                txtAdultPrice.ReadOnly = True
                                txtChildprice.ReadOnly = True
                                txtSeniorsPrice.ReadOnly = True
                                txtwlUnitPrice.ReadOnly = True


                            End If

                            txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(lblAdultSaleValue.ClientID, String) + "')")
                            txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(lblchildSaleValue.ClientID, String) + "')")
                            txtSeniorsPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfSeniors.ClientID, String) + "', '" + CType(txtSeniorsPrice.ClientID, String) + "' ,'" + CType(lblSeniorSaleValue.ClientID, String) + "')")
                            txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")
                        Else
                            dvComplimentaryToCustomer.Visible = False
                            txtUnitPrice.ReadOnly = True
                            txtAdultPrice.ReadOnly = True
                            txtChildprice.ReadOnly = True
                            txtSeniorsPrice.ReadOnly = True
                        End If
                        If hdBookingEngineRateType.Value = "1" Then

                            dvnoUnits.Style.Add("display", "none")
                            dvsalevalue.Style.Add("display", "none")
                        End If
                        mpTotalprice.Show()
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: lbwlPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub CreateSelectedTourDataTable()
        'Dim SelectExcDT As DataTable = New DataTable("SelectedExc")
        'SelectExcDT.Columns.Add("exctypcode", Type.GetType("System.String"))
        'SelectExcDT.Columns.Add("excdate", Type.GetType("System.String"))
        'Session("selectedotherdatatable") = SelectExcDT

        Dim SelectExcDT As DataTable = New DataTable("SelectedExc")
        SelectExcDT.Columns.Add("othtypcode", Type.GetType("System.String"))
        SelectExcDT.Columns.Add("othdate", Type.GetType("System.String"))
        Session("selectedotherdatatable") = SelectExcDT
    End Sub
    Private Sub Othersearch()
        Try
            Dim objBLLTourSearch As New BLLTourSearch
            Dim objBLLOtherSearch As New BLLOtherSearch

            Dim strSearchCriteria As String = ""

            Dim strFromDate As String = txtothFromDate.Text
            Dim strToDate As String = txtothToDate.Text




            Dim strSourceCountry As String = txtothSourceCountry.Text
            Dim strSourceCountryCode As String = txtothSourceCountryCode.Text
            Dim strCustomer As String = txtothCustomer.Text
            Dim strCustomerCode As String = txtothCustomercode.Text


            Dim strAdult As String = ddlTourAdult.SelectedValue
            Dim strChildren As String = ddlTourChildren.SelectedValue
            Dim strChild1 As String = txtTourChild1.Text
            Dim strChild2 As String = txtTourChild2.Text
            Dim strChild3 As String = txtTourChild3.Text
            Dim strChild4 As String = txtTourChild4.Text
            Dim strChild5 As String = txtTourChild5.Text
            Dim strChild6 As String = txtTourChild6.Text
            Dim strChild7 As String = txtTourChild7.Text
            Dim strChild8 As String = txtTourChild8.Text

            Dim strreset As String = hdnreset.Value


            Dim strOveride As String = chkothoverride.Checked



            'If HttpContext.Current.Session("sLoginType") = "RO" Then

            '    If chkothoverride.Checked = True Then
            '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any hotel name. \n </br></br></br></br></br></br></br></br></br>* If override is ticked then Hotel selection is compulsory.")
            '        Exit Sub
            '    End If
            'End If



            If strAdult = "0" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any adult.")
                Exit Sub
            End If
            If strChildren <> "0" Then
                If strChildren = "1" Then
                    If strChild1 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "2" Then
                    If strChild1 = "" Or strChild2 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "3" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "4" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "5" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "6" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "7" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                ElseIf strChildren = "8" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Or strChild8 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                End If

            End If

            If strSourceCountryCode = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any source country.")
                Exit Sub
            End If

            Dim strQueryString As String = ""


            If strFromDate <> "" Then
                objBLLOtherSearch.FromDate = strFromDate
                strSearchCriteria = "FromDate :" & strFromDate
            End If
            If strToDate <> "" Then
                objBLLOtherSearch.ToDate = strToDate
                strSearchCriteria = strSearchCriteria & "||" & "ToDate :" & strToDate
            End If

            ' If strreset <> "" Then
            objBLLOtherSearch.ResetSearch = strreset
            '  End If


            If strAdult <> "" Then
                objBLLOtherSearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "||" & "Adult :" & strAdult
            End If
            If strChildren <> "" Then
                objBLLOtherSearch.Children = strChildren
                strSearchCriteria = strSearchCriteria & "||" & "Children :" & strChildren
                If strChildren = "1" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.ChildAgeString = strChild1
                ElseIf strChildren = "2" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.Child3 = strChild3
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.Child3 = strChild3
                    objBLLOtherSearch.Child4 = strChild4
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.Child3 = strChild3
                    objBLLOtherSearch.Child4 = strChild4
                    objBLLOtherSearch.Child5 = strChild5
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.Child3 = strChild3
                    objBLLOtherSearch.Child4 = strChild4
                    objBLLOtherSearch.Child5 = strChild5
                    objBLLOtherSearch.Child6 = strChild6
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.Child3 = strChild3
                    objBLLOtherSearch.Child4 = strChild4
                    objBLLOtherSearch.Child5 = strChild5
                    objBLLOtherSearch.Child6 = strChild6
                    objBLLOtherSearch.Child7 = strChild7
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7
                ElseIf strChildren = "8" Then
                    objBLLOtherSearch.Child1 = strChild1
                    objBLLOtherSearch.Child2 = strChild2
                    objBLLOtherSearch.Child3 = strChild3
                    objBLLOtherSearch.Child4 = strChild4
                    objBLLOtherSearch.Child5 = strChild5
                    objBLLOtherSearch.Child6 = strChild6
                    objBLLOtherSearch.Child7 = strChild7
                    objBLLOtherSearch.Child8 = strChild8
                    objBLLOtherSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
                End If
            End If
            strSearchCriteria = strSearchCriteria & "||" & "ChildAgeString :" & objBLLOtherSearch.ChildAgeString
            If strSourceCountry <> "" Then
                objBLLOtherSearch.SourceCountry = strSourceCountry
                strSearchCriteria = strSearchCriteria & "||" & "SourceCountry :" & strSourceCountry
            End If
            If strSourceCountryCode <> "" Then
                objBLLOtherSearch.SourceCountryCode = strSourceCountryCode
            End If


            If Not Session("sEditRequestId") Is Nothing Then
                objBLLOtherSearch.AmendRequestid = Session("sEditRequestId")
                objBLLOtherSearch.AmendLineno = ViewState("Olineno")
                strSearchCriteria = strSearchCriteria & "||" & "AmendRequestid :" & objBLLOtherSearch.AmendRequestid
            Else
                objBLLOtherSearch.AmendRequestid = GetExistingRequestId()
                objBLLOtherSearch.AmendLineno = ViewState("Olineno")
                strSearchCriteria = strSearchCriteria & "||" & "AmendLineno :" & objBLLOtherSearch.AmendLineno
            End If

            If strCustomer <> "" Then
                objBLLOtherSearch.Customer = strCustomer
                strSearchCriteria = strSearchCriteria & "||" & "Agent :" & strCustomer
            End If
            If strCustomerCode <> "" Then
                objBLLOtherSearch.CustomerCode = strCustomerCode
            End If
            If txtothgroupcode.Text <> "" Then
                objBLLOtherSearch.SelectGroupCode = txtothgroupcode.Text
            End If
            If txtothgroupcode.Text <> "" Then
                objBLLOtherSearch.SelectGroup = txtothgroup.Text
                strSearchCriteria = strSearchCriteria & "||" & "SERVICE GROUP :" & txtothgroup.Text
            End If





            objBLLOtherSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & "LoginType :" & objBLLOtherSearch.LoginType
            objBLLOtherSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLOtherSearch.CustomerCode, Session("sAgentCode"))
            strSearchCriteria = strSearchCriteria & "||" & "AgentCode :" & objBLLOtherSearch.AgentCode
            objBLLOtherSearch.WebuserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "||" & "WebuserName :" & objBLLOtherSearch.WebuserName
            If chkothoverride.Checked = True Then
                objBLLOtherSearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice :Yes"
            Else
                objBLLOtherSearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice :No"
            End If
            Session("sobjBLLOtherSearch") = objBLLOtherSearch
            Dim dsTourSearchResults As New DataSet
            objBLLOtherSearch.DateChange = "0"


            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                Dim objBLLCommonFuntions As New BLLCommonFuntions()
                '   Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Other Service Search Page", "Other Service Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            dsTourSearchResults = objBLLOtherSearch.GetSearchDetails()

            If dsTourSearchResults.Tables(0).Rows.Count = 0 Then
                dvhotnoshow.Style.Add("display", "block")
            Else
                dvhotnoshow.Style.Add("display", "none")
            End If


            'dsTourSearchResults.Tables(0).Columns.Add(New DataColumn("complimentarycust", GetType(String)))
            'dsTourSearchResults.Tables(0).AcceptChanges()


            Session("sDSOtherSearchResults") = dsTourSearchResults
            If dsTourSearchResults.Tables.Count > 0 Then

                BindTourPricefilter(dsTourSearchResults.Tables(1))
                BindTourHotelStars(dsTourSearchResults.Tables(2))
                ' BindTourRoomClassification(dsTourSearchResults.Tables(3))
                Session("sDSOtherSearchResults") = dsTourSearchResults
                Session("sOtherPageIndex") = "1"
                Dim dvMaiDetails As DataView = New DataView(dsTourSearchResults.Tables(0))
                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "othtypname ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "unitsalevalue ASC"
                    'ElseIf ddlSorting.Text = "Rating" Then
                    '    dvMaiDetails.Sort = "othgrpcode DESC,othtypname ASC "
                End If
                Dim recordCount As Integer = dvMaiDetails.Count
                BindTourMainDetails(dvMaiDetails)
                Me.PopulatePager(recordCount)
                lblHotelCount.Text = dsTourSearchResults.Tables(0).Rows.Count & " Records Found"

            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnOtherSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnTourSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTourSearch.Click
        Try

            'changed by Shahul on 13/02/2018
            txtSearchTour.Text = ""
            txtSearchTour.Enabled = True
            btnotherTextSearch.Enabled = True
            ddlSorting.Enabled = True

            Othersearch()
            btnPageBottom.Focus()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: Othersearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnPriceBreakupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupSave.Click
        Try
           
            Dim fTotalSaleValue As Double = 0
            Dim dwlTotalSaleValue As Double = 0

            If hdRateBasisPopup.Value = "ACS" Then
                fTotalSaleValue = CType(IIf(lblAdultSaleValue.Text = "", "0", lblAdultSaleValue.Text), Double) + CType(IIf(lblchildSaleValue.Text = "", "0", lblchildSaleValue.Text), Double) + CType(IIf(lblSeniorSaleValue.Text = "", "0", lblSeniorSaleValue.Text), Double)
            Else
                fTotalSaleValue = IIf(lblUnitSaleValue.Text = "", "0", lblUnitSaleValue.Text)
                dwlTotalSaleValue = IIf(txtwlUnitSaleValue.Text = "", "0", txtwlUnitSaleValue.Text)
            End If
            Dim ds As DataSet
            ds = Session("sDSOtherSearchResults")

            If ds.Tables(0).Rows.Count >= 1 Then
                Dim dr As DataRow = ds.Tables(0).Select("othtypcode='" & hdExcCodePopup.Value & "'").First
                dr("unitsalevalue") = fTotalSaleValue.ToString.Replace(".00", "").Replace(".0", "")
                dr("units") = lblNoOfUnits.Text.Replace(".00", "").Replace(".0", "")
                dr("unitprice") = txtUnitPrice.Text.ToString.Replace(".000", "").Replace(".0", "")
                dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)


                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                dr("wlunitprice") = Math.Round(dr("unitprice") * dWlMarkup, 2) ' txtUnitPrice.Text.ToString.Replace(".000", "").Replace(".0", "")
                dr("wlunitsalevalue") = Math.Round((Math.Round(dr("unitprice") * dWlMarkup, 2)) * Val(lblNoOfUnits.Text))
                dr("unitsalevalue") = Math.Round((Math.Round(dr("unitprice"), 2)) * Val(lblNoOfUnits.Text))

                dr("unitcostvalue") = Math.Round((Math.Round(dr("unitcprice"), 2)) * Val(lblNoOfUnits.Text))
                '   dr("wltotalsalevalue") = dwlTotalSaleValue
            End If



            Session("sDSOtherSearchResults") = ds
            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbOtherTotalSaleValue"), LinkButton)

            Dim dlItem As DataListItem = CType((lbTotalPrice).NamingContainer, DataListItem)
            Dim lbPrice As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lbPrice"), LinkButton)
            Dim lbwlPrice As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lbwlPrice"), LinkButton)

            If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                lbPrice.Text = fTotalSaleValue.ToString() & " " & hdCurrCodePopup.Value
            ElseIf hdWhiteLabel.Value = "1" Then
                lbwlPrice.Text = dwlTotalSaleValue.ToString() & " " & hdCurrCodePopup.Value
            Else
                lbPrice.Text = fTotalSaleValue.ToString() & " " & hdCurrCodePopup.Value
            End If


            Dim lblunits As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lblunits"), LinkButton)
            lblunits.Text = lblNoOfUnits.Text + " Units"

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnPriceBreakupSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Shared Function GetNewOrExistingRequestId() As String
        Dim strRequestId As String = ""
        Dim objBLLHotelSearch2 As New BLLHotelSearch

        strRequestId = GetExistingRequestId()
        If strRequestId = "" Then
            strRequestId = objBLLHotelSearch2.getrequestid()
        End If
        Return strRequestId
    End Function
    Private Shared Function GetExistingRequestId() As String
        Dim strRequestId As String = ""
        If HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = ""
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            strRequestId = HttpContext.Current.Session("sRequestId")
            ' strRequestId = objBLLCommonFuntions.GetExistingBookingRequestId(strRequestId)
        End If
        Return strRequestId
    End Function
    Private Function validatetimelimit() As String

        Dim strQuery As String = ""

        Dim othtimelimit As String = ""
        strQuery = "select option_selected  from reservation_parameters(nolock) where param_id='1148'"
        othtimelimit = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        For Each gvrow As DataListItem In dlTourSearchResults.Items

            Dim chkSelectTour As CheckBox = CType(gvrow.FindControl("chkSelectTour"), CheckBox)
            Dim hdExcCode As HiddenField = CType(gvrow.FindControl("hdExcCode"), HiddenField)
            Dim txttourchangedate As TextBox = CType(gvrow.FindControl("txttourchangedate"), TextBox)
            Dim dtselectedtour As DataTable = CType(Session("selectedotherdatatable"), DataTable)


            If chkSelectTour.Checked = True Then
                strQuery = "select isnull(timelimitcheck,0)  from othtypmast(nolock) where othtypcode='" & hdExcCode.Value & "'"
                Dim othtimelimitchq As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                Dim othdate As Date = DateAdd(DateInterval.Hour, (Val(othtimelimit) * -1), CType(txttourchangedate.Text, Date))
                Dim checkindate As Date
                ' If hdCheckInPrevDay.Value = "" Then
                checkindate = CType(txttourchangedate.Text, Date)
                'Else
                '    checkindate = DateAdd(DateInterval.Day, 1, CType(hdCheckInPrevDay.Value, Date)) ' hdCheckInPrevDay.Value
                'End If
                If DateDiff(DateInterval.Day, Now.Date, CType(txttourchangedate.Text, Date)) <= 3 And othtimelimitchq = "1" Then
                    'If DateDiff(DateInterval.Day, CType(txtothFromDate.Text, Date), CType(othdate, Date)) < 0 Then
                    chkSelectTour.Checked = False
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Selected Other Services to Book 72 hours Prior Arrival .")
                    Return False
                    Exit Function
                End If
            End If

        Next

        Dim dtselectedtour1 As DataTable = CType(Session("selectedotherdatatable"), DataTable)

        If dtselectedtour1.Rows.Count > 1 And Not ViewState("Olineno") Is Nothing Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Amend/Edit Option Please Select One Tour ")
            Return False
            Exit Function
        End If

        Return True
    End Function
    Protected Sub btnbooknow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbooknow.Click
        Try


            If validatetimelimit() Then




                objBLLTourSearch = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
                objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)
                Dim objBLLHotelSearch = New BLLHotelSearch


                Dim requestid As String = ""
                requestid = GetNewOrExistingRequestId()




                Dim dt As DataTable
                Dim objBLLCommonFuntions As New BLLCommonFuntions

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
                If dt.Rows.Count > 0 Then

                    objBLLOtherSearch.AgentCode = dt.Rows(0)("agentcode").ToString
                    objBLLOtherSearch.OBDiv_Code = dt.Rows(0)("div_code").ToString
                    objBLLOtherSearch.OBRequestId = requestid
                    objBLLOtherSearch.OBSourcectryCode = dt.Rows(0)("sourcectrycode").ToString
                    objBLLOtherSearch.OBAgentCode = IIf(Session("sLoginType") = "RO", objBLLOtherSearch.CustomerCode, dt.Rows(0)("agentcode").ToString)

                    objBLLOtherSearch.OBReqoverRide = IIf(chkothoverride.Checked = True, "1", "0")
                    objBLLOtherSearch.OBAgentref = dt.Rows(0)("agentref").ToString
                    objBLLOtherSearch.OBColumbusRef = dt.Rows(0)("ColumbusRef").ToString
                    objBLLOtherSearch.OBRemarks = dt.Rows(0)("remarks").ToString
                    objBLLOtherSearch.UserLogged = Session("GlobalUserName")

                Else

                    objBLLOtherSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLOtherSearch.CustomerCode, Session("sAgentCode"))
                    objBLLOtherSearch.OBDiv_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast where agentcode='" & Session("sAgentCode") & "'")
                    objBLLOtherSearch.OBRequestId = requestid
                    objBLLOtherSearch.OBSourcectryCode = CType(txtothSourceCountryCode.Text, String)
                    objBLLOtherSearch.OBReqoverRide = IIf(chkothoverride.Checked, "1", "0")
                    objBLLOtherSearch.OBAgentref = ""
                    objBLLOtherSearch.OBColumbusRef = ""
                    objBLLOtherSearch.OBRemarks = ""
                    objBLLOtherSearch.UserLogged = Session("GlobalUserName")

                End If

                Dim dsTourSearchResults As New DataSet
                dsTourSearchResults = CType(Session("sDSOtherSearchResults"), DataSet)
                If dsTourSearchResults IsNot Nothing Then
                    Dim dv As DataView
                    dv = New DataView(dsTourSearchResults.Tables(0))
                    Dim dtselectedtour As DataTable = CType(Session("selectedotherdatatable"), DataTable)

                    If dtselectedtour.Rows.Count = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any service.")
                        Exit Sub
                    End If

                    For i As Integer = 0 To dtselectedtour.Rows.Count - 1
                        If dtselectedtour.Rows(i)("othdate").ToString = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select date.")
                            Exit Sub
                        End If
                    Next

                    Dim lsStrQry As String = ""
                    Dim strOlineno As String = ""
                    For Each ltRow As DataRow In dtselectedtour.Rows
                        lsStrQry = IIf(lsStrQry = "", "", lsStrQry + " or ") & "othtypcode='" & ltRow(0) & "'"

                    Next
                    If lsStrQry.Trim <> "" Then
                        dv.RowFilter = lsStrQry
                        Dim dtResult As DataTable = dv.ToTable
                        Dim rownum As Integer = 1
                        Dim strselectedrow As String = ""
                        Dim strBuffer As New Text.StringBuilder
                        '   strBuffer.Append("<DocumentElement>")
                        Dim rlineonostring As String = ""
                        If dtselectedtour.Rows.Count >= 1 Then
                            strselectedrow = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "OTHERS")
                        End If

                        If dtResult.Rows.Count > 0 Then

                            For Each myrow As DataRow In dtResult.Rows

                                If ViewState("Olineno") Is Nothing Then
                                    If strOlineno = "" Then
                                        strOlineno = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "OTHERS")
                                    Else
                                        strOlineno = CType(strOlineno, Integer) + 1
                                    End If


                                Else
                                    If Not ViewState("Olineno") Is Nothing Then
                                        strOlineno = ViewState("Olineno")
                                    Else
                                        strOlineno = strselectedrow
                                    End If

                                End If

                                'If ViewState("Olineno") Is Nothing Then
                                '    If strOlineno = "" Then
                                '        strOlineno = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "OTHERS")
                                '    Else
                                '        strOlineno = CType(strOlineno, Integer) + 1
                                '    End If

                                'Else
                                '    strOlineno = ViewState("Olineno")
                                'End If



                                If rlineonostring <> "" Then
                                    rlineonostring = rlineonostring + ";" + CType(strOlineno, String)
                                Else
                                    rlineonostring = strOlineno
                                End If


                                ' strBuffer.Append("<Table>")
                                strBuffer.Append("<DocumentElement><Table>")
                                strBuffer.Append(" <olineno>" & strOlineno & "</olineno>")
                                strBuffer.Append("<othtypcode>" & myrow("othtypcode").ToString & "</othtypcode>")
                                strBuffer.Append("<othgrpcode>" & myrow("othgrpcode").ToString & "</othgrpcode>")
                                Dim excdate As String = ""
                                For Each row As DataRow In dtselectedtour.Rows
                                    If row("othtypcode").ToString = myrow("othtypcode").ToString Then
                                        excdate = row("othdate").ToString
                                        If excdate <> "" Then
                                            Dim strDates As String() = excdate.Split("/")
                                            excdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                        End If

                                        Exit For

                                    End If
                                Next
                                If excdate.ToString = "" Or excdate.ToString = "yyyy/MM/dd" Then
                                    Dim strFromDate As String = txtothFromDate.Text
                                    If strFromDate <> "" Then
                                        Dim strDates As String() = excdate.Split("/")
                                        strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                    End If

                                    strBuffer.Append("<othdate>" & Format(CType(strFromDate, Date), "yyyy/MM/dd") & "</othdate>")
                                Else
                                    strBuffer.Append("<othdate>" & Format(CType(excdate, Date), "yyyy/MM/dd") & "</othdate>")
                                End If


                                strBuffer.Append("<adults>" & CType(ddlTourAdult.SelectedValue, String) & "</adults>")
                                strBuffer.Append("<child>" & CType(ddlTourChildren.SelectedValue, String) & "</child>")

                                Dim objBLLOtherSearch As New BLLOtherSearch
                                objBLLOtherSearch = CType(Session("sobjBLLOtherSearch"), BLLOtherSearch)
                                strBuffer.Append("<childages>" & objBLLOtherSearch.ChildAgeString & "</childages>")

                                strBuffer.Append("<units>" & myrow("units").ToString & "</units>")

                                strBuffer.Append("<unitprice>" & myrow("unitprice").ToString & "</unitprice>")

                                strBuffer.Append("<unitsalevalue>" & myrow("unitsalevalue").ToString & "</unitsalevalue>")
                                strBuffer.Append("<wlunitsalevalue>" & myrow("wlunitsalevalue").ToString & "</wlunitsalevalue>")
                                strBuffer.Append("<complimentarycust>" & Val(myrow("complimentarycust").ToString) & "</complimentarycust>")
                                strBuffer.Append("<overrideprice>" & IIf(chkothoverride.Checked = True, 1, 0) & "</overrideprice>")
                                strBuffer.Append("<oplistcode>" & myrow("oplistcode").ToString & "</oplistcode>")

                                ''' added shahul 28/09/17 white label fields
                                strBuffer.Append("<preferredsupplier>" & myrow("preferredsupplier").ToString & "</preferredsupplier>")
                                strBuffer.Append("<unitcprice>" & myrow("unitcprice").ToString & "</unitcprice>")
                                strBuffer.Append("<unitcostvalue>" & myrow("unitcostvalue").ToString & "</unitcostvalue>")
                                strBuffer.Append("<ocplistcode>" & myrow("ocplistcode").ToString & "</ocplistcode>")
                                strBuffer.Append("<wlunitprice>" & myrow("wlunitprice").ToString & "</wlunitprice>")
                                strBuffer.Append("<wlcurrcode>" & myrow("wlcurrcode").ToString & "</wlcurrcode>")
                                strBuffer.Append("<wlconvrate>" & myrow("wlconvrate").ToString & "</wlconvrate>")
                                strBuffer.Append("<wlmarkupperc>" & myrow("wlmarkupperc").ToString & "</wlmarkupperc>")



                                ' Added by abin on 20180602 *****************************
                                strBuffer.Append("<PriceVATPerc>" & myrow("PriceVATPerc").ToString & "</PriceVATPerc>")
                                strBuffer.Append("<PriceWithTax>" & myrow("PriceWithTax").ToString & "</PriceWithTax>")
                                strBuffer.Append("<CostVATPerc>" & myrow("CostVATPerc").ToString & "</CostVATPerc>")
                                strBuffer.Append("<CostWithTax>" & myrow("CostWithTax").ToString & "</CostWithTax>")
                                Dim dtotalSalevalue As Decimal
                                Dim dtotalTv As Decimal
                                Dim dtotalVAT As Decimal
                                dtotalSalevalue = Convert.ToDecimal(IIf(myrow("unitsalevalue") Is DBNull.Value, "0", (Val(myrow("unitsalevalue").ToString)).ToString))
                                If myrow("PriceWithTax").ToString = "1" Then
                                    dtotalTv = Math.Round(dtotalSalevalue / (1 + (Convert.ToDecimal(myrow("PriceVATPerc").ToString) / 100)), 3)
                                    dtotalVAT = Math.Round(dtotalSalevalue - dtotalTv, 3)
                                Else
                                    dtotalTv = Math.Round(dtotalSalevalue, 3)
                                    dtotalVAT = Math.Round(dtotalSalevalue * (Convert.ToDecimal(myrow("PriceVATPerc").ToString) / 100), 3)
                                End If

                                Dim dtotalCostvalue As Decimal
                                Dim dtotalCostTv As Decimal
                                Dim dtotalCostVAT As Decimal
                                dtotalCostvalue = Convert.ToDecimal(IIf(myrow("unitcostvalue") Is DBNull.Value, "0", (Val(myrow("unitcostvalue").ToString)).ToString))
                                If myrow("CostWithTax").ToString = "1" Then
                                    dtotalCostTv = Math.Round(dtotalCostvalue / (1 + (Convert.ToDecimal(myrow("CostVATPerc").ToString) / 100)), 3)
                                    dtotalCostVAT = Math.Round(dtotalCostvalue - dtotalCostTv, 3)
                                Else
                                    dtotalCostTv = Math.Round(dtotalCostvalue, 3)
                                    dtotalCostVAT = Math.Round(dtotalCostvalue * (Convert.ToDecimal(myrow("CostVATPerc").ToString) / 100), 3)
                                End If


                                strBuffer.Append("<PriceTaxableValue>" & dtotalTv.ToString & "</PriceTaxableValue>")
                                strBuffer.Append("<PriceVATValue>" & dtotalVAT.ToString & "</PriceVATValue>")
                                strBuffer.Append("<CostTaxableValue>" & dtotalCostTv.ToString & "</CostTaxableValue>")
                                strBuffer.Append("<CostVATValue>" & dtotalCostVAT.ToString & "</CostVATValue>")
                                '***************end

                                ' strBuffer.Append("</Table>")
                                strBuffer.Append("</Table></DocumentElement>")
                                If myrow("currentselection").ToString <> "1" Then
                                    strselectedrow = CType(strselectedrow, Integer) + 1
                                End If
                                rownum = rownum + 1
                            Next
                            '   strBuffer.Append("</DocumentElement>")
                            objBLLOtherSearch.OBOtherXml = strBuffer.ToString()
                            objBLLOtherSearch.UserLogged = Session("GlobalUserName")
                            objBLLOtherSearch.OBRlinenoString = rlineonostring

                            If Not Session("sobjResParam") Is Nothing Then
                                objResParam = Session("sobjResParam")
                                objBLLOtherSearch.SubUserCode = objResParam.SubUserCode
                            End If
                        Else
                            GoTo errNotSaved
                        End If
                    Else
                        GoTo errNotSaved
                    End If
                Else
                    GoTo errNotSaved
                End If

errNotSaved:
                If objBLLOtherSearch.SaveOtherserviceBookingInTemp() Then
                    ' MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
                    Session("sRequestId") = objBLLOtherSearch.OBRequestId
                    Session("sobjBLLOtherSearchActive") = objBLLOtherSearch
                    'Response.Redirect("~\GuestPage.aspx")
                    Response.Redirect("~\MoreServices.aspx")
                End If
            Else
                Exit Sub
            End If
            '  Exit Sub
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnbooknow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Protected Sub btnSelectedTourChecknox_Click(sender As Object, e As System.EventArgs) Handles btnSelectedTourChecknox.Click
        Try

            'Dim mycheckbox As CheckBox = CType(sender, CheckBox)
            'Dim dlItem As DataListItem = CType((mycheckbox).NamingContainer, DataListItem)

            ''SelectExcDT.AllowDBNull = False
            ''SelectExcDT.Unique = True
            'If mycheckbox.Checked Then
            '    
            'End If
            Dim strQuery As String = ""
            Dim othtimelimit As String = ""
            strQuery = "select option_selected  from reservation_parameters(nolock) where param_id='1148'"
            othtimelimit = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)


            Dim iValue As Integer = hddlRowNumber.Value

            Dim dlItem As DataListItem = dlTourSearchResults.Items(iValue)
            Dim chkSelectTour As CheckBox = CType(dlItem.FindControl("chkSelectTour"), CheckBox)
            Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
            Dim txttourchangedate As TextBox = CType(dlItem.FindControl("txttourchangedate"), TextBox)
            Dim dtselectedtour As DataTable = CType(Session("selectedotherdatatable"), DataTable)

            strQuery = "select isnull(timelimitcheck,0)  from othtypmast(nolock) where othtypcode='" & hdExcCode.Value & "'"
            Dim othtimelimitchq As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)


            If chkSelectTour.Checked Then
                If txttourchangedate.Text = "" Then
                    For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                        If dtselectedtour.Rows(i)(0) = hdExcCode.Value.ToString Then
                            dtselectedtour.Rows.RemoveAt(i)
                        End If
                    Next
                    chkSelectTour.Checked = False
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select date.")
                    Exit Sub
                Else

                    'Dim othdate As Date = DateAdd(DateInterval.Hour, (Val(othtimelimit) * -1), CType(txttourchangedate.Text, Date))

                    Dim excdate As String = ""
                    ' checkindate = CType(txttourchangedate.Text, Date)

                    Dim strDates As String() = txttourchangedate.Text.Split("/")
                    excdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                    If DateDiff(DateInterval.Day, Now.Date, CType(excdate, Date)) <= 3 And othtimelimitchq = "1" Then
                        'If DateDiff(DateInterval.Day, CType(txtothFromDate.Text, Date), CType(othdate, Date)) < 0 Then
                        chkSelectTour.Checked = False
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Selected Other Services to Book 72 hours Prior Arrival .")
                        Exit Sub
                    End If


                End If


                If Not Request.QueryString("OLineNo") Is Nothing Then
                    For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                        If dtselectedtour.Rows(i)("othtypcode") = hdExcCode.Value.ToString Then
                            dtselectedtour.Rows.RemoveAt(i)
                        End If
                    Next
                End If

                dtselectedtour.Rows.Add(hdExcCode.Value.ToString, txttourchangedate.Text)

            Else
                For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                    If dtselectedtour.Rows(i)(0) = hdExcCode.Value.ToString Then
                        dtselectedtour.Rows.RemoveAt(i)
                    End If
                Next
            End If
            Session("selectedotherdatatable") = dtselectedtour

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnSelectedTourChecknox_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    <WebMethod()> _
    Public Shared Function GetCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
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
                strSqlQry = "select agentcode,agentname  from agentmast where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
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
    Private Function GetRequestId() As String
        Dim strRequestId As String = ""
        Dim objBLLHotelSearch2 As New BLLHotelSearch
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch2 = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            strRequestId = objBLLHotelSearch2.OBrequestid
        ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
            Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
            objBLLTourSearch = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
            strRequestId = objBLLTourSearch.EbRequestID
        ElseIf Not Session("sobjBLLTransferSearchActive") Is Nothing Then
            Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
            objBLLTransferSearch = CType(Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
            strRequestId = objBLLTransferSearch.OBRequestId
        ElseIf Not HttpContext.Current.Session("sobjBLLMASearchActive") Is Nothing Then
            Dim objBLLMASearch As BLLMASearch = New BLLMASearch
            objBLLMASearch = CType(HttpContext.Current.Session("sobjBLLMASearchActive"), BLLMASearch)
            strRequestId = objBLLMASearch.OBRequestId
        ElseIf Not Session("sobjBLLOtherSearchActive") Is Nothing Then
            Dim objBLLOtherSearch As BLLOtherSearch = New BLLOtherSearch
            objBLLOtherSearch = CType(Session("sobjBLLOtherSearchActive"), BLLOtherSearch)
            strRequestId = objBLLOtherSearch.OBRequestId
        End If
        Return strRequestId
    End Function

    'Private Sub LoadRoomAdultChild()
    '    Dim strRequestId As String = ""
    '    Dim strQuery As String = ""
    '    Dim objBLLHotelSearch1 As New BLLHotelSearch
    '    Dim dt As DataTable
    '    If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
    '        Dim objBLLHotelSearch_ = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
    '        strRequestId = objBLLHotelSearch_.OBrequestid
    '        strQuery = "select noofrooms,adults,child,childages from booking_hotel_detailtemp where requestid='" & strRequestId & "' and rlineno=1"
    '        dt = objBLLHotelSearch1.GetResultAsDataTable(strQuery)
    '        If dt.Rows.Count > 0 Then

    '            FillSpecifiedAdultChild(dt.Rows(0)("adults").ToString, dt.Rows(0)("child").ToString)

    '            If dt.Rows(0)("child").ToString > 0 Then
    '                FillSpecifiedChildAges(dt.Rows(0)("childages").ToString)
    '            End If
    '        Else
    '            FillSpecifiedAdultChild("16", "8")

    '        End If
    '    ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
    '        Dim BLLTourSearch_ = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
    '        strRequestId = BLLTourSearch_.EbRequestID
    '        strQuery = "select adults,child,childages from booking_tourstemp where requestid='" & strRequestId & "' and elineno=1"
    '        dt = objBLLHotelSearch1.GetResultAsDataTable(strQuery)
    '        If dt.Rows.Count > 0 Then
    '            FillSpecifiedAdultChild(dt.Rows(0)("adults").ToString, dt.Rows(0)("child").ToString)

    '            If dt.Rows(0)("child").ToString > 0 Then
    '                FillSpecifiedChildAges(dt.Rows(0)("childages").ToString)
    '            End If
    '        Else
    '            FillSpecifiedAdultChild("16", "8")

    '        End If
    '    ElseIf Not Session("sobjBLLTransferSearchActive") Is Nothing Then
    '        Dim BLLTourSearch_ = CType(Session("sobjBLLTransferSearchActive"), BLLTourSearch)
    '        strRequestId = BLLTourSearch_.EbRequestID
    '        strQuery = "select adults,child,childagestring from booking_transferstemp where requestid='" & strRequestId & "' and tlineno=1"
    '        dt = objBLLHotelSearch1.GetResultAsDataTable(strQuery)
    '        If dt.Rows.Count > 0 Then
    '            FillSpecifiedAdultChild(dt.Rows(0)("adults").ToString, dt.Rows(0)("child").ToString)

    '            If dt.Rows(0)("child").ToString > 0 Then
    '                FillSpecifiedChildAges(dt.Rows(0)("childagestring").ToString)
    '            End If
    '        Else
    '            FillSpecifiedAdultChild("16", "8")
    '        End If
    '    ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
    '        Dim BLLTourSearch_ = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
    '        strRequestId = BLLTourSearch_.EbRequestID
    '        strQuery = "select adults,child,childages from booking_tourstemp where requestid='" & strRequestId & "' and tlineno=1"
    '        dt = objBLLHotelSearch1.GetResultAsDataTable(strQuery)
    '        If dt.Rows.Count > 0 Then
    '            FillSpecifiedAdultChild(dt.Rows(0)("adults").ToString, dt.Rows(0)("child").ToString)

    '            If dt.Rows(0)("child").ToString > 0 Then
    '                FillSpecifiedChildAges(dt.Rows(0)("childages").ToString)
    '            End If
    '        Else
    '            FillSpecifiedAdultChild("16", "8")
    '        End If
    '    ElseIf Not Session("sobjBLLOtherSearchActive") Is Nothing Then
    '        Dim BLLOtherSearch_ = CType(Session("sobjBLLOtherSearchActive"), BLLOtherSearch)
    '        strRequestId = BLLOtherSearch_.OBRequestId
    '        strQuery = "select adults,child,childages from booking_tourstemp where requestid='" & strRequestId & "' and tlineno=1"
    '        dt = objBLLHotelSearch1.GetResultAsDataTable(strQuery)
    '        If dt.Rows.Count > 0 Then
    '            FillSpecifiedAdultChild(dt.Rows(0)("adults").ToString, dt.Rows(0)("child").ToString)

    '            If dt.Rows(0)("child").ToString > 0 Then
    '                FillSpecifiedChildAges(dt.Rows(0)("childages").ToString)
    '            End If
    '        Else
    '            FillSpecifiedAdultChild("16", "8")
    '        End If
    '    Else
    '        FillSpecifiedAdultChild("16", "8")
    '    End If

    'End Sub

    Private Sub FillSpecifiedChildAges(childages As String)
        ' objclsUtilities.FillDropDownListWithSpecifiedAges(txtTourChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild8, childages)

    End Sub

    Private Sub FillSpecifiedAdultChild(adults As String, child As String)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourChildren, child)
    End Sub
    Protected Sub btnMyBooking_Click(sender As Object, e As System.EventArgs) Handles btnMyBooking.Click
        Try
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
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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

                txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtothCustomer.Text = dt.Rows(0)("agentname").ToString
                txtothSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                ''' Added 01/06/17 shahul
                Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                If Left(childages, 1) = ";" Then
                    childages = Right(childages, (childages.Length - 1))
                End If
                Dim childagestring As String() = childages.ToString.Split(";")
                '''''''

                '  Dim childagestring As String() = dt.Rows(0)("childages").ToString.ToString.Split(";")

                ddlTourAdult.SelectedValue = dt.Rows(0)("adults").ToString
                ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString

                If childagestring.Length <> 0 Then
                    txtTourChild1.Text = childagestring(0)
                End If

                If childagestring.Length > 1 Then
                    txtTourChild2.Text = childagestring(1)
                End If
                If childagestring.Length > 2 Then
                    txtTourChild3.Text = childagestring(2)
                End If
                If childagestring.Length > 3 Then
                    txtTourChild4.Text = childagestring(3)
                End If
                If childagestring.Length > 4 Then
                    txtTourChild5.Text = childagestring(4)
                End If
                If childagestring.Length > 5 Then
                    txtTourChild6.Text = childagestring(5)
                End If
                If childagestring.Length > 6 Then
                    txtTourChild7.Text = childagestring(6)
                End If
                If childagestring.Length > 7 Then
                    txtTourChild8.Text = childagestring(7)
                End If

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
            objclsUtilities.WriteErrorLog("OtherSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnMyAccount_Click(sender As Object, e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
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
