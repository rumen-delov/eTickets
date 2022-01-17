using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.Base.Contracts;
using eTickets.Models;

namespace eTickets.Data.Services.Contracts
{
    public interface IActorsService : IEntityBaseRepository<Actor>
    {
        Task<IEnumerable<Actor>> GetActorsByNameFilter(string searchString);
    }
}
