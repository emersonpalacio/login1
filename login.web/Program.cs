using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using login.web.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace login.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            RuunSeed(host);
            host.Run();
        }

        private static void RuunSeed(IWebHost host)
        {
            IServiceScopeFactory scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                SeedDb seedDb = scope.ServiceProvider.GetService<SeedDb>();
                seedDb.SeedAsync().Wait();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
           return   WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
        }
    }
}
