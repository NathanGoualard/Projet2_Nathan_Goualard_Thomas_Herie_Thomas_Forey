using Microsoft.AspNetCore.Mvc;
using front.Models;
using front.Filters;
using System.Net.Http.Json;

namespace front.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [RoleAuthorize("Étudiant", "Enseignant", "Bibliothécaire")]
        public async Task<IActionResult> Index()
        {
            var livres = await _httpClient.GetFromJsonAsync<List<Livre>>(
                "http://localhost:5000/api/livres");

            if (livres == null || livres.Count == 0)
            {
                ViewBag.Error = "Aucun livre disponible";
                return View(new List<Livre>());
            }

            
            var random = new Random();
            var randomLivres = livres
                .OrderBy(x => random.Next())
                .Take(4)
                .ToList();

            return View(randomLivres);
        }
    }
}

