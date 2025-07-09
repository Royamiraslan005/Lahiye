using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repo;

        public ReviewService(IReviewRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ReviewVm>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task CreateAsync(ReviewVm vm,string email)
        {
            var review = new Review()
            {
                Email = email,
                Comment = vm.Comment,

            };

            await _repo.CreateAsync(review);
        }
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<ReviewVm> GetByIdAsync(int id)
        {
            var review = await _repo.GetByIdAsync(id);

            if (review == null)
                return null;

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
