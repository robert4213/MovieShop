using System;
using System.Collections;
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
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IAsyncRepository<MovieGenre> _movieGenreRepository;
        private readonly IMapper _mapper;
        
        //dependence injection allows to create loosely coupled code
        public MovieService(IMovieRepository movieRepository, IReviewRepository reviewRepository,IAsyncRepository<MovieGenre> movieGenreRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _movieGenreRepository = movieGenreRepository;
            _mapper = mapper;
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
            // // concurrent methods - not valid
            // var movieTask =  _movieRepository.GetByIdAsync(id);
            // var ratingTask =  _reviewRepository.GetAvgRatingById(id);
            //
            // await Task.WhenAll(movieTask, ratingTask);
            //
            // var movie = await movieTask;
            // var rating = await ratingTask;
            
            //non concurrent methods
            var movie = await _movieRepository.GetByIdAsync(id);
            var rating = await _reviewRepository.GetAvgRatingById(id);
            
            // Map process
            if (movie is null) throw new NotFoundException("movie",id);
            
            var response = _mapper.Map<MovieDetailsResponseModel>(movie);
            response.Rating = rating;
            response.FavoritesCount = 0; // temporal
            response.Casts = _mapper.Map<List<MovieDetailsResponseModel.CastResponseModel>>(movie.MovieCasts);
            response.Genres =
                _mapper.Map<List<MovieDetailsResponseModel.GenreMovieResponseModel>>(response.Genres);
            return response;
        }

        public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            var reviews = await _reviewRepository.GetReviewByMovie(id);
            return _mapper.Map<IEnumerable<ReviewMovieResponseModel>>(reviews);
        }

        public async Task<int> GetMoviesCount(string title = "")
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();
            return _mapper.Map<IEnumerable<MovieResponseModel>>(movies);

        }

        public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMovieByGenre(genreId);
            return _mapper.Map<IEnumerable<MovieResponseModel>>(movies);
        }

        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            var validMovie = await _movieRepository.GetMovieByImdbUrl(movieCreateRequest.ImdbUrl);
            
            if (validMovie != null && String.Equals(validMovie.ImdbUrl,movieCreateRequest.ImdbUrl,StringComparison.CurrentCultureIgnoreCase))
                throw new ConflictException("Movie already exists in the database");

            var movie = _mapper.Map<Movie>(movieCreateRequest);

            var createdMovie = await _movieRepository.AddAsync(movie);

            if (movieCreateRequest.Genres != null)
            {
                var genres = new List<MovieGenre>();
                foreach (var genre in movieCreateRequest.Genres)
                {
                    genres.Add(new MovieGenre()
                    {
                        Movieid = createdMovie.Id,
                        Genreid = genre.Id
                    });
                }
                await _movieGenreRepository.AddAsync(genres);
            }
            
            var finalMovie = await _movieRepository.GetByIdAsync(createdMovie.Id);
            
            var response = _mapper.Map<MovieDetailsResponseModel>(finalMovie);

            return response;
        }

        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            // var validMovie = await _movieRepository.GetMovieByImdbUrl(movieCreateRequest.ImdbUrl);
            // if (validMovie is null) throw new NotFoundException("Movie", "ImbdUrl", movieCreateRequest.ImdbUrl);

            var movie = _mapper.Map<Movie>(movieCreateRequest);
            // movie.Id = validMovie.Id;
            
            var updatedMovie = await _movieRepository.UpdateAsync(movie);
            
            if (movieCreateRequest.Genres != null)
            {
                var movieGenres = (await _movieRepository.GetMovieByImdbUrl(movieCreateRequest.ImdbUrl)).MovieGenres;
                await _movieGenreRepository.DeleteAsync(movieGenres);
                
                var genres = new List<MovieGenre>();
                foreach (var genre in movieCreateRequest.Genres)
                {
                    genres.Add(new MovieGenre()
                    {
                        Movieid = updatedMovie.Id,
                        Genreid = genre.Id
                    });
                }
                await _movieGenreRepository.AddAsync(genres);
            }
            
            var finalMovie = await _movieRepository.GetByIdAsync(updatedMovie.Id);
            
            var response = _mapper.Map<MovieDetailsResponseModel>(finalMovie);

            return response;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighestRevenueMovies();
            var movieResponseModel = _mapper.Map<List<MovieResponseModel>>(movies);
            
            // var movieResponseModel = new List<MovieResponseModel>();
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