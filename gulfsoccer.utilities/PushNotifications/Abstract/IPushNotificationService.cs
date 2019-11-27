using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;

namespace gulfsoccer.utilities.PushNotifications.Abstract
{
    /// <summary>
    /// With such API sending push message can be represented as a single call.
    /// await _subscriptionStore.ForEachSubscriptionAsync(
    ///     (PushSubscription subscription) => _notificationService.SendNotification(subscription, "<Push Message>")
    /// );
    /// </summary>
    public interface IPushNotificationService
    {
        string PublicKey { get; }

        Task SendNotificationAsync(PushSubscription subscription, PushMessage message);

        Task SendNotificationAsync(PushSubscription subscription, PushMessage message, CancellationToken cancellationToken);
    }
}
