using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Entity
{
    public class CartEntity
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public bool CheckedOut { get; set; }
        public UserEntity User { get; set; }
        public int UserId { get; set; }
        //public List<InventoryEntity> Inventoriess { get; set; } // remove.
        public List<CartItemEntity> CartItemss { get; set; }

    }
}
