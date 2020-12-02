using System;
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
        private readonly Mapper _mapper;
        
        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            
            var config = new MapperConfiguration(
                configure =>
                {
                    configure.CreateMap<User, UserLoginResponseModel>();
                    configure.CreateMap<UserRegisterRequestModel,UserRegisterResponseModel>();
                }
            );
            _mapper = new Mapper(config);
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