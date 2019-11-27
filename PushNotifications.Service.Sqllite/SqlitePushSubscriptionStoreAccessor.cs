﻿using Microsoft.Extensions.DependencyInjection;

using gulfsoccer.utilities.PushNotifications.Abstract;

namespace PushNotifications.Service.Sqllite
{
    internal class SqlitePushSubscriptionStoreAccessor : IPushSubscriptionStoreAccessor
    {
        private IServiceScope _serviceScope;

        public IPushSubscriptionStore PushSubscriptionStore { get; private set; }

        public SqlitePushSubscriptionStoreAccessor(IPushSubscriptionStore pushSubscriptionStore)
        {
            PushSubscriptionStore = pushSubscriptionStore;
        }

        public SqlitePushSubscriptionStoreAccessor(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
            PushSubscriptionStore = _serviceScope.ServiceProvider.GetService<IPushSubscriptionStore>();
        }

        public void Dispose()
        {
            PushSubscriptionStore = null;

            if (!(_serviceScope is null))
            {
                _serviceScope.Dispose();
                _serviceScope = null;
            }
        }
    }
}
