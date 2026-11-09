using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using System.Text.Json;

namespace RaceDay.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var response = await _apiService.PostAsync("api/auth/login", new { Username = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonDocument.Parse(content).RootElement;

                HttpContext.Session.SetString("UserID", result.GetProperty("userID").GetInt32().ToString());
                HttpContext.Session.SetString("Role", result.GetProperty("role").GetString());
                HttpContext.Session.SetString("Username", username);

                if (result.GetProperty("role").GetString() == "Organiser")
                    return RedirectToAction("Dashboard", "Organiser");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string role, string name, string email, string phone)
        {
            var response = await _apiService.PostAsync("api/auth/register",
                new { Username = username, Password = password, Role = role, Name = name, Email = email, Phone = phone });

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            // Get the actual error message from the API
            var errorContent = await response.Content.ReadAsStringAsync();
            ViewBag.Error = $"API Error: {response.StatusCode} - {errorContent}";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}