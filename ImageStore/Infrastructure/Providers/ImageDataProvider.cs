using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageStore.Domain;
using ImageStore.Domain.Service;
using Microsoft.Win32;

namespace ImageStore.Infrastructure.Providers
{
    public class ImageDataProvider : DataProvider<Image>
    {
        public ImageDataProvider(IDataProvider<Image> executor) : base(executor)
        {
        }
        public void UploadImage(byte[] data, string fileName, string folder)
        {
            //upload to local disk
            var uploadDir = $"" + fileName.Substring(0, 2) + "\\" + fileName.Substring(2, 2) + "\\";
            var fullFileName=$""+uploadDir + fileName + ".jpg";
            DirectoryInfo dirInfo = new DirectoryInfo(uploadDir);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            File.WriteAllBytes(fullFileName,data);

            //write to database
            var service = new FolderService();
            var image = new Image
            {
                Name = fileName,
                Folder = (Guid) service.PathToParentId(folder),
            };
            ImageStoreRegistry.Current.Images.Save(image);
        }




    }
}