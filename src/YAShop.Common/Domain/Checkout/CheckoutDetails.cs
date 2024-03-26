using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.Common.Domain.Checkout
{
    public class Address
    {
        public Guid CityId{ get; set; }
        public string AddressLine{ get; set; }
        public string Phone { get; set; }
    }

    public class CheckoutDetails : OrderClientDetails
    {
        public string Email { get; set; }

    }
    public class OrderClientDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address{ get; set; }
        public string? Comment{ get; set; }
    }
}
