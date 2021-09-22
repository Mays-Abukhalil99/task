using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Model
{
    public class AddItemModel
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
    }
}
