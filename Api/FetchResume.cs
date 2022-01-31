using System.Collections.Generic;
using System.Linq;
using System.Net;
using Admiralfeb_dev.Shared;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Admiralfeb_dev.Api
{
    public class FetchResume
    {
        private readonly ILogger _logger;

        public FetchResume(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FetchResume>();
        }

        [Function("FetchResume")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var client = new CosmosClientBuilder(System.Environment.GetEnvironmentVariable("CosmosConnString"))
            .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
            .Build();

            List<Experience> experiences = client.GetContainer("admiralfeb-dev", "Experiences").GetItemLinqQueryable<Experience>(allowSynchronousQueryExecution: true).ToList();
            List<SkillItem> skills = client.GetContainer("admiralfeb-dev", "Skills").GetItemLinqQueryable<SkillItem>(allowSynchronousQueryExecution: true).ToList();

            Resume resume = new(experiences, skills);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(resume);

            return response;
        }
    }
}
