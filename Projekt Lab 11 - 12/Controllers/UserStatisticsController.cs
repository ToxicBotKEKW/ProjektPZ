using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Controllers
{
    public class UserStatisticsController : Controller
    {
        private readonly IUserStatisticsService _userStatisticsService;

        public UserStatisticsController(IUserStatisticsService userStatisticsService)
        {
            _userStatisticsService = userStatisticsService;
        }

        [Authorize]
        public async Task<IActionResult> Index(String userId)
        {
            var model = await _userStatisticsService.UserStatisticsViewModel(userId);

            return View(model);
        }
    }
}
