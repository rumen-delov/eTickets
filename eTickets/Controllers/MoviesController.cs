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
    }
}
