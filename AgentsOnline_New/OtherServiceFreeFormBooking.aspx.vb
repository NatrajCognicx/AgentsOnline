Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System

Partial Class OtherServiceFreeFormBooking
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '  Session("selectedotherdatatable") = Nothing

            Try



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

                If Not Session("sFinalBooked") Is Nothing Then
                    clearallBookingSessions()
                    Session("sFinalBooked") = Nothing '"0" 'changed by mohamed on 12/08/2018

                End If

                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
                LoadHome()


            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub

    'Protected Overrides Sub OnInit(ByVal e As EventArgs)
    '    MyBase.OnInit(e)

    'End Sub
    Private Sub LoadRoomAdultChild()


        ' Above part commented asper Arun request on 04/06/2017. No need to restrict adult and child based on other booking.
        FillSpecifiedAdultChild("1000", "6")

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

                txtothgroup.Text = dt.Rows(0)("groupname").ToString
                txtothgroupcode.Text = dt.Rows(0)("groupcode").ToString

                txtothSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                txtothCustomer.Text = dt.Rows(0)("agentname").ToString
                txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString



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
                txtServiceName.Text = dt.Rows(0)("othtypname").ToString
                txtServiceNameCode.Text = dt.Rows(0)("othtypcode").ToString
                txtNoOfUnit.Text = dt.Rows(0)("units").ToString
                txtUnitPrice.Text = dt.Rows(0)("unitprice").ToString
                txtTotal.Text = dt.Rows(0)("unitsalevalue").ToString

                txtCostPricePax.Text = dt.Rows(0)("unitcprice").ToString
                txtCostPricePaxTotal.Text = dt.Rows(0)("unitcostvalue").ToString


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
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: AmendHeaderFille :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub

    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        LoadRoomAdultChild()
        LoadFields()
        CreateSelectedTourDataTable()
        BindOtherdetails()
        ShowMyBooking()
        hdnLineno.Value = Request.QueryString("OLineNo")
        ViewState("Olineno") = Request.QueryString("OLineNo")

        If Not Session("sRequestId") Is Nothing Then
            Dim dtBookingHeader As DataTable
            dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dtBookingHeader.Rows.Count > 0 Then
                txtothSourceCountry.Text = dtBookingHeader.Rows(0)("sourcectryname").ToString
                txtothSourceCountryCode.Text = dtBookingHeader.Rows(0)("sourcectrycode").ToString
                txtothCustomer.Text = dtBookingHeader.Rows(0)("agentname").ToString
                txtothCustomercode.Text = dtBookingHeader.Rows(0)("agentcode").ToString
                If Session("sLoginType") = "RO" Then
                    txtothCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtothSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtothCustomer.Enabled = False
                    AutoCompleteExtender_txtothSourceCountry.Enabled = False
                End If
            End If
        End If

        '*** Danny 22/10/2018 FreeForm SupplierAgent
        Dim ds_ As New DataSet
        ds_ = objclsUtilities.GetDataSet("SP_SelectDefaultSupplierAgent", Nothing)
        If Not ds_ Is Nothing Then
            If ds_.Tables(0).Rows.Count > 0 Then
                Txt_SupplierAgentCode.Text = ds_.Tables(0).Rows(0)("supagentcode").ToString
                Txt_SupplierAgentName.Text = ds_.Tables(0).Rows(0)("supagentname").ToString
            End If
        End If
        Session("strDefaultCurrency") = objclsUtilities.ExecuteQueryReturnSingleValue("select isnull(option_selected,0) from reservation_parameters where param_id =457")

        If Not Session("strDefaultCurrency") Is Nothing Then
            If Session("strDefaultCurrency").ToString.Trim.Length > 0 Then
                lblCostPrice.Text = lblCostPrice.Text.Replace("---", Session("strDefaultCurrency").ToString.Trim)
                lblCostTotal.Text = lblCostTotal.Text.Replace("---", Session("strDefaultCurrency").ToString.Trim)
            End If
        End If

        If Not Session("sRequestId") Is Nothing Then

            If Not ViewState("Olineno") Is Nothing Then
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



            strrequestid = GetExistingRequestId()

            Dim objBLLOtherServiceFreeFormBooking As New BLLOtherServiceFreeFormBooking

            dt = objBLLOtherServiceFreeFormBooking.GetOtherServiceDatas(strrequestid, Request.QueryString("OLineNo"))

            If dt.Rows.Count > 0 Then

                txtothFromDate.Text = dt.Rows(0)("othdate").ToString

                txtothgroup.Text = dt.Rows(0)("othgrpname").ToString
                txtothgroupcode.Text = dt.Rows(0)("othgrpcode").ToString

                txtothSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                txtothCustomer.Text = dt.Rows(0)("agentname").ToString
                txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString


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

                txtServiceName.Text = dt.Rows(0)("othtypname").ToString
                txtServiceNameCode.Text = dt.Rows(0)("othtypcode").ToString
                txtNoOfUnit.Text = dt.Rows(0)("units").ToString
                txtUnitPrice.Text = dt.Rows(0)("unitprice").ToString
                txtTotal.Text = dt.Rows(0)("unitsalevalue").ToString

                txtCostPricePax.Text = dt.Rows(0)("unitcprice").ToString
                txtCostPricePaxTotal.Text = dt.Rows(0)("unitcostvalue").ToString

                If dt.Rows(0)("complimentarycust").ToString = "1" Then
                    chkComplSup.Checked = True
                Else
                    chkComplSup.Checked = False

                End If

                BindHotelCheckInAndCheckOutHiddenfield()
                Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                Dim javaScriptChldrn As String = "<script type='text/javascript'>CallToDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)

                If Session("sLoginType") = "RO" Then

                    txtothSourceCountry.Enabled = False
                    txtothCustomer.Enabled = False

                End If

                If dt.Rows(0)("SupplierAgentCode").ToString.Trim().Length > 0 Then
                    Txt_SupplierAgentCode.Text = dt.Rows(0)("SupplierAgentCode").ToString.Trim()
                    Txt_SupplierAgentName.Text = dt.Rows(0)("supagentname").ToString.Trim()
                End If
                If dt.Rows(0)("preferredsupplier").ToString.Trim().Length > 0 Then
                    Txt_SupplierCode.Text = dt.Rows(0)("preferredsupplier").ToString.Trim()
                    Txt_SupplierName.Text = dt.Rows(0)("SupplierName").ToString.Trim()
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
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetOtherServiceName(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim lstOtherService As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = ""
            Dim objBLLHotelSearch = New BLLHotelSearch

            strSqlQry = "select othtypcode,othtypname from othtypmast(nolock) where active=1 and othgrpcode not in (select othgrpcode from view_system_othgrp) and  othgrpcode='" & contextKey & "' and  othtypname like '" & prefixText & "%' order by othtypname "


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    lstOtherService.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othtypname").ToString(), myDS.Tables(0).Rows(i)("othtypcode").ToString()))
                Next

            End If

            Return lstOtherService
        Catch ex As Exception
            Return lstOtherService
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
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                lblPhoneNo.Text = objDataTable.Rows(0)("tel1").ToString '*** Danny 06/09/2018
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
                divothcustomer.Visible = False
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

            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub



    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        'Dim strScript As String = "javascript: CallPriceSlider();"
        'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)


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
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
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

    Protected Sub btnBook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBook.Click
        Try
            Dim requestid As String = ""
            Dim sourcectrycode As String = ""
            Dim agentcode As String = ""
            Dim agentref As String = ""
            Dim columbusref As String = ""
            Dim remarks As String = ""
            Dim strTLineno As String = ""


            If txtothSourceCountryCode.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any source country.")
                Exit Sub
            End If



            requestid = GetNewOrExistingRequestId()

            Dim objBLLOtherServiceFreeFormBooking As New BLLOtherServiceFreeFormBooking
            Dim dtt As DataTable
            dtt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
            If dtt.Rows.Count > 0 Then
                objBLLOtherServiceFreeFormBooking.AgentCode = dtt.Rows(0)("agentcode").ToString
                objBLLOtherServiceFreeFormBooking.Div_Code = dtt.Rows(0)("div_code").ToString
                objBLLOtherServiceFreeFormBooking.RequestId = requestid
                objBLLOtherServiceFreeFormBooking.SourcectryCode = dtt.Rows(0)("sourcectrycode").ToString
                objBLLOtherServiceFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtothCustomercode.Text, dtt.Rows(0)("agentcode").ToString)

                objBLLOtherServiceFreeFormBooking.Agentref = dtt.Rows(0)("agentref").ToString
                objBLLOtherServiceFreeFormBooking.ColumbusRef = dtt.Rows(0)("ColumbusRef").ToString
                objBLLOtherServiceFreeFormBooking.Remarks = dtt.Rows(0)("remarks").ToString
                objBLLOtherServiceFreeFormBooking.UserLogged = Session("GlobalUserName")
            Else
                objBLLOtherServiceFreeFormBooking.AgentCode = Session("sAgentCode")
                objBLLOtherServiceFreeFormBooking.Div_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
                objBLLOtherServiceFreeFormBooking.RequestId = requestid ' IIf(requestid = "", objBLLHotelSearch.getrequestid(), requestid)
                objBLLOtherServiceFreeFormBooking.SourcectryCode = txtothSourceCountryCode.Text


                objBLLOtherServiceFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtothCustomercode.Text, Session("sAgentCode"))

                objBLLOtherServiceFreeFormBooking.Agentref = ""
                objBLLOtherServiceFreeFormBooking.ColumbusRef = ""
                objBLLOtherServiceFreeFormBooking.Remarks = remarks
                objBLLOtherServiceFreeFormBooking.UserLogged = Session("GlobalUserName")

            End If


            Dim strBuffer As New Text.StringBuilder
            Dim strOlineno As String = ""

            If ViewState("Olineno") Is Nothing Then
                If strOlineno = "" Then
                    strOlineno = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "OTHERS")
                Else
                    strOlineno = CType(strOlineno, Integer) + 1
                End If


            Else

                strOlineno = ViewState("Olineno")


            End If


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

            If strAdult <> "" Then
                objBLLOtherSearch.Adult = strAdult
            End If
            If strChildren <> "" Then
                objBLLOtherSearch.Children = strChildren
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




            ' strBuffer.Append("<Table>")
            strBuffer.Append("<DocumentElement><Table>")
            strBuffer.Append(" <olineno>" & strOlineno & "</olineno>")
            strBuffer.Append("<othtypcode>" & txtServiceNameCode.Text.Trim & "</othtypcode>")
            strBuffer.Append("<othgrpcode>" & txtothgroupcode.Text & "</othgrpcode>")
            Dim excdate As String = txtothFromDate.Text

            If excdate <> "" Then
                Dim strDates As String() = excdate.Split("/")
                excdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            strBuffer.Append("<othdate>" & Format(CType(excdate, Date), "yyyy/MM/dd") & "</othdate>")

            strBuffer.Append("<adults>" & CType(ddlTourAdult.SelectedValue, String) & "</adults>")
            strBuffer.Append("<child>" & CType(ddlTourChildren.SelectedValue, String) & "</child>")


            strBuffer.Append("<childages>" & objBLLOtherSearch.ChildAgeString & "</childages>")

            strBuffer.Append("<units>" & txtNoOfUnit.Text.Trim & "</units>")

            strBuffer.Append("<unitprice>" & txtUnitPrice.Text.Trim & "</unitprice>")

            strBuffer.Append("<unitsalevalue>" & txtTotal.Text.Trim & "</unitsalevalue>")
            strBuffer.Append("<wlunitsalevalue>" & txtTotal.Text.Trim & "</wlunitsalevalue>")
            strBuffer.Append("<complimentarycust>" & IIf(chkComplSup.Checked = True, "1", "0") & "</complimentarycust>")
            strBuffer.Append("<overrideprice>0</overrideprice>")
            strBuffer.Append("<oplistcode></oplistcode>")

            '*** Danny 22/10/2018 FreeForm SupplierAgent
            If Txt_SupplierCode.Text.Length > 0 Then
                strBuffer.Append("<preferredsupplier>" + Txt_SupplierCode.Text + "</preferredsupplier>")
            Else
                strBuffer.Append("<preferredsupplier></preferredsupplier>")
            End If

            strBuffer.Append("<unitcprice>" & txtCostPricePax.Text & "</unitcprice>")
            strBuffer.Append("<unitcostvalue>" & txtCostPricePaxTotal.Text & "</unitcostvalue>")
            strBuffer.Append("<ocplistcode></ocplistcode>")
            strBuffer.Append("<wlunitprice>" & txtCostPricePax.Text & "</wlunitprice>")
            strBuffer.Append("<wlcurrcode></wlcurrcode>")
            strBuffer.Append("<wlconvrate>0</wlconvrate>")
            strBuffer.Append("<wlmarkupperc>0</wlmarkupperc>")



            ' Added by abin on 20180602 *****************************
            Dim strVATPer As String = objclsUtilities.ExecuteQueryReturnSingleValue("select isnull(option_selected,0) from reservation_parameters where param_id =2013")

            strBuffer.Append("<PriceVATPerc>" & strVATPer & "</PriceVATPerc>")
            strBuffer.Append("<PriceWithTax>1</PriceWithTax>")
            strBuffer.Append("<CostVATPerc>" & strVATPer & "</CostVATPerc>")
            strBuffer.Append("<CostWithTax>1</CostWithTax>")
            Dim dtotalSalevalue As Decimal
            Dim dtotalTv As Decimal
            Dim dtotalVAT As Decimal
            dtotalSalevalue = Convert.ToDecimal(IIf(txtTotal.Text.Trim Is DBNull.Value, "0", (Val(txtTotal.Text.Trim)).ToString))

            dtotalTv = Math.Round(dtotalSalevalue / (1 + (Convert.ToDecimal(strVATPer) / 100)), 3)
            dtotalVAT = Math.Round(dtotalSalevalue - dtotalTv, 3)

            Dim dtotalCostvalue As Decimal
            Dim dtotalCostTv As Decimal
            Dim dtotalCostVAT As Decimal
            dtotalCostvalue = Convert.ToDecimal(IIf(txtCostPricePaxTotal.Text.Trim Is DBNull.Value, "0", (Val(txtCostPricePaxTotal.Text.Trim)).ToString))

            dtotalCostTv = Math.Round(dtotalCostvalue / (1 + (Convert.ToDecimal(strVATPer) / 100)), 3)
            dtotalCostVAT = Math.Round(dtotalCostvalue - dtotalCostTv, 3)



            strBuffer.Append("<PriceTaxableValue>" & dtotalTv.ToString & "</PriceTaxableValue>")
            strBuffer.Append("<PriceVATValue>" & dtotalVAT.ToString & "</PriceVATValue>")
            strBuffer.Append("<CostTaxableValue>" & dtotalCostTv.ToString & "</CostTaxableValue>")
            strBuffer.Append("<CostVATValue>" & dtotalCostVAT.ToString & "</CostVATValue>")

            '*** Danny 22/10/2018 FreeForm SupplierAgent
            If Txt_SupplierAgentCode.Text.Length > 0 Then
                strBuffer.Append("<SupplierAgentCode>" + Txt_SupplierAgentCode.Text + "</SupplierAgentCode>")
            Else
                strBuffer.Append("<SupplierAgentCode></SupplierAgentCode>")
            End If
            '***************end

            ' strBuffer.Append("</Table>")
            strBuffer.Append("</Table></DocumentElement>")

            objBLLOtherServiceFreeFormBooking.OtherServiceXml = strBuffer.ToString
            objBLLOtherServiceFreeFormBooking.RlineNos = strOlineno
            objBLLOtherServiceFreeFormBooking.UserLogged = Session("GlobalUserName")
            If objBLLOtherServiceFreeFormBooking.SaveOtherServiceFreeFormBooking() = True Then
                Session("sRequestId") = requestid
                Response.Redirect("MoreServices.aspx")
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Failed to save.")
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: btnBook_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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

        'Dim strQuery As String = ""

        'Dim othtimelimit As String = ""
        'strQuery = "select option_selected  from reservation_parameters(nolock) where param_id='1148'"
        'othtimelimit = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        'For Each gvrow As DataListItem In dlTourSearchResults.Items

        '    Dim chkSelectTour As CheckBox = CType(gvrow.FindControl("chkSelectTour"), CheckBox)
        '    Dim hdExcCode As HiddenField = CType(gvrow.FindControl("hdExcCode"), HiddenField)
        '    Dim txttourchangedate As TextBox = CType(gvrow.FindControl("txttourchangedate"), TextBox)
        '    Dim dtselectedtour As DataTable = CType(Session("selectedotherdatatable"), DataTable)


        '    If chkSelectTour.Checked = True Then
        '        strQuery = "select isnull(timelimitcheck,0)  from othtypmast(nolock) where othtypcode='" & hdExcCode.Value & "'"
        '        Dim othtimelimitchq As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        '        Dim othdate As Date = DateAdd(DateInterval.Hour, (Val(othtimelimit) * -1), CType(txttourchangedate.Text, Date))
        '        Dim checkindate As Date
        '        ' If hdCheckInPrevDay.Value = "" Then
        '        checkindate = CType(txttourchangedate.Text, Date)
        '        'Else
        '        '    checkindate = DateAdd(DateInterval.Day, 1, CType(hdCheckInPrevDay.Value, Date)) ' hdCheckInPrevDay.Value
        '        'End If
        '        If DateDiff(DateInterval.Day, Now.Date, CType(txttourchangedate.Text, Date)) <= 3 And othtimelimitchq = "1" Then
        '            'If DateDiff(DateInterval.Day, CType(txtothFromDate.Text, Date), CType(othdate, Date)) < 0 Then
        '            chkSelectTour.Checked = False
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Selected Other Services to Book 72 hours Prior Arrival .")
        '            Return False
        '            Exit Function
        '        End If
        '    End If

        'Next

        Dim dtselectedtour1 As DataTable = CType(Session("selectedotherdatatable"), DataTable)

        If dtselectedtour1.Rows.Count > 1 And Not ViewState("Olineno") Is Nothing Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Amend/Edit Option Please Select One Tour ")
            Return False
            Exit Function
        End If

        Return True
    End Function


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

    '*** Danny 22/10/2018 FreeForm SupplierAgent>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetSuppliers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            'If HttpContext.Current.Session("sLoginType") = "RO" Then
            '    strSqlQry = "select agentcode,agentname  from agentmast where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            'Else
            '    If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
            '        strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
            '    Else
            '        strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
            '    End If
            'End If

            strSqlQry = "SELECT partycode,partyname FROM partymast WHERE sptypecode='Otherserv' AND active=1 AND partyname like  '" & prefixText & "%'  order by partyname  "


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
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
    Public Shared Function GetSuppliersAgents(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            'If HttpContext.Current.Session("sLoginType") = "RO" Then
            '    strSqlQry = "select agentcode,agentname  from agentmast where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            'Else
            '    If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
            '        strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
            '    Else
            '        strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
            '    End If
            'End If

            strSqlQry = "SELECT supagentcode,supagentname  FROM supplier_agents WHERE active=1 AND supagentname like  '" & prefixText & "%'  order by supagentname  "


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("supagentname").ToString(), myDS.Tables(0).Rows(i)("supagentcode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function
    '*** Danny 22/10/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


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

    Private Sub FillSpecifiedChildAges(ByVal childages As String)
        ' objclsUtilities.FillDropDownListWithSpecifiedAges(txtTourChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild8, childages)

    End Sub

    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourChildren, child)
    End Sub
    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
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
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("OtherServiceFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub
    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
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
