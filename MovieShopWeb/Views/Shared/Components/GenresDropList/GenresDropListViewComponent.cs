using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShopWeb.Views.Shared.Components.GenresDropList
{
    public class GenresDropListViewComponent:ViewComponent
    {
        private readonly IGenreService _genreService;

        public GenresDropListViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }
    }
}