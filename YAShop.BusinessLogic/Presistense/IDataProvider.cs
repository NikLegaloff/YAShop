using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Presistense
{
    public interface IDataProvider<T> where T : DomainObject, new()
    {
        IDataProvider<T> Init();
        Task<T> Find(Guid id);
        Task Save(T subj);
        Task Delete(T subj);
        Task<T[]> Select(string sql=null, dynamic param=null, int? top = null);
        Task<PageData<T>> SelectPage(string query, PagingArgs paging, dynamic param = null, string sortingAlias = null, string extraSorting = null);
    }
}
