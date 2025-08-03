using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using ServiceAbstraction;
using Shared;
using Shared.DTOS.Driver;
using Shared.DTOS.Nurse;
using Shared.DTOS.TripDTOs;
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
        private readonly IAmbulanceRepository ambulanceRepository;

        public DriverService(IDriverRepository driverRepository, IAmbulanceRepository ambulanceRepository)
        {
            this.driverRepository = driverRepository;
            this.ambulanceRepository = ambulanceRepository;

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

        public async Task<bool> DeleteDriverAsync(int driverId)
        {
            var driver = await driverRepository.GetByIdWithRequestsAndTripsAsync(driverId);
            if (driver == null)
                return false;

            var ongoingTrips = driver.Trips.Where(t => t.TripStatus == TripStatus.Ongoing).ToList();
            if (ongoingTrips.Any())
            {
                throw new InvalidOperationException("Cannot delete driver with ongoing trips. Complete or cancel trips first.");
            }

            foreach (var trip in driver.Trips.ToList())
            {
                if (trip.TripStatus == TripStatus.Assigned || trip.TripStatus == TripStatus.Pending)
                {
                    trip.TripStatus = TripStatus.Cancelled;
                    if (trip.Request != null)
                    {
                        trip.Request.Status = RequestStatus.Cancelled;
                        trip.Request.DriverId = null;
                        trip.Request.AssignedAmbulanceId = 0;
                        trip.Request.PatientConfirmed = false;
                    }
                }
            }

            foreach (var request in driver.AssignedRequests.ToList())
            {
                if (request.Status == RequestStatus.InProgress ||
                    request.Status == RequestStatus.Accepted ||
                    request.Status == RequestStatus.Pending)
                {
                    request.Status = RequestStatus.Cancelled;
                    request.DriverId = null;
                    request.AssignedAmbulanceId = 0;
                    request.PatientConfirmed = false;
                }
            }

            foreach (var ambulance in driver.Ambulances.ToList())
            {
                ambulanceRepository.Delete(ambulance);
            }

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
