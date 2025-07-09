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
    public class NailTypeRepository : INailTypeRepository
    {
        private readonly AppDbContext _context;

        public NailTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NailType>> GetAllAsync() => await _context.NailTypes.ToListAsync();

        public async Task<NailType> GetByIdAsync(int id) => await _context.NailTypes.FindAsync(id);

        public void Add(NailType nailType) => _context.NailTypes.Add(nailType);

        public void Update(NailType nailType) => _context.NailTypes.Update(nailType);

        public void Delete(NailType nailType) => _context.NailTypes.Remove(nailType);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<List<NailType>> GetByZodiacAsync(string zodiac)
        {
            return await _context.NailTypes
                .Where(x => x.Zodiac == zodiac || x.Zodiac == "All")
                .ToListAsync();
        }


    }

}
