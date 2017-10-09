using System.Collections;

namespace Sprut.MyShop.Infrastructure
{
    public interface ICommonInfrastructureProvider
    {
        IDictionary IdentityMap { get; }
        object GetFromSession(string key);
        void PutInSession(string key, object subj);
    }
}