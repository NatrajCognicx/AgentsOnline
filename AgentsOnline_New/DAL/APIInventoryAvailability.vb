Imports Microsoft.VisualBasic

Public Class APIInventoryAvailability
    Public Class AvailabilityRequest
        Public Property SourceCountry As String
        Public Property AgentID As String
        Public Property BookingID As String
        Public Property ELineNo As Integer
        Public Property ExcursionCode As String
        Public Property ExcursionFromDate As String
        Public Property ExcursionToDate As String
        Public Property InventoryID As Integer
    End Class

    Public Class TimeSlot
        Public Property PRODUCT_ID As String
        Public Property [DATE] As DateTime
        Public Property TIMESLOT_FROM As String
        Public Property TIMESLOT_TO As String
        Public Property AVAILABILITY As Integer
    End Class

    Public Class AvailabilityResponse
        Public Property Available As List(Of TimeSlot)
        Public Property STATUS As Integer
        Public Property MESSAGE As String
    End Class
End Class
