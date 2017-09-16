using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YAShop.BusinessLogic.Presistense
{
    public class IMDBDataProvider<T> : IDataProvider<T> where T : DomainObject, new()
    {
        public IMDBDataProvider()
        {
            Imdb.Add(typeof(T), new Dictionary<ObjectId, DomainObject>());
        }

        public T Find(ObjectId id)
        {
            var dict = Imdb[typeof(T)];
            if (dict.ContainsKey(id)) return (T)dict[id];
            return null;
        }

        public void Save(T subj)
        {
            bool isNew = subj.Id == ObjectId.Empty;
            if (isNew)
            {
                subj.Id = ObjectId.GenerateNewId();
                Imdb[typeof(T)].Add(subj.Id, subj);
            }
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            return Imdb[typeof(T)].Values.Cast<T>().Where(filter.Compile()).ToList();
        }

        static readonly Dictionary<Type, Dictionary<ObjectId, DomainObject>> Imdb = new Dictionary<Type, Dictionary<ObjectId, DomainObject>>();
        public IDataProvider<T> Init()
        {
            return this;
        }


    }
}