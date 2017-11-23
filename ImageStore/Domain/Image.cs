using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.Domain
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
