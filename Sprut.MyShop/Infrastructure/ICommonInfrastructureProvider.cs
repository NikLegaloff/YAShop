using System.Collections;

namespace Sprut.MyShop.Infrastructure
{
    public interface ICommonInfrastructureProvider
    {
        object GetFromSession(string key);
        void PutInSession(string key, object subj);

        IDictionary IdentityMap { get; }

    }
}