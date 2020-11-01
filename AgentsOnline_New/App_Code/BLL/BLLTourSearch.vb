Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLTourSearch

    Dim _FromDate As String = ""
    Dim _ToDate As String = ""
    Dim _NoOfNights As String = ""
    Dim _TourStartingFrom As String = ""
    Dim _TourStartingFromCode As String = ""
    Dim _Classification As String = ""
    Dim _ClassificationCode As String = ""
    Dim _StarCategory As String = ""
    Dim _StarCategoryCode As String = ""
    Dim _SourceCountry As String = ""
    Dim _SourceCountryCode As String = ""
    Dim _Customer As String = ""
    Dim _CustomerCode As String = ""
    Dim _SeniorCitizen As String = ""
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
    Dim _Child9 As String = ""

    Dim _PrivateOrSIC As String = ""
    Dim _OverridePrice As String = ""
    Dim _ChildAgeString As String = ""
    Dim _DateChange As String = "0"
    Dim _LoginType As String = ""
    Dim _WebUserName As String = ""
    Dim _AgentCode As String = ""
    Dim _ExcTypeCode As String = ""
    Dim _VehicleCode As String = ""
    Dim _SelectedDate As String = ""
    Dim _EBToursXml As String = ""
    Dim _EBRequestId As String = ""
    Dim _EBuserlogged As String = ""
    Dim _EBdiv_code As String = ""
    Dim _EBsourcectrycode As String = ""
    Dim _EBremarks As String = ""
    Dim _EBreqoverride As String = ""
    Dim _EBagentref As String = ""
    Dim _EBcolumbusref As String = ""
    Dim _RowLineNo As String = ""

    Dim _EBCancelToursXml As String = ""


    Dim _AmendRequestid As String = ""
    Dim _AmendLineno As String = ""
    Dim objDALTourSearch As New DALTourSearch
    Dim _SubUserCode As String = ""
    Dim _SectorgroupCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Public Property FromDate As String
        Get
            Return _FromDate
        End Get
        Set(ByVal value As String)
            _FromDate = value
        End Set
    End Property

    Public Property ToDate As String
        Get
            Return _ToDate
        End Get
        Set(ByVal value As String)
            _ToDate = value
        End Set
    End Property

    Public Property TourStartingFrom As String
        Get
            Return _TourStartingFrom
        End Get
        Set(ByVal value As String)
            _TourStartingFrom = value
        End Set
    End Property

    Public Property TourStartingFromCode As String
        Get
            Return _TourStartingFromCode
        End Get
        Set(ByVal value As String)
            _TourStartingFromCode = value
        End Set
    End Property
    Public Property Classification As String
        Get
            Return _Classification
        End Get
        Set(ByVal value As String)
            _Classification = value
        End Set
    End Property
    Public Property ClassificationCode As String
        Get
            Return _ClassificationCode
        End Get
        Set(ByVal value As String)
            _ClassificationCode = value
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
    Public Property SeniorCitizen As String
        Get
            Return _SeniorCitizen
        End Get
        Set(ByVal value As String)
            _SeniorCitizen = value
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
    Public Property Child9 As String
        Get
            Return _Child9
        End Get
        Set(ByVal value As String)
            _Child9 = value
        End Set
    End Property

    Public Property PrivateOrSIC As String
        Get
            Return _PrivateOrSIC
        End Get
        Set(ByVal value As String)
            _PrivateOrSIC = value
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
    Public Property ChildAgeString As String
        Get
            Return _ChildAgeString
        End Get
        Set(ByVal value As String)
            _ChildAgeString = value
        End Set
    End Property
    Public Property DateChange As String
        Get
            Return _DateChange
        End Get
        Set(ByVal value As String)
            _DateChange = value
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

    Public Property ExcTypeCode As String
        Get
            Return _ExcTypeCode
        End Get
        Set(ByVal value As String)
            _ExcTypeCode = value
        End Set
    End Property
    Public Property VehicleCode As String
        Get
            Return _VehicleCode
        End Get
        Set(ByVal value As String)
            _VehicleCode = value
        End Set
    End Property

    Public Property SelectedDate As String
        Get
            Return _SelectedDate
        End Get
        Set(ByVal value As String)
            _SelectedDate = value
        End Set
    End Property
    Public Property EBToursXml As String
        Get
            Return _EBToursXml

        End Get
        Set(value As String)
            _EBToursXml = value
        End Set
    End Property
    Public Property EBCancelToursXml As String
        Get
            Return _EBCancelToursXml

        End Get
        Set(ByVal value As String)
            _EBCancelToursXml = value
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
    Public Property EbRequestID As String
        Get
            Return _EBRequestID

        End Get
        Set(value As String)
            _EBRequestID = value
        End Set
    End Property
    Public Property EBuserlogged As String
        Get
            Return _EBuserlogged

        End Get
        Set(value As String)
            _EBuserlogged = value
        End Set
    End Property

    Public Property EBdiv_code As String
        Get
            Return _EBdiv_code

        End Get
        Set(value As String)
            _EBdiv_code = value
        End Set
    End Property

    Public Property EBsourcectrycode As String
        Get
            Return _EBsourcectrycode

        End Get
        Set(value As String)
            _EBsourcectrycode = value
        End Set
    End Property
    Public Property EBreqoverride As String
        Get
            Return _EBreqoverride

        End Get
        Set(value As String)
            _EBreqoverride = value
        End Set
    End Property
    Public Property EBagentref As String
        Get
            Return _EBagentref

        End Get
        Set(value As String)
            _EBagentref = value
        End Set
    End Property
    Public Property EBcolumbusref As String
        Get
            Return _EBcolumbusref

        End Get
        Set(value As String)
            _EBcolumbusref = value
        End Set
    End Property
    Public Property EBremarks As String
        Get
            Return _EBremarks

        End Get
        Set(value As String)
            _EBremarks = value
        End Set
    End Property
    Public Property RowLineNo As String
        Get
            Return _RowLineNo
        End Get
        Set(ByVal value As String)
            _RowLineNo = value
        End Set
    End Property
    Public Property SectorgroupCode As String
        Get
            Return _SectorgroupCode
        End Get
        Set(ByVal value As String)
            _SectorgroupCode = value
        End Set
    End Property
    Private _OpMode As String = ""
    Public Property OpMode As String
        Get
            Return _OpMode
        End Get
        Set(ByVal value As String)
            _OpMode = value
        End Set
    End Property
    Private _BufferMultiCostBreakup As String = ""
    Public Property BufferMultiCostBreakup As String
        Get
            Return _BufferMultiCostBreakup
        End Get
        Set(ByVal value As String)
            _BufferMultiCostBreakup = value
        End Set
    End Property
    Private strBufferComboBreakup As String = ""
    Public Property BufferComboBreakup As String
        Get
            Return strBufferComboBreakup
        End Get
        Set(ByVal value As String)
            strBufferComboBreakup = value
        End Set
    End Property

    Private strBufferInventoryBreakup As String = ""
    Public Property BufferInventoryBreakup As String
        Get
            Return strBufferInventoryBreakup
        End Get
        Set(ByVal value As String)
            strBufferInventoryBreakup = value
        End Set
    End Property

    Function SaveExcursionTypeBookingInTemp() As Boolean
        Dim result As Boolean

        objDALTourSearch.SubUserCode = SubUserCode
        result = objDALTourSearch.SaveExcursionTypeBookingInTemp(EBdiv_code, AgentCode, EBsourcectrycode, EBreqoverride, EbRequestID, EBagentref, EBcolumbusref, EBremarks, EBToursXml, EBuserlogged, RowLineNo, SectorgroupCode, OpMode, BufferMultiCostBreakup, BufferComboBreakup, BufferInventoryBreakup)
        Return result

    End Function




    Function GetSearchDetails() As DataSet
        Dim dsHotelSearch As New DataSet
        dsHotelSearch = objDALTourSearch.GetSearchDetails(LoginType, WebuserName, AgentCode, FromDate, ToDate, TourStartingFromCode, ClassificationCode, StarCategoryCode, Adult, Children, ChildAgeString, SeniorCitizen, SourceCountryCode, OverridePrice, DateChange, ExcTypeCode, VehicleCode, SelectedDate, PrivateOrSIC, AmendRequestid, AmendLineno)
        Return dsHotelSearch

    End Function

    Function GetTourSummary(strRequestId As String, strWhiteLabel As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTourSearch.GetTourSummary(strRequestId, strWhiteLabel)
        Return dt
    End Function

    Function GetEditBookingDetails(ByVal strRequestId As String, ByVal strelineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTourSearch.GetEditBookingDetails(strRequestId, strelineno)
        Return dt
    End Function

    Sub RemoveTours(ByVal strRequestId As String, ByVal strelineno As String)
        Dim objDALTourSearch As New DALTourSearch
        objDALTourSearch.RemoveTours(strRequestId, strelineno)

    End Sub

    Function GetTourCancelDetails(ByVal strRequestId As String, ByVal strelineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTourSearch.GetTourCancelDetails(strRequestId, strelineno)
        Return dt
    End Function
    Function SavingCancelTourInTemp() As Boolean
        Dim res As Boolean
        objDALTourSearch.EBrequestid = EbRequestID
        objDALTourSearch.EBCancelToursXml = EBCancelToursXml
        objDALTourSearch.EBuserlogged = EBuserlogged
        objDALTourSearch.SubUserCode = SubUserCode
        res = objDALTourSearch.SavingCancelTourInTemp()
        Return res
    End Function

    Function ValidateExcWeekDays(ExcTypeCode As String, ExcDate As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.ValidateExcWeekDays(ExcTypeCode, ExcDate)
        Return dt
    End Function

    Function FillTourPickupLocation(strRequestId As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.FillTourPickupLocation(strRequestId)
        Return dt
    End Function

    Function GetComboExcursions_WithRateBasis(ByVal ExcCode As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.GetComboExcursions_WithRateBasis(ExcCode)
        Return dt
    End Function
    Function GetMultipleDates_WithRateBasis(ByVal strFromDate As String, ByVal strToDate As String, ByVal ExcCode As String) As DataSet
        Dim ds As DataSet
        ds = objDALTourSearch.GetMultipleDates_WithRateBasis(strFromDate, strToDate, ExcCode)
        Return ds
    End Function
    Function GetComboExcursions(ExcCode As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.GetComboExcursions(ExcCode)
        Return dt
    End Function

    Function GetMultipleDates(strFromDate As String, strToDate As String, ExcCode As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.GetMultipleDates(strFromDate, strToDate, ExcCode)
        Return dt
    End Function

    Function BindComboTourDataTable(strrequestid As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.BindComboTourDataTable(strrequestid)
        Return dt
    End Function

    Function GetBookedComboMultiDateDetails(strrequestid As String, excCode As String, Type As String, ElineNo As String) As DataTable
        Dim dt As DataTable
        dt = objDALTourSearch.GetBookedComboMultiDateDetails(strrequestid, excCode, Type, ElineNo)
        Return dt
    End Function
    Function GetTourCheckInAndCheckOutDetails(strRequestId As String, sector As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTourSearch.GetTourCheckInAndCheckOutDetails(strRequestId, sector)
        Return dt
    End Function

    Function ValidateTourSearchDateGaps(strRequestId As String, strFromDate As String, strToDate As String) As Boolean
        Return objDALTourSearch.ValidateTourSearchDateGaps(strRequestId, strFromDate, strToDate)
    End Function

    Function BindExcInventoryDataTable(strrequestid As String, strSectorCode As String) As DataTable
        Dim dt As New DataTable
        dt = objDALTourSearch.BindExcInventoryDataTable(strrequestid, strSectorCode)
        Return dt
    End Function

End Class
