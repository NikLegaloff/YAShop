using System;

namespace ImageStore.Domain
{
    public class DomainObject
    {
        public DomainObject()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; set; }
    }
}