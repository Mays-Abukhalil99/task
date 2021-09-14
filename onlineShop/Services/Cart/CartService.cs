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
        public async Task<List<CartResource>> GetAll()
        {
            var dbCarts = await _Context.Set<CartEntity>().ToListAsync();
            return dbCarts.ToList().Select(item => item.MapEntityToResourceCart()).ToList();
        }
        public async Task<CartResource> AddCart(CartModel AddedCart)
        {
            var ServiceResponse = new ServiceResponse<List<CartResource>>();
            var flag = await _Context.Users.FirstOrDefaultAsync(x => x.Id == AddedCart.UserId);
            if (flag == null)
                throw new Exception($"User does not exist");
            var entity = AddedCart.MapModelToEntityCart(0); 
            _Context.Carts.Add(entity);
            await _Context.SaveChangesAsync();
            var addedEntity = await _Context.Carts
             .Include(c => c.UserEntity)
            .FirstOrDefaultAsync(c => c.Id == entity.Id);
            return addedEntity.MapEntityToResourceCart(); 
        }
        public async Task<CartResource> GetCartById(int Id)
        {
            var ServiceResponse = new ServiceResponse<CartResource>();
            var dbCarts = await _Context.Carts
             .Include(c => c.UserEntity)
            .FirstOrDefaultAsync(c => c.Id == Id);
            return dbCarts.MapEntityToResourceCart();
        }

        public async Task<CartResource> GetUserCarts(int UserId)
        {
            var ServiceResponse = new ServiceResponse<CartResource>();
            var dbCarts = await _Context.Carts
             .Include(c => c.UserEntity)
            .FirstOrDefaultAsync(c => c.UserId ==UserId);
            return dbCarts.MapEntityToResourceCart();
        }
        public async Task<CartResource> GetUserCartCheckOut(int UserId)
        {
            var ServiceResponse = new ServiceResponse<CartResource>();
            var dbCarts = await _Context.Carts
                .Include(c => c.UserEntity)
                .FirstOrDefaultAsync(c => c.UserId == UserId);
            if (dbCarts.CheckedOut == false)
            {
                return dbCarts.MapEntityToResourceCart();
            }
            return null;
        }

        public async Task<CartResource> CartCheckOut(int Id)
        {
            var dbCarts = await _Context.Carts.Include(c => c.UserEntity).AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
            if (dbCarts == null)
                throw new Exception($"Cart with id {Id} does not exist");
            // dbCarts = UpdatedCartCheckedOut.MapModelToEntityCart(Id);
            dbCarts.CheckedOut = true;
            //mark checkedout as true;
            _Context.Update(dbCarts);
            await _Context.SaveChangesAsync();
            dbCarts = await _Context.Carts.FirstOrDefaultAsync(c => c.Id == Id);
            return dbCarts.MapEntityToResourceCart();
        }


    }
}
