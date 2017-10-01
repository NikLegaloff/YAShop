using System;
using System.Collections;
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
            var subj = (T)IdentityMap[id.ToString()];
            if (subj == null)
            {
                subj = _executor.Find(id);
                Registry.Current.CommonInfrastructureProvider.IdentityMap.Add(id.ToString(), subj);
            }
            return subj;
        }

        public void Save(T subj)
        {
            if (!IdentityMap.Contains(subj.Id)) IdentityMap.Add(subj.Id, subj);
            _executor.Save(subj);
        }

        public void Delete(T subj)
        {
            if (IdentityMap.Contains(subj.Id)) IdentityMap.Remove(subj.Id);
            _executor.Delete(subj);
        }

        public List<T> Select(string query = null, dynamic param = null)
        {
            var result = _executor.Select(query, param);
            if (result != null) foreach (var subj in result) if (!IdentityMap.Contains(subj.Id)) IdentityMap.Add(subj.Id, subj);
            return result;
        }
        private static IDictionary IdentityMap => Registry.Current.CommonInfrastructureProvider.IdentityMap;

    }
}