drop materialized view ODATA_V1;

create materialized view ODATA_V1
refresh force ON demand
as
select odata2.invn
,(case when var_name='SO' and var_val='0'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO0
,(case when var_name='SO' and var_val='1'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO1
,(case when var_name='SO' and var_val='2'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO2
,(case when var_name='SO' and var_val='3'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO3
,(case when var_name='SO' and var_val='4'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO4
,(case when var_name='SO' and var_val='5'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO5
,(case when var_name='SO' and var_val='6'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO6
,(case when var_name='SO' and var_val='7'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO7
,(case when var_name='SO' and var_val='8'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO8
,(case when var_name='SO' and var_val='9'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SO9
,(case when var_name='SO' and var_val='10'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO10
,(case when var_name='SO' and var_val='11'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO11
,(case when var_name='SO' and var_val='12'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO12
,(case when var_name='SO' and var_val='13'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO13
,(case when var_name='SO' and var_val='14'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO14
,(case when var_name='SO' and var_val='15'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO15
,(case when var_name='SO' and var_val='16'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO16
,(case when var_name='SO' and var_val='17'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO17
,(case when var_name='SO' and var_val='18'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO18
,(case when var_name='SO' and var_val='19'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO19
,(case when var_name='SO' and var_val='20'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO20
,(case when var_name='SO' and var_val='21'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO21
,(case when var_name='SO' and var_val='22'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO22
,(case when var_name='SO' and var_val='23'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO23
,(case when var_name='SO' and var_val='24'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO24
,(case when var_name='SO' and var_val='25'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO25
,(case when var_name='SO' and var_val='26'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO26
,(case when var_name='SO' and var_val='100'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SO100

,(case when var_name='SA' and var_val='0'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA0
,(case when var_name='SA' and var_val='1'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA1
,(case when var_name='SA' and var_val='2'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA2
,(case when var_name='SA' and var_val='3'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA3
,(case when var_name='SA' and var_val='4'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA4
,(case when var_name='SA' and var_val='5'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA5
,(case when var_name='SA' and var_val='6'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA6
,(case when var_name='SA' and var_val='7'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA7
,(case when var_name='SA' and var_val='8'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA8
,(case when var_name='SA' and var_val='9'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end)  SA9
,(case when var_name='SA' and var_val='10'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA10
,(case when var_name='SA' and var_val='11'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA11
,(case when var_name='SA' and var_val='12'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA12
,(case when var_name='SA' and var_val='13'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA13
,(case when var_name='SA' and var_val='14'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA14
,(case when var_name='SA' and var_val='15'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA15
,(case when var_name='SA' and var_val='16'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA16
,(case when var_name='SA' and var_val='17'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA17
,(case when var_name='SA' and var_val='18'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA18
,(case when var_name='SA' and var_val='19'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA19
,(case when var_name='SA' and var_val='20'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA20
,(case when var_name='SA' and var_val='21'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA21
,(case when var_name='SA' and var_val='22'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA22
,(case when var_name='SA' and var_val='23'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA23
,(case when var_name='SA' and var_val='24'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA24
,(case when var_name='SA' and var_val='25'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA25
,(case when var_name='SA' and var_val='100'  then  cast (Round(nvl(var_enddate,sysdate) - var_date,6) as number(14,6))  else 0 end) SA100
,var_date startdate
,nvl(var_enddate,sysdate) findate
from odata2 ; --join OBOR on odata2.invn=obor.invn ;
