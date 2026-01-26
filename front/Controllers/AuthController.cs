using Microsoft.AspNetCore.Mvc;
using front.Models;
using System.Net.Http.Json;

namespace front.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           
            model.Login = model.Login?.Trim() ?? "";
            model.MotDePasse = model.MotDePasse?.Trim() ?? "";

            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5000/api/utilisateurs/login",
                new
                {
                    login = model.Login,
                    motDePasse = model.MotDePasse
                });

            
            if (!response.IsSuccessStatusCode)
            {
                model.ErrorMessage = "Login ou mot de passe incorrect";
                return View(model);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();

          
            if (result == null || result.Role == null)
            {
                model.ErrorMessage = "Erreur lors de la connexion";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", result.Id_Utilisateurs);
            HttpContext.Session.SetString("Login", result.Login);
            HttpContext.Session.SetString("Role", result.Role.Nom);

            
            return result.Role.Nom switch
            {
                "Admin" => RedirectToAction("Index", "Admin"),
                "Bibliothecaire" => RedirectToAction("Index", "Bibliothecaire"),
                _ => RedirectToAction("Index", "Home")
            };
        }
    }

    public class LoginResult
    {
        public int Id_Utilisateurs { get; set; }
        public string Login { get; set; } = string.Empty;
        public RoleDto Role { get; set; } = new();
    }

    public class RoleDto
    {
        public string Nom { get; set; } = string.Empty;
    }
}
