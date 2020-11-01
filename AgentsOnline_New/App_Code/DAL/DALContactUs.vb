Imports Microsoft.VisualBasic
Imports System.Data

Public Class DALContactUs
    Dim objclsUtilities As New clsUtilities
    Function GetContactDetails(strAgentCode As String) As Data.DataTable
        Try
            Dim objDataTable As DataTable         
            Dim strQuery As String = "select U.UserName,userdesign,'Email: '+ usemail usemail,'Phone: ' +ustel ustel,UserImage from agentmast(nolock) A,UserMaster(nolock) U where A.agentcode='" & strAgentCode & "' and U.UserCode=A.salescontact "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALContactUs:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

End Class
