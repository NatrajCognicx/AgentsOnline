Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Net.Mail
Imports System.Web.HttpServerUtility
Imports System.Net

Public Class clsEmail
    Dim strQry As String
    Dim strWhereCond As String
    Dim strInnerWhereCond As String
    Dim strMailMsg As String
    Dim strSubject As String
    Dim flgSendMail As Boolean

    Dim objclsUtilities As New clsUtilities
    Public Function SendEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMsg As String) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Try
            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            Mail_Message.To.Add(strTo)
            Mail_Message.Subject = strSubject

            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True

            msClient.Port = 25
            msClient.Host = "127.0.0.1"
            msClient.Credentials = New System.Net.NetworkCredential("", "") '





            'comment by csn since no firewall need to uncomment on instalation
            'msClient.Send(Mail_Message)
            SendEmail = True
        Catch ex As Exception
            SendEmail = False
        End Try
    End Function

    Public Function SendEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Try
            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            Mail_Message.To.Add(strTo)
            'Set Subject
            'Dim attachFile As New Attachment(txtAttachmentPath.Text)
            'MyMessage.Attachments.Add(attachFile)

            Mail_Message.Subject = strSubject
            'Set Msg Body
            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True
            Mail_Message.Attachments.Add(New Attachment(attchemnt))
            msClient.Port = 25
            msClient.Host = "127.0.0.1"

            '' Create file attachment
            'Dim ImageAttachment As Attachment = New Attachment(Server.MapPath("/Images/Logo.jpg"))
            '' Set the ContentId of the attachment, used in body HTML
            'ImageAttachment.ContentId = "Logo.jpg"

            '' Add an image as file attachment
            'Mail_Message.Attachments.Add(ImageAttachment)

            msClient.Credentials = New System.Net.NetworkCredential("", "") '
            'comment by csn since no firewall need to uncomment on instalation

            'msClient.Send(Mail_Message)
            Mail_Message.Dispose()
            SendEmail = True
        Catch ex As Exception
            Mail_Message.Dispose()
            SendEmail = False
        End Try
    End Function
    Public Function SendEmailCC(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Dim strarr() As String
        Dim strarrCC() As String

        Dim i, j As Integer
        Try
            strarr = strTo.Split(",")
            strarrCC = strToCC.Split(",")


            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            j = 0
            For i = 1 To strarr.Length
                Mail_Message.To.Add(strarr(j))
                j = j + 1
            Next

            If strToCC <> "" Then
                j = 0
                For i = 1 To strarrCC.Length
                    Mail_Message.CC.Add(strarrCC(j))
                    j = j + 1
                Next
            End If

            'Set Subject
            'Dim attachFile As New Attachment(txtAttachmentPath.Text)
            'MyMessage.Attachments.Add(attachFile)

            Mail_Message.Subject = strSubject
            'Set Msg Body
            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True
            msClient.Port = 25



            'comment by csn since no firewall need to uncomment on instalation
            'msClient.Send(Mail_Message)

            'Local Testing Start
            msClient.Port = 25
            msClient.Host = "smtp.mahce.com"
            msClient.Credentials = New System.Net.NetworkCredential("mahcefax@gmail.com", "Mahce123") '
            msClient.Send(Mail_Message)
            'Local Testing End

            SendEmailCC = True
        Catch ex As Exception
            SendEmailCC = False
        End Try
    End Function
    Public Function SendEmailCC(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String) As Boolean
        Try
            ' Create new mail message
            Dim MyMail As MailMessage = New MailMessage()
            ' Set recipient email, you can use Add() method to add 
            ' more than one recipient
            MyMail.To.Add(strTo)
            ' Set sender email address
            MyMail.From = New MailAddress("info@mahce.com", strFrom)

            ' Set subject of an email
            MyMail.Subject = strSubject
            ' Create mail body string. Body includes an image tag
            MyMail.Body = strMsg

            ' Set mail body as HTML
            MyMail.IsBodyHtml = True

            ' Create file attachment
            If attchemnt <> "" Then
                Dim ImageAttachment As Attachment = New Attachment(attchemnt)
                ' Set the ContentId of the attachment, used in body HTML
                ImageAttachment.ContentId = "mahce_logo.jpg"

                ' Add an image as file attachment
                MyMail.Attachments.Add(ImageAttachment)
            End If


            ' Create instance of Smtp client
            Dim MySmtpClient As SmtpClient = New SmtpClient("smtp.office365.com")
            ' If your ISP required it, set user name and password
            MySmtpClient.Credentials = New System.Net.NetworkCredential("info@mahce.com", "Lafu2206")
            MySmtpClient.Port = "587"
            ' Finally, send created email
            MySmtpClient.Send(MyMail)



            SendEmailCC = True
        Catch ex As Exception
            SendEmailCC = False
        End Try
    End Function

    Public Function SendEmailOnlinenew(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String, ByVal strfromusername As String, ByVal strfrompwd As String, ByVal asSmtpAddress As String, ByVal asPortNo As Integer) As Boolean


        Try
            ' Create new mail message
            Dim MyMail As MailMessage = New MailMessage()

            Dim strarr() As String
            Dim strarrCC() As String
            Dim i, j As Integer



            ' Set recipient email, you can use Add() method to add 
            ' more than one recipient
            strTo = strTo.Replace(";", ",")
            strToCC = strToCC.Replace(";", ",")

            strarr = strTo.Split(",")
            strarrCC = strToCC.Split(",")


            If strTo <> "" Then
                j = 0
                For i = 1 To strarr.Length
                    MyMail.To.Add(strarr(j))
                    j = j + 1
                Next
            End If

            If strToCC <> "" Then
                j = 0
                For i = 1 To strarrCC.Length
                    MyMail.CC.Add(strarrCC(j))
                    j = j + 1
                Next
            End If

            ' Set sender email address

            MyMail.From = New MailAddress(strfromusername, strFrom)

            ' MyMail.From = New MailAddress(strFrom)

            ' Set subject of an email
            MyMail.Subject = strSubject
            ' Create mail body string. Body includes an image tag
            MyMail.Body = strMsg

            ' Set mail body as HTML
            MyMail.IsBodyHtml = True

            ' Create file attachment
            If attchemnt <> "" Then
                'Dim ImageAttachment As Attachment = New Attachment(attchemnt)
                ''ImageAttachment.ContentId = "Logo.png"

                '' Add an image as file attachment
                'MyMail.Attachments.Add(ImageAttachment)

                Dim strarrAttach() As String
                strarrAttach = attchemnt.Split(";")
                For i = 0 To strarrAttach.Length - 1
                    MyMail.Attachments.Add(New Attachment(strarrAttach(i)))
                Next
            End If











            Dim MySmtpClient As New SmtpClient

            'If divcode = "01" Then


            Dim MyNetworkCredential As New NetworkCredential(strfromusername, strfrompwd)
            MySmtpClient.UseDefaultCredentials = False
            MySmtpClient.Credentials = MyNetworkCredential
            MySmtpClient.Host = asSmtpAddress '"smtp.office365.com"
            MySmtpClient.Port = asPortNo '25
            MySmtpClient.EnableSsl = True










            ' Finally, send created email
            MySmtpClient.Send(MyMail)


            SendEmailOnlinenew = True
        Catch ex As Exception
            SendEmailOnlinenew = False
            Dim strFaildRecepient As String = ""
            Dim strError As String '= ex.InnerException.Message 'changed by mohamed on 23/08/2018
            If ex.InnerException IsNot Nothing Then
                strError = ex.InnerException.Message
                objclsUtilities.WriteErrorLog("clsEmail:: " & ex.InnerException.StackTrace.ToString)
            Else
                strError = ex.Message
                objclsUtilities.WriteErrorLog("clsEmail:: " & ex.StackTrace.ToString)
            End If

            'Failure sending mail. :: smtp issue
            If strError = "Mailbox unavailable. The server response was: unrouteable address" Then 'Unable to send to a recipient.

                strFaildRecepient = DirectCast(ex, System.Net.Mail.SmtpFailedRecipientsException).FailedRecipient
                strFaildRecepient = "Failed Reciepient: " & strFaildRecepient

                SendEmailOnlinenew = True
            End If
            objclsUtilities.WriteErrorLog("clsEmail:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString & " :: " & strFaildRecepient)

        End Try
    End Function

    Public Function SendEmailOnline(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String) As Boolean
        Try
            ' Create new mail message
            Dim MyMail As MailMessage = New MailMessage()

            Dim strarr() As String
            Dim strarrCC() As String
            Dim i, j As Integer



            ' Set recipient email, you can use Add() method to add 
            ' more than one recipient
            strTo = strTo.Replace(";", ",")
            strToCC = strToCC.Replace(";", ",")

            strarr = strTo.Split(",")
            strarrCC = strToCC.Split(",")


            If strTo <> "" Then
                j = 0
                For i = 1 To strarr.Length
                    MyMail.To.Add(strarr(j))
                    j = j + 1
                Next
            End If

            If strToCC <> "" Then
                j = 0
                For i = 1 To strarrCC.Length
                    MyMail.CC.Add(strarrCC(j))
                    j = j + 1
                Next
            End If

            ' Set sender email address
            MyMail.From = New MailAddress("info@mahce.com", strFrom)
            'MyMail.From = New MailAddress("arun@royalpark.net", strFrom)

            ' MyMail.From = New MailAddress(strFrom)

            ' Set subject of an email
            MyMail.Subject = strSubject
            ' Create mail body string. Body includes an image tag
            MyMail.Body = strMsg

            ' Set mail body as HTML
            MyMail.IsBodyHtml = True

            ' Create file attachment
            If attchemnt <> "" Then
                'Dim ImageAttachment As Attachment = New Attachment(attchemnt)
                ''ImageAttachment.ContentId = "Logo.png"

                '' Add an image as file attachment
                'MyMail.Attachments.Add(ImageAttachment)

                Dim strarrAttach() As String
                strarrAttach = attchemnt.Split(";")
                For i = 0 To strarrAttach.Length - 1
                    MyMail.Attachments.Add(New Attachment(strarrAttach(i)))
                Next
            End If


            '' Create instance of Smtp client
            'Dim MySmtpClient As SmtpClient = New SmtpClient("smtp.office365.com")
            '' If your ISP required it, set user name and password
            'MySmtpClient.UseDefaultCredentials = False
            'MySmtpClient.Credentials = New System.Net.NetworkCredential("online-rpt@royalpark.net", "Lafu2206")
            '' MySmtpClient.Port = "587"



            Dim MySmtpClient As New SmtpClient

            'If divcode = "01" Then

            Dim MyNetworkCredential As New NetworkCredential("info@mahce.com", "Lafu2206")
            '   Dim MyNetworkCredential As New NetworkCredential("arun@royalpark.net", "Rob59076")
            MySmtpClient.UseDefaultCredentials = False
            MySmtpClient.Credentials = MyNetworkCredential
            MySmtpClient.Host = "smtp.office365.com"
            MySmtpClient.Port = 25
            MySmtpClient.EnableSsl = True
            'Else
            '    Dim MyNetworkCredential1 As New NetworkCredential("rgt-online@royalgulf.net", "@RGT9999")
            '    MySmtpClient.UseDefaultCredentials = False
            '    MySmtpClient.Credentials = MyNetworkCredential1
            '    MySmtpClient.Host = "smtp.office365.com"
            '    MySmtpClient.Port = 587
            '    MySmtpClient.EnableSsl = True

            'End If

            ' Finally, send created email
            MySmtpClient.Send(MyMail)


            SendEmailOnline = True
        Catch ex As Exception
            SendEmailOnline = False
            Dim strFaildRecepient As String = ""
            Dim strError As String = ex.InnerException.Message
            'Failure sending mail. :: smtp issue
            If strError = "Mailbox unavailable. The server response was: unrouteable address" Then 'Unable to send to a recipient.

                strFaildRecepient = DirectCast(ex, System.Net.Mail.SmtpFailedRecipientsException).FailedRecipient
                strFaildRecepient = "Failed Reciepient: " & strFaildRecepient

                SendEmailOnline = True
            End If
            objclsUtilities.WriteErrorLog("clsEmail:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString & " :: " & strFaildRecepient)

        End Try
    End Function

    Public Function SendEmailOnline_old(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String) As Boolean
        Try
            strFrom = "info@mahce.com"
            Dim MyMailMessage As New MailMessage()
            Dim MyFromAddress As New MailAddress(strFrom)
            Dim strarrCC() As String
            strTo = strTo.Replace(";", ",")
            strToCC = strToCC.Replace(";", ",")

            MyMailMessage.From = MyFromAddress
            Dim ToAddressMul As String() = Nothing
            ToAddressMul = strTo.Split(",")
            Dim J As Integer = 0
            If ToAddressMul.Length > 0 Then
                For J = 0 To ToAddressMul.Length - 1
                    If ToAddressMul(J) <> String.Empty Then
                        MyMailMessage.To.Add(ToAddressMul(J))
                    End If
                Next
            End If
            'If CCAddress <> String.Empty Then
            '    MyMailMessage.CC.Add(CCAddress)
            'End If
            strarrCC = strToCC.Split(",")
            If strToCC <> "" Then
                J = 0
                For i = 1 To strarrCC.Length
                    MyMailMessage.CC.Add(strarrCC(J))
                    J = J + 1
                Next
            End If

            MyMailMessage.Subject = strSubject
            MyMailMessage.Body = strMsg
            MyMailMessage.IsBodyHtml = True
            MyMailMessage.BodyEncoding = System.Text.Encoding.UTF8

            If attchemnt <> "" Then
                Dim strarrAttach() As String
                strarrAttach = attchemnt.Split(";")
                For i = 0 To strarrAttach.Length - 1
                    MyMailMessage.Attachments.Add(New Attachment(strarrAttach(i)))
                Next
            End If


            Dim MySmtpClient As New SmtpClient
            Dim MyNetworkCredential As New NetworkCredential("info@mahce.com", "Lafu2206") '"@online0987")
            MySmtpClient.UseDefaultCredentials = False
            MySmtpClient.Credentials = MyNetworkCredential
            MySmtpClient.Host = "smtp.office365.com"
            MySmtpClient.Port = 587
            MySmtpClient.EnableSsl = True
            MySmtpClient.Send(MyMailMessage)
            SendEmailOnline_old = True
        Catch ex As Exception
            SendEmailOnline_old = False
            Dim strFaildRecepient As String = ""
            Dim strError As String = ex.InnerException.Message
            'Failure sending mail. :: smtp issue
            If strError = "Mailbox unavailable. The server response was: unrouteable address" Then 'Unable to send to a recipient.

                strFaildRecepient = DirectCast(ex, System.Net.Mail.SmtpFailedRecipientsException).FailedRecipient
                strFaildRecepient = "Failed Reciepient: " & strFaildRecepient

                SendEmailOnline_old = True
            End If
            objclsUtilities.WriteErrorLog("clsEmail:: " & Reflection.MethodBase.GetCurrentMethod.Name & " :: " & ex.Message.ToString & " :: " & strFaildRecepient)

        End Try
    End Function

    Public Function SendEmailCCMultiAttachmnt(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Dim strarr() As String
        Dim strarrCC() As String
        Dim strarrAttach() As String

        Dim i, j As Integer
        Try
            strarr = strTo.Split(",")
            strarrCC = strToCC.Split(",")

            strarrAttach = attchemnt.Split(";")
            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            j = 0
            For i = 1 To strarr.Length
                Mail_Message.To.Add(strarr(j))
                j = j + 1
            Next

            If strToCC <> "" Then
                j = 0
                For i = 1 To strarrCC.Length
                    Mail_Message.CC.Add(strarrCC(j))
                    j = j + 1
                Next
            End If


            Mail_Message.Subject = strSubject
            'Set Msg Body
            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True
            For i = 0 To strarrAttach.Length - 1
                Mail_Message.Attachments.Add(New Attachment(strarrAttach(i)))
            Next



            msClient.Port = 25
            msClient.Host = "127.0.0.1"
            msClient.Credentials = New System.Net.NetworkCredential("", "") '

            'msClient.Port = 25

            'msClient.Host = "127.0.0.1"

            'comment by csn since no firewall need to uncomment on instalation
            'msClient.Send(Mail_Message)
            SendEmailCCMultiAttachmnt = True
        Catch ex As Exception
            SendEmailCCMultiAttachmnt = False
        End Try
    End Function

    Public Function ReSendEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMsg As String, ByVal ctr As Integer) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Try


            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            Mail_Message.To.Add(strTo)



            Mail_Message.Subject = strSubject
            'Set Msg Body
            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True

            msClient.Port = 25
            msClient.Host = "127.0.0.1"
            msClient.Credentials = New System.Net.NetworkCredential("", "") '

            'msClient.Port = 25

            'msClient.Host = "127.0.0.1"

            'comment by csn since no firewall need to uncomment on instalation
            'msClient.Send(Mail_Message)
            ReSendEmail = True
            ctr = 1

        Catch ex As Exception
            ReSendEmail = False

            'If ctr <= 5 Then
            '    ctr = ctr + 1
            '    ReSendEmail(strFrom, strTo, strSubject, strMsg, ctr)
            'End If
        End Try
    End Function

    Public Function ReSendEmail(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String, ByVal ctr As Integer) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Try
            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            Mail_Message.To.Add(strTo)

            'Set Subject
            'Dim attachFile As New Attachment(txtAttachmentPath.Text)
            'MyMessage.Attachments.Add(attachFile)

            Mail_Message.Subject = strSubject
            'Set Msg Body
            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True
            Mail_Message.Attachments.Add(New Attachment(attchemnt))

            msClient.Port = 25
            msClient.Host = "127.0.0.1"
            msClient.Credentials = New System.Net.NetworkCredential("", "") '

            'msClient.Port = 25

            'msClient.Host = "127.0.0.1"

            'comment by csn since no firewall need to uncomment on instalation
            'msClient.Send(Mail_Message)
            ReSendEmail = True
            ctr = 1
        Catch ex As Exception
            ReSendEmail = False

            'If ctr <= 5 Then
            '    ctr = ctr + 1
            '    ReSendEmail(strFrom, strTo, strSubject, strMsg, ctr)
            'End If
        End Try
    End Function

    Public Function SendEmailBCC(ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String, ByVal attchemnt As String) As Boolean
        Dim Mail_Message As New MailMessage
        Dim FromAddress As New MailAddress(strFrom)
        Dim msClient As New SmtpClient
        Dim strarr() As String
        Dim strarrCC() As String

        Dim i, j As Integer
        Try
            strarr = strTo.Split(",")
            strarrCC = strToCC.Split(",")


            'Set From Email id
            Mail_Message.From = FromAddress
            'Set To Email id
            j = 0
            For i = 1 To strarr.Length
                Mail_Message.To.Add(strarr(j))
                j = j + 1
            Next

            If strToCC <> "" Then
                j = 0
                For i = 1 To strarrCC.Length
                    Mail_Message.Bcc.Add(strarrCC(j))
                    j = j + 1
                Next
            End If

            'Set Subject
            'Dim attachFile As New Attachment(txtAttachmentPath.Text)
            'MyMessage.Attachments.Add(attachFile)

            Mail_Message.Subject = strSubject
            'Set Msg Body
            Mail_Message.Body = strMsg

            'Mail_Message.Priority = MailPriority.Normal
            Mail_Message.IsBodyHtml = True
            If attchemnt <> "" Then
                Mail_Message.Attachments.Add(New Attachment(attchemnt))
            End If


            msClient.Port = 25

            ' msClient.Host = "127.0.0.1"

            msClient.Host = "127.0.0.1"
            msClient.Credentials = New System.Net.NetworkCredential("", "") '

            'comment by csn since no firewall need to uncomment on instalation
            'msClient.Send(Mail_Message)
            SendEmailBCC = True
        Catch ex As Exception
            SendEmailBCC = False
        End Try
    End Function
    Public Function SendCDOMessage(ByVal attach_filename As String, ByVal strFrom As String, ByVal strTo As String, ByVal strToCC As String, ByVal strSubject As String, ByVal strMsg As String) As Boolean

        Try
            Dim Mail_Message As New MailMessage
            Dim FromAddress As New MailAddress(strFrom)
            Dim msClient As New SmtpClient
            Dim strarr() As String
            Dim strarrCC() As String

            Dim i, j As Integer
            Try
                strarr = strTo.Split(",")
                strarrCC = strToCC.Split(",")


                'Set From Email id
                Mail_Message.From = FromAddress
                'Set To Email id
                j = 0
                For i = 1 To strarr.Length
                    Mail_Message.To.Add(strarr(j))
                    j = j + 1
                Next

                If strToCC <> "" Then
                    j = 0
                    For i = 1 To strarrCC.Length
                        Mail_Message.CC.Add(strarrCC(j))
                        j = j + 1
                    Next
                End If

                'Set Subject
                'Dim attachFile As New Attachment(txtAttachmentPath.Text)
                'MyMessage.Attachments.Add(attachFile)

                Mail_Message.Subject = strSubject
                'Set Msg Body
                Mail_Message.Body = strMsg

                Mail_Message.Priority = MailPriority.Normal
                Mail_Message.IsBodyHtml = True
                'Mail_Message.Attachments.Add(New Attachment(attach_filename))

                If attach_filename <> "" Then
                    Mail_Message.Attachments.Add(New Attachment(attach_filename))
                End If


                msClient.Port = 25
                msClient.Host = "127.0.0.1"
                msClient.Credentials = New System.Net.NetworkCredential("", "") '

                'msClient.Port = 25
                'msClient.Host = "127.0.0.1"
                'msClient.Credentials = New System.Net.NetworkCredential("", "") '



                msClient.Send(Mail_Message)
                SendCDOMessage = True
            Catch ex As Exception
                SendCDOMessage = False
            End Try
            SendCDOMessage = True
        Catch ex As Exception
            SendCDOMessage = False
        End Try


    End Function

End Class
