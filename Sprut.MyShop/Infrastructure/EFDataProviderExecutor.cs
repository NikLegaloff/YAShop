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
            //new[]{new ObjectParameter("Name", "any value")
            //return _efContext.Database.SqlQuery<T>(query, , }).ToList();
            return _efContext.Database.SqlQuery<T>(query).ToList();
        }
        public T Find(Guid id)
        {
            return _efContext.Set<T>().Find(id);
        }

    }
}