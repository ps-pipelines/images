using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PetStore.Images.Infrastructure;

namespace PetStore.Images
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStore, SqlStore>();

            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.TypeNameHandling = TypeNameHandling.Objects;
                opts.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            }); 

            services.AddCors(options => options.AddPolicy("AllowAllOrigins",
                policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin();
                }));

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
