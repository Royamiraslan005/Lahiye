using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class CustomerController : Controller
    {
        IReservationService _reservationService;
        UserManager<AppUser> _userManager;
        IMasterService _masterService;
        INailTypeService _nailTypeService;

        public CustomerController(IReservationService reservationService, UserManager<AppUser> userManager, IMasterService masterService, INailTypeService nailTypeService)
        {
            _reservationService = reservationService;
            _userManager = userManager;
            _masterService = masterService;
            _nailTypeService = nailTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendReservation()
        {
            var masters =await _masterService.GetAllAsync();
            var designs = await _nailTypeService.GetAllAsync();
            ViewBag.Types = designs;
            ViewBag.MasterVMs =masters;
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SendReservation(ReservationVm reservationVm)
        {
            await _reservationService.MakeReservationAsync(reservationVm);
            return View("Index", "Home");

        }
    }
}
