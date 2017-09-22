using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure
{
    public class SqlDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
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

        public List<T> Select(string query, dynamic param)
        {
            throw new NotImplementedException();
        }
    }
}