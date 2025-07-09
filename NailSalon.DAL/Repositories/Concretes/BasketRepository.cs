using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.DAL.Contexts;
using NailSalon.DAL.Repositories.Abstracts;

public class BasketRepository : IBasketRepository
{
    private readonly AppDbContext _context;

    public BasketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BasketItem>> GetItemsAsync(string userId)
    {
        return await _context.BasketItems
            .Where(x => x.AppUserId == userId)
            .ToListAsync();
    }

    public async Task AddItemAsync(BasketItem item)
    {
        var existing = await _context.BasketItems
            .FirstOrDefaultAsync(x => x.AppUserId == item.AppUserId && x.ProductId == item.ProductId);

        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            _context.BasketItems.Add(item);
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(int itemId)
    {
        var item = await _context.BasketItems.Where(x=>x.ProductId == itemId).FirstOrDefaultAsync();
        if (item != null)
        {
            _context.BasketItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }



    public async Task ClearBasketAsync(string userId)
    {
        var items = _context.BasketItems.Where(x => x.AppUserId == userId);
        _context.BasketItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
