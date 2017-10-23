using System;
using System.Collections.Generic;
using ImageStore.Domain;

namespace ImageStore.Infrastructure
{
    public interface IDataProvider<T> where T : DomainObject
    {
        T Find(Guid id);
        void Save(T subj);
        void Delete(T subj);
        List<T> Select(string query, dynamic param);
    }
}