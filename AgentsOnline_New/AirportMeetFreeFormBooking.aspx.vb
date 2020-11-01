Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Linq
Partial Class AirportMeetFreeFormBooking
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
    Dim BLLAirportMeetFreeFormBooking As New BLLAirportMeetFreeFormBooking
    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try


                Session("State") = "New"
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
                    clearallBookingSessions()
                    Session("sFinalBooked") = Nothing '"0" 'changed by mohamed on 12/08/2018

                End If

                LoadHome()


            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub
    ''' <summary>
    ''' LoadHome
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadHome()

        LoadFooter()
        hdChildAgeLimit.Value = objResParam.ChildAgeLimit
        hdMaxNoOfNight.Value = objResParam.NoOfNightLimit
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        LoadFields()

        ShowMyBooking()
        If Not Request.QueryString("ALineNo") Is Nothing Then
            ViewState("vALineNo") = Request.QueryString("ALineNo")
            hdHotelTabFreeze.Value = "1"
        End If
        If Not Session("sRequestId") Is Nothing Then
            Dim dtBookingHeader As DataTable
            dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dtBookingHeader.Rows.Count > 0 Then
                txtCountry.Text = dtBookingHeader.Rows(0)("sourcectryname").ToString
                txtCountryCode.Text = dtBookingHeader.Rows(0)("sourcectrycode").ToString
                txtCustomer.Text = dtBookingHeader.Rows(0)("agentname").ToString
                txtCustomerCode.Text = dtBookingHeader.Rows(0)("agentcode").ToString
                If Session("sLoginType") = "RO" Then
                    txtCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtCustomer.Enabled = False
                    AutoCompleteExtender_txtCountry.Enabled = False
                End If
            End If
        End If
        Session("sdtAirportmeetDetails") = Nothing
        BindAirportMeetDatas()



        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If

    End Sub
    Private Sub BindAMflightclass(ByVal ddlAMFlightClass As DropDownList)
        Dim strQuery As String = ""
        strQuery = "select flightclscode,flightclsname from flightclsmast(nolock) where active=1"
        objclsUtilities.FillDropDownList(ddlAMFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")

        ddlAMFlightClass.SelectedIndex = 2

    End Sub
    ''' <summary>
    '''  <LoadLogo/>
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo 'Abin on 20180710

            End If

        End If
    End Sub

    ''' <summary>
    ''' LoadMenus
    ''' </summary>
    ''' <remarks></remarks>
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
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    ''' <summary>
    ''' LoadFields
    ''' </summary>
    ''' <remarks></remarks>
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
                ' dvForRO.Visible = False

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
                '   dvForRO.Visible = True

            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetAMCustomer(ByVal prefixText As String) As List(Of String)

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

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetAirportMeetList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If

            strSqlQry = "select o.othtypcode+'|'+ case when a.ratebasis ='Unit' then a.ratebasis else 'ACS' end othtypcode,o.othtypname from othtypmast(nolock) o,airportmatypes(nolock) a where othgrpcode='AIRPORTMA'  and  CHARINDEX('" & contextKey & "',upper(servicetype)) > 0   	and a.othtypcode=o.othtypcode and  o.active=1 and o.othtypname like  '" & prefixText & "%'  order by o.othtypname  "



            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othtypname").ToString(), myDS.Tables(0).Rows(i)("othtypcode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function


    ''' <summary>
    ''' GetCustomers
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCustomers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
                End If
            End If


            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("agentname").ToString(), myDt.Rows(i)("agentcode").ToString()))
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
    Public Shared Function GetCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
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
            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)
            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("ctryname").ToString(), myDt.Rows(i)("ctrycode").ToString()))
                Next
            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
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
            myDS = clsUtilities.GetSharedDataFromDataSet(strSqlQry)
            myDS.DataSetName = "Countries"
            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
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


    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetFlight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Dim strDate As String = ""
        Dim strAirportMeetType As String = ""
        If contextKey <> "" Then

            Dim str As String() = contextKey.Split("|")
            strAirportMeetType = str(0)
            strDate = str(1)
        End If


        Dim lstflight As New List(Of String)



        Try

            Dim strRequestId As String = ""
            strRequestId = GetRequestId()

            If prefixText = " " Then
                prefixText = ""
            End If


            If strAirportMeetType = "ARRIVAL" Then
                If strRequestId <> "" And 1 = 2 Then '1=2 is added since not able to change flight no. in edit mode 'changed by mohamed on 17/09/2018
                    strSqlQry = "select arrflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,arrflightcode flightcode from booking_guest_flightstemp(nolock) where requestid='" & strRequestId & "' "

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    Else
                        strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & strDate & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "


                        SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                        'Open connection
                        myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                        myDataAdapter.Fill(myDS)

                        If myDS.Tables(0).Rows.Count > 0 Then
                            For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                                lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                            Next
                        End If
                    End If

                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & strDate & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    End If

                End If
            ElseIf strAirportMeetType = "DEPARTURE" Then
                If strRequestId <> "" And 1 = 2 Then '1=2 is added since not able to change flight no. in edit mode 'changed by mohamed on 17/09/2018 
                    strSqlQry = "select depflight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,depflightcode flightcode from booking_guest_flightstemp(nolock) where requestid='" & strRequestId & "' "
                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    Else
                        strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & strDate & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
                        SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                        'Open connection
                        myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                        myDataAdapter.Fill(myDS)

                        If myDS.Tables(0).Rows.Count > 0 Then
                            For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                                lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                            Next
                        End If
                    End If
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & strDate & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "
                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                            lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                        Next
                    End If
                End If
            Else
                If prefixText = " " Then
                    prefixText = ""
                End If
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & strDate & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & strDate & "',103)))  and flightcode like  '" & prefixText & "%' order by replace(Flightcode,' ','') "

                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        lstflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                    Next

                End If
            End If

            Return lstflight
        Catch ex As Exception
            Return lstflight
        End Try

    End Function


    Private Shared Function GetRequestId() As String
        Dim strRequestId As String = ""
        If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sRequestId")
        End If
        Return strRequestId
    End Function


    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetAirportMeetFrom(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim lstAirportMeetFrom As New List(Of String)
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
                    lstAirportMeetFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                Next

            End If





            Return lstAirportMeetFrom
        Catch ex As Exception
            Return lstAirportMeetFrom
        End Try

    End Function


    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetAirportMeetTo(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim lstAirportMeetTo As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If


            strSqlQry = ""
            If contextKey = "ARRIVAL" Then
                If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                    Dim dt As New DataTable
                    'dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from booking_hotel_detailtemp b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                    dt = clsUtilities.GetSharedDataFromDataTable("select  sum(cnt) cnt from (select count(*) cnt from booking_hotel_detailtemp b(nolock)  where requestid='" & HttpContext.Current.Session("sRequestId") & "' union all  select count(*) cnt from booking_hotels_prearrangedtemp b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "' ) rs ")

                    If dt.Rows(0)("cnt") > 1 Then
                        strSqlQry = "select b.partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                       & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and isnull(b.shiftto,0)=0  and p.partyname like  '%" & prefixText & "%' "

                    Else
                        strSqlQry = "select  partycode,partyname from  ( select  partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode,othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                    & " where  partyname like '%" & prefixText & "%' order by partyname "
                    End If
                Else
                    strSqlQry = "select  partycode,partyname from  ( select  partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode,othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
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
                        lstAirportMeetTo.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    Next

                End If
            ElseIf contextKey = "DEPARTURE" Then



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
                        lstAirportMeetTo.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))

                    Next

                End If
            ElseIf contextKey = "INTER HOTEL" Then

                If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                    Dim dt As New DataTable
                    dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                    If dt.Rows(0)("cnt") > 1 Then
                        strSqlQry = "select b.partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply  (select min(b1.checkin) checkin from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                        & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and  b.checkin<>m.checkin and isnull(b.shiftto,0)=1 and p.partyname like  '%" & prefixText & "%' "

                    Else
                        strSqlQry = "select  partycode,partyname from  ( select  partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode,othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                        & " where  partyname like '%" & prefixText & "%' order by partyname "

                    End If
                Else
                    strSqlQry = "select  partycode,partyname from  ( select  partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode,othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
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
                        lstAirportMeetTo.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    Next

                End If
            End If


            Return lstAirportMeetTo
        Catch ex As Exception
            Return lstAirportMeetTo
        End Try

    End Function

    Dim ds As DataSet


    ''' <summary>
    ''' btnSearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnSearch.Click
        Try



        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub





    ''' <summary>
    ''' Page_LoadComplete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

    End Sub


    ''' <summary>
    ''' btnLogOut_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            If Not Session("sEditRequestId") Is Nothing Then

                Dim objBLLCommonFuntions As New BLLCommonFuntions
                Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
                If dt.Rows.Count > 0 Then
                    Response.Redirect("MoreServices.aspx")
                Else
                    MessageBox.ShowMessage(Page, MessageType.Info, "You have no booking.")
                End If

            Else
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
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx ::btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' ShowMyBooking
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowMyBooking()
        If Not Session("sEditRequestId") Is Nothing Then
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
            If dt.Rows.Count > 0 Then
                dvMybooking.Visible = True
            Else
                dvMybooking.Visible = False
            End If
        Else
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
        End If

    End Sub
    ''' <summary>
    ''' CheckOldBooking
    ''' </summary>
    ''' <param name="strReqestId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckOldBooking(ByVal strReqestId As String) As Boolean
        Dim bStatus As Boolean = True
        Dim objBLLCommonFuntions As New BLLCommonFuntions
        Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(strReqestId)
        If dt.Rows.Count > 0 Then
            bStatus = False
        End If
        Return bStatus
    End Function



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
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    ''' <summary>
    ''' btnMyAccount_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub


    ''' <summary>
    ''' btnConfirmHome_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
    End Sub
    ''' <summary>
    ''' clearallBookingSessions
    ''' </summary>
    ''' <remarks></remarks>
    Sub clearallBookingSessions()

        Session("sRequestId") = Nothing
        Session("sEditRequestId") = Nothing
        Session("sdtPriceBreakup") = Nothing
        Session("showservices") = Nothing
        Session("ShowGuests") = Nothing
        Session("ShowGuestsDep") = Nothing
        Session("sobjBLLHotelSearch") = Nothing
        Session("sobjBLLHotelSearchActive") = Nothing
        Session("sobjBLLOtherSearchActive") = Nothing
        Session("sobjBLLMASearchActive") = Nothing
        Session("sobjBLLTourSearchActive") = Nothing
        Session("sobjBLLTransferSearchActive") = Nothing
        Session("sdsSpecialEvents") = Nothing
        Session("sdtSelectedFreeFormSpclEvent") = Nothing
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
        Session("sdsHotelRoomTypes") = Nothing
    End Sub



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

    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        'objclsUtilities.FillDropDownListBasedOnNumber(ddlAMAdult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlAMChild, child)


    End Sub

    Private Sub BindAirportMeetDatas()
        Dim strRequestId As String = GetRequestId()
        Dim strMode As String = ""
        Dim strRlineNo As String = "0"
        If Not Request.QueryString("ALineNo") Is Nothing And strRequestId <> "" Then
            strMode = "Edit"
            strRlineNo = Request.QueryString("ALineNo")
        Else
            strMode = "New"
        End If

        Dim dtAirportmeetDetails As DataTable
        dtAirportmeetDetails = BLLAirportMeetFreeFormBooking.GetAirportMeetDatas(strRequestId, strRlineNo, strMode)
        If dtAirportmeetDetails.Rows.Count > 0 Then
            dlAirportMeetResults.DataSource = dtAirportmeetDetails
            dlAirportMeetResults.DataBind()
            Session("sdtAirportmeetDetails") = dtAirportmeetDetails
        End If

    End Sub

    Protected Sub dlAirportMeetResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlAirportMeetResults.ItemDataBound
        Try


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim ddlAirportMeetType As DropDownList = CType(e.Item.FindControl("ddlAirportMeetType"), DropDownList)
                Dim lblAirportMeetDate As Label = CType(e.Item.FindControl("lblAirportMeetDate"), Label)


                Dim btnDelete As Button = CType(e.Item.FindControl("btnDelete"), Button)
                ' btnAddMore.Visible = False
                If Not ViewState("vALineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                    btnAddMore.Visible = False
                    btnDelete.Visible = False
                End If
                If Not Session("sdtAirportmeetDetails") Is Nothing Then
                    Dim dt As DataTable
                    dt = Session("sdtAirportmeetDetails")
                    If dt.Rows.Count = 1 Then
                        btnDelete.Visible = False
                    End If
                End If
                'If Session("sdtAirportmeetDetails") Is Nothing Then'' Modified by Abin on 20180814
                '    btnDelete.Visible = False
                'End If
                Dim lblAirportMeetFrom As Label = CType(e.Item.FindControl("lblAirportMeetFrom"), Label)
                Dim lblAirportMeetTo As Label = CType(e.Item.FindControl("lblAirportMeetTo"), Label)
                Dim dvFlightDetails As HtmlGenericControl = CType(e.Item.FindControl("dvFlightDetails"), HtmlGenericControl)
                Dim dvTransitpart As HtmlGenericControl = CType(e.Item.FindControl("dvTransitpart"), HtmlGenericControl)
                Dim dvUnit As HtmlGenericControl = CType(e.Item.FindControl("dvUnit"), HtmlGenericControl)
                Dim dvACS As HtmlGenericControl = CType(e.Item.FindControl("dvACS"), HtmlGenericControl)
                Dim lblComplSup As Label = CType(e.Item.FindControl("lblComplSup"), Label)
                Dim chkComplSup As CheckBox = CType(e.Item.FindControl("chkComplSup"), CheckBox)
                If lblComplSup.Text = "1" Then
                    chkComplSup.Checked = True
                Else
                    chkComplSup.Checked = False
                End If



                Dim txtAirportMeet As TextBox = CType(e.Item.FindControl("txtAirportMeet"), TextBox)
                Dim txtAirportMeetCode As TextBox = CType(e.Item.FindControl("txtAirportMeetCode"), TextBox)
                Dim strOthtypcode As String = ""
                Dim strRateBasis As String = ""
                Dim AirportMeetsCode() As String
                If txtAirportMeetCode.Text <> "" Then
                    AirportMeetsCode = txtAirportMeetCode.Text.Trim.Split("|")

                    strOthtypcode = AirportMeetsCode(0)
                    If AirportMeetsCode.Length > 1 Then
                        strRateBasis = AirportMeetsCode(1)
                    End If

                End If

                If strRateBasis.Trim = "ACS" Then
                    dvACS.Style.Add("display", "block")
                    dvUnit.Style.Add("display", "none")
                Else
                    dvACS.Style.Add("display", "none")
                    dvUnit.Style.Add("display", "block")
                End If

                If ddlAirportMeetType.SelectedValue = "ARRIVAL" Then
                    dvTransitpart.Style.Add("display", "none")

                ElseIf ddlAirportMeetType.SelectedValue = "DEPARTURE" Then
                    dvTransitpart.Style.Add("display", "none")
                Else
                    dvTransitpart.Style.Add("display", "block")
                End If
                Dim lblAirportMeetType As Label = CType(e.Item.FindControl("lblAirportMeetType"), Label)
                ddlAirportMeetType.SelectedValue = lblAirportMeetType.Text

                ddlAirportMeetType.Attributes.Add("onChange", "javascript:AirportMeetTypeChanged('" + ddlAirportMeetType.ClientID + "','" + lblAirportMeetDate.ClientID + "','" + lblAirportMeetFrom.ClientID + "','" + lblAirportMeetTo.ClientID + "','" + dvFlightDetails.ClientID + "','" + dvTransitpart.ClientID + "','" + dvUnit.ClientID + "','" + dvACS.ClientID + "','" + lblAirportMeetType.ClientID + "')")


                Dim ddlAMAdult As DropDownList = CType(e.Item.FindControl("ddlAMAdult"), DropDownList)
                Dim ddlAMChild As DropDownList = CType(e.Item.FindControl("ddlAMChild"), DropDownList)

                objclsUtilities.FillDropDownListBasedOnNumber(ddlAMAdult, "1000") 'changed by mohamed on 08/11/2018 as per abin changes
                objclsUtilities.FillDropDownListBasedOnNumber(ddlAMChild, "6")

                Dim divAMChild As HtmlGenericControl = CType(e.Item.FindControl("divAMChild"), HtmlGenericControl)
                Dim dvAMChild1 As HtmlGenericControl = CType(e.Item.FindControl("dvAMChild1"), HtmlGenericControl)
                Dim dvAMChild2 As HtmlGenericControl = CType(e.Item.FindControl("dvAMChild2"), HtmlGenericControl)
                Dim dvAMChild3 As HtmlGenericControl = CType(e.Item.FindControl("dvAMChild3"), HtmlGenericControl)
                Dim dvAMChild4 As HtmlGenericControl = CType(e.Item.FindControl("dvAMChild4"), HtmlGenericControl)
                Dim dvAMChild5 As HtmlGenericControl = CType(e.Item.FindControl("dvAMChild5"), HtmlGenericControl)
                Dim dvAMChild6 As HtmlGenericControl = CType(e.Item.FindControl("dvAMChild6"), HtmlGenericControl)
                ddlAMChild.Attributes.Add("onChange", "javascript:AMChildChanged('" + ddlAMChild.ClientID + "','" + divAMChild.ClientID + "','" + dvAMChild1.ClientID + "','" + dvAMChild2.ClientID + "','" + dvAMChild3.ClientID + "','" + dvAMChild4.ClientID + "','" + dvAMChild5.ClientID + "','" + dvAMChild6.ClientID + "')")


                Dim lblAdult As Label = CType(e.Item.FindControl("lblAdult"), Label)
                ddlAMAdult.SelectedValue = lblAdult.Text
                Dim lblChild As Label = CType(e.Item.FindControl("lblChild"), Label)
                ddlAMChild.SelectedValue = lblChild.Text
                Dim lblChildAgeString As Label = CType(e.Item.FindControl("lblChildAgeString"), Label)
                If Val(lblChild.Text.ToString) <> 0 Then
                    ddlAMChild.SelectedValue = lblChild.Text.ToString

                    Dim txtAMChild1 As TextBox = CType(e.Item.FindControl("txtAMChild1"), TextBox)
                    Dim txtAMChild2 As TextBox = CType(e.Item.FindControl("txtAMChild2"), TextBox)
                    Dim txtAMChild3 As TextBox = CType(e.Item.FindControl("txtAMChild3"), TextBox)
                    Dim txtAMChild4 As TextBox = CType(e.Item.FindControl("txtAMChild4"), TextBox)
                    Dim txtAMChild5 As TextBox = CType(e.Item.FindControl("txtAMChild5"), TextBox)
                    Dim txtAMChild6 As TextBox = CType(e.Item.FindControl("txtAMChild6"), TextBox)


                    Dim childages As String = lblChildAgeString.Text.ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtAMChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtAMChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtAMChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtAMChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtAMChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtAMChild6.Text = strChildAges(5)
                    End If

                End If

                ' 
                Dim txtAirportMeets As TextBox = CType(e.Item.FindControl("txtAirportMeet"), TextBox)
                Dim txtAirportMeetsCode As TextBox = CType(e.Item.FindControl("txtAirportMeetCode"), TextBox)
                Dim strAirportMeetCode As String = ""
                If txtAirportMeetsCode.Text <> "" Then
                    Dim strAirportMeetCodeAndRateBasis As String() = txtAirportMeetsCode.Text.Split("|")
                    strAirportMeetCode = strAirportMeetCodeAndRateBasis(0)
                End If


                txtAirportMeets.Text = objclsUtilities.ExecuteQueryReturnStringValue("select othtypname from othtypmast(nolock) where othtypcode='" & strAirportMeetCode & "'")
                Dim ddlAMFlightClass As DropDownList = CType(e.Item.FindControl("ddlAMFlightClass"), DropDownList)
                BindAMflightclass(ddlAMFlightClass)

                Dim ddlTransitFlightClass As DropDownList = CType(e.Item.FindControl("ddlTransitFlightClass"), DropDownList)
                BindAMflightclass(ddlTransitFlightClass)



                Dim txtflight As TextBox = CType(e.Item.FindControl("txtflight"), TextBox)
                Dim txtAMDate As TextBox = CType(e.Item.FindControl("txtAMDate"), TextBox)
                Dim txtTransitFlight As TextBox = CType(e.Item.FindControl("txtTransitFlight"), TextBox)

                Dim AutoCompleteExtender_txtArrivalflight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtArrivalflight"), AjaxControlToolkit.AutoCompleteExtender)
                Dim AutoCompleteExtendertxtTransitFlight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtendertxtTransitFlight"), AjaxControlToolkit.AutoCompleteExtender)
                txtflight.Attributes.Add("onkeydown", "javascript:flightSetContextKey('" + ddlAirportMeetType.ClientID + "','" + txtAMDate.ClientID + "','" + AutoCompleteExtender_txtArrivalflight.ClientID + "')")
                txtTransitFlight.Attributes.Add("onkeydown", "javascript:flightSetContextKey('" + ddlAirportMeetType.ClientID + "','" + txtAMDate.ClientID + "','" + AutoCompleteExtendertxtTransitFlight.ClientID + "')")

                Dim AutoCompleteExtender_txtAirportMeetFrom As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtAirportMeetFrom"), AjaxControlToolkit.AutoCompleteExtender)
                Dim txtAirportMeetFrom As TextBox = CType(e.Item.FindControl("txtAirportMeetFrom"), TextBox)
                txtAirportMeetFrom.Attributes.Add("onkeydown", "javascript:AirportMeetFromContextKey('" + ddlAirportMeetType.ClientID + "','" + AutoCompleteExtender_txtAirportMeetFrom.ClientID + "')")
                Dim txtAirportMeetTo As TextBox = CType(e.Item.FindControl("txtAirportMeetTo"), TextBox)
                Dim AutoCompleteExtender_txtAirportMeetTo As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtAirportMeetTo"), AjaxControlToolkit.AutoCompleteExtender)
                txtAirportMeetTo.Attributes.Add("onkeydown", "javascript:AirportMeetToContextKey('" + ddlAirportMeetType.ClientID + "','" + AutoCompleteExtender_txtAirportMeetTo.ClientID + "')")

                Dim AutoCompleteExtendertxtAirportMeet As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtendertxtAirportMeet"), AjaxControlToolkit.AutoCompleteExtender)
                txtAirportMeets.Attributes.Add("onkeydown", "javascript:AirportMeetSetContextKey('" + ddlAirportMeetType.ClientID + "','" + AutoCompleteExtendertxtAirportMeet.ClientID + "')")

                Dim txtNoOfUnit As TextBox = CType(e.Item.FindControl("txtNoOfUnit"), TextBox)


                Dim txtUnitPrice As TextBox = CType(e.Item.FindControl("txtUnitPrice"), TextBox)
                Dim txtUnitSaleValue As TextBox = CType(e.Item.FindControl("txtUnitSaleValue"), TextBox)
                Dim txtNoOfAddionalPax As TextBox = CType(e.Item.FindControl("txtNoOfAddionalPax"), TextBox)
                Dim txtAdditionalPaxPrice As TextBox = CType(e.Item.FindControl("txtAdditionalPaxPrice"), TextBox)
                Dim txtAdditionalPaxValue As TextBox = CType(e.Item.FindControl("txtAdditionalPaxValue"), TextBox)
                Dim txtTotalSaleValue As TextBox = CType(e.Item.FindControl("txtTotalSaleValue"), TextBox)

                Dim txtCostPricePax As TextBox = CType(e.Item.FindControl("txtCostPricePax"), TextBox)
                Dim txtCostPricePaxTotal As TextBox = CType(e.Item.FindControl("txtCostPricePaxTotal"), TextBox)
                Dim txtAddionalPaxCostPrice As TextBox = CType(e.Item.FindControl("txtAddionalPaxCostPrice"), TextBox)
                Dim txtAddionalPaxCostValue As TextBox = CType(e.Item.FindControl("txtAddionalPaxCostValue"), TextBox)
                Dim txtTotalCostValue As TextBox = CType(e.Item.FindControl("txtTotalCostValue"), TextBox)


                txtNoOfUnit.Attributes.Add("onchange", "javascript:CalculateTotalValuewithCost('" + txtNoOfUnit.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtUnitSaleValue.ClientID + "','" + txtNoOfAddionalPax.ClientID + "','" + txtAdditionalPaxPrice.ClientID + "' ,'" + txtAdditionalPaxValue.ClientID + "','" + txtTotalSaleValue.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "','" + txtAddionalPaxCostPrice.ClientID + "','" + txtAddionalPaxCostValue.ClientID + "','" + txtTotalCostValue.ClientID + "')")
                txtUnitPrice.Attributes.Add("onchange", "javascript:CalculateTotalValuewithCost('" + txtNoOfUnit.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtUnitSaleValue.ClientID + "','" + txtNoOfAddionalPax.ClientID + "','" + txtAdditionalPaxPrice.ClientID + "' ,'" + txtAdditionalPaxValue.ClientID + "','" + txtTotalSaleValue.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "','" + txtAddionalPaxCostPrice.ClientID + "','" + txtAddionalPaxCostValue.ClientID + "','" + txtTotalCostValue.ClientID + "')")
                txtNoOfAddionalPax.Attributes.Add("onchange", "javascript:CalculateTotalValuewithCost('" + txtNoOfUnit.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtUnitSaleValue.ClientID + "','" + txtNoOfAddionalPax.ClientID + "','" + txtAdditionalPaxPrice.ClientID + "' ,'" + txtAdditionalPaxValue.ClientID + "','" + txtTotalSaleValue.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "','" + txtAddionalPaxCostPrice.ClientID + "','" + txtAddionalPaxCostValue.ClientID + "','" + txtTotalCostValue.ClientID + "')")
                txtAdditionalPaxPrice.Attributes.Add("onchange", "javascript:CalculateTotalValuewithCost('" + txtNoOfUnit.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtUnitSaleValue.ClientID + "','" + txtNoOfAddionalPax.ClientID + "','" + txtAdditionalPaxPrice.ClientID + "' ,'" + txtAdditionalPaxValue.ClientID + "','" + txtTotalSaleValue.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "','" + txtAddionalPaxCostPrice.ClientID + "','" + txtAddionalPaxCostValue.ClientID + "','" + txtTotalCostValue.ClientID + "')")
                txtCostPricePax.Attributes.Add("onchange", "javascript:CalculateTotalValuewithCost('" + txtNoOfUnit.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtUnitSaleValue.ClientID + "','" + txtNoOfAddionalPax.ClientID + "','" + txtAdditionalPaxPrice.ClientID + "' ,'" + txtAdditionalPaxValue.ClientID + "','" + txtTotalSaleValue.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "','" + txtAddionalPaxCostPrice.ClientID + "','" + txtAddionalPaxCostValue.ClientID + "','" + txtTotalCostValue.ClientID + "')")
                txtAddionalPaxCostPrice.Attributes.Add("onchange", "javascript:CalculateTotalValuewithCost('" + txtNoOfUnit.ClientID + "','" + txtUnitPrice.ClientID + "','" + txtUnitSaleValue.ClientID + "','" + txtNoOfAddionalPax.ClientID + "','" + txtAdditionalPaxPrice.ClientID + "' ,'" + txtAdditionalPaxValue.ClientID + "','" + txtTotalSaleValue.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "','" + txtAddionalPaxCostPrice.ClientID + "','" + txtAddionalPaxCostValue.ClientID + "','" + txtTotalCostValue.ClientID + "')")

                Dim txtAdultPrice As TextBox = CType(e.Item.FindControl("txtAdultPrice"), TextBox)
                Dim txtAdultSaleValue As TextBox = CType(e.Item.FindControl("txtAdultSaleValue"), TextBox)
                Dim txtChildPrice As TextBox = CType(e.Item.FindControl("txtChildPrice"), TextBox)
                Dim txtChildSaleValue As TextBox = CType(e.Item.FindControl("txtChildSaleValue"), TextBox)
                Dim txtACSTotalSaleValue As TextBox = CType(e.Item.FindControl("txtACSTotalSaleValue"), TextBox)
                Dim txtAdultCostPrice As TextBox = CType(e.Item.FindControl("txtAdultCostPrice"), TextBox)
                Dim txtAdultCostValue As TextBox = CType(e.Item.FindControl("txtAdultCostValue"), TextBox)
                Dim txtChildCostPrice As TextBox = CType(e.Item.FindControl("txtChildCostPrice"), TextBox)
                Dim txtChildCostValue As TextBox = CType(e.Item.FindControl("txtChildCostValue"), TextBox)
                Dim txtACSTotalCostValue As TextBox = CType(e.Item.FindControl("txtACSTotalCostValue"), TextBox)

                txtAdultPrice.Attributes.Add("onchange", "javascript:CalculateACSTotalValuewithCost('" + ddlAMAdult.ClientID + "')")
                txtChildPrice.Attributes.Add("onchange", "javascript:CalculateACSTotalValuewithCost('" + ddlAMAdult.ClientID + "')")
                txtAdultCostPrice.Attributes.Add("onchange", "javascript:CalculateACSTotalValuewithCost('" + ddlAMAdult.ClientID + "')")
                txtChildCostPrice.Attributes.Add("onchange", "javascript:CalculateACSTotalValuewithCost('" + ddlAMAdult.ClientID + "')")
                ddlAMAdult.Attributes.Add("onchange", "javascript:CalculateACSTotalValuewithCost('" + ddlAMAdult.ClientID + "')")

                txtNoOfUnit.TabIndex = 100 * (e.Item.ItemIndex + 1)
                txtUnitPrice.TabIndex = txtNoOfUnit.TabIndex + 1
                txtNoOfAddionalPax.TabIndex = txtUnitPrice.TabIndex + 1
                txtAdditionalPaxPrice.TabIndex = txtNoOfAddionalPax.TabIndex + 1
                txtCostPricePax.TabIndex = txtAdditionalPaxPrice.TabIndex + 1
                txtAddionalPaxCostPrice.TabIndex = txtCostPricePax.TabIndex + 1

                txtAdultPrice.TabIndex = txtAddionalPaxCostPrice.TabIndex + 1
                txtChildPrice.TabIndex = txtAdultPrice.TabIndex + 1
                txtAdultCostPrice.TabIndex = txtChildPrice.TabIndex + 1
                txtChildCostPrice.TabIndex = txtAdultCostPrice.TabIndex + 1

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: dlAirportMeetResults_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnBook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBook.Click
        Try

            Dim requestid As String = ""
            Dim sourcectrycode As String = ""
            Dim agentcode As String = ""
            Dim agentref As String = ""
            Dim columbusref As String = ""
            Dim remarks As String = ""
            Dim strALineno As String = ""
            requestid = GetNewOrExistingRequestId()

            Dim objBLLAirportMeetFreeFormBooking As New BLLAirportMeetFreeFormBooking
            Dim dtt As DataTable
            dtt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
            If dtt.Rows.Count > 0 Then
                objBLLAirportMeetFreeFormBooking.AgentCode = dtt.Rows(0)("agentcode").ToString
                objBLLAirportMeetFreeFormBooking.Div_Code = dtt.Rows(0)("div_code").ToString
                objBLLAirportMeetFreeFormBooking.RequestId = requestid
                objBLLAirportMeetFreeFormBooking.SourcectryCode = dtt.Rows(0)("sourcectrycode").ToString
                objBLLAirportMeetFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtCustomerCode.Text, dtt.Rows(0)("agentcode").ToString)

                objBLLAirportMeetFreeFormBooking.Agentref = dtt.Rows(0)("agentref").ToString
                objBLLAirportMeetFreeFormBooking.ColumbusRef = dtt.Rows(0)("ColumbusRef").ToString
                objBLLAirportMeetFreeFormBooking.Remarks = dtt.Rows(0)("remarks").ToString
                objBLLAirportMeetFreeFormBooking.UserLogged = Session("GlobalUserName")
            Else
                objBLLAirportMeetFreeFormBooking.AgentCode = Session("sAgentCode")
                objBLLAirportMeetFreeFormBooking.Div_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
                objBLLAirportMeetFreeFormBooking.RequestId = requestid ' IIf(requestid = "", objBLLHotelSearch.getrequestid(), requestid)
                objBLLAirportMeetFreeFormBooking.SourcectryCode = txtCountryCode.Text


                objBLLAirportMeetFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtCustomerCode.Text, Session("sAgentCode"))

                objBLLAirportMeetFreeFormBooking.Agentref = ""
                objBLLAirportMeetFreeFormBooking.ColumbusRef = ""
                objBLLAirportMeetFreeFormBooking.Remarks = remarks
                objBLLAirportMeetFreeFormBooking.UserLogged = Session("GlobalUserName")

            End If



            Dim strBuffer As New StringBuilder
            Dim rLineoNoString As String = ""
            Dim dt As DataTable = GetAirportMeetDetailsFromGrid()

            strBuffer.Append("<DocumentElement>")
            For i = 0 To dt.Rows.Count - 1

                Dim strQuery As String = ""
                Dim flighttranid As String = ""
                strQuery = "select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(dt.Rows(i)("flightcode").ToString, String) & "'"
                flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)




                If Session("sRequestId") Is Nothing Then

                    If strALineno = "" Then
                        strALineno = "1"
                    Else
                        strALineno = CType(strALineno, Integer) + 1
                    End If
                ElseIf ViewState("vALineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                    If strALineno = "" Then
                        strALineno = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "AIRPORT_MEET")
                    Else
                        strALineno = CType(strALineno, Integer) + 1
                    End If
                Else
                    strALineno = ViewState("vALineNo")
                End If
                If rLineoNoString <> "" Then
                    rLineoNoString = rLineoNoString + ";" + CType(strALineno, String)
                Else
                    rLineoNoString = strALineno
                End If



                Dim lineno As Integer = CType(Session("sMALineNo"), Integer)
                Dim str As String = ""
                Dim strZero As String = "0"
                strBuffer.Append("<Table>")
                strBuffer.Append(" <alineno>" & strALineno & "</alineno>")
                strBuffer.Append(" <airportmatype>" & dt.Rows(i)("airportmatype").ToString & "</airportmatype>")
                Dim strOthtypcode As String = ""
                Dim strRateBasis As String = ""
                Dim AirportMeetsCode() As String
                If dt.Rows(i)("othtypcode").ToString <> "" Then
                    AirportMeetsCode = dt.Rows(i)("othtypcode").ToString.Trim.Split("|")
                    strOthtypcode = AirportMeetsCode(0)
                    strRateBasis = AirportMeetsCode(1)
                End If

                strBuffer.Append(" <othtypcode>" & strOthtypcode & "</othtypcode>")
                strBuffer.Append(" <airportmadate>" & Format(CType(dt.Rows(i)("airportmadate").ToString, Date), "yyyy/MM/dd") & "</airportmadate>")

                'Dim strQuery As String = ""
                'Dim flighttranid As String = ""
                'strQuery = "select flight_tranid from flightmast where active=1 and flightcode='" & CType(txtArrivalflight.Text, String) & "'"
                'flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                strBuffer.Append(" <flightcode>" & dt.Rows(i)("flightcode").ToString & "</flightcode>")
                strBuffer.Append(" <flight_tranid>" & dt.Rows(i)("flight_tranid").ToString & "</flight_tranid>")
                strBuffer.Append(" <flighttime>" & dt.Rows(i)("flighttime").ToString & "</flighttime>")
                strBuffer.Append(" <flightclscode>" & dt.Rows(i)("flightclscode").ToString & "</flightclscode>")
                strBuffer.Append(" <airportbordercode>" & dt.Rows(i)("airportbordercode").ToString & "</airportbordercode>")
                strBuffer.Append(" <trdepflightcode>" & dt.Rows(i)("trdepflightcode").ToString & "</trdepflightcode>")
                strBuffer.Append(" <trdepflight_tranid>" & dt.Rows(i)("trdepflight_tranid").ToString & "</trdepflight_tranid>")
                strBuffer.Append(" <trdepflighttime>" & dt.Rows(i)("trdepflighttime").ToString & "</trdepflighttime>")
                strBuffer.Append(" <trdepflightclscode>" & dt.Rows(i)("trdepflightclscode").ToString & "</trdepflightclscode>")
                strBuffer.Append(" <trdepairportbordercode>" & dt.Rows(i)("trdepairportbordercode").ToString & "</trdepairportbordercode>")
                strBuffer.Append(" <adults>" & dt.Rows(i)("adults").ToString & "</adults>")
                strBuffer.Append(" <child>" & dt.Rows(i)("child").ToString & "</child>")
                strBuffer.Append(" <childagestring>" & dt.Rows(i)("childagestring").ToString & "</childagestring>")
                strBuffer.Append(" <maxpax>" & dt.Rows(i)("maxpax").ToString & "</maxpax>")
                strBuffer.Append(" <childtocharge>" & dt.Rows(i)("childtocharge").ToString & "</childtocharge>")
                strBuffer.Append(" <units>" & dt.Rows(i)("units").ToString & "</units>")
                strBuffer.Append(" <addlpax>" & dt.Rows(i)("addlpax").ToString & "</addlpax>")
                strBuffer.Append(" <complimentarycust>" & CType(dt.Rows(i)("ComplimentaryCust").ToString, String) & "</complimentarycust>")
                strBuffer.Append(" <adultprice>" & dt.Rows(i)("adultprice").ToString & "</adultprice>")
                strBuffer.Append(" <childprice>" & dt.Rows(i)("childprice").ToString & "</childprice>")
                strBuffer.Append(" <unitprice>" & dt.Rows(i)("unitprice").ToString & "</unitprice>")
                strBuffer.Append(" <addlpaxprice>" & dt.Rows(i)("addlpaxprice").ToString & "</addlpaxprice>")
                strBuffer.Append(" <adultsalevalue>" & dt.Rows(i)("adultsalevalue").ToString & "</adultsalevalue>")
                strBuffer.Append(" <childsalevalue>" & dt.Rows(i)("childsalevalue").ToString & "</childsalevalue>")
                strBuffer.Append(" <unitsalevalue>" & dt.Rows(i)("unitsalevalue").ToString & "</unitsalevalue>")
                strBuffer.Append(" <addlpaxsalevalue>" & dt.Rows(i)("addlpaxsalevalue").ToString & "</addlpaxsalevalue>")
                strBuffer.Append(" <totalsalevalue>" & dt.Rows(i)("totalsalevalue").ToString & "</totalsalevalue>")

                strBuffer.Append(" <wladultprice>" & dt.Rows(i)("wladultprice").ToString & "</wladultprice>")
                strBuffer.Append(" <wlchildprice>" & dt.Rows(i)("wlchildprice").ToString & "</wlchildprice>")
                strBuffer.Append(" <wlunitprice>" & dt.Rows(i)("wlunitprice").ToString & "</wlunitprice>")
                strBuffer.Append(" <wladdlpaxprice>" & dt.Rows(i)("wladdlpaxprice").ToString & "</wladdlpaxprice>")
                strBuffer.Append(" <wladultsalevalue>" & dt.Rows(i)("wladultsalevalue").ToString & "</wladultsalevalue>")

                strBuffer.Append(" <wlchildsalevalue>" & dt.Rows(i)("wlchildsalevalue").ToString & "</wlchildsalevalue>")
                strBuffer.Append(" <wlunitsalevalue>" & dt.Rows(i)("wlunitsalevalue").ToString & "</wlunitsalevalue>")
                strBuffer.Append(" <wladdlpaxsalevalue>" & dt.Rows(i)("wladdlpaxsalevalue").ToString & "</wladdlpaxsalevalue>")
                strBuffer.Append(" <wltotalsalevalue>" & dt.Rows(i)("totalsalevalue").ToString & "</wltotalsalevalue>")

                strBuffer.Append(" <overrideprice>0</overrideprice>")
                strBuffer.Append(" <adultplistcode>" & dt.Rows(i)("adultplistcode").ToString & "</adultplistcode>")
                strBuffer.Append(" <childplistcode>" & dt.Rows(i)("childplistcode").ToString & "</childplistcode>")
                strBuffer.Append(" <unitplistcode>" & dt.Rows(i)("unitplistcode").ToString & "</unitplistcode>")
                strBuffer.Append(" <addlpaxplistcode>" & dt.Rows(i)("addlpaxplistcode").ToString & "</addlpaxplistcode>")


                strBuffer.Append(" <preferredsupplier>" & dt.Rows(i)("preferredsupplier").ToString & "</preferredsupplier>")
                strBuffer.Append(" <adultcprice>" & dt.Rows(i)("adultcprice").ToString & "</adultcprice>")
                strBuffer.Append(" <childcprice>" & dt.Rows(i)("childcprice").ToString & "</childcprice>")
                strBuffer.Append(" <unitcprice>" & dt.Rows(i)("unitcprice").ToString & "</unitcprice>")
                strBuffer.Append(" <addlpaxcprice>" & dt.Rows(i)("addlpaxcprice").ToString & "</addlpaxcprice>")
                strBuffer.Append(" <adultcostvalue>" & dt.Rows(i)("adultcostvalue").ToString & "</adultcostvalue>")
                strBuffer.Append(" <childcostvalue>" & dt.Rows(i)("childcostvalue").ToString & "</childcostvalue>")
                strBuffer.Append(" <unitcostvalue>" & dt.Rows(i)("unitcostvalue").ToString & "</unitcostvalue>")

                strBuffer.Append(" <addlpaxcostvalue>" & dt.Rows(i)("addlpaxcostvalue").ToString & "</addlpaxcostvalue>")
                strBuffer.Append(" <totalcostvalue>" & dt.Rows(i)("totalcostvalue").ToString & "</totalcostvalue>")
                strBuffer.Append(" <adultcplistcode>" & dt.Rows(i)("adultcplistcode").ToString & "</adultcplistcode>")
                strBuffer.Append(" <childcplistcode>" & dt.Rows(i)("childcplistcode").ToString & "</childcplistcode>")
                strBuffer.Append(" <unitcplistcode>" & dt.Rows(i)("unitcplistcode").ToString & "</unitcplistcode>")
                strBuffer.Append(" <addlpaxcplistcode>" & dt.Rows(i)("addlpaxcplistcode").ToString & "</addlpaxcplistcode>")
                strBuffer.Append(" <wlcurrcode>" & dt.Rows(i)("wlcurrcode").ToString & "</wlcurrcode>")
                strBuffer.Append(" <wlconvrate>" & dt.Rows(i)("wlconvrate").ToString & "</wlconvrate>")
                strBuffer.Append(" <wlmarkupper>" & dt.Rows(i)("wlmarkupperc").ToString & "</wlmarkupper>")


                Dim totalcostvalue As Decimal = IIf(CType(dt.Rows(i)("totalcostvalue").ToString, String) = "", "0.00", CType(dt.Rows(i)("totalcostvalue").ToString, String))
                Dim cVATPer As Decimal = IIf(CType(dt.Rows(i)("CostVATPerc").ToString, String) = "", "0.00", CType(dt.Rows(i)("CostVATPerc").ToString, String))

                Dim dCostTaxableValue As Decimal = 0
                Dim dCostVATValue As Decimal = 0


                If CType(dt.Rows(i)("CostWithTax").ToString, String) = "1" Then
                    dCostTaxableValue = (totalcostvalue / (1 + (cVATPer / 100)))
                    dCostVATValue = totalcostvalue - dCostTaxableValue
                Else
                    dCostVATValue = (totalcostvalue * (cVATPer / 100))
                    dCostTaxableValue = totalcostvalue + dCostVATValue
                End If

                Dim totalsalevalue As Decimal = IIf(CType(dt.Rows(i)("totalsalevalue").ToString, String) = "", "0.00", CType(dt.Rows(i)("totalsalevalue").ToString, String))
                Dim PriceVATPer As Decimal = IIf(CType(dt.Rows(i)("PriceVATPerc").ToString, String) = "", "0.00", CType(dt.Rows(i)("PriceVATPerc").ToString, String))

                Dim dPriceTaxableValue As Decimal = 0
                Dim PriceVATValue As Decimal = 0

                '*** Sell Price always include TAX
                'If CType(dt.Rows(i)("PriceWithTax").ToString, String) = "1" Then
                dPriceTaxableValue = (totalsalevalue / (1 + (PriceVATPer / 100)))
                PriceVATValue = totalsalevalue - dPriceTaxableValue
                'Else
                'PriceVATValue = (totalsalevalue * (PriceVATPer / 100))
                'dPriceTaxableValue = totalsalevalue + PriceVATValue
                'End If


                dt.Rows(i)("CostTaxableValue") = Math.Round(dCostTaxableValue, 3)
                dt.Rows(i)("CostVATValue") = Math.Round(dCostVATValue, 3)

                dt.Rows(i)("PriceTaxableValue") = Math.Round(dPriceTaxableValue, 3)
                dt.Rows(i)("PriceVATValue") = Math.Round(PriceVATValue, 3)


                strBuffer.Append(" <CostTaxableValue>" & CType(dt.Rows(i)("CostTaxableValue").ToString, String) & "</CostTaxableValue>")
                strBuffer.Append(" <CostVATValue>" & CType(dt.Rows(i)("CostVATValue").ToString, String) & "</CostVATValue>")
                strBuffer.Append(" <CostVATPerc>" & CType(dt.Rows(i)("CostVATPerc").ToString, String) & "</CostVATPerc>")
                ' strBuffer.Append(" <CostWithTax>" & CType(dt.Rows(i)("CostWithTax").ToString, String) & "</CostWithTax>")
                strBuffer.Append(" <CostWithTax>1</CostWithTax>")
                strBuffer.Append(" <PriceTaxableValue>" & CType(dt.Rows(i)("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                strBuffer.Append(" <PriceVATValue>" & CType(dt.Rows(i)("PriceVATValue").ToString, String) & "</PriceVATValue>")
                strBuffer.Append(" <PriceVATPerc>" & CType(dt.Rows(i)("PriceVATPerc").ToString, String) & "</PriceVATPerc>")
                'strBuffer.Append(" <PriceWithTax>" & CType(dt.Rows(i)("PriceWithTax").ToString, String) & "</PriceWithTax>")
                strBuffer.Append(" <PriceWithTax>1</PriceWithTax>")
                strBuffer.Append("</Table>")

            Next
            strBuffer.Append("</DocumentElement>")



            objBLLAirportMeetFreeFormBooking.AirportMeetXml = strBuffer.ToString
            objBLLAirportMeetFreeFormBooking.RlineNos = rLineoNoString
            objBLLAirportMeetFreeFormBooking.UserLogged = Session("GlobalUserName")
            If objBLLAirportMeetFreeFormBooking.SaveAirportMeetFreeFormBooking() = True Then
                Session("sRequestId") = requestid
                Response.Redirect("MoreServices.aspx")
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Failed to save.")
                Exit Sub
            End If



        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: btnBook_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try


    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim btnDelete As Button = CType(sender, Button)
            Dim dlItem As DataListItem = CType(btnDelete.NamingContainer, DataListItem)
            Dim lbltlineno As Label = CType(dlItem.FindControl("lbltlineno"), Label)
            If Not Session("sdtAirportmeetDetails") Is Nothing Then
                Dim dtAirportmeetDetails As DataTable
                dtAirportmeetDetails = Session("sdtAirportmeetDetails")
                If dtAirportmeetDetails.Rows.Count = 1 Then

                ElseIf dtAirportmeetDetails.Rows.Count > 1 Then
                    For i As Integer = 0 To dtAirportmeetDetails.Rows.Count - 1
                        If dtAirportmeetDetails.Rows(i)("alineno").ToString.Trim = lbltlineno.Text.Trim Then
                            dtAirportmeetDetails.Rows.Remove(dtAirportmeetDetails.Rows(i))
                            Exit For
                        End If
                    Next
                End If


                dlAirportMeetResults.DataSource = dtAirportmeetDetails
                dlAirportMeetResults.DataBind()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: btnDelete_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnAddMore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddMore.Click
        Try

            Dim dtDetails As DataTable = GetAirportMeetDetailsFromGrid()

            If Not dtDetails Is Nothing Then
                dtDetails.Rows.Add()
                Session("sdtAirportmeetDetails") = dtDetails
                dlAirportMeetResults.DataSource = dtDetails
                dlAirportMeetResults.DataBind()

            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("AirportMeetFreeFormBooking.aspx :: btnAddMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Function GetAirportMeetDetailsFromGrid() As DataTable
        Dim dtNewAirportMeetDetails As DataTable = Nothing
        If Not Session("sdtAirportmeetDetails") Is Nothing Then
            Dim dtAirportmeetDetails As DataTable
            dtAirportmeetDetails = Session("sdtAirportmeetDetails")

            dtNewAirportMeetDetails = dtAirportmeetDetails.Clone()
            Dim strTLineno As String = ""
            Dim rLineoNoString As String = ""
            For Each dlItem As DataListItem In dlAirportMeetResults.Items
                dtNewAirportMeetDetails.Rows.Add()


                If Session("sRequestId") Is Nothing Then

                    If strTLineno = "" Then
                        strTLineno = "1"
                    Else
                        strTLineno = CType(strTLineno, Integer) + 1
                    End If
                ElseIf ViewState("vALineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                    If strTLineno = "" Then
                        strTLineno = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "AirportMeet")
                    Else
                        strTLineno = CType(strTLineno, Integer) + 1
                    End If
                Else
                    strTLineno = ViewState("vALineNo")
                End If
                If rLineoNoString <> "" Then
                    rLineoNoString = rLineoNoString + ";" + CType(strTLineno, String)
                Else
                    rLineoNoString = strTLineno
                End If

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("alineno") = strTLineno
                Dim ddlAirportMeetType As DropDownList = CType(dlItem.FindControl("ddlAirportMeetType"), DropDownList)
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportmatype") = ddlAirportMeetType.SelectedValue

                Dim flightcode As TextBox = CType(dlItem.FindControl("flightcode"), TextBox)
                Dim txtAirportMeetFrom As TextBox = CType(dlItem.FindControl("txtAirportMeetFrom"), TextBox)
                Dim txtAirportMeetFromcode As TextBox = CType(dlItem.FindControl("txtAirportMeetFromcode"), TextBox)

                Dim txtTransitDepDate As TextBox = CType(dlItem.FindControl("txtTransitDepDate"), TextBox)
                Dim ddlTransitFlightClass As DropDownList = CType(dlItem.FindControl("ddlTransitFlightClass"), DropDownList)
                Dim txtTransitFlight As TextBox = CType(dlItem.FindControl("txtTransitFlight"), TextBox)
                Dim txtTransitFlightCode As TextBox = CType(dlItem.FindControl("txtTransitFlightCode"), TextBox)
                Dim txtTransitFlightTime As TextBox = CType(dlItem.FindControl("txtTransitFlightTime"), TextBox)



                Dim txtAirportMeetTo As TextBox = CType(dlItem.FindControl("txtAirportMeetTo"), TextBox)
                Dim txtAirportMeetTocode As TextBox = CType(dlItem.FindControl("txtAirportMeetTocode"), TextBox)
                Dim txtflight As TextBox = CType(dlItem.FindControl("txtflight"), TextBox)
                Dim txtflightCode As TextBox = CType(dlItem.FindControl("txtflightCode"), TextBox)
                Dim txtArrivalTime As TextBox = CType(dlItem.FindControl("txtArrivalTime"), TextBox)
                Dim txtAMDate As TextBox = CType(dlItem.FindControl("txtAMDate"), TextBox)

                Dim ddlAMAdult As DropDownList = CType(dlItem.FindControl("ddlAMAdult"), DropDownList)
                Dim ddlAMChild As DropDownList = CType(dlItem.FindControl("ddlAMChild"), DropDownList)

                Dim txtAMChild1 As TextBox = CType(dlItem.FindControl("txtAMChild1"), TextBox)
                Dim txtAMChild2 As TextBox = CType(dlItem.FindControl("txtAMChild2"), TextBox)
                Dim txtAMChild3 As TextBox = CType(dlItem.FindControl("txtAMChild3"), TextBox)
                Dim txtAMChild4 As TextBox = CType(dlItem.FindControl("txtAMChild4"), TextBox)
                Dim txtAMChild5 As TextBox = CType(dlItem.FindControl("txtAMChild5"), TextBox)
                Dim txtAMChild6 As TextBox = CType(dlItem.FindControl("txtAMChild6"), TextBox)





                Dim txtAdultPrice As TextBox = CType(dlItem.FindControl("txtAdultPrice"), TextBox)
                Dim txtNoOfUnit As TextBox = CType(dlItem.FindControl("txtNoOfUnit"), TextBox)
                Dim txtAirportMeetsCode As TextBox = CType(dlItem.FindControl("txtAirportMeetCode"), TextBox)
                Dim txtAirportMeet As TextBox = CType(dlItem.FindControl("txtAirportMeet"), TextBox)

                Dim strOthtypcode As String = ""
                Dim strRateBasis As String = ""
                Dim AirportMeetsCode() As String
                If txtAirportMeetsCode.Text <> "" Then
                    AirportMeetsCode = txtAirportMeetsCode.Text.Trim.Split("|")
                    strOthtypcode = AirportMeetsCode(0)
                    strRateBasis = AirportMeetsCode(1)
                End If

                Dim ddlAMFlightClass As DropDownList = CType(dlItem.FindControl("ddlAMFlightClass"), DropDownList)

                Dim txtUnitPrice As TextBox = CType(dlItem.FindControl("txtUnitPrice"), TextBox)
                Dim txtNoOfAddionalPax As TextBox = CType(dlItem.FindControl("txtNoOfAddionalPax"), TextBox)
                Dim txtAdditionalPaxPrice As TextBox = CType(dlItem.FindControl("txtAdditionalPaxPrice"), TextBox)
                Dim txtCostPricePax As TextBox = CType(dlItem.FindControl("txtCostPricePax"), TextBox)
                Dim txtAddionalPaxCostPrice As TextBox = CType(dlItem.FindControl("txtAddionalPaxCostPrice"), TextBox)

                Dim txtChildPrice As TextBox = CType(dlItem.FindControl("txtChildPrice"), TextBox)
                Dim txtAdultCostPrice As TextBox = CType(dlItem.FindControl("txtAdultCostPrice"), TextBox)
                Dim txtChildCostPrice As TextBox = CType(dlItem.FindControl("txtChildCostPrice"), TextBox)
                Dim chkComplSup As CheckBox = CType(dlItem.FindControl("chkComplSup"), CheckBox)

                If txtAMDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter " & ddlAirportMeetType.SelectedValue & " DATE.")
                    Exit Function
                End If

                If txtAirportMeetFromcode.Text = "" Then
                    Dim lblAirportMeetFrom As Label = CType(dlItem.FindControl("lblAirportMeetFrom"), Label)
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter " & lblAirportMeetFrom.Text & ".")
                    Exit Function
                End If

                If txtAirportMeetTocode.Text = "" And ddlAirportMeetType.SelectedValue = "TRANSIT" Then
                    Dim lblAirportMeetto As Label = CType(dlItem.FindControl("lblAirportMeetto"), Label)
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter " & lblAirportMeetto.Text & ".")
                    Exit Function
                End If
                If ddlAMAdult.SelectedValue = "0" And ddlAMAdult.SelectedValue = "0" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter adult or child.")
                    Exit Function
                End If
                If ddlAMChild.SelectedValue <> 0 Then
                    If ddlAMChild.SelectedValue = 1 Then
                        If txtAMChild1.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If

                    ElseIf ddlAMChild.SelectedValue = 2 Then
                        If txtAMChild1.Text = "" Or txtAMChild2.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlAMChild.SelectedValue = 3 Then
                        If txtAMChild1.Text = "" Or txtAMChild2.Text = "" Or txtAMChild3.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlAMChild.SelectedValue = 4 Then
                        If txtAMChild1.Text = "" Or txtAMChild2.Text = "" Or txtAMChild3.Text = "" Or txtAMChild4.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlAMChild.SelectedValue = 5 Then
                        If txtAMChild1.Text = "" Or txtAMChild2.Text = "" Or txtAMChild3.Text = "" Or txtAMChild4.Text = "" Or txtAMChild5.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlAMChild.SelectedValue = 6 Then
                        If txtAMChild1.Text = "" Or txtAMChild2.Text = "" Or txtAMChild3.Text = "" Or txtAMChild4.Text = "" Or txtAMChild5.Text = "" Or txtAMChild6.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    End If
                End If
                If txtAirportMeetsCode.Text = "" Then

                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter Airport Meet service.")
                    Exit Function
                End If


                If strRateBasis = "Unit" Then
                    If txtNoOfUnit.Text = "" Or Val(txtNoOfUnit.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter no of units.")
                        Exit Function
                    End If
                    If txtUnitPrice.Text = "" Or Val(txtUnitPrice.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter unit price.")
                        Exit Function
                    End If
                    If txtNoOfAddionalPax.Text <> "" And Val(txtNoOfAddionalPax.Text) <> 0 Then
                        If txtAdditionalPaxPrice.Text = "" Or Val(txtAdditionalPaxPrice.Text) = 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter additional pax price.")
                            Exit Function
                        End If
                        If txtAddionalPaxCostPrice.Text = "" Or Val(txtAddionalPaxCostPrice.Text) = 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter addional pax cost price.")
                            Exit Function
                        End If
                    End If

                    If txtCostPricePax.Text = "" Or Val(txtCostPricePax.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter cost unit price.")
                        Exit Function
                    End If

                Else
                    If txtAdultPrice.Text = "" Or Val(txtAdultPrice.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter adult price.")
                        Exit Function
                    End If

                    If txtAdultCostPrice.Text = "" Or Val(txtAdultCostPrice.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter adult cost price.")
                        Exit Function
                    End If
                    If ddlAMChild.SelectedValue > 0 Then
                        If txtChildPrice.Text = "" Or Val(txtChildPrice.Text) = 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child price.")
                            Exit Function
                        End If
                        If txtChildCostPrice.Text = "" Or Val(txtChildCostPrice.Text) = 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child cost price.")
                            Exit Function
                        End If
                    End If


                End If



                Dim strSectorQuery = ""
                If ddlAirportMeetType.SelectedValue = "ARRIVAL" Then

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordercode") = txtAirportMeetFromcode.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordername") = txtAirportMeetFrom.Text
                    strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & txtAirportMeetTocode.Text & "'"

                ElseIf ddlAirportMeetType.SelectedValue = "DEPARTURE" Then
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordercode") = txtAirportMeetTocode.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordername") = txtAirportMeetTo.Text
                    strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & txtAirportMeetFromcode.Text & "'"
                Else
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordercode") = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                End If

                Dim strSectorGroupCode = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                If strSectorGroupCode = "" Then
                    If ddlAirportMeetType.SelectedValue = "ARRIVAL" Then
                        strSectorGroupCode = txtAirportMeetTocode.Text
                    ElseIf ddlAirportMeetType.SelectedValue = "DEPARTURE" Then
                        strSectorGroupCode = txtAirportMeetFromcode.Text
                    End If
                End If
                '  dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("sectorgroupcode") = strSectorGroupCode




                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("othtypcode") = txtAirportMeetsCode.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("othtypname") = txtAirportMeet.Text
                ' dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("shuttle") = "0"

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportmadate") = txtAMDate.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("vairportmadate") = txtAMDate.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("flightcode") = txtflight.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("flight_tranid") = objclsUtilities.ExecuteQueryReturnStringValue("select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(txtflight.Text, String) & "'")
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("flighttime") = txtArrivalTime.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordercode") = txtAirportMeetFromcode.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportbordername") = txtAirportMeetFrom.Text

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("airportmadate") = txtAMDate.Text
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("vairportmadate") = txtAMDate.Text
                If ddlAirportMeetType.SelectedValue = "TRANSIT" Then
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflightcode") = txtTransitFlight.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflight_tranid") = objclsUtilities.ExecuteQueryReturnStringValue("select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(txtTransitFlight.Text, String) & "'")
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflighttime") = txtTransitFlightTime.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepairportbordercode") = txtAirportMeetTocode.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepairportbordername") = txtAirportMeetTo.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflightclscode") = ddlTransitFlightClass.SelectedValue
                Else

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflightcode") = ""
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflight_tranid") = ""
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflighttime") = ""
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepairportbordercode") = ""
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepairportbordername") = ""
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("trdepflightclscode") = ""
                End If



                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adults") = ddlAMAdult.SelectedValue

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("child") = ddlAMChild.SelectedValue
                Dim strChildren As String = ddlAMChild.SelectedValue



                Dim strChild1 As String = txtAMChild1.Text
                Dim strChild2 As String = txtAMChild2.Text
                Dim strChild3 As String = txtAMChild3.Text
                Dim strChild4 As String = txtAMChild4.Text
                Dim strChild5 As String = txtAMChild5.Text
                Dim strChild6 As String = txtAMChild6.Text

                Dim strSourceCountry As String = txtCountry.Text
                Dim strSourceCountryCode As String = txtCountryCode.Text

                Dim ChildAgeString As String = ""
                If strChildren <> "" Then

                    If strChildren = "1" Then

                        ChildAgeString = strChild1
                    ElseIf strChildren = "2" Then

                        ChildAgeString = strChild1 & ";" & strChild2
                    ElseIf strChildren = "3" Then

                        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                    ElseIf strChildren = "4" Then

                        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                    ElseIf strChildren = "5" Then

                        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                    ElseIf strChildren = "6" Then

                        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6

                    End If
                End If
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childagestring") = ChildAgeString

                If strRateBasis = "Unit" Then


                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultsalevalue") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childsalevalue") = "0"

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultcprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childcprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultcostvalue") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childcostvalue") = "0"


                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("units") = txtNoOfUnit.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitprice") = txtUnitPrice.Text
                    Dim salevalue As Double = CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("units").ToString), Integer) * CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitprice").ToString, Decimal)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitsalevalue") = salevalue.ToString
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpax") = IIf(txtNoOfAddionalPax.Text = "", "0", txtNoOfAddionalPax.Text)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxprice") = IIf(txtAdditionalPaxPrice.Text = "", "0", txtAdditionalPaxPrice.Text)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxsalevalue") = (CType(Val(IIf(txtNoOfAddionalPax.Text = "", "0", txtNoOfAddionalPax.Text)), Decimal) * CType(IIf(txtAdditionalPaxPrice.Text = "", "0", txtAdditionalPaxPrice.Text), Decimal)).ToString
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("totalsalevalue") = (CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitsalevalue").ToString), Decimal) + CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxsalevalue").ToString, Decimal)).ToString

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitcprice") = txtCostPricePax.Text
                    Dim costvalue As Double = CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("units").ToString), Integer) * CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitcprice").ToString, Decimal)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitcostvalue") = costvalue.ToString
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxcprice") = IIf(txtAddionalPaxCostPrice.Text = "", "0", txtAddionalPaxCostPrice.Text)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxcostvalue") = (CType(Val(IIf(txtNoOfAddionalPax.Text = "", "0", txtNoOfAddionalPax.Text)), Decimal) * CType(IIf(txtAddionalPaxCostPrice.Text = "", "0", txtAddionalPaxCostPrice.Text), Decimal)).ToString
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("totalcostvalue") = (CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitcostvalue").ToString), Decimal) + CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxcostvalue").ToString, Decimal)).ToString





                Else

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("units") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitprice") = "0"

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitsalevalue") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpax") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxsalevalue") = "0"


                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitcprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitcostvalue") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxcprice") = "0"
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxcostvalue") = "0"


                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultprice") = txtAdultPrice.Text
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childprice") = IIf(txtChildPrice.Text = "", "0", txtChildPrice.Text)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultsalevalue") = CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adults").ToString), Integer) * CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultprice").ToString, Decimal)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childsalevalue") = CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("child").ToString), Integer) * CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childprice").ToString, Decimal)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("totalsalevalue") = (CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultsalevalue").ToString), Decimal) + CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childsalevalue").ToString, Decimal)).ToString

                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultcprice") = txtAdultCostPrice.Text 'txtAdultPrice.Text '' modified by abin on 20181024 :: ID141
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childcprice") = IIf(txtChildCostPrice.Text = "", "0", txtChildCostPrice.Text)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultcostvalue") = CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adults").ToString), Integer) * CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultcprice").ToString, Decimal)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childcostvalue") = CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("child").ToString), Integer) * CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childcprice").ToString, Decimal)
                    dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("totalcostvalue") = (CType(Val(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultcostvalue").ToString), Decimal) + CType(dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childcostvalue").ToString, Decimal)).ToString




                End If


                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wlunitprice") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitprice").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wlunitsalevalue") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("unitsalevalue").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wladdlpaxprice") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxprice").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wladdlpaxsalevalue") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("addlpaxsalevalue").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wltotalsalevalue") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("totalsalevalue").ToString

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wladultprice") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultprice").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wlchildprice") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childprice").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wladultsalevalue") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("adultsalevalue").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wlchildsalevalue") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("childsalevalue").ToString
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wltotalsalevalue") = dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("totalsalevalue").ToString





                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("complimentarycust") = IIf(chkComplSup.Checked = True, "1", "0")
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("overrideprice") = "0"

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("flightclscode") = ddlAMFlightClass.SelectedValue


                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("preferredsupplier") = "0"



                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("CostTaxableValue") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("CostVATValue") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("PriceTaxableValue") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("PriceVATValue") = "0"

                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wlmarkupperc") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("PriceVATPerc") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("CostWithTAX") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("CostVATPerc") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("PriceWithTAX") = "0"
                dtNewAirportMeetDetails.Rows(dlItem.ItemIndex)("wlconvrate") = "0"



            Next
        End If
        Return dtNewAirportMeetDetails
    End Function

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Not Session("sRequestId") Is Nothing Then
            Dim dtBookingHeader As DataTable
            dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dtBookingHeader.Rows.Count > 0 Then
                txtCountry.Text = dtBookingHeader.Rows(0)("sourcectryname").ToString
                txtCountryCode.Text = dtBookingHeader.Rows(0)("sourcectrycode").ToString
                txtCustomer.Text = dtBookingHeader.Rows(0)("agentname").ToString
                txtCustomerCode.Text = dtBookingHeader.Rows(0)("agentcode").ToString
                If Session("sLoginType") = "RO" Then
                    txtCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtCustomer.Enabled = False
                    AutoCompleteExtender_txtCountry.Enabled = False
                End If
            End If
        End If
        Session("sdtAirportmeetDetails") = Nothing
        BindAirportMeetDatas()
    End Sub
End Class
