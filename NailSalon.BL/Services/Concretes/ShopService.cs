using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    using global::NailSalon.BL.Services.Abstractions;
    using global::NailSalon.Core.Models;
    using global::NailSalon.Core.ViewModels;
    using global::NailSalon.DAL.Repositories.Abstracts;
    using global::NailSalon.DAL.Repositories.Concretes;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace NailSalon.Business.Services.Implementations
    {
        public class ShopService : IShopService
        {
            private readonly IShopRepository _repo;
            private readonly ILikeService _likeService;

            public ShopService(IShopRepository repo, ILikeService likeService)
            {
                _repo = repo;
                _likeService = likeService;
            }

            public async Task<ShopProductVm> GetByIdAsync(int id)
            {
                var product = await _repo.GetByIdAsync(id);
                if (product == null) return null;

                return new ShopProductVm
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    ImageUrl = product.ImageUrl
                };
            }

            public async Task CreateAsync(ShopProductVm vm, string wwwroot)
            {
                string fileName = await SaveImageAsync(vm.FormFile, wwwroot);

                var product = new ShopProduct
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price,
                    Category = vm.Category,
                    ImageUrl = fileName
                };

                await _repo.CreateAsync(product);
                await _repo.SaveAsync();
            }

            public async Task UpdateAsync(ShopProductVm vm)
            {
                var product = await _repo.GetByIdAsync(vm.Id);
                if (product == null) return;

                product.Name = vm.Name;
                product.Description = vm.Description;
                product.Price = vm.Price;
                product.Category = vm.Category;

                if (vm.FormFile != null)
                {
                    product.ImageUrl = await SaveImageAsync(vm.FormFile, Directory.GetCurrentDirectory() + "/wwwroot");
                }

                await _repo.UpdateAsync(product);
                await _repo.SaveAsync();
            }

            public async Task DeleteAsync(int id)
            {
                await _repo.DeleteAsync(id);
                await _repo.SaveAsync();
            }

            private async Task<string> SaveImageAsync(IFormFile file, string wwwroot)
            {
                var folder = Path.Combine(wwwroot, "images");
                Directory.CreateDirectory(folder);
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                return fileName;
            }

            public async Task<List<ShopProductVm>> GetAllAsync(string userId)
            {
                var products = await _repo.GetAllAsync();
                var productVms = new List<ShopProductVm>();

                foreach (var p in products)
                {
                    var likeCount = await _likeService.GetLikeCountAsync(p.Id);
                    var isLiked = await _likeService.IsLikedAsync(userId, p.Id);

                    productVms.Add(new ShopProductVm
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Category = p.Category,
                        ImageUrl = p.ImageUrl,
                        LikeCount = likeCount,
                        IsLikedByUser = isLiked
                    });
                }

                return productVms;
            }

        }
    }
}
