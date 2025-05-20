using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Controllers
{
    public class MineController : Controller
    {
        private readonly ILogger<MineController> _logger;
        private readonly IMineService _mineService;

        public MineController(
            IMineService mineService,
            ILogger<MineController> logger
            )
        {
            _mineService = mineService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> IronMine() {
            var model = await _mineService.MineViewModel();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> GoldMine()
        {
            var model = await _mineService.MineViewModel();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> DiamondMine()
        {
            var model = await _mineService.MineViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PointsForClick([FromBody] ResourceType resourceType)
        {
            var result = await _mineService.PointsForClick(resourceType);

            if (double.TryParse(result, out double points))
            {
                return Json(new { success = true, points = points });
            }
            else
            {
                return Json(new { success = false, message = result });
            }
        }


    }
}
