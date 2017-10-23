using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.Domain
{
    [Table("Image")]
    public class Image:DomainObject
    {
        public Guid Folder { get; set; }
        public string Name { get; set; }
    }
}
