using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class MasterController : Controller
    {
        IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        public async Task<IActionResult> Index()
        {
            var masters = await _masterService.GetAllAsync();
            var masterVm=masters.Select(x => new MasterVm
            {
                Id = x.Id,
                FullName=x.FullName,
                Experience=x.Experience,
                Zodiac=x.Zodiac,
                Specialty=x.Specialty,
                ImageUrl = x.ImageUrl
            }).ToList();
            return View(masterVm);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var master = await _masterService.GetByIdAsync(id);
            if (master == null) return NotFound();

            var masterVm = new MasterVm
            {
                Id = master.Id,
                FullName = master.FullName,
                Experience = master.Experience,
                Zodiac = master.Zodiac,
                Specialty = master.Specialty,
                ImageUrl = master.ImageUrl
            };

            return View(masterVm);
        }

    }
}
