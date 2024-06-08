using BidService.API.Abstraction;
using BidService.API.Repository;
using BidService.API.Service;
using BidService.Core.Abstraction;
using Microsoft.OpenApi.Models;

namespace BidService.API.BidService.Web.SercviceExtensions
{
    public static class ServiceRegistrations
    {
        public static void ConfigureKafka(this IServiceCollection services)
        {


            services.AddHostedService<KafkaConsumerService.KafkaConsumerService>();



        }

        public static IServiceCollection AppServices(this IServiceCollection services)
        {



            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<IBidService, BidServices>();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithMethods("GET", "PUT", "DELETE", "POST")
                    );
            });
            return services;
        }


        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddSwaggerGen
                (g =>
                {
                    g.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Auction BidService Management API",
                        Description = "Documentation for Auction BidService Management API"

                    });

                });
            return services;
        }
    }
}