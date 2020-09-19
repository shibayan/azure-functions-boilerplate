﻿using System.IO;
using System.Threading.Tasks;

using Boilerplate.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Boilerplate
{
    public class Function1
    {
        public Function1(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        private readonly IGreetingService _greetingService;

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult(_greetingService.SayHello(name))
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
