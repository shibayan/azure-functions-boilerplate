using Boilerplate.Options;
using Boilerplate.Services;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Boilerplate.Startup))]

namespace Boilerplate
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();

            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IGreetingService, GreetingService>();
            builder.Services.AddSingleton<IHttpService, HttpService>();

            builder.Services.AddSingleton(provider => new CosmosClient(context.Configuration["CosmosConnection"], new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            }));

            builder.Services.Configure<GreetingOptions>(context.Configuration.GetSection("Greeting"));
        }
    }
}
