using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class DataProvider<T> : IDataProvider<T> where T : DomainObject
    {
        private readonly IDataProvider<T> _executor;

        public DataProvider(IDataProvider<T> executor)
        {
            _executor = executor;
        }

        public T Find(Guid id)
        {
            return _executor.Find(id);
        }

        public void Save(T subj)
        {
            _executor.Save(subj);
        }

        public void Delete(T subj)
        {
            _executor.Delete(subj);
        }

        public List<T> Select(string query=null, dynamic param=null)
        {
            return _executor.Select(query, param);
        }
    }
}