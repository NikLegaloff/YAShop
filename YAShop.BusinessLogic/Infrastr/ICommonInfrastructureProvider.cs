using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace YAShop.BusinessLogic.Infrastr
{
    public interface ICommonInfrastructureProvider
    {
        object GetFromSession(string key);
        void PutInSession(string key, object subj);

        Dictionary<string, object> TransactionCache { get; }

    }
}