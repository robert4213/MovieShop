using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShopWeb.Controllers.Movie
{
    public class MovieController:Controller
    {
        private IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        
        public void Index()
        {
            // return View();
        }
        
        public void Details(int movieId)
        {
            // return View();
        }
        
        public void MovieByGenre(int genreId)
        {
            // return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateRequest movieCreateRequest, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            
            if (!ModelState.IsValid) return View();

            await _movieService.CreateMovie(movieCreateRequest);
            
            return LocalRedirect(returnUrl);
        }
    }
}