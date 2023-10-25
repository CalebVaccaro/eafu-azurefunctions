using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace eafu_azurefunctions
{
    public static class Extensions
    {
        public static async Task<CloudTable> GetCloudTable(this string connectionString, string tableName)
        {
            // Create a CloudStorageAccount instance using the connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the table client
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            var cloudTable = tableClient.GetTableReference(tableName);

            await cloudTable.CreateIfNotExistsAsync();

            // Get a reference to the table
            return tableClient.GetTableReference(tableName);
        }
    }
}
