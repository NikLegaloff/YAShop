using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace YAShop.ImagesHost.Domain
{
    public class FoldersHelper
    {
        private List<FolderDTO> _all;

        public List<Folder> SelectTree()
        {
            using (var db = DB.Open())
            {
                _all = db.Query<FolderDTO>("select * from Folder").ToList();
                var tree = new List<Folder>();
                tree.AddRange(_all.Where(a => a.ParentId == null).Select(GetFolderFor));
                return tree;
            }
        }

        private Folder GetFolderFor(FolderDTO a)
        {
            return new Folder
            {
                Id = a.Id,
                Name = a.Name,
                Childrens = _all.Where(_ => _.ParentId == a.Id).Select(GetFolderFor).ToList()
            };
        }
    }

    public class FolderDTO
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }

    public class ImageDTO
    {
        public Guid Id { get; set; }
        public Guid? FolderId { get; set; }
        public string Name { get; set; }
        public Guid Hash { get; set; }
        public int Size { get; set; }
    }
}