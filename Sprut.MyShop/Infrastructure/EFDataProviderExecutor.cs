using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var efTable = _efContext.Set(typeof(T));
            bool isNew = subj.Id == Guid.Empty;
            if (isNew)
            {
                subj.Id = Guid.NewGuid();
                efTable.Add(subj);
            }
            else
            {
                var subjFind = efTable.Find(subj.Id);
                subjFind = subj;
            }
            _efContext.SaveChanges();
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException(); 
        }

        public void Delete(T subj)
        {
            throw new NotImplementedException();
        }

        public List<T> Select(string query, dynamic param)
        {
<<<<<<< HEAD
            // why one line????
            var efTable = _efContext.Set(typeof(T));
            var list = efTable.AsQueryable();
            List<T> returnList = new List<T>();
            foreach (var s in list)
            {
                returnList.Add((T)s);
            }
            return returnList;
        }
        public T Find(Guid id)
        {
            var efTable = _efContext.Set(typeof(T));
            var subj = efTable.Find(id);
            return (T)subj;
=======
            //new[]{new ObjectParameter("Name", "any value")
            //return _efContext.Database.SqlQuery<T>(query, , }).ToList();
            return _efContext.Database.SqlQuery<T>(query).ToList();
        }
        public T Find(Guid id)
        {
            return _efContext.Set<T>().Find(id);
>>>>>>> parent of 98dbada... Merge branch 'master' of https://github.com/NikLegaloff/YAShop
        }

        public T Find(string sku)
        {
            var efTable = _efContext.Set(typeof(T));
            var subj = efTable.Where(c => c.SKU == sku).FirstOrDefault();
            efTable;
            return (T)subj;
        }
    }
}