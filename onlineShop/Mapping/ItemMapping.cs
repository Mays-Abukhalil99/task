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
        public static CartItemEntity MapModelToEntityItem (this AddItemModel model, int? Id)
        {
            if (model == null)
                return null;
            return new CartItemEntity()
            {
                Id = Id ?? 0,
                CartId = model.CartId,
                ItemId = model.ItemId,
                Count = model.Count,
            };
        }
        public static CartItemResource MapEntityToResourceItem(this CartItemEntity entity)
        {
            if (entity == null)
                return null;
            return new CartItemResource()
            {
                Id = entity.Id,
                Count = entity.Count,
               
            };
        }
    }
}
