using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        // Reference to the ordered movie
        public int MovieId { get; set; }

        [ForeignKey("MovieId")] // The ForeignKey data annotation can be omitted because
                                // the ASP.NET MVC can understand the relationship between MovieId and Movie
        public Movie Movie { get; set; }

        // Reference to the order
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
