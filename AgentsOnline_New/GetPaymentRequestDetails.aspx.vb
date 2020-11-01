Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Data

Partial Class GetPaymentRequestDetails
    Inherits System.Web.UI.Page
    Dim objclsUtilities As New clsUtilities
    Dim objclsUtils As New clsUtils
    Dim ObjBLLPaymentParameters As New BLLPaymentParameters
    Dim ObjBillingDetails As New BillingDetails
    Dim ObjCustomerDetails As New CustomerDetails
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim RequestId As String = String.Empty
                Dim strSqlQry As String = ""
                Dim myDS As New DataSet
                If Not Request.QueryString("RequestId") Is Nothing Then
                    RequestId = Request.QueryString("RequestId")
                End If
                strSqlQry = "execute sp_get_paymentRequestDetails '" & RequestId & "'"
                Dim SqlConn As New SqlConnection
                Dim myDataAdapter As New SqlDataAdapter
                SqlConn = clsDBConnect.dbConnectionnew("strDBConnection")            'Open connection
                myDataAdapter = New SqlDataAdapter(strSqlQry, SqlConn)
                myDataAdapter.Fill(myDS)
                Dim dt As DataTable = myDS.Tables(0)
                For Each row As DataRow In dt.Rows
                    ObjBLLPaymentParameters.TemporaryBookingID = IIf(IsDBNull(row.Item("requestId")), "", row.Item("requestId"))
                    ObjBLLPaymentParameters.PaymentAmount = IIf(IsDBNull(row.Item("payAmount")), "", row.Item("payAmount"))
                    ObjBLLPaymentParameters.PaymentCurrency = IIf(IsDBNull(row.Item("currency")), "", row.Item("currency"))
                    ObjBillingDetails.Address1 = IIf(IsDBNull(row.Item("address1")), "", row.Item("address1"))
                    ObjBillingDetails.Address2 = IIf(IsDBNull(row.Item("address2")), "", row.Item("address2"))
                    ObjBillingDetails.Address3 = IIf(IsDBNull(row.Item("address3")), "", row.Item("address3"))
                    ObjBillingDetails.Email = IIf(IsDBNull(row.Item("email")), "", row.Item("email"))
                    ObjBillingDetails.TelephoneNO = IIf(IsDBNull(row.Item("telephoneNo")), "", row.Item("telephoneNo"))
                    ObjCustomerDetails.AgentCode = IIf(IsDBNull(row.Item("agentcode")), "", row.Item("agentcode"))
                    ObjCustomerDetails.AgentName = IIf(IsDBNull(row.Item("agentname")), "", row.Item("agentname"))
                    ObjCustomerDetails.Salesperson = IIf(IsDBNull(row.Item("salesperson")), "", row.Item("salesperson"))
                    ObjCustomerDetails.LeadGuestName = IIf(IsDBNull(row.Item("leadGuestName")), "", row.Item("leadGuestName"))
                Next row
                ObjBLLPaymentParameters.BillingDetails = ObjBillingDetails
                ObjBLLPaymentParameters.CustomerDetails = ObjCustomerDetails
                Dim json As New JavaScriptSerializer
                Dim paymentdetails As String = json.Serialize(ObjBLLPaymentParameters)
                'Dim paymentdetails As String = DataSetToJSON(myDS)
                Response.Write(paymentdetails)
            Catch ex As Exception
                MessageBox.ShowMessage(Page, MessageType.Errors, "Error Description " + ex.Message.Replace("'", " "))
                objclsUtilities.WriteErrorLog("GuestPageNew.aspx :: Page_Load :: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
            End Try

        End If
    End Sub
    Function DataSetToJSON(ds As DataSet) As String
        Dim dict As New Dictionary(Of String, Object)

        For Each dt As DataTable In ds.Tables
            Dim arr(dt.Rows.Count) As Object

            For i As Integer = 0 To dt.Rows.Count - 1
                arr(i) = dt.Rows(i).ItemArray
            Next

            dict.Add(dt.TableName, arr)
        Next

        Dim json As New JavaScriptSerializer
        Return json.Serialize(dict)
    End Function
End Class
