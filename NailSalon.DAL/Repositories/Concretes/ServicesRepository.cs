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
    public class ServicesRepository : IServicesRepository
    {
        private readonly AppDbContext _context;

        public ServicesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Services>> GetAllAsync() => await _context.Services.ToListAsync();

        public async Task<Services> GetByIdAsync(int id) => await _context.Services.FindAsync(id);

        public void Add(Services service) => _context.Services.Add(service);

        public void Update(Services service) => _context.Services.Update(service);

        public void Delete(Services service) => _context.Services.Remove(service);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }

}
