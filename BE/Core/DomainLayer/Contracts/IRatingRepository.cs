using DomainLayer.Models.Trip_Module;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IRatingRepository
    {
        Task AddAsync(Rating rating);
        Task<IEnumerable<Rating>> GetRatingsForDriverAsync(int driverId);
        Task<IEnumerable<Rating>> GetRatingsForNurseAsync(int nurseId);
        Task<double> GetAverageRatingForDriverAsync(int driverId);
        Task<double> GetAverageRatingForNurseAsync(int nurseId);
    }
} 