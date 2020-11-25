using System.Threading.Tasks;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class CastService:ICastService
    {
        public async Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}