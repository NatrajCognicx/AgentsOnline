Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLHotelFreeFormBooking

    'Private _strHotelCode As String
    'Private _strRoomTypeCode As String
    'Private _strRoomCatCode As String
    'Private _strCustomerCode As String
    'Private _strSourceCountryCode As String
    'Private _strCheckIn As String
    'Private _strCheckOut As String
    'Private _strRoom As String
    'Private _strRoomString As String
    'Private _strHotelString As String
    'Private _strSupplierAgent As String
    'Private _strContractId As String
    'Private _strMealPlancode As String

    'Private _strSelectedEvents As String
    'Private _strRequestId As String
    'Private _strRLineNo As String


    Private _Div_Code As String = ""
    Private _Requestid As String = ""
    Private _AgentCode As String = ""
    Private _SourceCtryCode As String = ""
    Private _AgentRef As String = ""
    Private _ColumbusRef As String = ""
    Private _Remarks As String = ""
    Private _SubUserCode As String = ""

    Private _RlineNo As String
    Private _CheckIn As String = ""
    Private _CheckOut As String = ""
    Private _NoofRooms As String = ""
    Private _Adults As String = ""
    Private _Child As String = ""
    Private _ChildAges As String = ""
    Private _SharingOrExtraBed As String = ""
    Private _NoofAdultEb As String = ""
    Private _NoofChildeb As String = ""
    Private _supagentcode As String = ""
    Private _partycode As String = ""
    Private _rateplanid As String = ""
    Private _rateplanname As String = ""
    Private _rmtypcode As String = ""
    Private _roomclasscode As String = ""
    Private _rmcatcode As String = ""
    Private _mealplans As String = ""
    Private _salevalue As String = ""
    Private _salecurrcode As String = ""
    Private _costvalue As String = ""
    Private _costcurrcode As String = ""
    Private _wlsalevalue As String = ""
    Private _available As String = ""
    Private _comp_cust As String = ""
    Private _comp_supp As String = ""
    Private _comparrtrf As String = ""
    Private _compdeptrf As String = ""
    Private _RoomString As String = ""
    Private _Shifting As String = ""
    Private _ShiftingCode As String = ""
    Private _ShiftingLineNo As String = ""
    Private _wlcurrcode As String = ""
    Private _overrideprice As String = ""

    Private _PriceBreakupXMLInput As String = ""
    'Special Events
    Private _specialxml As String = ""
    Private _userlogged As String = ""
    Private _BookingMode As String = ""
    Private _RoomRankOrder As String = ""
    Private _NonRefundable As String = ""
    Private _CancelFreeUpto As String = ""


    Public Property Div_Code As String
        Get
            Return _Div_Code
        End Get
        Set(ByVal value As String)
            _Div_Code = value
        End Set
    End Property
    Public Property Requestid As String
        Get
            Return _Requestid
        End Get
        Set(ByVal value As String)
            _Requestid = value
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
    Public Property SourceCtryCode As String
        Get
            Return _SourceCtryCode
        End Get
        Set(ByVal value As String)
            _SourceCtryCode = value
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
    Public Property ColumbusRef As String
        Get
            Return _ColumbusRef
        End Get
        Set(ByVal value As String)
            _ColumbusRef = value
        End Set
    End Property
    Public Property Remarks As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property

    Public Property RlineNo As String
        Get
            Return _RlineNo
        End Get
        Set(ByVal value As String)
            _RlineNo = value
        End Set
    End Property
    Public Property CheckIn As String
        Get
            Return _CheckIn
        End Get
        Set(ByVal value As String)
            _CheckIn = value
        End Set
    End Property
    Public Property CheckOut As String
        Get
            Return _CheckOut
        End Get
        Set(ByVal value As String)
            _CheckOut = value
        End Set
    End Property
    Public Property NoofRooms As String
        Get
            Return _NoofRooms
        End Get
        Set(ByVal value As String)
            _NoofRooms = value
        End Set
    End Property

    Public Property Adults As String
        Get
            Return _Adults
        End Get
        Set(ByVal value As String)
            _Adults = value
        End Set
    End Property


    Public Property Child As String
        Get
            Return _Child
        End Get
        Set(ByVal value As String)
            _Child = value
        End Set
    End Property

    Public Property ChildAges As String
        Get
            Return _ChildAges
        End Get
        Set(ByVal value As String)
            _ChildAges = value
        End Set
    End Property


    Public Property SharingOrExtraBed As String
        Get
            Return _SharingOrExtraBed
        End Get
        Set(ByVal value As String)
            _SharingOrExtraBed = value
        End Set
    End Property
    Public Property NoofAdultEb As String
        Get
            Return _NoofAdultEb
        End Get
        Set(ByVal value As String)
            _NoofAdultEb = value
        End Set
    End Property
    Public Property NoofChildEb As String
        Get
            Return _NoofChildeb
        End Get
        Set(ByVal value As String)
            _NoofChildeb = value
        End Set
    End Property
    Public Property SupAgentCode As String
        Get
            Return _supagentcode
        End Get
        Set(ByVal value As String)
            _supagentcode = value
        End Set
    End Property
    Public Property PartyCode As String
        Get
            Return _PartyCode
        End Get
        Set(ByVal value As String)
            _partycode = value
        End Set
    End Property
    Public Property RateplanId As String
        Get
            Return _rateplanid
        End Get
        Set(ByVal value As String)
            _rateplanid = value
        End Set
    End Property
    Public Property RatePlanName As String
        Get
            Return _rateplanname
        End Get
        Set(ByVal value As String)
            _rateplanname = value
        End Set
    End Property
    Public Property RoomTypeCode As String
        Get
            Return _rmtypcode
        End Get
        Set(ByVal value As String)
            _rmtypcode = value
        End Set
    End Property
    Public Property RoomClassCode As String
        Get
            Return _roomclasscode
        End Get
        Set(ByVal value As String)
            _roomclasscode = value
        End Set
    End Property
    Public Property RoomCatCode As String
        Get
            Return _rmcatcode
        End Get
        Set(ByVal value As String)
            _rmcatcode = value
        End Set
    End Property
    Public Property MealPlans As String
        Get
            Return _mealplans
        End Get
        Set(ByVal value As String)
            _mealplans = value
        End Set
    End Property
    Public Property SaleValue As String
        Get
            Return _salevalue
        End Get
        Set(ByVal value As String)
            _salevalue = value
        End Set
    End Property
    Public Property SaleCurrCode As String
        Get
            Return _salecurrcode
        End Get
        Set(ByVal value As String)
            _salecurrcode = value
        End Set
    End Property
    Public Property CostValue As String
        Get
            Return _costvalue
        End Get
        Set(ByVal value As String)
            _costvalue = value
        End Set
    End Property
    Public Property CostCurrCode As String
        Get
            Return _costcurrcode
        End Get
        Set(ByVal value As String)
            _costcurrcode = value
        End Set
    End Property
    Public Property WlSaleValue As String
        Get
            Return _wlsalevalue
        End Get
        Set(ByVal value As String)
            _wlsalevalue = value
        End Set
    End Property
    Public Property Available As String
        Get
            Return _available
        End Get
        Set(ByVal value As String)
            _available = value
        End Set
    End Property
    Public Property Comp_Cust As String
        Get
            Return _comp_cust
        End Get
        Set(ByVal value As String)
            _comp_cust = value
        End Set
    End Property
    Public Property Comp_Supp As String
        Get
            Return _comp_supp
        End Get
        Set(ByVal value As String)
            _comp_supp = value
        End Set
    End Property
    Public Property Comparrtrf As String
        Get
            Return _comparrtrf
        End Get
        Set(ByVal value As String)
            _comparrtrf = value
        End Set
    End Property

    Public Property Compdeptrf As String
        Get
            Return _compdeptrf
        End Get
        Set(ByVal value As String)
            _compdeptrf = value
        End Set
    End Property
    Public Property RoomString As String
        Get
            Return _RoomString
        End Get
        Set(ByVal value As String)
            _RoomString = value
        End Set
    End Property
    Public Property Shifting As String
        Get
            Return _Shifting
        End Get
        Set(ByVal value As String)
            _Shifting = value
        End Set
    End Property
    Public Property ShiftingCode As String
        Get
            Return _ShiftingCode
        End Get
        Set(ByVal value As String)
            _ShiftingCode = value
        End Set
    End Property
    Public Property ShiftingLineNo As String
        Get
            Return _ShiftingLineNo
        End Get
        Set(ByVal value As String)
            _ShiftingLineNo = value
        End Set
    End Property
    Public Property WlCurrCode As String
        Get
            Return _wlcurrcode
        End Get
        Set(ByVal value As String)
            _wlcurrcode = value
        End Set
    End Property
    Public Property OverridePrice As String
        Get
            Return _overrideprice
        End Get
        Set(ByVal value As String)
            _overrideprice = value
        End Set
    End Property
    Public Property PriceBreakupXMLInput As String
        Get
            Return _PriceBreakupXMLInput
        End Get
        Set(ByVal value As String)
            _PriceBreakupXMLInput = value
        End Set
    End Property
    Public Property SpecialEventsXML As String
        Get
            Return _specialxml
        End Get
        Set(ByVal value As String)
            _specialxml = value
        End Set
    End Property
    'Special Events
    Public Property UserLogged As String
        Get
            Return _userlogged
        End Get
        Set(ByVal value As String)
            _userlogged = value
        End Set
    End Property

    Public Property BookingMode As String
        Get
            Return _BookingMode
        End Get
        Set(ByVal value As String)
            _BookingMode = value
        End Set
    End Property


    Public Property RoomRankOrder As String
        Get
            Return _RoomRankOrder
        End Get
        Set(ByVal value As String)
            _RoomRankOrder = value
        End Set
    End Property
    Public Property NonRefundable As String
        Get
            Return _NonRefundable
        End Get
        Set(ByVal value As String)
            _NonRefundable = value
        End Set
    End Property
    Public Property CancelFreeUpto As String
        Get
            Return _CancelFreeUpto
        End Get
        Set(ByVal value As String)
            _CancelFreeUpto = value
        End Set
    End Property

    Function GetFreeFormPricesBasedOnRooms(strCheckIn As String, strCheckOut As String, strRoom As String, strRoomString As String, strHotelCode As String, strAgentCode As String, strRandomNumber As String) As Data.DataTable
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        Dim dt As DataTable
        dt = objDALHotelFreeFormBooking.GetFreeFormPricesBasedOnRooms(strCheckIn, strCheckOut, strRoom, strRoomString, strHotelCode, strAgentCode, strRandomNumber)
        Return dt
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strHotelCode"></param>
    ''' <param name="strRoomTypeCode"></param>
    ''' <param name="strMealPlanCode"></param>
    ''' <param name="strRoomCatCode"></param>
    ''' <param name="strCustomerCode"></param>
    ''' <param name="strSourceCountryCode"></param>
    ''' <param name="strCheckIn"></param>
    ''' <param name="strCheckOut"></param>
    ''' <param name="strRoom"></param>
    ''' <param name="strRoomString"></param>
    ''' <param name="strHotelString"></param>
    ''' <param name="strSelectedEvents"></param>
    ''' <param name="strRequestId"></param>
    ''' <param name="strRLineNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFreeFormSpecialEvents(strHotelCode As String, strRoomTypeCode As String, strMealPlanCode As String, strRoomCatCode As String, strCustomerCode As String, strSourceCountryCode As String, strCheckIn As String, strCheckOut As String, strRoom As String, strRoomString As String, strHotelString As String, strSelectedEvents As String, strRequestId As String, strRLineNo As String) As DataSet
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        Dim ds As DataSet
        ds = objDALHotelFreeFormBooking.GetFreeFormSpecialEvents(strHotelCode, strRoomTypeCode, strMealPlanCode, strRoomCatCode, strCustomerCode, strSourceCountryCode, strCheckIn, strCheckOut, strRoom, strRoomString, strHotelString, strSelectedEvents, strRequestId, strRLineNo)
        Return ds
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strHotelCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSpecialEvents(strHotelCode As String) As DataTable
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        Dim dt As DataTable
        dt = objDALHotelFreeFormBooking.GetSpecialEvents(strHotelCode)
        Return dt
    End Function

    Function GetAccomodationMasterDetails(strAutoId As String) As DataTable
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        Dim dt As DataTable
        dt = objDALHotelFreeFormBooking.GetAccomodationMasterDetails(strAutoId)
        Return dt
    End Function

    Function SaveHotelFreeFormBooking() As String
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        objDALHotelFreeFormBooking.Div_Code = Div_Code
        objDALHotelFreeFormBooking.Requestid = Requestid
        objDALHotelFreeFormBooking.AgentCode = AgentCode
        objDALHotelFreeFormBooking.SourceCtryCode = SourceCtryCode
        objDALHotelFreeFormBooking.AgentRef = AgentRef
        objDALHotelFreeFormBooking.ColumbusRef = ColumbusRef
        objDALHotelFreeFormBooking.Remarks = Remarks
        objDALHotelFreeFormBooking.SubUserCode = SubUserCode
        objDALHotelFreeFormBooking.RlineNo = RlineNo
        objDALHotelFreeFormBooking.CheckIn = CheckIn
        objDALHotelFreeFormBooking.CheckOut = CheckOut
        objDALHotelFreeFormBooking.NoofRooms = NoofRooms
        objDALHotelFreeFormBooking.Adults = Adults
        objDALHotelFreeFormBooking.Child = Child
        objDALHotelFreeFormBooking.ChildAges = ChildAges
        objDALHotelFreeFormBooking.SharingOrExtraBed = SharingOrExtraBed
        objDALHotelFreeFormBooking.NoofAdultEb = NoofAdultEb
        objDALHotelFreeFormBooking.NoofChildEb = NoofChildEb
        objDALHotelFreeFormBooking.SupAgentCode = SupAgentCode
        objDALHotelFreeFormBooking.PartyCode = PartyCode
        objDALHotelFreeFormBooking.RateplanId = RateplanId
        objDALHotelFreeFormBooking.RatePlanName = RatePlanName
        objDALHotelFreeFormBooking.RoomTypeCode = RoomTypeCode
        objDALHotelFreeFormBooking.RoomClassCode = RoomClassCode
        objDALHotelFreeFormBooking.RoomCatCode = RoomCatCode
        objDALHotelFreeFormBooking.MealPlans = MealPlans
        objDALHotelFreeFormBooking.SaleValue = SaleValue
        objDALHotelFreeFormBooking.SaleCurrCode = SaleCurrCode
        objDALHotelFreeFormBooking.CostValue = CostValue
        objDALHotelFreeFormBooking.CostCurrCode = CostCurrCode
        objDALHotelFreeFormBooking.WlSaleValue = WlSaleValue
        objDALHotelFreeFormBooking.Available = Available
        objDALHotelFreeFormBooking.Comp_Cust = Comp_Cust
        objDALHotelFreeFormBooking.Comp_Supp = Comp_Supp
        objDALHotelFreeFormBooking.Comparrtrf = Comparrtrf
        objDALHotelFreeFormBooking.Compdeptrf = Compdeptrf
        objDALHotelFreeFormBooking.RoomString = RoomString
        objDALHotelFreeFormBooking.Shifting = Shifting
        objDALHotelFreeFormBooking.ShiftingCode = ShiftingCode
        objDALHotelFreeFormBooking.WlCurrCode = WlCurrCode
        objDALHotelFreeFormBooking.OverridePrice = OverridePrice
        objDALHotelFreeFormBooking.PriceBreakupXMLInput = PriceBreakupXMLInput
        objDALHotelFreeFormBooking.SpecialEventsXML = SpecialEventsXML
        objDALHotelFreeFormBooking.UserLogged = UserLogged
        objDALHotelFreeFormBooking.BookingMode = BookingMode
        objDALHotelFreeFormBooking.RoomRankOrder = RoomRankOrder
        objDALHotelFreeFormBooking.NonRefundable = NonRefundable
        objDALHotelFreeFormBooking.CancelFreeUpto = CancelFreeUpto

        Dim strStatus As String = objDALHotelFreeFormBooking.SaveFreeFormHotelBookinging()
        Return strStatus
    End Function
    Function GetHotelFreeFormBookingDetailsForEdit(ByVal strRequestId As Object, ByVal strRLineNo As String) As DataSet
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        Dim ds As DataSet
        ds = objDALHotelFreeFormBooking.GetHotelFreeFormBookingDetailsForEdit(strRequestId, strRLineNo)
        Return ds
    End Function

    Function GetDefaultSupplierAgent() As DataTable
        Dim objDALHotelFreeFormBooking As New DALHotelFreeFormBooking()
        Dim dt As DataTable
        dt = objDALHotelFreeFormBooking.GetDefaultSupplierAgent()
        Return dt
    End Function

End Class
