$jsonFilePath = "$PSScriptRoot\fake_data\QuestionSets.json"
$collection = "QuestionSets"
$partitionKey = "/id"
$idField = "id"

Write-Output "Add fake question sets"
Invoke-Expression "$PSScriptRoot\add_fake_data_to_collection.ps1"