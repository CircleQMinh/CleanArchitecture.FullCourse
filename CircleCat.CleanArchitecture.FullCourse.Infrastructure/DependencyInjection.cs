using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Repositories;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services;
using CircleCat.CleanArchitecture.FullCourse.Application.Interfaces.Services.Product;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.Database;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Persistence.Repository;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Services;
using CircleCat.CleanArchitecture.FullCourse.Infrastructure.Services.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CircleCat.CleanArchitecture.FullCourse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //Register Service
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            //Register Persistence
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:sqlConnection"])
            );

            return services;
        }
    }
}