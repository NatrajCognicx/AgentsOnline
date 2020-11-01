Imports Microsoft.VisualBasic

Public Class APIHotelCancellationRequest
    Public Class Login
        Public Property country As String
        Public Property lang As String
        Public Property password As String
        Public Property user As String
    End Class

    Public Class HotelCancellationRequest
        Public Property locator As String
        Public Property netPenaltyFee As Decimal
        Public Property reason As String
        Public Property login As Login
    End Class
End Class
