Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DALHome
    Dim objclsUtilities As New clsUtilities
    Function GetOffers(strType As String, strCompany As String) As DataTable
        Try
            Dim objDataTable As DataTable
            'Dim strQuery As String = "select P.partyname,C.cityname ,O.OfferImage,convert(varchar(15),ValidFrom,106)ValidFrom,convert(varchar(15),ValidTo,106)ValidTo,Stars,'' Price from Offers O, partymast P, citymast C where O.PartyCode=P.partycode and c.citycode=p.citycode and OfferType='" & strType & "' and   O.agentcode='" & strCompany & "'"
            Dim strQuery As String = "select P.partyname,C.cityname ,O.OfferImage,convert(varchar(15),ValidFrom,106)ValidFrom,convert(varchar(15),ValidTo,106)ValidTo,Stars,'' Price from Offers(nolock) O, partymast(nolock) P, citymast(nolock) C where O.PartyCode=P.partycode and c.citycode=p.citycode and OfferType='" & strType & "' "
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHome:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetResultAsDataTable(strQuery As String) As DataTable
        Try
            Dim objDataTable As DataTable
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALHome:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetHottestOffersAndPopularDeal(strLoginType As String, strUserName As String, strAgentCode As String, strCountryCode As String) As DataTable
        Dim ProcName As String
        ProcName = "sp_booking_hotels_search_homepage"
        Dim parm(3) As SqlParameter

        parm(0) = New SqlParameter("@logintype", CType(strLoginType, String))
        parm(1) = New SqlParameter("@webusername", CType(strUserName, String))
        parm(2) = New SqlParameter("@agentcode", CType(strAgentCode, String))
        parm(3) = New SqlParameter("@sourcectrycode", CType(strCountryCode, String))
        Dim dt As New DataTable
        dt = objclsUtilities.GetDataTable(ProcName, parm)
        Return dt
    End Function

End Class
