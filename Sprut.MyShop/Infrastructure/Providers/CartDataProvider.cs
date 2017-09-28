using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure.Providers
{
    class CartDataProvider : DataProvider<Cart>
    {
        public CartDataProvider(IDataProvider<Cart> executor) : base(executor) { }
    }
}
