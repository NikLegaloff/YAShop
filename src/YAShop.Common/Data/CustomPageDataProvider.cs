using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.Common.Domain;

namespace YAShop.Common.Data
{
   
  public class CustomPageDataProvider : FileJsonDataProvider<CustomPage>
    {
        public CustomPageDataProvider(string? dataPath = null) : base(dataPath)
        {
        }
    }
}
