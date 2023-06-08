using HouseService.Common;
using HouseService.Managers.Extensions;
using HouseService.Accessors.Extensions;
using HouseService.Web.Middlewares;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);
AddLoger(builder);
var app = builder.Build();

AddMiddleware(app);

app.Run();


static void AddLoger(WebApplicationBuilder builder)
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();

    builder.Logging.AddConfiguration(configuration);
}

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();
    builder.Services.AddDataLayer(EnvironmentVariables.ConnectionString);
    builder.Services.AddBusinessLogic();
    builder.Services.AddInMemoryRateLimiting();
    // in-memory cache is using to store rate limit counters
    builder.Services.AddMemoryCache();

    // load configuration from appsettings.json
    builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

    // inject counter and rules stores
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddInMemoryRateLimiting();

    // the clientId/clientIp resolvers use IHttpContextAccessor.
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    // the clientId/clientIp resolvers use IHttpContextAccessor.
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

}

static void AddMiddleware(WebApplication app)
{
    app.UseIpRateLimiting();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors();
    app.UseAuthorization();

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.MapHealthChecks("/health");
    app.MapControllers();
}

