using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IMenuRepository
    {
        Task<List<MenuItem>> GetAllAsync();
        Task<MenuItem> GetByIdAsync(int id);
        List<MenuItem> GetByIdsAsync(List<int> ids);
        Task AddAsync(MenuItem item);
        Task UpdateAsync(MenuItem item);
        void SaveAllChange();
        Task DeleteAsync(int id);
    }

}
