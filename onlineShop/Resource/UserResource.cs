using onlineShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Resource
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CartEntity> Carts { get; set; }


    }
}
