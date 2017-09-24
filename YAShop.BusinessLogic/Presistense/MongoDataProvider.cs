using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Presistense
{
    public class MongoDataProvider<T> : IDataProvider<T> where T : DomainObject, new()
    {

        public T Find(Guid id)
        {
            return _collection.AsQueryable().FirstOrDefault(doc => doc.Id == id);
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
                _collection.ReplaceOne(s=>s.Id==subj.Id, subj);
            }
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            return _collection.FindSync(new ExpressionFilterDefinition<T>(filter)).ToList();
        }

        private static IMongoDatabase _db;
        private IMongoCollection<T> _collection;

        public IDataProvider<T> Init()
        {
            if (_db == null)
            {
                var server = new MongoClient("mongodb://127.0.0.1");
                _db = server.GetDatabase("YAShop");
            }
            _collection = _db.GetCollection<T>(typeof(T).Name);
            // drop all records
            _collection.DeleteMany(subj => true);
            return this;
        }

        public void Delete(T subj)
        {
            _collection.DeleteOne(s => s.Id == subj.Id);
        }

        public T[] Select(string sql, dynamic param = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public PageData<T> SelectPage(string query, PagingArgs paging, dynamic param = null, string sortingAlias = null,
            string extraSorting = null)
        {
            throw new NotImplementedException();
        }
    }
}