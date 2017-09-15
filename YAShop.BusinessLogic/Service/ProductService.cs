using System;
using YAShop.BusinessLogic.DomainModel;

namespace YAShop.BusinessLogic.Service
{
    public class ProductService : AbstractService
    {
        public Product GetBySKU(string sku)
        {
            var product = Registry.Current.Data.Products.Select(p => p.SKU == sku);
            if (product != null && product.Count > 0) return product[0];
            throw new Exception("Product with SKU: " + sku + " not found");
        }

        public decimal GetPrice(string sku, int? qty)
        {
            ;
            return GetBySKU(sku).Price;
        }

        public void Take(string sku, int qty)
        {
            var p = GetBySKU(sku);
            p.QTY -= qty;
            Registry.Current.Data.Products.Save(p);
        }
    }
}