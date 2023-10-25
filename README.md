# EAFU Azure Functions Project

## Overview
This project consists of Azure Functions designed to manage player data and provide a leaderboard functionality for a gaming application. It is split into multiple functions, each responsible for a specific aspect of the application. This project is designed to work in tandem with the [Easy Azure Functions for Unity (EAFU)](https://github.com/CalebVaccaro/EAFU), providing a seamless backend solution for Unity applications. 

The project is open-source to facilitate developers in understanding and developing Azure Functions locally.

## Structure
The project is divided into four main parts:

1. **PostPlayer**: Handles the posting of player data.
2. **GetLeaderboard**: Manages the retrieval of the leaderboard, supporting both default and specific leaderboard counts.
3. **Extensions**: Contains helper extensions, specifically for working with Azure Table Storage.
4. **Models**: Classes that represent the structure of a player and provide utility for converting between Azure Table Storage entities and application models.

## Models

- **Player**: Represents a player with properties such as `Id`, `Name`, `Score`, and `GameDuration`. It can be converted from a `PlayerEntity` and vice versa.
- **PlayerEntity**: Represents a player entity in Azure Table Storage with additional properties like `PartitionKey` and `RowKey` for storage purposes.

## Setup


### Prerequisites
- An active Azure account. If you don't have one, you can create it for free [here](https://azure.microsoft.com/en-us/free/).
- Azure Functions Core Tools. Installation instructions can be found [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local).
- .NET Core SDK. You can download it from [here](https://dotnet.microsoft.com/download).

To get started with Azure Functions, you can follow the [official guide from Microsoft](https://docs.microsoft.com/en-us/azure/azure-functions/). 

## Configuration
Ensure you have the necessary configuration settings in your `local.settings.json` file or in the Azure portal for the following:

```json
{
    "EAFU_CONNECTION_STRING": "The connection string for Azure Table Storage.",
    "EAFU_PLAYER_TABLE": "The name of the table in Azure Table Storage."
}
```

## Local Development
1. Clone the repository to your local machine.
2. Navigate to the root folder of the project.
3. Open the terminal or command prompt.
4. Run the following command to start the functions locally:
   ```bash
   func start
   ```

## Deployment
To deploy the functions to Azure, you can follow the deployment guide provided by Microsoft.
Deploying your Azure Functions project can be done through the Azure Functions Core Tools from the command line. Follow these steps to publish your project.

### Prerequisites
- Azure CLI installed. [Install from here](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli).

### Steps to Publish with Azure CLI

1. **Log in to your Azure account (root folder):**
    ```bash
    az login
    ```
2. **Publish your Azure Functions project:**
   ```bash
   func azure functionapp publish <YourFunctionAppName>
   ```

### Steps to Publish with Visual Studio

1. Open your Azure Functions project in Visual Studio.

2. Right-click on the project in the Solution Explorer and select 'Publish'.

3. Choose 'Azure' and then 'Azure Function App'. Click 'Next'.

4. Select your existing Azure Function App or create a new one. Ensure all the settings are correct and click 'Finish'.

5. Review the summary and click 'Publish' to deploy your project.

Resources
Here are some useful resources to get you started:

- [Azure Functions Documentation](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [Azure Table Storage Documentation](https://docs.microsoft.com/en-us/azure/storage/tables/table-storage-overview)
- [Azure SDK for .NET](https://github.com/Azure/azure-sdk-for-net)