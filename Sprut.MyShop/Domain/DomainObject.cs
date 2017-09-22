using System;

namespace Sprut.MyShop.Domain
{
    public class DomainObject
    {
        public DomainObject()
        {
            Id=Guid.Empty;
        }

        public Guid Id { get; set; }
    }
}