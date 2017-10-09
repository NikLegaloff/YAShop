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

        private static IDictionary IdentityMap => Registry.Current.CommonInfrastructureProvider.IdentityMap;

        public T Find(Guid id)
        {
            var subj = (T) IdentityMap[id.ToString()];
            if (subj == null)
            {
                subj = _executor.Find(id);
                Registry.Current.CommonInfrastructureProvider.IdentityMap.Add(id.ToString(), subj);
                AfterLoad(subj);
            }
            return subj;
        }

        public void Save(T subj)
        {
            BeforeSave(subj);
            _executor.Save(subj);
            if (!IdentityMap.Contains(subj.Id)) IdentityMap.Add(subj.Id, subj); // save new in IdMap
        }

        public void Delete(T subj)
        {
            if (IdentityMap.Contains(subj.Id)) IdentityMap.Remove(subj.Id); // remove from IdMap
            _executor.Delete(subj);
        }

        public List<T> Select(string query = null, dynamic param = null)
        {
            var selected = _executor.Select(query, param);
            if (selected != null)
            {
                var result = new List<T>();
                foreach (var subj in selected)
                    if (!IdentityMap.Contains(subj.Id))
                    {
                        IdentityMap.Add(subj.Id, subj);
                        result.Add(subj);
                        AfterLoad(subj); // call it only for new loaded subjects
                    }
                    else
                    {
                        result.Add(IdentityMap[subj.Id]);
                    }
                return result;
            }
            return null;
        }

        protected virtual void AfterLoad(T subj)
        {
        }

        protected virtual void BeforeSave(T subj)
        {
        }
    }
}