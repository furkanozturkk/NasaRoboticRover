using Microsoft.Extensions.DependencyInjection;

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