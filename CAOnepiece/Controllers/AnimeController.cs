using AnimeInfoApp.Models;
using AnimeInfoApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimeInfoApp.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AnimeService _animeService;

        public AnimeController(AnimeService animeService)
        {
            _animeService = animeService;
        }

        public async Task<IActionResult> Details(int id = 21)
        {
            var anime = await _animeService.GetAnimeAsync(id);
            return View(anime);
        }
    }
}
