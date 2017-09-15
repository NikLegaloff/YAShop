using System;
using System.Collections.Generic;

namespace YAShop.BusinessLogic.Presistense
{
    public interface IDataProvider<T> where T : DomainObject, new()
    {
        T Find(Guid id);
        void Save(T subj);
        List<T> Select(Func<T, bool> filter);

    }
}
