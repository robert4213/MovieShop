using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseMovie(PurchaseRequestModel purchaseRequestModel)
        {
            await _userService.PurchaseMovie(purchaseRequestModel);
            return Ok("Succeed");
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            await _userService.AddFavorite(favoriteRequestModel);
            return Ok("Succeed");
        }
        
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            await _userService.RemoveFavorite(favoriteRequestModel);
            return Ok("Succeed");
        }

        [HttpGet]
        [Route("{id}/movie/{movieId}/favorite")]
        public async Task<IActionResult> FavoriteExists(int id, int movieId)
        {
            return await _userService.FavoriteExists(id, movieId) ? Ok("Exists") : NotFound("No favorite");
        }

        [HttpPost]
        [Route("Review")]
        public async Task<IActionResult> AddReview(ReviewRequestModel reviewRequestModel)
        {
            await _userService.AddMovieReview(reviewRequestModel);
            return Ok("Succeed");
        }
        
        [HttpPut]
        [Route("Review")]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel reviewRequestModel)
        {
            await _userService.UpdateMovieReview(reviewRequestModel);
            return Ok("Succeed");
        }

        [HttpDelete]
        [Route("{id}/movie/{movieId}")]
        public async Task<IActionResult> DeleteReview(int id, int movieId)
        {
            await _userService.DeleteMovieReview(id, movieId);
            return Ok("Succeed");
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/purchases")]
        public async Task<IActionResult> GetPurchase(int id)
        {
            var purchase = await _userService.GetAllPurchasesForUser(id);
            return purchase != null ? Ok(purchase) : NotFound("No Purchased Found");
        }
        
        [HttpGet]
        [Route("{id}/favorites")]
        public async Task<IActionResult> GetFavorite(int id)
        {
            var favorites = await _userService.GetAllFavoritesForUser(id);
            return favorites != null ? Ok(favorites) : NotFound("No Favorites Found");
        }
        
        [HttpGet]
        [Route("{id}/reviews")]
        public async Task<IActionResult> GetReview(int id)
        {
            var reviews = await _userService.GetAllReviewsByUser(id);
            return reviews != null ? Ok(reviews) : NotFound("No Reviews Found");
        }
    }
}