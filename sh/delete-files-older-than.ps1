# https://www.thomasmaurer.ch/2010/12/powershell-delete-files-older-than/

$Path = "C:\temp"
$Daysback = "-30"
$CurrentDate = Get-Date
$DatetoDelete = $CurrentDate.AddDays($Daysback)
Get-ChildItem $Path | Where-Object { $_.LastWriteTime -lt $DatetoDelete } | Remove-Item
