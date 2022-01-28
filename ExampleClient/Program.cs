using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
namespace ExampleClient
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
                    webBuilder.UseKestrel(options => {

                       // var certificate = new X509Certificate2("localhost.crt", "Redacted");
                      //  options.Listen(IPAddress.Any, 444, listenOptions => {
                        //    listenOptions.UseHttps(certificate);
                      //  });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
