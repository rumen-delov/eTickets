using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Services.Contracts;
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

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // Prepare the ViewData["NameSortParam"] for the next click in the Index.cshtml view
            // If the sortOrder is null then set ViewData["NameSortParam"] to "name_desc" and return to the Index view
            // so the next time the sorting button is clicked the sortOrder will be not empty 
            // and the ViewData["NameSortParam"] will be set to "" and returned to the Index view
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // For the search box
            ViewData["CurrentFilter"] = searchString;

            // Right now the service delivers all the actors from the DB
            // Later it should be adjusted in a way that the entire sorting query is done in the DB?   
            //IEnumerable<Actor> actors = await _service.GetAllAsync();
            var actors = string.IsNullOrEmpty(searchString) ?
                await _service.GetAllAsync() :
                await _service.GetActorsByNameFilter(searchString);

            switch (sortOrder)
            {
                case "name_desc":
                    actors = actors.OrderByDescending(a => a.FullName);
                    break;
                default:
                    actors = actors.OrderBy(a => a.FullName);
                    break;
            }

            return View(actors);
        }

        // Initially, we want just to return the empty view, and
        // when the user provides some data, we want to send another request

        // GET request: Actors/Create
        // No need to be async, because there is no data manipulation
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Because we are sending a POST request from Create.cshtml
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Actor actor) // Bind the properties that we are going to send from the Create view
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        // GET request: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);
        }

        // GET request: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);
        }

        [HttpPost] // Because we are sending a POST request from Edit.cshtml
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePictureURL, Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        // GET request: Actors/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);
        }

        [HttpPost] // Because we are sending a POST request from Delete.cshtml
        [ActionName("Delete")] // ActionName("Delete") allows the method DeleteConfirmed to be called as Delete in a POST request 
        public async Task<IActionResult> DeleteConfirmed(int id) // We cannot have two methods with the same name and same parameter,
                                                                 // so we use DeleteConfirmed instead of just Delete 
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
