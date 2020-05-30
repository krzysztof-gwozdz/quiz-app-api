$jsonFilePath = "$PSScriptRoot\fake_data\Quizzes.json"
$collection = "Quizzes"
$partitionKey = "/id"
$idField = "id"

Write-Output "Add fake quizzes"
Invoke-Expression "$PSScriptRoot\add_fake_data_to_collection.ps1"