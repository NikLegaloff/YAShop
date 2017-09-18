using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public interface IOrderProvider
    {
        string StartOrder(Cart cart);
        Order GetOrder(string number);
        void SetOrderState(string number, OrderState state);
    }
}
