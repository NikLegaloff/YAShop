using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using YAShop.BusinessLogic.Infrastr;

namespace YAShop.JobExecutor
{
    public class ProgrCommonInfrProvider : ICommonInfrastructureProvider
    {
        public object GetFromSession(string key)
        {
            throw new NotImplementedException();
        }

        public void PutInSession(string key, object subj)
        {
            throw new NotImplementedException();
        }

        static readonly Dictionary<int, Hashtable> _maps = new Dictionary<int, Hashtable>();

        public static void ResetIdentityMap(int id)
        {
            lock (_maps)
            {
                _maps.Remove(id);
            }
        }
        public IDictionary IdentityMap
        {
            get
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                lock (_maps)
                {
                    if (!_maps.ContainsKey(id)) _maps.Add(id, new Hashtable());
                    return _maps[id];
                    
                }
            }
        }
    }
}