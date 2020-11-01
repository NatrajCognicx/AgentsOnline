Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLOtherServiceFreeFormBooking

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
    Private _OtherServicetXml As String = ""
    Public Property OtherServiceXml As String
        Get
            Return _OtherServicetXml
        End Get
        Set(ByVal value As String)
            _OtherServicetXml = value
        End Set
    End Property

    Dim objDALOtherServiceFreeFormBooking As New DALOtherServiceFreeFormBooking
    Function GetOtherServiceDatas(strRequestId As String, strRlineNo As String) As DataTable
        Dim dt As DataTable
        dt = objDALOtherServiceFreeFormBooking.GetOtherServiceDatas(strRequestId, strRlineNo)
        Return dt
    End Function
    Function SaveOtherServiceFreeFormBooking() As Boolean

        Dim res As Boolean

        objDALOtherServiceFreeFormBooking.RequestId = RequestId
        objDALOtherServiceFreeFormBooking.OtherServiceXml = OtherServiceXml
        objDALOtherServiceFreeFormBooking.UserLogged = UserLogged
        objDALOtherServiceFreeFormBooking.Div_Code = Div_Code
        objDALOtherServiceFreeFormBooking.AgentCode = AgentCode
        objDALOtherServiceFreeFormBooking.SourcectryCode = SourcectryCode
        objDALOtherServiceFreeFormBooking.Agentref = Agentref
        objDALOtherServiceFreeFormBooking.ColumbusRef = ColumbusRef
        objDALOtherServiceFreeFormBooking.Remarks = Remarks
        objDALOtherServiceFreeFormBooking.SubUserCode = SubUserCode
        objDALOtherServiceFreeFormBooking.RlineNos = RlineNos

        res = objDALOtherServiceFreeFormBooking.SaveOtherServiceFreeFormBooking()

        Return res
    End Function
End Class
