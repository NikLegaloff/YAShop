using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public interface IOrderProvider
    {
        //void CreateOrder();
        void AddInOrder(string sku, int Qty);
        Order GetOrder();
    }
}
