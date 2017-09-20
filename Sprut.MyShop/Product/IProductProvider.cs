using System.Security.Cryptography.X509Certificates;

namespace Sprut.MyShop
{
    public interface IProductProvider
    {
        Product Get(string sku);
        void Add(Product product);
        void Delete(string sku);
        Product[] GetAll();
        string[,] ImportFromExcel(string filename);

    }
}