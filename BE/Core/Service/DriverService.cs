using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using ServiceAbstraction;
using Shared.DTOS.Driver;
using Shared.DTOS.Nurse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public async Task<IEnumerable<DriverDTO>> GetAllDriversAsync()
        {
            var drivers = await driverRepository.GetAllWithRelatedData(); 

            return drivers.Select(n => new DriverDTO
            {
                Id = n.Id,
                LicenseNumber = n.LicenseNumber,
                IsAvailable = n.IsAvailable,
                PhoneNumber = n.PhoneNumber,
                UserId = n.UserId,
                UserFullName = n.User.FullName
            });
        }

        public async Task<DriverDTO> GetDriverByIdAsync(int id)
        {
            var driver = await driverRepository.GetByIdWithRelatedData(id);
            if (driver == null)
            {
                return null;
            }

            return new DriverDTO
            {

                Id = driver.Id,
                LicenseNumber = driver.LicenseNumber,
                IsAvailable = driver.IsAvailable,
                PhoneNumber = driver.PhoneNumber,
                UserId = driver.UserId,
                UserFullName = driver.User.FullName

            };
        }
    }
}
