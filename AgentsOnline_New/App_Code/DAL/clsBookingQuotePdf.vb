Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Linq

Imports ClosedXML.Excel

Public Class clsBookingQuotePdf

#Region "Global Variable"
    Dim objclsUtilities As New clsUtilities
    Dim objBookingServicerVoucher As New clsBookingServicerVoucher
    Dim NormalFont As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
    Dim NormalFontRed As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.RED)
    Dim NormalFontBoldRed As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.RED)
    Dim NormalFontSubTitle As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, New BaseColor(6, 39, 133))
    Dim NormalFontBoldTax As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
    Dim titleColor As BaseColor = New BaseColor(214, 214, 214)

    Dim phrase As Phrase = Nothing
    Dim cell As PdfPCell = Nothing
    Dim documentWidth As Single = 550.0F
#End Region

#Region "Public Sub GenerateReport(ByVal quoteID As String, ByRef bytes() As Byte, ByVal objResParam As ReservationParameters)"

    Public Sub GenerateReport(ByVal quoteID As String, ByRef bytes() As Byte, ByVal objResParam As ReservationParameters, Optional ByVal printMode As String = "download", Optional ByVal fileName As String = "")
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As SqlCommand
            Dim mySqlCmd1 As SqlCommand
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

            'mySqlCmd1.CommandType = CommandType.StoredProcedure
            'mySqlCmd1.Parameters.Add(New SqlParameter("@requestid", SqlDbType.VarChar, 20)).Value = quoteID
            'mySqlCmd1.Parameters.Add(New SqlParameter("@quoteorbooking", SqlDbType.VarChar, 10)).Value = "0"
            'myDataAdapter1.SelectCommand = mySqlCmd1
            'myDataAdapter1.Fill(ds1)

            'Dim AdChBreakupDt As DataTable = ds1.Tables(8) 'changed by shahul           
            '*** Danny 20/10/2018
            'Dim parm(0) As SqlParameter
            'parm(0) = New SqlParameter("@RequestID", CType(quoteID, String))

            Dim ds_SR As New DataSet
            ds_SR = objclsUtilities.GetDataSet("SP_SelectServiceDetails", Nothing)

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

                Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 12, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
                Dim remainingPageSpace As Single
                Using memoryStream As New System.IO.MemoryStream()
                    Dim writer As PdfWriter
                    'changed by mohamed on 26/10/2020
                    'writer = PdfWriter.GetInstance(document, memoryStream)
                    If printMode = "download" Then
                        writer = PdfWriter.GetInstance(document, memoryStream)
                    Else
                        Dim path As String = System.Web.HttpContext.Current.Server.MapPath("~\SavedReports\") + fileName
                        writer = PdfWriter.GetInstance(document, New FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    End If

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
                    phrase.Add(New Chunk("Fax : " & Convert.ToString(headerDr("fax")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("Tel : " & Convert.ToString(headerDr("tel")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("E-mail : " & Convert.ToString(headerDr("email")) & vbLf, NormalFont))
                    phrase.Add(New Chunk("Website : " & Convert.ToString(headerDr("website")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    cell.SetLeading(12, 0)
                    tblLogo.AddCell(cell)
                    table.AddCell(tblLogo)

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
                    'Dim printMode As String = "download"
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblInv, printMode)

                    If hotelDt.Rows.Count > 0 Then
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
                            End If

                            Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                            Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
                            Dim tariffFilter = (From n In tariffDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n Order By Convert.ToDateTime(n.Field(Of String)("fromDate")) Ascending).ToList()
                            Dim filterTariffDt As New DataTable
                            If (tariffFilter.Count > 0) Then filterTariffDt = tariffFilter.CopyToDataTable()
                            If filterTariffDt.Rows.Count > 0 Then
                                For Each ratesDr As DataRow In filterTariffDt.Rows
                                    phrase.Add(New Chunk(vbCrLf + "From " + Convert.ToString(ratesDr("fromDate")) & " " & Convert.ToString(ratesDr("nights")) & " Nts * " & Convert.ToString(ratesDr("salePrice")) & " = ", NormalFont))
                                    phrase.Add(New Chunk(Convert.ToString(ratesDr("saleValue")) & " " & Convert.ToString(headerDr("currCode")), NormalFontBold))
                                    If ratesDr("bookingCode") <> "" Then
                                        phrase.Add(New Chunk(vbCrLf & "( " + Convert.ToString(ratesDr("bookingCode")) + " )", NormalFontBold))
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
                                        SpecialEvents(filterSplEvt, documentWidth, currCode, tblSplEvent)
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
                    End If

                    If othServDt.Rows.Count > 0 Or visaDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
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
                            AppendOtherServices(tblOthServ, MergeOrderDt)
                        End If
                        tblOthServ.Complete = True
                        tblOthServ.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblOthServ)
                    End If

                    Dim tblTotal As PdfPTable = New PdfPTable(2)
                    tblTotal.TotalWidth = documentWidth
                    tblTotal.LockedWidth = True
                    tblTotal.SetWidths(New Single() {0.8F, 0.2F})
                    tblTotal.KeepTogether = True

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



                    '*** Danny 20/10/2018 New Quoter Format>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    If ds_SR Is Nothing Then
                        document = FooterOLD(document)
                    Else
                        If Not ds_SR.Tables(0) Is Nothing Then
                            If ds_SR.Tables(0).Rows(0)("Footer").ToString() = "Footer1" Then
                                document = Footer1(document, ds_SR)
                            ElseIf ds_SR.Tables(0).Rows(0)("24hoursEmergencyMobileNo").ToString() = "Footer0" Then
                                document = FooterOLD(document)
                            Else
                                document = FooterOLD(document)
                            End If
                        Else
                            document = FooterOLD(document)
                        End If
                    End If
                    '*** Danny 20/10/2018 New Quoter Format<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<



                    document.AddTitle(Convert.ToString(headerDr("printHeader")) & " - " & quoteID)
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
    '*** Danny 20/10/20108>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    Private Function FooterOLD(ByVal document As Document) As Document
        Try
            Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 12, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
            '------- Tax Note ----------------
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
        Catch ex As Exception

        End Try
        Return document
    End Function

    Private Function Footer1(ByVal document As Document, ByVal ds_SR As DataSet) As Document

        Try
            Dim SmallFont As Font = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK)
            '------- Tax Note ----------------
            Dim tblTax As PdfPTable = New PdfPTable(2)
            tblTax.TotalWidth = documentWidth
            tblTax.LockedWidth = True
            tblTax.SetWidths(New Single() {0.03, 0.97F})
            tblTax.KeepTogether = True
            tblTax.Complete = False
            tblTax.SplitRows = False

            phrase = New Phrase()
            phrase.Add(New Chunk(ds_SR.Tables(0).Rows(0)("24hoursEmergencyMobileNo").ToString().Replace("|", vbCr).Trim, NormalFontBold))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, False)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
            cell.PaddingLeft = 2.0F
            cell.Colspan = 2
            cell.PaddingBottom = 3.0F
            cell.PaddingTop = 3.0F
            tblTax.AddCell(cell)
            document.Add(tblTax)
            'Dim strEmergencyNo As String()
            'strEmergencyNo = ds_SR.Tables(0).Rows(0)("24hoursEmergencyMobileNo").ToString().Split("|")

            'phrase = New Phrase()
            'If Not strEmergencyNo Is Nothing Then
            '    For a As Integer = 0 To strEmergencyNo.Length - 1
            '        phrase.Add(New Chunk("24-hours Emergency Mobile No. " + ds_SR.Tables(0).Rows(0)("24hoursEmergencyMobileNo").ToString(), Caption1))
            '    Next
            'End If



            Dim dtblFooter As PdfPTable = New PdfPTable(1)
            dtblFooter.TotalWidth = documentWidth
            dtblFooter.LockedWidth = True
            dtblFooter.SetWidths(New Single() {1.0F})
            dtblFooter.SplitRows = True
            '*** Footer File name and content Reading
            '*** Reading Columbus Folder path
            Dim strColumbusPath As String = System.Web.HttpContext.Current.Server.MapPath("")
            strColumbusPath = Path.GetDirectoryName(strColumbusPath)
            strColumbusPath = strColumbusPath + "\" + objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=8")

            Dim s As String = strColumbusPath + "\ExcelTemplates\" + ds_SR.Tables(0).Rows(0)("QuoteFooterFilename").ToString()
            s = ReadPdfFile(s)

            Dim strEmergencyNo As String()
            strEmergencyNo = s.ToString().Split(vbCrLf)

            phrase = New Phrase()
            phrase.Add(New Chunk(vbCr, SmallFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED, 1, False)
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
            'cell.Colspan = 4
            'cell.PaddingTop = 50
            'cell.SetLeading(12, 0)
            cell.PaddingBottom = 4
            dtblFooter.AddCell(cell)

            If Not strEmergencyNo Is Nothing Then

                For a As Integer = 0 To strEmergencyNo.Length - 1
                    Try
                        If a <> 0 Then
                            If strEmergencyNo(a - 1).ToString.Trim.Length = 0 And strEmergencyNo(a).ToString.Trim.Length = 0 Then
                            Else
                                phrase = New Phrase()
                                phrase.Add(New Chunk(strEmergencyNo(a).ToString.Trim, SmallFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED, 1, False)
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                'cell.Colspan = 4
                                'cell.PaddingTop = 50
                                'cell.SetLeading(12, 0)
                                cell.PaddingBottom = 4
                                dtblFooter.AddCell(cell)
                            End If
                        Else
                            phrase = New Phrase()
                            phrase.Add(New Chunk(strEmergencyNo(a).ToString.Trim, SmallFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_JUSTIFIED, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            'cell.Colspan = 4
                            'cell.PaddingTop = 50
                            'cell.SetLeading(12, 0)
                            cell.PaddingBottom = 4
                            dtblFooter.AddCell(cell)
                        End If


                    Catch ex As Exception

                    End Try
                Next
            End If
            'phrase.Add(New Chunk(s, NormalFont))



            document.Add(dtblFooter)

        Catch ex As Exception

        End Try
        Return document

    End Function
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
    '*** Danny 20/10/20108<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
#Region "Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef tblSplEvent As PdfPTable)"
    Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByVal CurrCode As String, ByRef tblSplEvent As PdfPTable)
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
        For Each splEventDr As DataRow In splEventDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventName")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingLeft = 3.0F
            cell.PaddingBottom = 4.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventDate")), NormalFont))
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

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("paxRate")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            cell.PaddingRight = 4.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventValue")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 2.0F
            cell.PaddingRight = 4.0F
            tblSplEvent.AddCell(cell)
        Next
        tblSplEvent.Complete = True
    End Sub
#End Region

#Region "Protected Sub AppendOtherServices(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)"
    Protected Sub AppendOtherServices(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        For Each inputDr As DataRow In inputDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("ServiceName")), NormalFont))
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
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("Unit")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("UnitPrice")), NormalFont))
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
            End If
        Next
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

    Public Sub GenerateCumulativeFormat(ByVal quoteID As String, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByRef ds2 As DataSet, ByRef ds3 As DataSet, ByVal printMode As String, ByVal objResParam As ReservationParameters, Optional ByVal fileName As String = "")
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dsbrkup As New DataSet
            Dim dscostingpdf As New DataSet

            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")

            mySqlCmd = New SqlCommand("sp_booking_adultchild_breakup", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@requestid", SqlDbType.VarChar, 20)).Value = quoteID
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteorbooking", SqlDbType.VarChar, 10)).Value = "Quote"
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(dsbrkup)
            ds2 = dsbrkup

            '    ds2.Tables.Add(clsUtilities.GetSharedDataFromDataTable("select * from packageterms_header"))


            'ds1 = ds
            'Dim headerDt As DataTable = ds.Tables(0)
            'Dim hotelDt As DataTable = ds.Tables(1)
            'Dim SplevtDt As DataTable = ds.Tables(3)
            'Dim OthserDt As DataTable = ds.Tables(5)
            'Dim UnitAppDt As DataTable = ds.Tables(7)
            'Dim TotalHotelDt As DataTable = ds.Tables(2)
            'Dim TotalSpleventDt As DataTable = ds.Tables(4)
            'Dim TotalOthServDt As DataTable = ds.Tables(6)

            Dim AdChBreakupDt As DataTable = dsbrkup.Tables(8)
            Dim TotalAdultchbrkdt As DataTable = dsbrkup.Tables(9)

            'AdChBreakupDt()

            'TotalAdultchbrkdt()

            '' Added shahul 07/06/18
            mySqlCmd = New SqlCommand("sp_booking_quote_costing_print", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteid", SqlDbType.VarChar, 20)).Value = quoteID

            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(dscostingpdf)
            ds3 = dscostingpdf

            mySqlCmd = New SqlCommand("sp_booking_quote_print_Cumulative", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteid", SqlDbType.VarChar, 20)).Value = quoteID

            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            ds1 = ds
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
            Dim PackageDt As DataTable = ds.Tables(10)
            Dim TermsCondDt As DataTable = ds.Tables(11)
            Dim CheckInOutDt As DataTable = ds.Tables(12)
            Dim CancelDt As DataTable = ds.Tables(13)
            Dim ExclusionDt As DataTable = ds.Tables(14)


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
                    tblLogo.AddCell(cell)
                    table.AddCell(tblLogo)

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
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblInv, printMode)

                    If hotelDt.Rows.Count > 0 Then
                        Dim arrServ() As String = {"Hotel Services", "Chk. in", "Chk. Out"}
                        Dim tblServ As PdfPTable = New PdfPTable(3)
                        tblServ.TotalWidth = documentWidth
                        tblServ.LockedWidth = True
                        tblServ.SetWidths(New Single() {0.74F, 0.13F, 0.13F})
                        tblServ.Complete = False
                        tblServ.HeaderRows = 1
                        tblServ.SplitRows = False
                        For i = 0 To 2
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
                                phrase.Add(New Chunk("[ " & Convert.ToString(hotelDr("occupancy")) & " ]" & vbLf, NormalFont))
                            End If

                            Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                            Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
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
                                        Dim tblSplEvent As New PdfPTable(4)
                                        CumulativeSpecialEvents(filterSplEvt, documentWidth, tblSplEvent)
                                        cell = New PdfPCell(tblSplEvent)
                                        cell.Colspan = 3
                                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                                        tblServ.AddCell(cell)
                                    End If
                                End If
                            End If
                        Next
                        tblServ.Complete = True
                        tblServ.SpacingBefore = 7
                        document.Add(tblServ)
                    End If

                    If othServDt.Rows.Count > 0 Or visaDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                        Dim OthServ() As String = {"Other Services", "Date of Service", "Units/ Pax"}
                        Dim tblOthServ As PdfPTable = New PdfPTable(3)
                        tblOthServ.TotalWidth = documentWidth
                        tblOthServ.LockedWidth = True
                        tblOthServ.SetWidths(New Single() {0.74F, 0.13F, 0.13F})
                        tblOthServ.SplitRows = False
                        tblOthServ.Complete = False
                        tblOthServ.HeaderRows = 1
                        For i = 0 To 2
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
                        Dim Unit As DataColumn = New DataColumn("Unit", GetType(String))
                        Dim ServiceDate As DataColumn = New DataColumn("ServiceDate", GetType(String))
                        Dim Adults As DataColumn = New DataColumn("Adults", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim Child As DataColumn = New DataColumn("Child", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim Senior As DataColumn = New DataColumn("Senior", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim PickUpDropOff As DataColumn = New DataColumn("PickUpDropOff", GetType(String)) With {.DefaultValue = DBNull.Value}
                        Dim Sic As DataColumn = New DataColumn("Sic", GetType(Integer)) With {.DefaultValue = DBNull.Value}

                        MergeDt.Columns.Add(OthServType)
                        MergeDt.Columns.Add(ServiceName)
                        MergeDt.Columns.Add(Unit)
                        MergeDt.Columns.Add(ServiceDate)
                        MergeDt.Columns.Add(Adults)
                        MergeDt.Columns.Add(Child)
                        MergeDt.Columns.Add(Senior)
                        MergeDt.Columns.Add(PickUpDropOff)
                        MergeDt.Columns.Add(Sic)

                        For Each othServDr As DataRow In othServDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Transfer"
                            MergeDr("ServiceName") = othServDr("transferName")
                            MergeDr("Unit") = othServDr("units")
                            MergeDr("ServiceDate") = othServDr("transferDate")
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
                            MergeDr("Unit") = VisaDr("noOfvisas")
                            MergeDr("ServiceDate") = VisaDr("VisaDate")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each airportDr As DataRow In airportDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Airport"
                            MergeDr("ServiceName") = airportDr("airportmaname")
                            MergeDr("Unit") = airportDr("units")
                            MergeDr("ServiceDate") = airportDr("airportmadate")
                            MergeDr("Adults") = airportDr("Adults")
                            MergeDr("Child") = airportDr("Child")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each tourDr As DataRow In tourDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Tour"
                            MergeDr("ServiceName") = tourDr("tourname")
                            MergeDr("Unit") = tourDr("units")
                            MergeDr("ServiceDate") = tourDr("tourdate")
                            MergeDr("Adults") = tourDr("Adults")
                            MergeDr("Child") = tourDr("Child")
                            MergeDr("Senior") = tourDr("Senior")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each otherDr As DataRow In OtherDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Other"
                            MergeDr("ServiceName") = otherDr("othername")
                            MergeDr("Unit") = otherDr("units")
                            MergeDr("ServiceDate") = otherDr("othdate")
                            MergeDr("Adults") = otherDr("Adults")
                            MergeDr("Child") = otherDr("Child")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        If MergeDt.Rows.Count > 0 Then
                            Dim MergeOrderDt As DataTable = (From n In MergeDt.AsEnumerable() Select n Order By Convert.ToDateTime(n.Field(Of String)("ServiceDate")) Ascending).CopyToDataTable()
                            CumulativeOtherService(tblOthServ, MergeOrderDt)
                        End If
                        tblOthServ.Complete = True
                        tblOthServ.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblOthServ)
                    End If

                    If PackageDt.Rows.Count > 0 Then
                        Dim tblPackage As PdfPTable = New PdfPTable(1)
                        tblPackage.TotalWidth = documentWidth
                        tblPackage.LockedWidth = True
                        tblPackage.SetWidths(New Single() {0.1F})
                        tblPackage.Complete = False
                        tblPackage.SplitRows = False
                        tblPackage.KeepTogether = True

                        Dim cntPackage As Integer = 0
                        For Each packageDr As DataRow In PackageDt.Rows
                            cntPackage = cntPackage + 1
                            phrase = New Phrase()
                            If cntPackage = 1 Then
                                phrase.Add(New Chunk("Package price for " + packageDr("package").ToString + vbCr, NormalFontBold))
                            Else
                                phrase.Add(New Chunk(packageDr("package").ToString + vbCr, NormalFont))
                            End If
                            cell = New PdfPCell(phrase)
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            If cntPackage = 1 And PackageDt.Rows.Count = 1 Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingTop = 1.0F
                                cell.PaddingBottom = 5.0F
                            ElseIf cntPackage = 1 Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                                cell.PaddingTop = 1.0F
                                cell.PaddingBottom = 4.0F
                            ElseIf cntPackage = PackageDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 5.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 4.0F
                            End If
                            tblPackage.AddCell(cell)
                        Next
                        tblPackage.Complete = True
                        document.Add(tblPackage)
                    End If

                    Dim tblTotal As PdfPTable = New PdfPTable(2)
                    tblTotal.TotalWidth = documentWidth
                    tblTotal.LockedWidth = True
                    tblTotal.SetWidths(New Single() {0.8F, 0.2F})
                    tblTotal.KeepTogether = True

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

                    If Convert.ToDecimal(headerDr("DiscountMarkup")) > 0 Then
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Discount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                        cell = New PdfPCell(phrase)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingTop = 3.0F
                        cell.PaddingRight = 10.0F
                        cell.PaddingBottom = 3.0F
                        tblTotal.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(Convert.ToString(headerDr("DiscountMarkup")), NormalFontBold))
                        cell = New PdfPCell(phrase)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingTop = 3.0F
                        cell.PaddingRight = 4.0F
                        cell.PaddingBottom = 3.0F
                        tblTotal.AddCell(cell)
                    End If

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Net Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 10.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("netSaleCurrency")), NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

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

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("SaleValue")), NormalFontBold))
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

                    Dim tblBrkupheader As PdfPTable = New PdfPTable(1)
                    tblBrkupheader.TotalWidth = documentWidth
                    tblBrkupheader.LockedWidth = True
                    tblBrkupheader.SetWidths(New Single() {0.12F})
                    tblBrkupheader.KeepTogether = True

                    phrase = New Phrase()
                    phrase.Add(New Chunk(" Per Pax Inclusive of all Services", NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    cell.BackgroundColor = titleColor
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.RIGHT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 10.0F
                    cell.PaddingBottom = 3.0F
                    tblBrkupheader.AddCell(cell)



                    tblBrkupheader.SpacingBefore = 7
                    document.Add(tblBrkupheader)


                    If Not AdChBreakupDt Is Nothing Then

                        If AdChBreakupDt.Rows.Count > 0 Then
                            Dim AdChBreakupstr() As String = {"Stay Details", "No. Of Pax", "Child Age", "Per Pax(USD)", "Sale Value(USD)"}
                            Dim tblAdChBreakup As PdfPTable = New PdfPTable(5)
                            tblAdChBreakup.TotalWidth = documentWidth
                            tblAdChBreakup.LockedWidth = True
                            tblAdChBreakup.SetWidths(New Single() {0.63F, 0.14F, 0.14F, 0.14F, 0.14F})
                            tblAdChBreakup.Complete = False
                            tblAdChBreakup.HeaderRows = 1
                            tblAdChBreakup.SplitRows = False
                            For i = 0 To 4
                                phrase = New Phrase()
                                phrase.Add(New Chunk(AdChBreakupstr(i), NormalFontBold))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 4.0F
                                cell.PaddingTop = 1.0F
                                cell.BackgroundColor = titleColor
                                tblAdChBreakup.AddCell(cell)
                            Next
                            For Each hotelDr As DataRow In AdChBreakupDt.Rows


                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr(0)), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 3.0F
                                tblAdChBreakup.AddCell(cell)


                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr("noofpax")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 3.0F
                                tblAdChBreakup.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr("childage")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 2.0F
                                tblAdChBreakup.AddCell(cell)


                                phrase = New Phrase()
                                phrase.Add(New Chunk(Math.Round(Convert.ToDecimal(hotelDr("usdperpax")), 2), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 2.0F
                                cell.PaddingRight = 4.0F
                                tblAdChBreakup.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Math.Round(Convert.ToDecimal(hotelDr("salevalueusd")), 2), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 2.0F
                                cell.PaddingRight = 4.0F
                                tblAdChBreakup.AddCell(cell)



                                'phrase = New Phrase()
                                'phrase.Add(New Chunk("Total Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                                'cell = New PdfPCell(phrase)
                                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                'cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                                'cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER
                                'cell.PaddingTop = 3.0F
                                'cell.PaddingRight = 10.0F
                                'cell.PaddingBottom = 3.0F
                                'tblTotal.AddCell(cell)

                            Next


                            tblAdChBreakup.Complete = True
                            tblAdChBreakup.SpacingBefore = 7
                            document.Add(tblAdChBreakup)






                            Dim tblBrkupTotal As PdfPTable = New PdfPTable(2)
                            tblBrkupTotal.TotalWidth = documentWidth
                            tblBrkupTotal.LockedWidth = True
                            tblBrkupTotal.SetWidths(New Single() {0.8F, 0.2F})
                            tblBrkupTotal.KeepTogether = True

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Total Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                            cell = New PdfPCell(phrase)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER
                            cell.PaddingTop = 3.0F
                            cell.PaddingRight = 10.0F
                            cell.PaddingBottom = 3.0F
                            tblBrkupTotal.AddCell(cell)

                            If TotalAdultchbrkdt.Rows.Count >= 0 Then
                                For Each Adultbrkrow As DataRow In TotalAdultchbrkdt.Rows


                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Math.Round(Convert.ToDecimal(Adultbrkrow("salevalueused")), 2), NormalFont))
                                    '' phrase.Add(New Chunk(Convert.ToDecimal(TotalAdultchbrkdt("salevalueused")), NormalFontBold))
                                    cell = New PdfPCell(phrase)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER

                                    cell.PaddingTop = 3.0F
                                    cell.PaddingRight = 4.0F
                                    cell.PaddingBottom = 3.0F
                                    tblBrkupTotal.AddCell(cell)
                                Next
                            End If


                            tblBrkupTotal.SpacingBefore = 7
                            document.Add(tblBrkupTotal)


                        End If
                    End If
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




                    '*** Footer File name and content Reading Danny 04/10/2018
                    '*** Reading Columbus Folder path
                    Dim parm(0) As SqlParameter
                    parm(0) = New SqlParameter("@RequestID", CType(quoteID, String))
                    Dim ds_Qt As New DataSet
                    ds_Qt = objclsUtilities.GetDataSet("SP_SelectQuoteParams", parm)

                    Dim strColumbusPath As String = System.Web.HttpContext.Current.Server.MapPath("")
                    strColumbusPath = Path.GetDirectoryName(strColumbusPath)
                    strColumbusPath = strColumbusPath + "\" + objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=8")

                    Dim s As String = strColumbusPath + "\ExcelTemplates\" + ds_Qt.Tables(0).Rows(0)("FooterFilename").ToString()
                    s = objBookingServicerVoucher.ReadPdfFile(s)

                    phrase = New Phrase()
                    'phrase.Add(New Chunk("ABOVE RATES ARE INCLUSIVE OF ALL TAXES INCLUDING VAT", NormalFontBoldTax))
                    phrase.Add(New Chunk(s, NormalFontBoldTax))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                    cell.PaddingLeft = 2.0F
                    cell.PaddingBottom = 3.0F
                    cell.PaddingTop = 3.0F
                    cell.BackgroundColor = BaseColor.YELLOW
                    tblTax.AddCell(cell)

                    'phrase = New Phrase()
                    'phrase.Add(New Chunk(Chr(149), NormalFontBoldTax))
                    'cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                    'cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                    'cell.PaddingLeft = 7.0F
                    'cell.PaddingBottom = 5.0F
                    'cell.BackgroundColor = BaseColor.YELLOW
                    'tblTax.AddCell(cell)

                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("ABOVE RATES DOES NOT INCLUDE TOURISM DIRHAM FEE WHICH IS TO BE PAID BY THE GUEST DIRECTLY AT THE HOTEL", NormalFontBoldTax))
                    'cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                    'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    'cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                    'cell.PaddingLeft = 2.0F
                    'cell.PaddingBottom = 5.0F
                    'cell.BackgroundColor = BaseColor.YELLOW
                    'tblTax.AddCell(cell)

                    tblTax.Complete = True
                    tblTax.SpacingBefore = 7
                    document.Add(tblTax)

                    '----------- Exclusion -------------------
                    If ExclusionDt.Rows.Count > 0 Then
                        Dim cntEx As Integer = 0
                        Dim tblExclusion As PdfPTable = New PdfPTable(2)
                        tblExclusion.TotalWidth = documentWidth
                        tblExclusion.LockedWidth = True
                        tblExclusion.SetWidths(New Single() {0.03F, 0.97F})
                        tblExclusion.Complete = False
                        tblExclusion.SplitRows = False
                        tblExclusion.KeepTogether = True

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Exclusions :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblExclusion.AddCell(cell)

                        For Each exculusionDr As DataRow In ExclusionDt.Rows
                            cntEx = cntEx + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntEx = ExclusionDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            cell.PaddingLeft = 10.0F
                            tblExclusion.AddCell(cell)

                            phrase = New Phrase()
                            If exculusionDr("Highlight").ToString().ToLower() = "yellow" Then
                                phrase.Add(New Chunk(exculusionDr("TermsDescription").ToString(), NormalFontRed).SetBackground(BaseColor.YELLOW))
                            Else
                                phrase.Add(New Chunk(exculusionDr("TermsDescription").ToString(), NormalFontRed))
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntEx = ExclusionDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            tblExclusion.AddCell(cell)
                        Next
                        tblExclusion.Complete = True
                        tblExclusion.SpacingBefore = 7
                        document.Add(tblExclusion)
                    End If

                    '---------- Terms and Condition ---------------
                    If TermsCondDt.Rows.Count > 0 Then
                        Dim cnt As Integer = 0
                        Dim tblNote As PdfPTable = New PdfPTable(2)
                        tblNote.TotalWidth = documentWidth
                        tblNote.LockedWidth = True
                        tblNote.SetWidths(New Single() {0.03F, 0.97F})
                        tblNote.Complete = False
                        tblNote.SplitRows = False
                        tblNote.HeaderRows = 2
                        tblNote.FooterRows = 1

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", NormalFont))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Colspan = 2
                        tblNote.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", NormalFont))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Colspan = 2
                        tblNote.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Terms and Conditions :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False, "No")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblNote.AddCell(cell)

                        For Each termsDr As DataRow In TermsCondDt.Rows
                            cnt = cnt + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFontRed).setLineHeight(13.0F))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cnt = TermsCondDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 5.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 3.0F
                            End If
                            cell.PaddingLeft = 10.0F
                            tblNote.AddCell(cell)

                            phrase = New Phrase()
                            If termsDr("Highlight").ToString().ToLower() = "yellow" Then
                                phrase.Add(New Chunk(termsDr("TermsDescription").ToString(), NormalFontRed).SetBackground(BaseColor.YELLOW).setLineHeight(13.0F))
                            Else
                                phrase.Add(New Chunk(termsDr("TermsDescription").ToString(), NormalFontRed).setLineHeight(13.0F))
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cnt = TermsCondDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 5.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 3.0F
                            End If
                            tblNote.AddCell(cell)
                        Next
                        tblNote.Complete = True
                        tblNote.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then
                            document.NewPage()
                        End If
                        document.Add(tblNote)
                    End If

                    '--------------- Check In / Check Out Policy -------------------
                    If CheckInOutDt.Rows.Count > 0 Then
                        Dim cntInOut As Integer = 0
                        Dim tblCheckInOut As PdfPTable = New PdfPTable(2)
                        tblCheckInOut.TotalWidth = documentWidth
                        tblCheckInOut.LockedWidth = True
                        tblCheckInOut.SetWidths(New Single() {0.03F, 0.97F})
                        tblCheckInOut.Complete = False
                        tblCheckInOut.SplitRows = False
                        tblCheckInOut.FooterRows = 1
                        tblCheckInOut.HeaderRows = 2

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Check In / Out Policy :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False, "No")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        Dim partyGroup = (From n In hotelDt.AsEnumerable() Group n By partyCode = n.Field(Of String)("partyCode") Into pg = Group Select New With {.partyCode = partyCode, .noofRows = pg.Count()}).ToList()

                        For Each checkInOutDr As DataRow In CheckInOutDt.Rows
                            cntInOut = cntInOut + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntInOut = CheckInOutDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            cell.PaddingLeft = 10.0F
                            tblCheckInOut.AddCell(cell)

                            phrase = New Phrase()
                            If partyGroup.Count > 1 Then
                                phrase.Add(New Chunk(checkInOutDr("partyName").ToString() + " ( " + checkInOutDr("rmtypName").ToString() + " ) - ", NormalFontSubTitle))
                            End If
                            phrase.Add(New Chunk(checkInOutDr("policytext").ToString(), NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntInOut = ExclusionDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            tblCheckInOut.AddCell(cell)
                        Next
                        tblCheckInOut.Complete = True
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblCheckInOut)
                    End If

                    '-------------- Cancellation Policy --------------------
                    If CancelDt.Rows.Count > 0 Then
                        Dim cntCancel As Integer = 0
                        Dim tblCancel As PdfPTable = New PdfPTable(2)
                        tblCancel.TotalWidth = documentWidth
                        tblCancel.LockedWidth = True
                        tblCancel.SetWidths(New Single() {0.03F, 0.97F})
                        tblCancel.Complete = False
                        tblCancel.SplitRows = False
                        tblCancel.FooterRows = 1
                        tblCancel.HeaderRows = 2

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCancel.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCancel.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Cancellation Policy :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False, "No")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblCancel.AddCell(cell)
                        Dim hotelGroup = (From n In hotelDt.AsEnumerable() Group n By partyCode = n.Field(Of String)("partyCode") Into pg = Group Select New With {.partyCode = partyCode, .noofRows = pg.Count()}).ToList()
                        Dim partyGroup = (From n In CancelDt.AsEnumerable() Group n By partyCode = n.Field(Of String)("partyCode"), partyName = n.Field(Of String)("partyName") Into canpolicy = Group Select New With {.partyCode = partyCode, .partyName = partyName, .noofRows = canpolicy.Count()}).ToList()
                        For i = 0 To partyGroup.Count - 1
                            Dim partycode = partyGroup(i).partyCode
                            Dim noOfRows As Integer = partyGroup(i).noofRows
                            Dim SplitDt As DataTable = (From n In CancelDt.AsEnumerable() Where n.Field(Of String)("partyCode") = partycode Select n Order By n.Field(Of Integer)("rlineno"), n.Field(Of Integer)("clineno") Ascending).CopyToDataTable()
                            If hotelGroup.Count > 1 Then
                                phrase = New Phrase()
                                phrase.Add(New Chunk(partyGroup(i).partyName.ToString() & vbCrLf, NormalFontSubTitle))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                                cell.SetLeading(15, 0)
                                cell.PaddingLeft = 10.0F
                                cell.PaddingBottom = 5.0F
                                cell.Colspan = 2
                                tblCancel.AddCell(cell)
                            End If
                            For Each splitDr As DataRow In SplitDt.Rows
                                cntCancel = cntCancel + 1
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Chr(149), NormalFontRed))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                If cntCancel = CancelDt.Rows.Count Then
                                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                    cell.PaddingBottom = 8.0F
                                Else
                                    cell.Border = Rectangle.LEFT_BORDER
                                    cell.PaddingBottom = 5.0F
                                End If
                                cell.PaddingLeft = 10.0F
                                tblCancel.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(splitDr("policytext").ToString(), NormalFontRed))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                If cntCancel = CancelDt.Rows.Count Then
                                    cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                    cell.PaddingBottom = 8.0F
                                Else
                                    cell.Border = Rectangle.RIGHT_BORDER
                                    cell.PaddingBottom = 5.0F
                                End If
                                tblCancel.AddCell(cell)
                            Next
                        Next
                        tblCancel.Complete = True
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblCancel)
                    End If
                    document.AddTitle(Convert.ToString(headerDr("printHeader")) & " - " & quoteID)
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

