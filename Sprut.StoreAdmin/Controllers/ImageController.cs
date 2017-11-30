using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageStore;
using Sprut.StoreAdmin.Models;

namespace Sprut.StoreAdmin.Controllers
{
    public class ImageController : Controller
    {
        readonly ImageStoreRepository _imageStoreRepository=new ImageStoreRepository();
        public ActionResult Index(Guid? folderId)
        {
            ImagesIndexDto imagesIndexDto = new ImagesIndexDto
            {
                Folders = _imageStoreRepository.GetSubFolders(folderId),
                Images = new List<ImageExt>()
            };
            foreach (var i in _imageStoreRepository.GetImagesInFolder(folderId))
            {
                ImageExt imageExt = new ImageExt
                {
                    Image = i,
                    ImageUrl = _imageStoreRepository.GetTmbUrl(i.Id)
                };
                imagesIndexDto.Images.Add(imageExt);
            }
            return View(imagesIndexDto);
        }
    }
}