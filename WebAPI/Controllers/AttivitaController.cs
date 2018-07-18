using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [DisableRequestSizeLimit]
    [Produces("application/json")]
    [Route("Hurtle/Attivita")]
    public class AttivitaController : Controller
    {
        private IConfiguration _configuration;

        public AttivitaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: Hurtle/Attivita
        [HttpGet]
        public IEnumerable<string> Get()
        {
             //return RedirectToPage("Index");
            return new string[] { "value1", "value2" };
        }

        // GET: Hurtle/Attivita/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: Hurtle/Attivita
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Telemetria value)
        {
            if (ModelState.IsValid)
            {
                var storageaccount = CloudStorageAccount.Parse(_configuration["StorageConnectionString"]);

                //creo il blob se non esiste
                var bloblClient = storageaccount.CreateCloudBlobClient();
                var telemetrieContainer =
                    bloblClient.GetContainerReference("selfie-telemetrie");
                await telemetrieContainer.CreateIfNotExistsAsync();

                var blobUri = string.Empty;

                var queueClient = storageaccount.CreateCloudQueueClient();

                var commandsQueue = queueClient.GetQueueReference("telemetryqueue");

                await commandsQueue.CreateIfNotExistsAsync();

                if (value.Selfie != null)
                {
                    byte[] data = System.Convert.FromBase64String(value.Selfie.Substring("data:image/jpeg;base64,".Length));
                    MemoryStream ms = new MemoryStream(data);

                    var selfieId = Guid.NewGuid().ToString();

                    var blobName = $"{value.IdAttivita}/{selfieId}/selfie.jpg";

                    var bloblRef = telemetrieContainer.GetBlockBlobReference(blobName);

                    await bloblRef.UploadFromStreamAsync(ms);

                    var sas = bloblRef.GetSharedAccessSignature(
                        new Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPolicy()
                        {
                            Permissions = Microsoft.WindowsAzure.Storage.Blob.SharedAccessBlobPermissions.Read,
                            SharedAccessStartTime = DateTime.Now.AddMinutes(-5),
                            SharedAccessExpiryTime = DateTime.Now.AddYears(1)
                        });

                    blobUri = $"{bloblRef.Uri.AbsoluteUri}{sas}";

                    var jsonData = JsonConvert.SerializeObject(new
                    {
                        value.Latitudine,
                        value.Longitudine,
                        value.Istante,
                        value.IdRunner,
                        value.IdAttivita,
                        UriSelfie = blobUri
                    });

                    var message = new CloudQueueMessage(jsonData);

                    await commandsQueue.AddMessageAsync(message);
                }

                else
                {
                    var jsonData = JsonConvert.SerializeObject(new
                    {
                        value.Latitudine,
                        value.Longitudine,
                        value.Istante,
                        value.IdRunner,
                        value.IdAttivita
                    });

                    var message = new CloudQueueMessage(jsonData);

                    await commandsQueue.AddMessageAsync(message);
                }

                     

                //using (var connection = new SqlConnection("Data Source=./;Initial Catalog=HurtleDB;Integrated Security=True"))
                //{
                //    connection.Open();
                //    string query =
                //        @"
                //        INSERT INTO [dbo].[Telemetria]
                //                ([Latitudine]
                //                ,[Longitudine]
                //                ,[Istante]
                //                ,[IdRunner]
                //                ,[IdAttivita]
                //                ,[UriSelfie])
                //         VALUES
                //               (@Latitudine
                //               ,@Longitudine
                //               ,@Istante
                //               ,@IdRunner
                //               ,@IdAttivita
                //               ,@UriSelfie)";

                //    connection.Query(query, value);
                //}

                return Ok();
            }
            else
            {
                ModelState.AddModelError("Name", "Errore generico in fase di creazione");
                return BadRequest(ModelState);
            }
        }

        // PUT: Hurtle/Attivita/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: Hurtle/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
