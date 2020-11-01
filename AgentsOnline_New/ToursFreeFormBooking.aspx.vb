Imports System.Data
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System
Imports System.Web.UI.HtmlControls

Partial Class TourSearch
    Inherits System.Web.UI.Page
    Public objBLLTourFreeFormBooking As New BLLTourFreeFormBooking
    Dim objBLLMenu As New BLLMenu


    Dim objBLLLogin As New BLLLogin
    Dim objclsUtilities As New clsUtilities
    Dim objBLLHome As New BLLHome
    Dim objBLLTourSearch As New BLLTourSearch
    Dim objBLLCommonFuntions As New BLLCommonFuntions
    Dim iBookNowFlag As Integer = 0
    Dim iRatePlan As Integer = 0
    Dim iCumulative As Integer = 0
    Private PageSize As Integer = 5
    Private dlcolumnRepeatFlag As Integer = 0
    Dim objUtil As New clsUtils
    Shared tourfromdate As String
    Shared tourtodate As String
    Dim objResParam As New ReservationParameters



    Private Sub btnTourFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTourFill.Click
        Try

            gvMultiCost.DataBind()
            Session("sdtMultiCost") = Nothing

            Dim divSelectComboDates As HtmlGenericControl = CType(FindControl("divSelectComboDates"), HtmlGenericControl)
            divSelectComboDates.Style.Item("display") = "none"

            Dim divSelectMultipleDates As HtmlGenericControl = CType(FindControl("divSelectMultipleDates"), HtmlGenericControl)
            divSelectMultipleDates.Style.Item("display") = "none"

            Dim divSelectNormal As HtmlGenericControl = CType(FindControl("divSelectNormal"), HtmlGenericControl)
            divSelectNormal.Style.Item("display") = "none"

            Dim divComplimentary As HtmlGenericControl = CType(FindControl("divComplimentary"), HtmlGenericControl)
            divComplimentary.Style.Item("display") = "none"

            Dim divTotalSale As HtmlGenericControl = CType(FindControl("divTotalSale"), HtmlGenericControl)
            divTotalSale.Style.Item("display") = "none"

            Dim divAdult As HtmlGenericControl = CType(FindControl("divAdult"), HtmlGenericControl)
            divAdult.Style.Item("display") = "none"

            'Dim divClearAdult As HtmlGenericControl = CType(FindControl("divClearAdult"), HtmlGenericControl)
            'divClearAdult.Style.Item("display") = "none"

            Dim divChild As HtmlGenericControl = CType(FindControl("divChild"), HtmlGenericControl)
            divChild.Style.Item("display") = "none"

            Dim divSenior As HtmlGenericControl = CType(FindControl("divSenior"), HtmlGenericControl)
            divSenior.Style.Item("display") = "none"

            Dim divpTotal As HtmlGenericControl = CType(FindControl("divpTotal"), HtmlGenericControl)
            divpTotal.Style.Item("display") = "none"

            Dim divpsenior As HtmlGenericControl = CType(FindControl("divpsenior"), HtmlGenericControl)
            divpsenior.Style.Item("display") = "none"

            Dim divChildAsAdult As HtmlGenericControl = CType(FindControl("divChildAsAdult"), HtmlGenericControl)
            divChildAsAdult.Style.Item("display") = "none"

            Dim divUnits As HtmlGenericControl = CType(FindControl("divUnits"), HtmlGenericControl)
            divUnits.Style.Item("display") = "none"


            Dim divCostAdult As HtmlGenericControl = CType(FindControl("divCostAdult"), HtmlGenericControl)
            divCostAdult.Style.Item("display") = "none"


            Dim divCostChild As HtmlGenericControl = CType(FindControl("divCostChild"), HtmlGenericControl)
            divCostChild.Style.Item("display") = "none"

            Dim divCostSenior As HtmlGenericControl = CType(FindControl("divCostSenior"), HtmlGenericControl)
            divCostSenior.Style.Item("display") = "none"

            Dim divCostUnits As HtmlGenericControl = CType(FindControl("divCostUnits"), HtmlGenericControl)
            divCostUnits.Style.Item("display") = "none"

            Dim divCostChildAsAdult As HtmlGenericControl = CType(FindControl("divCostChildAsAdult"), HtmlGenericControl)
            divCostChildAsAdult.Style.Item("display") = "none"


            Dim divCostArea As HtmlGenericControl = CType(FindControl("divCostArea"), HtmlGenericControl)
            divCostArea.Style.Item("display") = "none"


            Dim divCostpTotal As HtmlGenericControl = CType(FindControl("divpCostTotal"), HtmlGenericControl)
            divCostpTotal.Style.Item("display") = "none"



            dlSelectComboDates.DataSource = Nothing
            dlSelectComboDates.DataBind()
            'Chk_Complimentary.Checked = False
            dlMultipleDate.DataSource = Nothing
            dlMultipleDate.DataBind()

            Lbl_CurCodeAdult.Text = ""
            Lbl_CurCodeChAdult.Text = ""
            Lbl_CurCodeChild.Text = ""
            Lbl_CurCodeUnit.Text = ""
            Lbl_CurCodeSenior.Text = ""

            Txt_NoOfSeniors.Text = ""
            Txt_PriceSenior.Text = ""
            Txt_SaleValueSenior.Text = ""
            Txt_NoOfAdults.Text = ""
            Txt_PriceAdult.Text = ""
            Txt_SaleValueAdult.Text = ""
            Txt_NoOfChild.Text = ""
            Txt_PriceChild.Text = ""
            Txt_SaleValueChild.Text = ""
            Txt_NoOfChildAsAdult.Text = ""
            Txt_PriceChildAsAdult.Text = ""
            Txt_SaleValueChildAsAdult.Text = ""
            Txt_SaleValueUnits.Text = ""
            Txt_PriceUnits.Text = ""
            Txt_NoOfUnits.Text = ""


            Txt_CostNoOfSeniors.Text = ""
            Txt_CostPriceSenior.Text = ""
            Txt_CostSaleValueSenior.Text = ""
            Txt_CostNoOfAdults.Text = ""
            Txt_CostPriceAdult.Text = ""
            Txt_CostSaleValueAdult.Text = ""
            Txt_CostNoOfChild.Text = ""
            Txt_CostPriceChild.Text = ""
            Txt_CostSaleValueChild.Text = ""
            Txt_CostNoOfChildAsAdult.Text = ""
            Txt_CostPriceChildAsAdult.Text = ""
            Txt_CostSaleValueChildAsAdult.Text = ""
            Txt_CostSaleValueUnits.Text = ""
            Txt_CostPriceUnits.Text = ""
            Txt_CostNoOfUnits.Text = ""

            Txt_TotalCostValue.Text = ""
            Txt_TotalSaleValue.Text = ""

            objBLLTourFreeFormBooking.ChildAgeString = ""

            hdTourRateBasis.Value = ""

            objBLLTourFreeFormBooking.AgentCode = Session("sAgentCode")

            Dim strAdult As String = ddlTourAdult.SelectedValue
            Dim strChildren As String = ddlTourChildren.SelectedValue

            Dim strChild1 As String = "0"
            Dim strChild2 As String = "0"
            Dim strChild3 As String = "0"
            Dim strChild4 As String = "0"
            Dim strChild5 As String = "0"
            Dim strChild6 As String = "0"
            Dim strChild7 As String = "0"
            Dim strChild8 As String = "0"
            Dim intChAsAdult As Integer = 0

            'strChild1 = txtTourChild1.Text
            'strChild2 = txtTourChild2.Text
            'strChild3 = txtTourChild3.Text
            'strChild4 = txtTourChild4.Text
            'strChild5 = txtTourChild5.Text
            'strChild6 = txtTourChild6.Text
            'strChild7 = txtTourChild7.Text
            'strChild8 = txtTourChild8.Text


            If strAdult = "0" Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any adult.")
                Exit Sub
            End If
            If strChildren <> "0" Then
                If strChildren = "1" Then
                    If strChild1 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If

                    strChild1 = txtTourChild1.Text
                    strChild2 = ""
                    strChild3 = ""
                    strChild4 = ""
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1
                ElseIf strChildren = "2" Then
                    If strChild1 = "" Or strChild2 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If

                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = ""
                    strChild4 = ""
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = ""
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = txtTourChild4.Text
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = txtTourChild4.Text
                    strChild5 = txtTourChild5.Text
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = txtTourChild4.Text
                    strChild5 = txtTourChild5.Text
                    strChild6 = txtTourChild6.Text
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6

                    'ElseIf strChildren = "7" Then
                    '    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Then
                    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                    '        Exit Sub
                    '    End If
                    'ElseIf strChildren = "8" Then
                    '    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Or strChild8 = "" Then
                    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                    '        Exit Sub
                    '    End If
                End If

            End If


            hdChildAgeString.Value = objBLLTourFreeFormBooking.ChildAgeString

            If Txt_TourType.Text = "COMBO" Then
                'lblComboExcName.Text = lblExcName.Text
                hdExcCodeComboPopup.Value = Txt_ToursCode.Text
                'hdVehicleCodeComboPopup.Value = hdVehicleCode.Value
                Dim dt As New DataTable
                dt = objBLLTourSearch.GetComboExcursions_WithRateBasis(Txt_ToursCode.Text)
                dlSelectComboDates.DataSource = dt
                dlSelectComboDates.DataBind()
                If dlSelectComboDates.Items.Count > 0 Then
                    Dim dtCombo As New DataTable
                    dtCombo = Session("selectedCombotourdatatable")
                    For Each dlItem1 As DataListItem In dlSelectComboDates.Items
                        Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
                        Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
                        Dim foundRow As DataRow
                        foundRow = dtCombo.Select("exctypcode='" & Txt_ToursCode.Text.Trim & "' AND  vehiclecode='" & Txt_ToursCode.Text & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='COMBO' ").FirstOrDefault
                        If Not foundRow Is Nothing Then
                            txtExcComboDate.Text = foundRow("excdate")
                        End If
                    Next
                    hdTourRateBasis.Value = dt.Rows(0)("ratebasis").ToString

                    hdChangeFromdate.Value = DateTime.Now
                    'hdChangeTodate.Value = 0

                    If dt.Rows(0)("ratebasis").ToString = "ACS" Then
                        divUnits.Style.Item("display") = "none"

                        divAdult.Style.Item("display") = "block"
                        'divClearAdult.Style.Item("display") = "block"

                        divCostUnits.Style.Item("display") = "none"
                        divCostAdult.Style.Item("display") = "block"

                    Else
                        divUnits.Style.Item("display") = "block"
                        divCostUnits.Style.Item("display") = "block"
                        Txt_NoOfUnits.Text = 1
                    End If

                    Dim fromAge As Decimal = dt.Rows(0)("chidlagefrom").ToString()
                    Dim toAge As Decimal = dt.Rows(0)("childageto").ToString()

                    If strChild1 <> "" Then
                        If CType(strChild1, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild2 <> "" Then
                        If CType(strChild2, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild3 <> "" Then
                        If CType(strChild3, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild4 <> "" Then
                        If CType(strChild4, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild5 <> "" Then
                        If CType(strChild5, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild6 <> "" Then
                        If CType(strChild6, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild7 <> "" Then
                        If CType(strChild7, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If

                    Txt_NoOfAdults.Text = ddlTourAdult.SelectedValue.ToString()
                    Txt_NoOfSeniors.Text = ddlSeniorCitizen.SelectedValue.ToString()
                    Txt_NoOfChild.Text = CType(CType(ddlTourChildren.SelectedValue.ToString(), Integer) - intChAsAdult, String)
                    Txt_NoOfChildAsAdult.Text = intChAsAdult.ToString()

                    Txt_CostNoOfAdults.Text = ddlTourAdult.SelectedValue.ToString()
                    Txt_CostNoOfSeniors.Text = ddlSeniorCitizen.SelectedValue.ToString()
                    Txt_CostNoOfChild.Text = CType(CType(ddlTourChildren.SelectedValue.ToString(), Integer) - intChAsAdult, String)
                    Txt_CostNoOfChildAsAdult.Text = intChAsAdult.ToString()

                    Lbl_CurCodeAdult.Text = hdCurrCode.Value
                    Lbl_CurCodeChAdult.Text = hdCurrCode.Value
                    Lbl_CurCodeChild.Text = hdCurrCode.Value
                    Lbl_CurCodeUnit.Text = hdCurrCode.Value
                    Lbl_CurCodeSenior.Text = hdCurrCode.Value

                    Lbl_CostCurCodeAdult.Text = "AED"
                    Lbl_CostCurCodeChAdult.Text = "AED"
                    Lbl_CostCurCodeChild.Text = "AED"
                    Lbl_CostCurCodeUnit.Text = "AED"
                    Lbl_CostCurCodeSenior.Text = "AED"

                    divSelectComboDates.Style.Item("display") = "block"
                    btnSaveComboExcursion.Visible = True
                    divComplimentary.Style.Item("display") = "block"
                    divTotalSale.Style.Item("display") = "block"
                    divpsenior.Style.Item("display") = "block"
                    divpTotal.Style.Item("display") = "block"


                    divTotalCost.Style.Item("display") = "block"
                    divCostArea.Style.Item("display") = "block"
                    divpCostTotal.Style.Item("display") = "block"

                    If dt.Rows(0)("ratebasis").ToString = "ACS" Then
                        If Val(Txt_NoOfChild.Text.Trim()) > 0 Then
                            divChild.Style.Item("display") = "block"
                            divCostChild.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfSeniors.Text.Trim()) > 0 Then
                            divSenior.Style.Item("display") = "block"
                            divCostSenior.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0 Then
                            divChildAsAdult.Style.Item("display") = "block"
                            divCostChildAsAdult.Style.Item("display") = "block"
                        End If
                    End If
                Else
                    'lblSelectMultipleDates.Visible = False
                End If

            End If
            If Txt_TourType.Text = "MULTIPLE DATE" Then
                ' ''            dlMultipleDate.DataSource = dtDates
                ' ''            dlMultipleDate.DataBind()
                ' ''            lblSelectMultipleDates.Text = "Excursion: " & lblExcName.Text
                ' ''            hdMealPlanExcCode.Value = hdExcCode.Value
                ' ''            hdMealPlanVehicleCode.Value = hdVehicleCode.Value
                'hdChangeFromdate
                'hdChangeTodate
                Dim dsDates As DataSet
                ' ''dtDates = objBLLTourSearch.GetMultipleDates(hdChangeFromdate.Value, hdChangeTodate.Value, Txt_ToursCode.Text)
                dsDates = objBLLTourSearch.GetMultipleDates_WithRateBasis(txtTourFromDate.Text, txtTourToDate.Text, Txt_ToursCode.Text)
                If dsDates.Tables(0).Rows.Count > 0 Then
                    dlMultipleDate.DataSource = dsDates.Tables(0)
                    dlMultipleDate.DataBind()
                    'lblSelectMultipleDates.Text = "Excursion: " & lblExcName.Text
                    hdMealPlanExcCode.Value = Txt_ToursCode.Text
                    'hdMealPlanVehicleCode.Value = Txt_ToursCode.Text

                    If dlMultipleDate.Items.Count > 0 Then
                        Dim dtMultiDates As New DataTable
                        dtMultiDates = Session("selectedCombotourdatatable")
                        For Each dlItem1 As DataListItem In dlMultipleDate.Items
                            Dim lblMeanPlanDates As Label = CType(dlItem1.FindControl("lblMeanPlanDates"), Label)
                            Dim chkMealPlanDates As CheckBox = CType(dlItem1.FindControl("chkMealPlanDates"), CheckBox)
                            Dim foundRow As DataRow
                            foundRow = dtMultiDates.Select("exctypcode='" & Txt_ToursCode.Text.Trim & "' AND  vehiclecode='" & Txt_ToursCode.Text.Trim & "' AND exctypcombocode='" & Txt_ToursCode.Text.Trim & "' AND excdate='" & lblMeanPlanDates.Text.Trim & "' AND type='MULTI_DATE' ").FirstOrDefault
                            If Not foundRow Is Nothing Then
                                chkMealPlanDates.Checked = True
                            End If
                        Next
                        If dsDates.Tables(1).Rows.Count > 0 Then
                            hdTourRateBasis.Value = dsDates.Tables(1).Rows(0)("ratebasis").ToString

                            If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                                divUnits.Style.Item("display") = "none"
                                divAdult.Style.Item("display") = "block"

                                divCostUnits.Style.Item("display") = "none"
                                divCostAdult.Style.Item("display") = "block"

                                'divClearAdult.Style.Item("display") = "block"
                                'divChild.Style.Item("display") = "block"
                                'divSenior.Style.Item("display") = "block"
                                'divChildAsAdult.Style.Item("display") = "block"



                            Else
                                divCostUnits.Style.Item("display") = "block"
                                'Txt_NoOfUnits.Text = ddlTourAdult.SelectedValue.ToString()
                                Txt_NoOfUnits.Text = 1
                                Txt_CostNoOfUnits.Text = 1
                            End If
                            Dim fromAge As Decimal = dsDates.Tables(1).Rows(0)("chidlagefrom").ToString()
                            Dim toAge As Decimal = dsDates.Tables(1).Rows(0)("childageto").ToString()

                            If strChild1 <> "" Then
                                If CType(strChild1, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            If strChild2 <> "" Then
                                If CType(strChild2, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            If strChild3 <> "" Then
                                If CType(strChild3, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            If strChild4 <> "" Then
                                If CType(strChild4, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            If strChild5 <> "" Then
                                If CType(strChild5, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            If strChild6 <> "" Then
                                If CType(strChild6, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            If strChild7 <> "" Then
                                If CType(strChild7, Integer) > toAge Then
                                    intChAsAdult = intChAsAdult + 1
                                End If
                            End If
                            Txt_NoOfAdults.Text = ddlTourAdult.SelectedValue.ToString()
                            Txt_NoOfSeniors.Text = ddlSeniorCitizen.SelectedValue.ToString()

                            Txt_NoOfChild.Text = CType(CType(ddlTourChildren.SelectedValue.ToString(), Integer) - intChAsAdult, String)
                            Txt_NoOfChildAsAdult.Text = intChAsAdult.ToString()



                            Lbl_CurCodeAdult.Text = hdCurrCode.Value
                            Lbl_CurCodeChAdult.Text = hdCurrCode.Value
                            Lbl_CurCodeChild.Text = hdCurrCode.Value
                            Lbl_CurCodeUnit.Text = hdCurrCode.Value
                            Lbl_CurCodeSenior.Text = hdCurrCode.Value
                        End If
                    End If

                    divSelectMultipleDates.Style.Item("display") = "block"
                    'btnTourFill.Visible = False


                    btnSaveComboExcursion.Visible = True
                    divComplimentary.Style.Item("display") = "block"
                    divTotalSale.Style.Item("display") = "block"
                    divpsenior.Style.Item("display") = "block"
                    divpTotal.Style.Item("display") = "block"

                    divTotalCost.Style.Item("display") = "block"
                    divCostArea.Style.Item("display") = "block"
                    divCostpTotal.Style.Item("display") = "block"

                    If dsDates.Tables(1).Rows.Count > 0 Then
                        If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                            If Val(Txt_NoOfChild.Text.Trim()) > 0 Then
                                divChild.Style.Item("display") = "block"
                                divCostChild.Style.Item("display") = "block"
                            End If
                            If Val(Txt_NoOfSeniors.Text.Trim()) > 0 Then
                                divSenior.Style.Item("display") = "block"
                                divCostSenior.Style.Item("display") = "block"
                            End If
                            If Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0 Then
                                divChildAsAdult.Style.Item("display") = "block"
                                divCostChildAsAdult.Style.Item("display") = "block"
                            End If
                        End If
                    End If
                Else
                    'MessageBox.ShowMessage(Page, MessageType.Warning, lblSelectMultipleDates.Text & " is not operational on these dates.")

                End If


            End If
            If Txt_TourType.Text = "NORMAL" Then

                Dim dsDates As DataSet
                If txtTourFromDate.Text.Trim().Length = 0 Or txtTourToDate.Text.Trim.Length = 0 And txtTourToDate.Text.Trim.Length > 0 Then
                    txtTourFromDate.Text = Txt_TourDate.Text
                    txtTourToDate.Text = Txt_TourDate.Text
                    'Else
                    '    Exit Sub
                End If

                dsDates = objBLLTourSearch.GetMultipleDates_WithRateBasis(txtTourFromDate.Text, txtTourToDate.Text, Txt_ToursCode.Text)
                If dsDates.Tables(1).Rows.Count > 0 Then
                    hdTourRateBasis.Value = dsDates.Tables(1).Rows(0)("ratebasis").ToString
                    If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                        divUnits.Style.Item("display") = "none"
                        divAdult.Style.Item("display") = "block"

                        divCostUnits.Style.Item("display") = "none"
                        divCostAdult.Style.Item("display") = "block"

                        'divClearAdult.Style.Item("display") = "block"
                        'divChild.Style.Item("display") = "block"
                        'divSenior.Style.Item("display") = "block"
                        'divChildAsAdult.Style.Item("display") = "block"





                    Else
                        divUnits.Style.Item("display") = "block"
                        divCostUnits.Style.Item("display") = "block"
                        'Txt_NoOfUnits.Text = ddlTourAdult.SelectedValue.ToString()
                        Txt_NoOfUnits.Text = 1
                        Txt_CostNoOfUnits.Text = 1
                    End If


  


                    Dim fromAge As Decimal = dsDates.Tables(1).Rows(0)("chidlagefrom").ToString()
                    Dim toAge As Decimal = dsDates.Tables(1).Rows(0)("childageto").ToString()

                    If strChild1 <> "" Then
                        If CType(strChild1, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild2 <> "" Then
                        If CType(strChild2, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild3 <> "" Then
                        If CType(strChild3, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild4 <> "" Then
                        If CType(strChild4, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild5 <> "" Then
                        If CType(strChild5, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild6 <> "" Then
                        If CType(strChild6, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If
                    If strChild7 <> "" Then
                        If CType(strChild7, Integer) > toAge Then
                            intChAsAdult = intChAsAdult + 1
                        End If
                    End If

                    Txt_NoOfSeniors.Text = ddlSeniorCitizen.SelectedValue.ToString()
                    Txt_NoOfAdults.Text = ddlTourAdult.SelectedValue.ToString()

                    Txt_NoOfChild.Text = CType(CType(ddlTourChildren.SelectedValue.ToString(), Integer) - intChAsAdult, String)
                    Txt_NoOfChildAsAdult.Text = intChAsAdult.ToString()


                    Txt_CostNoOfSeniors.Text = ddlSeniorCitizen.SelectedValue.ToString()
                    Txt_CostNoOfAdults.Text = ddlTourAdult.SelectedValue.ToString()

                    Txt_CostNoOfChild.Text = CType(CType(ddlTourChildren.SelectedValue.ToString(), Integer) - intChAsAdult, String)
                    Txt_CostNoOfChildAsAdult.Text = intChAsAdult.ToString()



                    Lbl_CurCodeAdult.Text = hdCurrCode.Value
                    Lbl_CurCodeChAdult.Text = hdCurrCode.Value
                    Lbl_CurCodeChild.Text = hdCurrCode.Value
                    Lbl_CurCodeUnit.Text = hdCurrCode.Value
                    Lbl_CurCodeSenior.Text = hdCurrCode.Value
                    divComplimentary.Style.Item("display") = "block"
                    divSelectNormal.Style.Item("display") = "block"
                    divTotalSale.Style.Item("display") = "block"
                    divTotalCost.Style.Item("display") = "block"

                    btnSaveComboExcursion.Visible = True

                    divpsenior.Style.Item("display") = "block"
                    divpTotal.Style.Item("display") = "block"



                    divCostArea.Style.Item("display") = "block"
                    divCostpTotal.Style.Item("display") = "block"

                    If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                        If Val(Txt_NoOfChild.Text.Trim()) > 0 Then
                            divChild.Style.Item("display") = "block"
                            divCostChild.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfSeniors.Text.Trim()) > 0 Then
                            divSenior.Style.Item("display") = "block"
                            divCostSenior.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0 Then
                            divChildAsAdult.Style.Item("display") = "block"
                            divCostChildAsAdult.Style.Item("display") = "block"
                        End If

                    Else

                    End If
                End If
            End If

            'select * from excursiontypes where multipledatesyesno='YES' and exctypcode='ET/000346'


            Dim strMultiCostCount As String = objclsUtilities.ExecuteQueryReturnStringValue("select count(*)cnt from excursiontypes where multicost='YES' and exctypcode='" + Txt_ToursCode.Text + "'")
            If strMultiCostCount > 0 Then
                'sp_booking_muticost_price '000112','2019/10/12','000006','2','0','','0','IN','ET/000295' 
              
                btnMulticost.Visible = True
            Else
                btnMulticost.Visible = False
            End If


            ''changed by mohamed on 12/02/2018
            'txtSearchTour.Text = ""
            'txtSearchTour.Enabled = True
            'btnTourTextSearch.Enabled = True
            'ddlSorting.Enabled = True

            'CreateSelectedTourDataTable()
            'CreateComboTourDataTable()
            'If Not Session("sRequestId") Is Nothing Then
            '    If (objBLLTourSearch.ValidateTourSearchDateGaps(Session("sRequestId"), txtTourFromDate.Text, txtTourToDate.Text)) = False Then
            '        MessageBox.ShowMessage(Page, MessageType.Warning, "The search date should be in continuity with previous booking date range.")
            '        Exit Sub
            '    End If
            'End If

            'Toursearch()
            'txtSearchFocus.Focus()

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnTourFill_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Function ValidateTourFreeForm() As Boolean
        If (txtTourStartingFrom.Text.Trim().Length = 0) Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter PICK UP LOCATION")
            ValidateTourFreeForm = False
            Exit Function
        End If

        If (txtTourSourceCountry.Text.Trim().Length = 0) Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter SOURCE COUNTRY")
            ValidateTourFreeForm = False
            Exit Function
        End If

        If (Txt_ToursCode.Text.Trim().Length = 0) Then
            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select TOUR")
            ValidateTourFreeForm = False
            Exit Function
        End If
        Txt_NoOfAdults.BackColor = Drawing.Color.White
        Txt_NoOfSeniors.BackColor = Drawing.Color.White
        Txt_NoOfChild.BackColor = Drawing.Color.White
        Txt_NoOfChildAsAdult.BackColor = Drawing.Color.White
        Txt_NoOfUnits.BackColor = Drawing.Color.White
        Txt_PriceAdult.BackColor = Drawing.Color.White
        Txt_PriceChild.BackColor = Drawing.Color.White
        Txt_PriceChildAsAdult.BackColor = Drawing.Color.White
        Txt_PriceSenior.BackColor = Drawing.Color.White
        Txt_PriceUnits.BackColor = Drawing.Color.White


        If hdTourRateBasis.Value <> "UNIT" Then
            If (Val(Txt_NoOfSeniors.Text.Trim()) > 0) And (Val(Txt_PriceSenior.Text.Trim()) = 0) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter correct Seniors Price")
                Txt_PriceSenior.BackColor = Drawing.Color.Beige
                ValidateTourFreeForm = False
                Exit Function
            End If

            If (Val(Txt_NoOfAdults.Text.Trim()) = 0) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Adult No(s)")
                ValidateTourFreeForm = False
                Txt_NoOfAdults.BackColor = Drawing.Color.Beige
                Exit Function
            ElseIf (Val(Txt_NoOfAdults.Text.Trim()) > 16) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Too many Adult!")
                ValidateTourFreeForm = False
                Txt_NoOfAdults.BackColor = Drawing.Color.Beige
                Exit Function
            End If

            If (Val(Txt_PriceAdult.Text.Trim()) = 0) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter Adult Price")
                Txt_PriceAdult.BackColor = Drawing.Color.Beige
                ValidateTourFreeForm = False
                Exit Function
            End If

            If (Val(Txt_NoOfChild.Text.Trim()) > 0) And (Val(Txt_PriceChild.Text.Trim()) = 0) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter correct Child Price")
                Txt_PriceChild.BackColor = Drawing.Color.Beige
                ValidateTourFreeForm = False
                Exit Function
            End If

            If (Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0) And (Val(Txt_PriceChildAsAdult.Text.Trim()) = 0) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter correct Child As Adult Price")
                Txt_PriceChildAsAdult.BackColor = Drawing.Color.Beige
                ValidateTourFreeForm = False
                Exit Function
            End If
        Else
            If hdTourRateBasis.Value = "UNIT" And Val(Txt_NoOfUnits.Text.Trim()) = 0 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter correct Units")
                Txt_NoOfUnits.BackColor = Drawing.Color.Beige
                ValidateTourFreeForm = False
                Exit Function
            End If
            If hdTourRateBasis.Value = "UNIT" And Val(Txt_NoOfUnits.Text.Trim()) > 0 And Val(Txt_PriceUnits.Text.Trim()) = 0 Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Please Enter correct Units Price")
                Txt_PriceUnits.BackColor = Drawing.Color.Beige
                ValidateTourFreeForm = False
                Exit Function
            End If
        End If

        '===================================================================================








        ValidateTourFreeForm = True
    End Function

    Protected Sub btnSaveComboExcursion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveComboExcursion.Click
        Try
            'Dim ErrResult As String = ""
            If (ValidateTourFreeForm() = False) Then
                Exit Sub
            End If


            '** Validate Combo Date===============================
            Dim dtCombotourdatatable As New DataTable
            dtCombotourdatatable = Session("selectedCombotourdatatable")

            'Dim strExcCode As String = hdExcCodeComboPopup.Value
            'Dim strExcVehCode As String = hdVehicleCodeComboPopup.Value
            ' Dim dvCombo As DataView = New DataView(dt)
            Dim strType As String = ""
            If Txt_TourType.Text.ToString = "COMBO" Then
                strType = "COMBO"
                For Each dlItem As DataListItem In dlSelectComboDates.Items
                    Dim txtExcComboDate As TextBox = dlItem.FindControl("txtExcComboDate")
                    If txtExcComboDate.Text = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select excursion dates")
                        Exit Sub
                    End If
                    Dim lblExcComboCode As Label = dlItem.FindControl("lblExcComboCode")
                    Dim foundRow As DataRow

                    foundRow = dtCombotourdatatable.Select("exctypcode='" & Txt_ToursCode.Text.Trim & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='" + strType + "' ").FirstOrDefault
                    If foundRow Is Nothing Then
                        Dim drNew As DataRow = dtCombotourdatatable.NewRow()
                        drNew("exctypcode") = Txt_ToursCode.Text.Trim()
                        drNew("vehiclecode") = ""
                        drNew("excdate") = Format(CType(txtExcComboDate.Text.ToString, Date), "yyyy/MM/dd")
                        drNew("exctypcombocode") = lblExcComboCode.Text.Trim
                        drNew("type") = "COMBO"
                        dtCombotourdatatable.Rows.Add(drNew)

                    Else

                        'foundRow("exctypcode") = strExcCode.Trim
                        'foundRow("vehiclecode") = strExcVehCode.Trim
                        foundRow("excdate") = txtExcComboDate.Text.Trim
                        foundRow("exctypcombocode") = lblExcComboCode.Text.Trim
                    End If
                Next
            End If
            If Txt_TourType.Text.ToString = "MULTIPLE DATE" Then
                strType = "MULTI_DATE"

                For Each dlItem As DataListItem In dlMultipleDate.Items
                    Dim chkMealPlanDates As CheckBox = dlItem.FindControl("chkMealPlanDates")
                    Dim foundRow As DataRow
                    If chkMealPlanDates.Checked = True Then
                        Dim lblMeanPlanDates As Label = dlItem.FindControl("lblMeanPlanDates")
                        'dt.Rows.Add(strExcCode.Trim, "", lblMeanPlanDates.Text.Trim, strExcCode.Trim, "MULTI_DATE")


                        foundRow = dtCombotourdatatable.Select("exctypcode='" & Txt_ToursCode.Text.Trim & "' AND excdate='" & lblMeanPlanDates.Text.Trim & "' AND type='" + strType + "' ").FirstOrDefault
                        If foundRow Is Nothing Then
                            Dim drNew As DataRow = dtCombotourdatatable.NewRow()
                            drNew("exctypcode") = Txt_ToursCode.Text.Trim()
                            drNew("vehiclecode") = ""
                            drNew("excdate") = Format(CType(lblMeanPlanDates.Text.ToString, Date), "yyyy/MM/dd")
                            drNew("exctypcombocode") = Txt_ToursCode.Text.Trim
                            drNew("type") = "MULTI_DATE"
                            dtCombotourdatatable.Rows.Add(drNew)

                        Else

                            'foundRow("exctypcode") = strExcCode.Trim
                            'foundRow("vehiclecode") = strExcVehCode.Trim
                            foundRow("excdate") = lblMeanPlanDates.Text.Trim
                            foundRow("exctypcombocode") = Txt_ToursCode.Text.Trim
                        End If

                    End If

                Next

            End If

            Session("selectedCombotourdatatable") = dtCombotourdatatable
            Dim dataView As New DataView(dtCombotourdatatable)
            dataView.Sort = "excdate ASC"
            Dim dataTable As DataTable = dataView.ToTable()
            If dataTable.Rows.Count > 0 Then
                If (txtTourFromDate.Text.Trim().Length = 0) Then
                    txtTourFromDate.Text = Format(CType(dataTable.Rows(0)("excdate").ToString, Date), "yyyy/MM/dd")
                End If
                If (txtTourToDate.Text.Trim().Length = 0) Then
                    txtTourToDate.Text = Format(CType(dataTable.Rows(dataTable.Rows.Count - 1)("excdate").ToString, Date), "yyyy/MM/dd")
                End If
            Else
                If Txt_TourType.Text.ToString <> "NORMAL" Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select excursion dates")
                    Exit Sub
                End If
            End If

            Dim FrmDate As DateTime
            Dim ToDate As DateTime

            If (txtTourFromDate.Text.Trim.Length > 0) Then
                FrmDate = Format(CType(txtTourFromDate.Text, Date), "yyyy/MM/dd")

            End If
            If (txtTourToDate.Text.Trim.Length > 0) Then

                ToDate = Format(CType(txtTourToDate.Text, Date), "yyyy/MM/dd")
            End If

            If (Txt_TourType.Text = "NORMAL") Then
                FrmDate = Format(CType(Txt_TourDate.Text, Date), "yyyy/MM/dd")
                ToDate = FrmDate
            End If





            If (FrmDate < DateTime.Now) And (ToDate < DateTime.Now) And (Txt_TourDate.Text.Trim.Length > 0) Then
                FrmDate = Format(CType(Txt_TourDate.Text, Date), "yyyy/MM/dd")
                ToDate = Format(CType(Txt_TourDate.Text, Date), "yyyy/MM/dd")
            End If

            objBLLTourFreeFormBooking.FromDate = Format(CType(FrmDate, Date), "yyyy/MM/dd")
            objBLLTourFreeFormBooking.ToDate = Format(CType(ToDate, Date), "yyyy/MM/dd")
            ''*** Validate Save Value
            Dim iNoOfDays As Integer = 1

            Dim dblSeniorSaleValue As Double = 0
            Dim dblAdultSaleValue As Double = 0
            Dim dblChildSaleValue As Double = 0
            Dim dblChildAsAdultSaleValue As Double = 0
            Dim dblUnitSaleValue As Double = 0
            Dim dblTotalSaleValue As Double = 0


            Dim dblSeniorCostValue As Double = 0
            Dim dblAdultCostValue As Double = 0
            Dim dblChildCostValue As Double = 0
            Dim dblChildAsAdultCostValue As Double = 0
            Dim dblUnitCostValue As Double = 0
            Dim dblTotalCostValue As Double = 0


            If hdTourRateBasis.Value = "UNIT" Then
                If Txt_NoOfUnits.Text.Trim().Length > 0 And Txt_PriceUnits.Text.Trim().Length > 0 Then
                    'If (Txt_TourType.Text = "MULTIPLE DATE") Then
                    '    Txt_NoOfUnits.Text = CType(Txt_NoOfUnits.Text, Integer) * dtCombotourdatatable.Rows.Count
                    'End If

                    'Dim doNoOfUnits As Double = CType(Txt_NoOfUnits.Text.Trim, Double)
                    'Dim doPriceUnits As Double = CType(Txt_PriceUnits.Text.Trim, Double)
                    'Dim doSaleValueUnits As Double = Val(doNoOfUnits * doPriceUnits)

                    'Txt_SaleValueUnits.Text = doSaleValueUnits.ToString("0.000")

                Else
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select No Of Units / Price")

                    Exit Sub
                End If
            End If

            If (Txt_SaleValueSenior.Text.Trim().Length > 0) Then
                dblSeniorSaleValue = CType(Txt_SaleValueSenior.Text.Trim(), Double)
            End If
            If (Txt_SaleValueAdult.Text.Trim().Length > 0) Then
                dblAdultSaleValue = CType(Txt_SaleValueAdult.Text.Trim(), Double)
            End If
            If (Txt_SaleValueChild.Text.Trim().Length > 0) Then
                dblChildSaleValue = CType(Txt_SaleValueChild.Text.Trim(), Double)
            End If
            If (Txt_SaleValueChildAsAdult.Text.Trim().Length > 0) Then
                dblChildAsAdultSaleValue = CType(Txt_SaleValueChildAsAdult.Text.Trim(), Double)
            End If
            If (Txt_SaleValueUnits.Text.Trim().Length > 0) Then
                dblUnitSaleValue = CType(Txt_SaleValueUnits.Text.Trim(), Double)
            End If

            If hdTourRateBasis.Value = "ACS" Then
                dblTotalSaleValue = dblSeniorSaleValue + dblAdultSaleValue + dblChildSaleValue + dblChildAsAdultSaleValue
            Else
                dblTotalSaleValue = dblUnitSaleValue
            End If



            If (Txt_CostSaleValueSenior.Text.Trim().Length > 0) Then
                dblSeniorCostValue = CType(Txt_CostSaleValueSenior.Text.Trim(), Double)
            End If
            If (Txt_CostSaleValueAdult.Text.Trim().Length > 0) Then
                dblAdultCostValue = CType(Txt_CostSaleValueAdult.Text.Trim(), Double)
            End If
            If (Txt_CostSaleValueChild.Text.Trim().Length > 0) Then
                dblChildCostValue = CType(Txt_CostSaleValueChild.Text.Trim(), Double)
            End If
            If (Txt_CostSaleValueChildAsAdult.Text.Trim().Length > 0) Then
                dblChildAsAdultCostValue = CType(Txt_CostSaleValueChildAsAdult.Text.Trim(), Double)
            End If
            If (Txt_CostSaleValueUnits.Text.Trim().Length > 0) Then
                dblUnitCostValue = CType(Txt_CostSaleValueUnits.Text.Trim(), Double)
            End If

            If hdTourRateBasis.Value = "ACS" Then
                dblTotalCostValue = dblSeniorCostValue + dblAdultCostValue + dblChildCostValue + dblChildAsAdultCostValue
            Else
                dblTotalCostValue = dblUnitCostValue
            End If

            If (dtCombotourdatatable.Rows.Count > 0) And Txt_TourType.Text = "MULTIPLE DATE" Then
                dblTotalSaleValue = dblTotalSaleValue * dtCombotourdatatable.Rows.Count

            End If


            If Val(dblTotalSaleValue.ToString.Trim() <= 0) Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Enter Correct Units / Price")
                Exit Sub
            End If









            Dim requestid As String = ""
            Dim agentref As String = ""
            Dim columbusref As String = ""
            Dim remarks As String = ""
            Dim strSectorgroupCode As String = ""
            Dim strBuffer As New Text.StringBuilder
            Dim strBufferMultiCostBreakup As New Text.StringBuilder
            Dim strBufferCombo As New StringBuilder
            Dim strselectedrow As String = ""
            Dim strRowLineNo As String = ""
            Dim strElineno As String = ""
            requestid = GetNewOrExistingRequestId()

            strBuffer.Append("<DocumentElement>")
            strBufferMultiCostBreakup.Append("<DocumentElement>")
            strBufferCombo.Append("<DocumentElement>")

            Dim objBLLCommonFuntions = New BLLCommonFuntions
            Dim dtBookHeader = New DataTable
            dtBookHeader = objBLLCommonFuntions.GetBookingTempHeaderDetails(requestid)

            Dim lOlinene As Integer = 0
            If ViewState("OLineNo") IsNot Nothing Then
                lOlinene = Val(ViewState("OLineNo"))
            End If
            If lOlinene = 0 Then
                objBLLTourFreeFormBooking.OpMode = "N"
                strselectedrow = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "TOUR") '*** Line number for Selected tour dates
                If strselectedrow Is Nothing Then
                    strselectedrow = 1
                End If
                strselectedrow = IIf(Val(strselectedrow) <= 0, 1, Val(strselectedrow))

            Else
                strselectedrow = lOlinene
                objBLLTourFreeFormBooking.OpMode = "E"
            End If




            'strselectedrow = 1

            If dtBookHeader.Rows.Count > 0 Then

                objBLLTourFreeFormBooking.EBdiv_code = dtBookHeader.Rows(0)("div_code").ToString
                objBLLTourFreeFormBooking.EbRequestID = requestid
                objBLLTourFreeFormBooking.EBsourcectrycode = txtTourSourceCountryCode.Text
                objBLLTourFreeFormBooking.AgentCode = txtTourCustomerCode.Text

                objBLLTourFreeFormBooking.EBreqoverride = 0
                objBLLTourFreeFormBooking.EBagentref = dtBookHeader.Rows(0)("agentref").ToString
                objBLLTourFreeFormBooking.EBcolumbusref = dtBookHeader.Rows(0)("ColumbusRef").ToString
                objBLLTourFreeFormBooking.EBremarks = dtBookHeader.Rows(0)("remarks").ToString
                objBLLTourFreeFormBooking.EBuserlogged = Session("GlobalUserName")
            Else
                'objBLLTourFreeFormBooking.AgentCode = Session("sAgentCode")
                objBLLTourFreeFormBooking.EBdiv_code = objclsUtilities.ExecuteQueryReturnSingleValue("select divcode from agentmast where agentcode='" & Session("sAgentCode") & "'")
                objBLLTourFreeFormBooking.EbRequestID = requestid
                '' ''objBLLTourFreeFormBooking.EBsourcectrycode = IIf(sourcectrycode = "", objBLLTourSearch.SourceCountryCode, sourcectrycode)
                objBLLTourFreeFormBooking.EBsourcectrycode = txtTourSourceCountryCode.Text


                objBLLTourFreeFormBooking.AgentCode = txtTourCustomerCode.Text

                ' ''objBLLTourFreeFormBooking.EBreqoverride = IIf(chkTourOveridePrice.Checked = True, "1", "0")
                objBLLTourFreeFormBooking.EBreqoverride = 0
                objBLLTourFreeFormBooking.EBagentref = agentref
                objBLLTourFreeFormBooking.EBcolumbusref = columbusref
                objBLLTourFreeFormBooking.EBremarks = remarks

            End If
            objBLLTourFreeFormBooking.EBuserlogged = Session("GlobalUserName")
            objBLLTourFreeFormBooking.TourStartingFromCode = txtTourStartingFromCode.Text
            strSectorgroupCode = objBLLTourFreeFormBooking.TourStartingFromCode
            objBLLTourFreeFormBooking.SectorgroupCode = objBLLTourFreeFormBooking.TourStartingFromCode

            If strElineno = "" Then
                '' ''strElineno = objBLLCommonFuntions.GetBookingRowLineNo(requestid, "TOUR")
                strElineno = strselectedrow
            Else
                'strElineno = CType(strElineno, Integer) + 1
            End If


            If strRowLineNo <> "" Then
                strRowLineNo = strRowLineNo + ";" + CType(strElineno, String)
            Else
                strRowLineNo = strElineno
            End If



            '------------------Multicost ---------- start


            Dim strMultiCostCount As String = objclsUtilities.ExecuteQueryReturnStringValue("select count(*)cnt from excursiontypes where multicost='YES' and exctypcode='" + Txt_ToursCode.Text + "'")

            Dim dMultiPerAdult As Decimal = 0
            Dim dMultiPerChild As Decimal = 0
            Dim dMultiPerSenior As Decimal = 0
            Dim dMultiPerUnit As Decimal = 0


            Dim dMultiAdultCost As Decimal = 0
            Dim dMultiChildCost As Decimal = 0
            Dim dMultiSeniorCost As Decimal = 0
            Dim dMultiUnitCost As Decimal = 0

            Dim dMultiTotalCost As Decimal = 0


            If strMultiCostCount > 0 And Not Session("sdtMultiCost") Is Nothing Then

                Dim dtMultiCostBreakup As DataTable = Session("sdtMultiCost")
                If dtMultiCostBreakup.Rows.Count > 0 Then
                    For i As Integer = 0 To dtMultiCostBreakup.Rows.Count - 1


                        Dim gvMultiCostNew As GridView = FindControl("gvMultiCost")

                        Dim gvRow As GridViewRow = gvMultiCostNew.Rows(i)

                        '  Dim txtPeradult As String = CType(gvMultiCost.Rows(i).FindControl("txtPeradult"), Label).Text

                        Dim txtPeradult As String = CType(gvMultiCost.Rows(i).FindControl("txtPeradult"), TextBox).Text
                        dMultiPerAdult = dMultiPerAdult + Convert.ToDecimal(txtPeradult)
                        ' Dim lblAdultCost As String = CType(gvMultiCost.Rows(i).FindControl("lblAdultCost"), TextBox).Text
                        Dim lblAdultCost As String = CType(gvMultiCost.Rows(i).FindControl("hdAdultCost"), HiddenField).Value
                        dMultiAdultCost = dMultiAdultCost + Convert.ToDecimal(lblAdultCost)

                        Dim txtPerchild As String = CType(gvMultiCost.Rows(i).FindControl("txtPerchild"), TextBox).Text
                        dMultiPerChild = dMultiPerChild + Convert.ToDecimal(txtPerchild)
                        ' Dim lblchildcost As String = CType(gvMultiCost.Rows(i).FindControl("lblchildcost"), TextBox).Text
                        Dim lblchildcost As String = CType(gvMultiCost.Rows(i).FindControl("hdChildcost"), HiddenField).Value
                        dMultiChildCost = dMultiChildCost + Convert.ToDecimal(lblchildcost)

                        Dim txtPersenior As String = CType(gvMultiCost.Rows(i).FindControl("txtPersenior"), TextBox).Text
                        dMultiPerSenior = dMultiPerSenior + Convert.ToDecimal(txtPersenior)
                        ' Dim lblSeniorCost As String = CType(gvMultiCost.Rows(i).FindControl("lblSeniorCost"), TextBox).Text
                        Dim lblSeniorCost As String = CType(gvMultiCost.Rows(i).FindControl("hdSeniorCost"), HiddenField).Value
                        dMultiSeniorCost = dMultiSeniorCost + Convert.ToDecimal(lblSeniorCost)

                        Dim txtUnitcost As String = CType(gvMultiCost.Rows(i).FindControl("txtUnitcost"), TextBox).Text
                        dMultiUnitCost = dMultiUnitCost + Convert.ToDecimal(txtUnitcost)

                        '   Dim lbltotalcost As String = CType(gvRow.FindControl("lbltotalcost"), TextBox).Text

                        ' Dim lbltotalcost As String = CType(gvRow.FindControl("lbltotalcost"), TextBox).Text
                        Dim lbltotalcost As String = CType(gvRow.FindControl("hdTotalcost"), HiddenField).Value

                        dMultiTotalCost = dMultiTotalCost + Convert.ToDecimal(lbltotalcost)

                        strBufferMultiCostBreakup.Append("<Table>")
                        strBufferMultiCostBreakup.Append("<elineno>" & CType(strElineno, String) & "</elineno>")
                        strBufferMultiCostBreakup.Append("<mlineno>" & CType(i + 1, String) & "</mlineno>")
                        strBufferMultiCostBreakup.Append("<eplistcode>" & dtMultiCostBreakup.Rows(i)("eplistcode").ToString & "</eplistcode>")
                        strBufferMultiCostBreakup.Append("<partycode>" & dtMultiCostBreakup.Rows(i)("partycode").ToString & "</partycode>")
                        strBufferMultiCostBreakup.Append("<servicedescription>" & dtMultiCostBreakup.Rows(i)("servicedescription").ToString.Replace("&", "and") & "</servicedescription>")
                        strBufferMultiCostBreakup.Append("<noofadults>" & dtMultiCostBreakup.Rows(i)("noofadults").ToString & "</noofadults>")

                        ' strBufferMultiCostBreakup.Append("<peradult>" & dtMultiCostBreakup.Rows(i)("peradult").ToString & "</peradult>")
                        'strBufferMultiCostBreakup.Append("<adultcost>" & dtMultiCostBreakup.Rows(i)("adultcost").ToString & "</adultcost>")

                        strBufferMultiCostBreakup.Append("<peradult>" & txtPeradult.Trim & "</peradult>")
                        strBufferMultiCostBreakup.Append("<adultcost>" & lblAdultCost.Trim & "</adultcost>")


                        strBufferMultiCostBreakup.Append("<noofchildren>" & dtMultiCostBreakup.Rows(i)("noofchildren").ToString & "</noofchildren>")

                        'strBufferMultiCostBreakup.Append("<perchild>" & dtMultiCostBreakup.Rows(i)("perchild").ToString & "</perchild>")
                        'strBufferMultiCostBreakup.Append("<childcost>" & dtMultiCostBreakup.Rows(i)("childcost").ToString & "</childcost>")

                        strBufferMultiCostBreakup.Append("<perchild>" & txtPerchild.Trim & "</perchild>")
                        strBufferMultiCostBreakup.Append("<childcost>" & lblchildcost.Trim & "</childcost>")


                        strBufferMultiCostBreakup.Append("<noofseniors>" & dtMultiCostBreakup.Rows(i)("noofseniors").ToString & "</noofseniors>")

                        'strBufferMultiCostBreakup.Append("<persenior>" & dtMultiCostBreakup.Rows(i)("persenior").ToString & "</persenior>")
                        'strBufferMultiCostBreakup.Append("<seniorcost>" & dtMultiCostBreakup.Rows(i)("seniorcost").ToString & "</seniorcost>")
                        'strBufferMultiCostBreakup.Append("<unitcost>" & dtMultiCostBreakup.Rows(i)("unitcost").ToString & "</unitcost>")

                        strBufferMultiCostBreakup.Append("<persenior>" & txtPersenior.Trim & "</persenior>")
                        strBufferMultiCostBreakup.Append("<seniorcost>" & lblSeniorCost.Trim & "</seniorcost>")
                        strBufferMultiCostBreakup.Append("<unitcost>" & txtUnitcost.Trim & "</unitcost>")

                        strBufferMultiCostBreakup.Append("<totalcost>" & lbltotalcost.Trim & "</totalcost>")

                        ' strBufferMultiCostBreakup.Append("<totalcost>" & dtMultiCostBreakup.Rows(i)("totalcost").ToString & "</totalcost>")

                        strBufferMultiCostBreakup.Append("<markuptype>" & dtMultiCostBreakup.Rows(i)("markuptype").ToString & "</markuptype>")
                        strBufferMultiCostBreakup.Append("<Markupoperator>" & dtMultiCostBreakup.Rows(i)("Markupoperator").ToString & "</Markupoperator>")
                        strBufferMultiCostBreakup.Append("<markupvalue_adult>" & dtMultiCostBreakup.Rows(i)("markupvalue_adult").ToString & "</markupvalue_adult>")
                        strBufferMultiCostBreakup.Append("<markupvalue_child>" & dtMultiCostBreakup.Rows(i)("markupvalue_child").ToString & "</markupvalue_child>")
                        strBufferMultiCostBreakup.Append("<markupvalue_senior>" & dtMultiCostBreakup.Rows(i)("markupvalue_senior").ToString & "</markupvalue_senior>")

                        strBufferMultiCostBreakup.Append("<noofchildasadult>" & dtMultiCostBreakup.Rows(i)("noofchildasadult").ToString & "</noofchildasadult>")
                        strBufferMultiCostBreakup.Append("<perchildasadult>" & dtMultiCostBreakup.Rows(i)("perchildasadult").ToString & "</perchildasadult>")
                        strBufferMultiCostBreakup.Append("<childasadult>" & dtMultiCostBreakup.Rows(i)("childasadult").ToString & "</childasadult>")


                        strBufferMultiCostBreakup.Append("</Table>")
                    Next
                Else
                    dMultiAdultCost = 0
                    dMultiChildCost = 0
                    dMultiSeniorCost = 0
                    dMultiPerUnit = IIf(Txt_CostPriceUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceUnits.Text.ToString.ToString)
                    dMultiUnitCost = IIf(Txt_CostPriceUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceUnits.Text.ToString.ToString)
                    dMultiTotalCost = IIf(Txt_CostPriceUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceUnits.Text.ToString.ToString)
                End If
            End If

            '------------------Multicost





            strBuffer.Append("<Table>")
            strBuffer.Append(" <elineno>" & CType(strElineno, String) & "</elineno>")
            strBuffer.Append("<exctypcode>" & Txt_ToursCode.Text & "</exctypcode>")

            strBuffer.Append("<sectorgroupcode>" & strSectorgroupCode.ToString & "</sectorgroupcode>")
            strBuffer.Append("<vehiclecode>" & "" & "</vehiclecode>") '???


            strBuffer.Append("<excdate>" & Format(CType(objBLLTourFreeFormBooking.FromDate, Date), "yyyy/MM/dd") & "</excdate>") '???



            strBuffer.Append("<ratebasis>" & hdTourRateBasis.Value.ToString & "</ratebasis>")

            'strBuffer.Append("<childages>" & objBLLTourFreeFormBooking.ChildAgeString & "</childages>")
            strBuffer.Append("<childages>" & hdChildAgeString.Value.ToString & "</childages>")
            'If (hdTourRateBasis.Value.ToString <> "UNIT") Then
            strBuffer.Append("<adults>" & IIf(Txt_NoOfAdults.Text.ToString.Trim.Length = 0, "0", Txt_NoOfAdults.Text.ToString.ToString) & "</adults>")
            'If (Val(Txt_NoOfChildAsAdult.Text.ToString.Trim) = 0) Then
            strBuffer.Append("<child>" & IIf(Txt_NoOfChild.Text.ToString.Trim.Length = 0, "0", Txt_NoOfChild.Text.ToString.ToString) & "</child>")
            'Else
            'Dim ChNo As Integer = 0
            'ChNo = ddlTourChildren.SelectedValue - Val(Txt_NoOfChildAsAdult.Text)
            'strBuffer.Append("<child>" & ChNo.ToString & "</child>")
            'End If

            strBuffer.Append("<senior>" & IIf(Txt_NoOfSeniors.Text.ToString.Trim.Length = 0, "0", Txt_NoOfSeniors.Text.ToString.ToString) & "</senior>")
            strBuffer.Append("<units>" & IIf(Txt_NoOfUnits.Text.ToString.Trim.Length = 0, "0", Txt_NoOfUnits.Text.ToString) & "</units>")
            strBuffer.Append("<childasadult>" & IIf(Txt_NoOfChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_NoOfChildAsAdult.Text.ToString) & "</childasadult>")

            strBuffer.Append("<adultprice>" & IIf(Txt_PriceAdult.Text.ToString.Trim.Length = 0, "0", Txt_PriceAdult.Text.ToString) & "</adultprice>")
            strBuffer.Append("<childprice>" & IIf(Txt_PriceChild.Text.ToString.Trim.Length = 0, "0", Txt_PriceChild.Text.ToString) & "</childprice>")
            strBuffer.Append("<seniorprice>" & IIf(Txt_PriceSenior.Text.ToString.Trim.Length = 0, "0", Txt_PriceSenior.Text.ToString) & "</seniorprice>")
            strBuffer.Append("<unitprice>" & IIf(Txt_PriceUnits.Text.ToString.Trim.Length = 0, "0", Txt_PriceUnits.Text.ToString) & "</unitprice>")
            strBuffer.Append("<childasadultprice>" & IIf(Txt_PriceChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_PriceChildAsAdult.Text.ToString) & "</childasadultprice>")


            Dim adultsalevalue As Double = IIf(Txt_SaleValueAdult.Text.ToString.Trim.Length = 0, "0", Txt_SaleValueAdult.Text.ToString)
            Dim childsalevalue As Double = IIf(Txt_SaleValueChild.Text.ToString.Trim.Length = 0, "0", Txt_SaleValueChild.Text.ToString)
            Dim seniorsalevalue As Double = IIf(Txt_SaleValueSenior.Text.ToString.Trim.Length = 0, "0", Txt_SaleValueSenior.Text.ToString)
            Dim unitsalevalue As Double = IIf(Txt_SaleValueUnits.Text.ToString.Trim.Length = 0, "0", Txt_SaleValueUnits.Text.ToString)
            Dim ChildAsAdultSaleValue As Double = IIf(Txt_SaleValueChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_SaleValueChildAsAdult.Text.ToString)


            Dim totalsalevalueall As Decimal = adultsalevalue + childsalevalue + seniorsalevalue + unitsalevalue + ChildAsAdultSaleValue

            Dim totalsalevalue As Double = totalsalevalueall * iNoOfDays



            strBuffer.Append("<adultsalevalue>" & adultsalevalue.ToString & "</adultsalevalue>")
            strBuffer.Append("<childsalevalue>" & childsalevalue.ToString & "</childsalevalue>")
            strBuffer.Append("<seniorsalevalue>" & seniorsalevalue.ToString & "</seniorsalevalue>")
            strBuffer.Append("<unitsalevalue>" & unitsalevalue.ToString & "</unitsalevalue>")
            strBuffer.Append("<childasadultvalue>" & ChildAsAdultSaleValue.ToString & "</childasadultvalue>")
            strBuffer.Append("<totalsalevalue>" & totalsalevalue.ToString & "</totalsalevalue>")


            strBuffer.Append("<wladultsalevalue>" & adultsalevalue.ToString & "</wladultsalevalue>")
            strBuffer.Append("<wlchildsalevalue>" & childsalevalue.ToString & "</wlchildsalevalue>")
            strBuffer.Append("<wlseniorsalevalue>" & seniorsalevalue.ToString & "</wlseniorsalevalue>")
            strBuffer.Append("<wlunitsalevalue>" & unitsalevalue.ToString & "</wlunitsalevalue>")
            strBuffer.Append("<wltotalsalevalue>" & totalsalevalue.ToString & "</wltotalsalevalue>")
            strBuffer.Append("<wlchildasadultvalue>" & ChildAsAdultSaleValue.ToString & "</wlchildasadultvalue>")
            If Chk_Complimentary.Checked = True Then
                strBuffer.Append("<comp_cust>" & "1" & "</comp_cust>")
            Else
                strBuffer.Append("<comp_cust>" & "0" & "</comp_cust>")
            End If

            strBuffer.Append("<tourstartingfrom>" & txtTourStartingFromCode.Text & "</tourstartingfrom>")
            strBuffer.Append("<overrideprice>" & "0" & "</overrideprice>")

            strBuffer.Append("<adulteplistcode>" & "" & "</adulteplistcode>")
            strBuffer.Append("<childeplistcode>" & "" & "</childeplistcode>")
            strBuffer.Append("<senioreplistcode>" & "" & "</senioreplistcode>")
            strBuffer.Append("<uniteplistcode>" & "" & "</uniteplistcode>")
            strBuffer.Append("<preferredsupplier>" & "" & "</preferredsupplier>")


            Dim adultCostvalue As Double = 0 'IIf(Txt_CostSaleValueAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueAdult.Text.ToString)
            Dim childCostvalue As Double = 0 'IIf(Txt_CostSaleValueChild.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChild.Text.ToString)
            Dim seniorCostvalue As Double = 0 'IIf(Txt_CostSaleValueSenior.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueSenior.Text.ToString)
            Dim unitCostvalue As Double = 0 'IIf(Txt_CostSaleValueUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueUnits.Text.ToString)
            Dim ChildAsAdultCostValue As Double = 0 'IIf(Txt_CostSaleValueChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChildAsAdult.Text.ToString)

            If strMultiCostCount > 0 Then
                adultCostvalue = dMultiAdultCost ' IIf(Txt_CostSaleValueAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueAdult.Text.ToString)
                childCostvalue = dMultiChildCost 'IIf(Txt_CostSaleValueChild.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChild.Text.ToString)
                seniorCostvalue = dMultiSeniorCost 'IIf(Txt_CostSaleValueSenior.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueSenior.Text.ToString)
                unitCostvalue = dMultiUnitCost 'IIf(Txt_CostSaleValueUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueUnits.Text.ToString)
                ChildAsAdultCostValue = 0 ' IIf(Txt_CostSaleValueChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChildAsAdult.Text.ToString)
                Txt_CostPriceUnits.Text = (adultCostvalue + childCostvalue + seniorCostvalue + unitCostvalue).ToString
                Txt_CostSaleValueUnits.Text = (adultCostvalue + childCostvalue + seniorCostvalue + unitCostvalue).ToString
                '  unitCostvalue = adultCostvalue + childCostvalue + seniorCostvalue + unitCostvalue + dMultiPerUnit
            Else
                adultCostvalue = IIf(Txt_CostSaleValueAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueAdult.Text.ToString)
                childCostvalue = IIf(Txt_CostSaleValueChild.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChild.Text.ToString)
                seniorCostvalue = IIf(Txt_CostSaleValueSenior.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueSenior.Text.ToString)
                unitCostvalue = IIf(Txt_CostSaleValueUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueUnits.Text.ToString)
                ChildAsAdultCostValue = IIf(Txt_CostSaleValueChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChildAsAdult.Text.ToString)
            End If

        



            strBuffer.Append("<adultcprice>" & IIf(Txt_CostPriceAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceAdult.Text.ToString) & "</adultcprice>")
            strBuffer.Append("<childcprice>" & IIf(Txt_CostPriceChild.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceChild.Text.ToString) & "</childcprice>")
            strBuffer.Append("<seniorcprice>" & IIf(Txt_CostPriceSenior.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceSenior.Text.ToString) & "</seniorcprice>")
            strBuffer.Append("<childasadultcprice>" & IIf(Txt_CostPriceChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceChildAsAdult.Text.ToString) & "</childasadultcprice>")
            strBuffer.Append("<childasadultcostvalue>" & IIf(Txt_CostSaleValueChildAsAdult.Text.ToString.Trim.Length = 0, "0", Txt_CostSaleValueChildAsAdult.Text.ToString) & "</childasadultcostvalue>")

            strBuffer.Append("<unitcprice>" & IIf(Txt_CostPriceUnits.Text.ToString.Trim.Length = 0, "0", Txt_CostPriceUnits.Text.ToString) & "</unitcprice>")

            strBuffer.Append("<adultcostvalue>" & adultCostvalue & "</adultcostvalue>")
            strBuffer.Append("<childcostvalue>" & childCostvalue & "</childcostvalue>")
            strBuffer.Append("<seniorcostvalue>" & seniorCostvalue & "</seniorcostvalue>")
          


            ' Dim totalCostvalue As Double = CType(IIf(Txt_TotalCostValue.Text.ToString.Trim.Length = 0, "0", dblTotalCostValue), Double) * iNoOfDays


            Dim totalCostvaluell As Decimal = adultCostvalue + childCostvalue + seniorCostvalue + unitCostvalue + ChildAsAdultCostValue

            Dim totalCostvalue As Double = totalCostvaluell * iNoOfDays

            If strMultiCostCount > 0 Then
                strBuffer.Append("<unitcostvalue>" & totalCostvalue & "</unitcostvalue>")
            Else
                strBuffer.Append("<unitcostvalue>" & unitCostvalue & "</unitcostvalue>")
            End If

            strBuffer.Append("<totalcostvalue>" & totalCostvalue & "</totalcostvalue>")


            strBuffer.Append("<adultcplistcode>" & "0" & "</adultcplistcode>")
            strBuffer.Append("<childcplistcode>" & "0" & "</childcplistcode>")
            strBuffer.Append("<seniorcplistcode>" & "0" & "</seniorcplistcode>")
            strBuffer.Append("<unitcplistcode>" & "0" & "</unitcplistcode>")

            strBuffer.Append("<wlcurrcode>" & "" & "</wlcurrcode>")
            strBuffer.Append("<wlconvrate>" & "" & "</wlconvrate>")
            strBuffer.Append("<wlmarkupperc>" & "" & "</wlmarkupperc>")


            strBuffer.Append("<searchfromdate>" & Format(CType(objBLLTourFreeFormBooking.FromDate, Date), "yyyy/MM/dd") & "</searchfromdate>")
            strBuffer.Append("<searchtodate>" & Format(CType(objBLLTourFreeFormBooking.ToDate, Date), "yyyy/MM/dd") & "</searchtodate>")
            strBuffer.Append("<noofdays>" & iNoOfDays.ToString & "</noofdays>")

            Dim PriceVATPerc As Double = 0
            Dim PriceTaxableValue As Double = 0
            Dim PriceVATValue As Double = 0


            Dim CostPriceVATPerc As Double = 0
            Dim CostPriceTaxableValue As Double = 0
            Dim CostPriceVATValue As Double = 0


            Dim strSqlQry As String = "select option_selected    from reservation_parameters where param_id =2013"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            Dim myDS = New DataSet
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)


            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    PriceVATPerc = myDS.Tables(0).Rows(0)(0)
                End If
            End If



            PriceTaxableValue = Val(totalsalevalue) / (1 + (Val(PriceVATPerc) / 100))
            PriceVATValue = Val(totalsalevalue - PriceTaxableValue)


            CostPriceTaxableValue = Val(totalCostvalue) / (1 + (Val(PriceVATPerc) / 100))
            CostPriceVATValue = Val(totalCostvalue - CostPriceTaxableValue)


            strBuffer.Append("<PriceVATPerc>" & PriceVATPerc.ToString("0.000") & "</PriceVATPerc>")
            strBuffer.Append("<PriceWithTax>" & "1" & "</PriceWithTax>")
            strBuffer.Append("<PriceTaxableValue>" & PriceTaxableValue.ToString("0.000") & "</PriceTaxableValue>")
            strBuffer.Append("<PriceVATValue>" & PriceVATValue.ToString("0.000") & "</PriceVATValue>")
            strBuffer.Append("<CostTaxableValue>" & CostPriceTaxableValue.ToString & "</CostTaxableValue>")
            strBuffer.Append("<CostVATValue>" & CostPriceVATValue.ToString & "</CostVATValue>")
            strBuffer.Append("<CostVATPerc>" & PriceVATPerc.ToString("0.000") & "</CostVATPerc>")
            strBuffer.Append("<CostWithTax>" & "1" & "</CostWithTax>")
            strBuffer.Append("</Table>")


            If Txt_TourType.Text.ToString = "COMBO" Or Txt_TourType.Text.ToString = "MULTIPLE DATE" Then
                Dim dtselectedCombotour As New DataTable
                dtselectedCombotour = Session("selectedCombotourdatatable")


                Dim dvComboBreakup As DataView
                dvComboBreakup = New DataView(dtselectedCombotour)
                'dvComboBreakup.RowFilter = "exctypcode= '" & myrow("exctypcode").ToString & "'  AND  vehiclecode='" & myrow("vehiclecode").ToString & "'  AND type='" & strType & "' "
                Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
                If dtComboBreakup.Rows.Count > 0 Then
                    For i As Integer = 0 To dtComboBreakup.Rows.Count - 1
                        strBufferCombo.Append("<Table>")
                        strBufferCombo.Append("<elineno>" & CType(strElineno, String) & "</elineno>")
                        strBufferCombo.Append("<clineno>" & CType(i + 1, String) & "</clineno>")
                        strBufferCombo.Append("<exctypcode>" & dtComboBreakup.Rows(i)("exctypcode").ToString & "</exctypcode>")
                        strBufferCombo.Append("<vehiclecode>" & dtComboBreakup.Rows(i)("vehiclecode").ToString & "</vehiclecode>")
                        strBufferCombo.Append("<exctypcombocode>" & dtComboBreakup.Rows(i)("exctypcombocode").ToString & "</exctypcombocode>")
                        strBufferCombo.Append("<excdate>" & Format(CType(dtComboBreakup.Rows(i)("excdate").ToString, Date), "yyyy/MM/dd") & "</excdate>")
                        strBufferCombo.Append("<Type>" & dtComboBreakup.Rows(i)("Type").ToString & "</Type>")

                        strBufferCombo.Append("</Table>")
                    Next
                End If

            End If

            strBuffer.Append("</DocumentElement>")
            strBufferCombo.Append("</DocumentElement>")





            strBufferMultiCostBreakup.Append("</DocumentElement>")
            objBLLTourFreeFormBooking.EBToursXml = strBuffer.ToString()
            objBLLTourFreeFormBooking.EBuserlogged = Session("GlobalUserName")
            objBLLTourFreeFormBooking.RowLineNo = strRowLineNo
            objBLLTourFreeFormBooking.BufferMultiCostBreakup = strBufferMultiCostBreakup.ToString
            objBLLTourFreeFormBooking.BufferComboBreakup = strBufferCombo.ToString








            If Not Session("sobjResParam") Is Nothing Then
                objResParam = Session("sobjResParam")
                objBLLTourFreeFormBooking.SubUserCode = objResParam.SubUserCode
            End If




            If objBLLTourFreeFormBooking.SaveTourFreeFormTemp() Then
                Session("sRequestId") = objBLLTourFreeFormBooking.EbRequestID
                Session("sobjBLLTourFreeFormBooking") = objBLLTourFreeFormBooking
                Response.Redirect("MoreServices.aspx")
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnSaveComboExcursion_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Protected Sub lbSelectDate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim myLinkButton As LinkButton = CType(sender, LinkButton)
        Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)

        Dim lbPrice As LinkButton = CType(dlItem.FindControl("lbPrice"), LinkButton)
        Session("slbTourTotalSaleValue") = lbPrice

        Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
        Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
        Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
        Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
        'hdCurrCodePopup.Value = hdCurrCode.Value
        Dim hdCombo As HiddenField = CType(dlItem.FindControl("hdCombo"), HiddenField)
        Dim hdMultipleDates As HiddenField = CType(dlItem.FindControl("hdMultipleDates"), HiddenField)

        If hdCombo.Value = "YES" Then
            lblComboExcName.Text = lblExcName.Text
            hdExcCodeComboPopup.Value = hdExcCode.Value
            'hdVehicleCodeComboPopup.Value = hdVehicleCode.Value
            Dim dt As New DataTable
            dt = objBLLTourSearch.GetComboExcursions(hdExcCode.Value)
            dlSelectComboDates.DataSource = dt
            dlSelectComboDates.DataBind()
            If dlSelectComboDates.Items.Count > 0 Then
                Dim dtCombo As New DataTable
                dtCombo = Session("selectedCombotourdatatable")
                For Each dlItem1 As DataListItem In dlSelectComboDates.Items
                    Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
                    Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
                    Dim foundRow As DataRow
                    foundRow = dtCombo.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='COMBO' ").FirstOrDefault
                    If Not foundRow Is Nothing Then
                        txtExcComboDate.Text = foundRow("excdate")
                    End If
                Next

            End If

            'mpSelectComboDates.Show()
        End If
        If hdMultipleDates.Value = "YES" Then
            'hdChangeFromdate
            'hdChangeTodate
            Dim dtDates As DataTable
            dtDates = objBLLTourSearch.GetMultipleDates(hdChangeFromdate.Value, hdChangeTodate.Value, hdExcCode.Value)

            If dtDates.Rows.Count > 0 Then
                dlMultipleDate.DataSource = dtDates
                dlMultipleDate.DataBind()
                lblSelectMultipleDates.Text = "Excursion: " & lblExcName.Text
                hdMealPlanExcCode.Value = hdExcCode.Value
                'hdMealPlanVehicleCode.Value = hdVehicleCode.Value

                If dlMultipleDate.Items.Count > 0 Then
                    Dim dtMultiDates As New DataTable
                    dtMultiDates = Session("selectedCombotourdatatable")
                    For Each dlItem1 As DataListItem In dlMultipleDate.Items
                        Dim lblMeanPlanDates As Label = CType(dlItem1.FindControl("lblMeanPlanDates"), Label)
                        Dim chkMealPlanDates As CheckBox = CType(dlItem1.FindControl("chkMealPlanDates"), CheckBox)
                        Dim foundRow As DataRow
                        foundRow = dtMultiDates.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND exctypcombocode='" & hdExcCode.Value.Trim & "' AND excdate='" & lblMeanPlanDates.Text.Trim & "' AND type='MULTI_DATE' ").FirstOrDefault
                        If Not foundRow Is Nothing Then
                            chkMealPlanDates.Checked = True
                        End If
                    Next

                End If


                'mpMealPlanDatesPopup.Show()
            Else
                MessageBox.ShowMessage(Page, MessageType.Warning, lblSelectMultipleDates.Text & " is not operational on these dates.")

            End If


        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                ' ''If Session("sDSTourSearchResults") IsNot Nothing Then
                ' ''    'changed by mohamed on 12/02/2018
                ' ''    'txtSearchTour.Text = ""
                ' ''    'btnTourTextSearch.Enabled = False
                ' ''    ' ddlSorting.Enabled = False
                ' ''End If

                Session("State") = "New"
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

                If Not Session("sobjResParam") Is Nothing Then
                    objResParam = Session("sobjResParam")
                    hdWhiteLabel.Value = objResParam.WhiteLabel
                End If
                btnMulticost.Visible = False
                LoadHome()

                Txt_NoOfSeniors.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfSeniors.ClientID, String) + "', '" + CType(Txt_PriceSenior.ClientID, String) + "' ,'" + CType(Txt_SaleValueSenior.ClientID, String) + "' ,'0')")
                Txt_PriceSenior.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfSeniors.ClientID, String) + "', '" + CType(Txt_PriceSenior.ClientID, String) + "' ,'" + CType(Txt_SaleValueSenior.ClientID, String) + "','0')")
                Txt_SaleValueSenior.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfSeniors.ClientID, String) + "', '" + CType(Txt_PriceSenior.ClientID, String) + "' ,'" + CType(Txt_SaleValueSenior.ClientID, String) + "','0')")

                Txt_NoOfAdults.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfAdults.ClientID, String) + "', '" + CType(Txt_PriceAdult.ClientID, String) + "' ,'" + CType(Txt_SaleValueAdult.ClientID, String) + "','1')")
                Txt_PriceAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfAdults.ClientID, String) + "', '" + CType(Txt_PriceAdult.ClientID, String) + "' ,'" + CType(Txt_SaleValueAdult.ClientID, String) + "' ,'1')")
                Txt_SaleValueAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfAdults.ClientID, String) + "', '" + CType(Txt_PriceAdult.ClientID, String) + "' ,'" + CType(Txt_SaleValueAdult.ClientID, String) + "','1')")

                Txt_NoOfChild.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfChild.ClientID, String) + "', '" + CType(Txt_PriceChild.ClientID, String) + "' ,'" + CType(Txt_SaleValueChild.ClientID, String) + "','0')")
                Txt_PriceChild.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfChild.ClientID, String) + "', '" + CType(Txt_PriceChild.ClientID, String) + "' ,'" + CType(Txt_SaleValueChild.ClientID, String) + "','0')")
                Txt_SaleValueChild.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfChild.ClientID, String) + "', '" + CType(Txt_PriceChild.ClientID, String) + "' ,'" + CType(Txt_SaleValueChild.ClientID, String) + "','0')")

                Txt_NoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfUnits.ClientID, String) + "', '" + CType(Txt_PriceUnits.ClientID, String) + "' ,'" + CType(Txt_SaleValueUnits.ClientID, String) + "','0')")
                Txt_PriceUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfUnits.ClientID, String) + "', '" + CType(Txt_PriceUnits.ClientID, String) + "' ,'" + CType(Txt_SaleValueUnits.ClientID, String) + "','0')")
                Txt_SaleValueUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfUnits.ClientID, String) + "', '" + CType(Txt_PriceUnits.ClientID, String) + "' ,'" + CType(Txt_SaleValueUnits.ClientID, String) + "','0')")

                Txt_NoOfChildAsAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfChildAsAdult.ClientID, String) + "', '" + CType(Txt_PriceChildAsAdult.ClientID, String) + "' ,'" + CType(Txt_SaleValueChildAsAdult.ClientID, String) + "','2')")
                Txt_PriceChildAsAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfChildAsAdult.ClientID, String) + "', '" + CType(Txt_PriceChildAsAdult.ClientID, String) + "' ,'" + CType(Txt_SaleValueChildAsAdult.ClientID, String) + "','2')")
                Txt_SaleValueChildAsAdult.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(Txt_NoOfChildAsAdult.ClientID, String) + "', '" + CType(Txt_PriceChildAsAdult.ClientID, String) + "' ,'" + CType(Txt_SaleValueChildAsAdult.ClientID, String) + "','2')")


                Txt_CostNoOfSeniors.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfSeniors.ClientID, String) + "', '" + CType(Txt_CostPriceSenior.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueSenior.ClientID, String) + "' ,'0')")
                Txt_CostPriceSenior.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfSeniors.ClientID, String) + "', '" + CType(Txt_CostPriceSenior.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueSenior.ClientID, String) + "','0')")
                Txt_CostSaleValueSenior.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfSeniors.ClientID, String) + "', '" + CType(Txt_CostPriceSenior.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueSenior.ClientID, String) + "','0')")

                Txt_CostNoOfAdults.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfAdults.ClientID, String) + "', '" + CType(Txt_CostPriceAdult.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueAdult.ClientID, String) + "','1')")
                Txt_CostPriceAdult.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfAdults.ClientID, String) + "', '" + CType(Txt_CostPriceAdult.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueAdult.ClientID, String) + "' ,'1')")
                Txt_CostSaleValueAdult.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfAdults.ClientID, String) + "', '" + CType(Txt_CostPriceAdult.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueAdult.ClientID, String) + "','1')")

                Txt_CostNoOfChild.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfChild.ClientID, String) + "', '" + CType(Txt_CostPriceChild.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueChild.ClientID, String) + "','0')")
                Txt_CostPriceChild.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfChild.ClientID, String) + "', '" + CType(Txt_CostPriceChild.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueChild.ClientID, String) + "','0')")
                Txt_CostSaleValueChild.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfChild.ClientID, String) + "', '" + CType(Txt_CostPriceChild.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueChild.ClientID, String) + "','0')")

                Txt_CostNoOfUnits.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfUnits.ClientID, String) + "', '" + CType(Txt_CostPriceUnits.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueUnits.ClientID, String) + "','0')")
                Txt_CostPriceUnits.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfUnits.ClientID, String) + "', '" + CType(Txt_CostPriceUnits.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueUnits.ClientID, String) + "','0')")
                Txt_CostSaleValueUnits.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfUnits.ClientID, String) + "', '" + CType(Txt_CostPriceUnits.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueUnits.ClientID, String) + "','0')")

                Txt_CostNoOfChildAsAdult.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfChildAsAdult.ClientID, String) + "', '" + CType(Txt_CostPriceChildAsAdult.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueChildAsAdult.ClientID, String) + "','2')")
                Txt_CostPriceChildAsAdult.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfChildAsAdult.ClientID, String) + "', '" + CType(Txt_CostPriceChildAsAdult.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueChildAsAdult.ClientID, String) + "','2')")
                Txt_CostSaleValueChildAsAdult.Attributes.Add("onChange", "javascript:CalculateCostValue('" + CType(Txt_CostNoOfChildAsAdult.ClientID, String) + "', '" + CType(Txt_CostPriceChildAsAdult.ClientID, String) + "' ,'" + CType(Txt_CostSaleValueChildAsAdult.ClientID, String) + "','2')")


            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("TourSearch.aspx :: Page_Load :: " & ex.Message & ":: " & Session("GlobalUserName"))
                ModalPopupDays.Hide()
            End Try

        End If
    End Sub

    Private Sub LoadHome()
        LoadFooter()
        LoadLogo()
        LoadMenus()
        'objclsUtilities.LoadTheme(Session("sAgentCompany"), lnkCSS)
        lnkCSS.Attributes("href") = Session("strTheme").ToString '*** Danny 12/09/2018
        LoadRoomAdultChild()
        LoadFields()
        ShowMyBooking()
        CreateSelectedTourDataTable()
        CreateComboTourDataTable()
        ViewState("OLineNo") = Nothing
        If Not Request.QueryString("OLineNo") Is Nothing Then
            hdnlineno.Value = Request.QueryString("OLineNo")
            ViewState("OLineNo") = Request.QueryString("OLineNo")
        Else
            hdnlineno.Value = "0"
            ViewState("OLineNo") = 0
        End If

        FillTourPickupLocation()

        objBLLTourSearch = New BLLTourSearch

        If Not Session("sEditRequestId") Is Nothing Then

            If ViewState("OLineNo") Is Nothing Then

                NewHeaderFill()

            Else
                Amendheaderfill()
            End If
        Else
            If Not Session("sRequestId") Is Nothing Then
                If ViewState("OLineNo") Is Nothing Then

                    NewHeaderFill()
                Else
                    If ViewState("OLineNo") = "0" Then
                        NewHeaderFill()
                    Else
                        EditHeaderFill()

                    End If

                End If
            Else
                BindHotelCheckInAndCheckOutHiddenfield()
                If Not Page.Request.UrlReferrer Is Nothing Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    If previousPage.Contains("MoreServices.aspx") Then
                        BindTourChildAge()
                    End If
                End If
            End If
        End If

        If Not Session("sRequestId") Is Nothing Then

            If Not ViewState("OLineNo") Is Nothing And Val(ViewState("OLineNo")) > 0 Then
                FillTourFreeFormBooking()
            End If
            'Else
            '    If Not Session("sobjBLLTourSearch") Is Nothing Then
            '        If ViewState("Elineno") Is Nothing Then
            '            NewHeaderFill()
            '        Else
            '            EditHeaderFill()
            '        End If
            '    Else
            '        BindHotelCheckInAndCheckOutHiddenfield()
            '        If Not Page.Request.UrlReferrer Is Nothing Then
            '            Dim previousPage As String = Page.Request.UrlReferrer.ToString
            '            If previousPage.Contains("MoreServices.aspx") Then
            '                BindTourChildAge()
            '            End If
            '        End If
            '    End If
        End If

        ''*** For Edit

        'If Page.Request.UrlReferrer.ToString.con Then


        If hdWhiteLabel.Value = "1" Then
            dvMagnifyingMemories.Visible = False
        End If



    End Sub

    Private Sub FillTourFreeFormBooking()
        Dim strrequestid As String = ""

        strrequestid = GetExistingRequestId()
        Dim objBLLOtherServiceFreeFormBooking As New BLLOtherServiceFreeFormBooking
        Dim dsTourFreeFormBooking = New DataSet
        dsTourFreeFormBooking = objBLLTourFreeFormBooking.GetTourFreeFormBookingfromTemp(strrequestid, hdnlineno.Value)

        If dsTourFreeFormBooking.Tables(0).Rows.Count > 0 Then
            hdVehicleCode.Value = dsTourFreeFormBooking.Tables(0).Rows(0)("vehiclecode").ToString

            Txt_Tours.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("exctypname").ToString
            Txt_ToursCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("exctypcode").ToString
            txtTourStartingFrom.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("othtypname").ToString
            txtTourStartingFromCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("tourstartingfromCODE").ToString
            txtTourClassification.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("classificationname").ToString
            txtTourClassificationCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("classificationcode").ToString
            ddlStarCategory.SelectedValue = dsTourFreeFormBooking.Tables(0).Rows(0)("starcat").ToString

            hdTourRateBasis.Value = dsTourFreeFormBooking.Tables(0).Rows(0)("ratebasis").ToString

            txtTourCustomer.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("agentname").ToString
            txtTourCustomerCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("agentcode").ToString
            txtTourSourceCountry.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("ctryname").ToString
            txtTourSourceCountryCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("ctrycode").ToString
            Txt_TourCategory.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("sicpvt").ToString
            Txt_TourCategoryCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("sicpvt").ToString
            Txt_TourType.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("exctyp").ToString
            Txt_TourTypeCode.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("exctyp").ToString
            If dsTourFreeFormBooking.Tables(0).Rows(0)("comp_cust").ToString = 1 Then
                Chk_Complimentary.Checked = True
            End If

            ddlSeniorCitizen.SelectedValue = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString
            ddlTourAdult.SelectedValue = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString
            ddlTourChildren.SelectedValue = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString

            'If (hdTourRateBasis.Value <> "UNIT") Then
            ddlTourChildren.SelectedValue = ddlTourChildren.SelectedValue + Val(dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString)
            'End If

            hdCurrCode.Value = dsTourFreeFormBooking.Tables(0).Rows(0)("currcode").ToString
            If Txt_TourType.Text = "MULTIPLE DATE" Then
                txtTourFromDate.Text = Format(CType(dsTourFreeFormBooking.Tables(0).Rows(0)("searchfromdate").ToString(), Date), "dd/MM/yyyy")
                txtTourToDate.Text = Format(CType(dsTourFreeFormBooking.Tables(0).Rows(0)("searchtodate").ToString, Date), "dd/MM/yyyy")
                Txt_TourDate.Text = ""
                'If hdTourRateBasis.Value.ToString = "ACS" Then

                'End If
            End If
            If Txt_TourType.Text = "NORMAL" Then
                txtTourFromDate.Text = ""
                txtTourToDate.Text = ""
                Txt_TourDate.Text = Format(CType(dsTourFreeFormBooking.Tables(0).Rows(0)("searchfromdate").ToString, Date), "dd/MM/yyyy")
                'If hdTourRateBasis.Value.ToString = "ACS" Then

                'End If
            End If


            SetChilds(dsTourFreeFormBooking.Tables(0).Rows(0)("childages").ToString)

            FillTourComboAndOthers(dsTourFreeFormBooking)
        End If
    End Sub
    Private Sub FillTourComboAndOthers(ByVal dsTourFreeFormBooking As DataSet)
        Try

            Dim divSelectComboDates As HtmlGenericControl = CType(FindControl("divSelectComboDates"), HtmlGenericControl)
            divSelectComboDates.Style.Item("display") = "none"

            Dim divSelectMultipleDates As HtmlGenericControl = CType(FindControl("divSelectMultipleDates"), HtmlGenericControl)
            divSelectMultipleDates.Style.Item("display") = "none"

            Dim divSelectNormal As HtmlGenericControl = CType(FindControl("divSelectNormal"), HtmlGenericControl)
            divSelectNormal.Style.Item("display") = "none"

            Dim divComplimentary As HtmlGenericControl = CType(FindControl("divComplimentary"), HtmlGenericControl)
            divComplimentary.Style.Item("display") = "none"

            Dim divTotalSale As HtmlGenericControl = CType(FindControl("divTotalSale"), HtmlGenericControl)
            divTotalSale.Style.Item("display") = "none"

            Dim divAdult As HtmlGenericControl = CType(FindControl("divAdult"), HtmlGenericControl)
            divAdult.Style.Item("display") = "none"

            'Dim divClearAdult As HtmlGenericControl = CType(FindControl("divClearAdult"), HtmlGenericControl)
            'divClearAdult.Style.Item("display") = "none"

            Dim divChild As HtmlGenericControl = CType(FindControl("divChild"), HtmlGenericControl)
            divChild.Style.Item("display") = "none"

            Dim divSenior As HtmlGenericControl = CType(FindControl("divSenior"), HtmlGenericControl)
            divSenior.Style.Item("display") = "none"

            Dim divpTotal As HtmlGenericControl = CType(FindControl("divpTotal"), HtmlGenericControl)
            divpTotal.Style.Item("display") = "none"

            Dim divpsenior As HtmlGenericControl = CType(FindControl("divpsenior"), HtmlGenericControl)
            divpsenior.Style.Item("display") = "none"

            Dim divChildAsAdult As HtmlGenericControl = CType(FindControl("divChildAsAdult"), HtmlGenericControl)
            divChildAsAdult.Style.Item("display") = "none"

            Dim divUnits As HtmlGenericControl = CType(FindControl("divUnits"), HtmlGenericControl)
            divUnits.Style.Item("display") = "none"

            dlSelectComboDates.DataSource = Nothing
            dlSelectComboDates.DataBind()

            dlMultipleDate.DataSource = Nothing
            dlMultipleDate.DataBind()

            Lbl_CurCodeAdult.Text = ""
            Lbl_CurCodeChAdult.Text = ""
            Lbl_CurCodeChild.Text = ""
            Lbl_CurCodeUnit.Text = ""
            Lbl_CurCodeSenior.Text = ""

            Txt_NoOfSeniors.Text = ""
            Txt_PriceSenior.Text = ""
            Txt_SaleValueSenior.Text = ""
            Txt_NoOfAdults.Text = ""
            Txt_PriceAdult.Text = ""
            Txt_SaleValueAdult.Text = ""
            Txt_NoOfChild.Text = ""
            Txt_PriceChild.Text = ""
            Txt_SaleValueChild.Text = ""
            Txt_NoOfChildAsAdult.Text = ""
            Txt_PriceChildAsAdult.Text = ""
            Txt_SaleValueChildAsAdult.Text = ""
            Txt_SaleValueUnits.Text = ""
            Txt_PriceUnits.Text = ""
            Txt_NoOfUnits.Text = ""

            objBLLTourFreeFormBooking.ChildAgeString = ""

            'hdTourRateBasis.Value = ""

            objBLLTourFreeFormBooking.AgentCode = Session("sAgentCode")

            Dim strAdult As String = ddlTourAdult.SelectedValue
            Dim strChildren As String = ddlTourChildren.SelectedValue

            Dim strChild1 As String = "0"
            Dim strChild2 As String = "0"
            Dim strChild3 As String = "0"
            Dim strChild4 As String = "0"
            Dim strChild5 As String = "0"
            Dim strChild6 As String = "0"
            Dim strChild7 As String = "0"
            Dim strChild8 As String = "0"
            Dim intChAsAdult As Integer = 0

            If strChildren <> "0" Then
                If strChildren = "1" Then
                    If strChild1 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If

                    strChild1 = txtTourChild1.Text
                    strChild2 = ""
                    strChild3 = ""
                    strChild4 = ""
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1
                ElseIf strChildren = "2" Then
                    If strChild1 = "" Or strChild2 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If

                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = ""
                    strChild4 = ""
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2
                ElseIf strChildren = "3" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = ""
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
                ElseIf strChildren = "4" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = txtTourChild4.Text
                    strChild5 = ""
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
                ElseIf strChildren = "5" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = txtTourChild4.Text
                    strChild5 = txtTourChild5.Text
                    strChild6 = ""
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
                ElseIf strChildren = "6" Then
                    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                        Exit Sub
                    End If
                    strChild1 = txtTourChild1.Text
                    strChild2 = txtTourChild2.Text
                    strChild3 = txtTourChild3.Text
                    strChild4 = txtTourChild4.Text
                    strChild5 = txtTourChild5.Text
                    strChild6 = txtTourChild6.Text
                    strChild7 = ""
                    strChild8 = ""
                    objBLLTourFreeFormBooking.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
                    'ElseIf strChildren = "7" Then
                    '    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Then
                    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                    '        Exit Sub
                    '    End If
                    'ElseIf strChildren = "8" Then
                    '    If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Or strChild8 = "" Then
                    '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
                    '        Exit Sub
                    '    End If
                End If

            End If


            hdChildAgeString.Value = objBLLTourFreeFormBooking.ChildAgeString

            If Txt_TourType.Text = "COMBO" Then

                hdExcCodeComboPopup.Value = Txt_ToursCode.Text
                'hdVehicleCodeComboPopup.Value = hdVehicleCode.Value
                Dim dt As New DataTable
                dt = dsTourFreeFormBooking.Tables(1)
                dlSelectComboDates.DataSource = dt
                dlSelectComboDates.DataBind()
                If dlSelectComboDates.Items.Count > 0 Then
                    Dim dtCombo As New DataTable
                    dtCombo = Session("selectedCombotourdatatable")
                    For Each dlItem1 As DataListItem In dlSelectComboDates.Items
                        Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
                        Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
                        Dim foundRow As DataRow
                        foundRow = dt.Select("exctypcode='" & Txt_ToursCode.Text.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value & "' AND exctypcombocode='" & lblExcComboCode.Text.Trim & "' AND type='COMBO' ").FirstOrDefault
                        If Not foundRow Is Nothing Then
                            txtExcComboDate.Text = foundRow("excdate")
                        End If
                    Next
                    hdTourRateBasis.Value = dt.Rows(0)("ratebasis").ToString

                    hdChangeFromdate.Value = DateTime.Now
                    'hdChangeTodate.Value = 0

                    If dt.Rows(0)("ratebasis").ToString = "ACS" Then
                        divUnits.Style.Item("display") = "none"
                        divAdult.Style.Item("display") = "block"

                        divCostUnits.Style.Item("display") = "none"
                        divCostAdult.Style.Item("display") = "block"

                        'divClearAdult.Style.Item("display") = "block"
                        'divChild.Style.Item("display") = "block"
                        'divSenior.Style.Item("display") = "block"
                        'divChildAsAdult.Style.Item("display") = "block"


                    Else
                        divUnits.Style.Item("display") = "block"
                        divCostUnits.Style.Item("display") = "block"
                        Txt_NoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()
                    End If

                    Dim fromAge As Decimal = dt.Rows(0)("chidlagefrom").ToString()
                    Dim toAge As Decimal = dt.Rows(0)("childageto").ToString()

                    ' ''If strChild1 <> "" Then
                    ' ''    If CType(strChild1, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    ' ''If strChild2 <> "" Then
                    ' ''    If CType(strChild2, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    ' ''If strChild3 <> "" Then
                    ' ''    If CType(strChild3, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    ' ''If strChild4 <> "" Then
                    ' ''    If CType(strChild4, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    ' ''If strChild5 <> "" Then
                    ' ''    If CType(strChild5, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    ' ''If strChild6 <> "" Then
                    ' ''    If CType(strChild6, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    ' ''If strChild7 <> "" Then
                    ' ''    If CType(strChild7, Integer) > toAge Then
                    ' ''        intChAsAdult = intChAsAdult + 1
                    ' ''    End If
                    ' ''End If
                    Txt_NoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                    Txt_NoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()

                    Txt_NoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                    Txt_NoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                    Txt_NoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()

                    Txt_PriceSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorprice").ToString()
                    Txt_PriceAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultprice").ToString()
                    Txt_PriceChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childprice").ToString()
                    Txt_PriceChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultprice").ToString()
                    Txt_PriceUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitprice").ToString()

                    Txt_SaleValueSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorsalevalue").ToString()
                    Txt_SaleValueAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultsalevalue").ToString()
                    Txt_SaleValueChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childsalevalue").ToString()
                    Txt_SaleValueChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultvalue").ToString()
                    Txt_SaleValueUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitsalevalue").ToString()

                    Txt_TotalSaleValue.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("totalsalevalue").ToString()

                    Txt_CostNoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                    Txt_CostNoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()

                    Txt_CostNoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                    Txt_CostNoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                    Txt_CostNoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()

                    Txt_CostPriceSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorcprice").ToString()
                    Txt_CostPriceAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultcprice").ToString()
                    Txt_CostPriceChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childcprice").ToString()
                    Txt_CostPriceChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultcprice").ToString()
                    Txt_CostPriceUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitcprice").ToString()

                    Txt_CostSaleValueSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorcostvalue").ToString()
                    Txt_CostSaleValueAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultcostvalue").ToString()
                    Txt_CostSaleValueChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childcostvalue").ToString()
                    Txt_CostSaleValueChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultcostvalue").ToString()
                    Txt_CostSaleValueUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitcostvalue").ToString()

                    Txt_TotalCostValue.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("totalcostvalue").ToString()


                    Lbl_CurCodeAdult.Text = hdCurrCode.Value
                    Lbl_CurCodeChAdult.Text = hdCurrCode.Value
                    Lbl_CurCodeChild.Text = hdCurrCode.Value
                    Lbl_CurCodeUnit.Text = hdCurrCode.Value
                    Lbl_CurCodeSenior.Text = hdCurrCode.Value

                    divSelectComboDates.Style.Item("display") = "block"
                    btnSaveComboExcursion.Visible = True
                    divComplimentary.Style.Item("display") = "block"
                    divTotalSale.Style.Item("display") = "block"
                    divTotalCost.Style.Item("display") = "block"

                    divpsenior.Style.Item("display") = "block"
                    divpTotal.Style.Item("display") = "block"
                    divCostArea.Style.Item("display") = "block"
                    divpCostTotal.Style.Item("display") = "block"

                    If dt.Rows(0)("ratebasis").ToString = "ACS" Then
                        If Val(Txt_NoOfChild.Text.Trim()) > 0 Then
                            divChild.Style.Item("display") = "block"
                            divCostChild.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfSeniors.Text.Trim()) > 0 Then
                            divSenior.Style.Item("display") = "block"
                            divCostSenior.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0 Then
                            divChildAsAdult.Style.Item("display") = "block"
                            divCostChildAsAdult.Style.Item("display") = "block"
                        End If
                    End If
                Else
                    'lblSelectMultipleDates.Visible = False
                End If

            End If
            If Txt_TourType.Text = "MULTIPLE DATE" Then
                ' ''            dlMultipleDate.DataSource = dtDates
                ' ''            dlMultipleDate.DataBind()
                ' ''            lblSelectMultipleDates.Text = "Excursion: " & lblExcName.Text
                ' ''            hdMealPlanExcCode.Value = hdExcCode.Value
                ' ''            hdMealPlanVehicleCode.Value = hdVehicleCode.Value
                'hdChangeFromdate
                'hdChangeTodate
                Dim dsDates As DataSet
                dsDates = objBLLTourSearch.GetMultipleDates_WithRateBasis(txtTourFromDate.Text, txtTourToDate.Text, Txt_ToursCode.Text)

                If dsDates.Tables(0).Rows.Count > 0 Then
                    dlMultipleDate.DataSource = dsDates.Tables(0)
                    dlMultipleDate.DataBind()
                    'lblSelectMultipleDates.Text = "Excursion: " & lblExcName.Text
                    hdMealPlanExcCode.Value = Txt_ToursCode.Text
                    'hdMealPlanVehicleCode.Value = Txt_ToursCode.Text

                    If dlMultipleDate.Items.Count > 0 Then
                        Dim dtMultiDates As New DataTable
                        dtMultiDates = Session("selectedCombotourdatatable")
                        For Each dlItem1 As DataListItem In dlMultipleDate.Items
                            Dim lblMeanPlanDates As Label = CType(dlItem1.FindControl("lblMeanPlanDates"), Label)
                            Dim chkMealPlanDates As CheckBox = CType(dlItem1.FindControl("chkMealPlanDates"), CheckBox)
                            Dim foundRow As DataRow
                            foundRow = dsTourFreeFormBooking.Tables(1).Select("exctypcode='" & Txt_ToursCode.Text.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND excdate='" & lblMeanPlanDates.Text.Trim & "' AND type='MULTI_DATE' ").FirstOrDefault
                            If Not foundRow Is Nothing Then
                                chkMealPlanDates.Checked = True
                            End If
                        Next

                        hdTourRateBasis.Value = dsTourFreeFormBooking.Tables(0).Rows(0)("ratebasis").ToString

                        If hdTourRateBasis.Value.ToString = "ACS" Then
                            divUnits.Style.Item("display") = "none"
                            divAdult.Style.Item("display") = "block"
                            divCostUnits.Style.Item("display") = "none"
                            divCostAdult.Style.Item("display") = "block"
                            'divClearAdult.Style.Item("display") = "block"
                            'divChild.Style.Item("display") = "block"
                            'divSenior.Style.Item("display") = "block"
                            'divChildAsAdult.Style.Item("display") = "block"

                        Else
                            divUnits.Style.Item("display") = "block"
                            divCostUnits.Style.Item("display") = "block"
                            'Txt_NoOfUnits.Text = ddlTourAdult.SelectedValue.ToString()
                            'Txt_NoOfUnits.Text = 1
                        End If
                        Dim fromAge As Decimal = dsTourFreeFormBooking.Tables(1).Rows(0)("chidlagefrom").ToString()
                        Dim toAge As Decimal = dsTourFreeFormBooking.Tables(1).Rows(0)("childageto").ToString()

                        'If strChild1 <> "" Then
                        '    If CType(strChild1, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If
                        'If strChild2 <> "" Then
                        '    If CType(strChild2, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If
                        'If strChild3 <> "" Then
                        '    If CType(strChild3, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If
                        'If strChild4 <> "" Then
                        '    If CType(strChild4, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If
                        'If strChild5 <> "" Then
                        '    If CType(strChild5, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If
                        'If strChild6 <> "" Then
                        '    If CType(strChild6, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If
                        'If strChild7 <> "" Then
                        '    If CType(strChild7, Integer) > toAge Then
                        '        intChAsAdult = intChAsAdult + 1
                        '    End If
                        'End If

                        Txt_NoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                        Txt_NoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()

                        Txt_NoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                        Txt_NoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                        Txt_NoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()



                        Txt_CostNoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                        Txt_CostNoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()

                        Txt_CostNoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                        Txt_CostNoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                        Txt_CostNoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()

                        Txt_PriceSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorprice").ToString()
                        Txt_PriceAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultprice").ToString()
                        Txt_PriceChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childprice").ToString()
                        Txt_PriceChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultprice").ToString()
                        Txt_PriceUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitprice").ToString()

                        Txt_SaleValueSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorsalevalue").ToString()
                        Txt_SaleValueAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultsalevalue").ToString()
                        Txt_SaleValueChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childsalevalue").ToString()
                        Txt_SaleValueChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultvalue").ToString()
                        Txt_SaleValueUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitsalevalue").ToString()

                        Txt_TotalSaleValue.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("totalsalevalue").ToString()

                        Txt_CostPriceSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorcprice").ToString()
                        Txt_CostPriceAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultcprice").ToString()
                        Txt_CostPriceChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childcprice").ToString()
                        Txt_CostPriceChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultcprice").ToString()
                        Txt_CostPriceUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitcprice").ToString()

                        Txt_CostSaleValueSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorcostvalue").ToString()
                        Txt_CostSaleValueAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultcostvalue").ToString()
                        Txt_CostSaleValueChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childcostvalue").ToString()
                        Txt_CostSaleValueChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultcostvalue").ToString()
                        Txt_CostSaleValueUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitcostvalue").ToString()

                        Txt_TotalCostValue.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("totalcostvalue").ToString()


                        Lbl_CurCodeAdult.Text = hdCurrCode.Value
                        Lbl_CurCodeChAdult.Text = hdCurrCode.Value
                        Lbl_CurCodeChild.Text = hdCurrCode.Value
                        Lbl_CurCodeUnit.Text = hdCurrCode.Value
                        Lbl_CurCodeSenior.Text = hdCurrCode.Value
                    End If

                    divSelectMultipleDates.Style.Item("display") = "block"
                    'btnTourFill.Visible = False


                    btnSaveComboExcursion.Visible = True
                    divComplimentary.Style.Item("display") = "block"
                    divTotalSale.Style.Item("display") = "block"
                    divpsenior.Style.Item("display") = "block"
                    divpTotal.Style.Item("display") = "block"

                    divTotalCost.Style.Item("display") = "block"
                    divCostArea.Style.Item("display") = "block"
                    divpCostTotal.Style.Item("display") = "block"

                    If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                        If Val(Txt_NoOfChild.Text.Trim()) > 0 Then
                            divChild.Style.Item("display") = "block"
                            divCostChild.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfSeniors.Text.Trim()) > 0 Then
                            divSenior.Style.Item("display") = "block"
                            divCostSenior.Style.Item("display") = "block"
                        End If
                        If Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0 Then
                            divChildAsAdult.Style.Item("display") = "block"
                            divCostChildAsAdult.Style.Item("display") = "block"
                        End If
                    End If
                Else
                    'MessageBox.ShowMessage(Page, MessageType.Warning, lblSelectMultipleDates.Text & " is not operational on these dates.")

                End If
                Dim fromto As HtmlGenericControl = CType(FindControl("fromto"), HtmlGenericControl)
                divUnits.Style.Item("fromto") = "block"
                divCostUnits.Style.Item("fromto") = "block"

            End If
            If Txt_TourType.Text = "NORMAL" Then

                Dim dsDates As DataSet
                dsDates = objBLLTourSearch.GetMultipleDates_WithRateBasis(txtTourFromDate.Text, txtTourToDate.Text, Txt_ToursCode.Text)

                '//ID:119 modified by abin on 20180925 ========= Satrt
                If dsDates.Tables Is Nothing Then
                    MessageBox.ShowMessage(Page, MessageType.Warning, "Tour is not available.")
                    Exit Sub
                Else
                    If dsDates.Tables(1).Rows.Count = 0 Then
                        MessageBox.ShowMessage(Page, MessageType.Warning, "Tour is not available.")
                        Exit Sub
                    End If

                End If
                '//ID:119 modified by abin on 20180925 ========= End

                hdTourRateBasis.Value = dsTourFreeFormBooking.Tables(0).Rows(0)("ratebasis").ToString

                If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                    divUnits.Style.Item("display") = "none"
                    divAdult.Style.Item("display") = "block"

                    divCostUnits.Style.Item("display") = "none"
                    divCostAdult.Style.Item("display") = "block"

                    'divClearAdult.Style.Item("display") = "block"
                    'divChild.Style.Item("display") = "block"
                    'divSenior.Style.Item("display") = "block"
                    'divChildAsAdult.Style.Item("display") = "block"

                Else
                    divUnits.Style.Item("display") = "block"
                    divCostUnits.Style.Item("display") = "block"
                    'Txt_NoOfUnits.Text = ddlTourAdult.SelectedValue.ToString()
                    'Txt_NoOfUnits.Text = 1
                End If

                'Dim fromAge As Decimal = dsTourFreeFormBooking.Tables(1).Rows(0)("chidlagefrom").ToString()
                'Dim toAge As Decimal = dsTourFreeFormBooking.Tables(1).Rows(0)("childageto").ToString()

                Dim fromAge As Decimal = dsDates.Tables(1).Rows(0)("chidlagefrom").ToString()
                Dim toAge As Decimal = dsDates.Tables(1).Rows(0)("childageto").ToString()
                'If strChild1 <> "" Then
                '    If CType(strChild1, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                'If strChild2 <> "" Then
                '    If CType(strChild2, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                'If strChild3 <> "" Then
                '    If CType(strChild3, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                'If strChild4 <> "" Then
                '    If CType(strChild4, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                'If strChild5 <> "" Then
                '    If CType(strChild5, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                'If strChild6 <> "" Then
                '    If CType(strChild6, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                'If strChild7 <> "" Then
                '    If CType(strChild7, Integer) > toAge Then
                '        intChAsAdult = intChAsAdult + 1
                '    End If
                'End If
                Txt_NoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                Txt_NoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()
                Txt_NoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                Txt_NoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                Txt_NoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()



                'Txt_CostNoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                'Txt_CostNoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()

                'Txt_CostNoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                'Txt_CostNoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                'Txt_CostNoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()

                Txt_PriceSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorprice").ToString()
                Txt_PriceAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultprice").ToString()
                Txt_PriceChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childprice").ToString()
                Txt_PriceChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultprice").ToString()
                Txt_PriceUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitprice").ToString()

                Txt_SaleValueSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorsalevalue").ToString()
                Txt_SaleValueAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultsalevalue").ToString()
                Txt_SaleValueChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childsalevalue").ToString()
                Txt_SaleValueChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultvalue").ToString()
                Txt_SaleValueUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitsalevalue").ToString()

                Txt_TotalSaleValue.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("totalsalevalue").ToString()

                Txt_CostNoOfAdults.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adults").ToString()
                Txt_CostNoOfSeniors.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("senior").ToString()
                Txt_CostNoOfChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("child").ToString()
                Txt_CostNoOfChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadult").ToString()
                Txt_CostNoOfUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("units").ToString()



                Txt_CostPriceSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorcprice").ToString()
                Txt_CostPriceAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultcprice").ToString()
                Txt_CostPriceChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childcprice").ToString()
                Txt_CostPriceChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultcprice").ToString()
                Txt_CostPriceUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitcprice").ToString()

                Txt_CostSaleValueSenior.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("seniorcostvalue").ToString()
                Txt_CostSaleValueAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("adultcostvalue").ToString()
                Txt_CostSaleValueChild.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childcostvalue").ToString()
                Txt_CostSaleValueChildAsAdult.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("childasadultcostvalue").ToString()
                Txt_CostSaleValueUnits.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("unitcostvalue").ToString()

                Txt_TotalCostValue.Text = dsTourFreeFormBooking.Tables(0).Rows(0)("totalcostvalue").ToString()



                Lbl_CurCodeAdult.Text = hdCurrCode.Value
                Lbl_CurCodeChAdult.Text = hdCurrCode.Value
                Lbl_CurCodeChild.Text = hdCurrCode.Value
                Lbl_CurCodeUnit.Text = hdCurrCode.Value
                Lbl_CurCodeSenior.Text = hdCurrCode.Value

                divComplimentary.Style.Item("display") = "block"
                divSelectNormal.Style.Item("display") = "block"

                btnSaveComboExcursion.Visible = True

                divTotalSale.Style.Item("display") = "block"
                divpsenior.Style.Item("display") = "block"
                divpTotal.Style.Item("display") = "block"

                divTotalCost.Style.Item("display") = "block"
                divCostArea.Style.Item("display") = "block"
                divpCostTotal.Style.Item("display") = "block"

                If dsDates.Tables(1).Rows(0)("ratebasis").ToString = "ACS" Then
                    If Val(Txt_NoOfChild.Text.Trim()) > 0 Then
                        divChild.Style.Item("display") = "block"

                        divCostChild.Style.Item("display") = "block"

                    End If
                    If Val(Txt_NoOfSeniors.Text.Trim()) > 0 Then
                        divSenior.Style.Item("display") = "block"
                        divCostSenior.Style.Item("display") = "block"
                    End If
                    If Val(Txt_NoOfChildAsAdult.Text.Trim()) > 0 Then
                        divChildAsAdult.Style.Item("display") = "block"
                        divCostChildAsAdult.Style.Item("display") = "block"
                    End If
                Else
                    Txt_CostNoOfUnits.Text = 1
                End If
            End If



            If Not dsTourFreeFormBooking Is Nothing Then
                If dsTourFreeFormBooking.Tables.Count > 0 Then
                    If dsTourFreeFormBooking.Tables(0).Rows(0)("multicost").ToString = "YES" Then
                        btnMulticost.Visible = True
                        gvMultiCost.DataSource = dsTourFreeFormBooking.Tables(2)
                        gvMultiCost.DataBind()

                        Session("sdtMultiCost") = dsTourFreeFormBooking.Tables(2)


                    End If
                End If

            End If




            'Dim dtMultiCost As DataTable
            'dtMultiCost = objclsUtilities.GetDataFromDataTable("exec sp_booking_muticost_price '" + txtTourCustomerCode.Text + "','" + strTourDate + "','" + txtTourStartingFromCode.Text + "','" + ddlTourAdult.SelectedValue + "','" + ddlTourChildren.SelectedValue + "','','" + ddlSeniorCitizen.SelectedValue + "','" + txtTourSourceCountryCode.Text + "','" + Txt_ToursCode.Text + "' ")
            'If dtMultiCost.Rows.Count > 0 Then
            '    gvMultiCost.DataSource = dtMultiCost
            '    gvMultiCost.DataBind()

            '    Dim dMTotal As Decimal = 0
            '    For i As Integer = 0 To dtMultiCost.Rows.Count - 1
            '        dMTotal = dMTotal + dtMultiCost.Rows(i)("totalcost").ToString
            '    Next
            '    Txt_CostPriceUnits.Text = dMTotal.ToString
            '    Txt_CostSaleValueUnits.Text = dMTotal.ToString
            '    Txt_TotalCostValue.Text = dMTotal.ToString
            'Else
            '    gvMultiCost.DataBind()
            '    MessageBox.ShowMessage(Page, MessageType.Warning, "Multicost rate is not available. Please inform admin to enter multicost or Enter as single as cost.")

            'End If

            'Session("sdtMultiCost") = dtMultiCost



            ''changed by mohamed on 12/02/2018
            'txtSearchTour.Text = ""
            'txtSearchTour.Enabled = True
            'btnTourTextSearch.Enabled = True
            'ddlSorting.Enabled = True

            'CreateSelectedTourDataTable()
            'CreateComboTourDataTable()
            'If Not Session("sRequestId") Is Nothing Then
            '    If (objBLLTourSearch.ValidateTourSearchDateGaps(Session("sRequestId"), txtTourFromDate.Text, txtTourToDate.Text)) = False Then
            '        MessageBox.ShowMessage(Page, MessageType.Warning, "The search date should be in continuity with previous booking date range.")
            '        Exit Sub
            '    End If
            'End If

            'Toursearch()
            'txtSearchFocus.Focus()

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnTourFill_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub
    Private Sub SetChilds(ByVal childages As String)
        Dim ChiChildsAgelds As String()
        If childages.Contains(";") Then
            ChiChildsAgelds = childages.Split(";")
        Else
            ReDim ChiChildsAgelds(1)
            ChiChildsAgelds(0) = childages
        End If

        If ddlTourChildren.SelectedValue = 1 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ""
            txtTourChild3.Text = ""
            txtTourChild4.Text = ""
            txtTourChild5.Text = ""
            txtTourChild6.Text = ""
            txtTourChild7.Text = ""
            txtTourChild8.Text = ""

            Exit Sub
        End If
        If ddlTourChildren.SelectedValue = 2 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ""
            txtTourChild4.Text = ""
            txtTourChild5.Text = ""
            txtTourChild6.Text = ""
            txtTourChild7.Text = ""
            txtTourChild8.Text = ""
            Exit Sub

        End If
        If ddlTourChildren.SelectedValue = 3 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ChiChildsAgelds(2)
            txtTourChild4.Text = ""
            txtTourChild5.Text = ""
            txtTourChild6.Text = ""
            txtTourChild7.Text = ""
            txtTourChild8.Text = ""
            Exit Sub
        End If
        If ddlTourChildren.SelectedValue = 4 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ChiChildsAgelds(2)
            txtTourChild4.Text = ChiChildsAgelds(3)
            txtTourChild5.Text = ""
            txtTourChild6.Text = ""
            txtTourChild7.Text = ""
            txtTourChild8.Text = ""
            Exit Sub
        End If
        If ddlTourChildren.SelectedValue = 5 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ChiChildsAgelds(2)
            txtTourChild4.Text = ChiChildsAgelds(3)
            txtTourChild5.Text = ChiChildsAgelds(4)
            txtTourChild6.Text = ""
            txtTourChild7.Text = ""
            txtTourChild8.Text = ""
        End If
        If ddlTourChildren.SelectedValue = 6 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ChiChildsAgelds(2)
            txtTourChild4.Text = ChiChildsAgelds(3)
            txtTourChild5.Text = ChiChildsAgelds(4)
            txtTourChild6.Text = ChiChildsAgelds(5)
            txtTourChild7.Text = ""
            txtTourChild8.Text = ""
        End If
        If ddlTourChildren.SelectedValue = 7 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ChiChildsAgelds(2)
            txtTourChild4.Text = ChiChildsAgelds(3)
            txtTourChild5.Text = ChiChildsAgelds(4)
            txtTourChild6.Text = ChiChildsAgelds(5)
            txtTourChild7.Text = ChiChildsAgelds(6)
            txtTourChild8.Text = ""
        End If
        If ddlTourChildren.SelectedValue = 8 Then
            txtTourChild1.Text = ChiChildsAgelds(0)
            txtTourChild2.Text = ChiChildsAgelds(1)
            txtTourChild3.Text = ChiChildsAgelds(2)
            txtTourChild4.Text = ChiChildsAgelds(3)
            txtTourChild5.Text = ChiChildsAgelds(4)
            txtTourChild6.Text = ChiChildsAgelds(5)
            txtTourChild7.Text = ChiChildsAgelds(6)
            txtTourChild8.Text = ChiChildsAgelds(7)
        End If
    End Sub



    Private Sub EditHeaderFill()
        Try
            Dim strQuery As String = ""
            Dim chksector As String = ""
            Dim trftype As String = ""
            Dim dt As New DataTable

            Dim strrequestid As String = ""


            objBLLTourSearch = New BLLTourSearch()  'CType(Session("sobjBLLTourSearch"), BLLTourSearch)

            strrequestid = GetExistingRequestId()
            Dim strLineNo As String = Request.QueryString("OLineNo")
            dt = objBLLTourSearch.GetEditBookingDetails(strrequestid, strLineNo)

            If dt.Rows.Count > 0 Then
                txtTourFromDate.Text = dt.Rows(0)("fromdate").ToString
                txtTourToDate.Text = dt.Rows(0)("todate").ToString
                txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                ddlTourAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                txtTourStartingFromCode.Text = dt.Rows(0)("tourstartingfromcode").ToString
                txtTourStartingFrom.Text = dt.Rows(0)("tourstartingfromname").ToString
                ' ''chkTourOveridePrice.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)

                ' ''If chklPrivateOrSIC.Items.Count > 0 Then
                ' ''    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                ' ''        If objBLLTourSearch.PrivateOrSIC.Contains(chklPrivateOrSIC.Items(i).Value) = True Then
                ' ''            chklPrivateOrSIC.Items(i).Selected = True
                ' ''        Else
                ' ''            chklPrivateOrSIC.Items(i).Selected = False
                ' ''        End If
                ' ''    Next
                ' ''End If


                If Val(dt.Rows(0)("senior").ToString) <> 0 Then
                    ddlSeniorCitizen.SelectedValue = dt.Rows(0)("senior").ToString
                End If
                If Val(dt.Rows(0)("child").ToString) <> 0 Then
                    ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString

                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtTourChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtTourChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtTourChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtTourChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtTourChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtTourChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtTourChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtTourChild8.Text = strChildAges(7)
                    End If
                End If
                BindHotelCheckInAndCheckOutHiddenfield()
                ' ''Toursearch()



                If Session("sLoginType") = "RO" Then

                    txtTourSourceCountry.Enabled = False
                    txtTourCustomer.Enabled = False

                End If
                BindComboTourDataTable(strrequestid)

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx ::EditHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub

    Private Sub Amendheaderfill()
        Dim dt As DataTable
        Try

            dt = objBLLTourSearch.GetEditBookingDetails(Session("sEditRequestId"), Request.QueryString("OLineNo"))
            If dt.Rows.Count > 0 Then

                txtTourFromDate.Text = dt.Rows(0)("fromdate").ToString
                txtTourToDate.Text = dt.Rows(0)("todate").ToString
                txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                ddlTourAdult.SelectedValue = Val(dt.Rows(0)("adults").ToString)
                txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString


                txtTourStartingFromCode.Text = dt.Rows(0)("tourstartingfromcode").ToString
                txtTourStartingFrom.Text = dt.Rows(0)("tourstartingfromname").ToString
                ' ''chkTourOveridePrice.Checked = IIf(Val(dt.Rows(0)("overrideprice").ToString) = 1, True, False)



                ' ''If chklPrivateOrSIC.Items.Count > 0 Then
                ' ''    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                ' ''        chklPrivateOrSIC.Items(i).Selected = True
                ' ''    Next
                ' ''End If

                If Val(dt.Rows(0)("senior").ToString) <> 0 Then
                    ddlSeniorCitizen.SelectedValue = dt.Rows(0)("senior").ToString
                End If


                If Val(dt.Rows(0)("child").ToString) <> 0 Then
                    ddlTourChildren.SelectedValue = dt.Rows(0)("child").ToString

                    Dim childages As String = dt.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                    If Left(childages, 1) = ";" Then
                        childages = Right(childages, (childages.Length - 1))
                    End If
                    Dim strChildAges As String() = childages.ToString.Split(";")
                    '''''''

                    If strChildAges.Length <> 0 Then
                        txtTourChild1.Text = strChildAges(0)
                    End If

                    If strChildAges.Length > 1 Then
                        txtTourChild2.Text = strChildAges(1)
                    End If
                    If strChildAges.Length > 2 Then
                        txtTourChild3.Text = strChildAges(2)
                    End If
                    If strChildAges.Length > 3 Then
                        txtTourChild4.Text = strChildAges(3)
                    End If
                    If strChildAges.Length > 4 Then
                        txtTourChild5.Text = strChildAges(4)
                    End If
                    If strChildAges.Length > 5 Then
                        txtTourChild6.Text = strChildAges(5)
                    End If
                    If strChildAges.Length > 6 Then
                        txtTourChild7.Text = strChildAges(6)
                    End If
                    If strChildAges.Length > 7 Then
                        txtTourChild8.Text = strChildAges(7)
                    End If
                End If
                BindHotelCheckInAndCheckOutHiddenfield()
                ' ''Toursearch()



                If Session("sLoginType") = "RO" Then

                    txtTourSourceCountry.Enabled = False
                    txtTourCustomer.Enabled = False

                End If


                BindComboTourDataTable(Session("sEditRequestId"))

            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: AmendHeaderFille :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try

    End Sub

    Private Sub NewHeaderFill()

        Dim strrequestid As String = ""
        Dim strQuery As String = ""
        Dim dt As New DataTable
        Dim dtpax As New DataTable
        Try



            If Not Session("sobjBLLTourSearch") Is Nothing Then
                objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)

                Dim strFromMoreService As String = ""
                If Not Page.Request.UrlReferrer Is Nothing Then
                    Dim previousPage As String = Page.Request.UrlReferrer.ToString
                    If previousPage.Contains("MoreServices.aspx") Then
                        strFromMoreService = "1"
                    End If
                End If

                If strFromMoreService <> "1" Then
                    txtTourFromDate.Text = objBLLTourSearch.FromDate
                    txtTourToDate.Text = objBLLTourSearch.ToDate
                    txtTourStartingFrom.Text = objBLLTourSearch.TourStartingFrom
                    txtTourStartingFromCode.Text = objBLLTourSearch.TourStartingFromCode
                    txtTourClassification.Text = objBLLTourSearch.Classification
                    txtTourClassificationCode.Text = objBLLTourSearch.ClassificationCode
                End If


                txtTourSourceCountry.Text = objBLLTourSearch.SourceCountry
                txtTourSourceCountryCode.Text = objBLLTourSearch.SourceCountryCode
                'ddlStarCategory.SelectedValue = objBLLTourSearch.StarCategoryCode
                txtTourCustomer.Text = objBLLTourSearch.Customer
                txtTourCustomerCode.Text = objBLLTourSearch.CustomerCode

                Dim scriptKey As String = "UniqueKeyForThisScript"
                Dim javaScript As String = "<script type='text/javascript'>CallCheckOutDatePicker();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

                ddlTourAdult.SelectedValue = objBLLTourSearch.Adult
                ddlSeniorCitizen.SelectedValue = objBLLTourSearch.SeniorCitizen
                ddlTourChildren.SelectedValue = objBLLTourSearch.Children
                txtTourChild1.Text = objBLLTourSearch.Child1
                txtTourChild2.Text = objBLLTourSearch.Child2
                txtTourChild3.Text = objBLLTourSearch.Child3
                txtTourChild4.Text = objBLLTourSearch.Child4
                txtTourChild5.Text = objBLLTourSearch.Child5
                txtTourChild6.Text = objBLLTourSearch.Child6
                txtTourChild7.Text = objBLLTourSearch.Child7
                txtTourChild8.Text = objBLLTourSearch.Child8
                If objBLLTourSearch.Children <> "0" Then
                    Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
                    Dim javaScriptChldrn As String = "<script type='text/javascript'>ShowTourChild();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)
                End If


                ' ''If objBLLTourSearch.OverridePrice = "1" Then
                ' ''    chkTourOveridePrice.Checked = True
                ' ''Else
                ' ''    chkTourOveridePrice.Checked = False
                ' ''End If


                ' ''If chklPrivateOrSIC.Items.Count > 0 Then
                ' ''    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                ' ''        If objBLLTourSearch.PrivateOrSIC.Contains(chklPrivateOrSIC.Items(i).Value) = True Then
                ' ''            chklPrivateOrSIC.Items(i).Selected = True
                ' ''        Else
                ' ''            chklPrivateOrSIC.Items(i).Selected = False
                ' ''        End If
                ' ''    Next
                ' ''End If

                objBLLTourSearch.AmendRequestid = GetExistingRequestId()
                objBLLTourSearch.AmendLineno = ViewState("OLineNo")


                If strFromMoreService <> "1" Then
                    'BindSearchResults()
                End If


                BindHotelCheckInAndCheckOutHiddenfield()

                If strFromMoreService = "1" Then
                    BindTourChildAge()
                End If

            Else
                Dim objBLLCommonFuntions = New BLLCommonFuntions

                dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(Session("sRequestId"))
                If dt.Rows.Count > 0 Then

                    txtTourFromDate.Text = dt.Rows(0)("mindate_").ToString
                    txtTourToDate.Text = dt.Rows(0)("maxdate_").ToString
                    txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString

                    txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                    txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                    txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString
                    txtTourClassificationCode.Text = ""
                    txtTourClassification.Text = ""

                    'txtTourStartingFromCode.Text = ""
                    'txtTourStartingFrom.Text = ""
                    ' rblPrivateOrSIC.SelectedValue = "PRIVATE"
                    ' ''If chklPrivateOrSIC.Items.Count > 0 Then
                    ' ''    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
                    ' ''        chklPrivateOrSIC.Items(i).Selected = True
                    ' ''    Next
                    ' ''End If


                    '''' Added shahul 07/04/18
                    strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & Session("sRequestId") & "')"
                    dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                    If dtpax.Rows.Count > 0 Then
                        ddlTourAdult.SelectedValue = Val(dtpax.Rows(0)("adults").ToString)

                        If Val(dtpax.Rows(0)("child").ToString) <> 0 Then
                            ddlTourChildren.SelectedValue = dtpax.Rows(0)("child").ToString

                            Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                            If Left(childages, 1) = ";" Then
                                childages = Right(childages, (childages.Length - 1))
                            End If
                            Dim strChildAges As String() = childages.ToString.Split(";")
                            '''''''

                            If strChildAges.Length <> 0 Then
                                txtTourChild1.Text = strChildAges(0)
                            End If

                            If strChildAges.Length > 1 Then
                                txtTourChild2.Text = strChildAges(1)
                            End If
                            If strChildAges.Length > 2 Then
                                txtTourChild3.Text = strChildAges(2)
                            End If
                            If strChildAges.Length > 3 Then
                                txtTourChild4.Text = strChildAges(3)
                            End If
                            If strChildAges.Length > 4 Then
                                txtTourChild5.Text = strChildAges(4)
                            End If
                            If strChildAges.Length > 5 Then
                                txtTourChild6.Text = strChildAges(5)
                            End If
                            If strChildAges.Length > 6 Then
                                txtTourChild7.Text = strChildAges(6)
                            End If
                            If strChildAges.Length > 7 Then
                                txtTourChild8.Text = strChildAges(7)
                            End If
                        End If

                    End If




                End If
            End If
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: NewHeaderFill :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub


    Public Sub LoadLogo()
        Dim strLogo As String = ""
        If Not Session("sLogo") Is Nothing Then
            strLogo = Session("sLogo")
            If strLogo <> "" Then
                imgLogo.Src = Session("sLogo")

            End If

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
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
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
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
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
                dvMybooking.Visible = False
            End If
        End If
    End Sub

    Private Sub CreateSelectedTourDataTable()
        'Session("selectedtourdatatable") = Nothing
        Dim SelectExcDT As DataTable = New DataTable("SelectedExc")
        SelectExcDT.Columns.Add("exctypcode", Type.GetType("System.String"))
        SelectExcDT.Columns.Add("excdate", Type.GetType("System.DateTime"))
        SelectExcDT.Columns.Add("vehiclecode", Type.GetType("System.String"))
        Session("selectedtourdatatable") = SelectExcDT
    End Sub
    Private Sub CreateComboTourDataTable()

        Dim SelectComboDT As DataTable = New DataTable("SelectedExc")
        SelectComboDT.Columns.Add("exctypcode", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("vehiclecode", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("excdate", Type.GetType("System.DateTime"))
        SelectComboDT.Columns.Add("exctypcombocode", Type.GetType("System.String"))
        SelectComboDT.Columns.Add("type", Type.GetType("System.String"))
        Session("selectedCombotourdatatable") = SelectComboDT
    End Sub
    Private Sub FillTourPickupLocation()
        If Not Session("sRequestId") Is Nothing Then
            Dim dt As DataTable
            dt = objBLLTourSearch.FillTourPickupLocation(Session("sRequestId"))
            If dt.Rows.Count = 1 Then
                txtTourStartingFrom.Text = dt.Rows(0)("othtypname").ToString
                txtTourStartingFromCode.Text = dt.Rows(0)("othtypcode").ToString
            End If
        End If

    End Sub
    ''*** Need But check====================================================================================================================
    Private Sub LoadRoomAdultChild()
        Dim strRequestId As String = ""
        If Not Session("sRequestId") Is Nothing Then
            strRequestId = Session("sRequestId")
            Dim dtDetails As DataTable
            dtDetails = objBLLCommonFuntions.GetRoomAdultAndChildDetails(strRequestId)
            If dtDetails.Rows.Count > 0 Then
                FillSpecifiedAdultChild(dtDetails.Rows(0)("adults").ToString, dtDetails.Rows(0)("child").ToString)
                If dtDetails.Rows(0)("child").ToString > 0 Then
                    ''' Added 01/06/17 shahul
                    Dim childages As String = dtDetails.Rows(0)("childages").ToString.Replace(",", ";")
                    ''''
                    FillSpecifiedChildAges(childages)
                    'FillSpecifiedChildAges(dtDetails.Rows(0)("childages").ToString)n
                End If
            Else
                FillSpecifiedAdultChild("1000", "8")
            End If
        Else
            FillSpecifiedAdultChild("1000", "8")
        End If
        ' Above part commented asper Arun request on 04/06/2017. No need to restrict adult and child based on other booking.
        FillSpecifiedAdultChild("1000", "6")

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


        If Not Session("sLoginType") Is Nothing Then
            hdLoginType.Value = Session("sLoginType")
            If Session("sLoginType") <> "RO" Then
                dvTourCustomer.Visible = False
                'dvTourOveridePrice.Visible = False
                Dim strSqlQry As String = ""
                Dim myDS As New DataSet
                Dim Hotelnames As New List(Of String)
                Try



                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' order by ctryname"

                    Dim SqlConn As New SqlConnection
                    Dim myDataAdapter As New SqlDataAdapter
                    ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
                    SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                    'Open connection
                    myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                    myDataAdapter.Fill(myDS)

                    If myDS.Tables(0).Rows.Count = 1 Then
                        txtTourSourceCountry.Text = myDS.Tables(0).Rows(0)("ctryname").ToString
                        txtTourSourceCountryCode.Text = myDS.Tables(0).Rows(0)("ctrycode").ToString
                        txtTourSourceCountry.ReadOnly = True
                        AutoCompleteExtender_txtTourSourceCountry.Enabled = False
                    Else
                        txtTourSourceCountry.ReadOnly = False
                        AutoCompleteExtender_txtTourSourceCountry.Enabled = True
                    End If


                Catch ex As Exception

                End Try
            Else
                dvTourCustomer.Visible = True
                'dvTourOveridePrice.Visible = True
            End If
        Else
            hdLoginType.Value = ""
        End If

    End Sub
    ' May No Need=================================================================================================================================



    Private Sub BindTourChildAge()
        Dim dtpax As DataTable
        Dim objBLLCommonFuntions = New BLLCommonFuntions
        Dim strRequestId As String = ""
        Dim strQuery As String = ""
        If Not Session("sRequestId") Is Nothing Then
            strRequestId = Session("sRequestId")
            Dim dt As DataTable
            dt = objBLLCommonFuntions.GetBookingTempHeaderDetails(strRequestId)
            If dt.Rows.Count > 0 Then
                hdTab.Value = "1"

                txtTourCustomerCode.Text = dt.Rows(0)("agentcode").ToString
                txtTourCustomer.Text = dt.Rows(0)("agentname").ToString
                txtTourSourceCountry.Text = dt.Rows(0)("sourcectryname").ToString
                txtTourSourceCountryCode.Text = dt.Rows(0)("sourcectrycode").ToString

                strQuery = "select * from dbo.fn_get_adultschild_bookingtemp ('" & strRequestId & "')"
                dtpax = objclsUtilities.GetDataFromDataTable(strQuery)
                If dtpax.Rows.Count > 0 Then
                    ddlTourAdult.SelectedValue = dtpax.Rows(0)("adults").ToString
                    ddlTourChildren.SelectedValue = dtpax.Rows(0)("child").ToString

                    If dtpax.Rows(0)("child").ToString <> "0" Then

                        ''' Added 01/06/17 shahul
                        Dim childages As String = dtpax.Rows(0)("childages").ToString.ToString.Replace(",", ";")
                        If Left(childages, 1) = ";" Then
                            childages = Right(childages, (childages.Length - 1))
                        End If
                        Dim strChildAges As String() = childages.ToString.Split(";")
                        '''''''

                        '  Dim strChildAges As String() = dt.Rows(0)("childages").ToString.Split(";")

                        If strChildAges.Length <> 0 Then
                            txtTourChild1.Text = strChildAges(0)
                        End If

                        If strChildAges.Length > 1 Then
                            txtTourChild2.Text = strChildAges(1)
                        End If
                        If strChildAges.Length > 2 Then
                            txtTourChild3.Text = strChildAges(2)
                        End If
                        If strChildAges.Length > 3 Then
                            txtTourChild4.Text = strChildAges(3)
                        End If
                        If strChildAges.Length > 4 Then
                            txtTourChild5.Text = strChildAges(4)
                        End If
                        If strChildAges.Length > 5 Then
                            txtTourChild6.Text = strChildAges(5)
                        End If
                        If strChildAges.Length > 6 Then
                            txtTourChild7.Text = strChildAges(6)
                        End If
                        If strChildAges.Length > 7 Then
                            txtTourChild8.Text = strChildAges(7)
                        End If
                    End If



                    ' ''If dt.Rows(0)("reqoverride").ToString = "1" Then
                    ' ''    chkTourOveridePrice.Checked = True
                    ' ''Else
                    ' ''    chkTourOveridePrice.Checked = False
                    ' ''End If

                End If
            End If
        End If


    End Sub
    Private Sub BindHotelCheckInAndCheckOutHiddenfield()
        Dim objBLLHotelSearch = New BLLHotelSearch
        If Not Session("sRequestId") Is Nothing Then
            Dim strRequestId = GetExistingRequestId()
            Dim dt As DataTable
            dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(strRequestId)
            If dt.Rows.Count > 0 Then
                hdCheckInPrevDay.Value = dt.Rows(0)("CheckInPrevDay").ToString
                'hdCheckInNextDay.Value = dt.Rows(0)("CheckInNextDay").ToString
                ' hdCheckOutPrevDay.Value = dt.Rows(0)("CheckOutPrevDay").ToString
                hdCheckOutNextDay.Value = dt.Rows(0)("CheckOutNextDay").ToString

                txtTourFromDate.Text = dt.Rows(0)("CheckIn").ToString
                txtTourToDate.Text = dt.Rows(0)("CheckOut").ToString


                If Session("sLoginType") = "RO" Then
                    'txtTourCustomerCode.Text = objBLLHotelSearch.CustomerCode
                    'txtTourCustomer.Text = objBLLHotelSearch.Customer

                    'txtTourSourceCountry.Text = objBLLHotelSearch.SourceCountry
                    'txtTourSourceCountryCode.Text = objBLLHotelSearch.SourceCountryCode

                End If

            Else
                hdCheckInPrevDay.Value = "0"
                hdCheckOutNextDay.Value = "0"
            End If

        Else
            hdCheckInPrevDay.Value = "0"
            hdCheckOutNextDay.Value = "0"

        End If

        Dim dt1 As DataTable

        Dim strRequestId1 As String = ""
        If Not Session("sRequestId") Is Nothing Then
            ' objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
            strRequestId1 = Session("sRequestId")
            dt1 = objBLLHotelSearch.GetSectorCheckInAndCheckOutDetails(strRequestId1, txtTourStartingFromCode.Text)
            Dim strFlag As String = ""
            If dt1.Rows.Count > 0 Then
                If dt1.Rows(0)("CheckIn").ToString = "" Then
                    hdChangeFromdate.Value = txtTourFromDate.Text
                    strFlag = "1"
                Else
                    hdChangeFromdate.Value = dt1.Rows(0)("CheckIn").ToString
                End If
                If dt1.Rows(0)("CheckOut").ToString = "" Then
                    hdChangeTodate.Value = txtTourToDate.Text
                Else
                    hdChangeTodate.Value = dt1.Rows(0)("CheckOut").ToString
                End If

            Else
                Dim dt2 As DataTable
                strRequestId1 = Session("sRequestId")
                dt2 = objBLLTourSearch.GetTourCheckInAndCheckOutDetails(strRequestId1, txtTourStartingFromCode.Text)
                If dt2.Rows.Count > 0 Then
                    hdChangeFromdate.Value = dt2.Rows(0)("searchfromdate").ToString
                    hdChangeTodate.Value = dt2.Rows(0)("searchtodate").ToString
                    'hdChangeFromdate.Value = txtTourFromDate.Text
                Else
                    hdChangeFromdate.Value = txtTourFromDate.Text
                    hdChangeTodate.Value = txtTourToDate.Text
                End If

            End If
            If strFlag = "1" Then
                Dim dt2 As DataTable
                strRequestId1 = Session("sRequestId")
                dt2 = objBLLTourSearch.GetTourCheckInAndCheckOutDetails(strRequestId1, txtTourStartingFromCode.Text)
                If dt2.Rows.Count > 0 Then
                    hdChangeFromdate.Value = dt2.Rows(0)("searchfromdate").ToString
                    hdChangeTodate.Value = dt2.Rows(0)("searchtodate").ToString
                    'hdCheckInPrevDay.Value = dt2.Rows(0)("searchfromdate").ToString
                    'hdCheckOutNextDay.Value = dt2.Rows(0)("searchtodate").ToString
                    txtTourFromDate.Text = hdChangeFromdate.Value
                    txtTourToDate.Text = hdChangeTodate.Value
                Else
                    hdChangeFromdate.Value = txtTourFromDate.Text
                    hdChangeTodate.Value = txtTourToDate.Text
                End If
            End If

        Else

            hdChangeFromdate.Value = txtTourFromDate.Text
            hdChangeTodate.Value = txtTourToDate.Text

            Dim scriptKeyChldrn As String = "UniqueKeyForThisScript1"
            Dim javaScriptChldrn As String = "<script type='text/javascript'>CallToDatePicker();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), scriptKeyChldrn, javaScriptChldrn)



        End If

        tourfromdate = hdChangeFromdate.Value
        tourtodate = hdChangeTodate.Value
    End Sub


    ' ''Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
    ' ''    Try
    ' ''        Dim dsTourSearchResults As New DataSet
    ' ''        dsTourSearchResults = Session("sDSTourSearchResults")
    ' ''        BindTourMainDetailsWithFilter(dsTourSearchResults)

    ' ''    Catch ex As Exception
    ' ''        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    ' ''        objclsUtilities.WriteErrorLog("TourSearch.aspx :: Filter :: " & ex.Message & ":: " & Session("GlobalUserName"))
    ' ''        ModalPopupDays.Hide()
    ' ''    End Try
    ' ''End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete


        'Dim strScript As String = "javascript: HideProgess();"
        'ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", strScript, True)


    End Sub

    ' ''Protected Sub ddlSorting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSorting.SelectedIndexChanged
    ' ''    Try
    ' ''        Dim dsTourSearchResults As New DataSet
    ' ''        dsTourSearchResults = Session("sDSTourSearchResults")
    ' ''        BindTourMainDetailsWithFilter(dsTourSearchResults)

    ' ''    Catch ex As Exception
    ' ''        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    ' ''        objclsUtilities.WriteErrorLog("TourSearch.aspx :: ddlSorting_SelectedIndexChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
    ' ''        ModalPopupDays.Hide()
    ' ''    End Try
    ' ''End Sub

    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dsTourSearchResults As New DataSet
            dsTourSearchResults = Session("sDSTourSearchResults")
            Dim pageIndex As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
            Session("sTourPageIndex") = pageIndex.ToString
            BindTourMainDetailsWithFilter(dsTourSearchResults)

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: Page_Changed :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    Private Sub BindTourMainDetailsWithFilter(ByVal dsSearchResults As DataSet)

        If dsSearchResults.Tables.Count > 0 Then
            If dsSearchResults.Tables(0).Rows.Count > 0 Then
                'changed by mohamed on 12/02/2018
                Dim dtMainDetails1 As DataTable = dsSearchResults.Tables(0).Copy
                Dim dtMainDetailsRet As DataTable, dtMainDetailsMiddle As DataTable

                dtMainDetails1.Columns.Add("CustomSortHelp", Type.GetType("System.Int64"), "2")
                Dim dvMaiDetails As DataView = New DataView(dtMainDetails1) ' New DataView(dsSearchResults.Tables(0))


                ' Filter HotelStars *****************
                Dim strNotSelectedHotelStar As String = ""
                'If chkHotelStars.Items.Count > 0 Then
                '    For Each chkitem As ListItem In chkHotelStars.Items
                '        If chkitem.Selected = False Then
                '            If strNotSelectedHotelStar = "" Then
                '                strNotSelectedHotelStar = "'" & chkitem.Value & "'"
                '            Else
                '                strNotSelectedHotelStar = strNotSelectedHotelStar & "," & "'" & chkitem.Value & "'"
                '            End If

                '        End If
                '    Next
                'End If

                Dim strNotSelectedClassification As String = ""
                'If chkRoomClassification.Items.Count > 0 Then
                '    For Each chkitem As ListItem In chkRoomClassification.Items
                '        If chkitem.Selected = False Then
                '            If strNotSelectedClassification = "" Then
                '                strNotSelectedClassification = "'" & chkitem.Value & "'"
                '            Else
                '                strNotSelectedClassification = strNotSelectedClassification & "," & "'" & chkitem.Value & "'"
                '            End If

                '        End If
                '    Next
                'End If


                ' Filter for Price *****************
                Dim strFilterCriteria As String = ""
                If strNotSelectedHotelStar <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "starcategory NOT IN (" & strNotSelectedHotelStar & ")"
                    Else
                        strFilterCriteria = " starcategory NOT IN (" & strNotSelectedHotelStar & ")"
                    End If
                End If

                If strNotSelectedClassification <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "classificationcode NOT IN (" & strNotSelectedClassification & ")"
                    Else
                        strFilterCriteria = " classificationcode NOT IN (" & strNotSelectedClassification & ")"
                    End If
                End If


                If hdPriceMin.Value <> "" And hdPriceMax.Value <> "" Then
                    If strFilterCriteria <> "" Then
                        strFilterCriteria = strFilterCriteria & " AND " & "totalsalevalue >=" & hdPriceMin.Value & " AND totalsalevalue <=" & hdPriceMax.Value
                    Else
                        strFilterCriteria = "totalsalevalue >=" & hdPriceMin.Value & " AND totalsalevalue <=" & hdPriceMax.Value
                    End If
                End If

                Dim strFilterCriteriaSearchTour As String = ""
                Dim lsTourSearchOrder As String = ""
                'If txtSearchTour.Text <> "" Then
                '    lsTourSearchOrder = "CustomSortHelp, "
                '    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " excname like ('" & txtSearchTour.Text & "%')"
                'End If
                'changed by mohamed on 12/02/2018
                If strFilterCriteria & strFilterCriteriaSearchTour <> "" Then
                    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                End If

                'changed by mohamed on 12/02/2018
                'Search Text in Middle
                'If txtSearchTour.Text <> "" Then
                '    dtMainDetailsRet = dvMaiDetails.ToTable.Copy
                '    strFilterCriteriaSearchTour = IIf(strFilterCriteria = "", "", " AND ") & " excname like ('%" & txtSearchTour.Text & "%') and excname not like ('" & txtSearchTour.Text & "%')"
                '    dvMaiDetails.RowFilter = strFilterCriteria & strFilterCriteriaSearchTour
                '    dtMainDetailsMiddle = dvMaiDetails.ToTable.Copy
                '    dtMainDetailsMiddle.Columns("CustomSortHelp").Expression = "3"
                '    dtMainDetailsRet.Merge(dtMainDetailsMiddle)
                '    'dvMaiDetails = Nothing
                '    dvMaiDetails = New DataView(dtMainDetailsRet)
                'End If

                'changed by mohamed on 12/02/2018
                'If ddlSorting.Text = "Name" Then
                '    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & " excname ASC"
                'ElseIf ddlSorting.Text = "Price" Then
                '    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & " totalsalevalue ASC"
                'ElseIf ddlSorting.Text = "Preferred" Then
                '    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & "Preferred  DESC, excname ASC"
                'ElseIf ddlSorting.Text = "Rating" Then
                '    dvMaiDetails.Sort = "tourselected DESC, " & lsTourSearchOrder & " starcategory DESC,excname ASC "
                'End If


                Dim recordCount As Integer = dvMaiDetails.Count

                'BindTourMainDetails(dvMaiDetails)
                Me.PopulatePager(recordCount)

                FillCheckBox()

            End If
        Else
            'dlTourSearchResults.DataBind()
        End If
    End Sub
    Private Sub PopulatePager(ByVal recordCount As Integer)
        Dim currentPage As Integer = 1
        If Not Session("sTourPageIndex") Is Nothing Then
            currentPage = Session("sTourPageIndex")
        End If

        Dim dblPageCount As Double = CDbl(CDec(recordCount) / Convert.ToDecimal(PageSize))
        Dim pageCount As Integer = CInt(Math.Ceiling(dblPageCount))
        Dim pages As New List(Of ListItem)()
        If pageCount > 0 Then
            For i As Integer = 1 To pageCount
                pages.Add(New ListItem(i.ToString(), i.ToString(), i <> currentPage))
            Next
        End If

        'rptPager.DataSource = pages
        'rptPager.DataBind()
    End Sub


    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogOut.Click
        Try
            Dim strAbsoluteUrl As String = Session("sAbsoluteUrl")
            Session.Clear()
            Session.Abandon()
            Response.Redirect(strAbsoluteUrl, True)
        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnLogOut_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try

    End Sub
    ' ''    <System.Web.Script.Services.ScriptMethod()> _
    ' ''<System.Web.Services.WebMethod()> _
    ' ''    Public Shared Function GetClassification(ByVal prefixText As String) As List(Of String)

    ' ''        Dim strSqlQry As String = ""
    ' ''        Dim myDS As New DataSet
    ' ''        Dim Hotelnames As New List(Of String)
    ' ''        Try
    ' ''            If prefixText = " " Then
    ' ''                prefixText = ""
    ' ''            End If
    ' ''            strSqlQry = "select classificationcode,classificationname from excclassification_header where active=1 and classificationname like  '" & prefixText & "%' order by classificationname "
    ' ''            Dim SqlConn As New SqlConnection
    ' ''            Dim myDataAdapter As New SqlDataAdapter
    ' ''            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
    ' ''            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
    ' ''            'Open connection
    ' ''            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
    ' ''            myDataAdapter.Fill(myDS)

    ' ''            If myDS.Tables(0).Rows.Count > 0 Then
    ' ''                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
    ' ''                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("classificationname").ToString(), myDS.Tables(0).Rows(i)("classificationcode").ToString()))
    ' ''                    'Hotelnames.Add(myDS.Tables(0).Rows(i)("partyname").ToString() & "<span style='display:none'>" & i & "</span>")
    ' ''                Next

    ' ''            End If

    ' ''            Return Hotelnames
    ' ''        Catch ex As Exception
    ' ''            Return Hotelnames
    ' ''        End Try

    ' ''    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetTourStartingFrom(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim TourStartingFrom As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            strSqlQry = ""
            Dim objBLLHotelSearch = New BLLHotelSearch
            'If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
            If Not HttpContext.Current.Session("sRequestid") Is Nothing Then
                ' objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")
                Dim dt As DataTable

                dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(HttpContext.Current.Session("sRequestid"))
                If dt.Rows.Count > 0 Then
                    strSqlQry = "select distinct othtypcode,othtypname  from  othtypmast o,view_booking_hotel_prearr d,partymast p,sectormaster s where o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and d.requestid='" & HttpContext.Current.Session("sRequestid") & "' and  o.othtypname  like '" & LTrim(prefixText) & "%' order by o.othtypname "
                Else
                    strSqlQry = "select othtypcode,othtypname  from  othtypmast where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001)  and  othtypname  like '" & LTrim(prefixText) & "%'  order by othtypname "
                End If
            Else
                strSqlQry = "select othtypcode,othtypname  from  othtypmast where active=1 and othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and  othtypname  like '" & LTrim(prefixText) & "%' order by othtypname "
            End If


            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    TourStartingFrom.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("othtypname").ToString(), myDS.Tables(0).Rows(i)("othtypcode").ToString()))
                Next

            End If

            Return TourStartingFrom
        Catch ex As Exception
            Return TourStartingFrom
        End Try

    End Function

    ''' <summary>
    ''' GetHotelsDetails
    ''' </summary>
    ''' <param name="HotelCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetHotelsDetails(ByVal exctypcode As String) As String
        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try

            'strSqlQry = "select p.sectorcode,s.sectorname,c.catcode,c.catname from partymast(nolock) p,sectormaster(nolock) s,catmast(nolock) c where p.sectorcode=s.sectorcode and p.active=1 and p.sptypecode='HOT' and p.catcode=c.catcode and p.partycode= '" & HotelCode & "' "
            strSqlQry = "SELECT A.exctypcode,A.exctypname,A.classificationcode,B.classificationname,A.starcat,A.sicpvt,(CASE WHEN (combo ='NO' AND multipledatesyesno='NO') THEN 'NORMAL' WHEN (combo ='YES' AND multipledatesyesno='NO') THEN 'COMBO' WHEN (combo ='NO' AND multipledatesyesno='YES') THEN 'MULTIPLE DATE' END) exctyp       FROM view_excursiontypes A INNER JOIN excclassification_header B ON  A.classificationcode=B.classificationcode  WHERE exctypcode = '" & exctypcode & "' "
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
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetTourCategory(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        '*** 1) contextKey will contain all selected TextBox values and its stored procedure's Parameter name 
        '*** 2) each values are separated with "|" charested
        '*** 3) Value and parametername are separated with "@" charecter
        '*** 4) the first value is a count to indiacte current textbox
        '*** 5) Check function SetContextKey(ch) in aspx page 

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim TourCategory As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If

            Dim objBLLHotelSearch = New BLLHotelSearch
            If Not HttpContext.Current.Session("sRequestid") Is Nothing Then
                Dim dt As DataTable
                dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(HttpContext.Current.Session("sRequestid"))
                ' ''If dt.Rows.Count > 0 Then
                ' ''    strSqlQry = "SELECT sicpvt othtypcode,sicpvt othtypname FROM excursiontypes WHERE active=1 AND sicpvt like '" & LTrim(prefixText) & "%'  GROUP BY sicpvt  order by othtypname "
                ' ''Else
                ' ''    strSqlQry = "SELECT sicpvt othtypcode,sicpvt othtypname FROM excursiontypes WHERE active=1 AND sicpvt like '" & LTrim(prefixText) & "%' GROUP BY sicpvt  order by othtypname"
                ' ''End If
            Else
                ' ''strSqlQry = "SELECT sicpvt othtypcode,sicpvt othtypname FROM excursiontypes WHERE active=1 AND sicpvt like  '" & LTrim(prefixText) & "%' GROUP BY sicpvt  order by othtypname "
            End If

            Dim dtResult = New DataTable
            Dim parms As New List(Of SqlParameter)
            Dim parm As SqlParameter

            '*** Spliting value and Stored Procedure parametername and create Parameter
            Dim strContextVal As String() = contextKey.Split("|")
            Dim CurrControlCnt = strContextVal(0).ToString().Substring(0, strContextVal(0).ToString().IndexOf("@"))
            For i As Integer = 1 To strContextVal.Count - 1
                If strContextVal(i).ToString().Length > 0 AndAlso strContextVal(i).ToString().Contains("@") = True Then
                    If strContextVal(i).ToString().Substring(0, strContextVal(i).ToString().IndexOf("@")).ToString().Trim().Length > 0 Then
                        parm = New SqlParameter(strContextVal(i).ToString().Substring(strContextVal(i).ToString().IndexOf("@")), CType(strContextVal(i).ToString().Substring(0, strContextVal(i).ToString().IndexOf("@")).ToString.Trim, String))
                        parms.Add(parm)
                    End If
                End If

            Next
            If prefixText.Trim.Length > 0 Then

                parm = New SqlParameter(strContextVal(CurrControlCnt).ToString().Substring(strContextVal(CurrControlCnt).ToString().IndexOf("@")), CType(prefixText.ToString.Trim, String))
                parms.Add(parm)
            End If


            Using sqlCn As New SqlConnection(clsDBConnect.ConnectionString())
                Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                    With sqlCmd
                        If (CurrControlCnt = 1) Then
                            .CommandText = "SP_SelectTourFreeFormClassification"

                            'ElseIf (CurrControlCnt = 2) Then
                            '    .CommandText = "SP_SelectTourCategory"

                        ElseIf (CurrControlCnt = 3) Then
                            .CommandText = "SP_SelectTourCategory"

                        ElseIf (CurrControlCnt = 4) Then
                            .CommandText = "SP_SelectTourFreeFormValues"

                        ElseIf (CurrControlCnt = 5) Then
                            .CommandText = "SP_SelectTourFreeFormType"
                        End If

                        .CommandType = CommandType.StoredProcedure
                        .Connection = sqlCn
                        .CommandTimeout = 0
                        For i = 0 To parms.Count - 1
                            If (parms(i) IsNot Nothing) Then .Parameters.Add(parms(i))
                        Next

                    End With
                    Using sqlDa As New SqlDataAdapter(sqlCmd)
                        sqlDa.Fill(myDS)
                    End Using
                End Using
            End Using

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    If (CurrControlCnt = 3 Or CurrControlCnt = 5) Then
                        TourCategory.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)(0).ToString(), myDS.Tables(0).Rows(i)(0).ToString()))
                    Else
                        TourCategory.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)(1).ToString(), myDS.Tables(0).Rows(i)(0).ToString()))
                    End If
                Next

            End If

            Return TourCategory
        Catch ex As Exception
            Return TourCategory
        End Try

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
   <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountry(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If Not HttpContext.Current.Session("sLoginType") Is Nothing Then
                If HttpContext.Current.Session("sLoginType") = "RO" Then
                    If contextKey <> "" Then
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & contextKey.Trim & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    Else
                        strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"
                    End If
                Else
                    strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"

                End If


            Else
                strSqlQry = "select a.ctrycode,c.ctryname from agentmast_countries a,ctrymast c where a.ctrycode=c.ctrycode and a.agentcode= '" & HttpContext.Current.Session("sAgentCode") & "' and ctryname like  '" & prefixText & "%'  order by ctryname"


            End If

            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("ctryname").ToString(), myDS.Tables(0).Rows(i)("ctrycode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function

    ' ''   <System.Web.Script.Services.ScriptMethod()> _
    ' ''<System.Web.Services.WebMethod()> _
    ' ''   Public Shared Function GetTours(ByVal prefixText As String) As List(Of String)

    ' ''       Dim strSqlQry As String = ""
    ' ''       Dim myDS As New DataSet
    ' ''       Dim TourNames As New List(Of String)
    ' ''       Try
    ' ''           If prefixText = " " Then
    ' ''               prefixText = ""
    ' ''           End If
    ' ''           strSqlQry = ""
    ' ''           Dim objBLLHotelSearch = New BLLHotelSearch
    ' ''           'If Not HttpContext.Current.Session("sobjBLLHotelSearchActive") Is Nothing Then
    ' ''           If Not HttpContext.Current.Session("sRequestid") Is Nothing Then
    ' ''               ' objBLLHotelSearch = HttpContext.Current.Session("sobjBLLHotelSearchActive")
    ' ''               Dim dt As DataTable

    ' ''               dt = objBLLHotelSearch.GetCheckInAndCheckOutDetails(HttpContext.Current.Session("sRequestid"))
    ' ''               If dt.Rows.Count > 0 Then
    ' ''                   strSqlQry = "SELECT exctypcode, exctypname FROM excursiontypes WHERE active=1 AND exctypname LIKE '" & LTrim(prefixText) & "%'  GROUP BY exctypcode,exctypname order by exctypname "
    ' ''               Else
    ' ''                   strSqlQry = "SELECT exctypcode, exctypname FROM excursiontypes WHERE active=1 AND exctypname LIKE '" & LTrim(prefixText) & "%'  GROUP BY exctypcode,exctypname order by exctypname "
    ' ''               End If
    ' ''           Else
    ' ''               strSqlQry = "SELECT exctypcode, exctypname FROM excursiontypes WHERE active=1 AND exctypname LIKE '" & LTrim(prefixText) & "%' GROUP BY exctypcode,exctypname order by exctypname "
    ' ''           End If


    ' ''           Dim SqlConn As New SqlConnection
    ' ''           Dim myDataAdapter As New SqlDataAdapter
    ' ''           SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
    ' ''           'Open connection
    ' ''           myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
    ' ''           myDataAdapter.Fill(myDS)

    ' ''           If myDS.Tables(0).Rows.Count > 0 Then
    ' ''               For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
    ' ''                   TourNames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("exctypname").ToString(), myDS.Tables(0).Rows(i)("exctypcode").ToString()))
    ' ''               Next

    ' ''           End If

    ' ''           Return TourNames
    ' ''       Catch ex As Exception
    ' ''           Return TourNames
    ' ''       End Try

    ' ''   End Function


    Protected Sub lbReadMore_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim myLinkButton As LinkButton = CType(sender, LinkButton)
            Dim dlItem As DataListItem = CType((myLinkButton).NamingContainer, DataListItem)
            Dim lblExcText As Label = CType(dlItem.FindControl("lblExcText"), Label)
            Dim strText As String = lblExcText.Text
            Dim strToolTip As String = lblExcText.ToolTip
            If myLinkButton.Text = "Read More." Then
                lblExcText.Text = strToolTip
                lblExcText.ToolTip = strText
                myLinkButton.Text = "Read Less."
            Else
                lblExcText.Text = strToolTip
                lblExcText.ToolTip = strText
                myLinkButton.Text = "Read More."
            End If

        Catch ex As Exception
            MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
            objclsUtilities.WriteErrorLog("TourSearch.aspx :: lbReadMore_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
            ModalPopupDays.Hide()
        End Try
    End Sub

    ' ''Private Sub BindSearchResults()
    ' ''    If Not Session("sLoginType") Is Nothing Then
    ' ''        If Session("sLoginType") <> "RO" Then
    ' ''            Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
    ' ''            Dim objBLLHotelSearch As New BLLHotelSearch
    ' ''            iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
    ' ''            hdBookingEngineRateType.Value = iCumulative.ToString
    ' ''            If hdBookingEngineRateType.Value = "1" Then
    ' ''                'divslideprice.Style.Add("display", "none")
    ' ''            Else
    ' ''                'divslideprice.Style.Add("display", "block")
    ' ''            End If
    ' ''        End If
    ' ''    End If

    ' ''    objBLLTourSearch = New BLLTourSearch
    ' ''    If Session("sobjBLLTourSearch") Is Nothing Then
    ' ''        Response.Redirect("Home.aspx?Tab=1")
    ' ''    End If
    ' ''    objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)

    ' ''    Dim dsTourSearchResults As New DataSet
    ' ''    objBLLTourSearch.DateChange = "0"
    ' ''    dsTourSearchResults = objBLLTourSearch.GetSearchDetails()

    ' ''    If dsTourSearchResults.Tables(0).Rows.Count = 0 Then
    ' ''        dvhotnoshow.Style.Add("display", "block")
    ' ''    Else
    ' ''        dvhotnoshow.Style.Add("display", "none")
    ' ''    End If

    ' ''    Session("sDSTourSearchResults") = dsTourSearchResults
    ' ''    If dsTourSearchResults.Tables.Count > 0 Then

    ' ''        BindTourPricefilter(dsTourSearchResults.Tables(1))
    ' ''        BindTourHotelStars(dsTourSearchResults.Tables(2))
    ' ''        BindTourRoomClassification(dsTourSearchResults.Tables(3))
    ' ''        Session("sDSTourSearchResults") = dsTourSearchResults
    ' ''        Session("sTourPageIndex") = "1"
    ' ''        Dim dvMaiDetails As DataView = New DataView(dsTourSearchResults.Tables(0))
    ' ''        'If ddlSorting.Text = "Name" Then
    ' ''        '    dvMaiDetails.Sort = "excname ASC"
    ' ''        'ElseIf ddlSorting.Text = "Price" Then
    ' ''        '    dvMaiDetails.Sort = "totalsalevalue ASC"
    ' ''        'ElseIf ddlSorting.Text = "Preferred" Then
    ' ''        '    dvMaiDetails.Sort = "Preferred  DESC,excname ASC "
    ' ''        'ElseIf ddlSorting.Text = "Rating" Then
    ' ''        '    dvMaiDetails.Sort = "starcategory DESC,excname ASC "
    ' ''        'End If
    ' ''        Dim recordCount As Integer = dvMaiDetails.Count
    ' ''        BindTourMainDetails(dvMaiDetails)
    ' ''        Me.PopulatePager(recordCount)
    ' ''        'lblHotelCount.Text = dsTourSearchResults.Tables(0).Rows.Count & " Records Found"

    ' ''    Else
    ' ''        hdPriceMinRange.Value = "0"
    ' ''        hdPriceMaxRange.Value = "1"
    ' ''    End If


    ' ''End Sub

    ' ''Private Sub BindTourPricefilter(ByVal dataTable As DataTable)
    ' ''    If dataTable.Rows.Count > 0 Then
    ' ''        hdPriceMinRange.Value = IIf(dataTable.Rows(0)("minprice").ToString = "", "0", dataTable.Rows(0)("minprice").ToString)
    ' ''        hdPriceMaxRange.Value = IIf(dataTable.Rows(0)("maxprice").ToString = "", "0", dataTable.Rows(0)("maxprice").ToString)
    ' ''    Else
    ' ''        hdPriceMinRange.Value = "0"
    ' ''        hdPriceMaxRange.Value = "1"
    ' ''    End If
    ' ''End Sub

    ' ''Private Sub BindTourHotelStars(ByVal dataTable As DataTable)
    ' ''    If dataTable.Rows.Count > 0 Then
    ' ''        chkHotelStars.DataSource = dataTable
    ' ''        chkHotelStars.DataTextField = "catname"
    ' ''        chkHotelStars.DataValueField = "starcategory"
    ' ''        chkHotelStars.DataBind()
    ' ''        If chkHotelStars.Items.Count > 0 Then
    ' ''            For Each chkitem As ListItem In chkHotelStars.Items
    ' ''                chkitem.Selected = True
    ' ''            Next
    ' ''        End If
    ' ''    Else
    ' ''        chkHotelStars.Items.Clear()
    ' ''        chkHotelStars.DataBind()
    ' ''    End If
    ' ''End Sub

    ' ''Private Sub BindTourRoomClassification(ByVal dataTable As DataTable)
    ' ''    If dataTable.Rows.Count > 0 Then
    ' ''        chkRoomClassification.DataSource = dataTable
    ' ''        chkRoomClassification.DataTextField = "classificationname"
    ' ''        chkRoomClassification.DataValueField = "classificationcode"
    ' ''        chkRoomClassification.DataBind()
    ' ''        If chkRoomClassification.Items.Count > 0 Then
    ' ''            For Each chkitem As ListItem In chkRoomClassification.Items
    ' ''                chkitem.Selected = True
    ' ''            Next
    ' ''        End If
    ' ''    Else
    ' ''        chkRoomClassification.Items.Clear()
    ' ''        chkRoomClassification.DataBind()
    ' ''    End If
    ' ''End Sub

    ' ''Private Sub BindTourMainDetails(ByVal dvMaiDetails As DataView)
    ' ''    If Not Session("sLoginType") Is Nothing Then
    ' ''        If Session("sLoginType") <> "RO" Then
    ' ''            Dim strAgentCode As String = HttpContext.Current.Session("sAgentCode").ToString
    ' ''            Dim objBLLHotelSearch As New BLLHotelSearch
    ' ''            iCumulative = objBLLHotelSearch.FindBookingEnginRateType(strAgentCode)
    ' ''            hdBookingEngineRateType.Value = iCumulative.ToString
    ' ''            If hdBookingEngineRateType.Value = "1" Then
    ' ''                divslideprice.Style.Add("display", "none")
    ' ''            Else
    ' ''                divslideprice.Style.Add("display", "block")
    ' ''            End If
    ' ''        End If
    ' ''    End If
    ' ''    Dim dt As New DataTable
    ' ''    dt = dvMaiDetails.ToTable
    ' ''    Dim dv As DataView = dt.DefaultView
    ' ''    If dt.Rows.Count > 0 Then
    ' ''        lblHotelCount.Text = dt.Rows.Count & " Records Found"
    ' ''        Dim iPageIndex As Integer = 1
    ' ''        ' Dim iPageSize As Integer = 0
    ' ''        Dim iRowNoFrom As Integer = 0
    ' ''        Dim iRowNoTo As Integer = 0
    ' ''        If Not Session("sTourPageIndex") Is Nothing Then
    ' ''            iPageIndex = Session("sTourPageIndex")
    ' ''        End If

    ' ''        iRowNoFrom = (iPageIndex - 1) * PageSize + 1
    ' ''        iRowNoTo = (((iPageIndex - 1) * PageSize + 1) + PageSize) - 1
    ' ''        dv.Table.Columns.Add("rowIndex")
    ' ''        For i As Integer = 0 To dv.Count - 1
    ' ''            dv.Item(i)("rowIndex") = (i + 1).ToString
    ' ''        Next


    ' ''        dv.RowFilter = "rowIndex >= " & iRowNoFrom & " AND  rowIndex <=" & iRowNoTo
    ' ''        dvMaiDetails.Sort = "tourselected DESC"
    ' ''        dlTourSearchResults.DataSource = dv
    ' ''        dlTourSearchResults.DataBind()

    ' ''    Else
    ' ''        dlTourSearchResults.DataBind()
    ' ''    End If
    ' ''    Session("sdtTourPriceBreakup") = Nothing


    ' ''    Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
    ' ''    For Each gvRow As DataListItem In dlTourSearchResults.Items
    ' ''        Dim hdnselected As HiddenField = gvRow.FindControl("hdnselected")
    ' ''        Dim chkSelectTour As CheckBox = gvRow.FindControl("chkSelectTour")
    ' ''        Dim txtTourChangeDate As TextBox = gvRow.FindControl("txtTourChangeDate")
    ' ''        Dim hdntourdate As HiddenField = gvRow.FindControl("hdntourdate")
    ' ''        Dim hdExcCode As HiddenField = gvRow.FindControl("hdExcCode")
    ' ''        Dim hdVehicleCode As HiddenField = gvRow.FindControl("hdVehicleCode")
    ' ''        Dim lblunits As LinkButton = gvRow.FindControl("lblunits")
    ' ''        Dim hdncumunits As HiddenField = gvRow.FindControl("hdncumunits")

    ' ''        If hdnselected.Value = 1 Then
    ' ''            chkSelectTour.Checked = True
    ' ''            txtTourChangeDate.Text = Format(CType(hdntourdate.Value, Date), "dd/MM/yyyy")
    ' ''            lblunits.Text = hdncumunits.Value + " Units"
    ' ''            For i = dtselectedtour.Rows.Count - 1 To 0 Step -1
    ' ''                If dtselectedtour.Rows(i)("exctypcode") = hdExcCode.Value.ToString And dtselectedtour.Rows(i)("vehiclecode") = hdVehicleCode.Value.ToString Then
    ' ''                    dtselectedtour.Rows.RemoveAt(i)
    ' ''                End If
    ' ''            Next
    ' ''            dtselectedtour.Rows.Add(hdExcCode.Value.ToString, txtTourChangeDate.Text, hdVehicleCode.Value)
    ' ''        End If

    ' ''    Next


    ' ''    Session("selectedtourdatatable") = dtselectedtour




    ' ''End Sub

    ' ''Protected Sub dlTourSearchResults_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlTourSearchResults.ItemCommand


    ' ''End Sub

    'Protected Sub dlTourSearchResults_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlTourSearchResults.ItemDataBound
    '    Dim lblunits As LinkButton = CType(e.Item.FindControl("lblunits"), LinkButton)
    '    Dim hdncumunits As HiddenField = CType(e.Item.FindControl("hdncumunits"), HiddenField)


    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim lbPrice As LinkButton = CType(e.Item.FindControl("lbPrice"), LinkButton)
    '        Dim hdwlCurrCode As HiddenField = CType(e.Item.FindControl("hdwlCurrCode"), HiddenField)
    '        Dim hdCurrCode As HiddenField = CType(e.Item.FindControl("hdCurrCode"), HiddenField)
    '        lbPrice.Text = lbPrice.Text.Replace(".000", "")
    '        Dim hdMultipleDates As HiddenField = CType(e.Item.FindControl("hdMultipleDates"), HiddenField)
    '        Dim hdtotalsalevalue As HiddenField = CType(e.Item.FindControl("hdtotalsalevalue"), HiddenField)
    '        Dim hdExcCode As HiddenField = CType(e.Item.FindControl("hdExcCode"), HiddenField)
    '        Dim hdVehicleCode As HiddenField = CType(e.Item.FindControl("hdVehicleCode"), HiddenField)
    '        If hdMultipleDates.Value = "YES" Then
    '            If Not Session("selectedCombotourdatatable") Is Nothing Then
    '                Dim dtselectedCombotour As New DataTable

    '                dtselectedCombotour = Session("selectedCombotourdatatable")
    '                Dim strType As String = "MULTI_DATE"

    '                Dim dvComboBreakup As DataView
    '                dvComboBreakup = New DataView(dtselectedCombotour)
    '                dvComboBreakup.RowFilter = "exctypcode= '" & hdExcCode.Value & "'  AND  vehiclecode='" & hdVehicleCode.Value & "'  AND type='" & strType & "' "
    '                Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
    '                If dtComboBreakup.Rows.Count > 1 Then
    '                    lbPrice.Text = hdCurrCodePopup.Value & " " & Math.Round(Val(hdtotalsalevalue.Value) * dtComboBreakup.Rows.Count, 2).ToString
    '                End If
    '            End If

    '        End If





    '        Dim lbwlPrice As LinkButton = CType(e.Item.FindControl("lbwlPrice"), LinkButton)
    '        Dim dWlPrice As Double = IIf(lbwlPrice.Text = "", 0, lbwlPrice.Text)
    '        lbwlPrice.Text = Math.Round(dWlPrice, 2, MidpointRounding.AwayFromZero) & " " & hdwlCurrCode.Value
    '        Dim chkSelectTour As CheckBox = CType(e.Item.FindControl("chkSelectTour"), CheckBox)
    '        Dim txtTourChangeDate As TextBox = CType(e.Item.FindControl("txtTourChangeDate"), TextBox)
    '        Dim dvTourdates As HtmlGenericControl = CType(e.Item.FindControl("dvTourdates"), HtmlGenericControl)
    '        Dim lblTourAdultChild As Label = CType(e.Item.FindControl("lblTourAdultChild"), Label)
    '        Dim dvSelectDatelbl As HtmlGenericControl = CType(e.Item.FindControl("dvSelectDatelbl"), HtmlGenericControl)
    '        Dim dvSelectDatelink As HtmlGenericControl = CType(e.Item.FindControl("dvSelectDatelink"), HtmlGenericControl)

    '        Dim hdcombo As HiddenField = CType(e.Item.FindControl("hdcombo"), HiddenField)
    '        'Dim hdMultipleDates As HiddenField = CType(e.Item.FindControl("hdMultipleDates"), HiddenField)

    '        Dim dvTourType As HtmlGenericControl = CType(e.Item.FindControl("dvTourType"), HtmlGenericControl)
    '        Dim lblSicPvt As Label = CType(e.Item.FindControl("lblSicPvt"), Label)
    '        If lblSicPvt.Text.ToUpper = "WITHOUT TRANSFERS TOURS" Then
    '            dvTourType.Attributes.Add("style", "background-color:#EBD255;padding:4px 3px 3px 3px;color:white;font-size:12px;border-color:#EBD255;")
    '        ElseIf lblSicPvt.Text.ToUpper = "SIC (SEAT IN COACH) TOURS" Or lblSicPvt.Text.ToUpper = "SIC(SEAT IN COACH) TOURS" Then
    '            dvTourType.Attributes.Add("style", "background-color:#43C6DB;padding:4px 3px 3px 3px;color:white;font-size:12px;border-color:#43C6DB;")
    '        ElseIf lblSicPvt.Text.ToUpper = "PRIVATE TOURS" Then
    '            dvTourType.Attributes.Add("style", "background-color:#F660AB;padding:4px 3px 3px 3px;color:white;font-size:12px;border-color:#F660AB;")
    '        Else
    '            'dvTourType.Attributes.Add("style", "float:left;width:55%;text-transform:uppercase;")
    '        End If

    '        If hdcombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
    '            dvSelectDatelbl.Attributes.Add("style", "display:none")
    '            dvSelectDatelink.Attributes.Add("style", "display:block")
    '        Else
    '            dvSelectDatelbl.Attributes.Add("style", "display:block")
    '            dvSelectDatelink.Attributes.Add("style", "display:none")
    '        End If

    '        If Not Session("sobjBLLTourSearch") Is Nothing Then
    '            Dim objTour As New BLLTourSearch
    '            objTour = Session("sobjBLLTourSearch")
    '            Dim strAdultchild As String = ""
    '            strAdultchild = "[ " & objTour.Adult & " Ad "
    '            objTour = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
    '            If objTour.Children > 0 Then
    '                strAdultchild = strAdultchild & " + " & objTour.Children & " Ch (" & objTour.ChildAgeString.ToString.Replace(";", ",") & ")"
    '            Else
    '                strAdultchild = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & strAdultchild
    '            End If
    '            strAdultchild = strAdultchild & " ]"
    '            lblTourAdultChild.Text = strAdultchild
    '        End If

    '        'Show Prefered Button
    '        Dim lblPreferred As Label = CType(e.Item.FindControl("lblPreferred"), Label)
    '        Dim btnPreferred As HtmlInputButton = CType(e.Item.FindControl("btnPreferred"), HtmlInputButton)
    '        If lblPreferred.Text = "1" Then
    '            btnPreferred.Visible = True
    '        Else
    '            btnPreferred.Visible = False
    '        End If



    '        If hdWhiteLabel.Value = "1" And Session("sLoginType") <> "RO" Then
    '            lbwlPrice.Visible = True
    '            lbPrice.Visible = False
    '            hdSliderCurrency.Value = " " & hdwlCurrCode.Value
    '        Else
    '            lbwlPrice.Visible = False
    '            hdSliderCurrency.Value = " " & hdCurrCode.Value
    '            ' lbPrice.Visible = True
    '        End If
    '        chkSelectTour.Attributes.Add("onclick", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','c')")
    '        chkSelectTour.Attributes.Add("onchange", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','c')")

    '        txtTourChangeDate.Attributes.Add("onchange", "javascript:SelectedTour('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','d')")
    '        ' chkSelectTour.Attributes.Add("OnCheckedChanged", "Check_Changed()")
    '        dvTourdates.Attributes.Add("onclick", "javascript:SelectedTour1('" + chkSelectTour.ClientID + "', '" + e.Item.ItemIndex.ToString + "','d')")
    '        'Show Hotel Image
    '        Dim imgExcImage As Image = CType(e.Item.FindControl("imgExcImage"), Image)
    '        Dim lblExcImage As Label = CType(e.Item.FindControl("lblExcImage"), Label)
    '        imgExcImage.ImageUrl = "ImageDisplay.aspx?FileName=" & lblExcImage.Text & "&Type=1"

    '        'Show Hotel Stars
    '        Dim hdNoOfExcStars As HiddenField = CType(e.Item.FindControl("hdNoOfExcStars"), HiddenField)
    '        Dim dvExcStars As HtmlGenericControl = CType(e.Item.FindControl("dvExcStars"), HtmlGenericControl)
    '        Dim strExcStarHTML As New StringBuilder

    '        strExcStarHTML.Append(" <nav class='stars'><ul>")
    '        If hdNoOfExcStars.Value = "1" Then
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '        ElseIf hdNoOfExcStars.Value = "2" Then
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")

    '        ElseIf hdNoOfExcStars.Value = "3" Then
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '        ElseIf hdNoOfExcStars.Value = "4" Then
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            ' strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '        ElseIf hdNoOfExcStars.Value = "5" Then
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '            strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-b.png' /></a></li>")
    '        Else
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '            'strExcStarHTML.Append(" <li><a href='#'><img alt='' src='img/star-a.png' /></a></li>")
    '        End If

    '        strExcStarHTML.Append(" </ul>")
    '        dvExcStars.InnerHtml = strExcStarHTML.ToString

    '        Dim lblExcText As Label = CType(e.Item.FindControl("lblExcText"), Label)
    '        Dim lbReadMore As LinkButton = CType(e.Item.FindControl("lbReadMore"), LinkButton)
    '        If lblExcText.Text.Length > 150 Then
    '            lblExcText.Text = lblExcText.Text.Substring(0, 149)

    '        Else
    '            lbReadMore.Visible = False
    '        End If


    '    End If
    '    If iCumulative = 1 Then
    '        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '            Dim lbPrice As LinkButton = CType(e.Item.FindControl("lbPrice"), LinkButton)
    '            Dim lbwlPrice As LinkButton = CType(e.Item.FindControl("lbwlPrice"), LinkButton)
    '            Dim hdRateBasis As HiddenField = CType(e.Item.FindControl("hdRateBasis"), HiddenField)

    '            lbPrice.Text = lbPrice.Text.Replace(".000", "")
    '            'Dim dWlPrice As Double = IIf(lbwlPrice.Text = "", 0, lbwlPrice.Text)
    '            'lbwlPrice.Text = Math.Round(dWlPrice, 2, MidpointRounding.AwayFromZero)
    '            Dim lblPriceBy As Label = CType(e.Item.FindControl("lblPriceBy"), Label)
    '            lbPrice.Visible = False
    '            lblPriceBy.Visible = False
    '            lbwlPrice.Visible = False
    '            If hdRateBasis.Value.ToUpper = "UNIT" Then
    '                lblunits.Visible = True
    '                lblunits.Text = hdncumunits.Value + " Units"
    '            Else
    '                lblunits.Visible = False
    '            End If

    '        End If
    '    Else
    '        lblunits.Visible = False
    '    End If
    'End Sub

    'Protected Sub lbPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Try


    '        Dim lbPrice As LinkButton = CType(sender, LinkButton)
    '        Session("slbTourTotalSaleValue") = lbPrice
    '        Dim dlItem As DataListItem = CType((lbPrice).NamingContainer, DataListItem)
    '        Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
    '        Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
    '        Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
    '        Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
    '        Dim hdRateBasis As HiddenField = CType(dlItem.FindControl("hdRateBasis"), HiddenField)
    '        Dim txtTourChangeDate As TextBox = CType(dlItem.FindControl("txtTourChangeDate"), TextBox)
    '        Dim chkselecttour As CheckBox = CType(dlItem.FindControl("chkselecttour"), CheckBox)

    '        Dim hdcombo As HiddenField = CType(dlItem.FindControl("hdcombo"), HiddenField)
    '        Dim hdMultipleDates As HiddenField = CType(dlItem.FindControl("hdMultipleDates"), HiddenField)
    '        'hdMultiDay.Value = hdMultipleDates.Value
    '        'hdExcCodePopup.Value = hdExcCode.Value
    '        'hdRateBasisPopup.Value = hdRateBasis.Value
    '        'hdCurrCodePopup.Value = hdCurrCode.Value
    '        'hdVehicleCodePopup.Value = hdVehicleCode.Value

    '        Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
    '        Dim strDateFlag = "0"

    '        If hdcombo.Value = "YES" Or hdMultipleDates.Value = "YES" Then
    '            Dim dtselectedCombotour As New DataTable
    '            dtselectedCombotour = Session("selectedCombotourdatatable")

    '            Dim foundRow As DataRow
    '            foundRow = dtselectedCombotour.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' ").FirstOrDefault
    '            If foundRow Is Nothing Then
    '                strDateFlag = "1"
    '            End If



    '            Dim foundRow1 As DataRow
    '            Dim strType As String = ""
    '            If hdcombo.Value = "YES" Then
    '                strType = "COMBO"
    '            End If
    '            If hdMultipleDates.Value = "YES" Then
    '                strType = "MULTI_DATE"
    '            End If

    '            foundRow1 = dtselectedCombotour.Select("exctypcode='" & hdExcCode.Value.Trim & "' AND  vehiclecode='" & hdVehicleCode.Value.Trim & "' AND type='" & strType & "' ").FirstOrDefault
    '            If Not foundRow1 Is Nothing Then
    '                txtTourChangeDate.Text = foundRow("excdate")
    '            End If


    '        Else
    '            If txtTourChangeDate.Text = "" Then
    '                strDateFlag = "1"
    '            End If
    '        End If


    '        If strDateFlag = "1" Then
    '            MessageBox.ShowMessage(Page, MessageType.Warning, "Select date for price.")
    '        Else
    '            'lblTotlaPriceHeading.Text = lblExcName.Text
    '            objBLLTourSearch = New BLLTourSearch
    '            If Session("sobjBLLTourSearch") Is Nothing Then
    '                Response.Redirect("Home.aspx?Tab=1")
    '            End If
    '            objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
    '            objBLLTourSearch.DateChange = "1"
    '            objBLLTourSearch.ExcTypeCode = hdExcCode.Value
    '            objBLLTourSearch.VehicleCode = hdVehicleCode.Value
    '            objBLLTourSearch.SelectedDate = txtTourChangeDate.Text

    '            Dim strDate As String = txtTourChangeDate.Text
    '            If strDate <> "" Then
    '                Dim strDates As String() = strDate.Split("/")
    '                strDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
    '            End If

    '            'hdSelectedDatePopup.Value = strDate

    '            Dim sDt As New DataTable

    '            Dim dsTourPriceResults As New DataSet
    '            If Not Session("sdtTourPriceBreakup") Is Nothing Then
    '                sDt = Session("sdtTourPriceBreakup")
    '                If sDt.Rows.Count > 0 Then
    '                    Dim dvSDt As DataView = New DataView(sDt)
    '                    dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' AND vehiclecode='" & hdVehicleCode.Value & "' "
    '                    'dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
    '                    If dvSDt.Count = 0 Then
    '                        Dim ds As New DataSet
    '                        ds = objBLLTourSearch.GetSearchDetails()
    '                        Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'  AND vehiclecode='" & hdVehicleCode.Value & "' ").First
    '                        'Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'").First
    '                        Dim drNew As DataRow = sDt.NewRow()
    '                        drNew.ItemArray = dr.ItemArray
    '                        sDt.Rows.Add(drNew)
    '                        Session("sdtTourPriceBreakup") = sDt
    '                    Else
    '                        Session("sdtTourPriceBreakup") = sDt
    '                    End If
    '                Else
    '                    dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
    '                    sDt = dsTourPriceResults.Tables(0)
    '                    Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
    '                End If


    '            Else
    '                dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
    '                sDt = dsTourPriceResults.Tables(0)
    '                Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
    '            End If





    '            If sDt.Rows.Count > 0 Then

    '                Dim dvSDt As DataView = New DataView(sDt)
    '                dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "'  AND vehiclecode='" & hdVehicleCode.Value & "'  "
    '                ' dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
    '                If hdRateBasis.Value = "ACS" Then
    '                    dvACS.Visible = True
    '                    dvUnits.Visible = False
    '                    lblNoOfAdult.Text = dvSDt.Item(0)("adults").ToString
    '                    lblNoOfchild.Text = dvSDt.Item(0)("child").ToString
    '                    lblNoOfSeniors.Text = dvSDt.Item(0)("senior").ToString
    '                    lblNoOfUnits.Text = ""


    '                    txtAdultPrice.Text = dvSDt.Item(0)("adultprice").ToString
    '                    txtChildprice.Text = dvSDt.Item(0)("childprice").ToString
    '                    txtSeniorsPrice.Text = dvSDt.Item(0)("seniorprice").ToString




    '                    txtUnitPrice.Text = ""


    '                    lblAdultSaleValue.Text = dvSDt.Item(0)("adultsalevalue").ToString
    '                    lblchildSaleValue.Text = dvSDt.Item(0)("childsalevalue").ToString
    '                    lblSeniorSaleValue.Text = dvSDt.Item(0)("seniorsalevalue").ToString
    '                    lblUnitSaleValue.Text = ""

    '                    ''' Added shahul 27/03/18
    '                    lblNoOfchildasadult.Text = dvSDt.Item(0)("childasadult").ToString
    '                    txtChildasadultprice.Text = dvSDt.Item(0)("childasadultprice").ToString
    '                    lblchildasadultSaleValue.Text = dvSDt.Item(0)("childasadultvalue").ToString



    '                    txtwlUnitPrice.Text = ""


    '                    Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
    '                    Dim dwlAdultprice As Decimal
    '                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc"))) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate"))
    '                    dwlAdultprice = dAdultprice * dWlMarkup
    '                    txtwlAdultPrice.Text = Math.Round(dwlAdultprice)

    '                    Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
    '                    Dim dwlChildtprice As Decimal
    '                    dwlChildtprice = dChildprice * dWlMarkup
    '                    txtwlChildprice.Text = Math.Round(dwlChildtprice)

    '                    Dim dSeniorprice As Decimal = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
    '                    Dim dwlSeniorprice As Decimal
    '                    dwlSeniorprice = dSeniorprice * dWlMarkup
    '                    txtwlSeniorsPrice.Text = Math.Round(dwlSeniorprice)

    '                    ''' Added shahul 27/03/18
    '                    Dim dChildasadultprice As Decimal = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
    '                    Dim dwlChildasadulttprice As Decimal
    '                    dwlChildasadulttprice = dChildasadultprice * dWlMarkup
    '                    txtwlChildasadultprice.Text = Math.Round(dwlChildasadulttprice)



    '                    Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
    '                    Dim dwlAdultSaleValue As Decimal
    '                    dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup)) * Val(lblNoOfAdult.Text)
    '                    txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

    '                    Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '                    Dim dwlChildSaleValue As Decimal
    '                    ' dwlChildSaleValue = dChildSaleValue * dWlMarkup
    '                    dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup)) * Val(lblNoOfchild.Text)
    '                    txtwlChildSaleValue.Text = Math.Round(dwlChildSaleValue)

    '                    Dim dSeniorSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '                    Dim dwlSeniorSaleValue As Decimal
    '                    'dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
    '                    dwlSeniorSaleValue = (Math.Round(dSeniorprice * dWlMarkup)) * Val(lblNoOfSeniors.Text)
    '                    txtwlSeniorsPrice.Text = Math.Round(dwlSeniorSaleValue)

    '                    ''' Added shahul 27/03/18
    '                    Dim dChildasadultSaleValue As Decimal = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)
    '                    Dim dwlChildasadultSaleValue As Decimal
    '                    dwlChildasadultSaleValue = (Math.Round(dwlChildasadulttprice * dWlMarkup)) * Val(lblNoOfchildasadult.Text)
    '                    txtwlChildasadultSaleValue.Text = Math.Round(dwlChildasadultSaleValue)



    '                    If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "block")
    '                        txtChildprice.Style.Add("display", "block")
    '                        txtSeniorsPrice.Style.Add("display", "block")
    '                        txtUnitPrice.Style.Add("display", "none")

    '                        ''' Added shahul 27/03/18
    '                        txtChildasadultprice.Style.Add("display", "block")
    '                        lblchildasadultSaleValue.Style.Add("display", "block")
    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")


    '                        lblAdultSaleValue.Style.Add("display", "block")
    '                        lblchildSaleValue.Style.Add("display", "block")
    '                        lblSeniorSaleValue.Style.Add("display", "block")
    '                        lblUnitSaleValue.Style.Add("display", "none")

    '                    ElseIf hdWhiteLabel.Value = "1" Then
    '                        txtwlAdultPrice.Style.Add("display", "block")
    '                        txtwlChildprice.Style.Add("display", "block")
    '                        txtwlSeniorsPrice.Style.Add("display", "block")

    '                        txtwlAdultSaleValue.Style.Add("display", "block")
    '                        txtwlChildSaleValue.Style.Add("display", "block")
    '                        txtwlSeniorSaleValue.Style.Add("display", "block")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "none")

    '                        ''' Added shahul 27/03/18
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")
    '                        txtwlChildasadultprice.Style.Add("display", "block")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "none")
    '                    Else
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "block")
    '                        txtChildprice.Style.Add("display", "block")
    '                        txtSeniorsPrice.Style.Add("display", "block")
    '                        txtUnitPrice.Style.Add("display", "none")

    '                        ''' Added shahul 27/03/18
    '                        txtChildasadultprice.Style.Add("display", "block")
    '                        lblchildasadultSaleValue.Style.Add("display", "block")
    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")


    '                        lblAdultSaleValue.Style.Add("display", "block")
    '                        lblchildSaleValue.Style.Add("display", "block")
    '                        lblSeniorSaleValue.Style.Add("display", "block")
    '                        lblUnitSaleValue.Style.Add("display", "none")
    '                    End If

    '                Else
    '                    dvACS.Visible = False
    '                    dvUnits.Visible = True
    '                    lblNoOfAdult.Text = ""
    '                    lblNoOfchild.Text = ""
    '                    lblNoOfSeniors.Text = ""
    '                    ''' Added shahul 27/03/18
    '                    lblNoOfchildasadult.Text = ""

    '                    lblNoOfUnits.Text = dvSDt.Item(0)("units").ToString

    '                    txtAdultPrice.Text = ""
    '                    txtChildprice.Text = ""
    '                    txtSeniorsPrice.Text = ""

    '                    ''' Added shahul 27/03/18
    '                    txtChildasadultprice.Text = ""

    '                    txtUnitPrice.Text = dvSDt.Item(0)("unitprice").ToString


    '                    lblAdultSaleValue.Text = ""
    '                    lblchildSaleValue.Text = ""
    '                    lblSeniorSaleValue.Text = ""
    '                    ''' Added shahul 27/03/18
    '                    lblchildasadultSaleValue.Text = ""

    '                    lblUnitSaleValue.Text = dvSDt.Item(0)("unitsalevalue").ToString

    '                    txtwlAdultPrice.Text = ""
    '                    txtwlChildprice.Text = ""
    '                    txtwlSeniorsPrice.Text = ""
    '                    txtwlChildasadultprice.Text = ""
    '                    ' txtwlUnitPrice.Text = dvSDt.Item(0)("unitcprice").ToString


    '                    txtwlAdultSaleValue.Text = ""
    '                    txtwlChildSaleValue.Text = ""
    '                    txtwlSeniorSaleValue.Text = ""
    '                    ''' Added shahul 27/03/18
    '                    txtwlChildasadultSaleValue.Text = ""
    '                    ' txtwlUnitSaleValue.Text = dvSDt.Item(0)("wlunitsalevalue").ToString

    '                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc"))) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate"))

    '                    Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
    '                    Dim dwlUnitPrice As Decimal
    '                    dwlUnitPrice = dUnitPrice * dWlMarkup
    '                    txtwlUnitPrice.Text = Math.Round(dwlUnitPrice)

    '                    Dim dUnitSaleValue As Decimal = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)
    '                    Dim dwlUnitSaleValue As Decimal
    '                    ' dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
    '                    dwlUnitSaleValue = (Math.Round(dUnitSaleValue * dWlMarkup)) * Val(lblNoOfUnits.Text)
    '                    txtwlUnitSaleValue.Text = Math.Round(dwlUnitSaleValue)


    '                    If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")
    '                        txtwlUnitPrice.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "block")

    '                        ''' Added shahul 27/03/18
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")
    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")

    '                    ElseIf hdWhiteLabel.Value = "1" Then
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")
    '                        txtwlUnitPrice.Style.Add("display", "block")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "block")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "none")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "none")

    '                        ''' Added shahul 27/03/18
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")
    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")

    '                    Else
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")
    '                        txtwlUnitPrice.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "block")

    '                        ''' Added shahul 27/03/18
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")
    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")

    '                    End If

    '                End If

    '                If dvSDt.Item(0)("comp_cust").ToString() = "1" Then
    '                    chkComplimentaryToCustomer.Checked = True
    '                Else
    '                    chkComplimentaryToCustomer.Checked = False
    '                End If


    '                If Session("sLoginType") = "RO" Then
    '                    dvComplimentaryToCustomer.Visible = True
    '                    ' ''If chkTourOveridePrice.Checked = True Then
    '                    ' ''    txtUnitPrice.ReadOnly = False
    '                    ' ''    txtAdultPrice.ReadOnly = False
    '                    ' ''    txtChildprice.ReadOnly = False
    '                    ' ''    txtSeniorsPrice.ReadOnly = False
    '                    ' ''    txtChildasadultprice.ReadOnly = False ''' Added shahul 27/03/18
    '                    ' ''    lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")


    '                    ' ''Else
    '                    txtUnitPrice.ReadOnly = True
    '                    txtAdultPrice.ReadOnly = True
    '                    txtChildprice.ReadOnly = True
    '                    txtSeniorsPrice.ReadOnly = True
    '                    txtChildasadultprice.ReadOnly = True ''' Added shahul 27/03/18

    '                    lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

    '                    ' ''End If

    '                    txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(lblAdultSaleValue.ClientID, String) + "')")
    '                    txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(lblchildSaleValue.ClientID, String) + "')")
    '                    txtSeniorsPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfSeniors.ClientID, String) + "', '" + CType(txtSeniorsPrice.ClientID, String) + "' ,'" + CType(lblSeniorSaleValue.ClientID, String) + "')")
    '                    txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")
    '                    ''' Added shahul 27/03/18
    '                    txtChildasadultprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchildasadult.ClientID, String) + "', '" + CType(txtChildasadultprice.ClientID, String) + "' ,'" + CType(lblchildasadultSaleValue.ClientID, String) + "')")

    '                Else
    '                    dvComplimentaryToCustomer.Visible = False
    '                    txtUnitPrice.ReadOnly = True
    '                    txtAdultPrice.ReadOnly = True
    '                    txtChildprice.ReadOnly = True
    '                    txtSeniorsPrice.ReadOnly = True
    '                    txtChildasadultprice.ReadOnly = True ''' Added shahul 27/03/18
    '                    lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

    '                End If

    '                If hdBookingEngineRateType.Value = "1" Then

    '                    dvUnitprice.Style.Add("display", "none")
    '                    dvunitsalevalue.Style.Add("display", "none")
    '                End If
    '                mpTotalprice.Show()
    '            End If
    '        End If


    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("TourSearch.aspx :: lbPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '        ModalPopupDays.Hide()
    '    End Try

    'End Sub

    ' ''Private Sub Toursearch()
    ' ''    Try


    ' ''        Dim objBLLTourSearch As New BLLTourSearch
    ' ''        Dim strSearchCriteria As String = ""
    ' ''        Dim strFromDate As String = txtTourFromDate.Text
    ' ''        Dim strToDate As String = txtTourToDate.Text
    ' ''        Dim strTourStartingFrom As String = txtTourStartingFrom.Text
    ' ''        Dim strTourStartingFromCode As String = txtTourStartingFromCode.Text
    ' ''        Dim strTourClassification As String = txtTourClassification.Text
    ' ''        Dim strTourClassificationCode As String = txtTourClassificationCode.Text

    ' ''        Dim strSourceCountry As String = txtTourSourceCountry.Text
    ' ''        Dim strSourceCountryCode As String = txtTourSourceCountryCode.Text
    ' ''        Dim strCustomer As String = txtTourCustomer.Text
    ' ''        Dim strCustomerCode As String = txtTourCustomerCode.Text
    ' ''        ' ''Dim strStarCategoryCode As String = ddlStarCategory.SelectedValue
    ' ''        ' ''Dim strStarCategory As String = ddlStarCategory.Text
    ' ''        Dim strSeniorCitizen As String = ddlSeniorCitizen.SelectedValue
    ' ''        Dim strAdult As String = ddlTourAdult.SelectedValue
    ' ''        Dim strChildren As String = ddlTourChildren.SelectedValue
    ' ''        Dim strChild1 As String = txtTourChild1.Text
    ' ''        Dim strChild2 As String = txtTourChild2.Text
    ' ''        Dim strChild3 As String = txtTourChild3.Text
    ' ''        Dim strChild4 As String = txtTourChild4.Text
    ' ''        Dim strChild5 As String = txtTourChild5.Text
    ' ''        Dim strChild6 As String = txtTourChild6.Text
    ' ''        Dim strChild7 As String = txtTourChild7.Text
    ' ''        Dim strChild8 As String = txtTourChild8.Text





    ' ''        'If HttpContext.Current.Session("sLoginType") = "RO" Then

    ' ''        '    If chkTourOveridePrice.Checked = True Then
    ' ''        '        MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any hotel name. \n </br></br></br></br></br></br></br></br></br>* If override is ticked then Hotel selection is compulsory.")
    ' ''        '        Exit Sub
    ' ''        '    End If
    ' ''        'End If



    ' ''        If strAdult = "0" Then
    ' ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any adult.")
    ' ''            Exit Sub
    ' ''        End If
    ' ''        If strChildren <> "0" Then
    ' ''            If strChildren = "1" Then
    ' ''                If strChild1 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "2" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "3" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "4" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "5" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "6" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "7" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            ElseIf strChildren = "8" Then
    ' ''                If strChild1 = "" Or strChild2 = "" Or strChild3 = "" Or strChild4 = "" Or strChild5 = "" Or strChild6 = "" Or strChild7 = "" Or strChild8 = "" Then
    ' ''                    MessageBox.ShowMessage(Page, MessageType.Warning, "Please select child age.")
    ' ''                    Exit Sub
    ' ''                End If
    ' ''            End If

    ' ''        End If

    ' ''        If strSourceCountryCode = "" Then
    ' ''            MessageBox.ShowMessage(Page, MessageType.Warning, "Please select any source country.")
    ' ''            Exit Sub
    ' ''        End If

    ' ''        Dim strQueryString As String = ""


    ' ''        If strFromDate <> "" Then
    ' ''            objBLLTourSearch.FromDate = strFromDate
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "FromDate:" & strFromDate
    ' ''        End If
    ' ''        If strToDate <> "" Then
    ' ''            objBLLTourSearch.ToDate = strToDate
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "ToDate:" & strToDate
    ' ''        End If

    ' ''        If strTourStartingFrom <> "" Then
    ' ''            objBLLTourSearch.TourStartingFrom = strTourStartingFrom
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "TourStartingFrom:" & strTourStartingFrom
    ' ''        End If
    ' ''        If strTourStartingFromCode <> "" Then
    ' ''            objBLLTourSearch.TourStartingFromCode = strTourStartingFromCode
    ' ''        End If

    ' ''        Dim strPrivateOrSIC As String = ""
    ' ''        ' ''If chklPrivateOrSIC.Items.Count > 0 Then
    ' ''        ' ''    For i As Integer = 0 To chklPrivateOrSIC.Items.Count - 1
    ' ''        ' ''        If chklPrivateOrSIC.Items(i).Selected = True Then
    ' ''        ' ''            If strPrivateOrSIC = "" Then
    ' ''        ' ''                strPrivateOrSIC = chklPrivateOrSIC.Items(i).Value
    ' ''        ' ''            Else
    ' ''        ' ''                strPrivateOrSIC = strPrivateOrSIC & "," & chklPrivateOrSIC.Items(i).Value
    ' ''        ' ''            End If
    ' ''        ' ''        End If
    ' ''        ' ''    Next
    ' ''        ' ''End If

    ' ''        strSearchCriteria = strSearchCriteria & "||" & "PrivateOrSIC:" & strPrivateOrSIC
    ' ''        ' ''Dim strOveride As String = chkTourOveridePrice.Checked

    ' ''        ' ''If strOveride = "True" Then
    ' ''        ' ''    strSearchCriteria = strSearchCriteria & "||" & "OveridePrice:Yes"
    ' ''        ' ''Else
    ' ''        ' ''    strSearchCriteria = strSearchCriteria & "||" & "OveridePrice:No"
    ' ''        ' ''End If

    ' ''        If strTourClassification <> "" Then
    ' ''            objBLLTourSearch.Classification = strTourClassification
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "Classification:" & strTourClassification
    ' ''        End If
    ' ''        If strTourClassificationCode <> "" Then
    ' ''            objBLLTourSearch.ClassificationCode = strTourClassificationCode
    ' ''        End If

    ' ''        ' ''If strStarCategoryCode <> "" Then
    ' ''        ' ''    objBLLTourSearch.StarCategoryCode = strStarCategoryCode
    ' ''        ' ''End If
    ' ''        ' ''If strStarCategory <> "" Then
    ' ''        ' ''    objBLLTourSearch.StarCategory = strStarCategory
    ' ''        ' ''    strSearchCriteria = strSearchCriteria & "||" & "StarCategory:" & strStarCategory
    ' ''        ' ''End If

    ' ''        If Not Session("sEditRequestId") Is Nothing Then
    ' ''            objBLLTourSearch.AmendRequestid = Session("sEditRequestId")
    ' ''            objBLLTourSearch.AmendLineno = ViewState("Elineno")
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "AmendLineno:" & objBLLTourSearch.AmendLineno
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "AmendRequestid:" & objBLLTourSearch.AmendRequestid
    ' ''        Else
    ' ''            objBLLTourSearch.AmendRequestid = GetExistingRequestId()
    ' ''            objBLLTourSearch.AmendLineno = ViewState("Elineno")
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "AmendLineno:" & objBLLTourSearch.AmendLineno
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "AmendRequestid:" & objBLLTourSearch.AmendRequestid
    ' ''        End If


    ' ''        If strSeniorCitizen <> "" Then
    ' ''            objBLLTourSearch.SeniorCitizen = strSeniorCitizen
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "SeniorCitizen:" & objBLLTourSearch.SeniorCitizen
    ' ''        End If

    ' ''        If strAdult <> "" Then
    ' ''            objBLLTourSearch.Adult = strAdult
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "Adult:" & strAdult
    ' ''        End If
    ' ''        If strChildren <> "" Then
    ' ''            objBLLTourSearch.Children = strChildren
    ' ''            If strChildren = "1" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.ChildAgeString = strChild1
    ' ''                objBLLTourSearch.Child2 = ""
    ' ''                objBLLTourSearch.Child3 = ""
    ' ''                objBLLTourSearch.Child4 = ""
    ' ''                objBLLTourSearch.Child5 = ""
    ' ''                objBLLTourSearch.Child6 = ""
    ' ''                objBLLTourSearch.Child7 = ""
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''            ElseIf strChildren = "2" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = ""
    ' ''                objBLLTourSearch.Child4 = ""
    ' ''                objBLLTourSearch.Child5 = ""
    ' ''                objBLLTourSearch.Child6 = ""
    ' ''                objBLLTourSearch.Child7 = ""
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2
    ' ''            ElseIf strChildren = "3" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = strChild3
    ' ''                objBLLTourSearch.Child4 = ""
    ' ''                objBLLTourSearch.Child5 = ""
    ' ''                objBLLTourSearch.Child6 = ""
    ' ''                objBLLTourSearch.Child7 = ""
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3
    ' ''            ElseIf strChildren = "4" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = strChild3
    ' ''                objBLLTourSearch.Child4 = strChild4
    ' ''                objBLLTourSearch.Child5 = ""
    ' ''                objBLLTourSearch.Child6 = ""
    ' ''                objBLLTourSearch.Child7 = ""
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4
    ' ''            ElseIf strChildren = "5" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = strChild3
    ' ''                objBLLTourSearch.Child4 = strChild4
    ' ''                objBLLTourSearch.Child5 = strChild5
    ' ''                objBLLTourSearch.Child6 = ""
    ' ''                objBLLTourSearch.Child7 = ""
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5
    ' ''            ElseIf strChildren = "6" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = strChild3
    ' ''                objBLLTourSearch.Child4 = strChild4
    ' ''                objBLLTourSearch.Child5 = strChild5
    ' ''                objBLLTourSearch.Child6 = strChild6
    ' ''                objBLLTourSearch.Child7 = ""
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6
    ' ''            ElseIf strChildren = "7" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = strChild3
    ' ''                objBLLTourSearch.Child4 = strChild4
    ' ''                objBLLTourSearch.Child5 = strChild5
    ' ''                objBLLTourSearch.Child6 = strChild6
    ' ''                objBLLTourSearch.Child7 = strChild7
    ' ''                objBLLTourSearch.Child8 = ""
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7
    ' ''            ElseIf strChildren = "8" Then
    ' ''                objBLLTourSearch.Child1 = strChild1
    ' ''                objBLLTourSearch.Child2 = strChild2
    ' ''                objBLLTourSearch.Child3 = strChild3
    ' ''                objBLLTourSearch.Child4 = strChild4
    ' ''                objBLLTourSearch.Child5 = strChild5
    ' ''                objBLLTourSearch.Child6 = strChild6
    ' ''                objBLLTourSearch.Child7 = strChild7
    ' ''                objBLLTourSearch.Child8 = strChild8
    ' ''                objBLLTourSearch.ChildAgeString = strChild1 & ";" & strChild2 & ";" & strChild3 & ";" & strChild4 & ";" & strChild5 & ";" & strChild6 & ";" & strChild7 & ";" & strChild8
    ' ''            End If
    ' ''        End If
    ' ''        strSearchCriteria = strSearchCriteria & "||" & "ChildAgeString:" & objBLLTourSearch.ChildAgeString
    ' ''        If strSourceCountry <> "" Then
    ' ''            objBLLTourSearch.SourceCountry = strSourceCountry
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "SourceCountry:" & objBLLTourSearch.SourceCountry
    ' ''        End If
    ' ''        If strSourceCountryCode <> "" Then
    ' ''            objBLLTourSearch.SourceCountryCode = strSourceCountryCode
    ' ''        End If

    ' ''        If strCustomer <> "" Then
    ' ''            objBLLTourSearch.Customer = strCustomer
    ' ''            strSearchCriteria = strSearchCriteria & "||" & "Agent:" & objBLLTourSearch.Customer
    ' ''        End If
    ' ''        If strCustomerCode <> "" Then
    ' ''            objBLLTourSearch.CustomerCode = strCustomerCode
    ' ''        End If

    ' ''        ' ''If strStarCategory <> "" Then
    ' ''        ' ''    objBLLTourSearch.StarCategory = strStarCategory
    ' ''        ' ''    strSearchCriteria = strSearchCriteria & "||" & "StarCategory:" & objBLLTourSearch.StarCategory
    ' ''        ' ''End If
    ' ''        If strPrivateOrSIC <> "" Then
    ' ''            objBLLTourSearch.PrivateOrSIC = strPrivateOrSIC
    ' ''        End If

    ' ''        Dim dt As DataTable
    ' ''        Dim objBLLHotelSearch As New BLLHotelSearch
    ' ''        Dim strRequestId As String = ""
    ' ''        If Not Session("sobjBLLHotelSearchActive") Is Nothing Then
    ' ''            objBLLHotelSearch = CType(Session("sobjBLLHotelSearchActive"), BLLHotelSearch)
    ' ''            strRequestId = objBLLHotelSearch.OBrequestid
    ' ''            dt = objBLLHotelSearch.GetSectorCheckInAndCheckOutDetails(strRequestId, objBLLTourSearch.TourStartingFromCode)
    ' ''            If dt.Rows.Count > 0 Then
    ' ''                hdChangeFromdate.Value = dt.Rows(0)("CheckIn").ToString
    ' ''                hdChangeTodate.Value = dt.Rows(0)("CheckOut").ToString
    ' ''            End If
    ' ''        Else
    ' ''            hdChangeFromdate.Value = txtTourFromDate.Text
    ' ''            hdChangeTodate.Value = txtTourToDate.Text

    ' ''        End If

    ' ''        objBLLTourSearch.LoginType = HttpContext.Current.Session("sLoginType")
    ' ''        strSearchCriteria = strSearchCriteria & "||" & "LoginType:" & objBLLTourSearch.LoginType
    ' ''        objBLLTourSearch.AgentCode = IIf(Session("sLoginType") = "RO", objBLLTourSearch.CustomerCode, Session("sAgentCode"))
    ' ''        strSearchCriteria = strSearchCriteria & "||" & "AgentCode:" & objBLLTourSearch.AgentCode
    ' ''        objBLLTourSearch.WebuserName = Session("GlobalUserName")
    ' ''        ' ''If chkTourOveridePrice.Checked = True Then
    ' ''        ' ''    objBLLTourSearch.OverridePrice = "1"
    ' ''        ' ''Else
    ' ''        ' ''    objBLLTourSearch.OverridePrice = "0"
    ' ''        ' ''End If
    ' ''        Session("sobjBLLTourSearch") = objBLLTourSearch
    ' ''        Dim dsTourSearchResults As New DataSet
    ' ''        objBLLTourSearch.DateChange = "0"


    ' ''        If Not Session("sobjResParam") Is Nothing Then
    ' ''            objResParam = Session("sobjResParam")
    ' ''            Dim objBLLCommonFuntions As New BLLCommonFuntions()
    ' ''            '  Dim strStatus As String = objBLLCommonFuntions.SaveSearchLog(objResParam.AgentCode, objResParam.SubUserCode, objResParam.LoginIp, objResParam.LoginIpLocationName, "Tour Search Page", "Tour Search", strSearchCriteria, Session("GlobalUserName"))
    ' ''        End If

    ' ''        dsTourSearchResults = objBLLTourSearch.GetSearchDetails()
    ' ''        'If dsTourSearchResults Is Nothing Then
    ' ''        '    dvhotnoshow.Style.Add("display", "block")
    ' ''        '    dlTourSearchResults.DataBind()
    ' ''        '    Exit Sub
    ' ''        'End If
    ' ''        'If dsTourSearchResults.Tables(0).Rows.Count = 0 Then
    ' ''        '    dvhotnoshow.Style.Add("display", "block")
    ' ''        'Else
    ' ''        '    dvhotnoshow.Style.Add("display", "none")
    ' ''        'End If

    ' ''        Session("sDSTourSearchResults") = dsTourSearchResults
    ' ''        If dsTourSearchResults.Tables.Count > 0 Then

    ' ''            ' ''BindTourPricefilter(dsTourSearchResults.Tables(1))
    ' ''            ' ''BindTourHotelStars(dsTourSearchResults.Tables(2))
    ' ''            ' ''BindTourRoomClassification(dsTourSearchResults.Tables(3))
    ' ''            Session("sDSTourSearchResults") = dsTourSearchResults
    ' ''            Session("sTourPageIndex") = "1"
    ' ''            Dim dvMaiDetails As DataView = New DataView(dsTourSearchResults.Tables(0))
    ' ''            ' ''If ddlSorting.Text = "Name" Then
    ' ''            ' ''    dvMaiDetails.Sort = "tourselected DESC, excname ASC" 'changed by mohamed on 12/02/2018 --tourselected desc is added
    ' ''            ' ''ElseIf ddlSorting.Text = "Price" Then
    ' ''            ' ''    dvMaiDetails.Sort = "tourselected DESC, totalsalevalue ASC" 'changed by mohamed on 12/02/2018 --tourselected desc is added
    ' ''            ' ''ElseIf ddlSorting.Text = "Preferred" Then
    ' ''            ' ''    dvMaiDetails.Sort = "tourselected DESC,Preferred desc, excname ASC"
    ' ''            ' ''ElseIf ddlSorting.Text = "Rating" Then
    ' ''            ' ''    dvMaiDetails.Sort = "tourselected DESC, starcategory DESC,excname ASC " 'changed by mohamed on 12/02/2018 --tourselected desc is added
    ' ''            ' ''End If
    ' ''            Dim recordCount As Integer = dvMaiDetails.Count
    ' ''            ' ''BindTourMainDetails(dvMaiDetails)
    ' ''            Me.PopulatePager(recordCount)
    ' ''            ' ''lblHotelCount.Text = dsTourSearchResults.Tables(0).Rows.Count & " Records Found"

    ' ''        Else
    ' ''            hdPriceMinRange.Value = "0"
    ' ''            hdPriceMaxRange.Value = "1"
    ' ''        End If

    ' ''    Catch ex As Exception
    ' ''        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    ' ''        objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnTourFill_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    ' ''    End Try
    ' ''End Sub


    'Protected Sub btnPriceBreakupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceBreakupSave.Click
    '    Try

    '        Dim fTotalSaleValue As Double = 0
    '        Dim dwlTotalSaleValue As Double = 0
    '        If hdRateBasisPopup.Value = "ACS" Then
    '            fTotalSaleValue = CType(IIf(Val(lblNoOfAdult.Text) = 0, "0", lblAdultSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchild.Text) = 0, "0", lblchildSaleValue.Text), Double) + CType(IIf(Val(lblNoOfSeniors.Text) = 0, "0", lblSeniorSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchildasadult.Text) = 0, "0", lblchildasadultSaleValue.Text), Double)
    '            dwlTotalSaleValue = CType(IIf(Val(lblNoOfAdult.Text) = 0, "0", txtwlAdultSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchild.Text) = 0, "0", txtwlChildSaleValue.Text), Double) + CType(IIf(Val(lblNoOfSeniors.Text) = 0, "0", txtwlSeniorSaleValue.Text), Double) + CType(IIf(Val(lblNoOfchildasadult.Text) = 0, "0", txtwlChildasadultSaleValue.Text), Double)
    '        Else
    '            fTotalSaleValue = IIf(lblUnitSaleValue.Text = "", "0", lblUnitSaleValue.Text)
    '            dwlTotalSaleValue = IIf(txtwlUnitSaleValue.Text = "", "0", txtwlUnitSaleValue.Text)
    '        End If


    '        Dim ds As DataSet
    '        ds = Session("sDSTourSearchResults")

    '        If ds.Tables(0).Rows.Count > 0 Then
    '            Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCodePopup.Value & "'  AND vehiclecode='" & hdVehicleCodePopup.Value & "' ").First
    '            ' Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCodePopup.Value & "' ").First


    '            dr("totalsalevalue") = fTotalSaleValue.ToString.Replace(".00", "").Replace(".0", "")
    '            dr("adultprice") = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
    '            dr("childprice") = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)

    '            'Added shahul 27/03/18
    '            dr("childasadultprice") = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
    '            dr("childasadultvalue") = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)


    '            dr("seniorprice") = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
    '            dr("unitprice") = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
    '            dr("units") = IIf(lblNoOfUnits.Text = "", "0", lblNoOfUnits.Text)

    '            dr("adultsalevalue") = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
    '            dr("childsalevalue") = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '            dr("seniorsalevalue") = IIf(lblSeniorSaleValue.Text = "", "0.00", lblSeniorSaleValue.Text)
    '            dr("unitsalevalue") = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)


    '            Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
    '            Dim dwlAdultprice As Decimal
    '            Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
    '            dwlAdultprice = dAdultprice * dWlMarkup
    '            ' dr("adultcprice") = dwlAdultprice

    '            Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
    '            Dim dwlChildtprice As Decimal
    '            dwlChildtprice = dChildprice * dWlMarkup
    '            ' dr("childcprice") = dwlChildtprice

    '            'Added shahul 27/03/18
    '            Dim dChildasadultprice As Decimal = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
    '            Dim dwlChildasadulttprice As Decimal
    '            dwlChildasadulttprice = dChildasadultprice * dWlMarkup

    '            Dim dSeniorprice As Decimal = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
    '            Dim dwlSeniorprice As Decimal
    '            dwlSeniorprice = dSeniorprice * dWlMarkup
    '            ' dr("seniorcprice") = dwlSeniorprice


    '            Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
    '            Dim dwlUnitPrice As Decimal
    '            dwlUnitPrice = dUnitPrice * dWlMarkup
    '            ' dr("unitcprice") = dwlUnitPrice

    '            Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
    '            Dim dwlAdultSaleValue As Decimal
    '            dwlAdultSaleValue = dAdultSaleValue * dWlMarkup
    '            dr("wlAdultSaleValue") = dwlAdultSaleValue

    '            Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '            Dim dwlChildSaleValue As Decimal
    '            dwlChildSaleValue = dChildSaleValue * dWlMarkup
    '            dr("wlChildSaleValue") = dwlChildSaleValue

    '            'Added shahul 27/03/18
    '            Dim dChildadultSaleValue As Decimal = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)
    '            Dim dwlChildadultSaleValue As Decimal
    '            dwlChildadultSaleValue = dChildadultSaleValue * dWlMarkup
    '            dr("wlchildasadultvalue") = dwlChildadultSaleValue

    '            Dim dSeniorSaleValue As Decimal = IIf(lblSeniorSaleValue.Text = "", "0.00", lblSeniorSaleValue.Text)
    '            Dim dwlSeniorSaleValue As Decimal
    '            dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
    '            dr("wlSeniorSaleValue") = dwlSeniorSaleValue

    '            Dim dUnitSaleValue As Decimal = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)
    '            Dim dwlUnitSaleValue As Decimal
    '            dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
    '            dr("wlUnitSaleValue") = dwlUnitSaleValue
    '            If hdRateBasisPopup.Value = "ACS" Then
    '                dr("wltotalsalevalue") = Math.Round((Math.Round(dAdultprice * dWlMarkup, 2)) * Val(lblNoOfAdult.Text)) + Math.Round((Math.Round(dChildprice * dWlMarkup, 2)) * Val(lblNoOfchild.Text)) + Math.Round((Math.Round(dChildasadultprice * dWlMarkup, 2)) * Val(lblNoOfchildasadult.Text)) + Math.Round((Math.Round(dSeniorprice * dWlMarkup, 2)) * Val(lblNoOfSeniors.Text))
    '            Else
    '                dr("wltotalsalevalue") = Math.Round((Math.Round(dUnitPrice * dWlMarkup, 2)) * Val(lblNoOfUnits.Text))
    '            End If


    '            '  dr("wltotalsalevalue") = dwlTotalSaleValue ' fTotalSaleValue * dWlMarkup

    '            'Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
    '            'Dim dwlAdultSaleValue As Decimal
    '            'dwlAdultSaleValue = (Math.Round(dAdultprice * dWlMarkup)) * Val(lblNoOfAdult.Text)
    '            'txtwlAdultSaleValue.Text = Math.Round(dwlAdultSaleValue)

    '            'Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '            'Dim dwlChildSaleValue As Decimal
    '            '' dwlChildSaleValue = dChildSaleValue * dWlMarkup
    '            'dwlChildSaleValue = (Math.Round(dChildprice * dWlMarkup)) * Val(lblNoOfchild.Text)
    '            'txtwlChildSaleValue.Text = Math.Round(dwlChildSaleValue)

    '            'Dim dSeniorSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '            'Dim dwlSeniorSaleValue As Decimal
    '            'dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
    '            'dwlSeniorSaleValue = (Math.Round(dSeniorprice * dWlMarkup)) * Val(lblNoOfSeniors.Text)
    '            'txtwlSeniorsPrice.Text = Math.Round(dwlSeniorSaleValue)




    '            If chkComplimentaryToCustomer.Checked = True Then
    '                dr("comp_cust") = "1"
    '            Else
    '                dr("comp_cust") = "0"
    '            End If
    '        End If
    '        Dim dtTourPriceBreakup As New DataTable
    '        If Not Session("sdtTourPriceBreakup") Is Nothing Then
    '            dtTourPriceBreakup = Session("sdtTourPriceBreakup")
    '            If dtTourPriceBreakup.Rows.Count > 0 Then
    '                Dim dr As DataRow = dtTourPriceBreakup.Select("exctypcode='" & hdExcCodePopup.Value & "' and selecteddate='" & hdSelectedDatePopup.Value & "'  AND vehiclecode='" & hdVehicleCodePopup.Value & "' ").First
    '                ' Dim dr As DataRow = dtTourPriceBreakup.Select("exctypcode='" & hdExcCodePopup.Value & "' and selecteddate='" & hdSelectedDatePopup.Value & "' ").First

    '                dr("adultprice") = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
    '                dr("childprice") = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
    '                dr("seniorprice") = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
    '                dr("unitprice") = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
    '                dr("units") = IIf(lblNoOfUnits.Text = "", "0", lblNoOfUnits.Text)

    '                'Added shahul 27/03/18
    '                dr("childasadultprice") = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
    '                dr("childasadultvalue") = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)

    '                dr("adultsalevalue") = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
    '                dr("childsalevalue") = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '                dr("seniorsalevalue") = IIf(lblSeniorSaleValue.Text = "", "0.00", lblSeniorSaleValue.Text)
    '                dr("unitsalevalue") = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)

    '                '  If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then

    '                Dim dAdultprice As Decimal = IIf(txtAdultPrice.Text = "", "0.00", txtAdultPrice.Text)
    '                Dim dwlAdultprice As Decimal
    '                Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dr("wlmarkupperc"))) / 100) * Convert.ToDecimal(dr("wlconvrate"))
    '                dwlAdultprice = dAdultprice * dWlMarkup
    '                dr("adultcprice") = dwlAdultprice

    '                Dim dChildprice As Decimal = IIf(txtChildprice.Text = "", "0.00", txtChildprice.Text)
    '                Dim dwlChildtprice As Decimal
    '                dwlChildtprice = dChildprice * dWlMarkup
    '                dr("childcprice") = dwlChildtprice

    '                Dim dChildadultprice As Decimal = IIf(txtChildasadultprice.Text = "", "0.00", txtChildasadultprice.Text)
    '                Dim dwlChildadulttprice As Decimal
    '                dwlChildadulttprice = dChildadultprice * dWlMarkup
    '                dr("childasadultcprice") = dwlChildadulttprice

    '                Dim dSeniorprice As Decimal = IIf(txtSeniorsPrice.Text = "", "0.00", txtSeniorsPrice.Text)
    '                Dim dwlSeniorprice As Decimal
    '                dwlSeniorprice = dSeniorprice * dWlMarkup
    '                dr("seniorcprice") = dwlSeniorprice


    '                Dim dUnitPrice As Decimal = IIf(txtUnitPrice.Text = "", "0.00", txtUnitPrice.Text)
    '                Dim dwlUnitPrice As Decimal
    '                dwlUnitPrice = dUnitPrice * dWlMarkup
    '                dr("unitcprice") = dwlUnitPrice

    '                Dim dAdultSaleValue As Decimal = IIf(lblAdultSaleValue.Text = "", "0.00", lblAdultSaleValue.Text)
    '                Dim dwlAdultSaleValue As Decimal
    '                dwlAdultSaleValue = dAdultSaleValue * dWlMarkup
    '                dr("wlAdultSaleValue") = dwlAdultSaleValue

    '                Dim dChildSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '                Dim dwlChildSaleValue As Decimal
    '                dwlChildSaleValue = dChildSaleValue * dWlMarkup
    '                dr("wlChildSaleValue") = dwlChildSaleValue

    '                'Added shahul 27/03/18
    '                Dim dChildadultSaleValue As Decimal = IIf(lblchildasadultSaleValue.Text = "", "0.00", lblchildasadultSaleValue.Text)
    '                Dim dwlChildadultSaleValue As Decimal
    '                dwlChildadultSaleValue = dChildadultSaleValue * dWlMarkup
    '                dr("wlchildasadultvalue") = dwlChildadultSaleValue


    '                Dim dSeniorSaleValue As Decimal = IIf(lblchildSaleValue.Text = "", "0.00", lblchildSaleValue.Text)
    '                Dim dwlSeniorSaleValue As Decimal
    '                dwlSeniorSaleValue = dSeniorSaleValue * dWlMarkup
    '                dr("wlSeniorSaleValue") = dwlSeniorSaleValue

    '                Dim dUnitSaleValue As Decimal = IIf(lblUnitSaleValue.Text = "", "0.00", lblUnitSaleValue.Text)
    '                Dim dwlUnitSaleValue As Decimal
    '                dwlUnitSaleValue = dUnitSaleValue * dWlMarkup
    '                dr("wlUnitSaleValue") = dwlUnitSaleValue

    '                dr("wltotalsalevalue") = dwlTotalSaleValue ' fTotalSaleValue * dWlMarkup
    '                'End If

    '                If chkComplimentaryToCustomer.Checked = True Then
    '                    dr("comp_cust") = "1"
    '                Else
    '                    dr("comp_cust") = "0"
    '                End If
    '            End If

    '        End If


    '        Session("sDSTourSearchResults") = ds
    '        Session("sdtTourPriceBreakup") = dtTourPriceBreakup

    '        Dim lbTotalPrice As New LinkButton
    '        lbTotalPrice = CType(Session("slbTourTotalSaleValue"), LinkButton)

    '        Dim dlItem As DataListItem = CType((lbTotalPrice).NamingContainer, DataListItem)
    '        Dim lbPrice As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lbPrice"), LinkButton)
    '        Dim lblunits As LinkButton = CType(dlTourSearchResults.Items(dlItem.ItemIndex).FindControl("lblunits"), LinkButton)

    '        If hdMultiDay.Value = "YES" Then
    '            Dim dtselectedCombotour As New DataTable
    '            dtselectedCombotour = Session("selectedCombotourdatatable")
    '            Dim strType As String = "MULTI_DATE"

    '            Dim dvComboBreakup As DataView
    '            dvComboBreakup = New DataView(dtselectedCombotour)
    '            dvComboBreakup.RowFilter = "exctypcode= '" & hdExcCodePopup.Value & "'  AND  vehiclecode='" & hdVehicleCodePopup.Value & "'  AND type='" & strType & "' "
    '            Dim dtComboBreakup As DataTable = dvComboBreakup.ToTable
    '            If dtComboBreakup.Rows.Count > 1 Then
    '                fTotalSaleValue = fTotalSaleValue * dtComboBreakup.Rows.Count
    '            End If
    '        End If

    '        lbPrice.Text = hdCurrCodePopup.Value & " " & fTotalSaleValue.ToString()

    '        lblunits.Text = lblNoOfUnits.Text + " Units"




    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("TourSearch.aspx :: btnPriceBreakupSave_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '        ModalPopupDays.Hide()
    '    End Try
    'End Sub
    Private Function Validatedetails() As String
        Dim dtselectedtour As DataTable = CType(Session("selectedtourdatatable"), DataTable)
        Validatedetails = True
        If Not dtselectedtour Is Nothing Then
            If dtselectedtour.Rows.Count = 0 And Not ViewState("OLineNo") Is Nothing Then
                MessageBox.ShowMessage(Page, MessageType.Warning, "Amend/Edit Option Please Select any Tour ")
                Return False
                Exit Function
            End If
        End If


    End Function

    <WebMethod()> _
    Public Shared Function GetCountryDetails(ByVal CustCode As String) As String

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            strSqlQry = "select a.ctrycode,c.ctryname,d.currcode from agentmast_countries a,ctrymast c,agentmast d where a.ctrycode=c.ctrycode and a.agentcode= '" & CustCode.Trim & "' and d.agentcode ='" + CustCode.Trim + "'  order by ctryname"
            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS, "Countries")

            Return myDS.GetXml()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function GetCustomers(ByVal prefixText As String) As List(Of String)

        Dim strSqlQry As String = ""
        Dim myDS As New DataSet
        Dim Hotelnames As New List(Of String)
        Try
            If prefixText = " " Then
                prefixText = ""
            End If
            If HttpContext.Current.Session("sLoginType") = "RO" Then
                strSqlQry = "select agentcode,agentname  from agentmast where active=1  and divcode in (select divcode from agentmast where agentcode='" & HttpContext.Current.Session("sAgentCode") & "') and agentname like  '" & prefixText & "%'  order by agentname  "
            Else
                If HttpContext.Current.Session("sAgentCompany") = "924065660726315" Then 'AgentsOnlineCommon
                    strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='01' and agentname like  '" & prefixText & "%'  order by agentname  "
                Else
                    strSqlQry = "select agentcode,agentname  from agentmast where active=1 and divcode='02' and agentname like  '" & prefixText & "%'  order by agentname  "
                End If
            End If




            Dim SqlConn As New SqlConnection
            Dim myDataAdapter As New SqlDataAdapter
            ' SqlConn = clsDBConnect.dbConnectionnew(objclsConnectionName.ConnectionName)
            SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'Open connection
            myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
            myDataAdapter.Fill(myDS)

            If myDS.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                    Hotelnames.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myDS.Tables(0).Rows(i)("agentname").ToString(), myDS.Tables(0).Rows(i)("agentcode").ToString()))
                Next

            End If

            Return Hotelnames
        Catch ex As Exception
            Return Hotelnames
        End Try

    End Function



    Private Sub FillSpecifiedChildAges(ByVal childages As String)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild1, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild2, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild3, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild4, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild5, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild6, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild7, childages)
        'objclsUtilities.FillDropDownListWithSpecifiedAges(ddlTourChild8, childages)

    End Sub

    Private Sub FillSpecifiedAdultChild(ByVal adults As String, ByVal child As String)

        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourAdult, adults)
        objclsUtilities.FillDropDownListBasedOnNumber(ddlTourChildren, child)
    End Sub
    Private Shared Function GetExistingRequestId() As String
        Dim strRequestId As String = ""
        If HttpContext.Current.Session("sRequestId") Is Nothing Then
            strRequestId = ""
        Else
            Dim objBLLCommonFuntions As New BLLCommonFuntions
            strRequestId = HttpContext.Current.Session("sRequestId")
            ' strRequestId = objBLLCommonFuntions.GetExistingBookingRequestId(strRequestId)
        End If
        Return strRequestId
    End Function

    Private Shared Function GetNewOrExistingRequestId() As String
        Dim strRequestId As String = ""
        Dim objBLLHotelSearch2 As New BLLHotelSearch
        strRequestId = GetExistingRequestId()
        If strRequestId = "" Then
            strRequestId = objBLLHotelSearch2.getrequestid()
        End If
        Return strRequestId
    End Function
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



    Protected Sub btnMyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAccount.Click
        Response.Redirect("MyAccount.aspx")
    End Sub

    'Protected Sub lbwlPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim lbwlPrice As LinkButton = CType(sender, LinkButton)
    '        Session("slbTourTotalSaleValue") = lbwlPrice
    '        Dim dlItem As DataListItem = CType((lbwlPrice).NamingContainer, DataListItem)
    '        Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
    '        Dim hdCurrCode As HiddenField = CType(dlItem.FindControl("hdCurrCode"), HiddenField)
    '        Dim lblExcName As Label = CType(dlItem.FindControl("lblExcName"), Label)
    '        Dim hdVehicleCode As HiddenField = CType(dlItem.FindControl("hdVehicleCode"), HiddenField)
    '        Dim hdRateBasis As HiddenField = CType(dlItem.FindControl("hdRateBasis"), HiddenField)
    '        Dim txtTourChangeDate As TextBox = CType(dlItem.FindControl("txtTourChangeDate"), TextBox)
    '        Dim chkselecttour As CheckBox = CType(dlItem.FindControl("chkselecttour"), CheckBox)

    '        hdExcCodePopup.Value = hdExcCode.Value
    '        hdRateBasisPopup.Value = hdRateBasis.Value
    '        hdCurrCodePopup.Value = hdCurrCode.Value
    '        hdVehicleCodePopup.Value = hdVehicleCode.Value

    '        If txtTourChangeDate.Text = "" Then
    '            MessageBox.ShowMessage(Page, MessageType.Warning, "Select date for price.")
    '        Else
    '            lblTotlaPriceHeading.Text = lblExcName.Text
    '            objBLLTourSearch = New BLLTourSearch
    '            If Session("sobjBLLTourSearch") Is Nothing Then
    '                Response.Redirect("Home.aspx?Tab=1")
    '            End If
    '            objBLLTourSearch = CType(Session("sobjBLLTourSearch"), BLLTourSearch)
    '            objBLLTourSearch.DateChange = "1"
    '            objBLLTourSearch.ExcTypeCode = hdExcCode.Value
    '            objBLLTourSearch.VehicleCode = hdVehicleCode.Value
    '            objBLLTourSearch.SelectedDate = txtTourChangeDate.Text

    '            Dim strDate As String = txtTourChangeDate.Text
    '            If strDate <> "" Then
    '                Dim strDates As String() = strDate.Split("/")
    '                strDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
    '            End If

    '            hdSelectedDatePopup.Value = strDate

    '            Dim sDt As New DataTable

    '            Dim dsTourPriceResults As New DataSet
    '            If Not Session("sdtTourPriceBreakup") Is Nothing Then
    '                sDt = Session("sdtTourPriceBreakup")
    '                If sDt.Rows.Count > 0 Then
    '                    Dim dvSDt As DataView = New DataView(sDt)
    '                    dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' AND vehiclecode='" & hdVehicleCode.Value & "' "
    '                    'dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
    '                    If dvSDt.Count = 0 Then
    '                        Dim ds As New DataSet
    '                        ds = objBLLTourSearch.GetSearchDetails()
    '                        Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'  AND vehiclecode='" & hdVehicleCode.Value & "' ").First
    '                        'Dim dr As DataRow = ds.Tables(0).Select("exctypcode='" & hdExcCode.Value & "'").First
    '                        Dim drNew As DataRow = sDt.NewRow()
    '                        drNew.ItemArray = dr.ItemArray
    '                        sDt.Rows.Add(drNew)
    '                        Session("sdtTourPriceBreakup") = sDt
    '                    Else
    '                        Session("sdtTourPriceBreakup") = sDt
    '                    End If
    '                Else
    '                    dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
    '                    sDt = dsTourPriceResults.Tables(0)
    '                    Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
    '                End If


    '            Else
    '                dsTourPriceResults = objBLLTourSearch.GetSearchDetails()
    '                sDt = dsTourPriceResults.Tables(0)
    '                Session("sdtTourPriceBreakup") = dsTourPriceResults.Tables(0)
    '            End If





    '            If sDt.Rows.Count > 0 Then

    '                Dim dvSDt As DataView = New DataView(sDt)
    '                dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "'  AND vehiclecode='" & hdVehicleCode.Value & "'  "
    '                ' dvSDt.RowFilter = "exctypcode='" & hdExcCode.Value & "' AND selecteddate='" & strDate & "' "
    '                If hdRateBasis.Value = "ACS" Then
    '                    dvACS.Visible = True
    '                    dvUnits.Visible = False
    '                    lblNoOfAdult.Text = dvSDt.Item(0)("adults").ToString
    '                    lblNoOfchild.Text = dvSDt.Item(0)("child").ToString
    '                    lblNoOfSeniors.Text = dvSDt.Item(0)("senior").ToString
    '                    lblNoOfchildasadult.Text = dvSDt.Item(0)("childasadult").ToString

    '                    lblNoOfUnits.Text = ""

    '                    txtAdultPrice.Text = dvSDt.Item(0)("adultprice").ToString
    '                    txtChildprice.Text = dvSDt.Item(0)("childprice").ToString
    '                    txtSeniorsPrice.Text = dvSDt.Item(0)("seniorprice").ToString
    '                    txtChildasadultprice.Text = dvSDt.Item(0)("childasadultprice").ToString

    '                    txtUnitPrice.Text = ""


    '                    lblAdultSaleValue.Text = dvSDt.Item(0)("adultsalevalue").ToString
    '                    lblchildSaleValue.Text = dvSDt.Item(0)("childsalevalue").ToString
    '                    lblSeniorSaleValue.Text = dvSDt.Item(0)("seniorsalevalue").ToString
    '                    lblchildSaleValue.Text = dvSDt.Item(0)("childasadultvalue").ToString
    '                    lblUnitSaleValue.Text = ""

    '                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc").ToString)) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate").ToString)
    '                    If (dvSDt.Item(0)("adults").ToString = "0") Then
    '                        txtwlAdultPrice.Text = "0"
    '                    Else
    '                        txtwlAdultPrice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wladultsalevalue").ToString = "", "0", dvSDt.Item(0)("wladultsalevalue").ToString)) / Val(IIf(dvSDt.Item(0)("adults").ToString = "", "0", dvSDt.Item(0)("adults").ToString)), 2)  ' Math.Round(Val(IIf(dvSDt.Item(0)("adultprice").ToString = "", "0", dvSDt.Item(0)("adultprice").ToString) * dWlMarkup), 2)
    '                    End If

    '                    txtwlAdultPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")

    '                    If (dvSDt.Item(0)("childasadult").ToString = "0") Then
    '                        txtwlChildasadultprice.Text = "0"
    '                    Else
    '                        txtwlChildasadultprice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wlchildasadultvalue").ToString = "", "0", dvSDt.Item(0)("wlchildasadultvalue").ToString)) / Val(IIf(dvSDt.Item(0)("childasadult").ToString = "", "0", dvSDt.Item(0)("childasadult").ToString)))
    '                    End If

    '                    If (dvSDt.Item(0)("child").ToString = "0") Then
    '                        txtwlChildprice.Text = "0"
    '                    Else
    '                        txtwlChildprice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wlchildsalevalue").ToString = "", "0", dvSDt.Item(0)("wlchildsalevalue").ToString)) / Val(IIf(dvSDt.Item(0)("child").ToString = "", "0", dvSDt.Item(0)("child").ToString)))
    '                    End If


    '                    If (dvSDt.Item(0)("senior").ToString = "0") Then
    '                        txtwlSeniorsPrice.Text = "0"
    '                    Else
    '                        txtwlSeniorsPrice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("wlseniorsalevalue").ToString = "", "0", dvSDt.Item(0)("wlseniorsalevalue").ToString)) / Val(IIf(dvSDt.Item(0)("senior").ToString = "", "0", dvSDt.Item(0)("senior").ToString))) ' Math.Round(Val(IIf(dvSDt.Item(0)("seniorprice").ToString = "", "0", dvSDt.Item(0)("seniorprice").ToString) * dWlMarkup), 2)
    '                    End If

    '                    txtwlAdultPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")
    '                    txtwlChildprice.Attributes.Add("onkeydown", "fnReadOnly(event)")
    '                    txtwlSeniorsPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")

    '                    txtwlChildasadultprice.Attributes.Add("onkeydown", "fnReadOnly(event)")

    '                    txtwlUnitPrice.Text = ""


    '                    txtwlAdultSaleValue.Text = dvSDt.Item(0)("wladultsalevalue").ToString
    '                    txtwlChildSaleValue.Text = dvSDt.Item(0)("wlchildsalevalue").ToString
    '                    txtwlSeniorSaleValue.Text = dvSDt.Item(0)("wlseniorsalevalue").ToString

    '                    txtwlChildasadultSaleValue.Text = dvSDt.Item(0)("wlchildasadultvalue").ToString

    '                    txtwlUnitSaleValue.Text = ""

    '                    If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")

    '                        txtwlChildasadultprice.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "block")
    '                        txtChildprice.Style.Add("display", "block")
    '                        txtSeniorsPrice.Style.Add("display", "block")
    '                        txtUnitPrice.Style.Add("display", "none")

    '                        txtChildasadultprice.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "block")
    '                        lblchildSaleValue.Style.Add("display", "block")
    '                        lblSeniorSaleValue.Style.Add("display", "block")
    '                        lblUnitSaleValue.Style.Add("display", "none")

    '                        lblchildasadultSaleValue.Style.Add("display", "block")


    '                    ElseIf hdWhiteLabel.Value = "1" Then
    '                        txtwlAdultPrice.Style.Add("display", "block")
    '                        txtwlChildprice.Style.Add("display", "block")
    '                        txtwlSeniorsPrice.Style.Add("display", "block")

    '                        txtwlChildasadultprice.Style.Add("display", "block")


    '                        txtwlAdultSaleValue.Style.Add("display", "block")
    '                        txtwlChildSaleValue.Style.Add("display", "block")
    '                        txtwlSeniorSaleValue.Style.Add("display", "block")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtwlChildasadultSaleValue.Style.Add("display", "block")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "none")

    '                        txtChildasadultprice.Style.Add("display", "none")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "none")

    '                        lblchildasadultSaleValue.Style.Add("display", "none")
    '                    Else
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")

    '                        txtwlChildasadultprice.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "block")
    '                        txtChildprice.Style.Add("display", "block")
    '                        txtSeniorsPrice.Style.Add("display", "block")
    '                        txtUnitPrice.Style.Add("display", "none")

    '                        txtChildasadultprice.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "block")
    '                        lblchildSaleValue.Style.Add("display", "block")
    '                        lblSeniorSaleValue.Style.Add("display", "block")
    '                        lblUnitSaleValue.Style.Add("display", "none")

    '                        lblchildasadultSaleValue.Style.Add("display", "block")
    '                    End If

    '                Else
    '                    dvACS.Visible = False
    '                    dvUnits.Visible = True

    '                    lblNoOfAdult.Text = ""
    '                    lblNoOfchild.Text = ""
    '                    lblNoOfSeniors.Text = ""
    '                    lblNoOfchildasadult.Text = ""
    '                    lblNoOfUnits.Text = dvSDt.Item(0)("units").ToString

    '                    txtAdultPrice.Text = ""
    '                    txtChildprice.Text = ""
    '                    txtSeniorsPrice.Text = ""
    '                    txtChildasadultprice.Text = ""
    '                    txtUnitPrice.Text = dvSDt.Item(0)("unitprice").ToString


    '                    txtwlAdultSaleValue.Text = ""
    '                    txtwlChildSaleValue.Text = ""
    '                    txtwlSeniorSaleValue.Text = ""
    '                    txtwlChildasadultprice.Text = ""
    '                    txtwlUnitSaleValue.Text = dvSDt.Item(0)("unitsalevalue").ToString


    '                    txtwlAdultPrice.Text = ""
    '                    txtwlChildprice.Text = ""
    '                    txtwlSeniorsPrice.Text = ""
    '                    txtwlChildasadultprice.Text = ""

    '                    Dim dWlMarkup As Decimal = ((100 + Convert.ToDecimal(dvSDt.Item(0)("wlmarkupperc").ToString)) / 100) * Convert.ToDecimal(dvSDt.Item(0)("wlconvrate").ToString)
    '                    txtwlUnitPrice.Text = Math.Round(Val(IIf(dvSDt.Item(0)("unitprice").ToString = "", "0", dvSDt.Item(0)("unitprice").ToString) * dWlMarkup), 2)


    '                    txtwlAdultSaleValue.Text = ""
    '                    txtwlChildSaleValue.Text = ""
    '                    txtwlSeniorSaleValue.Text = ""
    '                    txtwlChildasadultSaleValue.Text = ""

    '                    txtwlUnitSaleValue.Text = dvSDt.Item(0)("wlunitsalevalue").ToString
    '                    txtwlUnitPrice.Attributes.Add("onkeydown", "fnReadOnly(event)")

    '                    If hdWhiteLabel.Value = "1" And Session("sLoginType") = "RO" Then
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")
    '                        txtwlUnitPrice.Style.Add("display", "none")

    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "block")
    '                    ElseIf hdWhiteLabel.Value = "1" Then
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")
    '                        txtwlUnitPrice.Style.Add("display", "block")

    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "block")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "none")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "none")


    '                    Else
    '                        txtwlAdultPrice.Style.Add("display", "none")
    '                        txtwlChildprice.Style.Add("display", "none")
    '                        txtwlSeniorsPrice.Style.Add("display", "none")
    '                        txtwlUnitPrice.Style.Add("display", "none")

    '                        txtwlChildasadultprice.Style.Add("display", "none")
    '                        txtwlChildasadultSaleValue.Style.Add("display", "none")
    '                        txtChildasadultprice.Style.Add("display", "none")
    '                        lblchildasadultSaleValue.Style.Add("display", "none")

    '                        txtwlAdultSaleValue.Style.Add("display", "none")
    '                        txtwlChildSaleValue.Style.Add("display", "none")
    '                        txtwlSeniorSaleValue.Style.Add("display", "none")
    '                        txtwlUnitSaleValue.Style.Add("display", "none")

    '                        txtAdultPrice.Style.Add("display", "none")
    '                        txtChildprice.Style.Add("display", "none")
    '                        txtSeniorsPrice.Style.Add("display", "none")
    '                        txtUnitPrice.Style.Add("display", "block")


    '                        lblAdultSaleValue.Style.Add("display", "none")
    '                        lblchildSaleValue.Style.Add("display", "none")
    '                        lblSeniorSaleValue.Style.Add("display", "none")
    '                        lblUnitSaleValue.Style.Add("display", "block")
    '                    End If

    '                End If

    '                If dvSDt.Item(0)("comp_cust").ToString() = "1" Then
    '                    chkComplimentaryToCustomer.Checked = True
    '                Else
    '                    chkComplimentaryToCustomer.Checked = False
    '                End If


    '                If Session("sLoginType") = "RO" Then
    '                    dvComplimentaryToCustomer.Visible = True
    '                    ' ''If chkTourOveridePrice.Checked = True Then
    '                    ' ''    txtUnitPrice.ReadOnly = False
    '                    ' ''    txtAdultPrice.ReadOnly = False
    '                    ' ''    txtChildprice.ReadOnly = False
    '                    ' ''    txtChildasadultprice.ReadOnly = False
    '                    ' ''    txtSeniorsPrice.ReadOnly = False
    '                    ' ''    lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")


    '                    ' ''Else
    '                    txtUnitPrice.ReadOnly = True
    '                    txtAdultPrice.ReadOnly = True
    '                    txtChildprice.ReadOnly = True
    '                    txtSeniorsPrice.ReadOnly = True
    '                    txtChildasadultprice.ReadOnly = True
    '                    lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

    '                    ' ''End If

    '                    txtAdultPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfAdult.ClientID, String) + "', '" + CType(txtAdultPrice.ClientID, String) + "' ,'" + CType(lblAdultSaleValue.ClientID, String) + "')")
    '                    txtChildprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchild.ClientID, String) + "', '" + CType(txtChildprice.ClientID, String) + "' ,'" + CType(lblchildSaleValue.ClientID, String) + "')")
    '                    txtChildasadultprice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfchildasadult.ClientID, String) + "', '" + CType(txtChildasadultprice.ClientID, String) + "' ,'" + CType(lblchildasadultSaleValue.ClientID, String) + "')")

    '                    txtSeniorsPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfSeniors.ClientID, String) + "', '" + CType(txtSeniorsPrice.ClientID, String) + "' ,'" + CType(lblSeniorSaleValue.ClientID, String) + "')")
    '                    txtUnitPrice.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")
    '                Else
    '                    dvComplimentaryToCustomer.Visible = False
    '                    txtUnitPrice.ReadOnly = True
    '                    txtAdultPrice.ReadOnly = True
    '                    txtChildprice.ReadOnly = True
    '                    txtSeniorsPrice.ReadOnly = True
    '                    txtChildasadultprice.ReadOnly = True

    '                    lblNoOfUnits.Attributes.Add("onChange", "javascript:CalculateSaleValue('" + CType(lblNoOfUnits.ClientID, String) + "', '" + CType(txtUnitPrice.ClientID, String) + "' ,'" + CType(lblUnitSaleValue.ClientID, String) + "')")

    '                End If


    '                If hdBookingEngineRateType.Value = "1" Then

    '                    dvUnitprice.Style.Add("display", "none")
    '                    dvunitsalevalue.Style.Add("display", "none")
    '                End If
    '                mpTotalprice.Show()
    '            End If
    '        End If


    '    Catch ex As Exception
    '        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    '        objclsUtilities.WriteErrorLog("TourSearch.aspx :: lbwlPrice_Click :: " & ex.Message & ":: " & Session("GlobalUserName"))
    '        ModalPopupDays.Hide()
    '    End Try
    'End Sub

    Private Sub FillCheckBox()
        Dim dtselectedtourfill As DataTable = CType(Session("selectedtourdatatable"), DataTable)
        If Not dtselectedtourfill Is Nothing Then
            If dtselectedtourfill.Rows.Count > 0 Then
                'If dlTourSearchResults.Items.Count > 0 Then
                '    For Each dlItem As DataListItem In dlTourSearchResults.Items
                '        Dim chkSelectTour As CheckBox = CType(dlItem.FindControl("chkSelectTour"), CheckBox)
                '        Dim hdExcCode As HiddenField = CType(dlItem.FindControl("hdExcCode"), HiddenField)
                '        Dim txttourchangedate As TextBox = CType(dlItem.FindControl("txttourchangedate"), TextBox)

                '        For i As Integer = 0 To dtselectedtourfill.Rows.Count - 1
                '            If hdExcCode.Value.Trim = dtselectedtourfill.Rows(i)("exctypcode").ToString Then
                '                txttourchangedate.Text = dtselectedtourfill.Rows(i)("excdate").ToString
                '                chkSelectTour.Checked = True
                '            End If
                '        Next


                '    Next
                'End If
            End If


        End If


    End Sub


    Protected Sub btnConfirmHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmHome.Click
        clearallBookingSessions()
        Response.Redirect("Home.aspx")
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
        Session("sobjBLLTourFreeFormBooking") = Nothing
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
        Session("sobjBLLTourFreeFormBooking") = Nothing
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






    Private Sub BindComboTourDataTable(ByVal strrequestid As String)
        Dim dt As DataTable
        dt = objBLLTourSearch.BindComboTourDataTable(strrequestid)
        If dt.Rows.Count > 0 Then
            Session("selectedCombotourdatatable") = dt
        End If
    End Sub

    ' ''Protected Sub txtSearchTour_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchTour.TextChanged
    ' ''    Try
    ' ''        Dim dsTourSearchResults As New DataSet
    ' ''        dsTourSearchResults = Session("sDSTourSearchResults")
    ' ''        BindTourMainDetailsWithFilter(dsTourSearchResults)

    ' ''    Catch ex As Exception
    ' ''        MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
    ' ''        objclsUtilities.WriteErrorLog("TourSearch.aspx :: txtSearchTour_TextChanged :: " & ex.Message & ":: " & Session("GlobalUserName"))
    ' ''        ModalPopupDays.Hide()
    ' ''    End Try
    ' ''End Sub

    Private Function hdTourType() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub btnMulticost_Click(sender As Object, e As System.EventArgs) Handles btnMulticost.Click

        Dim strTourDate As String = ""

        If Txt_TourType.Text = "NORMAL" Then

            strTourDate = Txt_TourDate.Text

        ElseIf Txt_TourType.Text = "MULTIPLE DATE" Then
            strTourDate = txtTourFromDate.Text

        ElseIf Txt_TourType.Text = "COMBO" Then
            If dlSelectComboDates.Items.Count > 0 Then

                For Each dlItem1 As DataListItem In dlSelectComboDates.Items
                    Dim lblExcComboCode As Label = CType(dlItem1.FindControl("lblExcComboCode"), Label)
                    Dim txtExcComboDate As TextBox = CType(dlItem1.FindControl("txtExcComboDate"), TextBox)
                    If txtExcComboDate.Text <> "" Then
                        strTourDate = txtExcComboDate.Text
                    End If
                    Exit For
                Next
            End If
        End If

        If strTourDate <> "" Then

        End If

        If strTourDate <> "" Then
            Dim strDates As String() = strTourDate.Split("/")
            strTourDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If

        Dim dtMultiCost As DataTable
        dtMultiCost = objclsUtilities.GetDataFromDataTable("exec sp_booking_muticost_price '" + txtTourCustomerCode.Text + "','" + strTourDate + "','" + txtTourStartingFromCode.Text + "','" + ddlTourAdult.SelectedValue + "','" + ddlTourChildren.SelectedValue + "','','" + ddlSeniorCitizen.SelectedValue + "','" + txtTourSourceCountryCode.Text + "','" + Txt_ToursCode.Text + "' ")
        If dtMultiCost.Rows.Count > 0 Then
            gvMultiCost.DataSource = dtMultiCost
            gvMultiCost.DataBind()

            Dim dMTotal As Decimal = 0
            For i As Integer = 0 To dtMultiCost.Rows.Count - 1
                dMTotal = dMTotal + dtMultiCost.Rows(i)("totalcost").ToString
            Next
            Txt_CostPriceUnits.Text = dMTotal.ToString
            Txt_CostSaleValueUnits.Text = dMTotal.ToString
            Txt_TotalCostValue.Text = dMTotal.ToString
        Else
            gvMultiCost.DataBind()
            MessageBox.ShowMessage(Page, MessageType.Warning, "Multicost rate is not available. Please inform admin to enter multicost or Enter as single as cost.")

        End If

        Session("sdtMultiCost") = dtMultiCost


    End Sub

    Protected Sub gvMultiCost_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMultiCost.RowDataBound

        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            Dim lblAdult As Label = CType(e.Row.FindControl("lblAdult"), Label)
            Dim txtPeradult As TextBox = CType(e.Row.FindControl("txtPeradult"), TextBox)
            Dim lblAdultCost As TextBox = CType(e.Row.FindControl("lblAdultCost"), TextBox)


            Dim lblChild As Label = CType(e.Row.FindControl("lblChild"), Label)
            Dim txtPerchild As TextBox = CType(e.Row.FindControl("txtPerchild"), TextBox)
            Dim lblchildcost As TextBox = CType(e.Row.FindControl("lblchildcost"), TextBox)


            Dim lblnoofseniors As Label = CType(e.Row.FindControl("lblnoofseniors"), Label)
            Dim txtPersenior As TextBox = CType(e.Row.FindControl("txtPersenior"), TextBox)
            Dim lblSeniorCost As TextBox = CType(e.Row.FindControl("lblSeniorCost"), TextBox)

            Dim txtUnitcost As TextBox = CType(e.Row.FindControl("txtUnitcost"), TextBox)

            Dim lbltotalcost As TextBox = CType(e.Row.FindControl("lbltotalcost"), TextBox)

            Dim hdAdultCost As HiddenField = CType(e.Row.FindControl("hdAdultCost"), HiddenField)
            Dim hdChildcost As HiddenField = CType(e.Row.FindControl("hdChildcost"), HiddenField)
            Dim hdSeniorCost As HiddenField = CType(e.Row.FindControl("hdSeniorCost"), HiddenField)
            Dim hdTotalcost As HiddenField = CType(e.Row.FindControl("hdTotalcost"), HiddenField)



            txtPeradult.Attributes.Add("onchange", "javascript:findMultiCostTotal('" + lblAdult.ClientID + "', '" + txtPeradult.ClientID + "','" + lblAdultCost.ClientID + "','" + lblChild.ClientID + "', '" + txtPerchild.ClientID + "','" + lblchildcost.ClientID + "','" + lblnoofseniors.ClientID + "', '" + txtPersenior.ClientID + "','" + lblSeniorCost.ClientID + "', '" + txtUnitcost.ClientID + "','" + lbltotalcost.ClientID + "','" + hdAdultCost.ClientID + "', '" + hdChildcost.ClientID + "','" + hdSeniorCost.ClientID + "','" + hdTotalcost.ClientID + "')")
            txtPerchild.Attributes.Add("onchange", "javascript:findMultiCostTotal('" + lblAdult.ClientID + "', '" + txtPeradult.ClientID + "','" + lblAdultCost.ClientID + "','" + lblChild.ClientID + "', '" + txtPerchild.ClientID + "','" + lblchildcost.ClientID + "','" + lblnoofseniors.ClientID + "', '" + txtPersenior.ClientID + "','" + lblSeniorCost.ClientID + "', '" + txtUnitcost.ClientID + "','" + lbltotalcost.ClientID + "','" + hdAdultCost.ClientID + "', '" + hdChildcost.ClientID + "','" + hdSeniorCost.ClientID + "','" + hdTotalcost.ClientID + "')")
            txtPersenior.Attributes.Add("onchange", "javascript:findMultiCostTotal('" + lblAdult.ClientID + "', '" + txtPeradult.ClientID + "','" + lblAdultCost.ClientID + "','" + lblChild.ClientID + "', '" + txtPerchild.ClientID + "','" + lblchildcost.ClientID + "','" + lblnoofseniors.ClientID + "', '" + txtPersenior.ClientID + "','" + lblSeniorCost.ClientID + "', '" + txtUnitcost.ClientID + "','" + lbltotalcost.ClientID + "','" + hdAdultCost.ClientID + "', '" + hdChildcost.ClientID + "','" + hdSeniorCost.ClientID + "','" + hdTotalcost.ClientID + "')")
            txtUnitcost.Attributes.Add("onchange", "javascript:findMultiCostTotal('" + lblAdult.ClientID + "', '" + txtPeradult.ClientID + "','" + lblAdultCost.ClientID + "','" + lblChild.ClientID + "', '" + txtPerchild.ClientID + "','" + lblchildcost.ClientID + "','" + lblnoofseniors.ClientID + "', '" + txtPersenior.ClientID + "','" + lblSeniorCost.ClientID + "', '" + txtUnitcost.ClientID + "','" + lbltotalcost.ClientID + "','" + hdAdultCost.ClientID + "', '" + hdChildcost.ClientID + "','" + hdSeniorCost.ClientID + "','" + hdTotalcost.ClientID + "')")
            ' txtUnitcost.Attributes.Add("onchange", "javascript:findMultiCostTotal('" + lblAdult.ClientID + "', '" + txtPeradult.ClientID + "','" + lblAdultCost.ClientID + "','" + lblChild.ClientID + "', '" + txtPerchild.ClientID + "','" + lblchildcost.ClientID + "','" + lblnoofseniors.ClientID + "', '" + txtPersenior.ClientID + "','" + lblSeniorCost.ClientID + "', '" + txtUnitcost.ClientID + "','" + lbltotalcost.ClientID + "','" + hdAdultCost.ClientID + "', '" + hdChildcost.ClientID + "','" + hdSeniorCost.ClientID + ")")

        End If
    End Sub
End Class
