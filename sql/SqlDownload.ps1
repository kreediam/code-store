$src = "https://xxx.blob.core.windows.net/db-backups/content-staging/xxx-db-cnt--2025-06-01--05-15-03-utc.bacpac?sp=xxx"
$dest = "G:\UPLOAD\xxx"

cd G:\UPLOAD\kreed\azcopy_windows_amd64_10.19.0

./azcopy.exe copy $src $dest
