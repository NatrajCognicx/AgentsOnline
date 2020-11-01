Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Linq
Partial Class HotelFreeFormBooking_New
    Inherits System.Web.UI.Page

    Dim objBLLMenu As New BLLMenu
    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLHotelSearch As New BLLHotelSearch
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Private strTotalValueHeading As String = ""
    Dim objResParam As New ReservationParameters

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try


                Session("State") = "New"
                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
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

                If Not Session("sFinalBooked") Is Nothing Then
                    clearallBookingSessions()
                    Session("sFinalBooked") = Nothing '"0" 'changed by mohamed on 12/08/2018

                End If

                dvPriceDetails.Visible = False
                pnlSpecialEvents.Attributes.Add("style", "display:none")

                hdHotelAvailableForShifting.Value = "0"

                LoadHome()

                'changed by mohamed on 11/04/2018 --keept is as last
                Dim lsStrScriptShift As String = "javascript: fnLockControlsForShifting();"
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", lsStrScriptShift, True)

            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub


    ''' <summary>
    ''' LoadHome
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadHome()

        LoadFooter()
        hdChildAgeLimit.Value = objResParam.ChildAgeLimit
        hdMaxNoOfNight.Value = objResParam.NoOfNightLimit
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        LoadRoomAdultChild()
        LoadFields()

        ShowMyBooking()
        If Not Request.QueryString("RLineNo") Is Nothing Then
            ViewState("vRLineNo") = Request.QueryString("RLineNo")
            hdHotelTabFreeze.Value = "1"
        End If

        BindDetailsForNewOrEditMode()



        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If


    End Sub

    Protected Sub ddlRoom_Dynamic_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRoom_Dynamic.SelectedIndexChanged

        Dim dtAdultChilds = New DataTable
        dtAdultChilds.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildAges", GetType(String)))
        Dim a As Integer = 1
        For Each ChildAgesitems In dlNofoRooms.Items
            Dim row As DataRow = dtAdultChilds.NewRow()
            Dim ddlDynRoomAdult As DropDownList = CType(ChildAgesitems.FindControl("ddlDynRoomAdult"), DropDownList)
            Dim ddlDynRoomChild As DropDownList = CType(ChildAgesitems.FindControl("ddlDynRoomChild"), DropDownList)

            row("colAdultLbl") = (a).ToString
            row("colChildLbl") = (a).ToString
            row("colAdultSelectNo") = ddlDynRoomAdult.Text
            row("colChildSelectNo") = ddlDynRoomChild.Text
            dtAdultChilds.Rows.Add(row)
            dtAdultChilds = GetChildAges(dtAdultChilds, (a - 1).ToString)

        Next
        Session("dtAdultChilds") = dtAdultChilds
        dlNofoRooms.DataSource = dtAdultChilds
        dlNofoRooms.DataBind()
        Dim intNoAdults As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=54")
        Dim intNoChilds As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=55")


        If Session("dtAdultChilds") Is Nothing Then
            If Val(ddlRoom_Dynamic.Text) = 0 Then
                Exit Sub
            End If
            For i As Integer = 0 To Val(ddlRoom_Dynamic.Text) - 1
                Dim row As DataRow = dtAdultChilds.NewRow()
                row("colAdultLbl") = (i + 1).ToString
                row("colChildLbl") = (i + 1).ToString
                row("colAdultSelectNo") = ""
                row("colChildSelectNo") = ""

                dtAdultChilds.Rows.Add(row)
            Next
            Session("dtAdultChilds") = dtAdultChilds
            dlNofoRooms.DataSource = dtAdultChilds
            dlNofoRooms.DataBind()
        Else
            dtAdultChilds = Session("dtAdultChilds")
            Dim dtAdultChilds_1 = New DataTable
            dtAdultChilds_1.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))
            dtAdultChilds_1.Columns.Add(New DataColumn("colChildAges", GetType(String)))

            For i As Integer = 0 To Val(ddlRoom_Dynamic.Text) - 1
                Dim row As DataRow = dtAdultChilds_1.NewRow()
                If (i + 1) > dtAdultChilds.Rows.Count Then
                    row("colAdultLbl") = (i + 1).ToString
                    row("colChildLbl") = (i + 1).ToString
                    row("colAdultSelectNo") = "2" '*** Danny 04/09/2018
                    row("colChildSelectNo") = ""
                    row("colChildAges") = ""
                Else
                    row("colAdultLbl") = dtAdultChilds.Rows(i)("colAdultLbl").ToString
                    row("colChildLbl") = dtAdultChilds.Rows(i)("colChildLbl").ToString
                    row("colAdultSelectNo") = dtAdultChilds.Rows(i)("colAdultSelectNo").ToString
                    row("colChildSelectNo") = dtAdultChilds.Rows(i)("colChildSelectNo").ToString
                    row("colChildAges") = dtAdultChilds.Rows(i)("colChildAges").ToString
                End If

                dtAdultChilds_1.Rows.Add(row)
            Next
            'Session("dtAdultChilds") = dtAdultChilds_1

            dlNofoRooms.DataSource = dtAdultChilds_1
            dlNofoRooms.DataBind()
        End If
        If dlNofoRooms.Items.Count > 0 Then
            Dim i As Integer = 0
            'Dim a As Integer = 0
            For Each dlItem1 As DataListItem In dlNofoRooms.Items
                Dim ddlDynRoomAdult As DropDownList = CType(dlItem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlItem1.FindControl("ddlDynRoomChild"), DropDownList)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomAdult, intNoAdults)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomChild, intNoChilds)

                'If dtAdultChilds.Rows(i)("colAdultSelectNo").ToString.Trim.Length > 0 Then
                If i <= dtAdultChilds.Rows.Count - 1 Then
                    ddlDynRoomAdult.Text = dtAdultChilds.Rows(i)("colAdultSelectNo").ToString
                    ddlDynRoomChild.Text = dtAdultChilds.Rows(i)("colChildSelectNo").ToString

                    If Val(ddlDynRoomChild.Text) > 0 Then
                        'If dtAdultChilds.Rows(i)("colChildAges").ToString.Trim.Length > 0 Then
                        Dim strroomstring As String() = dtAdultChilds.Rows(i)("colChildAges").ToString.Split("|")
                        Dim dtChildAge = New DataTable
                        dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
                        dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
                        dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
                        Dim ddlDynChildAges As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)

                        For j = 0 To strroomstring.Length - 1
                            Dim row2 As DataRow = dtChildAge.NewRow()
                            If strroomstring(j).ToString.Trim.Length > 0 Then
                                If j = 0 Then
                                    row2("colChildAgeLbl") = "Room " + strroomstring(j).ToString + " Child Age"
                                Else
                                    row2("colChildAgeLbl") = (j + 1).ToString
                                End If
                                row2("colChildAge") = IIf(Val(strroomstring(j)) = 0, "", strroomstring(j))
                                row2("colCHNo") = "CH" + (j + 1).ToString
                                dtChildAge.Rows.Add(row2)
                            Else
                                If Val(ddlDynRoomChild.Text.Trim) > 0 Then
                                    If j = 0 Then
                                        row2("colChildAgeLbl") = "Room " + strroomstring(j).ToString + " Child Age"
                                    Else
                                        row2("colChildAgeLbl") = (j + 1).ToString
                                    End If
                                    row2("colChildAge") = IIf(Val(strroomstring(j)) = 0, "", strroomstring(j))
                                    row2("colCHNo") = "CH" + (j + 1).ToString
                                    dtChildAge.Rows.Add(row2)
                                End If
                            End If

                        Next
                        ddlDynChildAges.DataSource = dtChildAge
                        ddlDynChildAges.DataBind()
                        'End If


                        'Dim strroom As String
                        'Dim strchildage As String
                        'If strroomstring.Length > 0 Then





                        'For Each dlRoomtem1 As DataListItem In dlNofoRooms.Items
                        'strroom = strroomstring(a).ToString()
                        'Dim ddlDynRoomAdult As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomAdult"), DropDownList)
                        'Dim ddlDynRoomChild As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomChild"), DropDownList)


                        'ddlDynRoomAdult.SelectedIndex = strroom(1)
                        'ddlDynRoomChild.SelectedIndex = strroom(2)

                        'strchildage = strroom.ToString()



                        'a = a + 1
                        'Next
                        'End If
                    End If

                Else '*** Danny 04/09/2018>>>>>>>>>>>>>>>>>>>>>
                    ddlDynRoomAdult.Text = "2"
                End If
                '*** Danny 04/09/2018<<<<<<<<<<<<<<<<<<<<<<<<

                'End If
                i = i + 1
            Next

        End If
        Session("dtAdultChilds") = dlNofoRooms.DataSource
        ''Added by abin on 20190714
        'If ddlRoom_Dynamic.SelectedValue > 5 Then
        '    ddlAvailability.SelectedValue = "1"
        'Else
        '    ddlAvailability.SelectedValue = "2"
        'End If

    End Sub
    Private Sub DynamicRoomCreate()
        Dim dtAdultChilds = New DataTable

        dtAdultChilds.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))

        dlNofoRooms.DataSource = dtAdultChilds
        dlNofoRooms.DataBind()
        If Val(ddlRoom_Dynamic.Text) = 0 Then
            Exit Sub
        End If
        For i As Integer = 0 To Val(ddlRoom_Dynamic.Text) - 1
            Dim row As DataRow = dtAdultChilds.NewRow()
            row("colAdultLbl") = (i + 1).ToString
            row("colChildLbl") = (i + 1).ToString
            row("colAdultSelectNo") = ""
            row("colChildSelectNo") = ""

            dtAdultChilds.Rows.Add(row)
        Next

        dlNofoRooms.DataSource = dtAdultChilds
        dlNofoRooms.DataBind()

        Dim intNoAdults As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=54")
        Dim intNoChilds As String = objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=55")


        If dlNofoRooms.Items.Count > 0 Then
            For Each dlItem1 As DataListItem In dlNofoRooms.Items
                Dim ddlDynRoomAdult As DropDownList = CType(dlItem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlItem1.FindControl("ddlDynRoomChild"), DropDownList)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomAdult, intNoAdults)
                objclsUtilities.FillDropDownListBasedOnNumber(ddlDynRoomChild, intNoChilds)
            Next

        End If


    End Sub

    Public Sub ChildAgeTxtCreate(ByVal sender As Object, ByVal e As System.EventArgs)



        Dim dtAdultChilds = New DataTable
        dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)

        Dim dtChildAge = New DataTable
        dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
        dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
        dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
        Dim dDLCh As DropDownList = sender
        Dim strRomeNo As String = dDLCh.ClientID.Replace("dlNofoRooms_ctl", "").Replace("_ddlDynRoomChild", "")
        Dim dDLAd As DropDownList = CType(dlNofoRooms.Items(strRomeNo).FindControl("ddlDynRoomAdult"), DropDownList)

        dtAdultChilds = GetChildAges(dtAdultChilds, strRomeNo)

        If dDLAd.SelectedItem.Text.Trim().Replace("--", "").Length = 0 Then
            dDLCh.SelectedIndex = 0
            Exit Sub
        End If
        'Dim s As String = dDLCh.SelectedValue()
        dtAdultChilds.Rows(strRomeNo)("colChildSelectNo") = dDLCh.SelectedValue()

        Dim colAdultSelectNo As String() = dtAdultChilds.Rows(Val(strRomeNo))("colChildAges").ToString.Split("|")
        For i As Integer = 0 To Val(sender.SelectedValue()) - 1
            Dim row As DataRow = dtChildAge.NewRow()
            If i = 0 Then
                row("colChildAgeLbl") = "Room " + (Val(strRomeNo) + 1).ToString + " Child Age"
            Else
                row("colChildAgeLbl") = (i + 1).ToString
            End If
            If dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString().Trim.Length = 0 Then
                row("colChildAge") = ""
            Else
                If i < colAdultSelectNo.Length Then
                    row("colChildAge") = colAdultSelectNo(i)
                Else
                    row("colChildAge") = ""
                End If

            End If
            row("colCHNo") = "CH" + (i + 1).ToString
            dtChildAge.Rows.Add(row)
        Next
        Dim dlItem1 As DataListItem = dlNofoRooms.Items(Val(strRomeNo))
        Dim ddlDynRoomAdult As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)

        'Session("dtChildAge") = dtChildAge
        ddlDynRoomAdult.DataSource = dtChildAge
        ddlDynRoomAdult.DataBind()

        Session("dtAdultChilds") = dtAdultChilds
        'Dim ss As String = dlNofoRooms.Items(0).ToString()
        'Dim dt As DataList = CType(e.FindControl("datalist2"), DataList)

    End Sub

    Function GetChildAges(ByVal dtAdultChilds As DataTable, ByVal strRomeNo As String) As DataTable
        Dim dldlChildAges As DataList = CType(dlNofoRooms.Items(strRomeNo).FindControl("dlChildAges"), DataList)
        dtAdultChilds.Rows(strRomeNo)("colChildAges") = ""
        For Each ChildAgesitems In dldlChildAges.Items
            Dim ddlDynRoomAdult1 As System.Web.UI.WebControls.TextBox = CType(ChildAgesitems.FindControl("txtRoom1Child1"), System.Web.UI.WebControls.TextBox)
            If dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString().Trim().Length > 0 Then
                dtAdultChilds.Rows(strRomeNo)("colChildAges") = dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString + "|"
            End If
            If ddlDynRoomAdult1.Text.Trim.Length = 0 Then
                ddlDynRoomAdult1.Text = 0
            End If
            dtAdultChilds.Rows(strRomeNo)("colChildAges") = dtAdultChilds.Rows(strRomeNo)("colChildAges").ToString.Trim + ddlDynRoomAdult1.Text
        Next
        Return dtAdultChilds
    End Function

    Private Function GetRoomString(ByVal strRoomString As String) As String
        strRoomString = ""
        If dlNofoRooms.Items.Count > 0 Then
            Dim i As Integer = 1
            For Each dlItem1 As DataListItem In dlNofoRooms.Items
                If i <> 1 Then
                    strRoomString = strRoomString + ";"
                End If
                Dim ddlDynRoomAdult As DropDownList = CType(dlItem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlItem1.FindControl("ddlDynRoomChild"), DropDownList)
                If ddlDynRoomAdult.SelectedIndex = 0 Then
                    strRoomString = "Please select Room" + i.ToString() + " Adult."
                    Return strRoomString
                End If
                strRoomString = strRoomString + i.ToString
                strRoomString = strRoomString + "," + ddlDynRoomAdult.Text
                If ddlDynRoomChild.SelectedIndex > 0 Then
                    strRoomString = strRoomString + "," + ddlDynRoomChild.Text + ","
                    Dim ddlDynChildAges As DataList = CType(dlItem1.FindControl("dlChildAges"), DataList)
                    Dim j As Integer = 1
                    For Each dlItem2 As DataListItem In ddlDynChildAges.Items
                        Dim ddlDynRoomAdult1 As System.Web.UI.WebControls.TextBox = CType(dlItem2.FindControl("txtRoom1Child1"), System.Web.UI.WebControls.TextBox)
                        If Val(ddlDynRoomAdult1.Text) = 0 Then
                            strRoomString = "Please select " + " Child" + j.ToString + " Age in Room" + i.ToString()
                            Return strRoomString
                        End If
                        strRoomString = strRoomString + "|" + (Convert.ToInt32(ddlDynRoomAdult1.Text)).ToString
                        j = j + 1
                    Next
                Else
                    strRoomString = strRoomString + ",0,0"
                End If
                i = i + 1
            Next

        End If

        'strRoomString = "1," & ddlRoom1Adult.SelectedValue
        'Dim strRoom1Child = ddlRoom1Child.SelectedValue
        'If strRoom1Child = "0" Then
        '    strRoomString = strRoomString & ",0,0"
        'ElseIf strRoom1Child = "1" Then
        '    strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text)
        'ElseIf strRoom1Child = "2" Then
        '    strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text)
        'End If
        Return strRoomString.Replace(",|", ",")
    End Function

    Public Sub AdultChanges(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dtAdultChilds = New DataTable
        dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)
        Dim dDLAd As DropDownList = sender
        Dim strRomeNo As String = dDLAd.ClientID.Replace("dlNofoRooms_ctl", "").Replace("_ddlDynRoomAdult", "")
        dtAdultChilds.Rows(Val(strRomeNo))("colAdultSelectNo") = dDLAd.SelectedValue.ToString()

        dtAdultChilds = GetChildAges(dtAdultChilds, strRomeNo)
        ''Dim dDLAd As DropDownList = CType(dlNofoRooms.Items(strRomeNo).FindControl("ddlDynRoomAdult"), DropDownList)
        'If dDLAd.SelectedItem.Text.Trim().Replace("--", "").Length = 0 Then
        '    dDLAd.SelectedIndex = 0
        '    Exit Sub
        'End If
        Session("dtAdultChilds") = dtAdultChilds

    End Sub

    ''' <summary>
    '''  <LoadLogo/>
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = strLogo 'Abin on 20180710

            End If

        End If
    End Sub

    ''' <summary>
    ''' LoadMenus
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadMenus()
        Try

            ' Modified by abin on 20180717
            Dim strType As String = ""
            Dim strMenuMobHtml As New StringBuilder
            Dim strMenuHtml As String = ""
            objResParam = Session("sobjResParam")
            strMenuHtml = objBLLMenu.GetMenusReturnAstring(objResParam.LoginType, objResParam.AgentCode, objResParam.SubUserCode)

            ltMenu.Text = strMenuHtml.ToString
            dvMobmenu.InnerHtml = strMenuHtml.ToString
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
    ''' <summary>
    ''' LoadFields
    ''' </summary>
    ''' <remarks></remarks>
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


        If Not Session("sLoginType") Is Nothing Then
            hdLoginType.Value = Session("sLoginType")
            If Session("sLoginType") <> "RO" Then
                dvForRO.Visible = False

                Dim strSqlQry As String = ""
                Dim myDS As New DataSet
                Dim Hotelnames As New List(Of String)
                Try



                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a(nolock),ctrymast c(nolock) where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' order by ctryname"

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count = 1 Then
                        txtCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString

                        txtCountry.ReadOnly = True
                        AutoCompleteExtender_txtCountry.Enabled = False


                    Else
                        txtCountry.ReadOnly = False
                        AutoCompleteExtender_txtCountry.Enabled = True

                    End If


                Catch ex As Exception

                End Try
            Else
                dvForRO.Visible = True

            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub

    ''' <summary>
    ''' GetShiftHotelList
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
  <System.Web.Services.WebMethod()> _
    Public Shared Function GetShiftHotelList(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If

            'changed by mohamed on 05/04/2018
            'strSqlQry = "select d.rlineno,d.partycode,p.partyname+' '+convert(varchar(10),d.checkin,103)+' - '+convert(varchar(10),d.checkout,103) HotelName from booking_hotel_detailtemp d(nolock),partymast p(nolock) where d.requestid= '" & HttpContext.Current.Session("sRequestId").ToString & "' and d.partycode=p.partycode and  p.partyname+' '+convert(varchar(10),d.checkin,103)+'-'+convert(varchar(10),d.checkout,103) like  '%" & prefixText & "%'  order by d.rlineno"
            strSqlQry = "execute sp_get_shifting_hotel_detail '" & HttpContext.Current.Session("sRequestId").ToString & "', -1 "
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    'Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString()))
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString() + "|" + Replace(myDS.Tables(0).Rows(i)("roomstring").ToString(), "|", "&s") + "**" + myDS.Tables(0).Rows(i)("checkout").ToString() & "**" & myDS.Tables(0).Rows(i)("checkin").ToString() & "**" & myDS.Tables(0).Rows(i)("partycode").ToString() & "**" & myDS.Tables(0).Rows(i)("partyname").ToString() & "**" & myDS.Tables(0).Rows(i)("nights").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function
    ''' <summary>

    ''' <summary>
    ''' GetDeastinationList
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
  <System.Web.Services.WebMethod()> _
    Public Shared Function GetDeastinationList(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try

            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select destcode,destname,desttype from view_destination_search where destname like  '%" & prefixText & "%' "
            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("destname").ToString(), myDt.Rows(i)("destcode").ToString() + "|" + myDt.Rows(i)("desttype").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    ''' <summary>
    ''' GetHotelName
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetHotelName(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try

            Dim strDest As String = ""
            Dim strStar As String = ""
            Dim strPropType As String = ""
            If prefixText = " " Then
                prefixText = ""
            End If
            If contextKey <> "" Then
                Dim strContext As String()
                strContext = contextKey.Trim.Split("||")
                For i As Integer = 0 To strContext.Length - 1
                    If strContext(i).Contains("DC:") Then
                        strDest = strContext(i).Replace("DC:", "")
                    End If
                    If strContext(i).Contains("HSC:") Then
                        strStar = strContext(i).Replace("HSC:", "")
                    End If
                    If strContext(i).Contains("PT:") Then
                        strPropType = strContext(i).Replace("PT:", "")
                    End If
                Next


            End If

            Dim str As String = contextKey
            '  strSqlQry = "select p.partycode,p.partyname from partymast p,sectormaster s,catmast c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partyname like  '" & prefixText & "%' "
            strSqlQry = "select v.partycode,v.partyname from sectormaster(nolock) s,catmast(nolock) c,partymast(nolock) v where v.sectorcode=s.sectorcode  and v.catcode=c.catcode and v.active=1 and v.partyname like  '%" & prefixText & "%' "
            If strDest.Trim <> "" Then
                strSqlQry = strSqlQry & " and (v.citycode = '" & strDest.Trim & "' or s.sectorcode = '" & strDest.Trim & "')  "
            End If

            If strStar.Trim <> "" Then
                strSqlQry = strSqlQry & " and c.catcode = '" & strStar.Trim & "' "
            End If
            If strPropType.Trim <> "" And strPropType.Trim <> "--" Then
                strSqlQry = strSqlQry & " and v.propertytype = '" & strPropType.Trim & "' "
            End If

            strSqlQry = strSqlQry & " order by v.partyname  "

            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("partyname").ToString(), myDt.Rows(i)("partycode").ToString()))
                Next
            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function




    ''' <summary>
    ''' GetCustomers
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCustomers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast(nolock) where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
                End If
            End If


            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("agentname").ToString(), myDt.Rows(i)("agentcode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function



    ''' <summary>
    ''' GetCountry
    ''' </summary>
    ''' <param name="prefixText"></param>
    ''' <param name="count"></param>
    ''' <param name="contextKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If Not HttpContext.Current.Session("sLoginType") Is Nothing Then
                If HttpContext.Current.Session("sLoginType") = "RO" Then
                    If contextKey <> "" Then
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & contextKey.Trim & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    Else
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    End If
                Else
                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"

                End If
            Else
                strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
            End If
            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)
            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("ctryname").ToString(), myDt.Rows(i)("ctrycode").ToString()))
                Next
            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function


    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetSupplierAgent(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = "select supagentcode,supagentname from supplier_agents(nolock) where active=1 and supagentname like  '%" & prefixText & "%' "
            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("supagentname").ToString().ToUpper, myDt.Rows(i)("supagentcode").ToString()))
                Next
            End If
            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetRoomType(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If contextKey <> "" Then
                strSqlQry = "select rmtypcode+'|'+roomclasscode+'|'+ convert(varchar(2),rankord) as rmtypcode,rmtypname from partyrmtyp(nolock) where partycode='" + contextKey + "' and inactive=0 and rmtypname like  '%" & prefixText & "%' order by rankord "
            Else
                strSqlQry = "select rmtypcode+'|'+roomclasscode+'|'+ convert(varchar(2),rankord) as rmtypcode,rmtypname from partyrmtyp(nolock) where partycode='000' and inactive=0 and rmtypname like  '%" & prefixText & "%' order by rankord "
            End If

            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("rmtypname").ToString().ToUpper, myDt.Rows(i)("rmtypcode").ToString()))
                Next
            End If
            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetAccomodationType(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If contextKey <> "" Then
                strSqlQry = "SELECT partyrmcat.rmcatcode FROM rmcatmast(nolock),partyrmcat(nolock) WHERE rmcatmast.rmcatcode=partyrmcat.rmcatcode and accom_extra='A' AND allotreqd='Yes' and partycode='" + contextKey + "' and  partyrmcat.rmcatcode like  '%" & prefixText & "%' "
            Else
                strSqlQry = "SELECT partyrmcat.rmcatcode FROM rmcatmast(nolock),partyrmcat(nolock) WHERE rmcatmast.rmcatcode=partyrmcat.rmcatcode and accom_extra='A' AND allotreqd='Yes' and partycode='000' and partyrmcat.rmcatcode like  '%" & prefixText & "%' "
            End If

            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("rmcatcode").ToString().ToUpper, myDt.Rows(i)("rmcatcode").ToString()))
                Next
            End If
            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function



    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetAccomodation(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            'If prefixText = " " Then
            '    prefixText = ""
            'End If

            'GetSharedDataFromDataTable

            If contextKey <> "" Then
                strSqlQry = "exec sp_GetAccomodation '" + contextKey + "','" + prefixText + "'"
            Else
                strSqlQry = "exec sp_GetAccomodation '" + contextKey + "','" + prefixText + "'"
            End If

            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("Accomodation").ToString().ToUpper, myDt.Rows(i)("autoid").ToString()))
                Next
            End If
            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function



    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetMealPlan(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDt As New DataTable
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If contextKey <> "" Then
                strSqlQry = "select mealcode from  partymeal(nolock) where partycode='" + contextKey + "' and  mealcode like  '%" & prefixText & "%' "
            Else
                strSqlQry = "select mealcode from  partymeal(nolock) where partycode='000' and mealcode like  '%" & prefixText & "%' "
            End If

            myDt = clsUtilities.GetSharedDataFromDataTable(strSqlQry)

            If myDt.Rows.Count > 0 Then
                For i As Integer = 0 To myDt.Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDt.Rows(i)("mealcode").ToString().ToUpper, myDt.Rows(i)("mealcode").ToString()))
                Next
            End If
            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    ''' <summary>
    ''' GetHotelsDetails
    ''' </summary>
    ''' <param name="HotelCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetHotelsDetails(ByVal HotelCode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select s.destname,c.catcode,c.catname,s.destcode + '|' +case when desttype='Area' or desttype='Sector' then 'Sector' else desttype end destcode  from partymast(nolock) p,view_destination_search(nolock) s,catmast c where p.sectorcode=s.destcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
            myDS = clsUtilities.GetSharedDataFromDataSet(strSqlQry)
            myDS.DataSetName = "Customers"
            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    <WebMethod()> _
    Public Shared Function GetSectorDetailsFromPartyCode(ByVal HotelCode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select * from view_destination_search_new where  partycode= '" & HotelCode & "' order by sortorder asc "
            myDS = clsUtilities.GetSharedDataFromDataSet(strSqlQry)
            myDS.DataSetName = "Customers"
            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' GetCountryDetails
    ''' </summary>
    ''' <param name="CustCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries(nolock) a,ctrymast(nolock) c where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "'  order by ctryname"
            myDS = clsUtilities.GetSharedDataFromDataSet(strSqlQry)
            myDS.DataSetName = "Countries"
            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' GetEndDate
    ''' </summary>
    ''' *** Danny 22/07/2018
    ''' <param name="dIn"></param>
    ''' <param name="dOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetEndDate(ByVal dIn As DateTime, ByVal txtNoDay As Integer) As String
        Return dIn.AddDays(txtNoDay).ToString("dd/MM/yyyy")
    End Function

    ''' <summary>
    ''' GetNoOfNights
    ''' </summary>
    ''' <param name="dIn"></param>
    ''' <param name="dOut"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetNoOfNights(ByVal dIn As String, ByVal dOut As String) As String
        Dim strNoOfNights As String
        strNoOfNights = ""
        strNoOfNights = clsUtilities.SharedExecuteQueryReturnStringValue("select DATEDIFF(day,convert(datetime,'" & dIn & "'),convert(datetime,'" & dOut & "'))")
        Return strNoOfNights
    End Function

    ''' <summary>
    ''' GetShiftingRoomAdultChild
    ''' </summary> 
    ''' <param name="strCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetShiftingRoomAdultChild(ByVal strCode As String) As String
        Dim strCodes As String() = strCode.Split("|")
        Dim strRequestid As String = HttpContext.Current.Session("sRequestId")
        Dim strLineNo As String = strCodes(1)
        Dim strRoomString As String = ""
        'changed by mohamed on 05/04/2018
        'strRoomString = clsUtilities.SharedExecuteQueryReturnStringValue("select  RoomString +'**'+ CONVERT(varchar(10),checkout,103) as rm from booking_hotel_detailtemp(nolock)  where requestid= '" & strRequestid & "' and  rlineno='" & strLineNo & "'")
        strRoomString = Replace(strCodes(2), "&s", "|")
        Return strRoomString
    End Function

    Dim ds As DataSet


    ''' <summary>
    ''' btnSearch_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            dvPriceDetails.Visible = True
            FillddlRoomNos(ddlRoom.SelectedValue)
            BindPricesBasedOnRooms()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: btnSearch_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub FillddlRoomNos(ByVal iRoomNo As Integer)
        If ddlRoomNos.Items.Count > 0 Then
            ddlRoomNos.Items.Clear()
        End If
        For i As Integer = 1 To iRoomNo
            ddlRoomNos.Items.Add(New ListItem("Room " & i.ToString, i.ToString))
        Next
    End Sub


    Private Sub FillddlSpclRoomNos(ByVal iRoomNo As Integer)
        'If ddlSpclRoom.Items.Count > 0 Then
        '    ddlSpclRoom.Items.Clear()
        'End If
        'For i As Integer = 1 To iRoomNo
        '    ddlSpclRoom.Items.Add(New ListItem("Room " & i.ToString, i.ToString))
        'Next
    End Sub


    ''' <summary>
    ''' Page_LoadComplete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        'Dim scriptKey As String = "UniqueKeyForThisScript"
        'Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
        'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

    End Sub


    ''' <summary>
    ''' btnLogOut_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub




    ''' <summary>
    ''' LoadRoomAdultChild
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRoomAdultChild()
        FillSpecifiedAdultChild("16", "6")
        FillSpecifiedRoom("100")
    End Sub
    ''' <summary>
    ''' FillSpecifiedChildAges
    ''' </summary>
    ''' <param name="childages"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedChildAges(ByVal childages As String)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild1, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild2, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild3, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild4, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild5, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild6, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild7, childages)
        objclsUtilities.FillDropDownListWithSpecifiedAges(ddlChild8, childages)

    End Sub
    ''' <summary>
    ''' FillSpecifiedRoom
    ''' </summary>
    ''' <param name="NoOfRoom"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedRoom(ByVal NoOfRoom As String)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom_Dynamic, NoOfRoom)
        'objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom, NoOfRoom)
    End Sub
    ''' <summary>
    ''' FillSpecifiedAdultChild
    ''' </summary>
    ''' <param name="adults"></param>
    ''' <param name="child"></param>
    ''' <remarks></remarks>
    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom1Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom2Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom3Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom4Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom5Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom6Adult, adults)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom7Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom8Adult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom9Adult, adults)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom1Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom2Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom3Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom4Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom5Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom6Child, child)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom7Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom8Child, child)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlRoom9Child, child)


    End Sub

    ''' <summary>
    ''' btnMyBooking_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMyBooking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyBooking.Click
        Try
            If Not Session("sEditRequestId") Is Nothing Then

                Dim objBLLCommonFuntions As New BLLCommonFuntions
                Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
                If dt.Rows.Count > 0 Then
                    Response.Redirect("MoreServices.aspx")
                Else
                    MessageBox.ShowMessage(Page, MessageType.Info, "You have no booking.")
                End If

            Else
                If Session("sRequestId") Is Nothing Then
                    MessageBox.ShowMessage(Page, MessageType.Info, "You have no booking.")
                Else
                    Dim objBLLCommonFuntions As New BLLCommonFuntions
                    Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                    If dt.Rows.Count > 0 Then
                        Response.Redirect("MoreServices.aspx")
                    Else
                        MessageBox.ShowMessage(Page, MessageType.Info, "You have no booking.")
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx ::btnMyBooking_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    ''' <summary>
    ''' ShowMyBooking
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowMyBooking()
        If Not Session("sEditRequestId") Is Nothing Then
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sEditRequestId"))
            If dt.Rows.Count > 0 Then
                dvMybooking.Visible = True
            Else
                dvMybooking.Visible = False
            End If
        Else
            If Session("sRequestId") Is Nothing Then
                dvMybooking.Visible = False
            Else
                Dim objBLLCommonFuntions As New BLLCommonFuntions
                Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                If dt.Rows.Count > 0 Then
                    dvMybooking.Visible = True
                Else
                    dvMybooking.Visible = False
                End If
            End If
        End If

    End Sub
    ''' <summary>
    ''' CheckOldBooking
    ''' </summary>
    ''' <param name="strReqestId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckOldBooking(ByVal strReqestId As String) As Boolean
        Dim bStatus As Boolean = True
        Dim objBLLCommonFuntions As New BLLCommonFuntions
        Dim dt As DataTable = objBLLCommonFuntions.GetBookingTempHeaderDetails(strReqestId)
        If dt.Rows.Count > 0 Then
            bStatus = False
        End If
        Return bStatus
    End Function


    ' ''' <summary>
    ' ''' GetRoomString
    ' ''' </summary>
    ' ''' <param name="strRoom"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoomString(ByVal strRoom As String) As String
    '    Dim strRoomString As String = ""

    '    If strRoom <> "" Then
    '        'strRoomString = strRoom

    '        If strRoom = "1" Then
    '            strRoomString = GetRoom1String(strRoomString)

    '        ElseIf strRoom = "2" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '        ElseIf strRoom = "3" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '        ElseIf strRoom = "4" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '        ElseIf strRoom = "5" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '        ElseIf strRoom = "6" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '            strRoomString = GetRoom6String(strRoomString)
    '        ElseIf strRoom = "7" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '            strRoomString = GetRoom6String(strRoomString)
    '            strRoomString = GetRoom7String(strRoomString)
    '        ElseIf strRoom = "8" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '            strRoomString = GetRoom6String(strRoomString)
    '            strRoomString = GetRoom7String(strRoomString)
    '            strRoomString = GetRoom8String(strRoomString)
    '        ElseIf strRoom = "9" Then
    '            strRoomString = GetRoom1String(strRoomString)
    '            strRoomString = GetRoom2String(strRoomString)
    '            strRoomString = GetRoom3String(strRoomString)
    '            strRoomString = GetRoom4String(strRoomString)
    '            strRoomString = GetRoom5String(strRoomString)
    '            strRoomString = GetRoom6String(strRoomString)
    '            strRoomString = GetRoom7String(strRoomString)
    '            strRoomString = GetRoom8String(strRoomString)
    '            strRoomString = GetRoom9String(strRoomString)
    '        End If
    '    End If
    '    Return strRoomString
    'End Function

    Private Function GetRoomChildAgeString(ByVal strRoomNo As String) As String

        'Dim strRoomChildString As String = "ddlRoom" & strRoomNo & "Child"
        'Dim ddlRoomChild As DropDownList = FindControl(strRoomChildString)
        Dim strChildAgeString As String = ""

        'Dim strRoomChild = ddlRoomChild.SelectedValue


        'If strRoomChild = "0" Then
        '    strChildAgeString = "0"
        'ElseIf strRoomChild = "1" Then
        '    strChildAgeString = Val(txtRoom1Child1.Text)
        'ElseIf strRoomChild = "2" Then
        '    strChildAgeString = Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text)
        'ElseIf strRoomChild = "3" Then
        '    strChildAgeString = Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text)
        'ElseIf strRoomChild = "4" Then
        '    strChildAgeString = Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text)
        'ElseIf strRoomChild = "5" Then
        '    strChildAgeString = Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text) & "|" & Val(txtRoom1Child5.Text)
        'ElseIf strRoomChild = "6" Then
        '    strChildAgeString = Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text) & "|" & Val(txtRoom1Child5.Text) & "|" & Val(txtRoom1Child6.Text)

        'End If
        'Dim strAccomodationType As String = txtRoom1AccomodationTypeCode.Text
        'Dim strAccomodation As String = txtRoom1AccomodationType.Text
        Return strChildAgeString
    End Function

    ' ''' <summary>
    ' ''' GetRoom1String
    ' ''' </summary>
    ' ''' <param name="strRoomString"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoom1String(ByVal strRoomString As String) As String
    '    strRoomString = "1," & ddlRoom1Adult.SelectedValue
    '    Dim strRoom1Child = ddlRoom1Child.SelectedValue


    '    If strRoom1Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom1Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text)
    '    ElseIf strRoom1Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text)
    '    ElseIf strRoom1Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text)
    '    ElseIf strRoom1Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text)
    '    ElseIf strRoom1Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text) & "|" & Val(txtRoom1Child5.Text)
    '    ElseIf strRoom1Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom1Child & "," & Val(txtRoom1Child1.Text) & "|" & Val(txtRoom1Child2.Text) & "|" & Val(txtRoom1Child3.Text) & "|" & Val(txtRoom1Child4.Text) & "|" & Val(txtRoom1Child5.Text) & "|" & Val(txtRoom1Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom1AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom1AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function
    ' ''' <summary>
    ' ''' GetRoom2String
    ' ''' </summary>
    ' ''' <param name="strRoomString"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoom2String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";2," & ddlRoom2Adult.SelectedValue
    '    Dim strRoom2Child = ddlRoom2Child.SelectedValue
    '    If strRoom2Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom2Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text)
    '    ElseIf strRoom2Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text)
    '    ElseIf strRoom2Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text)
    '    ElseIf strRoom2Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text) & "|" & Val(txtRoom2Child4.Text)
    '    ElseIf strRoom2Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text) & "|" & Val(txtRoom2Child4.Text) & "|" & Val(txtRoom2Child5.Text)
    '    ElseIf strRoom2Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom2Child & "," & Val(txtRoom2Child1.Text) & "|" & Val(txtRoom2Child2.Text) & "|" & Val(txtRoom2Child3.Text) & "|" & Val(txtRoom2Child4.Text) & "|" & Val(txtRoom2Child5.Text) & "|" & Val(txtRoom2Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom2AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom2AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function
    ' ''' <summary>
    ' ''' GetRoom3String
    ' ''' </summary>
    ' ''' <param name="strRoomString"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoom3String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";3," & ddlRoom3Adult.SelectedValue
    '    Dim strRoom3Child = ddlRoom3Child.SelectedValue
    '    If strRoom3Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom3Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text)
    '    ElseIf strRoom3Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text)
    '    ElseIf strRoom3Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text)
    '    ElseIf strRoom3Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text) & "|" & Val(txtRoom3Child4.Text)
    '    ElseIf strRoom3Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text) & "|" & Val(txtRoom3Child4.Text) & "|" & Val(txtRoom3Child5.Text)
    '    ElseIf strRoom3Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom3Child & "," & Val(txtRoom3Child1.Text) & "|" & Val(txtRoom3Child2.Text) & "|" & Val(txtRoom3Child3.Text) & "|" & Val(txtRoom3Child4.Text) & "|" & Val(txtRoom3Child5.Text) & "|" & Val(txtRoom3Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom3AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom3AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function
    ' ''' <summary>
    ' ''' GetRoom4String
    ' ''' </summary>
    ' ''' <param name="strRoomString"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoom4String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";4," & ddlRoom4Adult.SelectedValue
    '    Dim strRoom4Child = ddlRoom4Child.SelectedValue
    '    If strRoom4Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom4Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text)
    '    ElseIf strRoom4Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text)
    '    ElseIf strRoom4Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text)
    '    ElseIf strRoom4Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text) & "|" & Val(txtRoom4Child4.Text)
    '    ElseIf strRoom4Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text) & "|" & Val(txtRoom4Child4.Text) & "|" & Val(txtRoom4Child5.Text)
    '    ElseIf strRoom4Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom4Child & "," & Val(txtRoom4Child1.Text) & "|" & Val(txtRoom4Child2.Text) & "|" & Val(txtRoom4Child3.Text) & "|" & Val(txtRoom4Child4.Text) & "|" & Val(txtRoom4Child5.Text) & "|" & Val(txtRoom4Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom4AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom4AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function
    ' ''' <summary>
    ' ''' GetRoom5String
    ' ''' </summary>
    ' ''' <param name="strRoomString"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoom5String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";5," & ddlRoom5Adult.SelectedValue
    '    Dim strRoom5Child = ddlRoom5Child.SelectedValue
    '    If strRoom5Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom5Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text)
    '    ElseIf strRoom5Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text)
    '    ElseIf strRoom5Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text)
    '    ElseIf strRoom5Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text) & "|" & Val(txtRoom5Child4.Text)
    '    ElseIf strRoom5Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text) & "|" & Val(txtRoom5Child4.Text) & "|" & Val(txtRoom5Child5.Text)
    '    ElseIf strRoom5Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom5Child & "," & Val(txtRoom5Child1.Text) & "|" & Val(txtRoom5Child2.Text) & "|" & Val(txtRoom5Child3.Text) & "|" & Val(txtRoom5Child4.Text) & "|" & Val(txtRoom5Child5.Text) & "|" & Val(txtRoom5Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom5AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom5AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function
    ' ''' <summary>
    ' ''' GetRoom6String
    ' ''' </summary>
    ' ''' <param name="strRoomString"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function GetRoom6String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";6," & ddlRoom6Adult.SelectedValue
    '    Dim strRoom6Child = ddlRoom6Child.SelectedValue
    '    If strRoom6Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom6Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text)
    '    ElseIf strRoom6Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text)
    '    ElseIf strRoom6Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text)
    '    ElseIf strRoom6Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text) & "|" & Val(txtRoom6Child4.Text)
    '    ElseIf strRoom6Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text) & "|" & Val(txtRoom6Child4.Text) & "|" & Val(txtRoom6Child5.Text)
    '    ElseIf strRoom6Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom6Child & "," & Val(txtRoom6Child1.Text) & "|" & Val(txtRoom6Child2.Text) & "|" & Val(txtRoom6Child3.Text) & "|" & Val(txtRoom6Child4.Text) & "|" & Val(txtRoom6Child5.Text) & "|" & Val(txtRoom6Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom6AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom6AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function

    'Private Function GetRoom7String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";7," & ddlRoom7Adult.SelectedValue
    '    Dim strRoom7Child = ddlRoom7Child.SelectedValue
    '    If strRoom7Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom7Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom7Child & "," & Val(txtRoom7Child1.Text)
    '    ElseIf strRoom7Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom7Child & "," & Val(txtRoom7Child1.Text) & "|" & Val(txtRoom7Child2.Text)
    '    ElseIf strRoom7Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom7Child & "," & Val(txtRoom7Child1.Text) & "|" & Val(txtRoom7Child2.Text) & "|" & Val(txtRoom7Child3.Text)
    '    ElseIf strRoom7Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom7Child & "," & Val(txtRoom7Child1.Text) & "|" & Val(txtRoom7Child2.Text) & "|" & Val(txtRoom7Child3.Text) & "|" & Val(txtRoom7Child4.Text)
    '    ElseIf strRoom7Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom7Child & "," & Val(txtRoom7Child1.Text) & "|" & Val(txtRoom7Child2.Text) & "|" & Val(txtRoom7Child3.Text) & "|" & Val(txtRoom7Child4.Text) & "|" & Val(txtRoom7Child5.Text)
    '    ElseIf strRoom7Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom7Child & "," & Val(txtRoom7Child1.Text) & "|" & Val(txtRoom7Child2.Text) & "|" & Val(txtRoom7Child3.Text) & "|" & Val(txtRoom7Child4.Text) & "|" & Val(txtRoom7Child5.Text) & "|" & Val(txtRoom7Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom7AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom7AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function



    'Private Function GetRoom8String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";8," & ddlRoom8Adult.SelectedValue
    '    Dim strRoom8Child = ddlRoom8Child.SelectedValue
    '    If strRoom8Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom8Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom8Child & "," & Val(txtRoom8Child1.Text)
    '    ElseIf strRoom8Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom8Child & "," & Val(txtRoom8Child1.Text) & "|" & Val(txtRoom8Child2.Text)
    '    ElseIf strRoom8Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom8Child & "," & Val(txtRoom8Child1.Text) & "|" & Val(txtRoom8Child2.Text) & "|" & Val(txtRoom8Child3.Text)
    '    ElseIf strRoom8Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom8Child & "," & Val(txtRoom8Child1.Text) & "|" & Val(txtRoom8Child2.Text) & "|" & Val(txtRoom8Child3.Text) & "|" & Val(txtRoom8Child4.Text)
    '    ElseIf strRoom8Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom8Child & "," & Val(txtRoom8Child1.Text) & "|" & Val(txtRoom8Child2.Text) & "|" & Val(txtRoom8Child3.Text) & "|" & Val(txtRoom8Child4.Text) & "|" & Val(txtRoom8Child5.Text)
    '    ElseIf strRoom8Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom8Child & "," & Val(txtRoom8Child1.Text) & "|" & Val(txtRoom8Child2.Text) & "|" & Val(txtRoom8Child3.Text) & "|" & Val(txtRoom8Child4.Text) & "|" & Val(txtRoom8Child5.Text) & "|" & Val(txtRoom8Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom8AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom8AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function


    'Private Function GetRoom9String(ByVal strRoomString As String) As String
    '    strRoomString = strRoomString & ";9," & ddlRoom9Adult.SelectedValue
    '    Dim strRoom9Child = ddlRoom9Child.SelectedValue
    '    If strRoom9Child = "0" Then
    '        strRoomString = strRoomString & ",0,0"
    '    ElseIf strRoom9Child = "1" Then
    '        strRoomString = strRoomString & "," & strRoom9Child & "," & Val(txtRoom9Child1.Text)
    '    ElseIf strRoom9Child = "2" Then
    '        strRoomString = strRoomString & "," & strRoom9Child & "," & Val(txtRoom9Child1.Text) & "|" & Val(txtRoom9Child2.Text)
    '    ElseIf strRoom9Child = "3" Then
    '        strRoomString = strRoomString & "," & strRoom9Child & "," & Val(txtRoom9Child1.Text) & "|" & Val(txtRoom9Child2.Text) & "|" & Val(txtRoom9Child3.Text)
    '    ElseIf strRoom9Child = "4" Then
    '        strRoomString = strRoomString & "," & strRoom9Child & "," & Val(txtRoom9Child1.Text) & "|" & Val(txtRoom9Child2.Text) & "|" & Val(txtRoom9Child3.Text) & "|" & Val(txtRoom9Child4.Text)
    '    ElseIf strRoom9Child = "5" Then
    '        strRoomString = strRoomString & "," & strRoom9Child & "," & Val(txtRoom9Child1.Text) & "|" & Val(txtRoom9Child2.Text) & "|" & Val(txtRoom9Child3.Text) & "|" & Val(txtRoom9Child4.Text) & "|" & Val(txtRoom9Child5.Text)
    '    ElseIf strRoom9Child = "6" Then
    '        strRoomString = strRoomString & "," & strRoom9Child & "," & Val(txtRoom9Child1.Text) & "|" & Val(txtRoom9Child2.Text) & "|" & Val(txtRoom9Child3.Text) & "|" & Val(txtRoom9Child4.Text) & "|" & Val(txtRoom9Child5.Text) & "|" & Val(txtRoom9Child6.Text)
    '    End If
    '    Dim strAccomodationType As String = txtRoom9AccomodationTypeCode.Text
    '    Dim strAccomodation As String = txtRoom9AccomodationCode.Text
    '    Return strRoomString & "," & strAccomodationType & "," & strAccomodation
    'End Function

    ''' <summary>
    ''' LoadFooter
    ''' </summary>
    ''' <remarks></remarks>
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
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    ''' <summary>
    ''' FillShiftingRoomAdultChild
    ''' </summary>
    ''' <param name="RoomString"></param>
    ''' <remarks></remarks>
    Private Sub FillShiftingRoomAdultChild(ByVal RoomString As String)

        '*** Danny 26/08/2018
        Dim dtAdultChilds = New DataTable
        'dtAdultChilds = CType(Session("dtAdultChilds"), DataTable)

        dtAdultChilds.Columns.Add(New DataColumn("colAdultLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildLbl", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colAdultSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildSelectNo", GetType(String)))
        dtAdultChilds.Columns.Add(New DataColumn("colChildAges", GetType(String)))

        '*** Danny 04/09/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'txtNoOfNights.Text = objBLLHotelSearch.NoOfNights 
        If Val(objBLLHotelSearch.Room) > 0 Then
            ddlRoom_Dynamic.SelectedValue = objBLLHotelSearch.Room
        End If
        '*** Danny 04/09/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'ddlRoom_Dynamic.SelectedValue = IIf(Val(objBLLHotelSearch.Room.Trim) = 0, 1, objBLLHotelSearch.Room) 'changed by mohamed on 29/08/2018
        DynamicRoomCreate()

        Dim strroomstring As String() = RoomString.Split(";")
        Dim strroom As String()
        Dim strchildage As String()
        If strroomstring.Length > 0 Then
            Dim a As Integer = 0
            Dim b As Integer = 1
            For Each dlRoomtem1 As DataListItem In dlNofoRooms.Items
                Dim row As DataRow = dtAdultChilds.NewRow()
                strroom = strroomstring(a).Split(",")
                Dim ddlDynRoomAdult As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomAdult"), DropDownList)
                Dim ddlDynRoomChild As DropDownList = CType(dlRoomtem1.FindControl("ddlDynRoomChild"), DropDownList)
                Dim ddlDynChildAges As DataList = CType(dlRoomtem1.FindControl("dlChildAges"), DataList)

                ddlDynRoomAdult.SelectedIndex = strroom(1)
                ddlDynRoomChild.SelectedIndex = strroom(2)

                row("colAdultLbl") = (b).ToString
                row("colChildLbl") = (b).ToString
                row("colAdultSelectNo") = strroom(1)
                row("colChildSelectNo") = strroom(2)
                row("colChildAges") = strroom(3)
                dtAdultChilds.Rows.Add(row)

                If strroom(2).ToString <> "0" Then
                    Dim dtChildAge = New DataTable
                    dtChildAge.Columns.Add(New DataColumn("colChildAgeLbl", GetType(String)))
                    dtChildAge.Columns.Add(New DataColumn("colChildAge", GetType(String)))
                    dtChildAge.Columns.Add(New DataColumn("colCHNo", GetType(String)))
                    strchildage = strroom(3).Split("|")
                    For j = 0 To strchildage.Length - 1
                        Dim row2 As DataRow = dtChildAge.NewRow()
                        If j = 0 Then
                            row2("colChildAgeLbl") = "Room " + strroom(0).ToString + " Child Age"
                        Else
                            row2("colChildAgeLbl") = ""
                        End If
                        row2("colChildAge") = strchildage(j)
                        row2("colCHNo") = "CH" + (j + 1).ToString
                        dtChildAge.Rows.Add(row2)
                    Next
                    ddlDynChildAges.DataSource = dtChildAge
                    ddlDynChildAges.DataBind()
                End If

                a = a + 1
            Next
            Session("dtAdultChilds") = dtAdultChilds
        End If

        'Dim strroomstring As String() = RoomString.Split(";")
        'Dim strroom As String()
        'Dim strchildage As String()
        'If strroomstring.Length > 0 Then
        '    Dim strRoomAdultName As String = ""
        '    Dim strRoomchildName As String = ""
        '    Dim strRoomchildage As String = ""

        '    For i = 0 To strroomstring.Length - 1
        '        strroom = strroomstring(i).Split(",")
        '        If strroom(1) <> "0" Then
        '            strRoomAdultName = "ddlRoom" & strroom(0) & "Adult"
        '            strRoomchildName = "ddlRoom" & strroom(0) & "Child"

        '            Dim ddlRoomm As DropDownList = DirectCast(FindControl(strRoomAdultName), DropDownList)
        '            Dim ddlRoomc As DropDownList = DirectCast(FindControl(strRoomchildName), DropDownList)


        '            ddlRoomm.SelectedValue = strroom(1)
        '            ddlRoomc.SelectedValue = strroom(2)


        '        End If
        '        If strroom(2).ToString <> "0" Then

        '            strchildage = strroom(3).Split("|")

        '            For j = 0 To strchildage.Length - 1

        '                strRoomchildage = "txtRoom" & strroom(0) & "Child" & j + 1
        '                Dim txtRoomchild As TextBox = DirectCast(FindControl(strRoomchildage), TextBox)


        '                txtRoomchild.Text = strchildage(j)
        '            Next

        '        End If
        '    Next
        'End If
    End Sub

    ''' <summary>
    ''' btnMyAccount_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub


    ''' <summary>
    ''' btnConfirmHome_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
    End Sub
    ''' <summary>
    ''' clearallBookingSessions
    ''' </summary>
    ''' <remarks></remarks>
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
        Session("sdtSelectedFreeFormSpclEvent") = Nothing
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
        Session("sdsHotelRoomTypes") = Nothing
    End Sub


    Private Sub BindPricesBasedOnRooms()
        Dim dtFreeFormRoomType As New DataTable

        Dim strDestination As String = txtDestinationName.Text
        Dim strDestType As String = ""
        Dim strDestinationCode As String = ""
        Dim DestinationCodeAndType As String = txtDestinationCode.Text
        Dim strDest As String() = txtDestinationCode.Text.Split("|")
        If strDest.Length = 2 Then
            strDestinationCode = strDest(0)
            strDestType = strDest(1)
        End If
        Dim strCheckIn As String = txtCheckIn.Text
        Dim strCheckOut As String = txtCheckOut.Text
        Dim strNoOfNights As String = txtNoOfNights.Text
        Dim strRoom As String = ddlRoom.SelectedValue
        Dim strAdult As String = ddlAdult.SelectedValue
        Dim strSourceCountry As String = txtCountry.Text
        Dim strSourceCountryCode As String = txtCountryCode.Text
        Dim strCustomer As String = txtCustomer.Text
        Dim strCustomerCode As String = txtCustomerCode.Text
        Dim strHotels As String = txtHotelName.Text
        Dim strHotelCode As String = txtHotelCode.Text
        Dim strRoomString As String = GetRoomString(strRoom)
        Dim strRoomTypeCode As String = txtRoomTypeCode.Text
        If txtRoomTypeCode.Text <> "" Then
            Dim strRoomTypeCodes As String() = txtRoomTypeCode.Text.Split("|")
            strRoomTypeCode = strRoomTypeCodes(0)
        End If

        Dim strRoomCatCode As String = txtRoom1AccomodationTypeCode.Text

        Dim strMealPlanCode As String = txtMealPlanCode.Text
        Dim strChildagestring As String = ""
        Dim strHotelString As String = "1,DBL,2,0,0"
        Dim strSelectedEvents As String = ""
        Dim strRequestId As String = ""
        Dim strRLineNo As String = ""

        Dim objBLLHotelFreeFormBooking As New BLLHotelFreeFormBooking()
        dtFreeFormRoomType = objBLLHotelFreeFormBooking.GetFreeFormPricesBasedOnRooms(strCheckIn, strCheckOut, strRoom, strRoomString, strHotelCode, IIf(Session("sLoginType") = "RO", txtCustomerCode.Text, Session("sAgentCode")), Session("sAgentCompany"))
        'Dim asStr1 As String = "'" & txtContract.Text.Replace("contracts", "").Replace("contract", "") & "'" 'changed by mohamed on 02/08/2018
        'dtFreeFormRoomType.Columns("bookingcode").Expression = asStr1
        Session("sdtFreeFormRoomType") = dtFreeFormRoomType
        Dim dvFreeFormRoomType As DataView = New DataView(dtFreeFormRoomType)
        dltotalPriceBreak.DataSource = dvFreeFormRoomType.ToTable(True, "roomno", "RoomHeading", "saletotal", "costtotal")
        dltotalPriceBreak.DataBind()


        FillddlSpclRoomNos(ddlRoom.SelectedValue)

        BindSpecialEvents(strHotelCode)

        chkSpecialEvents.Checked = False



        Dim dsFreeFormSpecialEvents As DataSet = objBLLHotelFreeFormBooking.GetFreeFormSpecialEvents(strHotelCode, strRoomTypeCode, strMealPlanCode, strRoomCatCode, strCustomerCode, strSourceCountryCode, strCheckIn, strCheckOut, strRoom, strRoomString, strHotelString, strSelectedEvents, strRequestId, strRLineNo)
        Session("sdsFreeFormSpecialEvents") = dsFreeFormSpecialEvents
        If Not dsFreeFormSpecialEvents Is Nothing Then
            If dsFreeFormSpecialEvents.Tables(0).Rows.Count > 0 Then
                pnlSpecialEvents.Visible = True
                dlSpecialEvents.DataSource = dsFreeFormSpecialEvents.Tables(0)
                dlSpecialEvents.DataBind()
            Else
                dlSpecialEvents.DataBind()
                pnlSpecialEvents.Visible = True
            End If
        End If

    End Sub

    ' ''' <summary>
    ' ''' dlSpecialEvents_ItemDataBound
    ' ''' </summary>
    ' ''' <param name="sender"></param>
    ' ''' <param name="e"></param>
    ' ''' <remarks></remarks>
    Protected Sub dlSpecialEvents_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlSpecialEvents.ItemDataBound
        Try


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim ddlEvents As DropDownList = CType(e.Item.FindControl("ddlEvents"), DropDownList)
                '   Dim lblEventCode As Label = CType(e.Item.FindControl("lblEventCode"), Label)
                '  Dim spleventname As Label = CType(e.Item.FindControl("spleventname"), Label)
                Dim lblspecialeventcount As Label = CType(e.Item.FindControl("lblspecialeventcount"), Label)
                Dim lblEventDate As Label = CType(e.Item.FindControl("lblEventDate"), Label)
                Dim txtSpecEventDate As TextBox = CType(e.Item.FindControl("txtSpecEventDate"), TextBox)

                Dim gvSpecialEvents As GridView = CType(e.Item.FindControl("gvSpecialEvents"), GridView)


                Dim chkSpclComplimentaryToCustomer As CheckBox = CType(e.Item.FindControl("chkSpclComplimentaryToCustomer"), CheckBox)
                Dim chkSpclComplimentaryFromSupplier As CheckBox = CType(e.Item.FindControl("chkSpclComplimentaryFromSupplier"), CheckBox)


                If Session("sLoginType") = "RO" Then
                    chkSpclComplimentaryToCustomer.Visible = True
                    chkSpclComplimentaryFromSupplier.Visible = True
                Else
                    chkSpclComplimentaryToCustomer.Visible = False
                    chkSpclComplimentaryFromSupplier.Visible = False
                End If
                Dim dss As New DataSet
                If Not Session("sdsFreeFormSpecialEvents") Is Nothing Then
                    dss = Session("sdsFreeFormSpecialEvents")
                    If dss.Tables(2).Rows.Count > 0 Then
                        ddlEvents.DataSource = dss.Tables(2)
                        ddlEvents.DataTextField = "spleventname"
                        ddlEvents.DataValueField = "spleventcode"
                        ddlEvents.DataBind()


                        ddlEvents.SelectedValue = dss.Tables(0).Rows(Val(lblspecialeventcount.Text) - 1)("spleventcode").ToString
                        txtSpecEventDate.Text = dss.Tables(0).Rows(Val(lblspecialeventcount.Text) - 1)("spleventdate").ToString
                        If dss.Tables(0).Rows(Val(lblspecialeventcount.Text) - 1)("comp_cust").ToString = "1" Then
                            chkSpclComplimentaryToCustomer.Checked = True
                        Else
                            chkSpclComplimentaryToCustomer.Checked = False
                        End If
                        If dss.Tables(0).Rows(Val(lblspecialeventcount.Text) - 1)("comp_supp").ToString = "1" Then
                            chkSpclComplimentaryFromSupplier.Checked = True
                        Else
                            chkSpclComplimentaryFromSupplier.Checked = False
                        End If

                    End If
                    If dss.Tables(1).Rows.Count > 0 Then
                        gvSpecialEvents.DataSource = dss.Tables(1)
                        gvSpecialEvents.DataBind()
                    End If



                End If


                '    ddlEvents.Attributes.Add("onChange", "javascript:SpecialEventChanged(this, '" + e.Item.ItemIndex.ToString + "')")

                'Dim strDate As String = lblEventDate.Text
                ''    lblEventDate.Text = Format(CType(lblEventDate.Text, Date), "dd/MM/yyyy")
                'Dim dss As New DataSet
                'If Not Session("sdsFreeFormSpecialEvents") Is Nothing Then
                '    dss = Session("sdsFreeFormSpecialEvents")
                '    If dss.Tables(2).Rows.Count > 0 Then
                '        Dim dvSclEvents As DataView = New DataView(dss.Tables(2))
                '        dvSclEvents.RowFilter = "fromdate='" & strDate & "'"
                '        ddlEvents.AppendDataBoundItems = True
                '        ddlEvents.DataSource = dvSclEvents
                '        ddlEvents.DataValueField = "spleventcode"
                '        ddlEvents.DataTextField = "spleventname"
                '        ddlEvents.DataBind()

                '        ddlEvents.SelectedValue = lblEventCode.Text





                '        Dim dtSelectedSpclEventRow As New DataTable
                '        If Not Session("sdtSelectedFreeFormSpclEvent") Is Nothing Then
                '            dtSelectedSpclEventRow = Session("sdtSelectedFreeFormSpclEvent")

                '            If dtSelectedSpclEventRow.Rows.Count > 0 Then
                '                Dim dvSclEventDetailsFilter As DataView = New DataView(dtSelectedSpclEventRow)
                '                dvSclEventDetailsFilter.RowFilter = "PartyCode='" & hdSPEPartyCode.Value.Trim & "' AND RoomTypeCode ='" & hdSPERoomTypeCode.Value.Trim & "' AND MealPlanCode='" & hdSPEMealPlanCode.Value.Trim & "' AND  CatCode='" & hdSPEcatCode.Value.Trim & "' AND AccCode='" & hdSPEAccCode.Value.Trim & "' AND RatePlanId='" & hdSPERatePlanId.Value.Trim & "'  AND spleventdate='" & strDate & "' "
                '                If dvSclEventDetailsFilter.Count > 0 Then
                '                    ddlEvents.SelectedValue = dvSclEventDetailsFilter.Item(0)("spleventcode").ToString
                '                    lblEventCode.Text = dvSclEventDetailsFilter.Item(0)("spleventcode").ToString
                '                    strDate = dvSclEventDetailsFilter.Item(0)("spleventdate").ToString
                '                    '  lblEventDate.Text = Format(CType(strDate, Date), "dd/MM/yyyy")
                '                End If

                '            End If

                '            Dim dvSclEventDetails1 As DataView = New DataView(dtSelectedSpclEventRow)



                '            dvSclEventDetails1.RowFilter = "PartyCode='" & hdSPEPartyCode.Value.Trim & "' AND RoomTypeCode ='" & hdSPERoomTypeCode.Value.Trim & "' AND MealPlanCode='" & hdSPEMealPlanCode.Value.Trim & "' AND  CatCode='" & hdSPEcatCode.Value.Trim & "' AND AccCode='" & hdSPEAccCode.Value.Trim & "' AND RatePlanId='" & hdSPERatePlanId.Value.Trim & "' AND spleventcode='" & lblEventCode.Text & "'  AND spleventdate='" & strDate & "' " ' AND splistcode='" & strSplistcode & "' AND splineno='" & strSpLineno & "'    AND paxtype='" & strSpPaxtype & "'  and childage='" & strSpChildAge & "'
                '            If dvSclEventDetails1.Count > 0 Then
                '                gvSpecialEvents.DataSource = dvSclEventDetails1
                '                gvSpecialEvents.DataBind()
                '            Else
                '                Dim dvSclEventDetails As DataView = New DataView(dss.Tables(1))
                '                dvSclEventDetails.RowFilter = "spleventdate='" & strDate & "' AND spleventcode='" & ddlEvents.SelectedValue & "'"
                '                gvSpecialEvents.DataSource = dvSclEventDetails
                '                gvSpecialEvents.DataBind()
                '            End If

                '        Else
                '            Dim dvSclEventDetails As DataView = New DataView(dss.Tables(1))
                '            dvSclEventDetails.RowFilter = "spleventdate='" & strDate & "' AND spleventcode='" & ddlEvents.SelectedValue & "'"
                '            gvSpecialEvents.DataSource = dvSclEventDetails
                '            gvSpecialEvents.DataBind()
                '        End If


                '    End If
                'End If



            End If


        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: dlSpecialEvents_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub gvSpecialEvents_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If hdBookingEngineRateType.Value = "1" Then
                If (e.Row.RowType = DataControlRowType.Header) Then
                    e.Row.Cells(3).Visible = False
                    e.Row.Cells(4).Visible = False
                    e.Row.Cells(5).Visible = False
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(7).Visible = False
                End If
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then

                Dim lblpaxcurrcode As Label = CType(e.Row.FindControl("lblpaxcurrcode"), Label)
                Dim lblcostCurrcode As Label = CType(e.Row.FindControl("lblcostCurrcode"), Label)

                Dim lblNoOfPax As Label = CType(e.Row.FindControl("lblNoOfPax"), Label)

                Dim txtPaxRate As TextBox = CType(e.Row.FindControl("txtPaxRate"), TextBox)
                Dim txtPaxCost As TextBox = CType(e.Row.FindControl("txtPaxCost"), TextBox)

                Dim lblPaxRate As Label = CType(e.Row.FindControl("lblPaxRate"), Label)
                Dim lblPaxCost As Label = CType(e.Row.FindControl("lblPaxCost"), Label)

                Dim lblwlSpecialEventValue As Label = CType(e.Row.FindControl("lblwlSpecialEventValue"), Label)

                Dim lblwlconvrate As Label = CType(e.Row.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(e.Row.FindControl("lblwlmarkupperc"), Label)
                Dim lblwlcurrcode As Label = CType(e.Row.FindControl("lblwlcurrcode"), Label)
                Dim lblwlPaxRate As Label = CType(e.Row.FindControl("lblwlPaxRate"), Label)
                lblwlPaxRate.Text = Math.Round(Val(lblwlPaxRate.Text))
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(lblwlmarkupperc.Text)) / 100) * Convert.ToDecimal(lblwlconvrate.Text)


                Dim lblSpecialEventValue As Label = CType(e.Row.FindControl("lblSpecialEventValue"), Label)
                Dim lblSpecialEventCostValue As Label = CType(e.Row.FindControl("lblSpecialEventCostValue"), Label)
                Dim lblSpecialEventValueNew As Label = CType(e.Row.FindControl("lblSpecialEventValueNew"), Label)
                Dim lblSpecialEventCostValueNew As Label = CType(e.Row.FindControl("lblSpecialEventCostValueNew"), Label)

                txtPaxRate.Text = Math.Round(CType(Val(txtPaxRate.Text), Double), 2).ToString
                txtPaxCost.Text = Math.Round(CType(Val(txtPaxCost.Text), Double), 2).ToString
                lblPaxRate.Text = Math.Round(CType(Val(lblPaxRate.Text), Double), 2).ToString
                lblPaxCost.Text = Math.Round(CType(Val(lblPaxCost.Text), Double), 2).ToString

                lblSpecialEventValue.Text = Math.Round(CType(Val(lblSpecialEventValue.Text), Double), 2).ToString & " " & lblpaxcurrcode.Text
                lblSpecialEventCostValue.Text = Math.Round(CType(Val(lblSpecialEventCostValue.Text), Double), 2).ToString & " " & lblcostCurrcode.Text


                txtPaxRate.Attributes.Add("onChange", "javascript:CalculateSpecialEventSaleValue('" + CType(lblNoOfPax.Text, String) + "', '" + CType(txtPaxRate.ClientID, String) + "','" + CType(lblSpecialEventValue.ClientID, String) + "','" + CType(lblpaxcurrcode.Text, String) + "','" + CType(lblwlSpecialEventValue.ClientID, String) + "','" + CType(dWlMarkup, String) + "','" + CType(lblwlcurrcode.Text, String) + "','" + CType(lblwlPaxRate.ClientID, String) + "','" + CType(lblSpecialEventValueNew.ClientID, String) + "')")
                txtPaxCost.Attributes.Add("onChange", "javascript:CalculateSpecialEventCostValue('" + CType(lblNoOfPax.Text, String) + "', '" + CType(txtPaxCost.ClientID, String) + "','" + CType(lblSpecialEventCostValue.ClientID, String) + "','" + CType(lblcostCurrcode.Text, String) + "','" + CType(lblSpecialEventCostValueNew.Text, String) + "')")



                txtPaxRate.Visible = True
                lblPaxRate.Visible = False
                txtPaxCost.Visible = True
                lblPaxCost.Visible = False


                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    If hdBookingEngineRateType.Value = "1" Then
                        lblPaxRate.Attributes.Add("style", "display:none")
                        lblwlPaxRate.Attributes.Add("style", "display:none")

                        lblSpecialEventValue.Attributes.Add("style", "display:none")
                        lblwlSpecialEventValue.Attributes.Add("style", "display:none")
                    Else
                        lblPaxRate.Attributes.Add("style", "display:none")
                        lblwlPaxRate.Attributes.Add("style", "display:block")

                        lblSpecialEventValue.Attributes.Add("style", "display:none")
                        lblwlSpecialEventValue.Attributes.Add("style", "display:block")
                    End If
                Else
                    lblPaxRate.Attributes.Add("style", "display:block")
                    lblwlPaxRate.Attributes.Add("style", "display:none")

                    lblSpecialEventValue.Attributes.Add("style", "display:block")
                    lblwlSpecialEventValue.Attributes.Add("style", "display:none")
                End If


            End If


            If hdBookingEngineRateType.Value = "1" Then
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: gvSpecialEvents_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub dltotalPriceBreak_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dltotalPriceBreak.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblRoomNo As Label = CType(e.Item.FindControl("lblRoomNo"), Label)
            Dim dvcosttotal As HtmlGenericControl = CType(e.Item.FindControl("dvcosttotal"), HtmlGenericControl)
            Dim gvPricebreakup As GridView = CType(e.Item.FindControl("gvPricebreakup"), GridView)
            Dim lblwlSaleTotal As Label = CType(e.Item.FindControl("lblwlSaleTotal"), Label)

            If Not Session("sdtFreeFormRoomType") Is Nothing Then
                Dim dv1 As DataView = New DataView(Session("sdtFreeFormRoomType"))
                dv1.RowFilter = "roomno='" & lblRoomNo.Text & "' "
                If dv1.Count > 0 Then

                    gvPricebreakup.DataSource = dv1
                    gvPricebreakup.DataBind()

                End If
                hdgvPricebreakupRowwCount.Value = gvPricebreakup.Rows.Count
            End If




        End If


    End Sub
    Protected Sub btnPriceBreakupFillPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If txtBreakupTotalPriceForAll.Text <> "" Or txtsalepriceForAll.Text <> "" Or txtBookingCodeForAll.Text <> "" Then
                For Each dlitem As DataListItem In dltotalPriceBreak.Items
                    Dim lblSaleTotal As Label = CType(dlitem.FindControl("lblSaleTotal"), Label)
                    Dim lblCostTotal As Label = CType(dlitem.FindControl("lblCostTotal"), Label)

                    Dim strRoomNo As String = ""
                    strRoomNo = ddlRoomNos.SelectedValue
                    If strRoomNo = dlitem.ItemIndex + 1 Then
                        Dim gvPricebreakup As GridView = CType(dlitem.FindControl("gvPricebreakup"), GridView)

                        Dim fRoomSalePrice As Double = 0
                        Dim fRoomCostPrice As Double = 0
                        For Each row As GridViewRow In gvPricebreakup.Rows
                            Dim txtsaleprice As TextBox = CType(row.FindControl("txtsaleprice"), TextBox)
                            Dim txtBreakupTotalPrice As TextBox = CType(row.FindControl("txtBreakupTotalPrice"), TextBox)
                            Dim lblConversionRate As Label = CType(row.FindControl("lblConversionRate"), Label)
                            Dim lblUSDPrice As Label = CType(row.FindControl("lblUSDPrice"), Label)
                            Dim lblSalePriceCurrcode As Label = CType(row.FindControl("lblSalePriceCurrcode"), Label)
                            Dim lblwlconvrate As Label = CType(row.FindControl("lblwlconvrate"), Label)
                            Dim lblwlmarkupperc As Label = CType(row.FindControl("lblwlmarkupperc"), Label)
                            Dim lblwlcurrcode As Label = CType(row.FindControl("lblwlcurrcode"), Label)
                            Dim lblwlbreakupPrice As Label = CType(row.FindControl("lblwlbreakupPrice"), Label)
                            Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(Val(lblwlmarkupperc.Text))) / 100) * Convert.ToDecimal(Val(lblwlconvrate.Text))

                            Dim lblBookingCode As TextBox = CType(row.FindControl("lblBookingCode"), TextBox)

                            If chkFillBlank.Checked = True Then
                                If txtBreakupTotalPriceForAll.Text <> "" Then
                                    If txtBreakupTotalPrice.Text = "" Then
                                        txtBreakupTotalPrice.Text = txtBreakupTotalPriceForAll.Text

                                    End If
                                End If
                                If txtsalepriceForAll.Text <> "" Then
                                    If txtsaleprice.Text = "" Then
                                        txtsaleprice.Text = txtsalepriceForAll.Text
                                        lblwlbreakupPrice.Text = Math.Round(Val(txtsaleprice.Text) * dWlMarkup)
                                    End If
                                End If
                                If txtBookingCodeForAll.Text <> "" Then 'changed by mohamed on 02/08/2018
                                    If lblBookingCode.Text = "" Then
                                        lblBookingCode.Text = txtBookingCodeForAll.Text
                                    End If
                                End If
                            Else
                                If txtBreakupTotalPriceForAll.Text <> "" Then
                                    txtBreakupTotalPrice.Text = txtBreakupTotalPriceForAll.Text
                                End If
                                If txtsalepriceForAll.Text <> "" Then
                                    txtsaleprice.Text = txtsalepriceForAll.Text
                                    lblwlbreakupPrice.Text = Math.Round(Val(txtsaleprice.Text) * dWlMarkup)
                                End If

                                If txtBookingCodeForAll.Text <> "" Then 'changed by mohamed on 02/08/2018
                                    lblBookingCode.Text = txtBookingCodeForAll.Text
                                End If
                            End If


                            If txtsaleprice.Text <> "" Then
                                fRoomSalePrice = fRoomSalePrice + txtsaleprice.Text
                            End If
                            If txtBreakupTotalPrice.Text <> "" Then
                                fRoomCostPrice = fRoomCostPrice + txtBreakupTotalPrice.Text
                                lblUSDPrice.Text = Math.Round(CType(Val(txtBreakupTotalPrice.Text), Double) * CType(Val(lblConversionRate.Text), Double), 2).ToString & " " & lblSalePriceCurrcode.Text & ")"
                            End If


                            ' ***
                        Next
                        lblSaleTotal.Text = Math.Round(fRoomSalePrice, 2).ToString
                        lblCostTotal.Text = Math.Round(fRoomCostPrice, 2).ToString

                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: btnPriceBreakupFillPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))

        End Try

    End Sub


    Protected Sub gvPricebreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)



        Try

            If Session("sLoginType") = "RO" Then
                e.Row.Cells(1).Visible = True
                e.Row.Cells(3).Visible = True
            Else
                e.Row.Cells(1).Visible = False
                e.Row.Cells(3).Visible = False
            End If
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                Dim lblBreakupDate As Label = CType(e.Row.FindControl("lblBreakupDate"), Label)
                Dim lblBreakupDateName As Label = CType(e.Row.FindControl("lblBreakupDateName"), Label)
                Dim lblbreakupPrice As Label = CType(e.Row.FindControl("lblbreakupPrice"), Label)
                Dim lblBreakupTotalPrice As Label = CType(e.Row.FindControl("lblBreakupTotalPrice"), Label)
                Dim lblBookingCode As TextBox = CType(e.Row.FindControl("lblBookingCode"), TextBox)
                If lblBookingCode.Text.Length > 16 Then
                    lblBookingCode.Text = lblBookingCode.Text.Substring(0, 15) & ".."
                End If

                Dim txtsaleprice As TextBox = CType(e.Row.FindControl("txtsaleprice"), TextBox)
                Dim txtBreakupTotalPrice As TextBox = CType(e.Row.FindControl("txtBreakupTotalPrice"), TextBox)
                Dim dtDate As DateTime = CType(lblBreakupDate.Text, DateTime)
                lblBreakupDate.Text = dtDate.ToString("dd/MM/yyyy") & ", "
                lblBreakupDateName.Text = dtDate.ToString("dddd").Substring(0, 3)
                Dim lblUSDPrice As Label = CType(e.Row.FindControl("lblUSDPrice"), Label)
                Dim lblConversionRate As Label = CType(e.Row.FindControl("lblConversionRate"), Label)

                Dim lblSalePriceCurrcode As Label = CType(e.Row.FindControl("lblSalePriceCurrcode"), Label)
                Dim lblCostPriceCurrcode As Label = CType(e.Row.FindControl("lblCostPriceCurrcode"), Label)
                Dim lblwlcurrcode As Label = CType(e.Row.FindControl("lblwlcurrcode"), Label)

                Dim gvGridviewRow As GridViewRow = CType(lblConversionRate.NamingContainer, GridViewRow)
                Dim _gvPricebreakup As GridView = CType((gvGridviewRow.Parent.Parent), GridView)
                Dim dlPriceBreakupItem As DataListItem = CType(_gvPricebreakup.NamingContainer, DataListItem)
                Dim lblSaleTotal As Label = CType(dlPriceBreakupItem.FindControl("lblSaleTotal"), Label)
                Dim lblCostTotal As Label = CType(dlPriceBreakupItem.FindControl("lblCostTotal"), Label)
                Dim lblSaleTotalCurCode As Label = CType(dlPriceBreakupItem.FindControl("lblSaleTotalCurCode"), Label)
                Dim lblCostTotalCurCode As Label = CType(dlPriceBreakupItem.FindControl("lblCostTotalCurCode"), Label)

                Dim lblwlSaleTotal As Label = CType(dlPriceBreakupItem.FindControl("lblwlSaleTotal"), Label)
                Dim lblwlSaleTotalCurCode As Label = CType(dlPriceBreakupItem.FindControl("lblwlSaleTotalCurCode"), Label)
                lblwlSaleTotalCurCode.Text = lblwlcurrcode.Text

                lblSaleTotalCurCode.Text = lblSalePriceCurrcode.Text
                lblCostTotalCurCode.Text = lblCostPriceCurrcode.Text
                Dim lblSalePriceCurrcodeHeader As Label = CType(_gvPricebreakup.HeaderRow.FindControl("lblSalePriceCurrcodeHeader"), Label)
                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    lblSalePriceCurrcodeHeader.Text = "PRICE (" & lblwlcurrcode.Text & ")"
                Else
                    lblSalePriceCurrcodeHeader.Text = "PRICE (" & lblSalePriceCurrcode.Text & ")"
                End If

                Dim lblCostPriceCurrcodeHeader As Label = CType(_gvPricebreakup.HeaderRow.FindControl("lblCostPriceCurrcodeHeader"), Label)

                lblCostPriceCurrcodeHeader.Text = "COST PRICE (" & lblCostPriceCurrcode.Text & ")"

                Dim lblwlconvrate As Label = CType(e.Row.FindControl("lblwlconvrate"), Label)
                Dim lblwlmarkupperc As Label = CType(e.Row.FindControl("lblwlmarkupperc"), Label)

                Dim lblwlbreakupPrice As Label = CType(e.Row.FindControl("lblwlbreakupPrice"), Label)
                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(Val(lblwlmarkupperc.Text))) / 100) * Convert.ToDecimal(Val(lblwlconvrate.Text))
                'Dim _dlRatePlan As DataList = CType((dlPriceBreakupItem.Parent), DataList)

                txtsaleprice.Attributes.Add("onChange", "javascript:CalculateRoomTotalPrice('" + lblSaleTotal.ClientID + "','" + _gvPricebreakup.ClientID + "','" + dWlMarkup.ToString + "','" + lblwlbreakupPrice.ClientID + "','" + txtsaleprice.ClientID + "')")
                txtBreakupTotalPrice.Attributes.Add("onChange", "javascript:CalculateUSDAndCostPriceTotal('" + txtBreakupTotalPrice.ClientID + "', '" + lblConversionRate.Text + "', '" + lblUSDPrice.ClientID + "','" + lblCostTotal.ClientID + "','" + _gvPricebreakup.ClientID + "','" + lblSalePriceCurrcode.Text + "' )")
                Dim dPrice As Double = CType(Val(lblbreakupPrice.Text), Double)
                Dim dCostPrice As Double = CType(Val(lblBreakupTotalPrice.Text), Double)
                Dim dConvRate As Double = CType(Val(lblConversionRate.Text), Double)

                lblbreakupPrice.Text = Math.Round(dPrice, 2).ToString
                lblBreakupTotalPrice.Text = Math.Round(dCostPrice, 2).ToString



                lblUSDPrice.Text = "(" & (Math.Round((dCostPrice * dConvRate), 2)).ToString & " " & lblSalePriceCurrcode.Text & ")"


                'Dim dvPriceDate As HtmlGenericControl = CType(e.Row.FindControl("dvPriceDate"), HtmlGenericControl)
                'Dim dvPricePerNight As HtmlGenericControl = CType(e.Row.FindControl("dvPricePerNight"), HtmlGenericControl)
                'Dim dvCostPricePerNight As HtmlGenericControl = CType(e.Row.FindControl("dvCostPricePerNight"), HtmlGenericControl)
                'Dim dvCostPricePerNightText As HtmlGenericControl = CType(e.Row.FindControl("dvCostPricePerNightText"), HtmlGenericControl)

                'If Session("sLoginType") <> "RO" Then

                '    lblbreakupPrice.Visible = True
                '    lblBreakupTotalPrice.Visible = True
                '    txtsaleprice.Visible = False
                '    txtBreakupTotalPrice.Visible = False
                '    ' dvPriceBreakupSave.Visible = False
                '    chkComplimentaryArrivalTransfer.Visible = False
                '    chkComplimentaryDepartureTransfer.Visible = False
                '    chkComplimentaryFromSupplier.Visible = False
                '    chkComplimentaryToCustomer.Visible = False
                'Else
                '    If hdOveride.Value = "1" Then
                '        lblbreakupPrice.Visible = False
                '        lblBreakupTotalPrice.Visible = False
                '        txtsaleprice.Visible = True
                '        txtBreakupTotalPrice.Visible = True
                '        '  dvPriceBreakupSave.Visible = True
                '    Else
                '        lblbreakupPrice.Visible = True
                '        lblBreakupTotalPrice.Visible = True
                '        txtsaleprice.Visible = False
                '        txtBreakupTotalPrice.Visible = False
                '        '    dvPriceBreakupSave.Visible = False
                '    End If



                'End If

                lblbreakupPrice.Visible = False
                lblBreakupTotalPrice.Visible = False
                txtsaleprice.Visible = True
                txtBreakupTotalPrice.Visible = True


                chkComplimentaryArrivalTransfer.Visible = True
                chkComplimentaryDepartureTransfer.Visible = True
                chkComplimentaryFromSupplier.Visible = True
                chkComplimentaryToCustomer.Visible = True

                If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
                    '  lblwlSaleTotal.Text = Math.Round(Val(lblSaleTotal.Text) * dWlMarkup)
                    lblbreakupPrice.Attributes.Add("style", "display:none")
                    txtsaleprice.Attributes.Add("style", "display:none")
                    lblwlbreakupPrice.Attributes.Add("style", "display:block")
                    lblwlSaleTotal.Attributes.Add("style", "display:block")
                    lblwlSaleTotalCurCode.Attributes.Add("style", "display:block")
                    lblSaleTotal.Attributes.Add("style", "display:none")
                    lblSaleTotalCurCode.Attributes.Add("style", "display:none")
                Else
                    lblbreakupPrice.Attributes.Add("style", "display:block")
                    txtsaleprice.Attributes.Add("style", "display:block")
                    lblwlbreakupPrice.Attributes.Add("style", "display:none")
                    lblwlSaleTotal.Attributes.Add("style", "display:none")
                    lblwlSaleTotalCurCode.Attributes.Add("style", "display:none")
                    lblSaleTotal.Attributes.Add("style", "display:block")
                    lblSaleTotalCurCode.Attributes.Add("style", "display:block")
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: gvPricebreakup_RowDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Private Sub BindSpecialEvents(ByVal strHotelCode As String)
        Dim objBLLHotelFreeFormBooking As New BLLHotelFreeFormBooking
        Dim dt As DataTable = objBLLHotelFreeFormBooking.GetSpecialEvents(strHotelCode)
        If dt.Rows.Count > 0 Then
            'ddlSpclEvents.DataSource = dt
            'ddlSpclEvents.DataTextField = "spleventname"
            'ddlSpclEvents.DataValueField = "spleventcode"
            'ddlSpclEvents.DataBind()
        End If
    End Sub



    'Protected Sub ddlSpclRoom_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSpclRoom.SelectedIndexChanged
    '    Dim strRoom As String = ddlSpclRoom.SelectedValue
    '    Dim strRoomAdult As String = "ddlRoom" & strRoom & "Adult"
    '    Dim ddlRoomAdult As DropDownList = FindControl(strRoomAdult)
    '    txtSpclNoOfAdult.Text = ddlRoomAdult.SelectedValue

    '    Dim strRoomChild As String = "ddlRoom" & strRoom & "Child"
    '    Dim ddlRoomChild As DropDownList = FindControl(strRoomChild)
    '    txtNoOfChild.Text = ddlRoomChild.SelectedValue

    'End Sub
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

    Protected Sub btnBook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBook.Click
        Try
            Dim objBLLHotelFreeFormBooking As New BLLHotelFreeFormBooking
            objBLLHotelFreeFormBooking.Div_Code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast(nolock) where agentcode='" & Session("sAgentCode") & "'")
            objBLLHotelFreeFormBooking.Requestid = GetNewOrExistingRequestId()
            objBLLHotelFreeFormBooking.AgentCode = IIf(Session("sLoginType") = "RO", txtCustomerCode.Text, Session("sAgentCode"))
            objBLLHotelFreeFormBooking.SourceCtryCode = txtCountryCode.Text
            objBLLHotelFreeFormBooking.AgentRef = ""
            objBLLHotelFreeFormBooking.ColumbusRef = ""
            objBLLHotelFreeFormBooking.SubUserCode = ""
            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                objBLLHotelFreeFormBooking.SubUserCode = objResParam.SubUserCode
            End If
            Dim strHotelLineNo As String = ""
            If Session("sRequestId") Is Nothing Then
                strHotelLineNo = "1"
            ElseIf ViewState("vRLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
                strHotelLineNo = objBLLCommonFuntions.GetBookingRowLineNo(Session("sRequestId"), "HOTEL")
            Else
                strHotelLineNo = ViewState("vRLineNo")
            End If

            If chkShifting.Checked = True Then
                objBLLHotelFreeFormBooking.Shifting = "1"
                Dim strShiftCode As String() = txtShiftHotelCode.Text.Trim.Split("|")
                objBLLHotelFreeFormBooking.ShiftingCode = strShiftCode(0)
                objBLLHotelFreeFormBooking.ShiftingLineNo = strShiftCode(1)
            Else
                objBLLHotelFreeFormBooking.Shifting = "0"
                objBLLHotelFreeFormBooking.ShiftingCode = ""
            End If

            ' added by abin on 20181013 -- start
            FindCumilative()
            Dim iCumulativeUser As Integer = 0
            If Session("sLoginType") = "RO" Then
                Dim strQuery As String = "select count(agentcode)CNT from agentmast(nolock) where bookingengineratetype='CUMULATIVE' and agentcode=(select min(agentcode) from  booking_header(nolock) where requestid='" & objBLLHotelFreeFormBooking.Requestid & "')"
                iCumulativeUser = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)

            End If

            'changed by mohamed on 29/08/2018
            If hdBookingEngineRateType.Value = "1" Or iCumulativeUser > 0 Then
                Dim lsSqlQry As String = ""
                Dim lsFutureDateAvailable As String = ""
                If txtShiftHotelCode.Text.Trim = "" Then 'And ViewState("vRLineNo") Is Nothing
                    Dim dsFt As DataSet
                    lsSqlQry = "execute sp_check_future_checkin_booking '" & objBLLHotelFreeFormBooking.Requestid & "'," & strHotelLineNo & ",'" & txtCheckIn.Text & "','" & txtCheckOut.Text & "',''"
                    dsFt = objclsUtilities.GetDataFromDataset(lsSqlQry)
                    If dsFt.Tables.Count > 0 Then
                        If dsFt.Tables(0).Rows.Count >= 1 Then
                            lsFutureDateAvailable = dsFt.Tables(0).Rows(0)("AvailStatus")
                            If lsFutureDateAvailable.Trim <> "" Then
                                'MessageBox.ShowMessage(Page, MessageType.Errors, "Shifting/Add More Room in current hotel is not selected when additional hotel is added for future date")
                                MessageBox.ShowMessage(Page, MessageType.Errors, "Check In / Check out should be related to earlier hotel booked")
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
            ' added by abin on 20181013 -- end

            objBLLHotelFreeFormBooking.RlineNo = strHotelLineNo
            objBLLHotelFreeFormBooking.CheckIn = txtCheckIn.Text
            objBLLHotelFreeFormBooking.CheckOut = txtCheckOut.Text
            objBLLHotelFreeFormBooking.NoofRooms = ddlRoom.SelectedValue

            objBLLHotelFreeFormBooking.Adults = ddlAdult.SelectedValue
            objBLLHotelFreeFormBooking.Child = ddlChildren.SelectedValue
            objBLLHotelFreeFormBooking.RoomString = GetRoomString(objBLLHotelFreeFormBooking.NoofRooms)
            objBLLHotelFreeFormBooking.SupAgentCode = txtSupplierAgentCode.Text
            objBLLHotelFreeFormBooking.PartyCode = txtHotelCode.Text
            objBLLHotelFreeFormBooking.NonRefundable = IIf(chkNonrefundable.Checked = True, "1", "0")
            objBLLHotelFreeFormBooking.CancelFreeUpto = IIf(chkNonrefundable.Checked = True, "0", txtCancelFreeUpTo.Text)
            objBLLHotelFreeFormBooking.RateplanId = "FreeForm"
            objBLLHotelFreeFormBooking.RatePlanName = txtContract.Text
            Dim strRoomTypeCode As String = txtRoomTypeCode.Text
            If txtRoomTypeCode.Text <> "" Then
                Dim strRoomTypeCodes As String() = txtRoomTypeCode.Text.Trim.Split("|")
                strRoomTypeCode = strRoomTypeCodes(0)
                objBLLHotelFreeFormBooking.RoomTypeCode = strRoomTypeCodes(0)
                objBLLHotelFreeFormBooking.RoomClassCode = strRoomTypeCodes(1)
                objBLLHotelFreeFormBooking.RoomRankOrder = strRoomTypeCodes(2)
            End If
            objBLLHotelFreeFormBooking.MealPlans = txtMealPlanCode.Text
            Dim fSalePrice As Double = 0
            Dim fCostPrice As Double = 0
            Dim iTotalAdult As Integer = 0
            Dim iTotalChild As Integer = 0
            Dim strPriceBreakUp As New StringBuilder
            strPriceBreakUp.Append("<DocumentElement>")

            Dim liCostEntered As Boolean = False, liSaleEntered As Boolean = False



            If chkComplimentaryToCustomer.Checked = True Then
                objBLLHotelFreeFormBooking.Comp_Cust = "1"
            Else
                objBLLHotelFreeFormBooking.Comp_Cust = "0"
            End If
            If chkComplimentaryFromSupplier.Checked = True Then
                objBLLHotelFreeFormBooking.Comp_Supp = "1"
            Else
                objBLLHotelFreeFormBooking.Comp_Supp = "0"
            End If
            If chkComplimentaryArrivalTransfer.Checked = True Then
                objBLLHotelFreeFormBooking.Comparrtrf = "1"
            Else
                objBLLHotelFreeFormBooking.Comparrtrf = "0"
            End If
            If chkComplimentaryDepartureTransfer.Checked = True Then
                objBLLHotelFreeFormBooking.Compdeptrf = "1"
            Else
                objBLLHotelFreeFormBooking.Compdeptrf = "0"
            End If


            For Each dlItem As DataListItem In dltotalPriceBreak.Items
                Dim fRoomSalePrice As Double = 0
                Dim fRoomCostPrice As Double = 0
                Dim gvPricebreakup As GridView = dlItem.FindControl("gvPricebreakup")
                Dim lblRoomNo As Label = dlItem.FindControl("lblRoomNo")

                '---------------------------------------------
                'changed by mohamed on 28/08/2018
                liCostEntered = False
                liSaleEntered = False
                '---------------------------------------------

                For Each gvRow As GridViewRow In gvPricebreakup.Rows


                    Dim strRoomNo As String = lblRoomNo.Text

                    Dim txtsaleprice As TextBox = CType(gvRow.FindControl("txtsaleprice"), TextBox)
                    Dim txtBreakupTotalPrice As TextBox = CType(gvRow.FindControl("txtBreakupTotalPrice"), TextBox)
                    Dim lblBookingCode As TextBox = CType(gvRow.FindControl("lblBookingCode"), TextBox)

                    '---------------------------------------------
                    'changed by mohamed on 28/08/2018
                    'If txtsaleprice.Text = "" Or txtsaleprice.Text = "0" Or txtBreakupTotalPrice.Text = "" Or txtBreakupTotalPrice.Text = "0" Then
                    '    MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter sale price or cost price.")
                    '    Exit Sub
                    'End If



                    If (txtsaleprice.Text <> "" And txtsaleprice.Text <> "0") Or objBLLHotelFreeFormBooking.Comp_Cust = "1" Then
                        liSaleEntered = True
                    End If
                    If (txtBreakupTotalPrice.Text <> "" And txtBreakupTotalPrice.Text <> "0") Or objBLLHotelFreeFormBooking.Comp_Supp = "1" Then
                        liCostEntered = True
                    End If
                    '---------------------------------------------

                    '---------------------------------------------
                    'changed by mohamed on 28/08/2018
                    If liCostEntered = False Or liSaleEntered = False Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please enter sale price or cost price for Room " & strRoomNo)
                        Exit Sub
                    End If
                    '---------------------------------------------


                    Dim strRoomAdult As String = "ddlRoom" & strRoomNo & "Adult"
                    Dim ddlRoomAdult As DropDownList = FindControl(strRoomAdult)
                    iTotalAdult = iTotalAdult + ddlRoomAdult.SelectedValue
                    Dim strRoomChild As String = "ddlRoom" & strRoomNo & "Child"
                    Dim ddlRoomChild As DropDownList = FindControl(strRoomChild)
                    iTotalChild = iTotalChild + ddlRoomChild.SelectedValue

                    Dim strRoomAccomodationId As String = "txtRoom" & strRoomNo & "AccomodationCode"
                    Dim txtRoomAccomodationId As TextBox = FindControl(strRoomAccomodationId)

                    Dim strRoomAccomodationTypeId As String = "txtRoom" & strRoomNo & "AccomodationTypeCode"
                    Dim txtRoomAccomodationTypeId As TextBox = FindControl(strRoomAccomodationTypeId)

                    Dim strRoomAccomodation As String = "txtRoom" & strRoomNo & "Accomodation"
                    Dim txtRoomAccomodation As TextBox = FindControl(strRoomAccomodation)

                    Dim strRoomAccomodationType As String = "txtRoom" & strRoomNo & "AccomodationType"
                    Dim txtRoomAccomodationType As TextBox = FindControl(strRoomAccomodationType)


                    strPriceBreakUp.Append("<Table>")
                    strPriceBreakUp.Append("<rateplanid>" & objBLLHotelFreeFormBooking.RateplanId & "</rateplanid>")
                    strPriceBreakUp.Append("<rateplanname>" & objBLLHotelFreeFormBooking.RatePlanName & "</rateplanname>")
                    strPriceBreakUp.Append("<partycode>" & objBLLHotelFreeFormBooking.PartyCode & "</partycode>")
                    strPriceBreakUp.Append("<rmtypcode>" & objBLLHotelFreeFormBooking.RoomTypeCode & "</rmtypcode>")
                    strPriceBreakUp.Append("<roomclasscode>" & objBLLHotelFreeFormBooking.RoomClassCode & "</roomclasscode>")
                    strPriceBreakUp.Append("<rmtyporder>" & objBLLHotelFreeFormBooking.RoomRankOrder & "</rmtyporder>")
                    strPriceBreakUp.Append("<rmcatcode>" & txtRoomAccomodationTypeId.Text & "</rmcatcode>")
                    strPriceBreakUp.Append("<mealplans>" & objBLLHotelFreeFormBooking.MealPlans & "</mealplans>")
                    strPriceBreakUp.Append("<mealcode>" & objBLLHotelFreeFormBooking.MealPlans & "</mealcode>")
                    strPriceBreakUp.Append("<mealplannames>" & objBLLHotelFreeFormBooking.MealPlans & "</mealplannames>")
                    strPriceBreakUp.Append("<accommodationid>" & txtRoomAccomodationId.Text & "</accommodationid>")
                    strPriceBreakUp.Append("<roomno>" & strRoomNo & "</roomno>")
                    strPriceBreakUp.Append("<adults>" & ddlRoomAdult.SelectedValue & "</adults>")
                    strPriceBreakUp.Append("<child>" & ddlRoomChild.SelectedValue & "</child>")
                    strPriceBreakUp.Append("<childages>" & GetRoomChildAgeString(strRoomNo) & "</childages>")
                    Dim dt As DataTable = objBLLHotelFreeFormBooking.GetAccomodationMasterDetails(txtRoomAccomodationId.Text)
                    If ddlRoomChild.SelectedValue > 0 Then
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)("noofchildeb").ToString > 0 Then
                                strPriceBreakUp.Append("<sharingorextrabed>ExtraBed</sharingorextrabed>")
                            Else
                                strPriceBreakUp.Append("<sharingorextrabed>Sharing</sharingorextrabed>")
                            End If
                        Else
                            strPriceBreakUp.Append("<sharingorextrabed>Sharing</sharingorextrabed>")
                        End If
                    Else
                        strPriceBreakUp.Append("<sharingorextrabed></sharingorextrabed>")
                    End If


                    strPriceBreakUp.Append("<agecombination>" & txtRoomAccomodation.Text & "</agecombination>")


                    If dt.Rows.Count > 0 Then
                        strPriceBreakUp.Append("<priceadults>" & dt.Rows(0)("adults").ToString & "</priceadults>") '***
                        strPriceBreakUp.Append("<pricechild>" & dt.Rows(0)("child").ToString & "</pricechild>") '***
                        strPriceBreakUp.Append("<noofadulteb>" & dt.Rows(0)("noofadulteb").ToString & "</noofadulteb>") '***
                        strPriceBreakUp.Append("<noofchildeb>" & dt.Rows(0)("noofchildeb").ToString & "</noofchildeb>") '***
                    Else
                        strPriceBreakUp.Append("<priceadults>" & ddlRoomAdult.SelectedValue & "</priceadults>") '***
                        strPriceBreakUp.Append("<pricechild>" & ddlRoomChild.SelectedValue & "</pricechild>") '***
                        strPriceBreakUp.Append("<noofadulteb>0</noofadulteb>") '***
                        strPriceBreakUp.Append("<noofchildeb>0</noofchildeb>") '***
                    End If


                    Dim lblBreakupDate1 As Label = CType(gvRow.FindControl("lblBreakupDate1"), Label)
                    Dim strPriceDate As String = lblBreakupDate1.Text
                    If strPriceDate <> "" Then
                        Dim strDates As String() = strPriceDate.Split("/")
                        strPriceDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                    End If

                    strPriceBreakUp.Append("<Pricedate>" & strPriceDate & "</Pricedate>")



                    strPriceBreakUp.Append("<totalprice>" & IIf(txtBreakupTotalPrice.Text = "", "0", txtBreakupTotalPrice.Text) & "</totalprice>")

                    strPriceBreakUp.Append("<pricewithfreenight>0</pricewithfreenight>")
                    strPriceBreakUp.Append("<contractid></contractid>")
                    strPriceBreakUp.Append("<promotionid></promotionid>")
                    strPriceBreakUp.Append("<contpromid></contpromid>")
                    strPriceBreakUp.Append("<autoid>0</autoid>")
                    strPriceBreakUp.Append("<datelineno>1</datelineno>")
                    strPriceBreakUp.Append("<minstay></minstay>")
                    strPriceBreakUp.Append("<minstayoption></minstayoption>")
                    strPriceBreakUp.Append("<stayfor>0</stayfor>")
                    strPriceBreakUp.Append("<freenights>0</freenights>")
                    strPriceBreakUp.Append("<maxstay>0</maxstay>")
                    strPriceBreakUp.Append("<formulaid></formulaid>")
                    strPriceBreakUp.Append("<applymarkupid></applymarkupid>")
                    strPriceBreakUp.Append("<markupvalue>0</markupvalue>")
                    strPriceBreakUp.Append("<ctryformulaid></ctryformulaid>")
                    strPriceBreakUp.Append("<ctrymarkupvalue>0</ctrymarkupvalue>")
                    strPriceBreakUp.Append("<agformulaid></agformulaid>")
                    strPriceBreakUp.Append("<agmarkupvalue>0</agmarkupvalue>")

                    strPriceBreakUp.Append("<saleprice>" & IIf(txtsaleprice.Text = "", "0", txtsaleprice.Text) & "</saleprice>")
                    strPriceBreakUp.Append("<wlmarkupvalue>0</wlmarkupvalue>")
                    strPriceBreakUp.Append("<wlsaleprice>" & IIf(txtsaleprice.Text = "", "0", txtsaleprice.Text) & "</wlsaleprice>")
                    strPriceBreakUp.Append("<ctryapplymarkupid></ctryapplymarkupid>")
                    strPriceBreakUp.Append("<agapplymarkupid></agapplymarkupid>")
                    strPriceBreakUp.Append("<rmtypupgradefrom></rmtypupgradefrom>")
                    strPriceBreakUp.Append("<mealupgradefrom></mealupgradefrom>")
                    strPriceBreakUp.Append("<rmcatupgradefrom></rmcatupgradefrom>")
                    strPriceBreakUp.Append("<bookingcode>" & lblBookingCode.Text & "</bookingcode>") 'changed by mohamed on 02/08/2018

                    Dim lblConversionRate As Label = CType(gvRow.FindControl("lblConversionRate"), Label)
                    strPriceBreakUp.Append("<saleconvrate>" & lblConversionRate.Text & "</saleconvrate>") '***
                    Dim lblwlcurrcode As Label = CType(gvRow.FindControl("lblwlcurrcode"), Label)
                    strPriceBreakUp.Append("<wlcurrcode>" & lblwlcurrcode.Text & "</wlcurrcode>") '***
                    Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                    strPriceBreakUp.Append("<wlconvrate>" & lblwlconvrate.Text & "</wlconvrate>") '***
                    Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                    strPriceBreakUp.Append("<wlmarkupperc>" & lblwlmarkupperc.Text & "</wlmarkupperc>") '***lblwlmarkupperc
                    strPriceBreakUp.Append("<roomrate>" & IIf(txtBreakupTotalPrice.Text = "", "0", txtBreakupTotalPrice.Text) & "</roomrate>") '***
                    strPriceBreakUp.Append("<adultebprice>0</adultebprice>") '***
                    strPriceBreakUp.Append("<extrapaxprice>0</extrapaxprice>") '***
                    strPriceBreakUp.Append("<adultmealprice>0</adultmealprice>") '***
                    strPriceBreakUp.Append("<totalsharingcharge>0</totalsharingcharge>") '***
                    strPriceBreakUp.Append("<totalebcharge>0</totalebcharge>") '***
                    strPriceBreakUp.Append("<totalmealcharge>0</totalmealcharge>") '***
                    strPriceBreakUp.Append("<childmealdetails></childmealdetails>") '***
                    strPriceBreakUp.Append("<noofextrapax>0</noofextrapax>") '***
                    Dim lblVATPerc As Label = CType(gvRow.FindControl("lblVATPerc"), Label)
                    strPriceBreakUp.Append("<VATPerc>" & lblVATPerc.Text & "</VATPerc>") '***lblVATPerc
                    strPriceBreakUp.Append("<CostTaxableValue>0</CostTaxableValue>") '***
                    strPriceBreakUp.Append("<CostNonTaxableValue>0</CostNonTaxableValue>") '***
                    strPriceBreakUp.Append("<CostVATValue>0</CostVATValue>") '***
                    strPriceBreakUp.Append("<Cost_Price_Override>0</Cost_Price_Override>") '***
                    strPriceBreakUp.Append("</Table>")

                    Dim lblSalePriceCurrcode As Label = CType(gvRow.FindControl("lblSalePriceCurrcode"), Label)
                    objBLLHotelFreeFormBooking.SaleCurrCode = lblSalePriceCurrcode.Text
                    Dim lblCostPriceCurrcode As Label = CType(gvRow.FindControl("lblCostPriceCurrcode"), Label)
                    objBLLHotelFreeFormBooking.CostCurrCode = lblCostPriceCurrcode.Text



                    If txtsaleprice.Text <> "" Then
                        fSalePrice = fSalePrice + txtsaleprice.Text
                        fRoomSalePrice = fRoomSalePrice + txtsaleprice.Text
                    End If
                    If txtBreakupTotalPrice.Text <> "" Then
                        fRoomCostPrice = fRoomCostPrice + txtBreakupTotalPrice.Text
                        fCostPrice = fCostPrice + txtBreakupTotalPrice.Text
                    End If


                Next


            Next

            strPriceBreakUp.Append("</DocumentElement>")
            objBLLHotelFreeFormBooking.PriceBreakupXMLInput = strPriceBreakUp.ToString
            objBLLHotelFreeFormBooking.Adults = iTotalAdult.ToString
            objBLLHotelFreeFormBooking.Child = iTotalChild.ToString
            objBLLHotelFreeFormBooking.OverridePrice = "0"
            objBLLHotelFreeFormBooking.SaleValue = fSalePrice.ToString
            objBLLHotelFreeFormBooking.WlSaleValue = fSalePrice.ToString
            objBLLHotelFreeFormBooking.CostValue = fCostPrice.ToString




            objBLLHotelFreeFormBooking.Available = "1"
            Dim strSpecialEvents As New StringBuilder
            If chkSpecialEvents.Checked = True Then

                strSpecialEvents.Append("<DocumentElement>")
                For Each dlItem As DataListItem In dlSpecialEvents.Items
                    Dim fSpecialSalePrice As Double = 0
                    Dim fSpecialCostPrice As Double = 0

                    Dim ddlEvents As DropDownList = CType(dlItem.FindControl("ddlEvents"), DropDownList)
                    Dim txtSpecEventDate As TextBox = CType(dlItem.FindControl("txtSpecEventDate"), TextBox)
                    Dim chkSpclComplimentaryToCustomer As CheckBox = CType(dlItem.FindControl("chkSpclComplimentaryToCustomer"), CheckBox)
                    Dim chkSpclComplimentaryFromSupplier As CheckBox = CType(dlItem.FindControl("chkSpclComplimentaryFromSupplier"), CheckBox)
                    Dim strSDate As String = txtSpecEventDate.Text
                    If strSDate <> "" Then
                        Dim strDates As String() = strSDate.Split("/")
                        strSDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                    End If

                    If ddlEvents.SelectedValue = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any special event.")
                        Exit Sub
                    End If

                    If txtSpecEventDate.Text = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any special event date.")
                        Exit Sub
                    End If

                    Dim gvSpecialEvents As GridView = dlItem.FindControl("gvSpecialEvents")
                    For Each gvRow As GridViewRow In gvSpecialEvents.Rows
                        Dim txtPaxRate As TextBox = CType(gvRow.FindControl("txtPaxRate"), TextBox)
                        Dim txtPaxCost As TextBox = CType(gvRow.FindControl("txtPaxCost"), TextBox)
                        If txtPaxRate.Text = "" Or txtPaxCost.Text = "" Then
                            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any special event pax rate or pax cost.")
                            Exit Sub
                        End If

                        strSpecialEvents.Append("<Table>")
                        strSpecialEvents.Append("<evlineno>" & gvRow.RowIndex + 1 & "  </evlineno>")
                        strSpecialEvents.Append("<splistcode></splistcode>")
                        strSpecialEvents.Append("<splineno>" & dlItem.ItemIndex + 1 & "</splineno>")
                        strSpecialEvents.Append("<spleventcode>" & ddlEvents.SelectedValue & "</spleventcode>")
                        strSpecialEvents.Append("<compulsorytype>1</compulsorytype>")
                        strSpecialEvents.Append("<spleventdate>" & strSDate & "</spleventdate>")
                        Dim lblPaxtype As Label = CType(gvRow.FindControl("lblPaxtype"), Label)
                        strSpecialEvents.Append("<paxtype>" & lblPaxtype.Text & "</paxtype>")
                        Dim lblChildAges As Label = CType(gvRow.FindControl("lblChildAges"), Label)
                        strSpecialEvents.Append("<childage>" & lblChildAges.Text & "</childage>")
                        Dim lblNoOfPax As Label = CType(gvRow.FindControl("lblNoOfPax"), Label)
                        strSpecialEvents.Append("<noofpax>" & lblNoOfPax.Text & "</noofpax>")

                        strSpecialEvents.Append("<paxrate>" & txtPaxRate.Text & "</paxrate>")

                        fSpecialSalePrice = CType(txtPaxRate.Text, Decimal) * CType(lblNoOfPax.Text, Decimal)
                        strSpecialEvents.Append("<spleventvalue>" & fSpecialSalePrice.ToString & "</spleventvalue>")
                        strSpecialEvents.Append("<wlpaxrate>" & txtPaxRate.Text & "</wlpaxrate>")
                        strSpecialEvents.Append("<wlspleventvalue>" & fSpecialSalePrice.ToString & "</wlspleventvalue>")
                        Dim lblpaxcurrcode As Label = CType(gvRow.FindControl("lblpaxcurrcode"), Label)
                        strSpecialEvents.Append("<salecurrcode>" & lblpaxcurrcode.Text & "</salecurrcode>")

                        strSpecialEvents.Append("<paxcost>" & txtPaxCost.Text & "</paxcost>")
                        fSpecialCostPrice = CType(txtPaxCost.Text, Decimal) * CType(lblNoOfPax.Text, Decimal)

                        strSpecialEvents.Append("<spleventcostvalue>" & fSpecialCostPrice.ToString & "</spleventcostvalue>")
                        Dim lblcostCurrcode As Label = CType(gvRow.FindControl("lblcostCurrcode"), Label)
                        strSpecialEvents.Append("<costcurrcode>" & lblcostCurrcode.Text & "</costcurrcode>")
                        If chkSpclComplimentaryToCustomer.Checked = True Then
                            strSpecialEvents.Append("<comp_cust>1</comp_cust>")
                        Else
                            strSpecialEvents.Append("<comp_cust>0</comp_cust>")
                        End If
                        If chkSpclComplimentaryFromSupplier.Checked = True Then
                            strSpecialEvents.Append("<comp_supp>1</comp_supp>")
                        Else
                            strSpecialEvents.Append("<comp_supp>0</comp_supp>")
                        End If
                        Dim lblRoomNo As Label = CType(gvRow.FindControl("lblRoomNo"), Label)
                        strSpecialEvents.Append("<roomno>" & lblRoomNo.Text & "</roomno>")
                        Dim lblwlcurrcode As Label = CType(gvRow.FindControl("lblwlcurrcode"), Label)
                        strSpecialEvents.Append("<wlcurrcode>" & lblwlcurrcode.Text & "</wlcurrcode>")
                        Dim lblwlconvrate As Label = CType(gvRow.FindControl("lblwlconvrate"), Label)
                        strSpecialEvents.Append("<wlconvrate>" & lblwlconvrate.Text & "</wlconvrate>")
                        Dim lblwlmarkupperc As Label = CType(gvRow.FindControl("lblwlmarkupperc"), Label)
                        strSpecialEvents.Append("<wlmarkupperc>" & lblwlconvrate.Text & "</wlmarkupperc>")

                        strSpecialEvents.Append("</Table>")

                    Next

                Next
                strSpecialEvents.Append("</DocumentElement>")
            End If

            objBLLHotelFreeFormBooking.SpecialEventsXML = strSpecialEvents.ToString
            objBLLHotelFreeFormBooking.UserLogged = Session("GlobalUserName")

            Dim strStatus As String = objBLLHotelFreeFormBooking.SaveHotelFreeFormBooking()
            If strStatus = "True" Then
                Session("sRequestId") = objBLLHotelFreeFormBooking.Requestid
                Response.Redirect("MoreServices.aspx")
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelFreeFormBooking.aspx :: btnBook_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Shared Function GetNewOrExistingRequestId() As String
        Dim strRequestId As String = ""

        If Not HttpContext.Current.Session("sEditRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sEditRequestId")
        ElseIf Not HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = HttpContext.Current.Session("sRequestId")
        Else
            Dim objBLLHotelSearch2 As New BLLHotelSearch
            strRequestId = objBLLHotelSearch2.getrequestid()
        End If

        Return strRequestId
    End Function

    Private Sub FillRoomAdultChild(ByVal RoomString As String)
        Dim strroomstring As String() = RoomString.Split(";")
        Dim strroom As String()
        Dim strchildage As String()
        If strroomstring.Length > 0 Then
            Dim strRoomAdultName As String = ""
            Dim strRoomchildName As String = ""
            Dim strRoomchildage As String = ""
            Dim strRoomAccomodationCode As String = ""
            Dim strRoomAccomodation As String = ""
            Dim strRoomAccomodationTypeCode As String = ""
            Dim strRoomAccomodationType As String = ""

            For i = 0 To strroomstring.Length - 1
                strroom = strroomstring(i).Split(",")
                If strroom(1) <> "0" Then
                    strRoomAdultName = "ddlRoom" & strroom(0) & "Adult"
                    strRoomchildName = "ddlRoom" & strroom(0) & "Child"
                    strRoomAccomodationCode = "txtRoom" & strroom(0) & "AccomodationCode"
                    strRoomAccomodation = "txtRoom" & strroom(0) & "Accomodation"
                    strRoomAccomodationTypeCode = "txtRoom" & strroom(0) & "AccomodationTypeCode"
                    strRoomAccomodationType = "txtRoom" & strroom(0) & "AccomodationType"

                    Dim ddlRoomm As DropDownList = DirectCast(FindControl(strRoomAdultName), DropDownList)
                    Dim ddlRoomc As DropDownList = DirectCast(FindControl(strRoomchildName), DropDownList)
                    Dim txtRoomAccomodationCode As TextBox = DirectCast(FindControl(strRoomAccomodationCode), TextBox)
                    Dim txtRoomAccomodation As TextBox = DirectCast(FindControl(strRoomAccomodation), TextBox)
                    Dim txtRoomAccomodationTypeCode As TextBox = DirectCast(FindControl(strRoomAccomodationTypeCode), TextBox)
                    Dim txtRoomAccomodationType As TextBox = DirectCast(FindControl(strRoomAccomodationType), TextBox)

                    ddlRoomm.SelectedValue = strroom(1)
                    ddlRoomc.SelectedValue = strroom(2)

                    txtRoomAccomodationTypeCode.Text = strroom(4)
                    txtRoomAccomodationType.Text = strroom(4)

                    txtRoomAccomodationCode.Text = strroom(5)
                    Dim strQuery As String = "select min(agecombination) from booking_hotel_detail_pricestemp(nolock) where requestid='" & Session("sRequestId") & "' and accommodationid='" & strroom(5) & "' "
                    Dim objclsUtilities As New clsUtilities
                    txtRoomAccomodation.Text = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
                End If
                If strroom(2).ToString <> "0" Then

                    strchildage = strroom(3).Split("|")

                    For j = 0 To strchildage.Length - 1

                        strRoomchildage = "txtRoom" & strroom(0) & "Child" & j + 1
                        Dim txtRoomchild As TextBox = DirectCast(FindControl(strRoomchildage), TextBox)


                        txtRoomchild.Text = strchildage(j)
                    Next

                End If
            Next
        End If
    End Sub

    Private Sub BindDetailsForNewOrEditMode()

        If Not Session("sRequestId") Is Nothing Then
            Dim dtBookingHeader As DataTable
            dtBookingHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
            If dtBookingHeader.Rows.Count > 0 Then
                txtCountry.Text = dtBookingHeader.Rows(0)("sourcectryname").ToString
                txtCountryCode.Text = dtBookingHeader.Rows(0)("sourcectrycode").ToString
                txtCustomer.Text = dtBookingHeader.Rows(0)("agentname").ToString
                txtCustomerCode.Text = dtBookingHeader.Rows(0)("agentcode").ToString
                If Session("sLoginType") = "RO" Then
                    txtCustomer.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    txtCountry.Attributes.Add("onkeydown", "fnReadOnly(event)")
                    AutoCompleteExtender_txtCustomer.Enabled = False
                    AutoCompleteExtender_txtCountry.Enabled = False
                End If
            End If
        End If
        Dim objBLLHotelFreeFormBooking As New BLLHotelFreeFormBooking
        Dim dtSupAgent As DataTable = objBLLHotelFreeFormBooking.GetDefaultSupplierAgent()
        If dtSupAgent.Rows.Count > 0 Then
            txtSupplierAgent.Text = dtSupAgent.Rows(0)("supagentname").ToString
            txtSupplierAgentCode.Text = dtSupAgent.Rows(0)("supagentcode").ToString
        End If

        If Not Request.QueryString("RLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then

            Dim ds As DataSet

            ds = objBLLHotelFreeFormBooking.GetHotelFreeFormBookingDetailsForEdit(Session("sRequestId"), Request.QueryString("RLineNo"))
            If ds.Tables.Count > 0 Then

                If ds.Tables(1).Rows.Count > 0 Then 'changed by abin / mohamed on 04/08/2018
                    hdHotelAvailableForShifting.Value = "1"
                End If

                If ds.Tables(0).Rows.Count > 0 Then
                    hdOPMode.Value = "Edit"
                    chkShifting.Enabled = False

                    txtDestinationName.Text = ds.Tables(0).Rows(0)("destname").ToString
                    txtDestinationCode.Text = ds.Tables(0).Rows(0)("destinationcode").ToString & "|" & ds.Tables(0).Rows(0)("destinationtype").ToString
                    txtCheckIn.Text = ds.Tables(0).Rows(0)("vcheckin").ToString
                    txtCheckOut.Text = ds.Tables(0).Rows(0)("vcheckout").ToString
                    txtNoOfNights.Text = ds.Tables(0).Rows(0)("noofnights").ToString
                    ddlRoom.SelectedValue = ds.Tables(0).Rows(0)("noofrooms").ToString


                    FillShiftingRoomAdultChild(ds.Tables(0).Rows(0)("roomstring").ToString)
                    Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
                    Dim javaScriptChldrn1 As String = "<script type='text/javascript'>ShowAdultChild();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

                    txtCustomer.Text = ds.Tables(0).Rows(0)("agentname").ToString
                    txtCustomerCode.Text = ds.Tables(0).Rows(0)("agentcode").ToString
                    txtCountryCode.Text = ds.Tables(0).Rows(0)("sourcectrycode").ToString
                    txtCountry.Text = ds.Tables(0).Rows(0)("ctryname").ToString

                    txtHotelName.Text = ds.Tables(0).Rows(0)("hotelname").ToString
                    txtHotelCode.Text = ds.Tables(0).Rows(0)("partycode").ToString


                    AutoCompleteExtendertxtRoomType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtenderRoom1AccomodationType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtenderRoom2AccomodationType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtenderRoom3AccomodationType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtenderRoom4AccomodationType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtenderRoom5AccomodationType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtenderRoom6AccomodationType.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString
                    AutoCompleteExtendertxtMealPlan.ContextKey = ds.Tables(0).Rows(0)("partycode").ToString

                    txtSupplierAgent.Text = ds.Tables(0).Rows(0)("supagentname").ToString
                    txtSupplierAgentCode.Text = ds.Tables(0).Rows(0)("supagentcode").ToString

                    txtContract.Text = ds.Tables(0).Rows(0)("rateplanname").ToString
                    txtRoomTypeCode.Text = ds.Tables(0).Rows(0)("rmtypcode").ToString & "|" & ds.Tables(0).Rows(0)("roomclasscode").ToString & "|" & ds.Tables(0).Rows(0)("rmtyporder").ToString
                    txtRoomType.Text = ds.Tables(0).Rows(0)("rmtypname").ToString

                    txtMealPlan.Text = ds.Tables(0).Rows(0)("mealplans").ToString
                    txtMealPlanCode.Text = ds.Tables(0).Rows(0)("mealplans").ToString
                    If ds.Tables(0).Rows(0)("nonrefundable").ToString = "1" Then
                        chkNonrefundable.Checked = True
                        txtCancelFreeUpTo.Text = "0"
                    Else
                        txtCancelFreeUpTo.Text = ds.Tables(0).Rows(0)("cancelfreeupto").ToString
                    End If
                    FillRoomAdultChild(ds.Tables(0).Rows(0)("roomstring").ToString)

                    If ds.Tables(0).Rows(0)("comp_cust").ToString Then
                        chkComplimentaryToCustomer.Checked = True
                    Else
                        chkComplimentaryToCustomer.Checked = False
                    End If
                    If ds.Tables(0).Rows(0)("comp_supp").ToString Then
                        chkComplimentaryFromSupplier.Checked = True
                    Else
                        chkComplimentaryFromSupplier.Checked = False
                    End If
                    If ds.Tables(0).Rows(0)("comparrtrf").ToString Then
                        chkComplimentaryArrivalTransfer.Checked = True
                    Else
                        chkComplimentaryArrivalTransfer.Checked = False
                    End If
                    If ds.Tables(0).Rows(0)("compdeptrf").ToString Then
                        chkComplimentaryDepartureTransfer.Checked = True
                    Else
                        chkComplimentaryDepartureTransfer.Checked = False
                    End If

                    If ds.Tables(0).Rows(0)("shiftto").ToString = "1" Then

                        dvShifting.Attributes.Add("style", "display:block;")
                        txtShiftHotel.Text = ds.Tables(0).Rows(0)("ShiftHotelName").ToString
                        txtShiftHotelCode.Text = ds.Tables(0).Rows(0)("ShiftFromPartyCodeAndLineNo").ToString
                        ''chkShifting.Enabled = False
                        chkShifting.Checked = True

                        Dim strScript As String = "javascript: CheckShiftingCheckbox('" + chkShifting.ClientID + "')"
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)

                        ''Dim strScript As String = "javascript: CallPriceSlider();"
                        ''ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)

                    Else
                        chkShifting.Checked = False
                        dvShifting.Attributes.Add("style", "display:none;")
                    End If
                End If

                If ds.Tables(1).Rows.Count > 0 Then
                    Session("sdtFreeFormRoomType") = ds.Tables(1)
                    Dim dvFreeFormRoomType As DataView = New DataView(ds.Tables(1))
                    dltotalPriceBreak.DataSource = dvFreeFormRoomType.ToTable(True, "roomno", "RoomHeading", "saletotal", "costtotal")
                    dltotalPriceBreak.DataBind()

                    dvPriceDetails.Visible = True
                    FillddlRoomNos(ddlRoom.SelectedValue)
                    dvPriceDetails.Visible = True

                End If
                If ds.Tables(3).Rows.Count > 0 Then
                    chkSpecialEvents.Checked = True
                    pnlSpecialEvents.Visible = True

                    Dim dsSp As New DataSet

                    dsSp.Merge(ds.Tables(2))
                    dsSp.Merge(ds.Tables(3))
                    dsSp.Merge(ds.Tables(4))


                    Session("sdsFreeFormSpecialEvents") = dsSp
                    dlSpecialEvents.DataSource = dsSp.Tables(0)
                    dlSpecialEvents.DataBind()
                    pnlSpecialEvents.Attributes.Add("style", "display:block")
                Else

                End If

                '------------------------
                'Changed by mohamed on 06/09/2019
                Dim strHotelString As String = "1,DBL,2,0,0"
                Dim strSelectedEvents As String = ""
                Dim strRoomTypeCode As String = txtRoomTypeCode.Text
                If txtRoomTypeCode.Text <> "" Then
                    Dim strRoomTypeCodes As String() = txtRoomTypeCode.Text.Split("|")
                    strRoomTypeCode = strRoomTypeCodes(0)
                End If

                Dim strRoomCatCode As String = txtRoom1AccomodationTypeCode.Text
                Dim strRoom As String = ddlRoom.SelectedValue
                Dim strRoomString As String = GetRoomString(strRoom)
                Dim dsFreeFormSpecialEvents As DataSet = objBLLHotelFreeFormBooking.GetFreeFormSpecialEvents(txtHotelCode.Text, strRoomTypeCode, txtMealPlan.Text, strRoomCatCode, txtCustomerCode.Text, txtCountryCode.Text, txtCheckIn.Text, txtCheckOut.Text, ddlRoom.SelectedValue, strRoomString, strHotelString, strSelectedEvents, Session("sRequestId"), Request.QueryString("RLineNo"))
                Session("sdsFreeFormSpecialEvents") = dsFreeFormSpecialEvents
                If Not dsFreeFormSpecialEvents Is Nothing Then
                    If dsFreeFormSpecialEvents.Tables(0).Rows.Count > 0 Then
                        pnlSpecialEvents.Visible = True
                        dlSpecialEvents.DataSource = dsFreeFormSpecialEvents.Tables(0)
                        dlSpecialEvents.DataBind()
                    Else
                        dlSpecialEvents.DataBind()
                        pnlSpecialEvents.Visible = True
                    End If
                End If
                '------------------------
            End If
        ElseIf Request.QueryString("RLineNo") Is Nothing And Not Session("sRequestId") Is Nothing Then
            Dim dsBooking As DataSet
            dsBooking = objBLLCommonFuntions.GetTempFullBookingDetails(Session("sRequestId"))

            If dsBooking.Tables.Count > 0 Then
                If dsBooking.Tables(1).Rows.Count > 0 Then 'changed by abin / mohamed on 04/08/2018
                    hdHotelAvailableForShifting.Value = "1"
                Else
                    chkShifting.Checked = False
                    dvShifting.Attributes.Add("style", "display:none;")
                End If
            End If
        Else 'changed by mohamed on 02/08/2018
            If Session("sRequestId") Is Nothing Then
                dvShifting.Attributes.Add("style", "display:none;")
            End If
        End If
    End Sub

    ''' <summary>
    ''' <CheckShiftingAvailable/>
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckShiftingAvailable()
        Dim dt As DataTable
        'changed by mohamed on 05/04/2018
        'dt = objBLLHotelSearch.GetHotelBookingDetailsForShifting(Session("sRequestId").ToString)
        dt = objBLLHotelSearch.GetHotelBookingDetailsForShiftingNew(Session("sRequestId").ToString, -1)

        If dt.Rows.Count > 0 Then
            dvShifting.Attributes.Add("style", "display:block;")
            'If dt.Rows.Count = 1 Then 'changed by mohamed on 08/04/2018
            'changed by mohamed on 08/04/2018
            'txtShiftHotel.Text = dt.Rows(0)("HotelName").ToString
            'txtShiftHotelCode.Text = dt.Rows(0)("partycode").ToString & "|" & dt.Rows(0)("rlineno").ToString 'dt.Rows(0)("code").ToString 'changed by mohamed on 05/04/2018
            'txtCheckIn.Text = dt.Rows(0)("checkout").ToString
            'ddlRoom.SelectedValue = dt.Rows(0)("noofrooms").ToString
            'FillShiftingRoomAdultChild(dt.Rows(0)("RoomString").ToString)
            'Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            'Dim javaScriptChldrn1 As String = "<script type='text/javascript'>ShowAdultChild();</script>"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)

            'Else 'changed by mohamed on 08/04/2018
            ddlRoom.SelectedValue = dt.Rows(0)("noofrooms").ToString
            FillShiftingRoomAdultChild(dt.Rows(0)("RoomString").ToString)
            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            Dim javaScriptChldrn1 As String = "<script type='text/javascript'>ShowAdultChild();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
            'End If 'changed by mohamed on 08/04/2018
        Else
            dvShifting.Attributes.Add("style", "display:none;")
        End If
    End Sub

    Protected Sub btnSelectShiftHotel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectShiftHotel.Click
        'changed by mohamed on 05/04/2018
        Try
            Dim lsRlineno As String = -1
            Dim strSqlQry As String = ""
            Dim myDS As New DataSet
            Dim Hotelnames As New List(Of String)
            If Request.QueryString("RLineNo") IsNot Nothing Then
                lsRlineno = Request.QueryString("RLineNo")
            End If
            strSqlQry = "execute sp_get_shifting_hotel_detail '" & Session("sRequestId").ToString & "', " & lsRlineno
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            'changed by mohamed on 29/08/2018
            btnShiftHotelSave.Style.Remove("Float")
            If chkShifting.Checked = True Then
                lblShiftingMessageToUser.Visible = False
                btnShiftHotelSave.Style.Add("Float", "right")
            Else
                lblShiftingMessageToUser.Visible = True
                btnShiftHotelSave.Style.Add("Float", "left")
            End If

            If myDS.Tables(0).Rows.Count > 0 Then
                'For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                '    'Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString()))
                '    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("HotelName").ToString(), myDS.Tables(0).Rows(i)("partycode").ToString() + "|" + myDS.Tables(0).Rows(i)("rlineno").ToString() + "|" + Replace(myDS.Tables(0).Rows(i)("roomstring").ToString(), "|", "&s") + "**" + myDS.Tables(0).Rows(i)("checkout").ToString()))
                'Next
                dlShiftHotelBreak.DataSource = myDS.Tables(0)
                dlShiftHotelBreak.DataBind()
            End If

            mpShiftHotel1.Show()
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPreHotelSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub btnShiftHotelSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShiftHotelSave.Click
        Try
            Dim lsHotelName As String = "", lsPartyName As String = ""
            Dim lsPartyCode As String = "", lsCheckOut As String = "", lsRlineNo As String = "", lsRoomString As String = "", liNoOfRooms As Integer = 0
            Dim liNights As Integer = 0, lsCheckIn As String = ""
            Dim lbSelectedAnyLine As Boolean = False
            For Each dlShiftHotelBreakItem As DataListItem In dlShiftHotelBreak.Items
                Dim chkSelect As CheckBox = dlShiftHotelBreakItem.FindControl("chkSelect")
                Dim lblPartyCode As Label = dlShiftHotelBreakItem.FindControl("lblPartyCode")
                Dim lblCheckout As Label = dlShiftHotelBreakItem.FindControl("lblCheckout")
                Dim lblRlineNo As Label = dlShiftHotelBreakItem.FindControl("lblRlineNo")
                Dim lblRoomString As Label = dlShiftHotelBreakItem.FindControl("lblRoomString")
                Dim lblNoOfRooms As Label = dlShiftHotelBreakItem.FindControl("lblNoOfRooms")
                Dim lblHotelName As Label = dlShiftHotelBreakItem.FindControl("lblHotelName")
                Dim lblCheckIn As Label = dlShiftHotelBreakItem.FindControl("lblCheckIn")
                Dim lblnights As Label = dlShiftHotelBreakItem.FindControl("lblnights")
                Dim lblpartyname As Label = dlShiftHotelBreakItem.FindControl("lblpartyname")

                If chkSelect.Checked = True Then
                    lbSelectedAnyLine = True
                    If lsPartyCode = "" Then
                        lsPartyCode = lblPartyCode.Text
                        lsCheckOut = lblCheckout.Text
                        lsCheckIn = lblCheckIn.Text
                        liNights = Val(lblnights.Text)
                        lsPartyName = lblpartyname.Text
                    ElseIf lblPartyCode.Text.Trim.ToUpper <> lsPartyCode.Trim Or lblCheckout.Text.Trim.ToUpper <> lsCheckOut.Trim Then
                        MessageBox.ShowMessage(Page, MessageType.Errors, "Multiple Hotel or different check out cannot be selected")
                        mpShiftHotel1.Show()
                        GoTo lblExitPos
                    End If
                    Dim lsRLineNos As String(), lsRLineNoToCheck As String
                    lsRLineNos = lblRlineNo.Text.Split(",")
                    For li = LBound(lsRLineNos) To UBound(lsRLineNos)
                        lsRLineNoToCheck = lsRLineNos(li)
                        If InStr(lsRLineNoToCheck, "-") > 0 Then
                            lsRLineNoToCheck = Left(lsRLineNoToCheck, InStr(lsRLineNoToCheck, "-") - 1)
                        End If
                        If InStr("," + lsRlineNo + ",", "," + lsRLineNoToCheck + ",") > 0 Then
                            MessageBox.ShowMessage(Page, MessageType.Errors, "The same line has been selected multiple time, please remove in one place")
                            mpShiftHotel1.Show()
                            GoTo lblExitPos
                        End If
                    Next
                    lsHotelName = lsHotelName & IIf(lsHotelName = "", "", ";") & lblHotelName.Text
                    lsRlineNo = lsRlineNo & IIf(lsRlineNo = "", "", ",") & lblRlineNo.Text
                    lsRoomString = lsRoomString & IIf(lsRoomString = "", "", ";") & lblRoomString.Text
                    liNoOfRooms += Val(lblNoOfRooms.Text)
                End If
            Next

            lsRoomString = fnReArrangeRoomString(lsRoomString)
            Dim javaScriptChldrn1 As String
            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript2"
            If lbSelectedAnyLine = True Then
                'txtShiftHotel.Text = lsHotelName
                'txtShiftHotelCode.Text = lsPartyCode & "|" & lsRlineNo
                'txtCheckIn.Enabled = True
                'Dim strScript As String = "javascript: ShiftingCallSuccess(""" & lsRoomString & "**" & lsCheckOut & """);"
                'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)
                'Dim scriptKey As String = "UniqueKeyForThisScript"
                'Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

                txtCheckIn.Enabled = True
                txtShiftHotel.Text = lsHotelName
                txtShiftHotelCode.Text = lsPartyCode & "|" & lsRlineNo
                If chkShifting.Checked = False Then
                    txtCheckIn.Text = lsCheckIn
                    txtCheckOut.Text = lsCheckOut
                    txtHotelCode.Text = lsPartyCode
                    txtHotelName.Text = lsPartyName
                    txtNoOfNights.Text = liNights
                    javaScriptChldrn1 = "<script type='text/javascript'>ShowAdultChild(); fnLockControlsForShifting(); HotelNameAutoCompleteSelectedInLoadForShifting('" & lsPartyCode & "');  </script>"
                Else
                    txtCheckIn.Text = lsCheckOut
                    javaScriptChldrn1 = "<script type='text/javascript'>ShowAdultChild(); fnLockControlsForShifting();</script>"
                End If
                ddlRoom.SelectedValue = liNoOfRooms
                FillShiftingRoomAdultChild(lsRoomString)

                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
            Else
                txtShiftHotel.Text = ""
                txtShiftHotelCode.Text = ""

                'changed by mohamed on 11/04/2018
                javaScriptChldrn1 = "<script type='text/javascript'>fnLockControlsForShifting();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn1)
            End If

            Exit Sub
