using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageStore.Domain
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
