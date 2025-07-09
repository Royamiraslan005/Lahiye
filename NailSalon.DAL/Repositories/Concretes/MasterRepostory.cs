using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstractions;


namespace NailSalon.DAL.Repositories.Concretes;

public class MasterRepository : IMasterRepository
{

    private readonly AppDbContext _context;

    public MasterRepository(AppDbContext context) 
    {
        _context = context;
    }

    public async Task<List<Master>> GetAllAsync()
    {
        return  await _context.Masters.ToListAsync();

    }
    public async Task<string> Create(Master master)
    {
      await  _context.Masters.AddAsync(master);
        return "elave edildi";
    }
    public void Update(Master master)
    {
         _context.Masters.Update(master);

    }
    public void SaveAllChange()
    {
        _context.SaveChanges();
    }

    public async Task<Master> GetByIdAsync(int id)
    {
        var master = _context.Masters.Find(id);
        return master;
        
    }

    public async Task DeleteAsync(int id)
    {
        var master = await _context.Masters.FindAsync(id);
        if (master != null)
        {
            _context.Masters.Remove(master);
        }
    }
    public async Task UpdateAsync(Master master)
    {
        _context.Masters.Update(master);
        await _context.SaveChangesAsync();
    }


}

