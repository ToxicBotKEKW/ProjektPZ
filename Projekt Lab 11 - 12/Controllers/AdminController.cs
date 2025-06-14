using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<EquipmentController> _logger;

        public AdminController(IAdminService adminService, ILogger<EquipmentController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            AdminViewModel model = await _adminService.AdminViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddToShop(
            int pickaxeShopId,
            double ironCost,
            double goldCost,
            double diamondCost
            )
        {
            await _adminService.AddToShop(pickaxeShopId, ironCost, goldCost, diamondCost);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveFromShop(int pickaxeShopId)
        {
            await _adminService.RemoveFromShop(pickaxeShopId);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePickaxe(AdminViewModel adminViewModel)
        {
            await _adminService.CreatePickaxe(adminViewModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
