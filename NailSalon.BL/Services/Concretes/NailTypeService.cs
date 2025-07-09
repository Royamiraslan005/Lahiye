using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.DAL.Repositories.Abstracts;
using NailSalon.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.BL.Services.Concretes
{
    public class NailTypeService : INailTypeService
    {
        private readonly INailTypeRepository _repository;

        public NailTypeService(INailTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NailType>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<NailType> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateAsync(NailType model, IFormFile formFile, string wwwRootPath)
        {
            model.ImageUrl = await SaveImage(formFile, wwwRootPath);
            _repository.Add(model);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(NailType model, IFormFile formFile, string wwwRootPath)
        {
            var nailType = await _repository.GetByIdAsync(model.Id);
            if (nailType == null) return;
            nailType.Title = model.Title;
            nailType.Price = model.Price;

            if (formFile != null)
                nailType.ImageUrl = await SaveImage(formFile, wwwRootPath);

            _repository.Update(nailType);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id, string wwwRootPath)
        {
            var nailType = await _repository.GetByIdAsync(id);
            if (nailType == null) return;

            // Optional: Delete old image file
            var imagePath = Path.Combine(wwwRootPath, "images", nailType.ImageUrl);
            if (File.Exists(imagePath)) File.Delete(imagePath);

            _repository.Delete(nailType);
            await _repository.SaveAsync();
        }

        private async Task<string> SaveImage(IFormFile file, string wwwRootPath)
        {
            var folder = Path.Combine(wwwRootPath, "images");
            Directory.CreateDirectory(folder);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }
        public async Task<List<NailType>> GetByZodiacAsync(string zodiac)
        {
            return await _repository.GetByZodiacAsync(zodiac);
        }
    }

}
