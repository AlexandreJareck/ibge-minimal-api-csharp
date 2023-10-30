using Challenge.Balta.IBGE.MapperProfile;
using FluentValidation;
using IBGE.DTO;
using IBGE.FluentValidator;
using IBGE.Interfaces;
using IBGE.Repository;
using IBGE.Services;

namespace IBGE.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILocationService, LocationService>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IValidator<LocationViewModel>, LocationViewModelValidator>();
        }
    }
}