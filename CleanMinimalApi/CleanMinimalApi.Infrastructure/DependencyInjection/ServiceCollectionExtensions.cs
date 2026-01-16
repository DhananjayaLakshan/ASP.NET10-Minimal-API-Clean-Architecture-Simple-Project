using CleanMinimalApi.Application.Abstractions.Persistence;
using CleanMinimalApi.Infrastructure.Persistence;
using CleanMinimalApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanMinimalApi.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Default") ?? "Data Source=app.db";

        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(cs));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
