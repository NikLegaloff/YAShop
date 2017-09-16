using System.Text;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Presistense;

namespace YAShop.BusinessLogic.DomainModel
{
    public class Product : DomainObject
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int QTY { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
