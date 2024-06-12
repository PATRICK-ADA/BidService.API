using BidService.API.BidService.Web.SercviceExtensions;
using Extensions.NewSeriLog;



public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
       
        builder.Addserilog();
        builder.Services.ConfigureKafka();

        builder.Services.AppServices(builder.Configuration);
        builder.Services.ConfigureKafka();
        builder.Services.AddSwaggerServices();

      
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
        app.UseSerilogDbMigrationLogging();

        app.Run();
    }

}