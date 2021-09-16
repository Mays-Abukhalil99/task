using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Entity
{
    public class InventoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int AvailableStock { get; set; }
        public List<CartEntity> Cartss { get; set; }
        public int CartId { get; set; }
        public List<CartItemEntity> CartItemss { get; set; }

    }
}
