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

            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("YAShopDB");
            var collection = database.GetCollection<Product>("product");
            var testProduct1 = new Product{SKU="SKU1",Title="SK1Title",Price = 12,Descripton="SK1Description",Qty = 21};
            await collection.InsertOneAsync(testProduct1);
            
        }

        public void Delete(T subj)
        {
            throw new NotImplementedException();
        }

        public List<T> Select(string query=null, dynamic param=null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("YAShopDB");
            var collection = database.GetCollection<Product>("product");
            var filter = new BsonDocument();
            var products = collection.Find(filter).ToList();
            return products;
        }
    }
}