using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IBasketService
    {
        Task AddToBasketAsync(string userId, int productId, int quantity = 1);
        Task<List<BasketItemVm>> GetBasketAsync(string userId);
        Task RemoveFromBasketAsync(int itemId);
        Task ClearBasketAsync(string userId);
    }

}
