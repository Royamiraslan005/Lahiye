using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IShopService
    {
        Task<ShopProductVm> GetByIdAsync(int id);
        Task<List<ShopProductVm>> GetAllAsync(string userId);

        Task CreateAsync(ShopProductVm productVm, string wwwroot);
        Task UpdateAsync(ShopProductVm productVm);
        Task DeleteAsync(int id);
    }
}
