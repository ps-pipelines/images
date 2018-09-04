using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PetStore.Images
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:5000/")
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                })
               .UseStartup<Startup>();
    }
}
