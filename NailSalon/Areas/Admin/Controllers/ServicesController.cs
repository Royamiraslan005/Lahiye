using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServicesController : Controller
    {
        private readonly IServicesService _serviceService;
        private readonly IWebHostEnvironment _environment;

        public ServicesController(IServicesService serviceService, IWebHostEnvironment environment)
        {
            _serviceService = serviceService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicesVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _serviceService.CreateAsync(vm, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound();

            var vm = new ServicesVm
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                ImageUrl = service.ImageUrl
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServicesVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _serviceService.UpdateAsync(vm, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _serviceService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
