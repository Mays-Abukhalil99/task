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
        Task<ServiceResponse<List<InventoryResource>>> GetAll();
        Task<ServiceResponse<InventoryResource>> GetItemById(int Id);
        Task<ServiceResponse<InventoryResource>> AddItem(InventoryModel AddedItem);
        Task<ServiceResponse<InventoryResource>> UpdateItem(InventoryModel UpdatedItem, int Id);
        Task<ServiceResponse<InventoryResource>> DeleteItem(int Id);
      

    }
}
