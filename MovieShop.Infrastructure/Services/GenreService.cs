using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class GenreService:IGenreService
    {
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            throw new System.NotImplementedException();
        }
    }
}