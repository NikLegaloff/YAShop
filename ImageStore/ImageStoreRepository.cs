using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Drawing;
using Sprut.Domain;

namespace Sprut
{
    public class ImageStoreRepository
    {
        private string uploadDirPrefix = "e:\\Visual Studio 2017\\Projects\\YAShop\\Sprut.StoreAdmin\\Content\\Images\\";
        public void AddFolder(string name, string path)
        {
            var parentId = PathToParentId(path);
            if (GetFolder(name, parentId)!=null) return;
            var folder = new Folder
            {
                Name = name,
                ParentId = parentId
            };
            using (var db = Db.Open())
            {
                var sqlQuery = "INSERT INTO Folders (Id, ParentId, Name) VALUES (@Id, @ParentID, @Name)";
                db.Query<Folder>(sqlQuery, folder);
            }
        }
        public Folder GetFolder(string name, Guid? parentId)
        {
            using (var db = Db.Open())
            {
                string sqlQuery = "SELECT * FROM Folders WHERE Name=@name and ParentId";
                //TODO refactoring required
                if (parentId == null) sqlQuery += " is null";
                else sqlQuery += "=@parentId";
                return db.Query<Folder>(sqlQuery, new {name, parentId}).FirstOrDefault();
            }
        }
        public List<Domain.Image> GetImagesInFolder(Guid? folderId)
        {
            using (var db = Db.Open())
            {
                string sqlQuery = "SELECT * FROM Images WHERE Folder=@folderId";
                if (folderId == null) folderId = Guid.Empty;
                return db.Query<Domain.Image>(sqlQuery, new { folderId }).ToList();
            }
        }
        public List<Folder> GetSubFolders(Guid? folderId)
        {
            using (var db = Db.Open())
            {
                string sqlQuery = "SELECT * FROM Folders WHERE ParentId";
                //TODO refactoring required
                if (folderId == null || folderId==Guid.Empty) sqlQuery += " is null";
                else sqlQuery += "=@folderId";
                return db.Query<Folder>(sqlQuery, new { folderId }).ToList();
            }
        }
        private Guid? PathToParentId(string path)
        {
            if (path == "") return null;
            var fullPathList = path.Split('\\').ToList();
            Guid? parentFolderId = null;
            foreach (var folder in fullPathList)
            {
                Folder rFolder= GetFolder(folder.Trim(), parentFolderId);
                if (rFolder == null) return null;
                parentFolderId = rFolder.Id;
            }
            return parentFolderId;
        }
        public Guid UploadImage(byte[] data, string fileNameOrigin, string folder)
        {
            //upload image to local disk
            var fileName = Guid.NewGuid().ToString();
            var uploadDir = $""+uploadDirPrefix + fileName.Substring(0, 2) + "\\" + fileName.Substring(2, 2) + "\\";
            var fullFileName=$""+uploadDir + fileName + ".jpg";
            DirectoryInfo dirInfo = new DirectoryInfo(uploadDir);
            if (!dirInfo.Exists) dirInfo.Create();
            File.WriteAllBytes(fullFileName,data);

            //Thumbnail
            System.Drawing.Image.GetThumbnailImageAbort myCallback =
                new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap bitmap;
            using (var ms = new MemoryStream(data))
            {
                bitmap = new Bitmap(ms);
            }
            System.Drawing.Image thumbnail = bitmap.GetThumbnailImage(
                50, bitmap.Height * 50 / bitmap.Width, myCallback, IntPtr.Zero);
            using (var ms = new MemoryStream())
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                fullFileName = $"" + uploadDir + fileName + "_tmb.jpg";
                File.WriteAllBytes(fullFileName, ms.ToArray());
            }

            //write image to database (Thumbnail)
            var fileNameShort = fileNameOrigin.Split('\\').LastOrDefault();
            Guid imageFolder;
            if (folder == "") imageFolder = Guid.Empty;
            else imageFolder = (Guid) PathToParentId(folder);
            
            var image = new Domain.Image
            {
                Id=Guid.Parse(fileName),
                Name = fileNameShort,
                Folder = imageFolder
            };
            using (var db = Db.Open())
            {
                var sqlQuery = "INSERT INTO Images (Id, Folder, Name) VALUES (@Id, @Folder, @Name)";
                db.Query<Domain.Image>(sqlQuery, image);
            }
            return Guid.Parse(fileName);
        }
        private bool ThumbnailCallback()
        {
            return false;
        }
        private string GetBaseUrl(Guid id)
        {
            var fileName = id.ToString();
            //return $""+ uploadDirPrefix + fileName.Substring(0, 2) + "\\" + fileName.Substring(2, 2) + "\\";
            //for debuging
            return $"/Content/Images/" + fileName.Substring(0, 2) + "/" + fileName.Substring(2, 2) + "/";
        }
        public string GetImageUrl(Guid id)
        {
            return GetBaseUrl(id) + id + ".jpg";
        }
        public string GetTmbUrl(Guid id)
        {
            return GetBaseUrl(id) + id + "_tmb.jpg";
        }
    }
}
