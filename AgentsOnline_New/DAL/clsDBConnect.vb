Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class clsDBConnect
    Private Name As String
    Private Shared nameweb As String
    Dim Path As String = ConfigurationManager.AppSettings("AppLocation").ToString



    Public Shared Property webdb() As String
        Get
            Return nameweb
        End Get
        Set(ByVal Value As String)
            nameweb = Value
        End Set
    End Property


    Public Shared ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
        End Get
    End Property

    Public Shared Function dbConnection() As SqlConnection
        Dim SqlConn As New SqlConnection(ConnectionString())
        dbConnection = SqlConn
        If dbConnection.State = Data.ConnectionState.Open Then
            CType(dbConnection, IDisposable).Dispose()
        End If
        dbConnection.Open()
    End Function
    Public Shared Function dbConnectionnew(ByVal wbdb As String) As SqlConnection
        Dim SqlConn As New SqlConnection(ConnectionStringnew(wbdb))
        dbConnectionnew = SqlConn
        If dbConnectionnew.State = Data.ConnectionState.Open Then
            CType(dbConnectionnew, IDisposable).Dispose()
        End If
        dbConnectionnew.Open()
    End Function
    Public Shared ReadOnly Property ConnectionStringnew(ByVal dbcon As String) As String
        Get

            Return ConfigurationManager.ConnectionStrings(dbcon).ConnectionString

        End Get
    End Property

    Public Shared Sub dbConnectionClose(ByVal Sqlcon As SqlConnection)
        If Sqlcon.State = Data.ConnectionState.Open Then
            CType(Sqlcon, IDisposable).Dispose()
        End If
    End Sub
    Public Shared Sub dbReaderClose(ByVal myReader As SqlDataReader)
        CType(myReader, IDisposable).Dispose()
    End Sub
    Public Shared Sub dbCommandClose(ByVal myCommand As SqlCommand)
        CType(myCommand, IDisposable).Dispose()
    End Sub
    Public Shared Sub dbAdapterClose(ByVal myAdapter As SqlDataAdapter)
        CType(myAdapter, IDisposable).Dispose()
    End Sub
    Public Shared Sub dbDataSetClose(ByVal ds As DataSet)
        CType(ds, IDisposable).Dispose()
    End Sub
    Public Shared Sub dbSqlTransation(ByVal sqlTrans As SqlTransaction)
        CType(sqlTrans, IDisposable).Dispose()
    End Sub
   
End Class
