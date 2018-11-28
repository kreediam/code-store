
SELECT
	physical_memory_in_use_kb * 1.0 / 1024 / 1024 as physical_memory_in_use_gb,
	locked_page_allocations_kb * 1.0 / 1024 / 1024 as locked_page_allocations_gb,
	page_fault_count,
	memory_utilization_percentage, 
	available_commit_limit_kb / 1024 / 1024 as available_commit_limit_mb,
	process_physical_memory_low, 
	process_virtual_memory_low
FROM sys.dm_os_process_memory WITH (NOLOCK) OPTION (RECOMPILE);


SELECT counter_name,cntr_value*1.0/1024/1024 as Memory_GB
FROM sys.dm_os_performance_counters
WHERE ([OBJECT_NAME] like '%Memory Manager%') and
counter_name in( 'Target Server Memory (KB)','Total Server Memory (KB)')


SELECT 
	SUM(awe_allocated_kb)*1.0/1024 as total_awe_allocated_MB
FROM sys.dm_os_memory_clerks
