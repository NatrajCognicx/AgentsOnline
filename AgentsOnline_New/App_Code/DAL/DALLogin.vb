Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class DALLogin
    Dim objclsUtilities As New clsUtilities
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strCompany"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAgentLogo(strCompany As String) As String
        Try
            Dim strLogo As String = ""
        Dim strQuery As String = "select logofilename from agentmast_whitelabel where randomnumber=" & strCompany & ""
        strLogo = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strLogo
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function

    Function ValidateUser(LoginType As String, UserName As String, Password As String, SubUser As String, IpAddress As String, ShortName As String, DivCode As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_validate_agentsonline_login"
            Dim parm(6) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@mainuser", CType(UserName, String))
            parm(2) = New SqlParameter("@subuser", CType(SubUser, String))
            parm(3) = New SqlParameter("@password", CType(Password, String))
            parm(4) = New SqlParameter("@ipaddress", CType(IpAddress, String))
            parm(5) = New SqlParameter("@shortname", CType(ShortName, String))
            parm(6) = New SqlParameter("@DivCode", CType(DivCode, String))
            'Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            If dt.Rows.Count = 0 Then
                Return dt
            Else
                Return dt
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return dt
        End Try
    End Function


    Function ValidateUserWithoutPassword(ByVal LoginType As String, ByVal UserName As String, ByVal Password As String, ByVal SubUser As String, ByVal IpAddress As String, ByVal ShortName As String, ByVal DivCode As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim ProcName As String
            ProcName = "sp_validate_agentsonline_loginIDP"
            Dim parm(5) As SqlParameter
            parm(0) = New SqlParameter("@logintype", CType(LoginType, String))
            parm(1) = New SqlParameter("@mainuser", CType(UserName, String))
            parm(2) = New SqlParameter("@subuser", CType(SubUser, String))
            parm(3) = New SqlParameter("@ipaddress", CType(IpAddress, String))
            parm(4) = New SqlParameter("@shortname", CType(ShortName, String))
            parm(5) = New SqlParameter("@DivCode", CType(DivCode, String))
            'Dim dt As New DataTable
            dt = objclsUtilities.GetDataTable(ProcName, parm)
            If dt.Rows.Count = 0 Then
                Return dt
            Else
                Return dt
            End If
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return dt
        End Try
    End Function

    Function GetPhoneNo() As String
        Try
            Dim strPhoneNo As String = ""
            Dim strQuery As String = "select top 1 cotel from columbusmaster"
            strPhoneNo = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strPhoneNo
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function

    Function GetShortName(strCompany As String) As Object
        Try
            Dim strShortName As String = ""
            Dim strQuery As String = "select shortname from agentmast where agentcode = (select agentcode from agentmast_whitelabel where randomnumber='" & strCompany & "') "
            strShortName = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strShortName
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function
    Function GetAgentName(strCompany As String) As Object
        Try
            Dim strAgentName As String = ""
            Dim strQuery As String = "select agentname from agentmast where agentcode = (select agentcode from agentmast_whitelabel where randomnumber='" & strCompany & "') "
            strAgentName = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strAgentName
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function

    Function LoadLoginPageFields(strCompany As String, strLoginType As String, strAgentCode As String, strUserName As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""

            If strUserName = "LOGIN" Then
                If strLoginType = "RO" Then
                    strQuery = "select logofilename,case when a.Owncompany=1 then '' else am.shortname end shortname,tel1,agentname companyname,agentname from agentmast_whitelabel a,agentmast am  where a.agentcode=am.agentcode and randomnumber='" & strCompany & "'"
                Else
                    strQuery = "select logofilename,case when a.Owncompany=1 then '' else am.shortname end shortname,(select ams.tel1 from agentmast ams where ams.agentcode='" & strAgentCode & "') tel1,agentname companyname,(select ams.agentname from agentmast ams where ams.agentcode='" & strAgentCode & "') agentname from agentmast_whitelabel a,agentmast am  where a.agentcode=am.agentcode and randomnumber='" & strCompany & "'"
                End If
            Else
                If strLoginType = "RO" Then
                    strQuery = "select logofilename,case when a.Owncompany=1 then '' else am.shortname end shortname,um.ustel tel1,agentname companyname,agentname from agentmast_whitelabel a,agentmast am,UserMaster um  where a.agentcode=am.agentcode and um.UserCode='" & strUserName & "' and randomnumber='" & strCompany & "'"
                Else
                    strQuery = "select logofilename,case when a.Owncompany=1 then '' else am.shortname end shortname,(select ams.tel1 from agentmast ams where ams.agentcode='" & strAgentCode & "') tel1,agentname companyname,(select ams.agentname from agentmast ams where ams.agentcode='" & strAgentCode & "') agentname from agentmast_whitelabel a,agentmast am  where a.agentcode=am.agentcode and randomnumber='" & strCompany & "'"
                End If
            End If


            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function LoadLoginPageSessionFields(strCompany As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select agentcode,agentname,shortname,currcode,ctrycode,tel1,email,webusername from agentmast where agentcode in (select agentcode from agentmast_whitelabel where randomnumber='" & strCompany & "')"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function
    Function LoadLoginPageSessionFieldsAgents(strUserName As String, strPassword As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = "select agentcode,agentname,shortname,case when (select count(w.agentcode) from agentmast_whitelabel w where w.agentcode=agentmast.agentcode )>0 then (select min(w.wlcurrcode) from agentmast_whitelabel w where w.agentcode=agentmast.agentcode) else currcode end currcode,ctrycode,tel1,email,webusername from agentmast where webusername='" & strUserName & "' and dbo.pwddecript(webpassword)='" & strPassword & "'"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetUserWebMail(UserName As String, ShortName As String, MainUser As String, SubUser As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            'select usertype,case when option_selected='Test' then option_value else webemail end webemail, webusername,shortname,agentcode from view_agentsonline_login v ,reservation_parameters rp where param_id=2007 and param_desc='EmailMode' 

            If SubUser <> "" Then
                strQuery = "select usertype,case when option_selected='Test' then option_value else webemail end webemail, webusername,shortname,agentcode,option_selected,case when option_selected='Test' then '' else (select option_selected from reservation_parameters where param_id=1006) end CcMail,case when option_selected='Test' then '' else (select option_selected from reservation_parameters where param_id=1075) end AdminMail,Name,(select fromemailid from email_text)fromemailid, (select a.logofilename from agentmast_whitelabel a where a.agentcode=v.agentcode)logofilename from view_agentsonline_login v ,reservation_parameters rp  where param_id=2007 and param_desc='EmailMode' and webusername='" & UserName & "' and shortname='" & ShortName & "' and usertype='SubUser'"
            Else
                strQuery = "select usertype,case when option_selected='Test' then option_value else webemail end webemail, webusername,shortname,agentcode,option_selected,case when option_selected='Test' then '' else (select option_selected from reservation_parameters where param_id=1006) end CcMail,case when option_selected='Test' then '' else (select option_selected from reservation_parameters where param_id=1075) end AdminMail,Name,(select fromemailid from email_text)fromemailid , (select a.logofilename from agentmast_whitelabel a where a.agentcode=v.agentcode)logofilename  from view_agentsonline_login v ,reservation_parameters rp  where param_id=2007 and param_desc='EmailMode' and webusername='" & UserName & "' and shortname='" & ShortName & "' and usertype='MAIN'"
            End If
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetRandomPassword() As String
        Dim strPassword As String = ""
        Try
            strPassword = objclsUtilities.GetRandomPassword()
            Return strPassword
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return strPassword
        End Try
    End Function

    Function UpdateAgentPassword(UserName As String, ShortName As String, password As String, strUser As String) As Integer
        Dim strQuery As String
        If strUser = "MAIN" Then
            strQuery = "update agentmast set webpassword=dbo.pwdencript('" & password & "')  where active=1 and webusername='" & UserName & "' and shortname='" & ShortName & "'"
        Else
            strQuery = "Update agents_subusers set pass_word=dbo.pwdencript('" & password & "')  where active =1 and agent_sub_code='" & UserName & "' and agentcode='" & ShortName & "' "
        End If
        Dim iStatus As Integer = objclsUtilities.AddUpdateDeleteSQL(strQuery)
        Return iStatus

    End Function

    Sub PwdSendMailLog_Entry(AgentCode As String, strToMail As String, Pagename As String, userName As String)
        objclsUtilities.PwdSendMailLog_Entry(AgentCode, strToMail, Pagename, userName)
    End Sub

    Function getTestMail() As String
        Try
            Dim strTestMail As String = ""
            Dim strQuery As String = "select case when option_selected='Test' then option_value else '' end TestMail  from reservation_parameters where param_id='2007'"
            strTestMail = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
            Return strTestMail
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return ""
        End Try
    End Function

    Function GetEmailParameters(strCompany As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select (select case when option_selected='Test' then option_value else '' end  from reservation_parameters where param_id='2007')TestMail,(select fromemailid from email_text  where emailtextfor=0)fromemailid,(select agentname from agentmast where agentcode = (select agentcode from agentmast_whitelabel where randomnumber='" & strCompany & "'))agentname"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function ValidateROUsers(ByVal UserName As String, ByVal Password As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "SELECT UserCode,dbo.pwddecript(userpwd) userpwd FROM usermaster WHERE UserCode='" & UserName & "' AND dbo.pwddecript(userpwd)='" & Password & "' AND active=1"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function ValidateROUsers(ByVal UserName As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "SELECT UserCode FROM usermaster WHERE UserCode='" & UserName & "' AND active=1"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetReservationParamValues(strParams As String) As DataTable
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            strQuery = "select distinct * from reservation_parameters where param_id in (" & strParams & ")"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            Return objDataTable
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetTermsAndCondions(strDivCode As String) As String
        Try
            Dim objDataTable As DataTable
            Dim strQuery As String = ""
            Dim strTermsAndCondions As String = ""
            strQuery = "select detail_text from webdetails where divcode='" & strDivCode & "'"
            objDataTable = objclsUtilities.GetDataFromDataTable(strQuery)
            If objDataTable.Rows.Count > 0 Then
                strTermsAndCondions = objDataTable.Rows(0)("detail_text").ToString
            Else
                strTermsAndCondions = ""
            End If
            Return strTermsAndCondions
        Catch ex As Exception
            objclsUtilities.WriteErrorLog("DALLogin:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString)
            Return Nothing
        End Try
    End Function

    Function GetUnderConstructionValue() As String
        Dim strQuery As String = ""
        Dim strUnderConstruction As String = ""
        strQuery = "select option_selected from reservation_parameters where param_id=2010"
        strUnderConstruction = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
        Return strUnderConstruction
    End Function

    Function ValidateOldPassword(strUserName As String, strOldPassword As String, strLoginType As String) As String
        Dim strQuery As String = ""
        Dim strValidateOldPassword As String = ""
        If strLoginType = "RO" Then
            strQuery = "select count(UserCode) from usermaster(nolock) where UserCode='" & strUserName & "' and userpwd=dbo.pwdencript('" & strOldPassword & "')"
        Else
            strQuery = "select count(agentcode) from agentmast(nolock) where webusername='" & strUserName & "' and webpassword=dbo.pwdencript('" & strOldPassword & "')"
        End If

        strValidateOldPassword = objclsUtilities.ExecuteQueryReturnStringValue(strQuery)
        Return strValidateOldPassword

    End Function

    Function ChangePassword(strUserName As String, strOldPassword As String, strNewPassword As String, strLoginType As Object) As String
        Dim strQuery As String
        If strLoginType = "RO" Then
            strQuery = "update usermaster set userpwd=dbo.pwdencript('" & strNewPassword & "') where UserCode='" & strUserName & "' and userpwd=dbo.pwdencript('" & strOldPassword & "')"
        Else
            strQuery = "update agentmast set webpassword=dbo.pwdencript('" & strNewPassword & "') where webusername='" & strUserName & "' and webpassword=dbo.pwdencript('" & strOldPassword & "')"
        End If

        Dim strStatus As String = objclsUtilities.AddUpdateDeleteSQL(strQuery)
        Return strStatus

    End Function

   

End Class
