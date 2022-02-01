using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Admiralfeb_dev.Shared;


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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        [CosmosDBInput("admiralfeb-dev", "Projects", ConnectionStringSetting = "CosmosConnString")] IEnumerable<Project> projects)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(projects.ToList());
            return response;
        }
    }
}
