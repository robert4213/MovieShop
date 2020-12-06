using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class GenreService:IGenreService
    {
        private readonly IAsyncRepository<Genre> _repository;
        private readonly IMapper _mapper;
        public GenreService(IAsyncRepository<Genre> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _repository.ListAllAsync();
        }

        public async Task<IEnumerable<Genre>> GetByName(ICollection<string> names)
        {
            return await _repository.ListAsync(g => names.Contains(g.Name));
        }

        public async Task<IEnumerable<GenreResponseModel>> GetGenresResponse()
        {
            var genres = await GetAllGenres();
            return _mapper.Map<IEnumerable<GenreResponseModel>>(genres);
        }
    }
}