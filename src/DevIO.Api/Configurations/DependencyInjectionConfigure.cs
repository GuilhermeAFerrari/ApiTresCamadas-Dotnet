using DevIO.Business.Interfaces;
using DevIO.Business.Notifiers;
using DevIO.Business.Services;
using DevIO.Data.Context;
using DevIO.Data.Repository;

namespace DevIO.Api.Configurations;

public static class DependencyInjectionConfigure
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        //Data
        services.AddScoped<DatabaseContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        //Bussiness
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<INotifier, Notifier>();

        return services;
    }
}
