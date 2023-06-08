using HouseService.Managers.Accessors;
using HouseService.Managers.Interfaces;
using HouseService.Managers.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace HouseService.Managers.Extensions
{
    public static class DiExtension
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services
                .AddScoped<IDogAccessor, DogAccessor>()
                .AddScoped<IDogManager, DogManager>();
        }
    }
}
