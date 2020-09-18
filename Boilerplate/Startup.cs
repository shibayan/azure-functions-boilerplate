using System;

using Azure.Identity;

using Boilerplate.Options;
using Boilerplate.Services;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
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

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            if (string.Equals(context.EnvironmentName, "Production", StringComparison.OrdinalIgnoreCase))
            {
                // For Production (Using Key Vault with Managed Identity)
                var builtConfig = builder.ConfigurationBuilder.Build();

                builder.ConfigurationBuilder.AddAzureKeyVault(new Uri(builtConfig["KeyVaultEndpoint"]), new DefaultAzureCredential());
            }
            else
            {
                // For Local development (Using User Secrets)
                builder.ConfigurationBuilder.AddUserSecrets<Startup>();
            }
        }
    }
}
