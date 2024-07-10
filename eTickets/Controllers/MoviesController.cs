using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if(!string.IsNullOrEmpty(searchString))
            {
                var varFilteredResult = allMovies.Where(n=> n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", varFilteredResult);
            }
            return View("Index", allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetNewMovieDropdownsVM();
            ViewBag.cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsVM();
                ViewBag.cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var movieDropdownData = await _service.GetNewMovieDropdownsVM();
            ViewBag.cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsVM();
                ViewBag.cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
