using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface IPurchaseRepository:IAsyncRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 0);
        Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 0);
        Task<IEnumerable<Purchase>> GetAllPurchasesByOrderDesc(Expression<Func<Purchase,object>> expression, int pageSize = 30, int pageIndex = 0);
        Task<IEnumerable<Movie>> GetTopSaleMovie(int pageSize = 30, int pageIndex = 0);
        Task<Purchase> GetPurchaseByUserIdAndMovieId(int userId, int movieId);
        Task<IEnumerable<Purchase>> GetPurchaseByUserId(int userId);
    }
}