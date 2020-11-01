Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLMyAccount

    Private _LoginType As String = ""
    Private _WebUserName As String = ""
    Private _AgentCode As String = ""
    Private _RequestId As String = ""
    Private _ServiceType As String = ""
    Private _DestinationType As String = ""
    Private _DestinationCode As String = ""
    Private _AgentRef As String = ""
    Private _GuestFirstName As String = ""
    Private _GuestLastName As String = ""
    Private _TravelDateType As String = ""
    Private _TravelDateFrom As String = ""
    Private _TravelDateTo As String = ""
    Private _BookingDateType As String = ""
    Private _BookingDateFrom As String = ""
    Private _BookingDateTo As String = ""
    Private _BookingStatus As String = ""
    Private _PartyCode As String = ""
    Private _HotelConfNo As String = ""
    Private _SearchAgentCode As String = ""
    Private _SourceCountrycode As String = ""
    Private _UserCode As String = ""

    Private _CompanyCode As String = ""
    Private _SubUserCode As String = ""
    Private _Tab As String = ""
    Private _quoteType As String = ""

    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property

    Public Property UserCode As String
        Get
            Return _UserCode
        End Get
        Set(ByVal value As String)
            _UserCode = value
        End Set
    End Property

    Public Property CompanyCode As String
        Get
            Return _CompanyCode
        End Get
        Set(ByVal value As String)
            _CompanyCode = value
        End Set
    End Property

    Public Property SourceCountrycode As String
        Get
            Return _SourceCountrycode
        End Get
        Set(ByVal value As String)
            _SourceCountrycode = value
        End Set
    End Property

    Public Property SearchAgentCode As String
        Get
            Return _SearchAgentCode
        End Get
        Set(ByVal value As String)
            _SearchAgentCode = value
        End Set
    End Property

    Public Property HotelConfNo As String
        Get
            Return _HotelConfNo
        End Get
        Set(ByVal value As String)
            _HotelConfNo = value
        End Set
    End Property

    Public Property PartyCode As String
        Get
            Return _PartyCode
        End Get
        Set(ByVal value As String)
            _PartyCode = value
        End Set
    End Property

    Public Property BookingStatus As String
        Get
            Return _BookingStatus
        End Get
        Set(ByVal value As String)
            _BookingStatus = value
        End Set
    End Property

    Public Property BookingDateTo As String
        Get
            Return _BookingDateTo
        End Get
        Set(ByVal value As String)
            _BookingDateTo = value
        End Set
    End Property

    Public Property BookingDateFrom As String
        Get
            Return _BookingDateFrom
        End Get
        Set(ByVal value As String)
            _BookingDateFrom = value
        End Set
    End Property

    Public Property BookingDateType As String
        Get
            Return _BookingDateType
        End Get
        Set(ByVal value As String)
            _BookingDateType = value
        End Set
    End Property

    Public Property TravelDateTo As String
        Get
            Return _TravelDateTo
        End Get
        Set(ByVal value As String)
            _TravelDateTo = value
        End Set
    End Property

    Public Property TravelDateFrom As String
        Get
            Return _TravelDateFrom
        End Get
        Set(ByVal value As String)
            _TravelDateFrom = value
        End Set
    End Property

    Public Property TravelDateType As String
        Get
            Return _TravelDateType
        End Get
        Set(ByVal value As String)
            _TravelDateType = value
        End Set
    End Property
    Public Property GuestLastName As String
        Get
            Return _GuestLastName
        End Get
        Set(ByVal value As String)
            _GuestLastName = value
        End Set
    End Property

    Public Property GuestFirstName As String
        Get
            Return _GuestFirstName
        End Get
        Set(ByVal value As String)
            _GuestFirstName = value
        End Set
    End Property

    Public Property AgentRef As String
        Get
            Return _AgentRef
        End Get
        Set(ByVal value As String)
            _AgentRef = value
        End Set
    End Property

    Public Property DestinationCode As String
        Get
            Return _DestinationCode
        End Get
        Set(ByVal value As String)
            _DestinationCode = value
        End Set
    End Property

    Public Property DestinationType As String
        Get
            Return _DestinationType
        End Get
        Set(ByVal value As String)
            _DestinationType = value
        End Set
    End Property

    Public Property ServiceType As String
        Get
            Return _ServiceType
        End Get
        Set(ByVal value As String)
            _ServiceType = value
        End Set
    End Property

    Public Property RequestId As String
        Get
            Return _RequestId
        End Get
        Set(ByVal value As String)
            _RequestId = value
        End Set
    End Property

    Public Property AgentCode As String
        Get
            Return _AgentCode
        End Get
        Set(ByVal value As String)
            _AgentCode = value
        End Set
    End Property
    Public Property WebUserName As String
        Get
            Return _WebUserName
        End Get
        Set(ByVal value As String)
            _WebUserName = value
        End Set
    End Property
    Public Property LoginType As String
        Get
            Return _LoginType
        End Get
        Set(ByVal value As String)
            _LoginType = value
        End Set
    End Property

    Public Property Tab As String
        Get
            Return _Tab
        End Get
        Set(ByVal value As String)
            _Tab = value
        End Set
    End Property
    Private _GridPageIndex As String = "0"
    Public Property GridPageIndex As String
        Get
            Return _GridPageIndex
        End Get
        Set(ByVal value As String)
            _GridPageIndex = value
        End Set
    End Property

    Public Property quoteType As String
        Get
            Return _quoteType
        End Get
        Set(ByVal value As String)
            _quoteType = value
        End Set
    End Property

    Function GetBookingSearchDetails() As DataTable
        Dim objDALMyAccount As New DALMyAccount
        objDALMyAccount.LoginType = LoginType
        objDALMyAccount.WebUserName = WebUserName
        objDALMyAccount.AgentCode = AgentCode
        objDALMyAccount.RequestId = RequestId
        objDALMyAccount.ServiceType = ServiceType

        objDALMyAccount.DestinationCode = DestinationCode
        objDALMyAccount.DestinationType = DestinationType

        objDALMyAccount.AgentRef = AgentRef
        objDALMyAccount.GuestFirstName = GuestFirstName
        objDALMyAccount.GuestLastName = GuestLastName
        objDALMyAccount.TravelDateType = TravelDateType
        objDALMyAccount.TravelDateFrom = TravelDateFrom
        objDALMyAccount.TravelDateTo = TravelDateTo
        objDALMyAccount.BookingDateType = BookingDateType
        objDALMyAccount.BookingDateFrom = BookingDateFrom
        objDALMyAccount.BookingDateTo = BookingDateTo
        objDALMyAccount.BookingStatus = BookingStatus
        objDALMyAccount.PartyCode = PartyCode
        objDALMyAccount.HotelConfNo = HotelConfNo
        objDALMyAccount.SearchAgentCode = SearchAgentCode
        objDALMyAccount.SourceCountrycode = SourceCountrycode
        objDALMyAccount.UserCode = UserCode
        objDALMyAccount.CompanyCode = CompanyCode
        objDALMyAccount.SubUserCode = SubUserCode
        Dim dtResult As DataTable
        dtResult = objDALMyAccount.GetBookingSearchDetails()
        Return dtResult
    End Function

    Function FillBookingToTempForEdit(ByVal strRequestId As String, ByVal strLoggedUser As String, ByVal CopyOREdit As String) As String
        Dim objDALMyAccount As New DALMyAccount
        FillBookingToTempForEdit = objDALMyAccount.FillBookingToTempForEdit(strRequestId, strLoggedUser, CopyOREdit)

    End Function

    Sub FillQuoteToTempForEdit(ByVal strRequestId As String, ByVal strLoggedUser As String)
        Dim objDALMyAccount As New DALMyAccount
        objDALMyAccount.FillQuoteToTempForEdit(strRequestId, strLoggedUser)

    End Sub

    Function GetQuoteSearchDetails() As DataTable
        Dim objDALMyAccount As New DALMyAccount
        objDALMyAccount.LoginType = LoginType
        objDALMyAccount.WebUserName = WebUserName
        objDALMyAccount.AgentCode = AgentCode
        objDALMyAccount.RequestId = RequestId
        objDALMyAccount.ServiceType = ServiceType

        objDALMyAccount.DestinationCode = DestinationCode
        objDALMyAccount.DestinationType = DestinationType

        objDALMyAccount.AgentRef = AgentRef
        objDALMyAccount.GuestFirstName = GuestFirstName
        objDALMyAccount.GuestLastName = GuestLastName
        objDALMyAccount.TravelDateType = TravelDateType
        objDALMyAccount.TravelDateFrom = TravelDateFrom
        objDALMyAccount.TravelDateTo = TravelDateTo
        objDALMyAccount.BookingDateType = BookingDateType
        objDALMyAccount.BookingDateFrom = BookingDateFrom
        objDALMyAccount.BookingDateTo = BookingDateTo
        objDALMyAccount.BookingStatus = BookingStatus
        objDALMyAccount.PartyCode = PartyCode
        objDALMyAccount.HotelConfNo = HotelConfNo
        objDALMyAccount.SearchAgentCode = SearchAgentCode
        objDALMyAccount.SourceCountrycode = SourceCountrycode
        objDALMyAccount.UserCode = UserCode
        objDALMyAccount.CompanyCode = CompanyCode
        objDALMyAccount.SubUserCode = SubUserCode
        objDALMyAccount.quoteType = quoteType
        Dim dtResult As DataTable
        dtResult = objDALMyAccount.GetQuoteSearchDetails()
        Return dtResult
    End Function

    Function CheckQuoteRateChange(ByVal strRequestId As String) As String
        Dim objDALMyAccount As New DALMyAccount
        Dim strRateChange As String = objDALMyAccount.CheckQuoteRateChange(strRequestId)
        Return strRateChange
    End Function

    Function RevisePriceForEdit(ByVal strRequestid As String) As String
        Dim objDALMyAccount As New DALMyAccount
        Dim strStatus As String = objDALMyAccount.RevisePriceForEdit(strRequestid)
        Return strStatus
    End Function

    Function checkInvoiced(ByVal strRequestid As String) As String
        Dim objDALMyAccount As New DALMyAccount
        Dim strInvoiced As String = objDALMyAccount.checkInvoiced(strRequestid)
        Return strInvoiced
    End Function

    Function checkPrivilege(ByVal usercode As String, ByVal appid As String, ByVal privilegeid As String) As Boolean
        Dim objDALMyAccount As New DALMyAccount
        Dim privilegeStatus As Boolean = objDALMyAccount.checkPrivilege(usercode, appid, privilegeid)
        Return privilegeStatus
    End Function

    Function checkSupplierUpdated(ByVal strRequestId As String) As String
        Dim objDALMyAccount As New DALMyAccount
        Dim strSupplierUpdated As String = objDALMyAccount.checkSupplierUpdated(strRequestId)
        Return strSupplierUpdated
    End Function

End Class
