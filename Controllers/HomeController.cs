using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace shop.Controllers
{
    public class HomeController:Controller
    {
        public RedirectResult Index()
        {
            return Redirect("/Items/List");
        }
    }
}
