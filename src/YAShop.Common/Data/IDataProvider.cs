using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public interface IDataProvider<T> where T:DomainObject
{

    T[] SelectAll();
    T? Find(Guid id);
    void Save(T subj);
    void Delete(Guid id);
}