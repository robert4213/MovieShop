using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.ServiceInterfaces;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MovieShopWeb.Controllers.Movie
{
    public class MovieController:Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
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
            var genres = await _genreService.GetAllGenres();
            
            var model = new MovieCreateRequest()
            {
                Genres = genres.ToList()
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateRequest movieCreateRequest, List<string> genres ,IFormCollection formCollection, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            genres ??= new List<string>();
            
            if (!ModelState.IsValid) return View();
            // movieCreateRequest.Genres = Genres;
            movieCreateRequest.Genres = (await _genreService.GetByName(genres)).ToList();
            
            await _movieService.CreateMovie(movieCreateRequest);
            
            return LocalRedirect(returnUrl);
        }
    }
    
    class GenresSelected
    {
        public MovieShop.Core.Entities.Genre genre;
        public bool isSelected = false;
        
        public GenresSelected(MovieShop.Core.Entities.Genre genre)
        {
            this.genre = genre;
        }
    }
}