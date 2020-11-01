Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class clsQuoteItineraryPdf

#Region "Global Variable"
    Dim objclsUtilities As New clsUtilities
    Dim NormalFont As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
    Dim NormalFontRed As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.RED)
    Dim NormalFontBoldRed As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.RED)
    Dim NormalFontBoldUnderLineRed As Font = FontFactory.GetFont("Arial", 11, Font.BOLD Or Font.UNDERLINE, BaseColor.RED)
    Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 12, Font.BOLD Or Font.UNDERLINE, BaseColor.RED)
    Dim titleColor As BaseColor = New BaseColor(214, 214, 214)
    Dim bookingBGcolor As BaseColor = New BaseColor(213, 219, 219)
    Dim serviceHeaderBGcolor As BaseColor = New BaseColor(253, 235, 208)
    Dim serviceBGcolor As BaseColor = New BaseColor(214, 219, 223)
    Dim itineraryBGcolor As BaseColor = New BaseColor(142, 68, 173)
    Dim itineraryFGfont As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, New BaseColor(202, 254, 51))
    Dim itineraryHLfont As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, New BaseColor(245, 176, 65))
    Dim noteBGColor As BaseColor = New BaseColor(125, 206, 160)
    Dim contactBGcolor As BaseColor = New BaseColor(246, 221, 204)
    Dim contactFGfont As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, New BaseColor(175, 96, 26))
    Dim contactFGfontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, New BaseColor(175, 96, 26))
    Dim contactFGfontNote As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL, New BaseColor(81, 46, 95))
#End Region

