using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Abstractions
{
    public interface ILikeService
    {
        Task ToggleLikeAsync(string userId, int productId);
        Task<bool> IsLikedAsync(string userId, int productId);
        Task<int> GetLikeCountAsync(int productId);
        Task<List<int>> GetLikedProductIdsByUserAsync(string userId);

    }
}
