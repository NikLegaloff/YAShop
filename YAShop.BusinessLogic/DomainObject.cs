using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace YAShop.BusinessLogic
{
    public class DomainObject
    {
        public DomainObject()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; set; }

        public override string ToString()
        {
            return this.GetType().Name + "\n" +  string.Join(", ", this.GetType().GetProperties().Select(t => t.Name + ": " + t.GetValue(this)));
        }
    }
}