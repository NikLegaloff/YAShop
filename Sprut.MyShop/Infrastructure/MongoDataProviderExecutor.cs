using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure
{
    public class MongoDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
        private static IMongoDatabase _db;
        private IMongoCollection<T> _collection;

        public IDataProvider<T> Init()
        {
            if (_db == null)
            {
                var server = new MongoClient("mongodb://127.0.0.1");
                _db = server.GetDatabase("YAShopDB");
            }
            _collection = _db.GetCollection<T>(typeof(T).Name);
            return this;
        }

        public void Save(T subj)
        {
            bool isNew = subj.Id == Guid.Empty;
            if (isNew)
            {
                subj.Id = Guid.NewGuid();
                _collection.InsertOne(subj);
            }
            else
            {
                _collection.ReplaceOne(s => s.Id == subj.Id, subj);
            }
        }

        public T Find(Guid id)
        {
            return _collection.AsQueryable().FirstOrDefault(doc => doc.Id == id);
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            return _collection.FindSync(new ExpressionFilterDefinition<T>(filter)).ToList();
        }

        public void Delete(T subj)
        {
            _collection.DeleteOne(doc => doc.Id == subj.Id);
        }

        public List<T> Select(string query, dynamic param)
        {
            return _collection.AsQueryable().ToList();
        }
    }
}