Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Win32

Public Class DALHotelFreeFormBooking

    Dim objclsUtilities As New clsUtilities()
    Dim mysqlTrans As SqlTransaction


    Private _Div_Code As String = ""
    Private _Requestid As String = ""
    Private _AgentCode As String = ""
    Private _SourceCtryCode As String = ""
    Private _AgentRef As String = ""
    Private _ColumbusRef As String = ""
    Private _Remarks As String = ""
    Private _SubUserCode As String = ""

    Private _RlineNo As String = ""
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
    ''' <summary>
    ''' GetFreeFormPricesBasedOnRooms
    ''' </summary>
    ''' <param name="strCheckIn"></param>
    ''' <param name="strCheckOut"></param>
    ''' <param name="strRoom"></param>
    ''' <param name="strRoomString"></param>
    ''' <param name="strHotelCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFreeFormPricesBasedOnRooms(ByVal strCheckIn As String, ByVal strCheckOut As String, ByVal strRoom As String, ByVal strRoomString As String, ByVal strHotelCode As String, strAgentCode As String, strRandomNumber As String) As DataTable
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
            Dim dt As DataTable
            ProcName = "Sp_GetFreeFormRoomPrices"
            Dim parm(6) As SqlParameter
            parm(0) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(1) = New SqlParameter("@checkout", CType(strCheckOut, String))
            parm(2) = New SqlParameter("@noofrooms", CType(strRoom, String))
            parm(3) = New SqlParameter("@roomstring", CType(strRoomString, String))
            parm(4) = New SqlParameter("@partycode", CType(strHotelCode, String))
            parm(5) = New SqlParameter("@agentcode", CType(strAgentCode, String))
            parm(6) = New SqlParameter("@randomnumber", CType(strRandomNumber, String))

            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' GetFreeFormSpecialEvents
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
            Dim ds As DataSet
            ProcName = "sp_booking_specialevents_freeform"
            Dim parm(13) As SqlParameter
            parm(0) = New SqlParameter("@partycode", CType(strHotelCode, String))
            parm(1) = New SqlParameter("@rmtypcode", CType(strRoomTypeCode, String))
            parm(2) = New SqlParameter("@mealcode", CType(strMealPlanCode, String))
            parm(3) = New SqlParameter("@rmcatcode", CType(strRoomCatCode, String))
            parm(4) = New SqlParameter("@agentcode", CType(strCustomerCode, String))
            parm(5) = New SqlParameter("@sourcecountry", CType(strSourceCountryCode, String))
            parm(6) = New SqlParameter("@checkin", CType(strCheckIn, String))
            parm(7) = New SqlParameter("@checkout", CType(strCheckOut, String))
            parm(8) = New SqlParameter("@noofrooms", CType(strRoom, String))
            parm(9) = New SqlParameter("@roomstring", CType(strRoomString, String))
            parm(10) = New SqlParameter("@selectedevents", CType(strSelectedEvents, String))
            parm(11) = New SqlParameter("@hotelroomstring", CType(strHotelString, String))
            parm(12) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(13) = New SqlParameter("@rlineno", CType(strRLineNo, String))

            ds = objclsUtilities.GetDataSet(ProcName, parm)


            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' GetSpecialEvents
    ''' </summary>
    ''' <param name="strHotelCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSpecialEvents(strHotelCode As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select spleventcode,spleventname from party_splevents(nolock) where partycode='" & strHotelCode & "' and inactive=0"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SaveFreeFormHotelBookinging() As String
        Dim mySqlConn As New SqlConnection
        'Dim mysqlTrans As SqlTransaction
        Try

            'changed by mohamed on 03/09/2018
            Dim dtSet As DataSet, lGShitDetXML As String
            dtSet = objclsUtilities.GetDataFromDataset("execute sp_get_shifting_hotel_detail_guestpage '" & Requestid & "',-1")
            lGShitDetXML = objclsUtilities.GenerateXML(dtSet)

            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction


            If CheckIn <> "" Then
                Dim strDates As String() = CheckIn.Split("/")
                CheckIn = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            If CheckOut <> "" Then
                Dim strDates As String() = CheckOut.Split("/")
                CheckOut = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If


            Dim ProcName As String
            ProcName = "sp_save_freeformbooking_hotel"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(49) As SqlParameter

            parm(0) = New SqlParameter("@div_code", CType(Div_Code, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(Requestid, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@sourcectrycode", CType(SourceCtryCode, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@reqoverride", CType(OverridePrice, Integer))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@agentref", CType(AgentRef, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@columbusref", CType(ColumbusRef, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@remarks", CType(Remarks, String))
            sqlParamList.Add(parm(7))
            parm(8) = New SqlParameter("@SubUserCode", CType(SubUserCode, String))
            sqlParamList.Add(parm(8))
            parm(9) = New SqlParameter("@userlogged", CType(UserLogged, String))
            sqlParamList.Add(parm(9))
           

            parm(10) = New SqlParameter("@rlineno", CType(RlineNo, Integer))
            sqlParamList.Add(parm(10))
            parm(11) = New SqlParameter("@checkin", Format(CType(CheckIn, Date), "yyyy/MM/dd"))
            sqlParamList.Add(parm(11))
            parm(12) = New SqlParameter("@checkout", Format(CType(CheckOut, Date), "yyyy/MM/dd"))
            sqlParamList.Add(parm(12))
            parm(13) = New SqlParameter("@noofrooms", CType(NoofRooms, Integer))
            sqlParamList.Add(parm(13))
            parm(14) = New SqlParameter("@adults", CType(Adults, Integer))
            sqlParamList.Add(parm(14))
            parm(15) = New SqlParameter("@child", CType(Child, Integer))
            sqlParamList.Add(parm(15))
            parm(16) = New SqlParameter("@childages", CType(ChildAges, String))
            sqlParamList.Add(parm(16))
            parm(17) = New SqlParameter("@sharingorextrabed", CType(SharingOrExtraBed, String))
            sqlParamList.Add(parm(17))

            parm(18) = New SqlParameter("@noofadulteb", CType(NoOfAdultEb, String))
            sqlParamList.Add(parm(18))
            parm(19) = New SqlParameter("@noofchildeb", CType(NoOfChildEb, String))
            sqlParamList.Add(parm(19))

            parm(20) = New SqlParameter("@supagentcode", CType(SupAgentCode, String))
            sqlParamList.Add(parm(20))
            parm(21) = New SqlParameter("@partycode", CType(PartyCode, String))
            sqlParamList.Add(parm(21))
            parm(22) = New SqlParameter("@rateplanid", CType(RateplanId, String))
            sqlParamList.Add(parm(22))
            parm(23) = New SqlParameter("@rateplanname", CType(RatePlanName, String))
            sqlParamList.Add(parm(23))
            parm(24) = New SqlParameter("@rmtypcode", CType(RoomTypeCode, String))
            sqlParamList.Add(parm(24))
            parm(25) = New SqlParameter("@roomclasscode", CType(RoomClassCode, String))
            sqlParamList.Add(parm(25))
            parm(26) = New SqlParameter("@rmcatcode", CType(RoomCatCode, String))
            sqlParamList.Add(parm(26))
            'parm(18) = New SqlParameter("@accommodationid", CType(OBaccommodationid, Integer))
            'sqlParamList.Add(parm(18))
            parm(27) = New SqlParameter("@mealplans", CType(MealPlans, String))
            sqlParamList.Add(parm(27))
            parm(28) = New SqlParameter("@salevalue", CType(SaleValue, Decimal))
            sqlParamList.Add(parm(28))
            parm(29) = New SqlParameter("@salecurrcode", CType(SaleCurrCode, String))
            sqlParamList.Add(parm(29))
            parm(30) = New SqlParameter("@costvalue", CType(CostValue, Decimal))
            sqlParamList.Add(parm(30))
            parm(31) = New SqlParameter("@costcurrcode", CType(CostCurrCode, String))
            sqlParamList.Add(parm(31))
            parm(32) = New SqlParameter("@wlsalevalue", CType(WlSaleValue, Decimal))
            sqlParamList.Add(parm(32))
            parm(33) = New SqlParameter("@available", CType(Available, Integer))
            sqlParamList.Add(parm(33))
            parm(34) = New SqlParameter("@comp_cust", CType(Comp_Cust, Integer))
            sqlParamList.Add(parm(34))
            parm(35) = New SqlParameter("@comp_supp", CType(Comp_Supp, Integer))
            sqlParamList.Add(parm(35))
            parm(36) = New SqlParameter("@comparrtrf", CType(Comparrtrf, Integer))
            sqlParamList.Add(parm(36))
            parm(37) = New SqlParameter("@compdeptrf", CType(Compdeptrf, Integer))
            sqlParamList.Add(parm(37))
            parm(38) = New SqlParameter("@RoomString", CType(RoomString, String))
            sqlParamList.Add(parm(38))
            parm(39) = New SqlParameter("@Shifting", CType(Shifting, String))
            sqlParamList.Add(parm(39))
            parm(40) = New SqlParameter("@ShiftingCode", CType(ShiftingCode, String))
            sqlParamList.Add(parm(40))
            parm(41) = New SqlParameter("@ShiftingLineNo", CType(ShiftingLineNo, String))
            sqlParamList.Add(parm(41))
            parm(42) = New SqlParameter("@wlcurrcode", CType(wlCurrCode, String))
            sqlParamList.Add(parm(42))
            parm(43) = New SqlParameter("@overrideprice", CType(OverridePrice, String))
            sqlParamList.Add(parm(43))

            parm(44) = New SqlParameter("@PriceBreakupXMLInput", CType(PriceBreakupXMLInput, String))
            sqlParamList.Add(parm(44))


            parm(45) = New SqlParameter("@specialxml", CType(SpecialEventsXML, String))
            sqlParamList.Add(parm(45))
            parm(46) = New SqlParameter("@NonRefundable", CType(NonRefundable, String))
            sqlParamList.Add(parm(46))
            parm(47) = New SqlParameter("@CancelFreeUpto", CType(CancelFreeUpto, String))
            sqlParamList.Add(parm(47))

            'changed by mohamed on 03/09/2018
            parm(48) = New SqlParameter("@guestshifttableXML", lGShitDetXML)
            parm(48).DbType = DbType.Xml
            sqlParamList.Add(parm(48))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception
            mysqlTrans.Rollback()
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetAccomodationMasterDetails(strAutoId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select * from accommodation_master(nolock) where autoid='" & strAutoId & "' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetHotelFreeFormBookingDetailsForEdit(ByVal strRequestId As Object, ByVal strRLineNo As String) As DataSet
        Try

            Dim ProcName As String
            ProcName = "sp_booking_hotel_freeform_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@rlineno", CType(strRLineNo, String))
            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetDefaultSupplierAgent() As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select supagentcode,supagentname from supplier_agents(nolock) where supagentcode in (select option_selected from reservation_parameters where param_id=520)"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
        '
    End Function

End Class
