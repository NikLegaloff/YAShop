using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure
{
    public class EfDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
        private EfContext _efContext;
        public IDataProvider<T> Init()
        {
            if (_efContext == null)
            {
                _efContext = new EfContext();
            }
            return this;
        }

        public void Save(T subj)
        {
            var efTable = _efContext.Set<T>();
            bool isNew = subj.Id == Guid.Empty;
            if (isNew)
            {
                subj.Id = Guid.NewGuid();
                efTable.Add(subj);
            }
            _efContext.SaveChanges();
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException(); 
        }

        public void Delete(T subj)
        {
            _efContext.Database.ExecuteSqlCommand("DELETE FROM " + subj.GetType().Name + "s where id='" + subj.Id + "'");
            _efContext.SaveChanges();
        }

        public List<T> Select(string query, dynamic param)
        {
<<<<<<< HEAD
            //new[]{new ObjectParameter("Name", "any value")
            //return _efContext.Database.SqlQuery<T>(query, , }).ToList();
            return _efContext.Database.SqlQuery<T>(query).ToList();
=======
            List<object> ppp = new List<object>();
            Type type = param.GetType();
            foreach (var p in type.GetProperties())
            {
                ppp.Add(new ObjectParameter(p.Name, p.GetValue(param)));
            }
            return _efContext.Database.SqlQuery<T>(query, ppp.ToArray()).ToList();
>>>>>>> 7a26487f3718ef600bb6d64216d7b4f5912c01c1
        }
        public T Find(Guid id)
        {
            return _efContext.Set<T>().Find(new object[] {new ObjectParameter("Id", id)});
        }

    }
}