using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Core;
using ArQr.Interface;
using MediatR;
using StackExchange.Redis;

namespace ArQr.Infrastructure
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer connection, IPublisher publisher)
        {
            _database = connection.GetDatabase();

            SetupExpireEvent(connection.GetSubscriber(), publisher);
        }

        public async Task<bool> KeyExistAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task AddToUniqueListAsync(string key, string value)
        {
            await _database.SetAddAsync(key, value);
        }

        public async Task<IEnumerable<string>> GetUniqueListAsync(string key)
        {
            var setMembers = await _database.SetMembersAsync(key);
            if (setMembers is null) return Enumerable.Empty<string>();

            var values = setMembers.Select(redisValue => redisValue.ToString());
            return values;
        }

        public async Task<long> GetCountOfListAsync(string listKey)
        {
            return await _database.SetLengthAsync(listKey);
        }

        public async Task DeleteKeyAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        private void SetupExpireEvent(ISubscriber subscriber, IPublisher publisher)
        {
            _database.Execute("config", "set", "notify-keyspace-events", "EA");
            subscriber.Subscribe("__key*__:expired",
                                 (_, key) => publisher.Publish(new CacheExpireNotification(key)));
        }
    }
}