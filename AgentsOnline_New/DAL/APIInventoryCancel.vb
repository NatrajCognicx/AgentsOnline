Imports Microsoft.VisualBasic

Public Class APIInventoryCancel
    'Public Class CancelBooking
    '    Public Property AgentID As String
    '    Public Property ConfirmationNo As String
    '    Public Property ReasonForCancellation As String
    '    Public Property NoOfAdults As Integer
    '    Public Property NoOfChildren As Integer
    '    Public Property NoOfSeniorCitizens As Integer
    '    Public Property NoOfUnits As Integer
    'End Class

    Public Class TimeSlot
        Public Property Time As String
        Public Property NoOfAdults As Integer
        Public Property NoOfChildren As Integer
        Public Property NoOfSeniorCitizens As Integer
        Public Property NoOfUnits As Integer
    End Class

    Public Class CancelBooking
        Public Property AgentID As String
        Public Property ConfirmationNo As String
        Public Property ReasonForCancellation As String
        ' Public Property TimeSlots As TimeSlot()
        Public Property TimeSlots As New List(Of TimeSlot)
    End Class

    Public Class CancelBookingResponse
        Public Property Message As String
        Public Property CancellationNo As String
    End Class
End Class
