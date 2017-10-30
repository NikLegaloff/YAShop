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
using ImageStore.Domain;

namespace ImageStore
{
    public class ImageStoreRepository
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string connectionString =
                "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\Visual Studio 2017\\Projects\\YAShop\\ImageStore\\App_Data\\ImageStore.mdf';Integrated Security = True"
            ;
        public void AddFolder(string name, string path)
        {
            var folder = new Folder
            {
                Name = name,
                ParentId = PathToParentId(path)
            };
            IDbConnection dbConnection=new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO Folders (Id, ParentId, Name) VALUES (@Id, @ParentID, @Name)";
            dbConnection.Query<Folder>(sqlQuery, folder);
        }

        public Folder GetFolder(string name, Guid? parentId)
        {
            string sqlQuery= "SELECT * FROM Folders WHERE Name=@name and ParentId";
            if (parentId == null) sqlQuery += " is null";
            else sqlQuery += "=@parentId";
            IDbConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection.Query<Folder>(sqlQuery, new {name, parentId}).FirstOrDefault();
        }

        public Guid? PathToParentId(string path)
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

        public void UploadImage(byte[] data, string fileName, string folder)
        {
            //upload to local disk
            var uploadDir = $"" + fileName.Substring(0, 2) + "\\" + fileName.Substring(2, 2) + "\\";
            var fullFileName=$""+uploadDir + fileName + ".jpg";
            DirectoryInfo dirInfo = new DirectoryInfo(uploadDir);
            if (!dirInfo.Exists) dirInfo.Create();
            File.WriteAllBytes(fullFileName,data);

            //write to database
            var image = new Image
            {
               Name = fileName,
               Folder = (Guid) PathToParentId(folder)
            };
            IDbConnection dbConnection = new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO Images (Id, Folder, Name) VALUES (@Id, @Folder, @Name)";
            dbConnection.Query<Image>(sqlQuery, image);
        }

    }
}
