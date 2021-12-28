using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var data =await _service.GetAll();
            return View(data);
        }

        // Initially, we want just to return the empty view, and
        // when the user provides some data, we want to send another request

        // GET request
        // No need to be async, because there is no data manipulation
        public IActionResult Create()
        {
            return View();
        }
    }
}
