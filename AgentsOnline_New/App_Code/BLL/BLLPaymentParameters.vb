Imports Microsoft.VisualBasic

Public Class BLLPaymentParameters

    Public Property TemporaryBookingID As String
    Public Property PaymentAmount As String
    Public Property PaymentCurrency As String
    Public Property BillingDetails As BillingDetails
    Public Property CustomerDetails As CustomerDetails

End Class
Public Class BillingDetails

    Public Property Address1 As String
    Public Property Address2 As String
    Public Property Address3 As String
    Public Property Email As String
    Public Property TelephoneNO As String

End Class
Public Class CustomerDetails

    Public Property AgentCode As String
    Public Property AgentName As String
    Public Property Salesperson As String
    Public Property LeadGuestName As String
End Class

