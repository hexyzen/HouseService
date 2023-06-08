using HouseService.Accessors.Context;
using Microsoft.Extensions.DependencyInjection;

namespace HouseService.Accessors.Extensions
{
    public static class DiExtension
    {
        public static void AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<HouseServiceContext>(_ => new(connectionString));
        }
    }
}
