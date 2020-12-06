using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class ReviewRepository : EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<decimal> GetAvgRatingById(int id)
        {
            return await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
        }

        public async Task<Dictionary<int, decimal>> GetTopRating()
        {
            return await _dbContext.Reviews.GroupBy(r => r.MovieId)
                .Select(g => new
                {
                    Id = g.Key,
                    Rating = g.Average(r => r.Rating)
                }).ToDictionaryAsync(r => r.Id, r => r.Rating);
        }

        public async Task<IEnumerable<Review>> GetReviewByMovie(int id)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.MovieId == id)
                .Include(r => r.Movie).ToListAsync();
            return reviews;
        }
    }
}