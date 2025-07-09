using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NailTypeController : Controller
    {
        private readonly INailTypeService _service;
        private readonly IWebHostEnvironment _environment;

        public NailTypeController(INailTypeService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _service.GetAllAsync();
            var viewModels = models.Select(x => new NailTypeVm
            {
                Id = x.Id,
                Title = x.Title,
                Price = (decimal)x.Price,
                ImageUrl = x.ImageUrl
            }).ToList();

            return View(viewModels); 
        }

       
        public IActionResult Create() => View();

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NailTypeVm nailtypevm)
        {
            if (!ModelState.IsValid) return View(nailtypevm);

            var model = new NailType
            {
                Title = nailtypevm.Title,
                Price = nailtypevm.Price,
                ImageUrl = nailtypevm.ImageUrl
            };

            await _service.CreateAsync(model, nailtypevm.FormFile, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            var nailtypevm = new NailTypeVm
            {
                Id = model.Id,
                Title = model.Title,
                Price = (decimal)model.Price,
                ImageUrl = model.ImageUrl
            };

            return View(nailtypevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(NailTypeVm nailtypevm)
        {
            if (!ModelState.IsValid) return View(nailtypevm);

            var existingModel = await _service.GetByIdAsync(nailtypevm.Id);
            if (existingModel == null) return NotFound();

            // Yeni şəkil yüklənibsə, köhnəsini sil
            if (nailtypevm.FormFile != null)
            {
                RemoveImage(existingModel.ImageUrl);
            }

            var model = new NailType
            {
                Id = nailtypevm.Id,
                Title = nailtypevm.Title,
                Price = nailtypevm.Price,
                ImageUrl = existingModel.ImageUrl // ilkin olaraq köhnəni saxla, service yenisini yazacaq
            };

            await _service.UpdateAsync(model, nailtypevm.FormFile, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null) return NotFound();

            RemoveImage(model.ImageUrl); // Silinmədən əvvəl şəkli sil

            await _service.DeleteAsync(id, _environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }
        private void RemoveImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var fullPath = Path.Combine(_environment.WebRootPath, imageUrl.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
        }

    }
}
