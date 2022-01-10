using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        // we inject the ShoppingCart, because we are going to get the number of items in the shopping cart
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        // To call the ViewComponent we need the Invoke method
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            return View(items.Count);
        }
    }
}
