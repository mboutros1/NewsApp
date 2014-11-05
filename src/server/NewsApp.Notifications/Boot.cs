using System;
using System.IO;
using NewsAppModel.Infrastructure;
using NLog;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;

namespace NewsApp.Notifications
{
    public static class AppController
    {
        private static PushBroker _push;

        public static PushBroker PushProBroker
        {
            get
            {
                if (_push == null)
                {
                    throw new Exception("Please call start method before accessing the push notification broker");
                }
                return _push;
            }
        }


        public static void Start()
        {
            _push = new PushBroker();
            _push.OnNotificationSent += NotificationSent;
            _push.OnChannelException += ChannelException;
            _push.OnServiceException += ServiceException;
            _push.OnNotificationFailed += NotificationFailed;
            _push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            _push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            _push.OnChannelCreated += ChannelCreated;
            _push.OnChannelDestroyed += ChannelDestroyed;
            byte[] appleCert = File.ReadAllBytes(AppSettings.Instance.P12FileLocation);
            //IMPORTANT: If you are using a Development provisioning Profile, you must use the Sandbox push notification server 
            //  (so you would leave the first arg in the ctor of ApplePushChannelSettings as 'false')
            //  If you are using an AdHoc or AppStore provisioning profile, you must use the Production push notification server
            //  (so you would change the first arg in the ctor of ApplePushChannelSettings to 'true')
            _push.RegisterAppleService(new ApplePushChannelSettings(appleCert, AppSettings.Instance.P12FilePassword));

            //---------------------------
            // ANDROID GCM NOTIFICATIONS
            //---------------------------
            //Configure and start Android GCM
            //IMPORTANT: The API KEY comes from your Google APIs Console App, under the API Access section, 
            //  by choosing 'Create new Server key...'
            //  You must ensure the 'Google Cloud Messaging for Android' service is enabled in your APIs Console
            //    _push.RegisterGcmService(new GcmPushChannelSettings("YOUR Google API's Console API Access  API KEY for Server Apps HERE"));
            //Fluent construction of an Android GCM Notification
            //IMPORTANT: For Android you MUST use your own RegistrationId here that gets generated within your Android app itself!
            //-------------------------
            // WINDOWS NOTIFICATIONS
            //-------------------------
            //Configure and start Windows Notifications
            //_push.RegisterWindowsService(new WindowsPushChannelSettings("WINDOWS APP PACKAGE NAME HERE","WINDOWS APP PACKAGE SECURITY IDENTIFIER HERE", "CLIENT SECRET HERE"));
		
        }

        public static void End()
        {
            if (_push != null) _push.StopAllServices();
        }

        private static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId,
            INotification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Logger.Info("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " +
                              newSubscriptionId + " -> " + notification);
        }

        private static void NotificationSent(object sender, INotification notification)
        {
            Logger.Info("Sent: " + sender + " -> " + notification);
        }

        private static void NotificationFailed(object sender, INotification notification,
            Exception notificationFailureException)
        {
            Logger.Info("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " +
                              notification);
        }
        private static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {
            Logger.Info("Channel Exception: " + sender + " -> " + exception);
        }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //   
        private static void ServiceException(object sender, Exception exception)
        {
            Logger.Info("Service Exception: " + sender + " -> " + exception);
        }

        private static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId,
            DateTime timestamp, INotification notification)
        {
            Logger.Info("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
        }

        private static void ChannelDestroyed(object sender)
        {
            Logger.Info("Channel Destroyed for: " + sender);
        }

        private static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Logger.Info("Channel Created for: " + sender);
        }
    }
}