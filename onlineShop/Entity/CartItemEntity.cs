using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Entity
{
    public class CartItemEntity
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ItemId { get; set; } 
        public int Count { get; set; }
        public CartEntity Cart{ get; set; }
        public InventoryEntity Item{ get; set; }

    }
}
