using System;
using System.Data;
using YAShop.BusinessLogic.Presistense;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.DomainModel
{
    public class Listing : DomainObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Number{ get; set; }
    }
    public class Product : DomainObject
    {
        [DBField(SqlDbType.NVarChar, 32)]
        public string SKU { get; set; }

        [DBField(SqlDbType.NVarChar, 512)]
        public string Title { get; set; }

        [DBField(SqlDbType.Int)]
        public int QTY { get; set; }

        [DBField(SqlDbType.Decimal)]
        public decimal Price { get; set; }

        [DBField(SqlDbType.NText, 0, true)]
        public string Description { get; set; }

        [DBField(SqlDbType.NVarChar, 512, true)]
        public string Image { get; set; }

        [DBField(SqlDbType.UniqueIdentifier, 0, true)]
        public Guid? CategoryId { get; set; }

        public Category Category
        {
            get { return CategoryId == null ? null : Registry.Current.Data.Categories.Find(CategoryId.Value).Result; }
            set { CategoryId = value?.Id; }
        }
    }
}