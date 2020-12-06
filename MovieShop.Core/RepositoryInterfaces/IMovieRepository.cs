using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IMovieRepository:IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRatedMovies(int size = 20);
        Task<IEnumerable<Movie>> GetMovieByGenre(int genreId);
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<Movie> GetMovieByImdbUrl(string imdbUrl);
    }
}