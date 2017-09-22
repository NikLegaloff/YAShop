using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure
{
    public interface IDataProvider<T> where T : DomainObject
    {
        T Find(Guid id);
        void Save(T subj);
        void Delete(T subj);
        List<T> Select(string query, dynamic param);
    }
}