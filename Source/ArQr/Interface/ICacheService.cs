using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface ICacheService
    {
        public Task                      SetAsync(string key, string value, TimeSpan? expiry = null);
        public Task<string?>             GetAsync(string key);
        public Task                      AddToUniqueList(string key, string value);
        public Task<IEnumerable<string>> GetUniqueList(string key);
        public Task                      SubscribeToExpireEventAsync(Action<string> onExpired);
    }
}