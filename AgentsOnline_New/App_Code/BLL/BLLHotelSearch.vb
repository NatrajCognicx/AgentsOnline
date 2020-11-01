Imports Microsoft.VisualBasic
Imports System.Data


Public Class BLLHotelSearch

    Dim _Destination As String = ""
    Dim _DestinationCode As String = ""
    Dim _DestinationType As String = ""
    Dim _DestinationCodeAndType As String = ""
    Dim _CheckIn As String = ""
    Dim _CheckOut As String = ""
    Dim _NoOfNights As String = ""
    Dim _Room As String = ""
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
    Dim _SourceCountry As String = ""
    Dim _SourceCountryCode As String = ""
    Dim _OrderBy As String = ""
    Dim _Customer As String = ""
    Dim _CustomerCode As String = ""
    Dim _StarCategory As String = ""
    Dim _StarCategoryCode As String = ""
    Dim _Availabilty As String = ""
    Dim _PropertyType As String = ""
    Dim _Hotels As String = ""
    Dim _HotelCode As String = ""
    Dim _LoginType As String = ""
    Dim _WebUserName As String = ""
    Dim _AgentCode As String = ""
    Dim _ChildAgeString As String = ""
    Dim _OverridePrice As String = ""
    Dim _QueryString As String = ""
    Dim _FilterRoomClass As String = ""

    Dim _Mealplan As String = "" ''** Shahul 26/06/2018
    Dim _ShowallCategory As String = "" ''** Shahul 26/06/2018

    Dim _OBdiv_code As String = ""
    Dim _requestid As String = ""
    Dim _OBRequestId As String = ""
    Dim _OBagentcode As String = ""
    Dim _OBsourcectrycode As String = ""
    Dim _OBreqoverride As String = ""
    Dim _OBagentref As String = ""
    Dim _OBremarks As String = ""
    Dim _OBcolumbusref As String = ""
    Dim _userlogged As String = ""
    Dim _OBrlineno As Integer
    Dim _OBnoofrooms As Integer
    Dim _OBadults As Integer
    Dim _OBchild As Integer
    Dim _OBchildages As String
    Dim _OBsupagentcode As String = ""
    Dim _OBpartycode As String = ""
    Dim _OBrateplanid As String
    Dim _OBrateplanname As String = ""
    Dim _OBrmtypcode As String = ""
    Dim _OBroomclasscode As String = ""
    Dim _OBrmcatcode As String = ""
    Dim _OBaccommodationid As Integer
    Dim _OBCheckIn As String = ""
    Dim _OBCheckOut As String = ""
    Dim _OBmealplans As String
    Dim _OBsalevalue As String
    Dim _OBsalecurrcode As String = ""
    Dim _OBcostvalue As String
    Dim _OBcostcurrcode As String = ""
    Dim _OBwlsalevalue As String = ""
    Dim _OBavailable As Integer
    Dim _OBcomp_cust As Integer
    Dim _OBcomp_supp As Integer
    Dim _OBcomparrtrf As Integer
    Dim _OBcompdeptrf As Integer
    Dim _obpricebreakuptemp As String = ""
    Dim _SpecialEventXML As String = ""
    Dim _SharingOrExtraBed As String = ""
    Dim _NoOfAdultEb As String = "0"
    Dim _NoOfChildEb As String = "0"
    Dim _RoomString As String = ""
    Dim _Shifting As String = "0"
    Dim _ShiftingCode As String = ""

    Public _ShiftingLineNo As String = ""
    Public _EditRequestId As String = ""
    Public _EditRLineNo As String = ""
    Public _EditRatePlanId As String = ""
    Public _AmendMode As String = "0"
    Public _wlCurrCode As String = ""


    Private _PreHotelRLineNo As String = ""
    Private _PreHotelCheckIn As String = ""
    Private _PreHotelCheckout As String = ""
    Private _PreHotelPartyCode As String = ""
    Private _PreHotelAdults As String = ""
    Private _PreHotelChild As String = ""
    Private _PreHotelChildages As String = ""
    Private _PreHotelSectorCode As String = ""
    Private _OneTimePayXML As String = ""
    'changed by mohamed on 09/04/2018
    Private _PreArrangedShifting As String = "0"
    Private _PreArrangedShiftingCode As String = ""
    Private _PreArrangedShiftingLineNo As String = ""
    Dim _PreShowHotel As String = "" ''** Shahul 10/11/2018


    Dim objDALHotelSearch As New DALHotelSearch

    Dim _accomodationcode As String = ""
    Public Property Accomodationcode As String
        Get
            Return _accomodationcode
        End Get
        Set(ByVal value As String)
            _accomodationcode = value
        End Set
    End Property


    Dim _offercode As String = ""
    Public Property Offercode As String
        Get
            Return _offercode
        End Get
        Set(ByVal value As String)
            _offercode = value
        End Set
    End Property

    Dim _Int_PartyCode As String = ""
    Public Property Int_PartyCode As String
        Get
            Return _Int_PartyCode
        End Get
        Set(ByVal value As String)
            _Int_PartyCode = value
        End Set
    End Property
    Dim _Int_rmtypecode As String = ""
    Public Property Int_rmtypecode As String
        Get
            Return _Int_rmtypecode
        End Get
        Set(ByVal value As String)
            _Int_rmtypecode = value
        End Set
    End Property
    Dim _Int_mealcode As String = ""
    Public Property Int_mealcode As String
        Get
            Return _Int_mealcode
        End Get
        Set(ByVal value As String)
            _Int_mealcode = value
        End Set
    End Property
    Dim _Int_costprice As Decimal = 0
    Public Property Int_costprice As Decimal
        Get
            Return _Int_costprice
        End Get
        Set(ByVal value As Decimal)
            _Int_costprice = value
        End Set
    End Property
    Dim _Int_costcurrcode As String = ""
    Public Property Int_costcurrcode As String
        Get
            Return _Int_costcurrcode
        End Get
        Set(ByVal value As String)
            _Int_costcurrcode = value
        End Set
    End Property

    Dim _Int_RoomtypeCodes As String = ""
    Public Property Int_RoomtypeCodes As String
        Get
            Return _Int_RoomtypeCodes
        End Get
        Set(ByVal value As String)
            _Int_RoomtypeCodes = value
        End Set
    End Property
    Dim _Int_RoomtypeNames As String = ""
    Public Property Int_RoomtypeNames As String
        Get
            Return _Int_RoomtypeNames
        End Get
        Set(ByVal value As String)
            _Int_RoomtypeNames = value
        End Set
    End Property

    Dim _Int_Roomtypes As String = ""
    Public Property Int_Roomtypes As String
        Get
            Return _Int_Roomtypes
        End Get
        Set(ByVal value As String)
            _Int_Roomtypes = value
        End Set
    End Property
  

    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Public Property Destination As String
        Get
            Return _Destination
        End Get
        Set(ByVal value As String)
            _Destination = value
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
    Public Property DestinationCodeAndType As String
        Get
            Return _DestinationCodeAndType
        End Get
        Set(ByVal value As String)
            _DestinationCodeAndType = value
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
    Public Property NoOfNights As String
        Get
            Return _NoOfNights
        End Get
        Set(ByVal value As String)
            _NoOfNights = value
        End Set
    End Property

    Public Property Room As String
        Get
            Return _Room
        End Get
        Set(ByVal value As String)
            _Room = value
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
    Public Property OrderBy As String
        Get
            Return _OrderBy
        End Get
        Set(ByVal value As String)
            _OrderBy = value
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
    Public Property StarCategory As String
        Get
            Return _StarCategory
        End Get
        Set(ByVal value As String)
            _StarCategory = value
        End Set
    End Property
    Public Property StarCategoryCode As String
        Get
            Return _StarCategoryCode
        End Get
        Set(ByVal value As String)
            _StarCategoryCode = value
        End Set
    End Property
    Public Property Availabilty As String
        Get
            Return _Availabilty
        End Get
        Set(ByVal value As String)
            _Availabilty = value
        End Set
    End Property
    Public Property PropertyType As String
        Get
            Return _PropertyType
        End Get
        Set(ByVal value As String)
            _PropertyType = value
        End Set
    End Property

    Public Property Hotels As String
        Get
            Return _Hotels
        End Get
        Set(ByVal value As String)
            _Hotels = value
        End Set
    End Property
    Public Property HotelCode As String
        Get
            Return _HotelCode
        End Get
        Set(ByVal value As String)
            _HotelCode = value
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
    Public Property AgentCode As String
        Get
            Return _AgentCode
        End Get
        Set(ByVal value As String)
            _AgentCode = value
        End Set
    End Property
    Public Property WebuserName As String
        Get
            Return _WebUserName
        End Get
        Set(ByVal value As String)
            _WebUserName = value
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
    Public Property OverridePrice As String
        Get
            Return _OverridePrice
        End Get
        Set(ByVal value As String)
            _OverridePrice = value
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


    Public Property OBdiv_code As String
        Get
            Return _OBdiv_code
        End Get
        Set(ByVal value As String)
            _OBdiv_code = value
        End Set
    End Property
    Public Property OBrequestid As String
        Get
            Return _OBrequestid
        End Get
        Set(ByVal value As String)
            _OBrequestid = value
        End Set
    End Property
    Public Property OBagentcode As String
        Get
            Return _OBagentcode
        End Get
        Set(ByVal value As String)
            _OBagentcode = value
        End Set
    End Property
    Public Property OBsourcectrycode As String
        Get
            Return _OBsourcectrycode
        End Get
        Set(ByVal value As String)
            _OBsourcectrycode = value
        End Set
    End Property
    Public Property OBreqoverride As String
        Get
            Return _OBreqoverride
        End Get
        Set(ByVal value As String)
            _OBreqoverride = value
        End Set
    End Property
    Public Property OBagentref As String
        Get
            Return _OBagentref
        End Get
        Set(ByVal value As String)
            _OBagentref = value
        End Set
    End Property
    Public Property OBcolumbusref As String
        Get
            Return _OBcolumbusref
        End Get
        Set(ByVal value As String)
            _OBcolumbusref = value
        End Set
    End Property
    Public Property OBremarks As String
        Get
            Return _OBremarks
        End Get
        Set(ByVal value As String)
            _OBremarks = value
        End Set
    End Property
    Public Property userlogged As String
        Get
            Return _userlogged
        End Get
        Set(ByVal value As String)
            _userlogged = value
        End Set
    End Property
    Public Property OBrlineno As Integer
        Get
            Return _OBrlineno
        End Get
        Set(ByVal value As Integer)
            _OBrlineno = value
        End Set
    End Property




    Public Property OBnoofrooms As Integer
        Get
            Return _OBnoofrooms
        End Get
        Set(ByVal value As Integer)
            _OBnoofrooms = value
        End Set
    End Property
    Public Property OBadults As Integer
        Get
            Return _OBadults
        End Get
        Set(ByVal value As Integer)
            _OBadults = value
        End Set
    End Property

    Public Property OBchild As Integer
        Get
            Return _OBchild
        End Get
        Set(ByVal value As Integer)
            _OBchild = value
        End Set
    End Property
    Public Property OBchildages As String
        Get
            Return _OBchildages
        End Get
        Set(ByVal value As String)
            _OBchildages = value
        End Set
    End Property


    Public Property OBsupagentcode As String
        Get
            Return _OBsupagentcode
        End Get
        Set(ByVal value As String)
            _OBsupagentcode = value
        End Set
    End Property


    Public Property OBpartycode As String
        Get
            Return _OBpartycode
        End Get
        Set(ByVal value As String)
            _OBpartycode = value
        End Set
    End Property


    Public Property OBrateplanid As String
        Get
            Return _OBrateplanid
        End Get
        Set(ByVal value As String)
            _OBrateplanid = value
        End Set
    End Property
    Public Property OBrateplanname As String
        Get
            Return _OBrateplanname
        End Get
        Set(ByVal value As String)
            _OBrateplanname = value
        End Set
    End Property


    Public Property OBrmtypcode As String
        Get
            Return _OBrmtypcode
        End Get
        Set(ByVal value As String)
            _OBrmtypcode = value
        End Set
    End Property



    Public Property OBroomclasscode As String
        Get
            Return _OBroomclasscode
        End Get
        Set(ByVal value As String)
            _OBroomclasscode = value
        End Set
    End Property
    Public Property OBrmcatcode As String
        Get
            Return _OBrmcatcode
        End Get
        Set(ByVal value As String)
            _OBrmcatcode = value
        End Set
    End Property


    Public Property OBaccommodationid As Integer
        Get
            Return _OBaccommodationid
        End Get
        Set(ByVal value As Integer)
            _OBaccommodationid = value
        End Set
    End Property
    Public Property OBmealplans As String
        Get
            Return _OBmealplans
        End Get
        Set(ByVal value As String)
            _OBmealplans = value
        End Set
    End Property
    Public Property OBsalevalue As Decimal
        Get
            Return _OBsalevalue
        End Get
        Set(ByVal value As Decimal)
            _OBsalevalue = value
        End Set
    End Property



    Public Property OBsalecurrcode As String
        Get
            Return _OBsalecurrcode
        End Get
        Set(ByVal value As String)
            _OBsalecurrcode = value
        End Set
    End Property
    Public Property OBcostvalue As Decimal
        Get
            Return _OBcostvalue
        End Get
        Set(ByVal value As Decimal)
            _OBcostvalue = value
        End Set
    End Property

    Public Property OBcostcurrcode As String
        Get
            Return _OBcostcurrcode
        End Get
        Set(ByVal value As String)
            _OBcostcurrcode = value
        End Set
    End Property
    Public Property OBCheckin As String
        Get
            Return _OBCheckIn
        End Get
        Set(ByVal value As String)
            _OBCheckIn = value
        End Set
    End Property
    Public Property OBCheckout As String
        Get
            Return _OBCheckOut
        End Get
        Set(ByVal value As String)
            _OBCheckOut = value
        End Set
    End Property

    Public Property OBwlsalevalue As Decimal
        Get
            Return _OBwlsalevalue
        End Get
        Set(ByVal value As Decimal)
            _OBwlsalevalue = value
        End Set
    End Property

    Public Property OBavailable As Integer
        Get
            Return _OBavailable
        End Get
        Set(ByVal value As Integer)
            _OBavailable = value
        End Set
    End Property
    Public Property obpricebreakuptemp As String
        Get
            Return _obpricebreakuptemp
        End Get
        Set(ByVal value As String)
            _obpricebreakuptemp = value
        End Set
    End Property
    Public Property OBcomp_cust As Integer
        Get
            Return _OBcomp_cust
        End Get
        Set(ByVal value As Integer)
            _OBcomp_cust = value
        End Set
    End Property

    Public Property OBcomp_supp As Integer
        Get
            Return _OBcomp_supp
        End Get
        Set(ByVal value As Integer)
            _OBcomp_supp = value
        End Set
    End Property
    Public Property OBcomparrtrf As Integer
        Get
            Return _OBcomparrtrf
        End Get
        Set(ByVal value As Integer)
            _OBcomparrtrf = value
        End Set
    End Property

    Public Property OBcompdeptrf As Integer
        Get
            Return _OBcompdeptrf
        End Get
        Set(ByVal value As Integer)
            _OBcompdeptrf = value
        End Set
    End Property

    Public Property SpecialEventXML As String
        Get
            Return _SpecialEventXML
        End Get
        Set(ByVal value As String)
            _SpecialEventXML = value
        End Set
    End Property

    Public Property OneTimePayXML As String
        Get
            Return _OneTimePayXML
        End Get
        Set(ByVal value As String)
            _OneTimePayXML = value
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
    Public Property NoOfAdultEb As String
        Get
            Return _NoOfAdultEb
        End Get
        Set(ByVal value As String)
            _NoOfAdultEb = value
        End Set
    End Property
    Public Property NoOfChildEb As String
        Get
            Return _NoOfChildEb
        End Get
        Set(ByVal value As String)
            _NoOfChildEb = value
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

    Public Property wlCurrCode As String
        Get
            Return _wlCurrCode
        End Get
        Set(ByVal value As String)
            _wlCurrCode = value
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


    Public Property EditRequestId As String
        Get
            Return _EditRequestId
        End Get
        Set(ByVal value As String)
            _EditRequestId = value
        End Set
    End Property


    Public Property EditRLineNo As String
        Get
            Return _EditRLineNo
        End Get
        Set(ByVal value As String)
            _EditRLineNo = value
        End Set
    End Property


    Public Property EditRatePlanId As String
        Get
            Return _EditRatePlanId
        End Get
        Set(ByVal value As String)
            _EditRatePlanId = value
        End Set
    End Property

    Public Property AmendMode As String
        Get
            Return _AmendMode
        End Get
        Set(ByVal value As String)
            _AmendMode = value
        End Set
    End Property

    Public Property PreHotelRLineNo As String
        Get
            Return _PreHotelRLineNo
        End Get
        Set(ByVal value As String)
            _PreHotelRLineNo = value
        End Set
    End Property
    Public Property PreHotelCheckIn As String
        Get
            Return _PreHotelCheckIn
        End Get
        Set(ByVal value As String)
            _PreHotelCheckIn = value
        End Set
    End Property

    Public Property PreHotelCheckout As String
        Get
            Return _PreHotelCheckout
        End Get
        Set(ByVal value As String)
            _PreHotelCheckout = value
        End Set
    End Property

    Public Property PreHotelPartyCode As String
        Get
            Return _PreHotelPartyCode
        End Get
        Set(ByVal value As String)
            _PreHotelPartyCode = value
        End Set
    End Property

    Public Property PreHotelAdults As String
        Get
            Return _PreHotelAdults
        End Get
        Set(ByVal value As String)
            _PreHotelAdults = value
        End Set
    End Property
    Public Property PreHotelChild As String
        Get
            Return _PreHotelChild
        End Get
        Set(ByVal value As String)
            _PreHotelChild = value
        End Set
    End Property
    Public Property PreHotelChildages As String
        Get
            Return _PreHotelChildages
        End Get
        Set(ByVal value As String)
            _PreHotelChildages = value
        End Set
    End Property

    Public Property PreHotelSectorCode As String
        Get
            Return _PreHotelSectorCode
        End Get
        Set(ByVal value As String)
            _PreHotelSectorCode = value
        End Set
    End Property

    Public Property PreArrangedShifting As String
        Get
            Return _PreArrangedShifting
        End Get
        Set(ByVal value As String)
            _PreArrangedShifting = value
        End Set
    End Property

    Public Property PreArrangedShiftingCode As String
        Get
            Return _PreArrangedShiftingCode
        End Get
        Set(ByVal value As String)
            _PreArrangedShiftingCode = value
        End Set
    End Property

    Public Property PreArrangedShiftingLineNo As String
        Get
            Return _PreArrangedShiftingLineNo
        End Get
        Set(ByVal value As String)
            _PreArrangedShiftingLineNo = value
        End Set
    End Property



    Function FindBookingEnginRateType(ByVal sAgentCode As String) As Integer
        Dim iCumulative As Integer = 0
        iCumulative = objDALHotelSearch.FindBookingEnginRateType(sAgentCode)
        Return iCumulative
    End Function

    Function FindWhiteLabel(ByVal sAgentCode As String) As Integer
        Dim strOwnCompany As String = 0
        strOwnCompany = objDALHotelSearch.FindWhiteLabel(sAgentCode)
        Return strOwnCompany
    End Function
    Public Property MealPlan As String
        Get
            Return _Mealplan
        End Get
        Set(ByVal value As String)
            _Mealplan = value
        End Set
    End Property
    Public Property ShowallCategory As String
        Get
            Return _ShowallCategory
        End Get
        Set(ByVal value As String)
            _ShowallCategory = value
        End Set
    End Property
    Public Property PreShowHotel As String
        Get
            Return _PreShowHotel
        End Get
        Set(ByVal value As String)
            _PreShowHotel = value
        End Set
    End Property


    Private _RatePlanSource As String = ""
    Public Property RatePlanSource As String
        Get
            Return _RatePlanSource
        End Get
        Set(ByVal value As String)
            _RatePlanSource = value
        End Set
    End Property

    'Function GetSearchDetails() As DataSet
    '    Dim dsHotelSearch As New DataSet
    '    dsHotelSearch = objDALHotelSearch.GetSearchDetails(LoginType, WebuserName, AgentCode, DestinationType, DestinationCode, CheckIn, CheckOut, Room, RoomString, SourceCountryCode, OrderBy, StarCategoryCode, Availabilty, PropertyType, HotelCode, OverridePrice, FilterRoomClass)
    '    Return dsHotelSearch

    'End Function

    Function SavePreArrangedHotelBookinginTemp() As Boolean
        Dim res As Boolean
        objDALHotelSearch.obdiv_code = OBdiv_code
        objDALHotelSearch.obrequestid = OBrequestid
        objDALHotelSearch.agentcode = AgentCode
        objDALHotelSearch.obsourcectrycode = OBsourcectrycode
        objDALHotelSearch.obreqoverride = OBreqoverride
        objDALHotelSearch.obagentref = OBagentref
        objDALHotelSearch.obcolumbusref = OBcolumbusref
        objDALHotelSearch.obremarks = OBremarks
        objDALHotelSearch.userlogged = userlogged

        objDALHotelSearch.PreHotelRLineNo = PreHotelRLineNo
        objDALHotelSearch.PreHotelCheckIn = PreHotelCheckIn
        objDALHotelSearch.PreHotelCheckout = PreHotelCheckout
        objDALHotelSearch.PreHotelPartyCode = PreHotelPartyCode
        objDALHotelSearch.PreHotelSectorCode = PreHotelSectorCode
        objDALHotelSearch.PreHotelAdults = PreHotelAdults
        objDALHotelSearch.PreHotelChild = PreHotelChild
        objDALHotelSearch.PreHotelChildages = PreHotelChildages

        objDALHotelSearch.PreArrangedShifting = PreArrangedShifting
        objDALHotelSearch.PreArrangedShiftingCode = PreArrangedShiftingCode
        objDALHotelSearch.PreArrangedShiftingLineNo = PreArrangedShiftingLineNo

        objDALHotelSearch.PreShowHotel = PreShowHotel '' Added shahul 10/11/2018

        res = objDALHotelSearch.SavePreArrangedHotelBookinginTemp()
        Return res
    End Function


    Function savingbookingintemp() As String
        Dim res As String

        objDALHotelSearch.obdiv_code = OBdiv_code
        objDALHotelSearch.obrequestid = OBrequestid
        objDALHotelSearch.agentcode = AgentCode
        objDALHotelSearch.obsourcectrycode = OBsourcectrycode
        objDALHotelSearch.obreqoverride = OBreqoverride
        objDALHotelSearch.obagentref = OBagentref
        objDALHotelSearch.obcolumbusref = OBcolumbusref
        objDALHotelSearch.obremarks = OBremarks
        objDALHotelSearch.userlogged = userlogged
        objDALHotelSearch.OBrlineno = OBrlineno
        objDALHotelSearch.OBcheckin = OBCheckin
        objDALHotelSearch.OBcheckout = OBCheckout
        objDALHotelSearch.OBnoofrooms = OBnoofrooms
        objDALHotelSearch.OBadults = OBadults
        objDALHotelSearch.OBchild = OBchild
        objDALHotelSearch.OBchildages = ChildAgeString
        objDALHotelSearch.OBsupagentcode = OBsupagentcode
        objDALHotelSearch.OBpartycode = OBpartycode
        objDALHotelSearch.OBrateplanid = OBrateplanid
        objDALHotelSearch.OBrateplanname = OBrateplanname
        objDALHotelSearch.OBrmtypcode = OBrmtypcode
        objDALHotelSearch.OBroomclasscode = OBroomclasscode
        objDALHotelSearch.OBrmcatcode = OBrmcatcode
        objDALHotelSearch.OBaccommodationid = OBaccommodationid
        objDALHotelSearch.OBmealplans = OBmealplans
        objDALHotelSearch.OBsalevalue = OBsalevalue
        objDALHotelSearch.OBsalecurrcode = OBsalecurrcode
        objDALHotelSearch.OBcostvalue = OBcostvalue
        objDALHotelSearch.OBcostcurrcode = OBcostcurrcode
        objDALHotelSearch.OBwlsalevalue = OBwlsalevalue
        objDALHotelSearch.OBavailable = OBavailable
        objDALHotelSearch.OBcomp_cust = OBcomp_cust
        objDALHotelSearch.OBcomp_supp = OBcomp_supp
        objDALHotelSearch.OBcomparrtrf = OBcomparrtrf
        objDALHotelSearch.OBcompdeptrf = OBcompdeptrf
        objDALHotelSearch.obpricebreakuptemp = obpricebreakuptemp
        objDALHotelSearch.SpecialEventXML = SpecialEventXML
        objDALHotelSearch.SharingOrExtraBed = SharingOrExtraBed
        objDALHotelSearch.NoOfAdultEb = NoOfAdultEb
        objDALHotelSearch.NoOfChildEb = NoOfChildEb
        objDALHotelSearch.RoomString = RoomString
        objDALHotelSearch.Shifting = Shifting
        objDALHotelSearch.ShiftingCode = ShiftingCode
        objDALHotelSearch.ShiftingLineNo = ShiftingLineNo
        objDALHotelSearch.AmendMode = AmendMode
        objDALHotelSearch.wlCurrCode = wlCurrCode
        objDALHotelSearch.SubUserCode = SubUserCode
        objDALHotelSearch.OneTimePayXML = OneTimePayXML

        objDALHotelSearch.RatePlanSource = RatePlanSource
        objDALHotelSearch.Int_RoomtypeCodes = Int_RoomtypeCodes
        objDALHotelSearch.Int_RoomtypeNames = Int_RoomtypeNames
        objDALHotelSearch.Int_Roomtypes = Int_Roomtypes


        objDALHotelSearch.Int_PartyCode = Int_PartyCode
        objDALHotelSearch.Int_rmtypecode = Int_rmtypecode
        objDALHotelSearch.Int_mealcode = Int_mealcode

        objDALHotelSearch.Offercode = Offercode
        objDALHotelSearch.Accomodationcode = Accomodationcode


        objDALHotelSearch.Int_costprice = Int_costprice
        objDALHotelSearch.Int_costcurrcode = Int_costcurrcode


        res = objDALHotelSearch.savingbookingintemp()

        Return res
    End Function




    Function getrequestid() As String
        Dim reqid As String
        reqid = objDALHotelSearch.getrequestid()
        Return reqid
    End Function


    Function GetMinNightStayDetails(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetMinNightStayDetails(strPartyCode, strRoomTypeCode, strMealPlanCode, strRatePlancode, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return dt
    End Function
    Function GetCancelationPolicyDetails(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetCancelationPolicyDetails(strPartyCode, strRoomTypeCode, strMealPlanCode, strRatePlancode, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return dt
    End Function

    Function GetSearchDetails(Optional ByVal strPriceBreakup As String = "0", Optional ByVal strRatePlanId As String = "", Optional ByVal strpartCode As String = "", Optional ByVal strRoomtypeCode As String = "", Optional ByVal strRoomCatCode As String = "", Optional ByVal strMealPlanCode As String = "", Optional ByVal SharingOrExtraBed As String = "", Optional ByVal strPreferred As String = "0") As DataSet
        Dim dsHotelSearch As New DataSet
        dsHotelSearch = objDALHotelSearch.GetSearchDetails(LoginType, WebuserName, AgentCode, DestinationType, DestinationCode, CheckIn, CheckOut, Room, RoomString, SourceCountryCode, OrderBy, StarCategoryCode, Availabilty, PropertyType, HotelCode, OverridePrice, FilterRoomClass, strPriceBreakup, strRatePlanId, strpartCode, strRoomtypeCode, strRoomCatCode, MealPlan, SharingOrExtraBed, EditRequestId, EditRLineNo, EditRatePlanId, strPreferred, ShowallCategory) '' Added shhaul 27/06/18
        Return dsHotelSearch
    End Function

    Function GetSearchDetailsSingleHotel(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal CheckIn As String, ByVal CheckOut As String, ByVal Room As String, ByVal RoomString As String, ByVal SourceCountry As String, ByVal OrderBy As String, ByVal Availabilty As String, ByVal HotelCode As String, ByVal OverridePrice As String, ByVal strEditRequestId As String, ByVal strEditRLineNo As String, ByVal strEditRatePlanId As String, ByVal strMealPlan As String) As DataSet '' Added Shahul 27/06/18
        Try
            Dim ds As New DataSet
            ds = objDALHotelSearch.GetSearchDetailsSingleHotel(LoginType, WebuserName, AgentCode, CheckIn, CheckOut, Room, RoomString, SourceCountry, OrderBy, Availabilty, HotelCode, OverridePrice, strEditRequestId, strEditRLineNo, strEditRatePlanId, strMealPlan) '' Added Shahul 27/06/18
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Function GetSearchDetailsSingleHotelAlternatives(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal CheckIn As String, ByVal CheckOut As String, ByVal Room As String, ByVal RoomString As String, ByVal SourceCountry As String, ByVal OrderBy As String, ByVal Availabilty As String, ByVal HotelCode As String, ByVal OverridePrice As String, ByVal strEditRequestId As String, ByVal strEditRLineNo As String, ByVal strEditRatePlanId As String, ByVal strMealPlan As String) As DataSet '' Added Shahul 27/06/18
        Try
            Dim ds As New DataSet
            ds = objDALHotelSearch.GetSearchDetailsSingleHotelAlternatives(LoginType, WebuserName, AgentCode, CheckIn, CheckOut, Room, RoomString, SourceCountry, OrderBy, Availabilty, HotelCode, OverridePrice, strEditRequestId, strEditRLineNo, strEditRatePlanId, strMealPlan) '' Added Shahul 27/06/18
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function GetCheckInAndCheckOutDetails(ByVal strRequestId As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetCheckInAndCheckOutDetails(strRequestId)
        Return dt
    End Function
    Function GetCheckInAndCheckOutDetailsFlights(ByVal strRequestId As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetCheckInAndCheckOutDetailsFlights(strRequestId)
        Return dt
    End Function
    Function CheckExistsSectorCheckInAndCheckOut(ByVal strRequestId As String, ByVal sector As String, ByVal tourdate As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.CheckExistsSectorCheckInAndCheckOut(strRequestId, sector, tourdate)
        Return dt
    End Function
    Function GetSectorCheckInAndCheckOutDetails(ByVal strRequestId As String, ByVal sector As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetSectorCheckInAndCheckOutDetails(strRequestId, sector)
        Return dt
    End Function

    Function GetHotelSpecialEventsSummary(ByVal strRequestId As String, ByVal strRLineNo As String, ByVal strWhiteLabel As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetHotelSpecialEventsSummary(strRequestId, strRLineNo, strWhiteLabel)
        Return dt
    End Function
    Function GetBookingSummary(ByVal strRequestId As String, ByVal strWhiteLabel As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetBookingSummary(strRequestId, strWhiteLabel)
        Return dt
    End Function

    Function GetFlightdetails(ByVal strRequestId As String) As DataSet
        Dim ds As New DataSet
        ds = objDALHotelSearch.GetFlightdetails(strRequestId)
        Return ds
    End Function
    Function GetPersonalDetails(ByVal strRequestId As String) As DataSet
        Dim ds As New DataSet
        ds = objDALHotelSearch.GetPersonalDetails(strRequestId)
        Return ds
    End Function
    Function GetVisaSummary(ByVal strRequestId As String, Optional ByVal strwhitelabel As String = "0") As DataTable
        Dim ds As New DataTable
        ds = objDALHotelSearch.GetVisaSummary(strRequestId, strwhitelabel)
        Return ds
    End Function
    Function GetResultAsDataTable(ByVal strQuery As String) As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALHotelSearch.GetResultAsDataTable(strQuery)
        Return objDataTable
    End Function

    Function GetTariff(ByVal strPartyCode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetTariff(strPartyCode, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return dt
    End Function

    Function GetSpecialOffers(ByVal strPartyCode As String, ByVal strRatePlanId As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetSpecialOffers(strPartyCode, strRatePlanId)
        Return dt
    End Function

    Function FillTransferDetails(ByVal strReqId As String) As DataSet
        Dim ds As New DataSet
        ds = objDALHotelSearch.FillTransferDetails(strReqId)
        Return ds
    End Function

    Function GetHotelContruction(ByVal StrPartyCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetHotelContruction(StrPartyCode, strCheckIn, strCheckOut)
        Return dt
    End Function

    Function GetSpecialEventsDetails(ByVal strPartCode As String, ByVal strRoomTypecode As String, ByVal strMealPlanCode As String, ByVal strCatCode As String, ByVal strAccCode As String, ByVal strRatePlanId As String, ByVal strAgentCode As String, ByVal strSourceCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String, ByVal strRoom As String, ByVal strAdult As String, ByVal strChildren As String, ByVal strChildAgeString As String, ByVal strSelectedEvents As String, ByVal strRoomString As String, ByVal strPriceOveride As String, ByVal strEditRequestId As String, ByVal strEdirRLineNo As String) As DataSet
        Dim ds As New DataSet
        ds = objDALHotelSearch.GetSpecialEventsDetails(strPartCode, strRoomTypecode, strMealPlanCode, strCatCode, strAccCode, strRatePlanId, strAgentCode, strSourceCountryCode, strCheckIn, strCheckOut, strRoom, strAdult, strChildren, strChildAgeString, strSelectedEvents, strRoomString, OverridePrice, strEditRequestId, strEdirRLineNo)
        Return ds
    End Function

    Function GetAdultAndChildFromRoomString(ByVal strRoomString As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetAdultAndChildFromRoomString(strRoomString)
        Return dt
    End Function

    Function GetPartyName(ByVal strHId As String) As String
        Dim strHotelName As String = objDALHotelSearch.GetPartyName(strHId)
        Return strHotelName
    End Function
    Function GetHotelBookingDetailsForShifting(ByVal strRequestId As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetHotelBookingDetailsForShifting(strRequestId)
        Return dt
    End Function

    'Created / changed by mohamed on 05/04/2018
    Function GetHotelBookingDetailsForShiftingNew(ByVal strRequestId As String, ByVal rlineno As Integer) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetHotelBookingDetailsForShiftingNew(strRequestId, rlineno)
        Return dt
    End Function

    Function GetBookingDetailsForEdit(ByVal strRequestId As Object, ByVal strRLineNo As String) As DataTable
        Dim objDALHotelSearch As New DALHotelSearch
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetBookingDetailsForEdit(strRequestId, strRLineNo)
        Return dt
    End Function

    Function RemoveHotelBooking(ByVal strRequestId As String, ByVal strelineno As String) As String

        Dim str As String = ""
        str = objDALHotelSearch.RemoveHotelBooking(strRequestId, strelineno)
        Return str

    End Function

    Function GetHotelCancelDetails(ByVal strRequestId As String, ByVal strelineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetHotelCancelDetails(strRequestId, strelineno)
        Return dt
    End Function

    Function GetCancelDays(ByVal strRMPartyCode As String, ByVal strRMRoomTypeCode As String, ByVal strRMMealPlanCode As String, ByVal strRMRatePlanId As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As String
        Dim strcancelDays As String = objDALHotelSearch.GetCancelDays(strRMPartyCode, strRMRoomTypeCode, strRMMealPlanCode, strRMRatePlanId, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return strcancelDays
    End Function
    Function GetCancelDaysWithNonRefundable(ByVal strRMPartyCode As String, ByVal strRMRoomTypeCode As String, ByVal strRMMealPlanCode As String, ByVal strRMRatePlanId As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Dim dtCancelDays As DataTable = objDALHotelSearch.GetCancelDaysWithNonRefundable(strRMPartyCode, strRMRoomTypeCode, strRMMealPlanCode, strRMRatePlanId, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return dtCancelDays
    End Function

    Function PreHotelSummary(ByVal strRequestId As String, Optional ByVal strwhite As String = "0", Optional ByVal RLineNo As String = "") As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.PreHotelSummary(strRequestId, strwhite, RLineNo)
        Return dt
    End Function

    Function RemovePreArrangedHotel(ByVal strRequestId As Object, ByVal rLineNo As String) As String
        Dim str As String = ""
        str = objDALHotelSearch.RemovePreArrangedHotel(strRequestId, rLineNo)
        Return str
    End Function

    Function GetAdultAndChildSum(strRoomString As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetAdultAndChildSum(strRoomString)
        Return dt
    End Function

    Function GetNew_booking_OneTimePay(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String, ByVal strNoOfExtraBed As String, ByVal strRequestId As String, ByVal strrlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetNew_booking_OneTimePay(strPartyCode, strRoomTypeCode, strMealPlanCode, strRatePlancode, strAgentCode, strCountryCode, strCheckIn, strCheckOut, strNoOfExtraBed, strRequestId, strrlineno)
        Return dt
    End Function



    Function GetBooking_checkinoutpolicy(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetBooking_checkinoutpolicy(strPartyCode, strRoomTypeCode, strMealPlanCode, strRatePlancode, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return dt
    End Function

  
    Function GetAirportTransferCompliment(ByVal strPartyCode As String, ByVal strRatePlanId As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetAirportTransferCompliment(strPartyCode, strRatePlanId)
        Return dt
    End Function

    Function GetOrCreateRoomType(ByVal strPartyCode As String, strRoomTypes As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetOrCreateRoomType(strPartyCode, strRoomTypes)
        Return dt
    End Function

    Function CallHotelDetailsAPIAndSave(objBLLHotelSearch As BLLHotelSearch) As String
        Dim strDetailSave As String = objDALHotelSearch.CallHotelDetailsAPIAndSave(objBLLHotelSearch)
        Return strDetailSave
    End Function

    Function GetInt_TariffNote(strPartyCode As String, strAgentCode As String, strCountryCode As String, strCheckIn As String, strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetInt_TariffNote(strPartyCode, strAgentCode, strCountryCode, strCheckIn, strCheckOut)
        Return dt
    End Function

    Function GetInt_HotelContruction(StrPartyCode As String, strCheckIn As String, strCheckOut As String) As DataTable
        Dim dt As New DataTable
        dt = objDALHotelSearch.GetInt_HotelContruction(StrPartyCode, strCheckIn, strCheckOut)
        Return dt
    End Function

End Class
