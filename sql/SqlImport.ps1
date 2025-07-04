#cd "C:\Program Files (x86)\Microsoft SQL Server\140\DAC\bin"

SqlPackage /Action:Import /sf:"G:\UPLOAD\xxx\xxx-db-cnt--2025-06-01--05-15-03-utc.bacpac" /tcs:"initial catalog=xxx-db-cnt--2025-06-01;data source=devsql;UID=uid;PWD=pwd;encrypt=false"
