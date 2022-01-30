using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");


            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDbConnString"));
            var database = client.GetDatabase("admiralfebnet");
            var collection = database.GetCollection<Project>("projects");

            var projects = collection.Find(Builders<Project>.Filter.Empty).Sort(Builders<Project>.Sort.Ascending(p => p.Name)).ToList();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(projects);
            return response;
        }
    }
}
