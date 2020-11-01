Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.IO.File
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web.Configuration
Public Class clsUtils
    Dim myDataReader As SqlDataReader
    Dim SqlConn As SqlConnection
    Dim myCommand As SqlCommand
    Dim myDataAdapter As SqlDataAdapter
    Dim _strConn As String
    Dim Path As String = ConfigurationManager.AppSettings("AppLogs").ToString

    Public Sub FillDropDownListnew(ByVal connstr As String, ByVal ddl As System.Web.UI.WebControls.DropDownList, ByVal FiledName As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False)
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection
            myCommand = New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            ddl.DataSource = myDataReader
            ddl.DataTextField = FiledName
            ddl.DataValueField = FiledName
            ddl.DataBind()
            If addNewFlag = True Then
                ddl.Items.Add("[Select]")
                ddl.SelectedValue = "[Select]"
            End If
        Catch ex As Exception

        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbReaderClose(myDataReader)                'Close reader
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Sub

    Public Sub Clear_All_contract_sessions()

        HttpContext.Current.Session("Maxid") = Nothing
        HttpContext.Current.Session("contractid") = Nothing
    End Sub
#Region "Public Sub FillDropDownListnew(ByVal connstr As String, ByVal ddl As System.Web.UI.WebControls.DropDownList, ByVal strDataTextField As String, ByVal strDataValueField As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False)"
    ' This method is used for fill dropdown
    Public Sub FillDropDownListWithValuenew(ByVal connstr As String, ByVal ddl As System.Web.UI.WebControls.DropDownList, ByVal strDataTextField As String, ByVal strDataValueField As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False)
        Dim myDataReader As SqlDataReader
        Dim SqlConn As New SqlConnection
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)
            Dim myCommand As New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader()
            ddl.DataSource = myDataReader
            ddl.DataTextField = strDataTextField
            ddl.DataValueField = strDataValueField
            ddl.DataBind()
            If addNewFlag = True Then
                ddl.Items.Add("[Select]")
                ddl.SelectedValue = "[Select]"
            End If
            myCommand.Dispose()
            myDataReader.Close()
            SqlConn.Close()
        Catch ex As Exception
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
        End Try
    End Sub
#End Region

#Region "Public Sub FillDropDownListnewAll(ByVal connstr As String, ByVal ddl As System.Web.UI.WebControls.DropDownList, ByVal strDataTextField As String, ByVal strDataValueField As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False)"
    ' This method is used for fill dropdown
    Public Sub FillDropDownListWithValuenewAll(ByVal connstr As String, ByVal ddl As System.Web.UI.WebControls.DropDownList, ByVal strDataTextField As String, ByVal strDataValueField As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False)
        Dim myDataReader As SqlDataReader
        Dim SqlConn As New SqlConnection
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)
            Dim myCommand As New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader()
            ddl.DataSource = myDataReader
            ddl.DataTextField = strDataTextField
            ddl.DataValueField = strDataValueField
            ddl.DataBind()
            If addNewFlag = True Then
                ddl.Items.Add("[All]")
                ddl.SelectedValue = "[All]"
            End If
            myCommand.Dispose()
            myDataReader.Close()
            SqlConn.Close()
        Catch ex As Exception
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If
        End Try
    End Sub
