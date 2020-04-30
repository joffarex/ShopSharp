using System.Linq;
using System.Threading.Tasks;
using ShopSharp.Database;

namespace ShopSharp.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private readonly ApplicationDbContext _context;

        public UpdateOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExecAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return false;

            order.Status += 1;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}