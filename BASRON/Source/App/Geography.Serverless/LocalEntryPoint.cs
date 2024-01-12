using Framework.Configuration.Models;
using BASRON.DataAccess;
using Framework.Configuration;

namespace BASRON.Serverless;

/// <summary>
/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
/// </summary>
public class LocalEntryPoint
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var btrasctionProvider = services.GetRequiredService<BTransactionTableCreationProvider>();
            await btrasctionProvider.Initialize("BTransaction");

            var listOfFilesTableCreationProvider = services.GetRequiredService<RequestTableCreationProvider>();
            await listOfFilesTableCreationProvider.Initialize("Request");

        }
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly }, args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}