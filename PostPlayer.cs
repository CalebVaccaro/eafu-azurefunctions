using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using eafu_azurefunctions;

namespace AzureFunctions
{
    public static class PostPlayer
    {
        static string connectionString = Environment.GetEnvironmentVariable("EAFU_CONNECTION_STRING");
        static string table = Environment.GetEnvironmentVariable("EAFU_PLAYER_TABLE");

        [FunctionName("PostPlayer")]
        public static async Task<IActionResult> Post([HttpTrigger(AuthorizationLevel.Function, "post", Route = "player")]
            HttpRequest req, ILogger log)
        {
            // Parse the request body to player object
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            eafu_azurefunctions.Player player = JsonConvert.DeserializeObject<eafu_azurefunctions.Player>(requestBody);

            // Get a reference to the  table
            CloudTable playerTable = await connectionString.GetCloudTable(table);

            // Set the ID and create the TableOperation for inserting the player
            PlayerEntity playerEntity = new PlayerEntity(player);
            TableOperation insertOperation = TableOperation.Insert(playerEntity);

            // Execute the insert operation
            await playerTable.ExecuteAsync(insertOperation);

            log.LogInformation($"Added player with ID: {player.Id}");

            return new OkObjectResult(player);
        }
    }
}
