using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace YAShop.BusinessLogic.Presistense
{
    public interface IDataProvider<T> where T : DomainObject, new()
    {
        IDataProvider<T> Init();
        T Find(ObjectId id);
        void Save(T subj);
        List<T> Select(Expression<Func<T, bool>> filter);

    }
}
