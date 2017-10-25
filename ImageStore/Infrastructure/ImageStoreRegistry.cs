using System;
using ImageStore.Domain;
using ImageStore.Infrastructure.Providers;

namespace ImageStore.Infrastructure
{
    public class ImageStoreRegistry
    {
        private static ImageStoreRegistry _current;
        public FolderDataProvider Folders;
        public ImageDataProvider Images;


        public ImageStoreRegistry(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            CommonInfrastructureProvider = commonInfrastructureProvider;
            Folders = new FolderDataProvider(new EfDataProviderExecutor<Folder>().Init());
            Images = new ImageDataProvider(new EfDataProviderExecutor<Image>().Init());
        }

        public ICommonInfrastructureProvider CommonInfrastructureProvider { get; }

        public static ImageStoreRegistry  Current
        {
            get
            {
                if (_current == null) throw new Exception("Registry is not initialized");
                return _current;
            }
        }

        public static void Init(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            _current = new ImageStoreRegistry(commonInfrastructureProvider);
        }

    }
}
