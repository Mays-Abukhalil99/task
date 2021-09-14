using onlineShop.Model;
using onlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Services.Inventory
{
    public interface IInventoryService
    {
        Task<List<InventoryResource>> GetAll();
        Task<InventoryResource> GetItemById(int Id);
        Task<InventoryResource> AddItem(InventoryModel AddedItem);
        Task<InventoryResource> UpdateItem(InventoryModel UpdatedItem, int Id);
        Task<InventoryResource> DeleteItem(int Id);
      

    }
}
