using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NasaRoboticRover;
namespace NasaRoboticRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddApp()
            .BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<App>();
            app.Init();
        }
    }

}
