using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IMenuService
    {
        Task<List<MenuItemVm>> GetAllAsync();
        Task<MenuItemVm> GetByIdAsync(int id);
     
        Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> ids);

        Task CreateAsync(MenuItemVm vm, string wwwRootPath);
        Task UpdateAsync(MenuItemVm vm, string wwwRootPath);
        Task DeleteAsync(int id, string wwwRootPath);
    }
}
