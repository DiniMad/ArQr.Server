using System.Threading.Tasks;
using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class ServiceRepository : Repository<Service, byte>, IServiceRepository
    {
        public ServiceRepository(DbContext context) : base(context)
        {
        }

        public async Task UpdateActiveProperty(byte serviceId, bool active)
        {
            var service = await GetAsync(serviceId);
            if (service is null) return;
            service.Active = active;
            Context.Set<Service>().Update(service);
        }
    }
}