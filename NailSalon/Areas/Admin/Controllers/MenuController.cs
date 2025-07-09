using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class MenuController : Controller
    {
        private readonly IMenuService _service;
        private readonly IWebHostEnvironment _env;

        public MenuController(IMenuService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemVm vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.FormFile != null)
                {
                    // Unikal ad yarat
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.FormFile.FileName);
                    string uploadPath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await vm.FormFile.CopyToAsync(stream);
                    }

                    vm.ImageUrl = fileName; // Bazaya fayl adını yaz
                }

                await _service.CreateAsync(vm, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
        public async Task<IActionResult> Update(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MenuItemVm vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.FormFile != null)
                {
                    // Köhnə şəkli sil
                    var existingItem = await _service.GetByIdAsync(vm.Id);
                    if (!string.IsNullOrEmpty(existingItem.ImageUrl))
                    {
                        string oldFilePath = Path.Combine(_env.WebRootPath, "images", existingItem.ImageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Yeni şəkli yüklə
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.FormFile.FileName);
                    string uploadPath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await vm.FormFile.CopyToAsync(stream);
                    }

                    vm.ImageUrl = fileName;
                }

                await _service.UpdateAsync(vm, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id, _env.WebRootPath);
            return RedirectToAction(nameof(Index));
        }
    }
}
