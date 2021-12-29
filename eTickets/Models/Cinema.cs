using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }

        [Display(Name="Cinema Name")]
        public string Name { get; set; }

        [Display(Name = "Cinema Description")]
        public string Description { get; set; }

        // Relationships

        // A cinema can have multiple movies (one-to-many relationship)
        public List<Movie> Movies { get; set; }
    }
}
