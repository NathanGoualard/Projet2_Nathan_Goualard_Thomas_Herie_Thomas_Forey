using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetoursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RetoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/retours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Retour>>> GetRetours()
        {
            return await _context.Retours
                .Include(r => r.Stock)
                    .ThenInclude(s => s!.Livre)
                .Include(r => r.Utilisateur)
                .ToListAsync();
        }

        // GET: api/retours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Retour>> GetRetour(int id)
        {
            var retour = await _context.Retours
                .Include(r => r.Stock)
                    .ThenInclude(s => s!.Livre)
                .Include(r => r.Utilisateur)
                .FirstOrDefaultAsync(r => r.Id_Retours == id);

            if (retour == null)
            {
                return NotFound();
            }

            return retour;
        }

        // POST: api/retours
        [HttpPost]
        public async Task<ActionResult<Retour>> PostRetour(Retour retour)
        {
            var stock = await _context.Stocks.FindAsync(retour.Id_Stock);
            if (stock == null)
            {
                return BadRequest("Stock non trouvé");
            }

            stock.Nb++;  

            retour.Date = DateTime.Now;
            _context.Retours.Add(retour);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRetour), new { id = retour.Id_Retours }, retour);
        }

        // DELETE: api/retours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetour(int id)
        {
            var retour = await _context.Retours.FindAsync(id);
            if (retour == null)
            {
                return NotFound();
            }

            _context.Retours.Remove(retour);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
