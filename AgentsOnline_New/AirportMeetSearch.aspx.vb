Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System

Partial Class AirportMeetSearch
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLMASearch As New BLLMASearch
    Dim objBLLHotelSearch As New BLLHotelSearch

    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Dim objResParam As New ReservationParameters





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
                objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub
    Private Sub BindMAflightclass()
        Dim strQuery As String = ""
        strQuery = "select flightclscode,flightclsname from flightclsmast(nolock) where active=1"
        objclsUtilities.FillDropDownList(ddlMAArrFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddlMADepFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddltranarrflightclass, strQuery, "flightclscode", "flightclsname", True, "--")
        objclsUtilities.FillDropDownList(ddlMAtrandepflightlass, strQuery, "flightclscode", "flightclsname", True, "--")
        ddlMAArrFlightClass.SelectedIndex = 2
        ddlMADepFlightClass.SelectedIndex = 2

        ddltranarrflightclass.SelectedIndex = 2
        ddlMAtrandepflightlass.SelectedIndex = 2
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
            Return Arrivalflight
        Catch ex As Exception
            Return Arrivalflight
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

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster(nolock) where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername,airportbordercode  from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"

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
    Public Shared Function GetMATranArrivalpickup(ByVal prefixText As String) As List(Of String)

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
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
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
            Return Departureflight
        Catch ex As Exception
            Return Departureflight
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
    Public Shared Function GetArrivalflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)


        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Arrivalflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim strRequestId As String = ""
            strRequestId = GetExistingRequestId()


            Dim objBLLTransferSearch = New BLLTransferSearch
            If Not HttpContext.Current.Session("sobjBLLTransferSearchActive") Is Nothing Then
                objBLLTransferSearch = HttpContext.Current.Session("sobjBLLTransferSearchActive")
                Dim dt As DataTable

                dt = objBLLTransferSearch.GetAirportTerminal(objBLLTransferSearch.OBRequestId)
                If dt.Rows.Count > 0 And 1 = 2 Then '1=2 is added since not able to change flight no. in edit mode 'changed by mohamed on 17/09/2018 

                    strSqlQry = "select v.flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,v.flightcode from view_flightmast_arrival v, booking_transferstemp t(nolock)  where  " _
                        & " v.airportbordercode=t.airportbordercode and t.requestid='" & objBLLTransferSearch.OBRequestId & "' and  convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) and t.transfertype='ARRIVAL' and v.flightcode like  '" & prefixText & "%' order by replace(v.Flightcode,' ','') "
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%'  order by replace(Flightcode,' ','') "
                End If
            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
            End If


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



            'If strRequestId <> "" Then
            '    '  strSqlQry = "select arrflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,arrflightcode flightcode from booking_guest_flightstemp where requestid='" & strRequestId & "' "

            '    Dim SqlConn As New SqlConnection
            '    Dim myDataAdapter As New SqlDataAdapter
            '    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            '    'Open connection
            '    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            '    myDataAdapter.Fill(myDS)

            '    If myDS.Tables(0).Rows.Count > 0 Then
            '        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
            '            Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
            '        Next
            '    Else
            '        strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "


            '        SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            '        'Open connection
            '        myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            '        myDataAdapter.Fill(myDS)

            '        If myDS.Tables(0).Rows.Count > 0 Then
            '            For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
            '                Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
            '            Next
            '        End If
            '    End If

            'Else
            '    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

            '    Dim SqlConn As New SqlConnection
            '    Dim myDataAdapter As New SqlDataAdapter
            '    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            '    'Open connection
            '    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            '    myDataAdapter.Fill(myDS)

            '    If myDS.Tables(0).Rows.Count > 0 Then
            '        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
            '            Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
            '        Next
            '    End If

            'End If


            Return Arrivalflight
        Catch ex As Exception
            Return Arrivalflight
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
    Public Shared Function GetDepartureflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Departureflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim strRequestId As String = ""
            strRequestId = GetExistingRequestId()

            Dim objBLLTransferSearch = New BLLTransferSearch
            If Not HttpContext.Current.Session("sobjBLLTransferSearchActive") Is Nothing Then
                objBLLTransferSearch = HttpContext.Current.Session("sobjBLLTransferSearchActive")
                Dim dt As DataTable

                dt = objBLLTransferSearch.GetAirportTerminal(objBLLTransferSearch.OBRequestId)
                If dt.Rows.Count > 0 And 1 = 2 Then '1=2 is added since not able to change flight no. in edit mode 'changed by mohamed on 17/09/2018

                    strSqlQry = "select v.flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,v.flightcode from view_flightmast_departure v ,  " _
                        & " booking_transferstemp t(nolock)  where v.airportbordercode=t.airportbordercode and t.requestid='" & objBLLTransferSearch.OBRequestId & "' and  convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) and t.transfertype='DEPARTURE'  and v.flightcode like  '" & prefixText & "%' "
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "

                End If
            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "
            End If

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

            'If strRequestId <> "" Then
            '    strSqlQry = "select depflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,depflightcode flightcode from booking_guest_flightstemp where requestid='" & strRequestId & "' "
            '    Dim SqlConn As New SqlConnection
            '    Dim myDataAdapter As New SqlDataAdapter
            '    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            '    'Open connection
            '    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            '    myDataAdapter.Fill(myDS)

            '    If myDS.Tables(0).Rows.Count > 0 Then
            '        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
            '            Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
            '        Next
            '    Else
            '        strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
            '        SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            '        'Open connection
            '        myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            '        myDataAdapter.Fill(myDS)

            '        If myDS.Tables(0).Rows.Count > 0 Then
            '            For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
            '                Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
            '            Next
            '        End If
            '    End If
            'Else
            '    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
            '    Dim SqlConn As New SqlConnection
            '    Dim myDataAdapter As New SqlDataAdapter
            '    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            '    'Open connection
            '    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            '    myDataAdapter.Fill(myDS)

            '    If myDS.Tables(0).Rows.Count > 0 Then
            '        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
            '            Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
            '        Next
            '    End If
            'End If
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
    Public Shared Function GetMADeppickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim dropoffs As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  partycode,partyname from partymast (nolock) where active=1 and partyname like  '" & prefixText & "%' order by partyname "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
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
    Public Shared Function GetMADepairportdrop(ByVal prefixText As String) As List(Of String)

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
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
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
    Public Shared Function GetMACountryDetails(ByVal CustCode As String) As String

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
            myDataAdapter.Fill(myDS, "MACountries")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
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
    Private Sub BindAirportMeetTypes(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            chkHotelStars.DataSource = dataTable
            chkHotelStars.DataTextField = "airportmatypename"
            chkHotelStars.DataValueField = "airportmatypecode"
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

        'Dim objDALTransferSearch As New DALTransferSearch
        Dim objBLLTransferSearch = New BLLTransferSearch
        'objBLLTransferSearch = Session("sobjBLLTransferSearchActive")

        'Dim objDALHotelSearch As New DALHotelSearch
        'Dim objBLLHotelSearch = New BLLHotelSearch
        'objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
        Dim hotelcount As Integer
        If Not Session("sRequestId") Is Nothing Then
            Dim ds As DataSet
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            ds = objBLLCommonFuntions.GetTempFullBookingDetails(Session("sRequestId"))
            If Not ds Is Nothing Then
                If ds.Tables(3).Rows.Count > 0 Then


                    dsTrfdetails = objBLLTransferSearch.FillTransferDetails(Session("sRequestId"))


                    If dsTrfdetails.Tables.Count > 0 Then
                        ' If recordexists = "" Then
                        BindHotelArrivalDetails(dsTrfdetails.Tables(1))
                        BindHotelDepartureDetails(dsTrfdetails.Tables(2))
                        'End If

                    End If


                Else
                    If ds.Tables(1).Rows.Count > 0 Or ds.Tables(6).Rows.Count > 0 Then

                        '  strQuery = "select count(distinct partycode) from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
                        hotelcount = ds.Tables(1).Rows.Count + ds.Tables(6).Rows.Count


                        strQuery = "select 't' from booking_transferstemp where  requestid='" & Session("sRequestId") & "'"
                        recordexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        dsTrfdetails = objBLLHotelSearch.FillTransferDetails(Session("sRequestId"))


                        If dsTrfdetails.Tables.Count > 0 Then
                            If recordexists = "" Then
                                BindHotelArrivalDetails(dsTrfdetails.Tables(1))
                                BindHotelDepartureDetails(dsTrfdetails.Tables(2))
                            End If

                        End If

                    End If
                End If
            End If

        End If


    End Sub
    Protected Sub lbDepShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))


            If myLinkButton.Text = "Show More" Then
                ViewState("DepShow") = "1"
                BindDepartureDetails(dvDepMaiDetails, "1")

                myLinkButton.Text = "Show Less"
            Else
                ViewState("DepShow") = "0"
                BindDepartureDetails(dvDepMaiDetails, "")
                dlMADepartureSearchResults.Focus()
                myLinkButton.Text = "Show More"

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: lbDepShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub lbArrShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)

            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))

            Dim chkbooknow As CheckBox



            If myLinkButton.Text = "Show More" Then
                ViewState("ArrShow") = "1"
                BindArrivalDetails(dvMaiDetails, "1")
                myLinkButton.Text = "Show Less"

            Else

                'Dim exists As Boolean = dvMaiDetails.ToTable().Columns.Contains("Selected")
                'If exists = False Then
                '    dvMaiDetails.Table.Columns.Add(New DataColumn("Selected", GetType(String)))
                '    dvMaiDetails.ToTable.AcceptChanges()
                'End If



                'For Each gvRow As DataListItem In dlArrTransferSearchResults.Items
                '    chkbooknow = gvRow.FindControl("chkbooknow")
                '    Dim lblcartypecode As HiddenField = gvRow.FindControl("lblcartypecode")
                '    If chkbooknow.Checked = True Then

                '        For i = 0 To dvMaiDetails.Table.Rows.Count - 1
                '            If lblcartypecode.Value = dvMaiDetails.Table.Rows(i)("cartypecode") Then
                '                dvMaiDetails.Table.Rows(i)("Selected") = 1

                '            End If
                '        Next


                '    End If
                'Next


                ViewState("ArrShow") = "0"
                BindArrivalDetails(dvMaiDetails, "")
                myLinkButton.Text = "Show More"
                dlMAArrivalSearchResults.Focus()
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportmeetSearch.aspx :: lbArrShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub BindHotelDepartureDetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkDeparture.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE", True, False)
            txtMADeparturedate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlMADepFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtMADepairportdropcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = " select   a.airportbordername from flightmast f,airportbordersmaster a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtMADepairportdrop.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMADeppickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = "select partyname from partymast where active=1 and partycode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtMADeppickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtDepartureFlightCode.Text = dataTable.Rows(0)("flightcode")
            txtDepartureFlight.Text = dataTable.Rows(0)("flightcode")
            txtDepartureTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub BindHotelArrivalDetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkarrival.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL", True, False)
            txtMAArrivaldate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlMAArrFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtMAArrDropoffcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = "select partyname from partymast where active=1 and partycode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtMAArrDropoff.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMAArrivalpickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = " select   a.airportbordername from flightmast f,airportbordersmaster a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtMAArrivalpickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtArrivalflightCode.Text = dataTable.Rows(0)("flightcode")
            txtArrivalflight.Text = dataTable.Rows(0)("flightcode")
            txtArrivalTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub
    Private Sub Amendheaderfill()
        Dim dt As DataTable
        Dim dtpax As DataTable
        Dim strQuery As String = ""
        Try
            dt = objBLLMASearch.GetEditBookingDetails(Session("sEditRequestId"), Request.QueryString("ALineNo"))
            If dt.Rows.Count > 0 Then

                txtMACustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtMACustomer.Text = dt.Rows(0)("agentname").ToString
                txtMASourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtMASourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                chkMAoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                'strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sEditRequestId") & "')"
                'dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                'If dtpax.Rows.Count > 0 Then
                ddlMAAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)

                If Val(dt.Rows(0)("child").ToString) <> 0 Then
                    ddlMAChild.SelectedValue = dt.Rows(0)("child").ToString

                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtMAChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtMAChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtMAChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtMAChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtMAChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtMAChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtMAChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtMAChild8.Text = strChildAges(7)
                    End If
                End If
                '  End If


            End If
            If dt.Rows(0)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                chkarrival.Checked = True
                chkDeparture.Checked = False
                chktransit.Checked = False
                txtMAArrivaldate.Text = dt.Rows(0)("airportmadate").ToString
                txtArrivalflight.Text = dt.Rows(0)("flightcode").ToString
                txtArrivalflightCode.Text = dt.Rows(0)("flightcode").ToString
                txtArrivalTime.Text = dt.Rows(0)("flighttime").ToString
                txtMAArrivalpickupcode.Text = dt.Rows(0)("airportbordercode").ToString
                txtMAArrivalpickup.Text = dt.Rows(0)("airportbordername").ToString
                ddlMAArrFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString

                chkDeparture.Enabled = False
                chktransit.Enabled = False
            End If
            If dt.Rows(0)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                chkDeparture.Checked = True
                chkarrival.Checked = False
                chktransit.Checked = False
                txtMADeparturedate.Text = dt.Rows(0)("airportmadate").ToString
                txtDepartureFlight.Text = dt.Rows(0)("flightcode").ToString
                txtDepartureFlightCode.Text = dt.Rows(0)("flightcode").ToString
                txtDepartureTime.Text = dt.Rows(0)("flighttime").ToString
                txtMADepairportdropcode.Text = dt.Rows(0)("airportbordercode").ToString
                txtMADepairportdrop.Text = dt.Rows(0)("airportbordername").ToString
                ddlMADepFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString

                chkarrival.Enabled = False
                chktransit.Enabled = False

            End If
            If dt.Rows(0)("airportmatype").ToString.ToUpper = "TRANSIT" Then

                txtTransitarrdate.Text = dt.Rows(0)("airportmadate").ToString
                txtMATrandepdate.Text = dt.Rows(0)("airportmadate").ToString
                chktransit.Checked = True
                chkarrival.Checked = False
                chkDeparture.Checked = False
                ddltranarrflightclass.SelectedValue = dt.Rows(0)("transitflightclass").ToString
                ddlMAtrandepflightlass.SelectedValue = dt.Rows(0)("transitflightclass").ToString

                txtMAtranArrFlight.Text = dt.Rows(0)("transitflightcode").ToString
                txtMATranArrFlightCode.Text = dt.Rows(0)("transitflightcode").ToString
                txtMATranArrTime.Text = dt.Rows(0)("transitflighttime").ToString
                txtMAtranArrivalpickup.Text = dt.Rows(0)("transitairportbordername").ToString
                txtMATransitArrivalpickupcode.Text = dt.Rows(0)("transitairportbordercode").ToString

                txtMAtranDepartureFlight.Text = dt.Rows(0)("transitflightcode").ToString
                txtMATranDepartureFlightCode.Text = dt.Rows(0)("transitflightcode").ToString
                txtMATranDepartureTime.Text = dt.Rows(0)("transitflighttime").ToString
                txtMAtranDeppickup.Text = dt.Rows(0)("transitairportbordername").ToString
                txtMATransitDeparturepickupcode.Text = dt.Rows(0)("transitairportbordercode").ToString

                chkarrival.Enabled = False
                chkDeparture.Enabled = False

            End If



            Airportsearch()
            If Session("sLoginType") = "RO" Then

                txtMASourcecountry.Enabled = False
                txtMACustomer.Enabled = False

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportmeetSearch.aspx :: AmendHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub NewHeaderFill()
        Dim strrequestid As String = ""
        Dim strQuery As String = ""
        Dim dt As DataTable
        Dim dtpax As DataTable
        Try
            If Not Session("sobjBLLMASearch") Is Nothing And Session("sobjBLLMASearchActive") Is Nothing Then
                objBLLMASearch = CType(Session("sobjBLLMASearch"), BLLMASearch)
                strrequestid = GetExistingRequestId()

                txtMAArrivaldate.Text = objBLLMASearch.MAArrTransferDate
                txtArrivalflight.Text = objBLLMASearch.MAArrFlightNo
                txtArrivalflightCode.Text = objBLLMASearch.MAArrFlightNo
                txtArrivalTime.Text = objBLLMASearch.MAArrFlightTime
                txtMAArrivalpickupcode.Text = objBLLMASearch.MAArrPickupCode
                txtMAArrivalpickup.Text = objBLLMASearch.MAArrPickupName
                '  txtMAArrDropoffcode.Text = objBLLMASearch.ArrDropCode
                ' txtMAArrDropoff.Text = objBLLMASearch.ArrDropName
                ddlMAArrFlightClass.SelectedValue = objBLLMASearch.MAArrFlightClass
                chkarrival.Checked = IIf(objBLLMASearch.MAArrivalType = "ARRIVAL", True, False)

                txtMADeparturedate.Text = objBLLMASearch.MADepTransferDate
                txtDepartureFlight.Text = objBLLMASearch.MADepFlightNo
                txtDepartureFlightCode.Text = objBLLMASearch.MADepFlightNo
                txtDepartureTime.Text = objBLLMASearch.MADepFlightTime
                '    txtMADeppickupcode.Text = objBLLMASearch.DepPickupCode
                '   txtMADeppickup.Text = objBLLMASearch.DepPickupName
                txtMADepairportdropcode.Text = objBLLMASearch.MADepDropCode
                txtMADepairportdrop.Text = objBLLMASearch.MADepDropName
                ddlMADepFlightClass.SelectedValue = objBLLMASearch.MADepFlightClass
                chkDeparture.Checked = IIf(objBLLMASearch.MADepartueType = "DEPARTURE", True, False)

                txtTransitarrdate.Text = objBLLMASearch.MATranArrDate
                txtMATrandepdate.Text = objBLLMASearch.MATranDepDate

                chktransit.Checked = IIf(objBLLMASearch.MATransitType = "TRANSIT", True, False)
                ddltranarrflightclass.SelectedValue = objBLLMASearch.MATranArrFlightClass
                ddlMAtrandepflightlass.SelectedValue = objBLLMASearch.MATranDepFlightClass

                txtMAtranArrFlight.Text = objBLLMASearch.MATranArrFlightNo
                txtMATranArrFlightCode.Text = objBLLMASearch.MATranArrFlightNo
                txtMATranArrTime.Text = objBLLMASearch.MATranArrFlightTime
                txtMAtranArrivalpickup.Text = objBLLMASearch.MATranArrPickupName
                txtMATransitArrivalpickupcode.Text = objBLLMASearch.MATranArrPickupCode

                txtMAtranDepartureFlight.Text = objBLLMASearch.MATranDepFlightNo
                txtMATranDepartureFlightCode.Text = objBLLMASearch.MATranDepFlightNo
                txtMATranDepartureTime.Text = objBLLMASearch.MATranDepFlightTime
                txtMAtranDeppickup.Text = objBLLMASearch.MATranDepDropName
                txtMATransitDeparturepickupcode.Text = objBLLMASearch.MATranDepDropCode


                ddlMAAdult.SelectedValue = objBLLMASearch.Adult
                ddlMAChild.SelectedValue = objBLLMASearch.Children
                txtMAChild1.Text = objBLLMASearch.Child1
                txtMAChild2.Text = objBLLMASearch.Child2
                txtMAChild3.Text = objBLLMASearch.Child3
                txtMAChild4.Text = objBLLMASearch.Child4
                txtMAChild5.Text = objBLLMASearch.Child5
                txtMAChild6.Text = objBLLMASearch.Child6
                txtMAChild7.Text = objBLLMASearch.Child7
                txtMAChild8.Text = objBLLMASearch.Child8

                txtMACustomercode.Text = objBLLMASearch.CustomerCode
                txtMACustomer.Text = objBLLMASearch.Customer
                txtMASourcecountrycode.Text = objBLLMASearch.SourceCountryCode
                txtMASourcecountry.Text = objBLLMASearch.SourceCountry

                objBLLMASearch.AmendRequestid = strrequestid
                objBLLMASearch.AmendLineno = hdlineno.Value

                chkMAoverride.Checked = IIf(objBLLMASearch.OverridePrice = "1", True, False)
            ElseIf Not Session("sobjBLLMASearch") Is Nothing Then

                objBLLMASearch = CType(Session("sobjBLLMASearch"), BLLMASearch)
                strrequestid = GetExistingRequestId()

                txtMAArrivaldate.Text = objBLLMASearch.MAArrTransferDate
                txtArrivalflight.Text = objBLLMASearch.MAArrFlightNo
                txtArrivalflightCode.Text = objBLLMASearch.MAArrFlightNo
                txtArrivalTime.Text = objBLLMASearch.MAArrFlightTime
                txtMAArrivalpickupcode.Text = objBLLMASearch.MAArrPickupCode
                txtMAArrivalpickup.Text = objBLLMASearch.MAArrPickupName
                '  txtMAArrDropoffcode.Text = objBLLMASearch.ArrDropCode
                ' txtMAArrDropoff.Text = objBLLMASearch.ArrDropName
                ddlMAArrFlightClass.SelectedValue = objBLLMASearch.MAArrFlightClass
                chkarrival.Checked = IIf(objBLLMASearch.MAArrivalType = "ARRIVAL", True, False)

                txtMADeparturedate.Text = objBLLMASearch.MADepTransferDate
                txtDepartureFlight.Text = objBLLMASearch.MADepFlightNo
                txtDepartureFlightCode.Text = objBLLMASearch.MADepFlightNo
                txtDepartureTime.Text = objBLLMASearch.MADepFlightTime
                '    txtMADeppickupcode.Text = objBLLMASearch.DepPickupCode
                '   txtMADeppickup.Text = objBLLMASearch.DepPickupName
                txtMADepairportdropcode.Text = objBLLMASearch.MADepDropCode
                txtMADepairportdrop.Text = objBLLMASearch.MADepDropName
                ddlMADepFlightClass.SelectedValue = objBLLMASearch.MADepFlightClass
                chkDeparture.Checked = IIf(objBLLMASearch.MADepartueType = "DEPARTURE", True, False)

                txtTransitarrdate.Text = objBLLMASearch.MATranArrDate
                txtMATrandepdate.Text = objBLLMASearch.MATranDepDate

                chktransit.Checked = IIf(objBLLMASearch.MATransitType = "TRANSIT", True, False)
                ddltranarrflightclass.SelectedValue = objBLLMASearch.MATranArrFlightClass
                ddlMAtrandepflightlass.SelectedValue = objBLLMASearch.MATranDepFlightClass

                txtMAtranArrFlight.Text = objBLLMASearch.MATranArrFlightNo
                txtMATranArrFlightCode.Text = objBLLMASearch.MATranArrFlightNo
                txtMATranArrTime.Text = objBLLMASearch.MATranArrFlightTime
                txtMAtranArrivalpickup.Text = objBLLMASearch.MATranArrPickupName
                txtMATransitArrivalpickupcode.Text = objBLLMASearch.MATranArrPickupCode

                txtMAtranDepartureFlight.Text = objBLLMASearch.MATranDepFlightNo
                txtMATranDepartureFlightCode.Text = objBLLMASearch.MATranDepFlightNo
                txtMATranDepartureTime.Text = objBLLMASearch.MATranDepFlightTime
                txtMAtranDeppickup.Text = objBLLMASearch.MATranDepDropName
                txtMATransitDeparturepickupcode.Text = objBLLMASearch.MATranDepDropCode

                '''' Added shahul 07/04/18
                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & strrequestid & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 Then
                    ddlMAAdult.SelectedValue = Val(dtpax.Rows(0)("adults").ToString)

                    If Val(dtpax.Rows(0)("child").ToString) <> 0 Then
                        ddlMAChild.SelectedValue = dtpax.Rows(0)("child").ToString

                        Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                        If Left(childages, 1) = ";" Then
                            childages = Right(childages, (childages.Length - 1))
                        End If
                        Dim strChildAges As String() = childages.ToString.Split(";")
                        '''''''

                        If strChildAges.Length <> 0 Then
                            txtMAChild1.Text = strChildAges(0)
                        End If

                        If strChildAges.Length > 1 Then
                            txtMAChild2.Text = strChildAges(1)
                        End If
                        If strChildAges.Length > 2 Then
                            txtMAChild3.Text = strChildAges(2)
                        End If
                        If strChildAges.Length > 3 Then
                            txtMAChild4.Text = strChildAges(3)
                        End If
                        If strChildAges.Length > 4 Then
                            txtMAChild5.Text = strChildAges(4)
                        End If
                        If strChildAges.Length > 5 Then
                            txtMAChild6.Text = strChildAges(5)
                        End If
                        If strChildAges.Length > 6 Then
                            txtMAChild7.Text = strChildAges(6)
                        End If
                        If strChildAges.Length > 7 Then
                            txtMAChild8.Text = strChildAges(7)
                        End If
                    End If
                Else
                    ddlMAAdult.SelectedValue = objBLLMASearch.Adult
                    ddlMAChild.SelectedValue = objBLLMASearch.Children
                    txtMAChild1.Text = objBLLMASearch.Child1
                    txtMAChild2.Text = objBLLMASearch.Child2
                    txtMAChild3.Text = objBLLMASearch.Child3
                    txtMAChild4.Text = objBLLMASearch.Child4
                    txtMAChild5.Text = objBLLMASearch.Child5
                    txtMAChild6.Text = objBLLMASearch.Child6
                    txtMAChild7.Text = objBLLMASearch.Child7
                    txtMAChild8.Text = objBLLMASearch.Child8
                End If

               

                txtMACustomercode.Text = objBLLMASearch.CustomerCode
                txtMACustomer.Text = objBLLMASearch.Customer
                txtMASourcecountrycode.Text = objBLLMASearch.SourceCountryCode
                txtMASourcecountry.Text = objBLLMASearch.SourceCountry

                objBLLMASearch.AmendRequestid = strrequestid
                objBLLMASearch.AmendLineno = hdlineno.Value

                chkMAoverride.Checked = IIf(objBLLMASearch.OverridePrice = "1", True, False)
            Else



                Dim objBLLCommonFuntions = New BLLCommonFuntions

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
                If dt.Rows.Count > 0 Then

                    txtMACustomercode.Text = dt.Rows(0)("agentcode").ToString
                    txtMACustomer.Text = dt.Rows(0)("agentname").ToString
                    txtMASourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
                    txtMASourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                    '  chkMAoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                    strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sEditRequestId") & "')"
                    dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                    If dtpax.Rows.Count > 0 Then

                        ddlMAAdult.SelectedValue = Val(dtpax.Rows(0)("adults").ToString)

                        If Val(dtpax.Rows(0)("child").ToString) <> 0 Then
                            ddlMAChild.SelectedValue = dtpax.Rows(0)("child").ToString

                            Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                            If Left(childages, 1) = ";" Then
                                childages = Right(childages, (childages.Length - 1))
                            End If
                            Dim strChildAges As String() = childages.ToString.Split(";")
                            '''''''

                            If strChildAges.Length <> 0 Then
                                txtMAChild1.Text = strChildAges(0)
                            End If

                            If strChildAges.Length > 1 Then
                                txtMAChild2.Text = strChildAges(1)
                            End If
                            If strChildAges.Length > 2 Then
                                txtMAChild3.Text = strChildAges(2)
                            End If
                            If strChildAges.Length > 3 Then
                                txtMAChild4.Text = strChildAges(3)
                            End If
                            If strChildAges.Length > 4 Then
                                txtMAChild5.Text = strChildAges(4)
                            End If
                            If strChildAges.Length > 5 Then
                                txtMAChild6.Text = strChildAges(5)
                            End If
                            If strChildAges.Length > 6 Then
                                txtMAChild7.Text = strChildAges(6)
                            End If
                            If strChildAges.Length > 7 Then
                                txtMAChild8.Text = strChildAges(7)
                            End If
                        End If
                    End If


                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportmeetSearch.aspx :: NewHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub EditHeaderFill()
        Dim strrequestid As String = ""
        Dim strQuery As String = ""
        Dim trftype As String = ""
        Dim dt As New DataTable
        Try
            objBLLMASearch = CType(Session("sobjBLLMASearch"), BLLMASearch)
            strrequestid = GetExistingRequestId()

            objBLLMASearch.AmendRequestid = strrequestid
            objBLLMASearch.AmendLineno = hdlineno.Value

            chkMAoverride.Checked = IIf(objBLLMASearch.OverridePrice = "1", True, False)

            strQuery = "select airportmatype from booking_airportmatemp(nolock) where requestid='" & strrequestid & "' and alineno=" & hdlineno.Value
            trftype = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            dt = objBLLMASearch.GetEditBookingDetails(strrequestid, hdlineno.Value)
            If dt.Rows.Count > 0 Then

                If dt.Rows(0)("airportmatype").ToString.ToUpper = "ARRIVAL" Then

                    txtMAArrivaldate.Text = dt.Rows(0)("airportmadate").ToString
                    txtArrivalflight.Text = dt.Rows(0)("flightcode").ToString
                    txtArrivalflightCode.Text = dt.Rows(0)("flight_tranid").ToString
                    txtArrivalTime.Text = dt.Rows(0)("flighttime").ToString
                    txtMAArrivalpickupcode.Text = dt.Rows(0)("airportbordercode").ToString
                    txtMAArrivalpickup.Text = dt.Rows(0)("airportbordername").ToString
                    ddlMAArrFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString
                    chkarrival.Checked = True ' IIf(dt.Rows(0)("airportmatype").ToString.ToUpper = "ARRIVAL", True, False)

                    chkDeparture.Checked = False
                    chktransit.Checked = False
                    chkDeparture.Enabled = False
                    chktransit.Enabled = False

                ElseIf dt.Rows(0)("airportmatype").ToString.ToUpper = "DEPARTURE" Then

                    txtMADeparturedate.Text = dt.Rows(0)("airportmadate").ToString
                    txtDepartureFlight.Text = dt.Rows(0)("flightcode").ToString
                    txtDepartureFlightCode.Text = dt.Rows(0)("flight_tranid").ToString
                    txtDepartureTime.Text = dt.Rows(0)("flighttime").ToString
                    txtMADepairportdropcode.Text = dt.Rows(0)("airportbordercode").ToString
                    txtMADepairportdrop.Text = dt.Rows(0)("airportbordername").ToString
                    ddlMADepFlightClass.SelectedValue = dt.Rows(0)("flightclass").ToString
                    chkDeparture.Checked = True ' IIf(dt.Rows(0)("airportmatype").ToString.ToUpper = "DEPARTURE", True, False)

                    chkarrival.Checked = False
                    chktransit.Checked = False
                    chkarrival.Enabled = False
                    chktransit.Enabled = False

                Else

                    txtTransitarrdate.Text = objBLLMASearch.MATranArrDate
                    txtMATrandepdate.Text = objBLLMASearch.MATranDepDate
                    chktransit.Checked = IIf(objBLLMASearch.MATransitType = "TRANSIT", True, False)
                    ddltranarrflightclass.SelectedValue = objBLLMASearch.MATranArrFlightClass
                    ddlMAtrandepflightlass.SelectedValue = objBLLMASearch.MATranDepFlightClass
                    txtMAtranArrFlight.Text = objBLLMASearch.MATranArrFlightNo
                    txtMATranArrFlightCode.Text = objBLLMASearch.MATranArrFlightNo
                    txtMATranArrTime.Text = objBLLMASearch.MATranArrFlightTime
                    txtMAtranArrivalpickup.Text = objBLLMASearch.MATranArrPickupName
                    txtMATransitArrivalpickupcode.Text = objBLLMASearch.MATranArrPickupCode
                    txtMAtranDepartureFlight.Text = objBLLMASearch.MATranDepFlightNo
                    txtMATranDepartureFlightCode.Text = objBLLMASearch.MATranDepFlightNo
                    txtMATranDepartureTime.Text = objBLLMASearch.MATranDepFlightTime
                    txtMAtranDeppickup.Text = objBLLMASearch.MATranDepDropName
                    txtMATransitDeparturepickupcode.Text = objBLLMASearch.MATranDepDropCode

                    chkarrival.Checked = False
                    chkDeparture.Checked = False
                    chkarrival.Enabled = False
                    chkDeparture.Enabled = False

                End If


            End If

           
            chkMAoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

           ddlMAAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
            ddlMAChild.SelectedValue = dt.Rows(0)("child").ToString
            chkMAoverride.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

            If Val(dt.Rows(0)("child").ToString) <> 0 Then




                Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                If Left(childages, 1) = ";" Then
                    childages = Right(childages, (childages.Length - 1))
                End If
                Dim strChildAges As String() = childages.ToString.Split(";")
                '''''''

                If strChildAges.Length <> 0 Then
                    txtMAChild1.Text = strChildAges(0)
                End If

                If strChildAges.Length > 1 Then
                    txtMAChild2.Text = strChildAges(1)
                End If
                If strChildAges.Length > 2 Then
                    txtMAChild3.Text = strChildAges(2)
                End If
                If strChildAges.Length > 3 Then
                    txtMAChild4.Text = strChildAges(3)
                End If
                If strChildAges.Length > 4 Then
                    txtMAChild5.Text = strChildAges(4)
                End If
                If strChildAges.Length > 5 Then
                    txtMAChild6.Text = strChildAges(5)
                End If
                If strChildAges.Length > 6 Then
                    txtMAChild7.Text = strChildAges(6)
                End If
                If strChildAges.Length > 7 Then
                    txtMAChild8.Text = strChildAges(7)
                End If
            End If

            txtMAChild1.Text = objBLLMASearch.Child1
            txtMAChild2.Text = objBLLMASearch.Child2
            txtMAChild3.Text = objBLLMASearch.Child3
            txtMAChild4.Text = objBLLMASearch.Child4
            txtMAChild5.Text = objBLLMASearch.Child5
            txtMAChild6.Text = objBLLMASearch.Child6
            txtMAChild7.Text = objBLLMASearch.Child7
            txtMAChild8.Text = objBLLMASearch.Child8


            txtMACustomercode.Text = dt.Rows(0)("agentcode").ToString
            txtMACustomer.Text = dt.Rows(0)("agentname").ToString
            txtMASourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
            txtMASourcecountry.Text = dt.Rows(0)("sourcectryname").ToString

            Airportsearch()
            If Session("sLoginType") = "RO" Then

                txtMASourcecountry.Enabled = False
                txtMACustomer.Enabled = False

            End If

            'objBLLMASearch.AmendRequestid = strrequestid
            'objBLLMASearch.AmendLineno = hdlineno.Value


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportmeetSearch.aspx :: EditHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub LoadHome()
        Try
            LoadFooter()
            objBLLMASearch = New BLLMASearch
            LoadLogo()
            LoadMenus()
            objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)

            LoadRoomAdultChild()
            LoadFields()
            BindMAflightclass()
            BindTransferdetails()

            hdPriceMinRange.Value = "0"
            hdPriceMaxRange.Value = "1"
            hdlineno.value = Request.QueryString("ALineNo")

            Dim dt As DataTable

            If Not Session("sEditRequestId") Is Nothing Then
                If Val(hdlineno.Value) = 0 Then
                    NewHeaderFill()
                Else
                    Amendheaderfill()
                End If

            Else
                If Not Session("sobjBLLMASearch") Is Nothing Then

                    If Val(hdlineno.Value) = 0 Then
                        NewHeaderFill()

                    Else
                        EditHeaderFill()
                    End If

                    BindSearchResults()

                    If Not Page.Request.UrlReferrer Is Nothing Then
                        Dim previousPage As String = Page.Request.UrlReferrer.ToString
                        If previousPage.Contains("MoreServices.aspx") Then
                            LoadFlightDetails()
                            '  LoadFiledsBasedOnTrfdetails()
                            BindMAChildAge()
                        End If
                    End If
                Else
                    If Not Page.Request.UrlReferrer Is Nothing Then
                        Dim previousPage As String = Page.Request.UrlReferrer.ToString
                        If previousPage.Contains("MoreServices.aspx") Then
                            LoadFlightDetails()
                            '  LoadFiledsBasedOnTrfdetails()
                            BindMAChildAge()
                        End If
                    Else
                        BindMAChildAge()
                    End If
                End If
            End If

            If hdWhiteLabel.Value = "1" Then
                dvMagnifyingMemories.Visible = False
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: LoadHome :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub LoadFlightDetails()
        Dim strRequestId As String = ""
        strRequestId = GetExistingRequestId()
        Dim dt As DataTable
        Dim objBLLTransferSearch As New BLLTransferSearch
        dt = objBLLTransferSearch.LaodTrfFlightDetails(strRequestId)
        If dt.Rows.Count > 0 Then
            If dt.Rows.Count = 1 Then
                txtArrivalflight.Text = dt.Rows(0)("arrflightcode").ToString()
                txtArrivalflightCode.Text = dt.Rows(0)("arrflight_tranid").ToString()
                txtArrivalTime.Text = dt.Rows(0)("arrflighttime").ToString()


                txtDepartureFlight.Text = dt.Rows(0)("depflightcode").ToString()
                txtDepartureFlightCode.Text = dt.Rows(0)("depflight_tranid").ToString()
                txtDepartureTime.Text = dt.Rows(0)("depflighttime").ToString()


                txtMAArrivalpickup.Text = dt.Rows(0)("arrairportborderName").ToString()
                txtMAArrivalpickupcode.Text = dt.Rows(0)("arrairportbordercode").ToString()


                txtMADepairportdrop.Text = dt.Rows(0)("depairportborderName").ToString()
                txtMADepairportdropcode.Text = dt.Rows(0)("depairportbordercode").ToString()

            End If
        End If

    End Sub

    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo '*** Danny 30/06/2018

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
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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


                divMAOverride.Style.Add("display", "none")
                dvMACustomer.Style.Add("display", "none")
                ' dvMACustomer.Visible = False

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


                        txtMASourcecountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtMASourcecountrycode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtMASourcecountry.ReadOnly = True
                        AutoCompleteExtender_txtMASourcecountry.Enabled = False
                    Else

                        txtMASourcecountry.ReadOnly = False
                        AutoCompleteExtender_txtMASourcecountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else

                divMAOverride.Style.Add("display", "block")
                dvMACustomer.Style.Add("display", "block")


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
            strSqlQry = "select s.destname,c.catcode,c.catname,s.destcode + '|' +case when desttype='Area' or desttype='Sector' then 'Sector' else desttype end destcode  from partymast p,view_destination_search s,catmast c where p.sectorcode=s.destcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
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

    Private Sub LoadFiledsBasedOnTrfdetails()
        Dim strQuery As String = ""
        Dim dsTrfdetails As New DataSet
        If Not Session("sRequestId") Is Nothing Then
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                txtMACustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtMACustomer.Text = dt.Rows(0)("agentname").ToString
                txtMASourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtMASourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString
            End If

            Dim objBLLHotelSearch = New BLLHotelSearch
            dsTrfdetails = objBLLHotelSearch.FillTransferDetails(Session("sRequestId"))

            If dsTrfdetails.Tables(0).Rows.Count > 0 Then
                Dim childagestring As String() = dsTrfdetails.Tables(0).Rows(0)("childages").ToString.Split(";")

                ddlMAAdult.SelectedValue = IIf(Val(dsTrfdetails.Tables(0).Rows(0)("adults").ToString) = "0.0", "", dsTrfdetails.Tables(0).Rows(0)("adults"))
                ddlMAChild.SelectedValue = IIf(Val(dsTrfdetails.Tables(0).Rows(0)("child").ToString) = "0.0", "", dsTrfdetails.Tables(0).Rows(0)("child")) 'dsTrfdetails.Tables(0).Rows(0)("child")


            End If

            If dsTrfdetails.Tables.Count > 0 Then

                BindMAArrivaldetails(dsTrfdetails.Tables(1))
                BindMADeparturedetails(dsTrfdetails.Tables(2))
            End If
        End If
    End Sub
    Private Sub BindMAArrivaldetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkarrival.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "ARRIVAL", True, False)
            txtMAArrivaldate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlMAArrFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtMAArrDropoffcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = "select partyname from partymast where active=1 and partycode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtMAArrDropoff.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMAArrivalpickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = " select   a.airportbordername from flightmast f,airportbordersmaster a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtMAArrivalpickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtArrivalflightCode.Text = dataTable.Rows(0)("flightcode")
            txtArrivalflight.Text = dataTable.Rows(0)("flightcode")
            txtArrivalTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub

    Private Sub BindMADeparturedetails(ByVal dataTable As DataTable)
        Dim strQuery As String = ""
        If dataTable.Rows.Count > 0 Then
            chkDeparture.Checked = IIf(dataTable.Rows(0)("transfertype").ToString.ToUpper = "DEPARTURE", True, False)
            txtMADeparturedate.Text = Format(CType(dataTable.Rows(0)("transferdate"), Date), "dd/MM/yyyy")
            ddlMADepFlightClass.SelectedValue = dataTable.Rows(0)("flightclscode")
            txtMADepairportdropcode.Text = dataTable.Rows(0)("dropoffcode")
            strQuery = " select   a.airportbordername from flightmast f,airportbordersmaster a  where f.airportbordercode=a.airportbordercode and a.active=1 and f.airportbordercode='" & dataTable.Rows(0)("dropoffcode") & "'"
            txtMADepairportdrop.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtMADeppickupcode.Text = dataTable.Rows(0)("pickupcode")

            strQuery = "select partyname from partymast where active=1 and partycode='" & dataTable.Rows(0)("pickupcode") & "'"
            txtMADeppickup.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            txtDepartureFlightCode.Text = dataTable.Rows(0)("flightcode")
            txtDepartureFlight.Text = dataTable.Rows(0)("flightcode")
            txtDepartureTime.Text = dataTable.Rows(0)("flighttime")


        End If
    End Sub

    Private Sub BindSearchResults()
        Try


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


            objBLLMASearch = New BLLMASearch
            If Session("sobjBLLMASearch") Is Nothing Then
                ' Response.Redirect("Home.aspx?Tab=2")
                MessageBox.ShowMessage(Page, MessageType.Warning, "Select any criteria for tour search.")
                Exit Sub
            End If
            objBLLMASearch = CType(Session("sobjBLLMASearch"), BLLMASearch)

            Dim dsSearchResults As New DataSet
            objBLLMASearch.FilterRoomClass = ""
            dsSearchResults = objBLLMASearch.GetSearchDetails()
            If Not dsSearchResults Is Nothing Then
                If dsSearchResults.Tables(4).Rows.Count = 0 Then
                    dvhotnoshow.Style.Add("display", "block")
                Else
                    dvhotnoshow.Style.Add("display", "none")
                End If


                Session("sDSMASearchResults") = dsSearchResults
                If dsSearchResults.Tables.Count > 0 Then

                    BindAirportMeetTypes(dsSearchResults.Tables(4))
                    BindPricefilter(dsSearchResults.Tables(3))


                    Session("sDSMASearchResults") = dsSearchResults
                    Session("sMAMailBoxPageIndex") = "1"
                    Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
                    Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))
                    Dim dvTransitDetails As DataView = New DataView(dsSearchResults.Tables(2))
                    If ddlSorting.Text = "Name" Then
                        dvMaiDetails.Sort = "airportmatypename ASC"
                        dvDepMaiDetails.Sort = "airportmatypename ASC"
                        dvTransitDetails.Sort = "airportmatypename ASC"
                    ElseIf ddlSorting.Text = "Price" Then
                        dvMaiDetails.Sort = "totalsalevalue ASC"
                        dvDepMaiDetails.Sort = "totalsalevalue ASC"
                        dvTransitDetails.Sort = "totalsalevalue ASC"
                    End If
                    Dim recordCount As Integer = dvMaiDetails.Count + dvDepMaiDetails.Count + dvTransitDetails.Count

                    ViewState("Arrviewmorehide") = dvMaiDetails.Count
                    BindArrivalDetails(dvMaiDetails)
                    ViewState("Depviewmorehide") = dvDepMaiDetails.Count
                    BindDepartureDetails(dvDepMaiDetails)
                    ViewState("Transitviewmorehide") = dvDepMaiDetails.Count
                    BindTransitDetails(dvTransitDetails)
                    Me.PopulatePager(recordCount)

                    lblHotelCount.Text = recordCount & " Records Found"
                Else
                    hdPriceMinRange.Value = "0"
                    hdPriceMaxRange.Value = "1"
                End If
            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: BindSearchResults :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Protected Sub dlMAArrivalSearchResults_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMAArrivalSearchResults.ItemCreated
        If dlMAArrivalSearchResults.Items.Count = 1 And e.Item.ItemType = 1 And ViewState("Arrviewmorehide") = 1 Then
            Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbArrShowMore"), LinkButton)

            myLinkButton.Visible = False
        End If
    End Sub

    Protected Sub dlMAArrivalSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMAArrivalSearchResults.ItemDataBound
        Try

            If e.Item.ItemType = 1 Then
                Dim myarrButton As LinkButton = CType(e.Item.FindControl("lbArrShowMore"), LinkButton)
                'Dim lblremarks As Label = CType(e.Item.FindControl("lblremarks"), Label)
                'lblremarks.Text = lblremarks.Text.Replace(Environment.NewLine, "<br/>")
                If ViewState("ArrShow") = "1" Then
                    myarrButton.Text = "Show Less"
                Else
                    myarrButton.Text = "Show More"
                End If

            End If



            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim imgHotelImage As Image = CType(e.Item.FindControl("imgMAImage"), Image)
                Dim lblMAImage As Label = CType(e.Item.FindControl("lblMAImage"), Label)
                imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblMAImage.Text & "&type=3"

                Dim lblminpax As Label = CType(e.Item.FindControl("lblminpax"), Label)
                Dim lblmaxpax As Label = CType(e.Item.FindControl("lblmaxpax"), Label)
                Dim dvArrMin As HtmlGenericControl = CType(e.Item.FindControl("dvArrMin"), HtmlGenericControl)
                Dim dvArrMax As HtmlGenericControl = CType(e.Item.FindControl("dvArrMax"), HtmlGenericControl)

                Dim lblremarks As Label = CType(e.Item.FindControl("lblremarks"), Label)
                lblremarks.Text = lblremarks.Text.Replace(Environment.NewLine, "<br/>")
                Dim chkbooknow As CheckBox = CType(e.Item.FindControl("chkbooknow"), CheckBox)

                If lblminpax.Text.Trim = "0 PAX" Then
                    dvArrMin.Visible = False
                End If
                If lblmaxpax.Text.Trim = "0 PAX" Then
                    dvArrMax.Visible = False
                End If

                chkbooknow.Attributes.Add("onChange", "javascript:calculatevalue('" + CType(chkbooknow.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "' )")
                Dim lbtotalValue As LinkButton = CType(e.Item.FindControl("lbtotalValue"), LinkButton)
                Dim lbwltotalValue As LinkButton = CType(e.Item.FindControl("lbwltotalValue"), LinkButton)
                Dim lblTotalCurrcode As Label = CType(e.Item.FindControl("lblTotalCurrcode"), Label)
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    lbwltotalValue.Visible = True
                    lbtotalValue.Visible = False

                    Dim dTotalValue As Double = IIf(lbwltotalValue.Text = "", 0, lbwltotalValue.Text)
                    lbwltotalValue.Text = Math.Round(dTotalValue)

                    Dim lblwlcurrcode As Label = CType(e.Item.FindControl("lblwlcurrcode"), Label)
                    lblTotalCurrcode.Text = lblwlcurrcode.Text
                Else

                    Dim dTotalValue As Double = IIf(lbtotalValue.Text = "", 0, lbtotalValue.Text)
                    lbtotalValue.Text = Math.Round(dTotalValue)
                    lbwltotalValue.Visible = False
                    lbtotalValue.Visible = True
                End If
                hdSliderCurrency.Value = " " & lblTotalCurrcode.Text
                If hdBookingEngineRateType.Value = "1" Then
                    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                        Dim dvunit As HtmlGenericControl = CType(e.Item.FindControl("dvunit"), HtmlGenericControl)
                        Dim divtot As HtmlGenericControl = CType(e.Item.FindControl("divtot"), HtmlGenericControl)
                        Dim lnkcumunits As LinkButton = CType(e.Item.FindControl("lnkcumunits"), LinkButton)

                        Dim lbltotal As Label = CType(e.Item.FindControl("lbltotal"), Label)
                        Dim lblratebasis As Label = CType(e.Item.FindControl("lblratebasis"), Label)
                        Dim lblunit As Label = CType(e.Item.FindControl("lblunit"), Label)

                        lnkcumunits.Text = lblunit.Text + " Units"

                     

                        'divtot.Visible = False



                        If lblratebasis.Text.ToUpper = "UNIT" Then
                            lnkcumunits.Style.Add("display", "block")
                            lbtotalValue.Style.Add("display", "none")
                            lbwltotalValue.Style.Add("display", "none")
                            lbltotal.Style.Add("display", "none")
                        Else
                            lnkcumunits.Style.Add("display", "none")
                            lbtotalValue.Style.Add("display", "none")
                            lbwltotalValue.Style.Add("display", "none")
                            lbltotal.Style.Add("display", "none")
                        End If
                       

                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: dlMAArrivalSearchResults_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Function Validatedetails() As String

        Dim arrivalflag As Boolean = False
        Dim depflag As Boolean = False
        Dim transitFlag As Boolean = False
        Dim ArrMAflag As Boolean = False
        Dim DepMAflag As Boolean = False
        Dim TransitMAflag As Boolean = False
        If chkarrival.Checked = True Then
            arrivalflag = True
        End If
        If chkDeparture.Checked = True Then
            depflag = True
        End If
        If chktransit.Checked = True Then
            transitFlag = True
        End If

        For Each gvRow As DataListItem In dlMAArrivalSearchResults.Items
            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

            If chkbooknow.Checked = True Then
                ArrMAflag = True
                Exit For
            End If

        Next

        For Each gvRow As DataListItem In dlMADepartureSearchResults.Items
            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

            If chkbooknow.Checked = True Then
                DepMAflag = True
                Exit For
            End If

        Next
        For Each gvRow As DataListItem In dlMATransitSearchResults.Items
            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")

            If chkbooknow.Checked = True Then
                TransitMAflag = True
                Exit For
            End If

        Next

        If ArrMAflag = False And DepMAflag = False And TransitMAflag = False Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any airport meet.")
            Return False
            Exit Function
        End If

        'If arrivalflag = True And ArrMAflag = False Then
        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival airport meet Selected. Please Select atleast any one.")
        '    Return False
        '    Exit Function
        'End If

        'If depflag = True And DepMAflag = False Then
        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Departure airport meet Selected. Please Select Departure Vehicles")
        '    Return False
        '    Exit Function
        'End If


        Return True
    End Function

    '*** Danny 04/06/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    Protected Sub btnBookNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbooknow.Click
        Try

            Dim strBuffer As New Text.StringBuilder
            Dim strFliBuffer As New Text.StringBuilder
            Dim requestid As String = ""
            Dim sourcectrycode As String = ""
            Dim agentcode As String = ""
            Dim agentref As String = ""
            Dim columbusref As String = ""
            Dim remarks As String = ""



            If Validatedetails() Then

                requestid = GetNewOrExistingRequestId()
                If Session("State") = "New" Then

                    If Not Session("sAgentCode") Is Nothing Then
                        objBLLMASearch = New BLLMASearch
                        objBLLMASearch = CType(Session("sobjBLLMASearch"), BLLMASearch)
                        Dim objBLLCommonFuntions = New BLLCommonFuntions
                        Dim dt As DataTable
                        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
                        If dt.Rows.Count > 0 Then
                            objBLLMASearch.AgentCode = dt.Rows(0)("agentcode").ToString
                            objBLLMASearch.OBDiv_Code = dt.Rows(0)("div_code").ToString
                            objBLLMASearch.OBRequestId = requestid
                            objBLLMASearch.OBSourcectryCode = dt.Rows(0)("sourcectrycode").ToString
                            objBLLMASearch.OBAgentCode = IIf(Session("sLoginType") = "RO", objBLLMASearch.CustomerCode, dt.Rows(0)("agentcode").ToString)

                            objBLLMASearch.OBReqoverRide = IIf(chkMAoverride.Checked = True, "1", "0")
                            objBLLMASearch.OBAgentref = dt.Rows(0)("agentref").ToString
                            objBLLMASearch.OBColumbusRef = dt.Rows(0)("ColumbusRef").ToString
                            objBLLMASearch.OBRemarks = dt.Rows(0)("remarks").ToString
                            objBLLMASearch.UserLogged = Session("GlobalUserName")
                        Else
                            objBLLMASearch.AgentCode = Session("sAgentCode")
                            objBLLMASearch.OBDiv_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast where agentcode='" & Session("sAgentCode") & "'")
                            objBLLMASearch.OBRequestId = requestid
                            objBLLMASearch.OBSourcectryCode = IIf(sourcectrycode = "", objBLLMASearch.SourceCountryCode, sourcectrycode)


                            objBLLMASearch.OBAgentCode = IIf(Session("sLoginType") = "RO", objBLLMASearch.CustomerCode, Session("sAgentCode"))

                            objBLLMASearch.OBReqoverRide = IIf(chkMAoverride.Checked = True, "1", "0")
                            objBLLMASearch.OBAgentref = agentref
                            objBLLMASearch.OBColumbusRef = columbusref
                            objBLLMASearch.OBRemarks = remarks
                            objBLLMASearch.UserLogged = Session("GlobalUserName")
                        End If




                        Dim strALineNo As String = ""

                        Dim btnbooknow As Button = CType(sender, Button)
                        Dim rowid As Integer = 0
                        Dim rlineonostring As String = ""
                        Dim dss As DataSet = CType(Session("sDSMASearchResults"), DataSet)

                        For Each gvRow As DataListItem In dlMAArrivalSearchResults.Items
                            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                            Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")

                            If chkbooknow.Checked = True Then

                                strBuffer.Append("<DocumentElement>")
                                Dim dr As DataRow = dss.Tables(0).Select("airportmatypecode='" & hdAirportTypeCode.Value & "'").First

                                If Not dr Is Nothing Then



                                    If Val(hdlineno.Value) = 0 Then
                                        If strALineNo = "" Then
                                            strALineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLMASearch.OBRequestId, "AIRPORT_MEET")
                                        Else
                                            strALineNo = CType(strALineNo, Integer) + 1
                                        End If

                                        'strALineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLMASearch.OBRequestId, "AIRPORT_MEET")
                                    Else
                                        strALineNo = hdlineno.Value
                                    End If
                                    If rlineonostring <> "" Then
                                        rlineonostring = rlineonostring + ";" + CType(strALineNo, String)
                                    Else
                                        rlineonostring = strALineNo
                                    End If


                                    'If Val(hdlineno.Value) = 0 Then
                                    '    If Session("sMALineNo") Is Nothing Then
                                    '        Session.Add("sMALineNo", "1")
                                    '    Else
                                    '        Session("sMALineNo") = CType(Session("sMALineNo"), Integer) + 1

                                    '    End If
                                    'Else
                                    '    Session("sMALineNo") = hdlineno.Value
                                    'End If
                                    'If rlineonostring <> "" Then
                                    '    rlineonostring = rlineonostring + ";" + CType(Session("sMALineNo"), String)
                                    'Else
                                    '    rlineonostring = Session("sMALineNo")
                                    'End If


                                    Dim lineno As Integer = CType(Session("sMALineNo"), Integer)
                                    Dim str As String = ""
                                    Dim strZero As String = "0"
                                    strBuffer.Append("<Table>")
                                    strBuffer.Append(" <alineno>" & strALineNo & "</alineno>")
                                    strBuffer.Append(" <airportmatype>ARRIVAL</airportmatype>")
                                    strBuffer.Append(" <othtypcode>" & hdAirportTypeCode.Value & "</othtypcode>")
                                    strBuffer.Append(" <airportmadate>" & Format(CType(dr("airportmadate").ToString, Date), "yyyy/MM/dd") & "</airportmadate>")

                                    Dim strQuery As String = ""
                                    Dim flighttranid As String = ""
                                    strQuery = "select flight_tranid from flightmast where active=1 and flightcode='" & CType(txtArrivalflight.Text, String) & "'"
                                    flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                                    strBuffer.Append(" <flightcode>" & txtArrivalflight.Text & "</flightcode>")
                                    strBuffer.Append(" <flight_tranid>" & flighttranid & "</flight_tranid>")
                                    strBuffer.Append(" <flighttime>" & txtArrivalTime.Text & "</flighttime>")
                                    strBuffer.Append(" <flightclscode>" & ddlMAArrFlightClass.SelectedValue & "</flightclscode>")
                                    strBuffer.Append(" <airportbordercode>" & txtMAArrivalpickupcode.Text & "</airportbordercode>")
                                    strBuffer.Append(" <trdepflightcode>" & str & "</trdepflightcode>")
                                    strBuffer.Append(" <trdepflight_tranid>" & str & "</trdepflight_tranid>")
                                    strBuffer.Append(" <trdepflighttime>" & str & "</trdepflighttime>")
                                    strBuffer.Append(" <trdepflightclscode>" & str & "</trdepflightclscode>")
                                    strBuffer.Append(" <trdepairportbordercode>" & str & "</trdepairportbordercode>")
                                    strBuffer.Append(" <adults>" & dr("adults").ToString & "</adults>")
                                    strBuffer.Append(" <child>" & dr("child").ToString & "</child>")
                                    strBuffer.Append(" <childagestring>" & dr("childagestring").ToString & "</childagestring>")
                                    strBuffer.Append(" <maxpax>" & dr("maxpax").ToString & "</maxpax>")
                                    strBuffer.Append(" <childtocharge>" & dr("childtocharge").ToString & "</childtocharge>")
                                    strBuffer.Append(" <units>" & dr("units").ToString & "</units>")
                                    strBuffer.Append(" <addlpax>" & dr("addlpax").ToString & "</addlpax>")
                                    strBuffer.Append(" <complimentarycust>" & dr("complimentarycust").ToString & "</complimentarycust>")
                                    strBuffer.Append(" <adultprice>" & dr("adultprice").ToString & "</adultprice>")
                                    strBuffer.Append(" <childprice>" & dr("childprice").ToString & "</childprice>")
                                    strBuffer.Append(" <unitprice>" & dr("unitprice").ToString & "</unitprice>")
                                    strBuffer.Append(" <addlpaxprice>" & dr("addlpaxprice").ToString & "</addlpaxprice>")
                                    strBuffer.Append(" <adultsalevalue>" & dr("adultsalevalue").ToString & "</adultsalevalue>")
                                    strBuffer.Append(" <childsalevalue>" & dr("childsalevalue").ToString & "</childsalevalue>")
                                    strBuffer.Append(" <unitsalevalue>" & dr("unitsalevalue").ToString & "</unitsalevalue>")
                                    strBuffer.Append(" <addlpaxsalevalue>" & dr("addlpaxsalevalue").ToString & "</addlpaxsalevalue>")
                                    strBuffer.Append(" <totalsalevalue>" & dr("totalsalevalue").ToString & "</totalsalevalue>")

                                    strBuffer.Append(" <wladultprice>" & dr("wladultprice").ToString & "</wladultprice>")
                                    strBuffer.Append(" <wlchildprice>" & dr("wlchildprice").ToString & "</wlchildprice>")
                                    strBuffer.Append(" <wlunitprice>" & dr("wlunitprice").ToString & "</wlunitprice>")
                                    strBuffer.Append(" <wladdlpaxprice>" & dr("wladdlpaxprice").ToString & "</wladdlpaxprice>")
                                    strBuffer.Append(" <wladultsalevalue>" & dr("wladultsalevalue").ToString & "</wladultsalevalue>")

                                    strBuffer.Append(" <wlchildsalevalue>" & dr("wlchildsalevalue").ToString & "</wlchildsalevalue>")
                                    strBuffer.Append(" <wlunitsalevalue>" & dr("wlunitsalevalue").ToString & "</wlunitsalevalue>")
                                    strBuffer.Append(" <wladdlpaxsalevalue>" & dr("wladdlpaxsalevalue").ToString & "</wladdlpaxsalevalue>")
                                    strBuffer.Append(" <wltotalsalevalue>" & dr("wladultsalevalue").ToString & "</wltotalsalevalue>")

                                    strBuffer.Append(" <overrideprice>" & IIf(chkMAoverride.Checked = True, 1, 0) & "</overrideprice>")
                                    strBuffer.Append(" <adultplistcode>" & dr("adultplistcode").ToString & "</adultplistcode>")
                                    strBuffer.Append(" <childplistcode>" & dr("childplistcode").ToString & "</childplistcode>")
                                    strBuffer.Append(" <unitplistcode>" & dr("unitplistcode").ToString & "</unitplistcode>")
                                    strBuffer.Append(" <addlpaxplistcode>" & dr("addlpaxplistcode").ToString & "</addlpaxplistcode>")


                                    strBuffer.Append(" <preferredsupplier>" & dr("preferredsupplier").ToString & "</preferredsupplier>")
                                    strBuffer.Append(" <adultcprice>" & dr("adultcprice").ToString & "</adultcprice>")
                                    strBuffer.Append(" <childcprice>" & dr("childcprice").ToString & "</childcprice>")
                                    strBuffer.Append(" <unitcprice>" & dr("unitcprice").ToString & "</unitcprice>")
                                    strBuffer.Append(" <addlpaxcprice>" & dr("addlpaxcprice").ToString & "</addlpaxcprice>")
                                    strBuffer.Append(" <adultcostvalue>" & dr("adultcostvalue").ToString & "</adultcostvalue>")
                                    strBuffer.Append(" <childcostvalue>" & dr("childcostvalue").ToString & "</childcostvalue>")
                                    strBuffer.Append(" <unitcostvalue>" & dr("unitcostvalue").ToString & "</unitcostvalue>")

                                    strBuffer.Append(" <addlpaxcostvalue>" & dr("addlpaxcostvalue").ToString & "</addlpaxcostvalue>")
                                    strBuffer.Append(" <totalcostvalue>" & dr("totalcostvalue").ToString & "</totalcostvalue>")
                                    strBuffer.Append(" <adultcplistcode>" & dr("adultcplistcode").ToString & "</adultcplistcode>")
                                    strBuffer.Append(" <childcplistcode>" & dr("childcplistcode").ToString & "</childcplistcode>")
                                    strBuffer.Append(" <unitcplistcode>" & dr("unitcplistcode").ToString & "</unitcplistcode>")
                                    strBuffer.Append(" <addlpaxcplistcode>" & dr("addlpaxcplistcode").ToString & "</addlpaxcplistcode>")
                                    strBuffer.Append(" <wlcurrcode>" & dr("wlcurrcode").ToString & "</wlcurrcode>")
                                    strBuffer.Append(" <wlconvrate>" & dr("wlconvrate").ToString & "</wlconvrate>")
                                    strBuffer.Append(" <wlmarkupper>" & dr("wlmarkupperc").ToString & "</wlmarkupper>")


                                    Dim totalcostvalue As Decimal = IIf(CType(dr("totalcostvalue").ToString, String) = "", "0.00", CType(dr("totalcostvalue").ToString, String))
                                    Dim cVATPer As Decimal = IIf(CType(dr("CostVATPerc").ToString, String) = "", "0.00", CType(dr("CostVATPerc").ToString, String))

                                    Dim dCostTaxableValue As Decimal = 0
                                    Dim dCostVATValue As Decimal = 0


                                    If CType(dr("CostWithTax").ToString, String) = "1" Then
                                        dCostTaxableValue = (totalcostvalue / (1 + (cVATPer / 100)))
                                        dCostVATValue = totalcostvalue - dCostTaxableValue
                                    Else
                                        dCostVATValue = (totalcostvalue * (cVATPer / 100))
                                        dCostTaxableValue = totalcostvalue + dCostVATValue
                                    End If

                                    Dim totalsalevalue As Decimal = IIf(CType(dr("totalsalevalue").ToString, String) = "", "0.00", CType(dr("totalsalevalue").ToString, String))
                                    Dim PriceVATPer As Decimal = IIf(CType(dr("PriceVATPerc").ToString, String) = "", "0.00", CType(dr("PriceVATPerc").ToString, String))

                                    Dim dPriceTaxableValue As Decimal = 0
                                    Dim PriceVATValue As Decimal = 0

                                    '*** Sell Price always include TAX
                                    'If CType(dr("PriceWithTax").ToString, String) = "1" Then
                                    dPriceTaxableValue = (totalsalevalue / (1 + (PriceVATPer / 100)))
                                    PriceVATValue = totalsalevalue - dPriceTaxableValue
                                    'Else
                                    'PriceVATValue = (totalsalevalue * (PriceVATPer / 100))
                                    'dPriceTaxableValue = totalsalevalue + PriceVATValue
                                    'End If


                                    dr("CostTaxableValue") = Math.Round(dCostTaxableValue, 3)
                                    dr("CostVATValue") = Math.Round(dCostVATValue, 3)

                                    dr("PriceTaxableValue") = Math.Round(dPriceTaxableValue, 3)
                                    dr("PriceVATValue") = Math.Round(PriceVATValue, 3)


                                    strBuffer.Append(" <CostTaxableValue>" & CType(dr("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                                    strBuffer.Append(" <CostVATValue>" & CType(dr("CostVATValue").ToString, String) & "</CostVATValue>")
                                    strBuffer.Append(" <CostVATPerc>" & CType(dr("CostVATPerc").ToString, String) & "</CostVATPerc>")
                                    strBuffer.Append(" <CostWithTax>" & CType(dr("CostWithTax").ToString, String) & "</CostWithTax>")
                                    strBuffer.Append(" <PriceTaxableValue>" & CType(dr("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                                    strBuffer.Append(" <PriceVATValue>" & CType(dr("PriceVATValue").ToString, String) & "</PriceVATValue>")
                                    strBuffer.Append(" <PriceVATPerc>" & CType(dr("PriceVATPerc").ToString, String) & "</PriceVATPerc>")
                                    strBuffer.Append(" <PriceWithTax>" & CType(dr("PriceWithTax").ToString, String) & "</PriceWithTax>")
                                    strBuffer.Append("</Table>")
                                End If

                                strBuffer.Append("</DocumentElement>")



                            End If

                        Next
                        For Each gvRow As DataListItem In dlMADepartureSearchResults.Items
                            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                            Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")

                            If chkbooknow.Checked = True Then

                                strBuffer.Append("<DocumentElement>")
                                Dim dr As DataRow = dss.Tables(1).Select("airportmatypecode='" & hdAirportTypeCode.Value & "'").First

                                If Not dr Is Nothing Then


                                    If Val(hdlineno.Value) = 0 Then
                                        If strALineNo = "" Then
                                            strALineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLMASearch.OBRequestId, "AIRPORT_MEET")
                                        Else
                                            strALineNo = CType(strALineNo, Integer) + 1
                                        End If
                                        ' strALineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLMASearch.OBRequestId, "AIRPORT_MEET")
                                    Else
                                        strALineNo = hdlineno.Value
                                    End If
                                    If rlineonostring <> "" Then
                                        rlineonostring = rlineonostring + ";" + CType(strALineNo, String)
                                    Else
                                        rlineonostring = strALineNo
                                    End If


                                    'If Val(hdlineno.Value) = 0 Then
                                    '    If Session("sMALineNo") Is Nothing Then
                                    '        Session.Add("sMALineNo", "1")
                                    '    Else
                                    '        Session("sMALineNo") = CType(Session("sMALineNo"), Integer) + 1

                                    '    End If
                                    'Else
                                    '    Session("sMALineNo") = hdlineno.Value
                                    'End If

                                    'If rlineonostring <> "" Then
                                    '    rlineonostring = rlineonostring + ";" + CType(Session("sMALineNo"), String)
                                    'Else
                                    '    rlineonostring = Session("sMALineNo")
                                    'End If


                                    Dim lineno As Integer = CType(Session("sMALineNo"), Integer)
                                    Dim str As String = ""
                                    Dim strZero As String = "0"
                                    strBuffer.Append("<Table>")
                                    strBuffer.Append(" <alineno>" & strALineNo & "</alineno>")
                                    strBuffer.Append(" <airportmatype>DEPARTURE</airportmatype>")
                                    strBuffer.Append(" <othtypcode>" & hdAirportTypeCode.Value & "</othtypcode>")
                                    strBuffer.Append(" <airportmadate>" & Format(CType(dr("airportmadate").ToString, Date), "yyyy/MM/dd") & "</airportmadate>")

                                    Dim strQuery As String = ""
                                    Dim flighttranid As String = ""
                                    strQuery = "select flight_tranid from flightmast where active=1 and flightcode='" & CType(txtDepartureFlight.Text, String) & "'"
                                    flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                                    strBuffer.Append(" <flightcode>" & txtDepartureFlight.Text & "</flightcode>")
                                    strBuffer.Append(" <flight_tranid>" & flighttranid & "</flight_tranid>")
                                    strBuffer.Append(" <flighttime>" & txtDepartureTime.Text & "</flighttime>")
                                    strBuffer.Append(" <flightclscode>" & ddlMADepFlightClass.SelectedValue & "</flightclscode>")
                                    strBuffer.Append(" <airportbordercode>" & txtMADepairportdropcode.Text & "</airportbordercode>")
                                    strBuffer.Append(" <trdepflightcode>" & str & "</trdepflightcode>")
                                    strBuffer.Append(" <trdepflight_tranid>" & str & "</trdepflight_tranid>")
                                    strBuffer.Append(" <trdepflighttime>" & str & "</trdepflighttime>")
                                    strBuffer.Append(" <trdepflightclscode>" & str & "</trdepflightclscode>")
                                    strBuffer.Append(" <trdepairportbordercode>" & str & "</trdepairportbordercode>")
                                    strBuffer.Append(" <adults>" & dr("adults").ToString & "</adults>")
                                    strBuffer.Append(" <child>" & dr("child").ToString & "</child>")
                                    strBuffer.Append(" <childagestring>" & dr("childagestring").ToString & "</childagestring>")
                                    strBuffer.Append(" <maxpax>" & dr("maxpax").ToString & "</maxpax>")
                                    strBuffer.Append(" <childtocharge>" & dr("childtocharge").ToString & "</childtocharge>")
                                    strBuffer.Append(" <units>" & dr("units").ToString & "</units>")
                                    strBuffer.Append(" <complimentarycust>" & dr("complimentarycust").ToString & "</complimentarycust>")
                                    strBuffer.Append(" <addlpax>" & dr("addlpax").ToString & "</addlpax>")
                                    strBuffer.Append(" <adultprice>" & dr("adultprice").ToString & "</adultprice>")
                                    strBuffer.Append(" <childprice>" & dr("childprice").ToString & "</childprice>")
                                    strBuffer.Append(" <unitprice>" & dr("unitprice").ToString & "</unitprice>")
                                    strBuffer.Append(" <addlpaxprice>" & dr("addlpaxprice").ToString & "</addlpaxprice>")
                                    strBuffer.Append(" <adultsalevalue>" & dr("adultsalevalue").ToString & "</adultsalevalue>")
                                    strBuffer.Append(" <childsalevalue>" & dr("childsalevalue").ToString & "</childsalevalue>")
                                    strBuffer.Append(" <unitsalevalue>" & dr("unitsalevalue").ToString & "</unitsalevalue>")
                                    strBuffer.Append(" <addlpaxsalevalue>" & dr("addlpaxsalevalue").ToString & "</addlpaxsalevalue>")
                                    strBuffer.Append(" <totalsalevalue>" & dr("totalsalevalue").ToString & "</totalsalevalue>")

                                    strBuffer.Append(" <wladultprice>" & dr("wladultprice").ToString & "</wladultprice>")
                                    strBuffer.Append(" <wlchildprice>" & dr("wlchildprice").ToString & "</wlchildprice>")
                                    strBuffer.Append(" <wlunitprice>" & dr("wlunitprice").ToString & "</wlunitprice>")
                                    strBuffer.Append(" <wladdlpaxprice>" & dr("wladdlpaxprice").ToString & "</wladdlpaxprice>")
                                    strBuffer.Append(" <wladultsalevalue>" & dr("wladultsalevalue").ToString & "</wladultsalevalue>")

                                    strBuffer.Append(" <wlchildsalevalue>" & dr("wlchildsalevalue").ToString & "</wlchildsalevalue>")
                                    strBuffer.Append(" <wlunitsalevalue>" & dr("wlunitsalevalue").ToString & "</wlunitsalevalue>")
                                    strBuffer.Append(" <wladdlpaxsalevalue>" & dr("wladdlpaxsalevalue").ToString & "</wladdlpaxsalevalue>")
                                    strBuffer.Append(" <wltotalsalevalue>" & dr("wladultsalevalue").ToString & "</wltotalsalevalue>")
                                    strBuffer.Append(" <overrideprice>" & IIf(chkMAoverride.Checked = True, 1, 0) & "</overrideprice>")
                                    strBuffer.Append(" <adultplistcode>" & dr("adultplistcode").ToString & "</adultplistcode>")
                                    strBuffer.Append(" <childplistcode>" & dr("childplistcode").ToString & "</childplistcode>")
                                    strBuffer.Append(" <unitplistcode>" & dr("unitplistcode").ToString & "</unitplistcode>")
                                    strBuffer.Append(" <addlpaxplistcode>" & dr("addlpaxplistcode").ToString & "</addlpaxplistcode>")


                                    strBuffer.Append(" <preferredsupplier>" & dr("preferredsupplier").ToString & "</preferredsupplier>")
                                    strBuffer.Append(" <adultcprice>" & dr("adultcprice").ToString & "</adultcprice>")
                                    strBuffer.Append(" <childcprice>" & dr("childcprice").ToString & "</childcprice>")
                                    strBuffer.Append(" <unitcprice>" & dr("unitcprice").ToString & "</unitcprice>")
                                    strBuffer.Append(" <addlpaxcprice>" & dr("addlpaxcprice").ToString & "</addlpaxcprice>")
                                    strBuffer.Append(" <adultcostvalue>" & dr("adultcostvalue").ToString & "</adultcostvalue>")
                                    strBuffer.Append(" <childcostvalue>" & dr("childcostvalue").ToString & "</childcostvalue>")
                                    strBuffer.Append(" <unitcostvalue>" & dr("unitcostvalue").ToString & "</unitcostvalue>")

                                    strBuffer.Append(" <addlpaxcostvalue>" & dr("addlpaxcostvalue").ToString & "</addlpaxcostvalue>")
                                    strBuffer.Append(" <totalcostvalue>" & dr("totalcostvalue").ToString & "</totalcostvalue>")
                                    strBuffer.Append(" <adultcplistcode>" & dr("adultcplistcode").ToString & "</adultcplistcode>")
                                    strBuffer.Append(" <childcplistcode>" & dr("childcplistcode").ToString & "</childcplistcode>")
                                    strBuffer.Append(" <unitcplistcode>" & dr("unitcplistcode").ToString & "</unitcplistcode>")
                                    strBuffer.Append(" <addlpaxcplistcode>" & dr("addlpaxcplistcode").ToString & "</addlpaxcplistcode>")
                                    strBuffer.Append(" <wlcurrcode>" & dr("wlcurrcode").ToString & "</wlcurrcode>")
                                    strBuffer.Append(" <wlconvrate>" & dr("wlconvrate").ToString & "</wlconvrate>")
                                    strBuffer.Append(" <wlmarkupper>" & dr("wlmarkupperc").ToString & "</wlmarkupper>")

                                    Dim totalcostvalue As Decimal = IIf(CType(dr("totalcostvalue").ToString, String) = "", "0.00", CType(dr("totalcostvalue").ToString, String))
                                    Dim cVATPer As Decimal = IIf(CType(dr("CostVATPerc").ToString, String) = "", "0.00", CType(dr("CostVATPerc").ToString, String))

                                    Dim dCostTaxableValue As Decimal = 0
                                    Dim dCostVATValue As Decimal = 0


                                    If CType(dr("CostWithTax").ToString, String) = "1" Then
                                        dCostTaxableValue = (totalcostvalue / (1 + (cVATPer / 100)))
                                        dCostVATValue = totalcostvalue - dCostTaxableValue
                                    Else
                                        dCostVATValue = (totalcostvalue * (cVATPer / 100))
                                        dCostTaxableValue = totalcostvalue + dCostVATValue
                                    End If

                                    Dim totalsalevalue As Decimal = IIf(CType(dr("totalsalevalue").ToString, String) = "", "0.00", CType(dr("totalsalevalue").ToString, String))
                                    Dim PriceVATPer As Decimal = IIf(CType(dr("PriceVATPerc").ToString, String) = "", "0.00", CType(dr("PriceVATPerc").ToString, String))

                                    Dim dPriceTaxableValue As Decimal = 0
                                    Dim PriceVATValue As Decimal = 0

                                    '*** Sell Price always include TAX
                                    'If CType(dr("PriceWithTax").ToString, String) = "1" Then
                                    dPriceTaxableValue = (totalsalevalue / (1 + (PriceVATPer / 100)))
                                    PriceVATValue = totalsalevalue - dPriceTaxableValue
                                    'Else
                                    'PriceVATValue = (totalsalevalue * (PriceVATPer / 100))
                                    'dPriceTaxableValue = totalsalevalue + PriceVATValue
                                    'End If


                                    dr("CostTaxableValue") = Math.Round(dCostTaxableValue, 3)
                                    dr("CostVATValue") = Math.Round(dCostVATValue, 3)

                                    dr("PriceTaxableValue") = Math.Round(dPriceTaxableValue, 3)
                                    dr("PriceVATValue") = Math.Round(PriceVATValue, 3)


                                    strBuffer.Append(" <CostTaxableValue>" & CType(dr("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                                    strBuffer.Append(" <CostVATValue>" & CType(dr("CostVATValue").ToString, String) & "</CostVATValue>")
                                    strBuffer.Append(" <CostVATPerc>" & CType(dr("CostVATPerc").ToString, String) & "</CostVATPerc>")
                                    strBuffer.Append(" <CostWithTax>" & CType(dr("CostWithTax").ToString, String) & "</CostWithTax>")
                                    strBuffer.Append(" <PriceTaxableValue>" & CType(dr("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                                    strBuffer.Append(" <PriceVATValue>" & CType(dr("PriceVATValue").ToString, String) & "</PriceVATValue>")
                                    strBuffer.Append(" <PriceVATPerc>" & CType(dr("PriceVATPerc").ToString, String) & "</PriceVATPerc>")
                                    strBuffer.Append(" <PriceWithTax>" & CType(dr("PriceWithTax").ToString, String) & "</PriceWithTax>")
                                    strBuffer.Append("</Table>")
                                End If

                                strBuffer.Append("</DocumentElement>")



                            End If

                        Next
                        For Each gvRow As DataListItem In dlMATransitSearchResults.Items
                            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                            Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")

                            If chkbooknow.Checked = True Then

                                strBuffer.Append("<DocumentElement>")
                                Dim dr As DataRow = dss.Tables(2).Select("airportmatypecode='" & hdAirportTypeCode.Value & "'").First

                                If Not dr Is Nothing Then


                                    If Val(hdlineno.Value) = 0 Then
                                        If strALineNo = "" Then
                                            strALineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLMASearch.OBRequestId, "AIRPORT_MEET")
                                        Else
                                            strALineNo = CType(strALineNo, Integer) + 1
                                        End If
                                        '  strALineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLMASearch.OBRequestId, "AIRPORT_MEET")
                                    Else
                                        strALineNo = hdlineno.Value
                                    End If
                                    If rlineonostring <> "" Then
                                        rlineonostring = rlineonostring + ";" + CType(strALineNo, String)
                                    Else
                                        rlineonostring = strALineNo
                                    End If

                                    'If Val(hdlineno.Value) = 0 Then
                                    '    If Session("sMALineNo") Is Nothing Then
                                    '        Session.Add("sMALineNo", "1")
                                    '    Else
                                    '        Session("sMALineNo") = CType(Session("sMALineNo"), Integer) + 1

                                    '    End If
                                    'Else
                                    '    Session("sMALineNo") = hdlineno.Value
                                    'End If

                                    'If rlineonostring <> "" Then
                                    '    rlineonostring = rlineonostring + ";" + CType(Session("sMALineNo"), String)
                                    'Else
                                    '    rlineonostring = Session("sMALineNo")
                                    'End If


                                    Dim lineno As Integer = CType(Session("sMALineNo"), Integer)
                                    Dim str As String = ""
                                    Dim strZero As String = "0"
                                    strBuffer.Append("<Table>")
                                    strBuffer.Append(" <alineno>" & strALineNo & "</alineno>")
                                    strBuffer.Append(" <airportmatype>TRANSIT</airportmatype>")
                                    strBuffer.Append(" <othtypcode>" & hdAirportTypeCode.Value & "</othtypcode>")
                                    strBuffer.Append(" <airportmadate>" & Format(CType(dr("airportmadate").ToString, Date), "yyyy/MM/dd") & "</airportmadate>")

                                    Dim strQuery As String = ""
                                    Dim flighttranid As String = ""
                                    strQuery = "select flight_tranid from flightmast where active=1 and flightcode='" & CType(txtMAtranArrFlight.Text, String) & "'"
                                    flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                                    strBuffer.Append(" <flightcode>" & txtMAtranArrFlight.Text & "</flightcode>")
                                    strBuffer.Append(" <flight_tranid>" & flighttranid & "</flight_tranid>")
                                    strBuffer.Append(" <flighttime>" & txtMATranArrTime.Text & "</flighttime>")
                                    strBuffer.Append(" <flightclscode>" & ddltranarrflightclass.SelectedValue & "</flightclscode>")
                                    strBuffer.Append(" <airportbordercode>" & txtMATransitArrivalpickupcode.Text & "</airportbordercode>")

                                    Dim strTrasQuery As String = ""
                                    Dim flightTRtranid As String = ""
                                    strTrasQuery = "select flight_tranid from flightmast where active=1 and flightcode='" & CType(txtMAtranDepartureFlight.Text, String) & "'"
                                    flightTRtranid = objclsUtilities.ExecuteQueryReturnStringValue(strTrasQuery)


                                    strBuffer.Append(" <trdepflightcode>" & txtMAtranDepartureFlight.Text & "</trdepflightcode>")
                                    strBuffer.Append(" <trdepflight_tranid>" & flightTRtranid & "</trdepflight_tranid>")
                                    strBuffer.Append(" <trdepflighttime>" & txtMATranDepartureTime.Text & "</trdepflighttime>")
                                    strBuffer.Append(" <trdepflightclscode>" & ddlMAtrandepflightlass.SelectedValue & "</trdepflightclscode>")
                                    strBuffer.Append(" <trdepairportbordercode>" & txtMATransitDeparturepickupcode.Text & "</trdepairportbordercode>")

                                    strBuffer.Append(" <adults>" & dr("adults").ToString & "</adults>")
                                    strBuffer.Append(" <child>" & dr("child").ToString & "</child>")
                                    strBuffer.Append(" <childagestring>" & dr("childagestring").ToString & "</childagestring>")
                                    strBuffer.Append(" <maxpax>" & dr("maxpax").ToString & "</maxpax>")
                                    strBuffer.Append(" <childtocharge>" & dr("childtocharge").ToString & "</childtocharge>")
                                    strBuffer.Append(" <units>" & dr("units").ToString & "</units>")
                                    strBuffer.Append(" <addlpax>" & dr("addlpax").ToString & "</addlpax>")
                                    strBuffer.Append(" <complimentarycust>" & dr("complimentarycust").ToString & "</complimentarycust>")
                                    strBuffer.Append(" <adultprice>" & dr("adultprice").ToString & "</adultprice>")
                                    strBuffer.Append(" <childprice>" & dr("childprice").ToString & "</childprice>")
                                    strBuffer.Append(" <unitprice>" & dr("unitprice").ToString & "</unitprice>")
                                    strBuffer.Append(" <addlpaxprice>" & dr("addlpaxprice").ToString & "</addlpaxprice>")
                                    strBuffer.Append(" <adultsalevalue>" & dr("adultsalevalue").ToString & "</adultsalevalue>")
                                    strBuffer.Append(" <childsalevalue>" & dr("childsalevalue").ToString & "</childsalevalue>")
                                    strBuffer.Append(" <unitsalevalue>" & dr("unitsalevalue").ToString & "</unitsalevalue>")
                                    strBuffer.Append(" <addlpaxsalevalue>" & dr("addlpaxsalevalue").ToString & "</addlpaxsalevalue>")
                                    strBuffer.Append(" <totalsalevalue>" & dr("totalsalevalue").ToString & "</totalsalevalue>")

                                    strBuffer.Append(" <wladultprice>" & dr("wladultprice").ToString & "</wladultprice>")
                                    strBuffer.Append(" <wlchildprice>" & dr("wlchildprice").ToString & "</wlchildprice>")
                                    strBuffer.Append(" <wlunitprice>" & dr("wlunitprice").ToString & "</wlunitprice>")
                                    strBuffer.Append(" <wladdlpaxprice>" & dr("wladdlpaxprice").ToString & "</wladdlpaxprice>")
                                    strBuffer.Append(" <wladultsalevalue>" & dr("wladultsalevalue").ToString & "</wladultsalevalue>")

                                    strBuffer.Append(" <wlchildsalevalue>" & dr("wlchildsalevalue").ToString & "</wlchildsalevalue>")
                                    strBuffer.Append(" <wlunitsalevalue>" & dr("wlunitsalevalue").ToString & "</wlunitsalevalue>")
                                    strBuffer.Append(" <wladdlpaxsalevalue>" & dr("wladdlpaxsalevalue").ToString & "</wladdlpaxsalevalue>")
                                    strBuffer.Append(" <wltotalsalevalue>" & dr("wladultsalevalue").ToString & "</wltotalsalevalue>")

                                    strBuffer.Append(" <overrideprice>" & IIf(chkMAoverride.Checked = True, 1, 0) & "</overrideprice>")
                                    strBuffer.Append(" <adultplistcode>" & dr("adultplistcode").ToString & "</adultplistcode>")
                                    strBuffer.Append(" <childplistcode>" & dr("childplistcode").ToString & "</childplistcode>")
                                    strBuffer.Append(" <unitplistcode>" & dr("unitplistcode").ToString & "</unitplistcode>")
                                    strBuffer.Append(" <addlpaxplistcode>" & dr("addlpaxplistcode").ToString & "</addlpaxplistcode>")

                                    strBuffer.Append(" <preferredsupplier>" & dr("preferredsupplier").ToString & "</preferredsupplier>")
                                    strBuffer.Append(" <adultcprice>" & dr("adultcprice").ToString & "</adultcprice>")
                                    strBuffer.Append(" <childcprice>" & dr("childcprice").ToString & "</childcprice>")
                                    strBuffer.Append(" <unitcprice>" & dr("unitcprice").ToString & "</unitcprice>")
                                    strBuffer.Append(" <addlpaxcprice>" & dr("addlpaxcprice").ToString & "</addlpaxcprice>")
                                    strBuffer.Append(" <adultcostvalue>" & dr("adultcostvalue").ToString & "</adultcostvalue>")
                                    strBuffer.Append(" <childcostvalue>" & dr("childcostvalue").ToString & "</childcostvalue>")
                                    strBuffer.Append(" <unitcostvalue>" & dr("unitcostvalue").ToString & "</unitcostvalue>")

                                    strBuffer.Append(" <addlpaxcostvalue>" & dr("addlpaxcostvalue").ToString & "</addlpaxcostvalue>")
                                    strBuffer.Append(" <totalcostvalue>" & dr("totalcostvalue").ToString & "</totalcostvalue>")
                                    strBuffer.Append(" <adultcplistcode>" & dr("adultcplistcode").ToString & "</adultcplistcode>")
                                    strBuffer.Append(" <childcplistcode>" & dr("childcplistcode").ToString & "</childcplistcode>")
                                    strBuffer.Append(" <unitcplistcode>" & dr("unitcplistcode").ToString & "</unitcplistcode>")
                                    strBuffer.Append(" <addlpaxcplistcode>" & dr("addlpaxcplistcode").ToString & "</addlpaxcplistcode>")
                                    strBuffer.Append(" <wlcurrcode>" & dr("wlcurrcode").ToString & "</wlcurrcode>")
                                    strBuffer.Append(" <wlconvrate>" & dr("wlconvrate").ToString & "</wlconvrate>")
                                    strBuffer.Append(" <wlmarkupper>" & dr("wlmarkupperc").ToString & "</wlmarkupper>")

                                    Dim totalcostvalue As Decimal = IIf(CType(dr("totalcostvalue").ToString, String) = "", "0.00", CType(dr("totalcostvalue").ToString, String))
                                    Dim cVATPer As Decimal = IIf(CType(dr("CostVATPerc").ToString, String) = "", "0.00", CType(dr("CostVATPerc").ToString, String))

                                    Dim dCostTaxableValue As Decimal = 0
                                    Dim dCostVATValue As Decimal = 0


                                    If CType(dr("CostWithTax").ToString, String) = "1" Then
                                        dCostTaxableValue = (totalcostvalue / (1 + (cVATPer / 100)))
                                        dCostVATValue = totalcostvalue - dCostTaxableValue
                                    Else
                                        dCostVATValue = (totalcostvalue * (cVATPer / 100))
                                        dCostTaxableValue = totalcostvalue + dCostVATValue
                                    End If

                                    Dim totalsalevalue As Decimal = IIf(CType(dr("totalsalevalue").ToString, String) = "", "0.00", CType(dr("totalsalevalue").ToString, String))
                                    Dim PriceVATPer As Decimal = IIf(CType(dr("PriceVATPerc").ToString, String) = "", "0.00", CType(dr("PriceVATPerc").ToString, String))

                                    Dim dPriceTaxableValue As Decimal = 0
                                    Dim PriceVATValue As Decimal = 0

                                    '*** Sell Price always include TAX
                                    'If CType(dr("PriceWithTax").ToString, String) = "1" Then
                                    dPriceTaxableValue = (totalsalevalue / (1 + (PriceVATPer / 100)))
                                    PriceVATValue = totalsalevalue - dPriceTaxableValue
                                    'Else
                                    'PriceVATValue = (totalsalevalue * (PriceVATPer / 100))
                                    'dPriceTaxableValue = totalsalevalue + PriceVATValue
                                    'End If


                                    dr("CostTaxableValue") = Math.Round(dCostTaxableValue, 3)
                                    dr("CostVATValue") = Math.Round(dCostVATValue, 3)

                                    dr("PriceTaxableValue") = Math.Round(dPriceTaxableValue, 3)
                                    dr("PriceVATValue") = Math.Round(PriceVATValue, 3)


                                    strBuffer.Append(" <CostTaxableValue>" & CType(dr("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                                    strBuffer.Append(" <CostVATValue>" & CType(dr("CostVATValue").ToString, String) & "</CostVATValue>")
                                    strBuffer.Append(" <CostVATPerc>" & CType(dr("CostVATPerc").ToString, String) & "</CostVATPerc>")
                                    strBuffer.Append(" <CostWithTax>" & CType(dr("CostWithTax").ToString, String) & "</CostWithTax>")
                                    strBuffer.Append(" <PriceTaxableValue>" & CType(dr("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                                    strBuffer.Append(" <PriceVATValue>" & CType(dr("PriceVATValue").ToString, String) & "</PriceVATValue>")
                                    strBuffer.Append(" <PriceVATPerc>" & CType(dr("PriceVATPerc").ToString, String) & "</PriceVATPerc>")
                                    strBuffer.Append(" <PriceWithTax>" & CType(dr("PriceWithTax").ToString, String) & "</PriceWithTax>")

                                    strBuffer.Append("</Table>")
                                End If

                                strBuffer.Append("</DocumentElement>")



                            End If

                        Next
                        objBLLMASearch.OBTransferXml = strBuffer.ToString
                        objBLLMASearch.OBRlinenoString = rlineonostring
                        If Not Session("sobjResParam") Is Nothing Then
                            objResParam = Session("sobjResParam")
                            objBLLMASearch.SubUserCode = objResParam.SubUserCode
                        End If

                        If objBLLMASearch.SavingAMBookingtemp() Then 'renamed function name on 20180724 by abin
                            Session("sRequestId") = objBLLMASearch.OBRequestId
                            Session("sobjBLLMASearchActive") = Session("sobjBLLMASearch")
                            Response.Redirect("~\MoreServices.aspx")
                        End If

                    End If
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: btnBookNow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub Airportsearch()
        Try

            Dim objBLLMASearch As New BLLMASearch
            Dim strSearchCriteria As String = ""
            Dim strArrivaldate As String = txtMAArrivaldate.Text
            Dim strDeparturedate As String = txtMADeparturedate.Text

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

            Session("SelectedMAArrival") = Nothing
            Session("SelectedMADeparture") = Nothing
            Session("SelectedMATransit") = Nothing
            ViewState("DepShow") = "0"
            ViewState("ArrShow") = "0"
            ViewState("TransitShow") = "0"

            If HttpContext.Current.Session("sLoginType") = "RO" Then

                'If strHotelCode = "" And chkOveridePrice.Checked = True Then
                '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any hotel name. \n </br></br></br></br></br></br></br></br></br>* If override is ticked then Hotel selection is compulsory.")
                '    Exit Sub
                'End If
            End If

            Dim strQueryString As String = ""

            If Not Session("sEditRequestId") Is Nothing Then
                objBLLMASearch.AmendRequestid = Session("sEditRequestId")
                objBLLMASearch.AmendLineno = hdlineno.Value
            Else
                objBLLMASearch.AmendRequestid = GetExistingRequestId()
                objBLLMASearch.AmendLineno = hdlineno.Value
            End If


            '' Arrival
            If chkarrival.Checked = True Then
                objBLLMASearch.MAArrivalType = "ARRIVAL"
                strSearchCriteria = "ARRIVAL:Yes"
            Else
                strSearchCriteria = "ARRIVAL:Yes"
            End If

            If txtMAArrivaldate.Text <> "" Then
                objBLLMASearch.MAArrTransferDate = txtMAArrivaldate.Text
                strSearchCriteria = strSearchCriteria & "||" & "ARRIVAL DATE:" & txtMAArrivaldate.Text
            End If

            If txtArrivalflight.Text <> "" Then
                objBLLMASearch.MAArrFlightNo = txtArrivalflight.Text
                strSearchCriteria = strSearchCriteria & "||" & "FlightNo:" & txtArrivalflight.Text
            End If
            If ddlMAArrFlightClass.SelectedIndex <> 0 And chkarrival.Checked = True Then
                objBLLMASearch.MAArrFlightClass = ddlMAArrFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & "FlightClass:" & ddlMAArrFlightClass.Text
            End If
            If txtArrivalTime.Text <> "" Then
                objBLLMASearch.MAArrFlightTime = txtArrivalTime.Text
                strSearchCriteria = strSearchCriteria & "||" & "FlightTime:" & txtArrivalTime.Text
            End If
            If txtMAArrivalpickupcode.Text <> "" Then
                objBLLMASearch.MAArrPickupCode = txtMAArrivalpickupcode.Text
            End If

            If txtMAArrivalpickup.Text <> "" Then
                objBLLMASearch.MAArrPickupName = txtMAArrivalpickup.Text
                strSearchCriteria = strSearchCriteria & "||" & "Arrivalpickup:" & txtMAArrivalpickup.Text
            End If





            If txtMASourcecountrycode.Text <> "" Then
                objBLLMASearch.SourceCountryCode = txtMASourcecountrycode.Text
            End If

            If txtMASourcecountry.Text <> "" Then
                objBLLMASearch.SourceCountry = txtMASourcecountry.Text
                strSearchCriteria = strSearchCriteria & "||" & "SourceCountry:" & txtMASourcecountry.Text
            End If

            If txtMACustomercode.Text <> "" Then
                objBLLMASearch.CustomerCode = txtMACustomercode.Text

            End If

            If txtMACustomer.Text <> "" Then
                objBLLMASearch.Customer = txtMACustomer.Text
                strSearchCriteria = strSearchCriteria & "||" & "Agent:" & txtMACustomer.Text
            End If

            If txtMACustomer.Text <> "" Then
                objBLLMASearch.Customer = txtMACustomer.Text
            End If

            If strAdult <> "" Then
                objBLLMASearch.Adult = strAdult
            End If

            '''''''''''
            ''Departure
            If chkDeparture.Checked = True Then
                objBLLMASearch.MADepartueType = "DEPARTURE"
                strSearchCriteria = strSearchCriteria & "||" & "DEPARTURE:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & "DEPARTURE:No"
            End If

            If txtMADeparturedate.Text <> "" Then
                objBLLMASearch.MADepTransferDate = txtMADeparturedate.Text
                strSearchCriteria = strSearchCriteria & "||" & "DEPARTURE DATE:" & txtMADeparturedate.Text
            End If

            If txtDepartureFlight.Text <> "" Then
                objBLLMASearch.MADepFlightNo = txtDepartureFlight.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepartureFlight:" & txtDepartureFlight.Text
            End If
            If ddlMADepFlightClass.SelectedIndex <> 0 And chkDeparture.Checked = True Then
                objBLLMASearch.MADepFlightClass = ddlMADepFlightClass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & "DepFlightClass:" & ddlMADepFlightClass.Text
            End If
            If txtDepartureTime.Text <> "" Then
                objBLLMASearch.MADepFlightTime = txtDepartureTime.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepFlightTime:" & txtDepartureTime.Text
            End If


            If txtMADepairportdropcode.Text <> "" Then
                objBLLMASearch.MADepDropCode = txtMADepairportdropcode.Text
            End If

            If txtMADepairportdrop.Text <> "" Then
                objBLLMASearch.MADepDropName = txtMADepairportdrop.Text
                strSearchCriteria = strSearchCriteria & "||" & "DEPARTURE AIRPORT:" & txtMADepairportdrop.Text
            End If
            '''''''''''

            ''Transit
            If chktransit.Checked = True Then
                objBLLMASearch.MATransitType = "TRANSIT"
                strSearchCriteria = strSearchCriteria & "||" & "TRANSIT:Yes"
            Else
                strSearchCriteria = strSearchCriteria & "||" & "TRANSIT:No"
            End If

            If txtMATrandepdate.Text <> "" Then
                objBLLMASearch.MATranDepDate = txtMATrandepdate.Text
                strSearchCriteria = strSearchCriteria & "||" & "TranDepDate:" & txtMATrandepdate.Text
            End If

            If txtTransitarrdate.Text <> "" Then
                objBLLMASearch.MATranArrDate = txtTransitarrdate.Text
                strSearchCriteria = strSearchCriteria & "||" & "TranArrDate:" & txtTransitarrdate.Text
            End If

            If txtMAtranArrFlight.Text <> "" Then
                objBLLMASearch.MATranArrFlightNo = txtMAtranArrFlight.Text
                strSearchCriteria = strSearchCriteria & "||" & "TransArrFlightNo:" & txtMAtranArrFlight.Text
            End If

            If txtMAtranDepartureFlight.Text <> "" Then
                objBLLMASearch.MATranDepFlightNo = txtMAtranDepartureFlight.Text
                strSearchCriteria = strSearchCriteria & "||" & "TranDepFlightNo:" & txtMAtranDepartureFlight.Text
            End If

            If ddlMAtrandepflightlass.SelectedIndex <> 0 And chktransit.Checked = True Then
                objBLLMASearch.MATranDepFlightClass = ddlMAtrandepflightlass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & "DepFlightClass:" & ddlMAtrandepflightlass.Text
            End If

            If ddltranarrflightclass.SelectedIndex <> 0 And chktransit.Checked = True Then
                objBLLMASearch.MATranArrFlightClass = ddltranarrflightclass.SelectedValue
                strSearchCriteria = strSearchCriteria & "||" & "TranArrFlightClass:" & ddltranarrflightclass.Text
            End If

            If txtMATranArrTime.Text <> "" Then
                objBLLMASearch.MATranArrFlightTime = txtMATranArrTime.Text
                strSearchCriteria = strSearchCriteria & "||" & "TranArrTime:" & txtMATranArrTime.Text
            End If

            If txtMATranDepartureTime.Text <> "" Then
                objBLLMASearch.MATranDepFlightTime = txtMATranDepartureTime.Text
                strSearchCriteria = strSearchCriteria & "||" & "DepartureTime:" & txtMATranDepartureTime.Text
            End If

            If txtMATransitArrivalpickupcode.Text <> "" Then
                objBLLMASearch.MATranArrPickupCode = txtMATransitArrivalpickupcode.Text

            End If

            If txtMAtranArrivalpickup.Text <> "" Then
                objBLLMASearch.MATranArrPickupName = txtMAtranArrivalpickup.Text
                strSearchCriteria = strSearchCriteria & "||" & "tranArrivalpickup:" & txtMAtranArrivalpickup.Text
            End If

            If txtMATransitDeparturepickupcode.Text <> "" Then
                objBLLMASearch.MATranDepDropCode = txtMATransitDeparturepickupcode.Text
            End If

            If txtMAtranDeppickup.Text <> "" Then
                objBLLMASearch.MATranDepDropName = txtMAtranDeppickup.Text
                strSearchCriteria = strSearchCriteria & "||" & "TranDepDropName:" & txtMAtranDeppickup.Text
            End If


            ''''

            If strAdult <> "" Then
                objBLLMASearch.Adult = strAdult
                strSearchCriteria = strSearchCriteria & "||" & "TranDepAdultDropName:" & strAdult
            End If



            If strChildren <> "" Then
                objBLLMASearch.Children = strChildren
                strSearchCriteria = strSearchCriteria & "||" & "Children:" & strChildren
                If strChildren = "1" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.ChildAgeString = strChild1
                    objBLLMASearch.Child2 = ""
                    objBLLMASearch.Child3 = ""
                    objBLLMASearch.Child4 = ""
                    objBLLMASearch.Child5 = ""
                    objBLLMASearch.Child6 = ""
                    objBLLMASearch.Child7 = ""
                    objBLLMASearch.Child8 = ""
                ElseIf strChildren = "2" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = ""
                    objBLLMASearch.Child4 = ""
                    objBLLMASearch.Child5 = ""
                    objBLLMASearch.Child6 = ""
                    objBLLMASearch.Child7 = ""
                    objBLLMASearch.Child8 = ""
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = ""
                    objBLLMASearch.Child5 = ""
                    objBLLMASearch.Child6 = ""
                    objBLLMASearch.Child7 = ""
                    objBLLMASearch.Child8 = ""

                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = ""
                    objBLLMASearch.Child6 = ""
                    objBLLMASearch.Child7 = ""
                    objBLLMASearch.Child8 = ""
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.Child6 = ""
                    objBLLMASearch.Child7 = ""
                    objBLLMASearch.Child8 = ""
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.Child6 = strChild6
                    objBLLMASearch.Child7 = ""
                    objBLLMASearch.Child8 = ""
                    objBLLMASearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                ElseIf strChildren = "7" Then
                    objBLLMASearch.Child1 = strChild1
                    objBLLMASearch.Child2 = strChild2
                    objBLLMASearch.Child3 = strChild3
                    objBLLMASearch.Child4 = strChild4
                    objBLLMASearch.Child5 = strChild5
                    objBLLMASearch.Child6 = strChild6
                    objBLLMASearch.Child7 = strChild7
                    objBLLMASearch.Child8 = ""
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
            End If
            strSearchCriteria = strSearchCriteria & "||" & "ChildAgeString:" & objBLLMASearch.ChildAgeString

            If chkMAoverride.Checked = True Then
                objBLLMASearch.OverridePrice = "1"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice:Yes"
            Else
                objBLLMASearch.OverridePrice = "0"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice:No"
            End If

            objBLLMASearch.TransferType = "TRANSFER"
            strSearchCriteria = strSearchCriteria & "||" & "TransferType:TRANSFER"

            objBLLMASearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & "LoginType:" & objBLLMASearch.LoginType
            objBLLMASearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLMASearch.CustomerCode, Session("sAgentCode")) 'Session("sAgentCode")
            strSearchCriteria = strSearchCriteria & "||" & "AgentCode:" & objBLLMASearch.AgentCode
            objBLLMASearch.WebUserName = Session("GlobalUserName")

            strSearchCriteria = strSearchCriteria & "||" & "WebUserName:" & objBLLMASearch.WebUserName

            Session("sobjBLLMASearch") = objBLLMASearch

            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                Dim objBLLCommonFuntions As New BLLCommonFuntions()
                ' Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Airport Meet Search Page", "Airport Meet Search", strSearchCriteria, Session("GlobalUserName"))
            End If

            Dim dsSearchResults As New DataSet
            dsSearchResults = objBLLMASearch.GetSearchDetails()

            If dsSearchResults.Tables(4).Rows.Count = 0 Then
                dvhotnoshow.Style.Add("display", "block")
            Else
                dvhotnoshow.Style.Add("display", "none")
            End If


            Session("sDSMASearchResults") = dsSearchResults
            If dsSearchResults.Tables.Count > 0 Then
                Dim dvArrivalDetails As DataView = New DataView(dsSearchResults.Tables(0))
                Dim dvDepartureDetails As DataView = New DataView(dsSearchResults.Tables(1))
                Dim dvTransitDetails As DataView = New DataView(dsSearchResults.Tables(2))
                Dim recordCount As Integer = dvArrivalDetails.Count + dvDepartureDetails.Count + dvTransitDetails.Count

                Me.PopulatePager(recordCount)

                BindAirportMeetTypes(dsSearchResults.Tables(4))
                BindArrivalDetails(dvArrivalDetails)
                BindDepartureDetails(dvDepartureDetails)
                BindTransitDetails(dvTransitDetails)
                BindPricefilter(dsSearchResults.Tables(3))

                lblHotelCount.Text = recordCount & " Records Found"
            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Airportsearch()
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



    Private Sub BindArrivalDetails(ByVal dvResults As DataView, Optional ByVal showmore As String = "")
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLMASearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If
        Dim dt As New DataTable
        dt = dvResults.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then
            lblHotelCount.Text = dt.Rows.Count & " Records Found"
            Dim iPageIndex As Integer = 1
            Dim iPageSize As Integer = dt.Rows.Count
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sMAMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sMAMailBoxPageIndex")
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

            ' dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count

            dlMAArrivalSearchResults.DataSource = dv
            dlMAArrivalSearchResults.DataBind()

        Else
            dlMAArrivalSearchResults.DataBind()
        End If

        If Not Session("SelectedMAArrival") Is Nothing Then

            dt = Session("SelectedMAArrival")

            For Each gvRow As DataListItem In dlMAArrivalSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")





                Dim lblMAImage As Label = gvRow.FindControl("lblMAImage")
                Dim lblAirportTypeName As Label = gvRow.FindControl("lblAirportTypeName")
                Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                Dim lblminpax1 As Label = gvRow.FindControl("lblminpax1")
                Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax1")
                Dim lblunit As Label = gvRow.FindControl("lblunit")
                Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
                Dim lblprice As Label = gvRow.FindControl("lblprice")
                Dim lblvalue As Label = gvRow.FindControl("lblvalue")

                Dim lblratebasis As Label = gvRow.FindControl("lblratebasis")
                Dim lblairportmadate As Label = gvRow.FindControl("lblairportmadate")
                Dim lbladdlpax As Label = gvRow.FindControl("lbladdlpax")
                Dim lbladultprice As Label = gvRow.FindControl("lbladultprice")
                Dim lblchildprice As Label = gvRow.FindControl("lblchildprice")
                Dim lblunitprice As Label = gvRow.FindControl("lblunitprice")
                Dim lbladdlpaxprice As Label = gvRow.FindControl("lbladdlpaxprice")
                Dim lbladultsalevalue As Label = gvRow.FindControl("lbladultsalevalue")
                Dim lblchildsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                Dim lbladdlpaxsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                Dim lnkcumunits As LinkButton = gvRow.FindControl("lnkcumunits")

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString = hdAirportTypeCode.Value And dt.Rows(i)("Selected").ToString = "1" Then
                        chkbooknow.Checked = True
                    End If
                Next

            Next
        End If


        For Each gvRow As DataListItem In dlMAArrivalSearchResults.Items

            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
            Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")
            Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
            Dim lblunit As Label = gvRow.FindControl("lblunit")
            Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
            Dim lbladults As Label = gvRow.FindControl("lbladults")
            Dim lblchild As Label = gvRow.FindControl("lblchild")
            Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
            Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
            Dim lblprice As Label = gvRow.FindControl("lblprice")
            Dim lblvalue As Label = gvRow.FindControl("lblvalue")

            If hdnselected.Value = 1 Then
                chkbooknow.Checked = True
                
            End If
        Next

    End Sub
    Private Sub BindDepartureDetails(ByVal dvResults As DataView, Optional ByVal showmore As String = "")
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLMASearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If
        Dim dt As New DataTable
        dt = dvResults.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then

            Dim iPageIndex As Integer = 1
            Dim iPageSize As Integer = dt.Rows.Count
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sMAMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sMAMailBoxPageIndex")
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

            '  dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count

            dlMADepartureSearchResults.DataSource = dv
            dlMADepartureSearchResults.DataBind()

        Else
            dlMADepartureSearchResults.DataBind()
        End If


        If Not Session("SelectedMADeparture") Is Nothing Then

            dt = Session("SelectedMADeparture")

            For Each gvRow As DataListItem In dlMADepartureSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")


                Dim lblMAImage As Label = gvRow.FindControl("lblMAImage")
                Dim lblAirportTypeName As Label = gvRow.FindControl("lblAirportTypeName")
                Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                Dim lblminpax1 As Label = gvRow.FindControl("lblminpax1")
                Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax1")
                Dim lblunit As Label = gvRow.FindControl("lblunit")
                Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
                Dim lblprice As Label = gvRow.FindControl("lblprice")
                Dim lblvalue As Label = gvRow.FindControl("lblvalue")

                Dim lblratebasis As Label = gvRow.FindControl("lblratebasis")
                Dim lblairportmadate As Label = gvRow.FindControl("lblairportmadate")
                Dim lbladdlpax As Label = gvRow.FindControl("lbladdlpax")
                Dim lbladultprice As Label = gvRow.FindControl("lbladultprice")
                Dim lblchildprice As Label = gvRow.FindControl("lblchildprice")
                Dim lblunitprice As Label = gvRow.FindControl("lblunitprice")
                Dim lbladdlpaxprice As Label = gvRow.FindControl("lbladdlpaxprice")
                Dim lbladultsalevalue As Label = gvRow.FindControl("lbladultsalevalue")
                Dim lblchildsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                Dim lbladdlpaxsalevalue As Label = gvRow.FindControl("lblchildsalevalue")

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString = hdAirportTypeCode.Value And dt.Rows(i)("Selected").ToString = "1" Then
                        chkbooknow.Checked = True
                    End If
                Next

            Next
        End If

        For Each gvRow As DataListItem In dlMADepartureSearchResults.Items

            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
            Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")
            Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
            Dim lblunit As Label = gvRow.FindControl("lblunit")
            Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
            Dim lbladults As Label = gvRow.FindControl("lbladults")
            Dim lblchild As Label = gvRow.FindControl("lblchild")
            Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
            Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
            Dim lblprice As Label = gvRow.FindControl("lblprice")
            Dim lblvalue As Label = gvRow.FindControl("lblvalue")

            If hdnselected.Value = 1 Then
                chkbooknow.Checked = True

            End If
        Next


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
        If dsSearchResults.Tables.Count > 0 Then
            If dsSearchResults.Tables(0).Rows.Count > 0 Then
                ''ARrival
                Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
                ''departure
                Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))

                ''departure
                Dim dvTransitMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))

                Dim strNotAirportMAType As String = ""
                If chkHotelStars.Items.Count > 0 Then
                    For Each chkitem As ListItem In chkHotelStars.Items
                        If chkitem.Selected = False Then
                            If strNotAirportMAType = "" Then
                                strNotAirportMAType = "'" & chkitem.Value & "'"
                            Else
                                strNotAirportMAType = strNotAirportMAType & "," & "'" & chkitem.Value & "'"
                            End If

                        End If
                    Next
                End If

                Dim strFilterCriteria As String = ""
                If strNotAirportMAType <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "airportmatypecode NOT IN (" & strNotAirportMAType & ")"
                    Else
                        strFilterCriteria = " airportmatypecode NOT IN (" & strNotAirportMAType & ")"
                    End If
                End If

                If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "totalsalevalue >=" & hdPriceMin.Value & " AND totalsalevalue <=" & hdPriceMax.Value
                    Else
                        strFilterCriteria = "totalsalevalue >=" & hdPriceMin.Value & " AND totalsalevalue <=" & hdPriceMax.Value
                    End If
                End If


                If strFilterCriteria <> "" Then
                    dvMaiDetails.RowFilter = strFilterCriteria
                End If
                If strFilterCriteria <> "" Then
                    dvDepMaiDetails.RowFilter = strFilterCriteria
                End If

                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "airportmatypename ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "totalsalevalue ASC"
                ElseIf ddlSorting.Text = "0" Then
                    dvMaiDetails.Sort = "totalsalevalue ASC"

                End If
                Dim recordCount As Integer = dvMaiDetails.Count + dvDepMaiDetails.Count + dvTransitMaiDetails.Count


                BindArrivalDetails(dvMaiDetails)
                BindDepartureDetails(dvDepMaiDetails)
                BindTransitDetails(dvTransitMaiDetails)
                Me.PopulatePager(recordCount)

                lblHotelCount.Text = recordCount & " Record Found "

            End If

        Else
            dlMAArrivalSearchResults.DataBind()
            dlMADepartureSearchResults.DataBind()
            dlMATransitSearchResults.DataBind()
        End If


    End Sub


    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Try
            System.Threading.Thread.Sleep(200)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            'BindHotelMainDetailsWithFilter(dsSearchResults)
            BindArrivalDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim strScript As String = "javascript: CallPriceSlider();"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)



    End Sub

    Protected Sub lbReadMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
            Dim lblHotelText As Label = CType(dlItem.FindControl("lblHotelText"), Label)
            Dim strText As String = lblHotelText.Text
            Dim strToolTip As String = lblHotelText.ToolTip
            If myLinkButton.Text = "Read More." Then
                lblHotelText.Text = strToolTip
                lblHotelText.ToolTip = strText
                myLinkButton.Text = "Read Less."
            Else
                lblHotelText.Text = strToolTip
                lblHotelText.ToolTip = strText
                myLinkButton.Text = "Read More."
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub ddlSorting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSorting.SelectedIndexChanged
        Try
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")

            BindArrivalDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: ddlSorting_SelectedIndexChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sMAMailBoxPageIndex") = pageIndex.ToString
            BindArrivalDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Sub PopulatePager(ByVal recordCount As Integer)

        Dim currentPage As Integer = 1
        If Not Session("sMAMailBoxPageIndex") Is Nothing Then
            currentPage = Session("sMAMailBoxPageIndex")
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
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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

    Protected Sub dlMADepartureSearchResults_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMADepartureSearchResults.ItemCreated
        If dlMADepartureSearchResults.Items.Count = 1 And e.Item.ItemType = 1 And ViewState("Depviewmorehide") = 1 Then
            Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbDepShowMore"), LinkButton)

            myLinkButton.Visible = False
        End If
    End Sub


    Protected Sub dlMADepartureSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMADepartureSearchResults.ItemDataBound
        Try

            If e.Item.ItemType = 1 Then
                Dim myarrButton As LinkButton = CType(e.Item.FindControl("lbDepShowMore"), LinkButton)
              
                If ViewState("DepShow") = "1" Then
                    myarrButton.Text = "Show Less"
                Else
                    myarrButton.Text = "Show More"
                End If

            End If


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Show MA Image
                Dim lblremarks As Label = CType(e.Item.FindControl("lblremarks"), Label)
                lblremarks.Text = lblremarks.Text.Replace(Environment.NewLine, "<br/>")
                Dim imgHotelImage As Image = CType(e.Item.FindControl("imgMAImage"), Image)
                Dim lblMAImage As Label = CType(e.Item.FindControl("lblMAImage"), Label)
                imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblMAImage.Text & "&type=3"
                Dim lblminpax As Label = CType(e.Item.FindControl("lblminpax"), Label)
                Dim lblmaxpax As Label = CType(e.Item.FindControl("lblmaxpax"), Label)
                Dim dvDepMin As HtmlGenericControl = CType(e.Item.FindControl("dvDepMin"), HtmlGenericControl)
                Dim dvDepMax As HtmlGenericControl = CType(e.Item.FindControl("dvDepMax"), HtmlGenericControl)
                If lblminpax.Text.Trim = "0 PAX" Then
                    dvDepMin.Visible = False
                End If
                If lblmaxpax.Text.Trim = "0 PAX" Then
                    dvDepMax.Visible = False
                End If
                Dim chkbooknow As CheckBox = CType(e.Item.FindControl("chkbooknow"), CheckBox)
                chkbooknow.Attributes.Add("onChange", "javascript:calculatevaluedep('" + CType(chkbooknow.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "' )")

                Dim lbtotalValue As LinkButton = CType(e.Item.FindControl("lbtotalValue"), LinkButton)
                Dim lbwltotalValue As LinkButton = CType(e.Item.FindControl("lbwltotalValue"), LinkButton)
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    lbwltotalValue.Visible = True
                    lbtotalValue.Visible = False
                    Dim lblTotalCurrcode As Label = CType(e.Item.FindControl("lblTotalCurrcode"), Label)
                    Dim lblwlcurrcode As Label = CType(e.Item.FindControl("lblwlcurrcode"), Label)
                    lblTotalCurrcode.Text = lblwlcurrcode.Text

                    Dim dTotalValue As Double = IIf(lbwltotalValue.Text = "", 0, lbwltotalValue.Text)
                    lbwltotalValue.Text = Math.Round(dTotalValue)
                

                Else
                    Dim dTotalValue As Double = IIf(lbtotalValue.Text = "", 0, lbtotalValue.Text)
                    lbtotalValue.Text = Math.Round(dTotalValue)
                    lbwltotalValue.Visible = False
                    lbtotalValue.Visible = True
                End If


                If hdBookingEngineRateType.Value = "1" Then
                    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                        Dim dvunit As HtmlGenericControl = CType(e.Item.FindControl("dvunit"), HtmlGenericControl)
                        Dim divtot As HtmlGenericControl = CType(e.Item.FindControl("divtot"), HtmlGenericControl)

                        Dim lnkcumunits As LinkButton = CType(e.Item.FindControl("lnkcumunits"), LinkButton)
                        '  Dim lbtotalValue As LinkButton = CType(e.Item.FindControl("lbtotalValue"), LinkButton)
                        Dim lbltotal As Label = CType(e.Item.FindControl("lbltotal"), Label)
                        Dim lblratebasis As Label = CType(e.Item.FindControl("lblratebasis"), Label)
                        Dim lblunit As Label = CType(e.Item.FindControl("lblunit"), Label)

                        lnkcumunits.Text = lblunit.Text + " Units"

                        If lblratebasis.Text.ToUpper = "UNIT" Then
                            lnkcumunits.Style.Add("display", "block")
                            lbtotalValue.Style.Add("display", "none")
                            lbwltotalValue.Style.Add("display", "none")
                            lbltotal.Style.Add("display", "none")
                        Else
                            lnkcumunits.Style.Add("display", "none")
                            lbtotalValue.Style.Add("display", "none")
                            lbwltotalValue.Style.Add("display", "none")
                            lbltotal.Style.Add("display", "none")
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: dlMADepartureSearchResults_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
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

    Private Sub BindTransitDetails(ByVal dvTransitDetails As DataView, Optional ByVal showmore As String = "")
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLMASearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
                If hdBookingEngineRateType.Value = "1" Then
                    divslideprice.Style.Add("display", "none")
                Else
                    divslideprice.Style.Add("display", "block")
                End If
            End If
        End If
        Dim dt As New DataTable
        dt = dvTransitDetails.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then

            Dim iPageIndex As Integer = 1
            Dim iPageSize As Integer = dt.Rows.Count
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sMAMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sMAMailBoxPageIndex")
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

            ' dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & dt.Rows.Count

            dlMATransitSearchResults.DataSource = dv
            dlMATransitSearchResults.DataBind()

        Else
            dlMATransitSearchResults.DataBind()
        End If


        If Not Session("SelectedMATransit") Is Nothing Then

            dt = Session("SelectedMATransit")

            For Each gvRow As DataListItem In dlMATransitSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")


                Dim lblMAImage As Label = gvRow.FindControl("lblMAImage")
                Dim lblAirportTypeName As Label = gvRow.FindControl("lblAirportTypeName")
                Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                Dim lblminpax1 As Label = gvRow.FindControl("lblminpax1")
                Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax1")
                Dim lblunit As Label = gvRow.FindControl("lblunit")
                Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
                Dim lbladults As Label = gvRow.FindControl("lbladults")
                Dim lblchild As Label = gvRow.FindControl("lblchild")
                Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
                Dim lblprice As Label = gvRow.FindControl("lblprice")
                Dim lblvalue As Label = gvRow.FindControl("lblvalue")

                Dim lblratebasis As Label = gvRow.FindControl("lblratebasis")
                Dim lblairportmadate As Label = gvRow.FindControl("lblairportmadate")
                Dim lbladdlpax As Label = gvRow.FindControl("lbladdlpax")
                Dim lbladultprice As Label = gvRow.FindControl("lbladultprice")
                Dim lblchildprice As Label = gvRow.FindControl("lblchildprice")
                Dim lblunitprice As Label = gvRow.FindControl("lblunitprice")
                Dim lbladdlpaxprice As Label = gvRow.FindControl("lbladdlpaxprice")
                Dim lbladultsalevalue As Label = gvRow.FindControl("lbladultsalevalue")
                Dim lblchildsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                Dim lbladdlpaxsalevalue As Label = gvRow.FindControl("lblchildsalevalue")

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)(0).ToString = hdAirportTypeCode.Value And dt.Rows(i)("Selected").ToString = "1" Then
                        chkbooknow.Checked = True
                    End If
                Next

            Next
        End If


        For Each gvRow As DataListItem In dlMATransitSearchResults.Items

            Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
            Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")
            Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
            Dim lblunit As Label = gvRow.FindControl("lblunit")
            Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
            Dim lbladults As Label = gvRow.FindControl("lbladults")
            Dim lblchild As Label = gvRow.FindControl("lblchild")
            Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
            Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
            Dim lblprice As Label = gvRow.FindControl("lblprice")
            Dim lblvalue As Label = gvRow.FindControl("lblvalue")

            If hdnselected.Value = 1 Then
                chkbooknow.Checked = True

            End If
        Next

    End Sub
    Protected Sub lbTransitShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))


            If myLinkButton.Text = "Show More" Then
                ViewState("TransitShow") = "1"

                BindTransitDetails(dvMaiDetails, "1")
                myLinkButton.Text = "Show Less"

            Else
                ViewState("TransitShow") = "0"

                BindTransitDetails(dvMaiDetails, "")
                myLinkButton.Text = "Show More"
                dlMATransitSearchResults.Focus()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportmeetSearch.aspx :: lbTransitShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub lbtotalValue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lbPrice As LinkButton = CType(sender, LinkButton)
            Session("slbMATotalSaleValue") = lbPrice
            Dim dlItem As DataListItem = CType((lbPrice).NamingContainer, DataListItem)
            Dim hdAirportTypeCode As HiddenField = CType(dlItem.FindControl("hdAirportTypeCode"), HiddenField)
            Dim lblAirportTypeName As Label = CType(dlItem.FindControl("lblAirportTypeName"), Label)
            lblTotlaPriceHeading.Text = "ARRIVAL: " & lblAirportTypeName.Text
            Dim dtArrival As DataTable
            If Not Session("sDSMASearchResults") Is Nothing Then
                dtArrival = CType(Session("sDSMASearchResults"), DataSet).Tables(0)
                If dtArrival.Rows.Count > 0 Then

                    hdAirportTypeCodePopup.Value = hdAirportTypeCode.Value
                    hdTypePopup.Value = "ARRIVAL"

                    Dim dr As DataRow = dtArrival.Select("airportmatypecode='" & hdAirportTypeCode.Value & "'").First
                    hdRateBasisPopup.Value = dr("ratebasis").ToString
                    If dr("ratebasis").ToString = "Adult/Child" Then
                        dvACS.Visible = True
                        dvUnits.Visible = False

                        txtNoOfAdult.Text = dr("adults").ToString
                        txtAdultPrice.Text = dr("adultprice").ToString
                        txtAdultSaleValue.Text = dr("adultsalevalue").ToString

                        txtNoOfchild.Text = dr("child").ToString
                        txtChildprice.Text = dr("childprice").ToString
                        txtchildSaleValue.Text = dr("childsalevalue").ToString
                        txtTotalSaleVale.Text = dr("totalsalevalue").ToString

                        chkComplimentaryToCustomer.Checked = IIf(dr("complimentarycust").ToString = "1", True, False)

                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        txtwlAdultPrice.Text = Math.Round(dwlAdultprice)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        txtwlChildprice.Text = Math.Round(dwlChildtprice)


                        Dim dAdultSaleValue As Decimal = IIf(txtAdultSaleValue.Text = "", "0.00", txtAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)
                        txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(txtchildSaleValue.Text = "", "0.00", txtchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        ' dwlChildSaleValue = dChildSaleValue * dWlMarkup
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text)
                        txtwlchildSaleValue.Text = Math.Round(dwlChildSaleValue)

                        txtwlTotalSaleVale.Text = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text))

                        txtNoOfUnits.Text = ""
                        txtUnitPrice.Text = ""
                        txtUnitSaleValue.Text = ""

                        txtAdditionalPax.Text = ""
                        txtAdditionalPaxPrice.Text = ""
                        txtAdditionalPaxValue.Text = ""

                        txtwlUnitPrice.Text = ""
                        txtwlUnitSaleValue.Text = ""

                        txtwlAdditionalPax.Text = ""
                        txtwlAdditionalPaxPrice.Text = ""
                        txtwlAdditionalPaxValue.Text = ""

                        txtNoOfAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtNoOfchild.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")

                        If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then

                            txtAdultPrice.Style.Add("display", "none")
                            txtAdultSaleValue.Style.Add("display", "none")
                            txtChildprice.Style.Add("display", "none")
                            txtchildSaleValue.Style.Add("display", "none")
                            txtTotalSaleVale.Style.Add("display", "none")
                        

                            txtwlAdultPrice.Style.Add("display", "block")
                            txtwlChildprice.Style.Add("display", "block")
                            txtwlAdultSaleValue.Style.Add("display", "block")
                            txtwlchildSaleValue.Style.Add("display", "block")
                            txtwlTotalSaleVale.Style.Add("display", "block")

                        Else
                            txtAdultPrice.Style.Add("display", "block")
                            txtChildprice.Style.Add("display", "block")
                            txtTotalSaleVale.Style.Add("display", "block")
                            txtAdultSaleValue.Style.Add("display", "block")
                            txtAdultSaleValue.Style.Add("display", "block")

                            txtwlAdultPrice.Style.Add("display", "none")
                            txtwlChildprice.Style.Add("display", "none")
                            txtwlAdultSaleValue.Style.Add("display", "none")
                            txtwlchildSaleValue.Style.Add("display", "none")
                            txtwlTotalSaleVale.Style.Add("display", "none")

                        End If

                    Else
                        dvACS.Visible = False
                        dvUnits.Visible = True

                        txtNoOfAdult.Text = ""
                        txtAdultPrice.Text = ""
                        txtAdultSaleValue.Text = ""

                        txtNoOfchild.Text = ""
                        txtChildprice.Text = ""
                        txtchildSaleValue.Text = ""


                        txtwlAdultPrice.Text = ""
                        txtwlAdultSaleValue.Text = ""


                        txtwlChildprice.Text = ""
                        txtwlchildSaleValue.Text = ""


                        txtTotalSaleVale.Text = dr("totalsalevalue").ToString

                        txtNoOfUnits.Text = dr("units").ToString
                        txtUnitPrice.Text = dr("unitprice").ToString
                        txtUnitSaleValue.Text = dr("unitsalevalue").ToString

                        txtAdditionalPax.Text = dr("addlpax").ToString
                        txtAdditionalPaxPrice.Text = dr("addlpaxprice").ToString
                        txtAdditionalPaxValue.Text = dr("addlpaxsalevalue").ToString

                        chkComplimentaryToCustomer.Checked = IIf(dr("complimentarycust").ToString = "1", True, False)

                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        txtwlUnitPrice.Text = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(txtUnitSaleValue.Text = "", "0.00", txtUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(txtNoOfUnits.Text)
                        txtwlUnitSaleValue.Text = Math.Round(dwlUnitSaleValue)

                        Dim dwlAdditionalPaxPrice As Decimal
                        Dim dAdditionalPaxPrice As Decimal = IIf(txtAdditionalPaxPrice.Text = "", "0.00", txtAdditionalPaxPrice.Text)
                        dwlAdditionalPaxPrice = dAdditionalPaxPrice * dWlMarkup
                        txtwlAdditionalPaxPrice.Text = Math.Round(dwlAdditionalPaxPrice)

                        Dim dAdditionalPaxValue As Decimal = IIf(txtAdditionalPaxValue.Text = "", "0.00", txtAdditionalPaxValue.Text)
                        Dim dwlAdditionalPaxValue As Decimal
                        dwlAdditionalPaxValue = (Math.Round(dAdditionalPaxValue * dWlMarkup)) * Val(txtAdditionalPax.Text)
                        txtwlAdditionalPaxValue.Text = Math.Round(dwlAdditionalPaxValue)

                        txtwlTotalSaleVale.Text = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(txtNoOfUnits.Text)) + Math.Round((Math.Round(dAdditionalPaxPrice * dWlMarkup, 2)) * Val(txtAdditionalPax.Text))


                        txtNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdditionalPax.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdditionalPaxPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")

                        If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                            txtUnitPrice.Style.Add("display", "none")
                            txtAdditionalPaxPrice.Style.Add("display", "none")
                            txtUnitSaleValue.Style.Add("display", "none")
                            txtAdditionalPaxValue.Style.Add("display", "none")
                            txtTotalSaleVale.Style.Add("display", "none")

                            txtwlTotalSaleVale.Style.Add("display", "block")
                            txtwlUnitPrice.Style.Add("display", "block")
                            txtwlAdditionalPaxPrice.Style.Add("display", "block")
                            txtwlUnitSaleValue.Style.Add("display", "block")
                            txtwlAdditionalPaxValue.Style.Add("display", "block")

                        Else

                            txtTotalSaleVale.Style.Add("display", "block")
                            txtUnitPrice.Style.Add("display", "block")
                            txtAdditionalPaxPrice.Style.Add("display", "block")
                            txtUnitSaleValue.Style.Add("display", "block")
                            txtAdditionalPaxValue.Style.Add("display", "block")

                            txtwlTotalSaleVale.Style.Add("display", "none")
                            txtwlUnitPrice.Style.Add("display", "none")
                            txtwlAdditionalPaxPrice.Style.Add("display", "none")
                            txtwlUnitSaleValue.Style.Add("display", "none")
                            txtwlAdditionalPaxValue.Style.Add("display", "none")
                        End If

                    End If


                End If
            End If

            If Session("sLoginType") = "RO" Then

                If chkMAoverride.Checked = True Then
                    txtNoOfAdult.ReadOnly = False
                    txtAdultPrice.ReadOnly = False
                    txtNoOfchild.ReadOnly = False
                    txtChildprice.ReadOnly = False
                    txtUnitPrice.ReadOnly = False
                    txtNoOfUnits.ReadOnly = False
                    txtAdditionalPax.ReadOnly = False
                    txtAdditionalPaxPrice.ReadOnly = False
                    dvPriceBreakupSave.Visible = True

                Else
                    txtNoOfAdult.ReadOnly = True
                    txtAdultPrice.ReadOnly = True
                    txtNoOfchild.ReadOnly = True
                    txtChildprice.ReadOnly = True
                    txtUnitPrice.ReadOnly = True
                    ' txtNoOfUnits.ReadOnly = True
                    'txtAdditionalPax.ReadOnly = True
                    txtAdditionalPaxPrice.ReadOnly = True
                    dvPriceBreakupSave.Visible = True



                End If

            Else
                dvPriceBreakupSave.Visible = True
                dvComplimentaryToCustomer.Visible = False

                txtNoOfAdult.ReadOnly = True
                txtAdultPrice.ReadOnly = True
                txtNoOfchild.ReadOnly = True
                txtChildprice.ReadOnly = True
                txtUnitPrice.ReadOnly = True
                txtNoOfUnits.ReadOnly = False
                txtAdditionalPax.ReadOnly = False
                txtAdditionalPaxPrice.ReadOnly = True

            End If

            If hdBookingEngineRateType.Value = "1" Then
                dvPriceBreakupSave.Visible = True
                dvComplimentaryToCustomer.Visible = False
                txtNoOfUnits.ReadOnly = False
                dvnoUnits.Style.Add("display", "none")
                dvsalevalue.Style.Add("display", "none")
                dvtotalsalevalue.Style.Add("display", "none")
                dvaddpaxvalue.Style.Add("display", "none")
                dvaddpax.Style.Add("display", "none")
                dvaddnopax.Style.Add("display", "none")
            End If

       

            mpTotalprice.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: lbtotalValue_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbtotalValue_Click1(ByVal sender As Object, ByVal e As System.EventArgs)

        Try



            Dim lbPrice As LinkButton = CType(sender, LinkButton)
            Session("slbMATotalSaleValue") = lbPrice
            Dim dlItem As DataListItem = CType((lbPrice).NamingContainer, DataListItem)
            Dim hdAirportTypeCode As HiddenField = CType(dlItem.FindControl("hdAirportTypeCode"), HiddenField)
            Dim lblAirportTypeName As Label = CType(dlItem.FindControl("lblAirportTypeName"), Label)
            lblTotlaPriceHeading.Text = "DEPARTURE: " & lblAirportTypeName.Text
            Dim dtDeparture As DataTable
            If Not Session("sDSMASearchResults") Is Nothing Then
                dtDeparture = CType(Session("sDSMASearchResults"), DataSet).Tables(1)
                If dtDeparture.Rows.Count > 0 Then

                    hdAirportTypeCodePopup.Value = hdAirportTypeCode.Value
                    hdTypePopup.Value = "DEPARTURE"

                    Dim dr As DataRow = dtDeparture.Select("airportmatypecode='" & hdAirportTypeCode.Value & "'").First
                    hdRateBasisPopup.Value = dr("ratebasis").ToString
                    If dr("ratebasis").ToString = "Adult/Child" Then
                        dvACS.Visible = True
                        dvUnits.Visible = False

                        txtNoOfAdult.Text = dr("adults").ToString
                        txtAdultPrice.Text = dr("adultprice").ToString
                        txtAdultSaleValue.Text = dr("adultsalevalue").ToString

                        txtNoOfchild.Text = dr("child").ToString
                        txtChildprice.Text = dr("childprice").ToString
                        txtchildSaleValue.Text = dr("childsalevalue").ToString

                        txtTotalSaleVale.Text = dr("totalsalevalue").ToString

                        txtNoOfUnits.Text = ""
                        txtUnitPrice.Text = ""
                        txtUnitSaleValue.Text = ""

                        txtAdditionalPax.Text = ""
                        txtAdditionalPaxPrice.Text = ""
                        txtAdditionalPaxValue.Text = ""

                        txtwlUnitPrice.Text = ""
                        txtwlUnitSaleValue.Text = ""

                        txtwlAdditionalPax.Text = ""
                        txtwlAdditionalPaxPrice.Text = ""
                        txtwlAdditionalPaxValue.Text = ""

                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        txtwlAdultPrice.Text = Math.Round(dwlAdultprice, 2)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        txtwlChildprice.Text = Math.Round(dwlChildtprice, 2)


                        Dim dAdultSaleValue As Decimal = IIf(txtAdultSaleValue.Text = "", "0.00", txtAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)
                        txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(txtchildSaleValue.Text = "", "0.00", txtchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        ' dwlChildSaleValue = dChildSaleValue * dWlMarkup
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text)
                        txtwlchildSaleValue.Text = Math.Round(dwlChildSaleValue)

                        txtwlTotalSaleVale.Text = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text))


                        txtNoOfAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtNoOfchild.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")

                    Else
                        dvACS.Visible = False
                        dvUnits.Visible = True

                        txtNoOfAdult.Text = ""
                        txtAdultPrice.Text = ""
                        txtAdultSaleValue.Text = ""

                        txtNoOfchild.Text = ""
                        txtChildprice.Text = ""
                        txtchildSaleValue.Text = ""



                        txtwlAdultPrice.Text = ""
                        txtwlAdultSaleValue.Text = ""
                        txtwlChildprice.Text = ""
                        txtwlchildSaleValue.Text = ""

                        txtTotalSaleVale.Text = dr("totalsalevalue").ToString

                        txtNoOfUnits.Text = dr("units").ToString
                        txtUnitPrice.Text = dr("unitprice").ToString
                        txtUnitSaleValue.Text = dr("unitsalevalue").ToString

                        txtAdditionalPax.Text = dr("addlpax").ToString
                        txtAdditionalPaxPrice.Text = dr("addlpaxprice").ToString
                        txtAdditionalPaxValue.Text = dr("addlpaxsalevalue").ToString


                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        txtwlUnitPrice.Text = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(txtUnitSaleValue.Text = "", "0.00", txtUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(txtNoOfUnits.Text)
                        txtwlUnitSaleValue.Text = Math.Round(dwlUnitSaleValue)

                        Dim dwlAdditionalPaxPrice As Decimal
                        Dim dAdditionalPaxPrice As Decimal = IIf(txtAdditionalPaxPrice.Text = "", "0.00", txtAdditionalPaxPrice.Text)
                        dwlAdditionalPaxPrice = dAdditionalPaxPrice * dWlMarkup
                        txtwlAdditionalPaxPrice.Text = Math.Round(dwlAdditionalPaxPrice)

                        Dim dAdditionalPaxValue As Decimal = IIf(txtAdditionalPaxValue.Text = "", "0.00", txtAdditionalPaxValue.Text)
                        Dim dwlAdditionalPaxValue As Decimal
                        dwlAdditionalPaxValue = (Math.Round(dAdditionalPaxValue * dWlMarkup)) * Val(txtAdditionalPax.Text)
                        txtwlAdditionalPaxValue.Text = Math.Round(dwlAdditionalPaxValue)

                        txtwlTotalSaleVale.Text = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(txtNoOfUnits.Text)) + Math.Round((Math.Round(dAdditionalPaxPrice * dWlMarkup, 2)) * Val(txtAdditionalPax.Text))



                        txtNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdditionalPax.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdditionalPaxPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")

                    End If
                End If
            End If

            If Session("sLoginType") = "RO" Then

                If chkMAoverride.Checked = True Then
                    dvPriceBreakupSave.Visible = True
                    txtNoOfAdult.ReadOnly = False
                    txtAdultPrice.ReadOnly = False
                    txtNoOfchild.ReadOnly = False
                    txtChildprice.ReadOnly = False
                    txtUnitPrice.ReadOnly = False
                    txtNoOfUnits.ReadOnly = False
                    txtAdditionalPax.ReadOnly = False
                    txtAdditionalPaxPrice.ReadOnly = False

                Else
                    txtNoOfAdult.ReadOnly = True
                    txtAdultPrice.ReadOnly = True
                    txtNoOfchild.ReadOnly = True
                    txtChildprice.ReadOnly = True
                    txtUnitPrice.ReadOnly = True
                    txtNoOfUnits.ReadOnly = False
                    txtAdditionalPax.ReadOnly = False
                    txtAdditionalPaxPrice.ReadOnly = True
                    dvPriceBreakupSave.Visible = True

                End If

            Else
                dvPriceBreakupSave.Visible = True
                dvComplimentaryToCustomer.Visible = False
                txtNoOfAdult.ReadOnly = True
                txtAdultPrice.ReadOnly = True
                txtNoOfchild.ReadOnly = True
                txtChildprice.ReadOnly = True
                txtUnitPrice.ReadOnly = True
                '   txtNoOfUnits.ReadOnly = True
                '   txtAdditionalPax.ReadOnly = True
                txtAdditionalPaxPrice.ReadOnly = True
            End If

            If hdBookingEngineRateType.Value = "1" Then
                dvPriceBreakupSave.Visible = True
                dvComplimentaryToCustomer.Visible = False
                txtNoOfUnits.ReadOnly = False
                dvnoUnits.Style.Add("display", "none")
                dvsalevalue.Style.Add("display", "none")
                dvtotalsalevalue.Style.Add("display", "none")
                dvaddpaxvalue.Style.Add("display", "none")
                dvaddpax.Style.Add("display", "none")
                dvaddnopax.Style.Add("display", "none")
            End If


            mpTotalprice.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: lbtotalValue_Click1 :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbtotalValue_Click2(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lbPrice As LinkButton = CType(sender, LinkButton)
            Session("slbMATotalSaleValue") = lbPrice
            Dim dlItem As DataListItem = CType((lbPrice).NamingContainer, DataListItem)
            Dim hdAirportTypeCode As HiddenField = CType(dlItem.FindControl("hdAirportTypeCode"), HiddenField)
            Dim lblAirportTypeName As Label = CType(dlItem.FindControl("lblAirportTypeName"), Label)
            lblTotlaPriceHeading.Text = "TRANSIT: " & lblAirportTypeName.Text
            Dim dtTransit As DataTable
            If Not Session("sDSMASearchResults") Is Nothing Then
                dtTransit = CType(Session("sDSMASearchResults"), DataSet).Tables(2)
                If dtTransit.Rows.Count > 0 Then

                    hdAirportTypeCodePopup.Value = hdAirportTypeCode.Value
                    hdTypePopup.Value = "TRANSIT"

                    Dim dr As DataRow = dtTransit.Select("airportmatypecode='" & hdAirportTypeCode.Value & "'").First
                    hdRateBasisPopup.Value = dr("ratebasis").ToString
                    If dr("ratebasis").ToString = "Adult/Child" Then
                        dvACS.Visible = True
                        dvUnits.Visible = False

                        txtNoOfAdult.Text = dr("adults").ToString
                        txtAdultPrice.Text = dr("adultprice").ToString
                        txtAdultSaleValue.Text = dr("adultsalevalue").ToString

                        txtNoOfchild.Text = dr("child").ToString
                        txtChildprice.Text = dr("childprice").ToString
                        txtchildSaleValue.Text = dr("childsalevalue").ToString

                        txtTotalSaleVale.Text = dr("totalsalevalue").ToString

                        txtNoOfUnits.Text = ""
                        txtUnitPrice.Text = ""
                        txtUnitSaleValue.Text = ""

                        txtAdditionalPax.Text = ""
                        txtAdditionalPaxPrice.Text = ""
                        txtAdditionalPaxValue.Text = ""

                        txtwlUnitPrice.Text = ""
                        txtwlUnitSaleValue.Text = ""

                        txtwlAdditionalPax.Text = ""
                        txtwlAdditionalPaxPrice.Text = ""
                        txtwlAdditionalPaxValue.Text = ""

                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        txtwlAdultPrice.Text = Math.Round(dwlAdultprice, 2)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        txtwlChildprice.Text = Math.Round(dwlChildtprice, 2)


                        Dim dAdultSaleValue As Decimal = IIf(txtAdultSaleValue.Text = "", "0.00", txtAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)
                        txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(txtchildSaleValue.Text = "", "0.00", txtchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        ' dwlChildSaleValue = dChildSaleValue * dWlMarkup
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text)
                        txtwlchildSaleValue.Text = Math.Round(dwlChildSaleValue)

                        txtwlTotalSaleVale.Text = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text))


                        txtNoOfAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtNoOfchild.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(txtAdultSaleValue.ClientID, String) + "','" + CType(txtNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(txtchildSaleValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")

                    Else
                        dvACS.Visible = False
                        dvUnits.Visible = True

                        txtNoOfAdult.Text = ""
                        txtAdultPrice.Text = ""
                        txtAdultSaleValue.Text = ""

                        txtNoOfchild.Text = ""
                        txtChildprice.Text = ""
                        txtchildSaleValue.Text = ""


                        txtwlAdultPrice.Text = ""
                        txtwlAdultSaleValue.Text = ""
                        txtwlChildprice.Text = ""
                        txtwlchildSaleValue.Text = ""

                        txtTotalSaleVale.Text = dr("totalsalevalue").ToString

                        txtNoOfUnits.Text = dr("units").ToString
                        txtUnitPrice.Text = dr("unitprice").ToString
                        txtUnitSaleValue.Text = dr("unitsalevalue").ToString

                        txtAdditionalPax.Text = dr("addlpax").ToString
                        txtAdditionalPaxPrice.Text = dr("addlpaxprice").ToString
                        txtAdditionalPaxValue.Text = dr("addlpaxsalevalue").ToString


                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        txtwlUnitPrice.Text = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(txtUnitSaleValue.Text = "", "0.00", txtUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(txtNoOfUnits.Text)
                        txtwlUnitSaleValue.Text = Math.Round(dwlUnitSaleValue)

                        Dim dwlAdditionalPaxPrice As Decimal
                        Dim dAdditionalPaxPrice As Decimal = IIf(txtAdditionalPaxPrice.Text = "", "0.00", txtAdditionalPaxPrice.Text)
                        dwlAdditionalPaxPrice = dAdditionalPaxPrice * dWlMarkup
                        txtwlAdditionalPaxPrice.Text = Math.Round(dwlAdditionalPaxPrice)

                        Dim dAdditionalPaxValue As Decimal = IIf(txtAdditionalPaxValue.Text = "", "0.00", txtAdditionalPaxValue.Text)
                        Dim dwlAdditionalPaxValue As Decimal
                        dwlAdditionalPaxValue = (Math.Round(dAdditionalPaxValue * dWlMarkup)) * Val(txtAdditionalPax.Text)
                        txtwlAdditionalPaxValue.Text = Math.Round(dwlAdditionalPaxValue)

                        txtwlTotalSaleVale.Text = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(txtNoOfUnits.Text)) + Math.Round((Math.Round(dAdditionalPaxPrice * dWlMarkup, 2)) * Val(txtAdditionalPax.Text))



                        txtNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdditionalPax.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                        txtAdditionalPaxPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(txtNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(txtUnitSaleValue.ClientID, String) + "','" + CType(txtAdditionalPax.ClientID, String) + "', '" + CType(txtAdditionalPaxPrice.ClientID, String) + "' ,'" + CType(txtAdditionalPaxValue.ClientID, String) + "','" + CType(txtTotalSaleVale.ClientID, String) + "')")
                    End If
                End If
            End If
            If Session("sLoginType") = "RO" Then

                If chkMAoverride.Checked = True Then
                    dvPriceBreakupSave.Visible = True
                    txtNoOfAdult.ReadOnly = False
                    txtAdultPrice.ReadOnly = False
                    txtNoOfchild.ReadOnly = False
                    txtChildprice.ReadOnly = False
                    txtUnitPrice.ReadOnly = False
                    txtNoOfUnits.ReadOnly = False
                    txtAdditionalPax.ReadOnly = False
                    txtAdditionalPaxPrice.ReadOnly = False

                Else
                    txtNoOfAdult.ReadOnly = True
                    txtAdultPrice.ReadOnly = True
                    txtNoOfchild.ReadOnly = True
                    txtChildprice.ReadOnly = True
                    txtUnitPrice.ReadOnly = True
                    txtNoOfUnits.ReadOnly = False
                    txtAdditionalPax.ReadOnly = False
                    txtAdditionalPaxPrice.ReadOnly = True
                    dvPriceBreakupSave.Visible = True

                End If

            Else
                dvPriceBreakupSave.Visible = True
                dvComplimentaryToCustomer.Visible = False
                txtNoOfAdult.ReadOnly = True
                txtAdultPrice.ReadOnly = True
                txtNoOfchild.ReadOnly = True
                txtChildprice.ReadOnly = True
                txtUnitPrice.ReadOnly = True
                txtNoOfUnits.ReadOnly = False
                txtAdditionalPax.ReadOnly = False
                txtAdditionalPaxPrice.ReadOnly = True
            End If

            If hdBookingEngineRateType.Value = "1" Then
                dvPriceBreakupSave.Visible = True
                dvComplimentaryToCustomer.Visible = False
                txtNoOfUnits.ReadOnly = False
                dvnoUnits.Style.Add("display", "none")
                dvsalevalue.Style.Add("display", "none")
                dvtotalsalevalue.Style.Add("display", "none")
                dvaddpaxvalue.Style.Add("display", "none")
                dvaddpax.Style.Add("display", "none")
                dvaddnopax.Style.Add("display", "none")
            End If


            mpTotalprice.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: lbtotalValue_Click2 :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnPriceBreakupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupSave.Click
        Try

            Dim ds As DataSet
            ds = Session("sDSMASearchResults")

            If hdTypePopup.Value = "ARRIVAL" Then
                If hdRateBasisPopup.Value = "Adult/Child" Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(0).Select("airportmatypecode='" & hdAirportTypeCodePopup.Value & "'").First
                        dr("totalsalevalue") = txtTotalSaleVale.Text
                        dr("adults") = txtNoOfAdult.Text
                        dr("adultprice") = txtAdultPrice.Text
                        dr("adultsalevalue") = txtAdultSaleValue.Text
                        dr("child") = txtNoOfchild.Text
                        dr("childprice") = txtChildprice.Text
                        dr("childsalevalue") = txtchildSaleValue.Text
                        dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)

                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        dr("wladultprice") = Math.Round(dwlAdultprice, 2)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        dr("wlchildprice") = Math.Round(dwlChildtprice, 2)


                        Dim dAdultSaleValue As Decimal = IIf(txtAdultSaleValue.Text = "", "0.00", txtAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)
                        dr("wladultsalevalue") = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(txtchildSaleValue.Text = "", "0.00", txtchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text)
                        dr("wlchildsalevalue") = Math.Round(dwlChildSaleValue)

                        dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text))


                    End If
                Else
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(0).Select("airportmatypecode='" & hdAirportTypeCodePopup.Value & "'").First
                        dr("totalsalevalue") = txtTotalSaleVale.Text
                        dr("units") = txtNoOfUnits.Text
                        dr("unitprice") = txtUnitPrice.Text
                        dr("unitsalevalue") = txtUnitSaleValue.Text
                        dr("addlpax") = txtAdditionalPax.Text
                        dr("addlpaxprice") = txtAdditionalPaxPrice.Text
                        dr("addlpaxsalevalue") = txtAdditionalPaxValue.Text
                        dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        dr("wlunitprice") = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(txtUnitSaleValue.Text = "", "0.00", txtUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(txtNoOfUnits.Text)
                        dr("wlunitsalevalue") = Math.Round(dwlUnitSaleValue)

                        Dim dwlAdditionalPaxPrice As Decimal
                        Dim dAdditionalPaxPrice As Decimal = IIf(txtAdditionalPaxPrice.Text = "", "0.00", txtAdditionalPaxPrice.Text)
                        dwlAdditionalPaxPrice = dAdditionalPaxPrice * dWlMarkup
                        dr("wladdlpaxprice") = Math.Round(dwlAdditionalPaxPrice)

                        Dim dAdditionalPaxValue As Decimal = IIf(txtAdditionalPaxValue.Text = "", "0.00", txtAdditionalPaxValue.Text)
                        Dim dwlAdditionalPaxValue As Decimal
                        dwlAdditionalPaxValue = (Math.Round(dAdditionalPaxValue * dWlMarkup)) * Val(txtAdditionalPax.Text)
                        dr("wladdlpaxsalevalue") = Math.Round(dwlAdditionalPaxValue)

                        dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(txtNoOfUnits.Text)) + Math.Round((Math.Round(dAdditionalPaxPrice * dWlMarkup, 2)) * Val(txtAdditionalPax.Text))


                    End If
                End If
            ElseIf hdTypePopup.Value = "DEPARTURE" Then
                If hdRateBasisPopup.Value = "Adult/Child" Then
                    If ds.Tables(1).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(1).Select("airportmatypecode='" & hdAirportTypeCodePopup.Value & "'").First
                        dr("totalsalevalue") = txtTotalSaleVale.Text
                        dr("adults") = txtNoOfAdult.Text
                        dr("adultprice") = txtAdultPrice.Text
                        dr("adultsalevalue") = txtAdultSaleValue.Text
                        dr("child") = txtNoOfchild.Text
                        dr("childprice") = txtChildprice.Text
                        dr("childsalevalue") = txtchildSaleValue.Text
                        dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)
                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        dr("wladultprice") = Math.Round(dwlAdultprice, 2)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        dr("wlchildprice") = Math.Round(dwlChildtprice, 2)


                        Dim dAdultSaleValue As Decimal = IIf(txtAdultSaleValue.Text = "", "0.00", txtAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)
                        dr("wladultsalevalue") = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(txtchildSaleValue.Text = "", "0.00", txtchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text)
                        dr("wlchildsalevalue") = Math.Round(dwlChildSaleValue)

                        dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text))

                    End If
                Else
                    If ds.Tables(1).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(1).Select("airportmatypecode='" & hdAirportTypeCodePopup.Value & "'").First
                        dr("totalsalevalue") = txtTotalSaleVale.Text
                        dr("units") = txtNoOfUnits.Text
                        dr("unitprice") = txtUnitPrice.Text
                        dr("unitsalevalue") = txtUnitSaleValue.Text
                        dr("addlpax") = txtAdditionalPax.Text
                        dr("addlpaxprice") = txtAdditionalPaxPrice.Text
                        dr("addlpaxsalevalue") = txtAdditionalPaxValue.Text
                        dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        dr("wlunitprice") = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(txtUnitSaleValue.Text = "", "0.00", txtUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(txtNoOfUnits.Text)
                        dr("wlunitsalevalue") = Math.Round(dwlUnitSaleValue)

                        Dim dwlAdditionalPaxPrice As Decimal
                        Dim dAdditionalPaxPrice As Decimal = IIf(txtAdditionalPaxPrice.Text = "", "0.00", txtAdditionalPaxPrice.Text)
                        dwlAdditionalPaxPrice = dAdditionalPaxPrice * dWlMarkup
                        dr("wladdlpaxprice") = Math.Round(dwlAdditionalPaxPrice)

                        Dim dAdditionalPaxValue As Decimal = IIf(txtAdditionalPaxValue.Text = "", "0.00", txtAdditionalPaxValue.Text)
                        Dim dwlAdditionalPaxValue As Decimal
                        dwlAdditionalPaxValue = (Math.Round(dAdditionalPaxValue * dWlMarkup)) * Val(txtAdditionalPax.Text)
                        dr("wladdlpaxsalevalue") = Math.Round(dwlAdditionalPaxValue)

                        dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(txtNoOfUnits.Text)) + Math.Round((Math.Round(dAdditionalPaxPrice * dWlMarkup, 2)) * Val(txtAdditionalPax.Text))


                    End If
                End If
            ElseIf hdTypePopup.Value = "TRANSIT" Then
                If hdRateBasisPopup.Value = "Adult/Child" Then
                    If ds.Tables(2).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(2).Select("airportmatypecode='" & hdAirportTypeCodePopup.Value & "'").First
                        dr("totalsalevalue") = txtTotalSaleVale.Text
                        dr("adults") = txtNoOfAdult.Text
                        dr("adultprice") = txtAdultPrice.Text
                        dr("adultsalevalue") = txtAdultSaleValue.Text
                        dr("child") = txtNoOfchild.Text
                        dr("childprice") = txtChildprice.Text
                        dr("childsalevalue") = txtchildSaleValue.Text
                        dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)
                        Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
                        Dim dwlAdultprice As Decimal
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
                        dwlAdultprice = dAdultprice * dWlMarkup
                        dr("wladultprice") = Math.Round(dwlAdultprice, 2)

                        Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
                        Dim dwlChildtprice As Decimal
                        dwlChildtprice = dChildprice * dWlMarkup
                        dr("wlchildprice") = Math.Round(dwlChildtprice, 2)


                        Dim dAdultSaleValue As Decimal = IIf(txtAdultSaleValue.Text = "", "0.00", txtAdultSaleValue.Text)
                        Dim dwlAdultSaleValue As Decimal
                        dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)
                        dr("wladultsalevalue") = Math.Round(dwlAdultSaleValue)

                        Dim dChildSaleValue As Decimal = IIf(txtchildSaleValue.Text = "", "0.00", txtchildSaleValue.Text)
                        Dim dwlChildSaleValue As Decimal
                        dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text)
                        dr("wlchildsalevalue") = Math.Round(dwlChildSaleValue)

                        dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(txtNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(txtNoOfchild.Text))

                    End If
                Else
                    If ds.Tables(2).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(2).Select("airportmatypecode='" & hdAirportTypeCodePopup.Value & "'").First
                        dr("totalsalevalue") = txtTotalSaleVale.Text
                        dr("units") = txtNoOfUnits.Text
                        dr("unitprice") = txtUnitPrice.Text
                        dr("unitsalevalue") = txtUnitSaleValue.Text
                        dr("addlpax") = txtAdditionalPax.Text
                        dr("addlpaxprice") = txtAdditionalPaxPrice.Text
                        dr("addlpaxsalevalue") = txtAdditionalPaxValue.Text
                        dr("complimentarycust") = IIf(chkComplimentaryToCustomer.Checked = True, 1, 0)
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))

                        Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
                        Dim dwlUnitPrice As Decimal
                        dwlUnitPrice = dUnitPrice * dWlMarkup
                        dr("wlunitprice") = Math.Round(dwlUnitPrice)

                        Dim dUnitSaleValue As Decimal = IIf(txtUnitSaleValue.Text = "", "0.00", txtUnitSaleValue.Text)
                        Dim dwlUnitSaleValue As Decimal
                        ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
                        dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(txtNoOfUnits.Text)
                        dr("wlunitsalevalue") = Math.Round(dwlUnitSaleValue)

                        Dim dwlAdditionalPaxPrice As Decimal
                        Dim dAdditionalPaxPrice As Decimal = IIf(txtAdditionalPaxPrice.Text = "", "0.00", txtAdditionalPaxPrice.Text)
                        dwlAdditionalPaxPrice = dAdditionalPaxPrice * dWlMarkup
                        dr("wladdlpaxprice") = Math.Round(dwlAdditionalPaxPrice)

                        Dim dAdditionalPaxValue As Decimal = IIf(txtAdditionalPaxValue.Text = "", "0.00", txtAdditionalPaxValue.Text)
                        Dim dwlAdditionalPaxValue As Decimal
                        dwlAdditionalPaxValue = (Math.Round(dAdditionalPaxValue * dWlMarkup)) * Val(txtAdditionalPax.Text)
                        dr("wladdlpaxsalevalue") = Math.Round(dwlAdditionalPaxValue)

                        dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(txtNoOfUnits.Text)) + Math.Round((Math.Round(dAdditionalPaxPrice * dWlMarkup, 2)) * Val(txtAdditionalPax.Text))


                    End If
                End If
            End If



            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbMATotalSaleValue"), LinkButton)

            Dim dlItem As DataListItem = CType((lbTotalPrice).NamingContainer, DataListItem)
            Dim lbtotalValue As LinkButton
            Dim lnkcumunits As LinkButton
            Dim lblunit As Label
            If hdTypePopup.Value = "ARRIVAL" Then
                lbtotalValue = CType(dlMAArrivalSearchResults.Items(dlItem.ItemIndex).FindControl("lbtotalValue"), LinkButton)
                lbtotalValue.Text = txtTotalSaleVale.Text & " " & hdCurrCodePopup.Value
                lnkcumunits = CType(dlMAArrivalSearchResults.Items(dlItem.ItemIndex).FindControl("lnkcumunits"), LinkButton)
                lnkcumunits.Text = txtNoOfUnits.Text + " Units"

                lblunit = CType(dlMAArrivalSearchResults.Items(dlItem.ItemIndex).FindControl("lblunit"), Label)
                lblunit.Text = txtNoOfUnits.Text

            ElseIf hdTypePopup.Value = "DEPARTURE" Then
                lbtotalValue = CType(dlMADepartureSearchResults.Items(dlItem.ItemIndex).FindControl("lbtotalValue"), LinkButton)
                lbtotalValue.Text = txtTotalSaleVale.Text & " " & hdCurrCodePopup.Value

                lnkcumunits = CType(dlMADepartureSearchResults.Items(dlItem.ItemIndex).FindControl("lnkcumunits"), LinkButton)
                lnkcumunits.Text = txtNoOfUnits.Text + " Units"

                lblunit = CType(dlMADepartureSearchResults.Items(dlItem.ItemIndex).FindControl("lblunit"), Label)
                lblunit.Text = txtNoOfUnits.Text

            ElseIf hdTypePopup.Value = "TRANSIT" Then
                lbtotalValue = CType(dlMATransitSearchResults.Items(dlItem.ItemIndex).FindControl("lbtotalValue"), LinkButton)
                lbtotalValue.Text = txtTotalSaleVale.Text & " " & hdCurrCodePopup.Value

                lnkcumunits = CType(dlMATransitSearchResults.Items(dlItem.ItemIndex).FindControl("lnkcumunits"), LinkButton)
                lnkcumunits.Text = txtNoOfUnits.Text + " Units"

                lblunit = CType(dlMATransitSearchResults.Items(dlItem.ItemIndex).FindControl("lblunit"), Label)
                lblunit.Text = txtNoOfUnits.Text

            End If
            Session("sDSMASearchResults") = ds

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: btnPriceBreakupSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub

    Protected Sub dlMATransitSearchResults_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMATransitSearchResults.ItemCreated
        If dlMATransitSearchResults.Items.Count = 1 And e.Item.ItemType = 1 And ViewState("Transitviewmorehide") = 1 Then
            Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbTransitShowMore"), LinkButton)

            myLinkButton.Visible = False
        End If
    End Sub

    Protected Sub dlMATransitSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlMATransitSearchResults.ItemDataBound

        Try

            If e.Item.ItemType = 1 Then
                Dim myarrButton As LinkButton = CType(e.Item.FindControl("lbTransitShowMore"), LinkButton)
             
                If ViewState("TransitShow") = "1" Then
                    myarrButton.Text = "Show Less"
                Else
                    myarrButton.Text = "Show More"
                End If

            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim imgHotelImage As Image = CType(e.Item.FindControl("imgMAImage"), Image)
                Dim lblMAImage As Label = CType(e.Item.FindControl("lblMAImage"), Label)
                imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblMAImage.Text & "&type=3"
                Dim lblremarks As Label = CType(e.Item.FindControl("lblremarks"), Label)
                lblremarks.Text = lblremarks.Text.Replace(Environment.NewLine, "<br/>")

                Dim lblminpax As Label = CType(e.Item.FindControl("lblminpax"), Label)
                Dim lblmaxpax As Label = CType(e.Item.FindControl("lblmaxpax"), Label)
                Dim dvTranMin As HtmlGenericControl = CType(e.Item.FindControl("dvTranMin"), HtmlGenericControl)
                Dim dvTranMax As HtmlGenericControl = CType(e.Item.FindControl("dvTranMax"), HtmlGenericControl)
                If lblminpax.Text.Trim = "0 PAX" Then
                    dvTranMin.Visible = False
                End If
                If lblmaxpax.Text.Trim = "0 PAX" Then
                    dvTranMax.Visible = False
                End If

                Dim chkbooknow As CheckBox = CType(e.Item.FindControl("chkbooknow"), CheckBox)
                chkbooknow.Attributes.Add("onChange", "javascript:calculatevaluetransit('" + CType(chkbooknow.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "' )")

                Dim lbtotalValue As LinkButton = CType(e.Item.FindControl("lbtotalValue"), LinkButton)
                Dim lbwltotalValue As LinkButton = CType(e.Item.FindControl("lbwltotalValue"), LinkButton)
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    lbwltotalValue.Visible = True
                    lbtotalValue.Visible = False
                    Dim lblTotalCurrcode As Label = CType(e.Item.FindControl("lblTotalCurrcode"), Label)
                    Dim lblwlcurrcode As Label = CType(e.Item.FindControl("lblwlcurrcode"), Label)
                    lblTotalCurrcode.Text = lblwlcurrcode.Text

                    Dim dTotalValue As Double = IIf(lbwltotalValue.Text = "", 0, lbwltotalValue.Text)
                    lbwltotalValue.Text = Math.Round(dTotalValue)
                   
                Else
                    Dim dTotalValue As Double = IIf(lbtotalValue.Text = "", 0, lbtotalValue.Text)
                    lbtotalValue.Text = Math.Round(dTotalValue)
                    lbwltotalValue.Visible = False
                    lbtotalValue.Visible = True
                End If

                If hdBookingEngineRateType.Value = "1" Then
                    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                        Dim dvunit As HtmlGenericControl = CType(e.Item.FindControl("dvunit"), HtmlGenericControl)
                        Dim divtot As HtmlGenericControl = CType(e.Item.FindControl("divtot"), HtmlGenericControl)

                        Dim lnkcumunits As LinkButton = CType(e.Item.FindControl("lnkcumunits"), LinkButton)
                        ' Dim lbtotalValue As LinkButton = CType(e.Item.FindControl("lbtotalValue"), LinkButton)
                        Dim lbltotal As Label = CType(e.Item.FindControl("lbltotal"), Label)
                        Dim lblratebasis As Label = CType(e.Item.FindControl("lblratebasis"), Label)

                        Dim lblunit As Label = CType(e.Item.FindControl("lblunit"), Label)

                        lnkcumunits.Text = lblunit.Text + " Units"

                        If lblratebasis.Text.ToUpper = "UNIT" Then
                            lnkcumunits.Style.Add("display", "block")
                            lbtotalValue.Style.Add("display", "none")
                            lbwltotalValue.Style.Add("display", "none")
                            lbltotal.Style.Add("display", "none")
                        Else
                            lnkcumunits.Style.Add("display", "none")
                            lbtotalValue.Style.Add("display", "none")
                            lbwltotalValue.Style.Add("display", "none")
                            lbltotal.Style.Add("display", "none")
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetSearch.aspx :: dlMATransitSearchResults_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub LoadRoomAdultChild()
        'Dim strRequestId As String = ""
        'If Not Session("sRequestId") Is Nothing Then
        '    strRequestId = Session("sRequestId")
        '    Dim dtDetails As DataTable
        '    Dim objBLLCommonFuntions As New BLLCommonFuntions
        '    dtDetails = objBLLCommonFuntions.GetRoomAdultAndChildDetails(strRequestId)
        '    If dtDetails.Rows.Count > 0 Then
        '        FillSpecifiedAdultChild(dtDetails.Rows(0)("adults").ToString, dtDetails.Rows(0)("child").ToString)

        '        If dtDetails.Rows(0)("child").ToString > 0 Then
        '            ''' Added 01/06/17 shahul
        '            Dim childages As String = dtDetails.Rows(0)("childages").ToString.Replace(",", ";")
        '            ''''
        '            'FillSpecifiedChildAges(dtDetails.Rows(0)("childages").ToString)
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

    Private Sub FillSpecifiedChildAges(ByVal childages As String)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlMAChild8, childages)

    End Sub

    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlMAAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlMAChild, child)
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
    Private Sub BindMAChildAge()

        Dim objBLLCommonFuntions = New BLLCommonFuntions
        Dim strRequestId As String = ""
        Dim dtpax As DataTable
        Dim strQuery As String = ""
        If Not Session("sRequestId") Is Nothing Then
            strRequestId = Session("sRequestId")
            Dim dt As DataTable
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(strRequestId)
            If dt.Rows.Count > 0 Then

                txtMACustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtMACustomer.Text = dt.Rows(0)("agentname").ToString
                txtMASourcecountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtMASourcecountrycode.Text = dt.Rows(0)("sourcectrycode").ToString

                ''' To Fill Adult  & Child

                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & strRequestId & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 Then
                    ddlMAAdult.SelectedValue = dtpax.Rows(0)("adults").ToString
                    ddlMAChild.SelectedValue = dtpax.Rows(0)("child").ToString

                    If dtpax.Rows(0)("child").ToString <> "0" Then

                        ''' Added 01/06/17 shahul
                        Dim childages As String = dtpax.Rows(0)("childages").ToString.Replace(",", ";")
                        If Left(childages, 1) = ";" Then
                            childages = Right(childages, (childages.Length - 1))
                        End If
                        Dim strChildAges As String() = childages.ToString.Split(";")
                        ''''''''''


                        ' Dim strChildAges As String() = dt.Rows(0)("childages").ToString.Split(";")
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 1 Then
                            txtMAChild1.Text = strChildAges(0)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 2 Then
                            txtMAChild2.Text = strChildAges(1)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 3 Then
                            txtMAChild3.Text = strChildAges(2)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 4 Then
                            txtMAChild4.Text = strChildAges(3)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 5 Then
                            txtMAChild5.Text = strChildAges(4)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 6 Then
                            txtMAChild6.Text = strChildAges(5)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 7 Then
                            txtMAChild7.Text = strChildAges(6)
                        End If
                        If CType(dtpax.Rows(0)("child").ToString, Integer) >= 8 Then
                            txtMAChild8.Text = strChildAges(7)
                        End If


                        If dt.Rows(0)("reqoverride").ToString = "1" Then
                            chkMAoverride.Checked = True
                        Else
                            chkMAoverride.Checked = False
                        End If

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
            objclsUtilities.WriteErrorLog("Airportmeet.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnSelectedArrival_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedArrival.Click
        Dim dt As New DataTable
        Dim dr As DataRow
        If dlMAArrivalSearchResults.Items.Count > 1 Then
            Session("SelectedMAArrival") = Nothing


            dt.Columns.Add("airportmatypecode", GetType(String))
            dt.Columns.Add("airportmatypename", GetType(String))
            dt.Columns.Add("imagename", GetType(String))
            dt.Columns.Add("remarks", GetType(String))
            dt.Columns.Add("Selected", GetType(String))
            dt.Columns.Add("minpax", GetType(Integer))
            dt.Columns.Add("maxpax", GetType(String))
            dt.Columns.Add("ratebasis", GetType(String))
            dt.Columns.Add("airportmadate", GetType(String))
            dt.Columns.Add("adults", GetType(String))
            dt.Columns.Add("child", GetType(String))
            dt.Columns.Add("childagestring", GetType(String))
            dt.Columns.Add("units", GetType(String))
            dt.Columns.Add("addlpax", GetType(String))
            dt.Columns.Add("adultprice", GetType(String))
            dt.Columns.Add("childprice", GetType(String))
            dt.Columns.Add("unitprice", GetType(String))
            dt.Columns.Add("addlpaxprice", GetType(String))
            dt.Columns.Add("adultsalevalue", GetType(String))
            dt.Columns.Add("childsalevalue", GetType(String))
            dt.Columns.Add("unitsalevalue", GetType(String))
            dt.Columns.Add("addlpaxsalevalue", GetType(String))
            dt.Columns.Add("totalsalevalue", GetType(String))
            dt.Columns.Add("AdultChildText", GetType(String))
            dt.Columns.Add("adultplistcode", GetType(String))
            dt.Columns.Add("childplistcode", GetType(String))
            dt.Columns.Add("unitplistcode", GetType(String))
            dt.Columns.Add("addlpaxplistcode", GetType(String))
            dt.Columns.Add("currentselection", GetType(String))
            dt.Columns.Add("cumulativeunits", GetType(String))

            dt.Columns.Add("preferredsupplier", GetType(String))
            dt.Columns.Add("adultcprice", GetType(String))
            dt.Columns.Add("childcprice", GetType(String))
            dt.Columns.Add("unitcprice", GetType(String))
            dt.Columns.Add("addlpaxcprice", GetType(String))
            dt.Columns.Add("adultcostvalue", GetType(String))
            dt.Columns.Add("childcostvalue", GetType(String))
            dt.Columns.Add("unitcostvalue", GetType(String))
            dt.Columns.Add("addlpaxcostvalue", GetType(String))
            dt.Columns.Add("totalcostvalue", GetType(String))
            dt.Columns.Add("adultcplistcode", GetType(String))
            dt.Columns.Add("childcplistcode", GetType(String))
            dt.Columns.Add("unitcplistcode", GetType(String))

            dt.Columns.Add("addlpaxcplistcode", GetType(String))
            dt.Columns.Add("wladultprice", GetType(String))
            dt.Columns.Add("wlchildprice", GetType(String))
            dt.Columns.Add("wlunitprice", GetType(String))
            dt.Columns.Add("wladdlpaxprice", GetType(String))
            dt.Columns.Add("wladultsalevalue", GetType(String))
            dt.Columns.Add("wlchildsalevalue", GetType(String))
            dt.Columns.Add("wlunitsalevalue", GetType(String))
            dt.Columns.Add("wladdlpaxsalevalue", GetType(String))
            dt.Columns.Add("wltotalsalevalue", GetType(String))
            dt.Columns.Add("wlcurrcode", GetType(String))
            dt.Columns.Add("wlconvrate", GetType(String))
            dt.Columns.Add("wlmarkupperc", GetType(String))
            dt.Columns.Add("currcode", GetType(String))

            'dt.Columns.Add("CostTaxableValue", GetType(String))
            'dt.Columns.Add("CostVATValue", GetType(String))
            'dt.Columns.Add("VATPer", GetType(String))
            'dt.Columns.Add("PriceWithTAX", GetType(String))
            'dt.Columns.Add("PriceTaxableValue", GetType(String))
            'dt.Columns.Add("PriceVATValue", GetType(String))
            'dt.Columns.Add("PriceVATPer", GetType(String))
            'dt.Columns.Add("PriceWithTAX1", GetType(String))

            For Each gvRow As DataListItem In dlMAArrivalSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")

                dr = dt.NewRow

                If chkbooknow.Checked = True Then

                    Dim lblMAImage As Label = gvRow.FindControl("lblMAImage")
                    Dim lblAirportTypeName As Label = gvRow.FindControl("lblAirportTypeName")
                    Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                    Dim lblminpax1 As Label = gvRow.FindControl("lblminpax1")
                    Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax1")
                    Dim lblunit As Label = gvRow.FindControl("lblunit")
                    Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
                    Dim lbladults As Label = gvRow.FindControl("lbladults")
                    Dim lblchild As Label = gvRow.FindControl("lblchild")
                    Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                    Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
                    Dim lblprice As Label = gvRow.FindControl("lblprice")
                    Dim lblvalue As Label = gvRow.FindControl("lblvalue")

                    Dim lblratebasis As Label = gvRow.FindControl("lblratebasis")
                    Dim lblairportmadate As Label = gvRow.FindControl("lblairportmadate")
                    Dim lbladdlpax As Label = gvRow.FindControl("lbladdlpax")
                    Dim lbladultprice As Label = gvRow.FindControl("lbladultprice")
                    Dim lblchildprice As Label = gvRow.FindControl("lblchildprice")
                    Dim lblunitprice As Label = gvRow.FindControl("lblprice")
                    Dim lbladdlpaxprice As Label = gvRow.FindControl("lbladdlpaxprice")
                    Dim lbladultsalevalue As Label = gvRow.FindControl("lbladultsalevalue")
                    Dim lblchildsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                    Dim lbladdlpaxsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                    Dim lblAdultChildText As Label = gvRow.FindControl("lblAdultChildText")

                    Dim lbladultplistcode As Label = gvRow.FindControl("lbladultplistcode")
                    Dim lblchildplistcode As Label = gvRow.FindControl("lblchildplistcode")
                    Dim lblunitplistcode As Label = gvRow.FindControl("lblunitplistcode")
                    Dim lbladdlpaxplistcode As Label = gvRow.FindControl("lbladdlpaxplistcode")
                    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")

                    Dim lnkcumunits As LinkButton = gvRow.FindControl("lnkcumunits")

                    Dim lblpreferredsupplier As Label = gvRow.FindControl("lblpreferredsupplier")
                    Dim lbladultcprice As Label = gvRow.FindControl("lbladultcprice")
                    Dim lblchildcprice As Label = gvRow.FindControl("lblchildcprice")
                    Dim lblunitcprice As Label = gvRow.FindControl("lblunitcprice")
                    Dim lbladdlpaxcprice As Label = gvRow.FindControl("lbladdlpaxcprice")
                    Dim lbladultcostvalue As Label = gvRow.FindControl("lbladultcostvalue")
                    Dim lblchildcostvalue As Label = gvRow.FindControl("lblchildcostvalue")
                    Dim lblunitcostvalue As Label = gvRow.FindControl("lblunitcostvalue")
                    Dim lbladdlpaxcostvalue As Label = gvRow.FindControl("lbladdlpaxcostvalue")
                    Dim lbltotalcostvalue As Label = gvRow.FindControl("lbltotalcostvalue")
                    Dim lbladultcplistcode As Label = gvRow.FindControl("lbladultcplistcode")
                    Dim lblchildcplistcode As Label = gvRow.FindControl("lblchildcplistcode")
                    Dim lblunitcplistcode As Label = gvRow.FindControl("lblunitcplistcode")
                    Dim lbladdlpaxcplistcode As Label = gvRow.FindControl("lbladdlpaxcplistcode")
                    Dim lblwladultprice As Label = gvRow.FindControl("lblwladultprice")
                    Dim lblwlchildprice As Label = gvRow.FindControl("lblwlchildprice")
                    Dim lblwlunitprice As Label = gvRow.FindControl("lblwlunitprice")
                    Dim lblwladdlpaxprice As Label = gvRow.FindControl("lblwladdlpaxprice")
                    Dim lblwladultsalevalue As Label = gvRow.FindControl("lblwladultsalevalue")
                    Dim lblwlchildsalevalue As Label = gvRow.FindControl("lblwlchildsalevalue")
                    Dim lblwlunitsalevalue As Label = gvRow.FindControl("lblwlunitsalevalue")
                    Dim lblwladdlpaxsalevalue As Label = gvRow.FindControl("lblwladdlpaxsalevalue")
                    Dim lblwltotalsalevalue As Label = gvRow.FindControl("lblwltotalsalevalue")
                    Dim lblwlcurrcode As Label = gvRow.FindControl("lblwlcurrcode")
                    Dim lblwlconvrate As Label = gvRow.FindControl("lblwlconvrate")
                    Dim lblwlmarkupperc As Label = gvRow.FindControl("lblwlmarkupperc")
                    Dim lblTotalCurrcode As Label = gvRow.FindControl("lblTotalCurrcode")

                    'Dim lblCostTaxableValue As Label = gvRow.FindControl("lblCostTaxableValue")
                    'Dim lblCostVATValue As Label = gvRow.FindControl("lblCostVATValue")
                    'Dim lblVATPer As Label = gvRow.FindControl("lblVATPer")
                    'Dim lblPriceWithTAX As Label = gvRow.FindControl("lblPriceWithTAX")
                    'Dim lblPriceTaxableValue As Label = gvRow.FindControl("lblPriceTaxableValue")
                    'Dim lblPriceVATValue As Label = gvRow.FindControl("lblPriceVATValue")
                    'Dim lblPriceVATPer As Label = gvRow.FindControl("lblPriceVATPer")
                    'Dim lblPriceWithTAX1 As Label = gvRow.FindControl("lblPriceWithTAX1")


                    dr("airportmatypecode") = hdAirportTypeCode.Value
                    dr("airportmatypename") = lblAirportTypeName.Text
                    dr("imagename") = lblMAImage.Text
                    dr("remarks") = lblremarks.Text
                    dr("Selected") = "1"
                    dr("minpax") = lblminpax1.Text
                    dr("maxpax") = lblmaxpax.Text
                    dr("ratebasis") = lblratebasis.Text
                    dr("airportmadate") = lblairportmadate.Text
                    dr("adults") = lbladults.Text
                    dr("child") = lblchild.Text
                    dr("childagestring") = lblchildagestring.Text
                    dr("units") = lblunit.Text
                    dr("addlpax") = lbladdlpax.Text
                    dr("adultprice") = lbladultprice.Text
                    dr("childprice") = lblchildprice.Text
                    dr("unitprice") = Val(lblprice.Text)
                    dr("addlpaxprice") = lbladdlpaxprice.Text
                    dr("adultsalevalue") = lbladultsalevalue.Text
                    dr("childsalevalue") = lblchildsalevalue.Text
                    dr("unitsalevalue") = lblvalue.Text
                    dr("addlpaxsalevalue") = lbladdlpaxsalevalue.Text
                    dr("totalsalevalue") = lbtotalValue.Text
                    dr("AdultChildText") = lblAdultChildText.Text
                    dr("adultplistcode") = lbladultplistcode.Text
                    dr("childplistcode") = lblchildplistcode.Text
                    dr("unitplistcode") = lblunitplistcode.Text
                    dr("addlpaxplistcode") = lbladdlpaxplistcode.Text
                    dr("currentselection") = hdnselected.Value
                    dr("cumulativeunits") = lnkcumunits.Text
                    dr("currcode") = lblTotalCurrcode.Text

                    dr("preferredsupplier") = lblpreferredsupplier.Text
                    dr("adultcprice") = lbladultcprice.Text
                    dr("childcprice") = lblchildcprice.Text
                    dr("unitcprice") = lblunitcprice.Text
                    dr("addlpaxcprice") = lbladdlpaxcprice.Text
                    dr("adultcostvalue") = lbladultcostvalue.Text
                    dr("unitcostvalue") = lblunitcostvalue.Text
                    dr("addlpaxcostvalue") = lbladdlpaxcostvalue.Text
                    dr("totalcostvalue") = lbltotalcostvalue.Text
                    dr("adultcplistcode") = lbladultcplistcode.Text
                    dr("childcplistcode") = lblchildcplistcode.Text
                    dr("unitcplistcode") = lblunitcplistcode.Text
                    dr("addlpaxcplistcode") = lbladdlpaxcplistcode.Text

                    Dim dAdultPrice As Decimal = IIf(lbladultprice.Text = "", "0.00", lbladultprice.Text)
                    Dim dwlAdultPrice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                    dwlAdultPrice = dAdultPrice * dWlMarkup
                    dr("wladultprice") = Math.Round(Val(dwlAdultPrice))

                    Dim dChildPrice As Decimal = IIf(lblchildprice.Text = "", "0.00", lblchildprice.Text)
                    Dim dwlChildPrice As Decimal
                    dwlChildPrice = dChildPrice * dWlMarkup
                    dr("wlchildprice") = Math.Round(Val(dwlChildPrice))


                    Dim dUnitprice As Decimal = IIf(lblunitprice.Text = "", "0.00", lblunitprice.Text)
                    Dim dwlunitprice As Decimal
                    dwlunitprice = dUnitprice * dWlMarkup
                    dr("wlunitprice") = Math.Round(Val(dwlunitprice))


                    Dim daddlpaxprice As Decimal = IIf(lbladdlpaxprice.Text = "", "0.00", lbladdlpaxprice.Text)
                    Dim dwladdlpaxprice As Decimal
                    dwladdlpaxprice = daddlpaxprice * dWlMarkup
                    dr("wladdlpaxprice") = Math.Round(Val(dwladdlpaxprice))

                    Dim dwlAdultSaleValue As Decimal
                    dwlAdultSaleValue = (Math.Round(dAdultPrice * dWlMarkup, 2)) * Val(lbladults.Text)
                    dr("wladultsalevalue") = Math.Round(dwlAdultSaleValue)

                    Dim dwlChildSaleValue As Decimal
                    dwlChildSaleValue = (Math.Round(dChildPrice * dWlMarkup, 2)) * Val(lblchild.Text)
                    dr("wlchildsalevalue") = Math.Round(dwlChildSaleValue)

                    Dim dwlunitsalevalue As Decimal
                    dwlunitsalevalue = (Math.Round(dUnitprice * dWlMarkup, 2)) * Val(lblunit.Text)
                    dr("wlunitsalevalue") = Math.Round(dwlunitsalevalue)

                    Dim dwlAddlpaxsalevalue As Decimal
                    dwlAddlpaxsalevalue = (Math.Round(daddlpaxprice * dWlMarkup, 2)) * Val(lbladdlpax.Text)
                    dr("wladdlpaxsalevalue") = Math.Round(dwlAddlpaxsalevalue)

                    If dr("ratebasis").ToString = "Adult/Child" Then
                        dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultPrice * dWlMarkup, 2)) * Val(lbladults.Text)) + Math.Round((Math.Round(dChildPrice * dWlMarkup, 2)) * Val(lblchild.Text))
                    Else
                        dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitprice * dWlMarkup, 2)) * Val(lblunit.Text)) + Math.Round((Math.Round(daddlpaxprice * dWlMarkup, 2)) * Val(lbladdlpax.Text))
                    End If


                    dr("wlcurrcode") = lblwlcurrcode.Text
                    dr("wlconvrate") = lblwlconvrate.Text
                    dr("wlmarkupperc") = lblwlmarkupperc.Text

                    'Dim dCostTaxableValue As Decimal = IIf(lblCostTaxableValue.Text = "", "0.00", lblCostTaxableValue.Text)
                    'Dim dCostVATValue As Decimal = IIf(lblCostVATValue.Text = "", "0.00", lblCostVATValue.Text)

                    'dr("CostTaxableValue") = lblCostTaxableValue.Text
                    'dr("CostVATValue") = lblCostVATValue.Text
                    'dr("VATPer") = lblVATPer.Text
                    'dr("PriceWithTAX") = lblPriceWithTAX.Text

                    'Dim dPriceTaxableValue As Decimal = IIf(lbltotalcostvalue.Text = "", "0.00", lbltotalcostvalue.Text)
                    'Dim dPriceVATPer As Decimal = IIf(lblPriceVATPer.Text = "", "0.00", lblPriceVATPer.Text)

                    'dPriceTaxableValue = dPriceTaxableValue / (1 + (dPriceVATPer / 100))

                    'Dim dPriceVATValue As Decimal = Val(lbltotalcostvalue.Text) - dPriceTaxableValue

                    'dr("PriceTaxableValue") = Math.Round(dPriceTaxableValue, 2)

                    'dr("PriceVATValue") = Math.Round(dPriceVATValue, 2)
                    'dr("PriceVATPer") = lblPriceVATPer.Text
                    'dr("PriceWithTAX1") = lblPriceWithTAX1.Text






                    dt.Rows.Add(dr)


                End If
            Next

            Session.Add("SelectedMAArrival", dt)
            Dim dvArrivalDetails As DataView = New DataView(dt)

            '''Arrival Close



            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))

            Dim myLinkButton As LinkButton = TryCast(dlMAArrivalSearchResults.Controls(dlMAArrivalSearchResults.Controls.Count - 1).FindControl("lbArrShowMore"), LinkButton)


            If myLinkButton.Text = "Show More" Then
                ViewState("ArrShow") = "1"
                BindArrivalDetails(dvMaiDetails, "1")
                myLinkButton.Text = "Show Less"

            Else
                ViewState("ArrShow") = "0"
                BindArrivalDetails(dvArrivalDetails, "")
                myLinkButton.Text = "Show More"
                dlMAArrivalSearchResults.Focus()
            End If

            '''' End

            '''' Departure  Expand

            If dlMADepartureSearchResults.Items.Count > 0 Then

                Dim mydepButton As LinkButton = TryCast(dlMADepartureSearchResults.Controls(dlMADepartureSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)

                dsSearchResults = Session("sDSMASearchResults")
                Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))


                If mydepButton.Text = "Show More" Then
                    ViewState("DepShow") = "1"
                    BindDepartureDetails(dvDepMaiDetails, "1")

                    mydepButton.Text = "Show Less"
                Else
                    ViewState("DepShow") = "0"
                    BindDepartureDetails(dvDepMaiDetails, "")
                    dlMADepartureSearchResults.Focus()
                    mydepButton.Text = "Show More"

                End If

            End If

        End If
        ''' End

    End Sub
    Protected Sub btnSelectedDeparture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedDeparture.Click
        Dim dt As New DataTable
        Dim dr As DataRow
        If dlMADepartureSearchResults.Items.Count > 1 Then
            Session("SelectedMADeparture") = Nothing


            dt.Columns.Add("airportmatypecode", GetType(String))
            dt.Columns.Add("airportmatypename", GetType(String))
            dt.Columns.Add("imagename", GetType(String))
            dt.Columns.Add("remarks", GetType(String))
            dt.Columns.Add("Selected", GetType(String))
            dt.Columns.Add("minpax", GetType(Integer))
            dt.Columns.Add("maxpax", GetType(String))
            dt.Columns.Add("ratebasis", GetType(String))
            dt.Columns.Add("airportmadate", GetType(String))
            dt.Columns.Add("adults", GetType(String))
            dt.Columns.Add("child", GetType(String))
            dt.Columns.Add("childagestring", GetType(String))
            dt.Columns.Add("units", GetType(String))
            dt.Columns.Add("addlpax", GetType(String))
            dt.Columns.Add("adultprice", GetType(String))
            dt.Columns.Add("childprice", GetType(String))
            dt.Columns.Add("unitprice", GetType(String))
            dt.Columns.Add("addlpaxprice", GetType(String))
            dt.Columns.Add("adultsalevalue", GetType(String))
            dt.Columns.Add("childsalevalue", GetType(String))
            dt.Columns.Add("unitsalevalue", GetType(String))
            dt.Columns.Add("addlpaxsalevalue", GetType(String))
            dt.Columns.Add("totalsalevalue", GetType(String))
            dt.Columns.Add("AdultChildText", GetType(String))
            dt.Columns.Add("adultplistcode", GetType(String))
            dt.Columns.Add("childplistcode", GetType(String))
            dt.Columns.Add("unitplistcode", GetType(String))
            dt.Columns.Add("addlpaxplistcode", GetType(String))
            dt.Columns.Add("currentselection", GetType(String))

            dt.Columns.Add("preferredsupplier", GetType(String))
            dt.Columns.Add("adultcprice", GetType(String))
            dt.Columns.Add("childcprice", GetType(String))
            dt.Columns.Add("unitcprice", GetType(String))
            dt.Columns.Add("addlpaxcprice", GetType(String))
            dt.Columns.Add("adultcostvalue", GetType(String))
            dt.Columns.Add("childcostvalue", GetType(String))
            dt.Columns.Add("unitcostvalue", GetType(String))
            dt.Columns.Add("addlpaxcostvalue", GetType(String))
            dt.Columns.Add("totalcostvalue", GetType(String))
            dt.Columns.Add("adultcplistcode", GetType(String))
            dt.Columns.Add("childcplistcode", GetType(String))
            dt.Columns.Add("unitcplistcode", GetType(String))

            dt.Columns.Add("addlpaxcplistcode", GetType(String))
            dt.Columns.Add("wladultprice", GetType(String))
            dt.Columns.Add("wlchildprice", GetType(String))
            dt.Columns.Add("wlunitprice", GetType(String))
            dt.Columns.Add("wladdlpaxprice", GetType(String))
            dt.Columns.Add("wladultsalevalue", GetType(String))
            dt.Columns.Add("wlchildsalevalue", GetType(String))
            dt.Columns.Add("wlunitsalevalue", GetType(String))
            dt.Columns.Add("wladdlpaxsalevalue", GetType(String))
            dt.Columns.Add("wltotalsalevalue", GetType(String))
            dt.Columns.Add("wlcurrcode", GetType(String))
            dt.Columns.Add("wlconvrate", GetType(String))
            dt.Columns.Add("wlmarkupperc", GetType(String))
            dt.Columns.Add("currcode", GetType(String))


            'dt.Columns.Add("CostTaxableValue", GetType(String))
            'dt.Columns.Add("CostVATValue", GetType(String))
            'dt.Columns.Add("VATPer", GetType(String))
            'dt.Columns.Add("PriceWithTAX", GetType(String))
            'dt.Columns.Add("PriceTaxableValue", GetType(String))
            'dt.Columns.Add("PriceVATValue", GetType(String))
            'dt.Columns.Add("PriceVATPer", GetType(String))
            'dt.Columns.Add("PriceWithTAX1", GetType(String))
            For Each gvRow As DataListItem In dlMADepartureSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")

                dr = dt.NewRow

                If chkbooknow.Checked = True Then

                    Dim lblMAImage As Label = gvRow.FindControl("lblMAImage")
                    Dim lblAirportTypeName As Label = gvRow.FindControl("lblAirportTypeName")
                    Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                    Dim lblminpax1 As Label = gvRow.FindControl("lblminpax1")
                    Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax1")
                    Dim lblunit As Label = gvRow.FindControl("lblunit")
                    Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
                    Dim lbladults As Label = gvRow.FindControl("lbladults")
                    Dim lblchild As Label = gvRow.FindControl("lblchild")
                    Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                    Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
                    Dim lblprice As Label = gvRow.FindControl("lblprice")
                    Dim lblvalue As Label = gvRow.FindControl("lblvalue")

                    Dim lblratebasis As Label = gvRow.FindControl("lblratebasis")
                    Dim lblairportmadate As Label = gvRow.FindControl("lblairportmadate")
                    Dim lbladdlpax As Label = gvRow.FindControl("lbladdlpax")
                    Dim lbladultprice As Label = gvRow.FindControl("lbladultprice")
                    Dim lblchildprice As Label = gvRow.FindControl("lblchildprice")
                    Dim lblunitprice As Label = gvRow.FindControl("lblprice")
                    Dim lbladdlpaxprice As Label = gvRow.FindControl("lbladdlpaxprice")
                    Dim lbladultsalevalue As Label = gvRow.FindControl("lbladultsalevalue")
                    Dim lblchildsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                    Dim lbladdlpaxsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                    Dim lblAdultChildText As Label = gvRow.FindControl("lblAdultChildText")
                    Dim lbladultplistcode As Label = gvRow.FindControl("lbladultplistcode")
                    Dim lblchildplistcode As Label = gvRow.FindControl("lblchildplistcode")
                    Dim lblunitplistcode As Label = gvRow.FindControl("lblunitplistcode")
                    Dim lbladdlpaxplistcode As Label = gvRow.FindControl("lbladdlpaxplistcode")
                    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")

                    Dim lblpreferredsupplier As Label = gvRow.FindControl("lblpreferredsupplier")
                    Dim lbladultcprice As Label = gvRow.FindControl("lbladultcprice")
                    Dim lblchildcprice As Label = gvRow.FindControl("lblchildcprice")
                    Dim lblunitcprice As Label = gvRow.FindControl("lblunitcprice")
                    Dim lbladdlpaxcprice As Label = gvRow.FindControl("lbladdlpaxcprice")
                    Dim lbladultcostvalue As Label = gvRow.FindControl("lbladultcostvalue")
                    Dim lblchildcostvalue As Label = gvRow.FindControl("lblchildcostvalue")
                    Dim lblunitcostvalue As Label = gvRow.FindControl("lblunitcostvalue")
                    Dim lbladdlpaxcostvalue As Label = gvRow.FindControl("lbladdlpaxcostvalue")
                    Dim lbltotalcostvalue As Label = gvRow.FindControl("lbltotalcostvalue")
                    Dim lbladultcplistcode As Label = gvRow.FindControl("lbladultcplistcode")
                    Dim lblchildcplistcode As Label = gvRow.FindControl("lblchildcplistcode")
                    Dim lblunitcplistcode As Label = gvRow.FindControl("lblunitcplistcode")
                    Dim lbladdlpaxcplistcode As Label = gvRow.FindControl("lbladdlpaxcplistcode")
                    Dim lblwladultprice As Label = gvRow.FindControl("lblwladultprice")
                    Dim lblwlchildprice As Label = gvRow.FindControl("lblwlchildprice")
                    Dim lblwlunitprice As Label = gvRow.FindControl("lblwlunitprice")
                    Dim lblwladdlpaxprice As Label = gvRow.FindControl("lblwladdlpaxprice")
                    Dim lblwladultsalevalue As Label = gvRow.FindControl("lblwladultsalevalue")
                    Dim lblwlchildsalevalue As Label = gvRow.FindControl("lblwlchildsalevalue")
                    Dim lblwlunitsalevalue As Label = gvRow.FindControl("lblwlunitsalevalue")
                    Dim lblwladdlpaxsalevalue As Label = gvRow.FindControl("lblwladdlpaxsalevalue")
                    Dim lblwltotalsalevalue As Label = gvRow.FindControl("lblwltotalsalevalue")
                    Dim lblwlcurrcode As Label = gvRow.FindControl("lblwlcurrcode")
                    Dim lblwlconvrate As Label = gvRow.FindControl("lblwlconvrate")
                    Dim lblwlmarkupperc As Label = gvRow.FindControl("lblwlmarkupperc")
                    Dim lblTotalCurrcode As Label = gvRow.FindControl("lblTotalCurrcode")

                    'Dim lblCostTaxableValue As Label = gvRow.FindControl("lblCostTaxableValue")
                    'Dim lblCostVATValue As Label = gvRow.FindControl("lblCostVATValue")
                    'Dim lblVATPer As Label = gvRow.FindControl("lblVATPer")
                    'Dim lblPriceWithTAX As Label = gvRow.FindControl("lblPriceWithTAX")
                    'Dim lblPriceTaxableValue As Label = gvRow.FindControl("lblPriceTaxableValue")
                    'Dim lblPriceVATValue As Label = gvRow.FindControl("lblPriceVATValue")
                    'Dim lblPriceVATPer As Label = gvRow.FindControl("lblPriceVATPer")
                    'Dim lblPriceWithTAX1 As Label = gvRow.FindControl("lblPriceWithTAX1")

                    dr("airportmatypecode") = hdAirportTypeCode.Value
                    dr("airportmatypename") = lblAirportTypeName.Text
                    dr("imagename") = lblMAImage.Text
                    dr("remarks") = lblremarks.Text
                    dr("Selected") = "1"
                    dr("minpax") = lblminpax1.Text
                    dr("maxpax") = lblmaxpax.Text
                    dr("ratebasis") = lblratebasis.Text
                    dr("airportmadate") = lblairportmadate.Text
                    dr("adults") = lbladults.Text
                    dr("child") = lblchild.Text
                    dr("childagestring") = lblchildagestring.Text
                    dr("units") = lblunit.Text
                    dr("addlpax") = lbladdlpax.Text
                    dr("adultprice") = lbladultprice.Text
                    dr("childprice") = lblchildprice.Text
                    dr("unitprice") = Val(lblprice.Text)
                    dr("addlpaxprice") = lbladdlpaxprice.Text
                    dr("adultsalevalue") = lbladultsalevalue.Text
                    dr("childsalevalue") = lblchildsalevalue.Text
                    dr("unitsalevalue") = lblvalue.Text
                    dr("addlpaxsalevalue") = lbladdlpaxsalevalue.Text
                    dr("totalsalevalue") = lbtotalValue.Text
                    dr("AdultChildText") = lblAdultChildText.Text
                    dr("adultplistcode") = lbladultplistcode.Text
                    dr("childplistcode") = lblchildplistcode.Text
                    dr("unitplistcode") = lblunitplistcode.Text
                    dr("addlpaxplistcode") = lbladdlpaxplistcode.Text
                    dr("currentselection") = hdnselected.Value
                    dr("currcode") = lblTotalCurrcode.Text

                    dr("preferredsupplier") = lblpreferredsupplier.Text
                    dr("adultcprice") = lbladultcprice.Text
                    dr("childcprice") = lblchildcprice.Text
                    dr("unitcprice") = lblunitcprice.Text
                    dr("addlpaxcprice") = lbladdlpaxcprice.Text
                    dr("adultcostvalue") = lbladultcostvalue.Text
                    dr("childcostvalue") = lblchildcostvalue.Text
                    dr("unitcostvalue") = lblunitcostvalue.Text
                    dr("addlpaxcostvalue") = lbladdlpaxcostvalue.Text
                    dr("totalcostvalue") = lbltotalcostvalue.Text
                    dr("adultcplistcode") = lbladultcplistcode.Text
                    dr("childcplistcode") = lblchildcplistcode.Text
                    dr("unitcplistcode") = lblunitcplistcode.Text
                    dr("addlpaxcplistcode") = lbladdlpaxcplistcode.Text

                    Dim dAdultPrice As Decimal = IIf(lbladultprice.Text = "", "0.00", lbladultprice.Text)
                    Dim dwlAdultPrice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                    dwlAdultPrice = dAdultPrice * dWlMarkup
                    dr("wladultprice") = Math.Round(Val(dwlAdultPrice))

                    Dim dChildPrice As Decimal = IIf(lblchildprice.Text = "", "0.00", lblchildprice.Text)
                    Dim dwlChildPrice As Decimal
                    dwlChildPrice = dChildPrice * dWlMarkup
                    dr("wlchildprice") = Math.Round(Val(dwlChildPrice))


                    Dim dUnitprice As Decimal = IIf(lblunitprice.Text = "", "0.00", lblunitprice.Text)
                    Dim dwlunitprice As Decimal
                    dwlunitprice = dUnitprice * dWlMarkup
                    dr("wlunitprice") = Math.Round(Val(dwlunitprice))


                    Dim daddlpaxprice As Decimal = IIf(lbladdlpaxprice.Text = "", "0.00", lbladdlpaxprice.Text)
                    Dim dwladdlpaxprice As Decimal
                    dwladdlpaxprice = daddlpaxprice * dWlMarkup
                    dr("wladdlpaxprice") = Math.Round(Val(dwladdlpaxprice))

                    Dim dwlAdultSaleValue As Decimal
                    dwlAdultSaleValue = (Math.Round(dAdultPrice * dWlMarkup, 2)) * Val(lbladults.Text)
                    dr("wladultsalevalue") = Math.Round(dwlAdultSaleValue)

                    Dim dwlChildSaleValue As Decimal
                    dwlChildSaleValue = (Math.Round(dChildPrice * dWlMarkup, 2)) * Val(lblchild.Text)
                    dr("wlchildsalevalue") = Math.Round(dwlChildSaleValue)

                    Dim dwlunitsalevalue As Decimal
                    dwlunitsalevalue = (Math.Round(dUnitprice * dWlMarkup, 2)) * Val(lblunit.Text)
                    dr("wlunitsalevalue") = Math.Round(dwlunitsalevalue)

                    Dim dwlAddlpaxsalevalue As Decimal
                    dwlAddlpaxsalevalue = (Math.Round(daddlpaxprice * dWlMarkup, 2)) * Val(lbladdlpax.Text)
                    dr("wladdlpaxsalevalue") = Math.Round(dwlAddlpaxsalevalue)

                    If dr("ratebasis").ToString = "Adult/Child" Then
                        dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultPrice * dWlMarkup, 2)) * Val(lbladults.Text)) + Math.Round((Math.Round(dChildPrice * dWlMarkup, 2)) * Val(lblchild.Text))
                    Else
                        dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitprice * dWlMarkup, 2)) * Val(lblunit.Text)) + Math.Round((Math.Round(daddlpaxprice * dWlMarkup, 2)) * Val(lbladdlpax.Text))
                    End If


                    dr("wlcurrcode") = lblwlcurrcode.Text
                    dr("wlconvrate") = lblwlconvrate.Text
                    dr("wlmarkupperc") = lblwlmarkupperc.Text

                    dt.Rows.Add(dr)


                End If
            Next

            Session.Add("SelectedMADeparture", dt)
            Dim dvDepartureDetails As DataView = New DataView(dt)

            ''' Departure close



            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(1))

            Dim myLinkButton As LinkButton = TryCast(dlMADepartureSearchResults.Controls(dlMADepartureSearchResults.Controls.Count - 1).FindControl("lbDepShowMore"), LinkButton)


            If myLinkButton.Text = "Show More" Then
                ViewState("DepShow") = "1"
                BindDepartureDetails(dvDepMaiDetails, "1")

                myLinkButton.Text = "Show Less"
            Else
                ViewState("DepShow") = "0"
                BindDepartureDetails(dvDepartureDetails, "")
                dlMADepartureSearchResults.Focus()
                myLinkButton.Text = "Show More"

            End If

            '''' End

            '''' Transit  Expand

            If dlMATransitSearchResults.Items.Count > 0 Then

                Dim mydepButton As LinkButton = TryCast(dlMATransitSearchResults.Controls(dlMATransitSearchResults.Controls.Count - 1).FindControl("lbTransitShowMore"), LinkButton)

                dsSearchResults = Session("sDSMASearchResults")
                Dim dvTransitDetails As DataView = New DataView(dsSearchResults.Tables(2))

                If mydepButton.Text = "Show More" Then
                    ViewState("TransitShow") = "1"

                    BindTransitDetails(dvTransitDetails, "1")
                    mydepButton.Text = "Show Less"

                Else
                    ViewState("TransitShow") = "0"

                    BindTransitDetails(dvTransitDetails, "")
                    mydepButton.Text = "Show More"
                    dlMATransitSearchResults.Focus()
                End If

            End If

        End If
        ''' End
    End Sub
    Protected Sub btnSelectedTransit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedTransit.Click
        Dim dt As New DataTable
        Dim dr As DataRow
        If dlMATransitSearchResults.Items.Count > 1 Then
            Session("SelectedMATransit") = Nothing


            dt.Columns.Add("airportmatypecode", GetType(String))
            dt.Columns.Add("airportmatypename", GetType(String))
            dt.Columns.Add("imagename", GetType(String))
            dt.Columns.Add("remarks", GetType(String))
            dt.Columns.Add("Selected", GetType(String))
            dt.Columns.Add("minpax", GetType(Integer))
            dt.Columns.Add("maxpax", GetType(String))
            dt.Columns.Add("ratebasis", GetType(String))
            dt.Columns.Add("airportmadate", GetType(String))
            dt.Columns.Add("adults", GetType(String))
            dt.Columns.Add("child", GetType(String))
            dt.Columns.Add("childagestring", GetType(String))
            dt.Columns.Add("units", GetType(String))
            dt.Columns.Add("addlpax", GetType(String))
            dt.Columns.Add("adultprice", GetType(String))
            dt.Columns.Add("childprice", GetType(String))
            dt.Columns.Add("unitprice", GetType(String))
            dt.Columns.Add("addlpaxprice", GetType(String))
            dt.Columns.Add("adultsalevalue", GetType(String))
            dt.Columns.Add("childsalevalue", GetType(String))
            dt.Columns.Add("unitsalevalue", GetType(String))
            dt.Columns.Add("addlpaxsalevalue", GetType(String))
            dt.Columns.Add("totalsalevalue", GetType(String))
            dt.Columns.Add("AdultChildText", GetType(String))
            dt.Columns.Add("adultplistcode", GetType(String))
            dt.Columns.Add("childplistcode", GetType(String))
            dt.Columns.Add("unitplistcode", GetType(String))
            dt.Columns.Add("addlpaxplistcode", GetType(String))
            dt.Columns.Add("currentselection", GetType(String))

            dt.Columns.Add("preferredsupplier", GetType(String))
            dt.Columns.Add("adultcprice", GetType(String))
            dt.Columns.Add("childcprice", GetType(String))
            dt.Columns.Add("unitcprice", GetType(String))
            dt.Columns.Add("addlpaxcprice", GetType(String))
            dt.Columns.Add("adultcostvalue", GetType(String))
            dt.Columns.Add("childcostvalue", GetType(String))
            dt.Columns.Add("unitcostvalue", GetType(String))
            dt.Columns.Add("addlpaxcostvalue", GetType(String))
            dt.Columns.Add("totalcostvalue", GetType(String))
            dt.Columns.Add("adultcplistcode", GetType(String))
            dt.Columns.Add("childcplistcode", GetType(String))
            dt.Columns.Add("unitcplistcode", GetType(String))

            dt.Columns.Add("addlpaxcplistcode", GetType(String))
            dt.Columns.Add("wladultprice", GetType(String))
            dt.Columns.Add("wlchildprice", GetType(String))
            dt.Columns.Add("wlunitprice", GetType(String))
            dt.Columns.Add("wladdlpaxprice", GetType(String))
            dt.Columns.Add("wladultsalevalue", GetType(String))
            dt.Columns.Add("wlchildsalevalue", GetType(String))
            dt.Columns.Add("wlunitsalevalue", GetType(String))
            dt.Columns.Add("wladdlpaxsalevalue", GetType(String))
            dt.Columns.Add("wltotalsalevalue", GetType(String))
            dt.Columns.Add("wlcurrcode", GetType(String))
            dt.Columns.Add("wlconvrate", GetType(String))
            dt.Columns.Add("wlmarkupperc", GetType(String))
            dt.Columns.Add("currcode", GetType(String))

            For Each gvRow As DataListItem In dlMATransitSearchResults.Items
                Dim chkbooknow As CheckBox = gvRow.FindControl("chkbooknow")
                Dim hdAirportTypeCode As HiddenField = gvRow.FindControl("hdAirportTypeCode")

                dr = dt.NewRow

                If chkbooknow.Checked = True Then

                    Dim lblMAImage As Label = gvRow.FindControl("lblMAImage")
                    Dim lblAirportTypeName As Label = gvRow.FindControl("lblAirportTypeName")
                    Dim lblremarks As Label = gvRow.FindControl("lblremarks")
                    Dim lblminpax1 As Label = gvRow.FindControl("lblminpax1")
                    Dim lblmaxpax As Label = gvRow.FindControl("lblmaxpax1")
                    Dim lblunit As Label = gvRow.FindControl("lblunit")
                    Dim lbtotalValue As LinkButton = gvRow.FindControl("lbtotalValue")
                    Dim lbladults As Label = gvRow.FindControl("lbladults")
                    Dim lblchild As Label = gvRow.FindControl("lblchild")
                    Dim lblchildagestring As Label = gvRow.FindControl("lblchildagestring")
                    Dim lblMAunit As Label = gvRow.FindControl("lblMAunit")
                    Dim lblprice As Label = gvRow.FindControl("lblprice")
                    Dim lblvalue As Label = gvRow.FindControl("lblvalue")

                    Dim lblratebasis As Label = gvRow.FindControl("lblratebasis")
                    Dim lblairportmadate As Label = gvRow.FindControl("lblairportmadate")
                    Dim lbladdlpax As Label = gvRow.FindControl("lbladdlpax")
                    Dim lbladultprice As Label = gvRow.FindControl("lbladultprice")
                    Dim lblchildprice As Label = gvRow.FindControl("lblchildprice")
                    Dim lblunitprice As Label = gvRow.FindControl("lblprice")
                    Dim lbladdlpaxprice As Label = gvRow.FindControl("lbladdlpaxprice")
                    Dim lbladultsalevalue As Label = gvRow.FindControl("lbladultsalevalue")
                    Dim lblchildsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                    Dim lbladdlpaxsalevalue As Label = gvRow.FindControl("lblchildsalevalue")
                    Dim lblAdultChildText As Label = gvRow.FindControl("lblAdultChildText")
                    Dim lbladultplistcode As Label = gvRow.FindControl("lbladultplistcode")
                    Dim lblchildplistcode As Label = gvRow.FindControl("lblchildplistcode")
                    Dim lblunitplistcode As Label = gvRow.FindControl("lblunitplistcode")
                    Dim lbladdlpaxplistcode As Label = gvRow.FindControl("lbladdlpaxplistcode")
                    Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")

                    Dim lblpreferredsupplier As Label = gvRow.FindControl("lblpreferredsupplier")
                    Dim lbladultcprice As Label = gvRow.FindControl("lbladultcprice")
                    Dim lblchildcprice As Label = gvRow.FindControl("lblchildcprice")
                    Dim lblunitcprice As Label = gvRow.FindControl("lblunitcprice")
                    Dim lbladdlpaxcprice As Label = gvRow.FindControl("lbladdlpaxcprice")
                    Dim lbladultcostvalue As Label = gvRow.FindControl("lbladultcostvalue")
                    Dim lblchildcostvalue As Label = gvRow.FindControl("lblchildcostvalue")
                    Dim lblunitcostvalue As Label = gvRow.FindControl("lblunitcostvalue")
                    Dim lbladdlpaxcostvalue As Label = gvRow.FindControl("lbladdlpaxcostvalue")
                    Dim lbltotalcostvalue As Label = gvRow.FindControl("lbltotalcostvalue")
                    Dim lbladultcplistcode As Label = gvRow.FindControl("lbladultcplistcode")
                    Dim lblchildcplistcode As Label = gvRow.FindControl("lblchildcplistcode")
                    Dim lblunitcplistcode As Label = gvRow.FindControl("lblunitcplistcode")
                    Dim lbladdlpaxcplistcode As Label = gvRow.FindControl("lbladdlpaxcplistcode")
                    Dim lblwladultprice As Label = gvRow.FindControl("lblwladultprice")
                    Dim lblwlchildprice As Label = gvRow.FindControl("lblwlchildprice")
                    Dim lblwlunitprice As Label = gvRow.FindControl("lblwlunitprice")
                    Dim lblwladdlpaxprice As Label = gvRow.FindControl("lblwladdlpaxprice")
                    Dim lblwladultsalevalue As Label = gvRow.FindControl("lblwladultsalevalue")
                    Dim lblwlchildsalevalue As Label = gvRow.FindControl("lblwlchildsalevalue")
                    Dim lblwlunitsalevalue As Label = gvRow.FindControl("lblwlunitsalevalue")
                    Dim lblwladdlpaxsalevalue As Label = gvRow.FindControl("lblwladdlpaxsalevalue")
                    Dim lblwltotalsalevalue As Label = gvRow.FindControl("lblwltotalsalevalue")
                    Dim lblwlcurrcode As Label = gvRow.FindControl("lblwlcurrcode")
                    Dim lblwlconvrate As Label = gvRow.FindControl("lblwlconvrate")
                    Dim lblwlmarkupperc As Label = gvRow.FindControl("lblwlmarkupperc")
                    Dim lblTotalCurrcode As Label = gvRow.FindControl("lblTotalCurrcode")

                    dr("airportmatypecode") = hdAirportTypeCode.Value
                    dr("airportmatypename") = lblAirportTypeName.Text
                    dr("imagename") = lblMAImage.Text
                    dr("remarks") = lblremarks.Text
                    dr("Selected") = "1"
                    dr("minpax") = lblminpax1.Text
                    dr("maxpax") = lblmaxpax.Text
                    dr("ratebasis") = lblratebasis.Text
                    dr("airportmadate") = lblairportmadate.Text
                    dr("adults") = lbladults.Text
                    dr("child") = lblchild.Text
                    dr("childagestring") = lblchildagestring.Text
                    dr("units") = lblunit.Text
                    dr("addlpax") = lbladdlpax.Text
                    dr("adultprice") = lbladultprice.Text
                    dr("childprice") = lblchildprice.Text
                    dr("unitprice") = Val(lblprice.Text)
                    dr("addlpaxprice") = lbladdlpaxprice.Text
                    dr("adultsalevalue") = lbladultsalevalue.Text
                    dr("childsalevalue") = lblchildsalevalue.Text
                    dr("unitsalevalue") = lblvalue.Text
                    dr("addlpaxsalevalue") = lbladdlpaxsalevalue.Text
                    dr("totalsalevalue") = lbtotalValue.Text
                    dr("AdultChildText") = lblAdultChildText.Text
                    dr("adultplistcode") = lbladultplistcode.Text
                    dr("childplistcode") = lblchildplistcode.Text
                    dr("unitplistcode") = lblunitplistcode.Text
                    dr("addlpaxplistcode") = lbladdlpaxplistcode.Text
                    dr("currentselection") = hdnselected.Value
                    dr("currcode") = lblTotalCurrcode.Text

                    dr("preferredsupplier") = lblpreferredsupplier.Text
                    dr("adultcprice") = lbladultcprice.Text
                    dr("childcprice") = lblchildcprice.Text
                    dr("unitcprice") = lblunitcprice.Text
                    dr("addlpaxcprice") = lbladdlpaxcprice.Text
                    dr("adultcostvalue") = lbladultcostvalue.Text
                    dr("childcostvalue") = lblchildcostvalue.Text
                    dr("unitcostvalue") = lblunitcostvalue.Text
                    dr("addlpaxcostvalue") = lbladdlpaxcostvalue.Text
                    dr("totalcostvalue") = lbltotalcostvalue.Text
                    dr("adultcplistcode") = lbladultcplistcode.Text
                    dr("childcplistcode") = lblchildcplistcode.Text
                    dr("unitcplistcode") = lblunitcplistcode.Text
                    dr("addlpaxcplistcode") = lbladdlpaxcplistcode.Text

                    Dim dAdultPrice As Decimal = IIf(lbladultprice.Text = "", "0.00", lbladultprice.Text)
                    Dim dwlAdultPrice As Decimal
                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                    dwlAdultPrice = dAdultPrice * dWlMarkup
                    dr("wladultprice") = Math.Round(Val(dwlAdultPrice))

                    Dim dChildPrice As Decimal = IIf(lblchildprice.Text = "", "0.00", lblchildprice.Text)
                    Dim dwlChildPrice As Decimal
                    dwlChildPrice = dChildPrice * dWlMarkup
                    dr("wlchildprice") = Math.Round(Val(dwlChildPrice))


                    Dim dUnitprice As Decimal = IIf(lblunitprice.Text = "", "0.00", lblunitprice.Text)
                    Dim dwlunitprice As Decimal
                    dwlunitprice = dUnitprice * dWlMarkup
                    dr("wlunitprice") = Math.Round(Val(dwlunitprice))


                    Dim daddlpaxprice As Decimal = IIf(lbladdlpaxprice.Text = "", "0.00", lbladdlpaxprice.Text)
                    Dim dwladdlpaxprice As Decimal
                    dwladdlpaxprice = daddlpaxprice * dWlMarkup
                    dr("wladdlpaxprice") = Math.Round(Val(dwladdlpaxprice))

                    Dim dwlAdultSaleValue As Decimal
                    dwlAdultSaleValue = (Math.Round(dAdultPrice * dWlMarkup, 2)) * Val(lbladults.Text)
                    dr("wladultsalevalue") = Math.Round(dwlAdultSaleValue)

                    Dim dwlChildSaleValue As Decimal
                    dwlChildSaleValue = (Math.Round(dChildPrice * dWlMarkup, 2)) * Val(lblchild.Text)
                    dr("wlchildsalevalue") = Math.Round(dwlChildSaleValue)

                    Dim dwlunitsalevalue As Decimal
                    dwlunitsalevalue = (Math.Round(dUnitprice * dWlMarkup, 2)) * Val(lblunit.Text)
                    dr("wlunitsalevalue") = Math.Round(dwlunitsalevalue)

                    Dim dwlAddlpaxsalevalue As Decimal
                    dwlAddlpaxsalevalue = (Math.Round(daddlpaxprice * dWlMarkup, 2)) * Val(lbladdlpax.Text)
                    dr("wladdlpaxsalevalue") = Math.Round(dwlAddlpaxsalevalue)

                    If dr("ratebasis").ToString = "Adult/Child" Then
                        dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultPrice * dWlMarkup, 2)) * Val(lbladults.Text)) + Math.Round((Math.Round(dChildPrice * dWlMarkup, 2)) * Val(lblchild.Text))
                    Else
                        dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitprice * dWlMarkup, 2)) * Val(lblunit.Text)) + Math.Round((Math.Round(daddlpaxprice * dWlMarkup, 2)) * Val(lbladdlpax.Text))
                    End If


                    dr("wlcurrcode") = lblwlcurrcode.Text
                    dr("wlconvrate") = lblwlconvrate.Text
                    dr("wlmarkupperc") = lblwlmarkupperc.Text

                    dt.Rows.Add(dr)


                End If
            Next

            Session.Add("SelectedMATransit", dt)
            Dim dvTransitDetails As DataView = New DataView(dt)

            ''' Transit close



            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSMASearchResults")
            Dim dvDepMaiDetails As DataView = New DataView(dsSearchResults.Tables(2))

            Dim myLinkButton As LinkButton = TryCast(dlMATransitSearchResults.Controls(dlMATransitSearchResults.Controls.Count - 1).FindControl("lbTransitShowMore"), LinkButton)


            If myLinkButton.Text = "Show More" Then
                ViewState("TransitShow") = "1"

                BindTransitDetails(dvDepMaiDetails, "1")
                myLinkButton.Text = "Show Less"

            Else
                ViewState("TransitShow") = "0"

                BindTransitDetails(dvTransitDetails, "")
                myLinkButton.Text = "Show More"
                dlMATransitSearchResults.Focus()
            End If

            '''' End
        End If

    End Sub
    '*** Danny 04/06/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
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
