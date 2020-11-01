﻿Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DALTourSearch

    Public EBrequestid As String = ""
    Public EVBVisaXml As String = ""
    Public EBuserlogged As String = ""
    Public EBCancelToursXml As String = ""

    Dim objclsUtilities As New clsUtilities
    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property


    Function SaveExcursionTypeBookingInTemp(ByVal EBdiv_code As String, ByVal agentcode As String, ByVal EBsourcectrycode As String, ByVal EBreqoverride As String, ByVal EBrequestid As String, ByVal EBagentref As String, ByVal EBcolumbusref As String, ByVal EBobremarks As String, ByVal EBToursXml As String, ByVal EBuserlogged As String, ByVal RowLineNo As String, ByVal SectorgroupCode As String, ByVal OpMode As String, ByVal BufferMultiCostBreakup As String, ByVal BufferComboBreakup As String, ByVal BufferInventoryBreakup As String) As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction


            Dim ProcNameheader As String
            ProcNameheader = "sp_update_booking_headertemp"

            Dim sqlParamListheader As New List(Of SqlParameter)
            Dim parm(10) As SqlParameter

            parm(0) = New SqlParameter("@div_code", CType(EBdiv_code, String))
            sqlParamListheader.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(EBrequestid, String))
            sqlParamListheader.Add(parm(1))
            parm(2) = New SqlParameter("@agentcode", CType(agentcode, String))
            sqlParamListheader.Add(parm(2))
            parm(3) = New SqlParameter("@sourcectrycode", CType(EBsourcectrycode, String))
            sqlParamListheader.Add(parm(3))
            parm(4) = New SqlParameter("@reqoverride", CType(EBreqoverride, Integer))
            sqlParamListheader.Add(parm(4))
            parm(5) = New SqlParameter("@agentref", CType(EBagentref, String))
            sqlParamListheader.Add(parm(5))
            parm(6) = New SqlParameter("@columbusref", CType(EBcolumbusref, String))
            sqlParamListheader.Add(parm(6))
            parm(7) = New SqlParameter("@remarks", CType(EBobremarks, String))
            sqlParamListheader.Add(parm(7))
            parm(8) = New SqlParameter("@SubUserCode", CType(SubUserCode, String))
            sqlParamListheader.Add(parm(8))
            parm(9) = New SqlParameter("@userlogged", CType(EBuserlogged, String))
            sqlParamListheader.Add(parm(9))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameheader, sqlParamListheader, mySqlConn, mysqlTrans)

            Dim ProcName As String

            ProcName = "sp_del_booking_tourstemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parmtour(3) As SqlParameter

            parmtour(0) = New SqlParameter("@requestid", CType(EBrequestid, String))
            sqlParamList.Add(parmtour(0))
            parmtour(1) = New SqlParameter("@RowLineNo", CType(RowLineNo, String))
            sqlParamList.Add(parmtour(1))
            parmtour(2) = New SqlParameter("@SectorgroupCode", CType(SectorgroupCode, String))
            sqlParamList.Add(parmtour(2))
            parmtour(3) = New SqlParameter("@OpMode", CType(OpMode, String))
            sqlParamList.Add(parmtour(3))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)




            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_tourstemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(5) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(EBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@tourxml", CType(EBToursXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@MultiCostBreakup", CType(BufferMultiCostBreakup, String))
            sqlParamListguest.Add(parmguest(2))
            parmguest(3) = New SqlParameter("@BufferComboBreakup", CType(BufferComboBreakup, String))
            sqlParamListguest.Add(parmguest(3))
            parmguest(4) = New SqlParameter("@BufferInventoryBreakup", CType(BufferInventoryBreakup, String))
            sqlParamListguest.Add(parmguest(4))

            parmguest(5) = New SqlParameter("@userlogged", CType(EBuserlogged, String))
            sqlParamListguest.Add(parmguest(5))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function
    Function GetSearchDetails(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal FromDate As String, ByVal ToDate As String, ByVal TourStartingFromCode As String, ByVal ClassificationCode As String, ByVal StarCategoryCode As String, ByVal Adult As String, ByVal Children As String, ByVal ChildAgeString As String, ByVal SeniorCitizen As String, ByVal SourceCountryCode As String, ByVal OverridePrice As String, ByVal DateChange As String, ByVal ExcTypeCode As String, ByVal VehicleCode As String, ByVal SelectedDate As String, ByVal PrivateOrSIC As String, ByVal AmendRequestid As String, ByVal AmendLineno As String) As DataSet
        Try
            If FromDate <> "" Then
                Dim strDates As String() = FromDate.Split("/")
                FromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If ToDate <> "" Then
                Dim strDates As String() = ToDate.Split("/")
                ToDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If
            If SelectedDate <> "" Then
                Dim strDates As String() = SelectedDate.Split("/")
                SelectedDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
            End If

            Dim ProcName As String
            ProcName = "sp_booking_tours_search"
            Dim parm(20) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@fromdate", CType(FromDate, String))
            parm(4) = New SqlParameter("@todate", CType(ToDate, String))

            parm(5) = New SqlParameter("@sectorgroupcode", CType(TourStartingFromCode, String))
            parm(6) = New SqlParameter("@classificationcode", CType(ClassificationCode, String))
            parm(7) = New SqlParameter("@starcategory", CType(StarCategoryCode, String))

            parm(8) = New SqlParameter("@noofadults", CType(Adult, String))
            parm(9) = New SqlParameter("@noofchild", CType(Children, String))
            parm(10) = New SqlParameter("@childagestring", CType(ChildAgeString, String))
            parm(11) = New SqlParameter("@noofsenior", CType(SeniorCitizen, String))
            parm(12) = New SqlParameter("@sourcectrycode", CType(SourceCountryCode, String))
            parm(13) = New SqlParameter("@override", CType(OverridePrice, String))

            parm(14) = New SqlParameter("@datechange", CType(DateChange, String))
            parm(15) = New SqlParameter("@exctypcode", CType(ExcTypeCode, String))
            parm(16) = New SqlParameter("@vehiclecode", CType(VehicleCode, String))
            parm(17) = New SqlParameter("@selecteddate", CType(SelectedDate, String))
            parm(18) = New SqlParameter("@privateorsic", CType(PrivateOrSIC, String))
            parm(19) = New SqlParameter("@requestid", CType(AmendRequestid, String))
            parm(20) = New SqlParameter("@elineno", CType(AmendLineno, String))

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetTourSummary(strRequestId As String, strWhiteLabel As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            If strWhiteLabel = "1" Then
                strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(case when isnull(tc.cancelled,0)=1 then 0 else wltotalsalevalue end)),3)   as total from view_booking_tourstemp t (nolock) left join booking_tours_confcanceltemp tc (nolock) on tc.requestid=t.requestid and tc.elineno=t.elineno where t.requestid='" & strRequestId & "'))+' ' +wlcurrcode  as total, " _
               & " ROUND(convert(varchar(15),wltotalsalevalue),3)totalsalevalueNew, (select isnull(min(isnull(cancelled,0)),0) from booking_tours_confcanceltemp  bb(nolock) " _
               & " where bb.requestid='" & strRequestId & "' and bb.elineno=view_booking_tourstemp.elineno)  cancelled,sectorgroupcode,isnull(sectorgroupname,''),combo,multipledatesyesno , case  when ratebasis<>'ACS' then convert(varchar(2), units)+ '('+convert(varchar(2),adults) +'A' + case when child <>0 then '+'+convert(varchar(2),child) + 'C' else '' end  + ')'  else convert(varchar(2),units)  end unitnew,BookingMode    from view_booking_tourstemp where requestid='" & strRequestId & "'  order by isnull(view_booking_tourstemp.sectorgroupname,''),CONVERT(datetime,excdate,103) asc "
            Else
                strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(case when isnull(tc.cancelled,0)=1 then 0 else totalsalevalue end)),3)   as total from view_booking_tourstemp t (nolock) left join booking_tours_confcanceltemp tc (nolock) on tc.requestid=t.requestid and tc.elineno=t.elineno where t.requestid='" & strRequestId & "'))+' ' +currcode  as total, " _
               & " ROUND(convert(varchar(15),totalsalevalue),3)totalsalevalueNew, (select isnull(min(isnull(cancelled,0)),0) from booking_tours_confcanceltemp  bb(nolock) " _
               & " where bb.requestid='" & strRequestId & "' and bb.elineno=view_booking_tourstemp.elineno)  cancelled,sectorgroupcode,isnull(sectorgroupname,'') ,combo,multipledatesyesno , case  when ratebasis<>'ACS' then convert(varchar(2), units)+ '('+convert(varchar(2),adults) +'A' + case when child <>0 then '+'+convert(varchar(2),child) + 'C' else '' end  + ')'  else convert(varchar(2),units)  end unitnew,BookingMode    from view_booking_tourstemp where requestid='" & strRequestId & "'  order by isnull(view_booking_tourstemp.sectorgroupname,''),CONVERT(datetime,excdate,103) asc "
            End If
           
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetEditBookingDetails(ByVal requestid As String, ByVal elineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_tours_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@elineno", CType(elineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Sub RemoveTours(ByVal strRequestId As String, ByVal strelineno As String)

        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            Dim ProcName As String


            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            ProcName = "sp_del_booking_tourstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(3) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@RowLineNo", CType(strelineno, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@SectorgroupCode", CType("", String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@OpMode", CType("N", String))
            sqlParamList.Add(parm(3))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)

        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)

        End Try


    End Sub
    Function GetTourCancelDetails(ByVal strRequestId As String, ByVal strvlineno As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_get_tour_canceldetails"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@elineno", CType(strvlineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTOURSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingCancelTourInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_updatetourcanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(EBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@tourcancelxml", CType(EBCancelToursXml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(EBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALTOURSSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function ValidateExcWeekDays(ExcTypeCode As String, ExcDate As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "SP_VALIDATE_EXC_WEEKDAYS"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@exctypcode", CType(ExcTypeCode, String))
            parm(1) = New SqlParameter("@tourdate", CType(ExcDate, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTOURSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function FillTourPickupLocation(strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select distinct othtypcode,othtypname  from  othtypmast(nolock) o,booking_hotel_detailtemp(nolock) d,partymast(nolock) p,sectormaster(nolock) s where o.active=1 and d.partycode=p.partycode and p.sectorcode=s.sectorcode and s.sectorgroupcode=o.othtypcode and  o.othgrpcode in (select option_selected from reservation_parameters where param_id=1001) and d.requestid='" & strRequestId & "' order by o.othtypname "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetComboExcursions_WithRateBasis(ByVal ExcCode As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select e.exctypname,exctypcombocode,ec.exctypcode,(SELECT ratebasis FROM excursiontypes D WHERE D.exctypcode='" + ExcCode + "') ratebasis,isnull(bb.chidlagefrom,0) chidlagefrom,isnull(bb.childageto,0)childageto from excursiontypes_combodetails(nolock) ec,excursiontypes(nolock)e left join excursiontypes_occupancy(nolock)BB on bb.exctypcode='" + ExcCode + "'  where ec.exctypcode='" & ExcCode & "' and  ec.exctypcombocode=e.exctypcode"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetMultipleDates_WithRateBasis(ByVal strFromDate As String, ByVal strToDate As String, ByVal ExcCode As String) As DataSet
        Try

            Dim ProcName As String
            ProcName = "SP_GET_EXC_MULTIPLE_DATES_Multidate"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@FromDate", CType(strFromDate, String))
            parm(1) = New SqlParameter("@ToDate", CType(strToDate, String))
            parm(2) = New SqlParameter("@exctypcode", CType(ExcCode, String))
            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTOURSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetComboExcursions(ExcCode As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select e.exctypname,exctypcombocode,ec.exctypcode from excursiontypes_combodetails(nolock) ec,excursiontypes(nolock)e where ec.exctypcode='" & ExcCode & "' and  ec.exctypcombocode=e.exctypcode"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetMultipleDates(strFromDate As String, strToDate As String, ExcCode As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "SP_GET_EXC_MULTIPLE_DATES"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@FromDate", CType(strFromDate, String))
            parm(1) = New SqlParameter("@ToDate", CType(strToDate, String))
            parm(2) = New SqlParameter("@exctypcode", CType(ExcCode, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTOURSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function BindComboTourDataTable(strrequestid As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select distinct exctypcode,vehiclecode,convert(varchar(10),excdate,103)excdate,exctypcombocode,type from booking_tours_combodetails_temp(nolock) where requestid='" & strrequestid & "'"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetBookedComboMultiDateDetails(strrequestid As String, excCode As String, Type As String, ElineNo As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select distinct exctypcombocode,e.exctypname,convert(varchar(10),b.excdate,103)excdate from booking_tours_combodetails_temp b(nolock),excursiontypes e(nolock) where requestid='" & strrequestid & "' and b.exctypcode='" & excCode & "' and b.exctypcombocode=e.exctypcode and type='" & Type & "' and b.elineno='" & ElineNo & "' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetTourCheckInAndCheckOutDetails(strRequestId As String, sector As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            If sector = "" Then
                strQuery = "select convert(varchar(10), min(searchfromdate),103)searchfromdate,convert(varchar(10), max(searchtodate),103)searchtodate from booking_tourstemp(nolock) where requestid='" & strRequestId & "'  "
            Else
                strQuery = "select convert(varchar(10), min(searchfromdate),103)searchfromdate,convert(varchar(10), max(searchtodate),103)searchtodate from booking_tourstemp(nolock) where requestid='" & strRequestId & "' and  tourstartingfrom='" & sector & "' "
            End If
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function ValidateTourSearchDateGaps(strRequestId As String, strFromDate As String, strToDate As String) As Boolean
        Try
            Dim bStatus As Boolean = False
            Dim ProcName As String
            ProcName = "sp_validate_toursearch_date_gap"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@RequestId", CType(strRequestId, String))
            parm(1) = New SqlParameter("@FromDate", CType(strFromDate, String))
            parm(2) = New SqlParameter("@ToDate", CType(strToDate, String))

            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("FLAG").ToString = "0" Then
                    bStatus = True
                End If
            End If
            Return bStatus
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTOURSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
    End Function

    Function BindExcInventoryDataTable(strrequestid As String, strSectorCode As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select exctypcode EXC_CODE,vehiclecode VEHICLE_CODE,exctypcombocode EXC_TYPE_COMBO_CODE,convert(varchar(10),excdate,103) EXC_DATE,TYPE,	TIMESLOT_FROM,TIMESLOT_TO,TIMESLOT,ADULT,CHILD,SENIOR ,TEMP_CANCEL_NO,TEMP_CANCEL_DATE,TEMP_CONF_NO,TEMP_CONF_DATE,CANCEL_NO,CANCEL_DATE,CONF_NO,CONF_DATE,TEMP_ID,INVENTORY_ID,0 IS_CHANGED from booking_tours_inventory_temp(nolock) where requestid='" & strrequestid & "' and elineno in (select distinct elineno from booking_tourstemp (nolock) where requestid='" & strrequestid & "' and tourstartingfrom='" & strSectorCode & "')"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALTourSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

End Class
