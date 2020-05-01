using ShopSharp.Application.Cart;
using ShopSharp.Application.Orders;
using ShopSharp.Application.OrdersAdmin;
using ShopSharp.Application.ProductsAdmin;
using ShopSharp.Application.StockAdmin;
using ShopSharp.Application.UsersAdmin;
using GetOrder = ShopSharp.Application.OrdersAdmin.GetOrder;
using GetProduct = ShopSharp.Application.Products.GetProduct;
using GetProducts = ShopSharp.Application.Products.GetProducts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<ShopSharp.Application.Cart.GetOrder>();
            @this.AddTransient<RemoveFromCart>();


            @this.AddTransient<CreateOrder>();
            @this.AddTransient<ShopSharp.Application.Orders.GetOrder>();

            @this.AddTransient<GetProduct>();
            @this.AddTransient<GetProducts>();

            @this.AddTransient<CreateProduct>();
            @this.AddTransient<DeleteProduct>();
            @this.AddTransient<ShopSharp.Application.ProductsAdmin.GetProduct>();
            @this.AddTransient<ShopSharp.Application.ProductsAdmin.GetProducts>();
            @this.AddTransient<UpdateProduct>();

            @this.AddTransient<CreateStock>();
            @this.AddTransient<DeleteStock>();
            @this.AddTransient<GetStock>();
            @this.AddTransient<UpdateStock>();

            @this.AddTransient<CreateUser>();

            return @this;
        }
    }
}