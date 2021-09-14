using onlineShop.Model;
using onlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Services.Cart
{
    public interface ICartService
    {
        Task<List<CartResource>> GetAll();
        Task<CartResource> AddCart(CartModel AddedCountries);
        Task<CartResource> GetCartById(int Id);
        Task<CartResource> CartCheckOut(int Id);
        Task<CartResource> GetUserCarts(int UserId);
        Task<CartResource> GetUserCartCheckOut(int UserId);



    }
}
