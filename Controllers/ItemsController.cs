using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shop.Data.Interfaces;
using shop.Data.ViewModell;
using shop.Data.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using shop.Data.ViewModel;
using shop.Helpers;

namespace shop.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private IItems IAllItems;
        private ICategorys IAllCategorys;
        private readonly IItems _itemsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        VMItems VMItems = new VMItems();
        public ItemsController(IItems IAllItems, ICategorys IAllCategorys,IHostingEnvironment environment, IItems itemsRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
            this.hostingEnvironment = environment;
            _itemsRepository = itemsRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public ActionResult Basket(int idItem = -1)
        {
            if (idItem != -1)
            {
                Startup.BasketItem.Add(new ItemBasket(1, IAllItems.AllItems.Where(x => x.Id == idItem).First()));
            }
            ////int totalItemCount = Startup.BasketItem.Sum(x => x.Count);
            ////ViewBag.TotalItemCount = totalItemCount;
            //int totalItemCount = Startup.BasketItem.Sum(x => x.Count);
            //_httpContextAccessor.HttpContext.Session.SetItemCount(totalItemCount);
            return Json(Startup.BasketItem);
        }
        public IActionResult BasketList()
        {
            var viewModel = new BasketViewModell
            {
                BasketItems = Startup.BasketItem
            };

            return View(viewModel);
        }
        public ActionResult BasketCount(int idItem = -1, int count = -1)
        {
            
            if (Startup.BasketItem == null)
            {
                Startup.BasketItem = new List<ItemBasket>();
            }
            if (idItem != -1)
            {
                if (count == 0)
                {
                    Startup.BasketItem.Remove(Startup.BasketItem.Find(x => x.Id == idItem));
                    _httpContextAccessor.HttpContext.Session.SetItemCount(Startup.BasketItem.Count);
                }
                else
                {
                    Startup.BasketItem.Find(x => x.Id == idItem).Count = count;
                    _httpContextAccessor.HttpContext.Session.SetItemCount(Startup.BasketItem.Count);
                }
            }
            int totalItemCount = Startup.BasketItem.Sum(x => x.Count);
            _httpContextAccessor.HttpContext.Session.SetItemCount(totalItemCount);
            return Json(Startup.BasketItem);
            
        }
        public IActionResult AddToBasket(int itemId)
        {
            var item = _itemsRepository.GetItemById(itemId);

            if (item != null)
            {
                Startup.BasketItem.Add((ItemBasket)item);
                _httpContextAccessor.HttpContext.Session.SetItemCount(Startup.BasketItem.Count);
            }
            _httpContextAccessor.HttpContext.Session.SetItemCount(Startup.BasketItem.Count);
            return RedirectToAction("Index", "Home");
        }
        public ViewResult List(int id=0, int sort=0)
        {
            ViewBag.Title = "Страница с предметами";
            // Получаем все элементы
            IEnumerable<Items> items = IAllItems.AllItems;
            // Если выбрана категория, то фильтруем по категории
            if (id != 0)
            {
                items = items.Where(i => i.Category.Id == id);
            }
            // Сортировка по цене
            switch (sort)
            {
                case 1: // по возрастанию
                    items = items.OrderBy(i => i.Price);
                    break;
                case 2: // по убыванию
                    items = items.OrderByDescending(i => i.Price);
                    break;
                default: // не сортировать
                    break;
            }
            VMItems.Items = items;
            VMItems.Categorys = IAllCategorys.AllCategorys;
            VMItems.SelectCategory = id;
            VMItems.sortir = sort;
            return View(VMItems);
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
            Items item = IAllItems.GetItemById(id);

            // Создание экземпляра модели представления и установка данных
            UpdateItemViewModel viewModel = new UpdateItemViewModel
            {
                Item = item,
                Categories = IAllCategorys.AllCategorys
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
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory)
        {
            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = files.FileName;
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categorys() { Id = idCategory };
            int id = IAllItems.Add(newItems);
            return Redirect("/Items/Update?id=" + id);
        }
        
        [HttpPost]
        public RedirectResult Update(int id, string name, string description, IFormFile files, float price, int idCategory)
        {
            //// Получаем предмет по его идентификатору
            Items item = IAllItems.GetItemById(id);
            
            // Обновляем данные предмета
            item.Name = name;
            item.Description = description;
            item.Price = Convert.ToInt32(price);
            item.Category = new Categorys { Id = idCategory };

            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
                item.Img = files.FileName;
            }

            // Сохраняем обновленные данные предмета
            IAllItems.Update(item);

            return Redirect("/Items/List");
        }
        
        [HttpPost]
        public RedirectResult Delete(int id)
        {
            Items item = IAllItems.GetItemById(id);
            item.Id = id;
            // Call the Delete method from the IItems interface
            IAllItems.Delete(item);

            return Redirect("/Items/List");
        }
        



    }
}
