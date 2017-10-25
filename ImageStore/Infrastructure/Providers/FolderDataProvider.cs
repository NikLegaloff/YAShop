using System;
using System.Collections.Generic;
using System.Linq;
using ImageStore.Domain;
using ImageStore.Domain.Service;
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
            //TODO need check or not ???
            //if (string.IsNullOrEmpty(path) && ImageStoreRegistry.Current.Folders
            //        .Select($"where Name='" + name.Trim() + "' and ParentId is null").Count >
            //    0) return;
            var servive = new FolderService();
            var folder=new Folder
            {
                Name = name,
                ParentId= servive.PathToParentId(path)
            };
            ImageStoreRegistry.Current.Folders.Save(folder);
        }
    }
}