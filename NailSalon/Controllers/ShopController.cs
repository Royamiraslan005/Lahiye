using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            var productVms = await _shopService.GetAllAsync(userId);

            return View(productVms);
        }


        // Məhsul detalları
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _shopService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
