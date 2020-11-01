Imports Microsoft.VisualBasic
Imports System.Data

Public Class DALMenu
    Dim objclsUtilities As New clsUtilities

    Function Getmenus(strLoginType As String, strAgentCode As String, strSubUserCode As String, Optional ByVal strMenuType As String = "1") As DataTable
        Dim objDataTable As DataTable
        Dim strQuery As String = ""
        If strLoginType = "RO" Then
            strQuery = "select menuid,menudesc,parentid,menu_type,menu_status,pagename,imagepath, (select menudesc from agent_menumaster p where p.menuid=agent_menumaster.parentid)parentname from agent_menumaster where active=1 and menu_type=" & strMenuType & " and parentid<>0  and parentid<>2"
        Else
            If strSubUserCode = "" Then
                '  strQuery = "select menuid,menudesc,parentid,menu_type,menu_status,pagename,imagepath, (select menudesc from agent_menumaster p where p.menuid=agent_menumaster.parentid)parentname from agent_menumaster where active=1 and menu_type=" & strMenuType & " and menuid in (select menuid from agents_subuser_rights where agentcode='" & strAgentCode & "' and agentsubcode='' and active=1)"
                strQuery = "select menuid,menudesc,parentid,menu_type,menu_status,pagename,imagepath, (select menudesc from agent_menumaster p where p.menuid=agent_menumaster.parentid)parentname from agent_menumaster where active=1 and menu_type=" & strMenuType & " and parentid<>0  and parentid<>2"
            Else
                strQuery = "select menuid,menudesc,parentid,menu_type,menu_status,pagename,imagepath, (select menudesc from agent_menumaster p where p.menuid=agent_menumaster.parentid)parentname from agent_menumaster where active=1 and menu_type=" & strMenuType & " and menuid in (select menuid from agents_subuser_rights where agentcode='" & strAgentCode & "' and agentsubcode='" & strSubUserCode & "' and active=1)"
            End If
        End If
        objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Return objDataTable
    End Function


    Function GetDataFromDataTable(ByVal strSqlQuery As String) As DataTable
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function
End Class
