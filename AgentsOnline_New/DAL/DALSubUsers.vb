Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class DALSubUsers
    Dim objclsUtilities As New clsUtilities
    Dim strAgentCode As String = ""
    Dim strAgentSubCode As String = ""
    Dim strPassword As String = ""
    Dim strSubUserName As String = ""
    Dim strSubUserEmail As String = ""
    Dim strAddress As String = ""
    Dim strTelephone As String = ""
    Dim strFax As String = ""
    Dim stradduser As String = ""
    Dim strmoduser As String = ""
    Dim stractive As String = ""
    Dim strOPMode As String = ""
    Dim strEmailConfirm As String = ""

    Public Property AgentCode As String
        Get
            Return strAgentCode
        End Get
        Set(value As String)
            strAgentCode = value
        End Set
    End Property
    Public Property AgentSubCode As String
        Get
            Return strAgentSubCode
        End Get
        Set(value As String)
            strAgentSubCode = value
        End Set
    End Property
    Public Property Password As String
        Get
            Return strPassword
        End Get
        Set(value As String)
            strPassword = value
        End Set
    End Property
    Public Property SubUserName As String
        Get
            Return strSubUserName
        End Get
        Set(value As String)
            strSubUserName = value
        End Set
    End Property
    Public Property Address As String
        Get
            Return strAddress
        End Get
        Set(value As String)
            strAddress = value
        End Set
    End Property
    Public Property Telephone As String
        Get
            Return strTelephone
        End Get
        Set(value As String)
            strTelephone = value
        End Set
    End Property

    Public Property Fax As String
        Get
            Return strFax
        End Get
        Set(value As String)
            strFax = value
        End Set
    End Property
    Public Property LoggedUser As String
        Get
            Return stradduser
        End Get
        Set(value As String)
            stradduser = value
        End Set
    End Property
    Public Property Active As String
        Get
            Return stractive
        End Get
        Set(value As String)
            stractive = value
        End Set
    End Property
    Public Property OPMode As String
        Get
            Return strOPMode
        End Get
        Set(value As String)
            strOPMode = value
        End Set
    End Property
    Public Property EmailConfirm As String
        Get
            Return strEmailConfirm
        End Get
        Set(value As String)
            strEmailConfirm = value
        End Set
    End Property
    Function GetWebAgentList(strAgentCode As String, strAgentName As String, strShortName As String, strCountryCode As String) As DataTable
        Dim objDataTable As DataTable
        Dim strQuery As String = ""
        Dim strWhereCond As String = ""
        strQuery = "select * from V_WEB_AGENTS "
        If strAgentCode <> "" Then
            If strWhereCond <> "" Then
                strWhereCond = strWhereCond & " and agentcode like '%" & strAgentCode & "%' "
            Else
                strWhereCond = "where agentcode like '%" & strAgentCode & "%' "
            End If
        End If
        If strAgentName <> "" Then
            If strWhereCond <> "" Then
                strWhereCond = strWhereCond & " and agentcode like '%" & strAgentName & "%' "
            Else
                strWhereCond = "where agentcode like '%" & strAgentName & "%' "
            End If
        End If
        If strShortName <> "" Then
            If strWhereCond <> "" Then
                strWhereCond = strWhereCond & " and shortname like '%" & strShortName & "%' "
            Else
                strWhereCond = "where shortname like '%" & strShortName & "%' "
            End If
        End If
        If strCountryCode <> "" Then
            If strWhereCond <> "" Then
                strWhereCond = strWhereCond & " and ctrycode like '%" & strCountryCode & "%' "
            Else
                strWhereCond = "where ctrycode like '%" & strCountryCode & "%' "
            End If
        End If
        strQuery = strQuery & strWhereCond & " order by agentname"
        objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Return objDataTable
    End Function

    Function GetSubUsersList(strAgentCode As String) As DataTable
        Dim objDataTable As DataTable
        Dim strQuery As String = ""
        strQuery = "select * from V_SUBUSER_LIST where agentcode='" & strAgentCode & "' order by SubUserName"
        objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Return objDataTable
    End Function

    Function IsExistSubUser(strEmailId As String, strId As String) As Boolean

        Dim strQuery As String = ""
        If strId <> "" Then
            strQuery = "select count('t') from agents_subusers where upper(AgentSubCode)=upper('" & strEmailId & "') and Id<>'" & strId & "' "

        Else
            strQuery = "select count('t') from agents_subusers where upper(AgentSubCode)=upper('" & strEmailId & "')"
        End If

        Dim strCount As String = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
        If strCount = "0" Then
            Return False
        Else
            Return True
        End If
    End Function

    Function SaveSubUserDetails() As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "Sp_AddModSubUsers"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(9) As SqlParameter
            parm(0) = New SqlParameter("@AgentCode", CType(AgentCode, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@AgentSubCode", CType(AgentSubCode, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@Password", CType(Password, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@SubUserName", CType(SubUserName, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@Address", CType(Address, String))
            sqlParamList.Add(parm(4))
            parm(5) = New SqlParameter("@Telephone", CType(Telephone, String))
            sqlParamList.Add(parm(5))
            parm(6) = New SqlParameter("@Fax", CType(Fax, String))
            sqlParamList.Add(parm(6))
            parm(7) = New SqlParameter("@LoggedUser", CType(LoggedUser, String))
            sqlParamList.Add(parm(7))
            parm(8) = New SqlParameter("@active", CType(Active, String))
            sqlParamList.Add(parm(8))
            parm(8) = New SqlParameter("@EmailConfirm", CType(EmailConfirm, String))
            sqlParamList.Add(parm(8))
            parm(9) = New SqlParameter("@OpMode", CType(OPMode, String))
            sqlParamList.Add(parm(9))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetMenuList(ByVal strMenuType As String, ByVal logintype As String) As DataTable
        Dim objDataTable As DataTable
        Dim strQuery As String = ""
        If logintype = "RO" Then
            strQuery = "select * from View_AgentMenuMaster where menu_type='" & strMenuType & "' order by parentid,menuid"
        Else
            strQuery = "select * from View_AgentMenuMaster where menu_type='" & strMenuType & "' and showornot=1 order by parentid,menuid"
        End If
        objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Return objDataTable
    End Function

    Function SaveUserRights(strUserType As String, strAgentCode As String, strSubUserCode As String, strUserRights As String, strLoggedUser As String) As Boolean
        Dim mySqlConn As New SqlConnection
        Dim mysqlTrans As SqlTransaction
        Try
            Dim constring As String = ConfigurationManager.ConnectionStrings("strDBConnection").ConnectionString
            mySqlConn = clsDBConnect.dbConnectionnew("strDBConnection")
            'connection open
            mysqlTrans = mySqlConn.BeginTransaction
            Dim ProcName As String
            ProcName = "sp_Add_Mod_Agents_Subuser_Rights"
            Dim sqlParamList As New List(Of SqlParameter)
            Dim parm(5) As SqlParameter
            parm(0) = New SqlParameter("@UserType", CType(strUserType, String))
            sqlParamList.Add(parm(0))
            parm(1) = New SqlParameter("@AgentCode", CType(strAgentCode, String))
            sqlParamList.Add(parm(1))
            parm(2) = New SqlParameter("@SubUserCode", CType(strSubUserCode, String))
            sqlParamList.Add(parm(2))
            parm(3) = New SqlParameter("@UserRightXml", CType(strUserRights, String))
            sqlParamList.Add(parm(3))
            parm(4) = New SqlParameter("@UserLogged", CType(strLoggedUser, String))
            sqlParamList.Add(parm(4))

            objclsUtilities.ExecuteNonQuerynew(constring, ProcName, sqlParamList, mySqlConn, mysqlTrans)
            mysqlTrans.Commit()    'SQl Tarn Commit
            clsDBConnect.dbSqlTransation(mysqlTrans)              ' sql Transaction disposed 
            'clsDBConnect.dbCommandClose(mySqlCmd)               'sql command disposed
            clsDBConnect.dbConnectionClose(mySqlConn)
            Return True
        Catch ex As Exception

            mysqlTrans.Rollback()

            objclsUtilities.WriteErrorLog("DALMASearch:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return False
        End Try
        Return False
    End Function

    Function GetActiveMenuList(strUserType As String, strAgentCode As String, strSubUserCode As String) As DataTable
        Dim strQuery As String = ""
        Dim objDataTable As DataTable
        If strUserType = "Agent" Then
            strQuery = "select * from agents_subuser_rights where agentcode='" & strAgentCode & "' and agentsubcode=''"
        ElseIf strUserType = "SubUser" Then
            strQuery = "select * from agents_subuser_rights where agentcode='" & strAgentCode & "' and agentsubcode='" & strSubUserCode & "'"
        End If
        objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Return objDataTable
    End Function

    Function UpdateEmailConfirmStatus(strEmailCode As String) As Boolean
        Dim objclsUtilities As New clsUtilities()
        Dim strDCode As String = objclsUtilities.Decrypt(strEmailCode)
        Dim strQuery As String = "update agents_subusers set EmailConfirm=1 where AgentSubCode='" & strDCode & "'"
        Dim iStatus As Integer = objclsUtilities.AddUpdateDeleteSQL(strQuery)
        Return True
    End Function
    Function GetSubUsersListFromSubAgentCode(strSubAgentCode As String) As DataTable
        Dim objDataTable As DataTable
        Dim strQuery As String = ""
        Dim objclsUtilities As New clsUtilities()
        Dim strDCode As String = objclsUtilities.Decrypt(strSubAgentCode)
        strQuery = "select * from V_SUBUSER_LIST where AgentSubCode='" & strDCode & "'"
        objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
        Return objDataTable
    End Function
End Class
