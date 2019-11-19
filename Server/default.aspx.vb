
Partial Class d
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("ID") <> "" Then
            Response.Redirect("monitor.aspx")
            Response.End()
            Exit Sub
        End If
    End Sub
End Class
