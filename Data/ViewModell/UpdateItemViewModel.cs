using shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Data.ViewModell
{
    public class UpdateItemViewModel
    {
        public Items Item { get; set; }
        public Categorys Categorys { get; set; }
        public IEnumerable<Categorys> Categories { get; set; }
    }
}
