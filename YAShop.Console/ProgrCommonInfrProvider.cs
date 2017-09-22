using System;
using System.Collections;
using System.Collections.Generic;
using YAShop.BusinessLogic.Infrastr;

namespace YAShop.ConsoleApp
{
    public class ProgrCommonInfrProvider : ICommonInfrastructureProvider
    {
        readonly Dictionary<string, object> session = new Dictionary<string, object>();
        public object GetFromSession(string key)
        {
            return session.ContainsKey(key) ? session[key] : null;
        }

        public void PutInSession(string key, object subj)
        {
            if (session.ContainsKey(key)) session[key] = subj; else session.Add(key, subj);
        }


        Dictionary<string, object> _identityMap { get; } = new Dictionary<string, object>();
        public IDictionary IdentityMap => _identityMap;
    }
}