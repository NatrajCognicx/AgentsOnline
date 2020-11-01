Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
'Imports Microsoft.VisualBasic


Partial Class GuestPage
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objclsUtils As New clsUtils
    Dim objBLLHotelSearch As New BLLHotelSearch
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim objResParam As New ReservationParameters
    Dim objBLLguest As New BLLGuest
    Dim iCumulative As Integer = 0
    Dim roomno As Integer = 0
    Dim rlineno As Integer = 0
    Dim bookingpax As Integer = 0
    Dim transferArrpax As Integer = 0
    Dim guesttocheck As String = "0"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                If Session("sAgentCompany") Is Nothing Or Session("GlobalUserName") Is Nothing Then
                    Dim strAbsoluteUrl As String = hdAbsoluteUrl.Value
                    If (strAbsoluteUrl = "") Then 'rsa ins - 81/
                        strAbsoluteUrl = "Login.aspx"
                    End If
                    Response.Redirect(strAbsoluteUrl, True)
                End If
                If Not Session("sAbsoluteUrl") Is Nothing Then
                    Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
                    hdAbsoluteUrl.Value = strAbsoluteUrl
                End If
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel

                End If
                btnGenerateFlightDetails.Style.Add("display", "block")
                btnAdd.Style.Add("display", "block")
                btnAddchd.Style.Add("display", "block")
                divaccept.Style.Add("display", "block")
                dlPersonalInfo.Enabled = True
                divsubmit.Style.Add("display", "block")

                dlFlightDetails.Enabled = True
                dldeparturedetails.Enabled = True
                btnAddflight.Style.Add("display", "block")
                btnAddDepflight.Style.Add("display", "block")
                txtAgentReference.Enabled = True

                '*** Danny 12/08/2018
                Dim lsdivTerms As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=50")
                If Session("sLoginType") = "RO" And lsdivTerms = 1 Then
                    chkTermsAndConditions.Checked = True
                    Dim divTerms As HtmlGenericControl = CType(FindControl("divTerms"), HtmlGenericControl)
                    divTerms.Style.Add("display", "none")
                End If

                LoadHome()
                '   divFlightdetail.Style.Add("display", "none")
                If Not Session("sdtPriceBreakup") Is Nothing Then
                    Session.Remove("sdtPriceBreakup")
                End If

                chkApplySameConf.Attributes.Add("onChange", "javascript:ChkApplyConfirm('" + chkApplySameConf.ClientID + "' )")

                'changed by mohamed on 08/07/2018
                Dim lsDiv01BookingIdPrefix As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=2031")
                Dim lsDiv02BookingIdPrefix As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=2032")
                hdnBookingIDPrefix01.Value = lsDiv01BookingIdPrefix
                hdnBookingIDPrefix02.Value = lsDiv02BookingIdPrefix
                lblOldBookingNo.InnerText = "Old Booking No (" & hdnBookingIDPrefix01.Value & IIf(hdnBookingIDPrefix02.Value.Trim = "", "", "/" & hdnBookingIDPrefix02.Value) & "):"
            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: Page_Load :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try
        Else
            'Loading menu in postback if sFinalbooked is not nothing 'changed by mohamed on 12/08/2018
            If Not Session("sFinalBooked") Is Nothing Then
                LoadMenus()
            End If
        End If
        EnableArrivalFlightdetails()
        EnableDepartureFlightdetails()
        If hdWhiteLabel.Value = "1" Then
            'btnSubmitBooking.Visible = False
        End If
    End Sub

    Protected Sub btnApplySameConfChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplySameConfChk.Click

        Try

            If chkApplySameConf.Checked Then
                If gvConfirmBooking.Rows.Count > 0 Then

                    Dim dvhotelconfno As HtmlGenericControl = CType(gvConfirmBooking.Rows(0).FindControl("dvhotelconfno"), HtmlGenericControl)
                    Dim dvtimelimitdate As HtmlGenericControl = CType(gvConfirmBooking.Rows(0).FindControl("dvtimelimitdate"), HtmlGenericControl)
                    Dim dvtimelimittime As HtmlGenericControl = CType(gvConfirmBooking.Rows(0).FindControl("dvtimelimittime"), HtmlGenericControl)
                    Dim dvdays As HtmlGenericControl = CType(gvConfirmBooking.Rows(0).FindControl("dvdays"), HtmlGenericControl)

                    Dim txthotelconfno As TextBox = CType(dvhotelconfno.FindControl("txthotelconfno"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(dvtimelimitdate.FindControl("txtTimeLimitDate"), TextBox)
                    Dim txtTimeLimitTime As HtmlInputText = CType(dvtimelimittime.FindControl("txtTimeLimitTime"), HtmlInputText)
                    Dim txtCancelDays As TextBox = CType(dvdays.FindControl("txtCancelDays"), TextBox)

                    If (txthotelconfno.Text) <> "" Or (txtTimeLimitDate.Text.Trim <> "") Or (txtTimeLimitTime.Value.Trim <> "") Then


                        For i = 1 To gvConfirmBooking.Rows.Count - 1

                            'Next
                            'For Each gvrow As GridViewRow In gvConfirmBooking.Rows


                            Dim dvhotelconfnonew As HtmlGenericControl = CType(gvConfirmBooking.Rows(i).FindControl("dvhotelconfno"), HtmlGenericControl)
                            Dim dvtimelimitdatenew As HtmlGenericControl = CType(gvConfirmBooking.Rows(i).FindControl("dvtimelimitdate"), HtmlGenericControl)
                            Dim dvtimelimittimenew As HtmlGenericControl = CType(gvConfirmBooking.Rows(i).FindControl("dvtimelimittime"), HtmlGenericControl)
                            Dim dvdaysnew As HtmlGenericControl = CType(gvConfirmBooking.Rows(i).FindControl("dvdays"), HtmlGenericControl)

                            Dim txthotelconfnonew As TextBox = CType(dvhotelconfnonew.FindControl("txthotelconfno"), TextBox)
                            Dim txtTimeLimitDatenew As TextBox = CType(dvtimelimitdatenew.FindControl("txtTimeLimitDate"), TextBox)
                            Dim txtTimeLimitTimenew As HtmlInputText = CType(dvtimelimittimenew.FindControl("txtTimeLimitTime"), HtmlInputText)
                            Dim txtdaysnew As TextBox = CType(dvdaysnew.FindControl("txtcanceldays"), TextBox)

                            If (txthotelconfnonew.Text.Trim = "") Then
                                txthotelconfnonew.Text = txthotelconfno.Text
                                chkApplySameConf.Checked = False

                            End If
                            If (txtdaysnew.Text.Trim = "0" Or txtdaysnew.Text.Trim = "") And (txtTimeLimitTimenew.Value.Trim = "") Then
                                txtTimeLimitDatenew.Text = txtTimeLimitDate.Text
                                txtTimeLimitTimenew.Value = txtTimeLimitTime.Value
                                txtdaysnew.Text = txtCancelDays.Text
                                chkApplySameConf.Checked = False
                            End If

                            If (txtTimeLimitTimenew.Value.Trim = "") Then

                                txtTimeLimitTimenew.Value = txtTimeLimitTime.Value
                                chkApplySameConf.Checked = False
                            End If
                        Next

                    Else

                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No. / Days /Timit limit in First Row...")
                        chkApplySameConf.Checked = False
                    End If

                End If
                '   mpConfirm.Show()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: btnApplySameConfChk_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Function ValidateConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvConfirmBooking.Rows
            Dim txthotelconfno As TextBox = CType(gvrow.FindControl("txthotelconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)



            'If lblleadguest.Text = "" Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest.")

            '    ValidateConfirmBookingDetails = False
            '    Exit Function
            'End If
            If txthotelconfno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No.")
                txthotelconfno.Focus()
                ValidateConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitDate.Text = "DD/MM/YYYY" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                txtTimeLimitDate.Focus()
                ValidateConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitTime.Value = "hh:mm" Or txtTimeLimitTime.Value = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit- Time.")
                txtTimeLimitTime.Focus()
                ValidateConfirmBookingDetails = False
                Exit Function
            End If

        Next
        Return True
    End Function
    Private Sub FillHotelRemarksChk()
        Dim sqlstr As String
        sqlstr = "select remarkscode,remarksdesc from hotelremarkstemplate where active=1"
        objclsUtilities.FillCheckBoxList(chkHotelRemarks, sqlstr, "remarkscode", "remarksdesc")
        If chkHotelRemarks.Items.Count = 0 And Session("sLoginType") = "Agent" Then
            dvHotelRemarkslbl.Style.Add("display", "none")
        Else
            dvHotelRemarkslbl.Style.Add("display", "block")
        End If

    End Sub

    Private Sub FillRemarksPopUp(ByVal sender As Object)
        Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
        hdnrlineno.Value = lblrlineno.Text
        Dim objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataFromDataTable("select  * from booking_hotel_detail_remarkstemp  where requestid= '" & objBLLHotelSearch.OBrequestid & "' and rlineno ='" & lblrlineno.Text & " 'order by requestid")
            If dt.Rows.Count > 0 Then

                Dim remarkstemplate As String()
                remarkstemplate = dt.Rows(0).Item("remarkstemplate").Split(";")

                For Each remark As String In remarkstemplate
                    For Each item As System.Web.UI.WebControls.ListItem In chkHotelRemarks.Items
                        If item.Value = remark Then
                            item.Selected = True
                            Exit For
                        End If
                    Next
                Next
                txthotremarks.Text = dt.Rows(0).Item("hotelremarks")
                txtcustRemarks.Text = dt.Rows(0).Item("agentremarks")
                txtArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
                txtDeptRemarks.Text = dt.Rows(0).Item("departureremarks")
            End If
            'strsqlqry = "select  * from booking_hotel_detail_remarkstemp  where requestid= '" & objBLLHotelSearch.OBrequestid & "' and rlineno ='" & lblrlineno.Text & " 'order by requestid"
            'Dim SqlConn As New SqlConnection
            'Dim sqldr As SqlDataReader
            'SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Dim sqlcmd As New SqlCommand(strsqlqry, SqlConn)
            ''Open connection

            'sqldr = sqlcmd.ExecuteReader()

            'While (sqldr.Read())

            '    Dim remarkstemplate As String()
            '    remarkstemplate = sqldr.Item("remarkstemplate").Split(";")

            '    For Each remark As String In remarkstemplate
            '        For Each item As ListItem In chkHotelRemarks.Items
            '            If item.Value = remark Then
            '                item.Selected = True
            '                Exit For
            '            End If
            '        Next
            '    Next
            '    txthotremarks.Text = sqldr.Item("hotelremarks")
            '    txtcustRemarks.Text = sqldr.Item("agentremarks")
            '    txtArrRemarks.Text = sqldr.Item("arrivalremarks")
            '    txtDeptRemarks.Text = sqldr.Item("departureremarks")
            'End While
            'sqldr.Close()
            'SqlConn.Close()

        End If
    End Sub
    Private Sub BindConfirmSummaryDetails(ByVal sender As Object)



        Dim dt As New DataTable

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim rlineno As String = CType(dlItem.FindControl("lblrlineno"), Label).Text


        dt = objBLLguest.GetConfirmSummary(GetNewOrExistingRequestId(), rlineno)


        gvConfirmBooking.DataSource = dt

        gvConfirmBooking.DataBind()
        FillConfirmSummaryDetails()

    End Sub

    Protected Sub btnsaveconfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsaveconfirm.Click
        Try


            If ValidateConfirmBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvConfirmBooking.Rows
                    Dim lblRowNo As Label = CType(gvrow.FindControl("lblRowNo"), Label)

                    Dim lblNoOfRooms As Label = CType(gvrow.FindControl("lblNoOfRooms"), Label)
                    Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
                    Dim txthotelconfno As TextBox = CType(gvrow.FindControl("txthotelconfno"), TextBox)
                    Dim txtCancelDays As TextBox = CType(gvrow.FindControl("txtCancelDays"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
                    Dim hdnPrevConfNo As HiddenField = CType(gvrow.FindControl("hdnPrevConfNo"), HiddenField)
                    Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

                    Dim txtconfirmDate As TextBox = CType(gvrow.FindControl("txtconfirmdate"), TextBox)
                    Dim txtPrevConfNo As TextBox = CType(gvrow.FindControl("txtPrevConfNo"), TextBox)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<rlineno>" & lbldlrlineno.Text & "</rlineno>")
                    strbuffer.Append("<roomno>" & lblNoOfRooms.Text & "</roomno>") '.Split(" ")(1)
                    strbuffer.Append("<hotelconfno>" & txthotelconfno.Text & "</hotelconfno>")
                    'If lblleadguest.Text <> "" Then
                    strbuffer.Append("<confirmby>" & Session("GlobalUserName") & "</confirmby>")

                    'Else
                    'strbuffer.Append("<confirmby>" & DBNull.Value & "</confirmby>")
                    'End If
                    strbuffer.Append("<confirmdate>" & Format(CType(txtconfirmDate.Text, Date), "yyyy/MM/dd") & "</confirmdate>")
                    If txthotelconfno.Text <> hdnPrevConfNo.Value And hdnPrevConfNo.Value <> "" Then
                        strbuffer.Append("<prevconfno>" & hdnPrevConfNo.Value & "</prevconfno>")
                    Else
                        strbuffer.Append("<prevconfno>" & txtPrevConfNo.Text & "</prevconfno>")
                    End If

                    strbuffer.Append("<timelimit>" & Format(CType(txtTimeLimitDate.Text, Date), "yyyy/MM/dd") + " " + txtTimeLimitTime.Value & "</timelimit>")
                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If
                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLguest.CBConfirmXml = strbuffer.ToString
                objBLLguest.GBuserlogged = Session("GlobalUserName")

                If objBLLguest.SavingConfirmBookingDetailsInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpConfirm.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: btnsaveconfirm_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub FillConfirmSummaryDetails()
        Dim dt As New DataTable
        dt = objBLLguest.SetConfirmDetFromDataTable("select rlineno,roomno,hotelconfno,confirmby,convert(varchar(10),confirmdate,103) as confirmdate,prevconfno,convert(varchar(10),timelimit,103) as timelimitdate,convert(varchar(5), timelimit, 108) as timelimittime from booking_hotel_detail_confcanceltemp  where requestid='" & GetNewOrExistingRequestId() & "' and rlineno='" & lbldlrlineno.Text & "'")

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim lblRowNo As Label = CType(gvConfirmBooking.Rows(i).FindControl("lblRowNo"), Label)
                Dim txthotelconfno As TextBox = CType(gvConfirmBooking.Rows(i).FindControl("txthotelconfno"), TextBox)
                Dim txtConfirmDate As TextBox = CType(gvConfirmBooking.Rows(i).FindControl("txtConfirmDate"), TextBox)
                Dim txtPrevConfNo As TextBox = CType(gvConfirmBooking.Rows(i).FindControl("txtPrevConfNo"), TextBox)
                Dim txtTimeLimitDate As TextBox = CType(gvConfirmBooking.Rows(i).FindControl("txtTimeLimitDate"), TextBox)
                Dim hdnPrevConfNo As HiddenField = CType(gvConfirmBooking.Rows(i).FindControl("hdnPrevConfNo"), HiddenField)
                Dim txtTimeLimitTime As HtmlInputText = CType(gvConfirmBooking.Rows(i).FindControl("txtTimeLimitTime"), HtmlInputText)
                Dim txtCancelDays As TextBox = CType(gvConfirmBooking.Rows(i).FindControl("txtCancelDays"), TextBox)

                For rowindex As Integer = 0 To gvConfirmBooking.Columns.Count - 1
                    If gvConfirmBooking.Columns(rowindex).HeaderText.Trim = "Prev. Confirmation No." Then
                        gvConfirmBooking.Columns(rowindex).Visible = True
                        dvCancelheading.Style.Add("width", "375px")
                        dvCancelheading.Style.Add("margin-left", "520px")
                    End If
                Next
                '    If dt.Rows(i).Item("rlineno").ToString = lblRowNo.Text Then
                txthotelconfno.Text = dt.Rows(i).Item("hotelconfno").ToString
                txtConfirmDate.Text = dt.Rows(i).Item("confirmdate").ToString
                txtPrevConfNo.Text = dt.Rows(i).Item("prevconfno").ToString
                txtTimeLimitDate.Text = dt.Rows(i).Item("timelimitdate").ToString
                txtTimeLimitTime.Value = dt.Rows(i).Item("timelimittime").ToString
                txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblCheckInDate.Text, Date))
                hdnPrevConfNo.Value = dt.Rows(i).Item("hotelconfno").ToString
                ' End If

            Next

        Else

            For i = 0 To gvConfirmBooking.Columns.Count - 1

                If gvConfirmBooking.Columns(i).HeaderText.Trim = "Prev. Confirmation No." Then

                    gvConfirmBooking.Columns(i).Visible = False
                    dvCancelheading.Style.Add("width", "290px")
                    dvCancelheading.Style.Add("margin-left", "605px")

                End If
            Next
        End If



    End Sub

    Private Shared Function GetNewOrExistingRequestId() As String
        Dim strRequestId As String = ""
        Dim objBLLHotelSearch2 As New BLLHotelSearch
        strRequestId = GetExistingRequestId()
        If strRequestId = "" Then
            strRequestId = objBLLHotelSearch2.getrequestid()
        End If
        Return strRequestId
    End Function
    Private Shared Function GetExistingRequestId() As String
        Dim strRequestId As String = ""
        If HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = ""
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            strRequestId = HttpContext.Current.Session("sRequestId")
            '  strRequestId = objBLLCommonFuntions.GetExistingBookingRequestId(strRequestId)
        End If
        Return strRequestId
    End Function
    Protected Sub gvConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvConfirmBooking.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim lblNoOfRooms As Label = CType(e.Row.FindControl("lblNoOfRooms"), Label)
            Dim txtCancelDays As TextBox = CType(e.Row.FindControl("txtCancelDays"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtconfirmdate"), TextBox)
            Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)
            Dim PrevConfNo As HtmlGenericControl = CType(e.Row.FindControl("PrevConfNo"), HtmlGenericControl)
            Dim dvconfirmdiv As HtmlGenericControl = CType(e.Row.FindControl("dvconfirmdiv"), HtmlGenericControl)
            Dim dvtimelimittime As HtmlGenericControl = CType(e.Row.FindControl("dvtimelimittime"), HtmlGenericControl)

            Dim txtTimeLimitTime As HtmlInputText = CType(dvtimelimittime.FindControl("txtTimeLimitTime"), HtmlInputText)

            txtTimeLimitTime.Value = objBLLguest.getdefaulttimeforconfirm(" select option_selected  from reservation_parameters  where param_id =2010")

            Dim timelimitdate As Date = CType(lblCheckInDate.Text, Date)

            txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer)))


            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:ChangeDate('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "')")

        End If

    End Sub

    Protected Sub imgbConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            chkApplySameConf.Checked = False
            Dim myButton As ImageButton = CType(sender, ImageButton)

            Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

            Dim lblroomtype As Label = CType(dlItem.FindControl("lblroomtype"), Label)
            Dim txtTimeLimitTime As TextBox = CType(dlItem.FindControl("txtTimeLimitTime"), TextBox)
            Dim lblchkindateformat As Label = CType(dlItem.FindControl("lblchkindateformat"), Label)
            Dim lblchkoutdateformat As Label = CType(dlItem.FindControl("lblchkoutdateformat"), Label)
            Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
            '  Dim chkinoutdates As String() = lbldates.Text.Split("/")
            Dim lblnoofrooms As Label = CType(dlItem.FindControl("lblnoofrooms"), Label)
            'btnTimeLimitTime.Attributes.Add("onclick", "selectTime(this," & txtTimeLimitTime.ClientID & ")")


            lbldlrlineno.Text = lblrlineno.Text
            lblrooms.Text = lblnoofrooms.Text
            lblCheckInDate.Text = lblchkindateformat.Text
            lblCheckOutDate.Text = lblchkoutdateformat.Text
            lblConfirmHeading.Text = lblroomtype.Text
            BindConfirmSummaryDetails(sender)
            mpConfirm.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: imgbConfirm_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub imgbRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

                If Session("sLoginType") <> "RO" Then

                    dvArrRemarks.Style.Add("display", "none")
                    dvDepRemarks.Style.Add("display", "none")
                    dvTxtHotRem.Style.Add("display", "none")


                Else
                    dvArrRemarks.Style.Add("display", "block")
                    dvDepRemarks.Style.Add("display", "block")
                    dvTxtHotRem.Style.Add("display", "block")

                End If
            End If
            clearcontrols()
            FillHotelRemarksChk()
            FillRemarksPopUp(sender)
            mpRemarks.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub clearcontrols()
        For Each item As System.Web.UI.WebControls.ListItem In chkHotelRemarks.Items
            item.Selected = False
        Next
        txtArrRemarks.Text = ""
        txtDeptRemarks.Text = ""
        txtcustRemarks.Text = ""
        txthotremarks.Text = ""
    End Sub
    Private Sub LoadHome()
        LoadFooter()
        btnGenerateFlightDetails.Style.Add("display", "block")
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        LoadFields()
        showservice()
        ShowGuests()
        FillGuestNameAll()
        'BindPersonalInformations()
        FindCumilative()

        BindCheckInAndCheckOutHiddenfield()
        BindBookingSummary()
        BindVisaSummary()
        BindTourSummary()
        BindTransferSummary()
        BindAirportserviceSummary()
        BindOtherserviceSummary()
        BindPreHotelSummary()
        HideTabTotalPrice()
        Dim dt As DataTable
        If Session("sLoginType") = "RO" Then
            dt = objBLLCommonFuntions.GetBookingvalue(Session("sRequestId"), "0")
        Else
            dt = objBLLCommonFuntions.GetBookingvalue(Session("sRequestId"), objResParam.WhiteLabel)
        End If

        If dt.Rows.Count > 0 Then
            lbltotalbooking.Text = dt.Rows(0)("totalbookingvalue").ToString
        End If

        If hdBookingEngineRateType.Value = "1" Then
            divtotal.Style.Add("display", "none")
        Else
            divtotal.Style.Add("display", "block")
        End If

        If Not Session("sEditRequestId") Is Nothing Then
            lblEditRequestId.Text = Session("sEditRequestId")
            lblEditRequestId1.Visible = True
            lblEditRequestId.Visible = True
        Else
            lblEditRequestId1.Visible = False
            lblEditRequestId.Visible = False
        End If

        If Not Session("sEditRequestId") Is Nothing Then

            Fillguestnames(Session("sEditRequestId"))

            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
            If dt.Rows.Count > 0 Then
                txtAgentReference.Text = dt.Rows(0)("agentref").ToString
                txtColumbusReference.Text = dt.Rows(0)("ColumbusRef").ToString
            End If
            Dim flightexists As String = ""

            Dim strQuery As String = "select 't' from booking_guest_flightstemp(nolock) where  requestid='" & CType(Session("sEditRequestId"), String) & "'"
            flightexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            If flightexists <> "" Then
                BindFlightDetailsAmend()
                divFlightdetail.Style.Add("display", "block")
            End If
        Else

        End If
        If Session("sLoginType") = "RO" Then 'Not Session("sEditRequestId") Is Nothing And 'changed by mohamed on 11/11/2018
            'If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") Then
            '    dvSendEmail.Style.Add("display", "block")
            'Else
            '    dvSendEmail.Style.Add("display", "none")
            'End If
            dvSendEmail.Style.Add("display", "block")
            Dim dt1 As New DataTable
            dt1 = objBLLguest.SetConfirmDetFromDataTable("execute sp_get_bookingtype '" & GetNewOrExistingRequestId() & "', 0")
            If dt1.Rows.Count > 0 Then
                If dt1.Rows(0)("bookingtype") = "FREEFORM" Then
                    chkEmailToAgent.Checked = False
                    chkEmailToHotel.Checked = False
                End If
            End If
        Else
            dvSendEmail.Style.Add("display", "none")
        End If
        ShowProfitSummary()
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If

        '' Added shahul 29/07/18
        Dim lsABANDONtext As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=5306")
        If lsABANDONtext = "1" Then
            btnAbondon.Text = objclsUtilities.ExecuteQueryReturnSingleValue("select option_value from reservation_parameters where param_id=5306")
            If btnAbondon.Text.Trim = "" Then
                btnAbondon.Text = "CANCEL"
            End If
        End If

        '' Added shahul 04/08/18
        guesttocheck = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=5310")
    End Sub

    Private Sub HideTabTotalPrice()
        If hdBookingEngineRateType.Value = "1" Then
            lblTabHotelTotalPrice1.Visible = False
            lblTourTabTotalPrice1.Visible = False
            lblAirportTabtotalPrice1.Visible = False
            lblTransferTabTotalPrice1.Visible = False
            lblVisaTabTotalPrice1.Visible = False
            lblOtherServiceTabTotalPrice1.Visible = False
        End If
    End Sub
    Private Sub Fillguestnames(ByVal requestid As String)
        Dim objBLLGuest = New BLLGuest
        Dim BLLcommon = New BLLCommonFuntions
        Dim dt As DataTable
        Dim ddlTitle As DropDownList
        Dim lblRowNoAll As Label
        Dim lblrlineno As Label
        Dim txtFirstNameg As TextBox
        Dim txtMidName As TextBox
        Dim txtLastNameg As TextBox
        Dim txtNationalityCode As TextBox
        Dim txtchildage As TextBox
        Dim ddlVisa As DropDownList
        Dim ddlVisatype As DropDownList
        Dim txtVisaPrice As TextBox
        Dim txtPassportNo As TextBox
        Dim lblGuestLineNo As Label

        dt = objBLLGuest.Fillguests(requestid)
        If dt.Rows.Count > 0 Then
            Session("Fillguests") = dt

            For Each item As DataListItem In dlPersonalInfo.Items
                Dim strTittle As String = ""
                lblrlineno = CType(item.FindControl("lblrlineno"), Label)

                lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
                ddlTitle = CType(item.FindControl("ddlTittle"), DropDownList)
                txtFirstNameg = CType(item.FindControl("txtFirstName"), TextBox)
                txtMidName = CType(item.FindControl("txtMiddleName"), TextBox)
                txtLastNameg = CType(item.FindControl("txtlastName"), TextBox)
                txtchildage = CType(item.FindControl("txtchildage"), TextBox)
                ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
                ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
                txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
                txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
                txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)
                Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
                lblGuestLineNo = CType(item.FindControl("lblGuestLineNo"), Label)
                Dim lblType As Label = CType(item.FindControl("lblPType"), Label)

                Dim lblRowHeading = CType(item.FindControl("lblRowHeading"), Label)
                Dim lblroomno = CType(item.FindControl("lblroomno"), Label)

                For i = 0 To dt.Rows.Count - 1
                    'If dt.Rows(i)("rlineno").ToString = Val(lblrlineno.Text) And dt.Rows(i)("type").ToString = lblType.Text.Trim.ToString And dt.Rows(i)("GuestLineNo").ToString = lblGuestLineNo.Text Then              'Modified param 13/10/2018
                    If dt.Rows(i)("rlineno").ToString = Val(lblrlineno.Text) And dt.Rows(i)("GuestLineNo").ToString = lblGuestLineNo.Text Then              'Modified param 13/10/2018
                        txtFirstNameg.Text = dt.Rows(i)("firstname").ToString
                        txtMidName.Text = dt.Rows(i)("middlename").ToString
                        txtLastNameg.Text = dt.Rows(i)("lastname").ToString
                        txtchildage.Text = dt.Rows(i)("ChildAge").ToString
                        txtNationalityCode.Text = dt.Rows(i)("Nationaltycode").ToString
                        txtNationality.Text = dt.Rows(i)("Nationalty").ToString
                        ddlTitle.SelectedValue = dt.Rows(i)("Title").ToString

                    End If
                Next

            Next



            For Each gvRow As DataListItem In dlPersonalInfo.Items
                Dim chkservices As CheckBoxList = gvRow.FindControl("chkservices")
                Dim lblType As Label = gvRow.FindControl("lblPType")
                lblrlineno = CType(gvRow.FindControl("lblrlineno"), Label)
                lblRowNoAll = CType(gvRow.FindControl("lblRowNoAll"), Label)
                lblGuestLineNo = CType(gvRow.FindControl("lblGuestLineNo"), Label)
                If chkservices.Items.Count > 0 Then

                    For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                        Dim othtypcode As String() = chkitem.Value.Split(";")
                        othtypcode = othtypcode(1).Split(":")

                        dt = BLLcommon.AmendGuestServices(requestid)

                        If dt.Rows.Count > 0 Then
                            'chkitem.Selected = False 'changed from here to below --changed by mohamed on 17/09/2018
                            For i = 0 To dt.Rows.Count - 1
                                Dim servicecode As String() = dt.Rows(i)("servicecode").ToString.Split(":")
                                If Val(lblrlineno.Text) = dt.Rows(i)("rlineno").ToString Then 'changed by mohamed on 17/09/2018 'make it false, if particular rlineneno is available
                                    chkitem.Selected = False
                                End If
                                If Val(lblGuestLineNo.Text) = dt.Rows(i)("guestlineno").ToString And Val(lblrlineno.Text) = dt.Rows(i)("rlineno").ToString And othtypcode(1).ToString.Trim = servicecode(1).ToString.Trim Then
                                    chkitem.Selected = True
                                    Exit For
                                Else
                                    chkitem.Selected = False
                                End If
                            Next
                        End If

                    Next
                End If

            Next




        End If


    End Sub

    Private Sub showservice()
        Dim BLLcommon = New BLLCommonFuntions
        Dim strRequestId As String = Session("sRequestId") '"00001124" '

        Dim dt As DataTable

        dt = BLLcommon.GetAllServices(strRequestId)
        If dt.Rows.Count > 0 Then
            Session("showservices") = dt
        End If
    End Sub
    Private Sub showservicenew(ByVal requestid As String, ByVal checkindate As String, ByVal checkoutdate As String)
        Dim BLLcommon = New BLLCommonFuntions
        Dim strRequestId As String = Session("sRequestId") '"00001124" '

        Dim dt As DataTable

        dt = BLLcommon.GetAllServiceswithdates(strRequestId, checkindate, checkoutdate)
        If dt.Rows.Count > 0 Then
            Session("showservices") = dt
        Else
            Session("showservices") = Nothing
        End If
    End Sub
    Private Sub ShowGuests()
        Dim BLLcommon = New BLLCommonFuntions
        Dim strRequestId As String = Session("sRequestId") ' "00001124" '

        Dim dt As New DataTable

        dt.Columns.Add(New DataColumn("Guestname", GetType(String)))
        dt.Columns.Add(New DataColumn("Guestlineno", GetType(String)))
        dt.Columns.Add(New DataColumn("Requestid", GetType(String)))


        Dim row As DataRow = dt.NewRow()
        row("Guestname") = "Adult 1"
        row("Guestlineno") = "1"
        row("Requestid") = "00001124"

        dt.Rows.Add(row)

        row = dt.NewRow()

        row("Guestname") = "Adult 2"
        row("Guestlineno") = "2"
        row("Requestid") = "00001124"

        dt.Rows.Add(row)

        row = dt.NewRow()

        row("Guestname") = "Child 1"
        row("Guestlineno") = "3"
        row("Requestid") = "00001124"
        dt.Rows.Add(row)

        Session("ShowGuests") = dt

    End Sub

    Private Sub BindOtherserviceSummary()
        dvTabOtherServicesSummary.Visible = False

        Dim BLLOtherSearch = New BLLOtherSearch
        Dim dt As DataTable
        Dim strRequestId As String = ""
        strRequestId = GetExistingRequestId()

        ' dt = BLLOtherSearch.GetOtherSummary(strRequestId)
        If Session("sLoginType") = "RO" Then
            dt = BLLOtherSearch.GetOtherSummary(strRequestId)
        Else
            dt = BLLOtherSearch.GetOtherSummary(strRequestId, objResParam.WhiteLabel)
        End If
        If dt.Rows.Count > 0 Then

            dlOtherSummary.Visible = True

            dlOtherSummary.DataSource = dt
            dlOtherSummary.DataBind()

            lblothTotalPrice.Text = dt.Rows(0)("total").ToString
            dvTabOtherServicesSummary.Visible = True

            If hdBookingEngineRateType.Value = "1" Then
                dvOthtotal.Visible = False

            Else
                dvOthtotal.Visible = True
                lblOtherServiceTabTotalPrice.Text = dt.Rows(0)("total").ToString

            End If
            hdOtherServiceTabTotalPrice.Value = "1"
        Else
            dlOtherSummary.Visible = False
        End If



        'If Not Session("sobjBLLOtherSearchActive") Is Nothing Then
        '    BLLOtherSearch = Session("sobjBLLOtherSearchActive")


        '    dt = BLLOtherSearch.GetOtherSummary(BLLOtherSearch.OBRequestId)
        '    If dt.Rows.Count > 0 Then

        '        dlOtherSummary.Visible = True

        '        dlOtherSummary.DataSource = dt
        '        dlOtherSummary.DataBind()

        '        lblothTotalPrice.Text = dt.Rows(0)("total").ToString
        '        dvTabOtherServicesSummary.Visible = True

        '        If hdBookingEngineRateType.Value = "1" Then
        '            dvOthtotal.Visible = False

        '        Else
        '            dvOthtotal.Visible = True
        '            lblOtherServiceTabTotalPrice.Text = dt.Rows(0)("total").ToString

        '        End If
        '        hdOtherServiceTabTotalPrice.Value = "1"
        '    Else
        '        dlOtherSummary.Visible = False
        '    End If
        'Else
        '    dvOtherSummary.Visible = False
        'End If
    End Sub
    Private Sub BindAirportserviceSummary()
        dvTabAirportSummary.Visible = False
        Dim BLLMASearch = New BLLMASearch
        Dim dt As DataTable

        Dim strRequestId As String = ""
        strRequestId = GetExistingRequestId()
        If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
            dt = BLLMASearch.GetAiportMeetSummary(strRequestId, "1")
        Else
            dt = BLLMASearch.GetAiportMeetSummary(strRequestId, "0")
        End If

        If dt.Rows.Count > 0 Then

            dlAirportSummary.Visible = True

            dlAirportSummary.DataSource = dt
            dlAirportSummary.DataBind()

            lblairporttotal.Text = dt.Rows(0)("total").ToString
            dvTabAirportSummary.Visible = True

            If hdBookingEngineRateType.Value = "1" Then
                dvairportTotal.Visible = False

            Else
                dvairportTotal.Visible = True
                lblAirportTabtotalPrice.Text = dt.Rows(0)("total").ToString

            End If
            hdAirportTabtotalPrice.Value = "1"
        Else
            dvAirportSummary.Visible = False
        End If


        'If Not Session("sobjBLLMASearchActive") Is Nothing Then
        '    BLLMASearch = Session("sobjBLLMASearchActive")
        '    dt = BLLMASearch.GetTransferSummary(BLLMASearch.OBRequestId)
        '    If dt.Rows.Count > 0 Then

        '        dlAirportSummary.Visible = True

        '        dlAirportSummary.DataSource = dt
        '        dlAirportSummary.DataBind()

        '        lblairporttotal.Text = dt.Rows(0)("total").ToString
        '        dvTabAirportSummary.Visible = True

        '        If hdBookingEngineRateType.Value = "1" Then
        '            dvairportTotal.Visible = False

        '        Else
        '            dvairportTotal.Visible = True
        '            lblAirportTabtotalPrice.Text = dt.Rows(0)("total").ToString

        '        End If
        '        hdAirportTabtotalPrice.Value = "1"
        '    Else
        '        dvAirportSummary.Visible = False
        '    End If
        'Else
        '    dvAirportSummary.Visible = False
        'End If
    End Sub
    Private Sub LoadMenus()
        Try

            ' Modified by abin on 20180717
            Dim strType As String = ""
            Dim strMenuMobHtml As New StringBuilder
            Dim strMenuHtml As String = ""
            objResParam = Session("sobjResParam")
            strMenuHtml = objBLLMenu.GetMenusReturnAstring(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode, "1", "0")

            ltMenu.Text = strMenuHtml.ToString
            dvMobmenu.InnerHtml = strMenuHtml.ToString
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo '*** Danny 30/06/2018

            End If

        End If
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
        If Session("sLoginType") <> "RO" Then
            dvColumbusReference.Visible = False
        Else
            dvColumbusReference.Visible = True
        End If


    End Sub

    Private Sub BindPersonalInformations()
        Dim objBLLHotelSearch = New BLLHotelSearch
        Dim objBLLTransferSearch = New BLLTransferSearch
        Dim objBLLTourSearch = New BLLTourSearch
        Dim objBLLMASearch = New BLLMASearch
        Dim requestid As String = ""
        Dim strQuery As String = ""
        Dim dt As DataTable
        Dim flightchk As String = ""
        Dim strAdults As String = ""
        Dim strChild As String = ""
        Dim strChilds As String = ""
        Dim strpartyname As String = ""
        Dim strrlineno As String = ""
        Dim strroomno As String = ""
        Dim strshiftfrom As String = "", strfromhotel As String = "", strtohotel As String = ""
        Dim strshiftto As String = ""

        Dim iTotalRows As Integer = Val(strAdults) + Val(strChild)
        Dim iRNo As Integer = 0
        Dim iGuestLineNo As Integer = 0
        Dim dtDynamicPersonalInfo = New DataTable()
        Dim dcRowNo = New DataColumn("RowNo", GetType(String))
        Dim RowNoAll = New DataColumn("RowNoAll", GetType(String))
        Dim Rlineno = New DataColumn("Rlineno", GetType(String))
        Dim dcType = New DataColumn("Type", GetType(String))
        Dim dcChildAge = New DataColumn("ChildAge", GetType(String))
        Dim Partyname = New DataColumn("Partyname", GetType(String))
        Dim Roomno = New DataColumn("Roomno", GetType(String))
        Dim Title = New DataColumn("Title", GetType(String))
        Dim Firstname = New DataColumn("Firstname", GetType(String))
        Dim Middlename = New DataColumn("Middlename", GetType(String))
        Dim Lastname = New DataColumn("Lastname", GetType(String))
        Dim Nationalty = New DataColumn("Nationalty", GetType(String))
        Dim Nationaltycode = New DataColumn("Nationaltycode", GetType(String))
        Dim Shiftfrom = New DataColumn("Shiftfrom", GetType(String))
        Dim Shiftto = New DataColumn("Shiftto", GetType(String))
        Dim Fromhotel = New DataColumn("Fromhotel", GetType(String))
        Dim Tohotel = New DataColumn("Tohotel", GetType(String))
        Dim GuestLineNo = New DataColumn("GuestLineNo", GetType(String))

        dtDynamicPersonalInfo.Columns.Add(dcRowNo)
        dtDynamicPersonalInfo.Columns.Add(RowNoAll)
        dtDynamicPersonalInfo.Columns.Add(Rlineno)
        dtDynamicPersonalInfo.Columns.Add(dcType)
        dtDynamicPersonalInfo.Columns.Add(dcChildAge)
        dtDynamicPersonalInfo.Columns.Add(Partyname)
        dtDynamicPersonalInfo.Columns.Add(Roomno)
        dtDynamicPersonalInfo.Columns.Add(Title)
        dtDynamicPersonalInfo.Columns.Add(Firstname)
        dtDynamicPersonalInfo.Columns.Add(Middlename)
        dtDynamicPersonalInfo.Columns.Add(Lastname)
        dtDynamicPersonalInfo.Columns.Add(Nationalty)
        dtDynamicPersonalInfo.Columns.Add(Nationaltycode)
        dtDynamicPersonalInfo.Columns.Add(Shiftfrom)
        dtDynamicPersonalInfo.Columns.Add(Shiftto)
        dtDynamicPersonalInfo.Columns.Add(Fromhotel)
        dtDynamicPersonalInfo.Columns.Add(Tohotel)
        dtDynamicPersonalInfo.Columns.Add(GuestLineNo)

        'If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

        ' Dim objBLLHotelSearch_ = Session("sobjBLLHotelSearchActive")


        Dim objBLLCommonFuntions As New BLLCommonFuntions
        dt = objBLLCommonFuntions.GetBookingRoomstring(Session("sRequestId"))
        If dt.Rows.Count > 0 Then
            'Changed by mohamed on 11/09/2018 to find out who are not shifted from
            Dim dsGdet As DataSet = objclsUtilities.GetDataFromDataset("execute sp_get_shifting_hotel_detail_guestpage '" & Session("sRequestId") & "'")
            Dim dvGDet As DataView = New DataView(dsGdet.Tables(1))

            For k As Integer = 0 To dt.Rows.Count - 1
                strroomno = dt.Rows(k)("roomno").ToString
                strAdults = dt.Rows(k)("adults").ToString
                strChild = dt.Rows(k)("child").ToString
                strChilds = dt.Rows(k)("childages").ToString.Replace(";", "|")
                strpartyname = dt.Rows(k)("partyname").ToString
                strrlineno = dt.Rows(k)("rlineno").ToString
                strshiftfrom = dt.Rows(k)("shiftfrom").ToString
                strshiftto = dt.Rows(k)("shiftto").ToString
                strfromhotel = dt.Rows(k)("fromhotel").ToString
                strtohotel = dt.Rows(k)("tohotel").ToString

                Dim strShiftFromPartycode As String = dt.Rows(k)("shiftfrompartycode").ToString
                'BindPersonalInfoDataList(strAdults, strChild, strChilds, strpartyname)

                For i As Integer = 0 To strAdults - 1
                    iRNo = iRNo + 1
                  
                    Dim dvDynamicPersonalInfo As DataView = New DataView(dtDynamicPersonalInfo)

                    Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
                    row("RowNo") = (i + 1).ToString
                    row("RowNoAll") = iRNo.ToString

                    If strShiftFromPartycode.Contains("-") Then
                        row("Type") = "Adult 2" + (i + 1).ToString
                    Else
                        row("Type") = "Adult " + (i + 1).ToString
                    End If


                    row("ChildAge") = ""
                    row("Partyname") = strpartyname
                    row("Roomno") = strroomno
                    row("Title") = ""
                    row("Firstname") = ""
                    row("Middlename") = ""
                    row("Lastname") = ""
                    row("Nationalty") = ""
                    row("Nationaltycode") = ""
                    row("rlineno") = strrlineno
                    row("Shiftfrom") = strshiftfrom
                    row("Shiftto") = strshiftto
                    row("Fromhotel") = strfromhotel
                    row("Tohotel") = strtohotel

                    If row("Shiftto") = "0" Then
                        iGuestLineNo = iGuestLineNo + 1
                    Else
                        dvDynamicPersonalInfo.RowFilter = "Shiftfrom=1 and Type='" & row("Type") & "' and ToHotel='" & row("Fromhotel") & "'  and roomno='" & row("roomno") & "'"
                        If dvDynamicPersonalInfo.Count > 0 Then
                            iGuestLineNo = dvDynamicPersonalInfo.Item(0)("GuestLineNo")
                        Else
                            iGuestLineNo = iGuestLineNo + 1
                        End If

                    End If
                    row("GuestLineNo") = iGuestLineNo.ToString
                    'changed by mohamed on 11/09/2018
                    dvGDet.RowFilter = "rlineno=" & row("rlineno") & " and roomno=" & row("RoomNo") & " and PType='" & row("Type") & "'" ' and taken=0"
                    If dvGDet.ToTable.Rows.Count = 0 And row("Shiftto") = "1" And row("Shiftfrom") <> "1" Then
                        row("Shiftto") = "0"
                        'row("Fromhotel") = ""
                    End If

                    dtDynamicPersonalInfo.Rows.Add(row)
                Next
                Dim strChildArray As String() = strChilds.Split("|")
                For i As Integer = 0 To strChild - 1
                    iRNo = iRNo + 1
                    Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
                    row("RowNo") = (i + 1).ToString
                    row("RowNoAll") = iRNo.ToString


                    If strShiftFromPartycode.Contains("-") Then
                        row("Type") = "Child 2" + (i + 1).ToString
                    Else
                        row("Type") = "Child " + (i + 1).ToString
                    End If

                    row("ChildAge") = strChildArray(i)
                    row("Partyname") = strpartyname
                    row("Roomno") = strroomno
                    row("Title") = ""
                    row("Firstname") = ""
                    row("Middlename") = ""
                    row("Lastname") = ""
                    row("Nationalty") = ""
                    row("Nationaltycode") = ""
                    row("rlineno") = strrlineno
                    row("Shiftfrom") = strshiftfrom
                    row("Shiftto") = strshiftto
                    row("Fromhotel") = strfromhotel
                    row("Tohotel") = strtohotel
                    Dim dvDynamicPersonalInfo As DataView = New DataView(dtDynamicPersonalInfo)
                    If row("Shiftto") = "0" Then
                        iGuestLineNo = iGuestLineNo + 1
                    Else
                        dvDynamicPersonalInfo.RowFilter = "Shiftfrom=1 and Type='" & row("Type") & "' and ToHotel='" & row("Fromhotel") & "' and roomno='" & row("roomno") & "'"

                        If dvDynamicPersonalInfo.Count > 0 Then
                            iGuestLineNo = dvDynamicPersonalInfo.Item(0)("GuestLineNo")
                        Else
                            iGuestLineNo = iGuestLineNo + 1
                        End If
                    End If
                    row("GuestLineNo") = iGuestLineNo.ToString

                    'changed by mohamed on 11/09/2018
                    dvGDet.RowFilter = "rlineno=" & row("rlineno") & " and roomno=" & row("RoomNo") & " and PType='" & row("Type") & "'" ' and taken=0"
                    If dvGDet.ToTable.Rows.Count = 0 And row("Shiftto") = "1" Then
                        row("Shiftto") = "0"
                        'row("Fromhotel") = ""
                    End If

                    dtDynamicPersonalInfo.Rows.Add(row)
                Next

            Next

        
        Else
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                strroomno = 0
                strAdults = dt.Rows(0)("adults").ToString
                strChild = dt.Rows(0)("child").ToString
                strChilds = dt.Rows(0)("childages").ToString.Replace(";", "|")
                strpartyname = ""

                For i As Integer = 0 To strAdults - 1
                    iRNo = iRNo + 1
                    Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
                    row("RowNo") = (i + 1).ToString
                    row("RowNoAll") = iRNo.ToString
                    row("Type") = "Adult " + (i + 1).ToString
                    row("ChildAge") = ""
                    row("Partyname") = strpartyname
                    row("Roomno") = strroomno
                    row("Title") = ""
                    row("Firstname") = ""
                    row("Middlename") = ""
                    row("Lastname") = ""
                    row("Nationalty") = ""
                    row("Nationaltycode") = ""
                    row("rlineno") = strrlineno
                    row("Shiftfrom") = ""
                    row("Shiftto") = ""
                    row("Fromhotel") = ""
                    row("Tohotel") = ""
                    row("GuestLineNo") = iRNo.ToString
                    dtDynamicPersonalInfo.Rows.Add(row)
                Next
                Dim strChildArray As String() = strChilds.Split("|")
                For i As Integer = 0 To strChild - 1
                    iRNo = iRNo + 1
                    Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
                    row("RowNo") = (i + 1).ToString
                    row("RowNoAll") = iRNo.ToString
                    row("Type") = "Child " + (i + 1).ToString
                    row("ChildAge") = strChildArray(i)
                    row("Partyname") = strpartyname
                    row("Roomno") = strroomno
                    row("Title") = ""
                    row("Firstname") = ""
                    row("Middlename") = ""
                    row("Lastname") = ""
                    row("Nationalty") = ""
                    row("Nationaltycode") = ""
                    row("rlineno") = strrlineno
                    row("Shiftfrom") = ""
                    row("Shiftto") = ""
                    row("Fromhotel") = ""
                    row("Tohotel") = ""
                    row("GuestLineNo") = iRNo.ToString
                    dtDynamicPersonalInfo.Rows.Add(row)
                Next


            End If
        End If

        Dim adultsic As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=1149")
        Dim childsic As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=1150")

        If dtDynamicPersonalInfo.Rows.Count > 0 Then
            dlPersonalInfo.DataSource = dtDynamicPersonalInfo
            dlPersonalInfo.DataBind()


            For Each gvRow As DataListItem In dlPersonalInfo.Items
                Dim chkservices As CheckBoxList = gvRow.FindControl("chkservices")
                Dim lblType As Label = gvRow.FindControl("lblPType")

                If chkservices.Items.Count > 0 Then

                    For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                        Dim othtypcode As String() = chkitem.Value.Split(";")
                        othtypcode = othtypcode(1).Split(":")
                        If lblType.Text.Contains("Adult") = True And childsic.ToString.Trim = othtypcode(1).ToString.Trim Then
                            chkitem.Selected = False
                        ElseIf lblType.Text.Contains("Child") = True And adultsic.ToString.Trim = othtypcode(1).ToString.Trim Then
                            chkitem.Selected = False
                        Else

                            chkitem.Selected = True


                        End If


                    Next
                End If

            Next
        End If

        Dim dtt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
        If dtt.Rows.Count > 0 And dt.Rows.Count = 1 Then
            If Val(dtt.Rows(0)("adults").ToString) > Val(dt.Rows(0)("adults").ToString) Then
                Dim iDiff As Integer = 0
                iDiff = Val(dtt.Rows(0)("adults").ToString) - Val(dt.Rows(0)("adults").ToString)
                For ii As Integer = 0 To iDiff - 1
                    Createrows()
                Next

            End If
        End If


    End Sub
    Private Sub BindFlightDetailsAmend()
        Dim dtDynamicFlightDetails = New DataTable()

        Dim dtDynamicDepFlightDetails = New DataTable()


        Dim FlightType = New DataColumn("FlightType", GetType(String))
        Dim Flightdate = New DataColumn("Flightdate", GetType(String))
        Dim Flightno = New DataColumn("Flightno", GetType(String))
        Dim Flighttime = New DataColumn("Flighttime", GetType(String))
        Dim Airport = New DataColumn("Airport", GetType(String))
        Dim SameGuest = New DataColumn("SameGuest", GetType(String))
        Dim Flightcode = New DataColumn("Flightcode", GetType(String))
        Dim Guestnames = New DataColumn("Guestnames", GetType(String))
        Dim ArrBordecode = New DataColumn("ArrBordecode", GetType(String))
        Dim Arrivaltoairport = New DataColumn("Arrivaltoairport", GetType(String))
        Dim NRReason = New DataColumn("NAReason", GetType(String))
        Dim NRticked = New DataColumn("NAticked", GetType(String))
        Dim tlineo = New DataColumn("tlineno", GetType(String)) ' S***

        Dim depFlightType = New DataColumn("FlightType", GetType(String))
        Dim depFlightdate = New DataColumn("Flightdate", GetType(String))
        Dim depFlightno = New DataColumn("Flightno", GetType(String))
        Dim depFlighttime = New DataColumn("Flighttime", GetType(String))
        Dim depAirport = New DataColumn("Airport", GetType(String))
        Dim depSameGuest = New DataColumn("SameGuest", GetType(String))
        Dim depFlightcode = New DataColumn("Flightcode", GetType(String))
        Dim depGuestnames = New DataColumn("Guestnames", GetType(String))
        Dim DepBordecode = New DataColumn("DepBordecode", GetType(String))
        Dim DeparturetoAirport = New DataColumn("DeparturetoAirport", GetType(String))
        Dim NAReasonDep = New DataColumn("NAReasonDep", GetType(String))
        Dim NAtickedDep = New DataColumn("NAtickedDep", GetType(String))
        Dim Deptlineo = New DataColumn("tlineno", GetType(String)) ' S***



        dtDynamicFlightDetails.Columns.Add(FlightType)
        dtDynamicFlightDetails.Columns.Add(Flightdate)
        dtDynamicFlightDetails.Columns.Add(Flightno)
        dtDynamicFlightDetails.Columns.Add(Flighttime)
        dtDynamicFlightDetails.Columns.Add(Airport)
        dtDynamicFlightDetails.Columns.Add(SameGuest)
        dtDynamicFlightDetails.Columns.Add(Flightcode)
        dtDynamicFlightDetails.Columns.Add(Guestnames)
        dtDynamicFlightDetails.Columns.Add(ArrBordecode)
        dtDynamicFlightDetails.Columns.Add(Arrivaltoairport)
        dtDynamicFlightDetails.Columns.Add(NRReason)
        dtDynamicFlightDetails.Columns.Add(NRticked)
        dtDynamicFlightDetails.Columns.Add(tlineo) ' S***


        dtDynamicDepFlightDetails.Columns.Add(depFlightType)
        dtDynamicDepFlightDetails.Columns.Add(depFlightdate)
        dtDynamicDepFlightDetails.Columns.Add(depFlightno)
        dtDynamicDepFlightDetails.Columns.Add(depFlighttime)
        dtDynamicDepFlightDetails.Columns.Add(depAirport)
        dtDynamicDepFlightDetails.Columns.Add(depSameGuest)
        dtDynamicDepFlightDetails.Columns.Add(depFlightcode)
        dtDynamicDepFlightDetails.Columns.Add(depGuestnames)
        dtDynamicDepFlightDetails.Columns.Add(DepBordecode)
        dtDynamicDepFlightDetails.Columns.Add(DeparturetoAirport)
        dtDynamicDepFlightDetails.Columns.Add(NAReasonDep)
        dtDynamicDepFlightDetails.Columns.Add(NAtickedDep)
        dtDynamicDepFlightDetails.Columns.Add(Deptlineo) ' S***

        Dim hotelexists As String = "", strQuery As String = ""
        strQuery = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sEditRequestId") & "'"
        hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)



        'Dim dt As New DataTable
        'If hotelexists <> "" Then
        '    dt = objBLLCommonFuntions.GetBookingGuestnames(Session("sEditRequestId"), 1)
        'Else
        '    dt = objBLLCommonFuntions.GetOthersGuestnames(Session("sEditRequestId"), 1)
        'End If


        Dim dt As New DataTable
        Dim dt_ar As New DataTable
        Dim dt_dep As New DataTable
        If hotelexists <> "" Then
            dt_ar = objBLLCommonFuntions.GetBookingGuestnames_new(Session("sRequestId"), 1, "ARRIVAL")
            dt_dep = objBLLCommonFuntions.GetBookingGuestnames_new(Session("sRequestId"), 1, "DEPARTURE")
        Else
            dt = objBLLCommonFuntions.GetOthersGuestnames(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                Session("ShowGuests") = dt
                Session("ShowGuestsDep") = dt
            End If
        End If


        If dt_ar.Rows.Count > 0 Then
            Session("ShowGuests") = dt_ar

        End If
        If dt_dep.Rows.Count > 0 Then
            Session("ShowGuestsDep") = dt_dep
        End If


        Dim dtarrival As DataTable = objBLLCommonFuntions.Getflightdetails(Session("sEditRequestId"), "ARRIVAL", 1)

        Dim row As DataRow

        If dtarrival.Rows.Count > 0 Then
            For i = 0 To dtarrival.Rows.Count - 1
                row = dtDynamicFlightDetails.NewRow()

                row("FlightType") = "ARRIVAL"
                row("Flightdate") = dtarrival.Rows(i)("Flightdate").ToString
                row("Flightno") = dtarrival.Rows(i)("flightno").ToString
                row("Flighttime") = dtarrival.Rows(i)("flighttime").ToString
                row("Airport") = dtarrival.Rows(i)("Origin").ToString
                row("SameGuest") = "1"
                row("Flightcode") = dtarrival.Rows(i)("flightcode").ToString
                row("Guestnames") = ""
                row("ArrBordecode") = dtarrival.Rows(i)("ArrBordecode").ToString
                row("Arrivaltoairport") = dtarrival.Rows(i)("airport").ToString
                row("NAReason") = dtarrival.Rows(i)("NAReason").ToString
                row("NAticked") = dtarrival.Rows(i)("NAticked").ToString
                row("tlineno") = dtarrival.Rows(i)("tlineno").ToString ' S***
                dtDynamicFlightDetails.Rows.Add(row)
            Next
        End If




        dlFlightDetails.DataSource = dtDynamicFlightDetails
        dlFlightDetails.DataBind()


        Dim dtdepart As DataTable = objBLLCommonFuntions.Getflightdetails(Session("sRequestId"), "DEPARTURE", 1)
        Dim deprow As DataRow

        If dtdepart.Rows.Count > 0 Then
            For i = 0 To dtdepart.Rows.Count - 1
                deprow = dtDynamicDepFlightDetails.NewRow()

                deprow("FlightType") = "DEPARTURE"
                deprow("Flightdate") = dtdepart.Rows(i)("Flightdate").ToString
                deprow("Flightno") = dtdepart.Rows(i)("flightno").ToString
                deprow("Flighttime") = dtdepart.Rows(i)("flighttime").ToString
                deprow("Airport") = dtdepart.Rows(i)("airport").ToString
                deprow("SameGuest") = "1"
                deprow("Flightcode") = dtdepart.Rows(i)("flightcode").ToString
                deprow("Guestnames") = ""
                deprow("DepBordecode") = dtdepart.Rows(i)("ArrBordecode").ToString
                deprow("DeparturetoAirport") = dtdepart.Rows(i)("Origin").ToString
                deprow("NAReasonDep") = dtdepart.Rows(i)("NAReason").ToString
                deprow("NAtickedDep") = dtdepart.Rows(i)("NAticked").ToString
                deprow("tlineno") = dtdepart.Rows(i)("tlineno").ToString ' S***

                dtDynamicDepFlightDetails.Rows.Add(deprow)
            Next


        End If



        dldeparturedetails.DataSource = dtDynamicDepFlightDetails
        dldeparturedetails.DataBind()


        'For Each gvRow As DataListItem In dlFlightDetails.Items
        '    Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")

        '    If chkguest.Items.Count > 0 Then
        '        For Each chkitem As ListItem In chkguest.Items
        '            chkitem.Selected = True
        '        Next
        '    End If

        'Next

        For Each gvRow As DataListItem In dlFlightDetails.Items

            Dim ChkFlightNotRequired As CheckBox = CType(gvRow.FindControl("ChkFlightNotRequired"), CheckBox)
              If dlFlightDetails.Items.Count = 1 Then
                ChkFlightNotRequired.Attributes.Add("style", "display:none")

            End If



            Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")
            Dim txtArrivalflight As TextBox = gvRow.FindControl("txtArrivalflight")
            If chkguest.Items.Count > 0 Then

                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    Dim othtypcode As String() = chkitem.Value.Split(";")

                    chkitem.Selected = False

                    dt = Session("ShowGuests")

                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            Dim guestcode As String() = dt.Rows(i)("guestlineno").ToString.Split(";")
                            '  If othtypcode(1).ToString.Trim = guestcode(1).ToString.Trim And othtypcode(0).ToString.Trim = guestcode(0).ToString.Trim And txtArrivalflight.Text = guestcode(2).ToString.Trim Then
                            If othtypcode(1).ToString.Trim = guestcode(1).ToString.Trim And othtypcode(0).ToString.Trim = guestcode(0).ToString.Trim Then
                                chkitem.Selected = True
                                Exit For
                            End If

                        Next
                    End If

                Next
            End If

        Next


        'For Each gvRow As DataListItem In dldeparturedetails.Items
        '    Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")

        '    If chkguest.Items.Count > 0 Then
        '        For Each chkitem As ListItem In chkguest.Items
        '            chkitem.Selected = True
        '        Next
        '    End If

        'Next

        For Each gvRow As DataListItem In dldeparturedetails.Items

            Dim chkDepFlightNotrquired As CheckBox = CType(gvRow.FindControl("chkDepFlightNotrquired"), CheckBox)
          If dldeparturedetails.Items.Count = 1 Then
                chkDepFlightNotrquired.Attributes.Add("style", "display:none")
            End If


            Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")
            Dim txtDepartureFlight As TextBox = gvRow.FindControl("txtDepartureFlight")
            If chkguest.Items.Count > 0 Then

                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    Dim othtypcode As String() = chkitem.Value.Split(";")

                    chkitem.Selected = False

                    dt = Session("ShowGuestsDep")

                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            Dim guestcode As String() = dt.Rows(i)("guestlineno").ToString.Split(";")
                            ' If othtypcode(1).ToString.Trim = guestcode(1).ToString.Trim And othtypcode(0).ToString.Trim = guestcode(0).ToString.Trim And txtDepartureFlight.Text = guestcode(3).ToString.Trim Then
                            If othtypcode(1).ToString.Trim = guestcode(1).ToString.Trim And othtypcode(0).ToString.Trim = guestcode(0).ToString.Trim Then
                                chkitem.Selected = True
                                Exit For
                            End If

                        Next
                    End If

                Next
            End If

        Next


    End Sub
    Private Sub BindFlightDetails(ByVal intAuto As Integer)
        Dim dtDynamicFlightDetails = New DataTable()
        Dim dtDynamicDepFlightDetails = New DataTable()

        Dim FlightType = New DataColumn("FlightType", GetType(String))
        Dim Flightdate = New DataColumn("Flightdate", GetType(String))
        Dim Flightno = New DataColumn("Flightno", GetType(String))
        Dim Flighttime = New DataColumn("Flighttime", GetType(String))
        Dim Airport = New DataColumn("Airport", GetType(String))
        Dim SameGuest = New DataColumn("SameGuest", GetType(String))
        Dim Flightcode = New DataColumn("Flightcode", GetType(String))
        Dim Guestnames = New DataColumn("Guestnames", GetType(String))
        Dim ArrBordecode = New DataColumn("ArrBordecode", GetType(String))
        Dim Arrivaltoairport = New DataColumn("Arrivaltoairport", GetType(String))
        Dim NRReason = New DataColumn("NAReason", GetType(String))
        Dim NRticked = New DataColumn("NAticked", GetType(String))
        Dim tlineno = New DataColumn("tlineno", GetType(String))  ''S***

        Dim depFlightType = New DataColumn("FlightType", GetType(String))
        Dim depFlightdate = New DataColumn("Flightdate", GetType(String))
        Dim depFlightno = New DataColumn("Flightno", GetType(String))
        Dim depFlighttime = New DataColumn("Flighttime", GetType(String))
        Dim depAirport = New DataColumn("Airport", GetType(String))
        Dim depSameGuest = New DataColumn("SameGuest", GetType(String))
        Dim depFlightcode = New DataColumn("Flightcode", GetType(String))
        Dim depGuestnames = New DataColumn("Guestnames", GetType(String))
        Dim DepBordecode = New DataColumn("DepBordecode", GetType(String))
        Dim DeparturetoAirport = New DataColumn("DeparturetoAirport", GetType(String))
        Dim NAReasonDep = New DataColumn("NAReasonDep", GetType(String))
        Dim NAtickedDep = New DataColumn("NAtickedDep", GetType(String))
        Dim Deptlineno = New DataColumn("tlineno", GetType(String))  ''S***

        dtDynamicFlightDetails.Columns.Add(FlightType)
        dtDynamicFlightDetails.Columns.Add(Flightdate)
        dtDynamicFlightDetails.Columns.Add(Flightno)
        dtDynamicFlightDetails.Columns.Add(Flighttime)
        dtDynamicFlightDetails.Columns.Add(Airport)
        dtDynamicFlightDetails.Columns.Add(SameGuest)
        dtDynamicFlightDetails.Columns.Add(Flightcode)
        dtDynamicFlightDetails.Columns.Add(Guestnames)
        dtDynamicFlightDetails.Columns.Add(ArrBordecode)
        dtDynamicFlightDetails.Columns.Add(Arrivaltoairport)
        dtDynamicFlightDetails.Columns.Add(NRReason)
        dtDynamicFlightDetails.Columns.Add(NRticked)
        dtDynamicFlightDetails.Columns.Add(tlineno)  ''S***

        dtDynamicDepFlightDetails.Columns.Add(depFlightType)
        dtDynamicDepFlightDetails.Columns.Add(depFlightdate)
        dtDynamicDepFlightDetails.Columns.Add(depFlightno)
        dtDynamicDepFlightDetails.Columns.Add(depFlighttime)
        dtDynamicDepFlightDetails.Columns.Add(depAirport)
        dtDynamicDepFlightDetails.Columns.Add(depSameGuest)
        dtDynamicDepFlightDetails.Columns.Add(depFlightcode)
        dtDynamicDepFlightDetails.Columns.Add(depGuestnames)
        dtDynamicDepFlightDetails.Columns.Add(DepBordecode)
        dtDynamicDepFlightDetails.Columns.Add(DeparturetoAirport)
        dtDynamicDepFlightDetails.Columns.Add(NAReasonDep)
        dtDynamicDepFlightDetails.Columns.Add(NAtickedDep)
        dtDynamicDepFlightDetails.Columns.Add(Deptlineno)  ''S***

        Dim hotelexists As String = "", strQuery As String = ""
        strQuery = "select 't' from view_booking_hotel_prearr(nolock) where requestid='" & Session("sRequestId") & "'"
        hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        Dim dt As New DataTable
        If hotelexists <> "" Then
            dt = objBLLCommonFuntions.GetBookingGuestnames(Session("sRequestId"))
        Else
            dt = objBLLCommonFuntions.GetOthersGuestnames(Session("sRequestId"))
        End If

        If dt.Rows.Count > 0 Then
            Session("ShowGuests") = dt
            Session("ShowGuestsDep") = dt
        End If

        Dim dtarrival As DataTable = objBLLCommonFuntions.Getflightdetails(Session("sRequestId"), "ARRIVAL")

        Dim row As DataRow = dtDynamicFlightDetails.NewRow()
        'changed by mohamed on 11/09/2018 'Generated all the rows instead of 1 row
        If dtarrival.Rows.Count > 0 Then
            For i = 0 To dtarrival.Rows.Count - 1
                row = dtDynamicFlightDetails.NewRow()
                row("FlightType") = "ARRIVAL"
                row("Flightdate") = dtarrival.Rows(i)("Flightdate").ToString
                row("Flightno") = dtarrival.Rows(i)("flightno").ToString
                row("Flighttime") = dtarrival.Rows(i)("flighttime").ToString
                row("Airport") = dtarrival.Rows(i)("Origin").ToString
                row("SameGuest") = "1"
                row("Flightcode") = dtarrival.Rows(i)("flightcode").ToString
                row("Guestnames") = ""
                row("ArrBordecode") = dtarrival.Rows(i)("ArrBordecode").ToString
                row("Arrivaltoairport") = dtarrival.Rows(i)("airport").ToString

                If dtarrival.Rows(i)("ArrBordecode").ToString = "" Then
                    Dim dtArrBordecode As DataTable = objBLLCommonFuntions.GetBordercode(Session("sRequestId"), "ARRIVAL")
                    If dtArrBordecode.Rows.Count > 0 Then
                        row("ArrBordecode") = dtArrBordecode.Rows(i)("airportbordercode").ToString
                        row("Arrivaltoairport") = dtArrBordecode.Rows(i)("airport").ToString
                    End If
                End If

                row("NAReason") = ""
                row("NAticked") = ""
                row("tlineno") = dtarrival.Rows(i)("tlineno").ToString ''S***
                dtDynamicFlightDetails.Rows.Add(row)
            Next
        Else
            row("FlightType") = "ARRIVAL"
            row("Flightdate") = ""
            row("Flightno") = ""
            row("Flighttime") = ""
            row("Airport") = ""
            row("SameGuest") = "1"
            row("Flightcode") = ""
            row("Guestnames") = ""
            row("ArrBordecode") = ""
            row("Arrivaltoairport") = ""
            row("NAReason") = ""
            row("NAticked") = ""
            row("ArrBordecode") = "" ''' Changed shahul 11/06/18
            row("Arrivaltoairport") = ""
            row("tlineno") = "" ''S***

            ''' Changed shahul 11/06/18
            'Dim dtArrBordecode As DataTable = objBLLCommonFuntions.GetBordercode(Session("sRequestId"), "ARRIVAL")
            'If dtArrBordecode.Rows.Count > 0 Then
            '    row("ArrBordecode") = dtArrBordecode.Rows(0)("airportbordercode").ToString
            '    row("Arrivaltoairport") = dtArrBordecode.Rows(0)("airport").ToString
            'End If
            dtDynamicFlightDetails.Rows.Add(row)
        End If

        dlFlightDetails.DataSource = dtDynamicFlightDetails
        dlFlightDetails.DataBind()

        Dim dtdepart As DataTable = objBLLCommonFuntions.Getflightdetails(Session("sRequestId"), "DEPARTURE")
        Dim deprow As DataRow = dtDynamicDepFlightDetails.NewRow()
        'row = dtDynamicFlightDetails.NewRow()

        'changed by mohamed on 11/09/2018 'Generated all the rows instead of 1 row
        If dtdepart.Rows.Count > 0 Then
            For i = 0 To dtdepart.Rows.Count - 1
                deprow = dtDynamicDepFlightDetails.NewRow()
                deprow("FlightType") = "DEPARTURE"
                deprow("Flightdate") = dtdepart.Rows(i)("Flightdate").ToString
                deprow("Flightno") = dtdepart.Rows(i)("flightno").ToString
                deprow("Flighttime") = dtdepart.Rows(i)("flighttime").ToString
                deprow("Airport") = dtdepart.Rows(i)("airport").ToString
                deprow("SameGuest") = "1"
                deprow("Flightcode") = dtdepart.Rows(i)("flightcode").ToString
                deprow("Guestnames") = ""
                deprow("DepBordecode") = dtdepart.Rows(i)("ArrBordecode").ToString
                deprow("DeparturetoAirport") = dtdepart.Rows(i)("Origin").ToString
                deprow("NAReasonDep") = ""
                deprow("NAtickedDep") = ""


                If dtdepart.Rows(i)("ArrBordecode").ToString = "" Then ''' Changed shahul 11/06/18
                    Dim dtArrBordecode As DataTable = objBLLCommonFuntions.GetBordercode(Session("sRequestId"), "DEPARTURE")
                    If dtArrBordecode.Rows.Count > 0 Then
                        deprow("DepBordecode") = dtArrBordecode.Rows(i)("airportbordercode").ToString
                        deprow("Airport") = dtArrBordecode.Rows(i)("airport").ToString
                    End If
                End If
                deprow("tlineno") = dtdepart.Rows(i)("tlineno").ToString ''S***
                dtDynamicDepFlightDetails.Rows.Add(deprow)
            Next
        Else
            deprow("FlightType") = "DEPARTURE"
            deprow("Flightdate") = ""
            deprow("Flightno") = ""
            deprow("Flighttime") = ""
            deprow("Airport") = ""
            deprow("SameGuest") = "1"
            deprow("Flightcode") = ""
            deprow("Guestnames") = ""
            deprow("NAReasonDep") = ""
            deprow("NAtickedDep") = ""
            deprow("DepBordecode") = ""   ''' Changed shahul 11/06/18
            deprow("Airport") = ""
            deprow("tlineno") = "" ''S***
            ''' Changed shahul 11/06/18
            'Dim dtArrBordecode As DataTable = objBLLCommonFuntions.GetBordercode(Session("sRequestId"), "DEPARTURE")
            'If dtArrBordecode.Rows.Count > 0 Then
            '    deprow("DepBordecode") = dtArrBordecode.Rows(0)("airportbordercode").ToString
            '    deprow("Airport") = dtArrBordecode.Rows(0)("airport").ToString
            'End If
            dtDynamicDepFlightDetails.Rows.Add(deprow)
        End If

        dldeparturedetails.DataSource = dtDynamicDepFlightDetails
        dldeparturedetails.DataBind()


        For Each gvRow As DataListItem In dlFlightDetails.Items
            Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")
            Dim NAReason As TextBox = gvRow.FindControl("txtreason")
            Dim NAticked As CheckBox = gvRow.FindControl("chkNA")

            If chkguest.Items.Count > 0 Then
                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    chkitem.Selected = True
                Next
            End If
            'If intAuto = 1 Then
            '    NAReason.Text = "NA"
            '    NAticked.Checked = True
            'End If


            Dim ChkFlightNotRequired As CheckBox = CType(gvRow.FindControl("ChkFlightNotRequired"), CheckBox)
            If dlFlightDetails.Items.Count = 1 Then
                ChkFlightNotRequired.Attributes.Add("style", "display:none")

            End If

        Next

        For Each gvRow As DataListItem In dldeparturedetails.Items
            Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")
            Dim NAReason As TextBox = gvRow.FindControl("txtdepreason")
            Dim NAticked As CheckBox = gvRow.FindControl("chkdepNA")
            If chkguest.Items.Count > 0 Then
                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    chkitem.Selected = True
                Next
            End If
            'If intAuto = 1 Then
            '    NAReason.Text = "NA"
            '    NAticked.Checked = True
            'End If

            Dim chkDepFlightNotrquired As CheckBox = CType(gvRow.FindControl("chkDepFlightNotrquired"), CheckBox)
           If dldeparturedetails.Items.Count = 1 Then
                chkDepFlightNotrquired.Attributes.Add("style", "display:none")
            End If

        Next

        If intAuto = 1 Then
            For Each item As DataListItem In dlFlightDetails.Items
                Dim btnAppArrival As Button = item.FindControl("btnAppArrival")
                FlightArriovalSave(btnAppArrival)
                Exit For
            Next
            For Each item As DataListItem In dldeparturedetails.Items
                Dim btnAppDeparture As Button = item.FindControl("btnAppDeparture")
                FlightDepartureSave(btnAppDeparture)
                Exit For
            Next
        End If


    End Sub
    Private Sub FlightDepartureSave(ByVal sender As Object)
        Dim dt As DataTable
        Dim btnAppDeparture As Button = CType(sender, Button)
        Dim dlItem As DataListItem = CType((btnAppDeparture).NamingContainer, DataListItem)

        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        Dim strQuery As String = ""
        Dim rsDefaultFlightNo As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=57")

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet, myDS1 As New DataSet


        Dim Arrivalflight As New List(Of String)
        strSqlQry = "select top 1 flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'05/11/2018',103))) flight_tranid,flightcode from view_flightmast_departure where flight_tranid ='" + rsDefaultFlightNo + "'"

        Dim SqlConn As New SqlConnection
        Dim myDataAdapter As New SqlDataAdapter
        SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
        'Open connection
        myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
        myDataAdapter.Fill(myDS)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then

                Dim strFlightCode As String = ""
                Dim strDayName As String = ""
                If myDS.Tables(0).Rows(0)(0).ToString <> "" Then
                    Dim strFlightCodes As String() = myDS.Tables(0).Rows(0)(0).Split("|")
                    strFlightCode = strFlightCodes(0)
                    strDayName = strFlightCodes(1)
                End If

                'strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername,airportbordercode,airport  from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
                strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername,airportbordercode,airport  from view_flightmast_departure where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
                Dim SqlConn1 As New SqlConnection
                Dim myDataAdapter1 As New SqlDataAdapter
                ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                SqlConn1 = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter1 = New SqlDataAdapter(strSqlQry, SqlConn1)
                myDataAdapter1.Fill(myDS1, "Customers")

                If myDS1.Tables.Count > 0 Then
                    If myDS1.Tables(0).Rows.Count > 0 Then
                        'If ValidateDepartureFlightDetails() Then


                        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))

                        '' Departure Flights
                        If dt.Rows.Count > 0 Then
                            objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                            For Each item As DataListItem In dldeparturedetails.Items
                                'For Each item As DataListItem In dldeparturedetails.Items

                                Dim strTittle As String = ""

                                If item.ItemIndex = dlItem.ItemIndex Then

                                    Dim chkDepFlightNotrquired As CheckBox = CType(dlItem.FindControl("chkDepFlightNotrquired"), CheckBox)
                                    If chkDepFlightNotrquired.Checked = False Then


                                        Dim txtDepartureDate As TextBox = CType(dlItem.FindControl("txtDepartureDate"), TextBox)
                                        Dim txtDepartureFlight As TextBox = CType(dlItem.FindControl("txtDepartureFlight"), TextBox)
                                        Dim txtDepartureTime As TextBox = CType(dlItem.FindControl("txtDepartureTime"), TextBox)
                                        Dim txtDepartureAirport As TextBox = CType(dlItem.FindControl("txtDepartureAirport"), TextBox)
                                        Dim txtDeparturetoAirport As TextBox = CType(dlItem.FindControl("txtDeparturetoAirport"), TextBox)
                                        Dim chkguest As CheckBoxList = CType(item.FindControl("chkguest"), CheckBoxList)
                                        Dim txtDepartureFlightCode As TextBox = CType(dlItem.FindControl("txtDepartureFlightCode"), TextBox)
                                        Dim txtDepBordecode As TextBox = CType(dlItem.FindControl("txtDepBordecode"), TextBox)

                                        Dim chkdepNA As CheckBox = CType(dlItem.FindControl("chkdepNA"), CheckBox)
                                        Dim divdepreason As HtmlGenericControl = CType(dlItem.FindControl("divdepreason"), HtmlGenericControl)
                                        Dim txtdepreason As TextBox = CType(dlItem.FindControl("txtdepreason"), TextBox)


                                        txtDepartureFlightCode.Text = strFlightCode
                                        txtDepartureTime.Text = myDS1.Tables(0).Rows(0)("destintime").ToString
                                        txtDeparturetoAirport.Text = myDS1.Tables(0).Rows(0)("airportbordername").ToString
                                        txtDepBordecode.Text = myDS1.Tables(0).Rows(0)("airportbordercode").ToString
                                        txtDepartureAirport.Text = myDS1.Tables(0).Rows(0)("airport").ToString

                                        If chkguest.Items.Count > 0 Then
                                            Dim k As Integer = 1
                                            For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                                                If chkitem.Selected = True Then

                                                    Dim Arrdate As String = txtDepartureDate.Text
                                                    If Arrdate <> "" Then
                                                        Dim strDates As String() = Arrdate.Split("/")
                                                        Arrdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                                                    End If

                                                    Dim flighttranid As String() = txtDepartureFlightCode.Text.Split("|")

                                                    Dim strGuestValues As String() = chkitem.Value.Split("|")
                                                    For i As Integer = 0 To strGuestValues.Length - 1

                                                        Dim guestline As String() = strGuestValues(i).Split(";")

                                                        strBuffer.Append("<DocumentElement>")
                                                        If chkdepNA.Checked = True Then
                                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                            strBuffer.Append("<departuredate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</departuredate>")
                                                            strBuffer.Append(" <flightcode></flightcode>")
                                                            strBuffer.Append(" <flight_tranid></flight_tranid>")
                                                            strBuffer.Append(" <flighttime></flighttime>")
                                                            strBuffer.Append(" <depaiport></depaiport>")
                                                            strBuffer.Append(" <originaiport></originaiport>")
                                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                            strBuffer.Append(" <NAReason>" & txtdepreason.Text & "</NAReason>")
                                                            strBuffer.Append(" <NAticked>1</NAticked>")
                                                        Else
                                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                            strBuffer.Append("<departuredate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</departuredate>")
                                                            strBuffer.Append(" <flightcode>" & CType(txtDepartureFlight.Text, String) & "</flightcode>")
                                                            strBuffer.Append(" <flight_tranid>" & CType(flighttranid(0), String) & "</flight_tranid>")
                                                            strBuffer.Append(" <flighttime>" & CType(myDS1.Tables(0).Rows(0)("destintime").ToString, String) & "</flighttime>")
                                                            strBuffer.Append(" <depaiport>" & CType(myDS1.Tables(0).Rows(0)("airportbordercode").ToString, String) & "</depaiport>")
                                                            strBuffer.Append(" <originaiport>" & CType(myDS1.Tables(0).Rows(0)("airport").ToString, String) & "</originaiport>")
                                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                            strBuffer.Append(" <NAReason>" & txtdepreason.Text & "</NAReason>")
                                                            strBuffer.Append(" <NAticked>0</NAticked>")
                                                        End If

                                                        strBuffer.Append("</DocumentElement>")
                                                    Next
                                                End If
                                            Next

                                        End If
                                    End If
                                End If
                            Next
                        End If

                        objBLLguest.GBDepFlightXml = strBuffer.ToString
                        objBLLguest.GBuserlogged = Session("GlobalUserName")

                        If objBLLguest.SavingDepartureFlightTemp() Then
                            EnableDepartureFlightdetails()

                            Dim hotelexists As String = "", strQuery1 As String = ""
                            strQuery1 = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
                            hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)

                            Dim dt1 As New DataTable

                            If hotelexists <> "" Then
                                dt1 = objBLLCommonFuntions.GetBookingGuestnames_departure(Session("sRequestId"))
                            Else
                                dt1 = objBLLCommonFuntions.GetOtherGuestnames_departure(Session("sRequestId"))
                            End If

                            ' Dim dt1 As DataTable = objBLLCommonFuntions.GetBookingGuestnames_departure(Session("sRequestId"))
                            If dt1.Rows.Count > 0 Then
                                btnAddDepflight.Style.Add("display", "block")
                                'btnAddDepflight_Click(sender, e)
                                createdepflightrows()
                            Else
                                btnAddDepflight.Style.Add("display", "none")
                            End If
                            'Dim btnAppDeparture1 As Button = CType(dldeparturedetails.Items(dlItem.ItemIndex).FindControl("btnAppDeparture"), Button)
                            'btnAppDeparture1.Text = "Selected"

                            'MessageBox.ShowMessage(Page, MessageType.Success, "Saved Departure Details")
                        End If

                        'End If
                    End If
                End If
            End If
        End If



    End Sub
    Private Sub FlightArriovalSave(ByVal sender As Object)
        'Dim objbtn As Button = CType(sender, Button)
        'Dim rowid As Integer = 0
        'Dim row As DataListItem
        'row = CType(objbtn.NamingContainer, DataListItem)
        'rowid = row.ItemIndex

        'Dim dtRow As DataRow
        'dtRow = DataListItemToDataRow(row)

        Dim dt As DataTable
        Dim btnAppArrival As Button = CType(sender, Button)
        Dim dlItem As DataListItem = CType((btnAppArrival).NamingContainer, DataListItem)

        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        Dim strQuery As String = ""

        Dim rsDefaultFlightNo As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=56")

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet, myDS1 As New DataSet


        Dim Arrivalflight As New List(Of String)
        strSqlQry = "select top 1 flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'05/11/2018',103))) flight_tranid,flightcode from view_flightmast_arrival_TBA where flight_tranid ='" + rsDefaultFlightNo + "'"

        Dim SqlConn As New SqlConnection
        Dim myDataAdapter As New SqlDataAdapter
        SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
        'Open connection
        myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
        myDataAdapter.Fill(myDS)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then

                Dim strFlightCode As String = ""
                Dim strDayName As String = ""
                If myDS.Tables(0).Rows(0)(0).ToString <> "" Then
                    Dim strFlightCodes As String() = myDS.Tables(0).Rows(0)(0).Split("|")
                    strFlightCode = strFlightCodes(0)
                    strDayName = strFlightCodes(1)
                End If

                strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_arrival_TBA.airportbordercode)airportbordername,airportbordercode,airport  from view_flightmast_arrival_TBA where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"

                Dim SqlConn1 As New SqlConnection
                Dim myDataAdapter1 As New SqlDataAdapter
                ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                SqlConn1 = clsDBConnect.dbConnectionnew("strDBConnection")
                'Open connection
                myDataAdapter1 = New SqlDataAdapter(strSqlQry, SqlConn1)
                myDataAdapter1.Fill(myDS1, "Customers")

                If myDS1.Tables.Count > 0 Then
                    If myDS1.Tables(0).Rows.Count > 0 Then
                        'If ValidateArrivalFlightDetails() Then



                        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                        'If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
                        '    Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
                        '    objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
                        '    objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
                        '    objBLLguest.GBRlineno = objBLLHotelSearch.OBrlineno
                        'Else
                        '    objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                        '    objBLLguest.GBRlineno = "0"
                        'End If

                        '' Arrival Flights
                        If dt.Rows.Count > 0 Then
                            objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                            For Each item As DataListItem In dlFlightDetails.Items
                                'For Each item As DataListItem In dlFlightDetails.Items

                                Dim strTittle As String = ""

                                If item.ItemIndex = dlItem.ItemIndex Then
                                    Dim ChkFlightNotRequired As CheckBox = CType(dlItem.FindControl("ChkFlightNotRequired"), CheckBox)
                                    If ChkFlightNotRequired.Checked = False Then



                                        Dim txtArrivalDate As TextBox = CType(dlItem.FindControl("txtArrivalDate"), TextBox)
                                        Dim txtArrivalflight As TextBox = CType(dlItem.FindControl("txtArrivalflight"), TextBox)
                                        Dim txtArrivalTime As TextBox = CType(dlItem.FindControl("txtArrivalTime"), TextBox)
                                        Dim txtArrivalAirport As TextBox = CType(dlItem.FindControl("txtArrivalAirport"), TextBox)
                                        Dim txtArrivaltoairport As TextBox = CType(dlItem.FindControl("txtArrivaltoairport"), TextBox)
                                        Dim chkguest As CheckBoxList = CType(item.FindControl("chkguest"), CheckBoxList)
                                        Dim txtArrivalflightCode As TextBox = CType(dlItem.FindControl("txtArrivalflightCode"), TextBox)
                                        Dim txtArrBordecode As TextBox = CType(dlItem.FindControl("txtArrBordecode"), TextBox)
                                        Dim btnAppArrival1 As Button = CType(dlItem.FindControl("btnAppArrival"), Button)

                                        Dim chkNA As CheckBox = CType(dlItem.FindControl("chkNA"), CheckBox)
                                        Dim divreason As HtmlGenericControl = CType(dlItem.FindControl("divreason"), HtmlGenericControl)
                                        Dim txtreason As TextBox = CType(dlItem.FindControl("txtreason"), TextBox)

                                        txtArrivalflightCode.Text = strFlightCode
                                        txtArrivalTime.Text = myDS1.Tables(0).Rows(0)("destintime").ToString
                                        txtArrivaltoairport.Text = myDS1.Tables(0).Rows(0)("airportbordername").ToString
                                        txtArrBordecode.Text = myDS1.Tables(0).Rows(0)("airportbordercode").ToString
                                        txtArrivalAirport.Text = myDS1.Tables(0).Rows(0)("airport").ToString

                                        If chkguest.Items.Count > 0 Then
                                            Dim k As Integer = 1
                                            For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                                                If chkitem.Selected = True Then

                                                    Dim Arrdate As String = txtArrivalDate.Text
                                                    If Arrdate <> "" Then
                                                        Dim strDates As String() = Arrdate.Split("/")
                                                        Arrdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                                                    End If

                                                    Dim flighttranid As String() = txtArrivalflightCode.Text.Split("|")

                                                    Dim strGuestValues As String() = chkitem.Value.Split("|")
                                                    For i As Integer = 0 To strGuestValues.Length - 1

                                                        Dim guestline As String() = strGuestValues(i).Split(";")

                                                        strBuffer.Append("<DocumentElement>")

                                                        If chkNA.Checked = True Then
                                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                            strBuffer.Append("<arrivaldate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</arrivaldate>")
                                                            strBuffer.Append(" <flightcode></flightcode>")
                                                            strBuffer.Append(" <flight_tranid></flight_tranid>")
                                                            strBuffer.Append(" <flighttime></flighttime>")
                                                            strBuffer.Append(" <arraiport></arraiport>")
                                                            strBuffer.Append(" <originaiport></originaiport>")
                                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                            strBuffer.Append(" <NAReason>" & txtreason.Text & "</NAReason>")
                                                            strBuffer.Append(" <NAticked>1</NAticked>")
                                                        Else
                                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                            strBuffer.Append("<arrivaldate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</arrivaldate>")
                                                            strBuffer.Append(" <flightcode>" & CType(txtArrivalflight.Text, String) & "</flightcode>")
                                                            strBuffer.Append(" <flight_tranid>" & CType(flighttranid(0), String) & "</flight_tranid>")
                                                            strBuffer.Append(" <flighttime>" & CType(myDS1.Tables(0).Rows(0)("destintime").ToString, String) & "</flighttime>")
                                                            strBuffer.Append(" <arraiport>" & CType(myDS1.Tables(0).Rows(0)("airportbordercode").ToString, String) & "</arraiport>")
                                                            strBuffer.Append(" <originaiport>" & CType(myDS1.Tables(0).Rows(0)("airport").ToString, String) & "</originaiport>")
                                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                            strBuffer.Append(" <NAReason>" & txtreason.Text & "</NAReason>")
                                                            strBuffer.Append(" <NAticked>0</NAticked>")
                                                        End If


                                                        strBuffer.Append("</DocumentElement>")

                                                    Next

                                                 

                                                End If
                                            Next
                                            ' Dim btnAppArrival1 As Button = CType(dlFlightDetails.Items(dlItem.ItemIndex).FindControl("btnAppArrival"), Button)
                                            '   btnAppArrival1.Text = "Selected"
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        objBLLguest.GBFlightXml = strBuffer.ToString
                        objBLLguest.GBuserlogged = Session("GlobalUserName")

                        If objBLLguest.SavingArrivalFlightTemp() Then
                            EnableArrivalFlightdetails()
                            Dim hotelexists As String = "", strQuery1 As String = ""
                            strQuery1 = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
                            hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)

                            Dim dt1 As New DataTable

                            If hotelexists <> "" Then
                                dt1 = objBLLCommonFuntions.GetBookingGuestnames_arrival(Session("sRequestId"))
                            Else
                                dt1 = objBLLCommonFuntions.GetOtherGuestnames_arrival(Session("sRequestId"))
                            End If

                            ' Dim dt1 As DataTable = objBLLCommonFuntions.GetBookingGuestnames_arrival(Session("sRequestId"))
                            If dt1.Rows.Count > 0 Then
                                btnAddflight.Style.Add("display", "block")
                                'btnAddflight_Click(sender, e)
                                CreateflightRows()
                            Else
                                btnAddflight.Style.Add("display", "none")
                            End If

                            'MessageBox.ShowMessage(Page, MessageType.Success, "Saved Arrival Details")
                        End If
                        'End If
                    End If
                End If
            End If


        End If




    End Sub
    Private Sub BindFlightdatalist(ByVal requestid As String)

        Dim strName As String = ""
        Dim iRowNo As Integer = 0
        dvFlightDetailsHeading.Visible = True
        Dim dtDynamicFlightDetails = New DataTable()
        Dim dcRowNo = New DataColumn("RowNo", GetType(String))
        Dim dcName = New DataColumn("Name", GetType(String))
        dtDynamicFlightDetails.Columns.Add(dcRowNo)
        dtDynamicFlightDetails.Columns.Add(dcName)
        Dim chkallflight As Integer
        Dim strQuery As String = ""
        strQuery = "select guestlineno from view_booking_guest_flightstemp where requestid='" & CType(requestid, String) & "'"
        chkallflight = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        If chkallflight = 0 Then
            chkSameFlight.Checked = True
        Else
            chkSameFlight.Checked = False
        End If

        If chkSameFlight.Checked = False Then


            For Each item As DataListItem In dlPersonalInfo.Items
                Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
                Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
                Dim txtMiddleName As TextBox = CType(item.FindControl("txtMiddleName"), TextBox)
                Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)


                strName = ddlTittle.Text & " " & txtFirstName.Text & " " & txtMiddleName.Text & " " & txtLastName.Text
                iRowNo = iRowNo + 1

                Dim row As DataRow = dtDynamicFlightDetails.NewRow()
                row("RowNo") = (iRowNo).ToString
                row("Name") = "Passenger " & iRowNo & ": " & strName

                dtDynamicFlightDetails.Rows.Add(row)

            Next
        Else
            Dim row As DataRow = dtDynamicFlightDetails.NewRow()
            row("RowNo") = (1).ToString


            dtDynamicFlightDetails.Rows.Add(row)
        End If

        dlFlightDetails.DataSource = dtDynamicFlightDetails
        dlFlightDetails.DataBind()

        For Each gvRow As DataListItem In dlFlightDetails.Items

            Dim ChkFlightNotRequired As CheckBox = CType(gvRow.FindControl("ChkFlightNotRequired"), CheckBox)
              If dlFlightDetails.Items.Count = 1 Then
                ChkFlightNotRequired.Attributes.Add("style", "display:none")

            End If
        Next
       

    End Sub
    Private Sub BindFlightDetails(ByVal Requestid As String)
        Dim ds As DataSet

        ds = objBLLHotelSearch.GetFlightdetails(Requestid)
        If ds.Tables(0).Rows.Count > 0 Then
            dlFlightDetails.Visible = True
            If ds.Tables(0).Rows.Count <> 1 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1

                    Dim gvRow As DataListItem = dlFlightDetails.Items(i)


                    Dim txtArrivalDate As TextBox = gvRow.FindControl("txtArrivalDate")
                    Dim txtArrivalflight As TextBox = gvRow.FindControl("txtArrivalflight")
                    Dim txtArrivalflightCode As TextBox = gvRow.FindControl("txtArrivalflightCode")
                    Dim txtArrivalTime As TextBox = gvRow.FindControl("txtArrivalTime")
                    Dim txtArrivalAirport As TextBox = gvRow.FindControl("txtArrivalAirport")
                    Dim txtDepartureDate As TextBox = gvRow.FindControl("txtDepartureDate")
                    Dim txtDepartureFlight As TextBox = gvRow.FindControl("txtDepartureFlight")
                    Dim txtDepartureFlightCode As TextBox = gvRow.FindControl("txtDepartureFlightCode")
                    Dim txtDepartureTime As TextBox = gvRow.FindControl("txtDepartureTime")
                    Dim txtDepartureAirport As TextBox = gvRow.FindControl("txtDepartureAirport")

                    txtArrivalDate.Text = CType(Format(CType(ds.Tables(0).Rows(i)("arrdate"), Date), "dd/MM/yyyy"), String)
                    txtArrivalflight.Text = ds.Tables(0).Rows(i)("arrflightcode")
                    txtArrivalflightCode.Text = ds.Tables(0).Rows(i)("arrflight_tranid")
                    txtArrivalTime.Text = ds.Tables(0).Rows(i)("arrflighttime")
                    txtArrivalAirport.Text = ds.Tables(0).Rows(i)("arrairportbordername")

                    txtDepartureDate.Text = CType(Format(CType(ds.Tables(0).Rows(i)("depdate"), Date), "dd/MM/yyyy"), String)
                    txtDepartureFlight.Text = ds.Tables(0).Rows(i)("depflightcode")
                    txtDepartureFlightCode.Text = ds.Tables(0).Rows(i)("depflight_tranid")
                    txtDepartureTime.Text = ds.Tables(0).Rows(i)("flighttime")
                    txtDepartureAirport.Text = ds.Tables(0).Rows(i)("depairportbordername")

                Next

            Else

                For i = 0 To ds.Tables(0).Rows.Count - 1

                    ' Dim gvRow As DataListItem = dlFlightDetails.Items(i)
                    For Each gvrow As DataListItem In dlFlightDetails.Items

                        Dim txtArrivalDate As TextBox = gvrow.FindControl("txtArrivalDate")
                        Dim txtArrivalflight As TextBox = gvrow.FindControl("txtArrivalflight")
                        Dim txtArrivalflightCode As TextBox = gvrow.FindControl("txtArrivalflightCode")
                        Dim txtArrivalTime As TextBox = gvrow.FindControl("txtArrivalTime")
                        Dim txtArrivalAirport As TextBox = gvrow.FindControl("txtArrivalAirport")
                        Dim txtDepartureDate As TextBox = gvrow.FindControl("txtDepartureDate")
                        Dim txtDepartureFlight As TextBox = gvrow.FindControl("txtDepartureFlight")
                        Dim txtDepartureFlightCode As TextBox = gvrow.FindControl("txtDepartureFlightCode")
                        Dim txtDepartureTime As TextBox = gvrow.FindControl("txtDepartureTime")
                        Dim txtDepartureAirport As TextBox = gvrow.FindControl("txtDepartureAirport")

                        txtArrivalDate.Text = CType(Format(CType(ds.Tables(0).Rows(i)("arrdate"), Date), "dd/MM/yyyy"), String)
                        txtArrivalflight.Text = ds.Tables(0).Rows(i)("arrflightcode")
                        txtArrivalflightCode.Text = ds.Tables(0).Rows(i)("arrflight_tranid")
                        txtArrivalTime.Text = ds.Tables(0).Rows(i)("flighttime")
                        txtArrivalAirport.Text = ds.Tables(0).Rows(i)("arrairportbordername")
                        txtDepartureDate.Text = CType(Format(CType(ds.Tables(0).Rows(i)("depdate"), Date), "dd/MM/yyyy"), String)
                        txtDepartureFlight.Text = ds.Tables(0).Rows(i)("depflightcode")
                        txtDepartureFlightCode.Text = ds.Tables(0).Rows(i)("depflight_tranid")
                        txtDepartureTime.Text = ds.Tables(0).Rows(i)("depflighttime")
                        txtDepartureAirport.Text = ds.Tables(0).Rows(i)("depairportbordername")

                    Next
                Next
            End If

        End If

    End Sub
    Private Sub BindPersonalDetails(ByVal Requestid As String)
        Dim ds As DataSet


        ds = objBLLHotelSearch.GetPersonalDetails(Requestid)

        If ds.Tables(0).Rows.Count > 0 Then
            dlPersonalInfo.Visible = True

            For i = 0 To ds.Tables(0).Rows.Count - 1

                'For Each gvRow As DataListItem In dlPersonalInfo.Items
                Dim gvRow As DataListItem = dlPersonalInfo.Items(i)

                Dim ddlTittle As DropDownList = gvRow.FindControl("ddlTittle")
                Dim txtFirstName As TextBox = gvRow.FindControl("txtFirstName")
                Dim txtMiddleName As TextBox = gvRow.FindControl("txtMiddleName")
                Dim txtLastName As TextBox = gvRow.FindControl("txtLastName")
                Dim txtNationalityCode As TextBox = gvRow.FindControl("txtNationalityCode")
                Dim txtNationality As TextBox = gvRow.FindControl("txtNationality")
                ''Dim txtPassportNo As TextBox = gvRow.FindControl("txtPassportNo")
                'Dim ddlVisa As DropDownList = gvRow.FindControl("ddlVisa")
                Dim txtChildAge As TextBox = gvRow.FindControl("txtChildAge")
                'Dim ddlVisatype As DropDownList = gvRow.FindControl("ddlVisatype")
                'Dim txtVisaPrice As TextBox = gvRow.FindControl("txtVisaPrice")




                ddlTittle.SelectedValue = ds.Tables(0).Rows(i)("title")
                txtFirstName.Text = ds.Tables(0).Rows(i)("firstname")
                txtMiddleName.Text = ds.Tables(0).Rows(i)("middlename")
                txtLastName.Text = ds.Tables(0).Rows(i)("lastname")
                txtNationalityCode.Text = ds.Tables(0).Rows(i)("NationalityCode")
                'txtPassportNo.Text = ds.Tables(0).Rows(i)("passportno")
                'ddlVisa.SelectedValue = ds.Tables(0).Rows(i)("visaoptions")
                txtChildAge.Text = ds.Tables(0).Rows(i)("childage")
                'ddlVisatype.Items(ddlVisatype.SelectedIndex).Text = ds.Tables(0).Rows(i)("visatypecode")
                'ddlVisatype.SelectedValue = ds.Tables(0).Rows(i)("visatypecode")
                'txtVisaPrice.Text = IIf(ds.Tables(0).Rows(i)("visaprice") = "0", "", ds.Tables(0).Rows(i)("visaprice"))


            Next


        End If
    End Sub
    Private Sub BindPersonalInfoDataList(ByVal strAdults As String, ByVal strChild As String, ByVal strChilds As String, ByVal Hotelname As String)


        Dim iTotalRows As Integer = Val(strAdults) + Val(strChild)
        Dim iRNo As Integer = 0
        Dim dtDynamicPersonalInfo = New DataTable()
        Dim dcRowNo = New DataColumn("RowNo", GetType(String))

        Dim RowNoAll = New DataColumn("RowNoAll", GetType(String))
        Dim dcType = New DataColumn("Type", GetType(String))
        Dim dcChildAge = New DataColumn("ChildAge", GetType(String))
        ' Dim Partyname = New DataColumn("Partyname", GetType(String))

        dtDynamicPersonalInfo.Columns.Add(dcRowNo)
        dtDynamicPersonalInfo.Columns.Add(RowNoAll)
        dtDynamicPersonalInfo.Columns.Add(dcType)
        dtDynamicPersonalInfo.Columns.Add(dcChildAge)
        'dtDynamicPersonalInfo.Columns.Add(Partyname)

        For i As Integer = 0 To strAdults - 1
            iRNo = iRNo + 1
            Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
            row("RowNo") = (i + 1).ToString
            row("RowNoAll") = iRNo.ToString
            row("Type") = "Adult"
            row("ChildAge") = ""
            '  row("Partyname") = Hotelname
            dtDynamicPersonalInfo.Rows.Add(row)
        Next
        Dim strChildArray As String() = strChilds.Split("|")
        For i As Integer = 0 To strChild - 1
            iRNo = iRNo + 1
            Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
            row("RowNo") = (i + 1).ToString
            row("RowNoAll") = iRNo.ToString
            row("Type") = "Child"
            row("ChildAge") = strChildArray(i)
            ' row("Partyname") = Hotelname
            dtDynamicPersonalInfo.Rows.Add(row)
        Next

        If dtDynamicPersonalInfo.Rows.Count > 0 Then
            dlPersonalInfo.DataSource = dtDynamicPersonalInfo
            dlPersonalInfo.DataBind()


            For Each gvRow As DataListItem In dlPersonalInfo.Items
                Dim chkservices As CheckBoxList = gvRow.FindControl("chkservices")

                If chkservices.Items.Count > 0 Then
                    For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                        chkitem.Selected = True
                    Next
                End If

            Next
        End If


    End Sub
    Private Function ValidateFillguest() As String


        Dim guestflag As Boolean = False
        For Each item As DataListItem In dlPersonalInfo.Items

            Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
            Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
            Dim lblshiftfrom As Label = CType(item.FindControl("lblshiftfrom"), Label)



            guesttocheck = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=5310")
            If lblshiftfrom.Text = 1 And txtFirstName.Text <> "" And (txtLastName.Text <> "" Or guesttocheck = "1") Then

                guestflag = True
                Exit For
            End If

        Next

        If guestflag = False Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest Shifting Room  ")

            Return False
            Exit Function
        End If


        Return True
    End Function
    Sub btnfillguest_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As New DataTable
            Dim dr As DataRow

            Session("ShftingGuest") = Nothing


            dt.Columns.Add("Title", GetType(String))
            dt.Columns.Add("Firstname", GetType(String))
            dt.Columns.Add("Middlename", GetType(String))
            dt.Columns.Add("Lastname", GetType(String))
            dt.Columns.Add("Fromhotel", GetType(String))
            dt.Columns.Add("Tohotel", GetType(String))
            dt.Columns.Add("PType", GetType(String))
            dt.Columns.Add("Nationalty", GetType(String))
            dt.Columns.Add("Nationaltycode", GetType(String))
            dt.Columns.Add("Roomno", GetType(String))
            dt.Columns.Add("rlineno", GetType(String))

            Dim btnfillguest As Button = CType(sender, Button)
            Dim dlItem As DataListItem = CType((btnfillguest).NamingContainer, DataListItem)

            If ValidateFillguest() Then

                For Each item As DataListItem In dlPersonalInfo.Items

                    Dim lblshiftfrom As Label = CType(item.FindControl("lblshiftfrom"), Label) 'DlItem is changed to Item 'changed by mohamed on 07/08/2018
                    Dim lblshiftto As Label = CType(item.FindControl("lblshiftto"), Label)

                    dr = dt.NewRow

                    Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
                    Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
                    Dim txtMiddleName As TextBox = CType(item.FindControl("txtMiddleName"), TextBox)
                    Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
                    Dim lblfromhotel As Label = CType(item.FindControl("lblfromhotel"), Label)
                    Dim lbltohotel As Label = CType(item.FindControl("lbltohotel"), Label)
                    Dim lblType As Label = CType(item.FindControl("lblPType"), Label)
                    Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
                    Dim txtNationalityCode As TextBox = CType(item.FindControl("txtNationalityCode"), TextBox)
                    Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
                    Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label) 'changed by mohamed on 06/08/2018

                    If lblshiftfrom.Text = 1 And lblshiftto.Text = 0 Then
                        dr("Title") = ddlTittle.SelectedValue
                        dr("Firstname") = txtFirstName.Text
                        dr("Middlename") = txtMiddleName.Text
                        dr("Lastname") = txtLastName.Text
                        dr("Fromhotel") = lblfromhotel.Text
                        dr("Tohotel") = lbltohotel.Text
                        dr("PType") = lblType.Text
                        dr("Nationalty") = txtNationality.Text
                        dr("Nationaltycode") = txtNationalityCode.Text
                        dr("Roomno") = lblroomno.Text
                        dr("rlineno") = lblrlineno.Text

                        dt.Rows.Add(dr)
                    End If

                    'changed by mohamed on 06/08/2018
                    If lblshiftfrom.Text = 0 Or (lblshiftfrom.Text = 1 And lblshiftto.Text = 1) Then
                        BindTittle(lblType.Text, ddlTittle)
                        ddlTittle.SelectedValue = "0"
                        txtFirstName.Text = ""
                        txtMiddleName.Text = ""
                        txtLastName.Text = ""
                    End If
                Next
                Session.Add("ShftingGuest", dt)
            End If

            If Not Session("ShftingGuest") Is Nothing Then

                dt = Session("ShftingGuest")
                Dim dvMain As DataView = New DataView(dt)
                Dim dsGdet As DataSet = objclsUtilities.GetDataFromDataset("execute sp_get_shifting_hotel_detail_guestpage '" & Session("sRequestId") & "'")
                Dim dvGDet As DataView = New DataView(dsGdet.Tables(1))
                Dim lDTRow As DataRow

                For Each item As DataListItem In dlPersonalInfo.Items

                    Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
                    Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
                    Dim txtMiddleName As TextBox = CType(item.FindControl("txtMiddleName"), TextBox)
                    Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
                    Dim lblfromhotel As Label = CType(item.FindControl("lblfromhotel"), Label)
                    Dim lbltohotel As Label = CType(item.FindControl("lbltohotel"), Label)
                    Dim lblshiftfrom As Label = CType(item.FindControl("lblshiftfrom"), Label)
                    Dim lblshiftto As Label = CType(item.FindControl("lblshiftto"), Label)
                    Dim lblType As Label = CType(item.FindControl("lblPType"), Label)
                    Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
                    Dim txtNationalityCode As TextBox = CType(item.FindControl("txtNationalityCode"), TextBox)
                    Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
                    Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label)
                    Dim lblRowNo As Label = CType(item.FindControl("lblRowNo"), Label)
                    Dim lsNewPType As String = ""
                    If lblshiftto.Text = 1 Then
                        lDTRow = Nothing
                        dvGDet.RowFilter = "rlineno=" & lblrlineno.Text & " and roomno=" & lblroomno.Text & " and PType='" & lblType.Text & "'" ' and taken=0"
                        If dvGDet.ToTable.Rows.Count > 0 Then

                            dvMain.RowFilter = "rlineno=" & dvGDet.ToTable.Rows(0)("FromRlineno") & " and roomno=" & dvGDet.ToTable.Rows(0)("FromRoomNo") & " and PType='" & dvGDet.ToTable.Rows(0)("PTypeFrom") & "'"
                            If dvMain.ToTable.Rows.Count > 0 Then
                                lDTRow = dvMain.ToTable.Rows(0)
                            End If

                            'dvGDet.RowFilter = ""
                            'For Each lRow As DataRow In dsGdet.Tables(1).Rows
                            '    If lRow("rlineno") = lblrlineno.Text And lRow("roomno") = lblroomno.Text And lRow("PType") = lblType.Text And lRow("taken") = 0 Then
                            '        lRow.BeginEdit()
                            '        lRow("taken") = 1
                            '        lRow.EndEdit()
                            '        lRow.AcceptChanges()
                            '        dvGDet = New DataView(dsGdet.Tables(1))
                            '    End If
                            'Next
                        End If


                        If lDTRow IsNot Nothing Then
                            txtFirstName.Text = lDTRow("Firstname").ToString
                            txtMiddleName.Text = lDTRow("Middlename").ToString
                            txtLastName.Text = lDTRow("Lastname").ToString
                            txtNationality.Text = lDTRow("Nationalty").ToString
                            txtNationalityCode.Text = lDTRow("Nationaltycode").ToString
                            BindTittle(lblType.Text, ddlTittle)
                            ddlTittle.SelectedValue = lDTRow("Title").ToString

                            If lblshiftfrom.Text = 1 And lblshiftto.Text = 1 Then
                                dr = dt.NewRow

                                dr("Title") = ddlTittle.SelectedValue
                                dr("Firstname") = txtFirstName.Text
                                dr("Middlename") = txtMiddleName.Text
                                dr("Lastname") = txtLastName.Text
                                dr("Fromhotel") = lblfromhotel.Text
                                dr("Tohotel") = lbltohotel.Text
                                dr("PType") = lblType.Text
                                dr("Nationalty") = txtNationality.Text
                                dr("Nationaltycode") = txtNationalityCode.Text
                                dr("Roomno") = lblroomno.Text
                                dr("rlineno") = lblrlineno.Text

                                dt.Rows.Add(dr)
                                Session("ShftingGuest") = dt
                            End If
                        End If
                        dvMain.RowFilter = ""
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: btnfillguest_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub dlPersonalInfo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlPersonalInfo.ItemDataBound


        'guesttocheck = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=5310")

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '    If e.Item.ItemIndex = 0 Then
            '        roomno = 0
            '        rlineno = 0
            '    End If

            '    Dim dtRow As DataRow = DataListItemToDataRow(e.Item)


            Dim lblType As Label = CType(e.Item.FindControl("lblPType"), Label)
            Dim ddlTittle As DropDownList = CType(e.Item.FindControl("ddlTittle"), DropDownList)

            '    Dim dvChildAge As HtmlGenericControl = CType(e.Item.FindControl("dvChildAge"), HtmlGenericControl)
            '    Dim txtNationality As TextBox = CType(e.Item.FindControl("txtNationality"), TextBox)
            '    Dim txtNationalityCode As TextBox = CType(e.Item.FindControl("txtNationalityCode"), TextBox)
            '    Dim txtFirstName As TextBox = CType(e.Item.FindControl("txtFirstName"), TextBox)
            '    Dim txtMiddleName As TextBox = CType(e.Item.FindControl("txtMiddleName"), TextBox)
            '    Dim txtLastName As TextBox = CType(e.Item.FindControl("txtLastName"), TextBox)

            '    Dim lblRowNo As Label = CType(e.Item.FindControl("lblRowNo"), Label)
            '    Dim txtChildAge As TextBox = CType(e.Item.FindControl("txtChildAge"), TextBox)
            '    Dim lblRowNoAll As Label = CType(e.Item.FindControl("lblRowNoAll"), Label)
            '    Dim lblRowHeading As Label = CType(e.Item.FindControl("lblRowHeading"), Label)

            '    Dim lblroomno As Label = CType(e.Item.FindControl("lblroomno"), Label)
            '    Dim lblroomtext As Label = CType(e.Item.FindControl("lblroomtext"), Label)
            '    Dim dvroomhead As HtmlGenericControl = CType(e.Item.FindControl("divroomhead"), HtmlGenericControl)
            '    Dim lblrlineno As Label = CType(e.Item.FindControl("lblrlineno"), Label)

            '    Dim lblFirstName As HtmlGenericControl = CType(e.Item.FindControl("lblFirstName"), HtmlGenericControl)
            '    Dim lbltitle As HtmlGenericControl = CType(e.Item.FindControl("lbltitle"), HtmlGenericControl)
            '    Dim lblLastname As HtmlGenericControl = CType(e.Item.FindControl("lblLastname"), HtmlGenericControl)
            '    Dim lblnationality As HtmlGenericControl = CType(e.Item.FindControl("lblnationality"), HtmlGenericControl)

            '    Dim lnkshow As LinkButton = CType(e.Item.FindControl("lnkshow"), LinkButton)

            '    Dim imgSclose As ImageButton = CType(e.Item.FindControl("imgSclose"), ImageButton)

            '    'Dim lblshiftfrom As Label = CType(e.Item.FindControl("lblshiftfrom"), Label)
            '    'Dim lblshiftto As Label = CType(e.Item.FindControl("lblshiftto"), Label)

            '    'Dim lblfromhotel As Label = CType(e.Item.FindControl("lblfromhotel"), Label)
            '    'Dim lbltohotel As Label = CType(e.Item.FindControl("lbltohotel"), Label)

            '    Dim btnfillguest As Button = CType(e.Item.FindControl("btnfillguest"), Button)

            '    '' Added shahul 04/08/8
            '    Dim dvmiddlename As HtmlGenericControl = CType(e.Item.FindControl("dvmiddlename"), HtmlGenericControl)
            '    Dim dvlastname As HtmlGenericControl = CType(e.Item.FindControl("dvlastname"), HtmlGenericControl)
            '    If guesttocheck = "1" Then
            '        dvmiddlename.Style.Add("display", "none")
            '        dvlastname.Style.Add("display", "none")
            '    Else
            '        dvmiddlename.Style.Add("display", "block")
            '        dvlastname.Style.Add("display", "block")
            '    End If

            '    'txtFirstName.Style.Add("width", "35% !important")
            '    'txtFirstName.Width = 200

            '    txtFirstName.Text = IIf(Convert.ToString(dtRow("Firstname")) = "", "", dtRow("Firstname"))
            '    txtMiddleName.Text = IIf(Convert.ToString(dtRow("Middlename")) = "", "", dtRow("Middlename"))
            '    txtLastName.Text = IIf(Convert.ToString(dtRow("Lastname")) = "", "", dtRow("Lastname"))
            '    txtChildAge.Text = IIf(Convert.ToString(dtRow("ChildAge")) = "", "", dtRow("ChildAge"))
            '    lblRowNoAll.Text = IIf(Convert.ToString(dtRow("RowNoAll")) = "", "", dtRow("RowNoAll"))
            '    lblRowHeading.Text = IIf(Convert.ToString(dtRow("Type")) = "", "", dtRow("Type"))

            '    lblrlineno.Text = IIf(Convert.ToString(dtRow("Rlineno")) = "", "", dtRow("Rlineno"))

            '    'lblshiftfrom.Text = IIf(Convert.ToString(dtRow("shiftfrom")) = "", "", dtRow("shiftfrom"))
            '    'lblshiftto.Text = IIf(Convert.ToString(dtRow("shiftto")) = "", "", dtRow("shiftto"))

            '    'lblfromhotel.Text = IIf(Convert.ToString(dtRow("fromhotel")) = "", "", dtRow("fromhotel"))
            '    'lbltohotel.Text = IIf(Convert.ToString(dtRow("tohotel")) = "", "", dtRow("tohotel"))

            '    lblRowNo.Text = IIf(Convert.ToString(dtRow("RowNo")) = "", "", dtRow("RowNo"))
            '    lblroomno.Text = IIf(Convert.ToString(dtRow("Roomno")) = "", "", dtRow("Roomno"))
            '    lblroomtext.Text = IIf(Convert.ToString(dtRow("Partyname")) = "", "", dtRow("Partyname"))

            '    txtNationality.Text = IIf(Convert.ToString(dtRow("Nationalty")) = "", "", dtRow("Nationalty"))
            '    txtNationalityCode.Text = IIf(Convert.ToString(dtRow("Nationaltycode")) = "", "", dtRow("Nationaltycode"))


            '    If lblRowHeading.Text.ToUpper.Contains("ADDITIONAL") = False Then
            '        imgSclose.Style.Add("display", "none")
            '    End If

            '    If Val(lblshiftfrom.Text) = 1 And e.Item.ItemIndex = 0 And (roomno <> Val(lblroomno.Text) Or rlineno <> Val(lblrlineno.Text)) Then
            '        btnfillguest.Style.Add("display", "block")

            '    Else
            '        btnfillguest.Style.Add("display", "none")
            '    End If




            '    If roomno <> Val(lblroomno.Text) Or rlineno <> Val(lblrlineno.Text) Then
            '        dvroomhead.Style.Add("display", "block")
            '    Else
            '        If lblroomtext.Text <> "" And e.Item.ItemIndex > 0 Then
            '            lblFirstName.Attributes("class") = ""
            '            lblnationality.Attributes("class") = ""
            '            lblLastname.Attributes("class") = ""
            '            lbltitle.Attributes("class") = ""

            '        ElseIf lblroomtext.Text = "" And e.Item.ItemIndex > 0 Then
            '            lblFirstName.Attributes("class") = ""
            '            lblnationality.Attributes("class") = ""
            '            lblLastname.Attributes("class") = ""
            '            lbltitle.Attributes("class") = ""
            '        End If
            '        dvroomhead.Style.Add("display", "none")
            '    End If

            '    roomno = Val(lblroomno.Text)
            '    rlineno = Val(lblrlineno.Text)
            '    Dim chkservices As CheckBoxList = CType(e.Item.FindControl("chkservices"), CheckBoxList)


            '    Dim dt1 As DataTable = objBLLHotelSearch.GetResultAsDataTable("select case when (isnull(shiftfrom,0)=0  and isnull(shiftto,0)=0) or ( isnull(shiftfrom,0)=1  and isnull(shiftto,0)=0) then convert(varchar(10),dateadd(d,-1,checkin),111) when  isnull(shiftto,0)=1 then convert(varchar(10),dateadd(d,1,checkin),111) else convert(varchar(10),checkin,111) end  checkin,case when isnull(shiftfrom,0)=0  and isnull(shiftto,0)=0 then   convert(varchar(10),dateadd(d,1,checkout),111) when isnull(shiftfrom,0)=0  and isnull(shiftto,0)=1 then   convert(varchar(10),dateadd(d,1,checkout),111) else convert(varchar(10),checkout,111) end checkout from view_booking_hotel_prearr(nolock)  where requestid='" & Session("sRequestId") & "'  and  rlineno=" & Val(lblrlineno.Text))
            '    If dt1.Rows.Count > 0 Then
            '        showservicenew(Session("sRequestId"), dt1.Rows(0)("checkin").ToString, dt1.Rows(0)("checkout").ToString)
            '    Else
            '        showservice()
            '    End If

            '    If Not Session("showservices") Is Nothing Then

            '        'changed by mohamed on 28/08/2018 as to select needed
            '        Dim lsSICType As String = "Adult"
            '        If lblType.Text.Contains("Child") Then
            '            lsSICType = "Child"
            '        End If
            '        Dim dtServ As DataTable
            '        dtServ = Session("showservices")
            '        Dim dvServ As New DataView(dtServ)
            '        dvServ.RowFilter = "SICTransfer=0 or (SICTransfer=1 and SICAdultOrChild='" & lsSICType & "')"

            '        chkservices.DataSource = dvServ.ToTable
            '        chkservices.DataTextField = "servicetype"
            '        chkservices.DataValueField = "servicelineno"
            '        chkservices.DataBind()
            '        lnkshow.Style.Add("display", "block")
            '    Else
            '        lnkshow.Style.Add("display", "none")
            '    End If



            '    If lblType.Text.Contains("Child") = True Then

            '        dvChildAge.Style.Add("display", "block")
            '        txtChildAge.ReadOnly = False
            '    Else

            '        dvChildAge.Style.Add("display", "none")
            '    End If



            '    Dim divservices As HtmlGenericControl = CType(e.Item.FindControl("divservices"), HtmlGenericControl)
            '    lnkshow.Attributes.Add("onClick", "javascript:return showhideservices('" + CType(lnkshow.ClientID, String) + "','" + CType(divservices.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "' )")



            '    If divFlightdetail.Visible = True Then
            '        txtFirstName.Attributes.Add("onChange", "javascript:Guestchange('" + CType(txtFirstName.ClientID, String) + "','" + CType(txtMiddleName.ClientID, String) + "','" + CType(txtLastName.ClientID, String) + "','" + CType(ddlTittle.ClientID, String) + "','" + CType(txtNationality.ClientID, String) + "','" + CType(divFlightdetail.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "')")
            '        txtLastName.Attributes.Add("onChange", "javascript:Guestchange('" + CType(txtFirstName.ClientID, String) + "','" + CType(txtMiddleName.ClientID, String) + "','" + CType(txtLastName.ClientID, String) + "','" + CType(ddlTittle.ClientID, String) + "','" + CType(txtNationality.ClientID, String) + "','" + CType(divFlightdetail.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "')")
            '        txtNationality.Attributes.Add("onChange", "javascript:Guestchange('" + CType(txtFirstName.ClientID, String) + "','" + CType(txtMiddleName.ClientID, String) + "','" + CType(txtLastName.ClientID, String) + "','" + CType(ddlTittle.ClientID, String) + "','" + CType(txtNationality.ClientID, String) + "','" + CType(divFlightdetail.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "')")
            '    End If

            BindTittle(lblType.Text, ddlTittle)
            '    ddlTittle.Text = IIf(Convert.ToString(dtRow("Title")) = "", "", dtRow("Title"))


            '    Dim dt As DataTable
            '    dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestid"))
            '    If dt.Rows.Count > 0 Then
            '        txtNationality.Text = dt.Rows(0)("sourcectryname").ToString
            '        txtNationalityCode.Text = dt.Rows(0)("sourcectrycode").ToString
            '    End If



        End If


    End Sub

    Private Sub BindTittle(ByVal strType As String, ByVal ddlTittle As DropDownList)
        If strType.Contains("Adult") = True Then
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("--", "0"))
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("Mr", "Mr"))
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("Mrs", "Mrs"))
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("Ms", "Ms"))
        Else
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("--", "0"))
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("Child Male", "Child Male"))
            ddlTittle.Items.Add(New System.Web.UI.WebControls.ListItem("Child Female", "Child Female"))
        End If
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
       <System.Web.Services.WebMethod()> _
    Public Shared Function SerMeth_GetFirstName(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim lGuestList As New List(Of String)
        Dim lAvailGuestList As New List(Of String)
        Try
            Dim lsArr() = contextKey.Split(",")
            For i = 0 To lsArr.GetUpperBound(0)
                If lAvailGuestList.Contains(lsArr(i).ToString.ToUpper()) = False And (lsArr(i).ToString.ToUpper().Contains(prefixText.Trim.ToUpper()) = True Or prefixText.Trim = "") Then
                    lAvailGuestList.Add(lsArr(i).ToString.ToUpper())
                    lGuestList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(lsArr(i).ToString.ToUpper(), lsArr(i).ToString.ToUpper()))
                End If
            Next

            Return lGuestList
        Catch ex As Exception
            Return lGuestList
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
       <System.Web.Services.WebMethod()> _
    Public Shared Function GetNationality(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Nationality As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select ctrycode,ctryname from ctrymast where active=1 and ctryname like  '%" & prefixText & "%' "
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

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetArrivalflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Arrivalflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            Dim objBLLTransferSearch = New BLLTransferSearch
            If Not HttpContext.Current.Session("sobjBLLTransferSearchActive") Is Nothing Then
                objBLLTransferSearch = HttpContext.Current.Session("sobjBLLTransferSearchActive")
                Dim dt As DataTable

                dt = objBLLTransferSearch.GetAirportTerminal(objBLLTransferSearch.OBRequestId)
                If dt.Rows.Count > 0 Then

                    strSqlQry = "select v.flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,v.flightcode from view_flightmast_arrival v, booking_transferstemp t  where  " _
                        & " v.airportbordercode=t.airportbordercode and t.requestid='" & objBLLTransferSearch.OBRequestId & "' and  convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) and t.transfertype='ARRIVAL' and v.flightcode like  '" & prefixText & "%' "
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "
                End If
            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "
            End If



            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Arrivalflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                Next

            End If
            Return Arrivalflight
        Catch ex As Exception
            Return Arrivalflight
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetDepartureflight(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Departureflight As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            Dim objBLLTransferSearch = New BLLTransferSearch
            If Not HttpContext.Current.Session("sobjBLLTransferSearchActive") Is Nothing Then
                objBLLTransferSearch = HttpContext.Current.Session("sobjBLLTransferSearchActive")
                Dim dt As DataTable

                dt = objBLLTransferSearch.GetAirportTerminal(objBLLTransferSearch.OBRequestId)
                If dt.Rows.Count > 0 Then

                    strSqlQry = "select v.flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,v.flightcode from view_flightmast_departure v ,  " _
                        & " booking_transferstemp t  where v.airportbordercode=t.airportbordercode and t.requestid='" & objBLLTransferSearch.OBRequestId & "' and  convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) and t.transfertype='DEPARTURE'  and v.flightcode like  '" & prefixText & "%' "
                Else
                    strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "

                End If
            Else
                strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "
            End If

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Departureflight.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("flightcode").ToString(), myDS.Tables(0).Rows(i)("flight_tranid").ToString()))
                Next

            End If
            Return Departureflight
        Catch ex As Exception
            Return Departureflight
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function GetAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername,airportbordercode,airport  from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Shared Function GetDepartureAirportAndTimeDetails(ByVal flightcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet

        Try
            Dim strFlightCode As String
            Dim strDayName As String
            If flightcode <> "" Then
                Dim strFlightCodes As String() = flightcode.Split("|")
                strFlightCode = strFlightCodes(0)
                strDayName = strFlightCodes(1)
            End If

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername,airportbordercode,airport  from view_flightmast_departure where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Customers")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function SelectVisa(ByVal strNationality As String) As String
        Dim strVisaOnArival As String = ""
        strVisaOnArival = clsUtilities.SharedExecuteQueryReturnStringValue("select count(CtryCode)cnt from VisaOnArrivalCountries where  CHARINDEX('" & strNationality & "',CtryCode)>0  ")
        If strVisaOnArival = "" Then
            strVisaOnArival = "0"
        End If
        Return strVisaOnArival
    End Function

    Private Sub BindVisaType(ByVal strCheckIn As String, ByVal ddlVisatype As DropDownList)
        Dim strQuery As String = ""
        If strCheckIn <> "" Then
            Dim strDates As String() = strCheckIn.Split("/")
            strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        strQuery = "select distinct Visacode,visaname from view_visa_types where '" & strCheckIn & "' between fromdate and todate"
        objclsUtilities.FillDropDownList(ddlVisatype, strQuery, "Visacode", "visaname", True, "--")
    End Sub

    Protected Sub ddlVisatype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlVisaType As DropDownList = CType(sender, DropDownList)
        Dim str As String = ddlVisaType.SelectedValue
        Dim dlItem As DataListItem = CType(ddlVisaType.NamingContainer, DataListItem)
        Dim txtVisaPrice As TextBox = CType(dlItem.FindControl("txtVisaPrice"), TextBox)
        Dim txtNationalityCode As TextBox = CType(dlItem.FindControl("txtNationalityCode"), TextBox)



        Dim objBLLHotelSearch = New BLLHotelSearch

        If ddlVisaType.Text = "--" Then
            txtVisaPrice.Text = ""
        Else
            ''" & objBLLHotelSearch.SourceCountryCode & "'

            txtVisaPrice.Text = GetVisaPrice(ddlVisaType.SelectedValue, txtNationalityCode.Text)

            For Each item As DataListItem In dlPersonalInfo.Items
                Dim ddlVisaTypeNew As DropDownList = CType(item.FindControl("ddlVisaType"), DropDownList)
                If ddlVisaTypeNew.Text = "--" Then
                    ddlVisaTypeNew.SelectedValue = ddlVisaType.SelectedValue
                    Dim txtVisaPriceNew As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
                    txtVisaPriceNew.Text = txtVisaPrice.Text
                End If
            Next

        End If



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
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: btnLogOut_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Protected Sub imgSclose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            '  Dim row As GridViewRow = CType((CType(sender, ImageButton)).NamingContainer, GridViewRow)
            Dim row As ImageButton = CType(sender, ImageButton)
            Dim dlItem As DataListItem = CType((row).NamingContainer, DataListItem)

            GenerateGridColumns("DELETE", dlItem.ItemIndex)
            divFlightdetail.Style.Add("display", "none")
            For Each item As DataListItem In dlPersonalInfo.Items
                Dim chkservices As CheckBoxList = CType(item.FindControl("chkservices"), CheckBoxList)

                For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                    chkitem.Selected = True
                Next

            Next

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Guestpagenew.aspx :: btnLogOut_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Sub GenerateGridColumns(ByVal lsMode As String, ByVal liRowIndex As Integer)

        Dim dt As New DataTable
        Dim dr As DataRow



        dt.Columns.Add(New DataColumn("Title", GetType(String)))
        dt.Columns.Add(New DataColumn("Firstname", GetType(String)))
        dt.Columns.Add(New DataColumn("Middlename", GetType(String)))
        dt.Columns.Add(New DataColumn("Lastname", GetType(String)))
        dt.Columns.Add(New DataColumn("Nationalty", GetType(String)))
        dt.Columns.Add(New DataColumn("Nationaltycode", GetType(String)))
        dt.Columns.Add(New DataColumn("ChildAge", GetType(String)))
        dt.Columns.Add(New DataColumn("Type", GetType(String)))
        dt.Columns.Add(New DataColumn("RowNo", GetType(String)))
        dt.Columns.Add(New DataColumn("RowNoAll", GetType(String)))
        dt.Columns.Add(New DataColumn("Partyname", GetType(String)))
        dt.Columns.Add(New DataColumn("Roomno", GetType(String)))
        dt.Columns.Add(New DataColumn("Rlineno", GetType(String)))
        dt.Columns.Add(New DataColumn("ShiftFrom", GetType(String)))
        dt.Columns.Add(New DataColumn("ShiftTo", GetType(String)))
        dt.Columns.Add(New DataColumn("FromHotel", GetType(String)))
        dt.Columns.Add(New DataColumn("ToHotel", GetType(String)))
        dt.Columns.Add(New DataColumn("GuestLineNo", GetType(String)))

        Dim adultcount As Integer = 0

        For Each gvRow As DataListItem In dlPersonalInfo.Items






            Dim ddlTittle As DropDownList = gvRow.FindControl("ddlTittle")
            Dim txtFirstName As TextBox = gvRow.FindControl("txtFirstName")
            Dim txtMiddleName As TextBox = gvRow.FindControl("txtMiddleName")
            Dim txtLastName As TextBox = gvRow.FindControl("txtLastName")
            Dim txtNationality As TextBox = gvRow.FindControl("txtNationality")
            Dim txtNationalityCode As TextBox = gvRow.FindControl("txtNationalityCode")
            Dim txtChildAge As TextBox = gvRow.FindControl("txtChildAge")
            Dim lblRowHeading As Label = gvRow.FindControl("lblRowHeading")
            Dim lblRowNoAll As Label = gvRow.FindControl("lblRowNoAll")
            Dim lblRowNo As Label = gvRow.FindControl("lblRowNo")
            Dim lblroomtext As Label = gvRow.FindControl("lblroomtext")
            Dim lblroomno As Label = gvRow.FindControl("lblroomno")
            Dim lblrlineno As Label = gvRow.FindControl("lblrlineno")
            Dim lblshiftfrom As Label = gvRow.FindControl("lblshiftfrom")
            Dim lblshiftto As Label = gvRow.FindControl("lblshiftto")
            Dim lblfromhotel As Label = gvRow.FindControl("lblfromhotel")
            Dim lbltohotel As Label = gvRow.FindControl("lbltohotel")
            Dim lblGuestLineNo As Label = gvRow.FindControl("lblGuestLineNo")

            dr = dt.NewRow

            dr("Title") = ddlTittle.Text
            dr("Firstname") = txtFirstName.Text
            dr("Middlename") = txtMiddleName.Text
            dr("Lastname") = txtLastName.Text
            dr("Nationalty") = txtNationality.Text
            dr("Nationaltycode") = txtNationalityCode.Text
            dr("ChildAge") = txtChildAge.Text
            dr("Type") = lblRowHeading.Text 'Left(lblRowHeading.Text, (Len(lblRowHeading.Text) - 1))
            dr("RowNo") = lblRowNo.Text
            dr("RowNoAll") = lblRowNoAll.Text
            dr("Partyname") = lblroomtext.Text
            dr("Roomno") = Val(lblroomno.Text)
            dr("Rlineno") = Val(lblrlineno.Text)
            dr("ShiftFrom") = Val(lblshiftfrom.Text)
            dr("ShiftTo") = Val(lblshiftto.Text)
            dr("FromHotel") = lblfromhotel.Text
            dr("ToHotel") = lbltohotel.Text
            dr("GuestLineNo") = lblGuestLineNo.Text
            If lblRowHeading.Text.Contains("Adult") = True Then
                adultcount = adultcount + 1
            End If

            dt.Rows.Add(dr)

        Next

        If lsMode.Trim.ToUpper = "DELETE" Then
            If (dt.Rows.Count > liRowIndex) Then
                dt.Rows(liRowIndex).Delete()
            End If
        End If

        dlPersonalInfo.DataSource = dt
        dlPersonalInfo.DataBind()


    End Sub
    Private Sub ValidateGuestService(ByVal intAuto As Integer) '*** Danny 01/09/2018
        Dim dt As DataTable

        If ValidatePersonalDetails() Then


            Dim strBuffer As New Text.StringBuilder
            Dim strFliBuffer As New Text.StringBuilder
            Dim strServiceBuffer As New Text.StringBuilder
            ''If Session("sAgentCompany") Is  Nothing Or Session("GlobalUserName") Is Nothing Then
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))




            Dim ddlTitle As DropDownList
            Dim lblRowNoAll As Label
            Dim lblrlineno As Label
            Dim txtFirstNameg As TextBox
            Dim txtMidName As TextBox
            Dim txtLastNameg As TextBox
            Dim txtNationalityCode As TextBox
            Dim txtchildage As TextBox
            Dim ddlVisa As DropDownList
            Dim ddlVisatype As DropDownList
            Dim txtVisaPrice As TextBox
            Dim txtPassportNo As TextBox

            Dim lblGuestLineNo As Label

            Dim i As Integer = 1
            If dlPersonalInfo.Items.Count > 0 Then

                If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
                    Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
                    objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
                    objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
                    objBLLguest.GBRlineno = objBLLHotelSearch.OBrlineno
                Else
                    objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                    objBLLguest.GBRlineno = "0"
                End If
                ' 

                For Each item As DataListItem In dlPersonalInfo.Items
                    Dim strTittle As String = ""
                    lblrlineno = CType(item.FindControl("lblrlineno"), Label)

                    lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
                    ddlTitle = CType(item.FindControl("ddlTittle"), DropDownList)
                    txtFirstNameg = CType(item.FindControl("txtFirstName"), TextBox)
                    txtMidName = CType(item.FindControl("txtMiddleName"), TextBox)
                    txtLastNameg = CType(item.FindControl("txtlastName"), TextBox)
                    txtchildage = CType(item.FindControl("txtchildage"), TextBox)
                    ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
                    ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
                    txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
                    txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
                    txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)
                    lblGuestLineNo = CType(item.FindControl("lblGuestLineNo"), Label)

                    Dim lblType As Label = CType(item.FindControl("lblPType"), Label)

                    Dim lblRowHeading = CType(item.FindControl("lblRowHeading"), Label)
                    Dim lblroomno = CType(item.FindControl("lblroomno"), Label)

                    strBuffer.Append("<DocumentElement>")

                    ' strBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
                    strBuffer.Append(" <guestlineno>" & lblGuestLineNo.Text & "</guestlineno>")
                    strBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                    'strBuffer.Append(" <guestlineno>" & i & "</guestlineno>")
                    If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                        strBuffer.Append(" <title>Mr</title>")
                    ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                        strBuffer.Append(" <title>Child Male</title>")
                    Else
                        strBuffer.Append(" <title>" & ddlTitle.Text & "</title>")
                    End If

                    strBuffer.Append(" <firstname>" & IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text.ToUpper) & "</firstname>")
                    strBuffer.Append(" <middlename>" & txtMidName.Text.ToUpper & "</middlename>")
                    strBuffer.Append(" <lastname>" & txtLastNameg.Text.ToUpper & "</lastname>")
                    strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
                    If lblType.Text.Contains("Child") = True Then  ''' Added shahul 30/07/18
                        strBuffer.Append(" <childage>" & CType(Val(txtchildage.Text), Decimal) & "</childage>")
                    Else
                        strBuffer.Append(" <childage>0</childage>")
                    End If
                    strBuffer.Append(" <visaoptions></visaoptions>")
                    strBuffer.Append(" <visatypecode></visatypecode>")
                    strBuffer.Append(" <visaprice>0</visaprice>")
                    strBuffer.Append(" <passportno></passportno>")
                    strBuffer.Append(" <roomno>" & lblroomno.Text & "</roomno>")
                    If lblType.Text.Contains("Adult") = True Then
                        strBuffer.Append(" <guesttype>Adult</guesttype>")
                    Else
                        strBuffer.Append(" <guesttype>Child</guesttype>")
                    End If

                    strBuffer.Append("</DocumentElement>")

                    Dim chkservices As CheckBoxList = CType(item.FindControl("chkservices"), CheckBoxList)
                    Dim title As String = ""
                    Dim firstname As String = ""
                    Dim Lastname As String = ""
                    Dim middlename As String = "", Guestname As String = ""

                    If chkservices.Items.Count > 0 Then

                        If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                            title = "Mr"
                        ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                            title = "Child Male"
                        Else
                            title = ddlTitle.Text
                        End If
                        firstname = IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text)
                        middlename = txtMidName.Text
                        Lastname = txtLastNameg.Text
                        Guestname = title + " " + firstname + " " + middlename + " " + Lastname
                        Dim k As Integer = 1
                        For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                            If chkitem.Selected = True Then

                                Dim othtypcode As String() = chkitem.Value.Split(";")

                                strServiceBuffer.Append("<DocumentElement>")
                                strServiceBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                                strServiceBuffer.Append(" <guestlineno>" & lblGuestLineNo.Text & "</guestlineno>")
                                strServiceBuffer.Append(" <servicelineno>" & othtypcode(0) & "</servicelineno>")
                                strServiceBuffer.Append(" <guestname>" & Guestname.ToUpper & "</guestname>")
                                strServiceBuffer.Append(" <services>" & chkitem.Text & "</services>")
                                strServiceBuffer.Append(" <servicescode>" & othtypcode(1) & "</servicescode>")
                                strServiceBuffer.Append("</DocumentElement>")
                                k += 1
                            End If

                        Next
                    End If

                    '  i = i + 1
                Next
            End If
            objBLLguest.GBGuestXml = strBuffer.ToString.Replace("&", "and")
            objBLLguest.GBServiceXml = strServiceBuffer.ToString.Replace("&", "and")
            objBLLguest.GBuserlogged = Session("GlobalUserName")




            Dim strQuery As String = ""
            Dim transferDeppax As Integer = 0
            If objBLLguest.SavingGuestBookingInTemp() Then

                Dim dtGuestValidate As DataTable = objBLLguest.ValidateGuestService(Session("sRequestId"))
                gvValidateGuestService.DataSource = dtGuestValidate
                gvValidateGuestService.DataBind()
                mpValidateGuestService.Show()
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, " ")
            End If
            ' BindVisaSummary()
        End If
    End Sub

    Protected Sub gvValidateGuestService_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvValidateGuestService.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim lblGuestName As Label = CType(e.Row.FindControl("lblGuestName"), Label)
            lblGuestName.Text = lblGuestName.Text.Replace(",", ",<br />")
        End If


    End Sub


    Private Sub FlightBtnCall(ByVal intAuto As Integer) '*** Danny 01/09/2018
        Dim dt As DataTable

        If ValidatePersonalDetails() Then


            Dim strBuffer As New Text.StringBuilder
            Dim strFliBuffer As New Text.StringBuilder
            Dim strServiceBuffer As New Text.StringBuilder
            ''If Session("sAgentCompany") Is  Nothing Or Session("GlobalUserName") Is Nothing Then
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))




            Dim ddlTitle As DropDownList
            Dim lblRowNoAll As Label
            Dim lblrlineno As Label
            Dim txtFirstNameg As TextBox
            Dim txtMidName As TextBox
            Dim txtLastNameg As TextBox
            Dim txtNationalityCode As TextBox
            Dim txtchildage As TextBox
            Dim ddlVisa As DropDownList
            Dim ddlVisatype As DropDownList
            Dim txtVisaPrice As TextBox
            Dim txtPassportNo As TextBox

            Dim lblGuestLineNo As Label

            Dim i As Integer = 1
            If dlPersonalInfo.Items.Count > 0 Then

                If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
                    Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
                    objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
                    objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
                    objBLLguest.GBRlineno = objBLLHotelSearch.OBrlineno
                Else
                    objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                    objBLLguest.GBRlineno = "0"
                End If
                ' 

                For Each item As DataListItem In dlPersonalInfo.Items
                    Dim strTittle As String = ""
                    lblrlineno = CType(item.FindControl("lblrlineno"), Label)

                    lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
                    ddlTitle = CType(item.FindControl("ddlTittle"), DropDownList)
                    txtFirstNameg = CType(item.FindControl("txtFirstName"), TextBox)
                    txtMidName = CType(item.FindControl("txtMiddleName"), TextBox)
                    txtLastNameg = CType(item.FindControl("txtlastName"), TextBox)
                    txtchildage = CType(item.FindControl("txtchildage"), TextBox)
                    ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
                    ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
                    txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
                    txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
                    txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)
                    lblGuestLineNo = CType(item.FindControl("lblGuestLineNo"), Label)

                    Dim lblType As Label = CType(item.FindControl("lblPType"), Label)

                    Dim lblRowHeading = CType(item.FindControl("lblRowHeading"), Label)
                    Dim lblroomno = CType(item.FindControl("lblroomno"), Label)

                    strBuffer.Append("<DocumentElement>")

                    ' strBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
                    strBuffer.Append(" <guestlineno>" & lblGuestLineNo.Text & "</guestlineno>")
                    strBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                    'strBuffer.Append(" <guestlineno>" & i & "</guestlineno>")
                    If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                        strBuffer.Append(" <title>Mr</title>")
                    ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                        strBuffer.Append(" <title>Child Male</title>")
                    Else
                        strBuffer.Append(" <title>" & ddlTitle.Text & "</title>")
                    End If

                    strBuffer.Append(" <firstname>" & IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text.ToUpper) & "</firstname>")
                    strBuffer.Append(" <middlename>" & txtMidName.Text.ToUpper & "</middlename>")
                    strBuffer.Append(" <lastname>" & txtLastNameg.Text.ToUpper & "</lastname>")
                    strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
                    If lblType.Text.Contains("Child") = True Then  ''' Added shahul 30/07/18
                        strBuffer.Append(" <childage>" & CType(Val(txtchildage.Text), Decimal) & "</childage>")
                    Else
                        strBuffer.Append(" <childage>0</childage>")
                    End If
                    strBuffer.Append(" <visaoptions></visaoptions>")
                    strBuffer.Append(" <visatypecode></visatypecode>")
                    strBuffer.Append(" <visaprice>0</visaprice>")
                    strBuffer.Append(" <passportno></passportno>")
                    strBuffer.Append(" <roomno>" & lblroomno.Text & "</roomno>")
                    If lblType.Text.Contains("Adult") = True Then
                        strBuffer.Append(" <guesttype>Adult</guesttype>")
                    Else
                        strBuffer.Append(" <guesttype>Child</guesttype>")
                    End If

                    strBuffer.Append("</DocumentElement>")

                    Dim chkservices As CheckBoxList = CType(item.FindControl("chkservices"), CheckBoxList)
                    Dim title As String = ""
                    Dim firstname As String = ""
                    Dim Lastname As String = ""
                    Dim middlename As String = "", Guestname As String = ""

                    If chkservices.Items.Count > 0 Then

                        If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                            title = "Mr"
                        ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                            title = "Child Male"
                        Else
                            title = ddlTitle.Text
                        End If
                        firstname = IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text)
                        middlename = txtMidName.Text
                        Lastname = txtLastNameg.Text
                        Guestname = title + " " + firstname + " " + middlename + " " + Lastname
                        Dim k As Integer = 1
                        For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                            If chkitem.Selected = True Then

                                Dim othtypcode As String() = chkitem.Value.Split(";")

                                strServiceBuffer.Append("<DocumentElement>")
                                strServiceBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                                strServiceBuffer.Append(" <guestlineno>" & lblGuestLineNo.Text & "</guestlineno>")
                                strServiceBuffer.Append(" <servicelineno>" & othtypcode(0) & "</servicelineno>")
                                strServiceBuffer.Append(" <guestname>" & Guestname.ToUpper & "</guestname>")
                                strServiceBuffer.Append(" <services>" & chkitem.Text & "</services>")
                                strServiceBuffer.Append(" <servicescode>" & othtypcode(1) & "</servicescode>")
                                strServiceBuffer.Append("</DocumentElement>")
                                k += 1
                            End If

                        Next
                    End If

                    '  i = i + 1
                Next
            End If
            objBLLguest.GBGuestXml = strBuffer.ToString.Replace("&", "and")
            objBLLguest.GBServiceXml = strServiceBuffer.ToString.Replace("&", "and")
            objBLLguest.GBuserlogged = Session("GlobalUserName")




            Dim strQuery As String = ""
            Dim transferDeppax As Integer = 0
            If objBLLguest.SavingGuestBookingInTemp() Then

                If Not Session("sobjBLLTransferSearchActive") Is Nothing Then
                    Dim objBLLTransferSearch As New BLLTransferSearch
                    objBLLTransferSearch = New BLLTransferSearch
                    objBLLTransferSearch = CType(Session("sobjBLLTransferSearch"), BLLTransferSearch)

                    strQuery = "select isnull(max(isnull(adults,0))+max(isnull(child,0)),0) transferpax from booking_transferstemp(nolock) where transfertype='ARRIVAL' and requestid='" & CType(objBLLTransferSearch.OBRequestId, String) & "'"
                    transferArrpax = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

                    strQuery = "select isnull(max(isnull(adults,0))+max(isnull(child,0)),0) transferpax from booking_transferstemp(nolock) where transfertype='DEPARTURE' and requestid='" & CType(objBLLTransferSearch.OBRequestId, String) & "'"
                    transferDeppax = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

                    dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                    If dt.Rows.Count > 0 Then
                        bookingpax = Val(dt.Rows(0)("adults").ToString) + Val(dt.Rows(0)("child").ToString)

                    End If
                End If

                If bookingpax = transferArrpax Then
                    ViewState("AllGuestBookedArr") = 1
                Else
                    ViewState("AllGuestBookedArr") = 0
                End If
                If bookingpax = transferDeppax Then
                    ViewState("AllGuestBookedDep") = 1
                Else
                    ViewState("AllGuestBookedDep") = 0
                End If

                BindFlightDetails(intAuto)
                divFlightdetail.Style.Add("display", "block")

                divsubmit.Style.Add("display", "block")

                hdnguestsaved.Value = "1"

                hdnguestchange.Value = "0"

                'FlightArriovalSave(intAuto)

            End If
            ' BindVisaSummary()
        End If
    End Sub
    Protected Sub btnGenerateFlightDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateFlightDetails.Click

        FlightBtnCall(0) '*** Danny 01/09/2018
    End Sub

    Protected Sub btnValidateServiceGuest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidateServiceGuest.Click
        ValidateGuestService(0)
    End Sub

    Private Sub BindCheckInAndCheckOutHiddenfield()
        Dim objBLLHotelSearch = New BLLHotelSearch
        Dim dt As DataTable
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch = Session("sobjBLLHotelSearchActive")

            dt = objBLLHotelSearch.GetCheckInAndCheckOutDetailsFlights(objBLLHotelSearch.OBrequestid)
            If dt.Rows.Count > 0 Then
                hdCheckInPrevDay.Value = dt.Rows(0)("CheckInPrevDay").ToString
                hdCheckInNextDay.Value = dt.Rows(0)("CheckInNextDay").ToString
                hdCheckOutPrevDay.Value = dt.Rows(0)("CheckOutPrevDay").ToString
                hdCheckOutNextDay.Value = dt.Rows(0)("CheckOutNextDay").ToString '
                hdCheckIn.Value = dt.Rows(0)("CheckIn").ToString
                hdCheckOut.Value = dt.Rows(0)("CheckOut").ToString

            End If
        Else
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                hdCheckInPrevDay.Value = DateAdd("d", -1, CType(dt.Rows(0)("mindate_").ToString, Date))
                hdCheckInNextDay.Value = DateAdd("d", 1, CType(dt.Rows(0)("mindate_").ToString, Date))
                hdCheckOutPrevDay.Value = DateAdd("d", -1, CType(dt.Rows(0)("maxdate_").ToString, Date))
                hdCheckOutNextDay.Value = DateAdd("d", 1, CType(dt.Rows(0)("maxdate_").ToString, Date))
                hdCheckIn.Value = dt.Rows(0)("mindate_").ToString
                hdCheckOut.Value = dt.Rows(0)("maxdate_").ToString
            End If

        End If

    End Sub

    Protected Sub dlFlightDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlFlightDetails.ItemDataBound
        'onChange="ChangeArrivalDate(this)"
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dtRow As DataRow = DataListItemToDataRow(e.Item)

            Dim txtArrivalDate As TextBox = CType(e.Item.FindControl("txtArrivalDate"), TextBox)

            '  Dim ddlFlighttype As DropDownList = CType(e.Item.FindControl("ddlFlighttype"), DropDownList)

            Dim txtArrivalflight As TextBox = CType(e.Item.FindControl("txtArrivalflight"), TextBox)
            Dim txtArrivalTime As TextBox = CType(e.Item.FindControl("txtArrivalTime"), TextBox)
            Dim txtArrivalAirport As TextBox = CType(e.Item.FindControl("txtArrivalAirport"), TextBox)
            Dim chkSameFlight As CheckBox = CType(e.Item.FindControl("chkSameFlight"), CheckBox)
            Dim txtArrivalflightCode As TextBox = CType(e.Item.FindControl("txtArrivalflightCode"), TextBox)
            Dim txtArrivaltoairport As TextBox = CType(e.Item.FindControl("txtArrivaltoairport"), TextBox)
            Dim txtArrBordecode As TextBox = CType(e.Item.FindControl("txtArrBordecode"), TextBox)
            Dim chkNA As CheckBox = CType(e.Item.FindControl("chkNA"), CheckBox)
            Dim divreason As HtmlGenericControl = CType(e.Item.FindControl("divreason"), HtmlGenericControl)
            Dim txtreason As TextBox = CType(e.Item.FindControl("txtreason"), TextBox)
            Dim txtservicelineno As TextBox = CType(e.Item.FindControl("txtservicelineno"), TextBox)


            txtArrivalDate.Text = IIf(Convert.ToString(dtRow("flightdate")) = "", "", dtRow("flightdate"))
            txtArrivalflight.Text = IIf(Convert.ToString(dtRow("Flightno")) = "", "", dtRow("Flightno"))
            txtArrivalTime.Text = IIf(Convert.ToString(dtRow("Flighttime")) = "", "", dtRow("Flighttime"))
            txtArrivalAirport.Text = IIf(Convert.ToString(dtRow("Airport")) = "", "", dtRow("Airport"))
            '  ddlFlighttype.Text = IIf(Convert.ToString(dtRow("Flighttype")) = "", "", dtRow("Flighttype"))
            txtArrivalflightCode.Text = IIf(Convert.ToString(dtRow("Flightcode")) = "", "", dtRow("Flightcode"))
            '   chkSameFlight.Checked = IIf(Convert.ToString(dtRow("SameGuest")) = "1", True, False)
            txtArrBordecode.Text = IIf(Convert.ToString(dtRow("ArrBordecode")) = "", "", dtRow("ArrBordecode"))
            txtArrivaltoairport.Text = IIf(Convert.ToString(dtRow("Arrivaltoairport")) = "", "", dtRow("Arrivaltoairport"))
            txtreason.Text = IIf(Convert.ToString(dtRow("NAReason")) = "", "", dtRow("NAReason"))
            If Convert.ToString(dtRow("NAticked")) = "" Or Convert.ToString(dtRow("NAticked")) = "0" Then
                chkNA.Checked = False
            Else
                chkNA.Checked = True
            End If

            txtservicelineno.Text = IIf(Convert.ToString(dtRow("tlineno")) = "", "", dtRow("tlineno"))



            Dim strQuery As String = "select 't' from booking_guest_flightstemp(nolock) where  requestid='" & CType(Session("sEditRequestId"), String) & "'"
            Dim flightexists As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)



            Dim chkguest As CheckBoxList = CType(e.Item.FindControl("chkguest"), CheckBoxList)
            Dim guetsnamesnew As String()
            If Convert.ToString(dtRow("Guestnames")) <> "" Then
                Dim guetsnames As String() = Right(dtRow("Guestnames").ToString, Len(dtRow("Guestnames").ToString) - 1).Split(",")
                For i = 0 To guetsnames.Length - 1
                    guetsnamesnew = guetsnames(i).ToString.Split("|")

                    chkguest.Items.Add(New System.Web.UI.WebControls.ListItem(guetsnamesnew(0), guetsnamesnew(1)))

                Next

                'chkguest.DataSource = Session("ShowGuests")
                'chkguest.DataTextField = "Guestname"
                'chkguest.DataValueField = "Guestlineno"
                'chkguest.DataBind()
            Else
                ' If flightexists = "" Then
                '  Dim dt1 As DataTable = objBLLHotelSearch.GetResultAsDataTable("select distinct g.requestid,ltrim(str(g.guestlineno))+';'+ltrim(str(g.rlineno)) guestlineno,g.Guestname from booking_guestservicestemp g(nolock)  where requestid='" & Session("sRequestId") & "'  and  servicelineno=" & Val(txtservicelineno.Text) & " and (servicecode like 'AIRPORT%' or servicecode like 'TRANSFER%')  ") 'modified by abin on 20181219
                'Dim dt1 As DataTable = objBLLHotelSearch.GetResultAsDataTable("select distinct g.requestid,ltrim(str(g.guestlineno))+';'+ltrim(str(g.rlineno)) guestlineno,g.Guestname from booking_guestservicestemp g(nolock)  where requestid='" & Session("sRequestId") & "'  and (servicecode like 'AIRPORT%' or servicecode like 'TRANSFER%')  ") 'modified by abin on 20181219
                Dim dt1 As DataTable = objBLLHotelSearch.GetResultAsDataTable("execute sp_getflightguest '" & Session("sRequestId") & "'  ,'ARRIVAL'")
                If dt1.Rows.Count > 0 Then
                    Session("ShowGuests") = dt1
                Else
                    dt1 = objBLLHotelSearch.GetResultAsDataTable("execute GetOthersGuestnames_new '" & Session("sRequestId") & "'  ,'0'")
                    If dt1.Rows.Count > 0 Then
                        Session("ShowGuests") = dt1
                    End If

                End If
                'End If
                chkguest.DataSource = Session("ShowGuests")
                chkguest.DataTextField = "Guestname"
                chkguest.DataValueField = "Guestlineno"
                chkguest.DataBind()
            End If

            If ViewState("AllGuestBookedArr") = 1 Then
                chkguest.Enabled = True 'False 'changed by mohamed on 11/09/2018
                btnAddflight.Visible = True 'False 'changed for testing 21/10/2018
            Else
                chkguest.Enabled = True
                btnAddflight.Visible = True
            End If


            chkNA.Attributes.Add("onChange", "javascript:showreason('" + chkNA.ClientID + "','" + CType(divreason.ClientID, String) + "','" + CType(txtArrivalflight.ClientID, String) + "','" + CType(txtArrivalTime.ClientID, String) + "','" + CType(txtArrivalAirport.ClientID, String) + "','" + CType(txtArrivaltoairport.ClientID, String) + "' )")

            ' Dim divguestnames As HtmlGenericControl = CType(e.Item.FindControl("divguestnames"), HtmlGenericControl)
            '  chkSameFlight.Attributes.Add("onChange", "javascript:return ShowhideGuestname('" + CType(chkSameFlight.ClientID, String) + "','" + CType(divguestnames.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "' )")



            '   Dim txtDepartureDate As TextBox = CType(e.Item.FindControl("txtDepartureDate"), TextBox)
            If txtArrivalDate.Text = "" Then
                txtArrivalDate.Text = hdCheckIn.Value
            End If
            '   txtDepartureDate.Text = hdCheckOut.Value
            Dim AutoCompleteExtender_txtArrivalflight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtArrivalflight"), AjaxControlToolkit.AutoCompleteExtender)
            '  Dim AutoCompleteExtender_DepartureFlight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_DepartureFlight"), AjaxControlToolkit.AutoCompleteExtender)
            AutoCompleteExtender_txtArrivalflight.ContextKey = hdCheckOut.Value
            '    AutoCompleteExtender_DepartureFlight.ContextKey = hdCheckOut.Value
            txtArrivalDate.Attributes.Add("onchange", "ChangeArrivalDate('" & txtArrivalDate.ClientID & "','" & AutoCompleteExtender_txtArrivalflight.ClientID & "')")
            '  txtDepartureDate.Attributes.Add("onchange", "ChangeDepartureDate('" & txtDepartureDate.ClientID & "','" & AutoCompleteExtender_DepartureFlight.ClientID & "')")
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

    Private Sub BindBookingSummary()
        dvTabHotelSummary.Visible = False
        Dim strRequestId As String = ""
        Dim dt As DataTable
        Dim objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch = Session("sobjBLLHotelSearchActive")

            strRequestId = GetExistingRequestId()
            If Session("sLoginType") = "RO" Then
                dt = objBLLHotelSearch.GetBookingSummary(strRequestId, "0")
            Else
                dt = objBLLHotelSearch.GetBookingSummary(strRequestId, objResParam.WhiteLabel)
            End If


            If dt.Rows.Count > 0 Then
                dlBookingSummary.DataSource = dt
                dlBookingSummary.DataBind()
                dvTabHotelSummary.Visible = True
                If hdBookingEngineRateType.Value <> "1" Then
                    lblTabHotelTotalPrice.Text = dt.Rows(0)("HotelTotalPrice").ToString

                End If
                hdTabHotelTotalPrice.Value = "1"
            End If
        Else
            strRequestId = GetExistingRequestId()

            If Session("sLoginType") = "RO" Then
                dt = objBLLHotelSearch.GetBookingSummary(strRequestId, "0")
            Else
                dt = objBLLHotelSearch.GetBookingSummary(strRequestId, objResParam.WhiteLabel)
            End If
            If dt.Rows.Count > 0 Then
                dlBookingSummary.DataSource = dt
                dlBookingSummary.DataBind()
                dvTabHotelSummary.Visible = True
                If hdBookingEngineRateType.Value <> "1" Then
                    lblTabHotelTotalPrice.Text = dt.Rows(0)("HotelTotalPrice").ToString

                End If
                hdTabHotelTotalPrice.Value = "1"
            End If

        End If

    End Sub
    Private Sub BindTourSummary()
        dvTabTourSummary.Visible = False
        Dim objBLLTourSearch = New BLLTourSearch

        Dim strRequestId As String = ""
        Dim dt As DataTable

        strRequestId = GetExistingRequestId()
        If Session("sLoginType") = "RO" Then
            dt = objBLLTourSearch.GetTourSummary(strRequestId, "0")
        Else
            dt = objBLLTourSearch.GetTourSummary(strRequestId, objResParam.WhiteLabel)
        End If

        If dt.Rows.Count > 0 Then

            dvTourSummarry.Visible = True

            dlTourSummary.DataSource = dt
            dlTourSummary.DataBind()
            lblTourTotalPrice.Text = dt.Rows(0)("total").ToString
            dvTabTourSummary.Visible = True
            If hdBookingEngineRateType.Value = "1" Then
                dvTourTotal.Visible = False

            Else
                dvTourTotal.Visible = True
                lblTourTabTotalPrice.Text = dt.Rows(0)("total").ToString
            End If
            hdTourTabTotalPrice.Value = "1"
        Else
            dvTourSummarry.Visible = False
        End If



        'If Not Session("sobjBLLTourSearchActive") Is Nothing Then
        '    objBLLTourSearch = Session("sobjBLLTourSearchActive")
        '    Dim dt As DataTable

        '    dt = objBLLTourSearch.GetTourSummary(objBLLTourSearch.EbRequestID)
        '    If dt.Rows.Count > 0 Then

        '        dvTourSummarry.Visible = True

        '        dlTourSummary.DataSource = dt
        '        dlTourSummary.DataBind()
        '        lblTourTotalPrice.Text = dt.Rows(0)("total").ToString
        '        dvTabTourSummary.Visible = True
        '        If hdBookingEngineRateType.Value = "1" Then
        '            dvTourTotal.Visible = False

        '        Else
        '            dvTourTotal.Visible = True
        '            lblTourTabTotalPrice.Text = dt.Rows(0)("total").ToString
        '        End If
        '        hdTourTabTotalPrice.Value = "1"
        '    Else
        '        dvTourSummarry.Visible = False
        '    End If
        'Else
        '    dvTourSummarry.Visible = False
        'End If

    End Sub
    Private Sub BindTransferSummary()
        dvTabTransfersummary.Visible = False
        Dim objBLLTransferSearch = New BLLTransferSearch


        Dim strRequestId As String = ""
        Dim dt As DataTable

        strRequestId = GetExistingRequestId()
        If Session("sLoginType") = "RO" Then
            dt = objBLLTransferSearch.GetTransferSummary(strRequestId, "0")
        Else
            dt = objBLLTransferSearch.GetTransferSummary(strRequestId, hdWhiteLabel.Value)
        End If

        If dt.Rows.Count > 0 Then

            dvTransferSummary.Visible = True
            dvTabTransfersummary.Visible = True
            dlTransferSummary.DataSource = dt
            dlTransferSummary.DataBind()

            lblTransfertotal.Text = dt.Rows(0)("total").ToString


            If hdBookingEngineRateType.Value = "1" Then
                dvTransferTotal.Visible = False

            Else
                dvTransferTotal.Visible = True
                lblTransferTabTotalPrice.Text = dt.Rows(0)("total").ToString
            End If
            hdTransferTabTotalPrice.Value = "1"
        Else
            dvTransferSummary.Visible = False
        End If



        'If Not Session("sobjBLLTransferSearchActive") Is Nothing Then
        '    objBLLTransferSearch = Session("sobjBLLTransferSearchActive")
        '    Dim dt As DataTable

        '    dt = objBLLTransferSearch.GetTransferSummary(objBLLTransferSearch.OBRequestId)
        '    If dt.Rows.Count > 0 Then

        '        dvTransferSummary.Visible = True
        '        dvTabTransfersummary.Visible = True
        '        dlTransferSummary.DataSource = dt
        '        dlTransferSummary.DataBind()

        '        lblTransfertotal.Text = dt.Rows(0)("total").ToString


        '        If hdBookingEngineRateType.Value = "1" Then
        '            dvTransferTotal.Visible = False

        '        Else
        '            dvTransferTotal.Visible = True
        '            lblTransferTabTotalPrice.Text = dt.Rows(0)("total").ToString
        '        End If
        '        hdTransferTabTotalPrice.Value = "1"
        '    Else
        '        dvTransferSummary.Visible = False
        '    End If
        'Else
        '    dvTransferSummary.Visible = False
        'End If

    End Sub

    Private Sub BindVisaSummary()
        dvTabVisaSummary.Visible = False
        Dim objBLLHotelSearch = New BLLHotelSearch

        Dim dtvisdet As DataTable
        Dim strRequestId As String = ""

        Dim dt As DataTable
        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
        If dt.Rows.Count > 0 Then
            strRequestId = dt.Rows(0)("requestid").ToString
        End If
        '  dtvisdet = objBLLHotelSearch.GetVisaSummary(strRequestId)

        If Session("sLoginType") = "RO" Then
            dtvisdet = objBLLHotelSearch.GetVisaSummary(strRequestId)
        Else
            dtvisdet = objBLLHotelSearch.GetVisaSummary(strRequestId, objResParam.WhiteLabel)
        End If
        Dim totalvisa As Double = 0
        If dtvisdet.Rows.Count > 0 Then
            dvVisaSummary.Visible = True

            dlVisaSummary.DataSource = dtvisdet 'ff
            dlVisaSummary.DataBind()
            dvTabVisaSummary.Visible = True
            lblVisaHeading.Visible = True

            For i = 0 To dtvisdet.Rows.Count - 1
                ' totalvisa = totalvisa + Math.Round(Val(dtvisdet.Rows(i).Item("totalvisavalue").ToString))
                totalvisa = Math.Round(Val(dtvisdet.Rows(i).Item("totalvisavalue").ToString))
            Next

            If hdBookingEngineRateType.Value = "1" Then
                dvVisatotal.Visible = False
                lblVisaTotal.Visible = False
                lblVIsaTotalPrice.Visible = False

            Else
                lblVisaTotal.Visible = True
                dvVisatotal.Visible = True
                lblVIsaTotalPrice.Text = CType(Math.Round(Val(dtvisdet.Rows(0).Item("totalvisavalue").ToString)), String) + " " + dtvisdet.Rows(0).Item("currcode").ToString
                lblVisaTabTotalPrice.Text = CType(Math.Round(Val(totalvisa)), String) + " " + dtvisdet.Rows(0).Item("currcode").ToString 'CType(Math.Round(Val(dtvisdet.Rows(0).Item("totalvisavalue").ToString)), String) + " " + dtvisdet.Rows(0).Item("currcode").ToString
            End If
            hdVisaTabTotalPrice.Value = "1"

        Else
            dvVisaSummary.Visible = False
        End If

    End Sub

    Protected Sub dlBookingSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBookingSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim dvAmend As HtmlGenericControl = CType(e.Item.FindControl("dvAmend"), HtmlGenericControl)
            Dim lblStars As Label = CType(e.Item.FindControl("lblStars"), Label)
            Dim lblrlineno As Label = CType(e.Item.FindControl("lblrlineno"), Label)
            Dim dvHotelStars As HtmlGenericControl = CType(e.Item.FindControl("dvHotelStars"), HtmlGenericControl)
            Dim dvTourTotal As HtmlGenericControl = CType(e.Item.FindControl("dvTourTotal"), HtmlGenericControl)
            Dim dvHotelButtons As HtmlGenericControl = CType(e.Item.FindControl("dvHotelButtons"), HtmlGenericControl)
            Dim dvHotelCancelled As HtmlGenericControl = CType(e.Item.FindControl("dvHotelCancelled"), HtmlGenericControl)
            Dim dvHotelAmend As HtmlGenericControl = CType(e.Item.FindControl("dvHotelAmend"), HtmlGenericControl)
            Dim dvHotelDelete As HtmlGenericControl = CType(e.Item.FindControl("dvHotelDelete"), HtmlGenericControl)
            Dim dvHotelRemarks As HtmlGenericControl = CType(e.Item.FindControl("dvHotelRemarks"), HtmlGenericControl)
            Dim dvHotelConfirm As HtmlGenericControl = CType(e.Item.FindControl("dvHotelConfirm"), HtmlGenericControl)
            Dim dvSplEvents As HtmlGenericControl = CType(e.Item.FindControl("dvSplEvents"), HtmlGenericControl)
            Dim dlSpecialEventsSummary As DataList = CType(e.Item.FindControl("dlSpecialEventsSummary"), DataList)
            Dim dvRoomValue As HtmlGenericControl = CType(e.Item.FindControl("dvRoomValue"), HtmlGenericControl)

            Dim hdCancelled As HiddenField = CType(e.Item.FindControl("hdCancelled"), HiddenField)
            If Not Session("sLoginType") Is Nothing Then
                If Session("sLoginType") = "RO" Then
                    dvAmend.Visible = True
                Else
                    dvAmend.Visible = False
                End If
            Else
                dvAmend.Visible = False
            End If


            If hdBookingEngineRateType.Value = "1" Then
                dvTourTotal.Visible = False
                dvRoomValue.Visible = False
            Else
                dvTourTotal.Visible = True
                dvRoomValue.Visible = True
            End If

            Dim strHotelStarHTML As New StringBuilder

            strHotelStarHTML.Append(" <nav class='chk-stars'><ul>")
            If lblStars.Text = "1" Then
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
            ElseIf lblStars.Text = "2" Then
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
            ElseIf lblStars.Text = "3" Then
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
            ElseIf lblStars.Text = "4" Then
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
            ElseIf lblStars.Text = "5" Then
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-b.png' /></li>")
            Else
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
                strHotelStarHTML.Append(" <li><img alt='' src='img/chk-star-a.png' /></li>")
            End If

            strHotelStarHTML.Append(" </ul>")
            dvHotelStars.InnerHtml = strHotelStarHTML.ToString
            If hdCancelled.Value = "1" Then
                ' dvHotelButtons.Visible = False
                dvHotelCancelled.Visible = True
            Else
                'dvHotelButtons.Visible = True
                dvHotelCancelled.Visible = False
            End If
            dvHotelButtons.Visible = False
            BindSpecilaEventsDetails(lblrlineno.Text, dvSplEvents, dlSpecialEventsSummary, dvRoomValue)
            'If Session("sLoginType") <> "RO" Then
            '    dvHotelConfirm.Visible = False
            'End If
        End If

    End Sub
    Private Sub BindSpecilaEventsDetails(ByVal strRLineNo As String, ByVal dvSplEvents As HtmlGenericControl, ByVal dlSpecialEventsSummary As DataList, ByVal dvRoomValue As HtmlGenericControl)
        Dim dt As DataTable

        If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
            dt = objBLLHotelSearch.GetHotelSpecialEventsSummary(Session("sRequestId"), strRLineNo, hdWhiteLabel.Value)
        Else
            dt = objBLLHotelSearch.GetHotelSpecialEventsSummary(Session("sRequestId"), strRLineNo, "0")
        End If
        If dt.Rows.Count > 0 Then
            dlSpecialEventsSummary.DataSource = dt
            dlSpecialEventsSummary.DataBind()
        Else
            dvSplEvents.Visible = False
            dvRoomValue.Visible = False
        End If

    End Sub
    Protected Sub ddlVisa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlVisa As DropDownList = CType(sender, DropDownList)
        Dim dlItem As DataListItem = CType(ddlVisa.NamingContainer, DataListItem)
        Dim ddlVisatype As DropDownList = CType(dlItem.FindControl("ddlVisatype"), DropDownList)
        Dim txtVisaPrice As TextBox = CType(dlItem.FindControl("txtVisaPrice"), TextBox)
        Dim txtNationalityCode As TextBox = CType(dlItem.FindControl("txtNationalityCode"), TextBox)
        txtVisaPrice.Text = ""
        If ddlVisa.SelectedValue = "Required" Then
            ddlVisatype.SelectedValue = "V-000001"
            txtVisaPrice.Text = GetVisaPrice(ddlVisatype.SelectedValue, txtNationalityCode.Text)
        Else
            ddlVisatype.SelectedIndex = 0
            txtVisaPrice.Text = ""
        End If
    End Sub

    Private Function SavingPersonalFlightDetails() As String

        If ValidateGuestDetails() Then

            Dim strBuffer As New Text.StringBuilder
            Dim strFliBuffer As New Text.StringBuilder


            objBLLguest.GBRequestid = GetRequestId()

            Dim ddlTittle As DropDownList
            Dim lblRowNoAll As Label
            Dim txtFirstName As TextBox
            Dim txtMiddleName As TextBox
            Dim txtLastName As TextBox
            Dim txtNationalityCode As TextBox
            Dim txtchildage As TextBox
            Dim ddlVisa As DropDownList
            Dim ddlVisatype As DropDownList
            Dim txtVisaPrice As TextBox
            Dim txtPassportNo As TextBox
            Dim i As Integer = 1
            If dlPersonalInfo.Items.Count > 0 Then
                For Each item As DataListItem In dlPersonalInfo.Items
                    Dim strTittle As String = ""
                    lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
                    ddlTittle = CType(item.FindControl("ddlTittle"), DropDownList)
                    txtFirstName = CType(item.FindControl("txtFirstName"), TextBox)
                    txtMiddleName = CType(item.FindControl("txtMiddleName"), TextBox)
                    txtLastName = CType(item.FindControl("txtlastName"), TextBox)
                    txtchildage = CType(item.FindControl("txtchildage"), TextBox)
                    ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
                    ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
                    txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
                    txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
                    txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)

                    If ddlTittle.Text <> "0" Or txtFirstName.Text <> "" Or txtLastName.Text <> "" Then

                        If ddlTittle.Text = "0" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
                            ddlTittle.Focus()
                            Return False
                            Exit Function

                        End If
                        If txtFirstName.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter first name")
                            txtFirstName.Focus()
                            Return False
                            Exit Function
                        End If
                        If txtLastName.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter last name")
                            txtLastName.Focus()
                            Return False
                            Exit Function
                        End If
                        If txtNationalityCode.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
                            Return False
                            Exit Function
                        End If


                        strBuffer.Append("<DocumentElement>")

                        strBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
                        'strBuffer.Append(" <guestlineno>" & i & "</guestlineno>")
                        strBuffer.Append(" <title>" & ddlTittle.Text & "</title>")
                        strBuffer.Append(" <firstname>" & txtFirstName.Text & "</firstname>")
                        strBuffer.Append(" <middlename>" & txtMiddleName.Text & "</middlename>")
                        strBuffer.Append(" <lastname>" & txtLastName.Text & "</lastname>")
                        strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
                        strBuffer.Append(" <childage>" & CType(Val(txtchildage.Text), Decimal) & "</childage>")
                        strBuffer.Append(" <visaoptions>" & ddlVisa.SelectedValue & "</visaoptions>")
                        strBuffer.Append(" <visatypecode>" & ddlVisatype.SelectedValue & "</visatypecode>")
                        strBuffer.Append(" <visaprice>" & CType(Val(txtVisaPrice.Text), Decimal) & "</visaprice>")
                        strBuffer.Append(" <passportno>" & txtPassportNo.Text & "</passportno>")
                        strBuffer.Append("</DocumentElement>")
                    End If
                    '  i = i + 1
                Next
            End If
            objBLLguest.GBGuestXml = strBuffer.ToString
            objBLLguest.GBuserlogged = Session("GlobalUserName")



            Dim txtArrivalDate As TextBox
            Dim txtArrivalflightCode As TextBox
            Dim txtArrivalTime As TextBox
            Dim txtArrivalAirport As TextBox
            Dim txtArrivalflight As TextBox
            Dim txtDepartureDate As TextBox
            Dim txtDepartureFlightCode As TextBox
            Dim txtDepartureTime As TextBox
            Dim txtDepartureAirport As TextBox
            Dim txtDepartureFlight As TextBox

            Dim j As Integer = 1
            If dlFlightDetails.Items.Count > 0 Then
                For Each item As DataListItem In dlFlightDetails.Items
                    Dim strTittle As String = ""
                    txtArrivalDate = CType(item.FindControl("txtArrivalDate"), TextBox)
                    txtDepartureDate = CType(item.FindControl("txtDepartureDate"), TextBox)
                    If txtArrivalDate.Text <> "" And txtDepartureDate.Text <> "" Then





                        lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)

                        txtArrivalflight = CType(item.FindControl("txtArrivalflight"), TextBox)


                        txtArrivalflightCode = CType(item.FindControl("txtArrivalflightCode"), TextBox)
                        txtArrivalTime = CType(item.FindControl("txtArrivalTime"), TextBox)
                        txtArrivalAirport = CType(item.FindControl("txtArrivalAirport"), TextBox)

                        txtDepartureFlightCode = CType(item.FindControl("txtDepartureFlightCode"), TextBox)
                        txtDepartureTime = CType(item.FindControl("txtDepartureTime"), TextBox)
                        txtDepartureAirport = CType(item.FindControl("txtDepartureAirport"), TextBox)

                        txtDepartureFlight = CType(item.FindControl("txtDepartureFlight"), TextBox)

                        strFliBuffer.Append("<DocumentElement>")

                        If chkSameFlight.Checked Then
                            strFliBuffer.Append(" <guestlineno> 0</guestlineno>")
                        Else
                            strFliBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
                        End If
                        Dim strFromDate As String = txtArrivalDate.Text
                        If strFromDate <> "" Then
                            Dim strDates As String() = strFromDate.Split("/")
                            strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                        End If

                        strFliBuffer.Append(" <arrdate>" & Format(CType(strFromDate, Date), "yyyy/MM/dd") & "</arrdate>")
                        strFliBuffer.Append(" <arrflightcode>" & txtArrivalflight.Text & "</arrflightcode>")
                        strFliBuffer.Append(" <arrflight_tranid>" & txtArrivalflightCode.Text.Trim.Split("|").GetValue(0) & "</arrflight_tranid>")
                        strFliBuffer.Append(" <arrflighttime>" & txtArrivalTime.Text & "</arrflighttime>")
                        Dim arrairportbordercode As String = objclsUtilities.ExecuteQueryReturnStringValue("select  airportbordercode  from  flightmast where flight_tranid='" & txtArrivalflightCode.Text.Trim.Split("|").GetValue(0) & "'and flightcode= '" & txtArrivalflight.Text.Trim & "'")
                        strFliBuffer.Append(" <arrairportbordercode>" & arrairportbordercode & "</arrairportbordercode>")

                        Dim strDeptDate As String = txtDepartureDate.Text
                        If strDeptDate <> "" Then
                            Dim strDates As String() = strDeptDate.Split("/")
                            strDeptDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                        End If

                        strFliBuffer.Append(" <depdate>" & Format(CType(strDeptDate, Date), "yyyy/MM/dd") & "</depdate>")
                        strFliBuffer.Append(" <depflightcode>" & txtDepartureFlight.Text & "</depflightcode>")
                        strFliBuffer.Append(" <depflight_tranid>" & txtDepartureFlightCode.Text.Split("|").GetValue(0) & "</depflight_tranid>")
                        strFliBuffer.Append(" <depflighttime>" & txtDepartureTime.Text & "</depflighttime>")
                        Dim depairportbordercode As String = objclsUtilities.ExecuteQueryReturnStringValue("select airportbordercode  from  flightmast where flight_tranid='" & txtDepartureFlightCode.Text.Trim.Split("|").GetValue(0) & "'and flightcode= '" & txtDepartureFlight.Text.Trim & "'")
                        strFliBuffer.Append(" <depairportbordercode>" & depairportbordercode & "</depairportbordercode>")
                        strFliBuffer.Append("</DocumentElement>")
                        ' j = j + 1
                    End If
                Next
            End If
            objBLLguest.GBFlightXml = strFliBuffer.ToString
            If objBLLguest.SavingGuestFlightBookingInTemp() Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
            End If
        Else
            Return False
            Exit Function
        End If
        Return True
    End Function
    Sub SavingGuests()

        Dim dt As DataTable

        '  If ValidatePersonalDetails() Then


        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        ''If Session("sAgentCompany") Is  Nothing Or Session("GlobalUserName") Is Nothing Then
        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))



        Dim ddlTitle As DropDownList
        Dim lblRowNoAll As Label
        Dim lblrlineno As Label
        Dim txtFirstNameg As TextBox
        Dim txtMidName As TextBox
        Dim txtLastNameg As TextBox
        Dim txtNationalityCode As TextBox
        Dim txtchildage As TextBox
        Dim ddlVisa As DropDownList
        Dim ddlVisatype As DropDownList
        Dim txtVisaPrice As TextBox
        Dim lblGuestLineNo As Label
        Dim txtPassportNo As TextBox
        Dim i As Integer = 1
        If dlPersonalInfo.Items.Count > 0 Then

            If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
                Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
                objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
                objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
                objBLLguest.GBRlineno = objBLLHotelSearch.OBrlineno
            Else
                objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                objBLLguest.GBRlineno = "0"
            End If
            ' 

            For Each item As DataListItem In dlPersonalInfo.Items
                Dim strTittle As String = ""
                lblrlineno = CType(item.FindControl("lblrlineno"), Label)
                lblGuestLineNo = CType(item.FindControl("lblGuestLineNo"), Label)
                lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
                ddlTitle = CType(item.FindControl("ddlTittle"), DropDownList)
                txtFirstNameg = CType(item.FindControl("txtFirstName"), TextBox)
                txtMidName = CType(item.FindControl("txtMiddleName"), TextBox)
                txtLastNameg = CType(item.FindControl("txtlastName"), TextBox)
                txtchildage = CType(item.FindControl("txtchildage"), TextBox)
                ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
                ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
                txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
                txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
                txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)

                Dim lblRowHeading = CType(item.FindControl("lblRowHeading"), Label)
                Dim lblroomno = CType(item.FindControl("lblroomno"), Label)
                Dim lblType = CType(item.FindControl("lblPType"), Label)



                strBuffer.Append("<DocumentElement>")

                strBuffer.Append(" <guestlineno>" & lblGuestLineNo.Text & "</guestlineno>")

                strBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                'strBuffer.Append(" <guestlineno>" & i & "</guestlineno>")
                If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                    strBuffer.Append(" <title>Mr</title>")
                ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                    strBuffer.Append(" <title>Child Male</title>")
                Else
                    strBuffer.Append(" <title>" & ddlTitle.Text & "</title>")
                End If

                strBuffer.Append(" <firstname>" & IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text.ToUpper) & "</firstname>")
                strBuffer.Append(" <middlename>" & txtMidName.Text.ToUpper & "</middlename>")
                strBuffer.Append(" <lastname>" & txtLastNameg.Text.ToUpper & "</lastname>")
                strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
                If lblType.Text.Contains("Child") = True Then  '' Added shahul 30/07/18
                    strBuffer.Append(" <childage>" & CType(Val(txtchildage.Text), Decimal) & "</childage>")
                Else
                    strBuffer.Append(" <childage>0</childage>")
                End If
                strBuffer.Append(" <visaoptions></visaoptions>")
                strBuffer.Append(" <visatypecode></visatypecode>")
                strBuffer.Append(" <visaprice>0</visaprice>")
                strBuffer.Append(" <passportno></passportno>")
                strBuffer.Append(" <roomno>" & lblroomno.Text & "</roomno>")
                If lblType.Text.Contains("Adult") = True Then
                    strBuffer.Append(" <guesttype>Adult</guesttype>")
                Else
                    strBuffer.Append(" <guesttype>Child</guesttype>")
                End If
                strBuffer.Append("</DocumentElement>")

                Dim chkservices As CheckBoxList = CType(item.FindControl("chkservices"), CheckBoxList)
                Dim title As String = ""
                Dim firstname As String = ""
                Dim Lastname As String = ""
                Dim middlename As String = "", Guestname As String = ""

                If chkservices.Items.Count > 0 Then

                    If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                        title = "Mr"
                    ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                        title = "Child Male"
                    Else
                        title = ddlTitle.Text
                    End If
                    firstname = IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text)
                    middlename = txtMidName.Text
                    Lastname = txtLastNameg.Text
                    Guestname = title + " " + firstname + " " + middlename + " " + Lastname
                    Dim k As Integer = 1
                    For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                        If chkitem.Selected = True Then

                            Dim othtypcode As String() = chkitem.Value.Split(";")

                            strServiceBuffer.Append("<DocumentElement>")
                            strServiceBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                            strServiceBuffer.Append(" <guestlineno>" & lblGuestLineNo.Text & "</guestlineno>")
                            strServiceBuffer.Append(" <servicelineno>" & othtypcode(0) & "</servicelineno>")
                            strServiceBuffer.Append(" <guestname>" & Guestname.ToUpper & "</guestname>")
                            strServiceBuffer.Append(" <services>" & chkitem.Text & "</services>")
                            strServiceBuffer.Append(" <servicescode>" & othtypcode(1) & "</servicescode>")
                            strServiceBuffer.Append("</DocumentElement>")
                            k += 1
                        End If

                    Next
                End If

                '  i = i + 1
            Next
        End If
        objBLLguest.GBGuestXml = strBuffer.ToString
        objBLLguest.GBServiceXml = strServiceBuffer.ToString
        objBLLguest.GBuserlogged = Session("GlobalUserName")






        If objBLLguest.SavingGuestBookingInTemp() Then
            'BindFlightDetails()
            'divFlightdetail.Style.Add("display", "block")
            'btnGenerateFlightDetails.Style.Add("display", "none")
            'btnAdd.Style.Add("display", "none")
            'btnAddchd.Style.Add("display", "none")
            'divsubmit.Style.Add("display", "block")
            'dlPersonalInfo.Enabled = False
        End If

        ' End If

    End Sub
    Function Validateflightdetails() As Boolean

        Validateflightdetails = False
        For Each item As DataListItem In dlFlightDetails.Items

            Dim txtArrivalDate As TextBox = CType(item.FindControl("txtArrivalDate"), TextBox)
            Dim txtArrivalflight As TextBox = CType(item.FindControl("txtArrivalflight"), TextBox)

            If txtArrivalflight.Text <> "" Then
                Validateflightdetails = True
                Exit For
            End If
        Next

        For Each item As DataListItem In dldeparturedetails.Items

            Dim txtDepartureDate As TextBox = CType(item.FindControl("txtDepartureDate"), TextBox)
            Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)

            If txtDepartureFlight.Text <> "" Then
                Validateflightdetails = True
                Exit For
            End If
        Next


    End Function
    Private Sub FillErrorlist(ByVal dserror As DataTable)
        gdErrorlist.DataSource = dserror
        gdErrorlist.DataBind()
    End Sub
    Protected Sub ImgToursRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

                'If Session("sLoginType") <> "RO" Then
                If Session("sLoginType") = "Agent" Then
                    dvToursArrRemarks.Style.Add("display", "none")
                    dvToursDeptRemarks.Style.Add("display", "none")
                    dvToursPartyRemarks.Style.Add("display", "none")

                Else
                    dvToursArrRemarks.Style.Add("display", "block")
                    dvDepRemarks.Style.Add("display", "block")
                    dvToursPartyRemarks.Style.Add("display", "block")


                End If
            End If



            cleartextboxes(txtToursPartyRemarks, txtToursDeptRemarks, txtToursArrRemarks, txtToursCustRemarks)
            FillToursRemarksPopUp(sender)
            MPToursRemarks.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnTrfsSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrfsSaveRemarks.Click
        Try
            Dim type As String = "TRFS"
            If ValidateRemarksPopup(txtTrfsCustRemarks, txtTrfsPartyRemarks, txttrfsArrRemarks, txtTrfsDeptRemarks, type) Then

                objBLLguest.GBRequestid = GetExistingRequestId()
                If trfshdntlineno.Value <> "" Then
                    objBLLguest.GBGuestLineNo = CType(trfshdntlineno.Value, Integer)
                End If


                If txtTrfsPartyRemarks.Text <> "" Then
                    objBLLguest.GBTrfsPartyRemarks = CType(txtTrfsPartyRemarks.Text, String)
                Else
                    objBLLguest.GBTrfsPartyRemarks = ""
                End If
                If txtTrfsCustRemarks.Text <> "" Then
                    objBLLguest.GBTrfsAgentRemarks = CType(txtTrfsCustRemarks.Text, String)
                Else
                    objBLLguest.GBTrfsAgentRemarks = ""
                End If
                If txttrfsArrRemarks.Text <> "" Then
                    objBLLguest.GBTrfsArrivalRemarks = CType(txttrfsArrRemarks.Text, String)
                Else
                    objBLLguest.GBTrfsArrivalRemarks = ""

                End If
                If txtTrfsDeptRemarks.Text <> "" Then
                    objBLLguest.GBTrfsDepartureRemarks = CType(txtTrfsDeptRemarks.Text, String)
                Else
                    objBLLguest.GBTrfsDepartureRemarks = ""

                End If

                objBLLguest.GBuserlogged = Session("GlobalUserName")
                If objBLLguest.SavingtransfersRemarksInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnTrfsSaveRemarks_Click:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Function ValidateRemarksPopup(ByVal txtagentremarks As TextBox, ByVal txtpartyremarks As TextBox, ByVal txtarrremarks As TextBox, ByVal txtdeptremarks As TextBox, ByVal type As String) As Boolean
        If txtagentremarks.Text.Trim = "" And txtpartyremarks.Text.Trim = "" And txtarrremarks.Text.Trim = "" And txtdeptremarks.Text.Trim = "" Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Fill atleast one  RemarksType")
            txtToursPartyRemarks.Focus()
            If type = "OTH" Then
                '' MPOthServRemarks.Show()
            ElseIf type = "TRFS" Then
                ''   MPTransfersRemarks.Show()
            ElseIf type = "TOURS" Then
                MPToursRemarks.Show()
            ElseIf type = "AIR" Then
                MpAirportMaRemarks.Show()
            End If
            Return False
            Exit Function
        End If

        Return True
    End Function


    Protected Sub ImgAirportmaRemarks_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

                If Session("sLoginType") = "Agent" Then

                    dvAirArrRemarks.Style.Add("display", "none")
                    dvAirdeptremarks.Style.Add("display", "none")
                    dvAirPartyRemarks.Style.Add("display", "none")

                Else

                    dvAirArrRemarks.Style.Add("display", "block")
                    dvAirdeptremarks.Style.Add("display", "block")
                    dvAirPartyRemarks.Style.Add("display", "block")

                End If
            End If
            cleartextboxes(txtAirPartyRemarks, txtAirdeptRemarks, txtAirArrRemarks, txtAirCustRemarks)
            FillAiportmaRemarksPopUp(sender)
            MpAirportMaRemarks.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try


    End Sub
    Protected Sub btnAirSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAirSaveRemarks.Click
        Try
            Dim Type As String = "AIR"
            If ValidateRemarksPopup(txtAirCustRemarks, txtAirPartyRemarks, txtAirArrRemarks, txtAirdeptRemarks, Type) Then

                objBLLguest.GBRequestid = GetExistingRequestId()
                If airhdnalineno.Value <> "" Then
                    objBLLguest.GBGuestLineNo = CType(airhdnalineno.Value, Integer)
                End If


                If txtAirPartyRemarks.Text <> "" Then
                    objBLLguest.GBAirPartyRemarks = CType(txtAirPartyRemarks.Text, String)
                Else
                    objBLLguest.GBAirPartyRemarks = ""
                End If
                If txtAirCustRemarks.Text <> "" Then
                    objBLLguest.GBAirAgentRemarks = CType(txtAirCustRemarks.Text, String)
                Else
                    objBLLguest.GBAirAgentRemarks = ""
                End If
                If txtAirArrRemarks.Text <> "" Then
                    objBLLguest.GBAirArrivalRemarks = CType(txtAirArrRemarks.Text, String)
                Else
                    objBLLguest.GBAirArrivalRemarks = ""

                End If
                If txtAirdeptRemarks.Text <> "" Then
                    objBLLguest.GBAirDepartureRemarks = CType(txtAirdeptRemarks.Text, String)
                Else
                    objBLLguest.GBAirDepartureRemarks = ""

                End If

                objBLLguest.GBuserlogged = Session("GlobalUserName")
                If objBLLguest.SavingAirportmaRemarksInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: btnAirSaveRemarks_Click:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub ImgOthServRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

                If Session("sLoginType") = "Agent" Then

                    dvOthArrRemarks.Style.Add("display", "none")
                    dvOthdeptremarks.Style.Add("display", "none")
                    dvOthPartyRemarks.Style.Add("display", "none")


                Else
                    dvOthArrRemarks.Style.Add("display", "block")
                    dvOthdeptremarks.Style.Add("display", "block")
                    dvOthPartyRemarks.Style.Add("display", "block")

                End If
            End If
            cleartextboxes(txtOthPartyRemarks, txtOthDeptRemarks, txtOthArrRemarks, txtOthCustRemarks)
            FillOthServRemarksPopUp(sender)
            MPOthServRemarks.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub FillOthServRemarksPopUp(ByVal sender As Object)
        '  Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lbldlistolineno As Label = CType(dlItem.FindControl("lblolineno"), Label)
        Dim lblairporttype As Label = CType(dlItem.FindControl("lblairporttype"), Label)
        Dim lblothservicedate As Label = CType(dlItem.FindControl("lblothservicedate"), Label)
        OthHdnRlineno.Value = lbldlistolineno.Text
        ' Dim objBLLHotelSearch = New BLLHotelSearch
        lblothservicehead.Text = lblairporttype.Text
        lblotherservicedate.Text = lblothservicedate.Text
        Dim dt As New DataTable
        dt = objBLLguest.SetRemarksDetFromDataTable("select  * from booking_others_remarkstemp  where requestid= '" & GetNewOrExistingRequestId() & "' and rlineno ='" & lbldlistolineno.Text & " 'order by requestid")

        If dt.Rows.Count > 0 Then

            txtOthPartyRemarks.Text = dt.Rows(0).Item("Partyremarks")
            txtOthCustRemarks.Text = dt.Rows(0).Item("agentremarks")
            txtOthArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
            txtOthDeptRemarks.Text = dt.Rows(0).Item("departureremarks")



        End If
    End Sub
    Protected Sub btnOthSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOthSaveRemarks.Click
        Try


            Dim type As String = "OTH"
            If ValidateRemarksPopup(txtOthCustRemarks, txtOthPartyRemarks, txtOthArrRemarks, txtOthDeptRemarks, type) Then

                objBLLguest.GBRequestid = GetExistingRequestId()
                If OthHdnRlineno.Value <> "" Then
                    objBLLguest.GBGuestLineNo = CType(OthHdnRlineno.Value, Integer)
                End If


                If txtOthPartyRemarks.Text <> "" Then
                    objBLLguest.GBOthPartyRemarks = CType(txtOthPartyRemarks.Text, String)
                Else
                    objBLLguest.GBOthPartyRemarks = ""
                End If
                If txtOthCustRemarks.Text <> "" Then
                    objBLLguest.GBOthAgentRemarks = CType(txtOthCustRemarks.Text, String)
                Else
                    objBLLguest.GBOthAgentRemarks = ""
                End If
                If txtOthArrRemarks.Text <> "" Then
                    objBLLguest.GBOthArrivalRemarks = CType(txtOthArrRemarks.Text, String)
                Else
                    objBLLguest.GBOthArrivalRemarks = ""

                End If
                If txtOthDeptRemarks.Text <> "" Then
                    objBLLguest.GBOthDepartureRemarks = CType(txtOthDeptRemarks.Text, String)
                Else
                    objBLLguest.GBOthDepartureRemarks = ""

                End If

                objBLLguest.GBuserlogged = Session("GlobalUserName")
                If objBLLguest.SavingOtherServRemarksInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")
                End If

            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnOthSaveRemarks_Click:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub btnVisaSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisaSaveRemarks.Click

        Try
            objBLLguest.GBRequestid = GetExistingRequestId()
            If visahdnvlineno.Value <> "" Then
                objBLLguest.GBGuestLineNo = CType(visahdnvlineno.Value, Integer)
            End If


            If txtVisaAgentRemarks.Text <> "" Then
                objBLLguest.GBVisaAgentRemarks = CType(txtVisaAgentRemarks.Text, String)
            Else
                objBLLguest.GBVisaAgentRemarks = ""
            End If
            If txtvisaarrremarks.Text <> "" Then
                objBLLguest.GBVisaArrivalRemarks = CType(txtvisaarrremarks.Text, String)
            Else
                objBLLguest.GBVisaArrivalRemarks = ""

            End If

            objBLLguest.GBuserlogged = Session("GlobalUserName")
            If objBLLguest.SavingVisaRemarksInTemp() Then
                MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")
            End If
            ''   End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: btnVisaSaveRemarks_Click:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub FillVisaRemarksPopUp(ByVal sender As Object)
        '  Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lbldlistvlineno As Label = CType(dlItem.FindControl("lblvlineno"), Label)
        Dim lblvisatypename As Label = CType(dlItem.FindControl("lblvisatypename"), Label)
        Dim lblvisadate As Label = CType(dlItem.FindControl("lblvisadate"), Label)
        lblremvisadate.Text = lblvisadate.Text
        lblvisatype.Text = lblvisatypename.Text

        visahdnvlineno.Value = lbldlistvlineno.Text



        Dim dt As New DataTable
        dt = objBLLguest.SetRemarksDetFromDataTable("select  * from booking_visa_remarkstemp  where requestid= '" & GetNewOrExistingRequestId() & "' and vlineno ='" & lbldlistvlineno.Text & " 'order by requestid")

        If dt.Rows.Count > 0 Then


            txtVisaAgentRemarks.Text = dt.Rows(0).Item("agentremarks")

            txtvisaarrremarks.Text = dt.Rows(0).Item("arrivalremarks")



        End If
    End Sub
    Protected Sub ImgVisaRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try



            If Session("sLoginType") = "Agent" Then

                dvvisaarrivalrem.Style.Add("display", "none")

            Else
                dvvisaarrivalrem.Style.Add("display", "block")
            End If
            clearvisacontrols()
            FillVisaRemarksPopUp(sender)
            MPVisaRemarks.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Sub clearvisacontrols()

        txtVisaAgentRemarks.Text = ""
        txtvisaarrremarks.Text = ""
    End Sub
    Private Sub FillTransfersRemarksPopUp(ByVal sender As Object)
        '  Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lbldlisttlineno As Label = CType(dlItem.FindControl("lbltlineno"), Label)

        trfshdntlineno.Value = lbldlisttlineno.Text
        ' Dim objBLLHotelSearch = New BLLHotelSearch


        Dim lbltranferstype As Label = CType(dlItem.FindControl("lblExcursionName"), Label)

        Dim lblVehicleName As Label = CType(dlItem.FindControl("lblVehicleName"), Label)

        Dim lbldltransferdate As Label = CType(dlItem.FindControl("lbltransdate"), Label)

        lbltransfername.Text = lblVehicleName.Text
        lbltransferdate.Text = lbldltransferdate.Text
        If Session("sLoginType") <> "Agent" Then
            If lbltranferstype.Text = "ARRIVAL" Then
                dvtrfsDeptRemarks.Style.Add("display", "none")
            ElseIf lbltranferstype.Text = "DEPARTURE" Then
                dvtrfsArrRemarks.Style.Add("display", "none")
            End If
        End If
        Dim dt As New DataTable
        dt = objBLLguest.SetRemarksDetFromDataTable("select  * from booking_transfers_remarkstemp  where requestid= '" & GetNewOrExistingRequestId() & "' and tlineno ='" & lbldlisttlineno.Text & " 'order by requestid")

        If dt.Rows.Count > 0 Then

            txtTrfsPartyRemarks.Text = dt.Rows(0).Item("Partyremarks")
            txtTrfsCustRemarks.Text = dt.Rows(0).Item("agentremarks")
            txttrfsArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
            txtTrfsDeptRemarks.Text = dt.Rows(0).Item("departureremarks")



        End If
    End Sub
    Protected Sub imgTrfsRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            '   If Not Session("sobjBLLHotelSearchActive") Is Nothing Then

            If Session("sLoginType") = "Agent" Then

                dvtrfsArrRemarks.Style.Add("display", "none")
                dvtrfsDeptRemarks.Style.Add("display", "none")
                ' dvTxtHotRem.Style.Add("display", "none")
                dvTrfsPartyRemarks.Style.Add("display", "none")

            Else
                dvtrfsArrRemarks.Style.Add("display", "block")
                dvtrfsDeptRemarks.Style.Add("display", "block")
                'dvTxtHotRem.Style.Add("display", "block")
                dvTrfsPartyRemarks.Style.Add("display", "block")

            End If
            '  End If
            cleartextboxes(txtTrfsCustRemarks, txtTrfsPartyRemarks, txttrfsArrRemarks, txtTrfsDeptRemarks)
            FillTransfersRemarksPopUp(sender)
            MPTransfersRemarks.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub FillAiportmaRemarksPopUp(ByVal sender As Object)
        '  Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lbldlistalineno As Label = CType(dlItem.FindControl("lblalineno"), Label)

        Dim lblairporttype As Label = CType(dlItem.FindControl("lblairporttype"), Label)
        Dim lblairservicename As Label = CType(dlItem.FindControl("lblairservicename"), Label)


        Dim lblservicedate As Label = CType(dlItem.FindControl("lblservicedate"), Label)

        lblairservname.Text = lblairservicename.Text

        lblairservdate.Text = lblservicedate.Text


        airhdnalineno.Value = lbldlistalineno.Text

        If Session("sLoginType") <> "Agent" Then
            If lblairporttype.Text = "ARRIVAL" Then
                dvAirdeptremarks.Style.Add("display", "none")
            Else
                dvAirdeptremarks.Style.Add("display", "block")
            End If
            If lblairporttype.Text = "DEPARTURE" Then
                dvAirArrRemarks.Style.Add("display", "none")
            Else
                dvAirArrRemarks.Style.Add("display", "block")
            End If
        End If
        Dim dt As New DataTable
        dt = objBLLguest.SetRemarksDetFromDataTable("select  * from booking_airportma_remarkstemp  where requestid= '" & GetNewOrExistingRequestId() & "' and alineno ='" & lbldlistalineno.Text & " 'order by requestid")

        If dt.Rows.Count > 0 Then
            txtAirPartyRemarks.Text = dt.Rows(0).Item("Partyremarks")
            txtAirCustRemarks.Text = dt.Rows(0).Item("agentremarks")
            txtAirArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
            txtAirdeptRemarks.Text = dt.Rows(0).Item("departureremarks")

        End If
    End Sub
    Protected Sub btnToursSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToursSaveRemarks.Click
        Try
            Dim type As String = "TOURS"
            If ValidateRemarksPopup(txtToursCustRemarks, txtToursPartyRemarks, txtToursArrRemarks, txtToursDeptRemarks, type) Then

                objBLLguest.GBRequestid = GetExistingRequestId()
                If ToursHdnElineno.Value <> "" Then
                    objBLLguest.GBGuestLineNo = CType(ToursHdnElineno.Value, Integer)
                End If


                If txtToursPartyRemarks.Text <> "" Then
                    objBLLguest.GBToursPartyRemarks = CType(txtToursPartyRemarks.Text, String)
                Else
                    objBLLguest.GBToursPartyRemarks = ""
                End If
                If txtToursCustRemarks.Text <> "" Then
                    objBLLguest.GBToursAgentRemarks = CType(txtToursCustRemarks.Text, String)
                Else
                    objBLLguest.GBToursAgentRemarks = ""
                End If
                If txtToursArrRemarks.Text <> "" Then
                    objBLLguest.GBToursArrivalRemarks = CType(txtToursArrRemarks.Text, String)
                Else
                    objBLLguest.GBToursArrivalRemarks = ""

                End If
                If txtToursDeptRemarks.Text <> "" Then
                    objBLLguest.GBToursDepartureRemarks = CType(txtToursDeptRemarks.Text, String)
                Else
                    objBLLguest.GBToursDepartureRemarks = ""

                End If

                objBLLguest.GBuserlogged = Session("GlobalUserName")
                If objBLLguest.SavingToursRemarksInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: btnToursSaveRemarks_Click:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub FillToursRemarksPopUp(ByVal sender As Object)
        '  Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lbldlistelineno As Label = CType(dlItem.FindControl("lblelineno"), Label)

        Dim lblexcursionname As Label = CType(dlItem.FindControl("lblExcursionName"), Label)


        Dim lblexcursiondate As Label = CType(dlItem.FindControl("lblexcursiondate"), Label)

        ToursHdnElineno.Value = lbldlistelineno.Text


        lblToursRemarksheading.Text = lblexcursionname.Text

        lbltoursdate.Text = lblexcursiondate.Text
        Dim dt As New DataTable
        dt = objBLLguest.SetRemarksDetFromDataTable("select  * from booking_tours_remarkstemp  where requestid= '" & GetNewOrExistingRequestId() & "' and rlineno ='" & lbldlistelineno.Text & " 'order by requestid")

        If dt.Rows.Count > 0 Then

            txtToursPartyRemarks.Text = dt.Rows(0).Item("Partyremarks")
            txtToursCustRemarks.Text = dt.Rows(0).Item("agentremarks")
            txtToursArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
            txtToursDeptRemarks.Text = dt.Rows(0).Item("departureremarks")

        End If
    End Sub

    Private Sub cleartextboxes(ByVal txt1 As TextBox, ByVal txt2 As TextBox, ByVal txt3 As TextBox, ByVal txt4 As TextBox)
        txt1.Text = ""
        txt2.Text = ""
        txt3.Text = ""
        txt4.Text = ""
    End Sub
    Sub sendemailtohotel(ByVal partycode As String, ByVal amended As String, ByVal cancelled As String)
        ''' Added shahul 30/06/18
        Dim strfromemailid As String = ""
        Dim to_email As String = "", roemail As String = ""
        Dim strSubject1 As String = ""
        Dim strMessage As String = ""
        Dim strpath1 As String = ""
        Dim requestid As String = ""
        Dim tosendhotelflag As String = ""

        Try

            Dim ds As New DataSet

            '  If PrintDt.Rows.Count > 0 Then

            Dim cnt As Integer = 0
            'For Each printDr As DataRow In PrintDt.Rows
            requestid = hdFinalReqestId.Value ''' Added shahul 30/06/18

            tosendhotelflag = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from agentbooking_notsenthotel(nolock) where agentcode in (select agentcode from booking_header(nolock) where requestid='" & requestid & "')")

            Dim lsShowRateParam As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=2021")
            Dim lbShowRate As Boolean = False
            If lsShowRateParam.Trim.ToUpper = "Y" Then
                lbShowRate = True
            End If

            cnt = cnt + 1
            'Dim partycode = printDr("partycode").ToString()
            'Dim amended As Integer = printDr("amended").ToString()
            'Dim cancelled As Integer = printDr("cancelled").ToString()
            Dim strpop As String
            Dim pop As String = "popup" + cnt.ToString()

            Dim bh As ClsBookingHotelPdf = New ClsBookingHotelPdf()

            Dim bytes As Byte() = {}
            Dim fileName As String = "BookHotel@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            bh.BookingHotelPrint(requestid, partycode, amended, cancelled, bytes, ds, "SaveServer", fileName)
            strpath1 = Server.MapPath("~\SavedReports\") + fileName


            ''' Email Formatting
            ''' 

            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim guestDt As DataTable = ds.Tables(3)
            Dim allotmentdDt As DataTable = ds.Tables(4)
            Dim contactDt As DataTable = ds.Tables(5)

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim URLAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1503")
            Dim RequestDt As DataTable = objclsUtilities.GetDataFromDataTable("select * from booking_hotel_request where requestid='" & requestid & "' and partycode='" & partycode & "'")
            Dim requestDr = (From n In RequestDt.AsEnumerable() Where n.Field(Of String)("partycode") = partycode).FirstOrDefault()
            Dim encryptParam As String = ""
            If Not requestDr Is Nothing Then
                Dim crypto As New clsHotelCryptography()
                Dim combineParam As String = requestid.Trim() + "~" + partycode.Trim() + "~" + amended.ToString() + "~" + cancelled.ToString() + "~" + requestDr("pwdConfirm").ToString().Trim
                encryptParam = crypto.Encrypt(combineParam)
            End If


            Dim dt As New DataTable
            Dim BLLGuest = New BLLGuest
            Dim roomtocheck As Boolean = False






            Dim companyname As String = "", partyname As String = "", status As String = "", contact As String = "", address As String = "", agentuser As String = ""
            Dim confstatus As String = ""
            Dim strQuery As String = ""

            Dim clsEmail As New clsEmail

            strfromemailid = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            confstatus = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1151")

            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")



            Dim guestnames As String = "", offernames As String = "", price As Double
            Dim divcode As String = ""
            If headerDt.Rows.Count > 0 Then
                companyname = headerDt.Rows(0)("division_master_des")
                partyname = headerDt.Rows(0)("partyname")
                address = headerDt.Rows(0)("address1") + " Tel No:" + headerDt.Rows(0)("tel") + " Fax No: " + headerDt.Rows(0)("fax")
                to_email = headerDt.Rows(0)("email1")
                divcode = headerDt.Rows(0)("div_code")
                If Not Session("sEditRequestId") Is Nothing Then
                    If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") Then
                        strSubject1 = IIf(cancelled = "1", "Cancellation Reservation Alert - ", "Amendment Reservation Alert - ") + hdFinalReqestId.Value
                    Else
                        strSubject1 = "New Reservation Alert - " + hdFinalReqestId.Value
                    End If

                Else
                    strSubject1 = "New Reservation Alert - " + hdFinalReqestId.Value
                End If

            End If
            Dim GRcontactno As String = ""
            If divcode = "01" Then
                GRcontactno = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1203")
            Else
                GRcontactno = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1204")
            End If
            Dim salesemail As String = ""
            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                roemail = contactDt.Rows(0)("salesemail")
                salesemail = contactDt.Rows(0)("salesemail")
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                contact = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
            End If
            If guestDt.Rows.Count > 0 Then

                For i = 0 To guestDt.Rows.Count - 1
                    If Val(guestDt.Rows(i)("childage")) <> 0 Then
                        guestnames = guestnames + "/" + guestDt.Rows(i)("guestname") + " ( " + CType(Format(guestDt.Rows(i)("childage"), 0), String) + " Yrs) "
                    Else
                        guestnames = guestnames + "/" + guestDt.Rows(i)("guestname")
                    End If

                Next

                guestnames = Right(guestnames, Len(guestnames) - 1)
            End If
            Dim totalcost As Double
            If hotelDt.Rows.Count > 0 Then
                For i = 0 To hotelDt.Rows.Count - 1
                    totalcost = totalcost + Val(hotelDt.Rows(i)("costvalue"))
                Next
            End If

            If tariffDt.Rows.Count > 0 Then
                For i = 0 To tariffDt.Rows.Count - 1
                    offernames = offernames + "/" + tariffDt.Rows(i)("bookingcode")

                Next
                offernames = Right(offernames, Len(offernames) - 1)
            End If

            strQuery = "select isnull(d.hotelconfno,'') from booking_hotel_detail h, booking_hotel_detail_confcancel d(nolock) where h.requestid=d.requestid and h.rlineno=d.rlineno and  h.requestid='" & hdFinalReqestId.Value & "' and h.partycode='" & partycode & "' and isnull(d.cancelled,0)=0 and isnull(d.hotelconfno,'') <>''"
            Dim hotelconf As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

            If hotelDt.Rows.Count > 0 Then



                strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #3E3F4B; font-size: 12pt;'>Dear Team,</span><span style='font-family:calibri,sans-serif;color:#3E3F4B'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                If cancelled = "1" Then   '' Cancellation Email Text

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Greetings from  <span style='font-weight: bold;'>" + CType(companyname, String) + "  </span> !.</p>"

                    '' Added shahul 04/12/18
                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'><span style='font-weight: bold;text-decoration: underline;'>BOOKING CANCELLATION REQUEST:</span> </p>"



                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We request you to cancel the below booking without any charges.</p>"

                    If hotelconf <> "" Then
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'><span style='font-weight: bold;'>HOTEL CONFIRMATION NO : " + IIf(hotelconf <> "WEBCONF", hotelconf, "") + "</span></p> "
                    End If

                    For i = 0 To hotelDt.Rows.Count - 1

                        Dim guestname As String = ""
                        Dim bookingcode As String = ""
                        dt = BLLGuest.HotelEmailRoomcheck(requestid, partycode, amended, cancelled, hotelDt.Rows(i)("rlineno"))
                        If dt.Rows.Count > 0 Then
                            roomtocheck = True
                        Else
                            roomtocheck = False
                        End If

                        strQuery = "select Isnull((Stuff((Select  '/' + g.title+' '+g.firstname+' '+g.middlename+' '+g.lastname + case when guesttype='Child' then ' ( ' + convert(varchar(10),convert(int,g.childage)) + ' Yrs)' else ''  end  from booking_guest g(nolock)  " _
                            & " where requestid='" & hdFinalReqestId.Value & "' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,1,'')),'') "
                        guestname = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        'strQuery = "select Isnull((Stuff((Select distinct  'and' + g.bookingcode    from booking_hotel_detail_prices g(nolock)  " _
                        '  & " where requestid='" & hdFinalReqestId.Value & "'  and bookingcode<>'' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,4,'')),'') "

                        strQuery = "select Isnull((Stuff((select ' and ' +bookingcode from (SELECT g.bookingcode,pricedate,ROW_NUMBER() over(PARTITION BY   g.bookingcode ORDER BY Pricedate ASC) rowno FROM booking_hotel_detail_prices g(nolock) where requestid='" & hdFinalReqestId.Value & "'   and bookingcode<>'' and rlineno=" & hotelDt.Rows(i)("rlineno") & "  and roomno=" & hotelDt.Rows(i)("roomno") & "  group by bookingcode,Pricedate )vv where rowno=1 order by Pricedate For Xml Path('')),1,4,'')),'')"
                        bookingcode = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)



                        strMessage += "<br />"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Hotel Name        : " + partyname + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Room Type         : " + hotelDt.Rows(i)("rmtypname") + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Meal Plan         : " + hotelDt.Rows(i)("mealcode") + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Name of Pax       : " + guestname + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Offer Confirmed   : " + bookingcode + "</span></p>"

                        'strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Confirmation No   : " + hotelDt.Rows(i)("hotelconfno") + "</span></p>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Cancellation No   : " + hotelDt.Rows(i)("hotelcancelno") + "</span></p>"
                    Next





                Else

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Greetings from  <span style='font-weight: bold;'>" + CType(companyname, String) + "  </span> !.</p>"

                    '' Added shahul 04/12/18
                    If Not Session("sEditRequestId") Is Nothing Then
                        If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") Then
                            'strSubject1 = IIf(cancelled = "1", "Cancellation Reservation Alert - ", "Amendment Reservation Alert - ") + hdFinalReqestId.Value
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'><span style='font-weight: bold;text-decoration: underline;'>BOOKING AMENDMENT REQUEST:</span> </p>"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We have received the below AMENDMENT REQUEST from our agency partner for your hotel.Kindly AMEND & CONFIRM the services as per attached detailed Booking Request form..</p>"

                        Else
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'><span style='font-weight: bold;text-decoration: underline;'>NEW BOOKING REQUEST:</span> </p>"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We have received the below NEW BOOKING REQUEST from our agency partner for your hotel.Kindly Confirm the services as per attached detailed Booking Request form..</p>"
                        End If

                    Else
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'><span style='font-weight: bold;text-decoration: underline;'>NEW BOOKING REQUEST:</span> </p>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We have received the below NEW BOOKING REQUEST from our agency partner for your hotel.Kindly Confirm the services as per attached detailed Booking Request form..</p>"
                    End If

                    If hotelconf <> "" Then
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'><span style='font-weight: bold;text-decoration: underline;'>HOTEL CONFIRMATION NO : " + IIf(hotelconf <> "WEBCONF", hotelconf, "") + "</span></p> "
                    End If



                    'strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Kindly Confirm the services as per attached Booking Request form.</p>"

                    '' Added shahul 04/12/18
                    'If hotelconf <> "" Then
                    '    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>It has already been confirmed to the agency through the availability in our system and the agency has received the prepaid accommodation voucher.</p>"
                    'Else
                    '    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>We request you to send the confirmation for the below booking received from our agency partner.</p>"
                    'End If

                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Please find the  below the summarized booking details:</span></p>"


                    For i = 0 To hotelDt.Rows.Count - 1

                        Dim guestname As String = ""
                        Dim bookingcode As String = ""
                        dt = BLLGuest.HotelEmailRoomcheck(requestid, partycode, amended, cancelled, hotelDt.Rows(i)("rlineno"))
                        If dt.Rows.Count > 0 Then
                            roomtocheck = True
                        Else
                            roomtocheck = False
                        End If

                        strQuery = "select Isnull((Stuff((Select  '/' + g.title+' '+g.firstname+' '+g.middlename+' '+g.lastname + case when guesttype='Child' then ' ( ' + convert(varchar(10),convert(int,g.childage)) + ' Yrs)' else ''  end  from booking_guest g(nolock)  " _
                            & " where requestid='" & hdFinalReqestId.Value & "' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,1,'')),'') "
                        guestname = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        'strQuery = "select Isnull((Stuff((Select distinct  'and' + g.bookingcode    from booking_hotel_detail_prices g(nolock)  " _
                        '  & " where requestid='" & hdFinalReqestId.Value & "'  and bookingcode<>'' and rlineno=" & hotelDt.Rows(i)("rlineno") & " and roomno=" & hotelDt.Rows(i)("roomno") & " For Xml Path('')),1,4,'')),'') "

                        strQuery = "select Isnull((Stuff((select ' and ' +bookingcode from (SELECT g.bookingcode,pricedate,ROW_NUMBER() over(PARTITION BY   g.bookingcode ORDER BY Pricedate ASC) rowno FROM booking_hotel_detail_prices g(nolock) where requestid='" & hdFinalReqestId.Value & "'   and bookingcode<>'' and rlineno=" & hotelDt.Rows(i)("rlineno") & "  and roomno=" & hotelDt.Rows(i)("roomno") & "  group by bookingcode,Pricedate )vv where rowno=1 order by Pricedate For Xml Path('')),1,4,'')),'')"

                        bookingcode = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                        If roomtocheck = True Then

                            strMessage += "<br />"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Booking Time      : " + Now + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Hotel Name        : " + partyname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Room Type         : " + hotelDt.Rows(i)("rmtypname") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Meal Plan         : " + hotelDt.Rows(i)("mealcode") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Accommodation Type: " + hotelDt.Rows(i)("roomdetails") + "</span></p>"
                            ''Added shahul 30/07/18
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Check In Date: " + hotelDt.Rows(i)("Checkin") + "</span></p>"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Check Out Date: " + hotelDt.Rows(i)("Checkout") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Name        : " + guestname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Nationality : " + IIf(guestnames <> "", guestDt.Rows(0)("nationality"), "") + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Offer Name        : " + bookingcode + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>No.Of.Rooms        : " + CType(hotelDt.Rows.Count, String) + "</span></p>"

                            ''// Hide value temporarily for VAT implementation
                            If lbShowRate = True Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Total Reservation Price: " + headerDt.Rows(0)("costcurrcode") + " " + CType(totalcost, String) + "</span></p>"
                            End If
                            '' Added shahul 15/08/2018
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Special Requests       : " + hotelDt.Rows(i)("hotelremarks") + "</span></p>"

                            Exit For
                        Else
                            strMessage += "<br />"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Booking Time      : " + Now + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Hotel Name        : " + partyname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Room Type         : " + hotelDt.Rows(i)("rmtypname") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Meal Plan         : " + hotelDt.Rows(i)("mealcode") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Accommodation Type: " + hotelDt.Rows(i)("roomdetails") + "</span></p>"

                            ''Added shahul 30/07/18
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Check In Date: " + hotelDt.Rows(i)("Checkin") + "</span></p>"
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Check Out Date: " + hotelDt.Rows(i)("Checkout") + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Name        : " + guestname + "</span></p>"

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Guest Nationality : " + IIf(guestnames <> "", guestDt.Rows(0)("nationality"), "") + "</span></p> "

                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Offer Name        : " + bookingcode + "</span></p>"

                            ''// Hide value temporarily for VAT implementation
                            If lbShowRate = True Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Total Reservation Price: " + headerDt.Rows(0)("costcurrcode") + " " + CType(hotelDt.Rows(i)("costvalue"), String) + "</span></p>"
                            End If

                            '' Added shahul 15/08/2018
                            strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Special Requests       : " + hotelDt.Rows(i)("hotelremarks") + "</span></p>"
                        End If



                    Next
                    '' Added shahul 04/12/18
                    If hotelconf <> "" Then
                        strMessage += "<br />"
                        strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Note :</span></p>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>It has already been confirmed to the agency through the availability in our system and the agency has received the prepaid accommodation voucher.</p>"
                    End If

                    If cancelled = 0 Then
                        strMessage += "<br />"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B;font-weight:bold'>Please visit the following link to confirm the booking :</span><br/><a href='" + URLAddress + "/BookingHotelConfirm.aspx?ids=" + encryptParam + "'>" + URLAddress + "/BookingHotelConfirm.aspx?ids=" + encryptParam + "</a></p>"
                    End If

                    ' strMessage += "<br />"
                    ' strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Please find attached herewith PDF document for  further details </span></p>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Also please note : </span></p>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoListParagraph' style='text-indent:-.25in'><span style='font-family:symbol'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ·</span><span style='font-size:5.0pt;font-family:times new roman,serif'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family: calibri, arial, helvetica, sans-serif;'>Payment has been guaranteed by " + CType(companyname, String) + " as per the booking and credit terms.</span>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoListParagraph' style='text-indent:-.25in'><span style='font-family:symbol'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ·</span><span style='font-size:5.0pt;font-family:times new roman,serif'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family: calibri, arial, helvetica, sans-serif;'>Rates are confidential and not to be disclosed to the clients.</span>"
                    strMessage += "<br />"
                    strMessage += "<p class='MsoListParagraph' style='text-indent:-.25in'><span style='font-family:symbol'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ·</span><span style='font-size:5.0pt;font-family:times new roman,serif'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><span style='font-family: calibri, arial, helvetica, sans-serif;'>Deposits / Extras / Tourism Dirham under the account of the guest and to be collected directly as per hotel policy.</span>"

                    strMessage += "<br /><br />"
                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Contact details: </span></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Reservation  : " + contact + "<" + roemail + ">" + " </span></p>" ' <p class='MsoNormal' style='margin: 0'><font face='Calibri,sans-serif'>&nbsp;</font></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>During guest(s) stay: In case of any Emergency Please contact Guest Relation Officer ( " + GRcontactno + " ) </span></p>"
                    strMessage += "<br /><br />"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Other Contact Email ID's:</span></p>"
                    '' Added shahul 04/12/18
                    strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>For Availability Updates  :<span style='font-family: calibri, arial, helvetica, sans-serif; color: #0033ff; text-decoration: underline;'>info@mahce.com</span></span></p>"
                    strMessage += "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>For Rate related updates  :<span style='font-family: calibri, arial, helvetica, sans-serif; color: #0033ff; text-decoration: underline;'>info@mahce.com</span></span></p>"
                    strMessage += "<br /><br />"
                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Invoice address:" + companyname + "</span></p>"

                    strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>" + address + "</span></p>"

                End If

                strMessage += "<br /><br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>Best Regards,</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>" + contact + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#3E3F4B'>" + companyname + "</span></p>"

            End If


            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentOnline
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentOnline
            End If


            If to_email = "" Then
                If Emailmode = "Test" Then
                    to_email = testEmail
                    roemail = testEmail
                Else
                    to_email = strfromemailid  '''  if Hotel Email Blank it will take Admin

                    roemail = roemail + "," + ccemails
                End If
            Else
                If Emailmode = "Test" Then
                    to_email = testEmail
                    roemail = testEmail
                Else
                    ' to_email = strfromemailid
                    If roemail = "" Then    '''  if RO Email Blank it will take Admin
                        roemail = strfromemailid + "," + ccemails
                    Else
                        roemail = roemail + "," + ccemails
                    End If

                End If
            End If

            ''Added shahul  27/05/2018
            Dim defaultusername As String = "", defaultpwd As String = ""
            Dim strfromusername As String = "", strfrompwd As String = ""

            defaultusername = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1504")
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) option_value from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='" & salesperson & "'")
            If emaildt.Rows.Count > 0 Then
                'strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                'strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))
                'Modified by abin on 20181212
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Or Emailmode = "Test" Then
                    strfromusername = defaultusername
                    strfrompwd = defaultpwd
                Else
                    strfromusername = emaildt.Rows(0)("emailusername")
                    strfrompwd = emaildt.Rows(0)("emailpwd")
                End If
            Else
                strfromusername = defaultusername
                strfrompwd = defaultpwd
            End If


            If chkEmailToHotel.Checked = False Then    '' Send email to RO untick send mail to hotel     ---param 20/10/2018
                tosendhotelflag = "BookingEmailToRO"
                If Emailmode = "Test" Then
                    to_email = testEmail
                    roemail = testEmail
                Else
                    to_email = salesemail
                    If to_email = "" Then to_email = strfromemailid
                    roemail = ccemails
                End If
                If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, roemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_HOTEL")
                Else
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                End If
            ElseIf tosendhotelflag <> "" Then
                to_email = strfromemailid
                roemail = salesemail


                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    to_email = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                End If


                If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, roemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_HOTEL")
                Else
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                End If
            Else
                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    to_email = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                    If Session("sLoginType") = "RO" Then
                        Dim lsROEmailId As String
                        lsROEmailId = objclsUtilities.ExecuteQueryReturnStringValue("select usemail from usermaster (nolock) where usercode='" & Session("GlobalUserName") & "'")
                        roemail = IIf(lsROEmailId.Trim = "", to_email, lsROEmailId.Trim)
                        strfromemailid = roemail
                    End If
                End If

                If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, roemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_HOTEL")
                Else
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
                End If
            End If




            '    Next




            'End If


        Catch ex As Exception
            ModalPopupDays.Hide()
            '*** Danny Commented
            'If tosendhotelflag = "" Then
            '   objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, roemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_HOTEL")
            'End If
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: SendemailHotel :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Sub sendemail(ByVal emailstatus As String)
        Try

            Dim bc As clsBookingConfirmationPdf = New clsBookingConfirmationPdf()
            Dim requestid = hdFinalReqestId.Value
            Dim objclsUtilities As New clsUtilities

            Dim ds As New DataSet
            Dim strpath1 As String = ""
            Dim bytes As Byte() = {}
            Dim ResParam As New ReservationParameters
            ResParam = Session("sobjResParam")
            Dim fileName As String = requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + requestid + "'")
            If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
            If chkCumulative.Trim() = "CUMULATIVE" Then
                bc.GenerateCumulativeReport(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            Else
                bc.GenerateReport(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            End If
            'Dim clsServiceReport As clsBookingServiceReport = New clsBookingServiceReport()
            'fileName = "SR_" + fileName
            'clsServiceReport.GenerateServiceReport_SR(requestid, bytes, ds, "SaveServer", ResParam, fileName) '*** Danny 25/09/2018

            strpath1 = Server.MapPath("~\SavedReports\") + fileName
            Session("sobjResParam") = ResParam

            If chkCumulative.Trim() = "CUMULATIVE" Then
                fileName = "Itinerary@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
                fileName = fileName.Replace("/", "-")
                Dim Itinerary As clsItineraryPdf = New clsItineraryPdf()
                bytes = {}
                Itinerary.GenerateItineraryReport(requestid, bytes, "SaveServer", fileName)
                If strpath1 = "" Then
                    strpath1 = Server.MapPath("~\SavedReports\") + fileName
                Else
                    strpath1 = strpath1 + ";" + Server.MapPath("~\SavedReports\") + fileName
                End If
            End If


            ''' Email Formatting
            ''' 

            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim othServDt As DataTable = ds.Tables(3)
            Dim airportDt As DataTable = ds.Tables(4)
            Dim visaDt As DataTable = ds.Tables(5)
            Dim tourDt As DataTable = ds.Tables(6)
            Dim OtherDt As DataTable = ds.Tables(7)
            Dim guestDt As DataTable = ds.Tables(8)
            Dim contactDt As DataTable = ds.Tables(9)
            Dim BankDt As DataTable = ds.Tables(10)
            Dim dtGuestDetails As DataTable = ds.Tables(12)

            Dim strMessage As String = ""
            Dim AgentName As String = "", agentref As String = "", status As String = "", agentcontact As String = "", agentemail As String = "", agentuser As String = ""
            Dim confstatus As String = ""
            Dim clsEmail As New clsEmail
            Dim strQuery As String = ""

            Dim strfromemailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            confstatus = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=1151")

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")
            Dim timelimit As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=528")
            timelimit = CType(Val(timelimit) * 24, String)


            Dim to_email As String = ""

            Dim revertmsg As String = "(Please revert within" & confstatus & " hours)"
            Dim strSubject1 As String = ""
            Dim bookingvalue As String = ""
            Dim divcode As String = ""

            If headerDt.Rows.Count > 0 Then
                AgentName = headerDt.Rows(0)("agentname")
                agentref = headerDt.Rows(0)("agentref")
                agentcontact = headerDt.Rows(0)("agentcontact")
                ResParam = Session("sobjResParam")
                If ResParam.LoginType <> "RO" And ResParam.WhiteLabel = "1" Then
                    agentemail = headerDt.Rows(0)("agentandsubemail")
                Else
                    agentemail = headerDt.Rows(0)("agentemail")
                End If



                agentuser = headerDt.Rows(0)("webusername")
                divcode = headerDt.Rows(0)("div_code")
                bookingvalue = CType(headerDt.Rows(0)("salevalue"), String)
                If Emailmode = "Test" Then
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmed", "On Request")
                Else
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmed", "On Request")
                End If

                If Emailmode = "Test" Then
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmation(" & hdFinalReqestId.Value & ")", "Request(" & hdFinalReqestId.Value & ")")
                Else
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmation(" & hdFinalReqestId.Value & ")", "Request(" & hdFinalReqestId.Value & ")")
                End If

            End If

            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            Dim salesemail As String = ""
            If contactDt.Rows.Count > 0 Then
                to_email = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                salesemail = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                '   strMessage = "Dear " + contactDt.Rows(0)("salesperson") + "," + "&nbsp;<br /><br />"
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                If Session("sLoginType") = "RO" Then
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We Cancelled the attached  booking .</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'>" + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                        If emailstatus = "Amended" Then
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the Attached revised invoice as requested by You and   Details of the booking is as follows:</span></p>"
                        Else
                            If status = "Confirmed" Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following booking  and the same has been <span style='font-weight: bold;color:#1E8449;'>" & status & " </span> . Details of the booking is as follows:</span></p>"
                            Else
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following booking  and the same has been <span style='font-weight: bold;color:#F72A0A;'>" & status & " </span> . Details of the booking is as follows:</span></p>"
                            End If

                        End If


                    End If


                Else
                    Dim contact As String = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You Received the  Cancelled Booking from   <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> .Please check the Attached Booking Ref.</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You have received the following booking from  <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> . Details of the booking is as follows:</span></p>"
                    End If

                End If

            End If



            ' strMessage += "You have received the following booking from " + CType(hdFinalReqestId.Value, String) + " and the same has been confirmed / (on request). Details of the booking is as follows:  <br /><br />"






            'strMessage += "Agent Name : " & AgentName & "&nbsp;<br />"
            'strMessage += "Agency Reference Number : " & agentref & "&nbsp;<br />"

            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agent Name : " + AgentName + "</span></p>"
            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agency Reference Number : " + agentref + "</span></p> <p class='MsoNormal' style='margin: 0'><font face='Calibri,sans-serif'>&nbsp;</font></p>"




            If hotelDt.Rows.Count > 0 Then

                strMessage += " <br /><table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Hotel Name</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check In</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check Out</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Meal Plan</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Lead Guest</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Confirmation Status</th></tr><tr> "

                For i = 0 To hotelDt.Rows.Count - 1

                    strQuery = "select  top 1 title+ ' ' + firstname + ' ' + isnull(middlename,'') + ' ' + isnull(g.lastname,'') guestname   from booking_guest g(nolock),booking_hotel_detail d(nolock) ,booking_hotel_detail_confcancel c(nolock) " _
                        & "  where g.requestid=d.requestid  and g.rlineno=d.rlineno and  d.requestid=c.requestid and d.rlineno =c.rlineno and isnull(c.cancelled,0)=0 and g.roomno=c.roomno  " _
                        & "  and g.requestid='" & requestid & "' and g.rlineno =" & hotelDt.Rows(i)("rlineno") & "  and g.roomno=" & hotelDt.Rows(i)("roomno")
                    Dim leadguest As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("partyname") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkin") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkout") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("roomdetail") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + leadguest + "</td>"
                    If hotelDt.Rows(i)("hotelconfno") <> "" Then
                        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>YES</td></tr>"
                    Else
                        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>NO " + revertmsg + "</td></tr>"
                    End If


                Next
                strMessage += "</table>"
            End If

            If dtGuestDetails.Rows.Count > 0 Then
                strMessage = strMessage + "<br /><p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'><b>Flight Details</b></span></p> "
                strMessage += " <table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>No</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Guest Name</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Arrival Date</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Code</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Time</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Arrival</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Departure Date</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Code</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Time</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Departure</th></tr><tr> "

                For i = 0 To dtGuestDetails.Rows.Count - 1

                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + (i + 1).ToString + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("GuestName") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrdate") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrflightcode") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrflighttime") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arairportbordername") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depdate") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depflightcode") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depflighttime") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depairportbordername") + "</td> </tr>"
                Next
                strMessage += "</table>"
            End If





            If visaDt.Rows.Count > 0 Or othServDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                strMessage += "<br />"
                '  strMessage += "Please find the details of other services booked:" & "&nbsp;<br /><br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the details of other services booked:</span></p>"

            End If
            strMessage += "<br />"
            If visaDt.Rows.Count > 0 Then
                '    Dim visaconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_visa_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(visaconfno,'') <> '' ")
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>VISA            :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If

            If othServDt.Rows.Count > 0 Then
                ' Dim trfconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_transfers_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(transferconfno,'') <> '' ")

                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TRANSFERS       :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If
            If airportDt.Rows.Count > 0 Then
                '  Dim airconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_airportmate_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(airportmateconfno,'') <> '' ")

                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>AIRPORT SERVICES:" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If
            If tourDt.Rows.Count > 0 Then
                ' Dim tourconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_tours_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(toursconfno,'') <> '' ")

                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TOURS           :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If
            If OtherDt.Rows.Count > 0 Then
                '  Dim tourconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from booking_others_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(othersconfno,'') <> '' ")
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>OTHER SERVICES  :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
            End If

            strMessage += "  <br /><br />"
            '  strMessage += "<br />Best Regards,<br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Best Regards,</span></p>"
            ' strMessage += "<br />AgentOnline software admin team<br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>MAHCE  software admin team</span></p>"




            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentOnline --*** Hotel CC emails 01 & 01
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentOnline --*** Hotel CC emails 01 & 01
            End If



            If agentemail = "" Then
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else
                    agentemail = agentemail + "," + ccemails
                    to_email = IIf(to_email = "", strfromemailid, to_email)
                End If
            Else
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else

                    agentemail = agentemail + "," + ccemails
                    to_email = IIf(to_email = "", strfromemailid, to_email)  ''' RO Email Blank it will take admin Email
                End If
            End If

            ''Added shahul  27/05/2018
            Dim defaultusername As String = "", defaultpwd As String = ""
            Dim strfromusername As String = "", strfrompwd As String = ""

            defaultusername = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1504")
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) option_value from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='" & salesperson & "'")
            If emaildt.Rows.Count > 0 Then
                'strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                'strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))
                'Modified by abin on 20181212
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Or Emailmode = "Test" Then
                    strfromusername = defaultusername
                    strfrompwd = defaultpwd
                Else
                    strfromusername = emaildt.Rows(0)("emailusername")
                    strfrompwd = emaildt.Rows(0)("emailpwd")
                End If
            Else
                strfromusername = defaultusername
                strfrompwd = defaultpwd
            End If



            '*** Service Voucher for RO Danny 30/09/2018
            Dim SR_fileName As String = ""
            Dim attachSRV As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=73")


            If Session("sLoginType") = "RO" And attachSRV = "Y" Then
                Dim clsBookingService As New clsBookingServicerVoucher
                SR_fileName = "SR_" + requestid.Replace("/", "") + ".PDF"
                Dim bytes4 As Byte() = {}
                Dim SRds1 As New DataSet
                Dim SRResParam As ReservationParameters = Session("sobjResParam")
                SR_fileName = clsBookingService.GenerateServiceReport_SR(requestid, bytes4, SRds1, "SaveServer", SRResParam, SR_fileName)
                If SR_fileName.Trim().Length > 0 Then
                    SR_fileName = Server.MapPath("~\SavedReports\") + SR_fileName
                End If
            End If

            'Exit Sub '*** Need to Remove
            '*** Danny 19/09/2018
            If chkEmailToAgent.Checked = False Then       ''' Modified param 21/10/2018
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else
                    agentemail = ccemails
                    to_email = IIf(salesemail = "", strfromemailid, salesemail)  ''' RO Email Blank it will take admin Email
                End If
                If clsEmail.SendEmailOnlinenew(strfromemailid, agentemail, to_email, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                Else
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                End If
            ElseIf objclsUtilities.fn_NeedToSendAgentEmail() Then 'This Condition is added By Mohamed 'changed by mohamed on 28/06/2018
                If chkEmailToAgent.Checked = True Then
                    'If Amended = True Then
                    '    sendemail("Amended")
                    'Else
                    '    sendemail("New")
                    'End If

                    If Session("sLoginType") <> "RO" Then

                        If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                            agentemail = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                        End If

                        If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, agentemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                            objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                        Else
                            objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                        End If
                    Else
                        If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                            agentemail = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                            If Session("sLoginType") = "RO" Then
                                Dim lsROEmailId As String
                                lsROEmailId = objclsUtilities.ExecuteQueryReturnStringValue("select usemail from usermaster (nolock) where usercode='" & Session("GlobalUserName") & "'")
                                to_email = IIf(lsROEmailId.Trim = "", agentemail, lsROEmailId.Trim)
                                strfromemailid = to_email
                            End If
                        End If

                        '*** Service Doc Attaching for RO
                        If SR_fileName.Length > 0 And Session("sLoginType") = "RO" Then
                            If strpath1.Length > 0 Then
                                strpath1 = strpath1 + ";"
                            End If
                            strpath1 = strpath1 + SR_fileName
                        End If
                        If clsEmail.SendEmailOnlinenew(strfromemailid, agentemail, to_email, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                            objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                        Else
                            objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                        End If
                    End If
                End If
                objclsUtilities.SaveEmailLog(Session("sRequestId").ToString, "Agent", "", "0", "0", IIf(chkEmailToAgent.Checked, "1", "0"), IIf(chkEmailToHotel.Checked, "1", "0"), Session("GlobalUserName").ToString)
            Else '*** Email to operations with details of the booking

                '*** Service Doc Attaching for RO
                If SR_fileName.Length > 0 And Session("sLoginType") = "RO" Then
                    If strpath1.Length > 0 Then
                        strpath1 = strpath1 + ";"
                    End If
                    strpath1 = strpath1 + SR_fileName
                End If
                If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, "", strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                Else
                    objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                End If


            End If


            '*** Number of days to service date
            Dim strOperationsNoDays As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=66")

            '*** Operations email ids
            Dim strOperationsEmailIDs As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=67")

            Dim strReturnstring As String = objclsUtilities.GetNumberofdaystoservicedate(requestid)

            If Not strReturnstring Is Nothing Then
                If strReturnstring.Trim().Length > 0 Then
                    If Not strOperationsEmailIDs Is Nothing Then
                        If clsEmail.SendEmailOnlinenew(strfromemailid, strOperationsEmailIDs, "", "Short Service Date-[" + strReturnstring + "]" + strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                            objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, strOperationsEmailIDs, "", "", "Short Service Date-[" + strReturnstring + "]" + strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_OPERATION")
                        Else
                            objclsUtilities.SendEmailNotification("BOOKING", strfromemailid, strOperationsEmailIDs, "", "", "Short Service Date-[" + strReturnstring + "]" + strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_OPERATION")
                        End If

                        objclsUtilities.SaveEmailLog(Session("sRequestId").ToString, "Operations", "", "0", "0", IIf(chkEmailToAgent.Checked, "1", "0"), IIf(chkEmailToHotel.Checked, "1", "0"), Session("GlobalUserName").ToString)
                    End If
                End If
            End If
        Catch ex As Exception
            ModalPopupDays.Hide()
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: Sendemail :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Sub final_save()
        Dim strrequestid As String = ""
        Dim dt As DataTable
        Dim divcode As String = ""
        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
        If dt.Rows.Count > 0 Then
            objBLLguest.GBdivcode = dt.Rows(0)("div_code").ToString
            objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
            objBLLguest.GBAgentcode = dt.Rows(0)("agentcode").ToString
            objBLLguest.GBAgentsourcectrycode = dt.Rows(0)("sourcectrycode").ToString
            objBLLguest.GBLogintype = Session("sLoginType")
            objBLLguest.GBuserlogged = Session("GlobalUserName") ' dt.Rows(0)("adduser").ToString
            objBLLguest.GBAgentrefno = txtAgentReference.Text
            objBLLguest.GBColumbusref = txtColumbusReference.Text

        End If

        If Not Session("sEditRequestId") Is Nothing Then
            objBLLguest.GBBookmode = "EDIT"

            If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Or Session("sEditRequestId").ToString.Contains("TP/") Then
                objBLLguest.GBBookmode = "NEW"
            Else
                Dim isBooking As String = objclsUtilities.ExecuteQueryReturnStringValue("select count(*)cnt from booking_header(nolock) where requestid='" + Session("sEditRequestId") + "'")
                If isBooking = 0 Then
                    objBLLguest.GBBookmode = "NEW"
                End If
            End If

        End If

        strrequestid = objBLLguest.FinalSaveBooking()
        If strrequestid <> "" Then
            hdFinalReqestId.Value = strrequestid
            If objBLLguest.GBBookmode = "EDIT" Then
                MessageBox.ShowMessage(Page, MessageType.Success, "Booking Amended  " + strrequestid)
            Else
                MessageBox.ShowMessage(Page, MessageType.Success, "Booking Created  " + strrequestid)
            End If



            btnSubmitBooking.Style.Add("display", "none")
            divabondon.Style.Add("display", "none")
            divprintbook.Style.Add("display", "block")
            divnewbook.Style.Add("display", "block")
            'btnAbondon.Style.Add("display", "none")
            ' btnprint.Style.Add("display", "block")
            divaccept.Style.Add("display", "none")
            dlFlightDetails.Enabled = False
            dldeparturedetails.Enabled = False
            btnAddflight.Style.Add("display", "none")
            btnAddDepflight.Style.Add("display", "none")
            ' btnnewbooking.Style.Add("display", "block")
            Session("sFinalBooked") = "1"
            LoadMenus() 'changed by mohamed on 12/08/2018
            txtAgentReference.Enabled = False
            dlPersonalInfo.Enabled = False
            btnAdd.Style.Add("display", "none")
            btnAddchd.Style.Add("display", "none")
            btnGenerateFlightDetails.Style.Add("display", "none")

            '' Email Sent hotel & Agent 'changed by mohamed on 11/08/2020 'commented email part
            'Try
            '    hotelmail()
            'Catch ex As Exception
            '    objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: final_save :: Sending email part :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            'End Try


            If hdFinalReqestId.Value <> "" Then
                If objBLLguest.GBBookmode = "EDIT" Then
                    lblheader.Text = "Booking - " + hdFinalReqestId.Value + " has been  Modified  Email has been Sent to Concern Person's "
                    dvhotnoshow.Style.Add("display", "block")
                Else
                    lblheader.Text = "  New Booking - " + hdFinalReqestId.Value + " Generated Email has been Sent to Concern Person's "
                    dvhotnoshow.Style.Add("display", "block")
                End If

            End If

        Else
            MessageBox.ShowMessage(Page, MessageType.Errors, "Booking Failed  Please Submit Again ")

            btnSubmitBooking.Style.Add("display", "block")
            divabondon.Style.Add("display", "block")
            divprintbook.Style.Add("display", "none")
            divnewbook.Style.Add("display", "none")

            'btnAbondon.Style.Add("display", "block")
            'btnprint.Style.Add("display", "none")
            divaccept.Style.Add("display", "none")
            dlFlightDetails.Enabled = True
            dldeparturedetails.Enabled = True
            '  btnnewbooking.Style.Add("display", "none")
            Session("sFinalBooked") = Nothing '"0" 'changed by mohamed on 30/08/2018
            txtAgentReference.Enabled = True
            dlPersonalInfo.Enabled = True
            btnAdd.Style.Add("display", "block")
            btnAddchd.Style.Add("display", "block")
            btnGenerateFlightDetails.Style.Add("display", "block")
        End If

    End Sub

    Private Function SaveArrivalflightdetails() As Boolean

        SaveArrivalflightdetails = False
        Dim dt As DataTable
        'Dim btnAppArrival As Button = CType(sender, Button)
        'Dim dlItem As DataListItem = CType((btnAppArrival).NamingContainer, DataListItem)

        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        Dim strQuery As String = ""

        If ValidateArrivalFlightDetails() Then



            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))


            '' Arrival Flights
            If dt.Rows.Count > 0 Then
                objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                For Each item As DataListItem In dlFlightDetails.Items

                    'For Each item As DataListItem In dlFlightDetails.Items

                    Dim strTittle As String = ""

                    ' If item.ItemIndex = dlItem.ItemIndex Then

                    Dim ChkFlightNotRequired As CheckBox = CType(item.FindControl("ChkFlightNotRequired"), CheckBox)
                    If ChkFlightNotRequired.Checked = False Then

                        Dim txtArrivalDate As TextBox = CType(item.FindControl("txtArrivalDate"), TextBox)
                        Dim txtArrivalflight As TextBox = CType(item.FindControl("txtArrivalflight"), TextBox)
                        Dim txtArrivalTime As TextBox = CType(item.FindControl("txtArrivalTime"), TextBox)
                        Dim txtArrivalAirport As TextBox = CType(item.FindControl("txtArrivalAirport"), TextBox)
                        Dim txtArrivaltoairport As TextBox = CType(item.FindControl("txtArrivaltoairport"), TextBox)
                        Dim chkguest As CheckBoxList = CType(item.FindControl("chkguest"), CheckBoxList)
                        Dim txtArrivalflightCode As TextBox = CType(item.FindControl("txtArrivalflightCode"), TextBox)
                        Dim txtArrBordecode As TextBox = CType(item.FindControl("txtArrBordecode"), TextBox)
                        Dim btnAppArrival1 As Button = CType(item.FindControl("btnAppArrival"), Button)

                        Dim chkNA As CheckBox = CType(item.FindControl("chkNA"), CheckBox)
                        Dim divreason As HtmlGenericControl = CType(item.FindControl("divreason"), HtmlGenericControl)
                        Dim txtreason As TextBox = CType(item.FindControl("txtreason"), TextBox)


                        If chkguest.Items.Count > 0 Then
                            Dim k As Integer = 1
                            For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                                If chkitem.Selected = True Then

                                    Dim Arrdate As String = txtArrivalDate.Text
                                    If Arrdate <> "" Then
                                        Dim strDates As String() = Arrdate.Split("/")
                                        Arrdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                                    End If

                                    Dim flighttranid As String() = txtArrivalflightCode.Text.Split("|")

                                    '    Dim guestline As String() = chkitem.Value.Split(";")

                                    Dim strGuestValues As String() = chkitem.Value.Split("|")
                                    For i As Integer = 0 To strGuestValues.Length - 1

                                        Dim guestline As String() = strGuestValues(i).Split(";")


                                        strBuffer.Append("<DocumentElement>")

                                        If chkNA.Checked = True Then
                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                            strBuffer.Append("<arrivaldate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</arrivaldate>")
                                            strBuffer.Append(" <flightcode></flightcode>")
                                            strBuffer.Append(" <flight_tranid></flight_tranid>")
                                            strBuffer.Append(" <flighttime></flighttime>")
                                            strBuffer.Append(" <arraiport></arraiport>")
                                            strBuffer.Append(" <originaiport></originaiport>")
                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                            strBuffer.Append(" <NAReason>" & txtreason.Text & "</NAReason>")
                                            strBuffer.Append(" <NAticked>1</NAticked>")
                                        Else
                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                            strBuffer.Append("<arrivaldate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</arrivaldate>")
                                            strBuffer.Append(" <flightcode>" & CType(txtArrivalflight.Text, String) & "</flightcode>")
                                            strBuffer.Append(" <flight_tranid>" & CType(flighttranid(0), String) & "</flight_tranid>")
                                            strBuffer.Append(" <flighttime>" & CType(txtArrivalTime.Text, String) & "</flighttime>")
                                            strBuffer.Append(" <arraiport>" & CType(txtArrBordecode.Text, String) & "</arraiport>")
                                            strBuffer.Append(" <originaiport>" & CType(txtArrivalAirport.Text, String) & "</originaiport>")
                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                            strBuffer.Append(" <NAReason>" & txtreason.Text & "</NAReason>")
                                            strBuffer.Append(" <NAticked>0</NAticked>")
                                        End If


                                        strBuffer.Append("</DocumentElement>")
                                    Next
                                End If
                            Next
                            ' Dim btnAppArrival1 As Button = CType(dlFlightDetails.Items(dlItem.ItemIndex).FindControl("btnAppArrival"), Button)
                            '   btnAppArrival1.Text = "Selected"
                        End If
                    End If
                    ' End If
                Next


                objBLLguest.GBFlightXml = strBuffer.ToString
                objBLLguest.GBuserlogged = Session("GlobalUserName")

                If objBLLguest.SavingArrivalFlightTemp(True) Then 'changed by mohamed on 24/09/2018
                    SaveArrivalflightdetails = True
                End If
            Else
                SaveArrivalflightdetails = False
            End If


        End If
    End Function
    Private Function SaveDepartureflightdetails() As Boolean
        Dim dt As DataTable
        SaveDepartureflightdetails = False

        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        Dim strQuery As String = ""

        If ValidateDepartureFlightDetails() Then


            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))

            '' Departure Flights
            If dt.Rows.Count > 0 Then
                objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                For Each item As DataListItem In dldeparturedetails.Items
                    'For Each item As DataListItem In dldeparturedetails.Items

                    Dim chkDepFlightNotrquired As CheckBox = CType(item.FindControl("chkDepFlightNotrquired"), CheckBox)
                    If chkDepFlightNotrquired.Checked = False Then



                        Dim strTittle As String = ""

                        '  If item.ItemIndex = dlItem.ItemIndex Then

                        Dim txtDepartureDate As TextBox = CType(item.FindControl("txtDepartureDate"), TextBox)
                        Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)
                        Dim txtDepartureTime As TextBox = CType(item.FindControl("txtDepartureTime"), TextBox)
                        Dim txtDepartureAirport As TextBox = CType(item.FindControl("txtDepartureAirport"), TextBox)
                        Dim txtDeparturetoAirport As TextBox = CType(item.FindControl("txtDeparturetoAirport"), TextBox)
                        Dim chkguest As CheckBoxList = CType(item.FindControl("chkguest"), CheckBoxList)
                        Dim txtDepartureFlightCode As TextBox = CType(item.FindControl("txtDepartureFlightCode"), TextBox)
                        Dim txtDepBordecode As TextBox = CType(item.FindControl("txtDepBordecode"), TextBox)

                        Dim chkdepNA As CheckBox = CType(item.FindControl("chkdepNA"), CheckBox)
                        Dim divdepreason As HtmlGenericControl = CType(item.FindControl("divdepreason"), HtmlGenericControl)
                        Dim txtdepreason As TextBox = CType(item.FindControl("txtdepreason"), TextBox)

                        If chkguest.Items.Count > 0 Then
                            Dim k As Integer = 1
                            For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                                If chkitem.Selected = True Then

                                    Dim Arrdate As String = txtDepartureDate.Text
                                    If Arrdate <> "" Then
                                        Dim strDates As String() = Arrdate.Split("/")
                                        Arrdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                                    End If

                                    Dim flighttranid As String() = txtDepartureFlightCode.Text.Split("|")

                                    Dim strGuestValues As String() = chkitem.Value.Split("|")
                                    For i As Integer = 0 To strGuestValues.Length - 1

                                        Dim guestline As String() = strGuestValues(i).Split(";")

                                        strBuffer.Append("<DocumentElement>")
                                        If chkdepNA.Checked = True Then
                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                            strBuffer.Append("<departuredate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</departuredate>")
                                            strBuffer.Append(" <flightcode></flightcode>")
                                            strBuffer.Append(" <flight_tranid></flight_tranid>")
                                            strBuffer.Append(" <flighttime></flighttime>")
                                            strBuffer.Append(" <depaiport></depaiport>")
                                            strBuffer.Append(" <originaiport></originaiport>")
                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                            strBuffer.Append(" <NAReason>" & txtdepreason.Text & "</NAReason>")
                                            strBuffer.Append(" <NAticked>1</NAticked>")
                                        Else
                                            strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                            strBuffer.Append("<departuredate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</departuredate>")
                                            strBuffer.Append(" <flightcode>" & CType(txtDepartureFlight.Text, String) & "</flightcode>")
                                            strBuffer.Append(" <flight_tranid>" & CType(flighttranid(0), String) & "</flight_tranid>")
                                            strBuffer.Append(" <flighttime>" & CType(txtDepartureTime.Text, String) & "</flighttime>")
                                            strBuffer.Append(" <depaiport>" & CType(txtDepBordecode.Text, String) & "</depaiport>")
                                            strBuffer.Append(" <originaiport>" & CType(txtDeparturetoAirport.Text, String) & "</originaiport>")
                                            strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                            strBuffer.Append(" <NAReason>" & txtdepreason.Text & "</NAReason>")
                                            strBuffer.Append(" <NAticked>0</NAticked>")
                                        End If

                                        strBuffer.Append("</DocumentElement>")
                                    Next
                                End If
                            Next

                        End If
                        ' End If
                    End If
                Next
                objBLLguest.GBDepFlightXml = strBuffer.ToString
                objBLLguest.GBuserlogged = Session("GlobalUserName")

                If objBLLguest.SavingDepartureFlightTemp() Then
                    SaveDepartureflightdetails = True
                End If

            Else
                SaveDepartureflightdetails = False
            End If


        Else
            SaveDepartureflightdetails = False
        End If
    End Function

    'changed by mohamed on 11/09/2018
    Function fnGetGuestName() As String

        Dim strBuffer As New Text.StringBuilder

        Dim ddlTitle As DropDownList
        Dim lblRowNoAll As Label
        Dim lblrlineno As Label
        Dim txtFirstNameg As TextBox
        Dim txtMidName As TextBox
        Dim txtLastNameg As TextBox
        Dim i As Integer = 1
        If dlPersonalInfo.Items.Count > 0 Then

            For Each item As DataListItem In dlPersonalInfo.Items
                Dim lblshiftto As Label = item.FindControl("lblshiftto")
                If lblshiftto.Text = "0" Or lblshiftto.Text.Trim = "" Then 'changed by mohamed on 16/09/2018
                    Dim strTittle As String = ""
                    lblrlineno = CType(item.FindControl("lblrlineno"), Label)

                    lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
                    ddlTitle = CType(item.FindControl("ddlTittle"), DropDownList)
                    txtFirstNameg = CType(item.FindControl("txtFirstName"), TextBox)
                    txtMidName = CType(item.FindControl("txtMiddleName"), TextBox)
                    txtLastNameg = CType(item.FindControl("txtlastName"), TextBox)

                    Dim lblRowHeading = CType(item.FindControl("lblRowHeading"), Label)
                    Dim lblroomno = CType(item.FindControl("lblroomno"), Label)
                    Dim lblType = CType(item.FindControl("lblPType"), Label)

                    strBuffer.Append("<DocumentElement>")
                    strBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
                    strBuffer.Append(" <rlineno>" & lblrlineno.Text & "</rlineno>")
                    If ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Adult") Then
                        strBuffer.Append(" <title>Mr</title>")
                    ElseIf ddlTitle.Text = "0" And lblRowHeading.Text.Contains("Child") Then
                        strBuffer.Append(" <title>Child Male</title>")
                    Else
                        strBuffer.Append(" <title>" & ddlTitle.Text & "</title>")
                    End If

                    strBuffer.Append(" <firstname>" & IIf(txtFirstNameg.Text = "", lblRowHeading.Text, txtFirstNameg.Text.ToUpper) & "</firstname>")
                    strBuffer.Append(" <middlename>" & txtMidName.Text.ToUpper & "</middlename>")
                    strBuffer.Append(" <lastname>" & txtLastNameg.Text.ToUpper & "</lastname>")
                    strBuffer.Append(" <roomno>" & lblroomno.Text & "</roomno>")
                    If lblType.Text.Contains("Adult") = True Then   '' Added shahul 30/07/18
                        strBuffer.Append(" <guesttype>Adult</guesttype>")
                    Else
                        strBuffer.Append(" <guesttype>Child</guesttype>")
                    End If
                    strBuffer.Append("</DocumentElement>")
                End If
            Next
        End If
        Return strBuffer.ToString
    End Function

    Protected Sub btnSubmitBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitBooking.Click
        Try




            '*** Danny 12/08/2018
            Dim lsGustCheck As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=52")


            If Val(hdnguestsaved.Value) = 0 Then
                If ValidatePersonalDetails() = False Then
                    Exit Sub
                    'If lsGustCheck = "RO0" Then
                    '    ' *** In Elite For agent login all guest names should be compulsory. For RO login no need of any guest name to be compulsory
                    '    ' *** In RPTS only Lead Gust for all
                    '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest For Each Room  ")
                    '    Exit Sub
                    'ElseIf lsGustCheck = "RO1" And Session("sLoginType") <> "RO" Then
                    '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest For Each Room  ")
                    '    Exit Sub
                    'End If


                    'If lsGustCheck = "RO0" Then
                    '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest")
                    '    Return False
                    'ElseIf lsGustCheck = "RO0" Then
                    '    'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Room No " & previousroomno)
                    '    Return True
                    'ElseIf lsGustCheck = "RO1" And Session("sLoginType") <> "RO" Then
                    '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
                    '    Return False
                    'ElseIf lsGustCheck = "RO1" And Session("sLoginType") = "RO" Then
                    '    'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
                    '    Return True
                    'End If


                Else
                    SavingGuests()
                End If
            End If

            If chkTermsAndConditions.Checked = False Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please tick Terms and Conditions")
                Exit Sub
            End If

            If hdnguestchange.Value = "1" And Validateflightdetails() = True Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Guest Names changed, please generate flight details again")
                divFlightdetail.Style.Add("display", "none")
                Exit Sub

            End If


            If SaveArrivalflightdetails() = False Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Arrival Flight Details Saving Failed")
                Exit Sub
            End If
            If SaveDepartureflightdetails() = False Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Departure Flight Details Saving Failed")
                Exit Sub
            End If

            '*** Danny 12/08/2018
            Dim lsFilghtCheck As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=51")

            If objBLLguest.IsExistGuestFlights(Session("sRequestId")) = False Then
                If lsFilghtCheck = 0 Or (lsFilghtCheck = 1 And Session("sLoginType") <> "RO") Then ' *** Flight details should be compulsory for Agents login and not for RO login
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please generate flight details")
                    divFlightdetail.Style.Add("display", "none")
                    FlightBtnCall(1)
                    Exit Sub
                Else
                    FlightBtnCall(1)
                    'Exit Sub
                End If
            End If




            Dim dt As DataTable
            Dim divcode As String = ""
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                objBLLguest.GBdivcode = dt.Rows(0)("div_code").ToString
                objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                objBLLguest.GBAgentcode = dt.Rows(0)("agentcode").ToString
                objBLLguest.GBAgentsourcectrycode = dt.Rows(0)("sourcectrycode").ToString
                objBLLguest.GBLogintype = Session("sLoginType")
                objBLLguest.GBuserlogged = dt.Rows(0)("adduser").ToString
            End If

            'changed by mohamed on 11/09/2018
            Dim lsGuestName As String = ""
            lsGuestName = fnGetGuestName()

            Dim strrequestid As String = ""
            Dim ds As DataSet = objBLLguest.ValidateBooking(objBLLguest.GBRequestid, objBLLguest.GBAgentcode, objBLLguest.GBAgentsourcectrycode, objBLLguest.GBLogintype, 0, lsGuestName) 'changed by mohamed on 11/09/2018

            If ds.Tables(0).Rows.Count > 0 Or ds.Tables(1).Rows.Count > 0 Then
                '' Errors to list
                If ds.Tables(0).Rows.Count > 0 Then
                    btnproceed.Style.Add("display", "none")

                    btnTempBookingRef.Style.Add("display", "block")


                    gdErrorlist.DataSource = ds.Tables(0)
                    gdErrorlist.DataBind()
                    lblErrorlist.Text = "Booking Errors"

                ElseIf ds.Tables(0).Rows.Count = 0 And ds.Tables(1).Rows.Count > 0 Then


                    gdErrorlist.DataSource = ds.Tables(1)
                    gdErrorlist.DataBind()
                    lblErrorlist.Text = "Booking Warnings"
                    btnTempBookingRef.Style.Add("display", "none")
                    btnproceed.Style.Add("display", "block")
                End If

                mpBookingError.Show()
            Else
                If objBLLguest.PrearrangedValidate(Session("sRequestId")) = 1 Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Can not book pre-arranged hotel only.")
                    Exit Sub
                End If
                final_save()

            End If

            ModalPopupDays.Hide()


            'If hdFinalReqestId.Value <> "" Then
            '    Dim requestid = hdFinalReqestId.Value
            '    Dim PrintDt As DataTable = objclsUtilities.GetDataFromDataTable("select  * from booking_hotel_detail where requestid= '" & requestid & "'")
            '    Dim cnt As Integer = 0
            '    For Each printDr As DataRow In PrintDt.Rows
            '        cnt = cnt + 1
            '        Dim partycode = printDr("partycode").ToString()
            '        Dim amended As Integer = 0
            '        Dim cancelled As Integer = 0
            '        Dim strpop As String
            '        Dim pop As String = "popup" + cnt.ToString()
            '        strpop = "window.open('PrintBookingHotel.aspx?RequestId=" & requestid.Trim & "&partycode=" & partycode & "&amended=" & amended & "&cancelled=" & cancelled & "');"
            '        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), pop, strpop, True)
            '    Next
            'End If



        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: btnSubmitBooking_Click :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Function ValidateGuestDetails() As String
        For Each item As DataListItem In dlPersonalInfo.Items
            Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
            Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
            Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
            Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
            Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
            Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)
            'If ddlTittle.Text = "0" Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
            '    ddlTittle.Focus()
            '    Return False
            '    Exit Function

            'End If
            'If txtFirstName.Text = "" Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter first name")
            '    txtFirstName.Focus()
            '    Return False
            '    Exit Function
            'End If
            'If txtLastName.Text = "" Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter last name")
            '    txtLastName.Focus()
            '    Return False
            '    Exit Function
            'End If
            'If txtNationality.Text = "" Then
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
            '    txtNationality.Focus()
            '    Return False
            '    Exit Function
            'End If
            If ddlVisa.SelectedValue = "Required" And (txtFirstName.Text <> "" And txtLastName.Text <> "") Then
                If ddlVisatype.SelectedValue = "--" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Visa Type")
                    ddlVisatype.Focus()
                    Return False
                    Exit Function
                End If
            End If
        Next
        Return True
    End Function
    'Private Function ValidateROAgent(ByVal previousroomno As String) As String '*** Danny 12/08/2018
    '    Dim lsGustCheck As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=52")
    '    If lsGustCheck = "RO0" And previousroomno = 1 Then
    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest for Earch Room")
    '        Return False
    '    ElseIf lsGustCheck = "RO0" And previousroomno > 1 Then
    '        'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Room No " & previousroomno)
    '        Return True
    '    ElseIf lsGustCheck = "RO1" And Session("sLoginType") <> "RO" Then
    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
    '        Return False
    '    ElseIf lsGustCheck = "RO1" And Session("sLoginType") = "RO" Then
    '        'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
    '        Return True
    '    End If
    'End Function

    Private Function ValidatePersonalDetails() As String
        Dim chkflag As Boolean = True
        Dim previousroomno As String = ""
        Dim previouslineno As String = ""
        Dim previousrowno As String = ""
        Dim chkflag1 As Boolean = True
        Dim chkflag2 As Boolean = True

        '' Added shahul 23/07/18
        Dim lsguestcompulsory As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=5301")

        Dim lsGustCheck As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=52")
        For Each item As DataListItem In dlPersonalInfo.Items
            Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
            Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
            Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
            Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
            Dim lblPType As Label = CType(item.FindControl("lblPType"), Label)
            Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label)
            Dim lblRowNo As Label = CType(item.FindControl("lblRowNo"), Label)
            Dim lblroomtext As Label = CType(item.FindControl("lblroomtext"), Label)

            '' Added shahul 04/08/8
            Dim dvmiddlename As HtmlGenericControl = CType(item.FindControl("dvmiddlename"), HtmlGenericControl)
            Dim dvlastname As HtmlGenericControl = CType(item.FindControl("dvlastname"), HtmlGenericControl)

            If lsguestcompulsory = "1" Then  '' Added shahul 23/07/18
                If (txtFirstName.Text = "") Then
                    chkflag = False
                    Exit For
                End If
            Else
                If (txtFirstName.Text = "" Or txtLastName.Text = "") And (Val(lblrlineno.Text) <> Val(previouslineno) And Val(lblrlineno.Text) <> 0 And lblroomtext.Text <> "") Then
                    chkflag = False
                    previousroomno = lblroomno.Text
                    Exit For
                End If
            End If

            'If (txtFirstName.Text = "" Or txtLastName.Text = "") And (Val(lblrlineno.Text) <> Val(previousrowno) And Val(lblrlineno.Text) = 0) Then
            '    chkflag = False
            '    previousrowno = lblrlineno.Text
            '    Exit For
            'End If


            previousroomno = lblroomno.Text
            previouslineno = lblrlineno.Text
            '   previousrowno = lblrlineno.Text
        Next

        If chkflag = False Then
            '  If previousroomno = 0 Then
            'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest For Each Room  ")
            If lsguestcompulsory = "1" Then '' Added shahul 23/07/18
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter All Guest Names For Each Room  ")
            Else
                If lsGustCheck = "RO0" And previousroomno = 1 Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest")
                    Return False
                ElseIf lsGustCheck = "RO0" And previousroomno > 1 Then
                    'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Room No " & previousroomno)
                    Return True
                ElseIf lsGustCheck = "RO1" And Session("sLoginType") <> "RO" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Each Room")
                    Return False
                ElseIf lsGustCheck = "RO1" And Session("sLoginType") = "RO" Then
                    'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
                    Return True
                End If

                'Else
                '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest for Room No " & previousroomno)
                'End If
            End If
            Return False
            Exit Function
        End If

        For Each item As DataListItem In dlPersonalInfo.Items
            Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
            Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
            Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
            Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
            Dim lblPType As Label = CType(item.FindControl("lblPType"), Label)
            Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label)
            Dim lblRowNo As Label = CType(item.FindControl("lblRowNo"), Label)
            Dim lblroomtext As Label = CType(item.FindControl("lblroomtext"), Label)

            If lblroomtext.Text <> "" Then
                chkflag1 = False
                previousrowno = lblrlineno.Text
                Exit For
            Else
                If (txtFirstName.Text <> "" Or txtLastName.Text <> "") And (Val(lblrlineno.Text) = 0 And lblroomtext.Text = "") Then
                    chkflag1 = False
                    previousrowno = lblrlineno.Text
                    Exit For
                End If
            End If



            previousrowno = lblrlineno.Text
        Next
        If chkflag1 = True Then


            If lsGustCheck = "RO0" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest ")
                Return False
            ElseIf lsGustCheck = "RO1" And Session("sLoginType") <> "RO" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Each Room")
                Return False
            ElseIf lsGustCheck = "RO1" And Session("sLoginType") = "RO" Then
                'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
                Return True
            End If
        End If

        For Each item As DataListItem In dlPersonalInfo.Items
            Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
            Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
            Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
            Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
            Dim lblPType As Label = CType(item.FindControl("lblPType"), Label)
            Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label)
            Dim lblRowNo As Label = CType(item.FindControl("lblRowNo"), Label)
            Dim lblroomtext As Label = CType(item.FindControl("lblroomtext"), Label)

            If (txtFirstName.Text = "" Or txtLastName.Text = "") Then
                chkflag2 = False
                'previousrowno = lblrlineno.Text
                Exit For
            End If
        Next
        If chkflag2 = False Then


            If lsGustCheck = "RO0" Then
                'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest ")
                'Return False
            ElseIf lsGustCheck = "RO1" And Session("sLoginType") <> "RO" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Each Room")
                Return False
            ElseIf lsGustCheck = "RO1" And Session("sLoginType") = "RO" Then
                'MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Guest for Earch Room")
                Return True
            End If
        End If



        'If chkflag1 = True Then
        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest  ")


        '    Return False
        '    Exit Function
        'End If


        'Next
        Return True
    End Function


    'Private Function ValidatePersonalDetails() As String '*** Danny 12/08/2018
    '    Dim chkflag As Boolean = True
    '    Dim previousroomno As String = ""
    '    Dim previouslineno As String = ""
    '    Dim previousrowno As String = ""
    '    Dim chkflag1 As Boolean = True
    '    For Each item As DataListItem In dlPersonalInfo.Items
    '        Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    '        Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    '        Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
    '        Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
    '        Dim lblPType As Label = CType(item.FindControl("lblPType"), Label)
    '        Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label)
    '        Dim lblRowNo As Label = CType(item.FindControl("lblRowNo"), Label)
    '        Dim lblroomtext As Label = CType(item.FindControl("lblroomtext"), Label)

    '        If (txtFirstName.Text = "" Or txtLastName.Text = "") And (Val(lblrlineno.Text) <> Val(previouslineno) And Val(lblrlineno.Text) <> 0 And lblroomtext.Text <> "") Then
    '            chkflag = False
    '            previousroomno = lblroomno.Text
    '            Exit For
    '        End If

    '        If (txtFirstName.Text = "" Or txtLastName.Text = "") And (Val(lblrlineno.Text) <> Val(previousrowno) And Val(lblrlineno.Text) = 0) Then
    '            chkflag = False
    '            previousrowno = lblrlineno.Text
    '            'Exit For
    '        End If


    '        previousroomno = lblroomno.Text
    '        previouslineno = lblrlineno.Text
    '        '   previousrowno = lblrlineno.Text
    '    Next

    '    If chkflag = False Then
    '        If ValidateROAgent(previousroomno) = False Then
    '            'If previousroomno = 0 Then

    '            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest For Each Room  ")
    '            'Else
    '            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest for Room No " & previousroomno)
    '            'End If

    '            Return False
    '            Exit Function
    '        End If


    '    End If

    '    For Each item As DataListItem In dlPersonalInfo.Items
    '        Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    '        Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    '        Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
    '        Dim lblroomno As Label = CType(item.FindControl("lblroomno"), Label)
    '        Dim lblPType As Label = CType(item.FindControl("lblPType"), Label)
    '        Dim lblrlineno As Label = CType(item.FindControl("lblrlineno"), Label)
    '        Dim lblRowNo As Label = CType(item.FindControl("lblRowNo"), Label)
    '        Dim lblroomtext As Label = CType(item.FindControl("lblroomtext"), Label)

    '        If lblroomtext.Text <> "" Then
    '            chkflag1 = False
    '            previousrowno = lblrlineno.Text
    '            Exit For
    '        Else
    '            If (txtFirstName.Text <> "" Or txtLastName.Text <> "") And (Val(lblrlineno.Text) = 0 And lblroomtext.Text = "") Then
    '                chkflag1 = False
    '                previousrowno = lblrlineno.Text
    '                Exit For
    '            End If
    '        End If


    '        previousrowno = lblrlineno.Text
    '    Next

    '    If chkflag1 = True Then
    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Lead Guest  ")


    '        Return False
    '        Exit Function
    '    End If


    '    'Next
    '    Return True
    'End Function
    Private Sub EnableArrivalFlightdetails()

        For Each item As DataListItem In dlFlightDetails.Items

            Dim txtArrivalflight As TextBox = CType(item.FindControl("txtArrivalflight"), TextBox)
            Dim txtArrivalTime As TextBox = CType(item.FindControl("txtArrivalTime"), TextBox)
            Dim txtArrivalAirport As TextBox = CType(item.FindControl("txtArrivalAirport"), TextBox)
            Dim txtArrivaltoairport As TextBox = CType(item.FindControl("txtArrivaltoairport"), TextBox)
            Dim chkNA As CheckBox = CType(item.FindControl("chkNA"), CheckBox)
            Dim divreason As HtmlGenericControl = CType(item.FindControl("divreason"), HtmlGenericControl)

            If chkNA.Checked = True Then
                txtArrivalAirport.Enabled = False
                txtArrivalflight.Enabled = False
                txtArrivalTime.Enabled = False
                txtArrivaltoairport.Enabled = False
                divreason.Style.Add("display", "block")
            Else
                txtArrivalAirport.Enabled = True
                txtArrivalflight.Enabled = True
                txtArrivalTime.Enabled = True
                txtArrivaltoairport.Enabled = True
                divreason.Style.Add("display", "none")
            End If

        Next

    End Sub
    Private Sub EnableDepartureFlightdetails()

        For Each item As DataListItem In dldeparturedetails.Items

            Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)
            Dim txtDepartureTime As TextBox = CType(item.FindControl("txtDepartureTime"), TextBox)
            Dim txtDepartureAirport As TextBox = CType(item.FindControl("txtDepartureAirport"), TextBox)
            Dim txtDeparturetoAirport As TextBox = CType(item.FindControl("txtDeparturetoAirport"), TextBox)
            Dim chkNA As CheckBox = CType(item.FindControl("chkdepNA"), CheckBox)
            Dim divdepreason As HtmlGenericControl = CType(item.FindControl("divdepreason"), HtmlGenericControl)

            If chkNA.Checked = True Then
                txtDepartureAirport.Enabled = False
                txtDepartureFlight.Enabled = False
                txtDepartureTime.Enabled = False
                txtDeparturetoAirport.Enabled = False
                divdepreason.Style.Add("display", "block")
            Else
                txtDepartureAirport.Enabled = True
                txtDepartureFlight.Enabled = True
                txtDepartureTime.Enabled = True
                txtDeparturetoAirport.Enabled = True
                divdepreason.Style.Add("display", "none")
            End If

        Next

    End Sub
    Private Function ValidateArrivalFlightDetails() As String

        For Each item As DataListItem In dlFlightDetails.Items

            Dim ChkFlightNotRequired As CheckBox = CType(item.FindControl("ChkFlightNotRequired"), CheckBox)
            If ChkFlightNotRequired.Checked = False Then


                Dim txtArrivalflight As TextBox = CType(item.FindControl("txtArrivalflight"), TextBox)
                Dim txtArrivalTime As TextBox = CType(item.FindControl("txtArrivalTime"), TextBox)
                Dim chkNA As CheckBox = CType(item.FindControl("chkNA"), CheckBox)
                Dim txtArrivaltoairport As TextBox = CType(item.FindControl("txtArrivaltoairport"), TextBox)


                If (txtArrivalflight.Text = "" And txtArrivaltoairport.Text = "") And chkNA.Checked = False Then
                    EnableArrivalFlightdetails()
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
                    txtArrivalflight.Focus()
                    Return False
                    Exit Function
                End If


                If (txtArrivalTime.Text = "" And txtArrivaltoairport.Text = "") And chkNA.Checked = False Then
                    EnableArrivalFlightdetails()
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Arrival Time")
                    txtArrivalTime.Focus()
                    Return False
                    Exit Function
                End If

            End If
        Next
        Return True
    End Function
    Private Function ValidateDepartureFlightDetails() As String

        For Each item As DataListItem In dldeparturedetails.Items
            Dim chkDepFlightNotrquired As CheckBox = CType(item.FindControl("chkDepFlightNotrquired"), CheckBox)
            If chkDepFlightNotrquired.Checked = False Then


                Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)
                Dim txtDepartureTime As TextBox = CType(item.FindControl("txtDepartureTime"), TextBox)
                Dim chkdepNA As CheckBox = CType(item.FindControl("chkdepNA"), CheckBox)
                Dim txtDepartureAirport As TextBox = CType(item.FindControl("txtDepartureAirport"), TextBox)


                If (txtDepartureFlight.Text = "" And txtDepartureAirport.Text = "") And chkdepNA.Checked = False Then
                    EnableDepartureFlightdetails()
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Departure Flight Details")
                    txtDepartureFlight.Focus()
                    Return False
                    Exit Function
                End If


                If (txtDepartureTime.Text = "" And txtDepartureAirport.Text = "") And chkdepNA.Checked = False Then
                    EnableDepartureFlightdetails()
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Departure Time")
                    txtDepartureTime.Focus()
                    Return False
                    Exit Function
                End If
            End If

        Next
        Return True
    End Function
    '' ''Private Function ValidatePersonalFlightDetails() As String
    '' ''    For Each item As DataListItem In dlPersonalInfo.Items
    '' ''        Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    '' ''        Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    '' ''        Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
    '' ''        Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
    '' ''        Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
    '' ''        Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)

    '' ''        If ddlTittle.Text = "0" Then
    '' ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
    '' ''            ddlTittle.Focus()
    '' ''            Return False
    '' ''            Exit Function

    '' ''        End If
    '' ''        If txtFirstName.Text = "" Then
    '' ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter first name")
    '' ''            txtFirstName.Focus()
    '' ''            Return False
    '' ''            Exit Function
    '' ''        End If
    '' ''        If txtLastName.Text = "" Then
    '' ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter last name")
    '' ''            txtLastName.Focus()
    '' ''            Return False
    '' ''            Exit Function
    '' ''        End If
    '' ''        If txtNationality.Text = "" Then
    '' ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
    '' ''            txtNationality.Focus()
    '' ''            Return False
    '' ''            Exit Function
    '' ''        End If
    '' ''        If ddlVisa.SelectedValue = "Required" Then
    '' ''            If ddlVisatype.SelectedValue = "--" Then
    '' ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Visa Type")
    '' ''                ddlVisatype.Focus()
    '' ''                Return False
    '' ''                Exit Function
    '' ''            End If
    '' ''        End If

    '' ''    Next


    '' ''    '*** Danny 12/08/2018
    '' ''    Dim lsFilghtCheck As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=51")
    '' ''    If lsFilghtCheck = 1 Then ' *** Flight details should be compulsory for Agents login and not for RO login
    '' ''        If Session("sLoginType") <> "RO" Then
    '' ''            For Each item As DataListItem In dlFlightDetails.Items

    '' ''                Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)
    '' ''                Dim txtDeparturetime As TextBox = CType(item.FindControl("txtDeparturetime"), TextBox)
    '' ''                Dim txtArrivalFlight As TextBox = CType(item.FindControl("txtArrivalFlight"), TextBox)
    '' ''                Dim txtarrivaltime As TextBox = CType(item.FindControl("txtarrivaltime"), TextBox)



    '' ''                If txtDepartureFlight.Text = "" Then
    '' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
    '' ''                    txtDepartureFlight.Focus()
    '' ''                    Return False
    '' ''                    Exit Function
    '' ''                End If


    '' ''                If txtArrivalFlight.Text = "" Then
    '' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
    '' ''                    txtArrivalFlight.Focus()
    '' ''                    Return False
    '' ''                    Exit Function
    '' ''                End If
    '' ''                If txtarrivaltime.Text = "" Then
    '' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Arrival Time")
    '' ''                    txtDepartureFlight.Focus()
    '' ''                    Return False
    '' ''                    Exit Function
    '' ''                End If
    '' ''                If txtDeparturetime.Text = "" Then
    '' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Departure Time")
    '' ''                    txtDeparturetime.Focus()
    '' ''                    Return False
    '' ''                    Exit Function
    '' ''                End If

    '' ''            Next
    '' ''        End If
    '' ''    Else
    '' ''        For Each item As DataListItem In dlFlightDetails.Items

    '' ''            Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)
    '' ''            Dim txtDeparturetime As TextBox = CType(item.FindControl("txtDeparturetime"), TextBox)
    '' ''            Dim txtArrivalFlight As TextBox = CType(item.FindControl("txtArrivalFlight"), TextBox)
    '' ''            Dim txtarrivaltime As TextBox = CType(item.FindControl("txtarrivaltime"), TextBox)



    '' ''            If txtDepartureFlight.Text = "" Then
    '' ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
    '' ''                txtDepartureFlight.Focus()
    '' ''                Return False
    '' ''                Exit Function
    '' ''            End If


    '' ''            If txtArrivalFlight.Text = "" Then
    '' ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
    '' ''                txtArrivalFlight.Focus()
    '' ''                Return False
    '' ''                Exit Function
    '' ''            End If
    '' ''            If txtarrivaltime.Text = "" Then
    '' ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Arrival Time")
    '' ''                txtDepartureFlight.Focus()
    '' ''                Return False
    '' ''                Exit Function
    '' ''            End If
    '' ''            If txtDeparturetime.Text = "" Then
    '' ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Departure Time")
    '' ''                txtDeparturetime.Focus()
    '' ''                Return False
    '' ''                Exit Function
    '' ''            End If

    '' ''        Next
    '' ''    End If
    '' ''    If Session("sLoginType") <> "RO" And lsFilghtCheck = 1 Then ' *** Flight details should be compulsory for Agents login and not for RO login


    '' ''    End If
    '' ''    Return True
    '' ''End Function

    Protected Sub dlVisaSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlVisaSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblVisaPrice As Label = CType(e.Item.FindControl("lblVisaPrice"), Label)
            Dim lblVisaValue As Label = CType(e.Item.FindControl("lblVisaValue"), Label)

            Dim dvVisaSalevalue As HtmlGenericControl = CType(e.Item.FindControl("dvVisaSalevalue"), HtmlGenericControl)

            If hdBookingEngineRateType.Value = "1" Then
                dvVisaSalevalue.Visible = False
            Else
                dvVisaSalevalue.Visible = True
            End If

            lblVisaPrice.Text = Format(Val(lblVisaPrice.Text), "0.00")
            lblVisaValue.Text = Format(Val(lblVisaValue.Text), "0.00")



        End If



    End Sub

    Protected Sub dlTourSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlTourSummary.ItemDataBound
        '
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hdRateBasisSummary As HiddenField = CType(e.Item.FindControl("hdRateBasisSummary"), HiddenField)
            Dim dvACS As HtmlGenericControl = CType(e.Item.FindControl("dvACS"), HtmlGenericControl)
            Dim dvUnit As HtmlGenericControl = CType(e.Item.FindControl("dvUnit"), HtmlGenericControl)
            Dim dvTourACSSalevalue As HtmlGenericControl = CType(e.Item.FindControl("dvTourACSSalevalue"), HtmlGenericControl)
            Dim dvTourUnitSalevalue As HtmlGenericControl = CType(e.Item.FindControl("dvTourUnitSalevalue"), HtmlGenericControl)

            Dim lblExcursionCode As Label = CType(e.Item.FindControl("lblExcursionCode"), Label)
            Dim lblexcursiondate As Label = CType(e.Item.FindControl("lblexcursiondate"), Label)
            Dim lblCombo As Label = CType(e.Item.FindControl("lblCombo"), Label)
            Dim lblmultipleDates As Label = CType(e.Item.FindControl("lblmultipleDates"), Label)
            Dim lblRequestId As Label = CType(e.Item.FindControl("lblRequestId"), Label)
            Dim lblelineno As Label = CType(e.Item.FindControl("lblelineno"), Label)
            If lblCombo.Text = "YES" Then
                Dim dt As DataTable
                Dim objBLLTourSearch As New BLLTourSearch
                dt = objBLLTourSearch.GetBookedComboMultiDateDetails(lblRequestId.Text, lblExcursionCode.Text, "COMBO", lblelineno.Text)
                If dt.Rows.Count > 0 Then
                    lblexcursiondate.Text = "<ul >"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        lblexcursiondate.Text = lblexcursiondate.Text & " <li type='square' style='margin-left:-30px;padding-bottom:10px;'>" & dt.Rows(i)("excdate").ToString & "- " & dt.Rows(i)("exctypname").ToString & "</li> "
                    Next
                    lblexcursiondate.Text = lblexcursiondate.Text & "</ul>"
                End If

            End If

            If lblmultipleDates.Text = "YES" Then
                Dim dt As DataTable
                Dim objBLLTourSearch As New BLLTourSearch
                dt = objBLLTourSearch.GetBookedComboMultiDateDetails(lblRequestId.Text, lblExcursionCode.Text, "MULTI_DATE", lblelineno.Text)
                If dt.Rows.Count > 0 Then
                    lblexcursiondate.Text = "<ul >"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        lblexcursiondate.Text = lblexcursiondate.Text & " <li type='square' style='margin-left:-30px;padding-bottom:10px;'>" & dt.Rows(i)("excdate").ToString & "</li> "
                    Next
                    lblexcursiondate.Text = lblexcursiondate.Text & "</ul>"
                End If
            End If

            If hdRateBasisSummary.Value = "ACS" Then
                dvACS.Visible = True
                dvUnit.Visible = False
                If hdBookingEngineRateType.Value = "1" Then
                    dvTourACSSalevalue.Visible = False
                    dvTourUnitSalevalue.Visible = False
                Else
                    dvTourACSSalevalue.Visible = True
                    dvTourUnitSalevalue.Visible = True
                End If
            Else
                dvACS.Visible = False
                dvUnit.Visible = True
                If hdBookingEngineRateType.Value = "1" Then
                    dvTourACSSalevalue.Visible = False
                    dvTourUnitSalevalue.Visible = False
                Else
                    dvTourACSSalevalue.Visible = True
                    dvTourUnitSalevalue.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub FindCumilative()
        If Not Session("sLoginType") Is Nothing Then
            If Session("sLoginType") <> "RO" Then
                Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
                'Dim objBLLHotelSearch As New BLLHotelSearch
                iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
                hdBookingEngineRateType.Value = iCumulative.ToString
            End If
        End If
    End Sub

    Protected Sub dlTransferSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlTransferSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            FindCumilative()
            Dim dvTransferUnitPrice As HtmlGenericControl = CType(e.Item.FindControl("dvTransferUnitPrice"), HtmlGenericControl)
            If hdBookingEngineRateType.Value = "1" Then
                dvTransferUnitPrice.Visible = False
            Else
                dvTransferUnitPrice.Visible = True
            End If
        End If



    End Sub

    Private Function GetVisaPrice(ByVal strSelectedValue As String, ByVal strNationality As String) As String
        Dim strCheckIn As String = ""
        Dim strCountryCode As String = ""
        Dim strAgentCode As String = ""
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            objBLLHotelSearch = New BLLHotelSearch
            objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            strCheckIn = objBLLHotelSearch.CheckIn
            strCountryCode = objBLLHotelSearch.SourceCountryCode
            strAgentCode = objBLLHotelSearch.AgentCode

        ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
            Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
            objBLLTourSearch = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
            strCheckIn = objBLLTourSearch.FromDate
            strCountryCode = objBLLTourSearch.SourceCountryCode
            strAgentCode = objBLLTourSearch.AgentCode

        ElseIf Not Session("sobjBLLTransferSearchActive") Is Nothing Then
            Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
            objBLLTransferSearch = CType(Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
            If objBLLTransferSearch.ArrTransferDate = "" Then
                strCheckIn = objBLLTransferSearch.DepTransferDate
            Else
                strCheckIn = objBLLTransferSearch.ArrTransferDate
            End If

            strCountryCode = objBLLTransferSearch.TrfSourceCountryCode
            strAgentCode = objBLLTransferSearch.AgentCode
        End If

        objBLLHotelSearch = Session("sobjBLLHotelSearchActive")
        Dim strQuery = "select visaprice from view_visa_types where visacode='" & strSelectedValue & "' and convert(datetime,'" & strCheckIn & "',105) between fromdate and todate and (CHARINDEX('" & strNationality & "',countries)>0 or CHARINDEX('" & strAgentCode & "',agents)>0)"
        Dim strVisaPrice As String = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)
        Return strVisaPrice
    End Function
    Private Function GetRequestId() As String
        Dim strRequestId As String = ""
        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            Dim objBLLHotelSearch2 = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            strRequestId = objBLLHotelSearch2.OBrequestid
        ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
            Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
            objBLLTourSearch = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
            strRequestId = objBLLTourSearch.EbRequestID
        ElseIf Not Session("sobjBLLTransferSearchActive") Is Nothing Then
            Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
            objBLLTransferSearch = CType(Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
            strRequestId = objBLLTransferSearch.OBRequestId
        ElseIf Not HttpContext.Current.Session("sobjBLLMASearchActive") Is Nothing Then
            Dim objBLLMASearch As BLLMASearch = New BLLMASearch
            objBLLMASearch = CType(HttpContext.Current.Session("sobjBLLMASearchActive"), BLLMASearch)
            strRequestId = objBLLMASearch.OBRequestId
        End If
        Return strRequestId
    End Function

    Private Function ValidateGuestRemarks() As String
        Dim remarktemplate As Boolean = False
        For Each item As System.Web.UI.WebControls.ListItem In chkHotelRemarks.Items
            If item.Selected Then
                remarktemplate = True
                Exit For
            End If
        Next
        If txthotremarks.Text = "" And txtcustRemarks.Text = "" And txtDeptRemarks.Text = "" And txtArrRemarks.Text = "" And remarktemplate = False Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Fill atleast one  RemarksType")
            txthotremarks.Focus()
            Return False
            Exit Function
        End If

        Return True
    End Function

    Protected Sub btnSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRemarks.Click
        If ValidateGuestRemarks() Then
            Dim hoteltemplatestring As New Text.StringBuilder
            objBLLguest.GBRequestid = GetRequestId()
            If hdnrlineno.Value <> "" Then
                objBLLguest.GBGuestLineNo = hdnrlineno.Value
            End If
            ' lblrlineno.Text
            For Each item As System.Web.UI.WebControls.ListItem In chkHotelRemarks.Items
                If item.Selected Then
                    hoteltemplatestring.Append(item.Value)
                    hoteltemplatestring.Append(";")
                End If
            Next
            If hoteltemplatestring.ToString <> "" Then
                objBLLguest.GBRemarksTemplate = hoteltemplatestring.ToString()
            Else
                objBLLguest.GBRemarksTemplate = ""
            End If

            If txthotremarks.Text <> "" Then
                objBLLguest.GBHotelRemarks = CType(txthotremarks.Text, String)
            Else
                objBLLguest.GBHotelRemarks = ""
            End If
            If txtcustRemarks.Text <> "" Then
                objBLLguest.GBAgentRemarks = CType(txtcustRemarks.Text, String)
            Else
                objBLLguest.GBHotelRemarks = ""
            End If
            If txtArrRemarks.Text <> "" Then
                objBLLguest.GBArrivalRemarks = CType(txtArrRemarks.Text, String)
            Else
                objBLLguest.GBArrivalRemarks = ""

            End If
            If txtDeptRemarks.Text <> "" Then
                objBLLguest.GBDepartureRemarks = CType(txtDeptRemarks.Text, String)
            Else
                objBLLguest.GBDepartureRemarks = ""

            End If

            objBLLguest.GBuserlogged = Session("GlobalUserName")
            If objBLLguest.SavingGuestRemarksInTemp() Then
                MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")
                ModalPopupDays.Hide()
            End If
        End If
    End Sub
    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
        If Session("sRequestId") Is Nothing Then
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
    End Sub

    Private Sub ShowMyBooking()
        If Session("sRequestId") Is Nothing Then
            dvMybooking.Visible = False
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                dvMybooking.Visible = True
            Else
                dvMybooking.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Createrows()
    End Sub

    Private Sub Createrows()

        Dim dt As New DataTable
        Dim dr As DataRow



        dt.Columns.Add(New DataColumn("Title", GetType(String)))
        dt.Columns.Add(New DataColumn("Firstname", GetType(String)))
        dt.Columns.Add(New DataColumn("Middlename", GetType(String)))
        dt.Columns.Add(New DataColumn("Lastname", GetType(String)))
        dt.Columns.Add(New DataColumn("Nationalty", GetType(String)))
        dt.Columns.Add(New DataColumn("Nationaltycode", GetType(String)))
        dt.Columns.Add(New DataColumn("ChildAge", GetType(String)))
        dt.Columns.Add(New DataColumn("Type", GetType(String)))
        dt.Columns.Add(New DataColumn("RowNo", GetType(String)))
        dt.Columns.Add(New DataColumn("RowNoAll", GetType(String)))
        dt.Columns.Add(New DataColumn("Partyname", GetType(String)))
        dt.Columns.Add(New DataColumn("Roomno", GetType(String)))
        dt.Columns.Add(New DataColumn("Rlineno", GetType(String)))
        dt.Columns.Add(New DataColumn("ShiftFrom", GetType(String)))
        dt.Columns.Add(New DataColumn("ShiftTo", GetType(String)))
        dt.Columns.Add(New DataColumn("FromHotel", GetType(String)))
        dt.Columns.Add(New DataColumn("ToHotel", GetType(String)))
        dt.Columns.Add(New DataColumn("GuestLineNo", GetType(String)))

        Dim adultcount As Integer = 0
        Dim guestcount As Integer = 0
        For Each gvRow As DataListItem In dlPersonalInfo.Items

            dr = dt.NewRow




            Dim ddlTittle As DropDownList = gvRow.FindControl("ddlTittle")
            Dim txtFirstName As TextBox = gvRow.FindControl("txtFirstName")
            Dim txtMiddleName As TextBox = gvRow.FindControl("txtMiddleName")
            Dim txtLastName As TextBox = gvRow.FindControl("txtLastName")
            Dim txtNationality As TextBox = gvRow.FindControl("txtNationality")
            Dim txtNationalityCode As TextBox = gvRow.FindControl("txtNationalityCode")
            Dim txtChildAge As TextBox = gvRow.FindControl("txtChildAge")
            Dim lblRowHeading As Label = gvRow.FindControl("lblRowHeading")
            Dim lblRowNoAll As Label = gvRow.FindControl("lblRowNoAll")
            Dim lblRowNo As Label = gvRow.FindControl("lblRowNo")
            Dim lblroomtext As Label = gvRow.FindControl("lblroomtext")
            Dim lblroomno As Label = gvRow.FindControl("lblroomno")
            Dim lblrlineno As Label = gvRow.FindControl("lblrlineno")
            Dim lblshiftfrom As Label = gvRow.FindControl("lblshiftfrom")
            Dim lblshiftto As Label = gvRow.FindControl("lblshiftto")
            Dim lblfromhotel As Label = gvRow.FindControl("lblfromhotel")
            Dim lbltohotel As Label = gvRow.FindControl("lbltohotel")
            Dim lblGuestLineNo As Label = gvRow.FindControl("lblGuestLineNo")

            dr("Title") = ddlTittle.Text
            dr("Firstname") = txtFirstName.Text
            dr("Middlename") = txtMiddleName.Text
            dr("Lastname") = txtLastName.Text
            dr("Nationalty") = txtNationality.Text
            dr("Nationaltycode") = txtNationalityCode.Text
            dr("ChildAge") = txtChildAge.Text
            dr("Type") = lblRowHeading.Text 'Left(lblRowHeading.Text, (Len(lblRowHeading.Text) - 1))
            dr("RowNo") = lblRowNo.Text
            dr("RowNoAll") = lblRowNoAll.Text
            dr("Partyname") = lblroomtext.Text
            dr("Roomno") = Val(lblroomno.Text)
            dr("Rlineno") = Val(lblrlineno.Text)
            dr("ShiftFrom") = Val(lblshiftfrom.Text)
            dr("ShiftTo") = Val(lblshiftto.Text)
            dr("FromHotel") = lblfromhotel.Text
            dr("ToHotel") = lbltohotel.Text
            dr("GuestLineNo") = lblGuestLineNo.Text

            If lblRowHeading.Text.Contains("Adult") = True Then
                adultcount = adultcount + 1
            End If
            guestcount = guestcount + 1
            dt.Rows.Add(dr)



        Next


        dr = dt.NewRow
        dr("Type") = "Additional Adult"
        dr("RowNo") = adultcount + 1
        dr("Roomno") = 0
        dr("RowNoAll") = guestcount + 1
        dr("GuestLineNo") = guestcount + 1
        dt.Rows.Add(dr)



        dlPersonalInfo.DataSource = dt
        dlPersonalInfo.DataBind()

        For Each item As DataListItem In dlPersonalInfo.Items
            Dim chkservices As CheckBoxList = CType(item.FindControl("chkservices"), CheckBoxList)

            For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                chkitem.Selected = True
            Next

        Next

    End Sub

    Protected Sub btnAddflight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddflight.Click
        CreateflightRows()
    End Sub

    Private Sub CreateflightRows()

        Dim dt As New DataTable
        Dim dr As DataRow



        dt.Columns.Add(New DataColumn("FlightType", GetType(String)))
        dt.Columns.Add(New DataColumn("Flightdate", GetType(String)))
        dt.Columns.Add(New DataColumn("Flightno", GetType(String)))
        dt.Columns.Add(New DataColumn("Flighttime", GetType(String)))
        dt.Columns.Add(New DataColumn("Airport", GetType(String)))
        dt.Columns.Add(New DataColumn("SameGuest", GetType(String)))
        dt.Columns.Add(New DataColumn("Flightcode", GetType(String)))
        dt.Columns.Add(New DataColumn("ArrBordecode", GetType(String)))
        dt.Columns.Add(New DataColumn("Arrivaltoairport", GetType(String)))
        dt.Columns.Add(New DataColumn("Guestnames", GetType(String)))
        dt.Columns.Add(New DataColumn("NAticked", GetType(String)))
        dt.Columns.Add(New DataColumn("NAReason", GetType(String)))
        dt.Columns.Add(New DataColumn("tlineno", GetType(String)))

        Dim guestnames As String = ""

        For Each gvRow As DataListItem In dlFlightDetails.Items
            guestnames = ""
            dr = dt.NewRow




            ' Dim ddlFlighttype As DropDownList = gvRow.FindControl("ddlFlighttype")
            Dim txtArrivalDate As TextBox = gvRow.FindControl("txtArrivalDate")
            Dim txtArrivalflight As TextBox = gvRow.FindControl("txtArrivalflight")
            Dim txtArrivalTime As TextBox = gvRow.FindControl("txtArrivalTime")
            Dim txtArrivalAirport As TextBox = gvRow.FindControl("txtArrivalAirport")
            Dim txtArrBordecode As TextBox = gvRow.FindControl("txtArrBordecode")
            '   Dim chkSameFlight As CheckBox = gvRow.FindControl("chkSameFlight")
            Dim txtArrivalflightCode As TextBox = gvRow.FindControl("txtArrivalflightCode")
            Dim txtArrivaltoairport As TextBox = gvRow.FindControl("txtArrivaltoairport")
            Dim chkguest As CheckBoxList = CType(gvRow.FindControl("chkguest"), CheckBoxList)
            Dim chkNA As CheckBox = CType(gvRow.FindControl("chkNA"), CheckBox)
            Dim txtreason As TextBox = gvRow.FindControl("txtreason")
            Dim txtservicelineno As TextBox = gvRow.FindControl("txtservicelineno")

            If chkguest.Items.Count > 0 Then
                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    If chkitem.Selected = True Then
                        guestnames = guestnames + "," + chkitem.Text + "|" + chkitem.Value
                    End If
                Next
            End If



            dr("FlightType") = "" 'ddlFlighttype.Text
            dr("Flightdate") = txtArrivalDate.Text
            dr("Flightno") = txtArrivalflight.Text
            dr("Flighttime") = txtArrivalTime.Text
            dr("Airport") = txtArrivalAirport.Text
            dr("SameGuest") = 0
            dr("Flightcode") = txtArrivalflightCode.Text
            dr("ArrBordecode") = txtArrBordecode.Text
            dr("Arrivaltoairport") = txtArrivaltoairport.Text
            dr("Guestnames") = guestnames 'Left(guestnames, Len(guestnames) - 1)
            dr("NAticked") = IIf(chkNA.Checked = True, "1", "0")
            dr("NAReason") = txtreason.Text
            dr("tlineno") = txtservicelineno.Text

            dt.Rows.Add(dr)



        Next


        dr = dt.NewRow
        dt.Rows.Add(dr)


        Dim hotelexists As String = "", strQuery As String = ""
        strQuery = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
        hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        Dim dt1 As New DataTable

        If hotelexists <> "" Then
            dt1 = objBLLCommonFuntions.GetBookingGuestnames_arrival(Session("sRequestId"))
        Else
            dt1 = objBLLCommonFuntions.GetOtherGuestnames_arrival(Session("sRequestId"))
        End If


        If dt1.Rows.Count > 0 Then
            Session("ShowGuests") = dt1
        End If

        dlFlightDetails.DataSource = dt
        dlFlightDetails.DataBind()


        For Each gvRow As DataListItem In dlFlightDetails.Items
            Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")

            If chkguest.Items.Count > 0 Then
                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    chkitem.Selected = True
                Next
            End If

        Next





    End Sub

    Protected Sub btnAddchd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddchd.Click
        createchildrows()
    End Sub

    Private Sub createchildrows()

        Dim dt As New DataTable
        Dim dr As DataRow



        dt.Columns.Add(New DataColumn("Title", GetType(String)))
        dt.Columns.Add(New DataColumn("Firstname", GetType(String)))
        dt.Columns.Add(New DataColumn("Middlename", GetType(String)))
        dt.Columns.Add(New DataColumn("Lastname", GetType(String)))
        dt.Columns.Add(New DataColumn("Nationalty", GetType(String)))
        dt.Columns.Add(New DataColumn("Nationaltycode", GetType(String)))
        dt.Columns.Add(New DataColumn("ChildAge", GetType(String)))
        dt.Columns.Add(New DataColumn("Type", GetType(String)))
        dt.Columns.Add(New DataColumn("RowNo", GetType(String)))
        dt.Columns.Add(New DataColumn("RowNoAll", GetType(String)))
        dt.Columns.Add(New DataColumn("Partyname", GetType(String)))
        dt.Columns.Add(New DataColumn("Roomno", GetType(String)))
        dt.Columns.Add(New DataColumn("Rlineno", GetType(String)))
        dt.Columns.Add(New DataColumn("ShiftFrom", GetType(String)))
        dt.Columns.Add(New DataColumn("ShiftTo", GetType(String)))
        dt.Columns.Add(New DataColumn("FromHotel", GetType(String)))
        dt.Columns.Add(New DataColumn("ToHotel", GetType(String)))
        dt.Columns.Add(New DataColumn("GuestLineNo", GetType(String)))


        Dim childcount As Integer = 0
        Dim guestcount As Integer = 0
        For Each gvRow As DataListItem In dlPersonalInfo.Items

            dr = dt.NewRow




            Dim ddlTittle As DropDownList = gvRow.FindControl("ddlTittle")
            Dim txtFirstName As TextBox = gvRow.FindControl("txtFirstName")
            Dim txtMiddleName As TextBox = gvRow.FindControl("txtMiddleName")
            Dim txtLastName As TextBox = gvRow.FindControl("txtLastName")
            Dim txtNationality As TextBox = gvRow.FindControl("txtNationality")
            Dim txtNationalityCode As TextBox = gvRow.FindControl("txtNationalityCode")
            Dim txtChildAge As TextBox = gvRow.FindControl("txtChildAge")
            Dim lblRowHeading As Label = gvRow.FindControl("lblRowHeading")
            Dim lblRowNoAll As Label = gvRow.FindControl("lblRowNoAll")
            Dim lblRowNo As Label = gvRow.FindControl("lblRowNo")
            Dim lblroomtext As Label = gvRow.FindControl("lblroomtext")
            Dim lblroomno As Label = gvRow.FindControl("lblroomno")
            Dim lblrlineno As Label = gvRow.FindControl("lblrlineno")
            Dim lblshiftfrom As Label = gvRow.FindControl("lblshiftfrom")
            Dim lblshiftto As Label = gvRow.FindControl("lblshiftto")
            Dim lblfromhotel As Label = gvRow.FindControl("lblfromhotel")
            Dim lbltohotel As Label = gvRow.FindControl("lbltohotel")
            Dim lblGuestLineNo As Label = gvRow.FindControl("lblGuestLineNo")


            dr("Title") = ddlTittle.Text
            dr("Firstname") = txtFirstName.Text
            dr("Middlename") = txtMiddleName.Text
            dr("Lastname") = txtLastName.Text
            dr("Nationalty") = txtNationality.Text
            dr("Nationaltycode") = txtNationalityCode.Text
            dr("ChildAge") = txtChildAge.Text
            dr("Type") = lblRowHeading.Text  'Left(lblRowHeading.Text, (Len(lblRowHeading.Text) - 1))
            dr("RowNo") = lblRowNo.Text
            dr("RowNoAll") = lblRowNoAll.Text
            dr("Partyname") = lblroomtext.Text
            dr("Roomno") = Val(lblroomno.Text)
            dr("Rlineno") = Val(lblrlineno.Text)
            dr("ShiftFrom") = Val(lblshiftfrom.Text)
            dr("ShiftTo") = Val(lblshiftto.Text)
            dr("FromHotel") = lblfromhotel.Text
            dr("ToHotel") = lbltohotel.Text
            dr("GuestLineNo") = lblGuestLineNo.Text
            If lblRowHeading.Text.Contains("Child") = True Then
                childcount = childcount + 1
            End If
            guestcount += 1
            dt.Rows.Add(dr)



        Next


        dr = dt.NewRow
        dr("Type") = "Additional Child"
        dr("RowNo") = childcount + 1
        dr("Roomno") = 0
        dr("RowNoAll") = guestcount + 1
        dr("GuestLineNo") = guestcount + 1
        dt.Rows.Add(dr)



        dlPersonalInfo.DataSource = dt
        dlPersonalInfo.DataBind()


        For Each item As DataListItem In dlPersonalInfo.Items
            Dim chkservices As CheckBoxList = CType(item.FindControl("chkservices"), CheckBoxList)

            For Each chkitem As System.Web.UI.WebControls.ListItem In chkservices.Items
                chkitem.Selected = True
            Next

        Next

    End Sub

    Protected Sub dldeparturedetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dldeparturedetails.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim dtRow As DataRow = DataListItemToDataRow(e.Item)

            Dim txtDepartureDate As TextBox = CType(e.Item.FindControl("txtDepartureDate"), TextBox)



            '  Dim ddlFlighttype As DropDownList = CType(e.Item.FindControl("ddlFlighttype"), DropDownList)

            Dim txtDepartureFlight As TextBox = CType(e.Item.FindControl("txtDepartureFlight"), TextBox)
            Dim txtDepartureTime As TextBox = CType(e.Item.FindControl("txtDepartureTime"), TextBox)
            Dim txtDepartureAirport As TextBox = CType(e.Item.FindControl("txtDepartureAirport"), TextBox)
            Dim chkSameFlight As CheckBox = CType(e.Item.FindControl("chkSameFlight"), CheckBox)
            Dim txtDepartureFlightCode As TextBox = CType(e.Item.FindControl("txtDepartureFlightCode"), TextBox)
            Dim txtDepBordecode As TextBox = CType(e.Item.FindControl("txtDepBordecode"), TextBox)
            Dim txtDeparturetoAirport As TextBox = CType(e.Item.FindControl("txtDeparturetoAirport"), TextBox)
            Dim chkdepNA As CheckBox = CType(e.Item.FindControl("chkdepNA"), CheckBox)
            Dim divdepreason As HtmlGenericControl = CType(e.Item.FindControl("divdepreason"), HtmlGenericControl)
            Dim txtdepreason As TextBox = CType(e.Item.FindControl("txtdepreason"), TextBox)
            Dim txtservicelineno As TextBox = CType(e.Item.FindControl("txtservicelineno"), TextBox)

            txtDepartureDate.Text = IIf(Convert.ToString(dtRow("flightdate")) = "", "", dtRow("flightdate"))
            txtDepartureFlight.Text = IIf(Convert.ToString(dtRow("Flightno")) = "", "", dtRow("Flightno"))
            txtDepartureTime.Text = IIf(Convert.ToString(dtRow("Flighttime")) = "", "", dtRow("Flighttime"))
            txtDepartureAirport.Text = IIf(Convert.ToString(dtRow("Airport")) = "", "", dtRow("Airport"))
            '  ddlFlighttype.Text = IIf(Convert.ToString(dtRow("Flighttype")) = "", "", dtRow("Flighttype"))
            txtDepartureFlightCode.Text = IIf(Convert.ToString(dtRow("Flightcode")) = "", "", dtRow("Flightcode"))
            '   chkSameFlight.Checked = IIf(Convert.ToString(dtRow("SameGuest")) = "1", True, False)
            txtDepBordecode.Text = IIf(Convert.ToString(dtRow("DepBordecode")) = "", "", dtRow("DepBordecode"))
            txtDeparturetoAirport.Text = IIf(Convert.ToString(dtRow("DeparturetoAirport")) = "", "", dtRow("DeparturetoAirport"))
            txtdepreason.Text = IIf(Convert.ToString(dtRow("NAReasonDep")) = "", "", dtRow("NAReasonDep"))
            '  chkdepNA.Checked = IIf(Val(dtRow("NAtickedDep")) = 0, False, True)
            If Convert.ToString(dtRow("NAtickedDep")) = "" Or Convert.ToString(dtRow("NAtickedDep")) = "0" Then
                chkdepNA.Checked = False
            Else
                chkdepNA.Checked = True
            End If
            txtservicelineno.Text = IIf(Convert.ToString(dtRow("tlineno")) = "", "", dtRow("tlineno"))

            Dim strQuery As String = "select 't' from booking_guest_flightstemp(nolock) where  requestid='" & CType(Session("sEditRequestId"), String) & "'"
            Dim flightexists As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)


            Dim chkguest As CheckBoxList = CType(e.Item.FindControl("chkguest"), CheckBoxList)
            Dim guetsnamesnew As String()
            If Convert.ToString(dtRow("Guestnames")) <> "" Then
                Dim guetsnames As String() = Right(dtRow("Guestnames").ToString, Len(dtRow("Guestnames").ToString) - 1).Split(",")
                For i = 0 To guetsnames.Length - 1
                    guetsnamesnew = guetsnames(i).ToString.Split("|")

                    chkguest.Items.Add(New System.Web.UI.WebControls.ListItem(guetsnamesnew(0), guetsnamesnew(1)))

                Next

            Else
                '  If flightexists = "" Then
                'Dim dt1 As DataTable = objBLLHotelSearch.GetResultAsDataTable("select distinct g.requestid,ltrim(str(g.guestlineno))+';'+ltrim(str(g.rlineno)) guestlineno,g.Guestname from booking_guestservicestemp g(nolock)  where requestid='" & Session("sRequestId") & "'  and  servicelineno=" & Val(txtservicelineno.Text) & " and (servicecode like 'AIRPORT%' or servicecode like 'TRANSFER%')  ") 'modified by abin on 20181219
                ' Dim dt1 As DataTable = objBLLHotelSearch.GetResultAsDataTable("select distinct g.requestid,ltrim(str(g.guestlineno))+';'+ltrim(str(g.rlineno)) guestlineno,g.Guestname from booking_guestservicestemp g(nolock)  where requestid='" & Session("sRequestId") & "'   and (servicecode like 'AIRPORT%' or servicecode like 'TRANSFER%')  ") 'modified by abin on 20181219
                Dim dt1 As DataTable
                dt1 = objBLLHotelSearch.GetResultAsDataTable("execute sp_getflightguest '" & Session("sRequestId") & "'  ,'DEPARTURE'")
                If dt1.Rows.Count > 0 Then
                    Session("ShowGuestsDep") = dt1
                Else
                    dt1 = objBLLHotelSearch.GetResultAsDataTable("execute GetOthersGuestnames_new '" & Session("sRequestId") & "'  ,'0'")
                    If dt1.Rows.Count > 0 Then
                        Session("ShowGuestsDep") = dt1
                    End If
                End If
                'End If


                chkguest.DataSource = Session("ShowGuestsDep")
                chkguest.DataTextField = "Guestname"
                chkguest.DataValueField = "Guestlineno"
                chkguest.DataBind()
            End If

            If ViewState("AllGuestBookedDep") = 1 Then
                chkguest.Enabled = True 'False 'changed by mohamed on 11/09/2018
                btnAddDepflight.Visible = True 'False 'changed for testing 21/10/2018
            Else
                chkguest.Enabled = True
                btnAddDepflight.Visible = True
            End If

            'chkdepNA.Attributes.Add("onChange", "javascript:showdivreason('" + chkdepNA.ClientID + "','" + CType(divdepreason.ClientID, String) + "' )")
            chkdepNA.Attributes.Add("onChange", "javascript:showdivreason('" + chkdepNA.ClientID + "','" + CType(divdepreason.ClientID, String) + "','" + CType(txtDepartureFlight.ClientID, String) + "','" + CType(txtDepartureTime.ClientID, String) + "','" + CType(txtDepartureAirport.ClientID, String) + "','" + CType(txtDeparturetoAirport.ClientID, String) + "' )")


            ' Dim divguestnames As HtmlGenericControl = CType(e.Item.FindControl("divguestnames"), HtmlGenericControl)
            '  chkSameFlight.Attributes.Add("onChange", "javascript:return ShowhideGuestname('" + CType(chkSameFlight.ClientID, String) + "','" + CType(divguestnames.ClientID, String) + "','" + CType(e.Item.ItemIndex, String) + "' )")



            '   Dim txtDepartureDate As TextBox = CType(e.Item.FindControl("txtDepartureDate"), TextBox)
            '   txtDepartureDate.Text = hdCheckOut.Value
            If txtDepartureDate.Text = "" Then
                txtDepartureDate.Text = hdCheckOut.Value
            End If

            Dim AutoCompleteExtender_txtArrivalflight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_DepartureFlight"), AjaxControlToolkit.AutoCompleteExtender)
            '  Dim AutoCompleteExtender_DepartureFlight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_DepartureFlight"), AjaxControlToolkit.AutoCompleteExtender)
            AutoCompleteExtender_txtArrivalflight.ContextKey = hdCheckOut.Value
            '    AutoCompleteExtender_DepartureFlight.ContextKey = hdCheckOut.Value
            txtDepartureDate.Attributes.Add("onchange", "ChangeArrivalDate('" & txtDepartureDate.ClientID & "','" & AutoCompleteExtender_txtArrivalflight.ClientID & "')")
            '  txtDepartureDate.Attributes.Add("onchange", "ChangeDepartureDate('" & txtDepartureDate.ClientID & "','" & AutoCompleteExtender_DepartureFlight.ClientID & "')")
        End If
    End Sub

    Protected Sub btnAddDepflight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDepflight.Click
        createdepflightrows()
    End Sub


    Private Sub createdepflightrows()

        Dim dt As New DataTable
        Dim dr As DataRow



        dt.Columns.Add(New DataColumn("FlightType", GetType(String)))
        dt.Columns.Add(New DataColumn("Flightdate", GetType(String)))
        dt.Columns.Add(New DataColumn("Flightno", GetType(String)))
        dt.Columns.Add(New DataColumn("Flighttime", GetType(String)))
        dt.Columns.Add(New DataColumn("Airport", GetType(String)))
        dt.Columns.Add(New DataColumn("SameGuest", GetType(String)))
        dt.Columns.Add(New DataColumn("Flightcode", GetType(String)))
        dt.Columns.Add(New DataColumn("DepBordecode", GetType(String)))
        dt.Columns.Add(New DataColumn("DeparturetoAirport", GetType(String)))
        dt.Columns.Add(New DataColumn("Guestnames", GetType(String)))
        dt.Columns.Add(New DataColumn("NAtickedDep", GetType(String)))
        dt.Columns.Add(New DataColumn("NAReasonDep", GetType(String)))
        dt.Columns.Add(New DataColumn("tlineno", GetType(String))) ''S***


        Dim guestnames As String = ""

        For Each gvRow As DataListItem In dldeparturedetails.Items
            guestnames = ""
            dr = dt.NewRow




            ' Dim ddlFlighttype As DropDownList = gvRow.FindControl("ddlFlighttype")
            Dim txtDepartureDate As TextBox = gvRow.FindControl("txtDepartureDate")
            Dim txtDepartureFlight As TextBox = gvRow.FindControl("txtDepartureFlight")
            Dim txtDepartureTime As TextBox = gvRow.FindControl("txtDepartureTime")
            Dim txtDepartureAirport As TextBox = gvRow.FindControl("txtDepartureAirport")
            '   Dim chkSameFlight As CheckBox = gvRow.FindControl("chkSameFlight")
            Dim txtDepartureFlightCode As TextBox = gvRow.FindControl("txtDepartureFlightCode")
            Dim txtDepBordecode As TextBox = gvRow.FindControl("txtDepBordecode")
            Dim txtDeparturetoAirport As TextBox = gvRow.FindControl("txtDeparturetoAirport")
            Dim chkguest As CheckBoxList = CType(gvRow.FindControl("chkguest"), CheckBoxList)
            Dim chkdepNA As CheckBox = gvRow.FindControl("chkdepNA")
            Dim txtdepreason As TextBox = gvRow.FindControl("txtdepreason")
            Dim txtservicelineno As TextBox = gvRow.FindControl("txtservicelineno")

            If chkguest.Items.Count > 0 Then
                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    If chkitem.Selected = True Then
                        guestnames = guestnames + "," + chkitem.Text + "|" + chkitem.Value
                    End If
                Next
            End If



            dr("FlightType") = "" 'ddlFlighttype.Text
            dr("Flightdate") = txtDepartureDate.Text
            dr("Flightno") = txtDepartureFlight.Text
            dr("Flighttime") = txtDepartureTime.Text
            dr("Airport") = txtDepartureAirport.Text
            dr("SameGuest") = 0
            dr("Flightcode") = txtDepartureFlightCode.Text
            dr("DepBordecode") = txtDepBordecode.Text
            dr("DeparturetoAirport") = txtDeparturetoAirport.Text

            dr("Guestnames") = guestnames
            dr("NAtickedDep") = IIf(chkdepNA.Checked = True, "1", "0")
            dr("NAReasonDep") = txtdepreason.Text
            dr("tlineno") = txtservicelineno.Text ''S***


            dt.Rows.Add(dr)



        Next


        dr = dt.NewRow
        dt.Rows.Add(dr)

        Dim hotelexists As String = "", strQuery As String = ""
        strQuery = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
        hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

        Dim dt1 As New DataTable

        If hotelexists <> "" Then
            dt1 = objBLLCommonFuntions.GetBookingGuestnames_departure(Session("sRequestId"))
        Else
            dt1 = objBLLCommonFuntions.GetOtherGuestnames_departure(Session("sRequestId"))
        End If


        ' Dim dt1 As DataTable = objBLLCommonFuntions.GetBookingGuestnames_departure(Session("sRequestId"))
        If dt1.Rows.Count > 0 Then
            Session("ShowGuestsDep") = dt1
        End If


        dldeparturedetails.DataSource = dt
        dldeparturedetails.DataBind()

        For Each gvRow As DataListItem In dldeparturedetails.Items
            Dim chkDepFlightNotrquired As CheckBox = CType(gvRow.FindControl("chkDepFlightNotrquired"), CheckBox)
            If dldeparturedetails.Items.Count = 1 Then
                chkDepFlightNotrquired.Attributes.Add("style", "display:none")
            End If

            Dim chkguest As CheckBoxList = gvRow.FindControl("chkguest")

            If chkguest.Items.Count > 0 Then
                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                    chkitem.Selected = True
                Next
            End If

        Next

    End Sub
    Protected Sub btnAppDeparture_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        Dim btnAppDeparture As Button = CType(sender, Button)
        Dim dlItem As DataListItem = CType((btnAppDeparture).NamingContainer, DataListItem)

        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        Dim strQuery As String = ""

        If ValidateDepartureFlightDetails() Then


            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))

            '' Departure Flights
            If dt.Rows.Count > 0 Then
                objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                For Each item As DataListItem In dldeparturedetails.Items
                    'For Each item As DataListItem In dldeparturedetails.Items

                    Dim strTittle As String = ""

                    If item.ItemIndex = dlItem.ItemIndex Then

                        Dim chkDepFlightNotrquired As CheckBox = CType(dlItem.FindControl("chkDepFlightNotrquired"), CheckBox)
                        If chkDepFlightNotrquired.Checked = False Then



                            Dim txtDepartureDate As TextBox = CType(dlItem.FindControl("txtDepartureDate"), TextBox)
                            Dim txtDepartureFlight As TextBox = CType(dlItem.FindControl("txtDepartureFlight"), TextBox)
                            Dim txtDepartureTime As TextBox = CType(dlItem.FindControl("txtDepartureTime"), TextBox)
                            Dim txtDepartureAirport As TextBox = CType(dlItem.FindControl("txtDepartureAirport"), TextBox)
                            Dim txtDeparturetoAirport As TextBox = CType(dlItem.FindControl("txtDeparturetoAirport"), TextBox)
                            Dim chkguest As CheckBoxList = CType(item.FindControl("chkguest"), CheckBoxList)
                            Dim txtDepartureFlightCode As TextBox = CType(dlItem.FindControl("txtDepartureFlightCode"), TextBox)
                            Dim txtDepBordecode As TextBox = CType(dlItem.FindControl("txtDepBordecode"), TextBox)

                            Dim chkdepNA As CheckBox = CType(dlItem.FindControl("chkdepNA"), CheckBox)
                            Dim divdepreason As HtmlGenericControl = CType(dlItem.FindControl("divdepreason"), HtmlGenericControl)
                            Dim txtdepreason As TextBox = CType(dlItem.FindControl("txtdepreason"), TextBox)

                            If chkguest.Items.Count > 0 Then
                                Dim k As Integer = 1
                                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                                    If chkitem.Selected = True Then

                                        Dim Arrdate As String = txtDepartureDate.Text
                                        If Arrdate <> "" Then
                                            Dim strDates As String() = Arrdate.Split("/")
                                            Arrdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                                        End If

                                        Dim flighttranid As String() = txtDepartureFlightCode.Text.Split("|")

                                        Dim strGuestValues As String() = chkitem.Value.Split("|")
                                        For i As Integer = 0 To strGuestValues.Length - 1

                                            Dim guestline As String() = strGuestValues(i).Split(";")

                                            strBuffer.Append("<DocumentElement>")
                                            If chkdepNA.Checked = True Then
                                                strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                strBuffer.Append("<departuredate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</departuredate>")
                                                strBuffer.Append(" <flightcode></flightcode>")
                                                strBuffer.Append(" <flight_tranid></flight_tranid>")
                                                strBuffer.Append(" <flighttime></flighttime>")
                                                strBuffer.Append(" <depaiport></depaiport>")
                                                strBuffer.Append(" <originaiport></originaiport>")
                                                strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                strBuffer.Append(" <NAReason>" & txtdepreason.Text & "</NAReason>")
                                                strBuffer.Append(" <NAticked>1</NAticked>")
                                            Else
                                                strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                strBuffer.Append("<departuredate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</departuredate>")
                                                strBuffer.Append(" <flightcode>" & CType(txtDepartureFlight.Text, String) & "</flightcode>")
                                                strBuffer.Append(" <flight_tranid>" & CType(flighttranid(0), String) & "</flight_tranid>")
                                                strBuffer.Append(" <flighttime>" & CType(txtDepartureTime.Text, String) & "</flighttime>")
                                                strBuffer.Append(" <depaiport>" & CType(txtDepBordecode.Text, String) & "</depaiport>")
                                                strBuffer.Append(" <originaiport>" & CType(txtDeparturetoAirport.Text, String) & "</originaiport>")
                                                strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                strBuffer.Append(" <NAReason>" & txtdepreason.Text & "</NAReason>")
                                                strBuffer.Append(" <NAticked>0</NAticked>")
                                            End If

                                            strBuffer.Append("</DocumentElement>")
                                        Next
                                    End If
                                Next

                            End If
                        Else

                        End If
                    End If
                Next
            End If

            objBLLguest.GBDepFlightXml = strBuffer.ToString
            objBLLguest.GBuserlogged = Session("GlobalUserName")

            If objBLLguest.SavingDepartureFlightTemp() Then
                EnableDepartureFlightdetails()

                Dim hotelexists As String = "", strQuery1 As String = ""
                strQuery1 = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
                hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)

                Dim dt1 As New DataTable

                If hotelexists <> "" Then
                    dt1 = objBLLCommonFuntions.GetBookingGuestnames_departure(Session("sRequestId"))
                Else
                    dt1 = objBLLCommonFuntions.GetOtherGuestnames_departure(Session("sRequestId"))
                End If

                ' Dim dt1 As DataTable = objBLLCommonFuntions.GetBookingGuestnames_departure(Session("sRequestId"))
                If dt1.Rows.Count > 0 Then
                    btnAddDepflight.Style.Add("display", "block")
                    'btnAddDepflight_Click(sender, e)
                Else
                    btnAddDepflight.Style.Add("display", "block")
                    'btnAddDepflight.Style.Add("display", "none")'changed for testing 21/10/2018
                End If
                'Dim btnAppDeparture1 As Button = CType(dldeparturedetails.Items(dlItem.ItemIndex).FindControl("btnAppDeparture"), Button)
                'btnAppDeparture1.Text = "Selected"

                MessageBox.ShowMessage(Page, MessageType.Success, "Saved Departure Details")
            End If

        End If
    End Sub
    Protected Sub btnAppArrival_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim objbtn As Button = CType(sender, Button)
        'Dim rowid As Integer = 0
        'Dim row As DataListItem
        'row = CType(objbtn.NamingContainer, DataListItem)
        'rowid = row.ItemIndex

        'Dim dtRow As DataRow
        'dtRow = DataListItemToDataRow(row)

        Dim dt As DataTable
        Dim btnAppArrival As Button = CType(sender, Button)
        Dim dlItem As DataListItem = CType((btnAppArrival).NamingContainer, DataListItem)

        Dim strBuffer As New Text.StringBuilder
        Dim strFliBuffer As New Text.StringBuilder
        Dim strServiceBuffer As New Text.StringBuilder
        Dim strQuery As String = ""
        If ValidateArrivalFlightDetails() Then



            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            'If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
            '    Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
            '    objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            '    objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
            '    objBLLguest.GBRlineno = objBLLHotelSearch.OBrlineno
            'Else
            '    objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
            '    objBLLguest.GBRlineno = "0"
            'End If

            '' Arrival Flights
            If dt.Rows.Count > 0 Then
                objBLLguest.GBRequestid = dt.Rows(0)("requestid").ToString
                For Each item As DataListItem In dlFlightDetails.Items
                    'For Each item As DataListItem In dlFlightDetails.Items

                    Dim strTittle As String = ""

                    If item.ItemIndex = dlItem.ItemIndex Then
                        Dim ChkFlightNotRequired As CheckBox = CType(item.FindControl("ChkFlightNotRequired"), CheckBox)
                        If ChkFlightNotRequired.Checked = False Then

                            Dim txtArrivalDate As TextBox = CType(dlItem.FindControl("txtArrivalDate"), TextBox)
                            Dim txtArrivalflight As TextBox = CType(dlItem.FindControl("txtArrivalflight"), TextBox)
                            Dim txtArrivalTime As TextBox = CType(dlItem.FindControl("txtArrivalTime"), TextBox)
                            Dim txtArrivalAirport As TextBox = CType(dlItem.FindControl("txtArrivalAirport"), TextBox)
                            Dim txtArrivaltoairport As TextBox = CType(dlItem.FindControl("txtArrivaltoairport"), TextBox)
                            Dim chkguest As CheckBoxList = CType(item.FindControl("chkguest"), CheckBoxList)
                            Dim txtArrivalflightCode As TextBox = CType(dlItem.FindControl("txtArrivalflightCode"), TextBox)
                            Dim txtArrBordecode As TextBox = CType(dlItem.FindControl("txtArrBordecode"), TextBox)
                            Dim btnAppArrival1 As Button = CType(dlItem.FindControl("btnAppArrival"), Button)

                            Dim chkNA As CheckBox = CType(dlItem.FindControl("chkNA"), CheckBox)
                            Dim divreason As HtmlGenericControl = CType(dlItem.FindControl("divreason"), HtmlGenericControl)
                            Dim txtreason As TextBox = CType(dlItem.FindControl("txtreason"), TextBox)


                            If chkguest.Items.Count > 0 Then
                                Dim k As Integer = 1
                                For Each chkitem As System.Web.UI.WebControls.ListItem In chkguest.Items
                                    If chkitem.Selected = True Then

                                        Dim Arrdate As String = txtArrivalDate.Text
                                        If Arrdate <> "" Then
                                            Dim strDates As String() = Arrdate.Split("/")
                                            Arrdate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)

                                        End If

                                        Dim flighttranid As String() = txtArrivalflightCode.Text.Split("|")

                                        Dim strGuestValues As String() = chkitem.Value.Split("|")
                                        For i As Integer = 0 To strGuestValues.Length - 1

                                            Dim guestline As String() = strGuestValues(i).Split(";")

                                            strBuffer.Append("<DocumentElement>")

                                            If chkNA.Checked = True Then
                                                strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                strBuffer.Append("<arrivaldate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</arrivaldate>")
                                                strBuffer.Append(" <flightcode></flightcode>")
                                                strBuffer.Append(" <flight_tranid></flight_tranid>")
                                                strBuffer.Append(" <flighttime></flighttime>")
                                                strBuffer.Append(" <arraiport></arraiport>")
                                                strBuffer.Append(" <originaiport></originaiport>")
                                                strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                strBuffer.Append(" <NAReason>" & txtreason.Text & "</NAReason>")
                                                strBuffer.Append(" <NAticked>1</NAticked>")
                                            Else
                                                strBuffer.Append(" <guestlineno>" & guestline(0) & "</guestlineno>")
                                                strBuffer.Append("<arrivaldate>" & Format(CType(Arrdate, Date), "yyyy/MM/dd") & "</arrivaldate>")
                                                strBuffer.Append(" <flightcode>" & CType(txtArrivalflight.Text, String) & "</flightcode>")
                                                strBuffer.Append(" <flight_tranid>" & CType(flighttranid(0), String) & "</flight_tranid>")
                                                strBuffer.Append(" <flighttime>" & CType(txtArrivalTime.Text, String) & "</flighttime>")
                                                strBuffer.Append(" <arraiport>" & CType(txtArrBordecode.Text, String) & "</arraiport>")
                                                strBuffer.Append(" <originaiport>" & CType(txtArrivalAirport.Text, String) & "</originaiport>")
                                                strBuffer.Append(" <rlineno>" & guestline(1) & "</rlineno>")
                                                strBuffer.Append(" <NAReason>" & txtreason.Text & "</NAReason>")
                                                strBuffer.Append(" <NAticked>0</NAticked>")
                                            End If


                                            strBuffer.Append("</DocumentElement>")
                                        Next
                                    End If
                                Next
                                ' Dim btnAppArrival1 As Button = CType(dlFlightDetails.Items(dlItem.ItemIndex).FindControl("btnAppArrival"), Button)
                                '   btnAppArrival1.Text = "Selected"
                            End If
                        End If
                    End If
                Next
            End If

            objBLLguest.GBFlightXml = strBuffer.ToString
            objBLLguest.GBuserlogged = Session("GlobalUserName")

            If objBLLguest.SavingArrivalFlightTemp() Then
                EnableArrivalFlightdetails()
                Dim hotelexists As String = "", strQuery1 As String = ""
                strQuery1 = "select 't' from booking_hotel_detailtemp(nolock) where requestid='" & Session("sRequestId") & "'"
                hotelexists = objclsUtilities.ExecuteQueryReturnStringValue(strQuery1)

                Dim dt1 As New DataTable

                If hotelexists <> "" Then
                    dt1 = objBLLCommonFuntions.GetBookingGuestnames_arrival(Session("sRequestId"))
                Else
                    dt1 = objBLLCommonFuntions.GetOtherGuestnames_arrival(Session("sRequestId"))
                End If

                ' Dim dt1 As DataTable = objBLLCommonFuntions.GetBookingGuestnames_arrival(Session("sRequestId"))
                If dt1.Rows.Count > 0 Then
                    btnAddflight.Style.Add("display", "block")
                    ' btnAddflight_Click(sender, e)
                Else
                    btnAddflight.Style.Add("display", "block")
                    'btnAddflight.Style.Add("display", "none") 'changed for testing 21/10/2018
                End If

                MessageBox.ShowMessage(Page, MessageType.Success, "Saved Arrival Details")
            End If
        End If
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

        Session("VisaDetailsDT") = Nothing
        Session("vlineno") = Nothing
        Session("sdsPackageSummary") = Nothing
        Session("sEditRequestId") = Nothing



    End Sub

    '' Added shahul 29/07/18
    Sub showhotelrequest(ByVal requestid As String)
        Try

            Dim strpop As String
            'Dim requestid = hdFinalReqestId.Value
            Dim sqlstr As String = "select  distinct d.partycode,isnull(h.amended,0)  amended,  " _
                                   & " isnull(h.cancelled,0) cancelled from booking_hotel_detail d(nolock),booking_hotel_detail_confcancel h(nolock) where d.requestid=h.requestid and d.rlineno=h.rlineno and " _
                                   & " d.requestid='" & requestid & "' and ( isnull(amended,0)=1 or isnull(cancelled,0)=1 or convert(varchar(10),d.moddate,111)=convert(varchar(10),getdate(),111) or  isnull(convert(varchar(10),d.moddate,111),'')='' ) "
            Dim PrintDt As DataTable = objclsUtilities.GetDataFromDataTable(sqlstr)
            Dim Amended As Boolean = False

            If PrintDt.Rows.Count > 0 Then
                Dim lbNeedHotelEmail As Boolean
                lbNeedHotelEmail = objclsUtilities.fn_NeedToSendHotelEmail()
                For Each printDr As DataRow In PrintDt.Rows


                    strpop = "window.open('PrintPage.aspx?printId=hotelRequest&requestid=" & requestid.Trim & "&partycode=" & printDr("partycode").ToString() & "&amended=" & printDr("amended").ToString() & "&cancelled=" & printDr("cancelled").ToString() & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupHotelRequest" + printDr("partycode").ToString(), strpop, True)



                Next


            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Sub hotelmail()
        Dim objclsUtilities As New clsUtilities

        Dim requestid = hdFinalReqestId.Value
        Dim sqlstr As String = ""
        If Not Session("sEditRequestId") Is Nothing Then
            If Session("sEditRequestId").ToString.Contains("QG") Or Session("sEditRequestId").ToString.Contains("QR") Or Session("sLoginType") = "RO" Then
                sqlstr = "select  distinct d.partycode,isnull(h.amended,0)  amended,  " _
                & " isnull(h.cancelled,0) cancelled from booking_hotel_detail d(nolock),booking_hotel_detail_confcancel h(nolock) where d.requestid=h.requestid and d.rlineno=h.rlineno and " _
                & " d.requestid='" & requestid & "' "
            Else
                sqlstr = "select  distinct d.partycode,isnull(h.amended,0)  amended,  " _
               & " isnull(h.cancelled,0) cancelled from booking_hotel_detail d(nolock),booking_hotel_detail_confcancel h(nolock) where d.requestid=h.requestid and d.rlineno=h.rlineno and " _
               & " d.requestid='" & requestid & "' and ( isnull(amended,0)=1 or isnull(cancelled,0)=1 or convert(varchar(10),d.moddate,111)=convert(varchar(10),getdate(),111) or  isnull(convert(varchar(10),d.moddate,111),'')='' ) "

            End If

        Else
            sqlstr = "select  distinct d.partycode,isnull(h.amended,0)  amended,  " _
                      & " isnull(h.cancelled,0) cancelled from booking_hotel_detail d(nolock),booking_hotel_detail_confcancel h(nolock) where d.requestid=h.requestid and d.rlineno=h.rlineno and " _
                      & " d.requestid='" & requestid & "' and ( isnull(amended,0)=1 or isnull(cancelled,0)=1 or convert(varchar(10),d.moddate,111)=convert(varchar(10),getdate(),111) or  isnull(convert(varchar(10),d.moddate,111),'')='' ) "

        End If


        Dim PrintDt As DataTable = objclsUtilities.GetDataFromDataTable(sqlstr)
        Dim Amended As Boolean = False

        If PrintDt.Rows.Count > 0 Then
            Dim lbNeedHotelEmail As Boolean
            lbNeedHotelEmail = objclsUtilities.fn_NeedToSendHotelEmail()
            For Each printDr As DataRow In PrintDt.Rows
                If lbNeedHotelEmail = True Then 'This Condition is added By Mohamed 'changed by mohamed on 28/06/2018
                    If chkEmailToHotel.Checked = True Then
                        ' disabled hotel email for cumulative user. Modified by abin on 17/12/2017
                        FindCumilative()
                        Dim iCumulativeUser As Integer = 0
                        If Session("sLoginType") = "RO" Then
                            Dim strQuery As String = "select count(agentcode)CNT from agentmast(nolock) where bookingengineratetype='CUMULATIVE' and agentcode=(select min(agentcode) from  booking_header(nolock) where requestid='" & requestid & "')"
                            iCumulativeUser = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

                        End If

                        'If hdBookingEngineRateType.Value = "1" Or iCumulativeUser > 0 Then 'commented / changed by mohamed on 25/09/2018 - REF DT25092018_A
                        'Else

                        If Not Session("sEditRequestId") Is Nothing And printDr("amended").ToString() = 1 And printDr("cancelled").ToString() = 0 Then
                            sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())
                            Amended = True
                        ElseIf Session("sEditRequestId") Is Nothing And printDr("amended").ToString() = 0 Then
                            sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())

                        ElseIf Not Session("sEditRequestId") Is Nothing And printDr("cancelled").ToString() = 1 Then
                            sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())
                        ElseIf Not Session("sEditRequestId") Is Nothing And printDr("amended").ToString() = 0 Then
                            sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())
                        End If

                        'End If
                    Else
                        Dim sendEmailToRo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected  from reservation_parameters where param_id=5309")
                        If sendEmailToRo = "1" Then
                            If Not Session("sEditRequestId") Is Nothing And printDr("amended").ToString() = 1 And printDr("cancelled").ToString() = 0 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())
                                Amended = True
                            ElseIf Session("sEditRequestId") Is Nothing And printDr("amended").ToString() = 0 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())

                            ElseIf Not Session("sEditRequestId") Is Nothing And printDr("cancelled").ToString() = 1 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())
                            ElseIf Not Session("sEditRequestId") Is Nothing And printDr("amended").ToString() = 0 Then
                                sendemailtohotel(printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString())
                            End If
                        End If
                    End If
                    objclsUtilities.SaveEmailLog(requestid, "Hotel", printDr("partycode").ToString(), printDr("amended").ToString(), printDr("cancelled").ToString(), IIf(chkEmailToAgent.Checked, "1", "0"), IIf(chkEmailToHotel.Checked, "1", "0"), Session("GlobalUserName").ToString)
                End If
            Next
            If objclsUtilities.fn_NeedToSendAgentEmail() Then 'This Condition is added By Mohamed 'changed by mohamed on 28/06/2018
                If chkEmailToAgent.Checked = True Then
                    If Amended = True Then
                        sendemail("Amended")
                    Else
                        sendemail("New")
                    End If
                Else
                    Dim sendEmailToRo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected  from reservation_parameters where param_id=5309")
                    If sendEmailToRo = "1" Then


                        If Amended = True Then
                            sendemail("Amended")
                        Else
                            sendemail("New")
                        End If
                    End If
                End If
                objclsUtilities.SaveEmailLog(requestid, "Agent", "", "0", "0", IIf(chkEmailToAgent.Checked, "1", "0"), IIf(chkEmailToHotel.Checked, "1", "0"), Session("GlobalUserName").ToString)
            End If
        Else
            If objclsUtilities.fn_NeedToSendAgentEmail() Then 'This Condition is added By Mohamed 'changed by mohamed on 28/06/2018
                If chkEmailToAgent.Checked = True Then
                    sendemail("New")


                Else
                    Dim sendEmailToRo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected  from reservation_parameters where param_id=5309")
                    If sendEmailToRo = "1" Then






                        sendemail("New")
                    End If
                End If
                objclsUtilities.SaveEmailLog(requestid, "Agent", "", "0", "0", IIf(chkEmailToAgent.Checked, "1", "0"), IIf(chkEmailToHotel.Checked, "1", "0"), Session("GlobalUserName").ToString)
            End If
        End If
    End Sub
    Protected Sub btnnewbooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnewbooking.Click
        'sendemail()
        ' hotelmail()


        clearallBookingSessions()
        Response.Redirect("~\Home.aspx?Tab=0")
    End Sub

    Protected Sub btnbacktobooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbacktobooking.Click
        mpBookingError.Hide()
    End Sub

    Protected Sub btnproceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnproceed.Click
        Try
            mpBookingError.Hide()
            If objBLLguest.PrearrangedValidate(Session("sRequestId")) = 1 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Can not book pre-arranged hotel only.")
                Exit Sub
            End If
            final_save()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        Finally

        End Try
    End Sub

    Protected Sub btnTempBookingRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTempBookingRef.Click

        If Not Session("sRequestId") Is Nothing Then
            Dim strRequestId As String = "TP/" & Session("sRequestId").ToString

            MessageBox.ShowMessage(Page, MessageType.Confirm, "Your Temporary Booking Reference is " & strRequestId)
        End If

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
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            Dim requestid = hdFinalReqestId.Value.Replace(",", "")
            Dim lsshowhotelrequest As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected  from reservation_parameters where param_id=5308")
            'Dim requestid = hdFinalReqestId.Value
            If hdFinalReqestId.Value <> "" Then
                Dim strpop As String
                Dim ResParam As New ReservationParameters
                ResParam = Session("sobjResParam")
                Dim divName As String
                If ResParam.DivCode = "01" Then
                    divName = "Mahce Tourism"
                Else
                    divName = "Mahce  Tourism"
                End If
                MessageBox.ShowMessage(Page, MessageType.Important, "Please note the option of Proforma Invoice has been temporarily suspended due to VAT implementation in UAE with effect from 01st January 2018. Hence the final confirmation and Invoice will be sent to your email ID by our accounts team & the value of this Invoice should be treated as full & final.</br></br>Apologies for the inconvenience.</br></br>Admin</br>" + divName + "</br>")
                Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + requestid + "'")
                If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                If chkCumulative.Trim() = "CUMULATIVE" Then
                    strpop = "window.open('PrintPage.aspx?printId=Itinerary&RequestId=" & requestid.Trim & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupItinerary", strpop, True)
                    Dim ConfirmStatus As String = objclsUtilities.ExecuteQueryReturnStringValue("select dbo.fn_booking_confirmstatus('" & requestid.Trim & "') as ConfirmStatus")
                    If ConfirmStatus = "1" And ResParam.LoginType = "RO" Then
                        Dim hvDt As DataTable = objclsUtilities.GetDataFromDataTable("select rlineno from booking_hotel_detail where requestid ='" + requestid + "' group by rlineno")
                        If hvDt.Rows.Count > 0 Then
                            Dim rlineNumber As Integer = 0
                            For Each hvDr As DataRow In hvDt.Rows
                                rlineNumber = Convert.ToInt32(hvDr("rlineno"))
                                strpop = "window.open('PrintPage.aspx?printId=hotelVoucher&RequestId=" & requestid.Trim & "&rlineNo=" & rlineNumber.ToString() & "');"
                                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup" + rlineNumber.ToString(), strpop, True)
                            Next
                        End If
                    End If
                End If
                Session("sobjResParam") = ResParam
                strpop = "window.open('PrintPage.aspx?printId=bookingConfirmation&RequestId=" & requestid.Trim & "');"
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popup", strpop, True)

                If Session("sLoginType") = "RO" And lsshowhotelrequest = "1" Then '' Added shahul 29/07/18
                    showhotelrequest(requestid)
                End If

                '*** Service Voucher Danny 30/09/2018 
                If Session("sLoginType") = "RO" Then
                    '*** Check parameter for Need to attach.  Danny
                    '*** Only Confirmed Booking need to create SR. Danny
                    Dim parm(0) As SqlParameter
                    parm(0) = New SqlParameter("@RequestID", CType(requestid, String))
                    Dim ds_SR As New DataSet
                    ds_SR = objclsUtilities.GetDataSet("SP_SelectServiceDetails", parm)
                    If ds_SR.Tables(0).Rows(0)("Attach").ToString() = "Y" And ds_SR.Tables(0).Rows(0)("ConfirmStatus").ToString() = "1" Then
                        strpop = "window.open('PrintPage.aspx?printId=ServiceVoucher&RequestId=" & requestid.Trim & "');"
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupSR", strpop, True)
                    End If
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: GenerateReport :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub


    Protected Sub btnAbondon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbondon.Click
        clearallBookingSessions()
        Response.Redirect("~\Home.aspx?Tab=0")
    End Sub
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub
    Protected Sub dlSpecialEventsSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If hdBookingEngineRateType.Value = "1" Then
                Dim dvSplEventValue As HtmlGenericControl = CType(e.Item.FindControl("dvSplEventValue"), HtmlGenericControl)
                dvSplEventValue.Visible = False
            End If
        End If

    End Sub

    Protected Sub dlAirportSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlAirportSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim divunit As HtmlGenericControl = CType(e.Item.FindControl("divunit"), HtmlGenericControl)
            Dim divaddtional As HtmlGenericControl = CType(e.Item.FindControl("divaddtional"), HtmlGenericControl)
            Dim divadults As HtmlGenericControl = CType(e.Item.FindControl("divadults"), HtmlGenericControl)
            Dim divchild As HtmlGenericControl = CType(e.Item.FindControl("divchild"), HtmlGenericControl)

            Dim dvadultPrice As HtmlGenericControl = CType(e.Item.FindControl("dvadultPrice"), HtmlGenericControl)
            Dim dvChildprice As HtmlGenericControl = CType(e.Item.FindControl("dvChildprice"), HtmlGenericControl)
            Dim dvaddPaxPrice As HtmlGenericControl = CType(e.Item.FindControl("dvaddPaxPrice"), HtmlGenericControl)
            Dim dvAirportUnitPrice As HtmlGenericControl = CType(e.Item.FindControl("dvAirportUnitPrice"), HtmlGenericControl)

            Dim lblratebasis As Label = CType(e.Item.FindControl("lblratebasis"), Label)
            Dim lblchild As Label = CType(e.Item.FindControl("lblchild"), Label)
            Dim lbladdpax As Label = CType(e.Item.FindControl("lbladdpax"), Label)



            If lblratebasis.Text = "Unit" Then
                divunit.Style.Add("display", "block")
                divaddtional.Style.Add("display", "block")
                divadults.Style.Add("display", "none")
                divchild.Style.Add("display", "none")
                If Val(lbladdpax.Text) = 0 Then
                    divaddtional.Style.Add("display", "none")
                End If


            ElseIf lblratebasis.Text = "Adult/Child" Then
                divunit.Style.Add("display", "none")
                divaddtional.Style.Add("display", "none")
                divadults.Style.Add("display", "block")
                divchild.Style.Add("display", "block")

                If Val(lblchild.Text) = 0 Then
                    divchild.Style.Add("display", "none")
                End If

            End If

            If hdBookingEngineRateType.Value = "1" Then
                dvadultPrice.Style.Add("display", "none")
                dvChildprice.Style.Add("display", "none")
                dvaddPaxPrice.Style.Add("display", "none")
                dvAirportUnitPrice.Style.Add("display", "none")
            End If



        End If
    End Sub

    Protected Sub dlOtherSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlOtherSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim dvothUnitPrice As HtmlGenericControl = CType(e.Item.FindControl("dvothUnitPrice"), HtmlGenericControl)

            Dim dvothwlUnitPrice As HtmlGenericControl = CType(e.Item.FindControl("dvothwlUnitPrice"), HtmlGenericControl)

            If hdBookingEngineRateType.Value = "1" Then
                dvothUnitPrice.Style.Add("display", "none")
                dvothwlUnitPrice.Style.Add("display", "none")
            Else
                If hdWhiteLabel.Value = "1" Then
                    dvothwlUnitPrice.Style.Add("display", "block")
                    dvothUnitPrice.Style.Add("display", "none")
                Else
                    dvothUnitPrice.Style.Add("display", "block")
                    dvothwlUnitPrice.Style.Add("display", "none")
                End If

                ' dvothUnitPrice.Style.Add("display", "block")
            End If


        End If
    End Sub

    'Private Sub ShowProfitSummary()
    '    Try
    '        If Not Session("sRequestId") Is Nothing Then
    '            Dim iCumulative As Integer = 0
    '            If Session("sLoginType") = "RO" Then
    '                iCumulative = objBLLguest.CheckSelectedAgentIsCumulative(Session("sRequestId"))
    '            End If

    '            If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
    '                'btnSubmitBooking.Visible = False
    '                Dim ds As DataSet
    '                Dim objBLLGuest As New BLLGuest

    '                If Not Session("sdsPackageSummary") Is Nothing Then
    '                    ds = Session("sdsPackageSummary")
    '                    If ds.Tables.Count > 0 Then
    '                        Dim strBaseCurrency As String = ds.Tables(2).Rows(0)("BaseCurrency").ToString
    '                        Dim strSaleCurrCode As String = ds.Tables(2).Rows(0)("salecurrcode").ToString

    '                        lblBaseCurrency.Text = strBaseCurrency
    '                        lblsalecurrcode.Text = strSaleCurrCode
    '                        lblsalecurrcodeAgent.Text = strSaleCurrCode
    '                        lblPTotalSaleValueText.Text = "Total Sale Value" ' (" & strSaleCurrCode & ") "
    '                        lblPTotalSaleValue.Text = ds.Tables(2).Rows(0)("totalsalevalue").ToString
    '                        lblPTotalSaleValueTextAgent.Text = "Total Sale Value" ' (" & strSaleCurrCode & ") "
    '                        lblPTotalSaleValueAgent.Text = ds.Tables(2).Rows(0)("totalsalevalue").ToString


    '                        'lblPTotalSaleValueAEDText.Text = "Total Sale Value (" & strBaseCurrency & ") "
    '                        lblPTotalSaleValueAED.Text = ds.Tables(2).Rows(0)("totalsalevaluebase").ToString

    '                        lblPTotalCostValueText.Text = "Total Cost Value" ' (" & strBaseCurrency & ") "
    '                        lblPTotalCostValue.Text = ds.Tables(2).Rows(0)("totalcostvaluebase").ToString
    '                        lblPTotalCostValueCurr.Text = ds.Tables(2).Rows(0)("totalcostvaluecurr").ToString

    '                        lblPTotalProfitText.Text = "Total Profit" ' (" & strBaseCurrency & ") "
    '                        lbPTotalProfit.Text = ds.Tables(2).Rows(0)("totalmarkupbase").ToString
    '                        lbPTotalProfitCurr.Text = ds.Tables(2).Rows(0)("totalmarkupcurr").ToString

    '                        lblPMinimumMarkupText.Text = "Minimum Markup" ' (" & strBaseCurrency & ") "
    '                        lblPMinimumMarkup.Text = ds.Tables(2).Rows(0)("minimummarkup").ToString
    '                        lblPMinimumMarkupCurr.Text = ds.Tables(2).Rows(0)("minimummarkupcurr").ToString

    '                        lblPFormulaIdText.Text = "Formula Id "
    '                        lblPFormulaId.Text = ds.Tables(2).Rows(0)("formulaid").ToString
    '                        lblPDifferentialMarkupText.Text = "Differential Markup" '  (" & strSaleCurrCode & ") "
    '                        lblPDifferentialMarkup.Text = ds.Tables(2).Rows(0)("differentialmarkup").ToString
    '                        lblPDifferentialMarkupbase.Text = ds.Tables(2).Rows(0)("differentialmarkupbase").ToString

    '                        lblPDiscount_DifferentialMarkupText.Text = "Discount % on Differential Markup"
    '                        lblPDiscount_DifferentialMarkup.Text = ds.Tables(2).Rows(0)("discountperc").ToString & " %"
    '                        lblPDiscount_DifferentialMarkup1.Text = ds.Tables(2).Rows(0)("discountperc").ToString & " %"

    '                        lblPDiscountValueText.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
    '                        lblPDiscountValue.Text = ds.Tables(2).Rows(0)("discountmarkup").ToString
    '                        lblPDiscountValueBase.Text = ds.Tables(2).Rows(0)("discountmarkupbase").ToString

    '                        lblPDiscountValueTextAgent.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
    '                        lblPDiscountValueAgent.Text = ds.Tables(2).Rows(0)("discountmarkup").ToString


    '                        '' Added Shahul 09/10/18
    '                        lblPNetprofitText.Text = "Net Profit"
    '                        lblPNetprofitValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalmarkupcurr").ToString) - Val(ds.Tables(2).Rows(0)("discountmarkup").ToString), String)
    '                        lblPNetprofitValueBase.Text = CType(Val(ds.Tables(2).Rows(0)("totalmarkupbase").ToString) - Val(ds.Tables(2).Rows(0)("discountmarkupbase").ToString), String)

    '                        '' Added Shahul 30/06/18
    '                        lblSystemMarkupText.Text = "System Markup"
    '                        lblSystemMarkupValue.Text = CType(Val(lbPTotalProfitCurr.Text) - Val(lblPDiscountValue.Text), String)
    '                        lblSystemMarkupValueBase.Text = CType(Val(lbPTotalProfit.Text) - Val(lblPDiscountValueBase.Text), String)

    '                        lblRevisedMarkupText.Text = "Revised Markup"
    '                        lblRevisedMarkupValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalrevisedmarkup").ToString), String)
    '                        lblRevisedMarkupValueBase.Text = CType(Math.Round(Val(ds.Tables(2).Rows(0)("totalrevisedmarkup").ToString) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String)

    '                        lblSystemDiscountText.Text = "System Discount"
    '                        lblSystemDiscountValue.Text = CType(Val(lblPDiscountValue.Text), String)
    '                        lblSystemDiscountValueBase.Text = CType(Val(lblPDiscountValueBase.Text), String)

    '                        lblRevisedDiscountText.Text = "Revised Discount"
    '                        If Val(lblRevisedMarkupValue.Text) <> 0 Then

    '                            lblRevisedDiscountValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalreviseddiscount").ToString), String)
    '                            lblRevisedDiscountValueBase.Text = CType(Math.Round(Val(lblRevisedDiscountValue.Text) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String)
    '                        Else
    '                            lblRevisedDiscountValue.Text = ""
    '                            lblRevisedDiscountValueBase.Text = ""
    '                        End If


    '                        lblPNetSaleValueText.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
    '                        lblPNetSaleValueTextAgent.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "

    '                        '' Added shahul 06/10/18
    '                        If Val(lblRevisedDiscountValue.Text) = 0 Then
    '                            lblPNetSaleValue.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString
    '                            lblPNetSaleValueAgent.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString
    '                            lblPNetSaleValueBase.Text = ds.Tables(2).Rows(0)("netsalevaluebase").ToString
    '                        Else
    '                            lblPNetSaleValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalsalevalue").ToString) - Val(lblRevisedDiscountValue.Text), String)
    '                            lblPNetSaleValueAgent.Text = CType(Val(ds.Tables(2).Rows(0)("totalsalevalue").ToString) - Val(lblRevisedDiscountValue.Text), String)
    '                            lblPNetSaleValueBase.Text = CType(Math.Round(Val(ds.Tables(2).Rows(0)("totalsalevaluebase").ToString) - Val(CType(Math.Round(Val(lblRevisedDiscountValue.Text) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String))), String)
    '                        End If



    '                        'lblPNetSaleValueText.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
    '                        'lblPNetSaleValue.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString

    '                        'lblPNetSaleValueTextAgent.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
    '                        'lblPNetSaleValueAgent.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString

    '                        'lblPNetSaleValueBase.Text = ds.Tables(2).Rows(0)("netsalevaluebase").ToString



    '                        dvPackageDetails.Style.Add("display", "block")
    '                        If Session("sLoginType") = "RO" Then
    '                            dvPackageSummaryRO.Style.Add("display", "block")
    '                            dvPackageSummaryAgent.Style.Add("display", "none")
    '                        Else
    '                            dvPackageSummaryRO.Style.Add("display", "none")
    '                            dvPackageSummaryAgent.Style.Add("display", "block")

    '                        End If

    '                        'If ds.Tables(0).Rows.Count > 0 Then
    '                        '    '  dvPackageDetails.Visible = False
    '                        '    '  MessageBox.ShowMessage(Page, MessageType.Warning, ds.Tables(0).Rows(0)("errdesc").ToString)
    '                        'Else
    '                        '    If ds.Tables(2).Rows.Count > 0 Then
    '                        '        Dim strBaseCurrency As String = ds.Tables(2).Rows(0)("BaseCurrency").ToString
    '                        '        Dim strSaleCurrCode As String = ds.Tables(2).Rows(0)("salecurrcode").ToString
    '                        '        lblPTotalSaleValueText.Text = "Total Sale Value(" & strSaleCurrCode & ")"
    '                        '        lblPTotalSaleValue.Text = ds.Tables(2).Rows(0)("totalsalevalue").ToString
    '                        '        lblPTotalSaleValueAEDText.Text = "Total Sale Value(" & strBaseCurrency & ")"
    '                        '        lblPTotalSaleValueAED.Text = ds.Tables(2).Rows(0)("totalsalevaluebase").ToString
    '                        '    Else

    '                        '    End If


    '                        'End If
    '                        'If ds.Tables(2).Rows.Count > 0 Then
    '                        '    Dim strBaseCurrency As String = ds.Tables(2).Rows(0)("BaseCurrency").ToString
    '                        '    Dim strSaleCurrCode As String = ds.Tables(2).Rows(0)("salecurrcode").ToString
    '                        '    lblPTotalSaleValueText.Text = "Total Sale Value (" & strSaleCurrCode & ") "
    '                        '    lblPTotalSaleValue.Text = ds.Tables(2).Rows(0)("totalsalevalue").ToString
    '                        '    lblPTotalSaleValueAEDText.Text = "Total Sale Value (" & strBaseCurrency & ") "
    '                        '    lblPTotalSaleValueAED.Text = ds.Tables(2).Rows(0)("totalsalevaluebase").ToString
    '                        '    lblPTotalCostValueText.Text = "Total Cost Value (" & strBaseCurrency & ") "
    '                        '    lblPTotalCostValue.Text = ds.Tables(2).Rows(0)("totalcostvaluebase").ToString

    '                        '    lblPTotalProfitText.Text = "Total Profit (" & strBaseCurrency & ") "
    '                        '    lbPTotalProfit.Text = ds.Tables(2).Rows(0)("totalmarkupbase").ToString
    '                        '    lblPMinimumMarkupText.Text = "Minimum Markup (" & strBaseCurrency & ") "
    '                        '    lblPMinimumMarkup.Text = ds.Tables(2).Rows(0)("minimummarkup").ToString
    '                        '    lblPFormulaIdText.Text = "Formula Id "
    '                        '    lblPFormulaId.Text = ds.Tables(2).Rows(0)("formulaid").ToString
    '                        '    lblPDifferentialMarkupText.Text = "Differential Markup  (" & strSaleCurrCode & ") "
    '                        '    lblPDifferentialMarkup.Text = ds.Tables(2).Rows(0)("differentialmarkup").ToString
    '                        '    lblPDiscount_DifferentialMarkupText.Text = "Discount % on Differential Markup"
    '                        '    lblPDiscount_DifferentialMarkup.Text = ds.Tables(2).Rows(0)("discountperc").ToString
    '                        '    lblPDiscountValueText.Text = "Discount Value (" & strSaleCurrCode & ") "
    '                        '    lblPDiscountValue.Text = ds.Tables(2).Rows(0)("discountmarkup").ToString
    '                        '    lblPNetSaleValueText.Text = "Net Sale Value (" & strSaleCurrCode & ") "
    '                        '    lblPNetSaleValue.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString
    '                        'End If
    '                        'dvPackageDetails.Style.Add("display", "block")
    '                        'If Session("sLoginType") = "RO" Then
    '                        '    dvROPackage.Style.Add("display", "block")
    '                        'Else
    '                        '    dvROPackage.Style.Add("display", "none")
    '                        'End If
    '                    Else
    '                        dvPackageDetails.Style.Add("display", "none")
    '                    End If
    '                End If

    '            Else
    '                dvPackageDetails.Style.Add("display", "none")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnGeneratePackageValue_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
    '    End Try
    'End Sub
    Private Sub ShowProfitSummary()
        Try
            If Not Session("sRequestId") Is Nothing Then
                Dim iCumulative As Integer = 0
                If Session("sLoginType") = "RO" Then
                    iCumulative = objBLLguest.CheckSelectedAgentIsCumulative(Session("sRequestId"))
                End If

                If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
                    'btnSubmitBooking.Visible = False
                    Dim ds As DataSet
                    Dim objBLLGuest As New BLLGuest

                    If Not Session("sdsPackageSummary") Is Nothing Then
                        ds = Session("sdsPackageSummary")
                        If ds.Tables.Count > 0 Then
                            Dim strBaseCurrency As String = ds.Tables(2).Rows(0)("BaseCurrency").ToString
                            Dim strSaleCurrCode As String = ds.Tables(2).Rows(0)("salecurrcode").ToString

                            lblBaseCurrency.Text = strBaseCurrency
                            lblsalecurrcode.Text = strSaleCurrCode
                            lblsalecurrcodeAgent.Text = strSaleCurrCode
                            lblPTotalSaleValueText.Text = "Total Sale Value" ' (" & strSaleCurrCode & ") "
                            lblPTotalSaleValue.Text = ds.Tables(2).Rows(0)("totalsalevalue").ToString
                            lblPTotalSaleValueTextAgent.Text = "Total Sale Value" ' (" & strSaleCurrCode & ") "
                            lblPTotalSaleValueAgent.Text = ds.Tables(2).Rows(0)("totalsalevalue").ToString


                            'lblPTotalSaleValueAEDText.Text = "Total Sale Value (" & strBaseCurrency & ") "
                            lblPTotalSaleValueAED.Text = ds.Tables(2).Rows(0)("totalsalevaluebase").ToString

                            lblPTotalCostValueText.Text = "Total Cost Value" ' (" & strBaseCurrency & ") "
                            lblPTotalCostValue.Text = ds.Tables(2).Rows(0)("totalcostvaluebase").ToString
                            lblPTotalCostValueCurr.Text = ds.Tables(2).Rows(0)("totalcostvaluecurr").ToString

                            lblPTotalProfitText.Text = "Total Profit" ' (" & strBaseCurrency & ") "
                            lbPTotalProfit.Text = ds.Tables(2).Rows(0)("totalmarkupbase").ToString
                            lbPTotalProfitCurr.Text = ds.Tables(2).Rows(0)("totalmarkupcurr").ToString

                            lblPMinimumMarkupText.Text = "Minimum Markup" ' (" & strBaseCurrency & ") "
                            lblPMinimumMarkup.Text = ds.Tables(2).Rows(0)("minimummarkup").ToString
                            lblPMinimumMarkupCurr.Text = ds.Tables(2).Rows(0)("minimummarkupcurr").ToString

                            lblPFormulaIdText.Text = "Formula Id "
                            lblPFormulaId.Text = ds.Tables(2).Rows(0)("formulaid").ToString
                            lblPDifferentialMarkupText.Text = "Differential Markup" '  (" & strSaleCurrCode & ") "
                            lblPDifferentialMarkup.Text = ds.Tables(2).Rows(0)("differentialmarkup").ToString
                            lblPDifferentialMarkupbase.Text = ds.Tables(2).Rows(0)("differentialmarkupbase").ToString

                            lblPDiscount_DifferentialMarkupText.Text = "Discount % on Differential Markup"
                            lblPDiscount_DifferentialMarkup.Text = ds.Tables(2).Rows(0)("discountperc").ToString & " %"
                            lblPDiscount_DifferentialMarkup1.Text = ds.Tables(2).Rows(0)("discountperc").ToString & " %"

                            lblPDiscountValueText.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
                            lblPDiscountValue.Text = ds.Tables(2).Rows(0)("discountmarkup").ToString
                            lblPDiscountValueBase.Text = ds.Tables(2).Rows(0)("discountmarkupbase").ToString

                            lblPDiscountValueTextAgent.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
                            lblPDiscountValueAgent.Text = ds.Tables(2).Rows(0)("discountmarkup").ToString

                            '' Added Shahul 09/10/18
                            lblPNetprofitText.Text = "Net Profit"
                            lblPNetprofitValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalmarkupcurr").ToString) - Val(ds.Tables(2).Rows(0)("discountmarkup").ToString), String)
                            lblPNetprofitValueBase.Text = CType(Val(ds.Tables(2).Rows(0)("totalmarkupbase").ToString) - Val(ds.Tables(2).Rows(0)("discountmarkupbase").ToString), String)

                            '' Added Shahul 30/06/18
                            lblSystemMarkupText.Text = "System Markup"
                            lblSystemMarkupValue.Text = CType(Val(lbPTotalProfitCurr.Text) - Val(lblPDiscountValue.Text), String)
                            lblSystemMarkupValueBase.Text = CType(Val(lbPTotalProfit.Text) - Val(lblPDiscountValueBase.Text), String)

                            lblRevisedMarkupText.Text = "Revised Markup"
                            lblRevisedMarkupValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalrevisedmarkup").ToString), String)
                            lblRevisedMarkupValueBase.Text = CType(Math.Round(Val(ds.Tables(2).Rows(0)("totalrevisedmarkup").ToString) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String)

                            lblSystemDiscountText.Text = "System Discount"
                            lblSystemDiscountValue.Text = CType(Val(lblPDiscountValue.Text), String)
                            lblSystemDiscountValueBase.Text = CType(Val(lblPDiscountValueBase.Text), String)

                            lblRevisedDiscountText.Text = "Revised Discount"
                            If Val(lblRevisedMarkupValue.Text) <> 0 Then

                                lblRevisedDiscountValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalreviseddiscount").ToString), String)
                                lblRevisedDiscountValueBase.Text = CType(Math.Round(Val(lblRevisedDiscountValue.Text) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String)

                                lblPDiscountValueBase.Text = CType(Math.Round(Val(lblRevisedDiscountValue.Text) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String) ' ds.Tables(2).Rows(0)("discountmarkup").ToString
                                lblPDiscountValueAgent.Text = CType(Val(ds.Tables(2).Rows(0)("totalreviseddiscount").ToString), String)

                            Else
                                lblRevisedDiscountValue.Text = ""
                                lblRevisedDiscountValueBase.Text = ""
                            End If


                            lblPNetSaleValueText.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
                            lblPNetSaleValueTextAgent.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "

                            '' Added shahul 06/10/18
                            If Val(lblRevisedDiscountValue.Text) = 0 Then
                                lblPNetSaleValue.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString
                                lblPNetSaleValueAgent.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString
                                lblPNetSaleValueBase.Text = ds.Tables(2).Rows(0)("netsalevaluebase").ToString
                            Else
                                lblPNetSaleValue.Text = CType(Val(ds.Tables(2).Rows(0)("totalsalevalue").ToString) - Val(lblRevisedDiscountValue.Text), String)
                                lblPNetSaleValueAgent.Text = CType(Val(ds.Tables(2).Rows(0)("totalsalevalue").ToString) - Val(lblRevisedDiscountValue.Text), String)
                                lblPNetSaleValueBase.Text = CType(Math.Round(Val(ds.Tables(2).Rows(0)("totalsalevaluebase").ToString) - Val(CType(Math.Round(Val(lblRevisedDiscountValue.Text) * Val(ds.Tables(2).Rows(0)("saleconvrate").ToString), 0), String))), String)
                            End If


                            'lblPNetSaleValueText.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
                            'lblPNetSaleValue.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString

                            'lblPNetSaleValueTextAgent.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
                            'lblPNetSaleValueAgent.Text = ds.Tables(2).Rows(0)("netsalevalue").ToString

                            'lblPNetSaleValueBase.Text = ds.Tables(2).Rows(0)("netsalevaluebase").ToString
                            'dvPackageDetails.Style.Add("display", "block")
                            'If Session("sLoginType") = "RO" Then
                            '    dvPackageSummaryRO.Style.Add("display", "block")
                            '    dvPackageSummaryAgent.Style.Add("display", "none")
                            'Else
                            '    dvPackageSummaryRO.Style.Add("display", "none")
                            '    dvPackageSummaryAgent.Style.Add("display", "block")

                            'End If


                        Else
                            dvPackageDetails.Style.Add("display", "none")
                        End If
                    End If

                Else
                    dvPackageDetails.Style.Add("display", "none")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnGeneratePackageValue_Click:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub lbPTotalProfit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbPTotalProfit.Click
        If Session("sLoginType") = "RO" Then
            If Not Session("sdsPackageSummary") Is Nothing Then
                Dim ds As DataSet
                ds = Session("sdsPackageSummary")
                If ds.Tables(1).Rows.Count > 0 Then
                    gvPackageROSummary.DataSource = ds.Tables(1)
                    gvPackageROSummary.DataBind()
                    mpPackageROSummary.Show()
                    Dim lblSaleValueHeader As Label = CType(gvPackageROSummary.HeaderRow.FindControl("lblSaleValueHeader"), Label)
                    lblSaleValueHeader.Text = "Sale Value (" & ds.Tables(1).Rows(0)("salecurrcode").ToString & ")"
                    Dim lblSaleValueBaseHeader As Label = CType(gvPackageROSummary.HeaderRow.FindControl("lblSaleValueBaseHeader"), Label)
                    lblSaleValueBaseHeader.Text = "Sale Value Base (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"
                    Dim lblCostValueHeader As Label = CType(gvPackageROSummary.HeaderRow.FindControl("lblCostValueHeader"), Label)
                    lblCostValueHeader.Text = "Cost Value (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"
                End If
            End If
        End If
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetArrivalpickup(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = " select  distinct f.airportbordercode,a.airportbordername from flightmast f,airportbordersmaster a  where f.airportbordercode=a.airportbordercode and a.active=1 and a.airportbordername like  '" & prefixText & "%' order by airportbordername "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("airportbordername").ToString(), myDS.Tables(0).Rows(i)("airportbordercode").ToString()))
                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function
    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
    End Sub
    Private Sub BindPreHotelSummary()

        Dim strRequestId As String = ""
        If Not Session("sEditRequestId") Is Nothing Then
            strRequestId = Session("sEditRequestId")
        Else
            strRequestId = Session("sRequestId")
        End If
        Dim objBLLHotelSearch = New BLLHotelSearch
        dvTabPrehotelSummary.Visible = False
        Dim dt As DataTable
        If Session("sLoginType") = "RO" Then
            dt = objBLLHotelSearch.PreHotelSummary(strRequestId)
        Else
            dt = objBLLHotelSearch.PreHotelSummary(strRequestId, objResParam.WhiteLabel)
        End If

        FillPreHotelSummaryDetails(dt)

    End Sub

    Private Sub FillPreHotelSummaryDetails(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            dlPreHotelSummary.Visible = True
            dlPreHotelSummary.DataSource = dt
            dlPreHotelSummary.DataBind()
            dvTabPrehotelSummary.Visible = True
        Else
            dvTabPrehotelSummary.Visible = False
        End If
    End Sub

    Protected Sub dlPreHotelSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlPreHotelSummary.ItemDataBound
        '
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgPreHotelEdit As ImageButton = CType(e.Item.FindControl("imgPreHotelEdit"), ImageButton)
            Dim imgPreHotelDelete As ImageButton = CType(e.Item.FindControl("imgPreHotelDelete"), ImageButton)
            Dim lblChildAges As Label = CType(e.Item.FindControl("lblChildAges"), Label)
            If lblChildAges.Text = "()" Then
                lblChildAges.Text = ""
            End If
        End If
    End Sub

    Private Sub FillGuestNameAll()
        Dim strQuery As String = "SELECT * FROM fn_get_adultschild_bookingtemp('" + Session("sRequestId") + "')"
        Dim dtGuest As DataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Dim strAdults As String = "0"
        Dim strChild As String = "0"

        If dtGuest.Rows.Count > 0 Then
            strAdults = dtGuest.Rows(0)("adults")
            strChild = dtGuest.Rows(0)("child")
        End If

        Dim iTotalRows As Integer = Val(strAdults) + Val(strChild)
        Dim iRNo As Integer = 0
        Dim iGuestLineNo As Integer = 0
        Dim dtDynamicPersonalInfo = New DataTable()
        Dim dcRowNo = New DataColumn("RowNo", GetType(String))
        Dim dcType = New DataColumn("Type", GetType(String))
        Dim dcGuestType = New DataColumn("GuestType", GetType(String))
        Dim dcChildAge = New DataColumn("ChildAge", GetType(String))

        Dim Title = New DataColumn("Title", GetType(String))
        Dim Firstname = New DataColumn("Firstname", GetType(String))
        Dim Middlename = New DataColumn("Middlename", GetType(String))
        Dim Lastname = New DataColumn("Lastname", GetType(String))
        Dim Nationalty = New DataColumn("Nationalty", GetType(String))
        Dim Nationaltycode = New DataColumn("Nationaltycode", GetType(String))


        dtDynamicPersonalInfo.Columns.Add(dcRowNo)

        dtDynamicPersonalInfo.Columns.Add(dcType)
        dtDynamicPersonalInfo.Columns.Add(dcGuestType)
        dtDynamicPersonalInfo.Columns.Add(dcChildAge)

        dtDynamicPersonalInfo.Columns.Add(Title)
        dtDynamicPersonalInfo.Columns.Add(Firstname)
        dtDynamicPersonalInfo.Columns.Add(Middlename)
        dtDynamicPersonalInfo.Columns.Add(Lastname)
        dtDynamicPersonalInfo.Columns.Add(Nationalty)
        dtDynamicPersonalInfo.Columns.Add(Nationaltycode)



        For i As Integer = 0 To strAdults - 1

            Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
            row("RowNo") = (i + 1).ToString

            row("Type") = "Adult " + (i + 1).ToString
            row("ChildAge") = ""
            row("GuestType") = "Adult"

            row("Title") = ""
            row("Firstname") = ""
            row("Middlename") = ""
            row("Lastname") = ""
            row("Nationalty") = ""
            row("Nationaltycode") = ""

            dtDynamicPersonalInfo.Rows.Add(row)
        Next

        For i As Integer = 0 To strChild - 1
            iRNo = iRNo + 1
              Dim row As DataRow = dtDynamicPersonalInfo.NewRow()
            row("RowNo") = (i + 1).ToString
            row("Type") = "Child " + (i + 1).ToString
            row("ChildAge") = ""
            row("GuestType") = "Child"
            row("Title") = ""
            row("Firstname") = ""
            row("Middlename") = ""
            row("Lastname") = ""
            row("Nationalty") = ""
            row("Nationaltycode") = ""
            dtDynamicPersonalInfo.Rows.Add(row)
        Next

        dlPersonalInfo.DataSource = dtDynamicPersonalInfo
        dlPersonalInfo.DataBind()
    End Sub

End Class
