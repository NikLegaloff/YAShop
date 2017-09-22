using System;
using System.Collections.Generic;
using System.Linq;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure
{
    public class InMemoryDataProviderExecutor<T> : IDataProvider<T> where T : DomainObject
    {
        readonly List<T> _subjects = new List<T>();
        public T Find(Guid id)
        {
            return _subjects.FirstOrDefault(i => i.Id == id);
        }

        public void Save(T subj)
        {
            if (!_subjects.Contains(subj)) _subjects.Add(subj);
        }

        public void Delete(T subj)
        {
            if (_subjects.Contains(subj)) _subjects.Remove(subj);
        }

        public List<T> Select(string query, dynamic param)
        {
            return _subjects;
        }
    }
}