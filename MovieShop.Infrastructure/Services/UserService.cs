using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.Exceptions;
using MovieShop.Core.MappingProfiles;
using MovieShop.Core.Models.Requests;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository userRepository, ICryptoService cryptoService,IPurchaseRepository purchaseRepository, IAsyncRepository<Favorite> favoriteRepository, IReviewRepository reviewRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _mapper = mapper;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }
        
        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // we are gonna check if the email exists in the database
            var user = await _userRepository.GetUserByEmail(email);
            if (user is null) return null;
            var hashedPassword = _cryptoService.HashPassword(password, user.Salt);
            var isSuccess =  user.HashedPassword == hashedPassword;

            var response = _mapper.Map<UserLoginResponseModel>(user);

            return isSuccess ? response : null;
        }

        public async Task<UserLoginResponseModel> ValidateUser(LoginRequestModel loginRequestModel)
        {
            return await ValidateUser(loginRequestModel.Email, loginRequestModel.Password);
        }

        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            // send email to repo, and check whether email exists
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            
            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new ConflictException("Email Already Exits");
            
            // first is to create unique random salt
            string salt = _cryptoService.CreateSalt();
            string hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);
            
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);
            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id, Email = createdUser.Email, FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return response;
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserRegisterResponseModel>(user);
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
            if (await FavoriteExists(favoriteRequest.UserId,favoriteRequest.MovieId))
                throw new ConflictException("User already add favorite");

            var favorite = _mapper.Map<Favorite>(favoriteRequest);
            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            // if (!(await FavoriteExists(favoriteRequest.UserId,favoriteRequest.MovieId)))
            //     throw new NotFoundException("User doesn't have this favorite");
            var favorite =await _favoriteRepository.ListAsync(f =>
                f.MovieId == favoriteRequest.MovieId && f.UserId == favoriteRequest.UserId);
            if (!favorite.Any()) throw new NotFoundException("No Favorite Found");
            await _favoriteRepository.DeleteAsync(favorite.First());
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f =>
                f.MovieId == movieId && f.UserId == id);
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.ListAllWithIncludesAsync(f => f.UserId == id, f => f.Movie);
            var response = _mapper.Map<FavoriteResponseModel>(favorites);
            response.FavoriteMovies =
                _mapper.Map<IEnumerable<FavoriteResponseModel.FavoriteMovieResponseModel>>(favorites).ToList();
            return response;
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            if (await _purchaseRepository.GetPurchaseByUserIdAndMovieId(purchaseRequest.UserId, purchaseRequest.MovieId) !=
                null)
                throw new ConflictException("Purchase already exists");
            
            purchaseRequest.PurchaseNumber??= Guid.NewGuid();
            purchaseRequest.PurchaseDateTime??= DateTime.Now;
            var purchase = _mapper.Map<Purchase>(purchaseRequest);
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.GetPurchaseByUserId(id);
            var response = _mapper.Map<PurchaseResponseModel>(purchases);
            response.PurchasedMovies =
                _mapper.Map<IEnumerable<PurchaseResponseModel.PurchasedMovieResponseModel>>(purchases).ToList();
            return response;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            if (await _reviewRepository.GetExistsAsync(r =>
                r.MovieId == reviewRequest.MovieId && r.UserId == reviewRequest.UserId))
                throw new ConflictException("Review already exists");
            var review = _mapper.Map<Review>(reviewRequest);
            await _reviewRepository.AddAsync(review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            if (!(await _reviewRepository.GetExistsAsync(r =>
                r.MovieId == reviewRequest.MovieId && r.UserId == reviewRequest.UserId)))
                throw new NotFoundException("Review does not exist");
            var review = _mapper.Map<Review>(reviewRequest);
            await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var review = await _reviewRepository.ListAsync(r =>
                r.MovieId == movieId && r.UserId == userId);
            if(!review.Any()) throw new NotFoundException("Review does not exist");
            await _reviewRepository.DeleteAsync(review.First());
        }

        public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var review = await _reviewRepository.ListAllWithIncludesAsync(r => r.UserId == id, r => r.Movie);
            var response = _mapper.Map<ReviewResponseModel>(review);
            response.MovieReviews = _mapper.Map<IEnumerable<ReviewMovieResponseModel>>(review).ToList();
            return response;
        }
    }
}