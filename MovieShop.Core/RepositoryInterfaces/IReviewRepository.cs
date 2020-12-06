using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IReviewRepository:IAsyncRepository<Review>
    {
        Task<decimal> GetAvgRatingById(int id);
        Task<Dictionary<int, decimal>> GetTopRating();
        Task<IEnumerable<Review>> GetReviewByMovie(int id);
    }
}