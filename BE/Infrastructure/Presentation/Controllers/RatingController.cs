using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.TripDTOs;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingDTO dto)
        {
            await _ratingService.AddRatingAsync(dto);
            return Ok();
        }

        [HttpGet("driver/{driverId}")]
        public async Task<IActionResult> GetRatingsForDriver(int driverId)
        {
            var ratings = await _ratingService.GetRatingsForDriverAsync(driverId);
            return Ok(ratings);
        }

        [HttpGet("nurse/{nurseId}")]
        public async Task<IActionResult> GetRatingsForNurse(int nurseId)
        {
            var ratings = await _ratingService.GetRatingsForNurseAsync(nurseId);
            return Ok(ratings);
        }

        [HttpGet("driver/{driverId}/average")]
        public async Task<IActionResult> GetAverageRatingForDriver(int driverId)
        {
            var avg = await _ratingService.GetAverageRatingForDriverAsync(driverId);
            return Ok(avg);
        }

        [HttpGet("nurse/{nurseId}/average")]
        public async Task<IActionResult> GetAverageRatingForNurse(int nurseId)
        {
            var avg = await _ratingService.GetAverageRatingForNurseAsync(nurseId);
            return Ok(avg);
        }
    }
} 