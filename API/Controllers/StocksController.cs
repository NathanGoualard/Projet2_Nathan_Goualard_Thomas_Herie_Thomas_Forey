using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            return await _context.Stocks
                .Include(s => s.Livre)
                    .ThenInclude(l => l!.Auteur)
                .Include(s => s.Livre)
                    .ThenInclude(l => l!.Genre)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            var stock = await _context.Stocks
                .Include(s => s.Livre)
                    .ThenInclude(l => l!.Auteur)
                .Include(s => s.Livre)
                    .ThenInclude(l => l!.Genre)
                .FirstOrDefaultAsync(s => s.Id_Stock == id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id_Stock }, stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(int id, Stock stock)
        {
            if (id != stock.Id_Stock)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.Id_Stock == id);
        }
    }
}
