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

Public Class clsBookingServicerVoucher
    Dim objclsUtilities As New clsUtilities

    '*** Danny 25/09/2018
#Region "Global Variable"

    Dim NormalFont_SR As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold_SR As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
    Dim NormalFontBoldRed_SR As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.RED)
    Dim NormalFontBoldTax_SR As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
    Dim titleColor_SR As BaseColor = New BaseColor(214, 214, 214)

    Dim H1 As Font = FontFactory.GetFont("Calibri", 12, Font.BOLD, BaseColor.WHITE)
    Dim Caption1 As Font = FontFactory.GetFont("Calibri", 10, Font.BOLD, BaseColor.DARK_GRAY)
    Dim Caption2 As Font = FontFactory.GetFont("Calibri", 10, Font.BOLD, BaseColor.WHITE)

    Dim V1 As Font = FontFactory.GetFont("Calibri", 10, Font.NORMAL, BaseColor.BLACK)
    Dim V2 As Font = FontFactory.GetFont("Calibri", 10, Font.NORMAL, BaseColor.WHITE)
#End Region
#Region "Private Shared Function PhraseCell_SR(phrase As Phrase, align As Integer, Cols As Integer, celBorder As Boolean, Optional celBottomBorder As String = ""None"") As PdfPCell"
    Private Shared Function PhraseCell_SR(ByVal phrase As Phrase, ByVal align As Integer, ByVal Cols As Integer, ByVal celBorder As Boolean, Optional ByVal celBottomBorder As String = "None") As PdfPCell
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
#End Region
#Region "Private Shared Function ImageCell(path As String, scale As Single, align As Integer) As PdfPCell"
    Private Shared Function ImageCell_SR(ByVal path As String, ByVal ScaleHeight As Single, ByVal ScaleWidth As Single, ByVal align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScaleAbsolute(ScaleHeight, ScaleWidth)
        Dim cell As New PdfPCell(image)
        'cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function
#End Region

#Region "Protected Sub GuestList(ByRef tblGuest As PdfPTable, ByVal guestDt As DataTable)"
    Protected Sub GuestList_SR(ByRef tblGuest As PdfPTable, ByVal guestDt As DataTable)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        phrase = New Phrase()
        tblGuest.SetWidths(New Single() {0.64F, 0.18F, 0.18F})
        Dim guestHeader() As String = {"Name of the Guest(s)", "Child D-O-B", "Arrival"}
        For i = 0 To 2
            phrase = New Phrase()
            phrase.Add(New Chunk(guestHeader(i), NormalFontBold_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 4.0F
            cell.PaddingTop = 1.0F
            cell.BackgroundColor = titleColor_SR
            tblGuest.AddCell(cell)
        Next
        For Each guestDr As DataRow In guestDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(guestDr(0)), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingBottom = 3.0F
            tblGuest.AddCell(cell)

            Dim age As String = IIf(guestDr(1) = 0.0, "", Convert.ToString(Math.Round(guestDr(1))))
            phrase = New Phrase()
            phrase.Add(New Chunk(age, NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            tblGuest.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(guestDr(2)), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            tblGuest.AddCell(cell)
        Next
        phrase = New Phrase()
        phrase.Add(New Chunk("Total No. of Pax = " & Convert.ToString(guestDt.Rows.Count), NormalFontBold_SR))
        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
        cell.Colspan = 3
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.PaddingLeft = 5.0F
        cell.PaddingBottom = 3.0F
        tblGuest.AddCell(cell)
    End Sub
#End Region
#Region "Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef tblSplEvent As PdfPTable)"
    Protected Sub SpecialEvents_SR(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef tblSplEvent As PdfPTable)
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
            phrase.Add(New Chunk(arrSplEvent(i), NormalFontBold_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 4.0F
            cell.PaddingTop = 1.0F
            cell.BackgroundColor = splEventTitleColor
            tblSplEvent.AddCell(cell)
        Next
        For Each splEventDr As DataRow In splEventDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventName")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingLeft = 3.0F
            cell.PaddingBottom = 4.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventDate")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("noOfPax")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("paxType")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("paxRate")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            cell.PaddingRight = 4.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventValue")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            cell.PaddingRight = 4.0F
            tblSplEvent.AddCell(cell)
        Next
        tblSplEvent.Complete = True
    End Sub
#End Region
#Region "Protected Sub AppendOtherServices(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)"
    Protected Sub AppendOtherServices_SR(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        For Each inputDr As DataRow In inputDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("ServiceName")), NormalFont_SR))
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
            Else
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            End If
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingBottom = 3.0F
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("ServiceDate")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("Unit")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("UnitPrice")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            cell.PaddingRight = 4.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("UnitSaleValue")), NormalFont_SR))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            cell.PaddingRight = 4.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                phrase = New Phrase()
                phrase.Add(New Chunk(Convert.ToString(inputDr("pickupdropoff")), NormalFont_SR))
                If Convert.ToInt32(inputDr("sic")) <> 1 Then
                    phrase.Add(New Chunk(" (" & inputDr("adults").ToString() & " Adults", NormalFont_SR))
                    If String.IsNullOrEmpty(Convert.ToString(inputDr("child")).Trim()) Or Convert.ToString(inputDr("child")).Trim() = "0" Then
                        phrase.Add(New Chunk(")", NormalFont_SR))
                    Else
                        phrase.Add(New Chunk(", " & inputDr("child").ToString() & " Child)", NormalFont_SR))
                    End If
                End If
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 3.0F
                tblOthServ.AddCell(cell)
            End If
        Next
    End Sub
#End Region
#Region "Protected Sub BankDetails(ByRef tblBank As PdfPTable, ByVal BankDt As DataTable)"
    Protected Sub BankDetails_SR(ByRef tblBank As PdfPTable, ByVal BankDt As DataTable)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        phrase = New Phrase()
        tblBank.SetWidths(New Single() {0.27F, 0.73F})
        phrase = New Phrase()
        phrase.Add(New Chunk("BENEFICIARY BANK DETAILS", NormalFontBold_SR))
        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.PaddingTop = 2.0F
        cell.PaddingBottom = 5.0F
        cell.Colspan = 2
        cell.BackgroundColor = titleColor_SR
        tblBank.AddCell(cell)

        Dim bankDr As DataRow = BankDt.Rows(0)
        Dim beneficiaryDetails() As String = {"BENEFICIARY NAME", Convert.ToString(bankDr("beneficiaryName")), "BENEFICIARY ADDRESS", Convert.ToString(bankDr("beneficiaryAddress")), "BANK NAME & ADDRESS", Convert.ToString(bankDr("bankName")) & ", " & Convert.ToString(bankDr(3)), _
         "ACCOUNT NUMBER", Convert.ToString(bankDr("accountNumber")), "IBAN NUMBER", Convert.ToString(bankDr("ibanNumber")), "SWIFT CODE", Convert.ToString(bankDr("swiftCode"))}
        For i = 0 To 11
            If i Mod 2 = 0 Then
                phrase = New Phrase()
                phrase.Add(New Chunk(beneficiaryDetails(i), NormalFontBold_SR))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingTop = 1
                cell.PaddingLeft = 3
                cell.PaddingRight = 3
                cell.PaddingBottom = 3
                tblBank.AddCell(cell)
            Else
                phrase = New Phrase()
                If i = 1 Then
                    phrase.Add(New Chunk(beneficiaryDetails(i), NormalFontBoldRed_SR))
                Else
                    phrase.Add(New Chunk(beneficiaryDetails(i), NormalFontBold_SR))
                End If
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingTop = 1
                cell.PaddingLeft = 3
                cell.PaddingRight = 3
                cell.PaddingBottom = 3
                tblBank.AddCell(cell)
            End If
        Next

        phrase = New Phrase()
        phrase.Add(New Chunk("Note : It is mandatory to mention the IBAN number for Bank Payment Transfer", NormalFontBold_SR))
        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        cell.SetLeading(12, 0)
        cell.PaddingTop = 2.0F
        cell.PaddingBottom = 5.0F
        cell.Colspan = 2
        tblBank.AddCell(cell)
    End Sub
#End Region

    Public Function ReadPdfFile(ByVal fileName As String) As String
        Try
            'Dim text As New StringBuilder

            'If File.Exists(fileName) Then
            '    Dim pdfReader As New PdfReader(fileName)
            '    For Page As Integer = 1 To pdfReader.NumberOfPages
            '        Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
            '        Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfReader, Page, strategy)
            '        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.[Default], Encoding.UTF8, Encoding.[Default].GetBytes(currentText)))
            '        text.Append(currentText)
            '    Next
            '    pdfReader.Close()
            'End If

            'Return text.ToString()


            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(fileName)
            Return fileReader
        Catch ex As Exception
            Return ""
        End Try


    End Function




    Public Function GenerateServiceReport_SR(ByVal requestID As String, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, ByVal objResParam As ReservationParameters, Optional ByVal fileName As String = "") As String
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As New SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                mySqlCmd = New SqlCommand("sp_booking_confirmation_print_whitelabel", sqlConn)
            Else
                mySqlCmd = New SqlCommand("sp_booking_confirmation_print", sqlConn)
            End If
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@RequestID", SqlDbType.VarChar, 20)).Value = requestID
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            ds1 = ds
            clsDBConnect.dbConnectionClose(sqlConn)


            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@RequestID", CType(requestID, String))

            Dim ds_SR As New DataSet
            ds_SR = objclsUtilities.GetDataSet("SP_SelectServiceDetails", parm)



            If ds Is Nothing Or ds_SR Is Nothing _
                    Or ds.Tables(0) Is Nothing _
                    Or ds.Tables(1) Is Nothing _
                    Or ds.Tables(3) Is Nothing _
                    Or ds.Tables(4) Is Nothing _
                    Or ds.Tables(8) Is Nothing Then

                fileName = ""
                Return fileName
            End If
            If ds.Tables.Count = 0 _
                Or ds_SR.Tables.Count = 0 _
                Or ds.Tables(0).Rows.Count = 0 _
                Or ds.Tables(8).Rows.Count = 0 _
                Or ds_SR.Tables(0).Rows.Count = 0 Then

                fileName = ""
                Return fileName
            End If


            '*** Check parameter for Need to attach.  Danny
            '*** Only Confirmed Booking need to create SR. Danny
            'If ds_SR.Tables(0).Rows(0)("Attach").ToString() <> "Y" Or ds_SR.Tables(0).Rows(0)("ConfirmStatus").ToString() <> "1" Then
            '    fileName = ""
            '    Return fileName
            'End If

            If ds.Tables(0).Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 20.0F, 20.0F, 25.0F, 25.0F)
                Dim documentWidth As Single = 550.0F
                'Dim H1 As Font = FontFactory.GetFont("Calibri", 16, Font.BOLD, BaseColor.WHITE)
                'Dim Caption1 As Font = FontFactory.GetFont("Calibri", 10, Font.BOLD, BaseColor.DARK_GRAY)
                'Dim Caption2 As Font = FontFactory.GetFont("Calibri", 12, Font.BOLD, BaseColor.WHITE)
                ''Dim H1 As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)

                ''Dim Caption2 As Font = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)
                'Dim V1 As Font = FontFactory.GetFont("Calibri", 10, Font.NORMAL, BaseColor.BLACK)
                'Dim V2 As Font = FontFactory.GetFont("Calibri", 10, Font.NORMAL, BaseColor.WHITE)
                'Dim F1 As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK)

                'Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                'Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                'Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                'Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                Using memoryStream As New System.IO.MemoryStream()
                    Dim writer As PdfWriter
                    If printMode = "download" Then
                        writer = PdfWriter.GetInstance(document, memoryStream)
                    Else
                        Dim path As String = System.Web.HttpContext.Current.Server.MapPath("~\SavedReports\") + fileName
                        writer = PdfWriter.GetInstance(document, New FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    End If
                    Dim phrase As Phrase = Nothing
                    Dim cell As PdfPCell = Nothing
                    Dim table As PdfPTable = Nothing
                    Dim remainingPageSpace As Single
                    document.Open()

                    'Header Table
                    'Dim headerDr As DataRow = headerDt.Rows(0)

                    Dim clrBase As BaseColor = New BaseColor(0, 51, 153)
                    Dim dtblLogo As PdfPTable = New PdfPTable(4)
                    dtblLogo.SetWidths(New Single() {0.2F, 0.3F, 0.2F, 0.3F})
                    dtblLogo.TotalWidth = documentWidth
                    dtblLogo.LockedWidth = True
                    '*** Heading
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString("SERVICE VOUCHER"), H1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    cell.Colspan = 4
                    cell.BackgroundColor = clrBase
                    cell.PaddingTop = 1
                    cell.PaddingBottom = 5
                    dtblLogo.AddCell(cell)






                    Dim dtblLogoDetail As PdfPTable = New PdfPTable(2)
                    dtblLogoDetail.SetWidths(New Single() {0.6F, 0.4F})
                    dtblLogoDetail.TotalWidth = documentWidth
                    dtblLogoDetail.LockedWidth = True


                    '*** Image
                    If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                        Dim logoName As String = objclsUtilities.ExecuteQueryReturnStringValue("select logofilename from agentmast_whitelabel where agentcode ='" + objResParam.AgentCode.Trim() + "'")
                        cell = ImageCell_SR("~/vgimages/" & logoName, 129.0F, 77.0F, PdfPCell.ALIGN_CENTER)
                    Else
                        If (ds.Tables(0).Rows(0)("div_code") = "01") Then
                            cell = ImageCell_SR("~/vgimages/mahce_logo.jpg", 129.0F, 77.0F, PdfPCell.ALIGN_CENTER)
                        Else
                            cell = ImageCell_SR("~/vgimages/mahce_logo.jpg", 129.0F, 77.0F, PdfPCell.ALIGN_CENTER)
                        End If
                    End If
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 5
                    cell.PaddingLeft = 5
                    cell.PaddingRight = 1
                    cell.PaddingBottom = 5
                    cell.Border = Rectangle.NO_BORDER
                    dtblLogoDetail.AddCell(cell)


                    Dim dtblLogoDetail1 As PdfPTable = New PdfPTable(3)
                    dtblLogoDetail1.SetWidths(New Single() {0.4F, 0.4F, 0.1F})
                    dtblLogoDetail1.TotalWidth = 220
                    dtblLogoDetail1.LockedWidth = True

                    '*** Booking Details 1
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Voucher Issued on ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER
                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(DateTime.Now.ToString("ddd, dd MMM yyyy")), V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    'cell.PaddingTop = 1
                    'cell.PaddingBottom = 1
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    '*** Booking Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Booking Ref No ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER
                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(requestID), V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    'cell.PaddingTop = 1
                    'cell.PaddingBottom = 1
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    '*** Booking Details3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Customer Ref No ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER                  'cell.PaddingTop = 1

                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(ds.Tables(0).Rows(0)("agentref").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER                    'cell.PaddingTop = 1

                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    'cell.PaddingTop = 1
                    'cell.PaddingBottom = 1
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    '*** Booking Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Booking Status ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER                 'cell.PaddingTop = 1

                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk("Confirmed", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER                    'cell.PaddingTop = 1

                    'cell.PaddingTop = 1
                    cell.PaddingBottom = 2
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    'cell.PaddingTop = 1
                    'cell.PaddingBottom = 1
                    'cell.PaddingRight = 1
                    'cell.PaddingLeft = 1
                    dtblLogoDetail1.AddCell(cell)

                    cell = New PdfPCell(dtblLogoDetail1)
                    'phrase.Add(New Chunk("Tab", V1))
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = Rectangle.NO_BORDER
                    cell.PaddingTop = 5
                    cell.PaddingLeft = 5
                    cell.PaddingRight = 1
                    cell.PaddingBottom = 5
                    dtblLogoDetail.AddCell(cell)

                    ''*** Booking Details 3
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Arrival " + vbLf, Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 1
                    'dtblLogoDetail.AddCell(cell)

                    ''*** Booking Details 4
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk(Convert.ToString(Format(CType(ds_SR.Tables(0).Rows(0)("minCheckin"), Date), "dd MMM yy")) & vbLf, V1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 1
                    'dtblLogoDetail.AddCell(cell)


                    cell = New PdfPCell(dtblLogoDetail)
                    'cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER
                    'cell.PaddingTop = 2
                    'cell.PaddingBottom = 2
                    'cell.PaddingLeft = 4
                    'cell.PaddingRight = 2
                    cell.Colspan = 4
                    dtblLogo.AddCell(cell)

                    'document.Add(dtblLogo)

                    '*** Heading 

                    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                        Dim dtblHotelDetail As PdfPTable = New PdfPTable(2)
                        dtblHotelDetail.SetWidths(New Single() {0.3F, 0.7F})
                        dtblHotelDetail.TotalWidth = documentWidth - 2
                        dtblHotelDetail.LockedWidth = True
                        dtblHotelDetail.SplitRows = True
                        Dim dvGusts As New DataView(ds.Tables(8))
                        dvGusts.RowFilter = "rlineno ='" + ds.Tables(1).Rows(i)("rlineno").ToString + "'" & " AND roomno ='" + ds.Tables(1).Rows(i)("roomno").ToString + "'"
                        'dvGusts.RowFilter = "rlineno=" & row("rlineno") & " and roomno=" & row("RoomNo") & " and PType='" & row("Type") & "'" ' and taken=0"
                        'dvGDet.ToTable.Rows(0)("FromRlineno")


                        '*** Heading Hotel
                        phrase = New Phrase()
                        phrase.Add(New Chunk(Convert.ToString("Hotel Details "), Caption2))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Colspan = 2
                        cell.BackgroundColor = clrBase
                        cell.PaddingTop = 1
                        cell.PaddingBottom = 5
                        'cell.PaddingLeft = 5
                        'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                        dtblHotelDetail.AddCell(cell)


                        '*** Hotel Details 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Service Type ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        'cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.LEFT_BORDER
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk("Hotel Accommodation", V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.RIGHT_BORDER
                        dtblHotelDetail.AddCell(cell)


                        '*** Hotel Details 2
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Hotel Name  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        'cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.LEFT_BORDER
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("partyname").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Hotel Details 3
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Address  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.LEFT_BORDER
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("add1").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Hotel Details 4
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Telephone  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("tel1").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        'cell = New PdfPCell(dtblHotelDetailH)
                        ''cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        ''cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                        'cell.PaddingTop = 2
                        'cell.PaddingBottom = 2
                        'cell.PaddingLeft = 4
                        'cell.PaddingRight = 2
                        'cell.Colspan = 4
                        'dtblLogo.AddCell(cell)

                        'document.Add(dtblHotelDetailH)

                        'Dim dtblHotelDetail As PdfPTable = New PdfPTable(2)
                        'dtblHotelDetail.SetWidths(New Single() {0.3F, 0.7F})
                        'dtblHotelDetail.TotalWidth = documentWidth
                        'dtblHotelDetail.LockedWidth = True
                        'dtblHotelDetail.SplitRows = True
                        '*** Gust Details========================================================
                        '*** Heading Hotel
                        phrase = New Phrase()
                        phrase.Add(New Chunk(Convert.ToString("Guest Details"), Caption2))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Colspan = 2
                        cell.BackgroundColor = clrBase
                        cell.PaddingTop = 1
                        cell.PaddingBottom = 5
                        'cell.PaddingLeft = 5
                        'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Guest Name(s)  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.LEFT_BORDER
                        dtblHotelDetail.AddCell(cell)

                        Dim strGustName As String = ""
                        For ii As Integer = 0 To dvGusts.Count - 1
                            If strGustName.Length > 0 Then
                                strGustName = strGustName + ", "
                            End If
                            strGustName = strGustName + dvGusts.ToTable.Rows(ii)("guestname").ToString
                        Next

                        phrase = New Phrase()
                        phrase.Add(New Chunk(strGustName, V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.RIGHT_BORDER
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 2
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Hotel Conf#  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("hotelconfno").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 3
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Check-in  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("checkin").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 4
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Check-out  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("checkout").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 5
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Room Type  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("roomdetail").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 6
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Room Occupancy  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("occupancy").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        '*** Gust Details 7
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Meal plan  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("mealcode").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        ''*** Gust Details 8
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Remarks/Special Request  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(1).Rows(i)("CustomerRemark").ToString(), V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtblHotelDetail.AddCell(cell)

                        cell = New PdfPCell(dtblHotelDetail)
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                        'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                        'cell.PaddingTop = 2
                        'cell.PaddingBottom = 2
                        'cell.PaddingLeft = 4
                        'cell.PaddingRight = 2
                        cell.Colspan = 4
                        dtblLogo.AddCell(cell)

                        'document.Add(dtblHotelDetail)
                    Next



                    dtblLogo = TransferBlock(ds.Tables(3), ds.Tables(13), documentWidth, dtblLogo) '*** Transfer Services Details
                    dtblLogo = InterTranBlock(ds.Tables(3), ds.Tables(13), documentWidth, dtblLogo) '*** Transfer Services Details
                    dtblLogo = AirportMeetBlock(ds.Tables(4), ds.Tables(13), documentWidth, dtblLogo) '*** Transfer Services Details






                    dtblLogo = OtherServiceCreate(ds.Tables(7), ds.Tables(13), documentWidth, dtblLogo) '*** Other Services Details
                    dtblLogo = TermsAndConditions(ds.Tables(1), ds.Tables(0), documentWidth, dtblLogo) '*** Terms And Conditions

                    document.Add(dtblLogo)
                    ''cell = New PdfPCell(dtbltransferDetail)
                    ' ''cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    ' ''cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    ''cell.PaddingTop = 2
                    ''cell.PaddingBottom = 2
                    ''cell.PaddingLeft = 4
                    ''cell.PaddingRight = 2
                    ''cell.Colspan = 4
                    ''dtblLogo.AddCell(cell)

                    Dim dtblGap As PdfPTable = New PdfPTable(2)
                    dtblGap.SetWidths(New Single() {0.3F, 0.7F})
                    dtblGap.TotalWidth = documentWidth
                    dtblGap.LockedWidth = True
                    dtblGap.SplitRows = True
                    phrase = New Phrase()
                    phrase.Add(New Chunk(ds.Tables(0).Rows(0)("division_master_des").ToString() + " - ", V2))
                    phrase.Add(New Chunk("ADD:" + ds.Tables(0).Rows(0)("address1").ToString() + " - ", V2))
                    phrase.Add(New Chunk("Hotline:" + ds.Tables(0).Rows(0)("tel").ToString() + " - ", V2))
                    phrase.Add(New Chunk("Tel:" + ds.Tables(0).Rows(0)("tel").ToString() + " - ", V2))
                    phrase.Add(New Chunk("Fax:" + ds.Tables(0).Rows(0)("fax").ToString() + " - ", V2))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Colspan = 2
                    cell.BackgroundColor = clrBase
                    cell.PaddingTop = 1
                    cell.PaddingBottom = 5
                    cell.PaddingLeft = 5
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    dtblGap.AddCell(cell)
                    document.Add(dtblGap)





                    'Dim dtblFooter As PdfPTable = New PdfPTable(1)
                    'dtblFooter.TotalWidth = documentWidth
                    'dtblFooter.LockedWidth = True
                    'dtblFooter.SetWidths(New Single() {1.0F})

                    '*** Footer File name and content Reading
                    '*** Reading Columbus Folder path
                    'Dim strColumbusPath As String = System.Web.HttpContext.Current.Server.MapPath("")
                    'strColumbusPath = Path.GetDirectoryName(strColumbusPath)
                    'strColumbusPath = strColumbusPath + "\" + objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=8")

                    'Dim s As String = strColumbusPath + "\ExcelTemplates\" + ds_SR.Tables(0).Rows(0)("FooterFilename").ToString()
                    's = ReadPdfFile(s)
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk(s, F1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_JUSTIFIED, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    ''cell.Colspan = 4
                    ''cell.PaddingTop = 50
                    ''cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 4
                    'dtblFooter.AddCell(cell)


                    'document.Add(dtblFooter)


                    'writer.PageEvent = New clsBookingConfirmPageEvents(tblInv, printMode)

                    'document.AddTitle(Convert.ToString(ds.Tables(0).Rows(0)("printHeader")) & "-" & requestID)
                    document.AddTitle("SERVICE VOUCHER -" & requestID)
                    document.Close()

                    If printMode = "download" Then
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
                    End If
                End Using
                Return fileName
            Else
                Return ""
            End If
        Catch ex As Exception
            'MessageBox.ShowMessage(Page, MessageType.Warning, ex.StackTrace.ToString)
            Return ""
        End Try
    End Function
    '****
    Private Function AirportMeetBlock(ByVal dtService As DataTable, ByVal dtServiceDetails As DataTable, ByVal documentWidth As Single, ByVal dtblLogo As PdfPTable) As PdfPTable
        '*** Airport Services Details 
        Try

            Dim phrase As New Phrase
            Dim cell As New PdfPCell

            Dim clrBase As BaseColor = New BaseColor(0, 51, 153)

            Dim dtblAirMeetDetail As PdfPTable = New PdfPTable(2)
            dtblAirMeetDetail.SetWidths(New Single() {0.3F, 0.7F})
            dtblAirMeetDetail.TotalWidth = documentWidth - 2
            dtblAirMeetDetail.LockedWidth = True
            dtblAirMeetDetail.SplitRows = True
            For i As Integer = 0 To dtService.Rows.Count - 1


                Dim strPicUp As String() = dtService.Rows(i)("ServiceName").ToString.Split("/")


                '*** Heading transfer
                phrase = New Phrase()
                phrase.Add(New Chunk(Convert.ToString("Airport Services "), Caption2))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.Colspan = 2
                'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                cell.BackgroundColor = clrBase
                cell.PaddingTop = 1
                cell.PaddingBottom = 5
                'cell.PaddingLeft = 5
                dtblAirMeetDetail.AddCell(cell)

                '*** Airport Details 1
                phrase = New Phrase()
                phrase.Add(New Chunk("Service Type  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingRight = 15
                'cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)



                phrase = New Phrase()
                phrase.Add(New Chunk(strPicUp(1), V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)




                '*** Airport Services Details 2
                phrase = New Phrase()
                phrase.Add(New Chunk("Supplier Name  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("partyname").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                '*** Airport Services Details 3
                phrase = New Phrase()
                phrase.Add(New Chunk("Address  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("add1").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                '*** Airport Services Details 4
                phrase = New Phrase()
                phrase.Add(New Chunk("Telephone  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("tel1").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                '*** Gust Details========================================================
                '*** Heading Gust
                phrase = New Phrase()
                phrase.Add(New Chunk(Convert.ToString("Guest Details"), Caption2))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.Colspan = 2
                cell.BackgroundColor = clrBase
                cell.PaddingTop = 1
                cell.PaddingBottom = 5
                'cell.PaddingLeft = 5
                'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                dtblAirMeetDetail.AddCell(cell)

                '*** Gust Details 1
                phrase = New Phrase()
                phrase.Add(New Chunk("Guest Name(s)  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                Dim dvGusts As New DataView(dtServiceDetails)
                dvGusts.RowFilter = "servicelineno ='" + dtService.Rows(i)("alineno").ToString + "' AND Services LIKE 'AIRPORT SERVICE%'"
                Dim strGustName As String = ""
                Dim strGustNo As String = ""
                For ii As Integer = 0 To dvGusts.Count - 1
                    If strGustName.Length > 0 Then
                        strGustName = strGustName + ", "
                    End If
                    strGustName = strGustName + dvGusts.ToTable.Rows(ii)("guestname").ToString
                    strGustNo = dvGusts.ToTable.Rows(ii)("guestlineno").ToString
                Next

                'Dim dvGustsService As New DataView(ds.Tables(12))
                'dvGustsService.RowFilter = "guestlineno ='" + strGustNo.ToString + "'"

                phrase = New Phrase()
                phrase.Add(New Chunk(strGustName, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                '*** Gust Details 2
                phrase = New Phrase()
                phrase.Add(New Chunk("Services Confirmatoin  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("airportmateconfno").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                cell.PaddingLeft = 2
                'cell.Border = Rectangle.RIGHT_BORDER
                dtblAirMeetDetail.AddCell(cell)

                If dtService.Rows(i)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                    '*** Gust Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Flight Details   ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    phrase.Add(New Chunk("", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    '*** Gust Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Date  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("airportmadate").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)

                    '*** Gust Details 5
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Time  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 6
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Vehicle Type  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'If ds.Tables(3).Rows(i)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                    '    phrase.Add(New Chunk(ds.Tables(3).Rows(i)("othcatname").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    'dtblAirMeetDetail.AddCell(cell)


                    '*** Gust Details 7
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Location  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    'If ds.Tables(3).Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    phrase.Add(New Chunk(strPicUp(0), V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 8
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'phrase.Add(New Chunk(strPicUp(1), V1))
                    ''If ds.Tables(3).Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    ''    phrase.Add(New Chunk(ds.Tables(3).Rows(i)("Drop").ToString, V1))
                    ''Else
                    ''    phrase.Add(New Chunk("", V1))
                    ''End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)
                End If
                If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                    '*** Gust Details 9
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Flight Details  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)

                    '*** Gust Details 10
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Date  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("airportmadate").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 11
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Pick up time  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                    '    phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)

                    '*** Gust Details 12
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Location  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    'If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                    '    phrase.Add(New Chunk(dtService.Rows(i)("othtypname").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    phrase.Add(New Chunk(strPicUp(0), V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 13
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                    '    phrase.Add(New Chunk(dtService.Rows(i)("Drop").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)
                End If
                '*** Gust Details 14
                phrase = New Phrase()
                phrase.Add(New Chunk("Remarks/Special Request  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                cell.PaddingBottom = 5
                'cell.Border = Rectangle.LEFT_BORDER
                'cell.Border = Rectangle.BOTTOM_BORDER
                cell.PaddingRight = 15
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)



                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("CustomerRemark").ToString(), V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                'cell.Border = Rectangle.BOTTOM_BORDER

                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                cell = New PdfPCell(dtblAirMeetDetail)
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                'cell.PaddingTop = 2
                'cell.PaddingBottom = 2
                'cell.PaddingLeft = 4
                'cell.PaddingRight = 2
                cell.Colspan = 4
                dtblLogo.AddCell(cell)

            Next

        Catch ex As Exception

        End Try

        Return dtblLogo

    End Function

    '****
    Private Function InterTranBlock(ByVal dtService As DataTable, ByVal dtServiceDetails As DataTable, ByVal documentWidth As Single, ByVal dtblLogo As PdfPTable) As PdfPTable

        '*** Inter transfer 
        Dim phrase As New Phrase
        Dim cell As New PdfPCell

        Dim clrBase As BaseColor = New BaseColor(0, 51, 153)
        Dim dtbltransferDetail As PdfPTable = New PdfPTable(2)
        dtbltransferDetail.SetWidths(New Single() {0.3F, 0.7F})
        dtbltransferDetail.TotalWidth = documentWidth - 2
        dtbltransferDetail.LockedWidth = True
        dtbltransferDetail.SplitRows = True
        Try
            For i As Integer = 0 To dtService.Rows.Count - 1
                If dtService.Rows(i)("transfertype").ToString.ToUpper = "INTERHOTEL" Then


                    '*** Heading transfer
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString("Inter Transfer Details "), Caption2))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Colspan = 2
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    cell.BackgroundColor = clrBase
                    cell.PaddingTop = 1
                    cell.PaddingBottom = 5
                    'cell.PaddingLeft = 5
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 1
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Service Type  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("pickupdropoff").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Supplier Name  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("partyname").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Address  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("add1").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Telephone  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("tel1").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    '*** Gust Details========================================================
                    '*** Heading Gust
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString("Guest Details"), Caption2))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Colspan = 2
                    cell.BackgroundColor = clrBase
                    cell.PaddingTop = 1
                    cell.PaddingBottom = 5
                    cell.PaddingLeft = 5
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    dtbltransferDetail.AddCell(cell)

                    '*** Gust Details 1
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Guest Name(s)  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    Dim dvGusts As New DataView(dtServiceDetails)
                    dvGusts.RowFilter = "servicelineno ='" + dtService.Rows(i)("tlineno").ToString + "' AND Services LIKE 'TRANSFER%'"
                    Dim strGustName As String = ""
                    Dim strGustNo As String = ""
                    For ii As Integer = 0 To dvGusts.Count - 1
                        If strGustName.Length > 0 Then
                            strGustName = strGustName + ", "
                        End If
                        strGustName = strGustName + dvGusts.ToTable.Rows(ii)("guestname").ToString
                        strGustNo = dvGusts.ToTable.Rows(ii)("guestlineno").ToString
                    Next

                    'Dim dvGustsService As New DataView(ds.Tables(12))
                    'dvGustsService.RowFilter = "guestlineno ='" + strGustNo.ToString + "'"

                    phrase = New Phrase()
                    phrase.Add(New Chunk(strGustName, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    '*** Gust Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Transfer Confirmatoin  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("transferconfno").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    dtbltransferDetail.AddCell(cell)



                    '*** Gust Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Arr Flight Details   ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("transfertype").ToString.ToUpper = "INTERHOTEL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    phrase.Add(New Chunk("", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    '*** Gust Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Transfer Date - Arrival  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("transfertype").ToString.ToUpper = "INTERHOTEL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("transferdate").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** Gust Details 5
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Transfer Time  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("transfertype").ToString.ToUpper = "INTERHOTEL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** Gust Details 6
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Vehicle Type  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    If dtService.Rows(i)("transfertype").ToString.ToUpper = "INTERHOTEL" Then
                        phrase.Add(New Chunk(dtService.Rows(i)("othcatname").ToString, V1))
                    Else
                        phrase.Add(New Chunk("", V1))
                    End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    dtbltransferDetail.AddCell(cell)


                    '*** Gust Details 7
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Pick Up Location  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    Dim strPicUp As String() = dtService.Rows(i)("PickAndDrop").ToString.Split("/")
                    phrase = New Phrase()
                    'If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    phrase.Add(New Chunk(strPicUp(0), V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    dtbltransferDetail.AddCell(cell)

                    '*** Gust Details 8
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(strPicUp(1), V1))
                    'If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    '    phrase.Add(New Chunk(dtService.Rows(i)("Drop").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    'If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                    '    '*** Gust Details 9
                    '    phrase = New Phrase()
                    '    phrase.Add(New Chunk("Dept Flight Details  ", Caption1))
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    ''    cell.Border = Rectangle.LEFT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)


                    '    phrase = New Phrase()
                    '    If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                    '        phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                    '    Else
                    '        phrase.Add(New Chunk("", V1))
                    '    End If
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.Border = Rectangle.RIGHT_BORDER
                    '    cell.PaddingRight = 15
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)

                    '    '*** Gust Details 10
                    '    phrase = New Phrase()
                    '    phrase.Add(New Chunk("Transfer Date - Departure  ", Caption1))
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.Border = Rectangle.LEFT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)


                    '    phrase = New Phrase()
                    '    If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                    '        phrase.Add(New Chunk(dtService.Rows(i)("transferdate").ToString, V1))
                    '    Else
                    '        phrase.Add(New Chunk("", V1))
                    '    End If
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.Border = Rectangle.RIGHT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)

                    '    '*** Gust Details 11
                    '    phrase = New Phrase()
                    '    phrase.Add(New Chunk("Pick up time  ", Caption1))
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.Border = Rectangle.LEFT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)


                    '    phrase = New Phrase()
                    '    If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                    '        phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                    '    Else
                    '        phrase.Add(New Chunk("", V1))
                    '    End If
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.Border = Rectangle.RIGHT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)

                    '    '*** Gust Details 12
                    '    phrase = New Phrase()
                    '    phrase.Add(New Chunk("Pick Up Location  ", Caption1))
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.Border = Rectangle.LEFT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)


                    '    phrase = New Phrase()
                    '    If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                    '        phrase.Add(New Chunk(dtService.Rows(i)("othtypname").ToString, V1))
                    '    Else
                    '        phrase.Add(New Chunk("", V1))
                    '    End If
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.PaddingLeft = 2
                    '    cell.Border = Rectangle.RIGHT_BORDER
                    '    dtbltransferDetail.AddCell(cell)

                    '    '*** Gust Details 13
                    '    phrase = New Phrase()
                    '    phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.Border = Rectangle.LEFT_BORDER
                    '    cell.PaddingRight = 15
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)


                    '    phrase = New Phrase()
                    '    If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                    '        phrase.Add(New Chunk(dtService.Rows(i)("Drop").ToString, V1))
                    '    Else
                    '        phrase.Add(New Chunk("", V1))
                    '    End If
                    '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    '    cell.SetLeading(12, 0)
                    '    cell.PaddingBottom = 5
                    '    cell.PaddingRight = 15
                    '    cell.Border = Rectangle.RIGHT_BORDER
                    '    cell.PaddingLeft = 2
                    '    dtbltransferDetail.AddCell(cell)
                    'End If
                    '*** Gust Details 14
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Remarks/Special Request  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)



                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("CustomerRemark").ToString(), V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    cell = New PdfPCell(dtbltransferDetail)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    'cell.PaddingTop = 2
                    'cell.PaddingBottom = 2
                    'cell.PaddingLeft = 4
                    'cell.PaddingRight = 2
                    cell.Colspan = 4
                    dtblLogo.AddCell(cell)

                End If
            Next
        Catch ex As Exception

        End Try

        Return dtblLogo
    End Function

    '****
    Private Function TransferBlock(ByVal dtService As DataTable, ByVal dtServiceDetails As DataTable, ByVal documentWidth As Single, ByVal dtblLogo As PdfPTable) As PdfPTable
        '*** transfer 
        Try
            Dim phrase As New Phrase
            Dim cell As New PdfPCell

            Dim clrBase As BaseColor = New BaseColor(0, 51, 153)
            Dim dtbltransferDetail As PdfPTable = New PdfPTable(2)
            dtbltransferDetail.SetWidths(New Single() {0.3F, 0.7F})
            dtbltransferDetail.TotalWidth = documentWidth - 2
            dtbltransferDetail.LockedWidth = True
            dtbltransferDetail.SplitRows = True

            For i As Integer = 0 To dtService.Rows.Count - 1
                If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" _
                    Or dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then

                    '*** Heading transfer
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString("Transfer Details "), Caption2))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Colspan = 2
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    cell.BackgroundColor = clrBase
                    cell.PaddingTop = 1
                    cell.PaddingBottom = 5
                    'cell.PaddingLeft = 5
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 1
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Service Type  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("pickupdropoff").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Supplier Name  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("partyname").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Address  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("add1").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    '*** transfer Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Telephone  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("tel1").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    '*** Gust Details========================================================
                    '*** Heading Gust
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString("Guest Details"), Caption2))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Colspan = 2
                    cell.BackgroundColor = clrBase
                    cell.PaddingTop = 1
                    cell.PaddingBottom = 5
                    cell.PaddingLeft = 5
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    dtbltransferDetail.AddCell(cell)

                    '*** Gust Details 1
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Guest Name(s)  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)

                    Dim dvGusts As New DataView(dtServiceDetails)
                    dvGusts.RowFilter = "servicelineno ='" + dtService.Rows(i)("tlineno").ToString + "' AND Services LIKE 'TRANSFER%'"
                    Dim strGustName As String = ""
                    Dim strGustNo As String = ""
                    For ii As Integer = 0 To dvGusts.Count - 1
                        If strGustName.Length > 0 Then
                            strGustName = strGustName + ", "
                        End If
                        strGustName = strGustName + dvGusts.ToTable.Rows(ii)("guestname").ToString
                        strGustNo = dvGusts.ToTable.Rows(ii)("guestlineno").ToString
                    Next

                    'Dim dvGustsService As New DataView(ds.Tables(12))
                    'dvGustsService.RowFilter = "guestlineno ='" + strGustNo.ToString + "'"

                    phrase = New Phrase()
                    phrase.Add(New Chunk(strGustName, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    '*** Gust Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Transfer Confirmatoin  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("transferconfno").ToString, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    'cell.Border = Rectangle.RIGHT_BORDER
                    dtbltransferDetail.AddCell(cell)

                    If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                        '*** Gust Details 3
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Arr Flight Details   ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        phrase.Add(New Chunk("", V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        '*** Gust Details 4
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Transfer Date - Arrival  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("transferdate").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 5
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Transfer Time  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 6
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Vehicle Type  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("othcatname").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.RIGHT_BORDER
                        dtbltransferDetail.AddCell(cell)


                        '*** Gust Details 7
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Pick Up Location  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)

                        Dim strPicUp As String() = dtService.Rows(i)("PickAndDrop").ToString.Split("/")
                        phrase = New Phrase()
                        'If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                        phrase.Add(New Chunk(strPicUp(0), V1))
                        'Else
                        '    phrase.Add(New Chunk("", V1))
                        'End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.RIGHT_BORDER
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 8
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        phrase.Add(New Chunk(strPicUp(1), V1))
                        'If dtService.Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                        '    phrase.Add(New Chunk(dtService.Rows(i)("Drop").ToString, V1))
                        'Else
                        '    phrase.Add(New Chunk("", V1))
                        'End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)
                    End If
                    If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                        '*** Gust Details 9
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Dept Flight Details  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 10
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Transfer Date - Departure  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("transferdate").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 11
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Pick up time  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 12
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Pick Up Location  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("othtypname").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        'cell.Border = Rectangle.RIGHT_BORDER
                        dtbltransferDetail.AddCell(cell)

                        '*** Gust Details 13
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        'cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingRight = 15
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)


                        phrase = New Phrase()
                        If dtService.Rows(i)("transfertype").ToString.ToUpper = "DEPARTURE" Then
                            phrase.Add(New Chunk(dtService.Rows(i)("Drop").ToString, V1))
                        Else
                            phrase.Add(New Chunk("", V1))
                        End If
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(12, 0)
                        cell.PaddingBottom = 5
                        cell.PaddingRight = 15
                        'cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingLeft = 2
                        dtbltransferDetail.AddCell(cell)
                    End If
                    '*** Gust Details 14
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Remarks/Special Request  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)



                    phrase = New Phrase()
                    phrase.Add(New Chunk(dtService.Rows(i)("CustomerRemark").ToString(), V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingLeft = 2
                    dtbltransferDetail.AddCell(cell)


                    cell = New PdfPCell(dtbltransferDetail)
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    'cell.PaddingTop = 2
                    'cell.PaddingBottom = 2
                    'cell.PaddingLeft = 4
                    'cell.PaddingRight = 2
                    cell.Colspan = 4
                    dtblLogo.AddCell(cell)

                End If
            Next

        Catch ex As Exception

        End Try

        Return dtblLogo
    End Function

    '****
    Private Function TermsAndConditions(ByVal dtService As DataTable, ByVal dtServiceDetails As DataTable, ByVal documentWidth As Single, ByVal dtblLogo As PdfPTable) As PdfPTable
        Try
            Dim phrase As New Phrase
            Dim cell As New PdfPCell


            Dim clrBase As BaseColor = New BaseColor(0, 51, 153)
            Dim dtblAirMeetDetail As PdfPTable = New PdfPTable(2)
            dtblAirMeetDetail.SetWidths(New Single() {0.3F, 0.7F})
            dtblAirMeetDetail.TotalWidth = documentWidth - 2
            dtblAirMeetDetail.LockedWidth = True
            dtblAirMeetDetail.SplitRows = True
            '*** Heading TERMS & CONDITIONS


            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString("TERMS & CONDITIONS "), Caption2))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Colspan = 2
            'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
            cell.BackgroundColor = clrBase
            cell.PaddingTop = 1
            cell.PaddingBottom = 5
            'cell.PaddingLeft = 5
            dtblAirMeetDetail.AddCell(cell)

            Dim strHotelcode As String = ""
            For i As Integer = 0 To dtService.Rows.Count - 1
                '*** '*** Heading Check in & Check out Policy:
                If strHotelcode.Contains(dtService.Rows(i)("partycode").ToString.Trim) = False Then

                    strHotelcode = strHotelcode + dtService.Rows(i)("partycode").ToString.Trim + ","
                    Dim parm(7) As SqlParameter
                    parm(0) = New SqlParameter("@partycode", CType(dtService.Rows(i)("partycode").ToString, String))
                    parm(1) = New SqlParameter("@rmtypcode", CType(dtService.Rows(i)("rmtypcode").ToString, String))
                    parm(2) = New SqlParameter("@mealcode", CType(dtService.Rows(i)("mealcode").ToString, String))
                    parm(3) = New SqlParameter("@rateplanid", CType(dtService.Rows(i)("rateplanid").ToString, String))
                    parm(4) = New SqlParameter("@agentcode", CType(dtServiceDetails.Rows(0)("agentcode").ToString, String))
                    parm(5) = New SqlParameter("@sourcecountry", CType(dtServiceDetails.Rows(0)("sourcectrycode").ToString, String))
                    parm(6) = New SqlParameter("@checkin", CType(dtService.Rows(i)("checkin"), Date).ToString("yyyy/MM/dd"))
                    parm(7) = New SqlParameter("@checkout", CType(dtService.Rows(i)("checkout"), Date).ToString("yyyy/MM/dd"))

                    Dim ds_ChP As New DataSet
                    ds_ChP = objclsUtilities.GetDataSet("sp_booking_checkinoutpolicy", parm)


                    If Not ds_ChP Is Nothing Then
                        If ds_ChP.Tables.Count > 0 Then
                            If ds_ChP.Tables(0).Rows.Count > 0 Then

                                phrase = New Phrase()
                                phrase.Add(New Chunk("Check in & Check out Policy  " + vbCr, Caption1))
                                phrase.Add(New Chunk("(" + CType(dtService.Rows(i)("partyname").ToString, String) + ")", Caption1))
                                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingBottom = 5
                                'cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingRight = 15
                                cell.PaddingLeft = 2
                                dtblAirMeetDetail.AddCell(cell)

                                phrase = New Phrase()
                                For ii As Integer = 0 To ds_ChP.Tables(0).Rows.Count - 1
                                    phrase.Add(New Chunk((ii + 1).ToString + ") " + ds_ChP.Tables(0).Rows(ii)("checkinoutpolicytext").ToString + vbCr, V1))
                                Next
                                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingBottom = 5
                                cell.PaddingRight = 15
                                'cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingLeft = 2
                                dtblAirMeetDetail.AddCell(cell)
                            End If
                        End If
                    End If

                End If
            Next


            Dim strSpecialRequest As String = ""
            strHotelcode = ""
            For i As Integer = 0 To dtService.Rows.Count - 1

                '*** '*** Heading Check in & Check out Policy:
                If strHotelcode.Contains(dtService.Rows(i)("partycode").ToString.Trim) = False Then

                    strHotelcode = strHotelcode + dtService.Rows(i)("partycode").ToString.Trim + ","
                    Dim parm(7) As SqlParameter
                    parm(0) = New SqlParameter("@partycode", CType(dtService.Rows(i)("partycode").ToString, String))
                    parm(1) = New SqlParameter("@rmtypcode", CType(dtService.Rows(i)("rmtypcode").ToString, String))
                    parm(2) = New SqlParameter("@mealcode", CType(dtService.Rows(i)("mealcode").ToString, String))
                    parm(3) = New SqlParameter("@rateplanid", CType(dtService.Rows(i)("rateplanid").ToString, String))
                    parm(4) = New SqlParameter("@agentcode", CType(dtServiceDetails.Rows(0)("agentcode").ToString, String))
                    parm(5) = New SqlParameter("@sourcecountry", CType(dtServiceDetails.Rows(0)("sourcectrycode").ToString, String))
                    parm(6) = New SqlParameter("@checkin", CType(dtService.Rows(i)("checkin"), Date).ToString("yyyy/MM/dd"))
                    parm(7) = New SqlParameter("@checkout", CType(dtService.Rows(i)("checkout"), Date).ToString("yyyy/MM/dd"))

                    Dim ds_CP As New DataSet
                    ds_CP = objclsUtilities.GetDataSet("sp_booking_cancelpolicy", parm)

                    If Not ds_CP Is Nothing Then
                        If ds_CP.Tables.Count > 0 Then
                            If ds_CP.Tables(0).Rows.Count > 0 Then

                                phrase = New Phrase()
                                phrase.Add(New Chunk("Cancellation Policy  ", Caption1))
                                phrase.Add(New Chunk("(" + CType(dtService.Rows(i)("partyname").ToString, String) + ")", Caption1))
                                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingBottom = 5
                                'cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingRight = 15
                                cell.PaddingLeft = 2
                                dtblAirMeetDetail.AddCell(cell)

                                phrase = New Phrase()
                                For ii As Integer = 0 To ds_CP.Tables(0).Rows.Count - 1
                                    phrase.Add(New Chunk((ii + 1).ToString + ") " + ds_CP.Tables(0).Rows(ii)("canceltext").ToString + vbCr, V1))
                                Next
                                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingBottom = 5
                                cell.PaddingRight = 15
                                'cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingLeft = 2
                                dtblAirMeetDetail.AddCell(cell)
                            End If
                        End If
                    End If

                    If CType(dtService.Rows(i)("CustomerRemark").ToString, String).Trim.Length > 0 Then
                        strSpecialRequest = strSpecialRequest + CType(dtServiceDetails.Rows(i)("CustomerRemarks").ToString, String) + vbCr
                    End If
                End If
            Next


            ''*** Special Request 
            phrase = New Phrase()
            phrase.Add(New Chunk("Special Request  ", Caption1))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingBottom = 5
            'cell.Border = Rectangle.LEFT_BORDER
            cell.PaddingRight = 15
            cell.PaddingLeft = 2
            dtblAirMeetDetail.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(strSpecialRequest, V1))
            cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingBottom = 5
            cell.PaddingRight = 15
            'cell.Border = Rectangle.RIGHT_BORDER
            cell.PaddingLeft = 2
            dtblAirMeetDetail.AddCell(cell)


            cell = New PdfPCell(dtblAirMeetDetail)
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
            'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
            'cell.PaddingTop = 2
            'cell.PaddingBottom = 2
            'cell.PaddingLeft = 4
            'cell.PaddingRight = 2
            cell.Colspan = 4
            dtblLogo.AddCell(cell)


        Catch ex As Exception

        End Try
        Return dtblLogo
    End Function
    '*** Other Services Details 
    Private Function OtherServiceCreate(ByVal dtService As DataTable, ByVal dtServiceDetails As DataTable, ByVal documentWidth As Single, ByVal dtblLogo As PdfPTable) As PdfPTable
        Try
            Dim phrase As New Phrase
            Dim cell As New PdfPCell

            Dim dtblAirMeetDetail As PdfPTable = New PdfPTable(2)
            dtblAirMeetDetail.SetWidths(New Single() {0.3F, 0.7F})
            dtblAirMeetDetail.TotalWidth = documentWidth - 2
            dtblAirMeetDetail.LockedWidth = True
            dtblAirMeetDetail.SplitRows = True
            Dim clrBase As BaseColor = New BaseColor(0, 51, 153)

            For i As Integer = 0 To dtService.Rows.Count - 1


                Dim strPicUp As String() = dtService.Rows(i)("ServiceName").ToString.Split("/")


                '*** Heading transfer
                phrase = New Phrase()
                phrase.Add(New Chunk(Convert.ToString("Other Services "), Caption2))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.Colspan = 2
                'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                cell.BackgroundColor = clrBase
                cell.PaddingTop = 1
                cell.PaddingBottom = 5
                'cell.PaddingLeft = 5
                dtblAirMeetDetail.AddCell(cell)

                '*** Other Details 1
                phrase = New Phrase()
                phrase.Add(New Chunk("Service Type  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingRight = 15
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)



                phrase = New Phrase()
                phrase.Add(New Chunk(strPicUp(0), V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)




                '*** Other Services Details 2
                phrase = New Phrase()
                phrase.Add(New Chunk("Supplier Name  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("partyname").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                '*** Other Services Details 3
                phrase = New Phrase()
                phrase.Add(New Chunk("Address  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("add1").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                '*** Other Services Details 4
                phrase = New Phrase()
                phrase.Add(New Chunk("Telephone  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("tel1").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                '*** Gust Details========================================================
                '*** Heading Gust
                phrase = New Phrase()
                phrase.Add(New Chunk(Convert.ToString("Guest Details"), Caption2))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.Colspan = 2
                cell.BackgroundColor = clrBase
                cell.PaddingTop = 1
                cell.PaddingBottom = 5
                cell.PaddingLeft = 5
                ''cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                dtblAirMeetDetail.AddCell(cell)

                '*** Gust Details 1
                phrase = New Phrase()
                phrase.Add(New Chunk("Guest Name(s)  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                Dim dvGusts As New DataView(dtServiceDetails)
                dvGusts.RowFilter = "servicelineno ='" + dtService.Rows(i)("olineno").ToString + "' AND Services LIKE 'OTHERS%'"
                Dim strGustName As String = ""
                Dim strGustNo As String = ""
                For ii As Integer = 0 To dvGusts.Count - 1
                    If strGustName.Length > 0 Then
                        strGustName = strGustName + ", "
                    End If
                    strGustName = strGustName + dvGusts.ToTable.Rows(ii)("guestname").ToString
                    strGustNo = dvGusts.ToTable.Rows(ii)("guestlineno").ToString
                Next

                'Dim dvGustsService As New DataView(ds.Tables(12))
                'dvGustsService.RowFilter = "guestlineno ='" + strGustNo.ToString + "'"

                phrase = New Phrase()
                phrase.Add(New Chunk(strGustName, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                '*** Gust Details 2
                phrase = New Phrase()
                phrase.Add(New Chunk("Services Confirmatoin  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.LEFT_BORDER
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)


                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("othersconfno").ToString, V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                cell.PaddingLeft = 2
                'cell.Border = Rectangle.RIGHT_BORDER
                dtblAirMeetDetail.AddCell(cell)

                If dtService.Rows(i)("Servicetype").ToString.ToUpper = "OTHERS" Then
                    ''*** Gust Details 3
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Services Flight Details   ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'If dtService.Rows(i)("Servicetype").ToString.ToUpper = "ARRIVAL" Then
                    '    phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    'phrase.Add(New Chunk("", V1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.RIGHT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    '*** Gust Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Services Date  ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 15
                    'cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)


                    phrase = New Phrase()
                    'If dtService.Rows(i)("Servicetype").ToString.ToUpper = "ARRIVAL" Then
                    phrase.Add(New Chunk(dtService.Rows(i)("othdate").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 5
                    'cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingRight = 15
                    cell.PaddingLeft = 2
                    dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 5
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Services Time  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'If dtService.Rows(i)("Servicetype").ToString.ToUpper = "ARRIVAL" Then
                    '    phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    ''cell.Border = Rectangle.RIGHT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 6
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Vehicle Type  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'If ds.Tables(3).Rows(i)("airportmatype").ToString.ToUpper = "ARRIVAL" Then
                    '    phrase.Add(New Chunk(ds.Tables(3).Rows(i)("othcatname").ToString, V1))
                    'Else
                    '    phrase.Add(New Chunk("", V1))
                    'End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    ''cell.Border = Rectangle.RIGHT_BORDER
                    'dtblAirMeetDetail.AddCell(cell)


                    ''*** Gust Details 7
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Services Location  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    ''If ds.Tables(3).Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    'phrase.Add(New Chunk(strPicUp(0), V1))
                    ''Else
                    ''    phrase.Add(New Chunk("", V1))
                    ''End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    ''cell.Border = Rectangle.RIGHT_BORDER
                    'dtblAirMeetDetail.AddCell(cell)

                    ''*** Gust Details 8
                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.LEFT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)


                    'phrase = New Phrase()
                    'phrase.Add(New Chunk(strPicUp(1), V1))
                    ''If ds.Tables(3).Rows(i)("transfertype").ToString.ToUpper = "ARRIVAL" Then
                    ''    phrase.Add(New Chunk(ds.Tables(3).Rows(i)("Drop").ToString, V1))
                    ''Else
                    ''    phrase.Add(New Chunk("", V1))
                    ''End If
                    'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.SetLeading(12, 0)
                    'cell.PaddingBottom = 5
                    ''cell.Border = Rectangle.RIGHT_BORDER
                    'cell.PaddingRight = 15
                    'cell.PaddingLeft = 2
                    'dtblAirMeetDetail.AddCell(cell)
                End If
                'If dtService.Rows(i)("Servicetype").ToString.ToUpper = "DEPARTURE" Then
                '    '*** Gust Details 9
                '    phrase = New Phrase()
                '    phrase.Add(New Chunk("Services Flight Details  ", Caption1))
                '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                '    cell.SetLeading(12, 0)
                '    cell.PaddingBottom = 5
                '    cell.PaddingRight = 15
                ''    cell.Border = Rectangle.LEFT_BORDER
                '    cell.PaddingLeft = 2
                '    dtblAirMeetDetail.AddCell(cell)


                '    phrase = New Phrase()
                '    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                '        phrase.Add(New Chunk(dtService.Rows(i)("flightcode").ToString + "@" + dtService.Rows(i)("flighttime").ToString, V1))
                '    Else
                '        phrase.Add(New Chunk("", V1))
                '    End If
                '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                '    cell.SetLeading(12, 0)
                '    cell.PaddingBottom = 5
                ''    cell.Border = Rectangle.RIGHT_BORDER
                '    cell.PaddingRight = 15
                '    cell.PaddingLeft = 2
                '    dtblAirMeetDetail.AddCell(cell)

                '    '*** Gust Details 10
                '    phrase = New Phrase()
                '    phrase.Add(New Chunk("Services Date  ", Caption1))
                '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                '    cell.SetLeading(12, 0)
                '    cell.PaddingBottom = 5
                '    cell.PaddingRight = 15
                ''    cell.Border = Rectangle.LEFT_BORDER
                '    cell.PaddingLeft = 2
                '    dtblAirMeetDetail.AddCell(cell)


                '    phrase = New Phrase()
                '    If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                '        phrase.Add(New Chunk(dtService.Rows(i)("airportmadate").ToString, V1))
                '    Else
                '        phrase.Add(New Chunk("", V1))
                '    End If
                '    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                '    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                '    cell.SetLeading(12, 0)
                '    cell.PaddingBottom = 5
                '    cell.PaddingRight = 15
                ''    cell.Border = Rectangle.RIGHT_BORDER
                '    cell.PaddingLeft = 2
                '    dtblAirMeetDetail.AddCell(cell)

                ''*** Gust Details 11
                'phrase = New Phrase()
                'phrase.Add(New Chunk("Pick up time  ", Caption1))
                'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                'cell.SetLeading(12, 0)
                'cell.PaddingBottom = 5
                'cell.PaddingRight = 15
                ''cell.Border = Rectangle.LEFT_BORDER
                'cell.PaddingLeft = 2
                'dtblAirMeetDetail.AddCell(cell)


                'phrase = New Phrase()
                'If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                '    phrase.Add(New Chunk(dtService.Rows(i)("flighttime").ToString, V1))
                'Else
                '    phrase.Add(New Chunk("", V1))
                'End If
                'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                'cell.SetLeading(12, 0)
                'cell.PaddingBottom = 5
                'cell.PaddingRight = 15
                ''cell.Border = Rectangle.RIGHT_BORDER
                'cell.PaddingLeft = 2
                'dtblAirMeetDetail.AddCell(cell)

                ''*** Gust Details 12
                'phrase = New Phrase()
                'phrase.Add(New Chunk("Services Location  ", Caption1))
                'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                'cell.SetLeading(12, 0)
                'cell.PaddingBottom = 5
                'cell.PaddingRight = 15
                ''cell.Border = Rectangle.LEFT_BORDER
                'cell.PaddingLeft = 2
                'dtblAirMeetDetail.AddCell(cell)


                'phrase = New Phrase()
                ''If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                ''    phrase.Add(New Chunk(dtService.Rows(i)("othtypname").ToString, V1))
                ''Else
                ''    phrase.Add(New Chunk("", V1))
                ''End If
                'phrase.Add(New Chunk(strPicUp(0), V1))
                'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                'cell.SetLeading(12, 0)
                'cell.PaddingBottom = 5
                'cell.PaddingRight = 15
                'cell.PaddingLeft = 2
                ''cell.Border = Rectangle.RIGHT_BORDER
                'dtblAirMeetDetail.AddCell(cell)

                ''*** Gust Details 13
                'phrase = New Phrase()
                'phrase.Add(New Chunk("Drop Off Location  ", Caption1))
                'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_RIGHT, 1, False)
                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                'cell.SetLeading(12, 0)
                'cell.PaddingBottom = 5
                ''cell.Border = Rectangle.LEFT_BORDER
                'cell.PaddingRight = 15
                'cell.PaddingLeft = 2
                'dtblAirMeetDetail.AddCell(cell)


                'phrase = New Phrase()
                'If dtService.Rows(i)("airportmatype").ToString.ToUpper = "DEPARTURE" Then
                '    phrase.Add(New Chunk(dtService.Rows(i)("Drop").ToString, V1))
                'Else
                '    phrase.Add(New Chunk("", V1))
                'End If
                'cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                'cell.SetLeading(12, 0)
                'cell.PaddingBottom = 5
                'cell.PaddingRight = 15
                ''cell.Border = Rectangle.RIGHT_BORDER
                'cell.PaddingLeft = 2
                'dtblAirMeetDetail.AddCell(cell)
                'End If
                '*** Gust Details 14
                phrase = New Phrase()
                phrase.Add(New Chunk("Remarks/Special Request  ", Caption1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                'cell.Border = Rectangle.LEFT_BORDER
                'cell.Border = Rectangle.BOTTOM_BORDER
                cell.PaddingRight = 15
                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)



                phrase = New Phrase()
                phrase.Add(New Chunk(dtService.Rows(i)("CustomerRemark").ToString(), V1))
                cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                cell.SetLeading(12, 0)
                cell.PaddingBottom = 5
                cell.PaddingRight = 15
                'cell.Border = Rectangle.RIGHT_BORDER
                'cell.Border = Rectangle.BOTTOM_BORDER

                cell.PaddingLeft = 2
                dtblAirMeetDetail.AddCell(cell)

                cell = New PdfPCell(dtblAirMeetDetail)
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                'cell.Border = Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                'cell.PaddingTop = 2
                'cell.PaddingBottom = 2
                'cell.PaddingLeft = 4
                'cell.PaddingRight = 2
                cell.Colspan = 4
                dtblLogo.AddCell(cell)

                'document.Add(dtblAirMeetDetail)

            Next

        Catch ex As Exception

        End Try
        Return dtblLogo
    End Function

End Class
