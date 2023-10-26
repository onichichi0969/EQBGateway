using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BalanceInquiry.Micorservice;
using Gateway.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;

namespace BalanceInquiry.Micorservice
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                }).ConfigureAppConfiguration((host, config) =>
                {
                    config.AddJsonFile("ocelot.json");
                });
    }
}




