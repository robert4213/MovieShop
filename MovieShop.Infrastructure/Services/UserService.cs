using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class UserService:IUserService
    {
        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PagedResultSet<User>> GetAllUsersByPagination(int pageSize = 20, int page = 0, string lastName = "")
        {
            throw new System.NotImplementedException();
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}