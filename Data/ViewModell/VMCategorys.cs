using shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Data.ViewModell
{
    public class VMCategorys
    {
        public IEnumerable<Categorys> Categorys { get; set; }
        public IEnumerable<Categorys> SearchTerm { get; set; }
    }
}
