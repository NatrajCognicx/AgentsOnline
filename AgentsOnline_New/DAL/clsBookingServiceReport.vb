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

Public Class clsBookingServiceReport
    Dim objclsUtilities As New clsUtilities

    '*** Danny 25/09/2018
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
    Private Shared Function ImageCell_SR(ByVal path As String, ByVal scale As Single, ByVal align As Integer) As PdfPCell
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
#Region "Global Variable"

    Dim NormalFont_SR As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold_SR As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
    Dim NormalFontBoldRed_SR As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.RED)
    Dim NormalFontBoldTax_SR As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
    Dim titleColor_SR As BaseColor = New BaseColor(214, 214, 214)
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

    Private Function ReadPdfFile(ByVal fileName As String) As String
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

            'Dim myDataAdapter1 As New SqlDataAdapter

            'Dim mySqlCmd1 As New SqlCommand
            'Dim sqlConn1 As New SqlConnection
            'sqlConn1 = clsDBConnect.dbConnectionnew("strDBConnection")
            'mySqlCmd1 = New SqlCommand("SP_SelectServiceDetails", sqlConn1)

            'mySqlCmd1.CommandType = CommandType.StoredProcedure
            'mySqlCmd1.Parameters.Add(New SqlParameter("@RequestID", SqlDbType.VarChar, 20)).Value = requestID
            'myDataAdapter1.SelectCommand = mySqlCmd1
            'myDataAdapter1.Fill(ds_SR)

            'clsDBConnect.dbConnectionClose(sqlConn1)


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
                Or ds.Tables(1).Rows.Count = 0 _
                Or ds.Tables(8).Rows.Count = 0 _
                Or ds_SR.Tables(0).Rows.Count = 0 Then

                fileName = ""
                Return fileName
            End If


            '*** Check parameter for Need to attach.  Danny
            '*** Only Confirmed Booking need to create SR. Danny
            If ds_SR.Tables(0).Rows(0)("Attach").ToString() <> "Y" Or ds_SR.Tables(0).Rows(0)("ConfirmStatus").ToString() <> "1" Then
                fileName = ""
                Return fileName
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 40.0F, 20.0F, 10.0F, 10.0F)
                Dim documentWidth As Single = 480.0F
                Dim H1 As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                Dim Caption1 As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
                Dim Caption2 As Font = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK)
                Dim V1 As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)
                Dim F1 As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK)

                Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
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

                    '*** Image
                    Dim dtblLogo As PdfPTable = New PdfPTable(4)
                    dtblLogo.SetWidths(New Single() {0.2F, 0.3F, 0.2F, 0.3F})
                    dtblLogo.TotalWidth = documentWidth
                    dtblLogo.LockedWidth = True
                    If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                        Dim logoName As String = objclsUtilities.ExecuteQueryReturnStringValue("select logofilename from agentmast_whitelabel where agentcode ='" + objResParam.AgentCode.Trim() + "'")
                        cell = ImageCell_SR("~/Logos/" + logoName, 60.0F, PdfPCell.ALIGN_CENTER)
                    Else
                        If (ds.Tables(0).Rows(0)("div_code") = "01") Then
                            cell = ImageCell_SR("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_CENTER)
                        Else
                            cell = ImageCell_SR("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_CENTER)
                        End If
                    End If
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingTop = 50
                    cell.Colspan = 2
                    dtblLogo.AddCell(cell)

                    '*** Heading
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString("SERVICE VOUCHER"), H1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM
                    cell.Colspan = 2
                    cell.PaddingTop = 50
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Gap
                    phrase = New Phrase()
                    phrase.Add(New Chunk(" ", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 4
                    'cell.PaddingTop = 50
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 1
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Booking Ref: " & vbLf, Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(requestID) & vbLf, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Arrival: " + vbLf, Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(Format(CType(ds_SR.Tables(0).Rows(0)("minCheckin"), Date), "dd MMM yy")) & vbLf, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Booking Name: " + vbLf, Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk(ds.Tables(8).Rows(0)("guestname").ToString() + vbLf, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Consultant: " & vbLf, Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(ds_SR.Tables(0).Rows(0)("Consultant")) & vbLf, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Booking Status: ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 2
                    phrase = New Phrase()
                    phrase.Add(New Chunk("CF", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 3
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Issued: ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)

                    '*** Booking Details 4
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(Format(CType(ds.Tables(0).Rows(0)("requestdate"), Date), "dd MMM yy")) & vbLf, V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)


                    '*** Gap
                    phrase = New Phrase()
                    phrase.Add(New Chunk(" ", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 4
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    dtblLogo.AddCell(cell)

                    '*** Guests
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Guests: ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 2
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk("Agent: ", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 1
                    dtblLogo.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(ds.Tables(0).Rows(0)("agentName")), V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    dtblLogo.AddCell(cell)


                    phrase = New Phrase()

                    For SRi As Integer = 0 To ds.Tables(8).Rows.Count - 1
                        phrase.Add(New Chunk(ds.Tables(8).Rows(SRi)("guestname").ToString() + vbLf, V1))
                    Next
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 4
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    dtblLogo.AddCell(cell)

                    '*** Gap
                    phrase = New Phrase()
                    phrase.Add(New Chunk(" ", V1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Colspan = 4
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    dtblLogo.AddCell(cell)

                    document.Add(dtblLogo)


                    '*** Table Head
                    Dim dtblTable As PdfPTable = New PdfPTable(4)
                    dtblTable.TotalWidth = documentWidth
                    dtblTable.LockedWidth = True
                    dtblTable.SetWidths(New Single() {0.2F, 0.2F, 0.3F, 0.3F})

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Supplier", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    cell.PaddingBottom = 4
                    dtblTable.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Date", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    cell.PaddingBottom = 4
                    dtblTable.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Service", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 4
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    dtblTable.AddCell(cell)


                    phrase = New Phrase()
                    phrase.Add(New Chunk("Details", Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 4
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    dtblTable.AddCell(cell)


                    '*** Table Details=============================================
                    For SR_Trani As Integer = 0 To ds.Tables(3).Rows.Count - 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("division_master_des").ToString() + vbLf, Caption2))
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("tel").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)




                        phrase = New Phrase()
                        Dim TRDate As DateTime
                        TRDate = CType(ds.Tables(3).Rows(SR_Trani)("transferdate"), Date)
                        phrase.Add(New Chunk(Convert.ToString(Format(TRDate, "dd MMM yy")) & vbLf, F1))

                        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
                        Dim dayOfWeek As DayOfWeek = myCulture.Calendar.GetDayOfWeek(CType(ds.Tables(3).Rows(SR_Trani)("transferdate"), Date))
                        ' dayOfWeek.ToString() would return "Sunday" but it's an enum value,
                        ' the correct dayname can be retrieved via DateTimeFormat.
                        ' Following returns "Sonntag" for me since i'm in germany '
                        Dim dayName As String = myCulture.DateTimeFormat.GetDayName(dayOfWeek)

                        phrase.Add(New Chunk(Convert.ToString(dayName) & vbLf, V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(3).Rows(SR_Trani)("transfername").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(3).Rows(SR_Trani)("pickupdropoff").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)
                    Next

                    For SR_Trani As Integer = 0 To ds.Tables(4).Rows.Count - 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("division_master_des").ToString() + vbLf, Caption2))
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("tel").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)




                        phrase = New Phrase()
                        Dim TRDate As DateTime
                        TRDate = CType(ds.Tables(4).Rows(SR_Trani)("airportmadate"), Date)
                        phrase.Add(New Chunk(Convert.ToString(Format(TRDate, "dd MMM yy")) & vbLf, F1))

                        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
                        Dim dayOfWeek As DayOfWeek = myCulture.Calendar.GetDayOfWeek(CType(ds.Tables(4).Rows(SR_Trani)("airportmadate"), Date))
                        ' dayOfWeek.ToString() would return "Sunday" but it's an enum value,
                        ' the correct dayname can be retrieved via DateTimeFormat.
                        ' Following returns "Sonntag" for me since i'm in germany '
                        Dim dayName As String = myCulture.DateTimeFormat.GetDayName(dayOfWeek)

                        phrase.Add(New Chunk(Convert.ToString(dayName) & vbLf, V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(4).Rows(SR_Trani)("airportmaname").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("" + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)
                    Next


                    For SR_Trani As Integer = 0 To ds.Tables(5).Rows.Count - 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("division_master_des").ToString() + vbLf, Caption2))
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("tel").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)




                        phrase = New Phrase()
                        Dim TRDate As DateTime
                        TRDate = CType(ds.Tables(5).Rows(SR_Trani)("visadate"), Date)
                        phrase.Add(New Chunk(Convert.ToString(Format(TRDate, "dd MMM yy")) & vbLf, F1))

                        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
                        Dim dayOfWeek As DayOfWeek = myCulture.Calendar.GetDayOfWeek(CType(ds.Tables(5).Rows(SR_Trani)("visadate"), Date))
                        ' dayOfWeek.ToString() would return "Sunday" but it's an enum value,
                        ' the correct dayname can be retrieved via DateTimeFormat.
                        ' Following returns "Sonntag" for me since i'm in germany '
                        Dim dayName As String = myCulture.DateTimeFormat.GetDayName(dayOfWeek)

                        phrase.Add(New Chunk(Convert.ToString(dayName) & vbLf, V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(5).Rows(SR_Trani)("visaname").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("" + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)
                    Next

                    For SR_Trani As Integer = 0 To ds.Tables(6).Rows.Count - 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("division_master_des").ToString() + vbLf, Caption2))
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("tel").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)




                        phrase = New Phrase()
                        Dim TRDate As DateTime
                        TRDate = CType(ds.Tables(6).Rows(SR_Trani)("tourdate"), Date)
                        phrase.Add(New Chunk(Convert.ToString(Format(TRDate, "dd MMM yy")) & vbLf, F1))

                        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
                        Dim dayOfWeek As DayOfWeek = myCulture.Calendar.GetDayOfWeek(CType(ds.Tables(6).Rows(SR_Trani)("tourdate"), Date))
                        ' dayOfWeek.ToString() would return "Sunday" but it's an enum value,
                        ' the correct dayname can be retrieved via DateTimeFormat.
                        ' Following returns "Sonntag" for me since i'm in germany '
                        Dim dayName As String = myCulture.DateTimeFormat.GetDayName(dayOfWeek)

                        phrase.Add(New Chunk(Convert.ToString(dayName) & vbLf, V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(6).Rows(SR_Trani)("tourname").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("" + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)
                    Next

                    For SR_Trani As Integer = 0 To ds.Tables(7).Rows.Count - 1
                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("division_master_des").ToString() + vbLf, Caption2))
                        phrase.Add(New Chunk(ds.Tables(0).Rows(0)("tel").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)




                        phrase = New Phrase()
                        Dim TRDate As DateTime
                        TRDate = CType(ds.Tables(7).Rows(SR_Trani)("othdate"), Date)
                        phrase.Add(New Chunk(Convert.ToString(Format(TRDate, "dd MMM yy")) & vbLf, F1))

                        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
                        Dim dayOfWeek As DayOfWeek = myCulture.Calendar.GetDayOfWeek(CType(ds.Tables(7).Rows(SR_Trani)("othdate"), Date))
                        ' dayOfWeek.ToString() would return "Sunday" but it's an enum value,
                        ' the correct dayname can be retrieved via DateTimeFormat.
                        ' Following returns "Sonntag" for me since i'm in germany '
                        Dim dayName As String = myCulture.DateTimeFormat.GetDayName(dayOfWeek)

                        phrase.Add(New Chunk(Convert.ToString(dayName) & vbLf, V1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(ds.Tables(7).Rows(SR_Trani)("othername").ToString() + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("" + vbLf, F1))
                        cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                        'cell.Colspan = 4
                        'cell.PaddingTop = 50
                        'cell.SetLeading(12, 0)
                        cell.PaddingBottom = 4
                        dtblTable.AddCell(cell)
                    Next

                    '*** Gap
                    phrase = New Phrase()
                    phrase.Add(New Chunk("24-hours Emergency Mobile No. " + ds_SR.Tables(0).Rows(0)("24hoursEmergencyMobileNo").ToString(), Caption1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Colspan = 4
                    'cell.PaddingTop = 50
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    dtblTable.AddCell(cell)

                    document.Add(dtblTable)

                    Dim dtblFooter As PdfPTable = New PdfPTable(1)
                    dtblFooter.TotalWidth = documentWidth
                    dtblFooter.LockedWidth = True
                    dtblFooter.SetWidths(New Single() {1.0F})

                    '*** Footer File name and content Reading

                    Dim strColumbusPath As String = System.Web.HttpContext.Current.Server.MapPath("")
                    strColumbusPath = Path.GetDirectoryName(strColumbusPath)
                    strColumbusPath = strColumbusPath + "\" + objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=8")

                    Dim s As String = strColumbusPath + "\ExcelTemplates\" + ds_SR.Tables(0).Rows(0)("FooterFilename").ToString()
                    s = ReadPdfFile(s)
                    phrase = New Phrase()
                    phrase.Add(New Chunk(s, F1))
                    cell = PhraseCell_SR(phrase, PdfPCell.ALIGN_JUSTIFIED, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    'cell.Colspan = 4
                    'cell.PaddingTop = 50
                    'cell.SetLeading(12, 0)
                    cell.PaddingBottom = 4
                    dtblFooter.AddCell(cell)


                    document.Add(dtblFooter)


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
            Return ""
        End Try
    End Function

End Class
