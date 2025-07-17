using Shared.DTOS.Registeration;
using Shared.DTOS.RequestDTOS;
using Shared.DTOS.TripDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDTO>> GetAllPatientsAsync();
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task<PatientDTO> UpdatePatientAsync(int id, PatientDTO dto);
        Task<bool> DeletePatientAsync(int id);
        Task<IEnumerable<RequestDTO>> GetRequestsForPatientAsync(int patientId);
        Task<IEnumerable<TripDTO>> GetTripsForPatientAsync(int patientId);
    }
} 