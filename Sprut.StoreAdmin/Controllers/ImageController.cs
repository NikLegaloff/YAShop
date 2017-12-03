using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut;
using Sprut.ImageStoreClient;
using Sprut.StoreAdmin.Models;

namespace Sprut.StoreAdmin.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index(Guid? folderId)
        {
            ImagesIndexDto imagesIndexDto = new ImagesIndexDto();
            using (var imageStoreHostClient = new ImageStoreHostClient())
            {
                imagesIndexDto.Folders = imageStoreHostClient.GetSubFolders(folderId);
                imagesIndexDto.Images = new List<ImageExt>();

                foreach (var i in imageStoreHostClient.GetImagesInFolder(folderId))
                {
                    ImageExt imageExt = new ImageExt
                    {
                        Image = i,
                        ImageUrl = imageStoreHostClient.GetTmbUrl(i.Id)
                    };
                    imagesIndexDto.Images.Add(imageExt);
                }
            }
            return View(imagesIndexDto);
        }
    }
}