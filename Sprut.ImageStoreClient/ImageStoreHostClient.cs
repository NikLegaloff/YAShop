using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprut.ImageStoreClient.ImageService;

namespace Sprut.ImageStoreClient
{
    public class ImageStoreHostClient
    {
        private readonly ServiceClient _service;

        public ImageStoreHostClient()
        {
            _service=new ServiceClient();
        }


        public void AddFolder(string name, string path)
        {
            _service.AddFolder(name, path);
        }

        //public Task AddFolderAsync(string name, string path)
        //{
        //    return _service.AddFolderAsync(name, path);
        //}

        public Folder GetFolder(string name, Guid? parentId)
        {
            return _service.GetFolder(name, parentId);
        }

        //public Task<Folder> GetFolderAsync(string name, Guid parentId)
        //{
        //    return _service.GetFolderAsync(name, parentId);
        //}

        public List<Image> GetImagesInFolder(Guid? folderId)
        {
            return _service.GetImagesInFolder(folderId);
        }

        //public Task<List<Image>> GetImagesInFolderAsync(Guid? folderId)
        //{
        //    return _service.GetImagesInFolderAsync(folderId);
        //}

        public List<Folder> GetSubFolders(Guid? folderId)
        {
            return _service.GetSubFolders(folderId);
        }

        //public Task<List<Folder>> GetSubFoldersAsync(Guid? folderId)
        //{
        //    return _service.GetSubFoldersAsync(folderId);
        //}

        public Guid UploadImage(byte[] data, string fileNameOrigin, string folder)
        {
            return _service.UploadImage(data, fileNameOrigin, folder);
        }

        //public Task<Guid> UploadImageAsync(byte[] data, string fileNameOrigin, string folder)
        //{
        //    return _service.UploadImageAsync(data, fileNameOrigin, folder);
        //}

        public string GetImageUrl(System.Guid id)
        {
            return _service.GetImageUrl(id);
        }

        //public Task<string> GetImageUrlAsync(System.Guid id)
        //{
        //    return _service.GetImageUrlAsync(id);
        //}

        public string GetTmbUrl(System.Guid id)
        {
            return _service.GetTmbUrl(id);
        }

        //public Task<string> GetTmbUrlAsync(System.Guid id)
        //{
        //    return _service.GetTmbUrlAsync(id);
        //}

    }
}
