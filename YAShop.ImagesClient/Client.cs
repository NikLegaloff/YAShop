using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using YAShop.ImagesClient.Images;



namespace YAShop.ImagesClient
{
    public  class Client 
    {
        private ServiceClient _service;
        private string _baseUrl;

        public string GetImageUrl(Guid imageId)
        {
            var str = imageId.ToString();
            return $"{_baseUrl}/Img/{str.Substring(0, 2)}/{str.Substring(2, 4)}/{imageId}.jpg"; 
        }
        public string GetTmbUrl(Guid imageId)
        {
            var str = imageId.ToString();
            return $"{_baseUrl}/Img/{str.Substring(0, 2)}/{str.Substring(2, 4)}/{imageId}_tmb.jpg"; 
        }


        public Client(string baseUrl)
        {
            _baseUrl = baseUrl.Trim('/');
            _service=new ServiceClient(new BasicHttpBinding(),new EndpointAddress(_baseUrl + "/Service.svc"));
        }

        public Folder[] GetFoldersTree()
        {
            return _service.GetFoldersTree();
        }

        public Task<Folder[]> GetFoldersTreeAsync()
        {
            return _service.GetFoldersTreeAsync();
        }

        public Image[] GetImages(Guid? folderId)
        {
            return _service.GetImages(folderId);
        }

        public Task<Image[]> GetImagesAsync(Guid? folderId)
        {
            return _service.GetImagesAsync(folderId);
        }

        public Guid AddFolder(string name, Guid? parentId)
        {
            return _service.AddFolder(name, parentId);
        }

        public Task<Guid> AddFolderAsync(string name, Guid? parentId)
        {
            return _service.AddFolderAsync(name, parentId);
        }

        public Guid UploadImage(string name, byte[] data, Guid? folderId)
        {
            return _service.UploadImage(name, data, folderId);
        }

        public Task<Guid> UploadImageAsync(string name, byte[] data, Guid? folderId)
        {
            return _service.UploadImageAsync(name, data, folderId);
        }

        public bool RenameFolder(Guid folderId, string newName)
        {
            return _service.RenameFolder(folderId, newName);
        }

        public Task<bool> RenameFolderAsync(Guid folderId, string newName)
        {
            return _service.RenameFolderAsync(folderId, newName);
        }

        public bool RenameImage(Guid imageId, string newName)
        {
            return _service.RenameImage(imageId, newName);
        }

        public Task<bool> RenameImageAsync(Guid imageId, string newName)
        {
            return _service.RenameImageAsync(imageId, newName);
        }

        public void DeleteFolder(Guid folderId, Guid? newFolderId)
        {
            _service.DeleteFolder(folderId, newFolderId);
        }

        public Task DeleteFolderAsync(Guid folderId, Guid? newFolderId)
        {
            return _service.DeleteFolderAsync(folderId, newFolderId);
        }

        public void DeleteImage(Guid imageId)
        {
            _service.DeleteImage(imageId);
        }

        public Task DeleteImageAsync(Guid imageId)
        {
            return _service.DeleteImageAsync(imageId);
        }
    }
}
