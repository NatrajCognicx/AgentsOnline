Imports Microsoft.VisualBasic

Public Class APIInventoryConfirm
    Public Class SaveAllotment
        Public Property AgentID As String
        Public Property SourceCountry As String
        Public Property BookingID As String
        Public Property ELineNo As Integer
        Public Property ExcursionCode As String
        Public Property ExcursionDate As String
        Public Property TimeSlots As New List(Of TimeSlot)
        Public Property InventoryType As String

        Public Property InventoryID As Integer
    End Class
    Public Class TimeSlot
        Public Property Time As String
        Public Property NoOfAdults As Integer
        Public Property NoOfChildren As Integer
        Public Property NoOfSeniorCitizens As Integer
        Public Property NoOfUnits As Integer
    End Class

    Public Class SaveAllotmentResponse
        Public Property Message As String
        Public Property TempConfirmationNo As String
    End Class

    Public Class SubmitBooking
        Public Property AgentID As String
        Public Property BookingID As String
        Public Property ELineNo As Integer
        Public Property TempConfirmationNo As String
        Public Property InventoryID As String
    End Class

    Public Class SubmitBookingResponse
        Public Property Message As String
        Public Property ConfirmationNo As String
    End Class
    Public Class FinalSaveRequest
        Public Property ColumbusBookingID As String
        Public Property ELineNo As Integer
        Public Property ColumbusTempBookingID As String
        Public Property InventoryConfirmationNo As String
    End Class
    Public Class FinalSaveResponse
        Public Property Message As String
        Public Property Status As Integer
    End Class
End Class
