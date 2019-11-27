using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.PushNotifications.Abstract
{
    public interface IPushSubscriptionStoreAccessor : IDisposable
    {
        IPushSubscriptionStore PushSubscriptionStore { get; }
    }
}
