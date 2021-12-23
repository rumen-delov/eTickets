using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        // To get or send data to the Db we need a db context
        private readonly AppDbContext _context;

        // To be able to use the context we need to inject it into the constructor
        public ActorsController(AppDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            // Get the data
            List<Actor> data = _context.Actors.ToList();

            return View();
        }
    }
}
