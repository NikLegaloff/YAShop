using System;
using YAShop.BusinessLogic.Presistense.MSSQL;
using YAShop.BusinessLogic.Service.Product.Viewing;

namespace YAShop.BusinessLogic.Service.Product
{
    public class ProductService : AbstractService
    {
        public DomainModel.Product GetBySKU(string sku)
        {
            var product = Registry.Current.Data.Products.Select(" where SKU=@sku", new {sku});
            if (product != null && product.Length > 0) return product[0];
            throw new Exception("Product with SKU: " + sku + " not found");
        }

        public decimal GetPrice(string sku, int? qty)
        {
            return GetBySKU(sku).Price;
        }

        public void Take(string sku, int qty)
        {
            var p = GetBySKU(sku);
            p.QTY -= qty;
            Registry.Current.Data.Products.Save(p);
        }

        public PageData<ProductViewRow> SelectPage(ProductFilter filter)
        {
            return new ProductViewer(filter).SelectPage();
        }
    }
}