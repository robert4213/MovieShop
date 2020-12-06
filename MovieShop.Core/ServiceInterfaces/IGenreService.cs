using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Responses;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Genre>> GetByName(ICollection<string> names);
        Task<IEnumerable<GenreResponseModel>> GetGenresResponse();
    }
}