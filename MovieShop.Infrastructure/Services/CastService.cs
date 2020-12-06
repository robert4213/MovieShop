using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Repository;

namespace MovieShop.Infrastructure.Services
{
    public class CastService:ICastService
    {
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;
        public CastService(ICastRepository castRepository, IMapper mapper)
        {
            _castRepository = castRepository;
            _mapper = mapper;
        }
        
        public async Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
        {
            var cast = await _castRepository.GetCastWithMovie(id);
            if (cast is null) throw new NotFoundException("Cast", id);
            var response = _mapper.Map<CastDetailsResponseModel>(cast);
            response.Movies = _mapper.Map<IEnumerable<MovieResponseModel>>(cast.MovieCasts);
            return response;
        }
    }
}