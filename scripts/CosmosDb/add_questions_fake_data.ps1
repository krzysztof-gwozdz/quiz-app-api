$jsonFilePath = "$PSScriptRoot\fake_data\Questions.json"
$collectionName = "Questions"
$partitionKey = "/id"
$idField = "id"

Invoke-Expression "$PSScriptRoot\add_fake_data_to_collection.ps1"