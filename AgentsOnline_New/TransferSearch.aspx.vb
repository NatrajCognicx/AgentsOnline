Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System

Partial Class TransferSearch
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLTransferSearch As New BLLTransferSearch
    Dim objBLLHotelSearch As New BLLHotelSearch

    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim objResParam As New ReservationParameters
    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton
    '*** Danny 20/05/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    '*** Danny 20/05/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
                LoadHome()


            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("HomeSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub
    Private Sub BindTrfflightclass()
        Dim strQuery As String = ""
        strQuery = "select flightclscode,flightclsname from flightclsmast(nolock) where active=1"
        objclsUtilities.FillDropDownList(ddlTrfArrFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddlTrfDepFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        ddlTrfArrFlightClass.SelectedIndex = 2
        ddlTrfDepFlightClass.SelectedIndex = 2
    End Sub
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

            If strRequestId <> "" And 1 = 2 Then '1=2 is added since not able to change flight no. in edit mode 'changed by mohamed on 17/09/2018
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
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "


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
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

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
            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername, airportbordercode from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"

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
    Public Shared Function GetArrivalpickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast f(nolock),airportbordersmaster a(nolock)  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
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


            strSqlQry = ""

            If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                Dim dt As New DataTable
                'dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from booking_hotel_detailtemp b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                dt = clsUtilities.GetSharedDataFromDataTable("select  sum(cnt) cnt from (select count(*) cnt from booking_hotel_detailtemp b(nolock)  where requestid='" & HttpContext.Current.Session("sRequestId") & "' union all  select count(*) cnt from booking_hotels_prearrangedtemp b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "' ) rs ")

                If dt.Rows(0)("cnt") > 1 Then
                    strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                   & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and  (isnull(b.shiftto,0)=0 or (isnull(b.shiftto,0)=1 and isnull(shiftfrompartycode,'') like '%1-1%' ))   and p.partyname like  '%" & prefixText & "%' "

                Else
                    strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                        & " where  partyname like '%" & prefixText & "%' order by partyname "
                End If
            Else
                'strSqlQry = "select  partycode,partyname from  ( select  partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode,othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                '    & " where  partyname like '%" & prefixText & "%' order by partyname "

                strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                 & " where  partyname like '%" & prefixText & "%' order by partyname "

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
                    dropoffs.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                Next

            End If

            Return dropoffs
        Catch ex As Exception
            Return dropoffs
        End Try

    End Function
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

            If strRequestId <> "" And 1 = 2 Then '1=2 is added since not able to change flight no. in edit mode 'changed by mohamed on 17/09/2018
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
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
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
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
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

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername, airportbordercode  from view_flightmast_departure where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
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
    Public Shared Function GetTrfDeppickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If


            strSqlQry = ""
            If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                Dim dt As New DataTable
                dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                If dt.Rows(0)("cnt") > 1 Then
                    strSqlQry = "select distinct b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b cross apply  (select min(b1.checkin) checkin from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
            & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "'  and  isnull(b.shiftfrom,0)=0  and p.partyname like  '%" & prefixText & "%' "
                Else
                    strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
& " where  partyname like '%" & prefixText & "%' order by partyname "

                End If
            Else
                strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
& " where  partyname like '%" & prefixText & "%' order by partyname "

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
    Public Shared Function GetTrfDepairportdrop(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast f(nolock),airportbordersmaster a(nolock)  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
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
    Public Shared Function GetTrfCustomer(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast(nolock) where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
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
    <WebMethod()> _
    Public Shared Function GetArrivaldate(ByVal DropCode As String) As String
        Dim SqlConn As New SqlConnection
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Dim reqid As String
        Try
            'strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & Pickupcode.Trim & "'  order by ctryname"

            reqid = Convert.ToString(HttpContext.Current.Session("sRequestId").ToString())


            Dim objBLLHotelSearch = New BLLHotelSearch

            If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then

                objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")

                strSqlQry = " select CONVERT(varchar(10),checkin,103) checkin,adults,child,replace(childages,',',';') childages from view_booking_hotel_prearr(nolock) where requestid='" & objBLLHotelSearch.OBrequestid & "' and partycode='" & DropCode.Trim & "'"


                'Else

                '    strSqlQry = "select null checkin,'' adults,'' child ,'' childages"
            End If





            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Arrivaldates")

            Return myDS.GetXml()
        Catch ex As Exception
            SqlConn.Close()
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function GetDeparturedate(ByVal Pickupcode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Dim reqid As String
        Dim SqlConn As New SqlConnection
        Try
            'strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & Pickupcode.Trim & "'  order by ctryname"

            reqid = Convert.ToString(HttpContext.Current.Session("sRequestId").ToString())


            Dim objBLLHotelSearch = New BLLHotelSearch

            If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then

                objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")

                strSqlQry = " select CONVERT(varchar(10),checkout,103) checkout,adults,child,replace(childages,',',';') childages from view_booking_hotel_prearr(nolock) where requestid='" & objBLLHotelSearch.OBrequestid & "' and partycode='" & Pickupcode.Trim & "'"


                'Else

                '    strSqlQry = "select null checkout,adults,child,childages "
            End If



            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Departdates")

            Return myDS.GetXml()


        Catch ex As Exception
            SqlConn.Close()
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Shared Function GetIntertransferdate(ByVal Pickupcode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Dim reqid As String
        Try
            'strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & Pickupcode.Trim & "'  order by ctryname"

            reqid = Convert.ToString(HttpContext.Current.Session("sRequestId").ToString())


            Dim objBLLHotelSearch = New BLLHotelSearch

            If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then

                objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")

                strSqlQry = "select top 1 convert(varchar(10),a.checkout,103) checkout,partycode=(select partycode from view_booking_hotel_prearr  b(nolock) where  isnull(shiftto,0)=1 " _
                           & " and b.requestid=a.requestid and b.checkin=a.checkout) , partyname=(select partyname from view_booking_hotel_prearr  b(nolock),partymast p(nolock) where b.partycode=p.partycode and  isnull(shiftto,0)=1 " _
                           & " and b.requestid=a.requestid and b.checkin=a.checkout) from view_booking_hotel_prearr a(nolock) where a.requestid='" & objBLLHotelSearch.OBrequestid & "' and a.partycode= '" & Pickupcode.Trim & "'  order by checkout desc"

                'Else

                '    strSqlQry = "select null checkout,'' partycode,'' partyname "
            End If




            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Interdates")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function GetTrfCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
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
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & contextKey.Trim & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    Else
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    End If
                Else
                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"

                End If


            Else
                strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"


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
    Private Sub BindVehicleTypes(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            chkHotelStars.DataSource = dataTable
            chkHotelStars.DataTextField = "vehiclename"
            chkHotelStars.DataValueField = "cartypecode"
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
    Private Sub BindTransferdetails()
        Dim dsTrfdetails As New DataSet

        Dim strQuery As String = ""

        Dim recordexists As String = ""

        If Not Session("sRequestId") Is Nothing Then
            Dim ds As DataSet
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            ds = objBLLCommonFuntions.GetTempFullBookingDetails(Session("sRequestId"))

            If Not ds Is Nothing Then

                'Dim objDALHotelSearch As New DALHotelSearch
                'Dim objBLLHotelSearch = New BLLHotelSearch
                'objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
                Dim hotelcount As Integer
                If ds.Tables(4).Rows.Count > 0 And ds.Tables(1).Rows.Count > 0 Then

                    dsTrfdetails = objBLLTransferSearch.FillTransferDetailsFromMA(Session("sRequestId"))


                    If dsTrfdetails.Tables.Count > 0 Then
                        If recordexists = "" Then
                            BindHotelArrivalDetails(dsTrfdetails.Tables(1))
                            BindHotelDepartureDetails(dsTrfdetails.Tables(2))
                        End If

                    End If


                End If
                If ds.Tables(1).Rows.Count > 0 Or ds.Tables(6).Rows.Count > 0 Then

                    '  strQuery = "select count(distinct partycode) from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
                    hotelcount = ds.Tables(1).Rows.Count + ds.Tables(6).Rows.Count

                    If hotelcount > 1 Then
                        divinterhotel.Style.Add("display", "block")
                    Else
                        divinterhotel.Style.Add("display", "none")
                    End If

                    strQuery = "select 't' from booking_transferstemp(nolock) where  requestid='" & Session("sRequestId") & "'"
                    recordexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    dsTrfdetails = objBLLHotelSearch.FillTransferDetails(Session("sRequestId"))

                    txtTrfCustomercode.Text = ds.Tables(0).Rows(0)("AgentCode").ToString
                    strQuery = "select agentname from agentmast(nolock) where active=1 and agentcode='" & ds.Tables(0).Rows(0)("AgentCode") & "'"
                    txtTrfCustomer.Text = ds.Tables(0).Rows(0)("agentname").ToString 'objBLLHotelSearch.Customer
                    txtTrfSourcecountry.Text = ds.Tables(0).Rows(0)("sourcectryname").ToString
                    txtTrfSourcecountrycode.Text = ds.Tables(0).Rows(0)("sourcectrycode").ToString 'objBLLHotelSearch.SourceCountryCode


                    If dsTrfdetails.Tables(0).Rows.Count > 0 Then
                        ''' Added 01/06/17 shahul
                        Dim childages As String = dsTrfdetails.Tables(0).Rows(0)("childages").ToString.Replace(",", ";")
                        Dim childagestring As String() = childages.ToString.Split(";")
                        ''''''''''
                        'Dim childagestring As String() = dsTrfdetails.Tables(0).Rows(0)("childages").ToString.Split(";")

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

                    End If

                    If dsTrfdetails.Tables.Count > 0 Then
                        If recordexists = "" Then
                            BindHotelArrivalDetails(dsTrfdetails.Tables(1))
                            BindHotelDepartureDetails(dsTrfdetails.Tables(2))
                            BindInterHotelDetails(dsTrfdetails.Tables(3))
                        End If

                    End If

                End If

            End If
            'Dim objDALMASearch As New DALMASearch
            'Dim objBLLMASearch = New BLLMASearch
            'objBLLMASearch = Session("sobjBLLMASearchActive")


        End If







    End Sub
    Private Sub BindHotelArrivalDetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkarrival.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL", True, False)
            txtTrfArrivaldate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlTrfArrFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            If dataTable.Rows(0)("dropoffcode") <> "" Then
                txtTrfArrDropoffcode.Text = dataTable.Rows(0)("dropoffcode")
                txtTrfArrDroptype.Text = dataTable.Rows(0)("dropoffcodetype")  '' Added shahul 21/07/18
            End If

            strQuery = "select partyname from partymast(nolock) where active=1 and partycode='" & dataTable.Rows(0)("dropoffcode") & "'"
            Dim strfArrDropoff As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            If strfArrDropoff <> "" Then
                txtTrfArrDropoff.Text = strfArrDropoff
            End If

            If dataTable.Rows(0)("pickupcode") <> "" Then
                txtTrfArrivalpickupcode.Text = dataTable.Rows(0)("pickupcode")
            End If


            strQuery = " select   a.airportbordername from flightmast f(nolock),airportbordersmaster a(nolock)  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("pickupcode") & "'"
            Dim strPickup As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            If strPickup <> "" Then
                txtTrfArrivalpickup.Text = strPickup
            End If

            If dataTable.Rows(0)("flightcode") <> "" Then
                txtArrivalflightCode.Text = dataTable.Rows(0)("flightcode")
            End If
            If dataTable.Rows(0)("flightcode") <> "" Then
                txtArrivalflight.Text = dataTable.Rows(0)("flightcode")
            End If
            If dataTable.Rows(0)("flighttime") <> "" Then
                txtArrivalTime.Text = dataTable.Rows(0)("flighttime")
            End If



        End If
    End Sub
    Private Sub BindHotelDepartureDetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkDeparture.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE", True, False)
            txtTrfDeparturedate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlTrfDepFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            If dataTable.Rows(0)("dropoffcode") <> "" Then
                txtTrfDepairportdropcode.Text = dataTable.Rows(0)("dropoffcode")

            End If

            strQuery = " select   a.airportbordername from flightmast f(nolock),airportbordersmaster a(nolock)  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("dropoffcode") & "'"
            Dim strDrop As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            If strDrop <> "" Then
                txtTrfDepairportdrop.Text = strDrop
            End If

            If dataTable.Rows(0)("pickupcode") <> "" Then
                txtTrfDeppickupcode.Text = dataTable.Rows(0)("pickupcode")
                txtTrfDeppickuptype.Text = dataTable.Rows(0)("pickupcodetype")  '' Added shahul 21/07/18
            End If


            strQuery = "select partyname from partymast(nolock) where active=1 and partycode='" & dataTable.Rows(0)("pickupcode") & "'"
            Dim strDepPic As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            If strDepPic <> "" Then
                txtTrfDeppickup.Text = strDepPic
            End If

            If dataTable.Rows(0)("flightcode") <> "" Then
                txtDepartureFlightCode.Text = dataTable.Rows(0)("flightcode")
            End If
            If dataTable.Rows(0)("flightcode") <> "" Then
                txtDepartureFlight.Text = dataTable.Rows(0)("flightcode")
            End If
            If dataTable.Rows(0)("flighttime") <> "" Then
                txtDepartureTime.Text = dataTable.Rows(0)("flighttime")
            End If



        End If
    End Sub
    Private Sub BindInterHotelDetails(ByVal datatable As DataTable)

        Dim strQuery As String = ""
        If datatable.Rows.Count > 0 Then
            chkinter.Checked = IIf(datatable.Rows(0)("transfertype").ToString.ToUpper = "INTERHOTEL", True, False)
            txtTrfinterdate.Text = Format(CType(datatable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")

            txtTrfInterdropffcode.Text = datatable.Rows(0)("dropoffcode").ToString
            strQuery = " select   p.partyname from partymast p(nolock)  where p.active=1 and p.partycode='" & datatable.Rows(0)("dropoffcode") & "'"
            txtTrfInterdropff.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtTrfinterPickupcode.Text = datatable.Rows(0)("pickupcode").ToString

            strQuery = "select partyname from partymast(nolock) where active=1 and partycode='" & datatable.Rows(0)("pickupcode") & "'"
            txtTrfinterPickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtTrfinterPickuptype.Text = datatable.Rows(0)("pickupcodetype").ToString  '' Added shahul 21/07/18
            txtTrfInterdropfftype.Text = datatable.Rows(0)("dropoffcodetype").ToString  '' Added shahul 21/07/18

            'txtDepartureFlightCode.Text = datatable.Rows(0)("flightcode")
            'txtDepartureFlight.Text = datatable.Rows(0)("flightcode")
            'txtDepartureTime.Text = datatable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub Amendheaderfill()
        Dim dt As DataTable
        Dim dtpax As DataTable
        Dim strQuery As String = ""
        Try

            dt = objBLLTransferSearch.GetEditBookingDetails(Session("sEditRequestId"), Request.QueryString("Tlineno"))
            If dt.Rows.Count > 0 Then

                txtTrfCustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtTrfCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTrfSourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtTrfSourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                chkTrfoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                '''' ADDed shahul 07/04/18

                'strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sEditRequestId") & "')"
                'dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                'If dtpax.Rows.Count > 0 Then
                ddlTrfAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                ddlTrfChild.SelectedValue = dt.Rows(0)("child").ToString

                If Val(dt.Rows(0)("child").ToString) <> 0 Then


                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtTrfChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtTrfChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtTrfChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtTrfChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtTrfChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtTrfChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtTrfChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtTrfChild8.Text = strChildAges(7)
                    End If
                End If

                'End If

                If dt.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    txtTrfArrivaldate.Text = dt.Rows(0)("transferdate").ToString
                    txtArrivalflight.Text = dt.Rows(0)("flightcode").ToString
                    txtArrivalflightCode.Text = dt.Rows(0)("flightcode").ToString
                    txtArrivalTime.Text = dt.Rows(0)("flighttime").ToString
                    txtTrfArrivalpickupcode.Text = dt.Rows(0)("pickupcode").ToString
                    txtTrfArrivalpickup.Text = dt.Rows(0)("pickupname").ToString
                    txtTrfArrDropoffcode.Text = dt.Rows(0)("dropoffcode").ToString
                    txtTrfArrDroptype.Text = dt.Rows(0)("Dropoffcodetype").ToString  '' Added  shahhul 21/07/18
                    txtTrfArrDropoff.Text = dt.Rows(0)("dropoffname").ToString
                    chkarrival.Checked = True
                    ddlTrfArrFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString

                    chkDeparture.Checked = False
                    chkinter.Checked = False

                    chkDeparture.Enabled = False
                    chkinter.Enabled = False
                End If
                If dt.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE" Then

                    txtTrfDeparturedate.Text = dt.Rows(0)("transferdate").ToString
                    txtDepartureFlight.Text = dt.Rows(0)("flightcode").ToString
                    txtDepartureFlightCode.Text = dt.Rows(0)("flightcode").ToString
                    txtDepartureTime.Text = dt.Rows(0)("flighttime").ToString
                    txtTrfDeppickupcode.Text = dt.Rows(0)("pickupcode").ToString
                    txtTrfDeppickuptype.Text = dt.Rows(0)("Pickupcodetype").ToString '' Added  shahhul 21/07/18
                    txtTrfDeppickup.Text = dt.Rows(0)("pickupname").ToString
                    txtTrfDepairportdropcode.Text = dt.Rows(0)("dropoffcode").ToString
                    txtTrfDepairportdrop.Text = dt.Rows(0)("dropoffname").ToString
                    ddlTrfDepFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString
                    chkDeparture.Checked = True

                    chkarrival.Checked = False
                    chkinter.Checked = False

                    chkarrival.Enabled = False
                    chkinter.Enabled = False
                End If
                If dt.Rows(0)("transfertype").ToString.ToUpper = "INTERHOTEL" Then
                    chkinter.Checked = True
                    txtTrfinterdate.Text = dt.Rows(0)("transferdate").ToString
                    txtTrfinterPickupcode.Text = dt.Rows(0)("pickupcode").ToString
                    txtTrfinterPickup.Text = dt.Rows(0)("pickupname").ToString
                    txtTrfInterdropffcode.Text = dt.Rows(0)("dropoffcode").ToString
                    txtTrfInterdropff.Text = dt.Rows(0)("dropoffname").ToString
                    txtTrfInterdropfftype.Text = dt.Rows(0)("Dropoffcodetype").ToString
                    txtTrfinterPickuptype.Text = dt.Rows(0)("Pickupcodetype").ToString

                    chkarrival.Checked = False
                    chkDeparture.Checked = False

                    chkarrival.Enabled = False
                    chkDeparture.Enabled = False
                End If
                Transfersearch()
                If Session("sLoginType") = "RO" Then

                    txtTrfSourcecountry.Enabled = False
                    txtTrfCustomer.Enabled = False

                End If

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: AmendHeaderFille :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Private Sub NewHeaderFill()

        Dim strrequestid As String = ""
        Dim strQuery As String = ""
        Dim dt As New DataTable
        Dim dtpax As New DataTable
        Try


            If Not Session("sobjBLLTransferSearch") Is Nothing Then
                objBLLTransferSearch = CType(Session("sobjBLLTransferSearch"), BLLTransferSearch)

                strrequestid = GetExistingRequestId()

                chkTrfoverride.Checked = IIf(objBLLTransferSearch.OverridePrice = "1", True, False)
                objBLLTransferSearch.AmendRequestid = strrequestid
                objBLLTransferSearch.AmendLineno = ViewState("Tlineno")


                txtTrfArrivaldate.Text = objBLLTransferSearch.ArrTransferDate
                txtArrivalflight.Text = objBLLTransferSearch.ArrFlightNo
                txtArrivalflightCode.Text = objBLLTransferSearch.ArrFlightNo
                txtArrivalTime.Text = objBLLTransferSearch.ArrFlightTime
                txtTrfArrivalpickupcode.Text = objBLLTransferSearch.ArrPickupCode
                txtTrfArrivalpickup.Text = objBLLTransferSearch.ArrPickupName
                txtTrfArrDropoffcode.Text = objBLLTransferSearch.ArrDropCode
                txtTrfArrDroptype.Text = objBLLTransferSearch.ArrDroptype  '' Added shahul 19/07/18
                txtTrfArrDropoff.Text = objBLLTransferSearch.ArrDropName
                ddlTrfArrFlightClass.SelectedValue = objBLLTransferSearch.ArrFlightClass
                chkarrival.Checked = IIf(objBLLTransferSearch.ArrTransferType = "ARRIVAL", True, False)

                txtTrfDeparturedate.Text = objBLLTransferSearch.DepTransferDate
                txtDepartureFlight.Text = objBLLTransferSearch.DepFlightNo
                txtDepartureFlightCode.Text = objBLLTransferSearch.DepFlightNo
                txtDepartureTime.Text = objBLLTransferSearch.DepFlightTime
                txtTrfDeppickupcode.Text = objBLLTransferSearch.DepPickupCode
                txtTrfDeppickuptype.Text = objBLLTransferSearch.DepPickuptype  '' Added shahul 19/07/18

                txtTrfDeppickup.Text = objBLLTransferSearch.DepPickupName
                txtTrfDepairportdropcode.Text = objBLLTransferSearch.DepDropCode
                txtTrfDepairportdrop.Text = objBLLTransferSearch.DepDropName
                ddlTrfDepFlightClass.SelectedValue = objBLLTransferSearch.DepFlightClass
                chkDeparture.Checked = IIf(objBLLTransferSearch.DepTransferType = "DEPARTURE", True, False)

                chkinter.Checked = IIf(objBLLTransferSearch.ShiftingTransferType = "INTERHOTEL", True, False)
                txtTrfinterdate.Text = objBLLTransferSearch.ShiftingDate
                txtTrfinterPickupcode.Text = objBLLTransferSearch.ShiftingPickupCode
                txtTrfinterPickuptype.Text = objBLLTransferSearch.ShiftingPickuptype '' Added shahul 19/07/18
                txtTrfinterPickup.Text = objBLLTransferSearch.ShiftingPickupName
                txtTrfInterdropffcode.Text = objBLLTransferSearch.ShiftingDropCode
                txtTrfInterdropfftype.Text = objBLLTransferSearch.ShiftingDroptype '' Added shahul 19/07/18
                txtTrfInterdropff.Text = objBLLTransferSearch.ShiftingDropName


                Dim chksector As String = ""
                'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ArrDropCode & "'"
                'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                '' Added shahul 19/07/18
                If txtTrfArrDroptype.Text = "P" Then
                    strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ArrDropCode & "'"
                    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                    If chksector <> "" Then
                        strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ArrDropCode & "'"
                        objBLLTransferSearch.ArrSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        If objBLLTransferSearch.ArrSector Is Nothing = True Then
                            objBLLTransferSearch.ArrSector = ""
                        End If
                    End If
                Else
                    objBLLTransferSearch.ArrSector = objBLLTransferSearch.ArrDropCode
                End If
                'strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ArrDropCode & "'"
                'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                'If chksector <> "" Then
                '    strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ArrDropCode & "'"
                '    objBLLTransferSearch.ArrSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                '    If objBLLTransferSearch.ArrSector Is Nothing = True Then
                '        objBLLTransferSearch.ArrSector = ""
                '    End If
                'Else
                '    objBLLTransferSearch.ArrSector = objBLLTransferSearch.ArrDropCode
                'End If

                'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.DepPickupCode & "'"
                'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                If txtTrfDeppickuptype.Text = "P" Then
                    strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.DepPickupCode & "'"
                    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                    If chksector <> "" Then
                        strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.DepPickupCode & "'"
                        objBLLTransferSearch.DepSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        If objBLLTransferSearch.DepSector Is Nothing = True Then
                            objBLLTransferSearch.DepSector = ""
                        End If
                    End If

                Else
                    objBLLTransferSearch.DepSector = objBLLTransferSearch.DepPickupCode
                End If

                'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
                'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                If txtTrfinterPickuptype.Text = "P" Then

                    strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
                    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                    If chksector <> "" Then
                        strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
                        objBLLTransferSearch.ShiftingPickupSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        If objBLLTransferSearch.ShiftingPickupSector Is Nothing = True Then
                            objBLLTransferSearch.ShiftingPickupSector = ""
                        End If
                    End If

                Else
                    objBLLTransferSearch.ShiftingPickupSector = objBLLTransferSearch.ShiftingPickupCode
                End If

                'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ShiftingDropCode & "'"
                'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                If txtTrfInterdropfftype.Text = "P" Then
                    strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ShiftingDropCode & "'"
                    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                    If chksector <> "" Then

                        strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ShiftingDropCode & "'"
                        objBLLTransferSearch.ShiftingDropSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        If objBLLTransferSearch.ShiftingDropSector Is Nothing = True Then
                            objBLLTransferSearch.ShiftingDropSector = ""
                        End If
                    End If

                Else
                    objBLLTransferSearch.ShiftingDropSector = objBLLTransferSearch.ShiftingDropCode
                End If

                '''' Added shahul 07/04/18
                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & strrequestid & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 And strrequestid <> "" Then
                    ddlTrfAdult.SelectedValue = Val(dtpax.Rows(0)("adults").ToString)

                    If Val(dtpax.Rows(0)("child").ToString) <> 0 Then
                        ddlTrfChild.SelectedValue = dtpax.Rows(0)("child").ToString

                        Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                        If Left(childages, 1) = ";" Then
                            childages = Right(childages, (childages.Length - 1))
                        End If
                        Dim strChildAges As String() = childages.ToString.Split(";")
                        '''''''

                        If strChildAges.Length <> 0 Then
                            txtTrfChild1.Text = strChildAges(0)
                        End If

                        If strChildAges.Length > 1 Then
                            txtTrfChild2.Text = strChildAges(1)
                        End If
                        If strChildAges.Length > 2 Then
                            txtTrfChild3.Text = strChildAges(2)
                        End If
                        If strChildAges.Length > 3 Then
                            txtTrfChild4.Text = strChildAges(3)
                        End If
                        If strChildAges.Length > 4 Then
                            txtTrfChild5.Text = strChildAges(4)
                        End If
                        If strChildAges.Length > 5 Then
                            txtTrfChild6.Text = strChildAges(5)
                        End If
                        If strChildAges.Length > 6 Then
                            txtTrfChild7.Text = strChildAges(6)
                        End If
                        If strChildAges.Length > 7 Then
                            txtTrfChild8.Text = strChildAges(7)
                        End If
                    End If
                Else
                    ddlTrfAdult.SelectedValue = objBLLTransferSearch.TrfAdult
                    ddlTrfChild.SelectedValue = objBLLTransferSearch.TrfChildren
                    txtTrfChild1.Text = objBLLTransferSearch.TrfChild1
                    txtTrfChild2.Text = objBLLTransferSearch.TrfChild2
                    txtTrfChild3.Text = objBLLTransferSearch.TrfChild3
                    txtTrfChild4.Text = objBLLTransferSearch.TrfChild4
                    txtTrfChild5.Text = objBLLTransferSearch.TrfChild5
                    txtTrfChild6.Text = objBLLTransferSearch.TrfChild6
                    txtTrfChild7.Text = objBLLTransferSearch.TrfChild7
                    txtTrfChild8.Text = objBLLTransferSearch.TrfChild8

                End If



                txtTrfCustomercode.Text = objBLLTransferSearch.TrfCustomerCode
                txtTrfCustomer.Text = objBLLTransferSearch.TrfCustomer
                txtTrfSourcecountrycode.Text = objBLLTransferSearch.TrfSourceCountryCode
                txtTrfSourcecountry.Text = objBLLTransferSearch.TrfSourceCountry
            Else
                Dim objBLLCommonFuntions = New BLLCommonFuntions

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                If dt.Rows.Count > 0 Then

                    txtTrfCustomercode.Text = dt.Rows(0)("agentcode").ToString
                    txtTrfCustomer.Text = dt.Rows(0)("agentname").ToString
                    txtTrfSourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                    txtTrfSourcecountry.Text = dt.Rows(0)("sourcectryname").ToString

                    '''' ADDed shahul 07/04/18

                    strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sRequestId") & "')"
                    dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                    If dtpax.Rows.Count > 0 Then
                        ddlTrfAdult.SelectedValue = Val(dtpax.Rows(0)("adults").ToString)

                        If Val(dtpax.Rows(0)("child").ToString) <> 0 Then
                            ddlTrfChild.SelectedValue = dtpax.Rows(0)("child").ToString

                            Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                            If Left(childages, 1) = ";" Then
                                childages = Right(childages, (childages.Length - 1))
                            End If
                            Dim strChildAges As String() = childages.ToString.Split(";")
                            '''''''

                            If strChildAges.Length <> 0 Then
                                txtTrfChild1.Text = strChildAges(0)
                            End If

                            If strChildAges.Length > 1 Then
                                txtTrfChild2.Text = strChildAges(1)
                            End If
                            If strChildAges.Length > 2 Then
                                txtTrfChild3.Text = strChildAges(2)
                            End If
                            If strChildAges.Length > 3 Then
                                txtTrfChild4.Text = strChildAges(3)
                            End If
                            If strChildAges.Length > 4 Then
                                txtTrfChild5.Text = strChildAges(4)
                            End If
                            If strChildAges.Length > 5 Then
                                txtTrfChild6.Text = strChildAges(5)
                            End If
                            If strChildAges.Length > 6 Then
                                txtTrfChild7.Text = strChildAges(6)
                            End If
                            If strChildAges.Length > 7 Then
                                txtTrfChild8.Text = strChildAges(7)
                            End If
                        End If
                    End If


                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: NewHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub EditHeaderFill()
        Try

            Dim strQuery As String = ""
            Dim chksector As String = ""
            Dim trftype As String = ""

            Dim strrequestid As String = ""
            Dim dt As New DataTable
            Dim dtpax As DataTable

            ' objBLLTransferSearch = CType(Session("sobjBLLTransferSearch"), BLLTransferSearch)

            strrequestid = GetExistingRequestId()


            'chkTrfoverride.Checked = IIf(objBLLTransferSearch.OverridePrice = "1", True, False)
            'objBLLTransferSearch.AmendRequestid = strrequestid
            'objBLLTransferSearch.AmendLineno = ViewState("Tlineno")


            dt = objBLLTransferSearch.GetEditBookingDetails(strrequestid, Request.QueryString("Tlineno"))
            If dt.Rows.Count > 0 Then

                txtTrfCustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtTrfCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTrfSourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtTrfSourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                chkTrfoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                ''' To Fill Adult  & Child
                ''' 



                ddlTrfAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                ddlTrfChild.SelectedValue = dt.Rows(0)("child").ToString

                If Val(dt.Rows(0)("child").ToString) <> 0 Then


                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtTrfChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtTrfChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtTrfChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtTrfChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtTrfChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtTrfChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtTrfChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtTrfChild8.Text = strChildAges(7)
                    End If
                End If







                If dt.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    txtTrfArrivaldate.Text = dt.Rows(0)("transferdate").ToString
                    txtArrivalflight.Text = dt.Rows(0)("flightcode").ToString
                    txtArrivalflightCode.Text = dt.Rows(0)("flightcode").ToString
                    txtArrivalTime.Text = dt.Rows(0)("flighttime").ToString
                    txtTrfArrivalpickupcode.Text = dt.Rows(0)("pickupcode").ToString
                    txtTrfArrivalpickup.Text = dt.Rows(0)("pickupname").ToString
                    txtTrfArrDropoffcode.Text = dt.Rows(0)("dropoffcode").ToString
                    txtTrfArrDropoff.Text = dt.Rows(0)("dropoffname").ToString
                    txtTrfArrDroptype.Text = dt.Rows(0)("dropoffcodetype").ToString   '' Added shahul 21/07/18
                    chkarrival.Checked = True
                    ddlTrfArrFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString
                    chkDeparture.Checked = False
                    chkDeparture.Enabled = False
                    chkinter.Checked = False
                    chkinter.Enabled = False

                    txtTrfDeparturedate.Text = ""
                    txtDepartureFlight.Text = ""
                    txtDepartureFlightCode.Text = ""
                    txtDepartureTime.Text = ""
                    txtTrfDeppickupcode.Text = ""
                    txtTrfDeppickuptype.Text = ""
                    txtTrfDeppickup.Text = ""
                    txtTrfDepairportdropcode.Text = ""
                    txtTrfDepairportdrop.Text = ""


                End If
                If dt.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE" Then

                    txtTrfDeparturedate.Text = dt.Rows(0)("transferdate").ToString
                    txtDepartureFlight.Text = dt.Rows(0)("flightcode").ToString
                    txtDepartureFlightCode.Text = dt.Rows(0)("flightcode").ToString
                    txtDepartureTime.Text = dt.Rows(0)("flighttime").ToString
                    txtTrfDeppickupcode.Text = dt.Rows(0)("pickupcode").ToString
                    txtTrfDeppickuptype.Text = dt.Rows(0)("Pickupcodetype").ToString  '' Added shahul 21/07/18
                    txtTrfDeppickup.Text = dt.Rows(0)("pickupname").ToString
                    txtTrfDepairportdropcode.Text = dt.Rows(0)("dropoffcode").ToString
                    txtTrfDepairportdrop.Text = dt.Rows(0)("dropoffname").ToString
                    ddlTrfDepFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString
                    chkDeparture.Checked = True

                    chkarrival.Checked = False
                    chkinter.Checked = False

                    chkarrival.Enabled = False
                    chkinter.Enabled = False

                    txtTrfArrivaldate.Text = ""
                    txtArrivalflight.Text = ""
                    txtArrivalflightCode.Text = ""
                    txtArrivalTime.Text = ""
                    txtTrfArrivalpickupcode.Text = ""
                    txtTrfArrivalpickup.Text = ""
                    txtTrfArrDropoffcode.Text = ""
                    txtTrfArrDroptype.Text = "" '' added shahul 21/07/18
                    txtTrfArrDropoff.Text = ""

                End If
                If dt.Rows(0)("transfertype").ToString.ToUpper = "INTERHOTEL" Then
                    chkinter.Checked = True
                    txtTrfinterdate.Text = dt.Rows(0)("transferdate").ToString
                    txtTrfinterPickupcode.Text = dt.Rows(0)("pickupcode").ToString
                    txtTrfinterPickup.Text = dt.Rows(0)("pickupname").ToString
                    txtTrfInterdropffcode.Text = dt.Rows(0)("dropoffcode").ToString
                    txtTrfInterdropff.Text = dt.Rows(0)("dropoffname").ToString
                    txtTrfinterPickuptype.Text = dt.Rows(0)("Pickupcodetype").ToString   '' Added shahul 21/07/18
                    txtTrfInterdropfftype.Text = dt.Rows(0)("dropoffcodetype").ToString   '' Added shahul 21/07/18

                    chkarrival.Checked = False
                    chkDeparture.Checked = False

                    chkarrival.Enabled = False
                    chkDeparture.Enabled = False
                End If
                Transfersearch()
                If Session("sLoginType") = "RO" Then

                    txtTrfSourcecountry.Enabled = False
                    txtTrfCustomer.Enabled = False

                End If
            End If

            'strQuery = "select transfertype from booking_transferstemp(nolock) where requestid='" & strrequestid & "' and tlineno=" & ViewState("Tlineno")
            'trftype = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            'If trftype.ToString.ToUpper = "ARRIVAL" Then
            '    txtTrfArrivaldate.Text = objBLLTransferSearch.ArrTransferDate
            '    txtArrivalflight.Text = objBLLTransferSearch.ArrFlightNo
            '    txtArrivalflightCode.Text = objBLLTransferSearch.ArrFlightNo
            '    txtArrivalTime.Text = objBLLTransferSearch.ArrFlightTime
            '    txtTrfArrivalpickupcode.Text = objBLLTransferSearch.ArrPickupCode
            '    txtTrfArrivalpickup.Text = objBLLTransferSearch.ArrPickupName
            '    txtTrfArrDropoffcode.Text = objBLLTransferSearch.ArrDropCode
            '    txtTrfArrDropoff.Text = objBLLTransferSearch.ArrDropName
            '    ddlTrfArrFlightClass.SelectedValue = objBLLTransferSearch.ArrFlightClass
            '    chkarrival.Checked = IIf(objBLLTransferSearch.ArrTransferType = "ARRIVAL", True, False)


            '    strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ArrDropCode & "'"
            '    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            '    If chksector = "" Then
            '        strQuery = "select s.sectorgroupcode from partymast p,sectormaster s where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ArrDropCode & "'"
            '        objBLLTransferSearch.ArrSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            '        If objBLLTransferSearch.ArrSector Is Nothing = True Then
            '            objBLLTransferSearch.ArrSector = ""
            '        End If
            '    Else
            '        objBLLTransferSearch.ArrSector = objBLLTransferSearch.ArrDropCode
            '    End If
            '    chkinter.Enabled = False
            '    chkDeparture.Enabled = False

            'ElseIf trftype.ToString.ToUpper = "DEPARTURE" Then
            '    txtTrfDeparturedate.Text = objBLLTransferSearch.DepTransferDate
            '    txtDepartureFlight.Text = objBLLTransferSearch.DepFlightNo
            '    txtDepartureFlightCode.Text = objBLLTransferSearch.DepFlightNo
            '    txtDepartureTime.Text = objBLLTransferSearch.DepFlightTime
            '    txtTrfDeppickupcode.Text = objBLLTransferSearch.DepPickupCode
            '    txtTrfDeppickup.Text = objBLLTransferSearch.DepPickupName
            '    txtTrfDepairportdropcode.Text = objBLLTransferSearch.DepDropCode
            '    txtTrfDepairportdrop.Text = objBLLTransferSearch.DepDropName
            '    ddlTrfDepFlightClass.SelectedValue = objBLLTransferSearch.DepFlightClass
            '    chkDeparture.Checked = IIf(objBLLTransferSearch.DepTransferType = "DEPARTURE", True, False)

            '    strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.DepPickupCode & "'"
            '    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            '    If chksector = "" Then
            '        strQuery = "select s.sectorgroupcode from partymast p,sectormaster s where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.DepPickupCode & "'"
            '        objBLLTransferSearch.DepSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            '        If objBLLTransferSearch.DepSector Is Nothing = True Then
            '            objBLLTransferSearch.DepSector = ""
            '        End If
            '    Else
            '        objBLLTransferSearch.DepSector = objBLLTransferSearch.DepPickupCode
            '    End If

            '    chkarrival.Enabled = False
            '    chkinter.Enabled = False
            'Else
            '    chkinter.Checked = IIf(objBLLTransferSearch.ShiftingTransferType = "INTERHOTEL", True, False)
            '    txtTrfinterdate.Text = objBLLTransferSearch.ShiftingDate
            '    txtTrfinterPickupcode.Text = objBLLTransferSearch.ShiftingPickupCode
            '    txtTrfinterPickup.Text = objBLLTransferSearch.ShiftingPickupName
            '    txtTrfInterdropffcode.Text = objBLLTransferSearch.ShiftingDropCode
            '    txtTrfInterdropff.Text = objBLLTransferSearch.ShiftingDropName

            '    strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
            '    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            '    If chksector = "" Then
            '        strQuery = "select s.sectorgroupcode from partymast p,sectormaster s where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
            '        objBLLTransferSearch.ShiftingPickupSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            '        If objBLLTransferSearch.ShiftingPickupSector Is Nothing = True Then
            '            objBLLTransferSearch.ShiftingPickupSector = ""
            '        End If
            '    Else
            '        objBLLTransferSearch.ShiftingPickupSector = objBLLTransferSearch.ShiftingPickupCode
            '    End If

            '    strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ShiftingDropCode & "'"
            '    chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            '    If chksector = "" Then

            '        strQuery = "select s.sectorgroupcode from partymast p,sectormaster s where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ShiftingDropCode & "'"
            '        objBLLTransferSearch.ShiftingDropSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            '        If objBLLTransferSearch.ShiftingDropSector Is Nothing = True Then
            '            objBLLTransferSearch.ShiftingDropSector = ""
            '        End If
            '    Else
            '        objBLLTransferSearch.ShiftingDropSector = objBLLTransferSearch.ShiftingPickupCode
            '    End If
            '    chkarrival.Enabled = False
            '    chkDeparture.Enabled = False
            'End If

            'strQuery = "select 't'  from booking_transferstemp t(nolock) where  cartypecode in (select option_selected from reservation_parameters where param_id=1149) and requestid='" & strrequestid & "' and tlineno=" & ViewState("Tlineno")
            'Dim cartype As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            'If cartype <> "" Then
            '    ddlTrfAdult.SelectedValue = objBLLTransferSearch.TrfAdult
            '    ddlTrfChild.Style.Add("display", "none")
            'Else
            '    ddlTrfAdult.SelectedValue = objBLLTransferSearch.TrfAdult
            '    ddlTrfChild.SelectedValue = objBLLTransferSearch.TrfChildren
            '    ddlTrfChild1.SelectedValue = objBLLTransferSearch.TrfChild1
            '    ddlTrfChild2.SelectedValue = objBLLTransferSearch.TrfChild2
            '    ddlTrfChild3.SelectedValue = objBLLTransferSearch.TrfChild3
            '    ddlTrfChild4.SelectedValue = objBLLTransferSearch.TrfChild4
            '    ddlTrfChild5.SelectedValue = objBLLTransferSearch.TrfChild5
            '    ddlTrfChild6.SelectedValue = objBLLTransferSearch.TrfChild6
            '    ddlTrfChild7.SelectedValue = objBLLTransferSearch.TrfChild7
            '    ddlTrfChild8.SelectedValue = objBLLTransferSearch.TrfChild8
            'End If




            'txtTrfCustomercode.Text = objBLLTransferSearch.TrfCustomerCode
            'txtTrfCustomer.Text = objBLLTransferSearch.TrfCustomer
            'txtTrfSourcecountrycode.Text = objBLLTransferSearch.TrfSourceCountryCode
            'txtTrfSourcecountry.Text = objBLLTransferSearch.TrfSourceCountry

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: EditHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub LoadHome()
        LoadFooter()
        objBLLTransferSearch = New BLLTransferSearch
        LoadLogo()
        LoadMenus()
        objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        LoadRoomAdultChild()
        LoadFields()
        BindTrfflightclass()
        BindTransferdetails()
        ShowMyBooking()
        hdPriceMinRange.Value = "0"
        hdPriceMaxRange.Value = "1"

        Dim strQuery As String = ""

        ViewState("Tlineno") = Request.QueryString("TLineNo")

        If Not Request.QueryString("TlineNo") Is Nothing Then
            hdOPMode.Value = "Edit"
        End If

        Dim strrequestid As String = ""

        Dim dt As DataTable
        If Not Session("sEditRequestId") Is Nothing Then

            If ViewState("Tlineno") Is Nothing Then
                NewHeaderFill()
            Else
                Amendheaderfill()
            End If

        Else
            If Not Session("sobjBLLTransferSearch") Is Nothing Then

                If ViewState("Tlineno") Is Nothing Then
                    '  NewHeaderFill()
                    ''Earlier cheking its commentted so its not filling form home page
                    NewHeaderFill()

                Else
                    EditHeaderFill()
                    ' BindSearchResults()
                End If
                ''Earlier cheking its not there its inside if statment
                BindSearchResults()

            Else
                NewHeaderFill()
            End If
        End If

        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If

        'If Not Page.Request.UrlReferrer Is Nothing Then
        '    Dim previousPage As String = Page.Request.UrlReferrer.ToString
        '    If previousPage.Contains("MoreServices.aspx") Then
        '        LoadFlightDetails()
        '        LoadFiledsBasedOnTrfdetails()
        '    End If
        'End If

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
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                'imgLogo.Src = "Logos/" & strLogo
                imgLogo.Src = Session("sLogo") '*** Danny 04/07/2018

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
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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


                divTrfOverride.Style.Add("display", "none")
                dvTrfCustomer.Style.Add("display", "none")
                ' dvTrfCustomer.Visible = False

                Dim strSqlQry As String = ""
                Dim myDS As New DataSet
                Dim Hotelnames As New List(Of String)
                Try



                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' order by ctryname"

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count = 1 Then


                        txtTrfSourcecountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtTrfSourcecountrycode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtTrfSourcecountry.ReadOnly = True
                        AutoCompleteExtender_txtTrfSourcecountry.Enabled = False
                    Else

                        txtTrfSourcecountry.ReadOnly = False
                        AutoCompleteExtender_txtTrfSourcecountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else

                divTrfOverride.Style.Add("display", "block")
                dvTrfCustomer.Style.Add("display", "block")


            End If
        Else
            hdLoginType.Value = ""
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
            strSqlQry = "select destcode,destname,desttype from view_destination_search where destname like  '%" & prefixText & "%' "
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

    <WebMethod()> _
    Public Shared Function GetHotelsDetails(ByVal HotelCode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            'strSqlQry = "select p.sectorcode,s.sectorname,c.catcode,c.catname from partymast p,sectormaster s,catmast c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
            strSqlQry = "select s.destname,c.catcode,c.catname,s.destcode + '|' +case when desttype='Area' or desttype='Sector' then 'Sector' else desttype end destcode  from partymast p(nolock),view_destination_search s,catmast c(nolock) where p.sectorcode=s.destcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
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


    Private Sub BindSearchResults()
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString

                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If



        objBLLTransferSearch = New BLLTransferSearch
        If Session("sobjBLLTransferSearch") Is Nothing Then
            Response.Redirect("Home.aspx?Tab=3")
        End If
        objBLLTransferSearch = CType(Session("sobjBLLTransferSearch"), BLLTransferSearch)

        Dim dsSearchResults As New DataSet
        objBLLTransferSearch.FilterRoomClass = ""
        dsSearchResults = objBLLTransferSearch.GetSearchDetails()
        If dsSearchResults.Tables(4).Rows.Count = 0 Then
            dvhotnoshow.Style.Add("display", "block")
        Else
            dvhotnoshow.Style.Add("display", "none")
        End If

        Session("sDSTrfSearchResults") = dsSearchResults
        If dsSearchResults.Tables.Count > 0 Then

            BindVehicleTypes(dsSearchResults.Tables(4))
            BindPricefilter(dsSearchResults.Tables(3))


            Session("sDSTrfSearchResults") = dsSearchResults
            Session("sTrfMailBoxPageIndex") = "1"
            Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
            Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))
            Dim dvInterMainDetails As DataView = New DataView(dsSearchResults.Tables(2))
            If ddlSorting.Text = "Name" Then
                dvMaiDetails.Sort = "partyname ASC"
            ElseIf ddlSorting.Text = "Price" Then
                dvMaiDetails.Sort = "avgprice ASC"
            ElseIf ddlSorting.Text = "Rating" Then
                dvMaiDetails.Sort = "noofstars DESC,partyname ASC "
            ElseIf ddlSorting.Text = "Preferred" Then
                dvMaiDetails.Sort = "Preferred  DESC,partyname ASC "
            End If
            Dim recordCount As Integer = dvMaiDetails.Count + dvDepMaiDetails.Count + dvInterMainDetails.Count

            ViewState("Arrviewmorehide") = dvMaiDetails.Count
            BindArrivalDetails(dvMaiDetails)
            ViewState("Depviewmorehide") = dvDepMaiDetails.Count
            BindDepartureDetails(dvDepMaiDetails)
            ViewState("Interviewmorehide") = dvInterMainDetails.Count
            BindInterhoteleDetails(dvInterMainDetails)

            Me.PopulatePager(recordCount)

            lblHotelCount.Text = recordCount & " Records Found"
        Else
            hdPriceMinRange.Value = "0"
            hdPriceMaxRange.Value = "1"
        End If


    End Sub

    Protected Sub lbArrShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSTrfSearchResults")
            Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))

            Dim chkbooknow As CheckBox



            If myLinkButton.Text = "Show More" Then
                ViewState("ArrShow") = "1"
                BindArrivalDetails(dvMaiDetails, "1")
                myLinkButton.Text = "Show Less"

            Else

                ViewState("ArrShow") = "0"
                BindArrivalDetails(dvMaiDetails, "")
                myLinkButton.Text = "Show More"
                dlArrTransferSearchResults.Focus()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: lbArrShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbinterShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSTrfSearchResults")
            Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))


            If myLinkButton.Text = "Show More" Then
                ViewState("InterShow") = "1"

                BindInterhoteleDetails(dvMaiDetails, "1")
                myLinkButton.Text = "Show Less"

            Else
                ViewState("InterShow") = "0"

                BindInterhoteleDetails(dvMaiDetails, "")
                myLinkButton.Text = "Show More"
                dlShifting.Focus()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: lbinterShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbDepShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSTrfSearchResults")
            Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))


            If myLinkButton.Text = "Show More" Then
                ViewState("DepShow") = "1"
                BindDepartureDetails(dvDepMaiDetails, "1")

                myLinkButton.Text = "Show Less"
            Else
                ViewState("DepShow") = "0"
                BindDepartureDetails(dvDepMaiDetails, "")
                dlDepTransferSearchResults.Focus()
                myLinkButton.Text = "Show More"

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: lbDepShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub dlArrTransferSearchResults_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlArrTransferSearchResults.ItemCreated
        If dlArrTransferSearchResults.Items.Count = 1 And e.Item.ItemType = 1 And ViewState("Arrviewmorehide") = 1 Then
            Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbArrShowMore"), LinkButton)

            myLinkButton.Visible = False
        End If
    End Sub

    Protected Sub dlArrTransferSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlArrTransferSearchResults.ItemDataBound

        If e.Item.ItemType = 1 Then
            Dim myarrButton As LinkButton = CType(e.Item.FindControl("lbArrShowMore"), LinkButton)

            If ViewState("ArrShow") = "1" Then
                myarrButton.Text = "Show Less"
            Else
                myarrButton.Text = "Show More"
            End If

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim imgHotelImage As Image = CType(e.Item.FindControl("imgvehicleImage"), Image)
            Dim lblHotelImage As Label = CType(e.Item.FindControl("lblHotelImage"), Label)
            imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblHotelImage.Text & "&type=2"

            Dim lblshuttle As Label = CType(e.Item.FindControl("lblshuttle"), Label)
            Dim lblpaxcheck As Label = CType(e.Item.FindControl("lblpaxcheck"), Label)
            Dim lblmin As Label = CType(e.Item.FindControl("lblmin"), Label)
            Dim lblminpax As Label = CType(e.Item.FindControl("lblminpax"), Label)
            Dim lblmax As Label = CType(e.Item.FindControl("lblmax"), Label)
            Dim lblmaxpax As Label = CType(e.Item.FindControl("lblmaxpax"), Label)

            Dim lblcurrcode As Label = CType(e.Item.FindControl("lblcurrcode"), Label)
            Dim txtnoofunits As TextBox = CType(e.Item.FindControl("txtnoofunits"), TextBox)
            Dim lbltrfunit As Label = CType(e.Item.FindControl("lbltrfunit"), Label)
            Dim txttotal As TextBox = CType(e.Item.FindControl("txttotal"), TextBox)
            Dim lblprice As Label = CType(e.Item.FindControl("lblprice"), Label)
            Dim lblvaluetext As Label = CType(e.Item.FindControl("lblvaluetext"), Label)
            Dim txtunitprice As TextBox = CType(e.Item.FindControl("txtunitprice"), TextBox)
            Dim chkbooknow As CheckBox = CType(e.Item.FindControl("chkbooknow"), CheckBox)
            ' Dim lblbooknow As Label = CType(e.Item.FindControl("lblbooknow"), Label)

            Dim txtwltotal As TextBox = CType(e.Item.FindControl("txtwltotal"), TextBox)
            Dim txtwlunitprice As TextBox = CType(e.Item.FindControl("txtwlunitprice"), TextBox)
            Dim lblwlprice As Label = CType(e.Item.FindControl("lblunitcprice"), Label)
            Dim lblwlvaluetext As Label = CType(e.Item.FindControl("lblwlvaluetext"), Label)
            Dim lblunitprice As Label = CType(e.Item.FindControl("lblunitprice"), Label)
            Dim lblwlunitprice As Label = CType(e.Item.FindControl("lblwlunitprice"), Label)

            Dim lblpreferedsupplier As Label = CType(e.Item.FindControl("lblpreferedsupplier"), Label)
            Dim lblunitcprice As Label = CType(e.Item.FindControl("lblunitcprice"), Label)
            Dim lblunitcostvalue As Label = CType(e.Item.FindControl("lblunitcostvalue"), Label)
            Dim lbltcplistcode As Label = CType(e.Item.FindControl("lbltcplistcode"), Label)
            Dim lblwlconvrate As Label = CType(e.Item.FindControl("lblwlconvrate"), Label)
            Dim lblwlmarkupperc As Label = CType(e.Item.FindControl("lblwlmarkupperc"), Label)
            Dim lblwlunitprice_grid As Label = CType(e.Item.FindControl("lblwlunitprice_grid"), Label)
            Dim lblwlunitsalevalue As Label = CType(e.Item.FindControl("lblwlunitsalevalue"), Label)

            Dim lblwlCurrCode As Label = CType(e.Item.FindControl("lblwlCurrCode"), Label)


            Dim divpaxprice As HtmlGenericControl = CType(e.Item.FindControl("divpaxprice"), HtmlGenericControl)
            Dim divlbl As HtmlGenericControl = CType(e.Item.FindControl("divlbl"), HtmlGenericControl)
            Dim divcomplement As HtmlGenericControl = CType(e.Item.FindControl("divcomplement"), HtmlGenericControl)

            Dim chkarrcompliment As CheckBox = CType(e.Item.FindControl("chkarrcompliment"), CheckBox)

            If Session("sLoginType") <> "RO" Then
                divcomplement.Style.Add("display", "none")
            Else
                divcomplement.Style.Add("display", "block")
            End If

            txtnoofunits.Text = lbltrfunit.Text

            If lblpaxcheck.Text = 0 Or lblshuttle.Text = 1 Then
                lblmin.Visible = False
                lblminpax.Visible = False
                lblmax.Visible = False
                lblmaxpax.Visible = False


                '  txtnoofunits.ReadOnly = True

                txttotal.Text = Val(txtnoofunits.Text) * Val(lblprice.Text)
                lblunitprice.Text = Math.Round(Val(lblprice.Text), 2).ToString + " " + lblcurrcode.Text + "  /  PAX"
                txtunitprice.Text = Math.Round(Val(lblprice.Text), 2)
                lblvaluetext.Text = " " + lblcurrcode.Text + "  /  PAX"

                Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                Dim dwlUnitprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                dwlUnitprice = dUnitprice * dWlMarkup

                lblwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2).ToString + " " + lblcurrcode.Text + "  /  PAX"
                txtwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2)
                txtwltotal.Text = Val(txtnoofunits.Text) * Val(dwlUnitprice)


            Else
                lblmin.Visible = True
                lblminpax.Visible = True
                lblmax.Visible = True
                lblmaxpax.Visible = True




                lblunitprice.Text = Math.Round(Val(lblprice.Text), 2).ToString + " " + lblcurrcode.Text + "  /  UNIT"

                txtunitprice.Text = Math.Round(Val(lblprice.Text), 2)
                lblvaluetext.Text = " " + lblcurrcode.Text + "  /  UNIT"
                txttotal.Text = Math.Round(Val(txtnoofunits.Text) * Val(txtunitprice.Text), 2)
                'lblwlunitprice.Text = Math.Round(Val(lblwlunitprice_grid.Text)) + " " + lblcurrcode.Text + "  /  UNIT"
                'txtwlunitprice.Text = Math.Round(Val(lblwlunitprice_grid.Text))

                Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                Dim dwlUnitprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                dwlUnitprice = dUnitprice * dWlMarkup

                lblwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2).ToString + " " + lblcurrcode.Text + "  /  UNIT"
                txtwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2)
                txtwltotal.Text = Math.Round(Val(txtnoofunits.Text) * Val(dwlUnitprice), 2)
                '  txtnoofunits.ReadOnly = False



            End If
            If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                lblwlunitprice.Style.Add("display", "block")
                txtwlunitprice.Style.Add("display", "block")
                txtwltotal.Style.Add("display", "block")

                txttotal.Style.Add("display", "none")
                lblunitprice.Style.Add("display", "none")
                txtunitprice.Style.Add("display", "none")
                ' lblcurrcode()
                hdSliderCurrency.Value = " " & lblwlCurrCode.Text
            Else
                lblwlunitprice.Style.Add("display", "none")
                txtwlunitprice.Style.Add("display", "none")
                txtwltotal.Style.Add("display", "none")

                txttotal.Style.Add("display", "block")
                lblunitprice.Style.Add("display", "block")
                txtunitprice.Style.Add("display", "block")
                hdSliderCurrency.Value = " " & lblCurrCode.Text
            End If
            If Session("sLoginType") = "RO" And chkTrfoverride.Checked = True Then
                divpaxprice.Style.Add("display", "block")
                divlbl.Style.Add("display", "none")
                'txtunitprice.Style.Add("display", "block")
                'lblunitprice.Style.Add("display", "none")
                'lblvaluetext.Visible = True
            Else
                divpaxprice.Style.Add("display", "none")
                divlbl.Style.Add("display", "block")
                'txtunitprice.Style.Add("display", "none")
                'lblunitprice.Style.Add("display", "block")
                'lblvaluetext.Visible = False

            End If
            ' txttotal.ReadOnly = True


            'If Val(lblbooknow.Text) = 0 Then
            '    chkbooknow.Checked = False
            'Else
            '    chkbooknow.Checked = True
            '    If Val(txtnoofunits.Text) = 0 Then
            '        txtnoofunits.Text = 1
            '    End If

            'End If

            '  chkbooknow.Attributes.Add("onChange", "calculatevalue('" & chkbooknow.ClientID & "','" & txtnoofunits.ClientID & "','" & txtunitprice.ClientID & "','" & txttotal.ClientID & "','" + CType(e.Item.ItemIndex, String) + "')")
            'txtnoofunits.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'txtunitprice.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'txtnoofunits.Attributes.Add("onChange", "javascript:calculatevalueWithWl('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            'txtwlunitprice.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'chkbooknow.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'chkarrcompliment.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")

            txtunitprice.Attributes.Add("onChange", "javascript:calculatevalueWithWl('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            txtnoofunits.Attributes.Add("onChange", "javascript:calculatevalueWithWl('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            txtwlunitprice.Attributes.Add("onChange", "javascript:calculatevalueWithWl('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            chkbooknow.Attributes.Add("onChange", "javascript:calculatevalueWithWl('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            chkarrcompliment.Attributes.Add("onChange", "javascript:calculatevalueWithWl('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")

            If iCumulative = 1 Then
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lblunitprice1 As Label = CType(e.Item.FindControl("lblunitprice"), Label)
                    Dim txtnoofunits1 As TextBox = CType(e.Item.FindControl("txtnoofunits"), TextBox)
                    Dim txttotal1 As TextBox = CType(e.Item.FindControl("txttotal"), TextBox)
                    Dim lbltotal1 As Label = CType(e.Item.FindControl("lbltotal"), Label)
                    Dim lblunitname As Label = CType(e.Item.FindControl("lblunitname"), Label)
                    Dim dvunit As HtmlGenericControl = CType(e.Item.FindControl("dvunit"), HtmlGenericControl)
                    Dim divtot As HtmlGenericControl = CType(e.Item.FindControl("divtot"), HtmlGenericControl)


                    'lblunitprice1.Visible = False

                    'txttotal1.Visible = False
                    'lbltotal1.Visible = False
                    'divtot.Visible = False

                    divtot.Style.Add("display", "none")
                    txttotal1.Style.Add("display", "none")
                    lbltotal1.Style.Add("display", "none")
                    lblunitprice1.Style.Add("display", "none")

                End If
            End If

        End If
    End Sub
    Private Sub Fn_ShowMore(ByVal sender As Object)
        Try
            iRatePlan = 0
            Dim myButton As Button = CType(sender, Button)
            If myButton.Text = "SHOW MORE" Then
                Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
                Dim dlRatePlan As DataList = CType(dlItem.FindControl("dlRatePlan"), DataList)
                Dim lblHotelCode As HiddenField = CType(dlItem.FindControl("lblHotelCode"), HiddenField)
                Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)
                Dim lblHotelName As Label = CType(dlItem.FindControl("lblHotelName"), Label)
                Dim lblCityName As Label = CType(dlItem.FindControl("lblCityName"), Label)
                Dim lblPrice As Label = CType(dlItem.FindControl("lblPrice"), Label)
                Dim lblHotelImage As Label = CType(dlItem.FindControl("lblHotelImage"), Label)

                myButton.Text = "SHOW LESS"
                BindRatePlan(dlRatePlan, lblHotelCode.Value)

                'Dim dtRecentlyViewedHotel As New DataTable
                'dtRecentlyViewedHotel = Session("sdtRecentlyViewedHotel")
                ''  If dtRecentlyViewedHotel.con
                'If dtRecentlyViewedHotel.Select("HotelCode = '" & lblHotelCode.Value.Trim & "'").Length = 0 Then
                '    Dim dr As DataRow = dtRecentlyViewedHotel.NewRow()
                '    dr("HotelCode") = lblHotelCode.Value.Trim
                '    dr("HotelName") = lblHotelName.Text
                '    dr("Location") = lblCityName.Text
                '    dr("Price") = lblPrice.Text
                '    dr("HotelImage") = lblHotelImage.Text ' "img/v-item-01.jpg"
                '    dtRecentlyViewedHotel.Rows.Add(dr)
                '    Session("sdtRecentlyViewedHotel") = dtRecentlyViewedHotel
                '    If dtRecentlyViewedHotel.Rows.Count > 0 Then
                '        ' For i As Integer = 0 To dtRecentlyViewedHotel.Rows.Count - 1
                '        ' Dim strScript As String = "javascript: WriteRecentlyViewedHotel('" & dtRecentlyViewedHotel.Rows(i)("HotelCode").ToString & "','" & dtRecentlyViewedHotel.Rows(i)("HotelName").ToString & "','" & dtRecentlyViewedHotel.Rows(i)("Location").ToString & "','" & dtRecentlyViewedHotel.Rows(i)("Price").ToString & "','" & dtRecentlyViewedHotel.Rows(i)("HotelImage").ToString & "');"
                '        Dim strScript As String = "javascript: WriteRecentlyViewedHotel('" & lblHotelCode.Value & "','" & lblHotelName.Text & "','" & lblCityName.Text & "','" & lblPrice.Text & "','" & lblHotelImage.Text & "');"
                '        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)
                '        ' Next
                '    End If
                'End If

            Else
                Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
                Dim dlRatePlan As DataList = CType(dlItem.FindControl("dlRatePlan"), DataList)
                myButton.Text = "SHOW MORE"
                BindRatePlanWithBlank(dlRatePlan)
            End If
            'Dim strScript1 As String = "javascript: CallPriceSlider();"
            'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript1, True)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: btnShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Fn_ShowMore(sender)
    End Sub
    Private Function Validatedetails() As String

        Dim arrivalflag As Boolean = False
        Dim depflag As Boolean = False
        Dim interflag As Boolean = False
        Dim Arrtransferflag As Boolean = False
        Dim Deptransferflag As Boolean = False
        Dim Intertransferflag As Boolean = False

        If chkarrival.Checked = True Then
            arrivalflag = True
        End If
        If chkDeparture.Checked = True Then
            depflag = True
        End If
        If chkinter.Checked = True Then
            interflag = True
        End If

        Dim dt As DataTable


        'For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

        '    If chkbooknow.Checked = True And arrivalflag = True Then
        '        Arrtransferflag = True
        '        Exit For
        '    End If

        'Next

        If Session("SelectedArrival") Is Nothing And arrivalflag = True Then
            Arrtransferflag = True
        End If



        'For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

        '    If chkbooknow.Checked = True And depflag = True Then
        '        Deptransferflag = True
        '        Exit For
        '    End If

        'Next

        If Session("SelectedDeparture") Is Nothing And Deptransferflag = True Then
            Deptransferflag = True
        End If


        'For Each gvRow As DataListItem In dlShifting.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

        '    If chkbooknow.Checked = True And interflag = True Then
        '        Intertransferflag = True
        '        Exit For
        '    End If

        'Next

        If Session("SelectedInter") Is Nothing And Intertransferflag = True Then
            Intertransferflag = True
        End If

        'If dlShifting.Items.Count = 0 Then
        '    interflag = False
        'End If

        ' End If



        If arrivalflag = True And Arrtransferflag = True Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Transfer Selected Please Select Arrival Vehicles")
            Return False
            Exit Function
        End If

        If depflag = True And Deptransferflag = True Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Transfer Selected Please Select Departure Vehicles")
            Return False
            Exit Function
        End If

        If interflag = True And Intertransferflag = True Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Transfer Selected Please Select InterHotel Vehicles")
            Return False
            Exit Function
        End If
        Dim sicadult As String = "", sicchild As String = ""
        Dim strQuery1 As String = ""
        strQuery1 = "select  stuff((select distinct ',' + option_selected from reservation_parameters  u(nolock) where param_id in (1149,1150)  for xml path('')),1,1,'' ) "
        sicadult = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)
        'strQuery1 = "select option_selected from reservation_parameters(nolock) where param_id=1150"
        'sicchild = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)


        Dim cartypecode As String = ""
        Dim siccount As Integer = 0

        If Not Session("SelectedArrival") Is Nothing Then

            dt = Session("SelectedArrival")

            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)("shuttle").ToString = 1 And Val(ddlTrfAdult.SelectedValue) > 0 And Val(ddlTrfChild.SelectedValue) > 0 Then
                    cartypecode = cartypecode + ";" + dt.Rows(i)("cartypecode").ToString
                    siccount = siccount + 1
                End If

                'Added by abin on 20190509 -- change requested by Mr.Vinilkumar
                Dim strSellingZeroAllowedSIC = "select count(*) from tblSICTransfer where isnull(othcatcode,'')='" & dt.Rows(i)("cartypecode").ToString & "' and ISNULL(FreeAllowed,0)=1"
                Dim iSellingZeroAllowedSIC As Integer = objclsUtilities.ExecuteQueryReturnStringValue(strSellingZeroAllowedSIC)
                If iSellingZeroAllowedSIC = 0 Then
                    Dim salevalue As Double = Math.Round(CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal))
                    If dt.Rows(i)("unitprice").ToString = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Vehicle Unit Price  Should not be zero")
                        Return False
                        Exit Function
                    End If
                    If Val(salevalue) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Vehicle Total Value Should not be zero")
                        Return False
                        Exit Function
                    End If
                End If

             
            Next

            If siccount < 2 And siccount <> 0 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
                Return False
                Exit Function
            End If

        End If



        'For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
        '    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
        '    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
        '    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
        '    Dim lblshuttle As Label = CType(gvRow.FindControl("lblshuttle"), Label)
        '    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

        '    If chkbooknow.Checked = True Then

        '        If lblshuttle.Text = 1 And Val(ddlTrfAdult.SelectedValue) > 0 And Val(ddlTrfChild.SelectedValue) > 0 Then
        '            cartypecode = cartypecode + ";" + lblcartypecode.Value
        '            siccount = siccount + 1
        '        End If

        '        Dim salevalue As Double = Math.Round(CType(Val(txtnoofunits.Text), Integer) * CType(txtunitprice.Text, Decimal))

        '        If Val(txtunitprice.Text) = 0 Then
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Vehicle Unit Price  Should not be zero")
        '            Return False
        '            Exit Function
        '        End If

        '        If Val(salevalue) = 0 Then
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Vehicle Total Value Should not be zero")
        '            Return False
        '            Exit Function
        '        End If
        '    End If

        'Next

        'If siccount < 2 And siccount <> 0 Then
        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
        '    Return False
        '    Exit Function
        'End If
        ''If cartypecode.Length > 0 Then
        ''    cartypecode = Right(cartypecode, Len(cartypecode) - 1)
        ''    Dim mString As String() = cartypecode.Split(";")
        ''    For i As Integer = 0 To mString.Length - 1
        ''        If cartypecode.Contains(mString(i)) = False Then
        ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
        ''            Return False
        ''            Exit Function
        ''        End If
        ''    Next

        ''End If
        cartypecode = ""
        Dim siccountdep As Integer = 0

        If Not Session("SelectedDeparture") Is Nothing Then

            dt = Session("SelectedDeparture")

            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)("shuttle").ToString = 1 And Val(ddlTrfAdult.SelectedValue) > 0 And Val(ddlTrfChild.SelectedValue) > 0 Then
                    cartypecode = cartypecode + ";" + dt.Rows(i)("cartypecode").ToString
                    siccountdep = siccountdep + 1
                End If

                Dim salevalue As Double = Math.Round(CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal))
                'Added by abin on 20190509 -- change requested by Mr.Vinilkumar
                Dim strSellingZeroAllowedSIC = "select count(*) from tblSICTransfer where isnull(othcatcode,'')='" & dt.Rows(i)("cartypecode").ToString & "' and ISNULL(FreeAllowed,0)=1"
                Dim iSellingZeroAllowedSIC As Integer = objclsUtilities.ExecuteQueryReturnStringValue(strSellingZeroAllowedSIC)
                If iSellingZeroAllowedSIC = 0 Then
                    If dt.Rows(i)("unitprice").ToString = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Vehicle Unit Price  Should not be zero")
                        Return False
                        Exit Function
                    End If
                    If Val(salevalue) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Vehicle Total Value Should not be zero")
                        Return False
                        Exit Function
                    End If
                End If
            Next

            If siccountdep < 2 And siccountdep <> 0 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
                Return False
                Exit Function
            End If

        End If

        'For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
        '    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
        '    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
        '    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
        '    Dim lblshuttle As Label = CType(gvRow.FindControl("lblshuttle"), Label)
        '    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

        '    If chkbooknow.Checked = True Then

        '        If lblshuttle.Text = 1 And Val(ddlTrfAdult.SelectedValue) > 0 And Val(ddlTrfChild.SelectedValue) > 0 Then
        '            cartypecode = cartypecode + ";" + lblcartypecode.Value
        '            siccountdep = siccountdep + 1
        '        End If

        '        Dim salevalue As Double = Math.Round(CType(Val(txtnoofunits.Text), Integer) * CType(txtunitprice.Text, Decimal))

        '        If Val(txtunitprice.Text) = 0 Then
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Vehicle Unit Price  Should not be zero")
        '            Return False
        '            Exit Function
        '        End If

        '        If Val(salevalue) = 0 Then
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Vehicle Total Value Should not be zero")
        '            Return False
        '            Exit Function
        '        End If
        '    End If

        'Next

        'If siccountdep < 2 And siccountdep <> 0 Then
        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
        '    Return False
        '    Exit Function
        'End If

        cartypecode = ""
        Dim siccountinter As Integer = 0

        If Not Session("SelectedInter") Is Nothing Then

            dt = Session("SelectedInter")

            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i)("shuttle").ToString = 1 And Val(ddlTrfAdult.SelectedValue) > 0 And Val(ddlTrfChild.SelectedValue) > 0 Then
                    cartypecode = cartypecode + ";" + dt.Rows(i)("cartypecode").ToString
                    siccountinter = siccountinter + 1
                End If

                Dim salevalue As Double = Math.Round(CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal))
                'Added by abin on 20190509 -- change requested by Mr.Vinilkumar
                Dim strSellingZeroAllowedSIC = "select count(*) from tblSICTransfer where isnull(othcatcode,'')='" & dt.Rows(i)("cartypecode").ToString & "' and ISNULL(FreeAllowed,0)=1"
                Dim iSellingZeroAllowedSIC As Integer = objclsUtilities.ExecuteQueryReturnStringValue(strSellingZeroAllowedSIC)
                If iSellingZeroAllowedSIC = 0 Then
                    If dt.Rows(i)("unitprice").ToString = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Vehicle Unit Price  Should not be zero")
                        Return False
                        Exit Function
                    End If
                    If Val(salevalue) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Vehicle Total Value Should not be zero")
                        Return False
                        Exit Function
                    End If
                End If
            Next

            If siccountinter < 2 And siccountinter <> 0 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
                Return False
                Exit Function
            End If

        End If




        'For Each gvRow As DataListItem In dlShifting.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
        '    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
        '    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
        '    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
        '    Dim lblshuttle As Label = CType(gvRow.FindControl("lblshuttle"), Label)
        '    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

        '    If chkbooknow.Checked = True Then

        '        If lblshuttle.Text = 1 And Val(ddlTrfAdult.SelectedValue) > 0 And Val(ddlTrfChild.SelectedValue) > 0 Then
        '            cartypecode = cartypecode + ";" + lblcartypecode.Value
        '            siccountdep = siccountdep + 1
        '        End If

        '        Dim salevalue As Double = Math.Round(CType(Val(txtnoofunits.Text), Integer) * CType(txtunitprice.Text, Decimal))

        '        If Val(txtunitprice.Text) = 0 Then
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Vehicle Unit Price  Should not be zero")
        '            Return False
        '            Exit Function
        '        End If

        '        If Val(salevalue) = 0 Then
        '            MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Vehicle Total Value Should not be zero")
        '            Return False
        '            Exit Function
        '        End If
        '    End If

        'Next

        'If siccountinter < 2 And siccountinter <> 0 Then
        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Inter Hotel Adult & Child Selected So You Need select SIC ADULT and CHILD vechicle category")
        '    Return False
        '    Exit Function
        'End If


        Return True
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
    Private Shared Function GetExistingRequestId() As String
        Dim strRequestId As String = ""
        If HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = ""
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            strRequestId = HttpContext.Current.Session("sRequestId")
            '  strRequestId = objBLLCommonFuntions.GetExistingBookingRequestId(strRequestId)
        End If
        Return strRequestId
    End Function

    Protected Sub btnBookNow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim strBuffer As New Text.StringBuilder
            Dim strFliBuffer As New Text.StringBuilder
            Dim requestid As String = ""
            Dim sourcectrycode As String = ""
            Dim agentcode As String = ""
            Dim agentref As String = ""
            Dim columbusref As String = ""
            Dim remarks As String = ""
            Dim strTlineno As String = ""
            requestid = GetNewOrExistingRequestId()


            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable



            If Validatedetails() Then




                If Session("State") = "New" Then

                    If Not Session("sAgentCode") Is Nothing Then
                        objBLLTransferSearch = New BLLTransferSearch
                        objBLLTransferSearch = CType(Session("sobjBLLTransferSearch"), BLLTransferSearch)

                        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
                        If dt.Rows.Count > 0 Then
                            objBLLTransferSearch.AgentCode = dt.Rows(0)("agentcode").ToString
                            objBLLTransferSearch.OBDiv_Code = dt.Rows(0)("div_code").ToString
                            objBLLTransferSearch.OBRequestId = requestid
                            objBLLTransferSearch.OBSourcectryCode = dt.Rows(0)("sourcectrycode").ToString
                            objBLLTransferSearch.OBAgentCode = IIf(Session("sLoginType") = "RO", objBLLTransferSearch.TrfCustomerCode, dt.Rows(0)("agentcode").ToString)

                            objBLLTransferSearch.OBReqoverRide = IIf(chkTrfoverride.Checked = True, "1", "0")
                            objBLLTransferSearch.OBAgentref = dt.Rows(0)("agentref").ToString
                            objBLLTransferSearch.OBColumbusRef = dt.Rows(0)("ColumbusRef").ToString
                            objBLLTransferSearch.OBRemarks = dt.Rows(0)("remarks").ToString
                            objBLLTransferSearch.UserLogged = Session("GlobalUserName")
                        Else
                            objBLLTransferSearch.AgentCode = Session("sAgentCode")
                            objBLLTransferSearch.OBDiv_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
                            objBLLTransferSearch.OBRequestId = requestid ' IIf(requestid = "", objBLLHotelSearch.getrequestid(), requestid)
                            objBLLTransferSearch.OBSourcectryCode = objBLLTransferSearch.TrfSourceCountryCode


                            objBLLTransferSearch.OBAgentCode = IIf(Session("sLoginType") = "RO", objBLLTransferSearch.TrfCustomerCode, Session("sAgentCode"))

                            objBLLTransferSearch.OBReqoverRide = IIf(chkTrfoverride.Checked = True, "1", "0")
                            objBLLTransferSearch.OBAgentref = ""
                            objBLLTransferSearch.OBColumbusRef = ""
                            objBLLTransferSearch.OBRemarks = remarks
                            objBLLTransferSearch.UserLogged = Session("GlobalUserName")

                        End If







                        Dim btnbooknow As Button = CType(sender, Button)
                        Dim rowid As Integer = 0
                        Dim rlineonostring As String = ""

                        ''' Arrival  Start
                        If Not Session("SelectedArrival") Is Nothing Then

                            dt = Session("SelectedArrival")

                            For i = 0 To dt.Rows.Count - 1

                                Dim strQuery As String = ""
                                Dim flighttranid As String = ""
                                strQuery = "select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(txtArrivalflight.Text, String) & "'"
                                flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)


                                If ViewState("Tlineno") Is Nothing Then

                                    If strTlineno = "" Then
                                        strTlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLTransferSearch.OBRequestId, "TRANSFER")
                                    Else
                                        strTlineno = CType(strTlineno, Integer) + 1
                                    End If

                                Else
                                    If strTlineno = "" Then
                                        strTlineno = ViewState("Tlineno")
                                    Else
                                        strTlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLTransferSearch.OBRequestId, "TRANSFER")
                                    End If
                                End If
                                If rlineonostring <> "" Then
                                    rlineonostring = rlineonostring + ";" + CType(strTlineno, String)
                                Else
                                    rlineonostring = strTlineno
                                End If

                                Dim lineno As Integer = strTlineno ' CType(Session("tlineno"), Integer)

                                Dim salevalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal) '(CType(Val(txtnoofunits.Text), Integer) * CType(txtunitprice.Text, Decimal))
                                Dim costvalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitcprice").ToString, Decimal) ''' added shahul 07/06/2018

                                If dt.Rows(i)("ComplimentaryCust").ToString = "1" Then
                                    salevalue = 0
                                End If

                                strBuffer.Append("<DocumentElement><Table>")

                                strBuffer.Append(" <tlineno>" & strTlineno & "</tlineno>")

                                strBuffer.Append(" <transfertype>ARRIVAL</transfertype>")
                                strBuffer.Append(" <airportbordercode>" & CType(dt.Rows(i)("airportbordercode").ToString, String) & "</airportbordercode>")
                                strBuffer.Append(" <sectorgroupcode>" & CType(dt.Rows(i)("sectorgroupcode").ToString, String) & "</sectorgroupcode>")
                                strBuffer.Append(" <cartypecode>" & CType(dt.Rows(i)("cartypecode").ToString, String) & "</cartypecode>")
                                strBuffer.Append(" <shuttle>" & CType(dt.Rows(i)("shuttle").ToString, Integer) & "</shuttle>")


                                Dim strFromDate As String = dt.Rows(i)("transferdate").ToString
                                If strFromDate <> "" Then
                                    Dim strDates As String() = strFromDate.Split("/")
                                    strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                End If




                                strBuffer.Append(" <transferdate>" & Format(CType(dt.Rows(i)("transferdate").ToString, Date), "yyyy/MM/dd") & "</transferdate>")
                                strBuffer.Append(" <flightcode>" & CType(txtArrivalflight.Text, String) & "</flightcode>")
                                strBuffer.Append(" <flight_tranid>" & CType(flighttranid, String) & "</flight_tranid>")
                                strBuffer.Append(" <flighttime>" & CType(txtArrivalTime.Text, String) & "</flighttime>")
                                strBuffer.Append(" <pickup>" & CType(txtTrfArrivalpickupcode.Text, String) & "</pickup>")
                                strBuffer.Append(" <dropoff>" & CType(txtTrfArrDropoffcode.Text, String) & "</dropoff>")

                                strBuffer.Append(" <adults>" & CType(dt.Rows(i)("adults").ToString, String) & "</adults>")
                                strBuffer.Append(" <child>" & CType(dt.Rows(i)("child").ToString, String) & "</child>")
                                strBuffer.Append(" <childagestring>" & CType(objBLLTransferSearch.ChildAgeString, String) & "</childagestring>")
                                strBuffer.Append(" <units>" & CType(dt.Rows(i)("units").ToString, String) & "</units>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <tplistcode>" & CType(dt.Rows(i)("tplistcode").ToString, String) & "</tplistcode>")
                                strBuffer.Append(" <complimentarycust>" & CType(dt.Rows(i)("ComplimentaryCust").ToString, String) & "</complimentarycust>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <overrideprice>" & IIf(chkTrfoverride.Checked = True, 1, 0) & "</overrideprice>")
                                strBuffer.Append(" <flightclass>" & CType(ddlTrfArrFlightClass.SelectedValue, String) & "</flightclass>")

                                '  Dim wlsalevalue As Double = (CType(Val(txtnoofunits.Text), Integer) * CType(txtwlunitprice.Text, Decimal))

                                Dim dUnitprice As Decimal = IIf(CType(dt.Rows(i)("unitprice").ToString, String) = "", "0.00", CType(dt.Rows(i)("unitprice").ToString, Decimal))
                                Dim dwlUnitprice As Decimal
                                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(CType(dt.Rows(i)("wlmarkupperc").ToString, Decimal))) / 100) * Convert.ToDecimal(CType(dt.Rows(i)("wlconvrate").ToString, Decimal))
                                dwlUnitprice = dUnitprice * dWlMarkup

                                dt.Rows(i)("wlunitprice") = Math.Round(Val(dwlUnitprice), 2)
                                dt.Rows(i)("wlunitsalevalue") = Math.Round(Val(CType(dt.Rows(i)("units").ToString, String)) * Val(dwlUnitprice))

                                strBuffer.Append(" <wlunitprice>" & CType(dt.Rows(i)("wlunitprice").ToString, String) & "</wlunitprice>")
                                strBuffer.Append(" <wlunitsalevalue>" & CType(dt.Rows(i)("wlunitsalevalue").ToString, String) & "</wlunitsalevalue>")
                                strBuffer.Append(" <preferredsupplier>" & CType(dt.Rows(i)("preferredsupplier").ToString, String) & "</preferredsupplier>")
                                strBuffer.Append(" <unitcprice>" & CType(dt.Rows(i)("unitcprice").ToString, String) & "</unitcprice>")
                                strBuffer.Append(" <unitcostvalue>" & CType(costvalue, String) & "</unitcostvalue>")
                                strBuffer.Append(" <tcplistcode>" & CType(dt.Rows(i)("tcplistcode").ToString, String) & "</tcplistcode>")
                                strBuffer.Append(" <wlcurrcode>" & CType(dt.Rows(i)("wlcurrcode").ToString, String) & "</wlcurrcode>")
                                strBuffer.Append(" <wlconvrate>" & CType(dt.Rows(i)("wlconvrate").ToString, String) & "</wlconvrate>")
                                strBuffer.Append(" <wlmarkupperc>" & CType(dt.Rows(i)("wlmarkupperc").ToString, String) & "</wlmarkupperc>")

                                strBuffer.Append(" <CostTaxableValue>" & CType(dt.Rows(i)("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                                strBuffer.Append(" <CostVATValue>" & CType(dt.Rows(i)("CostVATValue").ToString, String) & "</CostVATValue>")
                                strBuffer.Append(" <VATPer>" & CType(dt.Rows(i)("VATPer").ToString, String) & "</VATPer>")
                                strBuffer.Append(" <PriceWithTAX>" & CType(dt.Rows(i)("PriceWithTAX").ToString, String) & "</PriceWithTAX>")
                                strBuffer.Append(" <PriceTaxableValue>" & CType(dt.Rows(i)("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                                strBuffer.Append(" <PriceVATValue>" & CType(dt.Rows(i)("PriceVATValue").ToString, String) & "</PriceVATValue>")
                                strBuffer.Append(" <PriceVATPer>" & CType(dt.Rows(i)("PriceVATPer").ToString, String) & "</PriceVATPer>")
                                strBuffer.Append(" <PriceWithTAX1>" & CType(dt.Rows(i)("PriceWithTAX1").ToString, String) & "</PriceWithTAX1>")
                                ''  Added shahul 21/07/2018
                                strBuffer.Append(" <Pickupcodetype></Pickupcodetype>")
                                strBuffer.Append(" <Dropoffcodetype>" & CType(txtTrfArrDroptype.Text, String) & "</Dropoffcodetype>")


                                strBuffer.Append("</Table></DocumentElement>")
                                'If Not ViewState("Tlineno") Is Nothing Then
                                '    Exit For
                                'End If



                            Next
                        End If
                        ''' Arrival End
                        ''' 
                        ''''''''''''''''''''''''''''''''''''''''

                        ''''''''''''''''''''''''''''''''''''''''''''
                        ''' Departure  Start
                        If Not Session("SelectedDeparture") Is Nothing Then

                            dt = Session("SelectedDeparture")




                            For i = 0 To dt.Rows.Count - 1


                                Dim strQuery As String = ""
                                Dim flighttranid As String = ""
                                strQuery = "select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(txtDepartureFlight.Text, String) & "'"
                                flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)


                                If ViewState("Tlineno") Is Nothing Then

                                    If strTlineno = "" Then
                                        strTlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLTransferSearch.OBRequestId, "TRANSFER")
                                    Else
                                        strTlineno = CType(strTlineno, Integer) + 1
                                    End If

                                Else
                                    If strTlineno = "" Then
                                        strTlineno = ViewState("Tlineno")
                                    Else
                                        strTlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLTransferSearch.OBRequestId, "TRANSFER")
                                    End If
                                End If
                                If rlineonostring <> "" Then
                                    rlineonostring = rlineonostring + ";" + CType(strTlineno, String)
                                Else
                                    rlineonostring = strTlineno
                                End If

                                Dim lineno As Integer = strTlineno ' CType(Session("tlineno"), Integer)

                                Dim salevalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal) '(CType(Val(txtnoofunits.Text), Integer) * CType(txtunitprice.Text, Decimal))
                                Dim costvalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitcprice").ToString, Decimal) ''' added shahul 07/06/2018

                                strBuffer.Append("<DocumentElement><Table>")

                                strBuffer.Append(" <tlineno>" & strTlineno & "</tlineno>")

                                strBuffer.Append(" <transfertype>DEPARTURE</transfertype>")
                                strBuffer.Append(" <airportbordercode>" & CType(dt.Rows(i)("airportbordercode").ToString, String) & "</airportbordercode>")
                                strBuffer.Append(" <sectorgroupcode>" & CType(dt.Rows(i)("sectorgroupcode").ToString, String) & "</sectorgroupcode>")
                                strBuffer.Append(" <cartypecode>" & CType(dt.Rows(i)("cartypecode").ToString, String) & "</cartypecode>")
                                strBuffer.Append(" <shuttle>" & CType(dt.Rows(i)("shuttle").ToString, Integer) & "</shuttle>")


                                Dim strFromDate As String = dt.Rows(i)("transferdate").ToString
                                If strFromDate <> "" Then
                                    Dim strDates As String() = strFromDate.Split("/")
                                    strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                End If




                                strBuffer.Append(" <transferdate>" & Format(CType(dt.Rows(i)("transferdate").ToString, Date), "yyyy/MM/dd") & "</transferdate>")
                                strBuffer.Append(" <flightcode>" & CType(txtDepartureFlight.Text, String) & "</flightcode>")
                                strBuffer.Append(" <flight_tranid>" & CType(flighttranid, String) & "</flight_tranid>")
                                strBuffer.Append(" <flighttime>" & CType(txtDepartureTime.Text, String) & "</flighttime>")
                                strBuffer.Append(" <pickup>" & CType(txtTrfDeppickupcode.Text, String) & "</pickup>")
                                strBuffer.Append(" <dropoff>" & CType(txtTrfDepairportdropcode.Text, String) & "</dropoff>")

                                strBuffer.Append(" <adults>" & CType(dt.Rows(i)("adults").ToString, String) & "</adults>")
                                strBuffer.Append(" <child>" & CType(dt.Rows(i)("child").ToString, String) & "</child>")
                                strBuffer.Append(" <childagestring>" & CType(objBLLTransferSearch.ChildAgeString, String) & "</childagestring>")
                                strBuffer.Append(" <units>" & CType(dt.Rows(i)("units").ToString, String) & "</units>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <tplistcode>" & CType(dt.Rows(i)("tplistcode").ToString, String) & "</tplistcode>")
                                strBuffer.Append(" <complimentarycust>" & CType(dt.Rows(i)("ComplimentaryCust").ToString, String) & "</complimentarycust>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <overrideprice>" & IIf(chkTrfoverride.Checked = True, 1, 0) & "</overrideprice>")
                                strBuffer.Append(" <flightclass>" & CType(ddlTrfDepFlightClass.SelectedValue, String) & "</flightclass>")

                                '  Dim wlsalevalue As Double = (CType(Val(txtnoofunits.Text), Integer) * CType(txtwlunitprice.Text, Decimal))

                                Dim dUnitprice As Decimal = IIf(CType(dt.Rows(i)("unitprice").ToString, String) = "", "0.00", CType(dt.Rows(i)("unitprice").ToString, Decimal))
                                Dim dwlUnitprice As Decimal
                                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(CType(dt.Rows(i)("wlmarkupperc").ToString, Decimal))) / 100) * Convert.ToDecimal(CType(dt.Rows(i)("wlconvrate").ToString, Decimal))
                                dwlUnitprice = dUnitprice * dWlMarkup

                                dt.Rows(i)("wlunitprice") = Math.Round(Val(dwlUnitprice), 2)
                                dt.Rows(i)("wlunitsalevalue") = Math.Round(Val(CType(dt.Rows(i)("units").ToString, String)) * Val(dwlUnitprice))

                                strBuffer.Append(" <wlunitprice>" & CType(dt.Rows(i)("wlunitprice").ToString, String) & "</wlunitprice>")
                                strBuffer.Append(" <wlunitsalevalue>" & CType(dt.Rows(i)("wlunitsalevalue").ToString, String) & "</wlunitsalevalue>")
                                strBuffer.Append(" <preferredsupplier>" & CType(dt.Rows(i)("preferredsupplier").ToString, String) & "</preferredsupplier>")
                                strBuffer.Append(" <unitcprice>" & CType(dt.Rows(i)("unitcprice").ToString, String) & "</unitcprice>")
                                strBuffer.Append(" <unitcostvalue>" & CType(costvalue, String) & "</unitcostvalue>") '' Added shaul 0/06/2018
                                strBuffer.Append(" <tcplistcode>" & CType(dt.Rows(i)("tcplistcode").ToString, String) & "</tcplistcode>")
                                strBuffer.Append(" <wlcurrcode>" & CType(dt.Rows(i)("wlcurrcode").ToString, String) & "</wlcurrcode>")
                                strBuffer.Append(" <wlconvrate>" & CType(dt.Rows(i)("wlconvrate").ToString, String) & "</wlconvrate>")
                                strBuffer.Append(" <wlmarkupperc>" & CType(dt.Rows(i)("wlmarkupperc").ToString, String) & "</wlmarkupperc>")


                                strBuffer.Append(" <CostTaxableValue>" & CType(dt.Rows(i)("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                                strBuffer.Append(" <CostVATValue>" & CType(dt.Rows(i)("CostVATValue").ToString, String) & "</CostVATValue>")
                                strBuffer.Append(" <VATPer>" & CType(dt.Rows(i)("VATPer").ToString, String) & "</VATPer>")
                                strBuffer.Append(" <PriceWithTAX>" & CType(dt.Rows(i)("PriceWithTAX").ToString, String) & "</PriceWithTAX>")
                                strBuffer.Append(" <PriceTaxableValue>" & CType(dt.Rows(i)("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                                strBuffer.Append(" <PriceVATValue>" & CType(dt.Rows(i)("PriceVATValue").ToString, String) & "</PriceVATValue>")
                                strBuffer.Append(" <PriceVATPer>" & CType(dt.Rows(i)("PriceVATPer").ToString, String) & "</PriceVATPer>")
                                strBuffer.Append(" <PriceWithTAX1>" & CType(dt.Rows(i)("PriceWithTAX1").ToString, String) & "</PriceWithTAX1>")
                                ''  Added shahul 21/07/2018
                                strBuffer.Append(" <Pickupcodetype>" & CType(txtTrfDeppickuptype.Text, String) & "</Pickupcodetype>")
                                strBuffer.Append(" <Dropoffcodetype></Dropoffcodetype>")


                                strBuffer.Append("</Table></DocumentElement>")
                                'If Not ViewState("Tlineno") Is Nothing Then
                                '    Exit For
                                'End If



                            Next
                        End If
                        ''' Departure End




                        ''' Shifting  Start

                        If Not Session("SelectedInter") Is Nothing Then

                            dt = Session("SelectedInter")

                            For i = 0 To dt.Rows.Count - 1

                                If ViewState("Tlineno") Is Nothing Then

                                    If strTlineno = "" Then
                                        strTlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLTransferSearch.OBRequestId, "TRANSFER")
                                    Else
                                        strTlineno = CType(strTlineno, Integer) + 1
                                    End If

                                Else
                                    If strTlineno = "" Then
                                        strTlineno = ViewState("Tlineno")
                                    Else
                                        strTlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLTransferSearch.OBRequestId, "TRANSFER")
                                    End If
                                End If
                                If rlineonostring <> "" Then
                                    rlineonostring = rlineonostring + ";" + CType(strTlineno, String)
                                Else
                                    rlineonostring = strTlineno
                                End If

                                Dim strQuery As String = ""
                                Dim flighttranid As String = ""

                                flighttranid = ""

                                Dim lineno As Integer = strTlineno ' CType(Session("tlineno"), Integer)

                                Dim salevalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal) '(CType(Val(txtnoofunits.Text), Integer) * CType(txtunitprice.Text, Decimal))
                                Dim costvalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitcprice").ToString, Decimal) ''' added shahul 07/06/2018

                                strBuffer.Append("<DocumentElement><Table>")

                                strBuffer.Append(" <tlineno>" & strTlineno & "</tlineno>")

                                strBuffer.Append(" <transfertype>INTERHOTEL</transfertype>")
                                strBuffer.Append(" <airportbordercode>" & CType(dt.Rows(i)("airportbordercode").ToString, String) & "</airportbordercode>")
                                strBuffer.Append(" <sectorgroupcode>" & CType(dt.Rows(i)("sectorgroupcode").ToString, String) & "</sectorgroupcode>")
                                strBuffer.Append(" <cartypecode>" & CType(dt.Rows(i)("cartypecode").ToString, String) & "</cartypecode>")
                                strBuffer.Append(" <shuttle>" & CType(dt.Rows(i)("shuttle").ToString, Integer) & "</shuttle>")


                                Dim strFromDate As String = dt.Rows(i)("transferdate").ToString
                                If strFromDate <> "" Then
                                    Dim strDates As String() = strFromDate.Split("/")
                                    strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                                End If




                                strBuffer.Append(" <transferdate>" & Format(CType(dt.Rows(i)("transferdate").ToString, Date), "yyyy/MM/dd") & "</transferdate>")
                                strBuffer.Append(" <flightcode></flightcode>")
                                strBuffer.Append(" <flight_tranid></flight_tranid>")
                                strBuffer.Append(" <flighttime></flighttime>")
                                strBuffer.Append(" <pickup>" & CType(txtTrfinterPickupcode.Text, String) & "</pickup>")
                                strBuffer.Append(" <dropoff>" & CType(txtTrfInterdropffcode.Text, String) & "</dropoff>")

                                strBuffer.Append(" <adults>" & CType(dt.Rows(i)("adults").ToString, String) & "</adults>")
                                strBuffer.Append(" <child>" & CType(dt.Rows(i)("child").ToString, String) & "</child>")
                                strBuffer.Append(" <childagestring>" & CType(objBLLTransferSearch.ChildAgeString, String) & "</childagestring>")
                                strBuffer.Append(" <units>" & CType(dt.Rows(i)("units").ToString, String) & "</units>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <tplistcode>" & CType(dt.Rows(i)("tplistcode").ToString, String) & "</tplistcode>")
                                strBuffer.Append(" <complimentarycust>" & CType(dt.Rows(i)("ComplimentaryCust").ToString, String) & "</complimentarycust>")
                                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                                strBuffer.Append(" <overrideprice>" & IIf(chkTrfoverride.Checked = True, 1, 0) & "</overrideprice>")
                                strBuffer.Append(" <flightclass></flightclass>")

                                '  Dim wlsalevalue As Double = (CType(Val(txtnoofunits.Text), Integer) * CType(txtwlunitprice.Text, Decimal))

                                Dim dUnitprice As Decimal = IIf(CType(dt.Rows(i)("unitprice").ToString, String) = "", "0.00", CType(dt.Rows(i)("unitprice").ToString, Decimal))
                                Dim dwlUnitprice As Decimal
                                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(CType(dt.Rows(i)("wlmarkupperc").ToString, Decimal))) / 100) * Convert.ToDecimal(CType(dt.Rows(i)("wlconvrate").ToString, Decimal))
                                dwlUnitprice = dUnitprice * dWlMarkup

                                dt.Rows(i)("wlunitprice") = Math.Round(Val(dwlUnitprice), 2)
                                dt.Rows(i)("wlunitsalevalue") = Math.Round(Val(CType(dt.Rows(i)("units").ToString, String)) * Val(dwlUnitprice))

                                strBuffer.Append(" <wlunitprice>" & CType(dt.Rows(i)("wlunitprice").ToString, String) & "</wlunitprice>")
                                strBuffer.Append(" <wlunitsalevalue>" & CType(dt.Rows(i)("wlunitsalevalue").ToString, String) & "</wlunitsalevalue>")
                                strBuffer.Append(" <preferredsupplier>" & CType(dt.Rows(i)("preferredsupplier").ToString, String) & "</preferredsupplier>")
                                strBuffer.Append(" <unitcprice>" & CType(dt.Rows(i)("unitcprice").ToString, String) & "</unitcprice>")
                                strBuffer.Append(" <unitcostvalue>" & CType(costvalue, String) & "</unitcostvalue>") '' added shahul 07/06/18
                                strBuffer.Append(" <tcplistcode>" & CType(dt.Rows(i)("tcplistcode").ToString, String) & "</tcplistcode>")
                                strBuffer.Append(" <wlcurrcode>" & CType(dt.Rows(i)("wlcurrcode").ToString, String) & "</wlcurrcode>")
                                strBuffer.Append(" <wlconvrate>" & CType(dt.Rows(i)("wlconvrate").ToString, String) & "</wlconvrate>")
                                strBuffer.Append(" <wlmarkupperc>" & CType(dt.Rows(i)("wlmarkupperc").ToString, String) & "</wlmarkupperc>")

                                strBuffer.Append(" <CostTaxableValue>" & CType(dt.Rows(i)("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                                strBuffer.Append(" <CostVATValue>" & CType(dt.Rows(i)("CostVATValue").ToString, String) & "</CostVATValue>")
                                strBuffer.Append(" <VATPer>" & CType(dt.Rows(i)("VATPer").ToString, String) & "</VATPer>")
                                strBuffer.Append(" <PriceWithTAX>" & CType(dt.Rows(i)("PriceWithTAX").ToString, String) & "</PriceWithTAX>")
                                strBuffer.Append(" <PriceTaxableValue>" & CType(dt.Rows(i)("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                                strBuffer.Append(" <PriceVATValue>" & CType(dt.Rows(i)("PriceVATValue").ToString, String) & "</PriceVATValue>")
                                strBuffer.Append(" <PriceVATPer>" & CType(dt.Rows(i)("PriceVATPer").ToString, String) & "</PriceVATPer>")
                                strBuffer.Append(" <PriceWithTAX1>" & CType(dt.Rows(i)("PriceWithTAX1").ToString, String) & "</PriceWithTAX1>")
                                ''  Added shahul 21/07/2018
                                strBuffer.Append(" <Pickupcodetype>" & CType(txtTrfinterPickuptype.Text, String) & "</Pickupcodetype>")
                                strBuffer.Append(" <Dropoffcodetype>" & CType(txtTrfInterdropfftype.Text, String) & "</Dropoffcodetype>")

                                strBuffer.Append("</Table></DocumentElement>")
                                'If Not ViewState("Tlineno") Is Nothing Then
                                '    Exit For
                                'End If



                            Next
                        End If

                        ''' End Shifting



                        objBLLTransferSearch.OBRlinenoString = rlineonostring

                        objBLLTransferSearch.OBTransferXml = strBuffer.ToString

                        If Not Session("sobjResParam") Is Nothing Then
                            objResParam = Session("sobjResParam")
                            objBLLTransferSearch.SubUserCode = objResParam.SubUserCode
                        End If
                        If objBLLTransferSearch.SavingTransferBookingtemp() Then
                            Session("sRequestId") = objBLLTransferSearch.OBRequestId
                            Session("sobjBLLTransferSearchActive") = Session("sobjBLLTransferSearch")
                            '   Response.Redirect("~\GuestPage.aspx")
                            Response.Redirect("~\MoreServices.aspx")
                        End If

                    End If
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: btnBookNow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Dim ds As DataSet
    Protected Sub gvHotelRoomType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If hdBookingEngineRateType.Value = "1" Then
                If (e.Row.RowType = DataControlRowType.Header) Then
                    e.Row.Cells(3).Visible = False
                End If
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                ' Dim lblTotalPrice As Label = CType(e.Row.FindControl("lblTotalPrice"), Label)
                ' Dim txtTotalPrice As TextBox = CType(e.Row.FindControl("txtTotalPrice"), TextBox)

                Dim lbTotalprice As LinkButton = CType(e.Row.FindControl("lbTotalprice"), LinkButton)
                Dim lblAvailable As Label = CType(e.Row.FindControl("lblAvailable"), Label)
                Dim btnBookNow As Button = CType(e.Row.FindControl("btnBookNow"), Button)
                If lblAvailable.Text = 1 Then
                    btnBookNow.Text = "Book Now"
                    btnBookNow.CssClass = "roomtype-buttons-booknow"
                Else
                    btnBookNow.Text = "On Request"
                    btnBookNow.CssClass = "roomtype-buttons-onrequest"
                End If
                If Not Session("sLoginType") Is Nothing Then
                    If Session("sLoginType") <> "RO" Then

                        If hdBookingEngineRateType.Value = "1" Then
                            lbTotalprice.Visible = False

                        Else
                            lbTotalprice.Visible = True
                        End If
                    Else
                        lbTotalprice.Visible = True
                    End If
                Else
                    lbTotalprice.Visible = True
                End If

                Dim lblRMRatePlanName As Label = CType(e.Row.FindControl("lblRMRatePlanName"), Label)
                Dim lbOffer As LinkButton = CType(e.Row.FindControl("lbOffer"), LinkButton)
                If lblRMRatePlanName.Text = "Contract" Then
                    Dim spanOffer As HtmlGenericControl = CType(e.Row.FindControl("spanOffer"), HtmlGenericControl)
                    spanOffer.Attributes.Add("class", "")
                    spanOffer.Attributes.Add("class", "roomtype-icon-offer-inactive")

                    lbOffer.Enabled = False
                End If


                'spanOffer
                '

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: gvHotelRoomType_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbRatePlan_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
            Dim hdShow As HiddenField = CType(dlItem.FindControl("hdShow"), HiddenField)
            Dim hdRatePlanCode As HiddenField = CType(dlItem.FindControl("hdRatePlanCode"), HiddenField)
            Dim hdRatePlanHotelCode As HiddenField = CType(dlItem.FindControl("hdRatePlanHotelCode"), HiddenField)
            If hdShow.Value.ToUpper = "SHOW" Then
                hdShow.Value = "HIDE"
                Dim hdRatePlan As HiddenField = CType(dlItem.FindControl("hdRatePlan"), HiddenField)
                myLinkButton.Text = "Hide Rate Plan: " & hdRatePlan.Value
                Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)
                BindgvRoomType(gvHotelRoomType, hdRatePlanCode.Value, hdRatePlanHotelCode.Value)
            Else
                hdShow.Value = "SHOW"
                Dim hdRatePlan As HiddenField = CType(dlItem.FindControl("hdRatePlan"), HiddenField)
                myLinkButton.Text = "Show Rate Plan: " & hdRatePlan.Value
                Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)
                BindgvRoomTypeWithBlank(gvHotelRoomType)
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: lbRatePlan_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Protected Sub dlRatePlan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If iRatePlan = 0 Then
                    iRatePlan = 1
                    Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbRatePlan"), LinkButton)
                    Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
                    Dim hdShow As HiddenField = CType(dlItem.FindControl("hdShow"), HiddenField)
                    hdShow.Value = "HIDE"
                    Dim hdRatePlan As HiddenField = CType(dlItem.FindControl("hdRatePlan"), HiddenField)
                    Dim hdRatePlanCode As HiddenField = CType(dlItem.FindControl("hdRatePlanCode"), HiddenField)
                    Dim hdRatePlanHotelCode As HiddenField = CType(dlItem.FindControl("hdRatePlanHotelCode"), HiddenField)
                    myLinkButton.Text = "Hide Rate Plan: " & hdRatePlan.Value
                    Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)
                    BindgvRoomType(gvHotelRoomType, hdRatePlanCode.Value, hdRatePlanHotelCode.Value)
                Else
                    Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbRatePlan"), LinkButton)
                    Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
                    Dim hdRatePlan As HiddenField = CType(dlItem.FindControl("hdRatePlan"), HiddenField)
                    myLinkButton.Text = "Show Rate Plan: " & hdRatePlan.Value
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: dlRatePlan_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub Transfersearch()
        Try

            Dim objBLLTransferSearch As New BLLTransferSearch

            Dim strArrivaldate As String = txtTrfArrivaldate.Text
            Dim strDeparturedate As String = txtTrfDeparturedate.Text

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
            Dim chksector As String = ""

            Session("SelectedArrival") = Nothing
            Session("SelectedDeparture") = Nothing
            Session("SelectedInter") = Nothing



            Dim strQueryString As String = ""

            Dim strSearchCriteria As String = ""

            If Not Session("sEditRequestId") Is Nothing Then
                objBLLTransferSearch.AmendRequestid = Session("sEditRequestId")
                objBLLTransferSearch.AmendLineno = ViewState("Tlineno")
            Else
                objBLLTransferSearch.AmendRequestid = GetExistingRequestId()
                objBLLTransferSearch.AmendLineno = ViewState("Tlineno")
            End If


            '' Arrival
            If chkarrival.Checked = True Then
                objBLLTransferSearch.ArrTransferType = "ARRIVAL"
                strSearchCriteria = "ARRIVAL:Yes"
            Else
                strSearchCriteria = "ARRIVAL:No"

            End If

            If txtTrfArrivaldate.Text <> "" Then
                objBLLTransferSearch.ArrTransferDate = txtTrfArrivaldate.Text
                strSearchCriteria = strSearchCriteria & "||" & "Arrivaldate:" & txtTrfArrivaldate.Text
            End If

            If txtArrivalflight.Text <> "" Then
                objBLLTransferSearch.ArrFlightNo = txtArrivalflight.Text
                strSearchCriteria = strSearchCriteria & "||" & "ArrFlightNo:" & txtArrivalflight.Text
            End If
            If ddlTrfArrFlightClass.SelectedIndex <> 0 And chkarrival.Checked = True Then
                objBLLTransferSearch.ArrFlightClass = ddlTrfArrFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & "ArrFlightClass:" & ddlTrfArrFlightClass.Text
            End If
            If txtArrivalTime.Text <> "" Then
                objBLLTransferSearch.ArrFlightTime = txtArrivalTime.Text
                strSearchCriteria = strSearchCriteria & "||" & "ArrFlightTime:" & txtArrivalTime.Text
            End If
            If txtTrfArrivalpickupcode.Text <> "" Then
                objBLLTransferSearch.ArrPickupCode = txtTrfArrivalpickupcode.Text
            End If

            If txtTrfArrivalpickup.Text <> "" Then
                objBLLTransferSearch.ArrPickupName = txtTrfArrivalpickup.Text
                strSearchCriteria = strSearchCriteria & "||" & "ArrPickupName:" & txtTrfArrivalpickup.Text
            End If

            If txtTrfArrDropoffcode.Text <> "" Then
                objBLLTransferSearch.ArrDropCode = txtTrfArrDropoffcode.Text
                objBLLTransferSearch.ArrDroptype = txtTrfArrDroptype.Text   '' Added shahul 19/07/18
            End If

            If txtTrfArrDropoff.Text <> "" Then
                objBLLTransferSearch.ArrDropName = txtTrfArrDropoff.Text
                strSearchCriteria = strSearchCriteria & "||" & "ArrDropName:" & txtTrfArrDropoff.Text
            End If

            Dim strQuery As String = ""
            '' Added shahul 19/07/18
            If txtTrfArrDroptype.Text = "P" Then
                strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ArrDropCode & "'"
                chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                If chksector <> "" Then
                    strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ArrDropCode & "'"
                    objBLLTransferSearch.ArrSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    If objBLLTransferSearch.ArrSector Is Nothing = True Then
                        objBLLTransferSearch.ArrSector = ""
                    End If

                End If
            Else
                objBLLTransferSearch.ArrSector = objBLLTransferSearch.ArrDropCode
            End If


            strSearchCriteria = strSearchCriteria & "||" & "ArrSector:" & objBLLTransferSearch.ArrSector
            If txtTrfSourcecountrycode.Text <> "" Then
                objBLLTransferSearch.TrfSourceCountryCode = txtTrfSourcecountrycode.Text
            End If


            If txtTrfSourcecountry.Text <> "" Then
                objBLLTransferSearch.TrfSourceCountry = txtTrfSourcecountry.Text
                strSearchCriteria = strSearchCriteria & "||" & "SourceCountry :" & txtTrfSourcecountry.Text
            End If

            If txtTrfCustomercode.Text <> "" Then
                objBLLTransferSearch.TrfCustomerCode = txtTrfCustomercode.Text
            End If

            If txtTrfCustomer.Text <> "" Then
                objBLLTransferSearch.TrfCustomer = txtTrfCustomer.Text
                strSearchCriteria = strSearchCriteria & "||" & "Agent :" & txtTrfCustomer.Text
            End If

            If txtTrfCustomer.Text <> "" Then
                objBLLTransferSearch.TrfCustomer = txtTrfCustomer.Text

            End If

            If strAdult <> "" Then
                objBLLTransferSearch.TrfAdult = strAdult
                strSearchCriteria = strSearchCriteria & "||" & "Adult :" & strAdult
            End If

            '''''''''''
            ''Departure
            If chkDeparture.Checked = True Then
                objBLLTransferSearch.DepTransferType = "DEPARTURE"
                strSearchCriteria = strSearchCriteria & "||" & "DEPARTURE :Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & "DEPARTURE :No"

            End If

            If txtTrfDeparturedate.Text <> "" Then
                objBLLTransferSearch.DepTransferDate = txtTrfDeparturedate.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepTransferDate :" & txtTrfDeparturedate.Text
            End If

            If txtDepartureFlight.Text <> "" Then
                objBLLTransferSearch.DepFlightNo = txtDepartureFlight.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepFlightNo :" & txtDepartureFlight.Text
            End If
            If ddlTrfDepFlightClass.SelectedIndex <> 0 And chkDeparture.Checked = True Then
                objBLLTransferSearch.DepFlightClass = ddlTrfDepFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & "DepFlightClass :" & ddlTrfDepFlightClass.Text
            End If
            If txtDepartureTime.Text <> "" Then
                objBLLTransferSearch.DepFlightTime = txtDepartureTime.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepFlightTime :" & txtDepartureTime.Text
            End If
            If txtTrfDeppickupcode.Text <> "" Then
                objBLLTransferSearch.DepPickupCode = txtTrfDeppickupcode.Text
                objBLLTransferSearch.DepPickuptype = txtTrfDeppickuptype.Text   '' Added shahul 19/07/18
            End If

            If txtTrfDeppickup.Text <> "" Then
                objBLLTransferSearch.DepPickupName = txtTrfDeppickup.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepPickupName :" & txtTrfDeppickup.Text
            End If

            If txtTrfDepairportdropcode.Text <> "" Then
                objBLLTransferSearch.DepDropCode = txtTrfDepairportdropcode.Text
            End If

            If txtTrfDepairportdrop.Text <> "" Then
                objBLLTransferSearch.DepDropName = txtTrfDepairportdrop.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepDropName :" & txtTrfDepairportdrop.Text
            End If


            'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.DepPickupCode & "'"
            'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            If txtTrfDeppickuptype.Text = "P" Then
                strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.DepPickupCode & "'"
                chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                If chksector <> "" Then
                    strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.DepPickupCode & "'"
                    objBLLTransferSearch.DepSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    If objBLLTransferSearch.DepSector Is Nothing = True Then
                        objBLLTransferSearch.DepSector = ""
                    End If
                End If

            Else
                objBLLTransferSearch.DepSector = objBLLTransferSearch.DepPickupCode
            End If

            strSearchCriteria = strSearchCriteria & "||" & "DepSector :" & objBLLTransferSearch.DepSector

            '''' Shifting

            If chkinter.Checked = True Then
                objBLLTransferSearch.ShiftingTransferType = "INTERHOTEL"
                strSearchCriteria = strSearchCriteria & "||" & "INTERHOTEL :Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & "INTERHOTEL :No"
            End If

            If txtTrfinterdate.Text <> "" Then
                objBLLTransferSearch.ShiftingDate = txtTrfinterdate.Text
                strSearchCriteria = strSearchCriteria & "||" & "ShiftingDate :" & txtTrfinterdate.Text
            End If

            If txtTrfinterPickupcode.Text <> "" Then
                objBLLTransferSearch.ShiftingPickupCode = txtTrfinterPickupcode.Text
                objBLLTransferSearch.ShiftingPickuptype = txtTrfinterPickuptype.Text  '' Added shahul 21/07/18
            End If

            If txtTrfinterPickupcode.Text <> "" Then
                objBLLTransferSearch.ShiftingPickupName = txtTrfinterPickup.Text
                strSearchCriteria = strSearchCriteria & "||" & "ShiftingPickupName :" & txtTrfinterPickup.Text
            End If

            If txtTrfInterdropffcode.Text <> "" Then
                objBLLTransferSearch.ShiftingDropCode = txtTrfInterdropffcode.Text
                objBLLTransferSearch.ShiftingDroptype = txtTrfInterdropfftype.Text '' Added shahul 21/07/18
            End If

            If txtTrfInterdropffcode.Text <> "" Then
                objBLLTransferSearch.ShiftingDropName = txtTrfInterdropff.Text
                strSearchCriteria = strSearchCriteria & "||" & "ShiftingDropName :" & txtTrfInterdropff.Text
            End If

            'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
            'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            If txtTrfinterPickuptype.Text = "P" Then
                strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
                chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                If chksector <> "" Then
                    strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ShiftingPickupCode & "'"
                    objBLLTransferSearch.ShiftingPickupSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    If objBLLTransferSearch.ShiftingPickupSector Is Nothing = True Then
                        objBLLTransferSearch.ShiftingPickupSector = ""
                    End If

                End If

            Else
                objBLLTransferSearch.ShiftingPickupSector = objBLLTransferSearch.ShiftingPickupCode
            End If
            strSearchCriteria = strSearchCriteria & "||" & "ShiftingPickupSector :" & objBLLTransferSearch.ShiftingPickupSector

            'strQuery = "select 't' from othtypmast(nolock) where othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and othtypcode='" & objBLLTransferSearch.ShiftingDropCode & "'"
            'chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            If txtTrfInterdropfftype.Text = "P" Then
                strQuery = "select 't' from partymast(nolock) where  sptypecode  in (select option_selected from reservation_parameters where param_id=458) and partycode='" & objBLLTransferSearch.ShiftingDropCode & "'"
                chksector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                If chksector <> "" Then

                    strQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & objBLLTransferSearch.ShiftingDropCode & "'"
                    objBLLTransferSearch.ShiftingDropSector = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    If objBLLTransferSearch.ShiftingDropSector Is Nothing = True Then
                        objBLLTransferSearch.ShiftingDropSector = ""
                    End If
                End If

            Else
                objBLLTransferSearch.ShiftingDropSector = objBLLTransferSearch.ShiftingDropCode '' Added shahul 21/07/18
            End If

            strSearchCriteria = strSearchCriteria & "||" & "ShiftingDropSector :" & objBLLTransferSearch.ShiftingDropSector
            If strAdult <> "" Then
                objBLLTransferSearch.TrfAdult = strAdult
                strSearchCriteria = strSearchCriteria & "||" & "Adult :" & strAdult
            End If



            If strChildren <> "" Then
                objBLLTransferSearch.TrfChildren = strChildren
                strSearchCriteria = strSearchCriteria & "||" & "Children :" & strChildren
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
            strSearchCriteria = strSearchCriteria & "||" & "ChildAgeString :" & objBLLTransferSearch.ChildAgeString

            If chkTrfoverride.Checked = True Then
                objBLLTransferSearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice :" & objBLLTransferSearch.OverridePrice
            Else
                objBLLTransferSearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice :" & objBLLTransferSearch.OverridePrice
            End If

            objBLLTransferSearch.BookType = "TRANSFER"

            objBLLTransferSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & "LoginType :" & objBLLTransferSearch.LoginType
            objBLLTransferSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTransferSearch.TrfCustomerCode, Session("sAgentCode")) ' Session("sAgentCode")
            strSearchCriteria = strSearchCriteria & "||" & "AgentCode :" & objBLLTransferSearch.AgentCode
            objBLLTransferSearch.WebUserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "||" & "WebUserName :" & objBLLTransferSearch.WebUserName
            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                Dim objBLLCommonFuntions As New BLLCommonFuntions()
                '  Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Transfer Search Page", "Transfer Search", strSearchCriteria, Session("GlobalUserName"))
            End If


            Session("sobjBLLTransferSearch") = objBLLTransferSearch


            Dim dsSearchResults As New DataSet
            dsSearchResults = objBLLTransferSearch.GetSearchDetails()


            If dsSearchResults.Tables(4).Rows.Count = 0 Then
                dvhotnoshow.Style.Add("display", "block")
            Else
                dvhotnoshow.Style.Add("display", "none")
            End If

            Session("sDSTrfSearchResults") = dsSearchResults
            If dsSearchResults.Tables.Count > 0 Then
                Dim dvArrivalDetails As DataView = New DataView(dsSearchResults.Tables(0))
                Dim dvDepartureDetails As DataView = New DataView(dsSearchResults.Tables(1))
                Dim dvInterDetails As DataView = New DataView(dsSearchResults.Tables(2))
                Dim recordCount As Integer = dvArrivalDetails.Count + dvDepartureDetails.Count + dvInterDetails.Count

                Me.PopulatePager(recordCount)

                ' Session("sdtRoomType") = dsSearchResults.Tables(1)
                '  BindPricefilter(dsSearchResults.Tables(2))
                BindVehicleTypes(dsSearchResults.Tables(4))



                BindArrivalDetails(dvArrivalDetails)
                BindDepartureDetails(dvDepartureDetails)
                BindInterhoteleDetails(dvInterDetails)

                BindPricefilter(dsSearchResults.Tables(3))

                'BindRoomClassification(dsSearchResults.Tables(6))
                lblHotelCount.Text = recordCount & " Records Found"
            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If
            If dlArrTransferSearchResults.Items.Count > 0 Then
                Dim myarrButton As LinkButton = TryCast(dlArrTransferSearchResults.Controls(dlArrTransferSearchResults.Controls.Count - 1).FindControl("lbArrShowMore"), LinkButton)
                myarrButton.Text = "Show More"
            End If
            If dlDepTransferSearchResults.Items.Count > 0 Then
                Dim mydepButton As LinkButton = TryCast(dlDepTransferSearchResults.Controls(dlDepTransferSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)
                mydepButton.Text = "Show More"
            End If
            If dlShifting.Items.Count > 0 Then
                Dim myInterButton As LinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)
                myInterButton.Text = "Show More"
            End If


            btnPageBottom.Focus()

            'Dim strScript As String = "javascript: CallPriceSlider();"
            'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Session("SelectedArrival") = Nothing
        Session("SelectedDeparture") = Nothing
        Session("SelectedInter") = Nothing
        Transfersearch()
        txtSearchFocus.Focus()
    End Sub

    Private Sub BindDestName(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            chkSectors.DataSource = dataTable
            chkSectors.DataTextField = "sectorname"
            chkSectors.DataValueField = "sectorcode"
            chkSectors.DataBind()
            If chkSectors.Items.Count > 0 Then
                For Each chkitem As ListItem In chkSectors.Items
                    chkitem.Selected = True
                Next
            End If
        Else
            chkSectors.Items.Clear()
            chkSectors.DataBind()
        End If
    End Sub

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
            If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                Dim dt As New DataTable
                dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                If dt.Rows(0)("cnt") > 1 Then
                    strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply  (select min(b1.checkin) checkin from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                    & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and  b.checkin<>m.checkin  and p.partyname like  '%" & prefixText & "%' " 'and isnull(b.shiftto,0)=1

                Else
                    strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
        & " where  partyname like '%" & prefixText & "%' order by partyname "

                End If
            Else
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

            If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                Dim dt As New DataTable
                dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                If dt.Rows(0)("cnt") > 1 Then
                    strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                 & " inner join partymast p(nolock) on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and b.checkout<>m.checkout and isnull(b.shiftfrom,0)=1 and p.partyname like  '%" & prefixText & "%' "

                Else
                    strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                    & " where  partyname like '%" & prefixText & "%' order by partyname "

                End If
            Else
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
    Private Sub BindRoomClassification(ByVal dataTable As DataTable)

        If dataTable.Rows.Count > 0 Then
            chkRoomClassification.DataSource = dataTable
            chkRoomClassification.DataTextField = "roomclassname"
            chkRoomClassification.DataValueField = "roomclasscode"
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

    Private Sub BindArrivalDetails(ByVal dvResults As DataView, Optional ByVal showmore As String = "")
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLTransferSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If
        'start added param 22/10/2018
        Dim strNotvehicletype As String = ""
        If chkHotelStars.Items.Count > 0 Then
            For Each chkitem As ListItem In chkHotelStars.Items
                If chkitem.Selected = False Then
                    If strNotvehicletype = "" Then
                        strNotvehicletype = "'" & chkitem.Value & "'"
                    Else
                        strNotvehicletype = strNotvehicletype & "," & "'" & chkitem.Value & "'"
                    End If

                End If
            Next
        End If
        Dim strFilterCriteria As String = ""
        If strNotvehicletype <> "" Then
            If strFilterCriteria <> "" Then
                strFilterCriteria = strFilterCriteria & " AND " & "cartypecode NOT IN (" & strNotvehicletype & ")"
            Else
                strFilterCriteria = " cartypecode NOT IN (" & strNotvehicletype & ")"
            End If
        End If

        If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
            If strFilterCriteria <> "" Then
                strFilterCriteria = strFilterCriteria & " AND " & "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
            Else
                strFilterCriteria = "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
            End If
        End If
        If dvResults.Count > 0 Then
            If strFilterCriteria <> "" Then
                dvResults.RowFilter = strFilterCriteria
            End If
        End If
        'end added param 22/10/2018
        Dim dt As New DataTable
        dt = dvResults.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then
            lblHotelCount.Text = dt.Rows.Count & " Records Found"
            Dim iPageIndex As Integer = 1
            Dim iPageSize As Integer = dt.Rows.Count
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sTrfMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sTrfMailBoxPageIndex")
            End If

            iRowNoFrom = (iPageIndex - 1) * PageSize + 1
            iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
            dv.Table.Columns.Add("rowIndex")

            'Dim exists As Boolean = dv.ToTable().Columns.Contains("Selected")
            'If exists = False Then
            '    dv.Table.Columns.Add("Selected")
            'End If

            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("rowIndex") = (i + 1).ToString
            Next

            If showmore = "" Then
                'dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & IIf(ddlTrfChild1.SelectedValue = 0, "1", "2")
                dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=1" '& IIf(ddlTrfChild1.SelectedValue = 0, "1", "2")
            Else

                dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count
            End If

            dlArrTransferSearchResults.DataSource = dv

            dlArrTransferSearchResults.DataBind()

        Else
            dlArrTransferSearchResults.DataBind()
        End If
        '' ADDED Shahul 
        If hdOPMode.Value <> "" And Session("SelectedArrival") Is Nothing Then

            Session("SelectedArrival") = dv.ToTable
        End If

        Dim sicadult As String = "", sicchild As String = ""
        Dim strQuery1 As String = ""
        Dim sicchildselected As Boolean = False
        strQuery1 = "select  stuff((select distinct ',' + option_selected from reservation_parameters  u(nolock) where param_id in (1149,1150)  for xml path('')),1,1,'' ) "
        sicadult = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)
        If ddlTrfChild.SelectedValue > 0 Then
            sicchildselected = True
        End If


        If Not Session("SelectedArrival") Is Nothing Then

            dt = Session("SelectedArrival")
            For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

                Dim lblairportborder As Label = gvRow.FindControl("lblairportborder")
                Dim lblfromsector As Label = gvRow.FindControl("lblfromsector")
                Dim lblsectorgroupcode As Label = gvRow.FindControl("lblsectorgroupcode")
                Dim lbltransferdate As Label = gvRow.FindControl("lbltransferdate")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblshuttle As Label = gvRow.FindControl("lblshuttle")
                Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
                Dim txttotal As TextBox = gvRow.FindControl("txttotal")
                Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
                Dim lblplistcode As Label = gvRow.FindControl("lblplistcode")

                Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkarrcompliment")

                Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
                Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
                Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
                Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
                Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
                Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)

                Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
                Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")
                Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")


                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString = lblcartypecode.Value Then
                        chkbooknow.Checked = True
                        txtnoofunits.Text = dt.Rows(i)("units").ToString
                        txtunitprice.Text = dt.Rows(i)("Unitprice").ToString
                        txttotal.Text = dt.Rows(i)("unitsalevalue").ToString
                        hdnselected.Value = "1"
                        txtwlunitprice.Text = dt.Rows(i)("wlunitprice").ToString
                        txtwltotal.Text = dt.Rows(i)("wlunitsalevalue").ToString

                        chkarrcompliment.Checked = IIf(dt.Rows(i)("complimentarycust").ToString = 1, True, False)   ' modified param 04/10/18
                    Else '' ADDED Shahul 
                        hdnselected.Value = "0"
                    End If
                Next
                'If chkbooknow.Checked = False And sicchildselected = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True And hdOPMode.Value <> "Edit" Then
                '    chkbooknow.Checked = True

                'End If

            Next


        End If

        '' Commented Shahul 25/02/18
        'For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
        '    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
        '    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
        '    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
        '    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
        '    Dim lbltrfunit As Label = gvRow.FindControl("lbltrfunit")
        '    Dim lblprice As Label = gvRow.FindControl("lblprice")
        '    Dim lblvalue As Label = gvRow.FindControl("lblvalue")
        '    Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkarrcompliment")
        '    Dim hdncompliment As HiddenField = gvRow.FindControl("hdncompliment")


        '    Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
        '    Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
        '    Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
        '    Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
        '    Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
        '    Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
        '    Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
        '    Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)

        '    Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
        '    Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")
        '    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

        '    If hdnselected.Value = 1 Then
        '        chkbooknow.Checked = True
        '        txtnoofunits.Text = Val(lbltrfunit.Text)
        '        txtunitprice.Text = Val(lblprice.Text)
        '        txttotal.Text = Val(lblvalue.Text)

        '        txtwlunitprice.Text = Val(lblwlunitprice_grid.Text)
        '        txtwltotal.Text = Val(lblwlunitsalevalue.Text)

        '        chkarrcompliment.Checked = IIf(hdncompliment.Value = 1, True, False)
        '    End If

        'Next

    End Sub
    Private Sub BindDepartureDetails(ByVal dvResults As DataView, Optional ByVal showmore As String = "")
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLTransferSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If

        End If
        'start added param 22/10/2018
        Dim strNotvehicletype As String = ""
        If chkHotelStars.Items.Count > 0 Then
            For Each chkitem As ListItem In chkHotelStars.Items
                If chkitem.Selected = False Then
                    If strNotvehicletype = "" Then
                        strNotvehicletype = "'" & chkitem.Value & "'"
                    Else
                        strNotvehicletype = strNotvehicletype & "," & "'" & chkitem.Value & "'"
                    End If

                End If
            Next
        End If
        Dim strFilterCriteria As String = ""
        If strNotvehicletype <> "" Then
            If strFilterCriteria <> "" Then
                strFilterCriteria = strFilterCriteria & " AND " & "cartypecode NOT IN (" & strNotvehicletype & ")"
            Else
                strFilterCriteria = " cartypecode NOT IN (" & strNotvehicletype & ")"
            End If
        End If

        If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
            If strFilterCriteria <> "" Then
                strFilterCriteria = strFilterCriteria & " AND " & "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
            Else
                strFilterCriteria = "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
            End If
        End If
        If dvResults.Count > 0 Then
            If strFilterCriteria <> "" Then
                dvResults.RowFilter = strFilterCriteria
            End If
        End If
        'end added param 22/10/2018
        Dim dt As New DataTable
        dt = dvResults.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then

            Dim iPageIndex As Integer = 1
            Dim iPageSize As Integer = dt.Rows.Count
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sTrfMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sTrfMailBoxPageIndex")
            End If

            iRowNoFrom = (iPageIndex - 1) * PageSize + 1
            iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
            dv.Table.Columns.Add("rowIndex")
            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("rowIndex") = (i + 1).ToString
            Next

            If showmore = "" Then
                dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=1"
            Else
                dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count

            End If


            dlDepTransferSearchResults.DataSource = dv
            dlDepTransferSearchResults.DataBind()

        Else
            dlDepTransferSearchResults.DataBind()
        End If


        '' ADDED Shahul 
        If hdOPMode.Value <> "" And Session("SelectedDeparture") Is Nothing Then

            Session("SelectedDeparture") = dv.ToTable
        End If

        Dim sicadult As String = "", sicchild As String = ""
        Dim strQuery1 As String = ""
        Dim sicchildselected As Boolean = False
        strQuery1 = "select  stuff((select distinct ',' + option_selected from reservation_parameters  u(nolock) where param_id in (1149,1150)  for xml path('')),1,1,'' ) "
        sicadult = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)
        If ddlTrfChild.SelectedValue > 0 Then
            sicchildselected = True
        End If

        If Not Session("SelectedDeparture") Is Nothing Then

            dt = Session("SelectedDeparture")
            For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

                Dim lblairportborder As Label = gvRow.FindControl("lblairportborder")
                Dim lblfromsector As Label = gvRow.FindControl("lblfromsector")
                Dim lblsectorgroupcode As Label = gvRow.FindControl("lblsectorgroupcode")
                Dim lbltransferdate As Label = gvRow.FindControl("lbltransferdate")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblshuttle As Label = gvRow.FindControl("lblshuttle")
                Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
                Dim txttotal As TextBox = gvRow.FindControl("txttotal")
                Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
                Dim lblplistcode As Label = gvRow.FindControl("lblplistcode")

                Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkdepcompliment")

                Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
                Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")

                Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
                Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
                Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
                Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
                Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
                Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)
                Dim hdnselected As HiddenField = CType(gvRow.FindControl("hdnselected"), HiddenField)

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString = lblcartypecode.Value Then
                        chkbooknow.Checked = True
                        txtnoofunits.Text = dt.Rows(i)("units").ToString
                        txtunitprice.Text = dt.Rows(i)("Unitprice").ToString
                        txttotal.Text = dt.Rows(i)("unitsalevalue").ToString
                        hdnselected.Value = "1"
                        txtwlunitprice.Text = dt.Rows(i)("wlunitprice").ToString
                        txtwltotal.Text = dt.Rows(i)("wlunitsalevalue").ToString
                        chkarrcompliment.Checked = IIf(dt.Rows(i)("complimentarycust").ToString = 1, True, False)        'modified param 04/10/2018
                        'txtnoofunits.Text = dt.Rows(i)(1).ToString
                        'txtunitprice.Text = dt.Rows(i)(2).ToString
                        'txttotal.Text = dt.Rows(i)(3).ToString
                        'txtwlunitprice.Text = dt.Rows(i)("wlunitprice").ToString
                        'txtwltotal.Text = dt.Rows(i)("wlunitsalevalue").ToString

                    Else '' ADDED Shahul 
                        hdnselected.Value = "0"
                    End If
                Next

            Next


        End If

        '' Commented Shahul 25/02/18
        'For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
        '    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
        '    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
        '    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
        '    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
        '    Dim lbltrfunit As Label = gvRow.FindControl("lbltrfunit")
        '    Dim lblprice As Label = gvRow.FindControl("lblprice")
        '    Dim lblvalue As Label = gvRow.FindControl("lblvalue")
        '    Dim chkdepcompliment As CheckBox = gvRow.FindControl("chkdepcompliment")
        '    Dim hdncompliment As HiddenField = gvRow.FindControl("hdncompliment")
        '    Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
        '    Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")

        '    Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
        '    Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
        '    Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
        '    Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
        '    Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
        '    Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
        '    Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
        '    Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)

        '    If hdnselected.Value = 1 Then
        '        chkbooknow.Checked = True
        '        txtnoofunits.Text = Val(lbltrfunit.Text)
        '        txtunitprice.Text = Val(lblprice.Text)
        '        txttotal.Text = Val(lblvalue.Text)
        '        txtwlunitprice.Text = Val(lblwlunitprice_grid.Text)
        '        txtwltotal.Text = Val(lblwlunitsalevalue.Text)
        '        chkdepcompliment.Checked = IIf(hdncompliment.Value = 1, True, False)
        '    End If
        'Next


    End Sub
    Private Sub BindInterhoteleDetails(ByVal dvResults As DataView, Optional ByVal showmore As String = "")
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLTransferSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If
        'start added param 25/10/2018
        Dim strNotvehicletype As String = ""
        If chkHotelStars.Items.Count > 0 Then
            For Each chkitem As ListItem In chkHotelStars.Items
                If chkitem.Selected = False Then
                    If strNotvehicletype = "" Then
                        strNotvehicletype = "'" & chkitem.Value & "'"
                    Else
                        strNotvehicletype = strNotvehicletype & "," & "'" & chkitem.Value & "'"
                    End If

                End If
            Next
        End If
        Dim strFilterCriteria As String = ""
        If strNotvehicletype <> "" Then
            If strFilterCriteria <> "" Then
                strFilterCriteria = strFilterCriteria & " AND " & "cartypecode NOT IN (" & strNotvehicletype & ")"
            Else
                strFilterCriteria = " cartypecode NOT IN (" & strNotvehicletype & ")"
            End If
        End If

        If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
            If strFilterCriteria <> "" Then
                strFilterCriteria = strFilterCriteria & " AND " & "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
            Else
                strFilterCriteria = "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
            End If
        End If
        If dvResults.Count > 0 Then
            If strFilterCriteria <> "" Then
                dvResults.RowFilter = strFilterCriteria
            End If
        End If
        'end added param 25/10/2018
        Dim dt As New DataTable
        dt = dvResults.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then

            Dim iPageIndex As Integer = 1
            Dim iPageSize As Integer = dt.Rows.Count
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sTrfMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sTrfMailBoxPageIndex")
            End If

            iRowNoFrom = (iPageIndex - 1) * PageSize + 1
            iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
            dv.Table.Columns.Add("rowIndex")
            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("rowIndex") = (i + 1).ToString
            Next

            If showmore = "" Then
                dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=1"
            Else
                dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count

            End If
            'dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count

            dlShifting.DataSource = dv
            dlShifting.DataBind()

        Else
            dlShifting.DataBind()
        End If

        '' ADDED Shahul 
        If hdOPMode.Value <> "" And Session("SelectedInter") Is Nothing Then

            Session("SelectedInter") = dv.ToTable
        End If

        Dim myLinkButton As LinkButton
        If chkinter.Checked = True And dlShifting.Items.Count > 0 Then
            If dlShifting.Items.Count = 1 Then

                myLinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)
                myLinkButton.Style.Add("display", "block")
            Else
                myLinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)
                myLinkButton.Style.Add("display", "block")
            End If
        End If

        If Not Session("SelectedInter") Is Nothing Then

            dt = Session("SelectedInter")
            For Each gvRow As DataListItem In dlShifting.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

                Dim lblairportborder As Label = gvRow.FindControl("lblairportborder")
                Dim lblfromsector As Label = gvRow.FindControl("lblfromsector")
                Dim lblsectorgroupcode As Label = gvRow.FindControl("lblsectorgroupcode")
                Dim lbltransferdate As Label = gvRow.FindControl("lbltransferdate")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblshuttle As Label = gvRow.FindControl("lblshuttle")
                Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
                Dim txttotal As TextBox = gvRow.FindControl("txttotal")
                Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
                Dim lblplistcode As Label = gvRow.FindControl("lblplistcode")

                Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkintercompliment")


                Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
                Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")
                Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString = lblcartypecode.Value Then
                        chkbooknow.Checked = True
                        txtnoofunits.Text = dt.Rows(i)("units").ToString
                        txtunitprice.Text = dt.Rows(i)("Unitprice").ToString
                        txttotal.Text = dt.Rows(i)("unitsalevalue").ToString
                        hdnselected.Value = "1"
                        txtwlunitprice.Text = dt.Rows(i)("wlunitprice").ToString
                        txtwltotal.Text = dt.Rows(i)("wlunitsalevalue").ToString
                        chkarrcompliment.Checked = IIf(dt.Rows(i)("complimentarycust").ToString = 1, True, False)     'modified param 04/10/2018



                    Else
                        hdnselected.Value = "0"
                    End If
                Next

            Next


        End If

        'For Each gvRow As DataListItem In dlShifting.Items
        '    Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
        '    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
        '    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
        '    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
        '    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
        '    Dim lbltrfunit As Label = gvRow.FindControl("lbltrfunit")
        '    Dim lblprice As Label = gvRow.FindControl("lblprice")
        '    Dim lblvalue As Label = gvRow.FindControl("lblvalue")
        '    Dim chkintercompliment As CheckBox = gvRow.FindControl("chkintercompliment")
        '    Dim hdncompliment As HiddenField = gvRow.FindControl("hdncompliment")

        '    Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
        '    Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
        '    Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
        '    Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
        '    Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
        '    Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
        '    Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
        '    Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)


        '    Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
        '    Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")

        '    If hdnselected.Value = 1 Then
        '        chkbooknow.Checked = True
        '        txtnoofunits.Text = Val(lbltrfunit.Text)
        '        txtunitprice.Text = Val(lblprice.Text)
        '        txttotal.Text = Val(lblvalue.Text)
        '        txtwlunitprice.Text = Val(lblwlunitprice_grid.Text)
        '        txtwltotal.Text = Val(lblwlunitsalevalue.Text)
        '        chkintercompliment.Checked = IIf(hdncompliment.Value = 1, True, False)
        '    End If
        'Next

    End Sub
    Private Sub BindgvRoomType(ByVal gvHotelRoomType As GridView, ByVal strRatePlanCode As String, ByVal strHotelCode As String)
        Dim dtResults As New DataTable
        dtResults = Session("sdtRoomType")
        Dim dvResults As DataView = New DataView(dtResults)
        'Dim strNotSelectedRoomClass As String = ""
        'If chkRoomClassification.Items.Count > 0 Then
        '    For Each chkitem As ListItem In chkRoomClassification.Items
        '        If chkitem.Selected = False Then
        '            If strNotSelectedRoomClass = "" Then
        '                strNotSelectedRoomClass = "'" & chkitem.Value & "'"
        '            Else
        '                strNotSelectedRoomClass = strNotSelectedRoomClass & "," & "'" & chkitem.Value & "'"
        '            End If

        '        End If
        '    Next
        'End If
        'If strNotSelectedRoomClass <> "" Then
        '    dvResults.RowFilter = "RatePlanId='" & strRatePlanCode & "' AND PartyCode='" & strHotelCode & "' AND roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
        'Else
        '    dvResults.RowFilter = "RatePlanId='" & strRatePlanCode & "' AND PartyCode='" & strHotelCode & "'"
        'End If

        dvResults.RowFilter = "RatePlanId='" & strRatePlanCode & "' AND PartyCode='" & strHotelCode & "'"

        'If ddlOrderBy.Text = "By Room" Then
        '    dvResults.Sort = "rmtyporder ASC "
        'Else
        '    dvResults.Sort = "totalvalue ASC"
        'End If

        If dvResults.ToTable.Rows.Count > 0 Then 'added totable mhd 28/03/2017
            gvHotelRoomType.DataSource = dvResults.ToTable 'added totable mhd 28/03/2017
            gvHotelRoomType.DataBind()
        Else
            gvHotelRoomType.DataSource = Nothing 'mhd 28/03/2017
            gvHotelRoomType.DataBind()
        End If

    End Sub

    Private Sub BindgvRoomTypeWithBlank(ByVal gvHotelRoomType As GridView)
        Dim dtResults As New DataTable
        dtResults.Columns.Add("rmTypName", GetType(String))
        dtResults.Columns.Add("MealPlanNames", GetType(String))
        dtResults.Columns.Add("TotalValue", GetType(String))
        gvHotelRoomType.DataSource = dtResults
        gvHotelRoomType.DataBind()
    End Sub

    Private Sub BindRatePlan(ByVal dlRatePlan As DataList, ByVal strHotelCode As String)
        Dim dtRatePlan As New DataTable
        dtRatePlan = Session("sdtRoomType")

        Dim dvRatePlan As DataView = New DataView(dtRatePlan, "PartyCode='" & strHotelCode & "'", "RatePlanName", DataViewRowState.CurrentRows)
        Dim strNotSelectedRoomClass As String = ""
        If chkRoomClassification.Items.Count > 0 Then
            For Each chkitem As ListItem In chkRoomClassification.Items
                If chkitem.Selected = False Then
                    If strNotSelectedRoomClass = "" Then
                        strNotSelectedRoomClass = "'" & chkitem.Value & "'"
                    Else
                        strNotSelectedRoomClass = strNotSelectedRoomClass & "," & "'" & chkitem.Value & "'"
                    End If

                End If
            Next
        End If
        If strNotSelectedRoomClass <> "" Then
            dvRatePlan.RowFilter = " PartyCode='" & strHotelCode & "'AND roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
        Else
            dvRatePlan.RowFilter = "PartyCode='" & strHotelCode & "'"
        End If
        dvRatePlan.Sort = "rateplanorder asc"
        dtRatePlan = dvRatePlan.ToTable()
        If dtRatePlan.Rows.Count > 1 Then
            dtRatePlan = dtRatePlan.DefaultView.ToTable(True, "PartyCode", "RatePlanId", "RatePlanName", "Show", "rateplanorder")
        End If
        'If dtRatePlan.Rows.Count > 0 Then
        '    For i As Integer = 0 To dtRatePlan.Rows.Count - 1
        '        dtRatePlan.Rows(i)("RatePlanName") = dtRatePlan.Rows(i)("RatePlanName")
        '    Next
        'End If
        dlRatePlan.DataSource = dtRatePlan
        dlRatePlan.DataBind()
    End Sub

    Private Sub BindRatePlanWithBlank(ByVal dlRatePlan As DataList)
        Dim dtRatePlan As New DataTable
        dtRatePlan.Columns.Add("PartyCode", GetType(String))
        dtRatePlan.Columns.Add("RatePlanId", GetType(String))
        dtRatePlan.Columns.Add("RatePlanName", GetType(String))
        dtRatePlan.Columns.Add("Show", GetType(String))
        dlRatePlan.DataSource = dtRatePlan
        dlRatePlan.DataBind()
    End Sub

    Public Function GetDistinctRecords(ByVal dt As DataTable, ByVal Columns As String()) As DataTable
        Dim dtUniqRecords As DataTable = New DataTable()
        dtUniqRecords = dt.DefaultView.ToTable(True, Columns)
        Return dtUniqRecords
    End Function

    Private Sub BindPricefilter(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            hdPriceMinRange.Value = IIf(dataTable.Rows(0)("minprice").ToString = "", "0", dataTable.Rows(0)("minprice").ToString)
            hdPriceMaxRange.Value = IIf(dataTable.Rows(0)("maxprice").ToString = "", "0", dataTable.Rows(0)("maxprice").ToString)
        Else
            hdPriceMinRange.Value = "0"
            hdPriceMaxRange.Value = "1"
        End If
    End Sub


    Private Sub BindArrivalDetailsWithFilter(ByVal dsSearchResults As DataSet)
        Dim dvMaiDetails As DataView
        Dim dvDepMaiDetails As DataView
        Dim dvInterDetails As DataView
        'Hided param 29/10/2018
        'Dim strNotvehicletype As String = ""
        'If chkHotelStars.Items.Count > 0 Then
        '    For Each chkitem As ListItem In chkHotelStars.Items
        '        If chkitem.Selected = False Then
        '            If strNotvehicletype = "" Then
        '                strNotvehicletype = "'" & chkitem.Value & "'"
        '            Else
        '                strNotvehicletype = strNotvehicletype & "," & "'" & chkitem.Value & "'"
        '            End If

        '        End If
        '    Next
        'End If

        'Dim strFilterCriteria As String = ""
        'If strNotvehicletype <> "" Then
        '    If strFilterCriteria <> "" Then
        '        strFilterCriteria = strFilterCriteria & " AND " & "cartypecode NOT IN (" & strNotvehicletype & ")"
        '    Else
        '        strFilterCriteria = " cartypecode NOT IN (" & strNotvehicletype & ")"
        '    End If
        'End If

        'If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
        '    If strFilterCriteria <> "" Then
        '        strFilterCriteria = strFilterCriteria & " AND " & "unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
        '    Else
        '        strFilterCriteria = " unitsalevalue >=" & hdPriceMin.Value & " AND unitsalevalue <=" & hdPriceMax.Value
        '    End If
        'End If


        Dim recordCount As Integer = 0
        If dsSearchResults.Tables.Count > 0 Then

            If dsSearchResults.Tables(0).Rows.Count > 0 Then
                ''ARrival
                dvMaiDetails = New DataView(dsSearchResults.Tables(0))
                recordCount = dvMaiDetails.Count
                'If strFilterCriteria <> "" Then
                '    dvMaiDetails.RowFilter = strFilterCriteria
                'End If
                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "vehiclename ASC"


                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "unitprice ASC"


                ElseIf ddlSorting.Text = "0" Then
                    dvMaiDetails.Sort = "unitprice ASC"


                End If
                'added param 28/10/2018
                If dlArrTransferSearchResults.items.count > 0 Then
                    Dim myLinkButton1 As LinkButton = TryCast(dlArrTransferSearchResults.Controls(dlArrTransferSearchResults.Controls.Count - 1).FindControl("lbArrShowMore"), LinkButton)

                    If myLinkButton1.Text = "Show More" Then
                        ViewState("ArrShow") = "0"
                        BindArrivalDetails(dvMaiDetails, "")
                    Else
                        ViewState("ArrShow") = "1"
                        BindArrivalDetails(dvMaiDetails, "1")
                    End If
                Else
                    ViewState("ArrShow") = "0"
                    BindArrivalDetails(dvMaiDetails, "")
                End If
                'end param 28/10/2018
            Else
                dlArrTransferSearchResults.DataBind()
            End If
            ''Departure
            If dsSearchResults.Tables(1).Rows.Count > 0 Then
                dvDepMaiDetails = New DataView(dsSearchResults.Tables(1))
                recordCount += dvDepMaiDetails.Count
                'If strFilterCriteria <> "" Then
                '    dvDepMaiDetails.RowFilter = strFilterCriteria
                'End If

                If ddlSorting.Text = "Name" Then

                    dvDepMaiDetails.Sort = "vehiclename ASC"


                ElseIf ddlSorting.Text = "Price" Then

                    dvDepMaiDetails.Sort = "unitprice ASC"


                ElseIf ddlSorting.Text = "0" Then

                    dvDepMaiDetails.Sort = "unitprice ASC"


                End If

                'added param 29/10/2018
                If dlDepTransferSearchResults.Items.Count > 0 Then
                    Dim myLinkButtonDep As LinkButton = TryCast(dlDepTransferSearchResults.Controls(dlDepTransferSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)

                    If myLinkButtonDep.Text = "Show More" Then
                        ViewState("DepShow") = "0"
                        BindDepartureDetails(dvDepMaiDetails, "")
                    Else
                        ViewState("DepShow") = "1"
                        BindDepartureDetails(dvDepMaiDetails, "1")
                    End If
                Else
                    ViewState("DepShow") = "0"
                    BindDepartureDetails(dvDepMaiDetails, "")
                End If
                'end param 29/10/2018
            Else
                dlDepTransferSearchResults.DataBind()
            End If
            '' Shifting
            If dsSearchResults.Tables(2).Rows.Count > 0 Then
                dvInterDetails = New DataView(dsSearchResults.Tables(2))
                recordCount += dvInterDetails.Count
                'If strFilterCriteria <> "" Then
                '    dvInterDetails.RowFilter = strFilterCriteria
                'End If

                If ddlSorting.Text = "Name" Then

                    dvInterDetails.Sort = "vehiclename ASC"

                ElseIf ddlSorting.Text = "Price" Then

                    dvInterDetails.Sort = "unitprice ASC"

                ElseIf ddlSorting.Text = "0" Then

                    dvInterDetails.Sort = "unitprice ASC"

                End If

                'added param 29/10/2018
                If dlShifting.Items.Count > 0 Then
                    Dim myLinkButtonInter As LinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)

                    If myLinkButtonInter.Text = "Show More" Then
                        ViewState("InterShow") = "0"
                        BindInterhoteleDetails(dvInterDetails, "")
                    Else
                        ViewState("InterShow") = "1"
                        BindInterhoteleDetails(dvInterDetails, "1")
                    End If
                Else
                    ViewState("InterShow") = "0"
                    BindInterhoteleDetails(dvInterDetails, "")
                End If
                'end param 29/10/2018
            Else
                dlShifting.DataBind()
            End If


            Me.PopulatePager(recordCount)


        End If

        'Else
        'dlArrTransferSearchResults.DataBind()
        'dlDepTransferSearchResults.DataBind()
        'dlShifting.DataBind()
        'End If



    End Sub
    Private Sub BindHotelMainDetailsWithFilter(ByVal dsSearchResults As DataSet)
        If dsSearchResults.Tables.Count > 0 Then
            If dsSearchResults.Tables(0).Rows.Count > 0 Then
                Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
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


                ' Filter Sectors *****************
                Dim strNotSelectedSectors As String = ""
                If chkSectors.Items.Count > 0 Then
                    For Each chkitem As ListItem In chkSectors.Items
                        If chkitem.Selected = False Then
                            If strNotSelectedSectors = "" Then
                                strNotSelectedSectors = "'" & chkitem.Value & "'"
                            Else
                                strNotSelectedSectors = strNotSelectedSectors & "," & "'" & chkitem.Value & "'"
                            End If

                        End If
                    Next
                End If


                ' Filter Sectors *****************
                Dim strNotSelectedPropertyType As String = ""
                If chkPropertyType.Items.Count > 0 Then
                    For Each chkitem As ListItem In chkPropertyType.Items
                        If chkitem.Selected = False Then
                            If strNotSelectedPropertyType = "" Then
                                strNotSelectedPropertyType = "'" & chkitem.Value & "'"
                            Else
                                strNotSelectedPropertyType = strNotSelectedPropertyType & "," & "'" & chkitem.Value & "'"
                            End If

                        End If
                    Next
                End If


                ' Filter for Price *****************
                Dim strFilterCriteria As String = ""
                If strNotSelectedHotelStar <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "catcode NOT IN (" & strNotSelectedHotelStar & ")"
                    Else
                        strFilterCriteria = " catcode NOT IN (" & strNotSelectedHotelStar & ")"
                    End If
                End If

                If strNotSelectedSectors <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "sectorcode NOT IN (" & strNotSelectedSectors & ")"
                    Else
                        strFilterCriteria = "sectorcode NOT IN (" & strNotSelectedSectors & ")"
                    End If
                End If
                If strNotSelectedPropertyType <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "propertytype NOT IN (" & strNotSelectedPropertyType & ")"
                    Else
                        strFilterCriteria = "propertytype NOT IN (" & strNotSelectedPropertyType & ")"
                    End If
                End If
                If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "avgprice >=" & hdPriceMin.Value & " AND avgprice <=" & hdPriceMax.Value
                    Else
                        strFilterCriteria = "avgprice >=" & hdPriceMin.Value & " AND avgprice <=" & hdPriceMax.Value
                    End If
                End If
                If strFilterCriteria <> "" Then
                    dvMaiDetails.RowFilter = strFilterCriteria
                End If

                'If ddlSorting.Text = "Name" Then
                '    dvMaiDetails.Sort = "partyname ASC"
                'ElseIf ddlSorting.Text = "Price" Then
                '    dvMaiDetails.Sort = "avgprice ASC"
                'ElseIf ddlSorting.Text = "0" Then
                '    dvMaiDetails.Sort = "avgprice ASC"
                'ElseIf ddlSorting.Text = "Rating" Then
                '    dvMaiDetails.Sort = "noofstars DESC,partyname ASC "
                'ElseIf ddlSorting.Text = "Preferred" Then
                '    dvMaiDetails.Sort = "Preferred  DESC,partyname ASC "
                'End If
                Dim recordCount As Integer = dvMaiDetails.Count


                BindArrivalDetails(dvMaiDetails)
                Me.PopulatePager(recordCount)
                Session("sdtRoomType") = dsSearchResults.Tables(1)

                ' lblHotelCount.Text = dvMaiDetails.Count & " Records Found"

            End If
        Else
            dlArrTransferSearchResults.DataBind()
        End If
    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Try
            System.Threading.Thread.Sleep(200)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSTrfSearchResults")
            'BindHotelMainDetailsWithFilter(dsSearchResults)
            BindArrivalDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub btnFilterForRoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterForRoom.Click
        Try


            dlArrTransferSearchResults.DataSource = Nothing
            dlArrTransferSearchResults.DataBind()


            Dim strSelectedRoomClass As String = ""
            If chkRoomClassification.Items.Count > 0 Then
                For Each chkitem As ListItem In chkRoomClassification.Items
                    If chkitem.Selected = True Then
                        If strSelectedRoomClass = "" Then
                            strSelectedRoomClass = chkitem.Value
                        Else
                            strSelectedRoomClass = strSelectedRoomClass & ";" & chkitem.Value
                        End If

                    End If
                Next
            End If

            objBLLHotelSearch = New BLLHotelSearch
            If Session("sobjBLLHotelSearch") Is Nothing Then
                Response.Redirect("Home.aspx?Tab=2")
            End If
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)

            Dim dsSearchResults As New DataSet
            objBLLHotelSearch.FilterRoomClass = strSelectedRoomClass
            dsSearchResults = objBLLHotelSearch.GetSearchDetails()
            Session("sDSSearchResults") = dsSearchResults
            If dsSearchResults.Tables.Count > 0 Then

                Session("sDSSearchResults") = dsSearchResults

                Session("sTrfMailBoxPageIndex") = "1"
                BindHotelMainDetailsWithFilter(dsSearchResults)



            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If





        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim strScript As String = "javascript: CallPriceSlider();"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)


        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)



    End Sub


    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSSearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sTrfMailBoxPageIndex") = pageIndex.ToString
            BindHotelMainDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Private Sub PopulatePager(ByVal recordCount As Integer)
        'Dim currentPage As Integer
        'Dim pages As New List(Of ListItem)()
        'Dim startIndex As Integer, endIndex As Integer
        'Dim pagerSpan As Integer = 3

        ''Calculate the Start and End Index of pages to be displayed.
        'Dim dblPageCount As Double = CDbl(CDec(recordCount) / Convert.ToDecimal(PageSize))
        'Dim pageCount As Integer = CInt(Math.Ceiling(dblPageCount))
        'startIndex = If(currentPage > 1 AndAlso currentPage + pagerSpan - 1 < pagerSpan, currentPage, 1)
        'endIndex = If(pageCount > pagerSpan, pagerSpan, pageCount)
        'If currentPage > pagerSpan Mod 2 Then
        '    If currentPage = 2 Then
        '        endIndex = 3
        '    Else
        '        endIndex = currentPage + 2
        '    End If
        'Else
        '    endIndex = (pagerSpan - currentPage) + 1
        'End If

        'If endIndex - (pagerSpan - 1) > startIndex Then
        '    startIndex = endIndex - (pagerSpan - 1)
        'End If

        'If endIndex > pageCount Then
        '    endIndex = pageCount
        '    startIndex = If(((endIndex - pagerSpan) + 1) > 0, (endIndex - pagerSpan) + 1, 1)
        'End If

        ''Add the First Page Button.
        'If currentPage > 1 Then
        '    pages.Add(New ListItem("First", "1"))
        'End If

        ''Add the Previous Button.
        'If currentPage > 1 Then
        '    pages.Add(New ListItem("<<", (currentPage - 1).ToString()))
        'End If

        'For i As Integer = startIndex To endIndex
        '    pages.Add(New ListItem(i.ToString(), i.ToString(), i <> currentPage))
        'Next

        'Dim iMod As Integer
        'iMod = pageCount Mod pagerSpan
        'If iMod = 0 Then
        '    iMod = 2
        'Else
        '    iMod = 1
        'End If
        ''Add the Next Button.
        'If currentPage < pageCount And pagerSpan < pageCount Then


        '    If (pagerSpan + currentPage) <= pageCount Or currentPage < pageCount - iMod Then
        '        If pageCount - (pagerSpan - 1) <> currentPage Then
        '            pages.Add(New ListItem(">>", (currentPage + 1).ToString()))

        '        End If
        '    End If

        'End If

        ''Add the Last Button.
        'If currentPage <> pageCount And pagerSpan < pageCount Then
        '    If (pagerSpan + currentPage) <= pageCount Or currentPage < pageCount - iMod Then
        '        If pageCount - (pagerSpan - 1) <> currentPage Then
        '            pages.Add(New ListItem("Last", pageCount.ToString()))
        '        End If

        '    End If
        'End If
        Dim currentPage As Integer = 1
        If Not Session("sTrfMailBoxPageIndex") Is Nothing Then
            currentPage = Session("sTrfMailBoxPageIndex")
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
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub


    Private Sub BindRecentlyViewedHotels()
        Dim dtRecentlyViewedHotel As New DataTable
        Dim dcHotelCode = New DataColumn("HotelCode", GetType(String))
        Dim dcHotelName = New DataColumn("HotelName", GetType(String))
        Dim dcLocation = New DataColumn("Location", GetType(String))
        Dim dcPrice = New DataColumn("Price", GetType(String))
        Dim dcHotelImage = New DataColumn("HotelImage", GetType(String))
        dtRecentlyViewedHotel.Columns.Add(dcHotelCode)
        dtRecentlyViewedHotel.Columns.Add(dcHotelName)
        dtRecentlyViewedHotel.Columns.Add(dcLocation)
        dtRecentlyViewedHotel.Columns.Add(dcPrice)
        dtRecentlyViewedHotel.Columns.Add(dcHotelImage)
        Session("sdtRecentlyViewedHotel") = dtRecentlyViewedHotel
    End Sub



    Protected Sub dltotalPriceBreak_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dltotalPriceBreak.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim lblBreakupDate As Label = CType(e.Item.FindControl("lblBreakupDate"), Label)
                Dim lblBreakupDateName As Label = CType(e.Item.FindControl("lblBreakupDateName"), Label)
                Dim lblbreakupPrice As Label = CType(e.Item.FindControl("lblbreakupPrice"), Label)
                Dim lblBreakupTotalPrice As Label = CType(e.Item.FindControl("lblBreakupTotalPrice"), Label)
                Dim txtsaleprice As TextBox = CType(e.Item.FindControl("txtsaleprice"), TextBox)
                Dim txtBreakupTotalPrice As TextBox = CType(e.Item.FindControl("txtBreakupTotalPrice"), TextBox)
                Dim dtDate As DateTime = CType(lblBreakupDate.Text, DateTime)
                lblBreakupDate.Text = dtDate.ToString("dd/MM/yyyy")
                lblBreakupDateName.Text = dtDate.ToString("dddd")
                lblbreakupPrice.Text = lblbreakupPrice.Text.Replace(".000", "")
                lblBreakupTotalPrice.Text = lblBreakupTotalPrice.Text.Replace(".000", "")
                dlcolumnRepeatFlag = dlcolumnRepeatFlag + 1
                Dim dvPriceDate As HtmlGenericControl = CType(e.Item.FindControl("dvPriceDate"), HtmlGenericControl)
                Dim dvPricePerNight As HtmlGenericControl = CType(e.Item.FindControl("dvPricePerNight"), HtmlGenericControl)
                Dim dvCostPricePerNight As HtmlGenericControl = CType(e.Item.FindControl("dvCostPricePerNight"), HtmlGenericControl)
                Dim dvCostPricePerNightText As HtmlGenericControl = CType(e.Item.FindControl("dvCostPricePerNightText"), HtmlGenericControl)

                If dlcolumnRepeatFlag = 1 Or dlcolumnRepeatFlag Mod 3 = 1 Then
                    dvPriceDate.Visible = True
                    dvPricePerNight.Visible = True
                    dvCostPricePerNightText.Visible = True
                Else
                    dvPriceDate.Visible = False
                    dvPricePerNight.Visible = False
                    dvCostPricePerNightText.Visible = False
                End If
                If Session("sLoginType") <> "RO" Then
                    dvCostPricePerNight.Visible = False
                    dvCostPricePerNightText.Visible = False
                    lblbreakupPrice.Visible = True
                    lblBreakupTotalPrice.Visible = True
                    txtsaleprice.Visible = False
                    txtBreakupTotalPrice.Visible = False
                    dvPriceBreakupSave.Visible = False
                    chkComplimentaryArrivalTransfer.Visible = False
                    chkComplimentaryDepartureTransfer.Visible = False
                    chkComplimentaryFromSupplier.Visible = False
                    chkComplimentaryToCustomer.Visible = False
                Else
                    'If chkOveridePrice.Checked = True Then
                    '    lblbreakupPrice.Visible = False
                    '    lblBreakupTotalPrice.Visible = False
                    '    txtsaleprice.Visible = True
                    '    txtBreakupTotalPrice.Visible = True
                    '    dvPriceBreakupSave.Visible = True
                    'Else
                    '    lblbreakupPrice.Visible = True
                    '    lblBreakupTotalPrice.Visible = True
                    '    txtsaleprice.Visible = False
                    '    txtBreakupTotalPrice.Visible = False
                    '    dvPriceBreakupSave.Visible = False
                    'End If
                    chkComplimentaryArrivalTransfer.Visible = True
                    chkComplimentaryDepartureTransfer.Visible = True
                    chkComplimentaryFromSupplier.Visible = True
                    chkComplimentaryToCustomer.Visible = True
                End If

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: dltotalPriceBreak_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnPriceBreakupFillPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupFillPrice.Click
        Try
            If txtBreakupTotalPriceForAll.Text <> "" Or txtsalepriceForAll.Text <> "" Then
                For Each dlitem As DataListItem In dltotalPriceBreak.Items
                    Dim txtsaleprice As TextBox = CType(dlitem.FindControl("txtsaleprice"), TextBox)
                    Dim txtBreakupTotalPrice As TextBox = CType(dlitem.FindControl("txtBreakupTotalPrice"), TextBox)
                    If chkFillBlank.Checked = True Then
                        If txtBreakupTotalPriceForAll.Text <> "" Then
                            If txtBreakupTotalPrice.Text = "" Then
                                txtBreakupTotalPrice.Text = txtBreakupTotalPriceForAll.Text
                            End If
                        End If
                        If txtsalepriceForAll.Text <> "" Then
                            If txtsaleprice.Text = "" Then
                                txtsaleprice.Text = txtsalepriceForAll.Text
                            End If
                        End If

                    Else
                        If txtBreakupTotalPriceForAll.Text <> "" Then

                            txtBreakupTotalPrice.Text = txtBreakupTotalPriceForAll.Text
                        End If
                        If txtsalepriceForAll.Text <> "" Then
                            txtsaleprice.Text = txtsalepriceForAll.Text
                        End If
                    End If

                Next
            End If
            ' mpTotalprice.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: btnPriceBreakupFillPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnPriceBreakupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupSave.Click
        Try


            Dim dtPriceBreakupTemp As New DataTable
            dtPriceBreakupTemp = Session("sdtPriceBreakupTemp")

            Dim dtPriceBreakup As New DataTable
            If Not Session("sdtPriceBreakup") Is Nothing Then
                dtPriceBreakup = Session("sdtPriceBreakup")
            Else
                dtPriceBreakup = dtPriceBreakupTemp.Clone()

            End If

            Dim fSalePrice As Double = 0
            For Each item As DataListItem In dltotalPriceBreak.Items
                Dim lblBreakupDate As Label = CType(item.FindControl("lblBreakupDate"), Label)
                Dim lblBreakupDate1 As Label = CType(item.FindControl("lblBreakupDate1"), Label)
                Dim txtsaleprice As TextBox = CType(item.FindControl("txtsaleprice"), TextBox)
                Dim txtBreakupTotalPrice As TextBox = CType(item.FindControl("txtBreakupTotalPrice"), TextBox)

                Dim chkComplimentaryToCustomer As CheckBox = CType(item.FindControl("chkComplimentaryToCustomer"), CheckBox)
                Dim chkComplimentaryFromSupplier As CheckBox = CType(item.FindControl("chkComplimentaryFromSupplier"), CheckBox)
                Dim chkComplimentaryArrivalTransfer As CheckBox = CType(item.FindControl("chkComplimentaryArrivalTransfer"), CheckBox)
                Dim chkComplimentaryDepartureTransfer As CheckBox = CType(item.FindControl("chkComplimentaryDepartureTransfer"), CheckBox)
                If txtsaleprice.Text <> "" Then
                    fSalePrice = fSalePrice + txtsaleprice.Text
                End If

                If dtPriceBreakup.Rows.Count > 0 Then
                    Dim dr1 As DataRow = dtPriceBreakup.Select("pricedate='" & lblBreakupDate1.Text & "' AND rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'  AND rmcatcode='" + hdRMcatCode.Value + "'   AND accommodationid='" + hdRMAccCode.Value + "'").FirstOrDefault
                    If Not dr1 Is Nothing Then
                        dr1("wlsaleprice") = txtsaleprice.Text.Trim
                        dr1("totalprice") = txtBreakupTotalPrice.Text.Trim
                        dr1("saleprice") = txtsaleprice.Text.Trim
                    Else
                        Dim dr As DataRow = dtPriceBreakupTemp.Select("pricedate='" & lblBreakupDate1.Text & "' AND rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'  AND rmcatcode='" + hdRMcatCode.Value + "'   AND accommodationid='" + hdRMAccCode.Value + "'").FirstOrDefault
                        If Not dr Is Nothing Then
                            dr("wlsaleprice") = txtsaleprice.Text.Trim
                            dr("totalprice") = txtBreakupTotalPrice.Text.Trim
                            dr("saleprice") = txtsaleprice.Text.Trim
                            Dim drNew As DataRow = dtPriceBreakup.NewRow()
                            drNew.ItemArray = dr.ItemArray
                            dtPriceBreakup.Rows.Add(drNew)
                        End If
                    End If
                Else
                    Dim dr As DataRow = dtPriceBreakupTemp.Select("pricedate='" & lblBreakupDate1.Text & "' AND rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'  AND rmcatcode='" + hdRMcatCode.Value + "'   AND accommodationid='" + hdRMAccCode.Value + "'").FirstOrDefault
                    If Not dr Is Nothing Then
                        dr("wlsaleprice") = txtsaleprice.Text.Trim
                        dr("totalprice") = txtBreakupTotalPrice.Text.Trim
                        dr("saleprice") = txtsaleprice.Text.Trim
                        Dim drNew As DataRow = dtPriceBreakup.NewRow()
                        drNew.ItemArray = dr.ItemArray
                        dtPriceBreakup.Rows.Add(drNew)

                    End If
                End If
            Next

            Session("sdtPriceBreakup") = dtPriceBreakup

            Dim dtRoomType As New DataTable
            dtRoomType = Session("sdtRoomType")

            If dtRoomType.Rows.Count > 0 Then

                Dim dr As DataRow = dtRoomType.Select("rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'  AND rmcatcode='" + hdRMcatCode.Value + "'   AND accommodationid='" + hdRMAccCode.Value + "' ").First
                dr("totalvalue") = fSalePrice.ToString.Replace(".00", "").Replace(".0", "")
                dr("comp_cust") = IIf(chkComplimentaryToCustomer.Checked, "1", "0")
                dr("comp_supp") = IIf(chkComplimentaryFromSupplier.Checked, "1", "0")
                dr("comparrtrf") = IIf(chkComplimentaryArrivalTransfer.Checked, "1", "0")
                dr("compdeptrf") = IIf(chkComplimentaryDepartureTransfer.Checked, "1", "0")

            End If

            Session("sdtRoomType") = dtRoomType
            Dim dsSearchResults As DataSet
            dsSearchResults = Session("sDSSearchResults")
            If dsSearchResults.Tables.Count > 0 Then
                Dim dr As DataRow = dsSearchResults.Tables(1).Select("rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'  AND rmcatcode='" + hdRMcatCode.Value + "'   AND accommodationid='" + hdRMAccCode.Value + "' ").First
                dr("totalvalue") = fSalePrice.ToString.Replace(".00", "").Replace(".0", "")
                dr("comp_cust") = IIf(chkComplimentaryToCustomer.Checked, "1", "0")
                dr("comp_supp") = IIf(chkComplimentaryFromSupplier.Checked, "1", "0")
                dr("comparrtrf") = IIf(chkComplimentaryArrivalTransfer.Checked, "1", "0")
                dr("compdeptrf") = IIf(chkComplimentaryDepartureTransfer.Checked, "1", "0")
            End If

            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbTotalPrice"), LinkButton)

            Dim gvGridviewRow As GridViewRow = CType(lbTotalPrice.NamingContainer, GridViewRow)
            Dim _gvHotelRoomType As GridView = CType((gvGridviewRow.Parent.Parent), GridView)

            Dim dlRatePlanItem As DataListItem = CType(_gvHotelRoomType.NamingContainer, DataListItem)
            Dim _dlRatePlan As DataList = CType((dlRatePlanItem.Parent), DataList)

            Dim dlArrTransferSearchResultsItem As DataListItem = CType(_dlRatePlan.NamingContainer, DataListItem)

            Dim _dlRatePlan1 As New DataList
            _dlRatePlan1 = CType(dlArrTransferSearchResults.Items(dlArrTransferSearchResultsItem.ItemIndex).FindControl("dlRatePlan"), DataList)

            Dim _gvHotelRoomType1 As New GridView
            _gvHotelRoomType1 = CType(_dlRatePlan1.Items(dlRatePlanItem.ItemIndex).FindControl("gvHotelRoomType"), GridView)

            Dim lbTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbTotalprice"), LinkButton)

            Dim lblCompCust As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblCompCust"), Label)
            Dim lblCompSupp As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblCompSupp"), Label)
            Dim lblComparrtrf As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblComparrtrf"), Label)
            Dim lblCompdeptrf As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblCompdeptrf"), Label)

            lbTotalprice1.Text = fSalePrice.ToString.Replace(".00", "").Replace(".0", "") & " " & hdRoomTypeCurrCode.Value
            lblCompCust.Text = IIf(chkComplimentaryToCustomer.Checked, "1", "0")
            lblCompSupp.Text = IIf(chkComplimentaryFromSupplier.Checked, "1", "0")
            lblComparrtrf.Text = IIf(chkComplimentaryArrivalTransfer.Checked, "1", "0")
            lblCompdeptrf.Text = IIf(chkComplimentaryDepartureTransfer.Checked, "1", "0")


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HomeSearch.aspx :: btnPriceBreakupSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub dlDepTransferSearchResults_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlDepTransferSearchResults.ItemCreated
        If dlDepTransferSearchResults.Items.Count = 1 And e.Item.ItemType = 1 And ViewState("Depviewmorehide") = 1 Then
            Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbDepShowMore"), LinkButton)

            myLinkButton.Visible = False
        End If
    End Sub

    Protected Sub dlDepTransferSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlDepTransferSearchResults.ItemDataBound

        If e.Item.ItemType = 1 Then
            Dim myarrButton As LinkButton = CType(e.Item.FindControl("lbDepShowMore"), LinkButton)

            If ViewState("DepShow") = "1" Then
                myarrButton.Text = "Show Less"
            Else
                myarrButton.Text = "Show More"
            End If

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'Show Hotel Image
            Dim imgHotelImage As Image = CType(e.Item.FindControl("imgvehicleImage"), Image)
            Dim lblHotelImage As Label = CType(e.Item.FindControl("lblHotelImage"), Label)
            imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblHotelImage.Text & "&type=2"

            Dim lblshuttle As Label = CType(e.Item.FindControl("lblshuttle"), Label)
            Dim lblpaxcheck As Label = CType(e.Item.FindControl("lblpaxcheck"), Label)
            Dim lblmin As Label = CType(e.Item.FindControl("lblmin"), Label)
            Dim lblminpax As Label = CType(e.Item.FindControl("lblminpax"), Label)
            Dim lblmax As Label = CType(e.Item.FindControl("lblmax"), Label)
            Dim lblmaxpax As Label = CType(e.Item.FindControl("lblmaxpax"), Label)
            Dim lblcurrcode As Label = CType(e.Item.FindControl("lblcurrcode"), Label)


            Dim lblunitprice As Label = CType(e.Item.FindControl("lblunitprice"), Label)
            Dim txtnoofunits As TextBox = CType(e.Item.FindControl("txtnoofunits"), TextBox)
            Dim lbltrfunit As Label = CType(e.Item.FindControl("lbltrfunit"), Label)
            Dim txttotal As TextBox = CType(e.Item.FindControl("txttotal"), TextBox)
            Dim lblprice As Label = CType(e.Item.FindControl("lblprice"), Label)
            Dim lblvaluetext As Label = CType(e.Item.FindControl("lblvaluetext"), Label)
            Dim txtunitprice As TextBox = CType(e.Item.FindControl("txtunitprice"), TextBox)
            Dim chkbooknow As CheckBox = CType(e.Item.FindControl("chkbooknow"), CheckBox)


            Dim txtwltotal As TextBox = CType(e.Item.FindControl("txtwltotal"), TextBox)
            Dim txtwlunitprice As TextBox = CType(e.Item.FindControl("txtwlunitprice"), TextBox)
            Dim lblwlprice As Label = CType(e.Item.FindControl("lblunitcprice"), Label)
            Dim lblwlvaluetext As Label = CType(e.Item.FindControl("lblwlvaluetext"), Label)
            Dim lblwlunitprice As Label = CType(e.Item.FindControl("lblwlunitprice"), Label)

            Dim lblpreferedsupplier As Label = CType(e.Item.FindControl("lblpreferedsupplier"), Label)
            Dim lblunitcprice As Label = CType(e.Item.FindControl("lblunitcprice"), Label)
            Dim lblunitcostvalue As Label = CType(e.Item.FindControl("lblunitcostvalue"), Label)
            Dim lbltcplistcode As Label = CType(e.Item.FindControl("lbltcplistcode"), Label)
            Dim lblwlconvrate As Label = CType(e.Item.FindControl("lblwlconvrate"), Label)
            Dim lblwlmarkupperc As Label = CType(e.Item.FindControl("lblwlmarkupperc"), Label)
            Dim lblwlunitprice_grid As Label = CType(e.Item.FindControl("lblwlunitprice_grid"), Label)
            Dim lblwlunitsalevalue As Label = CType(e.Item.FindControl("lblwlunitsalevalue"), Label)


            Dim divpaxprice As HtmlGenericControl = CType(e.Item.FindControl("divpaxprice"), HtmlGenericControl)
            Dim divlbl As HtmlGenericControl = CType(e.Item.FindControl("divlbl"), HtmlGenericControl)
            Dim divdepcomplement As HtmlGenericControl = CType(e.Item.FindControl("divdepcomplement"), HtmlGenericControl)
            Dim chkdepcompliment As CheckBox = CType(e.Item.FindControl("chkdepcompliment"), CheckBox)

            If Session("sLoginType") <> "RO" Then
                divdepcomplement.Style.Add("display", "none")
            Else
                divdepcomplement.Style.Add("display", "block")
            End If
            txtnoofunits.Text = lbltrfunit.Text
            If lblpaxcheck.Text = 0 Or lblshuttle.Text = 1 Then
                lblmin.Visible = False
                lblminpax.Visible = False
                lblmax.Visible = False
                lblmaxpax.Visible = False


                ' txtnoofunits.ReadOnly = True

                txttotal.Text = Val(txtnoofunits.Text) * Val(lblprice.Text)
                lblunitprice.Text = Math.Round(Val(lblprice.Text), 2).ToString + " " + lblcurrcode.Text + "  /  PAX"
                txtunitprice.Text = Math.Round(Val(lblprice.Text), 2)
                lblvaluetext.Text = " " + lblcurrcode.Text + "  /  PAX"
                txttotal.Text = Val(txtnoofunits.Text) * Val(lblprice.Text)

                Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                Dim dwlUnitprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                dwlUnitprice = dUnitprice * dWlMarkup

                lblwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2).ToString + " " + lblcurrcode.Text + "  /  PAX"
                txtwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2)
                txtwltotal.Text = Val(txtnoofunits.Text) * Val(dwlUnitprice)

            Else
                lblmin.Visible = True
                lblminpax.Visible = True
                lblmax.Visible = True
                lblmaxpax.Visible = True



                txtnoofunits.ReadOnly = False
                lblunitprice.Text = Math.Round(Val(lblprice.Text)).ToString + " " + lblcurrcode.Text + "  /  UNIT"
                txtunitprice.Text = Math.Round(Val(lblprice.Text))
                lblvaluetext.Text = " " + lblcurrcode.Text + "  /  UNIT"
                txttotal.Text = Val(txtnoofunits.Text) * Val(lblprice.Text)

                Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                Dim dwlUnitprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                dwlUnitprice = dUnitprice * dWlMarkup

                lblwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2).ToString + " " + lblcurrcode.Text + "  /  UNIT"
                txtwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2)
                txtwltotal.Text = Math.Round(Val(txtnoofunits.Text) * Val(dwlUnitprice), 2)

            End If
            If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                lblwlunitprice.Style.Add("display", "block")
                txtwlunitprice.Style.Add("display", "block")
                txtwltotal.Style.Add("display", "block")

                txttotal.Style.Add("display", "none")
                lblunitprice.Style.Add("display", "none")
                txtunitprice.Style.Add("display", "none")
            Else
                lblwlunitprice.Style.Add("display", "none")
                txtwlunitprice.Style.Add("display", "none")
                txtwltotal.Style.Add("display", "none")

                txttotal.Style.Add("display", "block")
                lblunitprice.Style.Add("display", "block")
                txtunitprice.Style.Add("display", "block")
            End If

            If Session("sLoginType") = "RO" And chkTrfoverride.Checked = True Then
                divpaxprice.Style.Add("display", "block")
                divlbl.Style.Add("display", "none")
                'txtunitprice.Style.Add("display", "block")
                'lblunitprice.Style.Add("display", "none")
                'lblvaluetext.Visible = True
            Else
                divpaxprice.Style.Add("display", "none")
                divlbl.Style.Add("display", "block")
                'txtunitprice.Style.Add("display", "none")
                'lblunitprice.Style.Add("display", "block")
                'lblvaluetext.Visible = False

            End If
            ' txttotal.ReadOnly = True

            '  chkbooknow.Attributes.Add("onChange", "calculatevalue('" & chkbooknow.ClientID & "','" & txtnoofunits.ClientID & "','" & txtunitprice.ClientID & "','" & txttotal.ClientID & "','" + CType(e.Item.ItemIndex, String) + "')")
            'txtnoofunits.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'txtunitprice.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'chkdepcompliment.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'chkbooknow.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")


            txtunitprice.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            txtnoofunits.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            txtwlunitprice.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            chkbooknow.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            chkdepcompliment.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")   'param 04/10/2018

            If iCumulative = 1 Then
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lblunitprice1 As Label = CType(e.Item.FindControl("lblunitprice"), Label)
                    Dim txtnoofunits1 As TextBox = CType(e.Item.FindControl("txtnoofunits"), TextBox)
                    Dim txttotal1 As TextBox = CType(e.Item.FindControl("txttotal"), TextBox)
                    Dim lbltotal1 As Label = CType(e.Item.FindControl("lbltotal"), Label)
                    Dim lblunitname As Label = CType(e.Item.FindControl("lblunitname"), Label)
                    Dim dvunit As HtmlGenericControl = CType(e.Item.FindControl("dvunit"), HtmlGenericControl)
                    Dim divtot As HtmlGenericControl = CType(e.Item.FindControl("divtot"), HtmlGenericControl)


                    'lblunitprice1.Visible = False
                    'txttotal1.Visible = False
                    'lbltotal1.Visible = False
                    'divtot.Visible = False

                    divtot.Style.Add("display", "none")
                    txttotal1.Style.Add("display", "none")
                    lbltotal1.Style.Add("display", "none")
                    lblunitprice1.Style.Add("display", "none")


                End If
            End If

        End If
    End Sub
    Private Shared Function GetRequestId() As String
        Dim strRequestId As String = ""
        Dim objBLLHotelSearch2 As New BLLHotelSearch
        If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch2 = CType(HttpContext.Current.Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            strRequestId = objBLLHotelSearch2.OBrequestid
        ElseIf Not HttpContext.Current.Session("sobjBLLTourSearchActive") Is Nothing Then
            Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
            objBLLTourSearch = CType(HttpContext.Current.Session("sobjBLLTourSearchActive"), BLLTourSearch)
            strRequestId = objBLLTourSearch.EbRequestID
        ElseIf Not HttpContext.Current.Session("sobjBLLTransferSearchActive") Is Nothing Then
            Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
            objBLLTransferSearch = CType(HttpContext.Current.Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
            strRequestId = objBLLTransferSearch.OBRequestId
        ElseIf Not HttpContext.Current.Session("sobjBLLMASearchActive") Is Nothing Then
            Dim objBLLMASearch As BLLMASearch = New BLLMASearch
            objBLLMASearch = CType(HttpContext.Current.Session("sobjBLLMASearchActive"), BLLMASearch)
            strRequestId = objBLLMASearch.OBRequestId
        End If
        Return strRequestId
    End Function
    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfChild, child)


    End Sub
    Private Sub FillSpecifiedChildAges(ByVal childages As String)


        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTrfChild8, childages)



    End Sub

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

    Protected Sub dlShifting_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlShifting.ItemCreated
        If dlShifting.Items.Count = 1 And e.Item.ItemType = 1 And ViewState("Interviewmorehide") = 1 Then
            Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbinterShowMore"), LinkButton)

            myLinkButton.Visible = False
        End If
    End Sub

    Protected Sub dlShifting_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlShifting.ItemDataBound


        If e.Item.ItemType = 1 Then
            Dim myarrButton As LinkButton = CType(e.Item.FindControl("lbinterShowMore"), LinkButton)

            If ViewState("InterShow") = "1" Then
                myarrButton.Text = "Show Less"
            Else
                myarrButton.Text = "Show More"
            End If

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'Show Hotel Image
            Dim imgHotelImage As Image = CType(e.Item.FindControl("imgvehicleImage"), Image)
            Dim lblHotelImage As Label = CType(e.Item.FindControl("lblHotelImage"), Label)
            imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblHotelImage.Text & "&type=2"

            Dim lblshuttle As Label = CType(e.Item.FindControl("lblshuttle"), Label)
            Dim lblpaxcheck As Label = CType(e.Item.FindControl("lblpaxcheck"), Label)
            Dim lblmin As Label = CType(e.Item.FindControl("lblmin"), Label)
            Dim lblminpax As Label = CType(e.Item.FindControl("lblminpax"), Label)
            Dim lblmax As Label = CType(e.Item.FindControl("lblmax"), Label)
            Dim lblmaxpax As Label = CType(e.Item.FindControl("lblmaxpax"), Label)
            Dim lblcurrcode As Label = CType(e.Item.FindControl("lblcurrcode"), Label)


            Dim lblunitprice As Label = CType(e.Item.FindControl("lblunitprice"), Label)
            Dim txtnoofunits As TextBox = CType(e.Item.FindControl("txtnoofunits"), TextBox)
            Dim lbltrfunit As Label = CType(e.Item.FindControl("lbltrfunit"), Label)
            Dim txttotal As TextBox = CType(e.Item.FindControl("txttotal"), TextBox)
            Dim lblprice As Label = CType(e.Item.FindControl("lblprice"), Label)
            Dim lblvaluetext As Label = CType(e.Item.FindControl("lblvaluetext"), Label)
            Dim txtunitprice As TextBox = CType(e.Item.FindControl("txtunitprice"), TextBox)
            Dim chkbooknow As CheckBox = CType(e.Item.FindControl("chkbooknow"), CheckBox)

            Dim txtwltotal As TextBox = CType(e.Item.FindControl("txtwltotal"), TextBox)
            Dim txtwlunitprice As TextBox = CType(e.Item.FindControl("txtwlunitprice"), TextBox)
            Dim lblwlprice As Label = CType(e.Item.FindControl("lblunitcprice"), Label)
            Dim lblwlvaluetext As Label = CType(e.Item.FindControl("lblwlvaluetext"), Label)
            Dim lblwlunitprice As Label = CType(e.Item.FindControl("lblwlunitprice"), Label)

            Dim lblpreferedsupplier As Label = CType(e.Item.FindControl("lblpreferedsupplier"), Label)
            Dim lblunitcprice As Label = CType(e.Item.FindControl("lblunitcprice"), Label)
            Dim lblunitcostvalue As Label = CType(e.Item.FindControl("lblunitcostvalue"), Label)
            Dim lbltcplistcode As Label = CType(e.Item.FindControl("lbltcplistcode"), Label)
            Dim lblwlconvrate As Label = CType(e.Item.FindControl("lblwlconvrate"), Label)
            Dim lblwlmarkupperc As Label = CType(e.Item.FindControl("lblwlmarkupperc"), Label)
            Dim lblwlunitprice_grid As Label = CType(e.Item.FindControl("lblwlunitprice_grid"), Label)
            Dim lblwlunitsalevalue As Label = CType(e.Item.FindControl("lblwlunitsalevalue"), Label)

            Dim divpaxprice As HtmlGenericControl = CType(e.Item.FindControl("divpaxprice"), HtmlGenericControl)
            Dim divlbl As HtmlGenericControl = CType(e.Item.FindControl("divlbl"), HtmlGenericControl)
            Dim divintercomplement As HtmlGenericControl = CType(e.Item.FindControl("divintercomplement"), HtmlGenericControl)
            Dim chkintercompliment As CheckBox = CType(e.Item.FindControl("chkintercompliment"), CheckBox)

            If Session("sLoginType") <> "RO" Then
                divintercomplement.Style.Add("display", "none")
            Else
                divintercomplement.Style.Add("display", "block")
            End If
            txtnoofunits.Text = lbltrfunit.Text

            If lblpaxcheck.Text = 0 Or lblshuttle.Text = 1 Then
                lblmin.Visible = False
                lblminpax.Visible = False
                lblmax.Visible = False
                lblmaxpax.Visible = False


                '  txtnoofunits.ReadOnly = True

                txttotal.Text = Val(txtnoofunits.Text) * Val(lblprice.Text)
                lblunitprice.Text = Math.Round(Val(lblprice.Text), 2).ToString + " " + lblcurrcode.Text + "  /  PAX"
                txtunitprice.Text = Math.Round(Val(lblprice.Text), 2)
                lblvaluetext.Text = " " + lblcurrcode.Text + "  /  PAX"


                Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                Dim dwlUnitprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                dwlUnitprice = dUnitprice * dWlMarkup

                lblwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2).ToString + " " + lblcurrcode.Text + "  /  PAX"
                txtwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2)
                txtwltotal.Text = Val(txtnoofunits.Text) * Val(dwlUnitprice)

            Else
                lblmin.Visible = True
                lblminpax.Visible = True
                lblmax.Visible = True
                lblmaxpax.Visible = True




                lblunitprice.Text = Math.Round(Val(lblprice.Text), 2).ToString + " " + lblcurrcode.Text + "  /  UNIT"
                txtunitprice.Text = Math.Round(Val(lblprice.Text), 2)
                lblvaluetext.Text = " " + lblcurrcode.Text + "  /  UNIT"
                txttotal.Text = Math.Round(Val(txtnoofunits.Text) * Val(lblprice.Text), 2)

                Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                Dim dwlUnitprice As Decimal
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                dwlUnitprice = dUnitprice * dWlMarkup

                lblwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2).ToString + " " + lblcurrcode.Text + "  /  UNIT"
                txtwlunitprice.Text = Math.Round(Val(dwlUnitprice), 2)
                txtwltotal.Text = Math.Round(Val(txtnoofunits.Text) * Val(dwlUnitprice), 2)
                ' txtnoofunits.ReadOnly = False
            End If
            If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                lblwlunitprice.Style.Add("display", "block")
                txtwlunitprice.Style.Add("display", "block")
                txtwltotal.Style.Add("display", "block")

                txttotal.Style.Add("display", "none")
                lblunitprice.Style.Add("display", "none")
                txtunitprice.Style.Add("display", "none")
            Else
                lblwlunitprice.Style.Add("display", "none")
                txtwlunitprice.Style.Add("display", "none")
                txtwltotal.Style.Add("display", "none")

                txttotal.Style.Add("display", "block")
                lblunitprice.Style.Add("display", "block")
                txtunitprice.Style.Add("display", "block")
            End If
            If Session("sLoginType") = "RO" And chkTrfoverride.Checked = True Then
                divpaxprice.Style.Add("display", "block")
                divlbl.Style.Add("display", "none")
                'txtunitprice.Style.Add("display", "block")
                'lblunitprice.Style.Add("display", "none")
                'lblvaluetext.Visible = True
            Else
                divpaxprice.Style.Add("display", "none")
                divlbl.Style.Add("display", "block")
                'txtunitprice.Style.Add("display", "none")
                'lblunitprice.Style.Add("display", "block")
                'lblvaluetext.Visible = False

            End If
            ' txttotal.ReadOnly = True

            '  chkbooknow.Attributes.Add("onChange", "calculatevalue('" & chkbooknow.ClientID & "','" & txtnoofunits.ClientID & "','" & txtunitprice.ClientID & "','" & txttotal.ClientID & "','" + CType(e.Item.ItemIndex, String) + "')")
            'txtnoofunits.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'txtunitprice.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'chkintercompliment.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")
            'chkbooknow.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "' )")

            txtunitprice.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            txtnoofunits.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            txtwlunitprice.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")
            chkbooknow.Attributes.Add("onChange", "javascript:calculatevalueinter('" + CType(chkbooknow.ClientID, String) + "','" + CType(txtnoofunits.ClientID, String) + "','" + CType(txtunitprice.ClientID, String) + "','" + CType(txttotal.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(chkTrfoverride.ClientID, String) + "','" + CType(txtwlunitprice.ClientID, String) + "','" + CType(txtwltotal.ClientID, String) + "' )")


            If iCumulative = 1 Then
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lblunitprice1 As Label = CType(e.Item.FindControl("lblunitprice"), Label)
                    Dim txtnoofunits1 As TextBox = CType(e.Item.FindControl("txtnoofunits"), TextBox)
                    Dim txttotal1 As TextBox = CType(e.Item.FindControl("txttotal"), TextBox)
                    Dim lbltotal1 As Label = CType(e.Item.FindControl("lbltotal"), Label)
                    Dim lblunitname As Label = CType(e.Item.FindControl("lblunitname"), Label)
                    Dim dvunit As HtmlGenericControl = CType(e.Item.FindControl("dvunit"), HtmlGenericControl)
                    Dim divtot As HtmlGenericControl = CType(e.Item.FindControl("divtot"), HtmlGenericControl)


                    'lblunitprice1.Visible = False
                    'txttotal1.Visible = False
                    'lbltotal1.Visible = False
                    'divtot.Visible = False

                    divtot.Style.Add("display", "none")
                    txttotal1.Style.Add("display", "none")
                    lbltotal1.Style.Add("display", "none")
                    lblunitprice1.Style.Add("display", "none")


                End If
            End If

        End If
    End Sub

    Protected Sub dlShifting_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlShifting.PreRender

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
            If Session("sobjBLLHotelSearchActive") Is Nothing Then
                Dim objBLLCommonFuntions As New BLLCommonFuntions
                Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                If dt.Rows.Count > 0 Then
                    dvMybooking.Visible = True
                    txtTrfCustomercode.Text = dt.Rows(0)("agentcode").ToString
                    ' strQuery = "select agentname from agentmast where active=1 and agentcode='" & objBLLHotelSearch.AgentCode & "'"
                    txtTrfCustomer.Text = dt.Rows(0)("agentname").ToString
                    txtTrfSourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                    txtTrfSourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString

                    ''' Added 01/06/17 shahul
                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    Dim childagestring As String() = childages.ToString.Split(";")
                    '''''''
                    ' Dim childagestring As String() = dt.Rows(0)("childages").ToString.ToString.Split(";")
                    ddlTrfAdult.SelectedValue = dt.Rows(0)("adults").ToString
                    ddlTrfChild.SelectedValue = dt.Rows(0)("child").ToString

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
                    ' LoadRoomAdultChild()

                Else
                    dvMybooking.Visible = True
                End If
            End If
        End If
    End Sub
    Protected Sub btnSelectedArrival_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedArrival.Click
        Dim dt As New DataTable
        Dim dr As DataRow

        '' ADDED Shahul 
        Dim sicadult As String = "", sicchild As String = ""
        Dim strQuery1 As String = ""
        Dim sicchildselected As Boolean = False
        Dim sicadultchecked As Boolean = False
        strQuery1 = "select  stuff((select distinct ',' + option_selected from reservation_parameters  u(nolock) where param_id in (1149,1150)  for xml path('')),1,1,'' ) "
        sicadult = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)
        If ddlTrfChild.SelectedValue > 0 Then
            sicchildselected = True
        End If
        For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
            Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

            If chkbooknow.Checked = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True Then
                sicadultchecked = True
            End If
            If chkbooknow.Checked = False And sicadultchecked = True And sicchildselected = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True Then
                chkbooknow.Checked = True
            End If

        Next
        ''''''

        'Added param 28/10/2018
        Dim ArrlistDt As New DataTable
        ArrlistDt.Columns.Add("Cartypecode", GetType(String))
        Dim myLinkButton1 As LinkButton = TryCast(dlArrTransferSearchResults.Controls(dlArrTransferSearchResults.Controls.Count - 1).FindControl("lbArrShowMore"), LinkButton)
        If Not Session("SelectedArrival") Is Nothing Then
            Dim checkSelDt As DataTable = Session("SelectedArrival")
            If checkSelDt.Rows.Count > 1 And dlArrTransferSearchResults.Items.Count = 1 Then
                For Each selDr As DataRow In checkSelDt.Rows
                    For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
                        Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                        Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                        If selDr("Cartypecode") = lblcartypecode.Value.Trim And chkbooknow.Checked = False Then
                            Dim arrDr As DataRow = ArrlistDt.NewRow
                            arrDr("Cartypecode") = selDr("Cartypecode")
                            ArrlistDt.Rows.Add(arrDr)
                            Exit For
                        End If
                    Next
                Next

                If Not ArrlistDt Is Nothing Then
                    For Each row As DataRow In ArrlistDt.Rows
                        Dim cartypecode As String = row("Cartypecode")
                        Dim carDr = (From n In checkSelDt.AsEnumerable Where n.Field(Of String)("Cartypecode") = cartypecode Select n).FirstOrDefault
                        If Not carDr Is Nothing Then
                            carDr.Delete()
                        End If
                    Next
                    
                    If checkSelDt.Rows.Count > 0 Then
                        Dim dsSearchResults As New DataSet
                        dsSearchResults = Session("sDSTrfSearchResults")
                        Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
                        Session("SelectedArrival") = checkSelDt
                        'ViewState("ArrShow") = "0"
                        BindArrivalDetails(dvMaiDetails, "1")
                    End If

                End If
            End If
        End If
        'End param 28/10/2018

        If dlArrTransferSearchResults.Items.Count >= 1 Then
            Session("SelectedArrival") = Nothing



            dt.Columns.Add("Cartypecode", GetType(String))
            dt.Columns.Add("Noofunits", GetType(String))
            dt.Columns.Add("Unitprice", GetType(String))
            dt.Columns.Add("Totalvalue", GetType(String))
            dt.Columns.Add("Selected", GetType(String))
            dt.Columns.Add("ComplimentaryCust", GetType(Integer))
            dt.Columns.Add("Vehiclename", GetType(String))
            dt.Columns.Add("Imagename", GetType(String))
            dt.Columns.Add("remarks", GetType(String))
            dt.Columns.Add("minpax", GetType(String))
            dt.Columns.Add("maxpax", GetType(String))
            dt.Columns.Add("maxadults", GetType(String))
            dt.Columns.Add("maxchild", GetType(String))
            dt.Columns.Add("paxcheckreqd", GetType(String))
            dt.Columns.Add("shuttle", GetType(String))
            dt.Columns.Add("airportbordercode", GetType(String))
            dt.Columns.Add("fromsectorgroupcode", GetType(String))
            dt.Columns.Add("sectorgroupcode", GetType(String))
            dt.Columns.Add("transferdate", GetType(String))
            dt.Columns.Add("adults", GetType(String))
            dt.Columns.Add("child", GetType(String))
            dt.Columns.Add("childagestring", GetType(String))
            dt.Columns.Add("units", GetType(String))
            dt.Columns.Add("unitprice", GetType(String))
            dt.Columns.Add("unitsalevalue", GetType(String))
            dt.Columns.Add("currcode", GetType(String))
            dt.Columns.Add("tplistcode", GetType(String))
            dt.Columns.Add("currentselection", GetType(String))

            dt.Columns.Add("preferredsupplier", GetType(String))
            dt.Columns.Add("unitcprice", GetType(String))
            dt.Columns.Add("unitcostvalue", GetType(String))
            dt.Columns.Add("tcplistcode", GetType(String))
            dt.Columns.Add("wlconvrate", GetType(String))
            dt.Columns.Add("wlmarkupperc", GetType(String))
            dt.Columns.Add("wlunitprice", GetType(String))
            dt.Columns.Add("wlunitsalevalue", GetType(String))
            dt.Columns.Add("wlcurrcode", GetType(String))


            dt.Columns.Add("CostTaxableValue", GetType(String))
            dt.Columns.Add("CostVATValue", GetType(String))
            dt.Columns.Add("VATPer", GetType(String))
            dt.Columns.Add("PriceWithTAX", GetType(String))
            dt.Columns.Add("PriceTaxableValue", GetType(String))
            dt.Columns.Add("PriceVATValue", GetType(String))
            dt.Columns.Add("PriceVATPer", GetType(String))
            dt.Columns.Add("PriceWithTAX1", GetType(String))

            Dim siccount As Integer = 0
            For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

                Dim lblairportborder As Label = gvRow.FindControl("lblairportborder")
                Dim lblfromsector As Label = gvRow.FindControl("lblfromsector")
                Dim lblsectorgroupcode As Label = gvRow.FindControl("lblsectorgroupcode")
                Dim lbltransferdate As Label = gvRow.FindControl("lbltransferdate")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblshuttle As Label = gvRow.FindControl("lblshuttle")
                Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
                Dim txttotal As TextBox = gvRow.FindControl("txttotal")
                Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
                Dim lblplistcode As Label = gvRow.FindControl("lblplistcode")

                Dim lblvehiclename As Label = gvRow.FindControl("lblvehiclename")
                Dim lblHotelImage As Label = gvRow.FindControl("lblHotelImage")
                Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                Dim lblminpax As Label = gvRow.FindControl("lblminpax")
                Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax")
                Dim lblmaxadult As Label = gvRow.FindControl("lblmaxadult")
                Dim lblmaxchild As Label = gvRow.FindControl("lblmaxchild")
                Dim lblpaxcheck As Label = gvRow.FindControl("lblpaxcheck")
                Dim lbltrfunit As Label = gvRow.FindControl("lbltrfunit")
                Dim lblprice As Label = gvRow.FindControl("lblprice")
                Dim lblvalue As Label = gvRow.FindControl("lblvalue")
                Dim lblcurrcode As Label = gvRow.FindControl("lblcurrcode")
                Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")

                Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
                Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
                Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
                Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
                Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
                Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)
                Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
                Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")

                Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkarrcompliment")
                Dim lblwlCurrCode As Label = gvRow.FindControl("lblwlCurrCode")


                Dim lblCostTaxableValue As Label = gvRow.FindControl("lblCostTaxableValue")
                Dim lblCostVATValue As Label = gvRow.FindControl("lblCostVATValue")
                Dim lblVATPer As Label = gvRow.FindControl("lblVATPer")
                Dim lblPriceWithTAX As Label = gvRow.FindControl("lblPriceWithTAX")
                Dim lblPriceTaxableValue As Label = gvRow.FindControl("lblPriceTaxableValue")
                Dim lblPriceVATValue As Label = gvRow.FindControl("lblPriceVATValue")
                Dim lblPriceVATPer As Label = gvRow.FindControl("lblPriceVATPer")
                Dim lblPriceWithTAX1 As Label = gvRow.FindControl("lblPriceWithTAX1")





                dr = dt.NewRow
                If chkbooknow.Checked = True Then

                    dr("Cartypecode") = lblcartypecode.Value
                    dr("Noofunits") = txtnoofunits.Text
                    dr("Unitprice") = txtunitprice.Text
                    dr("Totalvalue") = txttotal.Text
                    dr("Selected") = "1"
                    dr("ComplimentaryCust") = IIf(chkarrcompliment.Checked = True, 1, 0)
                    dr("Vehiclename") = lblvehiclename.Text
                    dr("Imagename") = lblHotelImage.Text
                    dr("remarks") = lblremarks.Text
                    dr("minpax") = lblminpax.Text
                    dr("maxpax") = lblmaxpax.Text
                    dr("maxadults") = lblmaxadult.Text
                    dr("maxchild") = lblmaxchild.Text
                    dr("paxcheckreqd") = lblpaxcheck.Text
                    dr("shuttle") = lblshuttle.Text
                    dr("airportbordercode") = lblairportborder.Text
                    dr("fromsectorgroupcode") = lblfromsector.Text
                    dr("sectorgroupcode") = lblsectorgroupcode.Text
                    dr("transferdate") = lbltransferdate.Text
                    dr("adults") = lbladults.Text
                    dr("child") = Val(lblchild.Text)
                    dr("childagestring") = lblchildagestring.Text

                    dr("units") = txtnoofunits.Text 'lbltrfunit.Text  '' Added shahul 03/06/18
                    dr("unitprice") = txtunitprice.Text ' lblprice.Text '' Added shahul 03/06/18
                    dr("unitsalevalue") = txttotal.Text 'lblvalue.Text  '' Added shahul 03/06/18

                    dr("currcode") = lblcurrcode.Text
                    dr("tplistcode") = lblplistcode.Text
                    dr("currentselection") = hdnselected.Value
                    dr("wlcurrcode") = lblwlCurrCode.Text

                    dr("CostTaxableValue") = lblCostTaxableValue.Text
                    dr("CostVATValue") = lblCostVATValue.Text
                    dr("VATPer") = lblVATPer.Text
                    dr("PriceWithTAX") = lblPriceWithTAX.Text
                    dr("PriceTaxableValue") = lblPriceTaxableValue.Text
                    dr("PriceVATValue") = lblPriceVATValue.Text
                    dr("PriceVATPer") = lblPriceVATPer.Text
                    dr("PriceWithTAX1") = lblPriceWithTAX1.Text

                    Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                    Dim dwlUnitprice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                    dwlUnitprice = dUnitprice * dWlMarkup



                    txtwlunitprice.Text = Math.Round(Val(dwlUnitprice))
                    txtwltotal.Text = Val(txtnoofunits.Text) * Val(dwlUnitprice)


                    dr("preferredsupplier") = lblpreferedsupplier.Text
                    dr("unitcprice") = lblunitcprice.Text
                    dr("unitcostvalue") = lblunitcostvalue.Text
                    dr("tcplistcode") = lbltcplistcode.Text
                    dr("wlconvrate") = lblwlconvrate.Text
                    dr("wlmarkupperc") = lblwlmarkupperc.Text
                    dr("wlunitprice") = txtwlunitprice.Text
                    dr("wlunitsalevalue") = txtwltotal.Text


                    If lblshuttle.Text = 1 Then
                        siccount += 1
                    End If

                    dt.Rows.Add(dr)


                End If
            Next




            Session.Add("SelectedArrival", dt)
            Dim dvArrivalDetails As DataView = New DataView(dt)

            'added param 28/10/2018
            If dt.Rows.Count = 0 Then
                myLinkButton1.Text = "Show More"
                ViewState("ArrShow") = "0"
            End If

            '''' Arrival Close

            Dim dsSearchResults As New DataSet
            'Dim lblshuttle1 As Label = TryCast(dlArrTransferSearchResults.Controls(dlArrTransferSearchResults.Controls.Count - 1).FindControl("lblshuttle"), Label)

            '' ADDED Shahul 
            If siccount = 0 Then
                Dim myLinkButton As LinkButton = TryCast(dlArrTransferSearchResults.Controls(dlArrTransferSearchResults.Controls.Count - 1).FindControl("lbArrShowMore"), LinkButton)



                dsSearchResults = Session("sDSTrfSearchResults")
                Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))

                If myLinkButton.Text = "Show More" Then
                    ViewState("ArrShow") = "1"
                    BindArrivalDetails(dvMaiDetails, "1")
                    myLinkButton.Text = "Show Less"

                Else

                    ViewState("ArrShow") = "0"
                    ' BindArrivalDetails(dvMaiDetails, "")
                    BindArrivalDetails(dvArrivalDetails, "")
                    myLinkButton.Text = "Show More"
                    dlArrTransferSearchResults.Focus()
                End If

                '''' Departure  Expand
                If dlDepTransferSearchResults.Items.Count > 0 Then
                    Dim mydepButton As LinkButton = TryCast(dlDepTransferSearchResults.Controls(dlDepTransferSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)
                    ' mydepButton.Attributes.Add("OnClick", "return true")


                    dsSearchResults = Session("sDSTrfSearchResults")
                    Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))


                    If mydepButton.Text = "Show More" Then
                        ViewState("DepShow") = "1"
                        BindDepartureDetails(dvDepMaiDetails, "1")

                        mydepButton.Text = "Show Less"
                    Else
                        ViewState("DepShow") = "0"
                        BindDepartureDetails(dvDepMaiDetails, "")
                        dlDepTransferSearchResults.Focus()
                        mydepButton.Text = "Show More"

                    End If
                End If
            End If



            '''


        End If
        ''''''''''''''

    End Sub
    Protected Sub btnSelectedDeparture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedDeparture.Click
        Dim dt As New DataTable
        Dim dr As DataRow



        '' ADDED Shahul 
        Dim sicadult As String = "", sicchild As String = ""
        Dim strQuery1 As String = ""
        Dim sicchildselected As Boolean = False
        Dim sicadultchecked As Boolean = False
        strQuery1 = "select  stuff((select distinct ',' + option_selected from reservation_parameters  u(nolock) where param_id in (1149,1150)  for xml path('')),1,1,'' ) "
        sicadult = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)
        If ddlTrfChild.SelectedValue > 0 Then
            sicchildselected = True
        End If
        For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
            Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

            If chkbooknow.Checked = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True Then
                sicadultchecked = True
            End If
            If chkbooknow.Checked = False And sicadultchecked = True And sicchildselected = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True Then
                chkbooknow.Checked = True
            End If

        Next
        ''''''

        'Added param 29/10/2018
        Dim DeplistDt As New DataTable
        DeplistDt.Columns.Add("Cartypecode", GetType(String))
        Dim myLinkButton1 As LinkButton = TryCast(dlDepTransferSearchResults.Controls(dlDepTransferSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)
        If Not Session("SelectedDeparture") Is Nothing Then
            Dim checkSelDt As DataTable = Session("SelectedDeparture")
            If checkSelDt.Rows.Count > 1 And dlDepTransferSearchResults.Items.Count = 1 Then
                For Each selDr As DataRow In checkSelDt.Rows
                    For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
                        Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                        Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                        If selDr("Cartypecode") = lblcartypecode.Value.Trim And chkbooknow.Checked = False Then
                            Dim depDr As DataRow = DeplistDt.NewRow
                            depDr("Cartypecode") = selDr("Cartypecode")
                            DeplistDt.Rows.Add(depDr)
                            Exit For
                        End If
                    Next
                Next

                If Not DeplistDt Is Nothing Then
                    For Each row As DataRow In DeplistDt.Rows
                        Dim cartypecode As String = row("Cartypecode")
                        Dim carDr = (From n In checkSelDt.AsEnumerable Where n.Field(Of String)("Cartypecode") = cartypecode Select n).FirstOrDefault
                        If Not carDr Is Nothing Then
                            carDr.Delete()
                        End If
                    Next

                    If checkSelDt.Rows.Count > 0 Then
                        Dim dsSearchResults As New DataSet
                        dsSearchResults = Session("sDSTrfSearchResults")
                        Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))
                        Session("SelectedDeparture") = checkSelDt
                        BindDepartureDetails(dvDepMaiDetails, "1")
                    End If

                End If
            End If
        End If
        'End param 29/10/2018

        If dlDepTransferSearchResults.Items.Count >= 1 Then
            Session("SelectedDeparture") = Nothing


            dt.Columns.Add("Cartypecode", GetType(String))
            dt.Columns.Add("Noofunits", GetType(String))
            dt.Columns.Add("Unitprice", GetType(String))
            dt.Columns.Add("Totalvalue", GetType(String))
            dt.Columns.Add("Selected", GetType(String))
            dt.Columns.Add("ComplimentaryCust", GetType(Integer))
            dt.Columns.Add("Vehiclename", GetType(String))
            dt.Columns.Add("Imagename", GetType(String))
            dt.Columns.Add("remarks", GetType(String))
            dt.Columns.Add("minpax", GetType(String))
            dt.Columns.Add("maxpax", GetType(String))
            dt.Columns.Add("maxadults", GetType(String))
            dt.Columns.Add("maxchild", GetType(String))
            dt.Columns.Add("paxcheckreqd", GetType(String))
            dt.Columns.Add("shuttle", GetType(String))
            dt.Columns.Add("airportbordercode", GetType(String))
            dt.Columns.Add("fromsectorgroupcode", GetType(String))
            dt.Columns.Add("sectorgroupcode", GetType(String))
            dt.Columns.Add("transferdate", GetType(String))
            dt.Columns.Add("adults", GetType(String))
            dt.Columns.Add("child", GetType(String))
            dt.Columns.Add("childagestring", GetType(String))
            dt.Columns.Add("units", GetType(String))
            dt.Columns.Add("unitprice", GetType(String))
            dt.Columns.Add("unitsalevalue", GetType(String))
            dt.Columns.Add("currcode", GetType(String))
            dt.Columns.Add("tplistcode", GetType(String))
            dt.Columns.Add("currentselection", GetType(String))

            dt.Columns.Add("preferredsupplier", GetType(String))
            dt.Columns.Add("unitcprice", GetType(String))
            dt.Columns.Add("unitcostvalue", GetType(String))
            dt.Columns.Add("tcplistcode", GetType(String))
            dt.Columns.Add("wlconvrate", GetType(String))
            dt.Columns.Add("wlmarkupperc", GetType(String))
            dt.Columns.Add("wlunitprice", GetType(String))
            dt.Columns.Add("wlunitsalevalue", GetType(String))
            dt.Columns.Add("wlcurrcode", GetType(String))



            dt.Columns.Add("CostTaxableValue", GetType(String))
            dt.Columns.Add("CostVATValue", GetType(String))
            dt.Columns.Add("VATPer", GetType(String))
            dt.Columns.Add("PriceWithTAX", GetType(String))
            dt.Columns.Add("PriceTaxableValue", GetType(String))
            dt.Columns.Add("PriceVATValue", GetType(String))
            dt.Columns.Add("PriceVATPer", GetType(String))
            dt.Columns.Add("PriceWithTAX1", GetType(String))

            Dim siccount As Integer = 0
            For Each gvRow As DataListItem In dlDepTransferSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

                dr = dt.NewRow
                If chkbooknow.Checked = True Then


                    Dim lblairportborder As Label = gvRow.FindControl("lblairportborder")
                    Dim lblfromsector As Label = gvRow.FindControl("lblfromsector")
                    Dim lblsectorgroupcode As Label = gvRow.FindControl("lblsectorgroupcode")
                    Dim lbltransferdate As Label = gvRow.FindControl("lbltransferdate")
                    Dim lbladults As Label = gvRow.FindControl("lbladults")
                    Dim lblchild As Label = gvRow.FindControl("lblchild")
                    Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                    Dim lblshuttle As Label = gvRow.FindControl("lblshuttle")
                    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
                    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
                    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
                    Dim lblplistcode As Label = gvRow.FindControl("lblplistcode")


                    Dim lblvehiclename As Label = gvRow.FindControl("lblvehiclename")
                    Dim lblHotelImage As Label = gvRow.FindControl("lblHotelImage")
                    Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                    Dim lblminpax As Label = gvRow.FindControl("lblminpax")
                    Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax")
                    Dim lblmaxadult As Label = gvRow.FindControl("lblmaxadult")
                    Dim lblmaxchild As Label = gvRow.FindControl("lblmaxchild")
                    Dim lblpaxcheck As Label = gvRow.FindControl("lblpaxcheck")
                    Dim lbltrfunit As Label = gvRow.FindControl("lbltrfunit")
                    Dim lblprice As Label = gvRow.FindControl("lblprice")
                    Dim lblvalue As Label = gvRow.FindControl("lblvalue")
                    Dim lblcurrcode As Label = gvRow.FindControl("lblcurrcode")

                    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                    Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkdepcompliment")
                    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")


                    Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
                    Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
                    Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
                    Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
                    Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                    Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                    Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
                    Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)
                    Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
                    Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")
                    Dim lblwlCurrCode As Label = gvRow.FindControl("lblwlCurrCode")


                    Dim lblCostTaxableValue As Label = gvRow.FindControl("lblCostTaxableValue")
                    Dim lblCostVATValue As Label = gvRow.FindControl("lblCostVATValue")
                    Dim lblVATPer As Label = gvRow.FindControl("lblVATPer")
                    Dim lblPriceWithTAX As Label = gvRow.FindControl("lblPriceWithTAX")
                    Dim lblPriceTaxableValue As Label = gvRow.FindControl("lblPriceTaxableValue")
                    Dim lblPriceVATValue As Label = gvRow.FindControl("lblPriceVATValue")
                    Dim lblPriceVATPer As Label = gvRow.FindControl("lblPriceVATPer")
                    Dim lblPriceWithTAX1 As Label = gvRow.FindControl("lblPriceWithTAX1")

                    dr("Cartypecode") = lblcartypecode.Value
                    dr("Noofunits") = txtnoofunits.Text
                    dr("Unitprice") = txtunitprice.Text
                    dr("Totalvalue") = txttotal.Text
                    dr("Selected") = "1"
                    dr("ComplimentaryCust") = IIf(chkarrcompliment.Checked = True, 1, 0)
                    dr("Vehiclename") = lblvehiclename.Text
                    dr("Imagename") = lblHotelImage.Text
                    dr("remarks") = lblremarks.Text
                    dr("minpax") = lblminpax.Text
                    dr("maxpax") = lblmaxpax.Text
                    dr("maxadults") = lblmaxadult.Text
                    dr("maxchild") = lblmaxchild.Text
                    dr("paxcheckreqd") = lblpaxcheck.Text
                    dr("shuttle") = lblshuttle.Text
                    dr("airportbordercode") = lblairportborder.Text
                    dr("fromsectorgroupcode") = lblfromsector.Text
                    dr("sectorgroupcode") = lblsectorgroupcode.Text
                    dr("transferdate") = lbltransferdate.Text
                    dr("adults") = lbladults.Text
                    dr("child") = Val(lblchild.Text)
                    dr("childagestring") = lblchildagestring.Text

                    dr("units") = txtnoofunits.Text ' lbltrfunit.Text '' Added shahul 03/06/18
                    dr("unitprice") = txtunitprice.Text ' lblprice.Text '' Added shahul 03/06/18
                    dr("unitsalevalue") = txttotal.Text ' lblvalue.Text '' Added shahul 03/06/18


                    dr("currcode") = lblcurrcode.Text
                    dr("tplistcode") = lblplistcode.Text
                    dr("currentselection") = hdnselected.Value
                    dr("wlcurrcode") = lblwlCurrCode.Text




                    dr("CostTaxableValue") = lblCostTaxableValue.Text
                    dr("CostVATValue") = lblCostVATValue.Text
                    dr("VATPer") = lblVATPer.Text
                    dr("PriceWithTAX") = lblPriceWithTAX.Text
                    dr("PriceTaxableValue") = lblPriceTaxableValue.Text
                    dr("PriceVATValue") = lblPriceVATValue.Text
                    dr("PriceVATPer") = lblPriceVATPer.Text
                    dr("PriceWithTAX1") = lblPriceWithTAX1.Text



                    Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                    Dim dwlUnitprice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                    dwlUnitprice = dUnitprice * dWlMarkup

                    txtwlunitprice.Text = Math.Round(Val(dwlUnitprice))
                    txtwltotal.Text = Val(txtnoofunits.Text) * Val(dwlUnitprice)

                    dr("preferredsupplier") = lblpreferedsupplier.Text
                    dr("unitcprice") = lblunitcprice.Text
                    dr("unitcostvalue") = lblunitcostvalue.Text
                    dr("tcplistcode") = lbltcplistcode.Text
                    dr("wlconvrate") = lblwlconvrate.Text
                    dr("wlmarkupperc") = lblwlmarkupperc.Text
                    dr("wlunitprice") = txtwlunitprice.Text
                    dr("wlunitsalevalue") = txtwltotal.Text



                    If lblshuttle.Text = 1 Then
                        siccount += 1
                    End If

                    dt.Rows.Add(dr)
                End If
            Next

            Session.Add("SelectedDeparture", dt)
            Dim dvDepartureDetails As DataView = New DataView(dt)

            'added param 29/10/2018
            If dt.Rows.Count = 0 Then
                myLinkButton1.Text = "Show More"
                ViewState("DepShow") = "0"
            End If

            Dim dsSearchResults As New DataSet


            If siccount = 0 Then

                ''' Departure close

                Dim myLinkButtondep As LinkButton = TryCast(dlDepTransferSearchResults.Controls(dlDepTransferSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)

                dsSearchResults = Session("sDSTrfSearchResults")
                Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))


                If myLinkButtondep.Text = "Show More" Then
                    ViewState("DepShow") = "1"
                    BindDepartureDetails(dvDepMaiDetails, "1")

                    myLinkButtondep.Text = "Show Less"
                Else
                    ViewState("DepShow") = "0"
                    BindDepartureDetails(dvDepartureDetails, "")
                    dlDepTransferSearchResults.Focus()
                    myLinkButtondep.Text = "Show More"

                End If
                ''''''''''''''''''

                ''' Inter Hotel Expand

                If dlShifting.Items.Count > 0 Then



                    Dim myLinkButton As LinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)
                    dsSearchResults = Session("sDSTrfSearchResults")
                    Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))


                    If myLinkButton.Text = "Show More" Then
                        ViewState("InterShow") = "1"

                        BindInterhoteleDetails(dvMaiDetails, "1")
                        myLinkButton.Text = "Show Less"

                    Else
                        ViewState("InterShow") = "0"

                        BindInterhoteleDetails(dvMaiDetails, "")
                        myLinkButton.Text = "Show More"
                        dlShifting.Focus()
                    End If
                End If

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
            objclsUtilities.WriteErrorLog("TransferSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnSelectedInter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedInter.Click
        Dim dt As New DataTable
        Dim dr As DataRow


        '' ADDED Shahul 
        Dim sicadult As String = "", sicchild As String = ""
        Dim strQuery1 As String = ""
        Dim sicchildselected As Boolean = False
        Dim sicadultchecked As Boolean = False
        strQuery1 = "select  stuff((select distinct ',' + option_selected from reservation_parameters  u(nolock) where param_id in (1149,1150)  for xml path('')),1,1,'' ) "
        sicadult = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)
        If ddlTrfChild.SelectedValue > 0 Then
            sicchildselected = True
        End If
        For Each gvRow As DataListItem In dlShifting.Items
            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
            Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")

            If chkbooknow.Checked = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True Then
                sicadultchecked = True
            End If
            If chkbooknow.Checked = False And sicadultchecked = True And sicchildselected = True And Convert.ToString(sicadult).ToUpper.Trim.Contains(lblcartypecode.Value.ToUpper.Trim) = True Then
                chkbooknow.Checked = True
            End If

        Next
        ''''''

        'Added param 29/10/2018
        Dim InterlistDt As New DataTable
        InterlistDt.Columns.Add("Cartypecode", GetType(String))
        Dim myLinkButton1 As LinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)
        If Not Session("SelectedInter") Is Nothing Then
            Dim checkSelDt As DataTable = Session("SelectedInter")
            If checkSelDt.Rows.Count > 1 And dlShifting.Items.Count = 1 Then
                For Each selDr As DataRow In checkSelDt.Rows
                    For Each gvRow As DataListItem In dlShifting.Items
                        Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                        Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                        If selDr("Cartypecode") = lblcartypecode.Value.Trim And chkbooknow.Checked = False Then
                            Dim interDr As DataRow = InterlistDt.NewRow
                            interDr("Cartypecode") = selDr("Cartypecode")
                            InterlistDt.Rows.Add(interDr)
                            Exit For
                        End If
                    Next
                Next

                If Not InterlistDt Is Nothing Then
                    For Each row As DataRow In InterlistDt.Rows
                        Dim cartypecode As String = row("Cartypecode")
                        Dim carDr = (From n In checkSelDt.AsEnumerable Where n.Field(Of String)("Cartypecode") = cartypecode Select n).FirstOrDefault
                        If Not carDr Is Nothing Then
                            carDr.Delete()
                        End If
                    Next

                    If checkSelDt.Rows.Count > 0 Then
                        Dim dsSearchResults As New DataSet
                        dsSearchResults = Session("sDSTrfSearchResults")
                        Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))
                        Session("SelectedInter") = checkSelDt
                        BindInterhoteleDetails(dvMaiDetails, "1")
                    End If

                End If
            End If
        End If
        'End param 29/10/2018

        If dlShifting.Items.Count >= 1 Then
            Session("SelectedInter") = Nothing

            dt.Columns.Add("Cartypecode", GetType(String))
            dt.Columns.Add("Noofunits", GetType(String))
            dt.Columns.Add("Unitprice", GetType(String))
            dt.Columns.Add("Totalvalue", GetType(String))
            dt.Columns.Add("Selected", GetType(String))
            dt.Columns.Add("ComplimentaryCust", GetType(Integer))
            dt.Columns.Add("Vehiclename", GetType(String))
            dt.Columns.Add("Imagename", GetType(String))
            dt.Columns.Add("remarks", GetType(String))
            dt.Columns.Add("minpax", GetType(String))
            dt.Columns.Add("maxpax", GetType(String))
            dt.Columns.Add("maxadults", GetType(String))
            dt.Columns.Add("maxchild", GetType(String))
            dt.Columns.Add("paxcheckreqd", GetType(String))
            dt.Columns.Add("shuttle", GetType(String))
            dt.Columns.Add("airportbordercode", GetType(String))
            dt.Columns.Add("fromsectorgroupcode", GetType(String))
            dt.Columns.Add("sectorgroupcode", GetType(String))
            dt.Columns.Add("transferdate", GetType(String))
            dt.Columns.Add("adults", GetType(String))
            dt.Columns.Add("child", GetType(String))
            dt.Columns.Add("childagestring", GetType(String))
            dt.Columns.Add("units", GetType(String))
            dt.Columns.Add("unitprice", GetType(String))
            dt.Columns.Add("unitsalevalue", GetType(String))
            dt.Columns.Add("currcode", GetType(String))
            dt.Columns.Add("tplistcode", GetType(String))
            dt.Columns.Add("currentselection", GetType(String))

            dt.Columns.Add("preferredsupplier", GetType(String))
            dt.Columns.Add("unitcprice", GetType(String))
            dt.Columns.Add("unitcostvalue", GetType(String))
            dt.Columns.Add("tcplistcode", GetType(String))
            dt.Columns.Add("wlconvrate", GetType(String))
            dt.Columns.Add("wlmarkupperc", GetType(String))
            dt.Columns.Add("wlunitprice", GetType(String))
            dt.Columns.Add("wlunitsalevalue", GetType(String))
            dt.Columns.Add("wlcurrcode", GetType(String))


            dt.Columns.Add("CostTaxableValue", GetType(String))
            dt.Columns.Add("CostVATValue", GetType(String))
            dt.Columns.Add("VATPer", GetType(String))
            dt.Columns.Add("PriceWithTAX", GetType(String))
            dt.Columns.Add("PriceTaxableValue", GetType(String))
            dt.Columns.Add("PriceVATValue", GetType(String))
            dt.Columns.Add("PriceVATPer", GetType(String))
            dt.Columns.Add("PriceWithTAX1", GetType(String))

            Dim siccount As Integer = 0
            For Each gvRow As DataListItem In dlShifting.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

                dr = dt.NewRow
                If chkbooknow.Checked = True Then


                    Dim lblairportborder As Label = gvRow.FindControl("lblairportborder")
                    Dim lblfromsector As Label = gvRow.FindControl("lblfromsector")
                    Dim lblsectorgroupcode As Label = gvRow.FindControl("lblsectorgroupcode")
                    Dim lbltransferdate As Label = gvRow.FindControl("lbltransferdate")
                    Dim lbladults As Label = gvRow.FindControl("lbladults")
                    Dim lblchild As Label = gvRow.FindControl("lblchild")
                    Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                    Dim lblshuttle As Label = gvRow.FindControl("lblshuttle")
                    Dim txtnoofunits As TextBox = gvRow.FindControl("txtnoofunits")
                    Dim txttotal As TextBox = gvRow.FindControl("txttotal")
                    Dim txtunitprice As TextBox = gvRow.FindControl("txtunitprice")
                    Dim lblplistcode As Label = gvRow.FindControl("lblplistcode")

                    Dim lblvehiclename As Label = gvRow.FindControl("lblvehiclename")
                    Dim lblHotelImage As Label = gvRow.FindControl("lblHotelImage")
                    Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                    Dim lblminpax As Label = gvRow.FindControl("lblminpax")
                    Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax")
                    Dim lblmaxadult As Label = gvRow.FindControl("lblmaxadult")
                    Dim lblmaxchild As Label = gvRow.FindControl("lblmaxchild")
                    Dim lblpaxcheck As Label = gvRow.FindControl("lblpaxcheck")
                    Dim lbltrfunit As Label = gvRow.FindControl("lbltrfunit")
                    Dim lblprice As Label = gvRow.FindControl("lblprice")
                    Dim lblvalue As Label = gvRow.FindControl("lblvalue")
                    Dim lblcurrcode As Label = gvRow.FindControl("lblcurrcode")

                    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")

                    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                    Dim chkarrcompliment As CheckBox = gvRow.FindControl("chkintercompliment")


                    Dim lblpreferedsupplier As Label = CType(gvRow.FindControl("lblpreferedsupplier"), Label)
                    Dim lblunitcprice As Label = CType(gvRow.FindControl("lblunitcprice"), Label)
                    Dim lblunitcostvalue As Label = CType(gvRow.FindControl("lblunitcostvalue"), Label)
                    Dim lbltcplistcode As Label = CType(gvRow.FindControl("lbltcplistcode"), Label)
                    Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                    Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                    Dim lblwlunitprice_grid As Label = CType(gvRow.FindControl("lblwlunitprice_grid"), Label)
                    Dim lblwlunitsalevalue As Label = CType(gvRow.FindControl("lblwlunitsalevalue"), Label)
                    Dim txtwlunitprice As TextBox = gvRow.FindControl("txtwlunitprice")
                    Dim txtwltotal As TextBox = gvRow.FindControl("txtwltotal")
                    Dim lblwlCurrCode As Label = gvRow.FindControl("lblwlCurrCode")


                    Dim lblCostTaxableValue As Label = gvRow.FindControl("lblCostTaxableValue")
                    Dim lblCostVATValue As Label = gvRow.FindControl("lblCostVATValue")
                    Dim lblVATPer As Label = gvRow.FindControl("lblVATPer")
                    Dim lblPriceWithTAX As Label = gvRow.FindControl("lblPriceWithTAX")
                    Dim lblPriceTaxableValue As Label = gvRow.FindControl("lblPriceTaxableValue")
                    Dim lblPriceVATValue As Label = gvRow.FindControl("lblPriceVATValue")
                    Dim lblPriceVATPer As Label = gvRow.FindControl("lblPriceVATPer")
                    Dim lblPriceWithTAX1 As Label = gvRow.FindControl("lblPriceWithTAX1")


                    dr("Cartypecode") = lblcartypecode.Value
                    dr("Noofunits") = txtnoofunits.Text
                    dr("Unitprice") = txtunitprice.Text
                    dr("Totalvalue") = txttotal.Text
                    dr("Selected") = "1"
                    dr("ComplimentaryCust") = IIf(chkarrcompliment.Checked = True, 1, 0)
                    dr("Vehiclename") = lblvehiclename.Text
                    dr("Imagename") = lblHotelImage.Text
                    dr("remarks") = lblremarks.Text
                    dr("minpax") = lblminpax.Text
                    dr("maxpax") = lblmaxpax.Text
                    dr("maxadults") = lblmaxadult.Text
                    dr("maxchild") = lblmaxchild.Text
                    dr("paxcheckreqd") = lblpaxcheck.Text
                    dr("shuttle") = lblshuttle.Text
                    dr("airportbordercode") = lblairportborder.Text
                    dr("fromsectorgroupcode") = lblfromsector.Text
                    dr("sectorgroupcode") = lblsectorgroupcode.Text
                    dr("transferdate") = lbltransferdate.Text
                    dr("adults") = lbladults.Text
                    dr("child") = Val(lblchild.Text)
                    dr("childagestring") = lblchildagestring.Text

                    dr("units") = txtnoofunits.Text 'lbltrfunit.Text '' Added shahul 03/06/18
                    dr("unitprice") = txtunitprice.Text 'lblprice.Text '' Added shahul 03/06/18
                    dr("unitsalevalue") = txttotal.Text 'lblvalue.Text '' Added shahul 03/06/18

                    dr("currcode") = lblcurrcode.Text
                    dr("tplistcode") = lblplistcode.Text
                    dr("currentselection") = hdnselected.Value
                    dr("wlcurrcode") = lblwlCurrCode.Text
                    Dim dUnitprice As Decimal = IIf(lblprice.Text = "", "0.00", lblprice.Text)
                    Dim dwlUnitprice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                    dwlUnitprice = dUnitprice * dWlMarkup

                    txtwlunitprice.Text = Math.Round(Val(dwlUnitprice))
                    txtwltotal.Text = Val(txtnoofunits.Text) * Val(dwlUnitprice)

                    dr("preferredsupplier") = lblpreferedsupplier.Text
                    dr("unitcprice") = lblunitcprice.Text
                    dr("unitcostvalue") = lblunitcostvalue.Text
                    dr("tcplistcode") = lbltcplistcode.Text
                    dr("wlconvrate") = lblwlconvrate.Text
                    dr("wlmarkupperc") = lblwlmarkupperc.Text
                    dr("wlunitprice") = txtwlunitprice.Text
                    dr("wlunitsalevalue") = txtwltotal.Text

                    dr("CostTaxableValue") = lblCostTaxableValue.Text
                    dr("CostVATValue") = lblCostVATValue.Text
                    dr("VATPer") = lblVATPer.Text
                    dr("PriceWithTAX") = lblPriceWithTAX.Text
                    dr("PriceTaxableValue") = lblPriceTaxableValue.Text
                    dr("PriceVATValue") = lblPriceVATValue.Text
                    dr("PriceVATPer") = lblPriceVATPer.Text
                    dr("PriceWithTAX1") = lblPriceWithTAX1.Text

                    If lblshuttle.Text = 1 Then
                        siccount += 1
                    End If

                    dt.Rows.Add(dr)
                End If
            Next

            Session.Add("SelectedInter", dt)
            Dim dvInterDetails As DataView = New DataView(dt)

            'added param 29/10/2018
            If dt.Rows.Count = 0 Then
                myLinkButton1.Text = "Show More"
                ViewState("InterShow") = "0"
            End If

            Dim dsSearchResults As New DataSet

            If siccount = 0 Then

                ''' Inter close

                Dim myLinkButton As LinkButton = TryCast(dlShifting.Controls(dlShifting.Controls.Count - 1).FindControl("lbinterShowMore"), LinkButton)

                dsSearchResults = Session("sDSTrfSearchResults")
                Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))


                If myLinkButton.Text = "Show More" Then
                    ViewState("InterShow") = "1"

                    BindInterhoteleDetails(dvMaiDetails, "1")
                    myLinkButton.Text = "Show Less"

                Else
                    ViewState("InterShow") = "0"

                    BindInterhoteleDetails(dvInterDetails, "")
                    myLinkButton.Text = "Show More"
                    dlShifting.Focus()
                End If

            End If


        End If

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