#End Region

    Public Sub FillListBoxnew(ByVal connstr As String, ByVal lb As System.Web.UI.WebControls.ListBox, ByVal FiledName As String, ByVal strQry As String)
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection
            myCommand = New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            lb.DataSource = myDataReader
            lb.DataTextField = FiledName
            lb.DataValueField = FiledName
            lb.DataBind()
        Catch ex As Exception

        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbReaderClose(myDataReader)                'Close reader
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Sub

    Public Function DDLFieldAvliable(ByVal ddl As DropDownList, ByVal strval As String) As Boolean
        Dim lngcnt As Long
        DDLFieldAvliable = False
        For lngcnt = 0 To ddl.Items.Count - 1
            If ddl.Items(lngcnt).Text = strval Then
                DDLFieldAvliable = True
                Exit For
            End If
        Next
    End Function

    Public Function GetDataFromDatasetnew(ByVal connstr As String, ByVal strSqlQuery As String) As DataSet
        Dim ds As New DataSet
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection

            myDataAdapter = New SqlDataAdapter(strSqlQuery, SqlConn)
            myDataAdapter.SelectCommand.CommandTimeout = 0
            myDataAdapter.Fill(ds)
            GetDataFromDatasetnew = ds

        Catch ex As Exception
            GetDataFromDatasetnew = Nothing
        End Try
    End Function

    Public Function GetDataFromReadernew(ByVal connstr As String, ByVal strSqlQuery As String) As SqlDataReader
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection

            myCommand = New SqlCommand(strSqlQuery, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            GetDataFromReadernew = myDataReader

        Catch ex As Exception
            GetDataFromReadernew = Nothing
        End Try
    End Function
    Public Function ExecuteQueryReturnSingleValuenew(ByVal connstr As String, ByVal strQuery As String) As Object
        Dim objAgrValue As Object
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection

            myCommand = New SqlCommand(strQuery, SqlConn)
            objAgrValue = myCommand.ExecuteScalar
            If IsDBNull(objAgrValue) = False Then
                ExecuteQueryReturnSingleValuenew = objAgrValue
            Else
                ExecuteQueryReturnSingleValuenew = 0
            End If
        Catch ex As Exception
            ExecuteQueryReturnSingleValuenew = 0
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Function
    Public Function ExecuteQueryReturnStringValuenew(ByVal connstr As String, ByVal strQuery As String) As String
        Dim objAgrValue As String
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                    'Open connection
            'EventLog.WriteEntry("websrv", "qry:" & strQuery)
            myCommand = New SqlCommand(strQuery, SqlConn)
            objAgrValue = myCommand.ExecuteScalar
            If IsDBNull(objAgrValue) = False Then
                ExecuteQueryReturnStringValuenew = objAgrValue
            Else
                ExecuteQueryReturnStringValuenew = ""
            End If
        Catch ex As Exception
            ExecuteQueryReturnStringValuenew = ""
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Function
    Public Function GetDBFieldFromStringnew(ByVal connstr As String, ByVal strTableName As String, ByVal strKeyName As String, _
            ByVal strCriteria As String, ByVal strValue As String) As Object
        Dim strQry As String
        Try
            strQry = "SELECT " & strKeyName & " FROM " & strTableName & _
                     " WHERE " & strCriteria & "='" & strValue & "'"
            GetDBFieldFromStringnew = ExecuteQueryReturnStringValuenew(connstr, strQry)
        Catch ex As Exception
            GetDBFieldFromStringnew = 0
        Finally
        End Try
    End Function
    Public Function EntryExists(ByVal connstr As String, ByVal strTableName As String, ByVal strKeyName As String, _
               Optional ByVal strWhereCond As String = "") As Boolean
        Dim strQry As String
        Dim drResult As SqlDataReader
        Try
            strQry = "SELECT " & strKeyName & " FROM " & strTableName
            If strWhereCond <> "" Then
                strQry += " WHERE " & strWhereCond
            End If
            drResult = GetDataFromReadernew(connstr, strQry)

            If drResult Is Nothing = False Then
                If drResult.HasRows Then
                    EntryExists = True
                Else
                    EntryExists = False
                End If
            Else
                EntryExists = False
            End If

        Catch ex As Exception
            EntryExists = False
        Finally
        End Try

    End Function
    Public Function GetDBFieldFromLongnew(ByVal connstr As String, ByVal strTableName As String, ByVal strKeyName As String, _
    ByVal strCriteria As String, ByVal lngValue As Long) As Object
        Dim strQry As String
        Try
            strQry = "SELECT " & strKeyName & " FROM " & strTableName & _
            " WHERE " & strCriteria & "=" & lngValue
            GetDBFieldFromLongnew = ExecuteQueryReturnSingleValuenew(connstr, strQry)
        Catch ex As Exception
            GetDBFieldFromLongnew = 0
        Finally
        End Try
    End Function

    Public Function GetDBFieldFromMultipleCriterianew(ByVal connstr As String, ByVal strTableName As String, ByVal strKeyName As String, _
    ByVal strCriteria As String) As Object
        Dim strQry As String
        Try
            strQry = "SELECT " & strKeyName & " FROM " & strTableName & " WHERE " & strCriteria
            GetDBFieldFromMultipleCriterianew = ExecuteQueryReturnSingleValuenew(connstr, strQry)
        Catch ex As Exception
            GetDBFieldFromMultipleCriterianew = 0
        End Try
    End Function
    Public Function isDuplicatenew(ByVal connstr As String, ByVal strTableName As String, ByVal strFieldName As String, ByVal varDuplicateValue As Object, Optional ByVal strFilter As String = "") As Integer
        Dim strSelectQry As String
        strSelectQry = "SELECT " & strFieldName & " FROM " & strTableName & " WHERE " & strFieldName & " = '" & varDuplicateValue & "'"
        If strFilter <> "" Then
            strSelectQry = strSelectQry & " And " & strFilter
        End If
        Dim strQryValue
        strQryValue = ExecuteQueryReturnSingleValuenew(connstr, strSelectQry) 'Use Aggregate
        If Trim(strQryValue) = "" Then
            isDuplicatenew = 0
        Else
            isDuplicatenew = 1
        End If
    End Function
    Public Function isDuplicateForModifynew(ByVal connstr As String, ByVal strTable As String, ByVal strPrimaryKey As String, ByVal strFieldName As String, _
       ByVal varDupVal As Object, ByVal lngID As String, Optional ByVal strFilter As String = "") As Boolean
        Dim strSelectQry As String
        If strFilter <> "" Then
            strSelectQry = "SELECT " & strPrimaryKey & " FROM " & strTable & " WHERE " & strPrimaryKey & "<> '" & lngID & "' and " & strFieldName & "='" & varDupVal & "' and " & strFilter
        Else
            strSelectQry = "SELECT " & strPrimaryKey & " FROM " & strTable & " WHERE " & strPrimaryKey & "<> '" & lngID & "' and " & strFieldName & "='" & varDupVal & "'"
        End If

        Dim strQryValue
        strQryValue = ExecuteQueryReturnSingleValuenew(connstr, strSelectQry) 'Use Aggregate
        If strQryValue = "" Then
            isDuplicateForModifynew = 0
        Else
            isDuplicateForModifynew = 1
        End If
    End Function
    Public Sub WritErrorLog(ByVal PageName As String, ByVal strFileName As String, ByVal strErrorDescription As String, ByVal strUserName As String)
        'Open a file for writing
        'Get a StreamReader class that can be used to read the file
        Try
            Dim objStreamWriter As StreamWriter
            objStreamWriter = File.AppendText(strFileName)
            objStreamWriter.WriteLine(PageName & " || " & DateTime.Now.ToString() & " || " & strErrorDescription & " || " & strUserName)
            objStreamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------")
            objStreamWriter.Close()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub MessageBox(ByVal msg As String, ByVal page As Object)
        ScriptManager.RegisterClientScriptBlock(page, GetType(Page), "", "alert('" & msg & "'  );", True)

        'Dim lbl As New Label
        'lbl.Text = "<script language='javascript'>" & Environment.NewLine & _
        '       "window.alert('" + msg + "')</script>"
        'page.Controls.Add(lbl)
    End Sub

    Public Sub ConfirmMessageBox(ByVal msg As String, ByVal page As Object)

        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & _
               "if(confirm('" + msg + "')==false)return false;</script>"
        page.Controls.Add(lbl)
    End Sub
    Public Sub ExportToExcel(ByVal ds As DataSet, ByVal response As HttpResponse)
        Try
            'first let's clean up the response.object
            response.Clear()
            response.Charset = ""
            'set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel"
            response.AddHeader("content-disposition", " filename=Excel.xls")
            'create a string writer
            Dim stringWrite As New System.IO.StringWriter
            'create an htmltextwriter which uses the stringwriter
            Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
            'instantiate a datagrid
            Dim dg As New DataGrid
            'set the datagrid datasource to the dataset passed in
            dg.DataSource = ds.Tables(0)
            'bind the datagrid
            dg.DataBind()
            'tell the datagrid to render itself to our htmltextwriter
            dg.RenderControl(htmlWrite)
            'all that's left is to output the html
            response.Write(stringWrite.ToString)
            response.End()
            response.Clear()
            response.Charset = ""

        Catch ex As Exception
            ' response.Write(ex.ToString)
        End Try

    End Sub

    Public Sub ExportToExcelnew(ByVal ds As DataSet, ByVal response As HttpResponse, ByVal myexcel As String)
        Try
            '
            'first let's clean up the response.object
            response.Clear()
            response.Charset = ""
            'set the response mime type for excel
            response.ContentType = "application/vnd.ms-excel"
            response.AddHeader("content-disposition", " filename=" & myexcel & ".xls")
            'create a string writer
            Dim stringWrite As New System.IO.StringWriter
            'create an htmltextwriter which uses the stringwriter
            Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
            'instantiate a datagrid
            Dim dg As New DataGrid
            'set the datagrid datasource to the dataset passed in
            dg.DataSource = ds.Tables(0)
            'bind the datagrid
            dg.DataBind()
            'tell the datagrid to render itself to our htmltextwriter
            dg.RenderControl(htmlWrite)
            'all that's left is to output the html
            response.Write(stringWrite.ToString)
            response.End()
            response.Clear()
            response.Charset = ""

        Catch ex As Exception
            ' response.Write(ex.ToString)
        End Try

    End Sub
    Public Function ConvertSortDirectionToSql(ByVal strsortDireciton As SortDirection)
        Dim newSortDirection As String = Nothing
        Select Case (strsortDireciton)
            Case SortDirection.Ascending
                newSortDirection = "ASC"
            Case SortDirection.Descending
                newSortDirection = "DESC"
        End Select
        Return newSortDirection
    End Function

    Public Function SwapSortDirection(ByVal strsortDireciton As SortDirection)
        Dim newsortDireciton As SortDirection
        Select Case (strsortDireciton)
            Case SortDirection.Ascending
                newsortDireciton = SortDirection.Descending
            Case SortDirection.Descending
                newsortDireciton = SortDirection.Ascending
        End Select
        Return newsortDireciton
    End Function
    Public Sub FillDropDownListHTMLNEW(ByVal connstr As String, ByVal ddl As System.Web.UI.HtmlControls.HtmlSelect, ByVal TextFiledName As String, ByVal ValueFiledName As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False, Optional ByVal selecteditem As String = "")

        Try

            SqlConn = clsDBConnect.dbConnectionnew(connstr)  'Open   connection()
            myCommand = New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            ddl.DataSource = myDataReader
            ddl.DataTextField = TextFiledName
            ddl.DataValueField = ValueFiledName
            ddl.DataBind()
            If addNewFlag = True Then
                ddl.Items.Add("[Select]")
                ddl.Value = "[Select]"
            End If
            If selecteditem <> "" Then
                ddl.Value = selecteditem
            End If
        Catch ex As Exception

        Finally
            clsDBConnect.dbCommandClose(myCommand) 'Close command
            clsDBConnect.dbReaderClose(myDataReader) 'Close reader
            clsDBConnect.dbConnectionClose(SqlConn) 'Close  connection()
        End Try

    End Sub
    Public Sub FillDropDownListHTMLNEWForAll(ByVal connstr As String, ByVal ddl As System.Web.UI.HtmlControls.HtmlSelect, ByVal TextFiledName As String, ByVal ValueFiledName As String, ByVal strQry As String, Optional ByVal addNewFlag As Boolean = False, Optional ByVal selecteditem As String = "")

        Try

            SqlConn = clsDBConnect.dbConnectionnew(connstr)  'Open   connection()
            myCommand = New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            ddl.DataSource = myDataReader
            ddl.DataTextField = TextFiledName
            ddl.DataValueField = ValueFiledName
            ddl.DataBind()
            If addNewFlag = True Then
                'ddl.Items.Add("[Select]")
                'ddl.Value = "[Select]"

                ddl.Items.Add("[All]")
                ddl.Value = "[All]"
            End If
            If selecteditem <> "" Then
                ddl.Value = selecteditem
            End If
        Catch ex As Exception

        Finally
            clsDBConnect.dbCommandClose(myCommand) 'Close command
            clsDBConnect.dbReaderClose(myDataReader) 'Close reader
            clsDBConnect.dbConnectionClose(SqlConn) 'Close  connection()
        End Try

    End Sub


    Public Function GetAutoDocNoyear(ByVal optionname As String, ByVal dcon As SqlConnection, ByVal dtran As SqlTransaction, ByVal docyear As String) As String
        Dim str As String
        str = ""
        GetAutoDocNoyear = ""
        If optionname <> "" Then
            Dim ds As DataSet
            ds = New DataSet
            'SqlConn = clsDBConnect.dbConnectionnew(connstr)
            'Dim comcls As Commoncls
            'comcls = New Commoncls
            myCommand = New SqlCommand("sp_getnumber", dcon, dtran)
            myCommand.CommandText = "sp_getnumber"
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Connection = dcon
            myCommand.Parameters.Add(New SqlParameter("@optionname", optionname))
            Dim param As SqlParameter
            param = New SqlParameter
            param.ParameterName = "@newno"
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.String
            param.Size = 50
            myCommand.Parameters.Add(param)
            myCommand.Parameters.Add(New SqlParameter("@cyear", docyear))
            myDataAdapter = New SqlDataAdapter(myCommand)
            myCommand.ExecuteNonQuery()
            str = param.Value
            myCommand = Nothing
            Return str
        Else
            str = ""
            Return str
        End If
    End Function

    Public Function GetAutoDocNo(ByVal optionname As String, ByVal dcon As SqlConnection, ByVal dtran As SqlTransaction) As String
        Dim str As String
        str = ""
        GetAutoDocNo = ""
        If optionname <> "" Then
            Dim ds As DataSet
            ds = New DataSet
            'SqlConn = clsDBConnect.dbConnectionnew(connstr)
            'Dim comcls As Commoncls
            'comcls = New Commoncls
            myCommand = New SqlCommand("sp_getnumber", dcon, dtran)
            myCommand.CommandText = "sp_getnumber"
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Connection = dcon
            myCommand.Parameters.Add(New SqlParameter("@optionname", optionname))
            Dim param As SqlParameter
            param = New SqlParameter
            param.ParameterName = "@newno"
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.String
            param.Size = 50
            myCommand.Parameters.Add(param)
            myDataAdapter = New SqlDataAdapter(myCommand)
            myCommand.ExecuteNonQuery()
            str = param.Value
            myCommand = Nothing
            Return str
        Else
            str = ""
            Return str
        End If
    End Function

    Public Function Getdocgen(ByVal optionname As String, ByVal dcon As SqlConnection, ByVal dtran As SqlTransaction) As String
        Dim str As String
        Dim str1 As String
        str = ""
        str1 = ""

        Getdocgen = ""
        If optionname <> "" Then
            Dim ds As DataSet
            ds = New DataSet
            'SqlConn = clsDBConnect.dbConnectionnew(connstr)
            'Dim comcls As Commoncls
            'comcls = New Commoncls
            myCommand = New SqlCommand("sp_docgen", dcon, dtran)
            myCommand.CommandText = "sp_docgen"
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Connection = dcon
            myCommand.Parameters.Add(New SqlParameter("@optionname", optionname))
            Dim param As SqlParameter
            param = New SqlParameter
            param.ParameterName = "@newno"
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.String
            param.Size = 10
            myCommand.Parameters.Add(param)
            Dim param1 As SqlParameter
            param1 = New SqlParameter
            param1.ParameterName = "@docprefix"
            param1.Direction = ParameterDirection.Output
            param1.DbType = DbType.String
            param1.Size = 5
            myCommand.Parameters.Add(param1)
            myDataAdapter = New SqlDataAdapter(myCommand)
            myCommand.ExecuteNonQuery()
            'str = Format(CType(param.Value, String), "0000")
            str = param.Value
            ' str = str1 + "/" + str
            myCommand = Nothing
            Return str
        Else
            str = ""
            Return str
        End If
    End Function
    Public Function GetAutoDocNoWTnew(ByVal constr As String, ByVal optionname As String) As String
        Dim str As String
        str = ""
        GetAutoDocNoWTnew = ""
        If optionname <> "" Then
            SqlConn = clsDBConnect.dbConnectionnew(constr)
            myCommand = New SqlCommand("sp_getnumber", SqlConn)
            myCommand.CommandText = "sp_getnumber"
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Connection = SqlConn
            myCommand.Parameters.Add(New SqlParameter("@optionname", optionname))
            Dim param As SqlParameter
            param = New SqlParameter
            param.ParameterName = "@newno"
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.String
            param.Size = 50
            myCommand.Parameters.Add(param)
            myDataAdapter = New SqlDataAdapter(myCommand)
            myCommand.ExecuteNonQuery()
            str = param.Value
            myCommand = Nothing
            Return str
        Else
            str = ""
            Return str
        End If
    End Function
    Public Function DeleteFile(ByVal filepath As String)
        If File.Exists(filepath) = True Then
            File.Delete(filepath)
        End If
        Return True
    End Function
    Public Function GetDBFieldValueExistnew(ByVal constr As String, ByVal strTableName As String, ByVal strKeyName As String, ByVal strKeyValue As String) As Boolean
        Dim strQry As String
        Dim strRes As String
        Try

            strQry = "SELECT  't' FROM " & strTableName & _
                     " WHERE " & strKeyName & "='" & strKeyValue & "'"
            strRes = ""
            strRes = ExecuteQueryReturnStringValuenew(constr, strQry)
            If Trim(strRes) <> "" Then
                GetDBFieldValueExistnew = True
            Else
                GetDBFieldValueExistnew = False
            End If
        Catch ex As Exception
            GetDBFieldValueExistnew = False
        Finally
        End Try
    End Function

    Public Function ExecuteQuerynew(ByVal connstr As String, ByVal storedProcedure As String, ByVal sqlParamList As List(Of SqlParameter)) As DataSet


        SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection

        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandText = storedProcedure
        myCommand.Connection = SqlConn
        Dim size As Integer
        Dim i As Integer
        size = sqlParamList.Count
        For i = 0 To size - 1
            myCommand.Parameters.Add(sqlParamList(i))
        Next
        myCommand.CommandTimeout = 0
        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        Try
            adapter.Fill(ds)
        Catch ex As Exception
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        Finally
            adapter.Dispose()
            ds.Dispose()
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
        Return ds
    End Function
    Public Function ExecuteQuerynew(ByVal connstr As String, ByVal storedProcedure As String) As DataSet


        SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection

        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandText = storedProcedure
        myCommand.Connection = SqlConn
        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        Try
            adapter.Fill(ds)
        Catch ex As Exception
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        Finally
            adapter.Dispose()
            ds.Dispose()
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
        Return ds
    End Function
    Public Function ExecuteQuerySqlnew(ByVal connstr As String, ByVal sql As String) As DataSet

        SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection

        myCommand = New SqlCommand(sql, SqlConn)
        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        Try
            adapter.Fill(ds)
        Catch ex As Exception
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        Finally
            adapter.Dispose()
            ds.Dispose()
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
        Return ds
    End Function
    Public Function ExecuteNonQuerynew(ByVal connstr As String, ByVal storedProcedure As String, ByVal sqlParamList As List(Of SqlParameter)) As Integer
        SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection
        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandText = storedProcedure
        myCommand.Connection = SqlConn
        Dim size As Integer
        Dim i As Integer
        size = sqlParamList.Count
        For i = 0 To size - 1
            myCommand.Parameters.Add(sqlParamList(i))
        Next
        Dim Norows As New Integer
        Try
            Norows = myCommand.ExecuteNonQuery()

        Catch ex As Exception
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
        Return Norows
    End Function
    Public Function ExecuteNonQuerynew(ByVal constr As String, ByVal storedProcedure As String, ByVal sqlParamList As List(Of SqlParameter), ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As Integer
        'Open connection
        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandText = storedProcedure
        myCommand.Connection = conn
        myCommand.Transaction = trans
        Dim size As Integer
        Dim i As Integer
        size = sqlParamList.Count
        For i = 0 To size - 1
            myCommand.Parameters.Add(sqlParamList(i))
        Next
        Dim Norows As New Integer

        Norows = myCommand.ExecuteNonQuery()

        Return Norows
    End Function

    Public Function ExecuteNonQuerynew(ByVal constr As String, ByVal strSql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As Boolean
        'Open connection
        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.Text
        myCommand.CommandText = strSql
        myCommand.Connection = conn
        myCommand.Transaction = trans

        Dim Norows As New Integer

        Norows = myCommand.ExecuteNonQuery()
        If (Norows < 0) = True Then
            ExecuteNonQuerynew = False
            Exit Function
        End If

        Return True
    End Function

    Public Function IsNumber(ByVal strDigit As String) As Boolean
        If strDigit.Trim().Length = 0 Then
            Return False
        End If

        Dim objRegex As Regex = New Regex("^[0-9]*$")
        Return objRegex.IsMatch(strDigit)
    End Function


    Public Function IsDecimal(ByVal strDigit As String) As Boolean
        If strDigit.Trim().Length = 0 Then
            Return False
        End If

        Dim objRegex As Regex = New Regex("^[0-9]*[.,]?[0-9]*$")
        Return objRegex.IsMatch(strDigit)
    End Function
    Public Function FillArraynew(ByVal connstr As String, ByVal strQry As String, ByVal cntArray As Long) As String()
        Dim ResultArray(cntArray) As String
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr) 'Open   connection()
            myCommand = New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            If myDataReader.HasRows Then
                Dim i As Integer = 0
                Do While myDataReader.Read
                    ' ResultArray(i) = CType(myDataReader.GetValue(i).ToString(), String)
                    For i = 0 To cntArray - 1
                        ResultArray(i) = CType(myDataReader.GetValue(i).ToString(), String)
                        '  i = i + 1
                    Next

                Loop
            End If

        Catch ex As Exception

        Finally
            clsDBConnect.dbCommandClose(myCommand) 'Close command
            clsDBConnect.dbReaderClose(myDataReader) 'Close reader
            clsDBConnect.dbConnectionClose(SqlConn) 'Close  connection()
        End Try
        Return ResultArray

    End Function

    Public Function ValidateEmail(ByVal strEmail As String) As Boolean
        If strEmail.Trim().Length = 0 Then
            Return False
        End If
        Dim objRegex As Regex = New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        Return objRegex.IsMatch(strEmail)
    End Function

    Public Function DeleteRowForBackToMainnew(ByVal connstr As String, ByVal requestid As String, ByVal rlineno As String, ByVal basketid As String, ByVal requesttype As String) As Boolean
        SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection
        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandText = "sp_request_backtomainpage"
        myCommand.Connection = SqlConn
        Dim size As Integer = 3
        Dim i As Integer
        Dim res As Boolean = True
        Dim parm(4) As SqlParameter
        parm(0) = New SqlParameter("@requestid", requestid)
        parm(1) = New SqlParameter("@rlineno", rlineno)
        parm(2) = New SqlParameter("@basketid", basketid)
        parm(3) = New SqlParameter("@requesttype", requesttype)
        For i = 0 To size
            myCommand.Parameters.Add(parm(i))
        Next
        Dim Norows As New Integer
        Try
            Norows = myCommand.ExecuteNonQuery()
            res = True

        Catch ex As Exception
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
            res = False
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
        Return res
    End Function

    Public Function RoundwithParameternew(ByVal connstr As String, ByVal numbertoround As Decimal) As Decimal
        Dim strsql As String = ""
        Dim roundednumber As Decimal = 0
        strsql = "select dbo.roundwithparameter(" & numbertoround & ")"
        roundednumber = CType(ExecuteQueryReturnSingleValuenew(connstr, strsql), Decimal)
        Return roundednumber
    End Function
    Public Sub ExecuteNonQuerynewProc(ByVal connstr As String, ByVal storedProcedure As String, ByVal sqlParamList As List(Of SqlParameter))
        SqlConn = clsDBConnect.dbConnectionnew(connstr)                     'Open connection
        myCommand = New SqlCommand()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.CommandText = storedProcedure
        myCommand.Connection = SqlConn
        Dim size As Integer
        Dim i As Integer
        size = sqlParamList.Count
        For i = 0 To size - 1
            myCommand.Parameters.Add(sqlParamList(i))
        Next
        Try
            myCommand.ExecuteNonQuery()

        Catch ex As Exception
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Sub
    Public Function MyCodeReturn(ByVal MyPrefix As String, ByVal MyIdentity As Integer, ByVal MyLength As Integer) As String
        Try
            Dim MyString As String = String.Empty
            MyString = MyPrefix & Right("00000000" & MyIdentity, MyLength)
            Return MyString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Function CodeReturn(ByVal MyIdentity As Integer, ByVal MyLength As Integer) As String
        Try
            Dim MyString As String = String.Empty
            MyString = Right("00000000" & MyIdentity, MyLength)
            Return MyString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Function GetString(ByVal connstr As String, ByVal strQuery As String) As String
        Dim objAgrValue As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dr As DataRow
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                    'Open connection
            'EventLog.WriteEntry("websrv", "qry:" & strQuery)
            myCommand = New SqlCommand(strQuery, SqlConn)
            'objAgrValue = myCommand.ExecuteNonQuery()
            da = New SqlDataAdapter(myCommand)
            da.Fill(ds)
            dt = ds.Tables(0)

            If IsDBNull(dt.Rows(0).Item(0)) = False Then
                GetString = dt.Rows(0).Item(0)
            Else
                GetString = ""
            End If
        Catch ex As Exception
            GetString = ""
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Function

    Public Function CheckString(ByVal connstr As String, ByVal strQuery As String) As String
        Dim objAgrValue As String
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dr As DataRow
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr)                    'Open connection
            'EventLog.WriteEntry("websrv", "qry:" & strQuery)
            myCommand = New SqlCommand(strQuery, SqlConn)
            'objAgrValue = myCommand.ExecuteNonQuery()
            da = New SqlDataAdapter(myCommand)
            da.Fill(ds)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0).Item(0)) = False Then
                    CheckString = dt.Rows(0).Item(0)
                Else
                    CheckString = ""
                End If
            Else
                CheckString = ""
            End If
        Catch ex As Exception
            CheckString = ""
        Finally
            clsDBConnect.dbCommandClose(myCommand)                  'Close command 
            clsDBConnect.dbConnectionClose(SqlConn)                 'Close connection
        End Try
    End Function

    Public Function GenerateXML(ByVal ds As DataSet) As String
        Dim obj As New StringWriter()
        Dim xmlstring As String
        ds.WriteXml(obj)
        xmlstring = obj.ToString()
        Return xmlstring
    End Function

    Public Function GenerateXML(ByVal dt As DataTable) As String
        Dim obj As New StringWriter()
        Dim xmlstring As String
        dt.WriteXml(obj)
        xmlstring = obj.ToString()
        Return xmlstring
    End Function


    'Changed by Mohamed on 25/07/2016
    Public Function FillStringArray(ByVal connstr As String, ByVal strQry As String) As List(Of String)
        Dim SqlConn As SqlConnection
        Dim myDataReader As SqlDataReader
        Dim myCommand As SqlCommand
        Dim masterlist As New List(Of String)
        Try
            SqlConn = clsDBConnect.dbConnectionnew(connstr) 'Open   connection()
            myCommand = New SqlCommand(strQry, SqlConn)
            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            If myDataReader.HasRows Then
                Dim i As Integer = 0
                Do While myDataReader.Read
                    masterlist.Add(myDataReader.GetValue(0))
                    i = i + 1
                Loop
            End If

        Catch ex As Exception

        Finally
            clsDBConnect.dbCommandClose(myCommand) 'Close command
            clsDBConnect.dbReaderClose(myDataReader) 'Close reader
            clsDBConnect.dbConnectionClose(SqlConn) 'Close  connection()
        End Try
        Return masterlist

    End Function

    'Changed by Mohamed on 25/07/2016
    Function splitWithWords(ByVal asSplitString As String, ByVal asSplitWord As String) As String()
        Try

            Dim aSplit As New List(Of String)
            Dim liStartPos As Integer = 1
            Dim liEndPos As Integer = 1
            Dim lsStr = asSplitString
            If asSplitString.Trim = "" Then
                GoTo retpos
            End If
            Do
                If liStartPos > lsStr.Length Then
                    GoTo retpos
                End If
                liEndPos = lsStr.IndexOf(asSplitWord, liStartPos)
                If liEndPos > 0 Then
                    aSplit.Add(Mid(asSplitString, liStartPos, liEndPos - liStartPos + 1))
                    liStartPos = liEndPos + asSplitWord.Length + 1
                Else
                    aSplit.Add(Mid(asSplitString, liStartPos))
                    Exit Do
                End If
            Loop
