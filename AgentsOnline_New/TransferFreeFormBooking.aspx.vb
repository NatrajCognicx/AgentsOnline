Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Linq
Partial Class TransferFreeFormBooking
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
    Dim BLLTransferFreeFormBooking As New BLLTransferFreeFormBooking
    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
                objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
        objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)

        LoadFields()



        ShowMyBooking()
        If Not Request.QueryString("TLineNo") Is Nothing Then
            ViewState("vTLineNo") = Request.QueryString("TLineNo")
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
        Session("sdtTrfDetails") = Nothing
        BindTransferDatas()



        If Not Request.QueryString("TLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then



        End If


        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If

    End Sub
    Private Sub BindTrfflightclass(ByVal ddlTrfFlightClass As DropDownList)
        Dim strQuery As String = ""
        strQuery = "select flightclscode,flightclsname from flightclsmast(nolock) where active=1"
        objclsUtilities.FillDropDownList(ddlTrfFlightClass, strQuery, "flightclscode", "flightclsname", True, "--")

        ddlTrfFlightClass.SelectedIndex = 2

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
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                lblPhoneNo.Text = objDataTable.Rows(0)("tel1").ToString 
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

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetTransferList(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If

            strSqlQry = "select case when options='SIC' then  othcatcode+'|SIC' else  othcatcode+'|NA' end othcatcode,othcatname from othcatmast(nolock) where othgrpcode='TRFS' and active=1  and othcatcode not in (select options_available from reservation_parameters where param_id=5202) and othcatname like  '%" & prefixText & "%'  order by othcatname  "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othcatname").ToString(), myDS.Tables(0).Rows(i)("othcatcode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
  <System.Web.Services.WebMethod()> _
    Public Shared Function GetChildTransferList(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If

            strSqlQry = "select case when options='SIC' then  othcatcode+'|SIC' else  othcatcode+'|NA' end othcatcode,case when othcatcode=(select option_selected from reservation_parameters where param_id=5202) then 'SIC Transfer' else othcatname end othcatname from othcatmast(nolock) where othgrpcode='TRFS' and active=1  and othcatcode  in (select options_available from reservation_parameters where param_id=5202) and othcatname like  '%" & prefixText & "%'  order by othcatname  "

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othcatname").ToString(), myDS.Tables(0).Rows(i)("othcatcode").ToString()))
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
        Dim strTransferType As String = ""
        If contextKey <> "" Then

            Dim str As String() = contextKey.Split("|")
            strTransferType = str(0)
            strDate = str(1)
        End If


        Dim lstflight As New List(Of String)



        Try

            Dim strRequestId As String = ""
            strRequestId = GetRequestId()

            If prefixText = " " Then
                prefixText = ""
            End If


            If strTransferType = "ARRIVAL" Then
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
            ElseIf strTransferType = "DEPARTURE" Then
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
            ElseIf strTransferType = "INTERHOTEL" Then
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
    Public Shared Function GetTransferFrom(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim lstTransferFrom As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If


            If contextKey = "ARRIVAL" Then
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
                        lstTransferFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                    Next

                End If
            ElseIf contextKey = "DEPARTURE" Then
                If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                    Dim dt As New DataTable
                    dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                    If dt.Rows(0)("cnt") > 1 Then
                        strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b cross apply  (select min(b1.checkin) checkin from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                            & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "'  and  isnull(b.shiftfrom,0)=0  and p.partyname like  '%" & prefixText & "%' "
                    Else
                        strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                            & " where  partyname like '%" & prefixText & "%' order by partyname "
                        'Changed by mohamed on 11/09/2018 '& " inner join partymast p on b.partycode=p.partycode inner join sectormaster st on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "'  and  isnull(b.shiftfrom,0)=0  and p.partyname like  '%" & prefixText & "%' "
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
                        lstTransferFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                        'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                    Next

                End If
            ElseIf contextKey = "INTERHOTEL" Then

                If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                    Dim dt As New DataTable
                    Dim dt1 As New DataTable
                    dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                    If dt.Rows(0)("cnt") > 1 Then

                        dt1 = clsUtilities.GetSharedDataFromDataTable(" select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                               & " inner join partymast p(nolock) on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and b.checkout<>m.checkout and isnull(b.shiftfrom,0)=1 ")

                        If dt1.Rows.Count = 0 Then
                            strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                        & " where  partyname like '%" & prefixText & "%' order by partyname "
                        Else
                            strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                 & " inner join partymast p(nolock) on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and b.checkout<>m.checkout and isnull(b.shiftfrom,0)=1 and p.partyname like  '%" & prefixText & "%' "

                        End If

                    ElseIf dt.Rows(0)("cnt") = 1 Then

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
                        lstTransferFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    Next

                End If

            End If




            Return lstTransferFrom
        Catch ex As Exception
            Return lstTransferFrom
        End Try

    End Function


    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetTransferTo(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim lstTransferTo As New List(Of String)
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
                        strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                            & " inner join partymast p on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and isnull(b.shiftto,0)=0  and p.partyname like  '%" & prefixText & "%' "
                        'changed by mohamed on 11/09/2018 '& " inner join partymast p on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and isnull(b.shiftto,0)=0  and p.partyname like  '%" & prefixText & "%' "
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
                ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)

                If myDS.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        lstTransferTo.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
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
                        lstTransferTo.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))

                    Next

                End If
            ElseIf contextKey = "INTERHOTEL" Then

                If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                    Dim dt As New DataTable
                    Dim dt1 As New DataTable
                    dt = clsUtilities.GetSharedDataFromDataTable("select count(*)cnt from view_booking_hotel_prearr b(nolock) where requestid='" & HttpContext.Current.Session("sRequestId") & "'")
                    If dt.Rows(0)("cnt") > 1 Then

                        dt1 = clsUtilities.GetSharedDataFromDataTable(" select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                               & " inner join partymast p(nolock) on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and b.checkout<>m.checkout and isnull(b.shiftto,0)=1 ")

                        If dt1.Rows.Count = 0 Then
                            strSqlQry = "select  partycode,partyname from  ( select  partycode+';P' partycode,partyname from partymast(nolock)  where active=1  union all  select  othtypcode+';O',othtypname from  othtypmast(nolock)  where othgrpcode in (select option_selected from reservation_parameters where param_id=1001)) rs " _
                        & " where  partyname like '%" & prefixText & "%' order by partyname "
                        Else

                            strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                         & " inner join partymast p(nolock) on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and b.checkout<>m.checkout and isnull(b.shiftto,0)=1 and p.partyname like  '%" & prefixText & "%' "

                        End If

                    ElseIf dt.Rows(0)("cnt") = 1 Then

                        strSqlQry = "select b.partycode+';P' partycode,p.partyname from view_booking_hotel_prearr b(nolock) cross apply (select max(b1.checkout) checkout from view_booking_hotel_prearr b1(nolock) where b1.requestid='" & HttpContext.Current.Session("sRequestId") & "') m " _
                     & " inner join partymast p(nolock) on b.partycode=p.partycode inner join sectormaster st(nolock) on p.sectorcode=st.sectorcode  where b.partycode=p.partycode and b.requestid='" & HttpContext.Current.Session("sRequestId") & "' and b.checkout<>m.checkout and isnull(b.shiftto,0)=1 and p.partyname like  '%" & prefixText & "%' "

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
                        lstTransferTo.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("partyname").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString()))
                    Next

                End If
            End If


            Return lstTransferTo
        Catch ex As Exception
            Return lstTransferTo
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
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx ::btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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

        'objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfAdult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfChild, child)


    End Sub

    Private Sub BindTransferDatas()
        Dim strRequestId As String = GetRequestId()
        Dim strMode As String = ""
        Dim strRlineNo As String = "0"
        If Not Request.QueryString("TLineNo") Is Nothing And strRequestId <> "" Then
            strMode = "Edit"
            strRlineNo = Request.QueryString("TLineNo")
        Else
            strMode = "New"
        End If

        Dim dtTrfDetails As DataTable
        dtTrfDetails = BLLTransferFreeFormBooking.GetTransferDatas(strRequestId, strRlineNo, strMode)
        If dtTrfDetails.Rows.Count > 0 Then
            dlTransferResults.DataSource = dtTrfDetails
            dlTransferResults.DataBind()
            Session("sdtTrfDetails") = dtTrfDetails
        End If

    End Sub

    Protected Sub dlTransferResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlTransferResults.ItemDataBound
        Try


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim ddlTransferType As DropDownList = CType(e.Item.FindControl("ddlTransferType"), DropDownList)
                Dim lblTransferDate As Label = CType(e.Item.FindControl("lblTransferDate"), Label)



                Dim btnDelete As Button = CType(e.Item.FindControl("btnDelete"), Button)

                If Not ViewState("vTLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                    btnAddMore.Visible = False
                    btnDelete.Visible = False
                End If
                If Not Session("sdtTrfDetails") Is Nothing Then
                    Dim dt As DataTable
                    dt = Session("sdtTrfDetails")
                    If dt.Rows.Count = 1 Then
                        btnDelete.Visible = False
                    End If
                End If
                btnAddMore.Visible = False '' Modified by Abin on 20180814
                'If Session("sdtTrfDetails") Is Nothing Then
                '    btnDelete.Visible = False
                'End If
                Dim lblTransferFrom As Label = CType(e.Item.FindControl("lblTransferFrom"), Label)
                Dim lblTransferTo As Label = CType(e.Item.FindControl("lblTransferTo"), Label)
                Dim dvFlightDetails As HtmlGenericControl = CType(e.Item.FindControl("dvFlightDetails"), HtmlGenericControl)
                ddlTransferType.Attributes.Add("onChange", "javascript:TransferTypeChanged('" + ddlTransferType.ClientID + "','" + lblTransferDate.ClientID + "','" + lblTransferFrom.ClientID + "','" + lblTransferTo.ClientID + "','" + dvFlightDetails.ClientID + "')")

                Dim lbltransferType As Label = CType(e.Item.FindControl("lbltransferType"), Label)
                ddlTransferType.SelectedValue = lbltransferType.Text.Trim

                If lbltransferType.Text.Trim = "INTERHOTEL" Then
                    dvFlightDetails.Style.Add("display", "none")
                End If

                Dim ddlTrfAdult As DropDownList = CType(e.Item.FindControl("ddlTrfAdult"), DropDownList)
                Dim ddlTrfChild As DropDownList = CType(e.Item.FindControl("ddlTrfChild"), DropDownList)

                objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfAdult, "1000")
                objclsUtilities.FillDropDownListBasedOnNumber(ddlTrfChild, "6")

                Dim divTrfchild As HtmlGenericControl = CType(e.Item.FindControl("divTrfchild"), HtmlGenericControl)
                Dim dvTrfChild1 As HtmlGenericControl = CType(e.Item.FindControl("dvTrfChild1"), HtmlGenericControl)
                Dim dvTrfChild2 As HtmlGenericControl = CType(e.Item.FindControl("dvTrfChild2"), HtmlGenericControl)
                Dim dvTrfChild3 As HtmlGenericControl = CType(e.Item.FindControl("dvTrfChild3"), HtmlGenericControl)
                Dim dvTrfChild4 As HtmlGenericControl = CType(e.Item.FindControl("dvTrfChild4"), HtmlGenericControl)
                Dim dvTrfChild5 As HtmlGenericControl = CType(e.Item.FindControl("dvTrfChild5"), HtmlGenericControl)
                Dim dvTrfChild6 As HtmlGenericControl = CType(e.Item.FindControl("dvTrfChild6"), HtmlGenericControl)
                ddlTrfChild.Attributes.Add("onChange", "javascript:TrfChildChanged('" + ddlTrfChild.ClientID + "','" + divTrfchild.ClientID + "','" + dvTrfChild1.ClientID + "','" + dvTrfChild2.ClientID + "','" + dvTrfChild3.ClientID + "','" + dvTrfChild4.ClientID + "','" + dvTrfChild5.ClientID + "','" + dvTrfChild6.ClientID + "')")
                ddlTrfAdult.Attributes.Add("onChange", "javascript:TrfAdultChanged('" + ddlTrfAdult.ClientID + "')")



                Dim lblAdult As Label = CType(e.Item.FindControl("lblAdult"), Label)
                ddlTrfAdult.SelectedValue = lblAdult.Text
                Dim lblChild As Label = CType(e.Item.FindControl("lblChild"), Label)
                ddlTrfChild.SelectedValue = lblChild.Text
                Dim lblChildAgeString As Label = CType(e.Item.FindControl("lblChildAgeString"), Label)
                If Val(lblChild.Text.ToString) <> 0 Then
                    ddlTrfChild.SelectedValue = lblChild.Text.ToString

                    Dim txtTrfChild1 As TextBox = CType(e.Item.FindControl("txtTrfChild1"), TextBox)
                    Dim txtTrfChild2 As TextBox = CType(e.Item.FindControl("txtTrfChild2"), TextBox)
                    Dim txtTrfChild3 As TextBox = CType(e.Item.FindControl("txtTrfChild3"), TextBox)
                    Dim txtTrfChild4 As TextBox = CType(e.Item.FindControl("txtTrfChild4"), TextBox)
                    Dim txtTrfChild5 As TextBox = CType(e.Item.FindControl("txtTrfChild5"), TextBox)
                    Dim txtTrfChild6 As TextBox = CType(e.Item.FindControl("txtTrfChild6"), TextBox)


                    Dim childages As String = lblChildAgeString.Text.ToString.ToString.Replace(",", ";")
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

                End If
                Dim lblComplSup As Label = CType(e.Item.FindControl("lblComplSup"), Label)
                Dim chkComplSup As CheckBox = CType(e.Item.FindControl("chkComplSup"), CheckBox)
                ' IIf(lblComplSup.Text = "1", chkComplSup.Checked = True, chkComplSup.Checked = False)
                If lblComplSup.Text = "1" Then
                    chkComplSup.Checked = True
                Else
                    chkComplSup.Checked = False
                End If

                Dim txtTransfers As TextBox = CType(e.Item.FindControl("txtTransfers"), TextBox)
                Dim txtTransfersCode As TextBox = CType(e.Item.FindControl("txtTransfersCode"), TextBox)
                Dim strTransferCodeWithOption As String = txtTransfersCode.Text
                Dim strTransferCode As String = ""
                Dim strOptions As String = ""
                If strTransferCodeWithOption <> "" Then
                    Dim strTransfers As String() = strTransferCodeWithOption.Split("|")
                    strTransferCode = strTransfers(0)
                    If strTransfers.Length > 1 Then
                        strOptions = strTransfers(1)
                    End If
                End If

                txtTransfers.Text = objclsUtilities.ExecuteQueryReturnStringValue("select othcatname from othcatmast(nolock) where othgrpcode='TRFS' and active=1 and othcatcode='" & strTransferCode & "'")

                Dim txtChildTransfers As TextBox = CType(e.Item.FindControl("txtChildTransfers"), TextBox)
                Dim txtChildTransfersCode As TextBox = CType(e.Item.FindControl("txtChildTransfersCode"), TextBox)

                Dim strChildTransfer As String = objclsUtilities.ExecuteQueryReturnStringValue("select othcatcode +'|'+ othcatname othcatname from othcatmast(nolock) where othgrpcode='TRFS' and active=1  and othcatcode in (select options_available from reservation_parameters where param_id=5202) ")

                If strChildTransfer <> "" Then
                    Dim strChildTransfers As String() = strChildTransfer.Split("|")
                    txtChildTransfersCode.Text = strChildTransfers(0)
                    txtChildTransfers.Text = strChildTransfers(1)
                End If

                Dim dvSICChild As HtmlGenericControl = CType(e.Item.FindControl("dvSICChild"), HtmlGenericControl)
                dvSICChild.Style.Add("display", "none")


                Dim ddlTrfFlightClass As DropDownList = CType(e.Item.FindControl("ddlTrfFlightClass"), DropDownList)
                BindTrfflightclass(ddlTrfFlightClass)

                Dim txtflight As TextBox = CType(e.Item.FindControl("txtflight"), TextBox)
                Dim txtTrfDate As TextBox = CType(e.Item.FindControl("txtTrfDate"), TextBox)
                Dim AutoCompleteExtender_txtArrivalflight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtArrivalflight"), AjaxControlToolkit.AutoCompleteExtender)

                txtflight.Attributes.Add("onkeydown", "javascript:flightSetContextKey('" + ddlTransferType.ClientID + "','" + txtTrfDate.ClientID + "','" + AutoCompleteExtender_txtArrivalflight.ClientID + "')")
                Dim AutoCompleteExtender_txtTransferFrom As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtTransferFrom"), AjaxControlToolkit.AutoCompleteExtender)
                Dim txtTransferFrom As TextBox = CType(e.Item.FindControl("txtTransferFrom"), TextBox)
                txtTransferFrom.Attributes.Add("onkeydown", "javascript:TransferFromContextKey('" + ddlTransferType.ClientID + "','" + AutoCompleteExtender_txtTransferFrom.ClientID + "')")
                Dim txtTransferTo As TextBox = CType(e.Item.FindControl("txtTransferTo"), TextBox)
                Dim AutoCompleteExtender_txtTransferTo As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtTransferTo"), AjaxControlToolkit.AutoCompleteExtender)
                txtTransferTo.Attributes.Add("onkeydown", "javascript:TransferToContextKey('" + ddlTransferType.ClientID + "','" + AutoCompleteExtender_txtTransferTo.ClientID + "')")
                Dim txtNoOfUnit As TextBox = CType(e.Item.FindControl("txtNoOfUnit"), TextBox)
                Dim txtAdultPrice As TextBox = CType(e.Item.FindControl("txtAdultPrice"), TextBox)
                Dim txtTotal As TextBox = CType(e.Item.FindControl("txtTotal"), TextBox)
                Dim txtCostPricePax As TextBox = CType(e.Item.FindControl("txtCostPricePax"), TextBox)
                Dim txtCostPricePaxTotal As TextBox = CType(e.Item.FindControl("txtCostPricePaxTotal"), TextBox)
                txtNoOfUnit.Attributes.Add("onchange", "javascript:calculatetotalvaluewithcost('" + txtNoOfUnit.ClientID + "','" + txtAdultPrice.ClientID + "','" + txtTotal.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "')")
                txtCostPricePax.Attributes.Add("onchange", "javascript:calculatetotalvalue('" + txtNoOfUnit.ClientID + "','" + txtCostPricePax.ClientID + "','" + txtCostPricePaxTotal.ClientID + "')")
                txtAdultPrice.Attributes.Add("onchange", "javascript:calculatetotalvalue('" + txtNoOfUnit.ClientID + "','" + txtAdultPrice.ClientID + "','" + txtTotal.ClientID + "')")

                Dim txtChildNoOfUnit As TextBox = CType(e.Item.FindControl("txtChildNoOfUnit"), TextBox)
                Dim txtChildPrice As TextBox = CType(e.Item.FindControl("txtChildPrice"), TextBox)
                Dim txtChildTotal As TextBox = CType(e.Item.FindControl("txtChildTotal"), TextBox)
                Dim txtChildCostPricePax As TextBox = CType(e.Item.FindControl("txtChildCostPricePax"), TextBox)
                Dim txtChildCostPricePaxTotal As TextBox = CType(e.Item.FindControl("txtChildCostPricePaxTotal"), TextBox)

                txtChildNoOfUnit.Attributes.Add("onchange", "javascript:calculatetotalvaluewithcost('" + txtChildNoOfUnit.ClientID + "','" + txtChildPrice.ClientID + "','" + txtChildTotal.ClientID + "','" + txtChildCostPricePax.ClientID + "','" + txtChildCostPricePaxTotal.ClientID + "')")
                txtChildPrice.Attributes.Add("onchange", "javascript:calculatetotalvaluewithcost('" + txtChildNoOfUnit.ClientID + "','" + txtChildPrice.ClientID + "','" + txtChildTotal.ClientID + "','" + txtChildCostPricePax.ClientID + "','" + txtChildCostPricePaxTotal.ClientID + "')")
                txtChildCostPricePax.Attributes.Add("onchange", "javascript:calculatetotalvaluewithcost('" + txtChildNoOfUnit.ClientID + "','" + txtChildPrice.ClientID + "','" + txtChildTotal.ClientID + "','" + txtChildCostPricePax.ClientID + "','" + txtChildCostPricePaxTotal.ClientID + "')")


                txtTransfers.TabIndex = 100 * (e.Item.ItemIndex + 1)
                txtAdultPrice.TabIndex = txtTransfers.TabIndex + 1
                txtCostPricePax.TabIndex = txtAdultPrice.TabIndex + 1


                txtChildTransfers.TabIndex = txtCostPricePax.TabIndex + 1
                txtChildPrice.TabIndex = txtChildTransfers.TabIndex + 1
                txtChildCostPricePax.TabIndex = txtChildPrice.TabIndex + 1


                'changed by mohamed on 28/08/2018
                Dim chkAllowZeroCost As CheckBox = CType(e.Item.FindControl("chkAllowZeroCost"), CheckBox)
                If Val(txtAdultPrice.Text) <> 0 And Val(txtCostPricePax.Text) = 0 Then
                    chkAllowZeroCost.Checked = True
                End If


            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: dlTransferResults_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            Dim strTLineno As String = ""
            requestid = GetNewOrExistingRequestId()

            Dim objBLLTransferFreeFormBooking As New BLLTransferFreeFormBooking
            Dim dtt As DataTable
            dtt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)
            If dtt.Rows.Count > 0 Then
                objBLLTransferFreeFormBooking.AgentCode = dtt.Rows(0)("agentcode").ToString
                objBLLTransferFreeFormBooking.Div_Code = dtt.Rows(0)("div_code").ToString
                objBLLTransferFreeFormBooking.RequestId = requestid
                objBLLTransferFreeFormBooking.SourcectryCode = dtt.Rows(0)("sourcectrycode").ToString
                objBLLTransferFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtCustomerCode.Text, dtt.Rows(0)("agentcode").ToString)

                objBLLTransferFreeFormBooking.Agentref = dtt.Rows(0)("agentref").ToString
                objBLLTransferFreeFormBooking.ColumbusRef = dtt.Rows(0)("ColumbusRef").ToString
                objBLLTransferFreeFormBooking.Remarks = dtt.Rows(0)("remarks").ToString
                objBLLTransferFreeFormBooking.UserLogged = Session("GlobalUserName")
            Else
                objBLLTransferFreeFormBooking.AgentCode = Session("sAgentCode")
                objBLLTransferFreeFormBooking.Div_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
                objBLLTransferFreeFormBooking.RequestId = requestid ' IIf(requestid = "", objBLLHotelSearch.getrequestid(), requestid)
                objBLLTransferFreeFormBooking.SourcectryCode = txtCountryCode.Text


                objBLLTransferFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtCustomerCode.Text, Session("sAgentCode"))

                objBLLTransferFreeFormBooking.Agentref = ""
                objBLLTransferFreeFormBooking.ColumbusRef = ""
                objBLLTransferFreeFormBooking.Remarks = remarks
                objBLLTransferFreeFormBooking.UserLogged = Session("GlobalUserName")

            End If



            Dim strBuffer As New StringBuilder
            Dim rLineoNoString As String = ""
            Dim dt As DataTable = GetTransferDetailsFromGrid()

            strBuffer.Append("<DocumentElement>")
            For i = 0 To dt.Rows.Count - 1

                Dim strQuery As String = ""
                Dim flighttranid As String = ""
                strQuery = "select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(dt.Rows(i)("flightcode").ToString, String) & "'"
                flighttranid = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)




                If Session("sRequestId") Is Nothing Then

                    If strTLineno = "" Then
                        strTLineno = "1"
                    Else
                        strTLineno = CType(strTLineno, Integer) + 1
                    End If
                ElseIf ViewState("vTLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                    If strTLineno = "" Then
                        strTLineno = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "TRANSFER")
                    Else
                        strTLineno = CType(strTLineno, Integer) + 1
                    End If
                Else
                    strTLineno = ViewState("vTLineNo")
                End If
                If rLineoNoString <> "" Then
                    rLineoNoString = rLineoNoString + ";" + CType(strTLineno, String)
                Else
                    rLineoNoString = strTLineno
                End If

                Dim lineno As Integer = strTLineno ' CType(Session("tlineno"), Integer)

                Dim salevalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitprice").ToString, Decimal)
                Dim costvalue As Double = CType(Val(dt.Rows(i)("units").ToString), Integer) * CType(dt.Rows(i)("unitcprice").ToString, Decimal)

                strBuffer.Append("<Table>")

                strBuffer.Append(" <tlineno>" & strTLineno & "</tlineno>")

                strBuffer.Append(" <transfertype> " & CType(dt.Rows(i)("transfertype").ToString.Trim, String) & " </transfertype>")
                strBuffer.Append(" <airportbordercode>" & CType(dt.Rows(i)("airportbordercode").ToString, String) & "</airportbordercode>")
                strBuffer.Append(" <sectorgroupcode>" & CType(dt.Rows(i)("sectorgroupcode").ToString, String) & "</sectorgroupcode>")

                Dim strTransferCodeWithOption As String = dt.Rows(i)("cartypecode").ToString
                Dim strTransferCode As String = ""
                Dim strOptions As String = ""
                If strTransferCodeWithOption <> "" Then
                    Dim strTransfers As String() = strTransferCodeWithOption.Split("|")
                    strTransferCode = strTransfers(0)
                    If strTransfers.Length > 1 Then
                        strOptions = strTransfers(1)
                    End If

                End If

                strBuffer.Append(" <cartypecode>" & strTransferCode & "</cartypecode>")
                strBuffer.Append(" <shuttle>" & CType(dt.Rows(i)("shuttle").ToString, Integer) & "</shuttle>")


                Dim strFromDate As String = dt.Rows(i)("transferdate").ToString
                If strFromDate <> "" Then
                    Dim strDates As String() = strFromDate.Split("/")
                    strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If




                strBuffer.Append(" <transferdate>" & Format(CType(dt.Rows(i)("transferdate").ToString, Date), "yyyy/MM/dd") & "</transferdate>")
                strBuffer.Append(" <flightcode>" & CType(dt.Rows(i)("flightcode").ToString, String) & "</flightcode>")
                strBuffer.Append(" <flight_tranid>" & CType(flighttranid, String) & "</flight_tranid>")
                strBuffer.Append(" <flighttime>" & CType(dt.Rows(i)("flighttime").ToString, String) & "</flighttime>")
                strBuffer.Append(" <pickup>" & CType(dt.Rows(i)("pickup").ToString, String) & "</pickup>")
                strBuffer.Append(" <dropoff>" & CType(dt.Rows(i)("dropoff").ToString, String) & "</dropoff>")

                strBuffer.Append(" <adults>" & CType(dt.Rows(i)("adults").ToString, String) & "</adults>")
                strBuffer.Append(" <child>" & CType(dt.Rows(i)("child").ToString, String) & "</child>")
                strBuffer.Append(" <childagestring>" & CType(dt.Rows(i)("childagestring").ToString, String) & "</childagestring>")
                strBuffer.Append(" <units>" & CType(dt.Rows(i)("units").ToString, String) & "</units>")
                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                strBuffer.Append(" <unitsalevalue>" & CType(salevalue, String) & "</unitsalevalue>")
                strBuffer.Append(" <tplistcode>" & CType(dt.Rows(i)("tplistcode").ToString, String) & "</tplistcode>")
                strBuffer.Append(" <complimentarycust>" & CType(dt.Rows(i)("ComplimentaryCust").ToString, String) & "</complimentarycust>")
                strBuffer.Append(" <unitprice>" & CType(dt.Rows(i)("Unitprice").ToString, String) & "</unitprice>")
                strBuffer.Append(" <unitsalevalue>" & CType(dt.Rows(i)("unitsalevalue").ToString, String) & "</unitsalevalue>")
                strBuffer.Append(" <overrideprice>0</overrideprice>")
                strBuffer.Append(" <flightclass>" & CType(dt.Rows(i)("flightclass").ToString, String) & "</flightclass>")

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
                strBuffer.Append(" <PriceWithTAX>1</PriceWithTAX>")
                strBuffer.Append(" <PriceTaxableValue>" & CType(dt.Rows(i)("PriceTaxableValue").ToString, String) & "</PriceTaxableValue>")
                strBuffer.Append(" <PriceVATValue>" & CType(dt.Rows(i)("PriceVATValue").ToString, String) & "</PriceVATValue>")
                strBuffer.Append(" <PriceVATPer>" & CType(dt.Rows(i)("PriceVATPer").ToString, String) & "</PriceVATPer>")
                strBuffer.Append(" <PriceWithTAX1>1</PriceWithTAX1>")

                strBuffer.Append(" <Pickupcodetype>" & CType(dt.Rows(i)("Pickupcodetype").ToString, String) & "</Pickupcodetype>")
                strBuffer.Append(" <Dropoffcodetype>" & CType(dt.Rows(i)("Dropoffcodetype").ToString, String) & "</Dropoffcodetype>")


                strBuffer.Append("</Table>")


            Next
            strBuffer.Append("</DocumentElement>")

            objBLLTransferFreeFormBooking.TransferXml = strBuffer.ToString
            objBLLTransferFreeFormBooking.RlineNos = rLineoNoString
            objBLLTransferFreeFormBooking.UserLogged = Session("GlobalUserName")
            If objBLLTransferFreeFormBooking.SaveTransferFreeFormBooking() = True Then
                Session("sRequestId") = requestid
                Response.Redirect("MoreServices.aspx")
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Failed to save.")
                Exit Sub
            End If



        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: btnBook_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try


    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim btnDelete As Button = CType(sender, Button)
            Dim dlItem As DataListItem = CType(btnDelete.NamingContainer, DataListItem)
            Dim lbltlineno As Label = CType(dlItem.FindControl("lbltlineno"), Label)
            If Not Session("sdtTrfDetails") Is Nothing Then
                Dim dtTrfDetails As DataTable
                dtTrfDetails = Session("sdtTrfDetails")
                If dtTrfDetails.Rows.Count = 1 Then

                ElseIf dtTrfDetails.Rows.Count > 1 Then
                    For i As Integer = 0 To dtTrfDetails.Rows.Count - 1
                        If dtTrfDetails.Rows(i)("tlineno").ToString.Trim = lbltlineno.Text.Trim Then
                            dtTrfDetails.Rows.Remove(dtTrfDetails.Rows(i))
                            Exit For
                        End If
                    Next
                End If


                dlTransferResults.DataSource = dtTrfDetails
                dlTransferResults.DataBind()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: btnDelete_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnAddMore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddMore.Click
        Try

            Dim dtDetails As DataTable = GetTransferDetailsFromGrid()

            If Not dtDetails Is Nothing Then
                dtDetails.Rows.Add()
                Session("sdtTrfDetails") = dtDetails
                dlTransferResults.DataSource = dtDetails
                dlTransferResults.DataBind()

            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TransferFreeFormBooking.aspx :: btnAddMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Function GetTransferDetailsFromGrid() As DataTable
        Dim dtNewTrfDetails As DataTable = Nothing
        If Not Session("sdtTrfDetails") Is Nothing Then
            Dim dtTrfDetails As DataTable
            dtTrfDetails = Session("sdtTrfDetails")

            dtNewTrfDetails = dtTrfDetails.Clone()
            Dim strTLineno As String = ""
            Dim rLineoNoString As String = ""
            Dim iItemIndex As Integer = 0
            For Each dlItem As DataListItem In dlTransferResults.Items

                Dim ddlTransferType As DropDownList = CType(dlItem.FindControl("ddlTransferType"), DropDownList)

                Dim flightcode As TextBox = CType(dlItem.FindControl("flightcode"), TextBox)
                Dim txtTransferFrom As TextBox = CType(dlItem.FindControl("txtTransferFrom"), TextBox)
                Dim txtTransferFromcode As TextBox = CType(dlItem.FindControl("txtTransferFromcode"), TextBox)
                Dim txtTransferTo As TextBox = CType(dlItem.FindControl("txtTransferTo"), TextBox)
                Dim txtTransferTocode As TextBox = CType(dlItem.FindControl("txtTransferTocode"), TextBox)
                Dim txtflight As TextBox = CType(dlItem.FindControl("txtflight"), TextBox)
                Dim txtflightCode As TextBox = CType(dlItem.FindControl("txtflightCode"), TextBox)
                Dim txtArrivalTime As TextBox = CType(dlItem.FindControl("txtArrivalTime"), TextBox)
                Dim txtTrfDate As TextBox = CType(dlItem.FindControl("txtTrfDate"), TextBox)

                Dim ddlTrfAdult As DropDownList = CType(dlItem.FindControl("ddlTrfAdult"), DropDownList)
                Dim ddlTrfChild As DropDownList = CType(dlItem.FindControl("ddlTrfChild"), DropDownList)

                Dim txtTrfChild1 As TextBox = CType(dlItem.FindControl("txtTrfChild1"), TextBox)
                Dim txtTrfChild2 As TextBox = CType(dlItem.FindControl("txtTrfChild2"), TextBox)
                Dim txtTrfChild3 As TextBox = CType(dlItem.FindControl("txtTrfChild3"), TextBox)
                Dim txtTrfChild4 As TextBox = CType(dlItem.FindControl("txtTrfChild4"), TextBox)
                Dim txtTrfChild5 As TextBox = CType(dlItem.FindControl("txtTrfChild5"), TextBox)
                Dim txtTrfChild6 As TextBox = CType(dlItem.FindControl("txtTrfChild6"), TextBox)

                Dim txtAdultPrice As TextBox = CType(dlItem.FindControl("txtAdultPrice"), TextBox)
                Dim txtNoOfUnit As TextBox = CType(dlItem.FindControl("txtNoOfUnit"), TextBox)
                Dim txtTransfersCode As TextBox = CType(dlItem.FindControl("txtTransfersCode"), TextBox)
           

                Dim ddlTrfFlightClass As DropDownList = CType(dlItem.FindControl("ddlTrfFlightClass"), DropDownList)
                Dim txtCostPricePax As TextBox = CType(dlItem.FindControl("txtCostPricePax"), TextBox)

                If txtTrfDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter " & ddlTransferType.SelectedValue & " DATE.")
                    Exit Function
                End If

                If txtTransferFromcode.Text = "" Then
                    Dim lblTransferFrom As Label = CType(dlItem.FindControl("lblTransferFrom"), Label)
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter " & lblTransferFrom.Text & ".")
                    Exit Function
                End If
                If txtTransferTocode.Text = "" Then
                    Dim lblTransferto As Label = CType(dlItem.FindControl("lblTransferto"), Label)
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter " & lblTransferto.Text & ".")
                    Exit Function
                End If
                If ddlTrfAdult.SelectedValue = "0" And ddlTrfChild.SelectedValue = "0" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter adult or child.")
                    Exit Function
                End If
                If ddlTrfChild.SelectedValue <> 0 Then
                    If ddlTrfChild.SelectedValue = 1 Then
                        If txtTrfChild1.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child age.")
                            Exit Function
                        End If

                    ElseIf ddlTrfChild.SelectedValue = 2 Then
                        If txtTrfChild1.Text = "" Or txtTrfChild2.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlTrfChild.SelectedValue = 3 Then
                        If txtTrfChild1.Text = "" Or txtTrfChild2.Text = "" Or txtTrfChild3.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlTrfChild.SelectedValue = 4 Then
                        If txtTrfChild1.Text = "" Or txtTrfChild2.Text = "" Or txtTrfChild3.Text = "" Or txtTrfChild4.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlTrfChild.SelectedValue = 5 Then
                        If txtTrfChild1.Text = "" Or txtTrfChild2.Text = "" Or txtTrfChild3.Text = "" Or txtTrfChild4.Text = "" Or txtTrfChild5.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    ElseIf ddlTrfChild.SelectedValue = 6 Then
                        If txtTrfChild1.Text = "" Or txtTrfChild2.Text = "" Or txtTrfChild3.Text = "" Or txtTrfChild4.Text = "" Or txtTrfChild5.Text = "" Or txtTrfChild6.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child ages.")
                            Exit Function
                        End If
                    End If
                End If

                If txtNoOfUnit.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter no of units.")
                    Exit Function
                Else
                    If Val(txtNoOfUnit.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter no of units.")
                        Exit Function
                    End If
                End If
                If txtAdultPrice.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter unit price.")
                    Exit Function
                Else
                    If Val(txtAdultPrice.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter unit price.")
                        Exit Function
                    End If
                End If

                Dim chkAllowZeroCost As CheckBox = CType(dlItem.FindControl("chkAllowZeroCost"), CheckBox) 'changed by mohamed on 28/08/2018

                If txtCostPricePax.Text = "" And chkAllowZeroCost.Checked = False Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter cost unit price.")
                    Exit Function
                Else
                    If Val(txtCostPricePax.Text) = 0 And chkAllowZeroCost.Checked = False Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter cost unit price.")
                        Exit Function
                    End If
                End If


                If txtTransfersCode.Text = "" Then

                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter transfers.")
                    Exit Function
                End If
                Dim strTransferCodeWithOption As String = txtTransfersCode.Text
                Dim strTransferCode As String = ""
                Dim strOptions As String = ""
                If strTransferCodeWithOption <> "" Then
                    Dim strTransfers As String() = strTransferCodeWithOption.Split("|")
                    strTransferCode = strTransfers(0)
                    If strTransfers.Length > 1 Then
                        strOptions = strTransfers(1)
                    End If

                End If



                Dim strChildValid As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=5202 and (option_selected='" & strTransferCode & "'  or options_available='" & strTransferCode & "')")
                Dim txtChildTransfers As TextBox = CType(dlItem.FindControl("txtChildTransfers"), TextBox)
                Dim txtChildTransfersCode As TextBox = CType(dlItem.FindControl("txtChildTransfersCode"), TextBox)
                Dim strChildTransferCode As String = ""


                Dim txtChildNoOfUnit As TextBox = CType(dlItem.FindControl("txtChildNoOfUnit"), TextBox)
                Dim txtChildPrice As TextBox = CType(dlItem.FindControl("txtChildPrice"), TextBox)
                Dim txtChildCostPricePax As TextBox = CType(dlItem.FindControl("txtChildCostPricePax"), TextBox)
                If ddlTrfChild.SelectedValue > 0 And strChildValid <> "" Then
                    If txtChildTransfersCode.Text = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child vehicle type.")
                        Exit Function
                    End If


                    If txtChildNoOfUnit.Text = "" Or Val(txtChildNoOfUnit.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child no of units.")
                        Exit Function

                    End If


                    If txtChildPrice.Text = "" Or Val(txtChildPrice.Text) = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child unit price.")
                        Exit Function

                    End If
                    If (txtChildCostPricePax.Text = "" Or Val(txtChildCostPricePax.Text) = 0) And chkAllowZeroCost.Checked = False Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter child cost price.")
                        Exit Function

                    End If

                End If


                Dim chkComplSup As CheckBox = CType(dlItem.FindControl("chkComplSup"), CheckBox)
                Dim strChildren As String = ddlTrfChild.SelectedValue

                Dim strChild1 As String = txtTrfChild1.Text
                Dim strChild2 As String = txtTrfChild2.Text
                Dim strChild3 As String = txtTrfChild3.Text
                Dim strChild4 As String = txtTrfChild4.Text
                Dim strChild5 As String = txtTrfChild5.Text
                Dim strChild6 As String = txtTrfChild6.Text

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

                If ddlTrfAdult.SelectedValue > 0 Then


                    dtNewTrfDetails.Rows.Add()

                    If Session("sRequestId") Is Nothing Then

                        If strTLineno = "" Then
                            strTLineno = "1"
                        Else
                            strTLineno = CType(strTLineno, Integer) + 1
                        End If
                    ElseIf ViewState("vTLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                        If strTLineno = "" Then
                            strTLineno = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "TRANSFER")
                        Else
                            strTLineno = CType(strTLineno, Integer) + 1
                        End If
                    Else
                        strTLineno = ViewState("vTLineNo")
                    End If
                    If rLineoNoString <> "" Then
                        rLineoNoString = rLineoNoString + ";" + CType(strTLineno, String)
                    Else
                        rLineoNoString = strTLineno
                    End If

                    dtNewTrfDetails.Rows(iItemIndex)("Tlineno") = strTLineno
                    dtNewTrfDetails.Rows(iItemIndex)("transfertype") = ddlTransferType.SelectedValue.Trim



                    Dim strSectorQuery = ""
                    Dim pickuptype As String() = txtTransferFromcode.Text.Split(";")
                    Dim droptype As String() = txtTransferTocode.Text.Split(";")
                    If ddlTransferType.SelectedValue = "ARRIVAL" Then
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = pickuptype(0) 'txtTransferFromcode.Text
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordername") = txtTransferFrom.Text

                        dtNewTrfDetails.Rows(iItemIndex)("Pickupcodetype") = ""
                        dtNewTrfDetails.Rows(iItemIndex)("Dropoffcodetype") = droptype(1)
                        If droptype(1) = "P" Then
                            strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & droptype(0) & "'"
                        Else
                            strSectorQuery = ""
                        End If


                    ElseIf ddlTransferType.SelectedValue = "DEPARTURE" Then
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = droptype(0) ' txtTransferTocode.Text
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordername") = txtTransferTo.Text

                        dtNewTrfDetails.Rows(iItemIndex)("Pickupcodetype") = pickuptype(1)
                        dtNewTrfDetails.Rows(iItemIndex)("Dropoffcodetype") = ""
                        If pickuptype(1) = "P" Then
                            strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & pickuptype(0) & "'"
                        Else
                            strSectorQuery = ""
                        End If

                    Else
                        If pickuptype(1) = "P" Then
                            strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & pickuptype(0) & "'" 'changed by mohamed on 04/08/2018
                            dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                        Else
                            strSectorQuery = ""
                            dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = pickuptype(0)
                        End If

                        dtNewTrfDetails.Rows(iItemIndex)("Pickupcodetype") = pickuptype(1)
                        dtNewTrfDetails.Rows(iItemIndex)("Dropoffcodetype") = droptype(1)
                    End If

                    Dim strSectorGroupCode As String = ""
                    If strSectorQuery <> "" Then
                        strSectorGroupCode = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                        If strSectorGroupCode = "" Then
                            If ddlTransferType.SelectedValue = "ARRIVAL" Then
                                strSectorGroupCode = IIf(droptype(1) = "P", txtTransferTocode.Text, droptype(0))

                            ElseIf ddlTransferType.SelectedValue = "DEPARTURE" Or ddlTransferType.SelectedValue = "INTERHOTEL" Then
                                strSectorGroupCode = IIf(pickuptype(1) = "P", txtTransferFromcode.Text, pickuptype(0))
                            End If
                        End If
                    Else
                        If ddlTransferType.SelectedValue = "ARRIVAL" Then
                            strSectorGroupCode = IIf(droptype(1) = "P", txtTransferTocode.Text, droptype(0))

                        ElseIf ddlTransferType.SelectedValue = "DEPARTURE" Or ddlTransferType.SelectedValue = "INTERHOTEL" Then
                            strSectorGroupCode = IIf(pickuptype(1) = "P", txtTransferFromcode.Text, pickuptype(0))
                        End If
                    End If

                    dtNewTrfDetails.Rows(iItemIndex)("sectorgroupcode") = strSectorGroupCode

                    dtNewTrfDetails.Rows(iItemIndex)("cartypecode") = txtTransfersCode.Text
                    dtNewTrfDetails.Rows(iItemIndex)("shuttle") = "0"

                    dtNewTrfDetails.Rows(iItemIndex)("transferdate") = txtTrfDate.Text
                    dtNewTrfDetails.Rows(iItemIndex)("vtransferdate") = txtTrfDate.Text
                    dtNewTrfDetails.Rows(iItemIndex)("flightcode") = txtflight.Text
                    dtNewTrfDetails.Rows(iItemIndex)("flight_tranid") = objclsUtilities.ExecuteQueryReturnStringValue("select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(txtflight.Text, String) & "'")
                    dtNewTrfDetails.Rows(iItemIndex)("flighttime") = txtArrivalTime.Text
                    dtNewTrfDetails.Rows(iItemIndex)("pickup") = pickuptype(0) 'txtTransferFromcode.Text
                    dtNewTrfDetails.Rows(iItemIndex)("pickupname") = txtTransferFrom.Text
                    dtNewTrfDetails.Rows(iItemIndex)("dropoff") = droptype(0) 'txtTransferTocode.Text
                    dtNewTrfDetails.Rows(iItemIndex)("dropoffname") = txtTransferTo.Text

                    dtNewTrfDetails.Rows(iItemIndex)("adults") = ddlTrfAdult.SelectedValue



                    If strChildValid <> "" Then
                        dtNewTrfDetails.Rows(iItemIndex)("child") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("childagestring") = ""
                    Else
                        dtNewTrfDetails.Rows(iItemIndex)("child") = ddlTrfChild.SelectedValue
                        dtNewTrfDetails.Rows(iItemIndex)("childagestring") = ChildAgeString
                    End If



                    dtNewTrfDetails.Rows(iItemIndex)("units") = txtNoOfUnit.Text
                    dtNewTrfDetails.Rows(iItemIndex)("unitprice") = txtAdultPrice.Text
                    Dim salevalue As Double = CType(Val(dtNewTrfDetails.Rows(iItemIndex)("units").ToString), Integer) * CType(dtNewTrfDetails.Rows(iItemIndex)("unitprice").ToString, Decimal)
                    dtNewTrfDetails.Rows(iItemIndex)("unitsalevalue") = salevalue.ToString
                    dtNewTrfDetails.Rows(iItemIndex)("tplistcode") = ""

                    dtNewTrfDetails.Rows(iItemIndex)("complimentarycust") = IIf(chkComplSup.Checked = True, "1", "0")
                    dtNewTrfDetails.Rows(iItemIndex)("overrideprice") = "0"

                    dtNewTrfDetails.Rows(iItemIndex)("flightclass") = ddlTrfFlightClass.SelectedValue

                    dtNewTrfDetails.Rows(iItemIndex)("wlunitprice") = dtNewTrfDetails.Rows(iItemIndex)("unitprice").ToString
                    dtNewTrfDetails.Rows(iItemIndex)("wlunitsalevalue") = dtNewTrfDetails.Rows(iItemIndex)("unitsalevalue").ToString
                    dtNewTrfDetails.Rows(iItemIndex)("preferredsupplier") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("unitcprice") = txtCostPricePax.Text
                    Dim costvalue As Double = CType(Val(dtNewTrfDetails.Rows(iItemIndex)("units").ToString), Integer) * CType(dtNewTrfDetails.Rows(iItemIndex)("unitcprice").ToString, Decimal)
                    dtNewTrfDetails.Rows(iItemIndex)("unitcostvalue") = costvalue

                    dtNewTrfDetails.Rows(iItemIndex)("CostTaxableValue") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("CostVATValue") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("PriceTaxableValue") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("PriceVATValue") = "0"

                    dtNewTrfDetails.Rows(iItemIndex)("wlmarkupperc") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("PriceVATPer") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("PriceWithTAX1") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("VATPer") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("PriceWithTAX") = "0"
                    dtNewTrfDetails.Rows(iItemIndex)("wlconvrate") = "0"

                End If


                    If ddlTrfChild.SelectedValue > 0 And strChildValid <> "" Then

                        dtNewTrfDetails.Rows.Add()
                        If ddlTrfAdult.SelectedValue <> 0 Then
                            iItemIndex = iItemIndex + 1
                        End If

                        If Session("sRequestId") Is Nothing Then

                            If strTLineno = "" Then
                                strTLineno = "1"
                            Else
                                strTLineno = CType(strTLineno, Integer) + 1
                            End If
                        ElseIf ViewState("vTLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                            If strTLineno = "" Then
                                strTLineno = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "TRANSFER")
                            Else
                                strTLineno = CType(strTLineno, Integer) + 1
                            End If
                        Else
                            strTLineno = ViewState("vTLineNo")
                        End If
                        If rLineoNoString <> "" Then
                            rLineoNoString = rLineoNoString + ";" + CType(strTLineno, String)
                        Else
                            rLineoNoString = strTLineno
                        End If

                        dtNewTrfDetails.Rows(iItemIndex)("Tlineno") = strTLineno
                        dtNewTrfDetails.Rows(iItemIndex)("transfertype") = ddlTransferType.SelectedValue.Trim


                        If txtChildTransfersCode.Text <> "" Then
                            Dim strChildTransfers As String() = txtChildTransfersCode.Text.Split("|")
                            strChildTransferCode = strChildTransfers(0)

                        End If

                    Dim strSectorQuery = ""
                    Dim pickuptype As String() = txtTransferFromcode.Text.Split(";")
                    Dim droptype As String() = txtTransferTocode.Text.Split(";")
                    If ddlTransferType.SelectedValue = "ARRIVAL" Then
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = pickuptype(0) 'txtTransferFromcode.Text
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordername") = txtTransferFrom.Text

                        dtNewTrfDetails.Rows(iItemIndex)("Pickupcodetype") = ""
                        dtNewTrfDetails.Rows(iItemIndex)("Dropoffcodetype") = droptype(1)
                        If droptype(1) = "P" Then
                            strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & droptype(0) & "'"
                        Else
                            strSectorQuery = ""
                        End If


                        ' strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & txtTransferTocode.Text & "'"

                    ElseIf ddlTransferType.SelectedValue = "DEPARTURE" Then

                        dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = droptype(0) ' txtTransferTocode.Text
                        dtNewTrfDetails.Rows(iItemIndex)("airportbordername") = txtTransferTo.Text
                        'strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & txtTransferFromcode.Text & "'"
                        dtNewTrfDetails.Rows(iItemIndex)("Pickupcodetype") = pickuptype(1)
                        dtNewTrfDetails.Rows(iItemIndex)("Dropoffcodetype") = ""
                        If pickuptype(1) = "P" Then
                            strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & pickuptype(0) & "'"
                        Else
                            strSectorQuery = ""
                        End If

                    Else
                        If pickuptype(1) = "P" Then
                            strSectorQuery = "select s.sectorgroupcode from partymast p(nolock),sectormaster s(nolock) where p.sectorcode=s.sectorcode and  p.active=1 and p.partycode='" & pickuptype(0) & "'" 'changed by mohamed on 04/08/2018
                            dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                        Else
                            strSectorQuery = ""
                            dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = pickuptype(0)
                        End If

                        dtNewTrfDetails.Rows(iItemIndex)("Pickupcodetype") = pickuptype(1)
                        dtNewTrfDetails.Rows(iItemIndex)("Dropoffcodetype") = droptype(1)

                        ' dtNewTrfDetails.Rows(iItemIndex)("airportbordercode") = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                    End If

                    Dim strSectorGroupCode As String = ""
                    If strSectorQuery <> "" Then
                        strSectorGroupCode = objclsUtilities.ExecuteQueryReturnStringValue(strSectorQuery)
                        If strSectorGroupCode = "" Then
                            If ddlTransferType.SelectedValue = "ARRIVAL" Then
                                strSectorGroupCode = IIf(droptype(1) = "P", txtTransferTocode.Text, droptype(0))

                            ElseIf ddlTransferType.SelectedValue = "DEPARTURE" Or ddlTransferType.SelectedValue = "INTERHOTEL" Then
                                strSectorGroupCode = IIf(pickuptype(1) = "P", txtTransferFromcode.Text, pickuptype(0))
                            End If
                        End If
                    Else
                        If ddlTransferType.SelectedValue = "ARRIVAL" Then
                            strSectorGroupCode = IIf(droptype(1) = "P", txtTransferTocode.Text, droptype(0))

                        ElseIf ddlTransferType.SelectedValue = "DEPARTURE" Or ddlTransferType.SelectedValue = "INTERHOTEL" Then
                            strSectorGroupCode = IIf(pickuptype(1) = "P", txtTransferFromcode.Text, pickuptype(0))
                        End If
                    End If

                        dtNewTrfDetails.Rows(iItemIndex)("sectorgroupcode") = strSectorGroupCode

                        dtNewTrfDetails.Rows(iItemIndex)("cartypecode") = strChildTransferCode
                        dtNewTrfDetails.Rows(iItemIndex)("shuttle") = "0"

                        dtNewTrfDetails.Rows(iItemIndex)("transferdate") = txtTrfDate.Text
                        dtNewTrfDetails.Rows(iItemIndex)("vtransferdate") = txtTrfDate.Text
                        dtNewTrfDetails.Rows(iItemIndex)("flightcode") = txtflight.Text
                        dtNewTrfDetails.Rows(iItemIndex)("flight_tranid") = objclsUtilities.ExecuteQueryReturnStringValue("select flight_tranid from flightmast(nolock) where active=1 and flightcode='" & CType(txtflight.Text, String) & "'")
                        dtNewTrfDetails.Rows(iItemIndex)("flighttime") = txtArrivalTime.Text
                        dtNewTrfDetails.Rows(iItemIndex)("pickup") = txtTransferFromcode.Text
                        dtNewTrfDetails.Rows(iItemIndex)("pickupname") = txtTransferFrom.Text
                        dtNewTrfDetails.Rows(iItemIndex)("dropoff") = txtTransferTocode.Text
                        dtNewTrfDetails.Rows(iItemIndex)("dropoffname") = txtTransferTo.Text

                        dtNewTrfDetails.Rows(iItemIndex)("adults") = "0"

                        dtNewTrfDetails.Rows(iItemIndex)("child") = ddlTrfChild.SelectedValue
                        'Dim strChildren As String = ddlTrfChild.SelectedValue



                        'Dim strChild1 As String = txtTrfChild1.Text
                        'Dim strChild2 As String = txtTrfChild2.Text
                        'Dim strChild3 As String = txtTrfChild3.Text
                        'Dim strChild4 As String = txtTrfChild4.Text
                        'Dim strChild5 As String = txtTrfChild5.Text
                        'Dim strChild6 As String = txtTrfChild6.Text

                        'Dim strSourceCountry As String = txtCountry.Text
                        'Dim strSourceCountryCode As String = txtCountryCode.Text

                        'Dim ChildAgeString As String = ""
                        'If strChildren <> "" Then

                        '    If strChildren = "1" Then

                        '        ChildAgeString = strChild1
                        '    ElseIf strChildren = "2" Then

                        '        ChildAgeString = strChild1 & ";" & strChild2
                        '    ElseIf strChildren = "3" Then

                        '        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                        '    ElseIf strChildren = "4" Then

                        '        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                        '    ElseIf strChildren = "5" Then

                        '        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                        '    ElseIf strChildren = "6" Then

                        '        ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6

                        '    End If
                        'End If
                        dtNewTrfDetails.Rows(iItemIndex)("childagestring") = ChildAgeString
                        If ddlTrfAdult.SelectedValue = 0 Then
                            dtNewTrfDetails.Rows(iItemIndex)("units") = txtNoOfUnit.Text
                            dtNewTrfDetails.Rows(iItemIndex)("unitprice") = txtAdultPrice.Text
                            dtNewTrfDetails.Rows(iItemIndex)("unitcprice") = txtCostPricePax.Text
                        Else
                            dtNewTrfDetails.Rows(iItemIndex)("units") = txtChildNoOfUnit.Text
                            dtNewTrfDetails.Rows(iItemIndex)("unitprice") = txtChildPrice.Text
                            dtNewTrfDetails.Rows(iItemIndex)("unitcprice") = txtChildCostPricePax.Text
                        End If


                        Dim Childsalevalue As Double = CType(Val(dtNewTrfDetails.Rows(iItemIndex)("units").ToString), Integer) * CType(dtNewTrfDetails.Rows(iItemIndex)("unitprice").ToString, Decimal)
                        dtNewTrfDetails.Rows(iItemIndex)("unitsalevalue") = Childsalevalue.ToString
                        dtNewTrfDetails.Rows(iItemIndex)("tplistcode") = ""

                        dtNewTrfDetails.Rows(iItemIndex)("complimentarycust") = IIf(chkComplSup.Checked = True, "1", "0")
                        dtNewTrfDetails.Rows(iItemIndex)("overrideprice") = "0"

                        dtNewTrfDetails.Rows(iItemIndex)("flightclass") = ddlTrfFlightClass.SelectedValue

                        dtNewTrfDetails.Rows(iItemIndex)("wlunitprice") = dtNewTrfDetails.Rows(iItemIndex)("unitprice").ToString
                        dtNewTrfDetails.Rows(iItemIndex)("wlunitsalevalue") = dtNewTrfDetails.Rows(iItemIndex)("unitsalevalue").ToString
                        dtNewTrfDetails.Rows(iItemIndex)("preferredsupplier") = "0"

                        Dim childcostvalue As Double = CType(Val(dtNewTrfDetails.Rows(iItemIndex)("units").ToString), Integer) * CType(dtNewTrfDetails.Rows(iItemIndex)("unitcprice").ToString, Decimal)
                        dtNewTrfDetails.Rows(iItemIndex)("unitcostvalue") = childcostvalue

                        dtNewTrfDetails.Rows(iItemIndex)("CostTaxableValue") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("CostVATValue") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("PriceTaxableValue") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("PriceVATValue") = "0"

                        dtNewTrfDetails.Rows(iItemIndex)("wlmarkupperc") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("PriceVATPer") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("PriceWithTAX1") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("VATPer") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("PriceWithTAX") = "0"
                        dtNewTrfDetails.Rows(iItemIndex)("wlconvrate") = "0"






                    End If



                    iItemIndex = iItemIndex + 1

            Next




        End If
        Return dtNewTrfDetails
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
                    'txtCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    'txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    'AutoCompleteExtender_txtCustomer.Enabled = False
                    'AutoCompleteExtender_txtCountry.Enabled = False
                End If
            End If
        End If
        Session("sdtTrfDetails") = Nothing
        BindTransferDatas()
    End Sub
End Class
