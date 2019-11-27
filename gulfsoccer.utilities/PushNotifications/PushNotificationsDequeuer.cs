using gulfsoccer.utilities.PushNotifications.Abstract;
using Lib.Net.Http.WebPush;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gulfsoccer.utilities.PushNotifications
{
    internal class PushNotificationsDequeuer
    {
        private readonly IPushSubscriptionStoreAccessorProvider _subscriptionStoreAccessorProvider;
        private readonly IPushNotificationsQueue _messagesQueue;
        private readonly IPushNotificationService _notificationService;
        private readonly CancellationTokenSource _stopTokenSource = new CancellationTokenSource();

        private Task _dequeueMessagesTask;

        public PushNotificationsDequeuer(IPushNotificationsQueue messagesQueue, IPushSubscriptionStoreAccessorProvider subscriptionStoreAccessorProvider, IPushNotificationService notificationService)
        {
            _subscriptionStoreAccessorProvider = subscriptionStoreAccessorProvider;
            _messagesQueue = messagesQueue;
            _notificationService = notificationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _dequeueMessagesTask = Task.Run(DequeueMessagesAsync);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _stopTokenSource.Cancel();

            return Task.WhenAny(_dequeueMessagesTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        private async Task DequeueMessagesAsync()
        {
            while (!_stopTokenSource.IsCancellationRequested)
            {
                PushMessage message = await _messagesQueue.DequeueAsync(_stopTokenSource.Token);

                if (!_stopTokenSource.IsCancellationRequested)
                {
                    using (IPushSubscriptionStoreAccessor subscriptionStoreAccessor = _subscriptionStoreAccessorProvider.GetPushSubscriptionStoreAccessor())
                    {
                        await subscriptionStoreAccessor.PushSubscriptionStore.ForEachSubscriptionAsync(
                            (PushSubscription subscription) =>
                            {
                                // Fire-and-forget 
                                _notificationService.SendNotificationAsync(subscription, message, _stopTokenSource.Token);
                            }, 
                            _stopTokenSource.Token
                        );
                    }

                }
            }

        }
    }
}
