Imports Microsoft.VisualBasic

Public Class APIHotelSearchResponse
    Public Class Category
        Public Property id As String
        Public Property name As String
    End Class

    Public Class City
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Type
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Hotel
        Public Property id As String
        Public Property name As String
        Public Property category As Category
        Public Property city As City
        Public Property photo As String
        Public Property posLatitude As Double
        Public Property posLongitude As Double
        Public Property type As Type
    End Class

    Public Class Board
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Room
        Public Property id As String
        Public Property name As String
        Public Property description As String
    End Class

    Public Class Distribution
        Public Property numberAdults As Integer
        Public Property numberChildren As Integer
        Public Property childrenAges As Integer()
        Public Property numberRooms As Integer
        Public Property board As Board
        Public Property room As Room
        Public Property numberExtraBeds As Integer
        Public Property roomcode As String
        Public Property arrivalDate As DateTime
        Public Property departureDate As DateTime
        Public Property totalPrice As Double
        Public Property netPrice As Double
        Public Property offers As Object
    End Class

    Public Class Accomodation
        Public Property id As String
        Public Property description As String
        Public Property nights As Integer
        Public Property totalPrice As Double
        Public Property netPrice As Double
        Public Property earlyBooking As Boolean
        Public Property sellingPriceMandatory As Boolean
        Public Property creationDate As DateTime
        Public Property cancellationDate As Object
        Public Property arrivalDate As DateTime
        Public Property departureDate As DateTime
        Public Property reference As Object
        Public Property currency As String
        Public Property onRequest As Boolean
        Public Property noRefundable As Boolean
        Public Property hotel As Object
        Public Property distributions As Distribution()
    End Class

    Public Class Result
        Public Property hotel As New Hotel
        Public Property accomodations As New List(Of Accomodation) 
    End Class

    Public Class HotelSearchResponse
        Public Property type As String
        Public Property results As New List(Of Result)
    End Class


End Class
