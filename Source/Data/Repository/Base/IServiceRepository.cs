using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Data.Repository.Base
{
    public interface IServiceRepository : IRepository<Service, byte>
    {
        [Obsolete("This repository should not have update method.", true)]
        void Update(Service domain);

        [Obsolete("This repository should not have update method.", true)]
        void UpdateCollection(IEnumerable<Service> domains);

        [Obsolete("This repository should not have remove method.", true)]
        void Remove(Service domain);

        [Obsolete("This repository should not have remove method.", true)]
        void RemoveCollection(IEnumerable<Service> domains);

        public Task UpdateActiveProperty(byte serviceId, bool active);
    }
}