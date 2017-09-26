using Sprut.MyShop.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Infrastructure.EFDatabase
{
    class EFContext : DbContext
    {
        public EFContext() : base("DbConnection") { }
        public DbSet<EFProduct> Products { get; set; }
    }
}
