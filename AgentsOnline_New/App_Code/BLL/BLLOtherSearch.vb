Imports Microsoft.VisualBasic
Imports System.Data

Public Class BLLOtherSearch

    Dim _FromDate As String = ""
    Dim _ToDate As String = ""
    Dim _SelectGroupCode As String = ""
    Dim _SelectGroup As String = ""
    Dim _SourceCountry As String = ""
    Dim _SourceCountryCode As String = ""
    Dim _Customer As String = ""
    Dim _CustomerCode As String = ""

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
    Dim _OverridePrice As String = ""
    Dim _ChildAgeString As String = ""

    Dim _DateChange As String = "0"
    Dim _LoginType As String = ""
    Dim _WebUserName As String = ""
    Dim _AgentCode As String = ""
    Dim _OBOtherXml As String = ""
    Dim _SelectedDate As String = ""

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
    Dim _OBComplimentaryCust As Integer

    Dim _AmendRequestid As String = ""
    Dim _AmendLineno As String = ""
    Dim _ResetSearch As String = ""

    Dim _OBOtherCancelXml As String = ""

    Dim _OtherTypeCode As String = ""
    Dim objDALOtherSearch As New DALOtherSearch


    Dim _SubUserCode As String = ""
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

    Public Property SelectGroup As String
        Get
            Return _SelectGroup
        End Get
        Set(ByVal value As String)
            _SelectGroup = value
        End Set
    End Property
    Public Property SelectGroupCode As String
        Get
            Return _SelectGroupCode
        End Get
        Set(ByVal value As String)
            _SelectGroupCode = value
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
    Public Property OBOtherXml As String
        Get
            Return _OBOtherXml
        End Get
        Set(ByVal value As String)
            _OBOtherXml = value
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

    Public Property OBComplimentaryCust As String
        Get
            Return _OBComplimentaryCust
        End Get
        Set(ByVal value As String)
            _OBComplimentaryCust = value
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

    Public Property OtherTypeCode As String
        Get
            Return _OtherTypeCode
        End Get
        Set(ByVal value As String)
            _OtherTypeCode = value
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
    Public Property ResetSearch As String
        Get
            Return _ResetSearch
        End Get
        Set(ByVal value As String)
            _ResetSearch = value
        End Set
    End Property
    Public Property OBOtherCancelXml As String
        Get
            Return _OBOtherCancelXml
        End Get
        Set(ByVal value As String)
            _OBOtherCancelXml = value
        End Set
    End Property
    Function GetSearchDetails() As DataSet
        Dim dsHotelSearch As New DataSet
        dsHotelSearch = objDALOtherSearch.GetSearchDetails(LoginType, WebuserName, AgentCode, FromDate, ToDate, SelectGroupCode, Adult, Children, ChildAgeString, SourceCountryCode, OverridePrice, DateChange, OtherTypeCode, SelectedDate, AmendRequestid, AmendLineno, ResetSearch)
        Return dsHotelSearch

    End Function
    Function SaveOtherserviceBookingInTemp() As Boolean
        Dim result As Boolean

        objDALOtherSearch.SubUserCode = SubUserCode
        result = objDALOtherSearch.SaveOtherServiceBookingInTemp(OBDiv_Code, AgentCode, SourceCountryCode, OBReqoverRide, OBRequestId, OBAgentref, OBColumbusRef, OBRemarks, OBOtherXml, UserLogged, OBRlinenoString)
        Return result

    End Function
    Function GetOtherSummary(ByVal strRequestId As String, Optional ByVal strwhite As String = "0") As DataTable
        Dim dt As New DataTable
        dt = objDALOtherSearch.GetOtherSummary(strRequestId, strwhite)
        Return dt
    End Function
    Sub RemoveOthers(ByVal strRequestId As String, ByVal strelineno As String)
        Dim objDALOtherSearch As New DALOtherSearch
        objDALOtherSearch.RemoveOthers(strRequestId, strelineno)

    End Sub
    Function GetEditBookingDetails(ByVal strRequestId As String, ByVal strolineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALOtherSearch.GetEditBookingDetails(strRequestId, strolineno)
        Return dt
    End Function

    Function GetOtherCancelDetails(ByVal strRequestId As String, ByVal strolineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALOtherSearch.GetOtherCancelDetails(strRequestId, strolineno)
        Return dt
    End Function

    Function SavingCancelOtherInTemp() As Boolean
        Dim res As Boolean
        objDALOtherSearch.OBRequestId = OBRequestId
        objDALOtherSearch.OBOtherCancelXml = OBOtherCancelXml
        objDALOtherSearch.UserLogged = UserLogged
        res = objDALOtherSearch.SavingCancelOtherInTemp()
        Return res
    End Function
End Class
