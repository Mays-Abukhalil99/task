using onlineShop.Entity;
using onlineShop.Model;
using onlineShop.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Mapping
{
    public static class UserMapping
    {
        public static UserEntity MapModelToEntity(this UserModel model, int? Id)
        {
            if (model == null)
                return null;
            return new UserEntity()
            {
                Id = Id ?? 0,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
            };
        }
        public static UserResource MapEntityToResource(this UserEntity entity)
        {
            if (entity == null)
                return null;
            return new UserResource()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
            };
        }
    }
}
