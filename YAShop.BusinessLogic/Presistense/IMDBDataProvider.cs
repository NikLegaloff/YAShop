using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Presistense
{
    public class IMDBDataProvider<T> : IDataProvider<T> where T : DomainObject, new()
    {
        private static readonly Dictionary<Type, Dictionary<Guid, DomainObject>> Imdb =
            new Dictionary<Type, Dictionary<Guid, DomainObject>>();

        public IMDBDataProvider()
        {
            Imdb.Add(typeof(T), new Dictionary<Guid, DomainObject>());
        }

        public IDataProvider<T> Init()
        {
            throw new NotImplementedException();
        }

        public Task<T> Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Save(T subj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T subj)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> Select(string sql = null, dynamic param = null, int? top = null)
        {
            throw new NotImplementedException();
        }

        public Task<PageData<T>> SelectPage(string query, PagingArgs paging, dynamic param = null,
            string sortingAlias = null,
            string extraSorting = null)
        {
            throw new NotImplementedException();
        }
    }
}