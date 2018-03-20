-- https://docs.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql

USE msdb;  
GO
EXEC sp_delete_backuphistory @oldest_date = '12/31/2017'
