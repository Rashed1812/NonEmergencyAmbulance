using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Trip_Module;
using Service.Mapping;
using ServiceAbstraction;
using Shared;
using Shared.DTOS.TripDTOs;

namespace Service
{
    public class TripService(ITripRepository _tripRepo ,IRequestRepository _requestRepo) : ITripService
    {
        public async Task<TripDTO> CreateTripFromRequestAsync(int requestId)
        {
            var request = await _requestRepo.GetByIdWithReletadData(requestId);
            if (request == null)
                throw new Exception("Request not found");

            if (request.Status != RequestStatus.InProgress || !request.PatientConfirmed)
                throw new Exception("Request not confirmed by patient");

            var existingTrip = await _tripRepo.GetByRequestIdAsync(requestId);
            if (existingTrip != null)
                throw new Exception("Trip already exists");

            var distance = await _distanceService.CalculateKMAsync(request.PickupAddress, request.DropOffAddress);
            var price = _priceCalculator.Calculate(request, distance);

            var trip = new Trip
            {
                RequestId = requestId,
                StartTime = request.ScheduledDate,
                EndTime = request.ScheduledDate.AddHours(1), // مؤقت
                DistanceKM = distance,
                Price = price,
                DriverId = request.DriverId.Value,
                NurseId = request.NurseId,
                AmbulanceId = request.AssignedAmbulanceId,
                TripStatus = TripStatus.Assigned
            };

            await _tripRepo.AddAsync(trip);
            await _tripRepo.SaveChangesAsync();

            return trip.ToTripDTO();
        }

        public async Task<IEnumerable<TripDTO>> GetTripsForDriverAsync(int driverId)
        {
            var trips = await _tripRepo.GetByDriverIdAsync(driverId);
            return trips.Select(t => t.ToTripDTO());
        }

        public async Task<IEnumerable<TripDTO>> GetTripsForNurseAsync(int nurseId)
        {
            var trips = await _tripRepo.GetByNurseIdAsync(nurseId);
            return trips.Select(t => t.ToTripDTO());
        }

        public async Task<TripDTO?> GetTripByIdAsync(int tripId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            return trip?.ToTripDTO();
        }

        public async Task<TripDTO?> GetTripByRequestIdAsync(int requestId)
        {
            var trip = await _tripRepo.GetByRequestIdAsync(requestId);
            return trip?.ToTripDTO();
        }

        public async Task<bool> ConfirmTripStartAsync(int tripId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null || trip.TripStatus != TripStatus.Assigned)
                return false;

            trip.TripStatus = TripStatus.Ongoing;
            trip.StartTime = DateTime.UtcNow;
            await _tripRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteTripAsync(int tripId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId);
            if (trip == null || trip.TripStatus != TripStatus.Ongoing)
                return false;

            trip.TripStatus = TripStatus.Completed;
            trip.EndTime = DateTime.UtcNow;
            await _tripRepo.SaveChangesAsync();
            return true;
        }
    }
}
