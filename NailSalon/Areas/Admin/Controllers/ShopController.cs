using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IWebHostEnvironment _environment;

        public ShopController(IShopService shopService, IWebHostEnvironment environment)
        {
            _shopService = shopService;
            _environment = environment;
        }


        public async Task<IActionResult> Index()
        {
            string userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            var productVms = await _shopService.GetAllAsync(userId);

            return View(productVms);
        }


 

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShopProductVm vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Form düzgün doldurulmayıb!";
                return View(vm);
            }

            await _shopService.CreateAsync(vm, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


   
        public async Task<IActionResult> Update(int id)
        {
            var product = await _shopService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product); 
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ShopProductVm model)
        {
            if (ModelState.IsValid)
            {
                await _shopService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _shopService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
