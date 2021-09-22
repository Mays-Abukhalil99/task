using onlineShop.Entity;
using onlineShop.Model;
using onlineShop.Resource;

namespace onlineShop.Mapping
{
    public static class InventoryMapping
    {

        public static InventoryEntity MapInventoryModelToEntity(this InventoryModel model, int? Id)
        {
            if (model == null)
                return null;
            return new InventoryEntity()
            {
                Id = Id ?? 0,
                //CartId = model.CartId,
                Name = model.Name,
                UnitPrice = model.UnitPrice,
                AvailableStock = model.AvailableStock,
               
            };
        }
        public static InventoryResource MapEntityToResource(this InventoryEntity entity)
        {
            if (entity == null)
                return null;
            return new InventoryResource()
            {
                Id = entity.Id,
                Name = entity.Name,
                UnitPrice = entity.UnitPrice,
                AvailableStock = entity.AvailableStock,
              //  CartId = entity.CartId,
            };
        }
    }
}
