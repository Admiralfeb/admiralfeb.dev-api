using System.Collections.Generic;
using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Admiralfeb_dev.Shared;


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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            [CosmosDBInput("admiralfeb-dev", "Experiences", ConnectionStringSetting = "CosmosConnString")] IEnumerable<Experience> experiences,
            [CosmosDBInput("admiralfeb-dev", "Skills", ConnectionStringSetting = "CosmosConnString")] IEnumerable<SkillItem> skills)

        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            Resume resume = new(experiences, skills);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(resume);

            return response;
        }
    }
}
