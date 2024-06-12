using BidService.API.Abstraction;
using BidService.API.Repository;
using BidService.API.Service;
using BidService.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RoomService.Infrastructure.Data;
using Serilog;

namespace BidService.API.BidService.Web.SercviceExtensions
{
    public static class ServiceRegistrations
    {
        public static void ConfigureKafka(this IServiceCollection services)
        {

            services.AddHostedService<KafkaConsumerService.KafkaConsumerService>();

        }


        public static WebApplicationBuilder Addserilog(this WebApplicationBuilder services) 
        {
            services.Host.UseSerilog((context, config) =>
            {
                config.Enrich.FromLogContext()
                    .WriteTo.Console()
                    .ReadFrom.Configuration(context.Configuration);

            });

            return services;
        }

        public static IServiceCollection AppServices(this IServiceCollection services, IConfiguration configuration)
        {


            

            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(
                 configuration.GetConnectionString("DefaultConnection"),

                  b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)), ServiceLifetime.Transient);



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