retpos:
            Return aSplit.ToArray()
        Catch ex As Exception
            Dim aSplit As New List(Of String)
            Return aSplit.ToArray()
        End Try
    End Function

    'Changed by Mohamed on 25/07/2016
    Function sbSetSelectedValueForHTMLSelect(ByVal asSelectString As String, ByVal aObject As HtmlSelect)
        For i As Integer = 0 To aObject.Items.Count - 1
            If Trim(UCase(aObject.Items(i).Text)) = Trim(UCase(asSelectString)) Then
                aObject.SelectedIndex = i
                Exit For
            End If
        Next
    End Function

    Function fnGridViewRowToDataRow(ByVal gvr As GridViewRow) As DataRow
        Dim di As Object = Nothing
        Dim drv As DataRowView = Nothing
        Dim dr As DataRow = Nothing

        If gvr IsNot Nothing Then
            di = TryCast(gvr.DataItem, System.Object)
            If di IsNot Nothing Then
                drv = TryCast(di, System.Data.DataRowView)
                If drv IsNot Nothing Then
                    dr = TryCast(drv.Row, System.Data.DataRow)
                End If
            End If
        End If

        Return dr
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pageIndex"></param>
    ''' <param name="pageSize"></param>
    ''' <param name="strQuery"></param>
    ''' <param name="strConnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDetailsPageWise(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByVal strQuery As String) As DataSet
        Dim strConnName As String = (HttpContext.Current.Session("dbconnectionName")).ToString
        Dim constring As String = ConfigurationManager.ConnectionStrings(strConnName).ConnectionString
        Using con As New SqlConnection(constring)
            Using cmd As New SqlCommand("[GetDetailsPageWise]")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex)
                cmd.Parameters.AddWithValue("@PageSize", pageSize)
                cmd.Parameters.AddWithValue("@SqlQuery", strQuery)
                cmd.Parameters.Add("@PageCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using ds As New DataSet()
                        sda.Fill(ds, "Customers")
                        Dim dt As New DataTable("PageCount")
                        dt.Columns.Add("PageCount")
                        dt.Rows.Add()
                        dt.Rows(0)(0) = cmd.Parameters("@PageCount").Value
                        ds.Tables.Add(dt)
                        Return ds
                    End Using
                End Using
            End Using
        End Using
    End Function


    Public Function getdataset(ByVal sqlstr As String, ByRef scon As SqlConnection, ByRef stran As SqlTransaction) As DataSet
        Dim ds As DataSet
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        ds = New DataSet
        Try
            If Not sqlstr = Nothing Then
                cmd = New SqlCommand(sqlstr, scon, stran)
                da = New SqlDataAdapter(cmd)
                da.Fill(ds)
                cmd = Nothing
            End If
        Catch ex As Exception
            '            MsgBox(ex.Message)
        End Try
        Return ds
    End Function

    Public Function getdatatable(ByVal sqlstr As String, ByRef scon As SqlConnection, ByRef stran As SqlTransaction) As DataTable
        Dim dt As DataTable
        dt = New DataTable
        Try
            If Not sqlstr = Nothing Then
                Dim ds As DataSet
                ds = New DataSet
                ds = getdataset(sqlstr, scon, stran)
                dt = ds.Tables(0)
                ds = Nothing
            End If
        Catch ex As Exception
            '           MsgBox(ex.Message)
        End Try
        Return dt
    End Function


    ''' <summary>
    ''' Function to execute a Stored Procedure
    ''' </summary>
    ''' <param name="spName">SQL Command Text</param>
    ''' <returns>DataTable filled with the queried Results</returns>
    ''' <remarks></remarks>
    Public Function GetDataTable(ByVal spName As String, ByVal ParamArray sqlParams() As SqlParameter) As DataTable

        _strConn = getConnectionString()
        Dim dataTable As DataTable = Nothing
        Dim filledRows As Integer = 0
        If spName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
            Using sqlCn As New SqlConnection(_strConn)
                Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                    With sqlCmd
                        .CommandText = spName
                        .CommandType = CommandType.StoredProcedure
                        .Connection = sqlCn
                        If (sqlParams IsNot Nothing) AndAlso (sqlParams.Length > 0) Then .Parameters.AddRange(sqlParams)
                    End With
                    Using sqlDa As New SqlDataAdapter(sqlCmd)
                        Try
                            dataTable = New DataTable
                            filledRows = sqlDa.Fill(dataTable)
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                WriteErrorLog(sqlE.Message.ToString & " :: " & Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            dataTable = Nothing
                        Catch ex As Exception
                            WriteErrorLog(ex.Message.ToString & " :: " & Reflection.MethodBase.GetCurrentMethod.Name)
                            dataTable = Nothing

                        Finally
                            sqlCmd.Parameters.Clear()
                            spName = String.Empty
                            sqlParams = Nothing
                            sqlCn.Close()
                            sqlCn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
        Else
            Return dataTable
        End If
        Return dataTable
    End Function

    Public Sub WriteErrorLog(ByVal StrMsgText As String)
        Try
            Dim SW As StreamWriter
            Dim StrErrTime As String = ""
            Dim StrFileName As String = ""
            StrFileName = Path & Format(Today.Year, "0000") & Format(Today.Month, "00") & Format(Today.Day, "00") & ".SV"
            If Not Directory.Exists(Path) Then
                Directory.CreateDirectory(Path)
            End If
            StrErrTime = Format(DateTime.Now) ' & ":" & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00") & ":" & Format(Now.Second, "00")
            If Dir(StrFileName) = "" Then SW = File.CreateText(StrFileName) Else SW = File.AppendText(StrFileName)
            SW.Write(StrErrTime & ":" & StrMsgText)
            SW.Write(vbNewLine)
            SW.Close()
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetDataSet(ByVal spName As String, ByVal ParamArray sqlParams() As SqlParameter) As DataSet
        Dim dataSet As DataSet = Nothing
        _strConn = getConnectionString()

        Dim filledRows As Integer = 0
        If spName.Trim.Length > 0 AndAlso _strConn.Trim.Length > 0 Then
            Using sqlCn As New SqlConnection(_strConn)
                Using sqlCmd As SqlCommand = sqlCn.CreateCommand
                    With sqlCmd
                        .CommandText = spName
                        .CommandType = CommandType.StoredProcedure
                        .Connection = sqlCn
                        If (sqlParams IsNot Nothing) AndAlso (sqlParams.Length > 0) Then .Parameters.AddRange(sqlParams)
                    End With
                    Using sqlDa As New SqlDataAdapter(sqlCmd)
                        Try
                            dataSet = New DataSet
                            filledRows = sqlDa.Fill(dataSet)
                        Catch sqlEx As SqlException
                            For Each sqlE As SqlError In sqlEx.Errors
                                WriteErrorLog(sqlE.Message.ToString & " :: " & Reflection.MethodBase.GetCurrentMethod.Name)
                            Next
                            dataSet = Nothing
                        Catch ex As Exception
                            WriteErrorLog(ex.Message.ToString & " :: " & Reflection.MethodBase.GetCurrentMethod.Name)
                            dataSet = Nothing
                        Finally
                            sqlCmd.Parameters.Clear()
                            spName = String.Empty
                            sqlParams = Nothing
                            sqlCn.Close()
                            sqlCn.Dispose()
                        End Try
                    End Using
                End Using
            End Using
        Else
            Return dataSet
        End If
        Return dataSet
    End Function

    Public Shared Function getConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
    End Function
End Class