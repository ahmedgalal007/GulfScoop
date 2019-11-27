using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gulfsoccer.utilities.PushNotifications.Abstract
{
    public interface IPushNotificationsQueue
    {
        void Enqueue(PushMessage message);

        Task<PushMessage> DequeueAsync(CancellationToken cancellationToken);
    }
}
