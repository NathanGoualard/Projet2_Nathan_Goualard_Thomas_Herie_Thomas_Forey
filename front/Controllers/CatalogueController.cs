using Microsoft.AspNetCore.Mvc;
using front.Models;
using front.Filters;
using System.Net.Http.Json;

namespace front.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly HttpClient _httpClient;

        public CatalogueController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [RoleAuthorize("Étudiant", "Enseignant", "Bibliothécaire")]
        public async Task<IActionResult> Index(string? search)
        {
            var livres = await _httpClient.GetFromJsonAsync<List<Livre>>(
                "http://localhost:5000/api/livres");
            var stocks = await _httpClient
                .GetFromJsonAsync<List<Stock>>("http://localhost:5000/api/stocks");

            foreach (var livre in livres)
            {
                livre.Stock = stocks.FirstOrDefault(s => s.Id_Livres == livre.Id_Livres);
            }
            
            

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();

                livres = livres.Where(l =>
                    l.Nom.ToLower().Contains(search) ||
                    l.Auteur.Nom.ToLower().Contains(search) ||
                    l.Genre.Nom.ToLower().Contains(search)
                ).ToList();
            }

            ViewBag.Search = search;
            return View(livres);
        }
    }
}
