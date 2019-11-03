using Boilerplate.Options;
using Boilerplate.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Boilerplate.Startup))]

namespace Boilerplate
{
    public class Startup : FunctionsStartup
    {
        public Startup()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            Configuration = config.Build();
        }

        public IConfiguration Configuration { get; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IGreetingService, GreetingService>();
            builder.Services.AddSingleton<IHttpService, HttpService>();

            builder.Services.Configure<GreetingOptions>(Configuration.GetSection("Greeting"));
        }
    }
}
