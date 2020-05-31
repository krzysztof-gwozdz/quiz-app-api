# Quiz App

### Status
![GitHub](https://github.com/krzysztof-gwozdz/QuizApp.Backend/workflows/GitHub/badge.svg)
### How to Run
1. Download and install [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) 
2. Azure local development tools [optional]
    - Download and install [Azure Cosmos Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes) to emulate Cosmos Db
    - Download and install [Azure DocumentDb Data Migration Tool](https://aka.ms/csdmtool) to add fake date to CosmosDb. After installation:
        - Add `dt.exe` to `PATH` or set `$dataMigrationToolPath` in `scripts\CosmosDb\add_fake_data_to_collection.ps1`
        - Set `$connectionString` in `scripts\CosmosDb\add_fake_data_to_collection.ps1`
    - Download and install [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator) to emulate Azure Blob
    - Download and install [Microsoft Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/) to explore Azure Blob and Cosmos Db
3. Set configuration in `QuizApp.Api/application.json`:    
    ```javascript
    "CosmosDb": {
        "Mode": "<Azure OR Emulator>",
        "Azure": {
          "ServiceEndpoint": "<YOUR SERVICE ENDPOINT URI>",
          "AuthKey": "<YOUR AUTHORIZATION KEY>"
        },
        "Emulator": {
          "ServiceEndpoint": "<YOUR SERVICE ENDPOINT URI>",
          "AuthKey": "<YOUR AUTHORIZATION KEY>"
        },
        "DatabaseName": "<DATABASE NAME>"   
    },
    ```
4. Run project using:
    - [Visual Studio](https://visualstudio.microsoft.com/)
    - [Visual Studio Code](https://code.visualstudio.com/)
    - run script `run.bat` from root directory
5. If you set up Azure DocumentDb Data Migration Tool in 3. you can run scripts\add_fake_date.ps1 [optional]    