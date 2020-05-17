# Quiz App
---
### How to Run
1. Download and install [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) 
2. Download and install [Azure Cosmos Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes) [optional] 
3. Set configuration in `QuizApp.Api/application.json`:    
    ```javascript
    "ConnectionStrings": {
        "Mode": "<Azure or Emulator>",
        "Azure": {
            "ServiceEndpoint": "<YOUR SERVICE ENDPOINT URI>"
        },
        "Emulator": {
            "ServiceEndpoint": "https://localhost:8081",
            "AuthKey": "<YOUR AUTH KEY>"
        }
    },
    ```
4. Run project using:
    - [Visual Studio](https://visualstudio.microsoft.com/)
    - ~~[Visual Studio Code](https://code.visualstudio.com/)~~ *[TODO: add configuration]*
    - run script `run.bat` in main directory
