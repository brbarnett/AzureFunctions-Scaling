using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BB.ScalingAzureFunctions.Api.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Swashbuckle.Swagger.Annotations;

namespace BB.ScalingAzureFunctions.Api.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly CloudTable _table;

        public PeopleController()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            this._table = tableClient.GetTableReference("people");
        }

        // GET api/people?id={id}
        [SwaggerOperation("Get")]
        public async Task<HttpResponseMessage> Get(string id)
        {
            Person person = await this.GetById(id);

            if (person == null) return base.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find person");

            return base.Request.CreateResponse(HttpStatusCode.OK, person);
        }

        // GET api/people?id={id}&lastUpdated={lastUpdated}
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [HttpPatch]
        public async Task<HttpResponseMessage> Update(string id, string lastUpdated)
        {
            Person person = await this.GetById(id);

            if (person == null) return base.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not find person");

            person.LastUpdated = DateTime.Parse(lastUpdated);

            TableOperation updateOperation = TableOperation.Replace(person);

            await this._table.ExecuteAsync(updateOperation);

            return base.Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task<Person> GetById(string id)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<Person>("Person", id);

            TableResult retrievedResult = await this._table.ExecuteAsync(retrieveOperation);

            // return new item
            return (Person)retrievedResult.Result;
        }
    }
}
