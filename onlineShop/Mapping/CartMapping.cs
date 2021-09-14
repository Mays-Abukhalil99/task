using onlineShop.Entity;
using onlineShop.Model;
using onlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Mapping
{
    public static class CartMapping
    {
        public static CartEntity MapModelToEntityCart(this CartModel model, int? Id)
        {
            if (model == null)
                return null;
            return new CartEntity()
            {
                Id = Id ?? 0,
                UserId = model.UserId,
            };
        }
        public static CartEntity MapModelToEntityCarts(this CartModel model)
        {
            if (model == null)
                return null;
            return new CartEntity()
            {
                UserId = model.UserId,
            };
        }
        public static CartResource MapEntityToResourceCart(this CartEntity entity)
        {
            if (entity == null)
                return null;
            return new CartResource()
            {
                Id = entity.Id,
                TotalPrice = entity.TotalPrice,
                CheckedOut = entity.CheckedOut,
                UserId = entity.UserId,
            };
        }
        public static CartEntity MappingCartResourceToCartEntity(this CartResource cartResource)
        {
            if (cartResource == null)
            {
                return null;
            }
            return new CartEntity
            {
                TotalPrice = cartResource.TotalPrice,
                CheckedOut = cartResource.CheckedOut,
                UserId = cartResource.UserId,
            };
        }
    }
}
