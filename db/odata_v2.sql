create or replace view odata_v2 as
select invn,so0
,so1+so26 as so1
,so2,so3,so4,so5,so6,so7,so8,so9,so10,so11,so12,so13,so14,so15,so16,so17,so18,so19,so20,so21,so22,so23,so24,so25,so100
,so0+ so2 + so3 + so25 s_ner
,so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15 s_ned
,so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 s_vspom
,so24 s_nez
,sa2 + sa3 + sa5 + sa6 + sa7 sa
,(findate -startdate) kv
,case when (((findate -startdate) -
(so0 + so1 + so2 + so3 + so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 + so25+so26)) > 0) then
(findate -startdate) - (so0 + so1 + so2 + so3 + so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 + so25+so26)
else 0  end neterr
,so0 + so1 + so2 + so3 + so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 + so25 + so26 kv_corr
,so1 + so4 + so5 + so6 + so7 + so8 + so9 + so10 + so11 + so12 + so13 + so14 + so15 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24 zv
,(so1 + so16 + so17 + so18 + so19 + so20 + so21 + so22 + so23 + so24) dv
,(so1 + so24) pv
,so1 chv
,nvl(findate,sysdate) findate, startdate
from odata_v1;
