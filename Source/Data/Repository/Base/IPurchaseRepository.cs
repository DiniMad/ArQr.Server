using System;
using System.Collections.Generic;
using Domain;

namespace Data.Repository.Base
{
    public interface IPurchaseRepository : IRepository<Purchase, long>
    {
        [Obsolete("This repository should not have update method.", true)]
        void Update(Purchase domain);

        [Obsolete("This repository should not have update method.", true)]
        void UpdateCollection(IEnumerable<Purchase> domains);

        [Obsolete("This repository should not have remove method.", true)]
        void Remove(Purchase domain);

        [Obsolete("This repository should not have remove method.", true)]
        void RemoveCollection(IEnumerable<Purchase> domains);
    }
}