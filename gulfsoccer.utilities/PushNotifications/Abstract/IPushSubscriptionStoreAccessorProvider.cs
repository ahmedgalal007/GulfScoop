using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.PushNotifications.Abstract
{
    public interface IPushSubscriptionStoreAccessorProvider
    {
        IPushSubscriptionStoreAccessor GetPushSubscriptionStoreAccessor();
    }
}
