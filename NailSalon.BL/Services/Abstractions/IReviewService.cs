using NailSalon.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface IReviewService
    {
        Task<List<ReviewVm>> GetAllAsync();
        Task<ReviewVm> GetByIdAsync(int id);
        Task CreateAsync(ReviewVm vm,string email);
        Task DeleteAsync(int id);

    }

}
