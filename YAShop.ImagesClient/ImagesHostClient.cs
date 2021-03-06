﻿using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using YAShop.ImagesClient.Images;

namespace YAShop.ImagesClient
{
    public class ImagesHostClient
    {
        private readonly Guid _contextId;
        private readonly string _baseUrl;
        private readonly ServiceClient _service;


        public ImagesHostClient(string baseUrl, Guid contextId)
        {
            _contextId = contextId;
            _baseUrl = baseUrl.Trim('/');
            _service = new ServiceClient(new BasicHttpBinding {
                MaxReceivedMessageSize = 52428800,
                MaxBufferSize = 52428800,
                MaxBufferPoolSize = 52428800,
                
                ReaderQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxDepth = 200000,
                    MaxArrayLength = 52428800,
                    MaxStringContentLength = 52428800,
                    MaxBytesPerRead = 52428800,
                    MaxNameTableCharCount = 52428800,
            } }, new EndpointAddress(_baseUrl + "/Service.svc"));
        }

        public string GetImageUrl(Guid imageId)
        {
            var str = imageId.ToString();
            return $"{_baseUrl}/Img/{_contextId}/{str.Substring(0, 2)}/{str.Substring(2, 4)}/{imageId}.jpg";
        }

        public string GetTmbUrl(Guid imageId)
        {
            var str = imageId.ToString();
            return $"{_baseUrl}/Img/{_contextId}/{str.Substring(0, 2)}/{str.Substring(2, 4)}/{imageId}_tmb.jpg";
        }

        public Folder[] GetFoldersTree()
        {
            return _service.GetFoldersTree(_contextId);
        }

        public Task<Folder[]> GetFoldersTreeAsync()
        {
            return _service.GetFoldersTreeAsync(_contextId);
        }

        public Image[] GetImages(Guid? folderId)
        {
            return _service.GetImages(folderId, _contextId);
        }

        public Task<Image[]> GetImagesAsync(Guid? folderId)
        {
            return _service.GetImagesAsync(folderId, _contextId);
        }

        public Guid AddFolder(string name, Guid? parentId)
        {
            return _service.AddFolder(name, parentId, _contextId);
        }

        public Task<Guid> AddFolderAsync(string name, Guid? parentId)
        {
            return _service.AddFolderAsync(name, parentId, _contextId);
        }

        public Guid UploadImage(string name, byte[] data, Guid? folderId)
        {
            return _service.UploadImage(name, data, folderId, _contextId);
        }

        public Task<Guid> UploadImageAsync(string name, byte[] data, Guid? folderId)
        {
            return _service.UploadImageAsync(name, data, folderId, _contextId);
        }

        public bool RenameFolder(Guid folderId, string newName)
        {
            return _service.RenameFolder(folderId, newName, _contextId);
        }

        public Task<bool> RenameFolderAsync(Guid folderId, string newName)
        {
            return _service.RenameFolderAsync(folderId, newName, _contextId);
        }

        public bool RenameImage(Guid imageId, string newName)
        {
            return _service.RenameImage(imageId, newName, _contextId);
        }

        public Task<bool> RenameImageAsync(Guid imageId, string newName)
        {
            return _service.RenameImageAsync(imageId, newName, _contextId);
        }

        public void DeleteFolder(Guid folderId, Guid? newFolderId)
        {
            _service.DeleteFolder(folderId, newFolderId, _contextId);
        }

        public Task DeleteFolderAsync(Guid folderId, Guid? newFolderId)
        {
            return _service.DeleteFolderAsync(folderId, newFolderId, _contextId);
        }

        public void DeleteImage(Guid imageId)
        {
            _service.DeleteImage(imageId, _contextId);
        }

        public Task DeleteImageAsync(Guid imageId)
        {
            return _service.DeleteImageAsync(imageId, _contextId);
        }
    }
}