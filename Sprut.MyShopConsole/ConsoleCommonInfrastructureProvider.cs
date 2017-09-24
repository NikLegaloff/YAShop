using System;
using System.Collections;
using System.Collections.Generic;
using Sprut.MyShop.Infrastructure;

namespace Sprut.MyShopConsole
{
    public class ConsoleCommonInfrastructureProvider : ICommonInfrastructureProvider
    {
        readonly Dictionary<string, object> _session = new Dictionary<string, object>();
        public object GetFromSession(string key)
        {
            return _session.ContainsKey(key) ? _session[key] : null;
        }

        public void PutInSession(string key, object subj)
        {
            if (_session.ContainsKey(key)) _session[key] = subj; else _session.Add(key, subj);
        }


        private Dictionary<Guid, object> _identityMap { get; } = new Dictionary<Guid, object>();
        public IDictionary IdentityMap => _identityMap;
    }
}