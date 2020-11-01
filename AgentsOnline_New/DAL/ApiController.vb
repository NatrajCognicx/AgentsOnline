Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization



Public Class ApiController

    Dim objclsUtilities As clsUtilities = New clsUtilities()


    Public Function MakeAPIRequest(ByVal requestUrl As String, ByVal JSONRequest As Object, ByVal JSONmethod As String, ByVal JSONContentType As String) As Object
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)
            Dim sb As String = New JavaScriptSerializer().Serialize(JSONRequest)
            request.Method = JSONmethod ' "POST";
            request.ContentType = JSONContentType ' "application/json";
            Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
            Dim st As Stream = request.GetRequestStream()
            st.Write(bt, 0, bt.Length)
            st.Close()

            Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode <> HttpStatusCode.OK Then Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
                '  DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                ' object objResponse = JsonConvert.DeserializeObject();
                Dim stream1 As Stream = response.GetResponseStream()
                Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                Dim sr As StreamReader = New StreamReader(stream1, encode)
                Dim strsb As String = sr.ReadToEnd()

                Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                ' Dim objresponse As APIStaticResponse = serializer.Deserialize(Of APIStaticResponse)(strsb)
                Dim objResponse As APIStaticResponse = New JavaScriptSerializer().Deserialize(Of APIStaticResponse)(strsb)
                '  objResponse = New JavaScriptSerializer().Deserialize(Of objResponse)(strsb)

                Return objResponse
            End Using

        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Return Nothing
        End Try
    End Function
    Public Function MakeAPISearchRequest(ByVal requestUrl As String, ByVal JSONRequest As Object, ByVal JSONmethod As String, ByVal JSONContentType As String) As Object
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)
            Dim sb As String = New JavaScriptSerializer().Serialize(JSONRequest)

            objclsUtilities.WriteAPILog("ApiController::SearchRequest " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & sb, "Search", "Request")

            request.Method = JSONmethod ' "POST";
            request.ContentType = JSONContentType ' "application/json";
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
                '     Dim strsbNew As String = Encoding.UTF8.GetString(sr.ReadToEnd(), 0, strsb.Length)
                objclsUtilities.WriteAPILog("ApiController::SearchResponse " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & strsb, "Search", "Reseponse")
                Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                Dim objresponse As APIHotelSearchResponse.HotelSearchResponse = serializer.Deserialize(Of APIHotelSearchResponse.HotelSearchResponse)(strsb)
                'Dim objResponse As APIStaticResponse = New JavaScriptSerializer().Deserialize(Of APIStaticResponse)(strsb)

                '  Dim objResponse As Object
                Return objresponse
            End Using

        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function CallHotelSearchAPI(ByVal objBLLHotelSearch As BLLHotelSearch) As Object

        Dim objDALHotelSearchNew As DALHotelSearch = New DALHotelSearch()
        Dim dsMapping As DataSet = objDALHotelSearchNew.GetMappingFields("OneDMC", objBLLHotelSearch.SourceCountryCode, objBLLHotelSearch.MealPlan, objBLLHotelSearch.PropertyType, objBLLHotelSearch.HotelCode, objBLLHotelSearch.StarCategoryCode, objBLLHotelSearch.DestinationCode, objBLLHotelSearch.DestinationType)
        Dim objResult As Object
        If dsMapping.Tables(1).Rows.Count > 0 Then
            objResult = Nothing
            Return objResult
        End If

        Dim objAPIHotelSearchRequest As APIHotelSearchRequest = New APIHotelSearchRequest()

        Dim CheckIn As String = objBLLHotelSearch.CheckIn
        Dim CheckOut As String = objBLLHotelSearch.CheckOut
        If CheckIn <> "" Then
            Dim strDates As String() = CheckIn.Split("/")
            CheckIn = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
        End If
        If CheckOut <> "" Then
            Dim strDates As String() = CheckOut.Split("/")
            CheckOut = strDates(2) & "-" & strDates(1) & "-" & strDates(0)
        End If

        objAPIHotelSearchRequest.arrivalDate = CheckIn
        objAPIHotelSearchRequest.departureDate = CheckOut
        Dim strRoomString As String = objBLLHotelSearch.RoomString
        Dim strRoomsAll() = strRoomString.Split(";")

        Dim dist As List(Of distributions) = New List(Of distributions)
        Dim distribution As distributions

        For i As Integer = 0 To strRoomsAll.Length - 1
            Dim strRooms() = strRoomsAll(i).Split(",")
            distribution = New distributions()
            distribution.numberAdults = strRooms(1)
            distribution.numberChildren = strRooms(2)
            Dim childrenAges As New List(Of Integer)
            If strRooms(2) > 0 Then
                Dim strChildAges As String() = strRooms(3).Split("|")
                For j As Integer = 0 To strChildAges.Length - 1
                    childrenAges.Add(strChildAges(j))
                Next

            End If
            If objBLLHotelSearch.MealPlan <> "" Then
                distribution.board.id = dsMapping.Tables(0).Rows(0)("mealcode_new").ToString
            End If

            distribution.childrenAges = childrenAges
            distribution.numberRooms = 1 'strRooms(0)
            dist.Add(distribution)
        Next





        objAPIHotelSearchRequest.distribution = dist

        Dim hotel As New List(Of Integer)
        'hotel.Add(objBLLHotelSearch.HotelCode)
        If objBLLHotelSearch.HotelCode <> "" Then
            hotel.Add(dsMapping.Tables(0).Rows(0)("partycode_new").ToString)
        Else
            If objBLLHotelSearch.DestinationType = "City" Then
                objAPIHotelSearchRequest.hotel.city = dsMapping.Tables(0).Rows(0)("desttypecode_new").ToString
                'Else
                '    objAPIHotelSearchRequest.hotel.city = Nothing
            End If
            If objBLLHotelSearch.DestinationType = "Area" Or objBLLHotelSearch.DestinationType = "Sector" Then
                objAPIHotelSearchRequest.hotel.region = dsMapping.Tables(0).Rows(0)("desttypecode_new").ToString
            End If
            If objBLLHotelSearch.StarCategory <> "" Then
                objAPIHotelSearchRequest.hotel.category = dsMapping.Tables(0).Rows(0)("catcode_new").ToString
            End If

        End If

        'If objBLLHotelSearch.HotelCode <> "" Then
        '    hotel.Add(dsMapping.Tables(0).Rows(0)("partycode_new").ToString)
        'End If

        objAPIHotelSearchRequest.hotel.hotel = hotel

        objAPIHotelSearchRequest.onRequest = False
        objAPIHotelSearchRequest.priceDetails = True
        objAPIHotelSearchRequest.ratePlanCount = 100
        objAPIHotelSearchRequest.login.country = dsMapping.Tables(0).Rows(0)("ctrycode_new").ToString '"US"
        objAPIHotelSearchRequest.login.lang = "en"
        objAPIHotelSearchRequest.login.password = "pDfekNA92pd29b2w"
        objAPIHotelSearchRequest.login.user = "discover.saudixml"
        objResult = MakeAPISearchRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/search", objAPIHotelSearchRequest, "POST", "application/json")
        Return objResult


    End Function

    Public Function MakeAPIHotelDetailsRequest(ByVal requestUrl As String, ByVal JSONRequest As Object, ByVal JSONmethod As String, ByVal JSONContentType As String) As Object
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)
            Dim sb As String
            If JSONRequest.GetType.FullName = Type.GetType("System.String").FullName Then
                sb = CType(JSONRequest, String)
            Else
                sb = New JavaScriptSerializer().Serialize(JSONRequest)
            End If
            request.Method = JSONmethod
            request.ContentType = JSONContentType
            Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
            Dim st As Stream = request.GetRequestStream()
            st.Write(bt, 0, bt.Length)
            st.Close()

            Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Dim stream1 As Stream = response.GetResponseStream()
                    '     Dim sr As StreamReader = New StreamReader(stream1)
                    Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim sr As StreamReader = New StreamReader(stream1, encode)
                    Dim strsb As String = sr.ReadToEnd()

                    Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                    Dim objresponse As APIHotelDetailsResponse.HotelDetailsResponse = serializer.Deserialize(Of APIHotelDetailsResponse.HotelDetailsResponse)(strsb)

                    Return objresponse
                Else
                    Dim errResponse As APIHotelDetailsResponse.ResponseStatus = New APIHotelDetailsResponse.ResponseStatus()
                    errResponse.StatusCode = response.StatusCode
                    errResponse.StatusDescription = response.StatusDescription
                    Return errResponse
                End If
            End Using
        Catch e As WebException
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Dim statusCode As String = HttpStatusCode.BadRequest
            Dim StatusDescription As String = e.Message.ToString()
            If Not e.Response Is Nothing Then
                Using response As WebResponse = e.Response
                    Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                    Using Data As Stream = response.GetResponseStream()
                        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        Using reader As StreamReader = New StreamReader(Data, encode)
                            Dim strsb As String = reader.ReadToEnd()
                            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                            Dim objresponse As APIHotelDetailsResponse.ErrorResponseStatus = serializer.Deserialize(Of APIHotelDetailsResponse.ErrorResponseStatus)(strsb)
                            statusCode = objresponse.code
                            StatusDescription = objresponse.message
                        End Using
                    End Using
                End Using
            End If
            Dim errResponse As APIHotelDetailsResponse.ResponseStatus = New APIHotelDetailsResponse.ResponseStatus()
            errResponse.StatusCode = statusCode
            errResponse.StatusDescription = StatusDescription
            Return errResponse
        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            'Throw e
            Dim errResponse As APIHotelDetailsResponse.ResponseStatus = New APIHotelDetailsResponse.ResponseStatus()
            errResponse.StatusCode = HttpStatusCode.BadRequest
            errResponse.StatusDescription = e.Message.ToString()
            Return errResponse
        End Try
    End Function

    Function CallHotelDetailsAPI_CheckPriceAndAvailability(ByVal dr As DataRow, ByVal userlogged As String) As Object
        Try
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            Dim hotelDetailRequest As APIHotelDetailsRequest.HotelDetailsRequest = serializer.Deserialize(Of APIHotelDetailsRequest.HotelDetailsRequest)(dr("APIRequest"))
            Dim strHotelDetailRequest As String = CType(dr("APIRequest"), String)

            Dim objResult As Object = MakeAPIHotelDetailsRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/details", strHotelDetailRequest, "POST", "application/json")

            If objResult.GetType.FullName = "APIHotelDetailsResponse+HotelDetailsResponse" Then
                Dim resDetail As APIHotelDetailsResponse.HotelDetailsResponse = CType(objResult, APIHotelDetailsResponse.HotelDetailsResponse)
                'dr("availability") = True
                If dr("costvalue") = resDetail.result.netPrice Then
                    'dr("matchPrice") = True
                    Return New APIHotelDetailsResponse.ResponseStatus With {
                    .StatusCode = HttpStatusCode.OK,
                    .StatusDescription = "Available"
                    }
                Else
                    Dim newSaleValue As Decimal = 0
                    Dim dt As New DataTable
                    For Each distList In resDetail.result.distributions
                        dt.Columns.Add(New DataColumn("hotelcode", GetType(String)))
                        dt.Columns.Add(New DataColumn("roomtypecode", GetType(String)))
                        dt.Columns.Add(New DataColumn("cost", GetType(Decimal)))
                        dt.Columns.Add(New DataColumn("noofroom", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("noofadult", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("noofchild", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("Int_RoomtypeCodes", GetType(String)))
                        dt.Columns.Add(New DataColumn("mealplan", GetType(String)))
                        dt.Columns.Add(New DataColumn("currCode", GetType(String)))
                        Dim mdr As DataRow = dt.NewRow
                        mdr("hotelcode") = resDetail.result.hotel.id
                        mdr("roomtypecode") = distList.roomcode
                        mdr("cost") = distList.netPrice
                        mdr("noofroom") = distList.numberRooms
                        mdr("noofadult") = distList.numberAdults
                        mdr("noofchild") = distList.numberChildren
                        mdr("Int_RoomtypeCodes") = ""
                        mdr("mealplan") = distList.board.id
                        mdr("currCode") = resDetail.result.currency
                        dt.Rows.Add(mdr)
                    Next
                    dt.TableName = "Table"
                    Dim searchdetail As String = objclsUtilities.GenerateXML_FromDataTable(dt)
                    Dim param(6) As SqlParameter
                    param(0) = New SqlParameter("@requestid", dr("requestid"))
                    param(1) = New SqlParameter("@rlineno", dr("rlineno"))
                    param(2) = New SqlParameter("@checkin", resDetail.result.arrivalDate.ToString("yyy-MM-dd"))
                    param(3) = New SqlParameter("@checkout", resDetail.result.departureDate.ToString("yyy-MM-dd"))
                    param(4) = New SqlParameter("@searchxml", searchdetail)
                    param(5) = New SqlParameter("@userlogged", userlogged)
                    param(6) = New SqlParameter("@clientCode", "oneDMC")
                    Dim resultDt As DataTable = objclsUtilities.GetDataTable("sp_get_Booking_markup", param)
                    If Not resultDt Is Nothing AndAlso resultDt.Rows.Count > 0 Then
                        newSaleValue = resultDt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("salevalue"))
                        Dim agentcurrcode As String = resultDt.Rows(0)("agentcurrcode")
                        If newSaleValue <= 0 Then
                            Return New APIHotelDetailsResponse.ResponseStatus With {
                            .StatusCode = "0000",
                            .StatusDescription = "Unknown Error"
                            }
                        Else
                            Return New APIHotelDetailsResponse.ResponseStatus With {
                            .StatusCode = "0005",
                            .StatusDescription = "Price has changed, Current price : " + agentcurrcode + " " + dr("salevalue").ToString() + "; Revised price : " + agentcurrcode + " " + newSaleValue.ToString()
                            }
                        End If
                    Else
                        Return New APIHotelDetailsResponse.ResponseStatus With {
                            .StatusCode = "0000",
                            .StatusDescription = "Unknown Error"
                            }
                    End If
                End If
            Else
                Return objResult
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return New APIHotelDetailsResponse.ResponseStatus With {
                .StatusCode = HttpStatusCode.InternalServerError,
                .StatusDescription = ex.Message.ToString()
                }
        End Try
    End Function

    Public Function MakeAPIHotelConfirmRequest(ByVal requestUrl As String, ByVal JSONRequest As Object, ByVal JSONmethod As String, ByVal JSONContentType As String) As Object
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)
            Dim sb As String
            sb = New JavaScriptSerializer().Serialize(JSONRequest)
            request.Method = JSONmethod
            request.ContentType = JSONContentType
            Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
            Dim st As Stream = request.GetRequestStream()
            st.Write(bt, 0, bt.Length)
            st.Close()

            Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Dim stream1 As Stream = response.GetResponseStream()
                    'Dim sr As StreamReader = New StreamReader(stream1)
                    Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim sr As StreamReader = New StreamReader(stream1, encode)
                    Dim strsb As String = sr.ReadToEnd()

                    Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                    Dim objresponse As APIHotelConfirmResponse.HotelConfirmResponse = serializer.Deserialize(Of APIHotelConfirmResponse.HotelConfirmResponse)(strsb)

                    Return objresponse
                Else
                    Dim errResponse As APIHotelConfirmResponse.ResponseStatus = New APIHotelConfirmResponse.ResponseStatus()
                    errResponse.StatusCode = response.StatusCode
                    errResponse.StatusDescription = response.StatusDescription
                    Return errResponse
                End If
            End Using
        Catch e As WebException
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Dim statusCode As String = HttpStatusCode.BadRequest
            Dim StatusDescription As String = e.Message.ToString()
            If Not e.Response Is Nothing Then
                Using response As WebResponse = e.Response
                    Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                    Using Data As Stream = response.GetResponseStream()
                        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        Using reader As StreamReader = New StreamReader(Data, encode)
                            Dim strsb As String = reader.ReadToEnd()
                            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                            Dim objresponse As APIHotelConfirmResponse.ErrorResponseStatus = serializer.Deserialize(Of APIHotelConfirmResponse.ErrorResponseStatus)(strsb)
                            statusCode = objresponse.code
                            StatusDescription = objresponse.message
                        End Using
                    End Using
                End Using
            End If
            Dim errResponse As APIHotelConfirmResponse.ResponseStatus = New APIHotelConfirmResponse.ResponseStatus()
            errResponse.StatusCode = statusCode
            errResponse.StatusDescription = StatusDescription
            Return errResponse
        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            'Throw e
            Dim errResponse As APIHotelConfirmResponse.ResponseStatus = New APIHotelConfirmResponse.ResponseStatus()
            errResponse.StatusCode = HttpStatusCode.BadRequest
            errResponse.StatusDescription = e.Message.ToString
            Return errResponse
        End Try
    End Function

    Function CallHotelConfirmation(ByVal dr As DataRow, ByVal strPath As String, ByVal guestDt As DataTable, ByVal agentRef As String, ByVal userlogged As String, ByVal NoOfTries As Integer) As Object
        Try
            Dim HotelConfirmRequest As New APIHotelConfirmRequest.HotelConfirmRequest()
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            Dim hotelDetailRequest As APIHotelDetailsRequest.HotelDetailsRequest = serializer.Deserialize(Of APIHotelDetailsRequest.HotelDetailsRequest)(dr("APIRequest"))
            Dim hotelDetailResponse As APIHotelDetailsResponse.HotelDetailsResponse = serializer.Deserialize(Of APIHotelDetailsResponse.HotelDetailsResponse)(dr("APIResponse"))

            HotelConfirmRequest.arrivalDate = Convert.ToDateTime(hotelDetailRequest.arrivalDate).ToString("yyyy-MM-dd")  'DateTimeOffset.ParseExact(hotelDetailRequest.arrivalDate, "yyyy-MM-dd'T'hh:mm:ss'Z'", CultureInfo.InvariantCulture) 
            HotelConfirmRequest.departureDate = Convert.ToDateTime(hotelDetailRequest.departureDate).ToString("yyyy-MM-dd") 'DateTimeOffset.ParseExact(hotelDetailRequest.arrivalDate, "yyyy-MM-dd'T'hh:mm:ss'Z'", CultureInfo.InvariantCulture) 
            Dim distribute As New List(Of APIHotelConfirmRequest.Distribution)
            Dim i As Integer = 0
            For Each distList In hotelDetailRequest.distribution
                i = i + 1
                Dim dist As New APIHotelConfirmRequest.Distribution()
                dist.numberAdults = distList.numberAdults
                dist.numberChildren = distList.numberChildren
                dist.numberExtraBeds = distList.numberExtraBeds
                dist.childrenAges = distList.childrenAges
                Dim dv As DataView = guestDt.DefaultView
                dv.RowFilter = "roomno = " & (i).ToString
                dv.Sort = "guestlineno asc"
                Dim guest As New DataTable
                If dv.Count > 0 Then
                    guest = dv.ToTable()
                End If
                If guest.Rows.Count > 0 Then
                    Dim paxNameList As New List(Of APIHotelConfirmRequest.PaxNames)
                    Dim gcnt As Integer = 0
                    For Each guestDr In guest.Rows
                        gcnt = gcnt + 1
                        Dim paxName As New APIHotelConfirmRequest.PaxNames()
                        paxName.name = guestDr("title") + " " + guestDr("firstname") + " " + guestDr("middlename")
                        paxName.surname = guestDr("lastname")
                        If guestDr("childage") > 0 Then
                            paxName.age = Convert.ToInt32(guestDr("childage"))
                        End If
                        paxNameList.Add(paxName)
                        If gcnt = distList.numberAdults Then
                            Exit For
                        End If
                    Next
                    dist.paxNames = paxNameList
                Else
                    dist.paxNames = Nothing
                End If
                dist.numberRooms = distList.numberRooms
                Dim board As New APIHotelConfirmRequest.Board()
                board.id = distList.board.id
                dist.board = board
                Dim room As New APIHotelConfirmRequest.Room()
                room.id = distList.room.id
                dist.room = room
                dist.searchcode = distList.searchcode
                dist.roomcode = distList.roomcode
                distribute.Add(dist)
            Next
            HotelConfirmRequest.distribution = distribute
            HotelConfirmRequest.hotel = hotelDetailRequest.hotel
            HotelConfirmRequest.searchcode = hotelDetailRequest.searchcode
            Dim leadGuest As String = ""
            If guestDt.Rows.Count > 0 Then
                leadGuest = guestDt.Rows(0)("title") + " " + guestDt.Rows(0)("firstname") + " " + guestDt.Rows(0)("middlename") + " " + guestDt.Rows(0)("lastname")
            End If
            HotelConfirmRequest.customer = leadGuest
            HotelConfirmRequest.reference = agentRef
            HotelConfirmRequest.observations = ""
            HotelConfirmRequest.netPrice = hotelDetailResponse.result.netPrice 'dr("costvalue")
            Dim login As New APIHotelConfirmRequest.Login()
            login.country = hotelDetailRequest.login.country
            login.lang = hotelDetailRequest.login.lang
            login.user = hotelDetailRequest.login.user
            login.password = hotelDetailRequest.login.password
            HotelConfirmRequest.login = login

            NoOfTries = NoOfTries - 1

            Dim objResult As Object = MakeAPIHotelConfirmRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/confirm", HotelConfirmRequest, "POST", "application/json")

            Dim _random As New Random()
            Dim randomNo As String = _random.Next(99999999).ToString()
            Dim JsonFileName As String = DateTime.Now.ToString("yyyyMMdd@HHmmss") + "_" + randomNo

            Dim ReqFileName As String = JsonFileName + "_" + "Request.json"
            Dim strpath1 As String = strPath + ReqFileName
            Dim jsondata As String = New JavaScriptSerializer().Serialize(HotelConfirmRequest)
            System.IO.File.WriteAllText(strpath1, jsondata)

            Dim ResFileName As String = JsonFileName + "_" + "Response.json"
            strpath1 = strPath + ResFileName
            jsondata = New JavaScriptSerializer().Serialize(objResult)
            System.IO.File.WriteAllText(strpath1, jsondata)

            If objResult.GetType.FullName = "APIHotelConfirmResponse+HotelConfirmResponse" Then
                Dim objResponse As APIHotelConfirmResponse.HotelConfirmResponse = CType(objResult, APIHotelConfirmResponse.HotelConfirmResponse)
                Dim paramList As New List(Of SqlParameter)
                paramList.Add(New SqlParameter("@requestid", dr("requestid")))
                paramList.Add(New SqlParameter("@rlineno", dr("rlineno")))
                paramList.Add(New SqlParameter("@confirmId", objResponse.result.id))
                paramList.Add(New SqlParameter("@confirmOrder", objResponse.result.order))
                paramList.Add(New SqlParameter("@conditions", objResponse.result.conditions))
                paramList.Add(New SqlParameter("@bookableAndPayable", objResponse.result.bookableAndPayable))
                paramList.Add(New SqlParameter("@confirmReqFileName", ReqFileName))
                paramList.Add(New SqlParameter("@confirmResFileName", ResFileName))
                paramList.Add(New SqlParameter("@userlogged", userlogged))
                objclsUtilities.ExecuteNonQuery_Param("sp_update_booking_hotelDetailApiTemp_confirm", paramList)
                Return New APIHotelConfirmResponse.ResponseStatus With {
                .StatusCode = HttpStatusCode.OK,
                .StatusDescription = "Booking Confirmed"
                }
            Else
                If NoOfTries > 0 Then
                    Dim Result = CallHotelConfirmation(dr, strPath, guestDt, agentRef, userlogged, NoOfTries)
                    Return Result
                Else
                    Return objResult
                End If
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            'Throw ex
            Return New APIHotelConfirmResponse.ResponseStatus With {
                .StatusCode = HttpStatusCode.InternalServerError,
                .StatusDescription = ex.Message.ToString()
                }
        End Try
    End Function

    Function CallHotelDetailsAPI_updateResponse(ByVal dtHotel As DataTable, ByVal userlogged As String) As String
        Try
            Dim hotelDetailRequest As String
            For Each dr As DataRow In dtHotel.Rows
                hotelDetailRequest = dr("APIRequest")
                Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                Dim objHotelDetailRequest As APIHotelDetailsRequest.HotelDetailsRequest = serializer.Deserialize(Of APIHotelDetailsRequest.HotelDetailsRequest)(dr("APIRequest"))

                Dim objResult As Object = MakeAPIHotelDetailsRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/details", hotelDetailRequest, "POST", "application/json")

                If objResult.GetType.FullName = "APIHotelDetailsResponse+HotelDetailsResponse" Then
                    Dim resDetail As APIHotelDetailsResponse.HotelDetailsResponse = CType(objResult, APIHotelDetailsResponse.HotelDetailsResponse)
                    Dim strResponse As String = New JavaScriptSerializer().Serialize(resDetail)
                    Dim sqlParam As New List(Of SqlParameter)
                    sqlParam.Add(New SqlParameter("@requestid", dr("requestid")))
                    sqlParam.Add(New SqlParameter("@rlineno", dr("rlineno")))
                    sqlParam.Add(New SqlParameter("@apiResponse", strResponse))
                    objclsUtilities.ExecuteNonQuery_Param("sp_update_booking_hotelDetailApiTemp_response", sqlParam)

                    Dim dt As New DataTable
                    Dim i As Integer = 0
                    For Each distList In resDetail.result.distributions
                        i = i + 1
                        dt.Columns.Add(New DataColumn("hotelcode", GetType(String)))
                        dt.Columns.Add(New DataColumn("roomtypecode", GetType(String)))
                        dt.Columns.Add(New DataColumn("cost", GetType(Decimal)))
                        dt.Columns.Add(New DataColumn("noofroom", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("noofadult", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("noofchild", GetType(Integer)))
                        dt.Columns.Add(New DataColumn("Int_RoomtypeCodes", GetType(String)))
                        dt.Columns.Add(New DataColumn("mealplan", GetType(String)))
                        dt.Columns.Add(New DataColumn("currCode", GetType(String)))
                        Dim mdr As DataRow = dt.NewRow
                        mdr("hotelcode") = resDetail.result.hotel.id
                        mdr("roomtypecode") = distList.roomcode
                        mdr("cost") = distList.netPrice
                        mdr("noofroom") = distList.numberRooms
                        mdr("noofadult") = distList.numberAdults
                        mdr("noofchild") = distList.numberChildren
                        mdr("Int_RoomtypeCodes") = i.ToString + ":" + objHotelDetailRequest.distribution(i - 1).roomcode
                        mdr("mealplan") = distList.board.id
                        mdr("currCode") = resDetail.result.currency
                        dt.Rows.Add(mdr)
                    Next
                    dt.TableName = "Table"
                    Dim searchdetail As String = objclsUtilities.GenerateXML_FromDataTable(dt)
                    Dim param(6) As SqlParameter
                    param(0) = New SqlParameter("@requestid", dr("requestid"))
                    param(1) = New SqlParameter("@rlineno", dr("rlineno"))
                    param(2) = New SqlParameter("@checkin", resDetail.result.arrivalDate.ToString("yyy-MM-dd"))
                    param(3) = New SqlParameter("@checkout", resDetail.result.departureDate.ToString("yyy-MM-dd"))
                    param(4) = New SqlParameter("@searchxml", searchdetail)
                    param(5) = New SqlParameter("@userlogged", userlogged)
                    param(6) = New SqlParameter("@clientCode", "oneDMC")
                    Dim resultDt As DataTable = objclsUtilities.GetDataTable("sp_get_Booking_markup", param)

                    Dim markupxml As String = ""
                    If Not resultDt Is Nothing And resultDt.Rows.Count > 0 Then
                        resultDt.TableName = "Table"
                        markupxml = objclsUtilities.GenerateXML_FromDataTable(resultDt)

                        Dim mySqlConn As New SqlConnection
                        Dim mysqlTrans As SqlTransaction = Nothing
                        Try
                            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
                            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
                            mysqlTrans = mySqlConn.BeginTransaction
                            Dim paramlist As New List(Of SqlParameter)
                            paramlist.Add(New SqlParameter("@requestid", dr("requestid")))
                            paramlist.Add(New SqlParameter("@rlineno", dr("rlineno")))
                            paramlist.Add(New SqlParameter("@checkin", resDetail.result.arrivalDate.ToString("yyy-MM-dd")))
                            paramlist.Add(New SqlParameter("@checkout", resDetail.result.departureDate.ToString("yyy-MM-dd")))
                            paramlist.Add(New SqlParameter("@markupxml", markupxml))
                            objclsUtilities.ExecuteNonQuerynew(constring, "sp_update_booking_hotel_detail_pricestemp", paramlist, mySqlConn, mysqlTrans)
                            mysqlTrans.Commit()    'SQl Tarn Commit
                            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
                            clsDBConnect.dbConnectionClose(mySqlConn)
                        Catch ex As Exception
                            If Not mysqlTrans Is Nothing Then
                                mysqlTrans.Rollback()
                                clsDBConnect.dbSqlTransation(mysqlTrans)
                            End If
                            clsDBConnect.dbConnectionClose(mySqlConn)
                            objclsUtilities.WriteErrorLog("DALAirportMeetSearch: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
                        End Try
                    End If
                End If
            Next
            Return ""
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Throw ex
        End Try
    End Function

    Public Function MakeAPIHotelBookingInfoRequest(ByVal requestUrl As String, ByVal JSONRequest As Object, ByVal JSONmethod As String, ByVal JSONContentType As String) As Object
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)
            Dim sb As String
            sb = New JavaScriptSerializer().Serialize(JSONRequest)
            request.Method = JSONmethod
            request.ContentType = JSONContentType
            Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
            Dim st As Stream = request.GetRequestStream()
            st.Write(bt, 0, bt.Length)
            st.Close()

            Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Dim stream1 As Stream = response.GetResponseStream()
                    Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim sr As StreamReader = New StreamReader(stream1, encode)
                    Dim strsb As String = sr.ReadToEnd()

                    Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                    Dim objresponse As APIHotelBookingInfoResponse.HotelBookingInfoResponse = serializer.Deserialize(Of APIHotelBookingInfoResponse.HotelBookingInfoResponse)(strsb)
                    Return objresponse
                Else
                    Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
                End If
            End Using
        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Throw e
        End Try
    End Function

    Function CallHotelBookingInfo(ByVal hotelConfirmId As String, ByVal sourcectrycode As String) As APIHotelBookingInfoResponse.HotelBookingInfoResponse
        Try
            Dim bookingInfoReq As APIHotelBookingInfoRequest.HotelBookingInfoRequest = New APIHotelBookingInfoRequest.HotelBookingInfoRequest()
            bookingInfoReq.locator = hotelConfirmId
            bookingInfoReq.newCancellation = True
            Dim login As APIHotelBookingInfoRequest.Login = New APIHotelBookingInfoRequest.Login()
            login.country = sourcectrycode
            login.lang = "en"
            login.user = "discover.saudixml"
            login.password = "pDfekNA92pd29b2w"
            bookingInfoReq.login = login
            Dim objResult As APIHotelBookingInfoResponse.HotelBookingInfoResponse = MakeAPIHotelBookingInfoRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/info", bookingInfoReq, "POST", "application/json")

            Return objResult
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Throw New Exception(ex.Message.ToString())
        End Try
    End Function

    Public Function MakeAPIHotelCancelRequest(ByVal requestUrl As String, ByVal JSONRequest As Object, ByVal JSONmethod As String, ByVal JSONContentType As String) As Object
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)
            Dim sb As String
            sb = New JavaScriptSerializer().Serialize(JSONRequest)
            request.Method = JSONmethod
            request.ContentType = JSONContentType
            Dim bt As Byte() = Encoding.UTF8.GetBytes(sb)
            Dim st As Stream = request.GetRequestStream()
            st.Write(bt, 0, bt.Length)
            st.Close()

            Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Dim stream1 As Stream = response.GetResponseStream()
                    Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim sr As StreamReader = New StreamReader(stream1, encode)
                    Dim strsb As String = sr.ReadToEnd()

                    Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                    Dim objresponse As APIHotelCancellationResponse.HotelCancellationResponse = serializer.Deserialize(Of APIHotelCancellationResponse.HotelCancellationResponse)(strsb)

                    Return objresponse
                Else
                    Dim errResponse As APIHotelCancellationResponse.ResponseStatus = New APIHotelCancellationResponse.ResponseStatus()
                    errResponse.StatusCode = response.StatusCode
                    errResponse.StatusDescription = response.StatusDescription
                    Return errResponse
                    'Throw New Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription))
                End If
            End Using
        Catch e As WebException
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Dim statusCode As String = HttpStatusCode.BadRequest
            Dim StatusDescription As String = e.Message.ToString()
            If Not e.Response Is Nothing Then
                Using response As WebResponse = e.Response
                    Dim httpResponse As HttpWebResponse = CType(response, HttpWebResponse)
                    Using Data As Stream = response.GetResponseStream()
                        Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                        Using reader As StreamReader = New StreamReader(Data, encode)
                            Dim strsb As String = reader.ReadToEnd()
                            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                            Dim objresponse As APIHotelCancellationResponse.ErrorResponseStatus = serializer.Deserialize(Of APIHotelCancellationResponse.ErrorResponseStatus)(strsb)
                            statusCode = objresponse.code
                            StatusDescription = objresponse.message
                        End Using
                    End Using
                End Using
            End If
            Dim errResponse As APIHotelCancellationResponse.ResponseStatus = New APIHotelCancellationResponse.ResponseStatus()
            errResponse.StatusCode = statusCode
            errResponse.StatusDescription = StatusDescription
            Return errResponse
        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            'Throw e
            Return New APIHotelCancellationResponse.ResponseStatus With {
            .StatusCode = HttpStatusCode.BadRequest,
            .StatusDescription = e.Message.ToString()
            }
        End Try
    End Function

    Function CallHotelCancellation(ByVal dr As DataRow, ByVal strPath As String, ByVal userlogged As String, ByVal NoOfTries As Integer) As Object
        Try
            Dim HotelCancelRequest As New APIHotelCancellationRequest.HotelCancellationRequest()
            HotelCancelRequest.locator = dr("ConfirmId")
            HotelCancelRequest.netPenaltyFee = dr("costvalue")
            Dim login As APIHotelCancellationRequest.Login = New APIHotelCancellationRequest.Login()
            login.country = dr("sourcectrycode")
            login.lang = "en"
            login.user = "discover.saudixml"
            login.password = "pDfekNA92pd29b2w"
            HotelCancelRequest.login = login
            NoOfTries = NoOfTries - 1

            Dim objResult As Object = MakeAPIHotelCancelRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/cancel", HotelCancelRequest, "POST", "application/json")

            Dim _random As New Random()
            Dim randomNo As String = _random.Next(99999999).ToString()
            Dim JsonFileName As String = DateTime.Now.ToString("yyyyMMdd@HHmmss") + "_" + randomNo

            Dim ReqFileName As String = JsonFileName + "_" + "CancelReq.json"
            Dim strpath1 As String = strPath + ReqFileName
            Dim jsondata As String = New JavaScriptSerializer().Serialize(HotelCancelRequest)
            System.IO.File.WriteAllText(strpath1, jsondata)

            Dim ResFileName As String = JsonFileName + "_" + "CancelRes.json"
            strpath1 = strPath + ResFileName
            jsondata = New JavaScriptSerializer().Serialize(objResult)
            System.IO.File.WriteAllText(strpath1, jsondata)

            If objResult.GetType.FullName = "APIHotelCancellationResponse+HotelCancellationResponse" Then
                Dim objResponse As APIHotelCancellationResponse.HotelCancellationResponse = CType(objResult, APIHotelCancellationResponse.HotelCancellationResponse)
                Dim paramList As New List(Of SqlParameter)
                paramList.Add(New SqlParameter("@requestid", dr("requestid")))
                paramList.Add(New SqlParameter("@rlineno", dr("rlineno")))
                paramList.Add(New SqlParameter("@cancelId", objResponse.result.id))
                paramList.Add(New SqlParameter("@cancelOrder", objResponse.result.order))
                paramList.Add(New SqlParameter("@CancelReqFileName", ReqFileName))
                paramList.Add(New SqlParameter("@CancelResFileName", ResFileName))
                paramList.Add(New SqlParameter("@userlogged", userlogged))
                paramList.Add(New SqlParameter("@cancelStatus", "SUCCESS"))
                objclsUtilities.ExecuteNonQuery_Param("sp_update_booking_hotelDetail_finalCancelTemp", paramList)
                Return New APIHotelCancellationResponse.ResponseStatus With {
                    .StatusCode = HttpStatusCode.OK,
                    .StatusDescription = "SUCCESS"
                    }
            Else
                If NoOfTries > 0 Then
                    Dim Result = CallHotelCancellation(dr, strPath, userlogged, NoOfTries)
                    Return Result
                Else
                    Return objResult
                End If
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            'Throw ex
            Return New APIHotelCancellationResponse.ResponseStatus With {
                    .StatusCode = HttpStatusCode.InternalServerError,
                    .StatusDescription = ex.Message.ToString()
                    }
        End Try
    End Function



    Public Class APIHotelSearchRequest

        Public arrivalDate As String   'Yes		datetime
        Public departureDate As String 'Yes		datetime
        Public distribution As New List(Of distributions)

        ' Dim distribution As distributions = New distributions()
        Public hotel As hotels = New hotels()

        Public Property onRequest As Boolean
        Public Property priceDetails As Boolean
        Public Property ratePlanCount As Integer
        Public Property excludeStaticDetails As Boolean

        Public login As loginDetail = New loginDetail()
    End Class

    Public Class distributions
        Public Property numberAdults As Integer
        Public Property numberChildren As Integer
        Public Property childrenAges As New List(Of Integer)
        Public Property numberRooms As Integer
        Public board As board = New board()
        Dim room As room = New room()
    End Class

    Public Class board
        Public Property id As Integer?
    End Class
    Public Class room
        Public Property id As String
    End Class

    Public Class hotels

        Public Property hotel As New List(Of Integer)
        Public Property region As Integer?
        Public Property city As Integer?
        Public Property touristZone As Integer
        Public Property category As Integer?
    End Class


    Public Class loginDetail
        Public Property country As String
        Public Property lang As String
        Public Property user As String
        Public Property password As String
        Public Property residence As String
        Public Property timestampId As String
    End Class


    Function CallTestAPI() As String
        Dim objAPIRequest As APIStaticRequest = New APIStaticRequest()

        objAPIRequest.login.lang = "en"
        objAPIRequest.login.password = "pDfekNA92pd29b2w"
        objAPIRequest.login.user = "discover.saudixml"



        'Dim obj As Object = MakeAPIRequest("http://pre-xml.seeraspain.com/rst/services/information/roomTypes", objAPIRequest, "POST", "application/json")
    End Function
    Public Class login

        Public Property lang As String
        Public Property password As String
        Public Property user As String
    End Class
    Public Class APIStaticRequest
        Public login As New login()
    End Class

    Public Class APIStaticResponse
        Public Property type As String
        Public results As New List(Of StaticResult)
    End Class
    Public Class StaticResult

        Public Property id As String
        Public Property name As String
    End Class

    Function GetDetailApiResponse(ByVal requestUrl As String, ByVal JSONRequest As String, ByVal JSONmethod As String, ByVal JSONContentType As String) As String
        Try
            Dim request As HttpWebRequest = TryCast(WebRequest.Create(requestUrl), HttpWebRequest)

            request.Method = JSONmethod
            request.ContentType = JSONContentType
            Dim bt As Byte() = Encoding.UTF8.GetBytes(JSONRequest)
            Dim st As Stream = request.GetRequestStream()
            st.Write(bt, 0, bt.Length)
            st.Close()

            Using response As HttpWebResponse = TryCast(request.GetResponse(), HttpWebResponse)
                If response.StatusCode = HttpStatusCode.OK Then
                    Dim stream1 As Stream = response.GetResponseStream()
                    Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim sr As StreamReader = New StreamReader(stream1, encode)
                    Dim strsb As String = sr.ReadToEnd()
                    Return strsb
                    'Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
                    'Dim objresponse As APIHotelDetailsResponse.HotelDetailsResponse = serializer.Deserialize(Of APIHotelDetailsResponse.HotelDetailsResponse)(strsb)

                    'Return objresponse
                Else
                    If response.StatusCode = "0013" Then
                        Return "No Availability"
                    Else
                        Return "Error"
                    End If
                    'Dim errResponse As APIHotelDetailsResponse.ResponseStatus = New APIHotelDetailsResponse.ResponseStatus()
                    'errResponse.StatusCode = response.StatusCode
                    'errResponse.StatusDescription = response.StatusDescription

                End If
            End Using
        Catch e As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & e.Message.ToString)
            Return "Error"
        End Try
    End Function

    Function GetCancelationPolicyFromDetailApiResponse(ByVal objAPIHotelDetailsRequest As APIHotelDetailsRequest.HotelDetailsRequest) As Object
        Try
            Dim resDetail As APIHotelDetailsResponse.HotelDetailsResponse
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            Dim strHotelDetailRequest As String = New JavaScriptSerializer().Serialize(objAPIHotelDetailsRequest)

            Dim objResult As Object = MakeAPIHotelDetailsRequest("http://pre-xml.seeraspain.com/rst/services/accomodation/details", strHotelDetailRequest, "POST", "application/json")

            If objResult.GetType.FullName = "APIHotelDetailsResponse+HotelDetailsResponse" Then
                resDetail = CType(objResult, APIHotelDetailsResponse.HotelDetailsResponse)
                Return resDetail
                '  strCancelPolicy=resDetail.result.cancelConditions.
                '   the starting cancel penalty

            Else
                Return Nothing
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("ApiController:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function



End Class
