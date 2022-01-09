using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public void AddItemToCart(Movie movie)
        {
            // Check if we already have this item (movie) in the shopping cart
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            // If we do not have this item in our cart
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1 // this is going to be the first item (first movie of that type)
                };

                // Add this shopping cart to the DB
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else // if we have already added this item to the shopping cart
            {
                // then increase the amount by 1
                shoppingCartItem.Amount++;
            }

            _context.SaveChanges();
        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // ?? the null-calescing operator returns
            // the value of the left-hand operand if is not null
            // otherwise, it evaluates the right-hand operand and returns its result 

            return ShoppingCartItems ??  (ShoppingCartItems = _context.ShoppingCartItems
                
                .Include(n => n.Movie)
                .ToList());           
        }

        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount)
                .Sum();
        }
    }
}
