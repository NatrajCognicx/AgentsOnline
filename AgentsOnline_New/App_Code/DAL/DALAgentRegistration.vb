Imports Microsoft.VisualBasic
Imports System.Data.SqlClient



Public Class DALAgentRegistration
    Dim objclsUtilities As New clsUtilities()

    Function SaveRegistrationDetails(RegNo As String, Name As String, BuildingNo As String, StreetLine1 As String, StreetLine2 As String, CityCode As String, StateCode As String, CtryCode As String, ZipCode As String, Tel1 As String, Fax As String, Email As String, Type As String, Founded As String, NoOfEmployees As String, Website As String, AboutUs As String, Contactperson_Name As String, Contactperson_MobileNo As String, Contactperson_FaxNo As String, Contactperson_Email As String, Contactperson_Position As String, Contactperson_PhneNo As String) As Integer
        Dim iStatus As Integer
        Dim ProcName As String
        ProcName = "sp_registration"
        Dim sqlParamList As New List(Of SqlParameter)
        Dim parm(24) As SqlParameter

        parm(0) = New SqlParameter("@RegNo", CType(RegNo, String))
        sqlParamList.Add(parm(0))

        parm(1) = New SqlParameter("@Name", CType(Name, String))
        sqlParamList.Add(parm(1))

        parm(2) = New SqlParameter("@BuildingNo", CType(BuildingNo, String))
        sqlParamList.Add(parm(2))

        parm(3) = New SqlParameter("@StreetLine1", CType(StreetLine1, String))
        sqlParamList.Add(parm(3))

        parm(4) = New SqlParameter("@StreetLine2", CType(StreetLine2, String))
        sqlParamList.Add(parm(4))

        parm(5) = New SqlParameter("@CityCode", CType(CityCode, String))
        sqlParamList.Add(parm(5))

        parm(6) = New SqlParameter("@StateCode", CType(StateCode, String))
        sqlParamList.Add(parm(6))

        parm(7) = New SqlParameter("@CtryCode", CType(CtryCode, String))
        sqlParamList.Add(parm(7))

        parm(8) = New SqlParameter("@ZipCode", CType(StreetLine1, String))
        sqlParamList.Add(parm(8))

        parm(9) = New SqlParameter("@Tel1", CType(Tel1, String))
        sqlParamList.Add(parm(9))

        parm(10) = New SqlParameter("@Fax", CType(Fax, String))
        sqlParamList.Add(parm(10))

        parm(11) = New SqlParameter("@Email", CType(Email, String))
        sqlParamList.Add(parm(11))

        parm(12) = New SqlParameter("@Type", CType(Type, String))
        sqlParamList.Add(parm(12))

        parm(13) = New SqlParameter("@Founded", CType(Founded, String))
        sqlParamList.Add(parm(13))

        parm(14) = New SqlParameter("@NoOfEmployees", CType(NoOfEmployees, String))
        sqlParamList.Add(parm(14))

        parm(15) = New SqlParameter("@Website", CType(Website, String))
        sqlParamList.Add(parm(15))

        parm(16) = New SqlParameter("@AboutUs", CType(AboutUs, String))
        sqlParamList.Add(parm(16))

        parm(17) = New SqlParameter("@Contactperson_Name", CType(Contactperson_Name, String))
        sqlParamList.Add(parm(17))

        parm(18) = New SqlParameter("@Contactperson_Position", CType(Contactperson_Position, String))
        sqlParamList.Add(parm(18))

        parm(19) = New SqlParameter("@Contactperson_PhneNo", CType(Contactperson_PhneNo, String))
        sqlParamList.Add(parm(19))

        parm(20) = New SqlParameter("@Contactperson_FaxNo", CType(Contactperson_FaxNo, String))
        sqlParamList.Add(parm(20))

        parm(21) = New SqlParameter("@Contactperson_MobileNo", CType(Contactperson_MobileNo, String))
        sqlParamList.Add(parm(21))

        parm(22) = New SqlParameter("@Contactperson_Email", CType(Contactperson_Email, String))
        sqlParamList.Add(parm(22))

        parm(23) = New SqlParameter("@Contactperson_designation", CType(Contactperson_Position, String))
        sqlParamList.Add(parm(23))

        parm(24) = New SqlParameter("@approve", CType("0", String))
        sqlParamList.Add(parm(24))
        iStatus = objclsUtilities.ExecuteNonQuery_Param(ProcName, sqlParamList)
        Return iStatus
    End Function
    Public Function checkForAgentDuplicate(strCompany As String, strTel As String, strFax As String, strEmail As String, strRegNo As String) As String()
        Dim strValidate() As String
        strValidate = objclsUtilities.checkForAgentDuplicate(strCompany, strTel, strFax, strEmail, strRegNo)
        Return strValidate
    End Function
End Class
