using Microsoft.AspNetCore.Mvc;
using HRManagementAppFrontEnd.Models;

namespace HRManagementAppFrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public AdminController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["BaseUrl"];
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/admin/login", model);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("SuccessfulRequests");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        public IActionResult SuccessfulRequests()
        {
            // Logic to fetch and display successful requests
            return View();
        }
    }
}

