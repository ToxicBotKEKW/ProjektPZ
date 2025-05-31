using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Services;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Controllers
{
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;

        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await _upgradeService.UpgradeViewModel();
            return View(model);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Upgrade(int upgradeMineId)
        {
            var (success, message) = await _upgradeService.Upgrade(upgradeMineId);
            var model = await _upgradeService.UpgradeViewModel();

            if (!success)
            {
                model.PurchaseErrors[upgradeMineId] = message;
                return View("Index", model);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
