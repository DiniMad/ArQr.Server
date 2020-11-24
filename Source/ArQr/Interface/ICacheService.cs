using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArQr.Interface
{
    public interface ICacheService
    {
        public Task<bool>                KeyExist(string key);
        public Task                      SetAsync(string key, string value, TimeSpan? expiry = null);
        public Task<string?>             GetAsync(string key);
        public Task                      AddToUniqueListAsync(string key, string value);
        public Task<IEnumerable<string>> GetUniqueListAsync(string key);
        public Task<long>                GetCountOfListAsync(string listKey);
        public Task                      SubscribeToExpireEventAsync(Action<string> onExpired);
    }
}