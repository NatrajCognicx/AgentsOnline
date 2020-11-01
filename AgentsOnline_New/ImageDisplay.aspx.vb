Imports System.IO

Partial Class ImageDisplay
    Inherits System.Web.UI.Page
    Dim objclsUtilities As New clsUtilities
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("FileName") IsNot Nothing Then
            Try
                ' Read the file and convert it to Byte Array
                Dim filePath As String = ""
                If Request.QueryString("type") = 0 Then
                    filePath = ConfigurationManager.AppSettings("HotelImagePath").ToString()
                ElseIf Request.QueryString("type") = 1 Then '' tour Image
                    filePath = ConfigurationManager.AppSettings("TourImagePath").ToString()
                ElseIf Request.QueryString("type") = 2 Then '' Transfer Image
                    filePath = ConfigurationManager.AppSettings("TransferImagePath").ToString()
                ElseIf Request.QueryString("type") = 3 Then '' Airport Meet Image
                    filePath = ConfigurationManager.AppSettings("AirportMeetImagePath").ToString()
                ElseIf Request.QueryString("type") = 4 Then '' OtherService Image
                    filePath = ConfigurationManager.AppSettings("OtherServiceImagePath").ToString()
                ElseIf Request.QueryString("type") = 5 Then '' Home Page Offer Images
                    filePath = ConfigurationManager.AppSettings("HomePageOfferImages").ToString()
                ElseIf Request.QueryString("type") = 6 Then '' Destination Specialist Images
                    filePath = ConfigurationManager.AppSettings("DestinationSpecialistImages").ToString()
                End If

                Dim filename As String = Request.QueryString("FileName")
                'If filename = "" Then
                '    filename = "NoImage.png"
                'End If
                Dim contenttype As String = "image/" & Path.GetExtension(filename).Replace(".", "")
                Dim fs As FileStream
                If Request.QueryString("type") = 0 Or Request.QueryString("type") = 1 Or Request.QueryString("type") = 2 Or Request.QueryString("type") = 3 Or Request.QueryString("type") = 4 Then

                    '*** Danny 04/07/2018>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    Try
                        Dim strColumbusPath As String = Server.MapPath("")
                        'objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 1:: " & strColumbusPath & ":: " & Session("GlobalUserName"))
                        strColumbusPath = Path.GetDirectoryName(strColumbusPath)
                        'objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 2:: " & strColumbusPath & ":: " & Session("GlobalUserName"))

                        strColumbusPath = strColumbusPath + IIf(Right(strColumbusPath, 1) = "\", "", "\") + objclsUtilities.ExecuteQueryReturnStringValue("select option_selected from reservation_parameters where param_id=8")
                        'objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 3:: " & strColumbusPath & ":: " & Session("GlobalUserName"))
                        strColumbusPath = IIf(filePath = "", strColumbusPath, "") & filePath & filename
                        'objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 4:: " & strColumbusPath & ":: " & Session("GlobalUserName"))

                        If File.Exists(strColumbusPath) Then
                            fs = New FileStream(strColumbusPath, FileMode.Open, FileAccess.Read)
                            'objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 5:: " & strColumbusPath & ":: " & Session("GlobalUserName"))
                        Else
                            fs = New FileStream(Server.MapPath("img/NoImage.png"), FileMode.Open, FileAccess.Read)
                            'objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 6:: " & strColumbusPath & ":: " & Session("GlobalUserName"))
                        End If
                    Catch ex As Exception
                        fs = New FileStream(Server.MapPath("img/NoImage.png"), FileMode.Open, FileAccess.Read)
                        objclsUtilities.WriteErrorLog("ImageDisplay.aspx :: Image 7:: " & ex.StackTrace & ":: " & Session("GlobalUserName"))
                    End Try
                    'fs = New FileStream(filePath & filename, FileMode.Open, FileAccess.Read)
                    '*** Danny 04/07/2018<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                Else
                    fs = New FileStream(filePath & filename,
                FileMode.Open, FileAccess.Read)
                End If


                Dim br As BinaryReader = New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
                br.Close()
                fs.Close()

                'Write the file to Reponse
                Response.Buffer = True
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = contenttype
                Response.AddHeader("content-disposition", "attachment;filename=" & filename)
                Response.BinaryWrite(bytes)
                Response.Flush()
                Response.End()
            Catch

            End Try


        End If
    End Sub
End Class
