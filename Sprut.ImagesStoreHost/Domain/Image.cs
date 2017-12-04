using System;

namespace ImagesStoreHost.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid? Folder { get; set; }
        public string Name { get; set; }
        public Image()
        {
            Id = Guid.NewGuid();
        }
    }
}
