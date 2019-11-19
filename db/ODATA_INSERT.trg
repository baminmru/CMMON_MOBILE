create or replace trigger ODATA_INSERT
  before insert on odata  
  for each row
declare
  -- local variables here
  pdate date;
begin
  select max(var_date) into pdate 
  from odata 
  where invn=:new.invn and var_name=:new.var_name and var_date<:new.var_date;
  
  update odata 
  set var_enddate=:new.var_date 
  where invn=:new.invn and var_name=:new.var_name and var_date=pdate; 
end ODATA_INSERT;
/
