using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using YAShop.BusinessLogic.Presistense.MSSQL;

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
                subj.Id = Guid.Empty;
                Imdb[typeof(T)].Add(subj.Id, subj);
            }
        }

        public List<T> Select(Expression<Func<T, bool>> filter)
        {
            return Imdb[typeof(T)].Values.Cast<T>().Where(filter.Compile()).ToList();
        }

        static readonly Dictionary<Type, Dictionary<Guid, DomainObject>> Imdb = new Dictionary<Type, Dictionary<Guid, DomainObject>>();
        public IDataProvider<T> Init()
        {
            return this;
        }

        public void Delete(T subj)
        {
            var dict = Imdb[typeof(T)];
            if (dict.ContainsKey(subj.Id)) dict.Remove(subj.Id);
        }

        public T[] Select(string sql, dynamic param = null, int? top=null)
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