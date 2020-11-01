Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DALGuest

    Public GBrequestid As String = ""
    Public GBGuestLineNo As String = ""
    Public GBRlineno As String = ""
    Public GBRoomno As String = ""
    Public GBTitle As String = ""
    Public GBFirstName As String = ""
    Public GBMiddleName As String = ""
    Public GBLastName As String = ""
    Public GBNationalityCode As String = ""
    Public GBChildAge As String = ""
    Public GBVisaOptions As String = ""
    Public GBVisaTypeCode As String = ""
    Public GBVisaPrice As String = ""
    Public GBPassportNo As String = ""
    Public GBUpdatedDate As String = ""
    Public GBUpdatedUser As String = ""
    Public GBGuestXml As String = ""
    Public GBServiceXml As String = ""
    Public GBFlightXml As String = ""
    Public GBDepFlightXml As String = ""
    Public GBuserlogged As String = ""

    Public GBconfirmxml As String = ""
    Public GBCancelxml As String = ""
    Public GBRemarksTemplate As String = ""
    Public GBHotelRemarks As String = ""
    Public GBAgentRemarks As String = ""
    Public GBArrivalRemarks As String = ""
    Public GBdivcode As String = ""
    Public GBAgentcode As String = ""
    Public GBAgentsourcectrycode As String = ""
    Public GBLogintype As String = ""

    Public GBDepartureRemarks As String = ""
    Public GBOthPartyRemarks As String = ""
    Public GBOthAgentRemarks As String = ""
    Public GBOthArrivalRemarks As String = ""
    Public GBOthDepartureRemarks As String = ""
    Public GBToursRemarks As String = ""
    Public GBToursPartyRemarks As String = ""
    Public GBToursAgentRemarks As String = ""
    Public GBToursArrivalRemarks As String = ""
    Public GBToursDepartureRemarks As String = ""
    Public GBAirPartyRemarks As String = ""
    Public GBAirAgentRemarks As String = ""
    Public GBAirArrivalRemarks As String = ""
    Public GBAirDepartureRemarks As String = ""
    Public GBTrfsPartyRemarks As String = ""
    Public GBTrfsAgentRemarks As String = ""
    Public GBTrfsArrivalRemarks As String = ""
    Public GBTrfsDepartureRemarks As String = ""
    Public GBVisaAgentRemarks As String = ""
    Public GBVisaArrivalRemarks As String = ""

    Public GBAgentrefno As String = ""
    Public GBColumbusref As String = ""
    Public GBBookmode As String = ""
    Public GBCancelEntireBooking As String = "0"
    Public CBratePlanSource As String = ""
    Public CBconnectMarkupXml As String = ""
    Public GBErrorStatus As String = ""
    Public GBErrorStatusDescription As String = ""
    Public GBBookingStatus As String = ""
    Public GBPayBookingRef As String = ""

    Dim objclsUtilities As New clsUtilities

    Dim _strPackageSummary As String = ""
    Dim _strPackageValueSummary As String
    Dim _strCumulative As String

    Public Property PackageSummary As String
        Get
            Return _strPackageSummary
        End Get
        Set(ByVal value As String)
            _strPackageSummary = value
        End Set
    End Property
    Public Property PackageValueSummary As String
        Get
            Return _strPackageValueSummary
        End Get
        Set(ByVal value As String)
            _strPackageValueSummary = value
        End Set
    End Property
    Public Property Cumulative As String
        Get
            Return _strCumulative
        End Get
        Set(ByVal value As String)
            _strCumulative = value
        End Set
    End Property


    'Function GetConfirmSummary(ByVal requestid As String, ByVal rlineno As String) As DataTable

    '    Dim mySqlConn As New SqlConnection
    '    Dim mysqlTrans As SqlTransaction
    '    Dim dt As New DataTable
    '    Try
    '        Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
    '        mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
    '        'connection open
    '        Dim ProcName As String
    '        ProcName = "sp_get_hotel_confirmdetails"
    '        Dim sqlParamList As New List(Of SqlParameter)
    '        Dim parm(1) As SqlParameter

    '        parm(0) = New SqlParameter("@requestid", CType(requestid, String))

    '        parm(1) = New SqlParameter("@rlineno", CType(rlineno, Integer))


    '        dt = objclsUtilities.GetDataTable(ProcName, parm)

    '        'SQl Tarn Commit
    '        clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
    '        'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
    '        clsDBConnect.dbConnectionClose(mySqlConn)
    '        Return dt
    '    Catch ex As Exception

    '        mysqlTrans.Rollback()

    '        objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)

    '    End Try


    'End Function


    Function GetConfirmSummary(ByVal requestid As String, ByVal rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_hotel_confirmdetails"
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@rlineno", CType(rlineno, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetTransferConfirmSummary(ByVal requestid As String, ByVal rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_transfer_confirmdetails"
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@tlineno", CType(rlineno, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function SavingGuestRemarksInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_hotel_detail_remarkstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(7) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))

            parm(1) = New SqlParameter("@rlineno", CType(GBGuestLineNo, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@remarkstemplate", CType(GBRemarksTemplate, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@hotelremarks", CType(GBHotelRemarks, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@agentremarks ", CType(GBAgentRemarks, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@arrivalremarks", CType(GBArrivalRemarks, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@departureremarks", CType(GBDepartureRemarks, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(7))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function

    Function getdefaulttimeforconfirm(ByVal sqlquery As String) As String
        Dim defaulttime As String
        defaulttime = objclsUtilities.ExecuteQueryReturnSingleValue(sqlquery)
        Return defaulttime
    End Function
    Function SavingOtherServRemarksInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_others_remarkstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(6) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@rlineno", CType(GBGuestLineNo, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@partyremarks", CType(GBOthPartyRemarks, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@agentremarks ", CType(GBOthAgentRemarks, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@arrivalremarks", CType(GBOthArrivalRemarks, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@departureremarks", CType(GBOthDepartureRemarks, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(6))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function
    Function SavingToursRemarksInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_tours_remarkstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(6) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@rlineno", CType(GBGuestLineNo, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@partyremarks", CType(GBToursPartyRemarks, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@agentremarks ", CType(GBToursAgentRemarks, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@arrivalremarks", CType(GBToursArrivalRemarks, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@departureremarks", CType(GBToursDepartureRemarks, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(6))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function

    Function SavingVisaRemarksInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_Visa_remarkstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(4) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@vlineno", CType(GBGuestLineNo, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@agentremarks ", CType(GBVisaAgentRemarks, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@arrivalremarks", CType(GBVisaArrivalRemarks, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(4))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function

    Function SavingtransfersRemarksInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_transfers_remarkstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(6) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@tlineno", CType(GBGuestLineNo, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@partyremarks", CType(GBTrfsPartyRemarks, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@agentremarks ", CType(GBTrfsAgentRemarks, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@arrivalremarks", CType(GBTrfsArrivalRemarks, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@departureremarks", CType(GBTrfsDepartureRemarks, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(6))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function
    Function SavingAirportmaRemarksInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_airportma_remarkstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(6) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@alineno", CType(GBGuestLineNo, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@partyremarks", CType(GBAirPartyRemarks, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@agentremarks ", CType(GBAirAgentRemarks, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@arrivalremarks", CType(GBAirArrivalRemarks, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@departureremarks", CType(GBAirDepartureRemarks, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(6))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function
    Function ValidateBooking(ByVal requestid As String, ByVal agentcode As String, ByVal sourcectrycode As String, ByVal logintype As String, Optional ByVal submitquote As Integer = 0, Optional ByVal asGuestName As String = "") As DataSet
        'changed by mohamed on 11/09/2018
        Try
            'changed by mohamed on 03/09/2018
            Dim dtSet As DataSet, lGShitDetXML As String
            dtSet = objclsUtilities.GetDataFromDataset("execute sp_get_shifting_hotel_detail_guestpage '" & requestid & "',-1")
            lGShitDetXML = objclsUtilities.GenerateXML(dtSet)

            Dim ProcName As String
            ProcName = "sp_validate_bookingtemp"
            Dim parm(7) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(logintype, String))
            parm(1) = New SqlParameter("@webusername", CType(GBuserlogged, String))
            parm(2) = New SqlParameter("@agentcode", CType(agentcode, String))
            parm(3) = New SqlParameter("@sourcectrycode", CType(sourcectrycode, String))
            parm(4) = New SqlParameter("@requestid", CType(requestid, String))
            parm(5) = New SqlParameter("@submitforquote", CType(submitquote, String))
            parm(6) = New SqlParameter("@guestnames", CType(asGuestName, String))
            parm(6).DbType = DbType.Xml
            parm(7) = New SqlParameter("@guestdetails", CType(lGShitDetXML, String))   '' Added shahul 22/11/18
            parm(7).DbType = DbType.Xml

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)

            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function FinalQuoteSaveBooking(ByVal requestid As String, ByVal divcode As String, ByVal strAgentRef As String) As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Dim strrequestid As String = ""

        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            Dim ProcName2 As String
            ProcName2 = "sp_update_booking_Agentrefnotemp"

            Dim sqlParamList2 As New List(Of SqlParameter)
            Dim parm2(3) As SqlParameter

            parm2(0) = New SqlParameter("@div_code", CType(divcode, String))
            sqlParamList2.Add(parm2(0))
            parm2(1) = New SqlParameter("@requestid", CType(requestid, String))
            sqlParamList2.Add(parm2(1))
            parm2(2) = New SqlParameter("@agentref", CType(strAgentRef, String))
            sqlParamList2.Add(parm2(2))
            parm2(3) = New SqlParameter("@columbusref", "")
            sqlParamList2.Add(parm2(3))


            objclsUtilities.ExecuteNonQuerynew(constring, ProcName2, sqlParamList2, mySqlConn, mysqlTrans)

            If GBBookmode <> "EDIT" Then


                Dim ProcName1 As String
                ProcName1 = "sp_getnumber_div"

                Dim sqlParamList1 As New List(Of SqlParameter)
                Dim parm1(2) As SqlParameter


                parm1(0) = New SqlParameter("@optionname", CType("QUOTE", String))
                sqlParamList1.Add(parm1(0))
                parm1(1) = New SqlParameter("@divid", CType(divcode, String))
                sqlParamList1.Add(parm1(1))
                parm1(2) = New SqlParameter("@newno", "")
                parm1(2).Direction = ParameterDirection.Output
                parm1(2).DbType = DbType.String
                parm1(2).Size = 50
                sqlParamList1.Add(parm1(2))
                objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamList1, mySqlConn, mysqlTrans)
                strrequestid = parm1(2).Value
            Else

                '  strrequestid = CType(GBrequestid, String)
                Dim ProcName1 As String
                ProcName1 = "sp_getsubquoteid"

                Dim sqlParamList1 As New List(Of SqlParameter)
                Dim parm1(1) As SqlParameter

                Dim strGBrequestids() As String = GBrequestid.Split("/")
                GBrequestid = strGBrequestids(0) & "/" & strGBrequestids(1)
                parm1(0) = New SqlParameter("@RequestId", CType(GBrequestid, String))
                sqlParamList1.Add(parm1(0))
                parm1(1) = New SqlParameter("@subquoteid", "")
                parm1(1).Direction = ParameterDirection.Output
                parm1(1).DbType = DbType.String
                parm1(1).Size = 50
                sqlParamList1.Add(parm1(1))
                objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamList1, mySqlConn, mysqlTrans)
                strrequestid = parm1(1).Value

        
            End If





            Dim ProcName As String
            ProcName = "sp_finalquotesave_booking"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(3) As SqlParameter
            parm(0) = New SqlParameter("@temprequestid", CType(requestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(strrequestid, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            If Cumulative = 1 Then
                If PackageSummary <> Nothing Or PackageValueSummary <> Nothing Then
                    Dim ProcNamePackage As String
                    ProcNamePackage = "sp_save_quote_profit_summary"
                    Dim sqlParamListPackage As New List(Of SqlParameter)
                    Dim parmPackage(3) As SqlParameter
                    parmPackage(0) = New SqlParameter("@Requestid", CType(strrequestid, String))
                    sqlParamListPackage.Add(parmPackage(0))
                    parmPackage(1) = New SqlParameter("@PackageSummary", PackageSummary)
                    sqlParamListPackage.Add(parmPackage(1))
                    parmPackage(2) = New SqlParameter("@PackageValueSummary", PackageValueSummary)
                    sqlParamListPackage.Add(parmPackage(2))
                    objclsUtilities.ExecuteNonQuerynew(constring, ProcNamePackage, sqlParamListPackage, mySqlConn, mysqlTrans)
                End If

            End If


            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return strrequestid

        Catch ex As Exception

            mysqlTrans.Rollback()
            strrequestid = ""
            objclsUtilities.WriteErrorLog("DALQuoteGUEST: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return strrequestid
        End Try
        Return False

    End Function
    Function SaveBookingProfitInTemp() As String

        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction

        Try

            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            If Cumulative = 1 Then
                Dim ProcNamePackage As String
                ProcNamePackage = "sp_save_booking_profit_summarytemp"
                Dim sqlParamListPackage As New List(Of SqlParameter)
                Dim parmPackage(3) As SqlParameter
                parmPackage(0) = New SqlParameter("@Requestid", CType(GBrequestid, String))
                sqlParamListPackage.Add(parmPackage(0))
                parmPackage(1) = New SqlParameter("@PackageSummary", PackageSummary)
                sqlParamListPackage.Add(parmPackage(1))
                parmPackage(2) = New SqlParameter("@PackageValueSummary", PackageValueSummary)
                sqlParamListPackage.Add(parmPackage(2))
                objclsUtilities.ExecuteNonQuerynew(constring, ProcNamePackage, sqlParamListPackage, mySqlConn, mysqlTrans)
                mysqlTrans.Commit()    'SQl Tarn Commit
                clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
                'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
                clsDBConnect.dbConnectionClose(mySqlConn)
            End If
        Catch ex As Exception

            mysqlTrans.Rollback()
            objclsUtilities.WriteErrorLog("DALGUEST:SaveBookingProfitInTemp: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
        Return False
    End Function

    Function FinalSaveBooking() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Dim strrequestid As String = ""

        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            Dim ProcName2 As String
            ProcName2 = "sp_update_booking_Agentrefnotemp"

            Dim sqlParamList2 As New List(Of SqlParameter)
            Dim parm2(3) As SqlParameter

            parm2(0) = New SqlParameter("@div_code", CType(GBdivcode, String))
            sqlParamList2.Add(parm2(0))
            parm2(1) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList2.Add(parm2(1))
            parm2(2) = New SqlParameter("@agentref", CType(GBAgentrefno, String))
            sqlParamList2.Add(parm2(2))
            parm2(3) = New SqlParameter("@columbusref", CType(GBColumbusref, String))
            sqlParamList2.Add(parm2(3))


            objclsUtilities.ExecuteNonQuerynew(constring, ProcName2, sqlParamList2, mySqlConn, mysqlTrans)

            If GBBookmode <> "EDIT" Then

                If GBColumbusref.Trim <> "" Then
                    strrequestid = GBColumbusref
                ElseIf GBPayBookingRef.Trim <> "" Then
                    strrequestid = GBPayBookingRef
                Else
                    Dim ProcName1 As String
                    ProcName1 = "sp_getnumber_div"

                    Dim sqlParamList1 As New List(Of SqlParameter)
                    Dim parm1(2) As SqlParameter


                    parm1(0) = New SqlParameter("@optionname", CType("RESSNO", String))
                    sqlParamList1.Add(parm1(0))
                    parm1(1) = New SqlParameter("@divid", CType(GBdivcode, String))
                    sqlParamList1.Add(parm1(1))
                    parm1(2) = New SqlParameter("@newno", "")
                    parm1(2).Direction = ParameterDirection.Output
                    parm1(2).DbType = DbType.String
                    parm1(2).Size = 50
                    sqlParamList1.Add(parm1(2))
                    objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamList1, mySqlConn, mysqlTrans)
                    strrequestid = parm1(2).Value
                End If
            Else
                strrequestid = CType(GBrequestid, String)
            End If

            Dim ProcName As String
            ProcName = "sp_finalsave_booking"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(3) As SqlParameter
            parm(0) = New SqlParameter("@temprequestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(strrequestid, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return strrequestid

        Catch ex As Exception

            mysqlTrans.Rollback()
            strrequestid = ""
            objclsUtilities.WriteErrorLog("DALGUEST: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return strrequestid
        End Try
        Return False

    End Function

    Function SavingConfirmBookingDetailsInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_hotel_detail_confirmtemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@confirmxml", CType(GBconfirmxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False


    End Function
    Function GetConfirmDetFromDataTable(ByVal strSqlQuery As String) As DataTable
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function
    Function GetRemarksDetFromDataTable(ByVal strSqlQuery As String) As DataTable
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function

    Function SavingDepartureFlightTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            'Dim ProcName As String
            'ProcName = "sp_del_booking_guesttemp"

            'Dim sqlParamList As New List(Of SqlParameter)
            'Dim parm(2) As SqlParameter

            'parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            'sqlParamList.Add(parm(0))
            'parm(1) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            'sqlParamList.Add(parm(1))
            'objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_guest_departureflightstemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@depflightsxml", CType(GBDepFlightXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamListguest.Add(parmguest(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)

            'Dim ProcNameservice As String
            'ProcNameservice = "sp_add_booking_guestservicetemp"
            'Dim sqlParamListservice As New List(Of SqlParameter)
            'Dim parmservice(3) As SqlParameter
            'parmservice(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            'sqlParamListservice.Add(parmservice(0))
            'parmservice(1) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            'sqlParamListservice.Add(parmservice(1))
            'parmservice(2) = New SqlParameter("@servicexml", CType(GBServiceXml, String))
            'sqlParamListservice.Add(parmservice(2))

            'objclsUtilities.ExecuteNonQuerynew(constring, ProcNameservice, sqlParamListservice, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function
    Function SavingArrivalFlightTemp(Optional ByVal lbNeedToDeleteFlightTemp As Boolean = False) As String 'changed by mohamed on 24/09/2018
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            'Dim ProcName As String
            'ProcName = "sp_del_booking_guesttemp"

            'Dim sqlParamList As New List(Of SqlParameter)
            'Dim parm(2) As SqlParameter

            'parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            'sqlParamList.Add(parm(0))
            'parm(1) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            'sqlParamList.Add(parm(1))
            'objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_guest_arrivalflightstemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(4) As SqlParameter 'changed by mohamed on 24/09/2018
            parmguest(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@flightsxml", CType(GBFlightXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamListguest.Add(parmguest(2))
            'changed by mohamed on 24/09/2018
            parmguest(3) = New SqlParameter("@deleteExistingEntries", CType(IIf(lbNeedToDeleteFlightTemp = True, 1, 0), String))
            sqlParamListguest.Add(parmguest(3))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)

            'Dim ProcNameservice As String
            'ProcNameservice = "sp_add_booking_guestservicetemp"
            'Dim sqlParamListservice As New List(Of SqlParameter)
            'Dim parmservice(3) As SqlParameter
            'parmservice(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            'sqlParamListservice.Add(parmservice(0))
            'parmservice(1) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            'sqlParamListservice.Add(parmservice(1))
            'parmservice(2) = New SqlParameter("@servicexml", CType(GBServiceXml, String))
            'sqlParamListservice.Add(parmservice(2))

            'objclsUtilities.ExecuteNonQuerynew(constring, ProcNameservice, sqlParamListservice, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function
    Function SavingGuestBookingInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_del_booking_guesttemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            sqlParamList.Add(parm(1))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_guesttemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@guestxml", CType(GBGuestXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamListguest.Add(parmguest(2))
            'parmguest(3) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            'sqlParamListguest.Add(parmguest(3))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)

            Dim ProcNameservice As String
            ProcNameservice = "sp_add_booking_guestservicetemp"
            Dim sqlParamListservice As New List(Of SqlParameter)
            Dim parmservice(2) As SqlParameter
            parmservice(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListservice.Add(parmservice(0))
            'parmservice(1) = New SqlParameter("@rlineno", CType(GBRlineno, String))
            'sqlParamListservice.Add(parmservice(1))
            parmservice(1) = New SqlParameter("@servicexml", CType(GBServiceXml, String))
            sqlParamListservice.Add(parmservice(1))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameservice, sqlParamListservice, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            clsDBConnect.dbConnectionClose(mySqlConn)
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function SavingGuestFlightBookingInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_del_booking_guesttemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_guesttemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@guestxml", CType(GBGuestXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamListguest.Add(parmguest(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)

            Dim ProcNameflight As String
            ProcNameflight = "sp_del_booking_guest_flightstemp"
            Dim sqlParamListflight As New List(Of SqlParameter)
            Dim parmflight(1) As SqlParameter
            parmflight(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListflight.Add(parmflight(0))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameflight, sqlParamListflight, mySqlConn, mysqlTrans)

            Dim ProcNameFlite As String
            ProcNameFlite = "sp_add_booking_guest_flightstemp"
            Dim sqlParamListflite As New List(Of SqlParameter)
            Dim parmflite(3) As SqlParameter
            parmflite(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamListflite.Add(parmflite(0))
            parmflite(1) = New SqlParameter("@flightsxml", CType(GBFlightXml, String))
            sqlParamListflite.Add(parmflite(1))
            parmflite(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamListflite.Add(parmflite(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameFlite, sqlParamListflite, mySqlConn, mysqlTrans)


            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function Fillguests(ByVal strRequestId As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_guestfill"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function Fillguests_new(ByVal strRequestId As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_guestfill_newone"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function HotelEmailRoomcheck(ByVal strRequestId As String, ByVal partycode As String, ByVal amended As String, ByVal cancelled As String, ByVal rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_hotelemail_roomcheck"
            Dim parm(4) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@partycode", CType(partycode, String))
            parm(2) = New SqlParameter("@amended", CType(amended, Integer))
            parm(3) = New SqlParameter("@cancelled", CType(cancelled, Integer))
            parm(4) = New SqlParameter("@rlineno", CType(rlineno, Integer))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingCancelBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_UpdateBookingHotelDetailCancelTemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(3) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@cancelxml", CType(GBCancelxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@cancelallbooking", CType(GBCancelEntireBooking, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(3))
            
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function SavingCancelConnectivityBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_Update_connectivityBookingHotelDetailCancelTemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(5) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@rlineno", CType(GBRlineno, Integer))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@cancelxml", CType(GBCancelxml, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@rateplanSource", CType(CBratePlanSource, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@connectMarkupXml", CType(CBconnectMarkupXml, String))
            sqlParamList.Add(parm(5))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function SavingTransferConfirmBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_transfers_confcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@confirmxml", CType(GBconfirmxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetTransferConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Dim strSqlQuery As String = "select tlineno,transferconfno,confirmby,convert(varchar(10),isnull(confirmdate,getdate()),103) as confirmdate,prevconfno,convert(varchar(10),isnull(timelimit,getdate()),103) as timelimitdate,convert(varchar(5), isnull(timelimit,getdate()), 108) as timelimittime from booking_transfers_confcanceltemp(nolock)  where requestid='" & strRequestId & "' and tlineno='" & strRLineNo & "'"
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function

    Function GetToursConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_tours_confirmdetails"
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@elineno", CType(rlineno, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetToursConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Dim strSqlQuery As String = "select elineno,toursconfno,confirmby,convert(varchar(10),isnull(confirmdate,getdate()),103) as confirmdate,prevconfno,convert(varchar(10),isnull(timelimit,getdate()),103) as timelimitdate,convert(varchar(5), isnull(timelimit,getdate()), 108) as timelimittime from booking_tours_confcanceltemp(nolock)  where requestid='" & strRequestId & "' and elineno='" & strRLineNo & "'"
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function

    Function SavingToursConfirmBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_tours_confcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@confirmxml", CType(GBconfirmxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetAirportMateConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_airportmate_confirmdetails"
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@alineno", CType(rlineno, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetAirportMateConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Dim strSqlQuery As String = "select alineno,isnull(airportmateconfno,'') airportmateconfno,isnull(confirmby,'') confirmby,convert(varchar(10),isnull(confirmdate,getdate()),103) as confirmdate,prevconfno,convert(varchar(10),isnull(timelimit,getdate()),103) as timelimitdate,convert(varchar(5), isnull(timelimit,getdate()), 108) as timelimittime from booking_airportmate_confcanceltemp(nolock)  where requestid='" & strRequestId & "' and alineno='" & strRLineNo & "'"
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function
    Function SavingAirportMateConfirmBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_airportmate_confcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@confirmxml", CType(GBconfirmxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetOthersConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_others_confirmdetails"
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@olineno", CType(rlineno, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetOthersConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Dim strSqlQuery As String = "select olineno,Othersconfno,confirmby,convert(varchar(10),isnull(confirmdate,getdate()),103) as confirmdate,prevconfno,convert(varchar(10),isnull(timelimit,getdate()),103) as timelimitdate,convert(varchar(5), isnull(timelimit,getdate()), 108) as timelimittime from booking_others_confcanceltemp(nolock)  where requestid='" & strRequestId & "' and olineno='" & strRLineNo & "'"
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function
    Function SavingOthersConfirmBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_others_confcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@confirmxml", CType(GBconfirmxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetVisaConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_visa_confirmdetails"
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@vlineno", CType(rlineno, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetVisaConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Dim strSqlQuery As String = "select vlineno,visaconfno,confirmby,convert(varchar(10),isnull(confirmdate,getdate()),103) as confirmdate,prevconfno,convert(varchar(10),isnull(timelimit,getdate()),103) as timelimitdate,convert(varchar(5), isnull(timelimit,getdate()), 108) as timelimittime from booking_visa_confcanceltemp(nolock)  where requestid='" & strRequestId & "' and vlineno='" & strRLineNo & "'"
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataFromDataTable(strSqlQuery)
        Return dt
    End Function
    Function SavingVisaConfirmBookingDetailsInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_visa_confcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(GBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@confirmxml", CType(GBconfirmxml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GeneratePackageValue(ByVal strRequestId As String, ByVal strAvoidDiscount As String, ByVal strlogintype As String) As DataSet
        Try
            Dim ProcName As String
            ProcName = "sp_booking_profittemp"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@avoiddiscount", CType(strAvoidDiscount, String))
            parm(2) = New SqlParameter("@userlogged", CType(strlogintype, String))
            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function CheckSelectedAgentIsCumulative(requestid As String) As Integer
        Dim iCumulative As Integer = 0
        Dim strQuery As String = "select count(agentcode)CNT from agentmast(nolock) where bookingengineratetype='CUMULATIVE' and agentcode=(select min(agentcode) from  booking_headertemp(nolock) where requestid='" & requestid & "')"
        iCumulative = objclsUtilities.ExecuteQueryReturnSingleValue(strQuery)
        Return iCumulative
    End Function

    Function IsExistGuestFlights(strRequestId As String) As Boolean
        Try
            Dim ProcName As String
            ProcName = "IsExistGuestFlight"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Dim bStatus As Boolean = True
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("fl_cnt").ToString = 0 Then
                    bStatus = False
                End If

            End If
            Return bStatus
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetAgentRef(strRequestId As String) As String
        Dim strAgentRef As String
        Dim strQuery As String = "select agentref from booking_headertemp(nolock) where requestid='" & strRequestId & "'"
        strAgentRef = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
        Return strAgentRef
    End Function

    Function PrearrangedValidate(strRequestId As Object) As Integer
        Try
            Dim ProcName As String
            ProcName = "sp_prearranged_validate"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Dim iStatus As Integer
            If dt.Rows.Count > 0 Then
                iStatus = dt.Rows(0)("sts").ToString
            End If
            Return iStatus

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function ValidateGuestService(strRequestId As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booked_servicestemp"
            Dim parm(0) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function UpdateConnectivityBookingFailedConfirmationTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_hotelDetail_confirmFailTemp"
            Dim sqlparam As New List(Of SqlParameter)
            sqlparam.Add(New SqlParameter("@requestid", GBrequestid))
            sqlparam.Add(New SqlParameter("@rlineno", GBRlineno))
            sqlparam.Add(New SqlParameter("@confirmStatus", GBErrorStatus))
            sqlparam.Add(New SqlParameter("@confirmErrorDescription", GBErrorStatusDescription))
            sqlparam.Add(New SqlParameter("@userlogged", GBuserlogged))
            sqlparam.Add(New SqlParameter("@bookingStatus", GBBookingStatus))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlparam, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function UpdateConnectivityBookingFailedCancellationTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_update_booking_hotelDetail_cancelFailTemp"
            Dim sqlparam As New List(Of SqlParameter)
            sqlparam.Add(New SqlParameter("@requestid", GBrequestid))
            sqlparam.Add(New SqlParameter("@rlineno", GBRlineno))
            sqlparam.Add(New SqlParameter("@cancelStatus", GBErrorStatus))
            sqlparam.Add(New SqlParameter("@cancelErrorDescription", GBErrorStatusDescription))
            sqlparam.Add(New SqlParameter("@userlogged", GBuserlogged))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlparam, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALGUEST:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GeneratePaymentRequestId(ByVal strRequestId As String, ByVal requestAmount As Decimal, ByVal userlogged As String, ByVal bookingNo As String, ByVal payType As String, ByVal paymentMode As String, ByVal RefundPaymentRequestId As String) As String
        Dim SqlConn As New SqlConnection
        Try
            Dim paymentRequestId As String = ""
            SqlConn = clsDBConnect.dbConnection()         'connection open
            Dim myCommand As SqlCommand = New SqlCommand("sp_generate_PaymentRequestId", SqlConn)
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Parameters.Add(New SqlParameter("@tempRequestId", SqlDbType.VarChar, 20)).Value = CType(strRequestId, String)
            myCommand.Parameters.Add(New SqlParameter("@requestAmount", SqlDbType.Decimal)).Value = requestAmount
            myCommand.Parameters.Add(New SqlParameter("@userlogged", SqlDbType.VarChar, 50)).Value = userlogged
            myCommand.Parameters.Add(New SqlParameter("@PayType", SqlDbType.VarChar, 20)).Value = payType
            myCommand.Parameters.Add(New SqlParameter("@PaymentMode", SqlDbType.VarChar, 20)).Value = paymentMode
            myCommand.Parameters.Add(New SqlParameter("@requestId", SqlDbType.VarChar, 20)).Value = bookingNo
            myCommand.Parameters.Add(New SqlParameter("@RefundPaymentRequestId", SqlDbType.VarChar, 50)).Value = RefundPaymentRequestId

            Dim param As SqlParameter = New SqlParameter()
            param.ParameterName = "@paymentRequestId"
            param.Direction = ParameterDirection.Output
            param.DbType = SqlDbType.VarChar
            param.Size = 50
            myCommand.Parameters.Add(param)
            myCommand.ExecuteNonQuery()
            paymentRequestId = param.Value.ToString().Trim()
            clsDBConnect.dbCommandClose(myCommand)               'sql command disposed
            clsDBConnect.dbConnectionClose(SqlConn)
            Return paymentRequestId
        Catch ex As Exception
            If Not SqlConn Is Nothing Then
                If SqlConn.State = ConnectionState.Open Then
                    clsDBConnect.dbConnectionClose(SqlConn)
                End If
            End If
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try

    End Function

    Function FinalQuoteCreation(ByVal requestid As String, ByVal divcode As String, ByVal strAgentRef As String, ByVal quoteId As String) As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Dim strrequestid As String = ""

        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            Dim ProcName2 As String
            ProcName2 = "sp_update_booking_Agentrefnotemp"

            Dim sqlParamList2 As New List(Of SqlParameter)
            Dim parm2(3) As SqlParameter

            parm2(0) = New SqlParameter("@div_code", CType(divcode, String))
            sqlParamList2.Add(parm2(0))
            parm2(1) = New SqlParameter("@requestid", CType(requestid, String))
            sqlParamList2.Add(parm2(1))
            parm2(2) = New SqlParameter("@agentref", CType(strAgentRef, String))
            sqlParamList2.Add(parm2(2))
            parm2(3) = New SqlParameter("@columbusref", "")
            sqlParamList2.Add(parm2(3))


            objclsUtilities.ExecuteNonQuerynew(constring, ProcName2, sqlParamList2, mySqlConn, mysqlTrans)

            If quoteId = "" Then

                If GBBookmode <> "EDIT" Then

                    Dim ProcName1 As String
                    ProcName1 = "sp_getnumber_div"

                    Dim sqlParamList1 As New List(Of SqlParameter)
                    Dim parm1(2) As SqlParameter
                    parm1(0) = New SqlParameter("@optionname", CType("QUOTEPAY", String))
                    sqlParamList1.Add(parm1(0))
                    parm1(1) = New SqlParameter("@divid", CType(divcode, String))
                    sqlParamList1.Add(parm1(1))
                    parm1(2) = New SqlParameter("@newno", "")
                    parm1(2).Direction = ParameterDirection.Output
                    parm1(2).DbType = DbType.String
                    parm1(2).Size = 50
                    sqlParamList1.Add(parm1(2))
                    objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamList1, mySqlConn, mysqlTrans)
                    strrequestid = parm1(2).Value
                Else

                    '  strrequestid = CType(GBrequestid, String)
                    Dim ProcName1 As String
                    ProcName1 = "sp_getsubquoteid"

                    Dim sqlParamList1 As New List(Of SqlParameter)
                    Dim parm1(1) As SqlParameter

                    Dim strGBrequestids() As String = GBrequestid.Split("/")
                    GBrequestid = strGBrequestids(0) & "/" & strGBrequestids(1)
                    parm1(0) = New SqlParameter("@RequestId", CType(GBrequestid, String))
                    sqlParamList1.Add(parm1(0))
                    parm1(1) = New SqlParameter("@subquoteid", "")
                    parm1(1).Direction = ParameterDirection.Output
                    parm1(1).DbType = DbType.String
                    parm1(1).Size = 50
                    sqlParamList1.Add(parm1(1))
                    objclsUtilities.ExecuteNonQuerynew(constring, ProcName1, sqlParamList1, mySqlConn, mysqlTrans)
                    strrequestid = parm1(1).Value
                End If
            Else
                strrequestid = quoteId
            End If

            Dim ProcName As String
            ProcName = "sp_finalquotesave_booking"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(4) As SqlParameter
            parm(0) = New SqlParameter("@temprequestid", CType(requestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(strrequestid, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@QuoteForPay", CType(True, Boolean))
            sqlParamList.Add(parm(3))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            'If Cumulative = 1 Then
            '    If PackageSummary <> Nothing Or PackageValueSummary <> Nothing Then
            '        Dim ProcNamePackage As String
            '        ProcNamePackage = "sp_save_quote_profit_summary"
            '        Dim sqlParamListPackage As New List(Of SqlParameter)
            '        Dim parmPackage(3) As SqlParameter
            '        parmPackage(0) = New SqlParameter("@Requestid", CType(strrequestid, String))
            '        sqlParamListPackage.Add(parmPackage(0))
            '        parmPackage(1) = New SqlParameter("@PackageSummary", PackageSummary)
            '        sqlParamListPackage.Add(parmPackage(1))
            '        parmPackage(2) = New SqlParameter("@PackageValueSummary", PackageValueSummary)
            '        sqlParamListPackage.Add(parmPackage(2))
            '        objclsUtilities.ExecuteNonQuerynew(constring, ProcNamePackage, sqlParamListPackage, mySqlConn, mysqlTrans)
            '    End If

            'End If

            ProcName = "sp_quote_edit"
            sqlParamList = New List(Of SqlParameter)
            Dim parmlist(1) As SqlParameter
            parmlist(0) = New SqlParameter("@requestid", CType(strrequestid, String))
            sqlParamList.Add(parmlist(0))
            parmlist(1) = New SqlParameter("@userlogged", CType(GBuserlogged, String))
            sqlParamList.Add(parmlist(1))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return strrequestid

        Catch ex As Exception

            mysqlTrans.Rollback()
            strrequestid = ""
            objclsUtilities.WriteErrorLog("DALQuoteGUEST: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return strrequestid
        End Try
        Return False

    End Function

    Function UpdatePaymentStatus(ByVal paymentRequestId As String, ByVal paymentId As String, ByVal paymentStatus As Boolean, ByVal userlogged As String) As Boolean
        Dim SqlConn As New SqlConnection
        Try
            SqlConn = clsDBConnect.dbConnection()         'connection open
            Dim myCommand As SqlCommand = New SqlCommand("sp_update_booking_payment_requestHeader", SqlConn)
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Parameters.Add(New SqlParameter("@paymentRequestId", SqlDbType.VarChar, 50)).Value = CType(paymentRequestId, String)
            myCommand.Parameters.Add(New SqlParameter("@paymentId", SqlDbType.VarChar, 100)).Value = paymentId
            myCommand.Parameters.Add(New SqlParameter("@paymentStatus", SqlDbType.Bit)).Value = paymentStatus
            myCommand.Parameters.Add(New SqlParameter("@userlogged", SqlDbType.VarChar, 50)).Value = userlogged
            myCommand.ExecuteNonQuery()
            clsDBConnect.dbCommandClose(myCommand)               'sql command disposed
            clsDBConnect.dbConnectionClose(SqlConn)
            Return True
        Catch ex As Exception
            If Not SqlConn Is Nothing Then
                If SqlConn.State = ConnectionState.Open Then
                    clsDBConnect.dbConnectionClose(SqlConn)
                End If
            End If
            objclsUtilities.WriteErrorLog("DALQuoteGUEST: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
    End Function

    Function CalculatePaidAmount(ByVal tempRequestId As String) As DataTable
        Dim SqlConn As New SqlConnection
        Try
            SqlConn = clsDBConnect.dbConnection()         'connection open
            Dim myCommand As SqlCommand = New SqlCommand("sp_get_booking_PaidAmount", SqlConn)
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Parameters.Add(New SqlParameter("@tempRequestId", SqlDbType.VarChar, 20)).Value = CType(tempRequestId, String)
            Dim dt As New DataTable
            Using myAdapter As New SqlDataAdapter(myCommand)
                myAdapter.Fill(dt)
            End Using
            clsDBConnect.dbCommandClose(myCommand)               'sql command disposed
            clsDBConnect.dbConnectionClose(SqlConn)
            Return dt
        Catch ex As Exception
            If Not SqlConn Is Nothing Then
                If SqlConn.State = ConnectionState.Open Then
                    clsDBConnect.dbConnectionClose(SqlConn)
                End If
            End If
            objclsUtilities.WriteErrorLog("DALQuoteGUEST: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

End Class
