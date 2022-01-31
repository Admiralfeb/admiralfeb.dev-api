using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Admiralfeb_dev.Shared;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Admiralfeb_dev.Api
{
    public class FetchProjects
    {
        private readonly ILogger _logger;

        public FetchProjects(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FetchProjects>();
        }

        [Function("FetchProjects")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var client = new CosmosClientBuilder(System.Environment.GetEnvironmentVariable("CosmosConnString"))
            .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .Build();
            var projectContainer = client.GetContainer("admiralfeb-dev", "Projects");

            var projects = projectContainer.GetItemLinqQueryable<Project>(allowSynchronousQueryExecution: true).ToList();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(projects);
            return response;
        }
    }
}
