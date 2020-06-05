$jsonFilePath = "$PSScriptRoot\fake_data\Quizzes.json"
$collectionName = "Quizzes"
$partitionKey = "/id"
$idField = "id"

Invoke-Expression "$PSScriptRoot\add_fake_data_to_collection.ps1"