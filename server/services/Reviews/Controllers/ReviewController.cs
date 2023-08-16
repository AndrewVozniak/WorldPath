using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reviews.Data;
using Reviews.Models;

namespace Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewDbContext _context;
        public ReviewController(ReviewDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review is null) return NotFound();

            return Ok(review);
        }
        
        [HttpGet("{count:int}")]
        public async Task<IActionResult> GetSomeReviews(int count)
        {
            var reviews = await _context.Reviews
                .Take(count)
                .ToListAsync();

            if (reviews.Count == 0) return NotFound();

            return Ok(reviews);
        }
        
        [HttpGet("{stars:int}")]
        public async Task<IActionResult> GetReviewsByStars(int stars)
        {
            var reviews = await _context.Reviews
                .Where(r => r.Stars == stars)
                .ToListAsync();

            if (reviews.Count == 0) return NotFound();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            return Created($"/GetReviewById/{review.Id}", review);
        }
    }
}
