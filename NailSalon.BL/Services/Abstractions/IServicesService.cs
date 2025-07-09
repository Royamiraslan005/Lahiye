using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IServicesService
    {
        Task<List<ServicesVm>> GetAllAsync();
        Task<ServicesVm> GetByIdAsync(int id);
        Task CreateAsync(ServicesVm vm, string wwwroot);
        Task UpdateAsync(ServicesVm vm, string wwwroot);
        Task DeleteAsync(int id);
    }

}
