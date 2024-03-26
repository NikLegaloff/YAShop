using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.Common.Infrastructure
{
    public interface ICommonInfrastructureProvider
    {
        IDictionary<object, object?> IdentityMap { get; }
        
        T? GetFromSession<T>(string key);
        void PutInSession(string key, object subj);

        T? CacheGet<T>(string key);
        void CacheAdd(string key, object subj, int expireInMinutes);
        void CacheRemove(string key);
    }
}
