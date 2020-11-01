Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLMASearch

    Dim _TransferType As String = ""

    Dim _MAArrivalType As String = ""
    Dim _MADepartueType As String = ""
    Dim _MATransitType As String = ""

    Dim _MAArrTransferDate As String = ""
    Dim _MADepTransferDate As String = ""
    Dim _MATranArrDate As String = ""
    Dim _MATranDepDate As String = ""
    Dim _MATranArrFlightClass As String = ""
    Dim _MATranDepFlightClass As String = ""
    Dim _MATranArrFlightNo As String = ""
    Dim _MATranDepFlightNo As String = ""
    Dim _MATranArrFlightTime As String = ""
    Dim _MATranDepFlightTime As String = ""
    Dim _MATranArrPickupCode As String = ""
    Dim _MATranArrPickupName As String = ""
    Dim _MATranDepDropCode As String = ""
    Dim _MATranDepDropName As String = ""
    Dim _MATranArrSector As String = ""
    Dim _MATranDepSector As String = ""

    Dim _MAArrFlightClass As String = ""
    Dim _MADepFlightClass As String = ""
    Dim _MAArrFlightNo As String = ""
    Dim _MADepFlightNo As String = ""
    Dim _MAArrFlightTime As String = ""
    Dim _MADepFlightTime As String = ""
    Dim _MAArrPickupCode As String = ""
    Dim _MAArrPickupName As String = ""
    Dim _MADepDropCode As String = ""
    Dim _MADepDropName As String = ""
    Dim _ArrSector As String = ""
    Dim _DepSector As String = ""
    Dim _QueryString As String = ""
    Dim _FilterRoomClass As String = ""

    Dim _Adult As String = ""
    Dim _Children As String = ""
    Dim _Child1 As String = ""
    Dim _Child2 As String = ""
    Dim _Child3 As String = ""
    Dim _Child4 As String = ""
    Dim _Child5 As String = ""
    Dim _Child6 As String = ""
    Dim _Child7 As String = ""
    Dim _Child8 As String = ""
    Dim _ChildAgeString As String = ""

    Dim _SourceCountry As String = ""
    Dim _SourceCountryCode As String = ""

    Dim _Customer As String = ""
    Dim _CustomerCode As String = ""

    Dim _LoginType As String = ""
    Dim _WebUserName As String = ""
    Dim _AgentCode As String = ""

    Dim _OverridePrice As String = ""
    Dim _Details As String = ""
    Dim _AirportMATypeCode As String = ""
    Dim _Units As String = ""


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

    Dim _OBAirportType As String = ""
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

    Dim _OBCancelAirXml As String = ""


    Dim objDALMASearch As New DALMASearch
    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Public Property TransferType As String
        Get
            Return _TransferType
        End Get
        Set(ByVal value As String)
            _TransferType = value
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
    Public Property OBCancelAirXml As String
        Get
            Return _OBCancelAirXml
        End Get
        Set(ByVal value As String)
            _OBCancelAirXml = value
        End Set
    End Property
    Public Property MAArrivalType As String
        Get
            Return _MAArrivalType
        End Get
        Set(ByVal value As String)
            _MAArrivalType = value
        End Set
    End Property
    Public Property MADepartueType As String
        Get
            Return _MADepartueType
        End Get
        Set(ByVal value As String)
            _MADepartueType = value
        End Set
    End Property
    Public Property MATransitType As String
        Get
            Return _MATransitType
        End Get
        Set(ByVal value As String)
            _MATransitType = value
        End Set
    End Property

    Public Property MAArrTransferDate As String
        Get
            Return _MAArrTransferDate
        End Get
        Set(ByVal value As String)
            _MAArrTransferDate = value
        End Set
    End Property
    Public Property MADepTransferDate As String
        Get
            Return _MADepTransferDate
        End Get
        Set(ByVal value As String)
            _MADepTransferDate = value
        End Set
    End Property
    Public Property MATranArrDate As String
        Get
            Return _MATranArrDate
        End Get
        Set(ByVal value As String)
            _MATranArrDate = value
        End Set
    End Property
    Public Property MATranDepDate As String
        Get
            Return _MATranDepDate
        End Get
        Set(ByVal value As String)
            _MATranDepDate = value
        End Set
    End Property

    Public Property MATranArrFlightClass As String
        Get
            Return _MATranArrFlightClass
        End Get
        Set(ByVal value As String)
            _MATranArrFlightClass = value
        End Set
    End Property
    Public Property MATranDepFlightClass As String
        Get
            Return _MATranDepFlightClass
        End Get
        Set(ByVal value As String)
            _MATranDepFlightClass = value
        End Set
    End Property
    Public Property MATranArrFlightNo As String
        Get
            Return _MATranArrFlightNo
        End Get
        Set(ByVal value As String)
            _MATranArrFlightNo = value
        End Set
    End Property
    Public Property MATranDepFlightNo As String
        Get
            Return _MATranDepFlightNo
        End Get
        Set(ByVal value As String)
            _MATranDepFlightNo = value
        End Set
    End Property
    Public Property MATranArrFlightTime As String
        Get
            Return _MATranArrFlightTime
        End Get
        Set(ByVal value As String)
            _MATranArrFlightTime = value
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
    Public Property MATranDepFlightTime As String
        Get
            Return _MATranDepFlightTime
        End Get
        Set(ByVal value As String)
            _MATranDepFlightTime = value
        End Set
    End Property
    Public Property MATranArrPickupCode As String
        Get
            Return _MATranArrPickupCode
        End Get
        Set(ByVal value As String)
            _MATranArrPickupCode = value
        End Set
    End Property
    Public Property MATranArrPickupName As String
        Get
            Return _MATranArrPickupName
        End Get
        Set(ByVal value As String)
            _MATranArrPickupName = value
        End Set
    End Property
    Public Property MATranDepDropCode As String
        Get
            Return _MATranDepDropCode
        End Get
        Set(ByVal value As String)
            _MATranDepDropCode = value
        End Set
    End Property
    Public Property MATranDepDropName As String
        Get
            Return _MATranDepDropName
        End Get
        Set(ByVal value As String)
            _MATranDepDropName = value
        End Set
    End Property
    Public Property MATranArrSector As String
        Get
            Return _MATranArrSector
        End Get
        Set(ByVal value As String)
            _MATranArrSector = value
        End Set
    End Property
    Public Property MATranDepSector As String
        Get
            Return _MATranDepSector
        End Get
        Set(ByVal value As String)
            _MATranDepSector = value
        End Set
    End Property

    Public Property MAArrFlightClass As String
        Get
            Return _MAArrFlightClass
        End Get
        Set(ByVal value As String)
            _MAArrFlightClass = value
        End Set
    End Property
    Public Property MADepFlightClass As String
        Get
            Return _MADepFlightClass
        End Get
        Set(ByVal value As String)
            _MADepFlightClass = value
        End Set
    End Property
    Public Property MAArrFlightNo As String
        Get
            Return _MAArrFlightNo
        End Get
        Set(ByVal value As String)
            _MAArrFlightNo = value
        End Set
    End Property
    Public Property MADepFlightNo As String
        Get
            Return _MADepFlightNo
        End Get
        Set(ByVal value As String)
            _MADepFlightNo = value
        End Set
    End Property
    Public Property MAArrFlightTime As String
        Get
            Return _MAArrFlightTime
        End Get
        Set(ByVal value As String)
            _MAArrFlightTime = value
        End Set
    End Property
    Public Property MADepFlightTime As String
        Get
            Return _MADepFlightTime
        End Get
        Set(ByVal value As String)
            _MADepFlightTime = value
        End Set
    End Property
    Public Property MAArrPickupCode As String
        Get
            Return _MAArrPickupCode
        End Get
        Set(ByVal value As String)
            _MAArrPickupCode = value
        End Set
    End Property
    Public Property MAArrPickupName As String
        Get
            Return _MAArrPickupName
        End Get
        Set(ByVal value As String)
            _MAArrPickupName = value
        End Set
    End Property
    Public Property MADepDropCode As String
        Get
            Return _MADepDropCode
        End Get
        Set(ByVal value As String)
            _MADepDropCode = value
        End Set
    End Property
    Public Property MADepDropName As String
        Get
            Return _MADepDropName
        End Get
        Set(ByVal value As String)
            _MADepDropName = value
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

    Public Property Adult As String
        Get
            Return _Adult
        End Get
        Set(ByVal value As String)
            _Adult = value
        End Set
    End Property
    Public Property Children As String
        Get
            Return _Children
        End Get
        Set(ByVal value As String)
            _Children = value
        End Set
    End Property

    Public Property Child1 As String
        Get
            Return _Child1
        End Get
        Set(ByVal value As String)
            _Child1 = value
        End Set
    End Property
    Public Property Child2 As String
        Get
            Return _Child2
        End Get
        Set(ByVal value As String)
            _Child2 = value
        End Set
    End Property
    Public Property Child3 As String
        Get
            Return _Child3
        End Get
        Set(ByVal value As String)
            _Child3 = value
        End Set
    End Property
    Public Property Child4 As String
        Get
            Return _Child4
        End Get
        Set(ByVal value As String)
            _Child4 = value
        End Set
    End Property
    Public Property Child5 As String
        Get
            Return _Child5
        End Get
        Set(ByVal value As String)
            _Child5 = value
        End Set
    End Property
    Public Property Child6 As String
        Get
            Return _Child6
        End Get
        Set(ByVal value As String)
            _Child6 = value
        End Set
    End Property
    Public Property Child7 As String
        Get
            Return _Child7
        End Get
        Set(ByVal value As String)
            _Child7 = value
        End Set
    End Property
    Public Property Child8 As String
        Get
            Return _Child8
        End Get
        Set(ByVal value As String)
            _Child8 = value
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
    Public Property SourceCountry As String
        Get
            Return _SourceCountry
        End Get
        Set(ByVal value As String)
            _SourceCountry = value
        End Set
    End Property

    Public Property SourceCountryCode As String
        Get
            Return _SourceCountryCode
        End Get
        Set(ByVal value As String)
            _SourceCountryCode = value
        End Set
    End Property

    Public Property Customer As String
        Get
            Return _Customer
        End Get
        Set(ByVal value As String)
            _Customer = value
        End Set
    End Property

    Public Property CustomerCode As String
        Get
            Return _CustomerCode
        End Get
        Set(ByVal value As String)
            _CustomerCode = value
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

    Public Property Details As String
        Get
            Return _Details
        End Get
        Set(ByVal value As String)
            _Details = value
        End Set
    End Property

    Public Property AirportMATypeCode As String
        Get
            Return _AirportMATypeCode
        End Get
        Set(ByVal value As String)
            _AirportMATypeCode = value
        End Set
    End Property
    Public Property Units As String
        Get
            Return _Units
        End Get
        Set(ByVal value As String)
            _Units = value
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
    Public Property OBRlinenoString As String
        Get
            Return _OBRlinenoString
        End Get
        Set(ByVal value As String)
            _OBRlinenoString = value
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
    Public Property OBAirportType As String
        Get
            Return _OBAirportType
        End Get
        Set(ByVal value As String)
            _OBAirportType = value
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
        iCumulative = objDALMASearch.FindBookingEnginRateType(sAgentCode)
        Return iCumulative
    End Function
    Function GetSearchDetails() As DataSet
        Dim dsMASearch As New DataSet
        dsMASearch = objDALMASearch.GetMASearchDetails(LoginType, WebUserName, AgentCode, MAArrivalType, MAArrTransferDate, MAArrPickupCode, MADepartueType, MADepTransferDate, MADepDropCode, MATransitType, MATranArrDate, MATranArrPickupCode, MATranDepDate, MATranDepDropCode, Adult, Children, ChildAgeString, SourceCountryCode, OverridePrice, Details, TransferType, AirportMATypeCode, Units, AmendRequestid, AmendLineno)
        Return dsMASearch

    End Function
    Function SavingAMBookingtemp() As Boolean 'renamed function name on 20180724 by abin
        Dim res As Boolean


        objDALMASearch.OBRequestId = OBRequestId
        objDALMASearch.OBTransfertXml = OBTransferXml
        objDALMASearch.UserLogged = UserLogged

        objDALMASearch.OBDiv_Code = OBDiv_Code
        objDALMASearch.OBAgentCode = OBAgentCode
        objDALMASearch.OBSourcectryCode = OBSourcectryCode
        objDALMASearch.OBReqoverRide = OBReqoverRide
        objDALMASearch.OBAgentref = OBAgentref
        objDALMASearch.OBColumbusRef = OBColumbusRef
        objDALMASearch.OBRemarks = OBRemarks
        objDALMASearch.OBRlinenoString = OBRlinenoString
        objDALMASearch.SubUserCode = SubUserCode


        res = objDALMASearch.savingbookingintemp()

        Return res
    End Function

    Function GetAiportMeetSummary(ByVal strRequestId As String, strFrontLabel As String) As DataTable
        Dim dt As New DataTable
        dt = objDALMASearch.GetAiportMeetSummary(strRequestId, strFrontLabel)
        Return dt
    End Function
    Sub RemoveAirportservice(ByVal strRequestId As String, ByVal strtlineno As String)
        Dim objDALMASearch As New DALMASearch
        objDALMASearch.RemoveAirportservice(strRequestId, strtlineno)

    End Sub
    Function GetEditBookingDetails(ByVal strRequestId As String, ByVal stralineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALMASearch.GetEditBookingDetails(strRequestId, stralineno)
        Return dt
    End Function
    Function GetAirportCancelDetails(ByVal strRequestId As String, ByVal stralineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALMASearch.GetAirportCancelDetails(strRequestId, stralineno)
        Return dt
    End Function


    Function SavingCancelAirportInTemp() As Boolean
        Dim res As Boolean
        objDALMASearch.OBRequestId = OBRequestId
        objDALMASearch.OBCancelAirXml = OBCancelAirXml
        objDALMASearch.UserLogged = UserLogged
        res = objDALMASearch.SavingCancelAirportInTemp()
        Return res
    End Function
End Class
