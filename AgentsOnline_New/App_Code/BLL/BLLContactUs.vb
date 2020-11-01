Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLContactUs

    Function GetContactDetails(strAgentCode As String) As DataTable
        Dim objDALContactUs As New DALContactUs()
        Dim dt As DataTable
        dt = objDALContactUs.GetContactDetails(strAgentCode)
        Return dt
    End Function

End Class
