using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Models.Responses;

namespace MovieShop.Core.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseDetailResponseModel>> GetPurchasesRecord();
        Task<IEnumerable<MovieDetailsResponseModel>> GetTopSaleMovie();
    }
}