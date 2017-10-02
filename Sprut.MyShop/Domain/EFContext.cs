using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Domain
{
    public class EfContext : DbContext
    {
        //public EfContext() : base("Data Source=.;Initial Catalog=YAShopDBv2;Integrated Security=True") { }
        public EfContext() : base("YAShopDBv1") { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        
    }
}
