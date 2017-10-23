using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageStore.Domain
{
    [Table("Folder")]
    public class Folder : DomainObject
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }
}
