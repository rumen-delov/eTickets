using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Movie Movie { get; set; }

        public int Amount { get; set; }

        // The items in the shopping cart will be stored in the DB
        // but after the order is completed, we are going to clean up the DB 

        // Each shopping cart item is going to belong to a single shopping cart
        public string ShoppingCartId { get; set; }

        // Another possibility is to create a shopping cart model and 
        // then the shopping cart item is going to have a relationship with the shopping cart model:
        // the shopping cart will have multiple shopping cart items,
        // and a single shopping cart item is going to belong to just one shopping cart model
    }
}
