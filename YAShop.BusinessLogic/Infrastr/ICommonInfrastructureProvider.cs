using System.Collections;

namespace YAShop.BusinessLogic.Infrastr
{
    public interface ICommonInfrastructureProvider
    {
        IDictionary IdentityMap { get; }
        object GetFromSession(string key);
        void PutInSession(string key, object subj);
    }
}