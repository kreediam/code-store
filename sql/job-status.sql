select
	convert(varchar(20), serverproperty('ServerName')) as ServerName,
	j.name AS JobName,
	case j.enabled when 1 then 'Enabled' else 'Disabled' end as JobStatus,
	case jh.run_status
		when 0 then 'Error Failed'
		when 1 then 'Succeeded'
		when 2 then 'Retry'
		when 3 then 'cancelled'
		when 4 then 'In Progress'
		else 'Status Unknown' end as LastRunStatus,
	ja.run_requested_date as LastRunDate,
	convert(varchar(10), convert(datetime, rtrim(19000101)) + (jh.run_duration * 9 + jh.run_duration % 10000 * 6 + jh.run_duration % 100 * 10) / 216e4, 108) AS RunDuration,
	ja.next_scheduled_run_date as NextScheduledRunDate,
	convert(varchar(500), jh.message) AS StepDescription
from
	msdb.dbo.sysjobactivity ja
	left join msdb.dbo.sysjobhistory jh on ja.job_history_id = jh.instance_id
	join msdb.dbo.sysjobs_view j on ja.job_id = j.job_id
where
	ja.session_id = (select max(session_id) from msdb.dbo.sysjobactivity)
order by 7--job_name, job_status

