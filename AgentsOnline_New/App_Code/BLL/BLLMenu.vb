Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLMenu

    Private _MenuId As String
    Private _MenuDesc As String
    Private _ParentId As String
    Private _Menu_Type As String
    Private _Menu_Status As String
    Private _PageName As String
    Private _ImagePath As String

    Dim objDALMenu As New DALMenu

    Public Property MenuId As String
        Get
            Return _MenuId
        End Get
        Set(value As String)
            _MenuId = value
        End Set
    End Property

    Public Property MenuDesc As String
        Get
            Return _MenuDesc
        End Get
        Set(value As String)
            _MenuDesc = value
        End Set
    End Property

    Public Property ParentId As String
        Get
            Return _ParentId
        End Get
        Set(value As String)
            _ParentId = value
        End Set
    End Property

    Public Property Menu_Type As String
        Get
            Return _Menu_Type
        End Get
        Set(value As String)
            _Menu_Type = value
        End Set
    End Property


    Public Property Menu_Status As String
        Get
            Return _Menu_Status
        End Get
        Set(value As String)
            _Menu_Status = value
        End Set
    End Property

    Public Property PageName As String
        Get
            Return _PageName
        End Get
        Set(value As String)
            _PageName = value
        End Set
    End Property

    Public Property ImagePath As String
        Get
            Return _ImagePath
        End Get
        Set(value As String)
            _ImagePath = value
        End Set
    End Property

    Function Getmenus(strLoginType As String, strAgentCode As String, strSubUserCode As String, Optional ByVal strMenuType As String = "1") As DataTable
        Dim dtMenus As DataTable
        dtMenus = objDALMenu.Getmenus(strLoginType, strAgentCode, strSubUserCode, strMenuType)
        Return dtMenus
    End Function
    Function GetMenusReturnAstring(strLoginType As String, strAgentCode As String, strSubUserCode As String, Optional ByVal strMenuType As String = "1", Optional ByVal strSub As String = "1") As String
        Dim dtMenus As DataTable
        dtMenus = objDALMenu.Getmenus(strLoginType, strAgentCode, strSubUserCode, strMenuType)

        Dim strMenuMobHtml As New StringBuilder
        Dim strMenuHtml As New StringBuilder

        '----------------------
        'Changed by mohamed on 01/08/2018
        Dim lsRequestID As String = "", lsBookingMode As String = "", lsFreeFormMenuId As String = ""
        If Not HttpContext.Current.Session("sEditRequestId") Is Nothing Then
            lsRequestID = HttpContext.Current.Session("sEditRequestId")
        ElseIf Not HttpContext.Current.Session("sRequestId") Is Nothing Then
            lsRequestID = HttpContext.Current.Session("sRequestId")
        End If
        Dim dt As New DataTable
        If lsRequestID <> "" Then
            dt = objDALMenu.GetDataFromDataTable("execute sp_get_bookingtype '" & lsRequestID & "', 0")
            If dt.Rows.Count > 0 Then
                lsBookingMode = dt.Rows(0)("bookingtype")
            End If
        End If
        dt = objDALMenu.GetDataFromDataTable("select option_selected from reservation_parameters where param_id=2044")
        If dt.Rows.Count > 0 Then
            lsFreeFormMenuId = dt.Rows(0)("option_selected")
        End If
        '----------------------

        Dim dvMainMenu As DataView = New DataView(dtMenus)
        If strLoginType <> "RO" Then
            dvMainMenu.RowFilter = "parentid=1 and menuid<>" & lsFreeFormMenuId & " and menuid<>23 "
        Else
            dvMainMenu.RowFilter = "parentid=1"
        End If

        Dim dvSubMenu As DataView
        If dvMainMenu.Count > 0 Then
            'strMenuMobHtml.Append(" <a href='#'><img id='lcoloumbuslogo' alt='' src='img/LoginLogoSmall.png' /></a> <nav><ul> ")
            'strMenuHtml.Append(" <a href='#'><img id='lcoloumbuslogo' alt='' src='img/LoginLogoSmall.png' /></a> <nav  class='header-nav'><ul>") '

            strMenuMobHtml.Append(" <nav><ul>")
            strMenuHtml.Append(" <nav  class='header-nav'><ul>") '

            For i As Integer = 0 To dvMainMenu.Count - 1

                '13/12
                ''changed by mohamed on 01/08/2018
                'If lsBookingMode.ToUpper.Trim = "NORMAL" And (dvMainMenu.Item(i)("menuid") = lsFreeFormMenuId Or dvMainMenu.Item(i)("parentid") = lsFreeFormMenuId) Then
                '    GoTo nextMenuID
                'ElseIf lsBookingMode.ToUpper.Trim = "FREEFORM" And dvMainMenu.Item(i)("menudesc").ToString = "Home" Then
                '    GoTo nextMenuID
                'End If

                'changed by mohamed on 12/08/2018
                If HttpContext.Current.Session("sFinalBooked") IsNot Nothing Then
                    If (dvMainMenu.Item(i)("menuid") = lsFreeFormMenuId Or dvMainMenu.Item(i)("parentid") = lsFreeFormMenuId) And HttpContext.Current.Session("sFinalBooked") = "1" Then

                        GoTo nextMenuID
                    End If
                End If

                strMenuMobHtml.Append("<li><a href='" & dvMainMenu.Item(i)("pagename").ToString & "'>" & dvMainMenu.Item(i)("menudesc").ToString & "</a></li>")
                If dvMainMenu.Item(i)("pagename").ToString = "" Then
                    strMenuHtml.Append("<li><a style='color:#2c2c2c;'>" & dvMainMenu.Item(i)("menudesc").ToString & "</a>")

                    dvSubMenu = New DataView(dtMenus)
                    dvSubMenu.RowFilter = "parentid='" & dvMainMenu.Item(i)("menuid").ToString & "'"
                    If dvSubMenu.Count > 0 Then
                        strMenuHtml.Append("<ul>")
                        For k As Integer = 0 To dvSubMenu.Count - 1
                            strMenuHtml.Append("<li><div> <a href='" & dvSubMenu.Item(k)("pagename").ToString & "'>" & dvSubMenu.Item(k)("menudesc").ToString & "</a> </div></li>")
                        Next
                        strMenuHtml.Append("</ul>")
                    End If
                    strMenuHtml.Append("</li>")
                    '   strMenuHtml.Append("<ul><li
                Else
                    If dvMainMenu.Item(i)("menudesc").ToString = "Home" Then
                        If Not HttpContext.Current.Session("sEditRequestId") Is Nothing Then
                            Dim str As String = "" 'changed by mohamed on 24/09/2018 as it was throwing error
                            If HttpContext.Current.Session("sRequestId") IsNot Nothing Then
                                str = HttpContext.Current.Session("sRequestId").ToString.Trim
                            End If
                            strMenuHtml.Append("<li><div onclick=fnConfirmHome('" + str + "')><a href='#'>  " & dvMainMenu.Item(i)("menudesc").ToString & " </a>" & "</div></li>")
                        Else
                            If Not HttpContext.Current.Session("sRequestId") Is Nothing Then
                                Dim str As String = "New"
                                strMenuHtml.Append("<li><div onclick=fnConfirmHome('" + str + "')><a href='#'>  " & dvMainMenu.Item(i)("menudesc").ToString & " </a>" & "</div></li>")
                            Else
                                strMenuHtml.Append("<li><div> <a href='" & dvMainMenu.Item(i)("pagename").ToString & "'>" & dvMainMenu.Item(i)("menudesc").ToString & "</a> </div></li>")
                            End If

                        End If

                    Else
                        strMenuHtml.Append("<li><a href='" & dvMainMenu.Item(i)("pagename").ToString & "'>" & dvMainMenu.Item(i)("menudesc").ToString & "</a> ")

                        dvSubMenu = New DataView(dtMenus)
                        dvSubMenu.RowFilter = "parentid='" & dvMainMenu.Item(i)("menuid").ToString & "'"
                        If dvSubMenu.Count > 0 Then
                            strMenuHtml.Append("<ul>")
                            For k As Integer = 0 To dvSubMenu.Count - 1
                                strMenuHtml.Append("<li><div> <a href='" & dvSubMenu.Item(k)("pagename").ToString & "'>" & dvSubMenu.Item(k)("menudesc").ToString & "</a> </div></li>")
                            Next
                            strMenuHtml.Append("</ul>")
                        End If
                        strMenuHtml.Append("</li>")
                        '   strMenuHtml.Append("<ul><li><a href='" & dvMainMenu.Item(i)("pagename").ToString & "'>" & dvMainMenu.Item(i)("menudesc").ToString & "</a><li></ul>")
                    End If

                End If
nextMenuID:     'changed by mohamed on 01/08/2018
            Next
            strMenuHtml.Append("</ul></nav>")
            strMenuMobHtml.Append("</ul></nav>")
        End If

        Return strMenuHtml.ToString
    End Function


End Class
