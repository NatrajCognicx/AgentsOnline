Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DALCommonFuntions
    Dim objclsUtilities As New clsUtilities
    ''' <summary>
    ''' GetRoomAdultAndChildDetails
    ''' </summary>
    ''' <param name="requestid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRoomAdultAndChildDetails(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetRoomAdultAndChildDetails"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function AmendGuestServices(ByVal strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select * from vw_Amendguestservices where requestid='" & strRequestId & "' "


            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetAllServices(ByVal strRequestId As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select * from vw_showservices_new where requestid='" & strRequestId & "'"
            strQuery += " and isnull(cancelled,0)=0"  'changed by mohamed on 16/09/2018

            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetAllServiceswithdates(ByVal strRequestId As String, ByVal checkindate As String, ByVal checkoutdate As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""

            'changed by mohamed on 15/09/2018
            strQuery = "select * from vw_showservices_new where requestid='" & strRequestId & "' and convert(varchar(10),servicedate,111) between '" & checkindate & "' and  '" & checkoutdate & "'  and isnull(cancelled,0)=0 "

            'strQuery = "select * from vw_showservices where requestid='" & strRequestId & "' and "
            'strQuery += " (convert(varchar(10),servicedate,111) between '" & checkindate & "' and  '" & checkoutdate & "' or (convert(varchar(10),servicedate,111)=dateadd(day,-1,'" & checkindate & "') and servicetype like '%ARRIVAL%') or (convert(varchar(10),servicedate,111)=dateadd(day,+1,'" & checkoutdate & "') and servicetype like '%DEPARTURE%'))" 'modified by abin on 20190416
            'strQuery += " and ((convert(varchar(10),servicedate,111)<>'" & checkoutdate & "' and servicetype like '%ARRIVAL%') or servicetype not like '%ARRIVAL%') "
            'strQuery += " and isnull(cancelled,0)=0 " 'changed by mohamed on 16/09/2018

            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetBookingTempHeaderDetails(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingTempHeaderDetails"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetBookingvalue(ByVal requestid As String, ByVal strWhiteLabel As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingvalue"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@WhiteLabel", CType(strWhiteLabel, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingRoomstring(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingRoomstring"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function Getflightdetails(ByVal requestid As String, ByVal Flighttype As String, Optional ByVal mode As Integer = 0) As DataTable
        Try
            Dim ProcName As String
            ' ProcName = "Getflightdetails"
            ProcName = "Getflightdetails_new"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@Flighttype", CType(Flighttype, String))
            parm(2) = New SqlParameter("@mode", CType(mode, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetBookingGuestnames(ByVal requestid As String, Optional ByVal mode As Integer = 0) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingGuestnames"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@mode", CType(mode, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function


    Function GetBookingGuestnames_new(ByVal requestid As String, Optional ByVal mode As Integer = 0, Optional ByVal Type As String = "") As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingGuestnames_new"
            Dim parm(2) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@mode", CType(mode, String))
            parm(2) = New SqlParameter("@type", CType(Type, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetOthersGuestnames(ByVal requestid As String, Optional ByVal mode As Integer = 0) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetOthersGuestnames"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            parm(1) = New SqlParameter("@mode", CType(mode, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingGuestnames_arrival(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingGuestnames_arrival"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetOtherGuestnames_arrival(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetOtherGuestnames_arrival"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingGuestnames_departure(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetBookingGuestnames_departure"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetOtherGuestnames_departure(ByVal requestid As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "GetOtherGuestnames_departure"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetTempFullBookingDetails(ByVal requestid As String) As DataSet
        Try
            Dim ProcName As String
            ProcName = "GetTempFullBookingDetails"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim ds As New DataSet
            ds = objclsUtilities.GetDataSet(ProcName, parm)
            ds.Tables(0).TableName = "BOOKING_HEADER"
            ds.Tables(1).TableName = "HOTEL"
            ds.Tables(2).TableName = "TOUR"
            ds.Tables(3).TableName = "TRANSFER"
            ds.Tables(4).TableName = "MEET_AND_ASIST"
            ds.Tables(5).TableName = "OTHERS"
            ds.Tables(6).TableName = "PREARRANGED" 'changed by abin / mohamed on 08/04/2018
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetExistingBookingTypes(ByVal requestid As String) As String
        Try
            Dim ProcName As String
            Dim strBookingType As String = ""
            ProcName = "GetRoomAdultAndChildDetails"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            If dt.Rows.Count > 0 Then
                strBookingType = dt.Rows(0)("BookingType").ToString
            Else
                strBookingType = ""
            End If
            Return strBookingType
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetExistingBookingRequestId(ByVal requestid As String) As String
        Try
            Dim ProcName As String
            Dim strRequestId As String = ""
            ProcName = "GetRoomAdultAndChildDetails"
            Dim parm(0) As SqlParameter
            parm(0) = New SqlParameter("@requestid", CType(requestid, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            If dt.Rows.Count > 0 Then
                strRequestId = requestid
            Else
                strRequestId = ""
            End If
            Return strRequestId
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetBookingRowLineNo(strRequestId As String, strBookingType As String) As String
        Try
            Dim ProcName As String
            Dim strBookingRowLineNo As String = ""
            ProcName = "GetBookingRowLineNumber"
            Dim parm(1) As SqlParameter
            parm(0) = New SqlParameter("@RequestId", CType(strRequestId, String))
            parm(1) = New SqlParameter("@BookingType", CType(strBookingType, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            If dt.Rows.Count > 0 Then
                strBookingRowLineNo = dt.Rows(0)("RLineNo").ToString
            Else
                strBookingRowLineNo = ""
            End If
            Return strBookingRowLineNo
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetFooterAddress(strCompany As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select * from agentmast_whitelabel where randomnumber='" & strCompany & "'"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetBookingPreHotelRowLineNo(strRequestId As String) As String
        Try
            Return objclsUtilities.ExecuteQueryReturnSingleValue("select isnull(max(rlineno),0)+1 rlineno from booking_hotels_prearrangedtemp where requestid='" & strRequestId & "'")
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetBordercode(strRequestId As String, strType As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select distinct a.airportbordername airport,a.airportbordercode from  booking_airportmatemp(nolock) t,airportbordersmaster(nolock) a,flightmast(nolock) f  where requestid='" & strRequestId & "' and airportmatype='" & strType & "' and t.airportbordercode=a.airportbordercode union select distinct a.airportbordername airport,a.airportbordercode  from  booking_transferstemp (nolock) t,airportbordersmaster(nolock) a,flightmast(nolock) f where requestid='" & strRequestId & "' and transfertype='" & strType & "' and t.airportbordercode=a.airportbordercode "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SaveSearchLog(strAgentCode As String, strSubUserCode As String, strIpAddress As String, strSearchLocation As String, strSearchPage As String, strSearchServiceType As String, strSearchCriteria As String, strLoggedUser As String) As String
        Try
            Dim ProcName As String
            ProcName = "sp_AgentOnline_SearchLog"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parms(7) As SqlParameter
            parms(0) = New SqlParameter("@AgentCode", CType(strAgentCode, String))
            sqlParamList.Add(parms(0))
            parms(1) = New SqlParameter("@SubUserCode", CType(strSubUserCode, String))
            sqlParamList.Add(parms(1))
            parms(2) = New SqlParameter("@IPAddress", CType(strIpAddress, String))
            sqlParamList.Add(parms(2))
            parms(3) = New SqlParameter("@SearchLocation", CType(strSearchLocation, String))
            sqlParamList.Add(parms(3))
            parms(4) = New SqlParameter("@SearchPage", CType(strSearchPage, String))
            sqlParamList.Add(parms(4))
            parms(5) = New SqlParameter("@SearchServiceType", CType(strSearchServiceType, String))
            sqlParamList.Add(parms(5))
            parms(6) = New SqlParameter("@SearchCritieria", CType(strSearchCriteria, String))
            sqlParamList.Add(parms(6))
            parms(7) = New SqlParameter("@LoggedUser", CType(strLoggedUser, String))
            sqlParamList.Add(parms(7))
            Dim strStatus As String
            strStatus = objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)
            Return strStatus
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function fnValidateTotalPax(strRequestId As String, iTotalPax As Integer) As Integer

        Try
            Dim objclsUtilities As New clsUtilities()
            Dim ProcName As String
            ProcName = "sp_validate_totalpax"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parms(1) As SqlParameter
            parms(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            sqlParamList.Add(parms(0))
            parms(1) = New SqlParameter("@pax", CType(iTotalPax, String))
            sqlParamList.Add(parms(1))

            Dim strStatus As String
            strStatus = objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)
            Return strStatus
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try


    End Function

    Function GetTempHotelDetailsWithAPIDetails(ByVal strRequestId As String) As DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_get_booking_hotel_detail_api_temp"
            Dim param(0) As SqlParameter
            param(0) = New SqlParameter("@requestid", CType(strRequestId, String))
            Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, param)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function fnItineraryTokenGeneration(ByVal tokenId As String, ByVal strRequestId As String, ByVal loginType As String, ByVal loginUser As String) As String
        Dim SqlConn As New SqlConnection
        Try
            Dim finalTokenId As String = ""
            SqlConn = clsDBConnect.dbConnection()         'connection open
            Dim myCommand As SqlCommand = New SqlCommand("sp_add_ItineraryAccessRights", SqlConn)
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Parameters.Add(New SqlParameter("@tokenId", SqlDbType.VarChar, 300)).Value = CType(tokenId, String)
            myCommand.Parameters.Add(New SqlParameter("@requestId", SqlDbType.VarChar, 20)).Value = CType(strRequestId, String)
            myCommand.Parameters.Add(New SqlParameter("@loginType", SqlDbType.VarChar, 20)).Value = CType(loginType, String)
            myCommand.Parameters.Add(New SqlParameter("@loginUser", SqlDbType.VarChar, 100)).Value = CType(loginUser, String)
            Dim param As SqlParameter = New SqlParameter()
            param.ParameterName = "@finalTokenId"
            param.Direction = ParameterDirection.Output
            param.DbType = SqlDbType.VarChar
            param.Size = 300
            myCommand.Parameters.Add(param)
            myCommand.ExecuteNonQuery()
            finalTokenId = param.Value.ToString().Trim()
            clsDBConnect.dbCommandClose(myCommand)               'sql command disposed
            clsDBConnect.dbConnectionClose(SqlConn)
            Return finalTokenId
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
End Class
