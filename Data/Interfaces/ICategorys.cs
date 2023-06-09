using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shop.Data.Models;

namespace shop.Data.Interfaces
{
    public interface ICategorys
    {
        public IEnumerable<Categorys> AllCategorys { get; }
        public int Add(Categorys categorys);
        public void Update(Categorys categorys);
        public void Delete(Categorys categorys);
        public IEnumerable<Categorys> FindCategorys(string searchTerm);
        public Categorys GetCategoryById(int id);
    }
}
