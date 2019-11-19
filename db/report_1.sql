select 
 OBORTOP.CEH,
 OBORTOP.ZAVOD,
 OBORTOP.NAIM,
 OBORTOP.MGROUP,
 smens.smen,
 smens.krit,
 cast (t.INVN as varchar(20)) INVN,
 cast (Round(KZV,3) as number(4,3)) KZV,
 cast (Round(KDV,3) as number(4,3)) KDV,
 cast (Round(KPV,3) as number(4,3)) KPV,
 cast (Round(KCHV,3) as number(4,3)) KCHV,
 cast (Round(OEE,3) as number(4,3)) OEE,
 cast (Round(TEEPO,3) as number(4,3)) TEEPO,
 cast(NUMTODSINTERVAL(MVO,'DAY') as interval Day(1) to second(0)) MVO,
 cast (Round(TEEPA,3) as number(4,3)) TEEPA,
 cast(NUMTODSINTERVAL(MVA,'DAY') as interval Day(1) to second(0)) MVA,
 cast(NUMTODSINTERVAL(neterr,'DAY') as interval Day(1) to second(0)) NETERR,
 cast(NUMTODSINTERVAL(NER_FULL,'DAY') as interval Day(1) to second(0)) NER_FULL,
 cast(NUMTODSINTERVAL(SO25,'DAY') as interval Day(1) to second(0)) SO25,
 cast(NUMTODSINTERVAL(SO3,'DAY') as interval Day(1) to second(0)) SO3,
 cast(NUMTODSINTERVAL(SO2,'DAY') as interval Day(1) to second(0)) SO2,
 cast(NUMTODSINTERVAL(NED_FULL,'DAY') as interval Day(1) to second(0)) NED_FULL,
 cast(NUMTODSINTERVAL(SO4,'DAY') as interval Day(1) to second(0))SO4,
 cast(NUMTODSINTERVAL(SO5,'DAY') as interval Day(1) to second(0)) SO5,
 cast(NUMTODSINTERVAL(SO6,'DAY') as interval Day(1) to second(0)) SO6,
 cast(NUMTODSINTERVAL(SO7,'DAY') as interval Day(1) to second(0)) SO7,
 cast(NUMTODSINTERVAL(SO8,'DAY') as interval Day(1) to second(0)) SO8,
 cast(NUMTODSINTERVAL(SO9,'DAY') as interval Day(1) to second(0)) SO9,
 cast(NUMTODSINTERVAL(SO10,'DAY') as interval Day(1) to second(0)) SO10,
 cast(NUMTODSINTERVAL(SO11,'DAY') as interval Day(1) to second(0)) SO11,
 cast(NUMTODSINTERVAL(SO12,'DAY') as interval Day(1) to second(0)) SO12,
 cast(NUMTODSINTERVAL(SO13,'DAY') as interval Day(1) to second(0)) SO13,
 cast(NUMTODSINTERVAL(SO14,'DAY') as interval Day(1) to second(0)) SO14,
 cast(NUMTODSINTERVAL(SO15,'DAY') as interval Day(1) to second(0)) SO15,
 cast(NUMTODSINTERVAL(VSPOM_FULL,'DAY') as interval Day(1) to second(0)) VSPOM_FULL,
 cast(NUMTODSINTERVAL(SO16,'DAY') as interval Day(1) to second(0)) SO16,
 cast(NUMTODSINTERVAL(SO17,'DAY') as interval Day(1) to second(0)) SO17,
 cast(NUMTODSINTERVAL(SO18,'DAY') as interval Day(1) to second(0)) SO18,
 cast(NUMTODSINTERVAL(SO19,'DAY') as interval Day(1) to second(0)) SO19,
 cast(NUMTODSINTERVAL(SO20,'DAY') as interval Day(1) to second(0)) SO20,
 cast(NUMTODSINTERVAL(SO21,'DAY') as interval Day(1) to second(0)) SO21,
 cast(NUMTODSINTERVAL(SO22,'DAY') as interval Day(1) to second(0)) SO22,
 cast(NUMTODSINTERVAL(SO23,'DAY') as interval Day(1) to second(0)) SO23,
 cast(NUMTODSINTERVAL(NEZ_FULL,'DAY') as interval Day(1) to second(0)) NEZ_FULL,
 cast(NUMTODSINTERVAL(SO24,'DAY') as interval Day(1) to second(0)) SO24,
 cast(NUMTODSINTERVAL(SO0,'DAY') as interval Day(1) to second(0)) SO0
from obor obortop join 
(
 select
  invn,
  case  when (kv_corr > 0) then  zv / kv_corr  else  0  end kzv,
  case  when (zv > 0) then  dv / zv  else  0  end kdv,
  case  when (dv > 0) then  pv / dv  else  0  end kpv,
  case  when (pv > 0) then  chv / pv  else  0  end kchv,
  case  when (zv > 0) then  (chv) / (zv)  else  0  end OEE,
  case  when (kv_corr * zv > 0) then  (chv) / (kv_corr)  else  0  end TEEPO,  
  so1 mvo,
  case  when (kv_corr > 0) then  (sa) / (kv_corr)  else  0  end TEEPA,
  sa mva,
  neterr neterr,
  s_ner ner_full,
  s_ned ned_full,
  s_nez nez_full,
  s_vspom vspom_full,
  so0,  so2,  so3,  so4,  so5,  so6,  so7,  so8,  so9,  so10,
  so11,  so12,  so13,  so14,  so15,  so16,  so17,  so18,  so19,  so20,
  so21,  so22,  so23,  so24,  so25
  from
	(
		select odata_v2.invn,
		 sum(SO0) SO0,sum(SO1) SO1,sum(SO2) SO2,sum(SO3) SO3,sum(SO4) SO4,sum(SO5) SO5,sum(SO6) SO6,sum(SO7) SO7,sum(SO8) SO8,sum(SO9) SO9,sum(SO10) SO10
		,sum(SO11) SO11,sum(SO12) SO12,sum(SO13) SO13,sum(SO14) SO14,sum(SO15) SO15,sum(SO16) SO16,sum(SO17) SO17,sum(SO18) SO18,sum(SO19) SO19,sum(SO20) SO20
		,sum(SO21) SO21,sum(SO22) SO22,sum(SO23) SO23,sum(SO24) SO24,sum(SO25) SO25,sum(SO100) SO100
		,sum(s_ner) s_ner,sum(s_ned) s_ned,sum(s_vspom) s_vspom,sum(s_nez) s_nez,sum(sa) sa,sum(kv) kv
		,sum(neterr) neterr,sum(kv_corr) kv_corr,sum(zv) zv,sum(dv) dv,sum(pv) pv,sum(chv) chv
		,max(findate) findate,min(startdate) startdate from odata_v2 join OBOR on odata_v2.invn=obor.invn
		  where( ( startdate >= sysdate-12 and startdate < sysdate-6 )
		  or  ( startdate < sysdate-12 and findate >= sysdate-12)  )  
		  and OBOR.CEH='011'
		  group by odata_v2.invn
	)
) t on obortop.invn = t.invn
left join smens on obortop.invn =smens.invn and (sysdate-12>=smens.startdate and  (smens.findate is null or smens.findate>=   sysdate-12))

