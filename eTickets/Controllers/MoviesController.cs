using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
            ViewData["Welcome"] = "Welcome to our store"; // If you want to pass complex data type from controller to a view
                                                          // using the ViewData, it requires type casting 
            ViewBag.Description = "This is the store description"; // while ViewBag does not require type casting for complex data types

            return View();
        }
    }
}
