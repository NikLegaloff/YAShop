using System;
using System.ComponentModel.DataAnnotations;

namespace Sprut.MyShop.Domain
{
    public class DomainObject
    {
        public DomainObject()
        {
            Id=Guid.Empty;
        }
        [Key]
        public Guid Id { get; set; }
    }
}