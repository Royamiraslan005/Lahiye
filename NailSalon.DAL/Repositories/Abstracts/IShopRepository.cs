using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IShopRepository
    {
        Task<List<ShopProduct>> GetAllAsync();
        Task<ShopProduct> GetByIdAsync(int id);
        Task CreateAsync(ShopProduct product);
        Task UpdateAsync(ShopProduct product);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
