Imports Microsoft.VisualBasic
Imports System.Data


Public Class BLLHome
    Private _SearchParam As String = ""

    Public Property SearchParam As String
        Get
            Return _SearchParam
        End Get
        Set(ByVal value As String)
            _SearchParam = value
        End Set
    End Property

    Dim objDALHome As New DALHome
    Function GetOffers(strType As String, strCompany As String) As DataTable
        Dim dtOffers As DataTable
        dtOffers = objDALHome.GetOffers(strType, strCompany)
        Return dtOffers
    End Function

    Function GetResultAsDataTable(strQuery As String) As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALHome.GetResultAsDataTable(strQuery)
        Return objDataTable
    End Function

    Function GetHottestOffersAndPopularDeal(strLoginType As String, strUserName As String, strAgentCode As String, strCountryCode As String) As DataTable
        Dim objDataTable As DataTable
        objDataTable = objDALHome.GetHottestOffersAndPopularDeal(strLoginType, strUserName, strAgentCode, strCountryCode)
        Return objDataTable
    End Function

End Class
