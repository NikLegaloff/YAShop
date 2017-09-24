using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Presistense
{
    public interface IDataProvider<T> where T : DomainObject, new()
    {
        IDataProvider<T> Init();
        T Find(Guid id);
        void Save(T subj);
        void Delete(T subj);
        List<T> Select(Expression<Func<T, bool>> filter);
        T[] Select(string sql=null, dynamic param=null, int? top = null);

        PageData<T> SelectPage(string query, PagingArgs paging, dynamic param = null, string sortingAlias = null, string extraSorting = null);

    }
}
