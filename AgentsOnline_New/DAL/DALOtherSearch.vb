Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DALOtherSearch

    Dim objclsUtilities As New clsUtilities
    Public OBOtherCancelXml As String = ""
    Public OBRequestId As String = ""
    Public UserLogged As String = ""
    Dim _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property


    Function GetSearchDetails(ByVal LoginType As String, ByVal WebuserName As String, ByVal AgentCode As String, ByVal FromDate As String, ByVal ToDate As String, ByVal SelectGroupCode As String, ByVal Adult As String, ByVal Children As String, ByVal ChildAgeString As String, ByVal SourceCountryCode As String, ByVal OverridePrice As String, ByVal DateChange As String, ByVal OtherTypeCode As String, ByVal SelectedDate As String, ByVal AmendRequestid As String, ByVal AmendLineno As String, ByVal Resetsearch As String) As DataSet
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
            ProcName = "sp_booking_others_search"
            Dim parm(16) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@webusername", CType(WebuserName, String))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            parm(3) = New SqlParameter("@fromdate", CType(FromDate, String))
            parm(4) = New SqlParameter("@todate", CType(ToDate, String))

            parm(5) = New SqlParameter("@othgrpcode", CType(SelectGroupCode, String))

            parm(6) = New SqlParameter("@noofadults", CType(Adult, String))
            parm(7) = New SqlParameter("@noofchild", CType(Children, String))
            parm(8) = New SqlParameter("@childagestring", CType(ChildAgeString, String))

            parm(9) = New SqlParameter("@sourcectrycode", CType(SourceCountryCode, String))
            parm(10) = New SqlParameter("@override", CType(OverridePrice, String))

            parm(11) = New SqlParameter("@datechange", CType(DateChange, String))
            parm(12) = New SqlParameter("@othtypcode", CType(OtherTypeCode, String))

            parm(13) = New SqlParameter("@selecteddate", CType(SelectedDate, String))
            parm(14) = New SqlParameter("@requestid", CType(AmendRequestid, String))
            parm(15) = New SqlParameter("@olineno", CType(AmendLineno, String))
            parm(16) = New SqlParameter("@refinesearch", CType(Resetsearch, String))

            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetTourSummary(ByVal strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(totalsalevalue)),3)   as total from view_booking_tourstemp where requestid='" & strRequestId & "'))+' ' +currcode  as total,ROUND(convert(varchar(15),totalsalevalue),3)totalsalevalueNew from view_booking_tourstemp where requestid='" & strRequestId & "' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function


    Function SaveOtherServiceBookingInTemp(ByVal EBdiv_code As String, ByVal agentcode As String, ByVal EBsourcectrycode As String, ByVal EBreqoverride As String, ByVal EBrequestid As String, ByVal EBagentref As String, ByVal EBcolumbusref As String, ByVal EBobremarks As String, ByVal EBToursXml As String, ByVal EBuserlogged As String, ByVal OBRlinenoString As String) As String
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

            ProcName = "sp_del_booking_otherstemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parmtour(2) As SqlParameter

            parmtour(0) = New SqlParameter("@requestid", CType(EBrequestid, String))
            sqlParamList.Add(parmtour(0))
            parmtour(1) = New SqlParameter("@tlinenostring", CType(OBRlinenoString, String))
            sqlParamList.Add(parmtour(1))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)




            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_otherstemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(EBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@otherxml", CType(EBToursXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(EBuserlogged, String))
            sqlParamListguest.Add(parmguest(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

    Function GetOtherSummary(ByVal strRequestId As String, Optional ByVal strwhite As String = "0") As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""

            If strwhite = "1" Then
                strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(case when isnull(tc.cancelled,0)=1 then 0 else wlunitsalevalue end)),3)   as total from view_booking_otherstemp t (nolock) left join booking_others_confcanceltemp tc (nolock) on tc.requestid=t.requestid and tc.olineno=t.olineno where t.requestid='" & strRequestId & "'))+' ' +wlcurrcode  as total, " _
              & " ROUND(convert(varchar(15),wlunitsalevalue),3)totalsalevalueNew,ROUND(convert(varchar(15),wlunitprice),3)unitpriceNew,(select isnull(min(isnull(cancelled,0)),0) from booking_others_confcanceltemp  bb(nolock)  where bb.requestid='" & strRequestId & "' and bb.olineno=view_booking_otherstemp.olineno)  cancelled ,bookingmode " _
              & " from view_booking_otherstemp where requestid='" & strRequestId & "' order by olineno"
            Else
                strQuery = "select *,convert(varchar(10),(select ROUND(convert(varchar(15),sum(case when isnull(tc.cancelled,0)=1 then 0 else unitsalevalue end)),3)   as total from view_booking_otherstemp t (nolock) left join booking_others_confcanceltemp tc (nolock) on tc.requestid=t.requestid and tc.olineno=t.olineno where t.requestid='" & strRequestId & "'))+' ' +currcode  as total, " _
              & " ROUND(convert(varchar(15),unitsalevalue),3)totalsalevalueNew,ROUND(convert(varchar(15),unitprice),3)unitpriceNew,(select isnull(min(isnull(cancelled,0)),0) from booking_others_confcanceltemp  bb(nolock)  where bb.requestid='" & strRequestId & "' and bb.olineno=view_booking_otherstemp.olineno)  cancelled,bookingmode  " _
              & " from view_booking_otherstemp where requestid='" & strRequestId & "' order by olineno"
            End If

          
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Sub RemoveOthers(ByVal strRequestId As String, ByVal strelineno As String)

        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            Dim ProcName As String


            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            ProcName = "sp_del_booking_otherstemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@tlinenostring", CType(strelineno, String))
            sqlParamList.Add(parm(1))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)

        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)

        End Try


    End Sub

    Function GetEditBookingDetails(ByVal requestid As String, ByVal olineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_other_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@olineno", CType(olineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetOtherCancelDetails(ByVal strRequestId As String, ByVal strvlineno As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_get_other_canceldetails"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@olineno", CType(strvlineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function


    Function SavingCancelOtherInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_updateotherscanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(OBRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@othercancelxml", CType(OBOtherCancelXml, String))
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

            objclsUtilities.WriteErrorLog("DALOtherSEARCH:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function


End Class
