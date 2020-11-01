Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Public Class DALHotelConfirm
    Public HBrequestid As String = ""
    Public HBRlineno As String = ""
    Public HBRoomno As String = ""
    Public HBConfirmationNo As String = ""
    Public HBConfirmBy As String = ""
    Public HBConfirmationDate As String = ""

    Dim objclsUtilities As New clsUtilities
    Dim objClsUtils As New clsUtils
    Function SavingBookingHotelConfirmation(objList As List(Of BLLHotelConfirm)) As String
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
        mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
        'connection open
        mysqlTrans = mySqlConn.BeginTransaction
        Try
            For Each objIn As BLLHotelConfirm In objList
                Dim ProcName As String
                ProcName = "sp_booking_hotel_Confirmation"
                Dim sqlParamList As New List(Of SqlParameter)
                Dim parm(5) As SqlParameter
                parm(0) = New SqlParameter("@RequestID", CType(objIn.HBRequestID, String))
                sqlParamList.Add(parm(0))
                parm(1) = New SqlParameter("@RLineNo", CType(objIn.HBRlineNo, Integer))
                sqlParamList.Add(parm(1))
                parm(2) = New SqlParameter("@RoomNo", CType(objIn.HBRoomNo, Integer))
                sqlParamList.Add(parm(2))
                parm(3) = New SqlParameter("@HotelConfNo", CType(objIn.HBConfirmationNo, String))
                sqlParamList.Add(parm(3))
                parm(4) = New SqlParameter("@ConfirmBy", CType(objIn.HBConfirmBy, String))
                sqlParamList.Add(parm(4))
                parm(5) = New SqlParameter("@confirmdate", CType(objIn.HBConfirmationDate, String))
                sqlParamList.Add(parm(5))
                objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            Next
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception
            mysqlTrans.Rollback()
            objclsUtilities.WriteErrorLog("DALHotelConfirm :: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ex.Message().ToString()
        End Try
        Return False
    End Function

    Function FetchBookingHotelConfirmation() As String
        Try
            Dim strQry As String
            strQry = "select * from booking_hotel_detail_confcancel where requestid='" & CType(HBrequestid, String) & "' and rlineno=" & CType(HBRlineno, String) & " and roomno=" & CType(HBRoomno, String) & " and hotelConfNo <>'WEBCONF' and isnull(hotelconfno,'') <>''"
            Dim myDataTable As DataTable = objclsUtilities.GetDataFromDataTable(strQry)
            If Not myDataTable Is Nothing Then
                Dim dr As DataRow = myDataTable.Rows(0)
                HBConfirmationNo = Convert.ToString(dr("hotelConfNo"))
                HBConfirmBy = Convert.ToString(dr("confirmBy"))
                HBConfirmationDate = Convert.ToString(dr("confirmDate"))
                Return True
            End If
            Return False
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHotelConfirm :: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ex.Message().ToString()
        End Try
    End Function
End Class
