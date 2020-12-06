using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Models.Responses;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
    public class PurchaseService:IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;
        
        public PurchaseService(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<PurchaseDetailResponseModel>> GetPurchasesRecord()
        {
            var purchases = await _purchaseRepository.GetAllPurchasesByOrderDesc(p => p.PurchaseDateTime);
            return _mapper.Map<IEnumerable<PurchaseDetailResponseModel>>(purchases);
        }

        public async Task<IEnumerable<MovieDetailsResponseModel>> GetTopSaleMovie()
        {
            var movies = await _purchaseRepository.GetTopSaleMovie();
            return _mapper.Map<IEnumerable<MovieDetailsResponseModel>>(movies);
        }
    }
}