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
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;

        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetAllAsync() => await _context.MenuItems.ToListAsync();

        public async Task<MenuItem> GetByIdAsync(int id) => await _context.MenuItems.FindAsync(id);

        public async Task AddAsync(MenuItem item)
        {
            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem item)
        {
            _context.MenuItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item != null)
            {
                _context.MenuItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public void SaveAllChange()
        {
            _context.SaveChanges();
        }
        // MenuRepository.cs
        public  List<MenuItem> GetByIdsAsync(List<int> ids)
        {
            return  _context.MenuItems.Where(m => ids.Contains(m.Id)).ToList();
        }

    }

}
