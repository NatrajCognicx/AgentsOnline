Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System

Partial Class VisaBooking
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLguest As New BLLGuest

    Dim objBLLTransferSearch As New BLLTransferSearch
    Dim objBLLTourSearch As New BLLTourSearch
    Dim objBLLVisa As New BLLVISA
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim objBLLHotelSearch As New BLLHotelSearch
    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Dim objUtil As New clsUtils
    Dim totalvisas As Integer
    Public visascount As Integer = 0
    Dim objResParam As New ReservationParameters
    ' Protected WithEvents lbMinLengthStay As Global.System.Web.UI.WebControls.LinkButton

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '  Session("selectedtourdatatable") = Nothing

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
                'If Not Session("sFinalBooked") Is Nothing Then 'this part is commented by mohamed on 17/11/2018 for testing
                '    clearallBookingSessions()
                '    Session("sFinalBooked") = Nothing '"0" 'changed by mohamed on 12/08/2018

                'End If
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
                CreateDataTable()
                If Not Request.QueryString("FreeForm") Is Nothing Then ' Modified by abin on 20180728
                    If Request.QueryString("FreeForm") = "1" Then
                        lblheading.Text = "VISA BOOKING- FREE FORM BOOKING"
                        chkOveridePrice.Checked = True
                        dvchkoverride.Visible = False
                        ViewState("vFreeForm") = "FreeForm"
                    End If
                End If
                  LoadHome()





            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("VisaBooking.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub
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
    Private Sub CreateDataTable()

        Dim dt As New DataTable

        dt.Columns.Add("VlineNo", GetType(String))
        dt.Columns.Add("NoofVisas", GetType(String))
        dt.Columns.Add("NationalityCode", GetType(String))

        dt.Columns.Add("SupplierName", GetType(String)) '*** Danny 22/10/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        dt.Columns.Add("SupplierAgentCode", GetType(String)) '*** Danny 22/10/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        dt.Columns.Add("SupplierAgentName", GetType(String)) '*** Danny 22/10/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        dt.Columns.Add("VisaOptions", GetType(String))
        dt.Columns.Add("VisaTypeCode", GetType(String))
        dt.Columns.Add("VisaDate", GetType(String))
        dt.Columns.Add("VisaPrice", GetType(String))
        dt.Columns.Add("VisaValue", GetType(String))
        dt.Columns.Add("OPlistCode", GetType(String))
        dt.Columns.Add("ComplimentaryCust", GetType(Integer))
        dt.Columns.Add("OCPlistCode", GetType(String))
        dt.Columns.Add("Visacprice", GetType(String))
        dt.Columns.Add("preferredsupplier", GetType(String))
        dt.Columns.Add("wlcurrcode", GetType(String))
        dt.Columns.Add("wlmarkupperc", GetType(String))
        dt.Columns.Add("wlconvrate", GetType(String))
        dt.Columns.Add("wlvisaprice", GetType(String))
        dt.Columns.Add("wlvisavalue", GetType(String))
        dt.Columns.Add("visacostvalue", GetType(String))

        ''' Added 05/04/18 shahul
        dt.Columns.Add("child", GetType(String))
        dt.Columns.Add("Childage1", GetType(String))
        dt.Columns.Add("Childage2", GetType(String))
        dt.Columns.Add("Childage3", GetType(String))
        dt.Columns.Add("Childage4", GetType(String))
        dt.Columns.Add("Childage5", GetType(String))
        dt.Columns.Add("Childage6", GetType(String))

        dt.Columns.Add("Visachildprice", GetType(String))
        dt.Columns.Add("VisachildCostprice", GetType(String))

        Session.Add("VisaDetailsDT", dt)

    End Sub
    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
        Try
            If Session("sRequestId") Is Nothing And CType(Session("sEditRequestId"), String) Is Nothing Then 'CType(Session("sEditRequestId"), String) is added by mohamed on 17/11/2018
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
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: btnaddrow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    'Protected Overrides Sub OnInit(ByVal e As EventArgs)
    '    MyBase.OnInit(e)

    'End Sub
    Private Sub bindvisadetailsforAmend(ByVal requestid As String, ByVal vlineno As String)
        Dim strName As String = ""
        Dim iRowNo As Integer = 0

        Dim dt As New DataTable
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                divvisacustomer.Style.Add("display", "none")
                dvvisacustomercode.Style.Add("display", "none")
                dvsourcectry.Style.Add("margin-left", "30px")
                dvchkoverride.Style.Add("margin-left", "-535px")
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString


            End If
        End If



        Session("VisaDetailsDT") = Nothing



        Dim strQuery As String = ""
        ' ''' Added 05/04/18 shahul
        '--*** Danny 22/10/2018 FreeForm SupplierAgent"
        strQuery = "select  Vlineno,  noofvisas, t.nationalitycode,c.ctryname nationalityname ,visaoptions, visatypecode,ot.othtypname visatypename,   convert(varchar(10),visadate,103) visadate , visaprice, visavalue, oplistcode,  " _
            & " complimentarycust,OCPlistCode,Visacprice,ISNULL(preferredsupplier,'') preferredsupplier,wlcurrcode,wlmarkupperc,wlconvrate,wlvisaprice,wlvisavalue,visacostvalue,adults,child,childages " _
            & " ,ISNULL(partyname,'') SupplierName,ISNULL(SupplierAgentCode,'') SupplierAgentCode,ISNULL(supagentname,'') SupplierAgentName  ,visachildprice,visacchildprice" _
            & " from  ctrymast c(nolock),booking_visatemp t(nolock) " _
            & " LEFT OUTER JOIN supplier_agents AA(nolock)  ON AA.supagentcode =t.SupplierAgentCode " _
            & " LEFT OUTER JOIN partymast B(nolock)  ON B.partycode =t.preferredsupplier " _
            & " left join othtypmast ot on ot.othtypcode=t.visatypecode  " _
            & " where   t.nationalitycode=c.ctrycode and  requestid='" & CType(requestid, String) & "' and vlineno=" & vlineno

        ' ''' '*** Danny 22/10/2018 FreeForm SupplierAgent * NOT USED Yet
        'strQuery = "select '' Vlineno, '' noofvisas,'' nationalitycode, '' nationalityname," _
        '     & "'' supagentcode,'' supagentname,'' supagentcode,'' supagentname," _
        '     & "'' visaoptions,'' visatypecode, '' visatypename, ''  visadate , '' visaprice,'' visavalue,'' oplistcode,  " _
        '     & " '' complimentarycust, '' OCPlistCode ,'' Visacprice,'' preferredsupplier,'' wlcurrcode,'' wlmarkupperc,'' wlconvrate,'' wlvisaprice,'' wlvisavalue,'' visacostvalue  , '' child , " _
        '     & " '' childage1 ,'' childage2 ,'' childage3, '' childage4, '' childage5  , '' PriceVATPerc , '' PriceWithTax, '' , '' CostVATPerc, '' CostWithTax, '' PriceTaxableValue, '' PriceNonTaxableValue, '' PriceVATValue, '' CostTaxableValue, '' CostNonTaxableValue, '' CostVATValue "  'from booking_visatemp where requestid='" & CType(requestid, String) & "'"

        dt = objclsUtilities.GetDataFromDataTable(strQuery)

        If dt.Rows.Count > 0 Then
            dlVisaInfo.DataSource = dt
            dlVisaInfo.DataBind()
        End If

        ' iRowNo = iRowNo + 1
        '  Dim row As DataRow = dt.NewRow()
        'Dim row As DataRow = dt.Select.First
        'row("Vlineno") = (iRowNo).ToString

        ' dt.Rows.Add(row)

        '  End If






    End Sub
    Private Sub bindvisadetails(ByVal requestid As String)
        Dim strName As String = ""
        Dim iRowNo As Integer = 0

        Dim dt As New DataTable
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                divvisaCustomer.Style.Add("display", "none")
                dvvisacustomercode.Style.Add("display", "none")
                dvsourcectry.Style.Add("margin-left", "30px")
                dvchkoverride.Style.Add("margin-left", "-535px")
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString


            End If
        End If



        Session("VisaDetailsDT") = Nothing



        Dim strQuery As String = ""
        'strQuery = "select Vlineno,noofvisas,nationalitycode,visaoptions,visatypecode, convert(varchar(10), visadate,103) as visadate ,visaprice,visavalue,oplistcode, complimentarycust from booking_visatemp where requestid='" & CType(requestid, String) & "'"
        ''' Added 05/04/18 shahul
        ''' '*** Danny 22/10/2018 FreeForm SupplierAgent
        strQuery = "select '' Vlineno, '' noofvisas,'' nationalitycode, '' nationalityname," _
             & " '' SupplierName,'' SupplierAgentCode,'' SupplierAgentName," _
             & " '' visaoptions,'' visatypecode, '' visatypename, ''  visadate , '' visaprice,'' visavalue,'' oplistcode,  " _
             & " '' complimentarycust, '' OCPlistCode ,'' Visacprice,'' preferredsupplier,'' wlcurrcode,'' wlmarkupperc,'' wlconvrate,'' wlvisaprice,'' wlvisavalue,'' visacostvalue  , '' child , " _
             & " '' childage1 ,'' childage2 ,'' childage3, '' childage4, '' childage5  , '' PriceVATPerc , '' PriceWithTax, '' , '' CostVATPerc, '' CostWithTax, '' PriceTaxableValue, '' PriceNonTaxableValue, '' PriceVATValue, '' CostTaxableValue, '' CostNonTaxableValue, '' CostVATValue "  'from booking_visatemp where requestid='" & CType(requestid, String) & "'"

        dt = objclsUtilities.GetDataFromDataTable(strQuery)




        iRowNo = iRowNo + 1
        '  Dim row As DataRow = dt.NewRow()
        Dim row As DataRow = dt.Select.First
        row("Vlineno") = (iRowNo).ToString

        ' dt.Rows.Add(row)
        dlVisaInfo.DataSource = dt
        dlVisaInfo.DataBind()
        '  End If






    End Sub
    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        hdChildAgeLimit.Value = objResParam.ChildAgeLimit
        ''  LoadRoomAdultChild()
        LoadFields()

        Dim strQuery As String = ""
        ViewState("VLineNo") = Request.QueryString("VLineNo")
        hdnlineno.Value = Request.QueryString("VLineNo")

        If Not Session("sRequestid") Is Nothing Then
            strQuery = "select sum(noofvisas) from booking_visatemp(nolock) where requestid='" & CType(Session("sRequestId"), String) & "'"
            ViewState("visascount") = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
        Else
            strQuery = "select sum(noofvisas) from booking_visatemp(nolock) where requestid='" & CType(Session("sEditRequestId"), String) & "'"
            ViewState("visascount") = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
        End If

        Dim dt As New DataTable

        objBLLVisa = New BLLVISA

        '*** Danny 22/10/2018 FreeForm SupplierAgent
        Dim ds_ As New DataSet
        ds_ = objclsUtilities.GetDataSet("SP_SelectDefaultSupplierAgent", Nothing)
        Session("strSupplierAgentCode") = ""
        Session("strSupplierAgentName") = ""
        If Not ds_ Is Nothing Then
            If ds_.Tables(0).Rows.Count > 0 Then
                Session("strSupplierAgentCode") = ds_.Tables(0).Rows(0)("supagentcode").ToString
                Session("strSupplierAgentName") = ds_.Tables(0).Rows(0)("supagentname").ToString
            End If
        End If
        Session("strDefaultCurrency") = objclsUtilities.ExecuteQueryReturnSingleValue("select isnull(option_selected,0) from reservation_parameters where param_id =457")


        If Not Session("sEditRequestId") Is Nothing Then

            If Val(hdnlineno.Value) = 0 Then
                NewHeaderFill()
            Else
                Amendheaderfill()
            End If
        Else
            If Not Session("sRequestId") Is Nothing Then

                If Val(hdnlineno.Value) = 0 Then
                    NewHeaderFill()

                Else
                    EditHeaderFill()
                End If
            End If
        End If



        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If
    End Sub
    Private Sub EditHeaderFill()
        Dim dt As New DataTable
        Try
            Dim strrequestid As String = ""
            strrequestid = GetExistingRequestId()

            dt = objBLLVisa.GetEditBookingDetails(strrequestid, Request.QueryString("VLineNo"))
            If dt.Rows.Count > 0 Then

                txtVisaCustomer.Text = dt.Rows(0).Item("agentcode")
                txtVisaCustomer.Text = dt.Rows(0).Item("agentname")
                txtSourceCountryCode.Text = dt.Rows(0).Item("sourcectrycode")
                txtSourceCountry.Text = dt.Rows(0).Item("sourcectryname")
                chkOveridePrice.Checked = IIf(dt.Rows(0).Item("reqoverride") = 1, True, False)

                totalvisas = dt.Rows(0).Item("totalvisas")
                ViewState("Totalvisas") = dt.Rows(0).Item("totalvisas")
                bindvisadetailsforAmend(strrequestid, Request.QueryString("VLineNo"))
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: Editheaderfill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Private Sub Amendheaderfill()
        Dim dt As New DataTable
        Try

            dt = objBLLVisa.GetEditBookingDetails(Session("sEditRequestId"), Request.QueryString("VLineNo"))
            If dt.Rows.Count > 0 Then

                txtVisaCustomer.Text = dt.Rows(0).Item("agentcode")
                txtVisaCustomer.Text = dt.Rows(0).Item("agentname")
                txtSourceCountryCode.Text = dt.Rows(0).Item("sourcectrycode")
                txtSourceCountry.Text = dt.Rows(0).Item("sourcectryname")
                chkOveridePrice.Checked = IIf(dt.Rows(0).Item("reqoverride") = 1, True, False)

                totalvisas = dt.Rows(0).Item("totalvisas")
                ViewState("Totalvisas") = dt.Rows(0).Item("totalvisas")
                bindvisadetailsforAmend(Session("sEditRequestId"), Request.QueryString("VLineNo"))
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: Amendheaderfill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Private Sub NewHeaderFill()
        Dim dt As New DataTable
        Dim strQuery As String = ""
        Dim dtpax As New DataTable

        Try

            If Not Session("sRequestId") Is Nothing Then

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(CType(Session("sRequestId"), String))

                txtVisaCustomerCode.Text = dt.Rows(0).Item("agentcode")
                txtVisaCustomer.Text = dt.Rows(0).Item("agentname")
                txtSourceCountryCode.Text = dt.Rows(0).Item("sourcectrycode")
                txtSourceCountry.Text = dt.Rows(0).Item("sourcectryname")

                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sRequestId") & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 Then
                    totalvisas = Val(dtpax.Rows(0).Item("adults")) + Val(dtpax.Rows(0).Item("child"))

                    ViewState("Totalvisas") = Val(dtpax.Rows(0).Item("adults")) + Val(dtpax.Rows(0).Item("child"))
                    ViewState("Adultvisas") = Val(dtpax.Rows(0).Item("adults"))
                    ViewState("Childvisas") = Val(dtpax.Rows(0).Item("child"))
                End If


                If Not dt Is Nothing Then
                    bindvisadetails(dt.Rows(0).Item("requestid"))

                End If
            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: NewHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Sub txtNationality_click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim myButton As TextBox = CType(sender, TextBox)

        Dim dlItem As DataListItem = CType(myButton.NamingContainer, DataListItem)
        Dim lblrowno As Label = CType(dlItem.FindControl("lblrowno"), Label)
        Dim txtnationalitycode As TextBox = CType(dlItem.FindControl("txtnationalitycode"), TextBox)
        Dim txtNationality As TextBox = CType(dlItem.FindControl("txtNationality"), TextBox)
        Dim visaremarks As String = objclsUtilities.ExecuteQueryReturnStringValue("select visaremarks from ctrymast where ctrycode='" & txtnationalitycode.Text & "'")
        If visaremarks <> "" Then
            MessageBox.ShowMessage(Page, MessageType.Info, "Choosen Nationality...</Br></br>" + txtNationality.Text + " -- " + visaremarks)
        End If
    End Sub

    Private Sub BindVisaDetails()
        Dim rowIndex As Integer = 0
        If Session("VisaDetailsDT") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(Session("VisaDetailsDT"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim dlitem As DataListItem = dlVisaInfo.Items(i)
                    Dim ddlVisatype As DropDownList = CType(dlitem.FindControl("ddlVisatype"), DropDownList)
                    Dim ddlVisa As DropDownList = CType(dlitem.FindControl("ddlVisa"), DropDownList)
                    Dim txtVisaPrice As TextBox = CType(dlitem.FindControl("txtVisaPrice"), TextBox)
                    Dim txtVisaChildPrice As TextBox = CType(dlitem.FindControl("txtVisaChildPrice"), TextBox)
                    Dim txtVisaValue As TextBox = CType(dlitem.FindControl("txtVisaValue"), TextBox)
                    Dim hdnplistcode As HiddenField = CType(dlitem.FindControl("hdnplistcode"), HiddenField)
                    Dim txtNoofVisas As TextBox = CType(dlitem.FindControl("txtNoofVisas"), TextBox)
                    Dim txtNationalityCode As TextBox = CType(dlitem.FindControl("txtNationalityCode"), TextBox)

                    '*** Danny 22/10/2018 FreeForm SupplierAgent
                    Dim TxtSupplierCode As TextBox = CType(dlitem.FindControl("Txt_SupplierCode"), TextBox)
                    Dim TxtSupplierName As TextBox = CType(dlitem.FindControl("Txt_SupplierName"), TextBox)
                    Dim TxtSupplierAgentCode As TextBox = CType(dlitem.FindControl("Txt_SupplierAgentCode"), TextBox)
                    Dim TxtSupplierAgentName As TextBox = CType(dlitem.FindControl("Txt_SupplierAgentName"), TextBox)
                    Dim lblCostPrice As Label = CType(dlitem.FindControl("lblCostPrice"), Label)
                    Dim lblCostTotal As Label = CType(dlitem.FindControl("lblCostTotal"), Label)
                    If Not Session("strDefaultCurrency") Is Nothing Then
                        If Session("strDefaultCurrency").ToString.Trim.Length > 0 Then
                            lblCostPrice.Text = lblCostPrice.Text.Replace("---", Session("strDefaultCurrency").ToString.Trim)
                            lblCostTotal.Text = lblCostTotal.Text.Replace("---", Session("strDefaultCurrency").ToString.Trim)
                        End If
                    End If


                    Dim txtvisadate As TextBox = CType(dlitem.FindControl("txtvisadate"), TextBox)
                    Dim txtNationality As TextBox = CType(dlitem.FindControl("txtNationality"), TextBox)
                    Dim chkcomplementary As CheckBox = CType(dlitem.FindControl("chkComplementCust"), CheckBox)
                    Dim lblrowno As Label = CType(dlitem.FindControl("lblrowno"), Label)

                    Dim txtwlVisaPrice As TextBox = CType(dlitem.FindControl("txtwlVisaPrice"), TextBox)
                    Dim txtwlVisaValue As TextBox = CType(dlitem.FindControl("txtwlVisaValue"), TextBox)

                    Dim hdncplistcode As HiddenField = CType(dlitem.FindControl("hdncplistcode"), HiddenField)
                    Dim hdncostprice As HiddenField = CType(dlitem.FindControl("hdncostprice"), HiddenField)
                    Dim hdncostchildprice As HiddenField = CType(dlitem.FindControl("hdncostchildprice"), HiddenField)
                    Dim hdnpartycode As HiddenField = CType(dlitem.FindControl("hdnpartycode"), HiddenField)

                    Dim hdnwlcurrcode As HiddenField = CType(dlitem.FindControl("hdnwlcurrcode"), HiddenField)
                    Dim hdnwlmarkup As HiddenField = CType(dlitem.FindControl("hdnwlmarkup"), HiddenField)
                    Dim hdnwlconvrate As HiddenField = CType(dlitem.FindControl("hdnwlconvrate"), HiddenField)
                    Dim hdVisaCostValue As HiddenField = CType(dlitem.FindControl("hdVisaCostValue"), HiddenField)
                    Dim hdnChildcostprice As HiddenField = CType(dlitem.FindControl("hdnChildcostprice"), HiddenField)
                    ''' Added 05/04/18 shahul
                    Dim ddlchild As DropDownList = CType(dlitem.FindControl("ddlchild"), DropDownList)
                    Dim txtchildage1 As TextBox = CType(dlitem.FindControl("txtchildage1"), TextBox)
                    Dim txtchildage2 As TextBox = CType(dlitem.FindControl("txtchildage2"), TextBox)
                    Dim txtchildage3 As TextBox = CType(dlitem.FindControl("txtchildage3"), TextBox)
                    Dim txtchildage4 As TextBox = CType(dlitem.FindControl("txtchildage4"), TextBox)
                    Dim txtchildage5 As TextBox = CType(dlitem.FindControl("txtchildage5"), TextBox)
                    Dim txtchildage6 As TextBox = CType(dlitem.FindControl("txtchildage6"), TextBox)
                    Dim dvChild1 As HtmlGenericControl = CType(dlitem.FindControl("dvChild1"), HtmlGenericControl)
                    Dim dvChild2 As HtmlGenericControl = CType(dlitem.FindControl("dvChild2"), HtmlGenericControl)
                    Dim dvChild3 As HtmlGenericControl = CType(dlitem.FindControl("dvChild3"), HtmlGenericControl)
                    Dim dvChild4 As HtmlGenericControl = CType(dlitem.FindControl("dvChild4"), HtmlGenericControl)
                    Dim dvChild5 As HtmlGenericControl = CType(dlitem.FindControl("dvChild5"), HtmlGenericControl)
                    Dim dvChild6 As HtmlGenericControl = CType(dlitem.FindControl("dvChild6"), HtmlGenericControl)
                    Dim chdage As HtmlGenericControl = CType(dlitem.FindControl("chdage"), HtmlGenericControl)







                    If (dt.Rows(i)("nationalitycode").ToString()) <> "" Then
                        lblrowno.Text = dt.Rows(i)("Vlineno").ToString()
                        txtNoofVisas.Text = dt.Rows(i)("NoofVisas").ToString()
                        ''' Added 05/04/18 shahul
                        ddlchild.SelectedValue = IIf(dt.Rows(i)("child").ToString() = "0", "--", dt.Rows(i)("child").ToString())

                        Dim str As String = ddlchild.SelectedValue
                        Select Case str
                            Case "--"
                                dvChild1.Style.Add("display", "none")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "none")
                            Case 1
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "block")
                            Case 2
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "block")
                            Case 3
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "block")
                            Case 4
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "block")
                            Case 5
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "block")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "block")
                            Case 6
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "block")
                                dvChild6.Style.Add("display", "block")
                                chdage.Style.Add("display", "block")
                        End Select

                        txtNationalityCode.Text = dt.Rows(i)("nationalitycode").ToString()

                        txtNationality.Text = objclsUtilities.ExecuteQueryReturnStringValue("select   ctryname from ctrymast where ctrycode='" & dt.Rows(i)("nationalitycode").ToString() & "'")

                        '*** Danny 22/10/2018 FreeForm SupplierAgent
                        TxtSupplierCode.Text = dt.Rows(i)("preferredsupplier").ToString()
                        TxtSupplierName.Text = dt.Rows(i)("SupplierName").ToString()
                        TxtSupplierAgentCode.Text = dt.Rows(i)("SupplierAgentCode").ToString()
                        TxtSupplierAgentName.Text = dt.Rows(i)("SupplierAgentName").ToString()

                        ddlVisa.SelectedValue = dt.Rows(i)("VisaOptions").ToString()
                        ddlVisatype.SelectedValue = dt.Rows(i)("VisaTypeCode").ToString()
                        txtvisadate.Text = dt.Rows(i)("VisaDate").ToString()
                        If dt.Rows(i)("VisaPrice").ToString() <> "" Then
                            txtVisaPrice.Text = dt.Rows(i)("VisaPrice").ToString()
                            txtVisaPrice.Text = Math.Round(Convert.ToDecimal(dt.Rows(i)("visaprice").ToString()), 2)
                            txtVisaChildPrice.Text = Math.Round(Convert.ToDecimal(dt.Rows(i)("visachildprice").ToString()), 2)

                        End If

                        If dt.Rows(i)("wlVisaPrice").ToString() <> "" Then
                            txtwlVisaPrice.Text = dt.Rows(i)("wlVisaPrice").ToString()
                            txtwlVisaPrice.Text = Math.Round(Convert.ToDecimal(dt.Rows(i)("wlVisaPrice").ToString()), 2)


                        End If

                        If dt.Rows(i)("VisaValue").ToString() <> "" Then
                            txtVisaValue.Text = dt.Rows(i)("VisaValue").ToString()
                            txtVisaValue.Text = Math.Round(Convert.ToDecimal(dt.Rows(i)("visavalue").ToString()), 2)



                        End If
                        If dt.Rows(i)("wlVisaValue").ToString() <> "" Then
                            txtwlVisaValue.Text = dt.Rows(i)("wlVisaValue").ToString()
                            txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(dt.Rows(i)("wlVisaValue").ToString()), 2)


                        End If
                        hdnplistcode.Value = dt.Rows(i)("Oplistcode").ToString()
                        hdncplistcode.Value = dt.Rows(i)("OCplistcode").ToString()
                        If dt.Rows(i)("Visacprice").ToString() <> "" Then
                            hdncostprice.Value = Math.Round(Convert.ToDecimal(dt.Rows(i)("Visacprice").ToString()), 2) 'dt.Rows(i)("Visacprice").ToString()
                            hdnChildcostprice.Value = Math.Round(Convert.ToDecimal(dt.Rows(i)("Visacchildprice").ToString()), 2) 'dt.Rows(i)("Visacprice").ToString()
                        End If
                        hdVisaCostValue.Value = dt.Rows(i)("visacostvalue").ToString()
                        hdnpartycode.Value = dt.Rows(i)("preferredsupplier").ToString()
                        hdnwlcurrcode.Value = dt.Rows(i)("wlcurrcode").ToString()
                        hdnwlmarkup.Value = dt.Rows(i)("wlmarkupperc").ToString()
                        hdnwlconvrate.Value = dt.Rows(i)("wlconvrate").ToString()

                        chkcomplementary.Checked = IIf((dt.Rows(i)("ComplimentaryCust").ToString() = "1"), True, False)

                    Else
                        txtNoofVisas.Text = dt.Rows(i)("NoofVisas").ToString()

                        '*** Danny 22/10/2018 FreeForm SupplierAgent
                        TxtSupplierCode.Text = dt.Rows(i)("preferredsupplier").ToString()
                        TxtSupplierName.Text = dt.Rows(i)("SupplierName").ToString()
                        TxtSupplierAgentCode.Text = dt.Rows(i)("SupplierAgentCode").ToString()
                        TxtSupplierAgentName.Text = dt.Rows(i)("SupplierAgentName").ToString()

                        ''' Added 05/04/18 shahul
                        ddlchild.SelectedValue = IIf(dt.Rows(i)("child").ToString() = "0", "--", dt.Rows(i)("child").ToString())
                        Select Case ddlchild.SelectedValue
                            Case "--"
                                dvChild1.Style.Add("display", "none")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "none")
                            Case 1
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 2
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 3
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 4
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 5
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "block")
                                dvChild6.Style.Add("display", "none")
                            Case 6
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "block")
                                dvChild6.Style.Add("display", "block")
                        End Select
                        Exit For
                    End If
                    'ddlVisatype.Focus()
                Next
            End If
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
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
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

                    'If myDS.Tables(0).Rows.Count = 1 Then

                    'Else
                    '    txtSourceCountry.ReadOnly = False
                    '    AutoCompleteExtender_txtSourceCountry.Enabled = True
                    'End If


                Catch ex As Exception

                End Try
            Else

                ' dvTourOveridePrice.Visible = True
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub


    Private Sub BindVisaType(ByVal strCheckIn As String, ByVal ddlVisatype As DropDownList)
        Dim strQuery As String = ""
        If strCheckIn <> "" Then
            Dim strDates As String() = strCheckIn.Split("/")
            strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        strQuery = "select distinct Visacode,visaname from view_visa_types where '" & strCheckIn & "' between fromdate and todate"
        objclsUtilities.FillDropDownList(ddlVisatype, strQuery, "Visacode", "visaname", True, "--")
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function SelectVisa(ByVal strNationality As String) As String
        Dim strVisaOnArival As String = ""
        strVisaOnArival = clsUtilities.SharedExecuteQueryReturnStringValue("select count(CtryCode)cnt from VisaOnArrivalCountries where  CHARINDEX('" & strNationality & "',CtryCode)>0  ")
        If strVisaOnArival = "" Then
            strVisaOnArival = "0"
        End If
        Return strVisaOnArival
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetNationality(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Nationality As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select ctrycode,ctryname from ctrymast where active=1 and ctryname like  '" & prefixText & "%' "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Nationality.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("ctryname").ToString(), myDS.Tables(0).Rows(i)("ctrycode").ToString()))
                Next

            End If
            Return Nationality
        Catch ex As Exception
            Return Nationality
        End Try

    End Function



    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        'Dim strScript As String = "javascript: CallPriceSlider();"
        'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)


    End Sub




    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSTourSearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sTourPageIndex") = pageIndex.ToString
            '  BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

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

        ''rptPager.DataSource = pages
        ''rptPager.DataBind()
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
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub

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
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
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


    Private Sub BindTourMainDetails(ByVal dvMaiDetails As DataView)
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                Dim objBLLHotelSearch As New BLLHotelSearch
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
            End If
        End If
        Dim dt As New DataTable
        dt = dvMaiDetails.ToTable
        Dim dv As DataView = dt.DefaultView
        If dt.Rows.Count > 0 Then
            '' lblHotelCount.Text = dt.Rows.Count & " Records Found"
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

            ''    dlTourSearchResults.DataSource = dv
            ''    dlTourSearchResults.DataBind()

            ''Else
            ''    dlTourSearchResults.DataBind()
        End If
    End Sub




    Private Sub CreateSelectedTourDataTable()
        Dim SelectExcDT As DataTable = New DataTable("SelectedExc")
        SelectExcDT.Columns.Add("exctypcode", Type.GetType("System.String"))
        SelectExcDT.Columns.Add("excdate", Type.GetType("System.String"))
        Session("selectedtourdatatable") = SelectExcDT
    End Sub

    Protected Sub ddlVisa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlVisa As DropDownList = CType(sender, DropDownList)
        Dim dlItem As DataListItem = CType(ddlVisa.NamingContainer, DataListItem)
        Dim ddlVisatype As DropDownList = CType(dlItem.FindControl("ddlVisatype"), DropDownList)
        Dim txtVisaPrice As TextBox = CType(dlItem.FindControl("txtVisaPrice"), TextBox)
        Dim txtVisaValue As TextBox = CType(dlItem.FindControl("txtVisaValue"), TextBox)
        Dim hdnplistcode As HiddenField = CType(dlItem.FindControl("hdnplistcode"), HiddenField)
        Dim txtNoofVisas As TextBox = CType(dlItem.FindControl("txtNoofVisas"), TextBox)
        Dim txtNationalityCode As TextBox = CType(dlItem.FindControl("txtNationalityCode"), TextBox)

        Dim hdncplistcode As HiddenField = CType(dlItem.FindControl("hdncplistcode"), HiddenField)
        Dim hdncostprice As HiddenField = CType(dlItem.FindControl("hdncostprice"), HiddenField)
        Dim hdnpartycode As HiddenField = CType(dlItem.FindControl("hdnpartycode"), HiddenField)

        Dim hdnwlcurrcode As HiddenField = CType(dlItem.FindControl("hdnwlcurrcode"), HiddenField)
        Dim hdnwlmarkup As HiddenField = CType(dlItem.FindControl("hdnwlmarkup"), HiddenField)
        Dim hdnwlconvrate As HiddenField = CType(dlItem.FindControl("hdnwlconvrate"), HiddenField)

        Dim hdnChildcostprice As HiddenField = CType(dlItem.FindControl("hdnChildcostprice"), HiddenField)

        Dim dvVisaPrice As HtmlGenericControl = CType(dlItem.FindControl("dvVisaPrice"), HtmlGenericControl)
        Dim dvwlVisaPrice As HtmlGenericControl = CType(dlItem.FindControl("dvwlVisaPrice"), HtmlGenericControl)
        Dim dvVisaValue As HtmlGenericControl = CType(dlItem.FindControl("dvVisaValue"), HtmlGenericControl)
        Dim dvwlVisaValue As HtmlGenericControl = CType(dlItem.FindControl("dvwlVisaValue"), HtmlGenericControl)
        Dim txtwlVisaValue As TextBox = CType(dlItem.FindControl("txtwlVisaValue"), TextBox)
        Dim txtwlVisaPrice As TextBox = CType(dlItem.FindControl("txtwlVisaPrice"), TextBox)
        Dim txtVisaChildPrice As TextBox = CType(dlItem.FindControl("txtVisaChildPrice"), TextBox)

        Dim txtVisaDate As TextBox = CType(dlItem.FindControl("txtVisaDate"), TextBox)

        txtVisaPrice.Text = ""
        txtVisaValue.Text = ""
        txtNoofVisas.Text = IIf(Val(txtNoofVisas.Text) = 0, "", txtNoofVisas.Text)
        If ddlVisa.SelectedValue = "Required" Then

            ddlVisatype.SelectedValue = "V-000001"

            Dim visapriceplistcode As String = GetVisaPrice(ddlVisatype.SelectedValue, txtNationalityCode.Text, "Adult")

            Dim visaChildpriceplistcode As String = GetVisaPrice(ddlVisatype.SelectedValue, txtNationalityCode.Text, "Child")

            If visapriceplistcode <> "" Then
                hdnplistcode.Value = visapriceplistcode.Split("|")(0)
                If Not Request.QueryString("FreeForm") Is Nothing Then ' Modified by abin on 20180728
                    If Request.QueryString("FreeForm") = "1" Then
                        txtVisaPrice.Text = "0"
                        txtwlVisaPrice.Text = "0"
                        txtVisaChildPrice.Text = "0"
                    End If
                Else

                    txtVisaPrice.Text = visapriceplistcode.Split("|")(1)
                    txtwlVisaPrice.Text = visapriceplistcode.Split("|")(1)
                    txtVisaChildPrice.Text = visaChildpriceplistcode.Split("|")(1)
                End If

            End If

        Else
            ddlVisatype.SelectedIndex = 0
            txtVisaPrice.Text = ""
            txtwlVisaPrice.Text = ""
            txtVisaChildPrice.Text = ""

        End If

        ''' Cost Price for Cummulative
        Dim strQuery As String = ""
        Dim strChildQuery As String = ""
        If hdBookingEngineRateType.Value = "1" Then
            strQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisatype.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec  and othcatcode='VISACHG'"

            Dim strVisaPrice As String

            Dim visapricedt As New DataTable
            visapricedt = objclsUtilities.GetDataFromDataTable(strQuery)
            If visapricedt.Rows.Count > 0 Then
                hdncplistcode.Value = CType(visapricedt.Rows(0).Item("ocplistcode"), String)
                hdncostprice.Value = CType(visapricedt.Rows(0).Item("ocostprice"), String)
                hdnpartycode.Value = CType(visapricedt.Rows(0).Item("partycode"), String)

            End If

            strChildQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisatype.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec  and othcatcode='VISACHG-CH'"
            Dim visaChildpricedt As New DataTable
            visaChildpricedt = objclsUtilities.GetDataFromDataTable(strChildQuery)
            If visaChildpricedt.Rows.Count > 0 Then
                hdnChildcostprice.Value = CType(visaChildpricedt.Rows(0).Item("ocostprice"), String)
            End If

        End If

        ''whitelabel values
        Dim dt As New DataTable
        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(CType(Session("sRequestId"), String))

        If dt.Rows.Count > 0 Then
            txtVisaCustomerCode.Text = dt.Rows(0).Item("agentcode").ToString
            txtVisaCustomer.Text = dt.Rows(0).Item("agentname")
        End If


        Dim dWlMarkup As Decimal
        If hdWhiteLabel.Value = "1" Or Session("sLoginType") = "RO" Then
            Dim bookingheaderdt As New DataTable
            strQuery = "execute sp_booking_whitelabelagent '" & txtVisaCustomerCode.Text & "'"
            bookingheaderdt = objclsUtilities.GetDataFromDataTable(strQuery)
            If bookingheaderdt.Rows.Count > 0 Then
                hdnwlcurrcode.Value = CType(bookingheaderdt.Rows(0).Item("wlcurrcode"), String)
                hdnwlmarkup.Value = CType(bookingheaderdt.Rows(0).Item("wlmarkupperc"), String)
                hdnwlconvrate.Value = CType(bookingheaderdt.Rows(0).Item("wlconvrate"), String)
                dWlMarkup = ((100 + Convert.ToDecimal(hdnwlmarkup.Value)) / 100) '* Convert.ToDecimal(hdnwlconvrate.Value)

                txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(Val(txtNoofVisas.Text)), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
            End If
        End If

        If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

            dvVisaPrice.Style.Add("display", "none")
            dvwlVisaPrice.Style.Add("display", "block")
            dvVisaValue.Style.Add("display", "none")
            dvwlVisaValue.Style.Add("display", "block")


        ElseIf hdWhiteLabel.Value = "1" Then
            dvVisaPrice.Style.Add("display", "none")
            dvwlVisaPrice.Style.Add("display", "block")
            dvVisaValue.Style.Add("display", "none")
            dvwlVisaValue.Style.Add("display", "block")

            'txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
            'txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtNoofVisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
        Else

            If hdBookingEngineRateType.Value = "1" Then
                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "none")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "none")
            Else
                dvVisaPrice.Style.Add("display", "block")
                dvwlVisaPrice.Style.Add("display", "none")
                dvVisaValue.Style.Add("display", "block")
                dvwlVisaValue.Style.Add("display", "none")
            End If


        End If

        txtNoofVisas.Focus()

    End Sub


    Private Function GetVisaPrice(ByVal strSelectedValue As String, ByVal strNationality As String, ByVal strPaxType As String) As String
        Dim strCheckIn As String = ""
        Dim strCountryCode As String = ""
        Dim strAgentCode As String = ""

        If Not Session("sRequestId") Is Nothing Then


            Dim bookingheaderdt As New DataTable
            bookingheaderdt = objBLLCommonFuntions.GetBookingTempHeaderDetails(CType(Session("sRequestId"), String))
            If bookingheaderdt.Rows.Count > 0 Then
                strAgentCode = bookingheaderdt.Rows(0).Item("agentcode")
                strCheckIn = bookingheaderdt.Rows(0).Item("MinDate_")
                strCountryCode = bookingheaderdt.Rows(0).Item("sourcectrycode")
            End If
        End If


        Dim strQuery = ""

        If strPaxType = "Adult" Then
            strQuery = "select oplistcode,visaprice,pricecurrcode from view_visa_types where visacode='" & strSelectedValue & "' and convert(datetime,'" & strCheckIn & "',105) between fromdate and todate and (CHARINDEX('" & strNationality & "',countries)>0 or CHARINDEX('" & strAgentCode & "',agents)>0)  and othcatcode='VISACHG'"
        Else
            strQuery = "select oplistcode,visaprice,pricecurrcode from view_visa_types where visacode='" & strSelectedValue & "' and convert(datetime,'" & strCheckIn & "',105) between fromdate and todate and (CHARINDEX('" & strNationality & "',countries)>0 or CHARINDEX('" & strAgentCode & "',agents)>0)  and othcatcode='VISACHG-CH'"
        End If


        Dim strVisaPrice As String
        Dim visapricedt As New DataTable

        visapricedt = objclsUtilities.GetDataFromDataTable(strQuery)


        If visapricedt.Rows.Count > 0 Then
            strVisaPrice = CType(visapricedt.Rows(0).Item(0), String) + "|" + CType(visapricedt.Rows(0).Item(1), String) + "|" + CType(visapricedt.Rows(0).Item(2), String)
        Else

            strVisaPrice = ""
        End If
        ' SqlDr.Close()

        Return strVisaPrice
    End Function
    'Protected Sub ddlVisa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ddlVisa As DropDownList = CType(sender, DropDownList)
    '    Dim str As String = ddlVisa.SelectedValue

    '    If str = "Endorsed in Mothers Visa" Or str = "Endorsed in Fathers Visa" Then
    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Child photo is mandatory in the passport in order to apply.")
    '        ddlVisa.Focus()
    '    End If
    'End Sub
    Protected Sub ddlchild_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ddlchild As DropDownList = CType(sender, DropDownList)
        Dim str As String = ddlchild.SelectedValue
        Dim dlItem As DataListItem = CType(ddlchild.NamingContainer, DataListItem)
        Dim dvChild1 As HtmlGenericControl = CType(dlItem.FindControl("dvChild1"), HtmlGenericControl)
        Dim dvChild2 As HtmlGenericControl = CType(dlItem.FindControl("dvChild2"), HtmlGenericControl)
        Dim dvChild3 As HtmlGenericControl = CType(dlItem.FindControl("dvChild3"), HtmlGenericControl)
        Dim dvChild4 As HtmlGenericControl = CType(dlItem.FindControl("dvChild4"), HtmlGenericControl)
        Dim dvChild5 As HtmlGenericControl = CType(dlItem.FindControl("dvChild5"), HtmlGenericControl)
        Dim dvChild6 As HtmlGenericControl = CType(dlItem.FindControl("dvChild6"), HtmlGenericControl)
        Dim chdage As HtmlGenericControl = CType(dlItem.FindControl("chdage"), HtmlGenericControl)


        Select Case str
            Case "--"
                dvChild1.Style.Add("display", "none")
                dvChild2.Style.Add("display", "none")
                dvChild3.Style.Add("display", "none")
                dvChild4.Style.Add("display", "none")
                dvChild5.Style.Add("display", "none")
                dvChild6.Style.Add("display", "none")
                chdage.Style.Add("display", "none")

            Case 1
                dvChild1.Style.Add("display", "block")
                dvChild2.Style.Add("display", "none")
                dvChild3.Style.Add("display", "none")
                dvChild4.Style.Add("display", "none")
                dvChild5.Style.Add("display", "none")
                dvChild6.Style.Add("display", "none")
                chdage.Style.Add("display", "block")
            Case 2
                dvChild1.Style.Add("display", "block")
                dvChild2.Style.Add("display", "block")
                dvChild3.Style.Add("display", "none")
                dvChild4.Style.Add("display", "none")
                dvChild5.Style.Add("display", "none")
                dvChild6.Style.Add("display", "none")
                chdage.Style.Add("display", "block")
            Case 3
                dvChild1.Style.Add("display", "block")
                dvChild2.Style.Add("display", "block")
                dvChild3.Style.Add("display", "block")
                dvChild4.Style.Add("display", "none")
                dvChild5.Style.Add("display", "none")
                dvChild6.Style.Add("display", "none")
                chdage.Style.Add("display", "block")
            Case 4
                dvChild1.Style.Add("display", "block")
                dvChild2.Style.Add("display", "block")
                dvChild3.Style.Add("display", "block")
                dvChild4.Style.Add("display", "block")
                dvChild5.Style.Add("display", "none")
                dvChild6.Style.Add("display", "none")
                chdage.Style.Add("display", "block")
            Case 5
                dvChild1.Style.Add("display", "block")
                dvChild2.Style.Add("display", "block")
                dvChild3.Style.Add("display", "block")
                dvChild4.Style.Add("display", "block")
                dvChild5.Style.Add("display", "block")
                dvChild6.Style.Add("display", "none")
                chdage.Style.Add("display", "block")
            Case 6
                dvChild1.Style.Add("display", "block")
                dvChild2.Style.Add("display", "block")
                dvChild3.Style.Add("display", "block")
                dvChild4.Style.Add("display", "block")
                dvChild5.Style.Add("display", "block")
                dvChild6.Style.Add("display", "block")
                chdage.Style.Add("display", "block")
        End Select
        Dim txtVisaPrice As TextBox = CType(dlItem.FindControl("txtVisaPrice"), TextBox)
        Dim hdnplistcode As HiddenField = CType(dlItem.FindControl("hdnplistcode"), HiddenField)
        Dim txtNationalityCode As TextBox = CType(dlItem.FindControl("txtNationalityCode"), TextBox)
        Dim txtVisaValue As TextBox = CType(dlItem.FindControl("txtVisaValue"), TextBox)
        Dim txtnoofvisas As TextBox = CType(dlItem.FindControl("txtnoofvisas"), TextBox)
        Dim lblrowno As Label = CType(dlItem.FindControl("lblrowno"), Label)
        Dim txtNationality As TextBox = CType(dlItem.FindControl("txtNationality"), TextBox)

        Dim txtVisaDate As TextBox = CType(dlItem.FindControl("txtVisaDate"), TextBox)
        Dim hdncplistcode As HiddenField = CType(dlItem.FindControl("hdncplistcode"), HiddenField)
        Dim hdncostprice As HiddenField = CType(dlItem.FindControl("hdncostprice"), HiddenField)
        Dim hdnpartycode As HiddenField = CType(dlItem.FindControl("hdnpartycode"), HiddenField)

        Dim hdnwlcurrcode As HiddenField = CType(dlItem.FindControl("hdnwlcurrcode"), HiddenField)
        Dim hdnwlmarkup As HiddenField = CType(dlItem.FindControl("hdnwlmarkup"), HiddenField)
        Dim hdnwlconvrate As HiddenField = CType(dlItem.FindControl("hdnwlconvrate"), HiddenField)

        Dim dvVisaPrice As HtmlGenericControl = CType(dlItem.FindControl("dvVisaPrice"), HtmlGenericControl)
        Dim dvwlVisaPrice As HtmlGenericControl = CType(dlItem.FindControl("dvwlVisaPrice"), HtmlGenericControl)
        Dim dvVisaValue As HtmlGenericControl = CType(dlItem.FindControl("dvVisaValue"), HtmlGenericControl)
        Dim dvwlVisaValue As HtmlGenericControl = CType(dlItem.FindControl("dvwlVisaValue"), HtmlGenericControl)
        Dim txtwlVisaValue As TextBox = CType(dlItem.FindControl("txtwlVisaValue"), TextBox)
        Dim txtwlVisaPrice As TextBox = CType(dlItem.FindControl("txtwlVisaPrice"), TextBox)
        Dim ddlVisaType As DropDownList = CType(dlItem.FindControl("ddlVisaType"), DropDownList)
        Dim txtVisaChildPrice As TextBox = CType(dlItem.FindControl("txtVisaChildPrice"), TextBox)

        If ddlVisaType.Text = "--" Then
            txtVisaPrice.Text = ""
            txtnoofvisas.Text = ""
            txtVisaValue.Text = ""
        Else
            ''" & objBLLHotelSearch.SourceCountryCode & "'

            Dim visapriceplistcode As String = GetVisaPrice(ddlVisaType.SelectedValue, txtNationalityCode.Text, "Adult")

            Dim visaChildpriceplistcode As String = GetVisaPrice(ddlVisaType.SelectedValue, txtNationalityCode.Text, "Child")

            Dim pricevisaVal As Decimal
            Dim ChildpricevisaVal As Decimal
            Dim pricecurrcode As String = ""
            Dim agentdt As New DataTable
            Dim decimalplaces As Integer = Convert.ToInt32(objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1142")) 'Modified by param 0n 08/10/2018
            Dim dt As DataTable
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(CType(Session("sRequestId"), String))

            If dt.Rows.Count > 0 Then
                txtVisaCustomerCode.Text = dt.Rows(0).Item("agentcode")
                txtVisaCustomer.Text = dt.Rows(0).Item("agentname")
            End If

            If visapriceplistcode <> "" Then
                hdnplistcode.Value = visapriceplistcode.Split("|")(0)
                If Not Request.QueryString("FreeForm") Is Nothing Then ' Modified by abin on 20180728
                    If Request.QueryString("FreeForm") = "1" Then
                        txtVisaPrice.Text = "0"
                        txtwlVisaPrice.Text = "0"
                        txtVisaChildPrice.Text = "0"
                    End If
                Else
                    pricecurrcode = Convert.ToString(visapriceplistcode.Split("|")(2))  'Modified by param 0n 07/10/2018
                    pricevisaVal = Convert.ToDecimal(visapriceplistcode.Split("|")(1))
                    ChildpricevisaVal = Convert.ToDecimal(visaChildpriceplistcode.Split("|")(1))

                    Dim strQry = "select a.agentcode,a.currcode agentcurrcode,c.convrate from agentmast a(nolock) inner join currrates c(nolock) " &
                    "on c.currcode='" & pricecurrcode & "' and c.tocurr= a.currcode where a.agentcode='" & txtVisaCustomerCode.Text & "'"
                    agentdt = objclsUtilities.GetDataFromDataTable(strQry)
                    If agentdt.Rows.Count > 0 Then
                        If CType(agentdt.Rows(0).Item("agentcurrcode"), String) <> pricecurrcode Then
                            Dim visaprice As Decimal
                            visaprice = Math.Round(pricevisaVal * Convert.ToDecimal(agentdt.Rows(0).Item("convrate")), decimalplaces)
                            txtVisaPrice.Text = visaprice
                            txtwlVisaPrice.Text = visaprice
                            txtVisaChildPrice.Text = Math.Round(ChildpricevisaVal * Convert.ToDecimal(agentdt.Rows(0).Item("convrate")), decimalplaces)
                        Else
                            txtVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
                            txtwlVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
                            txtVisaChildPrice.Text = Math.Round(Convert.ToDecimal(visaChildpriceplistcode.Split("|")(1)), 2)
                        End If
                    Else
                        txtVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
                        txtwlVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
                        txtVisaChildPrice.Text = Math.Round(Convert.ToDecimal(visaChildpriceplistcode.Split("|")(1)), 2)
                    End If
                End If



                If txtnoofvisas.Text <> "" And txtVisaPrice.Text <> "" Then

                    'txtVisaValue.Text = Math.Round((Convert.ToDecimal(txtnoofvisas.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)
                    'txtwlVisaValue.Text = Math.Round((Convert.ToDecimal(txtnoofvisas.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)

                    'txtVisaValue.Text = (Math.Round((Convert.ToDecimal(txtnoofvisas.Text) * Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)) + (Convert.ToDecimal(Val(ddlchild.SelectedValue))) * Math.Round(Convert.ToDecimal(txtVisaChildPrice.Text), 2), 2))
                    '' Math.Round((Convert.ToDecimal(txtnoofvisas.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)
                    'txtwlVisaValue.Text = Math.Round((Convert.ToDecimal(txtnoofvisas.Text) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2) + Convert.ToDecimal(Val(ddlchild.SelectedValue))) * Math.Round(Convert.ToDecimal(txtVisaChildPrice.Text), 2), 2)
                    Dim chPrice As Decimal = 0
                    If txtVisaChildPrice.Text <> "" Then
                        chPrice = txtVisaChildPrice.Text
                    End If
                    txtVisaValue.Text = Convert.ToDecimal(txtnoofvisas.Text) * Convert.ToDecimal(txtVisaPrice.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue)) * Convert.ToDecimal(chPrice)
                    txtwlVisaValue.Text = Convert.ToDecimal(txtnoofvisas.Text) * Convert.ToDecimal(txtwlVisaPrice.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue)) * Convert.ToDecimal(chPrice)

                Else

                    txtVisaValue.Text = "" ' Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)
                    txtwlVisaValue.Text = "" ' Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
                End If
            Else
                txtVisaPrice.Text = ""
                hdnplistcode.Value = ""
                txtVisaValue.Text = ""
                txtwlVisaValue.Text = ""
                txtwlVisaPrice.Text = ""


            End If

            Dim strQuery As String = ""
            Dim strVisaPrice As String
            Dim visapricedt As New DataTable
            ''' Cost Price for Cummulative
            ''' 
            If hdBookingEngineRateType.Value = "1" Then

                strQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisaType.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec"



                visapricedt = objclsUtilities.GetDataFromDataTable(strQuery)


                If visapricedt.Rows.Count > 0 Then
                    hdncplistcode.Value = CType(visapricedt.Rows(0).Item("ocplistcode"), String)
                    hdncostprice.Value = CType(visapricedt.Rows(0).Item("ocostprice"), String)
                    hdnpartycode.Value = CType(visapricedt.Rows(0).Item("partycode"), String)

                End If
            End If

            ''whitelabel values

            Dim dWlMarkup As Decimal
            If hdWhiteLabel.Value = "1" Or Session("sLoginType") = "RO" Then
                Dim bookingheaderdt As New DataTable
                strQuery = "execute sp_booking_whitelabelagent '" & txtVisaCustomerCode.Text & "'"
                bookingheaderdt = objclsUtilities.GetDataFromDataTable(strQuery)
                If bookingheaderdt.Rows.Count > 0 Then
                    hdnwlcurrcode.Value = CType(bookingheaderdt.Rows(0).Item("wlcurrcode"), String)
                    hdnwlconvrate.Value = CType(bookingheaderdt.Rows(0).Item("wlconvrate"), String)
                    hdnwlmarkup.Value = CType(bookingheaderdt.Rows(0).Item("wlmarkupperc"), String)

                    dWlMarkup = ((100 + Convert.ToDecimal(hdnwlmarkup.Value)) / 100) '* Convert.ToDecimal(hdnwlconvrate.Value)
                    If pricecurrcode <> hdnwlcurrcode.Value Then 'Modified by param 0n 07/10/2018
                        Dim strQry = "select a.agentcode,a.currcode agentcurrcode,c.convrate from agentmast a(nolock) inner join currrates c(nolock) " &
                    "on c.currcode='" & pricecurrcode & "' and c.tocurr= a.currcode where a.agentcode='" & txtVisaCustomerCode.Text & "'"
                        agentdt = objclsUtilities.GetDataFromDataTable(strQry)
                        If agentdt.Rows.Count > 0 Then
                            Dim visaprice As Decimal
                            visaprice = pricevisaVal * dWlMarkup
                            visaprice = Math.Round(visaprice * Convert.ToDecimal(agentdt.Rows(0).Item("convrate")), decimalplaces)
                            txtwlVisaPrice.Text = visaprice
                        Else
                            txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                        End If
                    Else
                        txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                    End If
                    If txtnoofvisas.Text <> "" Then
                        txtwlVisaValue.Text = Math.Round((Convert.ToDecimal(txtnoofvisas.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
                    End If

                End If
            End If


            If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "block")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "block")


            ElseIf hdWhiteLabel.Value = "1" Then
                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "block")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "block")

                'txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                'txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtnoofvisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
            Else
                If hdBookingEngineRateType.Value = "1" Then
                    dvVisaPrice.Style.Add("display", "none")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvVisaValue.Style.Add("display", "none")
                    dvwlVisaValue.Style.Add("display", "none")
                Else
                    dvVisaPrice.Style.Add("display", "block")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvVisaValue.Style.Add("display", "block")
                    dvwlVisaValue.Style.Add("display", "none")
                End If

            End If

            ' If txtNationalityCode.Text = "IN" Then

            Dim visaremarks As String = objclsUtilities.ExecuteQueryReturnStringValue("select visaremarks from ctrymast where isnull(visaremarks,'')<>'' and ctrycode='" & txtNationalityCode.Text & "'")
            If visaremarks <> "" Then
                MessageBox.ShowMessage(Page, MessageType.Info, "Choosen Nationality...</Br></br>" + txtNationality.Text + " -- " + visaremarks)
            End If
            'End If


        End If
        txtnoofvisas.Focus()


    End Sub
    Protected Sub ddlVisatype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlVisaType As DropDownList = CType(sender, DropDownList)
        Dim str As String = ddlVisaType.SelectedValue
        Dim dlItem As DataListItem = CType(ddlVisaType.NamingContainer, DataListItem)
        Dim txtVisaPrice As TextBox = CType(dlItem.FindControl("txtVisaPrice"), TextBox)
        Dim hdnplistcode As HiddenField = CType(dlItem.FindControl("hdnplistcode"), HiddenField)
        Dim txtNationalityCode As TextBox = CType(dlItem.FindControl("txtNationalityCode"), TextBox)
        Dim txtVisaValue As TextBox = CType(dlItem.FindControl("txtVisaValue"), TextBox)
        Dim txtnoofvisas As TextBox = CType(dlItem.FindControl("txtnoofvisas"), TextBox)
        Dim lblrowno As Label = CType(dlItem.FindControl("lblrowno"), Label)
        Dim txtNationality As TextBox = CType(dlItem.FindControl("txtNationality"), TextBox)

        Dim txtVisaDate As TextBox = CType(dlItem.FindControl("txtVisaDate"), TextBox)
        Dim hdncplistcode As HiddenField = CType(dlItem.FindControl("hdncplistcode"), HiddenField)
        Dim hdncostprice As HiddenField = CType(dlItem.FindControl("hdncostprice"), HiddenField)
        Dim hdnpartycode As HiddenField = CType(dlItem.FindControl("hdnpartycode"), HiddenField)

        Dim hdnwlcurrcode As HiddenField = CType(dlItem.FindControl("hdnwlcurrcode"), HiddenField)
        Dim hdnwlmarkup As HiddenField = CType(dlItem.FindControl("hdnwlmarkup"), HiddenField)
        Dim hdnwlconvrate As HiddenField = CType(dlItem.FindControl("hdnwlconvrate"), HiddenField)
        Dim hdnChildcostprice As HiddenField = CType(dlItem.FindControl("hdnChildcostprice"), HiddenField)
        Dim dvVisaPrice As HtmlGenericControl = CType(dlItem.FindControl("dvVisaPrice"), HtmlGenericControl)
        Dim dvwlVisaPrice As HtmlGenericControl = CType(dlItem.FindControl("dvwlVisaPrice"), HtmlGenericControl)
        Dim dvVisaValue As HtmlGenericControl = CType(dlItem.FindControl("dvVisaValue"), HtmlGenericControl)
        Dim dvwlVisaValue As HtmlGenericControl = CType(dlItem.FindControl("dvwlVisaValue"), HtmlGenericControl)
        Dim txtwlVisaValue As TextBox = CType(dlItem.FindControl("txtwlVisaValue"), TextBox)
        Dim txtwlVisaPrice As TextBox = CType(dlItem.FindControl("txtwlVisaPrice"), TextBox)
        Dim txtVisaChildPrice As TextBox = CType(dlItem.FindControl("txtVisaChildPrice"), TextBox)
        Dim ddlchild As DropDownList = CType(dlItem.FindControl("ddlchild"), DropDownList)

        Dim objBLLHotelSearch = New BLLHotelSearch

        If ddlVisaType.Text = "--" Then
            txtVisaPrice.Text = ""
            txtnoofvisas.Text = ""
            txtVisaValue.Text = ""
        Else
            ''" & objBLLHotelSearch.SourceCountryCode & "'

            Dim visapriceplistcode As String = GetVisaPrice(ddlVisaType.SelectedValue, txtNationalityCode.Text, "Adult")

            Dim visaChildpriceplistcode As String = GetVisaPrice(ddlVisaType.SelectedValue, txtNationalityCode.Text, "Child")

            Dim pricevisaVal As Decimal
            Dim priceChildvisaVal As Decimal
            Dim pricecurrcode As String = ""
            Dim agentdt As New DataTable
            Dim decimalplaces As Integer = Convert.ToInt32(objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1142")) 'Modified by param 0n 08/10/2018
            Dim dt As DataTable
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(CType(Session("sRequestId"), String))

            If dt.Rows.Count > 0 Then
                txtVisaCustomerCode.Text = dt.Rows(0).Item("agentcode")
                txtVisaCustomer.Text = dt.Rows(0).Item("agentname")
            End If

            If visapriceplistcode <> "" Then
                hdnplistcode.Value = visapriceplistcode.Split("|")(0)
                If Not Request.QueryString("FreeForm") Is Nothing Then ' Modified by abin on 20180728
                    If Request.QueryString("FreeForm") = "1" Then
                        txtVisaPrice.Text = "0"
                        txtwlVisaPrice.Text = "0"
                        ' txtch.Text = "0"
                    End If
                Else
                    pricecurrcode = Convert.ToString(visapriceplistcode.Split("|")(2))  'Modified by param 0n 07/10/2018
                    pricevisaVal = Convert.ToDecimal(visapriceplistcode.Split("|")(1))
                    priceChildvisaVal = visaChildpriceplistcode.Split("|")(1)
                    txtVisaChildPrice.Text = visaChildpriceplistcode.Split("|")(1)


                    Dim strQry = "select a.agentcode,a.currcode agentcurrcode,c.convrate from agentmast a(nolock) inner join currrates c(nolock) " &
                    "on c.currcode='" & pricecurrcode & "' and c.tocurr= a.currcode where a.agentcode='" & txtVisaCustomerCode.Text & "'"
                    agentdt = objclsUtilities.GetDataFromDataTable(strQry)
                    If agentdt.Rows.Count > 0 Then
                        If CType(agentdt.Rows(0).Item("agentcurrcode"), String) <> pricecurrcode Then
                            Dim visaprice As Decimal
                            visaprice = Math.Round(pricevisaVal * Convert.ToDecimal(agentdt.Rows(0).Item("convrate")), decimalplaces)
                            txtVisaPrice.Text = visaprice
                            txtwlVisaPrice.Text = visaprice

                            txtVisaChildPrice.Text = Math.Round(priceChildvisaVal * Convert.ToDecimal(agentdt.Rows(0).Item("convrate")), decimalplaces)

                        Else
                            txtVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
                            txtwlVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)

                            txtVisaChildPrice.Text = Math.Round(Convert.ToDecimal(visaChildpriceplistcode.Split("|")(1)), 2)

                        End If
                    Else
                        txtVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
                        txtwlVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)

                        txtVisaChildPrice.Text = Math.Round(Convert.ToDecimal(visaChildpriceplistcode.Split("|")(1)), 2)
                    End If
                End If



                If txtnoofvisas.Text <> "" And txtVisaPrice.Text <> "" Then

                    Dim chPrice As Decimal = 0
                    If txtVisaChildPrice.Text <> "" Then
                        chPrice = txtVisaChildPrice.Text
                    End If
                    txtVisaValue.Text = Convert.ToDecimal(txtnoofvisas.Text) * Convert.ToDecimal(txtVisaPrice.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue)) * Convert.ToDecimal(chPrice)
                    txtwlVisaValue.Text = Convert.ToDecimal(txtnoofvisas.Text) * Convert.ToDecimal(txtwlVisaPrice.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue)) * Convert.ToDecimal(chPrice)


                    'txtVisaValue.Text = Math.Round((Convert.ToDecimal(txtnoofvisas.Text) * Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)
                    'txtwlVisaValue.Text = Math.Round((Convert.ToDecimal(txtnoofvisas.Text) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)

                Else

                    txtVisaValue.Text = "" ' Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)
                    txtwlVisaValue.Text = "" ' Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)

                End If
            Else
                txtVisaPrice.Text = ""
                hdnplistcode.Value = ""
                txtVisaValue.Text = ""
                txtwlVisaValue.Text = ""
                txtwlVisaPrice.Text = ""


            End If

            Dim strQuery As String = ""
            Dim strVisaPrice As String
            Dim visapricedt As New DataTable
            Dim strChildQuery As String = ""

            ''' Cost Price for Cummulative
            ''' 
            If hdBookingEngineRateType.Value = "1" Then

                strQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisaType.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec"



                visapricedt = objclsUtilities.GetDataFromDataTable(strQuery)


                If visapricedt.Rows.Count > 0 Then
                    hdncplistcode.Value = CType(visapricedt.Rows(0).Item("ocplistcode"), String)
                    hdncostprice.Value = CType(visapricedt.Rows(0).Item("ocostprice"), String)
                    hdnpartycode.Value = CType(visapricedt.Rows(0).Item("partycode"), String)

                End If


                strChildQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisaType.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec  and othcatcode='VISACHG-CH'"
                Dim visaChildpricedt As New DataTable
                visaChildpricedt = objclsUtilities.GetDataFromDataTable(strChildQuery)
                If visaChildpricedt.Rows.Count > 0 Then
                    hdnChildcostprice.Value = CType(visaChildpricedt.Rows(0).Item("ocostprice"), String)
                End If

            End If

            ''whitelabel values

            Dim dWlMarkup As Decimal
            If hdWhiteLabel.Value = "1" Or Session("sLoginType") = "RO" Then
                Dim bookingheaderdt As New DataTable
                strQuery = "execute sp_booking_whitelabelagent '" & txtVisaCustomerCode.Text & "'"
                bookingheaderdt = objclsUtilities.GetDataFromDataTable(strQuery)
                If bookingheaderdt.Rows.Count > 0 Then
                    hdnwlcurrcode.Value = CType(bookingheaderdt.Rows(0).Item("wlcurrcode"), String)
                    hdnwlconvrate.Value = CType(bookingheaderdt.Rows(0).Item("wlconvrate"), String)
                    hdnwlmarkup.Value = CType(bookingheaderdt.Rows(0).Item("wlmarkupperc"), String)

                    dWlMarkup = ((100 + Convert.ToDecimal(hdnwlmarkup.Value)) / 100) '* Convert.ToDecimal(hdnwlconvrate.Value)
                    If pricecurrcode <> hdnwlcurrcode.Value Then 'Modified by param 0n 07/10/2018
                        Dim strQry = "select a.agentcode,a.currcode agentcurrcode,c.convrate from agentmast a(nolock) inner join currrates c(nolock) " &
                    "on c.currcode='" & pricecurrcode & "' and c.tocurr= a.currcode where a.agentcode='" & txtVisaCustomerCode.Text & "'"
                        agentdt = objclsUtilities.GetDataFromDataTable(strQry)
                        If agentdt.Rows.Count > 0 Then
                            Dim visaprice As Decimal
                            visaprice = pricevisaVal * dWlMarkup
                            visaprice = Math.Round(visaprice * Convert.ToDecimal(agentdt.Rows(0).Item("convrate")), decimalplaces)
                            txtwlVisaPrice.Text = visaprice
                        Else
                            txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                        End If
                    Else
                        txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                    End If
                    txtwlVisaValue.Text = Math.Round((Convert.ToDecimal(Val(txtnoofvisas.Text)) + Convert.ToDecimal(Val(ddlchild.SelectedValue))), 2) * Math.Round(Convert.ToDecimal(Val(txtwlVisaPrice.Text)), 2)
                End If
            End If


            If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "block")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "block")


            ElseIf hdWhiteLabel.Value = "1" Then
                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "block")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "block")

                'txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                'txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtnoofvisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
            Else
                If hdBookingEngineRateType.Value = "1" Then
                    dvVisaPrice.Style.Add("display", "none")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvVisaValue.Style.Add("display", "none")
                    dvwlVisaValue.Style.Add("display", "none")
                Else
                    dvVisaPrice.Style.Add("display", "block")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvVisaValue.Style.Add("display", "block")
                    dvwlVisaValue.Style.Add("display", "none")
                End If

            End If

            ' If txtNationalityCode.Text = "IN" Then

            Dim visaremarks As String = objclsUtilities.ExecuteQueryReturnStringValue("select visaremarks from ctrymast where isnull(visaremarks,'')<>'' and ctrycode='" & txtNationalityCode.Text & "'")
            If visaremarks <> "" Then
                MessageBox.ShowMessage(Page, MessageType.Info, "Choosen Nationality...</Br></br>" + txtNationality.Text + " -- " + visaremarks)
            End If
            'End If


        End If
        txtnoofvisas.Focus()
        '  dlVisaInfo.Items(dlItem.ItemIndex).Focus()

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
    Public Shared Function GetNationalityDetails(ByVal CustCode As String) As String

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
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'Company 1
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





    Sub RemoveRow_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim myButton As LinkButton = CType(sender, LinkButton)

        Dim dlItem As DataListItem = CType(myButton.NamingContainer, DataListItem)
        Dim lblrowno As Label = CType(dlItem.FindControl("lblrowno"), Label)

        If Not Session("VisaDetailsDt") Is Nothing Then
            Dim dt As New DataTable
            dt = CType(Session("VisaDetailsDt"), DataTable)
            Dim dv As New DataView(dt)
            dv.RowFilter = "Vlineno='" + lblrowno.Text + "'"
            'dv.ToTable()

            For i = dt.Rows.Count - 1 To 0 Step -1
                If dt.Rows(i)("VLineNo") = lblrowno.Text Then
                    dt.Rows.RemoveAt(i)
                End If
            Next
            Session("VisaDetailsDt") = dt
            dlVisaInfo.DataSource = dt
            dlVisaInfo.DataBind()
            bindvisadetails()
        End If
        If dlVisaInfo.Items.Count = 0 Then

        End If


    End Sub

    Private Function DataListItemToDataRow(ByVal gvr As DataListItem) As DataRow
        Dim di As Object = Nothing
        Dim drv As DataRowView = Nothing
        Dim dr As DataRow = Nothing

        If gvr IsNot Nothing Then
            di = TryCast(gvr.DataItem, System.Object)
            If di IsNot Nothing Then
                drv = TryCast(di, System.Data.DataRowView)
                If drv IsNot Nothing Then
                    dr = TryCast(drv.Row, System.Data.DataRow)
                End If
            End If
        End If

        Return dr
    End Function
    Protected Sub dlVisaInfo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlVisaInfo.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim ddlVisatype As DropDownList = CType(e.Item.FindControl("ddlVisatype"), DropDownList)
            Dim ddlVisa As DropDownList = CType(e.Item.FindControl("ddlVisa"), DropDownList)
            Dim txtVisaPrice As TextBox = CType(e.Item.FindControl("txtVisaPrice"), TextBox)
            Dim txtVisaChildPrice As TextBox = CType(e.Item.FindControl("txtVisaChildPrice"), TextBox)
            Dim txtNoOfVisas As TextBox = CType(e.Item.FindControl("txtNoOfVisas"), TextBox)
            Dim txtVisaValue As TextBox = CType(e.Item.FindControl("txtVisaValue"), TextBox)
            Dim txtVisaDate As TextBox = CType(e.Item.FindControl("txtVisaDate"), TextBox)
            Dim lblRowNo As Label = CType(e.Item.FindControl("lblrowno"), Label)
            Dim txtNationality As TextBox = CType(e.Item.FindControl("txtNationality"), TextBox)
            Dim txtNationalityCode As TextBox = CType(e.Item.FindControl("txtNationalityCode"), TextBox)

            '*** Danny 22/10/2018 FreeForm SupplierAgent
            Dim Txt_SupplierCode As TextBox = CType(e.Item.FindControl("Txt_SupplierCode"), TextBox)
            Dim Txt_SupplierName As TextBox = CType(e.Item.FindControl("Txt_SupplierName"), TextBox)
            Dim Txt_SupplierAgentCode As TextBox = CType(e.Item.FindControl("Txt_SupplierAgentCode"), TextBox)
            Dim Txt_SupplierAgentName As TextBox = CType(e.Item.FindControl("Txt_SupplierAgentName"), TextBox)
            If Txt_SupplierAgentCode.Text.Trim.Length = 0 Then
                Txt_SupplierAgentCode.Text = Session("strSupplierAgentCode").ToString()
                Txt_SupplierAgentName.Text = Session("strSupplierAgentName").ToString
            End If
            Dim Txt_VisaCost As TextBox = CType(e.Item.FindControl("Txt_VisaCost"), TextBox)
            Dim Txt_VisaCostValue As TextBox = CType(e.Item.FindControl("Txt_VisaCostValue"), TextBox)
            Dim dv_VatCost As HtmlGenericControl = CType(e.Item.FindControl("div_VatCost"), HtmlGenericControl)

            Dim lblCostPrice As Label = CType(e.Item.FindControl("lblCostPrice"), Label)
            Dim lblCostTotal As Label = CType(e.Item.FindControl("lblCostTotal"), Label)
            If Not Session("strDefaultCurrency") Is Nothing Then
                If Session("strDefaultCurrency").ToString.Trim.Length > 0 Then
                    lblCostPrice.Text = lblCostPrice.Text.Replace("---", Session("strDefaultCurrency").ToString.Trim)
                    lblCostTotal.Text = lblCostTotal.Text.Replace("---", Session("strDefaultCurrency").ToString.Trim)
                End If
            End If

            Dim dvcompheader As HtmlGenericControl = CType(e.Item.FindControl("dvcompheader"), HtmlGenericControl)
            Dim dvComplementCust As HtmlGenericControl = CType(e.Item.FindControl("dvComplementCust"), HtmlGenericControl)
            Dim dvVisaValue As HtmlGenericControl = CType(e.Item.FindControl("dvVisaValue"), HtmlGenericControl)
            Dim dvVisaPrice As HtmlGenericControl = CType(e.Item.FindControl("dvVisaPrice"), HtmlGenericControl)
            Dim chkComplementCust As CheckBox = CType(e.Item.FindControl("chkComplementCust"), CheckBox)
            Dim hdnplistcode As HiddenField = CType(e.Item.FindControl("hdnplistcode"), HiddenField)

            Dim hdncplistcode As HiddenField = CType(e.Item.FindControl("hdncplistcode"), HiddenField)
            Dim hdncostprice As HiddenField = CType(e.Item.FindControl("hdncostprice"), HiddenField)
            Dim hdncostChildprice As HiddenField = CType(e.Item.FindControl("hdnChildcostprice"), HiddenField)
            Dim hdnpartycode As HiddenField = CType(e.Item.FindControl("hdnpartycode"), HiddenField)
            Dim hdnwlcurrcode As HiddenField = CType(e.Item.FindControl("hdnwlcurrcode"), HiddenField)
            Dim hdnwlmarkup As HiddenField = CType(e.Item.FindControl("hdnwlmarkup"), HiddenField)
            Dim hdnwlconvrate As HiddenField = CType(e.Item.FindControl("hdnwlconvrate"), HiddenField)
            Dim hdvisacostvalue As HiddenField = CType(e.Item.FindControl("hdvisacostvalue"), HiddenField)
            Dim hdnChildcostprice As HiddenField = CType(e.Item.FindControl("hdnChildcostprice"), HiddenField)
            Dim hdnChildprice As HiddenField = CType(e.Item.FindControl("hdnChildprice"), HiddenField)
            Dim dvwlVisaPrice As HtmlGenericControl = CType(e.Item.FindControl("dvwlVisaPrice"), HtmlGenericControl)
            Dim dvwlVisaValue As HtmlGenericControl = CType(e.Item.FindControl("dvwlVisaValue"), HtmlGenericControl)
            Dim txtwlVisaValue As TextBox = CType(e.Item.FindControl("txtwlVisaValue"), TextBox)
            Dim txtwlVisaPrice As TextBox = CType(e.Item.FindControl("txtwlVisaPrice"), TextBox)

            Dim RemoveRow As LinkButton = CType(e.Item.FindControl("RemoveRow"), LinkButton)

            ''' Added 05/04/18 shahul
            Dim ddlchild As DropDownList = CType(e.Item.FindControl("ddlchild"), DropDownList)
            Dim txtchildage1 As TextBox = CType(e.Item.FindControl("txtchildage1"), TextBox)
            Dim txtchildage2 As TextBox = CType(e.Item.FindControl("txtchildage2"), TextBox)
            Dim txtchildage3 As TextBox = CType(e.Item.FindControl("txtchildage3"), TextBox)
            Dim txtchildage4 As TextBox = CType(e.Item.FindControl("txtchildage4"), TextBox)
            Dim txtchildage5 As TextBox = CType(e.Item.FindControl("txtchildage5"), TextBox)
            Dim txtchildage6 As TextBox = CType(e.Item.FindControl("txtchildage6"), TextBox)
            Dim dvChild1 As HtmlGenericControl = CType(e.Item.FindControl("dvChild1"), HtmlGenericControl)
            Dim dvChild2 As HtmlGenericControl = CType(e.Item.FindControl("dvChild2"), HtmlGenericControl)
            Dim dvChild3 As HtmlGenericControl = CType(e.Item.FindControl("dvChild3"), HtmlGenericControl)
            Dim dvChild4 As HtmlGenericControl = CType(e.Item.FindControl("dvChild4"), HtmlGenericControl)
            Dim dvChild5 As HtmlGenericControl = CType(e.Item.FindControl("dvChild5"), HtmlGenericControl)
            Dim dvChild6 As HtmlGenericControl = CType(e.Item.FindControl("dvChild6"), HtmlGenericControl)

            Dim chdage As HtmlGenericControl = CType(e.Item.FindControl("chdage"), HtmlGenericControl)


            Dim dtRow As DataRow = DataListItemToDataRow(e.Item)



            If chkOveridePrice.Checked = False Then
                txtVisaPrice.Attributes.Add("readonly", "readonly")
                txtwlVisaPrice.Attributes.Add("readonly", "readonly")
                txtVisaChildPrice.Attributes.Add("readonly", "readonly")

            End If
            txtVisaValue.Style("text-align") = "right"
            txtVisaPrice.Style("text-align") = "right"
            txtwlVisaValue.Style("text-align") = "right"
            txtwlVisaPrice.Style("text-align") = "right"
            lblRowNo.Text = e.Item.ItemIndex + 1
            txtVisaValue.Attributes.Add("readonly", "readonly")
            txtwlVisaValue.Attributes.Add("readonly", "readonly")

            '*** Danny 22/10/2018 FreeForm SupplierAgent
            Txt_VisaCost.Style("text-align") = "right"
            Txt_VisaCostValue.Style("text-align") = "right"
            Txt_VisaCostValue.Attributes.Add("readonly", "readonly")
            If ViewState("vFreeForm") = "FreeForm" Then
                dv_VatCost.Style.Add("display", "none")
                ' dv_VatCost.Visible = True
            Else
                dv_VatCost.Style.Add("display", "none")
                ' dv_VatCost.Visible = False
            End If

            If Session("sLoginType") <> "RO" Then



                If hdBookingEngineRateType.Value = "1" Then

                    dvVisaValue.Style.Add("display", "none")
                    dvVisaPrice.Style.Add("display", "none")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvwlVisaValue.Style.Add("display", "none")
                    chkOveridePrice.Visible = False
                    lbloverrideprice.Visible = False
                Else
                    If hdWhiteLabel.Value = "1" Then
                        chkOveridePrice.Visible = False
                        lbloverrideprice.Visible = False
                    End If
                End If
                dvComplementCust.Style.Add("display", "none")
            Else

            End If



            Dim strQuery As String = ""
            If Not Session("sRequestId") Is Nothing Then
                Dim bookingheaderdt As New DataTable

                Dim childages As String
                Dim strChildAges As String()
                Dim dtpax As New DataTable
                bookingheaderdt = objBLLCommonFuntions.GetBookingTempHeaderDetails(CType(Session("sRequestId"), String))
                If Not bookingheaderdt Is Nothing Then
                    '  txtVisaCustomerCode.Text = bookingheaderdt.Rows(0).Item("agentcode")
                    txtVisaDate.Text = objclsUtilities.ExecuteQueryReturnStringValue("select convert(varchar(10),min(datein),103) as datein from view_booking_allservicestemp where requestid ='" & bookingheaderdt.Rows(0).Item("requestid") & "'")

                    BindVisaType(bookingheaderdt.Rows(0).Item("MinDate_"), ddlVisatype)
                    txtNationalityCode.Text = bookingheaderdt.Rows(0).Item("sourcectrycode")
                    txtNationality.Text = bookingheaderdt.Rows(0).Item("sourcectryname")

                    strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sRequestId") & "')"
                    dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                    If dtpax.Rows.Count > 0 Then
                        txtNoOfVisas.Text = IIf(Val(txtNoOfVisas.Text) = 0, (Val(dtpax.Rows(0).Item("adults"))), txtNoOfVisas.Text)

                        ddlchild.SelectedValue = dtpax.Rows(0).Item("child").ToString

                        childages = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                        If Left(childages, 1) = ";" Then
                            childages = Right(childages, (childages.Length - 1))
                        End If
                        strChildAges = childages.ToString.Split(";")
                        '''''''
                        If strChildAges.Length <> 0 Then
                            txtchildage1.Text = strChildAges(0)
                        End If

                        If strChildAges.Length > 1 Then
                            txtchildage2.Text = strChildAges(1)
                        End If
                        If strChildAges.Length > 2 Then
                            txtchildage3.Text = strChildAges(2)
                        End If
                        If strChildAges.Length > 3 Then
                            txtchildage4.Text = strChildAges(3)
                        End If
                        If strChildAges.Length > 4 Then
                            txtchildage5.Text = strChildAges(4)
                        End If
                        If strChildAges.Length > 5 Then
                            txtchildage6.Text = strChildAges(5)
                        End If

                    End If



                    ''' Added 05/04/18 shahul
                    ''' 

                    Select Case Val(dtpax.Rows(0).Item("child").ToString)
                        Case 0
                            dvChild1.Style.Add("display", "none")
                            dvChild2.Style.Add("display", "none")
                            dvChild3.Style.Add("display", "none")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "none")
                        Case 1
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "none")
                            dvChild3.Style.Add("display", "none")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 2
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "none")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 3
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 4
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "block")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 5
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "block")
                            dvChild5.Style.Add("display", "block")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 6
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "block")
                            dvChild5.Style.Add("display", "block")
                            dvChild6.Style.Add("display", "block")
                            chdage.Style.Add("display", "block")
                    End Select


                End If
            End If



            '  Dim strQuery = "select count(*)cnt from view_visa_types where convert(datetime,'" & objBLLHotelSearch.CheckIn & "',105) between fromdate and todate and (CHARINDEX('" & objBLLHotelSearch.SourceCountryCode & "',countries)>0 or CHARINDEX('" & objBLLHotelSearch.AgentCode & "',agents)>0)"
            strQuery = " select CtryCode from VisaOnArrivalCountries where  CHARINDEX('" & txtSourceCountryCode.Text & "',CtryCode)>0  "
            Dim dt As DataTable
            dt = objBLLHotelSearch.GetResultAsDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                ddlVisa.SelectedValue = "Not Required"
                ddlVisatype.Attributes.Add("disabled", False)
            Else
                'If ddlVisatype.SelectedValue <> "" Then
                '    ddlVisa.SelectedValue = "Not Required"
                '    ddlVisatype.Attributes.Add("disabled", False)

                'Else
                ddlVisa.SelectedValue = "Required"
                ddlVisatype.Attributes.Remove("disabled")
                ddlVisatype.SelectedValue = "Tourist Single Entry Visa - 30 days"

                ' End If

            End If

            Dim dWlMarkup As Decimal
            If Not Session("sEditRequestId") Is Nothing Then
                If Val(hdnlineno.Value) <> 0 Then

                    txtVisaPrice.Text = IIf(Convert.ToString(dtRow("visaprice")) = "", "", dtRow("visaprice"))


                    txtVisaValue.Text = IIf(Convert.ToString(dtRow("visavalue")) = "", "", dtRow("visavalue"))
                    txtVisaDate.Text = IIf(Convert.ToString(dtRow("visadate")) = "", "", dtRow("visadate"))

                    '*** Danny 22/10/2018 FreeForm SupplierAgent
                    Txt_VisaCost.Text = IIf(Convert.ToString(dtRow("Visacprice")) = "", "", dtRow("Visacprice"))
                    Txt_VisaCostValue.Text = IIf(Convert.ToString(dtRow("visacostvalue")) = "", "", dtRow("visacostvalue"))

                    ' txtNoOfVisas.Text = IIf(Convert.ToString(dtRow("noofvisas")) = "", "", dtRow("noofvisas"))
                    txtNationalityCode.Text = IIf(Convert.ToString(dtRow("nationalitycode")) = "", "", dtRow("nationalitycode"))
                    txtNationality.Text = IIf(Convert.ToString(dtRow("nationalityname")) = "", "", dtRow("nationalityname"))
                    ddlVisatype.SelectedValue = IIf(Convert.ToString(dtRow("visatypecode")) = "", "", dtRow("visatypecode"))
                    ddlVisa.SelectedValue = IIf(Convert.ToString(dtRow("visaoptions")) = "", "", dtRow("visaoptions"))
                    chkComplementCust.Checked = IIf(Convert.ToString(dtRow("complimentarycust")) = "1", True, False)
                    hdnplistcode.Value = IIf(Convert.ToString(dtRow("oplistcode")) = "", "", dtRow("oplistcode"))
                    hdncplistcode.Value = IIf(Convert.ToString(dtRow("OCPlistCode")) = "", "", dtRow("OCPlistCode"))
                    hdncostprice.Value = IIf(Convert.ToString(dtRow("Visacprice")) = "", "", dtRow("Visacprice"))
                    hdncostChildprice.Value = IIf(Convert.ToString(dtRow("Visacchildprice")) = "", "", dtRow("Visacchildprice"))
                    hdnpartycode.Value = IIf(Convert.ToString(dtRow("preferredsupplier")) = "", "", dtRow("preferredsupplier"))
                    hdnwlcurrcode.Value = IIf(Convert.ToString(dtRow("wlcurrcode")) = "", "", dtRow("wlcurrcode"))
                    hdnwlmarkup.Value = IIf(Convert.ToString(dtRow("wlmarkupperc")) = "", "", dtRow("wlmarkupperc"))
                    hdnwlconvrate.Value = IIf(Convert.ToString(dtRow("wlconvrate")) = "", "", dtRow("wlconvrate"))
                    hdnwlconvrate.Value = IIf(Convert.ToString(dtRow("wlconvrate")) = "", "", dtRow("wlconvrate"))
                    txtwlVisaPrice.Text = IIf(Convert.ToString(dtRow("wlvisaprice")) = "", "", dtRow("wlvisaprice"))
                    txtwlVisaValue.Text = IIf(Convert.ToString(dtRow("wlvisavalue")) = "", "", dtRow("wlvisavalue"))
                    hdvisacostvalue.Value = IIf(Convert.ToString(dtRow("visacostvalue")) = "", "", dtRow("visacostvalue"))

                    '' Added shahul 05/04/18
                    txtNoOfVisas.Text = IIf(Convert.ToString(dtRow("adults")) = "", "", dtRow("adults"))
                    ddlchild.SelectedValue = IIf(Convert.ToString(dtRow("child")) = 0, "--", dtRow("child"))

                    Dim childages1 As String = dtRow("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages1, 1) = ";" Then
                        childages1 = Right(childages1, (childages1.Length - 1))
                    End If
                    Dim strChildAges1 As String() = childages1.ToString.Split(";")
                    '''''''
                    If strChildAges1.Length <> 0 And strChildAges1(0) <> "" Then
                        txtchildage1.Text = strChildAges1(0)
                    End If

                    If strChildAges1.Length > 1 Then
                        txtchildage2.Text = strChildAges1(1)
                    End If
                    If strChildAges1.Length > 2 Then
                        txtchildage3.Text = strChildAges1(2)
                    End If
                    If strChildAges1.Length > 3 Then
                        txtchildage4.Text = strChildAges1(3)
                    End If
                    If strChildAges1.Length > 4 Then
                        txtchildage5.Text = strChildAges1(4)
                    End If
                    If strChildAges1.Length > 5 Then
                        txtchildage6.Text = strChildAges1(5)
                    End If
                    Select Case strChildAges1.Length
                        Case 0
                            dvChild1.Style.Add("display", "none")
                            dvChild2.Style.Add("display", "none")
                            dvChild3.Style.Add("display", "none")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "none")
                        Case 1
                            If strChildAges1(0) <> "" Then

                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "block")
                            Else
                                dvChild1.Style.Add("display", "none")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                                chdage.Style.Add("display", "none")
                            End If
                        Case 2
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "none")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 3
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "none")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 4
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "block")
                            dvChild5.Style.Add("display", "none")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 5
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "block")
                            dvChild5.Style.Add("display", "block")
                            dvChild6.Style.Add("display", "none")
                            chdage.Style.Add("display", "block")
                        Case 6
                            dvChild1.Style.Add("display", "block")
                            dvChild2.Style.Add("display", "block")
                            dvChild3.Style.Add("display", "block")
                            dvChild4.Style.Add("display", "block")
                            dvChild5.Style.Add("display", "block")
                            dvChild6.Style.Add("display", "block")
                            chdage.Style.Add("display", "block")
                    End Select


                    'txtchildage1.Text = IIf(Convert.ToString(dtRow("Childage1")) = "", "", dtRow("Childage1"))
                    'txtchildage2.Text = IIf(Convert.ToString(dtRow("Childage2")) = "", "", dtRow("Childage2"))
                    'txtchildage3.Text = IIf(Convert.ToString(dtRow("Childage3")) = "", "", dtRow("Childage3"))
                    'txtchildage4.Text = IIf(Convert.ToString(dtRow("Childage4")) = "", "", dtRow("Childage4"))
                    'txtchildage5.Text = IIf(Convert.ToString(dtRow("Childage5")) = "", "", dtRow("Childage5"))
                    'txtchildage6.Text = IIf(Convert.ToString(dtRow("Childage6")) = "", "", dtRow("Childage6"))


                    btnaddrow.Style.Add("display", "none")
                    RemoveRow.Style.Add("display", "none")

                    dWlMarkup = ((100 + Convert.ToDecimal(hdnwlmarkup.Value)) / 100) * Convert.ToDecimal(hdnwlconvrate.Value)
                    If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

                        dvVisaPrice.Style.Add("display", "none")
                        dvwlVisaPrice.Style.Add("display", "block")

                        txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                        txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtNoOfVisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
                    ElseIf hdWhiteLabel.Value = "1" Then
                        dvVisaPrice.Style.Add("display", "none")
                        dvwlVisaPrice.Style.Add("display", "block")

                        txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                        txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtNoOfVisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
                    Else

                        dvVisaPrice.Style.Add("display", "block")
                        dvwlVisaPrice.Style.Add("display", "none")
                    End If

                End If


            Else
                If Val(hdnlineno.Value) <> 0 Then

                    txtVisaPrice.Text = IIf(Convert.ToString(dtRow("visaprice")) = "", "", dtRow("visaprice"))
                    txtVisaChildPrice.Text = IIf(Convert.ToString(dtRow("visachildprice")) = "", "", dtRow("visachildprice"))
                    txtVisaValue.Text = IIf(Convert.ToString(dtRow("visavalue")) = "", "", dtRow("visavalue"))
                    txtVisaDate.Text = IIf(Convert.ToString(dtRow("visadate")) = "", "", dtRow("visadate"))

                    '*** Danny 22/10/2018 FreeForm SupplierAgent
                    Txt_VisaCost.Text = IIf(Convert.ToString(dtRow("Visacprice")) = "", "", dtRow("Visacprice"))
                    Txt_VisaCostValue.Text = IIf(Convert.ToString(dtRow("visacostvalue")) = "", "", dtRow("visacostvalue"))

                    '  txtNoOfVisas.Text = IIf(Convert.ToString(dtRow("noofvisas")) = "", "", dtRow("noofvisas"))
                    txtNationalityCode.Text = IIf(Convert.ToString(dtRow("nationalitycode")) = "", "", dtRow("nationalitycode"))
                    txtNationality.Text = IIf(Convert.ToString(dtRow("nationalityname")) = "", "", dtRow("nationalityname"))
                    ddlVisatype.SelectedValue = IIf(Convert.ToString(dtRow("visatypecode")) = "", "", dtRow("visatypecode"))
                    ddlVisa.SelectedValue = IIf(Convert.ToString(dtRow("visaoptions")) = "", "", dtRow("visaoptions"))
                    chkComplementCust.Checked = IIf(Convert.ToString(dtRow("complimentarycust")) = "1", True, False)
                    hdnplistcode.Value = IIf(Convert.ToString(dtRow("oplistcode")) = "", "", dtRow("oplistcode"))

                    hdnplistcode.Value = IIf(Convert.ToString(dtRow("oplistcode")) = "", "", dtRow("oplistcode"))
                    hdncplistcode.Value = IIf(Convert.ToString(dtRow("OCPlistCode")) = "", "", dtRow("OCPlistCode"))
                    hdncostprice.Value = IIf(Convert.ToString(dtRow("Visacprice")) = "", "", dtRow("Visacprice"))
                    hdnChildcostprice.Value = IIf(Convert.ToString(dtRow("Visacchildprice")) = "", "", dtRow("Visacchildprice"))
                    hdnpartycode.Value = IIf(Convert.ToString(dtRow("preferredsupplier")) = "", "", dtRow("preferredsupplier"))
                    hdnwlcurrcode.Value = IIf(Convert.ToString(dtRow("wlcurrcode")) = "", "", dtRow("wlcurrcode"))
                    hdnwlmarkup.Value = IIf(Convert.ToString(dtRow("wlmarkupperc")) = "", "", dtRow("wlmarkupperc"))
                    hdnwlconvrate.Value = IIf(Convert.ToString(dtRow("wlconvrate")) = "", "", dtRow("wlconvrate"))
                    txtwlVisaPrice.Text = IIf(Convert.ToString(dtRow("wlvisaprice")) = "", "", dtRow("wlvisaprice"))
                    txtwlVisaValue.Text = IIf(Convert.ToString(dtRow("wlvisavalue")) = "", "", dtRow("wlvisavalue"))
                    hdvisacostvalue.Value = IIf(Convert.ToString(dtRow("visacostvalue")) = "", "", dtRow("visacostvalue"))

                    '' Added shahul 05/04/18 
                    ddlchild.SelectedValue = IIf(Convert.ToString(dtRow("child")) = 0, "--", dtRow("child"))
                    txtNoOfVisas.Text = IIf(Convert.ToString(dtRow("adults")) = "", "", dtRow("adults"))
                    Dim childages1 As String = dtRow("childages").ToString.ToString.Replace(",", ";")
                    If childages1 <> "" Then
                        If Left(childages1, 1) = ";" Then
                            childages1 = Right(childages1, (childages1.Length - 1))
                        End If
                        Dim strChildAges1 As String() = childages1.ToString.Split(";")
                        '''''''
                        If strChildAges1.Length <> 0 Then
                            txtchildage1.Text = strChildAges1(0)
                        End If

                        If strChildAges1.Length > 1 Then
                            txtchildage2.Text = strChildAges1(1)
                        End If
                        If strChildAges1.Length > 2 Then
                            txtchildage3.Text = strChildAges1(2)
                        End If
                        If strChildAges1.Length > 3 Then
                            txtchildage4.Text = strChildAges1(3)
                        End If
                        If strChildAges1.Length > 4 Then
                            txtchildage5.Text = strChildAges1(4)
                        End If
                        If strChildAges1.Length > 5 Then
                            txtchildage6.Text = strChildAges1(5)
                        End If


                        Select Case strChildAges1.Length
                            Case 1
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "none")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 2
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "none")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 3
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "none")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 4
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "none")
                                dvChild6.Style.Add("display", "none")
                            Case 5
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "block")
                                dvChild6.Style.Add("display", "none")
                            Case 6
                                dvChild1.Style.Add("display", "block")
                                dvChild2.Style.Add("display", "block")
                                dvChild3.Style.Add("display", "block")
                                dvChild4.Style.Add("display", "block")
                                dvChild5.Style.Add("display", "block")
                                dvChild6.Style.Add("display", "block")
                        End Select
                    Else
                        dvChild1.Style.Add("display", "none")
                        dvChild2.Style.Add("display", "none")
                        dvChild3.Style.Add("display", "none")
                        dvChild4.Style.Add("display", "none")
                        dvChild5.Style.Add("display", "none")
                        dvChild6.Style.Add("display", "none")
                    End If

                    'txtchildage1.Text = IIf(Convert.ToString(dtRow("Childage1")) = "", "", dtRow("Childage1"))
                    'txtchildage2.Text = IIf(Convert.ToString(dtRow("Childage2")) = "", "", dtRow("Childage2"))
                    'txtchildage3.Text = IIf(Convert.ToString(dtRow("Childage3")) = "", "", dtRow("Childage3"))
                    'txtchildage4.Text = IIf(Convert.ToString(dtRow("Childage4")) = "", "", dtRow("Childage4"))
                    'txtchildage5.Text = IIf(Convert.ToString(dtRow("Childage5")) = "", "", dtRow("Childage5"))
                    'txtchildage6.Text = IIf(Convert.ToString(dtRow("Childage6")) = "", "", dtRow("Childage6"))

                    btnaddrow.Style.Add("display", "none")
                    RemoveRow.Style.Add("display", "none")

                    dWlMarkup = ((100 + Convert.ToDecimal(hdnwlmarkup.Value)) / 100) * Convert.ToDecimal(hdnwlconvrate.Value)
                    If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

                        dvVisaPrice.Style.Add("display", "none")
                        dvwlVisaPrice.Style.Add("display", "block")

                        txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                        txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtNoOfVisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
                    ElseIf hdWhiteLabel.Value = "1" Then
                        dvVisaPrice.Style.Add("display", "none")
                        dvwlVisaPrice.Style.Add("display", "block")

                        txtwlVisaPrice.Text = Val(txtVisaPrice.Text) * dWlMarkup
                        txtwlVisaValue.Text = Math.Round(Convert.ToDecimal(txtNoOfVisas.Text), 2) * Math.Round(Convert.ToDecimal(txtwlVisaPrice.Text), 2)
                    Else
                        dvVisaPrice.Style.Add("display", "block")
                        dvwlVisaPrice.Style.Add("display", "none")
                    End If
                End If
            End If

            If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "block")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "block")
            ElseIf hdWhiteLabel.Value = "1" Then
                dvVisaPrice.Style.Add("display", "none")
                dvwlVisaPrice.Style.Add("display", "block")
                dvVisaValue.Style.Add("display", "none")
                dvwlVisaValue.Style.Add("display", "block")
            Else
                If hdBookingEngineRateType.Value = "1" Then

                    dvVisaValue.Style.Add("display", "none")
                    dvVisaPrice.Style.Add("display", "none")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvwlVisaValue.Style.Add("display", "none")
                Else
                    dvVisaPrice.Style.Add("display", "block")
                    dvwlVisaPrice.Style.Add("display", "none")
                    dvVisaValue.Style.Add("display", "block")
                    dvwlVisaValue.Style.Add("display", "none")

                End If


            End If





            '*** Danny 22/10/2018 FreeForm SupplierAgent
            txtNoOfVisas.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(hdnwlmarkup.ClientID, String) + "','" + CType(ddlchild.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(Txt_VisaCost.ClientID, String) + "','" + CType(Txt_VisaCostValue.ClientID, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")   '' Added shahul 05/04/18  hdnChildcostprice
            txtVisaPrice.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(hdnwlmarkup.ClientID, String) + "','" + CType(ddlchild.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(Txt_VisaCost.ClientID, String) + "','" + CType(Txt_VisaCostValue.ClientID, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")   '' Added shahul 05/04/18
            txtwlVisaPrice.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(hdnwlmarkup.ClientID, String) + "','" + CType(ddlchild.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(Txt_VisaCost.ClientID, String) + "','" + CType(Txt_VisaCostValue.ClientID, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")    '' Added shahul 05/04/18
            Txt_VisaCost.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(hdnwlmarkup.ClientID, String) + "','" + CType(ddlchild.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(Txt_VisaCost.ClientID, String) + "','" + CType(Txt_VisaCostValue.ClientID, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")   '' Added shahul 05/04/18
            Txt_VisaCostValue.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(hdnwlmarkup.ClientID, String) + "','" + CType(ddlchild.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(Txt_VisaCost.ClientID, String) + "','" + CType(Txt_VisaCostValue.ClientID, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")   '' Added shahul 05/04/18
            txtVisaChildPrice.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(hdnwlmarkup.ClientID, String) + "','" + CType(ddlchild.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(Txt_VisaCost.ClientID, String) + "','" + CType(Txt_VisaCostValue.ClientID, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")   '' Added shahul 05/04/18

            ddlVisa.Attributes.Add("onChange", "javascript:return showmessage('" + CType(ddlVisa.ClientID, String) + "','" + CType(ddlVisatype.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "','" + CType(txtVisaChildPrice.ClientID, String) + "')")
            'txtNoOfVisas.Attributes.Add("onChange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "')")
            'txtVisaPrice.Attributes.Add("onchange", "javascript:CalculateVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtVisaPrice.ClientID, String) + "','" + CType(txtVisaValue.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "')")
            'txtwlVisaPrice.Attributes.Add("onchange", "javascript:CalculatewlVisaValue('" + CType(txtNoOfVisas.ClientID, String) + "','" + CType(txtwlVisaPrice.ClientID, String) + "','" + CType(txtwlVisaValue.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "')")

        End If
    End Sub

    Private Sub btnaddrow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddrow.Click
        Try
            Dim dt As New DataTable
            '
            'If Not dt Is Nothing Then
            '    dt.Rows.Clear()
            'End If
            CreateDataTable()
            Dim iRowNo As Integer = 0
            Dim visapax As Integer = 0
            Dim visachild As Integer = 0
            Dim pendingvisaadult As Integer = 0
            If Not Session("VisaDetailsDT") Is Nothing Then
                dt = CType(Session("VisaDetailsDT"), DataTable)

                For Each item As DataListItem In dlVisaInfo.Items
                    Dim txtVisaCustomer As TextBox = CType(item.FindControl("txtVisaCustomer"), TextBox)
                    Dim txtVisaCustomerCode As TextBox = CType(item.FindControl("txtVisaCustomerCode"), TextBox)
                    Dim txtSourceCountry As TextBox = CType(item.FindControl("txtSourceCountry"), TextBox)
                    Dim txtSourceCountryCode As TextBox = CType(item.FindControl("txtSourceCountryCode"), TextBox)
                    Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)
                    Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
                    Dim txtVisaPrice As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
                    Dim txtNoOfVisas As TextBox = CType(item.FindControl("txtNoOfVisas"), TextBox)
                    Dim txtVisaValue As TextBox = CType(item.FindControl("txtVisaValue"), TextBox)
                    Dim txtVisaDate As TextBox = CType(item.FindControl("txtVisaDate"), TextBox)
                    Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
                    Dim txtNationalityCode As TextBox = CType(item.FindControl("txtNationalityCode"), TextBox)

                    '*** Danny 22/10/2018 FreeForm SupplierAgent
                    Dim TxtSupplierCode As TextBox = CType(item.FindControl("Txt_SupplierCode"), TextBox)
                    Dim TxtSupplierName As TextBox = CType(item.FindControl("Txt_SupplierName"), TextBox)
                    Dim TxtSupplierAgentCode As TextBox = CType(item.FindControl("Txt_SupplierAgentCode"), TextBox)
                    Dim TxtSupplierAgentName As TextBox = CType(item.FindControl("Txt_SupplierAgentName"), TextBox)
                    Dim hdnpartycode As HiddenField = CType(item.FindControl("hdnpartycode"), HiddenField)

                    'If TxtSupplierAgentCode.Text.ToString.Trim.Length = 0 Then
                    '    'TxtSupplierAgentCode.Text = strSupplierAgentCode
                    '    'TxtSupplierAgentName.Text = strSupplierAgentName
                    '    hdnpartycode.Value = TxtSupplierAgentCode.Text
                    'Else
                    '    'TxtSupplierAgentCode.Text = strSupplierAgentCode
                    '    'TxtSupplierAgentName.Text = strSupplierAgentName
                    'End If

                    Dim hdnoplistcode As HiddenField = CType(item.FindControl("hdnplistcode"), HiddenField)
                    Dim lblrowno As Label = CType(item.FindControl("lblrowno"), Label)
                    Dim chkComplementCust As CheckBox = CType(item.FindControl("chkComplementCust"), CheckBox)

                    Dim hdncplistcode As HiddenField = CType(item.FindControl("hdncplistcode"), HiddenField)
                    Dim hdncostprice As HiddenField = CType(item.FindControl("hdncostprice"), HiddenField)

                    Dim hdnwlcurrcode As HiddenField = CType(item.FindControl("hdnwlcurrcode"), HiddenField)
                    Dim hdnwlmarkup As HiddenField = CType(item.FindControl("hdnwlmarkup"), HiddenField)
                    Dim hdnwlconvrate As HiddenField = CType(item.FindControl("hdnwlconvrate"), HiddenField)
                    Dim hdvisacostvalue As HiddenField = CType(item.FindControl("hdvisacostvalue"), HiddenField)

                    Dim txtwlVisaPrice As TextBox = CType(item.FindControl("txtwlVisaPrice"), TextBox)
                    Dim txtwlVisaValue As TextBox = CType(item.FindControl("txtwlVisaValue"), TextBox)

                    Dim txtVisaChildPrice As TextBox = CType(item.FindControl("txtVisaChildPrice"), TextBox)
                    Dim hdnChildcostprice As HiddenField = CType(item.FindControl("hdnChildcostprice"), HiddenField)

                    Dim ddlchild As DropDownList = CType(item.FindControl("ddlchild"), DropDownList)

                    Dim dlItem As DataListItem = CType(ddlchild.NamingContainer, DataListItem)

                    Dim txtchildage1 As TextBox = CType(dlItem.FindControl("txtchildage1"), TextBox)
                    Dim txtchildage2 As TextBox = CType(dlItem.FindControl("txtchildage2"), TextBox)
                    Dim txtchildage3 As TextBox = CType(dlItem.FindControl("txtchildage3"), TextBox)
                    Dim txtchildage4 As TextBox = CType(dlItem.FindControl("txtchildage4"), TextBox)
                    Dim txtchildage5 As TextBox = CType(dlItem.FindControl("txtchildage5"), TextBox)
                    Dim txtchildage6 As TextBox = CType(dlItem.FindControl("txtchildage6"), TextBox)

                    Dim dvChild1 As HtmlGenericControl = CType(dlItem.FindControl("dvChild1"), HtmlGenericControl)
                    Dim dvChild2 As HtmlGenericControl = CType(dlItem.FindControl("dvChild2"), HtmlGenericControl)
                    Dim dvChild3 As HtmlGenericControl = CType(dlItem.FindControl("dvChild3"), HtmlGenericControl)
                    Dim dvChild4 As HtmlGenericControl = CType(dlItem.FindControl("dvChild4"), HtmlGenericControl)
                    Dim dvChild5 As HtmlGenericControl = CType(dlItem.FindControl("dvChild5"), HtmlGenericControl)
                    Dim dvChild6 As HtmlGenericControl = CType(dlItem.FindControl("dvChild6"), HtmlGenericControl)



                    lblrowno.Text = item.ItemIndex + 1
                    iRowNo = iRowNo + 1
                    visapax = visapax + Val(txtNoOfVisas.Text)
                    visachild = visachild + Val(ddlchild.SelectedValue)
                    pendingvisaadult = Val(txtNoOfVisas.Text)

                    ', TxtSupplierCode.Text

                    '*** Danny 22/10/2018 FreeForm SupplierAgent
                    dt.Rows.Add(lblrowno.Text, txtNoOfVisas.Text, txtNationalityCode.Text _
                                    , TxtSupplierName.Text, TxtSupplierAgentCode.Text, TxtSupplierAgentName.Text _
                                    , ddlVisa.SelectedValue, ddlVisatype.SelectedValue, txtVisaDate.Text, _
                                  txtVisaPrice.Text, txtVisaValue.Text, hdnoplistcode.Value, CType(IIf(chkComplementCust.Checked, "1", "0"), Integer), hdncplistcode.Value, _
                                  hdncostprice.Value, TxtSupplierCode.Text.ToString.Trim, hdnwlcurrcode.Value, hdnwlmarkup.Value, hdnwlconvrate.Value, txtwlVisaPrice.Text, txtwlVisaValue.Text, _
                                  hdvisacostvalue.Value, ddlchild.SelectedValue, txtchildage1.Text, txtchildage2.Text, txtchildage3.Text, txtchildage4.Text, txtchildage5.Text, txtchildage6.Text, txtVisaChildPrice.Text, hdnChildcostprice.Value)
                Next

                iRowNo = iRowNo + 1
                Dim dr As DataRow = dt.NewRow

                Dim pendingvisas As Integer = ViewState("Totalvisas") - (Val(visapax) + Val(visachild))
                'ViewState("Adultvisas") = Val(dt.Rows(0).Item("adults"))
                'ViewState("Childvisas")
                Dim pendingvisaschild As Integer = (Val(ViewState("Childvisas")) - Val(visachild))
                Dim pendingvisasadult As Integer = (Val(ViewState("Adultvisas")) - Val(visapax))
                If Val(pendingvisas) < 0 Then
                    dt.Rows.Add(1)
                    dt.Rows(dt.Rows.Count - 1)("VlineNo") = iRowNo
                    dt.Rows(dt.Rows.Count - 1)("SupplierAgentCode") = Session("strSupplierAgentCode").ToString
                    dt.Rows(dt.Rows.Count - 1)("SupplierAgentName") = Session("strSupplierAgentName").ToString
                    ''dt.Rows.Add(iRowNo, "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                Else
                    dt.Rows.Add(iRowNo, pendingvisasadult, "", "", Session("strSupplierAgentCode").ToString, Session("strSupplierAgentName").ToString, "", "", "", "", "", "", 0, "", "", "", "", "", "", "", "", "", pendingvisaschild, "", "", "", "", "", "")

                End If
                '  dt.Rows.Add(iRowNo, pendingvisas, "", "", "", "", "", "", 0)
                Session("VisaDetailsDT") = dt
                dlVisaInfo.DataSource = dt
                dlVisaInfo.DataBind()
                bindvisadetails()
                Dim txtNoOfVisasfocus As TextBox = CType(dlVisaInfo.Items(dlVisaInfo.Items.Count - 1).FindControl("txtNoOfVisas"), TextBox)
                txtNoOfVisasfocus.Focus()

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: btnaddrow_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
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

    Private Function ValidateVisaDetails() As String
        Dim totalpax As Integer = 0
        For Each item As DataListItem In dlVisaInfo.Items
            Dim txtVisaCustomer As TextBox = CType(item.FindControl("txtVisaCustomer"), TextBox)
            Dim txtVisaCustomerCode As TextBox = CType(item.FindControl("txtVisaCustomerCode"), TextBox)
            Dim txtSourceCountry As TextBox = CType(item.FindControl("txtSourceCountry"), TextBox)
            Dim txtSourceCountryCode As TextBox = CType(item.FindControl("txtSourceCountryCode"), TextBox)
            Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)
            Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
            Dim txtVisaPrice As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
            Dim txtNoOfVisas As TextBox = CType(item.FindControl("txtNoOfVisas"), TextBox)
            Dim txtVisaValue As TextBox = CType(item.FindControl("txtVisaValue"), TextBox)
            Dim txtVisaDate As TextBox = CType(item.FindControl("txtVisaDate"), TextBox)
            Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
            Dim txtNationalityCode As TextBox = CType(item.FindControl("txtNationalityCode"), TextBox)
            Dim hdnplistcode As HiddenField = CType(item.FindControl("hdnplistcode"), HiddenField)

            '*** Danny 22/10/2018 FreeForm SupplierAgent
            Dim Txt_VisaCost As TextBox = CType(item.FindControl("Txt_VisaCost"), TextBox)
            Dim Txt_VisaCostValue As TextBox = CType(item.FindControl("Txt_VisaCostValue"), TextBox)
            Dim dv_VatCost As HtmlGenericControl = CType(item.FindControl("div_VatCost"), HtmlGenericControl)

            If ViewState("vFreeForm") = "FreeForm" And dv_VatCost.Visible = True Then
                If Val(Txt_VisaCost.Text) = 0 Or Val(Txt_VisaCostValue.Text) = 0 Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Visa Cost ")
                    Txt_VisaCost.Focus()
                    Return False
                    Exit Function
                End If
            End If

            Dim ddlchild As DropDownList = CType(item.FindControl("ddlchild"), DropDownList)
            If ddlVisa.SelectedIndex = "0" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any Visa")
                ' txtNoOfVisas.Focus()
                Return False
                Exit Function
            End If

            If ddlVisa.SelectedIndex = "1" Then '*** Danny 25/10/2018 To validate Visa type Field
                If ddlVisatype.Text = "--" Or ddlVisatype.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Visa Type")
                    ddlVisatype.Focus()
                    Return False
                    Exit Function
                End If
            End If

            If ddlVisa.SelectedIndex <> "2" Then
                If txtNoOfVisas.Text = "" And Val(ddlchild.SelectedValue) = 0 Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter No. Of Visas")
                    ' txtNoOfVisas.Focus()
                    Return False
                    Exit Function
                End If
            End If

            If txtVisaDate.Text.Trim = "" Then '*** Danny 25/10/2018 To validate Visa type Field
                'If txtNoOfVisas.Text = "" And Val(ddlchild.SelectedValue) = 0 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Select Visa Date")
                ' txtNoOfVisas.Focus()
                Return False
                Exit Function
                'End If
            Else
                Try
                    Dim dat As DateTime = txtVisaDate.Text.Trim

                Catch ex As Exception
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Invalid Visa Date")
                    Return False
                    Exit Function
                End Try

            End If

            If txtNationality.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Nationality")
                ' txtNationality.Focus()
                Return False
                Exit Function
            End If

            If (txtVisaPrice.Text = "" Or Val(txtVisaPrice.Text) = 0) And ddlVisa.SelectedValue = "Required" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Visa Price Should not be Empty or 0 ")
                ' txtNationality.Focus()
                Return False
                Exit Function
            End If

            If (txtVisaValue.Text = "" Or Val(txtVisaValue.Text) = 0) And ddlVisa.SelectedValue = "Required" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Visa Value Should not be Empty or 0 ")
                ' txtNationality.Focus()
                Return False
                Exit Function
            End If


            'If ddlVisa.SelectedValue = "Not Required" Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Visa Type Selected Not Required you Can't  Book Visa's")
            '    ' ddlVisa.Focus()
            '    Return False
            '    Exit Function
            'End If
            totalpax = totalpax + Val(txtNoOfVisas.Text) + Val(ddlchild.SelectedValue)

        Next
        If dlVisaInfo.Items.Count = 0 Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select any One VisaType")

            Return False
            Exit Function
        End If
        If totalpax < ViewState("Totalvisas") And Val(ViewState("visascount")) = 0 Then
            btnaddrow_Click(Nothing, Nothing)
            MessageBox.ShowMessage(Page, MessageType.Warning, "Total Pax not Tally with Selected Visa's")

            Return False
            Exit Function

        End If


        Return True
    End Function




    Protected Sub chkOveridePrice_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOveridePrice.CheckedChanged
        Try
            If chkOveridePrice.Checked Then
                For Each item As DataListItem In dlVisaInfo.Items
                    Dim txtVisaPrice As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
                    Dim txtwlVisaPrice As TextBox = CType(item.FindControl("txtwlVisaPrice"), TextBox)
                    Dim txtVisaChildPrice As TextBox = CType(item.FindControl("txtVisaChildPrice"), TextBox)
                    txtVisaPrice.Attributes.Remove("readonly")
                    txtwlVisaPrice.Attributes.Remove("readonly")
                    txtVisaChildPrice.Attributes.Remove("readonly")
                    txtVisaPrice.Focus()
                Next
            Else
                For Each item As DataListItem In dlVisaInfo.Items
                    Dim txtVisaPrice As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
                    Dim txtwlVisaPrice As TextBox = CType(item.FindControl("txtwlVisaPrice"), TextBox)
                    Dim txtVisaChildPrice As TextBox = CType(item.FindControl("txtVisaChildPrice"), TextBox)
                    txtVisaPrice.Attributes.Add("readonly", "readonly")
                    txtwlVisaPrice.Attributes.Add("readonly", "readonly")
                    txtVisaChildPrice.Attributes.Add("readonly", "readonly")
                Next
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: chkOveridePrice_CheckedChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    'Protected Sub btnSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelected.Click

    '    Try

    '        Dim iValue As Integer = hddlRowNumber.Value



    '        Dim dlItem As DataListItem = dlVisaInfo.Items(iValue)
    '        Dim ddlVisaType As DropDownList = CType(dlItem.FindControl("ddlVisaType"), DropDownList)
    '        Dim str As String = ddlVisaType.SelectedValue

    '        Dim txtVisaPrice As TextBox = CType(dlItem.FindControl("txtVisaPrice"), TextBox)
    '        Dim hdnplistcode As HiddenField = CType(dlItem.FindControl("hdnplistcode"), HiddenField)
    '        Dim txtNationalityCode As TextBox = CType(dlItem.FindControl("txtNationalityCode"), TextBox)
    '        Dim txtVisaValue As TextBox = CType(dlItem.FindControl("txtVisaValue"), TextBox)
    '        Dim txtnoofvisas As TextBox = CType(dlItem.FindControl("txtnoofvisas"), TextBox)
    '        Dim lblrowno As Label = CType(dlItem.FindControl("lblrowno"), Label)
    '        Dim txtNationality As TextBox = CType(dlItem.FindControl("txtNationality"), TextBox)
    '        Dim objBLLHotelSearch = New BLLHotelSearch

    '        If ddlVisaType.Text = "--" Then
    '            txtVisaPrice.Text = ""
    '            txtnoofvisas.Text = ""
    '            txtVisaValue.Text = ""
    '        Else
    '            ''" & objBLLHotelSearch.SourceCountryCode & "'

    '            Dim visapriceplistcode As String = GetVisaPrice(ddlVisaType.SelectedValue, txtNationalityCode.Text)
    '            If visapriceplistcode <> "" Then
    '                hdnplistcode.Value = visapriceplistcode.Split("|")(0)
    '                txtVisaPrice.Text = Math.Round(Convert.ToDecimal(visapriceplistcode.Split("|")(1)), 2)
    '                If txtnoofvisas.Text <> "" And txtVisaPrice.Text <> "" Then

    '                    txtVisaValue.Text = Math.Round(Convert.ToDecimal(txtnoofvisas.Text), 2) * Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)

    '                Else

    '                    txtVisaValue.Text = Math.Round(Convert.ToDecimal(txtVisaPrice.Text), 2)
    '                End If
    '            Else
    '                txtVisaPrice.Text = ""
    '                hdnplistcode.Value = ""
    '                txtVisaValue.Text = ""
    '            End If
    '            ' If txtNationalityCode.Text = "IN" Then

    '            Dim visaremarks As String = objclsUtilities.ExecuteQueryReturnStringValue("select visaremarks from ctrymast where isnull(visaremarks,'')<>'' and ctrycode='" & txtNationalityCode.Text & "'")
    '            If visaremarks <> "" Then
    '                MessageBox.ShowMessage(Page, MessageType.Info, "Choosen Nationality...</Br></br>" + txtNationality.Text + " -- " + visaremarks)
    '            End If
    '            'End If


    '        End If
    '        txtnoofvisas.Focus()
    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("VisaBooking.aspx :: btnSelected_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ValidateVisaDetails() Then


                Dim strBuffer As New Text.StringBuilder

                objBLLVisa.VBRequestid = GetExistingRequestId()
                Dim i As Integer = 1
                Dim rlineonostring As String = ""
                Dim strVlineno As String = ""
                If dlVisaInfo.Items.Count > 0 Then
                    For Each item As DataListItem In dlVisaInfo.Items
                        Dim strTittle As String = ""
                        Dim txtVisaCustomer As TextBox = CType(item.FindControl("txtVisaCustomer"), TextBox)
                        Dim txtVisaCustomerCode As TextBox = CType(item.FindControl("txtVisaCustomerCode"), TextBox)
                        Dim txtSourceCountry As TextBox = CType(item.FindControl("txtSourceCountry"), TextBox)
                        Dim txtSourceCountryCode As TextBox = CType(item.FindControl("txtSourceCountryCode"), TextBox)
                        Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)
                        Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
                        Dim txtVisaPrice As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
                        Dim txtVisaChildPrice As TextBox = CType(item.FindControl("txtVisaChildPrice"), TextBox)
                        Dim txtNoOfVisas As TextBox = CType(item.FindControl("txtNoOfVisas"), TextBox)
                        Dim txtVisaValue As TextBox = CType(item.FindControl("txtVisaValue"), TextBox)
                        Dim txtVisaDate As TextBox = CType(item.FindControl("txtVisaDate"), TextBox)
                        Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
                        Dim txtNationalityCode As TextBox = CType(item.FindControl("txtNationalityCode"), TextBox)

                        '*** Danny 22/10/2018 FreeForm SupplierAgent
                        Dim Txt_SupplierCode As TextBox = CType(item.FindControl("Txt_SupplierCode"), TextBox)
                        Dim Txt_SupplierAgentCode As TextBox = CType(item.FindControl("Txt_SupplierAgentCode"), TextBox)
                        Dim Txt_VisaCost As TextBox = CType(item.FindControl("Txt_VisaCost"), TextBox)
                        Dim Txt_VisaCostValue As TextBox = CType(item.FindControl("Txt_VisaCostValue"), TextBox)
                        Dim dv_VatCost As HtmlGenericControl = CType(item.FindControl("div_VatCost"), HtmlGenericControl)

                        Dim hdnplistcode As HiddenField = CType(item.FindControl("hdnplistcode"), HiddenField)
                        Dim chkComplementCust As CheckBox = CType(item.FindControl("chkComplementCust"), CheckBox)

                        Dim hdncplistcode As HiddenField = CType(item.FindControl("hdncplistcode"), HiddenField)
                        Dim hdncostprice As HiddenField = CType(item.FindControl("hdncostprice"), HiddenField)
                        Dim hdncostChildprice As HiddenField = CType(item.FindControl("hdncostChildprice"), HiddenField)
                        Dim hdnpartycode As HiddenField = CType(item.FindControl("hdnpartycode"), HiddenField)

                        Dim hdnwlcurrcode As HiddenField = CType(item.FindControl("hdnwlcurrcode"), HiddenField)
                        Dim hdnwlmarkup As HiddenField = CType(item.FindControl("hdnwlmarkup"), HiddenField)
                        Dim hdnwlconvrate As HiddenField = CType(item.FindControl("hdnwlconvrate"), HiddenField)

                        Dim lblrowno As Label = CType(item.FindControl("lblrowno"), Label)

                        Dim txtwlVisaPrice As TextBox = CType(item.FindControl("txtwlVisaPrice"), TextBox)
                        Dim txtwlVisaValue As TextBox = CType(item.FindControl("txtwlVisaValue"), TextBox)
                        '' Added shahul 05/04/18
                        Dim ddlchild As DropDownList = CType(item.FindControl("ddlchild"), DropDownList)
                        Dim txtchildage1 As TextBox = CType(item.FindControl("txtchildage1"), TextBox)
                        Dim txtchildage2 As TextBox = CType(item.FindControl("txtchildage2"), TextBox)
                        Dim txtchildage3 As TextBox = CType(item.FindControl("txtchildage3"), TextBox)
                        Dim txtchildage4 As TextBox = CType(item.FindControl("txtchildage4"), TextBox)
                        Dim txtchildage5 As TextBox = CType(item.FindControl("txtchildage5"), TextBox)
                        Dim txtchildage6 As TextBox = CType(item.FindControl("txtchildage6"), TextBox)

                        Dim childagestring As String = ""

                        Select Case ddlchild.SelectedValue
                            Case 1
                                childagestring = txtchildage1.Text
                            Case 2
                                childagestring = txtchildage1.Text + ";" + txtchildage2.Text
                            Case 3
                                childagestring = txtchildage1.Text + ";" + txtchildage2.Text + ";" + txtchildage3.Text
                            Case 4
                                childagestring = txtchildage1.Text + ";" + txtchildage2.Text + ";" + txtchildage3.Text + ";" + txtchildage4.Text
                            Case 5
                                childagestring = txtchildage1.Text + ";" + txtchildage2.Text + ";" + txtchildage3.Text + ";" + txtchildage4.Text + ";" + txtchildage5.Text
                            Case 6
                                childagestring = txtchildage1.Text + ";" + txtchildage2.Text + ";" + txtchildage3.Text + ";" + txtchildage4.Text + ";" + txtchildage5.Text + ";" + txtchildage6.Text

                        End Select

                        If Val(hdnlineno.Value) = 0 Then

                            If strVlineno = "" Then
                                strVlineno = objBLLCommonFuntions.GetBookingRowLineNo(objBLLVisa.VBRequestid, "VISA")
                            Else
                                strVlineno = CType(strVlineno, Integer) + 1
                            End If

                        Else
                            strVlineno = hdnlineno.Value 'ViewState("Vlineno")
                        End If

                        If rlineonostring <> "" Then
                            rlineonostring = rlineonostring + ";" + CType(strVlineno, String)
                        Else
                            rlineonostring = strVlineno
                        End If



                        Dim strvisadate As String = txtVisaDate.Text
                        If strvisadate <> "" Then
                            Dim strDates As String() = strvisadate.Split("/")
                            strvisadate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                        End If


                        strBuffer.Append("<DocumentElement>")
                        strBuffer.Append("<Table>")
                        strBuffer.Append(" <vlineno>" & strVlineno & "</vlineno>")
                        strBuffer.Append(" <noofvisas>" & CType(Val(txtNoOfVisas.Text) + Val(ddlchild.SelectedValue), String) & "</noofvisas>")
                        strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
                        strBuffer.Append(" <visaoptions>" & ddlVisa.SelectedValue & "</visaoptions>")
                        strBuffer.Append(" <visatypecode>" & ddlVisatype.SelectedValue & "</visatypecode>")
                        strBuffer.Append(" <visadate>" & Format(CType(strvisadate, Date), "yyyy/MM/dd") & "</visadate>")
                        strBuffer.Append(" <visaprice>" & CType(Val(txtVisaPrice.Text), Decimal) & "</visaprice>")
                        strBuffer.Append(" <visavalue>" & CType(Val(txtVisaValue.Text), Decimal) & "</visavalue>")
                        strBuffer.Append("<wlvisaprice>" & CType(Val(txtwlVisaPrice.Text), Decimal) & "</wlvisaprice>")
                        strBuffer.Append("<wlvisavalue>" & CType(Val(txtwlVisaValue.Text), Decimal) & "</wlvisavalue>")
                        strBuffer.Append(" <oplistcode>" & hdnplistcode.Value & "</oplistcode>")
                        strBuffer.Append(" <complimentarycust>" & IIf(chkComplementCust.Checked, "1", "0") & "</complimentarycust>")
                        strBuffer.Append("<reqoverride>" & IIf(chkOveridePrice.Checked, "1", "0") & "</reqoverride>")

                        '*** Danny 22/10/2018 FreeForm SupplierAgent
                        'If hdnpartycode.Value.ToString.Trim.Length = 0 Then
                        strBuffer.Append(" <preferredsupplier>" & Txt_SupplierCode.Text & "</preferredsupplier>")
                        'Else

                        '    strBuffer.Append("<preferredsupplier>" & hdnpartycode.Value & "</preferredsupplier>")
                        'End If

                        Dim strQuery As String = ""
                        strQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisatype.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec"

                        Dim strVisaPrice As String
                        Dim visapricedt1 As New DataTable

                        visapricedt1 = objclsUtilities.GetDataFromDataTable(strQuery)


                        If visapricedt1.Rows.Count > 0 Then

                            hdncostprice.Value = CType(visapricedt1.Rows(0).Item("ocostprice"), String)
                            hdncplistcode.Value = CType(visapricedt1.Rows(0).Item("ocplistcode"), String)
                        End If




                        Dim hdnChildcostprice As HiddenField = CType(item.FindControl("hdnChildcostprice"), HiddenField)
                        Dim strChildQuery As String = ""
                        strChildQuery = "select ocplistcode,ocostprice,partycode from view_visa_costprices_preferred(nolock) where othtypcode='" & ddlVisatype.SelectedValue & "' and convert(datetime,'" & txtVisaDate.Text & "',105) between frmdatec and todatec  and othcatcode='VISACHG-CH'"
                        Dim visaChildpricedt As New DataTable
                        visaChildpricedt = objclsUtilities.GetDataFromDataTable(strChildQuery)
                        If visaChildpricedt.Rows.Count > 0 Then
                            hdnChildcostprice.Value = CType(visaChildpricedt.Rows(0).Item("ocostprice"), String)
                        End If

                        ' End If

                        '*** Danny 22/10/2018 FreeForm SupplierAgent
                        If dv_VatCost.Visible = True Then
                            If Txt_VisaCost.Visible = True Then
                                hdncostprice.Value = CType(Txt_VisaCost.Text, String)
                                hdncplistcode.Value = CType(Txt_VisaCostValue.Text, String)
                            End If
                        End If

                        strBuffer.Append("<visacostprice>" & Val(hdncostprice.Value) & "</visacostprice>")
                        strBuffer.Append("<visacostvalue>" & CType((Val(hdncostprice.Value) * (Val(txtNoOfVisas.Text)) + (Val(ddlchild.SelectedValue) * Val(hdnChildcostprice.Value))), Decimal) & "</visacostvalue>")
                        strBuffer.Append("<ocplistcode>" & hdncplistcode.Value & "</ocplistcode>")
                        strBuffer.Append("<wlcurrcode>" & hdnwlcurrcode.Value & "</wlcurrcode>")
                        strBuffer.Append("<wlconvrate>" & CType(Val(hdnwlconvrate.Value), Decimal) & "</wlconvrate>")
                        strBuffer.Append("<wlmarkupperc>" & CType(Val(hdnwlmarkup.Value), Decimal) & "</wlmarkupperc>")
                        '' Added shahul 05/04/18
                        strBuffer.Append(" <adults>" & txtNoOfVisas.Text & "</adults>")
                        strBuffer.Append(" <child>" & IIf(ddlchild.SelectedValue = "--", "0", ddlchild.SelectedValue) & "</child>")
                        strBuffer.Append(" <childages>" & childagestring & "</childages>")

                        strBuffer.Append(" <childages>" & childagestring & "</childages>")
                        If Not ViewState("vFreeForm") Is Nothing Then ' Modified by abin on 20180728
                            strBuffer.Append(" <bookingmode>FreeForm</bookingmode>")
                        Else
                            strBuffer.Append(" <bookingmode>Normal</bookingmode>")
                        End If

                        '*** Danny 22/10/2018 FreeForm SupplierAgent
                        If txtVisaChildPrice.Text = "" Then
                            txtVisaChildPrice.Text = "0"
                        End If
                        If hdnChildcostprice.Value = "" Then
                            hdnChildcostprice.Value = "0"
                        End If
                        strBuffer.Append(" <SupplierAgentCode>" & Txt_SupplierAgentCode.Text & "</SupplierAgentCode>")
                        strBuffer.Append(" <visachildprice>" & txtVisaChildPrice.Text & "</visachildprice>")
                        strBuffer.Append(" <visacchildprice>" & hdnChildcostprice.Value & "</visacchildprice>")
                        strBuffer.Append("</Table>")
                        strBuffer.Append("</DocumentElement>")
                        '  i = i + 1
                    Next
                End If
                objBLLVisa.VBVisaXml = strBuffer.ToString
                objBLLVisa.VBuserlogged = Session("GlobalUserName")
                objBLLVisa.VBRlinenoString = rlineonostring






                If objBLLVisa.SavingVisaBookingInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                    Response.Redirect("~\MoreServices.aspx")
                    '' ' BindVisaSummary()
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: btnSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
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
            objclsUtilities.WriteErrorLog("VisaBooking.aspx :: LoadFooter :: " & ex.Message & ":: " & Session("GlobalUserName"))
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
