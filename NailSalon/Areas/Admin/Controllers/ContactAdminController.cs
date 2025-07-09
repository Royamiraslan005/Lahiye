using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.DAL.Contexts;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ContactAdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var messages = _context.ContactMessages.OrderByDescending(x => x.CreatedAt).ToList();
            return View(messages);
        }
        [HttpGet]
        public IActionResult Reply(int id)
        {
            var message = _context.ContactMessages.FirstOrDefault(x => x.Id == id);
            if (message == null) return NotFound();
            return View(message);
        }
        [HttpPost]
        public IActionResult Reply(int id, string reply)
        {
            var message = _context.ContactMessages.FirstOrDefault(x => x.Id == id);
            if (message == null) return NotFound();

            message.Reply = reply;
            message.IsReplied = true;
            _context.SaveChanges();

            TempData["Success"] = "Cavab uğurla göndərildi!";
            return RedirectToAction("Index");
        }


    }

}
