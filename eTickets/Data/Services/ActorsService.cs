using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.Services.Contracts;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Actor>> GetActorsByNameFilter(string searchString)
        {
            return await _context.Actors.Where(a => a.FullName.Contains(searchString)).ToListAsync();
        }
    }
}
