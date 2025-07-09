using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.BL.Services.Concretes;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly UserManager<AppUser> _userManager;
        

        public SaleController(ISaleService saleService, UserManager<AppUser> userManager)
        {
            _saleService = saleService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Success(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return RedirectToAction("Index", "Home"); // Əgər sessionId yoxdursa, yönləndir

            await _saleService.ProcessStripePaymentAsync(sessionId);
            
            return View(); // Success.cshtml view-ə yönləndirir
        }




        [HttpPost]
        public async Task<IActionResult> Create(SaleVm vm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var sessionUrl = await _saleService.CreateStripeSessionAsync(vm, user.Id);
            return Redirect(sessionUrl);
        }


        [HttpGet]
        public async Task<IActionResult> MySales()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var sales = await _saleService.GetSalesByUserAsync(user.Id);
            return View(sales);
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return View("Cancel");
        }
    }

}
