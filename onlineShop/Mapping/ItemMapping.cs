using onlineShop.Entity;
using onlineShop.Model;
using onlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Mapping
{
    public static class ItemMapping
    {

        public static InventoryEntity MapModelToEntityItem (this AddItemModel model, int? Id)
        {
            if (model == null)
                return null;
            return new InventoryEntity()
            {
                Id = Id ?? 0,
                CartId = model.CartId,
                
                
            };
        }
    
        public static InventoryResource MapEntityToResourceItem(this InventoryEntity entity)
        {
            if (entity == null)
                return null;
            return new InventoryResource()
            {
                Id = entity.Id,
                Name = entity.Name,
                UnitPrice = entity.UnitPrice,
                AvailableStock = entity.AvailableStock,
                CartId = entity.CartId,
            };
        }
    }
}
