using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.Common.Infrastructure;

namespace YAShop.Console;

public class StaticCommonInfrProvider : ICommonInfrastructureProvider
{
    private static readonly Dictionary<object, object?> Map = new Dictionary<object, object?>();
    private static readonly Hashtable Session = new Hashtable();


    IDictionary<object, object?> ICommonInfrastructureProvider.IdentityMap => Map;

    public T? GetFromSession<T>(string key)
    {
        lock (Session) return Session.Contains(key) ? (T)Session[key] : default;
    }

    public void PutInSession(string key, object subj)
    {
        lock (Session) if (Session.ContainsKey(key)) Session[key] = subj; else Session.Add(key, subj);
    }

    public void WipeAll()
    {
        lock (Cache) Cache.Clear();
        lock (Session) Session.Clear();
        lock (Map) Map.Clear();

    }

    private static readonly Hashtable Cache = new();
    public T? CacheGet<T>(string key)
    {
        if (Cache.ContainsKey(key)) return (T)Cache[key]; else return default;
    }
    public void CacheAdd(string key, object subj, int expireInMinutes)
    {
        if (Cache.ContainsKey(key)) Cache[key] = subj; else Cache.Add(key, subj);
    }
    public void CacheRemove(string key)
    {
        if (Cache.ContainsKey(key)) Cache.Remove(key);
    }

}
