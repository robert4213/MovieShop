using Microsoft.AspNetCore.DataProtection.Internal;

namespace MovieShopWeb.Controllers.Movie
{
    public class MovieController
    {
        public IActivator Index()
        {
            return View();
        }
        
        public IActivator Details(int movieId)
        {
            return View();
        }
        
        public IActivator MovieByGenre(int genreId)
        {
            return View();
        }
    }
}