#Region "Public Sub GenerateCumulative(ByVal quoteID As String, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, ByVal objResParam As ReservationParameters, Optional ByVal fileName As String = "")"
    Public Sub GenerateCumulative(ByVal quoteID As String, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, ByVal objResParam As ReservationParameters, Optional ByVal fileName As String = "")
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            Dim dsbrkup As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")

            mySqlCmd = New SqlCommand("sp_booking_adultchild_breakup", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@requestid", SqlDbType.VarChar, 20)).Value = quoteID
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteorbooking", SqlDbType.VarChar, 10)).Value = "0"
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(dsbrkup)
            'ds1 = ds
            'Dim headerDt As DataTable = ds.Tables(0)
            'Dim hotelDt As DataTable = ds.Tables(1)
            'Dim SplevtDt As DataTable = ds.Tables(3)
            'Dim OthserDt As DataTable = ds.Tables(5)
            'Dim UnitAppDt As DataTable = ds.Tables(7)
            'Dim TotalHotelDt As DataTable = ds.Tables(2)
            'Dim TotalSpleventDt As DataTable = ds.Tables(4)
            'Dim TotalOthServDt As DataTable = ds.Tables(6)

            Dim AdChBreakupDt As DataTable
            Dim TotalAdultchbrkdt As DataTable

            AdChBreakupDt = dsbrkup.Tables(8)

            TotalAdultchbrkdt = dsbrkup.Tables(9)

            mySqlCmd = New SqlCommand("sp_booking_quote_print_Cumulative", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteid", SqlDbType.VarChar, 20)).Value = quoteID

            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            ds1 = ds
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
            Dim PackageDt As DataTable = ds.Tables(10)
            Dim TermsCondDt As DataTable = ds.Tables(11)
            Dim CheckInOutDt As DataTable = ds.Tables(12)
            Dim CancelDt As DataTable = ds.Tables(13)
            Dim ExclusionDt As DataTable = ds.Tables(14)


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
                    tblLogo.AddCell(cell)
                    table.AddCell(tblLogo)

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
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblInv, printMode)

                    If hotelDt.Rows.Count > 0 Then
                        Dim arrServ() As String = {"Hotel Services", "Chk. in", "Chk. Out"}
                        Dim tblServ As PdfPTable = New PdfPTable(3)
                        tblServ.TotalWidth = documentWidth
                        tblServ.LockedWidth = True
                        tblServ.SetWidths(New Single() {0.74F, 0.13F, 0.13F})
                        tblServ.Complete = False
                        tblServ.HeaderRows = 1
                        tblServ.SplitRows = False
                        For i = 0 To 2
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
                                phrase.Add(New Chunk("[ " & Convert.ToString(hotelDr("occupancy")) & " ]" & vbLf, NormalFont))
                            End If

                            Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                            Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
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
                                        Dim tblSplEvent As New PdfPTable(4)
                                        CumulativeSpecialEvents(filterSplEvt, documentWidth, tblSplEvent)
                                        cell = New PdfPCell(tblSplEvent)
                                        cell.Colspan = 3
                                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                                        tblServ.AddCell(cell)
                                    End If
                                End If
                            End If
                        Next
                        tblServ.Complete = True
                        tblServ.SpacingBefore = 7
                        document.Add(tblServ)
                    End If

                    If othServDt.Rows.Count > 0 Or visaDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                        Dim OthServ() As String = {"Other Services", "Date of Service", "Units/ Pax"}
                        Dim tblOthServ As PdfPTable = New PdfPTable(3)
                        tblOthServ.TotalWidth = documentWidth
                        tblOthServ.LockedWidth = True
                        tblOthServ.SetWidths(New Single() {0.74F, 0.13F, 0.13F})
                        tblOthServ.SplitRows = False
                        tblOthServ.Complete = False
                        tblOthServ.HeaderRows = 1
                        For i = 0 To 2
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
                        Dim Unit As DataColumn = New DataColumn("Unit", GetType(String))
                        Dim ServiceDate As DataColumn = New DataColumn("ServiceDate", GetType(String))
                        Dim Adults As DataColumn = New DataColumn("Adults", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim Child As DataColumn = New DataColumn("Child", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim Senior As DataColumn = New DataColumn("Senior", GetType(Integer)) With {.DefaultValue = DBNull.Value}
                        Dim PickUpDropOff As DataColumn = New DataColumn("PickUpDropOff", GetType(String)) With {.DefaultValue = DBNull.Value}
                        Dim Sic As DataColumn = New DataColumn("Sic", GetType(Integer)) With {.DefaultValue = DBNull.Value}

                        MergeDt.Columns.Add(OthServType)
                        MergeDt.Columns.Add(ServiceName)
                        MergeDt.Columns.Add(Unit)
                        MergeDt.Columns.Add(ServiceDate)
                        MergeDt.Columns.Add(Adults)
                        MergeDt.Columns.Add(Child)
                        MergeDt.Columns.Add(Senior)
                        MergeDt.Columns.Add(PickUpDropOff)
                        MergeDt.Columns.Add(Sic)

                        For Each othServDr As DataRow In othServDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Transfer"
                            MergeDr("ServiceName") = othServDr("transferName")
                            MergeDr("Unit") = othServDr("units")
                            MergeDr("ServiceDate") = othServDr("transferDate")
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
                            MergeDr("Unit") = VisaDr("noOfvisas")
                            MergeDr("ServiceDate") = VisaDr("VisaDate")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each airportDr As DataRow In airportDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Airport"
                            MergeDr("ServiceName") = airportDr("airportmaname")
                            MergeDr("Unit") = airportDr("units")
                            MergeDr("ServiceDate") = airportDr("airportmadate")
                            MergeDr("Adults") = airportDr("Adults")
                            MergeDr("Child") = airportDr("Child")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each tourDr As DataRow In tourDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Tour"
                            MergeDr("ServiceName") = tourDr("tourname")
                            MergeDr("Unit") = tourDr("units")
                            MergeDr("ServiceDate") = tourDr("tourdate")
                            MergeDr("Adults") = tourDr("Adults")
                            MergeDr("Child") = tourDr("Child")
                            MergeDr("Senior") = tourDr("Senior")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        For Each otherDr As DataRow In OtherDt.Rows
                            Dim MergeDr As DataRow = MergeDt.NewRow
                            MergeDr("OthServType") = "Other"
                            MergeDr("ServiceName") = otherDr("othername")
                            MergeDr("Unit") = otherDr("units")
                            MergeDr("ServiceDate") = otherDr("othdate")
                            MergeDr("Adults") = otherDr("Adults")
                            MergeDr("Child") = otherDr("Child")
                            MergeDt.Rows.Add(MergeDr)
                        Next
                        If MergeDt.Rows.Count > 0 Then
                            Dim MergeOrderDt As DataTable = (From n In MergeDt.AsEnumerable() Select n Order By Convert.ToDateTime(n.Field(Of String)("ServiceDate")) Ascending).CopyToDataTable()
                            CumulativeOtherService(tblOthServ, MergeOrderDt)
                        End If
                        tblOthServ.Complete = True
                        tblOthServ.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblOthServ)
                    End If

                    If PackageDt.Rows.Count > 0 Then
                        Dim tblPackage As PdfPTable = New PdfPTable(1)
                        tblPackage.TotalWidth = documentWidth
                        tblPackage.LockedWidth = True
                        tblPackage.SetWidths(New Single() {0.1F})
                        tblPackage.Complete = False
                        tblPackage.SplitRows = False
                        tblPackage.KeepTogether = True

                        Dim cntPackage As Integer = 0
                        For Each packageDr As DataRow In PackageDt.Rows
                            cntPackage = cntPackage + 1
                            phrase = New Phrase()
                            If cntPackage = 1 Then
                                phrase.Add(New Chunk("Package price for " + packageDr("package").ToString + vbCr, NormalFontBold))
                            Else
                                phrase.Add(New Chunk(packageDr("package").ToString + vbCr, NormalFont))
                            End If
                            cell = New PdfPCell(phrase)
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            If cntPackage = 1 And PackageDt.Rows.Count = 1 Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingTop = 1.0F
                                cell.PaddingBottom = 5.0F
                            ElseIf cntPackage = 1 Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.TOP_BORDER
                                cell.PaddingTop = 1.0F
                                cell.PaddingBottom = 4.0F
                            ElseIf cntPackage = PackageDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 5.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 4.0F
                            End If
                            tblPackage.AddCell(cell)
                        Next
                        tblPackage.Complete = True
                        document.Add(tblPackage)
                    End If

                    Dim tblTotal As PdfPTable = New PdfPTable(2)
                    tblTotal.TotalWidth = documentWidth
                    tblTotal.LockedWidth = True
                    tblTotal.SetWidths(New Single() {0.8F, 0.2F})
                    tblTotal.KeepTogether = True

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

                    If Convert.ToDecimal(headerDr("DiscountMarkup")) > 0 Then
                        phrase = New Phrase()
                        phrase.Add(New Chunk("Discount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                        cell = New PdfPCell(phrase)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        cell.Border = Rectangle.LEFT_BORDER
                        cell.PaddingTop = 3.0F
                        cell.PaddingRight = 10.0F
                        cell.PaddingBottom = 3.0F
                        tblTotal.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk(Convert.ToString(headerDr("DiscountMarkup")), NormalFontBold))
                        cell = New PdfPCell(phrase)
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                        cell.Border = Rectangle.RIGHT_BORDER
                        cell.PaddingTop = 3.0F
                        cell.PaddingRight = 4.0F
                        cell.PaddingBottom = 3.0F
                        tblTotal.AddCell(cell)
                    End If

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Net Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.LEFT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 10.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("netSaleCurrency")), NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    cell.Border = Rectangle.RIGHT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    tblTotal.AddCell(cell)

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

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("SaleValue")), NormalFontBold))
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

                    Dim tblBrkupheader As PdfPTable = New PdfPTable(1)
                    tblBrkupheader.TotalWidth = documentWidth
                    tblBrkupheader.LockedWidth = True
                    tblBrkupheader.SetWidths(New Single() {0.12F})
                    tblBrkupheader.KeepTogether = True

                    phrase = New Phrase()
                    phrase.Add(New Chunk(" Per Pax Inclusive of all Services", NormalFontBold))
                    cell = New PdfPCell(phrase)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    cell.BackgroundColor = titleColor
                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.RIGHT_BORDER
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 10.0F
                    cell.PaddingBottom = 3.0F
                    tblBrkupheader.AddCell(cell)



                    tblBrkupheader.SpacingBefore = 7
                    document.Add(tblBrkupheader)


                    If Not AdChBreakupDt Is Nothing Then

                        If AdChBreakupDt.Rows.Count > 0 Then
                            Dim AdChBreakupstr() As String = {"Stay Details", "No. Of Pax", "Child Age", "Per Pax(" + Convert.ToString(headerDr("currcode")) + ")", "Sale Value(" + Convert.ToString(headerDr("currcode")) + ")"}
                            Dim tblAdChBreakup As PdfPTable = New PdfPTable(5)
                            tblAdChBreakup.TotalWidth = documentWidth
                            tblAdChBreakup.LockedWidth = True
                            tblAdChBreakup.SetWidths(New Single() {0.63F, 0.14F, 0.14F, 0.14F, 0.14F})
                            tblAdChBreakup.Complete = False
                            tblAdChBreakup.HeaderRows = 1
                            tblAdChBreakup.SplitRows = False
                            For i = 0 To 4
                                phrase = New Phrase()
                                phrase.Add(New Chunk(AdChBreakupstr(i), NormalFontBold))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 4.0F
                                cell.PaddingTop = 1.0F
                                cell.BackgroundColor = titleColor
                                tblAdChBreakup.AddCell(cell)
                            Next
                            For Each hotelDr As DataRow In AdChBreakupDt.Rows


                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr(0)), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 3.0F
                                tblAdChBreakup.AddCell(cell)


                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr("noofpax")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 3.0F
                                tblAdChBreakup.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Convert.ToString(hotelDr("childage")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 2.0F
                                tblAdChBreakup.AddCell(cell)


                                phrase = New Phrase()
                                phrase.Add(New Chunk(Math.Round(Convert.ToDecimal(hotelDr("usdperpax")), 2), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 2.0F
                                cell.PaddingRight = 4.0F
                                tblAdChBreakup.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(Math.Round(Convert.ToDecimal(hotelDr("salevalueusd")), 2), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.PaddingBottom = 2.0F
                                cell.PaddingRight = 4.0F
                                tblAdChBreakup.AddCell(cell)



                                'phrase = New Phrase()
                                'phrase.Add(New Chunk("Total Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                                'cell = New PdfPCell(phrase)
                                'cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                'cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                                'cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER
                                'cell.PaddingTop = 3.0F
                                'cell.PaddingRight = 10.0F
                                'cell.PaddingBottom = 3.0F
                                'tblTotal.AddCell(cell)

                            Next


                            tblAdChBreakup.Complete = True
                            tblAdChBreakup.SpacingBefore = 7
                            document.Add(tblAdChBreakup)






                            Dim tblBrkupTotal As PdfPTable = New PdfPTable(2)
                            tblBrkupTotal.TotalWidth = documentWidth
                            tblBrkupTotal.LockedWidth = True
                            tblBrkupTotal.SetWidths(New Single() {0.8F, 0.2F})
                            tblBrkupTotal.KeepTogether = True

                            phrase = New Phrase()
                            phrase.Add(New Chunk("Total Amount (" + Convert.ToString(headerDr("currcode")) + ")", NormalFontBold))
                            cell = New PdfPCell(phrase)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                            cell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER
                            cell.PaddingTop = 3.0F
                            cell.PaddingRight = 10.0F
                            cell.PaddingBottom = 3.0F
                            tblBrkupTotal.AddCell(cell)

                            If TotalAdultchbrkdt.Rows.Count >= 0 Then
                                For Each Adultbrkrow As DataRow In TotalAdultchbrkdt.Rows


                                    phrase = New Phrase()
                                    phrase.Add(New Chunk(Math.Round(Convert.ToDecimal(Adultbrkrow("salevalueused")), 2), NormalFont))
                                    '' phrase.Add(New Chunk(Convert.ToDecimal(TotalAdultchbrkdt("salevalueused")), NormalFontBold))
                                    cell = New PdfPCell(phrase)
                                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER

                                    cell.PaddingTop = 3.0F
                                    cell.PaddingRight = 4.0F
                                    cell.PaddingBottom = 3.0F
                                    tblBrkupTotal.AddCell(cell)
                                Next
                            End If


                            tblBrkupTotal.SpacingBefore = 7
                            document.Add(tblBrkupTotal)


                        End If
                    End If
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

                    '----------- Exclusion -------------------
                    If ExclusionDt.Rows.Count > 0 Then
                        Dim cntEx As Integer = 0
                        Dim tblExclusion As PdfPTable = New PdfPTable(2)
                        tblExclusion.TotalWidth = documentWidth
                        tblExclusion.LockedWidth = True
                        tblExclusion.SetWidths(New Single() {0.03F, 0.97F})
                        tblExclusion.Complete = False
                        tblExclusion.SplitRows = False
                        tblExclusion.KeepTogether = True

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Exclusions :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblExclusion.AddCell(cell)

                        For Each exculusionDr As DataRow In ExclusionDt.Rows
                            cntEx = cntEx + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntEx = ExclusionDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            cell.PaddingLeft = 10.0F
                            tblExclusion.AddCell(cell)

                            phrase = New Phrase()
                            If exculusionDr("Highlight").ToString().ToLower() = "yellow" Then
                                phrase.Add(New Chunk(exculusionDr("TermsDescription").ToString(), NormalFontRed).SetBackground(BaseColor.YELLOW))
                            Else
                                phrase.Add(New Chunk(exculusionDr("TermsDescription").ToString(), NormalFontRed))
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntEx = ExclusionDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            tblExclusion.AddCell(cell)
                        Next
                        tblExclusion.Complete = True
                        tblExclusion.SpacingBefore = 7
                        document.Add(tblExclusion)
                    End If

                    '---------- Terms and Condition ---------------
                    If TermsCondDt.Rows.Count > 0 Then
                        Dim cnt As Integer = 0
                        Dim tblNote As PdfPTable = New PdfPTable(2)
                        tblNote.TotalWidth = documentWidth
                        tblNote.LockedWidth = True
                        tblNote.SetWidths(New Single() {0.03F, 0.97F})
                        tblNote.Complete = False
                        tblNote.SplitRows = False
                        tblNote.HeaderRows = 2
                        tblNote.FooterRows = 1

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", NormalFont))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Colspan = 2
                        tblNote.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", NormalFont))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Colspan = 2
                        tblNote.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Terms and Conditions :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False, "No")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblNote.AddCell(cell)

                        For Each termsDr As DataRow In TermsCondDt.Rows
                            cnt = cnt + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFontRed).setLineHeight(13.0F))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cnt = TermsCondDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 5.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 3.0F
                            End If
                            cell.PaddingLeft = 10.0F
                            tblNote.AddCell(cell)

                            phrase = New Phrase()
                            If termsDr("Highlight").ToString().ToLower() = "yellow" Then
                                phrase.Add(New Chunk(termsDr("TermsDescription").ToString(), NormalFontRed).SetBackground(BaseColor.YELLOW).setLineHeight(13.0F))
                            Else
                                phrase.Add(New Chunk(termsDr("TermsDescription").ToString(), NormalFontRed).setLineHeight(13.0F))
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cnt = TermsCondDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 5.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 3.0F
                            End If
                            tblNote.AddCell(cell)
                        Next
                        tblNote.Complete = True
                        tblNote.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then
                            document.NewPage()
                        End If
                        document.Add(tblNote)
                    End If

                    '--------------- Check In / Check Out Policy -------------------
                    If CheckInOutDt.Rows.Count > 0 Then
                        Dim cntInOut As Integer = 0
                        Dim tblCheckInOut As PdfPTable = New PdfPTable(2)
                        tblCheckInOut.TotalWidth = documentWidth
                        tblCheckInOut.LockedWidth = True
                        tblCheckInOut.SetWidths(New Single() {0.03F, 0.97F})
                        tblCheckInOut.Complete = False
                        tblCheckInOut.SplitRows = False
                        tblCheckInOut.FooterRows = 1
                        tblCheckInOut.HeaderRows = 2

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Check In / Out Policy :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False, "No")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblCheckInOut.AddCell(cell)

                        Dim partyGroup = (From n In hotelDt.AsEnumerable() Group n By partyCode = n.Field(Of String)("partyCode") Into pg = Group Select New With {.partyCode = partyCode, .noofRows = pg.Count()}).ToList()

                        For Each checkInOutDr As DataRow In CheckInOutDt.Rows
                            cntInOut = cntInOut + 1
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Chr(149), NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntInOut = CheckInOutDt.Rows.Count Then
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.LEFT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            cell.PaddingLeft = 10.0F
                            tblCheckInOut.AddCell(cell)

                            phrase = New Phrase()
                            If partyGroup.Count > 1 Then
                                phrase.Add(New Chunk(checkInOutDr("partyName").ToString() + " ( " + checkInOutDr("rmtypName").ToString() + " ) - ", NormalFontSubTitle))
                            End If
                            phrase.Add(New Chunk(checkInOutDr("policytext").ToString(), NormalFontRed))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            If cntInOut = ExclusionDt.Rows.Count Then
                                cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                cell.PaddingBottom = 8.0F
                            Else
                                cell.Border = Rectangle.RIGHT_BORDER
                                cell.PaddingBottom = 5.0F
                            End If
                            tblCheckInOut.AddCell(cell)
                        Next
                        tblCheckInOut.Complete = True
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblCheckInOut)
                    End If

                    '-------------- Cancellation Policy --------------------
                    If CancelDt.Rows.Count > 0 Then
                        Dim cntCancel As Integer = 0
                        Dim tblCancel As PdfPTable = New PdfPTable(2)
                        tblCancel.TotalWidth = documentWidth
                        tblCancel.LockedWidth = True
                        tblCancel.SetWidths(New Single() {0.03F, 0.97F})
                        tblCancel.Complete = False
                        tblCancel.SplitRows = False
                        tblCancel.FooterRows = 1
                        tblCancel.HeaderRows = 2

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCancel.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("", TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "Yes")
                        cell.Colspan = 2
                        tblCancel.AddCell(cell)

                        phrase = New Phrase()
                        phrase.Add(New Chunk("Cancellation Policy :" & vbCrLf, TitleFontBoldUnderLine))
                        cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False, "No")
                        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                        cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        cell.SetLeading(15, 0)
                        cell.PaddingLeft = 10.0F
                        cell.PaddingBottom = 5.0F
                        cell.Colspan = 2
                        tblCancel.AddCell(cell)
                        Dim hotelGroup = (From n In hotelDt.AsEnumerable() Group n By partyCode = n.Field(Of String)("partyCode") Into pg = Group Select New With {.partyCode = partyCode, .noofRows = pg.Count()}).ToList()
                        Dim partyGroup = (From n In CancelDt.AsEnumerable() Group n By partyCode = n.Field(Of String)("partyCode"), partyName = n.Field(Of String)("partyName") Into canpolicy = Group Select New With {.partyCode = partyCode, .partyName = partyName, .noofRows = canpolicy.Count()}).ToList()
                        For i = 0 To partyGroup.Count - 1
                            Dim partycode = partyGroup(i).partyCode
                            Dim noOfRows As Integer = partyGroup(i).noofRows
                            Dim SplitDt As DataTable = (From n In CancelDt.AsEnumerable() Where n.Field(Of String)("partyCode") = partycode Select n Order By n.Field(Of Integer)("rlineno"), n.Field(Of Integer)("clineno") Ascending).CopyToDataTable()
                            If hotelGroup.Count > 1 Then
                                phrase = New Phrase()
                                phrase.Add(New Chunk(partyGroup(i).partyName.ToString() & vbCrLf, NormalFontSubTitle))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                                cell.SetLeading(15, 0)
                                cell.PaddingLeft = 10.0F
                                cell.PaddingBottom = 5.0F
                                cell.Colspan = 2
                                tblCancel.AddCell(cell)
                            End If
                            For Each splitDr As DataRow In SplitDt.Rows
                                cntCancel = cntCancel + 1
                                phrase = New Phrase()
                                phrase.Add(New Chunk(Chr(149), NormalFontRed))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                If cntCancel = CancelDt.Rows.Count Then
                                    cell.Border = Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                                    cell.PaddingBottom = 8.0F
                                Else
                                    cell.Border = Rectangle.LEFT_BORDER
                                    cell.PaddingBottom = 5.0F
                                End If
                                cell.PaddingLeft = 10.0F
                                tblCancel.AddCell(cell)

                                phrase = New Phrase()
                                phrase.Add(New Chunk(splitDr("policytext").ToString(), NormalFontRed))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True, "No")
                                cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                                If cntCancel = CancelDt.Rows.Count Then
                                    cell.Border = Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                                    cell.PaddingBottom = 8.0F
                                Else
                                    cell.Border = Rectangle.RIGHT_BORDER
                                    cell.PaddingBottom = 5.0F
                                End If
                                tblCancel.AddCell(cell)
                            Next
                        Next
                        tblCancel.Complete = True
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblCancel)
                    End If
                    document.AddTitle(Convert.ToString(headerDr("printHeader")) & " - " & quoteID)
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

