using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.TripDTOs;

namespace ServiceAbstraction
{
    public interface ITripService
    {
        Task<TripDTO> CreateTripFromRequestAsync(int requestId);
        Task<IEnumerable<TripDTO>> GetTripsForDriverAsync(int driverId);
        Task<IEnumerable<TripDTO>> GetTripsForNurseAsync(int nurseId);
        Task<TripDTO?> GetTripByIdAsync(int tripId);
        Task<TripDTO?> GetTripByRequestIdAsync(int requestId);
        Task<bool> ConfirmTripStartAsync(int tripId);
        Task<bool> CompleteTripAsync(int tripId);
    }
}
