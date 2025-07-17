using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly INurseService _nurseService;
        private readonly IAmbulanceService _ambulanceService;
        private readonly IPatientService _patientService;
        private readonly IRequestService _requestService;
        private readonly ITripService _tripService;

        public AdminController(
            IDriverService driverService,
            INurseService nurseService,
            IAmbulanceService ambulanceService,
            IPatientService patientService,
            IRequestService requestService,
            ITripService tripService)
        {
            _driverService = driverService;
            _nurseService = nurseService;
            _ambulanceService = ambulanceService;
            _patientService = patientService;
            _requestService = requestService;
            _tripService = tripService;
        }

        [HttpGet("drivers")]
        public async Task<IActionResult> GetAllDrivers() => Ok(await _driverService.GetAllDriversAsync());

        [HttpGet("nurses")]
        public async Task<IActionResult> GetAllNurses() => Ok(await _nurseService.GetAllNursesAsync());

        [HttpGet("ambulances")]
        public async Task<IActionResult> GetAllAmbulances() => Ok(await _ambulanceService.GetAllAmbulancesAsync());

        [HttpGet("patients")]
        public async Task<IActionResult> GetAllPatients() => Ok(await _patientService.GetAllPatientsAsync());

        [HttpGet("requests")]
        public async Task<IActionResult> GetAllRequests() => Ok(await _requestService.GetAllRequestsWithRelatedData());

        [HttpGet("trips")]
        public async Task<IActionResult> GetAllTrips() => Ok(await _tripService.GetTripsForDriverAsync(0));
    }
} 