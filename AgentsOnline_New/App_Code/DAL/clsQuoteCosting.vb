Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Linq
Imports ClosedXML.Excel
Imports System.Web.Services
Imports System.Math
Imports System.Net
Imports System
 
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Imports System.Diagnostics


Public Class clsQuoteCosting

#Region "Global Variable"
    Dim objclsUtilities As New clsUtilities
    Dim NormalFont As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)
    Dim NormalFontBold As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)
    Dim NormalFontRed As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.RED)
    Dim NormalFontBoldRed As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.RED)
    Dim NormalFontSubTitle As Font = FontFactory.GetFont("Arial", 10, Font.BOLD, New BaseColor(255, 133, 51))
    Dim titleColor As BaseColor = New BaseColor(214, 214, 214)
    Dim subTitleColor As BaseColor = New BaseColor(218, 247, 166)
    Dim totalbackColor As BaseColor = New BaseColor(228, 227, 225)
    Dim document As New XLWorkbook
    'Public Shared AdchBrkUpdt As DataTable
    'Public Shared TotalAdultchbrkdt As DataTable
#End Region

    Private Sub generatepdf(quoteID As String)
        Dim strpop As String
        Dim strpopName As String = "popup" + quoteid.Replace("/", "").ToString()
        strpop = "window.open('PrintPage.aspx?printID=bookingQuote&quoteId=" & quoteid.Trim & "');"

        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), strpopName, strpop, True)
        End If

    End Sub
