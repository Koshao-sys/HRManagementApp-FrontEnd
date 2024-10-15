using Microsoft.AspNetCore.Mvc;
using HRManagementAppFrontEnd.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HRManagementAppFrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/admin/login", model);
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

