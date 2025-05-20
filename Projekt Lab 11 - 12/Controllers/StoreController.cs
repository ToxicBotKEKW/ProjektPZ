using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<MineController> _logger;
        private readonly IStoreService _storeService;

        public StoreController(
            ILogger<MineController> logger, 
            IStoreService storeService
            )
        {
            _logger = logger;
            _storeService = storeService;
        }

        [Authorize]
        public async Task<IActionResult> Index(int page = 1, string searchQuery = "", string sortOrder = "")
        {
            var model = await _storeService.StoreViewModel(page, 3, searchQuery, sortOrder);
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buy(int pickaxeShopId)
        {
            var (success, message) = await _storeService.Buy(pickaxeShopId);
            var model = await _storeService.StoreViewModel();

            if (!success)
            {
                model.PurchaseErrors[pickaxeShopId] = message;
                return View("Index", model);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
