using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Business.Services;
using WDA.ApiDotNet.Business.Interfaces.IRepository;
using WDA.ApiDotNet.Business.Interfaces.IServices;
using WDA.ApiDotNet.Business.Services;
using WDA.ApiDotNet.Data.Context;
using WDA.ApiDotNet.Data.Repository;

namespace WDA.ApiDotNet.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextDb>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("BookstoreConnection")));
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IPublishersRepository, PublishersRepository>();
            services.AddScoped<IRentalsRepository, RentalsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Business.Profiles.Mapper));
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<IRentalsService, RentalsService>();
            services.AddScoped<IUsersService, UsersService>();
            return services;
        }
    }
}
