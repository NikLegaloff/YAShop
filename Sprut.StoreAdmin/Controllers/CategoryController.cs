using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut.MyShop.Domain;
using Sprut.StoreAdmin.Models;
using Sprut.MyShop.Infrastructure;

namespace Sprut.StoreAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryViewModels _cvModel=new CategoryViewModels();
        [HttpGet]
        public ActionResult Index()
        {
          
            return View(_cvModel);
        }

        [HttpPost]
        public ActionResult Index(CategoryViewModels model)
        {
            model.Category.ParentId = Guid.Parse(model.CategorySelectList.DataValueField);
            Registry.Current.Categories.Save(model.Category);
     
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            _cvModel.Category = Registry.Current.Categories.Find(id);
            _cvModel.CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName",_cvModel.Category.ParentId);
            return Index();
        }
    }
}