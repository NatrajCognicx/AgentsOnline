Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Linq

Public Class ClsBookingHotelPdf

#Region "Global Variable"
    Dim NormalFont As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
    Dim titleColor As BaseColor = New BaseColor(214, 214, 214)
    Dim inputDataColor As BaseColor = New BaseColor(51, 51, 255)
    Dim NormalFontBlue As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, inputDataColor)
    Dim NormalFontBoldBlue As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, inputDataColor)
#End Region

#Region "Public Sub BookingHotelPrint(ByVal requestID As String, ByVal partyCode As String, ByVal amended As Integer, ByVal cancelled As Integer, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, Optional ByVal fileName As String = "")"
    Public Sub BookingHotelPrint(ByVal requestID As String, ByVal partyCode As String, ByVal amended As Integer, ByVal cancelled As Integer, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, Optional ByVal fileName As String = "")
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            mySqlCmd = New SqlCommand("sp_booking_hotel_print", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@RequestID", SqlDbType.VarChar, 20)).Value = requestID
            mySqlCmd.Parameters.Add(New SqlParameter("@PartyCode", SqlDbType.VarChar, 20)).Value = partyCode
            mySqlCmd.Parameters.Add(New SqlParameter("@Amended", SqlDbType.Int)).Value = amended
            mySqlCmd.Parameters.Add(New SqlParameter("@Cancelled", SqlDbType.Int)).Value = cancelled
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            ds1 = ds
            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim guestDt As DataTable = ds.Tables(3)
            Dim inventoryDt As DataTable = ds.Tables(4)
            Dim contactDt As DataTable = ds.Tables(5)
            Dim splEventDt As DataTable = ds.Tables(6)
            clsDBConnect.dbConnectionClose(sqlConn)
            Dim remainingPageSpace As Single
            If headerDt.Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 23.0F, 23.0F, 20.0F, 35.0F)
                Dim documentWidth As Single = 550.0F
                Dim NormalFontRed As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.RED)
                Dim NormalFontBoldRed As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.RED)
                'Dim inputDataColor As BaseColor = New BaseColor(0, 112, 192)
                Dim confirmBackColor As BaseColor = New BaseColor(192, 0, 0)
                Dim confirmForeColor As BaseColor = New BaseColor(255, 255, 255)
                Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 12, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 16, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
                Dim confirmFontWhite As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, confirmForeColor)
                Dim confirmFontBoldWhite As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, confirmForeColor)
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
                    'Company Logo
                    If (headerDr("div_code") = "01") Then
                        cell = ImageCell("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_CENTER)
                    Else
                        cell = ImageCell("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_LEFT)
                    End If
                    tblLogo.AddCell(cell)
                    'Company Address
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("address1")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("Fax : " & Convert.ToString(headerDr("fax")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("Tel : " & Convert.ToString(headerDr("tel")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("E-mail : " & Convert.ToString(headerDr("email")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("Website : " & Convert.ToString(headerDr("website")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    cell.PaddingBottom = 3.0F
                    tblLogo.AddCell(cell)
                    table.AddCell(tblLogo)

                    Dim tblClient As PdfPTable = New PdfPTable(1)
                    tblClient.SetWidths(New Single() {1.0F})
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("PartyName")), TitleFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    cell.SetLeading(11, 0)
                    cell.PaddingBottom = 4
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Fax : " & Convert.ToString(headerDr("fax1")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Tel : " & Convert.ToString(headerDr("Tel1")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Email : " & Convert.ToString(headerDr("email1")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.SetLeading(12, 0)
                    tblClient.AddCell(cell)
                    phrase = New Phrase()
                    phrase.Add(New Chunk("Attn. : " & Convert.ToString(headerDr("Contact1")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 1, False)
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
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 3.0F
                    tblTitle.AddCell(cell)
                    tblTitle.SpacingBefore = 7
                    document.Add(tblTitle)

                    Dim tblInv As PdfPTable = New PdfPTable(2)
                    tblInv.SetWidths(New Single() {0.55F, 0.45F})
                    tblInv.TotalWidth = 250.0F
                    tblInv.SplitRows = False
                    tblInv.LockedWidth = True
                    tblInv.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    tblInv.KeepTogether = True
                    Dim arrTitle() As String = {"Booking Reference No.", headerDr("requestID").ToString(), "Agent Ref. No.", headerDr("agentRef"), "Booking Request Date", headerDr("requestDate")}
                    For i = 0 To 5
                        phrase = New Phrase()
                        phrase.Add(New Chunk(arrTitle(i), NormalFontBold))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Padding = 3
                        tblInv.AddCell(cell)
                    Next
                    tblInv.SpacingBefore = 9.0F
                    document.Add(tblInv)
                    '------------Page Header-----------------------------
                    Dim tblHeader As PdfPTable = New PdfPTable(6)
                    tblHeader.SetWidths(New Single() {0.18F, 0.14F, 0.15F, 0.16F, 0.25F, 0.12F})
                    tblHeader.TotalWidth = documentWidth
                    tblHeader.LockedWidth = True
                    tblHeader.SplitRows = False
                    Dim arrHeader() As String = {"Booking Ref. No.", headerDr("requestID").ToString(), "Agent Ref. No.", headerDr("agentRef"), "Booking Request Date", headerDr("requestDate")}
                    For i = 0 To 5
                        phrase = New Phrase()
                        phrase.Add(New Chunk(arrHeader(i), NormalFontBold))
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
                        tblHeader.AddCell(cell)
                    Next
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblHeader, printMode)
                    '-------------------******------------------------------------------------------
                    If hotelDt.Rows.Count > 0 Then
                        Dim RoomCnt As Integer = 0
                        For Each hotelDr As DataRow In hotelDt.Rows
                            RoomCnt = RoomCnt + 1
                            Dim tblServ As PdfPTable = New PdfPTable(6)
                            tblServ.TotalWidth = documentWidth
                            tblServ.LockedWidth = True
                            tblServ.SetWidths(New Single() {0.24F, 0.15F, 0.15F, 0.15F, 0.14F, 0.17F})
                            tblServ.Complete = False
                            tblServ.HeaderRows = 1
                            tblServ.SplitRows = False
                            phrase = New Phrase()
                            phrase.Add(New Chunk("ROOM " & RoomCnt & " DETAILS", TitleFontBold))  'Convert.ToString(hotelDr("RoomNo"))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 6, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 4.0F
                            cell.PaddingTop = 1.0F
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Check In", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("CheckIn")), NormalFontBoldBlue))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Check Out", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("CheckOut")), NormalFontBoldBlue))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Nights", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("Nights")) & " Nights", NormalFontBoldBlue))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Meal Plan", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("mealCode")), NormalFontBoldBlue))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 5, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Room Type", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("rmTypName")), NormalFontBoldBlue))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 5, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Occupancy", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = titleColor
                            tblServ.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("roomDetails")) & " (" & Convert.ToString(hotelDr("occupancy")) & ")", NormalFontBoldBlue))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 5, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblServ.AddCell(cell)

                            tblServ.Complete = True
                            tblServ.SpacingBefore = 10
                            remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                            If remainingPageSpace < 48 Then document.NewPage()
                            document.Add(tblServ)

                            Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                            Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
                            Dim filterRows = (From n In guestDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n).ToList()
                            Dim filterGuest As New DataTable
                            If (filterRows.Count > 0) Then filterGuest = filterRows.CopyToDataTable()
                            If filterGuest.Rows.Count > 0 Then
                                Dim tblGuest As PdfPTable = New PdfPTable(7)
                                tblGuest.TotalWidth = documentWidth
                                tblGuest.LockedWidth = True
                                tblGuest.SetWidths(New Single() {0.32F, 0.12F, 0.11F, 0.11F, 0.12F, 0.11F, 0.11F})
                                tblGuest.Complete = False
                                tblGuest.HeaderRows = 1
                                tblGuest.SplitRows = False
                                Dim arrGuest() As String = {"Guest Names", String.Format("Arrival{0}Date", vbCrLf), "Arrival Flight", "Arrival Time", "Departure Date", "Departure Flight", "Departure Time"}
                                For i = 0 To 6
                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(arrGuest(i), NormalFont))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.BackgroundColor = titleColor
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)
                                Next
                                Dim guestval As String = ""
                                For Each guestDr In filterGuest.Rows
                                    If Not String.IsNullOrEmpty(guestDr("childage")) And Convert.ToString(guestDr("childage")) <> "0.00" Then
                                        guestval = Convert.ToString(guestDr("guestName")) + " (" + Convert.ToString(guestDr("childage")) + " yrs)"
                                    Else
                                        guestval = Convert.ToString(guestDr("guestName"))
                                    End If
                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(guestval, NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(guestDr("arrDate")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(guestDr("arrFlightCode")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(guestDr("arrFlightTime")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(guestDr("depDate")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(guestDr("depFlightCode")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(guestDr("depFlightTime")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.PaddingBottom = 4.0F
                                    cell.PaddingTop = 1.0F
                                    tblGuest.AddCell(cell)
                                Next
                                tblGuest.Complete = True
                                remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                                If remainingPageSpace < 48 Then document.NewPage()
                                document.Add(tblGuest)
                            End If

                            Dim FinalTotal As Decimal = 0.0
                            Dim filterTariffRows = (From n In tariffDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n Order By Convert.ToDateTime(n.Field(Of String)("fromdate")) Ascending).ToList()
                            Dim filterTariffDt As New DataTable
                            If (filterTariffRows.Count > 0) Then filterTariffDt = filterTariffRows.CopyToDataTable()
                            If filterTariffDt.Rows.Count > 0 Then
                                Dim tblTariff As PdfPTable = New PdfPTable(6) '6
                                tblTariff.TotalWidth = documentWidth
                                tblTariff.LockedWidth = True
                                'tblTariff.SetWidths(New Single() {0.39F, 0.12F, 0.12F, 0.12F, 0.11F, 0.14F}) '{0.24F, 0.15F, 0.15F, 0.15F, 0.14F, 0.17F})
                                tblTariff.SetWidths(New Single() {0.24F, 0.15F, 0.15F, 0.15F, 0.14F, 0.17F}) '{0.55F, 0.15F, 0.15F, 0.15F}) 
                                tblTariff.Complete = False
                                tblTariff.HeaderRows = 1
                                tblTariff.SplitRows = False
                                Dim arrServ() As String = {"Offer Code", "From", "To", "Room Nights", "Per Night Rate (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")", "Total (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")"}
                                '  Dim arrServ() As String = {"Offer Code", "From", "To", "Room Nights"}
                                For i = 0 To 5
                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(arrServ(i), NormalFont))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    cell.BackgroundColor = titleColor
                                    tblTariff.AddCell(cell)
                                Next
                                Dim totalNights As Integer = 0
                                Dim totalValue As Decimal = 0.0
                                For Each tariffDr As DataRow In filterTariffDt.Rows
                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(tariffDr("bookingCode")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    tblTariff.AddCell(cell)
                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(tariffDr("fromDate")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    tblTariff.AddCell(cell)
                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(tariffDr("toDate")), NormalFontBoldBlue))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    tblTariff.AddCell(cell)

                  

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(tariffDr("nights")), NormalFont))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    cell.PaddingRight = 4.0F
                                    tblTariff.AddCell(cell)


                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(tariffDr("costPrice")), NormalFont))     '   // Hide value temporarily for VAT implementation
                                    phrase.Add(New Chunk("", NormalFont))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    cell.PaddingRight = 4.0F
                                    tblTariff.AddCell(cell)

                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Convert.ToString(tariffDr("costValue")), NormalFont))     '   // Hide value temporarily for VAT implementation
                                    phrase.Add(New Chunk("", NormalFont))
                                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.SetLeading(12, 0)
                                    cell.PaddingTop = 2.0F
                                    cell.PaddingBottom = 5.0F
                                    cell.PaddingRight = 4.0F
                                    tblTariff.AddCell(cell)

                                    totalNights = totalNights + Convert.ToInt32(tariffDr("nights"))
                                    totalValue = totalValue + Convert.ToDecimal(tariffDr("costValue"))
                                Next
                                'phrase = New Phrase()
                                'phrase.Add(New Chunk("Total Nights / Rate (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")", NormalFontBold))
                                '' phrase.Add(New Chunk("Total Nights", NormalFontBold))
                                'cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 3, True) '4
                                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                'cell.SetLeading(12, 0)
                                'cell.PaddingTop = 2.0F
                                'cell.PaddingBottom = 5.0F
                                'tblTariff.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(totalNights), NormalFontBold))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingRight = 4.0F
                                cell.BackgroundColor = titleColor
                                tblTariff.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(totalValue), NormalFontBold))     ' // Hide value temporarily for VAT implementation
                                phrase.Add(New Chunk("", NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingRight = 4.0F
                                cell.BackgroundColor = titleColor
                                tblTariff.AddCell(cell)

                                tblTariff.Complete = True
                                FinalTotal = FinalTotal + totalValue
                                remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                                If remainingPageSpace < 72 Then document.NewPage()
                                document.Add(tblTariff)
                            End If

                            '--------------Special Event-------------------------------------------
                            Dim filterSplEventRows = (From n In splEventDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n).ToList()
                            Dim filterSplEventDt As New DataTable
                            If (filterSplEventRows.Count > 0) Then filterSplEventDt = filterSplEventRows.CopyToDataTable()
                            If filterSplEventDt.Rows.Count > 0 Then
                                Dim tblSplEvent As New PdfPTable(4) '6
                                Dim currCode As String = Convert.ToString(headerDr("costCurrCode"))
                                SpecialEvents(filterSplEventDt, documentWidth, currCode, FinalTotal, tblSplEvent)
                                tblSplEvent.SpacingBefore = 7
                                remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                                If remainingPageSpace < 48 Then document.NewPage()
                                document.Add(tblSplEvent)
                            End If

                            '--------------Final Total-------------------------------------------
                            'Dim tblFinalTotal As PdfPTable = New PdfPTable(2)
                            'tblFinalTotal.TotalWidth = documentWidth
                            'tblFinalTotal.LockedWidth = True
                            'tblFinalTotal.SetWidths(New Single() {0.83F, 0.17F})
                            'tblFinalTotal.KeepTogether = True
                            'tblFinalTotal.SplitRows = False
                            'phrase = New Phrase()
                            'phrase.Add(New Chunk("Total Amount (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")", NormalFontBold))
                            'cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            'cell.PaddingBottom = 4.0F
                            'cell.MinimumHeight = 20.0F
                            'tblFinalTotal.AddCell(cell)

                            'phrase = New Phrase()
                            ''phrase.Add(New Chunk(Convert.ToString(FinalTotal), NormalFontBold))       // Hide value temporarily for VAT implementation
                            'phrase.Add(New Chunk("", NormalFont))
                            'cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            'cell.PaddingBottom = 4.0F
                            'cell.PaddingRight = 4.0F
                            'cell.BackgroundColor = titleColor
                            'tblFinalTotal.AddCell(cell)
                            'tblFinalTotal.SpacingBefore = 7
                            'remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                            'If remainingPageSpace < 48 Then document.NewPage()
                            'document.Add(tblFinalTotal)

                            '--------------Note-------------------------------------------
                            Dim tblNote As PdfPTable = New PdfPTable(1)
                            tblNote.TotalWidth = documentWidth
                            tblNote.LockedWidth = True
                            tblNote.SetWidths(New Single() {1.0F})
                            tblNote.Complete = False
                            tblNote.SplitRows = False
                            phrase = New Phrase()
                            phrase.Add(New Chunk("Note :" & vbCrLf, TitleFontBoldUnderLine))
                            phrase.Add(New Chunk("Payment will be made as per above booking and agreed conditions as per contract." + vbCrLf, NormalFontBoldRed))
                            phrase.Add(New Chunk("Tourism Dirham & All Extras to be collected from the Guest Directly.", NormalFontBoldRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(18, 0)
                            cell.PaddingLeft = 10.0F
                            cell.PaddingTop = 3.0F
                            cell.PaddingBottom = 8.0F
                            tblNote.AddCell(cell)

                            tblNote.Complete = True
                            tblNote.SpacingBefore = 9
                            document.Add(tblNote)

                            '--------------Bill Instruction-------------------------------------------
                            Dim tblBill As PdfPTable = New PdfPTable(2)
                            tblBill.TotalWidth = documentWidth
                            tblBill.LockedWidth = True
                            tblBill.SetWidths(New Single() {0.07, 0.93F})
                            tblBill.Complete = False
                            tblBill.SplitRows = False
                            tblBill.KeepTogether = True

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Billing Instruction For VAT Compliance", TitleFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 2, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 6.0F
                            cell.BackgroundColor = New BaseColor(232, 254, 222)
                            tblBill.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("1", NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 3.0F
                            tblBill.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Invoice to be under the name of ", NormalFont))
                            If (headerDr("div_code") = "01") Then   'Royal Park
                                phrase.Add(New Chunk("Mohd Al Humaidi Computer LLC", NormalFontBold))
                            Else 'Royal Gulf
                                phrase.Add(New Chunk("Mohd Al Humaidi Computer  LLC", NormalFontBold))
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 1.0F
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 5.0F
                            tblBill.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("2", NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 3.0F
                            tblBill.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Please mention our ", NormalFont))
                            If (headerDr("div_code") = "01") Then   'Royal Park
                                phrase.Add(New Chunk("TRN:100227539200003 ", NormalFontBold))
                            Else 'Royal Gulf
                                phrase.Add(New Chunk("TRN:100379048000003 ", NormalFontBold))
                            End If
                            phrase.Add(New Chunk("in addition to your TRN number", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 1.0F
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 5.0F
                            tblBill.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("3", NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 3.0F
                            tblBill.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Please ", NormalFont))
                            phrase.Add(New Chunk("specify the VAT amount charged to us separately ", NormalFontBold))
                            phrase.Add(New Chunk("on the Invoice", NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingTop = 1.0F
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 5.0F
                            tblBill.AddCell(cell)

                            tblBill.Complete = True
                            tblBill.SpacingBefore = 9
                            document.Add(tblBill)

                            '--------------Confirm-------------------------------------------
                            Dim tblConfirm As PdfPTable = New PdfPTable(3)
                            tblConfirm.TotalWidth = documentWidth
                            tblConfirm.LockedWidth = True
                            tblConfirm.SetWidths(New Single() {0.34F, 0.33F, 0.33F})
                            tblConfirm.Complete = False
                            tblConfirm.SplitRows = False
                            tblConfirm.KeepTogether = True
                            phrase = New Phrase()
                            phrase.Add(New Chunk("_________________________________________________________________________________________", NormalFontRed))
                            phrase.Add(New Chunk("_________________________________________________________________________________________", NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 3, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(2, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblConfirm.AddCell(cell)
                            phrase = New Phrase()
                            phrase.Add(New Chunk("For Hotel Use Only", NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 3, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblConfirm.AddCell(cell)
                            Dim arrConfirm() As String = {"Confirmation No", "Confirmed By", "Confirmation Date"}
                            For i = 0 To 2
                                phrase = New Phrase()
                                phrase.Add(New Chunk(arrConfirm(i), confirmFontBoldWhite))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.BackgroundColor = confirmBackColor
                                tblConfirm.AddCell(cell)
                            Next
                            Dim confirmStr As String = ""
                            If Not String.IsNullOrEmpty(Convert.ToString(hotelDr("hotelConfNo"))) Then
                                confirmStr = Convert.ToString(hotelDr("hotelConfNo"))
                            Else
                                confirmStr = " "
                            End If
                            phrase = New Phrase()
                            phrase.Add(New Chunk(confirmStr, TitleFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblConfirm.AddCell(cell)
                            If Not String.IsNullOrEmpty(Convert.ToString(hotelDr("confirmBy"))) Then
                                confirmStr = Convert.ToString(hotelDr("confirmBy"))
                            Else
                                confirmStr = " "
                            End If
                            phrase = New Phrase()
                            phrase.Add(New Chunk(confirmStr, TitleFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblConfirm.AddCell(cell)
                            If Not String.IsNullOrEmpty(Convert.ToString(hotelDr("confirmDate"))) Then
                                confirmStr = Convert.ToString(hotelDr("confirmDate"))
                            Else
                                confirmStr = " "
                            End If
                            phrase = New Phrase()
                            phrase.Add(New Chunk(confirmStr, TitleFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblConfirm.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Remarks (If Any)", confirmFontBoldWhite))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 3, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = confirmBackColor
                            tblConfirm.AddCell(cell)

                            If Not String.IsNullOrEmpty(Convert.ToString(hotelDr("hotelRemarks"))) Then
                                confirmStr = Convert.ToString(hotelDr("hotelRemarks"))
                            Else
                                confirmStr = " "
                            End If
                            phrase = New Phrase()
                            phrase.Add(New Chunk(confirmStr, NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 3, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingLeft = 10.0F
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            tblConfirm.AddCell(cell)

                            tblConfirm.Complete = True
                            tblConfirm.SpacingBefore = 9
                            document.Add(tblConfirm)
                        Next

                        If inventoryDt.Rows.Count > 0 Then
                            Dim tblInventory As PdfPTable = New PdfPTable(7)
                            tblInventory.TotalWidth = documentWidth
                            tblInventory.LockedWidth = True
                            tblInventory.SetWidths(New Single() {0.12F, 0.47F, 0.1F, 0.1F, 0.07F, 0.07F, 0.07F})
                            tblInventory.Complete = False
                            tblInventory.HeaderRows = 1
                            tblInventory.SplitRows = False
                            Dim arrInventory() As String = {"Dates", "Room Type", "Free Sale", "Allocation", "Xtra", "B2B", "Swap"}
                            For i = 0 To 6
                                phrase = New Phrase()
                                phrase.Add(New Chunk(arrInventory(i), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.BackgroundColor = titleColor
                                tblInventory.AddCell(cell)
                            Next
                            Dim str As String = ""
                            For Each inventoryDr As DataRow In inventoryDt.Rows
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToDateTime(inventoryDr("inventorydate")).ToString("dd/MM/yyyy"), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                tblInventory.AddCell(cell)
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(inventoryDr("rmTypName")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                tblInventory.AddCell(cell)
                                If String.IsNullOrEmpty(Convert.ToString(inventoryDr("freesale"))) Or Convert.ToString(inventoryDr("freesale")) = "0" Then
                                    str = ""
                                Else
                                    str = Convert.ToString(inventoryDr("freesale"))
                                End If
                                phrase = New Phrase()
                                phrase.Add(New Chunk(str, NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                tblInventory.AddCell(cell)
                                If String.IsNullOrEmpty(Convert.ToString(inventoryDr("Allocation"))) Or Convert.ToString(inventoryDr("Allocation")) = "0" Then
                                    str = ""
                                Else
                                    str = Convert.ToString(inventoryDr("Allocation"))
                                End If
                                phrase = New Phrase()
                                phrase.Add(New Chunk(str, NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingRight = 7.0F
                                tblInventory.AddCell(cell)
                                If String.IsNullOrEmpty(Convert.ToString(inventoryDr("xtraAllocation"))) Or Convert.ToString(inventoryDr("xtraAllocation")) = "0" Then
                                    str = ""
                                Else
                                    str = Convert.ToString(inventoryDr("xtraAllocation"))
                                End If
                                phrase = New Phrase()
                                phrase.Add(New Chunk(str, NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingRight = 7.0F
                                tblInventory.AddCell(cell)
                                If String.IsNullOrEmpty(Convert.ToString(inventoryDr("B2B"))) Or Convert.ToString(inventoryDr("B2B")) = "0" Then
                                    str = ""
                                Else
                                    str = Convert.ToString(inventoryDr("B2B"))
                                End If
                                phrase = New Phrase()
                                phrase.Add(New Chunk(str, NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingRight = 7.0F
                                tblInventory.AddCell(cell)
                                If String.IsNullOrEmpty(Convert.ToString(inventoryDr("swap"))) Or Convert.ToString(inventoryDr("swap")) = "0" Then
                                    str = ""
                                Else
                                    str = Convert.ToString(inventoryDr("swap"))
                                End If
                                phrase = New Phrase()
                                phrase.Add(New Chunk(str, NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.SetLeading(12, 0)
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingRight = 7.0F
                                tblInventory.AddCell(cell)
                            Next
                            tblInventory.Complete = True
                            tblInventory.SpacingBefore = 9
                            remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                            If remainingPageSpace < 48 Then document.NewPage()
                            document.Add(tblInventory)
                        End If


                        Dim tblFooter As New PdfPTable(1)
                        tblFooter.TotalWidth = documentWidth
                        tblFooter.LockedWidth = True
                        tblFooter.Complete = False
                        tblFooter.SetWidths({1.0F})
                        tblFooter.KeepTogether = True
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Thanks and Best Regards", NormalFont))
                        cell = New PdfPCell(phrase)
                        cell.BorderWidth = 0.7F
                        cell.Border = Rectangle.NO_BORDER
                        cell.PaddingTop = 6.0F
                        cell.PaddingLeft = 15.0F
                        tblFooter.AddCell(cell)
                        If contactDt.Rows.Count > 0 Then
                            Dim contractDr As DataRow = contactDt.Rows(0)
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(contractDr("salesPerson")) + "<" + Convert.ToString(contractDr("salesemail")) + ">" + vbCrLf + "DESTINATION SPECIALIST", NormalFont))
                            cell = New PdfPCell(phrase)
                            cell.BorderWidth = 0.7F
                            cell.BorderColor = BaseColor.WHITE
                            cell.SetLeading(15, 0)
                            cell.PaddingTop = 35.0F
                            tblFooter.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Mobile No - " + Convert.ToString(contractDr("salesmobile")), NormalFont))
                            cell = New PdfPCell(phrase)
                            cell.BorderWidth = 0.7F
                            cell.BorderColor = BaseColor.WHITE
                            cell.SetLeading(15, 0)
                            cell.PaddingTop = 14.0F
                            tblFooter.AddCell(cell)
                            tblFooter.KeepRowsTogether(0, 2)
                        End If
                        'phrase = New Phrase()
                        'phrase.Add(New Chunk("Any Discrepancy on the above Invoice to be revert back within 72 hours from the date of Confirmation or else treated as final", NormalFont))
                        'cell = New PdfPCell(phrase)
                        'cell.BorderWidth = 0.7F
                        'cell.BorderColor = BaseColor.WHITE
                        'cell.SetLeading(15, 0)
                        'cell.PaddingTop = 3.0F
                        'tblFooter.AddCell(cell)
                        tblFooter.Complete = True
                        document.Add(tblFooter)
                    End If
                    document.AddTitle(Convert.ToString(headerDr("printHeader")) & "-" & requestID)
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
            Else
                Throw New Exception("There is no rows in header table")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef FinalTotal As Decimal, ByRef tblSplEvent As PdfPTable)"
    Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef FinalTotal As Decimal, ByRef tblSplEvent As PdfPTable)
        Dim phrase As New Phrase
        Dim cell As New PdfPCell
        Dim arrSplEvent() As String = {"Special Events", "Date of Event", "Units/ Pax", "Type of Units/ Pax", "Rate per Units/Pax", "Charges (" & CurrCode & ")"}
        tblSplEvent.TotalWidth = documentWidth
        tblSplEvent.LockedWidth = True
        'tblSplEvent.SetWidths(New Single() {0.42F, 0.12F, 0.09F, 0.12F, 0.11F, 0.14F})
        tblSplEvent.SetWidths(New Single() {0.55F, 0.15F, 0.15F, 0.15F})
        tblSplEvent.Complete = False
        tblSplEvent.HeaderRows = 1
        tblSplEvent.SplitRows = False
        For i = 0 To 3 '5
            phrase = New Phrase()
            phrase.Add(New Chunk(arrSplEvent(i), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 4.0F
            cell.PaddingTop = 1.0F
            cell.BackgroundColor = titleColor
            tblSplEvent.AddCell(cell)
        Next
        Dim totalCost As Decimal = 0.0
        For Each splEventDr As DataRow In splEventDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventName")), NormalFontBoldBlue))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingLeft = 3.0F
            cell.PaddingBottom = 4.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventDate")), NormalFontBoldBlue))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("noOfPax")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("paxType")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            tblSplEvent.AddCell(cell)

            'phrase = New Phrase()
            ''phrase.Add(New Chunk(Convert.ToString(splEventDr("paxCost")), NormalFont))    // Hide value temporarily for VAT implementation
            'phrase.Add(New Chunk("", NormalFont))
            'cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            'cell.PaddingBottom = 2.0F
            'cell.PaddingRight = 4.0F
            'tblSplEvent.AddCell(cell)

            'phrase = New Phrase()
            ''phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventCostValue")), NormalFont))     // Hide value temporarily for VAT implementation
            'phrase.Add(New Chunk("", NormalFont))
            'cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            'cell.PaddingBottom = 2.0F
            'cell.PaddingRight = 4.0F
            'tblSplEvent.AddCell(cell)

            totalCost = totalCost + splEventDr("splEventCostValue")
        Next
        FinalTotal = FinalTotal + totalCost
        tblSplEvent.Complete = True
    End Sub
#End Region

#Region "Private Shared Sub DrawLine(writer As PdfWriter, x1 As Single, y1 As Single, x2 As Single, y2 As Single, color As BaseColor)"
    Private Shared Sub DrawLine(ByVal writer As PdfWriter, ByVal x1 As Single, ByVal y1 As Single, ByVal x2 As Single, ByVal y2 As Single, ByVal color As BaseColor)
        Dim contentByte As PdfContentByte = writer.DirectContent
        contentByte.SetColorStroke(color)
        contentByte.MoveTo(x1, y1)
        contentByte.LineTo(x2, y2)
        contentByte.Stroke()
    End Sub
#End Region

#Region "Private Shared Function PhraseCell(phrase As Phrase, align As Integer, Cols As Integer, celBorder As Boolean, Optional celBottomBorder As String = ""None"") As PdfPCell"
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
#End Region

#Region "Private Shared Function ImageCell(path As String, scale As Single, align As Integer) As PdfPCell"
    Private Shared Function ImageCell(ByVal path As String, ByVal scale As Single, ByVal align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScalePercent(scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function
#End Region

End Class
