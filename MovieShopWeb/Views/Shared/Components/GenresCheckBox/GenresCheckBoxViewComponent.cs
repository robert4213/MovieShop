using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShopWeb.Views.Shared.Components.GenresCheckBox
{
    public class GenresCheckBoxViewComponent:ViewComponent
    {
        private readonly IGenreService _genreService;
        
        public GenresCheckBoxViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _genreService.GetAllGenres());
        }
    }
}