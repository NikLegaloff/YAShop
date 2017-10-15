using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sprut.MyShop.Domain;
using Sprut.StoreAdmin.Models;
using Sprut.MyShop.Infrastructure;

namespace Sprut.StoreAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryViewModels _cvModel=new CategoryViewModels();

        public object MessageBox { get; private set; }

        [HttpGet]
        public ActionResult Index(Guid? id)
        {
            if (id!=null)
            {
                _cvModel.Category = Registry.Current.Categories.Find((Guid)id);
                _cvModel.CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName", _cvModel.Category.ParentId);
                return View(_cvModel);
            }
            return View(_cvModel);
        }

        [HttpPost]
        public ActionResult Index(CategoryViewModels model)
        {
            Category category;
            if (Registry.Current.Categories.Find(model.Category.Id) != null)
            {
                category = Registry.Current.Categories.Find(model.Category.Id);
                category.Name = model.Category.Name;
                category.ParentId = model.Category.ParentId;
            }
            else
            {
                category = new Category();
                category.Name = model.Category.Name;
                category.ParentId = model.Category.ParentId;
            }
            Registry.Current.Categories.Save(category);
            return Redirect("/Category/Index");
        }

        public ActionResult Delete(Guid id)
        {
            if (Registry.Current.Categories.Select("WHERE ParentId=@parentId", new {parentId = id}).Count > 0)
            {
                Response.Write("<script>window.alert('ku-ku');</script>");
                return Redirect("/Category/Index");
            }
            Category category = Registry.Current.Categories.Find(id);
            Registry.Current.Categories.Delete(category);
            return Redirect("/Category/Index");

        }

    }
}