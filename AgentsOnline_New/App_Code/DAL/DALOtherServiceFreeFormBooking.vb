Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class DALOtherServiceFreeFormBooking
    Private _Div_Code As String = ""
    Public Property Div_Code As String
        Get
            Return _Div_Code
        End Get
        Set(ByVal value As String)
            _Div_Code = value
        End Set
    End Property
    Private _RequestId As String = ""
    Public Property RequestId As String
        Get
            Return _RequestId
        End Get
        Set(ByVal value As String)
            _RequestId = value
        End Set
    End Property
    Private _AgentCode As String = ""
    Public Property AgentCode As String
        Get
            Return _AgentCode
        End Get
        Set(ByVal value As String)
            _AgentCode = value
        End Set
    End Property
    Private _SourcectryCode As String = ""
    Public Property SourcectryCode As String
        Get
            Return _SourcectryCode
        End Get
        Set(ByVal value As String)
            _SourcectryCode = value
        End Set
    End Property
    Private _Agentref As String = ""
    Public Property Agentref As String
        Get
            Return _Agentref
        End Get
        Set(ByVal value As String)
            _Agentref = value
        End Set
    End Property
    Private _Remarks As String = ""
    Public Property Remarks As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property
    Private _ColumbusRef As String = ""
    Public Property ColumbusRef As String
        Get
            Return _ColumbusRef
        End Get
        Set(ByVal value As String)
            _ColumbusRef = value
        End Set
    End Property
    Private _UserLogged As String = ""
    Public Property UserLogged As String
        Get
            Return _UserLogged
        End Get
        Set(ByVal value As String)
            _UserLogged = value
        End Set
    End Property
    Private _RlineNo As String = ""
    Public Property RlineNos As String
        Get
            Return _RlineNo
        End Get
        Set(ByVal value As String)
            _RlineNo = value
        End Set
    End Property
    Private _SubUserCode As String = ""
    Public Property SubUserCode As String
        Get
            Return _SubUserCode
        End Get
        Set(ByVal value As String)
            _SubUserCode = value
        End Set
    End Property
    Private _OtherServicetXml As String = ""
    Public Property OtherServiceXml As String
        Get
            Return _OtherServicetXml
        End Get
        Set(ByVal value As String)
            _OtherServicetXml = value
        End Set
    End Property

    Dim objclsUtilities As New clsUtilities
    Function GetOtherServiceDatas(strRequestId As String, strRlineNo As String) As Data.DataTable
        Try

            Dim ProcName As String
            ProcName = "sp_fill_OtherService_freeform"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            parm(1) = New SqlParameter("@olineno", CType(strRlineNo, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt

        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALOtherServiceFreeFormBooking:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SaveOtherServiceFreeFormBooking() As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try

            'Dim mySqlCmd As SqlCommand
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction


            Dim ProcName As String
            ProcName = "sp_save_freeformbooking_OtherService"

            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(11) As SqlParameter

            parm(0) = New SqlParameter("@div_code", CType(Div_Code, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@requestid", CType(RequestId, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@agentcode", CType(AgentCode, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@sourcectrycode", CType(SourcectryCode, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@reqoverride", CType("0", Integer))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@agentref", CType(Agentref, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@columbusref", CType(ColumbusRef, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@remarks", CType(Remarks, String))
            sqlParamList.Add(parm(7))
            parm(8) = New SqlParameter("@SubUserCode", CType(SubUserCode, String))
            sqlParamList.Add(parm(8))
            parm(9) = New SqlParameter("@userlogged", CType(UserLogged, String))
            sqlParamList.Add(parm(9))
            parm(10) = New SqlParameter("@olinenostring", CType(RlineNos, String))
            sqlParamList.Add(parm(10))
            parm(11) = New SqlParameter("@OtherServicexml", CType(OtherServiceXml, String))
            sqlParamList.Add(parm(11))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)


            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALOtherServiceSearch: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False




    End Function
End Class
