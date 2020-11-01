Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Script.Serialization
Imports System.IO

Public Class DALHotelSearch
    Dim objclsUtilities As New clsUtilities
    Public obdiv_code As String
    Public obrequestid As String
    Public agentcode As String
    Public obsourcectrycode As String
    Public obreqoverride As String
    Public obagentref As String
    Public obcolumbusref As String
    Public obremarks As String
    Public userlogged As String
    Public OBrlineno As Integer
    Public OBnoofrooms As Integer
    Public OBadults As Integer
    Public OBchild As Integer
    Public OBchildages As String = ""
    Public OBsupagentcode As String = ""
    Public OBcheckin As String
    Public OBcheckout As String
    Public OBpartycode As String = ""
    Public OBrateplanid As String = ""
    Public OBrateplanname As String = ""
    Public OBrmtypcode As String = ""
    Public OBroomclasscode As String = ""
    Public OBrmcatcode As String = ""
    Public OBaccommodationid As Integer
    Public OBmealplans As String
    Public OBsalevalue As Decimal
    Public OBsalecurrcode As String = ""
    Public OBcostvalue As Decimal
    Public OBcostcurrcode As String = ""
    Public OBwlsalevalue As Decimal
    Public OBavailable As Integer
    Public OBcomp_cust As Integer
    Public OBcomp_supp As Integer
    Public OBcomparrtrf As Integer
    Public OBcompdeptrf As Integer
    Public obpricebreakuptemp As String
    Dim _SpecialEventXML As String = ""
    Public SharingOrExtraBed As String = ""
    Public NoOfAdultEb As String = ""
    Public NoOfChildEb As String = ""
    Public RoomString As String = ""
    Public _Shifting As String = "0"
    Public _ShiftingCode As String = ""
    Public _ShiftingLineNo As String = ""
    Public _AmendMode As String = ""
    Public _wlCurrCode As String = ""
    Private _SubUserCode As String = ""

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

    Private _PreShowHotel As String = "0" '' Added shahul 10/11/18


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


    ''*** Danny 07/05/2018
    'Public _VATPerc As Decimal
    'Public _CostTaxableValue As Decimal
    'Public _CostNonTaxableValue As Decimal
    'Public _CostVATValue As Decimal

    Dim mysqlTrans As SqlTransaction



    Public Property AmendMode As String
        Get
            Return _AmendMode
        End Get
        Set(ByVal value As String)
            _AmendMode = value
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

    Public Property SpecialEventXML As String
        Get
            Return _SpecialEventXML
        End Get
        Set(ByVal value As String)
            _SpecialEventXML = value
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
    Public Property OneTimePayXML As String
        Get
            Return _OneTimePayXML
        End Get
        Set(ByVal value As String)
            _OneTimePayXML = value
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
    Function FindBookingEnginRateType(ByVal sAgentCode As String) As Integer
        Try
            Dim iCumulative As Integer = 0
            Dim strQuery As String = "select COUNT(agentcode)cnt from agentmast(nolock) where agentcode='" & sAgentCode & "' and isnull(bookingengineratetype,'')='CUMULATIVE'"
            iCumulative = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return iCumulative
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return 0
        End Try

    End Function
    Function FindWhiteLabel(ByVal sAgentCode As String) As Integer
        Try
            Dim strOwnCompany As String = 0
            Dim strQuery As String = "select count(OwnCompany)OwnCompany from agentmast_whitelabel where agentcode='" & sAgentCode & "'  and OwnCompany=0 "
            strOwnCompany = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strOwnCompany
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return 0
        End Try

    End Function

    Function FillTransferDetails(ByVal requestid As String) As DataSet
        Try
            Dim ProcName As String
            ProcName = "sp_booking_fillauto_transfers"
            Dim parm(0) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(requestid, String))


            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetSearchDetails(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal DestinationType As String, ByVal DestinationCode As String, ByVal CheckIn As String, ByVal CheckOut As String, ByVal Room As String, ByVal RoomString As String, ByVal SourceCountry As String, ByVal OrderBy As String, ByVal StarCategoryCode As String, ByVal Availabilty As String, ByVal PropertyType As String, ByVal HotelCode As String, ByVal OverridePrice As String, ByVal FilterRoomClass As String, ByVal strPriceBreakup As String, ByVal strRatePlanId As String, ByVal strpartCode As String, ByVal strRoomtypeCode As String, ByVal strRoomCatCode As String, ByVal strMealPlanCode As String, ByVal strSharingOrExtraBed As String, ByVal strEditRequestId As String, ByVal strEditRLineNo As String, ByVal strEditRatePlanId As String, ByVal Preferred As String, ByVal ShowallCategory As String) As Data.DataSet

        Try
            If CheckIn <> "" Then
                Dim strDates As String() = CheckIn.Split("/")
                CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If CheckOut <> "" Then
                Dim strDates As String() = CheckOut.Split("/")
                CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            '   ProcName = "sp_booking_hotels_search_minprice"
            ProcName = "New_MinPrice_Calculation"
            Dim parm(27) As SqlParameter ''' Added shahul 27/06/18
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@destinationtype", CType(DestinationType, String))
            parm(4) = New SqlParameter("@destinationcode", CType(DestinationCode, String))
            parm(5) = New SqlParameter("@checkin_new", CType(CheckIn, String))
            parm(6) = New SqlParameter("@checkout_new", CType(CheckOut, String))
            parm(7) = New SqlParameter("@noofrooms", CType(Room, String))

            parm(8) = New SqlParameter("@roomstring", CType(RoomString, String))

            parm(9) = New SqlParameter("@sourcectrycode", CType(SourceCountry, String))
            parm(10) = New SqlParameter("@orderby", CType(OrderBy, String))
            parm(11) = New SqlParameter("@catcode", CType(StarCategoryCode, String))

            parm(12) = New SqlParameter("@available", CType(Availabilty, String))
            parm(13) = New SqlParameter("@propertytypecode", CType(PropertyType, String))
            parm(14) = New SqlParameter("@hotelcode", CType(HotelCode, String))
            parm(15) = New SqlParameter("@override", CType(OverridePrice, String))
            parm(16) = New SqlParameter("@filterroomclass", CType(FilterRoomClass, String))

            parm(17) = New SqlParameter("@pricebreakup", CType(strPriceBreakup, String))
            parm(18) = New SqlParameter("@rateplanid", CType(strRatePlanId, String))
            parm(19) = New SqlParameter("@partycode", CType(strpartCode, String))
            parm(20) = New SqlParameter("@rmtypcode", CType(strRoomtypeCode, String))
            parm(21) = New SqlParameter("@mealplans", CType(strMealPlanCode, String))
            parm(22) = New SqlParameter("@sharingorextrabed", CType(strSharingOrExtraBed, String))
            parm(23) = New SqlParameter("@requestid", CType(strEditRequestId, String))
            parm(24) = New SqlParameter("@rlineno", CType(strEditRLineNo, String))
            parm(25) = New SqlParameter("@editrateplanid", CType(strEditRatePlanId, String))
            parm(26) = New SqlParameter("@Preferred", CType(Preferred, String))

            parm(27) = New SqlParameter("@showallcategory", CType(ShowallCategory, String))  ''' Added shahul 27/06/18

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetSearchDetailsSingleHotel(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal CheckIn As String, ByVal CheckOut As String, ByVal Room As String, ByVal RoomString As String, ByVal SourceCountry As String, ByVal OrderBy As String, ByVal Availabilty As String, ByVal HotelCode As String, ByVal OverridePrice As String, ByVal strEditRequestId As String, ByVal strEditRLineNo As String, ByVal strEditRatePlanId As String, ByVal strMealPlan As String) As DataSet

        Try
            If CheckIn <> "" Then
                Dim strDates As String() = CheckIn.Split("/")
                CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If CheckOut <> "" Then
                Dim strDates As String() = CheckOut.Split("/")
                CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            '   ProcName = "sp_booking_hotels_search_singlehotel"
            ProcName = "New_SingleHotelSearch"
            Dim parm(15) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@checkin_new", CType(CheckIn, String))
            parm(4) = New SqlParameter("@checkout_new", CType(CheckOut, String))
            parm(5) = New SqlParameter("@noofrooms", CType(Room, String))
            parm(6) = New SqlParameter("@roomstring", CType(RoomString, String))
            parm(7) = New SqlParameter("@sourcectrycode", CType(SourceCountry, String))
            parm(8) = New SqlParameter("@orderby", CType(OrderBy, String))
            parm(9) = New SqlParameter("@available", CType(Availabilty, String))
            parm(10) = New SqlParameter("@hotelcode", CType(HotelCode, String))
            parm(11) = New SqlParameter("@override", CType(OverridePrice, String))
            parm(12) = New SqlParameter("@requestid", CType(strEditRequestId, String))
            parm(13) = New SqlParameter("@rlineno", CType(strEditRLineNo, String))
            parm(14) = New SqlParameter("@editrateplanid", CType(strEditRatePlanId, String))

            parm(15) = New SqlParameter("@mealplan", CType(strMealPlan, String))  ''' Added shahul 27/06/18

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetSearchDetailsSingleHotelAlternatives(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal CheckIn As String, ByVal CheckOut As String, ByVal Room As String, ByVal RoomString As String, ByVal SourceCountry As String, ByVal OrderBy As String, ByVal Availabilty As String, ByVal HotelCode As String, ByVal OverridePrice As String, ByVal strEditRequestId As String, ByVal strEditRLineNo As String, ByVal strEditRatePlanId As String, ByVal strMealPlan As String) As DataSet

        Try
            If CheckIn <> "" Then
                Dim strDates As String() = CheckIn.Split("/")
                CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If CheckOut <> "" Then
                Dim strDates As String() = CheckOut.Split("/")
                CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "sp_booking_hotels_search_singlehotel_alternatives"
            Dim parm(15) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@checkin", CType(CheckIn, String))
            parm(4) = New SqlParameter("@checkout", CType(CheckOut, String))
            parm(5) = New SqlParameter("@noofrooms", CType(Room, String))
            parm(6) = New SqlParameter("@roomstring", CType(RoomString, String))
            parm(7) = New SqlParameter("@sourcectrycode", CType(SourceCountry, String))
            parm(8) = New SqlParameter("@orderby", CType(OrderBy, String))
            parm(9) = New SqlParameter("@available", CType(Availabilty, String))
            parm(10) = New SqlParameter("@hotelcode", CType(HotelCode, String))
            parm(11) = New SqlParameter("@override", CType(OverridePrice, String))
            parm(12) = New SqlParameter("@requestid", CType(strEditRequestId, String))
            parm(13) = New SqlParameter("@rlineno", CType(strEditRLineNo, String))
            parm(14) = New SqlParameter("@editrateplanid", CType(strEditRatePlanId, String))

            parm(15) = New SqlParameter("@mealplan", CType(strMealPlan, String))  ''' Added shahul 27/06/18

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavePreArrangedHotelBookinginTemp() As String
        Dim mySqlConn As New SqlConnection
        Try
            'changed by mohamed on 03/09/2018
            Dim dtSet As DataSet, lGShitDetXML As String
            dtSet = objclsUtilities.GetDataFromDataset("execute sp_get_shifting_hotel_detail_guestpage '" & obrequestid & "',-1")
            lGShitDetXML = objclsUtilities.GenerateXML(dtSet)

            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            Dim CheckIn As String = PreHotelCheckIn
            If CheckIn <> "" Then
                Dim strDates As String() = CheckIn.Split("/")
                CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            Dim CheckOut As String = PreHotelCheckout
            If CheckOut <> "" Then
                Dim strDates As String() = CheckOut.Split("/")
                CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "sp_update_booking_headertemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(10) As SqlParameter

            parm(0) = New SqlParameter("@div_code", CType(obdiv_code, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@agentcode", CType(agentcode, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@sourcectrycode", CType(obsourcectrycode, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@reqoverride", CType(obreqoverride, Integer))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@agentref", CType(obagentref, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@columbusref", CType(obcolumbusref, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@remarks", CType(obremarks, String))
            sqlParamList.Add(parm(7))
            parm(8) = New SqlParameter("@SubUserCode", CType(SubUserCode, String))
            sqlParamList.Add(parm(8))
            parm(9) = New SqlParameter("@userlogged", CType(userlogged, String))
            sqlParamList.Add(parm(9))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNamePreHotel As String
            ProcNamePreHotel = "sp_update_booking_hotels_prearrangedtemp"

            Dim sqlParamListPreHotel As New List(Of SqlParameter)
            Dim parmPreHotel(16) As SqlParameter

            parmPreHotel(0) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamListPreHotel.Add(parmPreHotel(0))
            parmPreHotel(1) = New SqlParameter("@rlineno", CType(PreHotelRLineNo, String))
            sqlParamListPreHotel.Add(parmPreHotel(1))
            parmPreHotel(2) = New SqlParameter("@checkin", CType(CheckIn, String))
            sqlParamListPreHotel.Add(parmPreHotel(2))
            parmPreHotel(3) = New SqlParameter("@checkout", CType(CheckOut, String))
            sqlParamListPreHotel.Add(parmPreHotel(3))
            parmPreHotel(4) = New SqlParameter("@partycode", CType(PreHotelPartyCode, String))
            sqlParamListPreHotel.Add(parmPreHotel(4))
            parmPreHotel(5) = New SqlParameter("@adults", CType(PreHotelAdults, String))
            sqlParamListPreHotel.Add(parmPreHotel(5))
            parmPreHotel(6) = New SqlParameter("@child", CType(PreHotelChild, String))
            sqlParamListPreHotel.Add(parmPreHotel(6))
            parmPreHotel(7) = New SqlParameter("@childages", CType(PreHotelChildages, String))
            sqlParamListPreHotel.Add(parmPreHotel(7))
            parmPreHotel(8) = New SqlParameter("@sectorcode", CType(PreHotelSectorCode, String))
            sqlParamListPreHotel.Add(parmPreHotel(8))
            parmPreHotel(9) = New SqlParameter("@agentcode", CType(agentcode, String))
            sqlParamListPreHotel.Add(parmPreHotel(9))
            parmPreHotel(10) = New SqlParameter("@sourcectrycode", CType(obsourcectrycode, String))
            sqlParamListPreHotel.Add(parmPreHotel(10))
            parmPreHotel(11) = New SqlParameter("@userlogged", CType(userlogged, String))
            sqlParamListPreHotel.Add(parmPreHotel(11))


            parmPreHotel(12) = New SqlParameter("@Shifting", CType(PreArrangedShifting, String))
            sqlParamListPreHotel.Add(parmPreHotel(12))
            parmPreHotel(13) = New SqlParameter("@ShiftingCode", CType(PreArrangedShiftingCode, String))
            sqlParamListPreHotel.Add(parmPreHotel(13))
            parmPreHotel(14) = New SqlParameter("@ShiftingLineNo", CType(PreArrangedShiftingLineNo, String))
            sqlParamListPreHotel.Add(parmPreHotel(14))



            'changed by mohamed on 03/09/2018
            parmPreHotel(15) = New SqlParameter("@guestshifttableXML", lGShitDetXML)
            parmPreHotel(15).DbType = DbType.Xml
            sqlParamListPreHotel.Add(parmPreHotel(15))
            parmPreHotel(16) = New SqlParameter("@showhotel", CType(PreShowHotel, String))  '' Added shhaul 10/11/18
            sqlParamListPreHotel.Add(parmPreHotel(16))



            objclsUtilities.ExecuteNonQuerynew(constring, ProcNamePreHotel, sqlParamListPreHotel, mySqlConn, mysqlTrans)


            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception
            mysqlTrans.Rollback()
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            clsDBConnect.dbConnectionClose(mySqlConn)
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
    End Function


    Function savingbookingintemp() As String
        Dim mySqlConn As New SqlConnection
        'Dim mysqlTrans As SqlTransaction
        Try

            'changed by mohamed on 03/09/2018
            Dim dtSet As DataSet, lGShitDetXML As String
            dtSet = objclsUtilities.GetDataFromDataset("execute sp_get_shifting_hotel_detail_guestpage '" & obrequestid & "',-1")
            lGShitDetXML = objclsUtilities.GenerateXML(dtSet)

            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            Dim CheckIn As String = OBcheckin
            If CheckIn <> "" Then
                Dim strDates As String() = CheckIn.Split("/")
                CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            Dim CheckOut As String = OBcheckout
            If CheckOut <> "" Then
                Dim strDates As String() = CheckOut.Split("/")
                CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcNameConfirmRevise As String
            ProcNameConfirmRevise = "sp_revise_hotel_booking_confirmation"
            Dim sqlParamListConfirmRevise As New List(Of SqlParameter)
            Dim parmConfirmRevise(18) As SqlParameter
            parmConfirmRevise(0) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(0))
            parmConfirmRevise(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(1))
            parmConfirmRevise(2) = New SqlParameter("@checkin", Format(CType(CheckIn, Date), "yyyy/MM/dd"))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(2))
            parmConfirmRevise(3) = New SqlParameter("@checkout", Format(CType(CheckOut, Date), "yyyy/MM/dd"))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(3))
            parmConfirmRevise(4) = New SqlParameter("@noofrooms", CType(OBnoofrooms, Integer))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(4))
            parmConfirmRevise(5) = New SqlParameter("@adults", CType(OBadults, Integer))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(5))
            parmConfirmRevise(6) = New SqlParameter("@child", CType(OBchild, Integer))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(6))
            parmConfirmRevise(7) = New SqlParameter("@supagentcode", CType(OBsupagentcode, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(7))
            parmConfirmRevise(8) = New SqlParameter("@partycode", CType(OBpartycode, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(8))
            parmConfirmRevise(9) = New SqlParameter("@rateplanid", CType(OBrateplanid, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(9))
            parmConfirmRevise(10) = New SqlParameter("@rmtypcode", CType(OBrmtypcode, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(10))
            parmConfirmRevise(11) = New SqlParameter("@roomclasscode", CType(OBroomclasscode, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(11))
            parmConfirmRevise(12) = New SqlParameter("@rmcatcode", CType(OBrmcatcode, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(12))
            parmConfirmRevise(13) = New SqlParameter("@mealplans", CType(OBmealplans, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(13))
            parmConfirmRevise(14) = New SqlParameter("@salevalue", CType(OBsalevalue, Decimal))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(14))
            parmConfirmRevise(15) = New SqlParameter("@costvalue", CType(OBcostvalue, Decimal))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(15))
            parmConfirmRevise(16) = New SqlParameter("@amend", CType(AmendMode, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(16))
            parmConfirmRevise(17) = New SqlParameter("@RoomString", CType(RoomString, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(17))
            parmConfirmRevise(18) = New SqlParameter("@userlogged", CType(userlogged, String))
            sqlParamListConfirmRevise.Add(parmConfirmRevise(18))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameConfirmRevise, sqlParamListConfirmRevise, mySqlConn, mysqlTrans)





            Dim ProcName As String
            ProcName = "sp_update_booking_headertemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(10) As SqlParameter

            parm(0) = New SqlParameter("@div_code", CType(obdiv_code, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@agentcode", CType(agentcode, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@sourcectrycode", CType(obsourcectrycode, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@reqoverride", CType(obreqoverride, Integer))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@agentref", CType(obagentref, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@columbusref", CType(obcolumbusref, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@remarks", CType(obremarks, String))
            sqlParamList.Add(parm(7))
            parm(8) = New SqlParameter("@SubUserCode", CType(SubUserCode, String))
            sqlParamList.Add(parm(8))
            parm(9) = New SqlParameter("@userlogged", CType(userlogged, String))
            sqlParamList.Add(parm(9))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNamedet As String
            ProcNamedet = "sp_update_booking_hotel_detailtemp"
            Dim sqlParamListdet As New List(Of SqlParameter)
            Dim parmdet(44) As SqlParameter
            parmdet(0) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamListdet.Add(parmdet(0))
            parmdet(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
            sqlParamListdet.Add(parmdet(1))
            parmdet(2) = New SqlParameter("@checkin", Format(CType(CheckIn, Date), "yyyy/MM/dd"))
            sqlParamListdet.Add(parmdet(2))
            parmdet(3) = New SqlParameter("@checkout", Format(CType(CheckOut, Date), "yyyy/MM/dd"))
            sqlParamListdet.Add(parmdet(3))
            parmdet(4) = New SqlParameter("@noofrooms", CType(OBnoofrooms, Integer))
            sqlParamListdet.Add(parmdet(4))
            parmdet(5) = New SqlParameter("@adults", CType(OBadults, Integer))
            sqlParamListdet.Add(parmdet(5))
            parmdet(6) = New SqlParameter("@child", CType(OBchild, Integer))
            sqlParamListdet.Add(parmdet(6))
            parmdet(7) = New SqlParameter("@childages", CType(OBchildages, String))
            sqlParamListdet.Add(parmdet(7))
            parmdet(8) = New SqlParameter("@sharingorextrabed", CType(SharingOrExtraBed, String))
            sqlParamListdet.Add(parmdet(8))

            parmdet(9) = New SqlParameter("@noofadulteb", CType(NoOfAdultEb, String))
            sqlParamListdet.Add(parmdet(9))
            parmdet(10) = New SqlParameter("@noofchildeb", CType(NoOfChildEb, String))
            sqlParamListdet.Add(parmdet(10))

            parmdet(11) = New SqlParameter("@supagentcode", CType(OBsupagentcode, String))
            sqlParamListdet.Add(parmdet(11))
            parmdet(12) = New SqlParameter("@partycode", CType(OBpartycode, String))
            sqlParamListdet.Add(parmdet(12))
            parmdet(13) = New SqlParameter("@rateplanid", CType(OBrateplanid, String))
            sqlParamListdet.Add(parmdet(13))
            parmdet(14) = New SqlParameter("@rateplanname", CType(OBrateplanname, String))
            sqlParamListdet.Add(parmdet(14))
            parmdet(15) = New SqlParameter("@rmtypcode", CType(OBrmtypcode, String))
            sqlParamListdet.Add(parmdet(15))
            parmdet(16) = New SqlParameter("@roomclasscode", CType(OBroomclasscode, String))
            sqlParamListdet.Add(parmdet(16))
            parmdet(17) = New SqlParameter("@rmcatcode", CType(OBrmcatcode, String))
            sqlParamListdet.Add(parmdet(17))
            'parmdet(18) = New SqlParameter("@accommodationid", CType(OBaccommodationid, Integer))
            'sqlParamListdet.Add(parmdet(18))
            parmdet(18) = New SqlParameter("@mealplans", CType(OBmealplans, String))
            sqlParamListdet.Add(parmdet(18))
            parmdet(19) = New SqlParameter("@salevalue", CType(OBsalevalue, Decimal))
            sqlParamListdet.Add(parmdet(19))
            parmdet(20) = New SqlParameter("@salecurrcode", CType(OBsalecurrcode, String))
            sqlParamListdet.Add(parmdet(20))
            parmdet(21) = New SqlParameter("@costvalue", CType(OBcostvalue, Decimal))
            sqlParamListdet.Add(parmdet(21))
            parmdet(22) = New SqlParameter("@costcurrcode", CType(OBcostcurrcode, String))
            sqlParamListdet.Add(parmdet(22))
            parmdet(23) = New SqlParameter("@wlsalevalue", CType(OBsalevalue, Decimal))
            sqlParamListdet.Add(parmdet(23))
            parmdet(24) = New SqlParameter("@available", CType(OBavailable, Integer))
            sqlParamListdet.Add(parmdet(24))
            parmdet(25) = New SqlParameter("@comp_cust", CType(OBcomp_cust, Integer))
            sqlParamListdet.Add(parmdet(25))
            parmdet(26) = New SqlParameter("@comp_supp", CType(OBcomp_supp, Integer))
            sqlParamListdet.Add(parmdet(26))
            parmdet(27) = New SqlParameter("@comparrtrf", CType(OBcomparrtrf, Integer))
            sqlParamListdet.Add(parmdet(27))
            parmdet(28) = New SqlParameter("@compdeptrf", CType(OBcompdeptrf, Integer))
            sqlParamListdet.Add(parmdet(28))
            parmdet(29) = New SqlParameter("@userlogged", CType(userlogged, String))
            sqlParamListdet.Add(parmdet(29))
            parmdet(30) = New SqlParameter("@RoomString", CType(RoomString, String))
            sqlParamListdet.Add(parmdet(30))
            parmdet(31) = New SqlParameter("@Shifting", CType(Shifting, String))
            sqlParamListdet.Add(parmdet(31))
            parmdet(32) = New SqlParameter("@ShiftingCode", CType(ShiftingCode, String))
            sqlParamListdet.Add(parmdet(32))
            parmdet(33) = New SqlParameter("@ShiftingLineNo", CType(ShiftingLineNo, String))
            sqlParamListdet.Add(parmdet(33))
            parmdet(33) = New SqlParameter("@wlcurrcode", CType(wlCurrCode, String))
            sqlParamListdet.Add(parmdet(33))
            parmdet(34) = New SqlParameter("@overrideprice", CType(obreqoverride, String))
            sqlParamListdet.Add(parmdet(34))
            parmdet(35) = New SqlParameter("@agentcode", CType(agentcode, String))
            sqlParamListdet.Add(parmdet(35))
            parmdet(36) = New SqlParameter("@sourcecountry", CType(obsourcectrycode, String))
            sqlParamListdet.Add(parmdet(36))

            'changed by mohamed on 03/09/2018
            parmdet(37) = New SqlParameter("@guestshifttableXML", lGShitDetXML)
            parmdet(37).DbType = DbType.Xml
            sqlParamListdet.Add(parmdet(37))

            parmdet(38) = New SqlParameter("@OneTimePayXML", OneTimePayXML)
            parmdet(38).DbType = DbType.Xml
            sqlParamListdet.Add(parmdet(38))

            parmdet(39) = New SqlParameter("@RatePlanSource", CType(RatePlanSource, String))
            sqlParamListdet.Add(parmdet(39))

            parmdet(40) = New SqlParameter("@Int_RoomtypeCodes", CType(Int_RoomtypeCodes, String))
            sqlParamListdet.Add(parmdet(40))

            parmdet(41) = New SqlParameter("@Int_RoomtypeNames", CType(Int_RoomtypeNames, String))
            sqlParamListdet.Add(parmdet(41))

            parmdet(42) = New SqlParameter("@Int_Roomtypes", CType(Int_Roomtypes, String))
            sqlParamListdet.Add(parmdet(42))

            parmdet(43) = New SqlParameter("@Int_costprice", CType(Int_costprice, Decimal))
            sqlParamListdet.Add(parmdet(43))

            parmdet(44) = New SqlParameter("@Int_costcurrcode", CType(Int_costcurrcode, String))
            sqlParamListdet.Add(parmdet(44))


            ''*** Danny 07/05/2018
            'parmdet(36) = New SqlParameter("@CostTaxableValue", CType(_VATPerc, Decimal))
            'sqlParamListdet.Add(parmdet(37))
            'parmdet(36) = New SqlParameter("@CostTaxableValue", CType(_CostTaxableValue, Decimal))
            'sqlParamListdet.Add(parmdet(38))
            'parmdet(36) = New SqlParameter("@CostNonTaxableValue", CType(_CostNonTaxableValue, Decimal))
            'sqlParamListdet.Add(parmdet(39))
            'parmdet(36) = New SqlParameter("@CostVATValue", CType(_CostVATValue, Decimal))
            'sqlParamListdet.Add(parmdet(40))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNamedet, sqlParamListdet, mySqlConn, mysqlTrans)



            Dim ProcNameprice As String
            ProcNameprice = "sp_del_booking_hotel_detail_pricestemp"
            Dim sqlParamListprices As New List(Of SqlParameter)
            Dim parmprices(2) As SqlParameter
            parmprices(0) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamListprices.Add(parmprices(0))
            parmprices(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
            sqlParamListprices.Add(parmprices(1))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameprice, sqlParamListprices, mySqlConn, mysqlTrans)

            Dim Procpricesave As String
            Procpricesave = "sp_add_booking_hotel_detail_pricestemp" '*** Danny 07/05/2018
            Dim sqlParamprices As New List(Of SqlParameter)
            Dim parmpricesadd(3) As SqlParameter
            parmpricesadd(0) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamprices.Add(parmpricesadd(0))
            parmpricesadd(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
            sqlParamprices.Add(parmpricesadd(1))
            parmpricesadd(2) = New SqlParameter("@PriceBreakupXMLInput", CType(obpricebreakuptemp, String))
            sqlParamprices.Add(parmpricesadd(2))

            objclsUtilities.ExecuteNonQuerynew(constring, Procpricesave, sqlParamprices, mySqlConn, mysqlTrans)

            Dim ProcNameSpecialEvent_Delete As String = "sp_del_booking_hotel_detail_specialeventstemp"
            Dim sqlParamListSpclEventDel As New List(Of SqlParameter)
            Dim parmSpclEventDel(2) As SqlParameter
            parmSpclEventDel(0) = New SqlParameter("@requestid", CType(obrequestid, String))
            sqlParamListSpclEventDel.Add(parmSpclEventDel(0))
            parmSpclEventDel(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
            sqlParamListSpclEventDel.Add(parmSpclEventDel(1))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameSpecialEvent_Delete, sqlParamListSpclEventDel, mySqlConn, mysqlTrans)

            If SpecialEventXML <> "" Then

                Dim ProcSpecialEvent As String = "sp_add_booking_hotel_detail_specialeventstemp"
                Dim sqlParamSpecialEvent As New List(Of SqlParameter)
                Dim ParmSpclEvent(4) As SqlParameter
                ParmSpclEvent(0) = New SqlParameter("@requestid", CType(obrequestid, String))
                sqlParamSpecialEvent.Add(ParmSpclEvent(0))
                ParmSpclEvent(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
                sqlParamSpecialEvent.Add(ParmSpclEvent(1))
                ParmSpclEvent(2) = New SqlParameter("@specialxml", CType(SpecialEventXML, String))
                sqlParamSpecialEvent.Add(ParmSpclEvent(2))
                ParmSpclEvent(3) = New SqlParameter("@userlogged", CType(userlogged, String))
                sqlParamSpecialEvent.Add(ParmSpclEvent(3))
                objclsUtilities.ExecuteNonQuerynew(constring, ProcSpecialEvent, sqlParamSpecialEvent, mySqlConn, mysqlTrans)


            End If


            If RatePlanSource = "OneDMC" Then

                Dim objAPIHotelDetailsRequest As APIHotelDetailsRequest.HotelDetailsRequest = New APIHotelDetailsRequest.HotelDetailsRequest()
                Dim CheckInNew As String = OBcheckin
                Dim CheckOutNew As String = OBcheckout
                If CheckIn <> "" Then
                    Dim strDates As String() = CheckInNew.Split("/")
                    CheckInNew = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
                End If
                If CheckOut <> "" Then
                    Dim strDates As String() = CheckOutNew.Split("/")
                    CheckOutNew = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
                End If

                objAPIHotelDetailsRequest.arrivalDate = CheckInNew
                objAPIHotelDetailsRequest.departureDate = CheckOutNew
                Dim strRoomString As String = RoomString
                Dim strRoomsAll() = strRoomString.Split(";")
                Dim ilenth As Integer = strRoomsAll.Length - 1
                Dim distribution As APIHotelDetailsRequest.Distribution = New APIHotelDetailsRequest.Distribution()
                Dim dist As New List(Of APIHotelDetailsRequest.Distribution)
                ' Dim dist(ilenth) As APIHotelDetailsRequest.Distribution


                For i As Integer = 0 To strRoomsAll.Length - 1
                    Dim strRooms() = strRoomsAll(i).Split(",")
                    distribution = New APIHotelDetailsRequest.Distribution()
                    distribution.numberAdults = strRooms(1)
                    distribution.numberChildren = strRooms(2)
                    Dim strChildAges As String() = strRooms(3).Split("|")
                    Dim childrenAges As New List(Of Integer)
                    If strRooms(2) > 0 Then

                        For j As Integer = 0 To strChildAges.Length - 1
                            childrenAges.Add(strChildAges(j))
                        Next

                    End If

                    distribution.childrenAges = childrenAges
                    distribution.numberRooms = 1 'strRooms(0)

                    Dim strInt_RoomtypeCodes As String() = Int_RoomtypeCodes.Split(";")
                    Dim strRoomCode As String() = strInt_RoomtypeCodes(i).Split(":")

                    Dim strInt_Roomtypes As String() = Int_Roomtypes.Split(";")
                    Dim strRoomIds As String() = strInt_Roomtypes(i).Split(":")

                    Dim strOfferCodes As String() = Offercode.Split(";")

                    Dim strOfferCode As String() = strOfferCodes(i).Split(":")


                    distribution.board.id = Int_mealcode 'dsMapping.Tables(0).Rows(0)("mealcode_new").ToString
                    distribution.room.id = strRoomIds(1)
                    distribution.searchcode = strOfferCode(1)
                    distribution.roomcode = strRoomCode(1)
                    dist.Add(distribution)
                Next


                objAPIHotelDetailsRequest.distribution = dist


                objAPIHotelDetailsRequest.hotel = Int_PartyCode 'dsMapping.Tables(0).Rows(0)("partycode_new").ToString
                objAPIHotelDetailsRequest.searchcode = Accomodationcode
                objAPIHotelDetailsRequest.login.country = obsourcectrycode
                objAPIHotelDetailsRequest.login.lang = "en"
                objAPIHotelDetailsRequest.login.password = "pDfekNA92pd29b2w"
                objAPIHotelDetailsRequest.login.user = "discover.saudixml"

                Dim strDetailRequest As String = New JavaScriptSerializer().Serialize(objAPIHotelDetailsRequest)
                Dim objApiController As New ApiController
                Dim strCancelPolicy As String = ""
                Dim strCancelTimeLimit As String = ""
                Dim strDetailResponse As String = objApiController.GetDetailApiResponse("http://pre-xml.seeraspain.com/rst/services/accomodation/details", strDetailRequest, "POST", "application/json")
                If strDetailResponse = "Error" Or strDetailResponse = "No Availability" Then
                    mysqlTrans.Rollback()
                    clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
                    'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
                    clsDBConnect.dbConnectionClose(mySqlConn)
                    If strDetailResponse = "No Availability" Then
                        Return "No Availability"
                    Else
                        Return "Failed to book."
                    End If

                Else

                    Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                    Dim objresponse As APIHotelDetailsResponse.HotelDetailsResponse = serializer.Deserialize(Of APIHotelDetailsResponse.HotelDetailsResponse)(strDetailResponse)
                    If objresponse.result.noRefundable = True Then
                        strCancelPolicy = "Non Refundable."
                    End If

                    For Each obj In objresponse.result.cancelConditions
                        strCancelPolicy = strCancelPolicy & "Date and time of the starting cancel penalty: " & obj.dateTime & ". "

                        If strCancelTimeLimit = "" Then
                            If obj.dateTime.ToString <> "" Then
                                strCancelTimeLimit = obj.dateTime.ToString("yyyy-MM-dd hh:mm:ss")
                            End If

                        End If
                    Next


                End If
                Dim ProcDetailAPI As String = "sp_update_booking_hotel_detail_API_temp"
                Dim sqlParamDetailAPI As New List(Of SqlParameter)
                Dim ParmDetailAPI(6) As SqlParameter
                ParmDetailAPI(0) = New SqlParameter("@requestid", CType(obrequestid, String))
                sqlParamDetailAPI.Add(ParmDetailAPI(0))
                ParmDetailAPI(1) = New SqlParameter("@rlineno", CType(OBrlineno, Integer))
                sqlParamDetailAPI.Add(ParmDetailAPI(1))
                ParmDetailAPI(2) = New SqlParameter("@APIRequest", CType(strDetailRequest, String))
                sqlParamDetailAPI.Add(ParmDetailAPI(2))
                ParmDetailAPI(3) = New SqlParameter("@APIResponse", CType(strDetailResponse, String))
                sqlParamDetailAPI.Add(ParmDetailAPI(3))
                ParmDetailAPI(4) = New SqlParameter("@CancelPolicy", CType(strCancelPolicy, String))
                sqlParamDetailAPI.Add(ParmDetailAPI(4))
                ParmDetailAPI(5) = New SqlParameter("@CancelTimeLimit", CType(strCancelTimeLimit, String))
                sqlParamDetailAPI.Add(ParmDetailAPI(5))
                ParmDetailAPI(6) = New SqlParameter("@userlogged", CType(userlogged, String))
                sqlParamDetailAPI.Add(ParmDetailAPI(6))


                objclsUtilities.ExecuteNonQuerynew(constring, ProcDetailAPI, sqlParamDetailAPI, mySqlConn, mysqlTrans)



            End If
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return "Success"
        Catch ex As Exception
            mysqlTrans.Rollback()
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return "Failed to book."
        End Try
        Return False
    End Function

    Function getrequestid() As String

        Dim mySqlConn As New SqlConnection
        Dim mySqlCmd As SqlCommand
        Dim reqid As String = ""
        Try
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            mySqlCmd = New SqlCommand("sp_get_new_request_temp", mySqlConn, mysqlTrans)
            mySqlCmd.CommandType = CommandType.StoredProcedure
            reqid = mySqlCmd.ExecuteScalar()
            mySqlCmd.Dispose()
            If reqid <> "" Then
                mysqlTrans.Commit()
            Else
                mysqlTrans.Rollback()
            End If
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return reqid
        Catch ex As Exception
            If Not mySqlConn Is Nothing Then
                If mySqlConn.State = ConnectionState.Open Then
                    mysqlTrans.Rollback()
                    clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
                    clsDBConnect.dbConnectionClose(mySqlConn)
                End If
            End If
            objclsUtilities.WriteErrorLog("DALHotelSearch :: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return reqid
        End Try
    End Function
    Function GetMinNightStayDetails(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable

        If strCheckIn <> "" Then
            Dim strDates As String() = strCheckIn.Split("/")
            strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        If strCheckOut <> "" Then
            Dim strDates As String() = strCheckOut.Split("/")
            strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If

        Dim ProcName As String
        ' ProcName = "sp_booking_minnights"
        ProcName = "New_booking_minnights"
        Dim parm(7) As SqlParameter
        parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
        parm(1) = New SqlParameter("@rmtypcode", CType(strRoomTypeCode, String))
        parm(2) = New SqlParameter("@mealcode", CType(strMealPlanCode, String))
        parm(3) = New SqlParameter("@rateplanid", CType(strRatePlancode, String))
        parm(4) = New SqlParameter("@agentcode", CType(strAgentCode, String))
        parm(5) = New SqlParameter("@sourcecountry", CType(strCountryCode, String))
        parm(6) = New SqlParameter("@checkin", CType(strCheckIn, String))
        parm(7) = New SqlParameter("@checkout", CType(strCheckOut, String))

        Dim dt As New DataTable
        dt = objclsUtilities.GetDataTable(ProcName, parm)
        Return dt

    End Function

    Function GetCancelationPolicyDetails(ByVal strPartyCode As String, ByVal strRoomTypeCode As String, ByVal strMealPlanCode As String, ByVal strRatePlancode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable

        If strCheckIn <> "" Then
            Dim strDates As String() = strCheckIn.Split("/")
            strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        If strCheckOut <> "" Then
            Dim strDates As String() = strCheckOut.Split("/")
            strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If

        Dim ProcName As String
        '  ProcName = "sp_booking_cancelpolicy"
        ProcName = "New_booking_cancelpolicy"
        Dim parm(7) As SqlParameter
        parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
        parm(1) = New SqlParameter("@rmtypcode", CType(strRoomTypeCode, String))
        parm(2) = New SqlParameter("@mealcode", CType(strMealPlanCode, String))
        parm(3) = New SqlParameter("@rateplanid", CType(strRatePlancode, String))
        parm(4) = New SqlParameter("@agentcode", CType(strAgentCode, String))
        parm(5) = New SqlParameter("@sourcecountry", CType(strCountryCode, String))
        parm(6) = New SqlParameter("@checkin", CType(strCheckIn, String))
        parm(7) = New SqlParameter("@checkout", CType(strCheckOut, String))

        Dim dt As New DataTable
        dt = objclsUtilities.GetDataTable(ProcName, parm)
        Return dt

    End Function




    Function GetCheckInAndCheckOutDetails(ByVal strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = " select requestid, isnull(CONVERT(varchar(10),min(checkin-1),103),'') CheckInPrevDay,isnull(CONVERT(varchar(10),min(checkin),103),'') CheckIn,isnull(CONVERT(varchar(10),min(checkin+1),103),'') CheckInNextDay, " _
            & " isnull(CONVERT(varchar(10),max(checkout-1),103),'') CheckOutPrevDay,isnull(CONVERT(varchar(10),max(checkout),103),'') CheckOut,isnull(CONVERT(varchar(10),max(checkout+1),103),'') CheckOutNextDay from view_booking_hotel_prearr(nolock) where requestid='" & strRequestId & "'  group by requestid having  min(checkin) <>'' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetCheckInAndCheckOutDetailsFlights(ByVal strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = " select CONVERT(varchar(10),min(checkin-1),103)CheckInPrevDay,CONVERT(varchar(10),min(checkin),103)CheckIn,CONVERT(varchar(10),max(checkin+1),103)CheckInNextDay,CONVERT(varchar(10),min(checkout-1),103)CheckOutPrevDay,CONVERT(varchar(10),max(checkout),103)CheckOut,CONVERT(varchar(10),max(checkout+1),103)CheckOutNextDay from booking_hotel_detailtemp(nolock) where requestid='" & strRequestId & "' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetSectorCheckInAndCheckOutDetails(ByVal strRequestId As String, ByVal sector As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = " select CONVERT(varchar(10),min(d.checkin),103)CheckIn,CONVERT(varchar(10),max(d.checkout),103)CheckOut from  othtypmast o(nolock),view_booking_hotel_prearr d(nolock),partymast p(nolock),sectormaster s(nolock) where  " _
                & " o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode  and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001)   " _
                & " and d.requestid= '" & strRequestId & "' and o.othtypcode='" & sector & "'"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function CheckExistsSectorCheckInAndCheckOut(ByVal strRequestId As String, ByVal sector As String, ByVal tourdate As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = " select CONVERT(varchar(10),d.checkin,103) CheckIn ,CONVERT(varchar(10),d.checkout,103)CheckOut from  othtypmast o(nolock),view_booking_hotel_prearr d(nolock),partymast p(nolock),sectormaster s(nolock) where  " _
                & " o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode  and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001)   " _
                & " and d.requestid= '" & strRequestId & "' and o.othtypcode='" & sector & "' and convert(varchar(10),'" & tourdate & "' ,111) between convert(varchar(10),d.checkin,111)  and convert(varchar(10),d.checkout,111) "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetBookingSummary(ByVal strRequestId As String, ByVal strWhiteLabel As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            ' strQuery = "select requestid,rlineno,partyname,sectorname,noofstar,rmtypname,roomclassname,noofrooms,convert(varchar(10),nights)+' Nights ' nights,checkin,checkout,CONVERT(varchar, CAST(checkin AS datetime), 103) as checkindate,CONVERT(varchar, CAST(checkout AS datetime), 103) as checkoutdate,checkin+ ' / ' +checkout bookDate,persons,salecurrcode,wlsalevalue, convert(varchar(10),wlsalevalue)+' '+salecurrcode totalPrice,convert(varchar(10),(select sum(wlsalevalue) from view_booking_hotel_detailtemp where requestid='" & strRequestId & "'))+' ' + salecurrcode as HotelTotalPrice,(select min(isnull(cancelled,0)) from booking_hotel_detail_confcanceltemp  bb where bb.requestid='" & strRequestId & "' and bb.rlineno=view_booking_hotel_detailtemp.rlineno)  cancelled  from  view_booking_hotel_detailtemp where requestid='" & strRequestId & "' "

            Dim ProcName As String
            ProcName = "GET_HOTEL_BOOKING_SUMMARY"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@WhiteLabel", CType(strWhiteLabel, String))
            objDataTable = objclsUtilities.GetDataTable(ProcName, parm)

            Return objDataTable

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetHotelSpecialEventsSummary(ByVal strRequestId As String, ByVal strRLineNo As String, ByVal strWhiteLabel As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            If strWhiteLabel = "1" Then ' Modified by abin on 20180728
                strQuery = "select  sp.requestid,sp.rlineno,convert(varchar(10),sp.spleventdate,103)spleventdate,sp.spleventcode,min(spleventname)spleventname,sum(case when isnull(hdc.cancelled,0)=1 then 0 else wlspleventvalue end)spleventvalue,sum(case when isnull(hdc.cancelled,0)=1 then 0 else wlspleventvalue end)wlspleventvalue,sum(case when isnull(hdc.cancelled,0)=1 then 0 else spleventcostvalue end)spleventcostvalue,min(hd.partycode)partycode from booking_hotel_detail_specialeventstemp(nolock) sp inner join party_splevents(nolock) s  on sp.spleventcode=s.spleventcode inner join booking_hotel_detailtemp(nolock) hd on hd.requestid=sp.requestid  and hd.partycode=s.partycode left join booking_hotel_detail_confcanceltemp hdc (nolock) on hdc.requestid=sp.requestid  and hdc.rlineno=sp.rlineno and hdc.roomno=sp.roomno where sp.requestid='" & strRequestId & "'  and sp.rlineno='" & strRLineNo & "' group by sp.requestid,sp.spleventdate,sp.rlineno,sp.spleventcode   "
            Else
                strQuery = "select  sp.requestid,sp.rlineno,convert(varchar(10),sp.spleventdate,103)spleventdate,sp.spleventcode,min(spleventname)spleventname,sum(case when isnull(hdc.cancelled,0)=1 then 0 else spleventvalue end)spleventvalue,sum(case when isnull(hdc.cancelled,0)=1 then 0 else wlspleventvalue end)wlspleventvalue,sum(case when isnull(hdc.cancelled,0)=1 then 0 else spleventcostvalue end)spleventcostvalue,min(hd.partycode)partycode from booking_hotel_detail_specialeventstemp(nolock) sp inner join party_splevents(nolock) s  on sp.spleventcode=s.spleventcode inner join booking_hotel_detailtemp(nolock) hd on hd.requestid=sp.requestid  and hd.partycode=s.partycode left join booking_hotel_detail_confcanceltemp hdc (nolock) on hdc.requestid=sp.requestid  and hdc.rlineno=sp.rlineno and hdc.roomno=sp.roomno where sp.requestid='" & strRequestId & "'  and sp.rlineno='" & strRLineNo & "' group by sp.requestid,sp.spleventdate,sp.rlineno,sp.spleventcode   "
            End If

            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetVisaSummary(ByVal strRequestId As String, Optional ByVal strwhitelabel As String = "0") As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            If strwhitelabel = "1" Then
                strQuery = "select  requestid,vlineno,visatypecode,visadate,visatypename,ctryname,noofpax,visaprice,visavalue,wlcurrcode currcode,complimentarycust,wlvisaprice,wlvisavalue,(Select t.wlvisavalue from view_booking_visatotaltempnew t where requestid=  '" & strRequestId & "') totalvisavalue , " _
                      & " (select isnull(min(isnull(cancelled,0)),0) from booking_visa_confcanceltemp  bb(nolock) where bb.requestid='" & strRequestId & "' and bb.vlineno=view_booking_visatempnew.vlineno)  cancelled,bookingmode  " _
                      & " from view_booking_visatempnew where requestid= '" & strRequestId & "' "

            Else
                strQuery = "select  *,(Select t.visavalue from view_booking_visatotaltempnew t where requestid=  '" & strRequestId & "') totalvisavalue , " _
                      & " (select isnull(min(isnull(cancelled,0)),0) from booking_visa_confcanceltemp  bb(nolock) where bb.requestid='" & strRequestId & "' and bb.vlineno=view_booking_visatempnew.vlineno)  cancelled,bookingmode  " _
                      & " from view_booking_visatempnew where requestid= '" & strRequestId & "' "

            End If

            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetFlightdetails(ByVal strRequestId As String) As DataSet
        Try
            Dim objDataSet As DataSet
            Dim strQuery As String = ""
            strQuery = "select * from view_booking_guest_flightstemp(nolock) where requestid= '" & strRequestId & "'"
            objDataSet = objclsUtilities.GetDataFromDataset(strQuery)
            Return objDataSet
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetPersonalDetails(ByVal strRequestId As String) As DataSet
        Try
            Dim objDataSet As DataSet
            Dim strQuery As String = ""
            strQuery = "select requestid,guestlineno,title,firstname,middlename,lastname,nationalitycode,ctryname,round(convert(varchar(10),childage),1) childage,visaoptions,visatypecode,visatypename,visaprice,passportno from view_booking_guesttemp(nolock) where requestid= '" & strRequestId & "'"
            objDataSet = objclsUtilities.GetDataFromDataset(strQuery)
            Return objDataSet
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetResultAsDataTable(ByVal strQuery As String) As DataTable
        Try
            Dim objDataTable As DataTable
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetTariff(ByVal strPartyCode As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Try


            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "New_booking_generalpolicy"
            Dim parm(4) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
            parm(1) = New SqlParameter("@agentcode", CType(strAgentCode, String))
            parm(2) = New SqlParameter("@sourcecountry", CType(strCountryCode, String))
            parm(3) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(4) = New SqlParameter("@checkout", CType(strCheckOut, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetSpecialOffers(ByVal strPartyCode As String, ByVal strRatePlanId As String) As DataTable
        Try


            Dim ProcName As String
            '     ProcName = "sp_booking_specialoffers"
            ProcName = "New_booking_specialoffers"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
            parm(1) = New SqlParameter("@rateplanid", CType(strRatePlanId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetHotelContruction(ByVal StrPartyCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Try


            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "New_booking_hotelconstruction"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(StrPartyCode, String))
            parm(1) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(2) = New SqlParameter("@checkout", CType(strCheckOut, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetSpecialEventsDetails(ByVal strPartCode As String, ByVal strRoomTypecode As String, ByVal strMealPlanCode As String, ByVal strCatCode As String, ByVal strAccCode As String, ByVal strRatePlanId As String, ByVal strAgentCode As String, ByVal strSourceCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String, ByVal strRoom As String, ByVal strAdult As String, ByVal strChildren As String, ByVal strChildAgeString As String, ByVal strSelectedEvents As String, ByVal strRoomString As String, ByVal strPriceOveride As String, ByVal strEditRequestId As String, ByVal strEdirRLineNo As String) As DataSet
        Try


            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ' ProcName = "sp_booking_specialevents"
            ProcName = "New_booking_specialevents"

            Dim parm(18) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(strPartCode, String))
            parm(1) = New SqlParameter("@rmtypcode", CType(strRoomTypecode, String))
            parm(2) = New SqlParameter("@mealcode", CType(strMealPlanCode, String))
            parm(3) = New SqlParameter("@rmcatcode", CType(strCatCode, String))
            parm(4) = New SqlParameter("@accommodationid", CType(strAccCode, String))
            parm(5) = New SqlParameter("@rateplanid", CType(strRatePlanId, String))
            parm(6) = New SqlParameter("@agentcode", CType(strAgentCode, String))
            parm(7) = New SqlParameter("@sourcecountry", CType(strSourceCountryCode, String))
            parm(8) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(9) = New SqlParameter("@checkout", CType(strCheckOut, String))
            parm(10) = New SqlParameter("@noofrooms", CType(strRoom, String))
            parm(11) = New SqlParameter("@noofadults", CType(strAdult, String))
            parm(12) = New SqlParameter("@noofchild", CType(strChildren, String))
            parm(13) = New SqlParameter("@childagestring", CType(strChildAgeString, String))
            parm(14) = New SqlParameter("@selectedevents", CType(strSelectedEvents, String))
            parm(15) = New SqlParameter("@hotelroomstring", CType(strRoomString, String))
            parm(16) = New SqlParameter("@reqoverride", CType(strPriceOveride, String))
            parm(17) = New SqlParameter("@requestid", CType(strEditRequestId, String))
            parm(18) = New SqlParameter("@rlineno", CType(strEdirRLineNo, String))


            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetAdultAndChildFromRoomString(ByVal strRoomString As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "GetAdultAndChildFromRoomString"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@RoomString", CType(strRoomString, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetPartyName(ByVal strHId As String) As String
        Try
            Dim strHotelName As String = ""
            Dim strQuery As String = "select partyname from partymast(nolock) where partycode='" & strHId & "' and active=1"
            strHotelName = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strHotelName
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return 0
        End Try
    End Function
    Function GetHotelBookingDetailsForShifting(ByVal strRequestId As String) As DataTable

        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select d.rlineno,d.partycode,d.partycode +'|'+ convert(varchar(2),d.rlineno) code,p.partyname+' '+convert(varchar(10),d.checkin,103)+' - '+convert(varchar(10),d.checkout,103) HotelName,d.noofrooms,adults,child,RoomString,CONVERT(varchar(10),checkout,103)checkout  from booking_hotel_detailtemp(nolock)  d,partymast(nolock)  p where d.requestid= '" & strRequestId & "' and d.partycode=p.partycode order by d.rlineno "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    'Created / changed by mohamed on 05/04/2018
    Function GetHotelBookingDetailsForShiftingNew(ByVal strRequestId As String, ByVal rlineno As Integer) As DataTable

        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "execute sp_get_shifting_hotel_detail '" & strRequestId & "', " & rlineno
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetBookingDetailsForEdit(ByVal strRequestId As Object, ByVal strRLineNo As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_booking_hotels_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@rlineno", CType(strRLineNo, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function RemoveHotelBooking(ByVal strRequestId As String, ByVal strelineno As String) As String
        Try

            Dim ProcName As String
            ProcName = "sp_del_booking_hoteltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@RowLineNo", CType(strelineno, String))
            sqlParamList.Add(parm(1))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

            Return True
        Catch ex As Exception

            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetHotelCancelDetails(ByVal strRequestId As String, ByVal strelineno As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_get_hotel_canceldetails"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@rlineno", CType(strelineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetCancelDays(ByVal strRMPartyCode As String, ByVal strRMRoomTypeCode As String, ByVal strRMMealPlanCode As String, ByVal strRMRatePlanId As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As String
        Try
            Dim strCancelDays As String
            Dim strQuery As String = ""
            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            ' strQuery = "select dbo.fn_get_canceldays('" & strRMPartyCode & "','" & strRMRoomTypeCode & "','" & strRMMealPlanCode & "','" & strRMRatePlanId & "','" & strAgentCode & "','" & strCountryCode & "','" & strCheckIn & "','" & strCheckOut & "')"
            strQuery = " select DATEDIFF (dd,getdate(),DATEADD (dd , ((select dbo.fn_get_canceldays('" & strRMPartyCode & "','" & strRMRoomTypeCode & "','" & strRMMealPlanCode & "','" & strRMRatePlanId & "','" & strAgentCode & "','" & strCountryCode & "','" & strCheckIn & "','" & strCheckOut & "')*-1))+1 ,'" & strCheckIn & "'))"
            strCancelDays = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strCancelDays
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetCancelDaysWithNonRefundable(ByVal strRMPartyCode As String, ByVal strRMRoomTypeCode As String, ByVal strRMMealPlanCode As String, ByVal strRMRatePlanId As String, ByVal strAgentCode As String, ByVal strCountryCode As String, ByVal strCheckIn As String, ByVal strCheckOut As String) As DataTable
        Try
            Dim dtCancelDays As DataTable

            Dim strQuery As String = ""
            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            ' strQuery = "select dbo.fn_get_canceldays('" & strRMPartyCode & "','" & strRMRoomTypeCode & "','" & strRMMealPlanCode & "','" & strRMRatePlanId & "','" & strAgentCode & "','" & strCountryCode & "','" & strCheckIn & "','" & strCheckOut & "')"
            strQuery = "exec sp_get_canceldays_booking '" & strRMPartyCode & "','" & strRMRoomTypeCode & "','" & strRMMealPlanCode & "','" & strRMRatePlanId & "','" & strAgentCode & "','" & strCountryCode & "','" & strCheckIn & "','" & strCheckOut & "'"
            dtCancelDays = objclsUtilities.GetDataFromDataTable(strQuery)
            Return dtCancelDays
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function


    Function PreHotelSummary(ByVal strRequestId As String, ByVal strwhite As String, ByVal RLineNo As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""

            'strQuery = "select *, convert(varchar(10),checkin,103) checkindate, convert(varchar(10),checkout,103) checkoutdate, (select partyname from partymast(nolock) p where p.partycode=hp.partycode)partyname,(select othtypname  from  othtypmast(nolock)o where o.othtypcode=hp.sectorcode)othtypname,(select agentname  from  agentmast(nolock)a where a.agentcode=hp.agentcode)agentname,(select ctryname  from  ctrymast(nolock)c where c.ctrycode=hp.sourcectrycode)ctryname  from booking_hotels_prearrangedtemp hp (nolock) where requestid='" & strRequestId & "'"
            strQuery = "sp_get_PreHotelSummary '" & strRequestId & "','" & RLineNo & "',''"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function RemovePreArrangedHotel(ByVal strRequestId As Object, ByVal rLineNo As String) As String
        Try

            Dim ProcName As String
            ProcName = "sp_del_booking_prearranged_hoteltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@RowLineNo", CType(rLineNo, String))
            sqlParamList.Add(parm(1))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

            Return True
        Catch ex As Exception

            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetAdultAndChildSum(strRoomString As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""

            strQuery = "select sum(convert(int,r2.Item1))adult,sum(convert(int,r3.Item1))child 	from dbo.SplitString1colsWithOrderField('" & strRoomString & "',';') r 	cross apply dbo.SplitString1colsWithOrderField(r.item1,',') r1 	cross apply dbo.SplitString1colsWithOrderField(r.item1,',') r2 	cross apply dbo.SplitString1colsWithOrderField(r.item1,',') r3 	cross apply dbo.SplitString1colsWithOrderField(r.item1,',') r4 	where r1.ordNo=1 and r2.ordNo=2 and r3.ordNo=3 and r4.ordNo=4 "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetNew_booking_OneTimePay(strPartyCode As String, strRoomTypeCode As String, strMealPlanCode As String, strRatePlancode As String, strAgentCode As String, strCountryCode As String, strCheckIn As String, strCheckOut As String, ByVal strNoOfExtraBed As String, ByVal strRequestId As String, ByVal strrlineno As String) As DataTable
        If strCheckIn <> "" Then
            Dim strDates As String() = strCheckIn.Split("/")
            strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        If strCheckOut <> "" Then
            Dim strDates As String() = strCheckOut.Split("/")
            strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If

        Dim ProcName As String
        ' ProcName = "sp_booking_minnights"
        ProcName = "New_booking_OneTimePay"
        Dim parm(10) As SqlParameter
        parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
        parm(1) = New SqlParameter("@rmtypcode", CType(strRoomTypeCode, String))
        parm(2) = New SqlParameter("@mealcode", CType(strMealPlanCode, String))
        parm(3) = New SqlParameter("@rateplanid", CType(strRatePlancode, String))
        parm(4) = New SqlParameter("@agentcode", CType(strAgentCode, String))
        parm(5) = New SqlParameter("@sourcecountry", CType(strCountryCode, String))
        parm(6) = New SqlParameter("@checkin", CType(strCheckIn, String))
        parm(7) = New SqlParameter("@checkout", CType(strCheckOut, String))
        parm(8) = New SqlParameter("@NoEB", CType(strNoOfExtraBed, String))
        parm(9) = New SqlParameter("@requestid", CType(strRequestId, String))
        parm(10) = New SqlParameter("@rlineno", CType(strrlineno, String))

        Dim dt As New DataTable
        dt = objclsUtilities.GetDataTable(ProcName, parm)
        Return dt
    End Function


    Function GetBooking_checkinoutpolicy(strPartyCode As String, strRoomTypeCode As String, strMealPlanCode As String, strRatePlancode As String, strAgentCode As String, strCountryCode As String, strCheckIn As String, strCheckOut As String) As DataTable
        If strCheckIn <> "" Then
            Dim strDates As String() = strCheckIn.Split("/")
            strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        If strCheckOut <> "" Then
            Dim strDates As String() = strCheckOut.Split("/")
            strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If

        Dim ProcName As String
        ' ProcName = "sp_booking_minnights"
        ProcName = "New_booking_checkinoutpolicy"
        Dim parm(7) As SqlParameter
        parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
        parm(1) = New SqlParameter("@rmtypcode", CType(strRoomTypeCode, String))
        parm(2) = New SqlParameter("@mealcode", CType(strMealPlanCode, String))
        parm(3) = New SqlParameter("@rateplanid", CType(strRatePlancode, String))
        parm(4) = New SqlParameter("@agentcode", CType(strAgentCode, String))
        parm(5) = New SqlParameter("@sourcecountry", CType(strCountryCode, String))
        parm(6) = New SqlParameter("@checkin", CType(strCheckIn, String))
        parm(7) = New SqlParameter("@checkout", CType(strCheckOut, String))



        Dim dt As New DataTable
        dt = objclsUtilities.GetDataTable(ProcName, parm)
        Return dt
    End Function

    Function GetAirportTransferCompliment(strPartyCode As String, strRatePlanId As String) As DataTable
        Try


            Dim ProcName As String
            '     ProcName = "sp_booking_specialoffers"
            ProcName = "New_booking_OfferTransfer"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
            parm(1) = New SqlParameter("@rateplanid", CType(strRatePlanId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetMappingFields(ByVal strClientName As String, ByVal strSourceCountryCode As String, ByVal strMealPlan As String, ByVal strPropertyType As String, ByVal strHotelCode As String, ByVal strStarCategoryCode As String, ByVal strDestinationCode As String, ByVal strDestinationType As String) As DataSet
        Try

            Dim ProcName As String
            ProcName = "sp_masters_mapping"
            Dim parm(7) As SqlParameter
            parm(0) = New SqlParameter("@clientname", CType(strClientName, String))
            parm(1) = New SqlParameter("@ctrycode", CType(strSourceCountryCode, String))

            parm(2) = New SqlParameter("@mealcode", CType(strMealPlan, String))
            parm(3) = New SqlParameter("@propertytypecode", CType(strPropertyType, String))

            parm(4) = New SqlParameter("@partycode", CType(strHotelCode, String))
            parm(5) = New SqlParameter("@catcode", CType(strStarCategoryCode, String))

            parm(6) = New SqlParameter("@desttype", CType(strDestinationType, String))
            parm(7) = New SqlParameter("@desttypecode", CType(strDestinationCode, String))
            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetOrCreateRoomType(strPartyCode As String, strRoomTypes As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_GetOrCreateRoomType"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@PartyCode", CType(strPartyCode, String))
            parm(1) = New SqlParameter("@RoomTypes", CType(strRoomTypes, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function CallHotelDetailsAPIAndSave(objBLLHotelSearch As BLLHotelSearch) As String


        Dim objDALHotelSearchNew As DALHotelSearch = New DALHotelSearch()
        'Dim dsMapping As DataSet = objDALHotelSearchNew.GetMappingFields("OneDMC", objBLLHotelSearch.SourceCountryCode, objBLLHotelSearch.MealPlan, objBLLHotelSearch.PropertyType, objBLLHotelSearch.HotelCode, objBLLHotelSearch.StarCategoryCode, objBLLHotelSearch.DestinationCode, objBLLHotelSearch.DestinationType)
        Dim objResult As Object
        'If dsMapping.Tables(1).Rows.Count > 0 Then
        '    objResult = Nothing
        '    Return objResult
        'End If

        Dim objAPIHotelDetailsRequest As APIHotelDetailsRequest.HotelDetailsRequest = New APIHotelDetailsRequest.HotelDetailsRequest()
        Dim CheckIn As String = objBLLHotelSearch.CheckIn
        Dim CheckOut As String = objBLLHotelSearch.CheckOut
        If CheckIn <> "" Then
            Dim strDates As String() = CheckIn.Split("/")
            CheckIn = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
        End If
        If CheckOut <> "" Then
            Dim strDates As String() = CheckOut.Split("/")
            CheckOut = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
        End If

        objAPIHotelDetailsRequest.arrivalDate = CheckIn
        objAPIHotelDetailsRequest.departureDate = CheckOut
        Dim strRoomString As String = objBLLHotelSearch.RoomString
        Dim strRoomsAll() = strRoomString.Split(";")
        Dim ilenth As Integer = strRoomsAll.Length - 1
        Dim distribution As APIHotelDetailsRequest.Distribution = New APIHotelDetailsRequest.Distribution()
        Dim dist As New List(Of APIHotelDetailsRequest.Distribution)
        ' Dim dist(ilenth) As APIHotelDetailsRequest.Distribution


        For i As Integer = 0 To strRoomsAll.Length - 1
            Dim strRooms() = strRoomsAll(i).Split(",")
            distribution = New APIHotelDetailsRequest.Distribution()
            distribution.numberAdults = strRooms(1)
            distribution.numberChildren = strRooms(2)
            Dim strChildAges As String() = strRooms(3).Split("|")
            Dim childrenAges As New List(Of Integer)
            If strRooms(2) > 0 Then

                For j As Integer = 0 To strChildAges.Length - 1
                    childrenAges.Add(strChildAges(j))
                Next

            End If

            distribution.childrenAges = childrenAges
            distribution.numberRooms = strRooms(0)

            Dim strInt_RoomtypeCodes As String() = objBLLHotelSearch.Int_RoomtypeCodes.Split(";")
            Dim strRoomCode As String() = strInt_RoomtypeCodes(i).Split(":")

            Dim strInt_Roomtypes As String() = objBLLHotelSearch.Int_Roomtypes.Split(";")
            Dim strRoomIds As String() = strInt_Roomtypes(i).Split(":")

            Dim strOfferCodes As String() = objBLLHotelSearch.Offercode.Split(";")

            Dim strOfferCode As String() = strOfferCodes(i).Split(":")


            distribution.board.id = objBLLHotelSearch.Int_mealcode 'dsMapping.Tables(0).Rows(0)("mealcode_new").ToString
            distribution.room.id = strRoomIds(1)
            distribution.searchcode = strOfferCode(1)
            distribution.roomcode = strRoomCode(1)
            dist.Add(distribution)
        Next


        objAPIHotelDetailsRequest.distribution = dist


        objAPIHotelDetailsRequest.hotel = objBLLHotelSearch.Int_PartyCode 'dsMapping.Tables(0).Rows(0)("partycode_new").ToString
        objAPIHotelDetailsRequest.searchcode = objBLLHotelSearch.Accomodationcode
        objAPIHotelDetailsRequest.login.country = objBLLHotelSearch.SourceCountryCode
        objAPIHotelDetailsRequest.login.lang = "en"
        objAPIHotelDetailsRequest.login.password = "pDfekNA92pd29b2w"
        objAPIHotelDetailsRequest.login.user = "discover.saudixml"

        Dim sb As String = New JavaScriptSerializer().Serialize(objAPIHotelDetailsRequest)
        Dim objApiController As New ApiController
        Dim strDetailResponse As String = objApiController.GetDetailApiResponse("http://pre-xml.seeraspain.com/rst/services/accomodation/details", sb, "POST", "application/json")

        Return ""



    End Function

    Function GetInt_TariffNote(strPartyCode As String, strAgentCode As String, strCountryCode As String, strCheckIn As String, strCheckOut As String) As DataTable
        Try


            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "int_booking_generalpolicy"
            Dim parm(5) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(strPartyCode, String))
            parm(1) = New SqlParameter("@agentcode", CType(strAgentCode, String))
            parm(2) = New SqlParameter("@sourcecountry", CType(strCountryCode, String))
            parm(3) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(4) = New SqlParameter("@checkout", CType(strCheckOut, String))
            parm(5) = New SqlParameter("@mappingSource", CType("1", String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetInt_HotelContruction(StrPartyCode As String, strCheckIn As String, strCheckOut As String) As DataTable
        Try


            If strCheckIn <> "" Then
                Dim strDates As String() = strCheckIn.Split("/")
                strCheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If strCheckOut <> "" Then
                Dim strDates As String() = strCheckOut.Split("/")
                strCheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "int_booking_hotelconstruction"
            Dim parm(3) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(StrPartyCode, String))
            parm(1) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(2) = New SqlParameter("@checkout", CType(strCheckOut, String))
            parm(3) = New SqlParameter("@mappingSource", CType("1", String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

End Class
