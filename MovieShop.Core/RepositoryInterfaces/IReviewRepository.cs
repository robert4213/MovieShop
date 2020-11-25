using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IReviewRepository
    {
        Task<decimal> GetAvgRatingById(int id);
    }
}