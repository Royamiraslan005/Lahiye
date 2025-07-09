using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.DAL.Repositories.Abstractions
{
    public interface IMasterRepository 
    {
        Task<List<Master>> GetAllAsync();
        Task<Master> GetByIdAsync(int id);

        Task<string> Create(Master master);
         Task UpdateAsync(Master master);
        void SaveAllChange();
        Task DeleteAsync(int id);
    }
}
