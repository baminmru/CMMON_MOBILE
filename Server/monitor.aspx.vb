Imports System.Text
Partial Class monitor
    Inherits System.Web.UI.Page

    Private M As String
    Private C As String
    Private Z As String
    Private I As String




    Public Function GetStartupScript() As String

        Dim sb As StringBuilder
        sb = New StringBuilder
        sb.AppendLine("function AfterLoad(){")
        If M = "R" Then
            sb.AppendLine("last_rz='" & Z & "';")
            sb.AppendLine("last_rc='" & C & "';")
            sb.AppendLine("contentPanel.removeAll();")
            sb.AppendLine(" contentPanel.add(GetRemontPanel());")
            sb.AppendLine("filterPanel.removeAll();")
            sb.AppendLine(" filterPanel.add(GetRemontFilter());")
            sb.AppendLine(" filterPanel.setVisible(true);")
            If C <> "" Then
                sb.AppendLine(" store_remont.load({ params: { C: last_rc, Z: last_rz} });")
            End If

        End If

        If M = "S" Then
            sb.AppendLine("last_nz='" & Z & "';")
            sb.AppendLine("last_nc='" & C & "';")
            sb.AppendLine("contentPanel.removeAll();")
            sb.AppendLine(" contentPanel.add(GetNowPanel());")
            sb.AppendLine("filterPanel.removeAll();")
            sb.AppendLine(" filterPanel.add(GetNowFilter());")
            sb.AppendLine(" filterPanel.setVisible(true);")
            If C <> "" Then
                sb.AppendLine(" store_now.load({ params: { C: last_nc, Z: last_nz} });")
            End If

        End If
        sb.AppendLine("}")
        Return sb.ToString
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub

        If Session.Item("ID") Is Nothing Or Session.Item("ID") = "" Then
            Response.Clear()
            Response.Redirect("default.aspx")
            Response.End()
            Exit Sub
        End If
        Z = Request.QueryString("Z") & ""
        C = Request.QueryString("C") & ""
        I = Request.QueryString("I") & ""
        M = Request.QueryString("M") & ""



    End Sub
End Class
