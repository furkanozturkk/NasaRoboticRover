using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace NasaRoboticRover
{
    public static class AppExtensions
    {
        public static ServiceCollection AddApp(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<App>();
            return serviceCollection;
        }
    }

}