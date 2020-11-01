Imports Microsoft.VisualBasic

 
Imports System.Data
Imports System.Data.SqlClient
Public Class DALVISA
    Public VBrequestid As String = ""
    Public VBVisaXml As String = ""
    Public VBuserlogged As String = ""
    Public VBRlinenoString As String = ""

    Public VBCancelVisaXml As String = ""
 


 
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

    Function SavingVisaBookingInTemp() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_del_booking_visatemp"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(1) As SqlParameter

            parm(0) = New SqlParameter("@requestid", CType(VBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@tlinenostring", CType(VBRlinenoString, String))
            sqlParamList.Add(parm(1))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)

            Dim ProcNameguest As String
            ProcNameguest = "sp_add_booking_visatemp"
            Dim sqlParamListguest As New List(Of SqlParameter)
            Dim parmguest(3) As SqlParameter
            parmguest(0) = New SqlParameter("@requestid", CType(VBrequestid, String))
            sqlParamListguest.Add(parmguest(0))
            parmguest(1) = New SqlParameter("@visaxml", CType(VBVisaXml, String))
            sqlParamListguest.Add(parmguest(1))
            parmguest(2) = New SqlParameter("@userlogged", CType(VBuserlogged, String))
            sqlParamListguest.Add(parmguest(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcNameguest, sqlParamListguest, mySqlConn, mysqlTrans)
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
    Sub RemoveVisa(ByVal strRequestId As String, ByVal strvlineno As String)

        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            Dim ProcName As String


            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction

            ProcName = "sp_del_booking_visatemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@tlinenostring", CType(strvlineno, String))
            sqlParamList.Add(parm(1))

            objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)

            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)

        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALVISA:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)

        End Try


    End Sub

    Function GetEditBookingDetails(ByVal requestid As String, ByVal vlineno As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_booking_visa_amend"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@vlineno", CType(vlineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALVisa:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetVisaCancelDetails(ByVal strRequestId As String, ByVal strvlineno As String) As DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_get_visa_canceldetails"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@vlineno", CType(strvlineno, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALVisa:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingCancelVisaInTemp() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_updatevisacanceltemp"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(VBrequestid, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@visacancelxml", CType(VBCancelVisaXml, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@userlogged", CType(VBuserlogged, String))
            sqlParamList.Add(parm(2))
            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALVisa:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function
  
End Class

