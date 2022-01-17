using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Data.Base.Contracts;

namespace eTickets.Models
{
    public class Movie : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageURL { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MovieCategory MovieCategory { get; set; }

        // Relationships

        // Many-to-many relation to actors through the mapping (joining) table
        // The relation to the mapping table is one-to-many
        public List<ActorMovie> ActorsMovies { get; set; }

        // A movie can be purchased from a single cinema (many-to-one relationship)
        public int CinemaId { get; set; }

        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        // A movie can have a single producer (many-to-one relationship)
        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
    }
}
