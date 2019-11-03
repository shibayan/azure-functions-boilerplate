using System.Net.Http;
using System.Threading.Tasks;

namespace Boilerplate.Services
{
    public interface IHttpService
    {
        Task<string> GetAsync(string url);
    }

    public class HttpService : IHttpService
    {
        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private readonly IHttpClientFactory _httpClientFactory;
        public Task<string> GetAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient();

            return httpClient.GetStringAsync(url);
        }
    }
}
