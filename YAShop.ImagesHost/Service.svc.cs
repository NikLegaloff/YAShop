using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dapper;
using YAShop.ImagesHost.Domain;

namespace YAShop.ImagesHost
{
    public class Service : IService
    {
        public List<Folder> GetFoldersTree(Guid contextId)
        {
            return new FoldersHelper(contextId).SelectTree();
        }

        public List<Image> GetImages(Guid? folderId, Guid contextId)
        {
            var images = new List<Image>();
            using (var db = DB.Open())
            {
                var imgs =  db.Query<ImageDTO>($"select * from Image where contextId='{contextId}' " + (folderId == null ? "folderId is null" : $"folderId=\'{folderId.Value}\'"));
                foreach (var i in imgs) images.Add(new Image {Id = i.Id,Name = i.Name,Size = i.Size});
            }
            return images;
        }

        public Guid AddFolder(string name, Guid? parentId, Guid contextId)
        {
            var existing = DB.ExecuteScalar<Guid?>("select Id from folder where contextId=@contextId and name=@name and parentId=@parentId", new {name, parentId, contextId });
            if (existing != null) return existing.Value;
            var id = Guid.NewGuid();
            DB.Execute("insert into folder(id, name, parentId,contextId) values (@id, @name, @parentId,@contextId)", new {id,name,parentId, contextId });
            return id;
        }

        public Guid UploadImage(string name, byte[] data, Guid? folderId, Guid contextId)
        {
            return new ImagesHelper(contextId).UploadFile(name, data, folderId);
        }

        public bool RenameFolder(Guid id, string newName, Guid contextId)
        {
            var parentId = DB.ExecuteScalar<Guid?>("select parentId from folder where id=@id", new {id });
            var count = DB.ExecuteScalar<int>("select count (*) from folder where contextId=@contextId and parentId=@parentId and name=@newName", new {parentId,newName, contextId });
            if (count > 0) return false;
            DB.Execute("update folder set name=@newName where id=@id", new {id, newName});
            return true;
        }

        public bool RenameImage(Guid id, string newName, Guid contextId)
        {
            var folderId = DB.ExecuteScalar<Guid?>("select folderId from image where id=@id", new { id });
            var count = DB.ExecuteScalar<int>("select count (*) from image where contextId=@contextId and folderId=@folderId and name=@newName", new { folderId, newName , contextId });
            if (count > 0) return false;
            DB.Execute("update image set name=@newName where id=@id", new { id, newName });
            return true;
        }

        public void DeleteFolder(Guid folderId, Guid? newFolderId, Guid contextId)
        {
            DB.Execute("update image set folderId=@newFolderId where contextId=@contextId and folderId=@folderId", new {folderId,newFolderId, contextId });
            DB.Execute("delete folder where id=@folderId", new {folderId});
        }

        public void DeleteImage(Guid id, Guid contextId)
        {
            DB.Execute("delete image where id=@id", new { id });
            new ImagesHelper(contextId).DeleteImageFile(id);

        }
    }
}
