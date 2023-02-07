#https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
#https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/versions-and-dependencies

(Get-ItemProperty "HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full").Release
