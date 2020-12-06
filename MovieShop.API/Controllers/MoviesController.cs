using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    // Attribute based routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);

            return movie is null ? NotFound("No movie found") : Ok(movie);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovie()
        {
            var movies = await _movieService.GetTopRatedMovies();
            
            return movies.Any()?Ok(movies): NotFound("No movies found");
        }
        
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            return movies.Any()?Ok(movies):NotFound("No Movies Found");
        }
        
        [HttpGet]
        [Route("genre/{id}")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movies = await _movieService.GetMoviesByGenre(id);

            return movies.Any()?Ok(movies):NotFound("No Movies Found");
        }

        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReviewsForMovie(int id)
        {
            var reviews = await _movieService.GetReviewsForMovie(id);
            return reviews.Any()?Ok(reviews):NotFound("No Reviews Found");
        }
    }
}
