Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLTransferSearch

    Dim _BookType As String = ""

    Dim _ArrTransferType As String = ""
    Dim _DepTransferType As String = ""
    Dim _ShiftingTransferType As String = ""
    Dim _ArrTransferDate As String = ""
    Dim _DepTransferDate As String = ""
    Dim _ShiftingDate As String = ""

    Dim _ArrFlightClass As String = ""
    Dim _DepFlightClass As String = ""
    Dim _ArrFlightNo As String = ""
    Dim _DepFlightNo As String = ""
    Dim _ArrFlightTime As String = ""
    Dim _DepFlightTime As String = ""
    Dim _ArrPickupCode As String = ""
    Dim _ArrPickupName As String = ""
    Dim _DepPickupCode As String = ""
    Dim _DepPickupName As String = ""

    Dim _ShiftingPickupCode As String = ""
    Dim _ShiftingPickupName As String = ""
    Dim _ShiftingDropCode As String = ""
    Dim _ShiftingDropName As String = ""

    Dim _ShiftingPickupSector As String = ""
    Dim _ShiftingDropSector As String = ""


    Dim _ArrDropCode As String = ""
    Dim _ArrDropName As String = ""
    Dim _DepDropCode As String = ""
    Dim _DepDropName As String = ""
    Dim _ArrSector As String = ""
    Dim _DepSector As String = ""
    Dim _QueryString As String = ""
    Dim _FilterRoomClass As String = ""

    Dim _ArrDroptype As String = ""
    Dim _DepPickuptype As String = ""
    Dim _ShiftingPickuptype As String = ""
    Dim _ShiftingDroptype As String = ""

    Dim _TrfAdult As String = ""
    Dim _TrfChildren As String = ""
    Dim _TrfChild1 As String = ""
    Dim _TrfChild2 As String = ""
    Dim _TrfChild3 As String = ""
    Dim _TrfChild4 As String = ""
    Dim _TrfChild5 As String = ""
    Dim _TrfChild6 As String = ""
    Dim _TrfChild7 As String = ""
    Dim _TrfChild8 As String = ""
    Dim _ChildAgeString As String = ""

    Dim _TrfSourceCountry As String = ""
    Dim _TrfSourceCountryCode As String = ""



    Dim _TrfCustomer As String = ""
    Dim _TrfCustomerCode As String = ""

    Dim _LoginType As String = ""
    Dim _WebUserName As String = ""
    Dim _AgentCode As String = ""

    Dim _OverridePrice As String = ""
    Dim _OBTransferXml As String = ""

    Dim _OBDiv_Code As String = ""
    Dim _OBRequestId As String = ""
    Dim _OBAgentCode As String = ""
    Dim _OBSourcectryCode As String = ""
    Dim _OBReqoverRide As String = ""
    Dim _OBAgentref As String = ""
    Dim _OBRemarks As String = ""
    Dim _OBColumbusRef As String = ""
    Dim _UserLogged As String = ""
    Dim _OBRlineNo As Integer
    Dim _OBRlinenoString As String = ""
    Dim _OBNoofUnits As Integer
    Dim _OBAdults As Integer
    Dim _OBChild As Integer
    Dim _OBChildAges As String
    Dim _OBSupagentCode As String = ""
    Dim _OBPartyCode As String = ""

    Dim _TPlistCode As String = ""
    Dim _OBComplimentcust As String = ""

    Dim _OBTransferType As String = ""
    Dim _OBAirportBorderCode As String = ""
    Dim _OBSectorGroupCode As String = ""
    Dim _OBCarTypeCode As String = ""
    Dim _OBShuttle As Integer
    Dim _OBTransferDate As String = ""
    Dim _OBFlightCode As String = ""
    Dim _OBFlightTranID As String = ""
    Dim _OBFlightTime As String = ""
    Dim _OBPickup As String = ""
    Dim _OBDropoff As String = ""
    Dim _OBUnitPrice As String
    Dim _OBUnitSaleValue As String
    Dim _OBWlUnitPrice As String
    Dim _OBWlUnitSaleValue As String

    Dim _AmendRequestid As String = ""
    Dim _AmendLineno As String = ""

    Dim _OBCancelTransferXml As String = ""

    Dim objDALTransferSearch As New DALTransferSearch
    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Public Property BookType As String
        Get
            Return _BookType
        End Get
        Set(ByVal value As String)
            _BookType = value
        End Set
    End Property
    Public Property OBTransferXml As String
        Get
            Return _OBTransferXml
        End Get
        Set(ByVal value As String)
            _OBTransferXml = value
        End Set
    End Property
    Public Property OBCancelTransferXml As String
        Get
            Return _OBCancelTransferXml
        End Get
        Set(ByVal value As String)
            _OBCancelTransferXml = value
        End Set
    End Property
    Public Property ArrTransferType As String
        Get
            Return _ArrTransferType
        End Get
        Set(ByVal value As String)
            _ArrTransferType = value
        End Set
    End Property
    Public Property DepTransferType As String
        Get
            Return _DepTransferType
        End Get
        Set(ByVal value As String)
            _DepTransferType = value
        End Set
    End Property
    Public Property ShiftingTransferType As String
        Get
            Return _ShiftingTransferType
        End Get
        Set(ByVal value As String)
            _ShiftingTransferType = value
        End Set
    End Property
    Public Property ArrTransferDate As String
        Get
            Return _ArrTransferDate
        End Get
        Set(ByVal value As String)
            _ArrTransferDate = value
        End Set
    End Property
    Public Property DepTransferDate As String
        Get
            Return _DepTransferDate
        End Get
        Set(ByVal value As String)
            _DepTransferDate = value
        End Set
    End Property
    Public Property ShiftingDate As String
        Get
            Return _ShiftingDate
        End Get
        Set(ByVal value As String)
            _ShiftingDate = value
        End Set
    End Property
    Public Property ArrFlightClass As String
        Get
            Return _ArrFlightClass
        End Get
        Set(ByVal value As String)
            _ArrFlightClass = value
        End Set
    End Property
    Public Property DepFlightClass As String
        Get
            Return _DepFlightClass
        End Get
        Set(ByVal value As String)
            _DepFlightClass = value
        End Set
    End Property
    Public Property ArrFlightNo As String
        Get
            Return _ArrFlightNo
        End Get
        Set(ByVal value As String)
            _ArrFlightNo = value
        End Set
    End Property
    Public Property DepFlightNo As String
        Get
            Return _DepFlightNo
        End Get
        Set(ByVal value As String)
            _DepFlightNo = value
        End Set
    End Property
    Public Property ArrFlightTime As String
        Get
            Return _ArrFlightTime
        End Get
        Set(ByVal value As String)
            _ArrFlightTime = value
        End Set
    End Property
    Public Property FilterRoomClass As String
        Get
            Return _FilterRoomClass
        End Get
        Set(ByVal value As String)
            _FilterRoomClass = value
        End Set
    End Property
    Public Property DepFlightTime As String
        Get
            Return _DepFlightTime
        End Get
        Set(ByVal value As String)
            _DepFlightTime = value
        End Set
    End Property
    Public Property ArrPickupCode As String
        Get
            Return _ArrPickupCode
        End Get
        Set(ByVal value As String)
            _ArrPickupCode = value
        End Set
    End Property
    Public Property ArrPickupName As String
        Get
            Return _ArrPickupName
        End Get
        Set(ByVal value As String)
            _ArrPickupName = value
        End Set
    End Property
    Public Property DepPickupCode As String
        Get
            Return _DepPickupCode
        End Get
        Set(ByVal value As String)
            _DepPickupCode = value
        End Set
    End Property
    Public Property DepPickupName As String
        Get
            Return _DepPickupName
        End Get
        Set(ByVal value As String)
            _DepPickupName = value
        End Set
    End Property
    Public Property ShiftingPickupCode As String
        Get
            Return _ShiftingPickupCode
        End Get
        Set(ByVal value As String)
            _ShiftingPickupCode = value
        End Set
    End Property
    Public Property ShiftingPickupName As String
        Get
            Return _ShiftingPickupName
        End Get
        Set(ByVal value As String)
            _ShiftingPickupName = value
        End Set
    End Property
    Public Property ShiftingDropCode As String
        Get
            Return _ShiftingDropCode
        End Get
        Set(ByVal value As String)
            _ShiftingDropCode = value
        End Set
    End Property
    Public Property ShiftingDropName As String
        Get
            Return _ShiftingDropName
        End Get
        Set(ByVal value As String)
            _ShiftingDropName = value
        End Set
    End Property
    Public Property ShiftingPickupSector As String
        Get
            Return _ShiftingPickupSector
        End Get
        Set(ByVal value As String)
            _ShiftingPickupSector = value
        End Set
    End Property
    Public Property ShiftingDropSector As String
        Get
            Return _ShiftingDropSector
        End Get
        Set(ByVal value As String)
            _ShiftingDropSector = value
        End Set
    End Property

    Public Property ArrDropCode As String
        Get
            Return _ArrDropCode
        End Get
        Set(ByVal value As String)
            _ArrDropCode = value
        End Set
    End Property
    Public Property ArrDropName As String
        Get
            Return _ArrDropName
        End Get
        Set(ByVal value As String)
            _ArrDropName = value
        End Set
    End Property

    Public Property DepDropCode As String
        Get
            Return _DepDropCode
        End Get
        Set(ByVal value As String)
            _DepDropCode = value
        End Set
    End Property
    Public Property DepDropName As String
        Get
            Return _DepDropName
        End Get
        Set(ByVal value As String)
            _DepDropName = value
        End Set
    End Property
    Public Property ArrSector As String
        Get
            Return _ArrSector
        End Get
        Set(ByVal value As String)
            _ArrSector = value
        End Set
    End Property
    Public Property DepSector As String
        Get
            Return _DepSector
        End Get
        Set(ByVal value As String)
            _DepSector = value
        End Set
    End Property
    Public Property TrfAdult As String
        Get
            Return _TrfAdult
        End Get
        Set(ByVal value As String)
            _TrfAdult = value
        End Set
    End Property
    Public Property TrfChildren As String
        Get
            Return _TrfChildren
        End Get
        Set(ByVal value As String)
            _TrfChildren = value
        End Set
    End Property

    Public Property TrfChild1 As String
        Get
            Return _TrfChild1
        End Get
        Set(ByVal value As String)
            _TrfChild1 = value
        End Set
    End Property
    Public Property TrfChild2 As String
        Get
            Return _TrfChild2
        End Get
        Set(ByVal value As String)
            _TrfChild2 = value
        End Set
    End Property
    Public Property TrfChild3 As String
        Get
            Return _TrfChild3
        End Get
        Set(ByVal value As String)
            _TrfChild3 = value
        End Set
    End Property
    Public Property TrfChild4 As String
        Get
            Return _TrfChild4
        End Get
        Set(ByVal value As String)
            _TrfChild4 = value
        End Set
    End Property
    Public Property TrfChild5 As String
        Get
            Return _TrfChild5
        End Get
        Set(ByVal value As String)
            _TrfChild5 = value
        End Set
    End Property
    Public Property TrfChild6 As String
        Get
            Return _TrfChild6
        End Get
        Set(ByVal value As String)
            _TrfChild6 = value
        End Set
    End Property
    Public Property TrfChild7 As String
        Get
            Return _TrfChild7
        End Get
        Set(ByVal value As String)
            _TrfChild7 = value
        End Set
    End Property
    Public Property TrfChild8 As String
        Get
            Return _TrfChild8
        End Get
        Set(ByVal value As String)
            _TrfChild8 = value
        End Set
    End Property

    Public Property OBRlinenoString As String
        Get
            Return _OBRlinenoString
        End Get
        Set(ByVal value As String)
            _OBRlinenoString = value
        End Set
    End Property
    Public Property ChildAgeString As String
        Get
            Return _ChildAgeString
        End Get
        Set(ByVal value As String)
            _ChildAgeString = value
        End Set
    End Property
    Public Property TrfSourceCountry As String
        Get
            Return _TrfSourceCountry
        End Get
        Set(ByVal value As String)
            _TrfSourceCountry = value
        End Set
    End Property

    Public Property TrfSourceCountryCode As String
        Get
            Return _TrfSourceCountryCode
        End Get
        Set(ByVal value As String)
            _TrfSourceCountryCode = value
        End Set
    End Property

    Public Property TrfCustomer As String
        Get
            Return _TrfCustomer
        End Get
        Set(ByVal value As String)
            _TrfCustomer = value
        End Set
    End Property

    Public Property TrfCustomerCode As String
        Get
            Return _TrfCustomerCode
        End Get
        Set(ByVal value As String)
            _TrfCustomerCode = value
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

    Public Property WebUserName As String
        Get
            Return _WebUserName
        End Get
        Set(ByVal value As String)
            _WebUserName = value
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

    Public Property OverridePrice As String
        Get
            Return _OverridePrice
        End Get
        Set(ByVal value As String)
            _OverridePrice = value
        End Set
    End Property

    Public Property QueryString As String
        Get
            Return _QueryString
        End Get
        Set(ByVal value As String)
            _QueryString = value
        End Set
    End Property
    Public Property OBDiv_Code As String
        Get
            Return _OBDiv_Code
        End Get
        Set(ByVal value As String)
            _OBDiv_Code = value
        End Set
    End Property
    Public Property OBRequestId As String
        Get
            Return _OBRequestId
        End Get
        Set(ByVal value As String)
            _OBRequestId = value
        End Set
    End Property
    Public Property OBAgentCode As String
        Get
            Return _OBAgentCode
        End Get
        Set(ByVal value As String)
            _OBAgentCode = value
        End Set
    End Property
    Public Property OBSourcectryCode As String
        Get
            Return _OBSourcectryCode
        End Get
        Set(ByVal value As String)
            _OBSourcectryCode = value
        End Set
    End Property

    Public Property OBReqoverRide As String
        Get
            Return _OBReqoverRide
        End Get
        Set(ByVal value As String)
            _OBReqoverRide = value
        End Set
    End Property
    Public Property OBAgentref As String
        Get
            Return _OBAgentref
        End Get
        Set(ByVal value As String)
            _OBAgentref = value
        End Set
    End Property

    Public Property OBRemarks As String
        Get
            Return _OBRemarks
        End Get
        Set(ByVal value As String)
            _OBRemarks = value
        End Set
    End Property

    Public Property OBColumbusRef As String
        Get
            Return _OBColumbusRef
        End Get
        Set(ByVal value As String)
            _OBColumbusRef = value
        End Set
    End Property

    Public Property UserLogged As String
        Get
            Return _UserLogged
        End Get
        Set(ByVal value As String)
            _UserLogged = value
        End Set
    End Property

    Public Property OBRlineNo As String
        Get
            Return _OBRlineNo
        End Get
        Set(ByVal value As String)
            _OBRlineNo = value
        End Set
    End Property

    Public Property OBNoofUnits As String
        Get
            Return _OBNoofUnits
        End Get
        Set(ByVal value As String)
            _OBNoofUnits = value
        End Set
    End Property
    Public Property OBAdults As String
        Get
            Return _OBAdults
        End Get
        Set(ByVal value As String)
            _OBAdults = value
        End Set
    End Property

    Public Property OBChild As String
        Get
            Return _OBChild
        End Get
        Set(ByVal value As String)
            _OBChild = value
        End Set
    End Property

    Public Property OBChildAges As String
        Get
            Return _OBChildAges
        End Get
        Set(ByVal value As String)
            _OBChildAges = value
        End Set
    End Property

    Public Property OBSupagentCode As String
        Get
            Return _OBSupagentCode
        End Get
        Set(ByVal value As String)
            _OBSupagentCode = value
        End Set
    End Property
    Public Property OBPartyCode As String
        Get
            Return _OBPartyCode
        End Get
        Set(ByVal value As String)
            _OBPartyCode = value
        End Set
    End Property
    Public Property OBTransferType As String
        Get
            Return _OBTransferType
        End Get
        Set(ByVal value As String)
            _OBTransferType = value
        End Set
    End Property
    Public Property OBAirportBorderCode As String
        Get
            Return _OBAirportBorderCode
        End Get
        Set(ByVal value As String)
            _OBAirportBorderCode = value
        End Set
    End Property
    Public Property OBSectorGroupCode As String
        Get
            Return _OBSectorGroupCode
        End Get
        Set(ByVal value As String)
            _OBSectorGroupCode = value
        End Set
    End Property
    Public Property OBCarTypeCode As String
        Get
            Return _OBCarTypeCode
        End Get
        Set(ByVal value As String)
            _OBCarTypeCode = value
        End Set
    End Property
    Public Property OBShuttle As Integer
        Get
            Return _OBShuttle
        End Get
        Set(ByVal value As Integer)
            _OBShuttle = value
        End Set
    End Property
    Public Property OBTransferDate As String
        Get
            Return _OBTransferDate
        End Get
        Set(ByVal value As String)
            _OBTransferDate = value
        End Set
    End Property
    Public Property OBFlightCode As String
        Get
            Return _OBFlightCode
        End Get
        Set(ByVal value As String)
            _OBFlightCode = value
        End Set
    End Property
    Public Property OBFlightTranID As String
        Get
            Return _OBFlightTranID
        End Get
        Set(ByVal value As String)
            _OBFlightTranID = value
        End Set
    End Property
    Public Property OBFlightTime As String
        Get
            Return _OBFlightTime
        End Get
        Set(ByVal value As String)
            _OBFlightTime = value
        End Set
    End Property
    Public Property OBPickup As String
        Get
            Return _OBPickup
        End Get
        Set(ByVal value As String)
            _OBPickup = value
        End Set
    End Property
    Public Property OBDropoff As String
        Get
            Return _OBDropoff
        End Get
        Set(ByVal value As String)
            _OBDropoff = value
        End Set
    End Property
    Public Property OBUnitPrice As String
        Get
            Return _OBUnitPrice
        End Get
        Set(ByVal value As String)
            _OBUnitPrice = value
        End Set
    End Property
    Public Property OBUnitSaleValue As String
        Get
            Return _OBUnitSaleValue
        End Get
        Set(ByVal value As String)
            _OBUnitSaleValue = value
        End Set
    End Property
    Public Property OBWlUnitPrice As String
        Get
            Return _OBWlUnitPrice
        End Get
        Set(ByVal value As String)
            _OBWlUnitPrice = value
        End Set
    End Property
    Public Property OBWlUnitSaleValue As String
        Get
            Return _OBWlUnitSaleValue
        End Get
        Set(ByVal value As String)
            _OBWlUnitSaleValue = value
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
    Function FindBookingEnginRateType(ByVal sAgentCode As String) As Integer
        Dim iCumulative As Integer = 0
        iCumulative = objDALTransferSearch.FindBookingEnginRateType(sAgentCode)
        Return iCumulative
    End Function
    Function GetSearchDetails() As DataSet
        Dim dsTransferSearch As New DataSet
        dsTransferSearch = objDALTransferSearch.GetTrfSearchDetails(LoginType, WebUserName, AgentCode, ArrTransferType, ArrTransferDate, ArrPickupCode, ArrSector, DepTransferType, DepTransferDate, DepSector, DepDropCode, TrfAdult, TrfChildren, ChildAgeString, TrfSourceCountryCode, OverridePrice, BookType, ShiftingTransferType, ShiftingDate, ShiftingPickupSector, ShiftingDropSector, AmendRequestid, AmendLineno)
        Return dsTransferSearch

    End Function
    Function SavingTransferBookingtemp() As Boolean
        Dim res As Boolean


        objDALTransferSearch.OBRequestId = OBRequestId
        objDALTransferSearch.OBTransfertXml = OBTransferXml
        objDALTransferSearch.UserLogged = UserLogged

        objDALTransferSearch.OBDiv_Code = OBDiv_Code
        objDALTransferSearch.OBAgentCode = OBAgentCode
        objDALTransferSearch.OBSourcectryCode = OBSourcectryCode
        objDALTransferSearch.OBReqoverRide = OBReqoverRide
        objDALTransferSearch.OBAgentref = OBAgentref
        objDALTransferSearch.OBColumbusRef = OBColumbusRef
        objDALTransferSearch.OBRemarks = OBRemarks
        objDALTransferSearch.SubUserCode = SubUserCode
        objDALTransferSearch.OBRlinenoString = OBRlinenoString


        res = objDALTransferSearch.savingbookingintemp()

        Return res
    End Function
    Function SavingCancelTransferDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALTransferSearch.OBRequestId = OBRequestId
        objDALTransferSearch.OBCancelTransfertXml = OBCancelTransferXml
        objDALTransferSearch.UserLogged = UserLogged
        res = objDALTransferSearch.SavingCancelTransferDetailsInTemp()
        Return res
    End Function
    Function GetTransferSummary(strRequestId As String, strWhiteLabel As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTransferSearch.GetTransferSummary(strRequestId, strWhiteLabel)
        Return dt
    End Function

    Function LaodTrfFlightDetails(strRequestId As String) As DataTable
        Dim dt As DataTable
        dt = objDALTransferSearch.LaodTrfFlightDetails(strRequestId)
        Return dt
    End Function
    Function FillTransferDetails(ByVal strReqId As String) As DataSet
        Dim ds As New DataSet
        ds = objDALTransferSearch.FillTransferDetails(strReqId)
        Return ds
    End Function
    Function FillTransferDetailsFromMA(ByVal strReqId As String) As DataSet
        Dim ds As New DataSet
        ds = objDALTransferSearch.FillTransferDetailsFromMA(strReqId)
        Return ds
    End Function
    Function GetAirportTerminal(ByVal strRequestId As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTransferSearch.GetAirportTerminal(strRequestId)
        Return dt
    End Function

    Sub RemoveTransfers(ByVal strRequestId As String, ByVal strtlineno As String)
        Dim objDALTransferSearch As New DALTransferSearch
        objDALTransferSearch.RemoveTransfers(strRequestId, strtlineno)

    End Sub
    Function GetEditBookingDetails(ByVal strRequestId As String, ByVal strolineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTransferSearch.GetEditBookingDetails(strRequestId, strolineno)
        Return dt
    End Function

    Function GetTransferCancelDetails(ByVal strRequestId As String, ByVal strtlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTransferSearch.GetTransferCancelDetails(strRequestId, strtlineno)
        Return dt
    End Function
    Public Property ArrDroptype As String
        Get
            Return _ArrDroptype
        End Get
        Set(ByVal value As String)
            _ArrDroptype = value
        End Set
    End Property
    Public Property DepPickuptype As String
        Get
            Return _DepPickuptype
        End Get
        Set(ByVal value As String)
            _DepPickuptype = value
        End Set
    End Property
    Public Property ShiftingPickuptype As String
        Get
            Return _ShiftingPickuptype
        End Get
        Set(ByVal value As String)
            _ShiftingPickuptype = value
        End Set
    End Property
    Public Property ShiftingDroptype As String
        Get
            Return _ShiftingDroptype
        End Get
        Set(ByVal value As String)
            _ShiftingDroptype = value
        End Set
    End Property

End Class
