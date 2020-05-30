$jsonFilePath = "$PSScriptRoot\fake_data\Questions.json"
$collection = "Questions"
$partitionKey = "/id"
$idField = "id"

Write-Output "Add fake questions"
Invoke-Expression "$PSScriptRoot\add_fake_data_to_collection.ps1"