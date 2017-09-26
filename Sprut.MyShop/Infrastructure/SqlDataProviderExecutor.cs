using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Sprut.MyShop.Domain;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Sprut.MyShop.Infrastructure
{
    public class SqlDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public async void Save(T subj)
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