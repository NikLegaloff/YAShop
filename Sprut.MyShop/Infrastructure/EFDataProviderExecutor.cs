using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
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
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfContext>());

            if (_efContext == null)
            {
                _efContext = new EfContext();
            }
            return this;
        }

        public void Save(T subj)
        {
            //var efTable = _efContext.Set(typeof(T));
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
            _efContext.Database.ExecuteSqlCommand("delete from [" + subj.GetType().Name + "] where Id='" + subj.Id + "'");
            _efContext.SaveChanges();
        }

        public List<T> Select(string query, dynamic param)
        {
            // allow to call with query = " where SKU=@sku" or " join category on product.CategoryId=Category.Id where ..." or full sql like "select Product.* from ..."
            if (!(query ?? "").StartsWith("select")) query = "select [" + typeof(T).Name + "].* from [" + typeof(T).Name + "] " + (query ?? "");
            
            //this working  return _efContext.Database.SqlQuery<T>(query, new SqlParameter("sku","NewSKu1")).ToList();
            
            if (param == null)
            {
                return _efContext.Database.SqlQuery<T>(query).ToList();
            }
            else
            {
            return _efContext.Database.SqlQuery<T>(query, (SqlParameter)param ).ToList();
            }
        }
        public T Find(Guid id)
        {
            return _efContext.Set<T>().Find(id);
        }

    }
}