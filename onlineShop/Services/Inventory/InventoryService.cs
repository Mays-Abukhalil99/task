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
        public async Task<InventoryResource> AddItem(InventoryModel AddedItem)
        {

            var ServiceResponse = new ServiceResponse<List<InventoryResource>>();
            var flag = await _Context.Carts.FirstOrDefaultAsync(x => x.Id == AddedItem.CartId);
            if (flag == null)
                throw new Exception($"Cart does not exist");
            var entity = AddedItem.MapInventoryModelToEntity(0);
            _Context.Inventories.Add(entity);
            await _Context.SaveChangesAsync();
            var addedEntity = await _Context.Inventories
             .Include(c => c.Cartss)
            .FirstOrDefaultAsync(c => c.Id == entity.Id);
            return addedEntity.MapEntityToResource();

        }

        public async Task<List<InventoryResource>> GetAll()
        {
            var dbInventory = await _Context.Set<InventoryEntity>().ToListAsync();
            return dbInventory.ToList().Select(item => item.MapEntityToResource()).ToList();
        }

        public async Task<InventoryResource> GetItemById(int Id)
        {
            var ServiceResponse = new ServiceResponse<InventoryResource>();
            var dbInventory = await _Context.Inventories
             .Include(c => c.Cartss)
            .FirstOrDefaultAsync(c => c.Id == Id);
            return dbInventory.MapEntityToResource();
        }



        public async Task<InventoryResource> UpdateItem(InventoryModel UpdatedItem, int Id)
        {
            var dbInventory = await _Context.Inventories.Include(c => c.Cartss).AsNoTracking().FirstOrDefaultAsync(c => c.Id == Id);
            if (dbInventory == null)
                throw new Exception($"Item with id {Id} does not exist");
            dbInventory = UpdatedItem.MapInventoryModelToEntity(Id);
            _Context.Update(dbInventory);
            await _Context.SaveChangesAsync();
            dbInventory = await _Context.Inventories.Include(c => c.Cartss).FirstOrDefaultAsync(c => c.Id == Id);
            return dbInventory.MapEntityToResource();
        }


        public async Task<InventoryResource> DeleteItem(int Id)
        {

            var ServiceResponse = new ServiceResponse<InventoryResource>();
            var dbInventory = await _Context.Inventories.FirstOrDefaultAsync(c => c.Id == Id);
            if (dbInventory != null)
            {
                _Context.Inventories.Remove(dbInventory);
                await _Context.SaveChangesAsync();
                return dbInventory.MapEntityToResource();
            }
            else
            {
                throw new Exception($"Item with id {Id} does not exist");
            }
        }

    }
}
