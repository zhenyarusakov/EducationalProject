using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EducationalProject.Extensions
{
    public static class ConfigureServices
    {
        public static void AddSwaggerGenConfigure (this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EP.Api", Version = "v1" });
            }); 
        }

        public static void AddControllersOptions(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions
                    .ReferenceHandler = ReferenceHandler.Preserve);
        }
    }
}