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
    public class ShopRepository : IShopRepository
    {
        private readonly AppDbContext _context;

        public ShopRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShopProduct>> GetAllAsync()
        {
            return await _context.ShopProducts.ToListAsync();
        }

        public async Task<ShopProduct> GetByIdAsync(int id)
        {
            return await _context.ShopProducts.FindAsync(id);
        }

        public async Task CreateAsync(ShopProduct product)
        {
            await _context.ShopProducts.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShopProduct product)
        {
            _context.ShopProducts.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.ShopProducts.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
