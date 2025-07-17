using DomainLayer.Contracts;
using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RatingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Rating rating)
        {
            await _dbContext.Ratings.AddAsync(rating);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Rating>> GetRatingsForDriverAsync(int driverId)
        {
            return await _dbContext.Ratings.Where(r => r.DriverId == driverId).ToListAsync();
        }
        public async Task<IEnumerable<Rating>> GetRatingsForNurseAsync(int nurseId)
        {
            return await _dbContext.Ratings.Where(r => r.NurseId == nurseId).ToListAsync();
        }
        public async Task<double> GetAverageRatingForDriverAsync(int driverId)
        {
            return await _dbContext.Ratings.Where(r => r.DriverId == driverId).AverageAsync(r => (double?)r.Score) ?? 0;
        }
        public async Task<double> GetAverageRatingForNurseAsync(int nurseId)
        {
            return await _dbContext.Ratings.Where(r => r.NurseId == nurseId).AverageAsync(r => (double?)r.Score) ?? 0;
        }
    }
} 