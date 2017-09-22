using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Domain
{
    public class Category : DomainObject
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; }
    }
}
