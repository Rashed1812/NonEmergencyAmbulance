using DomainLayer.Contracts;
using DomainLayer.Models.Trip_Module;
using ServiceAbstraction;
using Shared.DTOS.TripDTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task AddRatingAsync(RatingDTO dto)
        {
            var rating = new Rating
            {
                TripId = dto.TripId,
                DriverId = dto.DriverId,
                NurseId = dto.NurseId,
                PatientId = dto.PatientId,
                Score = dto.Score,
                Notes = dto.Notes,
                CreatedAt = dto.CreatedAt == default ? System.DateTime.UtcNow : dto.CreatedAt
            };
            await _ratingRepository.AddAsync(rating);
        }
        public async Task<IEnumerable<RatingDTO>> GetRatingsForDriverAsync(int driverId)
        {
            var ratings = await _ratingRepository.GetRatingsForDriverAsync(driverId);
            return ratings.Select(r => new RatingDTO
            {
                Id = r.Id,
                TripId = r.TripId,
                DriverId = r.DriverId,
                NurseId = r.NurseId,
                PatientId = r.PatientId,
                Score = r.Score,
                Notes = r.Notes,
                CreatedAt = r.CreatedAt
            });
        }
        public async Task<IEnumerable<RatingDTO>> GetRatingsForNurseAsync(int nurseId)
        {
            var ratings = await _ratingRepository.GetRatingsForNurseAsync(nurseId);
            return ratings.Select(r => new RatingDTO
            {
                Id = r.Id,
                TripId = r.TripId,
                DriverId = r.DriverId,
                NurseId = r.NurseId,
                PatientId = r.PatientId,
                Score = r.Score,
                Notes = r.Notes,
                CreatedAt = r.CreatedAt
            });
        }
        public async Task<double> GetAverageRatingForDriverAsync(int driverId)
        {
            return await _ratingRepository.GetAverageRatingForDriverAsync(driverId);
        }
        public async Task<double> GetAverageRatingForNurseAsync(int nurseId)
        {
            return await _ratingRepository.GetAverageRatingForNurseAsync(nurseId);
        }
    }
} 