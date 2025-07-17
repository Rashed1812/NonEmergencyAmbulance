using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.ApiResponse;
using Shared.DTOS.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]   
    public class NurseController : ControllerBase  
    {
        private readonly INurseService nurseService;
        private readonly IAuthenticationService _authService;

        public NurseController(INurseService nurseService, IAuthenticationService authService)
        {
            this.nurseService = nurseService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nurses = await nurseService.GetAllNursesAsync();
            return Ok(nurses);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nurse = await nurseService.GetNurseByIdAsync(id);
            if (nurse == null) return NotFound();
            return Ok(nurse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNurse(int id, NurseDto dto)
        {
            await nurseService.UpdateNurseAsync(id, dto);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Shared.DTOS.Registeration.NurseRegisterDTO dto)
        {
            var result = await _authService.NurseRegisterAsync(dto);
            return Ok(result);
        }

        [HttpGet("available")]
        public async Task<ActionResult> GetAvailableNurses() {
           return Ok(await nurseService.GetAvailableNursesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNurse(int id)
        {
            if (await nurseService.DeleteNurseAsync(id))
            {
                return NoContent();
            }
            return NotFound();

        }
        [HttpPatch("{id}/toggle-availability")]
        public async Task<ActionResult<ApiResponse>> ToggleAvailability(int id, [FromBody] ToggleAvailabilityDto dto)
        {
            try
            {
                await nurseService.ToggleAvailabilityAsync(id, dto.IsAvailable);

                return Ok(new ApiResponse
                {
                    IsSuccessful = true,
                    Data = "Availability updated successfully"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse
                {
                    IsSuccessful = false,
                    Data = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    IsSuccessful = false,
                    Data = "An unexpected error occurred"
                });
            }
        }



    }
}
