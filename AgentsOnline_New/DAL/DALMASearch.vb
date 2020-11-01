Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class DALMASearch
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
    Public OBCancelAirXml As String = ""

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
    Function GetMASearchDetails(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal MAArrivalType As String, ByVal MAArrTransferDate As String, ByVal MAArrPickupCode As String, ByVal MADepartueType As String, ByVal MADepTransferDate As String, ByVal MADepDropCode As String, ByVal MATransitType As String, ByVal MATranArrDate As String, ByVal MATranArrPickupCode As String, ByVal MATranDepDate As String, ByVal MATranDepDropCode As String, ByVal Adult As String, ByVal Children As String, ByVal ChildAgeString As String, ByVal SourceCountryCode As String, ByVal OverridePrice As String, ByVal Details As String, ByVal TransferType As String, ByVal AirportMATypeCode As String, ByVal Units As String, ByVal AmendRequestid As String, ByVal AmendLineno As String) As Data.DataSet

        Try
            If MAArrTransferDate <> "" Then
                Dim strDates As String() = MAArrTransferDate.Split("/")
                MAArrTransferDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If MADepTransferDate <> "" Then
                Dim strDates As String() = MADepTransferDate.Split("/")
                MADepTransferDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If MATranArrDate <> "" Then
                Dim strDates As String() = MATranArrDate.Split("/")
                MATranArrDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If MATranDepDate <> "" Then
                Dim strDates As String() = MATranDepDate.Split("/")
                MATranDepDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            Dim ProcName As String
            ProcName = "sp_booking_airportma_search"
            Dim parm(24) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@arrival", CType(IIf(MAArrivalType = "ARRIVAL", 1, 0), Integer))
            parm(4) = New SqlParameter("@arrdate", CType(MAArrTransferDate, String))
            parm(5) = New SqlParameter("@arrairport", CType(MAArrPickupCode, String))

            parm(6) = New SqlParameter("@departure", CType(IIf(MADepartueType = "DEPARTURE", 1, 0), Integer))
            parm(7) = New SqlParameter("@depdate", CType(MADepTransferDate, String))
            parm(8) = New SqlParameter("@depairport", CType(MADepDropCode, String))

            parm(9) = New SqlParameter("@transit", CType(IIf(MATransitType = "TRANSIT", 1, 0), Integer))
            parm(10) = New SqlParameter("@transitarrdate", CType(MATranArrDate, String))
            parm(11) = New SqlParameter("@transitarrpickup", CType(MATranArrPickupCode, String))

            parm(12) = New SqlParameter("@transitdepdate", CType(MATranDepDate, String))
            parm(13) = New SqlParameter("@transitdeppickup", CType(MATranDepDropCode, String))

            parm(14) = New SqlParameter("@noofadults", CType(Adult, Integer))
            parm(15) = New SqlParameter("@noofchild", CType(Children, Integer))
            parm(16) = New SqlParameter("@childagestring", CType(ChildAgeString, String))
            parm(17) = New SqlParameter("@sourcectrycode", CType(SourceCountryCode, String))

            parm(18) = New SqlParameter("@override", CType(OverridePrice, String))

            parm(19) = New SqlParameter("@detail", CType(Details, String))
            parm(20) = New SqlParameter("@transfertype", CType(TransferType, String))
            parm(21) = New SqlParameter("@airportmatypecode", CType(AirportMATypeCode, String))
            parm(22) = New SqlParameter("@units", CType(Units, String))
            parm(23) = New SqlParameter("@requestid", CType(AmendRequestid, String))
            parm(24) = New SqlParameter("@alineno", CType(AmendLineno, String))

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
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
            ProcName1 = "sp_del_booking_airportmatemp"

            Dim sqlParamtrf As New List(Of SqlParameter)
            Dim parmtrf(1) As SqlParameter

            parmtrf(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamtrf.Add(parmtrf(0))
            parmtrf(1) = New SqlParameter("@tlinenostring", CType(OBRlinenoString, String))
            sqlParamtrf.Add(parmtrf(1))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamtrf, mySqlConn, mysqlTrans)


            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_airportmatemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@airportmaxml", CType(OBTransfertXml, String))
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

            objclsUtilities.WriteErrorLog("DALMASearch: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False




    End Function
    Sub RemoveAirportservice(ByVal strRequestId As String, ByVal strtlineno As String)

        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            Dim ProcName As String


            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            ProcName = "sp_del_booking_airportmatemp"
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

            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)

        End Try


    End Sub
    Function GetAiportMeetSummary(ByVal strRequestId As String, strFrontLabel As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            If strFrontLabel = "1" Then
                strQuery = "select requestid,alineno,airportmatype,servicedetail,servicename,units,wlunitprice unitprice,wlcurrcode currcode,ratebasis,adults,child,addlpax,wladultprice adultprice,wlchildprice childprice,wladdlpaxprice addlpaxprice,wladultsalevalue adultsalevalue,wlchildsalevalue childsalevalue,wlunitsalevalue unitsalevalue,wladdlpaxsalevalue addlpaxsalevalue,wltotalsalevalue totalsalevalue,servicedate,complimentarycust,convert(varchar(10),(select ROUND(convert(varchar(15),sum(wltotalsalevalue)),3)   as total from view_booking_airportmatemp where requestid='" & strRequestId & "'))+' ' +wlcurrcode  as total, " _
            & " ROUND(convert(varchar(15),wltotalsalevalue),3)totalsalevalueNew,ROUND(convert(varchar(15),unitprice),3)unitpriceNew, (select isnull(min(isnull(cancelled,0)),0) from booking_airportmate_confcanceltemp  bb(nolock)  where bb.requestid='" & strRequestId & "' and bb.alineno=view_booking_airportmatemp.alineno)  cancelled ,bookingmode" _
            & " from view_booking_airportmatemp where requestid='" & strRequestId & "' order by alineno"
            Else
                strQuery = "select requestid,alineno,airportmatype,servicedetail,servicename,units,unitprice,currcode,ratebasis,adults,child,addlpax,adultprice,childprice,addlpaxprice,adultsalevalue,childsalevalue,unitsalevalue,addlpaxsalevalue,totalsalevalue,servicedate,complimentarycust,convert(varchar(10),(select ROUND(convert(varchar(15),sum(totalsalevalue)),3)   as total  from view_booking_airportmatemp where requestid='" & strRequestId & "'))+' ' +currcode  as total, " _
            & " ROUND(convert(varchar(15),totalsalevalue),3)totalsalevalueNew,ROUND(convert(varchar(15),unitprice),3)unitpriceNew, (select isnull(min(isnull(cancelled,0)),0) from booking_airportmate_confcanceltemp  bb(nolock)  where bb.requestid='" & strRequestId & "' and bb.alineno=view_booking_airportmatemp.alineno)  cancelled ,bookingmode" _
            & " from view_booking_airportmatemp where requestid='" & strRequestId & "' order by alineno"
            End If
        
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetEditBookingDetails(ByVal requestid As String, ByVal olineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_airportma_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@alineno", CType(olineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetAirportCancelDetails(ByVal strRequestId As String, ByVal stralineno As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_get_airportma_canceldetails"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@alineno", CType(stralineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingCancelAirportInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_updateairportcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@aircancelxml", CType(OBCancelAirXml, String))
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

            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

End Class

