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
        public EfContext() : base("Data Source=SPR\\SQLEXPRESS;Initial Catalog=YAShopDBv1;Integrated Security=True") { }
        //public EfContext() : base("d:\\Temp\\YAShopDBv1.mdf") { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        
    }
}
