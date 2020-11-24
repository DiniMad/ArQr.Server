using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ArQr.Core
{
    public record CacheExpireNotification(string CacheKey) : INotification
    {
    }

    public class CacheExpireNotificationHandler : INotificationHandler<CacheExpireNotification>
    {
        public async Task Handle(CacheExpireNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.CacheKey);
        }
    }
}