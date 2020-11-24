using System;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class UserRepository:EfRepository<User>,IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext):base(dbContext)
        {
            
        }


        public async Task<User> GetUserByEmail()
        {
            throw new NotImplementedException();
        }
    }
}