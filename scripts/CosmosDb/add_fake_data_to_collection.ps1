$dataMigrationToolPath = "F:\tools\dt\dt.exe"
$connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;Database=QuizAppDb;"

Write-Output " ~~~~~ Add fake $collectionName to CosmosDb ~~~~~ "
& $dataMigrationToolPath /s:JsonFile /s.Files:$jsonFilePath /t:DocumentDB /t.ConnectionString:$connectionString /t.Collection:$collectionName /t.PartitionKey:$partitionKey /t.IdField:$idField