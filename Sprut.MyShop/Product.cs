using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public class Product 
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string Image { get; set; }
        public string Descripton { get; set; }
        public Guid CategoryId { get; set; }


    }
}
