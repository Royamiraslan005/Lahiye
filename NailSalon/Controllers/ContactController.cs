using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.Models;
using NailSalon.Core.ViewModels;
using NailSalon.Core.ViewModels.NailSalon.Core.ViewModels;
using NailSalon.DAL.Contexts;

namespace NailSalon.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;
        private readonly AppDbContext _context;
        public ContactController(IContactService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var message = new ContactMessage
            {
               FullName=vm.FullName,
                Email = vm.Email,
                Message = vm.Message
            };

            _context.ContactMessages.Add(message);
            _context.SaveChanges();

            await _service.CreateAsync(vm);
            TempData["Success"] = "Mesajınız göndərildi 💌";
            return RedirectToAction("Thanks");
        }
        public IActionResult Thanks()
        {
            return View();
        }

    }

}
