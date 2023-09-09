using CircleCat.CleanArchitecture.FullCourse.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CircleCat.CleanArchitecture.FullCourse.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}