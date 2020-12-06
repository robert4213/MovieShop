using System.Collections.Generic;
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
            CreateMap<User, UserRegisterResponseModel>();
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
            
            //Purchase Service
            CreateMap<Purchase, PurchaseDetailResponseModel>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<PurchaseRequestModel,Purchase>();
            CreateMap<Movie, PurchaseResponseModel.PurchasedMovieResponseModel>();
            CreateMap<Purchase, PurchaseResponseModel.PurchasedMovieResponseModel>().IncludeMembers(src => src.Movie);
            CreateMap<IEnumerable<Purchase>, PurchaseResponseModel>()
                .ForMember(dest=>dest.UserId,opt=>opt.MapFrom(src=>src.First().UserId));
            
            // Cast Service
            CreateMap<Cast, CastDetailsResponseModel>();
            CreateMap<MovieCast, MovieResponseModel>().IncludeMembers(src => src.Movie);
            
            //Genre Service
            CreateMap<Genre, GenreResponseModel>();
            
            // Review Service
            CreateMap<Review,ReviewMovieResponseModel>().ForMember(dest=>dest.Name,
                opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<IEnumerable<Review>,ReviewResponseModel>()
                .ForMember(dest=>dest.UserId,opt=>opt.MapFrom(src=>src.First().UserId));
            CreateMap<ReviewRequestModel, Review>();
            
            //Favorite Service
            CreateMap<FavoriteRequestModel, Favorite>();
            CreateMap<Movie, FavoriteResponseModel.FavoriteMovieResponseModel>();
            CreateMap<Favorite, FavoriteResponseModel.FavoriteMovieResponseModel>().IncludeMembers(src => src.Movie);
            CreateMap<IEnumerable<Favorite>,FavoriteResponseModel>()
                .ForMember(dest=>dest.UserId,opt=>opt.MapFrom(src=>src.First().UserId));
        }
    }
}