using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Interface;
using StackExchange.Redis;

namespace ArQr.Infrastructure
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase   _database;
        private readonly ISubscriber _subscriber;

        public CacheService(IConnectionMultiplexer connection)
        {
            _database   = connection.GetDatabase();
            _subscriber = connection.GetSubscriber();
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task AddToUniqueList(string key, string value)
        {
            await _database.SetAddAsync(key, value);
        }

        public async Task<IEnumerable<string>> GetUniqueList(string key)
        {
            var setMembers = await _database.SetMembersAsync(key);
            if (setMembers is null) return Enumerable.Empty<string>();

            var values = setMembers.Select(redisValue => redisValue.ToString());
            return values;
        }

        public async Task SubscribeToExpireEventAsync(Action<string> onExpired)
        {
            await SetupExpireEvent();
            await _subscriber.SubscribeAsync("__key*__:expired", (_, key) => onExpired(key));
        }

        private async Task SetupExpireEvent()
        {
            await _database.ExecuteAsync("psubscribe", "'__key*__:expired'");
        }
    }
}