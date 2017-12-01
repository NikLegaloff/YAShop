using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.Domain
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
