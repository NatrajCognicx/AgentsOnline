Imports Microsoft.VisualBasic

Public Class BLLHotelConfirm
    Dim _HBRequestId As String = ""
    Dim _HBRlineNo As String = ""
    Dim _HBRoomNo As String = ""
    Dim _HBRoomType As String = ""
    Dim _HBConfirmationNo As String = ""
    Dim _HBConfirmBy As String = ""
    Dim _HBConfirmationDate As String = ""

    Public Property HBRequestID As String
        Get
            Return _HBRequestId
        End Get
        Set(ByVal value As String)
            _HBRequestId = value
        End Set
    End Property
    Public Property HBRlineNo As String
        Get
            Return _HBRlineNo
        End Get
        Set(ByVal value As String)
            _HBRlineNo = value
        End Set
    End Property
    Public Property HBRoomNo As String
        Get
            Return _HBRoomNo
        End Get
        Set(ByVal value As String)
            _HBRoomNo = value
        End Set
    End Property
    Public Property HBRoomType As String
        Get
            Return _HBRoomType
        End Get
        Set(ByVal value As String)
            _HBRoomType = value
        End Set
    End Property
    Public Property HBConfirmationNo As String
        Get
            Return _HBConfirmationNo
        End Get
        Set(ByVal value As String)
            _HBConfirmationNo = value
        End Set
    End Property
    Public Property HBConfirmBy As String
        Get
            Return _HBConfirmBy
        End Get
        Set(ByVal value As String)
            _HBConfirmBy = value
        End Set
    End Property
    Public Property HBConfirmationDate As String
        Get
            Return _HBConfirmationDate
        End Get
        Set(ByVal value As String)
            _HBConfirmationDate = value
        End Set
    End Property

    Function SavingBookingHotelConfirmation(confList As List(Of BLLHotelConfirm)) As String
        Dim res As String
        'Dim objDALHotelConf As New List(Of DALHotelConfirm)
        'For Each objIn As BLLHotelConfirm In confList
        '    Dim objDALHC As New DALHotelConfirm
        '    objDALHC.HBrequestid = objIn.HBRequestID
        '    objDALHC.HBRlineno = objIn.HBRlineNo
        '    objDALHC.HBRoomno = objIn.HBRoomNo
        '    objDALHC.HBConfirmationNo = objIn.HBConfirmationNo
        '    objDALHC.HBConfirmBy = objIn.HBConfirmBy
        '    objDALHC.HBConfirmationDate = objIn.HBConfirmationDate
        '    objDALHotelConf.Add(objDALHC)
        'Next
        Dim objDALHConf As New DALHotelConfirm
        res = objDALHConf.SavingBookingHotelConfirmation(confList)
        Return res
    End Function

    Function FetchBookingHotelConfirmation() As String
        Dim res As String
        Dim objDALHConf As New DALHotelConfirm
        objDALHConf.HBrequestid = HBRequestID
        objDALHConf.HBRlineno = HBRlineNo
        objDALHConf.HBRoomno = HBRoomNo
        res = objDALHConf.FetchBookingHotelConfirmation()
        If res = "True" Then
            HBConfirmationNo = objDALHConf.HBConfirmationNo
            HBConfirmBy = objDALHConf.HBConfirmBy
            HBConfirmationDate = objDALHConf.HBConfirmationDate
        End If
        Return res
    End Function
End Class
