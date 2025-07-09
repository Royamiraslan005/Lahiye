using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;

namespace NailSalon.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            var vm = new GlobalSearchVm
            {
                SearchText = searchText,
                Masters = await _context.Masters
                    .Where(m => m.FullName.Contains(searchText))
                    .Select(m => new MasterVm { FullName = m.FullName,Experience=m.Experience,Specialty=m.Specialty,Zodiac=m.Zodiac, ImageUrl = m.ImageUrl })
                    .ToListAsync(),

                NailTypes = await _context.NailTypes
                    .Where(d => d.Title.Contains(searchText))
                    .Select(d => new NailTypeVm { Title = d.Title, ImageUrl = d.ImageUrl })
                    .ToListAsync()
            };

            return View(vm);
        }
    }

}
