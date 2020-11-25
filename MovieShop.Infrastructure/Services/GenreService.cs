using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class GenreService:IGenreService
    {
        private readonly IAsyncRepository<Genre> _repository;
        public GenreService(IAsyncRepository<Genre> repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _repository.ListAllAsync();
        }
    }
}