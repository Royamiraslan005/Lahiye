using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.DAL.Repositories.Concretes
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReviewVm>> GetAllAsync()
        {
            return await _context.Reviews
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new ReviewVm
                {
                    Id = r.Id,
                    Email = r.Email,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }

        public async Task CreateAsync(Review vm)
        {
            
            _context.Reviews.Add(vm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReviewVm> GetByIdAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
                return null; // 👈 Əgər tapılmasa, null qaytar

            return new ReviewVm
            {
                Id = review.Id,
                Email = review.Email,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };
        }

    }

}