#Region "Public Sub GenerateQuoteItinerary(ByVal quoteID As String, ByRef bytes() As Byte)"
    Public Sub GenerateQuoteItinerary(ByVal quoteID As String, ByRef bytes() As Byte)
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As New SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            mySqlCmd = New SqlCommand("sp_Quote_Itinerary_Print", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteID", SqlDbType.VarChar, 20)).Value = quoteID
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim serviceDt As DataTable = ds.Tables(2)
            Dim CheckInOutDt As DataTable = ds.Tables(3)
            Dim ContactDt As DataTable = ds.Tables(4)
            clsDBConnect.dbConnectionClose(sqlConn)

            If headerDt.Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 35.0F)
                Dim documentWidth As Single = 550.0F
                Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                Dim WelcomeFont As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.YELLOW)
                Dim remainingPageSpace As Single
                Using memoryStream As New System.IO.MemoryStream()
                    Dim writer As PdfWriter
                    writer = PdfWriter.GetInstance(document, memoryStream)
                    document.Open()
                    Dim phrase As Phrase = Nothing
                    Dim cell As PdfPCell = Nothing

                    'Header Table
                    Dim headerDr As DataRow = headerDt.Rows(0)
                    Dim tblHeader As PdfPTable = New PdfPTable(3)
                    tblHeader.TotalWidth = documentWidth
                    tblHeader.LockedWidth = True
                    tblHeader.SetWidths(New Single() {0.33F, 0.34F, 0.33F})
                    tblHeader.Complete = False

                    'Left Image 
                    cell = HeaderImageCell("~/img/AtlantisEvening.jpg", 181, 95, PdfPCell.ALIGN_CENTER)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Rowspan = 2
                    tblHeader.AddCell(cell)

                    'Company Logo
                    If (headerDr("div_code") = "01") Then
                        cell = HeaderImageCell("~/Logos/mahce_logo.jpg", 187, 52, PdfPCell.ALIGN_CENTER)
                    Else
                        cell = HeaderImageCell("~/Logos/mahce_logo.jpg", 187, 52, PdfPCell.ALIGN_CENTER)
                    End If
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    tblHeader.AddCell(cell)

                    'Right Image 
                    cell = HeaderImageCell("~/img/WaterFountain.jpg", 181, 95, PdfPCell.ALIGN_CENTER)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.Rowspan = 2
                    tblHeader.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("WELCOME TO DUBAI", WelcomeFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.BackgroundColor = New BaseColor(70, 130, 180)
                    tblHeader.AddCell(cell)

                    tblHeader.Complete = True
                    document.Add(tblHeader)

                    Dim tblGreet As PdfPTable = New PdfPTable(1)
                    tblGreet.TotalWidth = documentWidth - 10
                    tblGreet.LockedWidth = True
                    tblGreet.SetWidths(New Single() {1.0F})
                    tblGreet.Complete = False
                    tblGreet.SplitRows = False

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Dear Guest", NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 10.0F
                    cell.PaddingBottom = 12.0F
                    tblGreet.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Greetings from " + Convert.ToString(headerDr("division_master_des")).Replace("L.L.C.", "") + "!!!", NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 12.0F
                    tblGreet.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("On behalf of " + Convert.ToString(headerDr("agentName")) + ", We wish you a pleasant and comfortable stay. Please find your itinerary during your stay in Dubai", NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 12.0F
                    tblGreet.AddCell(cell)

                    tblGreet.Complete = True
                    tblGreet.HorizontalAlignment = HorizontalAlign.Left
                    document.Add(tblGreet)
                    writer.PageEvent = New clsItineraryPageEvent("download")

                    '----Booking Details
                    If hotelDt.Rows.Count > 0 Then
                        Dim tblBooking As PdfPTable = New PdfPTable(2)
                        tblBooking.TotalWidth = documentWidth
                        tblBooking.LockedWidth = True
                        tblBooking.SetWidths(New Single() {0.4F, 0.6F})
                        tblBooking.Complete = False
                        tblBooking.SplitRows = False
                        Dim hotelCnt As Integer = 0
                        For Each hotelDr As DataRow In hotelDt.Rows
                            hotelCnt = hotelCnt + 1
                            If hotelCnt > 1 Then
                                cell = EmptyCell()
                                cell.Colspan = 2
                                cell.Border = Rectangle.BOTTOM_BORDER
                                tblBooking.AddCell(cell)
                            End If
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("ReferenceAgent")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            cell.BackgroundColor = bookingBGcolor
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 7.5F
                            tblBooking.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("ReferenceNumber")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.BackgroundColor = bookingBGcolor
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 5.0F
                            tblBooking.AddCell(cell)
                            For col As Integer = 2 To hotelDt.Columns.Count - 1
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDt.Columns(col).ColumnName), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                cell.BackgroundColor = bookingBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingLeft = 7.5F
                                tblBooking.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr(col)), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.BackgroundColor = bookingBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 5.0F
                                cell.PaddingLeft = 5.0F
                                tblBooking.AddCell(cell)
                            Next
                        Next
                        cell = EmptyCell()
                        cell.Colspan = 2
                        tblBooking.AddCell(cell)
                        tblBooking.Complete = True
                        document.Add(tblBooking)
                    End If

                    '-------Service-------------
                    If serviceDt.Rows.Count > 0 Then
                        Dim tblService As PdfPTable = New PdfPTable(2)
                        tblService.TotalWidth = documentWidth
                        tblService.LockedWidth = True
                        tblService.SetWidths(New Single() {0.4F, 0.6F})
                        tblService.Complete = False
                        tblService.SplitRows = False
                        For Each serviceDr As DataRow In serviceDt.Rows
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToDateTime(serviceDr("ServiceDate")).ToString("dd/MM/yyyy") + " - " + Convert.ToString(serviceDr("ServiceDay")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.Border = Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER
                            cell.BackgroundColor = serviceHeaderBGcolor
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 4.0F
                            cell.PaddingLeft = 5.0F
                            cell.Colspan = 2
                            tblService.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(serviceDr("ServiceNameTitle")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            cell.BackgroundColor = serviceBGcolor
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 4.0F
                            cell.PaddingLeft = 10.0F
                            tblService.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(serviceDr("ServiceName")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.BackgroundColor = serviceBGcolor
                            cell.PaddingTop = 2.0F
                            cell.PaddingBottom = 4.0F
                            cell.PaddingLeft = 5.0F
                            tblService.AddCell(cell)

                            If Convert.ToString(serviceDr("ServDetail1Title")) <> "" And Convert.ToString(serviceDr("ServDetail1")) <> "" Then
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(serviceDr("ServDetail1Title")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                cell.BackgroundColor = serviceBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 4.0F
                                cell.PaddingLeft = 10.0F
                                tblService.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(serviceDr("ServDetail1")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.BackgroundColor = serviceBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 4.0F
                                cell.PaddingLeft = 5.0F
                                tblService.AddCell(cell)
                            End If

                            If Convert.ToString(serviceDr("ServDetail2Title")) <> "" And Convert.ToString(serviceDr("ServDetail2")) <> "" Then
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(serviceDr("ServDetail2Title")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                cell.BackgroundColor = serviceBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 4.0F
                                cell.PaddingLeft = 10.0F
                                tblService.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(serviceDr("ServDetail2")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.BackgroundColor = serviceBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 4.0F
                                cell.PaddingLeft = 5.0F
                                tblService.AddCell(cell)
                            End If

                            If Convert.ToString(serviceDr("Note")) <> "" Then
                                phrase = New Phrase()
                                phrase.Add(New Chunk("Note : ", NormalFontBoldRed))
                                phrase.Add(New Chunk(Convert.ToString(serviceDr("Note")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.BackgroundColor = serviceBGcolor
                                cell.PaddingTop = 2.0F
                                cell.PaddingBottom = 4.0F
                                cell.PaddingLeft = 10.0F
                                cell.Colspan = 2
                                tblService.AddCell(cell)
                            End If

                            cell = EmptyCell()
                            cell.Colspan = 2
                            tblService.AddCell(cell)
                        Next
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        tblService.Complete = True
                        document.Add(tblService)
                    End If

                    Dim tblItineraryNote As PdfPTable = New PdfPTable(1)
                    tblItineraryNote.TotalWidth = documentWidth
                    tblItineraryNote.LockedWidth = True
                    tblItineraryNote.SetWidths(New Single() {1.0F})
                    tblItineraryNote.Complete = False
                    tblItineraryNote.SplitRows = False

                    phrase = New Phrase()
                    phrase.Add(New Chunk("We request you to go through above mentioned itinerary and ", itineraryFGfont))
                    phrase.Add(New Chunk("adhere to the above mentioned timings. ", itineraryHLfont))
                    phrase.Add(New Chunk("Failure in adhering to the same may result in ", itineraryFGfont))
                    phrase.Add(New Chunk("NO SHOW ", itineraryHLfont))
                    phrase.Add(New Chunk("on the excursions / attractions and ", itineraryFGfont))
                    phrase.Add(New Chunk("we would not be able to refund the same.", itineraryHLfont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.BackgroundColor = itineraryBGcolor
                    cell.PaddingTop = 2.0F
                    cell.PaddingBottom = 5.0F
                    cell.PaddingLeft = 7.5F
                    tblItineraryNote.AddCell(cell)

                    cell = EmptyCell()
                    tblItineraryNote.AddCell(cell)

                    tblItineraryNote.Complete = True
                    document.Add(tblItineraryNote)

                    '-----Check In / Out Policy
                    If CheckInOutDt.Rows.Count > 0 Then
                        Dim cntCheckInOut As Integer = 0
                        Dim tblCheckInOut As PdfPTable = New PdfPTable(2)
                        tblCheckInOut.TotalWidth = documentWidth - 30
                        tblCheckInOut.LockedWidth = True
                        tblCheckInOut.SetWidths(New Single() {0.05F, 0.95F})
                        tblCheckInOut.Complete = False
                        tblCheckInOut.SplitRows = False

                        phrase = New Phrase()
                        phrase.Add(New Chunk("CHECK IN & CHECK OUT NOTE" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.PaddingBottom = 5.0F
                        cell.BackgroundColor = noteBGColor
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        For Each CheckInOutDr As DataRow In CheckInOutDt.Rows
                            cntCheckInOut = cntCheckInOut + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntCheckInOut = CheckInOutDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            cell.PaddingLeft = 5.0F
                            cell.BackgroundColor = noteBGColor
                            tblCheckInOut.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(CheckInOutDr("Partyname")) + " -- ", NormalFontRed))
                            phrase.Add(New Chunk(Convert.ToString(CheckInOutDr("policyText")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            If cntCheckInOut = CheckInOutDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            cell.BackgroundColor = noteBGColor
                            tblCheckInOut.AddCell(cell)
                        Next

                        tblCheckInOut.Complete = True
                        tblCheckInOut.HorizontalAlignment = HorizontalAlign.Left
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblCheckInOut)
                    End If

                    Dim pickUpDt As New DataTable()
                    Dim pickUpText As New DataColumn("PolicyText", GetType(String))
                    pickUpDt.Columns.Add(pickUpText)
                    Dim arrPickUp() As String = New String() {"For Arrival Transfer:  90 Minutes after flight Landed",
                                                              "All Sharing tours / Departure: 10 Minutes",
                                                              "All Private Pick up: 20 Minutes"}
                    Dim pickupdrow As DataRow
                    For i = 0 To arrPickUp.GetUpperBound(0)
                        pickupdrow = pickUpDt.NewRow
                        pickupdrow("PolicyText") = arrPickUp(i)
                        pickUpDt.Rows.Add(pickupdrow)
                    Next
                    If pickUpDt.Rows.Count > 0 Then
                        Dim tblPickUp As PdfPTable = New PdfPTable(2)
                        tblPickUp.TotalWidth = documentWidth - 30
                        tblPickUp.LockedWidth = True
                        tblPickUp.SetWidths(New Single() {0.05F, 0.95F})
                        tblPickUp.Complete = False
                        tblPickUp.SplitRows = False

                        phrase = New Phrase()
                        phrase.Add(New Chunk("PICK UP  NOTE - MAXIMUM WAITING PERIOD" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.PaddingBottom = 5.0F
                        cell.BackgroundColor = noteBGColor
                        cell.Colspan = 2
                        tblPickUp.AddCell(cell)

                        For Each pickUpDr As DataRow In pickUpDt.Rows
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            cell.Border = Rectangle.LEFT_BORDER
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = noteBGColor
                            tblPickUp.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(pickUpDr("PolicyText")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.Border = Rectangle.RIGHT_BORDER
                            cell.PaddingBottom = 5.0F
                            cell.BackgroundColor = noteBGColor
                            tblPickUp.AddCell(cell)
                        Next

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Note:  If guest are not ready in the lobby as per the scheduled time mentioned in " &
                                             "the itinerary the guests will be treated as ", NormalFont))
                        phrase.Add(New Chunk("NO SHOW", NormalFontBoldRed))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER
                        cell.PaddingBottom = 8.0F
                        cell.BackgroundColor = noteBGColor
                        cell.Colspan = 2
                        tblPickUp.AddCell(cell)

                        tblPickUp.Complete = True
                        tblPickUp.SpacingBefore = 7.0F
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        tblPickUp.HorizontalAlignment = HorizontalAlign.Left
                        document.Add(tblPickUp)
                    End If

                    Dim tblCurrency As PdfPTable = New PdfPTable(1)
                    tblCurrency.TotalWidth = documentWidth - 30
                    tblCurrency.LockedWidth = True
                    tblCurrency.SetWidths(New Single() {1.0F})
                    tblCurrency.Complete = False
                    tblCurrency.SplitRows = False

                    phrase = New Phrase()
                    phrase.Add(New Chunk("As per DTCM guidelines ", NormalFont))
                    phrase.Add(New Chunk("‘TOURISM DIRHAM’ ", NormalFontBoldRed))
                    phrase.Add(New Chunk("a minimal charge applies to guests staying in all holiday accommodation including hotels," &
                                         "hotel apartments, guesthouses and holiday homes. 'TD' shall be settled by the guest / travel agent " &
                                         "directly with the hotel.", NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingBottom = 5.0F
                    cell.BackgroundColor = noteBGColor
                    tblCurrency.AddCell(cell)

                    tblCurrency.Complete = True
                    tblCurrency.SpacingBefore = 7.0F
                    remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                    If remainingPageSpace < 72 Then document.NewPage()
                    tblCurrency.HorizontalAlignment = HorizontalAlign.Left
                    document.Add(tblCurrency)

                    Dim tblContact As PdfPTable = New PdfPTable(3)
                    tblContact.TotalWidth = documentWidth
                    tblContact.LockedWidth = True
                    tblContact.SetWidths(New Single() {0.41F, 0.18F, 0.41F})
                    tblContact.Complete = False
                    tblContact.SplitRows = False

                    Dim agentContactDr As DataRow = ContactDt.Rows(0)
                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("division_master_des")) + vbCrLf + vbCrLf, contactFGfontBold))
                    phrase.Add(New Chunk(Convert.ToString(headerDr("address1")) + vbCrLf, contactFGfontBold))
                    phrase.Add(New Chunk("Contact Numbers:-" + vbCrLf, contactFGfontBold))
                    phrase.Add(New Chunk(Convert.ToString(agentContactDr("ContactNo")) + vbCrLf + vbCrLf, contactFGfontBold))
                    phrase.Add(New Chunk("Please feel free to contact us for any assistance you may require during your stay while in U.A.E.", contactFGfontNote))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.PaddingLeft = 7.5F
                    cell.PaddingTop = 5.0F
                    cell.PaddingBottom = 5.0F
                    cell.BackgroundColor = contactBGcolor
                    tblContact.AddCell(cell)

                    'Help Image
                    cell = HeaderImageCell("~/img/CustomerHelpdesk.jpg", 133, 111, PdfPCell.ALIGN_CENTER)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    tblContact.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("EMERGENCY CONTACT" + vbCrLf + vbCrLf, NormalFontBoldUnderLineRed))
                    If ContactDt.Rows.Count > 1 Then
                        Dim emergencyDt As DataTable = ContactDt.Copy()
                        emergencyDt.Rows(0).Delete()
                        emergencyDt.AcceptChanges()
                        For Each emergencyDr As DataRow In emergencyDt.Rows
                            phrase.Add(New Chunk(Convert.ToString(emergencyDr("contactPerson")).Trim + " : " + Convert.ToString(emergencyDr("contactNo")).Trim() + vbCrLf, NormalFontBoldRed))
                        Next
                    End If
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.PaddingLeft = 5.0F
                    cell.PaddingTop = 5.0F
                    cell.PaddingBottom = 5.0F
                    cell.BackgroundColor = contactBGcolor
                    tblContact.AddCell(cell)

                    tblContact.Complete = True
                    tblContact.SpacingBefore = 7.0F
                    remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                    If remainingPageSpace < 72 Then document.NewPage()
                    document.Add(tblContact)

                    Dim tblFooter As PdfPTable = New PdfPTable(1)
                    tblFooter.TotalWidth = documentWidth
                    tblFooter.LockedWidth = True
                    tblFooter.SetWidths(New Single() {1.0F})
                    tblFooter.Complete = False
                    tblFooter.SplitRows = False
                    tblFooter.KeepTogether = True

                    phrase = New Phrase()
                    phrase.Add(New Chunk("..................................................................................................................................................................................", NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    tblFooter.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("This is a computer generated document and may not bear signature of sender.", NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.UseVariableBorders = True
                    cell.PaddingTop = 2.0F
                    tblFooter.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("..................................................................................................................................................................................", NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    tblFooter.AddCell(cell)
                    tblFooter.Complete = True

                    cell = EmptyCell()
                    tblFooter.AddCell(cell)

                    tblFooter.Complete = True
                    tblFooter.SpacingBefore = 7.0F
                    document.Add(tblFooter)

                    document.AddTitle("Quote Itinerary-" & quoteID)
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
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Private Function EmptyCell() As PdfPCell"
    Private Function EmptyCell() As PdfPCell
        Dim Phrase As Phrase = New Phrase()
        Phrase.Add(New Chunk("" + vbCrLf, NormalFont))
        Dim cell As PdfPCell = PhraseCell(Phrase, PdfPCell.ALIGN_LEFT, 1, False)
        Return cell
    End Function
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
        image.ScaleAbsolute(scale, scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        Return cell
    End Function
#End Region

#Region "Private Shared Function HeaderImageCell(ByVal path As String, ByVal Widthscale As Single, HeightScale As Single, ByVal align As Integer) As PdfPCell"
    Private Shared Function HeaderImageCell(ByVal path As String, ByVal Widthscale As Single, HeightScale As Single, ByVal align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScaleToFit(Widthscale, HeightScale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP
        cell.HorizontalAlignment = align
        cell.BorderWidth = 1
        Return cell
    End Function
#End Region

End Class
