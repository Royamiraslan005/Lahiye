using Microsoft.AspNetCore.Mvc;
using NailSalon.DAL.Contexts;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.BL.Services.Abstractions;
using NailSalon.BL.Services.Concretes;
using Microsoft.AspNetCore.Authorization;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MasterController : Controller
    {
        private readonly IMasterService _masterService;
        private readonly IWebHostEnvironment _environment;

        public MasterController(IMasterService masterService, IWebHostEnvironment environment)
        {
            _masterService = masterService;
            _environment = environment;
        }


        public async Task<IActionResult> Index()
        {
            var masters = await _masterService.GetAllAsync();
            return View(masters);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MasterVm masterVm)
        {

          await  _masterService.CreateAsync(masterVm,_environment.WebRootPath);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var master = await _masterService.GetByIdAsync(id);
            if (master == null)
                return NotFound();

            return View(master);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MasterVm masterVm)
        {
            if (!ModelState.IsValid)
                return View(masterVm);

            string wwwroot = _environment.WebRootPath;
            await _masterService.UpdateAsync(masterVm, wwwroot);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _masterService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}

