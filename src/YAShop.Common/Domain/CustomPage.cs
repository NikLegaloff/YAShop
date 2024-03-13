using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.Common.Domain
{
    public class CustomPage: DomainObject
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Alias { get; set; }
    }
}
