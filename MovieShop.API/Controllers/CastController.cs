using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Exceptions;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController:ControllerBase
    {
        private readonly ICastService _castService;
        
        public  CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCastWithMovie(int id)
        {
            try
            {
                var response = await _castService.GetCastDetailsWithMovies(id);
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}