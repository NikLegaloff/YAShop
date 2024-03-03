using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using YAShop.Common;
using YAShop.Common.Infrastructure;

namespace YAShop.Web.Storefront.Code
{
    public class WebCommonInfrastructureProvider : ICommonInfrastructureProvider
    {
        public static WebApplication App { get; set; }
        public static HttpContext HttpContext => App.Services.GetService<IHttpContextAccessor>()?.HttpContext;
        public static IDistributedCache Cache => App.Services.GetService<IDistributedCache>();

        public T? CacheGet<T>(string key)
        {
            var json = Cache.GetString(key);
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }

        public void CacheRemove(string key)
        {
            Cache.Remove(key);
        }

        public void CacheAdd(string key, object subj, int expireInMinutes)
        {

            Cache.SetStringAsync(key, subj.ToMinJSON(), new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expireInMinutes) });
        }


        public T? GetFromSession<T>(string key)
        {
            if (IdentityMap.ContainsKey("Session:" + key)) return (T?)IdentityMap["Session:" + key];
            var value = HttpContext.Session.GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public void PutInSession(string key, object? subj)
        {
            if (subj == null) HttpContext.Session.Remove(key);
            else
            {
                HttpContext.Session.SetString(key, subj.ToMinJSON());
                IdentityMap.Add("Session:" + key, subj);
            }
        }

        public IDictionary<object, object?> IdentityMap => HttpContext.Items;
    }

}
