using System.Collections;

namespace ImageStore.Infrastructure
{
    public interface ICommonInfrastructureProvider
    {
        IDictionary IdentityMap { get; }
        object GetFromSession(string key);
        void PutInSession(string key, object subj);
    }
}