#Region "Public Sub GenerateExcelReport(ByVal quoteID As String, ByVal FilePath As String)"
    Public Sub GenerateExcelReport(ByVal quoteID As String, ByVal FilePath As String)


        Dim FileNameNew As String = "QuoteCosting_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xlsx"
        document = New XLWorkbook(FilePath)
        Dim ws As IXLWorksheet = document.Worksheet("QuoteCosting")
        'ws.Style.Font.FontName = "Times New Roman"
        Dim RandomCls As New Random()
        Dim RandomNo As String = RandomCls.Next(100000, 9999999).ToString
        Dim sqlConn As New SqlConnection
        Dim mySqlCmd As SqlCommand
        Dim myDataAdapter As New SqlDataAdapter
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim LastLine As Integer


        sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
        ' mySqlCmd = New SqlCommand("sp_booking_adultchild_breakup_develop", sqlConn)
        mySqlCmd = New SqlCommand("sp_booking_adultchild_breakup", sqlConn)
        mySqlCmd.CommandType = CommandType.StoredProcedure
        mySqlCmd.Parameters.Add(New SqlParameter("@requestid", SqlDbType.VarChar, 20)).Value = quoteID

        mySqlCmd.Parameters.Add(New SqlParameter("@quoteorbooking", SqlDbType.VarChar, 10)).Value = "Quote"
        myDataAdapter.SelectCommand = mySqlCmd
        myDataAdapter.Fill(ds)
        ds1 = ds
        Dim headerDt As DataTable = ds.Tables(0)
        Dim hotelDt As DataTable = ds.Tables(1)
        hotelDt.Columns.Remove("catname")
        hotelDt.Columns.Remove("destname")

        'Dim col As DataColumn
        'For Each col In hotelDt.Columns
        '    hotelDt.Columns.Remove("catname")
        '    hotelDt.Columns.Remove("destname")
        'Next




        Dim SplevtDt As DataTable = ds.Tables(3)
        Dim OthserDt As DataTable = ds.Tables(5)
        Dim UnitAppDt As DataTable = ds.Tables(7)
        Dim TotalHotelDt As DataTable = ds.Tables(2)
        Dim TotalSpleventDt As DataTable = ds.Tables(4)
        Dim TotalOthServDt As DataTable = ds.Tables(6)
        Dim AdchBrkUpdt As DataTable = ds.Tables(8)
        Dim TotalAdultchbrkdt As DataTable = ds.Tables(9)
        Dim Totalpaxcost As DataTable = ds.Tables(10)

        Dim dtSupplierToBook As DataTable = ds.Tables(12)

        ' generatepdf(quoteID)



        ws.Cell(2, 1).Value = String.Format("QuoteNo-{0}", quoteID)

        ws.Cell(2, 1).Style.Font.Bold = True
        If headerDt.Rows.Count > 0 Then
            If headerDt.Rows(0).Item("child").ToString() > 0 Then
                ws.Cell(4, 1).Value = headerDt.Rows(0).Item("adults").ToString() + " Adults  " + headerDt.Rows(0).Item("child").ToString() + " Child (" + headerDt.Rows(0).Item("childages").ToString().Replace(";", ",") + ")"
            Else
                ws.Cell(4, 1).Value = headerDt.Rows(0).Item("adults").ToString() + " Adults  "
            End If
        End If
        LastLine = 7
        If hotelDt.Rows.Count > 0 Then
            Dim RateSheet As IXLRange
            Dim someView As DataView
            Dim Hotelnameid = (From n In hotelDt.AsEnumerable() Group n By HotelNameRowid = New With {Key .Hotelname = n.Item(2), Key .Rowid = n.Item(0)} Into g1 = Group Select New With {.Hotelname = HotelNameRowid}).ToList()
            For Each row In Hotelnameid
                ws.Range(LastLine, 1, LastLine, 13).Merge()
                ws.Range(LastLine, 1, LastLine, 13).Style.Fill.BackgroundColor = XLColor.BabyPink
                ws.Range(LastLine, 1, LastLine, 13).Style.Alignment.WrapText = True
                ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
                ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
                ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
                ws.Row(LastLine).AdjustToContents()
                ws.Cell(LastLine, 1).Value = String.Format("Hotel -{0}", Hotelnameid.IndexOf(row) + 1)
                ws.Range(LastLine, 1, LastLine, 13).Style.Font.Bold = True
                ws.Range(LastLine, 1, LastLine, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                someView = New DataView(hotelDt)
                someView.RowFilter = String.Format("Rowid={0}", row.Hotelname.Rowid.ToString())
                ' ws.Rows(LastLine + 1).Delete()
                'RateSheet = ws.Cell(LastLine + 1, 1).InsertTable(someView.ToTable.AsEnumerable).SetShowHeaderRow(False).AsRange()
                RateSheet = ws.Cell(LastLine + 1, 1).InsertData(someView.ToTable.AsEnumerable).AsRange()
                RateSheet.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
                RateSheet.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
                RateSheet.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
                RateSheet.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
                LastLine = RateSheet.LastRow.WorksheetRow.RowNumber() + 1
                ws.Columns("3,4").AdjustToContents()
                RateSheet.Columns("1,2,5,6,9:13").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                RateSheet.Columns("1:2,9:13").SetDataType(XLCellValues.Number)
                RateSheet.Columns("3:4").SetDataType(XLCellValues.Text)
                RateSheet.Columns("7:8").SetDataType(XLCellValues.DateTime)
                RateSheet.Columns("7, 8").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                For Each rng As IXLCell In RateSheet.Columns("5:6,9:13").Cells
                    If rng.Value = 0 Then
                        rng.Value = ""
                    End If
                Next
            Next

            Dim style1 As IXLBorder = RateSheet.Cells.Style.Border
            style1.BottomBorder = XLBorderStyleValues.Thin
            style1.LeftBorder = XLBorderStyleValues.Thin


            ws.Cell(LastLine, 1).Value = "Total"
            ws.Row(LastLine).Style.Font.FontName = "Times New Roman"
            ws.Row(LastLine).Style.Font.FontSize = "12"
            ws.Row(LastLine).Style.Font.Bold = True

            ws.Cell(LastLine, 10).Value = ""
            ws.Cell(LastLine, 12).Value = TotalHotelDt(0).Item("hotelcostvalue")

            ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            ws.Range(LastLine, 1, LastLine, 13).Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
        End If






        Dim cnt As Integer = LastLine + 3
        If SplevtDt.Rows.Count > 0 Then


            Dim SplEventRange As IXLRange
            SplEventRange = ws.Range(cnt, 1, cnt + SplevtDt.Rows.Count + 1, 10)
            ws.Range(cnt, 1, cnt, 10).Merge().Value = "Special-Events"
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "14"
            ws.Row(cnt).Style.Font.Bold = True
            Dim script As IXLStyle = ws.Range(cnt, 1, cnt, 10).Style
            script.Fill.BackgroundColor = XLColor.FromArgb(240, 230, 198)
            cnt = cnt + 1
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(cnt).Style.Font.Bold = True
            ws.Cell(cnt, 1).Value = "Rowid"
            ws.Cell(cnt, 2).Value = "Room No."
            ws.Cell(cnt, 3).Value = "Hotel Name"
            ws.Cell(cnt, 4).Value = "Special Event Name"
            ws.Cell(cnt, 5).Value = "Pax Type"
            ws.Cell(cnt, 6).Value = "Child Age"
            ws.Cell(cnt, 7).Value = "Spl Event Date"
            ws.Cell(cnt, 7).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 8).Value = "No. Of Pax"
            ws.Cell(cnt, 9).Value = "Pax Cost"
            ws.Cell(cnt, 10).Value = "SplEvent Cost Val"
            ws.Cell(cnt, 10).Style.Alignment.SetWrapText(True)
            Dim scriptheader As IXLStyle = ws.Range(cnt, 1, cnt, 10).Style
            scriptheader.Fill.BackgroundColor = XLColor.BabyPink
            Dim RateSheet1 As IXLRange
            LastLine = cnt + 1
            RateSheet1 = ws.Cell(LastLine, 1).InsertData(SplevtDt.AsEnumerable).AsRange()
            ws.Columns("1,3:5,13").AdjustToContents()
            Dim colList As New List(Of Integer)

            SplEventRange.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            SplEventRange.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            SplEventRange.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            SplEventRange.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
            RateSheet1.Columns("1,2,6,8:10").SetDataType(XLCellValues.Number)
            RateSheet1.Columns("7").SetDataType(XLCellValues.DateTime)
            RateSheet1.Columns("7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
            LastLine = LastLine + SplevtDt.Rows.Count
            ws.Cell(LastLine, 1).Value = "Total"
            ws.Row(LastLine).Style.Font.FontName = "Times New Roman"
            ws.Row(LastLine).Style.Font.FontSize = "12"
            ws.Row(LastLine).Style.Font.Bold = True
            ws.Cell(LastLine, 10).Value = TotalSpleventDt(0).Item("spleventcostvalue")
            ws.Range(LastLine, 1, LastLine, 9).Merge()
            ws.Range(LastLine, 1, LastLine, 10).Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            ws.Range(LastLine, 1, LastLine, 10).Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            ws.Range(LastLine, 1, LastLine, 10).Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)

            ws.Range(LastLine, 1, LastLine, 10).Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            ws.Range(LastLine, 1, LastLine, 10).Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
        End If
        cnt = LastLine + 3
        If OthserDt.Rows.Count > 0 Then
            Dim OthservRange As IXLRange
            OthservRange = ws.Range(cnt, 1, cnt + OthserDt.Rows.Count + 2, 8)
            Dim RateSheet As IXLRange
            ws.Range(cnt, 1, cnt, 8).Merge().Value = "All Services"
            ws.Row(cnt).Style.Font.FontSize = "14"
            ws.Row(cnt).Style.Font.Bold = True
            Dim script1 As IXLStyle = ws.Range(cnt, 1, cnt, 8).Style
            script1.Fill.BackgroundColor = XLColor.FromArgb(240, 180, 168)
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(cnt).Style.Font.Bold = True
            cnt = cnt + 1
            ws.Cell(cnt, 1).Value = "Service Type"
            ws.Cell(cnt, 2).Value = "Service Date"
            ws.Cell(cnt, 3).Value = "Service Name"
            ws.Cell(cnt, 4).Value = "Pax Type"
            ws.Cell(cnt, 5).Value = "No.OfPax/Unit"
            ws.Cell(cnt, 5).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 6).Value = "Child Age"
            ws.Cell(cnt, 7).Value = "Pax/UnitAge"
            ws.Cell(cnt, 7).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 8).Value = "Cost Value"
            Dim scriptheader As IXLStyle = ws.Range(cnt, 1, cnt, 8).Style
            scriptheader.Fill.BackgroundColor = XLColor.FromArgb(240, 230, 198)
            LastLine = cnt + 1
            RateSheet = ws.Cell(LastLine, 1).InsertData(OthserDt.AsEnumerable).AsRange()
            ws.Columns("1,3:5,13").AdjustToContents()
            ws.Column(2).Width = "15"
            LastLine = LastLine + OthserDt.Rows.Count
            RateSheet.Columns("2").SetDataType(XLCellValues.DateTime)
            RateSheet.Columns("2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
            RateSheet.Columns("5:8").SetDataType(XLCellValues.Number)
            RateSheet.Columns("5:8").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            OthservRange.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            OthservRange.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            OthservRange.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            OthservRange.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
            ws.Cell(LastLine, 1).Value = "Total"
            ws.Range(LastLine, 1, LastLine, 7).Merge()
            ws.Row(LastLine).Style.Font.FontName = "Times New Roman"
            ws.Row(LastLine).Style.Font.FontSize = "12"
            ws.Row(LastLine).Style.Font.Bold = True
            ws.Cell(LastLine, 8).Value = TotalOthServDt(0).Item("servicecostvalue")
        End If






        cnt = LastLine + 3




        If Totalpaxcost.Rows.Count > 0 Then
            Dim OthservRange As IXLRange
            OthservRange = ws.Range(cnt, 1, cnt + Totalpaxcost.Rows.Count + 1, 2)
            Dim RateSheet As IXLRange
            ws.Range(cnt, 1, cnt, 2).Merge().Value = "Total Adult/Child/Unit"
            ws.Row(cnt).Style.Font.FontSize = "14"
            ws.Row(cnt).Style.Font.Bold = True
            Dim script1 As IXLStyle = ws.Range(cnt, 1, cnt, 2).Style
            script1.Fill.BackgroundColor = XLColor.FromArgb(240, 180, 168)
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(cnt).Style.Font.Bold = True
            cnt = cnt + 1
            ws.Cell(cnt, 1).Value = "Pax Type"

            ws.Cell(cnt, 2).Value = "Cost Value"

            Dim scriptheader As IXLStyle = ws.Range(cnt, 1, cnt, 2).Style
            scriptheader.Fill.BackgroundColor = XLColor.FromArgb(240, 230, 198)
            LastLine = cnt + 1


            RateSheet = ws.Cell(LastLine, 1).InsertData(Totalpaxcost.AsEnumerable).AsRange()

            LastLine = LastLine + Totalpaxcost.Rows.Count
            OthservRange.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            OthservRange.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            OthservRange.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            OthservRange.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)
        End If


        cnt = LastLine + 2



        If UnitAppDt.Rows.Count > 0 Then
            Dim UnitValueRange As IXLRange
            UnitValueRange = ws.Range(cnt, 1, cnt + UnitAppDt.Rows.Count + 1, 7)
            ws.Range(cnt, 1, cnt, 7).Merge().Value = "Unit Value of Services apportioned to Adult and child "
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.Bold = True
            ws.Row(cnt).Style.Font.FontSize = "14"
            Dim script2 As IXLStyle = ws.Range(cnt, 1, cnt, 7).Style
            script2.Fill.BackgroundColor = XLColor.FromArgb(240, 230, 198)
            cnt = cnt + 1
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(cnt).Style.Font.Bold = True
            ws.Cell(cnt, 1).Value = "Pax Type"
            ws.Cell(cnt, 2).Value = "Child Age"
            ws.Cell(cnt, 3).Value = "Cost Value"
            ws.Cell(cnt, 4).Value = "No. Of Pax"
            ws.Cell(cnt, 4).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 5).Value = "Cost Per Pax"
            ws.Cell(cnt, 5).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 6).Value = "Unit Per Pax"
            ws.Cell(cnt, 6).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 7).Value = "Total Per Pax"
            ws.Cell(cnt, 7).Style.Alignment.SetWrapText(True)
            Dim scriptheader As IXLStyle = ws.Range(cnt, 1, cnt, 7).Style
            scriptheader.Fill.BackgroundColor = XLColor.BabyPink
            Dim RateSheet As IXLRange
            LastLine = cnt + 1
            RateSheet = ws.Cell(LastLine, 1).InsertData(UnitAppDt.AsEnumerable).AsRange()
            For Each rng As IXLCell In RateSheet.Columns("2").Cells
                If rng.Value = 0 Then
                    rng.Value = ""
                End If
            Next
            ws.Columns("1,3:5,13").AdjustToContents()
            LastLine = LastLine + UnitAppDt.Rows.Count
            RateSheet.Columns("2:7").SetDataType(XLCellValues.Number)
            RateSheet.Columns("2:7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            UnitValueRange.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            UnitValueRange.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            UnitValueRange.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            UnitValueRange.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)

        End If

        cnt = LastLine + 3


        Dim mergeCalwidth As Integer = 0
        For i = 1 To 4
            mergeCalwidth = ws.Column(i).Width
        Next

        If AdchBrkUpdt.Rows.Count > 0 Then
            Dim AdChBrkRange As IXLRange
            AdChBrkRange = ws.Range(cnt, 1, cnt + AdchBrkUpdt.Rows.Count + 2, 15)
            ws.Range(cnt, 1, cnt, 15).Merge().Value = "Final Cost and Sale Working "
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "14"
            ws.Row(cnt).Style.Font.Bold = True
            Dim script3 As IXLStyle = ws.Range(cnt, 1, cnt, 10).Style
            script3.Fill.BackgroundColor = XLColor.FromArgb(240, 230, 198)
            cnt = cnt + 1
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(cnt).Style.Font.Bold = True
            ws.Cell(cnt, 1).Value = "Stay Details"
            ws.Range(cnt, 1, cnt, 4).Style.Alignment.SetWrapText(True)
            ws.Range(cnt, 1, cnt, 4).Merge()
            ws.Cell(cnt, 5).Value = "No. Of Pax"
            ws.Cell(cnt, 5).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 6).Value = "Child Age"
            ws.Cell(cnt, 7).Value = "Stay Value"
            ws.Cell(cnt, 8).Value = "Services Per Pax"
            ws.Cell(cnt, 9).Value = "Service Value"
            ws.Cell(cnt, 10).Value = "Total Cost Value"
            ws.Cell(cnt, 11).Value = "Per Pax Min. MarkUp"
            ws.Cell(cnt, 12).Value = "Per Pax Additional Mark Up"
            ws.Cell(cnt, 13).Value = "Total Mark Up"
            ws.Cell(cnt, 14).Value = "Sale Value"
            ws.Cell(cnt, 15).Value = "Sale Value USD"
            ws.Range(cnt, 7, cnt, 15).Style.Alignment.SetWrapText(True)
            Dim scriptheader As IXLStyle = ws.Range(cnt, 1, cnt, 15).Style
            scriptheader.Fill.BackgroundColor = XLColor.BabyPink

            For Each rs As DataRow In AdchBrkUpdt.Rows
                cnt = cnt + 1
                ws.Cell(cnt, 1).Value = rs(0)
                ws.Range(cnt, 1, cnt, 4).Merge()
                ws.Range(cnt, 1, cnt, 4).Style.Alignment.SetWrapText(True)
                Dim rowheight As Integer = Math.Ceiling(rs(0).ToString.Length / mergeCalwidth) * 6.0
                If rowheight < 30 Then
                    ws.Row(cnt).Height = rowheight + 8
                Else
                    ws.Row(cnt).Height = rowheight
                End If
                ws.Cell(cnt, 5).Value = rs(1)
                ws.Cell(cnt, 6).Value = rs(2)
                If ws.Cell(cnt, 6).Value = 0 Then
                    ws.Cell(cnt, 6).Value = ""
                End If
                ws.Cell(cnt, 7).Value = rs(3)
                ws.Cell(cnt, 8).Value = rs(4)
                ws.Cell(cnt, 9).Value = rs(5)
                ws.Cell(cnt, 10).Value = rs(6)
                ws.Cell(cnt, 11).Value = rs(7)
                ws.Cell(cnt, 12).Value = rs(8)
                ws.Cell(cnt, 13).Value = rs(9)
                ws.Cell(cnt, 14).Value = rs(10)
                ws.Cell(cnt, 15).Value = rs(11)
            Next

            LastLine = cnt + 1
            ws.Cell(LastLine, 1).Value = "Total"
            ws.Row(LastLine).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(LastLine).Style.Font.Bold = True
            ws.Range(LastLine, 1, LastLine, 4).Merge()

            ws.Cell(LastLine, 10).Value = TotalAdultchbrkdt(0).Item("totalcostvalue")
            ws.Cell(LastLine, 14).Value = TotalAdultchbrkdt(0).Item("salevalue")
            ws.Cell(LastLine, 15).Value = TotalAdultchbrkdt(0).Item("salevalueused")
            ws.Protect(RandomNo)
            ws.Protection.FormatColumns = True
            ws.Protection.FormatRows = True
            'RateSheet.Range(cnt, 1, cnt, colList(8)).Style.Border.SetRightBorder(XLBorderStyleValues.Medium).Border.SetLeftBorder(XLBorderStyleValues.Medium).Border.SetTopBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)

        End If



        'Added by abin on 20181215
        cnt = LastLine + 3


        Dim mergeCalwidth1 As Integer = 0
        For i = 1 To 4
            mergeCalwidth1 = ws.Column(i).Width
        Next

        If dtSupplierToBook.Rows.Count > 0 Then
            Dim AdChBrkRange As IXLRange
            AdChBrkRange = ws.Range(cnt, 1, cnt + dtSupplierToBook.Rows.Count + 2, 11)
            ws.Range(cnt, 1, cnt, 11).Merge().Value = "Supplier to Book "
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "14"
            ws.Row(cnt).Style.Font.Bold = True
            Dim script3 As IXLStyle = ws.Range(cnt, 1, cnt, 10).Style
            script3.Fill.BackgroundColor = XLColor.FromArgb(240, 230, 198)
            cnt = cnt + 1
            ws.Row(cnt).Style.Font.FontName = "Times New Roman"
            ws.Row(cnt).Style.Font.FontSize = "12"
            ws.Row(cnt).Style.Font.Bold = True
            ws.Cell(cnt, 1).Value = "Service Type"
            ws.Cell(cnt, 2).Value = "Service Date"
            ' ws.Cell(cnt, 5).Style.Alignment.SetWrapText(True)
            ws.Cell(cnt, 3).Value = "Service Name"
            ws.Range(cnt, 3, cnt, 5).Style.Alignment.SetWrapText(True)
            ws.Range(cnt, 3, cnt, 5).Merge()
            ws.Cell(cnt, 6).Value = "Pax Type"
            ws.Cell(cnt, 7).Value = "NoOfPax"
            ws.Cell(cnt, 8).Value = "Child No"
            ws.Cell(cnt, 9).Value = "Child Age"
            ws.Cell(cnt, 10).Value = "Pax or Unit Rate"
            ws.Cell(cnt, 11).Value = "Cost Value"
          
            ws.Range(cnt, 7, cnt, 11).Style.Alignment.SetWrapText(True)
            Dim scriptheader As IXLStyle = ws.Range(cnt, 1, cnt, 11).Style
            scriptheader.Fill.BackgroundColor = XLColor.BabyPink

            For Each rs As DataRow In dtSupplierToBook.Rows
                cnt = cnt + 1
                ws.Cell(cnt, 1).Value = rs(0)
                ws.Cell(cnt, 2).Value = rs(1)
                ws.Cell(cnt, 3).Value = rs(2)

                ws.Range(cnt, 3, cnt, 5).Merge()
                ws.Range(cnt, 3, cnt, 5).Style.Alignment.SetWrapText(True)
                Dim rowheight As Integer = Math.Ceiling(rs(0).ToString.Length / mergeCalwidth1) * 6.0
                If rowheight < 30 Then
                    ws.Row(cnt).Height = rowheight + 8
                Else
                    ws.Row(cnt).Height = rowheight
                End If

                'If ws.Cell(cnt, 6).Value = 0 Then
                '    ws.Cell(cnt, 6).Value = ""
                'End If
                ws.Cell(cnt, 6).Value = rs(3)
              
                ws.Cell(cnt, 7).Value = rs(4)
                ws.Cell(cnt, 8).Value = rs(5)
                ws.Cell(cnt, 9).Value = rs(6)
                ws.Cell(cnt, 10).Value = rs(7)
                ws.Cell(cnt, 11).Value = rs(8)
            Next

            '  LastLine = cnt + 1
            'ws.Cell(LastLine, 1).Value = "Total"
            'ws.Row(LastLine).Style.Font.FontName = "Times New Roman"
            'ws.Row(cnt).Style.Font.FontSize = "12"
            'ws.Row(LastLine).Style.Font.Bold = True
            'ws.Range(LastLine, 1, LastLine, 4).Merge()

            'ws.Cell(LastLine, 10).Value = TotalAdultchbrkdt(0).Item("totalcostvalue")
            'ws.Cell(LastLine, 14).Value = TotalAdultchbrkdt(0).Item("salevalue")
            'ws.Cell(LastLine, 15).Value = TotalAdultchbrkdt(0).Item("salevalueused")
            'ws.Protect(RandomNo)
            'ws.Protection.FormatColumns = True
            'ws.Protection.FormatRows = True
            'RateSheet.Range(cnt, 1, cnt, colList(8)).Style.Border.SetRightBorder(XLBorderStyleValues.Medium).Border.SetLeftBorder(XLBorderStyleValues.Medium).Border.SetTopBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetRightBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetLeftBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetTopBorder(XLBorderStyleValues.Medium)
            AdChBrkRange.Style.Border.SetBottomBorder(XLBorderStyleValues.Medium)

        End If




        Try
            Using MyMemoryStream As New MemoryStream()
                document.SaveAs(MyMemoryStream)
                document.Dispose()
                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.Buffer = True
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileNameNew)
                HttpContext.Current.Response.AddHeader("Content-Length", MyMemoryStream.Length.ToString())
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream)
                HttpContext.Current.Response.Cookies.Add(New HttpCookie("Downloaded", "True"))
                HttpContext.Current.Response.Flush()
                HttpContext.Current.ApplicationInstance.CompleteRequest()


            End Using
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Public Sub GenerateCostingReport(ByVal quoteID As String, ByRef bytes() As Byte)"
    Public Sub GenerateCostingReport(ByVal quoteID As String, ByRef bytes() As Byte)
        Try
            Dim sqlConn As New SqlConnection
            Dim mySqlCmd As SqlCommand
            Dim myDataAdapter As New SqlDataAdapter
            Dim ds As New DataSet
            sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            mySqlCmd = New SqlCommand("sp_booking_quote_costing_print", sqlConn)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            mySqlCmd.Parameters.Add(New SqlParameter("@quoteid", SqlDbType.VarChar, 20)).Value = quoteID
            myDataAdapter.SelectCommand = mySqlCmd
            myDataAdapter.Fill(ds)
            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim othServiceDt As DataTable = ds.Tables(2)
            clsDBConnect.dbConnectionClose(sqlConn)
            If headerDt.Rows.Count > 0 Then
                Dim document As New Document(PageSize.A4, 20.0F, 20.0F, 30.0F, 35.0F)
                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate)
                Dim documentWidth As Single = 770.0F
                Dim TitleFont As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBig As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
                Dim TitleFontBigBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
                Dim TitleFontBoldUnderLine As Font = FontFactory.GetFont("Arial", 11, Font.BOLD Or Font.UNDERLINE, BaseColor.BLACK)
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

                    '----- Title ------------
                    Dim package As String = ""
                    If Convert.ToString(headerDr("package")).Trim <> "" Then
                        package = Convert.ToString(headerDr("package"))
                    Else
                        Dim findPackage = (From n In othServiceDt.AsEnumerable() Where n.Field(Of Integer)("adults") <> Nothing Or n.Field(Of Integer)("child") <> Nothing Select n Order By n.Field(Of Integer)("adults") Descending, n.Field(Of Integer)("child") Descending).CopyToDataTable
                        If findPackage.Rows.Count > 0 Then
                            Dim dr As DataRow = findPackage.Rows(0)
                            If dr.IsNull("adults") = False Then package = Convert.ToString(dr("adults")) + " adults"
                            If dr.IsNull("child") = False Then
                                If Convert.ToInt32(dr("child")) > 0 Then
                                    If package <> "" Then
                                        package = package + " + " + Convert.ToString(dr("child")) + " child"
                                    Else
                                        package = Convert.ToString(dr("child")) + " child"
                                    End If
                                    If Convert.ToString(dr("childage")).Trim <> "" Then
                                        package = package + " (" + dr("childage") + ")"
                                    End If
                                End If
                            End If
                        End If
                    End If
                    If package <> "" Then package = " (" & package & ")"
                    Dim tblTitle As PdfPTable = New PdfPTable(2)
                    tblTitle.TotalWidth = documentWidth
                    tblTitle.LockedWidth = True
                    tblTitle.SetWidths(New Single() {0.5F, 0.5F})
                    tblTitle.Complete = False
                    tblTitle.SplitRows = False
                    phrase = New Phrase()
                    phrase.Add(New Chunk("COSTING - QUOTE " & quoteID & package, TitleFontBigBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    cell.PaddingTop = 3.0F
                    cell.PaddingBottom = 6.0F
                    cell.BackgroundColor = titleColor
                    tblTitle.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Date : " & Convert.ToString(Convert.ToDateTime(headerDr("requestDate")).ToString("dd/MM/yyyy")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    cell.PaddingTop = 3.0F
                    cell.PaddingBottom = 5.0F
                    cell.PaddingLeft = 5.0F
                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.BOTTOM_BORDER
                    tblTitle.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("Agent Ref. : " & Convert.ToString(headerDr("agentRef")), NormalFont))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 5.0F
                    cell.PaddingBottom = 5.0F
                    cell.Border = Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
                    tblTitle.AddCell(cell)
                    tblTitle.Complete = True
                    tblTitle.SpacingBefore = 10.0F
                    document.Add(tblTitle)

                    Dim tblheader As PdfPTable = New PdfPTable(1)
                    tblheader.SetWidths(New Single() {1.0F})
                    tblheader.TotalWidth = documentWidth
                    tblheader.LockedWidth = True
                    tblheader.SplitRows = False

                    phrase = New Phrase()
                    phrase.Add(New Chunk("COSTING - QUOTE " & quoteID & package, TitleFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_MIDDLE, 2, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER
                    cell.PaddingTop = 3.0F
                    cell.PaddingBottom = 5.0F
                    tblheader.AddCell(cell)
                    Dim printMode As String = "download"
                    writer.PageEvent = New clsBookingConfirmPageEvents(tblheader, printMode)

                    If hotelDt.Rows.Count > 0 Then
                        Dim arrServ() As String = {"Hotel", "Room Type", "Check In", "Check Out", "Nights", "Units", "Cost Per Unit", "Total Cost " & Convert.ToString(headerDr("basecurrCode"))}
                        Dim tblServ As PdfPTable = New PdfPTable(8)
                        tblServ.TotalWidth = documentWidth
                        tblServ.LockedWidth = True
                        tblServ.SetWidths(New Single() {0.19F, 0.27F, 0.1F, 0.1F, 0.07F, 0.07F, 0.1F, 0.1F})
                        tblServ.Complete = False
                        tblServ.HeaderRows = 1
                        tblServ.SplitRows = False
                        For i = 0 To 7
                            phrase = New Phrase()
                            phrase.Add(New Chunk(arrServ(i), NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 4.0F
                            cell.PaddingTop = 1.0F
                            cell.BackgroundColor = subTitleColor
                            tblServ.AddCell(cell)
                        Next
                        For Each hotelDr As DataRow In hotelDt.Rows
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("partyName")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 3.0F
                            cell.PaddingLeft = 4.0F
                            tblServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("roomDetail")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP
                            cell.PaddingBottom = 3.0F
                            cell.PaddingLeft = 4.0F
                            tblServ.AddCell(cell)

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
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("Nights")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 2.0F
                            tblServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("Units")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 2.0F
                            tblServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("Unitvalue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingRight = 4.0F
                            cell.PaddingBottom = 2.0F
                            tblServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(hotelDr("totalCostValue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 2.0F
                            cell.PaddingRight = 4.0F
                            tblServ.AddCell(cell)

                            If (Convert.ToString(hotelDr("RatePlanName")) <> "") Then
                                phrase = New Phrase()
                                phrase.Add(New Chunk("Rate Plan : ", NormalFontBold))
                                phrase.Add(New Chunk(Convert.ToString(hotelDr("RatePlanName")), NormalFont))
                                cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                                cell.Colspan = 8
                                cell.PaddingBottom = 3.0F
                                cell.PaddingLeft = 4.0F
                                tblServ.AddCell(cell)
                            End If
                        Next
                        tblServ.Complete = True
                        tblServ.SpacingBefore = 10
                        document.Add(tblServ)
                    End If

                    If othServiceDt.Rows.Count > 0 Then
                        Dim OthServ() As String = {"Other Services", "Pax / Unit", "Service Day", "Service Date", "Cost Adult", "Cost Senior", "Cost Child", "Cost Per Unit", "Total Cost " & Convert.ToString(headerDr("basecurrCode"))}
                        Dim tblOthServ As PdfPTable = New PdfPTable(9)
                        tblOthServ.TotalWidth = documentWidth
                        tblOthServ.LockedWidth = True
                        tblOthServ.SetWidths(New Single() {0.39F, 0.08F, 0.08F, 0.08F, 0.07F, 0.07F, 0.07F, 0.07F, 0.09F})
                        tblOthServ.SplitRows = False
                        tblOthServ.Complete = False
                        tblOthServ.HeaderRows = 1
                        For i = 0 To 8
                            phrase = New Phrase()
                            phrase.Add(New Chunk(OthServ(i), NormalFontBold))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 4.0F
                            cell.PaddingTop = 1.0F
                            cell.BackgroundColor = subTitleColor
                            tblOthServ.AddCell(cell)
                        Next

                        For Each othServDr As DataRow In othServiceDt.Rows
                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("ServiceName")), NormalFont))
                            If Convert.ToString(othServDr("pickupdropoff")).Trim <> "" Then
                                phrase.Add(New Chunk(vbCrLf & Convert.ToString(othServDr("pickupdropoff")), NormalFont))
                            End If
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 4.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("PAX")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 4.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("ServiceDay")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 5.0F
                            cell.PaddingLeft = 4.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("ServiceDate")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 5.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("adultcostvalue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingRight = 4.0F
                            cell.PaddingBottom = 5.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("seniorcostvalue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingRight = 4.0F
                            cell.PaddingBottom = 5.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("childcostvalue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingRight = 4.0F
                            cell.PaddingBottom = 5.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("unitcostvalue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingRight = 4.0F
                            cell.PaddingBottom = 5.0F
                            tblOthServ.AddCell(cell)

                            phrase = New Phrase()
                            phrase.Add(New Chunk(Convert.ToString(othServDr("totalCostValue")), NormalFont))
                            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                            cell.PaddingBottom = 5.0F
                            cell.PaddingRight = 4.0F
                            tblOthServ.AddCell(cell)
                        Next

                        tblOthServ.Complete = True
                        tblOthServ.SpacingBefore = 10
                        remainingPageSpace = writer.GetVerticalPosition(False) - document.BottomMargin
                        If remainingPageSpace < 72 Then document.NewPage()
                        document.Add(tblOthServ)
                    End If

                    Dim tblTotal As PdfPTable = New PdfPTable(2)
                    tblTotal.TotalWidth = documentWidth
                    tblTotal.LockedWidth = True
                    tblTotal.SetWidths(New Single() {0.9F, 0.1F})
                    tblTotal.KeepTogether = True

                    phrase = New Phrase()
                    phrase.Add(New Chunk("TOTAL COST (IN " + Convert.ToString(headerDr("basecurrcode")) + ")", NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 15.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("Totalcostvalue")), NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("TOTAL COST (IN " + Convert.ToString(headerDr("Currcode")) + ")", NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 15.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("CurrCostValue")), NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("TOTAL NET SALE VALUE ( Gross sale – Discount ) (IN " + Convert.ToString(headerDr("Currcode")) + ")", NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 15.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("netsalevalue")), NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk("PROFIT (IN " + Convert.ToString(headerDr("Currcode")) + ")", NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 15.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    phrase = New Phrase()
                    phrase.Add(New Chunk(Convert.ToString(headerDr("profit")), NormalFontBold))
                    cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT, 1, True)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.PaddingTop = 3.0F
                    cell.PaddingRight = 4.0F
                    cell.PaddingBottom = 3.0F
                    cell.BackgroundColor = totalbackColor
                    tblTotal.AddCell(cell)

                    tblTotal.SpacingBefore = 10
                    document.Add(tblTotal)

                    document.AddTitle("Quote Costing-" & quoteID)
                    document.Close()
                    Dim pagingFont As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.GRAY)
                    Dim reader As New PdfReader(memoryStream.ToArray())
                    Using mStream As New MemoryStream()
                        Using stamper As New PdfStamper(reader, mStream)
                            Dim pages As Integer = reader.NumberOfPages
                            For i As Integer = 1 To pages
                                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, New Phrase("Page " + i.ToString() + " of " + pages.ToString(), pagingFont), 385.0F, 20.0F, 0)
                            Next
                        End Using
                        bytes = mStream.ToArray()
                    End Using
                End Using
            Else
                Throw New Exception("There is no rows in header table")
            End If
        Catch ex As Exception
            Throw
        End Try
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

End Class
