Imports System.Data
Imports System.Data.SqlClient


Partial Class BookingHotelConfirm
    Inherits System.Web.UI.Page

#Region "Global Variables"
    Dim objclsUtilities As New clsUtilities
    Dim requestid As String
#End Region

#Region " Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim ids As String = Request.QueryString("ids")
            If ids <> "" Then
                Dim crypto As New clsHotelCryptography()
                Dim combineParam As String = crypto.Decrypt(ids)
                Dim paramArr() As String = combineParam.Split("~")
                requestid = paramArr(0)
                Dim partyCode As String = paramArr(1) '"FO05" '-"SU07"
                Dim amended As Integer = Convert.ToInt32(paramArr(2))
                Dim cancelled As Integer = Convert.ToInt32(paramArr(3))
                Dim pwd As String = Convert.ToString(paramArr(4))
                Dim user As String = requestid.Replace("/", "")
                Dim BookingTimeLimit As Integer = Convert.ToInt32(objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1502"))
                Dim sqlConn As New SqlConnection
                Dim mySqlCmd As SqlCommand
                sqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                Dim strqry = "select requestDate,(datediff(mi,requestDate,getdate())/60.0) as RunningHours from booking_hotel_request where RequestID=@requestID and PartyCode=@partyCode"
                mySqlCmd = New SqlCommand(strqry, sqlConn)
                mySqlCmd.CommandType = CommandType.Text
                mySqlCmd.Parameters.Add(New SqlParameter("@RequestID", SqlDbType.VarChar, 20)).Value = requestid
                mySqlCmd.Parameters.Add(New SqlParameter("@PartyCode", SqlDbType.VarChar, 20)).Value = partyCode
                Dim mySqlReader As SqlDataReader
                mySqlReader = mySqlCmd.ExecuteReader
                If mySqlReader.Read() Then
                    Dim runningHours As Integer = Convert.ToInt32(mySqlReader("RunningHours"))
                    If runningHours <= BookingTimeLimit Then
                        BookingHotelPrint(requestid, partyCode, amended, cancelled)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('This confirmation link is not valid because It reached the time limit.' );", True)
                    End If
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('Invalid URL' );", True)
                Exit Sub
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("BookingHotelConfirm.aspx :: FormLoad :: " & ex.Message & ":: " & requestid)
        End Try
    End Sub
#End Region

#Region "Public Sub BookingHotelPrint(ByVal requestID As String, ByVal partyCode As String, ByVal amended As Integer, ByVal cancelled As Integer)"
    Public Sub BookingHotelPrint(ByVal requestID As String, ByVal partyCode As String, ByVal amended As Integer, ByVal cancelled As Integer)
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
            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim guestDt As DataTable = ds.Tables(3)
            Dim inventoryDt As DataTable = ds.Tables(4)
            Dim contactDt As DataTable = ds.Tables(5)
            Dim splEventDt As DataTable = ds.Tables(6)
            clsDBConnect.dbConnectionClose(sqlConn)
            If headerDt.Rows.Count > 0 Then
                Dim mainTable As HtmlTable = New HtmlTable()
                mainTable.Attributes.Add("class", "TableStyle")
                Dim tblRow As New HtmlTableRow
                Dim tblCol As New HtmlTableCell
                Dim tblLogo As New HtmlTable
                '----------------Logo & Address------------------------------
                Dim headerDr As DataRow = headerDt.Rows(0)
                tblCol.InnerText = Convert.ToString(headerDr("division_master_des"))
                tblCol.ColSpan = 2
                tblCol.Attributes.Add("class", "TitleFont")
                tblRow.Cells.Add(tblCol)
                tblLogo.Rows.Add(tblRow)
                Dim logo As New HtmlImage
                If (headerDr("div_code") = "01") Then
                    logo.Src = "~/img/mahce_logo.jpg"
                Else
                    logo.Src = "~/img/mahce_logo.jpg"
                End If
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.Controls.Add(logo)
                tblCol.RowSpan = 5
                tblRow.Cells.Add(tblCol)
                tblCol = New HtmlTableCell
                tblCol.InnerText = Convert.ToString(headerDr("address1"))
                tblRow.Cells.Add(tblCol)
                tblLogo.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Fax : " & Convert.ToString(headerDr("fax"))
                tblRow.Cells.Add(tblCol)
                tblLogo.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Tel : " & Convert.ToString(headerDr("tel"))
                tblRow.Cells.Add(tblCol)
                tblLogo.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "E-mail : " & Convert.ToString(headerDr("email"))
                tblRow.Cells.Add(tblCol)
                tblLogo.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Website : " & Convert.ToString(headerDr("website"))
                tblRow.Cells.Add(tblCol)
                tblLogo.Rows.Add(tblRow)
                Dim MainTblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.Controls.Add(tblLogo)
                tblCol.Attributes.Add("class", "TableTd")
                MainTblRow.Cells.Add(tblCol)
                '----------------Hotel Details------------------------------
                Dim tblClient As New HtmlTable
                tblClient.Attributes.Add("class", "ClientStyle")
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = Convert.ToString(headerDr("PartyName"))
                tblCol.Attributes.Add("class", "TitleFont")
                tblRow.Cells.Add(tblCol)
                tblClient.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Fax : " & Convert.ToString(headerDr("fax1"))
                tblRow.Cells.Add(tblCol)
                tblClient.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Tel : " & Convert.ToString(headerDr("Tel1"))
                tblRow.Cells.Add(tblCol)
                tblClient.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Email : " & Convert.ToString(headerDr("email1"))
                tblRow.Cells.Add(tblCol)
                tblClient.Rows.Add(tblRow)
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = "Attn. : " & Convert.ToString(headerDr("Contact1"))
                tblRow.Cells.Add(tblCol)
                tblClient.Rows.Add(tblRow)
                tblCol = New HtmlTableCell
                tblCol.Controls.Add(tblClient)
                tblCol.Attributes.Add("class", "TableTd")
                MainTblRow.Cells.Add(tblCol)
                mainTable.Rows.Add(MainTblRow)
                MainDiv.Controls.Add(mainTable)
                '--------------------- Title -----------------------
                Dim tblTitle As New HtmlTable
                tblTitle.Attributes.Add("class", "TableStyle")
                tblRow = New HtmlTableRow
                tblCol = New HtmlTableCell
                tblCol.InnerText = Convert.ToString(headerDr("printHeader"))
                tblCol.Attributes.Add("class", "TitleFontBig")
                tblRow.Cells.Add(tblCol)
                tblTitle.Rows.Add(tblRow)
                MainDiv.Controls.Add(tblTitle)
                '--------------------- Header -----------------------
                Dim divHeader As New HtmlGenericControl("Div")
                divHeader.Attributes.Add("align", "left")
                Dim tblHeader As New HtmlTable
                tblHeader.CellPadding = 3
                tblHeader.Attributes.Add("class", "HeaderStyle")
                tblRow = New HtmlTableRow
                Dim arrHeader() As String = {"Booking Ref. No.", headerDr("requestID").ToString(), "Agent Ref. No.", headerDr("agentRef"), "Booking Request Date", headerDr("requestDate")}
                For i = 0 To 5
                    tblCol = New HtmlTableCell
                    tblCol.InnerText = arrHeader(i)
                    tblCol.Attributes.Add("class", "HeaderTd")
                    tblRow.Cells.Add(tblCol)
                    If i Mod 2 <> 0 Then
                        tblHeader.Rows.Add(tblRow)
                        tblRow = New HtmlTableRow
                    End If
                Next
                divHeader.Controls.Add(tblHeader)
                MainDiv.Controls.Add(divHeader)
                '---------------Room Details----------------------------------------
                If hotelDt.Rows.Count > 0 Then
                    Dim ConfirmVal As String = ""
                    Dim roomCnt As Integer = 0
                    For Each hotelDr As DataRow In hotelDt.Rows
                        roomCnt = roomCnt + 1
                        Dim tblServ As New HtmlTable
                        tblServ.Attributes.Add("class", "TableStyle")
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "ROOM " & roomCnt & " DETAILS"
                        tblCol.ColSpan = 6
                        tblCol.Attributes.Add("class", "TitleFont")
                        tblRow.Cells.Add(tblCol)
                        tblServ.Rows.Add(tblRow)
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Check In"
                        tblCol.Attributes.Add("class", "ServTitleTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(hotelDr("CheckIn"))
                        tblCol.Attributes.Add("class", "ServTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Check Out"
                        tblCol.Attributes.Add("class", "ServTitleTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(hotelDr("CheckOut"))
                        tblCol.Attributes.Add("class", "ServTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Nights"
                        tblCol.Attributes.Add("class", "ServTitleTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(hotelDr("Nights")) & " Nights"
                        tblCol.Attributes.Add("class", "ServTd")
                        tblRow.Cells.Add(tblCol)
                        tblServ.Rows.Add(tblRow)
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Meal Plan"
                        tblCol.Attributes.Add("class", "ServTitleTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(hotelDr("mealCode"))
                        tblCol.ColSpan = 5
                        tblCol.Attributes.Add("class", "ServTd")
                        tblRow.Cells.Add(tblCol)
                        tblServ.Rows.Add(tblRow)
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Room Type"
                        tblCol.Attributes.Add("class", "ServTitleTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(hotelDr("rmTypName"))
                        tblCol.ColSpan = 5
                        tblCol.Attributes.Add("class", "ServTd")
                        tblRow.Cells.Add(tblCol)
                        tblServ.Rows.Add(tblRow)
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Occupancy"
                        tblCol.Attributes.Add("class", "ServTitleTd")
                        tblRow.Cells.Add(tblCol)
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(hotelDr("roomDetails")) & " (" & Convert.ToString(hotelDr("occupancy")) & ")"
                        tblCol.ColSpan = 5
                        tblCol.Attributes.Add("class", "ServTd")
                        tblRow.Cells.Add(tblCol)
                        tblServ.Rows.Add(tblRow)
                        MainDiv.Controls.Add(tblServ)
                        '----------------------Guest Details------------------------------------------
                        Dim rLineNo As Integer = Convert.ToInt32(hotelDr("rLineNo"))
                        Dim roomNo As Integer = Convert.ToInt32(hotelDr("roomNo"))
                        Dim filterRows = (From n In guestDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n).ToList()
                        Dim filterGuest As New DataTable
                        If (filterRows.Count > 0) Then filterGuest = filterRows.CopyToDataTable()
                        If filterGuest.Rows.Count > 0 Then
                            Dim tblGuest As New HtmlTable()
                            tblGuest.Attributes.Add("class", "TableStyle")
                            tblGuest.Style("margin-top") = "0"
                            tblRow = New HtmlTableRow
                            Dim arrGuest() As String = {"Guest Names", String.Format("Arrival{0}Date", vbCrLf), "Arrival Flight", "Arrival Time", "Departure Date", "Departure Flight", "Departure Time"}
                            For i = 0 To 6
                                tblCol = New HtmlTableCell
                                tblCol.InnerText = arrGuest(i)
                                tblCol.Attributes.Add("class", "ServTitleTd")
                                tblRow.Cells.Add(tblCol)
                            Next
                            tblGuest.Rows.Add(tblRow)
                            Dim guestval As String = ""
                            For Each guestDr In filterGuest.Rows
                                tblRow = New HtmlTableRow
                                tblCol = New HtmlTableCell
                                If Not String.IsNullOrEmpty(guestDr("childage")) And Convert.ToString(guestDr("childage")) <> "0.00" Then
                                    guestval = Convert.ToString(guestDr("guestName")) + " (" + Convert.ToString(guestDr("childage")) + " yrs)"
                                Else
                                    guestval = Convert.ToString(guestDr("guestName"))
                                End If
                                tblCol.InnerText = guestval
                                tblCol.Attributes.Add("class", "ServTd")
                                tblCol.Width = "30%"
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(guestDr("arrDate"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(guestDr("arrFlightCode"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(guestDr("arrFlightTime"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(guestDr("depDate"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(guestDr("depFlightCode"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(guestDr("depFlightTime"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)
                                tblGuest.Rows.Add(tblRow)
                            Next
                            MainDiv.Controls.Add(tblGuest)
                        End If
                        '-----------------------------------Tariff Details---------------------------------
                        Dim FinalTotal As Decimal = 0.0
                        Dim filterTariffRows = (From n In tariffDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n Order By Convert.ToDateTime(n.Field(Of String)("fromdate")) Ascending).ToList()
                        Dim filterTariffDt As New DataTable
                        If (filterTariffRows.Count > 0) Then filterTariffDt = filterTariffRows.CopyToDataTable()
                        If filterTariffDt.Rows.Count > 0 Then
                            Dim tblTariff As New HtmlTable
                            tblTariff.Attributes.Add("class", "TableStyle")
                            tblTariff.Style("margin-top") = "0"
                            tblRow = New HtmlTableRow
                            'Dim arrServ() As String = {"Offer Code", "From", "To", "Per Night Rate (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")", "Room Nights", "Total (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")"}
                            Dim arrServ() As String = {"Offer Code", "From", "To", "Room Nights"}
                            For i = 0 To 3 '5
                                tblCol = New HtmlTableCell
                                tblCol.InnerText = arrServ(i)
                                tblCol.Attributes.Add("class", "ServTitleTd")
                                'tblCol.Style("border-top-width") = "0px"
                                tblRow.Cells.Add(tblCol)
                            Next
                            tblTariff.Rows.Add(tblRow)
                            Dim totalNights As Integer = 0
                            Dim totalValue As Decimal = 0.0
                            For Each tariffDr As DataRow In filterTariffDt.Rows
                                tblRow = New HtmlTableRow
                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(tariffDr("bookingCode"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblCol.Width = "35%"
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(tariffDr("fromDate"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(tariffDr("toDate"))
                                tblCol.Attributes.Add("class", "ServTd")
                                tblRow.Cells.Add(tblCol)

                                'tblCol = New HtmlTableCell
                                'tblCol.InnerText = "" ' Convert.ToString(tariffDr("costPrice"))   // Hide value temporarily for VAT implementation
                                'tblCol.Attributes.Add("class", "TariffTd")
                                'tblRow.Cells.Add(tblCol)

                                tblCol = New HtmlTableCell
                                tblCol.InnerText = Convert.ToString(tariffDr("nights"))
                                tblCol.Attributes.Add("class", "TariffTd")
                                tblRow.Cells.Add(tblCol)

                                'tblCol = New HtmlTableCell
                                'tblCol.InnerText = "" ' Convert.ToString(tariffDr("costValue"))     // Hide value temporarily for VAT implementation
                                'tblCol.Attributes.Add("class", "TariffTd")
                                'tblCol.Width = "12%"
                                'tblRow.Cells.Add(tblCol)

                                tblTariff.Rows.Add(tblRow)
                                totalNights = totalNights + Convert.ToInt32(tariffDr("nights"))
                                totalValue = totalValue + Convert.ToDecimal(tariffDr("costValue"))
                            Next
                            tblRow = New HtmlTableRow
                            tblCol = New HtmlTableCell
                            'tblCol.InnerText = "Total Nights / Rate (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")"
                            tblCol.InnerText = "Total Nights"
                            tblCol.ColSpan = 3 '4
                            tblCol.Attributes.Add("class", "TariffTotalTd")
                            tblCol.Align = "Center"
                            tblRow.Cells.Add(tblCol)

                            tblCol = New HtmlTableCell
                            tblCol.InnerText = Convert.ToString(totalNights)
                            tblCol.Attributes.Add("class", "TariffTotalTd")
                            tblCol.BgColor = "#d6d6d6"
                            tblCol.Align = "Right"
                            tblCol.Style("padding-right") = "5pt"
                            tblRow.Cells.Add(tblCol)

                            'tblCol = New HtmlTableCell
                            'tblCol.InnerText = "" ' Convert.ToString(totalValue)      // Hide value temporarily for VAT implementation
                            'tblCol.Attributes.Add("class", "TariffTotalTd")
                            'tblCol.BgColor = "#d6d6d6"
                            'tblCol.Align = "Right"
                            'tblCol.Style("padding-right") = "5pt"
                            'tblRow.Cells.Add(tblCol)

                            tblTariff.Rows.Add(tblRow)
                            FinalTotal = FinalTotal + totalValue
                            MainDiv.Controls.Add(tblTariff)
                        End If

                        '--------------Special Event-------------------------------------------
                        Dim filterSplEventRows = (From n In splEventDt.AsEnumerable() Where n.Field(Of Int32)("rLineNo") = rLineNo And n.Field(Of Int32)("roomNo") = roomNo Select n).ToList()
                        Dim filterSplEventDt As New DataTable
                        If (filterSplEventRows.Count > 0) Then filterSplEventDt = filterSplEventRows.CopyToDataTable()
                        If filterSplEventDt.Rows.Count > 0 Then
                            Dim tblSplEvent As New HtmlTable
                            tblSplEvent.Attributes.Add("class", "TableStyle")
                            Dim currCode As String = Convert.ToString(headerDr("costCurrCode"))
                            SpecialEvents(filterSplEventDt, currCode, FinalTotal, tblSplEvent)
                            MainDiv.Controls.Add(tblSplEvent)
                        End If

                        ''--------------Final Total-------------------------------------------
                        'Dim tblFinalTotal As New HtmlTable
                        'tblFinalTotal.Attributes.Add("class", "TableStyle")
                        'tblRow = New HtmlTableRow
                        'tblCol = New HtmlTableCell
                        'tblCol.InnerText = "Total Amount (" & Convert.ToString(headerDr("costCurrCode")).Trim & ")"
                        'tblCol.Attributes.Add("class", "TariffTotalTd")
                        'tblCol.Align = "Center"
                        'tblCol.Width = "80%"
                        'tblRow.Cells.Add(tblCol)

                        'tblCol = New HtmlTableCell
                        'tblCol.InnerText = "" ' Convert.ToString(FinalTotal)      // Hide value temporarily for VAT implementation
                        'tblCol.Attributes.Add("class", "TariffTotalTd")
                        'tblCol.BgColor = "#d6d6d6"
                        'tblCol.Align = "Right"
                        'tblCol.Style("padding-right") = "5pt"
                        'tblCol.Width = "20%"
                        'tblRow.Cells.Add(tblCol)

                        'tblFinalTotal.Rows.Add(tblRow)
                        'MainDiv.Controls.Add(tblFinalTotal)

                        '--------------Note-------------------------------------------
                        Dim tblNote As New HtmlTable()
                        tblNote.Attributes.Add("class", "tblNotecss")
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Note :"
                        tblCol.Attributes.Add("class", "NoteStyle")
                        tblRow.Cells.Add(tblCol)
                        tblNote.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Payment will be made as per above booking and agreed conditions as per contract."
                        tblCol.Attributes.Add("class", "NoteRowStyle")
                        tblRow.Cells.Add(tblCol)
                        tblNote.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Tourism Dirham & All Extras to be collected from the Guest Directly."
                        tblCol.Attributes.Add("class", "NoteRowStyle")
                        tblRow.Cells.Add(tblCol)
                        tblNote.Rows.Add(tblRow)
                        MainDiv.Controls.Add(tblNote)

                        '-------------- Bill Instruction -------------------------------------------
                        Dim tblBill As New HtmlTable()
                        tblBill.Attributes.Add("class", "BillStyle")
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.ColSpan = 2
                        tblCol.InnerText = "Billing Instruction For VAT Compliance"
                        tblCol.Attributes.Add("class", "BillHeader")
                        tblCol.Attributes.Add("align", "Center")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "1"
                        tblCol.Attributes.Add("class", "BillStyletd")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)

                        tblCol = New HtmlTableCell
                        If (headerDr("div_code") = "01") Then
                            tblCol.InnerHtml = "Invoice to be under the name of <b>Mohd Al Humaidi Computer  Tourism LLC</b>"
                        Else
                            tblCol.InnerHtml = "Invoice to be under the name of <b>Mohd Al Humaidi Computer  Tourism LLC</b>"
                        End If
                        tblCol.Attributes.Add("class", "BillStyletd")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "2"
                        tblCol.Attributes.Add("class", "BillStyletd")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)

                        tblCol = New HtmlTableCell
                        If (headerDr("div_code") = "01") Then
                            tblCol.InnerHtml = "Please mention our <b>TRN:100227539200003</b> in addition to your TRN number"
                        Else
                            tblCol.InnerHtml = "Please mention our <b>TRN:100379048000003</b> in addition to your TRN number"
                        End If
                        tblCol.Attributes.Add("class", "BillStyletd")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "3"
                        tblCol.Attributes.Add("class", "BillStyletd")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)

                        tblCol = New HtmlTableCell
                        tblCol.InnerHtml = "Please <b>specify the VAT amount charged to us separately</b> on the Invoice"
                        tblCol.Attributes.Add("class", "BillStyletd")
                        tblRow.Cells.Add(tblCol)
                        tblBill.Rows.Add(tblRow)
                        MainDiv.Controls.Add(tblBill)

                        '--------------Confirm-------------------------------------------
                        Dim tblConfirm As New HtmlTable()
                        tblConfirm.Attributes.Add("class", "tblConfirmcss")
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.ColSpan = 3
                        tblCol.Attributes.Add("class", "line")
                        tblRow.Cells.Add(tblCol)
                        tblConfirm.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.ColSpan = 3
                        tblCol.Attributes.Add("class", "line")
                        tblRow.Cells.Add(tblCol)
                        tblConfirm.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "For Hotel Use Only"
                        tblCol.ColSpan = 3
                        tblCol.Attributes.Add("class", "tblConfirmHotel")
                        tblRow.Cells.Add(tblCol)
                        tblConfirm.Rows.Add(tblRow)

                        Dim arrConfirm() As String = {"Confirmation No", "Confirmed By", "Confirmation Date"}
                        tblRow = New HtmlTableRow
                        For i = 0 To 2
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = arrConfirm(i)
                            tblCol.Attributes.Add("class", "tblConfirmTitle")
                            tblRow.Cells.Add(tblCol)
                        Next
                        tblConfirm.Rows.Add(tblRow)

                        Dim FindConf As New BLLHotelConfirm
                        FindConf.HBRequestID = Convert.ToString(headerDr("requestID"))
                        FindConf.HBRlineNo = Convert.ToString(hotelDr("rLineNo"))
                        FindConf.HBRoomNo = Convert.ToString(hotelDr("roomNo"))
                        Dim res As String = FindConf.FetchBookingHotelConfirmation()

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        Dim txtConfNo As New TextBox
                        txtConfNo.ID = "txtConfNo" + roomCnt.ToString()
                        'txtConfNo.AutoPostBack = True
                        'AddHandler txtConfNo.TextChanged, AddressOf txt_TextChanged
                        If res = "True" Then
                            txtConfNo.Text = Convert.ToString(FindConf.HBConfirmationNo)
                        Else
                            txtConfNo.Text = ""
                        End If
                        txtConfNo.Style("width") = "98%"
                        txtConfNo.Attributes.Add("class", "txtCss")
                        tblCol.Controls.Add(txtConfNo)
                        tblCol.Attributes.Add("class", "tblConfirmCol")
                        tblRow.Cells.Add(tblCol)

                        tblCol = New HtmlTableCell
                        Dim txtConfBy As New TextBox
                        txtConfBy.ID = "txtConfBy" + roomCnt.ToString()
                        'txtConfBy.AutoPostBack = True
                        'AddHandler txtConfBy.TextChanged, AddressOf txt_TextChanged
                        If res = "True" Then
                            txtConfBy.Text = Convert.ToString(FindConf.HBConfirmBy)
                        Else
                            txtConfBy.Text = ""
                        End If
                        txtConfBy.Style("width") = "98%"
                        txtConfBy.Attributes.Add("class", "txtCss")
                        tblCol.Controls.Add(txtConfBy)
                        tblCol.Attributes.Add("class", "tblConfirmCol")
                        tblRow.Cells.Add(tblCol)

                        tblCol = New HtmlTableCell
                        Dim txtConfDate As New TextBox
                        txtConfDate.ID = "txtConfDate" + roomCnt.ToString()
                        If res = "True" Then
                            txtConfDate.Text = Convert.ToString(FindConf.HBConfirmationDate)
                        Else
                            txtConfDate.Text = Date.Now.ToString()  '.ToString("dd/MM/yyyy")
                        End If
                        txtConfDate.Style("width") = "98%"
                        txtConfDate.Attributes.Add("class", "txtCss")
                        txtConfDate.Enabled = False
                        tblCol.Controls.Add(txtConfDate)
                        tblCol.Attributes.Add("class", "tblConfirmCol")
                        tblRow.Cells.Add(tblCol)
                        tblConfirm.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.ColSpan = 3
                        tblCol.InnerText = "Remarks (If Any)"
                        tblCol.Attributes.Add("class", "tblConfirmTitle")
                        tblRow.Cells.Add(tblCol)
                        tblConfirm.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.ColSpan = 3
                        Dim txtRemarks As New TextBox
                        txtRemarks.ID = "txtRemarks" + roomCnt.ToString()
                        txtRemarks.Style("width") = "99%"
                        txtRemarks.TextMode = TextBoxMode.MultiLine
                        txtRemarks.Height = 75
                        txtRemarks.Enabled = False
                        txtRemarks.Text = Convert.ToString(hotelDr("hotelremarks"))  '' Added Shahul 15/08/2018
                        tblCol.Controls.Add(txtRemarks)

                        Dim hdfRooms As New HiddenField
                        hdfRooms.ID = "hdfRooms" + roomCnt.ToString()
                        hdfRooms.Value = Convert.ToString(hotelDr("roomNo"))
                        tblCol.Controls.Add(hdfRooms)

                        Dim hdfRLineNo As New HiddenField
                        hdfRLineNo.ID = "hdfRLineNo" + roomCnt.ToString()
                        hdfRLineNo.Value = Convert.ToString(hotelDr("rLineNo"))
                        tblCol.Controls.Add(hdfRLineNo)

                        Dim hdfRoomType As New HiddenField
                        hdfRoomType.ID = "hdfRoomType" + roomCnt.ToString()
                        hdfRoomType.Value = Convert.ToString(hotelDr("rmTypName"))
                        tblCol.Controls.Add(hdfRoomType)

                        tblCol.Attributes.Add("class", "tblConfirmCol")
                        tblRow.Cells.Add(tblCol)
                        tblConfirm.Rows.Add(tblRow)
                        MainDiv.Controls.Add(tblConfirm)

                        If ConfirmVal = "" Then
                            ConfirmVal = "'" + txtConfNo.ClientID + "'"
                        Else
                            ConfirmVal = ConfirmVal + ",'" + txtConfNo.ClientID + "'"
                        End If
                        ConfirmVal = ConfirmVal + ",'" + txtConfBy.ClientID + "'"
                    Next

                    Dim btnDiv As New HtmlGenericControl("Div")
                    btnDiv.Attributes.Add("Align", "Center")
                    btnDiv.Style.Add("margin-top", "5pt")
                    Dim btnSubmit As New Button
                    btnSubmit.ID = "btnSubmit"
                    btnSubmit.Text = "Confirm"
                    'btnSubmit.OnClientClick = "return validate('" + txtConfNo.ClientID + "','" + txtConfBy.ClientID + "')"
                    btnSubmit.EnableViewState = True
                    btnSubmit.OnClientClick = "return validate(" + ConfirmVal + ")"
                    AddHandler btnSubmit.Click, AddressOf btnSubmit_Click
                    btnSubmit.Style("width") = "90pt"
                    btnSubmit.Attributes.Add("class", "btnCss")
                    btnDiv.Controls.Add(btnSubmit)

                    Dim hdfRequestID As New HiddenField
                    hdfRequestID.ID = "hdfRequestID"
                    hdfRequestID.Value = Convert.ToString(headerDr("requestID"))
                    btnDiv.Controls.Add(hdfRequestID)

                    Dim hdfPartyCode As New HiddenField
                    hdfPartyCode.ID = "hdfPartyCode"
                    hdfPartyCode.Value = Convert.ToString(hotelDt.Rows(0)(2))
                    btnDiv.Controls.Add(hdfPartyCode)

                    Dim hdfRoomsCnt As New HiddenField
                    hdfRoomsCnt.ID = "hdfRoomsCnt"
                    hdfRoomsCnt.Value = hotelDt.Rows.Count.ToString()
                    btnDiv.Controls.Add(hdfRoomsCnt)

                    Dim hdfCcAddressCode As New HiddenField
                    hdfCcAddressCode.ID = "hdfCcAddressCode"
                    hdfCcAddressCode.Value = Convert.ToString(headerDr("div_code"))
                    btnDiv.Controls.Add(hdfCcAddressCode)

                    Dim hdfPartyName As New HiddenField
                    hdfPartyName.ID = "hdfPartyName"
                    hdfPartyName.Value = Convert.ToString(headerDr("PartyName"))
                    btnDiv.Controls.Add(hdfPartyName)

                    Dim hdfEmailID As New HiddenField
                    hdfEmailID.ID = "hdfEmailID"
                    hdfEmailID.Value = Convert.ToString(headerDr("email1"))
                    btnDiv.Controls.Add(hdfEmailID)
                    MainDiv.Controls.Add(btnDiv)

                    If inventoryDt.Rows.Count > 0 Then
                        Dim tblInventory As New HtmlTable
                        tblInventory.Attributes.Add("class", "InventoryStyle")
                        Dim arrInventory() As String = {"Dates", "Room Type", "Free Sale", "Allocation", "Xtra", "B2B", "Swap"}
                        tblRow = New HtmlTableRow
                        For i = 0 To 6
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = arrInventory(i)
                            tblCol.BgColor = "#d6d6d6"
                            tblRow.Cells.Add(tblCol)
                        Next
                        tblInventory.Rows.Add(tblRow)

                        Dim str As String = ""
                        For Each inventoryDr As DataRow In inventoryDt.Rows
                            tblRow = New HtmlTableRow
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = Convert.ToDateTime(inventoryDr("inventorydate")).ToString("dd/MM/yyyy")
                            tblRow.Cells.Add(tblCol)

                            tblCol = New HtmlTableCell
                            tblCol.InnerText = Convert.ToString(inventoryDr("rmTypName"))
                            tblRow.Cells.Add(tblCol)

                            If String.IsNullOrEmpty(Convert.ToString(inventoryDr("freesale"))) Or Convert.ToString(inventoryDr("freesale")) = "0" Then
                                str = ""
                            Else
                                str = Convert.ToString(inventoryDr("freesale"))
                            End If
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = str
                            tblRow.Cells.Add(tblCol)

                            If String.IsNullOrEmpty(Convert.ToString(inventoryDr("Allocation"))) Or Convert.ToString(inventoryDr("Allocation")) = "0" Then
                                str = ""
                            Else
                                str = Convert.ToString(inventoryDr("Allocation"))
                            End If
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = str
                            tblRow.Cells.Add(tblCol)

                            If String.IsNullOrEmpty(Convert.ToString(inventoryDr("xtraAllocation"))) Or Convert.ToString(inventoryDr("xtraAllocation")) = "0" Then
                                str = ""
                            Else
                                str = Convert.ToString(inventoryDr("xtraAllocation"))
                            End If
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = str
                            tblRow.Cells.Add(tblCol)

                            If String.IsNullOrEmpty(Convert.ToString(inventoryDr("B2B"))) Or Convert.ToString(inventoryDr("B2B")) = "0" Then
                                str = ""
                            Else
                                str = Convert.ToString(inventoryDr("B2B"))
                            End If
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = str
                            tblRow.Cells.Add(tblCol)

                            If String.IsNullOrEmpty(Convert.ToString(inventoryDr("swap"))) Or Convert.ToString(inventoryDr("swap")) = "0" Then
                                str = ""
                            Else
                                str = Convert.ToString(inventoryDr("swap"))
                            End If
                            tblCol = New HtmlTableCell
                            tblCol.InnerText = str
                            tblRow.Cells.Add(tblCol)
                            tblInventory.Rows.Add(tblRow)
                        Next
                        MainDiv.Controls.Add(tblInventory)
                    End If
                    Dim tblFooter As New HtmlTable
                    tblFooter.Attributes.Add("class", "FooterStyle")
                    tblRow = New HtmlTableRow
                    tblCol = New HtmlTableCell
                    tblCol.InnerText = "Thanks and Best Regards"
                    tblRow.Cells.Add(tblCol)
                    tblFooter.Rows.Add(tblRow)
                    If contactDt.Rows.Count > 0 Then
                        Dim contractDr As DataRow = contactDt.Rows(0)
                        Dim brTag As New HtmlGenericControl("br")
                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = Convert.ToString(contractDr("salesPerson")) + "<" + Convert.ToString(contractDr("salesemail")) + ">"
                        tblCol.Controls.Add(brTag)
                        Dim design As New Literal
                        design.Text = "DESTINATION SPECIALIST"
                        tblCol.Controls.Add(design)
                        tblRow.Cells.Add(tblCol)
                        tblFooter.Rows.Add(tblRow)

                        tblRow = New HtmlTableRow
                        tblCol = New HtmlTableCell
                        tblCol.InnerText = "Mobile No - " + Convert.ToString(contractDr("salesmobile"))
                        tblRow.Cells.Add(tblCol)
                        tblFooter.Rows.Add(tblRow)
                    End If
                    MainDiv.Controls.Add(tblFooter)
                End If
            Else
                Throw New Exception("There is no rows in the header table")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal CurrCode As String, ByRef FinalTotal As Decimal, ByRef tblSplEvent As PdfPTable)"
    Protected Sub SpecialEvents(ByVal splEventDt As DataTable, ByVal CurrCode As String, ByRef FinalTotal As Decimal, ByRef tblSplEvent As HtmlTable)
        Dim tblRow = New HtmlTableRow
        Dim tblCol = New HtmlTableCell
        Dim arrSplEvent() As String = {"Special Events", "Date of Event", "Units/ Pax", "Type of Units/ Pax", "Rate per Units/Pax", "Charges (" & CurrCode & ")"}
        For i = 0 To 3 '5
            tblCol = New HtmlTableCell
            tblCol.InnerText = arrSplEvent(i)
            tblCol.Attributes.Add("class", "ServTitleTd")
            tblRow.Cells.Add(tblCol)
        Next
        tblSplEvent.Rows.Add(tblRow)
        Dim totalCost As Decimal = 0.0
        For Each splEventDr As DataRow In splEventDt.Rows
            tblRow = New HtmlTableRow
            tblCol = New HtmlTableCell
            tblCol.InnerText = Convert.ToString(splEventDr("splEventName"))
            tblCol.Attributes.Add("class", "ServTd")
            tblCol.Width = "35%"
            tblRow.Cells.Add(tblCol)

            tblCol = New HtmlTableCell
            tblCol.InnerText = Convert.ToString(splEventDr("splEventDate"))
            tblCol.Attributes.Add("class", "ServTd")
            tblRow.Cells.Add(tblCol)

            tblCol = New HtmlTableCell
            tblCol.InnerText = Convert.ToString(splEventDr("noOfPax"))
            tblCol.Attributes.Add("class", "TariffTd")
            tblRow.Cells.Add(tblCol)

            tblCol = New HtmlTableCell
            tblCol.InnerText = Convert.ToString(splEventDr("paxType"))
            tblCol.Attributes.Add("class", "TariffTd")
            tblRow.Cells.Add(tblCol)

            'tblCol = New HtmlTableCell
            'tblCol.InnerText = "" ' Convert.ToString(splEventDr("paxCost"))      // Hide value temporarily for VAT implementation
            'tblCol.Attributes.Add("class", "TariffTd")
            'tblRow.Cells.Add(tblCol)

            'tblCol = New HtmlTableCell
            'tblCol.InnerText = "" ' Convert.ToString(splEventDr("splEventCostValue"))      // Hide value temporarily for VAT implementation
            'tblCol.Attributes.Add("class", "TariffTd")
            'tblCol.Width = "12%"
            'tblRow.Cells.Add(tblCol)

            tblSplEvent.Rows.Add(tblRow)
            totalCost = totalCost + splEventDr("splEventCostValue")
        Next
        FinalTotal = FinalTotal + totalCost
    End Sub
#End Region

#Region "Protected Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)"
    Protected Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim noOfRooms As Integer = Convert.ToInt32(DirectCast(Me.FindControl("hdfRoomsCnt"), HiddenField).Value)
            If noOfRooms > 0 Then
                Dim requestID As String = Convert.ToString(DirectCast(Me.FindControl("hdfRequestID"), HiddenField).Value)
                Dim partyCode As String = Convert.ToString(DirectCast(Me.FindControl("hdfPartyCode"), HiddenField).Value)
                Dim RoomConfList As New List(Of BLLHotelConfirm)
                Dim RoomCnt As Integer = 1
                While (RoomCnt <= noOfRooms)
                    Dim confNo As TextBox = CType(Me.FindControl("txtConfNo" + RoomCnt.ToString()), TextBox)
                    Dim confBy As TextBox = CType(Me.FindControl("txtConfBy" + RoomCnt.ToString()), TextBox)
                    Dim confDt As TextBox = CType(Me.FindControl("txtConfDate" + RoomCnt.ToString()), TextBox)
                    Dim remarks As TextBox = CType(Me.FindControl("txtRemarks" + RoomCnt.ToString()), TextBox)
                    Dim rLineNo As Integer = Convert.ToInt32(DirectCast(Me.FindControl("hdfRLineNo" + RoomCnt.ToString()), HiddenField).Value)
                    Dim RoomNo As Integer = Convert.ToInt32(DirectCast(Me.FindControl("hdfRooms" + RoomCnt.ToString()), HiddenField).Value)
                    Dim RoomType As String = Convert.ToString(DirectCast(Me.FindControl("hdfRoomType" + RoomCnt.ToString()), HiddenField).Value)
                    Dim objBLLHotelConf As New BLLHotelConfirm
                    objBLLHotelConf.HBRequestID = requestID.ToString()
                    objBLLHotelConf.HBRlineNo = rLineNo.ToString()
                    objBLLHotelConf.HBRoomNo = RoomNo.ToString()
                    objBLLHotelConf.HBRoomType = RoomType
                    objBLLHotelConf.HBConfirmationNo = confNo.Text.ToString.Trim()
                    objBLLHotelConf.HBConfirmBy = confBy.Text.ToString.Trim()
                    objBLLHotelConf.HBConfirmationDate = confDt.Text.ToString.Trim()
                    RoomConfList.Add(objBLLHotelConf)
                    RoomCnt = RoomCnt + 1
                End While
                Dim objBllHotelConfList As New BLLHotelConfirm
                Dim strResult As String = objBllHotelConfList.SavingBookingHotelConfirmation(RoomConfList)
                If strResult = "True" Then
                    Dim str As String
                    If noOfRooms = 1 Then
                        str = "Room has"
                    Else
                        str = "Rooms have"
                    End If
                    Dim btn As Button = CType(sender, Button)
                    btn.Enabled = False
                    btn.BackColor = Drawing.ColorTranslator.FromHtml("#A9A9A9") 'Drawing.Color.Gray
                    sendEmail(requestID, RoomConfList)
                    ModalPopupDays.Hide()
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" + str + " been confirmed successfully');", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" + strResult + "' );", True)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("BookingHotelConfirm.aspx :: BtnSubmit :: " & ex.Message & ":: " & requestid)
        End Try
    End Sub
#End Region

#Region "Protected Sub txt_TextChanged(sender As Object, e As System.EventArgs)"
    Protected Sub txt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(Me.FindControl("btnSubmit"), Button)
        If btn.Enabled = False Then
            btn.Enabled = True
            btn.BackColor = Drawing.ColorTranslator.FromHtml("#034f13")
        End If
    End Sub
#End Region

#Region "Protected Sub sendEmail(ByVal requestID As String, RoomConfList As List(Of BLLHotelConfirm))"
    Protected Sub sendEmail(ByVal requestID As String, ByVal RoomConfList As List(Of BLLHotelConfirm))
        Dim partyName As String = Convert.ToString(DirectCast(Me.FindControl("hdfPartyName"), HiddenField).Value)
        Dim HotelMailID As String = Convert.ToString(DirectCast(Me.FindControl("hdfEmailID"), HiddenField).Value)
        Dim divCode As String = Convert.ToString(DirectCast(Me.FindControl("hdfCcAddressCode"), HiddenField).Value)
        Dim reservationParamID As String = ""
        If divCode = "01" Then
            reservationParamID = 1201
        Else
            reservationParamID = 1202
        End If
        Dim clsEmail As New clsEmail
        Dim strfromEmailID As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
        Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
        Dim testEmailID As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")
        Dim ccEmailID As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters where param_id=" + reservationParamID)

        '' Added Shahul 20/06/18
        Dim agentcode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select agentcode from booking_header(nolock) where requestid='" & requestID & "'")
        Dim Roemail As String = objclsUtilities.ExecuteQueryReturnStringValue("select  isnull(u.usemail,'') usemail     from agentmast  a(nolock) ,usermaster u(nolock) where a.spersoncode=u.usercode and  a.agentcode='" & agentcode & "'")

        Dim from_email As String = "", to_email As String = "", cc_email As String = ""
        Dim strSubject As String = "Hotel Confirmation Alert - " + partyName + " - " + requestID
        Dim strMessage As String
        strMessage = "<p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #0d0d0d; font-size: 12pt;'>Dear Team,</span></p>"
        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>The following Room has been confirmed by  <span style='font-weight: bold;'>" + partyName + "</span> against your reference no. <span style='font-weight: bold;'>" + requestID + "</span>.</span></p>"
        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Please find the confirmation details below.</span></p>"
        Dim cnt As Integer = 0
        For Each rm1 As BLLHotelConfirm In RoomConfList
            cnt = cnt + 1
            strMessage += "<br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Room No             : " + CType(cnt, String) + "</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Room Type           : " + rm1.HBRoomType + "</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Confirmation No.    : " + rm1.HBConfirmationNo + "</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Confirm By          : " + rm1.HBConfirmBy + "</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Confirmation Date   : " + rm1.HBConfirmationDate + "</span></p>"
        Next
        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Please login and check the confirmation details.</span></p>"
        strMessage += "<br /><br />"
        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>Best Regards,</span></p>"
        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>" + partyName + "</span></p>"

        strMessage += "<p class='MsoNormal' style='margin: 0'><br /><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#0d0d0d'>*** This is an automatically generated email, please do not reply ***</span></p>"
        If HotelMailID = "" Then from_email = strfromEmailID Else from_email = HotelMailID

        If Roemail <> "" Then to_email = Roemail '' Added Shahul 20/06/18


        If Emailmode = "Test" Then
            to_email = testEmailID
            cc_email = ""
        Else
            to_email = to_email + "," + testEmailID
            cc_email = ccEmailID
        End If
        If clsEmail.SendEmailOnline(from_email, to_email, cc_email, strSubject, strMessage, "") Then
            objclsUtilities.SendEmailNotification("CONFIRM", from_email, to_email, cc_email, "", strSubject, strMessage, "1", "1", "", "Y", requestID, "CONFIRM_FROM_HOTEL")
        Else
            objclsUtilities.SendEmailNotification("CONFIRM", from_email, to_email, cc_email, "", strSubject, strMessage, "1", "1", "", "N", requestID, "CONFIRM_FROM_HOTEL")
        End If
    End Sub
#End Region

End Class
