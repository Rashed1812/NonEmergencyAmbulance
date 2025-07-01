using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.Registeration;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationService _authService) : ControllerBase
    {
        //Register End Point for driver
        [HttpPost("register/driver")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterDriver([FromBody] DriverRegisterDTO driverDto)
        {
            var userDto = await _authService.DriverRegisterAsync(driverDto);
            return Ok(userDto);
        }
        //Register End Point for nurse
        [HttpPost("register/nurse")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterNurse([FromBody] NurseRegisterDTO nurseDto)
        {
            var userDto = await _authService.NurseRegisterAsync(nurseDto);
            return Ok(userDto);
        }
        // Register End Point for patient
        [HttpPost("register/patient")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDTO patientDto)
        {
            var userDto = await _authService.PatientRegisterAsync(patientDto);
            return Ok(userDto);
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var userDto = await _authService.LoginAsync(loginDto);
            return Ok(userDto);
        }
        [HttpGet("MyProfile")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var userDto = await _authService.GetCurrentUserAsync(email);
            return Ok(userDto);
        }
    }
}
