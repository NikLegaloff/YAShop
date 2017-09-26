using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprut.MyShop.Infrastructure;
using MongoDB.Bson.Serialization.Attributes;

namespace Sprut.MyShop.Domain
{
    public class Product : DomainObject
    {
        [BsonId]
        public string SKU { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string Image { get; set; }
        public string Descripton { get; set; }
        public Guid CategoryId { get; set; }
        //public Category Category => Registry.Current.Categories.Find(CategoryId);


    }

}
