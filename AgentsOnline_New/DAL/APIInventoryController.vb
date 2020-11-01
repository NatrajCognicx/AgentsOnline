Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.IO
Imports System.Data

Public Class APIInventoryController



    'Public Class Available
    '    Public Property Date As String
    '    Public Property Timeslot_From As String
    '    Public Property Timeslot_To As String
    '    Public Property Availability As Integer
    'End Class



    'Public Class BookingResponse
    '    Public Property Message As String
    '    Public Property ConfirmationNo As String
    'End Class

    'Public Class CancelResponse
    '    Public Property Message As String
    '    Public Property CancellationNo As String
    'End Class



    Function GetTimeSlot(AgentCode As String, CountryCode As String, ExcCode As String, FromDate As String, ToDate As String, RequestId As String, ELineNo As String, InventoryId As String) As Data.DataTable
        If FromDate <> "" Then
            Dim strDates As String() = FromDate.Split("/")
            FromDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        If ToDate <> "" Then
            Dim strDates As String() = ToDate.Split("/")
            ToDate = strDates(2) & "/" & strDates(1) & "/" & strDates(0)
        End If
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        Dim objAvailabilityRequest As APIInventoryAvailability.AvailabilityRequest = New APIInventoryAvailability.AvailabilityRequest()
        Dim objAvailabilityResponse As APIInventoryAvailability.AvailabilityResponse = New APIInventoryAvailability.AvailabilityResponse()
        objAvailabilityRequest.AgentID = AgentCode
        objAvailabilityRequest.SourceCountry = CountryCode
        objAvailabilityRequest.ExcursionCode = ExcCode
        objAvailabilityRequest.ExcursionFromDate = FromDate
        objAvailabilityRequest.ExcursionToDate = ToDate
        objAvailabilityRequest.BookingID = RequestId
        objAvailabilityRequest.ELineNo = ELineNo
        objAvailabilityRequest.InventoryID = InventoryId


        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "available"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objAvailabilityRequest)

        objclsUtilities.WriteAPILog("ApiController::InventoryAvailabilityRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "available", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()

        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventoryAvailabilityResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "available", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objAvailabilityResponse = serializer.Deserialize(Of APIInventoryAvailability.AvailabilityResponse)(strsb)

        End Using
        'Dim Path As String = ConfigurationManager.AppSettings("AppLogs").ToString
        'Dim strsb As String = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Logs/AvailabilityResponse.json"))
        'Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        'objAvailabilityResponse = serializer.Deserialize(Of APIInventoryAvailability.AvailabilityResponse)(strsb)

        Dim dtTimeSlot As DataTable = CreateTimeSlotDataTable()
        If objAvailabilityResponse.STATUS = "200" And objAvailabilityResponse.MESSAGE <> "No Inventory Available" Then
            For Each obj In objAvailabilityResponse.Available
                If obj.AVAILABILITY > 0 Then
                    Dim row As DataRow = dtTimeSlot.NewRow()
                    row("EXC_CODE") = ExcCode
                    row("TIMESLOT_FROM") = obj.TIMESLOT_FROM
                    row("TIMESLOT_TO") = obj.TIMESLOT_TO
                    row("TIMESLOT") = obj.TIMESLOT_FROM & " - " & obj.TIMESLOT_TO
                    row("DATE") = obj.DATE
                    row("AVAILABILITY") = obj.AVAILABILITY
                    dtTimeSlot.Rows.Add(row)
                End If

            Next
        End If


      

        Return dtTimeSlot

        'Catch e As Exception
        '    objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
        '    Return Nothing
        'End Try
    End Function
    Function CreateTimeSlotDataTable() As DataTable
        Dim table As New DataTable
        table.Columns.Add("EXC_CODE", GetType(String))
        table.Columns.Add("TIMESLOT_FROM", GetType(String))
        table.Columns.Add("TIMESLOT_TO", GetType(String))
        table.Columns.Add("TIMESLOT", GetType(String))
        table.Columns.Add("DATE", GetType(DateTime))
        table.Columns.Add("AVAILABILITY", GetType(Integer))


        ' requestid	elineno	clineno	exctypcode	vehiclecode	exctypcombocode	excdate	type	TIMESLOT_FROM	TIMESLOT_TO	TIMESLOT	ADULT	CHILD	SENIOR	TEMP_CANCEL_NO	TEMP_CANCEL_DATE	TEMP_CONF_NO	TEMP_CONF_DATE	CANCEL_NO	CANCEL_DATE	CONF_NO	CONF_DATE	TEMP_ID	INVENTORY_ID

        'table.Columns.Add("TEMP_CANCEL_NO", GetType(String))
        'table.Columns.Add("TEMP_CANCEL_DATE", GetType(String))
        'table.Columns.Add("TEMP_CONF_NO", GetType(String))
        'table.Columns.Add("TEMP_CONF_DATE", GetType(String))

        'table.Columns.Add("CANCEL_NO", GetType(String))
        'table.Columns.Add("CANCEL_DATE", GetType(String))
        'table.Columns.Add("CONF_NO", GetType(String))
        'table.Columns.Add("CONF_DATE", GetType(String))

        Return table
    End Function

    Function SaveAllotment(objSaveAllotment As APIInventoryConfirm.SaveAllotment) As APIInventoryConfirm.SaveAllotmentResponse
        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "saveAllotment"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objSaveAllotment)
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        objclsUtilities.WriteAPILog("ApiController::InventorysaveAllotmentRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "saveAllotment", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()
        Dim objSaveAllotmentResponse As New APIInventoryConfirm.SaveAllotmentResponse
        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventorysaveAllotmentResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "saveAllotment", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objSaveAllotmentResponse = serializer.Deserialize(Of APIInventoryConfirm.SaveAllotmentResponse)(strsb)

        End Using
        Return objSaveAllotmentResponse
    End Function

    Function SubmitExcursionInventory(objSubmitBooking As APIInventoryConfirm.SubmitBooking) As APIInventoryConfirm.SubmitBookingResponse
        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "booking"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objSubmitBooking)
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        objclsUtilities.WriteAPILog("ApiController::InventoryBookingRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "Booking", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()
        Dim objSubmitBookingResponse As New APIInventoryConfirm.SubmitBookingResponse
        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventoryBookingResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "Booking", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objSubmitBookingResponse = serializer.Deserialize(Of APIInventoryConfirm.SubmitBookingResponse)(strsb)

        End Using
        Return objSubmitBookingResponse
    End Function

    Function CancelExcursionInventory(objCancelBooking As APIInventoryCancel.CancelBooking) As APIInventoryCancel.CancelBookingResponse
        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "cancel"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objCancelBooking)
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        objclsUtilities.WriteAPILog("ApiController::InventoryCancelRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "Cancel", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()
        Dim objCancelBookingResponse As New APIInventoryCancel.CancelBookingResponse
        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventoryCancelResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "Cancel", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objCancelBookingResponse = serializer.Deserialize(Of APIInventoryCancel.CancelBookingResponse)(strsb)

        End Using
        Return objCancelBookingResponse
    End Function

    Function AmendInvetory(objAmendRequest As APIInventoryAmend.AmendRequest) As APIInventoryAmend.AmendResponse
        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "Amend"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objAmendRequest)
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        objclsUtilities.WriteAPILog("ApiController::InventoryAmendRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "Amend", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()
        Dim objAmendResponse As New APIInventoryAmend.AmendResponse
        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventoryAmendResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "Amend", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objAmendResponse = serializer.Deserialize(Of APIInventoryAmend.AmendResponse)(strsb)

        End Using
        Return objAmendResponse
    End Function
    Function EditInvetory(objAmendRequest As APIInventoryAmend.EditRequest) As APIInventoryAmend.AmendResponse
        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "Amend"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objAmendRequest)
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        objclsUtilities.WriteAPILog("ApiController::InventoryAmendRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "Amend", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()
        Dim objAmendResponse As New APIInventoryAmend.AmendResponse
        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventoryAmendResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "Amend", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objAmendResponse = serializer.Deserialize(Of APIInventoryAmend.AmendResponse)(strsb)

        End Using
        Return objAmendResponse
    End Function

    Function FinalSaveExcursionInventory(objFinalSaveRequest As APIInventoryConfirm.FinalSaveRequest) As APIInventoryConfirm.FinalSaveResponse
        Dim strAPIInventoryURL As String = ConfigurationManager.AppSettings("APIInventoryURL").ToString()
        Dim request As HttpWebRequest = TryCast(WebRequest.Create(strAPIInventoryURL & "saveFinal"), HttpWebRequest)
        Dim sb As String = New JavaScriptSerializer().Serialize(objFinalSaveRequest)
        Dim objclsUtilities As clsUtilities = New clsUtilities()
        objclsUtilities.WriteAPILog("ApiController::InventorysaveFinalRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "saveFinal", "Request")

        request.Method = "POST"
        request.ContentType = "application/json"
        Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
        Dim st As Stream = request.GetRequestStream()
        st.Write(bt, 0, bt.Length)
        st.Close()
        Dim objFinalSaveResponse As New APIInventoryConfirm.FinalSaveResponse
        Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
            If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
            Dim stream1 As Stream = response.GetResponseStream()

            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim sr As StreamReader = New StreamReader(stream1, encode)
            Dim strsb As String = sr.ReadToEnd()

            objclsUtilities.WriteAPILog("ApiController::InventorysaveFinalResponse" & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "saveFinal", "Response")
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            objFinalSaveResponse = serializer.Deserialize(Of APIInventoryConfirm.FinalSaveResponse)(strsb)

        End Using
        Return objFinalSaveResponse
    End Function

End Class
