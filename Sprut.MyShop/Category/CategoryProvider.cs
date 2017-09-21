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


        List<string[]> _cattree = new List<string[]>();
        int _r = 0;

        public CategoryProvider()
        {
            _categorys.Add(new Category { Id = 1, Parent_id = 0, Name = "CatRoot1" });
            _categorys.Add(new Category { Id = 2, Parent_id = 0, Name = "CatRoot2" });
            _categorys.Add(new Category { Id = 3, Parent_id = 0, Name = "CatRoot3" });
            _categorys.Add(new Category { Id = 4, Parent_id = 1, Name = "Cat4-1" });
            _categorys.Add(new Category { Id = 5, Parent_id = 3, Name = "Cat5-3" });
            _categorys.Add(new Category { Id = 6, Parent_id = 2, Name = "Cat6-2" });
            _categorys.Add(new Category { Id = 7, Parent_id = 1, Name = "Cat7-1" });
            _categorys.Add(new Category { Id = 8, Parent_id = 1, Name = "Cat8-1" });
            _categorys.Add(new Category { Id = 9, Parent_id = 3, Name = "Cat9-3" });
            _categorys.Add(new Category { Id = 10, Parent_id = 4, Name = "Cat10-4" });
        }

        public void Add(Category category)
        {
            //автоматическое ID не реалезованно
            _categorys.Add(category);
        }

<<<<<<< HEAD
        public List<string[]> TextCategoryTree(int rootParentid)
=======
        public List<Category> GetTree()
        {
            
        }
        public List<string[]> TextCategoryTree(int root_parentid)
>>>>>>> 2628bf27ff08288e8c52de0f99de37f8dd7e55e1
        {
            _cattree.Clear();
            _r = 0;
            BuildTree(rootParentid);
            return (_cattree);
        }


        void BuildTree(int rootParentid)
        {
            foreach (var cat in _categorys.FindAll(c => c.Parent_id == rootParentid))
            {
                _cattree.Add(new string[] { new string('-',_r) + " " + cat.Name, cat.Id.ToString() });
                _r++;
                BuildTree(cat.Id);
                _r--;
            }
        }
    }

}
