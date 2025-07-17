using Shared.DTOS.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IDriverService
    {

        Task<IEnumerable<DriverDTO>> GetAllDriversAsync();
        Task<DriverDTO> GetDriverByIdAsync(int id);
        Task<DriverDTO> UpdateDriverAsync(int id, DriverDTO dto);
        Task<bool> DeleteDriverAsync(int id);
        Task ToggleAvailabilityAsync(int id, bool isAvailable);

    }
}
