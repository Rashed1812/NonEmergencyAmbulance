using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;
        private readonly IAuthenticationService _authService;

        public DriverController(IDriverService driverService, IAuthenticationService authService)
        {
            this.driverService = driverService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var drivers = await driverService.GetAllDriversAsync();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await driverService.GetDriverByIdAsync(id);
            if (driver == null) return NotFound();
            return Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Shared.DTOS.Registeration.DriverRegisterDTO dto)
        {
            var result = await _authService.DriverRegisterAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Shared.DTOS.Driver.DriverDTO dto)
        {
            var updated = await driverService.UpdateDriverAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await driverService.DeleteDriverAsync(id);
                if (!deleted)
                    return NotFound("Driver not found");

                return Ok(new { message = "Driver deleted successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the driver" });
            }
        }

        [HttpPatch("{id}/toggle-availability")]
        public async Task<IActionResult> ToggleAvailability(int id, [FromBody] bool isAvailable)
        {
            await driverService.ToggleAvailabilityAsync(id, isAvailable);
            return Ok();
        }
    }
}
