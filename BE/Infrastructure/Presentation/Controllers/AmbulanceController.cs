using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS;
using Shared.DTOS.AmbulanceDTOS;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmbulanceController : ControllerBase
    {
        private readonly IAmbulanceService _ambulanceService;
        public AmbulanceController(IAmbulanceService ambulanceService)
        {
            _ambulanceService = ambulanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ambulances = await _ambulanceService.GetAllAmbulancesAsync();
            return Ok(ambulances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ambulance = await _ambulanceService.GetAmbulanceByIdAsync(id);
            if (ambulance == null) return NotFound();
            return Ok(ambulance);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AmbulanceDTO dto)
        {
            var created = await _ambulanceService.CreateAmbulanceAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AmbulanceDTO dto)
        {
            var updated = await _ambulanceService.UpdateAmbulanceAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ambulanceService.DeleteAmbulanceAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpPost("assign-driver")]
        public async Task<IActionResult> AssignDriver([FromQuery] int ambulanceId, [FromQuery] int driverId)
        {
            await _ambulanceService.AssignDriverAsync(ambulanceId, driverId);
            return Ok();
        }
    }
} 