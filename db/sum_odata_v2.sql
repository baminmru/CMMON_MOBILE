select  invn,
sum(SO0)  SO0
,sum(SO1)  SO1
,sum(SO2)  SO2
,sum(SO3)  SO3
,sum(SO4)  SO4
,sum(SO5)  SO5
,sum(SO6)  SO6
,sum(SO7)  SO7
,sum(SO8)  SO8
,sum(SO9)  SO9
,sum(SO10) SO10
,sum(SO11) SO11
,sum(SO12) SO12
,sum(SO13) SO13
,sum(SO14) SO14
,sum(SO15) SO15
,sum(SO16) SO16
,sum(SO17) SO17
,sum(SO18) SO18
,sum(SO19) SO19
,sum(SO20) SO20
,sum(SO21) SO21
,sum(SO22) SO22
,sum(SO23) SO23
,sum(SO24) SO24
,sum(SO25) SO25
,sum(SO100) SO100
,sum(s_ner) s_ner
,sum(s_ned) s_ned
,sum(s_vspom) s_vspom
,sum(s_nez) s_nez
,sum(sa) sa
,sum(kv) kv
,sum(neterr) neterr
,sum(kv_corr) kv_corr
,sum(zv) zv
,sum(dv) dv
,sum(pv) pv
,sum(chv) chv
,max(findate) findate
,min(startdate) startdate
 from odata_v2 where(
                      ( startdate >= sysdate-10 and startdate < sysdate-5 )
                      or
                      ( startdate < sysdate-10 and findate >= sysdate-10)
                      )
                       group by invn
