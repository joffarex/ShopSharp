using System.Threading.Tasks;
using ShopSharp.Domain.Infrastructure;

namespace ShopSharp.Application.ProductsAdmin
{
    [Service]
    public class DeleteProduct
    {
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<bool> ExecAsync(int id)
        {
            return await _productManager.DeleteProduct(id) > 0;
        }
    }
}