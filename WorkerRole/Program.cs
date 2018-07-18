using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace WorkerRole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                "appsettings.json",
                optional: true,
                reloadOnChange: true);
            var configuration = builder.Build();

            Console.WriteLine("BEGIN");

            var storageAccount =
               CloudStorageAccount.Parse(configuration["StorageConnectionString"]);

            var queueClient = storageAccount.CreateCloudQueueClient();

            var commandsQueue = queueClient.GetQueueReference("telemetryqueue");

            await commandsQueue.CreateIfNotExistsAsync();

            while (true)
            {
                
                var message = await commandsQueue.GetMessageAsync();
                if (message == null)
                {
                    await Task.Delay(1000);
                    continue;
                }

                var command = JsonConvert.DeserializeObject<JObject>(message.AsString);

                Console.WriteLine(command);

                using (var conn = new SqlConnection(configuration["HurtleSelfRunningDB"]))
                {
                    conn.Open();

                    var id = command.Value<int>("Id");
                    //var telemetryInfo = command.Value<JObject>("TelemetryInfo");
                    var lat = command.Value<double>("Latitudine");
                    var lon = command.Value<double>("Longitudine");
                    var istante = command.Value<DateTime>("Istante");
                    var idrunner = command.Value<int>("IdRunner");
                    var idattivita = command.Value<int>("IdAttivita");
                    var uriselfie = command.Value<string>("UriSelfie");

                    var insert = await conn.ExecuteAsync("INSERT INTO Telemetria " +
                        "([Latitudine],[Longitudine],[Istante],[IdRunner],[IdAttivita],[UriSelfie])" +
                        "VALUES (@lat, @lon, @istante, @idrunner, @idattivita, @uriselfie)",
                        new { lat, lon, istante, idrunner, idattivita, uriselfie });

                    conn.Close();

                }
                await commandsQueue.DeleteMessageAsync(message);
            }
        }
    }
}
