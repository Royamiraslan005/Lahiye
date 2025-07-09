using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Concretes
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _context;

        public LikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsLikedAsync(string userId, int productId)
        {
            return await _context.Likes.AnyAsync(l => l.UserId == userId && l.ProductId == productId);
        }

        public async Task AddLikeAsync(string userId, int productId)
        {
            if (!await IsLikedAsync(userId, productId))
            {
                _context.Likes.Add(new Like { UserId = userId, ProductId = productId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveLikeAsync(string userId, int productId)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.ProductId == productId);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetLikeCountAsync(int productId)
        {
            return await _context.Likes.CountAsync(l => l.ProductId == productId);
        }

        public async Task<List<int>> GetLikedProductIdsByUserAsync(string userId)
        {
            return await _context.Likes
                .Where(x => x.UserId == userId)
                .Select(x => x.ProductId)
                .ToListAsync();
        }

    }

}
