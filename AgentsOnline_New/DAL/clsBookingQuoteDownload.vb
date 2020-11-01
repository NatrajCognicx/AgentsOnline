Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Imports System.Diagnostics
Imports ClosedXML.Excel

Public Class clsBookingQuoteDownload
    Inherits System.Web.UI.Page
    Dim objclsUtilities As New clsUtilities
#Region "Global Variable"

    Dim NormalFont As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
    Dim NormalFontRed As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.RED)
    Dim NormalFontBoldRed As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.RED)
    Dim NormalFontSubTitle As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, New BaseColor(6, 39, 133))
    Dim NormalFontBoldTax As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
    Dim titleColor As BaseColor = New BaseColor(214, 214, 214)
#End Region
    Public Function GenerateReport_Download(ByVal quoteID As String, ByRef bytes() As Byte, ByVal objResParam As ReservationParameters, ByVal ExlWorkBook As XLWorkbook) As XLWorkbook
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As SqlCommand
            'Dim mySqlCmd1 As SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim myDataAdapter1 As New SqlDataAdapter
            Dim ds As New DataSet
            Dim ds1 As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")

            If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                mySqlCmd = New SqlCommand("sp_booking_quote_print_Whitelabel", sqlConn)
            Else
                mySqlCmd = New SqlCommand("sp_booking_quote_print", sqlConn)
                'mySqlCmd1 = New SqlCommand("sp_booking_adultchild_breakup_develop", sqlConn)
            End If
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteid", SqlDbType.VarChar, 20)).Value = quoteID
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)



            '*** Danny 22/09/2018 \


            Dim ExlSheet As IXLWorksheet = ExlWorkBook.Worksheet("Main")

            Dim ExlRowNo As Integer = 2
            Dim TemExlRowNo As Integer = 2
            'ExlSheet.Style.Font.FontName = "Arial"


            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim othServDt As DataTable = ds.Tables(3)
            Dim airportDt As DataTable = ds.Tables(4)
            Dim visaDt As DataTable = ds.Tables(5)
            Dim tourDt As DataTable = ds.Tables(6)
            Dim OtherDt As DataTable = ds.Tables(7)
            Dim contactDt As DataTable = ds.Tables(8)
            Dim splEventDt As DataTable = ds.Tables(9)
            clsDBConnect.dbConnectionClose(sqlConn)



            If headerDt.Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 35.0F)
                Dim documentWidth As Single = 550.0F
                Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 12, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
                Dim remainingPageSpace As Single
                Using memoryStream As New System.IO.MemoryStream()
                    Dim writer As PdfWriter
                    writer = PdfWriter.GetInstance(document, memoryStream)
                    Dim phrase As Phrase = Nothing
                    Dim cell As PdfPCell = Nothing
                    Dim table As PdfPTable = Nothing
                    document.Open()
                    'Header Table
                    Dim headerDr As DataRow = headerDt.Rows(0)
                    table = New PdfPTable(2)
                    table.TotalWidth = documentWidth
                    table.LockedWidth = True
                    table.SetWidths(New Single() {0.5F, 0.5F})
                    table.Complete = False
                    table.SplitRows = False
                    Dim tblLogo As PdfPTable = New PdfPTable(2)
                    tblLogo.SetWidths(New Single() {0.27F, 0.73F})
                    'Company Name 
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("division_master_des")), TitleFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 2
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    tblLogo.AddCell(cell)



                    Dim H1 As IXLRange
                    H1 = ExlSheet.Range("B2:N2")
                    H1.Style.Font.Bold = True
                    H1.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    H1.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)


                    Dim H2 As IXLRange
                    H2 = ExlSheet.Range("B2:N7")
                    H2.Style.Border.OutsideBorder = XLBorderStyleValues.Medium
                    H2.Style.Alignment.WrapText = True
                    H2.Style.Alignment.JustifyLastLine = True



                    ExlSheet.Cell(ExlRowNo, 2).Value = headerDr("division_master_des")
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Border.SetBottomBorder(XLBorderStyleValues.Thin)

                    ExlSheet.Cell(ExlRowNo, 9).Value = headerDr("agentName")
                    ExlSheet.Range("I" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetBottomBorder(XLBorderStyleValues.Thin)
                    ExlSheet.Cell(ExlRowNo, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin
                    'agentmast0 = ExlSheet.Cell(1, 1)
                    'agentmast0.Style.Border.OutsideBorder = XLBorderStyleValues.Medium
                    'agentmast0.Style.Border.InsideBorder = XLBorderStyleValues.Thin
                    'ExlSheet.Column("A1").Width = 50


                    'Company Logo



                    If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                        Dim logoName As String = objclsUtilities.ExecuteQueryReturnStringValue("select logofilename from agentmast_whitelabel where agentcode ='" + objResParam.AgentCode.Trim() + "'")
                        cell = ImageCell("~/Logos/" + logoName, 60.0F, PdfPCell.ALIGN_CENTER)
                    Else
                        If (headerDr("div_code") = "01") Then
                            cell = ImageCell("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_CENTER)
                        Else
                            cell = ImageCell("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_LEFT)
                        End If
                    End If
                    tblLogo.AddCell(cell)
                    'Company Address
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("address1")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("    Fax : " & Convert.ToString(headerDr("fax")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("    Tel : " & Convert.ToString(headerDr("tel")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("    E-mail : " & Convert.ToString(headerDr("email")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("    Website : " & Convert.ToString(headerDr("website")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    tblLogo.AddCell(cell)
                    table.AddCell(tblLogo)

                    ExlRowNo = ExlRowNo + 1

                    TemExlRowNo = ExlRowNo
                    ExlSheet.Cell(ExlRowNo, 4).Value = Convert.ToString(headerDr("address1"))
                    ExlSheet.Cell(ExlRowNo, 9).Value = Convert.ToString(headerDr("agentAddress"))

                    'ExlSheet.Range("B" + ExlRowNo.ToString + ":C" + (ExlRowNo + 5).ToString).Merge()
                    ExlSheet.Range("D" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                    ExlSheet.Range("I" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell(ExlRowNo, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    ExlRowNo = ExlRowNo + 1
                    ExlSheet.Cell(ExlRowNo, 4).Value = "    fax : " & Convert.ToString(headerDr("fax"))
                    ExlSheet.Cell(ExlRowNo, 9).Value = "    fax : " & Convert.ToString(headerDr("agentFax"))

                    ExlSheet.Range("D" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                    ExlSheet.Range("I" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell(ExlRowNo, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    ExlRowNo = ExlRowNo + 1
                    ExlSheet.Cell(ExlRowNo, 4).Value = "    tel : " & Convert.ToString(headerDr("tel"))
                    ExlSheet.Cell(ExlRowNo, 9).Value = "    tel : " & Convert.ToString(headerDr("agentTel"))

                    ExlSheet.Range("D" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                    ExlSheet.Range("I" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell(ExlRowNo, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    ExlRowNo = ExlRowNo + 1
                    ExlSheet.Cell(ExlRowNo, 4).Value = "    email : " & Convert.ToString(headerDr("email"))
                    ExlSheet.Cell(ExlRowNo, 9).Value = "    email : " & Convert.ToString(headerDr("agentEmail"))

                    ExlSheet.Range("D" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                    ExlSheet.Range("I" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell(ExlRowNo, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    ExlRowNo = ExlRowNo + 1
                    ExlSheet.Cell(ExlRowNo, 4).Value = "    Website : " & Convert.ToString(headerDr("website")) & vbLf
                    ExlSheet.Cell(ExlRowNo, 9).Value = "    Attn : " & Convert.ToString(headerDr("agentContact")) & vbLf

                    ExlSheet.Range("D" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                    ExlSheet.Range("I" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell(ExlRowNo, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin

                    ExlSheet.Range("B" + TemExlRowNo.ToString + ":C" + (ExlRowNo).ToString).Merge()

                    Dim tblClient As PdfPTable = New PdfPTable(2)
                    tblClient.SetWidths(New Single() {0.5F, 0.5F})
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("agentName")), TitleFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    cell.SetLeading(11, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("agentAddress")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Tel : " & Convert.ToString(headerDr("agentTel")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Fax : " & Convert.ToString(headerDr("agentFax")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Email : " & Convert.ToString(headerDr("agentEmail")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Attn. : " & Convert.ToString(headerDr("agentContact")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    table.AddCell(tblClient)
                    table.Complete = True
                    document.Add(table)

                    Dim tblTitle As PdfPTable = New PdfPTable(1)
                    tblTitle.SetWidths(New Single() {1.0F})
                    tblTitle.TotalWidth = documentWidth
                    tblTitle.LockedWidth = True
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("printHeader")), TitleFontBigBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = titleColor
                    tblTitle.AddCell(cell)
                    tblTitle.SpacingBefore = 7
                    document.Add(tblTitle)

                    ExlRowNo = ExlRowNo + 2
                    Dim H3 As IXLRange
                    H3 = ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString)
                    H3.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    H3.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    H3.Style.Font.Bold = True
                    H3.Style.Font.FontSize = 16
                    H3.Style.Fill.BackgroundColor = XLColor.LightGray
                    H3.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin)
                    H3.Merge()
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + (ExlRowNo + 1).ToString).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium)

                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("printHeader"))
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":C" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right)
                    ExlSheet.Range("D" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left)
                    ExlSheet.Range("G" + ExlRowNo.ToString + ":G" + ExlRowNo.ToString).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
                    ExlSheet.Range("H" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left)
                    ExlSheet.Range("K" + ExlRowNo.ToString + ":K" + ExlRowNo.ToString).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
                    ExlSheet.Range("L" + ExlRowNo.ToString + ":M" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left)
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Quote No: "
                    ExlSheet.Cell("G" + ExlRowNo.ToString).Value = "Dated: "
                    ExlSheet.Cell("K" + ExlRowNo.ToString).Value = "Your Ref: "
                    ExlSheet.Cell("D" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("requestID"))
                    ExlSheet.Cell("H" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("requestDate"))
                    ExlSheet.Cell("L" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("agentRef"))


                    Dim tblInv As PdfPTable = New PdfPTable(6)
                    tblInv.SetWidths(New Single() {0.12F, 0.14F, 0.12F, 0.14F, 0.12F, 0.25F})
                    tblInv.TotalWidth = documentWidth
                    tblInv.LockedWidth = True
                    tblInv.SplitRows = False
                    Dim arrTitle() As String = {"Quote No : ", headerDr("requestID").ToString(), "Dated : ", headerDr("requestDate"), "Your Ref : ", headerDr("agentRef")}
                    For i = 0 To 5
                        phrase = New Phrase()
                        phrase.Add(New Chunk(arrTitle(i), NormalFontBold))
                        cell = New PdfPCell(phrase)
                        If i = 0 Then
                            cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.TOP_BORDER
                        ElseIf i = 5 Then
                            cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.TOP_BORDER
                        Else
                            cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.TOP_BORDER
                        End If
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        If i Mod 2 = 0 Then
                            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        Else
                            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        End If
                        cell.Padding = 3
                        tblInv.AddCell(cell)
                    Next
                    document.Add(tblInv)
                    Dim printMode As String = "download"
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblInv, printMode)

                    If hotelDt.Rows.Count > 0 Then
                        ExlRowNo = ExlRowNo + 2
                        TemExlRowNo = ExlRowNo
                        Dim H4 As IXLRange
                        H4 = ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString)
                        H4.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        H4.Style.Font.Bold = True
                        H4.Style.Font.FontSize = 16
                        H4.Style.Fill.BackgroundColor = XLColor.LightGray
                        H4.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin)
                        ExlSheet.Rows(ExlRowNo).Height = 30

                        ExlSheet.Range("B" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                        ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                        ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

                        ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Hotel Services"
                        ExlSheet.Cell("I" + ExlRowNo.ToString).Value = "Chk. in"
                        ExlSheet.Cell("K" + ExlRowNo.ToString).Value = "Chk. Out"
                        ExlSheet.Cell("M" + ExlRowNo.ToString).Value = "Charges"
                        ExlRowNo = ExlRowNo + 1


                        Dim arrServ() As String = {"Hotel Services", "Chk. in", "Chk. Out", "Charges " & Convert.ToString(headerDr("currCode"))}
                        Dim tblServ As PdfPTable = New PdfPTable(4)
                        tblServ.TotalWidth = documentWidth
                        tblServ.LockedWidth = True
                        tblServ.SetWidths(New Single() {0.63F, 0.12F, 0.12F, 0.13F})
                        tblServ.Complete = False
                        tblServ.HeaderRows = 1
                        tblServ.SplitRows = False
                        For i = 0 To 3
                            phrase = New Phrase()
                            phrase.Add(New Chunk(arrServ(i), NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 4.0F
                            cell.PaddingTop = 1.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                        Next
                        For Each hotelDr As DataRow In hotelDt.Rows

                            'ExlSheet.Range("B" + ExlRowNo.ToString + ":C" + ExlRowNo.ToString).Merge()
                            'ExlSheet.Range("D" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge()
                            'ExlSheet.Range("H" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge()
                            'ExlSheet.Range("L" + ExlRowNo.ToString + ":M" + ExlRowNo.ToString).Merge()
                            'ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Quote No: "
                            'ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Dated: "
                            'ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Your Ref: "
                            ExlSheet.Range("B" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Font.Bold = True

                            ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)


                            ExlSheet.Cell("B" + ExlRowNo.ToString).Value = Convert.ToString(hotelDr("partyName"))

                            ExlRowNo = ExlRowNo + 1
                            ExlSheet.Range("B" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                            ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "    " + Convert.ToString(hotelDr("RoomDetail"))

                            ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

                            ExlRowNo = ExlRowNo + 1

                            ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

                            'ExlSheet.Cell("L" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("agentRef")) + Convert.ToString(headerDr("currCode"))




                            Dim tblTariff As PdfPTable = New PdfPTable(2)
                            tblTariff.SetWidths(New Single() {0.05F, 0.95F})
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("partyName")) & vbLf, NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingBottom = 3.0F
                            cell.Colspan = 2
                            tblTariff.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            cell.PaddingBottom = 3.0F
                            tblTariff.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("RoomDetail")) & vbLf, NormalFont))
                            If Convert.ToString(hotelDr("occupancy")) <> "" Then
                                phrase.Add(New Chunk("[ " & Convert.ToString(hotelDr("occupancy")) & " ]", NormalFont))
                                ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "    [" + Convert.ToString(hotelDr("occupancy")) + "]"
                            End If

                            Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                            Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
                            Dim tariffFilter = (From n In tariffDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n Order By Convert.ToDateTime(n.Field(Of String)("fromDate")) Ascending).ToList()
                            Dim filterTariffDt As New DataTable
                            If (tariffFilter.Count > 0) Then filterTariffDt = tariffFilter.CopyToDataTable()
                            If filterTariffDt.Rows.Count > 0 Then

                                ExlSheet.Cell("I" + ExlRowNo.ToString).Value = Convert.ToString(hotelDr("checkIn"))

                                ExlSheet.Cell("K" + ExlRowNo.ToString).Value = Convert.ToString(hotelDr("checkOut"))

                                ExlSheet.Cell("M" + ExlRowNo.ToString).Value = Convert.ToString(hotelDr("salevalue"))
                                ExlRowNo = ExlRowNo + 1
                                For Each ratesDr As DataRow In filterTariffDt.Rows
                                    phrase.Add(New Chunk(vbCrLf + "From " + Convert.ToString(ratesDr("fromDate")) & " " & Convert.ToString(ratesDr("nights")) & " Nts * " & Convert.ToString(ratesDr("salePrice")) & " = ", NormalFont))

                                    ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                                    ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                                    ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

                                    ExlSheet.Range("B" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge()
                                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "    From " + Convert.ToString(ratesDr("fromDate")) & " " & Convert.ToString(ratesDr("nights")) & " Nts * " & Convert.ToString(ratesDr("salePrice")) & " = " + "    " + Convert.ToString(ratesDr("saleValue")) & " " & Convert.ToString(headerDr("currCode"))
                                    ExlRowNo = ExlRowNo + 1
                                    phrase.Add(New Chunk(Convert.ToString(ratesDr("saleValue")) & " " & Convert.ToString(headerDr("currCode")), NormalFontBold))

                                    If ratesDr("bookingCode") <> "" Then
                                        phrase.Add(New Chunk(vbCrLf & "( " + Convert.ToString(ratesDr("bookingCode")) + " )", NormalFontBold))

                                        ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                                        ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                                        ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)


                                        ExlSheet.Range("B" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Font.Bold = True
                                        ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "    ( " + Convert.ToString(ratesDr("bookingCode")) + " )"
                                        ExlRowNo = ExlRowNo + 1
                                    End If
                                Next
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingBottom = 3.0F
                            tblTariff.AddCell(cell)
                            tblServ.AddCell(tblTariff)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("checkIn")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 3.0F
                            tblServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("checkOut")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 2.0F
                            tblServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("salevalue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 2.0F
                            cell.PaddingRight = 4.0F
                            tblServ.AddCell(cell)

                            If splEventDt.Rows.Count > 0 Then
                                Dim partyCode As String = hotelDr("partyCode").ToString()
                                Dim index As Integer = hotelDt.Rows.IndexOf(hotelDr)
                                Dim i As Integer = 0
                                Dim lastIndex As Integer = index
                                Dim filterRows = (From n In splEventDt.AsEnumerable() Where n.Field(Of String)("PartyCode") = partyCode)
                                If filterRows.Count > 0 Then
                                    Dim filterHotelRows = (From n In hotelDt.AsEnumerable() Where n.Field(Of String)("PartyCode") = partyCode)
                                    While i < filterHotelRows.Count
                                        If hotelDt.Rows.IndexOf(filterHotelRows(i)) > index Then
                                            lastIndex = hotelDt.Rows.IndexOf(filterHotelRows(i))
                                            Exit While
                                        End If
                                        i = i + 1
                                    End While
                                End If
                                If index = lastIndex Then
                                    Dim filterSplEvt As New DataTable
                                    If (filterRows.Count > 0) Then filterSplEvt = filterRows.CopyToDataTable()
                                    If filterSplEvt.Rows.Count > 0 Then
                                        Dim tblSplEvent As New PdfPTable(6)
                                        Dim currCode As String = Convert.ToString(headerDr("currCode"))
                                        SpecialEvents(filterSplEvt, documentWidth, currCode, tblSplEvent, ExlSheet, ExlRowNo)
                                        cell = New PdfPCell(tblSplEvent)
                                        cell.Colspan = 4
                                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                                        tblServ.AddCell(cell)
                                    End If
                                End If
                            End If
                        Next
                        tblServ.Complete = True
                        tblServ.SpacingBefore = 7
                        document.Add(tblServ)
                        ExlSheet.Range("B" + TemExlRowNo.ToString + ":N" + (ExlRowNo - 1).ToString).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                    End If


                    If othServDt.Rows.Count > 0 Or visaDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                        ExlRowNo = ExlRowNo + 1

                        TemExlRowNo = ExlRowNo
                        Dim H5 As IXLRange
                        H5 = ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString)
                        H5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        H5.Style.Font.Bold = True
                        H5.Style.Font.FontSize = 16
                        H5.Style.Fill.BackgroundColor = XLColor.LightGray
                        H5.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin)

                        ExlSheet.Range("B" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        ExlSheet.Range("G" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                        ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                        ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
                        ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

                        ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Other Services"
                        ExlSheet.Cell("G" + ExlRowNo.ToString).Value = "Date of Service"
                        ExlSheet.Cell("I" + ExlRowNo.ToString).Value = "Units/ Pax"
                        ExlSheet.Cell("K" + ExlRowNo.ToString).Value = "Rate per Units/Pax"
                        ExlSheet.Cell("M" + ExlRowNo.ToString).Value = "Charges " & Convert.ToString(headerDr("currCode"))
                        ExlRowNo = ExlRowNo + 1

                        Dim OthServ() As String = {"Other Services", "Date of Service", "Units/ Pax", "Rate per Units/Pax", "Charges " & Convert.ToString(headerDr("currCode"))}
                        Dim tblOthServ As PdfPTable = New PdfPTable(5)
                        tblOthServ.TotalWidth = documentWidth
                        tblOthServ.LockedWidth = True
                        tblOthServ.SetWidths(New Single() {0.57F, 0.12F, 0.09F, 0.11F, 0.13F})
                        tblOthServ.SplitRows = False
                        tblOthServ.Complete = False
                        tblOthServ.HeaderRows = 1
                        For i = 0 To 4
                            phrase = New Phrase()
                            phrase.Add(New Chunk(OthServ(i), NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 4.0F
                            cell.PaddingTop = 1.0F
                            cell.BackgroundColor = titleColor
                            tblOthServ.AddCell(cell)
                        Next

                        Dim MergeDt As DataTable = New DataTable()
                        Dim OthServType As DataColumn = New DataColumn("OthServType", GetType(String))
                        Dim ServiceName As DataColumn = New DataColumn("ServiceName", GetType(String))
                        Dim ServiceDate As DataColumn = New DataColumn("ServiceDate", GetType(String))
                        Dim Unit As DataColumn = New DataColumn("Unit", GetType(String))
                        Dim UnitPrice As DataColumn = New DataColumn("UnitPrice", GetType(Decimal))
                        Dim UnitSaleValue As DataColumn = New DataColumn("UnitSaleValue", GetType(Decimal))
                        Dim Adults As DataColumn = New DataColumn("Adults", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim Child As DataColumn = New DataColumn("Child", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim Senior As DataColumn = New DataColumn("Senior", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim PickUpDropOff As DataColumn = New DataColumn("PickUpDropOff", GetType(String)) With {.DefaultValue = DBNull.Value}
                        Dim Sic As DataColumn = New DataColumn("Sic", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        MergeDt.Columns.Add(OthServType)
                        MergeDt.Columns.Add(ServiceName)
                        MergeDt.Columns.Add(ServiceDate)
                        MergeDt.Columns.Add(Unit)
                        MergeDt.Columns.Add(UnitPrice)
                        MergeDt.Columns.Add(UnitSaleValue)
                        MergeDt.Columns.Add(Adults)
                        MergeDt.Columns.Add(Child)
                        MergeDt.Columns.Add(Senior)
                        MergeDt.Columns.Add(PickUpDropOff)
                        MergeDt.Columns.Add(Sic)

                        For Each othServDr As DataRow In othServDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Transfer"
                            MergeDr("ServiceName") = othServDr("transferName")
                            MergeDr("ServiceDate") = othServDr("transferDate")
                            MergeDr("Unit") = othServDr("units")
                            MergeDr("UnitPrice") = othServDr("unitPrice")
                            MergeDr("UnitSaleValue") = othServDr("unitSaleValue")
                            MergeDr("Adults") = othServDr("Adults")
                            MergeDr("Child") = othServDr("Child")
                            MergeDr("PickUpDropOff") = othServDr("PickUpDropOff")
                            MergeDr("Sic") = othServDr("Sic")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each VisaDr As DataRow In visaDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Visa"
                            MergeDr("ServiceName") = VisaDr("visaName")
                            MergeDr("ServiceDate") = VisaDr("VisaDate")
                            MergeDr("Unit") = VisaDr("noOfvisas")
                            MergeDr("UnitPrice") = VisaDr("visaPrice")
                            MergeDr("UnitSaleValue") = VisaDr("visaValue")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each airportDr As DataRow In airportDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Airport"
                            MergeDr("ServiceName") = airportDr("airportmaname")
                            MergeDr("ServiceDate") = airportDr("airportmadate")
                            MergeDr("Unit") = airportDr("units")
                            MergeDr("UnitPrice") = airportDr("unitPrice")
                            MergeDr("UnitSaleValue") = airportDr("unitSaleValue")
                            MergeDr("Adults") = airportDr("Adults")
                            MergeDr("Child") = airportDr("Child")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each tourDr As DataRow In tourDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Tour"
                            MergeDr("ServiceName") = tourDr("tourname")
                            MergeDr("ServiceDate") = tourDr("tourdate")
                            MergeDr("Unit") = tourDr("units")
                            MergeDr("UnitPrice") = tourDr("unitPrice")
                            MergeDr("UnitSaleValue") = tourDr("unitSaleValue")
                            MergeDr("Adults") = tourDr("Adults")
                            MergeDr("Child") = tourDr("Child")
                            MergeDr("Senior") = tourDr("Senior")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each otherDr As DataRow In OtherDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Other"
                            MergeDr("ServiceName") = otherDr("othername")
                            MergeDr("ServiceDate") = otherDr("othdate")
                            MergeDr("Unit") = otherDr("units")
                            MergeDr("UnitPrice") = otherDr("unitPrice")
                            MergeDr("UnitSaleValue") = otherDr("unitSaleValue")
                            MergeDr("Adults") = otherDr("Adults")
                            MergeDr("Child") = otherDr("Child")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        If MergeDt.Rows.Count > 0 Then
                            Dim MergeOrderDt As DataTable = (From n In MergeDt.AsEnumerable() Select n Order By Convert.ToDateTime(n.Field(Of String)("ServiceDate")) Ascending).CopyToDataTable()
                            AppendOtherServices(tblOthServ, MergeOrderDt, ExlSheet, ExlRowNo)
                        End If
                        tblOthServ.Complete = True
                        tblOthServ.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblOthServ)
                        ExlSheet.Range("B" + TemExlRowNo.ToString + ":N" + (ExlRowNo - 1).ToString).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                    End If

                    Dim tblTotal As PdfPTable = New PdfPTable(2)
                    tblTotal.TotalWidth = documentWidth
                    tblTotal.LockedWidth = True
                    tblTotal.SetWidths(New Single() {0.8F, 0.2F})
                    tblTotal.KeepTogether = True

                    ExlRowNo = ExlRowNo + 1
                    'Dim H6 As IXLRange
                    'H6 = ExlSheet.Range("B" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString)
                    'H6.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    'H6.Style.Font.Bold = True
                    'H6.Style.Font.FontSize = 16
                    'H6.Style.Fill.BackgroundColor = XLColor.LightGray
                    TemExlRowNo = ExlRowNo
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    ExlSheet.Range("K" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Total Amount (" + Convert.ToString(headerDr("currcode")) + ")"
                    ExlSheet.Cell("K" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("saleCurrency"))
                    ExlRowNo = ExlRowNo + 1


                    phrase = New Phrase()
                    phrase.Add(New Chunk("Total Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 10.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("saleCurrency")), NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    ExlSheet.Range("K" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)


                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Total Amount (" + Convert.ToString(headerDr("baseCurrcode")) + ")"
                    ExlSheet.Cell("K" + ExlRowNo.ToString).Value = Convert.ToString(headerDr("saleValue"))
                    ExlRowNo = ExlRowNo + 1

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Total Amount (" + Convert.ToString(headerDr("baseCurrcode")) + ")", NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 10.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)


                    ExlSheet.Range("B" + TemExlRowNo.ToString + ":N" + (ExlRowNo - 1).ToString).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium).Font.Bold = True

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("saleValue")), NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

                    tblTotal.SpacingBefore = 7
                    document.Add(tblTotal)




                    tblTotal.SpacingBefore = 27


                    '------- Tax Note ----------------
                    Dim tblTax As PdfPTable = New PdfPTable(2)
                    tblTax.TotalWidth = documentWidth
                    tblTax.LockedWidth = True
                    tblTax.SetWidths(New Single() {0.03, 0.97F})
                    tblTax.KeepTogether = True
                    tblTax.Complete = False
                    tblTax.SplitRows = False

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Chr(149), NormalFontBoldTax))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER
                    cell.PaddingLeft = 7.0F
                    cell.PaddingBottom = 3.0F
                    cell.PaddingTop = 3.0F
                    cell.BackgroundColor = BaseColor.YELLOW
                    tblTax.AddCell(cell)

                    ExlRowNo = ExlRowNo + 1
                    TemExlRowNo = ExlRowNo
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = Chr(149) + "ABOVE RATES ARE INCLUSIVE OF ALL TAXES INCLUDING VAT"
                    ExlRowNo = ExlRowNo + 1
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = Chr(149) + "ABOVE RATES DOES NOT INCLUDE TOURISM DIRHAM FEE WHICH IS TO BE PAID BY THE GUEST DIRECTLY AT THE HOTEL"

                    ExlSheet.Range("B" + TemExlRowNo.ToString + ":N" + (ExlRowNo).ToString).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium).Fill.BackgroundColor = XLColor.Yellow

                    ExlRowNo = ExlRowNo + 1

                    TemExlRowNo = ExlRowNo
                    phrase = New Phrase()
                    phrase.Add(New Chunk("ABOVE RATES ARE INCLUSIVE OF ALL TAXES INCLUDING VAT", NormalFontBoldTax))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    cell.PaddingLeft = 2.0F
                    cell.PaddingBottom = 3.0F
                    cell.PaddingTop = 3.0F
                    cell.BackgroundColor = BaseColor.YELLOW
                    tblTax.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Chr(149), NormalFontBoldTax))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                    cell.PaddingLeft = 7.0F
                    cell.PaddingBottom = 5.0F
                    cell.BackgroundColor = BaseColor.YELLOW
                    tblTax.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("ABOVE RATES DOES NOT INCLUDE TOURISM DIRHAM FEE WHICH IS TO BE PAID BY THE GUEST DIRECTLY AT THE HOTEL", NormalFontBoldTax))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                    cell.PaddingLeft = 2.0F
                    cell.PaddingBottom = 5.0F
                    cell.BackgroundColor = BaseColor.YELLOW
                    tblTax.AddCell(cell)

                    tblTax.Complete = True
                    tblTax.SpacingBefore = 7
                    document.Add(tblTax)

                    '------------ Note -----------
                    Dim tblNote As PdfPTable = New PdfPTable(1)
                    tblNote.TotalWidth = documentWidth
                    tblNote.LockedWidth = True
                    tblNote.SetWidths(New Single() {1.0F})
                    tblNote.Complete = False
                    tblNote.SplitRows = False

                    ExlRowNo = ExlRowNo + 1
                    TemExlRowNo = ExlRowNo
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Font.Bold = True
                    'ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Style.Font.Underline = True
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Note :"
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Style.Font.FontColor = XLColor.Red
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "   " + Chr(149) + " Tourism Dirham Tax will be charged directly to the clients upon arrival at the hotel."
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Style.Font.FontColor = XLColor.Red
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "   " + Chr(149) + " All the rates quoted are net and non – commissionable."
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Style.Font.FontColor = XLColor.Red
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "   " + Chr(149) + " The above is only an offer and rooms / rates will be subject to availability at the time of booking."
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Style.Font.FontColor = XLColor.Red
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "   " + Chr(149) + " Any amendments in the dates of travel or number of passengers will attract a re-quote."
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Style.Font.FontColor = XLColor.Red
                    ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge()
                    ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "   " + Chr(149) + String.Format(" Check in time at the hotel is after 14:00 hrs and check out is before 12:00 hrs all other requests are{0}{1}subject to availability.", {vbCrLf, Space(2)})
                    ExlRowNo = ExlRowNo + 1

                    ExlSheet.Range("B" + TemExlRowNo.ToString + ":N" + (ExlRowNo - 1).ToString).Style.Border.SetOutsideBorder(XLBorderStyleValues.Medium)

                    ExlSheet.Protect(quoteID)


                    phrase = New Phrase()
                    phrase.Add(New Chunk("Note :" & vbCrLf, TitleFontBoldUnderLine))
                    phrase.Add(New Chunk(Chr(149) & " Tourism Dirham Tax will be charged directly to the clients upon arrival at the hotel." & vbCrLf, NormalFontRed))
                    phrase.Add(New Chunk(Chr(149) & " All the rates quoted are net and non – commissionable." & vbCrLf, NormalFontRed))
                    phrase.Add(New Chunk(Chr(149) & " The above is only an offer and rooms / rates will be subject to availability at the time of booking." & vbCrLf, NormalFontRed))
                    phrase.Add(New Chunk(Chr(149) & " Any amendments in the dates of travel or number of passengers will attract a re-quote." & vbCrLf, NormalFontRed))
                    phrase.Add(New Chunk(Chr(149) & String.Format(" Check in time at the hotel is after 14:00 hrs and check out is before 12:00 hrs all other requests are{0}{1}subject to availability.", {vbCrLf, Space(2)}), NormalFontRed))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(15, 0)
                    cell.PaddingLeft = 10.0F
                    cell.PaddingBottom = 5.0F
                    tblNote.AddCell(cell)
                    tblNote.Complete = True
                    tblNote.SpacingBefore = 7
                    document.Add(tblNote)




                    document.AddTitle(Convert.ToString(headerDr("printHeader")) & " - " & quoteID)
                    document.Close()
                    Dim pagingFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.GRAY)
                    Dim reader As New PdfReader(memoryStream.ToArray())
                    Using mStream As New MemoryStream()
                        Using stamper As New PdfStamper(reader, mStream)
                            Dim pages As Integer = reader.NumberOfPages
                            For i As Integer = 1 To pages
                                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, New Phrase("Page " + i.ToString() + " of " + pages.ToString(), pagingFont), 300.0F, 20.0F, 0)
                            Next
                        End Using
                        bytes = mStream.ToArray()
                    End Using
                End Using

            Else
                Throw New Exception("There is no rows in header table")
            End If
            Return ExlWorkBook
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#Region "Protected Sub AppendOtherServices(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)"
    Protected Function AppendOtherServices(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable, ByVal ExlSheet As IXLWorksheet, ByRef ExlRowNo As Integer) As IXLWorksheet

        'ExlSheet.Range("B" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        'ExlSheet.Range("G" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        'ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        'ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        'ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)

        'ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Other Services"
        'ExlSheet.Cell("G" + ExlRowNo.ToString).Value = "Date of Service"
        'ExlSheet.Cell("I" + ExlRowNo.ToString).Value = "Units/ Pax"
        'ExlSheet.Cell("K" + ExlRowNo.ToString).Value = "Rate per Units/Pax"
        'ExlSheet.Cell("M" + ExlRowNo.ToString).Value = "Charges " & Convert.ToString(headerDr("currCode"))


        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        For Each inputDr As DataRow In inputDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("ServiceName")), NormalFont))

            ExlSheet.Range("B" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge()
            ExlSheet.Cell("B" + ExlRowNo.ToString).Value = Convert.ToString(inputDr("ServiceName"))

            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
            Else
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            End If
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingBottom = 3.0F
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("ServiceDate")), NormalFont))

            ExlSheet.Range("G" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("G" + ExlRowNo.ToString).Value = Convert.ToString(inputDr("ServiceDate"))

            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("Unit")), NormalFont))

            ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("I" + ExlRowNo.ToString).Value = Convert.ToString(inputDr("Unit"))

            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("UnitPrice")), NormalFont))

            ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("K" + ExlRowNo.ToString).Value = Convert.ToString(inputDr("UnitPrice"))

            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            cell.PaddingRight = 4.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("UnitSaleValue")), NormalFont))

            ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("M" + ExlRowNo.ToString).Value = Convert.ToString(inputDr("UnitPrice"))

            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            cell.PaddingRight = 4.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                phrase = New Phrase()
                phrase.Add(New Chunk(Convert.ToString(inputDr("pickupdropoff")), NormalFont))



                If Convert.ToInt32(inputDr("sic")) <> 1 Then
                    phrase.Add(New Chunk(" (" & inputDr("adults").ToString() & " Adults", NormalFont))
                    If String.IsNullOrEmpty(Convert.ToString(inputDr("child")).Trim()) Or Convert.ToString(inputDr("child")).Trim() = "0" Then
                        phrase.Add(New Chunk(")", NormalFont))
                    Else
                        phrase.Add(New Chunk(", " & inputDr("child").ToString() & " Child)", NormalFont))
                    End If
                End If
                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 3.0F
                tblOthServ.AddCell(cell)
                ExlRowNo = ExlRowNo + 1
                ExlSheet.Range("B" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge()
                ExlSheet.Cell("B" + ExlRowNo.ToString).Value = phrase.ToString()


            End If
            ExlRowNo = ExlRowNo + 1
        Next
        Return ExlSheet
    End Function
#End Region
#Region "Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef tblSplEvent As PdfPTable)"
    Protected Function SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef tblSplEvent As PdfPTable, ByVal ExlSheet As IXLWorksheet, ByRef ExlRowNo As Integer) As IXLWorksheet
        Dim phrase As New Phrase
        Dim cell As New PdfPCell
        Dim splEventTitleColor As BaseColor = New BaseColor(203, 235, 249)   '-255, 219, 212
        Dim arrSplEvent() As String = {"Special Events", "Date of Event", "Units/ Pax", "Type of Units/Pax", "Rate per Units/Pax", "Charges " & CurrCode}
        tblSplEvent.TotalWidth = documentWidth
        tblSplEvent.LockedWidth = True
        tblSplEvent.SetWidths(New Single() {0.42F, 0.12F, 0.09F, 0.12F, 0.12F, 0.13F})
        tblSplEvent.Complete = False
        tblSplEvent.HeaderRows = 1
        tblSplEvent.SplitRows = False
        For i = 0 To 5
            phrase = New Phrase()
            phrase.Add(New Chunk(arrSplEvent(i), NormalFontBold))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 4.0F
            cell.PaddingTop = 1.0F
            cell.BackgroundColor = splEventTitleColor
            tblSplEvent.AddCell(cell)
        Next
        Dim H5 As IXLRange
        H5 = ExlSheet.Range("B" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString)
        H5.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        H5.Style.Font.Bold = True
        H5.Style.Font.FontSize = 12
        H5.Style.Fill.BackgroundColor = XLColor.PowderBlue
        H5.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin).Border.SetTopBorder(XLBorderStyleValues.Thin)
        ExlSheet.Rows(ExlRowNo).Height = 30

        ExlSheet.Range("B" + ExlRowNo.ToString + ":D" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        ExlSheet.Range("E" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
        ExlSheet.Range("G" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
        ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
        ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)
        ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center).Border.SetLeftBorder(XLBorderStyleValues.Thin)

        ExlSheet.Cell("B" + ExlRowNo.ToString).Value = "Special Events"
        ExlSheet.Cell("E" + ExlRowNo.ToString).Value = "Date of Event"
        ExlSheet.Cell("G" + ExlRowNo.ToString).Value = "Units/ Pax"
        ExlSheet.Cell("I" + ExlRowNo.ToString).Value = "Type of Units/Pax"
        ExlSheet.Cell("K" + ExlRowNo.ToString).Value = "Rate per Units/Pax"
        ExlSheet.Cell("M" + ExlRowNo.ToString).Value = "Charges " & CurrCode
        ExlRowNo = ExlRowNo + 1

        For Each splEventDr As DataRow In splEventDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventName")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingLeft = 3.0F
            cell.PaddingBottom = 4.0F
            tblSplEvent.AddCell(cell)

            ExlSheet.Range("B" + ExlRowNo.ToString + ":D" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("B" + ExlRowNo.ToString).Value = Convert.ToString(splEventDr("splEventName"))


            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventDate")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            ExlSheet.Range("E" + ExlRowNo.ToString + ":F" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("E" + ExlRowNo.ToString).Value = Convert.ToString(splEventDr("splEventDate"))


            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("noOfPax")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            ExlSheet.Range("G" + ExlRowNo.ToString + ":H" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("G" + ExlRowNo.ToString).Value = Convert.ToString(splEventDr("noOfPax"))

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("paxType")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            ExlSheet.Range("I" + ExlRowNo.ToString + ":J" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("I" + ExlRowNo.ToString).Value = Convert.ToString(splEventDr("paxType"))

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("paxRate")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            cell.PaddingRight = 4.0F
            tblSplEvent.AddCell(cell)

            ExlSheet.Range("K" + ExlRowNo.ToString + ":L" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("K" + ExlRowNo.ToString).Value = Convert.ToString(splEventDr("paxRate"))

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventValue")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            cell.PaddingRight = 4.0F
            tblSplEvent.AddCell(cell)

            ExlSheet.Range("M" + ExlRowNo.ToString + ":N" + ExlRowNo.ToString).Merge().Style.Border.SetLeftBorder(XLBorderStyleValues.Thin)
            ExlSheet.Cell("M" + ExlRowNo.ToString).Value = Convert.ToString(splEventDr("splEventValue"))
        Next
        tblSplEvent.Complete = True

        Return ExlSheet
    End Function
#End Region
#Region "Private Shared Function ImageCell(path As String, scale As Single, align As Integer) As PdfPCell"
    Private Shared Function ImageCell(ByVal path As String, ByVal scale As Single, ByVal align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScaleAbsolute(scale, scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function
#End Region
    Private Shared Function PhraseCell(ByVal phrase As Phrase, ByVal align As Integer, ByVal Cols As Integer, ByVal celBorder As Boolean, Optional ByVal celBottomBorder As String = "None") As PdfPCell
        Dim cell As New PdfPCell(phrase)
        If Cols > 1 Then cell.Colspan = Cols
        If celBorder Then
            If celBottomBorder <> "None" Then
                If celBottomBorder = "No" Then
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                    cell.BorderColor = BaseColor.BLACK
                Else
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    cell.BorderColor = BaseColor.BLACK
                End If
            Else
                cell.BorderColor = BaseColor.BLACK
            End If
        Else
            cell.Border = Rectangle.NO_BORDER
        End If
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.5F
        cell.PaddingTop = 0.0F
        Return cell
    End Function

End Class
