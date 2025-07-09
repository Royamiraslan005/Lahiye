using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.BL.Services.Concretes.NailSalon.Business.Services.Implementations;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly IShopService _shopService;

        public LikeController(ILikeService likeService, IShopService shopService)
        {
            _likeService = likeService;
            _shopService = shopService;
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int productId)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = User.Identity.Name;

            await _likeService.ToggleLikeAsync(userId, productId);

            // İstifadəçini bəyəndiklər səhifəsinə qaytar
            return RedirectToAction("Liked");
        }

        [HttpGet]
        public async Task<IActionResult> Liked()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = User.Identity.Name;

            var allProducts = await _shopService.GetAllAsync(userId); // ← burda parametri əlavə et
            var likedProducts = allProducts
                .Where(p => p.IsLikedByUser) // ← artıq modeldə bu məlumat var
                .ToList();

            return View(likedProducts);
        }

    }
}


