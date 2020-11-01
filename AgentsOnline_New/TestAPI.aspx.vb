
Partial Class TestAPI
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objAPI As ApiController = New ApiController()
        '  Dim str As String = objAPI.CallTestAPI()
        '    Dim str As String = objAPI.CallHotelSearchAPI()
    End Sub
End Class
