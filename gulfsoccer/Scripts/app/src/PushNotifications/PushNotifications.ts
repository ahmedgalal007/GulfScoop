//let pushServiceWorkerRegistration;

//function registerPushServiceWorker() {
//    navigator.serviceWorker.register('/scripts/service-workers/push-service-worker.js',
//        { scope: '/scripts/service-workers/push-service-worker/' })
//        .then(function (serviceWorkerRegistration) {
//            pushServiceWorkerRegistration = serviceWorkerRegistration;

//            //...

//            console.log('Push Service Worker has been registered successfully');
//        }).catch (function (error) {
//            console.log('Push Service Worker registration has failed: ' + error);
//        });
//};

//function subscribeForPushNotifications() {
//    let applicationServerPublicKey = urlB64ToUint8Array('<Public Key in Base64 Format>'); //Use the public SSL key for registeration

//    pushServiceWorkerRegistration.pushManager.subscribe({
//        userVisibleOnly: true,
//        applicationServerKey: applicationServerPublicKey
//    }).then(function (pushSubscription) {
//        fetch('push-notifications-api/subscriptions', {
//            method: 'POST',
//            headers: { 'Content-Type': 'application/json' },
//            body: JSON.stringify(pushSubscription)
//        }).then(function (response) {
//            if (response.ok) {
//                console.log('Successfully subscribed for Push Notifications');
//            } else {
//                console.log('Failed to store the Push Notifications subscription on server');
//            }
//        }).catch(function (error) {
//            console.log('Failed to store the Push Notifications subscription on server: ' + error);
//        });

//        // ...
//    }).catch(function (error) {
//        if (Notification.permission === 'denied') {
//            // ...
//        } else {
//            console.log('Failed to subscribe for Push Notifications: ' + error);
//        }
//    });
//};

//self.addEventListener('push', function (event) {
//    event.waitUntil(self.registration.showNotification('Demo.AspNetCore.PushNotifications', {
//        body: event.data.text(),
//        icon: '/images/push-notification-icon.png'
//    }));
//});

//function unsubscribeFromPushNotifications() {
//    pushServiceWorkerRegistration.pushManager.getSubscription().then(function (pushSubscription) {
//        if (pushSubscription) {
//            pushSubscription.unsubscribe().then(function () {
//                fetch('push-notifications-api/subscriptions?endpoint='
//                    + encodeURIComponent(pushSubscription.endpoint),
//                    { method: 'DELETE' }
//                ).then(function (response) {
//                    if (response.ok) {
//                        console.log('Successfully unsubscribed from Push Notifications');
//                    } else {
//                        console.log('Failed to discard the Push Notifications subscription from server');
//                    }
//                }).catch(function (error) {
//                    console.log('Failed to discard the Push Notifications subscription from server: ' + error);
//                });

//                // ...
//            }).catch(function (error) {
//                console.log('Failed to unsubscribe from Push Notifications: ' + error);
//            });
//        }
//    });
//};