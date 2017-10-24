using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Domain.Model;

namespace Sprut.MyShop.Infrastructure
{
    public class EfDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
        private EfContext _efContext;

        public void Save(T subj)
        {
            var efTable = _efContext.Set(typeof(T));
            //var efTable = _efContext.Set<T>();
            var isNew = subj.Id == Guid.Empty;
            if (isNew)
            {
                subj.Id = Guid.NewGuid();
                efTable.Add(subj);
            }
            else
            {
                _efContext.Entry(subj).State = EntityState.Modified;
            }
            _efContext.SaveChanges();
            _efContext.Entry(subj).State = EntityState.Detached;

        }

        public void Delete(T subj)
        {
            _efContext.Database.ExecuteSqlCommand("delete from [" + subj.GetType().Name + "] where Id='" + subj.Id + "'");
            _efContext.SaveChanges();
        }

        public List<T> Select(string query, dynamic param)
        {
            // allow to call with query = " where SKU=@sku" or " join category on product.CategoryId=Category.Id where ..." or full sql like "select Product.* from ..."
            if (!(query ?? "").StartsWith("select"))
                query = "select [" + typeof(T).Name + "].* from [" + typeof(T).Name + "] " + (query ?? "");

            //this working  return _efContext.Database.SqlQuery<T>(query, new SqlParameter("sku","NewSKu1")).ToList();


            if (param == null) return _efContext.Database.SqlQuery<T>(query).ToList();
            
            SqlParameter[] pp;
            if (param is ExpandoObject)
                pp = ((ExpandoObject) param).Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
            else
                pp = ((Type)param.GetType()).GetProperties().Select(p => new SqlParameter(p.Name, p.GetValue(param))).ToArray();

            return _efContext.Database.SqlQuery<T>(query, pp).ToList();
            ;
        }

        public T Find(Guid id)
        {
            return _efContext.Set<T>().Find(id);
        }

        public IDataProvider<T> Init()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfContext>());

            if (_efContext == null)
                _efContext = new EfContext();
            return this;
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}