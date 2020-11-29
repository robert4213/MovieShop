using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using MovieShopWeb.Models;

namespace MovieShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        
        public HomeController(ILogger<HomeController> logger, IMovieService movieService, IGenreService genreService)
        {
            _logger = logger;
            _movieService = movieService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            //
            // var genres = await _genreService.GetAllGenres();
            //
            // var movie = await _movieService.GetMovieAsync(1);

            var testdata = "List of movies";

            ViewBag.myproperty = movies;
            
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}