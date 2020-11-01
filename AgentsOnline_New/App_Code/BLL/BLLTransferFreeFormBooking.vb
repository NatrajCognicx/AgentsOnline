Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLTransferFreeFormBooking

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
    Private _TransfertXml As String = ""
    Public Property TransferXml As String
        Get
            Return _TransfertXml
        End Get
        Set(ByVal value As String)
            _TransfertXml = value
        End Set
    End Property

    Dim objDALTransferFreeFormBooking As New DALTransferFreeFormBooking
    Function GetTransferDatas(strRequestId As String, strRlineNo As String, strMode As String) As DataTable
        Dim dt As DataTable
        dt = objDALTransferFreeFormBooking.GetTransferDatas(strRequestId, strRlineNo, strMode)
        Return dt
    End Function
    Function SaveTransferFreeFormBooking() As Boolean

        Dim res As Boolean

        objDALTransferFreeFormBooking.RequestId = RequestId
        objDALTransferFreeFormBooking.TransferXml = TransferXml
        objDALTransferFreeFormBooking.UserLogged = UserLogged
        objDALTransferFreeFormBooking.Div_Code = Div_Code
        objDALTransferFreeFormBooking.AgentCode = AgentCode
        objDALTransferFreeFormBooking.SourcectryCode = SourcectryCode
        objDALTransferFreeFormBooking.Agentref = Agentref
        objDALTransferFreeFormBooking.ColumbusRef = ColumbusRef
        objDALTransferFreeFormBooking.Remarks = Remarks
        objDALTransferFreeFormBooking.SubUserCode = SubUserCode
        objDALTransferFreeFormBooking.RlineNos = RlineNos

        res = objDALTransferFreeFormBooking.SaveTransferFreeFormBooking()

        Return res
    End Function
End Class
