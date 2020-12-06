using System.Threading.Tasks;
using MovieShop.Core.Entities;

namespace MovieShop.Core.RepositoryInterfaces
{
    public interface ICastRepository:IAsyncRepository<Cast>
    {
        Task<Cast> GetCastWithMovie(int id);
    }
}