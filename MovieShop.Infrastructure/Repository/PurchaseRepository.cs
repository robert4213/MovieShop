using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository
{
    public class PurchaseRepository: EfRepository<Purchase>,IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext):base(dbContext)
        {
            
        }
        
        public async Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 0)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 0)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByOrderDesc(Expression<Func<Purchase, object>> expression, int pageSize = 30, int pageIndex = 0)
        {
            return await _dbContext.Purchases.OrderByDescending(expression).Take(pageSize).Skip(pageIndex * pageSize)
                .Include(p=>p.Movie).Include(p=>p.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetTopSaleMovie(int pageSize = 30, int pageIndex = 0)
        {
            var order = await _dbContext.Purchases.GroupBy(p => p.MovieId).Select(g=>new {Id = g.Key,Count = g.Count()})
                .OrderByDescending(p => p.Count).ToDictionaryAsync(obj=>obj.Id,obj=>obj.Count);
            return _dbContext.Movies.Where(m => order.Keys.Contains(m.Id)).AsEnumerable().OrderByDescending(m => order[m.Id])
                .ToList();
        }

        public async Task<Purchase> GetPurchaseByUserIdAndMovieId(int userId, int movieId)
        {
            return await _dbContext.Purchases.Where(p => p.MovieId == movieId && p.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchaseByUserId(int userId)
        {
            return await _dbContext.Purchases.Where(p => p.UserId == userId).Include(p => p.Movie).ToListAsync();
        }
    }
}