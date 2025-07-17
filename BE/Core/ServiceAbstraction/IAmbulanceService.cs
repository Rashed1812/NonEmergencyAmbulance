using Shared.DTOS;
using Shared.DTOS.AmbulanceDTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAmbulanceService
    {
        Task<IEnumerable<AmbulanceDTO>> GetAllAmbulancesAsync();
        Task<AmbulanceDTO> GetAmbulanceByIdAsync(int id);
        Task<AmbulanceDTO> CreateAmbulanceAsync(AmbulanceDTO dto);
        Task<AmbulanceDTO> UpdateAmbulanceAsync(int id, AmbulanceDTO dto);
        Task<bool> DeleteAmbulanceAsync(int id);
        Task AssignDriverAsync(int ambulanceId, int driverId);
    }
} 