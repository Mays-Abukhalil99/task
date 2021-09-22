using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using onlineShop.Data;
using onlineShop.Entity;
using onlineShop.Mapping;
using onlineShop.Model;
using onlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace onlineShop.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly DataContext _Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(DataContext Context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _Context = Context;
        }
        public async Task<ServiceResponse<List<CartResource>>> GetAll()
        {
            var dbCarts = await _Context.Set<CartEntity>().ToListAsync();
            var result = getServiceResponse(true, "All Carts :", dbCarts.ToList().Select(item => item.MapEntityToResourceCart()).ToList());
            return result;
        }
        public async Task<ServiceResponse<CartResource>> AddCart(CartModel AddedCart)
        {
            var ServiceResponse = new ServiceResponse<List<CartResource>>();
            var dbUser = await _Context.Users
                .Include( c => c.Cartss)
                .FirstOrDefaultAsync(x => x.Id == AddedCart.UserId);
            if (dbUser == null)
            {
                return getServiceResponse<CartResource>(false, "User does not exist");
            }
            var userHasOpenCart = dbUser.Cartss.Any(c => c.CheckedOut == false);
            if (userHasOpenCart)
            {
                return getServiceResponse<CartResource>(false, "User already have cart"); 
            }
            var entity = AddedCart.MapModelToEntityCart(0); 
            _Context.Carts.Add(entity);
            await _Context.SaveChangesAsync();
            var addedEntity = await _Context.Carts
             .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == entity.Id);
            var result = getServiceResponse(true, "Cart added Succesfully", addedEntity.MapEntityToResourceCart());
            return result;
        }
        public async Task<ServiceResponse<CartResource>> GetCartById(int Id)
        {
            var ServiceResponse = new ServiceResponse<CartResource>();
            var dbCarts = await _Context.Carts
             .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == Id);
            if (dbCarts == null)
            {
                return getServiceResponse<CartResource>(false, "Cart does not exist");

            }
            var result = getServiceResponse(true, "Cart for id you entered:", dbCarts.MapEntityToResourceCart());
            return result;
        }

        public async Task<ServiceResponse<List<CartResource>>> GetUserCarts(int UserId)
        {
            var ServiceResponse = new ServiceResponse<List<CartResource>>();
            var dbCarts = await _Context.Carts
            .Where(c => c.UserId == UserId).ToListAsync();
            var resources = dbCarts.Select(c => c.MapEntityToResourceCart()).ToList();
            var result = getServiceResponse(true, "User Carts:", resources);
            return result;
        }

     

        public async Task<ServiceResponse<CartResource>> GetUserCartCheckOut(int UserId)
        {
            var ServiceResponse = new ServiceResponse<CartResource>();
            var dbCarts = await _Context.Carts
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == UserId);
            if (dbCarts.CheckedOut == false)
            {
                var result = getServiceResponse(true, "Carts CheckedOut:", dbCarts.MapEntityToResourceCart());
                return result;
            }
            return getServiceResponse<CartResource>(false, "No Carts CheckedOut");
        }

        public async Task<ServiceResponse<CartResource>> CartCheckOut(int Id)
        {
            var dbCarts = await _Context.Carts.Include(c => c.User).AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
            if (dbCarts == null)
            {
                return getServiceResponse<CartResource>(false, "Cart does not exist");
            }
            dbCarts.CheckedOut = true;
            //mark checkedout as true;
            _Context.Update(dbCarts);
            await _Context.SaveChangesAsync();
            dbCarts = await _Context.Carts.FirstOrDefaultAsync(c => c.Id == Id);
            var result = getServiceResponse(true, "Cart ChechOut Succesfully", dbCarts.MapEntityToResourceCart());
            return result;
        }
       public async Task<ServiceResponse<CartResource>> AddItemToCart(AddItemModel AddedItem)
        {
            var dbCart = await _Context.Carts.Include(x => x.CartItemss).FirstOrDefaultAsync(x => x.Id == AddedItem.CartId);
            if (dbCart == null)
            {
               return getServiceResponse<CartResource>(false, "Cart not found");
            }
            var dbItem = await _Context.Inventories.FirstOrDefaultAsync(x => x.Id == AddedItem.ItemId); 
            if (dbItem == null)
            {
                return getServiceResponse<CartResource>(false, "Item not found");
            }
            //var dbCount = await _Context.CartItems.FirstOrDefaultAsync(x => x.Count  == AddedItem.Count ); // validate that the dbItem AvailableStock is >= AddedItem.Count
            if (dbItem.AvailableStock < AddedItem.Count)
            {
                return getServiceResponse<CartResource>(false, "Item quantity is not enough");
            }
            var cartItemAlreadyAdded =  dbCart.CartItemss.Any(x => x.ItemId == AddedItem.ItemId);
            if (cartItemAlreadyAdded)
            {
                return getServiceResponse<CartResource>(false, "Item already exists in the cart");
            }
            var entity = AddedItem.MapModelToEntityItem(null);
            _Context.CartItems.Add(entity);
            await _Context.SaveChangesAsync();
            await updateCartTotalPrice(AddedItem);
            await updateAvailableAtstock(AddedItem.ItemId, AddedItem.Count);
            var addedEntity = await _Context.CartItems
            .FirstOrDefaultAsync(c => c.Id == entity.Id);
           // return addedEntity.MapEntityToResourceItem(); // remove
           // var items = dbCart.CartItemss.Select(x => x.MapEntityToResourceItem()).ToList();
            // call the new method to get service response and pass status :true, message :added succesfully, store the result in var,
            // and assign the Data field of the variable.
            // ex: var result = getServ... ()
            //result.Data = items
            //return result;
            var result = getServiceResponse(true, "Item Added Succesfully", dbCart.MapEntityToResourceCart());
            return result;
        }
        public async Task<ServiceResponse<CartItemResource> >DeleteCartItem(int CartId, int ItemId)
        {
            var ServiceResponse = new ServiceResponse<CartItemResource>();
            var dbCartItem = await _Context.CartItems.FirstOrDefaultAsync(c => c.CartId == CartId && c.ItemId == ItemId );
            if (dbCartItem == null)
            {
                return getServiceResponse<CartItemResource>(false, "Item does not exist");
            }
            _Context.CartItems.Remove(dbCartItem);
            // update total proice for cart  by: looping through the items and multiply the count of each one by its price;
            await updateCartTotalPrices(CartId);
            await updateAvailableAtstock(ItemId, dbCartItem.Count,true);
            await _Context.SaveChangesAsync();
            var result = getServiceResponse(true, "Item Added Succesfully", dbCartItem.MapEntityToResourceItem());
            return result;
        }
        private async Task updateCartTotalPrice(AddItemModel AddedItem ) // pass cart id;
        {
            var cart = _Context.Carts
                .Include(c => c.CartItemss )
                .ThenInclude( c => c.Item)
                .FirstOrDefault(cart => cart.Id == AddedItem.CartId);
            var totalPrice = 0;
            cart.CartItemss.ForEach(cartItem => totalPrice += (cartItem.Count * cartItem.Item.UnitPrice));
          //foreach(var cartItem in cart.CartItemss)
          //  {
          //      var totaltemp = cartItem.Count * cartItem.Item.UnitPrice;
          //      totalPrice += totaltemp;
          //  }
            cart.TotalPrice = totalPrice;
            await _Context.SaveChangesAsync();
        }
        private async Task updateCartTotalPrices(int CartId) 
        {
            var cart = _Context.Carts.FirstOrDefault(cart => cart.Id == CartId);
            var totalPrice = 0;
            cart.CartItemss.Select(cartItem => totalPrice += (cartItem.Count * cartItem.Item.UnitPrice));
            cart.TotalPrice = totalPrice;
            await _Context.SaveChangesAsync();
        }
        private async Task updateAvailableAtstock(int ItemId, int count, bool isDelete = false) // pass cart id; // isDelete pass as true in delete
        {
            var inventory = await _Context.Inventories.FirstOrDefaultAsync(inventory => inventory.Id == ItemId);
            //var availableStock = 0;
            //inventory.CartItemss.Where(x => x.CartId == AddedItem.CartId).Select(c => availableStock += c.Count);
            if(isDelete)
                inventory.AvailableStock += count;
            else
            inventory.AvailableStock -= count;
            await _Context.SaveChangesAsync();
        }
        private static  ServiceResponse<T> getServiceResponse<T>( bool status , string errorMessage, T data = null) where T: class
        {
            var ServiceResponse = new ServiceResponse<T>();
            ServiceResponse.Success = status;
            ServiceResponse.Message = errorMessage;
            ServiceResponse.Data = data;
            return ServiceResponse;
        }
        // create static method "getServiceResponse(status,errorMessage)" it will create new serviceResponse with the parameters provided, and return it.
        // Call this method above, and return its result.
    }
}
