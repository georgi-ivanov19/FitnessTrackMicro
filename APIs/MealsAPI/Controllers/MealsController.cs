using MealsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace MealsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        // Cosmos DB details, In real use cases, these details should be configured in secure configuraion file.
        private readonly string CosmosDBAccountUri = "https://fitness-track-cn.documents.azure.com:443/";
        // host.docker.internal
        private readonly string CosmosDBAccountPrimaryKey = "nET8oDlA3KtgqkIyYefoe78mABsbTcOQgwTtYCBFyNOJs4htUPh9DXKjLPJd4BgtR3Z4XSsXLg8fACDbcNSiUQ==";
        private readonly string CosmosDbName = "FitnessTrack";
        private readonly string CosmosDbContainerName = "Meals";

        /// <summary>
        /// Commom Container Client, you can also pass the configuration paramter dynamically.
        /// </summary>
        /// <returns> Container Client </returns>
        private Container ContainerClient()
        {

            CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Meal meal)
        {
            try
            {
                
                var container = ContainerClient();
                ContainerProperties properties = await container.ReadContainerAsync();
                Console.WriteLine(properties.PartitionKeyPath);
                var response = await container.CreateItemAsync(meal, new PartitionKey(meal.applicationUserId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
