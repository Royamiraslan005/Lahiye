using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class NailTypeController : Controller
    {
        INailTypeService _nailTypeService;

        public NailTypeController(INailTypeService nailTypeService)
        {
            _nailTypeService = nailTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var nailTypes = await _nailTypeService.GetAllAsync(); 

            var nailTypeVms = nailTypes.Select(x => new NailTypeVm
            {
                Id = x.Id,
                Title = x.Title,
                Price = (decimal)x.Price,
                ImageUrl = x.ImageUrl
            }).ToList();

            return View(nailTypeVms);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var nailType = await _nailTypeService.GetByIdAsync(id);
            if (nailType == null)
                return NotFound();

            var vm = new NailTypeVm
            {
                Id = nailType.Id,
                Title = nailType.Title,
                Price = (decimal)nailType.Price,
                ImageUrl = nailType.ImageUrl,
                Zodiac = nailType.Zodiac
            };

            return View(vm);
        }


    }
}
