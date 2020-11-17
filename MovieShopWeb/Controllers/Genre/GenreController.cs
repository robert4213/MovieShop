using Microsoft.AspNetCore.DataProtection.Internal;

namespace MovieShopWeb.Controllers.Genre
{
    public class GenreController
    {
        public IActivator Index()
        {
            return View();
        }
    }
}