lblExitPos:

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: btnPreHotelSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Protected Sub dlShiftHotelBreak_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlShiftHotelBreak.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblRlineNo As Label = CType(e.Item.FindControl("lblRlineNo"), Label)
                Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
                Dim lsShiftDet As String() = txtShiftHotelCode.Text.Split("|")
                If lsShiftDet.GetUpperBound(0) >= 1 Then
                    Dim lsRlineNos As String() = lsShiftDet(1).Split(",")
                    For li = lsRlineNos.GetLowerBound(0) To lsRlineNos.GetUpperBound(0)
                        If lsRlineNos(li) = lblRlineNo.Text Then
                            chkSelect.Checked = True
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("HotelSearch.aspx :: dlShiftHotelBreak_ItemDataBound :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    Function fnReArrangeRoomString(ByVal lsRoomString As String) As String
        Dim lsRoomStringNew As String = ""
        Dim lsRoomStrInd As String = ""
        Dim lsRoomStrArr As String() = lsRoomString.Split(";")
        Dim liRoomNo As Integer = 0
        For li As Integer = lsRoomStrArr.GetLowerBound(0) To lsRoomStrArr.GetUpperBound(0)
            liRoomNo += 1
            lsRoomStrInd = lsRoomStrArr(li)
            If lsRoomStrInd.Trim <> "" Then
                lsRoomStrInd = liRoomNo & Mid(lsRoomStrInd, InStr(1, lsRoomStrInd, ","))
                lsRoomStringNew = lsRoomStringNew & IIf(lsRoomStringNew = "", "", ";") & lsRoomStrInd
            End If
        Next

        Return lsRoomStringNew

    End Function
End Class
