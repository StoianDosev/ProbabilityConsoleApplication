using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlotMachine.Interfaces;
using System;
using System.IO;

namespace SlotMachine
{
    public class ServicesContainer
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public void RunApplication()
        {
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ISlotMashineApplication>().Run();
        }
        public void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IMachine, Machine>();
            services.AddSingleton<ISlotCalculator, SlotCalculator>();
            services.AddSingleton<IProbabilityGenerator, ProbabilityGenerator>();
            services.AddSingleton<ISlotMashineApplication, SlotMashineApplication>();

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)                             
                .Build();

            var conf =  _configuration.GetSection("Apple");
            var Wildcard = double.Parse( _configuration["Wildcard"]);
            var Apple = double.Parse( _configuration["Apple"]);
            var Pineapple =double.Parse( _configuration["Pineapple"]);
            var Banana =double.Parse( _configuration["Banana"]);
            
            services.AddOptions();
            services.Configure<CoefficientOptions>(o=>{
                o.Apple = Apple;
                o.Banana = Banana;
                o.Pineapple = Pineapple;
                o.Wildcard = Wildcard;
                 });

                 
            _serviceProvider = services.BuildServiceProvider(true);
        }

        public void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}