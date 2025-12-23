using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuteursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuteursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Auteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auteur>>> GetAuteurs()
        {
            return await _context.Auteurs.ToListAsync();
        }

        // GET: api/Auteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auteur>> GetAuteur(int id)
        {
            var auteur = await _context.Auteurs
                .FirstOrDefaultAsync(a => a.Id_Auteurs == id);

            if (auteur == null)
            {
                return NotFound();
            }

            return auteur;
        }

        // POST: api/Auteurs
        [HttpPost]
        public async Task<ActionResult<Auteur>> PostAuteur(Auteur auteur)
        {
            _context.Auteurs.Add(auteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuteur), new { id = auteur.Id_Auteurs }, auteur);
        }

        // PUT: api/Auteurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuteur(int id, Auteur auteur)
        {
            if (id != auteur.Id_Auteurs)
            {
                return BadRequest();
            }

            _context.Entry(auteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuteurExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Auteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuteur(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            _context.Auteurs.Remove(auteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuteurExists(int id)
        {
            return _context.Auteurs.Any(e => e.Id_Auteurs == id);
        }
    }
}
