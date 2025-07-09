using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using System.Text.Json;

namespace NailSalon.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IReservationService _reservationService;

        public MenuController(IMenuService menuService, IReservationService reservationService)
        {
            _menuService = menuService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var menuItems = await _menuService.GetAllAsync();

            if (TempData["ReservationVm"] is string json)
            {
                TempData.Keep("ReservationVm");
                var vm = JsonSerializer.Deserialize<ReservationVm>(json);
                vm.MenuItems = menuItems;
                return View(vm); // indi düzgün model göndərilir!
            }

            return RedirectToAction("Create", "Reservation");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ReservationVm vm)
        {
            if (vm.SelectedMenuIds == null || !vm.SelectedMenuIds.Any())
            {
                TempData["Message"] = "Zəhmət olmasa ən azı bir məhsul seçin.";
                TempData["ReservationVm"] = JsonSerializer.Serialize(vm);
                return RedirectToAction("Index");
            }

            var items =await _menuService.GetMenuItemsByIdsAsync(vm.SelectedMenuIds);
            
            var reservation = new Reservation
            {
                AppUserId = vm.UserId,
                MasterId = vm.MasterId,
                NailTypeId = vm.DesignId,
                Date = vm.Date,
                WantsFoodDrink = vm.WantsFoodDrink,
                
            };
        
            reservation.MenuItems = items;

            await _reservationService.CreateAsync(reservation); // VACİB!

            return RedirectToAction("Profile", "Account");
        }

    }
}
