using Microsoft.AspNetCore.Mvc;
using front.Models;
using front.Filters;
using System.Net.Http.Json;

namespace front.Controllers
{
    [RoleAuthorize("Bibliothécaire")]
    public class LivresController : Controller
    {
        private readonly HttpClient _httpClient;

        public LivresController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<IActionResult> Index()
        {
            var livres = await _httpClient.GetFromJsonAsync<List<Livre>>(
                "http://localhost:5000/api/livres");

            return View(livres);
        }

       
        public async Task<IActionResult> Create()
        {
            ViewBag.Auteurs = await _httpClient.GetFromJsonAsync<List<Auteur>>(
                "http://localhost:5000/api/auteurs");

            ViewBag.Genres = await _httpClient.GetFromJsonAsync<List<Genre>>(
                "http://localhost:5000/api/genres");

            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> Create(Livre livre)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Auteurs = await _httpClient.GetFromJsonAsync<List<Auteur>>(
                    "http://localhost:5000/api/auteurs");

                ViewBag.Genres = await _httpClient.GetFromJsonAsync<List<Genre>>(
                    "http://localhost:5000/api/genres");

                return View(livre);
            }

            var payload = new
            {
                Nom = livre.Nom,
                Annee = livre.Annee,
                Id_Auteurs = livre.Id_Auteurs,
                Id_Genres = livre.Id_Genres
            };

            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5000/api/livres", payload);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Erreur lors de l'ajout du livre";
                return View(livre);
            }

            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var livre = await _httpClient.GetFromJsonAsync<Livre>(
                $"http://localhost:5000/api/livres/{id}");

            ViewBag.Auteurs = await _httpClient.GetFromJsonAsync<List<Auteur>>(
                "http://localhost:5000/api/auteurs");

            ViewBag.Genres = await _httpClient.GetFromJsonAsync<List<Genre>>(
                "http://localhost:5000/api/genres");

            return View(livre);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Livre livre)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Auteurs = await _httpClient.GetFromJsonAsync<List<Auteur>>(
                    "http://localhost:5000/api/auteurs");

                ViewBag.Genres = await _httpClient.GetFromJsonAsync<List<Genre>>(
                    "http://localhost:5000/api/genres");

                return View(livre);
            }

            var payload = new
            {
                Id_Livres = id,
                Nom = livre.Nom,
                Annee = livre.Annee,
                Id_Auteurs = livre.Id_Auteurs,
                Id_Genres = livre.Id_Genres
            };

            var response = await _httpClient.PutAsJsonAsync(
                $"http://localhost:5000/api/livres/{id}", payload);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Erreur lors de la modification";
                return View(livre);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClient.DeleteAsync(
                $"http://localhost:5000/api/livres/{id}");

            return RedirectToAction("Index");
        }
    }
}

