using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class ReviewRepository:EfRepository<Review>,IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext):base(dbContext)
        {
        }


        public async Task<decimal> GetAvgRatingById(int id)
        {
            return await _dbContext.Reviews.Where(r=>r.MovieId==id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
        }
    }
}