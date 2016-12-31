using System;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;

namespace BB.ScalingAzureFunctions.Api.Controllers
{
    public class ItemsController : ApiController
    {
        // GET api/values
        [SwaggerOperation("Get")]
        public async Task<string> Get()
        {
            // simulate database query
            await Task.Run(() => System.Threading.Thread.Sleep(1000));

            // return new item
            return JsonConvert.SerializeObject(new { Id = Guid.NewGuid() });
        }
    }
}
