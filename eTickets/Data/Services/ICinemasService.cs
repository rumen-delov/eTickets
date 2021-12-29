using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface ICinemasService : IEntityBaseRepository<Cinema>
    {
    }
}
