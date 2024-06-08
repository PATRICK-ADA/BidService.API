using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using RoomService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BidService.API.KafkaConsumerService;
using BidService.API.BidService.Web.SercviceExtensions;



public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseNpgsql(
             configuration.GetConnectionString("DefaultConnection"),

              b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)), ServiceLifetime.Transient);


        builder.Host.UseSerilog((context, config) =>
        {
            config.Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(context.Configuration);

        });



        builder.Services.ConfigureKafka();

        builder.Services.AppServices();
        builder.Services.ConfigureKafka();
        builder.Services.AddSwaggerServices();
        


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        var app = builder.Build();


        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true)
                  .AllowCredentials());

        app.UseAuthorization();
        app.MapControllers();
        app.UseSerilogMigrationSetUpInfo();

        app.Run();
    }

}