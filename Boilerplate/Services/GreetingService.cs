using Boilerplate.Options;

using Microsoft.Extensions.Options;

namespace Boilerplate.Services
{
    public interface IGreetingService
    {
        string SayHello(string name);
    }

    public class GreetingService : IGreetingService
    {
        public GreetingService(IOptions<GreetingOptions> options)
        {
            _options = options.Value;
        }

        private readonly GreetingOptions _options;

        public string SayHello(string name)
        {
            return $"{_options.Prefix}, {name}!";
        }
    }
}
