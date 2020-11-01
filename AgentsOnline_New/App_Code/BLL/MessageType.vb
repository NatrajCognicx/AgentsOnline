Imports Microsoft.VisualBasic

Public Class MessageType
    Private Shared _Success As String = "Success"
    Private Shared _Warning As String = "Warning"
    Private Shared _Info As String = "Info"
    Private Shared _Error As String = "Errors"
    Private Shared _Confirm As String = "Confirm"
    Private Shared _Alert As String = "Alert"
    Private Shared _Important As String = "Important Message"

    Public Shared Property Success As String
        Get
            Return _Success
        End Get
        Set(value As String)
            _Success = value
        End Set
    End Property
    Public Shared Property Warning As String
        Get
            Return _Warning
        End Get
        Set(value As String)
            _Warning = value
        End Set
    End Property
    Public Shared Property Info As String
        Get
            Return _Info
        End Get
        Set(value As String)
            _Info = value
        End Set
    End Property
    Public Shared Property Errors As String
        Get
            Return _Error
        End Get
        Set(value As String)
            _Error = value
        End Set
    End Property
    Public Shared Property Confirm As String
        Get
            Return _Confirm
        End Get
        Set(value As String)
            _Confirm = value
        End Set
    End Property
    Public Shared Property Alert As String
        Get
            Return _Alert
        End Get
        Set(value As String)
            _Alert = value
        End Set
    End Property
    Public Shared Property Important As String
        Get
            Return _Important
        End Get
        Set(value As String)
            _Important = value
        End Set
    End Property
End Class
