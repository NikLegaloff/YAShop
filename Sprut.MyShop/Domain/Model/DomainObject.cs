using System;
using System.Runtime.Serialization;

namespace Sprut.MyShop.Domain.Model
{
    [DataContract]
    public class DomainObject
    {
        public DomainObject()
        {
            Id = Guid.Empty;
        }

        public Guid Id { get; set; }
    }
}