using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reviews.Data;
using Reviews.Dtos;
using Reviews.Models;
using Reviews.Services;

namespace Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewDbContext _context;
        private readonly IMapper _mapper;
        private readonly MongoReviewService _mongoReviewService;
        public ReviewController(ReviewDbContext context, IMapper mapper, MongoReviewService mongoReviewService)
        {
            _context = context;
            _mapper = mapper;
            _mongoReviewService = mongoReviewService;
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
        
        
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            var reviewFromDb = await _mongoReviewService.GetReviewByUserId(reviewDto.UserId);

            if (reviewFromDb != null) return Ok(reviewFromDb);

            var place = _mapper.Map<Review>(reviewDto);

            await _mongoReviewService.AddOneReviewAsync(place);

            return Ok(place);
        }
    }
}
