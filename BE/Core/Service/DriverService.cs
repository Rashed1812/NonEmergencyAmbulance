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

        public async Task<DriverDTO> UpdateDriverAsync(int id, DriverDTO dto)
        {
            var driver = await driverRepository.GetByIdAsync(id);
            if (driver == null) return null;
            driver.LicenseNumber = dto.LicenseNumber;
            driver.PhoneNumber = dto.PhoneNumber;
            driver.IsAvailable = dto.IsAvailable;
            driver.UserId = dto.UserId;
            driverRepository.Update(driver);
            await driverRepository.SaveChangesAsync();
            return new DriverDTO
            {
                Id = driver.Id,
                LicenseNumber = driver.LicenseNumber,
                IsAvailable = driver.IsAvailable,
                PhoneNumber = driver.PhoneNumber,
                UserId = driver.UserId,
                UserFullName = driver.User?.FullName
            };
        }

        public async Task<bool> DeleteDriverAsync(int id)
        {
            var driver = await driverRepository.GetByIdAsync(id);
            if (driver == null) return false;
            driverRepository.Delete(driver);
            await driverRepository.SaveChangesAsync();
            return true;
        }

        public async Task ToggleAvailabilityAsync(int id, bool isAvailable)
        {
            var driver = await driverRepository.GetByIdAsync(id);
            if (driver == null) throw new KeyNotFoundException("Driver not found");
            driver.IsAvailable = isAvailable;
            await driverRepository.SaveChangesAsync();
        }
    }
}
