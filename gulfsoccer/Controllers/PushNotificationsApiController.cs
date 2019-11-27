using gulfsoccer.utilities.PushNotifications;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using System.Web.Mvc;
using PushMessage = gulfsoccer.utilities.PushNotifications.PushMessage;

namespace gulfsoccer.Controllers
{
    public class PushNotificationsApiController : Controller
    {
        private readonly IPushSubscriptionStore _subscriptionStore;
        private readonly IPushNotificationService _notificationService;
        private readonly IPushNotificationsQueue _pushNotificationsQueue;

        public PushNotificationsApiController(IPushSubscriptionStore subscriptionStore, IPushNotificationService notificationService, IPushNotificationsQueue pushNotificationsQueue)
        {
            _subscriptionStore = subscriptionStore;
            _notificationService = notificationService;
            _pushNotificationsQueue = pushNotificationsQueue;
        }

        // GET push-notifications-api/public-key
        [HttpGet("public-key")]
        public ContentResult GetPublicKey()
        {
            return Content(_notificationService.PublicKey, "text/plain");
        }

        // POST push-notifications-api/subscriptions
        [HttpPost("subscriptions")]
        public async Task<IActionResult> StoreSubscription([FromBody]PushSubscription subscription)
        {
            await _subscriptionStore.StoreSubscriptionAsync(subscription);

            return Content(null);
        }

        // DELETE push-notifications-api/subscriptions?endpoint={endpoint}
        [HttpDelete()]
        public async Task<IActionResult> DiscardSubscription(string endpoint)
        {
            await _subscriptionStore.DiscardSubscriptionAsync(endpoint);

            return Content(null);
        }

        // POST push-notifications-api/notifications
        [HttpPost("notifications")]
        public IActionResult SendNotification([FromBody]PushMessageViewModel message)
        {
            _pushNotificationsQueue.Enqueue(new PushMessage(message.Notification)
            {
                Topic = message.Topic,
                Urgency = message.Urgency
            });

            return Content(null);
        }
    }
}
}
