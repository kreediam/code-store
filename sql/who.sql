--sp_who2

declare @who table(
	SPID INT,
	Status VARCHAR(MAX),
	LOGIN VARCHAR(MAX),
	HostName VARCHAR(MAX),
	BlkBy VARCHAR(MAX),
	DBName VARCHAR(MAX),
	Command VARCHAR(MAX),
	CPUTime INT,
	DiskIO INT,
	LastBatch VARCHAR(MAX),
	ProgramName VARCHAR(MAX),
	SPID_1 INT,
	REQUESTID INT
)

insert into @who exec sp_who2

select * from @who
where DBName = DB_NAME()
