Imports System.Data
Imports System.Data.OracleClient
Imports Newtonsoft.Json


Partial Class g_sa
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




        Dim Q As String
        Dim F As String
        Dim A As String

        Dim SN As String
        Dim sF As String
        Dim dF As Date
        Dim vA As Integer = 0

        SN = Request.QueryString("SN") & ""
        F = Request.QueryString("F") & ""
        A = Request.QueryString("A") & ""

        Dim w As String
        Dim w2 As String
        w2 = " 1=1 "
        w = " 1=1 "

        If F = "" Then
            dF = Date.Today()
            sF = cm.OracleDate(dF)
        Else

            Dim ss() As String = F.Split(" ")
            Dim dd() As String = ss(0).Split("-")
            dF = New Date(Integer.Parse(dd(0)), Integer.Parse(dd(1)), Integer.Parse(dd(2)))
            If A <> "" Then
                If Integer.TryParse(A, vA) Then
                    dF = dF.AddDays(vA)
                End If
            End If
            sF = cm.OracleDate(dF) ' " to_date('" + F + "','YYYY-MM-DD HH24:MI:SS')"
        End If



        If SN <> "" Then
            w = w + " and ODATA2.INVN='" + SN + "' "
        Else
            w = w + " and ODATA2.INVN is null"
        End If


        Q = ""
        Q = Q & vbCrLf & " select var_date,nvl(var_enddate,sysdate) var_enddate,statustext,to_char(16777216+to_number(statuses.statuscolor),'XXXXXX') rgb from odata2 "
        Q = Q & vbCrLf & " join statuses on odata2.var_val=statusvalue and tagname='SA'"
        Q = Q & vbCrLf & " where  var_name='SA' and   "
        Q = Q & vbCrLf & "	     ( "
        Q = Q & vbCrLf & "	     ( var_date >= %DFROM% and var_date <(%DFROM%+1) )"
        Q = Q & vbCrLf & "		  or  ( var_date < %DFROM% and nvl(var_enddate,sysdate) >= %DFROM% )  )  "
        Q = Q & vbCrLf & "		  and " & w
        Q = Q & vbCrLf & "order by var_date"
        Q = Q.Replace("%DFROM%", sF)

        dt = cm.QuerySelect(Q)

        Dim dt2 As DataTable
        dt2 = New DataTable

        dt2.Columns.Add("ETIME")
        dt2.Columns.Add("V")
        dt2.Columns.Add("COLOR")

        Dim i As Integer
        Dim v(288) As Integer
        Dim C(288) As String

        i = 0
        Dim j As Integer
        Dim sMin As Integer
        Dim eMin As Integer
        Dim dStart As Date
        Dim dEnd As Date

        For i = 0 To 288
            v(i) = 0
            C(i) = "#000000"
        Next

        For i = 0 To dt.Rows.Count - 1

            dStart = dt.Rows(i)("var_date")
            dEnd = dt.Rows(i)("var_enddate")
            dStart = dStart.AddSeconds(-dStart.Second)
            dEnd = dEnd.AddSeconds(-dEnd.Second)
            sMin = Math.Abs(DateDiff(DateInterval.Minute, dF, dStart))
            eMin = Math.Abs(DateDiff(DateInterval.Minute, dF, dEnd))
            If sMin < 0 Then sMin = 0
            If eMin > 1440 Then eMin = 1440
            For j = sMin To eMin Step 5
                If j >= 0 And j <= 1440 Then
                    v(j \ 5) = 1
                    Dim cs As String
                    cs = dt.Rows(i)("rgb").ToString().Replace(" ", "")
                    While cs.Length < 6
                        cs = "0" + cs
                    End While
                    C(j \ 5) = "#" + cs
                End If
            Next
        Next


        Dim dr As DataRow
        dStart = dF
        For j = 0 To 287
            dr = dt2.NewRow
            dr("V") = v(j)
            dr("COLOR") = C(j)
            dr("ETIME") = dStart.Hour.ToString("00") + ":" + dStart.Minute.ToString("00")
            dt2.Rows.Add(dr)
            dStart = dStart.AddMinutes(5)
        Next

        jj = New JOut
        jj.success = "true"
        jj.data = dt2
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()



    End Sub

    Private Sub AddData(dto As DataTable, drfrom As DataRow, ByVal fld As String, ByVal name As String, Optional ByVal AsNumber As Integer = 0)
        Dim dr As DataRow
        dr = dto.NewRow

        If TypeName(drfrom(fld)) = "DBNull" Then

            dr("VALUE") = ""
        Else
            If AsNumber = 1 Then
                dr("VALUE") = drfrom(fld).ToString.Replace(",", ".")
            Else
                dr("VALUE") = drfrom(fld).ToString & ""
            End If
        End If


        dr("NAME") = name
        dto.Rows.Add(dr)
    End Sub

End Class
