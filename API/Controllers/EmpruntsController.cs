using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpruntsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpruntsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/emprunts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprunt>>> GetEmprunts()
        {
            return await _context.Emprunts
                .Include(e => e.Stock)
                    .ThenInclude(s => s!.Livre)
                .Include(e => e.Utilisateur)
                .ToListAsync();
        }

        // GET: api/emprunts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprunt>> GetEmprunt(int id)
        {
            var emprunt = await _context.Emprunts
                .Include(e => e.Stock)
                    .ThenInclude(s => s!.Livre)
                .Include(e => e.Utilisateur)
                .FirstOrDefaultAsync(e => e.Id_Emprunts == id);

            if (emprunt == null)
            {
                return NotFound();
            }

            return emprunt;
        }

        // POST: api/emprunts
        [HttpPost]
        public async Task<ActionResult<Emprunt>> PostEmprunt(Emprunt emprunt)
        {

            var stock = await _context.Stocks.FindAsync(emprunt.Id_Stock);
            if (stock == null)
            {
                return BadRequest("Stock non trouvé");
            }

            if (stock.Nb <= 0)  
            {
                return BadRequest("Aucun exemplaire disponible");
            }

            stock.Nb--;

            emprunt.Date = DateTime.Now;
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmprunt), new { id = emprunt.Id_Emprunts }, emprunt);
        }

        // DELETE: api/emprunts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprunt(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return NotFound();
            }

            _context.Emprunts.Remove(emprunt);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
