using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Abstracts
{
    public interface ILikeRepository
    {
        Task<bool> IsLikedAsync(string userId, int productId);
        Task AddLikeAsync(string userId, int productId);
        Task RemoveLikeAsync(string userId, int productId);
        Task<int> GetLikeCountAsync(int productId);
        Task<List<int>> GetLikedProductIdsByUserAsync(string userId);

    }

}
