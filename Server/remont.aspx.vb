Imports System.Data
Imports System.Data.OracleClient
Imports Newtonsoft.Json

Partial Class remont
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
        Dim c As String
        Dim so As String
        z = Request.QueryString("Z") & ""
        c = Request.QueryString("C") & ""
        so = Request.QueryString("SO") & ""
        Dim w As String
        w = " NOWTABLE.SO IN (4,5) "

        If z <> "" And z <> "(ВСЕ)" Then
            w = w + " and ZAVOD='" + z + "' "

        End If

        If c <> "" And c <> "(ВСЕ)" Then
            w = w + " and CEH='" + c + "' "

        End If

        If so <> "" And so <> "-1" Then
            w = w + " and NOWTABLE.SO=" + so + " "
        End If

        dt = cm.QuerySelect("SELECT OBOR.*,NOWTABLE.SO ,TO_CHAR(NOWTABLE.SO_DATE,'dd.mm.YY hh24:mi:ss') SO_DATE,statustext NAME FROM OBOR JOIN NOWTABLE ON OBOR.INVN=NOWTABLE.INVN  JOIN statuses ON statusvalue=NOWTABLE.SO  and tagname='SO' WHERE " & w & " order  by ZAVOD,CEH,NAIM")
      

        jj = New JOut
        jj.success = "true"
        jj.data = dt
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()
    


    End Sub
End Class
