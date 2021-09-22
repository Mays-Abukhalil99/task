using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Model
{
    public class InventoryModel
    {
        public int CartId { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int AvailableStock { get; set; }
    }
}
