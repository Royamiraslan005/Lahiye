using Microsoft.AspNetCore.Mvc;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult MenuSelection()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Confirm(MenuSelectionVm vm)
        {
            // Seçimləri Session-a, DB-yə və ya istifadəçi profilinə əlavə edə bilərsən
            TempData["SelectedItems"] = string.Join(", ", vm.SelectedItems);

            // Əlavə olaraq rezervasiya tamamlandıqdan sonra yönləndirmək olar
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            ViewBag.SelectedItems = TempData["SelectedItems"];
            return View();
        }
    }
}
