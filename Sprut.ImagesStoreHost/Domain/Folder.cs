using System;

namespace ImagesStoreHost.Domain
{
    public class Folder
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }

        public Folder()
        {
            Id=Guid.NewGuid();
        }
    }
}
