using System;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace eafu_azurefunctions
{
    public class Player
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int GameDuration { get; set; }

        public Player(PlayerEntity playerEntity)
        {
            Id = playerEntity.Id;
            Name = playerEntity.Name;
            Score = playerEntity.Score;
            GameDuration = playerEntity.GameDuration;
        }

        [JsonConstructor]
        public Player(string id, string name, int score, int gameDuration)
        {
            Id = id;
            Name = name;
            Score = score;
            GameDuration = gameDuration;
        }
    }

    public class PlayerEntity : TableEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int GameDuration { get; set; }

        public PlayerEntity(Player player)
        {
            this.PartitionKey = Guid.NewGuid().ToString();
            this.RowKey = Guid.NewGuid().ToString();

            this.Id = player.Id;
            this.Name = player.Name;
            this.Score = player.Score;
            this.GameDuration = player.GameDuration;
        }

        public PlayerEntity() { }
    }
}
