
SELECT name as PackageName, ownersid, suser_sname(ownersid)
FROM [msdb].[dbo].[sysssispackages]
--where suser_sname(ownersid) = 'ST\jba'
order by 1,2

suser_sname(ownersid)

begin tran

UPDATE [msdb].[dbo].[sysssispackages]
SET ownersid = SUSER_SID('ST\Stgpsql')
WHERE ownersid = SUSER_SID('ST\jba')

commit
select @@TRANCOUNT
