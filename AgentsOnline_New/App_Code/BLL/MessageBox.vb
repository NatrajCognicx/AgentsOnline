Imports Microsoft.VisualBasic

Public Class MessageBox
    Public Shared Sub ShowMessage(page As Page, strMessageType As String, strMessage As String)
        If strMessageType = MessageType.Success Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('Result Status','" & strMessage & "','success');", True)
        ElseIf strMessageType = MessageType.Warning Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('Alert Message','" & strMessage & "','warning');", True)
        ElseIf strMessageType = MessageType.Info Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('Information','" & strMessage & "','info');", True)
        ElseIf strMessageType = MessageType.Errors Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('Error','" & strMessage & "','error');", True)
        ElseIf strMessageType = MessageType.Confirm Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('Confirmation','" & strMessage & "','prompt');", True)
        ElseIf strMessageType = MessageType.Alert Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('Alert Message','" & strMessage & "','warning');", True)
        ElseIf strMessageType = MessageType.Important Then
            ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "javascript:showDialog('IMPORTANT NOTICE','" & strMessage & "','warning');", True)
        End If
    End Sub

End Class
