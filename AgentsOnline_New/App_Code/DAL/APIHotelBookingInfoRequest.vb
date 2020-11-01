Imports Microsoft.VisualBasic

Public Class APIHotelBookingInfoRequest

    Public Class Login
        Public Property country As String
        Public Property lang As String
        Public Property password As String
        Public Property user As String
    End Class

    Public Class HotelBookingInfoRequest
        Public Property locator As String
        Public Property newCancellation As Boolean
        Public Property login As Login
    End Class

End Class
