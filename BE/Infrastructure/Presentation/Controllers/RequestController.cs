using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.RequestDTOS;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController(IRequestService _requestService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _requestService.GetAllRequestsWithRelatedData();
            return Ok(result);
        }
        [HttpGet("available-for-driver")]
        public async Task<IActionResult> GetPendingRequestsForDrivers()
        {
            var result = await _requestService.GetAvailableRequestsForDriverAsync();
            return Ok(result);
        }

        [HttpGet("available-for-nurse")]
        public async Task<IActionResult> GetPendingRequestsForNurses()
        {
            var result = await _requestService.GetAvailableRequestsForNurseAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromQuery] string userId, [FromBody] CreateRequestDTO dto)
        {
            var result = await _requestService.AddNewRequest(userId, dto);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = new RequestDTO { RequestId = id };
            var result = await _requestService.GetRequestById(dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] UpdateRequestDTO dto)
        {
            if (id != dto.RequestId)
                return BadRequest("Id mismatch");

            var result = await _requestService.UpdateRequest(dto);
            return Ok(result);
        }

        [HttpPut("assign-driver")]
        public async Task<IActionResult> AssignDriverToRequest([FromBody] AssignDriverDTO dto)
        {
            var result = await _requestService.AssignDriverAsync(dto);
            if (result == null) return Conflict("Driver Is Assigned Already");
            return Ok(result);
        }

        [HttpPut("assign-nurse")]
        public async Task<IActionResult> AssignNurseToRequest([FromBody] AssignNurseDTO dto)
        {
            var result = await _requestService.AssignNurseAsync(dto);
            if (result == null) return Conflict("Nurse Is Assigned Already");
            return Ok(result);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateRequestStatusDTO dto)
        {
            var result = await _requestService.UpdateStatusAsync(dto);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
