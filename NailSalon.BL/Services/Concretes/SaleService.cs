using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IShopRepository _shopRepo;
        private readonly IBasketService _basketService;

        public SaleService(ISaleRepository saleRepo, IShopRepository shopRepo, IBasketService basketService)
        {
            _saleRepo = saleRepo;
            _shopRepo = shopRepo;
            _basketService = basketService;
        }

        public async Task<string> CreateStripeSessionAsync(SaleVm vm, string userId)
        {
           
            var product = await _shopRepo.GetByIdAsync(vm.ProductId);
            if (product == null)
                throw new Exception("Məhsul tapılmadı.");

            if (vm.Quantity <= 0)
                vm.Quantity = 1;

            var amountInCents = (long)(product.Price * 100);
            if (amountInCents < 1)
                amountInCents = 100; 

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
    {
        new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "usd",
                UnitAmount = amountInCents,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = product.Name,
                    Description = product.Description
                }
            },
            Quantity = vm.Quantity
        }
    },
                Mode = "payment",
                SuccessUrl = "https://localhost:7284/Sale/Success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7284/Sale/Cancel",
                Metadata = new Dictionary<string, string>
    {
        { "productId", product.Id.ToString() },
        { "userId", userId },
        { "quantity", vm.Quantity.ToString() }
    }
            };




            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Url;
        }


        public async Task ProcessStripePaymentAsync(string sessionId)
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);

            var userId = session.Metadata["userId"];

     
            var sales = new List<Sale>();

            int index = 0;
            while (true)
            {
                string productIdKey = $"productId_{index}";
                string quantityKey = $"quantity_{index}";

                if (!session.Metadata.ContainsKey(productIdKey))
                    break; 

                int productId = int.Parse(session.Metadata[productIdKey]);
                int quantity = int.Parse(session.Metadata[quantityKey]);

                var product = await _shopRepo.GetByIdAsync(productId);
                if (product == null)
                {
  
                    index++;
                    continue;
                }

                var sale = new Sale
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    UserId = userId,
                    TotalPrice = product.Price * quantity
                };

                sales.Add(sale);
                index++;
            }

            foreach (var sale in sales)
            {
                await _saleRepo.CreateAsync(sale);
            }
            await _basketService.ClearBasketAsync(userId);
        }


        public async Task<List<SaleVm>> GetSalesByUserAsync(string userId)
        {
            var sales = await _saleRepo.GetByUserIdAsync(userId);
            return sales.Select(x => new SaleVm
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice
            }).ToList();
        }

        public async Task<string> CreateStripeSessionAsync(List<BasketItemVm> basketItems, string userId)
        {
            if (basketItems == null || !basketItems.Any())
                throw new Exception("Səbət boşdur.");

            var lineItems = new List<SessionLineItemOptions>();
            var metadata = new Dictionary<string, string>
    {
        { "userId", userId }
    };

            
            int index = 0;

            foreach (var item in basketItems)
            {
             
                var product = await _shopRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Məhsul tapılmadı: {item.Name}");

                var amountInCents = (long)(product.Price * 100);
                if (amountInCents < 1)
                    amountInCents = 100;

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = amountInCents,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                            Description = product.Description
                        }
                    },
                    Quantity = item.Count
                });

                metadata.Add($"productId_{index}", product.Id.ToString());
                metadata.Add($"quantity_{index}", item.Count.ToString());
                index++;
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7284/Sale/Success?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7284/Sale/Cancel",
                Metadata = metadata
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

    }

}
