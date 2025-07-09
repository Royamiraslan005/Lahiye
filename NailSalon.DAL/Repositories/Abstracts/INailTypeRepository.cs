using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface INailTypeRepository
    {
        Task<List<NailType>> GetAllAsync();
        Task<NailType> GetByIdAsync(int id);
        Task<List<NailType>> GetByZodiacAsync(string zodiac);

        void Add(NailType nailType);
        void Update(NailType nailType);
        void Delete(NailType nailType);
        Task SaveAsync();
    }

}
