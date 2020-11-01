Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DALMyAccount
    Dim objclsUtilities As New clsUtilities

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

    Public Property quoteType As String
        Get
            Return _quoteType
        End Get
        Set(ByVal value As String)
            _quoteType = value
        End Set
    End Property

    Function GetBookingSearchDetails() As DataTable
        Try
            Dim objDataTable As DataTable
            If TravelDateType = "Check In or Check Out" Or TravelDateType = "CheckIn Date" Or TravelDateType = "CheckOut Date" Then 'modified by abin on 20181106
                ' If TravelDateType = "Specific date" Then
                If TravelDateFrom <> "" Then
                    Dim strDates As String() = TravelDateFrom.Split("/")
                    TravelDateFrom = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
                If TravelDateTo <> "" Then
                    Dim strDates As String() = TravelDateTo.Split("/")
                    TravelDateTo = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
            End If
            If BookingDateType = "Specific date" Then
                If BookingDateFrom <> "" Then
                    Dim strDates As String() = BookingDateFrom.Split("/")
                    BookingDateFrom = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
                If BookingDateTo <> "" Then
                    Dim strDates As String() = BookingDateTo.Split("/")
                    BookingDateTo = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
            End If



            Dim ProcName As String
            ProcName = "sp_booking_search"
            Dim parm(23) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebUserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@requestid", CType(RequestId, String))
            parm(4) = New SqlParameter("@servicetype", CType(ServiceType, String))
            parm(5) = New SqlParameter("@destinationtype", CType(DestinationType, String))
            parm(6) = New SqlParameter("@destinationcode", CType(DestinationCode, String))
            parm(7) = New SqlParameter("@agentref", CType(AgentRef, String))
            parm(8) = New SqlParameter("@guestfirstname", CType(GuestFirstName, String))
            parm(9) = New SqlParameter("@guestlastname", CType(GuestLastName, String))
            parm(10) = New SqlParameter("@traveldatetype", CType(TravelDateType, String))
            parm(11) = New SqlParameter("@traveldatefrom", CType(TravelDateFrom, String))
            parm(12) = New SqlParameter("@traveldateto", CType(TravelDateTo, String))
            parm(13) = New SqlParameter("@bookingdatetype", CType(BookingDateType, String))
            parm(14) = New SqlParameter("@bookingdatefrom", CType(BookingDateFrom, String))
            parm(15) = New SqlParameter("@bookingdateto", CType(BookingDateTo, String))
            parm(16) = New SqlParameter("@bookingstatus", CType(BookingStatus, String))
            parm(17) = New SqlParameter("@partycode", CType(PartyCode, String))
            parm(18) = New SqlParameter("@hotelconfno", CType(HotelConfNo, String))
            parm(19) = New SqlParameter("@searchagentcode", CType(SearchAgentCode, String))
            parm(20) = New SqlParameter("@sourcectrycode", CType(SourceCountrycode, String))
            parm(21) = New SqlParameter("@usercode", CType(UserCode, String))
            parm(22) = New SqlParameter("@companycode", CType(CompanyCode, String))
            parm(23) = New SqlParameter("@subusercode", CType(SubUserCode, String))
            objDataTable = objclsUtilities.GetDataTable(ProcName, parm)
            Return objDataTable

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function FillBookingToTempForEdit(ByVal strRequestId As String, ByVal strLoggedUser As String, ByVal CopyOREdit As String) As String
        Try

            Dim ProcName As String
            ProcName = "sp_booking_edit"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(4) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))

            parm(1) = New SqlParameter("@userlogged", CType(strLoggedUser, String))
            sqlParamList.Add(parm(1))

            parm(2) = New SqlParameter("@CopyOREdit", CType(CopyOREdit, String))
            sqlParamList.Add(parm(2))

            parm(3) = New SqlParameter("@NEW_requestid", "")
            parm(3).Direction = ParameterDirection.Output
            parm(3).DbType = DbType.String
            parm(3).Size = 20
            sqlParamList.Add(parm(3))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)
            FillBookingToTempForEdit = parm(3).Value
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
        End Try
    End Function

    Sub FillQuoteToTempForEdit(ByVal strRequestId As String, ByVal strLoggedUser As String)
        Try

            Dim ProcName As String
            ProcName = "sp_quote_edit"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@userlogged", CType(strLoggedUser, String))
            sqlParamList.Add(parm(1))
            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.StackTrace.ToString)
        End Try
    End Sub

    Function GetQuoteSearchDetails() As DataTable
        Try
            Dim objDataTable As DataTable

            If TravelDateType = "Check In or Check Out" Or BookingDateType = "Specific date" Or TravelDateType = "CheckIn Date" Or TravelDateType = "CheckOut Date" Then 'modified by abin on 20181106
                If TravelDateFrom <> "" Then
                    Dim strDates As String() = TravelDateFrom.Split("/")
                    TravelDateFrom = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
                If TravelDateTo <> "" Then
                    Dim strDates As String() = TravelDateTo.Split("/")
                    TravelDateTo = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
            End If
            If BookingDateType = "Specific date" Then
                If BookingDateFrom <> "" Then
                    Dim strDates As String() = BookingDateFrom.Split("/")
                    BookingDateFrom = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
                If BookingDateTo <> "" Then
                    Dim strDates As String() = BookingDateTo.Split("/")
                    BookingDateTo = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
                End If
            End If



            Dim ProcName As String
            ProcName = "sp_quote_search"
            Dim parm(24) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebUserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@requestid", CType(RequestId, String))
            parm(4) = New SqlParameter("@servicetype", CType(ServiceType, String))
            parm(5) = New SqlParameter("@destinationtype", CType(DestinationType, String))
            parm(6) = New SqlParameter("@destinationcode", CType(DestinationCode, String))
            parm(7) = New SqlParameter("@agentref", CType(AgentRef, String))
            parm(8) = New SqlParameter("@guestfirstname", CType(GuestFirstName, String))
            parm(9) = New SqlParameter("@guestlastname", CType(GuestLastName, String))
            parm(10) = New SqlParameter("@traveldatetype", CType(TravelDateType, String))
            parm(11) = New SqlParameter("@traveldatefrom", CType(TravelDateFrom, String))
            parm(12) = New SqlParameter("@traveldateto", CType(TravelDateTo, String))
            parm(13) = New SqlParameter("@bookingdatetype", CType(BookingDateType, String))
            parm(14) = New SqlParameter("@bookingdatefrom", CType(BookingDateFrom, String))
            parm(15) = New SqlParameter("@bookingdateto", CType(BookingDateTo, String))
            parm(16) = New SqlParameter("@bookingstatus", CType(BookingStatus, String))
            parm(17) = New SqlParameter("@partycode", CType(PartyCode, String))
            parm(18) = New SqlParameter("@hotelconfno", CType(HotelConfNo, String))
            parm(19) = New SqlParameter("@searchagentcode", CType(SearchAgentCode, String))
            parm(20) = New SqlParameter("@sourcectrycode", CType(SourceCountrycode, String))
            parm(21) = New SqlParameter("@usercode", CType(UserCode, String))
            parm(22) = New SqlParameter("@companycode", CType(CompanyCode, String))
            parm(23) = New SqlParameter("@subusercode", CType(SubUserCode, String))
            parm(24) = New SqlParameter("@quoteType", CType(quoteType, String))
            objDataTable = objclsUtilities.GetDataTable(ProcName, parm)
            Return objDataTable


            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function CheckQuoteRateChange(ByVal strRequestId As String) As String
        Dim strRateChange As String = ""
        Try

            Dim ProcName As String
            ProcName = "sp_validate_booking_quotation_edit"

            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            Dim objDataTable As DataTable
            objDataTable = objclsUtilities.GetDataTable(ProcName, parm)
            If objDataTable.Rows.Count > 0 Then
                strRateChange = objDataTable.Rows(0)("ratechange").ToString
            End If
            Return strRateChange
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return strRateChange
        End Try
    End Function

    Function RevisePriceForEdit(ByVal strRequestid As String) As String
        Try

            Dim ProcName As String
            ProcName = "sp_revise_price_for_edit"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestid, String))
            sqlParamList.Add(parm(0))
            Dim strStatus As String = objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)
            Return strStatus
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function

    Function checkInvoiced(ByVal strRequestid As String) As String
        Dim Invoiced As String = objclsUtilities.ExecuteQueryReturnStringValue("select isnull(invno,'') as invno from booking_header where requestid='" + strRequestid + "'")
        Return Invoiced
    End Function

    Function checkPrivilege(ByVal usercode As String, ByVal appid As String, ByVal privilegeid As String) As Boolean
        Try
            Dim groupid As String = objclsUtilities.ExecuteQueryReturnStringValue("select isnull(groupid,'') from group_privilege_Detail where privilegeid='" + privilegeid + "' and appid=' " + appid + "' and groupid=(SELECT groupid from UserMaster where UserCode='" + usercode + "')")
            If groupid <> "" Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMyAccount:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return 0
        End Try
    End Function

    Function checkSupplierUpdated(ByVal strRequestId As String) As String
        Dim supplierUpdate As String = objclsUtilities.ExecuteQueryReturnStringValue("select distinct isnull(requestid,'') requestid from booking_services_cost(nolock) where requestid ='" + strRequestId + "'")
        Return supplierUpdate
    End Function

End Class
