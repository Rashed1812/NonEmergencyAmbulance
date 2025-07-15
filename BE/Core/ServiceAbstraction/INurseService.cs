using Shared.DTOS.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface INurseService
    {
        Task<IEnumerable<NurseDetailsDto>> GetAllNursesAsync();
        Task<NurseDetailsDto> GetNurseByIdAsync(int id);
        Task<IEnumerable<NurseDetailsDto>> GetAvailableNursesAsync();
        Task UpdateNurseAsync(int id, NurseDto dto);
        Task<bool> DeleteNurseAsync(int id);
        Task ToggleAvailabilityAsync(int id, bool isAvailable);





    }
}
