using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilisateursController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return await _context.Utilisateurs
                .Include(u => u.Role)
                .Select(u => new Utilisateur
                {
                    Id_Utilisateurs = u.Id_Utilisateurs,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    Login = u.Login,
                    Id_Roles = u.Id_Roles,
                    Role = u.Role
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id_Utilisateurs == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            utilisateur.MotDePasse = "";
            return utilisateur;
        }

        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            utilisateur.MotDePasse = HashPassword(utilisateur.MotDePasse);

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            utilisateur.MotDePasse = "";
            return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id_Utilisateurs }, utilisateur);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Id_Utilisateurs)
            {
                return BadRequest();
            }

            var existingUser = await _context.Utilisateurs.AsNoTracking().FirstOrDefaultAsync(u => u.Id_Utilisateurs == id);
            if (existingUser != null && utilisateur.MotDePasse != existingUser.MotDePasse)
            {
                utilisateur.MotDePasse = HashPassword(utilisateur.MotDePasse);
            }
            else
            {
                utilisateur.MotDePasse = existingUser?.MotDePasse;
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Utilisateur>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Utilisateurs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == loginDto.Login);

            if (user == null || !VerifyPassword(loginDto.MotDePasse, user.MotDePasse))
            {
                return Unauthorized("Identifiants invalides");
            }

            user.MotDePasse = "";
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id_Utilisateurs == id);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashOfInput = HashPassword(inputPassword);
            return hashOfInput == storedHash;
        }
    }

    public class LoginDto
    {
        public string Login { get; set; }
        public string MotDePasse { get; set; }
    }
}
