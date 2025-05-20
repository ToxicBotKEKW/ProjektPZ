using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEquipmentService equipmentService, ILogger<EquipmentController> logger)
        {
            _equipmentService = equipmentService;
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await _equipmentService.EquipmentViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Equip(int pickaxeId)
        {
            var (succes, message) = await _equipmentService.Equip(pickaxeId);
            return RedirectToAction(nameof(Index));
        }
    }
}
