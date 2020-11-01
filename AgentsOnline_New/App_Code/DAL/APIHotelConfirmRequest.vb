Imports Microsoft.VisualBasic

Public Class APIHotelConfirmRequest

    Public Class Board
        Public Property id As Integer
    End Class

    Public Class Room
        Public Property id As String
    End Class

    Public Class PaxNames
        Public Property name As String
        Public Property surname As String
        Public Property age As Integer?
    End Class

    Public Class Distribution
        Public Property numberAdults As Integer
        Public Property numberChildren As Integer
        Public Property numberExtraBeds As Integer
        Public Property childrenAges As List(Of Integer)
        Public Property paxNames As List(Of PaxNames)
        Public Property numberRooms As Integer
        Public Property board As Board
        Public Property room As Room
        Public Property searchcode As String
        Public Property roomcode As String
    End Class

    Public Class Login
        Public Property country As String
        Public Property lang As String
        Public Property password As String
        Public Property user As String
    End Class

    Public Class HotelConfirmRequest
        Public Property arrivalDate As String
        Public Property departureDate As String
        Public Property distribution As List(Of Distribution)
        Public Property hotel As Integer
        Public Property searchcode As String
        Public Property customer As String
        Public Property reference As String
        Public Property observations As String
        Public Property netPrice As Decimal
        Public Property login As Login
    End Class
End Class
