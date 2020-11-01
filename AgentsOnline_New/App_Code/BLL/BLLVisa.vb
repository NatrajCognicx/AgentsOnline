 
Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLVISA

    Dim _VBrequestid As String = ""
    Dim _VBVisaXml As String = ""
    Dim _VBCancelVisaXml As String = ""
    Dim _VBuserlogged As String = ""
    Dim _VBRlinenoString As String = ""
    Dim _AmendRequestid As String = ""
    Dim _AmendLineno As String = ""
 
    Dim objDALVISA As New DALVISA

    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Public Property VBuserlogged As String
        Get
            Return _VBuserlogged
        End Get
        Set(ByVal value As String)
            _VBuserlogged = value
        End Set
    End Property
    Public Property VBVisaXml As String
        Get
            Return _VBVisaXml
        End Get
        Set(ByVal value As String)
            _VBVisaXml = value
        End Set
    End Property
    Public Property VBCancelVisaXml As String
        Get
            Return _VBCancelVisaXml
        End Get
        Set(ByVal value As String)
            _VBCancelVisaXml = value
        End Set
    End Property

    Public Property VBRequestid As String
        Get
            Return _VBrequestid
        End Get
        Set(ByVal value As String)
            _VBrequestid = value
        End Set
    End Property
    Public Property VBRlinenoString As String
        Get
            Return _VBRlinenoString
        End Get
        Set(ByVal value As String)
            _VBRlinenoString = value
        End Set
    End Property
    Public Property AmendRequestid As String
        Get
            Return _AmendRequestid
        End Get
        Set(ByVal value As String)
            _AmendRequestid = value
        End Set
    End Property
    Public Property AmendLineno As String
        Get
            Return _Amendlineno
        End Get
        Set(ByVal value As String)
            _Amendlineno = value
        End Set
    End Property

    Function SavingVisaBookingInTemp() As Boolean
        Dim res As Boolean
        objDALVISA.VBrequestid = VBRequestid
        objDALVISA.VBVisaXml = VBVisaXml
        objDALVISA.VBuserlogged = VBuserlogged
        objDALVISA.VBRlinenoString = VBRlinenoString
        res = objDALVISA.SavingVisaBookingInTemp()
        Return res
    End Function

    Sub RemoveVisa(ByVal strRequestId As String, ByVal strvlineno As String)
        Dim objDALVisa As New DALVISA
        objDALVisa.RemoveVisa(strRequestId, strvlineno)

    End Sub
    Function GetEditBookingDetails(ByVal strRequestId As String, ByVal strelineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALVISA.GetEditBookingDetails(strRequestId, strelineno)
        Return dt
    End Function

    Function GetVisaCancelDetails(ByVal strRequestId As String, ByVal strvlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALVISA.GetVisaCancelDetails(strRequestId, strvlineno)
        Return dt
    End Function
    Function SavingCancelVisaInTemp() As Boolean
        Dim res As Boolean
        objDALVISA.VBrequestid = VBRequestid
        objDALVISA.VBCancelVisaXml = VBCancelVisaXml
        objDALVISA.VBuserlogged = VBuserlogged
        res = objDALVISA.SavingCancelVisaInTemp()
        Return res
    End Function
End Class
