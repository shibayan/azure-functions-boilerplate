namespace Boilerplate.Services
{
    public interface IGreetingService
    {
        string SayHello(string name);
    }

    public class GreetingService : IGreetingService
    {
        public string SayHello(string name)
        {
            return $"Hello, {name}!";
        }
    }
}
