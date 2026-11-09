using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using System.Text.Json;

namespace RaceDay.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;
        public HomeController(ApiService apiService) => _apiService = apiService;

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync("api/events");

                if (!response.IsSuccessStatusCode)
                {
                    // THIS WILL SHOW US THE EXACT ERROR FROM THE API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"API Error: {response.StatusCode}. Details: {errorContent}";

                    return View(System.Text.Json.JsonDocument.Parse("[]").RootElement);
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(jsonString).RootElement;
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An unexpected error occurred: {ex.Message}";
                return View(System.Text.Json.JsonDocument.Parse("[]").RootElement);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventResponse = await _apiService.GetAsync($"api/events/{id}");
            var categoriesResponse = await _apiService.GetAsync($"api/categories/event/{id}");

            ViewBag.EventData = JsonDocument.Parse(await eventResponse.Content.ReadAsStringAsync()).RootElement;
            ViewBag.Categories = JsonDocument.Parse(await categoriesResponse.Content.ReadAsStringAsync()).RootElement;

            return View();
        }
    }
}