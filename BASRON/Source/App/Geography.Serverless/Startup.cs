using Framework.Business.ServiceProvider.Storage;
using Framework.Configuration.Models;
using BASRON.Business;
using BASRON.Business.GraphQL;
using BASRON.Serverless.Extensions;
namespace BASRON.Serverless;
using Framework.Service.Extension;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddControllersWithViews().AddNewtonsoftJson();
        services.AddScoped(typeof(IStorageManager<AmazonS3ConfigurationOptions>), typeof(StorageManager));
        services.ConfigureClientServices();
        services.ConfigureGraphQLServices();
        services.ConfigureDataProvider();
        //services.ConfigureAwsCongnitoSecurity();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.AddProblemDetailsSupport();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseGraphQLUpload<AppSchema>().UseGraphQL<AppSchema>();
        app.UseGraphQLPlayground(options: new GraphQL.Server.Ui.Playground.PlaygroundOptions());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            //endpoints.MapGraphQL<AppSchema>().RequireAuthorization();
            endpoints.MapGraphQL<AppSchema>();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}