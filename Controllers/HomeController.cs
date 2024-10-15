using Microsoft.AspNetCore.Mvc;
using HRManagementAppFrontEnd.Models;

namespace HRManagementAppFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public HomeController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["BaseUrl"];
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/retrieval");
            if (response.IsSuccessStatusCode)
            {
                var staffList = await response.Content.ReadFromJsonAsync<List<RegisterStaffDto>>();
                return View(staffList);
            }
            return View(new List<RegisterStaffDto>());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/retrieval/{id}");
            RegisterStaffDto staffToEdit = null;

            if (response.IsSuccessStatusCode)
            {
                staffToEdit = await response.Content.ReadFromJsonAsync<RegisterStaffDto>();
                ViewBag.EditStaff = staffToEdit;
            }

            var allStaffResponse = await _httpClient.GetAsync("https://localhost:7287/api/retrieval");
            if (allStaffResponse.IsSuccessStatusCode)
            {
                var allStaff = await allStaffResponse.Content.ReadFromJsonAsync<List<RegisterStaffDto>>();
                return View("Register", allStaff);
            }

            return RedirectToAction("Register");
        }
    }
}