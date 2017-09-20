using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sprut.MyShop
{
    class TestProductProvider : IProductProvider
    {
        List<Product> _products = new List<Product>();  //было readonly

        public TestProductProvider()
        {
            _products.Add(new Product
            {
                SKU = "S1",
                Title = "Test product 1",
                Price = 12.5m,
                Qty = 5,
                Descripton = "Product 1 description"
            });
            _products.Add(new Product
            {
                SKU = "S2",
                Title = "Test product 2",
                Price = 12.0m,
                Qty = 10,
                Descripton = "Product 2 description"
            });

        }

        public Product Get(string sku)
        {
            return _products.FirstOrDefault(p => p.SKU == sku);
        }

        public Product[] GetAll()
        {
            // Возваращаем массив что бы никто не изменил его
            return _products.ToArray();
        }

        public void Add(Product product)
        {
            var _product = Get(product.SKU);


            if (_product != null)
            {
                var _product_index = _products.IndexOf(_product);
                _products[_product_index] = product;


            }
            else { _products.Add(product); }
        }

        public void Delete(string sku)
        {
            var _product= _products.FirstOrDefault(p => p.SKU == sku);
            _products.Remove(_product);
        }

        public string[,] ImportFromExcel(string filename)
        {
            //имя файла для теста
            //filename = "e:\\temp\\MyShopTest.xlsx";
            //Создаем приложение
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Открываем книгу
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filename);
            //Выбираем лист
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Sheets[1];
            int usedRows = xlWorkSheet.UsedRange.Rows.Count;
            int usedCols = xlWorkSheet.UsedRange.Columns.Count;
            string[,] xlArray = new string[usedRows,usedCols];
            for(int y = 0; y < usedRows; y++)
            {
                for(int x=0; x < usedCols; x++)
                {
                    xlArray[y,x]= (xlWorkSheet.Cells[y+1, x+1]).Text.ToString();
                }
            }
            //Удаляем приложение (выходим из экселя) - ато будет висеть в процессах!
            xlApp.Quit();
            return xlArray;
        }

    }
}