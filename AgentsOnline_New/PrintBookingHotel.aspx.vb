Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Partial Class PrintBookingHotel
    Inherits System.Web.UI.Page
    Dim objclsUtilities As New clsUtilities

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim bh As ClsBookingHotelPdf = New ClsBookingHotelPdf()
            Dim requestid = Request.QueryString("RequestId")
            Dim partyCode As String = Request.QueryString("PartyCode")
            Dim amended As Integer = Request.QueryString("amended")
            Dim cancelled As Integer = Request.QueryString("cancelled")
            Dim ds As New DataSet
            Dim bytes As Byte() = {}
            bh.BookingHotelPrint(requestid, partyCode, amended, cancelled, bytes, ds, "download", "")
            Dim fileName As String = "BookHotel@" + requestid + "@" + DateTime.Now.ToString("ddMMyyyy@HHmmss") + ".pdf"
            Response.Clear()
            Response.AddHeader("Content-Disposition", "inline; filename=" + fileName)
            Response.AddHeader("Content-Length", bytes.Length.ToString())
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.BinaryWrite(bytes)
            Response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "", "alert('" & "Error Description " + ex.Message.Replace("'", " ") & "' );", True)
            objclsUtilities.WriteErrorLog("PrintBookingHotel.aspx :: GenerateReport :: " & ex.Message & ":: " & Session("GlobalUserName"))
        End Try
    End Sub
End Class
