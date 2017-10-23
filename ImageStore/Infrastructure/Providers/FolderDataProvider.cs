using System;
using System.Collections.Generic;
using System.Linq;
using ImageStore.Domain;
using Microsoft.Win32;

namespace ImageStore.Infrastructure.Providers
{
    public class FolderDataProvider : DataProvider<Folder>
    {
        public FolderDataProvider(IDataProvider<Folder> executor) : base(executor)
        {
        }

        public void AddFolder(string name, string path)
        {
            //need check or not ???
            //if (string.IsNullOrEmpty(path) && ImageStoreRegistry.Current.Folders
            //        .Select($"where Name='" + name.Trim() + "' and ParentId is null").Count >
            //    0) return;
            var folder=new Folder
            {
                Name = name,
                ParentId= PathToParentId(path)
            };
            ImageStoreRegistry.Current.Folders.Save(folder);
        }

        
        static Guid? PathToParentId(string path)
        {
            if (path == "") return null;
            var fullPathList = path.Split('\\').ToList();
            var parentFolderId = Guid.Empty;
            if (ImageStoreRegistry.Current.Folders
                    .Select($"where Name='" + fullPathList[0].Trim() + "' and ParentId is null").Count ==
                0)
            {
                return null;
            }
            parentFolderId = ImageStoreRegistry.Current.Folders
                .Select($"where Name='" + fullPathList[0].Trim() + "' and ParentId is null").First().Id;
            fullPathList.RemoveAt(0);
            foreach (var folder in fullPathList)
            {
                if (ImageStoreRegistry.Current.Folders
                        .Select($"where Name='" + folder.Trim() + "' and ParentId='" + parentFolderId + "'").Count == 0)
                {
                    return null;
                }
                parentFolderId = ImageStoreRegistry.Current.Folders
                    .Select($"where Name='" + folder.Trim() + "' and ParentId='" + parentFolderId + "'").First().Id;
            }
            return parentFolderId;
        }


    }
}