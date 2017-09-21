using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    
    class CategoryProvider : ICategoryProvider
    {
        List<Category> _categorys = new List<Category>();


        List<Category> _cattree = new List<Category>();
        int _r = 0;
        Guid i0 = Guid.NewGuid();

        public CategoryProvider()
        {
            
            var i1 = Guid.NewGuid();
            var i2 = Guid.NewGuid();
            var i3 = Guid.NewGuid();
            var i4 = Guid.NewGuid();
            var i5 = Guid.NewGuid();
            var i6 = Guid.NewGuid();
            var i7 = Guid.NewGuid();
            var i8 = Guid.NewGuid();
            var i9 = Guid.NewGuid();
            var i10 = Guid.NewGuid();
            

            _categorys.Add(new Category { Id = i1, ParentId = i0, Name = "CatRoot1" });
            _categorys.Add(new Category { Id = i2, ParentId = i0, Name = "CatRoot2" });
            _categorys.Add(new Category { Id = i3, ParentId = i0, Name = "CatRoot3" });
            _categorys.Add(new Category { Id = i4, ParentId = i4, Name = "Cat4-1" });
            _categorys.Add(new Category { Id = i5, ParentId = i3, Name = "Cat5-3" });
            _categorys.Add(new Category { Id = i6, ParentId = i2, Name = "Cat6-2" });
            _categorys.Add(new Category { Id = i7, ParentId = i1, Name = "Cat7-1" });
            _categorys.Add(new Category { Id = i8, ParentId = i1, Name = "Cat8-1" });
            _categorys.Add(new Category { Id = i9, ParentId = i3, Name = "Cat9-3" });
            _categorys.Add(new Category { Id = i10, ParentId = i4, Name = "Cat10-4" });
        }

        public void Add(Category category)
        {
            //автоматическое ID не реалезованно
            _categorys.Add(category);
        }

        public List<Category> GetTree()
        {
            _cattree.Clear();
            _r = 0;
            BuildTree(i0);
            return (_cattree);
        }


        void BuildTree(Guid rootParentid)
        {
            foreach (var cat in _categorys.FindAll(c => c.ParentId == rootParentid))
            {
                _cattree.Add(new Category { Name=new string('-',_r) + " " + cat.Name, Id = cat.Id});
                _r++;
                BuildTree(cat.Id);
                _r--;
            }
        }
    }

}
