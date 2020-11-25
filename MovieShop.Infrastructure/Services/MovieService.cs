﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Data.SqlClient;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Data;
using MovieShop.Infrastructure.Repository;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IReviewRepository _reviewRepository;
        private readonly Mapper _mapper;
        
        //dependence injection allows to create loosely coupled code
        public MovieService(IMovieRepository repository, IReviewRepository reviewRepository)
        {
            _repository = repository;
            _reviewRepository = reviewRepository;

            var config = new MapperConfiguration(
                configure =>
                {
                    configure.CreateMap<Movie, MovieDetailsResponseModel>()
                        .ForMember(dest=>dest.Genres, opt =>
                        opt.MapFrom(src =>src.MovieGenres.ToList().ConvertAll(input => input.Genre)));
                    configure.CreateMap<Cast, MovieDetailsResponseModel.CastResponseModel>();
                    configure.CreateMap<MovieCast, MovieDetailsResponseModel.CastResponseModel>().IncludeMembers(src=>src.Cast);
                    configure.CreateMap<Movie, MovieResponseModel>()
                        .ForMember(dest => dest.ReleaseDate,
                            opt => opt.MapFrom(src => src.ReleaseDate.Value));
                });
            _mapper = new Mapper(config);
        }
        
        public async Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            // // concurrent methods
            // var movieTask =  _repository.GetByIdAsync(id);
            // var ratingTask =  _reviewRepository.GetAvgRatingById(id);
            //
            // await Task.WhenAll(movieTask, ratingTask);
            //
            // var movie = await movieTask;
            // var rating = await ratingTask;
            
            //non concurrent methods
            var movie = await _repository.GetByIdAsync(id);
            var rating = await _reviewRepository.GetAvgRatingById(id);
            
            // Map process
            if (movie is null) throw new NotFoundException("movie",id);
            
            var response = _mapper.Map<MovieDetailsResponseModel>(movie);
            response.Rating = rating;
            response.FavoritesCount = 0; // temporal
            response.Casts = _mapper.Map<List<MovieDetailsResponseModel.CastResponseModel>>(movie.MovieCasts);

            return response;
        }

        public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> GetMoviesCount(string title = "")
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _repository.GetHighestRevenueMovies();
            var movieResponseModel = _mapper.Map<List<MovieResponseModel>>(movies);
            
            // var movieResponseModel =    new List<MovieResponseModel>();
            //
            // foreach (var movie in movies)
            // {
            //     movieResponseModel.Add(new MovieResponseModel
            //     {
            //         Id = movie.Id, PosterUrl = movie.PosterUrl, ReleaseDate = movie.ReleaseDate.Value, Title = movie.Title
            //     });
            // }
            
            return movieResponseModel;
        }
    }
}