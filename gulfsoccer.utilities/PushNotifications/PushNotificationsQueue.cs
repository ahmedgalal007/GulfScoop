using gulfsoccer.utilities.PushNotifications.Abstract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gulfsoccer.utilities.PushNotifications
{
    internal class PushNotificationsQueue : IPushNotificationsQueue
    {
        private readonly ConcurrentQueue<PushMessage> _messages = new ConcurrentQueue<PushMessage>();
        private readonly SemaphoreSlim _messageEnqueuedSignal = new SemaphoreSlim(0);

        public void Enqueue(PushMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _messages.Enqueue(message);

            _messageEnqueuedSignal.Release();
        }

        public async Task<PushMessage> DequeueAsync(CancellationToken cancellationToken)
        {
            await _messageEnqueuedSignal.WaitAsync(cancellationToken);

            _messages.TryDequeue(out PushMessage message);

            return message;
        }
    }
}
