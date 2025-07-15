using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController (ITripService _tripService) : ControllerBase
    {
        // GET: api/trip/driver/5
        [HttpGet("driver/{driverId}")]
        public async Task<IActionResult> GetTripsForDriver(int driverId)
        {
            var trips = await _tripService.GetTripsForDriverAsync(driverId);
            return Ok(trips);
        }

        // GET: api/trip/nurse/3
        [HttpGet("nurse/{nurseId}")]
        public async Task<IActionResult> GetTripsForNurse(int nurseId)
        {
            var trips = await _tripService.GetTripsForNurseAsync(nurseId);
            return Ok(trips);
        }

        // GET: api/trip/request/12
        [HttpGet("request/{requestId}")]
        public async Task<IActionResult> GetTripByRequestId(int requestId)
        {
            var trip = await _tripService.GetTripByRequestIdAsync(requestId);
            if (trip == null)
                return NotFound();
            return Ok(trip);
        }

        // POST: api/trip/create/12
        [HttpPost("create/{requestId}")]
        public async Task<IActionResult> CreateTrip(int requestId)
        {
            try
            {
                var trip = await _tripService.CreateTripFromRequestAsync(requestId);
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/trip/8/start
        [HttpPut("{tripId}/start")]
        public async Task<IActionResult> StartTrip(int tripId)
        {
            var result = await _tripService.ConfirmTripStartAsync(tripId);
            if (!result)
                return BadRequest(new { message = "Could not start trip" });
            return Ok(new { message = "Trip started successfully" });
        }

        // PUT: api/trip/8/complete
        [HttpPut("{tripId}/complete")]
        public async Task<IActionResult> CompleteTrip(int tripId)
        {
            var result = await _tripService.CompleteTripAsync(tripId);
            if (!result)
                return BadRequest(new { message = "Could not complete trip" });
            return Ok(new { message = "Trip completed successfully" });
        }
    }
}
