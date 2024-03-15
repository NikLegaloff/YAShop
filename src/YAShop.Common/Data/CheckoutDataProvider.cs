using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.Common.Domain;

namespace YAShop.Common.Data
{
   
  public class CheckoutDataProvider : FileJsonDataProvider<Checkout>
    {
        public CheckoutDataProvider(string? dataPath = null) : base(dataPath)
        {
        }
    }
}
