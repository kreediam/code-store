set nocount on

declare @rownum int
declare @tblname varchar(50)

declare @tables table (
    rownum int identity (1,1),
    tblname varchar(50)
)

declare @results table (
    name varchar(50),
    rows varchar(50),
    reserved varchar(50),
    data varchar(50),
    index_size varchar(50),
    unused varchar(50)
)

insert into @tables select name from sysobjects where OBJECTPROPERTY(id, 'IsUserTable') = 1 order by name
set @rownum = 0

while 1 = 1 begin
    set @rownum = @rownum + 1
    select @tblname = tblname from @tables where rownum = @rownum
    if @@rowcount = 0 break
   
    insert into @results exec sp_spaceused @tblname, true
end

select
    name,
    rows,
    cast(replace(reserved, ' KB', '') as int) as reserved,
    cast(replace(data, ' KB', '') as int) as data,
    cast(replace(index_size, ' KB', '') as int) as index_size,
    cast(replace(unused, ' KB', '') as int) as unused
from
    @results
