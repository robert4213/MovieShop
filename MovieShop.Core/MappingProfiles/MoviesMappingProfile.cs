using System.Linq;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.Models.Responses;

namespace MovieShop.Core.MappingProfiles
{
    public class MoviesMappingProfile: Profile
    {
        public MoviesMappingProfile()
        {
            // User Service
            CreateMap<User, UserLoginResponseModel>();
            CreateMap<UserRegisterRequestModel,UserRegisterResponseModel>();
            
            // Movie Service
            CreateMap<Movie, MovieDetailsResponseModel>()
                .ForMember(dest=>dest.Genres, opt =>
                    opt.MapFrom(src =>src.MovieGenres.ToList().ConvertAll(input => input.Genre)));
            CreateMap<Cast, MovieDetailsResponseModel.CastResponseModel>();
            CreateMap<MovieCast, MovieDetailsResponseModel.CastResponseModel>().IncludeMembers(src=>src.Cast);
            CreateMap<Movie, MovieResponseModel>()
                .ForMember(dest => dest.ReleaseDate,
                    opt => opt.MapFrom(src => src.ReleaseDate.Value));
            CreateMap<MovieCreateRequest, Movie>();
        }
    }
}