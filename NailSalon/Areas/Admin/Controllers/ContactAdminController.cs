using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.Core.Helpers;
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

            // ✅ Email göndər
            string subject = "Zodiac Nail Salon - Mesajınıza Cavab";
            string body = $@"
        <p>Hörmətli {message.FullName},</p>
        <p>Sizin bizə göndərdiyiniz mesaja aşağıdakı cavabı verdik:</p>
        <blockquote style='border-left:4px solid #ccc; padding-left:10px; color:#444;'>{reply}</blockquote>
        <p>Əlavə sualınız olsa, bizimlə əlaqə saxlamaqdan çəkinməyin. 💅</p>
        <p>Sevgi ilə, <br> <strong>NailSalon</strong></p>";

            var result = EmailService.SendEmail(message.Email, subject, body);

            TempData["Success"] = "Cavab uğurla göndərildi və email ünvanına yollandı!";
            return RedirectToAction("Index");
        }


    }

}
