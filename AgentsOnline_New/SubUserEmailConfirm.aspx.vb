Imports System.Data

Partial Class SubUserEmailConfirm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strEmailCode As String = ""
            If Not Request.QueryString("Code") Is Nothing Then
                Dim objclsUtilities As New clsUtilities
                Dim strEmailConfirm As String = objclsUtilities.CheckEmailConfirm(Request.QueryString("Code").ToString)
                If strEmailConfirm = "0" Then
                    UpdateStatus(Request.QueryString("Code").ToString)
                    SendEmail(Request.QueryString("Code").ToString)
                    lblWaring.Text = "Varified succesfully!. Login details has been sent to your e-mail address."
                    lblWaring.Attributes.Add("CssClass", "lblsuccess")

                    dvWarning.Attributes.Add("class", "success")

                ElseIf strEmailConfirm = "1" Then
                    lblWaring.Text = "This user is already activated."
                    lblWaring.Attributes.Add("CssClass", "lblWarning")
                    dvWarning.Attributes.Add("class", "warning")
                Else
                    lblWaring.Text = "Invalid varification link."
                    lblWaring.Attributes.Add("CssClass", "lblWarning")
                    dvWarning.Attributes.Add("class", "warning")
                    btnLogin.Visible = False
                End If
            Else
                lblWaring.Text = "Invalid varification link."
                lblWaring.Attributes.Add("CssClass", "lblWarning")
                dvWarning.Attributes.Add("class", "warning")
                btnLogin.Visible = False
            End If
        End If

    End Sub

    Private Sub UpdateStatus(strEmailCode As String)
        Dim objBLLSubUsers As New BLLSubUsers()
        If objBLLSubUsers.UpdateEmailConfirmStatus(strEmailCode) Then

        End If
    End Sub

    Private Sub SendEmail(strEmailCode As String)
        Dim dtSubUserDetails As DataTable
        Dim objBLLSubUsers As New BLLSubUsers()
        Dim objclsUtilities As New clsUtilities()
        dtSubUserDetails = objBLLSubUsers.GetSubUsersListFromSubAgentCode(strEmailCode)
        If Not dtSubUserDetails Is Nothing Then
            If dtSubUserDetails.Rows.Count > 0 Then
                Dim strBody As String = ""
                strBody = "<html><table  style='width:100%;border-width:1px;border-color:black;border-style:solid;'><tr><td><table  style='width:100%'><tr><td style='width:100%;background-color:Black;height:30px;'></td></tr> "
                strBody += "<tr><td style='width:100%;background-color:White;height:50px;'></td></tr>"
                strBody += "<tr><td style='width:750px;background:#E6E6E6;padding-left:50px;font-family:Raleway;text-decoration:none;text-align: justify;'>"
                strBody += "<br/>Dear " & dtSubUserDetails.Rows(0)("SubUserName").Trim & " <br/>"
                strBody += "<br/>Greeting from " & dtSubUserDetails.Rows(0)("agentname").Trim & "<br/>"
                strBody += "<br/>Your registration is completed. Now you can login to your account using below details.<br/>"
                strBody += "<br/> User Name :" & dtSubUserDetails.Rows(0)("agentsubcode").Trim & "<br/>"
                strBody += "<br/> Password :" & dtSubUserDetails.Rows(0)("pwd").Trim & "<br/>"
                strBody += "<br/><br/>  Thank you & best Regards <br/> System Administrator <br/> " & dtSubUserDetails.Rows(0)("agentname").Trim & ""
                strBody += "<br/><br/></td></tr>"
                strBody += "<tr><td style='width:100%;background-color:Black;height:20px;'></td></tr></table></td></tr></table></html>"
                Dim strSubUserCode As String = dtSubUserDetails.Rows(0)("agentsubcode").Trim
                Dim objEmail As New clsEmail


                Dim strFromEmailid As String = objclsUtilities.ExecuteQueryReturnStringValue("Select fromemailid from email_text where emailtextfor=1")
                Dim Emailmode As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_selected from reservation_parameters  where param_id=2007")
                Dim TestEmail As String = objclsUtilities.ExecuteQueryReturnStringValue("Select option_value from reservation_parameters  where param_id=2007")
                If Emailmode = "Test" Then
                    strSubUserCode = TestEmail
                End If

                If objEmail.SendEmailOnline(strFromEmailid, strSubUserCode, "", "Registration Completed", strBody, "") Then
                    objclsUtilities.SendEmailNotification("REGISTER", strFromEmailid, strSubUserCode, "", "", "Registration Completed", strBody, "1", "1", "", "Y", "", "SUB USER REGISTERATION-CONFIRM")
                Else
                    objclsUtilities.SendEmailNotification("REGISTER", strFromEmailid, strSubUserCode, "", "", "Registration Completed", strBody, "1", "1", "", "N", "", "SUB USER REGISTERATION-CONFIRM")
                End If
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As System.EventArgs) Handles btnLogin.Click
        Response.Redirect("Login.aspx")
    End Sub
End Class
