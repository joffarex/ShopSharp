using System;
using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Database;

namespace ShopSharp.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private readonly ApplicationDbContext _context;

        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Exec(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product ?? throw new Exception("Product not found"));
            await _context.SaveChangesAsync();

            return true;
        }
    }
}