
Partial Class map
    Inherits System.Web.UI.Page
    Private C As String = "select"
    Private W As String = "700"
    Private H As String = "400"


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub
        If Session.Item("ID") Is Nothing Or Session.Item("ID") = "" Then
            Response.Redirect("default.aspx")
            Response.End()
            Exit Sub
        End If
        C = Request.QueryString("C")
        If C = "" Then C = "select"
        W = Request.QueryString("W")
        If W = "" Then W = "700" Else W = W - 20
        H = Request.QueryString("H")
        If H = "" Then H = "400" Else H = H - 20
    End Sub

    Public Function GetC() As String
        Return C
    End Function


    Public Function GetH() As String
        Return H
    End Function


    Public Function GetW() As String
        Return W
    End Function

    Protected Sub Page_PreLoad(sender As Object, e As System.EventArgs) Handles Me.PreLoad

    End Sub
End Class
