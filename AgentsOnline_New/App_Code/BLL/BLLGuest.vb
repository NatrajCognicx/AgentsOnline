Imports Microsoft.VisualBasic
Imports System.Data
Public Class BLLGuest
    Dim objclsUtilities As New clsUtilities

    Dim _GBGuestLineNo As String = ""
    Dim _GBrequestid As String = ""
    Dim _GBRlineno As String = ""
    Dim _GBRoomno As String = ""
    Dim _GBTitle As String = ""
    Dim _GBFirstName As String = ""
    Dim _GBMiddleName As String = ""
    Dim _GBLastName As String = ""
    Dim _GBNationalityCode As String = ""
    Dim _GBChildAge As String = ""
    Dim _GBVisaOptions As String = ""
    Dim _GBVisaTypeCode As String = ""
    Dim _GBVisaPrice As String = ""
    Dim _GBPassportNo As String = ""
    Dim _GBUpdatedDate As String = ""
    Dim _GBUpdatedUser As String = ""
    Dim _GBGuestXml As String = ""
    Dim _GBServiceXml As String = ""
    Dim _GBFlightXml As String = ""
    Dim _GBDepFlightXml As String = ""
    Dim _GBuserlogged As String = ""
    Dim _GBDepartureRemarks As String = ""
    Dim _GBArrivalRemarks As String = ""
    Dim _GBAgentRemarks As String = ""
    Dim _GBHotelRemarks As String = ""
    Dim _GBOthDepartureRemarks As String = ""
    Dim _GBOthArrivalRemarks As String = ""
    Dim _GBOthAgentRemarks As String = ""
    Dim _GBOthPartyRemarks As String = ""
    Dim _GBRemarksTemplate As String = ""
    Dim _GBToursDepartureRemarks As String = ""
    Dim _GBToursArrivalRemarks As String = ""
    Dim _GBToursAgentRemarks As String = ""
    Dim _GBToursPartyRemarks As String = ""
    Dim _GBAirDepartureRemarks As String = ""
    Dim _GBAirArrivalRemarks As String = ""
    Dim _GBAirAgentRemarks As String = ""
    Dim _GBAirPartyRemarks As String = ""
    Dim _GBTrfsDepartureRemarks As String = ""
    Dim _GBTrfsArrivalRemarks As String = ""
    Dim _GBTrfsAgentRemarks As String = ""
    Dim _GBTrfsPartyRemarks As String = ""
    Dim _CBConfirmXml As String = ""
    Dim _CBCancelXml As String = ""
    Dim _GBdivcode As String = ""
    Dim _GBAgentcode As String = ""
    Dim _GBAgentsourcectrycode As String = ""
    Dim _GBLogintype As String = ""
    Dim _GBVisaArrivalRemarks As String = ""
    Dim _GBVisaAgentRemarks As String = ""
    Dim _GBAgentrefno As String = ""
    Dim _GBColumbusref As String = ""
    Dim _GBBookmode As String
    Dim _GBCancelEntireBooking As String = "0"
    Dim _CBratePlanSource As String = ""
    Dim _CBconnectMarkupXml As String = ""
    Dim _GBErrorStatus As String = ""
    Dim _GBErrorStatusDescription As String = ""
    Dim _GBBookingStatus As String = ""
    Dim _GBPayBookingRef As String = ""

    Dim _strPackageSummary As String = ""
    Dim _strPackageValueSummary As String
    Dim _strCumulative As String

    Dim objDALGuest As New DALGuest
    Public Property PackageSummary As String
        Get
            Return _strPackageSummary
        End Get
        Set(ByVal value As String)
            _strPackageSummary = value
        End Set
    End Property
    Public Property PackageValueSummary As String
        Get
            Return _strPackageValueSummary
        End Get
        Set(ByVal value As String)
            _strPackageValueSummary = value
        End Set
    End Property
    Public Property Cumulative As String
        Get
            Return _strCumulative
        End Get
        Set(ByVal value As String)
            _strCumulative = value
        End Set
    End Property


    Public Property GBCancelEntireBooking As String
        Get
            Return _GBCancelEntireBooking
        End Get
        Set(ByVal value As String)
            _GBCancelEntireBooking = value
        End Set
    End Property

    Public Property GBAgentcode As String
        Get
            Return _GBAgentcode
        End Get
        Set(ByVal value As String)
            _GBAgentcode = value
        End Set
    End Property
    Public Property GBAgentsourcectrycode As String
        Get
            Return _GBAgentsourcectrycode
        End Get
        Set(ByVal value As String)
            _GBAgentsourcectrycode = value
        End Set
    End Property
    Public Property GBLogintype As String
        Get
            Return _GBLogintype
        End Get
        Set(ByVal value As String)
            _GBLogintype = value
        End Set
    End Property
    Public Property GBuserlogged As String
        Get
            Return _GBuserlogged
        End Get
        Set(ByVal value As String)
            _GBuserlogged = value
        End Set
    End Property
    Public Property GBdivcode As String
        Get
            Return _GBdivcode
        End Get
        Set(ByVal value As String)
            _GBdivcode = value
        End Set
    End Property
    Public Property CBConfirmXml As String
        Get
            Return _CBConfirmXml
        End Get
        Set(ByVal value As String)
            _CBConfirmXml = value
        End Set
    End Property
    Public Property CBCancelXml As String
        Get
            Return _CBCancelXml
        End Get
        Set(ByVal value As String)
            _CBCancelXml = value
        End Set
    End Property
    Public Property GBGuestXml As String
        Get
            Return _GBGuestXml
        End Get
        Set(ByVal value As String)
            _GBGuestXml = value
        End Set
    End Property
    Public Property GBServiceXml As String
        Get
            Return _GBServiceXml
        End Get
        Set(ByVal value As String)
            _GBServiceXml = value
        End Set
    End Property
    Public Property GBGuestLineNo As String
        Get
            Return _GBGuestLineNo
        End Get
        Set(ByVal value As String)
            _GBGuestLineNo = value
        End Set
    End Property
    Public Property GBRlineno As String
        Get
            Return _GBRlineno
        End Get
        Set(ByVal value As String)
            _GBRlineno = value
        End Set
    End Property
    Public Property GBRoomno As String
        Get
            Return _GBRoomno
        End Get
        Set(ByVal value As String)
            _GBRoomno = value
        End Set
    End Property
    Public Property GBRequestid As String
        Get
            Return _GBrequestid
        End Get
        Set(ByVal value As String)
            _GBrequestid = value
        End Set
    End Property
    Public Property GBTitle As String
        Get
            Return _GBTitle
        End Get
        Set(ByVal value As String)
            _GBTitle = value
        End Set
    End Property
    Public Property GBFirstName As String
        Get
            Return _GBFirstName
        End Get
        Set(ByVal value As String)
            _GBFirstName = value
        End Set
    End Property

    Public Property GBLastName As String
        Get
            Return _GBLastName
        End Get
        Set(ByVal value As String)
            _GBLastName = value
        End Set
    End Property
    Public Property GBMiddleName As String
        Get
            Return _GBMiddleName
        End Get
        Set(ByVal value As String)
            _GBMiddleName = value
        End Set
    End Property
    Public Property GBNationalityCode As String
        Get
            Return _GBNationalityCode
        End Get
        Set(ByVal value As String)
            _GBNationalityCode = value
        End Set
    End Property
    Public Property GBChildAge As String
        Get
            Return _GBChildAge
        End Get
        Set(ByVal value As String)
            _GBChildAge = value
        End Set
    End Property
    Public Property GBVisaOptions As String
        Get
            Return _GBVisaOptions
        End Get
        Set(ByVal value As String)
            _GBVisaOptions = value
        End Set
    End Property
    Public Property GBVisaPrice As String
        Get
            Return _GBVisaPrice
        End Get
        Set(ByVal value As String)
            _GBVisaPrice = value
        End Set
    End Property



    Public Property GBPassportNo As String
        Get
            Return _GBPassportNo
        End Get
        Set(ByVal value As String)
            _GBPassportNo = value
        End Set
    End Property
    Public Property GBUpdatedDate As String
        Get
            Return _GBUpdatedDate
        End Get
        Set(ByVal value As String)
            _GBUpdatedDate = value
        End Set
    End Property

    Public Property GBUpdatedUser As String
        Get
            Return _GBUpdatedUser
        End Get
        Set(ByVal value As String)
            _GBUpdatedUser = value
        End Set
    End Property
    Public Property GBVisaTypeCode As String
        Get
            Return _GBVisaTypeCode
        End Get
        Set(ByVal value As String)
            _GBVisaTypeCode = value
        End Set
    End Property
    Public Property GBFlightXml As String
        Get
            Return _GBFlightXml
        End Get
        Set(ByVal value As String)
            _GBFlightXml = value
        End Set
    End Property
    Public Property GBDepFlightXml As String
        Get
            Return _GBDepFlightXml
        End Get
        Set(ByVal value As String)
            _GBDepFlightXml = value
        End Set
    End Property
    Public Property GBRemarksTemplate As String
        Get
            Return _GBRemarksTemplate
        End Get
        Set(ByVal value As String)
            _GBRemarksTemplate = value
        End Set
    End Property
    Public Property GBHotelRemarks As String
        Get
            Return _GBHotelRemarks
        End Get
        Set(ByVal value As String)
            _GBHotelRemarks = value
        End Set
    End Property
    Public Property GBAgentRemarks As String
        Get
            Return _GBAgentRemarks
        End Get
        Set(ByVal value As String)
            _GBAgentRemarks = value
        End Set
    End Property
    Public Property GBOthArrivalRemarks As String
        Get
            Return _GBOthArrivalRemarks
        End Get
        Set(ByVal value As String)
            _GBOthArrivalRemarks = value
        End Set
    End Property

    Public Property GBOthDepartureRemarks As String
        Get
            Return _GBOthDepartureRemarks
        End Get
        Set(ByVal value As String)
            _GBOthDepartureRemarks = value
        End Set
    End Property

    Public Property GBOthPartyRemarks As String
        Get
            Return _GBOthPartyRemarks
        End Get
        Set(ByVal value As String)
            _GBOthPartyRemarks = value
        End Set
    End Property
    Public Property GBOthAgentRemarks As String
        Get
            Return _GBOthAgentRemarks
        End Get
        Set(ByVal value As String)
            _GBOthAgentRemarks = value
        End Set
    End Property
    Public Property GBToursArrivalRemarks As String
        Get
            Return _GBToursArrivalRemarks
        End Get
        Set(ByVal value As String)
            _GBToursArrivalRemarks = value
        End Set
    End Property

    Public Property GBToursDepartureRemarks As String
        Get
            Return _GBToursDepartureRemarks
        End Get
        Set(ByVal value As String)
            _GBToursDepartureRemarks = value
        End Set
    End Property

    Public Property GBToursPartyRemarks As String
        Get
            Return _GBToursPartyRemarks
        End Get
        Set(ByVal value As String)
            _GBToursPartyRemarks = value
        End Set
    End Property
    Public Property GBToursAgentRemarks As String
        Get
            Return _GBToursAgentRemarks
        End Get
        Set(ByVal value As String)
            _GBToursAgentRemarks = value
        End Set
    End Property
    Public Property GBArrivalRemarks As String
        Get
            Return _GBArrivalRemarks
        End Get
        Set(ByVal value As String)
            _GBArrivalRemarks = value
        End Set
    End Property

    Public Property GBDepartureRemarks As String
        Get
            Return _GBDepartureRemarks
        End Get
        Set(ByVal value As String)
            _GBDepartureRemarks = value
        End Set
    End Property


    Public Property GBAirArrivalRemarks As String
        Get
            Return _GBAirArrivalRemarks
        End Get
        Set(ByVal value As String)
            _GBAirArrivalRemarks = value
        End Set
    End Property

    Public Property GBAirDepartureRemarks As String
        Get
            Return _GBAirDepartureRemarks
        End Get
        Set(ByVal value As String)
            _GBAirDepartureRemarks = value
        End Set
    End Property

    Public Property GBAirPartyRemarks As String
        Get
            Return _GBAirPartyRemarks
        End Get
        Set(ByVal value As String)
            _GBAirPartyRemarks = value
        End Set
    End Property
    Public Property GBAirAgentRemarks As String
        Get
            Return _GBAirAgentRemarks
        End Get
        Set(ByVal value As String)
            _GBAirAgentRemarks = value
        End Set
    End Property

    Public Property GBTrfsArrivalRemarks As String
        Get
            Return _GBTrfsArrivalRemarks
        End Get
        Set(ByVal value As String)
            _GBTrfsArrivalRemarks = value
        End Set
    End Property

    Public Property GBTrfsDepartureRemarks As String
        Get
            Return _GBTrfsDepartureRemarks
        End Get
        Set(ByVal value As String)
            _GBTrfsDepartureRemarks = value
        End Set
    End Property

    Public Property GBTrfsPartyRemarks As String
        Get
            Return _GBTrfsPartyRemarks
        End Get
        Set(ByVal value As String)
            _GBTrfsPartyRemarks = value
        End Set
    End Property
    Public Property GBTrfsAgentRemarks As String
        Get
            Return _GBTrfsAgentRemarks
        End Get
        Set(ByVal value As String)
            _GBTrfsAgentRemarks = value
        End Set
    End Property
    Public Property GBVisaAgentRemarks As String
        Get
            Return _GBVisaAgentRemarks
        End Get
        Set(ByVal value As String)
            _GBVisaAgentRemarks = value
        End Set
    End Property
    Public Property GBVisaArrivalRemarks As String
        Get
            Return _GBVisaArrivalRemarks
        End Get
        Set(ByVal value As String)
            _GBVisaArrivalRemarks = value
        End Set
    End Property
    Public Property GBAgentrefno As String
        Get
            Return _GBAgentrefno
        End Get
        Set(ByVal value As String)
            _GBAgentrefno = value
        End Set
    End Property
    Public Property GBColumbusref As String
        Get
            Return _GBColumbusref
        End Get
        Set(ByVal value As String)
            _GBColumbusref = value
        End Set
    End Property
    Public Property GBBookmode As String
        Get
            Return _GBBookmode
        End Get
        Set(ByVal value As String)
            _GBBookmode = value
        End Set
    End Property
    Public Property CBratePlanSource As String
        Get
            Return _CBratePlanSource
        End Get
        Set(ByVal value As String)
            _CBratePlanSource = value
        End Set
    End Property
    Public Property CBconnectMarkupXml As String
        Get
            Return _CBconnectMarkupXml
        End Get
        Set(ByVal value As String)
            _CBconnectMarkupXml = value
        End Set
    End Property

    Public Property GBErrorStatus As String
        Get
            Return _GBErrorStatus
        End Get
        Set(ByVal value As String)
            _GBErrorStatus = value
        End Set
    End Property

    Public Property GBErrorStatusDescription As String
        Get
            Return _GBErrorStatusDescription
        End Get
        Set(ByVal value As String)
            _GBErrorStatusDescription = value
        End Set
    End Property

    Public Property GBBookingStatus As String
        Get
            Return _GBBookingStatus
        End Get
        Set(ByVal value As String)
            _GBBookingStatus = value
        End Set
    End Property

    Public Property GBPayBookingRef As String
        Get
            Return _GBPayBookingRef
        End Get
        Set(ByVal value As String)
            _GBPayBookingRef = value
        End Set
    End Property

    Function SavingGuestRemarksInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBGuestLineNo = GBGuestLineNo
        objDALGuest.GBRemarksTemplate = GBRemarksTemplate
        objDALGuest.GBHotelRemarks = GBHotelRemarks
        objDALGuest.GBAgentRemarks = GBAgentRemarks
        objDALGuest.GBArrivalRemarks = GBArrivalRemarks

        objDALGuest.GBDepartureRemarks = GBDepartureRemarks
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingGuestRemarksInTemp()
        Return res
    End Function

    Function SavingOtherServRemarksInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBGuestLineNo = GBGuestLineNo
        objDALGuest.GBOthPartyRemarks = GBOthPartyRemarks
        objDALGuest.GBOthAgentRemarks = GBOthAgentRemarks
        objDALGuest.GBOthArrivalRemarks = GBOthArrivalRemarks

        objDALGuest.GBOthDepartureRemarks = GBOthDepartureRemarks
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingOtherServRemarksInTemp()
        Return res
    End Function

    Function ValidateBooking(ByVal requestid As String, ByVal agentcode As String, ByVal sourcectrycode As String, ByVal logintype As String, Optional ByVal submitquote As Integer = 0, Optional ByVal asGuestName As String = "") As DataSet
        Try

            Dim ds As New DataSet
            'changed by mohamed on 11/09/2018
            ds = objDALGuest.ValidateBooking(requestid, agentcode, sourcectrycode, logintype, submitquote, asGuestName)
            Return ds
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function GetConfirmSummary(ByVal strRequestId As String, ByVal rlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetConfirmSummary(strRequestId, rlineno)
        Return dt
    End Function
    Function GetTransferConfirmSummary(ByVal strRequestId As String, ByVal rlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetTransferConfirmSummary(strRequestId, rlineno)
        Return dt
    End Function

    Function getdefaulttimeforconfirm(ByVal sqlquery As String) As String
        Dim deftime As String
        deftime = objDALGuest.getdefaulttimeforconfirm(sqlquery)
        Return deftime
    End Function
    Function SavingToursRemarksInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBGuestLineNo = GBGuestLineNo
        objDALGuest.GBToursPartyRemarks = GBToursPartyRemarks
        objDALGuest.GBToursAgentRemarks = GBToursAgentRemarks
        objDALGuest.GBToursArrivalRemarks = GBToursArrivalRemarks
        objDALGuest.GBToursDepartureRemarks = GBToursDepartureRemarks
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingToursRemarksInTemp()
        Return res
    End Function
    Function SavingAirportmaRemarksInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBGuestLineNo = GBGuestLineNo
        objDALGuest.GBAirPartyRemarks = GBAirPartyRemarks
        objDALGuest.GBAirAgentRemarks = GBAirAgentRemarks
        objDALGuest.GBAirArrivalRemarks = GBAirArrivalRemarks
        objDALGuest.GBAirDepartureRemarks = GBAirDepartureRemarks
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingAirportmaRemarksInTemp()
        Return res
    End Function
    Function SavingtransfersRemarksInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBGuestLineNo = GBGuestLineNo
        objDALGuest.GBTrfsPartyRemarks = GBTrfsPartyRemarks
        objDALGuest.GBTrfsAgentRemarks = GBTrfsAgentRemarks
        objDALGuest.GBTrfsArrivalRemarks = GBTrfsArrivalRemarks
        objDALGuest.GBTrfsDepartureRemarks = GBTrfsDepartureRemarks
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingtransfersRemarksInTemp()
        Return res
    End Function
    Function SavingVisaRemarksInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBGuestLineNo = GBGuestLineNo
        objDALGuest.GBVisaAgentRemarks = GBVisaAgentRemarks
        objDALGuest.GBVisaArrivalRemarks = GBVisaArrivalRemarks
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingVisaRemarksInTemp()
        Return res
    End Function
    Function SavingConfirmBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBconfirmxml = CBConfirmXml
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingConfirmBookingDetailsInTemp()
        Return res

    End Function
    Function SetRemarksDetFromDataTable(ByVal strSqlQuery As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetRemarksDetFromDataTable(strSqlQuery)
        Return dt
    End Function
    Function SetConfirmDetFromDataTable(ByVal strSqlQuery As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetConfirmDetFromDataTable(strSqlQuery)
        Return dt
    End Function
    Function SavingDepartureFlightTemp() As Boolean
        Dim res As Boolean


        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBRlineno = GBRlineno
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBDepFlightXml = GBDepFlightXml

        res = objDALGuest.SavingDepartureFlightTemp()

        Return res
    End Function

    Function SavingArrivalFlightTemp(Optional ByVal lbNeedToDeleteFlightTemp As Boolean = False) As Boolean 'changed by mohamed on 24/09/2018
        Dim res As Boolean


        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBRlineno = GBRlineno
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBFlightXml = GBFlightXml

        res = objDALGuest.SavingArrivalFlightTemp(lbNeedToDeleteFlightTemp) 'changed by mohamed on 24/09/2018

        Return res
    End Function
    Function FinalSaveBooking() As String
        Dim res As String


        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBdivcode = GBdivcode
        objDALGuest.GBAgentrefno = GBAgentrefno
        objDALGuest.GBColumbusref = GBColumbusref
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBBookmode = GBBookmode
        objDALGuest.GBPayBookingRef = GBPayBookingRef

        res = objDALGuest.FinalSaveBooking()

        Return res
    End Function

    Function FinalQuoteSaveBooking(ByVal requestid As String, ByVal divcode As String, ByVal strAgentRef As String, ByVal strMode As String) As String
        Dim res As String


        objDALGuest.GBrequestid = requestid
        objDALGuest.GBdivcode = divcode
        objDALGuest.Cumulative = Cumulative
        objDALGuest.PackageSummary = PackageSummary
        objDALGuest.PackageValueSummary = PackageValueSummary
        objDALGuest.GBBookmode = strMode
        res = objDALGuest.FinalQuoteSaveBooking(requestid, divcode, strAgentRef)

        Return res
    End Function

    Function SaveBookingProfitInTemp() As String
        Dim res As String

        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.Cumulative = Cumulative
        objDALGuest.PackageSummary = PackageSummary
        objDALGuest.PackageValueSummary = PackageValueSummary

        res = objDALGuest.SaveBookingProfitInTemp()

        Return res
    End Function




    Function SavingGuestBookingInTemp() As Boolean
        Dim res As Boolean


        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBRlineno = GBRlineno
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBGuestXml = GBGuestXml
        objDALGuest.GBFlightXml = GBFlightXml
        objDALGuest.GBServiceXml = GBServiceXml

        res = objDALGuest.SavingGuestBookingInTemp()

        Return res
    End Function

    Function SavingGuestFlightBookingInTemp() As Boolean
        Dim res As Boolean


        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBGuestXml = GBGuestXml
        objDALGuest.GBFlightXml = GBFlightXml

        res = objDALGuest.SavingGuestFlightBookingInTemp()

        ''objDALGuest.GBGuestLineNo = GBGuestLineNo
        ''objDALGuest.GBTitle = GBTitle
        ''objDALGuest.GBFirstName = GBFirstName
        ''objDALGuest.GBMiddleName = GBMiddleName
        ''objDALGuest.GBLastName = GBLastName
        ''objDALGuest.GBNationalityCode = GBNationalityCode
        ''objDALGuest.GBChildAge = GBChildAge

        ''objDALGuest.GBVisaOptions = GBVisaOptions

        ''objDALGuest.GBVisaTypeCode = GBVisaTypeCode

        ''objDALGuest.GBVisaPrice = GBVisaPrice
        ''objDALGuest.GBPassportNo = GBPassportNo
        ''objDALGuest.GBUpdatedDate = GBUpdatedDate
        ''objDALGuest.GBUpdatedUser = GBUpdatedUser



        Return res
    End Function
    Function Fillguests(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.Fillguests(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function Fillguests_new(ByVal requestid As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.Fillguests_new(requestid)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function
    Function HotelEmailRoomcheck(ByVal strRequestId As String, ByVal partycode As String, ByVal amended As String, ByVal cancelled As String, ByVal rlineno As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.HotelEmailRoomcheck(strRequestId, partycode, amended, cancelled, rlineno)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try

    End Function

    Function SavingCancelBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBCancelxml = CBCancelXml
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBCancelEntireBooking = GBCancelEntireBooking
        res = objDALGuest.SavingCancelBookingDetailsInTemp()
        Return res
    End Function

    Function SavingCancelConnectivityBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBRlineno = GBRlineno
        objDALGuest.GBCancelxml = CBCancelXml
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.CBratePlanSource = CBratePlanSource
        objDALGuest.CBconnectMarkupXml = CBconnectMarkupXml
        res = objDALGuest.SavingCancelConnectivityBookingDetailsInTemp()
        Return res
    End Function

    Function SavingTransferConfirmBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBconfirmxml = CBConfirmXml
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingTransferConfirmBookingDetailsInTemp()
        Return res
    End Function

    Function GetTransferConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.GetTransferConfirmSummaryDetails(strRequestId, strRLineNo)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetToursConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetToursConfirmSummary(strRequestId, rlineno)
        Return dt
    End Function

    Function GetToursConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.GetToursConfirmSummaryDetails(strRequestId, strRLineNo)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingToursConfirmBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBconfirmxml = CBConfirmXml
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingToursConfirmBookingDetailsInTemp()
        Return res
    End Function
    Function GetAirportMateConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetAirportMateConfirmSummary(strRequestId, rlineno)
        Return dt
    End Function

    Function GetAirportMateConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.GetAirportMateConfirmSummaryDetails(strRequestId, strRLineNo)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingAirportMateConfirmBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBconfirmxml = CBConfirmXml
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingAirportMateConfirmBookingDetailsInTemp()
        Return res
    End Function


    Function GetOthersConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetOthersConfirmSummary(strRequestId, rlineno)
        Return dt
    End Function

    Function GetOthersConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.GetOthersConfirmSummaryDetails(strRequestId, strRLineNo)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingOthersConfirmBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBconfirmxml = CBConfirmXml
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingOthersConfirmBookingDetailsInTemp()
        Return res
    End Function

    Function GetVisaConfirmSummary(strRequestId As String, rlineno As String) As DataTable
        Dim dt As New DataTable
        dt = objDALGuest.GetVisaConfirmSummary(strRequestId, rlineno)
        Return dt
    End Function

    Function GetVisaConfirmSummaryDetails(strRequestId As String, strRLineNo As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = objDALGuest.GetVisaConfirmSummaryDetails(strRequestId, strRLineNo)
            Return dt
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("BLLGuest:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function SavingVisaConfirmBookingDetailsInTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBconfirmxml = CBConfirmXml
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.SavingVisaConfirmBookingDetailsInTemp()
        Return res
    End Function

    Function GeneratePackageValue(ByVal strRquestId As String, ByVal strAvoidDiscount As String, ByVal strlogintype As String) As DataSet
        Dim ds As DataSet
        ds = objDALGuest.GeneratePackageValue(strRquestId, strAvoidDiscount, strlogintype)
        Return ds
    End Function

    Function CheckSelectedAgentIsCumulative(requestid As String) As Integer
        Dim iCumulative As Integer = 0
        iCumulative = objDALGuest.CheckSelectedAgentIsCumulative(requestid)
        Return iCumulative
    End Function

    Function IsExistGuestFlights(strRequestId As String) As Boolean
        Dim bStatus As Boolean
        bStatus = objDALGuest.IsExistGuestFlights(strRequestId)
        Return bStatus
    End Function

    Function GetAgentRef(strRequestId As String) As String
        Dim strAgentRef As String = objDALGuest.GetAgentRef(strRequestId)
        Return strAgentRef
    End Function

    Function PrearrangedValidate(strRequestId As Object) As Integer
        Return objDALGuest.PrearrangedValidate(strRequestId)
    End Function

    Function ValidateGuestService(strRequestId As String) As DataTable
        Return objDALGuest.ValidateGuestService(strRequestId)
    End Function

    Function UpdateConnectivityBookingFailedConfirmationTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBRlineno = GBRlineno
        objDALGuest.GBErrorStatus = GBErrorStatus
        objDALGuest.GBErrorStatusDescription = GBErrorStatusDescription
        objDALGuest.GBuserlogged = GBuserlogged
        objDALGuest.GBBookingStatus = GBBookingStatus
        res = objDALGuest.UpdateConnectivityBookingFailedConfirmationTemp()
        Return res
    End Function

    Function UpdateConnectivityBookingFailedCancellationTemp() As Boolean
        Dim res As Boolean
        objDALGuest.GBrequestid = GBRequestid
        objDALGuest.GBRlineno = GBRlineno
        objDALGuest.GBErrorStatus = GBErrorStatus
        objDALGuest.GBErrorStatusDescription = GBErrorStatusDescription
        objDALGuest.GBuserlogged = GBuserlogged
        res = objDALGuest.UpdateConnectivityBookingFailedCancellationTemp()
        Return res
    End Function

    Function GeneratePaymentRequestId(ByVal strRequestId As String, ByVal requestAmount As Decimal, ByVal userlogged As String, ByVal bookingNo As String, ByVal payType As String, ByVal paymentMode As String, ByVal RefundPaymentRequestId As String) As String
        Dim paymentRequestId = objDALGuest.GeneratePaymentRequestId(strRequestId, requestAmount, userlogged, bookingNo, payType, paymentMode, RefundPaymentRequestId)
        Return paymentRequestId
    End Function

    Function FinalQuoteCreation(ByVal requestid As String, ByVal divcode As String, ByVal strAgentRef As String, ByVal strMode As String, ByVal quoteId As String, ByVal ExistingQuoteId As String) As String
        Dim res As String
        objDALGuest.GBrequestid = ExistingQuoteId
        objDALGuest.GBdivcode = divcode
        objDALGuest.GBBookmode = strMode
        res = objDALGuest.FinalQuoteCreation(requestid, divcode, strAgentRef, quoteId)
        Return res
    End Function

    Function UpdatePaymentStatus(ByVal paymentRequestId As String, ByVal paymentId As String, ByVal paymentStatus As Boolean, ByVal userlogged As String) As Boolean
        Dim res As Boolean
        res = objDALGuest.UpdatePaymentStatus(paymentRequestId, paymentId, paymentStatus, userlogged)
        Return res
    End Function

    Function CalculatePaidAmount(ByVal tempRequestId As String) As DataTable
        Dim res As DataTable
        res = objDALGuest.CalculatePaidAmount(tempRequestId)
        Return res
    End Function

End Class
