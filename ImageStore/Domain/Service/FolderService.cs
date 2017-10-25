using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageStore.Infrastructure;

namespace ImageStore.Domain.Service
{
    public class FolderService
    {
        public Guid? PathToParentId(string path)
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
