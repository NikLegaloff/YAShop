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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        public List<Folder> GetFoldersTree()
        {
            return new FoldersHelper().SelectTree();
        }

        public List<Image> GetImages(Guid? folderId)
        {
            var images = new List<Image>();
            using (var db = DB.Open())
            {
                var imgs =  db.Query<ImageDTO>("select * from Image where " + (folderId == null ? "folderId is null" : $"folderId=\'{folderId.Value}\'"));
                foreach (var i in imgs) images.Add(new Image {Id = i.Id,Name = i.Name,Size = i.Size});
            }
            return images;
        }

        public Guid AddFolder(string name, Guid? parentId)
        {
            var existing = DB.ExecuteScalar<Guid?>("select Id from folder where name=@name, parentId=@parentId", new {name, parentId});
            if (existing != null) return existing.Value;
            var id = Guid.NewGuid();
            DB.Execute("insert into folder(id, name, parentId) values (@id, @name, @parentId)",new {id,name,parentId});
            return id;
        }

        public Guid UploadImage(string name, byte[] data, Guid? folderId)
        {
            return new ImagesHelper().UploadFile(name, data, folderId);
        }

        public bool RenameFolder(Guid id, string newName)
        {
            var parentId = DB.ExecuteScalar<Guid?>("select parentId from folder where id=@id", new {id});
            var count = DB.ExecuteScalar<int>("select count (*) from folder where parentId=@parentId and name=@newName", new {parentId,newName});
            if (count > 0) return false;
            DB.Execute("update folder set name=@newName where id=@id", new {id, newName});
            return true;
        }

        public bool RenameImage(Guid id, string newName)
        {
            var folderId = DB.ExecuteScalar<Guid?>("select folderId from image where id=@id", new { id });
            var count = DB.ExecuteScalar<int>("select count (*) from image where folderId=@folderId and name=@newName", new { folderId, newName });
            if (count > 0) return false;
            DB.Execute("update image set name=@newName where id=@id", new { id, newName });
            return true;
        }

        public void DeleteFolder(Guid folderId, Guid? newFolderId)
        {
            DB.Execute("update image set folderId=@newFolderId where folderId=@folderId", new {folderId,newFolderId});
            DB.Execute("delete folder where id=@folderId", new {folderId});
        }

        public void DeleteImage(Guid id)
        {
            DB.Execute("delete image where id=@id", new { id });
            new ImagesHelper().DeleteImageFile(id);

        }
    }
}
