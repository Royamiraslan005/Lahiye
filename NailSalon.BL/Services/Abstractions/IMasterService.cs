using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IMasterService
    {
        Task<List<MasterVm>> GetAllAsync();
        Task<MasterVm> GetByIdAsync(int id);
        Task CreateAsync(MasterVm masterVm, string wwwroot);
        Task UpdateAsync(MasterVm masterVm, string wwwroot);
        Task DeleteAsync(int id);

    }
}

