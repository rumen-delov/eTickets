using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Models;

namespace eTickets.Data.Services.Contracts
{
    public interface IOrdersService
    {
        // Add orders to the DB        
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress); // Authentication and autherization for the userId is
                                                                                                    // still not implemented 
        //  Get all orders from the DB
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
