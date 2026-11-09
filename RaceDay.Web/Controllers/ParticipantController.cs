using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using System.Text;
using System.Text.Json;

namespace RaceDay.Web.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ApiService _apiService;
        public ParticipantController(ApiService apiService) => _apiService = apiService;

        public async Task<IActionResult> MyEnrollments()
        {
            if (HttpContext.Session.GetString("Role") != "Participant") return RedirectToAction("Login", "Account");
            var response = await _apiService.GetAsync("api/enrollments/participant");
            var data = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            return View(data);
        }

        public async Task<IActionResult> MyResults()
        {
            if (HttpContext.Session.GetString("Role") != "Participant") return RedirectToAction("Login", "Account");
            var response = await _apiService.GetAsync("api/results/participant");
            var data = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int eventId, int categoryId)
        {
            var response = await _apiService.PostAsync("api/enrollments", new { EventID = eventId, CategoryID = categoryId });
            if (response.IsSuccessStatusCode) return RedirectToAction("MyEnrollments");
            TempData["Error"] = "Failed to enroll. You may already be enrolled in this category.";
            return RedirectToAction("Details", "Home", new { id = eventId });
        }
    }
}