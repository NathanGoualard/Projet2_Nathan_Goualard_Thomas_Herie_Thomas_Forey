using Microsoft.AspNetCore.Mvc;
using front.Models;
using front.Filters;
using System.Net.Http.Json;

namespace front.Controllers
{
    public class EmpruntsController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmpruntsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        [RoleAuthorize("Bibliothécaire")]
        public async Task<IActionResult> Index()
        {
            var emprunts = await _httpClient
                .GetFromJsonAsync<List<Emprunt>>("http://localhost:5000/api/emprunts");

            return View(emprunts);
        }

        
        [RoleAuthorize("Bibliothécaire")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Utilisateurs = await _httpClient
                .GetFromJsonAsync<List<Utilisateur>>("http://localhost:5000/api/utilisateurs");

            ViewBag.Stocks = await _httpClient
                .GetFromJsonAsync<List<Stock>>("http://localhost:5000/api/stocks");

            return View();
        }

        [RoleAuthorize("Bibliothécaire")]
        [HttpPost]
        public async Task<IActionResult> Create(Emprunt emprunt)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5000/api/emprunts",
                emprunt
            );

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Erreur lors de la création de l'emprunt";
                return View(emprunt);
            }

            return RedirectToAction("Index");
        }

       
        [RoleAuthorize("Bibliothécaire")]
        [HttpPost]
        public async Task<IActionResult> Retour(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"http://localhost:5000/api/emprunts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Erreur lors du retour";
            }

            return RedirectToAction("Index");
        }

        [RoleAuthorize("Étudiant", "Enseignant", "Bibliothécaire")]
        public async Task<IActionResult> MesEmprunts()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var emprunts = await _httpClient
                .GetFromJsonAsync<List<Emprunt>>("http://localhost:5000/api/emprunts");

            var mesEmprunts = emprunts
                .Where(e => e.Id_Utilisateurs == userId)
                .ToList();

            return View(mesEmprunts);
        }
    }
}
