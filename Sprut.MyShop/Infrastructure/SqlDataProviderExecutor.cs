using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure.EFDatabase;

namespace Sprut.MyShop.Infrastructure
{
    public class SqlDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
        EFContext db = new EFContext();
        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(T subj)
        {
            throw new NotImplementedException();
        }

        public void Delete(T subj)
        {
            throw new NotImplementedException();
        }

        public List<T> Select(string query=null, dynamic param=null)
        {
            
            throw new NotImplementedException();
        }
    }
}