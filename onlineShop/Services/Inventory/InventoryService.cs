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

namespace onlineShop.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly DataContext _Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InventoryService(DataContext Context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _Context = Context;
        }
        public async Task<ServiceResponse<InventoryResource>> AddItem(InventoryModel AddedItem)
        {
            var ServiceResponse = new ServiceResponse<List<InventoryResource>>();
            var dbCarts = await _Context.Carts.FirstOrDefaultAsync(x => x.Id == AddedItem.CartId);
            if (dbCarts == null)
            {
                return getServiceResponse<InventoryResource>(false, "Cart does not exist");
            }
            var entity = AddedItem.MapInventoryModelToEntity(0);
            _Context.Inventories.Add(entity);
            await _Context.SaveChangesAsync();
            var addedEntity = await _Context.Inventories
             .Include(c => c.CartItemss)
            .FirstOrDefaultAsync(c => c.Id == entity.Id);
            var result = getServiceResponse(true, "Item added Succsessfully:", addedEntity.MapEntityToResource());
            return result;
        }
        public async Task<ServiceResponse<List<InventoryResource>>> GetAll()
        {
            var dbInventory = await _Context.Set<InventoryEntity>().ToListAsync();
            if (dbInventory == null)
            {
                return getServiceResponse<List<InventoryResource>>(false, "No Items");
            }
            var result = getServiceResponse(true, "All Items:", dbInventory.ToList().Select(item => item.MapEntityToResource()).ToList());
            return result;
        }
        public async Task<ServiceResponse<InventoryResource>> GetItemById(int Id)
        {
            var ServiceResponse = new ServiceResponse<InventoryResource>();
            var dbInventory = await _Context.Inventories
             .Include(c => c.CartItemss )
            .FirstOrDefaultAsync(c => c.Id == Id);
            if (dbInventory == null)
            {
                return getServiceResponse<InventoryResource>(false, "Item does not exist");
            }
            var result = getServiceResponse(true, "Item with id you enterd :", dbInventory.MapEntityToResource());
            return result;
        }
        public async Task<ServiceResponse<InventoryResource>> UpdateItem(InventoryModel UpdatedItem, int Id)
        {
            var dbInventory = await _Context.Inventories.Include(c => c.CartItemss).AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
            if (dbInventory == null)
            {
                return getServiceResponse<InventoryResource>(false, "Item does not exist");
            }
            dbInventory = UpdatedItem.MapInventoryModelToEntity(Id);
            _Context.Update(dbInventory);
            await _Context.SaveChangesAsync();
            dbInventory = await _Context.Inventories.Include(c => c.CartItemss).FirstOrDefaultAsync(c => c.Id == Id);
            var result = getServiceResponse(true, "Item Updated Succsessfully", dbInventory.MapEntityToResource());
            return result;
        }
        public async Task<ServiceResponse<InventoryResource>> DeleteItem(int Id)
        {
            var ServiceResponse = new ServiceResponse<InventoryResource>();
            var dbInventory = await _Context.Inventories.FirstOrDefaultAsync(c => c.Id == Id);
            if (dbInventory == null)
            {
                return getServiceResponse<InventoryResource>(false, "Item does not exist");
            }
            _Context.Inventories.Remove(dbInventory);
            await _Context.SaveChangesAsync();
            var result = getServiceResponse(true, "Item deleted Succsessfully", dbInventory.MapEntityToResource());
            return result;
        }
        private static ServiceResponse<T> getServiceResponse<T>(bool status, string errorMessage, T data = null) where T : class
        {
            var ServiceResponse = new ServiceResponse<T>();
            ServiceResponse.Success = status;
            ServiceResponse.Message = errorMessage;
            ServiceResponse.Data = data;
            return ServiceResponse;
        }
    }
}
