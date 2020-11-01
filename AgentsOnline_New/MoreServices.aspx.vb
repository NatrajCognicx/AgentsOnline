Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Math

Partial Class MoreServices
    Inherits System.Web.UI.Page
    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objclsUtils As New clsUtils
    Dim objcommonfunctions As New BLLCommonFuntions
    Dim objResParam As New ReservationParameters
    Dim objBLLHotelSearch As New BLLHotelSearch
    Dim objBLLguest As New BLLGuest
    Dim iCumulative As Integer = 0
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim objclsquotecosting As New clsQuoteCosting

    Dim objApiController As New ApiController
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then

            Session("Status_dtAdultChilds") = Nothing '*** Danny 01/09/2018
            Session("dtAdultChilds") = Nothing

            Try
                If Session("sAgentCompany") Is Nothing Or Session("GlobalUserName") Is Nothing Then
                    Dim strAbsoluteUrl As String = hdAbsoluteUrl.Value
                    If (strAbsoluteUrl = "") Then
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

                Dim isBooking As String = objclsUtilities.ExecuteQueryReturnStringValue("select count(*)cnt from booking_header(nolock) where requestid='" + Session("sEditRequestId") + "'")
                If isBooking = 0 Then
                    hdTempRequest.Value = "NEW"
                Else
                    hdTempRequest.Value = "EDIT"
                End If

                LoadHome()
                If Not Session("sdtPriceBreakup") Is Nothing Then
                    Session.Remove("sdtPriceBreakup")
                End If
                If Not Session("sEditRequestId") Is Nothing Then





                    divcheck.Style.Add("display", "block")
                    divsubmitquote.Style.Add("display", "block")
                    If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then


                        hdOpMode.Value = "Edit"
                        dvAgenRef.Style.Add("display", "none")
                        divsubmitquote.Style.Add("display", "none")
                        divcheck.Style.Add("display", "none")
                    End If
                    txtAgencyRef.Text = objBLLguest.GetAgentRef(Session("sEditRequestId").ToString)
                Else
                    hdOpMode.Value = "New"
                    divcheck.Style.Add("display", "block")
                    divsubmitquote.Style.Add("display", "block")
                End If
                chkApplySameConf.Attributes.Add("onChange", "javascript:ChkApplyConfirm('" + chkApplySameConf.ClientID + "' )")
                chkApplySameCancel.Attributes.Add("onChange", "javascript:ChkApplyCancel('" + chkApplySameCancel.ClientID + "' )")

                If (Session("sRequestId") Is Nothing And Session("sEditRequestId") Is Nothing) Then
                    dvSummaryPart.Visible = False
                End If
                If Not Session("sFinalBooked") Is Nothing Then
                    If Session("sFinalBooked") = "1" Then
                        dvSummaryPart.Visible = False
                    End If
                End If

                '*** Danny 12/08/2018
                Dim lsdivTerms As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=50")
                If Session("sLoginType") = "RO" And lsdivTerms = 1 Then
                    chkTermsAndConditions.Checked = True
                    Dim divcheck As HtmlGenericControl = CType(FindControl("divcheck"), HtmlGenericControl)
                    divcheck.Style.Add("display", "none")
                End If

                '13/12
                'sbDisableBookingButtonAndMenu() 'Changed by mohamed on 01/08/2018

                '' Added shahul 29/07/18
                Dim lsABANDONtext As String = objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=5306")
                If lsABANDONtext = "1" Then
                    btnAbondon.Text = objclsUtilities.ExecuteQueryReturnSingleValue("select option_value from reservation_parameters where param_id=5306")
                    If btnAbondon.Text.Trim = "" Then
                        btnAbondon.Text = "CANCEL"
                    End If
                End If



                If Not Request.QueryString("ShiftWarning") Is Nothing Then
                    If Request.QueryString("ShiftWarning") = "1" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Check In / Check out should be related to earlier hotel booked. So please change Shifting Hotel Date.")
                    End If
                End If


            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("HomeSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
            End Try
        Else
            'Loading menu in postback if sFinalbooked is not nothing 'changed by mohamed on 12/08/2018
            If Not Session("sFinalBooked") Is Nothing Then
                LoadMenus()
            End If
        End If

        Dim str As String = ""

    End Sub

    Sub sbDisableBookingButtonAndMenu() 'Changed by mohamed on 01/08/2018
        Dim dt As New DataTable
        dt = objBLLguest.SetConfirmDetFromDataTable("execute sp_get_bookingtype '" & GetNewOrExistingRequestId() & "', 0")
        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("bookingtype") = "FREEFORM" Then
                lnkAccom.Disabled = True
                lnkTours.Disabled = True
                lnkAirport.Disabled = True
                lnkTransfer.Disabled = True
                lnkVisa.Disabled = True
                lnkOtherServices.Disabled = True

                lnkAccom.Attributes.Add("onclick", "alert('Please use FreeForm');return false;")
                lnkTours.Attributes.Add("onclick", "alert('Please use FreeForm');return false;")
                lnkAirport.Attributes.Add("onclick", "alert('Please use FreeForm');return false;")
                lnkTransfer.Attributes.Add("onclick", "alert('Please use FreeForm');return false;")
                lnkVisa.Attributes.Add("onclick", "alert('Please use FreeForm');return false;")
                lnkOtherServices.Attributes.Add("onclick", "alert('Please use FreeForm');return false;")

                lnkAccom.Style.Add("display", "none")
                lnkTours.Style.Add("display", "none")
                lnkAirport.Style.Add("display", "none")
                lnkTransfer.Style.Add("display", "none")
                lnkVisa.Style.Add("display", "none")
                lnkOtherServices.Style.Add("display", "none")
            End If
        End If
    End Sub

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
        ' Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
        hdnrlineno.Value = lblrlineno.Text
        Dim objBLLHotelSearch = New BLLHotelSearch


        'End If

        If Not Session("sRequestId") Is Nothing Then ' modified by abin on 20180724
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataFromDataTable("select  * from booking_hotel_detail_remarkstemp  where requestid= '" & Session("sRequestId") & "' and rlineno ='" & lblrlineno.Text & " 'order by requestid")


            If dt.Rows.Count > 0 Then

                Dim remarkstemplate As String()
                remarkstemplate = dt.Rows(0).Item("remarkstemplate").Split(";")


                For Each remark As String In remarkstemplate
                    For Each item As ListItem In chkHotelRemarks.Items
                        If item.Value = remark Then
                            item.Selected = True
                            Exit For
                        End If
                    Next
                Next

                If dt.Rows(0).Item("remarkstemplate") = "" Then
                    dvHotelRemarkshead.Style.Add("diplay", "none")
                Else
                    dvHotelRemarkshead.Style.Add("diplay", "block")
                End If

                txthotremarks.Text = dt.Rows(0).Item("hotelremarks")
                txtcustRemarks.Text = dt.Rows(0).Item("agentremarks")
                txtArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
                txtDeptRemarks.Text = dt.Rows(0).Item("departureremarks")


            End If
        End If
    End Sub

    Protected Sub imgbVisaConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim myButton As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
        lblCVisaType.Text = CType(dlItem.FindControl("lblVisaTypeName"), Label).Text
        lblClblVisaDate.Text = CType(dlItem.FindControl("lblvisadate"), Label).Text
        lblCVisaNationality.Text = CType(dlItem.FindControl("lblNationality"), Label).Text

        ' lblvehiclename
        Dim dt As New DataTable
        Dim rlineno As String = CType(dlItem.FindControl("lblvlineno"), Label).Text

        lblVisaLineNo.Text = rlineno
        Dim strRequestId As String = GetNewOrExistingRequestId()
        dt = objBLLguest.GetVisaConfirmSummary(strRequestId, rlineno)
        gvVisaConfirm.DataSource = dt
        gvVisaConfirm.DataBind()
        FillVisaConfirmSummaryDetails(strRequestId, rlineno)
        mpVisaConfirm.Show()
    End Sub

    Protected Sub imgbOthersConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim myButton As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
        lblCOthersType.Text = CType(dlItem.FindControl("lblairporttype"), Label).Text
        lblClblOtherdate.Text = CType(dlItem.FindControl("lblothservicedate"), Label).Text
        lblCOtherServicename.Text = CType(dlItem.FindControl("lblOthersServiceName"), Label).Text

        ' lblvehiclename
        Dim dt As New DataTable
        Dim rlineno As String = CType(dlItem.FindControl("lblolineno"), Label).Text

        lblOthersLineNo.Text = rlineno
        Dim strRequestId As String = GetNewOrExistingRequestId()
        dt = objBLLguest.GetOthersConfirmSummary(strRequestId, rlineno)
        gvOthersConfirm.DataSource = dt
        gvOthersConfirm.DataBind()
        FillOthersConfirmSummaryDetails(strRequestId, rlineno)
        mpOthersConfirm.Show()
    End Sub

    Protected Sub imgbAirportmateConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim myButton As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
        lblCAirportMateType.Text = CType(dlItem.FindControl("lblairporttype"), Label).Text
        lblClblAirportMateDate.Text = CType(dlItem.FindControl("lblservicedate"), Label).Text
        lblCServiceName.Text = CType(dlItem.FindControl("lblairservicename"), Label).Text

        ' lblCVehicleName.Text = CType(dlItem.FindControl("lblvehiclename"), Label).Text
        'lblToursDetails.Text = CType(dlItem.FindControl("lblToursDetails"), Label).Text
        ' lblvehiclename
        Dim dt As New DataTable
        Dim rlineno As String = CType(dlItem.FindControl("lblalineno"), Label).Text

        lblAirportMateLineNo.Text = rlineno
        Dim strRequestId As String = GetNewOrExistingRequestId()
        dt = objBLLguest.GetAirportMateConfirmSummary(strRequestId, rlineno)
        gvAirportMateConfirm.DataSource = dt
        gvAirportMateConfirm.DataBind()
        FillAirportmateConfirmSummaryDetails(strRequestId, rlineno)
        mpAirportMateConfirm.Show()
    End Sub


    Private Sub FillOthersConfirmSummaryDetails(ByVal strRequestId As String, ByVal strRLineNo As String)
        Dim dt As New DataTable
        dt = objBLLguest.GetOthersConfirmSummaryDetails(strRequestId, strRLineNo)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim lblRowNo As Label = CType(gvOthersConfirm.Rows(i).FindControl("lblRowNo"), Label)
                Dim txtothersconfno As TextBox = CType(gvOthersConfirm.Rows(i).FindControl("txtothersconfno"), TextBox)
                Dim txtConfirmDate As TextBox = CType(gvOthersConfirm.Rows(i).FindControl("txtConfirmDate"), TextBox)
                Dim txtPrevConfNo As TextBox = CType(gvOthersConfirm.Rows(i).FindControl("txtPrevConfNo"), TextBox)
                Dim txtTimeLimitDate As TextBox = CType(gvOthersConfirm.Rows(i).FindControl("txtTimeLimitDate"), TextBox)
                Dim hdnPrevConfNo As HiddenField = CType(gvOthersConfirm.Rows(i).FindControl("hdnPrevConfNo"), HiddenField)
                Dim txtTimeLimitTime As HtmlInputText = CType(gvOthersConfirm.Rows(i).FindControl("txtTimeLimitTime"), HtmlInputText)
                Dim txtCancelDays As TextBox = CType(gvOthersConfirm.Rows(i).FindControl("txtCancelDays"), TextBox)

                For rowindex As Integer = 0 To gvOthersConfirm.Columns.Count - 1
                    If gvOthersConfirm.Columns(rowindex).HeaderText.Trim = "Prev. Confirmation No." Then
                        gvOthersConfirm.Columns(rowindex).Visible = True
                        dvCancelheading.Style.Add("width", "375px")
                        dvCancelheading.Style.Add("margin-left", "520px")
                    End If
                Next
                '    If dt.Rows(i).Item("rlineno").ToString = lblRowNo.Text Then
                txtothersconfno.Text = dt.Rows(i).Item("othersconfno").ToString
                txtConfirmDate.Text = dt.Rows(i).Item("confirmdate").ToString
                txtPrevConfNo.Text = dt.Rows(i).Item("prevconfno").ToString
                txtTimeLimitDate.Text = dt.Rows(i).Item("timelimitdate").ToString
                txtTimeLimitTime.Value = dt.Rows(i).Item("timelimittime").ToString
                txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblClblOtherdate.Text, Date))
                hdnPrevConfNo.Value = dt.Rows(i).Item("othersconfno").ToString
                ' End If

            Next

        Else

        End If

    End Sub


    Private Sub FillVisaConfirmSummaryDetails(ByVal strRequestId As String, ByVal strRLineNo As String)
        Dim dt As New DataTable
        dt = objBLLguest.GetVisaConfirmSummaryDetails(strRequestId, strRLineNo)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim lblRowNo As Label = CType(gvVisaConfirm.Rows(i).FindControl("lblRowNo"), Label)
                Dim txtvisaconfno As TextBox = CType(gvVisaConfirm.Rows(i).FindControl("txtvisaconfno"), TextBox)
                Dim txtConfirmDate As TextBox = CType(gvVisaConfirm.Rows(i).FindControl("txtConfirmDate"), TextBox)
                Dim txtPrevConfNo As TextBox = CType(gvVisaConfirm.Rows(i).FindControl("txtPrevConfNo"), TextBox)
                Dim txtTimeLimitDate As TextBox = CType(gvVisaConfirm.Rows(i).FindControl("txtTimeLimitDate"), TextBox)
                Dim hdnPrevConfNo As HiddenField = CType(gvVisaConfirm.Rows(i).FindControl("hdnPrevConfNo"), HiddenField)
                Dim txtTimeLimitTime As HtmlInputText = CType(gvVisaConfirm.Rows(i).FindControl("txtTimeLimitTime"), HtmlInputText)
                Dim txtCancelDays As TextBox = CType(gvVisaConfirm.Rows(i).FindControl("txtCancelDays"), TextBox)

                For rowindex As Integer = 0 To gvVisaConfirm.Columns.Count - 1
                    If gvVisaConfirm.Columns(rowindex).HeaderText.Trim = "Prev. Confirmation No." Then
                        gvVisaConfirm.Columns(rowindex).Visible = True
                        dvCancelheading.Style.Add("width", "375px")
                        dvCancelheading.Style.Add("margin-left", "520px")
                    End If
                Next
                '    If dt.Rows(i).Item("rlineno").ToString = lblRowNo.Text Then
                txtvisaconfno.Text = dt.Rows(i).Item("visaconfno").ToString
                txtConfirmDate.Text = dt.Rows(i).Item("confirmdate").ToString
                txtPrevConfNo.Text = dt.Rows(i).Item("prevconfno").ToString
                txtTimeLimitDate.Text = dt.Rows(i).Item("timelimitdate").ToString
                txtTimeLimitTime.Value = dt.Rows(i).Item("timelimittime").ToString
                txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblClblVisaDate.Text, Date))
                hdnPrevConfNo.Value = dt.Rows(i).Item("visaconfno").ToString
                ' End If

            Next

        Else

        End If

    End Sub

    Private Sub FillAirportmateConfirmSummaryDetails(ByVal strRequestId As String, ByVal strRLineNo As String)
        Dim dt As New DataTable
        dt = objBLLguest.GetAirportMateConfirmSummaryDetails(strRequestId, strRLineNo)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim lblRowNo As Label = CType(gvAirportMateConfirm.Rows(i).FindControl("lblRowNo"), Label)
                Dim txtAirportmateconfno As TextBox = CType(gvAirportMateConfirm.Rows(i).FindControl("txtAirportmateconfno"), TextBox)
                Dim txtConfirmDate As TextBox = CType(gvAirportMateConfirm.Rows(i).FindControl("txtConfirmDate"), TextBox)
                Dim txtPrevConfNo As TextBox = CType(gvAirportMateConfirm.Rows(i).FindControl("txtPrevConfNo"), TextBox)
                Dim txtTimeLimitDate As TextBox = CType(gvAirportMateConfirm.Rows(i).FindControl("txtTimeLimitDate"), TextBox)
                Dim hdnPrevConfNo As HiddenField = CType(gvAirportMateConfirm.Rows(i).FindControl("hdnPrevConfNo"), HiddenField)
                Dim txtTimeLimitTime As HtmlInputText = CType(gvAirportMateConfirm.Rows(i).FindControl("txtTimeLimitTime"), HtmlInputText)
                Dim txtCancelDays As TextBox = CType(gvAirportMateConfirm.Rows(i).FindControl("txtCancelDays"), TextBox)

                For rowindex As Integer = 0 To gvAirportMateConfirm.Columns.Count - 1
                    If gvAirportMateConfirm.Columns(rowindex).HeaderText.Trim = "Prev. Confirmation No." Then
                        gvAirportMateConfirm.Columns(rowindex).Visible = True
                        dvCancelheading.Style.Add("width", "375px")
                        dvCancelheading.Style.Add("margin-left", "520px")
                    End If
                Next
                '    If dt.Rows(i).Item("rlineno").ToString = lblRowNo.Text Then
                txtAirportmateconfno.Text = dt.Rows(i).Item("airportmateconfno").ToString
                txtConfirmDate.Text = dt.Rows(i).Item("confirmdate").ToString
                txtPrevConfNo.Text = dt.Rows(i).Item("prevconfno").ToString
                txtTimeLimitDate.Text = dt.Rows(i).Item("timelimitdate").ToString
                txtTimeLimitTime.Value = dt.Rows(i).Item("timelimittime").ToString
                txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblClblAirportMateDate.Text, Date))
                hdnPrevConfNo.Value = dt.Rows(i).Item("airportmateconfno").ToString
                ' End If

            Next

        Else

        End If



    End Sub


    Protected Sub imgbTourConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim myButton As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
        lblCToursType.Text = CType(dlItem.FindControl("lblExcursionName"), Label).Text
        lblClblTourdate.Text = CType(dlItem.FindControl("lblexcursiondate"), Label).Text
        ' lblCVehicleName.Text = CType(dlItem.FindControl("lblvehiclename"), Label).Text
        'lblToursDetails.Text = CType(dlItem.FindControl("lblToursDetails"), Label).Text
        ' lblvehiclename
        Dim dt As New DataTable
        Dim rlineno As String = CType(dlItem.FindControl("lblelineno"), Label).Text
        lblTrfLineNo.Text = rlineno
        lblToursLineNo.Text = rlineno
        Dim strRequestId As String = GetNewOrExistingRequestId()
        dt = objBLLguest.GetToursConfirmSummary(strRequestId, rlineno)
        gvToursConfirm.DataSource = dt
        gvToursConfirm.DataBind()
        FillToursConfirmSummaryDetails(strRequestId, rlineno)
        mpToursConfirm.Show()
    End Sub
    Private Sub FillToursConfirmSummaryDetails(ByVal strRequestId As String, ByVal strRLineNo As String)
        Dim dt As New DataTable
        dt = objBLLguest.GetToursConfirmSummaryDetails(strRequestId, strRLineNo)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim lblRowNo As Label = CType(gvToursConfirm.Rows(i).FindControl("lblRowNo"), Label)
                Dim txtToursconfno As TextBox = CType(gvToursConfirm.Rows(i).FindControl("txtToursconfno"), TextBox)
                Dim txtConfirmDate As TextBox = CType(gvToursConfirm.Rows(i).FindControl("txtConfirmDate"), TextBox)
                Dim txtPrevConfNo As TextBox = CType(gvToursConfirm.Rows(i).FindControl("txtPrevConfNo"), TextBox)
                Dim txtTimeLimitDate As TextBox = CType(gvToursConfirm.Rows(i).FindControl("txtTimeLimitDate"), TextBox)
                Dim hdnPrevConfNo As HiddenField = CType(gvToursConfirm.Rows(i).FindControl("hdnPrevConfNo"), HiddenField)
                Dim txtTimeLimitTime As HtmlInputText = CType(gvToursConfirm.Rows(i).FindControl("txtTimeLimitTime"), HtmlInputText)
                Dim txtCancelDays As TextBox = CType(gvToursConfirm.Rows(i).FindControl("txtCancelDays"), TextBox)

                For rowindex As Integer = 0 To gvToursConfirm.Columns.Count - 1
                    If gvToursConfirm.Columns(rowindex).HeaderText.Trim = "Prev. Confirmation No." Then
                        gvToursConfirm.Columns(rowindex).Visible = True
                        dvCancelheading.Style.Add("width", "375px")
                        dvCancelheading.Style.Add("margin-left", "520px")
                    End If
                Next
                '    If dt.Rows(i).Item("rlineno").ToString = lblRowNo.Text Then
                txtToursconfno.Text = dt.Rows(i).Item("Toursconfno").ToString
                txtConfirmDate.Text = dt.Rows(i).Item("confirmdate").ToString
                txtPrevConfNo.Text = dt.Rows(i).Item("prevconfno").ToString
                txtTimeLimitDate.Text = dt.Rows(i).Item("timelimitdate").ToString
                txtTimeLimitTime.Value = dt.Rows(i).Item("timelimittime").ToString
                Try
                    txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblClblTourdate.Text, Date))
                Catch ex As Exception

                End Try

                hdnPrevConfNo.Value = dt.Rows(i).Item("Toursconfno").ToString
                ' End If

            Next

        Else

        End If



    End Sub
    Protected Sub imgTrfConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim myButton As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)
        lblCTransferType.Text = CType(dlItem.FindControl("lblExcursionName"), Label).Text
        lblClbltransdate.Text = CType(dlItem.FindControl("lbltransdate"), Label).Text
        lblCVehicleName.Text = CType(dlItem.FindControl("lblvehiclename"), Label).Text
        lblTransferDetails.Text = CType(dlItem.FindControl("lblTransferDetails"), Label).Text
        ' lblvehiclename
        Dim dt As New DataTable
        Dim rlineno As String = CType(dlItem.FindControl("lbltlineno"), Label).Text
        lblTrfLineNo.Text = rlineno
        Dim strRequestId As String = GetNewOrExistingRequestId()
        dt = objBLLguest.GetTransferConfirmSummary(strRequestId, rlineno)
        gvTransferConfirm.DataSource = dt
        gvTransferConfirm.DataBind()
        FillTransferConfirmSummaryDetails(strRequestId, rlineno)
        mpTransferConfirm.Show()

    End Sub
    Protected Sub imgbConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs)

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
    Private Sub FillConfirmSummaryDetails()
        Dim dt As New DataTable
        dt = objBLLguest.SetConfirmDetFromDataTable("select rlineno,roomno,hotelconfno,confirmby,convert(varchar(10),isnull(confirmdate,getdate()),103) as confirmdate,prevconfno, " _
            & " convert(varchar(10),timelimit,103) as timelimitdate,case when convert(varchar(5), timelimit, 108) ='00:00' then convert(varchar(5), isnull(null,getdate()), 108)  else  convert(varchar(5), timelimit, 108) end as timelimittime from booking_hotel_detail_confcanceltemp  where requestid='" & GetNewOrExistingRequestId() & "' and rlineno='" & lbldlrlineno.Text & "'")

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
                If txtTimeLimitDate.Text <> "" Then
                    txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblCheckInDate.Text, Date)) - 1 ' Srinivas wants to add 1 day as per his email 26/08/17 
                End If

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
    Private Sub FillTransferConfirmSummaryDetails(ByVal strRequestId As String, ByVal strRLineNo As String)
        Dim dt As New DataTable
        dt = objBLLguest.GetTransferConfirmSummaryDetails(strRequestId, strRLineNo)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim lblRowNo As Label = CType(gvTransferConfirm.Rows(i).FindControl("lblRowNo"), Label)
                Dim txttransferconfno As TextBox = CType(gvTransferConfirm.Rows(i).FindControl("txttransferconfno"), TextBox)
                Dim txtConfirmDate As TextBox = CType(gvTransferConfirm.Rows(i).FindControl("txtConfirmDate"), TextBox)
                Dim txtPrevConfNo As TextBox = CType(gvTransferConfirm.Rows(i).FindControl("txtPrevConfNo"), TextBox)
                Dim txtTimeLimitDate As TextBox = CType(gvTransferConfirm.Rows(i).FindControl("txtTimeLimitDate"), TextBox)
                Dim hdnPrevConfNo As HiddenField = CType(gvTransferConfirm.Rows(i).FindControl("hdnPrevConfNo"), HiddenField)
                Dim txtTimeLimitTime As HtmlInputText = CType(gvTransferConfirm.Rows(i).FindControl("txtTimeLimitTime"), HtmlInputText)
                Dim txtCancelDays As TextBox = CType(gvTransferConfirm.Rows(i).FindControl("txtCancelDays"), TextBox)

                For rowindex As Integer = 0 To gvConfirmBooking.Columns.Count - 1
                    If gvTransferConfirm.Columns(rowindex).HeaderText.Trim = "Prev. Confirmation No." Then
                        gvTransferConfirm.Columns(rowindex).Visible = True
                        dvCancelheading.Style.Add("width", "375px")
                        dvCancelheading.Style.Add("margin-left", "520px")
                    End If
                Next
                '    If dt.Rows(i).Item("rlineno").ToString = lblRowNo.Text Then
                txttransferconfno.Text = dt.Rows(i).Item("transferconfno").ToString
                txtConfirmDate.Text = dt.Rows(i).Item("confirmdate").ToString
                txtPrevConfNo.Text = dt.Rows(i).Item("prevconfno").ToString
                txtTimeLimitDate.Text = dt.Rows(i).Item("timelimitdate").ToString
                txtTimeLimitTime.Value = dt.Rows(i).Item("timelimittime").ToString
                txtCancelDays.Text = DateDiff(DateInterval.Day, CType(txtTimeLimitDate.Text, Date), CType(lblClbltransdate.Text, Date))
                hdnPrevConfNo.Value = dt.Rows(i).Item("transferconfno").ToString
                ' End If

            Next

        Else

            'For i = 0 To gvTransferConfirm.Columns.Count - 1

            '    If gvTransferConfirm.Columns(i).HeaderText.Trim = "Prev. Confirmation No." Then

            '        gvTransferConfirm.Columns(i).Visible = False
            '        dvCancelheading.Style.Add("width", "290px")
            '        dvCancelheading.Style.Add("margin-left", "605px")

            '    End If
            'Next
        End If



    End Sub
    Private Sub FillToursRemarksPopUp(ByVal sender As Object)
        '  Dim strsqlqry As String

        Dim myButton As ImageButton = CType(sender, ImageButton)

        Dim dlItem As DataListItem = CType((myButton).NamingContainer, DataListItem)

        Dim lbldlistelineno As Label = CType(dlItem.FindControl("lblelineno"), Label)

        Dim lblexcursionname As Label = CType(dlItem.FindControl("lblexcursionname"), Label)
        Dim lblexcursiondate As Label = CType(dlItem.FindControl("lblexcursiondate"), Label)

        ToursHdnElineno.Value = lbldlistelineno.Text
        ' Dim objBLLHotelSearch = New BLLHotelSearch

        lblToursRemarksheading.Text = lblexcursionname.Text

        lbltoursdate.Text = lblexcursiondate.Text
        Dim dt As New DataTable
        dt = objBLLguest.SetRemarksDetFromDataTable("select  * from booking_tours_remarkstemp  where requestid= '" & GetNewOrExistingRequestId() & "' and rlineno ='" & lbldlistelineno.Text & " 'order by requestid")

        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0).Item("Partyremarks")) Then
                txtToursPartyRemarks.Text = dt.Rows(0).Item("Partyremarks")
            End If
            If Not IsDBNull(dt.Rows(0).Item("agentremarks")) Then
                txtToursCustRemarks.Text = dt.Rows(0).Item("agentremarks")
            End If
            If Not IsDBNull(dt.Rows(0).Item("arrivalremarks")) Then
                txtToursArrRemarks.Text = dt.Rows(0).Item("arrivalremarks")
            End If
            If Not IsDBNull(dt.Rows(0).Item("departureremarks")) Then
                txtToursDeptRemarks.Text = dt.Rows(0).Item("departureremarks")
            End If





        End If
    End Sub

    Protected Sub imgbAmend_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim imgbAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgbAmend.NamingContainer, DataListItem)
        Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
        Dim hdRatePlanSource As HiddenField = CType(dlItem.FindControl("hdRatePlanSource"), HiddenField)

        If hdRatePlanSource.Value.Trim.ToUpper <> "" And hdRatePlanSource.Value.Trim.ToUpper <> "COLUMBUS" Then
            Session("AmendRatePlanSource") = hdRatePlanSource.Value.Trim.ToUpper
            imgbDelete_Click(imgbAmend, e)
            Exit Sub
        Else
            Session("AmendRatePlanSource") = ""
        End If
        Dim hdBookingMode As HiddenField = CType(dlItem.FindControl("hdBookingMode"), HiddenField)
        If hdBookingMode.Value <> "FreeForm" Then
            Response.Redirect("HotelSearch.aspx?RLineNo=" & lblrlineno.Text, False)
        Else
            Response.Redirect("HotelFreeFormBooking.aspx?RLineNo=" & lblrlineno.Text, False)
        End If

    End Sub
    Protected Sub imgTourAmend_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim imgbAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgbAmend.NamingContainer, DataListItem)
        Dim lblelineno As Label = CType(dlItem.FindControl("lblelineno"), Label)
        'Response.Redirect("TourSearch.aspx?ELineNo=" & lblelineno.Text & "")
        Dim hdBookingMode As HiddenField = CType(dlItem.FindControl("hdBookingMode"), HiddenField)
        If hdBookingMode.Value <> "FreeForm" Then
            Response.Redirect("TourSearch.aspx?ELineNo=" & lblelineno.Text, False)
        Else
            Dim sRequestId As String = Session("sRequestId")
            Dim dtAssignServiceLock As DataTable = objclsUtilities.GetDataFromDataTable("execute sp_CheckService_AssignStatus_individual '" + sRequestId + "','TOUR','" + lblelineno.Text + "' ")

            If dtAssignServiceLock.Rows.Count > 0 Then
                Dim strLockWarning As String = ""
                For i As Integer = 0 To dtAssignServiceLock.Rows.Count - 1
                    strLockWarning = strLockWarning & "</br>" & dtAssignServiceLock.Rows(i)("Warning").ToString
                Next
                MessageBox.ShowMessage(Page, MessageType.Warning, strLockWarning)
                Exit Sub

            End If

            Response.Redirect("ToursFreeFormBooking.aspx?OLineNo=" & lblelineno.Text, False)
        End If

    End Sub
    Protected Sub imgothEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim imgothAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgothAmend.NamingContainer, DataListItem)
        Dim lblolineno As Label = CType(dlItem.FindControl("lblolineno"), Label)
        'ADDED BY ABIN ON 2018730
        Dim hdBookingMode As HiddenField = CType(dlItem.FindControl("hdBookingMode"), HiddenField)
        If hdBookingMode.Value <> "FreeForm" Then
            Response.Redirect("OtherSearch.aspx?OLineNo=" & lblolineno.Text, False)
        Else
            Response.Redirect("OtherServiceFreeFormBooking.aspx?OLineNo=" & lblolineno.Text, False)
        End If

    End Sub
    Protected Sub imgAirEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim imgAirAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgAirAmend.NamingContainer, DataListItem)
        Dim lblalineno As Label = CType(dlItem.FindControl("lblalineno"), Label)
        Dim sRequestId As String = Session("sRequestId")
        Dim dtAssignServiceLock As DataTable = objclsUtilities.GetDataFromDataTable("execute sp_CheckService_AssignStatus_individual '" + sRequestId + "','AIRPORT','" + lblalineno.Text + "' ")

        If dtAssignServiceLock.Rows.Count > 0 Then
            Dim strLockWarning As String = ""
            For i As Integer = 0 To dtAssignServiceLock.Rows.Count - 1
                strLockWarning = strLockWarning & "</br>" & dtAssignServiceLock.Rows(i)("Warning").ToString
            Next
            MessageBox.ShowMessage(Page, MessageType.Warning, strLockWarning)
            Exit Sub

        End If

        'Added by abin on 20180724
        Dim hdBookingMode As HiddenField = CType(dlItem.FindControl("hdBookingMode"), HiddenField)
        If hdBookingMode.Value <> "FreeForm" Then
            Response.Redirect("AirportmeetSearch.aspx?ALineNo=" & lblalineno.Text, False)
        Else
            Response.Redirect("AirportMeetFreeFormBooking.aspx?ALineNo=" & lblalineno.Text, False)
        End If

    End Sub
    Protected Sub imgTrfEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim imgTrfAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgTrfAmend.NamingContainer, DataListItem)
        Dim lbltlineno As Label = CType(dlItem.FindControl("lbltlineno"), Label)

        Dim sRequestId As String = Session("sRequestId")
        Dim dtAssignServiceLock As DataTable = objclsUtilities.GetDataFromDataTable("execute sp_CheckService_AssignStatus_individual '" + sRequestId + "','TRANSFER','" + lbltlineno.Text + "' ")

        If dtAssignServiceLock.Rows.Count > 0 Then
            Dim strLockWarning As String = ""
            For i As Integer = 0 To dtAssignServiceLock.Rows.Count - 1
                strLockWarning = strLockWarning & "</br>" & dtAssignServiceLock.Rows(i)("Warning").ToString
            Next
            MessageBox.ShowMessage(Page, MessageType.Warning, strLockWarning)
            Exit Sub

        End If


        Dim hdBookingMode As HiddenField = CType(dlItem.FindControl("hdBookingMode"), HiddenField)
        If hdBookingMode.Value = "FreeForm" Then
            Response.Redirect("TransferFreeFormBooking.aspx?TLineNo=" & lbltlineno.Text & "")
        Else
            Response.Redirect("TransferSearch.aspx?TLineNo=" & lbltlineno.Text & "")
        End If

    End Sub
    Protected Sub imgVisaEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim imgVisaAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgVisaAmend.NamingContainer, DataListItem)
        Dim lblvlineno As Label = CType(dlItem.FindControl("lblvlineno"), Label)
        ' Response.Redirect("VisaBooking.aspx?VLineNo=" & lblvlineno.Text & "")


        Dim sRequestId As String = Session("sRequestId")
        Dim dtAssignServiceLock As DataTable = objclsUtilities.GetDataFromDataTable("execute sp_CheckService_AssignStatus_individual '" + sRequestId + "','VISA','" + lblvlineno.Text + "' ")

        If dtAssignServiceLock.Rows.Count > 0 Then
            Dim strLockWarning As String = ""
            For i As Integer = 0 To dtAssignServiceLock.Rows.Count - 1
                strLockWarning = strLockWarning & "</br>" & dtAssignServiceLock.Rows(i)("Warning").ToString
            Next
            MessageBox.ShowMessage(Page, MessageType.Warning, strLockWarning)
            Exit Sub

        End If


        Dim hdBookingMode As HiddenField = CType(dlItem.FindControl("hdBookingMode"), HiddenField)
        If hdBookingMode.Value <> "FreeForm" Then
            Response.Redirect("VisaBooking.aspx?VLineNo=" & lblvlineno.Text & "")
        Else
            Response.Redirect("VisaBooking.aspx?VLineNo=" & lblvlineno.Text & "&FreeForm=1")
        End If

    End Sub

    Protected Sub imgbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim imgbAmend As ImageButton = CType(sender, ImageButton)
            dvPackageDetails.Style.Add("display", "none")
            Session("sdsPackageSummary") = Nothing
            Session("connectCancelMarkupDt") = Nothing
            Session("cancelRatePlanSource") = ""
            Session("CancelAndAmendRatePlanSource") = ""
            Dim dlItem As DataListItem = CType(imgbAmend.NamingContainer, DataListItem)
            Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
            Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
            If Not Session("sEditRequestId") Is Nothing Then
                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    Dim lblroomtype As Label = CType(dlItem.FindControl("lblroomtype"), Label)
                    Dim txtTimeLimitTime As TextBox = CType(dlItem.FindControl("txtTimeLimitTime"), TextBox)
                    Dim lblchkindateformat As Label = CType(dlItem.FindControl("lblchkindateformat"), Label)
                    Dim lblchkoutdateformat As Label = CType(dlItem.FindControl("lblchkoutdateformat"), Label)
                    Dim hdRatePlanSource As HiddenField = CType(dlItem.FindControl("hdRatePlanSource"), HiddenField)
                    Dim lblHotelName As Label = CType(dlItem.FindControl("lblHotelName"), Label)

                    If hdRatePlanSource.Value.Trim.ToUpper <> "" And hdRatePlanSource.Value.Trim.ToUpper <> "COLUMBUS" Then  'onedmc
                        If CType(Session("AmendRatePlanSource"), String) <> "" Then
                            Session("CancelAndAmendRatePlanSource") = hdRatePlanSource.Value.Trim.ToUpper
                        Else
                            Session("CancelAndAmendRatePlanSource") = ""
                        End If
                        Session("AmendRatePlanSource") = ""
                        Session("cancelRatePlanSource") = hdRatePlanSource.Value.Trim.ToUpper

                        hdCanceltype.Value = CType(dlItem.FindControl("hdCancelled"), HiddenField).Value
                        lblConnectHotelCheckInDate.Text = lblchkindateformat.Text
                        lblConnectHotelCheckOutDate.Text = lblchkoutdateformat.Text
                        lblConnectHotelCancelHeading.Text = lblroomtype.Text
                        lblConnectHotelName.Text = lblHotelName.Text
                        txtConnectHotelRlineNo.Text = lblrlineno.Text
                        Dim dt As DataTable
                        dt = objBLLHotelSearch.GetHotelCancelDetails(Session("sRequestId"), lblrlineno.Text)
                        If dt.Rows.Count > 0 Then
                            Dim strQry As String = "select a.confirmid,h.sourcectrycode,c.timelimit from booking_hotel_detail_api_temp a(nolock) inner join " _
                            & "booking_headertemp h(nolock) on a.requestid=h.requestid inner join booking_hotel_detail_confcanceltemp c(nolock) " _
                            & "on a.requestid=c.requestid and a.rlineno=c.rlineno where a.requestid='" & Session("sRequestId") & "' and a.rlineno=" & lblrlineno.Text & " group by a.confirmId,h.sourcectrycode,c.timelimit"
                            Dim confirmDt As DataTable = objclsUtilities.GetDataFromDataTable(strQry)
                            If Not confirmDt Is Nothing AndAlso confirmDt.Rows.Count > 0 Then
                                Dim cancelTimelimit As Nullable(Of Date)
                                If Not IsDBNull(confirmDt.Rows(0)("timelimit")) Then
                                    cancelTimelimit = Convert.ToDateTime(confirmDt.Rows(0)("timelimit"))
                                Else
                                    cancelTimelimit = Nothing
                                End If
                                If cancelTimelimit Is Nothing Or cancelTimelimit >= Date.Now.Date Then
                                    Dim decimalplaces As Integer = CType(objclsUtilities.ExecuteQueryReturnSingleValue("select option_selected from reservation_parameters where param_id=509"), Integer)
                                    Dim cancelCostPrice As Decimal = 0
                                    Dim cancelDate As Date = Date.Now.Date
                                    Dim cancelCostCurrency As String = ""
                                    Dim hotelBookingInfo As APIHotelBookingInfoResponse.HotelBookingInfoResponse = objApiController.CallHotelBookingInfo(confirmDt.Rows(0)("confirmid").ToString(), confirmDt.Rows(0)("sourcectrycode").ToString())
                                    If hotelBookingInfo IsNot Nothing Then
                                        cancelCostCurrency = hotelBookingInfo.result.currency
                                        If hotelBookingInfo.result.cancelConditions IsNot Nothing AndAlso hotelBookingInfo.result.cancelConditions.Count > 0 Then
                                            For Each cancelList In hotelBookingInfo.result.cancelConditions
                                                Dim startDate As Date = Convert.ToDateTime(cancelList.dateTime).ToString("yyyy-MM-dd")
                                                Dim costPrice As Decimal = cancelList.netPenaltyFee
                                                If cancelDate >= startDate Then
                                                    cancelCostPrice = costPrice
                                                End If
                                            Next
                                        End If
                                    End If
                                    If cancelCostPrice > 0 Then
                                        Dim param(5) As SqlParameter
                                        param(0) = New SqlParameter("@requestid", Session("sRequestId"))
                                        param(1) = New SqlParameter("@rlineno", lblrlineno.Text)
                                        param(2) = New SqlParameter("@cost", cancelCostPrice)
                                        param(3) = New SqlParameter("@userlogged", Session("GlobalUserName"))
                                        param(4) = New SqlParameter("@clientCode", "oneDMC")
                                        param(5) = New SqlParameter("@costCurrCode", cancelCostCurrency)
                                        Dim resultDt As DataTable = objclsUtilities.GetDataTable("sp_get_cancelBooking_markup", param)
                                        If resultDt.Rows.Count > 0 Then
                                            If resultDt.Rows(0)("salevalue") Is Nothing AndAlso resultDt.Rows(0)("salevalue") = 0 Then
                                                Throw New Exception("Can not process this booking. Please contact reservation officer")
                                                Exit Sub
                                            End If
                                            txtCancelPrice.Text = Math.Round(Convert.ToDecimal(resultDt.Rows(0)("salevalue")), decimalplaces)
                                            lblCancelPrice.Text = "Cancellation Charge " + resultDt.Rows(0)("agentcurrcode")
                                            Session("connectCancelMarkupDt") = resultDt
                                        Else
                                            Throw New Exception("Can not process this booking. Please contact reservation officer")
                                            Exit Sub
                                        End If
                                        txtCancelPrice.Enabled = False
                                        dvCancelPrice.Visible = True
                                        dvCancelWithOutCharge.Visible = False
                                    Else
                                        txtCancelPrice.Text = ""
                                        dvCancelPrice.Visible = False
                                    End If
                                    btnConnectHotelCancelSave.Visible = True
                                Else
                                    btnConnectHotelCancelSave.Visible = False
                                End If
                            End If

                            gvConnectHotelCancel.DataSource = dt
                            gvConnectHotelCancel.DataBind()

                            mpConnectHotelCancel.Show()
                        End If
                    Else
                        hdCanceltype.Value = CType(dlItem.FindControl("hdCancelled"), HiddenField).Value
                        lblHotelCheckInDate.Text = lblchkindateformat.Text
                        lblHotelCheckOutDate.Text = lblchkoutdateformat.Text
                        lblHotelCancelHeading.Text = lblroomtype.Text

                        Session("cancelRatePlanSource") = ""
                        Dim dt As DataTable
                        dt = objBLLHotelSearch.GetHotelCancelDetails(Session("sRequestId"), lblrlineno.Text)
                        If dt.Rows.Count > 0 Then

                            gvHotelCancel.DataSource = dt
                            gvHotelCancel.DataBind()

                            mpHotelCancel.Show()
                        End If
                    End If
                Else
                    Dim str As String = objBLLHotelSearch.RemoveHotelBooking(Session("sRequestId"), lblrlineno.Text)
                    BindBookingSummary()
                    BindTotalValue()
                End If

            Else

                Dim str As String = objBLLHotelSearch.RemoveHotelBooking(Session("sRequestId"), lblrlineno.Text)
                BindBookingSummary()
                BindTotalValue()


            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("moreServices.aspx :: imgbDelete_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Sub BindTotalValue()
        Dim dt As DataTable
        If Not Session("sEditRequestId") Is Nothing Then
            dt = objcommonfunctions.GetBookingvalue(Session("sEditRequestId"), objResParam.WhiteLabel)
        Else
            dt = objcommonfunctions.GetBookingvalue(Session("sRequestId"), objResParam.WhiteLabel)
        End If

        If dt.Rows.Count > 0 Then
            lbltotalbooking.Text = dt.Rows(0)("totalbookingvalue").ToString
        End If

        If hdBookingEngineRateType.Value = "1" Then
            divtotal.Style.Add("display", "none")
        Else
            divtotal.Style.Add("display", "block")
        End If
    End Sub
    Protected Sub imgTourRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgbAmend As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgbAmend.NamingContainer, DataListItem)
        Dim lblelineno As Label = CType(dlItem.FindControl("lblelineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"


        Removetours(Session("sRequestId"), lblelineno.Text)

        BindTourSummary()

        BindTotalValue()


    End Sub
    Protected Sub imgAirDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgAirDelete As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgAirDelete.NamingContainer, DataListItem)
        Dim lblalineno As Label = CType(dlItem.FindControl("lblalineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"

        Dim objBLLBLLMASearch As New BLLMASearch
        objBLLBLLMASearch.RemoveAirportservice(Session("sRequestId"), lblalineno.Text)

        BindAirportserviceSummary()

        BindTotalValue()

    End Sub
    Protected Sub imgTrfDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgTrfDelete As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgTrfDelete.NamingContainer, DataListItem)
        Dim lbltlineno As Label = CType(dlItem.FindControl("lbltlineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"
        Dim objBLLTransferSearch As New BLLTransferSearch


        objBLLTransferSearch.RemoveTransfers(Session("sRequestId"), lbltlineno.Text)
        BindTransferSummary()
        BindTotalValue()


    End Sub
    Protected Sub imgothercancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgothercancel As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgothercancel.NamingContainer, DataListItem)
        Dim lblolineno As Label = CType(dlItem.FindControl("lblolineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"
        Dim objBLLOtherSearch As New BLLOtherSearch

        lblservice.Text = CType(dlItem.FindControl("lblOthersServiceName"), Label).Text
        lblservicecandate.Text = CType(dlItem.FindControl("lblothservicedate"), Label).Text


        Dim dt As DataTable
        dt = objBLLOtherSearch.GetOtherCancelDetails(Session("sEditRequestId"), lblolineno.Text)
        If dt.Rows.Count > 0 Then
            gvotherCancel.DataSource = dt
            gvotherCancel.DataBind()
            mpotherCancel.Show()
        End If


    End Sub
    Protected Sub imgtourcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgtourcancel As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgtourcancel.NamingContainer, DataListItem)
        Dim lblelineno As Label = CType(dlItem.FindControl("lblelineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"
        Dim objBLLTourSearch As New BLLTourSearch

        lblCantour.Text = CType(dlItem.FindControl("lblExcursionName"), Label).Text
        lbltourcandate.Text = CType(dlItem.FindControl("lblexcursiondate"), Label).Text


        Dim dt As DataTable
        dt = objBLLTourSearch.GetTourCancelDetails(Session("sEditRequestId"), lblelineno.Text)
        If dt.Rows.Count > 0 Then
            gvtourCancel.DataSource = dt
            gvtourCancel.DataBind()
            mptourCancel.Show()
        End If


    End Sub
    Protected Sub imgTrfcancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgTrfCancel As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgTrfCancel.NamingContainer, DataListItem)
        Dim lbltlineno As Label = CType(dlItem.FindControl("lbltlineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"
        Dim objBLLTransferSearch As New BLLTransferSearch

        lblCanTransferType.Text = CType(dlItem.FindControl("lblExcursionName"), Label).Text
        lblCanlbltransdate.Text = CType(dlItem.FindControl("lbltransdate"), Label).Text
        lblCanVehicleName.Text = CType(dlItem.FindControl("lblvehiclename"), Label).Text
        lblcanTransferDetails.Text = CType(dlItem.FindControl("lblTransferDetails"), Label).Text

        Dim dt As DataTable
        dt = objBLLTransferSearch.GetTransferCancelDetails(Session("sEditRequestId"), lbltlineno.Text)
        If dt.Rows.Count > 0 Then
            gvTransferCancel.DataSource = dt
            gvTransferCancel.DataBind()
            mpTransferCancel.Show()
        End If


    End Sub

    Protected Sub imgAircancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgAircancel As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgAircancel.NamingContainer, DataListItem)
        Dim lblalineno As Label = CType(dlItem.FindControl("lblalineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"
        Dim objBLLMASearch As New BLLMASearch

        lblcancelirportMateType.Text = CType(dlItem.FindControl("lblairporttype"), Label).Text
        lblCancelAirportMateDate.Text = CType(dlItem.FindControl("lblservicedate"), Label).Text
        lblCancelServiceName.Text = CType(dlItem.FindControl("lblairservicename"), Label).Text

        Dim dt As DataTable
        dt = objBLLMASearch.GetAirportCancelDetails(Session("sEditRequestId"), lblalineno.Text)
        If dt.Rows.Count > 0 Then
            gvairCancel.DataSource = dt
            gvairCancel.DataBind()
            mpairCancel.Show()
        End If


    End Sub
    Protected Sub imgVisacancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgVisaCancel As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgVisaCancel.NamingContainer, DataListItem)
        Dim lblvlineno As Label = CType(dlItem.FindControl("lblvlineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"
        Dim objBLLBLLVISA As New BLLVISA

        lblCanVisaType.Text = CType(dlItem.FindControl("lblVisaTypeName"), Label).Text
        lblcanNation.Text = CType(dlItem.FindControl("lblNationality"), Label).Text
        lblvisacandate.Text = CType(dlItem.FindControl("lblvisadate"), Label).Text


        Dim dt As DataTable
        dt = objBLLBLLVISA.GetVisaCancelDetails(Session("sEditRequestId"), lblvlineno.Text)
        If dt.Rows.Count > 0 Then
            gvVisaCancel.DataSource = dt
            gvVisaCancel.DataBind()
            mpVisaCancel.Show()
        End If


    End Sub
    Protected Sub imgVisaDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgVisaDelete As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgVisaDelete.NamingContainer, DataListItem)
        Dim lblvlineno As Label = CType(dlItem.FindControl("lblvlineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"

        Dim objBLLVisa As New BLLVISA
        objBLLVisa.RemoveVisa(Session("sRequestId"), lblvlineno.Text)

        BindVisaSummary()

        BindTotalValue()

    End Sub

    Protected Sub imgothDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        dvPackageDetails.Style.Add("display", "none")
        Session("sdsPackageSummary") = Nothing
        Dim imgothDelete As ImageButton = CType(sender, ImageButton)
        Dim dlItem As DataListItem = CType(imgothDelete.NamingContainer, DataListItem)
        Dim lblolineno As Label = CType(dlItem.FindControl("lblolineno"), Label)
        Dim strmsg As String = "Are sure Want to Delete ?"

        Dim objBLLBLLOtherSearch As New BLLOtherSearch
        objBLLBLLOtherSearch.RemoveOthers(Session("sRequestId"), lblolineno.Text)
        ' Response.Redirect(Request.RawUrl)
        BindOtherserviceSummary()

        BindTotalValue()

    End Sub
    Private Sub Removetours(ByVal strRequestId As String, ByVal strelineno As String)
        Dim objBLLTourSearch As New BLLTourSearch
        objBLLTourSearch.RemoveTours(strRequestId, strelineno)

    End Sub

    Protected Sub imgbRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sRequestId") Is Nothing Then ' modified by abin on 20180724

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
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))

        End Try
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
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub ImgOthServRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sRequestId") Is Nothing Then ' modified by abin on 20180724

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
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
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
    Protected Sub ImgToursRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sRequestId") Is Nothing Then ' modified by abin on 20180724

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
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub cleartextboxes(ByVal txt1 As TextBox, ByVal txt2 As TextBox, ByVal txt3 As TextBox, ByVal txt4 As TextBox)
        txt1.Text = ""
        txt2.Text = ""
        txt3.Text = ""
        txt4.Text = ""
    End Sub
    Private Sub clearcontrols()
        For Each item As ListItem In chkHotelRemarks.Items
            item.Selected = False
        Next
        txtArrRemarks.Text = ""
        txtDeptRemarks.Text = ""
        txtcustRemarks.Text = ""
        txthotremarks.Text = ""
    End Sub
    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
        objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)

        LoadFields()
        ''BindPersonalInformations()
        FindCumilative()
        ''dvFlightDetailsHeading.Visible = False
        BindCheckInAndCheckOutHiddenfield()
        BindBookingSummary()
        BindVisaSummary()
        BindTourSummary()
        BindTransferSummary()
        BindAirportserviceSummary()
        BindOtherserviceSummary()
        BindPreHotelSummary()
        BindTotalValue()

        HideTabTotalPrice()
        If dlAirportSummary.Items.Count > 0 Or dlOtherSummary.Items.Count > 0 Or dlTourSummary.Items.Count > 0 Or dlTransferSummary.Items.Count > 0 Or dlVisaSummary.Items.Count > 0 Then
            hdOnlyHotelbooking.Value = "0"
        Else
            hdOnlyHotelbooking.Value = "1"
        End If

        Dim dt As DataTable
        If Not Session("sEditRequestId") Is Nothing And hdTempRequest.Value = "EDIT" Then
            lblEditRequestId.Text = Session("sEditRequestId")
            lblEditRequestId1.Visible = True
            lblEditRequestId.Visible = True
            If Session("sLoginType") = "RO" Then
                dt = objcommonfunctions.GetBookingvalue(Session("sEditRequestId"), "0")
            Else
                dt = objcommonfunctions.GetBookingvalue(Session("sEditRequestId"), objResParam.WhiteLabel)
            End If

        Else
            If Session("sLoginType") = "RO" Then
                dt = objcommonfunctions.GetBookingvalue(Session("sRequestId"), "0")
            Else
                dt = objcommonfunctions.GetBookingvalue(Session("sRequestId"), objResParam.WhiteLabel)
            End If

            lblEditRequestId1.Visible = False
            lblEditRequestId.Visible = False
        End If

        If dt.Rows.Count > 0 Then
            lbltotalbooking.Text = dt.Rows(0)("totalbookingvalue").ToString
        End If

        Dim baseDt As DataTable = objclsUtilities.GetDataFromDataTable("select r.option_selected as baseCurrency, h.convrate from booking_headertemp h(nolock), reservation_parameters r where h.requestid='" + Convert.ToString(Session("sRequestId")) + "' and r.param_id = 457")
        If baseDt.Rows.Count > 0 Then
            lblTotalBaseCurrTitle.Text = "Total Booking Value in " + baseDt.Rows(0)("baseCurrency")
            If Not IsDBNull(baseDt.Rows(0)("convrate")) Then
                Dim totalAmt As String() = lbltotalbooking.Text.Split(" ")
                Dim totalAmount As Decimal = 0
                If Not totalAmt Is Nothing Then
                    totalAmount = Convert.ToDecimal(totalAmt(0))
                End If
                Dim baseCurrency As Decimal = totalAmount * baseDt.Rows(0)("convrate")
                lblTotalBaseCurrency.Text = Convert.ToString(Math.Round(baseCurrency, 3)) + " " + baseDt.Rows(0)("baseCurrency")
            Else
                lblTotalBaseCurrency.Text = "0" + " " + baseDt.Rows(0)("baseCurrency")
            End If
        End If

        Dim paidAmtDt As DataTable = objBLLguest.CalculatePaidAmount(Session("sRequestId"))
        If Not paidAmtDt Is Nothing AndAlso paidAmtDt.Rows.Count > 0 Then
            divPaid.Style.Add("display", "block")
            lblPaidAmount.Text = Convert.ToString(paidAmtDt.Rows(0)("PaidAmount")) + " " + paidAmtDt.Rows(0)("currcode")
            hdnPaidAmount.Value = paidAmtDt.Rows(0)("PaidAmount")
        Else
            divPaid.Style.Add("display", "none")
            lblPaidAmount.Text = ""
            hdnPaidAmount.Value = 0
        End If

        If hdBookingEngineRateType.Value = "1" Then
            divtotal.Style.Add("display", "none")
        Else
            divtotal.Style.Add("display", "block")
        End If
        Dim iCumulative As Integer = 0
        If Session("sLoginType") = "RO" Then
            iCumulative = objBLLguest.CheckSelectedAgentIsCumulative(Session("sRequestId"))
        Else
            btnCheckVAT.Visible = False
        End If

        If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
            If Session("sdsPackageSummary") Is Nothing Then  '' Added shahul 09/10/18
                Session("sdsPackageSummary") = Nothing
                dvGeneratePackageValue.Style.Add("display", "block")
                dvPackageDetails.Style.Add("display", "none")
            Else
                'dvGeneratePackageValue.Style.Add("display", "none")
                Session("sdsPackageSummary") = Nothing     'Collapse Package summary to refresh any changes param 15/10/2018
                dvPackageDetails.Style.Add("display", "none")
                ShowProfitSummary()
            End If
        Else
            dvGeneratePackageValue.Style.Add("display", "none")
            dvPackageDetails.Style.Add("display", "none")
        End If
        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If

        'added param 20/10/2020
        Dim requestid As String = Session("sRequestId")
        If requestid.Contains("QP/") = True Then
            HideServiceButtons()
        End If
    End Sub
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


                            dvPackageDetails.Style.Add("display", "block")
                            If Session("sLoginType") = "RO" Then
                                dvPackageSummaryRO.Style.Add("display", "block")
                                dvPackageSummaryAgent.Style.Add("display", "none")
                            Else
                                dvPackageSummaryRO.Style.Add("display", "none")
                                dvPackageSummaryAgent.Style.Add("display", "block")

                            End If



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
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnGeneratePackageValue_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
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
            'strRequestId = objBLLCommonFuntions.GetExistingBookingRequestId(strRequestId)
        End If
        Return strRequestId
    End Function


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
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                'imgLogo.Src = "Logos/" & strLogo
                imgLogo.Src = Session("sLogo")  '' ***** Danny 30/06/18

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
                lblPhoneNo.Text = objDataTable.Rows(0)("tel1").ToString
                Page.Title = objDataTable.Rows(0)("companyname").ToString 'companyname
                lblHeaderAgentName.Text = objDataTable.Rows(0)("agentname").ToString
            End If
        End If
        ''If Session("sLoginType") <> "RO" Then
        ''    dvColumbusReference.Visible = False
        ''Else
        ''    dvColumbusReference.Visible = True
        ''End If


    End Sub



    Private Sub BindTittle(ByVal strType As String, ByVal ddlTittle As DropDownList)
        If strType = "Adult" Then
            ddlTittle.Items.Add(New ListItem("--", "0"))
            ddlTittle.Items.Add(New ListItem("Mr", "Mr"))
            ddlTittle.Items.Add(New ListItem("Mrs", "Mrs"))
            ddlTittle.Items.Add(New ListItem("Ms", "Ms"))
        Else
            ddlTittle.Items.Add(New ListItem("--", "0"))
            ddlTittle.Items.Add(New ListItem("Child Male", "Child Male"))
            ddlTittle.Items.Add(New ListItem("Child Female", "Child Female"))
        End If
    End Sub

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
            strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_arrival where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "
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
            strSqlQry = "select flight_tranid +'|'+UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103))) flight_tranid,flightcode from view_flightmast_departure where convert(datetime,'" & contextKey & "',103) between convert(datetime,frmdatec,101) and convert(datetime,todatec,101) and fldayofweek=UPPER(DATENAME(dw,convert(datetime,'" & contextKey & "',103)))  and flightcode like  '" & prefixText & "%' "
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

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_arrival.airportbordercode)airportbordername  from view_flightmast_arrival where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
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

            strSqlQry = "select distinct destintime, (select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=view_flightmast_departure.airportbordercode)airportbordername  from view_flightmast_departure where flight_tranid='" & strFlightCode & "' and fldayofweek='" & strDayName & "'"
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

            ''For Each item As DataListItem In dlPersonalInfo.Items
            ''    Dim ddlVisaTypeNew As DropDownList = CType(item.FindControl("ddlVisaType"), DropDownList)
            ''    If ddlVisaTypeNew.Text = "--" Then
            ''        ddlVisaTypeNew.SelectedValue = ddlVisaType.SelectedValue
            ''        Dim txtVisaPriceNew As TextBox = CType(item.FindControl("txtVisaPrice"), TextBox)
            ''        txtVisaPriceNew.Text = txtVisaPrice.Text
            ''    End If
            ''Next

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
            'Server.Transfer("Login.aspx?ro=1&comp=675558760549078")
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

        'Try
        '    Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
        '    Session.Clear()
        '    Session.Abandon()
        '    Response.Redirect(strAbsoluteUrl, True)
        'Catch ex As Exception
        '    MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
        '    objclsUtilities.WriteErrorLog("HomeSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        'End Try

    End Sub

    ''Protected Sub btnGenerateFlightDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateFlightDetails.Click

    ''    If ValidatePersonalDetails() Then


    ''        Dim strBuffer As New Text.StringBuilder
    ''        Dim strFliBuffer As New Text.StringBuilder
    ''        ''If Session("sAgentCompany") Is  Nothing Or Session("GlobalUserName") Is Nothing Then

    ''        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
    ''            Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
    ''            objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
    ''            objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
    ''        ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
    ''            Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
    ''            objBLLTourSearch = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
    ''            objBLLguest.GBRequestid = objBLLTourSearch.EbRequestID
    ''        ElseIf Not Session("sobjBLLTransferSearchActive") Is Nothing Then
    ''            Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
    ''            objBLLTransferSearch = CType(Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
    ''            objBLLguest.GBRequestid = objBLLTransferSearch.OBRequestId
    ''        End If

    ''        Dim ddlTitle As DropDownList
    ''        Dim lblRowNoAll As Label
    ''        Dim txtFirstNameg As TextBox
    ''        Dim txtMidName As TextBox
    ''        Dim txtLastNameg As TextBox
    ''        Dim txtNationalityCode As TextBox
    ''        Dim txtchildage As TextBox
    ''        Dim ddlVisa As DropDownList
    ''        Dim ddlVisatype As DropDownList
    ''        Dim txtVisaPrice As TextBox
    ''        Dim txtPassportNo As TextBox
    ''        Dim i As Integer = 1
    ''        If dlPersonalInfo.Items.Count > 0 Then
    ''            For Each item As DataListItem In dlPersonalInfo.Items
    ''                Dim strTittle As String = ""
    ''                lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
    ''                ddlTitle = CType(item.FindControl("ddlTittle"), DropDownList)
    ''                txtFirstNameg = CType(item.FindControl("txtFirstName"), TextBox)
    ''                txtMidName = CType(item.FindControl("txtMiddleName"), TextBox)
    ''                txtLastNameg = CType(item.FindControl("txtlastName"), TextBox)
    ''                txtchildage = CType(item.FindControl("txtchildage"), TextBox)
    ''                ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
    ''                ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
    ''                txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
    ''                txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
    ''                txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)

    ''                strBuffer.Append("<DocumentElement>")

    ''                strBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
    ''                'strBuffer.Append(" <guestlineno>" & i & "</guestlineno>")
    ''                strBuffer.Append(" <title>" & ddlTitle.Text & "</title>")
    ''                strBuffer.Append(" <firstname>" & txtFirstNameg.Text & "</firstname>")
    ''                strBuffer.Append(" <middlename>" & txtMidName.Text & "</middlename>")
    ''                strBuffer.Append(" <lastname>" & txtLastNameg.Text & "</lastname>")
    ''                strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
    ''                strBuffer.Append(" <childage>" & CType(Val(txtchildage.Text), Decimal) & "</childage>")
    ''                strBuffer.Append(" <visaoptions>" & ddlVisa.SelectedValue & "</visaoptions>")
    ''                strBuffer.Append(" <visatypecode>" & ddlVisatype.SelectedValue & "</visatypecode>")
    ''                strBuffer.Append(" <visaprice>" & CType(Val(txtVisaPrice.Text), Decimal) & "</visaprice>")
    ''                strBuffer.Append(" <passportno>" & txtPassportNo.Text & "</passportno>")
    ''                strBuffer.Append("</DocumentElement>")
    ''                '  i = i + 1
    ''            Next
    ''        End If
    ''        objBLLguest.GBGuestXml = strBuffer.ToString
    ''        objBLLguest.GBuserlogged = Session("GlobalUserName")






    ''        If objBLLguest.SavingGuestBookingInTemp() Then
    ''            BindVisaSummary()
    ''        End If

    ''        Dim strName As String = ""
    ''        Dim iRowNo As Integer = 0
    ''        dvFlightDetailsHeading.Visible = True
    ''        Dim dtDynamicFlightDetails = New DataTable()
    ''        Dim dcRowNo = New DataColumn("RowNo", GetType(String))
    ''        Dim dcName = New DataColumn("Name", GetType(String))
    ''        dtDynamicFlightDetails.Columns.Add(dcRowNo)
    ''        dtDynamicFlightDetails.Columns.Add(dcName)

    ''        If chkSameFlight.Checked = False Then


    ''            For Each item As DataListItem In dlPersonalInfo.Items
    ''                Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    ''                Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    ''                Dim txtMiddleName As TextBox = CType(item.FindControl("txtMiddleName"), TextBox)
    ''                Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)


    ''                strName = ddlTittle.Text & " " & txtFirstName.Text & " " & txtMiddleName.Text & " " & txtLastName.Text
    ''                iRowNo = iRowNo + 1

    ''                Dim row As DataRow = dtDynamicFlightDetails.NewRow()
    ''                row("RowNo") = (iRowNo).ToString
    ''                row("Name") = "Passenger " & iRowNo & ": " & strName

    ''                dtDynamicFlightDetails.Rows.Add(row)

    ''            Next
    ''        Else
    ''            Dim row As DataRow = dtDynamicFlightDetails.NewRow()
    ''            row("RowNo") = (1).ToString


    ''            dtDynamicFlightDetails.Rows.Add(row)
    ''        End If

    ''        dlFlightDetails.DataSource = dtDynamicFlightDetails
    ''        dlFlightDetails.DataBind()

    ''    Else
    ''        Exit Sub
    ''    End If

    ''End Sub

    Private Sub BindCheckInAndCheckOutHiddenfield()
        Dim objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sRequestId") Is Nothing Then 'modified by abin on 20180718
            Dim dt As DataTable
            dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(Session("sRequestId"))
            If dt.Rows.Count > 0 Then
                hdCheckInPrevDay.Value = dt.Rows(0)("CheckInPrevDay").ToString
                hdCheckInNextDay.Value = dt.Rows(0)("CheckInNextDay").ToString
                hdCheckOutPrevDay.Value = dt.Rows(0)("CheckOutPrevDay").ToString
                hdCheckOutNextDay.Value = dt.Rows(0)("CheckOutNextDay").ToString '
                hdCheckIn.Value = dt.Rows(0)("CheckIn").ToString
                hdCheckOut.Value = dt.Rows(0)("CheckOut").ToString

            End If
        End If

    End Sub

    ''Protected Sub dlFlightDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlFlightDetails.ItemDataBound
    ''    'onChange="ChangeArrivalDate(this)"
    ''    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    ''        Dim txtArrivalDate As TextBox = CType(e.Item.FindControl("txtArrivalDate"), TextBox)
    ''        Dim txtDepartureDate As TextBox = CType(e.Item.FindControl("txtDepartureDate"), TextBox)
    ''        txtArrivalDate.Text = hdCheckIn.Value
    ''        txtDepartureDate.Text = hdCheckOut.Value
    ''        Dim AutoCompleteExtender_txtArrivalflight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_txtArrivalflight"), AjaxControlToolkit.AutoCompleteExtender)
    ''        Dim AutoCompleteExtender_DepartureFlight As AjaxControlToolkit.AutoCompleteExtender = CType(e.Item.FindControl("AutoCompleteExtender_DepartureFlight"), AjaxControlToolkit.AutoCompleteExtender)
    ''        AutoCompleteExtender_txtArrivalflight.ContextKey = hdCheckOut.Value
    ''        AutoCompleteExtender_DepartureFlight.ContextKey = hdCheckOut.Value
    ''        txtArrivalDate.Attributes.Add("onchange", "ChangeArrivalDate('" & txtArrivalDate.ClientID & "','" & AutoCompleteExtender_txtArrivalflight.ClientID & "')")
    ''        txtDepartureDate.Attributes.Add("onchange", "ChangeDepartureDate('" & txtDepartureDate.ClientID & "','" & AutoCompleteExtender_DepartureFlight.ClientID & "')")
    ''    End If

    ''End Sub

    Private Sub BindBookingSummary()
        dvTabHotelSummary.Visible = False

        If Not Session("sEditRequestId") Is Nothing Then
            Dim objBLLHotelSearch = New BLLHotelSearch

            Dim dt As DataTable
            If Session("sLoginType") = "RO" Then
                dt = objBLLHotelSearch.GetBookingSummary(Session("sEditRequestId"), "0")
            Else
                dt = objBLLHotelSearch.GetBookingSummary(Session("sEditRequestId"), objResParam.WhiteLabel)
            End If

            '  dt = objBLLHotelSearch.GetBookingSummary(Session("sEditRequestId"))
            FillHotelSummaryDetails(dt)

        Else
            Dim objBLLHotelSearch = New BLLHotelSearch
            If Not Session("sRequestId") Is Nothing Then

                Dim dt As DataTable
                If Session("sLoginType") = "RO" Then
                    dt = objBLLHotelSearch.GetBookingSummary(Session("sRequestId"), "0")
                Else
                    dt = objBLLHotelSearch.GetBookingSummary(Session("sRequestId"), hdWhiteLabel.Value)
                End If

                FillHotelSummaryDetails(dt)
            End If
        End If

    End Sub
    Private Sub FillHotelSummaryDetails(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            dlBookingSummary.DataSource = dt
            dlBookingSummary.DataBind()
            hdTabHotelTotalPrice.Value = "1"

            If hdBookingEngineRateType.Value <> "1" Then
                lblTabHotelTotalPrice.Text = dt.Rows(0)("HotelTotalPrice").ToString

                Dim format As String = "{0,-10} {1,10}"
                Dim strSpcae10 As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                Dim strSpcae9 As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                Dim strSpcae8 As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                Dim strSpcae7 As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                Dim strSpcae6 As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                'lblTourTabTotalPrice.Text = String.Format(format, "TOTAL PRICE:", dt.Rows(0)("total").ToString)
                'If dt.Rows(0)("HotelTotalPrice").ToString.Length = 4 Then
                '    lblTabHotelTotalPrice.Text = "TOTAL PRICE:" & strSpcae10 & dt.Rows(0)("HotelTotalPrice").ToString
                'ElseIf dt.Rows(0)("HotelTotalPrice").ToString.Length = 5 Then
                '    lblTabHotelTotalPrice.Text = "TOTAL PRICE:" & strSpcae9 & dt.Rows(0)("HotelTotalPrice").ToString
                'ElseIf dt.Rows(0)("HotelTotalPrice").ToString.Length = 6 Then
                '    lblTabHotelTotalPrice.Text = "TOTAL PRICE:" & strSpcae8 & dt.Rows(0)("HotelTotalPrice").ToString
                'ElseIf dt.Rows(0)("HotelTotalPrice").ToString.Length = 7 Then
                '    lblTabHotelTotalPrice.Text = "TOTAL PRICE:" & strSpcae7 & dt.Rows(0)("HotelTotalPrice").ToString
                'ElseIf dt.Rows(0)("HotelTotalPrice").ToString.Length = 8 Then
                '    lblTabHotelTotalPrice.Text = "TOTAL PRICE:" & strSpcae6 & dt.Rows(0)("HotelTotalPrice").ToString
                'Else
                '    lblTabHotelTotalPrice.Text = "TOTAL PRICE:" & dt.Rows(0)("HotelTotalPrice").ToString
                'End If
            End If

            dvTabHotelSummary.Visible = True
        Else
            dvTabHotelSummary.Visible = False
            Session("sobjBLLHotelSearchActive") = Nothing
        End If
    End Sub
    Private Sub BindTourSummary()
        dvTabTourSummary.Visible = False

        If Not Session("sEditRequestId") Is Nothing Then
            Dim objBLLTourSearch = New BLLTourSearch
            Dim dt As DataTable
            If Session("sLoginType") = "RO" Then
                dt = objBLLTourSearch.GetTourSummary(Session("sEditRequestId"), "0")
            Else
                dt = objBLLTourSearch.GetTourSummary(Session("sEditRequestId"), objResParam.WhiteLabel)
            End If

            FillTourSummaryDetails(dt)
        Else
            Dim objBLLTourSearch = New BLLTourSearch
            If Not Session("sRequestId") Is Nothing Then 'modified by abin on 20180718

                Dim dt As DataTable
                If Session("sLoginType") = "RO" Then
                    dt = objBLLTourSearch.GetTourSummary(Session("sRequestId"), "0")
                Else
                    dt = objBLLTourSearch.GetTourSummary(Session("sRequestId"), objResParam.WhiteLabel)
                End If

                FillTourSummaryDetails(dt)
            Else
                dvTourSummarry.Visible = False
            End If
        End If



    End Sub
    Private Sub FillTourSummaryDetails(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then

            dvTourSummarry.Visible = True
            dlTourSummary.DataSource = dt
            dlTourSummary.DataBind()
            lblTourTotalPrice.Text = dt.Rows(0)("total").ToString
            hdTourTabTotalPrice.Value = "1"
            If hdBookingEngineRateType.Value = "1" Then
                dvTourTotal.Visible = False

            Else


                lblTourTabTotalPrice.Text = dt.Rows(0)("total").ToString

                dvTourTotal.Visible = True
            End If
            dvTabTourSummary.Visible = True
        Else
            dvTourSummarry.Visible = False
            dvTabTourSummary.Visible = False
        End If
    End Sub


    Private Sub BindOtherserviceSummary()
        If Not Session("sEditRequestId") Is Nothing Then
            Dim BLLOtherSearch = New BLLOtherSearch
            dvTabOtherServicesSummary.Visible = False
            Dim dt As DataTable
            '  dt = BLLOtherSearch.GetOtherSummary(Session("sEditRequestId"))

            If Session("sLoginType") = "RO" Then
                dt = BLLOtherSearch.GetOtherSummary(Session("sEditRequestId"))
            Else
                dt = BLLOtherSearch.GetOtherSummary(Session("sEditRequestId"), objResParam.WhiteLabel)
            End If

            FillOtherServiceSummaryDetails(dt)
        Else
            Dim BLLOtherSearch = New BLLOtherSearch
            dvTabOtherServicesSummary.Visible = False
            If Not Session("sRequestId") Is Nothing Then 'modified by abin on 20180724
                Dim dt As DataTable

                If Session("sLoginType") = "RO" Then
                    dt = BLLOtherSearch.GetOtherSummary(Session("sRequestId"))
                Else
                    dt = BLLOtherSearch.GetOtherSummary(Session("sRequestId"), objResParam.WhiteLabel)
                End If

                FillOtherServiceSummaryDetails(dt)
            Else
                dvOtherSummary.Visible = False
            End If
        End If


    End Sub
    Private Sub FillOtherServiceSummaryDetails(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then

            dlOtherSummary.Visible = True

            dlOtherSummary.DataSource = dt
            dlOtherSummary.DataBind()
            hdOtherServiceTabTotalPrice.Value = "1"
            lblothTotalPrice.Text = dt.Rows(0)("total").ToString
            If hdBookingEngineRateType.Value = "1" Then
                dvOthtotal.Visible = False
            Else
                dvOthtotal.Visible = True
                lblOtherServiceTabTotalPrice.Text = dt.Rows(0)("total").ToString
            End If
            dvTabOtherServicesSummary.Visible = True
        Else
            dlOtherSummary.Visible = False
        End If
    End Sub

    Private Sub BindAirportserviceSummary()
        dvTabAirportSummary.Visible = False
        If Not Session("sEditRequestId") Is Nothing Then
            Dim BLLMASearch = New BLLMASearch
            Dim dt As DataTable
            If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                dt = BLLMASearch.GetAiportMeetSummary(Session("sEditRequestId"), hdWhiteLabel.Value)
            Else
                dt = BLLMASearch.GetAiportMeetSummary(Session("sEditRequestId"), "0")
            End If

            FillAirportMeetSummaryDetails(dt)
        Else
            Dim BLLMASearch = New BLLMASearch
            If Not Session("sRequestId") Is Nothing Then ' chnaged by abin on 20180724

                Dim dt As DataTable
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    dt = BLLMASearch.GetAiportMeetSummary(Session("sRequestId"), hdWhiteLabel.Value)
                Else
                    dt = BLLMASearch.GetAiportMeetSummary(Session("sRequestId"), "0")
                End If

                FillAirportMeetSummaryDetails(dt)
            Else
                dvAirportSummary.Visible = False
            End If
        End If

    End Sub

    Private Sub FillAirportMeetSummaryDetails(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then

            dlAirportSummary.Visible = True

            dlAirportSummary.DataSource = dt
            dlAirportSummary.DataBind()
            hdAirportTabtotalPrice.Value = "1"
            lblairporttotal.Text = dt.Rows(0)("total").ToString


            If hdBookingEngineRateType.Value = "1" Then
                dvairportTotal.Visible = False

            Else
                dvairportTotal.Visible = True
                lblAirportTabtotalPrice.Text = dt.Rows(0)("total").ToString
            End If
            dvTabAirportSummary.Visible = True
        Else
            dvAirportSummary.Visible = False
        End If
    End Sub
    Private Sub BindTransferSummary()
        dvTabTransfersummary.Visible = False
        If Not Session("sEditRequestId") Is Nothing Then
            Dim objBLLTransferSearch = New BLLTransferSearch
            Dim dt As DataTable
            If Session("sLoginType") = "RO" Then
                dt = objBLLTransferSearch.GetTransferSummary(Session("sEditRequestId"), "0")
            Else
                dt = objBLLTransferSearch.GetTransferSummary(Session("sEditRequestId"), hdWhiteLabel.Value)
            End If

            FillTransferSummaryDetails(dt)
        Else
            Dim objBLLTransferSearch = New BLLTransferSearch
            If Not Session("sRequestId") Is Nothing Then

                Dim dt As DataTable
                If Session("sLoginType") = "RO" Then
                    dt = objBLLTransferSearch.GetTransferSummary(Session("sRequestId"), "0")
                Else
                    dt = objBLLTransferSearch.GetTransferSummary(Session("sRequestId"), hdWhiteLabel.Value)
                End If

                FillTransferSummaryDetails(dt)
            Else
                dvTransferSummary.Visible = False
            End If
        End If



    End Sub
    Private Sub FillTransferSummaryDetails(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then

            dvTransferSummary.Visible = True

            dlTransferSummary.DataSource = dt
            dlTransferSummary.DataBind()

            lblTransfertotal.Text = dt.Rows(0)("total").ToString

            hdTransferTabTotalPrice.Value = "1"

            If hdBookingEngineRateType.Value = "1" Then
                dvTransferTotal.Visible = False

            Else
                dvTransferTotal.Visible = True
                lblTransferTabTotalPrice.Text = dt.Rows(0)("total").ToString

            End If
            dvTabTransfersummary.Visible = True
        Else
            dvTransferSummary.Visible = False
        End If
    End Sub
    Private Sub fillvisadetails(ByVal strRequestId As String)
        Dim dt As DataTable
        '  dt = objBLLHotelSearch.GetVisaSummary(strRequestId)

        If Session("sLoginType") = "RO" Then
            dt = objBLLHotelSearch.GetVisaSummary(strRequestId)
        Else
            dt = objBLLHotelSearch.GetVisaSummary(strRequestId, objResParam.WhiteLabel)
        End If
        Dim totalvisa As Double = 0
        If Not dt Is Nothing Then
            If dt.Rows.Count > 0 Then
                dvVisaSummary.Visible = True
                dlVisaSummary.DataSource = dt 'ff
                dlVisaSummary.DataBind()

                For i = 0 To dt.Rows.Count - 1

                    ' totalvisa = totalvisa + Math.Round(Val(dt.Rows(i).Item("totalvisavalue").ToString))
                    totalvisa = Math.Round(Val(dt.Rows(i).Item("totalvisavalue").ToString))

                Next

                lblVisaHeading.Visible = True

                If hdBookingEngineRateType.Value = "1" Then
                    dvVisatotal.Visible = False
                    lblVisaTotal.Visible = False
                    lblVIsaTotalPrice.Visible = False

                Else
                    lblVisaTotal.Visible = True
                    dvVisatotal.Visible = True

                    ' lblVisaTabTotalPrice.Text = "TOTAL PRICE: " &
                    Dim strTotal As String = CType(Math.Round(Val(totalvisa)), String) + " " + dt.Rows(0).Item("currcode").ToString 'CType(Math.Round(Val(dt.Rows(0).Item("totalvisavalue").ToString)), String) + " " + dt.Rows(0).Item("currcode").ToString
                    lblVisaTabTotalPrice.Text = strTotal
                    lblVIsaTotalPrice.Text = strTotal ' modified by abin on 20180728
                End If
                hdVisaTabTotalPrice.Value = "1"
                dvTabVisaSummary.Visible = True
            Else
                dvVisaSummary.Visible = False
            End If
        End If
    End Sub

    Private Sub BindVisaSummary()
        dvTabVisaSummary.Visible = False
        Dim objBLLHotelSearch = New BLLHotelSearch
        Dim dt As DataTable
        Dim strRequestId As String = ""
        If Not Session("sEditRequestId") Is Nothing Then
            strRequestId = Session("sEditRequestId")
        Else
            strRequestId = Session("sRequestId")
        End If

        fillvisadetails(strRequestId)





    End Sub

    Protected Sub dlSpecialEventsSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If hdBookingEngineRateType.Value = "1" Then
                Dim dvSplEventValue As HtmlGenericControl = CType(e.Item.FindControl("dvSplEventValue"), HtmlGenericControl)
                dvSplEventValue.Visible = False
            End If
        End If

    End Sub
    Protected Sub dlBookingSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlBookingSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
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
            Dim dvOneTimePay As HtmlGenericControl = CType(e.Item.FindControl("dvOneTimePay"), HtmlGenericControl)
            Dim dvHotelAvailability As HtmlGenericControl = CType(e.Item.FindControl("dvHotelAvailability"), HtmlGenericControl)
            Dim lblHotelAvailability As Label = CType(e.Item.FindControl("lblHotelAvailability"), Label)
            Dim hdRatePlanSource As HiddenField = CType(e.Item.FindControl("hdRatePlanSource"), HiddenField)
            Dim lblConfirmStatus As Label = CType(e.Item.FindControl("lblConfirmStatus"), Label)

            Dim lblFunctionType As Label = CType(e.Item.FindControl("lblFunctionType"), Label)
            Dim lblExtraBedPrice As Label = CType(e.Item.FindControl("lblExtraBedPrice"), Label)

            Dim hdCancelled As HiddenField = CType(e.Item.FindControl("hdCancelled"), HiddenField)

            Dim hdBookingMode As HiddenField = CType(e.Item.FindControl("hdBookingMode"), HiddenField) 'Added by abin on 20180716
            If hdBookingMode.Value <> "FreeForm" Then
                Dim dvBookingMode As HtmlGenericControl = CType(e.Item.FindControl("dvBookingMode"), HtmlGenericControl)
                dvBookingMode.Visible = False
            End If

            Dim imgbEdit As ImageButton = CType(e.Item.FindControl("imgbEdit"), ImageButton)
            Dim imgbDelete As ImageButton = CType(e.Item.FindControl("imgbDelete"), ImageButton)
            If Not Session("sLoginType") Is Nothing Then
                If Session("sLoginType") = "RO" Then
                    dvHotelConfirm.Visible = True
                Else
                    dvHotelConfirm.Visible = False
                End If
            Else
                dvHotelConfirm.Visible = False
            End If



            If Not Session("sEditRequestId") Is Nothing Then
                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    imgbEdit.ImageUrl = "~/img/button_amend.png"
                Else
                    imgbEdit.ImageUrl = "~/img/button_edit.png"
                End If

            Else
                imgbEdit.ImageUrl = "~/img/button_edit.png"
            End If

            If hdBookingEngineRateType.Value = "1" Then
                dvTourTotal.Visible = False
            Else
                dvTourTotal.Visible = True
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
                dvHotelAmend.Visible = False
                dvHotelConfirm.Visible = False
                dvHotelRemarks.Visible = False
                If lblConfirmStatus.Text.Trim.ToUpper() = "FAIL" Then
                    dvHotelAvailability.Visible = True
                Else
                    dvHotelAvailability.Visible = False
                End If

                If hdRatePlanSource.Value.ToUpper <> "" And hdRatePlanSource.Value.ToUpper <> "COLUMBUS" Then 'onedmc
                    dvHotelCancelled.Visible = True
                    imgbDelete.Visible = False
                Else
                    dvHotelCancelled.Visible = True
                    imgbDelete.ImageUrl = "~/img/button_undocancel.png"
                    imgbDelete.ToolTip = "Undo Cancel"
                End If
                ' btnHotelCancelSave.Attributes.Add("onclick", "confirmEntireServiceCancel('undo')")
            Else
                If hdRatePlanSource.Value.ToUpper <> "" And hdRatePlanSource.Value.ToUpper <> "COLUMBUS" Then   ' onedmc
                    dvHotelAmend.Visible = False
                    If lblHotelAvailability.Text.Trim <> "" Then
                        dvHotelAvailability.Visible = True
                    Else
                        dvHotelAvailability.Visible = False
                    End If
                Else
                    dvHotelAmend.Visible = True
                    dvHotelAvailability.Visible = False
                End If
                dvHotelConfirm.Visible = True
                dvHotelRemarks.Visible = True
                dvHotelCancelled.Visible = False

                If Not Session("sEditRequestId") Is Nothing Then
                    If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                        imgbDelete.ImageUrl = "~/img/button_cancel.png"
                        imgbDelete.ToolTip = "Cancel"
                    Else
                        imgbDelete.ImageUrl = "~/img/button_remove.png"
                        imgbDelete.ToolTip = "Remove"
                    End If

                Else
                    imgbDelete.ImageUrl = "~/img/button_remove.png"
                    imgbDelete.ToolTip = "Remove"
                End If

            End If
            'btnHotelCancelSave.Attributes.Add("onclick", "confirmEntireServiceCancel()")     'hided param on 25/08/2020
            BindSpecilaEventsDetails(lblrlineno.Text, dvSplEvents, dlSpecialEventsSummary, dvRoomValue)
            BindOneTimePayCharges(lblrlineno.Text, lblExtraBedPrice, lblFunctionType, dvOneTimePay)
            If Session("sLoginType") <> "RO" Then
                dvHotelConfirm.Visible = False
            End If
            If Not Session("sEditRequestId") Is Nothing Then
                If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Then
                    dvHotelConfirm.Visible = False
                End If
            End If

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

    ''Private Function SavingPersonalFlightDetails() As String

    ''    If ValidateGuestDetails() Then

    ''        Dim strBuffer As New Text.StringBuilder
    ''        Dim strFliBuffer As New Text.StringBuilder
    ''        ''If Session("sAgentCompany") Is  Nothing Or Session("GlobalUserName") Is Nothing Then
    ''        'objBLLHotelSearch = New BLLHotelSearch
    ''        'objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
    ''        'objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid

    ''        'If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
    ''        '    Dim objBLLHotelSearch As BLLHotelSearch = New BLLHotelSearch
    ''        '    objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
    ''        '    objBLLguest.GBRequestid = objBLLHotelSearch.OBrequestid
    ''        'ElseIf Not Session("sobjBLLTourSearchActive") Is Nothing Then
    ''        '    Dim objBLLTourSearch As BLLTourSearch = New BLLTourSearch
    ''        '    objBLLTourSearch = CType(Session("sobjBLLTourSearchActive"), BLLTourSearch)
    ''        '    objBLLguest.GBRequestid = objBLLTourSearch.EbRequestID
    ''        'ElseIf Not Session("sobjBLLTransferSearchActive") Is Nothing Then
    ''        '    Dim objBLLTransferSearch As BLLTransferSearch = New BLLTransferSearch
    ''        '    objBLLTransferSearch = CType(Session("sobjBLLTransferSearchActive"), BLLTransferSearch)
    ''        '    objBLLguest.GBRequestid = objBLLTransferSearch.OBRequestId
    ''        'End If

    ''        objBLLguest.GBRequestid = GetRequestId()

    ''        Dim ddlTittle As DropDownList
    ''        Dim lblRowNoAll As Label
    ''        Dim txtFirstName As TextBox
    ''        Dim txtMiddleName As TextBox
    ''        Dim txtLastName As TextBox
    ''        Dim txtNationalityCode As TextBox
    ''        Dim txtchildage As TextBox
    ''        Dim ddlVisa As DropDownList
    ''        Dim ddlVisatype As DropDownList
    ''        Dim txtVisaPrice As TextBox
    ''        Dim txtPassportNo As TextBox
    ''        Dim i As Integer = 1
    ''        'If dlPersonalInfo.Items.Count > 0 Then
    ''        '    For Each item As DataListItem In dlPersonalInfo.Items
    ''        '        Dim strTittle As String = ""
    ''        '        lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)
    ''        '        ddlTittle = CType(item.FindControl("ddlTittle"), DropDownList)
    ''        '        txtFirstName = CType(item.FindControl("txtFirstName"), TextBox)
    ''        '        txtMiddleName = CType(item.FindControl("txtMiddleName"), TextBox)
    ''        '        txtLastName = CType(item.FindControl("txtlastName"), TextBox)
    ''        '        txtchildage = CType(item.FindControl("txtchildage"), TextBox)
    ''        '        ddlVisa = CType(item.FindControl("ddlVisa"), DropDownList)
    ''        '        ddlVisatype = CType(item.FindControl("ddlVisatype"), DropDownList)
    ''        '        txtVisaPrice = CType(item.FindControl("txtVisaPrice"), TextBox)
    ''        '        txtNationalityCode = CType(item.FindControl("txtNationalityCode"), TextBox)
    ''        '        txtPassportNo = CType(item.FindControl("txtPassportNo"), TextBox)

    ''        '        If ddlTittle.Text <> "0" Or txtFirstName.Text <> "" Or txtLastName.Text <> "" Then

    ''        '            If ddlTittle.Text = "0" Then
    ''        '                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
    ''        '                ddlTittle.Focus()
    ''        '                Return False
    ''        '                Exit Function

    ''        '            End If
    ''        '            If txtFirstName.Text = "" Then
    ''        '                MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter first name")
    ''        '                txtFirstName.Focus()
    ''        '                Return False
    ''        '                Exit Function
    ''        '            End If
    ''        '            If txtLastName.Text = "" Then
    ''        '                MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter last name")
    ''        '                txtLastName.Focus()
    ''        '                Return False
    ''        '                Exit Function
    ''        '            End If
    ''        '            If txtNationalityCode.Text = "" Then
    ''        '                MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
    ''        '                Return False
    ''        '                Exit Function
    ''        '            End If


    ''        '            strBuffer.Append("<DocumentElement>")

    ''        '            strBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
    ''        '            'strBuffer.Append(" <guestlineno>" & i & "</guestlineno>")
    ''        '            strBuffer.Append(" <title>" & ddlTittle.Text & "</title>")
    ''        '            strBuffer.Append(" <firstname>" & txtFirstName.Text & "</firstname>")
    ''        '            strBuffer.Append(" <middlename>" & txtMiddleName.Text & "</middlename>")
    ''        '            strBuffer.Append(" <lastname>" & txtLastName.Text & "</lastname>")
    ''        '            strBuffer.Append(" <nationalitycode>" & txtNationalityCode.Text & "</nationalitycode>")
    ''        '            strBuffer.Append(" <childage>" & CType(Val(txtchildage.Text), Decimal) & "</childage>")
    ''        '            strBuffer.Append(" <visaoptions>" & ddlVisa.SelectedValue & "</visaoptions>")
    ''        '            strBuffer.Append(" <visatypecode>" & ddlVisatype.SelectedValue & "</visatypecode>")
    ''        '            strBuffer.Append(" <visaprice>" & CType(Val(txtVisaPrice.Text), Decimal) & "</visaprice>")
    ''        '            strBuffer.Append(" <passportno>" & txtPassportNo.Text & "</passportno>")
    ''        '            strBuffer.Append("</DocumentElement>")
    ''        '        End If
    ''        '        '  i = i + 1
    ''        '    Next
    ''        'End If
    ''        'objBLLguest.GBGuestXml = strBuffer.ToString
    ''        'objBLLguest.GBuserlogged = Session("GlobalUserName")



    ''        'Dim txtArrivalDate As TextBox
    ''        'Dim txtArrivalflightCode As TextBox
    ''        'Dim txtArrivalTime As TextBox
    ''        'Dim txtArrivalAirport As TextBox
    ''        'Dim txtArrivalflight As TextBox
    ''        'Dim txtDepartureDate As TextBox
    ''        'Dim txtDepartureFlightCode As TextBox
    ''        'Dim txtDepartureTime As TextBox
    ''        'Dim txtDepartureAirport As TextBox
    ''        'Dim txtDepartureFlight As TextBox

    ''        'Dim j As Integer = 1
    ''        'If dlFlightDetails.Items.Count > 0 Then
    ''        '    For Each item As DataListItem In dlFlightDetails.Items
    ''        '        Dim strTittle As String = ""
    ''        '        txtArrivalDate = CType(item.FindControl("txtArrivalDate"), TextBox)
    ''        '        txtDepartureDate = CType(item.FindControl("txtDepartureDate"), TextBox)
    ''        '        If txtArrivalDate.Text <> "" And txtDepartureDate.Text <> "" Then





    ''        '            lblRowNoAll = CType(item.FindControl("lblRowNoAll"), Label)

    ''        '            txtArrivalflight = CType(item.FindControl("txtArrivalflight"), TextBox)


    ''        '            txtArrivalflightCode = CType(item.FindControl("txtArrivalflightCode"), TextBox)
    ''        '            txtArrivalTime = CType(item.FindControl("txtArrivalTime"), TextBox)
    ''        '            txtArrivalAirport = CType(item.FindControl("txtArrivalAirport"), TextBox)

    ''        '            txtDepartureFlightCode = CType(item.FindControl("txtDepartureFlightCode"), TextBox)
    ''        '            txtDepartureTime = CType(item.FindControl("txtDepartureTime"), TextBox)
    ''        '            txtDepartureAirport = CType(item.FindControl("txtDepartureAirport"), TextBox)

    ''        '            txtDepartureFlight = CType(item.FindControl("txtDepartureFlight"), TextBox)

    ''        '            strFliBuffer.Append("<DocumentElement>")

    ''        '            If chkSameFlight.Checked Then
    ''        '                strFliBuffer.Append(" <guestlineno> 0</guestlineno>")
    ''        '            Else
    ''        '                strFliBuffer.Append(" <guestlineno>" & lblRowNoAll.Text & "</guestlineno>")
    ''        '            End If
    ''        '            Dim strFromDate As String = txtArrivalDate.Text
    ''        '            If strFromDate <> "" Then
    ''        '                Dim strDates As String() = strFromDate.Split("/")
    ''        '                strFromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
    ''        '            End If

    ''        '            strFliBuffer.Append(" <arrdate>" & Format(CType(strFromDate, Date), "yyyy/MM/dd") & "</arrdate>")
    ''        '            strFliBuffer.Append(" <arrflightcode>" & txtArrivalflight.Text & "</arrflightcode>")
    ''        '            strFliBuffer.Append(" <arrflight_tranid>" & txtArrivalflightCode.Text.Trim.Split("|").GetValue(0) & "</arrflight_tranid>")
    ''        '            strFliBuffer.Append(" <arrflighttime>" & txtArrivalTime.Text & "</arrflighttime>")
    ''        '            Dim arrairportbordercode As String = objclsUtilities.ExecuteQueryReturnStringValue("select  airportbordercode  from  flightmast where flight_tranid='" & txtArrivalflightCode.Text.Trim.Split("|").GetValue(0) & "'and flightcode= '" & txtArrivalflight.Text.Trim & "'")
    ''        '            strFliBuffer.Append(" <arrairportbordercode>" & arrairportbordercode & "</arrairportbordercode>")

    ''        '            Dim strDeptDate As String = txtDepartureDate.Text
    ''        '            If strDeptDate <> "" Then
    ''        '                Dim strDates As String() = strDeptDate.Split("/")
    ''        '                strDeptDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
    ''        '            End If

    ''        '            strFliBuffer.Append(" <depdate>" & Format(CType(strDeptDate, Date), "yyyy/MM/dd") & "</depdate>")
    ''        '            strFliBuffer.Append(" <depflightcode>" & txtDepartureFlight.Text & "</depflightcode>")
    ''        '            strFliBuffer.Append(" <depflight_tranid>" & txtDepartureFlightCode.Text.Split("|").GetValue(0) & "</depflight_tranid>")
    ''        '            strFliBuffer.Append(" <depflighttime>" & txtDepartureTime.Text & "</depflighttime>")
    ''        '            Dim depairportbordercode As String = objclsUtilities.ExecuteQueryReturnStringValue("select airportbordercode  from  flightmast where flight_tranid='" & txtDepartureFlightCode.Text.Trim.Split("|").GetValue(0) & "'and flightcode= '" & txtDepartureFlight.Text.Trim & "'")
    ''        '            strFliBuffer.Append(" <depairportbordercode>" & depairportbordercode & "</depairportbordercode>")
    ''        '            strFliBuffer.Append("</DocumentElement>")
    ''        '            ' j = j + 1
    ''        '        End If
    ''        '    Next
    ''    End If
    ''    objBLLguest.GBFlightXml = strFliBuffer.ToString
    ''    If objBLLguest.SavingGuestFlightBookingInTemp() Then
    ''        MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
    ''    End If
    ''    Else
    ''    Return False
    ''    Exit Function
    ''    End If
    ''    Return True
    ''End Function


    Sub final_save(ByVal requestid As String, ByVal divcode As String)

        Dim strrequestid As String = ""
        Dim strPackageSummary As New StringBuilder
        Dim strPackageValueSummary As New StringBuilder
        Dim iCumulative As Integer = 0
        If Session("sLoginType") = "RO" Then
            iCumulative = objBLLguest.CheckSelectedAgentIsCumulative(requestid)
        End If
        FindCumilative()
        If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
            iCumulative = 1
            objBLLguest.Cumulative = iCumulative
            If Not Session("sdsPackageSummary") Is Nothing Then
                Dim ds As DataSet = Session("sdsPackageSummary")
                If ds.Tables(0).Rows.Count > 0 Then

                    Dim strMessage As String = ""
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        strMessage = strMessage & "\n " & ds.Tables(0).Rows(i)("errdesc").ToString
                    Next
                    If strMessage.Contains("cannot proceed") Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, strMessage.Replace("\n", "</br>"))
                        Exit Sub
                    End If

                    lblPRequestId.Text = requestid
                    lblDivCode.Text = divcode
                    btnBacktoBookingForPackage.Visible = True
                    btnProceedWithBook.Visible = False

                    ' MessageBox.ShowMessage(Page, MessageType.Warning, ds.Tables(0).Rows(0)("errdesc").ToString)
                    gvPackageConfirmError.DataSource = ds.Tables(0)
                    gvPackageConfirmError.DataBind()
                    mpPackageConfirmError.Show()
                    Exit Sub
                Else


                    If ds.Tables(1).Rows.Count > 0 Then
                        strPackageSummary.Append("<DocumentElement>")
                        For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                            strPackageSummary.Append("<Table>")
                            strPackageSummary.Append("<requestid>" & requestid & "</requestid>")
                            strPackageSummary.Append("<requesttype>" & ds.Tables(1).Rows(i)("requesttype").ToString & "</requesttype>")
                            strPackageSummary.Append("<rlineno>" & ds.Tables(1).Rows(i)("rlineno").ToString & "</rlineno>")
                            strPackageSummary.Append("<adults>" & ds.Tables(1).Rows(i)("adults").ToString & "</adults>")
                            strPackageSummary.Append("<child>" & ds.Tables(1).Rows(i)("child").ToString & "</child>")
                            strPackageSummary.Append("<salevalue>" & ds.Tables(1).Rows(i)("salevalue").ToString & "</salevalue>")
                            strPackageSummary.Append("<salevaluebase>" & ds.Tables(1).Rows(i)("salevaluebase").ToString & "</salevaluebase>")
                            strPackageSummary.Append("<costvalue>" & ds.Tables(1).Rows(i)("costvalue").ToString & "</costvalue>")
                            strPackageSummary.Append("</Table>")

                        Next
                        strPackageSummary.Append("</DocumentElement>")
                        objBLLguest.PackageSummary = strPackageSummary.ToString
                    End If

                    If ds.Tables(2).Rows.Count > 0 Then
                        strPackageValueSummary.Append("<DocumentElement>")
                        For i As Integer = 0 To ds.Tables(2).Rows.Count - 1
                            strPackageValueSummary.Append("<Table>")
                            strPackageValueSummary.Append("<requestid>" & requestid & "</requestid>")
                            strPackageValueSummary.Append("<totalsalevalue>" & ds.Tables(2).Rows(i)("totalsalevalue").ToString & "</totalsalevalue>")
                            strPackageValueSummary.Append("<totalsalevaluebase>" & ds.Tables(2).Rows(i)("totalsalevaluebase").ToString & "</totalsalevaluebase>")
                            strPackageValueSummary.Append("<totalcostvaluebase>" & ds.Tables(2).Rows(i)("totalcostvaluebase").ToString & "</totalcostvaluebase>")
                            strPackageValueSummary.Append("<adults>" & Val(ds.Tables(2).Rows(i)("adults").ToString) & "</adults>")
                            strPackageValueSummary.Append("<child>" & Val(ds.Tables(2).Rows(i)("child").ToString) & "</child>")
                            strPackageValueSummary.Append("<withvisa>" & Val(ds.Tables(2).Rows(i)("withvisa").ToString) & "</withvisa>")
                            strPackageValueSummary.Append("<adultmarkup>" & Val(ds.Tables(2).Rows(i)("adultmarkup").ToString) & "</adultmarkup>")
                            strPackageValueSummary.Append("<childmarkup>" & Val(ds.Tables(2).Rows(i)("childmarkup").ToString) & "</childmarkup>")
                            strPackageValueSummary.Append("<minimummarkup>" & Val(ds.Tables(2).Rows(i)("minimummarkup").ToString) & "</minimummarkup>")
                            strPackageValueSummary.Append("<totalmarkupbase>" & Val(ds.Tables(2).Rows(i)("totalmarkupbase").ToString) & "</totalmarkupbase>")
                            strPackageValueSummary.Append("<differentialmarkup>" & Val(ds.Tables(2).Rows(i)("differentialmarkup").ToString) & "</differentialmarkup>")
                            strPackageValueSummary.Append("<formulaid>" & ds.Tables(2).Rows(i)("formulaid").ToString & "</formulaid>")
                            strPackageValueSummary.Append("<flineno>" & Val(ds.Tables(2).Rows(i)("flineno").ToString) & "</flineno>")
                            strPackageValueSummary.Append("<fromslab>" & Val(ds.Tables(2).Rows(i)("fromslab").ToString) & "</fromslab>")
                            strPackageValueSummary.Append("<toslab>" & Val(ds.Tables(2).Rows(i)("toslab").ToString) & "</toslab>")
                            strPackageValueSummary.Append("<discountperc>" & Val(ds.Tables(2).Rows(i)("discountperc").ToString) & "</discountperc>")
                            strPackageValueSummary.Append("<discountmarkup>" & Val(ds.Tables(2).Rows(i)("discountmarkup").ToString) & "</discountmarkup>")
                            strPackageValueSummary.Append("<netsalevalue>" & Val(ds.Tables(2).Rows(i)("netsalevalue").ToString) & "</netsalevalue>")
                            '' Added shahul 11/04/18
                            strPackageValueSummary.Append("<childfreeupto>" & Val(ds.Tables(2).Rows(i)("childfreeupto").ToString) & "</childfreeupto>")
                            strPackageValueSummary.Append("<ChildFreeWithVisa>" & Val(ds.Tables(2).Rows(i)("ChildFreeWithVisa").ToString) & "</ChildFreeWithVisa>")
                            strPackageValueSummary.Append("<ChildFreeWithoutVisa>" & Val(ds.Tables(2).Rows(i)("ChildFreeWithoutVisa").ToString) & "</ChildFreeWithoutVisa>")

                            '' Added shahul 01/07/18  temporaly commentd once finish development uncomment again
                            strPackageValueSummary.Append("<adultrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("adultrevisedmarkup").ToString) & "</adultrevisedmarkup>")
                            strPackageValueSummary.Append("<adultreviseddiscount>" & Val(ds.Tables(2).Rows(i)("adultreviseddiscount").ToString) & "</adultreviseddiscount>")
                            strPackageValueSummary.Append("<childrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("childrevisedmarkup").ToString) & "</childrevisedmarkup>")
                            strPackageValueSummary.Append("<childreviseddiscount>" & Val(ds.Tables(2).Rows(i)("childreviseddiscount").ToString) & "</childreviseddiscount>")
                            strPackageValueSummary.Append("<childfreeuptorevisedmarkup>" & Val(ds.Tables(2).Rows(i)("childfreeuptorevisedmarkup").ToString) & "</childfreeuptorevisedmarkup>")
                            strPackageValueSummary.Append("<childfreeuptoreviseddiscount>" & Val(ds.Tables(2).Rows(i)("childfreeuptoreviseddiscount").ToString) & "</childfreeuptoreviseddiscount>")
                            strPackageValueSummary.Append("<totalrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("totalrevisedmarkup").ToString) & "</totalrevisedmarkup>")
                            strPackageValueSummary.Append("<totalreviseddiscount>" & Val(ds.Tables(2).Rows(i)("totalreviseddiscount").ToString) & "</totalreviseddiscount>")

                            strPackageValueSummary.Append("</Table>")

                        Next
                        strPackageValueSummary.Append("</DocumentElement>")
                        objBLLguest.PackageValueSummary = strPackageValueSummary.ToString
                    End If
                End If
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please generate package value.")
                Exit Sub
            End If
        End If
        If Not Session("sEditRequestId") Is Nothing Then

            If requestid.Contains("QR/") Or requestid.Contains("QG/") Then
                strrequestid = objBLLguest.FinalQuoteSaveBooking(requestid, divcode, txtAgencyRef.Text, "EDIT")
            Else
                strrequestid = objBLLguest.FinalQuoteSaveBooking(requestid, divcode, txtAgencyRef.Text, "NEW")
            End If


        Else
            strrequestid = objBLLguest.FinalQuoteSaveBooking(requestid, divcode, txtAgencyRef.Text, "NEW")
        End If

        If strrequestid <> "" Then
            hdQuoteReqestId.Value = strrequestid
            '' As per Arun Email comment Email to Cummulative Agent'
            If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
                'sendemail("New", strrequestid)
                sendemailformat("New", strrequestid)
            ElseIf Session("sLoginType") = "RO" Then       'modified param 22/10/2018
                sendemailQuoteToRo("New", strrequestid)
            Else 'changed by mohamed on 02/07/2018
                'changed by mohamed on 02/07/2018
                Dim lsQuoteToIndividual As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2022")
                If lsQuoteToIndividual = "Y" Then
                    sendemailIndividualAgent("New", strrequestid)
                End If
            End If

            '''''''''''''
            MessageBox.ShowMessage(Page, MessageType.Success, "Quotation Created  " + strrequestid)
            divsubmitquote.Style.Add("display", "none")
            btnProceedBooking.Style.Add("display", "none")
            btnAbondon.Style.Add("display", "none")
            divprintquote.Style.Add("display", "block")
            divbackhome.Style.Add("display", "block")
            divcheck.Style.Add("display", "none")

            Div110.Style.Add("display", "none") 'changed by mohamed on 27/06/2018

            Session("sFinalBooked") = "1"
            LoadMenus() 'changed by mohamed on 12/08/2018
            HideServiceButtons()
            btnGeneratePackageValue.Visible = False
            '  divexcelreport.Style.Add("display", "block")
        Else
            MessageBox.ShowMessage(Page, MessageType.Errors, "Quotation Failed  Please Submit Again ")
            divsubmitquote.Style.Add("display", "block")
            btnProceedBooking.Style.Add("display", "block")
            btnAbondon.Style.Add("display", "block")
            divprintquote.Style.Add("display", "none")
            divbackhome.Style.Add("display", "none")
            divcheck.Style.Add("display", "block")
        End If
    End Sub

    Sub sendemail(ByVal emailstatus As String, ByVal requestid As String)
        Try
            Dim bc As clsBookingQuotePdf = New clsBookingQuotePdf()
            Dim objclsUtilities As New clsUtilities

            Dim ds As New DataSet
            Dim strpath1 As String = ""
            Dim bytes As Byte() = {}
            Dim fileName As String = "Quote@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            Dim ResParam As New ReservationParameters
            ResParam = Session("sobjResParam")
            bc.GenerateCumulative(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            strpath1 = Server.MapPath("~\SavedReports\") + fileName
            Session("sobjResParam") = ResParam

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
            Dim contactDt As DataTable = ds.Tables(8)

            Dim strMessage As String = ""
            Dim AgentName As String = "", agentref As String = "", status As String = "", agentcontact As String = "", agentemail As String = "", agentuser As String = ""
            Dim confstatus As String = ""
            Dim clsEmail As New clsEmail
            Dim strQuery As String = ""

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim strfromemailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")


            Dim to_email As String = ""

            Dim strSubject1 As String = ""
            Dim bookingvalue As String = ""
            Dim divcode As String = ""

            If headerDt.Rows.Count > 0 Then
                AgentName = headerDt.Rows(0)("agentname")
                agentref = headerDt.Rows(0)("agentref")
                agentcontact = headerDt.Rows(0)("agentcontact")
                agentemail = headerDt.Rows(0)("agentemail")

                agentuser = headerDt.Rows(0)("webusername")
                divcode = headerDt.Rows(0)("div_code")
                bookingvalue = CType(headerDt.Rows(0)("salevalue"), String)
                If Emailmode = "Test" Then
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE", "QUOTE")
                Else
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE", "QUOTE")
                End If

                If Emailmode = "Test" Then
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE(" & requestid.Trim & ")", "QUOTE(" & requestid & ")")
                Else
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE(" & requestid & ")", "QUOTE(" & requestid & ")")
                End If

            End If

            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                to_email = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                '   strMessage = "Dear " + contactDt.Rows(0)("salesperson") + "," + "&nbsp;<br /><br />"
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                Dim contact As String = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
                If Session("sLoginType") = "RO" Then
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We Cancelled the attached  booking .</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'>" + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                        If emailstatus = "Amended" Then
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the Attached revised quote as requested by You and   Details of the quote is as follows:</span></p>"
                        Else
                            If status = "Quote" Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote and Details of the quote is as follows:</span></p>"
                            Else
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote  and Details of the quote is as follows:</span></p>"
                            End If

                        End If
                    End If
                Else
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You Received the  Cancelled quote from   <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> .Please check the Attached Quote Ref.</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You have received the following quote from  <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span>. Details of the quote is as follows:</span></p>"
                    End If

                End If

            End If

            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agent Name : " + AgentName + "</span></p>"
            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agency Reference Number : " + agentref + "</span></p> <p class='MsoNormal' style='margin: 0'><font face='Calibri,sans-serif'>&nbsp;</font></p>"


            If hotelDt.Rows.Count > 0 Then
                strMessage += " <br /><table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Hotel Name</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check In</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check Out</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Meal Plan</th></tr><tr> "

                For i = 0 To hotelDt.Rows.Count - 1
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("partyname") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkin") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkout") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("roomdetail") + "</td></tr>"
                Next
                strMessage += "</table>"
            End If

            If visaDt.Rows.Count > 0 Or othServDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                strMessage += "<br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the details of other services quoted:</span></p>"
            End If
            strMessage += "<br />"
            If visaDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>VISA            : YES" + "</span></p>"
            End If

            If othServDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TRANSFERS       : YES" + "</span></p>"
            End If
            If airportDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>AIRPORT SERVICES: YES" + "</span></p>"
            End If
            If tourDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TOURS           : YES" + "</span></p>"
            End If
            If OtherDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>OTHER SERVICES  : YES" + "</span></p>"
            End If

            strMessage += "  <br /><br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Best Regards,</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Mahce software admin team</span></p>"


            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentsOnlineCommon
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentsOnlineCommon1
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
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='" & salesperson & "'")
            If emaildt.Rows.Count > 0 Then
                'strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                'strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))

                'Modified by abin on 20181212
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Then
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


            If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, agentemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_RO")
            Else
                objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_RO")
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: Sendemail :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    ''Private Function ValidateGuestDetails() As String
    ''    For Each item As DataListItem In dlPersonalInfo.Items
    ''        Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    ''        Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    ''        Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
    ''        Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
    ''        Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
    ''        Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)
    ''        'If ddlTittle.Text = "0" Then
    ''        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
    ''        '    ddlTittle.Focus()
    ''        '    Return False
    ''        '    Exit Function

    ''        'End If
    ''        'If txtFirstName.Text = "" Then
    ''        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter first name")
    ''        '    txtFirstName.Focus()
    ''        '    Return False
    ''        '    Exit Function
    ''        'End If
    ''        'If txtLastName.Text = "" Then
    ''        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter last name")
    ''        '    txtLastName.Focus()
    ''        '    Return False
    ''        '    Exit Function
    ''        'End If
    ''        'If txtNationality.Text = "" Then
    ''        '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
    ''        '    txtNationality.Focus()
    ''        '    Return False
    ''        '    Exit Function
    ''        'End If
    ''        If ddlVisa.SelectedValue = "Required" And (txtFirstName.Text <> "" And txtLastName.Text <> "") Then
    ''            If ddlVisatype.SelectedValue = "--" Then
    ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Visa Type")
    ''                ddlVisatype.Focus()
    ''                Return False
    ''                Exit Function
    ''            End If
    ''        End If
    ''    Next
    ''    Return True
    ''End Function
    ''Private Function ValidatePersonalDetails() As String
    ''    For Each item As DataListItem In dlPersonalInfo.Items
    ''        Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    ''        Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    ''        Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
    ''        Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
    ''        Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
    ''        Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)
    ''        If ddlTittle.Text = "0" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
    ''            ddlTittle.Focus()
    ''            Return False
    ''            Exit Function

    ''        End If
    ''        If txtFirstName.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter first name")
    ''            txtFirstName.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If txtLastName.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter last name")
    ''            txtLastName.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If txtNationality.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
    ''            txtNationality.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If ddlVisa.SelectedValue = "Required" And (txtFirstName.Text <> "" And txtLastName.Text <> "") Then
    ''            If ddlVisatype.SelectedValue = "--" Then
    ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Visa Type")
    ''                ddlVisatype.Focus()
    ''                Return False
    ''                Exit Function
    ''            End If
    ''        End If
    ''    Next
    ''    Return True
    ''End Function
    ''Private Function ValidatePersonalFlightDetails() As String
    ''    For Each item As DataListItem In dlPersonalInfo.Items
    ''        Dim ddlTittle As DropDownList = CType(item.FindControl("ddlTittle"), DropDownList)
    ''        Dim txtFirstName As TextBox = CType(item.FindControl("txtFirstName"), TextBox)
    ''        Dim txtNationality As TextBox = CType(item.FindControl("txtNationality"), TextBox)
    ''        Dim txtLastName As TextBox = CType(item.FindControl("txtLastName"), TextBox)
    ''        Dim ddlVisa As DropDownList = CType(item.FindControl("ddlVisa"), DropDownList)
    ''        Dim ddlVisatype As DropDownList = CType(item.FindControl("ddlVisatype"), DropDownList)

    ''        If ddlTittle.Text = "0" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select tittle")
    ''            ddlTittle.Focus()
    ''            Return False
    ''            Exit Function

    ''        End If
    ''        If txtFirstName.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter first name")
    ''            txtFirstName.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If txtLastName.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please eneter last name")
    ''            txtLastName.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If txtNationality.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter nationality")
    ''            txtNationality.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If ddlVisa.SelectedValue = "Required" Then
    ''            If ddlVisatype.SelectedValue = "--" Then
    ''                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Visa Type")
    ''                ddlVisatype.Focus()
    ''                Return False
    ''                Exit Function
    ''            End If
    ''        End If

    ''    Next
    ''    For Each item As DataListItem In dlFlightDetails.Items

    ''        Dim txtDepartureFlight As TextBox = CType(item.FindControl("txtDepartureFlight"), TextBox)
    ''        Dim txtDeparturetime As TextBox = CType(item.FindControl("txtDeparturetime"), TextBox)
    ''        Dim txtArrivalFlight As TextBox = CType(item.FindControl("txtArrivalFlight"), TextBox)
    ''        Dim txtarrivaltime As TextBox = CType(item.FindControl("txtarrivaltime"), TextBox)



    ''        If txtDepartureFlight.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
    ''            txtDepartureFlight.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If


    ''        If txtArrivalFlight.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Flight Details")
    ''            txtArrivalFlight.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If txtarrivaltime.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Arrival Time")
    ''            txtDepartureFlight.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If
    ''        If txtDeparturetime.Text = "" Then
    ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Departure Time")
    ''            txtDeparturetime.Focus()
    ''            Return False
    ''            Exit Function
    ''        End If

    ''    Next
    ''    Return True
    ''End Function
    Protected Sub lnkVisa_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVisa.ServerClick
        'If SavingPersonalFlightDetails() Then
        If Session("sFinalBooked") Is Nothing Then
            Response.Redirect("~\VisaBooking.aspx")
        Else
            If Session("sFinalBooked") = "1" Then
                Response.Redirect("~\Home.aspx?Tab=0")
            Else
                Response.Redirect("~\VisaBooking.aspx")
            End If
        End If

        '' End If
    End Sub
    Protected Sub lnkAccom_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAccom.ServerClick
        'If SavingPersonalFlightDetails() Then
        If Session("sFinalBooked") Is Nothing Then
            Response.Redirect("~\HotelSearch.aspx")
        Else
            If Session("sFinalBooked") = "1" Then
                Response.Redirect("~\Home.aspx?Tab=0")
            Else
                Response.Redirect("~\HotelSearch.aspx")
            End If
        End If


        '' End If
    End Sub

    Protected Sub lnkAirport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAirport.ServerClick
        If Session("sFinalBooked") Is Nothing Then
            Response.Redirect("~\AirportMeetSearch.aspx")
        Else
            If Session("sFinalBooked") = "1" Then
                Response.Redirect("~\Home.aspx?Tab=2")
            Else
                Response.Redirect("~\AirportMeetSearch.aspx")
            End If
        End If


    End Sub

    Protected Sub lnkTours_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTours.ServerClick

        If Session("sFinalBooked") Is Nothing Then
            Response.Redirect("~\TourSearch.aspx")
        Else
            If Session("sFinalBooked") = "1" Then
                Response.Redirect("~\Home.aspx?Tab=1")
            Else
                Response.Redirect("~\TourSearch.aspx")
            End If
        End If
    End Sub

    Protected Sub lnkTransfer_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTransfer.ServerClick

        If Session("sFinalBooked") Is Nothing Then
            Response.Redirect("~\TransferSearch.aspx")
        Else
            If Session("sFinalBooked") = "1" Then
                Response.Redirect("~\Home.aspx?Tab=3")
            Else
                Response.Redirect("~\TransferSearch.aspx")
            End If
        End If
    End Sub

    Protected Sub lnkOtherServices_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkOtherServices.ServerClick

        If Session("sFinalBooked") Is Nothing Then
            Response.Redirect("~\OtherSearch.aspx")
        Else
            If Session("sFinalBooked") = "1" Then
                Response.Redirect("~\Home.aspx?Tab=4")
            Else
                Response.Redirect("~\OtherSearch.aspx")
            End If
        End If
        ''End If
    End Sub


    Protected Sub dlVisaSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlVisaSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblVisaPrice As Label = CType(e.Item.FindControl("lblVisaPrice"), Label)
            Dim lblVisaValue As Label = CType(e.Item.FindControl("lblVisaValue"), Label)

            Dim dvVisaSalevalue As HtmlGenericControl = CType(e.Item.FindControl("dvVisaSalevalue"), HtmlGenericControl)

            Dim lblwlVisaPrice As Label = CType(e.Item.FindControl("lblwlVisaPrice"), Label)
            Dim lblwlVisaValue As Label = CType(e.Item.FindControl("lblwlVisaValue"), Label)

            Dim dvwlVisaSalevalue As HtmlGenericControl = CType(e.Item.FindControl("dvwlVisaSalevalue"), HtmlGenericControl)

            Dim imgVisaEdit As ImageButton = CType(e.Item.FindControl("imgVisaEdit"), ImageButton)
            Dim divvisacancel As HtmlGenericControl = CType(e.Item.FindControl("divvisacancel"), HtmlGenericControl)
            Dim divvisaremove As HtmlGenericControl = CType(e.Item.FindControl("divvisaremove"), HtmlGenericControl)
            Dim imgVisaDelete As ImageButton = CType(e.Item.FindControl("imgVisaDelete"), ImageButton)
            Dim dvvisaCancelled As HtmlGenericControl = CType(e.Item.FindControl("dvvisaCancelled"), HtmlGenericControl)
            Dim divvisaremarks As HtmlGenericControl = CType(e.Item.FindControl("divvisaremarks"), HtmlGenericControl)
            Dim dvVisaConfirm As HtmlGenericControl = CType(e.Item.FindControl("dvVisaConfirm"), HtmlGenericControl)
            Dim divvisaedit As HtmlGenericControl = CType(e.Item.FindControl("divvisaedit"), HtmlGenericControl)
            Dim hdvisaCancelled As HiddenField = CType(e.Item.FindControl("hdvisaCancelled"), HiddenField)
            Dim imgVisacancel As ImageButton = CType(e.Item.FindControl("imgVisacancel"), ImageButton)

            Dim hdBookingMode As HiddenField = CType(e.Item.FindControl("hdBookingMode"), HiddenField) 'Added by abin on 20180716
            If hdBookingMode.Value <> "FreeForm" Then
                Dim dvBookingMode As HtmlGenericControl = CType(e.Item.FindControl("dvBookingMode"), HtmlGenericControl)
                dvBookingMode.Visible = False
            End If

            If Not Session("sEditRequestId") Is Nothing Then


                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    divvisacancel.Style.Add("display", "block")
                    divvisaremove.Style.Add("display", "none")
                    imgVisaEdit.ImageUrl = "~/img/button_amend.png"
                    imgVisaEdit.ToolTip = "Amend"
                    imgVisaDelete.ImageUrl = "~/img/button_cancel.png"
                    imgVisaDelete.ToolTip = "Cancel"
                Else
                    divvisacancel.Style.Add("display", "none")
                    divvisaremove.Style.Add("display", "block")
                    imgVisaEdit.ImageUrl = "~/img/button_edit.png"
                    imgVisaEdit.ToolTip = "Edit"

                    imgVisaEdit.ImageUrl = "~/img/button_edit.png"
                    imgVisaDelete.ImageUrl = "~/img/button_remove.png"
                    imgVisaDelete.ToolTip = "Remove"
                End If


            Else

                divvisacancel.Style.Add("display", "none")
                divvisaremove.Style.Add("display", "block")
                imgVisaEdit.ImageUrl = "~/img/button_edit.png"
                imgVisaEdit.ToolTip = "Edit"

                imgVisaEdit.ImageUrl = "~/img/button_edit.png"
                imgVisaDelete.ImageUrl = "~/img/button_remove.png"
                imgVisaDelete.ToolTip = "Remove"
            End If


            If HttpContext.Current.Session("sLoginType") <> "RO" Then
                divvisacancel.Style.Add("display", "none")
                divvisaremove.Style.Add("display", "block")
            End If


            If hdBookingEngineRateType.Value = "1" Then
                dvVisaSalevalue.Visible = False
            Else
                If hdWhiteLabel.Value = "1" Then
                    dvVisaSalevalue.Style.Add("display", "none")
                    dvwlVisaSalevalue.Style.Add("display", "block")
                Else
                    dvVisaSalevalue.Style.Add("display", "block")
                    dvwlVisaSalevalue.Style.Add("display", "none")
                End If

            End If

            lblVisaPrice.Text = Format(Val(lblVisaPrice.Text), "0.00")
            lblVisaValue.Text = Format(Val(lblVisaValue.Text), "0.00")

            If hdvisaCancelled.Value = "1" Then

                dvVisaConfirm.Visible = False
                divvisacancel.Visible = True
                divvisaremarks.Visible = False
                divvisaedit.Visible = False
                dvvisaCancelled.Visible = True
                imgVisacancel.ImageUrl = "~/img/button_undocancel.png"
                imgVisacancel.ToolTip = "Undo Cancel"
            Else

                dvVisaConfirm.Visible = True
                divvisaremarks.Visible = True
                divvisacancel.Visible = True
                divvisaedit.Visible = True
                dvvisaCancelled.Visible = False
                imgVisacancel.ImageUrl = "~/img/button_cancel.png"
                imgVisacancel.ToolTip = "Cancel"
            End If

            If Session("sLoginType") <> "RO" Then
                dvVisaConfirm.Visible = False
            End If
            If Not Session("sEditRequestId") Is Nothing Then
                If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Then
                    dvVisaConfirm.Visible = False
                End If
            End If
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
            Dim imgbEdit As ImageButton = CType(e.Item.FindControl("imgbEdit"), ImageButton)
            If e.Item.ItemIndex = 0 Then
                hdSectorgroupcode.Value = ""
            End If
            Dim hdBookingMode As HiddenField = CType(e.Item.FindControl("hdBookingMode"), HiddenField) 'Added by abin on 20180716
            If hdBookingMode.Value <> "FreeForm" Then
                Dim dvBookingMode As HtmlGenericControl = CType(e.Item.FindControl("dvBookingMode"), HtmlGenericControl)
                dvBookingMode.Visible = False
            End If

            Dim imgtourcancel As ImageButton = CType(e.Item.FindControl("imgtourcancel"), ImageButton)
            Dim hdtourCancelled As HiddenField = CType(e.Item.FindControl("hdtourCancelled"), HiddenField)
            Dim divtourcancel As HtmlGenericControl = CType(e.Item.FindControl("divtourcancel"), HtmlGenericControl)
            Dim divtourremove As HtmlGenericControl = CType(e.Item.FindControl("divtourremove"), HtmlGenericControl)

            Dim dvTourConfirm As HtmlGenericControl = CType(e.Item.FindControl("dvTourConfirm"), HtmlGenericControl)
            Dim dvtourCancelled As HtmlGenericControl = CType(e.Item.FindControl("dvtourCancelled"), HtmlGenericControl)
            Dim divtourremarks As HtmlGenericControl = CType(e.Item.FindControl("divtourremarks"), HtmlGenericControl)
            Dim divtouredit As HtmlGenericControl = CType(e.Item.FindControl("divtouredit"), HtmlGenericControl)
            Dim dvPicupLocGroup As HtmlGenericControl = CType(e.Item.FindControl("dvPicupLocGroup"), HtmlGenericControl)
            Dim lblSectorgroupcode As Label = CType(e.Item.FindControl("lblSectorgroupcode"), Label)
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
                dt = objBLLTourSearch.GetBookedComboMultiDateDetails(lblRequestId.Text, lblExcursionCode.Text, "MULTI_DATE", lblelineno.Text.Trim)
                If dt.Rows.Count > 0 Then
                    lblexcursiondate.Text = "<ul >"
                    For i As Integer = 0 To dt.Rows.Count - 1
                        lblexcursiondate.Text = lblexcursiondate.Text & " <li type='square' style='margin-left:-30px;padding-bottom:10px;'>" & dt.Rows(i)("excdate").ToString & "</li> "
                    Next
                    lblexcursiondate.Text = lblexcursiondate.Text & "</ul>"
                End If
            End If
           

            'If hdSectorgroupcode.Value = "" And hdtourCancelled.Value = "1" Then
            '    hdSectorgroupcode.Value = ""
            'Else
            '    hdSectorgroupcode.Value = lblSectorgroupcode.Text.Trim
            'End If
            'Modified by abin on 20201026
            If hdSectorgroupcode.Value.Trim = lblSectorgroupcode.Text.Trim Then ' And e.Item.ItemIndex > 0
                dvPicupLocGroup.Style.Add("display", "none")
            End If
            If hdtourCancelled.Value <> "1" Then
                hdSectorgroupcode.Value = lblSectorgroupcode.Text.Trim
            End If

            If hdBookingMode.Value = "FreeForm" Then
                dvPicupLocGroup.Style.Add("display", "block")
            End If





            If Not Session("sEditRequestId") Is Nothing Then

                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    divtourcancel.Style.Add("display", "block")
                    divtourremove.Style.Add("display", "none")
                    imgbEdit.ImageUrl = "~/img/button_amend.png"
                Else
                    divtourcancel.Style.Add("display", "none")
                    divtourremove.Style.Add("display", "block")
                    imgbEdit.ImageUrl = "~/img/button_edit.png"
                End If

            Else

                divtourcancel.Style.Add("display", "none")
                divtourremove.Style.Add("display", "block")
                imgbEdit.ImageUrl = "~/img/button_edit.png"
                'imgbDelete.ImageUrl = "~/img/button_remove.png"
            End If


            If HttpContext.Current.Session("sLoginType") <> "RO" Then
                divtourcancel.Style.Add("display", "none")
                divtourremove.Style.Add("display", "block")
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

            If hdtourCancelled.Value = "1" Then

                dvTourConfirm.Visible = False
                divtourcancel.Visible = True
                divtourremarks.Visible = False
                divtouredit.Visible = False
                dvtourCancelled.Visible = True
                imgtourcancel.ImageUrl = "~/img/button_undocancel.png"
                imgtourcancel.ToolTip = "Undo Cancel"
            Else

                dvTourConfirm.Visible = True
                divtourremarks.Visible = True
                divtourcancel.Visible = True
                divtouredit.Visible = True
                dvtourCancelled.Visible = False
                imgtourcancel.ImageUrl = "~/img/button_cancel.png"
                imgtourcancel.ToolTip = "Cancel"
            End If

            If Session("sLoginType") <> "RO" Then
                dvTourConfirm.Visible = False
            End If
            If Not Session("sEditRequestId") Is Nothing Then
                If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Then
                    dvTourConfirm.Visible = False
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

            Dim hdBookingMode As HiddenField = CType(e.Item.FindControl("hdBookingMode"), HiddenField) 'Added by abin on 20180716
            If hdBookingMode.Value <> "FreeForm" Then
                Dim dvBookingMode As HtmlGenericControl = CType(e.Item.FindControl("dvBookingMode"), HtmlGenericControl)
                dvBookingMode.Visible = False
            End If

            Dim imgTrfEdit As ImageButton = CType(e.Item.FindControl("imgTrfEdit"), ImageButton)
            Dim divTrfcancel As HtmlGenericControl = CType(e.Item.FindControl("divTrfcancel"), HtmlGenericControl)
            Dim divTrfremove As HtmlGenericControl = CType(e.Item.FindControl("divTrfremove"), HtmlGenericControl)
            Dim divTrfConfirm As HtmlGenericControl = CType(e.Item.FindControl("divTrfConfirm"), HtmlGenericControl)
            Dim dvtransferCancelled As HtmlGenericControl = CType(e.Item.FindControl("dvtransferCancelled"), HtmlGenericControl)
            Dim divtrfremarks As HtmlGenericControl = CType(e.Item.FindControl("divtrfremarks"), HtmlGenericControl)
            Dim divtrfedit As HtmlGenericControl = CType(e.Item.FindControl("divtrfedit"), HtmlGenericControl)
            Dim hdtrfCancelled As HiddenField = CType(e.Item.FindControl("hdtrfCancelled"), HiddenField)
            Dim imgTrfcancel As ImageButton = CType(e.Item.FindControl("imgTrfcancel"), ImageButton)

            If Not Session("sEditRequestId") Is Nothing Then
                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    divTrfcancel.Style.Add("display", "block")
                    divTrfremove.Style.Add("display", "none")
                    imgTrfEdit.ImageUrl = "~/img/button_amend.png"
                    imgTrfEdit.ToolTip = "Amend"
                Else
                    divTrfcancel.Style.Add("display", "none")
                    divTrfremove.Style.Add("display", "block")
                    imgTrfEdit.ImageUrl = "~/img/button_edit.png"
                    imgTrfEdit.ToolTip = "Edit"
                End If

            Else

                divTrfcancel.Style.Add("display", "none")
                divTrfremove.Style.Add("display", "block")
                imgTrfEdit.ImageUrl = "~/img/button_edit.png"
                imgTrfEdit.ToolTip = "Edit"
            End If


            If HttpContext.Current.Session("sLoginType") <> "RO" Then
                divTrfcancel.Style.Add("display", "none")
                divTrfremove.Style.Add("display", "block")
                divTrfConfirm.Visible = False
            End If


            If hdBookingEngineRateType.Value = "1" Then
                dvTransferUnitPrice.Visible = False
            Else
                dvTransferUnitPrice.Visible = True
            End If

            If hdtrfCancelled.Value = "1" Then

                divTrfConfirm.Visible = False
                divTrfcancel.Visible = True
                divtrfremarks.Visible = False
                divtrfedit.Visible = False
                dvtransferCancelled.Visible = True
                imgTrfcancel.ImageUrl = "~/img/button_undocancel.png"
                imgTrfcancel.ToolTip = "Undo Cancel"
            Else

                divTrfConfirm.Visible = True
                divtrfremarks.Visible = True
                divTrfcancel.Visible = True
                divtrfedit.Visible = True
                dvtransferCancelled.Visible = False
                imgTrfcancel.ImageUrl = "~/img/button_cancel.png"
                imgTrfcancel.ToolTip = "Cancel"
            End If
            If Session("sLoginType") <> "RO" Then
                divTrfConfirm.Visible = False
            End If
            If Not Session("sEditRequestId") Is Nothing Then
                If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Then
                    divTrfConfirm.Visible = False
                End If
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
        Dim strRequestId As String = "" ' Modifed by abin on 20180724
        If Not Session("sRequestId") Is Nothing Then
            strRequestId = Session("sRequestId")
        End If
        Return strRequestId
    End Function

    Private Function ValidateGuestRemarks() As String
        Dim remarktemplate As Boolean = False
        For Each item As ListItem In chkHotelRemarks.Items
            If item.Selected Then
                remarktemplate = True
                Exit For
            End If
        Next
        If txthotremarks.Text.Trim = "" And txtcustRemarks.Text.Trim = "" And txtDeptRemarks.Text.Trim = "" And txtArrRemarks.Text.Trim = "" And remarktemplate = False Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Fill atleast one  RemarksType")
            txthotremarks.Focus()
            mpRemarks.Show()
            Return False
            Exit Function
        End If
        Return True
    End Function

    Protected Sub btnSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRemarks.Click
        Try
            If ValidateGuestRemarks() Then
                Dim hoteltemplatestring As New Text.StringBuilder
                objBLLguest.GBRequestid = GetRequestId()
                If hdnrlineno.Value <> "" Then
                    objBLLguest.GBGuestLineNo = hdnrlineno.Value
                End If
                ' lblrlineno.Text
                For Each item As ListItem In chkHotelRemarks.Items
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
                    objBLLguest.GBAgentRemarks = ""
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
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnSaveRemarks_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

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


            Dim imgAirEdit As ImageButton = CType(e.Item.FindControl("imgAirEdit"), ImageButton)
            Dim imgAircancel As ImageButton = CType(e.Item.FindControl("imgAircancel"), ImageButton)
            Dim divAircancel As HtmlGenericControl = CType(e.Item.FindControl("divAircancel"), HtmlGenericControl)
            Dim divAirremove As HtmlGenericControl = CType(e.Item.FindControl("divAirremove"), HtmlGenericControl)
            Dim dvAirportmateConfirm As HtmlGenericControl = CType(e.Item.FindControl("dvAirportmateConfirm"), HtmlGenericControl)
            Dim dvairportCancelled As HtmlGenericControl = CType(e.Item.FindControl("dvairportCancelled"), HtmlGenericControl)
            Dim divairremarks As HtmlGenericControl = CType(e.Item.FindControl("divairremarks"), HtmlGenericControl)
            Dim divairedit As HtmlGenericControl = CType(e.Item.FindControl("divairedit"), HtmlGenericControl)
            Dim hdairCancelled As HiddenField = CType(e.Item.FindControl("hdairCancelled"), HiddenField)

            Dim hdBookingMode As HiddenField = CType(e.Item.FindControl("hdBookingMode"), HiddenField) 'Added by abin on 20180724
            If hdBookingMode.Value <> "FreeForm" Then
                Dim dvBookingMode As HtmlGenericControl = CType(e.Item.FindControl("dvBookingMode"), HtmlGenericControl)
                dvBookingMode.Visible = False
            End If

            If Not Session("sEditRequestId") Is Nothing Then

                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    divAircancel.Style.Add("display", "block")
                    divAirremove.Style.Add("display", "none")
                    imgAirEdit.ImageUrl = "~/img/button_amend.png"
                    imgAirEdit.ToolTip = "Amend"
                Else
                    divAircancel.Style.Add("display", "none")
                    divAirremove.Style.Add("display", "block")
                    imgAirEdit.ImageUrl = "~/img/button_edit.png"
                    imgAirEdit.ToolTip = "Edit"
                End If

            Else

                divAircancel.Style.Add("display", "none")
                divAirremove.Style.Add("display", "block")
                imgAirEdit.ImageUrl = "~/img/button_edit.png"
                imgAirEdit.ToolTip = "Edit"
            End If


            If HttpContext.Current.Session("sLoginType") <> "RO" Then
                divAircancel.Style.Add("display", "none")
                divAirremove.Style.Add("display", "block")
            End If

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

            If hdairCancelled.Value = "1" Then

                dvAirportmateConfirm.Style.Add("display", "none")
                divAircancel.Style.Add("display", "block")
                divairremarks.Style.Add("display", "none")
                divairedit.Style.Add("display", "none")
                dvairportCancelled.Style.Add("display", "block")
                imgAircancel.ImageUrl = "~/img/button_undocancel.png"
                imgAircancel.ToolTip = "Undo Cancel"

            Else

                dvAirportmateConfirm.Style.Add("display", "block")
                divairremarks.Style.Add("display", "block")

                divairedit.Style.Add("display", "block")
                dvairportCancelled.Style.Add("display", "none")
                If Not Session("sEditRequestId") Is Nothing Then
                    imgAircancel.ImageUrl = "~/img/button_cancel.png"
                    imgAircancel.ToolTip = "Cancel"
                    divAircancel.Style.Add("display", "block")
                    divAirremove.Style.Add("display", "none")
                Else
                    'imgAircancel.ImageUrl = "~/img/button_cancel.png"
                    'imgAircancel.ToolTip = "Remove"
                    'divAircancel.Style.Add("display", "none")
                    'divAirremove.Style.Add("display", "block")
                End If



            End If

            If Session("sLoginType") <> "RO" Then
                dvAirportmateConfirm.Visible = False
            End If
            If Not Session("sEditRequestId") Is Nothing Then
                If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Then
                    dvAirportmateConfirm.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub dlOtherSummary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlOtherSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim dvothUnitPrice As HtmlGenericControl = CType(e.Item.FindControl("dvothUnitPrice"), HtmlGenericControl)
            Dim dvothwlUnitPrice As HtmlGenericControl = CType(e.Item.FindControl("dvothwlUnitPrice"), HtmlGenericControl)

            Dim imgothEdit As ImageButton = CType(e.Item.FindControl("imgothEdit"), ImageButton)



            Dim imgothercancel As ImageButton = CType(e.Item.FindControl("imgothercancel"), ImageButton)
            Dim hdothCancelled As HiddenField = CType(e.Item.FindControl("hdothCancelled"), HiddenField)
            Dim divothedit As HtmlGenericControl = CType(e.Item.FindControl("divothedit"), HtmlGenericControl)
            Dim divothercancel As HtmlGenericControl = CType(e.Item.FindControl("divothercancel"), HtmlGenericControl)
            Dim divotherremove As HtmlGenericControl = CType(e.Item.FindControl("divotherremove"), HtmlGenericControl)
            Dim dvOtherConfirm As HtmlGenericControl = CType(e.Item.FindControl("dvOtherConfirm"), HtmlGenericControl)
            Dim dvotherCancelled As HtmlGenericControl = CType(e.Item.FindControl("dvotherCancelled"), HtmlGenericControl)
            Dim divothremarks As HtmlGenericControl = CType(e.Item.FindControl("divothremarks"), HtmlGenericControl)
            Dim hdBookingMode As HiddenField = CType(e.Item.FindControl("hdBookingMode"), HiddenField) 'Added by abin on 20180716
            If hdBookingMode.Value <> "FreeForm" Then
                Dim dvBookingMode As HtmlGenericControl = CType(e.Item.FindControl("dvBookingMode"), HtmlGenericControl)
                dvBookingMode.Visible = False
            End If

            If Not Session("sEditRequestId") Is Nothing Then
                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then
                    divothercancel.Style.Add("display", "block")
                    divotherremove.Style.Add("display", "none")
                    imgothEdit.ImageUrl = "~/img/button_amend.png"
                    imgothEdit.ToolTip = "Amend"
                Else
                    divothercancel.Style.Add("display", "none")
                    divotherremove.Style.Add("display", "block")
                    imgothEdit.ImageUrl = "~/img/button_edit.png"
                    imgothEdit.ToolTip = "Edit"
                End If



            Else

                divothercancel.Style.Add("display", "none")
                divotherremove.Style.Add("display", "block")
                imgothEdit.ImageUrl = "~/img/button_edit.png"
                imgothEdit.ToolTip = "Edit"
            End If


            If HttpContext.Current.Session("sLoginType") <> "RO" Then
                divothercancel.Style.Add("display", "none")
                divotherremove.Style.Add("display", "block")
            End If


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

            End If

            If hdothCancelled.Value = "1" Then

                dvOtherConfirm.Visible = False
                divothercancel.Visible = True
                divothremarks.Visible = False
                divothedit.Visible = False
                dvotherCancelled.Visible = True
                imgothercancel.ImageUrl = "~/img/button_undocancel.png"
                imgothercancel.ToolTip = "Undo Cancel"

            Else

                dvOtherConfirm.Visible = True
                divothremarks.Visible = True
                divothercancel.Visible = True
                divothedit.Visible = True
                dvotherCancelled.Visible = False
                imgothercancel.ImageUrl = "~/img/button_cancel.png"
                imgothercancel.ToolTip = "Cancel"

            End If

            If Session("sLoginType") <> "RO" Then
                dvOtherConfirm.Visible = False
            End If
            If Not Session("sEditRequestId") Is Nothing Then
                If Session("sEditRequestId").ToString.Contains("QR/") Or Session("sEditRequestId").ToString.Contains("QG/") Then
                    dvOtherConfirm.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub btnProceedBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceedBooking.Click

        Dim objBLLCommonFuntions As New BLLCommonFuntions
        Dim strRequestId As String = Session("sRequestId")
        Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(strRequestId)
        If dt.Rows.Count > 0 Then


            '' Added by abin on 20191110
            If strRequestId.Contains("QG") Or strRequestId.Contains("QP") Then
                Dim dtVal As DataTable
                dtVal = objclsUtilities.GetDataFromDataTable("exec sp_validate_booking_quotation_edit_final  '" + strRequestId + "'")
                Dim strMsg As String = ""
                If dtVal.Rows.Count > 0 Then
                    For i As Integer = 0 To dtVal.Rows.Count - 1
                        strMsg = strMsg & dtVal.Rows(i)("warning").ToString & "</br>"
                    Next
                    strMsg = strMsg & " So this quote cannot convert to booking."
                    MessageBox.ShowMessage(Page, MessageType.Warning, strMsg)
                    Exit Sub
                End If
            End If



            '' Added by abin on 20171011

            Dim strPackageSummary As New StringBuilder
            Dim strPackageValueSummary As New StringBuilder
            Dim iCumulative As Integer = 0
            If Session("sLoginType") = "RO" Then
                iCumulative = objBLLguest.CheckSelectedAgentIsCumulative(strRequestId)
            End If
            FindCumilative()
            If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
                iCumulative = 1
                objBLLguest.Cumulative = iCumulative
                objBLLguest.GBRequestid = strRequestId
                lblPRequestId.Text = strRequestId
                If Not Session("sdsPackageSummary") Is Nothing Then
                    Dim ds As DataSet = Session("sdsPackageSummary")
                    If ds.Tables(0).Rows.Count > 0 Then

                        Dim strMessage As String = ""
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            strMessage = strMessage & "\n " & ds.Tables(0).Rows(i)("errdesc").ToString
                        Next
                        If strMessage.Contains("cannot proceed") Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, strMessage.Replace("\n", "</br>"))
                            Exit Sub
                        End If

                        '  MessageBox.ShowMessage(Page, MessageType.Warning, ds.Tables(0).Rows(0)("errdesc").ToString)

                        ' MessageBox.ShowMessage(Page, MessageType.Warning, ds.Tables(0).Rows(0)("errdesc").ToString)
                        btnBacktoBookingForPackage.Visible = False
                        btnProceedWithBook.Visible = True
                        gvPackageConfirmError.DataSource = ds.Tables(0)
                        gvPackageConfirmError.DataBind()
                        mpPackageConfirmError.Show()

                        Exit Sub
                    Else


                        If ds.Tables(1).Rows.Count > 0 Then
                            strPackageSummary.Append("<DocumentElement>")
                            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                                strPackageSummary.Append("<Table>")
                                strPackageSummary.Append("<requestid>" & strRequestId & "</requestid>")
                                strPackageSummary.Append("<requesttype>" & ds.Tables(1).Rows(i)("requesttype").ToString & "</requesttype>")
                                strPackageSummary.Append("<rlineno>" & ds.Tables(1).Rows(i)("rlineno").ToString & "</rlineno>")
                                strPackageSummary.Append("<adults>" & ds.Tables(1).Rows(i)("adults").ToString & "</adults>")
                                strPackageSummary.Append("<child>" & ds.Tables(1).Rows(i)("child").ToString & "</child>")
                                strPackageSummary.Append("<salevalue>" & ds.Tables(1).Rows(i)("salevalue").ToString & "</salevalue>")
                                strPackageSummary.Append("<salevaluebase>" & ds.Tables(1).Rows(i)("salevaluebase").ToString & "</salevaluebase>")
                                strPackageSummary.Append("<costvalue>" & ds.Tables(1).Rows(i)("costvalue").ToString & "</costvalue>")
                                strPackageSummary.Append("</Table>")

                            Next
                            strPackageSummary.Append("</DocumentElement>")
                            objBLLguest.PackageSummary = strPackageSummary.ToString
                        End If

                        If ds.Tables(2).Rows.Count > 0 Then
                            strPackageValueSummary.Append("<DocumentElement>")
                            For i As Integer = 0 To ds.Tables(2).Rows.Count - 1
                                strPackageValueSummary.Append("<Table>")
                                strPackageValueSummary.Append("<requestid>" & strRequestId & "</requestid>")
                                strPackageValueSummary.Append("<totalsalevalue>" & ds.Tables(2).Rows(i)("totalsalevalue").ToString & "</totalsalevalue>")
                                strPackageValueSummary.Append("<totalsalevaluebase>" & ds.Tables(2).Rows(i)("totalsalevaluebase").ToString & "</totalsalevaluebase>")
                                strPackageValueSummary.Append("<totalcostvaluebase>" & ds.Tables(2).Rows(i)("totalcostvaluebase").ToString & "</totalcostvaluebase>")
                                strPackageValueSummary.Append("<adults>" & Val(ds.Tables(2).Rows(i)("adults").ToString) & "</adults>")
                                strPackageValueSummary.Append("<child>" & Val(ds.Tables(2).Rows(i)("child").ToString) & "</child>")
                                strPackageValueSummary.Append("<withvisa>" & Val(ds.Tables(2).Rows(i)("withvisa").ToString) & "</withvisa>")
                                strPackageValueSummary.Append("<adultmarkup>" & Val(ds.Tables(2).Rows(i)("adultmarkup").ToString) & "</adultmarkup>")
                                strPackageValueSummary.Append("<childmarkup>" & Val(ds.Tables(2).Rows(i)("childmarkup").ToString) & "</childmarkup>")
                                strPackageValueSummary.Append("<minimummarkup>" & Val(ds.Tables(2).Rows(i)("minimummarkup").ToString) & "</minimummarkup>")
                                strPackageValueSummary.Append("<totalmarkupbase>" & Val(ds.Tables(2).Rows(i)("totalmarkupbase").ToString) & "</totalmarkupbase>")
                                strPackageValueSummary.Append("<differentialmarkup>" & Val(ds.Tables(2).Rows(i)("differentialmarkup").ToString) & "</differentialmarkup>")
                                strPackageValueSummary.Append("<formulaid>" & ds.Tables(2).Rows(i)("formulaid").ToString & "</formulaid>")
                                strPackageValueSummary.Append("<flineno>" & Val(ds.Tables(2).Rows(i)("flineno").ToString) & "</flineno>")
                                strPackageValueSummary.Append("<fromslab>" & Val(ds.Tables(2).Rows(i)("fromslab").ToString) & "</fromslab>")
                                strPackageValueSummary.Append("<toslab>" & Val(ds.Tables(2).Rows(i)("toslab").ToString) & "</toslab>")
                                strPackageValueSummary.Append("<discountperc>" & Val(ds.Tables(2).Rows(i)("discountperc").ToString) & "</discountperc>")
                                strPackageValueSummary.Append("<discountmarkup>" & Val(ds.Tables(2).Rows(i)("discountmarkup").ToString) & "</discountmarkup>")
                                strPackageValueSummary.Append("<netsalevalue>" & Val(ds.Tables(2).Rows(i)("netsalevalue").ToString) & "</netsalevalue>")
                                '' Added shahul 11/04/18
                                strPackageValueSummary.Append("<childfreeupto>" & Val(ds.Tables(2).Rows(i)("childfreeupto").ToString) & "</childfreeupto>")
                                strPackageValueSummary.Append("<ChildFreeWithVisa>" & Val(ds.Tables(2).Rows(i)("ChildFreeWithVisa").ToString) & "</ChildFreeWithVisa>")
                                strPackageValueSummary.Append("<ChildFreeWithoutVisa>" & Val(ds.Tables(2).Rows(i)("ChildFreeWithoutVisa").ToString) & "</ChildFreeWithoutVisa>")

                                '' Added shahul 01/07/18  temporaly commentd once finish development uncomment again
                                strPackageValueSummary.Append("<adultrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("adultrevisedmarkup").ToString) & "</adultrevisedmarkup>")
                                strPackageValueSummary.Append("<adultreviseddiscount>" & Val(ds.Tables(2).Rows(i)("adultreviseddiscount").ToString) & "</adultreviseddiscount>")
                                strPackageValueSummary.Append("<childrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("childrevisedmarkup").ToString) & "</childrevisedmarkup>")
                                strPackageValueSummary.Append("<childreviseddiscount>" & Val(ds.Tables(2).Rows(i)("childreviseddiscount").ToString) & "</childreviseddiscount>")
                                strPackageValueSummary.Append("<childfreeuptorevisedmarkup>" & Val(ds.Tables(2).Rows(i)("childfreeuptorevisedmarkup").ToString) & "</childfreeuptorevisedmarkup>")
                                strPackageValueSummary.Append("<childfreeuptoreviseddiscount>" & Val(ds.Tables(2).Rows(i)("childfreeuptoreviseddiscount").ToString) & "</childfreeuptoreviseddiscount>")
                                strPackageValueSummary.Append("<totalrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("totalrevisedmarkup").ToString) & "</totalrevisedmarkup>")
                                strPackageValueSummary.Append("<totalreviseddiscount>" & Val(ds.Tables(2).Rows(i)("totalreviseddiscount").ToString) & "</totalreviseddiscount>")

                                strPackageValueSummary.Append("</Table>")

                            Next
                            strPackageValueSummary.Append("</DocumentElement>")
                            objBLLguest.PackageValueSummary = strPackageValueSummary.ToString
                            Dim strResult As String = objBLLguest.SaveBookingProfitInTemp()

                        End If
                    End If
                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please generate package value.")
                    Exit Sub
                End If
            End If
            If objBLLguest.PrearrangedValidate(Session("sRequestId")) = 1 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Can not book pre-arranged hotel only.")
                Exit Sub
            End If
            Response.Redirect("~\GuestPagenew.aspx")
        Else
            MessageBox.ShowMessage(Page, MessageType.Warning, "There is no Service to be Booked Please Select ")
            Exit Sub
        End If


    End Sub
    Protected Sub gvVisaConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVisaConfirm.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim txtCancelDays As TextBox = CType(e.Row.FindControl("txtCancelDays"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtConfirmDate"), TextBox)
            Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)


            Dim txtTimeLimitTime As HtmlInputText = CType(e.Row.FindControl("txtTimeLimitTime"), HtmlInputText)

            txtTimeLimitTime.Value = objBLLguest.getdefaulttimeforconfirm(" select option_selected  from reservation_parameters  where param_id =2010")

            Dim timelimitdate As Date = CType(lblClblVisaDate.Text, Date)

            txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer)))


            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:ChangeDateForAll('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "','" + CType(lblClblVisaDate.ClientID, String) + "' )")

        End If
    End Sub
    Protected Sub gvOthersConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOthersConfirm.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim txtCancelDays As TextBox = CType(e.Row.FindControl("txtCancelDays"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtConfirmDate"), TextBox)
            Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)


            Dim txtTimeLimitTime As HtmlInputText = CType(e.Row.FindControl("txtTimeLimitTime"), HtmlInputText)

            txtTimeLimitTime.Value = objBLLguest.getdefaulttimeforconfirm(" select option_selected  from reservation_parameters  where param_id =2010")

            Dim timelimitdate As Date = CType(lblClblOtherdate.Text, Date)

            txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer)))


            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:ChangeDateForAll('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "','" + CType(lblClblOtherdate.ClientID, String) + "' )")

        End If
    End Sub


    Protected Sub gvAirportMateConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAirportMateConfirm.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim txtCancelDays As TextBox = CType(e.Row.FindControl("txtCancelDays"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtConfirmDate"), TextBox)
            Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)


            Dim txtTimeLimitTime As HtmlInputText = CType(e.Row.FindControl("txtTimeLimitTime"), HtmlInputText)

            txtTimeLimitTime.Value = objBLLguest.getdefaulttimeforconfirm(" select option_selected  from reservation_parameters  where param_id =2010")

            Dim timelimitdate As Date = CType(lblClblAirportMateDate.Text, Date)

            txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer)))


            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:ChangeDateForAll('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "','" + CType(lblClblAirportMateDate.ClientID, String) + "' )")

        End If
    End Sub
    Protected Sub gvToursConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvToursConfirm.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim txtCancelDays As TextBox = CType(e.Row.FindControl("txtCancelDays"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtConfirmDate"), TextBox)
            Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)


            Dim txtTimeLimitTime As HtmlInputText = CType(e.Row.FindControl("txtTimeLimitTime"), HtmlInputText)

            txtTimeLimitTime.Value = objBLLguest.getdefaulttimeforconfirm(" select option_selected  from reservation_parameters  where param_id =2010")
            Dim timelimitdate As Date
            Try
                timelimitdate = CType(lblClblTourdate.Text, Date)

                txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer)))
            Catch ex As Exception

            End Try




            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:ChangeDateForAll('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "','" + CType(lblClblTourdate.ClientID, String) + "')")

        End If
    End Sub

    Protected Sub gvTransferConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTransferConfirm.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim txtCancelDays As TextBox = CType(e.Row.FindControl("txtCancelDays"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtConfirmDate"), TextBox)
            Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)


            Dim txtTimeLimitTime As HtmlInputText = CType(e.Row.FindControl("txtTimeLimitTime"), HtmlInputText)

            txtTimeLimitTime.Value = objBLLguest.getdefaulttimeforconfirm(" select option_selected  from reservation_parameters  where param_id =2010")

            Dim timelimitdate As Date = CType(lblClbltransdate.Text, Date)

            txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer)))


            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:TrfChangeDate('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "')")

        End If
    End Sub

    Protected Sub gvConfirmBooking_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvConfirmBooking.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            'Dim lblNoOfRooms As Label = CType(e.Row.FindControl("lblNoOfRooms"), Label)
            'Dim txtDays As TextBox = CType(e.Row.FindControl("txtDays"), TextBox)
            'Dim txtTimeLimitDate As TextBox = CType(e.Row.FindControl("txtTimeLimitDate"), TextBox)
            'Dim txtconfirmDate As TextBox = CType(e.Row.FindControl("txtconfirmdate"), TextBox)
            'Dim txtPrevConfNo As TextBox = CType(e.Row.FindControl("txtPrevConfNo"), TextBox)
            'Dim PrevConfNo As HtmlGenericControl = CType(e.Row.FindControl("PrevConfNo"), HtmlGenericControl)
            'Dim dvconfirmdiv As HtmlGenericControl = CType(e.Row.FindControl("dvconfirmdiv"), HtmlGenericControl)

            'txtPrevConfNo.Attributes.Add("readonly", "readonly")
            'txtDays.Attributes.Add("onchange", "javascript:ChangeDate('" + CType(txtDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "')")
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

            txtTimeLimitDate.Text = timelimitdate.AddDays(-(CType(txtCancelDays.Text, Integer) + 1)) '' Srinivias wants to add 1 day as per his email 26/08/17


            txtPrevConfNo.Attributes.Add("readonly", "readonly")
            txtCancelDays.Attributes.Add("onchange", "javascript:ChangeDate('" + CType(txtCancelDays.ClientID, String) + "','" + CType(txtTimeLimitDate.ClientID, String) + "')")

        End If

    End Sub

    Function ValidateCancelBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvHotelCancel.Rows
            Dim txthotelcancelno As TextBox = CType(gvrow.FindControl("txthotelcancelno"), TextBox)
            Dim txtHotelCancelDate As TextBox = CType(gvrow.FindControl("txtHotelCancelDate"), TextBox)
            Dim chkHotelCancel As CheckBox = CType(gvrow.FindControl("chkHotelCancel"), CheckBox)
            If chkHotelCancel.Checked = True Then
                Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)
                'If txthotelcancelno.Text = "" Then
                '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                '    txthotelcancelno.Focus()
                '    ValidateCancelBookingDetails = False
                '    Exit Function
                'End If
                If txtHotelCancelDate.Text = "DD/MM/YYYY" Or txtHotelCancelDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                    txtHotelCancelDate.Focus()
                    ValidateCancelBookingDetails = False
                    Exit Function
                End If
            End If



        Next
        Return True
    End Function
    Function ValidateTransferCancelDetails() As Boolean
        For Each gvrow As GridViewRow In gvTransferCancel.Rows
            Dim txttransfercancelno As TextBox = CType(gvrow.FindControl("txtTrfcancelno"), TextBox)
            Dim txttrfCancelDate As TextBox = CType(gvrow.FindControl("txttrfCancelDate"), TextBox)

            Dim chkTrfCancel As CheckBox = CType(gvrow.FindControl("chkTrfCancel"), CheckBox)
            If chkTrfCancel.Checked = True Then
                If txttransfercancelno.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                    txttransfercancelno.Focus()
                    ValidateTransferCancelDetails = False
                    Exit Function
                End If
                If txttrfCancelDate.Text = "DD/MM/YYYY" Or txttrfCancelDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Cancel-Date.")
                    txttrfCancelDate.Focus()
                    ValidateTransferCancelDetails = False
                    Exit Function
                End If
            End If

        Next
        Return True
    End Function
    Function ValidateTourCancelDetails() As Boolean
        For Each gvrow As GridViewRow In gvtourCancel.Rows
            Dim txttourcancelno As TextBox = CType(gvrow.FindControl("txttourcancelno"), TextBox)
            Dim txttourCancelDate As TextBox = CType(gvrow.FindControl("txttourCancelDate"), TextBox)

            Dim chktourCancel As CheckBox = CType(gvrow.FindControl("chktourCancel"), CheckBox)
            If chktourCancel.Checked = True Then
                If txttourcancelno.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                    txttourcancelno.Focus()
                    ValidateTourCancelDetails = False
                    Exit Function
                End If
                If txttourCancelDate.Text = "DD/MM/YYYY" Or txttourCancelDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Cancel-Date.")
                    txttourCancelDate.Focus()
                    ValidateTourCancelDetails = False
                    Exit Function
                End If
            End If

        Next
        Return True
    End Function
    Function ValidateAirportCancelDetails() As Boolean
        For Each gvrow As GridViewRow In gvairCancel.Rows
            Dim txtaircancelno As TextBox = CType(gvrow.FindControl("txtaircancelno"), TextBox)
            Dim txtaircancelDate As TextBox = CType(gvrow.FindControl("txtaircancelDate"), TextBox)

            Dim chkairCancel As CheckBox = CType(gvrow.FindControl("chkairCancel"), CheckBox)
            If chkairCancel.Checked = True Then
                If txtaircancelno.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                    txtaircancelno.Focus()
                    ValidateAirportCancelDetails = False
                    Exit Function
                End If
                If txtaircancelDate.Text = "DD/MM/YYYY" Or txtaircancelDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Cancel-Date.")
                    txtaircancelDate.Focus()
                    ValidateAirportCancelDetails = False
                    Exit Function
                End If
            End If

        Next
        Return True
    End Function
    Function ValidateOtherCancelDetails() As Boolean
        For Each gvrow As GridViewRow In gvotherCancel.Rows
            Dim txtotherscancelno As TextBox = CType(gvrow.FindControl("txtotherscancelno"), TextBox)
            Dim txtothcancelDate As TextBox = CType(gvrow.FindControl("txtothcancelDate"), TextBox)

            Dim chkothCancel As CheckBox = CType(gvrow.FindControl("chkothCancel"), CheckBox)
            If chkothCancel.Checked = True Then
                If txtotherscancelno.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                    txtotherscancelno.Focus()
                    ValidateOtherCancelDetails = False
                    Exit Function
                End If
                If txtothcancelDate.Text = "DD/MM/YYYY" Or txtothcancelDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Cancel-Date.")
                    txtothcancelDate.Focus()
                    ValidateOtherCancelDetails = False
                    Exit Function
                End If
            End If

        Next
        Return True
    End Function
    Function ValidateVisaBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvVisaCancel.Rows
            Dim txtVisacancelno As TextBox = CType(gvrow.FindControl("txtVisacancelno"), TextBox)
            Dim txtvisacancelDate As TextBox = CType(gvrow.FindControl("txtvisacancelDate"), TextBox)
            Dim chkVisaCancel As CheckBox = CType(gvrow.FindControl("chkVisaCancel"), CheckBox)

            If chkVisaCancel.Checked = True Then
                If txtVisacancelno.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                    txtVisacancelno.Focus()
                    ValidateVisaBookingDetails = False
                    Exit Function
                End If
                If txtvisacancelDate.Text = "DD/MM/YYYY" Or txtvisacancelDate.Text = "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Visa CancelDate.")
                    txtvisacancelDate.Focus()
                    ValidateVisaBookingDetails = False
                    Exit Function
                End If
            End If

        Next
        Return True
    End Function


    Function ValidateConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvConfirmBooking.Rows
            Dim txthotelconfno As TextBox = CType(gvrow.FindControl("txthotelconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

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

    Function ValidateAirportmateConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvAirportMateConfirm.Rows
            Dim txtAirportmateconfno As TextBox = CType(gvrow.FindControl("txtAirportmateconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

            If txtAirportmateconfno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No.")
                txtAirportmateconfno.Focus()
                ValidateAirportmateConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitDate.Text = "DD/MM/YYYY" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                txtTimeLimitDate.Focus()
                ValidateAirportmateConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitTime.Value = "hh:mm" Or txtTimeLimitTime.Value = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit- Time.")
                txtTimeLimitTime.Focus()
                ValidateAirportmateConfirmBookingDetails = False
                Exit Function
            End If

        Next
        Return True
    End Function

    Function ValidateVisaConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvVisaConfirm.Rows
            Dim txtvisaconfno As TextBox = CType(gvrow.FindControl("txtvisaconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

            If txtvisaconfno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No.")
                txtvisaconfno.Focus()
                ValidateVisaConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitDate.Text = "DD/MM/YYYY" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                txtTimeLimitDate.Focus()
                ValidateVisaConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitTime.Value = "hh:mm" Or txtTimeLimitTime.Value = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit- Time.")
                txtTimeLimitTime.Focus()
                ValidateVisaConfirmBookingDetails = False
                Exit Function
            End If

        Next
        Return True
    End Function

    Function ValidateOthersConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvOthersConfirm.Rows
            Dim txtothersconfno As TextBox = CType(gvrow.FindControl("txtothersconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

            If txtothersconfno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No.")
                txtothersconfno.Focus()
                ValidateOthersConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitDate.Text = "DD/MM/YYYY" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                txtTimeLimitDate.Focus()
                ValidateOthersConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitTime.Value = "hh:mm" Or txtTimeLimitTime.Value = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit- Time.")
                txtTimeLimitTime.Focus()
                ValidateOthersConfirmBookingDetails = False
                Exit Function
            End If

        Next
        Return True
    End Function

    Function ValidateToursConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvTransferConfirm.Rows
            Dim txtToursconfno As TextBox = CType(gvrow.FindControl("txtToursconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

            If txtToursconfno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No.")
                txtToursconfno.Focus()
                ValidateToursConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitDate.Text = "DD/MM/YYYY" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                txtTimeLimitDate.Focus()
                ValidateToursConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitTime.Value = "hh:mm" Or txtTimeLimitTime.Value = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit- Time.")
                txtTimeLimitTime.Focus()
                ValidateToursConfirmBookingDetails = False
                Exit Function
            End If

        Next
        Return True
    End Function
    Function ValidateTrfConfirmBookingDetails() As Boolean
        For Each gvrow As GridViewRow In gvTransferConfirm.Rows
            Dim txttransferconfno As TextBox = CType(gvrow.FindControl("txttransferconfno"), TextBox)
            Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
            Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
            Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)

            If txttransferconfno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No.")
                txttransferconfno.Focus()
                ValidateTrfConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitDate.Text = "DD/MM/YYYY" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit-Date.")
                txtTimeLimitDate.Focus()
                ValidateTrfConfirmBookingDetails = False
                Exit Function
            End If
            If txtTimeLimitTime.Value = "hh:mm" Or txtTimeLimitTime.Value = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Time Limit- Time.")
                txtTimeLimitTime.Focus()
                ValidateTrfConfirmBookingDetails = False
                Exit Function
            End If

        Next
        Return True
    End Function

    Protected Sub btnHotelCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHotelCancelSave.Click
        Try
            If ValidateCancelBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvHotelCancel.Rows
                    Dim lbrLineNo As Label = CType(gvrow.FindControl("lbrLineNo"), Label)

                    Dim lblNoOfRooms As Label = CType(gvrow.FindControl("lblNoOfRooms"), Label)
                    Dim txthotelcancelno As TextBox = CType(gvrow.FindControl("txthotelcancelno"), TextBox)
                    Dim chkHotelCancel As CheckBox = CType(gvrow.FindControl("chkHotelCancel"), CheckBox)
                    Dim txtHotelCancelDate As TextBox = CType(gvrow.FindControl("txtHotelCancelDate"), TextBox)
                    Dim txtHotelCancelBy As TextBox = CType(gvrow.FindControl("txtHotelCancelBy"), TextBox)

                    'If txthotelcancelno.Text = "" Then
                    '    txthotelcancelno.Text = "Under Process"
                    'End If

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<rlineno>" & lbrLineNo.Text & "</rlineno>")
                    strbuffer.Append("<roomno>" & lblNoOfRooms.Text & "</roomno>") '.Split(" ")(1)
                    If chkHotelCancel.Checked = True Then
                        strbuffer.Append("<cancelled>1</cancelled>")
                    Else
                        strbuffer.Append("<cancelled>0</cancelled>")
                    End If
                    strbuffer.Append("<hotelcancelno>" & txthotelcancelno.Text & "</hotelcancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(txtHotelCancelDate.Text, Date), "yyyy/MM/dd") & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLguest.CBCancelXml = strbuffer.ToString
                objBLLguest.GBuserlogged = Session("GlobalUserName")
                If hdCancelEntireBooking.Value = "1" Then
                    objBLLguest.GBCancelEntireBooking = "1"
                Else
                    objBLLguest.GBCancelEntireBooking = "0"
                End If

                If objBLLguest.SavingCancelBookingDetailsInTemp() Then
                    BindBookingSummary()
                    BindVisaSummary()
                    BindTourSummary()
                    BindTransferSummary()
                    BindAirportserviceSummary()
                    BindOtherserviceSummary()
                    BindTotalValue()
                    Dim strFlag As String = "0"
                    For Each gvrow As GridViewRow In gvHotelCancel.Rows

                        Dim chkHotelCancel As CheckBox = CType(gvrow.FindControl("chkHotelCancel"), CheckBox)
                        If chkHotelCancel.Checked = True Then
                            strFlag = "1"
                            Exit For
                        End If
                    Next

                    If strFlag = "1" Then
                        MessageBox.ShowMessage(Page, MessageType.Success, "Cancelled Sucessfully...")
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Success, "Undo Cancelled Sucessfully...")
                    End If


                End If
            Else
                mpHotelCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnHotelCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnApplySameCancelChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplySameCancelChk.Click


        If chkApplySameCancel.Checked Then
            If gvHotelCancel.Rows.Count > 0 Then

                Dim dvhotelcancelno As HtmlGenericControl = CType(gvHotelCancel.Rows(0).FindControl("dvhotelcancelno"), HtmlGenericControl)


                Dim txthotelcancelno As TextBox = CType(gvHotelCancel.Rows(0).FindControl("txthotelcancelno"), TextBox)
                Dim chkHotelCancel As CheckBox = CType(gvHotelCancel.Rows(0).FindControl("chkHotelCancel"), CheckBox)
                Dim txtHotelCancelDate As TextBox = CType(gvHotelCancel.Rows(0).FindControl("txtHotelCancelDate"), TextBox)

                If (txthotelcancelno.Text <> "" Or txtHotelCancelDate.Text.Trim <> "") Then


                    For i = 1 To gvHotelCancel.Rows.Count - 1

                        Dim dvhotelcancelnoNew As HtmlGenericControl = CType(gvHotelCancel.Rows(i).FindControl("dvhotelcancelno"), HtmlGenericControl)


                        Dim txthotelcancelnoNew As TextBox = CType(gvHotelCancel.Rows(i).FindControl("txthotelcancelno"), TextBox)
                        Dim chkHotelCancelNew As CheckBox = CType(gvHotelCancel.Rows(i).FindControl("chkHotelCancel"), CheckBox)
                        Dim txtHotelCancelDateNew As TextBox = CType(gvHotelCancel.Rows(i).FindControl("txtHotelCancelDate"), TextBox)

                        If (txthotelcancelnoNew.Text.Trim <> txthotelcancelno.Text.Trim) Then
                            txthotelcancelnoNew.Text = txthotelcancelno.Text
                            chkApplySameCancel.Checked = False

                        End If
                        If (txtHotelCancelDateNew.Text.Trim <> txtHotelCancelDate.Text) Then
                            txtHotelCancelDateNew.Text = txtHotelCancelDate.Text
                            chkApplySameCancel.Checked = False
                        End If

                        If (chkHotelCancelNew.Checked = False) Then

                            chkHotelCancelNew.Checked = chkHotelCancel.Checked
                            chkApplySameCancel.Checked = False
                        End If
                    Next

                Else

                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Cancellation No. / Cancel Date in First Row...")
                    chkApplySameConf.Checked = False
                End If

            End If
            '   mpConfirm.Show()
        End If
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

                    If txtTimeLimitTime.Value = "" Or txtTimeLimitTime.Value = "0" Then
                        txtTimeLimitTime.Value = "00:00"
                    End If

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
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnsaveconfirm_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnVisaConfirmSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisaConfirmSave.Click
        Try

            If ValidateVisaConfirmBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvVisaConfirm.Rows
                    Dim lblRowNo As Label = CType(gvrow.FindControl("lblRowNo"), Label)
                    Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
                    Dim txtvisaconfno As TextBox = CType(gvrow.FindControl("txtvisaconfno"), TextBox)
                    Dim txtCancelDays As TextBox = CType(gvrow.FindControl("txtCancelDays"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
                    Dim hdnPrevConfNo As HiddenField = CType(gvrow.FindControl("hdnPrevConfNo"), HiddenField)
                    Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)
                    'Modified by abin on 20181223
                    If txtTimeLimitTime.Value = "" Or txtTimeLimitTime.Value = "0" Then
                        txtTimeLimitTime.Value = "00:00"
                    End If
                    Dim txtconfirmDate As TextBox = CType(gvrow.FindControl("txtconfirmdate"), TextBox)
                    Dim txtPrevConfNo As TextBox = CType(gvrow.FindControl("txtPrevConfNo"), TextBox)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<vlineno>" & lblVisaLineNo.Text & "</vlineno>")
                    strbuffer.Append("<visaconfno>" & txtvisaconfno.Text & "</visaconfno>")
                    strbuffer.Append("<confirmby>" & Session("GlobalUserName") & "</confirmby>")
                    strbuffer.Append("<confirmdate>" & Format(CType(txtconfirmDate.Text, Date), "yyyy/MM/dd") & "</confirmdate>")
                    If txtvisaconfno.Text <> hdnPrevConfNo.Value And hdnPrevConfNo.Value <> "" Then
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

                If objBLLguest.SavingVisaConfirmBookingDetailsInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpVisaConfirm.Show()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnAirportMateConfirmSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnOthersConfirmSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOthersConfirmSave.Click
        Try

            If ValidateOthersConfirmBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvOthersConfirm.Rows
                    Dim lblRowNo As Label = CType(gvrow.FindControl("lblRowNo"), Label)
                    Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
                    Dim txtothersconfno As TextBox = CType(gvrow.FindControl("txtothersconfno"), TextBox)
                    Dim txtCancelDays As TextBox = CType(gvrow.FindControl("txtCancelDays"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
                    Dim hdnPrevConfNo As HiddenField = CType(gvrow.FindControl("hdnPrevConfNo"), HiddenField)
                    Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)
                    'Modified by abin on 20181223
                    If txtTimeLimitTime.Value = "" Or txtTimeLimitTime.Value = "0" Then
                        txtTimeLimitTime.Value = "00:00"
                    End If
                    Dim txtconfirmDate As TextBox = CType(gvrow.FindControl("txtconfirmdate"), TextBox)
                    Dim txtPrevConfNo As TextBox = CType(gvrow.FindControl("txtPrevConfNo"), TextBox)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<olineno>" & lblOthersLineNo.Text & "</olineno>")
                    strbuffer.Append("<othersconfno>" & txtothersconfno.Text & "</othersconfno>")
                    strbuffer.Append("<confirmby>" & Session("GlobalUserName") & "</confirmby>")
                    strbuffer.Append("<confirmdate>" & Format(CType(txtconfirmDate.Text, Date), "yyyy/MM/dd") & "</confirmdate>")
                    If txtothersconfno.Text <> hdnPrevConfNo.Value And hdnPrevConfNo.Value <> "" Then
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

                If objBLLguest.SavingOthersConfirmBookingDetailsInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpOthersConfirm.Show()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnAirportMateConfirmSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnAirportMateConfirmSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAirportMateConfirmSave.Click
        Try

            If ValidateAirportmateConfirmBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvAirportMateConfirm.Rows
                    Dim lblRowNo As Label = CType(gvrow.FindControl("lblRowNo"), Label)
                    Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
                    Dim txtAirportmateconfno As TextBox = CType(gvrow.FindControl("txtAirportmateconfno"), TextBox)
                    Dim txtCancelDays As TextBox = CType(gvrow.FindControl("txtCancelDays"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
                    Dim hdnPrevConfNo As HiddenField = CType(gvrow.FindControl("hdnPrevConfNo"), HiddenField)
                    Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)
                    'Modified by abin on 20181223
                    If txtTimeLimitTime.Value = "" Or txtTimeLimitTime.Value = "0" Then
                        txtTimeLimitTime.Value = "00:00"
                    End If
                    Dim txtconfirmDate As TextBox = CType(gvrow.FindControl("txtconfirmdate"), TextBox)
                    Dim txtPrevConfNo As TextBox = CType(gvrow.FindControl("txtPrevConfNo"), TextBox)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<alineno>" & lblAirportMateLineNo.Text & "</alineno>")
                    strbuffer.Append("<airportmateconfno>" & txtAirportmateconfno.Text & "</airportmateconfno>")
                    strbuffer.Append("<confirmby>" & Session("GlobalUserName") & "</confirmby>")
                    strbuffer.Append("<confirmdate>" & Format(CType(txtconfirmDate.Text, Date), "yyyy/MM/dd") & "</confirmdate>")
                    If txtAirportmateconfno.Text <> hdnPrevConfNo.Value And hdnPrevConfNo.Value <> "" Then
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

                If objBLLguest.SavingAirportMateConfirmBookingDetailsInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpAirportMateConfirm.Show()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnAirportMateConfirmSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnToursConfirmSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToursConfirmSave.Click
        Try

            If ValidateToursConfirmBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvToursConfirm.Rows
                    Dim lblRowNo As Label = CType(gvrow.FindControl("lblRowNo"), Label)
                    Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
                    Dim txtToursconfno As TextBox = CType(gvrow.FindControl("txtToursconfno"), TextBox)
                    Dim txtCancelDays As TextBox = CType(gvrow.FindControl("txtCancelDays"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
                    Dim hdnPrevConfNo As HiddenField = CType(gvrow.FindControl("hdnPrevConfNo"), HiddenField)
                    Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)
                    'Modified by abin on 20181223
                    If txtTimeLimitTime.Value = "" Or txtTimeLimitTime.Value = "0" Then
                        txtTimeLimitTime.Value = "00:00"
                    End If

                    If txtTimeLimitDate.Text = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Plase enter time limit date.")
                        Exit Sub
                    End If
                    Dim txtconfirmDate As TextBox = CType(gvrow.FindControl("txtconfirmdate"), TextBox)
                    Dim txtPrevConfNo As TextBox = CType(gvrow.FindControl("txtPrevConfNo"), TextBox)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<elineno>" & lblToursLineNo.Text & "</elineno>")
                    strbuffer.Append("<toursconfno>" & txtToursconfno.Text & "</toursconfno>")
                    strbuffer.Append("<confirmby>" & Session("GlobalUserName") & "</confirmby>")
                    strbuffer.Append("<confirmdate>" & Format(CType(txtconfirmDate.Text, Date), "yyyy/MM/dd") & "</confirmdate>")
                    If txtToursconfno.Text <> hdnPrevConfNo.Value And hdnPrevConfNo.Value <> "" Then
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

                If objBLLguest.SavingToursConfirmBookingDetailsInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpToursConfirm.Show()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnsaveconfirm_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnTransferConfirmSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransferConfirmSave.Click
        Try

            If ValidateTrfConfirmBookingDetails() Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvTransferConfirm.Rows
                    Dim lblRowNo As Label = CType(gvrow.FindControl("lblRowNo"), Label)
                    Dim lblleadguest As Label = CType(gvrow.FindControl("lblleadguest"), Label)
                    Dim txttransferconfno As TextBox = CType(gvrow.FindControl("txttransferconfno"), TextBox)
                    Dim txtCancelDays As TextBox = CType(gvrow.FindControl("txtCancelDays"), TextBox)
                    Dim txtTimeLimitDate As TextBox = CType(gvrow.FindControl("txtTimeLimitDate"), TextBox)
                    Dim hdnPrevConfNo As HiddenField = CType(gvrow.FindControl("hdnPrevConfNo"), HiddenField)
                    Dim txtTimeLimitTime As HtmlInputText = CType(gvrow.FindControl("txtTimeLimitTime"), HtmlInputText)
                    'Modified by abin on 20181223
                    If txtTimeLimitTime.Value = "" Or txtTimeLimitTime.Value = "0" Then
                        txtTimeLimitTime.Value = "00:00"
                    End If
                    Dim txtconfirmDate As TextBox = CType(gvrow.FindControl("txtconfirmdate"), TextBox)
                    Dim txtPrevConfNo As TextBox = CType(gvrow.FindControl("txtPrevConfNo"), TextBox)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<tlineno>" & lblTrfLineNo.Text & "</tlineno>")
                    strbuffer.Append("<transferconfno>" & txttransferconfno.Text & "</transferconfno>")
                    strbuffer.Append("<confirmby>" & Session("GlobalUserName") & "</confirmby>")
                    strbuffer.Append("<confirmdate>" & Format(CType(txtconfirmDate.Text, Date), "yyyy/MM/dd") & "</confirmdate>")
                    If txttransferconfno.Text <> hdnPrevConfNo.Value And hdnPrevConfNo.Value <> "" Then
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

                If objBLLguest.SavingTransferConfirmBookingDetailsInTemp() Then
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpTransferConfirm.Show()
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnsaveconfirm_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub chkApplySameConf_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkApplySameConf.CheckedChanged

    End Sub

    Protected Sub btnApplySameConfChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplySameConfChk.Click
        Try


            mpConfirm.Show()

            If chkApplySameConf.Checked Then
                If gvConfirmBooking.Rows.Count > 0 Then

                    Dim dvhotelconfno As HtmlGenericControl = CType(gvConfirmBooking.Rows(0).FindControl("dvhotelconfno"), HtmlGenericControl)


                    Dim txthotelconfno As TextBox = CType(dvhotelconfno.FindControl("txthotelconfno"), TextBox)

                    If txthotelconfno.Text <> "" Then

                        For Each gvrow As GridViewRow In gvConfirmBooking.Rows


                            Dim dvhotelconfnonew As HtmlGenericControl = CType(gvrow.FindControl("dvhotelconfno"), HtmlGenericControl)

                            Dim txthotelconfnonew As TextBox = CType(dvhotelconfnonew.FindControl("txthotelconfno"), TextBox)
                            If (txthotelconfnonew.Text.Trim = "") Then
                                txthotelconfnonew.Text = txthotelconfno.Text
                                chkApplySameConf.Checked = False
                            End If


                        Next
                    Else

                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Confirmation No. in First Row...")
                        chkApplySameConf.Checked = False
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnApplySameConfChk_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Function ValidateRemarksPopup(ByVal txtagentremarks As TextBox, ByVal txtpartyremarks As TextBox, ByVal txtarrremarks As TextBox, ByVal txtdeptremarks As TextBox, ByVal type As String) As Boolean
        If txtagentremarks.Text.Trim = "" And txtpartyremarks.Text.Trim = "" And txtarrremarks.Text.Trim = "" And txtdeptremarks.Text.Trim = "" Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Fill atleast one  RemarksType")
            txtToursPartyRemarks.Focus()
            If type = "OTH" Then
                MPOthServRemarks.Show()
            ElseIf type = "TRFS" Then
                MPTransfersRemarks.Show()
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




    Private Sub FillVisaRemarksPopUp(ByVal sender)
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


    Protected Sub ImgAirportmaRemarks_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not Session("sRequestId") Is Nothing Then ' modified by abin on 20180724

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
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try


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
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Private Sub clearvisacontrols()

        txtVisaAgentRemarks.Text = ""
        txtvisaarrremarks.Text = ""
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
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnOthSaveRemarks_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
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
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnToursSaveRemarks_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnTrfsSaveRemarks_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnAirSaveRemarks_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
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
                MessageBox.ShowMessage(Page, MessageType.Warning, "Saved Sucessfully...")
            End If
            ''   End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnVisaSaveRemarks_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
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
            objclsUtilities.WriteErrorLog("MoreService.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Sub clearallBookingSessions()

        Session("sRequestId") = Nothing
        Session("sEditRequestId") = Nothing
        Session("sdtPriceBreakup") = Nothing
        Session("showservices") = Nothing
        Session("ShowGuests") = Nothing
        Session("ShowGuestsDep") = Nothing
        Session("sobjBLLHotelSearch") = Nothing
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
        Session("sdsPackageSummary") = Nothing
        Session("VisaDetailsDT") = Nothing
        Session("vlineno") = Nothing
    End Sub
    Protected Sub btnAbondon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbondon.Click
        Dim strPage As String = ""
        If Not Session("sEditRequestId") Is Nothing Then
            strPage = "~/MyAccount.aspx"
        Else
            strPage = "Home.aspx?Tab=0"
        End If

        clearallBookingSessions()
        Response.Redirect(strPage)
    End Sub

    Protected Sub btnSubmitQuote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitQuote.Click


        Try
            If chkTermsAndConditions.Checked = False Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please tick Terms and Conditions")
                Exit Sub
            End If



            Dim dt As DataTable
            Dim requestid As String = ""
            Dim divcode As String = "", agentcode As String = "", sourcectry As String = ""
            requestid = GetExistingRequestId()
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)

            If dt.Rows.Count > 0 Then
                divcode = dt.Rows(0)("div_code").ToString
                agentcode = dt.Rows(0)("agentcode").ToString
                sourcectry = dt.Rows(0)("sourcectrycode").ToString
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, "There is no Service to be Booked Please Select ")
                Exit Sub

            End If


            'Added by abin on 20191110
            If requestid.Contains("QG") Or requestid.Contains("QP") Then
                Dim dtVal As DataTable
                dtVal = objclsUtilities.GetDataFromDataTable("exec sp_validate_booking_quotation_edit_final  '" + requestid + "'")
                Dim strMsg As String = ""
                If dtVal.Rows.Count > 0 Then
                    For i As Integer = 0 To dtVal.Rows.Count - 1
                        strMsg = strMsg & dtVal.Rows(i)("warning").ToString & "</br>"
                    Next
                    strMsg = strMsg & " So this quote cannot be genereate."
                    MessageBox.ShowMessage(Page, MessageType.Warning, strMsg)
                    Exit Sub
                End If
            End If


            Dim strrequestid As String = ""
            Dim ds As DataSet = objBLLguest.ValidateBooking(requestid, agentcode, sourcectry, Session("sLoginType"), 1)

            If ds.Tables(2).Rows.Count > 0 Then
                Dim strMessage As String = ""
                For i As Integer = 0 To ds.Tables(2).Rows.Count - 1
                    strMessage = strMessage & "\n " & ds.Tables(2).Rows(i)("errmsg").ToString
                Next
                If strMessage <> "" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, strMessage.Replace("\n", "</br>"))
                    Exit Sub
                End If
            End If
            If ds.Tables(0).Rows.Count > 0 Or ds.Tables(1).Rows.Count > 0 Then


                Dim strMessage As String = ""
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    strMessage = strMessage & "\n " & i + 1 & "." & ds.Tables(1).Rows(i)("errmsg").ToString
                Next
                If strMessage.Contains("cannot proceed") Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, strMessage.Replace("\n", "</br>"))
                    Exit Sub
                End If
                '   MessageBox.ShowMessage(Page, MessageType.Warning, strMessage)

                strMessage = strMessage & "\n\n  " & "Do you want to continue To Save Quote? "
                Dim scriptKey As String = "UniqueKeyForThisScript2"
                Dim javaScript As String = "<script type='text/javascript'>confirmsaveDiscountPackage('" + strMessage + "');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                Exit Sub

                '' '' Errors to list
                'If ds.Tables(0).Rows.Count > 0 Then
                '    btnproceed.Style.Add("display", "none")
                '    gdErrorlist.DataSource = ds.Tables(0)
                '    gdErrorlist.DataBind()
                '    lblErrorlist.Text = "Booking Errors"

                'ElseIf ds.Tables(0).Rows.Count = 0 And ds.Tables(1).Rows.Count > 0 Then


                '    gdErrorlist.DataSource = ds.Tables(1)
                '    gdErrorlist.DataBind()
                '    lblErrorlist.Text = "Booking Warnings"
                '    btnproceed.Style.Add("display", "block")

                'End If

                'mpBookingError.Show()
            Else
                If objBLLguest.PrearrangedValidate(Session("sRequestId")) = 1 Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Can not book pre-arranged hotel only.")
                    Exit Sub
                End If
                final_save(requestid, divcode)
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnSubmitQuote_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnbacktohome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbacktohome.Click
        clearallBookingSessions()
        Response.Redirect("~\Home.aspx?Tab=0")
    End Sub
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub




    Protected Sub btnprintquote_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprintquote.Click
        Try
            Dim strpop As String

            If hdQuoteReqestId.Value <> "" Then
                Dim quoteid As String = hdQuoteReqestId.Value.Trim

                If quoteid.Contains(",") Then
                    Dim str As String() = quoteid.Split(",")
                    quoteid = str(0)
                End If

                Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + quoteid + "'")
                If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
                If chkCumulative.Trim() = "CUMULATIVE" And Session("sLoginType") = "RO" Then
                    strpop = "window.open('PrintPage.aspx?printId=QuoteCostingExcel&quoteId=" & quoteid.Trim & "');"
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupQCostingExcel", strpop, True)
                    'Dim quoteid As String = hdQuoteReqestId.Value.Trim
                    ''Dim FolderPath As String = "..\ExcelTemplates\"
                    ''Dim FileName As String = "QuoteCosting.xlsx"
                    ''Dim FilePath As String = Server.MapPath("~\ExcelTemplates\") + FileName
                    ''Dim RandomCls As New Random()
                    ''Dim RandomNo As String = RandomCls.Next(100000, 9999999).ToString
                    ''objclsquotecosting.GenerateExcelReport(quoteid, FilePath)
                End If

                ' Dim strpop As Stringl
                Dim strpopName As String = "popup" + quoteid.Replace("/", "").ToString()
                strpop = "window.open('PrintPage.aspx?printID=bookingQuote&quoteId=" & quoteid.Trim & "');"

                'Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
                'If page IsNot Nothing Then
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), strpopName, strpop, True)
                ' End If
                '    strpop = "window.open('PrintPage.aspx?printId=QuoteItinerary&quoteId=" & quoteid.Trim & "');"
                '    ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "popupQItinerary", strpop, True)
            Else
                btnGeneratePackageValue.Visible = False
                dvSummaryPart.Visible = False
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: printQuote :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub



    Protected Sub gvHotelCancel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHotelCancel.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnHotelCancel As HiddenField = CType(e.Row.FindControl("hdnHotelCancel"), HiddenField)
            Dim chkHotelCancel As CheckBox = CType(e.Row.FindControl("chkHotelCancel"), CheckBox)
            If hdnHotelCancel.Value = "1" Then
                chkHotelCancel.Checked = True
            Else
                chkHotelCancel.Checked = False

            End If

        End If
    End Sub

    Protected Sub gvTransferCancel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTransferCancel.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnTrfCancel As HiddenField = CType(e.Row.FindControl("hdnTrfCancel"), HiddenField)
            Dim chkTrfCancel As CheckBox = CType(e.Row.FindControl("chkTrfCancel"), CheckBox)
            If hdnTrfCancel.Value = "1" Then
                chkTrfCancel.Checked = True
            Else
                chkTrfCancel.Checked = False

            End If
        End If
    End Sub

    Function ValidateCancelTransferDetails() As Boolean
        For Each gvrow As GridViewRow In gvTransferCancel.Rows
            Dim txtTrfcancelno As TextBox = CType(gvrow.FindControl("txtTrfcancelno"), TextBox)
            Dim txttrfCancelDate As TextBox = CType(gvrow.FindControl("txttrfCancelDate"), TextBox)


            If txtTrfcancelno.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter cancellation No.")
                txtTrfcancelno.Focus()
                ValidateCancelTransferDetails = False
                Exit Function
            End If
            If txttrfCancelDate.Text = "DD/MM/YYYY" Or txttrfCancelDate.Text = "" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Select Cancel Date.")
                txttrfCancelDate.Focus()
                ValidateCancelTransferDetails = False
                Exit Function
            End If


        Next
        Return True
    End Function
    Protected Sub btntransferCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntransferCancelSave.Click
        Try
            Dim objBLLTransferSearch As New BLLTransferSearch
            If ValidateTransferCancelDetails() Then

                objBLLTransferSearch.OBRequestId = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvTransferCancel.Rows
                    Dim lbtLineNo As Label = CType(gvrow.FindControl("lbtLineNo"), Label)


                    Dim txtTrfcancelno As TextBox = CType(gvrow.FindControl("txtTrfcancelno"), TextBox)
                    Dim chkTrfCancel As CheckBox = CType(gvrow.FindControl("chkTrfCancel"), CheckBox)
                    Dim txttrfCancelDate As TextBox = CType(gvrow.FindControl("txttrfCancelDate"), TextBox)
                    Dim lbltrfCancelBy As Label = CType(gvrow.FindControl("lbltrfCancelBy"), Label)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<tlineno>" & lbtLineNo.Text & "</tlineno>")
                    If chkTrfCancel.Checked = True Then
                        strbuffer.Append("<cancelled>1</cancelled>")
                    Else
                        strbuffer.Append("<cancelled>0</cancelled>")
                    End If
                    strbuffer.Append("<transfercancelno>" & txtTrfcancelno.Text & "</transfercancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(txttrfCancelDate.Text, Date), "yyyy/MM/dd") & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLTransferSearch.OBCancelTransferXml = strbuffer.ToString
                objBLLTransferSearch.UserLogged = Session("GlobalUserName")

                If objBLLTransferSearch.SavingCancelTransferDetailsInTemp() Then
                    BindTransferSummary()
                    BindTotalValue()
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpTransferCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnTransferCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub gvVisaCancel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVisaCancel.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnVisaCancel As HiddenField = CType(e.Row.FindControl("hdnVisaCancel"), HiddenField)
            Dim chkVisaCancel As CheckBox = CType(e.Row.FindControl("chkVisaCancel"), CheckBox)
            If hdnVisaCancel.Value = "1" Then
                chkVisaCancel.Checked = True
            Else
                chkVisaCancel.Checked = False

            End If
        End If
    End Sub

    Protected Sub btnvisaCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnvisaCancelSave.Click
        Try
            Dim objBLLVISA As New BLLVISA
            If ValidateVisaBookingDetails() Then

                objBLLVISA.VBRequestid = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvVisaCancel.Rows
                    Dim lbvLineNo As Label = CType(gvrow.FindControl("lbvLineNo"), Label)


                    Dim txtvisacancelno As TextBox = CType(gvrow.FindControl("txtvisacancelno"), TextBox)
                    Dim chkvisaCancel As CheckBox = CType(gvrow.FindControl("chkvisaCancel"), CheckBox)
                    Dim txtvisaCancelDate As TextBox = CType(gvrow.FindControl("txtvisaCancelDate"), TextBox)
                    Dim lblvisaCancelBy As Label = CType(gvrow.FindControl("lblvisaCancelBy"), Label)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<vlineno>" & lbvLineNo.Text & "</vlineno>")
                    If chkvisaCancel.Checked = True Then
                        strbuffer.Append("<cancelled>1</cancelled>")
                    Else
                        strbuffer.Append("<cancelled>0</cancelled>")
                    End If
                    strbuffer.Append("<visacancelno>" & txtvisacancelno.Text & "</visacancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(txtvisaCancelDate.Text, Date), "yyyy/MM/dd") & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLVISA.VBCancelVisaXml = strbuffer.ToString
                objBLLVISA.VBuserlogged = Session("GlobalUserName")

                If objBLLVISA.SavingCancelVisaInTemp() Then
                    BindVisaSummary()
                    BindTotalValue()
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpVisaCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnvisaCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub gvtourCancel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvtourCancel.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnTourCancel As HiddenField = CType(e.Row.FindControl("hdntourCancel"), HiddenField)
            Dim chktourCancel As CheckBox = CType(e.Row.FindControl("chktourCancel"), CheckBox)
            If hdnTourCancel.Value = "1" Then
                chktourCancel.Checked = True
            Else
                chktourCancel.Checked = False

            End If
        End If
    End Sub

    Protected Sub btntourCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntourCancelSave.Click
        Try
            Dim objBLLTourSearch As New BLLTourSearch
            If ValidateTourCancelDetails() Then

                objBLLTourSearch.EbRequestID = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvtourCancel.Rows
                    Dim lbeLineNo As Label = CType(gvrow.FindControl("lbeLineNo"), Label)


                    Dim txttourcancelno As TextBox = CType(gvrow.FindControl("txttourcancelno"), TextBox)
                    Dim chktourCancel As CheckBox = CType(gvrow.FindControl("chktourCancel"), CheckBox)
                    Dim txttourCancelDate As TextBox = CType(gvrow.FindControl("txttourCancelDate"), TextBox)
                    Dim lbltourCancelBy As Label = CType(gvrow.FindControl("lbltourCancelBy"), Label)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<elineno>" & lbeLineNo.Text & "</elineno>")
                    If chktourCancel.Checked = True Then
                        strbuffer.Append("<cancelled>1</cancelled>")
                    Else
                        strbuffer.Append("<cancelled>0</cancelled>")
                    End If
                    strbuffer.Append("<tourscancelno>" & txttourcancelno.Text & "</tourscancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(txttourCancelDate.Text, Date), "yyyy/MM/dd") & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLTourSearch.EBCancelToursXml = strbuffer.ToString
                objBLLTourSearch.EBuserlogged = Session("GlobalUserName")

                If objBLLTourSearch.SavingCancelTourInTemp() Then
                    BindTourSummary()
                    BindTotalValue()
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mptourCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btntourCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnairCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnairCancelSave.Click
        Try
            Dim objBLLMASearch As New BLLMASearch
            If ValidateAirportCancelDetails() Then

                objBLLMASearch.OBRequestId = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvairCancel.Rows
                    Dim lbaLineNo As Label = CType(gvrow.FindControl("lbaLineNo"), Label)


                    Dim txtaircancelno As TextBox = CType(gvrow.FindControl("txtaircancelno"), TextBox)
                    Dim chkairCancel As CheckBox = CType(gvrow.FindControl("chkairCancel"), CheckBox)
                    Dim txtaircancelDate As TextBox = CType(gvrow.FindControl("txtaircancelDate"), TextBox)
                    Dim lblairCancelBy As Label = CType(gvrow.FindControl("lblairCancelBy"), Label)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<alineno>" & lbaLineNo.Text & "</alineno>")
                    If chkairCancel.Checked = True Then
                        strbuffer.Append("<cancelled>1</cancelled>")
                    Else
                        strbuffer.Append("<cancelled>0</cancelled>")
                    End If
                    strbuffer.Append("<airportmatecancelno>" & txtaircancelno.Text & "</airportmatecancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(txtaircancelDate.Text, Date), "yyyy/MM/dd") & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLMASearch.OBCancelAirXml = strbuffer.ToString
                objBLLMASearch.UserLogged = Session("GlobalUserName")

                If objBLLMASearch.SavingCancelAirportInTemp() Then
                    BindAirportserviceSummary()
                    BindTotalValue()
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpairCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnairCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnotherCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnotherCancelSave.Click
        Try
            Dim objBLLOtherSearch As New BLLOtherSearch
            If ValidateOtherCancelDetails() Then

                objBLLOtherSearch.OBRequestId = GetExistingRequestId()
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvotherCancel.Rows
                    Dim lboLineNo As Label = CType(gvrow.FindControl("lboLineNo"), Label)


                    Dim txtotherscancelno As TextBox = CType(gvrow.FindControl("txtotherscancelno"), TextBox)
                    Dim chkothCancel As CheckBox = CType(gvrow.FindControl("chkothCancel"), CheckBox)
                    Dim txtothcancelDate As TextBox = CType(gvrow.FindControl("txtothcancelDate"), TextBox)
                    Dim lblothCancelBy As Label = CType(gvrow.FindControl("lblothCancelBy"), Label)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<olineno>" & lboLineNo.Text & "</olineno>")
                    If chkothCancel.Checked = True Then
                        strbuffer.Append("<cancelled>1</cancelled>")
                    Else
                        strbuffer.Append("<cancelled>0</cancelled>")
                    End If
                    strbuffer.Append("<otherscancelno>" & txtotherscancelno.Text & "</otherscancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(txtothcancelDate.Text, Date), "yyyy/MM/dd") & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLOtherSearch.OBOtherCancelXml = strbuffer.ToString
                objBLLOtherSearch.UserLogged = Session("GlobalUserName")

                If objBLLOtherSearch.SavingCancelOtherInTemp() Then
                    BindOtherserviceSummary()
                    BindTotalValue()
                    MessageBox.ShowMessage(Page, MessageType.Success, "Saved Sucessfully...")

                End If
            Else
                mpotherCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnotherCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub gvairCancel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvairCancel.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnairCancel As HiddenField = CType(e.Row.FindControl("hdnairCancel"), HiddenField)
            Dim chkairCancel As CheckBox = CType(e.Row.FindControl("chkairCancel"), CheckBox)
            If hdnairCancel.Value = "1" Then
                chkairCancel.Checked = True
            Else
                chkairCancel.Checked = False

            End If
        End If
    End Sub
    Protected Sub gvotherCancel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvotherCancel.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim hdnothCancel As HiddenField = CType(e.Row.FindControl("hdnothCancel"), HiddenField)
            Dim chkothCancel As CheckBox = CType(e.Row.FindControl("chkothCancel"), CheckBox)
            If hdnothCancel.Value = "1" Then
                chkothCancel.Checked = True
            Else
                chkothCancel.Checked = False

            End If
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

        If hdBookingEngineRateType.Value = "1" Then
            dvRoomValue.Visible = False
        End If
    End Sub

    Private Sub HideTabTotalPrice()
        If hdBookingEngineRateType.Value = "1" Then
            lblTabHotelTotalPriceText.Visible = False
            lblTourTabTotalPrice1.Visible = False
            lblAirportTabtotalPrice1.Visible = False
            lblTransferTabTotalPrice1.Visible = False
            lblVisaTabTotalPrice1.Visible = False
            lblOtherServiceTabTotalPrice1.Visible = False
        End If
    End Sub

    Protected Sub btnGeneratePackageValue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeneratePackageValue.Click
        Try



            If Not Session("sRequestId") Is Nothing Then
                Dim ds As DataSet
                Dim objBLLGuest As New BLLGuest



                ds = objBLLGuest.GeneratePackageValue(Session("sRequestId"), "0", Session("sLoginType"))
                dvPackageDetails.Style.Add("display", "none")
                If ds.Tables.Count > 0 Then
                    Session("sdsPackageSummary") = ds
                    If ds.Tables(0).Rows.Count > 0 Then
                        '  dvPackageDetails.Visible = False
                        Dim strMessage As String = ""
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            strMessage = strMessage & "\n " & ds.Tables(0).Rows(i)("errdesc").ToString
                        Next
                        If strMessage.Contains("cannot proceed") Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, strMessage.Replace("\n", "</br>"))
                            Exit Sub
                        End If
                        '   MessageBox.ShowMessage(Page, MessageType.Warning, strMessage)
                        strMessage = strMessage & "\n\n  " & "Do you want to continue without discount? "
                        Dim scriptKey As String = "UniqueKeyForThisScript3"
                        Dim javaScript As String = "<script type='text/javascript'>confirmDiscountPackage('" + strMessage + "');</script>"
                        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
                        Exit Sub
                    Else
                        If ds.Tables(2).Rows.Count > 0 Then
                            If Val(ds.Tables(2).Rows(0)("totalrevisedmarkup").ToString) > 0 Then
                                UpdateRevisedMarkup() 'added by abin on 20190116
                            End If
                            FillPackageFields(ds.Tables(2))
                        End If
                    End If


                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnGeneratePackageValue_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Private Function fnRound(strValue As String, iRound As Integer) As String
        strValue = Math.Round(Val(strValue), iRound)
        Return strValue
    End Function
    Protected Sub lblPFormulaId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPFormulaId.Click
        If Session("sLoginType") = "RO" Then

            If Not Session("sdsPackageSummary") Is Nothing Then
                Dim ds As DataSet
                ds = Session("sdsPackageSummary")
                If ds.Tables(4).Rows.Count > 0 Then
                    gvFormulaROSummary.DataSource = ds.Tables(4)
                    gvFormulaROSummary.DataBind()
                    mpFormulaROSummary.Show()

                    txtadultvisa.Text = ds.Tables(3).Rows(0)("adultwithvisa").ToString
                    txtAdultwovisa.Text = ds.Tables(3).Rows(0)("adultwithoutvisa").ToString
                    txtChildwovisa.Text = ds.Tables(3).Rows(0)("childwithoutvisa").ToString
                    txtChildwvisa.Text = ds.Tables(3).Rows(0)("childwithvisa").ToString

                    txtChildfreewovisa.Text = ds.Tables(3).Rows(0)("childfreewithoutvisa").ToString
                    txtChildfreewvisa.Text = ds.Tables(3).Rows(0)("childfreewithvisa").ToString
                    txtchildfree.Text = ds.Tables(3).Rows(0)("childfreeupto").ToString

                    'Dim lblSaleValueHeader As Label = CType(gvFormulaROSummary.HeaderRow.FindControl("lblSaleValueHeader"), Label)
                    'lblSaleValueHeader.Text = "Sale Value (" & ds.Tables(1).Rows(0)("salecurrcode").ToString & ")"
                    'Dim lblSaleValueBaseHeader As Label = CType(gvFormulaROSummary.HeaderRow.FindControl("lblSaleValueBaseHeader"), Label)
                    'lblSaleValueBaseHeader.Text = "Sale Value Base (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"
                    'Dim lblCostValueHeader As Label = CType(gvFormulaROSummary.HeaderRow.FindControl("lblCostValueHeader"), Label)
                    'lblCostValueHeader.Text = "Cost Value (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"
                End If
            End If

        End If
    End Sub

    Protected Sub lblPNetprofitText_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPNetprofitText.Click
        If Session("sLoginType") = "RO" Then

            If Not Session("sdsPackageSummary") Is Nothing Then
                Dim ds As DataSet
                ds = Session("sdsPackageSummary")

                gvDiscountROSummary.DataSource = ds.Tables(5)
                gvDiscountROSummary.DataBind()
                mpDiscountROSummary.Show()

            End If


        End If

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
    ' added by abin on 20190116
    Private Sub UpdateRevisedMarkup()
        If Session("sLoginType") = "RO" Then

            If Not Session("sdsPackageSummary") Is Nothing Then
                Dim ds As DataSet
                ds = Session("sdsPackageSummary")

                gvDiscountROSummary.DataSource = ds.Tables(5)
                gvDiscountROSummary.DataBind()



                Dim netprofit As Double = 0
                Dim adultmarkup As Double = 0, childmarkup As Double = 0, childfreemarkup As Double = 0
                'Dim ds As DataSet
                'ds = Session("sdsPackageSummary")

                For Each GvRow1 As GridViewRow In gvDiscountROSummary.Rows
                    Dim txtsystemmarkup As TextBox = CType(GvRow1.FindControl("txtsystemmarkup"), TextBox)
                    Dim lblnoofpax As Label = CType(GvRow1.FindControl("lblpax"), Label)
                    Dim lblnetprofit As Label = CType(GvRow1.FindControl("lblnetprofit"), Label)
                    Dim lblpaxtype As Label = CType(GvRow1.FindControl("lblpaxtype"), Label)

                    adultmarkup = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                    childmarkup = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                    childfreemarkup = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))

                    netprofit = Val(netprofit) + (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))

                    If ds.Tables(2).Rows.Count > 0 Then
                        Dim dr As DataRow = ds.Tables(2).Select().First
                        Dim dr1 As DataRow
                        If lblpaxtype.Text = "Adult" And lblnoofpax.Text > 0 Then
                            dr("adultrevisedmarkup") = IIf(Val(txtsystemmarkup.Text) = 0, "0.00", Val(txtsystemmarkup.Text))

                            If ds.Tables(5).Rows.Count > 0 Then
                                dr1 = ds.Tables(5).Select("paxtype='" & lblpaxtype.Text & "'").First

                                dr1("systemmarkup") = Val(txtsystemmarkup.Text)
                                dr1("revisedmarkup") = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                            End If
                        End If
                        If lblpaxtype.Text = "Child" And lblnoofpax.Text > 0 Then
                            dr("childrevisedmarkup") = IIf(Val(txtsystemmarkup.Text) = 0, "0.00", Val(txtsystemmarkup.Text))

                            If ds.Tables(5).Rows.Count > 0 Then
                                dr1 = ds.Tables(5).Select("paxtype='" & lblpaxtype.Text & "'").First

                                dr1("systemmarkup") = Val(txtsystemmarkup.Text)
                                dr1("revisedmarkup") = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                            End If
                        End If
                        If lblpaxtype.Text = "ChildFree" And lblnoofpax.Text > 0 Then
                            dr("childfreeuptorevisedmarkup") = IIf(Val(txtsystemmarkup.Text) = 0, "0.00", Val(txtsystemmarkup.Text))
                            If ds.Tables(5).Rows.Count > 0 Then
                                dr1 = ds.Tables(5).Select("paxtype='" & lblpaxtype.Text & "'").First
                                dr1("systemmarkup") = Val(txtsystemmarkup.Text)
                                dr1("revisedmarkup") = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                            End If
                        End If
                    End If
                Next

                If ds.Tables(2).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(2).Select().First
                    dr("totalrevisedmarkup") = IIf(Val(netprofit) = 0, "0.00", Val(netprofit))
                    dr("totalreviseddiscount") = Val(dr("totalmarkupcurr")) - Val(dr("totalrevisedmarkup"))
                End If

                Session("sdsPackageSummary") = ds

            End If
        End If
    End Sub

    Protected Sub btnCheckVAT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckVAT.Click
        If Session("sLoginType") = "RO" Then
            Dim ds As DataSet
            If Not Session("sdsPackageSummary") Is Nothing Then
                ds = Session("sdsPackageSummary")
            Else
                Dim objBLLGuest As New BLLGuest
                ds = objBLLGuest.GeneratePackageValue(Session("sRequestId"), "1", Session("sLoginType"))
            End If
            If ds IsNot Nothing Then
                If ds.Tables(1).Rows.Count > 0 Then
                    gvCheckVATValues.DataSource = ds.Tables(1)
                    gvCheckVATValues.DataBind()
                    mpCheckVATValues.Show()

                    Dim lblSaleValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblSaleValueHeader"), Label)
                    lblSaleValueHeader.Text = "Sale Value (" & ds.Tables(1).Rows(0)("salecurrcode").ToString & ")"
                    Dim lblSaleValueBaseHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblSaleValueBaseHeader"), Label)
                    lblSaleValueBaseHeader.Text = "Sale Value Base (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"
                    Dim lblCostValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblCostValueHeader"), Label)
                    lblCostValueHeader.Text = "Cost Value (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"

                    Dim lblSaleTaxableValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblSaleTaxableValueHeader"), Label)
                    lblSaleTaxableValueHeader.Text = "Sale Taxable Value (" & ds.Tables(1).Rows(0)("salecurrcode").ToString & ")"

                    Dim lblSaleNonTaxableValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblSaleNonTaxableValueHeader"), Label)
                    lblSaleNonTaxableValueHeader.Text = "Sale NonTaxable Value (" & ds.Tables(1).Rows(0)("salecurrcode").ToString & ")"

                    Dim lblSaleVATValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblSaleVATValueHeader"), Label)
                    lblSaleVATValueHeader.Text = "Sale VAT Value (" & ds.Tables(1).Rows(0)("salecurrcode").ToString & ")"


                    Dim lblCostTaxableValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblCostTaxableValueHeader"), Label)
                    lblCostTaxableValueHeader.Text = "Cost Taxable Value (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"

                    Dim lblCostNonTaxableValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblCostNonTaxableValueHeader"), Label)
                    lblCostNonTaxableValueHeader.Text = "Cost NonTaxable Value (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"

                    Dim lblCostVATValueHeader As Label = CType(gvCheckVATValues.HeaderRow.FindControl("lblCostVATValueHeader"), Label)
                    lblCostVATValueHeader.Text = "Cost VAT Value (" & ds.Tables(1).Rows(0)("BaseCurrency").ToString & ")"

                End If
            End If


        End If
    End Sub


    Protected Sub gvPackageROSummary_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPackageROSummary.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then

        End If
    End Sub

    'dvHotelButtons
    'dvTourButtons
    'dvAMbutton
    'dvTrfBuuton
    'dvVisaButtons
    'dvOSButtons

    Private Sub HideServiceButtons()

        For Each dlItem As DataListItem In dlBookingSummary.Items
            Dim dvHotelButtons As HtmlGenericControl = CType(dlItem.FindControl("dvHotelButtons"), HtmlGenericControl)
            dvHotelButtons.Style.Add("display", "none")
        Next
        For Each dlItem As DataListItem In dlTourSummary.Items
            Dim dvTourButtons As HtmlGenericControl = CType(dlItem.FindControl("dvTourButtons"), HtmlGenericControl)
            Dim divtouredit As HtmlGenericControl = CType(dlItem.FindControl("divtouredit"), HtmlGenericControl)
            dvTourButtons.Style.Add("display", "none")
            divtouredit.Style.Add("display", "none")
        Next
        For Each dlItem As DataListItem In dlAirportSummary.Items
            Dim dvAMbutton As HtmlGenericControl = CType(dlItem.FindControl("dvAMbutton"), HtmlGenericControl)
            dvAMbutton.Style.Add("display", "none")
        Next
        For Each dlItem As DataListItem In dlTransferSummary.Items
            Dim dvTrfBuuton As HtmlGenericControl = CType(dlItem.FindControl("dvTrfBuuton"), HtmlGenericControl)
            dvTrfBuuton.Style.Add("display", "none")
        Next
        For Each dlItem As DataListItem In dlVisaSummary.Items
            Dim dvVisaButtons As HtmlGenericControl = CType(dlItem.FindControl("dvVisaButtons"), HtmlGenericControl)
            dvVisaButtons.Style.Add("display", "none")
        Next
        For Each dlItem As DataListItem In dlOtherSummary.Items
            Dim dvOSButtons As HtmlGenericControl = CType(dlItem.FindControl("dvOSButtons"), HtmlGenericControl)
            dvOSButtons.Style.Add("display", "none")
        Next


    End Sub
    Sub sendemailformat(ByVal emailstatus As String, ByVal requestid As String)
        Try
            Dim bc As clsBookingQuotePdf = New clsBookingQuotePdf()
            Dim objclsUtilities As New clsUtilities

            Dim ds As New DataSet
            Dim dsbreakup As New DataSet
            Dim dscostingpdf As New DataSet '' Added shahul 07/06/18

            Dim strpath1 As String = ""
            Dim bytes As Byte() = {}
            Dim fileName As String = "Quote@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            Dim isSubuser As String
            Dim ResParam As New ReservationParameters
            ResParam = Session("sobjResParam")
            isSubuser = ResParam.IsSubUser
            bc.GenerateCumulativeFormat(requestid, bytes, ds, dsbreakup, dscostingpdf, "SaveServer", ResParam, fileName) '' Added shahul 07/06/18
            strpath1 = Server.MapPath("~\SavedReports\") + fileName
            Session("sobjResParam") = ResParam

            ''' Email Formatting
            ''' 
            Dim hoteldetdt As DataTable = dsbreakup.Tables(1)
            'Dim allservicedt As DataTable = dsbreakup.Tables(5)
            Dim allservicedt As DataTable = dscostingpdf.Tables(2) '' Added shahul 07/06/18
            Dim adchbreakupdt As DataTable = dsbreakup.Tables(8)
            Dim totadchpricedt As DataTable = dsbreakup.Tables(9)

            Dim pktermsdt As DataTable = dsbreakup.Tables(11)
            Dim totadchdt As DataTable = dsbreakup.Tables(0)

            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim othServDt As DataTable = ds.Tables(3)
            Dim airportDt As DataTable = ds.Tables(4)
            Dim visaDt As DataTable = ds.Tables(5)
            Dim tourDt As DataTable = ds.Tables(6)
            Dim OtherDt As DataTable = ds.Tables(7)
            Dim contactDt As DataTable = ds.Tables(8)
            Dim cancelpoldt As DataTable = ds.Tables(13)


            'added param 08/08/2020
            Dim param(3) As SqlParameter
            param(0) = New SqlParameter("@bookingOrQuote", "QUOTE")
            param(1) = New SqlParameter("@bookingType", IIf(hdBookingEngineRateType.Value = "1", "CUMULATIVE", "INDIVIDUAL"))
            param(2) = New SqlParameter("@bookingCreatedBy", CType(Session("sLoginType"), String))
            param(3) = New SqlParameter("@requestId", CType(requestid, String))
            Dim dtEmailConfigure As DataTable = objclsUtilities.GetDataTable("sp_get_emailConfiguration", param)
            Dim roEmailRequired As Boolean = False
            Dim agentEmailRequired As Boolean = False
            Dim quoteCcEmailRequired As Boolean = False
            Dim salesforceEmailId As String = ""
            Dim commonEmailId As String = ""
            If dtEmailConfigure.Rows.Count > 0 Then
                roEmailRequired = CType(dtEmailConfigure(0)("roEmail"), Boolean)
                agentEmailRequired = CType(dtEmailConfigure(0)("agentEmail"), Boolean)
                quoteCcEmailRequired = CType(dtEmailConfigure(0)("CcEmail"), Boolean)
                salesforceEmailId = dtEmailConfigure(0)("salesforceEmail")
                commonEmailId = dtEmailConfigure(0)("commonEmail")
            End If


            Dim strMessage As String = ""
            Dim AgentName As String = "", agentref As String = "", status As String = "", agentcontact As String = "", agentemail As String = "", agentuser As String = ""
            Dim confstatus As String = ""
            Dim clsEmail As New clsEmail
            Dim strQuery As String = ""

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim strfromemailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")

            Dim strCurrCode As String = objclsUtilities.ExecuteQueryReturnStringValue("select currcode from quote_booking_header where requestid='" & requestid & "'")

            Dim to_email As String = ""

            Dim strSubject1 As String = ""
            Dim bookingvalue As String = ""
            Dim divcode As String = ""

            If headerDt.Rows.Count > 0 Then
                AgentName = headerDt.Rows(0)("agentname")
                agentref = headerDt.Rows(0)("agentref")
                agentcontact = headerDt.Rows(0)("agentcontact")
                agentemail = headerDt.Rows(0)("agentemail")
                Dim subuseremail As String = headerDt.Rows(0)("subuseremail")
                If isSubuser = "1" Then
                    agentemail = agentemail + "," + subuseremail.Trim
                End If
                agentuser = headerDt.Rows(0)("webusername")
                divcode = headerDt.Rows(0)("div_code")
                bookingvalue = CType(headerDt.Rows(0)("salevalue"), String)
                If Emailmode = "Test" Then
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE", "QUOTE")
                Else
                    status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE", "QUOTE")
                End If

                If Emailmode = "Test" Then
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE(" & requestid.Trim & ")", "QUOTE(" & requestid & ")")
                Else
                    strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "TEST QUOTE", "TEST QUOTE(" & requestid & ")", "QUOTE(" & requestid & ")")
                End If

            End If

            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                to_email = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                '   strMessage = "Dear " + contactDt.Rows(0)("salesperson") + "," + "&nbsp;<br /><br />"
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                Dim contact As String = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
                If Session("sLoginType") = "RO" Then
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We Cancelled the attached  booking .</span></p>"
                    Else
                        'comented for format strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'>" + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage = "<body style='font-family:Calibri'> <h3><b>Dear Partner ,</b></h3> "

                        If emailstatus = "Amended" Then
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the Attached revised quote as requested by You and   Details of the quote is as follows:</span></p>"
                        Else
                            If status = "Quote" Then

                                strMessage += "<p>Thanks for your e-mail, with regards to your request we are pleased to offer you below proposal for your kind perusal:-</p>"
                                'commented for format  strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote and Details of the quote is as follows:</span></p>"

                            Else

                                strMessage += "<p>Thanks for your e-mail, with regards to your request we are pleased to offer you below proposal for your kind perusal:-</p>"
                                ' strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote  and Details of the quote is as follows:</span></p>"
                            End If

                        End If
                    End If
                Else
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You Received the  Cancelled quote from   <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> .Please check the Attached Quote Ref.</span></p>"
                    Else
                        'commented for format strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage = "<body style='font-family:Calibri'> <h3><b>Dear Partner ,</b></h3> <p>Thanks for your e-mail, with regards to your request we are pleased to offer you below proposal for your kind perusal:-</p>"


                    End If

                End If

            End If
            Dim totadultchild As String
            If totadchdt(0).Item("child").ToString() > 0 Then
                totadultchild = " " + totadchdt(0).Item("adults").ToString() + " Adults & " + totadchdt(0).Item("child").ToString() + " Child (" + totadchdt(0).Item("childages").ToString().Replace(";", ",") + " Yrs )"
            Else
                totadultchild = " " + totadchdt(0).Item("adults").ToString() + " Adults "
            End If

            strMessage += "<h5 ><b><font color='#346978'>Kindly note that the hotel and service rates below are quoted based on Indian nationals based in India</font> </b></h5></b>"

            strMessage += "<hr color='#FF0000'><h5><b><u>Quote based on" + totadultchild + "</u></b></h5>"


            strMessage += "  <table style='border-collapse: collapse;'><tr> <td> <table style='border-collapse: collapse;'> "
            strMessage += "<tr> <td width='246' style='border: 1px solid black;padding-left:10px;padding-top:3px;padding-bottom:3px;padding-right:30px;background-color:#EEE;font-family:Calibri;'><font size='-1'> Quotation Ref :<strong>" + requestid + " </strong></font></td>"
            Dim totalpax As Integer = totadchdt(0).Item("adults") + totadchdt(0).Item("child")

            strMessage += "<td width='395' style='border: 1px solid black;padding-left:10px;padding-top:3px;padding-bottom:3px;padding-right:30px;background-color:#EEE;font-family:Calibri; '><font size='-1'> Passenger Name : <strong>" + AgentName + " +  " + totalpax.ToString() + " Pax </strong></font></td>  </tr>       </table></td>"
            Dim lastpartycode As String = ""

            If hotelDt.Rows.Count > 0 Then
                lastpartycode = hotelDt.Rows(0)("partycode")
                For i = 0 To hotelDt.Rows.Count - 1
                    strMessage += " <!--/******************End of Frist Section :QuotationNo & Passenger Name *************************/-->"
                    strMessage += "<!--/******************Second Section :Hotel Details1*************************/-->"
                    strMessage += "<table style='border-collapse: collapse;'> "

                    If hotelDt.Rows(i)("roomno") = 1 Then ''' Added shahul 19/06/18

                        strMessage += " <tr style='border: 1px solid black;'>"
                        strMessage += "<th scope='col' align='center' style='border:1px solid black;background-color: #c00000;color: #FFF; text-align: center; font-family:Calibri;'><font size='-1.5'> Hotel : <span style='text-transform:uppercase'> </span></font>  " + hotelDt.Rows(i)("partyname") + " </th>"
                        'Dim category() As DataRow = hoteldetdt.Select("hotelname='" + hotelDt.Rows(i)("partyname") + "' ")
                        Dim category() As DataRow = hotelDt.Select("partyname='" + hotelDt.Rows(i)("partyname") + "' ") ''' Added shahul 28/06/18

                        If category.Length > 1 Then
                            strMessage += "<th scope='col' align='center' style='border:1px solid black;background-color: #c00000;color: #FFF; text-align: center; font-family:Calibri;'><font size='-1.5'> Category : <span  Style = 'text-transform:uppercase'>" + category(0).Item("catname") + " </span></font> </th>"
                            strMessage += "<th scope='col' align='center' style='border:1px solid black;background-color: #c00000;color: #FFF; text-align: center; font-family:Calibri;'><font size='-1.5'>Location : <span style='text-transform:vbUpperCase'>" + category(0).Item("destname") + "</span></font> </th>          </tr>"
                        Else
                            strMessage += "<th scope='col' align='center' style='border:1px solid black;background-color: #c00000;color: #FFF; text-align: center; font-family:Calibri;'><font size='-1.5'> Category : <span  Style = 'text-transform:uppercase'>" + category(0).Item("catname") + " </span></font> </th>"
                            strMessage += "<th scope='col' align='center' style='border:1px solid black;background-color: #c00000;color: #FFF; text-align: center; font-family:Calibri;'><font size='-1.5'>Location : <span style='text-transform:vbUpperCase'>" + category(0).Item("destname") + "</span></font> </th>          </tr>"
                        End If

                    End If

                    strMessage += " <tr> <td width='304'style='border: 1px solid black;padding-left:10px;padding-top:3px;padding-bottom:3px;padding-right:10px;font-family:Calibri;'><font size='-1'><strong> " + hotelDt.Rows(i)("roomdetail") + "  </strong></font></td>"
                    strMessage += "<td width='163' align='center' style='border: 1px solid black;padding-left:10px;padding-top:3px;padding-bottom:3px;padding-right:10px;font-family:Calibri;'><font size='-1'><strong> " + Format(CType(hotelDt.Rows(i)("checkin"), Date), "MMM dd yyyy") + "</strong></font></td>"
                    strMessage += "<td  width='174' align='center' style='border: 1px solid black;padding-left:10px;padding-top:3px;padding-bottom:3px;padding-right:10px;font-family:Calibri;'><font size='-1'><strong>" + Format(CType(hotelDt.Rows(i)("checkout"), Date), "MMM dd yyyy") + "</strong></font></td> </tr>"

                    Dim liNeedPolicy As Integer = 0
                    If i = hotelDt.Rows.Count - 1 Then
                        liNeedPolicy = 1
                    Else
                        If hotelDt.Rows(i)("partycode") <> hotelDt.Rows(i + 1)("partycode") Then
                            liNeedPolicy = 1
                        End If
                    End If

                    If liNeedPolicy = 1 Then
                        If cancelpoldt.Rows.Count > 0 Then
                            ' Dim dv As New DataView(cancelpoldt)
                            Dim dv As DataView = cancelpoldt.DefaultView
                            dv.RowFilter = "partycode= '" + hotelDt.Rows(i)("partycode") + "'"
                            'dv.RowFilter = "partycode= '" + lastpartycode + "'"

                            strMessage += "<tr>  <td colspan='3' width='642' style='border: 1px solid black;padding-left:10px;padding-top:3px;padding-bottom:3px;padding-right:10px;font-family:Calibri;'> <font size='-1' color = '#FF0000' <b> <u> Cancellation Policy:</u></b></font><br>"
                            If dv.ToTable.Rows.Count > 0 Then
                                For rowindex = 0 To dv.ToTable.Rows.Count - 1
                                    strMessage += "<font size='-1.5'>" + dv(rowindex).Item("policytext") + "</font>  "
                                Next
                            End If
                            strMessage += " </td></tr>   </table>"   ''' Added shahul 19/06/18
                        Else
                            strMessage += " </td></tr>   </table>"

                        End If
                    Else
                        strMessage += "</table>"
                    End If
                    lastpartycode = hotelDt.Rows(i)("partycode")

                Next
                strMessage += "</table>"
            End If





            strMessage += " <p>&nbsp;</p><table width='700' border='1' cellpadding='0' cellspacing='0'>"
            If allservicedt.Rows.Count > 0 Then
                strMessage += " <tr> <th width='624' cope='col' style='border: 1px solidblack;background-color: #c00000; color: #FFF; text-align: center; font-family: Calibri;'><fontsize='-1.5'>Inclusions:</font> </th>"
                strMessage += "<th width='76' scope='col' align='center' style='border:1px solid black;background-color: #c00000; color: #FFF;text-align: center; font-family: Calibri;'><font size='-1.5'>Date</font></th>  </tr>"
                '' Added shahul 07/06/18
                'Dim AllServices = (From n In allservicedt.AsEnumerable() Group n By ServcieNameRowid = New With {Key .Servicename = n.Item(2), Key .Servicedate = n.Item(1)} Into g1 = Group Select New With {.Servicename = ServcieNameRowid}).ToList()

                'For Each row In AllServices

                '    strMessage += "           <tr>  <td width='624' style='border: dotted 1px;padding-left:10px;padding-right:10px;font-family:Calibri;'><font size='-1'>" + row.Servicename.Servicename + "</font></td> <td width='76' align='center' style='border: dotted 1px;font-family: Calibri;'><font size='-1'>" + Format(CType(row.Servicename.Servicedate, Date), "dd.MM.yyyy") + "</td>   </tr>"

                'Next
                For i = 0 To allservicedt.Rows.Count - 1

                    strMessage += "           <tr>  <td width='624' style='border: dotted 1px;padding-left:10px;padding-right:10px;font-family:Calibri;'><font size='-1'>" + allservicedt.Rows(i)("Servicename") + "</font></td> <td width='76' align='center' style='border: dotted 1px;font-family: Calibri;'><font size='-1'>" + Format(CType(allservicedt.Rows(i)("Servicedate"), Date), "dd.MM.yyyy") + "</td>   </tr>"
                Next

                strMessage += "        <tr> <td colspan='2'>  <p><font color='red'><strong><u>Note </u></strong>: Any services other than the above    mentioned are Excluded.</font> </p>     </td>     </tr>"

                strMessage += "</table>"
            End If

            strMessage += "    <p><strong><u>Applicable  Rates:</u></strong></p>"

            strMessage += "   <table border='1' cellspacing='0' cellpadding='0' width='700'style='font-family: Calibri;'> "

            strMessage += "    <tr>  <th width='400' nowrap='nowrap' rowspan='2' style='border:1px solid black;background-color:#ffff00; color: black;text-align: center; font-family: Calibri;'><font size='-1.5'>PER PERSON PACKAGE [ " + totadultchild + " ]</font> </th>"

            strMessage += " <th nowrap='nowrap' colspan='3' style='border: 1px solid black;background-color: #4f6228; color: #ffff00;text-align: center; font-family: Calibri;'><font size='-1.5'>USD</font></th> </tr>"

            strMessage += "<tr> <th width='60' scope='col' align='center' style='border:1px solid black;background-color: #ffff00; color: black; text-align: center; font-family: Calibri;'><font size='-1.5'>PAX COUNT</font> </th>"

            strMessage += " <th  width='120' scope='col' align='center' style='border:1px solid black;background-color: #ffff00; color: black;text-align: center; font-family: Calibri;'><font size='-1.5'>PKG PER PERSON</font> </th>"

            strMessage += "<th width='120' align='right' scope='col' align='center' Style = 'border: 1px solid black;background-color: #ffff00;  color: black; text-align: center; font-family:Calibri;'><font size='-1.5'>TOTAL</font> </th>  </tr>"

            If adchbreakupdt.Rows.Count > 0 Then


                For i = 0 To adchbreakupdt.Rows.Count - 1

                    strMessage += " <tr>  <td width='400' style='border: 1px solid black;padding-left:10px;padding-right:10px;font-family:Calibri;'><font size='-1'><strong>" + adchbreakupdt.Rows(i).Item(0) + "</strong></font></td>"

                    strMessage += "<td width='60'  align='center' style='border: 1px solid black ;font-family: Calibri;'><font size='-1'>" + adchbreakupdt.Rows(i).Item("noofpax").ToString() + "</font></td>"
                    strMessage += "  <td width='120' align='center'  style='border: 1px solid black ;font-family: Calibri;'><font size='-1'> " + strCurrCode + " " + Math.Round(Convert.ToDecimal(adchbreakupdt.Rows(i).Item("usdperpax").ToString()), 2).ToString() + "</font></td>"
                    strMessage += "  <td width='120'  align='center' style='border: 1px solid black ;font-family: Calibri;'><font size='-1'><strong> " + strCurrCode + " " + Math.Round(Convert.ToDecimal(adchbreakupdt.Rows(i).Item("salevalueusd").ToString()), 2).ToString() + " </strong></font></td>  </tr>"

                Next
            End If

            strMessage += "           <tr>   <td nowrap='nowrap' colspan='3' style='border: 1px solidblack;padding-left:10px;padding-right:10px;font-family:Calibri;background-color: #c00000; color: #FFF; text-align: center;'><font size='-1'><strong>TOTAL NET PAYABLE</strong></font></td>"
            strMessage += "        <td width='120' align='center' style='border: 1px solidblack;padding-left:10px;padding-right:10px;font-family:Calibri;background-color: #0070c0; color: #FFF;text-align: center;'><strong> " + strCurrCode + " " + Math.Round(Convert.ToDecimal(totadchpricedt.Rows(0).Item("salevalueused").ToString()), 2).ToString() + "</strong></td>"
            strMessage += "   </tr>   </table>"
            strMessage += "   <br />  <hr  Style = 'height:2px;border:none;color:#333;background-color:#333;'/>"
            strMessage += "        <table border='0' cellspacing='0' cellpadding='0'>"
            strMessage += "   <tr><td width='747' valign='top' >   <span style='font-family: Calibri;color:red'><font size='-1'><p><strong><u>VALUE    ADDED TAX (VAT):</u></strong></p></font></span>"
            strMessage += "   <ul>  <li style='font-family: Calibri;' ><font size='-1'><span dir='ltr'> </span>Above rate quotation is Inclusive of VAT.</font></li> </ul>"
            strMessage += " </td></tr> </table>"

            Dim Tourismpkgterms() As DataRow
            Tourismpkgterms = pktermsdt.Select("pktype='TDF'")
            If Tourismpkgterms.Count > 0 Then 'this condition is added / changed by mohamed on 01/07/2018
                strMessage += "   <hr style='height:1px;border:none;color:#333;' />     <table border='0' cellspacing='0' cellpadding='0' >"
                strMessage += "   <tr>   <td width='747' valign='top'> <span style='font-family: Calibri;color:red'><fontsize='-1'><p><strong><u>TOURISM    DIRHAM FEE:</u></strong></p></font></span>" + Tourismpkgterms(0).Item("pkterms")
                strMessage += "</td></tr></table> "
            End If

            Dim Visapkgterms() As DataRow

            Visapkgterms = pktermsdt.Select("pktype='VIH'")
            If Visapkgterms.Count > 0 Then 'this condition is added / changed by mohamed on 01/07/2018
                strMessage += " <hr Style = 'height:2px;border:none;color:#333;background-color:#333;'/>"
                strMessage += "     <table border='0' cellspacing='0' cellpadding='0'>  <tr> <td width='747' valign='top'> <span style='font-family: Calibri;color:red'><font size='-1'><p><strong><u>VISA INFORMATION:</u></strong></p></font></span>" + Visapkgterms(0).Item("pkterms")
                strMessage += "</td>  </tr>    </table> "
            End If

            Dim Bankchargesterms() As DataRow
            Bankchargesterms = pktermsdt.Select("pktype='BC'")
            If Visapkgterms.Count > 0 Then 'this condition is added / changed by mohamed on 01/07/2018
                strMessage += "    <hr Style = 'height:2px;border:none;color:#333;background-color:#333;'/>"
                strMessage += "   <table border='0' cellspacing='0' cellpadding='0'> <tr> <td width='747' valign='top'>  <span style='font-family: Calibri;color:red'><font size='-1'><p><strong><u>BANK CHARGES:</u></strong></p></font></span>" + Bankchargesterms(0).Item("pkterms")
                strMessage += "             </td> </tr> </table>"
            End If
            Dim Visatermsfooter() As DataRow

            Visatermsfooter = pktermsdt.Select("pktype='VIF'")
            If Visapkgterms.Count > 0 Then 'this condition is added / changed by mohamed on 01/07/2018
                strMessage += " <hr    Style = 'height:2px;border:none;color:#333;background-color:#333;'/>"
                strMessage += "    <table border='0' cellspacing='0' cellpadding='0'>"
                strMessage += "   <tr>  <td width='747' valign='top'>   <span style='font-family: Calibri;color:red'><font size='-1'><p><strong><u>GENERAL INFORMATION:</u></strong></p></font></span>" + Visatermsfooter(0).Item("pkterms")
                strMessage += "    </td>    </tr>       </table> "
            End If
            strMessage += "  <hr   Style = 'height:3px;border:none;color:#333;background-color:#333;'/>"

            Dim companyname As String = objclsUtilities.ExecuteQueryReturnStringValue("Select top 1 isnull(companytoshow,'') companytoshow from columbusmaster(nolock) ")

            strMessage += "  <br /><br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Best Regards,</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>" + companyname + " software admin team</span></p>"

            '<body style='font-family:Calibri'> <h5>

            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' Park
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' Gulf
            End If

            'added param on 08/08/2020
            If roEmailRequired = False Then
                to_email = ""
            End If
            If agentEmailRequired = False Then
                agentemail = ""
            End If
            If quoteCcEmailRequired = False Then
                ccemails = ""
            End If
            If salesforceEmailId <> "" Then
                If ccemails <> "" Then
                    ccemails = ccemails + "," + salesforceEmailId
                Else
                    ccemails = salesforceEmailId
                End If
            End If
            If commonEmailId <> "" Then
                If ccemails <> "" Then
                    ccemails = ccemails + "," + commonEmailId
                Else
                    ccemails = commonEmailId
                End If
            End If

            If agentemail = "" Then
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else
                    'agentemail = agentemail + "," + ccemails
                    agentemail = ccemails               'modified param 23/10/2018
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
            'to_email = "tanvir@mahce.com"
            'agentemail = "tanvir@mahce.com"

            ''Added shahul  27/05/2018
            Dim defaultusername As String = "", defaultpwd As String = ""
            Dim strfromusername As String = "", strfrompwd As String = ""

            defaultusername = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1504")
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='" & salesperson & "'")
            If emaildt.Rows.Count > 0 Then
                'strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                'strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))

                'Modified by abin on 20181212
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Then
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

            'passed true as parameters / changed by mohamed on 25/09/2018 - REF DT25092018_A
            If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser(True) = True Then 'changed by mohamed on 28/06/2018
                If Session("sLoginType") = "RO" Then
                    agentemail = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                    Dim lsROEmailId As String
                    lsROEmailId = objclsUtilities.ExecuteQueryReturnStringValue("select usemail from usermaster (nolock) where usercode='" & Session("GlobalUserName") & "'")
                    Dim to_email2 As String = IIf(lsROEmailId.Trim = "", agentemail, lsROEmailId.Trim)
                    to_email = to_email + "," + to_email2
                    strfromemailid = to_email2
                    agentemail = ccemails
                End If
            End If

            If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, agentemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_RO")
            Else
                objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_RO")
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: Sendemail :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnBacktoBookingForPackage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBacktoBookingForPackage.Click

        mpPackageConfirmError.Hide()




        Dim strrequestid As String = fnFinalQuoteSaveBooking(lblPRequestId.Text, lblDivCode.Text, txtAgencyRef.Text)
        If strrequestid <> "" Then
            hdQuoteReqestId.Value = strrequestid

            Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + strrequestid + "'")
            If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
            '' As per Arun email Comment Email option cummulative agent
            If chkCumulative.Trim() = "CUMULATIVE" Then
                'sendemail("New", strrequestid)
                sendemailformat("New", strrequestid)
            Else 'changed by mohamed on 02/07/2018
                'changed by mohamed on 02/07/2018
                Dim lsQuoteToIndividual As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2022")
                If lsQuoteToIndividual = "Y" Then
                    sendemailIndividualAgent("New", strrequestid)
                End If
            End If
            '''''''''
            divsubmitquote.Style.Add("display", "none")
            btnProceedBooking.Style.Add("display", "none")
            btnAbondon.Style.Add("display", "none")
            divprintquote.Style.Add("display", "block")

            divbackhome.Style.Add("display", "block")
            divcheck.Style.Add("display", "none")
            btnGeneratePackageValue.Visible = False
            MessageBox.ShowMessage(Page, MessageType.Success, "Quotation Created  " + strrequestid)

            Session("sFinalBooked") = "1"
            HideServiceButtons()
        Else
            MessageBox.ShowMessage(Page, MessageType.Errors, "Quotation Failed  Please Submit Again ")
            divsubmitquote.Style.Add("display", "block")
            btnProceedBooking.Style.Add("display", "block")
            btnAbondon.Style.Add("display", "block")
            divprintquote.Style.Add("display", "none")
            divbackhome.Style.Add("display", "none")
            divcheck.Style.Add("display", "block")
        End If

    End Sub

    Protected Sub btnBackTo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackTo.Click

    End Sub

    Private Function fnFinalQuoteSaveBooking(ByVal requestid As String, ByVal divcode As String, ByVal AgentRef As String) As String

        Dim strPackageSummary As New StringBuilder
        Dim strPackageValueSummary As New StringBuilder

        Dim ds As DataSet
        Dim objBLLGuest As New BLLGuest
        ds = objBLLGuest.GeneratePackageValue(requestid, "1", Session("sLoginType"))
        dvPackageDetails.Style.Add("display", "none")
        If ds.Tables.Count > 0 Then
            Session("sdsPackageSummary") = ds

            If ds.Tables(2).Rows.Count > 0 Then
                FillPackageFields(ds.Tables(2))
            End If

            If ds.Tables(1).Rows.Count > 0 Then
                strPackageSummary.Append("<DocumentElement>")
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    strPackageSummary.Append("<Table>")
                    strPackageSummary.Append("<requestid>" & requestid & "</requestid>")
                    strPackageSummary.Append("<requesttype>" & ds.Tables(1).Rows(i)("requesttype").ToString & "</requesttype>")
                    strPackageSummary.Append("<rlineno>" & ds.Tables(1).Rows(i)("rlineno").ToString & "</rlineno>")
                    strPackageSummary.Append("<adults>" & ds.Tables(1).Rows(i)("adults").ToString & "</adults>")
                    strPackageSummary.Append("<child>" & ds.Tables(1).Rows(i)("child").ToString & "</child>")
                    strPackageSummary.Append("<salevalue>" & ds.Tables(1).Rows(i)("salevalue").ToString & "</salevalue>")
                    strPackageSummary.Append("<salevaluebase>" & ds.Tables(1).Rows(i)("salevaluebase").ToString & "</salevaluebase>")
                    strPackageSummary.Append("<costvalue>" & ds.Tables(1).Rows(i)("costvalue").ToString & "</costvalue>")
                    strPackageSummary.Append("</Table>")

                Next
                strPackageSummary.Append("</DocumentElement>")
                objBLLGuest.PackageSummary = strPackageSummary.ToString
            End If

            If ds.Tables(2).Rows.Count > 0 Then
                strPackageValueSummary.Append("<DocumentElement>")
                For i As Integer = 0 To ds.Tables(2).Rows.Count - 1
                    strPackageValueSummary.Append("<Table>")
                    strPackageValueSummary.Append("<requestid>" & requestid & "</requestid>")
                    strPackageValueSummary.Append("<totalsalevalue>" & ds.Tables(2).Rows(i)("totalsalevalue").ToString & "</totalsalevalue>")
                    strPackageValueSummary.Append("<totalsalevaluebase>" & ds.Tables(2).Rows(i)("totalsalevaluebase").ToString & "</totalsalevaluebase>")
                    strPackageValueSummary.Append("<totalcostvaluebase>" & ds.Tables(2).Rows(i)("totalcostvaluebase").ToString & "</totalcostvaluebase>")
                    strPackageValueSummary.Append("<adults>" & Val(ds.Tables(2).Rows(i)("adults").ToString) & "</adults>")
                    strPackageValueSummary.Append("<child>" & Val(ds.Tables(2).Rows(i)("child").ToString) & "</child>")
                    strPackageValueSummary.Append("<withvisa>" & Val(ds.Tables(2).Rows(i)("withvisa").ToString) & "</withvisa>")
                    strPackageValueSummary.Append("<adultmarkup>" & Val(ds.Tables(2).Rows(i)("adultmarkup").ToString) & "</adultmarkup>")
                    strPackageValueSummary.Append("<childmarkup>" & Val(ds.Tables(2).Rows(i)("childmarkup").ToString) & "</childmarkup>")
                    strPackageValueSummary.Append("<minimummarkup>" & Val(ds.Tables(2).Rows(i)("minimummarkup").ToString) & "</minimummarkup>")
                    strPackageValueSummary.Append("<totalmarkupbase>" & Val(ds.Tables(2).Rows(i)("totalmarkupbase").ToString) & "</totalmarkupbase>")
                    strPackageValueSummary.Append("<differentialmarkup>" & Val(ds.Tables(2).Rows(i)("differentialmarkup").ToString) & "</differentialmarkup>")
                    strPackageValueSummary.Append("<formulaid>" & ds.Tables(2).Rows(i)("formulaid").ToString & "</formulaid>")
                    strPackageValueSummary.Append("<flineno>" & Val(ds.Tables(2).Rows(i)("flineno").ToString) & "</flineno>")
                    strPackageValueSummary.Append("<fromslab>" & Val(ds.Tables(2).Rows(i)("fromslab").ToString) & "</fromslab>")
                    strPackageValueSummary.Append("<toslab>" & Val(ds.Tables(2).Rows(i)("toslab").ToString) & "</toslab>")
                    strPackageValueSummary.Append("<discountperc>" & Val(ds.Tables(2).Rows(i)("discountperc").ToString) & "</discountperc>")
                    strPackageValueSummary.Append("<discountmarkup>" & Val(ds.Tables(2).Rows(i)("discountmarkup").ToString) & "</discountmarkup>")
                    strPackageValueSummary.Append("<netsalevalue>" & Val(ds.Tables(2).Rows(i)("netsalevalue").ToString) & "</netsalevalue>")
                    strPackageValueSummary.Append("</Table>")

                Next
                strPackageValueSummary.Append("</DocumentElement>")
                objBLLGuest.PackageValueSummary = strPackageValueSummary.ToString
            End If


        End If
        Dim iCumulative As Integer = 0
        If Session("sLoginType") = "RO" Then
            iCumulative = objBLLGuest.CheckSelectedAgentIsCumulative(Session("sRequestId"))
        End If
        If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
            objBLLGuest.Cumulative = "1"
        End If
        Dim strrequestid As String = ""
        If Not Session("sEditRequestId") Is Nothing Then
            strrequestid = objBLLGuest.FinalQuoteSaveBooking(requestid, divcode, AgentRef, "EDIT")
        Else
            strrequestid = objBLLGuest.FinalQuoteSaveBooking(requestid, divcode, AgentRef, "NEW")
        End If
        Return strrequestid
    End Function

    Protected Sub btnProceedWithBook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProceedWithBook.Click

        Dim strPackageSummary As New StringBuilder
        Dim strPackageValueSummary As New StringBuilder
        Dim strRequestId As String = lblPRequestId.Text
        Dim iCumulative As Integer = 0
        If Session("sLoginType") = "RO" Then
            iCumulative = objBLLguest.CheckSelectedAgentIsCumulative(strRequestId)
        End If
        FindCumilative()
        If hdBookingEngineRateType.Value = "1" Or iCumulative > 0 Then
            iCumulative = 1
            objBLLguest.Cumulative = iCumulative
            objBLLguest.GBRequestid = strRequestId


            '  ds = objBLLguest.GeneratePackageValue(Session("sRequestId"), "1")
            Dim ds As DataSet = objBLLguest.GeneratePackageValue(strRequestId, "1", Session("sLoginType"))

            If ds.Tables.Count > 0 Then



                If ds.Tables(1).Rows.Count > 0 Then
                    strPackageSummary.Append("<DocumentElement>")
                    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                        strPackageSummary.Append("<Table>")
                        strPackageSummary.Append("<requestid>" & strRequestId & "</requestid>")
                        strPackageSummary.Append("<requesttype>" & ds.Tables(1).Rows(i)("requesttype").ToString & "</requesttype>")
                        strPackageSummary.Append("<rlineno>" & ds.Tables(1).Rows(i)("rlineno").ToString & "</rlineno>")
                        strPackageSummary.Append("<adults>" & ds.Tables(1).Rows(i)("adults").ToString & "</adults>")
                        strPackageSummary.Append("<child>" & ds.Tables(1).Rows(i)("child").ToString & "</child>")
                        strPackageSummary.Append("<salevalue>" & ds.Tables(1).Rows(i)("salevalue").ToString & "</salevalue>")
                        strPackageSummary.Append("<salevaluebase>" & ds.Tables(1).Rows(i)("salevaluebase").ToString & "</salevaluebase>")
                        strPackageSummary.Append("<costvalue>" & ds.Tables(1).Rows(i)("costvalue").ToString & "</costvalue>")
                        strPackageSummary.Append("</Table>")

                    Next
                    strPackageSummary.Append("</DocumentElement>")
                    objBLLguest.PackageSummary = strPackageSummary.ToString
                End If

                If ds.Tables(2).Rows.Count > 0 Then
                    strPackageValueSummary.Append("<DocumentElement>")
                    For i As Integer = 0 To ds.Tables(2).Rows.Count - 1
                        strPackageValueSummary.Append("<Table>")
                        strPackageValueSummary.Append("<requestid>" & strRequestId & "</requestid>")
                        strPackageValueSummary.Append("<totalsalevalue>" & ds.Tables(2).Rows(i)("totalsalevalue").ToString & "</totalsalevalue>")
                        strPackageValueSummary.Append("<totalsalevaluebase>" & ds.Tables(2).Rows(i)("totalsalevaluebase").ToString & "</totalsalevaluebase>")
                        strPackageValueSummary.Append("<totalcostvaluebase>" & ds.Tables(2).Rows(i)("totalcostvaluebase").ToString & "</totalcostvaluebase>")
                        strPackageValueSummary.Append("<adults>" & Val(ds.Tables(2).Rows(i)("adults").ToString) & "</adults>")
                        strPackageValueSummary.Append("<child>" & Val(ds.Tables(2).Rows(i)("child").ToString) & "</child>")
                        strPackageValueSummary.Append("<withvisa>" & Val(ds.Tables(2).Rows(i)("withvisa").ToString) & "</withvisa>")
                        strPackageValueSummary.Append("<adultmarkup>" & Val(ds.Tables(2).Rows(i)("adultmarkup").ToString) & "</adultmarkup>")
                        strPackageValueSummary.Append("<childmarkup>" & Val(ds.Tables(2).Rows(i)("childmarkup").ToString) & "</childmarkup>")
                        strPackageValueSummary.Append("<minimummarkup>" & Val(ds.Tables(2).Rows(i)("minimummarkup").ToString) & "</minimummarkup>")
                        strPackageValueSummary.Append("<totalmarkupbase>" & Val(ds.Tables(2).Rows(i)("totalmarkupbase").ToString) & "</totalmarkupbase>")
                        strPackageValueSummary.Append("<differentialmarkup>" & Val(ds.Tables(2).Rows(i)("differentialmarkup").ToString) & "</differentialmarkup>")
                        strPackageValueSummary.Append("<formulaid>" & ds.Tables(2).Rows(i)("formulaid").ToString & "</formulaid>")
                        strPackageValueSummary.Append("<flineno>" & Val(ds.Tables(2).Rows(i)("flineno").ToString) & "</flineno>")
                        strPackageValueSummary.Append("<fromslab>" & Val(ds.Tables(2).Rows(i)("fromslab").ToString) & "</fromslab>")
                        strPackageValueSummary.Append("<toslab>" & Val(ds.Tables(2).Rows(i)("toslab").ToString) & "</toslab>")
                        strPackageValueSummary.Append("<discountperc>" & Val(ds.Tables(2).Rows(i)("discountperc").ToString) & "</discountperc>")
                        strPackageValueSummary.Append("<discountmarkup>" & Val(ds.Tables(2).Rows(i)("discountmarkup").ToString) & "</discountmarkup>")
                        strPackageValueSummary.Append("<netsalevalue>" & Val(ds.Tables(2).Rows(i)("netsalevalue").ToString) & "</netsalevalue>")
                        '' Added shahul 11/04/18
                        strPackageValueSummary.Append("<childfreeupto>" & Val(ds.Tables(2).Rows(i)("childfreeupto").ToString) & "</childfreeupto>")
                        strPackageValueSummary.Append("<ChildFreeWithVisa>" & Val(ds.Tables(2).Rows(i)("ChildFreeWithVisa").ToString) & "</ChildFreeWithVisa>")
                        strPackageValueSummary.Append("<ChildFreeWithoutVisa>" & Val(ds.Tables(2).Rows(i)("ChildFreeWithoutVisa").ToString) & "</ChildFreeWithoutVisa>")

                        '' Added shahul 01/07/18  temporaly commentd once finish development uncomment again
                        strPackageValueSummary.Append("<adultrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("adultrevisedmarkup").ToString) & "</adultrevisedmarkup>")
                        strPackageValueSummary.Append("<adultreviseddiscount>" & Val(ds.Tables(2).Rows(i)("adultreviseddiscount").ToString) & "</adultreviseddiscount>")
                        strPackageValueSummary.Append("<childrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("childrevisedmarkup").ToString) & "</childrevisedmarkup>")
                        strPackageValueSummary.Append("<childreviseddiscount>" & Val(ds.Tables(2).Rows(i)("childreviseddiscount").ToString) & "</childreviseddiscount>")
                        strPackageValueSummary.Append("<childfreeuptorevisedmarkup>" & Val(ds.Tables(2).Rows(i)("childfreeuptorevisedmarkup").ToString) & "</childfreeuptorevisedmarkup>")
                        strPackageValueSummary.Append("<childfreeuptoreviseddiscount>" & Val(ds.Tables(2).Rows(i)("childfreeuptoreviseddiscount").ToString) & "</childfreeuptoreviseddiscount>")
                        strPackageValueSummary.Append("<totalrevisedmarkup>" & Val(ds.Tables(2).Rows(i)("totalrevisedmarkup").ToString) & "</totalrevisedmarkup>")
                        strPackageValueSummary.Append("<totalreviseddiscount>" & Val(ds.Tables(2).Rows(i)("totalreviseddiscount").ToString) & "</totalreviseddiscount>")

                        strPackageValueSummary.Append("</Table>")

                    Next
                    strPackageValueSummary.Append("</DocumentElement>")
                    objBLLguest.PackageValueSummary = strPackageValueSummary.ToString
                    Dim strResult As String = objBLLguest.SaveBookingProfitInTemp()

                End If
            End If

        End If
        If objBLLguest.PrearrangedValidate(Session("sRequestId")) = 1 Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Can not book pre-arranged hotel only.")
            Exit Sub
        End If
        Response.Redirect("~\GuestPagenew.aspx")
    End Sub

  
    Private Sub FillPackageFields(ByVal dataTable As DataTable)
        Dim strBaseCurrency As String = dataTable.Rows(0)("BaseCurrency").ToString
        Dim strSaleCurrCode As String = dataTable.Rows(0)("salecurrcode").ToString

        lblBaseCurrency.Text = strBaseCurrency
        lblsalecurrcode.Text = strSaleCurrCode
        lblsalecurrcodeAgent.Text = strSaleCurrCode
        lblPTotalSaleValueText.Text = "Total Sale Value" ' (" & strSaleCurrCode & ") "
        lblPTotalSaleValue.Text = fnRound(dataTable.Rows(0)("totalsalevalue").ToString, 0)
        lblPTotalSaleValueTextAgent.Text = "Total Sale Value" ' (" & strSaleCurrCode & ") "
        lblPTotalSaleValueAgent.Text = fnRound(dataTable.Rows(0)("totalsalevalue").ToString, 0)


        'lblPTotalSaleValueAEDText.Text = "Total Sale Value (" & strBaseCurrency & ") "
        lblPTotalSaleValueAED.Text = dataTable.Rows(0)("totalsalevaluebase").ToString

        lblPTotalCostValueText.Text = "Total Cost Value" ' (" & strBaseCurrency & ") "
        lblPTotalCostValue.Text = dataTable.Rows(0)("totalcostvaluebase").ToString
        lblPTotalCostValueCurr.Text = fnRound(dataTable.Rows(0)("totalcostvaluecurr").ToString, 0)
        lblPTotalCostValueText.Font.Bold = True
        lblPTotalCostValue.Font.Bold = True
        lblPTotalCostValueCurr.Font.Bold = True

        lblPTotalProfitText.Text = "Total Profit" ' (" & strBaseCurrency & ") "
        lbPTotalProfit.Text = dataTable.Rows(0)("totalmarkupbase").ToString
        lbPTotalProfitCurr.Text = fnRound(dataTable.Rows(0)("totalmarkupcurr").ToString, 0)

        lblPMinimumMarkupText.Text = "Minimum Markup" ' (" & strBaseCurrency & ") "
        lblPMinimumMarkup.Text = dataTable.Rows(0)("minimummarkup").ToString
        lblPMinimumMarkupCurr.Text = fnRound(dataTable.Rows(0)("minimummarkupcurr").ToString, 0)

        lblPFormulaIdText.Text = "Formula Id "
        lblPFormulaId.Text = dataTable.Rows(0)("formulaid").ToString
        lblPDifferentialMarkupText.Text = "Differential Markup" '  (" & strSaleCurrCode & ") "
        lblPDifferentialMarkup.Text = fnRound(dataTable.Rows(0)("differentialmarkup").ToString, 2)
        lblPDifferentialMarkupbase.Text = dataTable.Rows(0)("differentialmarkupbase").ToString

        lblPDiscount_DifferentialMarkupText.Text = "Discount % on Differential Markup"
        lblPDiscount_DifferentialMarkup.Text = dataTable.Rows(0)("discountperc").ToString & " %"
        lblPDiscount_DifferentialMarkup1.Text = dataTable.Rows(0)("discountperc").ToString & " %"

        lblPDiscountValueText.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
        lblPDiscountValue.Text = fnRound(dataTable.Rows(0)("discountmarkup").ToString, 2)
        lblPDiscountValueBase.Text = dataTable.Rows(0)("discountmarkupbase").ToString

        '' Added Shahul 28/06/18
        'lblPNetprofitText.Text = "Net Profit"
        'lblPNetprofitValue.Text = CType(Val(dataTable.Rows(0)("totalmarkupcurr").ToString) - Val(dataTable.Rows(0)("discountmarkup").ToString), String)
        'lblPNetprofitValueBase.Text = CType(Val(dataTable.Rows(0)("totalmarkupbase").ToString) - Val(dataTable.Rows(0)("discountmarkupbase").ToString), String)

        lblPDiscountValueTextAgent.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
        lblPDiscountValueAgent.Text = dataTable.Rows(0)("discountmarkup").ToString

        lblPNetprofitText.Text = "Net Profit"
        lblPNetprofitValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalmarkupcurr").ToString) - Val(dataTable.Rows(0)("discountmarkup").ToString), String), 0)
        lblPNetprofitValueBase.Text = CType(Val(dataTable.Rows(0)("totalmarkupbase").ToString) - Val(dataTable.Rows(0)("discountmarkupbase").ToString), String)

        lblPNetprofitText.Font.Bold = True
        lblPNetprofitValue.Font.Bold = True
        lblPNetprofitValueBase.Font.Bold = True

        lblPDiscountValueTextAgent.Text = "Discount Value" ' (" & strSaleCurrCode & ") "
        lblPDiscountValueAgent.Text = dataTable.Rows(0)("discountmarkup").ToString




        '' Added Shahul 30/06/18
        lblSystemMarkupText.Text = "System Markup"
        lblSystemMarkupValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalmarkupcurr").ToString) - Val(dataTable.Rows(0)("discountmarkup").ToString), String), 0)
        lblSystemMarkupValueBase.Text = CType(Val(dataTable.Rows(0)("totalmarkupbase").ToString) - Val(dataTable.Rows(0)("discountmarkupbase").ToString), String)
        'lblSystemMarkupValue.Text = fnRound(CType(Val(lbPTotalProfitCurr.Text) - Val(lblPDiscountValue.Text), String), 0)
        'lblSystemMarkupValueBase.Text = CType(Val(lbPTotalProfit.Text) - Val(lblPDiscountValueBase.Text), String)

        lblRevisedMarkupText.Text = "Revised Markup"
        lblRevisedMarkupValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalrevisedmarkup").ToString), String), 0)
        lblRevisedMarkupValueBase.Text = CType(fnRound(Val(dataTable.Rows(0)("totalrevisedmarkup").ToString) * Val(dataTable.Rows(0)("saleconvrate").ToString), 0), String)


        lblSystemDiscountText.Text = "System Discount"
        lblSystemDiscountValue.Text = fnRound(CType(Val(dataTable.Rows(0)("discountmarkup").ToString), String), 0)
        lblSystemDiscountValueBase.Text = fnRound(CType(Val(dataTable.Rows(0)("discountmarkupbase").ToString), String), 0)

        lblRevisedDiscountText.Text = "Revised Discount"
        If Val(lblRevisedMarkupValue.Text) <> 0 Then
            ' lblRevisedDiscountValue.Text = CType(IIf(Val(lblRevisedMarkupValue.Text) > Val(lblSystemMarkupValue.Text), Val(lblSystemDiscountValue.Text) - (Val(lblRevisedMarkupValue.Text) - Val(lblSystemMarkupValue.Text)), Val(lblSystemDiscountValue.Text) - (Val(lblSystemMarkupValue.Text) - Val(lblRevisedMarkupValue.Text))), String)
            lblRevisedDiscountValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalreviseddiscount").ToString), String), 0)
            lblRevisedDiscountValueBase.Text = CType(fnRound(Val(dataTable.Rows(0)("totalreviseddiscount").ToString) * Val(dataTable.Rows(0)("saleconvrate").ToString), 0), String)
        Else
            lblRevisedDiscountValue.Text = ""
            lblRevisedDiscountValueBase.Text = ""
        End If

        lblPNetSaleValueText.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
        lblPNetSaleValueTextAgent.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "

        '' Added shahul 06/10/18
        If Val(lblRevisedDiscountValue.Text) = 0 Then
            lblPNetSaleValue.Text = fnRound(dataTable.Rows(0)("netsalevalue").ToString, 0)
            lblPNetSaleValueAgent.Text = fnRound(dataTable.Rows(0)("netsalevalue").ToString, 0)
            lblPNetSaleValueBase.Text = fnRound(dataTable.Rows(0)("netsalevaluebase").ToString, 0)
        Else
            lblPNetSaleValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalsalevalue").ToString) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString), String), 0)
            lblPNetSaleValueAgent.Text = fnRound(CType(Val(dataTable.Rows(0)("totalsalevalue").ToString) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString), String), 0)
            lblPNetSaleValueBase.Text = fnRound(CType(Math.Round(Val(dataTable.Rows(0)("totalsalevaluebase").ToString) - Val(CType(Math.Round(Val(dataTable.Rows(0)("totalreviseddiscount").ToString) * Val(dataTable.Rows(0)("saleconvrate").ToString), 0), String))), String), 0)

            'lblPNetSaleValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalsalevalue").ToString) + (Val(dataTable.Rows(0)("totalmarkupcurr").ToString)) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString), String), 0)
            'lblPNetSaleValueAgent.Text = fnRound(CType(Val(dataTable.Rows(0)("totalsalevalue").ToString) + (Val(dataTable.Rows(0)("totalmarkupcurr").ToString)) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString), String), 0)
            'lblPNetSaleValueBase.Text = fnRound(CType(Math.Round(Val(dataTable.Rows(0)("totalsalevaluebase").ToString) + (Val(dataTable.Rows(0)("totalmarkupbase").ToString)) - Val(CType(Math.Round(Val(dataTable.Rows(0)("totalreviseddiscount").ToString) * Val(dataTable.Rows(0)("saleconvrate").ToString), 0), String))), String), 0)


            'lblPNetSaleValue.Text = fnRound(CType(Val(dataTable.Rows(0)("totalcostvaluecurr").ToString) + (Val(dataTable.Rows(0)("minimummarkupcurr").ToString) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString)), String), 0)
            'lblPNetSaleValueAgent.Text = fnRound(CType(Val(dataTable.Rows(0)("totalcostvaluecurr").ToString) + (Val(dataTable.Rows(0)("minimummarkupcurr").ToString) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString)), String), 0)
            'lblPNetSaleValueBase.Text = fnRound(CType(Math.Round(Val(dataTable.Rows(0)("totalcostvaluebase").ToString) + (Val(dataTable.Rows(0)("minimummarkup").ToString) - Val(dataTable.Rows(0)("totalreviseddiscount").ToString))), String), 0)


        End If
        lblPNetSaleValue.Font.Bold = True
        lblPNetSaleValueAgent.Font.Bold = True
        lblPNetSaleValueBase.Font.Bold = True
        lblPNetSaleValueText.Font.Bold = True
        'lblPNetSaleValueText.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
        'lblPNetSaleValue.Text = dataTable.Rows(0)("netsalevalue").ToString

        'lblPNetSaleValueTextAgent.Text = "Net Sale Value" ' (" & strSaleCurrCode & ") "
        'lblPNetSaleValueAgent.Text = dataTable.Rows(0)("netsalevalue").ToString

        'lblPNetSaleValueBase.Text = dataTable.Rows(0)("netsalevaluebase").ToString

        '' '' Added Shahul 30/06/18 temporlay comment once development finish uncomment again
        'lblSystemMarkupText.Text = "System Markup"
        'lblSystemMarkupValue.Text = CType(Val(lbPTotalProfitCurr.Text) - Val(lblPDiscountValue.Text), String)
        'lblSystemMarkupValueBase.Text = CType(Val(lbPTotalProfit.Text) - Val(lblPDiscountValueBase.Text), String)

        'lblRevisedMarkupText.Text = "Revised Markup"


        'lblRevisedMarkupValue.Text = CType(Val(dataTable.Rows(0)("totalrevisedmarkup").ToString), String)
        'lblRevisedMarkupValueBase.Text = CType(Math.Round(Val(dataTable.Rows(0)("totalrevisedmarkup").ToString) * Val(dataTable.Rows(0)("saleconvrate").ToString), 0), String)
        ''lblRevisedMarkupValue.Text = CType(Val(dataTable.Rows(0)("adultrevisedmarkup").ToString) + Val(dataTable.Rows(0)("childrevisedmarkup").ToString) + Val(dataTable.Rows(0)("childfreeuptorevisedmarkup").ToString), String)
        ''lblRevisedMarkupValueBase.Text = CType(Val(dataTable.Rows(0)("adultrevisedmarkup").ToString) + Val(dataTable.Rows(0)("childrevisedmarkup").ToString) + Val(dataTable.Rows(0)("childfreeuptorevisedmarkup").ToString), String)


        'lblSystemDiscountText.Text = "System Discount"
        'lblSystemDiscountValue.Text = CType(Val(lblPDiscountValue.Text), String)
        'lblSystemDiscountValueBase.Text = CType(Val(lblPDiscountValueBase.Text), String)

        'lblRevisedDiscountText.Text = "Revised Discount"
        'If Val(lblRevisedMarkupValue.Text) <> 0 Then
        '    ' lblRevisedDiscountValue.Text = CType(IIf(Val(lblRevisedMarkupValue.Text) > Val(lblSystemMarkupValue.Text), Val(lblSystemDiscountValue.Text) - (Val(lblRevisedMarkupValue.Text) - Val(lblSystemMarkupValue.Text)), Val(lblSystemDiscountValue.Text) - (Val(lblSystemMarkupValue.Text) - Val(lblRevisedMarkupValue.Text))), String)
        '    lblRevisedDiscountValue.Text = CType(Val(dataTable.Rows(0)("totalreviseddiscount").ToString), String)
        '    lblRevisedDiscountValueBase.Text = CType(Math.Round(Val(lblRevisedDiscountValue.Text) * Val(dataTable.Rows(0)("saleconvrate").ToString), 0), String)
        'Else
        '    lblRevisedDiscountValue.Text = ""
        '    lblRevisedDiscountValueBase.Text = ""
        'End If

        dvPackageDetails.Style.Add("display", "block")
        If Session("sLoginType") = "RO" Then
            'dvROPackage1.Style.Add("display", "block")
            'dvROPackage2.Style.Add("display", "block")
            'dvROPackage3.Style.Add("display", "block")
            'dvROPackage4.Style.Add("display", "block")
            'dvROPackage5.Style.Add("display", "block")
            '' dvROPackage6.Style.Add("display", "block")
            'dvROPackage7.Style.Add("display", "block")
            dvPackageSummaryRO.Style.Add("display", "block")
            dvPackageSummaryAgent.Style.Add("display", "none")
        Else
            dvPackageSummaryRO.Style.Add("display", "none")
            dvPackageSummaryAgent.Style.Add("display", "block")

            'dvROPackage1.Style.Add("display", "none")
            'dvROPackage2.Style.Add("display", "none")
            'dvROPackage3.Style.Add("display", "none")
            'dvROPackage4.Style.Add("display", "none")
            'dvROPackage5.Style.Add("display", "none")
            ''   dvROPackage6.Style.Add("display", "none")
            'dvROPackage7.Style.Add("display", "none")
        End If
    End Sub

    Protected Sub btncheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncheck.Click
        Try

            Dim dt As DataTable
            Dim requestid As String = ""
            Dim divcode As String = "", agentcode As String = "", sourcectry As String = ""
            requestid = GetExistingRequestId()
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)

            If dt.Rows.Count > 0 Then
                divcode = dt.Rows(0)("div_code").ToString
                agentcode = dt.Rows(0)("agentcode").ToString
                sourcectry = dt.Rows(0)("sourcectrycode").ToString


            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, "There is no Service to be Booked Please Select ")
                Exit Sub

            End If
            If objBLLguest.PrearrangedValidate(Session("sRequestId")) = 1 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Can not book pre-arranged hotel only.")
                Exit Sub
            End If
            final_save(requestid, divcode)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btncheck_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    Protected Sub btnFillPackageWithoutDiscount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFillPackageWithoutDiscount.Click
        Try
            If Not Session("sRequestId") Is Nothing Then
                Dim ds As DataSet
                Dim objBLLGuest As New BLLGuest
                ds = objBLLGuest.GeneratePackageValue(Session("sRequestId"), "1", Session("sLoginType"))
                dvPackageDetails.Style.Add("display", "none")
                If ds.Tables.Count > 0 Then
                    Session("sdsPackageSummary") = ds

                    If ds.Tables(2).Rows.Count > 0 Then

                        FillPackageFields(ds.Tables(2))


                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnFillPackageWithoutDiscount_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub btnproceed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnproceed.Click
        Dim dt As DataTable
        Dim requestid As String = ""
        Dim divcode As String = "", agentcode As String = "", sourcectry As String = ""
        requestid = GetExistingRequestId()
        dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)

        If dt.Rows.Count > 0 Then
            divcode = dt.Rows(0)("div_code").ToString
            agentcode = dt.Rows(0)("agentcode").ToString
            sourcectry = dt.Rows(0)("sourcectrycode").ToString


        Else
            MessageBox.ShowMessage(Page, MessageType.Warning, "There is no Service to be Booked Please Select ")
            Exit Sub

        End If

        mpBookingError.Hide()
        'MessageBox.ShowMessage(Page, MessageType.Success, "Quotation Created  ")
        'divsubmitquote.Style.Add("display", "none")
        'btnProceedBooking.Style.Add("display", "none")
        'btnAbondon.Style.Add("display", "none")
        'divprintquote.Style.Add("display", "block")
        'divbackhome.Style.Add("display", "block")
        'divcheck.Style.Add("display", "none")
        'Session("sFinalBooked") = "1"
        'HideServiceButtons()
        'btnGeneratePackageValue.Visible = False

        ' final_save(requestid, divcode)

        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "<script type='text/javascript'>showhidedivs();</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

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

    Protected Sub imgPreHotelEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim imgPreHotelAmend As ImageButton = CType(sender, ImageButton)
            Dim dlItem As DataListItem = CType(imgPreHotelAmend.NamingContainer, DataListItem)
            Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
            Response.Redirect("HotelSearch.aspx?PLineNo=" & lblrlineno.Text & "")
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: imgPreHotelEdit_Click" & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Protected Sub imgPreHotelDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Session("sdsPackageSummary") = Nothing
            Dim imgPreHotelDelete As ImageButton = CType(sender, ImageButton)
            Dim dlItem As DataListItem = CType(imgPreHotelDelete.NamingContainer, DataListItem)
            Dim lblrlineno As Label = CType(dlItem.FindControl("lblrlineno"), Label)
            Dim strmsg As String = "Are sure Want to Delete ?"

            Dim objBLLHotelSearch As New BLLHotelSearch
            Dim str As String = objBLLHotelSearch.RemovePreArrangedHotel(Session("sRequestId"), lblrlineno.Text)

            BindPreHotelSummary()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("GuestPage.aspx :: imgPreHotelDelete_Click" & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub
    Protected Sub ImgPreHotelRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs)

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

            If Not Session("sEditRequestId") Is Nothing Then

                If Not Session("sEditRequestId").ToString.Contains("QR/") And Not Session("sEditRequestId").ToString.Contains("QG/") And hdTempRequest.Value = "EDIT" Then

                    imgPreHotelEdit.ImageUrl = "~/img/button_amend.png"
                    imgPreHotelDelete.ImageUrl = "~/img/button_cancel.png"
                Else

                    imgPreHotelEdit.ImageUrl = "~/img/button_edit.png"
                    imgPreHotelDelete.ImageUrl = "~/img/button_remove.png"
                End If

            Else

                imgPreHotelEdit.ImageUrl = "~/img/button_edit.png"
                imgPreHotelDelete.ImageUrl = "~/img/button_remove.png"
            End If





            'If hdtourCancelled.Value = "1" Then

            '    dvTourConfirm.Visible = False
            '    divtourcancel.Visible = True
            '    divtourremarks.Visible = False
            '    divtouredit.Visible = False
            '    dvtourCancelled.Visible = True
            '    imgtourcancel.ImageUrl = "~/img/button_undocancel.png"
            '    imgtourcancel.ToolTip = "Undo Cancel"
            'Else

            '    dvTourConfirm.Visible = True
            '    divtourremarks.Visible = True
            '    divtourcancel.Visible = True
            '    divtouredit.Visible = True
            '    dvtourCancelled.Visible = False
            '    imgtourcancel.ImageUrl = "~/img/button_cancel.png"
            '    imgtourcancel.ToolTip = "Cancel"
            'End If

        End If
    End Sub
    Protected Sub btnexcelreport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexcelreport.Click
        Dim quoteid As String = hdQuoteReqestId.Value.Trim
        'Dim FolderPath As String = "..\ExcelTemplates\"
        'Dim FileName As String = "CostingQuote.xlsx"

        'Dim FilePath As String = Server.MapPath("~\ExcelTemplates\") + FileName
        'Dim RandomCls As New Random()
        'Dim RandomNo As String = RandomCls.Next(100000, 9999999).ToString

        'objclsquotecosting.GenerateExcelReport(quoteid, FilePath)
        Dim strpop As String
        Dim strpopName As String = "popup" + quoteid.Replace("/", "").ToString()
        strpop = "window.open('PrintPage.aspx?printID=bookingQuote&quoteId=" & quoteid.Trim & "');"
        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), strpopName, strpop, True)
    End Sub

    Protected Sub gvDiscountROSummary_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDiscountROSummary.RowDataBound
        If e.Row.RowType = ListItemType.Item Then
            Dim txtsystemmarkup As TextBox = CType(e.Row.FindControl("txtsystemmarkup"), TextBox)
            Dim lblnoofpax As Label = CType(e.Row.FindControl("lblpax"), Label)
            Dim lblnetprofit As Label = CType(e.Row.FindControl("lblnetprofit"), Label)
            txtsystemmarkup.Attributes.Add("onchange", "javascript:Calculateprofit('" + CType(txtsystemmarkup.ClientID, String) + "','" + CType(lblnoofpax.ClientID, String) + "','" + CType(lblnetprofit.ClientID, String) + "' )")
        End If
    End Sub

    Protected Sub btndiscountsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndiscountsave.Click
        Try
            Dim netprofit As Double = 0
            Dim adultmarkup As Double = 0, childmarkup As Double = 0, childfreemarkup As Double = 0
            Dim ds As DataSet
            ds = Session("sdsPackageSummary")

            For Each GvRow1 As GridViewRow In gvDiscountROSummary.Rows
                Dim txtsystemmarkup As TextBox = CType(GvRow1.FindControl("txtsystemmarkup"), TextBox)
                Dim lblnoofpax As Label = CType(GvRow1.FindControl("lblpax"), Label)
                Dim lblnetprofit As Label = CType(GvRow1.FindControl("lblnetprofit"), Label)
                Dim lblpaxtype As Label = CType(GvRow1.FindControl("lblpaxtype"), Label)

                adultmarkup = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                childmarkup = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                childfreemarkup = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))

                netprofit = Val(netprofit) + (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))

                If ds.Tables(2).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(2).Select().First
                    Dim dr1 As DataRow
                    If lblpaxtype.Text = "Adult" And lblnoofpax.Text > 0 Then
                        dr("adultrevisedmarkup") = IIf(Val(txtsystemmarkup.Text) = 0, "0.00", Val(txtsystemmarkup.Text))

                        If ds.Tables(5).Rows.Count > 0 Then
                            dr1 = ds.Tables(5).Select("paxtype='" & lblpaxtype.Text & "'").First

                            dr1("systemmarkup") = Val(txtsystemmarkup.Text)
                            dr1("revisedmarkup") = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                        End If
                    End If
                    If lblpaxtype.Text = "Child" And lblnoofpax.Text > 0 Then
                        dr("childrevisedmarkup") = IIf(Val(txtsystemmarkup.Text) = 0, "0.00", Val(txtsystemmarkup.Text))

                        If ds.Tables(5).Rows.Count > 0 Then
                            dr1 = ds.Tables(5).Select("paxtype='" & lblpaxtype.Text & "'").First

                            dr1("systemmarkup") = Val(txtsystemmarkup.Text)
                            dr1("revisedmarkup") = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                        End If
                    End If
                    If lblpaxtype.Text = "ChildFree" And lblnoofpax.Text > 0 Then
                        dr("childfreeuptorevisedmarkup") = IIf(Val(txtsystemmarkup.Text) = 0, "0.00", Val(txtsystemmarkup.Text))
                        If ds.Tables(5).Rows.Count > 0 Then
                            dr1 = ds.Tables(5).Select("paxtype='" & lblpaxtype.Text & "'").First
                            dr1("systemmarkup") = Val(txtsystemmarkup.Text)
                            dr1("revisedmarkup") = (Val(lblnoofpax.Text) * Val(txtsystemmarkup.Text))
                        End If
                    End If
                End If
            Next

            If ds.Tables(2).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(2).Select().First
                dr("totalrevisedmarkup") = IIf(Val(netprofit) = 0, "0.00", Val(netprofit))
                dr("totalreviseddiscount") = Val(dr("totalmarkupcurr")) - Val(dr("totalrevisedmarkup"))
            End If

            Session("sdsPackageSummary") = ds

            If ds.Tables(2).Rows.Count > 0 Then
                FillPackageFields(ds.Tables(2))
            End If

            'lblRevisedMarkupValue.Text = CType(netprofit, String)
            'lblRevisedMarkupValueBase.Text = CType(netprofit, String)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("Moreservices.aspx :: btndiscountsave_Click" & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnTestEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnTestEmail.Click
        Try
            Dim clsEmail As New clsEmail

            'changed by mohamed on 01/07/2018
            Dim lsSMTPAddress As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=2017")
            Dim lsPortNo As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_value from reservation_parameters where param_id=2017")

            Dim strfromemailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
            Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
            Dim testEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")

            ''Added shahul  27/05/2018
            Dim defaultusername As String = "", defaultpwd As String = ""
            Dim strfromusername As String = "", strfrompwd As String = ""

            defaultusername = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1504")
            defaultpwd = objclsUtilities.ExecuteQueryReturnStringValue("Select dbo.pwddecript(option_value) from reservation_parameters  where param_id=1504")
            Dim emaildt As DataTable = objclsUtilities.GetDataFromDataTable("select isnull(emailusername,'') emailusername,isnull(dbo.pwddecript(emailpwd),'') emailpwd from usermaster(nolock) where usercode='admin'")
            If emaildt.Rows.Count > 0 Then
                'strfromusername = IIf(emaildt.Rows(0)("emailusername") = "", defaultusername, emaildt.Rows(0)("emailusername"))
                'strfrompwd = IIf(emaildt.Rows(0)("emailpwd") = "", defaultpwd, emaildt.Rows(0)("emailpwd"))

                'Modified by abin on 20181212
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Then
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

            clsEmail.SendEmailOnlinenew(strfromemailid, "clienthelp@mahce.com", "mohamed@mahce.com", "test email", "test email", "", strfromusername, strfrompwd, lsSMTPAddress, lsPortNo)
        Catch ex As Exception

        End Try

    End Sub


    Sub sendemailIndividualAgent(ByVal emailstatus As String, ByVal requestid As String)
        Try

            Dim bc As clsBookingQuotePdf = New clsBookingQuotePdf()
            'Dim requestid = hdFinalReqestId.Value
            Dim objclsUtilities As New clsUtilities

            Dim ds As New DataSet
            Dim strpath1 As String = ""
            Dim bytes As Byte() = {}
            Dim ResParam As New ReservationParameters
            ResParam = Session("sobjResParam")
            Dim fileName As String = "Quote@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + requestid + "'")
            If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
            If chkCumulative.Trim() = "CUMULATIVE" Then
                'changed by mohamed on 24/09/2018
                'bc.GenerateCumulativeReport(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            Else
                bc.GenerateReportNew(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            End If
            strpath1 = Server.MapPath("~\SavedReports\") + fileName
            Session("sobjResParam") = ResParam

            'changed by mohamed on 02/07/2018 --this function is only for individual agent
            'If chkCumulative.Trim() = "CUMULATIVE" Then
            '    fileName = "quote@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            '    fileName = fileName.Replace("/", "-")
            '    Dim Itinerary As clsItineraryPdf = New clsItineraryPdf()
            '    bytes = {}
            '    Itinerary.GenerateItineraryReport(requestid, bytes, "SaveServer", fileName)
            '    If strpath1 = "" Then
            '        strpath1 = Server.MapPath("~\SavedReports\") + fileName
            '    Else
            '        strpath1 = strpath1 + ";" + Server.MapPath("~\SavedReports\") + fileName
            '    End If
            'End If


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
            'Dim guestDt As DataTable = ds.Tables(8)
            Dim contactDt As DataTable = ds.Tables(8) '9)
            'Dim BankDt As DataTable = ds.Tables(10)
            'Dim dtGuestDetails As DataTable = ds.Tables(12)


            'added param 08/08/2020
            Dim param(3) As SqlParameter
            param(0) = New SqlParameter("@bookingOrQuote", "QUOTE")
            param(1) = New SqlParameter("@bookingType", IIf(hdBookingEngineRateType.Value = "1", "CUMULATIVE", "INDIVIDUAL"))
            param(2) = New SqlParameter("@bookingCreatedBy", CType(Session("sLoginType"), String))
            param(3) = New SqlParameter("@requestId", CType(requestid, String))
            Dim dtEmailConfigure As DataTable = objclsUtilities.GetDataTable("sp_get_emailConfiguration", param)
            Dim roEmailRequired As Boolean = False
            Dim agentEmailRequired As Boolean = False
            Dim quoteCcEmailRequired As Boolean = False
            Dim salesforceEmailId As String = ""
            Dim commonEmailId As String = ""
            If dtEmailConfigure.Rows.Count > 0 Then
                roEmailRequired = CType(dtEmailConfigure(0)("roEmail"), Boolean)
                agentEmailRequired = CType(dtEmailConfigure(0)("agentEmail"), Boolean)
                quoteCcEmailRequired = CType(dtEmailConfigure(0)("CcEmail"), Boolean)
                salesforceEmailId = dtEmailConfigure(0)("salesforceEmail")
                commonEmailId = dtEmailConfigure(0)("commonEmail")
            End If

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
                    'status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmed", "On Request")
                    'changed by mohamed on 02/07/2018 as it is quote
                    status = "QUOTED"
                Else
                    'status = IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmed", "On Request")
                    'changed by mohamed on 02/07/2018 as it is quote
                    status = "QUOTED"
                End If

                If Emailmode = "Test" Then
                    'changed by mohamed on 02/07/2018 as it is quote
                    'strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmation(" & hdFinalReqestId.Value & ")", "Request(" & hdFinalReqestId.Value & ")")
                    strSubject1 = AgentName & " - TEST QUOTE"
                Else
                    'changed by mohamed on 02/07/2018 as it is quote
                    'strSubject1 = AgentName & " - " & IIf(headerDt.Rows(0)("printheader").ToString.Trim = "PROFORMA INVOICE", "Confirmation(" & hdFinalReqestId.Value & ")", "Request(" & hdFinalReqestId.Value & ")")
                    strSubject1 = AgentName & " - QUOTE"
                End If

            End If

            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                to_email = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                '   strMessage = "Dear " + contactDt.Rows(0)("salesperson") + "," + "&nbsp;<br /><br />"
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                If Session("sLoginType") = "RO" Then
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We Cancelled the attached  quotation .</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'>" + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                        If emailstatus = "Amended" Then
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the Attached revised quote as requested by You and details of the quote is as follows:</span></p>"
                        Else
                            If status = "Confirmed" Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote, details of the quote is as follows:</span></p>"
                            Else
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote, details of the quote is as follows:</span></p>"
                            End If
                        End If
                    End If
                Else
                    Dim contact As String = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You Received the  Cancelled quote from   <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> .Please check the Attached quote Ref.</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You have received the following quote from  <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> . Details of the quote is as follows:</span></p>"
                    End If
                End If
            End If

            ' strMessage += "You have received the following quote from " + CType(hdFinalReqestId.Value, String) + " and the same has been confirmed / (on request). Details of the quote is as follows:  <br /><br />"
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
                'strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Lead Guest</th> "
                'strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Confirmation Status</th>
                strMessage += " </tr> "

                For i = 0 To hotelDt.Rows.Count - 1

                    'changed by mohamed on 02/07/2018 since quote does not have quest detail
                    'strQuery = "select  top 1 title+ ' ' + firstname + ' ' + isnull(middlename,'') + ' ' + isnull(g.lastname,'') guestname   from booking_guest g(nolock),booking_hotel_detail d(nolock) ,booking_hotel_detail_confcancel c(nolock) " _
                    '    & "  where g.requestid=d.requestid  and g.rlineno=d.rlineno and  d.requestid=c.requestid and d.rlineno =c.rlineno and isnull(c.cancelled,0)=0 and g.roomno=c.roomno  " _
                    '    & "  and g.requestid='" & requestid & "' and g.rlineno =" & hotelDt.Rows(i)("rlineno") & "  and g.roomno=" & hotelDt.Rows(i)("roomno")
                    'Dim leadguest As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)

                    strMessage += "<tr> <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("partyname") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkin") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkout") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("roomdetail") + "</td>"

                    ''changed by mohamed on 02/07/2018 since quote does not have quest detail
                    'strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'></td>"
                    ''strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + leadguest + "</td>"

                    'If hotelDt.Rows(i)("hotelconfno") <> "" Then
                    '    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>YES</td>"
                    'Else
                    '    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>NO " + revertmsg + "</td>"
                    'End If

                    strMessage += "</tr>"
                Next
                strMessage += "</table>"
            End If

            'changed by mohamed on 02/07/2018 as quote does not have guest detail
            'If dtGuestDetails.Rows.Count > 0 Then
            '    strMessage = strMessage + "<br /><p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'><b>Flight Details</b></span></p> "
            '    strMessage += " <table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>No</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Guest Name</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Arrival Date</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Code</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Time</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Arrival</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Departure Date</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Code</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Flight Time</th> "
            '    strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Departure</th></tr><tr> "

            '    For i = 0 To dtGuestDetails.Rows.Count - 1

            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + (i + 1).ToString + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("GuestName") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrdate") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrflightcode") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arrflighttime") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("arairportbordername") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depdate") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depflightcode") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depflighttime") + "</td>"
            '        strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + dtGuestDetails.Rows(i)("depairportbordername") + "</td> </tr>"
            '    Next
            '    strMessage += "</table>"
            'End If





            If visaDt.Rows.Count > 0 Or othServDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                strMessage += "<br />"
                '  strMessage += "Please find the details of other services booked:" & "&nbsp;<br /><br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the details of other services booked:</span></p>"

            End If
            strMessage += "<br />"
            If visaDt.Rows.Count > 0 Then
                ''    Dim visaconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from quote_booking_visa_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(visaconfno,'') <> '' ")
                'strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>VISA            :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>VISA</span></p>"
            End If

            If othServDt.Rows.Count > 0 Then
                '' Dim trfconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from quote_booking_transfers_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(transferconfno,'') <> '' ")
                'strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TRANSFERS       :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TRANSFERS</span></p>"
            End If
            If airportDt.Rows.Count > 0 Then
                ''  Dim airconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from quote_booking_airportmate_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(airportmateconfno,'') <> '' ")
                'strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>AIRPORT SERVICES:" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>AIRPORT SERVICES</span></p>"
            End If
            If tourDt.Rows.Count > 0 Then
                '' Dim tourconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from quote_booking_tours_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(toursconfno,'') <> '' ")
                'strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TOURS           :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TOURS</span></p>"
            End If
            If OtherDt.Rows.Count > 0 Then
                ''  Dim tourconfstatus As String = objclsUtilities.ExecuteQueryReturnStringValue("Select 't' from quote_booking_others_confcancel(nolock)  where requestid='" & hdFinalReqestId.Value & "' and isnull(cancelled,0)=0 and isnull(othersconfno,'') <> '' ")
                'strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>OTHER SERVICES  :" + IIf(status.ToString.ToUpper.Trim = "CONFIRMED", " YES", " NO (If Subject to confirm within " + timelimit + " Hrs )") + "</span></p>"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>OTHER SERVICES</span></p>"
            End If

            strMessage += "  <br /><br />"
            '  strMessage += "<br />Best Regards,<br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Best Regards,</span></p>"
            ' strMessage += "<br />AgentOnlineCommon software admin team<br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Mahce software admin team</span></p>"

            Dim ccemails As String = ""
            If divcode = "01" Then
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentOnlineCommon
            Else
                ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentOnlineCommon1
            End If

            'added param on 08/08/2020
            If roEmailRequired = False Then
                to_email = ""
            End If
            If agentEmailRequired = False Then
                agentemail = ""
            End If
            If quoteCcEmailRequired = False Then
                ccemails = ""
            End If
            If salesforceEmailId <> "" Then
                If ccemails <> "" Then
                    ccemails = ccemails + "," + salesforceEmailId
                Else
                    ccemails = salesforceEmailId
                End If
            End If
            If commonEmailId <> "" Then
                If ccemails <> "" Then
                    ccemails = ccemails + "," + commonEmailId
                Else
                    ccemails = commonEmailId
                End If
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
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Then
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

            If Session("sLoginType") <> "RO" Then
                If objclsUtilities.fn_NeedToSendHotelAgentEmailToReservationUser() = True Then 'changed by mohamed on 28/06/2018
                    agentemail = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2016")
                End If

                If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, agentemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                Else
                    objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
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

                If clsEmail.SendEmailOnlinenew(strfromemailid, agentemail, to_email, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                    objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_AGENT")
                Else
                    objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_AGENT")
                End If
            End If



        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("GuestPagenew.aspx :: Sendemail :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    'Added param 22/10/2018
   Protected Sub sendemailQuoteToRo(ByVal emailstatus As String, ByVal requestid As String)
        Try

            Dim bc As clsBookingQuotePdf = New clsBookingQuotePdf()
            Dim objclsUtilities As New clsUtilities

            Dim ds As New DataSet
            Dim strpath1 As String = ""
            Dim bytes As Byte() = {}
            Dim ResParam As New ReservationParameters
            ResParam = Session("sobjResParam")
            Dim fileName As String = "Quote@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            fileName = fileName.Replace("/", "-")
            Dim chkCumulative As String = objclsUtilities.ExecuteQueryReturnStringValue("select bookingengineratetype from quote_booking_header H inner join agentmast A on H.agentcode=A.agentcode and H.requestid='" + requestid + "'")
            If String.IsNullOrEmpty(chkCumulative) Then chkCumulative = ""
            If chkCumulative.Trim() = "CUMULATIVE" Then
                'changed by mohamed on 24/09/2018
                'bc.GenerateCumulativeReport(requestid, bytes, ds, "SaveServer", ResParam, fileName)
            Else
                bc.GenerateReportNew(requestid, bytes, ds, "SaveServer", ResParam, fileName)
                'bc.GenerateReport(requestid, bytes, ds, "SaveServer", ResParam, fileName) 'changed by mohamed on 12/11/2018 as per common project
            End If
            strpath1 = Server.MapPath("~\SavedReports\") + fileName
            Session("sobjResParam") = ResParam

            ''' Email Formatting

            Dim headerDt As DataTable = ds.Tables(0)
            Dim hotelDt As DataTable = ds.Tables(1)
            Dim tariffDt As DataTable = ds.Tables(2)
            Dim othServDt As DataTable = ds.Tables(3)
            Dim airportDt As DataTable = ds.Tables(4)
            Dim visaDt As DataTable = ds.Tables(5)
            Dim tourDt As DataTable = ds.Tables(6)
            Dim OtherDt As DataTable = ds.Tables(7)
            Dim contactDt As DataTable = ds.Tables(8)

            'added param 08/08/2020
            Dim param(3) As SqlParameter
            param(0) = New SqlParameter("@bookingOrQuote", "QUOTE")
            param(1) = New SqlParameter("@bookingType", IIf(hdBookingEngineRateType.Value = "1", "CUMULATIVE", "INDIVIDUAL"))
            param(2) = New SqlParameter("@bookingCreatedBy", CType(Session("sLoginType"), String))
            param(3) = New SqlParameter("@requestId", CType(requestid, String))
            Dim dtEmailConfigure As DataTable = objclsUtilities.GetDataTable("sp_get_emailConfiguration", param)
            Dim roEmailRequired As Boolean = False
            Dim agentEmailRequired As Boolean = False
            Dim quoteCcEmailRequired As Boolean = False
            Dim salesforceEmailId As String = ""
            Dim commonEmailId As String = ""
            If dtEmailConfigure.Rows.Count > 0 Then
                roEmailRequired = CType(dtEmailConfigure(0)("roEmail"), Boolean)
                agentEmailRequired = CType(dtEmailConfigure(0)("agentEmail"), Boolean)
                quoteCcEmailRequired = CType(dtEmailConfigure(0)("CcEmail"), Boolean)
                salesforceEmailId = dtEmailConfigure(0)("salesforceEmail")
                commonEmailId = dtEmailConfigure(0)("commonEmail")
            End If

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
                    status = "QUOTED"
                Else
                    status = "QUOTED"
                End If

                If Emailmode = "Test" Then
                    strSubject1 = AgentName & " - TEST QUOTE"
                Else
                    strSubject1 = AgentName & " - QUOTE"
                End If

            End If

            ''Added shahul  27/05/2018
            Dim salesperson As String = ""
            If contactDt.Rows.Count > 0 Then
                to_email = IIf(contactDt.Rows(0)("salesemail").ToString = "", "", contactDt.Rows(0)("salesemail"))
                '   strMessage = "Dear " + contactDt.Rows(0)("salesperson") + "," + "&nbsp;<br /><br />"
                strfromemailid = IIf(contactDt.Rows(0)("salesemail").ToString = "", strfromemailid, contactDt.Rows(0)("salesemail"))
                ''Added shahul  27/05/2018
                salesperson = contactDt.Rows(0)("salesperson")
                If Session("sLoginType") = "RO" Then
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We Cancelled the attached  quotation .</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'>" + agentcontact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"

                        If emailstatus = "Amended" Then
                            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the Attached revised quote as requested by You and details of the quote is as follows:</span></p>"
                        Else
                            If status = "Confirmed" Then
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote, details of the quote is as follows:</span></p>"
                            Else
                                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>We made the following quote, details of the quote is as follows:</span></p>"
                            End If
                        End If
                    End If
                Else
                    Dim contact As String = objclsUtilities.ExecuteQueryReturnStringValue("Select username from usermaster where usercode='" & contactDt.Rows(0)("salesperson") & "'")
                    If bookingvalue = "0.00" Then
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You Received the  Cancelled quote from   <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> .Please check the Attached quote Ref.</span></p>"
                    Else
                        strMessage = "<center> <p class='MsoNormal' style='text-align: left;'><span style='font-family: calibri, sans-serif; color: #1B4F72; font-size: 12pt;'>Dear </span><span style='font-family: calibri, sans-serif; color: #ff7200; font-size: 12pt;'> " + contact + ",</span><span style='font-family:calibri,sans-serif;color:#1B4F72'><o:p /></span><br /></p><p class='MsoNormal'><b><span style='font-family:calibri,sans-serif;color:#002060'><o:p /></span></b></p></center>"
                        strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>You have received the following quote from  <span style='font-weight: bold;'>" + CType(agentuser, String) + "  </span> and the same has been <span style='font-weight: bold;'>" & status & " </span> . Details of the quote is as follows:</span></p>"
                    End If
                End If
            End If

            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agent Name : " + AgentName + "</span></p>"
            strMessage = strMessage + "<p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Agency Reference Number : " + agentref + "</span></p> <p class='MsoNormal' style='margin: 0'><font face='Calibri,sans-serif'>&nbsp;</font></p>"

            If hotelDt.Rows.Count > 0 Then

                strMessage += " <br /><table style='font-family: calibri, sans-serif;border-collapse;color: #1B4F72;'><tr> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Hotel Name</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check In</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Check Out</th> "
                strMessage += " <th style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>Meal Plan</th> "
                strMessage += " </tr> "

                For i = 0 To hotelDt.Rows.Count - 1
                    strMessage += "<tr> <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("partyname") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkin") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("checkout") + "</td>"
                    strMessage += " <td style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>" + hotelDt.Rows(i)("roomdetail") + "</td>"
                    strMessage += "</tr>"
                Next
                strMessage += "</table>"
            End If

            If visaDt.Rows.Count > 0 Or othServDt.Rows.Count > 0 Or airportDt.Rows.Count > 0 Or tourDt.Rows.Count > 0 Or OtherDt.Rows.Count > 0 Then
                strMessage += "<br />"
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Please find the details of other services booked:</span></p>"

            End If
            strMessage += "<br />"
            If visaDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>VISA</span></p>"
            End If

            If othServDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TRANSFERS</span></p>"
            End If
            If airportDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>AIRPORT SERVICES</span></p>"
            End If
            If tourDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>TOURS</span></p>"
            End If
            If OtherDt.Rows.Count > 0 Then
                strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>OTHER SERVICES</span></p>"
            End If

            strMessage += "  <br /><br />"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Best Regards,</span></p>"
            strMessage += "<p class='MsoNormal' style='margin: 0'><br /></p> <p class='MsoNormal' style='margin: 0'><span style='font-family:calibri,sans-serif;color:#1B4F72'>Mahce software admin team</span></p>"

            Dim ccemails As String = ""
            'Hide Param 21/10/2018
            'If divcode = "01" Then
            '    ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1201")  ''' AgentOnlineCommon
            'Else
            '    ccemails = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=1202") ''' AgentOnlineCommon1
            'End If

            If Session("sLoginType") = "RO" Then
                Dim lsROEmailId As String
                lsROEmailId = objclsUtilities.ExecuteQueryReturnStringValue("select usemail from usermaster (nolock) where usercode='" & Session("GlobalUserName") & "'")
                agentemail = IIf(lsROEmailId.Trim = "", "", lsROEmailId.Trim)
            End If

            'added param on 08/08/2020
            If roEmailRequired = False Then
                to_email = ""
            End If
            If agentEmailRequired = False Then
                agentemail = ""
            End If
            If quoteCcEmailRequired = False Then
                ccemails = ""
            End If
            If salesforceEmailId <> "" Then
                If ccemails <> "" Then
                    ccemails = ccemails + "," + salesforceEmailId
                Else
                    ccemails = salesforceEmailId
                End If
            End If
            If commonEmailId <> "" Then
                If ccemails <> "" Then
                    ccemails = ccemails + "," + commonEmailId
                Else
                    ccemails = commonEmailId
                End If
            End If

            If agentemail = "" Then
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else
                    'agentemail = agentemail
                    agentemail = ccemails
                    to_email = IIf(to_email = "", strfromemailid, to_email)
                End If
            Else
                If Emailmode = "Test" Then
                    agentemail = testEmail
                    to_email = testEmail
                Else

                    'agentemail = agentemail
                    agentemail = ccemails
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
                If emaildt.Rows(0)("emailusername") = "" Or emaildt.Rows(0)("emailpwd") = "" Then
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

            If clsEmail.SendEmailOnlinenew(strfromemailid, to_email, agentemail, strSubject1, strMessage, strpath1, strfromusername, strfrompwd, lsSMTPAddress, lsPortNo) Then
                objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "Y", requestid, "EMAIL_TO_RO")
            Else
                objclsUtilities.SendEmailNotification("QUOTE", strfromemailid, to_email, agentemail, "", strSubject1, strMessage, "1", "1", strpath1, "N", requestid, "EMAIL_TO_RO")
            End If

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: Sendemail :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

 

    Private Sub BindOneTimePayCharges(rLineNo As String, lblExtraBedPrice As Label, lblFunctionType As Label, dvOneTimePay As HtmlGenericControl)
        Dim dtOneTimePay As DataTable = objclsUtilities.GetDataFromDataTable("select * from booking_hotel_onetime_pay_temp where RequestId='" & Session("sRequestId") & "' and Rlineno='" & rLineNo & "'")
        If dtOneTimePay.Rows.Count > 0 Then
            lblFunctionType.Text = dtOneTimePay.Rows(0)("FunctionType").ToString
            lblExtraBedPrice.Text = dtOneTimePay.Rows(0)("Unit").ToString + " x " + dtOneTimePay.Rows(0)("UnitPrice").ToString + "= " + dtOneTimePay.Rows(0)("TotalPrice").ToString
            dvOneTimePay.Visible = True
        Else
            dvOneTimePay.Visible = False
        End If
    End Sub


    Protected Sub btnItinerary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnItinerary.Click
        Try
            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@requestId", CType(Session("sRequestId"), String))
            Dim dtItinerary As DataTable = objclsUtilities.GetDataTable("sp_get_booking_Allservices_SequenceNo", param)
            gvItinerary.DataSource = dtItinerary
            gvItinerary.DataBind()
            btnEditItineraryOrder.Value = "Edit"
            mpIdineraryOrder.Show()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnItinerary_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub gvItinerary_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvItinerary.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblDayNoSeq As Label = e.Row.FindControl("lblDayNoSeq")
                Dim txtDayNoSeq As TextBox = e.Row.FindControl("txtDayNoSeq")
                If lblDayNoSeq.Text.Contains("/") Then
                    txtDayNoSeq.Text = ""
                    txtDayNoSeq.Text = lblDayNoSeq.Text.Substring(lblDayNoSeq.Text.IndexOf("/") + 1)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: gvItinerary_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnEditItineraryOrder_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditItineraryOrder.ServerClick
        Try
            If btnEditItineraryOrder.Value = "Edit" Then
                btnEditItineraryOrder.Value = "Cancel Edit"
                For Each dlItem As GridViewRow In gvItinerary.Rows
                    If dlItem.RowType = DataControlRowType.DataRow Then
                        Dim txtDayNoSeq As TextBox = CType(dlItem.FindControl("txtDayNoSeq"), TextBox)
                        Dim lblServiceType As Label = CType(dlItem.FindControl("lblServiceType"), Label)
                        If dlItem.Visible = True And lblServiceType.Text.Trim <> "Visa" Then
                            txtDayNoSeq.Visible = True
                        End If
                    End If
                Next
            Else
                btnEditItineraryOrder.Value = "Edit"
                For Each dlItem As GridViewRow In gvItinerary.Rows
                    If dlItem.RowType = DataControlRowType.DataRow Then
                        Dim lblDayNoSeq As Label = dlItem.FindControl("lblDayNoSeq")
                        Dim txtDayNoSeq As TextBox = dlItem.FindControl("txtDayNoSeq")
                        If lblDayNoSeq.Text.Contains("/") Then
                            txtDayNoSeq.Text = ""
                            txtDayNoSeq.Text = lblDayNoSeq.Text.Substring(lblDayNoSeq.Text.IndexOf("/") + 1)
                        End If
                        txtDayNoSeq.Visible = False
                    End If
                Next
            End If
            mpIdineraryOrder.Show()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnEditItineraryOrder_ServerClick :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnSaveItineraryOrder_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveItineraryOrder.ServerClick
        Try
            Dim lsDaySeqNo As String = ""
            Dim excludeServices As String = ""
            For Each gvr As GridViewRow In gvItinerary.Rows
                Dim lblServiceType As Label = gvr.FindControl("lblServiceType")
                Dim lblRlineno As Label = gvr.FindControl("lblRlineno")
                Dim txtDayNoSeq As TextBox = gvr.FindControl("txtDayNoSeq")
                Dim chkExcludeServ As CheckBox = gvr.FindControl("chkExcludeServ")
                If gvr.Visible = True Then
                    lsDaySeqNo += IIf(lsDaySeqNo.Trim = "", "", ";") & lblServiceType.Text & "," & lblRlineno.Text & "," & Val(txtDayNoSeq.Text)
                End If
                If chkExcludeServ.Checked = True Then
                    excludeServices += IIf(excludeServices.Trim = "", "", ";") & lblServiceType.Text & "," & lblRlineno.Text
                End If
            Next
            Dim paramSave As New List(Of SqlParameter)
            paramSave.Add(New SqlParameter("@requestId", CType(Session("sRequestId"), String)))
            paramSave.Add(New SqlParameter("@dayseqorder", CType(lsDaySeqNo, String)))
            paramSave.Add(New SqlParameter("@adduser", CType(Session("GlobalUserName"), String)))
            paramSave.Add(New SqlParameter("@excludeServices", CType(excludeServices.Trim, String)))
            objclsUtilities.ExecuteNonQuery_Param("SP_Insert_booking_ItineraryOrdertemp", paramSave)

            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@requestId", CType(Session("sRequestId"), String))
            Dim dtItinerary As DataTable = objclsUtilities.GetDataTable("sp_get_booking_Allservices_SequenceNo", param)
            gvItinerary.DataSource = dtItinerary
            gvItinerary.DataBind()
            btnEditItineraryOrder.Value = "Edit"
            MessageBox.ShowMessage(Page, MessageType.Success, "Itinerary updated successfully")
            mpIdineraryOrder.Show()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("MoreServices.aspx :: btnSaveItineraryOrder_ServerClick :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Protected Sub btnConnectHotelCancelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConnectHotelCancelSave.Click
        Try
            If txtConnectHotelRlineNo.Text.Trim <> "" And gvConnectHotelCancel.Rows.Count > 0 Then
                objBLLguest.GBRequestid = GetExistingRequestId()
                objBLLguest.GBRlineno = txtConnectHotelRlineNo.Text
                Dim strbuffer As New Text.StringBuilder
                For Each gvrow As GridViewRow In gvConnectHotelCancel.Rows
                    Dim lbrLineNo As Label = CType(gvrow.FindControl("lbrLineNo"), Label)
                    Dim lblNoOfRooms As Label = CType(gvrow.FindControl("lblNoOfRooms"), Label)
                    Dim txtHotelCancelDate As TextBox = CType(gvrow.FindControl("txtHotelCancelDate"), TextBox)
                    Dim hdnHotelCancelDate As HiddenField = CType(gvrow.FindControl("hdnHotelCancelDate"), HiddenField)

                    strbuffer.Append("<DocumentElement>")
                    strbuffer.Append("<Table>")
                    strbuffer.Append("<rlineno>" & lbrLineNo.Text & "</rlineno>")
                    strbuffer.Append("<roomno>" & lblNoOfRooms.Text & "</roomno>") '.Split(" ")(1)
                    strbuffer.Append("<cancelled>1</cancelled>")
                    strbuffer.Append("<hotelcancelno></hotelcancelno>")
                    strbuffer.Append("<cancelby>" & Session("GlobalUserName") & "</cancelby>")
                    strbuffer.Append("<canceldate>" & Format(CType(hdnHotelCancelDate.Value, Date), "yyyy/MM/dd").ToString & "</canceldate>")

                    If Session("sEditRequestId") Is Nothing Then
                        strbuffer.Append("<amended>0</amended>")
                    Else
                        strbuffer.Append("<amended>1</amended>")
                    End If

                    strbuffer.Append("</Table>")
                    strbuffer.Append("</DocumentElement>")

                Next

                objBLLguest.CBCancelXml = strbuffer.ToString
                objBLLguest.GBuserlogged = Session("GlobalUserName")

                If CType(Session("cancelRatePlanSource"), String) <> "" And CType(Session("cancelRatePlanSource"), String) <> "COLUMBUS" Then   'ONEDMC
                    objBLLguest.CBratePlanSource = CType(Session("cancelRatePlanSource"), String)
                Else
                    objBLLguest.CBratePlanSource = ""
                End If

                Dim resultDt As DataTable = CType(Session("connectCancelMarkupDt"), DataTable)
                If Not resultDt Is Nothing Then
                    resultDt.TableName = "Table"
                    Dim connectMarkupXml As String = objclsUtilities.GenerateXML_FromDataTable(resultDt)
                    If resultDt.Rows.Count > 0 Then
                        objBLLguest.CBconnectMarkupXml = connectMarkupXml
                    Else
                        objBLLguest.CBconnectMarkupXml = Nothing
                    End If
                Else
                    objBLLguest.CBconnectMarkupXml = Nothing
                End If

                If objBLLguest.SavingCancelConnectivityBookingDetailsInTemp() Then
                    If CType(Session("CancelAndAmendRatePlanSource"), String).ToUpper <> "" Then  'ONEDMC
                        If Session("sFinalBooked") Is Nothing Then
                            Response.Redirect("~\HotelSearch.aspx", False)
                        Else
                            If Session("sFinalBooked") = "1" Then
                                Response.Redirect("~\Home.aspx?Tab=0", False)
                            Else
                                Response.Redirect("~\HotelSearch.aspx", False)
                            End If
                        End If
                        Exit Sub
                    Else
                        BindBookingSummary()
                        BindVisaSummary()
                        BindTourSummary()
                        BindTransferSummary()
                        BindAirportserviceSummary()
                        BindOtherserviceSummary()
                        BindTotalValue()

                        MessageBox.ShowMessage(Page, MessageType.Success, "Cancelled Sucessfully...")

                    End If
                End If
            Else
                mpConnectHotelCancel.Show()
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("MoreServices.aspx ::btnConnectHotelCancelSave_Click:: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
End Class


