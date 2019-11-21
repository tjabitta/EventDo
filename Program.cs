using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EvendoTest_v1.Search.Models.Elasticsearch;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EvendoTest_v1.Search
{
    public class Program
    {
        // make sure your program is running C# 7.1 or later
        public static async Task Main(string[] args)
        {
            // setup host
            var host = CreateWebHostBuilder(args).Build();
            
            // load records from Csv to Elasticsearch
            using (var scope = host.Services.CreateScope())
            {
                var loader = scope.ServiceProvider.GetRequiredService<ItemsUtil>();
                await loader.RunAsync();
            }
            
            // change our run to async
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}