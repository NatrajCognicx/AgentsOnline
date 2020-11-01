Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Threading
Imports System.Globalization
Imports System.Web.Configuration
Imports System.Windows.Forms

Partial Class Home
    Inherits System.Web.UI.Page
    Dim objBLLLogin As New BLLLogin
    Dim objBLLMenu As New BLLMenu
    Dim objBLLHome As New BLLHome
    Dim objclsUtilities As New clsUtilities
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim iCumulative As Integer = 0
    Dim objResParam As New ReservationParameters
    Dim hdThread As HiddenField
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Session("Status_dtAdultChilds") = "" '*** Danny 06/09/2018
            Session("dtAdultChilds") = Nothing
            If Not Session("strPopularDealHTML") Is Nothing Then
                Session("strPopularDealHTML") = Nothing
            End If
            If Not Session("strOffersHTML") Is Nothing Then
                Session("strOffersHTML") = Nothing
            End If


            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                hdWhiteLabel.Value = objResParam.WhiteLabel
            End If
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
                ClearallBookingSessions()
                Session("sFinalBooked") = "0"

            End If
            If Session("sobjBLLHotelSearchActive") Is Nothing = True Then
                LoadHome()
            Else
                LoadHome()
                LoadTrfdetails()
                LoadFlightDetails()
            End If


        End If

        ' AutoCompleteExtender_txtHotelName.ContextKey = txtDestinationName.Text
    End Sub
    Private Sub CreateImageMap()

        Dim strImageMap As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=60")
        imagemapcommon.InnerHtml = strImageMap
    End Sub
    Protected Overrides Sub InitializeCulture()
        Dim language As String = "en-us"

        If Not Session("sLang") Is Nothing Then
            language = Session("sLang")
        End If
        'Set the Culture.
        Thread.CurrentThread.CurrentCulture = New CultureInfo(language)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(language)
    End Sub
    Private Sub LoadTrfdetails()
        Dim strQuery As String = ""

        Dim recordexists As String = ""


        Dim dsTrfdetails As New DataSet
        Dim objDALHotelSearch As New DALHotelSearch
        Dim objBLLHotelSearch = New BLLHotelSearch
        objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
        Dim hotelcount As Integer
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            strQuery = "select count(distinct partycode) from booking_hotel_detailtemp(nolock) where requestid='" & objBLLHotelSearch.OBrequestid & "'"
            hotelcount = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            If hotelcount > 1 Then
                divinterhotel.Style.Add("display", "block")
            Else
                divinterhotel.Style.Add("display", "none")
            End If

            strQuery = "select 't' from booking_transferstemp(nolock) where  requestid='" & objBLLHotelSearch.OBrequestid & "'"
            recordexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)


        End If








        dsTrfdetails = objBLLHotelSearch.FillTransferDetails(objBLLHotelSearch.OBrequestid)

        txtTrfCustomercode.Text = objBLLHotelSearch.CustomerCode
        strQuery = "select agentname from agentmast(nolock) where active=1 and agentcode='" & objBLLHotelSearch.AgentCode & "'"
        txtTrfCustomer.Text = objBLLHotelSearch.Customer
        txtTrfSourcecountry.Text = objBLLHotelSearch.SourceCountry
        txtTrfSourcecountrycode.Text = objBLLHotelSearch.SourceCountryCode

        txtothCustomer.Text = objBLLHotelSearch.Customer
        txtothCustomercode.Text = objBLLHotelSearch.CustomerCode
        txtothSourceCountry.Text = objBLLHotelSearch.SourceCountry
        txtothSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode


        txtMACustomercode.Text = objBLLHotelSearch.CustomerCode
        txtMACustomer.Text = objBLLHotelSearch.Customer
        txtMASourcecountry.Text = objBLLHotelSearch.SourceCountry
        txtMASourcecountrycode.Text = objBLLHotelSearch.SourceCountryCode

        If dsTrfdetails.Tables(0).Rows.Count > 0 Then
            Dim childagestring As String() = dsTrfdetails.Tables(0).Rows(0)("childages").ToString.Split(";")
            ddlTrfAdult.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("adults")
            ddlTrfChild.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child")

            If childagestring.Length <> 0 Then
                txtTrfChild1.Text = childagestring(0)
            End If

            If childagestring.Length > 1 Then
                txtTrfChild2.Text = childagestring(1)
            End If
            If childagestring.Length > 2 Then
                txtTrfChild3.Text = childagestring(2)
            End If
            If childagestring.Length > 3 Then
                txtTrfChild4.Text = childagestring(3)
            End If
            If childagestring.Length > 4 Then
                txtTrfChild5.Text = childagestring(4)
            End If
            If childagestring.Length > 5 Then
                txtTrfChild6.Text = childagestring(5)
            End If
            If childagestring.Length > 6 Then
                txtTrfChild7.Text = childagestring(6)
            End If
            If childagestring.Length > 7 Then
                txtTrfChild8.Text = childagestring(7)
            End If



            ddlMAAdult.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("adults")
            ddlMAChild.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child")

            ddlOthAdult.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("adults")
            ddlOthChild.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child")

            If childagestring.Length <> 0 Then
                'ddlOthChild1.SelectedValue = childagestring(0)
                txtOthChild1.Text = childagestring(0)
            End If

            If childagestring.Length > 1 Then
                'ddlOthChild2.SelectedValue = childagestring(1)
                txtOthChild2.Text = childagestring(1)
            End If
            If childagestring.Length > 2 Then
                txtOthChild3.Text = childagestring(2)
            End If
            If childagestring.Length > 3 Then
                txtOthChild4.Text = childagestring(3)
            End If
            If childagestring.Length > 4 Then
                txtOthChild5.Text = childagestring(4)
            End If
            If childagestring.Length > 5 Then
                txtOthChild6.Text = childagestring(5)
            End If
            If childagestring.Length > 6 Then
                txtOthChild7.Text = childagestring(6)
            End If
            If childagestring.Length > 7 Then
                txtOthChild8.Text = childagestring(7)
            End If

            'ddlMAChild1.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child1age")
            'ddlMAChild2.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child2age")
            'ddlMAChild3.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child3age")
            'ddlMAChild4.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child4age")
            'ddlMAChild5.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child5age")
            'ddlMAChild6.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child6age")
            'ddlMAChild7.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child7age")
            'ddlMAChild8.SelectedValue = dsTrfdetails.Tables(0).Rows(0)("child8age")

        End If

        If dsTrfdetails.Tables.Count > 0 Then
            If recordexists = "" Then
                BindArrivaldetails(dsTrfdetails.Tables(1))
                BindDeparturedetails(dsTrfdetails.Tables(2))
            End If
            BindMAArrivaldetails(dsTrfdetails.Tables(1))
            BindMADeparturedetails(dsTrfdetails.Tables(2))
        End If
    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetInterPickup(ByVal prefixText As String) As List(Of String)

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
                'Dim dt As DataTable

                'dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(objBLLHotelSearch.OBrequestid)

                strSqlQry = "select b.partycode,p.partyname from booking_hotel_detailtemp(nolock) b cross apply (select max(b1.checkout) checkout from booking_hotel_detailtemp(nolock) b1 where b1.requestid='" & objBLLHotelSearch.OBrequestid & "') m " _
                     & " inner join partymast(nolock) p on b.partycode=p.partycode inner join sectormaster(nolock) st on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & objBLLHotelSearch.OBrequestid & "' and b.checkout<>m.checkout and p.partyname like  '%" & prefixText & "%' "

            Else
                'strSqlQry = "select partycode,partyname from partymast where active=1 and partyname like  '" & prefixText & "%'  order by partyname"
                strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O' othtypcode,othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
           & " where  partyname like '%" & prefixText & "%' order by partyname "



            End If


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
        End Try

    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetInterDropoff(ByVal prefixText As String) As List(Of String)

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

                strSqlQry = "select b.partycode,p.partyname from booking_hotel_detailtemp(nolock) b cross apply  (select min(b1.checkin) checkin from booking_hotel_detailtemp(nolock) b1 where b1.requestid='" & objBLLHotelSearch.OBrequestid & "') m " _
               & " inner join partymast(nolock) p on b.partycode=p.partycode inner join sectormaster(nolock) st on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & objBLLHotelSearch.OBrequestid & "' and  b.checkin<>m.checkin and p.partyname like  '%" & prefixText & "%' "


            Else
                ' strSqlQry = "select partycode,partyname from partymast where active=1 and partyname like  '" & prefixText & "%' order by partyname"
                strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
           & " where  partyname like '%" & prefixText & "%' order by partyname "



            End If


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
        End Try

    End Function

    Private Sub BindMAArrivaldetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkMAarrival.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL", True, False)
            txtMAArrivaldate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlMAFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtMAArrDropoffcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = "select partyname from partymast where active=1 and partycode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtMAArrDropoff.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMAArrivalpickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = " select   a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtMAArrivalpickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMAArrivalflightCode.Text = dataTable.Rows(0)("flightcode")
            txtMAArrivalflight.Text = dataTable.Rows(0)("flightcode")
            txtMAArrivalTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub BindMADeparturedetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkMADeparture.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE", True, False)
            txtMADeparturedate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlMADepFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtMADepairportdropcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = " select   a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtMADepairportdrop.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMADeppickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = "select partyname from partymast(nolock) where active=1 and partycode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtMADeppickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMADepartureFlightCode.Text = dataTable.Rows(0)("flightcode")
            txtMADepartureFlight.Text = dataTable.Rows(0)("flightcode")
            txtMADepartureTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub BindDeparturedetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkDeparture.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE", True, False)
            txtTrfDeparturedate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlTrfDepFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtTrfDepairportdropcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = " select   a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtTrfDepairportdrop.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtTrfDeppickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = "select partyname from partymast(nolock) where active=1 and partycode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtTrfDeppickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtDepartureFlightCode.Text = dataTable.Rows(0)("flightcode")
            txtDepartureFlight.Text = dataTable.Rows(0)("flightcode")
            txtDepartureTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub BindArrivaldetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkarrival.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL", True, False)
            txtTrfArrivaldate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlTrfArrFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtTrfArrDropoffcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = "select partyname from partymast(nolock) where active=1 and partycode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtTrfArrDropoff.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtTrfArrivalpickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = " select   a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtTrfArrivalpickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtArrivalflightCode.Text = dataTable.Rows(0)("flightcode")
            txtArrivalflight.Text = dataTable.Rows(0)("flightcode")
            txtArrivalTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub LoadHome()
        LoadFooter()
        ddlAvailability.SelectedValue = "2"
        hdChildAgeLimit.Value = objResParam.ChildAgeLimit
        hdMaxNoOfNight.Value = objResParam.NoOfNightLimit
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS) '*** Danny 06/09/2018
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 06/09/2018

        '*** 1) Create Base Map Image
        '*** 2) Create Mouse over maps
        '*** 3) Create Image Map HTML and save to reservation_parameters is 60
        '*** 4) Style create in Company Stylesheet in "regions1-holder map" / "regions1-nav ul li" / 
        CreateImageMap()

        Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
        Dim objBLLHotelSearch As New BLLHotelSearch()
        iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
        hdBookingEngineRateType.Value = iCumulative.ToString

        LoadFields()
        BindPropertyType()
        BindMealPlan() ''** Shahul 26/06/2018
        BindHotelCheckInAndCheckOutHiddenfield()
        LoadRoomAdultChild()
        BindTourChildAge()

        BindTrfflightclass()
        BindSourceCountry()
        ShowMyBooking()
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If

        ''** Shahul 26/06/2018
        Dim showMeal As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=3002")
        If showMeal = "0" Then
            divmeal.Style.Add("display", "none")
        Else
            divmeal.Style.Add("display", "block")
        End If

        Dim showcatall As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=3003")
        If showcatall = "0" Then
            divstarshow.Style.Add("display", "none")
        Else
            divstarshow.Style.Add("display", "block")
        End If

        '*** Danny 14/07/2018
        Dim strPlaceHolder As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=10")
        If strPlaceHolder = "" Then
            txtDestinationName.Attributes.Add("placeholder", "Example: Type destination")
        Else
            txtDestinationName.Attributes.Add("placeholder", "Example: " & strPlaceHolder.ToString())
        End If
    End Sub
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                ' imgLogo.Src = "Logos/" & strLogo
                imgLogo.Src = Session("sLogo")  '' ***** Danny 30/06/18
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
            objclsUtilities.WriteErrorLog("Home.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub BindHotelCheckInAndCheckOutHiddenfield()
        Dim objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
            Dim dt As DataTable
            dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(objBLLHotelSearch.OBrequestid)
            If dt.Rows.Count > 0 Then
                hdCheckInPrevDay.Value = dt.Rows(0)("CheckInPrevDay").ToString
                'hdCheckInNextDay.Value = dt.Rows(0)("CheckInNextDay").ToString
                ' hdCheckOutPrevDay.Value = dt.Rows(0)("CheckOutPrevDay").ToString
                hdCheckOutNextDay.Value = dt.Rows(0)("CheckOutNextDay").ToString
                txtTourFromDate.Text = objBLLHotelSearch.CheckIn
                txtTourToDate.Text = objBLLHotelSearch.CheckOut

                txtothFromDate.Text = objBLLHotelSearch.CheckIn
                txtothToDate.Text = objBLLHotelSearch.CheckOut

                If Session("sLoginType") = "RO" Then
                    txtTourCustomerCode.Text = objBLLHotelSearch.CustomerCode
                    txtTourCustomer.Text = objBLLHotelSearch.Customer

                    txtTourSourceCountry.Text = objBLLHotelSearch.SourceCountry
                    txtTourSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode

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
    <System.Web.Script.Services.ScriptMethod()> _
  <System.Web.Services.WebMethod()> _
    Public Shared Function GetMATranArrivalflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Arrivalflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

            'strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where  flightcode like  '" & prefixText & "%' "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                Next

            End If
            Return Arrivalflight
        Catch ex As Exception
            Return Arrivalflight
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
   <System.Web.Services.WebMethod()> _
    Public Shared Function GetMAArrivalflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Arrivalflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim strRequestId As String = ""
            strRequestId = GetRequestId()

            If strRequestId <> "" Then
                strSqlQry = "select arrflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,arrflightcode flightcode from booking_guest_flightstemp(nolock) where requestid='" & strRequestId & "' "

                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "


                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    End If
                End If

            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                End If

            End If


            Return Arrivalflight
        Catch ex As Exception
            Return Arrivalflight
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetMATranArrivalpickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
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
    Public Shared Function GetMAArrivalpickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
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
    Public Shared Function GetMAArrDropoff(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  partycode,partyname from partymast(nolock)  where active=1 and partyname like  '" & prefixText & "%' order by partyname "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    dropoffs.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return dropoffs
        Catch ex As Exception
            Return dropoffs
        End Try

    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetMAtranDepartureflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Departureflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                Next

            End If
            Return Departureflight
        Catch ex As Exception
            Return Departureflight
        End Try

    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetMADepartureflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Departureflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim strRequestId As String = ""
            strRequestId = GetRequestId()

            If strRequestId <> "" Then
                strSqlQry = "select depflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,depflightcode flightcode from booking_guest_flightstemp(nolock) where requestid='" & strRequestId & "' "
                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    End If
                End If
            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                End If
            End If
            Return Departureflight
        Catch ex As Exception
            Return Departureflight
        End Try

    End Function
    <WebMethod()> _
    Public Shared Function GetMATransitDepartureAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername,airportbordercode  from view_flightmast_departure where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function GetMADepartureAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername,airportbordercode  from view_flightmast_departure(nolock) where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <System.Web.Script.Services.ScriptMethod()> _
 <System.Web.Services.WebMethod()> _
    Public Shared Function GetMACustomer(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                ' strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast(nolock) where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
                '' added shahul 10/11/18 if we took two tabs then division saved wrong in table 
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode= '" & HttpContext.Current.Session("sDivCode") & "' and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
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
    Public Shared Function GetMACountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

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
    Public Shared Function GetMACountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "MACountries")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetMADeppickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  partycode,partyname from partymast(nolock)  where active=1 and partyname like  '%" & prefixText & "%' order by partyname "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    dropoffs.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return dropoffs
        Catch ex As Exception
            Return dropoffs
        End Try

    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetMATranDeparturepickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If

            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    dropoffs.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return dropoffs
        Catch ex As Exception
            Return dropoffs
        End Try

    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetMADepairportdrop(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function
    <WebMethod()> _
    Public Shared Function GetMATransitAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            'strSqlQry = "select distinct destintime from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername,airportbordercode  from view_flightmast_arrival(nolock) where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function GetMAAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            'strSqlQry = "select distinct destintime from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername,airportbordercode  from view_flightmast_arrival(nolock) where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub BindTourChildAge()
        Try

            Dim objBLLCommonFuntions = New BLLCommonFuntions
            Dim strRequestId As String = ""
            If Not Session("sRequestId") Is Nothing Then
                strRequestId = Session("sRequestId")
                Dim dt As DataTable
                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(strRequestId)
                If dt.Rows.Count > 0 Then
                    hdTab.Value = "1"
                    ddlTourAdult.SelectedValue = dt.Rows(0)("adults").ToString
                    ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString
                    If dt.Rows(0)("child").ToString <> "0" Then
                        Dim strChildAges As String() = dt.Rows(0)("childages").ToString.Split(";")
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 1 Then
                            txtTourChild1.Text = strChildAges(0)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 2 Then
                            txtTourChild2.Text = strChildAges(1)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 3 Then
                            txtTourChild3.Text = strChildAges(2)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 4 Then
                            txtTourChild4.Text = strChildAges(3)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 5 Then
                            txtTourChild5.Text = strChildAges(4)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 6 Then
                            txtTourChild6.Text = strChildAges(5)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 7 Then
                            txtTourChild7.Text = strChildAges(6)
                        End If
                        If CType(dt.Rows(0)("child").ToString, Integer) >= 8 Then
                            txtTourChild8.Text = strChildAges(7)
                        End If


                        If dt.Rows(0)("reqoverride").ToString = "1" Then
                            chkTourOveridePrice.Checked = True
                        Else
                            chkTourOveridePrice.Checked = False
                        End If

                    End If
                End If
            End If

        Catch ex As Exception

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
                Dim strLogo As String = objDataTable.Rows(0)("logofilename").ToString
                If strLogo <> "" Then
                    imgLogo.Src = Session("sLogo") '"Logos/" & strLogo.Trim  ***** Danny 30/06/18
                    ' Session("sLogo") = strLogo.Trim
                End If
            End If
        End If

        If Not Session("sLoginType") Is Nothing Then
            hdLoginType.Value = Session("sLoginType")
            If Session("sLoginType") <> "RO" Then
                dvForRO.Visible = False
                dvPreHotelAgent.Visible = False
                dvOverridePrice.Visible = False

                dvTourCustomer.Visible = False
                dvTourOveridePrice.Visible = False
                divTrfOverride.Visible = False
                dvTrfCustomer.Visible = False
                divothcustomer.Visible = False

                dvMACustomer.Visible = False
                divMAOverride.Visible = False
                divothoverride.Visible = False

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

                        txtTourSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtTourSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString

                        txtTrfSourcecountrycode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtTrfSourcecountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString


                        txtMASourcecountrycode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtMASourcecountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString


                        txtothSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtothSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString


                        txtCountry.ReadOnly = True
                        AutoCompleteExtender_txtCountry.Enabled = False

                        txtTourSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtTourSourceCountry.Enabled = False

                        txtTrfSourcecountry.ReadOnly = True
                        AutoCompleteExtender_txtTrfSourcecountry.Enabled = False

                        txtMASourcecountry.ReadOnly = True
                        AutoCompleteExtender_txtMASourcecountry.Enabled = False

                        txtothSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtothSourceCountry.Enabled = False

                        txtPreHotelSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtPreHotelSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtPreHotelSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False

                    Else
                        txtCountry.ReadOnly = False
                        AutoCompleteExtender_txtCountry.Enabled = True

                        txtTourSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtTourSourceCountry.Enabled = True

                        txtTrfSourcecountry.ReadOnly = False
                        AutoCompleteExtender_txtTrfSourcecountry.Enabled = True

                        txtMASourcecountry.ReadOnly = False
                        AutoCompleteExtender_txtMASourcecountry.Enabled = True

                        txtothSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtothSourceCountry.Enabled = True

                        txtPreHotelSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else
                dvForRO.Visible = True
                dvOverridePrice.Visible = True
                dvPreHotelAgent.Visible = True
                dvTourCustomer.Visible = True
                dvTourOveridePrice.Visible = True
                divTrfOverride.Visible = True
                dvTrfCustomer.Visible = True

                divMAOverride.Visible = True
                dvMACustomer.Visible = True
                divothoverride.Visible = True
                divothcustomer.Visible = True

            End If
        Else
            hdLoginType.Value = ""
        End If
        '  dtOffers = objBLLHome.GetHottestOffersAndPopularDeal(HttpContext.Current.Session("sLoginType"), HttpContext.Current.Session("GlobalUserName"), HttpContext.Current.Session("sAgentCode"), HttpContext.Current.Session("sCountryCode"))
        'trd = New Thread(AddressOf BindOfferDetails(HttpContext.Current.Session("sLoginType").ToString, HttpContext.Current.Session("GlobalUserName").ToString, HttpContext.Current.Session("sAgentCode").ToString, HttpContext.Current.Session("sCountryCode").ToString))

        Dim parameters As New MyThreadParameters
        parameters.LoginType = HttpContext.Current.Session("sLoginType")
        parameters.UserName = HttpContext.Current.Session("GlobalUserName")
        parameters.AgentCode = HttpContext.Current.Session("sAgentCode").ToString
        parameters.CountryCode = HttpContext.Current.Session("sCountryCode")

        ' Need to uncomment ---- abin on 20190710 ####################################### start
        'Dim trd As Thread
        'trd = New Thread(AddressOf BindOfferDetails)
        'trd.IsBackground = False
        'trd.Start(parameters)
        ' Need to uncomment ---- abin on 20190710 ####################################### end




    End Sub

    Private Function GetOfferString(ByVal strImage As String) As String
        Dim strOffersHTML As New StringBuilder

        strOffersHTML.Append(" <div class='offer-slider-i'>")
        strOffersHTML.Append("<a class='offer-slider-img' href='#'><img alt='' src='" & strImage & "' /></a>")
        strOffersHTML.Append(" <div class='offer-slider-txt'>")
        strOffersHTML.Append(" <div class='offer-slider-link'><a href='#'>No Offer</a></div>")
        strOffersHTML.Append(" <div class='offer-slider-l'><div class='offer-slider-location'>.</div>")
        strOffersHTML.Append(" <nav class='stars'><ul>")
        '*** Danny 06/09/2018
        'strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
        'strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
        'strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
        'strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
        'strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
        strOffersHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
        strOffersHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
        strOffersHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
        strOffersHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
        strOffersHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
        strOffersHTML.Append(" </ul>")
        strOffersHTML.Append(" <div class='clear'></div></nav>  </div>")
        strOffersHTML.Append(" <div class='offer-slider-r'> <b class=""prband"">0$</b> <span>avg/night</span></div>")
        strOffersHTML.Append("<div class='offer-slider-devider'></div>")
        strOffersHTML.Append(" <div class='clear'></div>")
        strOffersHTML.Append(" </div></div>")
        Return strOffersHTML.ToString
    End Function

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
    Public Shared Function GetHotelStars(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select catcode,catname from view_hotel_category(nolock) where catname like  '" & prefixText & "%' order by rankorder "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("catname").ToString(), myDS.Tables(0).Rows(i)("catcode").ToString()))
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
    Public Shared Function GetClassification(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select classificationcode,classificationname from excclassification_header(nolock) where active=1 and classificationname like  '" & prefixText & "%' order by classificationname "
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
    Public Shared Function GetArrivalpickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
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
    Public Shared Function GetTrfDepairportdrop(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast(nolock) f,airportbordersmaster(nolock) a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
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
    Public Shared Function GetTrfArrDropoff(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            ' strSqlQry = " select  partycode,partyname from partymast  where active=1 and partyname like  '" & prefixText & "%' order by partyname "

            strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
            & " where  partyname like '%" & prefixText & "%' order by partyname "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    dropoffs.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return dropoffs
        Catch ex As Exception
            Return dropoffs
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetTrfDeppickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            '  strSqlQry = " select  partycode,partyname from partymast  where active=1 and partyname like  '" & prefixText & "%' order by partyname "

            strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
           & " where  partyname like '%" & prefixText & "%' order by partyname "


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    dropoffs.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return dropoffs
        Catch ex As Exception
            Return dropoffs
        End Try

    End Function

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
                strSqlQry = "select othgrpcode,othgrpname  from  othgrpmast(nolock) where active=1 and othgrpcode not in (select othgrpcode from view_system_othgrp(nolock)) order by othgrpname "
                '  End If
            Else
                strSqlQry = "select othgrpcode,othgrpname  from  othgrpmast(nolock) where active=1 and othgrpcode not in (select othgrpcode from view_system_othgrp(nolock)) order by othgrpname "
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
            If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
                objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")
                Dim dt As DataTable

                dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(objBLLHotelSearch.OBrequestid)
                If dt.Rows.Count > 0 Then
                    strSqlQry = "select othtypcode,othtypname  from  othtypmast(nolock) o,booking_hotel_detailtemp(nolock) d,partymast(nolock) p,sectormaster(nolock) s where o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and d.requestid='" & objBLLHotelSearch.OBrequestid & "' and  o.othtypname  like '%" & LTrim(prefixText) & "%' order by o.othtypname "
                Else
                    strSqlQry = "select othtypcode,othtypname  from  othtypmast(nolock) where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and  othtypname  like '%" & LTrim(prefixText) & "%' order by othtypname "
                End If
            Else
                strSqlQry = "select othtypcode,othtypname  from  othtypmast(nolock) where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and  othtypname  like '%" & LTrim(prefixText) & "%' order by othtypname "
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
                    If strContext(i).Contains("HSC:") Then
                        strStar = strContext(i).Replace("HSC:", "")
                    End If
                    If strContext(i).Contains("PT:") Then
                        strPropType = strContext(i).Replace("PT:", "")
                    End If
                Next


            End If

            Dim str As String = contextKey
            strSqlQry = "select v.partycode,v.partyname from sectormaster s(nolock),catmast c(nolock),view_approved_hotels_new v(nolock) where v.sectorcode=s.sectorcode and v.catcode=c.catcode   and v.partyname like  '%" & prefixText & "%' "

            If strDest.Trim <> "" Then
                strSqlQry = strSqlQry & " and (v.citycode = '" & strDest.Trim & "' or s.sectorcode = '" & strDest.Trim & "' or v.ctrycode='" & strDest & "')  "   ''' Added shahul 27/06/18
            End If

            If strStar.Trim <> "" Then
                strSqlQry = strSqlQry & " and c.catcode = '" & strStar.Trim & "' "
            End If
            If strPropType.Trim <> "" And strPropType.Trim <> "--" Then
                strSqlQry = strSqlQry & " and v.propertytype = '" & strPropType.Trim & "' "
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


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPreHotelName(ByVal prefixText As String) As List(Of String)

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


            strSqlQry = "select v.partycode,v.partyname from sectormaster(nolock) s,catmast(nolock) c,view_approved_hotels_new_not v where v.sectorcode=s.sectorcode  and v.catcode=c.catcode   and v.partyname like  '%" & prefixText & "%' "
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
    Public Shared Function GetUAELocation(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim TourStartingFrom As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = ""
            Dim objBLLHotelSearch = New BLLHotelSearch

            '  strSqlQry = "select othtypcode,othtypname  from  othtypmast(nolock) where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and  othtypname  like '%" & LTrim(prefixText) & "%' order by othtypname "
            strSqlQry = "select destcode,destname,desttype from view_destination_search where destname like  '%" & prefixText & "%' "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("destname").ToString(), myDS.Tables(0).Rows(i)("destcode").ToString() + "|" + myDS.Tables(0).Rows(i)("desttype").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
        End Try

    End Function


    ''' <summary>
    ''' GetPreHotelCustomers
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPreHotelCustomers(ByVal prefixText As String) As List(Of String)

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
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
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

    ''' <summary>
    ''' GetCountry
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPreHotelCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

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

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetTrfCustomer(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                'strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
                '' added shahul 10/11/18 if we took two tabs then division saved wrong in table 
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode= '" & HttpContext.Current.Session("sDivCode") & "' and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
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
    Public Shared Function GetothCustomers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                ' strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
                '' added shahul 10/11/18 if we took two tabs then division saved wrong in table 
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode= '" & HttpContext.Current.Session("sDivCode") & "' and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
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
    Public Shared Function GetCustomers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                'strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast(nolock) where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
                '' added shahul 10/11/18 if we took two tabs then division saved wrong in table 
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode= '" & HttpContext.Current.Session("sDivCode") & "' and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
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
    Public Shared Function GetTrfCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

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

            '    strSqlQry = "select c.ctrycode,c.ctryname from ctrymast c where  c.ctryname like  '" & prefixText & "%'  order by ctryname"

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
    ''' <summary>
    ''' GetHotelsDetails
    ''' </summary>
    ''' <param name="HotelCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetHotelsDetails(ByVal HotelCode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            strSqlQry = "select p.sectorcode,s.sectorname,c.catcode,c.catname from partymast(nolock) p,sectormaster(nolock) s,catmast(nolock) c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' GetTrfCountryDetails
    ''' </summary>
    ''' <param name="CustCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetTrfCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "TrfCountries")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' GetCountryDetails
    ''' </summary>
    ''' <param name="CustCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
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




    ''' <summary>
    ''' GetOtherCountryDetails
    ''' </summary>
    ''' <param name="CustCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetOtherCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "OthCountries")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' GetDepartureflight
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetDepartureflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Departureflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim strRequestId As String = ""
            strRequestId = GetRequestId()

            If strRequestId <> "" Then
                strSqlQry = "select depflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,depflightcode flightcode from booking_guest_flightstemp(nolock) where requestid='" & strRequestId & "' "
                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    End If
                End If
            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                End If
            End If
            '


            Return Departureflight
        Catch ex As Exception
            Return Departureflight
        End Try

    End Function
    ''' <summary>
    ''' GetDepartureAirportAndTimeDetails
    ''' </summary>
    ''' <param name="flightcode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetDepartureAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername,airportbordercode  from view_flightmast_departure(nolock) where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' GetAirportAndTimeDetails
    ''' </summary>
    ''' <param name="flightcode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            'strSqlQry = "select distinct destintime from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername,airportbordercode  from view_flightmast_arrival(nolock) where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' BindPropertyType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindPropertyType()
        Dim strQuery As String = ""
        strQuery = "select propertytypecode,propertytypename from hotel_propertytype(nolock)"
        objclsUtilities.FillDropDownList(ddlPropertType, strQuery, "propertytypecode", "propertytypename", True, "--")
    End Sub
    ''** Shahul 26/06/2018
    Private Sub BindMealPlan()
        Dim strQuery As String = ""
        strQuery = "select mainmealcode from mainmealplans(nolock) order by  rankorder"
        objclsUtilities.FillDropDownList(ddlMealPlan, strQuery, "mainmealcode", "mainmealcode", False, "--")
    End Sub

    ''' <summary>
    ''' BindTrfflightclass
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindTrfflightclass()
        Dim strQuery As String = ""
        strQuery = "select flightclscode,flightclsname from flightclsmast(nolock) where active=1"
        objclsUtilities.FillDropDownList(ddlTrfArrFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddlTrfDepFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")

        objclsUtilities.FillDropDownList(ddlMAFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddlMADepFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddltranarrflightclass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddlMAtrandepflightlass, strQuery, "flightclscode", "flightclsname", True, "--")

        ddltranarrflightclass.SelectedIndex = 2
        ddlMAtrandepflightlass.SelectedIndex = 2

        ddlMAFlightClass.SelectedIndex = 2
        ddlMADepFlightClass.SelectedIndex = 2

        ddlTrfArrFlightClass.SelectedIndex = 2
        ddlTrfDepFlightClass.SelectedIndex = 2
    End Sub
    ''' <summary>
    ''' GetEndDate
    ''' </summary>
    ''' *** Danny 22/07/2018
    ''' <param name="dIn"></param>
    ''' <param name="dOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetEndDate(ByVal dIn As DateTime, ByVal txtNoDay As Integer) As String
        Return dIn.AddDays(txtNoDay).ToString("dd/MM/yyyy")
    End Function
    ''' <summary>
    ''' GetNoOfNights
    ''' </summary>
    ''' <param name="dIn"></param>
    ''' <param name="dOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetNoOfNights(ByVal dIn As String, ByVal dOut As String) As String
        Dim strNoOfNights As String
        strNoOfNights = ""
        strNoOfNights = clsUtilities.SharedExecuteQueryReturnStringValue("select DATEDIFF(day,convert(datetime,'" & dIn & "'),convert(datetime,'" & dOut & "'))")
        Return strNoOfNights
    End Function
    ''' <summary>
    ''' IsHotelBookingExist
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function IsHotelBookingExist() As String
        Dim IsBookingExist As String
        If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
            IsBookingExist = "1"
        Else
            IsBookingExist = "0"
        End If
        Return IsBookingExist
    End Function
    ''' <summary>
    ''' GetArrivalflight
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
     <System.Web.Services.WebMethod()> _
    Public Shared Function GetArrivalflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Arrivalflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim strRequestId As String = ""
            strRequestId = GetRequestId()

            If strRequestId <> "" Then
                strSqlQry = "select arrflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,arrflightcode flightcode from booking_guest_flightstemp(nolock) where requestid='" & strRequestId & "' "

                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "


                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    End If
                End If

            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival(nolock) where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next
                End If

            End If


            Return Arrivalflight
        Catch ex As Exception
            Return Arrivalflight
        End Try

    End Function
    ''' <summary>
    ''' btnSearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            ClearallBookingSessions()
            Dim objBLLHotelSearch As New BLLHotelSearch
            Dim s = Session("dtAdultChilds").ToString

            Dim strDestination As String = txtDestinationName.Text
            Dim strDestType As String = ""
            Dim strDestinationCode As String = ""
            Dim DestinationCodeAndType As String = txtDestinationCode.Text
            Dim strDest As String() = txtDestinationCode.Text.Split("|")
            If strDest.Length = 2 Then
                strDestinationCode = strDest(0)
                strDestType = strDest(1)
            End If
            Dim strCheckIn As String = txtCheckIn.Text
            Dim strCheckOut As String = txtCheckOut.Text
            Dim strNoOfNights As String = txtNoOfNights.Text

            Dim strRoom As String = ddlRoom_Dynamic.SelectedValue


            Dim strSourceCountry As String = txtCountry.Text
            Dim strSourceCountryCode As String = txtCountryCode.Text
            Dim strOrderBy As String = ddlOrderBy.SelectedValue
            Dim strCustomer As String = txtCustomer.Text
            Dim strCustomerCode As String = txtCustomerCode.Text
            Dim strStarCategoryCode As String = txtHotelStarsCode.Text
            Dim strStarCategory As String = txtHotelStars.Text
            Dim strAvailabilty As String = ddlAvailability.SelectedValue
            Dim strPropertyType As String = ddlPropertType.SelectedValue
            Dim strHotels As String = txtHotelName.Text
            Dim strHotelCode As String = txtHotelCode.Text


            'Dim strMealplan As String = IIf(ddlMealPlan.SelectedValue = "None selected", "", ddlMealPlan.SelectedValue)  ''** Shahul 26/06/2018
            'strMealplan = IIf(strMealplan <> "", Replace(strMealplan, ",", ";"), "")


            Dim strMealplan As String = hdmealcode.Value   ''** Shahul 08/07/2018
            strMealplan = IIf(strMealplan <> "", Replace(strMealplan, ",", ";"), "")

            If HttpContext.Current.Session("sLoginType") = "RO" Then

                If strHotelCode = "" And chkOveridePrice.Checked = True Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any hotel name. \n </br></br></br></br></br></br></br></br></br>* If override is ticked then Hotel selection is compulsory.")
                    Exit Sub
                End If
            End If

            If (txtHotelStarsCode.Text = "" And txtHotelStars.Text <> "") Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Category code not select Please select Category again.")
                Exit Sub
            End If

            If (HttpContext.Current.Session("sLoginType") = "RO" And strCustomerCode = "") Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select Customer.")
                Exit Sub
            End If

            If strDestination = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any destination.")
                Exit Sub
            End If
            If strCheckIn = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any check-in date.")
                Exit Sub
            End If
            If strCheckOut = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any check-out date.")
                Exit Sub
            End If
            If strCheckOut = strCheckIn Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Same check-in and check-out date is not allowed.")
                Exit Sub
            End If

            If strRoom = "0" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any rooms.")
                Exit Sub
            End If


            If strSourceCountryCode = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any source country.")
                Exit Sub
            End If

            Dim strRoomString As String = ""

            '*** Danny 26/082018
            If strRoom <> "" Then
                'strRoomString = strRoom

                'If strRoom = "1" Then
                strRoomString = GetRoomString(strRoomString)

                If strRoomString.Contains("Please") Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, strRoomString.ToString)
                    Exit Sub
                End If
                'ElseIf strRoom = "2" Then
                '    strRoomString = GetRoom1String(strRoomString)
                '    strRoomString = GetRoom2String(strRoomString)
                'ElseIf strRoom = "3" Then
                '    strRoomString = GetRoom1String(strRoomString)
                '    strRoomString = GetRoom2String(strRoomString)
                '    'strRoomString = GetRoom3String(strRoomString)                
                'End If
            End If


            Dim strQueryString As String = ""
            Dim strSearchCriteria As String = ""
            If strDestination <> "" Then
                objBLLHotelSearch.Destination = strDestination
                strSearchCriteria = "Destination:" & strDestination
            End If
            If strDestinationCode <> "" Then
                objBLLHotelSearch.DestinationCode = strDestinationCode
            End If
            If DestinationCodeAndType <> "" Then
                objBLLHotelSearch.DestinationCodeAndType = DestinationCodeAndType

            End If

            If strDestType <> "" Then
                objBLLHotelSearch.DestinationType = strDestType
                strSearchCriteria = strSearchCriteria & "||" & " Destination Type:" & strDestType
            End If
            If strCheckIn <> "" Then
                objBLLHotelSearch.CheckIn = strCheckIn
                strSearchCriteria = strSearchCriteria & "||" & " CheckIn:" & strCheckIn
            End If
            If strCheckOut <> "" Then
                objBLLHotelSearch.CheckOut = strCheckOut
                strSearchCriteria = strSearchCriteria & "||" & " CheckOut:" & strCheckOut
            End If
            If strNoOfNights <> "" Then
                objBLLHotelSearch.NoOfNights = strNoOfNights
                strSearchCriteria = strSearchCriteria & "||" & " NoOfNights:" & strNoOfNights
            End If
            If strRoom <> "" Then
                objBLLHotelSearch.Room = strRoom
            End If

            objBLLHotelSearch.RoomString = strRoomString
            strSearchCriteria = strSearchCriteria & "||" & " RoomString:" & strRoomString
            If strSourceCountry <> "" Then
                objBLLHotelSearch.SourceCountry = strSourceCountry
                strSearchCriteria = strSearchCriteria & "||" & " SourceCountry:" & strSourceCountry
            End If
            If strSourceCountryCode <> "" Then
                objBLLHotelSearch.SourceCountryCode = strSourceCountryCode
            End If
            If strOrderBy <> "" Then
                objBLLHotelSearch.OrderBy = strOrderBy
            End If
            If strCustomer <> "" Then
                objBLLHotelSearch.Customer = strCustomer
                strSearchCriteria = strSearchCriteria & "||" & " Agent:" & strCustomer
            End If
            If strCustomerCode <> "" Then
                objBLLHotelSearch.CustomerCode = strCustomerCode
            End If
            If strStarCategory <> "" Then
                objBLLHotelSearch.StarCategory = strStarCategory
                strSearchCriteria = strSearchCriteria & "||" & " StarCategory:" & strStarCategory
            End If
            If strStarCategoryCode <> "" Then
                objBLLHotelSearch.StarCategoryCode = strStarCategoryCode
            End If
            If strAvailabilty <> "" Then
                objBLLHotelSearch.Availabilty = strAvailabilty
                strSearchCriteria = strSearchCriteria & "||" & " Availabilty:" & strAvailabilty
            End If
            If strPropertyType <> "" And strPropertyType <> "--" Then
                objBLLHotelSearch.PropertyType = strPropertyType
                strSearchCriteria = strSearchCriteria & "||" & " PropertyType:" & strPropertyType
            End If
            If strHotels <> "" Then
                objBLLHotelSearch.Hotels = strHotels
                strSearchCriteria = strSearchCriteria & "||" & " Hotels:" & strHotels
            End If
            If strHotelCode <> "" Then
                objBLLHotelSearch.HotelCode = strHotelCode
            End If

            If strMealplan <> "" Then  ''** Shahul 26/06/2018
                objBLLHotelSearch.MealPlan = strMealplan

            End If
            If chkshowall.Checked = True Then

                objBLLHotelSearch.ShowallCategory = "1"
            Else
                objBLLHotelSearch.ShowallCategory = "0"

            End If

            objBLLHotelSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & " LoginType:" & objBLLHotelSearch.LoginType
            objBLLHotelSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLHotelSearch.CustomerCode, Session("sAgentCode"))
            strSearchCriteria = strSearchCriteria & "||" & " AgentCode:" & objBLLHotelSearch.AgentCode
            objBLLHotelSearch.WebuserName = Session("GlobalUserName")

            If chkOveridePrice.Checked = True Then
                objBLLHotelSearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "||" & " OverridePrice:Yes"
            Else
                objBLLHotelSearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "||" & " OverridePrice:No"
            End If
            strSearchCriteria = strSearchCriteria & "||" & " LoggedUserName:" & objBLLHotelSearch.WebuserName
            Dim strRoomsAll() = objBLLHotelSearch.RoomString.Split(";")
            Dim iTotalAdult As Integer = 0
            Dim iTotalChild As Integer = 0
            For i As Integer = 0 To strRoomsAll.Length - 1
                Dim strRooms() = strRoomsAll(i).Split(",")

                iTotalAdult = iTotalAdult + Val(strRooms(1))
                iTotalChild = iTotalChild + Val(strRooms(2))
            Next
            objBLLHotelSearch.Adult = iTotalAdult.ToString
            objBLLHotelSearch.Children = iTotalChild.ToString
            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                ' Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Home Page", "Hotel Search", strSearchCriteria, Session("GlobalUserName"))
            End If
            Session("Status_dtAdultChilds") = 1
            Session("sobjBLLHotelSearch") = objBLLHotelSearch
            Response.Redirect("HotelSearch.aspx", False)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnLogOut_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogOut.Click
        Try
            'Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
            'Session.Clear()
            'Session.Abandon()
            'Response.Redirect(strAbsoluteUrl, False)

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
            objclsUtilities.WriteErrorLog("Home.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    <WebMethod()> _
    Public Shared Function CheckFlight(ByVal Flightcode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select 't' from flightmast(nolock) where type=1 and  flightcode= '" & Flightcode.Trim & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Flightdetails")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function CheckDepFlight(ByVal Flightcode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select 't' from flightmast(nolock) where type=0 and  flightcode= '" & Flightcode.Trim & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "DepFlightdetails")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function CheckMAFlight(ByVal Flightcode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select 't' from flightmast(nolock) where type=1 and  flightcode= '" & Flightcode.Trim & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "MAFlightdetails")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function CheckMADepFlight(ByVal Flightcode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select 't' from flightmast(nolock) where type=0 and  flightcode= '" & Flightcode.Trim & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "MADepFlightdetails")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' btnTrfsearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTrfsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrfsearch.Click
        Try

            Dim objBLLTransferSearch As New BLLTransferSearch
            Dim strSearchCriteria As String = ""
            Dim strAdult As String = ddlTrfAdult.SelectedValue
            Dim strChildren As String = ddlTrfChild.SelectedValue

            Dim strChild1 As String = txtTrfChild1.Text
            Dim strChild2 As String = txtTrfChild2.Text
            Dim strChild3 As String = txtTrfChild3.Text
            Dim strChild4 As String = txtTrfChild4.Text
            Dim strChild5 As String = txtTrfChild5.Text
            Dim strChild6 As String = txtTrfChild6.Text
            Dim strChild7 As String = txtTrfChild7.Text
            Dim strChild8 As String = txtTrfChild8.Text

            Dim strSourceCountry As String = txtTrfSourcecountry.Text
            Dim strSourceCountryCode As String = txtTrfSourcecountrycode.Text

            Dim strCustomer As String = txtTrfCustomer.Text
            Dim strCustomerCode As String = txtTrfCustomercode.Text



            '' Arrival
            If chkarrival.Checked = True Then
                objBLLTransferSearch.ArrTransferType = "ARRIVAL"
                strSearchCriteria = "Arrival:" & "Yes"
            Else
                strSearchCriteria = "Arrival:" & "No"
            End If

            If txtTrfArrivaldate.Text <> "" Then
                objBLLTransferSearch.ArrTransferDate = txtTrfArrivaldate.Text
                strSearchCriteria = strSearchCriteria & "||" & " Arrival Date:" & txtTrfArrivaldate.Text
            End If


            If ddlTrfArrFlightClass.SelectedIndex <> 0 And chkarrival.Checked = True Then
                objBLLTransferSearch.ArrFlightClass = ddlTrfArrFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & " ArrFlightClass:" & ddlTrfArrFlightClass.Text
            End If
            If txtArrivalflight.Text <> "" Then
                objBLLTransferSearch.ArrFlightNo = txtArrivalflight.Text
                strSearchCriteria = strSearchCriteria & "||" & " ArrFlightNo:" & txtArrivalflight.Text
            End If
            If txtArrivalTime.Text <> "" Then
                objBLLTransferSearch.ArrFlightTime = txtArrivalTime.Text
                strSearchCriteria = strSearchCriteria & "||" & " ArrFlightTime:" & txtArrivalTime.Text
            End If
            If txtTrfArrivalpickupcode.Text <> "" Then
                objBLLTransferSearch.ArrPickupCode = txtTrfArrivalpickupcode.Text

            End If

            If txtTrfArrivalpickup.Text <> "" Then
                objBLLTransferSearch.ArrPickupName = txtTrfArrivalpickup.Text
                strSearchCriteria = strSearchCriteria & "||" & " ArrivalPickup:" & txtTrfArrivalpickup.Text
            End If

            If txtTrfArrDropoffcode.Text <> "" Then
                objBLLTransferSearch.ArrDropCode = txtTrfArrDropoffcode.Text
                objBLLTransferSearch.ArrDroptype = txtTrfArrDroptype.Text   '' Added shahul 19/07/18
            End If

            If txtTrfArrDropoff.Text <> "" Then
                objBLLTransferSearch.ArrDropName = txtTrfArrDropoff.Text
                strSearchCriteria = strSearchCriteria & "||" & " DropOffHotel:" & txtTrfArrDropoff.Text
            End If

            If txtTrfSourcecountrycode.Text <> "" Then
                objBLLTransferSearch.TrfSourceCountryCode = txtTrfSourcecountrycode.Text
            End If

            If txtTrfSourcecountry.Text <> "" Then
                objBLLTransferSearch.TrfSourceCountry = txtTrfSourcecountry.Text
                strSearchCriteria = strSearchCriteria & "||" & " SourceCountry:" & txtTrfSourcecountry.Text
            End If

            If txtTrfCustomercode.Text <> "" Then
                objBLLTransferSearch.TrfCustomerCode = txtTrfCustomercode.Text
            End If

            If txtTrfCustomer.Text <> "" Then
                objBLLTransferSearch.TrfCustomer = txtTrfCustomer.Text
                strSearchCriteria = strSearchCriteria & "||" & " Agent:" & txtTrfCustomer.Text
            End If


            If strAdult <> "" Then
                objBLLTransferSearch.TrfAdult = strAdult
                '  strSearchCriteria = strSearchCriteria & "||" & " Adult:" & strAdult
            End If

            '''''''''''
            ''Departure
            If chkDeparture.Checked = True Then
                objBLLTransferSearch.DepTransferType = "DEPARTURE"
                strSearchCriteria = strSearchCriteria & "||" & " Departure:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & " Departure:No"
            End If

            If txtTrfDeparturedate.Text <> "" Then
                objBLLTransferSearch.DepTransferDate = txtTrfDeparturedate.Text
                strSearchCriteria = strSearchCriteria & "||" & " Departure Date:" & txtTrfDeparturedate.Text
            End If


            If ddlTrfDepFlightClass.SelectedIndex <> 0 And chkDeparture.Checked = True Then
                objBLLTransferSearch.DepFlightClass = ddlTrfDepFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & " DepFlightClass:" & ddlTrfDepFlightClass.Text
            End If
            If txtDepartureFlight.Text <> "" Then
                objBLLTransferSearch.DepFlightNo = txtDepartureFlight.Text
                strSearchCriteria = strSearchCriteria & "||" & " DepartureFlight:" & txtDepartureFlight.Text
            End If
            If txtDepartureTime.Text <> "" Then
                objBLLTransferSearch.DepFlightTime = txtDepartureTime.Text
                strSearchCriteria = strSearchCriteria & "||" & " DepFlightTime:" & txtDepartureTime.Text
            End If
            If txtTrfDeppickupcode.Text <> "" Then
                objBLLTransferSearch.DepPickupCode = txtTrfDeppickupcode.Text
                objBLLTransferSearch.DepPickuptype = txtTrfDeppickuptype.Text   '' Added shahul 19/07/18
            End If

            If txtTrfDeppickup.Text <> "" Then
                objBLLTransferSearch.DepPickupName = txtTrfDeppickup.Text
                strSearchCriteria = strSearchCriteria & "||" & " DeparturePickup:" & txtTrfDeppickup.Text
            End If

            If txtTrfDepairportdropcode.Text <> "" Then
                objBLLTransferSearch.DepDropCode = txtTrfDepairportdropcode.Text
            End If

            If txtTrfDepairportdrop.Text <> "" Then
                objBLLTransferSearch.DepDropName = txtTrfDepairportdrop.Text
                strSearchCriteria = strSearchCriteria & "||" & " DepartureDropOff:" & txtTrfDeppickup.Text
            End If

            '''' Shifting

            If chkinter.Checked = True Then
                objBLLTransferSearch.ShiftingTransferType = "INTERHOTEL"
                strSearchCriteria = strSearchCriteria & "||" & " INTERHOTEL:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & " INTERHOTEL:No"
            End If

            If txtTrfinterdate.Text <> "" Then
                objBLLTransferSearch.ShiftingDate = txtTrfinterdate.Text
                strSearchCriteria = strSearchCriteria & "||" & " Inter Hotel Date:" & txtTrfinterdate.Text
            End If

            If txtTrfinterPickupcode.Text <> "" Then
                objBLLTransferSearch.ShiftingPickupCode = txtTrfinterPickupcode.Text
                objBLLTransferSearch.ShiftingPickuptype = txtTrfinterPickuptype.Text  '' Added shahul 21/07/18
            End If

            If txtTrfinterPickupcode.Text <> "" Then
                objBLLTransferSearch.ShiftingPickupName = txtTrfinterPickup.Text
                strSearchCriteria = strSearchCriteria & "||" & " Inter Hotel Pickup:" & txtTrfinterPickup.Text
            End If

            If txtTrfInterdropffcode.Text <> "" Then
                objBLLTransferSearch.ShiftingDropCode = txtTrfInterdropffcode.Text
                objBLLTransferSearch.ShiftingDroptype = txtTrfInterdropfftype.Text '' Added shahul 21/07/18
            End If

            If txtTrfInterdropffcode.Text <> "" Then
                objBLLTransferSearch.ShiftingDropName = txtTrfInterdropff.Text
                strSearchCriteria = strSearchCriteria & "||" & " Inter Hotel DropOff:" & txtTrfInterdropff.Text
            End If

            ''''

            If strAdult <> "" Then
                objBLLTransferSearch.TrfAdult = strAdult
                strSearchCriteria = strSearchCriteria & "||" & " Adult:" & strAdult
            End If



            If strChildren <> "" Then
                objBLLTransferSearch.TrfChildren = strChildren
                strSearchCriteria = strSearchCriteria & "||" & " Children:" & strChildren
                If strChildren = "1" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.ChildAgeString = strChild1
                ElseIf strChildren = "2" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.TrfChild3 = strChild3
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.TrfChild3 = strChild3
                    objBLLTransferSearch.TrfChild4 = strChild4
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.TrfChild3 = strChild3
                    objBLLTransferSearch.TrfChild4 = strChild4
                    objBLLTransferSearch.TrfChild5 = strChild5
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.TrfChild3 = strChild3
                    objBLLTransferSearch.TrfChild4 = strChild4
                    objBLLTransferSearch.TrfChild5 = strChild5
                    objBLLTransferSearch.TrfChild6 = strChild6
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.TrfChild3 = strChild3
                    objBLLTransferSearch.TrfChild4 = strChild4
                    objBLLTransferSearch.TrfChild5 = strChild5
                    objBLLTransferSearch.TrfChild6 = strChild6
                    objBLLTransferSearch.TrfChild7 = strChild7
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7
                ElseIf strChildren = "8" Then
                    objBLLTransferSearch.TrfChild1 = strChild1
                    objBLLTransferSearch.TrfChild2 = strChild2
                    objBLLTransferSearch.TrfChild3 = strChild3
                    objBLLTransferSearch.TrfChild4 = strChild4
                    objBLLTransferSearch.TrfChild5 = strChild5
                    objBLLTransferSearch.TrfChild6 = strChild6
                    objBLLTransferSearch.TrfChild7 = strChild7
                    objBLLTransferSearch.TrfChild8 = strChild8
                    objBLLTransferSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
                End If
            End If
            strSearchCriteria = strSearchCriteria & "||" & " ChildAges:" & objBLLTransferSearch.ChildAgeString

            If chkTrfoverride.Checked = True Then
                objBLLTransferSearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "||" & " OverridePrice:Yes"
            Else
                objBLLTransferSearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "||" & " OverridePrice:No"
            End If

            objBLLTransferSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & " LoginType:" & objBLLTransferSearch.LoginType
            objBLLTransferSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTransferSearch.TrfCustomerCode, Session("sAgentCode")) ' Session("sAgentCode")
            strSearchCriteria = strSearchCriteria & "||" & " AgentCode:" & objBLLTransferSearch.AgentCode
            objBLLTransferSearch.WebUserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "||" & " LoggedUser:" & objBLLTransferSearch.WebUserName

            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                '   Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Home Page", "Transfer Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            Session("sobjBLLTransferSearch") = objBLLTransferSearch
            Response.Redirect("TransferSearch.aspx", False)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnTrfsearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnTourSearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTourSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTourSearch.Click
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

            Dim strOveride As String = chkTourOveridePrice.Checked



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
                End If

            End If

            If strSourceCountryCode = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any source country.")
                Exit Sub
            End If

            Dim strQueryString As String = ""


            If strFromDate <> "" Then
                objBLLTourSearch.FromDate = strFromDate
                strSearchCriteria = "From Date: " & strFromDate
            End If
            If strToDate <> "" Then
                objBLLTourSearch.ToDate = strToDate
                strSearchCriteria = strSearchCriteria & "||To Date: " & strToDate
            End If
            If strTourStartingFrom <> "" Then
                objBLLTourSearch.TourStartingFrom = strTourStartingFrom
                strSearchCriteria = strSearchCriteria & "||TourStartingFrom: " & strTourStartingFrom
            End If
            If strTourStartingFromCode <> "" Then
                objBLLTourSearch.TourStartingFromCode = strTourStartingFromCode
            End If
            If strTourClassification <> "" Then
                objBLLTourSearch.Classification = strTourClassification
                strSearchCriteria = strSearchCriteria & "||TourClassification: " & strTourClassification
            End If
            If strTourClassificationCode <> "" Then
                objBLLTourSearch.ClassificationCode = strTourClassificationCode
            End If

            If strStarCategoryCode <> "" Then
                objBLLTourSearch.StarCategoryCode = strStarCategoryCode
            End If
            If strStarCategory <> "" Then
                objBLLTourSearch.StarCategory = strStarCategory
                strSearchCriteria = strSearchCriteria & "||StarCategory: " & strStarCategory
            End If


            If strSeniorCitizen <> "" Then
                objBLLTourSearch.SeniorCitizen = strSeniorCitizen
                strSearchCriteria = strSearchCriteria & "||SeniorCitizen: " & strSeniorCitizen
            End If

            If strAdult <> "" Then
                objBLLTourSearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "||Adult: " & strAdult
            End If
            If strChildren <> "" Then
                objBLLTourSearch.Children = strChildren
                strSearchCriteria = strSearchCriteria & "||Children: " & strChildren
                If strChildren = "1" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.ChildAgeString = strChild1
                ElseIf strChildren = "2" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = strChild6
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLTourSearch.Child1 = strChild1
                    objBLLTourSearch.Child2 = strChild2
                    objBLLTourSearch.Child3 = strChild3
                    objBLLTourSearch.Child4 = strChild4
                    objBLLTourSearch.Child5 = strChild5
                    objBLLTourSearch.Child6 = strChild6
                    objBLLTourSearch.Child7 = strChild7
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
                    objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
                End If
                strSearchCriteria = strSearchCriteria & "||ChildAgeString: " & objBLLTourSearch.ChildAgeString
            End If

            If strSourceCountry <> "" Then
                objBLLTourSearch.SourceCountry = strSourceCountry
                strSearchCriteria = strSearchCriteria & "||SourceCountry: " & strSourceCountry
            End If
            If strSourceCountryCode <> "" Then
                objBLLTourSearch.SourceCountryCode = strSourceCountryCode
            End If

            If strCustomer <> "" Then
                objBLLTourSearch.Customer = strCustomer
                strSearchCriteria = strSearchCriteria & "||Customer: " & strCustomer
            End If
            If strCustomerCode <> "" Then
                objBLLTourSearch.CustomerCode = strCustomerCode
            End If

            If strStarCategory <> "" Then
                objBLLTourSearch.StarCategory = strStarCategory
                strSearchCriteria = strSearchCriteria & "||StarCategory: " & strStarCategory
            End If
            If strPrivateOrSIC <> "" Then
                objBLLTourSearch.PrivateOrSIC = strPrivateOrSIC
                strSearchCriteria = strSearchCriteria & "||PrivateOrSIC: " & strPrivateOrSIC
            End If


            objBLLTourSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||LoginType: " & objBLLTourSearch.LoginType
            objBLLTourSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTourSearch.CustomerCode, Session("sAgentCode"))
            strSearchCriteria = strSearchCriteria & "||AgentCode: " & objBLLTourSearch.AgentCode
            objBLLTourSearch.WebuserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "||WebuserName: " & objBLLTourSearch.WebuserName
            If chkTourOveridePrice.Checked = True Then
                objBLLTourSearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "||OverridePrice:Yes "
            Else
                objBLLTourSearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "||OverridePrice:No"
            End If

            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                '  Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Home Page", "Tour Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            Session("sobjBLLTourSearch") = objBLLTourSearch
            Response.Redirect("TourSearch.aspx", False)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnTourSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnMAsearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMAsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMAsearch.Click
        Try


            Dim objBLLMASearch As New BLLMASearch
            Dim strSearchCriteria As String = ""
            Dim strAdult As String = ddlMAAdult.SelectedValue
            Dim strChildren As String = ddlMAChild.SelectedValue

            Dim strChild1 As String = txtMAChild1.Text
            Dim strChild2 As String = txtMAChild2.Text
            Dim strChild3 As String = txtMAChild3.Text
            Dim strChild4 As String = txtMAChild4.Text
            Dim strChild5 As String = txtMAChild5.Text
            Dim strChild6 As String = txtMAChild6.Text
            Dim strChild7 As String = txtMAChild7.Text
            Dim strChild8 As String = txtMAChild8.Text

            Dim strSourceCountry As String = txtMASourcecountry.Text
            Dim strSourceCountryCode As String = txtMASourcecountrycode.Text

            Dim strCustomer As String = txtMACustomer.Text
            Dim strCustomerCode As String = txtMACustomercode.Text



            '' Arrival
            If chkMAarrival.Checked = True Then
                objBLLMASearch.MAArrivalType = "ARRIVAL"
                strSearchCriteria = "ARRIVAL:Yes"
            Else
                strSearchCriteria = "ARRIVAL:No"
            End If

            If txtMAArrivaldate.Text <> "" Then
                objBLLMASearch.MAArrTransferDate = txtMAArrivaldate.Text
                strSearchCriteria = strSearchCriteria & "|| Arrival date:" & txtMAArrivaldate.Text
            End If

            If txtMAArrivalflight.Text <> "" Then
                objBLLMASearch.MAArrFlightNo = txtMAArrivalflight.Text
                strSearchCriteria = strSearchCriteria & "|| Arrival flight:" & txtMAArrivalflight.Text
            End If
            If ddlMAFlightClass.SelectedIndex <> 0 And chkMAarrival.Checked = True Then
                objBLLMASearch.MAArrFlightClass = ddlMAFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "|| FlightClass:" & ddlMAFlightClass.SelectedValue
            End If
            If txtMAArrivalTime.Text <> "" Then
                objBLLMASearch.MAArrFlightTime = txtMAArrivalTime.Text
                strSearchCriteria = strSearchCriteria & "|| ArrivalTime:" & txtMAArrivalTime.Text
            End If
            If txtMAArrivalpickupcode.Text <> "" Then
                objBLLMASearch.MAArrPickupCode = txtMAArrivalpickupcode.Text
            End If

            If txtMAArrivalpickup.Text <> "" Then
                objBLLMASearch.MAArrPickupName = txtMAArrivalpickup.Text
                strSearchCriteria = strSearchCriteria & "|| ArrPickupName:" & txtMAArrivalpickup.Text
            End If

            'If txtTrfArrDropoffcode.Text <> "" Then
            '    objBLLTransferSearch.ArrDropCode = txtTrfArrDropoffcode.Text
            'End If

            'If txtTrfArrDropoff.Text <> "" Then
            '    objBLLTransferSearch.ArrDropName = txtTrfArrDropoff.Text
            'End If

            If txtMASourcecountrycode.Text <> "" Then
                objBLLMASearch.SourceCountryCode = txtMASourcecountrycode.Text
            End If

            If txtMASourcecountry.Text <> "" Then
                objBLLMASearch.SourceCountry = txtMASourcecountry.Text
                strSearchCriteria = strSearchCriteria & "|| SourceCountry:" & txtMASourcecountry.Text
            End If

            If txtMACustomercode.Text <> "" Then
                objBLLMASearch.CustomerCode = txtMACustomercode.Text
            End If

            If txtMACustomer.Text <> "" Then
                objBLLMASearch.Customer = txtMACustomer.Text
                strSearchCriteria = strSearchCriteria & "|| Agent:" & txtMACustomer.Text
            End If

            If txtMACustomer.Text <> "" Then
                objBLLMASearch.Customer = txtMACustomer.Text
            End If

            If strAdult <> "" Then
                objBLLMASearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "|| Adult:" & strAdult
            End If

            '''''''''''
            ''Departure
            If chkMADeparture.Checked = True Then
                objBLLMASearch.MADepartueType = "DEPARTURE"
                strSearchCriteria = strSearchCriteria & "|| DEPARTURE:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "|| DEPARTURE:No"
            End If

            If txtMADeparturedate.Text <> "" Then
                objBLLMASearch.MADepTransferDate = txtMADeparturedate.Text
                strSearchCriteria = strSearchCriteria & "|| Departure Date:" & txtMADeparturedate.Text
            End If

            If txtMADepartureFlight.Text <> "" Then
                objBLLMASearch.MADepFlightNo = txtMADepartureFlight.Text
                strSearchCriteria = strSearchCriteria & "|| DepFlightNo:" & txtMADepartureFlight.Text
            End If
            If ddlMADepFlightClass.SelectedIndex <> 0 And chkMADeparture.Checked = True Then
                objBLLMASearch.MADepFlightClass = ddlMADepFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "|| DepFlightClass:" & ddlMADepFlightClass.Text
            End If
            If txtMADepartureTime.Text <> "" Then
                objBLLMASearch.MADepFlightTime = txtMADepartureTime.Text
                strSearchCriteria = strSearchCriteria & "|| DepFlightTime:" & txtMADepartureTime.Text
            End If

            If txtMADepairportdropcode.Text <> "" Then
                objBLLMASearch.MADepDropCode = txtMADepairportdropcode.Text
            End If

            If txtMADepairportdrop.Text <> "" Then
                objBLLMASearch.MADepDropName = txtMADepairportdrop.Text
                strSearchCriteria = strSearchCriteria & "|| DepAirportDrop:" & txtMADepairportdrop.Text
            End If

            ''''''''
            ''Transit
            If chktransit.Checked = True Then
                objBLLMASearch.MATransitType = "TRANSIT"
                strSearchCriteria = strSearchCriteria & "|| TRANSIT:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "|| TRANSIT:No"
            End If



            If txtMATrandepdate.Text <> "" Then
                objBLLMASearch.MATranDepDate = txtMATrandepdate.Text
                strSearchCriteria = strSearchCriteria & "|| TransitDepDate:" & txtMATrandepdate.Text
            End If

            If txtTransitarrdate.Text <> "" Then
                objBLLMASearch.MATranArrDate = txtTransitarrdate.Text
                strSearchCriteria = strSearchCriteria & "|| TransitArrDate:" & txtTransitarrdate.Text
            End If

            If txtMAtranArrFlight.Text <> "" Then
                objBLLMASearch.MATranArrFlightNo = txtMAtranArrFlight.Text
                strSearchCriteria = strSearchCriteria & "|| TranArrFlight:" & txtMAtranArrFlight.Text
            End If

            If txtMAtranDepartureFlight.Text <> "" Then
                objBLLMASearch.MATranDepFlightNo = txtMAtranDepartureFlight.Text
                strSearchCriteria = strSearchCriteria & "|| TranDepartureFlight:" & txtMAtranDepartureFlight.Text
            End If

            If ddlMAtrandepflightlass.SelectedIndex <> 0 And chktransit.Checked = True Then
                objBLLMASearch.MATranDepFlightClass = ddlMAtrandepflightlass.SelectedValue
                strSearchCriteria = strSearchCriteria & "|| TranDepFlightClass:" & ddlMAtrandepflightlass.Text
            End If

            If ddltranarrflightclass.SelectedIndex <> 0 And chktransit.Checked = True Then
                objBLLMASearch.MATranArrFlightClass = ddltranarrflightclass.SelectedValue
                strSearchCriteria = strSearchCriteria & "|| TranArrFlightClass:" & ddltranarrflightclass.Text
            End If

            If txtMATranArrTime.Text <> "" Then
                objBLLMASearch.MATranArrFlightTime = txtMATranArrTime.Text
                strSearchCriteria = strSearchCriteria & "|| TranArrTime:" & txtMATranArrTime.Text
            End If

            If txtMATranDepartureTime.Text <> "" Then
                objBLLMASearch.MATranDepFlightTime = txtMATranDepartureTime.Text
                strSearchCriteria = strSearchCriteria & "|| TranDepFlightTime:" & txtMATranDepartureTime.Text
            End If

            If txtMATransitArrivalpickupcode.Text <> "" Then
                objBLLMASearch.MATranArrPickupCode = txtMATransitArrivalpickupcode.Text
            End If

            If txtMAtranArrivalpickup.Text <> "" Then
                objBLLMASearch.MATranArrPickupName = txtMAtranArrivalpickup.Text
                strSearchCriteria = strSearchCriteria & "|| TranArrPickupName:" & txtMAtranArrivalpickup.Text
            End If

            If txtMATransitDeparturepickupcode.Text <> "" Then
                objBLLMASearch.MATranDepDropCode = txtMATransitDeparturepickupcode.Text
            End If

            If txtMAtranDeppickup.Text <> "" Then
                objBLLMASearch.MATranDepDropName = txtMAtranDeppickup.Text
                strSearchCriteria = strSearchCriteria & "|| TranDepDropName:" & txtMAtranDeppickup.Text
            End If


            ''''

            If chkMAoverride.Checked = True Then
                objBLLMASearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "|| OverridePrice:Yes"
            Else
                objBLLMASearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "|| OverridePrice:No"
            End If


            If strAdult <> "" Then
                objBLLMASearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "|| Adult:" & strAdult
            End If
            If strChildren <> "" Then
                strSearchCriteria = strSearchCriteria & "|| Children:" & strChildren
                objBLLMASearch.Children = strChildren
                If strChildren = "1" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.ChildAgeString = strChild1
                ElseIf strChildren = "2" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.Child6 = strChild6
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.Child6 = strChild6
                    objBLLMASearch.Child7 = strChild7
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7
                ElseIf strChildren = "8" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.Child6 = strChild6
                    objBLLMASearch.Child7 = strChild7
                    objBLLMASearch.Child8 = strChild8
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
                End If
                strSearchCriteria = strSearchCriteria & "|| ChildAgeString:" & objBLLMASearch.ChildAgeString
            End If


            'If chkTrfoverride.Checked = True Then
            '    objBLLMASearch.OverridePrice = "1"
            'Else
            '    objBLLMASearch.OverridePrice = "0"
            'End If

            objBLLMASearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "|| LoginType:" & objBLLMASearch.LoginType
            objBLLMASearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLMASearch.CustomerCode, Session("sAgentCode")) 'Session("sAgentCode")
            strSearchCriteria = strSearchCriteria & "|| AgentCode:" & objBLLMASearch.AgentCode
            objBLLMASearch.WebUserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "|| WebUserName:" & objBLLMASearch.WebUserName
            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                ' Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Home Page", "Airport Meet Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            Session("sobjBLLMASearch") = objBLLMASearch
            Response.Redirect("AirportMeetSearch.aspx", False)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnMAsearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' BindSourceCountry
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindSourceCountry()
        Dim strRequestId As String = ""
        strRequestId = GetRequestId()
        If strRequestId <> "" Then
            Dim strQuery As String = ""
            Dim objBLLHotelSearch2 As New BLLHotelSearch
            Dim dt As DataTable
            strQuery = "select agentcode,(select agentname  from agentmast(nolock) where agentmast.agentcode=booking_headertemp.agentcode)agentname ,sourcectrycode,(select ctryname from ctrymast where ctrycode=sourcectrycode)ctryname from booking_headertemp(nolock) where requestid='" & strRequestId & "'"
            dt = objBLLHotelSearch2.GetResultAsDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("agentname").ToString <> "" Then
                    txtCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                    txtCustomer.Text = dt.Rows(0)("agentname").ToString

                    txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                    txtTourCustomer.Text = dt.Rows(0)("agentname").ToString

                    txtTrfCustomercode.Text = dt.Rows(0)("agentcode").ToString
                    txtTrfCustomer.Text = dt.Rows(0)("agentname").ToString

                    txtMACustomercode.Text = dt.Rows(0)("agentcode").ToString
                    txtMACustomer.Text = dt.Rows(0)("agentname").ToString

                    txtTrfSourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                    txtTrfSourcecountry.Text = dt.Rows(0)("ctryname").ToString

                    txtothCustomercode.Text = dt.Rows(0)("agentcode").ToString
                    txtothCustomer.Text = dt.Rows(0)("agentname").ToString

                    txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                    txtothSourceCountry.Text = dt.Rows(0)("ctryname").ToString

                End If

                txtCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtCustomer.Enabled = False

                txtCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtCountry.Text = dt.Rows(0)("ctryname").ToString
                txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtCountry.Enabled = False

                txtTourCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtTourCustomer.Enabled = False

                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("ctryname").ToString
                txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtTourSourceCountry.Enabled = False

                txtTrfCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtTrfCustomer.Enabled = False

                txtothCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtothCustomer.Enabled = False

                txtTrfSourcecountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtTrfSourcecountry.Enabled = False

                txtMACustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtMACustomer.Enabled = False

                txtMASourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtMASourcecountry.Text = dt.Rows(0)("ctryname").ToString
                txtMASourcecountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtMASourcecountry.Enabled = False

                txtothSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtothSourceCountry.Text = dt.Rows(0)("ctryname").ToString
                txtothSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtothSourceCountry.Enabled = False

            End If
        End If
    End Sub
    ''' <summary>
    ''' GetRequestId
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetRequestId() As String
        Dim strRequestId As String = ""

        If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sRequestId")
        End If
        'Dim objBLLHotelSearch2 As New BLLHotelSearch
        'If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
        '    objBLLHotelSearch2 = CType(HttpContext.Current.Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
        '    strRequestId = objBLLHotelSearch2.OBrequestid
        'ElseIf Not HttpContext.Current.Session("sobjBLLTourSearchActive") Is Nothing Then
        '    Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
        '    objBLLTourSearch = CType(HttpContext.Current.Session("sobjBLLTourSearchActive"), BLLTourSearch)
        '    strRequestId = objBLLTourSearch.EbRequestID
        'ElseIf Not HttpContext.Current.Session("sobjBLLTransferSearchActive") Is Nothing Then
        '    Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
        '    objBLLTransferSearch = CType(HttpContext.Current.Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
        '    strRequestId = objBLLTransferSearch.OBRequestId
        'End If
        Return strRequestId
    End Function

    ''' <summary>
    ''' LoadFlightDetails
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadFlightDetails()
        Dim strRequestId As String = ""
        strRequestId = GetRequestId()
        Dim dt As DataTable
        Dim objBLLTransferSearch As New BLLTransferSearch
        dt = objBLLTransferSearch.LaodTrfFlightDetails(strRequestId)
        If dt.Rows.Count > 0 Then
            If dt.Rows.Count = 1 Then
                txtArrivalflight.Text = dt.Rows(0)("arrflightcode").ToString()
                txtArrivalflightCode.Text = dt.Rows(0)("arrflight_tranid").ToString()
                txtArrivalTime.Text = dt.Rows(0)("arrflighttime").ToString()
                txtTrfArrivalpickup.Text = dt.Rows(0)("arrairportborderName").ToString()
                txtTrfArrivalpickupcode.Text = dt.Rows(0)("arrairportbordercode").ToString()

                txtDepartureFlight.Text = dt.Rows(0)("depflightcode").ToString()
                txtDepartureFlightCode.Text = dt.Rows(0)("depflight_tranid").ToString()
                txtDepartureTime.Text = dt.Rows(0)("depflighttime").ToString()
                txtTrfDepairportdrop.Text = dt.Rows(0)("depairportborderName").ToString()
                txtTrfDepairportdropcode.Text = dt.Rows(0)("depairportbordercode").ToString()

                txtMAArrivalflight.Text = dt.Rows(0)("arrflightcode").ToString()
                txtMAArrivalflightCode.Text = dt.Rows(0)("arrflight_tranid").ToString()
                txtMAArrivalTime.Text = dt.Rows(0)("arrflighttime").ToString()
                txtMAArrivalpickup.Text = dt.Rows(0)("arrairportborderName").ToString()
                txtMAArrivalpickupcode.Text = dt.Rows(0)("arrairportbordercode").ToString()

                txtMADepartureFlight.Text = dt.Rows(0)("depflightcode").ToString()
                txtMADepartureFlightCode.Text = dt.Rows(0)("depflight_tranid").ToString()
                txtMADepartureTime.Text = dt.Rows(0)("depflighttime").ToString()
                txtMADepairportdrop.Text = dt.Rows(0)("depairportborderName").ToString()
                txtMADepairportdropcode.Text = dt.Rows(0)("depairportbordercode").ToString()

            End If
        End If

    End Sub
    ''' <summary>
    ''' LoadRoomAdultChild
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRoomAdultChild()

        ' Above part commented asper Mr. Arun request on 04/06/2017. No need to restrict adult and child based on other booking.
        '*** Danny 26/08/2019 Dynamic Rooms load
        Dim intNoAdults As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=54")
        Dim intNoChilds As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=55")
        FillSpecifiedAdultChild(intNoAdults, intNoChilds)


        Dim intRoomNos As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=53")
        FillSpecifiedRoom(intRoomNos)

    End Sub
    ''' <summary>
    ''' FillSpecifiedRoom
    ''' </summary>
    ''' <param name="NoOfRoom"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedRoom(ByVal NoOfRoom As String)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom, NoOfRoom)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom_Dynamic, NoOfRoom)
    End Sub
    ''' <summary>
    ''' GetRoomString
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRoomString(ByVal strRoomString As String) As String
        strRoomString = ""
        If dlNofoRooms.Items.Count > 0 Then
            Dim i As Integer = 1
            For Each dlItem1 As DataListItem In dlNofoRooms.Items
                If i <> 1 Then
                    strRoomString = strRoomString + ";"
                End If
                Dim ddlDynRoomAdult As DropDownList = CType(dlItem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlItem1.FindControl("ddlDynRoomChild"), DropDownList)
                If ddlDynRoomAdult.SelectedIndex = 0 Then
                    strRoomString = "Please select Room" + i.ToString() + " Adult."
                    Return strRoomString
                End If
                strRoomString = strRoomString + i.ToString
                strRoomString = strRoomString + "," + ddlDynRoomAdult.Text
                If ddlDynRoomChild.SelectedIndex > 0 Then
                    strRoomString = strRoomString + "," + ddlDynRoomChild.Text + ","
                    Dim ddlDynChildAges As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)
                    Dim j As Integer = 1
                    For Each dlItem2 As DataListItem In ddlDynChildAges.Items
                        Dim ddlDynRoomAdult1 As System.Web.UI.WebControls.TextBox = CType(dlItem2.FindControl("txtRoom1Child1"), System.Web.UI.WebControls.TextBox)
                        If Val(ddlDynRoomAdult1.Text) = 0 Then
                            strRoomString = "Please select " + " Child" + j.ToString + " Age in Room" + i.ToString()
                            Return strRoomString
                        End If
                        strRoomString = strRoomString + "|" + ddlDynRoomAdult1.Text
                        j = j + 1
                    Next
                Else
                    strRoomString = strRoomString + ",0,0"
                End If
                i = i + 1
            Next

        End If

        'strRoomString = "1," & ddlRoom1Adult.SelectedValue
        'Dim strRoom1Child = ddlRoom1Child.SelectedValue
        'If strRoom1Child = "0" Then
        '    strRoomString = strRoomString & ",0,0"
        'ElseIf strRoom1Child = "1" Then
        '    strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text)
        'ElseIf strRoom1Child = "2" Then
        '    strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text)
        'End If
        Return strRoomString.Replace(",|", ",")
    End Function
    Function GetChildAges(ByVal dtAdultChilds As DataTable, ByVal strRomeNo As String) As DataTable
        Dim dldlChildAges As DataList = CType(dlNofoRooms.Items(strRomeNo).FindControl("dlChildAges"), DataList)
        dtAdultChilds.Rows(strRomeNo)("colChildAges") = ""
        For Each ChildAgesitems In dldlChildAges.Items
            Dim ddlDynRoomAdult1 As System.Web.UI.WebControls.TextBox = CType(ChildAgesitems.FindControl("txtRoom1Child1"), System.Web.UI.WebControls.TextBox)
            If dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString().Trim().Length > 0 Then
                dtAdultChilds.Rows(strRomeNo)("colChildAges") = dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString + "|"
            End If
            If ddlDynRoomAdult1.Text.Trim.Length = 0 Then
                ddlDynRoomAdult1.Text = 0
            End If
            dtAdultChilds.Rows(strRomeNo)("colChildAges") = dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString.Trim + ddlDynRoomAdult1.Text
        Next
        Return dtAdultChilds
    End Function
    Public Sub AdultChanges(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dtAdultChilds = New DataTable
        dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)
        Dim dDLAd As DropDownList = sender
        Dim strRomeNo As String = dDLAd.ClientID.Replace("dlNofoRooms_ctl", "").Replace("_ddlDynRoomAdult", "")
        dtAdultChilds.Rows(Val(strRomeNo))("colAdultSelectNo") = dDLAd.SelectedValue.ToString()

        dtAdultChilds = GetChildAges(dtAdultChilds, strRomeNo)


        ''Dim dDLAd As DropDownList = CType(dlNofoRooms.Items(strRomeNo).FindControl("ddlDynRoomAdult"), DropDownList)
        'If dDLAd.SelectedItem.Text.Trim().Replace("--", "").Length = 0 Then
        '    dDLAd.SelectedIndex = 0
        '    Exit Sub
        'End If
        Session("dtAdultChilds") = dtAdultChilds

    End Sub
    'Public Sub ChildAgeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim dtAdultChilds = New DataTable
    '    dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)
    '    Dim dDLTX As System.Web.UI.WebControls.TextBox = sender
    '    'Dim strRomeNo As String = dDLTX.ClientID.Replace("dlNofoRooms_ctl00_dlChildAges_ctl", "").Replace("_txtRoom1Child1", "")
    '    Dim strRomeNo As String = dDLTX.ClientID.Replace("dlNofoRooms_ctl", "").Replace("_txtRoom1Child1", "")
    '    Dim RowNo As String = Val(strRomeNo.Substring(0, 2)).ToString
    '    Dim ColNo As String = Val(strRomeNo.Substring(2).Replace("_dlChildAges_ctl", "")).ToString

    '    Dim colAdultSelectNo As String() = dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString.Split("|")

    '    If colAdultSelectNo.Length = ColNo Then
    '        If dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString.Trim().Length > 0 Then
    '            dtAdultChilds.Rows(Val(RowNo))("colChildAges") = dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString + "|"
    '        End If
    '        dtAdultChilds.Rows(Val(RowNo))("colChildAges") = dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString + dDLTX.Text
    '    Else
    '        dtAdultChilds.Rows(Val(RowNo))("colChildAges") = ""
    '        For i As Integer = 0 To colAdultSelectNo.Length - 1
    '            If dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString.Trim().Length > 0 Then
    '                dtAdultChilds.Rows(Val(RowNo))("colChildAges") = dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString + "|"
    '            End If
    '            If i = ColNo Then
    '                dtAdultChilds.Rows(Val(RowNo))("colChildAges") = dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString + dDLTX.Text
    '            Else
    '                dtAdultChilds.Rows(Val(RowNo))("colChildAges") = dtAdultChilds.Rows(Val(RowNo))("colChildAges").ToString + colAdultSelectNo(i).ToString
    '            End If

    '        Next
    '    End If

    '    'dtAdultChilds.Rows(Val(strRomeNo))("colAdultSelectNo") = dDLTX.Text.ToString()

    '    ''Dim dDLAd As DropDownList = CType(dlNofoRooms.Items(strRomeNo).FindControl("ddlDynRoomAdult"), DropDownList)
    '    'If dDLAd.SelectedItem.Text.Trim().Replace("--", "").Length = 0 Then
    '    '    dDLAd.SelectedIndex = 0
    '    '    Exit Sub
    '    'End If
    '    Session("dtAdultChilds") = dtAdultChilds
    'End Sub
    Public Sub ChildAgeTxtCreate(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim dtAdultChilds = New DataTable
        dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)

        Dim dtChildAge = New DataTable
        dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
        dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
        dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
        Dim dDLCh As DropDownList = sender
        Dim strRomeNo As String = dDLCh.ClientID.Replace("dlNofoRooms_ctl", "").Replace("_ddlDynRoomChild", "")
        Dim dDLAd As DropDownList = CType(dlNofoRooms.Items(strRomeNo).FindControl("ddlDynRoomAdult"), DropDownList)


        dtAdultChilds = GetChildAges(dtAdultChilds, strRomeNo)

        'Dim dldlChildAges As DataList = CType(dlNofoRooms.Items(strRomeNo).FindControl("dlChildAges"), DataList)

        'For Each ChildAgesitems In dldlChildAges.Items
        '    Dim ddlDynRoomAdult1 As System.Web.UI.WebControls.TextBox = CType(ChildAgesitems.FindControl("txtRoom1Child1"), System.Web.UI.WebControls.TextBox)
        '    If dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString().Trim().Length > 0 Then
        '        dtAdultChilds.Rows(strRomeNo)("colChildAges") = dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString + "|"
        '    End If
        '    dtAdultChilds.Rows(strRomeNo)("colChildAges") = dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString.Trim + ddlDynRoomAdult1.Text
        'Next

        If dDLAd.SelectedItem.Text.Trim().Replace("--", "").Length = 0 Then
            dDLCh.SelectedIndex = 0
            Exit Sub
        End If
        'Dim s As String = dDLCh.SelectedValue()
        dtAdultChilds.Rows(strRomeNo)("colChildSelectNo") = dDLCh.SelectedValue()

        Dim colAdultSelectNo As String() = dtAdultChilds.Rows(Val(strRomeNo))("colChildAges").ToString.Split("|")
        For i As Integer = 0 To Val(sender.SelectedValue()) - 1
            Dim row As DataRow = dtChildAge.NewRow()
            If i = 0 Then
                row("colChildAgeLbl") = "Room " + (Val(strRomeNo) + 1).ToString + " Child Age"
            Else
                row("colChildAgeLbl") = (i + 1).ToString
            End If
            If dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString().Trim.Length = 0 Then
                row("colChildAge") = ""
            Else
                If i < colAdultSelectNo.Length Then
                    row("colChildAge") = colAdultSelectNo(i)
                Else
                    row("colChildAge") = ""
                End If

            End If
            row("colCHNo") = "CH" + (i + 1).ToString
            dtChildAge.Rows.Add(row)
        Next
        Dim dlItem1 As DataListItem = dlNofoRooms.Items(Val(strRomeNo))
        Dim ddlDynRoomAdult As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)

        'Session("dtChildAge") = dtChildAge
        ddlDynRoomAdult.DataSource = dtChildAge
        ddlDynRoomAdult.DataBind()

        Session("dtAdultChilds") = dtAdultChilds
        'Dim ss As String = dlNofoRooms.Items(0).ToString()
        'Dim dt As DataList = CType(e.FindControl("datalist2"), DataList)
    End Sub
    '*** Danny 26/08/2018
    Protected Sub ddlRoom_Dynamic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRoom_Dynamic.SelectedIndexChanged

        Dim dtAdultChilds = New DataTable
        dtAdultChilds.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildAges", GetType(String)))
        Dim a As Integer = 1
        For Each ChildAgesitems In dlNofoRooms.Items
            Dim row As DataRow = dtAdultChilds.NewRow()
            Dim ddlDynRoomAdult As DropDownList = CType(ChildAgesitems.FindControl("ddlDynRoomAdult"), DropDownList)
            Dim ddlDynRoomChild As DropDownList = CType(ChildAgesitems.FindControl("ddlDynRoomChild"), DropDownList)

            row("colAdultLbl") = (a).ToString
            row("colChildLbl") = (a).ToString
            row("colAdultSelectNo") = ddlDynRoomAdult.Text
            row("colChildSelectNo") = ddlDynRoomChild.Text
            dtAdultChilds.Rows.Add(row)
            dtAdultChilds = GetChildAges(dtAdultChilds, (a - 1).ToString)

        Next
        Session("dtAdultChilds") = dtAdultChilds

        dlNofoRooms.DataSource = dtAdultChilds
        dlNofoRooms.DataBind()
        Dim intNoAdults As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=54")
        Dim intNoChilds As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=55")


        If Session("dtAdultChilds") Is Nothing Then
            If Val(ddlRoom_Dynamic.Text) = 0 Then
                Exit Sub
            End If
            For i As Integer = 0 To Val(ddlRoom_Dynamic.Text) - 1
                Dim row As DataRow = dtAdultChilds.NewRow()
                row("colAdultLbl") = (i + 1).ToString
                row("colChildLbl") = (i + 1).ToString
                row("colAdultSelectNo") = ""
                row("colChildSelectNo") = ""

                dtAdultChilds.Rows.Add(row)
            Next
            Session("dtAdultChilds") = dtAdultChilds
            dlNofoRooms.DataSource = dtAdultChilds
            dlNofoRooms.DataBind()
        Else
            dtAdultChilds = Session("dtAdultChilds")
            Dim dtAdultChilds_1 = New DataTable
            dtAdultChilds_1.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colChildAges", GetType(String)))

            For i As Integer = 0 To Val(ddlRoom_Dynamic.Text) - 1
                Dim row As DataRow = dtAdultChilds_1.NewRow()
                If (i + 1) > dtAdultChilds.Rows.Count Then
                    row("colAdultLbl") = (i + 1).ToString
                    row("colChildLbl") = (i + 1).ToString
                    row("colAdultSelectNo") = "2" '*** Danny 04/09/2018
                    row("colChildSelectNo") = ""
                    row("colChildAges") = ""
                Else
                    row("colAdultLbl") = dtAdultChilds.Rows(i)("colAdultLbl").ToString
                    row("colChildLbl") = dtAdultChilds.Rows(i)("colChildLbl").ToString
                    row("colAdultSelectNo") = dtAdultChilds.Rows(i)("colAdultSelectNo").ToString
                    row("colChildSelectNo") = dtAdultChilds.Rows(i)("colChildSelectNo").ToString
                    row("colChildAges") = dtAdultChilds.Rows(i)("colChildAges").ToString
                End If

                dtAdultChilds_1.Rows.Add(row)
            Next
            'Session("dtAdultChilds") = dtAdultChilds_1

            dlNofoRooms.DataSource = dtAdultChilds_1
            dlNofoRooms.DataBind()
        End If
        If dlNofoRooms.Items.Count > 0 Then
            Dim i As Integer = 0
            'Dim a As Integer = 0
            For Each dlItem1 As DataListItem In dlNofoRooms.Items
                Dim ddlDynRoomAdult As DropDownList = CType(dlItem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlItem1.FindControl("ddlDynRoomChild"), DropDownList)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomAdult, intNoAdults)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomChild, intNoChilds)

                'If dtAdultChilds.Rows(i)("colAdultSelectNo").ToString.Trim.Length > 0 Then
                If i <= dtAdultChilds.Rows.Count - 1 Then
                    ddlDynRoomAdult.Text = dtAdultChilds.Rows(i)("colAdultSelectNo").ToString
                    ddlDynRoomChild.Text = dtAdultChilds.Rows(i)("colChildSelectNo").ToString

                    If Val(ddlDynRoomChild.Text) > 0 Then
                        'If dtAdultChilds.Rows(i)("colChildAges").ToString.Trim.Length > 0 Then
                        Dim strroomstring As String() = dtAdultChilds.Rows(i)("colChildAges").ToString.Split("|")
                        Dim dtChildAge = New DataTable
                        dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
                        dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
                        dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
                        Dim ddlDynChildAges As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)

                        For j = 0 To strroomstring.Length - 1
                            Dim row2 As DataRow = dtChildAge.NewRow()
                            If strroomstring(j).ToString.Trim.Length > 0 Then
                                If j = 0 Then
                                    row2("colChildAgeLbl") = "Room " + strroomstring(j).ToString + " Child Age"
                                Else
                                    row2("colChildAgeLbl") = (j + 1).ToString
                                End If
                                row2("colChildAge") = IIf(Val(strroomstring(j)) = 0, "", strroomstring(j))
                                row2("colCHNo") = "CH" + (j + 1).ToString
                                dtChildAge.Rows.Add(row2)
                            Else
                                If Val(ddlDynRoomChild.Text.Trim) > 0 Then
                                    If j = 0 Then
                                        row2("colChildAgeLbl") = "Room " + strroomstring(j).ToString + " Child Age"
                                    Else
                                        row2("colChildAgeLbl") = (j + 1).ToString
                                    End If
                                    row2("colChildAge") = IIf(Val(strroomstring(j)) = 0, "", strroomstring(j))
                                    row2("colCHNo") = "CH" + (j + 1).ToString
                                    dtChildAge.Rows.Add(row2)
                                End If
                            End If

                        Next
                        ddlDynChildAges.DataSource = dtChildAge
                        ddlDynChildAges.DataBind()
                        'End If


                        'Dim strroom As String
                        'Dim strchildage As String
                        'If strroomstring.Length > 0 Then





                        'For Each dlRoomtem1 As DataListItem In dlNofoRooms.Items
                        'strroom = strroomstring(a).ToString()
                        'Dim ddlDynRoomAdult As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomAdult"), DropDownList)
                        'Dim ddlDynRoomChild As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomChild"), DropDownList)


                        'ddlDynRoomAdult.SelectedIndex = strroom(1)
                        'ddlDynRoomChild.SelectedIndex = strroom(2)

                        'strchildage = strroom.ToString()



                        'a = a + 1
                        'Next
                        'End If
                    End If
                Else '*** Danny 04/09/2018>>>>>>>>>>>>>>>>>>>>>
                    ddlDynRoomAdult.Text = "2"
                End If
                '*** Danny 04/09/2018<<<<<<<<<<<<<<<<<<<<<<<<
                'End If
                i = i + 1
            Next

        End If
        Session("dtAdultChilds") = dlNofoRooms.DataSource
        loder16.Attributes.Add("style", "display:none")
        If ddlRoom_Dynamic.SelectedValue > 5 Then
            ddlAvailability.SelectedValue = "1"
        Else
            ddlAvailability.SelectedValue = "2"
        End If
        'loder16.Visible = False
    End Sub
    ''' <summary>
    ''' FillSpecifiedChildAges
    ''' </summary>
    ''' <param name="childages"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedChildAges(ByVal childages As String)

        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild8, childages)

        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild8, childages)

        ' objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild8, childages)

        ' objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlOthChild8, childages)

    End Sub
    ''' <summary>
    ''' FillSpecifiedAdultChild
    ''' </summary>
    ''' <param name="adults"></param>
    ''' <param name="child"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)
        '*** Danny 26/082018
        objclsUtilities.FillDropDownListBasedOnNumber(ddlPreHotelAdult, "1000")
        objclsUtilities.FillDropDownListBasedOnNumber(ddlPreHotelChild, child)

        'objclsUtilities.FillDropDownListBasedOnNumber(ddlAdult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlChildren, child)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfChild, child)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlMAAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlMAChild, child)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourChildren, child)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlOthAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlOthChild, child)


    End Sub
    ''' <summary>
    ''' btnOthsearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnOthsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOthsearch.Click
        Try

            Dim objBLLOtherSearch As New BLLOtherSearch
            Dim strSearchCriteria As String = ""
            Dim strFromDate As String = txtothFromDate.Text
            Dim strToDate As String = txtothToDate.Text

            Dim strAdult As String = ddlOthAdult.SelectedValue
            Dim strChildren As String = ddlOthChild.SelectedValue

            Dim strChild1 As String = txtOthChild1.Text
            Dim strChild2 As String = txtOthChild2.Text
            Dim strChild3 As String = txtOthChild3.Text
            Dim strChild4 As String = txtOthChild4.Text
            Dim strChild5 As String = txtOthChild5.Text
            Dim strChild6 As String = txtOthChild6.Text
            Dim strChild7 As String = txtOthChild7.Text
            Dim strChild8 As String = txtOthChild8.Text

            Dim strSourceCountry As String = txtothSourceCountry.Text
            Dim strSourceCountryCode As String = txtothSourceCountryCode.Text

            Dim strCustomer As String = txtothCustomer.Text
            Dim strCustomerCode As String = txtothCustomercode.Text



            If strFromDate <> "" Then
                objBLLOtherSearch.FromDate = txtothFromDate.Text
                strSearchCriteria = "FromDate:" & txtothFromDate.Text
            End If

            If strToDate <> "" Then
                objBLLOtherSearch.ToDate = txtothToDate.Text
                strSearchCriteria = strSearchCriteria & "|| ToDate:" & txtothToDate.Text
            End If

            If txtothgroupcode.Text <> "" Then
                objBLLOtherSearch.SelectGroupCode = txtothgroupcode.Text
                objBLLOtherSearch.SelectGroup = txtothgroup.Text
                strSearchCriteria = strSearchCriteria & "|| Group:" & txtothgroup.Text
            End If




            If txtothSourceCountryCode.Text <> "" Then
                objBLLOtherSearch.SourceCountryCode = txtothSourceCountryCode.Text
                objBLLOtherSearch.SourceCountry = txtothSourceCountry.Text
                strSearchCriteria = strSearchCriteria & "|| SourceCountry:" & txtothSourceCountry.Text
            End If


            If txtothCustomercode.Text <> "" Then
                objBLLOtherSearch.CustomerCode = txtothCustomercode.Text
                objBLLOtherSearch.Customer = txtothCustomer.Text
                strSearchCriteria = strSearchCriteria & "|| Agent:" & txtothCustomer.Text
            End If



            If strAdult <> "" Then
                objBLLOtherSearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "|| Adult:" & strAdult
            End If




            If strChildren <> "" Then
                objBLLOtherSearch.Children = strChildren
                strSearchCriteria = strSearchCriteria & "|| Children:" & strChildren
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
                strSearchCriteria = strSearchCriteria & "|| ChildAgeString:" & objBLLOtherSearch.ChildAgeString
            End If


            If chkothoverride.Checked = True Then
                objBLLOtherSearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "|| OverridePrice:Yes"
            Else
                objBLLOtherSearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "|| OverridePrice:No"
            End If

            objBLLOtherSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "|| LoginType:" & objBLLOtherSearch.LoginType
            objBLLOtherSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLOtherSearch.CustomerCode, Session("sAgentCode")) 'Session("sAgentCode")
            strSearchCriteria = strSearchCriteria & "|| AgentCode:" & objBLLOtherSearch.AgentCode
            objBLLOtherSearch.WebuserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "|| WebuserName:" & objBLLOtherSearch.WebuserName

            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                '  Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Home Page", "Other Service Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            Session("sobjBLLOtherSearch") = objBLLOtherSearch
            Response.Redirect("OtherSearch.aspx", False)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnOthsearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnMyBooking_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
        Try

            If Session("sRequestId") Is Nothing Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "You have no booking.")
            Else
                Dim objBLLCommonFuntions As New BLLCommonFuntions
                Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                If dt.Rows.Count > 0 Then
                    Response.Redirect("MoreServices.aspx", False)
                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, "You have no booking.")
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' ShowMyBooking
    ''' </summary>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' GetRoom2String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''Private Function GetRoom2String(ByVal strRoomString As String) As String
    ''    strRoomString = strRoomString & ";2," & ddlRoom2Adult.SelectedValue
    ''    Dim strRoom2Child = ddlRoom2Child.SelectedValue
    ''    If strRoom2Child = "0" Then
    ''        strRoomString = strRoomString & ",0,0"
    ''    ElseIf strRoom2Child = "1" Then
    ''        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text)
    ''    ElseIf strRoom2Child = "2" Then
    ''        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text)
    ''    End If
    ''    Return strRoomString
    ''End Function

    ''' <summary>
    ''' ClearallBookingSessions
    ''' </summary>
    ''' <remarks></remarks>
    Sub ClearallBookingSessions()
        If Not Session("sRequestId") Is Nothing Then
            Session("sRequestId") = Nothing
        End If
        If Not Session("sEditRequestId") Is Nothing Then
            Session("sEditRequestId") = Nothing
        End If
        If Not Session("sdtPriceBreakup") Is Nothing Then
            Session("sdtPriceBreakup") = Nothing
        End If
        If Not Session("showservices") Is Nothing Then
            Session("showservices") = Nothing
        End If
        If Not Session("ShowGuests") Is Nothing Then
            Session("ShowGuests") = Nothing
        End If
        If Not Session("ShowGuestsDep") Is Nothing Then
            Session("ShowGuestsDep") = Nothing
        End If
        If Not Session("sobjBLLHotelSearch") Is Nothing Then
            Session("sobjBLLHotelSearch") = Nothing
        End If
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            Session("sobjBLLHotelSearchActive") = Nothing
        End If
        If Not Session("sobjBLLOtherSearchActive") Is Nothing Then
            Session("sobjBLLOtherSearchActive") = Nothing
        End If
        If Not Session("sobjBLLMASearchActive") Is Nothing Then
            Session("sobjBLLMASearchActive") = Nothing
        End If
        If Not Session("sobjBLLTourSearchActive") Is Nothing Then
            Session("sobjBLLTourSearchActive") = Nothing
        End If
        If Not Session("sobjBLLTransferSearchActive") Is Nothing Then
            Session("sobjBLLTransferSearchActive") = Nothing
        End If
        If Not Session("sdsSpecialEvents") Is Nothing Then
            Session("sdsSpecialEvents") = Nothing
        End If
        If Not Session("sdtSelectedSpclEvent") Is Nothing Then
            Session("sdtSelectedSpclEvent") = Nothing
        End If
        If Not Session("slbSpecialEvents") Is Nothing Then
            Session("slbSpecialEvents") = Nothing
        End If
        If Not Session("State") Is Nothing Then
            Session("State") = Nothing
        End If
        If Not Session("sdsPriceBreakup") Is Nothing Then
            Session("sdsPriceBreakup") = Nothing
        End If
        If Not Session("sdsPriceBreakupTemp") Is Nothing Then
            Session("sdsPriceBreakupTemp") = Nothing
        End If
        If Not Session("sDSSearchResults") Is Nothing Then
            Session("sDSSearchResults") = Nothing
        End If
        If Not Session("sMailBoxPageIndex") Is Nothing Then
            Session("sMailBoxPageIndex") = Nothing
        End If
        If Not Session("sdtRoomType") Is Nothing Then
            Session("sdtRoomType") = Nothing
        End If
        If Not Session("sobjBLLTourSearch") Is Nothing Then
            Session("sobjBLLTourSearch") = Nothing
        End If
        If Not Session("sobjBLLTourSearchActive") Is Nothing Then
            Session("sobjBLLTourSearchActive") = Nothing
        End If
        If Not Session("sDSTourSearchResults") Is Nothing Then
            Session("sDSTourSearchResults") = Nothing
        End If
        If Not Session("sTourPageIndex") Is Nothing Then
            Session("sTourPageIndex") = Nothing
        End If
        If Not Session("sdtTourPriceBreakup") Is Nothing Then
            Session("sdtTourPriceBreakup") = Nothing
        End If
        If Not Session("slbTourTotalSaleValue") Is Nothing Then
            Session("slbTourTotalSaleValue") = Nothing
        End If
        If Not Session("selectedtourdatatable") Is Nothing Then
            Session("selectedtourdatatable") = Nothing
        End If
        If Not Session("sTourLineNo") Is Nothing Then
            Session("sTourLineNo") = Nothing
        End If
        If Not Session("sobjBLLTransferSearch") Is Nothing Then
            Session("sobjBLLTransferSearch") = Nothing
        End If
        If Not Session("sobjBLLTransferSearchActive") Is Nothing Then
            Session("sobjBLLTransferSearchActive") = Nothing
        End If
        If Not Session("sDSTrfSearchResults") Is Nothing Then
            Session("sDSTrfSearchResults") = Nothing
        End If
        If Not Session("sTrfMailBoxPageIndex") Is Nothing Then
            Session("sTrfMailBoxPageIndex") = Nothing
        End If
        If Not Session("tlineno") Is Nothing Then
            Session("tlineno") = Nothing
        End If
        If Not Session("sSender") Is Nothing Then
            Session("sSender") = Nothing
        End If
        If Not Session("sEventArgs") Is Nothing Then
            Session("sEventArgs") = Nothing
        End If
        If Not Session("sobjBLLMASearch") Is Nothing Then
            Session("sobjBLLMASearch") = Nothing
        End If
        If Not Session("sobjBLLMASearchActive") Is Nothing Then
            Session("sobjBLLMASearchActive") = Nothing
        End If
        If Not Session("sDSMASearchResults") Is Nothing Then
            Session("sDSMASearchResults") = Nothing
        End If
        If Not Session("sMAMailBoxPageIndex") Is Nothing Then
            Session("sMAMailBoxPageIndex") = Nothing
        End If
        If Not Session("sMALineNo") Is Nothing Then
            Session("sMALineNo") = Nothing
        End If
        If Not Session("sMAMailBoxPageIndex") Is Nothing Then
            Session("sMAMailBoxPageIndex") = Nothing
        End If
        If Not Session("slbMATotalSaleValue") Is Nothing Then
            Session("slbMATotalSaleValue") = Nothing
        End If
        If Not Session("sdtRecentlyViewedHotel") Is Nothing Then
            Session("sdtRecentlyViewedHotel") = Nothing
        End If
        If Not Session("ShowGuests") Is Nothing Then
            Session("ShowGuests") = Nothing
        End If
        If Not Session("ShowGuestsDep") Is Nothing Then
            Session("ShowGuestsDep") = Nothing
        End If
        If Not Session("showservices") Is Nothing Then
            Session("showservices") = Nothing
        End If
        If Not Session("sdtPriceBreakup") Is Nothing Then
            Session("sdtPriceBreakup") = Nothing
        End If
        If Not Session("sobjBLLOtherSearchActive") Is Nothing Then
            Session("sobjBLLOtherSearchActive") = Nothing
        End If
        If Not Session("sobjBLLOtherSearch") Is Nothing Then
            Session("sobjBLLOtherSearch") = Nothing
        End If
        If Not Session("sDSOtherSearchResults") Is Nothing Then
            Session("sDSOtherSearchResults") = Nothing
        End If
        If Not Session("sOtherPageIndex") Is Nothing Then
            Session("sOtherPageIndex") = Nothing
        End If
        If Not Session("slbOtherTotalSaleValue") Is Nothing Then
            Session("slbOtherTotalSaleValue") = Nothing
        End If
        If Not Session("selectedotherdatatable") Is Nothing Then
            Session("selectedotherdatatable") = Nothing
        End If
        If Not Session("olineno") Is Nothing Then
            Session("olineno") = Nothing
        End If
        If Not Session("VisaDetailsDT") Is Nothing Then
            Session("VisaDetailsDT") = Nothing
        End If
        If Not Session("vlineno") Is Nothing Then
            Session("vlineno") = Nothing
        End If
        If Not Session("sobjBLLMyAccount") Is Nothing Then
            Session("sobjBLLMyAccount") = Nothing
        End If

    End Sub
    ''' <summary>
    ''' LoadFooter
    ''' </summary>
    ''' <remarks></remarks>
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
            objclsUtilities.WriteErrorLog("Home.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    ''' <summary>
    ''' GetNumberOfStars
    ''' </summary>
    ''' <param name="strNoOfStars"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNumberOfStars(ByVal strNoOfStars As String) As String
        Dim strOffersHTML As New StringBuilder
        If strNoOfStars = "1" Then
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
        ElseIf strNoOfStars = "2" Then
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
        ElseIf strNoOfStars = "3" Then
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
        ElseIf strNoOfStars = "4" Then
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
        ElseIf strNoOfStars = "5" Then
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
        Else
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
            strOffersHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
        End If
        Return strOffersHTML.ToString
    End Function
    ''' <summary>
    ''' btnMyAccount_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx", False)
    End Sub
    ''' <summary>
    ''' MyThreadParameters
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MyThreadParameters
        Public Property LoginType As String
        Public Property UserName As String
        Public Property AgentCode As String
        Public Property CountryCode As String
    End Class
    ''' <summary>
    ''' BindOfferDetails
    ''' </summary>
    ''' <param name="param"></param>
    ''' <remarks></remarks>
    Private Sub BindOfferDetails(ByVal param As MyThreadParameters)
        'ByVal strLoginType As String, ByVal strUserName As String, ByVal strAgentCode As String, ByVal strCountryCode As String
        '' OFFERS ==========
        Dim dtOffers As DataTable
        Dim strOffersHTML As New StringBuilder
        Dim strHotelCode As String = ""
        ' dtOffers = objBLLHome.GetHottestOffersAndPopularDeal(HttpContext.Current.Session("sLoginType"), HttpContext.Current.Session("GlobalUserName"), HttpContext.Current.Session("sAgentCode"), HttpContext.Current.Session("sCountryCode"))
        dtOffers = objBLLHome.GetHottestOffersAndPopularDeal(param.LoginType, param.UserName, param.AgentCode, param.CountryCode)
        If dtOffers.Rows.Count > 0 Then

            Dim dvHottestOffers As DataView = New DataView(dtOffers)
            dvHottestOffers.RowFilter = "resulttype='HottestOffers'"

            If dvHottestOffers.Count = 0 Then
                strOffersHTML.Append(GetOfferString("img/Offer1_626_464.jpg"))
                strOffersHTML.Append(GetOfferString("img/Offer2_626_464.jpg"))
                strOffersHTML.Append(GetOfferString("img/Offer3_626_464.jpg"))
                strOffersHTML.Append(GetOfferString("img/Offer4_626_464.jpg"))
            ElseIf dvHottestOffers.Count = 1 Then
                For i As Integer = 0 To dvHottestOffers.Count - 1
                    strHotelCode = dvHottestOffers.Item(i)("partycode").ToString
                    strOffersHTML.Append(" <div class='offer-slider-i'>")
                    strOffersHTML.Append("<a  title='BOOK NOW'  class='offer-slider-img' href='HotelSearch.aspx?HCode=" + strHotelCode + "'><img alt='BOOK NOW' src='ImageDisplay.aspx?FileName=" & dvHottestOffers.Item(i)("HotelImage").ToString & "&Type=5' /><span class='offer-slider-overlay'></span></a>")
                    strOffersHTML.Append(" <div class='offer-slider-txt'>")
                    strOffersHTML.Append(" <div class='offer-slider-link'><a href='#'>" & dvHottestOffers.Item(i)("partyname").ToString & "</a></div>")
                    strOffersHTML.Append(" <div class='offer-slider-l'><div class='offer-slider-location'>Location:   " & dvHottestOffers.Item(i)("sectorname").ToString & "</div>")
                    strOffersHTML.Append(" <nav class='stars'><ul>")
                    strOffersHTML.Append(GetNumberOfStars(dvHottestOffers.Item(i)("noofstars").ToString))
                    strOffersHTML.Append(" </ul>")
                    strOffersHTML.Append(" <div class='clear'></div></nav>  </div>")
                    If hdBookingEngineRateType.Value <> "1" Then
                        strOffersHTML.Append(" <div class='offer-slider-r'  style='margin-top:-5px;'><span  style='padding-bottom:5px;float:left;'>starting from</span></b> <b>" & dvHottestOffers.Item(i)("saleprice").ToString & " " & dvHottestOffers.Item(i)("salecurrency").ToString & "</b> <span>/night</span></div>")
                    End If

                    strOffersHTML.Append("<div class='offer-slider-devider'></div>")
                    strOffersHTML.Append(" <div class='clear'></div>")
                    strOffersHTML.Append(" </div></div>")


                Next
                strOffersHTML.Append(GetOfferString("img/Offer2_626_464.jpg"))
                strOffersHTML.Append(GetOfferString("img/Offer3_626_464.jpg"))
                strOffersHTML.Append(GetOfferString("img/Offer4_626_464.jpg"))
            ElseIf dvHottestOffers.Count = 2 Then
                For i As Integer = 0 To dvHottestOffers.Count - 1
                    strHotelCode = dvHottestOffers.Item(i)("partycode").ToString
                    strOffersHTML.Append(" <div class='offer-slider-i'>")
                    strOffersHTML.Append("<a  title='BOOK NOW' class='offer-slider-img' href='HotelSearch.aspx?HCode=" + strHotelCode + "'><img alt='BOOK NOW' src='ImageDisplay.aspx?FileName=" & dvHottestOffers.Item(i)("HotelImage").ToString & "&Type=5' /><span class='offer-slider-overlay'></span></a>")
                    strOffersHTML.Append(" <div class='offer-slider-txt'>")
                    strOffersHTML.Append(" <div class='offer-slider-link'><a href='#'>" & dvHottestOffers.Item(i)("partyname").ToString & "</a></div>")
                    strOffersHTML.Append(" <div class='offer-slider-l'><div class='offer-slider-location'>Location:   " & dvHottestOffers.Item(i)("sectorname").ToString & "</div>")
                    strOffersHTML.Append(" <nav class='stars'><ul>")
                    strOffersHTML.Append(GetNumberOfStars(dvHottestOffers.Item(i)("noofstars").ToString))
                    strOffersHTML.Append(" </ul>")
                    strOffersHTML.Append(" <div class='clear'></div></nav>  </div>")
                    If hdBookingEngineRateType.Value <> "1" Then
                        strOffersHTML.Append(" <div class='offer-slider-r'  style='margin-top:-5px;'><span  style='padding-bottom:5px;float:left;'>starting from</span></b><b>" & dvHottestOffers.Item(i)("saleprice").ToString & " " & dvHottestOffers.Item(i)("salecurrency").ToString & "</b> <span>/night</span></div>")
                    End If

                    strOffersHTML.Append("<div class='offer-slider-devider'></div>")
                    strOffersHTML.Append(" <div class='clear'></div>")
                    strOffersHTML.Append(" </div></div>")


                Next
                strOffersHTML.Append(GetOfferString("img/Offer3_626_464.jpg"))
                strOffersHTML.Append(GetOfferString("img/Offer4_626_464.jpg"))


            ElseIf dvHottestOffers.Count = 3 Then
                For i As Integer = 0 To dvHottestOffers.Count - 1
                    strHotelCode = dvHottestOffers.Item(i)("partycode").ToString
                    strOffersHTML.Append(" <div class='offer-slider-i'>")
                    strOffersHTML.Append("<a  title='BOOK NOW' class='offer-slider-img' href='HotelSearch.aspx?HCode=" + strHotelCode + "'><img alt='BOOK NOW' src='ImageDisplay.aspx?FileName=" & dvHottestOffers.Item(i)("HotelImage").ToString & "&Type=5' /><span class='offer-slider-overlay'></span></a>")
                    strOffersHTML.Append(" <div class='offer-slider-txt'>")
                    strOffersHTML.Append(" <div class='offer-slider-link'><a href='#'>" & dvHottestOffers.Item(i)("partyname").ToString & "</a></div>")
                    strOffersHTML.Append(" <div class='offer-slider-l'><div class='offer-slider-location'>Location:   " & dvHottestOffers.Item(i)("sectorname").ToString & "</div>")
                    strOffersHTML.Append(" <nav class='stars'><ul>")
                    strOffersHTML.Append(GetNumberOfStars(dvHottestOffers.Item(i)("noofstars").ToString))
                    strOffersHTML.Append(" </ul>")
                    strOffersHTML.Append(" <div class='clear'></div></nav>  </div>")
                    If hdBookingEngineRateType.Value <> "1" Then
                        strOffersHTML.Append(" <div class='offer-slider-r' style='margin-top:-5px;'><span  style='padding-bottom:5px;float:left;'>starting from</span></b></b> <b>" & dvHottestOffers.Item(i)("saleprice").ToString & " " & dvHottestOffers.Item(i)("salecurrency").ToString & "</b> <span>/night</span></div>")
                    End If

                    strOffersHTML.Append("<div class='offer-slider-devider'></div>")
                    strOffersHTML.Append(" <div class='clear'></div>")
                    strOffersHTML.Append(" </div></div>")
                    ' strOffersHTML.Append("  <a href='#' class='last-order-btn fly-in'>Book now</a> </div> </div>")
                Next
                strOffersHTML.Append(GetOfferString("img/Offer4_626_464.jpg"))


            Else
                For i As Integer = 0 To dvHottestOffers.Count - 1
                    strHotelCode = dvHottestOffers.Item(i)("partycode").ToString
                    strOffersHTML.Append(" <div class='offer-slider-i'>")
                    strOffersHTML.Append("<a  title='BOOK NOW' class='offer-slider-img' href='HotelSearch.aspx?HCode=" + strHotelCode + "'><img alt='BOOK NOW' src='ImageDisplay.aspx?FileName=" & dvHottestOffers.Item(i)("HotelImage").ToString & "&Type=5' /><span class='offer-slider-overlay'></span></a>")
                    strOffersHTML.Append(" <div class='offer-slider-txt'>")
                    strOffersHTML.Append(" <div class='offer-slider-link'><a href='#'>" & dvHottestOffers.Item(i)("partyname").ToString & "</a></div>")
                    strOffersHTML.Append(" <div class='offer-slider-l'><div class='offer-slider-location'>Location:   " & dvHottestOffers.Item(i)("sectorname").ToString & "</div>")
                    strOffersHTML.Append(" <nav class='stars'><ul>")
                    strOffersHTML.Append(GetNumberOfStars(dvHottestOffers.Item(i)("noofstars").ToString))
                    strOffersHTML.Append(" </ul>")
                    strOffersHTML.Append(" <div class='clear'></div></nav>  </div>")
                    If hdBookingEngineRateType.Value <> "1" Then
                        strOffersHTML.Append(" <div class='offer-slider-r' style='margin-top:-5px;'><span  style='padding-bottom:5px;float:left;'>starting from</span></b></b> <b>" & dvHottestOffers.Item(i)("saleprice").ToString & " " & dvHottestOffers.Item(i)("salecurrency").ToString & "</b> <span>/night</span></div>")
                    End If

                    strOffersHTML.Append("<div class='offer-slider-devider'></div>")
                    strOffersHTML.Append(" <div class='clear'></div>")
                    strOffersHTML.Append(" </div></div>")
                Next
            End If

        Else
            strOffersHTML.Append(GetOfferString("img/Offer1_626_464.jpg"))
            strOffersHTML.Append(GetOfferString("img/Offer2_626_464.jpg"))
            strOffersHTML.Append(GetOfferString("img/Offer3_626_464.jpg"))
            strOffersHTML.Append(GetOfferString("img/Offer4_626_464.jpg"))


        End If
        offers.InnerHtml = strOffersHTML.ToString
        Session("strOffersHTML") = strOffersHTML.ToString

        '' Load Poppular Deals
        Dim dtPopularDeal As DataTable
        Dim strPopularDealHTML As New StringBuilder

        If dtOffers.Rows.Count > 0 Then

            Dim dvPopularDeal As DataView = New DataView(dtOffers)
            dvPopularDeal.RowFilter = "resulttype='PopularDeal'"
            If dvPopularDeal.Count > 0 Then 'src='ImageDisplay.aspx?FileName=" & dvPopularDeal.Item(0)("HotelImage").ToString & "&Type=5'
                strPopularDealHTML.Append("  <div style='background:url(ImageDisplay.aspx?FileName=" & dvPopularDeal.Item(0)("HotelImage").ToString & "&Type=5) center top no-repeat;' class='last-order fly-in'>")
                'strPopularDealHTML.Append("  <div style='background:url(img/Offer_Single_1920_500.jpg) center top no-repeat;' class='last-order fly-in'>")
                strPopularDealHTML.Append("  <div class='last-order-content'>")
                strPopularDealHTML.Append("  <div class='last-order-a fly-in' style='text-shadow: 2px 2px 4px #000000;'>Popular Deal</div>")
                strPopularDealHTML.Append("  <div class='last-order-b fly-in'  style='text-shadow: 2px 2px 4px #000000;'>" & dvPopularDeal.Item(0)("partyname").ToString & " - " & dvPopularDeal.Item(0)("sectorname").ToString & "</div>")
                'strPopularDealHTML.Append("   <div class='last-order-c fly-in'>" & dvPopularDeal.Item(0)("ValidFrom").ToString & " -  " & dvPopularDeal.Item(0)("ValidTo").ToString & "</div>")
                If hdBookingEngineRateType.Value <> "1" Then
                    strPopularDealHTML.Append("   <div class='last-order-d fly-in' style='text-shadow: 2px 2px 4px #000000;'> " & dvPopularDeal.Item(0)("saleprice").ToString & " " & dvPopularDeal.Item(0)("salecurrency").ToString & " </div>")
                End If

                strPopularDealHTML.Append("  <a href='HotelSearch.aspx?HCode=" + dvPopularDeal.Item(0)("partycode").ToString + "' class='last-order-btn fly-in'  style='text-shadow: 2px 2px 4px #000000;box-shadow: 0px 0px 0px 2px #888888;'>Book now</a> </div> </div>")

            Else

                strPopularDealHTML.Append("  <div style='background:url(img/Offer_Single_1920_500.jpg) center top no-repeat;' class='last-order fly-in'>")
                'strPopularDealHTML.Append("  <div style='background:url(img/Offer_Single_1920_500.jpg) center top no-repeat;' class='last-order fly-in'>")
                strPopularDealHTML.Append("  <div class='last-order-content'>")
                strPopularDealHTML.Append("  <div class='last-order-a fly-in'></div>")
                strPopularDealHTML.Append("  <div class='last-order-b fly-in'></div>")
                'strPopularDealHTML.Append("   <div class='last-order-c fly-in'>" & dtPopularDeal.Rows(0)("ValidFrom").ToString & " -  " & dtPopularDeal.Rows(0)("ValidTo").ToString & "</div>")
                strPopularDealHTML.Append("   <div class='last-order-d fly-in'></div>")
                strPopularDealHTML.Append("   </div> </div>")
            End If

        Else
            dtPopularDeal = objBLLHome.GetOffers("1", param.AgentCode)
            If dtPopularDeal.Rows.Count > 0 Then
                strPopularDealHTML.Append("  <div style='background:url(img/Offer_Single_1920_500.jpg) center top no-repeat;' class='last-order fly-in'>")
                'strPopularDealHTML.Append("  <div style='background:url(img/Offer_Single_1920_500.jpg) center top no-repeat;' class='last-order fly-in'>")
                strPopularDealHTML.Append("  <div class='last-order-content'>")
                strPopularDealHTML.Append("  <div class='last-order-a fly-in'>Popular Deal</div>")
                strPopularDealHTML.Append("  <div class='last-order-b fly-in'></div>")
                'strPopularDealHTML.Append("   <div class='last-order-c fly-in'>" & dtPopularDeal.Rows(0)("ValidFrom").ToString & " -  " & dtPopularDeal.Rows(0)("ValidTo").ToString & "</div>")
                strPopularDealHTML.Append("   <div class='last-order-d fly-in'></div>")
                strPopularDealHTML.Append("  </div> </div>")

            End If
        End If



        '' Load Poppular Deals Logo
        Dim dtPopularDealLogo As DataTable
        dtPopularDealLogo = objBLLHome.GetOffers("2", param.AgentCode)
        If dtPopularDealLogo.Rows.Count > 0 Then
            strPopularDealHTML.Append("   <div class='partners fly-in'>")
            For i As Integer = 0 To dtPopularDealLogo.Rows.Count - 1
                strPopularDealHTML.Append("  <a href='#'><img alt='' src='img/" & dtPopularDealLogo.Rows(i)("OfferImage").ToString & "' /></a>")

            Next

            strPopularDealHTML.Append("  </div>")
        End If
        ltrlPopularDeal.Text = strPopularDealHTML.ToString

        Session("strPopularDealHTML") = strPopularDealHTML.ToString


    End Sub
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If Not Session("strPopularDealHTML") Is Nothing Then
                ltrlPopularDeal.Text = Session("strPopularDealHTML")
                offers.InnerHtml = Session("strOffersHTML")
                Timer1.Enabled = False

                ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterUPUpdate();", True)
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: Timer1_Tick :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    <WebMethod()> _
    Public Shared Function GetSectorDetailsFromPartyCode(ByVal HotelCode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            'strSqlQry = "select s.destname,c.catcode,c.catname,s.destcode + '|' +case when desttype='Area' or desttype='Sector' then 'Sector' else desttype end destcode  from partymast(nolock) p,view_destination_search(nolock) s,catmast c where p.sectorcode=s.destcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
            '  strSqlQry = "select top 1 othtypname,othtypcode from othtypmast(nolock) where othtypcode in (select sectorcode from partymast(nolock) where partycode= '" & HotelCode & "' )"
            strSqlQry = "select * from view_destination_search_new where  partycode= '" & HotelCode & "' order by sortorder asc "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Protected Sub btnPreHotelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreHotelSave.Click
        Try
            Dim objBLLHotelSearch = New BLLHotelSearch

            objBLLHotelSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLHotelSearch.CustomerCode, Session("sAgentCode"))
            objBLLHotelSearch.OBdiv_code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
            objBLLHotelSearch.CustomerCode = txtPreHotelCustomercode.Text
            objBLLHotelSearch.SourceCountryCode = txtPreHotelSourceCountryCode.Text
            objBLLHotelSearch.OBsourcectrycode = objBLLHotelSearch.SourceCountryCode
            objBLLHotelSearch.OBreqoverride = "0"
            objBLLHotelSearch.OBagentref = ""
            objBLLHotelSearch.OBcolumbusref = ""
            objBLLHotelSearch.OBremarks = ""
            objBLLHotelSearch.userlogged = Session("GlobalUserName")

            objBLLHotelSearch.OBrequestid = GetNewOrExistingRequestId()

            objBLLHotelSearch.PreArrangedShifting = "0" 'changed by mohamed on 10/04/2018 *********Abin commented for test
            objBLLHotelSearch.PreArrangedShiftingCode = ""

            objBLLHotelSearch.PreShowHotel = IIf(chkshowhotel.Checked = True, "1", "0")  '' Added shahul 10/11/18

            Dim strHotelLineNo As String = ""
            If ViewState("vPreHotelRLineNo") Is Nothing Then
                strHotelLineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLHotelSearch.OBrequestid, "HOTEL")
            Else
                strHotelLineNo = ViewState("vPreHotelRLineNo")
            End If
            objBLLHotelSearch.PreHotelRLineNo = strHotelLineNo


            Dim strCheckIn As String = txtPreHotelFromDate.Text
            objBLLHotelSearch.PreHotelCheckIn = strCheckIn
            Dim strCheckOut As String = txtPreHotelToDate.Text
            objBLLHotelSearch.PreHotelCheckout = strCheckOut




            Dim strCustomerCode As String = txtPreHotelCustomercode.Text
            Dim strUAELoc As String = txtUAELocationCode.Text

            objBLLHotelSearch.PreHotelPartyCode = txtPreHotelCode.Text
            objBLLHotelSearch.PreHotelSectorCode = txtUAELocationCode.Text



            objBLLHotelSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLHotelSearch.CustomerCode, Session("sAgentCode"))
            objBLLHotelSearch.WebuserName = Session("GlobalUserName")

            Dim strAdult As String = ddlPreHotelAdult.SelectedValue
            Dim strChildren As String = ddlPreHotelChild.SelectedValue
            Dim strChild1 As String = txtPreHotelChild1.Text
            Dim strChild2 As String = txtPreHotelChild2.Text
            Dim strChild3 As String = txtPreHotelChild3.Text
            Dim strChild4 As String = txtPreHotelChild4.Text
            Dim strChild5 As String = txtPreHotelChild5.Text
            Dim strChild6 As String = txtPreHotelChild6.Text
            Dim strChild7 As String = txtPreHotelChild7.Text
            Dim strChild8 As String = txtPreHotelChild8.Text


            If strAdult <> "" Then
                objBLLHotelSearch.PreHotelAdults = strAdult
            End If
            If strChildren <> "" Then
                If strChildren = "" Then
                    strChildren = "0"
                End If
                objBLLHotelSearch.PreHotelChild = strChildren
                If strChildren = "1" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1
                ElseIf strChildren = "2" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7
                ElseIf strChildren = "8" Then
                    objBLLHotelSearch.PreHotelChildages = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
                End If
            End If

            Dim iStatus As Boolean
            iStatus = objBLLHotelSearch.SavePreArrangedHotelBookinginTemp()
            If iStatus = True Then
                Session("sRequestId") = objBLLHotelSearch.OBrequestid
                MessageBox.ShowMessage(Page, MessageType.Success, "Saved Succesfully.")
                Response.Redirect("MoreServices.aspx", False)
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Saved faild.")
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Home.aspx :: btnPreHotelSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    ''' <summary>
    ''' GetNewOrExistingRequestId
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetNewOrExistingRequestId() As String
        Dim strRequestId As String = ""

        If Not HttpContext.Current.Session("sEditRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sEditRequestId")
        ElseIf Not HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sRequestId")
        Else
            Dim objBLLHotelSearch2 As New BLLHotelSearch
            strRequestId = objBLLHotelSearch2.getrequestid()
        End If

        Return strRequestId
    End Function

    'Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
    '    Dim scriptKey As String = "UniqueKeyForThisScript123"
    '    Dim javaScript As String = "<script type='text/javascript'>HideRoomLoders();</script>"
    '    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

    'End Sub
End Class
