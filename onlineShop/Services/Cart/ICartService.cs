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
        Task<ServiceResponse<List<CartResource>>> GetAll();
        Task<ServiceResponse<CartResource>> AddCart(CartModel AddedCountries);
        Task<ServiceResponse<CartResource>> GetCartById(int Id);
        Task<ServiceResponse<CartResource>> CartCheckOut(int Id);
       Task<ServiceResponse<List<CartResource>>> GetUserCarts(int UserId);

        Task<ServiceResponse<CartResource>> GetUserCartCheckOut(int UserId);

        Task<ServiceResponse<CartResource>> AddItemToCart(AddItemModel AddedItem);

        Task<ServiceResponse<CartItemResource>> DeleteCartItem(int CartId, int ItemId);



    }
}
