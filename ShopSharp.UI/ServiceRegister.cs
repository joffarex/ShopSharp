using System.Linq;
using System.Reflection;
using ShopSharp.Application;
using ShopSharp.Database;
using ShopSharp.Domain.Infrastructure;
using ShopSharp.UI.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedType = serviceType.Assembly.DefinedTypes;

            var services = definedType
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services) @this.AddTransient(service);

            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IStockManager, StockManager>();
            @this.AddScoped<ISessionManager, SessionManager>();

            return @this;
        }
    }
}