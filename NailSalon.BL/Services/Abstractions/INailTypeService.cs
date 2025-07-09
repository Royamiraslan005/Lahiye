using Microsoft.AspNetCore.Http;
using NailSalon.Core.Models;

namespace NailSalon.BL.Services.Abstractions;

public interface INailTypeService
{
    Task<List<NailType>> GetAllAsync();
    Task<NailType> GetByIdAsync(int id);
    Task<List<NailType>> GetByZodiacAsync(string zodiac);


    Task CreateAsync(NailType model, IFormFile formFile, string wwwRootPath);
    Task UpdateAsync(NailType model, IFormFile formFile, string wwwRootPath);
    Task DeleteAsync(int id, string wwwRootPath);
}
