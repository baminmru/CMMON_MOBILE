Imports System.Data
Imports System.Data.OracleClient
Imports Newtonsoft.Json

Partial Class mgroup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub
        Dim jj As JOut
        Dim dt As DataTable
        If Session.Item("ID") Is Nothing Or Session.Item("ID") = "" Then
            dt = New DataTable
            jj = New JOut
            jj.success = "false"
            jj.data = dt
            jj.msg = "use mobile application for logon"
            Response.Clear()
            Response.Write(JsonConvert.SerializeObject(jj))
            Response.End()
            Exit Sub
        End If
        Dim cm As CMConnector
        cm = New CMConnector()

        If Not cm.Init() Then

            dt = New DataTable
            jj = New JOut
            jj.success = "false"
            jj.data = dt
            jj.msg = "Error"
            Response.Clear()
            Response.Write(JsonConvert.SerializeObject(jj))
            Response.End()
            Exit Sub
        End If

        Dim z As String
        z = Request.QueryString("Z") & ""
        If z <> "" And z <> "(ВСЕ)" Then
            dt = cm.QuerySelect("select distinct MGROUP NAME from OBOR where ZAVOD='" + z + "' order  by MGROUP")
        Else
            dt = cm.QuerySelect("select distinct MGROUP NAME from OBOR order  by MGROUP")
        End If

        Dim dr As DataRow
        dr = dt.NewRow
        dr("NAME") = "(ВСЕ)"
        dt.Rows.InsertAt(dr, 0)

        jj = New JOut
        jj.success = "true"
        jj.data = dt
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()



    End Sub
End Class

