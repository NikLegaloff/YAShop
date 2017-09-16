using System;
using System.Collections.Generic;
using System.Linq;

namespace YAShop.BusinessLogic.Presistense
{
    public class IMDBDataProvider<T> : IDataProvider<T> where T : DomainObject, new()
    {
        public IMDBDataProvider()
        {
            Imdb.Add(typeof(T), new Dictionary<Guid, DomainObject>());
        }

        public T Find(Guid id)
        {
            var dict = Imdb[typeof(T)];
            if (dict.ContainsKey(id)) return (T)dict[id];
            return null;
        }

        public void Save(T subj)
        {
            bool isNew = subj.Id == Guid.Empty;
            if (isNew)
            {
                subj.Id = Guid.NewGuid();
                Imdb[typeof(T)].Add(subj.Id, subj);
            }
        }

        public List<T> Select(Func<T, bool> filter)
        {
            return Imdb[typeof(T)].Values.Cast<T>().Where(filter).ToList();
        }

        static readonly Dictionary<Type, Dictionary<Guid, DomainObject>> Imdb = new Dictionary<Type, Dictionary<Guid, DomainObject>>();
    }
}