#Region "Protected Sub CumulativeSpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByRef tblSplEvent As PdfPTable)"
    Protected Sub CumulativeSpecialEvents(ByVal splEventDt As DataTable, ByVal documentWidth As Single, ByRef tblSplEvent As PdfPTable)
        Dim phrase As New Phrase
        Dim cell As New PdfPCell
        Dim splEventTitleColor As BaseColor = New BaseColor(203, 235, 249)   '-255, 219, 212
        Dim arrSplEvent() As String = {"Special Events", "Date of Event", "Units/ Pax", "Type of Units/Pax"}
        tblSplEvent.TotalWidth = documentWidth
        tblSplEvent.LockedWidth = True
        tblSplEvent.SetWidths(New Single() {0.61F, 0.13F, 0.13F, 0.13F})
        tblSplEvent.Complete = False
        tblSplEvent.HeaderRows = 1
        tblSplEvent.SplitRows = False
        For i = 0 To 3
            phrase = New Phrase()
            phrase.Add(New Chunk(arrSplEvent(i), NormalFontBold))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 4.0F
            cell.PaddingTop = 1.0F
            cell.BackgroundColor = splEventTitleColor
            tblSplEvent.AddCell(cell)
        Next
        For Each splEventDr As DataRow In splEventDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventName")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.SetLeading(12, 0)
            cell.PaddingLeft = 3.0F
            cell.PaddingBottom = 4.0F
            tblSplEvent.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(splEventDr("splEventDate")), NormalFont))
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
        Next
        tblSplEvent.Complete = True
    End Sub
