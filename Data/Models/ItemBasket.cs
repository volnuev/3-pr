using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Data.Models
{
    public class ItemBasket:Items
    {
        public int Count { get; set; }
        public ItemBasket(int Count, Items item) : base(item)
        {
            this.Count = Count;
        }
    }
}
