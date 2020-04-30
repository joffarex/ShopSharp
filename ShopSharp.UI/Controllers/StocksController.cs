using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.StockAdmin;
using ShopSharp.Application.StockAdmin.Dto;
using ShopSharp.Database;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetStock()
        {
            return Ok(new GetStock(_context).Exec());
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] StockDto stockDto)
        {
            return Ok(await new CreateStock(_context).Exec(stockDto));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockDto updateStockDto)
        {
            return Ok(await new UpdateStock(_context).Exec(updateStockDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            return Ok(await new DeleteStock(_context).Exec(id));
        }
    }
}