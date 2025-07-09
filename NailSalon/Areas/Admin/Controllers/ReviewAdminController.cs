using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailSalon.BL.Services.Abstractions;

namespace NailSalon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReviewAdminController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewAdminController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }

        // GET: Show delete confirmation view
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Actually delete the review
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _reviewService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
