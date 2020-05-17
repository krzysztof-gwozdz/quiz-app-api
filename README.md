# Quiz App
---
### How to Run
1. Download and install [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) 
2. Download and install [Azure Cosmos Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes) [optional] 
3. Set configuration in `QuizApp.Api/application.json`:    
    ```javascript
    "CosmosDb": {
        "Mode": "Emulator",
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
