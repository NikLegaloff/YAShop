using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageStore;

namespace Sprut.StoreAdmin.Controllers
{
    public class ImageController : Controller
    {
        ImageStoreRepository imageStoreRepository=new ImageStoreRepository();
        public ActionResult Index(Guid? folderId)
        {
            ViewBag.Folders = imageStoreRepository.GetSubFolders(folderId);
            ViewBag.Images = imageStoreRepository.GetImagesInFolder(folderId);
            return View();
        }
    }
}