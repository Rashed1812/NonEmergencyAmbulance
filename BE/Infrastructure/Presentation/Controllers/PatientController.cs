using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS.Registeration;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDTO dto)
        {
            var updated = await _patientService.UpdatePatientAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpGet("{id}/requests")]
        public async Task<IActionResult> GetRequests(int id)
        {
            var requests = await _patientService.GetRequestsForPatientAsync(id);
            return Ok(requests);
        }

        [HttpGet("{id}/trips")]
        public async Task<IActionResult> GetTrips(int id)
        {
            var trips = await _patientService.GetTripsForPatientAsync(id);
            return Ok(trips);
        }
    }
} 