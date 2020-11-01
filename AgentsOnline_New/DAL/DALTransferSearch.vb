Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class DALTransferSearch
    Dim objclsUtilities As New clsUtilities

    Public OBDiv_Code As String
    Public OBRequestId As String
    Public OBAgentCode As String
    Public OBSourcectryCode As String
    Public OBReqoverRide As String
    Public OBAgentref As String
    Public OBRemarks As String
    Public OBColumbusRef As String
    Public UserLogged As String
    Public OBRlineNo As Integer
    Public OBRlinenoString As String
    Public OBNoofUnits As Integer
    Public OBAdults As Integer
    Public OBChild As Integer
    Public OBChildAges As String
    Public OBSupagentCode As String
    Public OBPartyCode As String

    Public OBTransferType As String = ""
    Public OBAirportBorderCode As String = ""
    Public OBSectorGroupCode As String = ""
    Public OBCarTypeCode As String = ""
    Public OBShuttle As Integer
    Public OBTransferDate As String = ""
    Public OBFlightCode As String = ""
    Public OBFlightTranID As String = ""
    Public OBFlightTime As String = ""
    Public OBPickup As String = ""
    Public OBDropoff As String = ""
    Public OBUnitPrice As String
    Public OBUnitSaleValue As String
    Public OBWlUnitPrice As String
    Public OBWlUnitSaleValue As String

    Public OBTransfertXml As String = ""
    Public OBCancelTransfertXml As String = ""

    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Function FindBookingEnginRateType(ByVal sAgentCode As String) As Integer
        Try
            Dim iCumulative As Integer = 0
            Dim strQuery As String = "select COUNT(agentcode)cnt from agentmast where agentcode='" & sAgentCode & "' and isnull(bookingengineratetype,'')='CUMULATIVE'"
            iCumulative = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return iCumulative
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return 0
        End Try

    End Function
    Function GetTrfSearchDetails(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal ArrTransferType As String, ByVal ArrTransferDate As String, ByVal ArrPickupCode As String, ByVal ArrDropCode As String, ByVal DepTransferType As String, ByVal DepTransferDate As String, ByVal DepPickupCode As String, ByVal DepDropCode As String, ByVal TrfAdult As String, ByVal TrfChildren As String, ByVal ChildAgeString As String, ByVal TrfSourceCountryCode As String, ByVal OverridePrice As String, ByVal BookType As String, ByVal ShiftingType As String, ByVal ShiftingDate As String, ByVal ShiftingPickupCode As String, ByVal ShiftingDropCode As String, ByVal requestid As String, ByVal tlineno As String) As Data.DataSet

        Try
            If ArrTransferDate <> "" Then
                Dim strDates As String() = ArrTransferDate.Split("/")
                ' Dim dtCheckIn As Date = CType(CheckIn, Date)
                ArrTransferDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If DepTransferDate <> "" Then
                'Dim dtCheckOut As Date = CType(CheckOut, Date)
                'CheckOut = dtCheckOut.ToString("yyyy/MM/dd")
                Dim strDates As String() = DepTransferDate.Split("/")
                DepTransferDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If ShiftingDate <> "" Then
                'Dim dtCheckOut As Date = CType(CheckOut, Date)
                'CheckOut = dtCheckOut.ToString("yyyy/MM/dd")
                Dim strDates As String() = ShiftingDate.Split("/")
                ShiftingDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If


            Dim ProcName As String
            ProcName = "sp_booking_transfers_search"
            Dim parm(26) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@arrival", CType(IIf(ArrTransferType = "ARRIVAL", 1, 0), Integer))
            parm(4) = New SqlParameter("@arrdate", CType(ArrTransferDate, String))
            parm(5) = New SqlParameter("@arrpickup", CType(ArrPickupCode, String))
            parm(6) = New SqlParameter("@arrdropoff", CType(ArrDropCode, String))

            parm(7) = New SqlParameter("@departure", CType(IIf(DepTransferType = "DEPARTURE", 1, 0), Integer))
            parm(8) = New SqlParameter("@depdate", CType(DepTransferDate, String))
            parm(9) = New SqlParameter("@deppickup", CType(DepPickupCode, String))
            parm(10) = New SqlParameter("@depdropoff", CType(DepDropCode, String))

            parm(11) = New SqlParameter("@noofadults", CType(TrfAdult, Integer))
            parm(12) = New SqlParameter("@noofchild", CType(TrfChildren, Integer))
            parm(13) = New SqlParameter("@childagestring", CType(ChildAgeString, String))
            parm(14) = New SqlParameter("@sourcectrycode", CType(TrfSourceCountryCode, String))

            parm(15) = New SqlParameter("@override", CType(OverridePrice, String))
            parm(16) = New SqlParameter("@internal", CType(IIf(ShiftingType = "INTERHOTEL", 1, 0), Integer))
            parm(17) = New SqlParameter("@internaldate", CType(ShiftingDate, String))
            parm(18) = New SqlParameter("@intpickup", CType(ShiftingPickupCode, String))
            parm(19) = New SqlParameter("@intdropoff", CType(ShiftingDropCode, String))

            parm(20) = New SqlParameter("@detail", 0)
            parm(21) = New SqlParameter("@transfertype", "")
            parm(22) = New SqlParameter("@cartypecode", "")
            parm(23) = New SqlParameter("@sectorgroupcode", "")
            parm(24) = New SqlParameter("@units", 1)
            parm(25) = New SqlParameter("@requestid", CType(requestid, String))
            parm(26) = New SqlParameter("@tlineno", CType(tlineno, String))

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function savingbookingintemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            
            Dim ProcName As String
            ProcName = "sp_update_booking_headertemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(10) As SqlParameter

            parm(0) = New SqlParameter("@div_code", CType(OBDiv_Code, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@agentcode", CType(OBAgentCode, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@sourcectrycode", CType(OBSourcectryCode, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@reqoverride", CType(OBReqoverRide, Integer))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@agentref", CType(OBAgentref, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@columbusref", CType(OBColumbusRef, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@remarks", CType(OBRemarks, String))
            sqlParamList.Add(parm(7))
            parm(8) = New SqlParameter("@SubUserCode", CType(SubUserCode, String))
            sqlParamList.Add(parm(8))
            parm(9) = New SqlParameter("@userlogged", CType(UserLogged, String))
            sqlParamList.Add(parm(9))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)


            Dim ProcName1 As String
            ProcName1 = "sp_del_booking_transferstemp"

            Dim sqlParamtrf As New List(Of SqlParameter)
            Dim parmtrf(2) As SqlParameter

            parmtrf(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamtrf.Add(parmtrf(0))
            parmtrf(1) = New SqlParameter("@tlinenostring", CType(OBRlinenoString, String))
            sqlParamtrf.Add(parmtrf(1))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamtrf, mySqlConn, mysqlTrans)


            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_transferstemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@transferxml", CType(OBTransfertXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(UserLogged, String))
            sqlParamListguest.Add(parmguest(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALTransferSearch: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False




    End Function

    Function GetTransferSummary(strRequestId As String, strWhiteLabel As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            If strWhiteLabel = "1" Then
                strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(case when isnull(tc.cancelled,0)=1 then 0 else t.wlunitsalevalue end)),3)   as total from view_booking_transferstemp t (nolock) left join booking_transfers_confcanceltemp tc (nolock) on tc.requestid=t.requestid and tc.tlineno=t.tlineno where  " _
             & " t.requestid='" & strRequestId & "'))+' ' +wlcurrcode  as total,ROUND(convert(varchar(15),wlunitsalevalue),3)totalsalevalueNew,ROUND(convert(varchar(15),wlunitprice),3)unitpriceNew ,  " _
             & "  (select isnull(min(isnull(cancelled,0)),0) from booking_transfers_confcanceltemp  bb(nolock) where bb.requestid='" & strRequestId & "' and bb.tlineno=view_booking_transferstemp.tlineno)  cancelled from view_booking_transferstemp where requestid='" & strRequestId & "' order by tlineno"
            Else
                strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(case when isnull(tc.cancelled,0)=1 then 0 else unitsalevalue end)),3)   as total from view_booking_transferstemp t (nolock) left join booking_transfers_confcanceltemp tc (nolock) on tc.requestid=t.requestid and tc.tlineno=t.tlineno where  " _
             & " t.requestid='" & strRequestId & "'))+' ' +currcode  as total,ROUND(convert(varchar(15),unitsalevalue),3)totalsalevalueNew,ROUND(convert(varchar(15),unitprice),3)unitpriceNew ,  " _
             & "  (select isnull(min(isnull(cancelled,0)),0) from booking_transfers_confcanceltemp  bb(nolock) where bb.requestid='" & strRequestId & "' and bb.tlineno=view_booking_transferstemp.tlineno)  cancelled from view_booking_transferstemp where requestid='" & strRequestId & "' order by tlineno"
            End If
         
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function LaodTrfFlightDetails(strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select *,(select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=booking_guest_flightstemp.arrairportbordercode)arrairportborderName,(select distinct airportbordername from  airportbordersmaster where airportbordersmaster.airportbordercode=booking_guest_flightstemp.depairportbordercode)depairportborderName from booking_guest_flightstemp where requestid='" & strRequestId & "'  order by guestlineno "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function FillTransferDetails(ByVal requestid As String) As DataSet
        Try
            Dim ProcName As String
            ProcName = "sp_autofill_airporttransfers"
            Dim parm(0) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(requestid, String))


            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function FillTransferDetailsFromMA(ByVal requestid As String) As DataSet
        Try
            Dim ProcName As String
            ProcName = "sp_autofill_airporttserviceransfers"
            Dim parm(0) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(requestid, String))


            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetAirportTerminal(ByVal strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = " select airportbordercode  from booking_transferstemp where transfertype in ('ARRIVAL','DEPARTURE') and requestid='" & strRequestId & "' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Sub RemoveTransfers(ByVal strRequestId As String, ByVal strtlineno As String)

        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            Dim ProcName As String


            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            ProcName = "sp_del_booking_transferstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@tlinenostring", CType(strtlineno, String))
            sqlParamList.Add(parm(1))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)

        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)

        End Try


    End Sub
    Function GetEditBookingDetails(ByVal requestid As String, ByVal olineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_transfer_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@tlineno", CType(olineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetTransferCancelDetails(ByVal strRequestId As String, ByVal strtlineno As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_get_transfer_canceldetails"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@tlineno", CType(strtlineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function SavingCancelTransferDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_updatetransferscanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@trfcancelxml", CType(OBCancelTransfertXml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(UserLogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALTransferSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function
End Class
