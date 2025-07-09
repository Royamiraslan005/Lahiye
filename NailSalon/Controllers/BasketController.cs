using Microsoft.AspNetCore.Mvc;

namespace NailSalon.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NailSalon.BL.Services.Abstractions;
    using NailSalon.BL.Services.Concretes;
    using NailSalon.Core.Models;
    using NailSalon.Core.ViewModels;
    using Stripe.Checkout;
    using System.Threading.Tasks;

    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISaleService _saleService;
        public BasketController(IBasketService basketService, UserManager<AppUser> userManager, ISaleService saleService)
        {
            _basketService = basketService;
            _userManager = userManager;
            _saleService = saleService;
        }

        // İstifadəçinin səbətini göstərir
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.Name : null;
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (userId == null)
            {
                // İstifadəçi daxil olmayıbsa, yönləndir və ya boş səbət göstər
                return RedirectToAction("Login", "Account");
            }

            var basketItems = await _basketService.GetBasketAsync(user.Id);
            return View(basketItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddToBasket(int productId, int quantity = 1)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var user =await _userManager.FindByEmailAsync(User.Identity.Name);

            await _basketService.AddToBasketAsync(user.Id, productId, quantity);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(int id)
        {
            await _basketService.RemoveFromBasketAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var basketItems = await _basketService.GetBasketAsync(user.Id);

            if (basketItems == null || !basketItems.Any())
                return RedirectToAction(nameof(Index));

            var sessionUrl = await _saleService.CreateStripeSessionAsync(basketItems, user.Id);

            
         

            return Redirect(sessionUrl);
        }

        [HttpPost]
        public async Task<IActionResult> ClearBasket()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

  
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
                return NotFound();

            await _basketService.ClearBasketAsync(user.Id); 
            return RedirectToAction(nameof(Index));
        }

        public async Task<string> CreateStripeSessionAsync(List<BasketItemVm> basketItems, string userId)
        {
            if (basketItems == null || !basketItems.Any())
                throw new Exception("Səbət boşdur.");

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var item in basketItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(item.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                           
                        }
                    },
                    Quantity = item.Count,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7284/Sale/Success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7284/Sale/Cancel",
                Metadata = new Dictionary<string, string>
        {
            { "userId", userId }
       
        }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

    }

}
