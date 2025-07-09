using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IBasketRepository
    {
        Task<List<BasketItem>> GetItemsAsync(string userId);
        Task AddItemAsync(BasketItem item);
        Task RemoveItemAsync(int itemId);
        Task ClearBasketAsync(string userId);
    }


}
