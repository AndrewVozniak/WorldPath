using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reviews.Dtos;
using Reviews.Models;
using Reviews.Services;

namespace Reviews.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MongoReviewService _mongoReviewService;
        public ReviewController(IMapper mapper, MongoReviewService mongoReviewService)
        {
            _mapper = mapper;
            _mongoReviewService = mongoReviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _mongoReviewService.GetAllReviews();
            return Ok(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewById(string id)
        {
            var review = await _mongoReviewService.GetReviewById(id);

            if (review is null) return NotFound();

            return Ok(review);
        }
        
        [HttpGet("{count:int}")]
        public async Task<IActionResult> GetSomeReviews(int count)
        {
            var reviews = await _mongoReviewService.GetSomeReview(count);

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
