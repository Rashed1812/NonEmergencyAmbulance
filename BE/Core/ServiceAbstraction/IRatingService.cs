using Shared.DTOS.TripDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IRatingService
    {
        Task AddRatingAsync(RatingDTO dto);
        Task<IEnumerable<RatingDTO>> GetRatingsForDriverAsync(int driverId);
        Task<IEnumerable<RatingDTO>> GetRatingsForNurseAsync(int nurseId);
        Task<double> GetAverageRatingForDriverAsync(int driverId);
        Task<double> GetAverageRatingForNurseAsync(int nurseId);
    }
} 