using YAShop.BusinessLogic.Presistense;

namespace YAShop.BusinessLogic.Service.Products.Viewing
{
    public class ProductViewRow : DomainObject
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int QTY { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}