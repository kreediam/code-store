# https://learn.microsoft.com/en-us/azure/storage/common/storage-ref-azcopy
# https://github.com/Azure/azure-storage-azcopy

$BLOB_SAS_URL = "https://xxx.blob.core.windows.net/db-backups/production/xxx-db--xxx.bacpac?sp=r&st=&se=&skoid=&sktid=xxx"
. "D:\Apps\azcopy_windows_amd64_10.24.0\azcopy.exe" copy $BLOB_SAS_URL "D:\Data\docker-mssql-data"
