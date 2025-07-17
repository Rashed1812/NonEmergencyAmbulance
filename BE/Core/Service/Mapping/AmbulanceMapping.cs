using DomainLayer.Models.Ambulance_Module;
using Shared.DTOS;
using Shared.DTOS.AmbulanceDTOS;

namespace Service.Mapping
{
    public static class AmbulanceMapping
    {
        public static AmbulanceDTO ToAmbulanceDTO(this Ambulance ambulance)
        {
            return new AmbulanceDTO
            {
                AmbulanceId = ambulance.AmbulanceId,
                PlateNumber = ambulance.PlateNumber,
                CurrentLocation = ambulance.CurrentLocation,
                Status = ambulance.Status,
                Type = ambulance.Type,
                DriverId = ambulance.DriverId,
                DriverName = ambulance.Driver?.User?.FullName
            };
        }
    }
} 