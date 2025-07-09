using NailSalon.BL.Services.Abstractions;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepo;

        public LikeService(ILikeRepository likeRepo)
        {
            _likeRepo = likeRepo;
        }

        public async Task ToggleLikeAsync(string userId, int productId)
        {
            if (await _likeRepo.IsLikedAsync(userId, productId))
                await _likeRepo.RemoveLikeAsync(userId, productId);
            else
                await _likeRepo.AddLikeAsync(userId, productId);
        }

        public Task<bool> IsLikedAsync(string userId, int productId)
            => _likeRepo.IsLikedAsync(userId, productId);

        public Task<int> GetLikeCountAsync(int productId)
            => _likeRepo.GetLikeCountAsync(productId);

        public Task<List<int>> GetLikedProductIdsByUserAsync(string userId)
        {
            return _likeRepo.GetLikedProductIdsByUserAsync(userId);
        }

    }

}
