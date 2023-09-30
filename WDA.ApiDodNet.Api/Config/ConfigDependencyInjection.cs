using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Application.Services;
using WDA.ApiDotNet.Application.Profiles;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Application.Services.Interface;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDotNet.Infra.Data.Context;
using WDA.ApiDotNet.Infra.Data.Repository;

namespace WDA.ApiDotNet.Api
{
    public static class ConfigDependencyInjection
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
            services.AddAutoMapper(typeof(BookstoreProfile));
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<IRentalsService, RentalsService>();
            services.AddScoped<IUsersService, UsersService>();
            return services;
        }
    }
}
