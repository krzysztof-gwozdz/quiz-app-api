$azcopy = "$PSScriptRoot\..\..\tools\azcopy.exe"
$connectionString = "http://127.0.0.1:10000/devstoreaccount1/"
$source =  "$PSScriptRoot\fake_data\" + $containerName + "\*"
$target = $connectionString + $containerName + $sasToken

Write-Output " ~~~~~ Add fake $containerName to Azure Blob container ~~~~~ "
Invoke-Expression "$azcopy copy '$source' '$target' --recursive --from-to=LocalBlob"