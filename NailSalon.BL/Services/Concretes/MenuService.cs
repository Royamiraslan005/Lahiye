using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Repositories.Abstracts;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repo;

    public MenuService(IMenuRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MenuItemVm>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();

        return items.Select(x => new MenuItemVm
        {
            Id = x.Id,
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            
            IsSelected = x.IsSelected
        }).ToList();
    }

    public async Task<MenuItemVm> GetByIdAsync(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) return null;

        return new MenuItemVm
        {
            Id = item.Id,
            Name = item.Name,
            ImageUrl = item.ImageUrl,
            IsSelected = item.IsSelected
        };
    }

    public async Task CreateAsync(MenuItemVm vm, string wwwRootPath)
    {
        string fileName = null;

        if (vm.FormFile != null)
        {
            string uploadsFolder = Path.Combine(wwwRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.FormFile.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await vm.FormFile.CopyToAsync(stream);
        }

        var item = new MenuItem
        {
            Name = vm.Name,
            ImageUrl = fileName,
            IsSelected = vm.IsSelected
        };

        await _repo.AddAsync(item);
         _repo.SaveAllChange();
    }

    public async Task UpdateAsync(MenuItemVm vm, string wwwRootPath)
    {
        var existing = await _repo.GetByIdAsync(vm.Id);
        if (existing == null) return;

        string fileName = existing.ImageUrl;

        if (vm.FormFile != null)
        {
            string uploadsFolder = Path.Combine(wwwRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Köhnə şəkili sil
            if (!string.IsNullOrEmpty(existing.ImageUrl))
            {
                string oldFilePath = Path.Combine(uploadsFolder, existing.ImageUrl);
                if (File.Exists(oldFilePath))
                    File.Delete(oldFilePath);
            }

            // Yeni şəkili yüklə
            fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.FormFile.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await vm.FormFile.CopyToAsync(stream);
        }

        existing.Name = vm.Name;
        existing.ImageUrl = fileName;
        existing.IsSelected = vm.IsSelected;

        await _repo.UpdateAsync(existing);
         _repo.SaveAllChange();
    }

    public async Task DeleteAsync(int id, string wwwRootPath)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) return;

        // Şəkili sil
        if (!string.IsNullOrEmpty(item.ImageUrl))
        {
            string uploadsFolder = Path.Combine(wwwRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, item.ImageUrl);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        await _repo.DeleteAsync(id);
         _repo.SaveAllChange();
    }

    // MenuService.cs
    public async Task<List<MenuItem>> GetMenuItemsByIdsAsync(List<int> ids)
    {
        return  _repo.GetByIdsAsync(ids);
    }

}
