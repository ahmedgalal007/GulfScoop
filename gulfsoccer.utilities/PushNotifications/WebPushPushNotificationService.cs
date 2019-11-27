using gulfsoccer.utilities.PushNotifications.Abstract;
using Lib.Net.Http.WebPush;
using System;
using System.Collections.Generic;
using System.Text;
using WebPush = Lib.Net.Http.WebPush;

namespace gulfsoccer.utilities.PushNotifications
{
    internal class WebPushPushNotificationService : IPushNotificationService
    {
        private readonly PushNotificationServiceOptions _options;
        private readonly WebPushClient _pushClient;

        public string PublicKey { get { return _options.PublicKey; } }

        public WebPushPushNotificationService(IOptions<PushNotificationServiceOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;

            _pushClient = new WebPushClient();
            _pushClient.SetVapidDetails(_options.Subject, _options.PublicKey, _options.PrivateKey);
        }
        public void SendNotification(PushSubscription subscription, string payload)
        {
            var webPushSubscription = WebPush.PushSubscription(
            subscription.Endpoint,
            subscription.Keys["p256dh"],
            subscription.Keys["auth"]);

            _pushClient.SendNotification(webPushSubscription, payload);
        }
    }
}
