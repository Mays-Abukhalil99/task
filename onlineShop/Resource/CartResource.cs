using onlineShop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Resource
{
    public class CartResource
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public bool CheckedOut { get; set; }
       // public UserEntity UserEntity { get; set; }
        public int UserId { get; set; }
        ///user id
    }
}
