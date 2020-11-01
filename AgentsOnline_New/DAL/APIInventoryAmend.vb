Imports Microsoft.VisualBasic

Public Class APIInventoryAmend
    Public Class TimeSlot
        Public Property Time As String
        Public Property NoOfAdults As Integer
        Public Property NoOfChildren As Integer
        Public Property NoOfSeniorCitizens As Integer
        Public Property NoOfUnits As Integer
    End Class

    Public Class AmendRequest
        Public Property BookingID As String
        Public Property ELineNo As Integer
        Public Property ConfirmationNo As String
        Public Property TimeSlots As New List(Of TimeSlot)
    End Class
    Public Class EditRequest
        Public Property BookingID As String
        Public Property ELineNo As Integer
        Public Property TempConfirmationNo As String
        Public Property TimeSlots As New List(Of TimeSlot)
    End Class
    Public Class AmendResponse
        Public Property Message As String
        Public Property TempConfirmationNo As String
    End Class
End Class
