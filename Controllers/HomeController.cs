using Microsoft.AspNetCore.Mvc;
using HRManagementAppFrontEnd.Models;

namespace HRManagementAppFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var response = await _httpClient.GetAsync("https://localhost:7287/api/retrieval");
            if (response.IsSuccessStatusCode)
            {
                var staffList = await response.Content.ReadFromJsonAsync<List<RegisterStaffDto>>();
                return View(staffList);
            }
            return View(new List<RegisterStaffDto>());
        }

        // Updated Edit Method
        public async Task<IActionResult> Edit(int id)
        {
            // Fetch the specific staff member by ID
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/retrieval/{id}");
            RegisterStaffDto staffToEdit = null;

            if (response.IsSuccessStatusCode)
            {
                staffToEdit = await response.Content.ReadFromJsonAsync<RegisterStaffDto>();
                ViewBag.EditStaff = staffToEdit; // Store the staff to be edited in ViewBag
            }

            // Fetch all staff members to display in the list
            var allStaffResponse = await _httpClient.GetAsync("https://localhost:7287/api/retrieval");
            if (allStaffResponse.IsSuccessStatusCode)
            {
                var allStaff = await allStaffResponse.Content.ReadFromJsonAsync<List<RegisterStaffDto>>();
                return View("Register", allStaff); // Return the Register view with all staff
            }

            return RedirectToAction("Register"); // Handle error case
        }


    }
}