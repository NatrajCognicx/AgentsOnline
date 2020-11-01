Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLAirportMeetFreeFormBooking

    Private _Div_Code As String = ""
    Public Property Div_Code As String
        Get
            Return _Div_Code
        End Get
        Set(ByVal value As String)
            _Div_Code = value
        End Set
    End Property
    Private _RequestId As String = ""
    Public Property RequestId As String
        Get
            Return _RequestId
        End Get
        Set(ByVal value As String)
            _RequestId = value
        End Set
    End Property
    Private _AgentCode As String = ""
    Public Property AgentCode As String
        Get
            Return _AgentCode
        End Get
        Set(ByVal value As String)
            _AgentCode = value
        End Set
    End Property
    Private _SourcectryCode As String = ""
    Public Property SourcectryCode As String
        Get
            Return _SourcectryCode
        End Get
        Set(ByVal value As String)
            _SourcectryCode = value
        End Set
    End Property
    Private _Agentref As String = ""
    Public Property Agentref As String
        Get
            Return _Agentref
        End Get
        Set(ByVal value As String)
            _Agentref = value
        End Set
    End Property
    Private _Remarks As String = ""
    Public Property Remarks As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Private _ColumbusRef As String = ""
    Public Property ColumbusRef As String
        Get
            Return _ColumbusRef
        End Get
        Set(ByVal value As String)
            _ColumbusRef = value
        End Set
    End Property
    Private _UserLogged As String = ""
    Public Property UserLogged As String
        Get
            Return _UserLogged
        End Get
        Set(ByVal value As String)
            _UserLogged = value
        End Set
    End Property
    Private _RlineNo As String = ""
    Public Property RlineNos As String
        Get
            Return _RlineNo
        End Get
        Set(ByVal value As String)
            _RlineNo = value
        End Set
    End Property
    Private _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Private _AirportMeettXml As String = ""
    Public Property AirportMeetXml As String
        Get
            Return _AirportMeettXml
        End Get
        Set(ByVal value As String)
            _AirportMeettXml = value
        End Set
    End Property

    Dim objDALAirportMeetFreeFormBooking As New DALAirportMeetFreeFormBooking
    Function GetAirportMeetDatas(strRequestId As String, strRlineNo As String, strMode As String) As DataTable
        Dim dt As DataTable
        dt = objDALAirportMeetFreeFormBooking.GetAirportMeetDatas(strRequestId, strRlineNo, strMode)
        Return dt
    End Function
    Function SaveAirportMeetFreeFormBooking() As Boolean

        Dim res As Boolean

        objDALAirportMeetFreeFormBooking.RequestId = RequestId
        objDALAirportMeetFreeFormBooking.AirportMeetXml = AirportMeetXml
        objDALAirportMeetFreeFormBooking.UserLogged = UserLogged
        objDALAirportMeetFreeFormBooking.Div_Code = Div_Code
        objDALAirportMeetFreeFormBooking.AgentCode = AgentCode
        objDALAirportMeetFreeFormBooking.SourcectryCode = SourcectryCode
        objDALAirportMeetFreeFormBooking.Agentref = Agentref
        objDALAirportMeetFreeFormBooking.ColumbusRef = ColumbusRef
        objDALAirportMeetFreeFormBooking.Remarks = Remarks
        objDALAirportMeetFreeFormBooking.SubUserCode = SubUserCode
        objDALAirportMeetFreeFormBooking.RlineNos = RlineNos

        res = objDALAirportMeetFreeFormBooking.SaveAirportMeetFreeFormBooking()

        Return res
    End Function
End Class
