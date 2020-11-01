Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Linq
Imports System.Threading
Imports System.Reflection

Partial Class HotelSearch
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

    Dim strShowNotColumbusRate As String = ConfigurationManager.AppSettings("ShowNotColumbusRate").ToString
    Dim strShowColumbusRate As String = ConfigurationManager.AppSettings("ShowColumbusRate").ToString

    Private trd1 As Thread = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
    Private trd2 As Thread = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)

    'Private trd3 As Thread = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCStatic)
    Private trd4 As Thread = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCDynamic)
    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                If Val(Session("Status_dtAdultChilds")) <> 1 Then '*** Danny 01/09/2018
                    Session("dtAdultChilds") = Nothing
                End If
                If System.Diagnostics.Process.GetProcessesByName("myThread1").Length > 0 Then
                    Dim str As String = ""
                End If



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
                Timer1.Enabled = False
                imgHotelthreadLoading.Visible = False
                Session("sDSSearchResultsForPreferred") = Nothing
                Session("sDSSearchResultsForNonPreferred") = Nothing
                Session("sDSSearchResultsForOneDMCDynamic") = Nothing
                Session("sDSSearchResults") = Nothing
                txtSearchHotel.Text = ""

                'If Session("sDSSearchResults") IsNot Nothing Then
                '    'changed by mohamed on 12/02/2018
                '    txtSearchHotel.Enabled = False
                '    btnHotelTextSearch.Enabled = False
                '    ddlSorting.Enabled = False
                'End If

                If Not Request.QueryString("RLineNo") Is Nothing Then
                    ViewState("vRLineNo") = Request.QueryString("RLineNo")
                    hdHotelTabFreeze.Value = "1"
                End If


                ViewState("vPreHotelRLineNo") = Request.QueryString("PLineNo")
                If Not Request.QueryString("PLineNo") Is Nothing Then
                    hdHotelTab.Value = "0"
                    hdHotelTabFreeze.Value = "1"
                End If

                hdHotelAvailableForShifting.Value = "0"
                LoadHome()

                If Not Session("sdsPriceBreakup") Is Nothing Then
                    Session.Remove("sdsPriceBreakup")
                End If
                If Not Session("sdsPriceBreakupTemp") Is Nothing Then
                    Session.Remove("sdsPriceBreakupTemp")
                End If

            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("HotelSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub
    ''' <summary>
    ''' LoadHome
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadHome()


        '*** Danny Update Following Code 17/10/2018
        Dim ds_SR As New DataSet
        ds_SR = objclsUtilities.GetDataSet("SP_GetReservationParameters", Nothing)
        Dim showMeal As String = ds_SR.Tables(0).Rows(0)("showMeal").ToString
        Dim showcatall As String = ds_SR.Tables(0).Rows(0)("showcatall").ToString
        Dim strPlaceHolder As String = ds_SR.Tables(0).Rows(0)("showMeal").ToString

        Session("strShowMoreVATText") = ds_SR.Tables(0).Rows(0)("strShowMoreVATText").ToString

        ddlAvailability.SelectedValue = "2"
        LoadFooter()
        hdChildAgeLimit.Value = objResParam.ChildAgeLimit
        hdMaxNoOfNight.Value = objResParam.NoOfNightLimit
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        LoadRoomAdultChild()
        LoadFields()
        BindPropertyType()
        BindMealPlan() ''** Shahul 26/06/2018
        ShowMyBooking()
        If Not Session("sdsHotelRoomTypes") Is Nothing Then
            Session("sdsHotelRoomTypes") = Nothing
        End If
        FillBookingDetailsBasedonNewOrEditMode()
        FillPreArrangedHotelDetailsBasedonNewOrEditMode()
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If
        'changed by mohamed on 11/04/2018
        Dim lsStrScriptShift As String = "javascript: fnLockControlsForShifting();"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", lsStrScriptShift, True)






        ''** Shahul 26/06/2018
        'Dim showMeal As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=3002")

        If showMeal = "0" Then
            divmeal.Style.Add("display", "none")
        Else
            divmeal.Style.Add("display", "block")
        End If

        'Dim showcatall As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=3003")

        If showcatall = "0" Then
            divstarshow.Style.Add("display", "none")
        Else
            divstarshow.Style.Add("display", "block")
        End If
        '*** Danny 14/07/2018
        'Dim strPlaceHolder As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=10")

        If strPlaceHolder = "" Then
            txtDestinationName.Attributes.Add("placeholder", "Example: Type destination")
        Else
            txtDestinationName.Attributes.Add("placeholder", "Example: " & strPlaceHolder.ToString())
        End If




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
                imgLogo.Src = Session("sLogo") '*** Danny 04/07/2018

            End If

        End If
    End Sub
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
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                dvForRO.Visible = False
                dvPreHotelAgent.Visible = False
                dvOverridePrice.Visible = False
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

                        txtPreHotelSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString 'modifed on 20180415
                        txtPreHotelSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtPreHotelSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
                    Else
                        txtCountry.ReadOnly = False
                        AutoCompleteExtender_txtCountry.Enabled = True

                        txtPreHotelSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else
                dvForRO.Visible = True
                dvPreHotelAgent.Visible = True
                dvOverridePrice.Visible = True
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub
    ''' <summary>
    ''' GetShiftHotelList
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
  <System.Web.Services.WebMethod()> _
    Public Shared Function GetShiftHotelList(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            'changed by mohamed on 05/04/2018
            'strSqlQry = "select d.rlineno,d.partycode,p.partyname+' '+convert(varchar(10),d.checkin,103)+' - '+convert(varchar(10),d.checkout,103) HotelName from booking_hotel_detailtemp d(nolock),partymast p(nolock) where d.requestid= '" & HttpContext.Current.Session("sRequestId").ToString & "' and d.partycode=p.partycode and  p.partyname+' '+convert(varchar(10),d.checkin,103)+'-'+convert(varchar(10),d.checkout,103) like  '%" & prefixText & "%'  order by d.rlineno"
            strSqlQry = "execute sp_get_shifting_hotel_detail '" & HttpContext.Current.Session("sRequestId").ToString & "', -1 "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    'Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString()))
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString() + "|" + Replace(myDS.Tables(0).Rows(i)("roomstring").ToString(), "|", "&s") + "**" + myDS.Tables(0).Rows(i)("checkout").ToString() & "**" & myDS.Tables(0).Rows(i)("checkin").ToString() & "**" & myDS.Tables(0).Rows(i)("partycode").ToString() & "**" & myDS.Tables(0).Rows(i)("partyname").ToString() & "**" & myDS.Tables(0).Rows(i)("nights").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function
    ''' <summary>
    ''' GetDeastinationList
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' GetHotelStars
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            strSqlQry = "select catcode,catname from view_hotel_category where catname like  '" & prefixText & "%' order by rankorder "
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
    ''' <summary>
    ''' GetHotelName
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            '  strSqlQry = "select p.partycode,p.partyname from partymast p,sectormaster s,catmast c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partyname like  '" & prefixText & "%' "
            strSqlQry = "select v.partycode,v.partyname from sectormaster(nolock) s,catmast(nolock) c,view_approved_hotels_new v where v.sectorcode=s.sectorcode  and v.catcode=c.catcode   and v.partyname like  '%" & prefixText & "%' "
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
                    'TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("destname").ToString(), myDS.Tables(0).Rows(i)("destcode").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
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
        Dim myDS As New DataSet
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
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentOnlineCommon
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

            'strSqlQry = "select p.sectorcode,s.sectorname,c.catcode,c.catname from partymast p,sectormaster s,catmast c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
            strSqlQry = "select s.destname,c.catcode,c.catname,s.destcode + '|' +case when desttype='Area' or desttype='Sector' then 'Sector' else desttype end destcode  from partymast(nolock) p,view_destination_search(nolock) s,catmast c where p.sectorcode=s.destcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
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
    ''' BindPropertyType
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub BindPropertyType(ByVal dt As DataTable)
        Dim dvPropertyType As DataView = New DataView(dt)
        dvPropertyType.Sort = "propertytypename asc"
        dt = dvPropertyType.ToTable(True)

        If dt.Rows.Count > 0 Then
            chkPropertyType.DataSource = dt
            chkPropertyType.DataTextField = "propertytypename"
            chkPropertyType.DataValueField = "propertytype"
            chkPropertyType.DataBind()
            'If chkPropertyType.Items.Count > 0 Then
            '    For Each chkitem As ListItem In chkPropertyType.Items
            '        chkitem.Selected = True
            '    Next
            'End If
            chkPropertyTypeSelectAll.Checked = True
        Else
            chkPropertyType.Items.Clear()
            chkPropertyType.DataBind()
        End If

    End Sub
    ''' <summary>
    ''' BindSearchResultsForEdit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindSearchResultsForEdit()
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


        objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sobjBLLHotelSearch") Is Nothing Then
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)

            Dim dsSearchResults As New DataSet
            objBLLHotelSearch.FilterRoomClass = ""

            If hdOPMode.Value = "Edit" Then
                objBLLHotelSearch.EditRequestId = Session("sRequestId")
                objBLLHotelSearch.EditRLineNo = hdEditRLineNo.Value

            Else

                If Not Session("sRequestId") Is Nothing Then
                    objBLLHotelSearch.EditRequestId = Session("sRequestId")
                    objBLLHotelSearch.EditRLineNo = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "HOTEL")
                Else
                    objBLLHotelSearch.EditRequestId = ""
                    objBLLHotelSearch.EditRLineNo = ""
                End If

                objBLLHotelSearch.EditRatePlanId = ""
            End If

            hdOveride.Value = objBLLHotelSearch.OverridePrice

        End If

    End Sub
    ''' <summary>
    ''' BindSearchResults
    ''' </summary>
    ''' <remarks></remarks>
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


        objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sobjBLLHotelSearch") Is Nothing Then
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)

            Dim dsSearchResults As New DataSet
            objBLLHotelSearch.FilterRoomClass = ""

            'If Not Session("sEditRequestId") Is Nothing Then
            '    objBLLHotelSearch.EditRequestId = Session("sEditRequestId")
            '    objBLLHotelSearch.EditRLineNo = hdEditRLineNo.Value
            '    objBLLHotelSearch.EditRatePlanId = hdEditRatePlanId.Value

            'Else

            If hdOPMode.Value = "Edit" Then
                objBLLHotelSearch.EditRequestId = Session("sRequestId")
                objBLLHotelSearch.EditRLineNo = hdEditRLineNo.Value

            Else

                If Not Session("sRequestId") Is Nothing Then
                    objBLLHotelSearch.EditRequestId = Session("sRequestId")
                    objBLLHotelSearch.EditRLineNo = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "HOTEL")
                Else
                    objBLLHotelSearch.EditRequestId = ""
                    objBLLHotelSearch.EditRLineNo = ""
                End If

                objBLLHotelSearch.EditRatePlanId = ""
            End If


            ' End If

            hdOveride.Value = objBLLHotelSearch.OverridePrice


            '  

      
            ''Changed for apply threading technique. Modified on 28/02/2018. -- thread revert back on 12/03/2018
            If objBLLHotelSearch.HotelCode = "" Then
               
                If strShowNotColumbusRate = "YES" And strShowColumbusRate = "YES" Then

                    imgHotelthreadLoading.Visible = True
                    dlHotelsSearchResults.DataBind()
                    rptPager.DataBind()
                    hdProgress.Value = "1"
                    hdProgressTimer2.Value = "1"
                    ModalPopupDays.Show()
                    Session("sDSSearchResults") = Nothing
                    Session("sDSSearchResultsForPreferred") = Nothing
                    Session("sDSSearchResultsForNonPreferred") = Nothing
                    Session("sPreferred") = Nothing
                    Session("sNonPreferred") = Nothing

                    Timer2.Enabled = True

                    trd1 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
                    trd1.Name = "myThread1"
                    trd1.IsBackground = False
                    trd1.Start(objBLLHotelSearch)

                    trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)
                    trd2.Name = "myThread2"
                    trd2.IsBackground = False
                    trd2.Start(objBLLHotelSearch)



                    Session("sDSSearchResultsOneDMCStatic") = Nothing
                    Session("sDSSearchResultsOneDMCDynamic") = Nothing
                    Session("OneDMCDynamic") = Nothing


                    'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCStatic)
                    'trd2.Name = "myThreadOneDMCStatic"
                    'trd2.IsBackground = False
                    'trd2.Start(objBLLHotelSearch)

                    trd4 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCDynamic)
                    trd4.Name = "myThreadOneDMCDynamic"
                    trd4.IsBackground = False
                    trd4.Start(objBLLHotelSearch)

                ElseIf strShowNotColumbusRate = "YES" And strShowColumbusRate <> "YES" Then

                    imgHotelthreadLoading.Visible = True
                    dlHotelsSearchResults.DataBind()
                    rptPager.DataBind()
                    hdProgress.Value = "1"
                    hdProgressTimer2.Value = "1"
                    ModalPopupDays.Show()
                    Session("sDSSearchResults") = Nothing
                    Session("sDSSearchResultsForPreferred") = Nothing
                    Session("sDSSearchResultsForNonPreferred") = Nothing
                    Session("sPreferred") = Nothing
                    Session("sNonPreferred") = Nothing

                    Timer2.Enabled = True

                    'trd1 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
                    'trd1.Name = "myThread1"
                    'trd1.IsBackground = False
                    'trd1.Start(objBLLHotelSearch)

                    'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)
                    'trd2.Name = "myThread2"
                    'trd2.IsBackground = False
                    'trd2.Start(objBLLHotelSearch)



                    Session("sDSSearchResultsOneDMCStatic") = Nothing
                    Session("sDSSearchResultsOneDMCDynamic") = Nothing
                    Session("OneDMCDynamic") = Nothing


                    'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCStatic)
                    'trd2.Name = "myThreadOneDMCStatic"
                    'trd2.IsBackground = False
                    'trd2.Start(objBLLHotelSearch)

                    trd4 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCDynamic)
                    trd4.Name = "myThreadOneDMCDynamic"
                    trd4.IsBackground = False
                    trd4.Start(objBLLHotelSearch)

                ElseIf strShowNotColumbusRate <> "YES" And strShowColumbusRate = "YES" Then

                    imgHotelthreadLoading.Visible = True
                    dlHotelsSearchResults.DataBind()
                    rptPager.DataBind()
                    hdProgress.Value = "1"
                    hdProgressTimer2.Value = "1"
                    ModalPopupDays.Show()
                    Session("sDSSearchResults") = Nothing
                    Session("sDSSearchResultsForPreferred") = Nothing
                    Session("sDSSearchResultsForNonPreferred") = Nothing
                    Session("sPreferred") = Nothing
                    Session("sNonPreferred") = Nothing

                    Timer2.Enabled = True

                    trd1 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
                    trd1.Name = "myThread1"
                    trd1.IsBackground = False
                    trd1.Start(objBLLHotelSearch)

                    trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)
                    trd2.Name = "myThread2"
                    trd2.IsBackground = False
                    trd2.Start(objBLLHotelSearch)



                    Session("sDSSearchResultsOneDMCStatic") = Nothing
                    Session("sDSSearchResultsOneDMCDynamic") = Nothing
                    Session("OneDMCDynamic") = Nothing


                    'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCStatic)
                    'trd2.Name = "myThreadOneDMCStatic"
                    'trd2.IsBackground = False
                    'trd2.Start(objBLLHotelSearch)

                    'trd4 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCDynamic)
                    'trd4.Name = "myThreadOneDMCDynamic"
                    'trd4.IsBackground = False
                    'trd4.Start(objBLLHotelSearch)

                End If


               

            Else
                'Added by abin on 20200806 12.01 AM
                'Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
                'Dim objApiController As ApiController = New ApiController()
                'obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelSearch)

                If strShowNotColumbusRate = "YES" And strShowColumbusRate = "YES" Then
                    Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
                    Dim objApiController As ApiController = New ApiController()
                    obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelSearch)
                    Session("sobHotelSearchResponse") = obHotelSearchResponse
                    dsSearchResults = GetMinPriceData(obHotelSearchResponse, objBLLHotelSearch.Room, objBLLHotelSearch.Adult, objBLLHotelSearch.Children, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode)
                    Session("sDSSearchResultsForOneDMCDynamic") = dsSearchResults


                    dsSearchResults = objBLLHotelSearch.GetSearchDetails()
                    If Not Session("sDSSearchResultsForOneDMCDynamic") Is Nothing Then
                        dsSearchResults.Merge(Session("sDSSearchResultsForOneDMCDynamic"), True, MissingSchemaAction.Add)
                        dsSearchResults.AcceptChanges()
                    End If

                    Session("sDSSearchResults") = dsSearchResults
                    BindThreadResultForPageLoad()
                ElseIf strShowNotColumbusRate = "YES" And strShowColumbusRate <> "YES" Then
                    Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
                    Dim objApiController As ApiController = New ApiController()
                    obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelSearch)
                    Session("sobHotelSearchResponse") = obHotelSearchResponse
                    dsSearchResults = GetMinPriceData(obHotelSearchResponse, objBLLHotelSearch.Room, objBLLHotelSearch.Adult, objBLLHotelSearch.Children, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode)
                    Session("sDSSearchResultsForOneDMCDynamic") = dsSearchResults


                    'dsSearchResults = objBLLHotelSearch.GetSearchDetails()
                    'Session("sDSSearchResults") = dsSearchResults
                    BindThreadResultForPageLoad()
                ElseIf strShowNotColumbusRate <> "YES" And strShowColumbusRate = "YES" Then
                    'Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
                    'Dim objApiController As ApiController = New ApiController()
                    'obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelSearch)
                    'Session("sobHotelSearchResponse") = obHotelSearchResponse
                    'dsSearchResults = GetMinPriceData(obHotelSearchResponse, objBLLHotelSearch.Room, objBLLHotelSearch.Adult, objBLLHotelSearch.Children, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode)
                    'Session("sDSSearchResultsForOneDMCDynamic") = dsSearchResults


                    dsSearchResults = objBLLHotelSearch.GetSearchDetails()
                    Session("sDSSearchResults") = dsSearchResults
                    BindThreadResultForPageLoad()
                End If
               
            End If





        End If

    End Sub
    ''' <summary>
    ''' dlHotelsSearchResults_ItemDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub dlHotelsSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlHotelsSearchResults.ItemDataBound
        Try

            If iCumulative = 1 Then
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lblPrice As Label = CType(e.Item.FindControl("lblPrice"), Label)
                    Dim lblPriceBy As Label = CType(e.Item.FindControl("lblPriceBy"), Label)
                    Dim lblForRoom As Label = CType(e.Item.FindControl("lblForRoom"), Label)
                    Dim lblIncTax As Label = CType(e.Item.FindControl("lblIncTax"), Label)
                    lblPrice.Visible = False
                    lblPriceBy.Visible = False
                    lblForRoom.Visible = False
                    lblIncTax.Visible = False
                End If
            End If
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Show Hotel Image
                Dim imgHotelImage As Image = CType(e.Item.FindControl("imgHotelImage"), Image)
                Dim lblHotelImage As Label = CType(e.Item.FindControl("lblHotelImage"), Label)
                Dim lblEmail As Label = CType(e.Item.FindControl("lblEmail"), Label)
                lblEmail.Text = lblEmail.Text.Replace(";", "; ").Replace(",", "; ")

                Dim hddlSliderCurcode As HiddenField = CType(e.Item.FindControl("hddlSliderCurcode"), HiddenField)
                Dim lblIncTax As Label = CType(e.Item.FindControl("lblIncTax"), Label)

                lblIncTax.Text = Session("strShowMoreVATText").ToString()

                hdSliderCurrency.Value = " " & hddlSliderCurcode.Value

                Dim hdRatePlanSource As HiddenField = CType(e.Item.FindControl("hdRatePlanSource"), HiddenField)
                If hdRatePlanSource.Value = "OneDMC" Then
                    imgHotelImage.ImageUrl = lblHotelImage.Text ' "ImageDisplay.aspx?FileName=" & lblHotelImage.Text & "&Type=0"
                Else
                    imgHotelImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblHotelImage.Text & "&Type=0"
                End If

                'Show Prefered Button
                Dim lblPreferred As Label = CType(e.Item.FindControl("lblPreferred"), Label)
                Dim btnPreferred As HtmlInputButton = CType(e.Item.FindControl("btnPreferred"), HtmlInputButton)
                If lblPreferred.Text = "1" Then
                    btnPreferred.Visible = True
                Else
                    btnPreferred.Visible = False
                End If
                'Show Hotel Stars
                Dim hdNoOfhotelStars As HiddenField = CType(e.Item.FindControl("hdNoOfhotelStars"), HiddenField)
                Dim dvHotelStars As HtmlGenericControl = CType(e.Item.FindControl("dvHotelStars"), HtmlGenericControl)
                Dim strHotelStarHTML As New StringBuilder

                'Added shahul  26/07/2018

                Dim showalteravailablity As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=3004")
                Dim btnshowallot As Button = CType(e.Item.FindControl("btnalteravailablity"), Button)
                If showalteravailablity <> 0 Then
                    btnshowallot.Visible = True
                    btnshowallot.Text = "+/- " + showalteravailablity + " Days"
                Else
                    btnshowallot.Visible = False

                End If

                strHotelStarHTML.Append(" <nav class='stars'><ul>")
                If hdNoOfhotelStars.Value = "1" Then
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                ElseIf hdNoOfhotelStars.Value = "2" Then
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                ElseIf hdNoOfhotelStars.Value = "3" Then
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                ElseIf hdNoOfhotelStars.Value = "4" Then
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                ElseIf hdNoOfhotelStars.Value = "5" Then
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-active"" aria-hidden=""true""></i></a></li>")
                Else
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                    strHotelStarHTML.Append(" <li><a href='#'><i  class=""fa fa-star fa-inactive"" aria-hidden=""true""></i></a></li>")
                End If

                strHotelStarHTML.Append(" </ul>")
                dvHotelStars.InnerHtml = strHotelStarHTML.ToString

                Dim lblHotelText As Label = CType(e.Item.FindControl("lblHotelText"), Label)
                Dim lbReadMore As LinkButton = CType(e.Item.FindControl("lbReadMore"), LinkButton)
                If lblHotelText.Text.Length > 250 Then
                    lblHotelText.Text = lblHotelText.Text.Substring(0, 249)

                Else
                    lbReadMore.Visible = False
                End If
                If Not Session("sLoginType") Is Nothing Then
                    If Session("sLoginType") <> "RO" Then
                        Dim dvPhone As HtmlGenericControl = CType(e.Item.FindControl("dvPhone"), HtmlGenericControl)
                        dvPhone.Visible = False
                    End If
                End If





            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlHotelsSearchResults_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' Fn_ShowMore
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub Fn_ShowMore(ByVal sender As Object)
        Try
            iRatePlan = 0
            Dim myButton As Button = CType(sender, Button)
            If myButton.Text = "SHOW MORE" Then
                Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
                Dim dlRatePlan As DataList = CType(dlItem.FindControl("dlRatePlan"), DataList)
                Dim lblHotelCode As HiddenField = CType(dlItem.FindControl("lblHotelCode"), HiddenField)
                Dim lblInt_HotelCode As HiddenField = CType(dlItem.FindControl("lblInt_HotelCode"), HiddenField)
                Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)
                Dim lblHotelName As Label = CType(dlItem.FindControl("lblHotelName"), Label)
                Dim lblCityName As Label = CType(dlItem.FindControl("lblCityName"), Label)
                Dim lblPrice As Label = CType(dlItem.FindControl("lblPrice"), Label)
                Dim lblHotelImage As Label = CType(dlItem.FindControl("lblHotelImage"), Label)
                Dim lblRoomTypeWarning As Label = CType(dlItem.FindControl("lblRoomTypeWarning"), Label)
                Dim hdRatePlanSource As HiddenField = CType(dlItem.FindControl("hdRatePlanSource"), HiddenField)
                lblRoomTypeWarning.Visible = False
                myButton.Text = "SHOW LESS"
                'If hdRatePlanSource.Value = "OneDMC" Then
                '    BindRatePlan(dlRatePlan, lblInt_HotelCode.Value, lblRoomTypeWarning, hdRatePlanSource)
                'Else
                '    BindRatePlan(dlRatePlan, lblHotelCode.Value, lblRoomTypeWarning, hdRatePlanSource)
                'End If

                BindRatePlan(dlRatePlan, lblHotelCode.Value, lblRoomTypeWarning, hdRatePlanSource, lblInt_HotelCode.Value)
            Else
                Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
                Dim dlRatePlan As DataList = CType(dlItem.FindControl("dlRatePlan"), DataList)
                Dim lblRoomTypeWarning As Label = CType(dlItem.FindControl("lblRoomTypeWarning"), Label)
                lblRoomTypeWarning.Visible = False
                myButton.Text = "SHOW MORE"
                BindRatePlanWithBlank(dlRatePlan)
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    ''' <summary>
    ''' btnShowMore_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnShowMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Fn_ShowMore(sender)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnShowMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    ''Added shahul 26/07/18
    Private Sub showalternativedates(ByVal partycode As String)
        Try

            Dim dsHotelAllotments As New DataSet
            Dim objBLLHotelSearchRM As New BLLHotelSearch
            objBLLHotelSearchRM = Session("sobjBLLHotelSearch")

            'lblpopuphead.Text =
            Dim showalteravailablity As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=3004")

            If showalteravailablity <> 0 Then

                lblpopuphead.Text = "Available Rooms  +/- " + showalteravailablity + " Days"
            Else
                Exit Sub

            End If
            lblAllotthotel.Text = "Hotel Name : " + objclsUtilities.ExecuteQueryReturnStringValue("select partyname from partymast where partycode='" & partycode & "'")

            dsHotelAllotments = objBLLHotelSearch.GetSearchDetailsSingleHotelAlternatives(objBLLHotelSearchRM.LoginType, objBLLHotelSearchRM.WebuserName, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.RoomString, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.OrderBy, objBLLHotelSearchRM.Availabilty, partycode, objBLLHotelSearchRM.OverridePrice, objBLLHotelSearchRM.EditRequestId, objBLLHotelSearchRM.EditRLineNo, objBLLHotelSearchRM.EditRatePlanId, objBLLHotelSearchRM.MealPlan)
            Session("sdsHotelAllotments") = dsHotelAllotments


            If Not dsHotelAllotments Is Nothing Then
                If dsHotelAllotments.Tables.Count > 0 Then
                    If dsHotelAllotments.Tables(0).Rows.Count > 0 Then
                        Session("sdsHotelAllotments") = dsHotelAllotments
                        Dim dv1 As DataView
                        dv1 = New DataView(dsHotelAllotments.Tables(0))
                        dlHotelallotment.DataSource = dv1.ToTable("Table", True, {"partycode", "roomno", "roomheading"})
                        dlHotelallotment.DataBind()
                        mpHotelAllotment.Show()
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Info, "No Rooms Available")
                        Exit Sub
                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Info, "No Rooms Available")
                    Exit Sub
                End If
            Else
                MessageBox.ShowMessage(Page, MessageType.Info, "No Rooms Available")
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnalteravailablity_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try


    End Sub
    '' Added shahul 26/07/18
    Protected Sub btnalteravailablity_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myButton As Button = CType(sender, Button)
            Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
            Dim dlRatePlan As DataList = CType(dlItem.FindControl("dlRatePlan"), DataList)
            Dim lblHotelCode As HiddenField = CType(dlItem.FindControl("lblHotelCode"), HiddenField)

            showalternativedates(lblHotelCode.Value)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnalteravailablity_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try


    End Sub
    ' '' Added shahul 26/07/18
    'Private Sub Fn_alternativeshotels()
    '    Try
    '        Dim dsHotelAllotments As New DataSet
    '        Dim objBLLHotelSearchRM As New BLLHotelSearch
    '        objBLLHotelSearchRM = Session("sobjBLLHotelSearch")


    '        dsHotelAllotments = objBLLHotelSearch.GetSearchDetailsSingleHotelAlternatives(objBLLHotelSearchRM.LoginType, objBLLHotelSearchRM.WebuserName, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.RoomString, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.OrderBy, objBLLHotelSearchRM.Availabilty, txtHotelCode.Text, objBLLHotelSearchRM.OverridePrice, objBLLHotelSearchRM.EditRequestId, objBLLHotelSearchRM.EditRLineNo, objBLLHotelSearchRM.EditRatePlanId, objBLLHotelSearchRM.MealPlan)
    '        Session("sdsHotelAllotments") = dsHotelAllotments


    '        If Not dsHotelAllotments Is Nothing Then
    '            If dsHotelAllotments.Tables.Count > 0 Then
    '                If dsHotelAllotments.Tables(0).Rows.Count > 0 Then
    '                    Session("sdsHotelAllotments") = dsHotelAllotments
    '                    gvHotelAllotment.DataSource = dsHotelAllotments.Tables(0)
    '                    gvHotelAllotment.DataBind()
    '                    '  mpHotelAllotmentnew.Show()
    '                Else
    '                    MessageBox.ShowMessage(Page, MessageType.Info, "No Alternative Dates")
    '                    Exit Sub
    '                End If
    '            Else
    '                MessageBox.ShowMessage(Page, MessageType.Info, "No Alternative Dates")
    '                Exit Sub
    '            End If
    '        Else
    '            MessageBox.ShowMessage(Page, MessageType.Info, "No Alternative Dates")
    '            Exit Sub
    '        End If

    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnalteravailablity_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try




    'End Sub

    Private Sub FindCumilative()
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                'Dim objBLLHotelSearch As New BLLHotelSearch
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
            End If
        End If
    End Sub

    ''' <summary>
    ''' btnBookNow_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBookNow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            If Session("State") = "New" Then
                If Not Session("sAgentCode") Is Nothing Then


                    objBLLHotelSearch = New BLLHotelSearch
                    objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
                    objBLLHotelSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLHotelSearch.CustomerCode, Session("sAgentCode"))
                    objBLLHotelSearch.OBdiv_code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
                    '' Added shhaul 05/12/18
                    Dim lstallowcancelperiod As Integer = objclsUtilities.ExecuteQueryReturnSingleValue("select isnull(allowcancelperiod,0) allowcancelperiod from agentmast_creditlimits(nolock) where agentcode='" & Session("sAgentCode") & "'")

                    objBLLHotelSearch.OBsourcectrycode = objBLLHotelSearch.SourceCountryCode
                    objBLLHotelSearch.OBreqoverride = hdOveride.Value 'IIf(chkOveridePrice.Checked, "1", "0")
                    objBLLHotelSearch.OBagentref = ""
                    objBLLHotelSearch.OBcolumbusref = ""
                    objBLLHotelSearch.OBremarks = ""
                    objBLLHotelSearch.userlogged = Session("GlobalUserName")

                    If Not Session("sobjResParam") Is Nothing Then
                        objResParam = Session("sobjResParam")
                        objBLLHotelSearch.SubUserCode = objResParam.SubUserCode
                    End If


                    Dim btnbooknow As Button = CType(sender, Button)
                    Dim rowid As Integer = 0

                    Dim gvhotelroomtyperow As GridViewRow
                    gvhotelroomtyperow = CType(btnbooknow.NamingContainer, GridViewRow)
                    Dim gvHotelRoomType As GridView = CType((gvhotelroomtyperow.Parent.Parent), GridView)
                    rowid = gvhotelroomtyperow.RowIndex


                    Dim lblRMPartyCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMPartyCode"), Label).Text
                    Dim lblRMRatePlanId As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMRatePlanId"), Label).Text
                    Dim lblRMRatePlanName As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMRatePlanName"), Label).Text
                    Dim lblRMRoomTypeCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMRoomTypeCode"), Label).Text
                    Dim lblRMcatCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMcatCode"), Label).Text
                    Dim lblRMSharingOrExtraBed As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMSharingOrExtraBed"), Label).Text
                    Dim lblBoardBasis As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblBoardBasis"), Label).Text
                    Dim lblSaleValue As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblSaleValue"), Label).Text
                    Dim lblRMMealPlanCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMMealPlanCode"), Label).Text
                    Dim lblRMCurcode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMCurcode"), Label).Text
                    Dim lblRoomClassCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRoomClassCode"), Label).Text
                    Dim lblSupAgentCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblSupAgentCode"), Label).Text
                    Dim lblCostValue As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblCostValue"), Label).Text
                    Dim lblCostCurrCode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblCostCurrCode"), Label).Text
                    Dim lblAvailable As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblAvailable"), Label).Text
                    Dim lblCompCust As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblCompCust"), Label).Text
                    Dim lblCompSupp As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblCompSupp"), Label).Text
                    Dim lblComparrtrf As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblComparrtrf"), Label).Text

                    Dim lblCompdeptrf As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblCompdeptrf"), Label).Text
                    Dim lblAdultEb As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblAdultEb"), Label).Text
                    Dim lblChildEb As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblChildEb"), Label).Text
                    Dim lblRMWlCurcode As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMWlCurcode"), Label).Text

                    Dim hdRoomRatePlanSource As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdRoomRatePlanSource"), Label)
                    Dim hdInt_RoomtypeCodes As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_RoomtypeCodes"), Label)
                    Dim hdInt_RoomtypeNames As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_RoomtypeNames"), Label)
                    Dim hdInt_Roomtypes As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_Roomtypes"), Label)

                    Dim hdInt_costprice As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_costprice"), Label)
                    Dim hdInt_costcurrcode As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_costcurrcode"), Label)

                    Dim hdInt_partycode As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_partycode"), Label)
                    Dim hdInt_rmtypecode As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_rmtypecode"), Label)
                    Dim hdInt_mealcode As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdInt_mealcode"), Label)

                    Dim hdOffercode As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdOffercode"), Label)
                    Dim hdAccomodationcode As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdAccomodationcode"), Label)
                    Dim lblRMRoomId As String = CType(gvHotelRoomType.Rows(rowid).FindControl("lblRMRoomId"), Label).Text
                      
                    If chkShifting.Checked = True Then
                        objBLLHotelSearch.Shifting = "1"
                        Dim strShiftCode As String() = txtShiftHotelCode.Text.Trim.Split("|")
                        objBLLHotelSearch.ShiftingCode = strShiftCode(0)
                        objBLLHotelSearch.ShiftingLineNo = strShiftCode(1)
                    Else
                        objBLLHotelSearch.Shifting = "0"
                        objBLLHotelSearch.ShiftingCode = ""
                    End If

                    If Session("sLoginType") <> "RO" And lblAvailable = 1 And lstallowcancelperiod = 0 Then  '' Added shhaul 05/12/18
                        Dim strGetCancelDays As String = objBLLHotelSearch.GetCancelDays(lblRMPartyCode, lblRMRoomTypeCode, lblRMMealPlanCode, lblRMRatePlanId, objBLLHotelSearch.AgentCode, objBLLHotelSearch.OBsourcectrycode, txtCheckIn.Text, txtCheckOut.Text)
                        If strGetCancelDays <= 1 Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please note the booking is falling within the cancellation policy of the hotel. Please contact Reservation officer for processing the booking.")

                            Exit Sub
                        End If
                    End If

                    'Guaranteed extra bed


                    Dim hdExtraBedRequired As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdExtraBedRequired"), Label)
                    Dim hdExtraBedValue As Label = CType(gvHotelRoomType.Rows(rowid).FindControl("hdExtraBedValue"), Label)

                    If hdExtraBedRequired.Text = "" And hdRoomRatePlanSource.Text <> "OneDMC" Then


                        Dim strNoOfExtraBed As String = Val(lblAdultEb) + Val(lblChildEb)
                        If strNoOfExtraBed > 0 Then
                            Dim dt As New DataTable
                            dt = objBLLHotelSearch.GetNew_booking_OneTimePay(lblRMPartyCode, lblRMRoomTypeCode, lblRMMealPlanCode, lblRMRatePlanId, HttpContext.Current.Session("sAgentCode").ToString, txtCountryCode.Text, txtCheckIn.Text, txtCheckOut.Text, strNoOfExtraBed, "", "")
                            If dt.Rows.Count > 0 Then
                                Dim mpAdditionalCharges As AjaxControlToolkit.ModalPopupExtender = CType(gvHotelRoomType.Rows(rowid).FindControl("mpAdditionalCharges"), AjaxControlToolkit.ModalPopupExtender)

                                txtNoOfExtraBed.Text = dt.Rows(0)("NoEB").ToString
                                txtExtraBedUnitPrice.Text = dt.Rows(0)("UnitPrice").ToString
                                txtExtraBedTotalPrice.Text = dt.Rows(0)("TotalPrice").ToString
                                lblEBBookingCode.Text = dt.Rows(0)("BookingCode").ToString
                                lblAdditionalCharges.Text = "Do you want add guaranteed extra bed?"

                                Dim slbAdditionalCharges As LinkButton = CType(gvHotelRoomType.Rows(rowid).FindControl("lbAdditionalCharges"), LinkButton)
                                Session("slbAdditionalCharges") = slbAdditionalCharges

                                btnAddChargeYes.Visible = True
                                btnAddChargeNo.Visible = True
                                mpAdditionalCharges.Show()
                                Exit Sub
                            End If

                        End If

                    End If


                    Dim strFlagSpclEvent As String = ""

                    If hdRoomRatePlanSource.Text <> "OneDMC" Then

                        Dim dsSpecialEvents As New DataSet
                        Dim dsSpecialEventsNew As New DataSet
                        Dim lbSpecialEvents As LinkButton = CType(gvHotelRoomType.Rows(rowid).FindControl("lbSpecialEvents"), LinkButton)
                        dsSpecialEventsNew = GetSpecialEventDetails(lbSpecialEvents, gvHotelRoomType.Rows(rowid), "")

                        If Not dsSpecialEventsNew Is Nothing Then
                            If dsSpecialEventsNew.Tables.Count > 0 Then
                                Dim strSplcode As String = ""
                                For j As Integer = 0 To dsSpecialEventsNew.Tables(2).Rows.Count - 1
                                    '' Commented shhaul 03/11/18
                                    'If strFlagSpclEvent = "0" Then 
                                    '    Exit For
                                    'End If
                                    strSplcode = Format(CType(dsSpecialEventsNew.Tables(2).Rows(j)("fromdate").ToString, Date), "yyyy/MM/dd") & "," & dsSpecialEventsNew.Tables(2).Rows(j)("spleventcode").ToString
                                    dsSpecialEvents = GetSpecialEventDetails(lbSpecialEvents, gvHotelRoomType.Rows(rowid), strSplcode)
                                    If Not dsSpecialEvents Is Nothing Then

                                        If dsSpecialEvents.Tables.Count > 0 Then
                                            Dim dv As DataView = New DataView(dsSpecialEvents.Tables(1))
                                            dv.RowFilter = "compulsorytype='0' OR compulsorytype='1' "
                                            If dv.Count > 0 Then
                                                If Not Session("sdtSelectedSpclEvent") Is Nothing Then
                                                    Dim dtSp As DataTable = Session("sdtSelectedSpclEvent")
                                                    Dim strSplistcode As String = ""
                                                    Dim strSpLineno As String = ""
                                                    Dim strSpEventCode As String = ""
                                                    Dim strSpEventDate As String = ""
                                                    Dim strSpPaxtype As String = ""
                                                    Dim strSpChildAge As String = ""
                                                    For i As Integer = 0 To dv.Count - 1
                                                        strSplistcode = dv.Item(i)("splistcode").ToString
                                                        strSpLineno = dv.Item(i)("splineno").ToString
                                                        strSpEventCode = dv.Item(i)("spleventcode").ToString
                                                        strSpEventDate = dv.Item(i)("spleventdate").ToString
                                                        strSpPaxtype = dv.Item(i)("paxtype").ToString
                                                        strSpChildAge = dv.Item(i)("childage").ToString
                                                        Dim foundRow As DataRow

                                                        foundRow = dtSp.Select("PartyCode='" & lblRMPartyCode.Trim & "' AND RoomTypeCode ='" & lblRMRoomTypeCode.Trim & "' AND MealPlanCode='" & lblRMMealPlanCode.Trim & "' AND  CatCode='" & lblRMcatCode.Trim & "'  AND RatePlanId='" & lblRMRatePlanId.Trim & "' AND AccCode='" & lblRMSharingOrExtraBed.Trim & "' AND splistcode='" & strSplistcode & "' AND splineno='" & strSpLineno & "'  AND spleventcode='" & strSpEventCode & "'  AND spleventdate='" & strSpEventDate & "'  AND paxtype='" & strSpPaxtype & "'  and childage='" & strSpChildAge & "' ").FirstOrDefault

                                                        If foundRow Is Nothing Then
                                                            strFlagSpclEvent = "1"
                                                            Exit For
                                                            'GoTo showevent '' Added shahul 03/11/2018
                                                        Else
                                                            GoTo eventclose
                                                            'strFlagSpclEvent = "0"
                                                            'Exit For
                                                        End If
                                                    Next
                                                Else
                                                    strFlagSpclEvent = "1"
                                                    Exit For
                                                End If
                                            Else
                                                strFlagSpclEvent = "0"
                                                ' Exit For  '' Commented shhaul 03/11/18
                                            End If
                                        End If

                                    End If
                                Next

                                If strFlagSpclEvent = "1" Then

                                    If dsSpecialEventsNew.Tables.Count > 0 Then
                                        If dsSpecialEventsNew.Tables(0).Rows.Count > 0 Then
                                            Session("sdsSpecialEvents") = dsSpecialEventsNew
                                            dlSpecialEvents.DataSource = dsSpecialEventsNew.Tables(0)
                                            dlSpecialEvents.DataBind()
                                            Dim mpSpecialEvents As AjaxControlToolkit.ModalPopupExtender = CType(gvHotelRoomType.Rows(rowid).FindControl("mpSpecialEvents"), AjaxControlToolkit.ModalPopupExtender)
                                            mpSpecialEvents.Show()
                                            Exit Sub
                                        Else
                                            ' MessageBox.ShowMessage(Page, MessageType.Info, "No event")
                                        End If

                                    End If
                                End If

                            End If
                        End If
                    End If




eventclose:
                    objBLLHotelSearch.OBrequestid = GetNewOrExistingRequestId()

                    Dim strHotelLineNo As String = ""
                    If ViewState("vRLineNo") Is Nothing Then
                        strHotelLineNo = objBLLCommonFuntions.GetBookingRowLineNo(objBLLHotelSearch.OBrequestid, "HOTEL")
                    Else
                        strHotelLineNo = ViewState("vRLineNo")
                    End If

                    ' added by abin on 20181013 -- start
                    FindCumilative()
                    Dim iCumulativeUser As Integer = 0
                    If Session("sLoginType") = "RO" Then
                        Dim strQuery As String = "select count(agentcode)CNT from agentmast(nolock) where bookingengineratetype='CUMULATIVE' and agentcode=(select min(agentcode) from  booking_header(nolock) where requestid='" & objBLLHotelSearch.OBrequestid & "')"
                        iCumulativeUser = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

                    End If

                    'changed by mohamed on 29/08/2018
                    Dim lsSqlQry As String = ""
                    Dim lsFutureDateAvailable As String = ""
                    Dim strShiftingWarning = "0"
                    If hdBookingEngineRateType.Value = "1" Or iCumulativeUser > 0 Then
                        If txtShiftHotelCode.Text.Trim = "" Then 'And ViewState("vRLineNo") Is Nothing
                            Dim dsFt As DataSet
                            lsSqlQry = "execute sp_check_future_checkin_booking '" & objBLLHotelSearch.OBrequestid & "'," & strHotelLineNo & ",'" & objBLLHotelSearch.CheckIn & "','" & objBLLHotelSearch.CheckOut & "',''"
                            dsFt = objclsUtilities.GetDataFromDataset(lsSqlQry)
                            If dsFt.Tables.Count > 0 Then
                                If dsFt.Tables(0).Rows.Count >= 1 Then
                                    lsFutureDateAvailable = dsFt.Tables(0).Rows(0)("AvailStatus")
                                    If lsFutureDateAvailable.Trim <> "" Then
                                        'MessageBox.ShowMessage(Page, MessageType.Errors, "Shifting/Add More Room in current hotel is not selected when additional hotel is added for future date")
                                        '  MessageBox.ShowMessage(Page, MessageType.Warning, "Check In / Check out should be related to earlier hotel booked")
                                        ' Exit Sub
                                        strShiftingWarning = "1"
                                    End If
                                End If
                            End If
                        End If
                    End If
                    '  added by abin on 20181013 -- end

                    Dim dExtrBedTotal As Decimal = 0
                    Dim strOneTimePayXML As New StringBuilder
                    If hdExtraBedRequired.Text = "YES" And hdExtraBedValue.Text <> "" Then
                        Dim str As String() = hdExtraBedValue.Text.Split("*")

                        strOneTimePayXML.Append("<DocumentElement>")
                        strOneTimePayXML.Append("<Olineno>1</Olineno>")
                        strOneTimePayXML.Append("<FunctionType>" & "Guaranteed Extra Bed" & "</FunctionType>")
                        strOneTimePayXML.Append("<OptionType>" & "Per EB" & "</OptionType>")
                        strOneTimePayXML.Append("<Unit>" & str(0) & "</Unit>")
                        strOneTimePayXML.Append("<UnitPrice>" & str(1) & "</UnitPrice>")
                        strOneTimePayXML.Append("<TotalPrice>" & Val(str(0)) * Val(str(1)) & "</TotalPrice>")
                        strOneTimePayXML.Append("<Remarks>" & str(2) & "</Remarks>")
                        strOneTimePayXML.Append("</DocumentElement>")

                        dExtrBedTotal = Val(str(0)) * Val(str(1))

                    End If


                    ' objBLLHotelSearch.OBrlineno = GetRowLineNumber()
                    objBLLHotelSearch.OBrlineno = strHotelLineNo

                    objBLLHotelSearch.OBCheckin = objBLLHotelSearch.CheckIn
                    objBLLHotelSearch.OBCheckout = objBLLHotelSearch.CheckOut
                    ' objBLLHotelSearch.OBnoofrooms = ddlRoom.SelectedValue
                    'objBLLHotelSearch.OBadults = Val(ddlAdult.SelectedValue)
                    'objBLLHotelSearch.OBchild = Val(ddlChildren.SelectedValue)

                    objBLLHotelSearch.OBnoofrooms = objBLLHotelSearch.Room
                    objBLLHotelSearch.OBchildages = objBLLHotelSearch.ChildAgeString
                    objBLLHotelSearch.OBsupagentcode = lblSupAgentCode
                    objBLLHotelSearch.OBpartycode = lblRMPartyCode
                    objBLLHotelSearch.OBrateplanid = lblRMRatePlanId
                    objBLLHotelSearch.OBrateplanname = lblRMRatePlanName
                    'If hdInt_RoomtypeCodes.Text <> "OneDMC" Then
                    '    Dim dtRoomTypes As DataTable = objBLLHotelSearch.GetOrCreateRoomType(lblRMPartyCode, hdInt_Roomtypes.Text)
                    '    objBLLHotelSearch.OBrmtypcode = lblRMRoomTypeCode
                    'Else
                    objBLLHotelSearch.OBrmtypcode = lblRMRoomTypeCode
                    'End If

                    objBLLHotelSearch.OBroomclasscode = lblRoomClassCode
                    objBLLHotelSearch.OBrmcatcode = lblRMcatCode
                    objBLLHotelSearch.OBmealplans = lblRMMealPlanCode
                    objBLLHotelSearch.OBsalevalue = Convert.ToDecimal(lblSaleValue) + dExtrBedTotal
                    objBLLHotelSearch.OBsalecurrcode = lblRMCurcode
                    objBLLHotelSearch.OBcostvalue = lblCostValue
                    objBLLHotelSearch.OBcostcurrcode = lblCostCurrCode
                    objBLLHotelSearch.OBwlsalevalue = Convert.ToDecimal(lblSaleValue) + dExtrBedTotal
                    objBLLHotelSearch.OBavailable = lblAvailable
                    objBLLHotelSearch.OBcomp_cust = lblCompCust
                    objBLLHotelSearch.OBcomp_supp = lblCompSupp
                    objBLLHotelSearch.OBcomparrtrf = lblComparrtrf
                    objBLLHotelSearch.OBcompdeptrf = lblCompdeptrf
                    objBLLHotelSearch.SharingOrExtraBed = lblRMSharingOrExtraBed
                    objBLLHotelSearch.NoOfAdultEb = lblAdultEb
                    objBLLHotelSearch.NoOfChildEb = lblChildEb
                    objBLLHotelSearch.wlCurrCode = lblRMWlCurcode


                    objBLLHotelSearch.RatePlanSource = hdRoomRatePlanSource.Text
                    objBLLHotelSearch.Int_RoomtypeCodes = hdInt_RoomtypeCodes.Text
                    objBLLHotelSearch.Int_RoomtypeNames = hdInt_RoomtypeNames.Text
                    objBLLHotelSearch.Int_Roomtypes = hdInt_Roomtypes.Text

                    objBLLHotelSearch.Int_PartyCode = hdInt_partycode.Text
                    objBLLHotelSearch.Int_rmtypecode = hdInt_rmtypecode.Text
                    objBLLHotelSearch.Int_mealcode = hdInt_mealcode.Text

                    objBLLHotelSearch.Offercode = hdOffercode.Text
                    objBLLHotelSearch.Accomodationcode = hdAccomodationcode.Text

                    objBLLHotelSearch.Int_costprice = hdInt_costprice.Text
                    objBLLHotelSearch.Int_costcurrcode = hdInt_costcurrcode.Text

                    Dim dsPriceBreakupNew As New DataSet
                    Dim pricebreakuptemp As New DataSet

                    If Not Session("sdsHotelRoomTypes") Is Nothing Then
                        dsPriceBreakupNew = CType(Session("sdsHotelRoomTypes"), DataSet)
                        Dim dvpricebreakupres As DataView = New DataView(dsPriceBreakupNew.Tables(1))

                        If hdRoomRatePlanSource.Text = "OneDMC" Then
                            If lblRMRatePlanId <> "" And lblRMPartyCode <> "" Then ' And lblRoomClassCode <> ""  AND roomclasscode IN ('" & lblRoomClassCode & "') 
                                dvpricebreakupres.RowFilter = "RatePlanId='" & lblRMRatePlanId & "' AND PartyCode='" & lblRMPartyCode & "'  AND rmtypcode='" + lblRMRoomTypeCode + "'  AND mealplans='" + lblRMMealPlanCode + "'    AND sharingorextrabed='" + lblRMSharingOrExtraBed + "' AND mealcode='" + lblRMMealPlanCode + "' AND roomId='" + lblRMRoomId + "' " 'AND rmcatcode='" + lblRMcatCode + "' 
                            End If
                        Else
                            If lblRMRatePlanId <> "" And lblRMPartyCode <> "" And lblRoomClassCode <> "" Then
                                dvpricebreakupres.RowFilter = "RatePlanId='" & lblRMRatePlanId & "' AND PartyCode='" & lblRMPartyCode & "'  AND rmtypcode='" + lblRMRoomTypeCode + "'  AND mealplans='" + lblRMMealPlanCode + "'   AND roomclasscode IN ('" & lblRoomClassCode & "')   AND sharingorextrabed='" + lblRMSharingOrExtraBed + "' AND mealcode='" + lblRMMealPlanCode + "' " 'AND rmcatcode='" + lblRMcatCode + "' 
                            End If
                        End If
                   
                        objBLLHotelSearch.obpricebreakuptemp = (objclsUtilities.GenerateXML_FromDataTable(dvpricebreakupres.ToTable)).Replace("NewDataSet", "DocumentElement").Replace("T00:00:00+04:00", "").Replace("</Table1>", "</Table>").Replace("<Table1>", "<Table>")

                    End If

                    'For special event saving
                    Dim strSpecialEvent As New StringBuilder
                    If Not Session("sdtSelectedSpclEvent") Is Nothing Then
                        Dim dtSp As DataTable = Session("sdtSelectedSpclEvent")
                        If dtSp.Rows.Count > 0 Then
                            Dim dvSelected As DataView = New DataView(dtSp)

                            dvSelected.RowFilter = "PartyCode='" & lblRMPartyCode.Trim & "' AND RoomTypeCode ='" & lblRMRoomTypeCode.Trim & "' AND MealPlanCode='" & lblRMMealPlanCode.Trim & "' AND  CatCode='" & lblRMcatCode.Trim & "'  AND RatePlanId='" & lblRMRatePlanId.Trim & "'  AND AccCode='" & lblRMSharingOrExtraBed.Trim & "'  "

                            strSpecialEvent.Append("<DocumentElement>")
                            For i As Integer = 0 To dvSelected.Count - 1
                                strSpecialEvent.Append("<Table>")
                                strSpecialEvent.Append("<evlineno>" & (i + 1).ToString & "</evlineno>")
                                strSpecialEvent.Append("<splistcode>" & dvSelected.Item(i)("splistcode").ToString & "</splistcode>")
                                strSpecialEvent.Append("<splineno>" & dvSelected.Item(i)("splineno").ToString & "</splineno>")
                                strSpecialEvent.Append("<spleventcode>" & dvSelected.Item(i)("spleventcode").ToString & "</spleventcode>")
                                strSpecialEvent.Append("<compulsorytype>" & dvSelected.Item(i)("compulsorytype").ToString & "</compulsorytype>")
                                strSpecialEvent.Append("<spleventdate>" & Format(CType(dvSelected.Item(i)("spleventdate").ToString, Date), "yyyy/MM/dd") & "</spleventdate>")
                                strSpecialEvent.Append("<paxtype>" & dvSelected.Item(i)("paxtype").ToString & "</paxtype>")
                                strSpecialEvent.Append("<childage>" & dvSelected.Item(i)("childage").ToString & "</childage>")
                                strSpecialEvent.Append("<noofpax>" & dvSelected.Item(i)("noofpax").ToString & "</noofpax>")
                                strSpecialEvent.Append("<paxrate>" & dvSelected.Item(i)("paxrate").ToString & "</paxrate>")
                                strSpecialEvent.Append("<spleventvalue>" & dvSelected.Item(i)("spleventvalue").ToString & "</spleventvalue>")
                                strSpecialEvent.Append("<wlpaxrate>" & dvSelected.Item(i)("wlpaxrate").ToString & "</wlpaxrate>")
                                strSpecialEvent.Append("<wlspleventvalue>" & dvSelected.Item(i)("wlspleventvalue").ToString & "</wlspleventvalue>")
                                strSpecialEvent.Append("<salecurrcode>" & dvSelected.Item(i)("salecurrcode").ToString & "</salecurrcode>")
                                strSpecialEvent.Append("<paxcost>" & dvSelected.Item(i)("paxcost").ToString & "</paxcost>")
                                strSpecialEvent.Append("<spleventcostvalue>" & dvSelected.Item(i)("spleventcostvalue").ToString & "</spleventcostvalue>")
                                strSpecialEvent.Append("<costcurrcode>" & dvSelected.Item(i)("costcurrcode").ToString & "</costcurrcode>")
                                strSpecialEvent.Append("<comp_cust>" & dvSelected.Item(i)("comp_cust").ToString & "</comp_cust>")
                                strSpecialEvent.Append("<comp_supp>" & dvSelected.Item(i)("comp_supp").ToString & "</comp_supp>")
                                strSpecialEvent.Append("<roomno>" & dvSelected.Item(i)("roomno").ToString & "</roomno>")
                                strSpecialEvent.Append("<wlcurrcode>" & dvSelected.Item(i)("wlcurrcode").ToString & "</wlcurrcode>")
                                strSpecialEvent.Append("<wlconvrate>" & dvSelected.Item(i)("wlconvrate").ToString & "</wlconvrate>")
                                strSpecialEvent.Append("<wlmarkupperc>" & dvSelected.Item(i)("wlmarkupperc").ToString & "</wlmarkupperc>")
                                strSpecialEvent.Append("</Table>")
                            Next
                            strSpecialEvent.Append("</DocumentElement>")
                        End If

                    End If
                    objBLLHotelSearch.SpecialEventXML = strSpecialEvent.ToString



                    'End
                    objBLLHotelSearch.OneTimePayXML = strOneTimePayXML.ToString
                    If Session("sEditRequestId") Is Nothing Then
                        objBLLHotelSearch.AmendMode = "0"
                    Else
                        objBLLHotelSearch.AmendMode = "1"
                    End If
                    Dim strBookingStatus As String = objBLLHotelSearch.savingbookingintemp()
                    If strBookingStatus = "Success" Then

                        Session("sobjBLLHotelSearchActive") = Session("sobjBLLHotelSearch")
                        Session("sRequestId") = objBLLHotelSearch.OBrequestid



                        If Not Session("sdtSelectedSpclEvent") Is Nothing Then
                            Session("sdtSelectedSpclEvent") = Nothing
                        End If

                        If trd1.IsAlive Then
                            trd1.Abort()
                        End If
                        If trd2.IsAlive Then
                            trd2.Abort()
                        End If
                        If trd4.IsAlive Then
                            trd4.Abort()
                        End If
                        Session("Status_dtAdultChilds") = Nothing '*** Danny 01/09/2018

                        If strShiftingWarning = "1" Then
                            Response.Redirect("MoreServices.aspx?ShiftWarning=1")
                        Else
                            Response.Redirect("MoreServices.aspx")
                        End If
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Errors, strBookingStatus)

                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnBookNow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
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
    ''' GetShiftingRoomAdultChild
    ''' </summary> 
    ''' <param name="strCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetShiftingRoomAdultChild(ByVal strCode As String) As String
        Dim strCodes As String() = strCode.Split("|")
        Dim strRequestid As String = HttpContext.Current.Session("sRequestId")
        Dim strLineNo As String = strCodes(1)
        Dim strRoomString As String = ""
        'changed by mohamed on 05/04/2018
        'strRoomString = clsUtilities.SharedExecuteQueryReturnStringValue("select  RoomString +'**'+ CONVERT(varchar(10),checkout,103) as rm from booking_hotel_detailtemp(nolock)  where requestid= '" & strRequestid & "' and  rlineno='" & strLineNo & "'")
        strRoomString = Replace(strCodes(2), "&s", "|")
        Return strRoomString
    End Function
    Dim ds As DataSet
    ''' <summary>
    ''' gvHotelRoomType_RowDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvHotelRoomType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If hdBookingEngineRateType.Value = "1" Then
                If (e.Row.RowType = DataControlRowType.Header) Then
                    e.Row.Cells(3).Visible = False
                End If
            End If
            If (e.Row.RowType = DataControlRowType.Header) Then
                'Dim lblForRoom As Label = CType(e.Row.FindControl("lblForRoom"), Label)
                'lblForRoom.Text = "Total Value " & strTotalValueHeading
                Dim lbHeaderTotalValue As LinkButton = CType(e.Row.FindControl("lbHeaderTotalValue"), LinkButton)
                lbHeaderTotalValue.Text = "Total Value " & strTotalValueHeading
            End If
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                ' Dim lblTotalPrice As Label = CType(e.Row.FindControl("lblTotalPrice"), Label)
                ' Dim txtTotalPrice As TextBox = CType(e.Row.FindControl("txtTotalPrice"), TextBox)



                Dim lbTotalprice As LinkButton = CType(e.Row.FindControl("lbTotalprice"), LinkButton)
                Dim lbwlTotalprice As LinkButton = CType(e.Row.FindControl("lbwlTotalprice"), LinkButton)

             


                'Added by abin on 20190714
                Dim lblmealupgradefrom As Label = CType(e.Row.FindControl("lblmealupgradefrom"), Label)
                Dim lblBoardBasis As Label = CType(e.Row.FindControl("lblBoardBasis"), Label)
                If lblmealupgradefrom.Text <> "" Then 'And lblBoardBasis.Text <> "" Then
                    If lblBoardBasis.Text <> lblmealupgradefrom.Text Then
                        lblBoardBasis.Text = lblmealupgradefrom.Text + " = " + lblBoardBasis.Text
                    End If

                End If
                'Added by abin on 20190714 -- end
                Dim lblAvailable As Label = CType(e.Row.FindControl("lblAvailable"), Label)
                Dim lblCurrentSeletion As Label = CType(e.Row.FindControl("lblCurrentSeletion"), Label)
                If lblCurrentSeletion.Text = "1" Then
                    e.Row.BackColor = Drawing.Color.AntiqueWhite

                End If
                Dim btnBookNow As Button = CType(e.Row.FindControl("btnBookNow"), Button)
                If lblAvailable.Text = "1" Then
                    btnBookNow.Text = "Book Now"
                    btnBookNow.CssClass = "roomtype-buttons-booknow"
                Else
                    btnBookNow.Text = "On Request"
                    btnBookNow.CssClass = "roomtype-buttons-onrequest"
                End If
                If lblAvailable.Text = "" Then
                    lblAvailable.Text = "0"
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
                Dim hdRoomRatePlanSource As Label = CType(e.Row.FindControl("hdRoomRatePlanSource"), Label)

                If hdRoomRatePlanSource.Text = "OneDMC" Then
                    Dim lbAdditionalCharges As LinkButton = CType(e.Row.FindControl("lbAdditionalCharges"), LinkButton)

                    Dim lbMinLengthStay As LinkButton = CType(e.Row.FindControl("lbMinLengthStay"), LinkButton)
                    Dim lbSpecialEvents As LinkButton = CType(e.Row.FindControl("lbSpecialEvents"), LinkButton)
                    lbAdditionalCharges.Visible = False
                    lbOffer.Visible = False
                    lbMinLengthStay.Visible = False
                    lbSpecialEvents.Visible = False
                End If
                Dim strVATMessage As String = "     IMPORTANT NOTICE \n ================= \n\nPlease note the  option of  Proforma  Invoice has been  temporarily  suspended  due to VAT  implementation in  UAE with effect from 01st January 2018. Hence the  final confirmation  and Invoice will be sent to your email ID by our accounts team & the value of this Invoice should be treated as full & final. \n\n Apologies for the inconvenience. \n\n Admin, \n "
                If objBLLHotelSearch.OBdiv_code = "01" Then
                    strVATMessage = strVATMessage & "Mahce  Tourism. \n"
                Else
                    strVATMessage = strVATMessage & "Mahce Tourism.\n"
                End If
                btnBookNow.Attributes.Add("OnClick", "return VATNewAlert('" + strVATMessage + "')")


                '*** Confirm message for VAT messsage 
                Dim lblVATExclude As Label = CType(e.Row.FindControl("lblVATExclude"), Label)
                If lblVATExclude.Text = "1" Then
                    Dim dCheckOut As Date = CType(txtCheckOut.Text, Date)
                    Dim dJan2018 As Date = CType("01/01/2018", Date)
                    If dCheckOut.Date >= dJan2018.Date Then
                        If Session("sLoginType") <> "RO" Then
                            btnBookNow.Attributes.Add("OnClick", "return VATConfirm('" + strVATMessage + "')")
                        Else
                            btnBookNow.Attributes.Add("OnClick", "return VATAlert('" + strVATMessage + "')")
                        End If

                    End If

                End If

                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    lbTotalprice.Attributes.Add("style", "display:none")
                    lbwlTotalprice.Attributes.Add("style", "display:block")
                Else
                    lbTotalprice.Attributes.Add("style", "display:block")
                    lbwlTotalprice.Attributes.Add("style", "display:none")

                End If
                If hdBookingEngineRateType.Value = "1" And Session("sLoginType") <> "RO" Then
                    lbTotalprice.Attributes.Add("style", "display:none")
                    lbwlTotalprice.Attributes.Add("style", "display:none")

                End If

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: gvHotelRoomType_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbRatePlan_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                myLinkButton.Text = "Rate Plan: " & hdRatePlan.Value & " - <span class='rateplan-show-hide'>HIDE</span>"
                Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)

                'changed by mohamed on 10/09/2018 included rateplanname in filter
                BindgvRoomType(gvHotelRoomType, hdRatePlanCode.Value, hdRatePlanHotelCode.Value, "0", "0", hdRatePlan.Value)
            Else
                hdShow.Value = "SHOW"
                Dim hdRatePlan As HiddenField = CType(dlItem.FindControl("hdRatePlan"), HiddenField)
                myLinkButton.Text = "Rate Plan: " & hdRatePlan.Value & " - <span class='rateplan-show-hide'>SHOW</span>"
                Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)
                BindgvRoomTypeWithBlank(gvHotelRoomType)
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbRatePlan_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    ''' <summary>
    ''' dlRatePlan_ItemDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
                    myLinkButton.Text = "Rate Plan: " & hdRatePlan.Value & " - <span class='rateplan-show-hide'>HIDE</span>"
                    Dim gvHotelRoomType As GridView = CType(dlItem.FindControl("gvHotelRoomType"), GridView)

                    'changed by mohamed on 10/09/2018 included rateplanname in filter
                    BindgvRoomType(gvHotelRoomType, hdRatePlanCode.Value, hdRatePlanHotelCode.Value, "0", "0", hdRatePlan.Value)
                Else
                    Dim myLinkButton As LinkButton = CType(e.Item.FindControl("lbRatePlan"), LinkButton)
                    Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
                    Dim hdRatePlan As HiddenField = CType(dlItem.FindControl("hdRatePlan"), HiddenField)
                    myLinkButton.Text = "Rate Plan: " & hdRatePlan.Value & " - <span class='rateplan-show-hide'>SHOW</span>"
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlRatePlan_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnSearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'changed by mohamed on 12/02/2018
            txtSearchHotel.Text = ""
            'txtSearchHotel.Enabled = True
            'btnHotelTextSearch.Enabled = True
            'ddlSorting.Enabled = True

            If Not Session("sdsHotelRoomTypes") Is Nothing Then
                Session("sdsHotelRoomTypes") = Nothing
            End If

            fnHotelSearch()
            Session("sdtSelectedSpclEvent") = Nothing


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' BindDestName
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <remarks></remarks>
    Private Sub BindDestName(ByVal dataTable As DataTable)
        Dim dvDest As DataView = New DataView(dataTable)
        dvDest.Sort = "sectorname asc"
        dataTable = dvDest.ToTable(True)

        If dataTable.Rows.Count > 0 Then
            chkSectors.DataSource = dataTable
            chkSectors.DataTextField = "sectorname"
            chkSectors.DataValueField = "sectorcode"
            chkSectors.DataBind()
            'If chkSectors.Items.Count > 0 Then
            '    For Each chkitem As ListItem In chkSectors.Items
            '        chkitem.Selected = True
            '    Next
            'End If
            chkSectorsSelectAll.Checked = True
        Else
            chkSectors.Items.Clear()
            chkSectors.DataBind()
        End If
    End Sub
    ''' <summary>
    ''' BindHotelStars
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <remarks></remarks>
    Private Sub BindHotelStars(ByVal dataTable As DataTable)
        Dim dvStars As DataView = New DataView(dataTable)
        dvStars.Sort = "catname asc"
        dataTable = dvStars.ToTable(True)
        If dataTable.Rows.Count > 0 Then
            chkHotelStars.DataSource = dataTable
            chkHotelStars.DataTextField = "catname"
            chkHotelStars.DataValueField = "catcode"
            chkHotelStars.DataBind()
            'If chkHotelStars.Items.Count > 0 Then
            '    For Each chkitem As ListItem In chkHotelStars.Items
            '        chkitem.Selected = True
            '    Next
            'End If
            chkHotelStarsSelectAll.Checked = True
        Else
            chkHotelStars.Items.Clear()
            chkHotelStars.DataBind()
        End If
    End Sub
    ''' <summary>
    ''' BindRoomClassification
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <remarks></remarks>
    Private Sub BindRoomClassification(ByVal dataTable As DataTable)
        Dim dvClass As DataView = New DataView(dataTable)
        dvClass.Sort = "roomclassname asc"
        dataTable = dvClass.ToTable(True)

        If dataTable.Rows.Count > 0 Then
            chkRoomClassification.DataSource = dataTable
            chkRoomClassification.DataTextField = "roomclassname"
            chkRoomClassification.DataValueField = "roomclasscode"
            chkRoomClassification.DataBind()
            'If chkRoomClassification.Items.Count > 0 Then
            '    For Each chkitem As ListItem In chkRoomClassification.Items
            '        chkitem.Selected = True
            '    Next
            'End If
            chkRoomClassificationSelectAll.Checked = True
        Else
            chkRoomClassification.Items.Clear()
            chkRoomClassification.DataBind()
        End If
    End Sub
    ''' <summary>
    ''' BindHotelMainDetails
    ''' </summary>
    ''' <param name="dvResults"></param>
    ''' <remarks></remarks>
    Private Sub BindHotelMainDetails(ByVal dvResults As DataView, Optional ByVal strIsFiltered As String = "0")
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
        Dim strFilterCriteria As String = ""
        If ddlPriceRange.SelectedValue <> "0" Then
            If ddlPriceRange.SelectedValue = "500" Then
                If strFilterCriteria <> "" Then
                    strFilterCriteria = strFilterCriteria & " AND " & "minprice >=500"
                Else
                    strFilterCriteria = "minprice >=500"

                End If

            Else
                Dim strPriceRange() As String = ddlPriceRange.SelectedValue.Split("-")
                If strFilterCriteria <> "" Then
                    strFilterCriteria = strFilterCriteria & " AND " & "minprice >=" & strPriceRange(0) & " AND minprice <=" & strPriceRange(1)
                Else
                    strFilterCriteria = "minprice >=" & strPriceRange(0) & " AND minprice <=" & strPriceRange(1)
                End If


            End If

        End If
        If strIsFiltered = "0" Then
            dvResults.RowFilter = strFilterCriteria
        End If



        Dim dt As New DataTable
        dt = dvResults.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then
            dvhotnoshow.Style.Add("display", "none")
            lblHotelCount.Text = dt.Rows.Count & " Records Found"
            Dim iPageIndex As Integer = 1
            ' Dim iPageSize As Integer = 0
            Dim iRowNoFrom As Integer = 0
            Dim iRowNoTo As Integer = 0
            If Not Session("sMailBoxPageIndex") Is Nothing Then
                iPageIndex = Session("sMailBoxPageIndex")
            End If

            iRowNoFrom = (iPageIndex - 1) * PageSize + 1
            iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
            dv.Table.Columns.Add("rowIndex")
            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("rowIndex") = (i + 1).ToString
            Next


            dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & iRowNoTo




            dlHotelsSearchResults.DataSource = dv
            dlHotelsSearchResults.DataBind()

            Dim recordCount As Integer = dvResults.Count
            Me.PopulatePager(recordCount)
            lblHotelCount.Text = recordCount.ToString & " Records Found"
        Else
            dlHotelsSearchResults.DataBind()
        End If


    End Sub
    ''' <summary>
    ''' BindgvRoomType
    ''' </summary>
    ''' <param name="gvHotelRoomType"></param>
    ''' <param name="strRatePlanCode"></param>
    ''' <param name="strHotelCode"></param>
    ''' <param name="strMealPlanOrder"></param>
    ''' <param name="strPriceOrder"></param>
    ''' <remarks></remarks>
    Private Sub BindgvRoomType(ByVal gvHotelRoomType As GridView, ByVal strRatePlanCode As String, ByVal strHotelCode As String, ByVal strMealPlanOrder As String, ByVal strPriceOrder As String, ByVal strRatePlanName As String)
        'changed by mohamed on 10/09/2018 included rateplanname in filter
        Dim dtResults As New DataTable
        Dim dsHotelRoomTypes As New DataSet
        dsHotelRoomTypes = Session("sdsHotelRoomTypes")
        dtResults = dsHotelRoomTypes.Tables(0)

        strRatePlanName = strRatePlanName.Replace("'", "''") ' to handle single quotes -- added by abin on 20181107

        Dim strNotSelectedRoomClass As String = ""
        If chkRoomClassificationSelectAll.Checked <> True Then
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
        End If


        Dim dvResults As DataView = New DataView(dtResults)

        'strFilterCriteria = strFilterCriteria & " AND " & "roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
        If strNotSelectedRoomClass <> "" Then
            dvResults.RowFilter = "RatePlanId='" & strRatePlanCode & "' AND PartyCode='" & strHotelCode & "' And RatePlanName='" & strRatePlanName & "' AND " & " roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
        Else
            dvResults.RowFilter = "RatePlanId='" & strRatePlanCode & "' AND PartyCode='" & strHotelCode & "' And RatePlanName='" & strRatePlanName & "'"
        End If


        If strMealPlanOrder = "1" And strPriceOrder = "0" Then
            dvResults.Sort = "mealorder ASC "
        ElseIf strMealPlanOrder = "0" And strPriceOrder = "0" Then
            dvResults.Sort = "totalvalue ASC "
        Else
            If strMealPlanOrder = "0" And strPriceOrder = "1" Then
                dvResults.Sort = "totalvalue ASC "
            Else
                dvResults.Sort = "mealplans ASC "
            End If
        End If

        'rmtyporder,totalvalue
        If dvResults.ToTable.Rows.Count > 0 Then 'added totable mhd 28/03/2017
            strTotalValueHeading = dvResults.ToTable.Rows(0)("forrooms").ToString
            gvHotelRoomType.DataSource = dvResults.ToTable 'added totable mhd 28/03/2017
            gvHotelRoomType.DataBind()
        Else
            gvHotelRoomType.DataSource = Nothing 'mhd 28/03/2017
            gvHotelRoomType.DataBind()
        End If

    End Sub
    ''' <summary>
    ''' BindgvRoomTypeWithBlank
    ''' </summary>
    ''' <param name="gvHotelRoomType"></param>
    ''' <remarks></remarks>
    Private Sub BindgvRoomTypeWithBlank(ByVal gvHotelRoomType As GridView)
        Dim dtResults As New DataTable
        dtResults.Columns.Add("rmTypName", GetType(String))
        dtResults.Columns.Add("MealPlanNames", GetType(String))
        dtResults.Columns.Add("TotalValue", GetType(String))
        gvHotelRoomType.DataSource = dtResults
        gvHotelRoomType.DataBind()
    End Sub

    ''' <summary>
    ''' BindRatePlan
    ''' </summary>
    ''' <param name="dlRatePlan"></param>
    ''' <param name="strHotelCode"></param>
    ''' <remarks></remarks>
    Private Sub BindRatePlan(ByVal dlRatePlan As DataList, ByVal strHotelCode As String, ByVal lblRoomTypeWarning As Label, ByVal hdRatePlanSource As HiddenField, ByVal strInt_HotelCode As String)
        Dim dtRatePlan As New DataTable
        ' dtRatePlan = Session("sdtRoomType")

        Dim dsHotelRoomTypes As New DataSet
        Dim objBLLHotelSearchRM As New BLLHotelSearch
        objBLLHotelSearchRM = Session("sobjBLLHotelSearch")

        Dim strIsColumbusHotel As String = ""
        Dim strIsOneDMCHotel As String = ""

        If Not Session("sDSSearchResults") Is Nothing Then
            Dim dsSearchResultsAll = Session("sDSSearchResults")
            Dim dvHotelRateSourceOneDMC As DataView = New DataView(dsSearchResultsAll.Tables(0))
            dvHotelRateSourceOneDMC.RowFilter = "PartyCode='" & strHotelCode & "' and rateplansource='OneDMC'"

            Dim dvHotelRateSourceColumbus As DataView = New DataView(dsSearchResultsAll.Tables(0))
            dvHotelRateSourceColumbus.RowFilter = "PartyCode='" & strHotelCode & "' and rateplansource='Columbus'"

            If dvHotelRateSourceOneDMC.Count > 0 Then
                strIsOneDMCHotel = "1"
            End If
            If dvHotelRateSourceColumbus.Count > 0 Then
                strIsColumbusHotel = "1"
            End If

        End If


        If strIsColumbusHotel = "1" And strIsOneDMCHotel = "1" Then
        ElseIf strIsColumbusHotel = "1" And strIsOneDMCHotel <> "1" Then
            If Session("sdsHotelRoomTypes") Is Nothing Then
                dsHotelRoomTypes = objBLLHotelSearch.GetSearchDetailsSingleHotel(objBLLHotelSearchRM.LoginType, objBLLHotelSearchRM.WebuserName, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.RoomString, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.OrderBy, objBLLHotelSearchRM.Availabilty, strHotelCode, objBLLHotelSearchRM.OverridePrice, objBLLHotelSearchRM.EditRequestId, objBLLHotelSearchRM.EditRLineNo, objBLLHotelSearchRM.EditRatePlanId, objBLLHotelSearchRM.MealPlan)  '' Added Shahul 27/06/18
                Session("sdsHotelRoomTypes") = dsHotelRoomTypes
            Else
                dsHotelRoomTypes = Session("sdsHotelRoomTypes")
                Dim dvHotelRoomType As DataView = New DataView(dsHotelRoomTypes.Tables(0))
                dvHotelRoomType.RowFilter = "PartyCode='" & strHotelCode & "'"
                If dvHotelRoomType.Count = 0 Then
                    Dim dsHotelRoomTypesNEW As New DataSet
                    dsHotelRoomTypesNEW = objBLLHotelSearch.GetSearchDetailsSingleHotel(objBLLHotelSearchRM.LoginType, objBLLHotelSearchRM.WebuserName, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.RoomString, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.OrderBy, objBLLHotelSearchRM.Availabilty, strHotelCode, objBLLHotelSearchRM.OverridePrice, objBLLHotelSearchRM.EditRequestId, objBLLHotelSearchRM.EditRLineNo, objBLLHotelSearchRM.EditRatePlanId, objBLLHotelSearchRM.MealPlan)  '' Added Shahul 27/06/18
                    dsHotelRoomTypes.Merge(dsHotelRoomTypesNEW, True, MissingSchemaAction.Add)
                    dsHotelRoomTypes.AcceptChanges()
                    Session("sdsHotelRoomTypes") = dsHotelRoomTypes
                End If
            End If
        ElseIf strIsColumbusHotel <> "1" And strIsOneDMCHotel = "1" Then
            If Session("sdsHotelRoomTypes") Is Nothing Then


                dsHotelRoomTypes = BindOneDMCHotelRoomTypeDataSet(strInt_HotelCode, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.Adult, objBLLHotelSearchRM.Children, objBLLHotelSearchRM.ChildAgeString, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.NoOfNights) 'objBLLHotelSearch.GetSearchDetailsSingleHotel(objBLLHotelSearchRM.LoginType, objBLLHotelSearchRM.WebuserName, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.RoomString, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.OrderBy, objBLLHotelSearchRM.Availabilty, strHotelCode, objBLLHotelSearchRM.OverridePrice, objBLLHotelSearchRM.EditRequestId, objBLLHotelSearchRM.EditRLineNo, objBLLHotelSearchRM.EditRatePlanId, objBLLHotelSearchRM.MealPlan)  
                Session("sdsHotelRoomTypes") = dsHotelRoomTypes
            Else
                dsHotelRoomTypes = Session("sdsHotelRoomTypes")
                Dim dvHotelRoomType As DataView = New DataView(dsHotelRoomTypes.Tables(0))
                dvHotelRoomType.RowFilter = "PartyCode='" & strHotelCode & "'"
                If dvHotelRoomType.Count = 0 Then
                    Dim dsHotelRoomTypesNEW As New DataSet
                    dsHotelRoomTypesNEW = BindOneDMCHotelRoomTypeDataSet(strInt_HotelCode, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.Adult, objBLLHotelSearchRM.Children, objBLLHotelSearchRM.ChildAgeString, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.NoOfNights)  ' objBLLHotelSearch.GetSearchDetailsSingleHotel(objBLLHotelSearchRM.LoginType, objBLLHotelSearchRM.WebuserName, objBLLHotelSearchRM.AgentCode, objBLLHotelSearchRM.CheckIn, objBLLHotelSearchRM.CheckOut, objBLLHotelSearchRM.Room, objBLLHotelSearchRM.RoomString, objBLLHotelSearchRM.SourceCountryCode, objBLLHotelSearchRM.OrderBy, objBLLHotelSearchRM.Availabilty, strHotelCode, objBLLHotelSearchRM.OverridePrice, objBLLHotelSearchRM.EditRequestId, objBLLHotelSearchRM.EditRLineNo, objBLLHotelSearchRM.EditRatePlanId, objBLLHotelSearchRM.MealPlan)  '' Added Shahul 27/06/18
                    dsHotelRoomTypes.Merge(dsHotelRoomTypesNEW, True, MissingSchemaAction.Add)
                    dsHotelRoomTypes.AcceptChanges()
                    Session("sdsHotelRoomTypes") = dsHotelRoomTypes
                End If
            End If
        End If



        dtRatePlan = dsHotelRoomTypes.Tables(0)

        Dim dvRatePlan As DataView = New DataView(dtRatePlan, "PartyCode='" & strHotelCode & "'", "RatePlanName", DataViewRowState.CurrentRows)

        If dvRatePlan.Count > 0 Then
            Dim strNotSelectedRoomClass As String = ""
            If chkRoomClassificationSelectAll.Checked <> True Then
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
            End If

            If strNotSelectedRoomClass <> "" Then
                dvRatePlan.RowFilter = " PartyCode='" & strHotelCode & "'AND roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
            Else
                dvRatePlan.RowFilter = "PartyCode='" & strHotelCode & "'"
            End If
            dvRatePlan.Sort = "rateplanorder asc"
            dtRatePlan = dvRatePlan.ToTable()
            If dtRatePlan.Rows.Count >= 1 Then
                dtRatePlan = dtRatePlan.DefaultView.ToTable(True, "PartyCode", "RatePlanId", "RatePlanName", "Show", "rateplanorder", "RatePlanSummary")
                lblRoomTypeWarning.Text = ""
                lblRoomTypeWarning.Attributes.Add("class", "")
                lblRoomTypeWarning.Attributes.Add("class", "roomtype-warning-hide")
                '  lblRoomTypeWarning.Visible = False
            Else
                lblRoomTypeWarning.Attributes.Add("class", "")
                lblRoomTypeWarning.Attributes.Add("class", "roomtype-warning")
                lblRoomTypeWarning.Visible = True
                lblRoomTypeWarning.Text = "No prices for selected hotel."
            End If
            'If dtRatePlan.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtRatePlan.Rows.Count - 1
            '        dtRatePlan.Rows(i)("RatePlanName") = dtRatePlan.Rows(i)("RatePlanName")
            '    Next
            'End If
            dlRatePlan.DataSource = dtRatePlan
            dlRatePlan.DataBind()
        Else
            lblRoomTypeWarning.Attributes.Add("class", "")
            lblRoomTypeWarning.Attributes.Add("class", "roomtype-warning")
            lblRoomTypeWarning.Visible = True

            If dsHotelRoomTypes.Tables(3).Rows.Count > 0 Then
                Dim dv As DataView = New DataView(dsHotelRoomTypes.Tables(3))
                dv.RowFilter = "HotelCode='" & strHotelCode & "'"
                If dv.Count > 0 Then
                    lblRoomTypeWarning.Text = dsHotelRoomTypes.Tables(3).Rows(0)("pricemessage").ToString
                Else
                    lblRoomTypeWarning.Text = "No prices for selected hotel."
                End If

            Else

                lblRoomTypeWarning.Text = "No prices for selected hotel."
            End If

        End If


    End Sub
    ''' <summary>
    ''' BindRatePlanWithBlank
    ''' </summary>
    ''' <param name="dlRatePlan"></param>
    ''' <remarks></remarks>
    Private Sub BindRatePlanWithBlank(ByVal dlRatePlan As DataList)
        Dim dtRatePlan As New DataTable
        dtRatePlan.Columns.Add("PartyCode", GetType(String))
        dtRatePlan.Columns.Add("RatePlanId", GetType(String))
        dtRatePlan.Columns.Add("RatePlanName", GetType(String))
        dtRatePlan.Columns.Add("Show", GetType(String))
        dtRatePlan.Columns.Add("rateplanorder", GetType(String))
        dtRatePlan.Columns.Add("RatePlanSummary", GetType(String))
        dlRatePlan.DataSource = dtRatePlan
        dlRatePlan.DataBind()
    End Sub
    ''' <summary>
    ''' GetDistinctRecords
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="Columns"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistinctRecords(ByVal dt As DataTable, ByVal Columns As String()) As DataTable
        Dim dtUniqRecords As DataTable = New DataTable()
        dtUniqRecords = dt.DefaultView.ToTable(True, Columns)
        Return dtUniqRecords
    End Function
    ''' <summary>
    ''' BindPricefilter
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <remarks></remarks>
    Private Sub BindPricefilter(ByVal dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then

            Dim strMax As String = dataTable.AsEnumerable().Max(Function(row) CDbl(row("minprice")))
            Dim strMin As String = dataTable.AsEnumerable().Min(Function(row) CDbl(row("minprice")))

            hdPriceMinRange.Value = strMin
            hdPriceMin.Value = strMin

            hdPriceMaxRange.Value = strMax
            hdPriceMax.Value = strMax

        Else
            hdPriceMinRange.Value = "0"
            hdPriceMaxRange.Value = "1"
        End If
    End Sub
    ''' <summary>
    ''' BindHotelMainDetailsWithFilter
    ''' </summary>
    ''' <param name="dsSearchResults"></param>
    ''' <remarks></remarks>
    Private Sub BindHotelMainDetailsWithFilter(ByVal dsSearchResults As DataSet)
        If dsSearchResults.Tables.Count > 0 Then
            If dsSearchResults.Tables(0).Rows.Count > 0 Then

                'changed by mohamed on 12/02/2018
                Dim dtMainDetails1 As DataTable = dsSearchResults.Tables(0).Copy
                Dim dtMainDetailsRet As DataTable, dtMainDetailsMiddle As DataTable

                dtMainDetails1.Columns.Add("CustomSortHelp", Type.GetType("System.Int64"), "2")
                Dim dvMaiDetails As DataView = New DataView(dtMainDetails1) 'Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))


                ' Filter HotelStars *****************
                Dim strNotSelectedHotelStar As String = ""
                If chkHotelStarsSelectAll.Checked <> True Then
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
                End If



                ' Filter Sectors *****************
                Dim strNotSelectedSectors As String = ""
                If chkSectorsSelectAll.Checked <> True Then
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
                End If



                ' Filter Sectors *****************
                Dim strNotSelectedPropertyType As String = ""
                If chkPropertyTypeSelectAll.Checked <> True Then
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
                End If


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

                'If strNotSelectedRoomClass <> "" Then
                '    If strFilterCriteria <> "" Then
                '        strFilterCriteria = strFilterCriteria & " AND " & "roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
                '    Else
                '        strFilterCriteria = "roomclasscode NOT IN (" & strNotSelectedRoomClass & ")"
                '    End If
                'End If



                If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "minprice >=" & hdPriceMin.Value & " AND minprice <=" & hdPriceMax.Value
                    Else
                        strFilterCriteria = "minprice >=" & hdPriceMin.Value & " AND minprice <=" & hdPriceMax.Value
                    End If
                End If



                Dim strFilterCriteriaSearchTour As String = ""
                Dim lsHotelSearchOrder As String = ""
                If txtSearchHotel.Text <> "" Then
                    lsHotelSearchOrder = "CustomSortHelp, "
                    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " partyname like ('" & txtSearchHotel.Text & "%')"
                End If
                'changed by mohamed on 12/02/2018
                If strFilterCriteria & strFilterCriteriaSearchTour <> "" Then
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                End If


                ''    Dim strFilterCriteria As String = ""
                'If ddlPriceRange.SelectedValue <> "0" Then
                '    If ddlPriceRange.SelectedValue = "500" Then
                '        If strFilterCriteria <> "" Then
                '            strFilterCriteria = strFilterCriteria & " AND " & "minprice >=500"
                '        Else
                '            strFilterCriteria = "minprice >=500"

                '        End If

                '    Else
                '        Dim strPriceRange() As String = ddlPriceRange.SelectedValue.Split("-")
                '        If strFilterCriteria <> "" Then
                '            strFilterCriteria = strFilterCriteria & " AND " & "minprice >=" & strPriceRange(0) & " AND minprice <=" & strPriceRange(1)
                '        Else
                '            strFilterCriteria = "minprice >=" & strPriceRange(0) & " AND minprice <=" & strPriceRange(1)
                '        End If


                '    End If

                'End If





                'changed by mohamed on 12/02/2018
                'Search Text in Middle
                If txtSearchHotel.Text <> "" Then
                    dtMainDetailsRet = dvMaiDetails.ToTable.Copy
                    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " partyname like ('%" & txtSearchHotel.Text & "%') and partyname not like ('" & txtSearchHotel.Text & "%')"
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                    dtMainDetailsMiddle = dvMaiDetails.ToTable.Copy
                    dtMainDetailsMiddle.Columns("CustomSortHelp").Expression = "3"
                    dtMainDetailsRet.Merge(dtMainDetailsMiddle)
                    'dvMaiDetails = Nothing
                    dvMaiDetails = New DataView(dtMainDetailsRet)
                End If

                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = lsHotelSearchOrder & "partyname ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = lsHotelSearchOrder & "minprice ASC"
                ElseIf ddlSorting.Text = "0" Then
                    dvMaiDetails.Sort = lsHotelSearchOrder & "Preferred  DESC,minprice ASC"
                ElseIf ddlSorting.Text = "Rating" Then
                    dvMaiDetails.Sort = lsHotelSearchOrder & "noofstars DESC,partyname ASC "
                ElseIf ddlSorting.Text = "Preferred" Then
                    dvMaiDetails.Sort = lsHotelSearchOrder & "Preferred  DESC,partyname ASC "
                End If



                BindHotelMainDetails(dvMaiDetails, "1")

                If hdPriceSliderActive.Value = "1" Then


                    If chkHotelStars.Items.Count > 0 Then
                        Dim dsSearchResultsNew As New DataSet
                        dsSearchResultsNew = Session("sDSSearchResults")


                        If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                            If strFilterCriteria <> "" Then
                                strFilterCriteria = strFilterCriteria & " AND " & "minprice >=" & hdPriceMin.Value & " AND minprice <=" & hdPriceMax.Value
                            Else
                                strFilterCriteria = "minprice >=" & hdPriceMin.Value & " AND minprice <=" & hdPriceMax.Value
                            End If
                        End If

                        Dim dv As DataView = New DataView(dsSearchResultsNew.Tables(0))

                        Dim strFilterCriteriaNew As String = ""
                        If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                            strFilterCriteriaNew = "minprice >=" & hdPriceMin.Value & " AND minprice <=" & hdPriceMax.Value
                        End If


                        If strFilterCriteriaNew <> "" Then
                            dv.RowFilter = strFilterCriteriaNew
                        End If


                        For Each chkitem As ListItem In chkHotelStars.Items
                            If chkitem.Selected = True Then
                                Dim dr As DataRow = dvMaiDetails.ToTable(True, "catcode").Select("catcode='" & chkitem.Value & "'").FirstOrDefault
                                If dr Is Nothing Then
                                    chkitem.Selected = False
                                Else
                                    chkitem.Selected = True
                                End If
                            Else

                                Dim dr As DataRow = dv.ToTable(True, "catcode").Select("catcode='" & chkitem.Value & "'").FirstOrDefault
                                If dr Is Nothing Then
                                    chkitem.Selected = False
                                Else
                                    chkitem.Selected = True
                                End If
                            End If
                        Next
                    End If
                End If

                '    Session("sdtRoomType") = dsSearchResults.Tables(1)

                ' lblHotelCount.Text = dvMaiDetails.Count & " Records Found"

            End If
        Else
            dlHotelsSearchResults.DataBind()
        End If
    End Sub
    ''' <summary>
    ''' btnFilter_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        Try
            ' System.Threading.Thread.Sleep(200)
            If hdFilterType.Value = "HotelStar" Then
                If chkHotelStarsSelectAll.Checked = True Then
                    If chkHotelStars.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkHotelStars.Items
                            chkitem.Selected = True
                        Next
                    End If
                Else
                    If chkHotelStars.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkHotelStars.Items
                            chkitem.Selected = False
                        Next
                    End If
                End If
            End If

            Dim strHotelStrarFlag As String = ""
            For Each chkitem As ListItem In chkHotelStars.Items
                If chkitem.Selected = True Then
                    strHotelStrarFlag = "1"
                    Exit For
                End If
            Next
            If strHotelStrarFlag = "1" Then
                chkHotelStarsSelectAll.Checked = False
            End If

            strHotelStrarFlag = "2"
            For Each chkitem As ListItem In chkHotelStars.Items
                If chkitem.Selected = False Then
                    strHotelStrarFlag = "1"
                    Exit For
                End If
            Next
            If strHotelStrarFlag = "2" Then
                chkHotelStarsSelectAll.Checked = True
            End If


            If hdFilterType.Value = "HotelSector" Then
                If chkSectorsSelectAll.Checked = True Then
                    If chkSectors.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkSectors.Items
                            chkitem.Selected = True
                        Next
                    End If
                Else
                    If chkSectors.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkSectors.Items
                            chkitem.Selected = False
                        Next
                    End If
                End If
            End If

            Dim strHotelSectorFlag As String = ""
            For Each chkitem As ListItem In chkSectors.Items
                If chkitem.Selected = True Then
                    strHotelSectorFlag = "1"
                    Exit For
                End If
            Next
            If strHotelSectorFlag = "1" Then
                chkSectorsSelectAll.Checked = False
            End If

            strHotelSectorFlag = "2"
            For Each chkitem As ListItem In chkSectors.Items
                If chkitem.Selected = False Then
                    strHotelSectorFlag = "1"
                    Exit For
                End If
            Next
            If strHotelSectorFlag = "2" Then
                chkSectorsSelectAll.Checked = True
            End If


            If hdFilterType.Value = "PropertyType" Then
                If chkPropertyTypeSelectAll.Checked = True Then
                    If chkPropertyType.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkPropertyType.Items
                            chkitem.Selected = True
                        Next
                    End If
                Else
                    If chkPropertyType.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkPropertyType.Items
                            chkitem.Selected = False
                        Next
                    End If
                End If
            End If

            Dim strPropertyTypeFlag As String = ""
            For Each chkitem As ListItem In chkPropertyType.Items
                If chkitem.Selected = True Then
                    strPropertyTypeFlag = "1"
                    Exit For
                End If
            Next
            If strPropertyTypeFlag = "1" Then
                chkPropertyTypeSelectAll.Checked = False
            End If

            strPropertyTypeFlag = "2"
            For Each chkitem As ListItem In chkPropertyType.Items
                If chkitem.Selected = False Then
                    strPropertyTypeFlag = "1"
                    Exit For
                End If
            Next
            If strPropertyTypeFlag = "2" Then
                chkPropertyTypeSelectAll.Checked = True
            End If

            If hdFilterType.Value = "RoomClass" Then
                If chkRoomClassificationSelectAll.Checked = True Then
                    If chkRoomClassification.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkRoomClassification.Items
                            chkitem.Selected = True
                        Next
                    End If
                Else
                    If chkRoomClassification.Items.Count > 0 Then
                        For Each chkitem As ListItem In chkRoomClassification.Items
                            chkitem.Selected = False
                        Next
                    End If
                End If
            End If

            Dim strRoomClassFlag As String = ""
            For Each chkitem As ListItem In chkRoomClassification.Items
                If chkitem.Selected = True Then
                    strRoomClassFlag = "1"
                    Exit For
                End If
            Next
            If strRoomClassFlag = "1" Then
                chkRoomClassificationSelectAll.Checked = False
            End If

            strRoomClassFlag = "2"
            For Each chkitem As ListItem In chkRoomClassification.Items
                If chkitem.Selected = False Then
                    strRoomClassFlag = "1"
                    Exit For
                End If
            Next
            If strRoomClassFlag = "2" Then
                chkRoomClassificationSelectAll.Checked = True
            End If

            '
            If Not Session("sDSSearchResults") Is Nothing Then
                Dim dsSearchResults As New DataSet
                dsSearchResults = Session("sDSSearchResults")
                Session("sMailBoxPageIndex") = "1"
                If chkOveridePrice.Checked = True Then
                    hdOveride.Value = "1"
                Else
                    hdOveride.Value = "0"
                End If

                BindHotelMainDetailsWithFilter(dsSearchResults)
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnFilterForRoom_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFilterForRoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterForRoom.Click
        Try
            dlHotelsSearchResults.DataSource = Nothing
            dlHotelsSearchResults.DataBind()


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
                Response.Redirect("Home.aspx?Tab=0")
            End If
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)

            Dim dsSearchResults As New DataSet
            objBLLHotelSearch.FilterRoomClass = strSelectedRoomClass
            If chkOveridePrice.Checked = True Then
                hdOveride.Value = "1"
            Else
                hdOveride.Value = "0"
            End If


            dsSearchResults = objBLLHotelSearch.GetSearchDetails()
            Session("sDSSearchResults") = dsSearchResults
            If dsSearchResults.Tables.Count > 0 Then

                Session("sDSSearchResults") = dsSearchResults

                Session("sMailBoxPageIndex") = "1"
                BindHotelMainDetailsWithFilter(dsSearchResults)



            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If





        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
        Dim strScript As String = "javascript: CallPriceSlider();"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

    End Sub
    ''' <summary>
    ''' lbReadMore_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' ddlSorting_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlSorting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSorting.SelectedIndexChanged
        Try
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSSearchResults")
            BindHotelMainDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: ddlSorting_SelectedIndexChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub lbAdditionalCharges_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lbAdditionalCharges As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbAdditionalCharges.NamingContainer, GridViewRow)
            Dim lblRoomType As Label = CType(gvGridviewRow.FindControl("lblRoomType"), Label)
            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
            Dim lblRMRatePlanId As Label = CType(gvGridviewRow.FindControl("lblRMRatePlanId"), Label)
            Dim lblRMRoomTypeCode As Label = CType(gvGridviewRow.FindControl("lblRMRoomTypeCode"), Label)
            Dim lblRMMealPlanCode As Label = CType(gvGridviewRow.FindControl("lblRMMealPlanCode"), Label)
            Dim lblAdultEb As Label = CType(gvGridviewRow.FindControl("lblAdultEb"), Label)
            Dim lblChildEb As Label = CType(gvGridviewRow.FindControl("lblChildEb"), Label)
            Dim strNoOfExtraBed As String = Val(lblAdultEb.Text) + Val(lblChildEb.Text)

            Dim hdExtraBedRequired As Label = CType(gvGridviewRow.FindControl("hdExtraBedRequired"), Label)
            Dim hdExtraBedValue As Label = CType(gvGridviewRow.FindControl("hdExtraBedValue"), Label)

            ' Session("sExreaBedSender") = sender
            Session("slbAdditionalCharges") = lbAdditionalCharges




            Dim objBLLHotelSearch As New BLLHotelSearch
            Dim dt As New DataTable
            dt = objBLLHotelSearch.GetNew_booking_OneTimePay(lblRMPartyCode.Text, lblRMRoomTypeCode.Text, lblRMMealPlanCode.Text, lblRMRatePlanId.Text, HttpContext.Current.Session("sAgentCode").ToString, txtCountryCode.Text, txtCheckIn.Text, txtCheckOut.Text, strNoOfExtraBed, "", "")
            If dt.Rows.Count > 0 Then
                txtNoOfExtraBed.Text = dt.Rows(0)("NoEB").ToString
                txtExtraBedUnitPrice.Text = dt.Rows(0)("UnitPrice").ToString
                txtExtraBedTotalPrice.Text = dt.Rows(0)("TotalPrice").ToString
                lblEBBookingCode.Text = dt.Rows(0)("BookingCode").ToString
                lblAdditionalCharges.Text = "Do you want add guaranteed extra bed?"
                btnAddChargeYes.Visible = True
                btnAddChargeNo.Visible = True

                If hdExtraBedRequired.Text = "YES" And hdExtraBedValue.Text <> "" Then
                    Dim str As String() = hdExtraBedValue.Text.Split("*")
                    txtNoOfExtraBed.Text = str(0)
                    txtExtraBedUnitPrice.Text = str(1)
                    lblEBBookingCode.Text = str(2)
                    txtExtraBedTotalPrice.Text = Val(str(0)) * Val(str(1))
                End If
                Dim mpAdditionalCharges As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpAdditionalCharges"), AjaxControlToolkit.ModalPopupExtender)
                mpAdditionalCharges.Show()
            Else

                MessageBox.ShowMessage(Page, MessageType.Info, "No Additional Charges")
            End If
            '

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbAdditionalCharges_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbMinLengthStay_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbMinLengthStay_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lbMinLengthStay As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbMinLengthStay.NamingContainer, GridViewRow)
            Dim lblRoomType As Label = CType(gvGridviewRow.FindControl("lblRoomType"), Label)
            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
            Dim lblRMRatePlanId As Label = CType(gvGridviewRow.FindControl("lblRMRatePlanId"), Label)
            Dim lblRMRoomTypeCode As Label = CType(gvGridviewRow.FindControl("lblRMRoomTypeCode"), Label)
            Dim lblRMMealPlanCode As Label = CType(gvGridviewRow.FindControl("lblRMMealPlanCode"), Label)

            Dim objBLLHotelSearch As New BLLHotelSearch
            Dim dt As New DataTable
            dt = objBLLHotelSearch.GetMinNightStayDetails(lblRMPartyCode.Text, lblRMRoomTypeCode.Text, lblRMMealPlanCode.Text, lblRMRatePlanId.Text, HttpContext.Current.Session("sAgentCode").ToString, txtCountryCode.Text, txtCheckIn.Text, txtCheckOut.Text)
            If dt.Rows.Count > 0 Then
                lblminlengthStay.Text = "For this room a minimum stay of " & dt.Rows(0)("minnights").ToString & " night(s) " & dt.Rows(0)("nightsoption").ToString & " " & dt.Rows(0)("fromdate").ToString & " to " & dt.Rows(0)("todate").ToString
                Dim mpMinLengthStay As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpMinLengthStay"), AjaxControlToolkit.ModalPopupExtender)
                mpMinLengthStay.Show()
            Else
                lblminlengthStay.Text = ""
                MessageBox.ShowMessage(Page, MessageType.Info, "No details to show.")
            End If
            '

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbMinLengthStay_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbSpecialEvents_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbSpecialEvents_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lbSpecialEvents As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbSpecialEvents.NamingContainer, GridViewRow)
            Dim mpSpecialEvents As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpSpecialEvents"), AjaxControlToolkit.ModalPopupExtender)
            Dim dsSpecialEvents As New DataSet
            dsSpecialEvents = GetSpecialEventDetails(lbSpecialEvents, gvGridviewRow, "")

            If Not dsSpecialEvents Is Nothing Then
                If dsSpecialEvents.Tables.Count > 0 Then
                    If dsSpecialEvents.Tables(0).Rows.Count > 0 Then
                        Session("sdsSpecialEvents") = dsSpecialEvents
                        dlSpecialEvents.DataSource = dsSpecialEvents.Tables(0)
                        dlSpecialEvents.DataBind()
                        mpSpecialEvents.Show()
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Info, "No event")
                        Exit Sub
                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Info, "No event")
                    Exit Sub
                End If
            Else
                MessageBox.ShowMessage(Page, MessageType.Info, "No event")
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbSpecialEvents_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    ''' <summary>
    ''' lbHotelContruction_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbHotelContruction_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbHotelContruction As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbHotelContruction.NamingContainer, GridViewRow)
            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
            Dim hdRoomRatePlanSource As Label = CType(gvGridviewRow.FindControl("hdRoomRatePlanSource"), Label)
            If Not Session("sobjBLLHotelSearch") Is Nothing Then
                objBLLHotelSearch = New BLLHotelSearch
                objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
                Dim dt As New DataTable
                If hdRoomRatePlanSource.Text = "OneDMC" Then
                    dt = objBLLHotelSearch.GetInt_HotelContruction(lblRMPartyCode.Text, objBLLHotelSearch.CheckIn, objBLLHotelSearch.CheckOut)
                Else
                    dt = objBLLHotelSearch.GetHotelContruction(lblRMPartyCode.Text, objBLLHotelSearch.CheckIn, objBLLHotelSearch.CheckOut)
                End If

                If dt.Rows.Count > 0 Then
                    'lblHotelContructionHeading.Text = dt.Rows(0)("heading").ToString()
                    'lblHotelContructionDate.Text = "Date : " & dt.Rows(0)("fromdate").ToString() & " to " & dt.Rows(0)("todate").ToString()
                    lblHotelContructionContent.Text = ""
                    For Each row As DataRow In dt.Rows
                        lblHotelContructionContent.Text += "<b><font color='red'>" & row("heading").ToString() & "</font></b>"
                        lblHotelContructionContent.Text += "<br />Date : " & row("fromdate").ToString() & " to " & row("todate").ToString()
                        lblHotelContructionContent.Text += "<br />" & row("policytext").ToString().Replace(Chr(10), "<br />") & "<hr>"
                    Next

                    Dim mpHotelContruction As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpHotelContruction"), AjaxControlToolkit.ModalPopupExtender)
                    mpHotelContruction.Show()
                Else
                    'lblHotelContructionDate.Text = ""
                    lblHotelContructionContent.Text = ""
                    MessageBox.ShowMessage(Page, MessageType.Info, "No Construction activity.")
                End If
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbHotelContruction_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbTariff_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbTariff_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbTariff As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbTariff.NamingContainer, GridViewRow)
            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
            Dim hdRoomRatePlanSource As Label = CType(gvGridviewRow.FindControl("hdRoomRatePlanSource"), Label)
           
            If Not Session("sobjBLLHotelSearch") Is Nothing Then
                objBLLHotelSearch = New BLLHotelSearch
                objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
                Dim dt As New DataTable
                If hdRoomRatePlanSource.Text = "OneDMC" Then


                    Dim hdInt_RoomtypeCodes As Label = CType(gvGridviewRow.FindControl("hdInt_RoomtypeCodes"), Label)
                    Dim hdInt_RoomtypeNames As Label = CType(gvGridviewRow.FindControl("hdInt_RoomtypeNames"), Label)
                    Dim hdInt_Roomtypes As Label = CType(gvGridviewRow.FindControl("hdInt_Roomtypes"), Label)

                    Dim hdInt_costprice As Label = CType(gvGridviewRow.FindControl("hdInt_costprice"), Label)
                    Dim hdInt_costcurrcode As Label = CType(gvGridviewRow.FindControl("hdInt_costcurrcode"), Label)

                    Dim hdInt_partycode As Label = CType(gvGridviewRow.FindControl("hdInt_partycode"), Label)
                    Dim hdInt_rmtypecode As Label = CType(gvGridviewRow.FindControl("hdInt_rmtypecode"), Label)
                    Dim hdInt_mealcode As Label = CType(gvGridviewRow.FindControl("hdInt_mealcode"), Label)

                    Dim hdOffercode As Label = CType(gvGridviewRow.FindControl("hdOffercode"), Label)
                    Dim hdAccomodationcode As Label = CType(gvGridviewRow.FindControl("hdAccomodationcode"), Label)

                    Dim objAPIHotelDetailsRequest As APIHotelDetailsRequest.HotelDetailsRequest = New APIHotelDetailsRequest.HotelDetailsRequest()
                    Dim CheckIn As String = objBLLHotelSearch.CheckIn
                    Dim CheckOut As String = objBLLHotelSearch.CheckOut
                    If CheckIn <> "" Then
                        Dim strDates As String() = CheckIn.Split("/")
                        CheckIn = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
                    End If
                    If CheckOut <> "" Then
                        Dim strDates As String() = CheckOut.Split("/")
                        CheckOut = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
                    End If

                    objAPIHotelDetailsRequest.arrivalDate = CheckIn
                    objAPIHotelDetailsRequest.departureDate = CheckOut
                    Dim strRoomString As String = objBLLHotelSearch.RoomString
                    Dim strRoomsAll() = strRoomString.Split(";")
                    Dim ilenth As Integer = strRoomsAll.Length - 1
                    Dim distribution As APIHotelDetailsRequest.Distribution = New APIHotelDetailsRequest.Distribution()
                    Dim dist As New List(Of APIHotelDetailsRequest.Distribution)
                    ' Dim dist(ilenth) As APIHotelDetailsRequest.Distribution


                    For i As Integer = 0 To strRoomsAll.Length - 1
                        Dim strRooms() = strRoomsAll(i).Split(",")
                        distribution = New APIHotelDetailsRequest.Distribution()
                        distribution.numberAdults = strRooms(1)
                        distribution.numberChildren = strRooms(2)
                        Dim strChildAges As String() = strRooms(3).Split("|")
                        Dim childrenAges As New List(Of Integer)
                        If strRooms(2) > 0 Then

                            For j As Integer = 0 To strChildAges.Length - 1
                                childrenAges.Add(strChildAges(j))
                            Next

                        End If

                        distribution.childrenAges = childrenAges
                        distribution.numberRooms = 1 'strRooms(0)

                        Dim strInt_RoomtypeCodes As String() = hdInt_RoomtypeCodes.Text.Split(";")
                        Dim strRoomCode As String() = strInt_RoomtypeCodes(i).Split(":")

                        Dim strInt_Roomtypes As String() = hdInt_Roomtypes.Text.Split(";")
                        Dim strRoomIds As String() = strInt_Roomtypes(i).Split(":")

                        Dim strOfferCodes As String() = hdOffercode.Text.Split(";")

                        Dim strOfferCode As String() = strOfferCodes(i).Split(":")


                        distribution.board.id = hdInt_mealcode.Text 'dsMapping.Tables(0).Rows(0)("mealcode_new").ToString
                        distribution.room.id = strRoomIds(1)
                        distribution.searchcode = strOfferCode(1)
                        distribution.roomcode = strRoomCode(1)
                        dist.Add(distribution)
                    Next

                    objAPIHotelDetailsRequest.distribution = dist


                    objAPIHotelDetailsRequest.hotel = hdInt_partycode.Text 'dsMapping.Tables(0).Rows(0)("partycode_new").ToString
                    objAPIHotelDetailsRequest.searchcode = hdAccomodationcode.Text
                    objAPIHotelDetailsRequest.login.lang = "en"
                    objAPIHotelDetailsRequest.login.password = "pDfekNA92pd29b2w"
                    objAPIHotelDetailsRequest.login.user = "discover.saudixml"

                    ' Dim sb As String = New JavaScriptSerializer().Serialize(objAPIHotelDetailsRequest)
                    Dim objApiController As New ApiController
                    Dim strCancelPolicy As String = ""
                    Dim resDetail As APIHotelDetailsResponse.HotelDetailsResponse
                    resDetail = objApiController.GetCancelationPolicyFromDetailApiResponse(objAPIHotelDetailsRequest)

                    lblConditions.Text = resDetail.result.conditions


                    dt = objBLLHotelSearch.GetInt_TariffNote(lblRMPartyCode.Text, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode, objBLLHotelSearch.CheckIn, objBLLHotelSearch.CheckOut)
                Else
                    dt = objBLLHotelSearch.GetTariff(lblRMPartyCode.Text, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode, objBLLHotelSearch.CheckIn, objBLLHotelSearch.CheckOut)
                End If

                If dt.Rows.Count > 0 Then
                    lblTariffDate.Text = "Date : " & dt.Rows(0)("fromdate").ToString() & " to " & dt.Rows(0)("todate").ToString()
                    lblTariffContent.Text = dt.Rows(0)("policytext").ToString().Replace(Chr(10), "<br />")
                    Dim mpTariff As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpTariff"), AjaxControlToolkit.ModalPopupExtender)
                    mpTariff.Show()
                Else
                    lblTariffDate.Text = ""
                    lblTariffContent.Text = ""
                End If
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbTariff_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbOffer_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lbOffer As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbOffer.NamingContainer, GridViewRow)
            Dim lblRMRatePlanId As Label = CType(gvGridviewRow.FindControl("lblRMRatePlanId"), Label)
            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)

            objBLLHotelSearch = New BLLHotelSearch
            Dim dt As New DataTable
            dt = objBLLHotelSearch.GetSpecialOffers(lblRMPartyCode.Text, lblRMRatePlanId.Text)
            If dt.Rows.Count > 0 Then
                Dim strProm As String = ""
                If dt.Rows.Count = 1 Then
                    If dt.Rows(0)("remarks").ToString() <> "No Remarks exist" Then
                        lblOfferDate.Text = dt.Rows(0)("promotionname").ToString().Replace(Chr(10), "<br />")
                        lblOfferContent.Text = dt.Rows(0)("remarks").ToString().Replace(Chr(10), "<br />")
                    Else
                        lblOfferDate.Text = ""
                        lblOfferContent.Text = strProm
                    End If



                Else

                    For i As Integer = 0 To dt.Rows.Count - 1

                        If dt.Rows(i)("remarks").ToString() <> "No Remarks exist" Then
                            strProm = strProm & dt.Rows(i)("promotionname").ToString().Replace(Chr(10), "<br /> - ")
                            strProm = strProm & dt.Rows(i)("remarks").ToString().Replace(Chr(10), "<br />") & "<br />"
                        End If
                    Next
                    lblOfferDate.Text = ""
                    lblOfferContent.Text = strProm
                End If





            Else
                lblOfferDate.Text = ""
                lblOfferContent.Text = ""
            End If


            Dim dtTransfer As New DataTable
            dtTransfer = objBLLHotelSearch.GetAirportTransferCompliment(lblRMPartyCode.Text, lblRMRatePlanId.Text)
            If dtTransfer.Rows.Count > 0 Then
                gvComplimentaryAirportTransfer.DataSource = dtTransfer
                gvComplimentaryAirportTransfer.DataBind()
            Else
                dvTransferCompliment.Visible = False
            End If

            If dt.Rows.Count > 0 Or dtTransfer.Rows.Count > 0 Then
                Dim mpOffers As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpOffers"), AjaxControlToolkit.ModalPopupExtender)
                mpOffers.Show()
            End If



        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbOffer_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbCancelationPolicy_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbCancelationPolicy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lbCancelationPolicy As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbCancelationPolicy.NamingContainer, GridViewRow)
            Dim lblRoomType As Label = CType(gvGridviewRow.FindControl("lblRoomType"), Label)

            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
            Dim lblRMRatePlanId As Label = CType(gvGridviewRow.FindControl("lblRMRatePlanId"), Label)
            Dim lblRMRoomTypeCode As Label = CType(gvGridviewRow.FindControl("lblRMRoomTypeCode"), Label)
            Dim lblRMMealPlanCode As Label = CType(gvGridviewRow.FindControl("lblRMMealPlanCode"), Label)

            Dim objBLLHotelSearch As New BLLHotelSearch
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
            If Not objBLLHotelSearch Is Nothing Then


                Dim hdRoomRatePlanSource As Label = CType(gvGridviewRow.FindControl("hdRoomRatePlanSource"), Label)

                If hdRoomRatePlanSource.Text = "OneDMC" Then
   
                    Dim hdInt_RoomtypeCodes As Label = CType(gvGridviewRow.FindControl("hdInt_RoomtypeCodes"), Label)
                    Dim hdInt_RoomtypeNames As Label = CType(gvGridviewRow.FindControl("hdInt_RoomtypeNames"), Label)
                    Dim hdInt_Roomtypes As Label = CType(gvGridviewRow.FindControl("hdInt_Roomtypes"), Label)

                    Dim hdInt_costprice As Label = CType(gvGridviewRow.FindControl("hdInt_costprice"), Label)
                    Dim hdInt_costcurrcode As Label = CType(gvGridviewRow.FindControl("hdInt_costcurrcode"), Label)

                    Dim hdInt_partycode As Label = CType(gvGridviewRow.FindControl("hdInt_partycode"), Label)
                    Dim hdInt_rmtypecode As Label = CType(gvGridviewRow.FindControl("hdInt_rmtypecode"), Label)
                    Dim hdInt_mealcode As Label = CType(gvGridviewRow.FindControl("hdInt_mealcode"), Label)

                    Dim hdOffercode As Label = CType(gvGridviewRow.FindControl("hdOffercode"), Label)
                    Dim hdAccomodationcode As Label = CType(gvGridviewRow.FindControl("hdAccomodationcode"), Label)

                    Dim objAPIHotelDetailsRequest As APIHotelDetailsRequest.HotelDetailsRequest = New APIHotelDetailsRequest.HotelDetailsRequest()
                    Dim CheckIn As String = objBLLHotelSearch.CheckIn
                    Dim CheckOut As String = objBLLHotelSearch.CheckOut
                    If CheckIn <> "" Then
                        Dim strDates As String() = CheckIn.Split("/")
                        CheckIn = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
                    End If
                    If CheckOut <> "" Then
                        Dim strDates As String() = CheckOut.Split("/")
                        CheckOut = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
                    End If

                    objAPIHotelDetailsRequest.arrivalDate = CheckIn
                    objAPIHotelDetailsRequest.departureDate = CheckOut
                    Dim strRoomString As String = objBLLHotelSearch.RoomString
                    Dim strRoomsAll() = strRoomString.Split(";")
                    Dim ilenth As Integer = strRoomsAll.Length - 1
                    Dim distribution As APIHotelDetailsRequest.Distribution = New APIHotelDetailsRequest.Distribution()
                    Dim dist As New List(Of APIHotelDetailsRequest.Distribution)
                    ' Dim dist(ilenth) As APIHotelDetailsRequest.Distribution


                    For i As Integer = 0 To strRoomsAll.Length - 1
                        Dim strRooms() = strRoomsAll(i).Split(",")
                        distribution = New APIHotelDetailsRequest.Distribution()
                        distribution.numberAdults = strRooms(1)
                        distribution.numberChildren = strRooms(2)
                        Dim strChildAges As String() = strRooms(3).Split("|")
                        Dim childrenAges As New List(Of Integer)
                        If strRooms(2) > 0 Then

                            For j As Integer = 0 To strChildAges.Length - 1
                                childrenAges.Add(strChildAges(j))
                            Next

                        End If

                        distribution.childrenAges = childrenAges
                        distribution.numberRooms = 1 'strRooms(0)

                        Dim strInt_RoomtypeCodes As String() = hdInt_RoomtypeCodes.Text.Split(";")
                        Dim strRoomCode As String() = strInt_RoomtypeCodes(i).Split(":")

                        Dim strInt_Roomtypes As String() = hdInt_Roomtypes.Text.Split(";")
                        Dim strRoomIds As String() = strInt_Roomtypes(i).Split(":")

                        Dim strOfferCodes As String() = hdOffercode.Text.Split(";")

                        Dim strOfferCode As String() = strOfferCodes(i).Split(":")


                        distribution.board.id = hdInt_mealcode.Text 'dsMapping.Tables(0).Rows(0)("mealcode_new").ToString
                        distribution.room.id = strRoomIds(1)
                        distribution.searchcode = strOfferCode(1)
                        distribution.roomcode = strRoomCode(1)
                        dist.Add(distribution)
                    Next

                    objAPIHotelDetailsRequest.distribution = dist


                    objAPIHotelDetailsRequest.hotel = hdInt_partycode.Text 'dsMapping.Tables(0).Rows(0)("partycode_new").ToString
                    objAPIHotelDetailsRequest.searchcode = hdAccomodationcode.Text
                    objAPIHotelDetailsRequest.login.lang = "en"
                    objAPIHotelDetailsRequest.login.password = "pDfekNA92pd29b2w"
                    objAPIHotelDetailsRequest.login.user = "discover.saudixml"

                    ' Dim sb As String = New JavaScriptSerializer().Serialize(objAPIHotelDetailsRequest)
                    Dim objApiController As New ApiController
                    Dim strCancelPolicy As String = ""
                    Dim resDetail As APIHotelDetailsResponse.HotelDetailsResponse
                    resDetail = objApiController.GetCancelationPolicyFromDetailApiResponse(objAPIHotelDetailsRequest)

                  

                   

                    For Each obj In resDetail.result.cancelConditions
                        Dim dt As New DataTable
                        dt.Columns.Add(New DataColumn("hotelcode", GetType(String)))
                        dt.Columns.Add(New DataColumn("roomtypecode", GetType(String)))
                        dt.Columns.Add(New DataColumn("cost", GetType(Decimal)))
                        dt.Columns.Add(New DataColumn("noofroom", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("noofadult", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("noofchild", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("Int_RoomtypeCodes", GetType(String)))
                        dt.Columns.Add(New DataColumn("mealplan", GetType(String)))
                        dt.Columns.Add(New DataColumn("currCode", GetType(String)))
                        Dim mdr As DataRow = dt.NewRow
                        mdr("hotelcode") = hdInt_partycode.Text
                        Dim strInt_RoomtypeCodes As String() = hdInt_RoomtypeCodes.Text.Split(";")
                        Dim strRoomCode As String() = strInt_RoomtypeCodes(0).Split(":")
                        mdr("roomtypecode") = strRoomCode(0)
                        mdr("cost") = obj.netPenaltyFee
                        mdr("noofroom") = objBLLHotelSearch.Room ' resDetail.result.distributions(i).numberRooms
                        mdr("noofadult") = objBLLHotelSearch.Adult
                        mdr("noofchild") = objBLLHotelSearch.Children
                        mdr("currCode") = hdInt_costcurrcode.Text
                        dt.Rows.Add(mdr)

                        dt.TableName = "Table"
                        Dim searchdetail As String = objclsUtilities.GenerateXML_FromDataTable(dt)
                        Dim param(8) As SqlParameter
                        param(0) = New SqlParameter("@checkin", CheckIn)
                        param(1) = New SqlParameter("@checkout", CheckOut)
                        param(2) = New SqlParameter("@agentcode", objBLLHotelSearch.AgentCode)
                        param(3) = New SqlParameter("@ctryCode", objBLLHotelSearch.SourceCountryCode)
                        param(4) = New SqlParameter("@searchxml", searchdetail)
                        param(5) = New SqlParameter("@userlogged", Session("GlobalUserName"))
                        param(6) = New SqlParameter("@descFlag", 0)
                        param(7) = New SqlParameter("@mappingSource", 0)
                        param(8) = New SqlParameter("@clientCode", "oneDMC")
                        Dim resultDS As DataSet = objclsUtilities.GetDataSet("int_markup", param)

                        If resultDS.Tables(0).Rows.Count > 0 Then

                            strCancelPolicy = strCancelPolicy & "<br /> Date and time of the starting cancel penalty: " & obj.dateTime & "<br /> Price of the cancel penalty : " & resultDS.Tables(0).Rows(0)("saleValue").ToString & " " & resultDS.Tables(0).Rows(0)("agentcurrcode").ToString
                        End If

                    Next
                    lblCancelationPolicy.Text = strCancelPolicy



                Else
                    Dim dt As New DataTable
                    Dim strData As String = ""
                    dt = objBLLHotelSearch.GetCancelationPolicyDetails(lblRMPartyCode.Text, lblRMRoomTypeCode.Text, lblRMMealPlanCode.Text, lblRMRatePlanId.Text, objBLLHotelSearch.AgentCode, txtCountryCode.Text, txtCheckIn.Text, txtCheckOut.Text)
                    If dt.Rows.Count > 0 Then

                        For i As Integer = 0 To dt.Rows.Count - 1
                            strData = strData & dt.Rows(i)("fromdate").ToString & " to " & dt.Rows(i)("todate").ToString & " - " & dt.Rows(i)("canceltext").ToString & "</br>"
                        Next
                        lblCancelationPolicy.Text = strData.Replace(Chr(10), "<br />")
                    Else
                        lblCancelationPolicy.Text = ""
                    End If

                    strData = ""
                    Dim dtCheckin As New DataTable
                    '(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String, ByVal strNoOfExtraBed As String) As DataTable
                    dtCheckin = objBLLHotelSearch.GetBooking_checkinoutpolicy(lblRMPartyCode.Text, lblRMRoomTypeCode.Text, lblRMMealPlanCode.Text, lblRMRatePlanId.Text, objBLLHotelSearch.AgentCode, txtCountryCode.Text, txtCheckIn.Text, txtCheckOut.Text)
                    If dtCheckin.Rows.Count > 0 Then

                        strData = strData + strData.Replace(Chr(10), "<br />")
                        strData = strData + "<span><b> Check In Check out Policy:</b></span>  "

                        For i As Integer = 0 To dtCheckin.Rows.Count - 1
                            strData = strData & dtCheckin.Rows(i)("fromdate").ToString & " to " & dtCheckin.Rows(i)("todate").ToString & " - " & dtCheckin.Rows(i)("checkinoutpolicytext").ToString & "</br>"
                        Next
                        lblCancelationPolicy.Text = lblCancelationPolicy.Text & " " & strData.Replace(Chr(10), "<br />")
                        '  Else
                        ' lblCancelationPolicy.Text = ""
                    End If
                End If

          


                Dim mpCancelationPolicy As AjaxControlToolkit.ModalPopupExtender = CType(gvGridviewRow.FindControl("mpCancelationPolicy"), AjaxControlToolkit.ModalPopupExtender)
                mpCancelationPolicy.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbCancelationPolicy_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' Page_Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dsSearchResults As New DataSet
            dsSearchResults = Session("sDSSearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sMailBoxPageIndex") = pageIndex.ToString
            BindHotelMainDetailsWithFilter(dsSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    ''' <summary>
    ''' PopulatePager
    ''' </summary>
    ''' <param name="recordCount"></param>
    ''' <remarks></remarks>
    Private Sub PopulatePager(ByVal recordCount As Integer)

        Dim currentPage As Integer = 1
        If Not Session("sMailBoxPageIndex") Is Nothing Then
            currentPage = Session("sMailBoxPageIndex")
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
    ''' <summary>
    ''' lbLocationMap_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbLocationMap_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lbLocationMap As LinkButton = CType(sender, LinkButton)
            Dim dlItem As DataListItem = CType(lbLocationMap.NamingContainer, DataListItem)
            Dim lblHotelName As Label = CType(dlItem.FindControl("lblHotelName"), Label)
            Dim hdMapLong As HiddenField = CType(dlItem.FindControl("hdMapLong"), HiddenField)
            Dim hdMapLatt As HiddenField = CType(dlItem.FindControl("hdMapLatt"), HiddenField)
            If lblHotelName.Text <> "" Then
                lblLocMaphotelName.Text = lblHotelName.Text
            Else
                lblLocMaphotelName.Text = ""
            End If
            If hdMapLatt.Value = "0.000000000000" And hdMapLong.Value = "0.000000000000" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Sorry. Location is not saved.")
                Exit Sub
            End If

            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "javascript:myMap('" & hdMapLatt.Value & "' ,'" & hdMapLong.Value & "' ,'" & lblHotelName.Text & "');", True)

            Dim mpLocationMap As AjaxControlToolkit.ModalPopupExtender = CType(dlItem.FindControl("mpLocationMap"), AjaxControlToolkit.ModalPopupExtender)
            mpLocationMap.Show()

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbLocationMap_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
            ModalPopupDays.Hide()
        End Try

    End Sub
    ''' <summary>
    ''' BindRecentlyViewedHotels
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' lbTotalprice_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbTotalprice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If hdBookingEngineRateType.Value = "1" Then
                Exit Sub
            End If
            Session("sSender") = sender
            Session("sEventArgs") = e
            Dim lbTotalPrice As LinkButton = CType(sender, LinkButton)
            Dim gvGridviewRow As GridViewRow = CType(lbTotalPrice.NamingContainer, GridViewRow)
            Dim lblRoomType As Label = CType(gvGridviewRow.FindControl("lblRoomType"), Label)

            Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
            Dim lblRMRatePlanId As Label = CType(gvGridviewRow.FindControl("lblRMRatePlanId"), Label)
            Dim lblRMRoomTypeCode As Label = CType(gvGridviewRow.FindControl("lblRMRoomTypeCode"), Label)
            Dim lblRMMealPlanCode As Label = CType(gvGridviewRow.FindControl("lblRMMealPlanCode"), Label)
            Dim lblRMcatCode As Label = CType(gvGridviewRow.FindControl("lblRMcatCode"), Label)
            Dim lblRMSharingOrExtraBed As Label = CType(gvGridviewRow.FindControl("lblRMSharingOrExtraBed"), Label)
            Dim lblRMCurcode As Label = CType(gvGridviewRow.FindControl("lblRMCurcode"), Label)
            Dim lblRMWlCurcode As Label = CType(gvGridviewRow.FindControl("lblRMWlCurcode"), Label)
            Dim lblCompCust As Label = CType(gvGridviewRow.FindControl("lblCompCust"), Label)
            Dim lblCompSupp As Label = CType(gvGridviewRow.FindControl("lblCompSupp"), Label)
            Dim lblComparrtrf As Label = CType(gvGridviewRow.FindControl("lblComparrtrf"), Label)
            Dim lblCompdeptrf As Label = CType(gvGridviewRow.FindControl("lblCompdeptrf"), Label)
            Dim lblNoOfRoom As Label = CType(gvGridviewRow.FindControl("lblNoOfRoom"), Label)
            Dim lblRMRoomId As Label = CType(gvGridviewRow.FindControl("lblRMRoomId"), Label)
            Dim hdRoomRatePlanSource As Label = CType(gvGridviewRow.FindControl("hdRoomRatePlanSource"), Label)
            '  Dim spAvgPrice As HtmlGenericControl = CType(gvGridviewRow.FindControl("spAvgPrice"), HtmlGenericControl)
            hdRMPartyCode.Value = lblRMPartyCode.Text
            hdRMRatePlanId.Value = lblRMRatePlanId.Text
            hdRMRoomTypeCode.Value = lblRMRoomTypeCode.Text
            hdRMMealPlanCode.Value = lblRMMealPlanCode.Text
            hdRMRoomId.Value = lblRMRoomId.Text
            hdRoomRatePlanSourcePopup.Value = hdRoomRatePlanSource.Text
            Dim strRoomCat As String() = lblRMcatCode.Text.Split("(")
            'If strRoomCat.Length > 0 Then
            '    hdRMcatCode.Value = strRoomCat(0)
            'Else
            hdRMcatCode.Value = lblRMcatCode.Text
            'End If

            hdRMSharingOrExtraBed.Value = lblRMSharingOrExtraBed.Text
            hdRoomTypeCurrCode.Value = lblRMCurcode.Text
            hdRoomTypewlCurrCode.Value = lblRMWlCurcode.Text
            'Dim writer As New System.IO.StringWriter
            'dsSearchResults.Tables(0).WriteXml(writer, True)

            Session("slbTotalPrice") = lbTotalPrice

            'objBLLHotelSearch = New BLLHotelSearch
            'objBLLHotelSearch = Session("sobjBLLHotelSearch")


            Dim dsHotelRoomTypes As New DataSet
            dsHotelRoomTypes = Session("sdsHotelRoomTypes")
            Dim dvRatePlan As DataView = New DataView(dsHotelRoomTypes.Tables(2))
            dvRatePlan.RowFilter = "PartyCode='" & lblRMPartyCode.Text & "' and  rateplanid='" & lblRMRatePlanId.Text & "' and rmtypcode='" & lblRMRoomTypeCode.Text & "' and mealplans='" & lblRMMealPlanCode.Text & "' and  sharingorextrabed='" & lblRMSharingOrExtraBed.Text & "' and RoomId='" & lblRMRoomId.Text & "' " ' and rmcatcode='" & lblRMcatCode.Text & "' 

            If dvRatePlan.Count > 0 Then

                dlcolumnRepeatFlag = 0
                lblTotlaPriceHeading.Text = "Daily Price Breakup" 'lblNoOfRoom.Text & " Units- " & lblRoomType.Text.Replace("<br>", "")
                chkComplimentaryToCustomer.Checked = lblCompCust.Text
                chkComplimentaryFromSupplier.Checked = lblCompSupp.Text
                chkComplimentaryArrivalTransfer.Checked = lblComparrtrf.Text
                chkComplimentaryDepartureTransfer.Checked = lblCompdeptrf.Text
                dltotalPriceBreak.DataSource = dvRatePlan
                dltotalPriceBreak.DataBind()

                Dim strRoomNo As String
                strRoomNo = dvRatePlan.ToTable(True, "roomno").Rows.Count
                FillddlRoomNos(strRoomNo)
                mpTotalprice.Show()
            End If
            txtsalepriceForAll.Text = ""
            txtBreakupTotalPriceForAll.Text = ""
            txtBreakupTotalPriceForAll.Text = ""

            If Session("sLoginType") = "RO" Then
                If hdOveride.Value = "1" Then
                    dvFillPrice.Visible = True
                    dvPriceSave.Visible = True
                Else
                    dvFillPrice.Visible = False
                    dvPriceSave.Visible = False
                End If
            Else
                dvFillPrice.Visible = False
                dvPriceSave.Visible = False
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbTotalprice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' dltotalPriceBreak_ItemDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub dltotalPriceBreak_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dltotalPriceBreak.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim lblRoomNo As Label = CType(e.Item.FindControl("lblRoomNo"), Label)
                Dim dvcosttotal As HtmlGenericControl = CType(e.Item.FindControl("dvcosttotal"), HtmlGenericControl)
                Dim gvPricebreakup As GridView = CType(e.Item.FindControl("gvPricebreakup"), GridView)
                Dim lblwlSaleTotal As Label = CType(e.Item.FindControl("lblwlSaleTotal"), Label)
                If Session("sLoginType") = "RO" Then
                    dvcosttotal.Visible = True
                Else
                    dvcosttotal.Visible = False
                End If
                Dim spAvgPrice As HtmlGenericControl = CType(e.Item.FindControl("spAvgPrice"), HtmlGenericControl)
                ' Dim dvCostEdit As HtmlGenericControl = CType(e.Item.FindControl("dvCostEdit"), HtmlGenericControl)
                If hdRoomRatePlanSourcePopup.Value = "OneDMC" Then
                    spAvgPrice.Visible = True
                    dvCostEdit.Visible = False
                Else
                    spAvgPrice.Visible = False
                    dvCostEdit.Visible = True
                End If
                Dim dsPriceBreakupNew As DataSet
                dsPriceBreakupNew = Session("sdsHotelRoomTypes")

                If dsPriceBreakupNew.Tables(1).Rows.Count > 0 Then
                    Dim dv1 As DataView = New DataView(dsPriceBreakupNew.Tables(1))
                    dv1.RowFilter = " rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'  AND sharingorextrabed='" + hdRMSharingOrExtraBed.Value + "' and roomno='" & lblRoomNo.Text & "' and RoomId='" & hdRMRoomId.Value & "' "
                    dv1.Sort = "pricedate" 'changed by mohamed on 14/07/2018
                    If dv1.Count > 0 Then

                        gvPricebreakup.DataSource = dv1
                        gvPricebreakup.DataBind()

                    End If
                End If



                hdgvPricebreakupRowwCount.Value = gvPricebreakup.Rows.Count

                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    Dim dWlsaletotal = 0
                    Dim dWlMarkup As Decimal
                    For Each gvrow In gvPricebreakup.Rows
                        Dim txtsaleprice As TextBox = CType(gvrow.FindControl("txtsaleprice"), TextBox)
                        Dim lblwlmarkupperc As Label = CType(gvrow.FindControl("lblwlmarkupperc"), Label)
                        Dim lblwlconvrate As Label = CType(gvrow.FindControl("lblwlconvrate"), Label)

                        dWlMarkup = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                        dWlsaletotal = dWlsaletotal + Math.Round(Val(IIf(txtsaleprice.Text = "", "0", txtsaleprice.Text)) * dWlMarkup)
                    Next
                    lblwlSaleTotal.Text = Math.Round(dWlsaletotal).ToString
                End If


            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dltotalPriceBreak_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub dlHotelallotment_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlHotelallotment.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim lblRoomNo As Label = CType(e.Item.FindControl("lblRoomNo"), Label)
                Dim gvHotelAllotment As GridView = CType(e.Item.FindControl("gvHotelAllotment"), GridView)


                Dim dsHotelallotmentNew As DataSet
                dsHotelallotmentNew = Session("sdsHotelAllotments")

                '    lblRoomNo.Text =lblRoomNo.Text  +  " - " + Val(obj

                If dsHotelallotmentNew.Tables(0).Rows.Count > 0 Then
                    Dim dv1 As DataView = New DataView(dsHotelallotmentNew.Tables(0))
                    dv1.RowFilter = " roomno='" & lblRoomNo.Text & "' "

                    'dv1.Sort = "pricedate" 
                    If dv1.Count > 0 Then

                        gvHotelAllotment.DataSource = dv1
                        gvHotelAllotment.DataBind()

                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlHotelallotment_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    ''' <summary>
    ''' btnPriceBreakupSave_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnPriceBreakupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupSave.Click
        Try

            Dim dsHotelRoomTypes As DataSet
            dsHotelRoomTypes = Session("sdsHotelRoomTypes")

            Dim fSalePrice As Double = 0
            Dim dWlMarkup As Decimal

            For Each item As DataListItem In dltotalPriceBreak.Items

                Dim gvPricebreakup As GridView = CType(item.FindControl("gvPricebreakup"), GridView)
                Dim lblRoomNo As Label = CType(item.FindControl("lblRoomNo"), Label)

                Dim fRoomSalePrice As Double = 0
                Dim fRoomCostPrice As Double = 0

                For Each rows As GridViewRow In gvPricebreakup.Rows
                    Dim lblgvRoomNo As Label = CType(rows.FindControl("lblgvRoomNo"), Label)

                    Dim lblwlconvrate As Label = CType(rows.FindControl("lblwlconvrate"), Label)
                    Dim lblwlmarkupperc As Label = CType(rows.FindControl("lblwlmarkupperc"), Label)
                    Dim lblwlcurrcode As Label = CType(rows.FindControl("lblwlcurrcode"), Label)
                    Dim lblwlbreakupPrice As Label = CType(rows.FindControl("lblwlbreakupPrice"), Label)
                    dWlMarkup = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)

                    If lblRoomNo.Text.Trim = lblgvRoomNo.Text.Trim Then


                        Dim lblBreakupDate As Label = CType(rows.FindControl("lblBreakupDate"), Label)
                        Dim lblBreakupDate1 As Label = CType(rows.FindControl("lblBreakupDate1"), Label)
                        Dim txtsaleprice As TextBox = CType(rows.FindControl("txtsaleprice"), TextBox)
                        Dim txtBreakupTotalPrice As TextBox = CType(rows.FindControl("txtBreakupTotalPrice"), TextBox)
                        Dim lblgvRoomCatCode As Label = CType(rows.FindControl("lblgvRoomCatCode"), Label)


                        '' Added shahul 02/06/18
                        Dim txtBookingCode As TextBox = CType(rows.FindControl("txtBookingCode"), TextBox)

                        If txtsaleprice.Text <> "" Then
                            fSalePrice = fSalePrice + txtsaleprice.Text
                            fRoomSalePrice = fRoomSalePrice + txtsaleprice.Text
                        End If
                        If txtBreakupTotalPrice.Text <> "" Then
                            fRoomCostPrice = fRoomCostPrice + txtBreakupTotalPrice.Text
                        End If

                        If dsHotelRoomTypes.Tables(1).Rows.Count > 0 Then
                            Dim dr1 As DataRow = dsHotelRoomTypes.Tables(1).Select("pricedate='" & lblBreakupDate1.Text & "' AND rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'     AND sharingorextrabed='" + hdRMSharingOrExtraBed.Value + "' AND roomno='" + lblRoomNo.Text + "' and RoomId='" & hdRMRoomId.Value & "' ").FirstOrDefault
                            If Not dr1 Is Nothing Then
                                ''*** Danny 09/05/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                Dim Old_saleprice As Double = dr1("saleprice")
                                Dim New_saleprice As Double = txtsaleprice.Text.Trim

                                Dim dActualTotalPrice As Double = dr1("totalprice")
                                Dim dNewTotalPrice As Double = txtBreakupTotalPrice.Text.Trim

                                '*** CANCELED [No Price Change] Cost_Price_Override=0 
                                '*** CANCELED [Sale Price Only Change] Cost_Price_Override=1
                                '*** CANCELED [Cost Price Only Change] Cost_Price_Override=2
                                '*** CANCELED [Both Price Change] Cost_Price_Override=3
                                'If Old_saleprice <> New_saleprice Then
                                '    If dActualTotalPrice <> dNewTotalPrice Then
                                '        dr1("Cost_Price_Override") = 3
                                '    Else
                                '        dr1("Cost_Price_Override") = 1
                                '    End If
                                'ElseIf dActualTotalPrice <> dNewTotalPrice Then
                                '    dr1("Cost_Price_Override") = 2
                                'End If

                                '***  Cost or Sale Change VAT Calculate for Both  Cost_Price_Override=3
                                If Old_saleprice <> New_saleprice Then
                                    dr1("Cost_Price_Override") = 3
                                End If
                                If dActualTotalPrice <> dNewTotalPrice Then
                                    dr1("Cost_Price_Override") = 3
                                End If
                                ''*** Danny 09/05/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                                dr1("saleprice") = txtsaleprice.Text.Trim
                                dr1("wlsaleprice") = Math.Round(Val(txtsaleprice.Text.Trim) * dWlMarkup)

                                ' modified on 20180408 -- start

                                Dim dTotalPriceDiff As Double = 0
                                Dim dRoomRate As Double = dr1("roomrate")
                                If dActualTotalPrice > dNewTotalPrice Then
                                    dTotalPriceDiff = dActualTotalPrice - dNewTotalPrice
                                    dRoomRate = dRoomRate - dTotalPriceDiff
                                ElseIf dActualTotalPrice < dNewTotalPrice Then
                                    dTotalPriceDiff = dNewTotalPrice - dActualTotalPrice
                                    dRoomRate = dRoomRate + dTotalPriceDiff
                                End If
                                dr1("roomrate") = dRoomRate.ToString
                                ' modified on 20180408 -- end

                                dr1("totalprice") = txtBreakupTotalPrice.Text.Trim
                                dr1("saleprice") = txtsaleprice.Text.Trim
                                dr1("roomno") = lblRoomNo.Text
                                dr1("bookingcode") = txtBookingCode.Text.Trim '' Added shahul 02/06/18

                                'dr1("CostTaxableValue") = txtBreakupTotalPrice.Text.Trim
                                'dr1("CostNonTaxableValue") = txtBreakupTotalPrice.Text.Trim
                                'dr1("CostVATValue") = txtBreakupTotalPrice.Text.Trim

                            End If
                        End If
                        If dsHotelRoomTypes.Tables(2).Rows.Count > 0 Then
                            Dim dr1 As DataRow = dsHotelRoomTypes.Tables(2).Select("rateplanid = '" + hdRMRatePlanId.Value + "' AND partycode='" + hdRMPartyCode.Value + "' AND rmtypcode='" + hdRMRoomTypeCode.Value + "'  AND mealplans='" + hdRMMealPlanCode.Value + "'     AND sharingorextrabed='" + hdRMSharingOrExtraBed.Value + "' AND roomno='" + lblRoomNo.Text + "'  and RoomId='" & hdRMRoomId.Value & "' ").FirstOrDefault
                            If Not dr1 Is Nothing Then
                                dr1("saletotal") = (Math.Round(fRoomSalePrice, 2)).ToString
                                dr1("costtotal") = (Math.Round(fRoomCostPrice, 2)).ToString

                            End If

                        End If


                        Session("sdsHotelRoomTypes") = dsHotelRoomTypes

                    End If

                Next
            Next


            Dim lbTotalPrice As New LinkButton
            lbTotalPrice = CType(Session("slbTotalPrice"), LinkButton)

            Dim gvGridviewRow As GridViewRow = CType(lbTotalPrice.NamingContainer, GridViewRow)
            Dim _gvHotelRoomType As GridView = CType((gvGridviewRow.Parent.Parent), GridView)
            Dim dlRatePlanItem As DataListItem = CType(_gvHotelRoomType.NamingContainer, DataListItem)
            Dim _dlRatePlan As DataList = CType((dlRatePlanItem.Parent), DataList)
            Dim dlHotelsSearchResultsItem As DataListItem = CType(_dlRatePlan.NamingContainer, DataListItem)
            Dim _dlRatePlan1 As New DataList
            _dlRatePlan1 = CType(dlHotelsSearchResults.Items(dlHotelsSearchResultsItem.ItemIndex).FindControl("dlRatePlan"), DataList)
            Dim _gvHotelRoomType1 As New GridView
            _gvHotelRoomType1 = CType(_dlRatePlan1.Items(dlRatePlanItem.ItemIndex).FindControl("gvHotelRoomType"), GridView)

            Dim lbTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbTotalprice"), LinkButton)
            Dim lbwlTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbwlTotalprice"), LinkButton)
            Dim lblCompCust As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblCompCust"), Label)
            Dim lblCompSupp As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblCompSupp"), Label)
            Dim lblComparrtrf As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblComparrtrf"), Label)
            Dim lblCompdeptrf As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblCompdeptrf"), Label)

            lbTotalprice1.Text = fSalePrice.ToString.Replace(".00", "").Replace(".0", "") & " " & hdRoomTypeCurrCode.Value
            lbwlTotalprice1.Text = Math.Round(fSalePrice * dWlMarkup) & " " & hdRoomTypewlCurrCode.Value
            lblCompCust.Text = IIf(chkComplimentaryToCustomer.Checked, "1", "0")
            lblCompSupp.Text = IIf(chkComplimentaryFromSupplier.Checked, "1", "0")
            lblComparrtrf.Text = IIf(chkComplimentaryArrivalTransfer.Checked, "1", "0")
            lblCompdeptrf.Text = IIf(chkComplimentaryDepartureTransfer.Checked, "1", "0")


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPriceBreakupSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' GetRequestId
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRequestId() As String
        Dim strRequestId As String = ""
        If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sRequestId")
        End If
        Return strRequestId
    End Function
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
    ''' <summary>
    ''' GetRowLineNumber
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRowLineNumber() As Integer
        Dim iRowLineNumber As String = ""
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            Dim objBLLHotelSearch1 As BLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            iRowLineNumber = objBLLHotelSearch1.OBrlineno
            iRowLineNumber = iRowLineNumber + 1
        Else
            iRowLineNumber = 1
        End If
        Return iRowLineNumber
    End Function
    ''' <summary>
    ''' LoadRoomAdultChild
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRoomAdultChild()
        '*** Danny 26/08/2018
        '*** Danny 26/08/2019 Dynamic Rooms load
        Dim intNoAdults As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=54")
        Dim intNoChilds As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=55")
        FillSpecifiedAdultChild(intNoAdults, intNoChilds)

        '*** Danny 26/08/2019 Dynamic Rooms load
        Dim intRoomNos As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=53")
        FillSpecifiedRoom(intRoomNos)
    End Sub
    Public Sub ChildAgeTxtCreate(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim dtChildAge = New DataTable
        'dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
        'dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
        'dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
        'Dim dDLCh As DropDownList = sender
        'Dim strRomeNo As String = dDLCh.ClientID.Replace("dlNofoRooms_ctl", "").Replace("_ddlDynRoomChild", "")
        'Dim dDLAd As DropDownList = CType(dlNofoRooms.Items(strRomeNo).FindControl("ddlDynRoomAdult"), DropDownList)
        'If dDLAd.SelectedItem.Text.Trim().Replace("--", "").Length = 0 Then
        '    dDLCh.SelectedIndex = 0
        '    Exit Sub
        'End If
        'Dim s As String = dDLCh.SelectedValue()
        'For i As Integer = 0 To Val(sender.SelectedValue()) - 1
        '    Dim row As DataRow = dtChildAge.NewRow()
        '    If i = 0 Then
        '        row("colChildAgeLbl") = "Room " + (Val(strRomeNo) + 1).ToString + " Child Age"
        '    Else
        '        row("colChildAgeLbl") = ""
        '    End If
        '    row("colChildAge") = ""
        '    row("colCHNo") = "CH" + (i + 1).ToString
        '    dtChildAge.Rows.Add(row)
        'Next
        'Dim dlItem1 As DataListItem = dlNofoRooms.Items(Val(strRomeNo))
        'Dim ddlDynRoomAdult As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)

        'ddlDynRoomAdult.DataSource = dtChildAge
        'ddlDynRoomAdult.DataBind()

        'Dim ss As String = dlNofoRooms.Items(0).ToString()
        ''Dim dt As DataList = CType(e.FindControl("datalist2"), DataList)



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

    ''' <summary>
    ''' FillSpecifiedChildAges
    ''' </summary>
    ''' <param name="childages"></param>
    ''' <remarks></remarks>
    'Private Sub FillSpecifiedChildAges(ByVal childages As String)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild1, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild2, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild3, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild4, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild5, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild6, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild7, childages)
    '    objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild8, childages)

    'End Sub
    ''' <summary>
    ''' FillSpecifiedRoom
    ''' </summary>
    ''' <param name="NoOfRoom"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedRoom(ByVal NoOfRoom As String)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom, NoOfRoom)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom_Dynamic, NoOfRoom) '*** Danny 26/08/2018
    End Sub
    ''' <summary>
    ''' FillSpecifiedAdultChild
    ''' </summary>
    ''' <param name="adults"></param>
    ''' <param name="child"></param>
    ''' <remarks></remarks>
    ''' '*** Danny 26/08/2018
    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom1Adult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom2Adult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom3Adult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom4Adult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom5Adult, adults)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom6Adult, adults)

        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom1Child, child)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom2Child, child)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom3Child, child)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom4Child, child)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom5Child, child)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom6Child, child)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlPreHotelAdult, "1000")
        objclsUtilities.FillDropDownListBasedOnNumber(ddlPreHotelChild, child)
    End Sub



    ''' <summary>
    ''' dlSpecialEvents_ItemDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub dlSpecialEvents_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlSpecialEvents.ItemDataBound
        Try


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim ddlEvents As DropDownList = CType(e.Item.FindControl("ddlEvents"), DropDownList)
                Dim lblEventCode As Label = CType(e.Item.FindControl("lblEventCode"), Label)
                Dim spleventname As Label = CType(e.Item.FindControl("spleventname"), Label)
                Dim lblEventDate As Label = CType(e.Item.FindControl("lblEventDate"), Label)
                Dim gvSpecialEvents As GridView = CType(e.Item.FindControl("gvSpecialEvents"), GridView)


                Dim chkSpclComplimentaryToCustomer As CheckBox = CType(e.Item.FindControl("chkSpclComplimentaryToCustomer"), CheckBox)
                Dim chkSpclComplimentaryFromSupplier As CheckBox = CType(e.Item.FindControl("chkSpclComplimentaryFromSupplier"), CheckBox)
                If Session("sLoginType") = "RO" Then
                    chkSpclComplimentaryToCustomer.Visible = True
                    chkSpclComplimentaryFromSupplier.Visible = True
                Else
                    chkSpclComplimentaryToCustomer.Visible = False
                    chkSpclComplimentaryFromSupplier.Visible = False
                End If



                ddlEvents.Attributes.Add("onChange", "javascript:SpecialEventChanged(this, '" + e.Item.ItemIndex.ToString + "')")

                Dim strDate As String = lblEventDate.Text
                lblEventDate.Text = Format(CType(lblEventDate.Text, Date), "dd/MM/yyyy")
                Dim dss As New DataSet
                If Not Session("sdsSpecialEvents") Is Nothing Then
                    dss = Session("sdsSpecialEvents")
                    If dss.Tables(2).Rows.Count > 0 Then
                        Dim dvSclEvents As DataView = New DataView(dss.Tables(2))
                        dvSclEvents.RowFilter = "fromdate='" & strDate & "'"
                        ddlEvents.AppendDataBoundItems = True
                        ddlEvents.DataSource = dvSclEvents
                        ddlEvents.DataValueField = "spleventcode"
                        ddlEvents.DataTextField = "spleventname"
                        ddlEvents.DataBind()

                        ddlEvents.SelectedValue = lblEventCode.Text





                        Dim dtSelectedSpclEventRow As New DataTable
                        If Not Session("sdtSelectedSpclEvent") Is Nothing Then
                            dtSelectedSpclEventRow = Session("sdtSelectedSpclEvent")

                            If dtSelectedSpclEventRow.Rows.Count > 0 Then
                                Dim dvSclEventDetailsFilter As DataView = New DataView(dtSelectedSpclEventRow)
                                dvSclEventDetailsFilter.RowFilter = "PartyCode='" & hdSPEPartyCode.Value.Trim & "' AND RoomTypeCode ='" & hdSPERoomTypeCode.Value.Trim & "' AND MealPlanCode='" & hdSPEMealPlanCode.Value.Trim & "' AND  CatCode='" & hdSPEcatCode.Value.Trim & "' AND AccCode='" & hdSPEAccCode.Value.Trim & "' AND RatePlanId='" & hdSPERatePlanId.Value.Trim & "'  AND spleventdate='" & strDate & "' "
                                If dvSclEventDetailsFilter.Count > 0 Then
                                    ddlEvents.SelectedValue = dvSclEventDetailsFilter.Item(0)("spleventcode").ToString
                                    lblEventCode.Text = dvSclEventDetailsFilter.Item(0)("spleventcode").ToString
                                    strDate = dvSclEventDetailsFilter.Item(0)("spleventdate").ToString
                                    lblEventDate.Text = Format(CType(strDate, Date), "dd/MM/yyyy")
                                End If

                            End If

                            Dim dvSclEventDetails1 As DataView = New DataView(dtSelectedSpclEventRow)



                            dvSclEventDetails1.RowFilter = "PartyCode='" & hdSPEPartyCode.Value.Trim & "' AND RoomTypeCode ='" & hdSPERoomTypeCode.Value.Trim & "' AND MealPlanCode='" & hdSPEMealPlanCode.Value.Trim & "' AND  CatCode='" & hdSPEcatCode.Value.Trim & "' AND AccCode='" & hdSPEAccCode.Value.Trim & "' AND RatePlanId='" & hdSPERatePlanId.Value.Trim & "' AND spleventcode='" & lblEventCode.Text & "'  AND spleventdate='" & strDate & "' " ' AND splistcode='" & strSplistcode & "' AND splineno='" & strSpLineno & "'    AND paxtype='" & strSpPaxtype & "'  and childage='" & strSpChildAge & "'
                            If dvSclEventDetails1.Count > 0 Then
                                gvSpecialEvents.DataSource = dvSclEventDetails1
                                gvSpecialEvents.DataBind()
                            Else
                                Dim dvSclEventDetails As DataView = New DataView(dss.Tables(1))
                                dvSclEventDetails.RowFilter = "spleventdate='" & strDate & "' AND spleventcode='" & ddlEvents.SelectedValue & "'"
                                gvSpecialEvents.DataSource = dvSclEventDetails
                                gvSpecialEvents.DataBind()
                            End If

                        Else
                            Dim dvSclEventDetails As DataView = New DataView(dss.Tables(1))
                            dvSclEventDetails.RowFilter = "spleventdate='" & strDate & "' AND spleventcode='" & ddlEvents.SelectedValue & "'"
                            gvSpecialEvents.DataSource = dvSclEventDetails
                            gvSpecialEvents.DataBind()
                        End If


                    End If
                End If



            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlSpecialEvents_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    ''' <summary>
    ''' btnSelectedSpclEvent_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSelectedSpclEvent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectedSpclEvent.Click
        Try

            Dim iValue As Integer = hddlSpclEventRowNumber.Value
            Dim dlItem As DataListItem = dlSpecialEvents.Items(iValue)


            Dim _dlItem As DataListItem = CType((dlSpecialEvents.Items(dlItem.ItemIndex)), DataListItem)


            Dim ddlEvents As DropDownList = CType(_dlItem.FindControl("ddlEvents"), DropDownList)
            Dim gvSpecialEvents As GridView = CType(_dlItem.FindControl("gvSpecialEvents"), GridView)
            Dim lblEventDatefull As Label = CType(_dlItem.FindControl("lblEventDatefull"), Label)

            Dim lblsplremarks As Label = CType(_dlItem.FindControl("lblsplremarks"), Label)  '' Added shahul 26/07/2018

            Dim lbSpecialEvents As New LinkButton
            lbSpecialEvents = CType(Session("slbSpecialEvents"), LinkButton)

            Dim gvGridviewRow As GridViewRow = CType(lbSpecialEvents.NamingContainer, GridViewRow)
            Dim _gvHotelRoomType As GridView = CType((gvGridviewRow.Parent.Parent), GridView)

            Dim dlRatePlanItem As DataListItem = CType(_gvHotelRoomType.NamingContainer, DataListItem)
            Dim _dlRatePlan As DataList = CType((dlRatePlanItem.Parent), DataList)

            Dim dlHotelsSearchResultsItem As DataListItem = CType(_dlRatePlan.NamingContainer, DataListItem)

            Dim _dlRatePlan1 As New DataList
            _dlRatePlan1 = CType(dlHotelsSearchResults.Items(dlHotelsSearchResultsItem.ItemIndex).FindControl("dlRatePlan"), DataList)

            Dim _gvHotelRoomType1 As New GridView
            _gvHotelRoomType1 = CType(_dlRatePlan1.Items(dlRatePlanItem.ItemIndex).FindControl("gvHotelRoomType"), GridView)


            Dim mpSpecialEvents As AjaxControlToolkit.ModalPopupExtender = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("mpSpecialEvents"), AjaxControlToolkit.ModalPopupExtender)


            objBLLHotelSearch = New BLLHotelSearch
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
            If Not objBLLHotelSearch Is Nothing Then






                Dim dsSpecialEventsSelected As New DataSet
                If Not Session("sdsSpecialEvents") Is Nothing Then
                    dsSpecialEventsSelected = Session("sdsSpecialEvents")
                    If dsSpecialEventsSelected.Tables(1).Rows.Count > 0 Then

                        Dim dtSelectedSpclEventRow As New DataTable
                        If Not Session("sdtSelectedSpclEvent") Is Nothing Then
                            dtSelectedSpclEventRow = Session("sdtSelectedSpclEvent")
                            Dim dvSclEventDetails1 As DataView = New DataView(dtSelectedSpclEventRow)
                            dvSclEventDetails1.RowFilter = "PartyCode='" & hdSPEPartyCode.Value.Trim & "' AND RoomTypeCode ='" & hdSPERoomTypeCode.Value.Trim & "' AND MealPlanCode='" & hdSPEMealPlanCode.Value.Trim & "' AND  CatCode='" & hdSPEcatCode.Value.Trim & "' AND AccCode='" & hdSPEAccCode.Value.Trim & "' AND RatePlanId='" & hdSPERatePlanId.Value.Trim & "' AND spleventcode='" & ddlEvents.SelectedValue & "'  AND spleventdate='" & lblEventDatefull.Text & "' " ' AND splistcode='" & strSplistcode & "' AND splineno='" & strSpLineno & "'    AND paxtype='" & strSpPaxtype & "'  and childage='" & strSpChildAge & "'
                            If dvSclEventDetails1.Count > 0 Then
                                gvSpecialEvents.DataSource = dvSclEventDetails1
                                gvSpecialEvents.DataBind()
                            Else
                                Dim dvSpclEventsSelected As DataView = New DataView(dsSpecialEventsSelected.Tables(1))
                                dvSpclEventsSelected.RowFilter = "spleventdate='" & lblEventDatefull.Text & "' AND spleventcode='" & ddlEvents.SelectedValue & "'"
                                If dvSpclEventsSelected.Count > 0 Then
                                    gvSpecialEvents.DataSource = dvSpclEventsSelected
                                    gvSpecialEvents.DataBind()
                                    lblsplremarks.Text = dvSpclEventsSelected.ToTable.Rows(0)("remarks")  '' Added shahul 26/07/18
                                Else
                                    Dim dsSpecialEventsNew As New DataSet
                                    Dim strDate As String = Format(CType(lblEventDatefull.Text, Date), "yyyy/MM/dd")
                                    dsSpecialEventsNew = GetSpecialEventDetails(lbSpecialEvents, gvGridviewRow, strDate & "," & ddlEvents.SelectedValue)
                                    If dsSpecialEventsNew.Tables(0).Rows.Count > 0 Then
                                        gvSpecialEvents.DataSource = dsSpecialEventsNew.Tables(0)
                                        gvSpecialEvents.DataBind()

                                        Dim dss As DataSet
                                        dss = Session("sdsSpecialEvents")
                                        If dss.Tables.Count > 1 Then
                                            dss.Tables(1).Rows.Clear()
                                            dss.Tables(1).Merge(dsSpecialEventsNew.Tables(0))
                                            Session("sdsSpecialEvents") = dss
                                        End If

                                    End If
                                End If
                            End If

                        Else
                            Dim dvSpclEventsSelected As DataView = New DataView(dsSpecialEventsSelected.Tables(1))
                            dvSpclEventsSelected.RowFilter = "spleventdate='" & lblEventDatefull.Text & "' AND spleventcode='" & ddlEvents.SelectedValue & "'"
                            If dvSpclEventsSelected.Count > 0 Then
                                gvSpecialEvents.DataSource = dvSpclEventsSelected
                                gvSpecialEvents.DataBind()
                                lblsplremarks.Text = dvSpclEventsSelected.ToTable.Rows(0)("remarks")  '' Added shahul 26/07/18
                            Else
                                Dim dsSpecialEventsNew As New DataSet
                                Dim strDate As String = Format(CType(lblEventDatefull.Text, Date), "yyyy/MM/dd")
                                dsSpecialEventsNew = GetSpecialEventDetails(lbSpecialEvents, gvGridviewRow, strDate & "," & ddlEvents.SelectedValue)
                                If dsSpecialEventsNew.Tables(0).Rows.Count > 0 Then
                                    gvSpecialEvents.DataSource = dsSpecialEventsNew.Tables(0)
                                    gvSpecialEvents.DataBind()

                                    Dim dss As DataSet
                                    dss = Session("sdsSpecialEvents")
                                    If dss.Tables.Count > 1 Then
                                        dss.Tables(1).Rows.Clear()
                                        dss.Tables(1).Merge(dsSpecialEventsNew.Tables(0))
                                        Session("sdsSpecialEvents") = dss
                                    End If

                                End If
                            End If
                        End If

                        mpSpecialEvents.Show()
                    End If
                End If
            Else
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnSelectedSpclEvent_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub


    ''' <summary>
    ''' gvPricebreakup_RowDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvPricebreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If Session("sLoginType") = "RO" Then
                e.Row.Cells(1).Visible = True
                e.Row.Cells(3).Visible = True
            Else
                e.Row.Cells(1).Visible = False
                e.Row.Cells(3).Visible = False
            End If
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                Dim lblBreakupDate As Label = CType(e.Row.FindControl("lblBreakupDate"), Label)
                Dim lblBreakupDateName As Label = CType(e.Row.FindControl("lblBreakupDateName"), Label)
                Dim lblbreakupPrice As Label = CType(e.Row.FindControl("lblbreakupPrice"), Label)
                Dim lblBreakupTotalPrice As Label = CType(e.Row.FindControl("lblBreakupTotalPrice"), Label)
                Dim lblBookingCode As Label = CType(e.Row.FindControl("lblBookingCode"), Label)
                If lblBookingCode.Text.Length > 16 Then
                    lblBookingCode.Text = lblBookingCode.Text.Substring(0, 15) & ".."
                End If

                Dim txtsaleprice As TextBox = CType(e.Row.FindControl("txtsaleprice"), TextBox)
                Dim txtBreakupTotalPrice As TextBox = CType(e.Row.FindControl("txtBreakupTotalPrice"), TextBox)
                Dim dtDate As DateTime = CType(lblBreakupDate.Text, DateTime)
                lblBreakupDate.Text = dtDate.ToString("dd/MM/yyyy") & ", "
                lblBreakupDateName.Text = dtDate.ToString("dddd").Substring(0, 3)
                Dim lblUSDPrice As Label = CType(e.Row.FindControl("lblUSDPrice"), Label)
                Dim lblConversionRate As Label = CType(e.Row.FindControl("lblConversionRate"), Label)

                Dim lblSalePriceCurrcode As Label = CType(e.Row.FindControl("lblSalePriceCurrcode"), Label)
                Dim lblCostPriceCurrcode As Label = CType(e.Row.FindControl("lblCostPriceCurrcode"), Label)
                Dim lblwlcurrcode As Label = CType(e.Row.FindControl("lblwlcurrcode"), Label)

                Dim gvGridviewRow As GridViewRow = CType(lblConversionRate.NamingContainer, GridViewRow)
                Dim _gvPricebreakup As GridView = CType((gvGridviewRow.Parent.Parent), GridView)
                Dim dlPriceBreakupItem As DataListItem = CType(_gvPricebreakup.NamingContainer, DataListItem)
                Dim lblSaleTotal As Label = CType(dlPriceBreakupItem.FindControl("lblSaleTotal"), Label)
                Dim lblCostTotal As Label = CType(dlPriceBreakupItem.FindControl("lblCostTotal"), Label)
                Dim lblSaleTotalCurCode As Label = CType(dlPriceBreakupItem.FindControl("lblSaleTotalCurCode"), Label)
                Dim lblCostTotalCurCode As Label = CType(dlPriceBreakupItem.FindControl("lblCostTotalCurCode"), Label)

                Dim lblwlSaleTotal As Label = CType(dlPriceBreakupItem.FindControl("lblwlSaleTotal"), Label)
                Dim lblwlSaleTotalCurCode As Label = CType(dlPriceBreakupItem.FindControl("lblwlSaleTotalCurCode"), Label)
                lblwlSaleTotalCurCode.Text = lblwlcurrcode.Text

                lblSaleTotalCurCode.Text = lblSalePriceCurrcode.Text
                lblCostTotalCurCode.Text = lblCostPriceCurrcode.Text
                Dim lblSalePriceCurrcodeHeader As Label = CType(_gvPricebreakup.HeaderRow.FindControl("lblSalePriceCurrcodeHeader"), Label)
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    lblSalePriceCurrcodeHeader.Text = "PRICE(" & lblwlcurrcode.Text & ")"
                Else
                    lblSalePriceCurrcodeHeader.Text = "PRICE(" & lblSalePriceCurrcode.Text & ")"
                End If

                Dim lblCostPriceCurrcodeHeader As Label = CType(_gvPricebreakup.HeaderRow.FindControl("lblCostPriceCurrcodeHeader"), Label)

                lblCostPriceCurrcodeHeader.Text = "COST PRICE(" & lblCostPriceCurrcode.Text & ")"

                Dim lblwlconvrate As Label = CType(e.Row.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(e.Row.FindControl("lblwlmarkupperc"), Label)

                Dim lblwlbreakupPrice As Label = CType(e.Row.FindControl("lblwlbreakupPrice"), Label)
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)
                'Dim _dlRatePlan As DataList = CType((dlPriceBreakupItem.Parent), DataList)

                txtsaleprice.Attributes.Add("onChange", "javascript:CalculateRoomTotalPrice('" + lblSaleTotal.ClientID + "','" + _gvPricebreakup.ClientID + "','" + dWlMarkup.ToString + "','" + lblwlbreakupPrice.ClientID + "','" + txtsaleprice.ClientID + "')")
                txtBreakupTotalPrice.Attributes.Add("onChange", "javascript:CalculateUSDAndCostPriceTotal('" + txtBreakupTotalPrice.ClientID + "', '" + lblConversionRate.Text + "', '" + lblUSDPrice.ClientID + "','" + lblCostTotal.ClientID + "','" + _gvPricebreakup.ClientID + "','" + lblSalePriceCurrcode.Text + "' )") ' modified by abin on 20180711
                If hdRoomRatePlanSourcePopup.Value = "OneDMC" Then
                    txtBreakupTotalPrice.Attributes.Add("onkeypress", "fnReadOnly(this)")
                    txtBreakupTotalPriceForAll.Attributes.Add("onkeypress", "fnReadOnly(this)")
                Else
                    txtBreakupTotalPrice.Attributes.Add("onkeypress", "validateDecimalOnly(event,this)")
                    txtBreakupTotalPriceForAll.Attributes.Add("onkeypress", "validateDecimalOnly(event,this)")
                End If
                ' onkeypress="validateDecimalOnly(event,this)"
                'onkeypress="fnReadOnly(this)"
                Dim dPrice As Double = CType(lblbreakupPrice.Text, Double)
                Dim dCostPrice As Double = CType(lblBreakupTotalPrice.Text, Double)
                Dim dConvRate As Double = CType(lblConversionRate.Text, Double)

                lblbreakupPrice.Text = Math.Round(dPrice, 2).ToString
                lblBreakupTotalPrice.Text = Math.Round(dCostPrice, 2).ToString ' modified by abin on 20180711

                '' Added shahul 02/06/18
                Dim txtBookingCode As TextBox = CType(e.Row.FindControl("txtBookingCode"), TextBox)

                lblUSDPrice.Text = "(" & (Math.Round((dCostPrice * dConvRate), 2)).ToString & " " & lblSalePriceCurrcode.Text & ")" ' modified by abin on 20180711


                'Dim dvPriceDate As HtmlGenericControl = CType(e.Row.FindControl("dvPriceDate"), HtmlGenericControl)
                'Dim dvPricePerNight As HtmlGenericControl = CType(e.Row.FindControl("dvPricePerNight"), HtmlGenericControl)
                'Dim dvCostPricePerNight As HtmlGenericControl = CType(e.Row.FindControl("dvCostPricePerNight"), HtmlGenericControl)
                'Dim dvCostPricePerNightText As HtmlGenericControl = CType(e.Row.FindControl("dvCostPricePerNightText"), HtmlGenericControl)

                If Session("sLoginType") <> "RO" Then

                    lblbreakupPrice.Visible = True
                    lblBreakupTotalPrice.Visible = True
                    txtsaleprice.Visible = False
                    txtBreakupTotalPrice.Visible = False
                    ' dvPriceBreakupSave.Visible = False
                    chkComplimentaryArrivalTransfer.Visible = False
                    chkComplimentaryDepartureTransfer.Visible = False
                    chkComplimentaryFromSupplier.Visible = False
                    chkComplimentaryToCustomer.Visible = False

                    '' Added shahul 02/06/18
                    txtBookingCode.Visible = False
                    lblBookingCode.Visible = True
                Else
                    If hdOveride.Value = "1" Then
                        lblbreakupPrice.Visible = False
                        lblBreakupTotalPrice.Visible = False
                        txtsaleprice.Visible = True
                        txtBreakupTotalPrice.Visible = True
                        '  dvPriceBreakupSave.Visible = True
                        '' Added shahul 02/06/18
                        lblBookingCode.Visible = False
                        txtBookingCode.Visible = True
                    Else
                        lblbreakupPrice.Visible = True
                        lblBreakupTotalPrice.Visible = True
                        txtsaleprice.Visible = False
                        txtBreakupTotalPrice.Visible = False
                        '    dvPriceBreakupSave.Visible = False
                        '' Added shahul 02/06/18
                        txtBookingCode.Visible = False
                        lblBookingCode.Visible = True
                    End If





                    chkComplimentaryArrivalTransfer.Visible = True
                    chkComplimentaryDepartureTransfer.Visible = True
                    chkComplimentaryFromSupplier.Visible = True
                    chkComplimentaryToCustomer.Visible = True
                End If
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    '  lblwlSaleTotal.Text = Math.Round(Val(lblSaleTotal.Text) * dWlMarkup)
                    lblbreakupPrice.Attributes.Add("style", "display:none")
                    txtsaleprice.Attributes.Add("style", "display:none")
                    lblwlbreakupPrice.Attributes.Add("style", "display:block")
                    lblwlSaleTotal.Attributes.Add("style", "display:block")
                    lblwlSaleTotalCurCode.Attributes.Add("style", "display:block")
                    lblSaleTotal.Attributes.Add("style", "display:none")
                    lblSaleTotalCurCode.Attributes.Add("style", "display:none")
                Else
                    lblbreakupPrice.Attributes.Add("style", "display:block")
                    txtsaleprice.Attributes.Add("style", "display:block")
                    lblwlbreakupPrice.Attributes.Add("style", "display:none")
                    lblwlSaleTotal.Attributes.Add("style", "display:none")
                    lblwlSaleTotalCurCode.Attributes.Add("style", "display:none")
                    lblSaleTotal.Attributes.Add("style", "display:block")
                    lblSaleTotalCurCode.Attributes.Add("style", "display:block")
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: gvPricebreakup_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' gvSpecialEvents_RowDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvSpecialEvents_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If hdBookingEngineRateType.Value = "1" Then
                If (e.Row.RowType = DataControlRowType.Header) Then
                    e.Row.Cells(3).Visible = False
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(7).Visible = False
                End If
            End If

            ' added shahul 28/07/18
            If Session("sLoginType") = "RO" Then
                e.Row.Cells(6).Visible = True
                e.Row.Cells(7).Visible = True
            Else
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then

                Dim lblpaxcurrcode As Label = CType(e.Row.FindControl("lblpaxcurrcode"), Label)
                Dim lblcostCurrcode As Label = CType(e.Row.FindControl("lblcostCurrcode"), Label)

                Dim lblNoOfPax As Label = CType(e.Row.FindControl("lblNoOfPax"), Label)

                Dim txtPaxRate As TextBox = CType(e.Row.FindControl("txtPaxRate"), TextBox)
                Dim txtPaxCost As TextBox = CType(e.Row.FindControl("txtPaxCost"), TextBox)

                Dim lblPaxRate As Label = CType(e.Row.FindControl("lblPaxRate"), Label)
                Dim lblPaxCost As Label = CType(e.Row.FindControl("lblPaxCost"), Label)

                Dim lblwlSpecialEventValue As Label = CType(e.Row.FindControl("lblwlSpecialEventValue"), Label)

                Dim lblwlconvrate As Label = CType(e.Row.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(e.Row.FindControl("lblwlmarkupperc"), Label)
                Dim lblwlcurrcode As Label = CType(e.Row.FindControl("lblwlcurrcode"), Label)
                Dim lblwlPaxRate As Label = CType(e.Row.FindControl("lblwlPaxRate"), Label)
                lblwlPaxRate.Text = Math.Round(Val(lblwlPaxRate.Text))
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)


                Dim lblSpecialEventValue As Label = CType(e.Row.FindControl("lblSpecialEventValue"), Label)
                Dim lblSpecialEventCostValue As Label = CType(e.Row.FindControl("lblSpecialEventCostValue"), Label)

                txtPaxRate.Text = Math.Round(CType(Val(txtPaxRate.Text), Double), 2).ToString '*** Danny - added Val 
                txtPaxCost.Text = Math.Round(CType(Val(txtPaxCost.Text), Double), 2).ToString '*** Danny - added Val 
                lblPaxRate.Text = Math.Round(CType(Val(lblPaxRate.Text), Double), 2).ToString '*** Danny - added Val 
                lblPaxCost.Text = Math.Round(CType(Val(lblPaxCost.Text), Double), 2).ToString '*** Danny - added Val 

                lblSpecialEventValue.Text = Math.Round(CType(Val(lblSpecialEventValue.Text), Double), 2).ToString & " " & lblpaxcurrcode.Text '*** Danny - added Val 
                lblSpecialEventCostValue.Text = Math.Round(CType(Val(lblSpecialEventCostValue.Text), Double), 2).ToString & " " & lblcostCurrcode.Text '*** Danny - added Val 


                txtPaxRate.Attributes.Add("onChange", "javascript:CalculateSpecialEventSaleValue('" + CType(lblNoOfPax.Text, String) + "', '" + CType(txtPaxRate.ClientID, String) + "','" + CType(lblSpecialEventValue.ClientID, String) + "','" + CType(lblpaxcurrcode.Text, String) + "','" + CType(lblwlSpecialEventValue.ClientID, String) + "','" + CType(dWlMarkup, String) + "','" + CType(lblwlcurrcode.Text, String) + "','" + CType(lblwlPaxRate.ClientID, String) + "')")
                txtPaxCost.Attributes.Add("onChange", "javascript:CalculateSpecialEventSaleValue('" + CType(lblNoOfPax.Text, String) + "', '" + CType(txtPaxCost.ClientID, String) + "','" + CType(lblSpecialEventCostValue.ClientID, String) + "','" + CType(lblcostCurrcode.Text, String) + "')")

                If Session("sLoginType") = "RO" Then
                    If chkOveridePrice.Checked = True Then
                        txtPaxRate.Visible = True
                        lblPaxRate.Visible = False
                        txtPaxCost.Visible = True
                        lblPaxCost.Visible = False
                    Else
                        txtPaxRate.Visible = False
                        lblPaxRate.Visible = True
                        txtPaxCost.Visible = False
                        lblPaxCost.Visible = True
                    End If
                Else
                    txtPaxRate.Visible = False
                    lblPaxRate.Visible = True
                    txtPaxCost.Visible = False
                    lblPaxCost.Visible = False ' True added shahul 28/07/18
                    lblSpecialEventCostValue.Visible = False ' True added shahul 28/07/18
                End If
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    If hdBookingEngineRateType.Value = "1" Then
                        lblPaxRate.Attributes.Add("style", "display:none")
                        lblwlPaxRate.Attributes.Add("style", "display:none")

                        lblSpecialEventValue.Attributes.Add("style", "display:none")
                        lblwlSpecialEventValue.Attributes.Add("style", "display:none")
                    Else
                        lblPaxRate.Attributes.Add("style", "display:none")
                        lblwlPaxRate.Attributes.Add("style", "display:block")

                        lblSpecialEventValue.Attributes.Add("style", "display:none")
                        lblwlSpecialEventValue.Attributes.Add("style", "display:block")
                    End If
                Else
                    lblPaxRate.Attributes.Add("style", "display:block")
                    lblwlPaxRate.Attributes.Add("style", "display:none")

                    lblSpecialEventValue.Attributes.Add("style", "display:block")
                    lblwlSpecialEventValue.Attributes.Add("style", "display:none")
                End If


            End If


            If hdBookingEngineRateType.Value = "1" Then
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: gvSpecialEvents_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnSpclEventSave_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSpclEventSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSpclEventSave.Click
        Try
            For Each dlItem As DataListItem In dlSpecialEvents.Items
                Dim lblCompulsory As Label = CType(dlItem.FindControl("lblCompulsory"), Label)
                Dim lblEventDate As Label = CType(dlItem.FindControl("lblEventDate"), Label)
                Dim ddlEvents As DropDownList = CType(dlItem.FindControl("ddlEvents"), DropDownList)
                Dim gvSpecialEvents As GridView = CType(dlItem.FindControl("gvSpecialEvents"), GridView)
                Dim chkSpclComplimentaryToCustomer As CheckBox = CType(dlItem.FindControl("chkSpclComplimentaryToCustomer"), CheckBox)
                Dim chkSpclComplimentaryFromSupplier As CheckBox = CType(dlItem.FindControl("chkSpclComplimentaryFromSupplier"), CheckBox)

                If lblCompulsory.Text = "1" Then ' Any one Compalsory
                    If ddlEvents.SelectedValue = "0" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Select Any Compuslory event for " & lblEventDate.Text)
                        Exit Sub
                    End If
                ElseIf lblCompulsory.Text = "0" Then 'Compalsory
                    If ddlEvents.SelectedValue = "0" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Select Any Compuslory event for " & lblEventDate.Text)
                        Exit Sub
                    End If
                End If




                If ddlEvents.SelectedValue <> "0" Then
                    For Each gvRow As GridViewRow In gvSpecialEvents.Rows
                        Dim hdSplistcode As HiddenField = CType(gvRow.FindControl("hdSplistcode"), HiddenField)
                        Dim hdSLineNo As HiddenField = CType(gvRow.FindControl("hdSLineNo"), HiddenField)
                        Dim hdspleventcode As HiddenField = CType(gvRow.FindControl("hdspleventcode"), HiddenField)
                        Dim hdspleventdate As HiddenField = CType(gvRow.FindControl("hdspleventdate"), HiddenField)
                        Dim lblPaxtype As Label = CType(gvRow.FindControl("lblPaxtype"), Label)
                        Dim lblRoomNo As Label = CType(gvRow.FindControl("lblRoomNo"), Label)

                        Dim txtPaxRate As TextBox = CType(gvRow.FindControl("txtPaxRate"), TextBox)
                        Dim txtPaxCost As TextBox = CType(gvRow.FindControl("txtPaxCost"), TextBox)
                        Dim lblSpecialEventValue As Label = CType(gvRow.FindControl("lblSpecialEventValue"), Label)
                        Dim lblSpecialEventCostValue As Label = CType(gvRow.FindControl("lblSpecialEventCostValue"), Label)
                        Dim lblNoOfPax As Label = CType(gvRow.FindControl("lblNoOfPax"), Label)
                        Dim lblChildAges As Label = CType(gvRow.FindControl("lblChildAges"), Label)

                        Dim lblwlPaxRate As Label = CType(gvRow.FindControl("lblwlPaxRate"), Label)
                        Dim lblwlSpecialEventValue As Label = CType(gvRow.FindControl("lblwlSpecialEventValue"), Label)
                        Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                        Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                        Dim lblwlcurrcode As Label = CType(gvRow.FindControl("lblwlcurrcode"), Label)
                        lblwlPaxRate.Text = Math.Round(Val(lblwlPaxRate.Text))
                        Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)

                        Dim dss As DataSet
                        dss = Session("sdsSpecialEvents")

                        Dim dtSpclEvent As New DataTable
                        dtSpclEvent = (CType(Session("sdsSpecialEvents"), DataSet)).Tables(1)

                        Dim dtSelectedSpclEvent As New DataTable
                        If Not Session("sdtSelectedSpclEvent") Is Nothing Then
                            dtSelectedSpclEvent = Session("sdtSelectedSpclEvent")
                        Else
                            dtSelectedSpclEvent = dtSpclEvent.Clone()
                            dtSelectedSpclEvent.Columns.Add("PartyCode", GetType(String))
                            dtSelectedSpclEvent.Columns.Add("RoomTypeCode", GetType(String))
                            dtSelectedSpclEvent.Columns.Add("MealPlanCode", GetType(String))
                            dtSelectedSpclEvent.Columns.Add("CatCode", GetType(String))
                            dtSelectedSpclEvent.Columns.Add("AccCode", GetType(String))
                            dtSelectedSpclEvent.Columns.Add("RatePlanId", GetType(String))
                            'dtSelectedSpclEvent.Columns.Add("comp_cust", GetType(String))
                            'dtSelectedSpclEvent.Columns.Add("comp_supp", GetType(String))
                        End If


                        If dss.Tables.Count > 0 Then


                            Dim dr As DataRow = dss.Tables(1).Select("splistcode='" & hdSplistcode.Value & "' AND splineno='" & hdSLineNo.Value & "'  AND spleventcode='" & hdspleventcode.Value & "'  AND spleventdate='" & hdspleventdate.Value & "' AND paxtype='" & lblPaxtype.Text & "' AND childage='" & lblChildAges.Text & "'  AND roomno='" & lblRoomNo.Text & "' ").FirstOrDefault

                            If Not dr Is Nothing Then

                                Dim drNew As DataRow = dtSelectedSpclEvent.NewRow()
                                If dtSelectedSpclEvent.Rows.Count > 0 Then

                                    Dim foundRow As DataRow
                                    foundRow = dtSelectedSpclEvent.Select("PartyCode='" & hdSPEPartyCode.Value.Trim & "' AND RoomTypeCode ='" & hdSPERoomTypeCode.Value.Trim & "' AND MealPlanCode='" & hdSPEMealPlanCode.Value.Trim & "' AND  CatCode='" & hdSPEcatCode.Value.Trim & "' AND AccCode='" & hdSPEAccCode.Value.Trim & "' AND RatePlanId='" & hdSPERatePlanId.Value.Trim & "' AND splistcode='" & hdSplistcode.Value & "' AND splineno='" & hdSLineNo.Value & "'  AND spleventcode='" & hdspleventcode.Value & "'  AND spleventdate='" & hdspleventdate.Value & "'  AND paxtype='" & lblPaxtype.Text & "'  AND childage='" & lblChildAges.Text & "' AND roomno='" & lblRoomNo.Text & "' ").FirstOrDefault

                                    If Not foundRow Is Nothing Then
                                        foundRow("paxrate") = txtPaxRate.Text.Trim
                                        foundRow("paxcost") = txtPaxCost.Text.Trim
                                        foundRow("wlpaxrate") = Math.Round(Val(txtPaxRate.Text.Trim * dWlMarkup))
                                        If txtPaxRate.Text = "" Then
                                            txtPaxRate.Text = "0"
                                        End If
                                        If txtPaxCost.Text = "" Then
                                            txtPaxCost.Text = "0"
                                        End If
                                        foundRow("spleventvalue") = CType(txtPaxRate.Text.Trim, Decimal) * CType(lblNoOfPax.Text, Decimal)
                                        foundRow("spleventcostvalue") = CType(txtPaxCost.Text.Trim, Decimal) * CType(lblNoOfPax.Text, Decimal)
                                        foundRow("wlspleventvalue") = Math.Round(Math.Round(Val(txtPaxRate.Text.Trim * dWlMarkup)) * CType(lblNoOfPax.Text, Decimal))

                                        'drNew.ItemArray = foundRow.ItemArray

                                        'drNew("PartyCode") = hdSPEPartyCode.Value.Trim
                                        'drNew("RoomTypeCode") = hdSPERoomTypeCode.Value.Trim
                                        'drNew("MealPlanCode") = hdSPEMealPlanCode.Value.Trim
                                        'drNew("CatCode") = hdSPEcatCode.Value.Trim
                                        'drNew("AccCode") = hdSPEAccCode.Value.Trim
                                        'drNew("RatePlanId") = hdSPERatePlanId.Value.Trim

                                    Else
                                        dr("paxrate") = txtPaxRate.Text.Trim
                                        dr("paxcost") = txtPaxCost.Text.Trim
                                        dr("wlpaxrate") = Math.Round(Val(txtPaxRate.Text.Trim * dWlMarkup))
                                        If txtPaxRate.Text = "" Then
                                            txtPaxRate.Text = "0"
                                        End If
                                        If txtPaxCost.Text = "" Then
                                            txtPaxCost.Text = "0"
                                        End If
                                        dr("spleventvalue") = CType(txtPaxRate.Text.Trim, Decimal) * CType(lblNoOfPax.Text, Decimal)
                                        dr("spleventcostvalue") = CType(txtPaxCost.Text.Trim, Decimal) * CType(lblNoOfPax.Text, Decimal)
                                        dr("wlspleventvalue") = Math.Round(Math.Round(Val(txtPaxRate.Text.Trim * dWlMarkup)) * CType(lblNoOfPax.Text, Decimal))
                                        drNew.ItemArray = dr.ItemArray

                                        drNew("PartyCode") = hdSPEPartyCode.Value.Trim
                                        drNew("RoomTypeCode") = hdSPERoomTypeCode.Value.Trim
                                        drNew("MealPlanCode") = hdSPEMealPlanCode.Value.Trim
                                        drNew("CatCode") = hdSPEcatCode.Value.Trim
                                        drNew("AccCode") = hdSPEAccCode.Value.Trim
                                        drNew("RatePlanId") = hdSPERatePlanId.Value.Trim
                                        If chkSpclComplimentaryToCustomer.Checked = True Then
                                            drNew("comp_cust") = "1"
                                        Else
                                            drNew("comp_cust") = "0"
                                        End If
                                        If chkSpclComplimentaryFromSupplier.Checked = True Then
                                            drNew("comp_supp") = "1"
                                        Else
                                            drNew("comp_supp") = "0"
                                        End If
                                        dtSelectedSpclEvent.Rows.Add(drNew)
                                    End If
                                Else
                                    dr("paxrate") = txtPaxRate.Text.Trim
                                    dr("paxcost") = txtPaxCost.Text.Trim
                                    dr("wlpaxrate") = Math.Round(Val(txtPaxRate.Text.Trim * dWlMarkup))
                                    If txtPaxRate.Text = "" Then
                                        txtPaxRate.Text = "0"
                                    End If
                                    If txtPaxCost.Text = "" Then
                                        txtPaxCost.Text = "0"
                                    End If
                                    dr("spleventvalue") = CType(txtPaxRate.Text.Trim, Decimal) * CType(lblNoOfPax.Text, Decimal)
                                    dr("spleventcostvalue") = CType(txtPaxCost.Text.Trim, Decimal) * CType(lblNoOfPax.Text, Decimal)
                                    dr("wlspleventvalue") = Math.Round(Math.Round(Val(txtPaxRate.Text.Trim * dWlMarkup)) * CType(lblNoOfPax.Text, Decimal))

                                    drNew.ItemArray = dr.ItemArray

                                    drNew("PartyCode") = hdSPEPartyCode.Value.Trim
                                    drNew("RoomTypeCode") = hdSPERoomTypeCode.Value.Trim
                                    drNew("MealPlanCode") = hdSPEMealPlanCode.Value.Trim
                                    drNew("CatCode") = hdSPEcatCode.Value.Trim
                                    drNew("AccCode") = hdSPEAccCode.Value.Trim
                                    drNew("RatePlanId") = hdSPERatePlanId.Value.Trim
                                    dtSelectedSpclEvent.Rows.Add(drNew)
                                End If



                            End If
                        End If

                        Session("sdtSelectedSpclEvent") = dtSelectedSpclEvent
                    Next

                End If

            Next
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnSpclEventSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' GetSpecialEventDetails
    ''' </summary>
    ''' <param name="lbSpecialEvents"></param>
    ''' <param name="gvGridviewRow"></param>
    ''' <param name="strSelectedEvents"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSpecialEventDetails(ByVal lbSpecialEvents As LinkButton, ByVal gvGridviewRow As GridViewRow, ByVal strSelectedEvents As String) As DataSet

        Dim lblRMPartyCode As Label = CType(gvGridviewRow.FindControl("lblRMPartyCode"), Label)
        Session("slbSpecialEvents") = lbSpecialEvents
        Dim lblRMRoomTypeCode As Label = CType(gvGridviewRow.FindControl("lblRMRoomTypeCode"), Label)
        Dim lblRMMealPlanCode As Label = CType(gvGridviewRow.FindControl("lblRMMealPlanCode"), Label)
        Dim lblRMcatCode As Label = CType(gvGridviewRow.FindControl("lblRMcatCode"), Label)
        Dim lblRMSharingOrExtraBed As Label = CType(gvGridviewRow.FindControl("lblRMSharingOrExtraBed"), Label)
        Dim lblRMRatePlanId As Label = CType(gvGridviewRow.FindControl("lblRMRatePlanId"), Label)
        Dim lblHotelRoomString As Label = CType(gvGridviewRow.FindControl("lblHotelRoomString"), Label)
        Dim lblCurrentSeletion As Label = CType(gvGridviewRow.FindControl("lblCurrentSeletion"), Label)
        hdSPEPartyCode.Value = lblRMPartyCode.Text
        hdSPERoomTypeCode.Value = lblRMRoomTypeCode.Text
        hdSPEMealPlanCode.Value = lblRMMealPlanCode.Text
        hdSPEcatCode.Value = lblRMcatCode.Text
        hdSPEAccCode.Value = lblRMSharingOrExtraBed.Text
        hdSPERatePlanId.Value = lblRMRatePlanId.Text
        hdSPHotelRoomString.Value = lblHotelRoomString.Text

        Dim strOverride As String = ""
        If hdOveride.Value = "1" Then
            strOverride = "1"
        Else
            strOverride = "0"
        End If

        Dim objBLLHotelSearch_ As BLLHotelSearch = New BLLHotelSearch
        objBLLHotelSearch_ = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
        Dim dsSpecialEvents As New DataSet
        If Not objBLLHotelSearch_ Is Nothing Then
            Dim dt As DataTable = objBLLHotelSearch_.GetAdultAndChildFromRoomString(objBLLHotelSearch_.RoomString)
            If dt.Rows.Count > 0 Then
                objBLLHotelSearch_.Adult = dt.Rows(0)("adults").ToString
                objBLLHotelSearch_.Children = dt.Rows(0)("child").ToString
                objBLLHotelSearch_.ChildAgeString = dt.Rows(0)("childages").ToString
            End If
            Dim strEditRequestId As String = ""
            Dim strEdirRLineNo As String = ""
            If lblCurrentSeletion.Text = "1" Then
                'If Not Session("sEditRequestId") Is Nothing Then
                '    strEditRequestId = Session("sEditRequestId")
                '    strEdirRLineNo = hdEditRLineNo.Value
                'Else

                If hdOPMode.Value = "Edit" Then
                    strEditRequestId = Session("sRequestId")
                    strEdirRLineNo = hdEditRLineNo.Value
                Else
                    strEditRequestId = ""
                    strEdirRLineNo = "0"
                End If
                ' End If
            Else
                strEditRequestId = ""
                strEdirRLineNo = "0"
            End If


            dsSpecialEvents = objBLLHotelSearch_.GetSpecialEventsDetails(lblRMPartyCode.Text, lblRMRoomTypeCode.Text, lblRMMealPlanCode.Text, lblRMcatCode.Text, "", lblRMRatePlanId.Text, objBLLHotelSearch_.AgentCode, objBLLHotelSearch_.SourceCountryCode, objBLLHotelSearch_.CheckIn, objBLLHotelSearch_.CheckOut, objBLLHotelSearch_.Room, objBLLHotelSearch_.Adult, objBLLHotelSearch_.Children, objBLLHotelSearch_.ChildAgeString, strSelectedEvents, lblHotelRoomString.Text, strOverride, strEditRequestId, strEdirRLineNo)
        End If
        Return dsSpecialEvents
    End Function
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
            objclsUtilities.WriteErrorLog("HotelSearch.aspx ::btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
    ''' btnPriceBreakupFillPrice_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnPriceBreakupFillPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If txtBreakupTotalPriceForAll.Text <> "" Or txtsalepriceForAll.Text <> "" Then
                For Each dlitem As DataListItem In dltotalPriceBreak.Items
                    Dim lblSaleTotal As Label = CType(dlitem.FindControl("lblSaleTotal"), Label)
                    Dim lblCostTotal As Label = CType(dlitem.FindControl("lblCostTotal"), Label)

                    Dim strRoomNo As String = ""
                    strRoomNo = ddlRoomNos.SelectedValue
                    If strRoomNo = dlitem.ItemIndex + 1 Then
                        Dim gvPricebreakup As GridView = CType(dlitem.FindControl("gvPricebreakup"), GridView)

                        Dim fRoomSalePrice As Double = 0
                        Dim fRoomCostPrice As Double = 0
                        For Each row As GridViewRow In gvPricebreakup.Rows
                            Dim txtsaleprice As TextBox = CType(row.FindControl("txtsaleprice"), TextBox)
                            Dim txtBreakupTotalPrice As TextBox = CType(row.FindControl("txtBreakupTotalPrice"), TextBox)
                            Dim lblConversionRate As Label = CType(row.FindControl("lblConversionRate"), Label)
                            Dim lblUSDPrice As Label = CType(row.FindControl("lblUSDPrice"), Label)
                            Dim lblSalePriceCurrcode As Label = CType(row.FindControl("lblSalePriceCurrcode"), Label)
                            Dim lblwlconvrate As Label = CType(row.FindControl("lblwlconvrate"), Label)
                            Dim lblwlmarkupperc As Label = CType(row.FindControl("lblwlmarkupperc"), Label)
                            Dim lblwlcurrcode As Label = CType(row.FindControl("lblwlcurrcode"), Label)
                            Dim lblwlbreakupPrice As Label = CType(row.FindControl("lblwlbreakupPrice"), Label)
                            Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)

                            If chkFillBlank.Checked = True Then
                                If txtBreakupTotalPriceForAll.Text <> "" Then
                                    If txtBreakupTotalPrice.Text = "" Then
                                        txtBreakupTotalPrice.Text = txtBreakupTotalPriceForAll.Text

                                    End If
                                End If
                                If txtsalepriceForAll.Text <> "" Then
                                    If txtsaleprice.Text = "" Then
                                        txtsaleprice.Text = txtsalepriceForAll.Text
                                        lblwlbreakupPrice.Text = Math.Round(Val(txtsaleprice.Text) * dWlMarkup)
                                    End If
                                End If

                            Else
                                If txtBreakupTotalPriceForAll.Text <> "" Then

                                    txtBreakupTotalPrice.Text = txtBreakupTotalPriceForAll.Text
                                End If
                                If txtsalepriceForAll.Text <> "" Then
                                    txtsaleprice.Text = txtsalepriceForAll.Text
                                    lblwlbreakupPrice.Text = Math.Round(Val(txtsaleprice.Text) * dWlMarkup)
                                End If
                            End If


                            If txtsaleprice.Text <> "" Then
                                fRoomSalePrice = fRoomSalePrice + txtsaleprice.Text
                            End If
                            If txtBreakupTotalPrice.Text <> "" Then
                                fRoomCostPrice = fRoomCostPrice + txtBreakupTotalPrice.Text
                                lblUSDPrice.Text = Math.Round(CType(txtBreakupTotalPrice.Text, Double) * CType(lblConversionRate.Text, Double), 2).ToString & " " & lblSalePriceCurrcode.Text & ")" ' modified by abin on 20180711
                            End If


                            '***
                        Next
                        lblSaleTotal.Text = Math.Round(fRoomSalePrice, 2).ToString
                        lblCostTotal.Text = Math.Round(fRoomCostPrice, 2).ToString

                    End If
                Next
            End If
            mpTotalprice.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPriceBreakupFillPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try



    End Sub
    ''' <summary>
    ''' FillddlRoomNos
    ''' </summary>
    ''' <param name="iRoomNo"></param>
    ''' <remarks></remarks>
    Private Sub FillddlRoomNos(ByVal iRoomNo As Integer)
        If ddlRoomNos.Items.Count > 0 Then
            ddlRoomNos.Items.Clear()
        End If
        For i As Integer = 1 To iRoomNo
            ddlRoomNos.Items.Add(New ListItem("Room " & i.ToString, i.ToString))
        Next
    End Sub
    ''' <summary>
    ''' GetRoomString
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' '*** Danny 26/08/2018
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
                        strRoomString = strRoomString + "|" + (Convert.ToInt32(ddlDynRoomAdult1.Text)).ToString
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
    ' ''' <summary>
    ' ''' GetRoomString
    ' ''' </summary>
    ' ''' <param name="strRoom"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoomString(ByVal strRoom As String) As String
    '    Dim strRoomString As String = ""

    '    If strRoom <> "" Then
    '        'strRoomString = strRoom

    '        If strRoom = "1" Then
    '            strRoomString = GetRoom1String(strRoomString)

    '        ElseIf strRoom = "2" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '        ElseIf strRoom = "3" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '        ElseIf strRoom = "4" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '        ElseIf strRoom = "5" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '        ElseIf strRoom = "6" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '            strRoomString = GetRoom6String(strRoomString)
    '        End If
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' GetRoom1String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' '*** Danny 26/08/2018
    'Private Function GetRoom1String(ByVal strRoomString As String) As String
    '    strRoomString = "1," & ddlRoom1Adult.SelectedValue
    '    Dim strRoom1Child = ddlRoom1Child.SelectedValue
    '    If strRoom1Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom1Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text)
    '    ElseIf strRoom1Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text)
    '    ElseIf strRoom1Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text)
    '    ElseIf strRoom1Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text)
    '    ElseIf strRoom1Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text) & "|" & Val(txtRoom1Child5.Text)
    '    ElseIf strRoom1Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text) & "|" & Val(txtRoom1Child5.Text) & "|" & Val(txtRoom1Child6.Text)
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' GetRoom2String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' '*** Danny 26/08/2018
    'Private Function GetRoom2String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";2," & ddlRoom2Adult.SelectedValue
    '    Dim strRoom2Child = ddlRoom2Child.SelectedValue
    '    If strRoom2Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom2Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text)
    '    ElseIf strRoom2Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text)
    '    ElseIf strRoom2Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text)
    '    ElseIf strRoom2Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text) & "|" & Val(txtRoom2Child4.Text)
    '    ElseIf strRoom2Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text) & "|" & Val(txtRoom2Child4.Text) & "|" & Val(txtRoom2Child5.Text)
    '    ElseIf strRoom2Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text) & "|" & Val(txtRoom2Child4.Text) & "|" & Val(txtRoom2Child5.Text) & "|" & Val(txtRoom2Child6.Text)
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' GetRoom3String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function GetRoom3String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";3," & ddlRoom3Adult.SelectedValue
    '    Dim strRoom3Child = ddlRoom3Child.SelectedValue
    '    If strRoom3Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom3Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text)
    '    ElseIf strRoom3Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text)
    '    ElseIf strRoom3Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text)
    '    ElseIf strRoom3Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text) & "|" & Val(txtRoom3Child4.Text)
    '    ElseIf strRoom3Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text) & "|" & Val(txtRoom3Child4.Text) & "|" & Val(txtRoom3Child5.Text)
    '    ElseIf strRoom3Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text) & "|" & Val(txtRoom3Child4.Text) & "|" & Val(txtRoom3Child5.Text) & "|" & Val(txtRoom3Child6.Text)
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' GetRoom4String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function GetRoom4String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";4," & ddlRoom4Adult.SelectedValue
    '    Dim strRoom4Child = ddlRoom4Child.SelectedValue
    '    If strRoom4Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom4Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text)
    '    ElseIf strRoom4Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text)
    '    ElseIf strRoom4Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text)
    '    ElseIf strRoom4Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text) & "|" & Val(txtRoom4Child4.Text)
    '    ElseIf strRoom4Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text) & "|" & Val(txtRoom4Child4.Text) & "|" & Val(txtRoom4Child5.Text)
    '    ElseIf strRoom4Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text) & "|" & Val(txtRoom4Child4.Text) & "|" & Val(txtRoom4Child5.Text) & "|" & Val(txtRoom4Child6.Text)
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' GetRoom5String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function GetRoom5String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";5," & ddlRoom5Adult.SelectedValue
    '    Dim strRoom5Child = ddlRoom5Child.SelectedValue
    '    If strRoom5Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom5Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text)
    '    ElseIf strRoom5Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text)
    '    ElseIf strRoom5Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text)
    '    ElseIf strRoom5Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text) & "|" & Val(txtRoom5Child4.Text)
    '    ElseIf strRoom5Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text) & "|" & Val(txtRoom5Child4.Text) & "|" & Val(txtRoom5Child5.Text)
    '    ElseIf strRoom5Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text) & "|" & Val(txtRoom5Child4.Text) & "|" & Val(txtRoom5Child5.Text) & "|" & Val(txtRoom5Child6.Text)
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' GetRoom6String
    ''' </summary>
    ''' <param name="strRoomString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function GetRoom6String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";6," & ddlRoom6Adult.SelectedValue
    '    Dim strRoom6Child = ddlRoom6Child.SelectedValue
    '    If strRoom6Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom6Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text)
    '    ElseIf strRoom6Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text)
    '    ElseIf strRoom6Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text)
    '    ElseIf strRoom6Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text) & "|" & Val(txtRoom6Child4.Text)
    '    ElseIf strRoom6Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text) & "|" & Val(txtRoom6Child4.Text) & "|" & Val(txtRoom6Child5.Text)
    '    ElseIf strRoom6Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text) & "|" & Val(txtRoom6Child4.Text) & "|" & Val(txtRoom6Child5.Text) & "|" & Val(txtRoom6Child6.Text)
    '    End If
    '    Return strRoomString
    'End Function
    ''' <summary>
    ''' lbHeaderTotalValue_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbHeaderTotalValue_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType((myLinkButton).NamingContainer, GridViewRow)
            Dim gvHotelRoomType As GridView = CType((gvRow).NamingContainer, GridView)
            Dim dlListItem As DataListItem = CType((gvHotelRoomType).NamingContainer, DataListItem)
            Dim hdRatePlanCode As HiddenField = CType(dlListItem.FindControl("hdRatePlanCode"), HiddenField)
            Dim hdRatePlanHotelCode As HiddenField = CType(dlListItem.FindControl("hdRatePlanHotelCode"), HiddenField)
            Dim hdMealPlanOrder As HiddenField = CType(dlListItem.FindControl("hdMealPlanOrder"), HiddenField)
            Dim hdPriceOrder As HiddenField = CType(dlListItem.FindControl("hdPriceOrder"), HiddenField)

            If hdPriceOrder.Value = "" Or hdPriceOrder.Value = "0" Then
                hdPriceOrder.Value = "1"
            Else
                hdPriceOrder.Value = "0"
            End If
            hdMealPlanOrder.Value = "0"

            'changed by mohamed on 10/09/2018 included rateplanname in filter
            Dim hdRatePlan As HiddenField = CType(dlListItem.FindControl("hdRatePlan"), HiddenField)
            BindgvRoomType(gvHotelRoomType, hdRatePlanCode.Value, hdRatePlanHotelCode.Value, hdMealPlanOrder.Value, hdPriceOrder.Value, hdRatePlan.Value)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: lbHeaderTotalValue_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' lbHeaderMealPlan_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbHeaderMealPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim gvRow As GridViewRow = CType((myLinkButton).NamingContainer, GridViewRow)
            Dim gvHotelRoomType As GridView = CType((gvRow).NamingContainer, GridView)
            Dim dlListItem As DataListItem = CType((gvHotelRoomType).NamingContainer, DataListItem)
            Dim hdRatePlanCode As HiddenField = CType(dlListItem.FindControl("hdRatePlanCode"), HiddenField)
            Dim hdRatePlanHotelCode As HiddenField = CType(dlListItem.FindControl("hdRatePlanHotelCode"), HiddenField)
            Dim hdMealPlanOrder As HiddenField = CType(dlListItem.FindControl("hdMealPlanOrder"), HiddenField)
            Dim hdPriceOrder As HiddenField = CType(dlListItem.FindControl("hdPriceOrder"), HiddenField)
            If hdMealPlanOrder.Value = "" Or hdMealPlanOrder.Value = "0" Then
                hdMealPlanOrder.Value = "1"
            Else
                hdMealPlanOrder.Value = "0"
            End If
            hdPriceOrder.Value = "0"

            'changed by mohamed on 10/09/2018 included rateplanname in filter
            Dim hdRatePlan As HiddenField = CType(dlListItem.FindControl("hdRatePlan"), HiddenField)
            BindgvRoomType(gvHotelRoomType, hdRatePlanCode.Value, hdRatePlanHotelCode.Value, hdMealPlanOrder.Value, hdPriceOrder.Value, hdRatePlan.Value)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx ::  lbHeaderMealPlan_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

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
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' FillShiftingRoomAdultChild
    ''' </summary>
    ''' <param name="RoomString"></param>
    ''' <remarks></remarks>
    Private Sub FillShiftingRoomAdultChild(ByVal RoomString As String)
        'Dim strroomstring As String() = RoomString.Split(";")
        'Dim strroom As String()
        'Dim strchildage As String()
        'If strroomstring.Length > 0 Then
        '    Dim strRoomAdultName As String = ""
        '    Dim strRoomchildName As String = ""
        '    Dim strRoomchildage As String = ""

        '    For i = 0 To strroomstring.Length - 1
        '        strroom = strroomstring(i).Split(",")
        '        If strroom(1) <> "0" Then
        '            strRoomAdultName = "ddlRoom" & strroom(0) & "Adult"
        '            strRoomchildName = "ddlRoom" & strroom(0) & "Child"

        '            Dim ddlRoomm As DropDownList = DirectCast(FindControl(strRoomAdultName), DropDownList)
        '            Dim ddlRoomc As DropDownList = DirectCast(FindControl(strRoomchildName), DropDownList)


        '            ddlRoomm.SelectedValue = strroom(1)
        '            ddlRoomc.SelectedValue = strroom(2)


        '        End If
        '        If strroom(2).ToString <> "0" Then

        '            strchildage = strroom(3).Split("|")

        '            For j = 0 To strchildage.Length - 1

        '                strRoomchildage = "txtRoom" & strroom(0) & "Child" & j + 1
        '                Dim txtRoomchild As TextBox = DirectCast(FindControl(strRoomchildage), TextBox)


        '                txtRoomchild.Text = strchildage(j)
        '            Next

        '        End If
        '    Next
        'End If

        '*** Danny 26/08/2018
        Dim dtAdultChilds = New DataTable
        'dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)

        dtAdultChilds.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildAges", GetType(String)))

        '*** Danny 04/09/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'txtNoOfNights.Text = objBLLHotelSearch.NoOfNights 
        If Val(objBLLHotelSearch.Room) > 0 Then
            ddlRoom_Dynamic.SelectedValue = objBLLHotelSearch.Room
        End If
        '*** Danny 04/09/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'ddlRoom_Dynamic.SelectedValue = IIf(Val(objBLLHotelSearch.Room.Trim) = 0, 1, objBLLHotelSearch.Room) 'changed by mohamed on 29/08/2018
        DynamicRoomCreate()

        Dim strroomstring As String() = RoomString.Split(";")
        Dim strroom As String()
        Dim strchildage As String()
        If strroomstring.Length > 0 Then
            Dim a As Integer = 0
            Dim b As Integer = 1
            For Each dlRoomtem1 As DataListItem In dlNofoRooms.Items
                Dim row As DataRow = dtAdultChilds.NewRow()
                strroom = strroomstring(a).Split(",")
                Dim ddlDynRoomAdult As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomChild"), DropDownList)
                Dim ddlDynChildAges As DataList = CType(dlRoomtem1.FindControl("dlChildAges"), DataList)

                ddlDynRoomAdult.SelectedIndex = strroom(1)
                ddlDynRoomChild.SelectedIndex = strroom(2)

                row("colAdultLbl") = (b).ToString
                row("colChildLbl") = (b).ToString
                row("colAdultSelectNo") = strroom(1)
                row("colChildSelectNo") = strroom(2)
                row("colChildAges") = strroom(3)
                dtAdultChilds.Rows.Add(row)

                If strroom(2).ToString <> "0" Then
                    Dim dtChildAge = New DataTable
                    dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
                    dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
                    dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
                    strchildage = strroom(3).Split("|")
                    For j = 0 To strchildage.Length - 1
                        Dim row2 As DataRow = dtChildAge.NewRow()
                        If j = 0 Then
                            row2("colChildAgeLbl") = "Room " + strroom(0).ToString + " Child Age"
                        Else
                            row2("colChildAgeLbl") = ""
                        End If
                        row2("colChildAge") = strchildage(j)
                        row2("colCHNo") = "CH" + (j + 1).ToString
                        dtChildAge.Rows.Add(row2)
                    Next
                    ddlDynChildAges.DataSource = dtChildAge
                    ddlDynChildAges.DataBind()
                End If

                a = a + 1
            Next
            Session("dtAdultChilds") = dtAdultChilds
        End If

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
    ''' fnHotelSearch
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fnHotelSearch()

        Try
            imgHotelthreadLoading.Visible = True
            dlHotelsSearchResults.DataBind()
            rptPager.DataBind()

            Session("sdsPriceBreakupTemp") = Nothing
            Session("sdsPriceBreakup") = Nothing

            Dim strSearchCriteria As String = ""


            Dim objBLLHotelSearch As New BLLHotelSearch
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
            Dim strAdult As String = ddlAdult.SelectedValue

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


            '   Dim strMealplan As String = ddlMealPlan.SelectedValue   ''** Shahul 26/06/2018

            Dim strMealplan As String = hdmealcode.Value   ''** Shahul 08/07/2018
            strMealplan = IIf(strMealplan <> "", Replace(strMealplan, ",", ";"), "")

            If HttpContext.Current.Session("sLoginType") = "RO" Then

                If strHotelCode = "" And chkOveridePrice.Checked = True Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any hotel name. \n </br></br></br></br></br></br></br></br></br>* If override is ticked then Hotel selection is compulsory.")
                    imgHotelthreadLoading.Visible = False
                    Exit Sub
                End If
            End If
            If strCheckOut = strCheckIn Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Same check-in and check-out date is not allowed.")
                imgHotelthreadLoading.Visible = False
                Exit Sub
            End If
            Dim strQueryString As String = ""
            If strDestination <> "" Then
                objBLLHotelSearch.Destination = strDestination
                strSearchCriteria = "Destination: " & strDestination
            End If
            If strDestinationCode <> "" Then
                objBLLHotelSearch.DestinationCode = strDestinationCode
            End If
            If DestinationCodeAndType <> "" Then
                objBLLHotelSearch.DestinationCodeAndType = DestinationCodeAndType
            End If
            If strDestType <> "" Then
                objBLLHotelSearch.DestinationType = strDestType
                strSearchCriteria = strSearchCriteria & "||" & "DestinationType: " & strDestination
            End If
            If strCheckIn <> "" Then
                objBLLHotelSearch.CheckIn = strCheckIn
                strSearchCriteria = strSearchCriteria & "||" & "CheckIn: " & strCheckIn
            End If
            If strCheckOut <> "" Then
                objBLLHotelSearch.CheckOut = strCheckOut
                strSearchCriteria = strSearchCriteria & "||" & "CheckOut: " & strCheckOut
            End If
            If strNoOfNights <> "" Then
                objBLLHotelSearch.NoOfNights = strNoOfNights
                strSearchCriteria = strSearchCriteria & "||" & "NoOfNights: " & strNoOfNights
            End If
            If strRoom <> "" Then
                objBLLHotelSearch.Room = strRoom
                strSearchCriteria = strSearchCriteria & "||" & "Room: " & strRoom
            End If



            If strSourceCountry <> "" Then
                objBLLHotelSearch.SourceCountry = strSourceCountry
                strSearchCriteria = strSearchCriteria & "||" & "SourceCountry: " & strSourceCountry
            End If
            If strSourceCountryCode <> "" Then
                objBLLHotelSearch.SourceCountryCode = strSourceCountryCode
            End If
            If strOrderBy <> "" Then
                objBLLHotelSearch.OrderBy = strOrderBy
            End If
            If strCustomer <> "" Then
                objBLLHotelSearch.Customer = strCustomer
                strSearchCriteria = strSearchCriteria & "||" & "Agent: " & strCustomer
            End If
            If strCustomerCode <> "" Then
                objBLLHotelSearch.CustomerCode = strCustomerCode
            End If
            If strStarCategory <> "" Then
                objBLLHotelSearch.StarCategory = strStarCategory
                strSearchCriteria = strSearchCriteria & "||" & "StarCategory: " & strStarCategory
            End If
            If strStarCategoryCode <> "" Then
                objBLLHotelSearch.StarCategoryCode = strStarCategoryCode
            End If
            If strAvailabilty <> "" Then
                objBLLHotelSearch.Availabilty = strAvailabilty
                strSearchCriteria = strSearchCriteria & "||" & "Availabilty: " & strAvailabilty
            End If
            If strPropertyType <> "" And strPropertyType <> "--" Then
                objBLLHotelSearch.PropertyType = strPropertyType
                strSearchCriteria = strSearchCriteria & "||" & "PropertyType: " & strPropertyType
            End If
            If strHotels <> "" Then
                objBLLHotelSearch.Hotels = strHotels
                strSearchCriteria = strSearchCriteria & "||" & "Hotels: " & strHotels
            End If

            If strMealplan <> "" And strMealplan <> "--" Then ''** Shahul 26/06/2018
                objBLLHotelSearch.MealPlan = strMealplan
                strSearchCriteria = strSearchCriteria & "||" & "MealPlan: " & strMealplan
            End If
            If chkshowall.Checked = True Then
                objBLLHotelSearch.ShowallCategory = "1"
                strSearchCriteria = strSearchCriteria & "||" & "ShowallCategory:Yes "
            Else
                objBLLHotelSearch.ShowallCategory = "0"
                hdOveride.Value = "0"
                strSearchCriteria = strSearchCriteria & "||" & "ShowallCategory:No "
            End If


            If strHotelCode <> "" Then
                objBLLHotelSearch.HotelCode = strHotelCode
            End If
            objBLLHotelSearch.LoginType = HttpContext.Current.Session("sLoginType")
            strSearchCriteria = strSearchCriteria & "||" & "LoginType: " & objBLLHotelSearch.LoginType
            objBLLHotelSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLHotelSearch.CustomerCode, Session("sAgentCode"))
            strSearchCriteria = strSearchCriteria & "||" & "AgentCode: " & objBLLHotelSearch.AgentCode
            objBLLHotelSearch.WebuserName = Session("GlobalUserName")
            strSearchCriteria = strSearchCriteria & "||" & "WebuserName: " & objBLLHotelSearch.WebuserName
            If chkOveridePrice.Checked = True Then
                objBLLHotelSearch.OverridePrice = "1"
                hdOveride.Value = "1"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice:Yes "
            Else
                objBLLHotelSearch.OverridePrice = "0"
                hdOveride.Value = "0"
                strSearchCriteria = strSearchCriteria & "||" & "OverridePrice:No "
            End If
            '*** Danny 26/08/2018
            objBLLHotelSearch.RoomString = GetRoomString(strRoom)
            If objBLLHotelSearch.RoomString.Contains("Please") Then
                MessageBox.ShowMessage(Page, MessageType.Warning, objBLLHotelSearch.RoomString.ToString)
                Exit Sub
            End If
            strSearchCriteria = strSearchCriteria & "||" & "RoomString: " & objBLLHotelSearch.RoomString

            ' Modified by abin on 23/04/2018 --- Pax validation for Shifting Hotel -- Start
            If chkShifting.Checked = True Then
                Dim iShiftAdults As Integer = 0
                Dim iShiftChild As Integer = 0
                If txtShiftHotelCode.Text <> "" Then
                    For Each dlShiftHotelBreakItem As DataListItem In dlShiftHotelBreak.Items
                        Dim chkSelect As CheckBox = dlShiftHotelBreakItem.FindControl("chkSelect")
                        If chkSelect.Checked = True Then
                            Dim lblAdults As Label = dlShiftHotelBreakItem.FindControl("lblAdults")
                            Dim lblChild As Label = dlShiftHotelBreakItem.FindControl("lblChild")
                            iShiftAdults = iShiftAdults + Val(lblAdults.Text)
                            iShiftChild = iShiftChild + Val(lblChild.Text)
                        End If
                    Next
                    Dim dt As DataTable = objBLLHotelSearch.GetAdultAndChildSum(objBLLHotelSearch.RoomString)
                    If dt.Rows.Count > 0 Then
                        Dim iCurrentAdults As Integer = dt.Rows(0)("adult")
                        Dim iCurrentChild As Integer = dt.Rows(0)("child")

                        If iCurrentAdults > iShiftAdults And iCurrentChild > iShiftChild Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Booking adult(" & iShiftAdults.ToString & ") and child(" & iShiftChild.ToString & ") pax not tally with selected adult and child.  selected adult and child should be less than or equal to booking adult and child.")
                            imgHotelthreadLoading.Visible = False
                            Exit Sub
                        ElseIf iCurrentAdults > iShiftAdults Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Booking adult pax(" & iShiftAdults.ToString & ") not tally with selected adult.  selected adult should be less than or equal to booking adult.")
                            imgHotelthreadLoading.Visible = False
                            Exit Sub
                        ElseIf iCurrentChild > iShiftChild Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Booking child(" & iShiftChild.ToString & ") pax not tally with selected child.  selected child should be less than or equal to booking child.")
                            imgHotelthreadLoading.Visible = False
                            Exit Sub
                        End If
                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select shifting hotel.")
                    imgHotelthreadLoading.Visible = False
                    Exit Sub
                End If
            End If


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

            ' Modified by abin on 23/04/2018 --- Pax validation for Shifting Hotel -- End
            Session("sobjBLLHotelSearch") = objBLLHotelSearch

            Dim dsSearchResults As New DataSet
            Dim strBlank As String = ""

            Session("sMailBoxPageIndex") = Nothing



            'If Not Session("sEditRequestId") Is Nothing Then
            '    objBLLHotelSearch.EditRequestId = Session("sEditRequestId")
            '    objBLLHotelSearch.EditRLineNo = hdEditRLineNo.Value
            '    objBLLHotelSearch.EditRatePlanId = hdEditRatePlanId.Value

            'Else

            If hdOPMode.Value = "Edit" Then
                objBLLHotelSearch.EditRequestId = Session("sRequestId")
                objBLLHotelSearch.EditRLineNo = hdEditRLineNo.Value
                objBLLHotelSearch.EditRatePlanId = hdEditRatePlanId.Value
            Else

                If Not Session("sRequestId") Is Nothing Then
                    objBLLHotelSearch.EditRequestId = Session("sRequestId")
                    objBLLHotelSearch.EditRLineNo = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "HOTEL")
                Else
                    objBLLHotelSearch.EditRequestId = ""
                    objBLLHotelSearch.EditRLineNo = ""
                End If

                objBLLHotelSearch.EditRatePlanId = ""
            End If


            '  End If


            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                '  Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Hotel Search Page", "Hotel Search", strSearchCriteria, Session("GlobalUserName"))
            End If

           


            '' Changed for apply threading technique. Modified on 26/08/2020.
       
            If objBLLHotelSearch.HotelCode = "" Then
           
                If strShowNotColumbusRate = "YES" And strShowColumbusRate = "YES" Then
                    hdProgress.Value = "1"
                    hdProgressTimer2.Value = "1"
                    ModalPopupDays.Show()
                    Session("sDSSearchResults") = Nothing
                    Session("sDSSearchResultsForPreferred") = Nothing
                    Session("sDSSearchResultsForNonPreferred") = Nothing
                    Session("sPreferred") = Nothing
                    Session("sNonPreferred") = Nothing


                    Session("sDSSearchResultsOneDMCStatic") = Nothing
                    Session("sDSSearchResultsOneDMCDynamic") = Nothing
                    Session("OneDMCDynamic") = Nothing
                    ' Timer1.Enabled = True
                    Timer2.Enabled = True

                    trd1 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
                    trd1.Name = "myThread1"
                    trd1.IsBackground = False
                    trd1.Start(objBLLHotelSearch)


                    trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)
                    trd2.Name = "myThread2"
                    trd2.IsBackground = False
                    trd2.Start(objBLLHotelSearch)


                    'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCStatic)
                    'trd2.Name = "myThreadOneDMCStatic"
                    'trd2.IsBackground = False
                    'trd2.Start(objBLLHotelSearch)

                    trd4 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCDynamic)
                    trd4.Name = "myThreadOneDMCDynamic"
                    trd4.IsBackground = False
                    trd4.Start(objBLLHotelSearch)
                ElseIf strShowNotColumbusRate = "YES" And strShowColumbusRate <> "YES" Then
                    hdProgress.Value = "1"
                    hdProgressTimer2.Value = "1"
                    ModalPopupDays.Show()
                    Session("sDSSearchResults") = Nothing
                    Session("sDSSearchResultsForPreferred") = Nothing
                    Session("sDSSearchResultsForNonPreferred") = Nothing
                    Session("sPreferred") = Nothing
                    Session("sNonPreferred") = Nothing


                    Session("sDSSearchResultsOneDMCStatic") = Nothing
                    Session("sDSSearchResultsOneDMCDynamic") = Nothing
                    Session("OneDMCDynamic") = Nothing
                    ' Timer1.Enabled = True
                    Timer2.Enabled = True

                    'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCStatic)
                    'trd2.Name = "myThreadOneDMCStatic"
                    'trd2.IsBackground = False
                    'trd2.Start(objBLLHotelSearch)

                    trd4 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForOneDMCDynamic)
                    trd4.Name = "myThreadOneDMCDynamic"
                    trd4.IsBackground = False
                    trd4.Start(objBLLHotelSearch)
                ElseIf strShowNotColumbusRate <> "YES" And strShowColumbusRate = "YES" Then
                    hdProgress.Value = "1"
                    hdProgressTimer2.Value = "1"
                    ModalPopupDays.Show()
                    Session("sDSSearchResults") = Nothing
                    Session("sDSSearchResultsForPreferred") = Nothing
                    Session("sDSSearchResultsForNonPreferred") = Nothing
                    Session("sPreferred") = Nothing
                    Session("sNonPreferred") = Nothing


                    Session("sDSSearchResultsOneDMCStatic") = Nothing
                    Session("sDSSearchResultsOneDMCDynamic") = Nothing
                    Session("OneDMCDynamic") = Nothing
                    ' Timer1.Enabled = True
                    Timer2.Enabled = True

                    trd1 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
                    trd1.Name = "myThread1"
                    trd1.IsBackground = False
                    trd1.Start(objBLLHotelSearch)


                    trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)
                    trd2.Name = "myThread2"
                    trd2.IsBackground = False
                    trd2.Start(objBLLHotelSearch)
                End If

                'hdProgress.Value = "1"
                'hdProgressTimer2.Value = "1"
                'ModalPopupDays.Show()
                'Session("sDSSearchResults") = Nothing
                'Session("sDSSearchResultsForPreferred") = Nothing
                'Session("sDSSearchResultsForNonPreferred") = Nothing
                'Session("sPreferred") = Nothing
                'Session("sNonPreferred") = Nothing
                '' Timer1.Enabled = True
                'Timer2.Enabled = True

                'trd1 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForPreferred)
                'trd1.Name = "myThread1"
                'trd1.IsBackground = False
                'trd1.Start(objBLLHotelSearch)


                'trd2 = New Thread(AddressOf GetHotelSearchDetailsUsingThreadForNonPreferred)
                'trd2.Name = "myThread2"
                'trd2.IsBackground = False
                'trd2.Start(objBLLHotelSearch)
            Else

                If strShowNotColumbusRate = "YES" And strShowColumbusRate = "YES" Then
                    Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
                    Dim objApiController As ApiController = New ApiController()
                    obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelSearch)
                    Session("sobHotelSearchResponse") = obHotelSearchResponse
                    Dim dsSearchResultsAPI As DataSet = GetMinPriceData(obHotelSearchResponse, objBLLHotelSearch.Room, objBLLHotelSearch.Adult, objBLLHotelSearch.Children, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode)
                    '  Session("sDSSearchResultsForOneDMCDynamic") = dsSearchResultsAPI


                    dsSearchResults = objBLLHotelSearch.GetSearchDetails()

                    If Not dsSearchResults Is Nothing And Not dsSearchResultsAPI Is Nothing Then
                        dsSearchResults.Merge(dsSearchResultsAPI, True, MissingSchemaAction.Add)
                        dsSearchResults.AcceptChanges()
                        Session("sDSSearchResults") = dsSearchResults
                    ElseIf dsSearchResults Is Nothing And Not dsSearchResultsAPI Is Nothing Then
                        Session("sDSSearchResults") = dsSearchResultsAPI
                    ElseIf Not dsSearchResults Is Nothing And dsSearchResultsAPI Is Nothing Then
                        Session("sDSSearchResults") = dsSearchResults
                    End If


                ElseIf strShowNotColumbusRate = "YES" And strShowColumbusRate <> "YES" Then
                    Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
                    Dim objApiController As ApiController = New ApiController()
                    obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelSearch)
                    Session("sobHotelSearchResponse") = obHotelSearchResponse
                    dsSearchResults = GetMinPriceData(obHotelSearchResponse, objBLLHotelSearch.Room, objBLLHotelSearch.Adult, objBLLHotelSearch.Children, objBLLHotelSearch.AgentCode, objBLLHotelSearch.SourceCountryCode)
                    ' Session("sDSSearchResultsForOneDMCDynamic") = dsSearchResults
                    Session("sDSSearchResults") = dsSearchResults
                ElseIf strShowNotColumbusRate <> "YES" And strShowColumbusRate = "YES" Then


                    dsSearchResults = objBLLHotelSearch.GetSearchDetails()
                    Session("sDSSearchResults") = dsSearchResults

                End If


                If dsSearchResults.Tables(0).Rows.Count = 0 Then
                    dvhotnoshow.Style.Add("display", "block")
                    showalternativedates(txtHotelCode.Text)  '' Added shahul 26/07/18

                Else
                    dvhotnoshow.Style.Add("display", "none")
                    dvSearchContent.Visible = True
                    dvPager.Visible = True
                End If

                Session("sDSSearchResults") = dsSearchResults
                If Not dsSearchResults Is Nothing Then


                    If dsSearchResults.Tables.Count > 0 Then
                        Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))






                        '  Dim recordCount As Integer = dvMaiDetails.Count
                        BindHotelMainDetails(dvMaiDetails)
                        '    Me.PopulatePager(recordCount)

                        Session("sdtRoomType") = dsSearchResults.Tables(1)

                        BindPricefilter(dsSearchResults.Tables(0).DefaultView.ToTable(True, "minprice"))
                        BindHotelStars(dsSearchResults.Tables(0).DefaultView.ToTable(True, "catcode", "catname"))
                        BindDestName(dsSearchResults.Tables(0).DefaultView.ToTable(True, "sectorcode", "sectorname"))
                        BindPropertyType(dsSearchResults.Tables(0).DefaultView.ToTable(True, "propertytype", "propertytypename"))
                        BindRoomClassification(dsSearchResults.Tables(1)) 'roomclasscode,roomclassname



                    Else
                        hdPriceMinRange.Value = "0"
                        hdPriceMaxRange.Value = "1"
                    End If
                End If
                imgHotelthreadLoading.Visible = False
            End If


            txtSearchFocus.Focus()

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx ::  fnHotelSearch:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub


    Public Function CreateMinPriceDataSet() As DataSet

        Dim ds As New DataSet
        Dim dtHeader As DataTable
        Dim dtRoomClass As DataTable
        Dim dr As DataRow

        dtHeader = New DataTable()
        dtHeader.TableName = "Table"
        Dim rowid As DataColumn = New DataColumn("rowid", Type.GetType("System.Int32"))
        Dim partycode As DataColumn = New DataColumn("partycode", Type.GetType("System.String"))
        Dim partyname As DataColumn = New DataColumn("partyname", Type.GetType("System.String"))
        Dim citycode As DataColumn = New DataColumn("citycode", Type.GetType("System.String"))
        Dim cityname As DataColumn = New DataColumn("cityname", Type.GetType("System.String"))
        Dim hotelimage As DataColumn = New DataColumn("hotelimage", Type.GetType("System.String"))
        Dim hoteltext As DataColumn = New DataColumn("hoteltext", Type.GetType("System.String"))
        Dim minprice As DataColumn = New DataColumn("minprice", Type.GetType("System.Decimal"))
        Dim catcode As DataColumn = New DataColumn("catcode", Type.GetType("System.String"))
        Dim catname As DataColumn = New DataColumn("catname", Type.GetType("System.String"))
        Dim sectorcode As DataColumn = New DataColumn("sectorcode", Type.GetType("System.String"))
        Dim sectorname As DataColumn = New DataColumn("sectorname", Type.GetType("System.String"))
        Dim propertytype As DataColumn = New DataColumn("propertytype", Type.GetType("System.String"))
        Dim propertytypename As DataColumn = New DataColumn("propertytypename", Type.GetType("System.String"))
        Dim preferred As DataColumn = New DataColumn("preferred", Type.GetType("System.Int32"))
        Dim available As DataColumn = New DataColumn("available", Type.GetType("System.Int32"))
        Dim currcode As DataColumn = New DataColumn("currcode", Type.GetType("System.String"))
        Dim noofstars As DataColumn = New DataColumn("noofstars", Type.GetType("System.String"))
        Dim latitude As DataColumn = New DataColumn("latitude", Type.GetType("System.Decimal"))
        Dim longitude As DataColumn = New DataColumn("longitude", Type.GetType("System.Decimal"))
        Dim forrooms As DataColumn = New DataColumn("forrooms", Type.GetType("System.String"))
        Dim tel1 As DataColumn = New DataColumn("tel1", Type.GetType("System.String"))
        Dim email As DataColumn = New DataColumn("email", Type.GetType("System.String"))
        Dim rateplansource As DataColumn = New DataColumn("rateplansource", Type.GetType("System.String"))
        Dim Int_partycode As DataColumn = New DataColumn("Int_partycode", Type.GetType("System.String"))
        'propertytype	propertytypename	preferred	available	currcode	noofstars	latitude	longitude	forrooms	tel1	email
        dtHeader.Columns.Add(rowid)
        dtHeader.Columns.Add(partycode)
        dtHeader.Columns.Add(partyname)
        dtHeader.Columns.Add(citycode)
        dtHeader.Columns.Add(cityname)
        dtHeader.Columns.Add(hotelimage)
        dtHeader.Columns.Add(hoteltext)
        dtHeader.Columns.Add(minprice)
        dtHeader.Columns.Add(catcode)
        dtHeader.Columns.Add(catname)
        dtHeader.Columns.Add(sectorcode)
        dtHeader.Columns.Add(sectorname)
        dtHeader.Columns.Add(propertytype)
        dtHeader.Columns.Add(propertytypename)
        dtHeader.Columns.Add(preferred)
        dtHeader.Columns.Add(available)
        dtHeader.Columns.Add(currcode)
        dtHeader.Columns.Add(noofstars)
        dtHeader.Columns.Add(latitude)
        dtHeader.Columns.Add(longitude)
        dtHeader.Columns.Add(forrooms)
        dtHeader.Columns.Add(tel1)
        dtHeader.Columns.Add(email)
        dtHeader.Columns.Add(rateplansource)
        dtHeader.Columns.Add(Int_partycode)
        ds.Tables.Add(dtHeader)

        dtRoomClass = New DataTable()
        dtRoomClass.TableName = "Table1"
        Dim roomclasscode As DataColumn = New DataColumn("roomclasscode", Type.GetType("System.String"))
        Dim roomclassname As DataColumn = New DataColumn("roomclassname", Type.GetType("System.String"))

        dtRoomClass.Columns.Add(roomclasscode)
        dtRoomClass.Columns.Add(roomclassname)

        ds.Tables.Add(dtRoomClass)
        Return ds

    End Function

    Public Function CreateSingleHotelDataSet() As DataSet
        Dim ds As New DataSet
        Dim dtHeader As DataTable
        Dim dtRoomPrice As DataTable

        dtHeader = New DataTable()
        dtHeader.TableName = "Table"
        Dim partycode As DataColumn = New DataColumn("partycode", Type.GetType("System.String"))
        Dim rateplanid As DataColumn = New DataColumn("rateplanid", Type.GetType("System.String"))
        Dim rateplanname As DataColumn = New DataColumn("rateplanname", Type.GetType("System.String"))
        Dim rmtypcode As DataColumn = New DataColumn("rmtypcode", Type.GetType("System.String"))
        Dim rmtypname As DataColumn = New DataColumn("rmtypname", Type.GetType("System.String"))
        Dim mealplans As DataColumn = New DataColumn("mealplans", Type.GetType("System.String"))
        Dim mealplannames As DataColumn = New DataColumn("mealplannames", Type.GetType("System.String"))
        Dim totalvalue As DataColumn = New DataColumn("totalvalue", Type.GetType("System.Decimal"))
        Dim available As DataColumn = New DataColumn("available", Type.GetType("System.Int32"))
        Dim rmtyporder As DataColumn = New DataColumn("rmtyporder", Type.GetType("System.Int32"))
        Dim avgprice As DataColumn = New DataColumn("avgprice", Type.GetType("System.Decimal"))
        Dim partyavgprice As DataColumn = New DataColumn("partyavgprice", Type.GetType("System.Decimal"))
        Dim partyavailable As DataColumn = New DataColumn("partyavailable", Type.GetType("System.Int32"))
        Dim currcode As DataColumn = New DataColumn("currcode", Type.GetType("System.String"))
        Dim roomclasscode As DataColumn = New DataColumn("roomclasscode", Type.GetType("System.String"))
        Dim rmcatcode As DataColumn = New DataColumn("rmcatcode", Type.GetType("System.String"))
        Dim rateplanorder As DataColumn = New DataColumn("rateplanorder", Type.GetType("System.Int32"))
        Dim salevalue As DataColumn = New DataColumn("salevalue", Type.GetType("System.Decimal"))
        Dim costvalue As DataColumn = New DataColumn("costvalue", Type.GetType("System.Decimal"))
        Dim costcurrcode As DataColumn = New DataColumn("costcurrcode", Type.GetType("System.String"))
        Dim supagentcode As DataColumn = New DataColumn("supagentcode", Type.GetType("System.String"))
        Dim comp_cust As DataColumn = New DataColumn("comp_cust", Type.GetType("System.Int32"))
        Dim comp_supp As DataColumn = New DataColumn("comp_supp", Type.GetType("System.Int32"))
        Dim comparrtrf As DataColumn = New DataColumn("comparrtrf", Type.GetType("System.Int32"))
        Dim compdeptrf As DataColumn = New DataColumn("compdeptrf", Type.GetType("System.Int32"))
        Dim forrooms As DataColumn = New DataColumn("forrooms", Type.GetType("System.String"))
        Dim rateplansummary As DataColumn = New DataColumn("rateplansummary", Type.GetType("System.String"))
        Dim roomname As DataColumn = New DataColumn("roomname", Type.GetType("System.String"))
        Dim noofrooms As DataColumn = New DataColumn("noofrooms", Type.GetType("System.Int32"))
        Dim noofadults As DataColumn = New DataColumn("noofadults", Type.GetType("System.Int32"))
        Dim noofchild As DataColumn = New DataColumn("noofchild", Type.GetType("System.Int32"))
        Dim childagestring As DataColumn = New DataColumn("childagestring", Type.GetType("System.String"))
        Dim noofadulteb As DataColumn = New DataColumn("noofadulteb", Type.GetType("System.Int32"))
        Dim noofchildeb As DataColumn = New DataColumn("noofchildeb", Type.GetType("System.Int32"))
        Dim sharingorextrabed As DataColumn = New DataColumn("sharingorextrabed", Type.GetType("System.String"))
        Dim mealorder As DataColumn = New DataColumn("mealorder", Type.GetType("System.Int32"))
        Dim currentselection As DataColumn = New DataColumn("currentselection", Type.GetType("System.Int32"))
        Dim hotelroomstring As DataColumn = New DataColumn("hotelroomstring", Type.GetType("System.String"))
        Dim VATexclude As DataColumn = New DataColumn("VATexclude", Type.GetType("System.Int32"))
        Dim vatperc As DataColumn = New DataColumn("vatperc", Type.GetType("System.Decimal"))
        Dim costtaxablevalue As DataColumn = New DataColumn("costtaxablevalue", Type.GetType("System.Decimal"))
        Dim costnontaxablevalue As DataColumn = New DataColumn("costnontaxablevalue", Type.GetType("System.Decimal"))
        Dim costvatvalue As DataColumn = New DataColumn("costvatvalue", Type.GetType("System.Decimal"))
        Dim wlcurrcode As DataColumn = New DataColumn("wlcurrcode", Type.GetType("System.String"))
        Dim Show As DataColumn = New DataColumn("Show", Type.GetType("System.String"))
        Dim mealupgradefrom As DataColumn = New DataColumn("mealupgradefrom", Type.GetType("System.String"))
        Dim rateplansource As DataColumn = New DataColumn("rateplansource", Type.GetType("System.String"))
        Dim Int_RoomtypeCodes As DataColumn = New DataColumn("Int_RoomtypeCodes", Type.GetType("System.String"))
        Dim Int_RoomtypeNames As DataColumn = New DataColumn("Int_RoomtypeNames", Type.GetType("System.String"))
        Dim Int_Roomtypes As DataColumn = New DataColumn("Int_Roomtypes", Type.GetType("System.String"))
        dtHeader.Columns.Add(partycode)
        dtHeader.Columns.Add(rateplanid)
        dtHeader.Columns.Add(rateplanname)
        dtHeader.Columns.Add(rmtypcode)
        dtHeader.Columns.Add(rmtypname)
        dtHeader.Columns.Add(mealplans)
        dtHeader.Columns.Add(mealplannames)
        dtHeader.Columns.Add(totalvalue)
        dtHeader.Columns.Add(available)
        dtHeader.Columns.Add(rmtyporder)
        dtHeader.Columns.Add(avgprice)
        dtHeader.Columns.Add(partyavgprice)
        dtHeader.Columns.Add(partyavailable)
        dtHeader.Columns.Add(currcode)
        dtHeader.Columns.Add(roomclasscode)
        dtHeader.Columns.Add(rmcatcode)
        dtHeader.Columns.Add(rateplanorder)
        dtHeader.Columns.Add(salevalue)
        dtHeader.Columns.Add(costvalue)
        dtHeader.Columns.Add(costcurrcode)
        dtHeader.Columns.Add(supagentcode)
        dtHeader.Columns.Add(comp_cust)
        dtHeader.Columns.Add(comp_supp)
        dtHeader.Columns.Add(comparrtrf)
        dtHeader.Columns.Add(compdeptrf)
        dtHeader.Columns.Add(forrooms)
        dtHeader.Columns.Add(rateplansummary)
        dtHeader.Columns.Add(roomname)
        dtHeader.Columns.Add(noofrooms)
        dtHeader.Columns.Add(noofadults)
        dtHeader.Columns.Add(noofchild)
        dtHeader.Columns.Add(childagestring)
        dtHeader.Columns.Add(noofadulteb)
        dtHeader.Columns.Add(noofchildeb)
        dtHeader.Columns.Add(sharingorextrabed)
        dtHeader.Columns.Add(mealorder)
        dtHeader.Columns.Add(currentselection)
        dtHeader.Columns.Add(hotelroomstring)
        dtHeader.Columns.Add(VATexclude)
        dtHeader.Columns.Add(vatperc)
        dtHeader.Columns.Add(costtaxablevalue)
        dtHeader.Columns.Add(costnontaxablevalue)
        dtHeader.Columns.Add(costvatvalue)
        dtHeader.Columns.Add(wlcurrcode)
        dtHeader.Columns.Add(Show)
        dtHeader.Columns.Add(mealupgradefrom)
        dtHeader.Columns.Add(rateplansource)
        dtHeader.Columns.Add(Int_RoomtypeCodes)
        dtHeader.Columns.Add(Int_RoomtypeNames)
        dtHeader.Columns.Add(Int_Roomtypes)
        Dim Int_costprice As DataColumn = New DataColumn("Int_costprice", Type.GetType("System.Decimal"))
        dtHeader.Columns.Add(Int_costprice)
        Dim Int_costcurrcode As DataColumn = New DataColumn("Int_costcurrcode", Type.GetType("System.String"))
        dtHeader.Columns.Add(Int_costcurrcode)
        Dim Int_partycode As DataColumn = New DataColumn("Int_partycode", Type.GetType("System.String"))
        dtHeader.Columns.Add(Int_partycode)

        Dim Int_rmtypecode As DataColumn = New DataColumn("Int_rmtypecode", Type.GetType("System.String"))
        dtHeader.Columns.Add(Int_rmtypecode)
        Dim Int_mealcode As DataColumn = New DataColumn("Int_mealcode", Type.GetType("System.String"))
        dtHeader.Columns.Add(Int_mealcode)
        Dim accomodationcode As DataColumn = New DataColumn("accomodationcode", Type.GetType("System.String"))
        dtHeader.Columns.Add(accomodationcode)

        Dim offercode As DataColumn = New DataColumn("offercode", Type.GetType("System.String"))
        dtHeader.Columns.Add(offercode)
        Dim RoomId As DataColumn = New DataColumn("RoomId", Type.GetType("System.String"))
        dtHeader.Columns.Add(RoomId)

        ds.Tables.Add(dtHeader)

        dtRoomPrice = CreateRoomPriceDataTable()
        ds.Tables.Add(dtRoomPrice)


        Dim dtRoomPriceTotal As DataTable = CreateRoomPriceTotalDataTable()
        ds.Tables.Add(dtRoomPriceTotal)

        Dim dtWarning = New DataTable()
        dtWarning.TableName = "Table3"
        Dim hotelcode As DataColumn = New DataColumn("hotelcode", Type.GetType("System.String"))
        Dim autoid As DataColumn = New DataColumn("autoid", Type.GetType("System.Int32"))
        Dim pricemessage As DataColumn = New DataColumn("pricemessage", Type.GetType("System.String"))

        dtWarning.Columns.Add(hotelcode)
        dtWarning.Columns.Add(autoid)
        dtWarning.Columns.Add(pricemessage)

        ds.Tables.Add(dtWarning)

        Return ds

    End Function
    Public Function ConvertToDataSet(Of T)(ByVal list As IList(Of T)) As DataSet

        Dim dsFromDtStru As New DataSet()
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields()
        For Each field As FieldInfo In fields
            table.Columns.Add(field.Name, field.FieldType)
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                row(field.Name) = field.GetValue(item)
            Next
            table.Rows.Add(row)
        Next

        dsFromDtStru.Tables.Add(table)
        Return dsFromDtStru

    End Function
    ''' <summary>
    ''' GetHotelSearchDetailsUsingThreadForPreferred
    ''' </summary>
    ''' <param name="objBLLHotelsSearch"></param>
    ''' <remarks></remarks>
    Private Sub GetHotelSearchDetailsUsingThreadForPreferred(ByVal objBLLHotelsSearch As BLLHotelSearch)
        Dim dsSearchResults As DataSet
        dsSearchResults = objBLLHotelsSearch.GetSearchDetails("0", "", "", "", "", "", "", "1")
        Session("sDSSearchResultsForPreferred") = dsSearchResults

    End Sub
    ''' <summary>
    ''' GetHotelSearchDetailsUsingThreadForNonPreferred
    ''' </summary>
    ''' <param name="objBLLHotelsSearch"></param>
    ''' <remarks></remarks>
    Private Sub GetHotelSearchDetailsUsingThreadForNonPreferred(ByVal objBLLHotelsSearch As BLLHotelSearch)
        Dim dsSearchResults As DataSet
        dsSearchResults = objBLLHotelsSearch.GetSearchDetails("0", "", "", "", "", "", "", "2")
        Session("sDSSearchResultsForNonPreferred") = dsSearchResults
    End Sub



    Private Sub GetHotelSearchDetailsUsingThreadForOneDMCStatic(ByVal objBLLHotelsSearch As BLLHotelSearch)
        Dim dsSearchResults As DataSet
        dsSearchResults = objBLLHotelsSearch.GetSearchDetails("0", "", "", "", "", "", "", "2")
        Session("sDSSearchResultsForOneDMCStatic") = dsSearchResults
    End Sub
    Private Sub GetHotelSearchDetailsUsingThreadForOneDMCDynamic(ByVal objBLLHotelsSearch_R As BLLHotelSearch)
        Dim dsSearchResults As DataSet

        'Added by abin on 20200806 12.01 AM
        Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
        Dim objApiController As ApiController = New ApiController()
        obHotelSearchResponse = objApiController.CallHotelSearchAPI(objBLLHotelsSearch_R)
        Session("sobHotelSearchResponse") = obHotelSearchResponse
        dsSearchResults = GetMinPriceData(obHotelSearchResponse, objBLLHotelsSearch_R.Room, objBLLHotelsSearch_R.Adult, objBLLHotelsSearch_R.Children, objBLLHotelsSearch_R.AgentCode, objBLLHotelsSearch_R.SourceCountryCode)
        Session("sDSSearchResultsForOneDMCDynamic") = dsSearchResults
    End Sub


    Private Sub FillBookingDetailsForEdit()
        objBLLHotelSearch = New BLLHotelSearch
        Dim dt As DataTable = objBLLHotelSearch.GetBookingDetailsForEdit(Session("sRequestId"), Request.QueryString("RLineNo"))
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                hdOPMode.Value = "Edit"
                chkShifting.Enabled = False

                txtDestinationName.Text = dt.Rows(0)("destname").ToString
                txtDestinationCode.Text = dt.Rows(0)("destinationcode").ToString & "|" & dt.Rows(0)("destinationtype").ToString
                txtCheckIn.Text = dt.Rows(0)("checkin").ToString
                txtCheckOut.Text = dt.Rows(0)("checkout").ToString
                txtNoOfNights.Text = dt.Rows(0)("noofnights").ToString
                ddlRoom_Dynamic.SelectedValue = dt.Rows(0)("noofrooms").ToString
                'Remove rooms
                If Val(dt.Rows(0)("cnt_cancel_entry").ToString) > 0 Then
                    If ddlRoom_Dynamic.Items.Count > 0 Then
                        For i As Integer = 1 To Val(ddlRoom_Dynamic.SelectedValue) - 1
                            ddlRoom_Dynamic.Items.Remove(i)
                        Next

                    End If
                End If

                FillShiftingRoomAdultChild(dt.Rows(0)("roomstring").ToString)
                Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
                Dim javaScriptChldrn1 As String = "<script type='text/javascript'>ShowAdultChild();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
                txtCustomer.Text = dt.Rows(0)("agentname").ToString
                txtCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                txtCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtCountry.Text = dt.Rows(0)("ctryname").ToString
                txtHotelStars.Text = dt.Rows(0)("catname").ToString
                txtHotelStarsCode.Text = dt.Rows(0)("catcode").ToString
                'ddlAvailability.SelectedValue = IIf(dt.Rows(0)("available").ToString = 0, 2, 1)
                ddlAvailability.SelectedValue = "2" '' added shahul Arun wants to show by default All 03/11/2018
                ddlPropertType.SelectedValue = dt.Rows(0)("propertytypecode").ToString
                txtHotelName.Text = dt.Rows(0)("hotelname").ToString
                txtHotelCode.Text = dt.Rows(0)("hotelcode").ToString
                If dt.Rows(0)("overrideprice").ToString = "1" Then
                    chkOveridePrice.Checked = True
                Else
                    chkOveridePrice.Checked = False
                End If

                hdEditRLineNo.Value = Request.QueryString("RLineNo")
                hdEditRatePlanId.Value = dt.Rows(0)("editrateplanid").ToString

                ddlMealPlan.SelectedValue = dt.Rows(0)("mealplan").ToString
                hdmealcode.Value = dt.Rows(0)("mealplan").ToString
                chkshowall.Enabled = False

                If txtDestinationCode.Text <> "" Then

                    Dim strContext As String = ""
                    If txtDestinationCode.Text <> "" Then
                        If strContext <> "" Then
                            strContext = strContext & "||" & "DC:" + txtDestinationCode.Text
                        Else
                            strContext = "DC:" + txtDestinationCode.Text
                        End If

                    End If
                    If txtHotelStarsCode.Text <> "" Then
                        If strContext <> "" Then
                            strContext = strContext & "||" & "HSC:" + txtHotelStarsCode.Text
                        Else
                            strContext = "HSC:" + txtHotelStarsCode.Text
                        End If

                    End If
                    If ddlPropertType.SelectedValue <> "" Then
                        If strContext <> "" Then
                            strContext = strContext & "||" & "PT:" + ddlPropertType.SelectedValue
                        Else
                            strContext = "PT:" + ddlPropertType.SelectedValue
                        End If

                    End If

                    AutoCompleteExtender_txtHotelName.ContextKey = strContext
                    If dt.Rows(0)("shiftto").ToString = "1" Then

                        dvShifting.Attributes.Add("style", "display:block;")
                        txtShiftHotel.Text = dt.Rows(0)("ShiftHotelName").ToString
                        txtShiftHotelCode.Text = dt.Rows(0)("ShiftFromPartyCodeAndLineNo").ToString
                        ''chkShifting.Enabled = False
                        chkShifting.Checked = True

                        Dim strScript As String = "javascript: CheckShiftingCheckbox('" + chkShifting.ClientID + "')"
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)

                        ''Dim strScript As String = "javascript: CallPriceSlider();"
                        ''ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)

                    Else
                        chkShifting.Checked = False
                        dvShifting.Attributes.Add("style", "display:none;")
                    End If
                End If

                sbBindShiftHotelDetail(False) 'changed by mohamed on 06/09/2018
                fnHotelSearch()
            End If
        End If
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
        Session("sdsHotelRoomTypes") = Nothing
        '*** Danny 26/082018
        Session("dtAdultChilds") = Nothing

    End Sub
    ''' <summary>
    ''' FillBookingDetailsBasedonNewOrEditMode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillBookingDetailsBasedonNewOrEditMode()
        If Not Request.QueryString("HCode") Is Nothing Then
            Dim strHId As String = Request.QueryString("HCode")
            'Dim strHId As String = "HI13"
            Dim scriptKey As String = "UniqueKeyForThisScript"

            txtHotelCode.Text = strHId
            txtHotelName.Text = objBLLHotelSearch.GetPartyName(strHId)

            Dim javaScriptGetHotelsDetails As String = "<script type='text/javascript'>GetHotelsDetails('" + strHId + "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScriptGetHotelsDetails)
            txtCheckIn.Text = ""
            txtCheckOut.Text = ""
            txtNoOfNights.Text = ""
            ddlRoom_Dynamic.SelectedIndex = 0
        Else

            If Session("sRequestId") Is Nothing Then ' Fresh Mode
                FillFreshSearchDetails()
                BindSearchResults()
                dvShifting.Attributes.Add("style", "display:none;")
                dvShiftingPreArranged.Attributes.Add("style", "display:none;")
            ElseIf Not Session("sRequestId") Is Nothing And Request.QueryString("PLineNo") Is Nothing Then
                Dim dsBooking As DataSet
                dsBooking = objBLLCommonFuntions.GetTempFullBookingDetails(Session("sRequestId"))

                If dsBooking.Tables(1).Rows.Count > 0 Or dsBooking.Tables(6).Rows.Count > 0 Then ' if hotel is exist 'If dsBooking.Tables(1).Rows.Count > 0 Then  'changed by abin / mohamed on 08/04/2018
                    hdHotelAvailableForShifting.Value = "1"
                    If Not Session("sRequestId") Is Nothing And Not Session("sEditRequestId") Is Nothing And Not Request.QueryString("RLineNo") Is Nothing Then ' Amend Mode
                        FillBookingDetailsForEdit()

                        ' dvShifting.Attributes.Add("style", "display:none;")
                    ElseIf Not Session("sRequestId") Is Nothing And Not Session("sEditRequestId") Is Nothing And Request.QueryString("RLineNo") Is Nothing Then ' Amend with New Mode

                        Dim dtBookingHeader As DataTable
                        dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
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
                        'CheckShiftingAvailable() 'commented / changed by mohamed on 11/04/2018
                        chkShifting.Checked = True 'changed by mohamed on 11/04/2018
                    ElseIf Not Session("sRequestId") Is Nothing And Session("sEditRequestId") Is Nothing And Not Request.QueryString("RLineNo") Is Nothing Then ' Edit mode
                        BindSearchResultsForEdit()
                        FillBookingDetailsForEdit()

                    ElseIf Not Session("sRequestId") Is Nothing And Session("sEditRequestId") Is Nothing And Request.QueryString("RLineNo") Is Nothing Then ' New mode
                        chkShifting.Checked = True 'changed by mohamed on 11/04/2018
                        FillFreshSearchDetails()
                        ' BindSearchResults()
                        'CheckShiftingAvailable() 'commented / changed by mohamed on 11/04/2018
                        'dvShiftingSub.Style.Add("display", "block")
                    End If

                Else 'if hotel is not exist
                    FillFreshSearchDetails()
                    BindSearchResults()
                    '  dvShifting.Attributes.Add("style", "display:block;")
                End If

                'changed by abin / mohamed on 11/04/2018
                txtCustomer.Attributes.Add("readonly", True)
                txtCountry.Attributes.Add("readonly", True)

                If Session("sLoginType") = "RO" Then
                    txtCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtCustomer.Enabled = False
                    AutoCompleteExtender_txtCountry.Enabled = False
                End If


            ElseIf Not Session("sRequestId") Is Nothing And Request.QueryString("RLineNo") Is Nothing And Not Request.QueryString("PLineNo") Is Nothing Then
                FillFreshSearchDetails()
            End If


        End If
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
        'Added by abin on 20190714
        If ddlRoom_Dynamic.SelectedValue > 5 Then
            ddlAvailability.SelectedValue = "1"
        Else
            ddlAvailability.SelectedValue = "2"
        End If

    End Sub
    Private Sub DynamicRoomCreate()
        Dim dtAdultChilds = New DataTable

        dtAdultChilds.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))

        dlNofoRooms.DataSource = dtAdultChilds
        dlNofoRooms.DataBind()
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

        dlNofoRooms.DataSource = dtAdultChilds
        dlNofoRooms.DataBind()

        Dim intNoAdults As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=54")
        Dim intNoChilds As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=55")


        If dlNofoRooms.Items.Count > 0 Then
            For Each dlItem1 As DataListItem In dlNofoRooms.Items
                Dim ddlDynRoomAdult As DropDownList = CType(dlItem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlItem1.FindControl("ddlDynRoomChild"), DropDownList)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomAdult, intNoAdults)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomChild, intNoChilds)
            Next

        End If


    End Sub

    ''' <summary>
    ''' <FillFreshSearchDetails/>
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillFreshSearchDetails()
        objBLLHotelSearch = New BLLHotelSearch
        Dim objBLLTourSearch As BLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
        If Not Session("sobjBLLHotelSearch") Is Nothing Then
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)

            If Not Page.Request.UrlReferrer Is Nothing Then
                Dim previousPages As String = Page.Request.UrlReferrer.ToString
                If previousPages.Contains("MoreServices.aspx") And Session("sobjBLLHotelSearchActive") IsNot Nothing Then
                    Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                    iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                    hdBookingEngineRateType.Value = iCumulative.ToString

                    Dim iCumRo As Integer = 0
                    If Session("sLoginType") = "RO" Then
                        Dim objBLLguest As New BLLGuest
                        If Not Session("sRequestId") Is Nothing Then
                            iCumRo = objBLLguest.CheckSelectedAgentIsCumulative(Session("sRequestId"))
                        End If

                    End If

                    If hdBookingEngineRateType.Value = "1" Or iCumRo > 0 Then
                        chkShifting.Checked = True
                        '' As per priorty Arun sheet 13/02/18 commented shahul
                        chkShifting.Enabled = True
                        '''
                    End If
                Else


                    txtCheckIn.Text = objBLLHotelSearch.CheckIn
                    Dim scriptKey As String = "UniqueKeyForThisScript"
                    Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                    txtCheckOut.Text = objBLLHotelSearch.CheckOut
                End If
            Else
                txtCheckIn.Text = objBLLHotelSearch.CheckIn
                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                txtCheckOut.Text = objBLLHotelSearch.CheckOut
            End If



            '*** Danny 26/08/2018
            FillShiftingRoomAdultChild(objBLLHotelSearch.RoomString.ToString)
            txtNoOfNights.Text = objBLLHotelSearch.NoOfNights

            ' ''ddlRoom_Dynamic.SelectedValue = objBLLHotelSearch.Room
            ' ''DynamicRoomCreate()

            ' ''Dim strroomstring As String() = objBLLHotelSearch.RoomString.Split(";")
            ' ''Dim strroom As String()
            ' ''Dim strchildage As String()
            ' ''If strroomstring.Length > 0 Then




            ' ''    Dim a As Integer = 0
            ' ''    For Each dlRoomtem1 As DataListItem In dlNofoRooms.Items
            ' ''        strroom = strroomstring(a).Split(",")
            ' ''        Dim ddlDynRoomAdult As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomAdult"), DropDownList)
            ' ''        Dim ddlDynRoomChild As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomChild"), DropDownList)
            ' ''        Dim ddlDynChildAges As DataList = CType(dlRoomtem1.FindControl("dlChildAges"), DataList)

            ' ''        ddlDynRoomAdult.SelectedIndex = strroom(1)
            ' ''        ddlDynRoomChild.SelectedIndex = strroom(2)
            ' ''        If strroom(2).ToString <> "0" Then
            ' ''            Dim dtChildAge = New DataTable
            ' ''            dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
            ' ''            dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
            ' ''            dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
            ' ''            strchildage = strroom(3).Split("|")
            ' ''            For j = 0 To strchildage.Length - 1
            ' ''                Dim row2 As DataRow = dtChildAge.NewRow()
            ' ''                If j = 0 Then
            ' ''                    row2("colChildAgeLbl") = "Room " + strroom(0).ToString + " Child Age"
            ' ''                Else
            ' ''                    row2("colChildAgeLbl") = ""
            ' ''                End If
            ' ''                row2("colChildAge") = strchildage(j)
            ' ''                row2("colCHNo") = "CH" + (j + 1).ToString
            ' ''                dtChildAge.Rows.Add(row2)
            ' ''            Next
            ' ''            ddlDynChildAges.DataSource = dtChildAge
            ' ''            ddlDynChildAges.DataBind()
            ' ''        End If
            ' ''        a = a + 1
            ' ''    Next
            ' ''End If



            If objBLLHotelSearch.Children <> "0" Then
                Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                Dim javaScriptChldrn As String = "<script type='text/javascript'>ShowAdultChild();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)
            End If
            txtCountry.Text = objBLLHotelSearch.SourceCountry
            txtCountryCode.Text = objBLLHotelSearch.SourceCountryCode
            ddlOrderBy.SelectedValue = objBLLHotelSearch.OrderBy
            txtCustomer.Text = objBLLHotelSearch.Customer
            txtCustomerCode.Text = objBLLHotelSearch.CustomerCode
            If Not Page.Request.UrlReferrer Is Nothing Then
                Dim previousPage As String = Page.Request.UrlReferrer.ToString
                If previousPage.Contains("MoreServices.aspx") And Session("sobjBLLHotelSearchActive") IsNot Nothing Then
                Else
                    txtDestinationCode.Text = objBLLHotelSearch.DestinationCodeAndType
                    txtDestinationName.Text = objBLLHotelSearch.Destination
                    txtHotelStars.Text = objBLLHotelSearch.StarCategory
                    txtHotelStarsCode.Text = objBLLHotelSearch.StarCategoryCode
                    ddlPropertType.SelectedValue = objBLLHotelSearch.PropertyType
                    txtHotelName.Text = objBLLHotelSearch.Hotels
                    txtHotelCode.Text = objBLLHotelSearch.HotelCode

                    ''** Shahul 26/06/2018
                    ddlMealPlan.SelectedValue = Replace(objBLLHotelSearch.MealPlan, ";", ",")
                    hdmealcode.Value = Replace(objBLLHotelSearch.MealPlan, ";", ",")
                    Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
                    Dim javaScriptChldrn1 As String = "<script type='text/javascript'>Assignmealcode();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

                    If objBLLHotelSearch.ShowallCategory = "1" Then
                        chkshowall.Checked = True
                    Else
                        chkshowall.Checked = False
                    End If
                    If txtHotelCode.Text <> "" Then
                        chkshowall.Enabled = False
                    Else
                        chkshowall.Enabled = True
                    End If
                End If
            Else
                txtDestinationCode.Text = objBLLHotelSearch.DestinationCodeAndType
                txtDestinationName.Text = objBLLHotelSearch.Destination
                txtHotelStars.Text = objBLLHotelSearch.StarCategory
                txtHotelStarsCode.Text = objBLLHotelSearch.StarCategoryCode
                ddlPropertType.SelectedValue = objBLLHotelSearch.PropertyType
                txtHotelName.Text = objBLLHotelSearch.Hotels
                txtHotelCode.Text = objBLLHotelSearch.HotelCode

                ''** Shahul 26/06/2018
                ddlMealPlan.SelectedValue = objBLLHotelSearch.MealPlan
                hdmealcode.Value = objBLLHotelSearch.MealPlan
                Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
                Dim javaScriptChldrn1 As String = "<script type='text/javascript'>Assignmealcode();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

                If objBLLHotelSearch.ShowallCategory = "1" Then
                    chkshowall.Checked = True
                Else
                    chkshowall.Checked = False
                End If
                If txtHotelCode.Text <> "" Then
                    chkshowall.Enabled = False
                Else
                    chkshowall.Enabled = True
                End If
            End If



            ' ddlAvailability.SelectedValue = objBLLHotelSearch.Availabilty
            If objBLLHotelSearch.OverridePrice = "1" Then
                chkOveridePrice.Checked = True
            Else
                chkOveridePrice.Checked = False
            End If

            ' chkHotelStars.SelectedValue = objBLLHotelSearch.StarCategoryCode
            ' chkPropertyType.SelectedValue = objBLLHotelSearch.PropertyType
            ' chkSectors.SelectedValue = objBLLHotelSearch.DestinationCode


            If txtDestinationCode.Text <> "" Then

                Dim strContext As String = ""
                If objBLLHotelSearch.DestinationCode <> "" Then
                    If strContext <> "" Then
                        strContext = strContext & "||" & "DC:" + objBLLHotelSearch.DestinationCode
                    Else
                        strContext = "DC:" + objBLLHotelSearch.DestinationCode
                    End If

                End If
                If objBLLHotelSearch.StarCategoryCode <> "" Then
                    If strContext <> "" Then
                        strContext = strContext & "||" & "HSC:" + objBLLHotelSearch.StarCategoryCode
                    Else
                        strContext = "HSC:" + objBLLHotelSearch.StarCategoryCode
                    End If

                End If
                If objBLLHotelSearch.PropertyType <> "" Then
                    If strContext <> "" Then
                        strContext = strContext & "||" & "PT:" + objBLLHotelSearch.PropertyType
                    Else
                        strContext = "PT:" + objBLLHotelSearch.PropertyType
                    End If

                End If

                AutoCompleteExtender_txtHotelName.ContextKey = strContext

            End If
        Else
            Dim dtBookingHeader As DataTable
            dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If Not dtBookingHeader Is Nothing Then
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

        End If
        If Not Session("sRequestId") Is Nothing Then
            'CheckShiftingAvailable() 'commented / changed by mohamed on 11/04/2018
        End If
    End Sub
    ''' <summary>
    ''' <CheckShiftingAvailable/>
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckShiftingAvailable()
        Dim dt As DataTable
        'changed by mohamed on 05/04/2018
        'dt = objBLLHotelSearch.GetHotelBookingDetailsForShifting(Session("sRequestId").ToString)
        dt = objBLLHotelSearch.GetHotelBookingDetailsForShiftingNew(Session("sRequestId").ToString, -1)

        If dt.Rows.Count > 0 Then
            dvShifting.Attributes.Add("style", "display:block;")
            'If dt.Rows.Count = 1 Then 'changed by mohamed on 08/04/2018
            'changed by mohamed on 08/04/2018
            'txtShiftHotel.Text = dt.Rows(0)("HotelName").ToString
            'txtShiftHotelCode.Text = dt.Rows(0)("partycode").ToString & "|" & dt.Rows(0)("rlineno").ToString 'dt.Rows(0)("code").ToString 'changed by mohamed on 05/04/2018
            'txtCheckIn.Text = dt.Rows(0)("checkout").ToString
            'ddlRoom.SelectedValue = dt.Rows(0)("noofrooms").ToString
            'FillShiftingRoomAdultChild(dt.Rows(0)("RoomString").ToString)
            'Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            'Dim javaScriptChldrn1 As String = "<script type='text/javascript'>ShowAdultChild();</script>"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

            'Else 'changed by mohamed on 08/04/2018
            ddlRoom_Dynamic.SelectedValue = dt.Rows(0)("noofrooms").ToString
            FillShiftingRoomAdultChild(dt.Rows(0)("RoomString").ToString)
            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            Dim javaScriptChldrn1 As String = "<script type='text/javascript'>ShowAdultChild();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
            'End If 'changed by mohamed on 08/04/2018
        Else
            dvShifting.Attributes.Add("style", "display:none;")
        End If
    End Sub
    ''' <summary>
    ''' txtSearchHotel_TextChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtSearchHotel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchHotel.TextChanged
        Try
            If Session("sDSSearchResults") IsNot Nothing Then
                Dim dsSearchResults As New DataSet
                dsSearchResults = Session("sDSSearchResults")
                Session("sMailBoxPageIndex") = "1"
                BindHotelMainDetailsWithFilter(dsSearchResults)
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("hotelSearch.aspx :: txtSearchHotel_TextChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' btnHotelTextSearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnHotelTextSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHotelTextSearch.Click
        Try
            'changed by mohamed on 12/02/2018
            If Session("sDSSearchResults") IsNot Nothing Then
                Dim dsSearchResults As New DataSet
                dsSearchResults = Session("sDSSearchResults")
                Session("sMailBoxPageIndex") = "1"
                BindHotelMainDetailsWithFilter(dsSearchResults)
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: ddlSorting_SelectedIndexChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' Timer1_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            If (Not Session("sDSSearchResultsForPreferred") Is Nothing Or strShowColumbusRate <> "YES") And (Not Session("sDSSearchResultsForNonPreferred") Is Nothing Or strShowColumbusRate <> "YES") And (Not Session("sDSSearchResultsForOneDMCDynamic") Is Nothing Or strShowNotColumbusRate <> "YES") Then 'Added by abin on 20200810
                '  If Not Session("sDSSearchResultsForPreferred") Is Nothing And Not Session("sDSSearchResultsForNonPreferred") Is Nothing And Not Session("sDSSearchResultsForOneDMCStatic") Is Nothing And Not Session("sDSSearchResultsForOneDMCDynamic") Is Nothing Then



                Timer1.Enabled = False
                Timer2.Enabled = False
                Dim DSSearchResultsForPreferred As DataSet
                DSSearchResultsForPreferred = CreateMinPriceDataSet()

                If Not Session("sDSSearchResultsForPreferred") Is Nothing Then
                    DSSearchResultsForPreferred.Merge(Session("sDSSearchResultsForPreferred"), True, MissingSchemaAction.Add)
                End If
                If Not Session("sDSSearchResultsForNonPreferred") Is Nothing Then
                    DSSearchResultsForPreferred.Merge(Session("sDSSearchResultsForNonPreferred"), True, MissingSchemaAction.Add)
                End If
                If Not Session("sDSSearchResultsForOneDMCDynamic") Is Nothing Then
                    DSSearchResultsForPreferred.Merge(Session("sDSSearchResultsForOneDMCDynamic"), True, MissingSchemaAction.Add)
                End If

                DSSearchResultsForPreferred.AcceptChanges()
                Session("sDSSearchResults") = DSSearchResultsForPreferred

                If Not Session("sDSSearchResults") Is Nothing Then
                    BindThreadResult()
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterThreadUpdate();", True)
                Else
                    dvhotnoshow.Style.Add("display", "none")
                End If
                Session("sDSSearchResultsForPreferred") = Nothing
                Session("sDSSearchResultsForNonPreferred") = Nothing

                Session("sDSSearchResultsForOneDMCStatic") = Nothing
                Session("sDSSearchResultsForOneDMCDynamic") = Nothing

                hdProgress.Value = "2"
                hdProgressTimer2.Value = "2"
                imgHotelthreadLoading.Visible = False
            Else

                Dim DSSearchResults As DataSet
                If Session("sDSSearchResults") Is Nothing Then
                    DSSearchResults = CreateMinPriceDataSet()
                Else
                    DSSearchResults = Session("sDSSearchResults")
                End If
                If strShowColumbusRate = "YES" Then

                    If Not Session("sDSSearchResultsForPreferred") Is Nothing And Session("sPreferred") Is Nothing Then
                        DSSearchResults.Merge(Session("sDSSearchResultsForPreferred"), True, MissingSchemaAction.Add)
                        DSSearchResults.AcceptChanges()
                        Session("sDSSearchResults") = DSSearchResults
                        Session("sPreferred") = "1"
                        Timer1.Enabled = False
                        'If Not Session("sDSSearchResults") Is Nothing Then
                        '    Dim ds As DataSet
                        '    ds = Session("sDSSearchResults")
                        '    If ds.Tables(0).Rows.Count = 0 Then
                        '        hdProgress.Value = "1"
                        '    Else
                        '        hdProgress.Value = "0"
                        '    End If

                        '    BindThreadResult()
                        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterThreadUpdate();", True)
                        'Else
                        '    dvhotnoshow.Style.Add("display", "none")
                        '    hdProgress.Value = "0"
                        'End If

                    End If

                    If Not Session("sDSSearchResultsForNonPreferred") Is Nothing And Session("sNonPreferred") Is Nothing Then
                        DSSearchResults.Merge(Session("sDSSearchResultsForNonPreferred"), True, MissingSchemaAction.Add)
                        DSSearchResults.AcceptChanges()
                        Session("sDSSearchResults") = DSSearchResults
                        Session("sNonPreferred") = "1"
                        Timer1.Enabled = False
                        'If Not Session("sDSSearchResults") Is Nothing Then
                        '    Dim ds As DataSet
                        '    ds = Session("sDSSearchResults")
                        '    If ds.Tables(0).Rows.Count = 0 Then
                        '        hdProgress.Value = "1"
                        '    Else
                        '        hdProgress.Value = "0"
                        '    End If

                        '    BindThreadResult()
                        '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterThreadUpdate();", True)
                        'Else
                        '    dvhotnoshow.Style.Add("display", "none")
                        '    hdProgress.Value = "0"
                        'End If

                    End If
                End If
                If strShowNotColumbusRate = "YES" Then
                    If Not Session("sDSSearchResultsForOneDMCDynamic") Is Nothing And Session("sOneDMCDynamic") Is Nothing Then
                        DSSearchResults.Merge(Session("sDSSearchResultsForOneDMCDynamic"), True, MissingSchemaAction.Add)
                        DSSearchResults.AcceptChanges()
                        Session("sDSSearchResults") = DSSearchResults
                        Session("sOneDMCDynamic") = "1"
                        Timer1.Enabled = False

                    End If
                End If

                If Not Session("sDSSearchResults") Is Nothing Then
                    Dim ds As DataSet
                    ds = Session("sDSSearchResults")
                    If ds.Tables(0).Rows.Count = 0 Then
                        hdProgress.Value = "1"
                    Else
                        hdProgress.Value = "0"
                    End If

                    BindThreadResult()
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterThreadUpdate();", True)
                Else
                    dvhotnoshow.Style.Add("display", "none")
                    hdProgress.Value = "0"
                End If

                'If Not Session("sDSSearchResultsForPreferred") Is Nothing And Session("sDSSearchResultsForNonPreferred") Is Nothing And Session("sPreferred") Is Nothing Then
                '    Session("sDSSearchResults") = Session("sDSSearchResultsForPreferred")
                '    Session("sPreferred") = "1"
                '    Timer1.Enabled = False
                '    If Not Session("sDSSearchResults") Is Nothing Then
                '        Dim ds As DataSet
                '        ds = Session("sDSSearchResults")
                '        If ds.Tables(0).Rows.Count = 0 Then
                '            hdProgress.Value = "1"
                '        Else
                '            hdProgress.Value = "0"
                '        End If

                '        BindThreadResult()
                '        ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterThreadUpdate();", True)
                '    Else
                '        dvhotnoshow.Style.Add("display", "none")
                '        hdProgress.Value = "0"
                '    End If

                'ElseIf Session("sDSSearchResultsForPreferred") Is Nothing And Not Session("sDSSearchResultsForNonPreferred") Is Nothing And Session("sNonPreferred") Is Nothing Then
                '    Session("sDSSearchResults") = Session("sDSSearchResultsForNonPreferred")
                '    Session("sNonPreferred") = "0"
                '    Timer1.Enabled = False
                '    If Not Session("sDSSearchResults") Is Nothing Then
                '        BindThreadResult()
                '        ScriptManager.RegisterStartupScript(Page, GetType(Page), "call-dynamic", "IncludeScriptAfterThreadUpdate();", True)
                '    Else
                '        dvhotnoshow.Style.Add("display", "none")
                '    End If
                '    hdProgress.Value = "0"
                'End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: Timer1_Tick :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub

    Protected Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (Session("sPreferred") Is Nothing And strShowColumbusRate = "YES") Then
                Timer1.Enabled = True
                hdProgressTimer2.Value = "0"
            End If
            If Session("sNonPreferred") Is Nothing And strShowColumbusRate = "YES" Then
                Timer1.Enabled = True
                hdProgressTimer2.Value = "0"
            End If
            If Session("sOneDMCDynamic") Is Nothing And strShowNotColumbusRate = "YES" Then
                Timer1.Enabled = True
                hdProgressTimer2.Value = "0"
            End If
            'If Not Session("sDSSearchResultsForPreferred") Is Nothing And Not Session("sDSSearchResultsForNonPreferred") Is Nothing Then
            '    Timer1.Enabled = True
            '    hdProgressTimer2.Value = "0"
            'ElseIf Not Session("sDSSearchResultsForPreferred") Is Nothing And Session("sDSSearchResultsForNonPreferred") Is Nothing And Session("sPreferred") Is Nothing Then
            '    Timer1.Enabled = True
            '    hdProgressTimer2.Value = "0"

            'ElseIf Session("sDSSearchResultsForPreferred") Is Nothing And Not Session("sDSSearchResultsForNonPreferred") Is Nothing And Session("sNonPreferred") Is Nothing Then
            '    Timer1.Enabled = True
            '    hdProgressTimer2.Value = "0"
            'ElseIf Session("sDSSearchResultsForPreferred") Is Nothing And Not Session("sDSSearchResultsForNonPreferred") Is Nothing And Session("sNonPreferred") Is Nothing Then
            '    Timer1.Enabled = True
            '    hdProgressTimer2.Value = "0"
            'End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: Timer2_Tick :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' BindThreadResult
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindThreadResult()
        Dim dsSearchResultsThread As DataSet
        dsSearchResultsThread = Session("sDSSearchResults")
        If Not dsSearchResultsThread Is Nothing Then


            If dsSearchResultsThread.Tables.Count > 0 Then
                dvSearchContent.Visible = True
                dvPager.Visible = True
                Dim dvMaiDetails As DataView = New DataView(dsSearchResultsThread.Tables(0))
                Dim recordCount As Integer = dvMaiDetails.Count

                Session("sMailBoxPageIndex") = "1"
                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "partyname ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "minprice ASC"
                ElseIf ddlSorting.Text = "0" Then
                    dvMaiDetails.Sort = "Preferred  DESC,minprice ASC"
                ElseIf ddlSorting.Text = "Rating" Then
                    dvMaiDetails.Sort = "noofstars DESC,partyname ASC "
                ElseIf ddlSorting.Text = "Preferred" Then
                    dvMaiDetails.Sort = "Preferred  DESC,partyname ASC "
                End If


                BindHotelMainDetails(dvMaiDetails)
                '  Me.PopulatePager(recordCount)

                '  Session("sdtRoomType") = dsSearchResultsThread.Tables(1)
                BindPricefilter(dsSearchResultsThread.Tables(0).DefaultView.ToTable(True, "minprice"))
                BindHotelStars(dsSearchResultsThread.Tables(0).DefaultView.ToTable(True, "catcode", "catname"))
                BindDestName(dsSearchResultsThread.Tables(0).DefaultView.ToTable(True, "sectorcode", "sectorname"))
                BindPropertyType(dsSearchResultsThread.Tables(0).DefaultView.ToTable(True, "propertytype", "propertytypename"))
                BindRoomClassification(dsSearchResultsThread.Tables(1))
                '  lblHotelCount.Text = dsSearchResultsThread.Tables(0).Rows.Count & " Records Found"
                dvhotnoshow.Style.Add("display", "none")
            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
            End If
        End If
    End Sub
    ''' <summary>
    ''' BindThreadResultForPageLoad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindThreadResultForPageLoad()

        Dim dsSearchResults As DataSet
        dsSearchResults = Session("sDSSearchResults")


        If Not dsSearchResults Is Nothing Then
            If dsSearchResults.Tables(0).Rows.Count = 0 Then
                dvhotnoshow.Style.Add("display", "block")
                showalternativedates(txtHotelCode.Text) ''Added shahul 26/07/18
            Else
                dvhotnoshow.Style.Add("display", "none")
            End If
            If dsSearchResults.Tables.Count > 0 Then

                BindPricefilter(dsSearchResults.Tables(0).DefaultView.ToTable(True, "minprice"))
                BindHotelStars(dsSearchResults.Tables(0).DefaultView.ToTable(True, "catcode", "catname"))
                BindDestName(dsSearchResults.Tables(0).DefaultView.ToTable(True, "sectorcode", "sectorname"))
                BindPropertyType(dsSearchResults.Tables(0).DefaultView.ToTable(True, "propertytype", "propertytypename"))
                BindRoomClassification(dsSearchResults.Tables(1))
                Session("sDSSearchResults") = dsSearchResults
                Session("sMailBoxPageIndex") = "1"
                Dim dvMaiDetails As DataView = New DataView(dsSearchResults.Tables(0))
                If ddlSorting.Text = "Name" Then
                    dvMaiDetails.Sort = "partyname ASC"
                ElseIf ddlSorting.Text = "Price" Then
                    dvMaiDetails.Sort = "minprice ASC"
                ElseIf ddlSorting.Text = "0" Then
                    dvMaiDetails.Sort = "Preferred  DESC,minprice ASC"
                ElseIf ddlSorting.Text = "Rating" Then
                    dvMaiDetails.Sort = "noofstars DESC,partyname ASC "
                ElseIf ddlSorting.Text = "Preferred" Then
                    dvMaiDetails.Sort = "Preferred  DESC,partyname ASC "
                End If





                Dim recordCount As Integer = dvMaiDetails.Count
                BindHotelMainDetails(dvMaiDetails)
                '   Me.PopulatePager(recordCount)
                '  Session("sdtRoomType") = dsSearchResults.Tables(1)

                lblHotelCount.Text = dsSearchResults.Tables(0).Rows.Count & " Records Found"
                If dsSearchResults.Tables(0).Rows.Count = 0 Then
                    If Not Page.Request.UrlReferrer Is Nothing Then
                        Dim previousPage As String = Page.Request.UrlReferrer.ToString
                        If previousPage.Contains("Home.aspx") Then

                            If Not Session("sEditRequestId") Is Nothing Or Not Session("sRequestId") Is Nothing Then
                                Dim sStatus As Boolean
                                sStatus = CheckOldBooking(Session("sRequestId"))
                                If sStatus = False Then
                                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please complete previous booking or abandon from My bookings link.")
                                    ' Exit Sub
                                End If
                            End If

                        End If

                    End If
                End If



            Else
                hdPriceMinRange.Value = "0"
                hdPriceMaxRange.Value = "1"
                If Not Page.Request.UrlReferrer Is Nothing Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    If previousPage.Contains("Home.aspx") Then

                        If Not Session("sEditRequestId") Is Nothing Or Not Session("sRequestId") Is Nothing Then
                            Dim sStatus As Boolean
                            sStatus = CheckOldBooking(Session("sRequestId"))
                            If sStatus = False Then
                                MessageBox.ShowMessage(Page, MessageType.Warning, "Please complete previous booking or abandon from My bookings link.")
                                Exit Sub
                            End If
                        End If

                    End If

                End If

            End If
        End If

    End Sub
    ''' <summary>
    ''' btnResetForClear_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub btnResetForClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetForClear.Click
        dlHotelsSearchResults.DataBind()
        rptPager.DataBind()
        chkHotelStars.Items.Clear()
        chkSectors.Items.Clear()
        chkPropertyType.Items.Clear()
        chkRoomClassification.Items.Clear()
        Session("sDSSearchResults") = Nothing

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlEvents_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnPreHotelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreHotelSave.Click
        Try

            objBLLHotelSearch = New BLLHotelSearch

            objBLLHotelSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLHotelSearch.CustomerCode, Session("sAgentCode"))
            objBLLHotelSearch.OBdiv_code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
            objBLLHotelSearch.CustomerCode = txtPreHotelCustomercode.Text
            objBLLHotelSearch.SourceCountryCode = txtPreHotelSourceCountryCode.Text
            objBLLHotelSearch.OBsourcectrycode = objBLLHotelSearch.SourceCountryCode
            objBLLHotelSearch.OBreqoverride = IIf(hdOveride.Value = "1", "1", "0")
            objBLLHotelSearch.OBagentref = ""
            objBLLHotelSearch.OBcolumbusref = ""
            objBLLHotelSearch.OBremarks = ""
            objBLLHotelSearch.userlogged = Session("GlobalUserName")

            objBLLHotelSearch.PreShowHotel = IIf(chkshowhotel.Checked = True, "1", "0")  '' Added shahul 10/11/18

            'changed by mohamed on 09/04/2018
            If chkShiftingPreArranged.Checked = True Then  '*********Abin commented for test
                objBLLHotelSearch.PreArrangedShifting = "1"
                Dim strShiftCode As String() = txtShiftHotelCodePreArranged.Text.Trim.Split("|")
                objBLLHotelSearch.PreArrangedShiftingCode = strShiftCode(0)
                objBLLHotelSearch.PreArrangedShiftingLineNo = strShiftCode(1)
            Else
                objBLLHotelSearch.PreArrangedShifting = "0"
                objBLLHotelSearch.PreArrangedShiftingCode = ""
            End If

            ' Modified by abin on 23/04/2018 --- Pax validation for Shifting Hotel -- Start
            If chkShiftingPreArranged.Checked = True Then
                Dim iShiftAdults As Integer = 0
                Dim iShiftChild As Integer = 0
                If txtShiftHotelCodePreArranged.Text <> "" Then
                    For Each dlShiftHotelBreakItem As DataListItem In dlShiftHotelBreakPreArranged.Items
                        Dim chkSelect As CheckBox = dlShiftHotelBreakItem.FindControl("chkSelect")
                        If chkSelect.Checked = True Then
                            Dim lblAdults As Label = dlShiftHotelBreakItem.FindControl("lblAdults")
                            Dim lblChild As Label = dlShiftHotelBreakItem.FindControl("lblChild")
                            iShiftAdults = iShiftAdults + Val(lblAdults.Text)
                            iShiftChild = iShiftChild + Val(lblChild.Text)
                        End If
                    Next

                    Dim iCurrentAdults As Integer = ddlPreHotelAdult.SelectedValue
                    Dim iCurrentChild As Integer = ddlPreHotelChild.SelectedValue

                    If iCurrentAdults > iShiftAdults And iCurrentChild > iShiftChild Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "  Booking adult(" & iShiftAdults.ToString & ") and child(" & iShiftChild.ToString & ") pax not tally with selected adult and child.  selected adult and child should be less than or equal to booking adult and child.")
                        imgHotelthreadLoading.Visible = False
                        Exit Sub
                    ElseIf iCurrentAdults > iShiftAdults Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Booking adult pax(" & iShiftAdults.ToString & ") not tally with selected adult.  selected adult should be less than or equal booking adult.")
                        imgHotelthreadLoading.Visible = False
                        Exit Sub
                    ElseIf iCurrentChild > iShiftChild Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Booking child pax(" & iShiftChild.ToString & ") not tally with selected child.  selected child should be less than or equal booking child.")
                        imgHotelthreadLoading.Visible = False
                        Exit Sub
                    End If

                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select shifting hotel.")
                    imgHotelthreadLoading.Visible = False
                    Exit Sub
                End If
            End If

            ' Modified by abin on 23/04/2018 --- Pax validation for Shifting Hotel -- End


            objBLLHotelSearch.OBrequestid = GetNewOrExistingRequestId()

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

            ' added by abin on 20181013 -- start
            FindCumilative()
            Dim iCumulativeUser As Integer = 0
            If Session("sLoginType") = "RO" Then
                Dim strQuery As String = "select count(agentcode)CNT from agentmast(nolock) where bookingengineratetype='CUMULATIVE' and agentcode=(select min(agentcode) from  booking_header(nolock) where requestid='" & objBLLHotelSearch.OBrequestid & "')"
                iCumulativeUser = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

            End If

            'changed by mohamed on 29/08/2018
            Dim lsSqlQry As String = ""
            Dim lsFutureDateAvailable As String = ""
            If hdBookingEngineRateType.Value = "1" Or iCumulativeUser > 0 Then
                If txtShiftHotelCodePreArranged.Text.Trim = "" Then 'And ViewState("vRLineNo") Is Nothing Then
                    Dim dsFt As DataSet
                    lsSqlQry = "execute sp_check_future_checkin_booking '" & objBLLHotelSearch.OBrequestid & "'," & strHotelLineNo & ",'" & objBLLHotelSearch.PreHotelCheckIn & "','" & objBLLHotelSearch.PreHotelCheckout & "',''"
                    dsFt = objclsUtilities.GetDataFromDataset(lsSqlQry)
                    If dsFt.Tables.Count > 0 Then
                        If dsFt.Tables(0).Rows.Count >= 1 Then
                            lsFutureDateAvailable = dsFt.Tables(0).Rows(0)("AvailStatus")
                            If lsFutureDateAvailable.Trim <> "" Then
                                'MessageBox.ShowMessage(Page, MessageType.Errors, "Shifting/Add More Room in current hotel is not selected when additional hotel is added for future date")
                                MessageBox.ShowMessage(Page, MessageType.Errors, "Check In / Check out should be related to earlier hotel booked")
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
            ' added by abin on 20181013 -- end


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
                Response.Redirect("MoreServices.aspx")
            Else
                MessageBox.ShowMessage(Page, MessageType.Errors, "Saved faild.")
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPreHotelSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub FillPreArrangedHotelDetailsBasedonNewOrEditMode()

        If Session("sRequestId") Is Nothing Then ' Fresh Mode
            If Not Session("sobjBLLHotelSearch") Is Nothing Then
                objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
                txtPreHotelSourceCountry.Text = objBLLHotelSearch.SourceCountry
                txtPreHotelSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode
                txtPreHotelCustomer.Text = objBLLHotelSearch.Customer
                txtPreHotelCustomercode.Text = objBLLHotelSearch.CustomerCode
                'changed by abin / mohamed on 11/04/2018
                'If Session("sLoginType") = "RO" Then
                '    txtPreHotelCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                '    txtPreHotelSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                '    AutoCompleteExtender_txtPreHotelCustomer.Enabled = False
                '    AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
                'End If
            End If

        ElseIf Not Session("sRequestId") Is Nothing And Not Request.QueryString("PLineNo") Is Nothing Then
            Dim dt As DataTable
            dt = objBLLHotelSearch.PreHotelSummary(Session("sRequestId"), RLineNo:=Request.QueryString("PLineNo"))

            If dt.Rows.Count > 0 Then ' if hotel is exist
                If Not Session("sRequestId") Is Nothing And Not Session("sEditRequestId") Is Nothing And Not Request.QueryString("PLineNo") Is Nothing Then ' Amend Mode
                    FillBookingPreArrangedDetailsForEdit(dt)
                ElseIf Not Session("sRequestId") Is Nothing And Not Session("sEditRequestId") Is Nothing And Request.QueryString("PLineNo") Is Nothing Then ' Amend with New Mode
                    BindPreArrangedAgentCountryDetails(dt)
                    chkShiftingPreArranged.Checked = True 'changed by mohamed on 11/04/2018

                ElseIf Not Session("sRequestId") Is Nothing And Session("sEditRequestId") Is Nothing And Not Request.QueryString("PLineNo") Is Nothing Then ' Edit mode
                    FillBookingPreArrangedDetailsForEdit(dt)
                ElseIf Not Session("sRequestId") Is Nothing And Session("sEditRequestId") Is Nothing And Request.QueryString("PLineNo") Is Nothing Then ' New mode
                    BindPreArrangedAgentCountryDetails(dt)
                    chkShiftingPreArranged.Checked = True 'changed by mohamed on 11/04/2018
                End If
            Else 'if hotel is not exist
                If Not Session("sobjBLLHotelSearch") Is Nothing Then
                    objBLLHotelSearch = CType(Session("sobjBLLHotelSearch"), BLLHotelSearch)
                    txtPreHotelSourceCountry.Text = objBLLHotelSearch.SourceCountry
                    txtPreHotelSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode
                    txtPreHotelCustomer.Text = objBLLHotelSearch.Customer
                    txtPreHotelCustomercode.Text = objBLLHotelSearch.CustomerCode
                    If Session("sLoginType") = "RO" Then
                        txtPreHotelCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                        txtPreHotelSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                        AutoCompleteExtender_txtPreHotelCustomer.Enabled = False
                        AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
                    End If
                End If

            End If

            'changed by abin / mohamed on 11/04/2018
            txtPreHotelCustomer.Attributes.Add("readonly", True)
            txtPreHotelSourceCountry.Attributes.Add("readonly", True)
        Else
            'changed by Abin
            Dim dtBookingHeader As DataTable
            dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dtBookingHeader.Rows.Count > 0 Then
                txtPreHotelSourceCountry.Text = dtBookingHeader.Rows(0)("sourcectryname").ToString
                txtPreHotelSourceCountryCode.Text = dtBookingHeader.Rows(0)("sourcectrycode").ToString
                txtPreHotelCustomer.Text = dtBookingHeader.Rows(0)("agentname").ToString
                txtPreHotelCustomercode.Text = dtBookingHeader.Rows(0)("agentcode").ToString
                If Session("sLoginType") = "RO" Then
                    txtPreHotelCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtPreHotelSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtPreHotelCustomer.Enabled = False
                    AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
                End If
                chkShiftingPreArranged.Checked = True 'changed by mohamed on 11/04/2018

                'changed by mohamed on 11/04/2018
                txtPreHotelCustomer.Attributes.Add("readonly", True)
                txtPreHotelSourceCountry.Attributes.Add("readonly", True)
            End If
        End If

    End Sub

    Private Sub FillBookingPreArrangedDetailsForEdit(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            hdOPModePreArranged.Value = "Edit"
            chkShiftingPreArranged.Enabled = False

            txtPreHotelFromDate.Text = dt.Rows(0)("checkindate").ToString
            txtPreHotelToDate.Text = dt.Rows(0)("checkoutdate").ToString
            txtPreHotel.Text = dt.Rows(0)("partyname").ToString
            txtPreHotelCode.Text = dt.Rows(0)("partycode").ToString
            txtUAELocation.Text = dt.Rows(0)("othtypname").ToString
            txtUAELocationCode.Text = dt.Rows(0)("sectorcode").ToString
            txtPreHotelSourceCountry.Text = dt.Rows(0)("ctryname").ToString
            txtPreHotelSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
            txtPreHotelCustomercode.Text = dt.Rows(0)("agentcode").ToString
            txtPreHotelCustomer.Text = dt.Rows(0)("agentname").ToString
            Dim liAllRoomsRequired As Integer = 0

            chkshowhotel.Checked = IIf(dt.Rows(0)("showhotel").ToString = "1", True, False) '' added shahul 10/11/18

            If dt.Rows(0)("shiftto").ToString = "1" Then
                dvShiftingPreArranged.Attributes.Add("style", "display:block;")
                Dim lsShiftFromPartyCode As String = "", lsShiftPartycode As String = "", lsShiftLineNo As String = ""
                lsShiftFromPartyCode = dt.Rows(0)("ShiftFromPartyCode").ToString
                Dim lsShiftFromPartyCodeWithLines As String() = lsShiftFromPartyCode.Split(";")
                If lsShiftFromPartyCodeWithLines.Count >= 1 Then
                    If lsShiftFromPartyCodeWithLines.Count >= 2 Then
                        liAllRoomsRequired = 1
                    End If
                    For li = lsShiftFromPartyCodeWithLines.GetLowerBound(0) To lsShiftFromPartyCodeWithLines.GetUpperBound(0)
                        If lsShiftFromPartyCodeWithLines(li) <> "" Then
                            Dim lsShiftSinglePartyCodeLineNo As String() = lsShiftFromPartyCodeWithLines(li).Split("|")
                            If lsShiftSinglePartyCodeLineNo.Count >= 1 Then
                                If lsShiftPartycode = "" Then
                                    lsShiftPartycode = lsShiftSinglePartyCodeLineNo(0)
                                End If
                                lsShiftLineNo += IIf(lsShiftLineNo = "", "", ",") & lsShiftSinglePartyCodeLineNo(1)
                            End If
                        End If
                    Next
                End If
                chkShiftingPreArranged.Checked = True
                txtShiftHotelCodePreArranged.Text = lsShiftPartycode & "|" & lsShiftLineNo

                Dim lsstrQuery As String = "select partyname from partymast (nolock) where partycode='" & lsShiftPartycode & "'"
                txtShiftHotelPreArranged.Text = IIf(liAllRoomsRequired = 1, "All Rooms : ", "Room : ") & objclsUtilities.ExecuteQueryReturnStringValue(lsstrQuery)
                txtShiftHotelPreArranged.Text += " - " & txtPreHotelFromDate.Text & " - " & txtPreHotelToDate.Text
            Else
                dvShiftingPreArranged.Attributes.Add("style", "display:none;")
                chkShiftingPreArranged.Checked = False
                txtShiftHotelCodePreArranged.Text = ""
                txtShiftHotelPreArranged.Text = ""
            End If

            If Session("sLoginType") = "RO" Then
                txtPreHotelCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                txtPreHotelSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtPreHotelCustomer.Enabled = False
                AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
            End If

            ddlPreHotelAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)


            If Val(dt.Rows(0)("child").ToString) <> 0 Then
                ddlPreHotelChild.SelectedValue = dt.Rows(0)("child").ToString

                Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                If Left(childages, 1) = ";" Then
                    childages = Right(childages, (childages.Length - 1))
                End If
                Dim strChildAges As String() = childages.ToString.Split(";")

                If strChildAges.Length <> 0 Then
                    txtPreHotelChild1.Text = strChildAges(0)
                End If

                If strChildAges.Length > 1 Then
                    txtPreHotelChild2.Text = strChildAges(1)
                End If
                If strChildAges.Length > 2 Then
                    txtPreHotelChild3.Text = strChildAges(2)
                End If
                If strChildAges.Length > 3 Then
                    txtPreHotelChild4.Text = strChildAges(3)
                End If
                If strChildAges.Length > 4 Then
                    txtPreHotelChild5.Text = strChildAges(4)
                End If
                If strChildAges.Length > 5 Then
                    txtPreHotelChild6.Text = strChildAges(5)
                End If
                If strChildAges.Length > 6 Then
                    txtPreHotelChild7.Text = strChildAges(6)
                End If
                If strChildAges.Length > 7 Then
                    txtPreHotelChild8.Text = strChildAges(7)
                End If
            End If
        End If
        sbBindShiftHotelDetailPreArranged(False) 'changed by mohamed on 06/09/2018
    End Sub

    Private Sub BindPreArrangedAgentCountryDetails(ByVal dt As DataTable)
        Dim dtBookingHeader As DataTable
        dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
        If dtBookingHeader.Rows.Count > 0 Then
            txtPreHotelSourceCountry.Text = dtBookingHeader.Rows(0)("sourcectryname").ToString
            txtPreHotelSourceCountryCode.Text = dtBookingHeader.Rows(0)("sourcectrycode").ToString
            txtPreHotelCustomer.Text = dtBookingHeader.Rows(0)("agentname").ToString
            txtPreHotelCustomercode.Text = dtBookingHeader.Rows(0)("agentcode").ToString
            If Session("sLoginType") = "RO" Then
                txtPreHotelCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                txtPreHotelSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                AutoCompleteExtender_txtPreHotelCustomer.Enabled = False
                AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
            End If
        Else
            If dt.Rows.Count > 0 Then
                txtPreHotelSourceCountry.Text = dt.Rows(0)("ctryname").ToString
                txtPreHotelSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                txtPreHotelCustomercode.Text = dt.Rows(0)("agentcode").ToString
                txtPreHotelCustomer.Text = dt.Rows(0)("agentname").ToString
                If Session("sLoginType") = "RO" Then
                    txtPreHotelCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtPreHotelSourceCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtPreHotelCustomer.Enabled = False
                    AutoCompleteExtender_txtPreHotelSourceCountry.Enabled = False
                End If
            End If
        End If
    End Sub

    Protected Sub btnSelectShiftHotel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectShiftHotel.Click
        sbBindShiftHotelDetail(True) 'changed by mohamed on 06/09/2018
    End Sub

    Sub sbBindShiftHotelDetail(ByVal abNeedToShowModal As Boolean)
        'changed by mohamed on 05/04/2018
        Try
            Dim lsRlineno As String = -1
            Dim strSqlQry As String = ""
            Dim myDS As New DataSet
            Dim Hotelnames As New List(Of String)
            If Request.QueryString("RLineNo") IsNot Nothing Then
                lsRlineno = Request.QueryString("RLineNo")
            End If
            strSqlQry = "execute sp_get_shifting_hotel_detail '" & Session("sRequestId").ToString & "', " & lsRlineno
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            'changed by mohamed on 29/08/2018
            btnShiftHotelSave.Style.Remove("Float")
            If chkShifting.Checked = True Then
                lblShiftingMessageToUser.Visible = False
                btnShiftHotelSave.Style.Add("Float", "right")
            Else
                lblShiftingMessageToUser.Visible = True
                btnShiftHotelSave.Style.Add("Float", "left")
            End If

            If myDS.Tables(0).Rows.Count > 0 Then
                'For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                '    'Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString()))
                '    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString() + "|" + Replace(myDS.Tables(0).Rows(i)("roomstring").ToString(), "|", "&s") + "**" + myDS.Tables(0).Rows(i)("checkout").ToString()))
                'Next
                dlShiftHotelBreak.DataSource = myDS.Tables(0)
                dlShiftHotelBreak.DataBind()
            End If

            If abNeedToShowModal = True Then
                mpShiftHotel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPreHotelSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnShiftHotelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShiftHotelSave.Click
        Try
            Dim lsHotelName As String = "", lsPartyName As String = ""
            Dim lsPartyCode As String = "", lsCheckOut As String = "", lsRlineNo As String = "", lsRoomString As String = "", liNoOfRooms As Integer = 0
            Dim liNights As Integer = 0, lsCheckIn As String = ""
            Dim lbSelectedAnyLine As Boolean = False
            For Each dlShiftHotelBreakItem As DataListItem In dlShiftHotelBreak.Items
                Dim chkSelect As CheckBox = dlShiftHotelBreakItem.FindControl("chkSelect")
                Dim lblPartyCode As Label = dlShiftHotelBreakItem.FindControl("lblPartyCode")
                Dim lblCheckout As Label = dlShiftHotelBreakItem.FindControl("lblCheckout")
                Dim lblRlineNo As Label = dlShiftHotelBreakItem.FindControl("lblRlineNo")
                Dim lblRoomString As Label = dlShiftHotelBreakItem.FindControl("lblRoomString")
                Dim lblNoOfRooms As Label = dlShiftHotelBreakItem.FindControl("lblNoOfRooms")
                Dim lblHotelName As Label = dlShiftHotelBreakItem.FindControl("lblHotelName")
                Dim lblCheckIn As Label = dlShiftHotelBreakItem.FindControl("lblCheckIn")
                Dim lblnights As Label = dlShiftHotelBreakItem.FindControl("lblnights")
                Dim lblpartyname As Label = dlShiftHotelBreakItem.FindControl("lblpartyname")

                If chkSelect.Checked = True Then
                    lbSelectedAnyLine = True
                    If lsPartyCode = "" Then
                        lsPartyCode = lblPartyCode.Text
                        lsCheckOut = lblCheckout.Text
                        lsCheckIn = lblCheckIn.Text
                        liNights = Val(lblnights.Text)
                        lsPartyName = lblpartyname.Text
                    ElseIf lblPartyCode.Text.Trim.ToUpper <> lsPartyCode.Trim Or lblCheckout.Text.Trim.ToUpper <> lsCheckOut.Trim Then
                        MessageBox.ShowMessage(Page, MessageType.Errors, "Multiple Hotel or different check out cannot be selected")
                        mpShiftHotel.Show()
                        GoTo lblExitPos
                    End If
                    Dim lsRLineNos As String(), lsRLineNoToCheck As String
                    lsRLineNos = lblRlineNo.Text.Split(",")
                    For li = LBound(lsRLineNos) To UBound(lsRLineNos)
                        lsRLineNoToCheck = lsRLineNos(li)
                        If InStr(lsRLineNoToCheck, "-") > 0 Then
                            lsRLineNoToCheck = Left(lsRLineNoToCheck, InStr(lsRLineNoToCheck, "-") - 1)
                        End If
                        If InStr("," + lsRlineNo + ",", "," + lsRLineNoToCheck + ",") > 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Errors, "The same line has been selected multiple time, please remove in one place")
                            mpShiftHotel.Show()
                            GoTo lblExitPos
                        End If
                    Next
                    lsHotelName = lsHotelName & IIf(lsHotelName = "", "", ";") & lblHotelName.Text
                    lsRlineNo = lsRlineNo & IIf(lsRlineNo = "", "", ",") & lblRlineNo.Text
                    lsRoomString = lsRoomString & IIf(lsRoomString = "", "", ";") & lblRoomString.Text
                    liNoOfRooms += Val(lblNoOfRooms.Text)
                End If
            Next

            lsRoomString = fnReArrangeRoomString(lsRoomString)
            Dim javaScriptChldrn1 As String
            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            If lbSelectedAnyLine = True Then
                'txtShiftHotel.Text = lsHotelName
                'txtShiftHotelCode.Text = lsPartyCode & "|" & lsRlineNo
                'txtCheckIn.Enabled = True
                'Dim strScript As String = "javascript: ShiftingCallSuccess(""" & lsRoomString & "**" & lsCheckOut & """);"
                'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)
                'Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

                txtCheckIn.Enabled = True
                txtShiftHotel.Text = lsHotelName
                txtShiftHotelCode.Text = lsPartyCode & "|" & lsRlineNo
                If chkShifting.Checked = False Then
                    txtCheckIn.Text = lsCheckIn
                    txtCheckOut.Text = lsCheckOut
                    txtHotelCode.Text = lsPartyCode
                    txtHotelName.Text = lsPartyName
                    txtNoOfNights.Text = liNights
                    javaScriptChldrn1 = "<script type='text/javascript'> ShowAdultChild(); fnLockControlsForShifting(); GetHotelsDetails('" & lsPartyCode & "'); </script>"
                Else
                    txtCheckIn.Text = lsCheckOut
                    javaScriptChldrn1 = "<script type='text/javascript'> ShowAdultChild(); fnLockControlsForShifting();</script>"
                End If
                ddlRoom_Dynamic.SelectedValue = liNoOfRooms
                FillShiftingRoomAdultChild(lsRoomString)

                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
            Else
                txtShiftHotel.Text = ""
                txtShiftHotelCode.Text = ""

                'changed by mohamed on 11/04/2018
                javaScriptChldrn1 = "<script type='text/javascript'>fnLockControlsForShifting();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
            End If

            Exit Sub
lblExitPos:

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPreHotelSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub dlShiftHotelBreak_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlShiftHotelBreak.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblRlineNo As Label = CType(e.Item.FindControl("lblRlineNo"), Label)
                Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
                Dim lsShiftDet As String() = txtShiftHotelCode.Text.Split("|")
                If lsShiftDet.GetUpperBound(0) >= 1 Then
                    Dim lsRlineNos As String() = lsShiftDet(1).Split(",")
                    For li = lsRlineNos.GetLowerBound(0) To lsRlineNos.GetUpperBound(0)
                        If lsRlineNos(li) = lblRlineNo.Text Then
                            chkSelect.Checked = True
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlShiftHotelBreak_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Function fnReArrangeRoomString(ByVal lsRoomString As String) As String
        Dim lsRoomStringNew As String = ""
        Dim lsRoomStrInd As String = ""
        Dim lsRoomStrArr As String() = lsRoomString.Split(";")
        Dim liRoomNo As Integer = 0
        For li As Integer = lsRoomStrArr.GetLowerBound(0) To lsRoomStrArr.GetUpperBound(0)
            liRoomNo += 1
            lsRoomStrInd = lsRoomStrArr(li)
            If lsRoomStrInd.Trim <> "" Then
                lsRoomStrInd = liRoomNo & Mid(lsRoomStrInd, InStr(1, lsRoomStrInd, ","))
                lsRoomStringNew = lsRoomStringNew & IIf(lsRoomStringNew = "", "", ";") & lsRoomStrInd
            End If
        Next

        Return lsRoomStringNew

    End Function

    Protected Sub btnSelectShiftHotelPreArranged_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectShiftHotelPreArranged.Click
        sbBindShiftHotelDetailPreArranged(True) 'changed by mohamed on 06/09/2018
    End Sub

    Sub sbBindShiftHotelDetailPreArranged(ByVal abNeedToShowModal As Boolean)
        'changed by mohamed on 05/04/2018
        Try
            Dim lsRlineno As String = -1
            Dim strSqlQry As String = ""
            Dim myDS As New DataSet
            Dim Hotelnames As New List(Of String)
            If Request.QueryString("RLineNo") IsNot Nothing Then
                lsRlineno = Request.QueryString("RLineNo")
            End If
            ''Added shahul 10/11/18
            If Request.QueryString("PLineNo") IsNot Nothing And lsRlineno = -1 Then
                lsRlineno = Request.QueryString("PLineNo")
            End If
            strSqlQry = "execute sp_get_shifting_hotel_detail '" & Session("sRequestId").ToString & "', " & lsRlineno
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            'changed by mohamed on 29/08/2018
            btnShiftHotelSavePreArranged.Style.Remove("Float")
            If chkShiftingPreArranged.Checked = True Then
                lblShiftingMessageToUserPreArranged.Visible = False
                btnShiftHotelSavePreArranged.Style.Add("Float", "right")
            Else
                lblShiftingMessageToUserPreArranged.Visible = True
                btnShiftHotelSavePreArranged.Style.Add("Float", "left")
            End If

            If myDS.Tables(0).Rows.Count > 0 Then
                'For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                '    'Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString()))
                '    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString() + "|" + Replace(myDS.Tables(0).Rows(i)("roomstring").ToString(), "|", "&s") + "**" + myDS.Tables(0).Rows(i)("checkout").ToString()))
                'Next
                dlShiftHotelBreakPreArranged.DataSource = myDS.Tables(0)
                dlShiftHotelBreakPreArranged.DataBind()
            End If
            If abNeedToShowModal = True Then
                mpShiftHotelPreArranged.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnSelectShiftHotelPreArranged_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub btnShiftHotelSavePreArranged_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShiftHotelSavePreArranged.Click
        Try
            Dim lsHotelName As String = "", lsPartyName As String = ""
            Dim lsPartyCode As String = "", lsCheckOut As String = "", lsRlineNo As String = "", lsRoomString As String = "", liNoOfRooms As Integer = 0
            Dim liNights As Integer = 0, lsCheckIn As String = ""
            Dim lbSelectedAnyLine As Boolean = False
            For Each dlShiftHotelBreakItem As DataListItem In dlShiftHotelBreakPreArranged.Items
                Dim chkSelect As CheckBox = dlShiftHotelBreakItem.FindControl("chkSelect")
                Dim lblPartyCode As Label = dlShiftHotelBreakItem.FindControl("lblPartyCode")
                Dim lblCheckout As Label = dlShiftHotelBreakItem.FindControl("lblCheckout")
                Dim lblRlineNo As Label = dlShiftHotelBreakItem.FindControl("lblRlineNo")
                Dim lblRoomString As Label = dlShiftHotelBreakItem.FindControl("lblRoomString")
                Dim lblNoOfRooms As Label = dlShiftHotelBreakItem.FindControl("lblNoOfRooms")
                Dim lblHotelName As Label = dlShiftHotelBreakItem.FindControl("lblHotelName")
                Dim lblCheckIn As Label = dlShiftHotelBreakItem.FindControl("lblCheckIn")
                Dim lblnights As Label = dlShiftHotelBreakItem.FindControl("lblnights")
                Dim lblpartyname As Label = dlShiftHotelBreakItem.FindControl("lblpartyname")

                If chkSelect.Checked = True Then
                    lbSelectedAnyLine = True
                    If lsPartyCode = "" Then
                        lsPartyCode = lblPartyCode.Text
                        lsCheckOut = lblCheckout.Text
                        lsCheckIn = lblCheckIn.Text
                        liNights = Val(lblnights.Text)
                        lsPartyName = lblpartyname.Text
                    ElseIf lblPartyCode.Text.Trim.ToUpper <> lsPartyCode.Trim Or lblCheckout.Text.Trim.ToUpper <> lsCheckOut.Trim Then
                        MessageBox.ShowMessage(Page, MessageType.Errors, "Multiple Hotel or different check out cannot be selected")
                        mpShiftHotelPreArranged.Show()
                        GoTo lblExitPos
                    End If
                    Dim lsRLineNos As String(), lsRLineNoToCheck As String
                    lsRLineNos = lblRlineNo.Text.Split(",")
                    For li = LBound(lsRLineNos) To UBound(lsRLineNos)
                        lsRLineNoToCheck = lsRLineNos(li)
                        If InStr(lsRLineNoToCheck, "-") > 0 Then
                            lsRLineNoToCheck = Left(lsRLineNoToCheck, InStr(lsRLineNoToCheck, "-") - 1)
                        End If
                        If InStr("," + lsRlineNo + ",", "," + lsRLineNoToCheck + ",") > 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Errors, "The same line has been selected multiple time, please remove in one place")
                            mpShiftHotelPreArranged.Show()
                            GoTo lblExitPos
                        End If
                    Next
                    lsHotelName = lsHotelName & IIf(lsHotelName = "", "", ";") & lblHotelName.Text
                    lsRlineNo = lsRlineNo & IIf(lsRlineNo = "", "", ",") & lblRlineNo.Text
                    lsRoomString = lsRoomString & IIf(lsRoomString = "", "", ";") & lblRoomString.Text
                    liNoOfRooms += Val(lblNoOfRooms.Text)
                End If
            Next

            lsRoomString = fnReArrangeRoomString(lsRoomString)

            If lbSelectedAnyLine = True Then
                'txtShiftHotel.Text = lsHotelName
                'txtShiftHotelCode.Text = lsPartyCode & "|" & lsRlineNo
                'txtCheckIn.Enabled = True
                'Dim strScript As String = "javascript: ShiftingCallSuccess(""" & lsRoomString & "**" & lsCheckOut & """);"
                'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)
                'Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

                txtPreHotelToDate.Enabled = True
                txtShiftHotelPreArranged.Text = lsHotelName
                txtShiftHotelCodePreArranged.Text = lsPartyCode & "|" & lsRlineNo
                If chkShiftingPreArranged.Checked = False Then
                    txtPreHotelFromDate.Text = lsCheckIn
                    txtPreHotelToDate.Text = lsCheckOut
                    txtPreHotelCode.Text = lsPartyCode
                    txtPreHotel.Text = lsPartyName
                    txtUAELocationCode.Text = ""
                    txtUAELocation.Text = ""

                    Dim dt As DataTable
                    dt = objclsUtilities.GetDataFromDataTable("select sectorcode, othtypname from partymast p (nolock), othtypmast o(nolock) where p.sectorcode=o.othtypcode and p.partycode='" & txtPreHotelCode.Text & "'")
                    If dt.Rows.Count > 0 Then
                        txtUAELocationCode.Text = dt.Rows(0)("sectorcode")
                        txtUAELocation.Text = dt.Rows(0)("othtypname")
                    End If

                    'javaScriptChldrn1 = "<script type='text/javascript'>ShowAdultChild(); GetHotelsDetails('" & lsPartyCode & "'); </script>"
                Else
                    txtPreHotelFromDate.Text = lsCheckOut
                    'javaScriptChldrn1 = "<script type='text/javascript'>ShowAdultChild();</script>"
                End If
                FillPreArrangedAdultChildFromRoomString(lsRoomString)
                'ddlRoom.SelectedValue = liNoOfRooms
                'FillShiftingRoomAdultChild(lsRoomString)
                'Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

                ''changed by mohamed on 11/04/2018
                'Dim lsStrScriptShift As String = "javascript: fnLockControlsForShifting();"
                'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", lsStrScriptShift, True)

            Else
                txtShiftHotelPreArranged.Text = ""
                txtShiftHotelCodePreArranged.Text = ""

                ''changed by mohamed on 11/04/2018
                'Dim lsStrScriptShift As String = "javascript: fnLockControlsForShifting();"
                'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", lsStrScriptShift, True)

            End If

            'changed by mohamed on 11/04/2018
            Dim javaScriptChldrn1 As String
            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            javaScriptChldrn1 = "<script type='text/javascript'>fnLockControlsForShifting();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

            Exit Sub
lblExitPos:

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnShiftHotelSavePreArranged_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()

        End Try
    End Sub

    Protected Sub dlShiftHotelBreakPreArranged_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlShiftHotelBreakPreArranged.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblRlineNo As Label = CType(e.Item.FindControl("lblRlineNo"), Label)
                Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
                Dim lsShiftDet As String() = txtShiftHotelCodePreArranged.Text.Split("|")
                If lsShiftDet.GetUpperBound(0) >= 1 Then
                    Dim lsRlineNos As String() = lsShiftDet(1).Split(",")
                    For li = lsRlineNos.GetLowerBound(0) To lsRlineNos.GetUpperBound(0)
                        If lsRlineNos(li) = lblRlineNo.Text Then
                            chkSelect.Checked = True
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlShiftHotelBreakPreArranged_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Sub FillPreArrangedAdultChildFromRoomString(ByVal lsRoomString As String)
        Dim liAdults As Integer = 0, liChild As Integer = 0, lsChildAge As String = ""
        Dim lsRooms As String() = lsRoomString.Split(";")
        For li = lsRooms.GetLowerBound(0) To lsRooms.GetUpperBound(0)
            Dim lsRoomDet As String() = Split(lsRooms(li) & ",", ",")
            If lsRoomDet.GetUpperBound(0) >= 3 Then
                liAdults += Val(lsRoomDet(1))
                liChild += Val(lsRoomDet(2))
                lsChildAge += IIf(lsChildAge = "", "", ",") & lsRoomDet(3)
            End If
        Next

        ddlPreHotelAdult.SelectedValue = liAdults
        If liChild <> 0 Then
            ddlPreHotelChild.SelectedValue = IIf(liChild > 6, 6, liChild)

            Dim childages As String = lsChildAge.Replace(",", ";").Replace("|", ";")
            If Left(childages, 1) = ";" Then
                childages = Right(childages, (childages.Length - 1))
            End If
            Dim strChildAges As String() = childages.ToString.Split(";")

            If strChildAges.Length <> 0 Then
                txtPreHotelChild1.Text = strChildAges(0)
            End If

            If strChildAges.Length > 1 Then
                txtPreHotelChild2.Text = strChildAges(1)
            End If
            If strChildAges.Length > 2 Then
                txtPreHotelChild3.Text = strChildAges(2)
            End If
            If strChildAges.Length > 3 Then
                txtPreHotelChild4.Text = strChildAges(3)
            End If
            If strChildAges.Length > 4 Then
                txtPreHotelChild5.Text = strChildAges(4)
            End If
            If strChildAges.Length > 5 Then
                txtPreHotelChild6.Text = strChildAges(5)
            End If
            If strChildAges.Length > 6 Then
                txtPreHotelChild7.Text = strChildAges(6)
            End If
            If strChildAges.Length > 7 Then
                txtPreHotelChild8.Text = strChildAges(7)
            End If
        End If
    End Sub

    Protected Sub btnAddChargeYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChargeYes.Click


        Dim lbAdditionalCharges As New LinkButton
        lbAdditionalCharges = CType(Session("slbAdditionalCharges"), LinkButton)

        Dim strNoOfExtrBed As String = txtNoOfExtraBed.Text
        Dim strExtraBedPrice As String = txtExtraBedUnitPrice.Text
        Dim strTotalExtraBedPrice As String = Val(strNoOfExtrBed) * Val(strExtraBedPrice)

        'Dim lbTotalPrice As New LinkButton
        'lbTotalPrice = CType(Session("slbTotalPrice"), LinkButton)

        Dim gvGridviewRow As GridViewRow = CType(lbAdditionalCharges.NamingContainer, GridViewRow)
        Dim _gvHotelRoomType As GridView = CType((gvGridviewRow.Parent.Parent), GridView)
        Dim dlRatePlanItem As DataListItem = CType(_gvHotelRoomType.NamingContainer, DataListItem)
        Dim _dlRatePlan As DataList = CType((dlRatePlanItem.Parent), DataList)
        Dim dlHotelsSearchResultsItem As DataListItem = CType(_dlRatePlan.NamingContainer, DataListItem)
        Dim _dlRatePlan1 As New DataList
        _dlRatePlan1 = CType(dlHotelsSearchResults.Items(dlHotelsSearchResultsItem.ItemIndex).FindControl("dlRatePlan"), DataList)
        Dim _gvHotelRoomType1 As New GridView
        _gvHotelRoomType1 = CType(_dlRatePlan1.Items(dlRatePlanItem.ItemIndex).FindControl("gvHotelRoomType"), GridView)
        Dim lblSaleValue As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lblSaleValue"), Label)
        Dim lbTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbTotalprice"), LinkButton)
        Dim lbwlTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbwlTotalprice"), LinkButton)

        Dim hdExtraBedRequired As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("hdExtraBedRequired"), Label)
        Dim hdExtraBedValue As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("hdExtraBedValue"), Label)
        hdExtraBedRequired.Text = "YES"
        hdExtraBedValue.Text = strNoOfExtrBed + "*" + strExtraBedPrice + "*" + lblEBBookingCode.Text
        Dim strTotal As String() = lbTotalprice1.Text.Split(" ")
        Dim strTotalPrice As String = Val(lblSaleValue.Text) + Val(strTotalExtraBedPrice)
        lbTotalprice1.Text = strTotalPrice + " " + strTotal(1)
        lbwlTotalprice1.Text = strTotalPrice + " " + strTotal(1)

    End Sub
    Protected Sub btnAddChargeNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChargeNo.Click

        Dim lbAdditionalCharges As New LinkButton
        lbAdditionalCharges = CType(Session("slbAdditionalCharges"), LinkButton)

        Dim gvGridviewRow As GridViewRow = CType(lbAdditionalCharges.NamingContainer, GridViewRow)
        Dim _gvHotelRoomType As GridView = CType((gvGridviewRow.Parent.Parent), GridView)
        Dim dlRatePlanItem As DataListItem = CType(_gvHotelRoomType.NamingContainer, DataListItem)
        Dim _dlRatePlan As DataList = CType((dlRatePlanItem.Parent), DataList)
        Dim dlHotelsSearchResultsItem As DataListItem = CType(_dlRatePlan.NamingContainer, DataListItem)
        Dim _dlRatePlan1 As New DataList
        _dlRatePlan1 = CType(dlHotelsSearchResults.Items(dlHotelsSearchResultsItem.ItemIndex).FindControl("dlRatePlan"), DataList)
        Dim _gvHotelRoomType1 As New GridView
        _gvHotelRoomType1 = CType(_dlRatePlan1.Items(dlRatePlanItem.ItemIndex).FindControl("gvHotelRoomType"), GridView)

        Dim lbTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbTotalprice"), LinkButton)
        Dim lbwlTotalprice1 As LinkButton = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("lbwlTotalprice"), LinkButton)

        Dim hdExtraBedRequired As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("hdExtraBedRequired"), Label)
        Dim hdExtraBedValue As Label = CType(_gvHotelRoomType1.Rows(gvGridviewRow.RowIndex).FindControl("hdExtraBedValue"), Label)
        hdExtraBedRequired.Text = "NO"
    End Sub

    Private Function GetMinPriceData(ByVal obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse, ByVal strRoom As String, ByVal strAdult As String, ByVal strChildren As String, ByVal strAgentCode As String, ByVal strSourceCountryCode As String) As DataSet
        ',objBLLHotelSearch.Adult,objBLLHotelSearch.Children,objBLLHotelSearch.AgentCode,objBLLHotelSearch.SourceCountryCode
        Dim ds As DataSet = CreateMinPriceDataSet()

        Try

  
        Dim i As Integer = 0

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("hotelcode", GetType(String)))
        dt.Columns.Add(New DataColumn("roomtypecode", GetType(String)))
        dt.Columns.Add(New DataColumn("cost", GetType(Decimal)))
        dt.Columns.Add(New DataColumn("noofroom", GetType(Integer)))
        dt.Columns.Add(New DataColumn("noofadult", GetType(Integer)))
        dt.Columns.Add(New DataColumn("noofchild", GetType(Integer)))
            dt.Columns.Add(New DataColumn("Int_RoomtypeCodes", GetType(String)))
            dt.Columns.Add(New DataColumn("mealplan", GetType(String)))
            dt.Columns.Add(New DataColumn("currCode", GetType(String)))


            Dim dtRemove As New DataTable
            dtRemove.Columns.Add(New DataColumn("partycode", GetType(String)))
          

        Dim strArrDate As DateTime
        Dim strDepDate As DateTime
        Dim strCurrCode As String = ""
            For Each item In obHotelSearchResponse.results


                Dim row As DataRow = ds.Tables(0).NewRow()


                i = i + 1
                row("rowid") = i
                row("partycode") = item.hotel.id
                row("partyname") = item.hotel.name
                row("citycode") = item.hotel.city.id
                row("cityname") = item.hotel.city.name
                row("hotelimage") = item.hotel.photo
                row("hoteltext") = ""
                Dim objMin = item.accomodations.OrderBy(Function(p) p.netPrice).FirstOrDefault()

                row("minprice") = objMin.netPrice / objMin.nights
                row("catcode") = item.hotel.category.id
                row("catname") = item.hotel.category.name
                row("sectorcode") = item.hotel.city.id
                row("sectorname") = item.hotel.city.name
                row("propertytype") = item.hotel.type.id
                row("propertytypename") = item.hotel.type.name
                row("preferred") = 0
                row("available") = 1
                row("currcode") = objMin.currency
                row("noofstars") = item.hotel.category.id
                row("latitude") = item.hotel.posLatitude
                row("longitude") = item.hotel.posLongitude
                row("forrooms") = "For " & strRoom & " Room(s)"
                row("tel1") = ""
                row("email") = ""
                row("rateplansource") = "OneDMC"

                If objMin.id <> "0" And objMin.id <> "" Then
                    ds.Tables(0).Rows.Add(row)
                End If
                Dim strAccId As String = objMin.id


                Dim mdr As DataRow = dt.NewRow
                mdr("hotelcode") = item.hotel.id
                mdr("roomtypecode") = objMin.distributions(0).roomcode
                mdr("cost") = objMin.netPrice / objMin.nights ' resDetail.result.distributions(i).netPrice
                mdr("noofroom") = strRoom ' resDetail.result.distributions(i).numberRooms
                mdr("noofadult") = strAdult
                mdr("noofchild") = strChildren
                mdr("currCode") = objMin.currency
                If objMin.id <> "0" And objMin.id <> "" Then
                    dt.Rows.Add(mdr)
                End If

                strArrDate = objMin.arrivalDate
                strDepDate = objMin.departureDate
                ' strCurrCode = objMin.currency

            Next
        dt.TableName = "Table"
        Dim searchdetail As String = objclsUtilities.GenerateXML_FromDataTable(dt)
            Dim param(8) As SqlParameter
        param(0) = New SqlParameter("@checkin", strArrDate.ToString("yyy-MM-dd"))
        param(1) = New SqlParameter("@checkout", strDepDate.ToString("yyy-MM-dd"))
        param(2) = New SqlParameter("@agentcode", strAgentCode)
        param(3) = New SqlParameter("@ctryCode", strSourceCountryCode)
        param(4) = New SqlParameter("@searchxml", searchdetail)
        param(5) = New SqlParameter("@userlogged", Session("GlobalUserName"))
            '  param(6) = New SqlParameter("@currCode", strCurrCode)
            param(6) = New SqlParameter("@descFlag", 1)
            param(7) = New SqlParameter("@mappingSource", 0)
            param(8) = New SqlParameter("@clientCode", "oneDMC")
        Dim resultDS As DataSet = objclsUtilities.GetDataSet("int_markup", param)

        For h As Integer = 0 To ds.Tables(0).Rows.Count - 1
            If resultDS.Tables(0).Rows.Count > 0 Then
                Dim dv As DataView = New DataView(resultDS.Tables(0))
                dv.RowFilter = "hotelcode='" & ds.Tables(0).Rows(h)("partycode") & "'"
                If dv.Count = 1 Then
                        ds.Tables(0).Rows(h)("minprice") = dv.Item(0)("salevalue").ToString
                        ds.Tables(0).Rows(h)("currcode") = dv.Item(0)("agentcurrcode").ToString
                        If dv.Item(0)("columbus_hotelcode").ToString = "" Or dv.Item(0)("Columbus_roomcode").ToString = "" Or dv.Item(0)("formulaMarkupId").ToString = "" Then
                            Dim drRemove As DataRow = dtRemove.NewRow
                            drRemove("partycode") = resultDS.Tables(0).Rows(h)("hotelcode")
                            dtRemove.Rows.Add(drRemove)
                        End If
                End If
            End If

               
            If resultDS.Tables(1).Rows.Count > 0 Then
                Dim dv As DataView = New DataView(resultDS.Tables(1))
                dv.RowFilter = "partycode='" & ds.Tables(0).Rows(h)("partycode") & "'"
                If dv.Count = 1 Then
                    ds.Tables(0).Rows(h)("hoteltext") = dv.Item(0)("hoteltext").ToString
                    ds.Tables(0).Rows(h)("tel1") = dv.Item(0)("phone").ToString
                        ds.Tables(0).Rows(h)("email") = dv.Item(0)("email").ToString

                        ds.Tables(0).Rows(h)("citycode") = dv.Item(0)("citycode").ToString
                        ds.Tables(0).Rows(h)("cityname") = dv.Item(0)("cityname").ToString
                        ds.Tables(0).Rows(h)("catcode") = dv.Item(0)("catcode").ToString
                        ds.Tables(0).Rows(h)("catname") = dv.Item(0)("catname").ToString
                        ds.Tables(0).Rows(h)("sectorcode") = dv.Item(0)("sectorcode").ToString
                        ds.Tables(0).Rows(h)("sectorname") = dv.Item(0)("sectorname").ToString
                        ds.Tables(0).Rows(h)("propertytype") = dv.Item(0)("propertytype").ToString
                        ds.Tables(0).Rows(h)("propertytypename") = dv.Item(0)("propertytypename").ToString
                        ds.Tables(0).Rows(h)("partycode") = dv.Item(0)("columbuspartycode").ToString
                        ds.Tables(0).Rows(h)("Int_partycode") = dv.Item(0)("partycode").ToString

               

                End If
            End If
            Next
            If dtRemove.Rows.Count > 0 And ds.Tables(0).Rows.Count > 0 Then
                Dim dtRemove_Distinct As DataTable = New DataView(dtRemove).ToTable(True, "partycode")
                For i = ds.Tables(0).Rows.Count - 1 To 0 Step -1
                    Dim DrResult As Array = dtRemove_Distinct.Select("partycode='" & ds.Tables(0).Rows(i)("Int_partycode") & "'")
                    If DrResult.Length > 0 Then
                        ds.Tables(0).Rows.RemoveAt(i)
                    ElseIf IsDBNull(ds.Tables(0).Rows(i)("Int_partycode")) Then
                        ds.Tables(0).Rows.RemoveAt(i)
                    End If
                Next

               
            End If
            For i = ds.Tables(0).Rows.Count - 1 To 0 Step -1
                If ds.Tables(0).Rows(i)("minprice") = "0" Then
                    ds.Tables(0).Rows.Remove(ds.Tables(0).Rows(i))
                End If
                If ds.Tables(0).Rows.Count = 0 Then
                    Exit For
                End If
            Next
            Return ds
        Catch ex As Exception
            Return ds
        End Try

    End Function

    Private Function CreateRoomPriceTotalDataTable() As DataTable

        Dim dtRoomPriceTotal As New DataTable
        dtRoomPriceTotal.TableName = "Table2"
        Dim rateplanid As DataColumn = New DataColumn("rateplanid", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(rateplanid)
        Dim partycode As DataColumn = New DataColumn("partycode", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(partycode)
        Dim rmtypcode As DataColumn = New DataColumn("rmtypcode", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(rmtypcode)
        Dim mealplans As DataColumn = New DataColumn("mealplans", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(mealplans)
        Dim sharingorextrabed As DataColumn = New DataColumn("sharingorextrabed", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(sharingorextrabed)
        Dim roomno As DataColumn = New DataColumn("roomno", Type.GetType("System.Int32"))
        dtRoomPriceTotal.Columns.Add(roomno)
        Dim RoomHeading As DataColumn = New DataColumn("RoomHeading", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(RoomHeading)
        Dim saletotal As DataColumn = New DataColumn("saletotal", Type.GetType("System.Decimal"))
        dtRoomPriceTotal.Columns.Add(saletotal)
        Dim costtotal As DataColumn = New DataColumn("costtotal", Type.GetType("System.Decimal"))
        dtRoomPriceTotal.Columns.Add(costtotal)
        Dim RoomId As DataColumn = New DataColumn("RoomId", Type.GetType("System.String"))
        dtRoomPriceTotal.Columns.Add(RoomId)

        Return dtRoomPriceTotal

    End Function


    Private Function CreateRoomPriceDataTable() As DataTable

        Dim dtRoomPrice As New DataTable
        dtRoomPrice.TableName = "Table1"
        Dim rateplanid As DataColumn = New DataColumn("rateplanid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(rateplanid)
        Dim rateplanname As DataColumn = New DataColumn("rateplanname", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(rateplanname)
        Dim partycode As DataColumn = New DataColumn("partycode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(partycode)
        Dim rmtypcode As DataColumn = New DataColumn("rmtypcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(rmtypcode)
        Dim roomclasscode As DataColumn = New DataColumn("roomclasscode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(roomclasscode)
        Dim rmtyporder As DataColumn = New DataColumn("rmtyporder", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(rmtyporder)
        Dim rmcatcode As DataColumn = New DataColumn("rmcatcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(rmcatcode)
        Dim mealplans As DataColumn = New DataColumn("mealplans", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(mealplans)
        Dim mealcode As DataColumn = New DataColumn("mealcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(mealcode)
        Dim mealplannames As DataColumn = New DataColumn("mealplannames", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(mealplannames)
        Dim accommodationid As DataColumn = New DataColumn("accommodationid", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(accommodationid)
        Dim roomno As DataColumn = New DataColumn("roomno", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(roomno)
        Dim adults As DataColumn = New DataColumn("adults", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(adults)
        Dim child As DataColumn = New DataColumn("child", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(child)
        Dim childages As DataColumn = New DataColumn("childages", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(childages)
        Dim sharingorextrabed As DataColumn = New DataColumn("sharingorextrabed", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(sharingorextrabed)
        Dim agecombination As DataColumn = New DataColumn("agecombination", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(agecombination)
        Dim priceadults As DataColumn = New DataColumn("priceadults", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(priceadults)
        Dim pricechild As DataColumn = New DataColumn("pricechild", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(pricechild)
        Dim noofadulteb As DataColumn = New DataColumn("noofadulteb", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(noofadulteb)
        Dim noofchildeb As DataColumn = New DataColumn("noofchildeb", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(noofchildeb)

        Dim Pricedate As DataColumn = New DataColumn("Pricedate", Type.GetType("System.DateTime"))
        dtRoomPrice.Columns.Add(Pricedate)
        Dim totalprice As DataColumn = New DataColumn("totalprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(totalprice)
        Dim pricewithfreenight As DataColumn = New DataColumn("pricewithfreenight", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(pricewithfreenight)
        Dim contractid As DataColumn = New DataColumn("contractid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(contractid)
        Dim promotionid As DataColumn = New DataColumn("promotionid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(promotionid)
        Dim contpromid As DataColumn = New DataColumn("contpromid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(contpromid)
        Dim autoid As DataColumn = New DataColumn("autoid", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(autoid)
        Dim datelineno As DataColumn = New DataColumn("datelineno", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(datelineno)
        Dim minstay As DataColumn = New DataColumn("minstay", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(minstay)
        Dim minstayoption As DataColumn = New DataColumn("minstayoption", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(minstayoption)
        Dim stayfor As DataColumn = New DataColumn("stayfor", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(stayfor)
        Dim freenights As DataColumn = New DataColumn("freenights", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(freenights)
        Dim maxstay As DataColumn = New DataColumn("maxstay", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(maxstay)
        Dim formulaid As DataColumn = New DataColumn("formulaid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(formulaid)
        Dim applymarkupid As DataColumn = New DataColumn("applymarkupid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(applymarkupid)
        Dim markupvalue As DataColumn = New DataColumn("markupvalue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(markupvalue)
        Dim ctryformulaid As DataColumn = New DataColumn("ctryformulaid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(ctryformulaid)
        Dim ctrymarkupvalue As DataColumn = New DataColumn("ctrymarkupvalue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(ctrymarkupvalue)
        Dim agformulaid As DataColumn = New DataColumn("agformulaid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(agformulaid)
        Dim agmarkupvalue As DataColumn = New DataColumn("agmarkupvalue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(agmarkupvalue)
        Dim saleprice As DataColumn = New DataColumn("saleprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(saleprice)
        Dim wlmarkupvalue As DataColumn = New DataColumn("wlmarkupvalue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(wlmarkupvalue)
        Dim wlsaleprice As DataColumn = New DataColumn("wlsaleprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(wlsaleprice)
        Dim ctryapplymarkupid As DataColumn = New DataColumn("ctryapplymarkupid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(ctryapplymarkupid)
        Dim agapplymarkupid As DataColumn = New DataColumn("agapplymarkupid", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(agapplymarkupid)
        Dim rmtypupgradefrom As DataColumn = New DataColumn("rmtypupgradefrom", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(rmtypupgradefrom)
        Dim mealupgradefrom As DataColumn = New DataColumn("mealupgradefrom", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(mealupgradefrom)
        Dim rmcatupgradefrom As DataColumn = New DataColumn("rmcatupgradefrom", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(rmcatupgradefrom)
        Dim bookingcode As DataColumn = New DataColumn("bookingcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(bookingcode)
        Dim saleconvrate As DataColumn = New DataColumn("saleconvrate", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(saleconvrate)
        Dim pricepax As DataColumn = New DataColumn("pricepax", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(pricepax)
        Dim currentselection As DataColumn = New DataColumn("currentselection", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(currentselection)
        Dim noofchildfree As DataColumn = New DataColumn("noofchildfree", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(noofchildfree)
        Dim roomrate As DataColumn = New DataColumn("roomrate", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(roomrate)
        Dim adultebprice As DataColumn = New DataColumn("adultebprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(adultebprice)
        Dim extrapaxprice As DataColumn = New DataColumn("extrapaxprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(extrapaxprice)
        Dim adultmealprice As DataColumn = New DataColumn("adultmealprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(adultmealprice)
        Dim totalsharingcharge As DataColumn = New DataColumn("totalsharingcharge", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(totalsharingcharge)
        Dim totalebcharge As DataColumn = New DataColumn("totalebcharge", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(totalebcharge)
        Dim totalmealcharge As DataColumn = New DataColumn("totalmealcharge", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(totalmealcharge)
        Dim childmealdetails As DataColumn = New DataColumn("childmealdetails", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(childmealdetails)
        Dim noofextrapax As DataColumn = New DataColumn("noofextrapax", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(noofextrapax)
        Dim VATPerc As DataColumn = New DataColumn("VATPerc", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(VATPerc)
        Dim CostTaxableValue As DataColumn = New DataColumn("CostTaxableValue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(CostTaxableValue)
        Dim CostNonTaxableValue As DataColumn = New DataColumn("CostNonTaxableValue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(CostNonTaxableValue)
        Dim CostVATValue As DataColumn = New DataColumn("CostVATValue", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(CostVATValue)
        Dim commissionformulastring As DataColumn = New DataColumn("commissionformulastring", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(commissionformulastring)
        Dim cprice As DataColumn = New DataColumn("cprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(cprice)
        Dim npr As DataColumn = New DataColumn("npr", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(npr)
        Dim exhibitionprice As DataColumn = New DataColumn("exhibitionprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(exhibitionprice)
        Dim Cost_Price_Override As DataColumn = New DataColumn("Cost_Price_Override", Type.GetType("System.Int32"))
        dtRoomPrice.Columns.Add(Cost_Price_Override)
        Dim costpricesalecurrency As DataColumn = New DataColumn("costpricesalecurrency", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(costpricesalecurrency)
        Dim costcurrcode As DataColumn = New DataColumn("costcurrcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(costcurrcode)
        Dim salecurrcode As DataColumn = New DataColumn("salecurrcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(salecurrcode)
        Dim RoomHeading As DataColumn = New DataColumn("RoomHeading", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(RoomHeading)
        Dim wlcurrcode As DataColumn = New DataColumn("wlcurrcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(wlcurrcode)
        Dim wlmarkupperc As DataColumn = New DataColumn("wlmarkupperc", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(wlmarkupperc)
        Dim wlconvrate As DataColumn = New DataColumn("wlconvrate", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(wlconvrate)
        Dim Int_RoomtypeCodes As DataColumn = New DataColumn("Int_RoomtypeCodes", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_RoomtypeCodes)
        Dim Int_RoomtypeNames As DataColumn = New DataColumn("Int_RoomtypeNames", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_RoomtypeNames)
        Dim Int_Roomtypes As DataColumn = New DataColumn("Int_Roomtypes", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_Roomtypes)
        Dim saletotal As DataColumn = New DataColumn("saletotal", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(saletotal)
        Dim costtotal As DataColumn = New DataColumn("costtotal", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(costtotal)
        Dim Int_costprice As DataColumn = New DataColumn("Int_costprice", Type.GetType("System.Decimal"))
        dtRoomPrice.Columns.Add(Int_costprice)
        Dim Int_costcurrcode As DataColumn = New DataColumn("Int_costcurrcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_costcurrcode)
        Dim Int_partycode As DataColumn = New DataColumn("Int_partycode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_partycode)
        Dim Int_rmtypecode As DataColumn = New DataColumn("Int_rmtypecode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_rmtypecode)
        Dim Int_mealcode As DataColumn = New DataColumn("Int_mealcode", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(Int_mealcode)
        Dim RoomId As DataColumn = New DataColumn("RoomId", Type.GetType("System.String"))
        dtRoomPrice.Columns.Add(RoomId)

        Return dtRoomPrice
    End Function

    Private Function BindOneDMCHotelRoomTypeDataSet(ByVal strHotelCode As String, ByVal strRoom As String, ByVal strAdult As String, ByVal strChildren As String, ByVal strChildAgeString As String, ByVal CheckIn As String, ByVal CheckOut As String, ByVal AgentCode As String, ByVal SourceCountryCode As String, ByVal NoOfNights As String) As DataSet


        Dim obHotelSearchResponse As APIHotelSearchResponse.HotelSearchResponse = New APIHotelSearchResponse.HotelSearchResponse()
        'Dim obHotelSearchResponse1 As New APIHotelSearchResponse.HotelSearchResponse.resu 'obHotelSearchResponse.results ' = New APIHotelSearchResponse.HotelSearchResponse.results

        obHotelSearchResponse = Session("sobHotelSearchResponse")
        Dim dsHotelRoomType As DataSet = CreateSingleHotelDataSet()
        If dsHotelRoomType Is Nothing Then
            Return dsHotelRoomType
        End If

        Dim obj As Object = obHotelSearchResponse.results
        Dim pers As Object = obHotelSearchResponse.results.FirstOrDefault(Function(p) p.hotel.id = strHotelCode)
        Dim strPax As String = strAdult & " Adults" & IIf(strChildren > 0, strChildAgeString.Replace(",", "Yrs, "), "")
        Dim i As Integer = 0
        Dim strRoomString As String = ""
        'If CheckIn <> "" Then
        '    Dim strDates As String() = CheckIn.Split("/")
        '    CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        'End If
        'If CheckOut <> "" Then
        '    Dim strDates As String() = CheckOut.Split("/")
        '    CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        'End If

        For Each item In obHotelSearchResponse.results
            item = obHotelSearchResponse.results.FirstOrDefault(Function(p) p.hotel.id = strHotelCode)
            If item Is Nothing Then
                Return dsHotelRoomType
            End If
            For Each dist In item.accomodations

                If dist.id = "0" Or dist.id = "" Then
                    Exit For
                End If
                Dim strRoomTypeCode As String = ""
                Dim strRoomTypeName As String = ""
                Dim strRoomTypes As String = ""
                Dim strRoomName As String = ""
                Dim strDistLength As Integer = dist.distributions.Length
                Dim iNoOfExtraBed As Integer = 0

                Dim stroffers As String = ""
                i = i + 1
                Dim strRoomId As String = ""
                For d As Integer = 0 To dist.distributions.Length - 1
                    For r As Integer = 0 To dist.distributions(d).numberRooms - 1
                        strRoomId = IIf(strRoomId = "", dist.distributions(d).room.id, strRoomId & ":" & dist.distributions(d).room.id)
                    Next
                Next
                Dim myListRoomName As New List(Of String)
                For d As Integer = 0 To dist.distributions.Length - 1
                    For r As Integer = 0 To dist.distributions(d).numberRooms - 1



                        myListRoomName.Add(dist.distributions(d).room.description)

                        strRoomTypeCode = IIf(strRoomTypeCode = "", (d + r + 1).ToString & ":" & dist.distributions(d).roomcode, strRoomTypeCode & ";" & (d + r + 1).ToString & ":" & dist.distributions(d).roomcode)

                        strRoomTypeName = IIf(strRoomTypeName = "", (d + r + 1).ToString & ":" & dist.distributions(d).room.description, strRoomTypeName & ";" & (d + r + 1).ToString & ":" & dist.distributions(d).room.description)
                        strRoomTypes = IIf(strRoomTypes = "", (d + r + 1).ToString & ":" & dist.distributions(d).room.id & ":" & dist.distributions(d).room.name & ":" & dist.distributions(d).room.description, strRoomTypes & ";" & (d + r + 1).ToString & ":" & dist.distributions(d).room.id & ":" & dist.distributions(d).room.name & ":" & dist.distributions(d).room.description)
                        iNoOfExtraBed = iNoOfExtraBed + dist.distributions(d).numberExtraBeds
                        Dim strChildAge As String = ""
                        If Not dist.distributions(d).childrenAges Is Nothing Then
                            For c As Integer = 0 To dist.distributions(d).childrenAges.Length - 1
                                strChildAge = IIf(strChildAge = "", dist.distributions(d).childrenAges(c).ToString, strChildAge & "|" & dist.distributions(d).childrenAges(c).ToString)
                            Next
                        End If
                        stroffers = IIf(stroffers = "", (d + r + 1).ToString & ":" & dist.distributions(d).offers, stroffers & ";" & (d + r + 1).ToString & ":" & dist.distributions(d).offers)
                        strRoomString = IIf(strRoomString = "", (d + r + 1).ToString & "," & dist.distributions(d).numberAdults.ToString & "," & dist.distributions(d).numberChildren.ToString & "," & IIf(dist.distributions(d).numberChildren > 0, strChildAge, "0"), strRoomString & "; " & +1).ToString & "," & dist.distributions(d).numberAdults.ToString & "," & dist.distributions(d).numberChildren.ToString & "," & IIf(dist.distributions(d).numberChildren > 0, strChildAge, "0")

                        Dim rowPriceTotal As DataRow = dsHotelRoomType.Tables(2).NewRow()
                        rowPriceTotal("rateplanid") = IIf(dist.distributions(0).offers = "", "Contract", dist.distributions(0).offers)
                        rowPriceTotal("partycode") = item.hotel.id.ToString
                        rowPriceTotal("rmtypcode") = dist.distributions(0).roomcode
                        rowPriceTotal("mealplans") = dist.distributions(0).board.id
                        rowPriceTotal("sharingorextrabed") = IIf(iNoOfExtraBed > 0, "Extrabed", "Sharing")
                        rowPriceTotal("roomno") = (d + r + 1).ToString
                        rowPriceTotal("RoomHeading") = dist.distributions(d).room.description & "- " & dist.distributions(d).numberAdults.ToString & " ADULTS " & IIf(dist.distributions(d).numberChildren > 0, dist.distributions(d).numberChildren.ToString & " Child(" & strChildAge & ")", "")
                        rowPriceTotal("saletotal") = 0 'dist.distributions(d).netPrice ' + Markup
                        rowPriceTotal("costtotal") = 0 'dist.distributions(d).netPrice
                        rowPriceTotal("RoomId") = dist.id.ToString + "|" + dist.netPrice.ToString + "|" + strRoomId
                        dsHotelRoomType.Tables(2).Rows.Add(rowPriceTotal)
                    Next
                Next


                'Dim dtRemove As New DataTable
                'dtRemove.Columns.Add(New DataColumn("hotelcode", GetType(String)))
                'dtRemove.Columns.Add(New DataColumn("rateplanid", GetType(String)))
                'dtRemove.Columns.Add(New DataColumn("rmtypcode", GetType(String)))
                'dtRemove.Columns.Add(New DataColumn("mealplans", GetType(String)))



                Dim groupedRoomNames = myListRoomName.GroupBy(Function(x) x)
                If groupedRoomNames IsNot Nothing AndAlso groupedRoomNames.Count > 0 Then
                    For Each room In groupedRoomNames
                        strRoomName = IIf(strRoomName = "", room.Key & "(" & room.Count.ToString & ")", strRoomName & " / " & room.Key & "(" & room.Count.ToString & ")")
                    Next
                End If

                Dim row As DataRow = dsHotelRoomType.Tables(0).NewRow()

                row("partycode") = item.hotel.id.ToString
                row("Int_partycode") = item.hotel.id.ToString
                row("rateplanid") = IIf(dist.distributions(0).offers = "", "Contract", dist.distributions(0).offers)
                row("rateplanname") = IIf(dist.distributions(0).offers = "", "Contract", dist.distributions(0).offers)

                row("rmtypcode") = dist.distributions(0).roomcode

                row("rmtypname") = strRoomName ' strRoomTypeName
                row("Int_RoomtypeCodes") = strRoomTypeCode
                row("Int_RoomtypeNames") = strRoomTypeName
                row("Int_Roomtypes") = strRoomTypes
                row("Int_rmtypecode") = dist.distributions(0).roomcode
                row("Int_mealcode") = dist.distributions(0).board.id
                row("mealplans") = dist.distributions(0).board.id
                row("mealplannames") = dist.distributions(0).board.name

                row("totalvalue") = 0 'dist.netPrice
                row("available") = IIf(dist.onRequest = False, 1, 0)
                row("rmtyporder") = i
                row("avgprice") = 0 'dist.netPrice
                row("partyavgprice") = dist.netPrice
                row("partyavailable") = IIf(dist.onRequest = False, 1, 0)
                row("currcode") = "" 'dist.currency
                row("roomclasscode") = "" '**** NEED CLARITY
                row("rmcatcode") = "" '**** NEED CLARITY
                row("rateplanorder") = "1"
                row("salevalue") = 0 'dist.netPrice ' + Markup 'salevalue
                row("costvalue") = 0 'dist.netPrice
                row("costcurrcode") = "" 'dist.currency
                row("supagentcode") = "" '**** NEED CLARITY
                row("comp_cust") = 0
                row("comp_supp") = 0
                row("comparrtrf") = 0
                row("compdeptrf") = 0
                row("forrooms") = "(" & strRoom & " Room(s))"
                row("rateplansummary") = strRoom & " Room(s) - " & strPax
                row("roomname") = strRoomName
                row("noofrooms") = strRoom
                row("noofadults") = strAdult
                row("noofchild") = strChildren
                row("childagestring") = strChildAgeString.Replace(",", ";")
                row("noofadulteb") = iNoOfExtraBed
                row("noofchildeb") = 0
                row("sharingorextrabed") = IIf(iNoOfExtraBed > 0, "Extrabed", "Sharing")
                row("mealorder") = i.ToString
                row("currentselection") = 0
                row("hotelroomstring") = strRoomString
                row("VATexclude") = 0
                row("vatperc") = 0
                row("costtaxablevalue") = 0
                row("costnontaxablevalue") = 0
                row("costvatvalue") = 0
                row("wlcurrcode") = dist.currency
                row("Show") = "Show"
                row("mealupgradefrom") = ""
                row("rateplansource") = "OneDMC"
                row("Int_costprice") = dist.netPrice
                row("Int_costcurrcode") = dist.currency
                row("offercode") = stroffers
                row("accomodationcode") = dist.id
                row("RoomId") = dist.id.ToString + "|" + dist.netPrice.ToString + "|" + strRoomId

                dsHotelRoomType.Tables(0).Rows.Add(row)

            Next
                Exit For
            Next

            Dim dt As New DataTable
            dt.Columns.Add(New DataColumn("hotelcode", GetType(String)))
            dt.Columns.Add(New DataColumn("roomtypecode", GetType(String)))
            dt.Columns.Add(New DataColumn("cost", GetType(Decimal)))
            dt.Columns.Add(New DataColumn("noofroom", GetType(Integer)))
            dt.Columns.Add(New DataColumn("noofadult", GetType(Integer)))
            dt.Columns.Add(New DataColumn("noofchild", GetType(Integer)))
            dt.Columns.Add(New DataColumn("Int_RoomtypeCodes", GetType(String)))
            dt.Columns.Add(New DataColumn("mealplan", GetType(String)))
            dt.Columns.Add(New DataColumn("currCode", GetType(String)))
            dt.Columns.Add(New DataColumn("RoomId", GetType(String)))
            Dim strArrDate As DateTime
            Dim strDepDate As DateTime
            Dim strCurrCode As String = ""

            For Each item In obHotelSearchResponse.results
                item = obHotelSearchResponse.results.FirstOrDefault(Function(p) p.hotel.id = strHotelCode)

                Dim dCheckIn As Date = CDate(CheckIn)
                Dim dCheckOut As Date = CDate(CheckOut)
                Dim dPriceDate As Date
                dPriceDate = dCheckIn
                Do While (dPriceDate < dCheckOut)
                    For Each dist In item.accomodations
                        If dist.id = "0" Or dist.id = "" Then
                            Exit For
                        End If
                        Dim strRoomTypeCode As String = ""
                        Dim strRoomTypeName As String = ""
                        Dim strRoomName As String = ""
                        Dim strDistLength As Integer = dist.distributions.Length
                        Dim iNoOfExtraBed As Integer = 0

                    Dim strRoomId As String = ""
                    For d As Integer = 0 To dist.distributions.Length - 1
                        For r As Integer = 0 To dist.distributions(d).numberRooms - 1
                            strRoomId = IIf(strRoomId = "", dist.distributions(d).room.id, strRoomId & ":" & dist.distributions(d).room.id)
                        Next
                    Next
                        For d As Integer = 0 To dist.distributions.Length - 1
                            For r As Integer = 0 To dist.distributions(d).numberRooms - 1
                                Dim strChildAge As String = ""
                                If Not dist.distributions(d).childrenAges Is Nothing Then
                                    For c As Integer = 0 To dist.distributions(d).childrenAges.Length - 1
                                        strChildAge = IIf(strChildAge = "", dist.distributions(d).childrenAges(c).ToString, strChildAge & "|" & dist.distributions(d).childrenAges(c).ToString)
                                    Next
                                End If



                                Dim row As DataRow = dsHotelRoomType.Tables(1).NewRow()

                                row("rateplanid") = IIf(dist.distributions(d).offers = "", "Contract", dist.distributions(d).offers) ''*** Need clarity- Clarified
                                row("rateplanname") = IIf(dist.distributions(d).offers = "", "Contract", dist.distributions(d).offers)
                                row("partycode") = item.hotel.id.ToString
                                row("rmtypcode") = dist.distributions(0).roomcode ''*** Need clarity - Clarified
                                row("Int_RoomtypeCodes") = (d + r + 1).ToString & ":" & dist.distributions(d).roomcode
                                row("Int_RoomtypeNames") = (d + r + 1).ToString & ":" & dist.distributions(d).room.description
                                row("Int_Roomtypes") = (d + r + 1).ToString & ":" & dist.distributions(d).room.id & ":" & dist.distributions(d).room.name & ":" & dist.distributions(d).room.description


                                row("roomclasscode") = "" '**** NEED CLARITY- Clarified
                                row("rmtyporder") = i
                                row("rmcatcode") = "" '**** NEED CLARITY- Clarified
                                row("mealplans") = dist.distributions(d).board.id
                                row("mealcode") = dist.distributions(d).board.id
                                row("Int_rmtypecode") = dist.distributions(0).roomcode
                                row("Int_mealcode") = dist.distributions(d).board.id

                                row("mealplannames") = dist.distributions(d).board.name
                                row("accommodationid") = dist.id
                                row("roomno") = (d + r + 1).ToString
                                row("adults") = dist.distributions(d).numberAdults.ToString
                                row("child") = dist.distributions(d).numberChildren.ToString
                                strChildAge = ""
                                If Not dist.distributions(d).childrenAges Is Nothing Then
                                    For c As Integer = 0 To dist.distributions(d).childrenAges.Length - 1
                                        strChildAge = IIf(strChildAge = "", dist.distributions(d).childrenAges(c).ToString, strChildAge & "|" & dist.distributions(d).childrenAges(c).ToString)
                                    Next
                                Else
                                    strChildAge = "0"
                                End If

                                row("childages") = strChildAge
                                row("sharingorextrabed") = IIf(dist.distributions(d).numberExtraBeds > 0, "Extrabed", "Sharing")
                                row("agecombination") = ""
                                row("priceadults") = dist.distributions(d).numberAdults.ToString
                                row("pricechild") = dist.distributions(d).numberChildren.ToString
                                row("noofadulteb") = dist.distributions(d).numberExtraBeds
                                row("noofchildeb") = 0
                                row("Pricedate") = dPriceDate.ToString("yyyy-MM-dd")
                                row("totalprice") = 0 'dist.distributions(d).netPrice / dist.nights
                                row("pricewithfreenight") = 0
                                row("contractid") = ""
                                row("promotionid") = ""
                                row("contractid") = ""
                                row("contpromid") = ""
                                row("autoid") = 0
                                row("datelineno") = 0
                                row("minstay") = 0
                                row("minstayoption") = ""
                                row("stayfor") = 0
                                row("freenights") = 0
                                row("maxstay") = 0
                                row("formulaid") = ""
                                row("applymarkupid") = ""
                                row("markupvalue") = 0
                                row("ctryformulaid") = ""
                                row("ctrymarkupvalue") = 0
                                row("agformulaid") = ""
                                row("agmarkupvalue") = 0
                                row("saleprice") = 0 ' dist.distributions(d).netPrice / dist.nights ' + Markup
                                row("wlmarkupvalue") = 0
                                row("wlsaleprice") = 0 'dist.distributions(d).netPrice / dist.nights ' + Markup
                                row("ctryapplymarkupid") = ""
                                row("agapplymarkupid") = ""
                                row("rmtypupgradefrom") = ""
                                row("mealupgradefrom") = ""
                                row("rmcatupgradefrom") = ""
                                row("bookingcode") = "" 'dist.distributions(d).
                                row("saleconvrate") = 1 '*** NEED CLARITY
                                row("pricepax") = dist.distributions(d).numberAdults + dist.distributions(d).numberChildren
                                row("currentselection") = 0
                                row("noofchildfree") = 0
                                row("roomrate") = 0 ' dist.distributions(d).netPrice / dist.nights
                                row("adultebprice") = 0
                                row("extrapaxprice") = 0
                                row("adultmealprice") = 0
                                row("totalebcharge") = 0
                                row("totalmealcharge") = 0
                                row("childmealdetails") = 0
                                row("noofextrapax") = dist.distributions(d).numberExtraBeds
                                row("VATPerc") = 0
                                row("CostTaxableValue") = 0
                                row("CostNonTaxableValue") = 0
                                row("CostVATValue") = 0
                                row("commissionformulastring") = ""
                                row("cprice") = 0
                                row("npr") = 0
                                row("exhibitionprice") = 0
                                row("Cost_Price_Override") = 3
                                row("costpricesalecurrency") = 0 'dist.currency
                                row("costcurrcode") = "" 'dist.currency
                                row("salecurrcode") = "" 'dist.currency
                                row("RoomHeading") = dist.distributions(d).numberAdults.ToString & " ADULTS " & IIf(dist.distributions(d).numberChildren > 0, dist.distributions(d).numberChildren.ToString & " Child(" & strChildAge & ")", "")

                                row("wlcurrcode") = "" ' dist.currency
                                row("wlmarkupperc") = 0
                                row("wlconvrate") = 0
                                row("saletotal") = 0
                                row("costtotal") = 0
                                If (dPriceDate = dCheckIn) Then
                                    row("Int_costprice") = dist.distributions(d).netPrice
                                Else
                                    row("Int_costprice") = 0
                                End If

                                row("Int_costcurrcode") = dist.currency
                                row("Int_partycode") = item.hotel.id.ToString
                            row("RoomId") = dist.id.ToString + "|" + dist.netPrice.ToString + "|" + strRoomId
                                dsHotelRoomType.Tables(1).Rows.Add(row)

                                If (dPriceDate = dCheckIn) Then
                                    Dim mdr As DataRow = dt.NewRow
                                    mdr("hotelcode") = item.hotel.id
                                    mdr("roomtypecode") = dist.distributions(0).roomcode 'dist.distributions(d).roomcode
                                    mdr("cost") = dist.distributions(d).netPrice / dist.distributions(d).numberRooms ' resDetail.result.distributions(i).netPrice
                                    mdr("noofroom") = 1 'strRoom ' resDetail.result.distributions(i).numberRooms
                                    mdr("noofadult") = dist.distributions(d).numberAdults.ToString
                                    mdr("noofchild") = dist.distributions(d).numberChildren.ToString
                                    mdr("Int_RoomtypeCodes") = (d + r + 1).ToString & ":" & dist.distributions(d).roomcode
                                    mdr("mealplan") = dist.distributions(d).board.id
                                    mdr("currCode") = dist.currency
                                mdr("RoomId") = dist.id.ToString + "|" + dist.netPrice.ToString + "|" + strRoomId

                                    dt.Rows.Add(mdr)
                                    strArrDate = dist.arrivalDate
                                    strDepDate = dist.departureDate
                                    '   strCurrCode = dist.currency
                                End If

                            Next

                        Next


                    Next
                    dPriceDate = dPriceDate.AddDays(1)
                Loop

                Exit For
            Next






            dt.TableName = "Table"
            Dim searchdetail As String = objclsUtilities.GenerateXML_FromDataTable(dt)
            Dim param(8) As SqlParameter
            param(0) = New SqlParameter("@checkin", strArrDate.ToString("yyy-MM-dd"))
            param(1) = New SqlParameter("@checkout", strDepDate.ToString("yyy-MM-dd"))
            param(2) = New SqlParameter("@agentcode", AgentCode)
            param(3) = New SqlParameter("@ctryCode", SourceCountryCode)
            param(4) = New SqlParameter("@searchxml", searchdetail)
            param(5) = New SqlParameter("@userlogged", Session("GlobalUserName"))
            param(6) = New SqlParameter("@descFlag", 0)
            param(7) = New SqlParameter("@mappingSource", 0)
            param(8) = New SqlParameter("@clientCode", "oneDMC")
        Dim resultDS As DataSet = objclsUtilities.GetDataSet("int_markup", param)

            Dim dtRemove As New DataTable
            dtRemove.Columns.Add(New DataColumn("partycode", GetType(String)))
            dtRemove.Columns.Add(New DataColumn("rateplanid", GetType(String)))
            dtRemove.Columns.Add(New DataColumn("rmtypcode", GetType(String)))
            dtRemove.Columns.Add(New DataColumn("mealplans", GetType(String)))
            dtRemove.Columns.Add(New DataColumn("RoomId", GetType(String)))

            For h As Integer = 0 To dsHotelRoomType.Tables(1).Rows.Count - 1
                If resultDS.Tables(0).Rows.Count > 0 Then
                    Dim dv As DataView = New DataView(resultDS.Tables(0))
                    dv.RowFilter = "hotelcode='" & dsHotelRoomType.Tables(1).Rows(h)("partycode") & "'  and Int_RoomtypeCodes='" & dsHotelRoomType.Tables(1).Rows(h)("Int_RoomtypeCodes") & "'  and mealplan='" & dsHotelRoomType.Tables(1).Rows(h)("mealplans") & "'  and RoomId='" & dsHotelRoomType.Tables(1).Rows(h)("RoomId") & "' "
                If dv.Count <> 1 Then
                    Dim str As String = ""
                End If

                If dv.Count = 1 Then
                    If dv.Item(0)("columbus_hotelcode").ToString = "" Or dv.Item(0)("Columbus_roomcode").ToString = "" Or dv.Item(0)("ColumbusMealCode").ToString = "" Or dv.Item(0)("formulaMarkupId").ToString = "" Then
                        Dim drRemove As DataRow = dtRemove.NewRow
                        drRemove("partycode") = dsHotelRoomType.Tables(1).Rows(h)("partycode")
                        drRemove("rateplanid") = dsHotelRoomType.Tables(1).Rows(h)("rateplanid")
                        drRemove("rmtypcode") = dsHotelRoomType.Tables(1).Rows(h)("rmtypcode")
                        drRemove("mealplans") = dsHotelRoomType.Tables(1).Rows(h)("mealplans")
                        drRemove("RoomId") = dsHotelRoomType.Tables(1).Rows(h)("RoomId")
                        dtRemove.Rows.Add(drRemove)
                    End If


                    dsHotelRoomType.Tables(1).Rows(h)("saleprice") = Convert.ToDecimal(dv.Item(0)("salevalue").ToString) / Val(NoOfNights)
                    dsHotelRoomType.Tables(1).Rows(h)("wlsaleprice") = Convert.ToDecimal(dv.Item(0)("salevalue").ToString) / Val(NoOfNights)
                    dsHotelRoomType.Tables(1).Rows(h)("formulaid") = dv.Item(0)("formulaMarkupId").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("applymarkupid") = dv.Item(0)("applyMarkupId").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("roomclasscode") = dv.Item(0)("roomclasscode").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("rmcatcode") = dv.Item(0)("roomCategory").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("saletotal") = dv.Item(0)("salevalue").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("costtotal") = Convert.ToDecimal(dv.Item(0)("cost").ToString)
                    dsHotelRoomType.Tables(1).Rows(h)("VATPerc") = dv.Item(0)("VATPerc").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("salecurrcode") = dv.Item(0)("agentcurrcode").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("wlcurrcode") = dv.Item(0)("agentcurrcode").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("saleconvrate") = dv.Item(0)("convrate").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("wlconvrate") = dv.Item(0)("convrate").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("totalprice") = Convert.ToDecimal(dv.Item(0)("cost").ToString) / Val(NoOfNights)
                    dsHotelRoomType.Tables(1).Rows(h)("roomrate") = Convert.ToDecimal(dv.Item(0)("cost").ToString) / Val(NoOfNights)
                    dsHotelRoomType.Tables(1).Rows(h)("costcurrcode") = dv.Item(0)("costCurrCode").ToString

                    dsHotelRoomType.Tables(1).Rows(h)("partycode") = dv.Item(0)("columbus_hotelcode").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("rmtypcode") = dv.Item(0)("Columbus_roomcode").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("mealplans") = dv.Item(0)("ColumbusMealCode").ToString
                    dsHotelRoomType.Tables(1).Rows(h)("mealcode") = dv.Item(0)("ColumbusMealCode").ToString

                End If
                End If

            Next

            For hh As Integer = 0 To dsHotelRoomType.Tables(0).Rows.Count - 1

                Dim strPartyCode As String = dsHotelRoomType.Tables(0).Rows(hh)("partycode").ToString
                Dim strrmtypcode As String = dsHotelRoomType.Tables(0).Rows(hh)("rmtypcode").ToString()
                Dim strmealplans As String = dsHotelRoomType.Tables(0).Rows(hh)("mealplans").ToString
                Dim strrateplanid As String = dsHotelRoomType.Tables(0).Rows(hh)("rateplanid").ToString


                Dim dtSaleTotal As DataTable
                Dim dvTotal As DataView = New DataView(dsHotelRoomType.Tables(1))
                dvTotal.RowFilter = "Int_partycode='" & strPartyCode & "' and Int_rmtypecode='" & strrmtypcode & "'  and Int_mealcode='" & strmealplans & "'  and rateplanid='" & strrateplanid & "' and RoomId='" & dsHotelRoomType.Tables(0).Rows(hh)("RoomId") & "'"
                Dim strVATPerc As String = dvTotal.Item(0)("VATPerc")
                strCurrCode = dvTotal.Item(0)("salecurrcode")
                Dim strCostCurrCode As String = dvTotal.Item(0)("costCurrCode")
                dtSaleTotal = dvTotal.ToTable(True, "partycode", "rmtypcode", "mealplans", "rateplanid", "roomno", "saletotal", "costtotal")

                Dim dSaleTotal As Decimal = 0
                Dim dCostTotal As Decimal = 0


                If dtSaleTotal.Rows.Count > 0 Then
                    For s As Integer = 0 To dtSaleTotal.Rows.Count - 1
                        dSaleTotal = dSaleTotal + Convert.ToDecimal(dtSaleTotal.Rows(s)("saletotal"))
                        dCostTotal = dCostTotal + Convert.ToDecimal(dtSaleTotal.Rows(s)("costtotal"))
                        'strVATPerc = dtSaleTotal.Rows(s)("VATPerc")
                        'strwlcurrcode = dtSaleTotal.Rows(s)("wlcurrcode")
                    Next
                End If
                dsHotelRoomType.Tables(0).Rows(hh)("salevalue") = dSaleTotal
                dsHotelRoomType.Tables(0).Rows(hh)("costvalue") = dCostTotal
                dsHotelRoomType.Tables(0).Rows(hh)("VATPerc") = strVATPerc
                dsHotelRoomType.Tables(0).Rows(hh)("wlcurrcode") = strCurrCode
                dsHotelRoomType.Tables(0).Rows(hh)("currcode") = strCurrCode
                dsHotelRoomType.Tables(0).Rows(hh)("costcurrcode") = strCostCurrCode
                dsHotelRoomType.Tables(0).Rows(hh)("totalvalue") = dCostTotal
                dsHotelRoomType.Tables(0).Rows(hh)("avgprice") = dCostTotal
                dsHotelRoomType.Tables(0).Rows(hh)("partyavgprice") = dCostTotal

                dsHotelRoomType.Tables(0).Rows(hh)("partycode") = dvTotal.Item(0)("partycode")
                dsHotelRoomType.Tables(0).Rows(hh)("rmtypcode") = dvTotal.Item(0)("rmtypcode")
                dsHotelRoomType.Tables(0).Rows(hh)("mealplans") = dvTotal.Item(0)("mealplans")
                dsHotelRoomType.Tables(0).Rows(hh)("roomclasscode") = dvTotal.Item(0)("roomclasscode")
                dsHotelRoomType.Tables(0).Rows(hh)("rmcatcode") = dvTotal.Item(0)("rmcatcode")



                'Dim lstSaleValue = (From n In dsHotelRoomType.Tables(1).AsEnumerable()
                '                    Where n.Field(Of String)("partycode") = strPartyCode And n.Field(Of String)("rmtypcode") = strrmtypcode And n.Field(Of String)("mealplans") = strmealplans And n.Field(Of String)("rateplanid") = strrateplanid Group n By salevalueGroup = New With {
                '                                                    Key .partycode = n.Field(Of Integer)("partycode"),
                '                                                    Key .rmtypcode = n.Field(Of Integer)("rmtypcode"),
                '                                                    Key .mealplans = n.Field(Of String)("mealplans"),
                'Key .rateplanid = n.Field(Of String)("rateplanid")
                '                                           } Into grp = Group
                '                       Select New With {
                '                                  Key .SumAmount = grp.Sum(Function(x) x.Field(Of Decimal)("saletotal"))})



            Next

            For hh As Integer = 0 To dsHotelRoomType.Tables(2).Rows.Count - 1

                Dim strPartyCode As String = dsHotelRoomType.Tables(2).Rows(hh)("partycode").ToString
                Dim strrmtypcode As String = dsHotelRoomType.Tables(2).Rows(hh)("rmtypcode").ToString()
                Dim strmealplans As String = dsHotelRoomType.Tables(2).Rows(hh)("mealplans").ToString
                Dim strrateplanid As String = dsHotelRoomType.Tables(2).Rows(hh)("rateplanid").ToString
                Dim strRoomNo As String = dsHotelRoomType.Tables(2).Rows(hh)("roomno").ToString

                Dim dtSaleTotal As DataTable
                Dim dvTotal As DataView = New DataView(dsHotelRoomType.Tables(1))
                dvTotal.RowFilter = "Int_partycode='" & strPartyCode & "' and Int_rmtypecode='" & strrmtypcode & "'  and Int_mealcode='" & strmealplans & "'  and rateplanid='" & strrateplanid & "'  and roomno='" & strRoomNo & "'  and RoomId='" & dsHotelRoomType.Tables(2).Rows(hh)("RoomId") & "'"
            If dvTotal.Count > 0 Then
                dtSaleTotal = dvTotal.ToTable(True, "partycode", "rmtypcode", "mealplans", "rateplanid", "roomno", "saletotal", "costtotal", "RoomId")
                Dim dSaleTotal As Decimal = 0
                Dim dCostotal As Decimal = 0
                If dtSaleTotal.Rows.Count > 0 Then
                    For s As Integer = 0 To dtSaleTotal.Rows.Count - 1
                        dSaleTotal = dSaleTotal + Convert.ToDecimal(dtSaleTotal.Rows(s)("saletotal"))
                        dCostotal = dCostotal + Convert.ToDecimal(dtSaleTotal.Rows(s)("costtotal"))
                    Next
                    dsHotelRoomType.Tables(2).Rows(hh)("saletotal") = dSaleTotal
                    dsHotelRoomType.Tables(2).Rows(hh)("costtotal") = dCostotal

                End If

                dsHotelRoomType.Tables(2).Rows(hh)("partycode") = dvTotal.Item(0)("partycode")
                dsHotelRoomType.Tables(2).Rows(hh)("rmtypcode") = dvTotal.Item(0)("rmtypcode")
                dsHotelRoomType.Tables(2).Rows(hh)("mealplans") = dvTotal.Item(0)("mealplans")
            End If

         


        Next

            If dtRemove.Rows.Count > 0 And dsHotelRoomType.Tables(0).Rows.Count > 0 Then
                Dim dtRemove_Distinct As DataTable = New DataView(dtRemove).ToTable(True, "partycode", "rmtypcode", "mealplans", "rateplanid", "RoomId")
                'For Each r As DataRow In dsHotelRoomType.Tables(0).Rows
                For i = dsHotelRoomType.Tables(0).Rows.Count - 1 To 0 Step -1
                    Dim DrResult As Array = dtRemove_Distinct.Select("partycode='" & dsHotelRoomType.Tables(0).Rows(i)("Int_partycode") & "' and rmtypcode='" & dsHotelRoomType.Tables(0).Rows(i)("Int_rmtypecode") & "'  and mealplans='" & dsHotelRoomType.Tables(0).Rows(i)("Int_mealcode") & "'  and rateplanid='" & dsHotelRoomType.Tables(0).Rows(i)("rateplanid") & "'  and RoomId='" & dsHotelRoomType.Tables(0).Rows(i)("RoomId") & "'")
                    If DrResult.Length > 0 Then
                        dsHotelRoomType.Tables(0).Rows.Remove(dsHotelRoomType.Tables(0).Rows(i))
                    ElseIf dsHotelRoomType.Tables(0).Rows(i)("salevalue") = "0" Then
                        dsHotelRoomType.Tables(0).Rows.Remove(dsHotelRoomType.Tables(0).Rows(i))
                    End If
                    If dsHotelRoomType.Tables(0).Rows.Count = 0 Then
                        Exit For
                    End If
                Next
            End If
            For i = dsHotelRoomType.Tables(0).Rows.Count - 1 To 0 Step -1
                If dsHotelRoomType.Tables(0).Rows(i)("salevalue") = "0" Then
                    dsHotelRoomType.Tables(0).Rows.Remove(dsHotelRoomType.Tables(0).Rows(i))
                End If
                If dsHotelRoomType.Tables(0).Rows.Count = 0 Then
                    Exit For
                End If
            Next

            Return dsHotelRoomType
    End Function
 
End Class

