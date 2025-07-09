using NailSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface ISaleRepository
    {
        Task CreateAsync(Sale sale);
        Task<List<Sale>> GetByUserIdAsync(string userId);
    }
}
