using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.BusinessLogic.Infrastr
{
    public class Infrastructure
    {

        public Infrastructure(ICommonInfrastructureProvider common)
        {
            Common = common;
        }


        public ICommonInfrastructureProvider Common { get; }
    }
}
