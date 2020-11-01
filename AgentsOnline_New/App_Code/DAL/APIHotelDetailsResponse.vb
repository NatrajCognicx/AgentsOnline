Imports Microsoft.VisualBasic

Public Class APIHotelDetailsResponse
    Public Class Category
        Public Property id As Integer
        Public Property name As String
    End Class

    Public Class City
        Public Property id As Integer
        Public Property name As String
    End Class

    Public Class Type
        Public Property id As Integer
        Public Property name As String
    End Class

    Public Class Hotel
        Public Property id As Integer
        Public Property name As String
        Public Property address As String
        Public Property category As Category
        Public Property city As City
        Public Property district As Object
        Public Property email As String
        Public Property fax As String
        Public Property phone As String
        Public Property touristZone As Object
        Public Property photo As String
        Public Property posLatitude As String
        Public Property posLongitude As String
        Public Property type As Type
        Public Property updated As Object
    End Class

    Public Class Board
        Public Property id As Integer
        Public Property name As String
    End Class

    Public Class Room
        Public Property id As String
        Public Property name As String
    End Class

    Public Class Distribution
        Public Property numberAdults As Integer
        Public Property numberChildren As Integer
        Public Property numberExtraBeds As Integer
        ' Public Property childrenAges As Integer()
        Public Property childrenAges As New List(Of Integer)
        Public Property numberRooms As Integer
        Public Property board As Board
        Public Property room As Room
        Public Property arrivalDate As DateTime
        Public Property departureDate As DateTime
        Public Property totalPrice As Decimal
        Public Property netPrice As Decimal
        Public Property offers As String
        Public Property searchcode As String
        Public Property roomcode As String
    End Class

    Public Class CancelCondition
        Public Property dateTime As DateTime
        Public Property totalPenaltyFee As Decimal
        Public Property netPenaltyFee As Decimal
    End Class

    Public Class Result
        Public Property id As String
        Public Property description As String
        Public Property nights As Integer
        Public Property totalPrice As Decimal
        Public Property netPrice As Decimal
        Public Property earlyBooking As Boolean
        Public Property sellingPriceMandatory As Boolean
        Public Property creationDate As Object
        Public Property cancellationDate As Object
        Public Property arrivalDate As DateTime
        Public Property departureDate As DateTime
        Public Property reference As String
        Public Property customer As String
        Public Property currency As String
        Public Property onRequest As Boolean
        Public Property noRefundable As Boolean
        Public Property hotel As Hotel
        ' Public Property distributions As Distribution()
        Public distributions As New List(Of Distribution)
        Public Property conditions As String
        Public Property totalPenaltyFee As Object
        Public Property netPenaltyFee As Object
        'Public Property cancelConditions As CancelCondition()
        Public cancelConditions As New List(Of CancelCondition)
    End Class

    Public Class HotelDetailsResponse
        Public Property type As String
        Public Property result As Result
    End Class

    Public Class ResponseStatus
        Public Property StatusCode As String
        Public Property StatusDescription As String
    End Class

    Public Class ErrorResponseStatus
        Public Property code As String
        Public Property message As String
    End Class
End Class
