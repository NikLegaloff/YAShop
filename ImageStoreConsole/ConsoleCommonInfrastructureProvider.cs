using System;
using System.Collections;
using System.Collections.Generic;
using ImageStore.Infrastructure;

namespace ImageStoreConsole
{
    public class ConsoleCommonInfrastructureProvider : ICommonInfrastructureProvider
    {
        private readonly Dictionary<string, object> _session = new Dictionary<string, object>();


        private Dictionary<Guid, object> _identityMap { get; } = new Dictionary<Guid, object>();

        public object GetFromSession(string key)
        {
            return _session.ContainsKey(key) ? _session[key] : null;
        }

        public void PutInSession(string key, object subj)
        {
            if (_session.ContainsKey(key)) _session[key] = subj;
            else _session.Add(key, subj);
        }

        public IDictionary IdentityMap => _identityMap;
    }
}