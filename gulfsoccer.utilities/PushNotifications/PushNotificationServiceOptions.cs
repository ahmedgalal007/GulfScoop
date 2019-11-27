using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.PushNotifications
{
    public class PushNotificationServiceOptions
    {
        public string Subject { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }
    }
}
