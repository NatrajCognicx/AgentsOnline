Imports Microsoft.VisualBasic

Public Class APIHotelDetailsRequest
    Public Class Board
        Public Property id As String
    End Class

    Public Class Room
        Public Property id As String
    End Class

    Public Class Distribution
        Public Property numberAdults As Integer
        Public Property numberChildren As Integer
        Public Property numberExtraBeds As Integer
        'Public Property childrenAges As Integer()
        Public Property childrenAges As New List(Of Integer)
        Public Property numberRooms As Integer
        Public Property board As New Board
        Public Property room As New Room
        Public Property searchcode As String
        Public Property roomcode As String
    End Class

    Public Class Login
        Public Property country As String
        Public Property lang As String
        Public Property password As String
        Public Property user As String
    End Class

    Public Class HotelDetailsRequest
        Public Property arrivalDate As String 'DateTime
        Public Property departureDate As String 'DateTime
        ' Public Property distribution As Distribution()
        Public distribution As New List(Of Distribution)
        Public Property hotel As Integer
        Public Property searchcode As String
        Public Property login As New Login
    End Class

End Class
