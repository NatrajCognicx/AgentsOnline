Imports Microsoft.VisualBasic

Public Class BLLAgentRegistration

    Private _RegId As String = ""
    Private _RegNo As String = ""
    Private _Name As String = ""
    Private _BuildingNo As String = ""
    Private _StreetLine1 As String = ""
    Private _StreetLine2 As String = ""
    Private _add3 As String = ""
    Private _CityCode As String = ""
    Private _StateCode As String = ""
    Private _CtryCode As String = ""
    Private _ZipCode As String = ""
    Private _Tel1 As String = ""
    Private _Tel2 As String = ""
    Private _Fax As String = ""
    Private _Email As String = ""
    Private _Type As String = ""
    Private _Founded As String = ""
    Private _NoOfEmployees As String = ""
    Private _Website As String = ""
    Private _AboutUs As String = ""
    Private _Contactperson_Name As String = ""
    Private _Contactperson_Position As String = ""
    Private _Contactperson_PhneNo As String = ""
    Private _Contactperson_FaxNo As String = ""
    Private _Contactperson_MobileNo As String = ""
    Private _Contactperson_Email As String = ""
    Private _Contactperson_designation As String = ""
    Private _Approve As String = ""
    Private _ApproveuUser As String = ""
    Private _Webusername As String = ""
    Private _Webpassword As String = ""

    Public Property RegId As String
        Get
            Return _RegId
        End Get
        Set(value As String)
            _RegId = value
        End Set
    End Property
    Public Property RegNo As String
        Get
            Return _RegNo
        End Get
        Set(value As String)
            _RegNo = value
        End Set
    End Property
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property
    Public Property BuildingNo As String
        Get
            Return _BuildingNo
        End Get
        Set(value As String)
            _BuildingNo = value
        End Set
    End Property
    Public Property StreetLine1 As String
        Get
            Return _StreetLine1
        End Get
        Set(value As String)
            _StreetLine1 = value
        End Set
    End Property
    Public Property StreetLine2 As String
        Get
            Return _StreetLine2
        End Get
        Set(value As String)
            _StreetLine2 = value
        End Set
    End Property
    Public Property add3 As String
        Get
            Return _add3
        End Get
        Set(value As String)
            _add3 = value
        End Set
    End Property
    Public Property CityCode As String
        Get
            Return _CityCode
        End Get
        Set(value As String)
            _CityCode = value
        End Set
    End Property
    Public Property StateCode As String
        Get
            Return _StateCode
        End Get
        Set(value As String)
            _StateCode = value
        End Set
    End Property
    Public Property CtryCode As String
        Get
            Return _CtryCode
        End Get
        Set(value As String)
            _CtryCode = value
        End Set
    End Property
    Public Property ZipCode As String
        Get
            Return _ZipCode
        End Get
        Set(value As String)
            _ZipCode = value
        End Set
    End Property
    Public Property Tel1 As String
        Get
            Return _Tel1
        End Get
        Set(value As String)
            _Tel1 = value
        End Set
    End Property
    Public Property Tel2 As String
        Get
            Return _Tel2
        End Get
        Set(value As String)
            _Tel2 = value
        End Set
    End Property
    Public Property Fax As String
        Get
            Return _Fax
        End Get
        Set(value As String)
            _Fax = value
        End Set
    End Property
    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property
    Public Property Type As String
        Get
            Return _Type
        End Get
        Set(value As String)
            _Type = value
        End Set
    End Property
    Public Property Founded As String
        Get
            Return _Founded
        End Get
        Set(value As String)
            _Founded = value
        End Set
    End Property
    Public Property NoOfEmployees As String
        Get
            Return _NoOfEmployees
        End Get
        Set(value As String)
            _NoOfEmployees = value
        End Set
    End Property
    Public Property Website As String
        Get
            Return _Website
        End Get
        Set(value As String)
            _Website = value
        End Set
    End Property
    Public Property AboutUs As String
        Get
            Return _AboutUs
        End Get
        Set(value As String)
            _AboutUs = value
        End Set
    End Property
    Public Property Contactperson_Name As String
        Get
            Return _Contactperson_Name
        End Get
        Set(value As String)
            _Contactperson_Name = value
        End Set
    End Property
    Public Property Contactperson_Position As String
        Get
            Return _Contactperson_Position
        End Get
        Set(value As String)
            _Contactperson_Position = value
        End Set
    End Property
    Public Property Contactperson_PhneNo As String
        Get
            Return _Contactperson_PhneNo
        End Get
        Set(value As String)
            _Contactperson_PhneNo = value
        End Set
    End Property
    Public Property Contactperson_FaxNo As String
        Get
            Return _Contactperson_FaxNo
        End Get
        Set(value As String)
            _Contactperson_FaxNo = value
        End Set
    End Property

    Public Property Contactperson_MobileNo As String
        Get
            Return _Contactperson_MobileNo
        End Get
        Set(value As String)
            _Contactperson_MobileNo = value
        End Set
    End Property
    Public Property Contactperson_Email As String
        Get
            Return _Contactperson_Email
        End Get
        Set(value As String)
            _Contactperson_Email = value
        End Set
    End Property
    Public Property Contactperson_designation As String
        Get
            Return _Contactperson_designation
        End Get
        Set(value As String)
            _Contactperson_designation = value
        End Set
    End Property
    Public Property Approve As String
        Get
            Return _Approve
        End Get
        Set(value As String)
            _Approve = value
        End Set
    End Property
    Public Property ApproveuUser As String
        Get
            Return _ApproveuUser
        End Get
        Set(value As String)
            _ApproveuUser = value
        End Set
    End Property
    Public Property Webusername As String
        Get
            Return _Webusername
        End Get
        Set(value As String)
            _Webusername = value
        End Set
    End Property
    Public Property Webpassword As String
        Get
            Return _Webpassword
        End Get
        Set(value As String)
            _Webpassword = value
        End Set
    End Property
    Dim objDALAgentRegistration As New DALAgentRegistration()

    Function SaveRegistrationDetails() As Integer
        Dim iStatus As Integer = objDALAgentRegistration.SaveRegistrationDetails(RegNo, Name, BuildingNo, StreetLine1, StreetLine2, CityCode, StateCode, CtryCode, ZipCode, Tel1, Fax, Email, Type, Founded, NoOfEmployees, Website, AboutUs, Contactperson_Name, Contactperson_MobileNo, Contactperson_FaxNo, Contactperson_Email, Contactperson_Position, Contactperson_PhneNo)
        Return iStatus
    End Function
    Public Function checkForAgentDuplicate() As String()
        Dim strValidate() As String
        strValidate = objDALAgentRegistration.checkForAgentDuplicate(Name, Tel1, Fax, Email, RegNo)
        Return strValidate
    End Function
End Class
