# Cosmos Db
Invoke-Expression "$PSScriptRoot\CosmosDb\add_tags_fake_data.ps1"
Invoke-Expression "$PSScriptRoot\CosmosDb\add_questions_sets_fake_data.ps1"
Invoke-Expression "$PSScriptRoot\CosmosDb\add_questions_fake_data.ps1"
Invoke-Expression "$PSScriptRoot\CosmosDb\add_quizzes_fake_data.ps1"

#Azure Blob
Invoke-Expression "$PSScriptRoot\AzureBlob\add_question_set_images_fake_data.ps1"