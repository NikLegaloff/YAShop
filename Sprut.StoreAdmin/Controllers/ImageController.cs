using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut;
using Sprut.StoreAdmin.Models;

using Sprut.StoreAdmin.ImageService;

namespace Sprut.StoreAdmin.Controllers
{
    public class ImageController : Controller
    {
        readonly ServiceClient _imageStoreRepository = new ServiceClient();
        //readonly ImageStoreRepository _imageStoreRepository=new ImageStoreRepository();
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
            _imageStoreRepository.Close();
            return View(imagesIndexDto);
        }
    }
}