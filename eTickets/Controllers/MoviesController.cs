using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
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

        public async Task<IActionResult> Filter(string searchString) // searchString comes from the _Layout.cshtml file: <input name="searchString" type="text" class="form-control" placeholder="Search for a movie..." />
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies
                    .Where(m => m.Name.Contains(searchString) || m.Description.Contains(searchString))
                    .ToList();

                return View("Index", filteredResult); // return the Index view but with the filtered result
            }

            // If the search string is empty return the Index view with all movies
            return View("Index", allMovies); 
        }

        // GET: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["Welcome"] = "Welcome to our store"; // If you want to pass complex data type from controller to a view
            //                                              // using the ViewData, it requires type casting 
            //ViewBag.Description = "This is the store description"; // while ViewBag does not require type casting for complex data types

            var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName");

                return View(movie); // return the same view, but now it will contains the validation errors 
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            
            if (movieDetails == null)
            {
                return View("NotFound");
            }

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
                ActorIds = movieDetails.ActorsMovies.Select(a => a.ActorId).ToList()
            };
                       
            var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if(id != movie.Id)
            {
                return View("NotFound");
            }
            
            
            // Validate the model state
            if (!ModelState.IsValid)
            {
                var movieDropdownsdata = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsdata.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsdata.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsdata.Actors, "Id", "FullName");

                return View(movie); // return the same view, but now it will contains the validation errors 
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
