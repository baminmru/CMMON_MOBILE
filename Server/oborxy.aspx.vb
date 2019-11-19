Imports System.Data
Imports System.Data.OracleClient
Imports Newtonsoft.Json
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary


Partial Class oborxy
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

        Dim c As String
        Dim Q As String

        c = Request.QueryString("C") & ""

        Dim w As String
        w = " 1=1 "
        If c <> "" And c <> "(ВСЕ)" Then
            w = w + " and OBOR.CEH='" + c + "' "

        End If

        Q = ""

        Q = Q & vbCrLf & "select obor.INVN, obor.NAIM, obor.PLANPARAMS,nowtable.*,  nvl(statuses.statustext,'не опр.') statustext, to_char(16777216+to_number(statuses.statuscolor),'XXXXXX') rgb, nvl(sa.statustext,'не опр.') satext, to_char(16777216+to_number(sa.statuscolor),'XXXXXX') sargb from nowtable "
        Q = Q & vbCrLf & " join obor on obor.invn=nowtable.invn"
        Q = Q & vbCrLf & " join statuses  on statuses.tagname='SO' and statuses.statusvalue=nowtable.so  "
        Q = Q & vbCrLf & " join statuses sa on sa.tagname='SA' and sa.statusvalue=nowtable.sa  "
        Q = Q & vbCrLf & " where  " & w
       

        dt = cm.QuerySelect(Q)
        dt.Columns.Add("FIGTYPE")
        dt.Columns.Add("ANGLE")
        dt.Columns.Add("WIDTH")
        dt.Columns.Add("HEIGHT")
        dt.Columns.Add("LOCATIONX")
        dt.Columns.Add("LOCATIONY")


        Dim sb() As Byte
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1





            Dim result As SharedLib.Figures.DataToSerializate

            Try
                sb = dt.Rows(i)("PLANPARAMS")
                Dim stream As System.IO.MemoryStream
                stream = New MemoryStream(sb)
                result = New BinaryFormatter().Deserialize(stream)
                dt.Rows(i)("PLANPARAMS") = DBNull.Value
                If result.FigType.Name = "RectangleMachine" Then
                    dt.Rows(i)("FIGTYPE") = "R"
                Else
                    dt.Rows(i)("FIGTYPE") = "E"
                End If

                dt.Rows(i)("ANGLE") = result.Angle.ToString
                dt.Rows(i)("WIDTH") = result.Width.ToString
                dt.Rows(i)("HEIGHT") = result.Height.ToString
                dt.Rows(i)("LOCATIONX") = result.Location.X.ToString
                dt.Rows(i)("LOCATIONY") = result.Location.Y.ToString
                'Dim qi As String
                'qi = "insert into oborxy(invn,t,a,w,h,x,y) values('" & dt.Rows(i)("INVN") & "','" & dt.Rows(i)("FIGTYPE") & "'," & dt.Rows(i)("ANGLE") & "," & dt.Rows(i)("WIDTH") & "," & dt.Rows(i)("HEIGHT") & "," & dt.Rows(i)("LOCATIONX") & "," & dt.Rows(i)("LOCATIONY") & ")"
                'Try
                '    cm.QueryExec(qi)
                'Catch ex As Exception

                'End Try

            Catch ex As Exception
                dt.Rows(i)("FIGTYPE") = "R"
                dt.Rows(i)("ANGLE") = 0
                dt.Rows(i)("WIDTH") = 10
                dt.Rows(i)("HEIGHT") = 10
                dt.Rows(i)("LOCATIONX") = 0
                dt.Rows(i)("LOCATIONY") = 0
            End Try

        Next


        jj = New JOut
        jj.success = "true"
        jj.data = dt
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()



    End Sub
End Class
