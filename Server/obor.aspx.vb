Imports System.Data
Imports System.Data.OracleClient
Imports Newtonsoft.Json
Imports System.Diagnostics

Partial Class obor
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
        Dim g As String
        Dim Q As String
        Dim F As String
        Dim T As String
        Dim K As String
        Dim S As String
        Dim sF As String
        Dim sT As String
        S = Request.QueryString("S") & ""
        K = Request.QueryString("K") & ""
        z = Request.QueryString("Z") & ""
        c = Request.QueryString("C") & ""
        g = Request.QueryString("G") & ""
        F = Request.QueryString("F") & ""
        T = Request.QueryString("T") & ""
        Dim w As String
        Dim w2 As String
        w2 = " 1=1 "
        w = " 1=1 "
        If T = "" Then
            sT = " sysdate "
        Else
            sT = " to_date('" + T + "','YYYY-MM-DD HH24:MI:SS')"
        End If

        If F = "" Then
            sF = " (sysdate-1) "
        Else
            sF = " to_date('" + F + "','YYYY-MM-DD HH24:MI:SS')"
        End If

        If K <> "" Then
            If K = "0" Then
                w2 = w2 + " and smens.KRIT=0 "
            End If
            If K = "1" Then
                w2 = w2 + " and smens.KRIT=1 "
            End If
        End If


        If S <> "" Then
            If S = "0" Then
                w2 = w2 + " and smens.smen='Периодический' "
            End If
            If S = "1" Then
                w2 = w2 + " and smens.smen='Односменный' "
            End If
            If S = "2" Then
                w2 = w2 + " and smens.smen='Двухсменный' "
            End If
            If S = "3" Then
                w2 = w2 + " and smens.smen='Трёхсменный' "
            End If
            If S = "4" Then
                w2 = w2 + " and smens.smen='Четырёхсменный' "
            End If
        End If


        If z <> "" And z <> "(ВСЕ)" Then
            w = w + " and OBOR.ZAVOD='" + z + "' "

        End If

        If c <> "" And c <> "(ВСЕ)" Then
            w = w + " and OBOR.CEH='" + c + "' "

        End If


        If g <> "" And g <> "(ВСЕ)" Then
            w = w + " and OBOR.MGROUP='" + g + "' "

        End If
        Q = ""
        Q = Q & vbCrLf & " select  OBORTOP.CEH,"
        Q = Q & vbCrLf & " OBORTOP.ZAVOD,"
        Q = Q & vbCrLf & " OBORTOP.NAIM,"
        Q = Q & vbCrLf & " OBORTOP.MGROUP,"
        Q = Q & vbCrLf & " smens.smen,"
        Q = Q & vbCrLf & " smens.krit,"
        Q = Q & vbCrLf & " cast (t.INVN as varchar(20)) INVN,"
        Q = Q & vbCrLf & " cast (Round(KZV,3) as number(4,3)) KZV,"
        Q = Q & vbCrLf & " cast (Round(KDV,3) as number(4,3)) KDV,"
        Q = Q & vbCrLf & " cast (Round(KPV,3) as number(4,3)) KPV,"
        Q = Q & vbCrLf & " cast (Round(KCHV,3) as number(4,3)) KCHV,"
        Q = Q & vbCrLf & " cast (Round(OEE,3) as number(4,3)) OEE,"
        Q = Q & vbCrLf & " cast (Round(TEEPO,3) as number(4,3)) TEEPO,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(MVO,'DAY') as interval Day(1) to second(0)) MVO,"
        Q = Q & vbCrLf & " cast (Round(TEEPA,3) as number(4,3)) TEEPA,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(MVA,'DAY') as interval Day(1) to second(0)) MVA,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(neterr,'DAY') as interval Day(1) to second(0)) NETERR,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(NER_FULL,'DAY') as interval Day(1) to second(0)) NER_FULL,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO25,'DAY') as interval Day(1) to second(0)) SO25,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO3,'DAY') as interval Day(1) to second(0)) SO3,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO2,'DAY') as interval Day(1) to second(0)) SO2,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(NED_FULL,'DAY') as interval Day(1) to second(0)) NED_FULL,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO4,'DAY') as interval Day(1) to second(0))SO4,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO5,'DAY') as interval Day(1) to second(0)) SO5,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO6,'DAY') as interval Day(1) to second(0)) SO6,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO7,'DAY') as interval Day(1) to second(0)) SO7,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO8,'DAY') as interval Day(1) to second(0)) SO8,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO9,'DAY') as interval Day(1) to second(0)) SO9,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO10,'DAY') as interval Day(1) to second(0)) SO10,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO11,'DAY') as interval Day(1) to second(0)) SO11,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO12,'DAY') as interval Day(1) to second(0)) SO12,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO13,'DAY') as interval Day(1) to second(0)) SO13,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO14,'DAY') as interval Day(1) to second(0)) SO14,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO15,'DAY') as interval Day(1) to second(0)) SO15,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(VSPOM_FULL,'DAY') as interval Day(1) to second(0)) VSPOM_FULL,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO16,'DAY') as interval Day(1) to second(0)) SO16,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO17,'DAY') as interval Day(1) to second(0)) SO17,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO18,'DAY') as interval Day(1) to second(0)) SO18,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO19,'DAY') as interval Day(1) to second(0)) SO19,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO20,'DAY') as interval Day(1) to second(0)) SO20,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO21,'DAY') as interval Day(1) to second(0)) SO21,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO22,'DAY') as interval Day(1) to second(0)) SO22,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO23,'DAY') as interval Day(1) to second(0)) SO23,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(NEZ_FULL,'DAY') as interval Day(1) to second(0)) NEZ_FULL,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO24,'DAY') as interval Day(1) to second(0)) SO24,"
        Q = Q & vbCrLf & " cast(NUMTODSINTERVAL(SO0,'DAY') as interval Day(1) to second(0)) SO0"
        Q = Q & vbCrLf & " from obor obortop join "

        ''''' ODATA_V3= T
        Q = Q & vbCrLf & " ("
        Q = Q & vbCrLf & " select"
        Q = Q & vbCrLf & "  invn,"
        Q = Q & vbCrLf & "  case  when (kv_corr > 0) then  zv / kv_corr  else  0  end kzv,"
        Q = Q & vbCrLf & "  case  when (zv > 0) then  dv / zv  else  0  end kdv,"
        Q = Q & vbCrLf & "  case  when (dv > 0) then  pv / dv  else  0  end kpv,"
        Q = Q & vbCrLf & "  case  when (pv > 0) then  chv / pv  else  0  end kchv,"
        Q = Q & vbCrLf & "  case  when (zv > 0) then  (chv) / (zv)  else  0  end OEE,"
        Q = Q & vbCrLf & "  case  when (kv_corr * zv > 0) then  (chv) / (kv_corr)  else  0  end TEEPO,  "
        Q = Q & vbCrLf & "  so1 mvo,"
        Q = Q & vbCrLf & "  case  when (kv_corr > 0) then  (sa) / (kv_corr)  else  0  end TEEPA,"
        Q = Q & vbCrLf & "  sa mva,"
        Q = Q & vbCrLf & "  neterr neterr,"
        Q = Q & vbCrLf & "  s_ner ner_full,"
        Q = Q & vbCrLf & "  s_ned ned_full,"
        Q = Q & vbCrLf & "  s_nez nez_full,"
        Q = Q & vbCrLf & "  s_vspom vspom_full,"
        Q = Q & vbCrLf & "  so0,  so2,  so3,  so4,  so5,  so6,  so7,  so8,  so9,  so10,"
        Q = Q & vbCrLf & "  so11,  so12,  so13,  so14,  so15,  so16,  so17,  so18,  so19,  so20,"
        Q = Q & vbCrLf & "  so21,  so22,  so23,  so24,  so25"
        Q = Q & vbCrLf & "  from"
        Q = Q & vbCrLf & "	("
        Q = Q & vbCrLf & "		select odata_v2.invn,"
        Q = Q & vbCrLf & "		 sum(SO0) SO0,sum(SO1) SO1,sum(SO2) SO2,sum(SO3) SO3,sum(SO4) SO4,sum(SO5) SO5,sum(SO6) SO6,sum(SO7) SO7,sum(SO8) SO8,sum(SO9) SO9,sum(SO10) SO10 "
        Q = Q & vbCrLf & "		,sum(SO11) SO11,sum(SO12) SO12,sum(SO13) SO13,sum(SO14) SO14,sum(SO15) SO15,sum(SO16) SO16,sum(SO17) SO17,sum(SO18) SO18,sum(SO19) SO19,sum(SO20) SO20 "
        Q = Q & vbCrLf & "		,sum(SO21) SO21,sum(SO22) SO22,sum(SO23) SO23,sum(SO24) SO24,sum(SO25) SO25,sum(SO100) SO100 "
        Q = Q & vbCrLf & "		,sum(s_ner) s_ner,sum(s_ned) s_ned,sum(s_vspom) s_vspom,sum(s_nez) s_nez,sum(sa) sa,sum(kv) kv "
        Q = Q & vbCrLf & "		,sum(neterr) neterr,sum(kv_corr) kv_corr,sum(zv) zv,sum(dv) dv,sum(pv) pv,sum(chv) chv "
        Q = Q & vbCrLf & "		 from "

        ''''' ODATA_V2
        Q = Q & vbCrLf & "( select invn,so0"
        Q = Q & vbCrLf & " ,odata_v1.so1+so26 as so1"
        Q = Q & vbCrLf & " ,so2,so3,so4,so5,so6,so7,so8,so9,so10,so11,so12,so13,so14,so15,so16,so17,so18,so19,so20,so21,so22,so23,so24,so25,so100"
        Q = Q & vbCrLf & " ,so0+ so2 + so3 + so25 s_ner"
        Q = Q & vbCrLf & " ,so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15 s_ned"
        Q = Q & vbCrLf & " ,so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 s_vspom"
        Q = Q & vbCrLf & " ,so24 s_nez"
        Q = Q & vbCrLf & " ,sa2 + sa3 + sa5 + sa6 + sa7 sa"
        Q = Q & vbCrLf & " ,(odata_v1.findate - odata_v1.startdate) kv"
        Q = Q & vbCrLf & " ,case when ("
        Q = Q & vbCrLf & "   ("
        Q = Q & vbCrLf & "     (odata_v1.findate -odata_v1.startdate) -"
        Q = Q & vbCrLf & "     (so0 + odata_v1.so1 + so2 + so3 + so4 + so5 +so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15"
        Q = Q & vbCrLf & "     + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 + so25+ so26)"
        Q = Q & vbCrLf & "    ) > 0"
        Q = Q & vbCrLf & " )"
        Q = Q & vbCrLf & " then"
        Q = Q & vbCrLf & " (odata_v1.findate - odata_v1.startdate) - (so0 + odata_v1.so1 + so2 + so3 + so4 + so5 + so6 + so7 + so8 + so9 + so10"
        Q = Q & vbCrLf & " + so11 + so12 + so13 + so14 + so15 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 + so25 + so26)"
        Q = Q & vbCrLf & " else 0"
        Q = Q & vbCrLf & "   end neterr"
        Q = Q & vbCrLf & " ,so0 + odata_v1.so1 + so2 + so3 + so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15"
        Q = Q & vbCrLf & " + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 + so25 + so26 kv_corr"
        Q = Q & vbCrLf & " ,odata_v1.so1 + so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15"
        Q = Q & vbCrLf & " + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 zv"
        Q = Q & vbCrLf & " ,(odata_v1.so1 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24) dv"
        Q = Q & vbCrLf & " ,(odata_v1.so1 + so24) pv"
        Q = Q & vbCrLf & " ,odata_v1.so1 chv"
        Q = Q & vbCrLf & " "
        Q = Q & vbCrLf & " from "

        ''''' ODATA_V1
        Q = Q & vbCrLf & " ("
        Q = Q & vbCrLf & " select odata2.invn"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='0'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO0"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='1'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO1"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='2'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO2"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='3'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO3"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='4'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO4"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='5'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO5"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='6'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO6"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='7'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO7"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='8'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO8"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='9'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SO9"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='10'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO10"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='11'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO11"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='12'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO12"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='13'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO13"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='14'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO14"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='15'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO15"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='16'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO16"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='17'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO17"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='18'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO18"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='19'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO19"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='20'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO20"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='21'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO21"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='22'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO22"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='23'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO23"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='24'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO24"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='25'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO25"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='26'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO26"
        Q = Q & vbCrLf & " ,(case when var_name='SO' and var_val='100'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SO100"
        Q = Q & vbCrLf & " "
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='0'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA0"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='1'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA1"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='2'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA2"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='3'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA3"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='4'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA4"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='5'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA5"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='6'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA6"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='7'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA7"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='8'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA8"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='9'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end)  SA9"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='10'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA10"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='11'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA11"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='12'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA12"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='13'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA13"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='14'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA14"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='15'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA15"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='16'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA16"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='17'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA17"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='18'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA18"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='19'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA19"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='20'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA20"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='21'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA21"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='22'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA22"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='23'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA23"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='24'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA24"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='25'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA25"
        Q = Q & vbCrLf & " ,(case when var_name='SA' and var_val='100'  then  cast (Round((case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) - (case when var_date<%DFROM% then %DFROM% else var_date end),6) as number(14,6))  else 0 end) SA100"
        Q = Q & vbCrLf & " ,(case when var_date<%DFROM% then %DFROM% else var_date end) startdate"
        Q = Q & vbCrLf & " ,(case when nvl(var_enddate,sysdate)>%DTO% then %DTO% else nvl(var_enddate,sysdate) end) findate"
        Q = Q & vbCrLf & " from odata2 join OBOR on odata2.invn=obor.invn where var_name in ('SA','SO')"
        Q = Q & vbCrLf & "	     and ( ( odata2.var_date >= %DFROM% and odata2.var_date < %DTO% )"
        Q = Q & vbCrLf & "		  or  ( odata2.var_date < %DFROM% and nvl(var_enddate,sysdate) >= %DFROM% )  )  "

        Q = Q & vbCrLf & "     ) odata_v1"

        Q = Q & vbCrLf & "   ) odata_v2 "


        Q = Q & vbCrLf & "  join OBOR on odata_v2.invn=obor.invn "
        Q = Q & vbCrLf & "  where " & w
        Q = Q & vbCrLf & "	group by odata_v2.invn"
        Q = Q & vbCrLf & "  )"
        Q = Q & vbCrLf & " ) t "
        Q = Q & vbCrLf & "   on obortop.invn = t.invn"
        Q = Q & vbCrLf & " left join smens on obortop.invn =smens.invn and (%DTO%>=smens.startdate and  (smens.findate is null or smens.findate>=%DFROM%)) where  " & w2




        Q = Q.Replace("%DFROM%", sF)
        Q = Q.Replace("%DTO%", sT)

        ' dt = cm.QuerySelect("select sys.OBOR.* , '1'  KDV	, '1'  KEV	, '1'  KLE	, '1'  KHHO	, '1'  OEE	, '1'  TEEPO	, '1'  MBO	, '1'  TEEPA, '1'  MVA	, '1'  KEF	, '1'  OSH	, '1'  NR from sys.OBOR where " & w & " order  by ZAVOD,CEH,NAIM")
        Debug.Print(Q)
        dt = cm.QuerySelect(Q)



        jj = New JOut
        jj.success = "true"
        jj.data = dt
        jj.msg = "OK"
        Response.Clear()
        Response.Write(JsonConvert.SerializeObject(jj))
        Response.End()



    End Sub
End Class
