using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class MovieRepository : EfRepository<Movie>,IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetMovieByGenre(int genreId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(50).ToListAsync();
            return movies;
        }
        
        public override async Task<Movie> GetByIdAsync(int id)
        {
            // non concurrent method
            // dynamic movie = await _dbContext.Movies
            //     .Include(m => m.MovieCasts)
            //     .ThenInclude(m => m.Cast)
            //     .Include(m => m.MovieGenres)
            //     .ThenInclude(m => m.Genre)
            //     .FirstOrDefaultAsync(m => m.Id == id);
            // if (movie == null) return null;
            // var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
            //     .AverageAsync(r => r == null ? 0 : r.Rating);
            // if (movieRating > 0) movie.Rating = movieRating;
            // return movie;
            
            // concurrent method
            // var movieTask = _dbContext.Movies
            //     .Include(m => m.MovieCasts)
            //     .ThenInclude(m => m.Cast)
            //     .Include(m => m.MovieGenres)
            //     .ThenInclude(m => m.Genre)
            //     .FirstOrDefaultAsync(m => m.Id == id);
            // var ratingTask = _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
            //     .AverageAsync(r => r == null ? 0 : r.Rating);
            // await Task.WhenAll(movieTask, ratingTask);
            //
            // dynamic movie = await movieTask;
            // var movieRating = await ratingTask;
            // if (movie == null) return null;
            // if (movieRating > 0) movie.Rating = movieRating;
            // return movie;
            
            // Separate method
            var movie = await _dbContext.Movies
            .Include(m => m.MovieCasts)
            .ThenInclude(m => m.Cast)
            .Include(m => m.MovieGenres)
            .ThenInclude(m => m.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);
            return movie is null ? null:movie;
        }
    }
}