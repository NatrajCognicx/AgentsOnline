Imports Microsoft.VisualBasic

Public Class APIHotelSearchRequest1

    Public arrivalDate As String   'Yes		datetime
    Public departureDate As String 'Yes		datetime
    Public Property distribution As List(Of distributions)
    ' Dim distribution As distributions = New distributions()
    Dim hotel As hotels = New hotels()

    Public Property onRequest As Boolean
    Public Property priceDetails As Boolean
    Public Property ratePlanCount As Integer
    Public Property excludeStaticDetails As Boolean

    Dim login As loginDetail = New loginDetail()

    Public Class distributions
        Public Property numberAdults As Integer
        Public Property numberChildren As Integer
        Public Property childrenAges As String
        Public Property numberRooms As Integer
        Dim board As board = New board()
        Dim room As room = New room()
    End Class

    Public Class board
        Public Property id As Integer
    End Class
    Public Class room
        Public Property id As String
    End Class

    Public Class hotels
        Public Property hotel As Integer
        Public Property region As Integer
        Public Property city As Integer
        Public Property touristZone As Integer
        Public Property category As Integer
    End Class


    Public Class loginDetail
        Public Property country As String
        Public Property lang As String
        Public Property user As String
        Public Property password As String
        Public Property residence As String
        Public Property timestampId As String
    End Class
End Class
