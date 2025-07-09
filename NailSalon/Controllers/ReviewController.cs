using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;
using NailSalon.Core.ViewModels;

namespace NailSalon.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _service.GetAllAsync();
            return View(reviews);
        }

        
    }

}
