Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Imports System.Diagnostics
Imports ClosedXML.Excel
Partial Class PrintPage
    Inherits System.Web.UI.Page
    Dim objclsUtilities As New clsUtilities
    Dim objclsquotecosting As New clsQuoteCosting

    Private Function GetStyleName(Optional ByVal strClientID As String = "") As String
        Try
            If strClientID Is Nothing Then strClientID = "924065660726315"
            Dim strStyle As String = ""
            strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select StylesheetName from TB_Clientstyles where RandomNumber=" & strClientID).ToString()

            If strStyle IsNot Nothing Then
                If strStyle.Trim().Length = 0 Or strStyle.Trim() = "0" Then strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=58").ToString()
            Else
                strStyle = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=58").ToString()
            End If

            Return strStyle
        Catch
            Return "style-style1.css"
        End Try
    End Function

    Private Sub GetReservationParamValues()
        Dim strParams As String = "514,2009"
        Dim objReservationParm As DataTable
        Dim objBLLLogin As New BLLLogin
        objReservationParm = objBLLLogin.GetReservationParamValues(strParams)

        If objReservationParm IsNot Nothing Then

            If objReservationParm.Rows.Count > 0 Then
                Dim objResParam As ReservationParameters = New ReservationParameters()
                If Session("sobjResParam") IsNot Nothing Then objResParam = CType(Session("sobjResParam"), ReservationParameters)

                For i As Integer = 0 To objReservationParm.Rows.Count - 1
                    If objReservationParm.Rows(i)("param_id").ToString() = "514" Then objResParam.NoOfNightLimit = objReservationParm.Rows(i)("option_selected").ToString()
                    If objReservationParm.Rows(i)("param_id").ToString() = "2009" Then objResParam.ChildAgeLimit = objReservationParm.Rows(i)("option_selected").ToString()
                Next

                Session("sobjResParam") = objResParam
            End If
        End If
    End Sub
    Private Sub fnROLogin(ByVal lUserEmail As String)
        Session.Clear()
        Session("sRequestId") = Nothing
        Session("sEditRequestId") = Nothing
        Dim strAbsoluteUri As String = Page.Request.Url.AbsoluteUri.ToString().Replace("loginokta.aspx", "login.aspx") & "?ro=1"
        strAbsoluteUri = ConfigurationManager.AppSettings("idp_sso_target_logouturl")
        Session("sAbsoluteUrl") = strAbsoluteUri
        Session("strTheme") = ""
        Dim strTheme As String = ""
        Dim strCompany As String
        Dim objBLLLogin As New BLLLogin

        If Request.QueryString("comp") IsNot Nothing Then
            strCompany = Request.QueryString("comp")
            Session("sAgentCompany") = strCompany

            If strCompany = "924065660726315" Then
                Session("sDivCode") = "01"
                strTheme = "css/" & GetStyleName(strCompany)
            ElseIf strCompany = "675558760549078" Then
                Session("sDivCode") = "02"
                strTheme = "css/style-style3.css"
            Else
                Session("sDivCode") = "0"
                strTheme = "css/" & GetStyleName(strCompany)
            End If
        Else
            strCompany = "924065660726315"
            Session("sAgentCompany") = strCompany
            Session("sDivCode") = "01"
            strTheme = "css/" & GetStyleName(strCompany)
        End If

        Session("strTheme") = strTheme

        If Session("sobjResParam") Is Nothing Then
            Dim objResParam As ReservationParameters = New ReservationParameters()
            objResParam.AbsoluteUrl = strAbsoluteUri
            objResParam.AgentCompany = strCompany
            objResParam.DivCode = CStr(Session("sDivCode"))
            objResParam.CssTheme = strTheme
            Session("sobjResParam") = objResParam
        Else
            Dim objResParam As ReservationParameters = New ReservationParameters()
            objResParam = CType(Session("sobjResParam"), ReservationParameters)
            objResParam.AbsoluteUrl = strAbsoluteUri
            objResParam.AgentCompany = strCompany
            objResParam.DivCode = CStr(Session("sDivCode"))
            objResParam.CssTheme = strTheme
            Session("sobjResParam") = objResParam
        End If

        Dim lUserName As String
        lUserName = "admin"

        If lUserName Is Nothing Then
            Response.Write("The account is not authorized to login")
            Return
        End If

        If lUserName.Trim() = "" Then
            Response.Write("The account is not authorized to login")
            Return
        End If

        If objBLLLogin.ValidateROUser(lUserName.Trim()) = True Then
            Session.Add("sLoginType", "RO")
            Dim objResParam As ReservationParameters = New ReservationParameters()
            If Session("sobjResParam") IsNot Nothing Then objResParam = CType(Session("sobjResParam"), ReservationParameters)
            objResParam.LoginType = "RO"
            Session("sLang") = "en-us"
            Dim objDataTable As DataTable
            objDataTable = objBLLLogin.LoadLoginPageSessionFields(CStr(Session("sAgentCompany")))

            If objDataTable.Rows.Count > 0 Then
                Session.Add("sAgentCode", objDataTable.Rows(0)("agentcode").ToString())
                Session.Add("sCurrencyCode", objDataTable.Rows(0)("currcode").ToString())
                Session.Add("sCountryCode", objDataTable.Rows(0)("ctrycode").ToString())
                Session.Add("GlobalUserName", lUserName.Trim())
                objResParam.AgentCode = objDataTable.Rows(0)("agentcode").ToString()
                objResParam.GlobalUserName = lUserName.Trim()
            Else
            End If

            GetReservationParamValues()
            Dim LoginIp As String = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            objResParam.LoginIp = LoginIp
            Dim strIpLocation As String = ""
            objResParam.LoginIpLocationName = strIpLocation
            Dim strAgentCode As String = CStr(Session("sAgentCode").ToString())
            Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch()
            objResParam.Cumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode).ToString()
            objResParam.WhiteLabel = objBLLHotelSearch.FindWhiteLabel(strAgentCode).ToString()
            Session("sobjResParam") = objResParam
            'Response.Redirect("Home.aspx", False)
        Else
            Response.Write("The account is not authorized to login")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("RedirectForPDF") IsNot Nothing Then
            If Request.QueryString("RedirectForPDF") = 1 Then
                If Session("sobjResParam") Is Nothing Or Session("GlobalUserName") Is Nothing Then

                    fnROLogin("admin")
                    Session("RedirectForPDF") = "1"
                Else
                    Session("RedirectForPDF") = ""
                End If
            Else
                Session("RedirectForPDF") = ""
            End If
        Else
            Session("RedirectForPDF") = ""
        End If

        If Not Session("sobjResParam") Is Nothing And Not Session("GlobalUserName") Is Nothing Then
            Try
                Dim printId = Request.QueryString("printId")
                Dim fileName As String = ""
                Dim bytes As Byte()
                If printId = "bookingQuote" Then
                    Dim bQuote As clsBookingQuotePdf = New clsBookingQuotePdf()
                    Dim clsQuoteDownload As clsBookingQuoteDownload = New clsBookingQuoteDownload()
                    Dim quoteid As String = Request.QueryString("quoteId")
                    bytes = {}
                    Dim ds As New DataSet
                    Dim objResParam As New ReservationParameters
                    objResParam = Session("sobjResParam")
                    Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + quoteid + "'")
                    If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""


                    'Dim wsSupplier As IXLWorksheet = wb.Worksheets.Add("Purchase related details")
                    'Dim wsCustomer As IXLWorksheet = wb.Worksheets.Add("Sales related details")
                    'Dim wsInfo As IXLWorksheet = wb.Worksheets.Add("Information details")
                    'IXLRange agentmast0, agentmast, agentmast1, agentmast2, Rnghead;

                    If chkCumulative.Trim() = "CUMULATIVE" Then
                        'changed by mohamed on 26/10/2020
                        fileName = "Quote@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                        fileName = Replace(fileName, "\", "").Replace("/", "")
                        'bQuote.GenerateCumulative(quoteid, bytes, ds, "download", objResParam)
                        bQuote.GenerateCumulative(quoteid, bytes, ds, "", objResParam, fileName)
                        Response.Redirect("~/SavedReports/" & fileName)
                    Else

                        If Not Session("QuoteDownload") Is Nothing Then
                            If Session("QuoteDownload").ToString() = "YES" Then
                                Dim ExlWorkBook As New XLWorkbook
                                Dim FilePath As String = Server.MapPath("ExcelTemplates\QuoteEditTemplate_Logo.xlsx")

                                ExlWorkBook = New XLWorkbook(FilePath)
                                ExlWorkBook = clsQuoteDownload.GenerateReport_Download(quoteid, bytes, objResParam, ExlWorkBook)

                                Dim SavePath As String = Server.MapPath("SavedReports\" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + "_Quote.xlsx")
                                'SavePath = "C:/Akshaya/Temp/" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + "_Quote.xlsx"
                                'ExlWorkBook.SaveAs(SavePath, True)
                                Session("QuoteDownload") = ""


                                Dim Ms As New MemoryStream

                                ExlWorkBook.SaveAs(Ms)
                                SavePath = "Quote_" + quoteid.Replace("/", "") + ".xlsx"


                                Response.Clear()
                                Response.AddHeader("Content-Type", "application/octet-stream")
                                'Response.AddHeader("Content-Transfer-Encoding", "Binary")
                                Response.AddHeader("Content-disposition", "attachment; filename=" + SavePath)
                                Response.AddHeader("Content-Length", Ms.Length.ToString())
                                Response.ContentType = "application/xlsx"
                                Response.Buffer = True
                                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                                Response.BinaryWrite(Ms.ToArray)
                                'Response.End()
                                Response.Flush()
                                HttpContext.Current.ApplicationInstance.CompleteRequest()




                                'Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                                'Response.AddHeader("Content-Length", bytes.Length.ToString())
                                'Response.ContentType = "application/pdf"
                                'Response.Buffer = True
                                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                                'Response.BinaryWrite(bytes)
                                'Response.Flush()
                                'HttpContext.Current.ApplicationInstance.CompleteRequest()
                                'Environment.GetFolderPath(Environment.SpecialFolder.).

                                '                        My.Computer.Network.DownloadFile(SavePath,
                                '"C:/Akshaya/Temp/" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + "_Quote.xlsx")



                            Else
                                fileName = "Quote@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                                fileName = Replace(fileName, "\", "").Replace("/", "")
                                bQuote.GenerateReport(quoteid, bytes, objResParam, "", fileName)
                                Response.Redirect("~/SavedReports/" & fileName)
                            End If
                        Else
                            fileName = "Quote@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                            fileName = Replace(fileName, "\", "").Replace("/", "")
                            bQuote.GenerateReport(quoteid, bytes, objResParam, "", fileName)
                            Response.Redirect("~/SavedReports/" & fileName)
                        End If



                    End If

                    Session("sobjResParam") = objResParam
                    'changed by mohamed on 26/10/2020
                    'fileName = "Quote@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    'Response.Clear()
                    'Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    'Response.AddHeader("Content-Length", bytes.Length.ToString())
                    'Response.ContentType = "application/pdf"
                    'Response.Buffer = True
                    'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    'Response.BinaryWrite(bytes)
                    'Response.Flush()
                    'HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf printId = "bookingConfirmation" Then
                    Dim bc As clsBookingConfirmationPdf = New clsBookingConfirmationPdf()
                    Dim requestid = Request.QueryString("RequestId")
                    Dim ds As New DataSet
                    bytes = {}
                    Dim objResParam As New ReservationParameters
                    objResParam = Session("sobjResParam")
                    fileName = requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    fileName = Replace(fileName, "\", "").Replace("/", "")
                    Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + requestid + "'")
                    If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                    If chkCumulative.Trim() = "CUMULATIVE" And Not requestid.Contains("RGV") And Not requestid.Contains("RPV") And Not requestid.Contains("RGT1.0") And Not requestid.Contains("RPT1.0") And Not requestid.Contains("RG1.0") And Not requestid.Contains("RP1.0") Then
                        'bc.GenerateCumulativeReport(requestid, bytes, ds, "download", objResParam)
                        bc.GenerateCumulativeReport(requestid, bytes, ds, "", objResParam, fileName)
                    Else
                        bc.GenerateReport(requestid, bytes, ds, "", objResParam, fileName)
                        'bc.GenerateReport(requestid, bytes, ds, "download", objResParam)
                    End If
                    Session("sobjResParam") = objResParam
                    'fileName = requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    'If Session("RedirectForPDF") = "1" Then
                    '    'Session.Clear()
                    '    'Response.Redirect(fileName)
                    'End If

                    'Response.Clear()

                    'Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
                    'Response.AddHeader("Content-Length", bytes.Length.ToString())
                    'Response.ContentType = "application/pdf"
                    'Response.Buffer = True
                    'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    'Response.BinaryWrite(bytes)
                    'Response.Flush()
                    ''If Session("RedirectForPDF") <> "1" Then
                    'HttpContext.Current.ApplicationInstance.CompleteRequest()
                    ''End If
                    'Dim strpop As String
                    'strpop = "window.open('SavedReports/" & fileName & "');"
                    'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), requestid & "popup" + "proformainvoice", strpop, True)
                    Response.Redirect("~/SavedReports/" & fileName)
                ElseIf printId = "hotelRequest" Then '' Added shahul 29/07/18
                    Dim bc As ClsBookingHotelPdf = New ClsBookingHotelPdf()
                    Dim requestid = Request.QueryString("requestid")
                    Dim partycode As String = Request.QueryString("partycode")
                    Dim amended As String = Request.QueryString("amended")
                    Dim cancelled As String = Request.QueryString("cancelled")
                    Dim ds As New DataSet
                    bytes = {}

                    bc.BookingHotelPrint(requestid, partycode, amended, cancelled, bytes, ds, "download", "")
                    fileName = requestid + "@" + partycode.Trim + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    Response.AddHeader("Content-Length", bytes.Length.ToString())
                    Response.ContentType = "application/pdf"
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(bytes)
                    Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf printId = "ServiceVoucher" Then '*** Service Voucher Danny 30/09/2018 
                    Dim requestid = Request.QueryString("RequestId")
                    '*** Service Voucher for RO Danny 30/09/2018
                    Dim SR_fileName As String = ""
                    If Session("sLoginType") = "RO" Then
                        Dim clsBookingService As New clsBookingServicerVoucher
                        SR_fileName = "SR_" + requestid.Replace("/", "") + ".PDF"
                        Dim bytes4 As Byte() = {}
                        Dim SRds1 As New DataSet
                        Dim SRResParam As ReservationParameters = Session("sobjResParam")
                        SR_fileName = clsBookingService.GenerateServiceReport_SR(requestid, bytes4, SRds1, "download", SRResParam, SR_fileName)
                        Response.Clear()

                        If SR_fileName.Trim().Length > 0 Then
                            SR_fileName = Server.MapPath("~\SavedReports\") + SR_fileName
                            Response.AddHeader("Content-Disposition", "inline; filename=" + SR_fileName)
                            Response.AddHeader("Content-Length", bytes4.Length.ToString())
                            Response.ContentType = "application/pdf"
                            Response.Buffer = True
                            Response.Cache.SetCacheability(HttpCacheability.NoCache)
                            Response.BinaryWrite(bytes4)

                        End If
                        Response.Flush()
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    End If
                ElseIf printId = "hotelVoucher" Then
                    Dim hVoucher As clsHotelVoucherPdf = New clsHotelVoucherPdf()
                    Dim requestid = Request.QueryString("RequestId")
                    Dim rlineNo As Integer = Request.QueryString("rlineNo")
                    bytes = {}
                    fileName = "HotelVoucher@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    fileName = Replace(fileName, "\", "").Replace("/", "")
                    hVoucher.GenerateHotelVoucher(requestid, rlineNo, bytes, "", fileName)
                    Response.Redirect("~/SavedReports/" & fileName)
                    'hVoucher.GenerateHotelVoucher(requestid, rlineNo, bytes, "download")
                    'fileName = "HotelVoucher@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    'Response.Clear()
                    'Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    'Response.AddHeader("Content-Length", bytes.Length.ToString())
                    'Response.ContentType = "application/pdf"
                    'Response.Buffer = True
                    'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    'Response.BinaryWrite(bytes)
                    'Response.Flush()
                    'HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf printId = "Itinerary" Then
                    Dim Itinerary As clsItineraryPdf = New clsItineraryPdf()
                    Dim requestid = Request.QueryString("RequestId")
                    Dim rlineNo = Request.QueryString("rlineNo")
                    bytes = {}
                    Dim objResParam As New ReservationParameters
                    Itinerary.GenerateItineraryReport(requestid, bytes, "download")
                    fileName = "Itinerary@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    Response.AddHeader("Content-Length", bytes.Length.ToString())
                    Response.ContentType = "application/pdf"
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(bytes)
                    Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf printId = "QuoteItinerary" Then
                    Dim QuoteItinerary As clsQuoteItineraryPdf = New clsQuoteItineraryPdf()
                    Dim quoteid = Request.QueryString("quoteId")
                    bytes = {}
                    Dim objResParam As New ReservationParameters
                    QuoteItinerary.GenerateQuoteItinerary(quoteid, bytes)
                    fileName = "QuoteItinerary@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    Response.AddHeader("Content-Length", bytes.Length.ToString())
                    Response.ContentType = "application/pdf"
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(bytes)
                    Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf printId = "QuoteCosting" Then
                    Dim QuoteItinerary As clsQuoteCosting = New clsQuoteCosting()
                    Dim quoteid = Request.QueryString("quoteId")
                    bytes = {}
                    Dim objResParam As New ReservationParameters
                    QuoteItinerary.GenerateCostingReport(quoteid, bytes)
                    fileName = "QuoteCosting@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    Response.AddHeader("Content-Length", bytes.Length.ToString())
                    Response.ContentType = "application/pdf"
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(bytes)
                    Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf printId = "QuoteCostingExcel" Then
                    Dim QuoteItinerary As clsQuoteCosting = New clsQuoteCosting()
                    Dim quoteid = Request.QueryString("quoteId")
                    bytes = {}

                    Dim FolderPath As String = "..\ExcelTemplates\"
                    fileName = "QuoteCosting.xlsx"
                    Dim FilePath As String = Server.MapPath("~\ExcelTemplates\") + fileName
                    Dim RandomCls As New Random()
                    Dim RandomNo As String = RandomCls.Next(100000, 9999999).ToString
                    objclsquotecosting.GenerateExcelReport(quoteid, FilePath)
                    Dim objResParam As New ReservationParameters
                    QuoteItinerary.GenerateCostingReport(quoteid, bytes)
                    '   fileName = "QuoteCosting@" + quoteid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
                    Response.AddHeader("Content-Length", bytes.Length.ToString())
                    Response.ContentType = "application/pdf"
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.BinaryWrite(bytes)
                    Response.Flush()
                    HttpContext.Current.ApplicationInstance.CompleteRequest()

                End If
            Catch ex As Exception
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
                objclsUtilities.WriteErrorLog("PrintPage.aspx :: GenerateReport :: " & ex.Message & ":: " & Session("GlobalUserName"))
            Finally
                If Session("RedirectForPDF") = "1" Then
                    Session.Clear()
                End If
            End Try
        Else
            Response.Redirect("Login.aspx", True)
        End If
    End Sub

End Class
