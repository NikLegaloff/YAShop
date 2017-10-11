using System;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Presistense.MSSQL;
using YAShop.BusinessLogic.Service.Products.Viewing;

namespace YAShop.BusinessLogic.Service.Products
{
    public class ProductService : AbstractService
    {
        public async Task<DomainModel.Product> GetBySKU(string sku)
        {
            var product = await Registry.Current.Data.Products.Select(" where SKU=@sku", new {sku});
            if (product != null && product.Length > 0) return product[0];
            throw new Exception("Product with SKU: " + sku + " not found");
        }

        public decimal GetPrice(string sku, int? qty)
        {
            return GetBySKU(sku).Result.Price;
        }

        public async Task<bool> Take(string sku, int qty)
        {
            var p = await GetBySKU(sku);
            if (p.QTY < qty) return false;
            p.QTY -= qty;
            await Registry.Current.Data.Products.Save(p);
            return true;
        }

        public async Task<PageData<ProductViewRow>> SelectPage(ProductFilter filter)
        {
            return await new ProductViewer(filter).SelectPage();
        }
    }
}