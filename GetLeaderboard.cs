using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using eafu_azurefunctions;


namespace eafu_azurefunctions
{
    public static class GetLeaderboard
    {
        static string connectionString = Environment.GetEnvironmentVariable("EAFU_CONNECTION_STRING");
        static string table = Environment.GetEnvironmentVariable("EAFU_PLAYER_TABLE");

        // This function will be called with a specific leaderboardCount
        [FunctionName("GetLeaderboardWithCount")]
        public static async Task<IActionResult> GetWithCount([HttpTrigger(AuthorizationLevel.Function, "get", Route = "leaderboard/{leaderboardCount}")]
            HttpRequest req, int leaderboardCount, ILogger log)
        {
            return await GetLeaderboardInternal(req, leaderboardCount, log);
        }

        // This function will be called without a leaderboardCount and use a default value
        [FunctionName("GetLeaderboard")]
        public static async Task<IActionResult> GetWithoutCount([HttpTrigger(AuthorizationLevel.Function, "get", Route = "leaderboard")]
            HttpRequest req, ILogger log)
        {
            int defaultLeaderboardCount = 10; // You can set this to any default value you want
            return await GetLeaderboardInternal(req, defaultLeaderboardCount, log);
        }

        private static async Task<IActionResult> GetLeaderboardInternal(HttpRequest req, int leaderboardCount, ILogger log)
        {
            try
            {
                // Retrieve the top N players based on score
                List<PlayerEntity> topPlayers = new List<PlayerEntity>();

                // Retrieve the leaderboard elements
                TableQuery<PlayerEntity> query = new TableQuery<PlayerEntity>();

                // Get a reference to the  table
                CloudTable playerTable = await connectionString.GetCloudTable(table);

                TableContinuationToken continuationToken = null;
                do
                {
                    TableQuerySegment<PlayerEntity> segment = await playerTable.ExecuteQuerySegmentedAsync(query, continuationToken);
                    topPlayers.AddRange(segment.Results);
                    continuationToken = segment.ContinuationToken;
                } while (continuationToken != null);

                // Process the top players
                List<Player> leaderboard = topPlayers
                    .OrderByDescending(x => x.Score)
                    .Take(leaderboardCount)
                    .Select(playerEntity => new Player(playerEntity))
                    .ToList();

                // Now you have the leaderboard list with the top players based on score
                // You can use the leaderboard list as needed
                return new OkObjectResult(leaderboard);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error retrieving leaderboard: {ex.Message}");
            }
        }
    }
}
