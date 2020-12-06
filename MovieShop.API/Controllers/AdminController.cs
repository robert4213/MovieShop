using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IPurchaseService _purchaseService;
        private readonly IGenreService _genreService;
        
        public AdminController(IMovieService movieService, IPurchaseService purchaseService, IGenreService genreService)
        {
            _movieService = movieService;
            _purchaseService = purchaseService;
            _genreService = genreService;
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            if (movieCreateRequest.GenresString != null) movieCreateRequest.Genres = (await _genreService.GetByName(movieCreateRequest.GenresString)).ToList();
            
            var response = await _movieService.CreateMovie(movieCreateRequest);
            return Ok(response);
        }
        
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            if (movieCreateRequest.GenresString != null) movieCreateRequest.Genres = (await _genreService.GetByName(movieCreateRequest.GenresString)).ToList();
            var response = await _movieService.UpdateMovie(movieCreateRequest);
            return Ok(response);
        }

        // Not tested no data
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetPurchasesRecord()
        {
            var purchasesRecord = await _purchaseService.GetPurchasesRecord();
            return Ok(purchasesRecord);
        }

        // Not tested no data
        [HttpGet]
        [Route("top")]
        public async Task<IActionResult> GetTopSaleMovie()
        {
            var movies = await _purchaseService.GetTopSaleMovie();
            return Ok(movies);
        }
    }
}