using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class ServicesController : Controller
    {
        IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _servicesService.GetAllAsync();
            var servicesVm = services.Select(x => new ServicesVm
            {
                Id = x.Id,
                Title = x.Title,
                ImageUrl = x.ImageUrl
            }).ToList();
            return View(servicesVm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var service = await _servicesService.GetByIdAsync(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

    }
}
