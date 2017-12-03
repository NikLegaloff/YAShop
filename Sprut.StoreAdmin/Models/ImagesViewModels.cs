using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sprut.ImageStoreClient.ImageService;

namespace Sprut.StoreAdmin.Models
{
    public class ImagesViewModels
    {

    }

    public class ImagesIndexDto
    {
        public List<ImageStoreClient.ImageService.Folder> Folders;
        public List<ImageExt> Images;
    }

    public class ImageExt
    {
        public Image Image;
        public string ImageUrl;
    }
}