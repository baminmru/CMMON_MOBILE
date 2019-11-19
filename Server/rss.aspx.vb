Imports System.Data
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography


Partial Class rss
    Inherits System.Web.UI.Page


    Public Function Md5FromString(ByVal Source As String) As String
        Dim Bytes() As Byte
        Dim sb As New StringBuilder()

        'Check for empty string.
        If String.IsNullOrEmpty(Source) Then
            Return ""
        End If

        'Get bytes from string.
        Bytes = Encoding.Default.GetBytes(Source)

        'Get md5 hash
        Bytes = MD5.Create().ComputeHash(Bytes)

        'Loop though the byte array and convert each byte to hex.
        For x As Integer = 0 To Bytes.Length - 1
            sb.Append(Bytes(x).ToString("x2"))
        Next

        'Return md5 hash.
        Return sb.ToString()

    End Function

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim U As String
        Dim P As String
        Dim Q As String
        Dim l As String

        U = Request.QueryString("username") & ""
        P = Request.QueryString("password") & ""

        Response.Clear()
        Response.ContentType = "application/rss+xml"
        Dim objX As New XmlTextWriter(Response.OutputStream, Encoding.UTF8)
        objX.WriteStartDocument()
        objX.WriteStartElement("rss")
        objX.WriteAttributeString("version", "2.0")
        objX.WriteStartElement("channel")
        Dim cm As CMConnector
        cm = New CMConnector()
        Dim dt As DataTable

        Dim web As String
        web = ""
        objX.WriteElementString("title", "Monitoring")
        objX.WriteElementString("link", web)
        objX.WriteElementString("description", "Monitoring events!")
        objX.WriteElementString("language", "ru-ru")
        objX.WriteElementString("ttl", "60")
        'objX.WriteElementString("image", "http://vbasic.net/media/logo.gif")

        If cm.Init() Then

            Q = "select count(*) cnt  from users where username='" & U & "' and passwd='" & Md5FromString(P) & "'"
            dt = cm.QuerySelect(Q)
            If dt.Rows.Count = 1 Then
                If dt.Rows(0)("cnt") = 1 Then
                    Session.Item("ID") = Date.Now().Ticks.ToString()

                    dt = cm.QuerySelect("select TITLE,LINK,DATEOF,INFO,CATEGORY,ZAVOD,CEH,INVN from RSS_V order by DATEOF desc ")
                    Dim dr As DataRow
                    Dim i As Integer
                    Dim name As String

                    For i = 0 To dt.Rows.Count - 1
                        dr = dt.Rows(i)
                        If i = 0 Then
                            objX.WriteElementString("lastBuildDate", String.Format("{0:R}", dr("DATEOF")))
                        End If
                        If i > 100 Then Exit For

                        If dr("INFO").ToString().Length() > 100 Then
                            name = dr("INFO").ToString().Substring(0, 100) & "..."
                        Else
                            name = dr("INFO").ToString()
                        End If

                        Dim dd As Date
                        dd = dr("DATEOF")
                        objX.WriteStartElement("item")
                        objX.WriteElementString("title", dd.ToString("dd/MM/yyyy hh:mm:ss") + ", " + dr("TITLE").ToString() & ". " & name)
                        objX.WriteElementString("author", "Мониторинг")
                        l = dr("LINK").ToString() + "&C=" + dr("CEH") '+ "&Z=" + dr("ZAVOD")
                        objX.WriteElementString("link", l)
                        objX.WriteStartElement("guid")
                        objX.WriteAttributeString("isPermaLink", "true")
                        objX.WriteString(l)
                        objX.WriteEndElement()
                        objX.WriteElementString("pubDate", String.Format("{0:R}", dr("DATEOF")))
                        objX.WriteStartElement("category")
                        objX.WriteString(dr("CATEGORY").ToString())
                        objX.WriteEndElement()
                        objX.WriteElementString("description", name)
                        objX.WriteEndElement()
                    Next
                End If
            End If
        End If
        objX.WriteEndElement()
        objX.WriteEndElement()
        objX.WriteEndDocument()
        objX.Flush()
        objX.Close()
        Response.End()
    End Sub
End Class