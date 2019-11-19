Imports System.Data
Imports System.Data.OracleClient
Imports Newtonsoft.Json

Partial Class legend
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
        dt = cm.QuerySelect("select distinct  (case when tagname='SA' then 'Автоматика' else 'Оператор' end)  OPTYPE,statustext NAME ,'#'||substr(replace(to_char(16777216+to_number(statuses.statuscolor),'XXXXXX'),' ','0'),2,6)  COLOR   from statuses where tagname in ('SO','SA') order by OPTYPE DESC,NAME ")
       
        jj = New JOut
        jj.success = "true"

        jj.data = dt
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()



    End Sub
End Class

