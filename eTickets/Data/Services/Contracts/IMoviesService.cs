using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.Base.Contracts;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services.Contracts
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);

        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();

        Task AddNewMovieAsync(NewMovieVM data);
        
        Task UpdateMovieAsync(NewMovieVM data); 
    }
}
