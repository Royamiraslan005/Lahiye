using Microsoft.AspNetCore.Http;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;
using NailSalon.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepository _repo;

        public ServicesService(IServicesRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ServicesVm>> GetAllAsync()
        {
           var services= await _repo.GetAllAsync();
            return services.Select(m => new ServicesVm
            {
                Id=m.Id,
                Title = m.Title,
                Description=m.Description,
                ImageUrl=m.ImageUrl
            }).ToList();
        }

        public async Task<ServicesVm> GetByIdAsync(int id)
        {
            var service = await _repo.GetByIdAsync(id);

            if (service == null)
                return null; 

            return new ServicesVm
            {
                Id = service.Id,
                Title = service.Title,
                ImageUrl = service.ImageUrl,
                Description = service.Description
            };
        }


        public async Task CreateAsync(ServicesVm vm, string wwwroot)
        {
            string fileName = await SaveImage(vm.FormFile, wwwroot);
            var service = new Core.Models.Services
            {
                Title = vm.Title,
                Description = vm.Description,
                ImageUrl = fileName
            };
            _repo.Add(service);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(ServicesVm vm, string wwwroot)
        {
            var service = await _repo.GetByIdAsync((int)vm.Id);
            if (service == null) return;

            service.Title = vm.Title;
            service.Description = vm.Description;

            if (vm.FormFile != null)
            {
                string fileName = await SaveImage(vm.FormFile, wwwroot);
                service.ImageUrl = fileName;
            }

            _repo.Update(service);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repo.GetByIdAsync(id);
            if (service != null)
            {
                _repo.Delete(service);
                await _repo.SaveAsync();
            }
        }

        private async Task<string> SaveImage(IFormFile file, string wwwroot)
        {
            string folder = Path.Combine(wwwroot, "images");
            Directory.CreateDirectory(folder);
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string path = Path.Combine(folder, fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
    }

}
