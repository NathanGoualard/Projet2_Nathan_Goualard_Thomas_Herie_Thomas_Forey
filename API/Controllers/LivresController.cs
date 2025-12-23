using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LivresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livre>>> GetLivres()
        {
            return await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Genre)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livre>> GetLivre(int id)
        {
            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Genre)
                .FirstOrDefaultAsync(l => l.Id_Livres == id);

            if (livre == null)
            {
                return NotFound();
            }

            return livre;
        }

        [HttpPost]
        public async Task<ActionResult<Livre>> PostLivre(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivre), new { id = livre.Id_Livres }, livre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivre(int id, Livre livre)
        {
            if (id != livre.Id_Livres)
            {
                return BadRequest();
            }

            _context.Entry(livre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivreExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivre(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.Id_Livres == id);
        }
    }
}
