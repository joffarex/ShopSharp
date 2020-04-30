using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Database;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [HttpGet("")]
        // public IActionResult GetOrders(int status) => Ok(new GerOrders(_context).Exec(status));
        //
        // [HttpGet("{id}")]
        // public IActionResult GetOrder(int id) => Ok(new GerOrders(_context).Exec(id));
        //
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateOrder(int id) => Ok((await new UpdateOrder(_context).Exec(id)));
    }
}