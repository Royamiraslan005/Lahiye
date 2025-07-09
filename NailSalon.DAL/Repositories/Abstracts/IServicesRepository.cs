using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface IServicesRepository
    {
        Task<List<Services>> GetAllAsync();
        Task<Services> GetByIdAsync(int id);
        void Add(Services service);
        void Update(Services service);
        void Delete(Services service);
        Task SaveAsync();
    }

}
