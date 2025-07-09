using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    using global::NailSalon.Core.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IShopRepository _shopRepo;

        public BasketService(IBasketRepository basketRepo, IShopRepository shopRepo)
        {
            _basketRepo = basketRepo;
            _shopRepo = shopRepo;
        }

        public async Task AddToBasketAsync(string userId, int productId, int quantity = 1)
        {
            var product = await _shopRepo.GetByIdAsync(productId);
            if (product == null) return;

            var newItem = new BasketItem
            {
                AppUserId = userId,
                ProductId = productId,
                Quantity = quantity,
                Name = product.Name,       
                ImageUrl = product.ImageUrl 
            };

            await _basketRepo.AddItemAsync(newItem);
        }


        public async Task<List<BasketItemVm>> GetBasketAsync(string userId)
        {
            var items = await _basketRepo.GetItemsAsync(userId);

            var basketVm = new List<BasketItemVm>();

            foreach (var item in items)
            {
                var product = await _shopRepo.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    basketVm.Add(new BasketItemVm
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Count = item.Quantity
                    });
                }
            }

            return basketVm;
        }

        public async Task RemoveFromBasketAsync(int itemId)
        {
            await _basketRepo.RemoveItemAsync(itemId);
        }


        public async Task ClearBasketAsync(string userId)
        {
            await _basketRepo.ClearBasketAsync(userId);
        }
    }


}
