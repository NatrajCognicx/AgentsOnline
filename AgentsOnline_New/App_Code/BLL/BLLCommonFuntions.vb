Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Security.Cryptography
Imports System.IO

Public Class BLLCommonFuntions
    Dim objclsUtilities As New clsUtilities
    Dim objDALCommonFuntions As New DALCommonFuntions
    ''' <summary>
    ''' GetRoomAdultAndChildDetails
    ''' </summary>
    ''' <param name="requestid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRoomAdultAndChildDetails(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetRoomAdultAndChildDetails(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetAllServiceswithdates(ByVal requestid As String, ByVal checkindate As String, ByVal checkoutdate As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetAllServiceswithdates(requestid, checkindate, checkoutdate)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetAllServices(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetAllServices(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="requestid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBookingTempHeaderDetails(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingTempHeaderDetails(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingvalue(ByVal requestid As String, ByVal strWhiteLabel As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingvalue(requestid, strWhiteLabel)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingRoomstring(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingRoomstring(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingGuestnames(ByVal requestid As String, Optional ByVal mode As Integer = 0) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingGuestnames(requestid, mode)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetBookingGuestnames_new(ByVal requestid As String, Optional ByVal mode As Integer = 0, Optional ByVal Type As String = "") As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingGuestnames_new(requestid, mode, Type)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetOthersGuestnames(ByVal requestid As String, Optional ByVal mode As Integer = 0) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetOthersGuestnames(requestid, mode)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function AmendGuestServices(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.AmendGuestServices(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function Getflightdetails(ByVal requestid As String, ByVal Flighttype As String, Optional ByVal mode As Integer = 0) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.Getflightdetails(requestid, Flighttype, mode)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetBookingGuestnames_arrival(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingGuestnames_arrival(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetOtherGuestnames_arrival(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetOtherGuestnames_arrival(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetBookingGuestnames_departure(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBookingGuestnames_departure(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function GetOtherGuestnames_departure(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetOtherGuestnames_departure(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="requestid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTempFullBookingDetails(ByVal requestid As String) As DataSet
        Try
            Dim ds As New DataSet
            ds = objDALCommonFuntions.GetTempFullBookingDetails(requestid)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="requestid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExistingBookingTypes(ByVal requestid As String) As String
        Try
            Dim strBookingType As String = ""
            strBookingType = objDALCommonFuntions.GetExistingBookingTypes(requestid)
            Return strBookingType
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetExistingBookingRequestId(ByVal requestid As String) As String
        Try
            Dim strRequestId As String = ""
            strRequestId = objDALCommonFuntions.GetExistingBookingRequestId(requestid)
            Return strRequestId
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetBookingRowLineNo(ByVal strRequestId As String, ByVal strBookingType As String) As String
        Try
            Dim strRowLineNo As String = ""
            strRowLineNo = objDALCommonFuntions.GetBookingRowLineNo(strRequestId, strBookingType)
            Return strRowLineNo
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function GetBookingPreHotelRowLineNo(ByVal strRequestId As String) As String
        Return objDALCommonFuntions.GetBookingPreHotelRowLineNo(strRequestId)
    End Function
    Function GetFooterAddress(ByVal strCompany As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetFooterAddress(strCompany)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetBordercode(ByVal strRequestId As String, ByVal strType As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetBordercode(strRequestId, strType)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SaveSearchLog(ByVal strAgentCode As String, ByVal strSubUserCode As String, ByVal strIpAddress As String, ByVal strSearchLocation As String, ByVal strSearchPage As String, ByVal strSearchServiceType As String, ByVal strSearchCriteria As String, ByVal strLoggedUser As String) As String
        Dim strStatus As String = ""
        strStatus = objDALCommonFuntions.SaveSearchLog(strAgentCode, strSubUserCode, strIpAddress, strSearchLocation, strSearchPage, strSearchServiceType, strSearchCriteria, strLoggedUser)
        Return strStatus
    End Function

    Function GetTempPreArrangedDetails(ByVal strRequestId As Object) As DataSet
        Throw New NotImplementedException
    End Function
    Function fnValidateTotalPax(ByVal strRequestId As String, ByVal iTotalPax As Integer) As Integer
        Try
            Dim strStatus As String
            strStatus = objDALCommonFuntions.fnValidateTotalPax(strRequestId, iTotalPax)
            Return strStatus
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetTempHotelDetailsWithAPIDetails(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALCommonFuntions.GetTempHotelDetailsWithAPIDetails(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function fnItineraryTokenGeneration(ByVal tokenId As String, ByVal strRequestId As String, ByVal loginType As String, ByVal loginUser As String) As String
        Try
            Dim finalTokenId As String
            finalTokenId = objDALCommonFuntions.fnItineraryTokenGeneration(tokenId, strRequestId, loginType, loginUser)
            Return finalTokenId
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLCommonFuntions:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function

    Function fnEncryption(ByVal encryptText As String, ByVal EncryptionKey As String) As String

        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(encryptText)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, &H76}, 900)
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                encryptText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return encryptText
    End Function

    Function fnDecryption(ByVal decryptText As String, ByVal EncryptionKey As String) As String
        decryptText = decryptText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(decryptText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, &H76}, 900)
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                decryptText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return decryptText
    End Function

End Class
