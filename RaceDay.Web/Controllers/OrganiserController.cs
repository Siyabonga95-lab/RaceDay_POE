using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using System.Text.Json;

namespace RaceDay.Web.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly ApiService _apiService;
        public OrganiserController(ApiService apiService) => _apiService = apiService;

        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("Role") != "Organiser") return RedirectToAction("Login", "Account");
            var response = await _apiService.GetAsync("api/events");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return View(System.Text.Json.JsonDocument.Parse("[]").RootElement);
            }

            var data = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            return View(data);
        }

        [HttpGet]
        public IActionResult CreateEvent() => View();

        [HttpPost]
        public async Task<IActionResult> CreateEvent(string eventName, string eventDescription, DateTime eventDate, string eventLocation, string eventDistance, string eventType, string routeStartPoint, string routeEndPoint, IFormFile eventImage)
        {
            string imageUrl = "";

            // 1. Try to upload the image first
            if (eventImage != null && eventImage.Length > 0)
            {
                var imgResponse = await _apiService.PostFileAsync("api/images/upload", eventImage);

                if (imgResponse.IsSuccessStatusCode)
                {
                    var imgResult = JsonDocument.Parse(await imgResponse.Content.ReadAsStringAsync()).RootElement;
                    imageUrl = imgResult.GetProperty("imageUrl").GetString();
                }
                else
                {
                    // If image fails, show the EXACT error from the API
                    var imgError = await imgResponse.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Image Upload Failed: {imgResponse.StatusCode} - {imgError}";
                    return View();
                }
            }

            // 2. Try to create the event
            var eventData = new
            {
                EventName = eventName,
                EventDescription = eventDescription,
                EventDate = eventDate,
                EventLocation = eventLocation,
                EventDistance = eventDistance,
                EventType = eventType,
                RouteStartPoint = routeStartPoint,
                RouteEndPoint = routeEndPoint,
                RouteMapUrl = imageUrl
            };

            var response = await _apiService.PostAsync("api/events", eventData);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Dashboard");

            // If event creation fails, show the EXACT error from the API
            var errorContent = await response.Content.ReadAsStringAsync();
            ViewBag.Error = $"API Error: {response.StatusCode} - {errorContent}";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageCategories(int id)
        {
            ViewBag.EventId = id;
            var response = await _apiService.GetAsync($"api/categories/event/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return View(System.Text.Json.JsonDocument.Parse("[]").RootElement);
            }

            var data = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(int eventId, int categoryAge, string categoryDistance)
        {
            await _apiService.PostAsync("api/categories", new { EventID = eventId, CategoryAge = categoryAge, CategoryDistance = categoryDistance });
            return RedirectToAction("ManageCategories", new { id = eventId });
        }
    }
}