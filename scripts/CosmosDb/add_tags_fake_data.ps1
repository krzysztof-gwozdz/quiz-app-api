$jsonFilePath = "$PSScriptRoot\fake_data\Tags.json"
$collectionName = "Tags"
$partitionKey = "/id"
$idField = "id"

Invoke-Expression "$PSScriptRoot\add_fake_data_to_collection.ps1"