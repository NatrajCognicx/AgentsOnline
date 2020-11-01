Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System

Partial Class TourSearch
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLTourSearch As New BLLTourSearch
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Dim objUtil As New clsUtils
    Shared tourfromdate As String
    Shared tourtodate As String
    Dim objResParam As New ReservationParameters
    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                If Session("sDSTourSearchResults") IsNot Nothing Then
                    'changed by mohamed on 12/02/2018
                    txtSearchTour.Text = ""
                    'btnTourTextSearch.Enabled = False
                    ' ddlSorting.Enabled = False
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
            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("TourSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub
    Private Sub EditHeaderFill()
        Try
            Dim strQuery As String = ""
            Dim chksector As String = ""
            Dim trftype As String = ""
            Dim dt As New DataTable

            Dim strrequestid As String = ""
            If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
                Session("sSelectedTourTimeSlotDatatable") = Nothing
            End If

            objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)

            strrequestid = GetExistingRequestId()

            dt = objBLLTourSearch.GetEditBookingDetails(strrequestid, Request.QueryString("ELineNo"))

            If dt.Rows.Count > 0 Then
                txtTourFromDate.Text = dt.Rows(0)("fromdate").ToString
                txtTourToDate.Text = dt.Rows(0)("todate").ToString
                txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                ddlTourAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                'txtTourClassificationCode.Text = dt.Rows(0)("classficationcode").ToString
                'txtTourClassification.Text = dt.Rows(0)("classficationanme").ToString

                txtTourStartingFromCode.Text = dt.Rows(0)("tourstartingfromcode").ToString
                txtTourStartingFrom.Text = dt.Rows(0)("tourstartingfromname").ToString
                chkTourOveridePrice.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                If chklPrivateOrSIC.Items.Count > 0 Then
                    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                        If objBLLTourSearch.PrivateOrSIC.Contains(chklPrivateOrSIC.Items(i).Value) = True Then
                            chklPrivateOrSIC.Items(i).Selected = True
                        Else
                            chklPrivateOrSIC.Items(i).Selected = False
                        End If
                    Next
                End If


                If Val(dt.Rows(0)("senior").ToString) <> 0 Then
                    ddlSeniorCitizen.SelectedValue = dt.Rows(0)("senior").ToString
                End If
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
                    If strChildAges.Length > 8 Then
                        txtTourChild9.Text = strChildAges(8)
                    End If
                End If
                BindHotelCheckInAndCheckOutHiddenfield()
                Toursearch()



                If Session("sLoginType") = "RO" Then

                    txtTourSourceCountry.Enabled = False
                    txtTourCustomer.Enabled = False

                End If
                BindComboTourDataTable(strrequestid)
                BindExcInventoryDataTable(strrequestid, txtTourStartingFromCode.Text)
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx ::EditHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub Amendheaderfill()
        Dim dt As DataTable
        Try

            dt = objBLLTourSearch.GetEditBookingDetails(Session("sEditRequestId"), Request.QueryString("ELineNo"))
            If dt.Rows.Count > 0 Then

                txtTourFromDate.Text = dt.Rows(0)("fromdate").ToString
                txtTourToDate.Text = dt.Rows(0)("todate").ToString
                txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                ddlTourAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                'txtTourClassificationCode.Text = dt.Rows(0)("classficationcode").ToString
                'txtTourClassification.Text = dt.Rows(0)("classficationanme").ToString

                txtTourStartingFromCode.Text = dt.Rows(0)("tourstartingfromcode").ToString
                txtTourStartingFrom.Text = dt.Rows(0)("tourstartingfromname").ToString
                chkTourOveridePrice.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

               

                If chklPrivateOrSIC.Items.Count > 0 Then
                    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                        chklPrivateOrSIC.Items(i).Selected = True
                    Next
                End If

                If Val(dt.Rows(0)("senior").ToString) <> 0 Then
                    ddlSeniorCitizen.SelectedValue = dt.Rows(0)("senior").ToString
                End If


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
                    If strChildAges.Length > 8 Then
                        txtTourChild9.Text = strChildAges(8)
                    End If
                End If
                BindHotelCheckInAndCheckOutHiddenfield()
                Toursearch()



                If Session("sLoginType") = "RO" Then

                    txtTourSourceCountry.Enabled = False
                    txtTourCustomer.Enabled = False

                End If


                BindComboTourDataTable(Session("sEditRequestId"))
                BindExcInventoryDataTable(Session("sEditRequestId"), txtTourStartingFromCode.Text)
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: AmendHeaderFille :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Private Sub NewHeaderFill()

        Dim strrequestid As String = ""
        Dim strQuery As String = ""
        Dim dt As New DataTable
        Dim dtpax As New DataTable
        Try

            If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
                Session("sSelectedTourTimeSlotDatatable") = Nothing
            End If

            If Not Session("sobjBLLTourSearch") Is Nothing Then
                objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)

                Dim strFromMoreService As String = ""
                If Not Page.Request.UrlReferrer Is Nothing Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    If previousPage.Contains("MoreServices.aspx") Then
                        strFromMoreService = "1"
                    End If
                End If

                If strFromMoreService <> "1" Then
                    txtTourFromDate.Text = objBLLTourSearch.FromDate
                    txtTourToDate.Text = objBLLTourSearch.ToDate
                    txtTourStartingFrom.Text = objBLLTourSearch.TourStartingFrom
                    txtTourStartingFromCode.Text = objBLLTourSearch.TourStartingFromCode
                    txtTourClassification.Text = objBLLTourSearch.Classification
                    txtTourClassificationCode.Text = objBLLTourSearch.ClassificationCode
                End If


                txtTourSourceCountry.Text = objBLLTourSearch.SourceCountry
                txtTourSourceCountryCode.Text = objBLLTourSearch.SourceCountryCode
                ddlStarCategory.SelectedValue = objBLLTourSearch.StarCategoryCode
                txtTourCustomer.Text = objBLLTourSearch.Customer
                txtTourCustomerCode.Text = objBLLTourSearch.CustomerCode

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

                ddlTourAdult.SelectedValue = objBLLTourSearch.Adult
                ddlSeniorCitizen.SelectedValue = objBLLTourSearch.SeniorCitizen
                ddlTourChildren.SelectedValue = objBLLTourSearch.Children
                txtTourChild1.Text = objBLLTourSearch.Child1
                txtTourChild2.Text = objBLLTourSearch.Child2
                txtTourChild3.Text = objBLLTourSearch.Child3
                txtTourChild4.Text = objBLLTourSearch.Child4
                txtTourChild5.Text = objBLLTourSearch.Child5
                txtTourChild6.Text = objBLLTourSearch.Child6
                txtTourChild7.Text = objBLLTourSearch.Child7
                txtTourChild8.Text = objBLLTourSearch.Child8
                txtTourChild9.Text = objBLLTourSearch.Child9
                If objBLLTourSearch.Children <> "0" Then
                    Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                    Dim javaScriptChldrn As String = "<script type='text/javascript'>ShowTourChild();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)
                End If


                If objBLLTourSearch.OverridePrice = "1" Then
                    chkTourOveridePrice.Checked = True
                Else
                    chkTourOveridePrice.Checked = False
                End If

                'If objBLLTourSearch.PrivateOrSIC.Contains("SIC") = True Then
                '    rblPrivateOrSIC.SelectedValue = "SIC (Seat In Coach)"
                'Else
                '    rblPrivateOrSIC.SelectedValue = objBLLTourSearch.PrivateOrSIC
                'End If

                If chklPrivateOrSIC.Items.Count > 0 Then
                    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                        If objBLLTourSearch.PrivateOrSIC.Contains(chklPrivateOrSIC.Items(i).Value) = True Then
                            chklPrivateOrSIC.Items(i).Selected = True
                        Else
                            chklPrivateOrSIC.Items(i).Selected = False
                        End If
                    Next
                End If

                objBLLTourSearch.AmendRequestid = GetExistingRequestId()
                objBLLTourSearch.AmendLineno = ViewState("Elineno")


                If strFromMoreService <> "1" Then
                    BindSearchResults()
                End If


                BindHotelCheckInAndCheckOutHiddenfield()

                If strFromMoreService = "1" Then
                    BindTourChildAge()
                End If

            Else
                Dim objBLLCommonFuntions = New BLLCommonFuntions

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
                If dt.Rows.Count > 0 Then

                    txtTourFromDate.Text = dt.Rows(0)("mindate_").ToString
                    txtTourToDate.Text = dt.Rows(0)("maxdate_").ToString
                    txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString

                    txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                    txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                    txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                    txtTourClassificationCode.Text = ""
                    txtTourClassification.Text = ""

                    'txtTourStartingFromCode.Text = ""
                    'txtTourStartingFrom.Text = ""
                    ' rblPrivateOrSIC.SelectedValue = "PRIVATE"
                    If chklPrivateOrSIC.Items.Count > 0 Then
                        For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                            chklPrivateOrSIC.Items(i).Selected = True
                        Next
                    End If


                    '''' Added shahul 07/04/18
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
                            If strChildAges.Length > 8 Then
                                txtTourChild9.Text = strChildAges(8)
                            End If
                        End If

                    End If


                   

                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: NewHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
        objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        LoadRoomAdultChild()
        LoadFields()
        ShowMyBooking()
        CreateSelectedTourDataTable()
        CreateComboTourDataTable()
        If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
            Session("sSelectedTourTimeSlotDatatable") = Nothing
        End If

        ViewState("Elineno") = Request.QueryString("ELineNo")

        If Not Request.QueryString("ELineNo") Is Nothing Then
            hdnlineno.Value = Request.QueryString("ELineNo")
        Else
            hdnlineno.Value = "0"
        End If

        FillTourPickupLocation()

        Dim dt As DataTable
        objBLLTourSearch = New BLLTourSearch

        If Not Session("sEditRequestId") Is Nothing Then

            If ViewState("Elineno") Is Nothing Then

                NewHeaderFill()

            Else
                Amendheaderfill()
            End If
        Else
            If Not Session("sobjBLLTourSearch") Is Nothing Then

                If ViewState("Elineno") Is Nothing Then

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
    Private Sub BindTourChildAge()
        Dim dtpax As DataTable
        Dim objBLLCommonFuntions = New BLLCommonFuntions
        Dim strRequestId As String = ""
        Dim strQuery As String = ""
        If Not Session("sRequestId") Is Nothing Then
            strRequestId = Session("sRequestId")
            Dim dt As DataTable
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(strRequestId)
            If dt.Rows.Count > 0 Then
                hdTab.Value = "1"

                txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & strRequestId & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 Then
                    ddlTourAdult.SelectedValue = dtpax.Rows(0)("adults").ToString
                    ddlTourChildren.SelectedValue = dtpax.Rows(0)("child").ToString

                    If dtpax.Rows(0)("child").ToString <> "0" Then

                        ''' Added 01/06/17 shahul
                        Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                        If Left(childages, 1) = ";" Then
                            childages = Right(childages, (childages.Length - 1))
                        End If
                        Dim strChildAges As String() = childages.ToString.Split(";")
                        '''''''

                        '  Dim strChildAges As String() = dt.Rows(0)("childages").ToString.Split(";")

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
                        If strChildAges.Length > 8 Then
                            txtTourChild9.Text = strChildAges(8)
                        End If
                    End If



                    If dt.Rows(0)("reqoverride").ToString = "1" Then
                        chkTourOveridePrice.Checked = True
                    Else
                        chkTourOveridePrice.Checked = False
                    End If

                End If
            End If
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

                txtTourFromDate.Text = dt.Rows(0)("CheckIn").ToString
                txtTourToDate.Text = dt.Rows(0)("CheckOut").ToString


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

        Dim dt1 As DataTable

        Dim strRequestId1 As String = ""
        If Not Session("sRequestId") Is Nothing Then
            ' objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            strRequestId1 = Session("sRequestId")
            dt1 = objBLLHotelSearch.GetSectorCheckInAndCheckOutDetails(strRequestId1, txtTourStartingFromCode.Text)
            Dim strFlag As String = ""
            If dt1.Rows.Count > 0 Then
                If dt1.Rows(0)("CheckIn").ToString = "" Then
                    hdChangeFromdate.Value = txtTourFromDate.Text
                    strFlag = "1"
                Else
                    hdChangeFromdate.Value = dt1.Rows(0)("CheckIn").ToString
                End If
                If dt1.Rows(0)("CheckOut").ToString = "" Then
                    hdChangeTodate.Value = txtTourToDate.Text
                Else
                    hdChangeTodate.Value = dt1.Rows(0)("CheckOut").ToString
                End If

            Else
                Dim dt2 As DataTable
                strRequestId1 = Session("sRequestId")
                dt2 = objBLLTourSearch.GetTourCheckInAndCheckOutDetails(strRequestId1, txtTourStartingFromCode.Text)
                If dt2.Rows.Count > 0 Then
                    hdChangeFromdate.Value = dt2.Rows(0)("searchfromdate").ToString
                    hdChangeTodate.Value = dt2.Rows(0)("searchtodate").ToString
                    'hdChangeFromdate.Value = txtTourFromDate.Text
                Else
                    hdChangeFromdate.Value = txtTourFromDate.Text
                    hdChangeTodate.Value = txtTourToDate.Text
                End If

            End If
            If strFlag = "1" Then
                Dim dt2 As DataTable
                strRequestId1 = Session("sRequestId")
                dt2 = objBLLTourSearch.GetTourCheckInAndCheckOutDetails(strRequestId1, txtTourStartingFromCode.Text)
                If dt2.Rows.Count > 0 Then
                    hdChangeFromdate.Value = dt2.Rows(0)("searchfromdate").ToString
                    hdChangeTodate.Value = dt2.Rows(0)("searchtodate").ToString
                    'hdCheckInPrevDay.Value = dt2.Rows(0)("searchfromdate").ToString
                    'hdCheckOutNextDay.Value = dt2.Rows(0)("searchtodate").ToString
                    txtTourFromDate.Text = hdChangeFromdate.Value
                    txtTourToDate.Text = hdChangeTodate.Value
                Else
                    hdChangeFromdate.Value = txtTourFromDate.Text
                    hdChangeTodate.Value = txtTourToDate.Text
                End If
            End If

        Else

            hdChangeFromdate.Value = txtTourFromDate.Text
            hdChangeTodate.Value = txtTourToDate.Text

            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
            Dim javaScriptChldrn As String = "<script type='text/javascript'>CallToDatePicker();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)



        End If

        tourfromdate = hdChangeFromdate.Value
        tourtodate = hdChangeTodate.Value
    End Sub
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo '"Logos/" & strLogo

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
            If Session("sLoginType") <> "RO" Then
                dvTourCustomer.Visible = False
                dvTourOveridePrice.Visible = False
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
                        txtTourSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtTourSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtTourSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtTourSourceCountry.Enabled = False
                    Else
                        txtTourSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtTourSourceCountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else
                dvTourCustomer.Visible = True
                dvTourOveridePrice.Visible = True
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSTourSearchResults")
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        'Dim strScript As String = "javascript: CallPriceSlider();"
        'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)


    End Sub

    Protected Sub ddlSorting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSorting.SelectedIndexChanged
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSTourSearchResults")
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: ddlSorting_SelectedIndexChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSTourSearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sTourPageIndex") = pageIndex.ToString
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                        strFilterCriteria = strFilterCriteria & " AND " & "starcategory NOT IN (" & strNotSelectedHotelStar & ")"
                    Else
                        strFilterCriteria = " starcategory NOT IN (" & strNotSelectedHotelStar & ")"
                    End If
                End If

                If strNotSelectedClassification <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "classificationcode NOT IN (" & strNotSelectedClassification & ")"
                    Else
                        strFilterCriteria = " classificationcode NOT IN (" & strNotSelectedClassification & ")"
                    End If
                End If


                If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "totalsalevalue >=" & hdPriceMin.Value & " AND totalsalevalue <=" & hdPriceMax.Value
                    Else
                        strFilterCriteria = "totalsalevalue >=" & hdPriceMin.Value & " AND totalsalevalue <=" & hdPriceMax.Value
                    End If
                End If

                Dim strFilterCriteriaSearchTour As String = ""
                Dim lsTourSearchOrder As String = ""
                If txtSearchTour.Text <> "" Then
                    lsTourSearchOrder = "CustomSortHelp, "
                    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " excname like ('" & txtSearchTour.Text & "%')"
                End If
                'changed by mohamed on 12/02/2018
                If strFilterCriteria & strFilterCriteriaSearchTour <> "" Then
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                End If

                'changed by mohamed on 12/02/2018
                'Search Text in Middle
                If txtSearchTour.Text <> "" Then
                    dtMainDetailsRet = dvMaiDetails.ToTable.Copy
                    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " excname like ('%" & txtSearchTour.Text & "%') and excname not like ('" & txtSearchTour.Text & "%')"
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                    dtMainDetailsMiddle = dvMaiDetails.ToTable.Copy
                    dtMainDetailsMiddle.Columns("CustomSortHelp").Expression = "3"
                    dtMainDetailsRet.Merge(dtMainDetailsMiddle)
                    'dvMaiDetails = Nothing
                    dvMaiDetails = New DataView(dtMainDetailsRet)
                End If

                'changed by mohamed on 12/02/2018
                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & " excname ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & " totalsalevalue ASC"
                ElseIf ddlSorting.Text = "Preferred" Then
                    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & "Preferred  DESC, excname ASC"
                ElseIf ddlSorting.Text = "Rating" Then
                    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & " starcategory DESC,excname ASC "
                End If


                Dim recordCount As Integer = dvMaiDetails.Count

                BindTourMainDetails(dvMaiDetails)
                Me.PopulatePager(recordCount)

                FillCheckBox()

            End If
        Else
            dlTourSearchResults.DataBind()
        End If
    End Sub
    Private Sub PopulatePager(ByVal recordCount As Integer)
        Dim currentPage As Integer = 1
        If Not Session("sTourPageIndex") Is Nothing Then
            currentPage = Session("sTourPageIndex")
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
            Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
            Session.Clear()
            Session.Abandon()
            Response.Redirect(strAbsoluteUrl, True)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetClassification(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select classificationcode,classificationname from excclassification_header where active=1 and classificationname like  '" & prefixText & "%' order by classificationname "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("classificationname").ToString(), myDS.Tables(0).Rows(i)("classificationcode").ToString()))
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
    Public Shared Function GetTourStartingFrom(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim TourStartingFrom As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = ""
            Dim objBLLHotelSearch = New BLLHotelSearch
            'If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
            If Not HttpContext.Current.Session("sRequestid") Is Nothing Then
                ' objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")
                Dim dt As DataTable

                dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(HttpContext.Current.Session("sRequestid"))
                If dt.Rows.Count > 0 Then
                    strSqlQry = "select distinct othtypcode,othtypname  from  othtypmast o,view_booking_hotel_prearr d,partymast p,sectormaster s where o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and d.requestid='" & HttpContext.Current.Session("sRequestid") & "' and  o.othtypname  like '" & LTrim(prefixText) & "%' order by o.othtypname "
                Else
                    strSqlQry = "select othtypcode,othtypname  from  othtypmast where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001)  and  othtypname  like '" & LTrim(prefixText) & "%'  order by othtypname "
                End If
            Else
                strSqlQry = "select othtypcode,othtypname  from  othtypmast where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and  othtypname  like '" & LTrim(prefixText) & "%' order by othtypname "
            End If


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othtypname").ToString(), myDS.Tables(0).Rows(i)("othtypcode").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
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

    Protected Sub lbSelectDate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim myLinkButton As LinkButton = CType(sender, LinkButton)
        Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)

        Dim lbPrice As LinkButton = CType(dlItem.FindControl("lbPrice"), LinkButton)
        Session("slbTourTotalSaleValue") = lbPrice

        Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
        Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
        Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
        Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
        hdCurrCodePopup.Value = hdCurrCode.Value
        Dim hdCombo As HiddenField = CType(dlItem.FindControl("hdCombo"), HiddenField)
        Dim hdMultipleDates As HiddenField = CType(dlItem.FindControl("hdMultipleDates"), HiddenField)

        If hdCombo.Value = "YES" Then
            lblComboExcName.Text = lblExcName.Text
            hdExcCodeComboPopup.Value = hdExcCode.Value
            hdVehicleCodeComboPopup.Value = hdVehicleCode.Value
            Dim dt As New DataTable
            dt = objBLLTourSearch.GetComboExcursions(hdExcCode.Value)
            dlSelectComboDates.DataSource = dt
            dlSelectComboDates.DataBind()
            If dlSelectComboDates.Items.Count > 0 Then
                Dim dtCombo As New DataTable
                dtCombo = Session("selectedCombotourdatatable")
                For Each dlItem1 As DataListItem In dlSelectComboDates.Items
                    Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
                    Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
                    Dim foundRow As DataRow
                    foundRow = dtCombo.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='COMBO' ").FirstOrDefault
                    If Not foundRow Is Nothing Then
                        txtExcComboDate.Text = foundRow("excdate")
                    End If
                Next

            End If

            mpSelectComboDates.Show()
        End If
        If hdMultipleDates.Value = "YES" Then
            'hdChangeFromdate
            'hdChangeTodate
            Dim dtDates As DataTable
            dtDates = objBLLTourSearch.GetMultipleDates(hdChangeFromdate.Value, hdChangeTodate.Value, hdExcCode.Value)

            If dtDates.Rows.Count > 0 Then
                dlMealPlanMultipleDate.DataSource = dtDates
                dlMealPlanMultipleDate.DataBind()
                lblMealPlanExcName.Text = "Excursion: " & lblExcName.Text
                hdMealPlanExcCode.Value = hdExcCode.Value
                hdMealPlanVehicleCode.Value = hdVehicleCode.Value

                If dlMealPlanMultipleDate.Items.Count > 0 Then
                    Dim dtMultiDates As New DataTable
                    dtMultiDates = Session("selectedCombotourdatatable")
                    For Each dlItem1 As DataListItem In dlMealPlanMultipleDate.Items
                        Dim lblMeanPlanDates As Label = CType(dlItem1.FindControl("lblMeanPlanDates"), Label)
                        Dim chkMealPlanDates As CheckBox = CType(dlItem1.FindControl("chkMealPlanDates"), CheckBox)
                        Dim foundRow As DataRow
                        foundRow = dtMultiDates.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & hdExcCode.Value.Trim & "' AND excdate='" & lblMeanPlanDates.Text.Trim & "' AND type='MULTI_DATE' ").FirstOrDefault
                        If Not foundRow Is Nothing Then
                            chkMealPlanDates.Checked = True
                        End If
                    Next

                End If


                mpMealPlanDatesPopup.Show()
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, lblMealPlanExcName.Text & " is not operational on these dates.")

            End If


        End If

    End Sub

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
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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

        objBLLTourSearch = New BLLTourSearch
        If Session("sobjBLLTourSearch") Is Nothing Then
            Response.Redirect("Home.aspx?Tab=1")
        End If
        objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)

        Dim dsTourSearchResults As New DataSet
        objBLLTourSearch.DateChange = "0"
        dsTourSearchResults = objBLLTourSearch.GetSearchDetails()

        If dsTourSearchResults.Tables(0).Rows.Count = 0 Then
            dvhotnoshow.Style.Add("display", "block")
        Else
            dvhotnoshow.Style.Add("display", "none")
        End If

        Session("sDSTourSearchResults") = dsTourSearchResults
        If dsTourSearchResults.Tables.Count > 0 Then

            BindTourPricefilter(dsTourSearchResults.Tables(1))
            BindTourHotelStars(dsTourSearchResults.Tables(2))
            BindTourRoomClassification(dsTourSearchResults.Tables(3))
            Session("sDSTourSearchResults") = dsTourSearchResults
            Session("sTourPageIndex") = "1"
            Dim dvMaiDetails As DataView = New DataView(dsTourSearchResults.Tables(0))
            If ddlSorting.Text = "Name" Then
                dvMaiDetails.Sort = "excname ASC"
            ElseIf ddlSorting.Text = "Price" Then
                dvMaiDetails.Sort = "totalsalevalue ASC"
            ElseIf ddlSorting.Text = "Preferred" Then
                dvMaiDetails.Sort = "Preferred  DESC,excname ASC "
            ElseIf ddlSorting.Text = "Rating" Then
                dvMaiDetails.Sort = "starcategory DESC,excname ASC "
            End If
            Dim recordCount As Integer = dvMaiDetails.Count
            BindTourMainDetails(dvMaiDetails)
            Me.PopulatePager(recordCount)
            lblHotelCount.Text = dsTourSearchResults.Tables(0).Rows.Count & " Records Found"

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
            chkHotelStars.DataTextField = "catname"
            chkHotelStars.DataValueField = "starcategory"
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
            If Not Session("sTourPageIndex") Is Nothing Then
                iPageIndex = Session("sTourPageIndex")
            End If

            iRowNoFrom = (iPageIndex - 1) * PageSize + 1
            iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
            dv.Table.Columns.Add("rowIndex")
            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("rowIndex") = (i + 1).ToString
            Next


            dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & iRowNoTo
            dvMaiDetails.Sort = "tourselected DESC"
            dlTourSearchResults.DataSource = dv
            dlTourSearchResults.DataBind()

        Else
            dlTourSearchResults.DataBind()
        End If
        Session("sdtTourPriceBreakup") = Nothing


        Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
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

                Dim lblExcName As Label = CType(gvRow.FindControl("lblExcName"), Label)
                Dim hdCurrCode As HiddenField = CType(gvRow.FindControl("hdCurrCode"), HiddenField)
                hdCurrCodePopup.Value = hdCurrCode.Value
                Dim hdCombo As HiddenField = CType(gvRow.FindControl("hdCombo"), HiddenField)
                Dim hdMultipleDates As HiddenField = CType(gvRow.FindControl("hdMultipleDates"), HiddenField)

                ' If hdCombo.Value <> "YES" And hdMultipleDates.Value <> "YES" Then



                chkSelectTour.Checked = True
                txtTourChangeDate.Text = Format(CType(hdntourdate.Value, Date), "dd/MM/yyyy")
                lblunits.Text = hdncumunits.Value + " Units"
                For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                    If dtselectedtour.Rows(i)("exctypcode") = hdExcCode.Value.ToString And dtselectedtour.Rows(i)("vehiclecode") = hdVehicleCode.Value.ToString Then
                        dtselectedtour.Rows.RemoveAt(i)
                    End If
                Next
                dtselectedtour.Rows.Add(hdExcCode.Value.ToString, txtTourChangeDate.Text, hdVehicleCode.Value)
                'End If




                'If hdCombo.Value = "YES" Then
                '    chkSelectTour.Checked = True
                '    lblComboExcName.Text = lblExcName.Text
                '    hdExcCodeComboPopup.Value = hdExcCode.Value
                '    hdVehicleCodeComboPopup.Value = hdVehicleCode.Value
                '    Dim dtt As New DataTable
                '    dtt = objBLLTourSearch.GetComboExcursions(hdExcCode.Value)
                '    dlSelectComboDates.DataSource = dtt
                '    dlSelectComboDates.DataBind()
                '    If dlSelectComboDates.Items.Count > 0 Then
                '        Dim dtCombo As New DataTable
                '        dtCombo = Session("selectedCombotourdatatable")
                '        For Each dlItem1 As DataListItem In dlSelectComboDates.Items
                '            Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
                '            Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
                '            Dim foundRow As DataRow
                '            foundRow = dtCombo.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='COMBO' ").FirstOrDefault
                '            If Not foundRow Is Nothing Then
                '                txtExcComboDate.Text = foundRow("excdate")
                '            End If
                '        Next

                '    End If


                'End If
                'If hdMultipleDates.Value = "YES" Then
                '    chkSelectTour.Checked = True
                '    'hdChangeFromdate
                '    'hdChangeTodate
                '    Dim dtDates As DataTable
                '    dtDates = objBLLTourSearch.GetMultipleDates(hdChangeFromdate.Value, hdChangeTodate.Value, hdExcCode.Value)

                '    If dtDates.Rows.Count > 0 Then
                '        dlMealPlanMultipleDate.DataSource = dtDates
                '        dlMealPlanMultipleDate.DataBind()
                '        lblMealPlanExcName.Text = "Excursion: " & lblExcName.Text
                '        hdMealPlanExcCode.Value = hdExcCode.Value
                '        hdMealPlanVehicleCode.Value = hdVehicleCode.Value

                '        If dlMealPlanMultipleDate.Items.Count > 0 Then
                '            Dim dtMultiDates As New DataTable
                '            dtMultiDates = Session("selectedCombotourdatatable")
                '            For Each dlItem1 As DataListItem In dlMealPlanMultipleDate.Items
                '                Dim lblMeanPlanDates As Label = CType(dlItem1.FindControl("lblMeanPlanDates"), Label)
                '                Dim chkMealPlanDates As CheckBox = CType(dlItem1.FindControl("chkMealPlanDates"), CheckBox)
                '                Dim foundRow As DataRow
                '                foundRow = dtMultiDates.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & hdExcCode.Value.Trim & "' AND excdate='" & lblMeanPlanDates.Text.Trim & "' AND type='MULTI_DATE' ").FirstOrDefault
                '                If Not foundRow Is Nothing Then
                '                    chkMealPlanDates.Checked = True
                '                End If
                '            Next

                '        End If

                '    End If
                'End If



            End If

        Next


        'If dlSelectComboDates.Items.Count > 0 Then
        '    Dim dtCombo As New DataTable
        '    dtCombo = Session("selectedCombotourdatatable")
        '    For Each dlItem1 As DataListItem In dlSelectComboDates.Items
        '        Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
        '        Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
        '        Dim foundRow As DataRow
        '        foundRow = dtCombo.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='COMBO' ").FirstOrDefault
        '        If Not foundRow Is Nothing Then
        '            txtExcComboDate.Text = foundRow("excdate")
        '        End If
        '    Next

        'End If






        Session("selectedtourdatatable") = dtselectedtour




    End Sub

    Protected Sub dlTourSearchResults_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlTourSearchResults.ItemCommand


    End Sub

    Protected Sub dlTourSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlTourSearchResults.ItemDataBound
        Dim lblunits As LinkButton = CType(e.Item.FindControl("lblunits"), LinkButton)
        Dim hdncumunits As HiddenField = CType(e.Item.FindControl("hdncumunits"), HiddenField)


        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbPrice As LinkButton = CType(e.Item.FindControl("lbPrice"), LinkButton)
            Dim hdwlCurrCode As HiddenField = CType(e.Item.FindControl("hdwlCurrCode"), HiddenField)
            Dim hdCurrCode As HiddenField = CType(e.Item.FindControl("hdCurrCode"), HiddenField)
            lbPrice.Text = lbPrice.Text.Replace(".000", "")
            Dim hdMultipleDates As HiddenField = CType(e.Item.FindControl("hdMultipleDates"), HiddenField)
            Dim hdtotalsalevalue As HiddenField = CType(e.Item.FindControl("hdtotalsalevalue"), HiddenField)
            Dim hdExcCode As HiddenField = CType(e.Item.FindControl("hdExcCode"), HiddenField)
            Dim hdVehicleCode As HiddenField = CType(e.Item.FindControl("hdVehicleCode"), HiddenField)
            If hdMultipleDates.Value = "YES" Then
                If Not Session("selectedCombotourdatatable") Is Nothing Then
                    Dim dtselectedCombotour As New DataTable

                    dtselectedCombotour = Session("selectedCombotourdatatable")
                    Dim strType As String = "MULTI_DATE"

                    Dim dvComboBreakup As DataView
                    dvComboBreakup = New DataView(dtselectedCombotour)
                    dvComboBreakup.RowFilter = "exctypcode= '" & hdExcCode.Value & "'  AND  vehiclecode='" & hdVehicleCode.Value & "'  AND type='" & strType & "' "
                    Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
                    If dtComboBreakup.Rows.Count > 1 Then
                        lbPrice.Text = hdCurrCodePopup.Value & " " & Math.Round(Val(hdtotalsalevalue.Value) * dtComboBreakup.Rows.Count, 2).ToString
                    End If
                End If

            End If





            Dim lbwlPrice As LinkButton = CType(e.Item.FindControl("lbwlPrice"), LinkButton)
            Dim dWlPrice As Double = IIf(lbwlPrice.Text = "", 0, lbwlPrice.Text)
            lbwlPrice.Text = Math.Round(dWlPrice, 2, MidpointRounding.AwayFromZero) & " " & hdwlCurrCode.Value
            Dim chkSelectTour As CheckBox = CType(e.Item.FindControl("chkSelectTour"), CheckBox)
            Dim txtTourChangeDate As TextBox = CType(e.Item.FindControl("txtTourChangeDate"), TextBox)
            Dim dvTourdates As HtmlGenericControl = CType(e.Item.FindControl("dvTourdates"), HtmlGenericControl)
            Dim lblTourAdultChild As Label = CType(e.Item.FindControl("lblTourAdultChild"), Label)
            Dim dvSelectDatelbl As HtmlGenericControl = CType(e.Item.FindControl("dvSelectDatelbl"), HtmlGenericControl)
            Dim dvSelectDatelink As HtmlGenericControl = CType(e.Item.FindControl("dvSelectDatelink"), HtmlGenericControl)

            Dim hdcombo As HiddenField = CType(e.Item.FindControl("hdcombo"), HiddenField)
            'Dim hdMultipleDates As HiddenField = CType(e.Item.FindControl("hdMultipleDates"), HiddenField)

            Dim dvTourType As HtmlGenericControl = CType(e.Item.FindControl("dvTourType"), HtmlGenericControl)
            Dim lblSicPvt As Label = CType(e.Item.FindControl("lblSicPvt"), Label)
            If lblSicPvt.Text.ToUpper = "WITHOUT TRANSFERS TOURS" Then
                dvTourType.Attributes.Add("style", "background-color:#EBD255;padding:4px 3px 3px 3px;color:white;font-size:12px;border-color:#EBD255;")
            ElseIf lblSicPvt.Text.ToUpper = "SIC (SEAT IN COACH) TOURS" Or lblSicPvt.Text.ToUpper = "SIC(SEAT IN COACH) TOURS" Then
                dvTourType.Attributes.Add("style", "background-color:#43C6DB;padding:4px 3px 3px 3px;color:white;font-size:12px;border-color:#43C6DB;")
            ElseIf lblSicPvt.Text.ToUpper = "PRIVATE TOURS" Then
                dvTourType.Attributes.Add("style", "background-color:#F660AB;padding:4px 3px 3px 3px;color:white;font-size:12px;border-color:#F660AB;")
            Else
                'dvTourType.Attributes.Add("style", "float:left;width:55%;text-transform:uppercase;")
            End If

            If hdcombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
                dvSelectDatelbl.Attributes.Add("style", "display:none")
                dvSelectDatelink.Attributes.Add("style", "display:block")
            Else
                dvSelectDatelbl.Attributes.Add("style", "display:block")
                dvSelectDatelink.Attributes.Add("style", "display:none")
            End If

            If Not Session("sobjBLLTourSearch") Is Nothing Then
                Dim objTour As New BLLTourSearch
                objTour = Session("sobjBLLTourSearch")
                Dim strAdultchild As String = ""
                strAdultchild = "[ " & objTour.Adult & " Ad "
                objTour = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
                If objTour.Children > 0 Then
                    strAdultchild = strAdultchild & " + " & objTour.Children & " Ch (" & objTour.ChildAgeString.ToString.Replace(";", ",") & ")"
                Else
                    strAdultchild = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & strAdultchild
                End If
                strAdultchild = strAdultchild & " ]"
                lblTourAdultChild.Text = strAdultchild
            End If

            'Show Prefered Button
            Dim lblPreferred As Label = CType(e.Item.FindControl("lblPreferred"), Label)
            Dim btnPreferred As HtmlInputButton = CType(e.Item.FindControl("btnPreferred"), HtmlInputButton)
            If lblPreferred.Text = "1" Then
                btnPreferred.Visible = True
            Else
                btnPreferred.Visible = False
            End If



            If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                lbwlPrice.Visible = True
                lbPrice.Visible = False
                hdSliderCurrency.Value = " " & hdwlCurrCode.Value
            Else
                lbwlPrice.Visible = False
                hdSliderCurrency.Value = " " & hdCurrCode.Value
                ' lbPrice.Visible = True
            End If
            chkSelectTour.Attributes.Add("onclick", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','c')")
            chkSelectTour.Attributes.Add("onchange", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','c')")

            txtTourChangeDate.Attributes.Add("onchange", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','d')")
            ' chkSelectTour.Attributes.Add("OnCheckedChanged", "Check_Changed()")
            dvTourdates.Attributes.Add("onclick", "javascript:SelectedTour1('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','d')")
            'Show Hotel Image
            Dim imgExcImage As Image = CType(e.Item.FindControl("imgExcImage"), Image)
            Dim lblExcImage As Label = CType(e.Item.FindControl("lblExcImage"), Label)
            imgExcImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblExcImage.Text & "&Type=1"

            'Show Hotel Stars
            Dim hdNoOfExcStars As HiddenField = CType(e.Item.FindControl("hdNoOfExcStars"), HiddenField)
            Dim dvExcStars As HtmlGenericControl = CType(e.Item.FindControl("dvExcStars"), HtmlGenericControl)
            Dim strExcStarHTML As New StringBuilder

            strExcStarHTML.Append(" <nav class='stars'><ul>")
            If hdNoOfExcStars.Value = "1" Then
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            ElseIf hdNoOfExcStars.Value = "2" Then
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")

            ElseIf hdNoOfExcStars.Value = "3" Then
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            ElseIf hdNoOfExcStars.Value = "4" Then
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                ' strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            ElseIf hdNoOfExcStars.Value = "5" Then
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
                strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            Else
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
                'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            End If

            strExcStarHTML.Append(" </ul>")
            dvExcStars.InnerHtml = strExcStarHTML.ToString

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
                Dim lbwlPrice As LinkButton = CType(e.Item.FindControl("lbwlPrice"), LinkButton)
                Dim hdRateBasis As HiddenField = CType(e.Item.FindControl("hdRateBasis"), HiddenField)

                lbPrice.Text = lbPrice.Text.Replace(".000", "")
                'Dim dWlPrice As Double = IIf(lbwlPrice.Text = "", 0, lbwlPrice.Text)
                'lbwlPrice.Text = Math.Round(dWlPrice, 2, MidpointRounding.AwayFromZero)
                Dim lblPriceBy As Label = CType(e.Item.FindControl("lblPriceBy"), Label)
                lbPrice.Visible = False
                lblPriceBy.Visible = False
                lbwlPrice.Visible = False
                If hdRateBasis.Value.ToUpper = "UNIT" Then
                    lblunits.Visible = True
                    lblunits.Text = hdncumunits.Value + " Units"
                Else
                    lblunits.Visible = False
                End If

            End If
        Else
            lblunits.Visible = False
        End If
    End Sub

    Protected Sub lbPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try


            Dim lbPrice As LinkButton = CType(sender, LinkButton)
            Session("slbTourTotalSaleValue") = lbPrice
            Dim dlItem As DataListItem = CType((lbPrice).NamingContainer, DataListItem)
            Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
            Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
            Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
            Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
            Dim hdRateBasis As HiddenField = CType(dlItem.FindControl("hdRateBasis"), HiddenField)
            Dim txtTourChangeDate As TextBox = CType(dlItem.FindControl("txtTourChangeDate"), TextBox)
            Dim chkselecttour As CheckBox = CType(dlItem.FindControl("chkselecttour"), CheckBox)

            Dim hdChildAsFree As HiddenField = CType(dlItem.FindControl("hdChildAsFree"), HiddenField) ' Added by abin on 20181208
            hdChildAsFreePopup.Value = IIf(hdChildAsFree.Value = "", "0", hdChildAsFree.Value)

            Dim hdcombo As HiddenField = CType(dlItem.FindControl("hdcombo"), HiddenField)
            Dim hdMultipleDates As HiddenField = CType(dlItem.FindControl("hdMultipleDates"), HiddenField)
            hdMultiDay.Value = hdMultipleDates.Value
            hdExcCodePopup.Value = hdExcCode.Value
            hdRateBasisPopup.Value = hdRateBasis.Value
            hdCurrCodePopup.Value = hdCurrCode.Value
            hdVehicleCodePopup.Value = hdVehicleCode.Value

            Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
            Dim strDateFlag = "0"

            If hdcombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
                Dim dtselectedCombotour As New DataTable
                dtselectedCombotour = Session("selectedCombotourdatatable")

                Dim foundRow As DataRow
                foundRow = dtselectedCombotour.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' ").FirstOrDefault
                If foundRow Is Nothing Then
                    strDateFlag = "1"
                End If



                Dim foundRow1 As DataRow
                Dim strType As String = ""
                If hdcombo.Value = "YES" Then
                    strType = "COMBO"
                End If
                If hdMultipleDates.Value = "YES" Then
                    strType = "MULTI_DATE"
                End If

                foundRow1 = dtselectedCombotour.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND type='" & strType & "' ").FirstOrDefault
                If Not foundRow1 Is Nothing Then
                    txtTourChangeDate.Text = foundRow("excdate")
                End If


            Else
                If txtTourChangeDate.Text = "" Then
                    strDateFlag = "1"
                End If
            End If


            If strDateFlag = "1" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Select date for price.")
            Else
                lblTotlaPriceHeading.Text = lblExcName.Text
                objBLLTourSearch = New BLLTourSearch
                If Session("sobjBLLTourSearch") Is Nothing Then
                    Response.Redirect("Home.aspx?Tab=1")
                End If
                objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
                objBLLTourSearch.DateChange = "1"
                objBLLTourSearch.ExcTypeCode = hdExcCode.Value
                objBLLTourSearch.VehicleCode = hdVehicleCode.Value
                objBLLTourSearch.SelectedDate = txtTourChangeDate.Text

                Dim strDate As String = txtTourChangeDate.Text
                If strDate <> "" Then
                    Dim strDates As String() = strDate.Split("/")
                    strDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If

                hdSelectedDatePopup.Value = strDate

                Dim sDt As New DataTable

                Dim dsTourPriceResults As New DataSet
                If Not Session("sdtTourPriceBreakup") Is Nothing Then
                    sDt = Session("sdtTourPriceBreakup")
                    If sDt.Rows.Count > 0 Then
                        Dim dvSDt As DataView = New DataView(sDt)
                        dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' AND vehiclecode='" & hdVehicleCode.Value & "' "
                        'dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
                        If dvSDt.Count = 0 Then
                            Dim ds As New DataSet
                            ds = objBLLTourSearch.GetSearchDetails()
                            Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'  AND vehiclecode='" & hdVehicleCode.Value & "' ").First
                            'Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'").First
                            Dim drNew As DataRow = sDt.NewRow()
                            drNew.ItemArray = dr.ItemArray
                            sDt.Rows.Add(drNew)
                            Session("sdtTourPriceBreakup") = sDt
                        Else
                            Session("sdtTourPriceBreakup") = sDt
                        End If
                    Else
                        dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
                        sDt = dsTourPriceResults.Tables(0)
                        Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
                    End If


                Else
                    dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
                    sDt = dsTourPriceResults.Tables(0)
                    Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
                End If





                If sDt.Rows.Count > 0 Then

                    Dim dvSDt As DataView = New DataView(sDt)
                    dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "'  AND vehiclecode='" & hdVehicleCode.Value & "'  "
                    ' dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
                    If hdRateBasis.Value = "ACS" Then
                        dvACS.Visible = True
                        dvUnits.Visible = False
                        lblNoOfAdult.Text = dvSDt.Item(0)("adults").ToString
                        lblNoOfchild.Text = dvSDt.Item(0)("child").ToString
                        lblNoOfSeniors.Text = dvSDt.Item(0)("senior").ToString
                        lblNoOfUnits.Text = ""


                        txtAdultPrice.Text = dvSDt.Item(0)("adultprice").ToString
                        txtChildprice.Text = dvSDt.Item(0)("childprice").ToString
                        txtSeniorsPrice.Text = dvSDt.Item(0)("seniorprice").ToString

                       


                        txtUnitPrice.Text = ""


                        lblAdultSaleValue.Text = dvSDt.Item(0)("adultsalevalue").ToString
                        lblchildSaleValue.Text = dvSDt.Item(0)("childsalevalue").ToString
                        lblSeniorSaleValue.Text = dvSDt.Item(0)("seniorsalevalue").ToString
                        lblUnitSaleValue.Text = ""

                        ''' Added shahul 27/03/18
                        lblNoOfchildasadult.Text = dvSDt.Item(0)("childasadult").ToString
                        txtChildasadultprice.Text = dvSDt.Item(0)("childasadultprice").ToString
                        lblchildasadultSaleValue.Text = dvSDt.Item(0)("childasadultvalue").ToString



                        txtwlUnitPrice.Text = ""


                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc"))) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        txtwlAdultPrice.Text = Math.Round(dwlAdultprice)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        txtwlChildprice.Text = Math.Round(dwlChildtprice)

                        Dim dSeniorprice As Decimal = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
                        Dim dwlSeniorprice As Decimal
                        dwlSeniorprice = dSeniorprice * dWlMarkup
                        txtwlSeniorsPrice.Text = Math.Round(dwlSeniorprice)

                        ''' Added shahul 27/03/18
                        Dim dChildasadultprice As Decimal = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
                        Dim dwlChildasadulttprice As Decimal
                        dwlChildasadulttprice = dChildasadultprice * dWlMarkup
                        txtwlChildasadultprice.Text = Math.Round(dwlChildasadulttprice)



                        Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup)) * Val(lblNoOfAdult.Text)
                        txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        ' dwlChildSaleValue = dChildSaleValue * dWlMarkup
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup)) * Val(lblNoOfchild.Text)
                        txtwlChildSaleValue.Text = Math.Round(dwlChildSaleValue)

                        Dim dSeniorSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                        Dim dwlSeniorSaleValue As Decimal
                        'dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
                        dwlSeniorSaleValue = (Math.Round(dSeniorprice * dWlMarkup)) * Val(lblNoOfSeniors.Text)
                        txtwlSeniorsPrice.Text = Math.Round(dwlSeniorSaleValue)

                        ''' Added shahul 27/03/18
                        Dim dChildasadultSaleValue As Decimal = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)
                        Dim dwlChildasadultSaleValue As Decimal
                        dwlChildasadultSaleValue = (Math.Round(dwlChildasadulttprice * dWlMarkup)) * Val(lblNoOfchildasadult.Text)
                        txtwlChildasadultSaleValue.Text = Math.Round(dwlChildasadultSaleValue)



                        If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "block")
                            txtChildprice.Style.Add("display", "block")
                            txtSeniorsPrice.Style.Add("display", "block")
                            txtUnitPrice.Style.Add("display", "none")

                            ''' Added shahul 27/03/18
                            txtChildasadultprice.Style.Add("display", "block")
                            lblchildasadultSaleValue.Style.Add("display", "block")
                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")


                            lblAdultSaleValue.Style.Add("display", "block")
                            lblchildSaleValue.Style.Add("display", "block")
                            lblSeniorSaleValue.Style.Add("display", "block")
                            lblUnitSaleValue.Style.Add("display", "none")

                        ElseIf hdWhiteLabel.Value = "1" Then
                            txtwlAdultPrice.Style.Add("display", "block")
                            txtwlChildprice.Style.Add("display", "block")
                            txtwlSeniorsPrice.Style.Add("display", "block")

                            txtwlAdultSaleValue.Style.Add("display", "block")
                            txtwlChildSaleValue.Style.Add("display", "block")
                            txtwlSeniorSaleValue.Style.Add("display", "block")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "none")

                            ''' Added shahul 27/03/18
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")
                            txtwlChildasadultprice.Style.Add("display", "block")
                            txtwlChildasadultSaleValue.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "none")
                        Else
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "block")
                            txtChildprice.Style.Add("display", "block")
                            txtSeniorsPrice.Style.Add("display", "block")
                            txtUnitPrice.Style.Add("display", "none")

                            ''' Added shahul 27/03/18
                            txtChildasadultprice.Style.Add("display", "block")
                            lblchildasadultSaleValue.Style.Add("display", "block")
                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")


                            lblAdultSaleValue.Style.Add("display", "block")
                            lblchildSaleValue.Style.Add("display", "block")
                            lblSeniorSaleValue.Style.Add("display", "block")
                            lblUnitSaleValue.Style.Add("display", "none")
                        End If

                    Else
                        dvACS.Visible = False
                        dvUnits.Visible = True
                        lblNoOfAdult.Text = ""
                        lblNoOfchild.Text = ""
                        lblNoOfSeniors.Text = ""
                        ''' Added shahul 27/03/18
                        lblNoOfchildasadult.Text = ""

                        lblNoOfUnits.Text = dvSDt.Item(0)("units").ToString

                        txtAdultPrice.Text = ""
                        txtChildprice.Text = ""
                        txtSeniorsPrice.Text = ""

                        ''' Added shahul 27/03/18
                        txtChildasadultprice.Text = ""

                        txtUnitPrice.Text = dvSDt.Item(0)("unitprice").ToString


                        lblAdultSaleValue.Text = ""
                        lblchildSaleValue.Text = ""
                        lblSeniorSaleValue.Text = ""
                        ''' Added shahul 27/03/18
                        lblchildasadultSaleValue.Text = ""

                        lblUnitSaleValue.Text = dvSDt.Item(0)("unitsalevalue").ToString

                        txtwlAdultPrice.Text = ""
                        txtwlChildprice.Text = ""
                        txtwlSeniorsPrice.Text = ""
                        txtwlChildasadultprice.Text = ""
                        ' txtwlUnitPrice.Text = dvSDt.Item(0)("unitcprice").ToString


                        txtwlAdultSaleValue.Text = ""
                        txtwlChildSaleValue.Text = ""
                        txtwlSeniorSaleValue.Text = ""
                        ''' Added shahul 27/03/18
                        txtwlChildasadultSaleValue.Text = ""
                        ' txtwlUnitSaleValue.Text = dvSDt.Item(0)("wlunitsalevalue").ToString

                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc"))) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        txtwlUnitPrice.Text = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(lblNoOfUnits.Text)
                        txtwlUnitSaleValue.Text = Math.Round(dwlUnitSaleValue)


                        If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "block")

                            ''' Added shahul 27/03/18
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")
                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")

                        ElseIf hdWhiteLabel.Value = "1" Then
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "block")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "block")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "none")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "none")

                            ''' Added shahul 27/03/18
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")
                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")

                        Else
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "block")

                            ''' Added shahul 27/03/18
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")
                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")

                        End If

                    End If

                    If dvSDt.Item(0)("comp_cust").ToString() = "1" Then
                        chkComplimentaryToCustomer.Checked = True
                    Else
                        chkComplimentaryToCustomer.Checked = False
                    End If


                    If Session("sLoginType") = "RO" Then
                        dvComplimentaryToCustomer.Visible = True
                        If chkTourOveridePrice.Checked = True Then
                            txtUnitPrice.ReadOnly = False
                            txtAdultPrice.ReadOnly = False
                            txtChildprice.ReadOnly = False
                            txtSeniorsPrice.ReadOnly = False
                            txtChildasadultprice.ReadOnly = False ''' Added shahul 27/03/18
                            lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")


                        Else
                            txtUnitPrice.ReadOnly = True
                            txtAdultPrice.ReadOnly = True
                            txtChildprice.ReadOnly = True
                            txtSeniorsPrice.ReadOnly = True
                            txtChildasadultprice.ReadOnly = True ''' Added shahul 27/03/18

                            lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

                        End If

                        txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(lblAdultSaleValue.ClientID, String) + "')")
                        txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValueForChild('" + CType(lblNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(lblchildSaleValue.ClientID, String) + "','" + CType(hdChildAsFreePopup.Value, String) + "')")

                        txtSeniorsPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfSeniors.ClientID, String) + "', '" + CType(txtSeniorsPrice.ClientID, String) + "' ,'" + CType(lblSeniorSaleValue.ClientID, String) + "')")
                        txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")
                        ''' Added shahul 27/03/18
                        txtChildasadultprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchildasadult.ClientID, String) + "', '" + CType(txtChildasadultprice.ClientID, String) + "' ,'" + CType(lblchildasadultSaleValue.ClientID, String) + "')")

                    Else
                        dvComplimentaryToCustomer.Visible = False
                        txtUnitPrice.ReadOnly = True
                        txtAdultPrice.ReadOnly = True
                        txtChildprice.ReadOnly = True
                        txtSeniorsPrice.ReadOnly = True
                        txtChildasadultprice.ReadOnly = True ''' Added shahul 27/03/18
                        lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

                    End If

                    If hdBookingEngineRateType.Value = "1" Then

                        dvUnitprice.Style.Add("display", "none")
                        dvunitsalevalue.Style.Add("display", "none")
                    End If
                    mpTotalprice.Show()
                End If
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: lbPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Sub CreateSelectedTourDataTable()
        'Session("selectedtourdatatable") = Nothing
        Dim SelectExcDT As DataTable = New DataTable("SelectedExc")
        SelectExcDT.Columns.Add("exctypcode", Type.GetType("System.String"))
        SelectExcDT.Columns.Add("excdate", Type.GetType("System.String"))
        SelectExcDT.Columns.Add("vehiclecode", Type.GetType("System.String"))
        Session("selectedtourdatatable") = SelectExcDT
    End Sub
    Private Sub Toursearch()
        Try


            Dim objBLLTourSearch As New BLLTourSearch
            Dim strSearchCriteria As String = ""
            Dim strFromDate As String = txtTourFromDate.Text
            Dim strToDate As String = txtTourToDate.Text
            Dim strTourStartingFrom As String = txtTourStartingFrom.Text
            Dim strTourStartingFromCode As String = txtTourStartingFromCode.Text
            Dim strTourClassification As String = txtTourClassification.Text
            Dim strTourClassificationCode As String = txtTourClassificationCode.Text

            Dim strSourceCountry As String = txtTourSourceCountry.Text
            Dim strSourceCountryCode As String = txtTourSourceCountryCode.Text
            Dim strCustomer As String = txtTourCustomer.Text
            Dim strCustomerCode As String = txtTourCustomerCode.Text
            Dim strStarCategoryCode As String = ddlStarCategory.SelectedValue
            Dim strStarCategory As String = ddlStarCategory.Text
            Dim strSeniorCitizen As String = ddlSeniorCitizen.SelectedValue
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
            Dim strChild9 As String = txtTourChild9.Text




            'If HttpContext.Current.Session("sLoginType") = "RO" Then

            '    If chkTourOveridePrice.Checked = True Then
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
                ElseIf strChildren = "9" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Or strChild8 = "" Or strChild9 = "" Then
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
                objBLLTourSearch.FromDate = strFromDate
                strSearchCriteria = strSearchCriteria & "||" & "FromDate:" & strFromDate
            End If
            If strToDate <> "" Then
                objBLLTourSearch.ToDate = strToDate
                strSearchCriteria = strSearchCriteria & "||" & "ToDate:" & strToDate
            End If

            If strTourStartingFrom <> "" Then
                objBLLTourSearch.TourStartingFrom = strTourStartingFrom
                strSearchCriteria = strSearchCriteria & "||" & "TourStartingFrom:" & strTourStartingFrom
            End If
            If strTourStartingFromCode <> "" Then
                objBLLTourSearch.TourStartingFromCode = strTourStartingFromCode
            End If

            Dim strPrivateOrSIC As String = ""
            'If rblPrivateOrSIC.SelectedValue.Contains("SIC") = True Then
            '    strPrivateOrSIC = "SIC"

            'Else
            '    strPrivateOrSIC = rblPrivateOrSIC.SelectedValue
            'End If
            If chklPrivateOrSIC.Items.Count > 0 Then
                For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                    If chklPrivateOrSIC.Items(i).Selected = True Then
                        If strPrivateOrSIC = "" Then
                            strPrivateOrSIC = chklPrivateOrSIC.Items(i).Value
                        Else
                            strPrivateOrSIC = strPrivateOrSIC & "," & chklPrivateOrSIC.Items(i).Value
                        End If
                    End If
                Next
            End If

            strSearchCriteria = strSearchCriteria & "||" & "PrivateOrSIC:" & strPrivateOrSIC
            Dim strOveride As String = chkTourOveridePrice.Checked

            If strOveride = "True" Then
                strSearchCriteria = strSearchCriteria & "||" & "OveridePrice:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & "OveridePrice:No"
            End If

            If strTourClassification <> "" Then
                objBLLTourSearch.Classification = strTourClassification
                strSearchCriteria = strSearchCriteria & "||" & "Classification:" & strTourClassification
            End If
            If strTourClassificationCode <> "" Then
                objBLLTourSearch.ClassificationCode = strTourClassificationCode
            End If

            If strStarCategoryCode <> "" Then
                objBLLTourSearch.StarCategoryCode = strStarCategoryCode
            End If
            If strStarCategory <> "" Then
                objBLLTourSearch.StarCategory = strStarCategory
                strSearchCriteria = strSearchCriteria & "||" & "StarCategory:" & strStarCategory
            End If

            If Not Session("sEditRequestId") Is Nothing Then
                objBLLTourSearch.AmendRequestid = Session("sEditRequestId")
                objBLLTourSearch.AmendLineno = ViewState("Elineno")
                strSearchCriteria = strSearchCriteria & "||" & "AmendLineno:" & objBLLTourSearch.AmendLineno
                strSearchCriteria = strSearchCriteria & "||" & "AmendRequestid:" & objBLLTourSearch.AmendRequestid
            Else
                objBLLTourSearch.AmendRequestid = GetExistingRequestId()
                objBLLTourSearch.AmendLineno = ViewState("Elineno")
                strSearchCriteria = strSearchCriteria & "||" & "AmendLineno:" & objBLLTourSearch.AmendLineno
                strSearchCriteria = strSearchCriteria & "||" & "AmendRequestid:" & objBLLTourSearch.AmendRequestid
            End If


            If strSeniorCitizen <> "" Then
                objBLLTourSearch.SeniorCitizen = strSeniorCitizen
                strSearchCriteria = strSearchCriteria & "||" & "SeniorCitizen:" & objBLLTourSearch.SeniorCitizen
            End If

            If strAdult <> "" Then
                objBLLTourSearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "||" & "Adult:" & strAdult
            End If
            If strChildren <> "" Then
                objBLLTourSearch.Children = strChildren
                If strChildren = "1" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.ChildAgeString = strChild1
                    objBLLTourSearch.Child2 = ""
                    objBLLTourSearch.Child3 = ""
                    objBLLTourSearch.Child4 = ""
                    objBLLTourSearch.Child5 = ""
                    objBLLTourSearch.Child6 = ""
                    objBLLTourSearch.Child7 = ""
                    objBLLTourSearch.Child8 = ""
                    objBLLTourSearch.Child9 = ""
                ElseIf strChildren = "2" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = ""
                    objBLLTourSearch.Child4 = ""
                    objBLLTourSearch.Child5 = ""
                    objBLLTourSearch.Child6 = ""
                    objBLLTourSearch.Child7 = ""
                    objBLLTourSearch.Child8 = ""
                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = ""
                    objBLLTourSearch.Child5 = ""
                    objBLLTourSearch.Child6 = ""
                    objBLLTourSearch.Child7 = ""
                    objBLLTourSearch.Child8 = ""
                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = ""
                    objBLLTourSearch.Child6 = ""
                    objBLLTourSearch.Child7 = ""
                    objBLLTourSearch.Child8 = ""
                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = ""
                    objBLLTourSearch.Child7 = ""
                    objBLLTourSearch.Child8 = ""
                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = strChild6
                    objBLLTourSearch.Child7 = ""
                    objBLLTourSearch.Child8 = ""
                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = strChild6
                    objBLLTourSearch.Child7 = strChild7
                    objBLLTourSearch.Child8 = ""

                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7
                ElseIf strChildren = "8" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = strChild6
                    objBLLTourSearch.Child7 = strChild7
                    objBLLTourSearch.Child8 = strChild8
                    objBLLTourSearch.Child9 = ""
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
                ElseIf strChildren = "9" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = strChild6
                    objBLLTourSearch.Child7 = strChild7
                    objBLLTourSearch.Child8 = strChild8
                    objBLLTourSearch.Child9 = strChild9
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8 & ";" & strChild9
                End If
            End If
            strSearchCriteria = strSearchCriteria & "||" & "ChildAgeString:" & objBLLTourSearch.ChildAgeString
            If strSourceCountry <> "" Then
                objBLLTourSearch.SourceCountry = strSourceCountry
                strSearchCriteria = strSearchCriteria & "||" & "SourceCountry:" & objBLLTourSearch.SourceCountry
            End If
            If strSourceCountryCode <> "" Then
                objBLLTourSearch.SourceCountryCode = strSourceCountryCode
            End If

            If strCustomer <> "" Then
                objBLLTourSearch.Customer = strCustomer
                strSearchCriteria = strSearchCriteria & "||" & "Agent:" & objBLLTourSearch.Customer
            End If
            If strCustomerCode <> "" Then
                objBLLTourSearch.CustomerCode = strCustomerCode
            End If

            If strStarCategory <> "" Then
                objBLLTourSearch.StarCategory = strStarCategory
                strSearchCriteria = strSearchCriteria & "||" & "StarCategory:" & objBLLTourSearch.StarCategory
            End If
            If strPrivateOrSIC <> "" Then
                objBLLTourSearch.PrivateOrSIC = strPrivateOrSIC
            End If

            Dim dt As DataTable
            Dim objBLLHotelSearch As New BLLHotelSearch
            Dim strRequestId As String = ""
            If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
                objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
                strRequestId = objBLLHotelSearch.OBrequestid
                dt = objBLLHotelSearch.GetSectorCheckInAndCheckOutDetails(strRequestId, objBLLTourSearch.TourStartingFromCode)
                If dt.Rows.Count > 0 Then
                    hdChangeFromdate.Value = dt.Rows(0)("CheckIn").ToString
                    hdChangeTodate.Value = dt.Rows(0)("CheckOut").ToString
                End If
            Else
                hdChangeFromdate.Value = txtTourFromDate.Text
                hdChangeTodate.Value = txtTourToDate.Text

            End If

            objBLLTourSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & "LoginType:" & objBLLTourSearch.LoginType
            objBLLTourSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTourSearch.CustomerCode, Session("sAgentCode"))
            strSearchCriteria = strSearchCriteria & "||" & "AgentCode:" & objBLLTourSearch.AgentCode
            objBLLTourSearch.WebuserName = Session("GlobalUserName")
            If chkTourOveridePrice.Checked = True Then
                objBLLTourSearch.OverridePrice = "1"
            Else
                objBLLTourSearch.OverridePrice = "0"
            End If
            Session("sobjBLLTourSearch") = objBLLTourSearch
            Dim dsTourSearchResults As New DataSet
            objBLLTourSearch.DateChange = "0"


            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                Dim objBLLCommonFuntions As New BLLCommonFuntions()
                '  Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Tour Search Page", "Tour Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            dsTourSearchResults = objBLLTourSearch.GetSearchDetails()
            If dsTourSearchResults Is Nothing Then
                dvhotnoshow.Style.Add("display", "block")
                dlTourSearchResults.DataBind()
                Exit Sub
            End If
            If dsTourSearchResults.Tables(0).Rows.Count = 0 Then
                dvhotnoshow.Style.Add("display", "block")
            Else
                dvhotnoshow.Style.Add("display", "none")
            End If

            Session("sDSTourSearchResults") = dsTourSearchResults
            If dsTourSearchResults.Tables.Count > 0 Then

                BindTourPricefilter(dsTourSearchResults.Tables(1))
                BindTourHotelStars(dsTourSearchResults.Tables(2))
                BindTourRoomClassification(dsTourSearchResults.Tables(3))
                Session("sDSTourSearchResults") = dsTourSearchResults
                Session("sTourPageIndex") = "1"
                Dim dvMaiDetails As DataView = New DataView(dsTourSearchResults.Tables(0))
                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "tourselected DESC, excname ASC" 'changed by mohamed on 12/02/2018 --tourselected desc is added
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "tourselected DESC, totalsalevalue ASC" 'changed by mohamed on 12/02/2018 --tourselected desc is added
                ElseIf ddlSorting.Text = "Preferred" Then
                    dvMaiDetails.Sort = "tourselected DESC,Preferred desc, excname ASC"
                ElseIf ddlSorting.Text = "Rating" Then
                    dvMaiDetails.Sort = "tourselected DESC, starcategory DESC,excname ASC " 'changed by mohamed on 12/02/2018 --tourselected desc is added
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
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnTourSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub btnTourSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTourSearch.Click
        Try
            If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
                Session("sSelectedTourTimeSlotDatatable") = Nothing
            End If

            'changed by mohamed on 12/02/2018
            txtSearchTour.Text = ""
            txtSearchTour.Enabled = True
            btnTourTextSearch.Enabled = True
            ddlSorting.Enabled = True

            CreateSelectedTourDataTable()
            CreateComboTourDataTable()
            If Not Session("sRequestId") Is Nothing Then
                If (objBLLTourSearch.ValidateTourSearchDateGaps(Session("sRequestId"), txtTourFromDate.Text, txtTourToDate.Text)) = False Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "The search date should be in continuity with previous booking date range.")
                    Exit Sub
                End If
            End If

            Toursearch()
            txtSearchFocus.Focus()

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnTourSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
   
    Protected Sub btnPriceBreakupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupSave.Click
        Try

            Dim fTotalSaleValue As Double = 0
            Dim dwlTotalSaleValue As Double = 0
            If hdRateBasisPopup.Value = "ACS" Then
                fTotalSaleValue = CType(IIf(Val(lblNoOfAdult.Text) = 0, "0", lblAdultSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchild.Text) = 0, "0", lblchildSaleValue.Text), Double) + CType(IIf(Val(lblNoOfSeniors.Text) = 0, "0", lblSeniorSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchildasadult.Text) = 0, "0", lblchildasadultSaleValue.Text), Double)
                dwlTotalSaleValue = CType(IIf(Val(lblNoOfAdult.Text) = 0, "0", txtwlAdultSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchild.Text) = 0, "0", txtwlChildSaleValue.Text), Double) + CType(IIf(Val(lblNoOfSeniors.Text) = 0, "0", txtwlSeniorSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchildasadult.Text) = 0, "0", txtwlChildasadultSaleValue.Text), Double)
            Else
                fTotalSaleValue = IIf(lblUnitSaleValue.Text = "", "0", lblUnitSaleValue.Text)
                dwlTotalSaleValue = IIf(txtwlUnitSaleValue.Text = "", "0", txtwlUnitSaleValue.Text)
            End If


            Dim ds As DataSet
            ds = Session("sDSTourSearchResults")

            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCodePopup.Value & "'  AND vehiclecode='" & hdVehicleCodePopup.Value & "' ").First
                ' Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCodePopup.Value & "' ").First


                dr("totalsalevalue") = fTotalSaleValue.ToString.Replace(".00", "").Replace(".0", "")
                dr("adultprice") = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                dr("childprice") = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)

                'Added shahul 27/03/18
                dr("childasadultprice") = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
                dr("childasadultvalue") = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)


                dr("seniorprice") = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
                dr("unitprice") = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                dr("units") = IIf(lblNoOfUnits.Text = "", "0", lblNoOfUnits.Text)

                dr("adultsalevalue") = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
                dr("childsalevalue") = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                dr("seniorsalevalue") = IIf(lblSeniorSaleValue.Text = "", "0.00", lblSeniorSaleValue.Text)
                dr("unitsalevalue") = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)


                Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                Dim dwlAdultprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                dwlAdultprice = dAdultprice * dWlMarkup
                ' dr("adultcprice") = dwlAdultprice

                Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                Dim dwlChildtprice As Decimal
                dwlChildtprice = dChildprice * dWlMarkup
                ' dr("childcprice") = dwlChildtprice

                'Added shahul 27/03/18
                Dim dChildasadultprice As Decimal = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
                Dim dwlChildasadulttprice As Decimal
                dwlChildasadulttprice = dChildasadultprice * dWlMarkup

                Dim dSeniorprice As Decimal = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
                Dim dwlSeniorprice As Decimal
                dwlSeniorprice = dSeniorprice * dWlMarkup
                ' dr("seniorcprice") = dwlSeniorprice


                Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                Dim dwlUnitPrice As Decimal
                dwlUnitPrice = dUnitPrice * dWlMarkup
                ' dr("unitcprice") = dwlUnitPrice

                Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
                Dim dwlAdultSaleValue As Decimal
                dwlAdultSaleValue = dAdultSaleValue * dWlMarkup
                dr("wlAdultSaleValue") = dwlAdultSaleValue

                Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                Dim dwlChildSaleValue As Decimal
                dwlChildSaleValue = dChildSaleValue * dWlMarkup
                dr("wlChildSaleValue") = dwlChildSaleValue

                'Added shahul 27/03/18
                Dim dChildadultSaleValue As Decimal = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)
                Dim dwlChildadultSaleValue As Decimal
                dwlChildadultSaleValue = dChildadultSaleValue * dWlMarkup
                dr("wlchildasadultvalue") = dwlChildadultSaleValue

                Dim dSeniorSaleValue As Decimal = IIf(lblSeniorSaleValue.Text = "", "0.00", lblSeniorSaleValue.Text)
                Dim dwlSeniorSaleValue As Decimal
                dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
                dr("wlSeniorSaleValue") = dwlSeniorSaleValue

                Dim dUnitSaleValue As Decimal = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)
                Dim dwlUnitSaleValue As Decimal
                dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                dr("wlUnitSaleValue") = dwlUnitSaleValue
                If hdRateBasisPopup.Value = "ACS" Then
                    dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(lblNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(lblNoOfchild.Text)) + Math.Round((Math.Round(dChildasadultprice * dWlMarkup, 2)) * Val(lblNoOfchildasadult.Text)) + Math.Round((Math.Round(dSeniorprice * dWlMarkup, 2)) * Val(lblNoOfSeniors.Text))
                Else
                    dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(lblNoOfUnits.Text))
                End If


                '  dr("wltotalsalevalue") = dwlTotalSaleValue ' fTotalSaleValue * dWlMarkup

                'Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
                'Dim dwlAdultSaleValue As Decimal
                'dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup)) * Val(lblNoOfAdult.Text)
                'txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

                'Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                'Dim dwlChildSaleValue As Decimal
                '' dwlChildSaleValue = dChildSaleValue * dWlMarkup
                'dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup)) * Val(lblNoOfchild.Text)
                'txtwlChildSaleValue.Text = Math.Round(dwlChildSaleValue)

                'Dim dSeniorSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                'Dim dwlSeniorSaleValue As Decimal
                'dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
                'dwlSeniorSaleValue = (Math.Round(dSeniorprice * dWlMarkup)) * Val(lblNoOfSeniors.Text)
                'txtwlSeniorsPrice.Text = Math.Round(dwlSeniorSaleValue)




                If chkComplimentaryToCustomer.Checked = True Then
                    dr("comp_cust") = "1"
                Else
                    dr("comp_cust") = "0"
                End If
            End If
            Dim dtTourPriceBreakup As New DataTable
            If Not Session("sdtTourPriceBreakup") Is Nothing Then
                dtTourPriceBreakup = Session("sdtTourPriceBreakup")
                If dtTourPriceBreakup.Rows.Count > 0 Then
                    Dim dr As DataRow = dtTourPriceBreakup.Select("exctypcode='" & hdExcCodePopup.Value & "' and selecteddate='" & hdSelectedDatePopup.Value & "'  AND vehiclecode='" & hdVehicleCodePopup.Value & "' ").First
                    ' Dim dr As DataRow = dtTourPriceBreakup.Select("exctypcode='" & hdExcCodePopup.Value & "' and selecteddate='" & hdSelectedDatePopup.Value & "' ").First

                    dr("adultprice") = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                    dr("childprice") = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                    dr("seniorprice") = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
                    dr("unitprice") = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                    dr("units") = IIf(lblNoOfUnits.Text = "", "0", lblNoOfUnits.Text)

                    'Added shahul 27/03/18
                    dr("childasadultprice") = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
                    dr("childasadultvalue") = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)

                    dr("adultsalevalue") = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
                    dr("childsalevalue") = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                    dr("seniorsalevalue") = IIf(lblSeniorSaleValue.Text = "", "0.00", lblSeniorSaleValue.Text)
                    dr("unitsalevalue") = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)

                    '  If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

                    Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                    Dim dwlAdultprice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                    dwlAdultprice = dAdultprice * dWlMarkup
                    dr("adultcprice") = dwlAdultprice

                    Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                    Dim dwlChildtprice As Decimal
                    dwlChildtprice = dChildprice * dWlMarkup
                    dr("childcprice") = dwlChildtprice

                    Dim dChildadultprice As Decimal = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
                    Dim dwlChildadulttprice As Decimal
                    dwlChildadulttprice = dChildadultprice * dWlMarkup
                    dr("childasadultcprice") = dwlChildadulttprice

                    Dim dSeniorprice As Decimal = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
                    Dim dwlSeniorprice As Decimal
                    dwlSeniorprice = dSeniorprice * dWlMarkup
                    dr("seniorcprice") = dwlSeniorprice


                    Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                    Dim dwlUnitPrice As Decimal
                    dwlUnitPrice = dUnitPrice * dWlMarkup
                    dr("unitcprice") = dwlUnitPrice

                    Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
                    Dim dwlAdultSaleValue As Decimal
                    dwlAdultSaleValue = dAdultSaleValue * dWlMarkup
                    dr("wlAdultSaleValue") = dwlAdultSaleValue

                    Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                    Dim dwlChildSaleValue As Decimal
                    dwlChildSaleValue = dChildSaleValue * dWlMarkup
                    dr("wlChildSaleValue") = dwlChildSaleValue

                    'Added shahul 27/03/18
                    Dim dChildadultSaleValue As Decimal = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)
                    Dim dwlChildadultSaleValue As Decimal
                    dwlChildadultSaleValue = dChildadultSaleValue * dWlMarkup
                    dr("wlchildasadultvalue") = dwlChildadultSaleValue


                    Dim dSeniorSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
                    Dim dwlSeniorSaleValue As Decimal
                    dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
                    dr("wlSeniorSaleValue") = dwlSeniorSaleValue

                    Dim dUnitSaleValue As Decimal = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)
                    Dim dwlUnitSaleValue As Decimal
                    dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                    dr("wlUnitSaleValue") = dwlUnitSaleValue

                    dr("wltotalsalevalue") = dwlTotalSaleValue ' fTotalSaleValue * dWlMarkup
                    'End If

                    If chkComplimentaryToCustomer.Checked = True Then
                        dr("comp_cust") = "1"
                    Else
                        dr("comp_cust") = "0"
                    End If
                End If

            End If


            Session("sDSTourSearchResults") = ds
            Session("sdtTourPriceBreakup") = dtTourPriceBreakup

            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbTourTotalSaleValue"), LinkButton)

            Dim dlItem As DataListItem = CType((lbTotalPrice).NamingContainer, DataListItem)
            Dim lbPrice As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lbPrice"), LinkButton)
            Dim lblunits As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lblunits"), LinkButton)

            If hdMultiDay.Value = "YES" Then
                Dim dtselectedCombotour As New DataTable
                dtselectedCombotour = Session("selectedCombotourdatatable")
                Dim strType As String = "MULTI_DATE"

                Dim dvComboBreakup As DataView
                dvComboBreakup = New DataView(dtselectedCombotour)
                dvComboBreakup.RowFilter = "exctypcode= '" & hdExcCodePopup.Value & "'  AND  vehiclecode='" & hdVehicleCodePopup.Value & "'  AND type='" & strType & "' "
                Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
                If dtComboBreakup.Rows.Count > 1 Then
                    fTotalSaleValue = fTotalSaleValue * dtComboBreakup.Rows.Count
                End If
            End If

            lbPrice.Text = hdCurrCodePopup.Value & " " & fTotalSaleValue.ToString()

            lblunits.Text = lblNoOfUnits.Text + " Units"




        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnPriceBreakupSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Function Validatedetails() As String
        Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
        Validatedetails = True
        If Not dtselectedtour Is Nothing Then
            If dtselectedtour.Rows.Count = 0 And Not ViewState("Elineno") Is Nothing Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Amend/Edit Option Please Select any Tour ")
                Return False
                Exit Function
            End If
            If dtselectedtour.Rows.Count > 0 Then


                For Each gvRow As DataListItem In dlTourSearchResults.Items
                    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
                    Dim chkSelectTour As CheckBox = gvRow.FindControl("chkSelectTour")
                    Dim hdExcCode As HiddenField = gvRow.FindControl("hdExcCode")
                    If chkSelectTour.Checked = True Then

                        Dim lblExcName As Label = CType(gvRow.FindControl("lblExcName"), Label)
                        '  Dim hdCurrCode As HiddenField = CType(gvRow.FindControl("hdCurrCode"), HiddenField)
                        '    hdCurrCodePopup.Value = hdCurrCode.Value
                        Dim hdCombo As HiddenField = CType(gvRow.FindControl("hdCombo"), HiddenField)
                        Dim hdMultipleDates As HiddenField = CType(gvRow.FindControl("hdMultipleDates"), HiddenField)

                        If hdCombo.Value <> "YES" And hdMultipleDates.Value <> "YES" Then
                            Dim strFlag As String = "0"
                            For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                                If dtselectedtour.Rows(i)("exctypcode") = hdExcCode.Value.ToString Then
                                    strFlag = "1"
                                    Exit For
                                End If
                            Next
                            If strFlag = "0" Then
                                MessageBox.ShowMessage(Page, MessageType.Warning, lblExcName.Text & " - date is not selected")
                                Return False
                            End If
                        End If
                        If hdCombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
                            Dim strFlag As String = "0"
                            Dim dtMultiDates As New DataTable
                            dtMultiDates = Session("selectedCombotourdatatable")

                            For i = dtMultiDates.Rows.Count - 1 To 0 Step -1
                                If dtMultiDates.Rows(i)("exctypcode") = hdExcCode.Value.ToString Then
                                    strFlag = "1"
                                    Exit For
                                End If
                            Next
                            If strFlag = "0" Then
                                MessageBox.ShowMessage(Page, MessageType.Warning, lblExcName.Text & " - date is not seleted")
                                Return False
                            End If
                        End If


                    End If

                Next


            End If

        End If


    End Function
    Protected Sub btnbooknow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbooknow.Click
        Try
            objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)

            Dim requestid As String = ""
            Dim sourcectrycode As String = ""
            Dim agentcode As String = ""
            Dim agentref As String = ""
            Dim columbusref As String = ""
            Dim remarks As String = ""

            If Validatedetails() Then

                requestid = GetNewOrExistingRequestId()


                Dim objBLLCommonFuntions = New BLLCommonFuntions
                Dim dt As DataTable
                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
                If dt.Rows.Count > 0 Then
                    objBLLTourSearch.AgentCode = dt.Rows(0)("agentcode").ToString
                    objBLLTourSearch.EBdiv_code = dt.Rows(0)("div_code").ToString
                    objBLLTourSearch.EbRequestID = requestid
                    objBLLTourSearch.EBsourcectrycode = dt.Rows(0)("sourcectrycode").ToString
                    objBLLTourSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTourSearch.CustomerCode, dt.Rows(0)("agentcode").ToString)

                    objBLLTourSearch.EBreqoverride = IIf(chkTourOveridePrice.Checked = True, "1", "0")
                    objBLLTourSearch.EBagentref = dt.Rows(0)("agentref").ToString
                    objBLLTourSearch.EBcolumbusref = dt.Rows(0)("ColumbusRef").ToString
                    objBLLTourSearch.EBremarks = dt.Rows(0)("remarks").ToString
                    objBLLTourSearch.EBuserlogged = Session("GlobalUserName")
                Else
                    objBLLTourSearch.AgentCode = Session("sAgentCode")
                    objBLLTourSearch.EBdiv_code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast where agentcode='" & Session("sAgentCode") & "'")
                    objBLLTourSearch.EbRequestID = requestid
                    objBLLTourSearch.EBsourcectrycode = IIf(sourcectrycode = "", objBLLTourSearch.SourceCountryCode, sourcectrycode)


                    objBLLTourSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTourSearch.CustomerCode, Session("sAgentCode"))

                    objBLLTourSearch.EBreqoverride = IIf(chkTourOveridePrice.Checked = True, "1", "0")
                    objBLLTourSearch.EBagentref = agentref
                    objBLLTourSearch.EBcolumbusref = columbusref
                    objBLLTourSearch.EBremarks = remarks
                    objBLLTourSearch.EBuserlogged = Session("GlobalUserName")
                End If

                Dim strSectorgroupCode As String = ""
                strSectorgroupCode = objBLLTourSearch.TourStartingFromCode
                Dim dsTourSearchResults As New DataSet
                dsTourSearchResults = CType(Session("sDSTourSearchResults"), DataSet)
                If dsTourSearchResults IsNot Nothing Then
                    Dim dv As DataView
                    dv = New DataView(dsTourSearchResults.Tables(0))




                    Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)

                    If dtselectedtour.Rows.Count = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any tour.")
                        Exit Sub
                    End If

                    For i As Integer = 0 To dtselectedtour.Rows.Count - 1
                        If dtselectedtour.Rows(i)("excdate").ToString = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select date.")
                            Exit Sub
                        End If
                    Next
                    ' Dim objBLLTourSearch1 As New BLLTourSearch
                    'For i As Integer = 0 To dtselectedtour.Rows.Count - 1


                    '    'Dim dtExcWeekDays As DataTable
                    '    'dtExcWeekDays = objBLLTourSearch1.ValidateExcWeekDays(dtselectedtour.Rows(i)("exctypcode").ToString, dtselectedtour.Rows(i)("excdate").ToString)
                    '    'If dtExcWeekDays.Rows.Count > 0 Then
                    '    '    Dim strExcName As String = ""
                    '    '    If dtExcWeekDays.Rows.Count > 0 Then
                    '    '        strExcName = dtExcWeekDays.Rows(0)("excname").ToString
                    '    '    End If

                    '    '    ' Dim strExcName As String = objBLLTourSearch1.ValidateExcWeekDays("ET/000009", "14/12/2017")
                    '    '    If strExcName <> "" Then
                    '    '        'Dim strMessage As String = "The " & strExcName & " tour is not available for the date " & dtselectedtour.Rows(i)("excdate").ToString & "."
                    '    '        'strMessage = strMessage & " </br></br> This tour applicable for " & dtExcWeekDays.Rows(0)("AvailableDays").ToString & "."

                    '    '        Dim strMessage As String = strExcName & " tour is not operational on " & dtExcWeekDays.Rows(0)("TourDayName").ToString & "(" & dtselectedtour.Rows(i)("excdate").ToString & ")."
                    '    '        strMessage = strMessage & " </br></br> Tour operational days:</br " & dtExcWeekDays.Rows(0)("AvailableDays").ToString & "."
                    '    '        MessageBox.ShowMessage(Page, MessageType.Alert, strMessage)

                    '    '        Exit Sub
                    '    '    End If
                    '    'End If



                    '    'Dim strExcName As String = objBLLTourSearch1.ValidateExcWeekDays(dtselectedtour.Rows(i)("exctypcode").ToString, dtselectedtour.Rows(i)("excdate").ToString)
                    '    '' Dim strExcName As String = objBLLTourSearch1.ValidateExcWeekDays("ET/000009", "14/12/2017")
                    '    'If strExcName <> "" Then
                    '    '    Dim strMessage As String = "The " & strExcName & " tour is not available for the date " & dtselectedtour.Rows(i)("excdate").ToString & "."
                    '    '    MessageBox.ShowMessage(Page, MessageType.Warning, strMessage)
                    '    '    Exit Sub
                    '    'End If
                    'Next

                    Dim lsStrQry As String = ""
                    Dim strElineno As String = ""
                    Dim strExcCodes As String = ""
                    For Each ltRow As DataRow In dtselectedtour.Rows
                        strExcCodes = IIf(strExcCodes = "", ltRow("exctypcode"), strExcCodes + "|" + ltRow("exctypcode"))
                        lsStrQry = IIf(lsStrQry = "", "", lsStrQry + " or ") & "(exctypcode='" & ltRow("exctypcode") & "' AND vehiclecode= '" & ltRow("vehiclecode") & "')"
                    Next
                    If strExcCodes <> "" Then
                        Dim dtInventory As DataTable
                        dtInventory = objclsUtilities.GetDataFromDataTable("exec sp_checkExcInventory '" + strExcCodes + "'")
                        If dtInventory.Rows.Count > 0 Then
                            For i As Integer = 0 To dtInventory.Rows.Count - 1
                                If dtInventory.Rows(i)("combo").ToString = "YES" Then
                                    If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
                                        Dim dtTSlot As DataTable = Session("sSelectedTourTimeSlotDatatable")
                                        If dtTSlot.Rows.Count > 0 Then
                                            Dim dvSelected As DataView
                                            dvSelected = New DataView(dtTSlot)
                                            dvSelected.RowFilter = "EXC_TYPE_COMBO_CODE='" & dtInventory.Rows(i)("exctypcombocode").ToString & "'"
                                            If dvSelected.Count = 0 Then
                                                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select time slot")
                                                Exit Sub
                                            End If
                                        Else
                                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select time slot")
                                            Exit Sub
                                        End If
                                    Else
                                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select time slot")
                                        Exit Sub

                                    End If
                                Else
                                    If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
                                        Dim dtTSlot As DataTable = Session("sSelectedTourTimeSlotDatatable")
                                        If dtTSlot.Rows.Count > 0 Then
                                            Dim dvSelected As DataView
                                            dvSelected = New DataView(dtTSlot)
                                            dvSelected.RowFilter = "EXC_CODE='" & dtInventory.Rows(i)("exctypcode").ToString & "'"
                                            If dvSelected.Count = 0 Then
                                                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select time slot")
                                                Exit Sub
                                            End If
                                        Else
                                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select time slot")
                                            Exit Sub
                                        End If
                                    Else
                                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select time slot")
                                        Exit Sub

                                    End If
                                End If
                            Next
                        End If
                    End If

                    'select exctypcode,InventoryId from excursionTypes_inventory where exctypcode in ('')
                    If lsStrQry.Trim <> "" Then
                        dv.RowFilter = lsStrQry
                        dv.Sort = "elineno asc"
                        Dim dtResult As DataTable = dv.ToTable
                        Dim rownum As Integer = 1
                        Dim strBuffer As New Text.StringBuilder
                        Dim strBufferMultiCostBreakup As New Text.StringBuilder
                        Dim strBufferCombo As New StringBuilder
                        Dim strBufferInventory As New StringBuilder

                        Dim strRowLineNo As String = ""
                        Dim strselectedrow As String = ""
                        strBuffer.Append("<DocumentElement>")
                        strBufferMultiCostBreakup.Append("<DocumentElement>")
                        strBufferCombo.Append("<DocumentElement>")
                        strBufferInventory.Append("<DocumentElement>")
                        If dtselectedtour.Rows.Count >= 1 Then
                            strselectedrow = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "TOUR")
                        End If



                        If dtResult.Rows.Count > 0 Then

                            For Each myrow As DataRow In dtResult.Rows


                                'If ViewState("Elineno") Is Nothing Then
                                If myrow("elineno").ToString = "0" Then
                                    If strElineno = "" Then
                                        strElineno = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "TOUR")
                                    Else
                                        strElineno = CType(strElineno, Integer) + 1
                                    End If

                                Else
                                    strElineno = myrow("elineno").ToString
                                    ' strElineno = CType(strElineno, Integer) + 1
                                End If


                                'Else
                                '    If Not ViewState("Elineno") Is Nothing Then
                                '        strElineno = ViewState("Elineno")
                                '    Else
                                '        strElineno = strselectedrow
                                '    End If

                                'End If


                                If strRowLineNo <> "" Then
                                    strRowLineNo = strRowLineNo + ";" + CType(strElineno, String)
                                Else
                                    strRowLineNo = strElineno
                                End If
                                Dim iNoOfDays As Integer = 1
                                If myrow("multipledatesyesno").ToString = "YES" Then
                                    Dim dtselectedCombotourNew As New DataTable
                                    dtselectedCombotourNew = Session("selectedCombotourdatatable")
                                    Dim strTypeNew As String = "MULTI_DATE"

                                    Dim dvComboBreakup As DataView
                                    dvComboBreakup = New DataView(dtselectedCombotourNew)
                                    dvComboBreakup.RowFilter = "exctypcode= '" & myrow("exctypcode").ToString & "'  AND  vehiclecode='" & myrow("vehiclecode").ToString & "'  AND type='" & strTypeNew & "' "
                                    Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
                                    If dtComboBreakup.Rows.Count > 1 Then
                                        iNoOfDays = dtComboBreakup.Rows.Count
                                    End If
                                End If


                                strBuffer.Append("<Table>")
                                strBuffer.Append(" <elineno>" & CType(strElineno, String) & "</elineno>")
                                strBuffer.Append("<exctypcode>" & myrow("exctypcode").ToString & "</exctypcode>")
                                strBuffer.Append("<sectorgroupcode>" & myrow("sectorgroupcode").ToString & "</sectorgroupcode>")

                                strBuffer.Append("<vehiclecode>" & myrow("vehiclecode").ToString & "</vehiclecode>")
                                Dim excdate As String = ""
                                For Each row As DataRow In dtselectedtour.Rows
                                    If row("exctypcode").ToString = myrow("exctypcode").ToString And row("vehiclecode").ToString = myrow("vehiclecode").ToString Then
                                        excdate = row("excdate").ToString
                                        If excdate <> "" Then
                                            Dim strDates As String() = excdate.Split("/")
                                            excdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                        End If

                                        Exit For

                                    End If
                                Next
                                If excdate.ToString = "" Or excdate.ToString = "yyyy/MM/dd" Then
                                    Dim strFromDate As String = txtTourFromDate.Text
                                    If strFromDate <> "" Then
                                        Dim strDates As String() = excdate.Split("/")
                                        strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                    End If

                                    strBuffer.Append("<excdate>" & Format(CType(strFromDate, Date), "yyyy/MM/dd") & "</excdate>")
                                Else
                                    strBuffer.Append("<excdate>" & Format(CType(excdate, Date), "yyyy/MM/dd") & "</excdate>")
                                End If

                                strBuffer.Append("<ratebasis>" & myrow("ratebasis").ToString & "</ratebasis>")
                                strBuffer.Append("<adults>" & myrow("adults").ToString & "</adults>")
                                strBuffer.Append("<child>" & myrow("child").ToString & "</child>")

                                Dim objBLLTourSearch As New BLLTourSearch
                                objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
                                strBuffer.Append("<childages>" & objBLLTourSearch.ChildAgeString & "</childages>")
                                strBuffer.Append("<senior>" & myrow("senior").ToString & "</senior>")
                                strBuffer.Append("<units>" & myrow("units").ToString & "</units>")
                                strBuffer.Append("<adultprice>" & myrow("adultprice").ToString & "</adultprice>")
                                strBuffer.Append("<childprice>" & myrow("childprice").ToString & "</childprice>")
                                strBuffer.Append("<seniorprice>" & myrow("seniorprice").ToString & "</seniorprice>")
                                strBuffer.Append("<unitprice>" & IIf(myrow("unitprice") Is DBNull.Value, "0", myrow("unitprice").ToString) & "</unitprice>")
                                strBuffer.Append("<adultsalevalue>" & myrow("adultsalevalue").ToString & "</adultsalevalue>")
                                strBuffer.Append("<childsalevalue>" & myrow("childsalevalue").ToString & "</childsalevalue>")
                                strBuffer.Append("<seniorsalevalue>" & myrow("seniorsalevalue").ToString & "</seniorsalevalue>")
                                'strBuffer.Append("<unitsalevalue>" & myrow("unitsalevalue").ToString & "</unitsalevalue>")
                                strBuffer.Append("<unitsalevalue>" & IIf(myrow("unitsalevalue") Is DBNull.Value, "0", myrow("unitsalevalue").ToString) & "</unitsalevalue>")
                                'strBuffer.Append("<totalsalevalue>" & myrow("totalsalevalue").ToString & "</totalsalevalue>")

                                Dim dAdultSaleValue As Decimal = IIf(myrow("adultsalevalue") Is DBNull.Value, "0", myrow("adultsalevalue").ToString)
                                Dim dChildSaleValue As Decimal = IIf(myrow("childsalevalue") Is DBNull.Value, "0", myrow("childsalevalue").ToString)
                                Dim dSeniorSaleValue As Decimal = IIf(myrow("seniorsalevalue") Is DBNull.Value, "0", myrow("seniorsalevalue").ToString)
                                Dim dUnitSaleValue As Decimal = IIf(myrow("unitsalevalue") Is DBNull.Value, "0", myrow("unitsalevalue").ToString)
                                Dim dchildasadultvalue As Decimal = IIf(myrow("childasadultvalue") Is DBNull.Value, "0", myrow("childasadultvalue").ToString)

                                Dim dTodalSaleValue As Decimal = dAdultSaleValue + dChildSaleValue + dUnitSaleValue + dSeniorSaleValue + dchildasadultvalue

                                strBuffer.Append("<totalsalevalue>" & dTodalSaleValue * iNoOfDays.ToString & "</totalsalevalue>")



                                strBuffer.Append("<wladultsalevalue>" & myrow("wladultsalevalue").ToString & "</wladultsalevalue>")
                                strBuffer.Append("<wlchildsalevalue>" & myrow("wlchildsalevalue").ToString & "</wlchildsalevalue>")
                                strBuffer.Append("<wlseniorsalevalue>" & myrow("wlseniorsalevalue").ToString & "</wlseniorsalevalue>")
                                'strBuffer.Append("<wlunitsalevalue>" & myrow("wlunitsalevalue").ToString & "</wlunitsalevalue>")

                                strBuffer.Append("<wlunitsalevalue>" & IIf(myrow("wlunitsalevalue") Is DBNull.Value, "0", myrow("wlunitsalevalue").ToString) & "</wlunitsalevalue>")

                                Dim dWlAdultSaleValue As Decimal = IIf(myrow("wladultsalevalue") Is DBNull.Value, "0", myrow("wladultsalevalue").ToString)
                                Dim dWlChildSaleValue As Decimal = IIf(myrow("wlchildsalevalue") Is DBNull.Value, "0", myrow("wlchildsalevalue").ToString)
                                Dim dWlSeniorSaleValue As Decimal = IIf(myrow("wlseniorsalevalue") Is DBNull.Value, "0", myrow("wlseniorsalevalue").ToString)
                                Dim dWlUnitSaleValue As Decimal = IIf(myrow("wlunitsalevalue") Is DBNull.Value, "0", myrow("wlunitsalevalue").ToString)
                                Dim dWlchildasadultvalue As Decimal = IIf(myrow("wlchildasadultvalue") Is DBNull.Value, "0", myrow("wlchildasadultvalue").ToString)

                                Dim dWlTodalSaleValue As Decimal = dWlAdultSaleValue + dWlChildSaleValue + dWlUnitSaleValue + dWlSeniorSaleValue + dWlchildasadultvalue



                                'strBuffer.Append("<wltotalsalevalue>" & myrow("wltotalsalevalue").ToString & "</wltotalsalevalue>")
                                ' strBuffer.Append("<wltotalsalevalue>" & IIf(myrow("wltotalsalevalue") Is DBNull.Value, "0", (Val(myrow("wltotalsalevalue").ToString) * iNoOfDays).ToString) & "</wltotalsalevalue>")
                                strBuffer.Append("<wltotalsalevalue>" & (dWlTodalSaleValue * iNoOfDays).ToString & "</wltotalsalevalue>")

                                strBuffer.Append("<comp_cust>" & myrow("comp_cust").ToString & "</comp_cust>")
                                strBuffer.Append("<tourstartingfrom>" & txtTourStartingFromCode.Text & "</tourstartingfrom>")
                                strBuffer.Append("<overrideprice>" & IIf(chkTourOveridePrice.Checked = True, 1, 0) & "</overrideprice>")
                                strBuffer.Append("<adulteplistcode>" & myrow("adulteplistcode").ToString & "</adulteplistcode>")
                                strBuffer.Append("<childeplistcode>" & myrow("childeplistcode").ToString & "</childeplistcode>")
                                strBuffer.Append("<senioreplistcode>" & myrow("senioreplistcode").ToString & "</senioreplistcode>")
                                strBuffer.Append("<uniteplistcode>" & myrow("uniteplistcode").ToString & "</uniteplistcode>")
                                strBuffer.Append("<preferredsupplier>" & myrow("preferredsupplier").ToString & "</preferredsupplier>")
                                strBuffer.Append("<adultcprice>" & myrow("adultcprice").ToString & "</adultcprice>")
                                strBuffer.Append("<childcprice>" & myrow("childcprice").ToString & "</childcprice>")
                                strBuffer.Append("<seniorcprice>" & myrow("seniorcprice").ToString & "</seniorcprice>")
                                'strBuffer.Append("<unitcprice>" & myrow("unitcprice").ToString & "</unitcprice>")
                                strBuffer.Append("<unitcprice>" & IIf(myrow("unitcprice") Is DBNull.Value, "0", myrow("unitcprice").ToString) & "</unitcprice>")
                                strBuffer.Append("<adultcostvalue>" & myrow("adultcostvalue").ToString & "</adultcostvalue>")
                                strBuffer.Append("<childcostvalue>" & myrow("childcostvalue").ToString & "</childcostvalue>")
                                strBuffer.Append("<seniorcostvalue>" & myrow("seniorcostvalue").ToString & "</seniorcostvalue>")
                                'strBuffer.Append("<unitcostvalue>" & myrow("unitcostvalue").ToString & "</unitcostvalue>")
                                strBuffer.Append("<unitcostvalue>" & IIf(myrow("unitcostvalue") Is DBNull.Value, "0", myrow("unitcostvalue").ToString) & "</unitcostvalue>")
                                'strBuffer.Append("<totalcostvalue>" & myrow("totalcostvalue").ToString & "</totalcostvalue>")



                                Dim dAdultCostValue As Decimal = IIf(myrow("adultcostvalue") Is DBNull.Value, "0", myrow("adultcostvalue").ToString)
                                Dim dChildCostValue As Decimal = IIf(myrow("childcostvalue") Is DBNull.Value, "0", myrow("childcostvalue").ToString)
                                Dim dSeniorCostValue As Decimal = IIf(myrow("seniorcostvalue") Is DBNull.Value, "0", myrow("seniorcostvalue").ToString)
                                Dim dUnitCostValue As Decimal = IIf(myrow("unitcostvalue") Is DBNull.Value, "0", myrow("unitcostvalue").ToString)
                                Dim dchildasadultcostvalue As Decimal = IIf(myrow("childasadultcostvalue") Is DBNull.Value, "0", myrow("childasadultcostvalue").ToString)

                                Dim dTodalCostValue As Decimal = dAdultCostValue + dChildCostValue + dSeniorCostValue + dUnitCostValue + dchildasadultcostvalue

                                strBuffer.Append("<totalcostvalue>" & (dTodalCostValue * iNoOfDays).ToString & "</totalcostvalue>")

                                strBuffer.Append("<adultcplistcode>" & myrow("adultcplistcode").ToString & "</adultcplistcode>")
                                strBuffer.Append("<childcplistcode>" & myrow("childcplistcode").ToString & "</childcplistcode>")
                                strBuffer.Append("<seniorcplistcode>" & myrow("seniorcplistcode").ToString & "</seniorcplistcode>")
                                strBuffer.Append("<unitcplistcode>" & myrow("unitcplistcode").ToString & "</unitcplistcode>")
                                strBuffer.Append("<wlcurrcode>" & myrow("wlcurrcode").ToString & "</wlcurrcode>")
                                strBuffer.Append("<wlconvrate>" & myrow("wlconvrate").ToString & "</wlconvrate>")
                                strBuffer.Append("<wlmarkupperc>" & myrow("wlmarkupperc").ToString & "</wlmarkupperc>")
                                strBuffer.Append("<searchfromdate>" & Format(CType(objBLLTourSearch.FromDate, Date), "yyyy/MM/dd") & "</searchfromdate>")
                                strBuffer.Append("<searchtodate>" & Format(CType(objBLLTourSearch.ToDate, Date), "yyyy/MM/dd") & "</searchtodate>")
                                strBuffer.Append("<noofdays>" & iNoOfDays.ToString & "</noofdays>")

                                ''' Added Shahul 28/03/18
                                strBuffer.Append("<childasadult>" & myrow("childasadult").ToString & "</childasadult>")
                                strBuffer.Append("<childasadultprice>" & myrow("childasadultprice").ToString & "</childasadultprice>")
                                strBuffer.Append("<childasadultvalue>" & myrow("childasadultvalue").ToString & "</childasadultvalue>")
                                strBuffer.Append("<childasadultcprice>" & myrow("childasadultcprice").ToString & "</childasadultcprice>")
                                strBuffer.Append("<childasadultcostvalue>" & myrow("childasadultcostvalue").ToString & "</childasadultcostvalue>")
                                strBuffer.Append("<wlchildasadultvalue>" & myrow("wlchildasadultvalue").ToString & "</wlchildasadultvalue>")

                                ' Added by abin on 20180602 *****************************
                                strBuffer.Append("<PriceVATPerc>" & myrow("PriceVATPerc").ToString & "</PriceVATPerc>")
                                strBuffer.Append("<PriceWithTax>" & myrow("PriceWithTax").ToString & "</PriceWithTax>")
                                strBuffer.Append("<CostVATPerc>" & myrow("CostVATPerc").ToString & "</CostVATPerc>")
                                strBuffer.Append("<CostWithTax>" & myrow("CostWithTax").ToString & "</CostWithTax>")
                                Dim dtotalSalevalue As Decimal
                                Dim dtotalTv As Decimal
                                Dim dtotalVAT As Decimal
                                dtotalSalevalue = dTodalSaleValue ' Convert.ToDecimal(IIf(myrow("totalsalevalue") Is DBNull.Value, "0", (Val(myrow("totalsalevalue").ToString) * iNoOfDays).ToString))
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
                                dtotalCostvalue = dTodalCostValue 'Convert.ToDecimal(IIf(myrow("totalcostvalue") Is DBNull.Value, "0", (Val(myrow("totalcostvalue").ToString) * iNoOfDays).ToString))
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
                                strBuffer.Append("<childasfree>" & myrow("childasfree").ToString & "</childasfree>") 'modified by abin on 20181208
                                strBuffer.Append("<childasfreeages>" & myrow("childasfreeages").ToString & "</childasfreeages>") 'modified by abin on 20181208
                                '***************end
                                strBuffer.Append("</Table>")

                                If myrow("multicost").ToString = "YES" Then
                                    Dim dvMultiCostBreakup As DataView
                                    dvMultiCostBreakup = New DataView(dsTourSearchResults.Tables(4))
                                    dvMultiCostBreakup.RowFilter = "eplistcode= '" & myrow("uniteplistcode") & "' "
                                    Dim dtMultiCostBreakup As DataTable = dvMultiCostBreakup.ToTable
                                    If dtMultiCostBreakup.Rows.Count > 0 Then
                                        For i As Integer = 0 To dtMultiCostBreakup.Rows.Count - 1
                                            strBufferMultiCostBreakup.Append("<Table>")
                                            strBufferMultiCostBreakup.Append("<elineno>" & CType(strElineno, String) & "</elineno>")
                                            strBufferMultiCostBreakup.Append("<mlineno>" & CType(i + 1, String) & "</mlineno>")
                                            strBufferMultiCostBreakup.Append("<eplistcode>" & dtMultiCostBreakup.Rows(i)("eplistcode").ToString & "</eplistcode>")
                                            strBufferMultiCostBreakup.Append("<partycode>" & dtMultiCostBreakup.Rows(i)("partycode").ToString & "</partycode>")
                                            strBufferMultiCostBreakup.Append("<servicedescription>" & dtMultiCostBreakup.Rows(i)("servicedescription").ToString.Replace("&", "and") & "</servicedescription>")
                                            strBufferMultiCostBreakup.Append("<noofadults>" & dtMultiCostBreakup.Rows(i)("noofadults").ToString & "</noofadults>")
                                            strBufferMultiCostBreakup.Append("<peradult>" & dtMultiCostBreakup.Rows(i)("peradult").ToString & "</peradult>")
                                            strBufferMultiCostBreakup.Append("<adultcost>" & dtMultiCostBreakup.Rows(i)("adultcost").ToString & "</adultcost>")
                                            strBufferMultiCostBreakup.Append("<noofchildren>" & dtMultiCostBreakup.Rows(i)("noofchildren").ToString & "</noofchildren>")
                                            strBufferMultiCostBreakup.Append("<perchild>" & dtMultiCostBreakup.Rows(i)("perchild").ToString & "</perchild>")
                                            strBufferMultiCostBreakup.Append("<childcost>" & dtMultiCostBreakup.Rows(i)("childcost").ToString & "</childcost>")
                                            strBufferMultiCostBreakup.Append("<noofseniors>" & dtMultiCostBreakup.Rows(i)("noofseniors").ToString & "</noofseniors>")
                                            strBufferMultiCostBreakup.Append("<persenior>" & dtMultiCostBreakup.Rows(i)("persenior").ToString & "</persenior>")
                                            ' strBufferMultiCostBreakup.Append("<seniorcost>" & IIf(dtMultiCostBreakup("seniorcost").ToString = "", "0", dtMultiCostBreakup("seniorcost").ToString) & "</seniorcost>")
                                            strBufferMultiCostBreakup.Append("<seniorcost>" & dtMultiCostBreakup.Rows(i)("seniorcost").ToString & "</seniorcost>")
                                            strBufferMultiCostBreakup.Append("<unitcost>" & dtMultiCostBreakup.Rows(i)("unitcost").ToString & "</unitcost>")
                                            'strBufferMultiCostBreakup.Append("<unitcost>" & IIf(dtMultiCostBreakup("unitcost") Is DBNull.Value, 0, dtMultiCostBreakup("unitcost").ToString) & "</unitcost>")
                                            strBufferMultiCostBreakup.Append("<totalcost>" & dtMultiCostBreakup.Rows(i)("totalcost").ToString & "</totalcost>")
                                            ' strBufferMultiCostBreakup.Append("<totalcost>" & IIf(dtMultiCostBreakup("totalcost") Is DBNull.Value, 0, dtMultiCostBreakup("totalcost").ToString) & "</totalcost>")
                                            strBufferMultiCostBreakup.Append("<markuptype>" & dtMultiCostBreakup.Rows(i)("markuptype").ToString & "</markuptype>")
                                            strBufferMultiCostBreakup.Append("<Markupoperator>" & dtMultiCostBreakup.Rows(i)("Markupoperator").ToString & "</Markupoperator>")
                                            strBufferMultiCostBreakup.Append("<markupvalue_adult>" & dtMultiCostBreakup.Rows(i)("markupvalue_adult").ToString & "</markupvalue_adult>")
                                            strBufferMultiCostBreakup.Append("<markupvalue_child>" & dtMultiCostBreakup.Rows(i)("markupvalue_child").ToString & "</markupvalue_child>")
                                            strBufferMultiCostBreakup.Append("<markupvalue_senior>" & dtMultiCostBreakup.Rows(i)("markupvalue_senior").ToString & "</markupvalue_senior>")

                                            ''' Added Shahul 28/03/18
                                            strBufferMultiCostBreakup.Append("<noofchildasadult>" & dtMultiCostBreakup.Rows(i)("noofchildasadult").ToString & "</noofchildasadult>")
                                            strBufferMultiCostBreakup.Append("<perchildasadult>" & dtMultiCostBreakup.Rows(i)("perchildasadult").ToString & "</perchildasadult>")
                                            strBufferMultiCostBreakup.Append("<childasadult>" & dtMultiCostBreakup.Rows(i)("childasadult").ToString & "</childasadult>")


                                            strBufferMultiCostBreakup.Append("</Table>")
                                        Next

                                    End If
                                End If


                                If myrow("combo").ToString = "YES" Or myrow("multipledatesyesno").ToString = "YES" Then


                                    Dim dtselectedCombotour As New DataTable
                                    dtselectedCombotour = Session("selectedCombotourdatatable")
                                    Dim strType As String = ""
                                    If myrow("combo").ToString = "YES" Then
                                        strType = "COMBO"
                                    End If
                                    If myrow("multipledatesyesno").ToString = "YES" Then
                                        strType = "MULTI_DATE"
                                    End If

                                    Dim dvComboBreakup As DataView
                                    dvComboBreakup = New DataView(dtselectedCombotour)
                                    dvComboBreakup.RowFilter = "exctypcode= '" & myrow("exctypcode").ToString & "'  AND  vehiclecode='" & myrow("vehiclecode").ToString & "'  AND type='" & strType & "' "
                                    Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
                                    If dtComboBreakup.Rows.Count > 0 Then
                                        For i As Integer = 0 To dtComboBreakup.Rows.Count - 1
                                            strBufferCombo.Append("<Table>")
                                            strBufferCombo.Append("<elineno>" & CType(strElineno, String) & "</elineno>")
                                            strBufferCombo.Append("<clineno>" & CType(i + 1, String) & "</clineno>")
                                            strBufferCombo.Append("<exctypcode>" & dtComboBreakup.Rows(i)("exctypcode").ToString & "</exctypcode>")
                                            strBufferCombo.Append("<vehiclecode>" & dtComboBreakup.Rows(i)("vehiclecode").ToString & "</vehiclecode>")
                                            strBufferCombo.Append("<exctypcombocode>" & dtComboBreakup.Rows(i)("exctypcombocode").ToString & "</exctypcombocode>")
                                            strBufferCombo.Append("<excdate>" & Format(CType(dtComboBreakup.Rows(i)("excdate").ToString, Date), "yyyy/MM/dd") & "</excdate>")
                                            strBufferCombo.Append("<Type>" & dtComboBreakup.Rows(i)("Type").ToString & "</Type>")

                                            strBufferCombo.Append("</Table>")
                                        Next
                                    End If

                                End If

                                Dim dtTSlot As DataTable = Session("sSelectedTourTimeSlotDatatable")
                                If Not dtTSlot Is Nothing Then

                                    If dtTSlot.Rows.Count > 0 Then
                                        'Dim dtselectedCombotour As New DataTable
                                        'dtselectedCombotour = Session("selectedCombotourdatatable")
                                        'Dim strType As String = ""
                                        'If myrow("combo").ToString = "YES" Then
                                        '    strType = "COMBO"
                                        'End If
                                        'If myrow("multipledatesyesno").ToString = "YES" Then
                                        '    strType = "MULTI_DATE"
                                        'End If

                                        Dim dvInventoryBreakup As DataView
                                        dvInventoryBreakup = New DataView(dtTSlot)
                                        dvInventoryBreakup.RowFilter = "EXC_CODE= '" & myrow("exctypcode").ToString & "'  AND  VEHICLE_CODE='" & myrow("vehiclecode").ToString & "' "
                                        Dim dtInventoryBreakup As DataTable = dvInventoryBreakup.ToTable
                                        If dtInventoryBreakup.Rows.Count > 0 Then
                                            For i As Integer = 0 To dtInventoryBreakup.Rows.Count - 1
                                                strBufferInventory.Append("<Table>")
                                                strBufferInventory.Append("<elineno>" & CType(strElineno, String) & "</elineno>")
                                                strBufferInventory.Append("<clineno>" & CType(i + 1, String) & "</clineno>")
                                                strBufferInventory.Append("<exctypcode>" & dtInventoryBreakup.Rows(i)("EXC_CODE").ToString & "</exctypcode>")
                                                strBufferInventory.Append("<vehiclecode>" & dtInventoryBreakup.Rows(i)("VEHICLE_CODE").ToString & "</vehiclecode>")
                                                strBufferInventory.Append("<exctypcombocode>" & dtInventoryBreakup.Rows(i)("EXC_TYPE_COMBO_CODE").ToString & "</exctypcombocode>")
                                                strBufferInventory.Append("<excdate>" & Format(CType(dtInventoryBreakup.Rows(i)("EXC_DATE").ToString, Date), "yyyy/MM/dd") & "</excdate>")
                                                strBufferInventory.Append("<Type>" & dtInventoryBreakup.Rows(i)("TYPE").ToString & "</Type>")

                                                strBufferInventory.Append("<timeslotfrom>" & dtInventoryBreakup.Rows(i)("TIMESLOT_FROM").ToString & "</timeslotfrom>")
                                                strBufferInventory.Append("<timeslotto>" & dtInventoryBreakup.Rows(i)("TIMESLOT_TO").ToString & "</timeslotto>")
                                                strBufferInventory.Append("<timeslot>" & dtInventoryBreakup.Rows(i)("TIMESLOT").ToString & "</timeslot>")
                                                strBufferInventory.Append("<adult>" & dtInventoryBreakup.Rows(i)("ADULT").ToString & "</adult>")
                                                strBufferInventory.Append("<child>" & dtInventoryBreakup.Rows(i)("CHILD").ToString & "</child>")
                                                strBufferInventory.Append("<senior>" & dtInventoryBreakup.Rows(i)("SENIOR").ToString & "</senior>")

                                                strBufferInventory.Append("<tempcancelno>" & dtInventoryBreakup.Rows(i)("TEMP_CANCEL_NO").ToString & "</tempcancelno>")
                                                strBufferInventory.Append("<tempcanceldate>" & dtInventoryBreakup.Rows(i)("TEMP_CANCEL_DATE").ToString & "</tempcanceldate>")
                                                strBufferInventory.Append("<cancelno>" & dtInventoryBreakup.Rows(i)("CANCEL_NO").ToString & "</cancelno>")
                                                strBufferInventory.Append("<canceldate>" & dtInventoryBreakup.Rows(i)("CANCEL_DATE").ToString & "</canceldate>")

                                                strBufferInventory.Append("<tempconfirmno>" & dtInventoryBreakup.Rows(i)("TEMP_CONF_NO").ToString & "</tempconfirmno>")
                                                strBufferInventory.Append("<tempconfirmdate>" & dtInventoryBreakup.Rows(i)("TEMP_CONF_DATE").ToString & "</tempconfirmdate>")
                                                strBufferInventory.Append("<confirmno>" & dtInventoryBreakup.Rows(i)("CONF_NO").ToString & "</confirmno>")
                                                strBufferInventory.Append("<confirmdate>" & dtInventoryBreakup.Rows(i)("CONF_DATE").ToString & "</confirmdate>")


                                                strBufferInventory.Append("<tempid>" & dtInventoryBreakup.Rows(i)("TEMP_ID").ToString & "</tempid>")
                                                strBufferInventory.Append("<inventoryid>" & dtInventoryBreakup.Rows(i)("INVENTORY_ID").ToString & "</inventoryid>")
                                                strBufferInventory.Append("<pendingtobook>1</pendingtobook>")
                                                strBufferInventory.Append("<pendingtocancel>0</pendingtocancel>")


                                                strBufferInventory.Append("</Table>")
                                            Next
                                        End If
                                    End If
                                  

                                End If

                                ' strBufferInventory.Append("<DocumentElement>")

                                rownum = rownum + 1
                                If myrow("currentselection").ToString <> "1" Then
                                    strselectedrow = CType(strselectedrow, Integer) + 1
                                End If
                            Next
                            strBuffer.Append("</DocumentElement>")
                            strBufferCombo.Append("</DocumentElement>")
                            strBufferMultiCostBreakup.Append("</DocumentElement>")
                            strBufferInventory.Append("</DocumentElement>")
                            objBLLTourSearch.EBToursXml = strBuffer.ToString()




                            objBLLTourSearch.EBuserlogged = Session("GlobalUserName")
                            objBLLTourSearch.RowLineNo = strRowLineNo
                            objBLLTourSearch.BufferMultiCostBreakup = strBufferMultiCostBreakup.ToString
                            objBLLTourSearch.BufferComboBreakup = strBufferCombo.ToString
                            objBLLTourSearch.BufferInventoryBreakup = strBufferInventory.ToString
                            If Not Session("sobjResParam") Is Nothing Then
                                objResParam = Session("sobjResParam")
                                objBLLTourSearch.SubUserCode = objResParam.SubUserCode
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
                objBLLTourSearch.SectorgroupCode = strSectorgroupCode
                If Not ViewState("Elineno") Is Nothing Then
                    objBLLTourSearch.OpMode = "E"
                Else
                    objBLLTourSearch.OpMode = "N"
                End If




                '    End If
                If objBLLTourSearch.SaveExcursionTypeBookingInTemp() Then
                    Session("sRequestId") = objBLLTourSearch.EbRequestID
                    Session("sobjBLLTourSearchActive") = objBLLTourSearch
                    Response.Redirect("MoreServices.aspx")
                End If
                '  Exit Sub
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnbooknow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Protected Sub btnSelectedTourCheckbox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedTourChecknox.Click
        Try

            Dim iValue As Integer = hddlRowNumber.Value

            Dim dlItem As DataListItem = dlTourSearchResults.Items(iValue)
            Dim chkSelectTour As CheckBox = CType(dlItem.FindControl("chkSelectTour"), CheckBox)
            Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
            Dim txttourchangedate As TextBox = CType(dlItem.FindControl("txttourchangedate"), TextBox)
            Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
            Dim hdcombo As HiddenField = CType(dlItem.FindControl("hdcombo"), HiddenField)
            Dim hdMultipleDates As HiddenField = CType(dlItem.FindControl("hdMultipleDates"), HiddenField)






            Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
            Dim strDateFlag = "0"
            If chkSelectTour.Checked Then
                If hdcombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
                    Dim dtselectedCombotour As New DataTable
                    dtselectedCombotour = Session("selectedCombotourdatatable")

                    Dim foundRow As DataRow
                    foundRow = dtselectedCombotour.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' ").FirstOrDefault
                    If foundRow Is Nothing Then
                        strDateFlag = "1"
                    End If
                Else
                    If txttourchangedate.Text = "" Then
                        strDateFlag = "1"
                    End If
                End If


                If strDateFlag = "1" Then
                    For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                        If dtselectedtour.Rows(i)("exctypcode") = hdExcCode.Value.ToString And dtselectedtour.Rows(i)("vehiclecode") = hdVehicleCode.Value.ToString Then
                            dtselectedtour.Rows.RemoveAt(i)
                        End If
                    Next
                    chkSelectTour.Checked = False
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select date.")
                    Exit Sub
                Else
                    Dim dt As DataTable
                    Dim dtdates As DataTable
                    Dim strsql As String = ""
                    Dim strmsg As String = ""
                    Dim objBLLHotelSearch As New BLLHotelSearch
                    Dim strRequestId As String = ""
                    If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
                        objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
                        If hdcombo.Value = "NO" And hdMultipleDates.Value = "NO" Then
                            strRequestId = objBLLHotelSearch.OBrequestid

                            strsql = "select d.* from  othtypmast o(nolock),view_booking_hotel_prearr d(nolock),partymast p(nolock),sectormaster s(nolock) where   o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode " _
                                     & " and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and d.requestid='" & strRequestId & "' and  o.othtypcode='" & txtTourStartingFromCode.Text & "'  order by d.checkin"


                            Dim excdate As String = ""
                            Dim strDates As String() = txttourchangedate.Text.Split("/")
                            excdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)


                            dt = objBLLHotelSearch.CheckExistsSectorCheckInAndCheckOut(strRequestId, txtTourStartingFromCode.Text, Format(CType(excdate, Date), "yyyy/MM/dd"))
                            If dt.Rows.Count = 0 Then
                                For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                                    If dtselectedtour.Rows(i)("exctypcode") = hdExcCode.Value.ToString And dtselectedtour.Rows(i)("vehiclecode") = hdVehicleCode.Value.ToString Then
                                        dtselectedtour.Rows.RemoveAt(i)
                                    End If
                                Next
                                dtdates = objBLLHotelSearch.GetResultAsDataTable(strsql)
                                If dtdates.Rows.Count > 0 Then
                                    strmsg = "Please Note Selected Dates not belongs to Hotel Sector. </br> Please Select the Date  Following Periods </br>"
                                    For k = 0 To dtdates.Rows.Count - 1 'To 0 Step -1
                                        strmsg += " From  - " + dtdates.Rows(k)("checkin") + " - To  " + dtdates.Rows(k)("checkout") + "</br>"

                                    Next
                                End If

                                chkSelectTour.Checked = False
                                MessageBox.ShowMessage(Page, MessageType.Warning, strmsg)
                                Exit Sub
                            End If
                        End If

                   

                    End If


                End If


                If Not Request.QueryString("ELineNo") Is Nothing Then
                    For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                        If dtselectedtour.Rows(i)("exctypcode") = hdExcCode.Value.ToString And dtselectedtour.Rows(i)("vehiclecode") = hdVehicleCode.Value.ToString Then
                            dtselectedtour.Rows.RemoveAt(i)
                        End If
                    Next
                End If
                Dim dtExcWeekDays As DataTable
                dtExcWeekDays = objBLLTourSearch.ValidateExcWeekDays(hdExcCode.Value.ToString, txttourchangedate.Text)
                If dtExcWeekDays.Rows.Count > 0 Then
                    Dim strExcName As String = ""
                    If dtExcWeekDays.Rows.Count > 0 Then
                        strExcName = dtExcWeekDays.Rows(0)("excname").ToString
                    End If

                    ' Dim strExcName As String = objBLLTourSearch1.ValidateExcWeekDays("ET/000009", "14/12/2017")
                    If strExcName <> "" Then
                        Dim strMessage As String = strExcName & " is not operational on " & dtExcWeekDays.Rows(0)("TourDayName").ToString & "(" & txttourchangedate.Text & ")."
                        strMessage = strMessage & " </br></br> Tour operational days:</br> " & dtExcWeekDays.Rows(0)("AvailableDays").ToString & "."
                        MessageBox.ShowMessage(Page, MessageType.Alert, strMessage)
                        chkSelectTour.Checked = False
                        Exit Sub
                    End If
                End If

                If hdcombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
                    Dim dtselectedCombotour As New DataTable
                    dtselectedCombotour = Session("selectedCombotourdatatable")

                    Dim foundRow As DataRow
                    Dim strType As String = ""
                    If hdcombo.Value = "YES" Then
                        strType = "COMBO"
                    End If
                    If hdMultipleDates.Value = "YES" Then
                        strType = "MULTI_DATE"
                    End If

                    foundRow = dtselectedCombotour.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND type='" & strType & "' ").FirstOrDefault
                    If Not foundRow Is Nothing Then
                        txttourchangedate.Text = foundRow("excdate")
                    End If
                End If

                dtselectedtour.Rows.Add(hdExcCode.Value.ToString, txttourchangedate.Text, hdVehicleCode.Value)



            Else
                Dim hdElineNo As HiddenField = CType(dlItem.FindControl("hdElineNo"), HiddenField)
                If hdElineNo.Value <> "" And hdElineNo.Value <> "0" And Session("sRequestId") IsNot Nothing Then

                    Dim sRequestId As String = Session("sRequestId")
                    Dim dtAssignServiceLock As DataTable = objclsUtilities.GetDataFromDataTable("execute sp_CheckService_AssignStatus_individual '" + sRequestId + "','TOUR','" + hdElineNo.Value + "' ")

                    If dtAssignServiceLock.Rows.Count > 0 Then
                        Dim strLockWarning As String = ""
                        For i As Integer = 0 To dtAssignServiceLock.Rows.Count - 1
                            strLockWarning = strLockWarning & "</br>" & dtAssignServiceLock.Rows(i)("Warning").ToString
                        Next
                        chkSelectTour.Checked = True
                        MessageBox.ShowMessage(Page, MessageType.Warning, strLockWarning)
                        Exit Sub

                    End If

                End If

                For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
                    If dtselectedtour.Rows(i)(0) = hdExcCode.Value.ToString And dtselectedtour.Rows(i)("vehiclecode") = hdVehicleCode.Value.ToString Then
                        dtselectedtour.Rows.RemoveAt(i)
                    End If
                Next
            End If

            If Not dtselectedtour Is Nothing Then
                If dtselectedtour.Rows.Count > 0 Then
                    Dim dsTourSearchResults As New DataSet
                    dsTourSearchResults = CType(Session("sDSTourSearchResults"), DataSet)
                    For i As Integer = 0 To dtselectedtour.Rows.Count - 1

                        If dsTourSearchResults.Tables(0).Rows.Count > 0 Then
                            Dim dr As DataRow = dsTourSearchResults.Tables(0).Select("exctypcode = '" + dtselectedtour.Rows(i)("exctypcode").ToString + "' and vehiclecode='" & dtselectedtour.Rows(i)("vehiclecode").ToString + "'").First
                            dr("tourselected") = "1"

                        End If
                    Next
                    ''' Added shahul 19/02/18 edit mode if we untick and select another after paging existing one coming
                    If chkSelectTour.Checked = False Then
                        Dim dr As DataRow = dsTourSearchResults.Tables(0).Select("exctypcode = '" + hdExcCode.Value.ToString + "' and vehiclecode='" & hdVehicleCode.Value.ToString + "'").First
                        dr("tourselected") = "0"
                        dr("currentselection") = "0"
                    End If
                    ''''

                    Session("sDSTourSearchResults") = dsTourSearchResults
                End If

            End If

            Session("selectedtourdatatable") = dtselectedtour

            Dim dtInventory As DataTable
            dtInventory = objclsUtilities.GetDataFromDataTable("exec sp_checkExcInventory '" + hdExcCode.Value + "'")
       
            If txttourchangedate.Text <> "" And hdcombo.Value <> "YES" And hdMultipleDates.Value <> "YES" And dtInventory.Rows.Count > 0 And hdClickFrom.Value = "d" Then

                Dim strInventoryId As String = dtInventory.Rows(0)("InventoryId").ToString


                Dim dTourDate As DateTime = txttourchangedate.Text
                lblTimeSlotSelectionDate.Text = "Day: " + dTourDate.ToString("dddd") + ", " + dTourDate.ToString("dd MMM yyyy")  ' txttourchangedate.Text

                Dim objBLLTourSearchTimeSlot As BLLTourSearch = New BLLTourSearch
                objBLLTourSearchTimeSlot = Session("sobjBLLTourSearch")

                Dim dtTimeSlot As DataTable
                Dim objAPIInventoryController As APIInventoryController = New APIInventoryController()


                dtTimeSlot = objAPIInventoryController.GetTimeSlot(objBLLTourSearchTimeSlot.AgentCode, objBLLTourSearchTimeSlot.SourceCountryCode, hdExcCode.Value, txttourchangedate.Text, txttourchangedate.Text, "", "0", strInventoryId)
                Dim iAFlag As Integer = 0
                If dtTimeSlot.Rows.Count = 0 Then
                    Dim SelectTourTimeSlotDT As DataTable
                    If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then

                        SelectTourTimeSlotDT = Session("sSelectedTourTimeSlotDatatable")
                        If SelectTourTimeSlotDT.Rows.Count > 0 Then
                            hdExcCodeTimeSlot.Value = hdExcCode.Value
                            hdVehicleCodeTimeSlot.Value = hdVehicleCode.Value.ToString
                            hdTimeSlotDate.Value = txttourchangedate.Text
                            hdTimeSlotInventoryId.Value = strInventoryId
                            Dim dv As DataView = New DataView(SelectTourTimeSlotDT)
                            dv.RowFilter = "EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_DATE='" & txttourchangedate.Text & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL'"
                            If dv.Count > 0 Then
                                For t As Integer = 0 To dv.Count - 1
                                    iAFlag = 1
                                    Dim row As DataRow = dtTimeSlot.NewRow()
                                    row("EXC_CODE") = dv.Item(t)("EXC_CODE")
                                    row("TIMESLOT_FROM") = dv.Item(t)("TIMESLOT_FROM")
                                    row("TIMESLOT_TO") = dv.Item(t)("TIMESLOT_TO")
                                    row("TIMESLOT") = dv.Item(t)("TIMESLOT")

                                    'Dim excdate As String
                                    'excdate = dv.Item(t)("EXC_DATE").ToString
                                    'If excdate <> "" Then
                                    '    Dim strDates As String() = excdate.Split("/")
                                    '    excdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                    'End If

                                    row("DATE") = dv.Item(t)("EXC_DATE").ToString 'excdate
                                    row("AVAILABILITY") = Val(dv.Item(t)("ADULT").ToString) + Val(dv.Item(t)("CHILD").ToString) + Val(dv.Item(t)("SENIOR").ToString)
                                    dtTimeSlot.Rows.Add(row)


                                Next
                              
                            End If
                        End If
                    End If
                 




                End If

                If dtTimeSlot.Rows.Count > 0 Then
                    gvTimeSlot.DataSource = dtTimeSlot
                    gvTimeSlot.DataBind()

                    hdExcCodeTimeSlot.Value = hdExcCode.Value
                    hdVehicleCodeTimeSlot.Value = hdVehicleCode.Value.ToString
                    hdTimeSlotDate.Value = txttourchangedate.Text
                    hdTimeSlotInventoryId.Value = strInventoryId

                    Dim SelectTourTimeSlotDT As DataTable
                    If Not Session("sSelectedTourTimeSlotDatatable") Is Nothing Then

                        SelectTourTimeSlotDT = Session("sSelectedTourTimeSlotDatatable")
                        If SelectTourTimeSlotDT.Rows.Count > 0 Then

                            Dim dv As DataView = New DataView(SelectTourTimeSlotDT)
                            dv.RowFilter = "EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_DATE='" & txttourchangedate.Text & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL'"
                            If dv.Count > 0 Then

                                If gvTimeSlot.Rows.Count > 0 Then
                                    For i As Integer = 0 To dv.Count - 1
                                        For Each row As GridViewRow In gvTimeSlot.Rows
                                            Dim lblTimeSlot As Label = CType(row.FindControl("lblTimeSlot"), Label)
                                            Dim lblTimeSlotFrom As Label = CType(row.FindControl("lblTimeSlotFrom"), Label)
                                            Dim lblTimeSlotTo As Label = CType(row.FindControl("lblTimeSlotTo"), Label)
                                            Dim lblAvailabilityCount As Label = CType(row.FindControl("lblAvailabilityCount"), Label)
                                            Dim ddlTimeSlotAdult As DropDownList = CType(row.FindControl("ddlTimeSlotAdult"), DropDownList)
                                            Dim ddlTimeSlotChild As DropDownList = CType(row.FindControl("ddlTimeSlotChild"), DropDownList)
                                            Dim ddlTimeSlotSenior As DropDownList = CType(row.FindControl("ddlTimeSlotSenior"), DropDownList)

                                            If lblTimeSlot.Text = dv.Item(i)("TIMESLOT") Then
                                                If dv.Item(i)("ADULT").ToString > 0 Then
                                                    ddlTimeSlotAdult.SelectedValue = dv.Item(i)("ADULT").ToString
                                                    If iAFlag = 0 Then
                                                        lblAvailabilityCount.Text = Val(lblAvailabilityCount.Text) + Val(dv.Item(i)("ADULT").ToString)
                                                    End If

                                                End If
                                                If dv.Item(i)("CHILD").ToString > 0 Then
                                                    ddlTimeSlotChild.SelectedValue = dv.Item(i)("CHILD").ToString
                                                    If iAFlag = 0 Then
                                                        lblAvailabilityCount.Text = Val(lblAvailabilityCount.Text) + Val(dv.Item(i)("CHILD").ToString)
                                                    End If

                                                End If
                                                If dv.Item(i)("SENIOR").ToString > 0 Then
                                                    ddlTimeSlotSenior.SelectedValue = dv.Item(i)("SENIOR").ToString
                                                    If iAFlag = 0 Then
                                                        lblAvailabilityCount.Text = Val(lblAvailabilityCount.Text) + Val(dv.Item(i)("SENIOR").ToString)
                                                    End If

                                                End If
                                                lblTimeSlotWarning1.Text = "success"
                                            End If

                                        Next
                                    Next

                                Else
                                End If

                            End If


                        End If

                    End If
                    mpTimeSlot.Show()


                Else
                    Dim dsTourSearchResults As New DataSet
                    dsTourSearchResults = CType(Session("sDSTourSearchResults"), DataSet)
                    Dim dr As DataRow = dsTourSearchResults.Tables(0).Select("exctypcode = '" + hdExcCode.Value.ToString + "' and vehiclecode='" & hdVehicleCode.Value.ToString + "'").First
                    dr("tourselected") = "0"
                    dr("currentselection") = "0"
                    Session("sDSTourSearchResults") = dsTourSearchResults
                    gvTimeSlot.DataBind()
                    chkSelectTour.Checked = False
                    MessageBox.ShowMessage(Page, MessageType.Warning, "No time slot available.")
                End If

            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnSelectedTourChecknox_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
        '            'FillSpecifiedChildAges(dtDetails.Rows(0)("childages").ToString)n
        '        End If
        '    Else
        '        FillSpecifiedAdultChild("16", "8")
        '    End If
        'Else
        '    FillSpecifiedAdultChild("16", "8")
        'End If
        ' Above part commented asper Arun request on 04/06/2017. No need to restrict adult and child based on other booking.
        FillSpecifiedAdultChild("20", "9")

    End Sub

    Private Sub FillSpecifiedChildAges(ByVal childages As String)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild1, childages)
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

    Private Shared Function GetNewOrExistingRequestId() As String
        Dim strRequestId As String = ""
        Dim objBLLHotelSearch2 As New BLLHotelSearch
        strRequestId = GetExistingRequestId()
        If strRequestId = "" Then
            strRequestId = objBLLHotelSearch2.getrequestid()
        End If
        Return strRequestId
    End Function
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
                dvMybooking.Visible = False
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
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub

    Protected Sub lbwlPrice_Click(sender As Object, e As System.EventArgs)
        Try
            Dim lbwlPrice As LinkButton = CType(sender, LinkButton)
            Session("slbTourTotalSaleValue") = lbwlPrice
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
            hdVehicleCodePopup.Value = hdVehicleCode.Value

            If txtTourChangeDate.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Select date for price.")
            Else
                lblTotlaPriceHeading.Text = lblExcName.Text
                objBLLTourSearch = New BLLTourSearch
                If Session("sobjBLLTourSearch") Is Nothing Then
                    Response.Redirect("Home.aspx?Tab=1")
                End If
                objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
                objBLLTourSearch.DateChange = "1"
                objBLLTourSearch.ExcTypeCode = hdExcCode.Value
                objBLLTourSearch.VehicleCode = hdVehicleCode.Value
                objBLLTourSearch.SelectedDate = txtTourChangeDate.Text

                Dim strDate As String = txtTourChangeDate.Text
                If strDate <> "" Then
                    Dim strDates As String() = strDate.Split("/")
                    strDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If

                hdSelectedDatePopup.Value = strDate

                Dim sDt As New DataTable

                Dim dsTourPriceResults As New DataSet
                If Not Session("sdtTourPriceBreakup") Is Nothing Then
                    sDt = Session("sdtTourPriceBreakup")
                    If sDt.Rows.Count > 0 Then
                        Dim dvSDt As DataView = New DataView(sDt)
                        dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' AND vehiclecode='" & hdVehicleCode.Value & "' "
                        'dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
                        If dvSDt.Count = 0 Then
                            Dim ds As New DataSet
                            ds = objBLLTourSearch.GetSearchDetails()
                            Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'  AND vehiclecode='" & hdVehicleCode.Value & "' ").First
                            'Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'").First
                            Dim drNew As DataRow = sDt.NewRow()
                            drNew.ItemArray = dr.ItemArray
                            sDt.Rows.Add(drNew)
                            Session("sdtTourPriceBreakup") = sDt
                        Else
                            Session("sdtTourPriceBreakup") = sDt
                        End If
                    Else
                        dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
                        sDt = dsTourPriceResults.Tables(0)
                        Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
                    End If


                Else
                    dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
                    sDt = dsTourPriceResults.Tables(0)
                    Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
                End If





                If sDt.Rows.Count > 0 Then

                    Dim dvSDt As DataView = New DataView(sDt)
                    dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "'  AND vehiclecode='" & hdVehicleCode.Value & "'  "
                    ' dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
                    If hdRateBasis.Value = "ACS" Then
                        dvACS.Visible = True
                        dvUnits.Visible = False
                        lblNoOfAdult.Text = dvSDt.Item(0)("adults").ToString
                        lblNoOfchild.Text = dvSDt.Item(0)("child").ToString
                        lblNoOfSeniors.Text = dvSDt.Item(0)("senior").ToString
                        lblNoOfchildasadult.Text = dvSDt.Item(0)("childasadult").ToString

                        lblNoOfUnits.Text = ""

                        txtAdultPrice.Text = dvSDt.Item(0)("adultprice").ToString
                        txtChildprice.Text = dvSDt.Item(0)("childprice").ToString
                        txtSeniorsPrice.Text = dvSDt.Item(0)("seniorprice").ToString
                        txtChildasadultprice.Text = dvSDt.Item(0)("childasadultprice").ToString

                        txtUnitPrice.Text = ""


                        lblAdultSaleValue.Text = dvSDt.Item(0)("adultsalevalue").ToString
                        lblchildSaleValue.Text = dvSDt.Item(0)("childsalevalue").ToString
                        lblSeniorSaleValue.Text = dvSDt.Item(0)("seniorsalevalue").ToString
                        lblchildSaleValue.Text = dvSDt.Item(0)("childasadultvalue").ToString
                        lblUnitSaleValue.Text = ""

                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc").ToString)) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate").ToString)
                        If (dvSDt.Item(0)("adults").ToString = "0") Then
                            txtwlAdultPrice.Text = "0"
                        Else
                            txtwlAdultPrice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wladultsalevalue").ToString = "", "0", dvSDt.Item(0)("wladultsalevalue").ToString)) / Val(IIf(dvSDt.Item(0)("adults").ToString = "", "0", dvSDt.Item(0)("adults").ToString)), 2)  ' Math.Round(Val(IIf(dvSDt.Item(0)("adultprice").ToString = "", "0", dvSDt.Item(0)("adultprice").ToString) * dWlMarkup), 2)
                        End If

                        txtwlAdultPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")

                        If (dvSDt.Item(0)("childasadult").ToString = "0") Then
                            txtwlChildasadultprice.Text = "0"
                        Else
                            txtwlChildasadultprice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wlchildasadultvalue").ToString = "", "0", dvSDt.Item(0)("wlchildasadultvalue").ToString)) / Val(IIf(dvSDt.Item(0)("childasadult").ToString = "", "0", dvSDt.Item(0)("childasadult").ToString)))
                        End If

                        If (dvSDt.Item(0)("child").ToString = "0") Then
                            txtwlChildprice.Text = "0"
                        Else
                            txtwlChildprice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wlchildsalevalue").ToString = "", "0", dvSDt.Item(0)("wlchildsalevalue").ToString)) / Val(IIf(dvSDt.Item(0)("child").ToString = "", "0", dvSDt.Item(0)("child").ToString)))
                        End If


                        If (dvSDt.Item(0)("senior").ToString = "0") Then
                            txtwlSeniorsPrice.Text = "0"
                        Else
                            txtwlSeniorsPrice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wlseniorsalevalue").ToString = "", "0", dvSDt.Item(0)("wlseniorsalevalue").ToString)) / Val(IIf(dvSDt.Item(0)("senior").ToString = "", "0", dvSDt.Item(0)("senior").ToString))) ' Math.Round(Val(IIf(dvSDt.Item(0)("seniorprice").ToString = "", "0", dvSDt.Item(0)("seniorprice").ToString) * dWlMarkup), 2)
                        End If

                        txtwlAdultPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")
                        txtwlChildprice.Attributes.Add("onkeydown", "fnReadOnly(event)")
                        txtwlSeniorsPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")

                        txtwlChildasadultprice.Attributes.Add("onkeydown", "fnReadOnly(event)")

                        txtwlUnitPrice.Text = ""


                        txtwlAdultSaleValue.Text = dvSDt.Item(0)("wladultsalevalue").ToString
                        txtwlChildSaleValue.Text = dvSDt.Item(0)("wlchildsalevalue").ToString
                        txtwlSeniorSaleValue.Text = dvSDt.Item(0)("wlseniorsalevalue").ToString

                        txtwlChildasadultSaleValue.Text = dvSDt.Item(0)("wlchildasadultvalue").ToString

                        txtwlUnitSaleValue.Text = ""

                        If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")

                            txtwlChildasadultprice.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "block")
                            txtChildprice.Style.Add("display", "block")
                            txtSeniorsPrice.Style.Add("display", "block")
                            txtUnitPrice.Style.Add("display", "none")

                            txtChildasadultprice.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "block")
                            lblchildSaleValue.Style.Add("display", "block")
                            lblSeniorSaleValue.Style.Add("display", "block")
                            lblUnitSaleValue.Style.Add("display", "none")

                            lblchildasadultSaleValue.Style.Add("display", "block")


                        ElseIf hdWhiteLabel.Value = "1" Then
                            txtwlAdultPrice.Style.Add("display", "block")
                            txtwlChildprice.Style.Add("display", "block")
                            txtwlSeniorsPrice.Style.Add("display", "block")

                            txtwlChildasadultprice.Style.Add("display", "block")


                            txtwlAdultSaleValue.Style.Add("display", "block")
                            txtwlChildSaleValue.Style.Add("display", "block")
                            txtwlSeniorSaleValue.Style.Add("display", "block")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtwlChildasadultSaleValue.Style.Add("display", "block")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "none")

                            txtChildasadultprice.Style.Add("display", "none")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "none")

                            lblchildasadultSaleValue.Style.Add("display", "none")
                        Else
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")

                            txtwlChildasadultprice.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtwlChildasadultSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "block")
                            txtChildprice.Style.Add("display", "block")
                            txtSeniorsPrice.Style.Add("display", "block")
                            txtUnitPrice.Style.Add("display", "none")

                            txtChildasadultprice.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "block")
                            lblchildSaleValue.Style.Add("display", "block")
                            lblSeniorSaleValue.Style.Add("display", "block")
                            lblUnitSaleValue.Style.Add("display", "none")

                            lblchildasadultSaleValue.Style.Add("display", "block")
                        End If

                    Else
                        dvACS.Visible = False
                        dvUnits.Visible = True

                        lblNoOfAdult.Text = ""
                        lblNoOfchild.Text = ""
                        lblNoOfSeniors.Text = ""
                        lblNoOfchildasadult.Text = ""
                        lblNoOfUnits.Text = dvSDt.Item(0)("units").ToString

                        txtAdultPrice.Text = ""
                        txtChildprice.Text = ""
                        txtSeniorsPrice.Text = ""
                        txtChildasadultprice.Text = ""
                        txtUnitPrice.Text = dvSDt.Item(0)("unitprice").ToString


                        txtwlAdultSaleValue.Text = ""
                        txtwlChildSaleValue.Text = ""
                        txtwlSeniorSaleValue.Text = ""
                        txtwlChildasadultprice.Text = ""
                        txtwlUnitSaleValue.Text = dvSDt.Item(0)("unitsalevalue").ToString


                        txtwlAdultPrice.Text = ""
                        txtwlChildprice.Text = ""
                        txtwlSeniorsPrice.Text = ""
                        txtwlChildasadultprice.Text = ""

                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc").ToString)) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate").ToString)
                        txtwlUnitPrice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("unitprice").ToString = "", "0", dvSDt.Item(0)("unitprice").ToString) * dWlMarkup), 2)


                        txtwlAdultSaleValue.Text = ""
                        txtwlChildSaleValue.Text = ""
                        txtwlSeniorSaleValue.Text = ""
                        txtwlChildasadultSaleValue.Text = ""

                        txtwlUnitSaleValue.Text = dvSDt.Item(0)("wlunitsalevalue").ToString
                        txtwlUnitPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")

                        If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "none")

                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "block")
                        ElseIf hdWhiteLabel.Value = "1" Then
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "block")

                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "block")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "none")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "none")


                        Else
                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlSeniorsPrice.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "none")

                            txtwlChildasadultprice.Style.Add("display", "none")
                            txtwlChildasadultSaleValue.Style.Add("display", "none")
                            txtChildasadultprice.Style.Add("display", "none")
                            lblchildasadultSaleValue.Style.Add("display", "none")

                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlChildSaleValue.Style.Add("display", "none")
                            txtwlSeniorSaleValue.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")

                            txtAdultPrice.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtSeniorsPrice.Style.Add("display", "none")
                            txtUnitPrice.Style.Add("display", "block")


                            lblAdultSaleValue.Style.Add("display", "none")
                            lblchildSaleValue.Style.Add("display", "none")
                            lblSeniorSaleValue.Style.Add("display", "none")
                            lblUnitSaleValue.Style.Add("display", "block")
                        End If

                    End If

                    If dvSDt.Item(0)("comp_cust").ToString() = "1" Then
                        chkComplimentaryToCustomer.Checked = True
                    Else
                        chkComplimentaryToCustomer.Checked = False
                    End If


                    If Session("sLoginType") = "RO" Then
                        dvComplimentaryToCustomer.Visible = True
                        If chkTourOveridePrice.Checked = True Then
                            txtUnitPrice.ReadOnly = False
                            txtAdultPrice.ReadOnly = False
                            txtChildprice.ReadOnly = False
                            txtChildasadultprice.ReadOnly = False
                            txtSeniorsPrice.ReadOnly = False
                            lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")


                        Else
                            txtUnitPrice.ReadOnly = True
                            txtAdultPrice.ReadOnly = True
                            txtChildprice.ReadOnly = True
                            txtSeniorsPrice.ReadOnly = True
                            txtChildasadultprice.ReadOnly = True
                            lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

                        End If

                        txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(lblAdultSaleValue.ClientID, String) + "')")
                        txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(lblchildSaleValue.ClientID, String) + "')")
                        txtChildasadultprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchildasadult.ClientID, String) + "', '" + CType(txtChildasadultprice.ClientID, String) + "' ,'" + CType(lblchildasadultSaleValue.ClientID, String) + "')")

                        txtSeniorsPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfSeniors.ClientID, String) + "', '" + CType(txtSeniorsPrice.ClientID, String) + "' ,'" + CType(lblSeniorSaleValue.ClientID, String) + "')")
                        txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")
                    Else
                        dvComplimentaryToCustomer.Visible = False
                        txtUnitPrice.ReadOnly = True
                        txtAdultPrice.ReadOnly = True
                        txtChildprice.ReadOnly = True
                        txtSeniorsPrice.ReadOnly = True
                        txtChildasadultprice.ReadOnly = True

                        lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

                    End If


                    If hdBookingEngineRateType.Value = "1" Then

                        dvUnitprice.Style.Add("display", "none")
                        dvunitsalevalue.Style.Add("display", "none")
                    End If
                    mpTotalprice.Show()
                End If
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: lbwlPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub FillCheckBox()
        Dim dtselectedtourfill As DataTable = CType(Session("selectedtourdatatable"), DataTable)
        If Not dtselectedtourfill Is Nothing Then
            If dtselectedtourfill.Rows.Count > 0 Then
                If dlTourSearchResults.Items.Count > 0 Then
                    For Each dlItem As DataListItem In dlTourSearchResults.Items
                        Dim chkSelectTour As CheckBox = CType(dlItem.FindControl("chkSelectTour"), CheckBox)
                        Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
                        Dim txttourchangedate As TextBox = CType(dlItem.FindControl("txttourchangedate"), TextBox)

                        For i As Integer = 0 To dtselectedtourfill.Rows.Count - 1
                            If hdExcCode.Value.Trim = dtselectedtourfill.Rows(i)("exctypcode").ToString Then
                                txttourchangedate.Text = dtselectedtourfill.Rows(i)("excdate").ToString
                                chkSelectTour.Checked = True
                            End If
                        Next


                    Next
                End If
            End If
            

        End If
       

    End Sub

    Private Sub FillTourPickupLocation()
        If Not Session("sRequestId") Is Nothing Then
            Dim dt As DataTable
            dt = objBLLTourSearch.FillTourPickupLocation(Session("sRequestId"))
            If dt.Rows.Count = 1 Then
                txtTourStartingFrom.Text = dt.Rows(0)("othtypname").ToString
                txtTourStartingFromCode.Text = dt.Rows(0)("othtypcode").ToString
            End If
        End If

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
    Protected Sub btnSaveMultipleDates_Click(sender As Object, e As System.EventArgs) Handles btnSaveMultipleDates.Click
        Try
            Dim dt As New DataTable
            dt = Session("selectedCombotourdatatable")
            Dim strExcCode As String = hdMealPlanExcCode.Value
            Dim strExcVehCode As String = hdMealPlanVehicleCode.Value

            For i = dt.Rows.Count - 1 To 0 Step -1
                If dt.Rows(i)("exctypcode") = strExcCode.Trim And dt.Rows(i)("vehiclecode") = strExcVehCode.Trim And dt.Rows(i)("exctypcombocode") = strExcCode.Trim And dt.Rows(i)("type") = "MULTI_DATE" Then
                    dt.Rows.RemoveAt(i)
                End If
            Next


            For Each dlItem As DataListItem In dlMealPlanMultipleDate.Items
                Dim chkMealPlanDates As CheckBox = dlItem.FindControl("chkMealPlanDates")
                If chkMealPlanDates.Checked = True Then
                    Dim lblMeanPlanDates As Label = dlItem.FindControl("lblMeanPlanDates")
                    dt.Rows.Add(strExcCode.Trim, strExcVehCode.Trim, lblMeanPlanDates.Text.Trim, strExcCode.Trim, "MULTI_DATE")
                End If
            
            Next

            Dim strType As String = "MULTI_DATE"
            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbTourTotalSaleValue"), LinkButton)
            Dim dlItemNew As DataListItem = CType((lbTotalPrice).NamingContainer, DataListItem)

            Dim dvComboBreakup As DataView
            dvComboBreakup = New DataView(dt)
            dvComboBreakup.RowFilter = "exctypcode= '" & strExcCode & "'  AND  vehiclecode='" & strExcVehCode & "'  AND type='" & strType & "' "
            Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
            If dtComboBreakup.Rows.Count > 1 Then
                Dim ds As DataSet
                ds = Session("sDSTourSearchResults")

                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & strExcCode & "'  AND vehiclecode='" & strExcVehCode & "' ").First
                    ' Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCodePopup.Value & "' ").First


                    Dim lbPrice As LinkButton = CType(dlTourSearchResults.Items(dlItemNew.ItemIndex).FindControl("lbPrice"), LinkButton)
                    lbPrice.Text = hdCurrCodePopup.Value & " " & Math.Round(Val(dr("totalsalevalue").ToString) * dtComboBreakup.Rows.Count, 2).ToString


                End If
            End If
           

            Session("selectedCombotourdatatable") = dt
            Dim dlNewItem As DataListItem = dlTourSearchResults.Items(dlItemNew.ItemIndex)
            Dim chkSelectTour As CheckBox = CType(dlNewItem.FindControl("chkSelectTour"), CheckBox)
            If dtComboBreakup.Rows.Count > 0 Then
                chkSelectTour.Checked = True
            Else
                chkSelectTour.Checked = False
            End If


            hddlRowNumber.Value = dlItemNew.ItemIndex
            btnSelectedTourCheckbox_Click(sender, e)


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnSaveMultipleDates_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
     
    End Sub
    Protected Sub btnSaveComboExcursion_Click(sender As Object, e As System.EventArgs) Handles btnSaveComboExcursion.Click
        Try


            Dim dt As New DataTable
            dt = Session("selectedCombotourdatatable")
            Dim strExcCode As String = hdExcCodeComboPopup.Value
            Dim strExcVehCode As String = hdVehicleCodeComboPopup.Value
            ' Dim dvCombo As DataView = New DataView(dt)
            For Each dlItem As DataListItem In dlSelectComboDates.Items
                Dim txtExcComboDate As TextBox = dlItem.FindControl("txtExcComboDate")
                If txtExcComboDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select excursion dates")
                    Exit Sub
                End If
                Dim lblExcComboCode As Label = dlItem.FindControl("lblExcComboCode")
                Dim foundRow As DataRow
                foundRow = dt.Select("exctypcode='" & strExcCode.Trim & "' AND  vehiclecode='" & strExcVehCode.Trim & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "'  AND type='COMBO'").FirstOrDefault
                If foundRow Is Nothing Then
                    Dim drNew As DataRow = dt.NewRow()
                    drNew("exctypcode") = strExcCode.Trim
                    drNew("vehiclecode") = strExcVehCode.Trim
                    drNew("excdate") = txtExcComboDate.Text.Trim
                    drNew("exctypcombocode") = lblExcComboCode.Text.Trim
                    drNew("type") = "COMBO"
                    dt.Rows.Add(drNew)

                Else

                    'foundRow("exctypcode") = strExcCode.Trim
                    'foundRow("vehiclecode") = strExcVehCode.Trim
                    foundRow("excdate") = txtExcComboDate.Text.Trim
                    foundRow("exctypcombocode") = lblExcComboCode.Text.Trim
                End If
            Next
            Session("selectedCombotourdatatable") = dt

            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbTourTotalSaleValue"), LinkButton)
            Dim dlItemNew As DataListItem = CType((lbTotalPrice).NamingContainer, DataListItem)
            Dim dlNewItem As DataListItem = dlTourSearchResults.Items(dlItemNew.ItemIndex)
            Dim chkSelectTour As CheckBox = CType(dlNewItem.FindControl("chkSelectTour"), CheckBox)

            chkSelectTour.Checked = True

            hddlRowNumber.Value = dlItemNew.ItemIndex
            btnSelectedTourCheckbox_Click(sender, e)


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnSaveComboExcursion_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub CreateComboTourDataTable()

        Dim SelectComboDT As DataTable = New DataTable("SelectedExc")
        SelectComboDT.Columns.Add("exctypcode", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("vehiclecode", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("excdate", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("exctypcombocode", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("type", Type.GetType("System.String"))
        Session("selectedCombotourdatatable") = SelectComboDT
    End Sub

    Private Sub BindComboTourDataTable(strrequestid As String)
        Dim dt As DataTable
        dt = objBLLTourSearch.BindComboTourDataTable(strrequestid)
        If dt.Rows.Count > 0 Then
            Session("selectedCombotourdatatable") = dt
        End If
    End Sub

    Protected Sub txtSearchTour_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearchTour.TextChanged
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSTourSearchResults")
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: txtSearchTour_TextChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub gvTimeSlot_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTimeSlot.RowDataBound

      
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim objBLLTourSearchTimeSlot1 As BLLTourSearch = New BLLTourSearch
            objBLLTourSearchTimeSlot1 = Session("sobjBLLTourSearch")
            Dim lblAvailabilityCount As Label = CType(e.Row.FindControl("lblAvailabilityCount"), Label)
            Dim lblAvailability As Label = CType(e.Row.FindControl("lblAvailability"), Label)
            If Val(lblAvailabilityCount.Text) > 0 Then
                lblAvailability.Text = "Available"
                lblAvailability.CssClass = "available"
            Else
                lblAvailability.Text = "Not Available"
                lblAvailability.CssClass = "not-available"
            End If
            Dim ddlTimeSlotAdult As DropDownList = CType(e.Row.FindControl("ddlTimeSlotAdult"), DropDownList)
            Dim ddlTimeSlotChild As DropDownList = CType(e.Row.FindControl("ddlTimeSlotChild"), DropDownList)
            Dim ddlTimeSlotSenior As DropDownList = CType(e.Row.FindControl("ddlTimeSlotSenior"), DropDownList)

            objclsUtilities.FillDropDownListBasedOnNumber(ddlTimeSlotAdult, objBLLTourSearchTimeSlot1.Adult)
            objclsUtilities.FillDropDownListBasedOnNumber(ddlTimeSlotChild, objBLLTourSearchTimeSlot1.Children)
            objclsUtilities.FillDropDownListBasedOnNumber(ddlTimeSlotSenior, objBLLTourSearchTimeSlot1.SeniorCitizen)

            Dim lblTimeSlotAdult As Label = CType(e.Row.FindControl("lblTimeSlotAdult"), Label)
            Dim lblTimeSlotChild As Label = CType(e.Row.FindControl("lblTimeSlotChild"), Label)
            Dim lblTimeSlotSenior As Label = CType(e.Row.FindControl("lblTimeSlotSenior"), Label)
            lblTimeSlotAdult.Text = objBLLTourSearchTimeSlot1.Adult
            lblTimeSlotChild.Text = objBLLTourSearchTimeSlot1.Children
            lblTimeSlotSenior.Text = objBLLTourSearchTimeSlot1.SeniorCitizen
            Dim iTotalPax As Integer = Val(lblTimeSlotAdult.Text) + Val(lblTimeSlotChild.Text) + Val(lblTimeSlotSenior.Text)
            ddlTimeSlotAdult.Attributes.Add("OnChange", "return fnValidatePaxCount('" + lblAvailabilityCount.ClientID + "','" + ddlTimeSlotAdult.ClientID + "','" + ddlTimeSlotChild.ClientID + "','" + ddlTimeSlotSenior.ClientID + "','" + iTotalPax.ToString + "','" + lblAvailability.ClientID + "','" + lblTimeSlotAdult.Text + "','" + lblTimeSlotChild.Text + "','" + lblTimeSlotSenior.Text + "')")
            ddlTimeSlotChild.Attributes.Add("OnChange", "return fnValidatePaxCount('" + lblAvailabilityCount.ClientID + "','" + ddlTimeSlotAdult.ClientID + "','" + ddlTimeSlotChild.ClientID + "','" + ddlTimeSlotSenior.ClientID + "','" + iTotalPax.ToString + "','" + lblAvailability.ClientID + "','" + lblTimeSlotAdult.Text + "','" + lblTimeSlotChild.Text + "','" + lblTimeSlotSenior.Text + "')")
            ddlTimeSlotSenior.Attributes.Add("OnChange", "return fnValidatePaxCount('" + lblAvailabilityCount.ClientID + "','" + ddlTimeSlotAdult.ClientID + "','" + ddlTimeSlotChild.ClientID + "','" + ddlTimeSlotSenior.ClientID + "','" + iTotalPax.ToString + "','" + lblAvailability.ClientID + "','" + lblTimeSlotAdult.Text + "','" + lblTimeSlotChild.Text + "','" + lblTimeSlotSenior.Text + "')")
        End If


    End Sub

    Protected Sub btnTimeSlotSave_Click(sender As Object, e As System.EventArgs) Handles btnTimeSlotSave.Click
        If lblTimeSlotWarning.Text <> "" Then
            Exit Sub
        End If

       
        Dim SelectTourTimeSlotDT As DataTable
        If Session("sSelectedTourTimeSlotDatatable") Is Nothing Then
            CreateTourTimeSlotDataTable()
            SelectTourTimeSlotDT = Session("sSelectedTourTimeSlotDatatable")
        Else
            SelectTourTimeSlotDT = Session("sSelectedTourTimeSlotDatatable")
        End If

        Dim strConfNo As String = ""
        Dim strTempConfNo As String = ""
        Dim strTempId As String = ""
        Dim strInvId As String = ""
        If SelectTourTimeSlotDT.Rows.Count > 0 Then

            For i = SelectTourTimeSlotDT.Rows.Count - 1 To 0 Step -1

                If SelectTourTimeSlotDT.Rows(i)("EXC_CODE") = hdExcCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("VEHICLE_CODE") = hdVehicleCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("EXC_TYPE_COMBO_CODE") = "" And SelectTourTimeSlotDT.Rows(i)("TYPE") = "NORMAL" Then
                    SelectTourTimeSlotDT.Rows(i)("IS_CHANGED") = 0
                    If strConfNo = "" Then
                        strConfNo = SelectTourTimeSlotDT.Rows(i)("CONF_NO").ToString
                    End If
                    If strTempConfNo = "" Then
                        strTempConfNo = SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_NO").ToString
                    End If
                    If strTempId = "" Then
                        strTempId = SelectTourTimeSlotDT.Rows(i)("TEMP_ID").ToString
                    End If
                End If
            Next

        End If
        For Each row As GridViewRow In gvTimeSlot.Rows
            Dim lblTimeSlot As Label = CType(row.FindControl("lblTimeSlot"), Label)
            Dim lblTimeSlotFrom As Label = CType(row.FindControl("lblTimeSlotFrom"), Label)
            Dim lblTimeSlotTo As Label = CType(row.FindControl("lblTimeSlotTo"), Label)
            Dim ddlTimeSlotAdult As DropDownList = CType(row.FindControl("ddlTimeSlotAdult"), DropDownList)
            Dim ddlTimeSlotChild As DropDownList = CType(row.FindControl("ddlTimeSlotChild"), DropDownList)
            Dim ddlTimeSlotSenior As DropDownList = CType(row.FindControl("ddlTimeSlotSenior"), DropDownList)
            If ddlTimeSlotAdult.SelectedValue > 0 Or ddlTimeSlotChild.SelectedValue > 0 Or ddlTimeSlotSenior.SelectedValue > 0 Then
                Dim foundRow As DataRow
                foundRow = SelectTourTimeSlotDT.Select("EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND EXC_DATE='" & hdTimeSlotDate.Value & "' AND TIMESLOT='" & lblTimeSlot.Text.Trim & "' AND ADULT='" & ddlTimeSlotAdult.SelectedValue.Trim & "'  AND [CHILD]='" & ddlTimeSlotChild.SelectedValue.Trim & "'   AND SENIOR='" & ddlTimeSlotSenior.SelectedValue.Trim & "' AND TYPE='NORMAL'").FirstOrDefault ' 
                If foundRow Is Nothing Then
                    Dim drNew As DataRow = SelectTourTimeSlotDT.NewRow()
                    drNew("EXC_CODE") = hdExcCodeTimeSlot.Value.Trim
                    drNew("VEHICLE_CODE") = hdVehicleCodeTimeSlot.Value.Trim
                    drNew("EXC_DATE") = hdTimeSlotDate.Value
                    drNew("EXC_TYPE_COMBO_CODE") = ""
                    drNew("TYPE") = "NORMAL"

                    drNew("TIMESLOT_FROM") = lblTimeSlotFrom.Text.Trim
                    drNew("TIMESLOT_TO") = lblTimeSlotTo.Text.Trim
                    drNew("TIMESLOT") = lblTimeSlot.Text.Trim
                    drNew("ADULT") = ddlTimeSlotAdult.SelectedValue.Trim
                    drNew("CHILD") = ddlTimeSlotChild.SelectedValue.Trim
                    drNew("SENIOR") = ddlTimeSlotSenior.SelectedValue.Trim
                    drNew("IS_CHANGED") = 1
                    If strConfNo <> "" Then
                        drNew("CONF_NO") = strConfNo
                    End If
                    If strTempConfNo <> "" Then
                        drNew("TEMP_CONF_NO") = strTempConfNo
                    End If
                    If strTempId <> "" Then
                        drNew("TEMP_ID") = strTempId
                    End If

                    SelectTourTimeSlotDT.Rows.Add(drNew)
                Else
                    foundRow("IS_CHANGED") = 1
                End If
            End If
        Next
        If SelectTourTimeSlotDT.Rows.Count > 0 Then

            For i = SelectTourTimeSlotDT.Rows.Count - 1 To 0 Step -1
                If SelectTourTimeSlotDT.Rows(i)("EXC_CODE") = hdExcCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("VEHICLE_CODE") = hdVehicleCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("EXC_TYPE_COMBO_CODE") = "" And SelectTourTimeSlotDT.Rows(i)("TYPE") = "NORMAL" And SelectTourTimeSlotDT.Rows(i)("IS_CHANGED") = "0" Then
                    SelectTourTimeSlotDT.Rows.Remove(SelectTourTimeSlotDT.Rows(i))
                End If
                If SelectTourTimeSlotDT.Rows.Count = 0 Then
                    Exit For
                End If
            Next

        End If

        If SelectTourTimeSlotDT.Rows.Count > 0 Then

            'Dim dvSelectedForCancel As DataView
            'dvSelectedForCancel = New DataView(SelectTourTimeSlotDT)
            'dvSelectedForCancel.RowFilter = "EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL' AND "

            'Amend Mode
            Dim dvSelectedAmend As DataView
            dvSelectedAmend = New DataView(SelectTourTimeSlotDT)
            dvSelectedAmend.RowFilter = "EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL' AND TEMP_CONF_NO NOT IS NULL  AND CONF_NO NOT IS NULL"
            If dvSelectedAmend.Count > 0 Then
                Dim objBLLTourSearchTimeSlot As BLLTourSearch = New BLLTourSearch
                objBLLTourSearchTimeSlot = Session("sobjBLLTourSearch")
                Dim objAmendRequest As New APIInventoryAmend.AmendRequest


                objAmendRequest.BookingID = dvSelectedAmend(0)("TEMP_ID") ' "RP/000089" ' 'Today.Day.ToString & Today.Hour.ToString & Today.Minute.ToString & Today.Second.ToString
                objAmendRequest.ELineNo = dvSelectedAmend(0)("TEMP_ID")
                objAmendRequest.ConfirmationNo = dvSelectedAmend(0)("CONF_NO")

                Dim objTimeSlots As APIInventoryAmend.TimeSlot
                Dim TimeSlots As List(Of APIInventoryAmend.TimeSlot) = New List(Of APIInventoryAmend.TimeSlot)


                For i As Integer = 0 To dvSelectedAmend.Count - 1
                    objTimeSlots = New APIInventoryAmend.TimeSlot()

                    objTimeSlots.NoOfAdults = dvSelectedAmend(i)("ADULT")
                    objTimeSlots.NoOfChildren = dvSelectedAmend(i)("CHILD")
                    objTimeSlots.NoOfSeniorCitizens = dvSelectedAmend(i)("SENIOR")
                    objTimeSlots.NoOfUnits = 0

                    objTimeSlots.Time = dvSelectedAmend(i)("TIMESLOT_FROM")
                    TimeSlots.Add(objTimeSlots)
                Next
                objAmendRequest.TimeSlots = TimeSlots

                Dim objAPIInventoryController As New APIInventoryController
                Dim objAmendResponse As New APIInventoryAmend.AmendResponse
                objAmendResponse = objAPIInventoryController.AmendInvetory(objAmendRequest)
                If objAmendResponse.Message = "Success" Or objAmendResponse.Message = "Records updated successfully" Then
                    If SelectTourTimeSlotDT.Rows.Count > 0 Then

                        For i As Integer = 0 To SelectTourTimeSlotDT.Rows.Count - 1
                            Dim foundRow As DataRow
                            foundRow = SelectTourTimeSlotDT.Select("EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL'").FirstOrDefault
                            If Not foundRow Is Nothing Then
                                If SelectTourTimeSlotDT.Rows(i)("EXC_CODE") = hdExcCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("VEHICLE_CODE") = hdVehicleCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("TYPE") = "NORMAL" Then
                                    'foundRow("TEMP_CONF_NO") = objSaveAllotmentResponse.TempConfirmationNo
                                    'foundRow("TEMP_CONF_DATE") = Date.Today.ToString("yyyy/MM/dd")
                                    'foundRow("TEMP_ID") = strRequestId
                                    'foundRow("INVENTORY_ID") = hdTimeSlotInventoryId.Value
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_NO") = objAmendResponse.TempConfirmationNo
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_DATE") = Date.Today.ToString("yyyy/MM/dd")
                                    ' SelectTourTimeSlotDT.Rows(i)("TEMP_ID") = strRequestId
                                    SelectTourTimeSlotDT.Rows(i)("INVENTORY_ID") = hdTimeSlotInventoryId.Value
                                End If


                            End If

                        Next

                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, objAmendResponse.Message)
                End If
            End If


            'Edit Mode
            Dim dvSelectedEdit As DataView
            dvSelectedEdit = New DataView(SelectTourTimeSlotDT)
            dvSelectedEdit.RowFilter = "EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL' AND TEMP_CONF_NO NOT IS NULL  AND CONF_NO IS NULL"
            If dvSelectedEdit.Count > 0 Then
                Dim objBLLTourSearchTimeSlot As BLLTourSearch = New BLLTourSearch
                objBLLTourSearchTimeSlot = Session("sobjBLLTourSearch")
                Dim objAmendRequest As New APIInventoryAmend.EditRequest


                objAmendRequest.BookingID = dvSelectedEdit(0)("TEMP_ID") 'Today.Day.ToString & Today.Hour.ToString & Today.Minute.ToString & Today.Second.ToString
                objAmendRequest.ELineNo = dvSelectedEdit(0)("TEMP_ID")
                objAmendRequest.TempConfirmationNo = dvSelectedEdit(0)("TEMP_CONF_NO")

                Dim objTimeSlots As APIInventoryAmend.TimeSlot
                Dim TimeSlots As List(Of APIInventoryAmend.TimeSlot) = New List(Of APIInventoryAmend.TimeSlot)


                For i As Integer = 0 To dvSelectedEdit.Count - 1
                    objTimeSlots = New APIInventoryAmend.TimeSlot()

                    objTimeSlots.NoOfAdults = dvSelectedEdit(i)("ADULT")
                    objTimeSlots.NoOfChildren = dvSelectedEdit(i)("CHILD")
                    objTimeSlots.NoOfSeniorCitizens = dvSelectedEdit(i)("SENIOR")
                    objTimeSlots.NoOfUnits = 0

                    objTimeSlots.Time = dvSelectedEdit(i)("TIMESLOT_FROM")
                    TimeSlots.Add(objTimeSlots)
                Next
                objAmendRequest.TimeSlots = TimeSlots

                Dim objAPIInventoryController As New APIInventoryController
                Dim objAmendResponse As New APIInventoryAmend.AmendResponse
                objAmendResponse = objAPIInventoryController.EditInvetory(objAmendRequest)
                If objAmendResponse.Message = "Success" Or objAmendResponse.Message = "Records updated successfully" Then
                    If SelectTourTimeSlotDT.Rows.Count > 0 Then

                        For i As Integer = 0 To SelectTourTimeSlotDT.Rows.Count - 1
                            Dim foundRow As DataRow
                            foundRow = SelectTourTimeSlotDT.Select("EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL'").FirstOrDefault
                            If Not foundRow Is Nothing Then
                                If SelectTourTimeSlotDT.Rows(i)("EXC_CODE") = hdExcCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("VEHICLE_CODE") = hdVehicleCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("TYPE") = "NORMAL" Then
                                    'foundRow("TEMP_CONF_NO") = objSaveAllotmentResponse.TempConfirmationNo
                                    'foundRow("TEMP_CONF_DATE") = Date.Today.ToString("yyyy/MM/dd")
                                    'foundRow("TEMP_ID") = strRequestId
                                    'foundRow("INVENTORY_ID") = hdTimeSlotInventoryId.Value
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_NO") = objAmendResponse.TempConfirmationNo
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_DATE") = Date.Today.ToString("yyyy/MM/dd")
                                    ' SelectTourTimeSlotDT.Rows(i)("TEMP_ID") = strRequestId
                                    SelectTourTimeSlotDT.Rows(i)("INVENTORY_ID") = hdTimeSlotInventoryId.Value
                                End If


                            End If

                        Next

                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, objAmendResponse.Message)
                End If
            End If


            'New Mode
            Dim dvSelected As DataView
            dvSelected = New DataView(SelectTourTimeSlotDT)
            dvSelected.RowFilter = "EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL' AND TEMP_CONF_NO IS NULL"
            If dvSelected.Count > 0 Then
                Dim objBLLTourSearchTimeSlot As BLLTourSearch = New BLLTourSearch
                objBLLTourSearchTimeSlot = Session("sobjBLLTourSearch")
                Dim objSaveAllotment As New APIInventoryConfirm.SaveAllotment
                objSaveAllotment.AgentID = objBLLTourSearchTimeSlot.AgentCode
                Dim objclsUtilities As New clsUtilities
                Dim strRequestId As String = objclsUtilities.GetExcInventoryTempId()

                'If Not Session("sRequestId") Is Nothing Then
                '    strRequestId = Session("sRequestId")
                'End If
                objSaveAllotment.BookingID = strRequestId 'Today.Day.ToString & Today.Hour.ToString & Today.Minute.ToString & Today.Second.ToString
                objSaveAllotment.ELineNo = strRequestId
                objSaveAllotment.ExcursionCode = hdExcCodeTimeSlot.Value.Trim
                objSaveAllotment.ExcursionDate = Format(CType(hdTimeSlotDate.Value, Date), "yyyy/MM/dd") '
                objSaveAllotment.InventoryID = hdTimeSlotInventoryId.Value
                objSaveAllotment.InventoryType = "ACS"
                objSaveAllotment.SourceCountry = objBLLTourSearchTimeSlot.SourceCountryCode
                Dim objTimeSlots As APIInventoryConfirm.TimeSlot
                Dim TimeSlots As List(Of APIInventoryConfirm.TimeSlot) = New List(Of APIInventoryConfirm.TimeSlot)


                For i As Integer = 0 To dvSelected.Count - 1
                    objTimeSlots = New APIInventoryConfirm.TimeSlot()
                    If objSaveAllotment.InventoryType = "ACS" Then
                        objTimeSlots.NoOfAdults = dvSelected(i)("ADULT")
                        objTimeSlots.NoOfChildren = dvSelected(i)("CHILD")
                        objTimeSlots.NoOfSeniorCitizens = dvSelected(i)("SENIOR")
                        objTimeSlots.NoOfUnits = 0

                    Else
                        objTimeSlots.NoOfAdults = 0
                        objTimeSlots.NoOfChildren = 0
                        objTimeSlots.NoOfSeniorCitizens = 0
                        objTimeSlots.NoOfUnits = Val(dvSelected(i)("ADULT")) + Val(dvSelected(i)("CHILD")) + Val(dvSelected(i)("SENIOR"))
                    End If
                    objTimeSlots.Time = dvSelected(i)("TIMESLOT_FROM")
                    TimeSlots.Add(objTimeSlots)
                Next
                objSaveAllotment.TimeSlots = TimeSlots

                Dim objAPIInventoryController As New APIInventoryController
                Dim objSaveAllotmentResponse As New APIInventoryConfirm.SaveAllotmentResponse
                objSaveAllotmentResponse = objAPIInventoryController.SaveAllotment(objSaveAllotment)
                If objSaveAllotmentResponse.Message = "Success" Then
                    If SelectTourTimeSlotDT.Rows.Count > 0 Then

                        For i As Integer = 0 To SelectTourTimeSlotDT.Rows.Count - 1
                            Dim foundRow As DataRow
                            foundRow = SelectTourTimeSlotDT.Select("EXC_CODE='" & hdExcCodeTimeSlot.Value.Trim & "' AND  VEHICLE_CODE='" & hdVehicleCodeTimeSlot.Value.Trim & "' AND EXC_TYPE_COMBO_CODE='" & "" & "'  AND TYPE='NORMAL'").FirstOrDefault
                            If Not foundRow Is Nothing Then
                                If SelectTourTimeSlotDT.Rows(i)("EXC_CODE") = hdExcCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("VEHICLE_CODE") = hdVehicleCodeTimeSlot.Value.Trim And SelectTourTimeSlotDT.Rows(i)("TYPE") = "NORMAL" Then
                                    'foundRow("TEMP_CONF_NO") = objSaveAllotmentResponse.TempConfirmationNo
                                    'foundRow("TEMP_CONF_DATE") = Date.Today.ToString("yyyy/MM/dd")
                                    'foundRow("TEMP_ID") = strRequestId
                                    'foundRow("INVENTORY_ID") = hdTimeSlotInventoryId.Value
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_NO") = objSaveAllotmentResponse.TempConfirmationNo
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_CONF_DATE") = Date.Today.ToString("yyyy/MM/dd")
                                    SelectTourTimeSlotDT.Rows(i)("TEMP_ID") = strRequestId
                                    SelectTourTimeSlotDT.Rows(i)("INVENTORY_ID") = hdTimeSlotInventoryId.Value
                                End If


                            End If

                        Next

                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, objSaveAllotmentResponse.Message)
                End If
            End If






        End If
        Session("sSelectedTourTimeSlotDatatable") = SelectTourTimeSlotDT

    End Sub

    Private Sub CreateTourTimeSlotDataTable()
        Dim SelectTourTimeSlotDT As DataTable = New DataTable("SelectTourTimeSlotDT")
        SelectTourTimeSlotDT.Columns.Add("EXC_CODE", Type.GetType("System.String"))
        SelectTourTimeSlotDT.Columns.Add("VEHICLE_CODE", Type.GetType("System.String"))
        SelectTourTimeSlotDT.Columns.Add("EXC_DATE", Type.GetType("System.String"))
        SelectTourTimeSlotDT.Columns.Add("EXC_TYPE_COMBO_CODE", Type.GetType("System.String"))
        SelectTourTimeSlotDT.Columns.Add("TYPE", Type.GetType("System.String"))
        SelectTourTimeSlotDT.Columns.Add("TIMESLOT_FROM", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("TIMESLOT_TO", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("TIMESLOT", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("ADULT", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("CHILD", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("SENIOR", GetType(String))

        SelectTourTimeSlotDT.Columns.Add("TEMP_CANCEL_NO", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("TEMP_CANCEL_DATE", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("TEMP_CONF_NO", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("TEMP_CONF_DATE", GetType(String))

        SelectTourTimeSlotDT.Columns.Add("CANCEL_NO", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("CANCEL_DATE", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("CONF_NO", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("CONF_DATE", GetType(String))

        SelectTourTimeSlotDT.Columns.Add("TEMP_ID", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("INVENTORY_ID", GetType(String))
        SelectTourTimeSlotDT.Columns.Add("IS_CHANGED", GetType(String))
        Session("sSelectedTourTimeSlotDatatable") = SelectTourTimeSlotDT
        Return
    End Sub

    Private Sub BindExcInventoryDataTable(strrequestid As String, strSectorCode As String)
        Dim dt As DataTable
        dt = objBLLTourSearch.BindExcInventoryDataTable(strrequestid, strSectorCode)
        If dt.Rows.Count > 0 Then
            Session("sSelectedTourTimeSlotDatatable") = dt
        End If
    End Sub

End Class
