using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using shop.Data.Interfaces;
using shop.Data.Models;
using shop.Data.ViewModell;

namespace shop.Controllers
{
    public class CategorysController : Controller
    {
        private IItems IAllItems;
        private ICategorys IAllCategorys;
        VMCategorys VMCategorys=new VMCategorys();
        public CategorysController(IItems IAllItems, ICategorys IAllCategorys)
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
        }
        public ViewResult List(string searchTerm = "")
        {
            ViewBag.Title = "Страница с категориями";
            var cars = IAllCategorys.AllCategorys;
            IEnumerable<Categorys> categorys = IAllCategorys.AllCategorys;
            IEnumerable<Categorys> search = IAllCategorys.FindCategorys(searchTerm);
            VMCategorys.Categorys = categorys;
            VMCategorys.SearchTerm = search;
            return View(VMCategorys);
        }
        [HttpGet]
        public ViewResult Add()
        {
            IEnumerable<Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }
        [HttpGet]
        public ViewResult Update(int id)
        {
            Categorys category = IAllCategorys.GetCategoryById(id);

            // Создание экземпляра модели представления и установка данных
            UpdateItemViewModel viewModel = new UpdateItemViewModel
            {
                Categorys = category,
            };

            // Передача модели представления в представление
            return View(viewModel);
        }
        [HttpGet]
        public ViewResult Delete()
        {
            IEnumerable<Categorys> Categorys = IAllCategorys.AllCategorys;
            return View(Categorys);
        }
        [HttpPost]
        public RedirectResult Add(string name, string description)
        {
            Categorys newCategorys = new Categorys();
            newCategorys.Name = name;
            newCategorys.Description = description;
            int id = IAllCategorys.Add(newCategorys);
            return Redirect("/Categorys/Update?id=" + id);
        }

        [HttpPost]
        public RedirectResult Update(int id, string name, string description)
        {
            //// Получаем предмет по его идентификатору
            Categorys category = IAllCategorys.GetCategoryById(id);

            // Обновляем данные предмета
            category.Name = name;
            category.Description = description;
            // Сохраняем обновленные данные предмета
            IAllCategorys.Update(category);

            return Redirect("/Categorys/List");
        }
        //public Items GetItemById(int id)
        //{
        //    return IAllItems.AllItems.FirstOrDefault(i => i.Id == id);
        //}
        [HttpPost]
        public RedirectResult Delete(int id)
        {
            Categorys category = IAllCategorys.GetCategoryById(id);
            category.Id = id;
            // Call the Delete method from the IItems interface
            IAllCategorys.Delete(category);

            return Redirect("/Categorys/List");
        }
        //[HttpPost]
        //public RedirectResult Delete(int id)
        //{
        //    id = IAllItems.GetItemById(id).Id;
        //    // Call the Delete method from the IItems interface
        //    IAllItems.Delete(id);

        //    return Redirect("/Items/List");
        //}
    }
}
