using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public interface ICategoryProvider
    {
        void Add(Category category);
        List<Category> GetTree();
    }
}