#End Region

#Region "Protected Sub CumulativeOtherService(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)"
    Protected Sub CumulativeOtherService(ByRef tblOthServ As PdfPTable, ByVal inputDt As DataTable)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        For Each inputDr As DataRow In inputDt.Rows
            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("ServiceName")), NormalFont))
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
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            tblOthServ.AddCell(cell)

            phrase = New Phrase()
            phrase.Add(New Chunk(Convert.ToString(inputDr("Unit")), NormalFont))
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.PaddingBottom = 3.0F
            If Convert.ToString(inputDr("OthServType")) = "Transfer" Then
                cell.Rowspan = 2
            End If
            cell.PaddingLeft = 3.0F
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
            End If
        Next
    End Sub
#End Region

#Region "Public Sub GenerateReportNew(ByVal requestID As String, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, objResParam As ReservationParameters, Optional ByVal fileName As String = "")"
    Public Sub GenerateReportNew(ByVal requestID As String, ByRef bytes() As Byte, ByRef ds1 As DataSet, ByVal printMode As String, ByVal objResParam As ReservationParameters, Optional ByVal fileName As String = "")
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As New SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                mySqlCmd = New SqlCommand("sp_booking_quote_print_Whitelabel", sqlConn)
            Else
                mySqlCmd = New SqlCommand("sp_booking_quote_print", sqlConn)
            End If
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@QuoteID", SqlDbType.VarChar, 20)).Value = requestID
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            ds1 = ds
            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim othServDt As DataTable = ds.Tables(3)
            Dim airportDt As DataTable = ds.Tables(4)
            Dim visaDt As DataTable = ds.Tables(5)
            Dim tourDt As DataTable = ds.Tables(6)
            Dim OtherDt As DataTable = ds.Tables(7)
            'Dim guestDt As DataTable = ds.Tables(8)
            Dim contactDt As DataTable = ds.Tables(8) '9)
            'Dim BankDt As DataTable = ds.Tables(10)
            Dim SplEventDt As DataTable = ds.Tables(9) '11)
            clsDBConnect.dbConnectionClose(sqlConn)
            If headerDt.Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 35.0F)
                Dim documentWidth As Single = 550.0F
                Dim TitleFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 16, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 12, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
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
                    If objResParam.LoginType = "Agent" And objResParam.WhiteLabel = "1" Then
                        Dim logoName As String = objclsUtilities.ExecuteQueryReturnStringValue("select logofilename from agentmast_whitelabel where agentcode ='" + objResParam.AgentCode.Trim() + "'")
                        cell = ImageCell("~/Logos/" + logoName, 60.0F, PdfPCell.ALIGN_CENTER)
                    Else
                        If (headerDr("div_code") = "01") Then
                            cell = ImageCell("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_CENTER)
                        Else
                            cell = ImageCell("~/img/mahce_logo.jpg", 60.0F, PdfPCell.ALIGN_CENTER)
                        End If
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
                    tblLogo.AddCell(cell)
                    table.AddCell(tblLogo)

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
                    phrase.Add(New Chunk("Fax : " & Convert.ToString(headerDr("agentfax")), NormalFont))
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
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblInv, printMode)

                    If hotelDt.Rows.Count > 0 Then
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
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("noofrooms")) & "  " & Convert.ToString(hotelDr("RoomDetail")) & vbLf, NormalFont))
                            If Convert.ToString(hotelDr("occupancy")) <> "" Then
                                phrase.Add(New Chunk("[ " & Convert.ToString(hotelDr("occupancy")) & " ]", NormalFont))
                            End If

                            Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                            Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
                            Dim tariffFilter = (From n In tariffDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n Order By Convert.ToDateTime(n.Field(Of String)("fromDate")) Ascending).ToList()
                            Dim filterTariffDt As New DataTable
                            If (tariffFilter.Count > 0) Then filterTariffDt = tariffFilter.CopyToDataTable()
                            If filterTariffDt.Rows.Count > 0 Then
                                For Each ratesDr As DataRow In filterTariffDt.Rows
                                    phrase.Add(New Chunk(vbLf + "From " + Convert.ToString(ratesDr("fromDate")) & " " & Convert.ToString(ratesDr("nights")) & " Nts * " & Convert.ToString(ratesDr("salePrice")) & " * " & Convert.ToString(hotelDr("noofrooms")) & " Units = ", NormalFont))
                                    phrase.Add(New Chunk(Convert.ToString(ratesDr("saleValue")) & " " & Convert.ToString(headerDr("currCode")), NormalFontBold))
                                    If ratesDr("bookingCode") <> "" Then
                                        phrase.Add(New Chunk(vbCrLf & "( " + Convert.ToString(ratesDr("bookingCode")) + " )", NormalFontBold))
                                    End If
                                Next
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingBottom = 3.0F
                            tblTariff.AddCell(cell)
                            phrase = New Phrase()
                            If Convert.ToString(hotelDr("hotelConfNo")) <> "" Then
                                phrase.Add(New Chunk("Hotel Conf No : " & Convert.ToString(hotelDr("hotelConfNo")), NormalFontBold))
                            End If

                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, False)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.SetLeading(12, 0)
                            cell.PaddingBottom = 3.0F
                            cell.Colspan = 2
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

                            If SplEventDt.Rows.Count > 0 Then
                                Dim partyCode As String = hotelDr("partyCode").ToString()
                                Dim index As Integer = hotelDt.Rows.IndexOf(hotelDr)
                                Dim i As Integer = 0
                                Dim lastIndex As Integer = index
                                Dim filterRows = (From n In SplEventDt.AsEnumerable() Where n.Field(Of String)("PartyCode") = partyCode)
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
                                        SpecialEvents(filterSplEvt, documentWidth, currCode, tblSplEvent)
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
                    End If

                    If othServDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or visaDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
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
                            AppendOtherServices(tblOthServ, MergeOrderDt)
                        End If
                        tblOthServ.Complete = True
                        tblOthServ.SpacingBefore = 7
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblOthServ)
                    End If

                    Dim tblTotal As PdfPTable = New PdfPTable(2)
                    tblTotal.TotalWidth = documentWidth
                    tblTotal.LockedWidth = True
                    tblTotal.SetWidths(New Single() {0.8F, 0.2F})
                    tblTotal.KeepTogether = True

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

                    'If guestDt.Rows.Count > 0 Then
                    '    Dim tblGuest As PdfPTable = New PdfPTable(3)
                    '    tblGuest.TotalWidth = documentWidth
                    '    tblGuest.LockedWidth = True
                    '    tblGuest.Complete = False
                    '    tblGuest.SplitRows = False
                    '    tblGuest.HeaderRows = 1
                    '    GuestList(tblGuest, guestDt)
                    '    tblGuest.Complete = True
                    '    tblGuest.SpacingBefore = 7
                    '    remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                    '    If remainingPageSpace < 48 Then document.NewPage()
                    '    document.Add(tblGuest)
                    'End If

                    '------------ Note -----------
                    Dim tblNote As PdfPTable = New PdfPTable(1)
                    tblNote.TotalWidth = documentWidth
                    tblNote.LockedWidth = True
                    tblNote.SetWidths(New Single() {1.0F})
                    tblNote.Complete = False
                    tblNote.SplitRows = False
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

                    Dim tblFooter As New PdfPTable(1)
                    tblFooter.TotalWidth = documentWidth
                    tblFooter.LockedWidth = True
                    tblFooter.Complete = False
                    tblFooter.SetWidths({1.0F})
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
                    End If

                    'phrase = New Phrase()
                    'phrase.Add(New Chunk("Any Discrepancy on the above quote to be revert back within 72 hours from the date of Confirmation or else treated as final", NormalFont))
                    'cell = New PdfPCell(phrase)
                    'cell.BorderWidth = 0.7F
                    'cell.BorderColor = BaseColor.WHITE
                    'cell.SetLeading(15, 0)
                    'cell.PaddingTop = 3.0F
                    'tblFooter.AddCell(cell)
                    tblFooter.Complete = True
                    document.Add(tblFooter)

                    'If BankDt.Rows.Count > 0 Then
                    '    Dim tblBank As PdfPTable = New PdfPTable(2)
                    '    tblBank.TotalWidth = documentWidth
                    '    tblBank.LockedWidth = True
                    '    tblBank.Complete = False
                    '    tblBank.SplitRows = False
                    '    BankDetails(tblBank, BankDt)
                    '    tblBank.Complete = True
                    '    tblBank.SpacingBefore = 7
                    '    tblBank.KeepTogether = True
                    '    document.Add(tblBank)
                    'End If
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
End Class
